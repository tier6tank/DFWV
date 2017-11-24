using System.Collections.Generic;
using System.Xml.Linq;
using DFWV.WorldClasses.EntityClasses;

namespace DFWV.WorldClasses.HistoricalEventClasses
{
    class HE_Merchant : HistoricalEvent
    {
        public int? EntityId_Source { get; set; }
        public Entity Entity_Source { get; set; }
        public int? EntityId_Destination { get; set; }
        public Entity Entity_Destination { get; set; }
        public int? SiteId { get; set; }
        public Site Site { get; set; }

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
                        EntityId_Source = valI;
                        break;
                    case "destination":
                        EntityId_Destination = valI;
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

        protected override string LegendsDescription() //Matched
        {
            var timestring = base.LegendsDescription();

            if (Entity_Destination != null && Entity_Source != null && Site != null)
                return $"{timestring} merchants from {Entity_Source} visited {Entity_Destination} at {Site.AltName}.";

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
                EntityId_Source.DBExport(),
                EntityId_Destination.DBExport(),
                SiteId.DBExport()
            };

            Database.ExportWorldItem(table, vals);

        }

    }
}

