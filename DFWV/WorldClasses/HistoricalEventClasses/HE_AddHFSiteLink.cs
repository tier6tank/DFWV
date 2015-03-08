using System;
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
        private int? SiteID { get; set; }
        private Site Site { get; set; }

        private int? HFID { get; set; }
        public HistoricalFigure HF { get; set; }
        private int? StructureID { get; set; }
        public Structure Structure { get; set; }
        private int? CivID { get; set; }
        public Entity Civ { get; set; }

        private int? LinkType { get; set; }

        public HFSiteLink HFSiteLink { get; set; }

        override public Point Location { get { return Site.Location; } }

        public override IEnumerable<HistoricalFigure> HFsInvolved
        {
            get { yield return HF; }
        }
        public override IEnumerable<Entity> EntitiesInvolved
        {
            get { yield return Civ; }
        }
        public HE_AddHFSiteLink(XDocument xdoc, World world)
            : base(xdoc, world)
        {
            foreach (var element in xdoc.Root.Elements())
            {
                var val = element.Value;
                int valI;
                Int32.TryParse(val, out valI);

                switch (element.Name.LocalName)
                {
                    case "id":
                    case "year":
                    case "seconds72":
                    case "type":
                        break;
                    case "site_id":
                        SiteID = valI;
                        break;

                    default:
                        DFXMLParser.UnexpectedXMLElement(xdoc.Root.Name.LocalName + "\t" + Types[Type], element, xdoc.Root.ToString());
                        break;
                }
            }
        }

        internal override void Link()
        {
            base.Link();
            if (SiteID.HasValue && World.Sites.ContainsKey(SiteID.Value))
            {
                Site = World.Sites[SiteID.Value];
                if (StructureID.HasValue)
                {
                    if (Site.GetStructure(StructureID.Value) == null)
                        Site.AddStructure(new Structure(Site, StructureID.Value,World));
                    Structure = Site.GetStructure(StructureID.Value);
                }
            }

            if (HFID.HasValue && World.HistoricalFigures.ContainsKey(HFID.Value))
                HF = World.HistoricalFigures[HFID.Value];
            if (CivID.HasValue && World.Entities.ContainsKey(CivID.Value))
                Civ = World.Entities[CivID.Value];
        }


        internal override void Process()
        {
            base.Process();

            var matched = false;
            if (HF != null && HF.SiteLinks != null)
            {
                foreach (var siteLinkList in HF.SiteLinks)
                {
                    foreach (var siteLink in siteLinkList.Value.Where(siteLink => siteLink.Site == Site && LinkType == siteLink.LinkType))
                    {
                        siteLink.AddEvent = this;
                        HFSiteLink = siteLink;
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
                Int32.TryParse(val, out valI);

                switch (element.Name.LocalName)
                {
                    case "id":
                    case "type":
                        break;
                    case "site":
                        break;
                    case "structure":
                        StructureID = valI;
                        break;
                    case "histfig":
                        HFID = valI;
                        break;
                    case "civ":
                        CivID = valI;
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
                        DFXMLParser.UnexpectedXMLElement(xdoc.Root.Name.LocalName + "\t" + Types[Type], element, xdoc.Root.ToString());
                        break;
                }
            }
        }


        protected override void WriteDataOnParent(MainForm frm, Control parent, ref Point location)
        {
            EventLabel(frm, parent, ref location, "Site:", Site);
            EventLabel(frm, parent, ref location, "HF:", HF);
            EventLabel(frm, parent, ref location, "Structure:", Structure);
            EventLabel(frm, parent, ref location, "Civ:", Civ);
            if (HFSiteLink != null)
                EventLabel(frm, parent, ref location, "Type:",
                    HFSiteLink.LinkTypes[HFSiteLink.LinkType]);
            if (LinkType != null)
                EventLabel(frm, parent, ref location, "Type:",
                    HFSiteLink.LinkTypes[LinkType.Value]);
        }

        protected override string LegendsDescription() //Matched
        {
            var timestring = base.LegendsDescription();


            if (LinkType.HasValue)
            {
                switch (HFSiteLink.LinkTypes[LinkType.Value])
                {
                    case "hangout":
                    case "seat of power":
                        return string.Format("{0} {1} ruled from {2} of {3} in {4}.",
                            timestring, HF,
                            Structure.Name != null ? Structure.ToString() : "UNKNOWN", Civ, Site.AltName);
                    case "home site realization building":
                    case "home structure":
                        return string.Format("{0} {1} took up residance in {2} of {3} in {4}.",
                            timestring, HF,
                            Structure.Name != null ? Structure.ToString() : "UNKNOWN", Civ, Site.AltName);
                    default:
                        return string.Format("{0} {1} became {2} of {3}.",
                            timestring, "UNKNOWN",
                            HFSiteLink.LinkTypes[LinkType.Value], Site.AltName);
                }
            }



            if (Structure != null && Civ != null && HF != null)
                return string.Format("{0} {1} ruled from {2} of {3} in {4}.",
                    timestring, HF,
                    Structure.Name != null ? Structure.ToString() : "UNKNOWN", Civ, Site.AltName);
            
            return string.Format("{0} {1} became {2} of {3}.",
                timestring, "UNKNOWN",
                LinkType.HasValue ? HFSiteLink.LinkTypes[LinkType.Value] : "UNKNOWN", Site.AltName);
        }

        internal override string ToTimelineString()
        {
            //TODO: Incorporate new data
            var timelinestring = base.ToTimelineString();

            return string.Format("{0} Added Site Link to {1}.",
                timelinestring, Site.AltName);
        }

        internal override void Export(string table)
        {
            base.Export(table);

            table = GetType().Name;
            var vals = new List<object>
            {
                ID, 
                SiteID.DBExport(),
                HFID.DBExport(),
                CivID.DBExport(),
                StructureID.DBExport(),
                LinkType.DBExport(HFSiteLink.LinkTypes)
            };

            Database.ExportWorldItem(table, vals);
        }
    }
}
