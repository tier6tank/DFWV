﻿using System.Collections.Generic;
using System.Xml.Linq;

namespace DFWV.WorldClasses.HistoricalEventClasses
{
    public class HE_UnassessedEvent : HistoricalEvent
    {
        public HE_UnassessedEvent(XDocument xdoc, World world)
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

        internal override void Export(string table)
        {
            base.Export(table);

            table = GetType().Name;

            var vals = new List<object>
            {
                Id, 
                Type.DBExport(Types)
            };

            Database.ExportWorldItem(table , vals);
        }
    }
}


