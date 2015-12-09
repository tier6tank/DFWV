using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Linq;
using DFWV.WorldClasses.EntityClasses;

namespace DFWV.WorldClasses.HistoricalEventClasses
{
    class HE_Performance : HistoricalEvent
    {
        private int? SiteID { get; set; }
        private Site Site { get; set; }
        private int? SubregionID { get; set; }
        private Region Subregion { get; set; }
        private int? FeatureLayerID { get; set; }
        private int? CivID { get; set; }
        public Entity Civ { get; private set; }

        override public Point Location => Site.Location;

        public int? OccasionID { get; set; }
        public int? ScheduleID { get; set; }

        public override IEnumerable<Entity> EntitiesInvolved
        {
            get { yield return Civ; }
        }
        public override IEnumerable<Site> SitesInvolved
        {
            get { yield return Site; }
        }

        public override IEnumerable<Region> RegionsInvolved
        {
            get { yield return Subregion; }
        }

        public HE_Performance(XDocument xdoc, World world)
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
                    case "occasion_id":
                        if (valI != -1)
                            OccasionID = valI;
                        break;
                    case "schedule_id":
                        if (valI != -1)
                            ScheduleID = valI;
                        break;
                    case "civ_id":
                        CivID = valI;
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
            if (CivID.HasValue && World.Entities.ContainsKey(CivID.Value))
                Civ = World.Entities[CivID.Value];

        }

        protected override void WriteDataOnParent(MainForm frm, Control parent, ref Point location)
        {
            EventLabel(frm, parent, ref location, "Site:", Site);
            EventLabel(frm, parent, ref location, "Civ:", Civ);
            EventLabel(frm, parent, ref location, "Region:", Subregion);
        }

        protected override string LegendsDescription()
        {
            var timestring = base.LegendsDescription();

            return
                $"{timestring} {Civ} held a performance of UNKNOWN in {Site.AltName} as part of {EventCollection.Name ?? "UNKNOWN"}.";


        }

        internal override string ToTimelineString()
        {
            var timelinestring = base.ToTimelineString();

            return $"{timelinestring} {Civ} held a performance in {Site.AltName}.";

        }

        internal override void Export(string table)
        {
            base.Export(table);

            table = GetType().Name;

            var vals = new List<object>
            {
                ID,
                CivID.DBExport(),
                SiteID.DBExport(),
                SubregionID.DBExport(),
                FeatureLayerID.DBExport()
            };

            Database.ExportWorldItem(table, vals);

        }

    }
}
