using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Linq;
using DFWV.WorldClasses.EntityClasses;
using DFWV.WorldClasses.HistoricalFigureClasses;

namespace DFWV.WorldClasses.HistoricalEventClasses
{
    class HE_ItemStolen : HistoricalEvent
    {
        private int? AttackerCivId { get; set; }
        public Entity AttackerCiv { private get; set; }
        private int? DefenderCivId { get; set; }
        public Entity DefenderCiv { private get; set; }
        private int? SiteId { get; set; }
        public Site Site { private get; set; }
        public Point Coords { private get; set; }
        private int? ItemID { get; set; }
        private int? ItemType { get; set; }
        private int? ItemSubType { get; set; }
        private int? Mat { get; set; }
        private int? MatType { get; set; }
        private int? MatIndex  { get; set; }
        private HistoricalFigure Hf { get; set; }
        private int? Hfid { get; set; }
        private Entity Entity { get; set; }
        private int? EntityId { get; set; }
        private Structure Structure { get; set; }
        private int? StructureId { get; set; }

        override public Point Location => Site?.Location ?? Coords;

        public override IEnumerable<HistoricalFigure> HFsInvolved
        {
            get { yield return Hf; }
        }
        public override IEnumerable<Entity> EntitiesInvolved
        {
            get
            {
                yield return AttackerCiv;
                yield return DefenderCiv;
                yield return Entity;
            }
        }
        public override IEnumerable<Site> SitesInvolved
        {
            get { yield return Site; }
        }


        public HE_ItemStolen(XDocument xdoc, World world)
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
                        break;
                    case "item":
                        ItemID = valI;
                        break;
                    case "item_type":
                        if (!Item.ItemTypes.Contains(val))
                            Item.ItemTypes.Add(val);
                        ItemType = Item.ItemTypes.IndexOf(val);
                        break;
                    case "item_subtype":
                        if (valI != -1)
                        {
                            if (!Item.ItemSubTypes.Contains(val))
                                Item.ItemSubTypes.Add(val);
                            ItemSubType = Item.ItemSubTypes.IndexOf(val);
                        }
                        break;
                    case "mattype":
                        MatType = valI;
                        break;
                    case "matindex":
                        MatIndex = valI;
                        break;
                    case "mat":
                        if (!Item.Materials.Contains(val))
                            Item.Materials.Add(val);
                        Mat = Item.Materials.IndexOf(val);
                        break;
                    case "entity":
                        EntityId = valI;
                        break;
                    case "histfig":
                        Hfid = valI;
                        break;
                    case "site":
                        SiteId = valI;
                        break;
                    case "structure":
                        if (valI != -1)
                            StructureId = valI;
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
            if (EntityId.HasValue && World.Entities.ContainsKey(EntityId.Value))
                Entity = World.Entities[EntityId.Value];
            if (Hfid.HasValue && World.HistoricalFigures.ContainsKey(Hfid.Value))
                Hf = World.HistoricalFigures[Hfid.Value];
            if (SiteId.HasValue && World.Sites.ContainsKey(SiteId.Value))
            {
                Site = World.Sites[SiteId.Value];
                if (StructureId.HasValue)
                {
                    Structure = Site.GetStructure(StructureId.Value);

                    if (Structure == null)
                    {
                        Structure = new Structure(Site, StructureId.Value, World);
                        Site.AddStructure(Structure);
                    }
                }
            }
        }

        internal override void Process()
        {
            base.Process();
            if (Structure != null)
            {
                if (Structure.Events == null)
                    Structure.Events = new List<HistoricalEvent>();
                Structure.Events.Add(this);
            }
        }

        protected override void WriteDataOnParent(MainForm frm, Control parent, ref Point location)
        {
            EventLabel(frm, parent, ref location, "Victim:", DefenderCiv);
            EventLabel(frm, parent, ref location, "Theif:", AttackerCiv);
            EventLabel(frm, parent, ref location, "Theif:", Hf);
            if (Mat != null || ItemType != null)
                EventLabel(frm, parent, ref location, "Item:",
                    $"{(Mat != null ? Materials[Mat.Value] : "UNKNOWN")} {(ItemType != null ? ItemTypes[ItemType.Value] : "UNKNOWN")}");
            
            EventLabel(frm, parent, ref location, "Item:", Hf);
            EventLabel(frm, parent, ref location, "Site:", Site);
            EventLabel(frm, parent, ref location, "Coords:", new Coordinate(Coords));
        }

        protected override string LegendsDescription() //Matched
        {

            var timestring = base.LegendsDescription();

            if (ItemType == null && Mat == null)
                return
                    $"{timestring} {"UNKNOWN"} was stolen from {"UNKNOWN"} in {Site?.ToString() ?? "UNKNOWN"} by the {"UNKNOWN"} {"UNKNOWN"} and brought to {"UNKNOWN"}";
            if (Hf != null)
                return
                    $"{timestring} {(Mat != null ? Materials[Mat.Value] : "UNKNOWN")} {(ItemType != null ? ItemTypes[ItemType.Value] : "UNKNOWN")} was stolen from {(Site == null ? "UNKNOWN" : Site.AltName)} by the {Hf.Race.ToString().ToLower()} {Hf}{""}."; //TODO: Missing "and brought to [Site]"
            return
                $"{timestring} {(Mat != null ? Materials[Mat.Value] : "UNKNOWN")} {(ItemType != null ? ItemTypes[ItemType.Value] : "UNKNOWN")} was stolen from {(Site == null ? "UNKNOWN" : Site.AltName)} by an unknown creature{""}."; //TODO: Missing "and brought to [Site]"
        }

        internal override string ToTimelineString()
        {
            //TODO: Incorporate new data
            var timelinestring = base.ToTimelineString();
            
            if (Site == null)
                return $"{timelinestring} Item stolen.";
            return $"{timelinestring} Item stolen from {Site.AltName}";
        }

        internal override void Export(string table)
        {
            base.Export(table);

            table = GetType().Name;

            var vals = new List<object>
            {
                Id, 
                AttackerCivId.DBExport(), 
                DefenderCivId.DBExport(), 
                SiteId.DBExport(),
                Coords.DBExport(),
                Item.DBExport(),
                ItemType.DBExport(ItemTypes),
                ItemSubType.DBExport(ItemSubTypes),
                Mat.DBExport(Materials),
                EntityId.DBExport(),
                Hfid.DBExport(),
                StructureId.DBExport()
            };

            Database.ExportWorldItem(table, vals);
        }

    }
}

