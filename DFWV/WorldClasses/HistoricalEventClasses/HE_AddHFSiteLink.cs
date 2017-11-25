using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;
using DFWV.WorldClasses.EntityClasses;
using DFWV.WorldClasses.HistoricalFigureClasses;

namespace DFWV.WorldClasses.HistoricalEventClasses
{
    public class HE_AddHFSiteLink : HistoricalEvent
    {
        private int? SiteId { get; }
        private Site Site { get; set; }

        private int? HfId { get; set; }
        public HistoricalFigure Hf { get; set; }
        private int? StructureId { get; set; }
        public Structure Structure { get; set; }
        private int? EntityId { get; set; }
        public Entity Entity { get; set; }

        private int? LinkType { get; set; }

        public HFSiteLink HfSiteLink { get; set; }

        override public Point Location => Site.Location;

        public override IEnumerable<HistoricalFigure> HFsInvolved
        {
            get { yield return Hf; }
        }
        public override IEnumerable<Entity> EntitiesInvolved
        {
            get { yield return Entity; }
        }
        public HE_AddHFSiteLink(XDocument xdoc, World world)
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
                    case "site_id":
                        SiteId = valI;
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
            if (Hf?.SiteLinks != null)
            {
                foreach (var siteLinkList in Hf.SiteLinks)
                {
                    foreach (var siteLink in siteLinkList.Value.Where(siteLink => siteLink.Site == Site && LinkType == siteLink.LinkType))
                    {
                        siteLink.AddEvent = this;
                        HfSiteLink = siteLink;
                        matched = true;
                        break;
                    }
                    if (matched)
                        break;
                }
            }

            if (Structure != null)
            {
                if (Structure.Events == null)
                    Structure.Events = new List<HistoricalEvent>();
                Structure.Events.Add(this);
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
                    case "site":
                        break;
                    case "structure":
                        StructureId = valI;
                        break;
                    case "histfig":
                        HfId = valI;
                        break;
                    case "civ":
                        EntityId = valI;
                        break;
                    case "link_type":
                        val = val.Replace('_', ' ');
                        if (val == "home site abstract building")
                            val = "home structure";
                        if (!HFSiteLink.LinkTypes.Contains(val))
                            HFSiteLink.LinkTypes.Add(val);
                        LinkType = HFSiteLink.LinkTypes.IndexOf(val);
                        break;
                    default:
                        DFXMLParser.UnexpectedXmlElement(xdoc.Root.Name.LocalName + "\t" + Types[Type], element, xdoc.Root.ToString());
                        break;
                }
            }
        }


        protected override void WriteDataOnParent(MainForm frm, Control parent, ref Point location)
        {
            EventLabel(frm, parent, ref location, "Site:", Site);
            EventLabel(frm, parent, ref location, "HF:", Hf);
            EventLabel(frm, parent, ref location, "Structure:", Structure);
            EventLabel(frm, parent, ref location, "Civ:", Entity);
            if (HfSiteLink != null)
                EventLabel(frm, parent, ref location, "Type:",
                    HFSiteLink.LinkTypes[HfSiteLink.LinkType]);
            if (LinkType != null)
                EventLabel(frm, parent, ref location, "Type:",
                    HFSiteLink.LinkTypes[LinkType.Value]);
        }

        protected override string LegendsDescription()
        {
            var timestring = base.LegendsDescription();


            if (LinkType.HasValue)
            {
                switch (HFSiteLink.LinkTypes[LinkType.Value])
                {
                    case "hangout":
                    case "seat of power":
                        return
                            $"{timestring} {Hf} ruled from {(Structure.Name != null ? Structure.ToString() : "UNKNOWN")} of {Entity} in {Site.AltName}.";
                    case "home site realization building":
                    case "home structure":
                        return
                            $"{timestring} {Hf} took up residance in {(Structure.Name != null ? Structure.ToString() : "UNKNOWN")} of {Entity} in {Site.AltName}.";
                    default:
                        return
                            $"{timestring} {"UNKNOWN"} became {HFSiteLink.LinkTypes[LinkType.Value]} of {Site.AltName}.";
                }
            }



            if (Structure != null && Entity != null && Hf != null)
                return
                    $"{timestring} {Hf} ruled from {(Structure.Name != null ? Structure.ToString() : "UNKNOWN")} of {Entity} in {Site.AltName}.";
            
            return
                $"{timestring} {"UNKNOWN"} became {(LinkType.HasValue ? HFSiteLink.LinkTypes[LinkType.Value] : "UNKNOWN")} of {Site.AltName}.";
        }

        internal override string ToTimelineString()
        {
            //TODO: Incorporate new data
            var timelinestring = base.ToTimelineString();

            return $"{timelinestring} Added Site Link to {Site.AltName}.";
        }

        internal override void Export(string table)
        {
            base.Export(table);

            table = GetType().Name;
            var vals = new List<object>
            {
                Id, 
                SiteId.DBExport(),
                HfId.DBExport(),
                EntityId.DBExport(),
                StructureId.DBExport(),
                LinkType.DBExport(HFSiteLink.LinkTypes)
            };

            Database.ExportWorldItem(table, vals);
        }
    }
}
