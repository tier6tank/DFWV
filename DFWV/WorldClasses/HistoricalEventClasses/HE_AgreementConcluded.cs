using System;
using System.Collections.Generic;
using System.Xml.Linq;
using DFWV.WorldClasses.EntityClasses;

namespace DFWV.WorldClasses.HistoricalEventClasses
{
    class HE_AgreementConcluded : HistoricalEvent
    {
        private int? SiteID { get; set; }
        private Site Site { get; set; }

        public int? Topic { get; set; }
        public int? Result { get; set; }
        public int? SourceEntID { get; set; }
        public int? DestinationEntID { get; set; }
        public Entity Source { get; set; }
        public Entity Destination { get; set; }

        public override IEnumerable<Entity> EntitiesInvolved
        {
            get
            {
                yield return Source;
                yield return Destination;
            }
        }

        public override IEnumerable<Site> SitesInvolved
        {
            get { yield return Site; }
        }
        public HE_AgreementConcluded(XDocument xdoc, World world)
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


                    default:
                        DFXMLParser.UnexpectedXMLElement(xdoc.Root.Name.LocalName + "\t" + Types[Type], element, xdoc.Root.ToString());
                        break;
                }
            }
        }

        internal override void Link()
        {
            //TODO: Incorporate new data
            
            base.Link();
            if (SiteID.HasValue && World.Sites.ContainsKey(SiteID.Value))
                Site = World.Sites[SiteID.Value];
            if (DestinationEntID.HasValue && World.Entities.ContainsKey(DestinationEntID.Value))
                Destination = World.Entities[DestinationEntID.Value];
            if (SourceEntID.HasValue && World.Entities.ContainsKey(SourceEntID.Value))
                Source = World.Entities[SourceEntID.Value];
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
                    case "topic":
                        if (!MeetingTopics.Contains(val))
                            MeetingTopics.Add(val);
                        Topic = MeetingTopics.IndexOf(val);
                        break;
                    case "source":
                        SourceEntID = valI;
                        break;
                    case "destination":
                        DestinationEntID = valI;
                        break;
                    case "site":
                        SiteID = valI;
                        break;
                    case "result":
                        if (!MeetingResults.Contains(val))
                            MeetingResults.Add(val);
                        Result = MeetingResults.IndexOf(val);
                        break;
                    default:
                        DFXMLParser.UnexpectedXMLElement(xdoc.Root.Name.LocalName + "\t" + Types[Type], element, xdoc.Root.ToString());
                        break;
                }
            }
        }

        protected override string LegendsDescription()
        {
            //TODO: Incorporate new data
            var timestring = base.LegendsDescription();

            
            return $"{timestring} agreement concluded.";
        }

        internal override string ToTimelineString()
        {
            //TODO: Incorporate new data
            var timelinestring = base.ToTimelineString();

            return $"{timelinestring} agreement concluded.";
        }

        internal override void Export(string table)
        {
            base.Export(table);

            table = GetType().Name;

            var vals = new List<object>
            {
                ID,
                Topic.DBExport(MeetingTopics),
                SourceEntID.DBExport(),
                DestinationEntID.DBExport(),
                SiteID.DBExport(),
                Result.DBExport(MeetingResults)
            };

            Database.ExportWorldItem(table, vals);

        }

    }
}