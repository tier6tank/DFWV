using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;
using DFWV.WorldClasses.HistoricalFigureClasses;

namespace DFWV.WorldClasses.HistoricalEventClasses
{
    public class HE_AddHFHFLink : HistoricalEvent
    {
        private int? HfId { get; }
        private HistoricalFigure Hf { get; set; }
        private int? HfId_Target { get; }
        private HistoricalFigure Hf_Target { get; set; }

        override public Point Location => Point.Empty;

        public int? LinkType { get; set; }

        public HFLink HfLink { get; set; }
        public HFLink HfLink2 { get; set; }

        public override IEnumerable<HistoricalFigure> HFsInvolved
        {
            get
            {
                yield return Hf;
                yield return Hf_Target;
            }
        }
        public HE_AddHFHFLink(XDocument xdoc, World world)
            : base(xdoc, world)
        {
            foreach (var element in xdoc.Root.Elements())
            {
                var val = element.Value;
                int valI;
                int.TryParse(val, out valI);

                switch (element.Name.LocalName)
                {
                    case "id":
                    case "year":
                    case "seconds72":
                    case "type":
                        break;
                    case "hfid":
                        HfId = valI;
                        break;
                    case "hfid_target":
                        HfId_Target = valI;
                        break;

                    default:
                        DFXMLParser.UnexpectedXmlElement(xdoc.Root.Name.LocalName + "\t" + Types[Type], element, xdoc.Root.ToString());
                        break;
                }
            }
        }

        internal override void Process()
        {
            base.Process();
            var matched = false;

            if (Hf?.HfLinks != null)
            {
                foreach (var hfLinkList in Hf.HfLinks)
                {
                    foreach (var hflink in hfLinkList.Value.Where(hflink => hflink.LinkedHfid == HfId_Target))
                    {
                        hflink.AddEvent = this;
                        HfLink = hflink;
                        matched = true;
                        break;
                    }
                    if (matched)
                        break;
                }
            }
            matched = false;
            if (Hf_Target?.HfLinks == null) return;
            foreach (var hfLinkList in Hf_Target.HfLinks)
            {
                foreach (var hflink in hfLinkList.Value.Where(hflink => hflink.LinkedHfid == HfId))
                {
                    hflink.AddEvent = this;
                    HfLink2 = hflink;
                    matched = true;
                    break;
                }
                if (matched)
                    break;
            }
        }

        internal override void Plus(XDocument xdoc)
        {
            foreach (var element in xdoc.Root.Elements())
            {
                var val = element.Value;
                int valI;
                int.TryParse(val, out valI);

                switch (element.Name.LocalName)
                {
                    case "id":
                    case "type":
                        break;
                    case "histfig1":
                    case "histfig2":
                    case "hf":
                    case "hf_target":
                        break;
                    case "link_type":
                        if (!HFLink.LinkTypes.Contains(val))
                            HFLink.LinkTypes.Add(val);
                        LinkType = HFLink.LinkTypes.IndexOf(val);
                        break;
                    default:
                        DFXMLParser.UnexpectedXmlElement(xdoc.Root.Name.LocalName + "\t" + Types[Type], element, xdoc.Root.ToString());
                        break;
                }
            }
        }
        protected override void WriteDataOnParent(MainForm frm, Control parent, ref Point location)
        {
            EventLabel(frm, parent, ref location, "HF:", Hf);
            EventLabel(frm, parent, ref location, "Target:", Hf_Target);
            if (HfLink != null)
                EventLabel(frm, parent, ref location, "Type:", HFLink.LinkTypes[HfLink.LinkType]);
            else if (LinkType.HasValue)
                EventLabel(frm, parent, ref location, "Type:", HFLink.LinkTypes[LinkType.Value]);
        }

        protected override string LegendsDescription()
        {
            var timestring = base.LegendsDescription();

            switch (HfLink != null ? 
                HFLink.LinkTypes[HfLink.LinkType] :
                (LinkType.HasValue ? HFLink.LinkTypes[LinkType.Value] : string.Empty))
            {
                case "spouse":
                    return
                        $"{timestring} {Hf} {"married"} {Hf_Target?.ToString() ?? "an unknown creature"}.";
                case "prisoner":
                    return
                        $"{timestring} {Hf} {"imprisoned"} {Hf_Target?.ToString() ?? "an unknown creature"}.";
                case "apprentice":
                    return
                        $"{timestring} {Hf} {"became master of the"} {Hf_Target?.Race?.ToString().ToLower() ?? ""} {Hf_Target?.ToString() ?? "an unknown creature"}.";
                case "deity":
                    return
                        $"{timestring} {Hf} {"began worshipping"} {Hf_Target?.ToString() ?? "an unknown creature"}.";
                default:
                    return $"{timestring} {Hf} {"UNKNOWN"} {Hf_Target}.";

            }
        }

        internal override string ToTimelineString()
        {
            var timelinestring = base.ToTimelineString();

            return
                $"{timelinestring} {Hf?.ToString() ?? HfId.ToString()} {" linked to "} {Hf_Target?.ToString() ?? HfId_Target.ToString()}.";
        }

        internal override void Export(string table)
        {
            base.Export(table);


            table = GetType().Name;

            var vals = new List<object>
            {
                Id, 
                HfId, 
                HfId_Target,
                LinkType.DBExport(HFLink.LinkTypes)
            };

            Database.ExportWorldItem(table, vals);

        }

    }
}
