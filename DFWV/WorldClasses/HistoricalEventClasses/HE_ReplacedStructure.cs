using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Linq;
using DFWV.WorldClasses.EntityClasses;

namespace DFWV.WorldClasses.HistoricalEventClasses
{
    class HE_ReplacedStructure : HistoricalEvent
    {
        private int? SiteId { get; }
        public Site Site { get; set; }
        private int? StructureId_Old { get; }
        private Structure Structure_Old { get; set; }
        private int? StructureId_New { get; }
        private Structure Structure_New { get; set; }
        private int? EntityId_SiteCiv { get; }
        public Entity Entity_SiteCiv { get; set; }
        private int? EntityId_Civ { get; }
        public Entity Entity_Civ { get; set; }

        override public Point Location => Site.Location;

        public override IEnumerable<Entity> EntitiesInvolved
        {
            get
            {
                yield return Entity_SiteCiv;
                yield return Entity_Civ;
            }
        }
        public override IEnumerable<Site> SitesInvolved
        {
            get { yield return Site; }
        }


        public HE_ReplacedStructure(XDocument xdoc, World world)
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
                    case "civ_id":
                        EntityId_Civ = valI;
                        break;
                    case "site_civ_id":
                        EntityId_SiteCiv = valI;
                        break;
                    case "site_id":
                        SiteId = valI;
                        break;
                    case "old_ab_id":
                        StructureId_Old = valI;
                        break;
                    case "new_ab_id":
                        StructureId_New = valI;
                        break;

                    default:
                        DFXMLParser.UnexpectedXmlElement(xdoc.Root.Name.LocalName + "\t" + Types[Type], element, xdoc.Root.ToString());
                        break;
                }
            }
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
                    case "civ":
                    case "site_civ":
                    case "site":
                    case "old_structure":
                    case "new_structure":
                        break;
                    default:
                        DFXMLParser.UnexpectedXmlElement(xdoc.Root.Name.LocalName + "\t" + Types[Type], element, xdoc.Root.ToString());
                        break;
                }
            }
        }

        internal override void Process()
        {
            base.Process();
            if (Structure_Old != null)
            {
                if (Structure_Old.Events == null)
                    Structure_Old.Events = new List<HistoricalEvent>();
                Structure_Old.Events.Add(this);
            }

            if (Structure_New != null)
            {
                if (Structure_New.Events == null)
                    Structure_New.Events = new List<HistoricalEvent>();
                Structure_New.Events.Add(this);
            }
        }

        protected override void WriteDataOnParent(MainForm frm, Control parent, ref Point location)
        {
            if (Entity_Civ != null)
                EventLabel(frm, parent, ref location, "Civ:", Entity_Civ);
            if (Entity_SiteCiv != null)
                EventLabel(frm, parent, ref location, "Owner:", Entity_SiteCiv);

            EventLabel(frm, parent, ref location, "Site:", Site);
            EventLabel(frm, parent, ref location, "Old Structure:", Structure_Old);
            EventLabel(frm, parent, ref location, "New Structure:", Structure_New);
        }

        protected override string LegendsDescription()
        {
            var timestring = base.LegendsDescription();

            if (Entity_SiteCiv == null)
                return
                    $"{timestring} {Entity_Civ} replaced {(Structure_Old != null ? Structure_Old.Name : "UNKNOWN")} in {Site.AltName} with {(Structure_New != null ? Structure_New.Name : "UNKNOWN")}.";

            return
                $"{timestring} {Entity_SiteCiv} of {Entity_Civ} replaced {(Structure_Old != null ? Structure_Old.Name : "UNKNOWN")} in {Site.AltName} with {(Structure_New != null ? Structure_New.Name : "UNKNOWN")}.";
        }

        internal override string ToTimelineString()
        {
            var timelinestring = base.ToTimelineString();

            return $"{timelinestring} {Entity_Civ} replaced a structure in {Site.AltName}.";
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
                EntityId_Civ.DBExport(), 
                StructureId_Old.DBExport(),
                StructureId_New.DBExport()
            };

            Database.ExportWorldItem(table, vals);
        }
    }
}
