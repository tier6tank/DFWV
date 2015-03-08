using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Linq;
using DFWV.WorldClasses.EntityClasses;

namespace DFWV.WorldClasses.HistoricalEventClasses
{
    class HE_AgreementMade : HistoricalEvent
    {
        private int? SiteID { get; set; }
        private Site Site { get; set; }

        public int? Topic { get; set; }
        public int? SourceEntID { get; set; }
        public int? DestinationEntID { get; set; }
        public Entity Source { get; set; }
        public Entity Destination { get; set; }

        override public Point Location { get { return Site != null ? Site.Location : Point.Empty; } }
        public override IEnumerable<Entity> EntitiesInvolved
        {
            get
            {
                yield return Source;
                yield return Destination;
            }
        }
        public HE_AgreementMade(XDocument xdoc, World world)
            : base(xdoc, world)
        {
            foreach (var element in xdoc.Root.Elements())
            {
                var val = element.Value;
                int valI;
                Int32.TryParse(val, out valI);

                switch (element.Name.LocalName)
                {
                    case "id":
                    case "year":
                    case "seconds72":
                    case "type":
                        break;
                    case "site_id":
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
                Int32.TryParse(val, out valI);

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

        protected override string LegendsDescription() //Not Matched (changed script to update meeting topics
        {
            //TODO: Incorporate new data
            var timestring = base.LegendsDescription();


            return string.Format("{0} agreement concluded.",
                        timestring);
        }

        internal override string ToTimelineString()
        {
            //TODO: Incorporate new data
            return base.ToTimelineString();
        }

        internal override void Export(string table)
        {
            table = GetType().Name;

            var vals = new List<object>
            {
                ID,
                Topic.DBExport(MeetingTopics),
                SourceEntID.DBExport(),
                DestinationEntID.DBExport(),
                SiteID.DBExport(),
            };


            Database.ExportWorldItem(table, vals);

        }

    }
}

