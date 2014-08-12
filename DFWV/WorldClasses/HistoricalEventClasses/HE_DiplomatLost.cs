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

        override public Point Location { get { return Site.Location; } }

        public HE_DiplomatLost(XDocument xdoc, World world)
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

        internal override void Process()
        {
            base.Process();
            if (Involved != null)
            {
                if (Involved.Events == null)
                    Involved.Events = new List<HistoricalEvent>();
                Involved.Events.Add(this);
            }
            if (Entity != null)
            {
                if (Entity.Events == null)
                    Entity.Events = new List<HistoricalEvent>();
                Entity.Events.Add(this);
            }
        }
        internal override void Plus(XDocument xdoc)
        {
            foreach (var element in xdoc.Root.Elements())
            {
                var val = element.Value;
                int valI;
                Int32.TryParse(val, out valI);

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
                return string.Format("{0} {1} lost a diplomat at {2}. They suspected the involvement of {3}.",
                                        timestring, Entity, Site.AltName, Involved);

            return string.Format("{0} A Diplomat was lost at {1}.",
                                    timestring, Site.AltName);
        }

        internal override string ToTimelineString()
        {
            //TODO: Incorporate new data
            var timelinestring = base.ToTimelineString();

            return string.Format("{0} Diplomat lost at {1}.",
                        timelinestring, Site.AltName);
        }

        internal override void Export(string table)
        {
            //TODO: Incorporate new data
            base.Export(table);


            table = GetType().Name;


            
            var vals = new List<object> { ID, SiteID };


            Database.ExportWorldItem(table, vals);

        }

    }
}
