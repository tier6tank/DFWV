using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Linq;
using DFWV.WorldClasses.EntityClasses;

namespace DFWV.WorldClasses.HistoricalEventClasses
{
    class HE_EntityRelocate : HistoricalEvent
    {
        private int? EntityID { get; set; }
        private Entity Entity { get; set; }
        private int? SiteID { get; set; }
        private Site Site { get; set; }
        private int? StructureID { get; set; }
        private Structure Structure { get; set; }

        override public Point Location { get { return Site.Location; } }

        public HE_EntityRelocate(XDocument xdoc, World world)
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
                    case "entity_id":
                        EntityID = valI;
                        break;
                    case "site_id":
                        SiteID = valI;
                        break;
                    case "structure_id":
                        if (valI != -1)
                            StructureID = valI;
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
            {
                Site = World.Sites[SiteID.Value];
                if (StructureID.HasValue)
                {
                    Structure = Site.GetStructure(StructureID.Value);

                    if (Structure == null)
                    {
                        Structure = new Structure(Site, StructureID.Value, World);
                        Site.AddStructure(Structure);
                    }
                }
            }
            if (EntityID.HasValue && World.Entities.ContainsKey(EntityID.Value))
                Entity = World.Entities[EntityID.Value];
        }

        internal override void Process()
        {
            base.Process();
            if (Entity == null) return;
            if (Entity.Events == null)
                Entity.Events = new List<HistoricalEvent>();
            Entity.Events.Add(this);

            if (Structure != null)
            {
                if (Structure.Events == null)
                    Structure.Events = new List<HistoricalEvent>();
                Structure.Events.Add(this);
            }

        }

        protected override void WriteDataOnParent(MainForm frm, Control parent, ref Point location)
        {
            EventLabel(frm, parent, ref location, "Entity:", Entity);
            EventLabel(frm, parent, ref location, "Site:", Site);
            EventLabel(frm, parent, ref location, "Structure:", Structure);
        }

        protected override string LegendsDescription() //Not Matched - Verify structure names are right
        {
            var timestring = base.LegendsDescription();

            return string.Format("{0} {1} moved to {2} in {3}.",
                timestring, Entity, Structure == null ? "UNKNOWN" : Structure.Name,
                            Site.AltName);
        }

        internal override string ToTimelineString()
        {
            var timelinestring = base.ToTimelineString();

            return string.Format("{0} {1} moved to {2}.",
                        timelinestring, Entity, Site.AltName);
        }

        internal override void Export(string table)
        {
            base.Export(table);

            table = GetType().Name;

            var vals = new List<object>
            {
                ID, 
                EntityID.DBExport(), 
                SiteID.DBExport(), 
                StructureID.DBExport()
            };

            Database.ExportWorldItem(table, vals);

        }

    }
}


