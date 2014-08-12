using System;
using System.Collections.Generic;
using System.Xml.Linq;
using DFWV.WorldClasses.EntityClasses;

namespace DFWV.WorldClasses.HistoricalEventClasses
{
    class HE_Merchant : HistoricalEvent
    {
        public int? SourceID { get; set; }
        public Entity Source { get; set; }
        public int? DestinationID { get; set; }
        public Entity Destination { get; set; }
        public int? SiteID { get; set; }
        public Site Site { get; set; }


        public HE_Merchant(XDocument xdoc, World world)
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
                    default:
                        DFXMLParser.UnexpectedXMLElement(xdoc.Root.Name.LocalName + "\t" + Types[Type], element, xdoc.Root.ToString());
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
                Int32.TryParse(val, out valI);

                switch (element.Name.LocalName)
                {
                    case "id":
                    case "type":
                        break;
                    case "source":
                        SourceID = valI;
                        break;
                    case "destination":
                        DestinationID = valI;
                        break;
                    case "site":
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
            if (DestinationID.HasValue && World.Entities.ContainsKey(DestinationID.Value))
                Destination = World.Entities[DestinationID.Value];
            if (SourceID.HasValue && World.Entities.ContainsKey(SourceID.Value))
                Source = World.Entities[SourceID.Value];
        }

        internal override void Process()
        {
            //TODO: Incorporate new data
            base.Process();
            if (Destination != null)
            {
                if (Destination.Events == null)
                    Destination.Events = new List<HistoricalEvent>();
                Destination.Events.Add(this);
            }
            if (Source != null)
            {
                if (Source.Events == null)
                    Source.Events = new List<HistoricalEvent>();
                Source.Events.Add(this);
            }
        }

        protected override string LegendsDescription() //Matched
        {
            var timestring = base.LegendsDescription();

            if (Destination != null && Source != null && Site != null)
                return string.Format("{0} merchants from {1} visited {2} at {3}.",
                    timestring, Source, Destination,
                    Site.AltName);

            return timestring;
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
                ID,
                SourceID.DBExport(),
                DestinationID.DBExport(),
                SiteID.DBExport()
            };

            Database.ExportWorldItem(table, vals);

        }

    }
}

