using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Linq;
using DFWV.WorldClasses.EntityClasses;
using DFWV.WorldClasses.HistoricalEventCollectionClasses;

namespace DFWV.WorldClasses.HistoricalEventClasses
{
    class HE_PeaceAccepted : HistoricalEvent
    {
        private int? SiteID { get; set; }
        private Site Site { get; set; }

        public int? Topic { get; set; }
        public int? SourceEntID { get; set; }
        public int? DestinationEntID { get; set; }
        public Entity Source { get; set; }
        public Entity Destination { get; set; }

        override public Point Location => Site != null ? Site.Location : Point.Empty;

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


        public HE_PeaceAccepted(XDocument xdoc, World world)
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
                        break;
                    default:
                        DFXMLParser.UnexpectedXMLElement(xdoc.Root.Name.LocalName + "\t" + Types[Type], element, xdoc.Root.ToString());
                        break;
                }
            }
        }

        protected override void WriteDataOnParent(MainForm frm, Control parent, ref Point location)
        {
            //TODO: Incorporate new data
            EventLabel(frm, parent, ref location, "Site:", Site);

        }

        protected override string LegendsDescription()
        {
            //TODO: Incorporate new data
            var timestring = base.LegendsDescription();
            if (EventCollection == null) return timestring;
            var war = (EC_War)EventCollection;
            return $"{timestring} {war.AggressorEnt} accepted an offer of peace from {war.DefenderEnt}.";
        }

        internal override string ToTimelineString()
        {
            //TODO: Incorporate new data
            var timelinestring = base.ToTimelineString();

            if (EventCollection == null) return timelinestring;
            var war = (EC_War)EventCollection;
            return $"{timelinestring} {war.AggressorEnt} accepted peace from {war.DefenderEnt}.";
        }

        internal override void Export(string table)
        {
            base.Export(table);

            table = GetType().Name;

            var vals = new List<object>
            {
                ID, 
                SiteID.DBExport(),
                Topic.DBExport(MeetingTopics),
                SourceEntID.DBExport(),
                DestinationEntID.DBExport()
            };

            Database.ExportWorldItem(table, vals);
        }
    }
}
