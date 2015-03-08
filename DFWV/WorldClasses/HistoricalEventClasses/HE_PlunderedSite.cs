﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Linq;
using DFWV.WorldClasses.EntityClasses;

namespace DFWV.WorldClasses.HistoricalEventClasses
{
    public class HE_PlunderedSite : HistoricalEvent
    {
        private int? SiteID { get; set; }
        private Site Site { get; set; }
        private int? SiteCivID { get; set; }
        private Entity SiteCiv { get; set; }
        private int? AttackerCivID { get; set; }
        private Entity AttackerCiv { get; set; }
        private int? DefenderCivID { get; set; }
        private Entity DefenderCiv { get; set; }

        override public Point Location { get { return Site.Location; } }
        public override IEnumerable<Entity> EntitiesInvolved
        {
            get
            {
                yield return SiteCiv;
                yield return AttackerCiv;
                yield return DefenderCiv;
            }
        }

        public HE_PlunderedSite(XDocument xdoc, World world)
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
                    case "defender_civ_id":
                        DefenderCivID = valI;
                        break;
                    case "attacker_civ_id":
                        AttackerCivID = valI;
                        break;
                    case "site_civ_id":
                        SiteCivID = valI;
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
            if (SiteCivID.HasValue && World.Entities.ContainsKey(SiteCivID.Value))
                SiteCiv = World.Entities[SiteCivID.Value];
            if (AttackerCivID.HasValue && World.Entities.ContainsKey(AttackerCivID.Value))
                AttackerCiv = World.Entities[AttackerCivID.Value];
            if (DefenderCivID.HasValue && World.Entities.ContainsKey(DefenderCivID.Value))
                DefenderCiv = World.Entities[DefenderCivID.Value];
        }

        internal override void Process()
        {
            base.Process();
            if (Site.PlunderedEvents == null)
                Site.PlunderedEvents = new List<HE_PlunderedSite>();
            Site.PlunderedEvents.Add(this);
        }

        protected override void WriteDataOnParent(MainForm frm, Control parent, ref Point location)
        {
            EventLabel(frm, parent, ref location, "Attacker:", AttackerCiv);
            EventLabel(frm, parent, ref location, "Defender:", DefenderCiv);
            EventLabel(frm, parent, ref location, "Owners:", SiteCiv);
            EventLabel(frm, parent, ref location, "Site:", Site);

        }

        protected override string LegendsDescription()
        {
            var timestring = base.LegendsDescription();

            return string.Format("{0} {1} defeated {2} and pillaged {3}.",
                            timestring, AttackerCiv, DefenderCiv,
                            Site.AltName);
        }

        internal override string ToTimelineString()
        {
            var timelinestring = base.ToTimelineString();

            return string.Format("{0} {1} defeated {2} and pillaged {3}.",
                            timelinestring, AttackerCiv, DefenderCiv,
                            Site.AltName);
        }

        internal override void Export(string table)
        {
            base.Export(table);

            table = GetType().Name;

            var vals = new List<object>
            {
                ID,
                SiteID.DBExport(), 
                SiteCivID.DBExport(), 
                DefenderCivID.DBExport(), 
                AttackerCivID.DBExport()
            };

            Database.ExportWorldItem(table, vals);
        }
    }
}