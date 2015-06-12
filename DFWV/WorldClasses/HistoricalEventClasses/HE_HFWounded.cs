using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Linq;
using DFWV.WorldClasses.HistoricalFigureClasses;

namespace DFWV.WorldClasses.HistoricalEventClasses
{
    class HE_HFWounded : HistoricalEvent
    {
        private int? WoundeeHFID { get; set; }
        private HistoricalFigure WoundeeHF { get; set; }
        private int? WounderHFID { get; set; }
        private HistoricalFigure WounderHF { get; set; }
        private int? SiteID { get; set; }
        private Site Site { get; set; }
        private int? SubregionID { get; set; }
        private Region Subregion { get; set; }
        private int? FeatureLayerID { get; set; }
        public int? BodyPart { get; set; }
        public int? InjuryType { get; set; }
        public int? PartLost { get; set; }

        override public Point Location { get { return Site != null ? Site.Location : (Subregion != null ? Subregion.Location : Point.Empty); } }
        public override IEnumerable<HistoricalFigure> HFsInvolved
        {
            get
            {
                yield return WoundeeHF;
                yield return WounderHF;
            }
        }
        public override IEnumerable<Site> SitesInvolved
        {
            get { yield return Site; }
        }
        public override IEnumerable<Region> RegionsInvolved
        {
            get { yield return Subregion; }
        }

        public HE_HFWounded(XDocument xdoc, World world)
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
                        if (valI != -1)
                            SiteID = valI;
                        break;
                    case "subregion_id":
                        if (valI != -1)
                            SubregionID = valI;
                        break;
                    case "feature_layer_id":
                        if (valI != -1)
                            FeatureLayerID = valI;
                        break;
                    case "woundee_hfid":
                        WoundeeHFID = valI;
                        break;
                    case "wounder_hfid":
                        WounderHFID = valI;
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
                Site = World.Sites[SiteID.Value];
            if (SubregionID.HasValue && World.Regions.ContainsKey(SubregionID.Value))
                Subregion = World.Regions[SubregionID.Value];
            if (WoundeeHFID.HasValue && World.HistoricalFigures.ContainsKey(WoundeeHFID.Value))
                WoundeeHF = World.HistoricalFigures[WoundeeHFID.Value];
            if (WounderHFID.HasValue && World.HistoricalFigures.ContainsKey(WounderHFID.Value))
                WounderHF = World.HistoricalFigures[WounderHFID.Value];
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
                    case "victim":
                    case "attacker":
                    case "woundee":
                    case "wounder":
                    case "site":
                    case "victim_race":
                    case "victim_caste":
                    case "woundee_race":
                    case "woundee_caste":
                        break;
                    case "body_part":
                        BodyPart = valI;
                        break;
                    case "injury_type":
                        InjuryType = valI;
                        break;
                    case "part_lost":
                        PartLost = valI;
                        break;
                    default:
                        DFXMLParser.UnexpectedXMLElement(xdoc.Root.Name.LocalName + "\t" + Types[Type], element, xdoc.Root.ToString());
                        break;
                }
            }
        }

        protected override void WriteDataOnParent(MainForm frm, Control parent, ref Point location)
        {
            EventLabel(frm, parent, ref location, "HF:", WoundeeHF);
            EventLabel(frm, parent, ref location, "By:", WounderHF);
            EventLabel(frm, parent, ref location, "Site:", Site);
            EventLabel(frm, parent, ref location, "Region:", Subregion);
        }

        protected override string LegendsDescription()
        {
            //TODO: Incorporate new data
            var timestring = base.LegendsDescription();

            return string.Format("{0} {1} was wounded by {2}.",
                timestring, WoundeeHF != null ? "the " + WoundeeHF.Race + " " + WoundeeHF: "an unknown creature",
                            WounderHF != null ? "the " + WounderHF.Race + " " + WounderHF: "an unknown creature");

           
        }

        internal override string ToTimelineString()
        {
            //TODO: Incorporate new data
            var timelinestring = base.ToTimelineString();

            return string.Format("{0} {1} was wounded by the {2}.",
                timelinestring,  WoundeeHF != null ? WoundeeHF.ToString() : WoundeeHFID.ToString(), WounderHF != null ? WounderHF.ToString() : WounderHFID.ToString());
        }        

        internal override void Export(string table)
        {
            base.Export(table);

            table = GetType().Name;

            var vals = new List<object>
            {
                ID, 
                WoundeeHFID.DBExport(), 
                WounderHFID.DBExport(), 
                SiteID.DBExport(), 
                SubregionID.DBExport(), 
                FeatureLayerID.DBExport(),
                BodyPart.DBExport(),
                InjuryType.DBExport(),
                PartLost.DBExport()
            };

            Database.ExportWorldItem(table, vals);
        }

    }
}
