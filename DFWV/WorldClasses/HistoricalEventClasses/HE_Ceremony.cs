using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Linq;
using DFWV.WorldClasses.EntityClasses;

namespace DFWV.WorldClasses.HistoricalEventClasses
{
    class HE_Ceremony : HistoricalEvent
    {
        private int? SiteId { get; }
        private Site Site { get; set; }
        private int? SubregionId { get; }
        private Region Subregion { get; set; }
        private int? FeatureLayerId { get; }
        private int? EntityId { get; }
        public Entity Entity { get; private set; }

        override public Point Location => Site.Location;

        public int? OccasionId { get; set; }
        public int? ScheduleId { get; set; }

        public override IEnumerable<Entity> EntitiesInvolved
        {
            get { yield return Entity; }
        }
        public override IEnumerable<Site> SitesInvolved
        {
            get { yield return Site; }
        }

        public override IEnumerable<Region> RegionsInvolved
        {
            get { yield return Subregion; }
        }

        public HE_Ceremony(XDocument xdoc, World world)
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
                    case "occasion_id":
                        if (valI != -1)
                            OccasionId = valI;
                        break;
                    case "schedule_id":
                        if (valI != -1)
                            ScheduleId = valI;
                        break;
                    case "civ_id":
                        EntityId = valI;
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
            EventLabel(frm, parent, ref location, "Civ:", Entity);
            EventLabel(frm, parent, ref location, "Region:", Subregion);
        }

        protected override string LegendsDescription() 
        {
            var timestring = base.LegendsDescription();

            return
                $"{timestring} {Entity} held a ceremony in {Site.AltName} as part of {EventCollection.Name ?? "UNKNOWN"}. The event featured UNKNOWN.";
        }

        internal override string ToTimelineString()
        {
            var timelinestring = base.ToTimelineString();

            return $"{timelinestring} {Entity} held a ceremony in {Site.AltName}.";

        }

        internal override void Export(string table)
        {
            base.Export(table);

            table = GetType().Name;

            var vals = new List<object> 
            { 
                Id, 
                EntityId.DBExport(),
                SiteId.DBExport(),
                SubregionId.DBExport(),
                FeatureLayerId.DBExport()
            };

            Database.ExportWorldItem(table, vals);

        }

    }
}
