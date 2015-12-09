using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;
using DFWV.WorldClasses.HistoricalFigureClasses;

namespace DFWV.WorldClasses.HistoricalEventClasses
{
    public class HeAddHfhfLink : HistoricalEvent
    {
        private int? Hfid { get; }
        private HistoricalFigure Hf { get; set; }
        private int? HfidTarget { get; }
        private HistoricalFigure HfTarget { get; set; }

        override public Point Location => Point.Empty;

        public int? LinkType { get; set; }

        public HfLink HfLink { get; set; }
        public HfLink HfLink2 { get; set; }

        public override IEnumerable<HistoricalFigure> HFsInvolved
        {
            get
            {
                yield return Hf;
                yield return HfTarget;
            }
        }
        public HeAddHfhfLink(XDocument xdoc, World world)
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
                        Hfid = valI;
                        break;
                    case "hfid_target":
                        HfidTarget = valI;
                        break;

                    default:
                        DfxmlParser.UnexpectedXmlElement(xdoc.Root.Name.LocalName + "\t" + Types[Type], element, xdoc.Root.ToString());
                        break;
                }
            }
        }

        internal override void Link()
        {
            base.Link();
            if (Hfid.HasValue && World.HistoricalFigures.ContainsKey(Hfid.Value))
                Hf = World.HistoricalFigures[Hfid.Value];
            if (HfidTarget.HasValue && World.HistoricalFigures.ContainsKey(HfidTarget.Value))
                HfTarget = World.HistoricalFigures[HfidTarget.Value];
        }

        internal override void Process()
        {
            base.Process();
            var matched = false;

            if (Hf?.HfLinks != null)
            {
                foreach (var hfLinkList in Hf.HfLinks)
                {
                    foreach (var hflink in hfLinkList.Value.Where(hflink => hflink.LinkedHfid == HfidTarget))
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
            if (HfTarget?.HfLinks == null) return;
            foreach (var hfLinkList in HfTarget.HfLinks)
            {
                foreach (var hflink in hfLinkList.Value.Where(hflink => hflink.LinkedHfid == Hfid))
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
                        if (!HfLink.LinkTypes.Contains(val))
                            HfLink.LinkTypes.Add(val);
                        LinkType = HfLink.LinkTypes.IndexOf(val);
                        break;
                    default:
                        DfxmlParser.UnexpectedXmlElement(xdoc.Root.Name.LocalName + "\t" + Types[Type], element, xdoc.Root.ToString());
                        break;
                }
            }
        }
        protected override void WriteDataOnParent(MainForm frm, Control parent, ref Point location)
        {
            EventLabel(frm, parent, ref location, "HF:", Hf);
            EventLabel(frm, parent, ref location, "Target:", HfTarget);
            if (HfLink != null)
                EventLabel(frm, parent, ref location, "Type:", HfLink.LinkTypes[HfLink.LinkType]);
            else if (LinkType.HasValue)
                EventLabel(frm, parent, ref location, "Type:", HfLink.LinkTypes[LinkType.Value]);
        }

        protected override string LegendsDescription() //Matched
        {
            var timestring = base.LegendsDescription();

            switch (HfLink != null ? 
                HfLink.LinkTypes[HfLink.LinkType] :
                (LinkType.HasValue ? HfLink.LinkTypes[LinkType.Value] : string.Empty))
            {
                case "spouse":
                    return
                        $"{timestring} {Hf} {"married"} {HfTarget?.ToString() ?? "an unknown creature"}.";
                case "prisoner":
                    return
                        $"{timestring} {Hf} {"imprisoned"} {HfTarget?.ToString() ?? "an unknown creature"}.";
                case "apprentice":
                    return
                        $"{timestring} {Hf} {"became master of the"} {HfTarget?.Race?.ToString().ToLower() ?? ""} {HfTarget?.ToString() ?? "an unknown creature"}.";
                case "deity":
                    return
                        $"{timestring} {Hf} {"began worshipping"} {HfTarget?.ToString() ?? "an unknown creature"}.";
                default:
                    return $"{timestring} {Hf} {"UNKNOWN"} {HfTarget}.";

            }
        }

        internal override string ToTimelineString()
        {
            var timelinestring = base.ToTimelineString();

            return
                $"{timelinestring} {Hf?.ToString() ?? Hfid.ToString()} {" linked to "} {HfTarget?.ToString() ?? HfidTarget.ToString()}.";
        }

        internal override void Export(string table)
        {
            base.Export(table);


            table = GetType().Name;

            var vals = new List<object>
            {
                Id, 
                Hfid, 
                HfidTarget,
                LinkType.DBExport(HfLink.LinkTypes)
            };

            Database.ExportWorldItem(table, vals);

        }

    }
}
