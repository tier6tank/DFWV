﻿using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;
using DFWV.WorldClasses.EntityClasses;
using DFWV.WorldClasses.HistoricalFigureClasses;

namespace DFWV.WorldClasses.HistoricalEventClasses
{
    class HE_Competition : HistoricalEvent
    {
        private int? SiteId { get; }
        private Site Site { get; set; }
        private int? SubregionId { get; }
        private Region Subregion { get; set; }
        private int? FeatureLayerId { get; }
        private int? EntityId { get; }
        public Entity Entity { get; private set; }
        private int? HfId_Winner { get; }
        private HistoricalFigure Hf_Winner { get; set; }
        public List<int> HfIDs_Competitor;
        public List<HistoricalFigure> Hfs_Competitor;

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
        public HE_Competition(XDocument xdoc, World world)
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

                    case "competitor_hfid":
                        if (HfIDs_Competitor == null)
                            HfIDs_Competitor = new List<int>();
                        HfIDs_Competitor.Add(valI);
                        break;
                    case "winner_hfid":
                        HfId_Winner = valI;
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
            if (HfIDs_Competitor != null)
            {
                Hfs_Competitor = new List<HistoricalFigure>();
                foreach (var competitorhfid in HfIDs_Competitor.Where(group1Hfid => World.HistoricalFigures.ContainsKey(group1Hfid)))
                {
                    Hfs_Competitor.Add(World.HistoricalFigures[competitorhfid]);

                }
            }
        }

        protected override void WriteDataOnParent(MainForm frm, Control parent, ref Point location)
        {
            EventLabel(frm, parent, ref location, "Site:", Site);
            EventLabel(frm, parent, ref location, "Civ:", Entity);
            EventLabel(frm, parent, ref location, "Region:", Subregion);
            EventLabel(frm, parent, ref location, "Winner:", Hf_Winner);
            if (Hfs_Competitor != null)
            {
                foreach (var hf in Hfs_Competitor)
                    EventLabel(frm, parent, ref location, "Competitor:", hf);
            }
        }
    

        protected override string LegendsDescription()
        {
            var timestring = base.LegendsDescription();

            var competitorsString = Hfs_Competitor.Aggregate("", (current, hf) => current + $"the {hf.Race.ToString().ToLower()} {hf}, ");
            competitorsString = competitorsString.Trim().TrimEnd(',');

            return
                $"{timestring} {Entity} held a UNKNOWN competition in {Site.AltName} as part of {EventCollection.Name ?? "UNKNOWN"}. \nCompeting were {competitorsString}.  \nThe {Hf_Winner.Race.ToString().ToLower()} {Hf_Winner} was the victor.";


        }

        internal override string ToTimelineString()
        {
            var timelinestring = base.ToTimelineString();

            return $"{timelinestring} {Entity} held a competition in {Site.AltName}.";

        }

        internal override void Export(string table)
        {
            base.Export(table);

            table = GetType().Name;

            var vals = new List<object>
            {
                Id,
                EntityId.DBExport(),
                HfId_Winner.DBExport(),
                HfIDs_Competitor.DBExport(),
                SiteId.DBExport(),
                SubregionId.DBExport(),
                FeatureLayerId.DBExport()
            };

            Database.ExportWorldItem(table, vals);

        }

    }
}
