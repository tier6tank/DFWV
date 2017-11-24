﻿using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Linq;
using DFWV.WorldClasses.EntityClasses;

namespace DFWV.WorldClasses.HistoricalEventClasses
{
    class HE_FirstContactFailed : HistoricalEvent
    {
        private int? SiteId { get; }
        private Site Site { get; set; }
        private int? EntityId_Contactor { get; }
        private Entity Entity_Contactor { get; set; }
        private int? EntityId_Rejector { get; }
        private Entity Entity_Rejector { get; set; }

        override public Point Location => Site.Location;

        public override IEnumerable<Entity> EntitiesInvolved
        {
            get
            {
                yield return Entity_Contactor;
                yield return Entity_Rejector;
            }
        }
        public override IEnumerable<Site> SitesInvolved
        {
            get { yield return Site; }
        }

        public HE_FirstContactFailed(XDocument xdoc, World world)
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
                    case "contactor_enid":
                        EntityId_Contactor = valI;
                        break;
                    case "rejector_enid":
                        EntityId_Rejector = valI;
                        break;
                    case "site_id":
                        SiteId = valI;
                        break;
                    default:
                        DFXMLParser.UnexpectedXmlElement(xdoc.Root.Name.LocalName + "\t" + Types[Type], element, xdoc.Root.ToString());
                        break;
                }
            }
        }

        protected override void WriteDataOnParent(MainForm frm, Control parent, ref Point location)
        {
            EventLabel(frm, parent, ref location, "Contactor:", Entity_Contactor);
            EventLabel(frm, parent, ref location, "Rejecter:", Entity_Rejector);
            EventLabel(frm, parent, ref location, "Site:", Site);
        }

        protected override string LegendsDescription()
        {
            var timestring = base.LegendsDescription();

            return $"{timestring} {Entity_Rejector} rejected contact with {Entity_Contactor} at {Site.AltName}.";
        }

        internal override string ToTimelineString()
        {
            var timelinestring = base.ToTimelineString();

            return $"{timelinestring} {Entity_Rejector} rejected contact with {Entity_Contactor} at {Site.AltName}.";
        }

        internal override void Export(string table)
        {
            base.Export(table);

            table = GetType().Name;
            
            var vals = new List<object>
            {
                Id, 
                SiteId.DBExport(), 
                EntityId_Contactor.DBExport(), 
                EntityId_Rejector.DBExport()
            };

            Database.ExportWorldItem(table, vals);

        }


    }
}

