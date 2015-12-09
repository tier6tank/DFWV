using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Linq;
using DFWV.WorldClasses.EntityClasses;
using DFWV.WorldClasses.HistoricalFigureClasses;

namespace DFWV.WorldClasses.HistoricalEventClasses
{
    class HE_RegionPopIncorporatedIntoEntity : HistoricalEvent
    {
        private int? PopRaceID { get; set; }
        private Race PopRace { get; set; }
        private int? SiteID { get; set; }
        private Site Site { get; set; }
        private int? JoinEntityID { get; set; }
        private Entity JoinEntity { get; set; }

        override public Point Location => Point.Empty;

        public int? PopNumberMoved { get; set; }
        public int? PopSRID { get; set; }
        public Region PopSR { get; set; }
        public int? PopFLID { get; set; }


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
            get { yield return PopSR; }
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
                        PopRaceID = valI;
                        break;
                    case "join_entity_id":
                        JoinEntityID = valI;
                        break;
                    case "site_id":
                        if (valI != -1)
                            SiteID = valI;
                        break;
                    case "pop_number_moved":
                        PopNumberMoved = valI;
                        break;
                    case "pop_srid":
                        PopSRID = valI;
                        break;
                    case "pop_flid":
                        PopFLID = valI;
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
            if (JoinEntityID.HasValue && World.Sites.ContainsKey(JoinEntityID.Value))
                JoinEntity = World.Entities[JoinEntityID.Value];
            if (PopSRID.HasValue && World.Sites.ContainsKey(PopSRID.Value))
                PopSR = World.Regions[PopSRID.Value];

        }

        protected override void WriteDataOnParent(MainForm frm, Control parent, ref Point location)
        {
            EventLabel(frm, parent, ref location, "PopRace:", PopRaceID.Value.ToString());
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


            var racetext = PopRace?.ToString().ToLower() ?? PopRaceID?.ToString() ?? "";


            return $"{count} of {racetext} from {PopSR} joined with the {JoinEntity} at {Site.AltName}.";
        }

        internal override string ToTimelineString()
        {
            var timelinestring = base.ToTimelineString();
            var racetext = PopRace?.ToString().ToLower() ?? PopRaceID?.ToString() ?? "";

            return $"{timelinestring} {PopNumberMoved.ToString()} {racetext} joined with {JoinEntity}.";

        }

        internal override void Export(string table)
        {
            base.Export(table);

            table = GetType().Name;

            var vals = new List<object>
            {
                ID,
                JoinEntityID.DBExport(),
                SiteID.DBExport(),
                PopRaceID.DBExport(),
                PopNumberMoved.DBExport(),
                PopSRID.DBExport(),
                PopFLID.DBExport()
            };

            Database.ExportWorldItem(table, vals);
        }

    }
}
