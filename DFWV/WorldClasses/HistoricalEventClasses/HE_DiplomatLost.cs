using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Linq;
using DFWV.WorldClasses.EntityClasses;

namespace DFWV.WorldClasses.HistoricalEventClasses
{
    class HE_DiplomatLost : HistoricalEvent
    {
        private int? SiteID { get; set; }
        private Site Site { get; set; }

        public int? EntityID { get; set; }
        public Entity Entity { get; set; }

        public int? InvolvedID { get; set; }
        public Entity Involved { get; set; }

        override public Point Location => Site.Location;

        public override IEnumerable<Entity> EntitiesInvolved
        {
            get
            {
                yield return Entity;
                yield return Involved;
            }
        }
        public override IEnumerable<Site> SitesInvolved
        {
            get { yield return Site; }
        }

        public HE_DiplomatLost(XDocument xdoc, World world)
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
            if (EntityID.HasValue && World.Entities.ContainsKey(EntityID.Value))
                Entity = World.Entities[EntityID.Value];
            if (InvolvedID.HasValue && World.Entities.ContainsKey(InvolvedID.Value))
                Involved = World.Entities[InvolvedID.Value];

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
                    case "entity":
                        if (valI != -1)
                            EntityID = valI;
                        break;
                    case "involved":
                        if (valI != -1)
                            InvolvedID = valI;
                        break;
                    case "site":
                        break;
                    default:
                        DFXMLParser.UnexpectedXMLElement(xdoc.Root.Name.LocalName + "\t" + Types[Type], element, xdoc.Root.ToString());
                        break;
                }
            }
        }

        protected override void WriteDataOnParent(MainForm frm, Control parent, ref Point location)
        {
            //TODO: Incorporate new data
            EventLabel(frm, parent, ref location, "Site:", Site);
            EventLabel(frm, parent, ref location, "Entity:", Entity);
            EventLabel(frm, parent, ref location, "Involved:", Involved);
        }

        protected override string LegendsDescription() //Matched
        {
            var timestring = base.LegendsDescription();

            if (Entity != null && Involved != null)
                return
                    $"{timestring} {Entity} lost a diplomat at {Site.AltName}. They suspected the involvement of {Involved}.";

            return $"{timestring} A Diplomat was lost at {Site.AltName}.";
        }

        internal override string ToTimelineString()
        {
            //TODO: Incorporate new data
            var timelinestring = base.ToTimelineString();

            return $"{timelinestring} Diplomat lost at {Site.AltName}.";
        }

        internal override void Export(string table)
        {
            base.Export(table);

            table = GetType().Name;

            var vals = new List<object> 
            { 
                ID, 
                SiteID.DBExport(),
                EntityID.DBExport(),
                Involved.DBExport()
            };

            Database.ExportWorldItem(table, vals);

        }

    }
}
