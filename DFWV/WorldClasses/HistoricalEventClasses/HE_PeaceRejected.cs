using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Linq;
using DFWV.WorldClasses.EntityClasses;
using DFWV.WorldClasses.HistoricalEventCollectionClasses;

namespace DFWV.WorldClasses.HistoricalEventClasses
{
    class HE_PeaceRejected : HistoricalEvent
    {
        private int? SiteId { get; }
        private Site Site { get; set; }

        public int? Topic { get; set; }
        public int? EntityId_Source { get; set; }
        public int? EntityId_Destination { get; set; }
        public Entity Entity_Source { get; set; }
        public Entity Entity_Destination { get; set; }

        override public Point Location => Site?.Location ?? Point.Empty;

        public override IEnumerable<Entity> EntitiesInvolved
        {
            get
            {
                yield return Entity_Source;
                yield return Entity_Destination;
            }
        }
        public override IEnumerable<Site> SitesInvolved
        {
            get { yield return Site; }
        }


        public HE_PeaceRejected(XDocument xdoc, World world)
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
                    default:
                        DFXMLParser.UnexpectedXmlElement(xdoc.Root.Name.LocalName + "\t" + Types[Type], element, xdoc.Root.ToString());
                        break;
                }
            }
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
                        EntityId_Source = valI;
                        break;
                    case "destination":
                        EntityId_Destination = valI;
                        break;
                    case "site":
                        break;
                    default:
                        DFXMLParser.UnexpectedXmlElement(xdoc.Root.Name.LocalName + "\t" + Types[Type], element, xdoc.Root.ToString());
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
            return $"{timestring} {war.AggressorEnt} rejected an offer of peace from {war.DefenderEnt}.";
        }

        internal override string ToTimelineString()
        {
            //TODO: Incorporate new data
            var timelinestring = base.ToTimelineString();

            if (EventCollection == null) return timelinestring;
            var war = (EC_War)EventCollection;
            return $"{timelinestring} {war.AggressorEnt} rejected peace from {war.DefenderEnt}.";
        }

        internal override void Export(string table)
        {
            base.Export(table);

            table = GetType().Name;

            var vals = new List<object>
            {
                Id, 
                SiteId.DBExport(),
                Topic.DBExport(MeetingTopics),
                EntityId_Source.DBExport(),
                EntityId_Destination.DBExport()
            };

            Database.ExportWorldItem(table, vals);
        }

    }
}