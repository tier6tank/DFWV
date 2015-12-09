using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Linq;
using DFWV.WorldClasses.HistoricalFigureClasses;

namespace DFWV.WorldClasses.HistoricalEventClasses
{
    class HeHfWounded : HistoricalEvent
    {
        private int? WoundeeHfid { get; }
        private HistoricalFigure WoundeeHf { get; set; }
        private int? WounderHfid { get; }
        private HistoricalFigure WounderHf { get; set; }
        private int? SiteId { get; }
        private Site Site { get; set; }
        private int? SubregionId { get; }
        private Region Subregion { get; set; }
        private int? FeatureLayerId { get; }
        public int? BodyPart { get; set; }
        public int? InjuryType { get; set; }
        public int? PartLost { get; set; }

        override public Point Location => Site?.Location ?? (Subregion?.Location ?? Point.Empty);

        public override IEnumerable<HistoricalFigure> HFsInvolved
        {
            get
            {
                yield return WoundeeHf;
                yield return WounderHf;
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

        public HeHfWounded(XDocument xdoc, World world)
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
                            SiteId = valI;
                        break;
                    case "subregion_id":
                        if (valI != -1)
                            SubregionId = valI;
                        break;
                    case "feature_layer_id":
                        if (valI != -1)
                            FeatureLayerId = valI;
                        break;
                    case "woundee_hfid":
                        WoundeeHfid = valI;
                        break;
                    case "wounder_hfid":
                        WounderHfid = valI;
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
            if (SiteId.HasValue && World.Sites.ContainsKey(SiteId.Value))
                Site = World.Sites[SiteId.Value];
            if (SubregionId.HasValue && World.Regions.ContainsKey(SubregionId.Value))
                Subregion = World.Regions[SubregionId.Value];
            if (WoundeeHfid.HasValue && World.HistoricalFigures.ContainsKey(WoundeeHfid.Value))
                WoundeeHf = World.HistoricalFigures[WoundeeHfid.Value];
            if (WounderHfid.HasValue && World.HistoricalFigures.ContainsKey(WounderHfid.Value))
                WounderHf = World.HistoricalFigures[WounderHfid.Value];
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
                        DfxmlParser.UnexpectedXmlElement(xdoc.Root.Name.LocalName + "\t" + Types[Type], element, xdoc.Root.ToString());
                        break;
                }
            }
        }

        protected override void WriteDataOnParent(MainForm frm, Control parent, ref Point location)
        {
            EventLabel(frm, parent, ref location, "HF:", WoundeeHf);
            EventLabel(frm, parent, ref location, "By:", WounderHf);
            EventLabel(frm, parent, ref location, "Site:", Site);
            EventLabel(frm, parent, ref location, "Region:", Subregion);
        }

        protected override string LegendsDescription()
        {
            //TODO: Incorporate new data
            var timestring = base.LegendsDescription();

            return
                $"{timestring} {(WoundeeHf != null ? "the " + WoundeeHf.Race + " " + WoundeeHf : "an unknown creature")} was wounded by {(WounderHf != null ? "the " + WounderHf.Race + " " + WounderHf : "an unknown creature")}.";

           
        }

        internal override string ToTimelineString()
        {
            //TODO: Incorporate new data
            var timelinestring = base.ToTimelineString();

            return
                $"{timelinestring} {WoundeeHf?.ToString() ?? WoundeeHfid.ToString()} was wounded by the {WounderHf?.ToString() ?? WounderHfid.ToString()}.";
        }        

        internal override void Export(string table)
        {
            base.Export(table);

            table = GetType().Name;

            var vals = new List<object>
            {
                Id, 
                WoundeeHfid.DBExport(), 
                WounderHfid.DBExport(), 
                SiteId.DBExport(), 
                SubregionId.DBExport(), 
                FeatureLayerId.DBExport(),
                BodyPart.DBExport(),
                InjuryType.DBExport(),
                PartLost.DBExport()
            };

            Database.ExportWorldItem(table, vals);
        }

    }
}
