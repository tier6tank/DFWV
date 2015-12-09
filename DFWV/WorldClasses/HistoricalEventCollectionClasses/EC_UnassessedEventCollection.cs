﻿using System.Collections.Generic;
using System.Xml.Linq;

namespace DFWV.WorldClasses.HistoricalEventCollectionClasses
{
    public class EC_UnassessedEventCollection : HistoricalEventCollection
    {
        public EC_UnassessedEventCollection(XDocument xdoc, World world)
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
                    case "start_year":
                    case "start_seconds72":
                    case "end_year":
                    case "end_seconds72":
                    case "event":
                    case "type":
                        break;

                    default:
                        DFXMLParser.UnexpectedXmlElement(xdoc.Root.Name.LocalName + "\t" + Types[Type], element, xdoc.Root.ToString());
                        break;
                }
            }

        }

        internal override void Export(string table)
        {
            base.Export(table);


            table = GetType().Name;

            var vals = new List<object> { Id };


            Database.ExportWorldItem(table, vals);

        }

    }
}
