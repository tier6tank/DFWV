using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;

namespace DFWV.WorldClasses.HistoricalEventClasses
{
    class HE_EntityPrimaryCriminals : HistoricalEvent
    {
        public int? EntityID { get; set; }
        public Entity Entity { get; set; }
        public int? SiteID { get; set; }
        public Site Site { get; set; }
        public int? StructureID { get; set; }
        public Structure Structure { get; set; }

        override public Point Location { get { return Site.Location; } }

        public HE_EntityPrimaryCriminals(XDocument xdoc, World world)
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
            if (StructureID.HasValue && StructureID.Value != -1 && Site != null)
            {
                if (Site.Structures == null)
                    Site.Structures = new Dictionary<int, Structure>();
                if (Site.Structures.ContainsKey(StructureID.Value))
                    Structure = Site.Structures[StructureID.Value];
                else
                {
                    Structure = new Structure(Site, StructureID.Value, World);
                    Site.Structures.Add(StructureID.Value, Structure);
                }

            }
            if (EntityID.HasValue && World.Entities.ContainsKey(EntityID.Value))
                Entity = World.Entities[EntityID.Value];
        }

        internal override void Process()
        {
            base.Process();
            if (Entity != null)
            {
                if (Entity.Events == null)
                    Entity.Events = new List<HistoricalEvent>();
                Entity.Events.Add(this);
            }
        }
        public override void WriteDataOnParent(MainForm frm, Control parent, ref Point location)
        {
            EventLabel(frm, parent, ref location, "Entity:", Entity);
            EventLabel(frm, parent, ref location, "Site:", Site);
            EventLabel(frm, parent, ref location, "Structure:", Structure);
        }

        public override string LegendsDescription()
        {
            string timestring = base.LegendsDescription();

            return string.Format("{0} {1} became the primary criminal organization in {2}.",
                            timestring, Entity.ToString(), Site.AltName);
        }

        internal override string ToTimelineString()
        {
            string timelinestring = base.ToTimelineString();

            return string.Format("{0} {1} became the primary criminal organization in {2}.",
                        timelinestring, Entity.ToString(), Site.AltName);
        }

        internal override void Export(string table)
        {
            base.Export(table);

            
            List<object> vals;
            table = this.GetType().Name.ToString();


            
            vals = new List<object>() { ID, EntityID, SiteID, StructureID };


            Database.ExportWorldItem(table, vals);

        }

    }
}


