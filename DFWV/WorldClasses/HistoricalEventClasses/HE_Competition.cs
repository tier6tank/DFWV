using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;
using DFWV.WorldClasses.EntityClasses;
using DFWV.WorldClasses.HistoricalFigureClasses;

namespace DFWV.WorldClasses.HistoricalEventClasses
{
    class HeCompetition : HistoricalEvent
    {
        private int? SiteId { get; }
        private Site Site { get; set; }
        private int? SubregionId { get; }
        private Region Subregion { get; set; }
        private int? FeatureLayerId { get; }
        private int? CivId { get; }
        public Entity Civ { get; private set; }
        private int? WinnerHfid { get; }
        private HistoricalFigure WinnerHf { get; set; }
        public List<int> CompetitorHfiDs;
        public List<HistoricalFigure> CompetitorHFs;

        override public Point Location => Site.Location;

        public int? OccasionId { get; set; }
        public int? ScheduleId { get; set; }

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
        public HeCompetition(XDocument xdoc, World world)
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
                        CivId = valI;
                        break;

                    case "competitor_hfid":
                        if (CompetitorHfiDs == null)
                            CompetitorHfiDs = new List<int>();
                        CompetitorHfiDs.Add(valI);
                        break;
                    case "winner_hfid":
                        WinnerHfid = valI;
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
            if (CivId.HasValue && World.Entities.ContainsKey(CivId.Value))
                Civ = World.Entities[CivId.Value];
            if (CompetitorHfiDs != null)
            {
                CompetitorHFs = new List<HistoricalFigure>();
                foreach (var competitorhfid in CompetitorHfiDs.Where(group1Hfid => World.HistoricalFigures.ContainsKey(group1Hfid)))
                {
                    CompetitorHFs.Add(World.HistoricalFigures[competitorhfid]);

                }
            }
            if (WinnerHfid.HasValue && World.HistoricalFigures.ContainsKey(WinnerHfid.Value))
                WinnerHf = World.HistoricalFigures[WinnerHfid.Value];

        }

        protected override void WriteDataOnParent(MainForm frm, Control parent, ref Point location)
        {
            EventLabel(frm, parent, ref location, "Site:", Site);
            EventLabel(frm, parent, ref location, "Civ:", Civ);
            EventLabel(frm, parent, ref location, "Region:", Subregion);
            EventLabel(frm, parent, ref location, "Winner:", WinnerHf);
            if (CompetitorHFs != null)
            {
                foreach (var hf in CompetitorHFs)
                    EventLabel(frm, parent, ref location, "Competitor:", hf);
            }
        }
    

        protected override string LegendsDescription()
        {
            var timestring = base.LegendsDescription();

            var competitorsString = CompetitorHFs.Aggregate("", (current, hf) => current + $"the {hf.Race.ToString().ToLower()} {hf}, ");
            competitorsString = competitorsString.Trim().TrimEnd(',');

            return
                $"{timestring} {Civ} held a UNKNOWN competition in {Site.AltName} as part of {EventCollection.Name ?? "UNKNOWN"}. \nCompeting were {competitorsString}.  \nThe {WinnerHf.Race.ToString().ToLower()} {WinnerHf} was the victor.";


        }

        internal override string ToTimelineString()
        {
            var timelinestring = base.ToTimelineString();

            return $"{timelinestring} {Civ} held a competition in {Site.AltName}.";

        }

        internal override void Export(string table)
        {
            base.Export(table);

            table = GetType().Name;

            var vals = new List<object>
            {
                Id,
                CivId.DBExport(),
                WinnerHfid.DBExport(),
                CompetitorHfiDs.DBExport(),
                SiteId.DBExport(),
                SubregionId.DBExport(),
                FeatureLayerId.DBExport()
            };

            Database.ExportWorldItem(table, vals);

        }

    }
}
