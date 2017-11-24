﻿
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Linq;
using DFWV.WorldClasses.EntityClasses;

namespace DFWV.WorldClasses.HistoricalEventClasses
{
    public class HE_SiteTakenOver : HistoricalEvent
    {
        private int? SiteId { get; }
        public Site Site { get; private set; }
        private int? EntityId_SiteCiv { get; }
        public Entity Entity_SiteCiv { get; private set; }
        private int? EntityId_Attacker { get; }
        public Entity Entity_Attacker { get; private set; }
        private int? EntityId_Defender { get; }
        public Entity Entity_Defender { get; private set; }
        private int? EntityId_NewSiteCiv { get; }
        private Entity Entity_NewSiteCiv { get; set; }

        override public Point Location => Site.Location;

        public override IEnumerable<Entity> EntitiesInvolved
        {
            get
            {
                yield return Entity_SiteCiv;
                yield return Entity_Attacker;
                yield return Entity_Defender;
                yield return Entity_NewSiteCiv;
            }
        }
        public override IEnumerable<Site> SitesInvolved
        {
            get { yield return Site; }
        }


        public HE_SiteTakenOver(XDocument xdoc, World world)
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
                    case "attacker_civ_id":
                        EntityId_Attacker = valI;
                        break;
                    case "new_site_civ_id":
                        EntityId_NewSiteCiv = valI;
                        break;
                    case "defender_civ_id":
                        EntityId_Defender = valI;
                        break;
                    case "site_civ_id":
                        EntityId_SiteCiv = valI;
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
            EventLabel(frm, parent, ref location, "Attacker:", Entity_Attacker);
            EventLabel(frm, parent, ref location, "New Owner:", Entity_NewSiteCiv);
            EventLabel(frm, parent, ref location, "Defender:", Entity_Defender);
            EventLabel(frm, parent, ref location, "Old Owner:", Entity_SiteCiv);
            EventLabel(frm, parent, ref location, "Site:", Site);
        }

        protected override string LegendsDescription()
        {
            var timestring = base.LegendsDescription();

            return
                $"{timestring} {Entity_Attacker} defeated {Entity_SiteCiv} and took over {Site.AltName}. The new government was called {Entity_NewSiteCiv}.";
        }

        internal override string ToTimelineString()
        {
            var timelinestring = base.ToTimelineString();

            return $"{timelinestring} {Entity_Attacker} captured {Site.AltName} from {Entity_Defender}.";
        }

        internal override void Export(string table)
        {
            base.Export(table);

            table = GetType().Name;

            var vals = new List<object>
            {
                Id, 
                SiteId.DBExport(), 
                EntityId_SiteCiv.DBExport(), 
                EntityId_Defender.DBExport(), 
                EntityId_NewSiteCiv.DBExport(), 
                EntityId_Attacker.DBExport()
            };

            Database.ExportWorldItem(table, vals);
        }
    }
}
