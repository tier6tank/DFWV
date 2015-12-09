using System.Collections.Generic;
using System.Xml.Linq;
using DFWV.WorldClasses.EntityClasses;

namespace DFWV.WorldClasses.HistoricalEventClasses
{
    class HE_Merchant : HistoricalEvent
    {
        public int? SourceId { get; set; }
        public Entity Source { get; set; }
        public int? DestinationId { get; set; }
        public Entity Destination { get; set; }
        public int? SiteId { get; set; }
        public Site Site { get; set; }

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


        public HE_Merchant(XDocument xdoc, World world)
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
                    case "source":
                        SourceId = valI;
                        break;
                    case "destination":
                        DestinationId = valI;
                        break;
                    case "site":
                        SiteId = valI;
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
            if (DestinationId.HasValue && World.Entities.ContainsKey(DestinationId.Value))
                Destination = World.Entities[DestinationId.Value];
            if (SourceId.HasValue && World.Entities.ContainsKey(SourceId.Value))
                Source = World.Entities[SourceId.Value];
        }

        protected override string LegendsDescription() //Matched
        {
            var timestring = base.LegendsDescription();

            if (Destination != null && Source != null && Site != null)
                return $"{timestring} merchants from {Source} visited {Destination} at {Site.AltName}.";

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
                Id,
                SourceId.DBExport(),
                DestinationId.DBExport(),
                SiteId.DBExport()
            };

            Database.ExportWorldItem(table, vals);

        }

    }
}

