﻿using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Linq;
using DFWV.WorldClasses.EntityClasses;

namespace DFWV.WorldClasses.HistoricalEventClasses
{
    class HeAgreementMade : HistoricalEvent
    {
        private int? SiteId { get; }
        private Site Site { get; set; }

        public int? Topic { get; set; }
        public int? SourceEntId { get; set; }
        public int? DestinationEntId { get; set; }
        public Entity Source { get; set; }
        public Entity Destination { get; set; }

        override public Point Location => Site?.Location ?? Point.Empty;

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

        public HeAgreementMade(XDocument xdoc, World world)
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
                        SiteId = valI;
                        break;
                    default:
                        DfxmlParser.UnexpectedXmlElement(xdoc.Root.Name.LocalName + "\t" + Types[Type], element, xdoc.Root.ToString());
                        break;
                }
            }
        }

        internal override void Link()
        {
            //TODO: Incorporate new data
            base.Link();
            if (SiteId.HasValue && World.Sites.ContainsKey(SiteId.Value))
                Site = World.Sites[SiteId.Value];
            if (DestinationEntId.HasValue && World.Entities.ContainsKey(DestinationEntId.Value))
                Destination = World.Entities[DestinationEntId.Value];
            if (SourceEntId.HasValue && World.Entities.ContainsKey(SourceEntId.Value))
                Source = World.Entities[SourceEntId.Value];
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
                        SourceEntId = valI;
                        break;
                    case "destination":
                        DestinationEntId = valI;
                        break;
                    case "site":
                        break;
                    default:
                        DfxmlParser.UnexpectedXmlElement(xdoc.Root.Name.LocalName + "\t" + Types[Type], element, xdoc.Root.ToString());
                        break;
                }
            }
        }

        protected override void WriteDataOnParent(MainForm frm, Control parent, ref Point location)
        {
            //TODO: Incorporate new data
            EventLabel(frm, parent, ref location, "Site:", Site);

        }

        protected override string LegendsDescription() //Not Matched (changed script to update meeting topics
        {
            //TODO: Incorporate new data
            var timestring = base.LegendsDescription();


            return $"{timestring} agreement concluded.";
        }

        internal override string ToTimelineString()
        {
            //TODO: Incorporate new data
            return base.ToTimelineString();
        }

        internal override void Export(string table)
        {
            base.Export(table);

            table = GetType().Name;

            var vals = new List<object>
            {
                Id,
                Topic.DBExport(MeetingTopics),
                SourceEntId.DBExport(),
                DestinationEntId.DBExport(),
                SiteId.DBExport()
            };


            Database.ExportWorldItem(table, vals);

        }

    }
}

