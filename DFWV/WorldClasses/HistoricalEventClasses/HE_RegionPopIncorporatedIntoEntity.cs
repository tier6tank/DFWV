using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Linq;
using DFWV.WorldClasses.EntityClasses;

namespace DFWV.WorldClasses.HistoricalEventClasses
{
    class HE_RegionPopIncorporatedIntoEntity : HistoricalEvent
    {
        private int? PopRaceId { get; }
        private Race PopRace { get; set; }
        private int? SiteId { get; }
        private Site Site { get; set; }
        private int? JoinEntityId { get; }
        private Entity JoinEntity { get; set; }

        override public Point Location => Point.Empty;

        public int? PopNumberMoved { get; set; }
        public int? PopSrid { get; set; }
        public Region PopSr { get; set; }
        public int? PopFlid { get; set; }


        public override IEnumerable<Entity> EntitiesInvolved
        {
            get { yield return JoinEntity; }
        }
        public override IEnumerable<Site> SitesInvolved
        {
            get { yield return Site; }
        }

        public override IEnumerable<Region> RegionsInvolved
        {
            get { yield return PopSr; }
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
                        PopRaceId = valI;
                        break;
                    case "join_entity_id":
                        JoinEntityId = valI;
                        break;
                    case "site_id":
                        if (valI != -1)
                            SiteId = valI;
                        break;
                    case "pop_number_moved":
                        PopNumberMoved = valI;
                        break;
                    case "pop_srid":
                        PopSrid = valI;
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

        internal override void Link()
        {
            base.Link();
            if (SiteId.HasValue && World.Sites.ContainsKey(SiteId.Value))
                Site = World.Sites[SiteId.Value];
            if (JoinEntityId.HasValue && World.Sites.ContainsKey(JoinEntityId.Value))
                JoinEntity = World.Entities[JoinEntityId.Value];
            if (PopSrid.HasValue && World.Sites.ContainsKey(PopSrid.Value))
                PopSr = World.Regions[PopSrid.Value];

        }

        protected override void WriteDataOnParent(MainForm frm, Control parent, ref Point location)
        {
            EventLabel(frm, parent, ref location, "PopRace:", PopRaceId.Value.ToString());
            EventLabel(frm, parent, ref location, "Number:", PopNumberMoved.Value.ToString());
            EventLabel(frm, parent, ref location, "Join Entity:", JoinEntity);
            EventLabel(frm, parent, ref location, "Site:", Site);
        }

        protected override string LegendsDescription() 
        {
            var timestring = base.LegendsDescription();

            var count = PopNumberMoved.ToString();
            if (PopNumberMoved >= 24)
                count = "dozens";


            var racetext = PopRace?.ToString().ToLower() ?? PopRaceId?.ToString() ?? "";


            return $"{timestring} {count} of {racetext} from {PopSr} joined with the {JoinEntity} at {Site.AltName}.";
        }

        internal override string ToTimelineString()
        {
            var timelinestring = base.ToTimelineString();
            var racetext = PopRace?.ToString().ToLower() ?? PopRaceId?.ToString() ?? "";

            return $"{timelinestring} {PopNumberMoved.ToString()} {racetext} joined with {JoinEntity}.";

        }

        internal override void Export(string table)
        {
            base.Export(table);

            table = GetType().Name;

            var vals = new List<object>
            {
                Id,
                JoinEntityId.DBExport(),
                SiteId.DBExport(),
                PopRaceId.DBExport(),
                PopNumberMoved.DBExport(),
                PopSrid.DBExport(),
                PopFlid.DBExport()
            };

            Database.ExportWorldItem(table, vals);
        }

    }
}
