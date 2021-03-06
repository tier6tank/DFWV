﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;
using DFWV.WorldClasses.HistoricalEventCollectionClasses;

namespace DFWV.WorldClasses.HistoricalEventClasses
{
    class HE_PeaceRejected : HistoricalEvent
    {
        public int? SiteID { get; set; }
        public Site Site { get; set; }

        override public Point Location { get { return Site != null ? Site.Location : Point.Empty; } }


        public HE_PeaceRejected(XDocument xdoc, World world)
            : base(xdoc, world)
        {
            foreach (XElement element in xdoc.Root.Elements())
            {
                string val = element.Value.ToString();
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
                        if (valI != -1)
                            SiteID = valI;
                        break;
                    default:
                        DFXMLParser.UnexpectedXMLElement(xdoc.Root.Name.LocalName + "\t" + HistoricalEvent.Types[Type], element, xdoc.Root.ToString());
                        break;
                }
            }
        }

        internal override void Link()
        {
            base.Link();
            if (SiteID.HasValue && World.Sites.ContainsKey(SiteID.Value))
                Site = World.Sites[SiteID.Value];
        }

        public override void WriteDataOnParent(MainForm frm, Control parent, ref Point location)
        {
            EventLabel(frm, parent, ref location, "Site:", Site);
        }

        public override string LegendsDescription()
        {
            string timestring = base.LegendsDescription();

            if (EventCollection != null)
            {
                EC_War war = (EC_War)EventCollection;
                return string.Format("{0} {1} rejected an offer of peace from {2}.",
                    timestring, war.AggressorEnt.ToString(), war.DefenderEnt.ToString());
            }
            else 
                return timestring;
        }

        internal override string ToTimelineString()
        {
            string timelinestring = base.ToTimelineString();

            if (EventCollection != null)
            {
                EC_War war = (EC_War)EventCollection;
                return string.Format("{0} {1} rejected peace from {2}.",
                                timelinestring, war.AggressorEnt.ToString(),
                                war.DefenderEnt.ToString());
            }
            else
                return timelinestring;
        }

        internal override void Export(string table)
        {
            base.Export(table);

            
            List<object> vals;
            table = this.GetType().Name.ToString();



            vals = new List<object>() { ID, SiteID };


            Database.ExportWorldItem(table, vals);

        }

    }
}