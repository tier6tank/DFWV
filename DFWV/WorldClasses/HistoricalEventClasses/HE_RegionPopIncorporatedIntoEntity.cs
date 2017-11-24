using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Linq;
using DFWV.WorldClasses.EntityClasses;

namespace DFWV.WorldClasses.HistoricalEventClasses
{
    class HE_RegionPopIncorporatedIntoEntity : HistoricalEvent
    {
        private int? RaceId { get; }
        private Race Race { get; set; }
        private int? SiteId { get; }
        private Site Site { get; set; }
        private int? EntityId { get; }
        private Entity Entity { get; set; }

        override public Point Location => Point.Empty;

        public int? PopNumberMoved { get; set; }
        public int? RegionId { get; set; }
        public Region Region { get; set; }
        public int? PopFlid { get; set; }


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
            get { yield return Region; }
        }

        public HE_RegionPopIncorporatedIntoEntity(XDocument xdoc, World world)
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
                    case "pop_race":
                        RaceId = valI;
                        break;
                    case "join_entity_id":
                        EntityId = valI;
                        break;
                    case "site_id":
                        if (valI != -1)
                            SiteId = valI;
                        break;
                    case "pop_number_moved":
                        PopNumberMoved = valI;
                        break;
                    case "pop_srid":
                        RegionId = valI;
                        break;
                    case "pop_flid":
                        PopFlid = valI;
                        break;
                    default:
                        DFXMLParser.UnexpectedXmlElement(xdoc.Root.Name.LocalName + "\t" + Types[Type], element, xdoc.Root.ToString());
                        break;
                }
            }
        }

        protected override void WriteDataOnParent(MainForm frm, Control parent, ref Point location)
        {
            EventLabel(frm, parent, ref location, "PopRace:", RaceId);
            EventLabel(frm, parent, ref location, "Number:", PopNumberMoved);
            EventLabel(frm, parent, ref location, "Join Entity:", Entity);
            EventLabel(frm, parent, ref location, "Site:", Site);
        }

        protected override string LegendsDescription() 
        {
            var timestring = base.LegendsDescription();

            var count = PopNumberMoved.ToString();
            if (PopNumberMoved >= 24)
                count = "dozens";


            var racetext = Race?.ToString().ToLower() ?? RaceId?.ToString() ?? "";


            return $"{timestring} {count} of {racetext} from {Region} joined with the {Entity} at {Site.AltName}.";
        }

        internal override string ToTimelineString()
        {
            var timelinestring = base.ToTimelineString();
            var racetext = Race?.ToString().ToLower() ?? RaceId?.ToString() ?? "";

            return $"{timelinestring} {PopNumberMoved} {racetext} joined with {Entity}.";

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
                RaceId.DBExport(),
                PopNumberMoved.DBExport(),
                RegionId.DBExport(),
                PopFlid.DBExport()
            };

            Database.ExportWorldItem(table, vals);
        }

    }
}
