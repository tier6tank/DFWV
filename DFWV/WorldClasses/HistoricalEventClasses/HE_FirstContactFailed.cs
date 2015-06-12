﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Linq;
using DFWV.WorldClasses.EntityClasses;

namespace DFWV.WorldClasses.HistoricalEventClasses
{
    class HE_FirstContactFailed : HistoricalEvent
    {
        private int? SiteID { get; set; }
        private Site Site { get; set; }
        private int? ContactorEnID { get; set; }
        private Entity ContactorEn { get; set; }
        private int? RejectorEnID { get; set; }
        private Entity RejectorEn { get; set; }

        override public Point Location { get { return Site.Location; } }

        public override IEnumerable<Entity> EntitiesInvolved
        {
            get
            {
                yield return ContactorEn;
                yield return RejectorEn;
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
                        ContactorEnID = valI;
                        break;
                    case "rejector_enid":
                        RejectorEnID = valI;
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
            base.Link();
            if (SiteID.HasValue && World.Sites.ContainsKey(SiteID.Value))
                Site = World.Sites[SiteID.Value];
            if (ContactorEnID.HasValue && World.Entities.ContainsKey(ContactorEnID.Value))
                ContactorEn = World.Entities[ContactorEnID.Value];
            if (RejectorEnID.HasValue && World.Entities.ContainsKey(RejectorEnID.Value))
                RejectorEn = World.Entities[RejectorEnID.Value];
        }


        protected override void WriteDataOnParent(MainForm frm, Control parent, ref Point location)
        {
            EventLabel(frm, parent, ref location, "Contactor:", ContactorEn);
            EventLabel(frm, parent, ref location, "Rejecter:", RejectorEn);
            EventLabel(frm, parent, ref location, "Site:", Site);
        }

        protected override string LegendsDescription()
        {
            var timestring = base.LegendsDescription();

            return string.Format("{0} {1} rejected contact with {2} at {3}.",
                                    timestring, RejectorEn, ContactorEn, Site.AltName);
        }

        internal override string ToTimelineString()
        {
            var timelinestring = base.ToTimelineString();

            return string.Format("{0} {1} rejected contact with {2} at {3}.",
                                    timelinestring, RejectorEn, ContactorEn, Site.AltName);
        }

        internal override void Export(string table)
        {
            base.Export(table);

            table = GetType().Name;
            
            var vals = new List<object>
            {
                ID, 
                SiteID.DBExport(), 
                ContactorEnID.DBExport(), 
                RejectorEnID.DBExport()
            };

            Database.ExportWorldItem(table, vals);

        }


    }
}

