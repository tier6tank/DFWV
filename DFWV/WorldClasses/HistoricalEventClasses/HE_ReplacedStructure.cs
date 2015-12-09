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
        private int? OldStructureId { get; }
        private Structure OldStructure { get; set; }
        private int? NewStructureId { get; }
        private Structure NewStructure { get; set; }
        private int? SiteCivId { get; }
        public Entity SiteCiv { get; set; }
        private int? CivId { get; }
        public Entity Civ { get; set; }

        override public Point Location => Site.Location;

        public override IEnumerable<Entity> EntitiesInvolved
        {
            get
            {
                yield return SiteCiv;
                yield return Civ;
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
                        CivId = valI;
                        break;
                    case "site_civ_id":
                        SiteCivId = valI;
                        break;
                    case "site_id":
                        SiteId = valI;
                        break;
                    case "old_ab_id":
                        OldStructureId = valI;
                        break;
                    case "new_ab_id":
                        NewStructureId = valI;
                        break;

                    default:
                        DFXMLParser.UnexpectedXmlElement(xdoc.Root.Name.LocalName + "\t" + Types[Type], element, xdoc.Root.ToString());
                        break;
                }
            }
        }
        internal override void Link()
        {
            base.Link();
            if (SiteId.HasValue && World.Sites.ContainsKey(SiteId.Value))
                Site = World.Sites[SiteId.Value];
            if (CivId.HasValue && World.Entities.ContainsKey(CivId.Value))
                Civ = World.Entities[CivId.Value];
            if (SiteCivId.HasValue && World.Entities.ContainsKey(SiteCivId.Value))
                SiteCiv = World.Entities[SiteCivId.Value];

            if (OldStructureId.HasValue && OldStructureId.Value != -1 && Site != null)
            {
                OldStructure = Site.GetStructure(OldStructureId.Value);
                if (OldStructure == null)
                {
                    OldStructure = new Structure(Site, OldStructureId.Value, World);
                    Site.AddStructure(OldStructure);
                }
            }

            if (NewStructureId.HasValue && NewStructureId.Value != -1 && Site != null)
            {
                NewStructure = Site.GetStructure(NewStructureId.Value);
                if (NewStructure == null)
                {
                    NewStructure = new Structure(Site, NewStructureId.Value, World);
                    Site.AddStructure(NewStructure);
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
            if (OldStructure != null)
            {
                if (OldStructure.Events == null)
                    OldStructure.Events = new List<HistoricalEvent>();
                OldStructure.Events.Add(this);
            }

            if (NewStructure != null)
            {
                if (NewStructure.Events == null)
                    NewStructure.Events = new List<HistoricalEvent>();
                NewStructure.Events.Add(this);
            }
        }

        protected override void WriteDataOnParent(MainForm frm, Control parent, ref Point location)
        {
            if (Civ != null)
                EventLabel(frm, parent, ref location, "Civ:", Civ);
            if (SiteCiv != null)
                EventLabel(frm, parent, ref location, "Owner:", SiteCiv);

            EventLabel(frm, parent, ref location, "Site:", Site);
            EventLabel(frm, parent, ref location, "Old Structure:", OldStructure);
            EventLabel(frm, parent, ref location, "New Structure:", NewStructure);
        }

        protected override string LegendsDescription()
        {
            var timestring = base.LegendsDescription();

            if (SiteCiv == null)
                return
                    $"{timestring} {Civ} replaced {(OldStructure != null ? OldStructure.Name : "UNKNOWN")} in {Site.AltName} with {(NewStructure != null ? NewStructure.Name : "UNKNOWN")}.";

            return
                $"{timestring} {SiteCiv} of {Civ} replaced {(OldStructure != null ? OldStructure.Name : "UNKNOWN")} in {Site.AltName} with {(NewStructure != null ? NewStructure.Name : "UNKNOWN")}.";
        }

        internal override string ToTimelineString()
        {
            var timelinestring = base.ToTimelineString();

            return $"{timelinestring} {Civ} replaced a structure in {Site.AltName}.";
        }

        internal override void Export(string table)
        {
            base.Export(table);

            table = GetType().Name;

            var vals = new List<object>
            {
                Id, 
                SiteId.DBExport(), 
                SiteCivId.DBExport(), 
                CivId.DBExport(), 
                OldStructureId.DBExport(),
                NewStructureId.DBExport()
            };

            Database.ExportWorldItem(table, vals);
        }
    }
}
