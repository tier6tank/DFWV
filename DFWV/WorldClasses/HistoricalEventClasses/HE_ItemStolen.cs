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
        private int? EntityId_Attacker { get; set; }
        public Entity Entity_Attacker { private get; set; }
        private int? EntityId_Defender { get; set; }
        public Entity Entity_Defender { private get; set; }
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

        private string Circumstance { get; set; }
        private int? CircumstanceId { get; set; }
        private int? HistoricalEventCollectionId { get; set; }
        override public Point Location => Site?.Location ?? Coords;

        public override IEnumerable<HistoricalFigure> HFsInvolved
        {
            get { yield return Hf; }
        }
        public override IEnumerable<Entity> EntitiesInvolved
        {
            get
            {
                yield return Entity_Attacker;
                yield return Entity_Defender;
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
                    case "circumstance":
                        Circumstance = val;
                        break;
                    case "circumstance_id":
                        CircumstanceId = valI;
                        if (Circumstance == "historical_event_collection")
                        {
                            HistoricalEventCollectionId = valI;
                        }
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
            if (HistoricalEventCollectionId.HasValue && World.HistoricalEventCollections.ContainsKey(HistoricalEventCollectionId.Value))
                EventCollection = World.HistoricalEventCollections[HistoricalEventCollectionId.Value];
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
            EventLabel(frm, parent, ref location, "Victim:", Entity_Defender);
            EventLabel(frm, parent, ref location, "Theif:", Entity_Attacker);
            EventLabel(frm, parent, ref location, "Theif:", Hf);
            if (Mat != null || ItemType != null)
                EventLabel(frm, parent, ref location, "Item:",
                    $"{(Mat != null ? Item.Materials[Mat.Value] : "UNKNOWN")} {(ItemType != null ? Item.ItemTypes[ItemType.Value] : "UNKNOWN")}");
            
            EventLabel(frm, parent, ref location, "Item:", Hf);
            EventLabel(frm, parent, ref location, "Site:", Site);
            EventLabel(frm, parent, ref location, "Coords:", new Coordinate(Coords));
        }

        protected override string LegendsDescription()
        {
            
            var timestring = base.LegendsDescription();

            if (ItemType == null && Mat == null)
                return
                    $"{timestring} {"UNKNOWN"} was stolen from {"UNKNOWN"} in {Site?.ToString() ?? "UNKNOWN"} by the {"UNKNOWN"} {"UNKNOWN"} and brought to {"UNKNOWN"}";
            if (Hf != null)
                return
                    $"{timestring} {(Mat != null ? Item.Materials[Mat.Value] : "UNKNOWN")} {(ItemType != null ? Item.ItemTypes[ItemType.Value] : "UNKNOWN")} was stolen from {(Site == null ? "UNKNOWN" : Site.AltName)} by the {Hf.Race.ToString().ToLower()} {Hf}{""}."; 
            return
                $"{timestring} {(Mat != null ? Item.Materials[Mat.Value] : "UNKNOWN")} {(ItemType != null ? Item.ItemTypes[ItemType.Value] : "UNKNOWN")} was stolen from {(Site == null ? "UNKNOWN" : Site.AltName)} by an unknown creature{""}.";
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
                EntityId_Attacker.DBExport(), 
                EntityId_Defender.DBExport(), 
                SiteId.DBExport(),
                Coords.DBExport(),
                ItemID.DBExport(),
                ItemType.DBExport(Item.ItemTypes),
                ItemSubType.DBExport(Item.ItemSubTypes),
                Mat.DBExport(Item.Materials),
                EntityId.DBExport(),
                Hfid.DBExport(),
                StructureId.DBExport()
            };

            Database.ExportWorldItem(table, vals);
        }

    }
}

