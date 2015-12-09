using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Linq;
using DFWV.WorldClasses.EntityClasses;
using DFWV.WorldClasses.HistoricalFigureClasses;

namespace DFWV.WorldClasses.HistoricalEventClasses
{
    public class HE_RemoveHFSiteLink : HistoricalEvent
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

        override public Point Location => Site != null ? Site.Location : Point.Empty;

        public override IEnumerable<HistoricalFigure> HFsInvolved
        {
            get { yield return HF; }
        }
        public override IEnumerable<Entity> EntitiesInvolved
        {
            get { yield return Civ; }
        }
        public override IEnumerable<Site> SitesInvolved
        {
            get { yield return Site; }
        }


        public HE_RemoveHFSiteLink(XDocument xdoc, World world)
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
                        Site.AddStructure(new Structure(Site, StructureID.Value, World));
                    Structure = Site.GetStructure(StructureID.Value);
                }
            }
            if (CivID.HasValue && World.Entities.ContainsKey(CivID.Value))
                Civ = World.Entities[CivID.Value];
            if (HFID.HasValue && World.HistoricalFigures.ContainsKey(HFID.Value))
                HF = World.HistoricalFigures[HFID.Value];
        }

        internal override void Process()
        {
            base.Process();

            var matched = false;
            if (HF != null && HF.SiteLinks != null)
            {
                foreach (var siteLinkList in HF.SiteLinks)
                {
                    foreach (var siteLink in siteLinkList.Value)
                    {
                        if (siteLink.Site == Site)
                        {
                            siteLink.RemoveEvent = this;
                            HFSiteLink = siteLink;
                            matched = true;
                            break;
                        }
                    }
                    if (matched)
                        break;
                }
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
                        SiteID = valI;
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
                        return
                            $"{timestring} {HF} stopped ruling from {(Structure.Name != null ? Structure.ToString() : "UNKNOWN")} of {Civ} in {Site.AltName}.";
                    case "home structure":
                        return
                            $"{timestring} {HF} moved out of {(Structure.Name != null ? Structure.ToString() : "UNKNOWN")} of {Civ} in {Site.AltName}.";
                    default:
                        return
                            $"{timestring} {"UNKNOWN"} became {HFSiteLink.LinkTypes[LinkType.Value]} of {Site.AltName}.";
                }
            }



            if (Structure != null && Civ != null && HF != null)
                return
                    $"{timestring} {HF} ruled from {(Structure.Name != null ? Structure.ToString() : "UNKNOWN")} of {Civ} in {Site.AltName}.";
            return
                $"{timestring} {"UNKNOWN"} became {(LinkType.HasValue ? HFSiteLink.LinkTypes[LinkType.Value] : "UNKNOWN")} of {Site.AltName}.";
        }

        internal override string ToTimelineString()
        {
            //TODO: Incorporate new data
            var timelinestring = base.ToTimelineString();

            return $"{timelinestring} Remove HF link from {Site.AltName}";
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