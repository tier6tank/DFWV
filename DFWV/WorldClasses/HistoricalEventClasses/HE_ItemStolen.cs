using System;
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
        private int? AttackerCivID { get; set; }
        public Entity AttackerCiv { private get; set; }
        private int? DefenderCivID { get; set; }
        public Entity DefenderCiv { private get; set; }
        private int? SiteID { get; set; }
        public Site Site { private get; set; }
        public Point Coords { private get; set; }
        private int? Item { get; set; }
        private int? ItemType { get; set; }
        private int? ItemSubType { get; set; }
        private int? Mat { get; set; }
        private int? MatType { get; set; }
        private int? MatIndex  { get; set; }
        private HistoricalFigure HF { get; set; }
        private int? HFID { get; set; }
        private Entity Entity { get; set; }
        private int? EntityID { get; set; }
        private Structure Structure { get; set; }
        private int? StructureID { get; set; }

        override public Point Location { get { return Site != null ? Site.Location : Coords; } }
        public override IEnumerable<HistoricalFigure> HFsInvolved
        {
            get { yield return HF; }
        }
        public HE_ItemStolen(XDocument xdoc, World world)
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
                    default:
                        DFXMLParser.UnexpectedXMLElement(xdoc.Root.Name.LocalName + "\t" + Types[Type], element, xdoc.Root.ToString());
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
                Int32.TryParse(val, out valI);

                switch (element.Name.LocalName)
                {
                    case "id":
                    case "type":
                        break;
                    case "item":
                        Item = valI;
                        break;
                    case "item_type":
                        if (!ItemTypes.Contains(val))
                            ItemTypes.Add(val);
                        ItemType = ItemTypes.IndexOf(val);
                        break;
                    case "item_subtype":
                        if (valI != -1)
                        {
                            if (!ItemSubTypes.Contains(val))
                                ItemSubTypes.Add(val);
                            ItemSubType = ItemSubTypes.IndexOf(val);
                        }
                        break;
                    case "mattype":
                        MatType = valI;
                        break;
                    case "matindex":
                        MatIndex = valI;
                        break;
                    case "mat":
                        if (!Materials.Contains(val))
                            Materials.Add(val);
                        Mat = Materials.IndexOf(val);
                        break;
                    case "entity":
                        EntityID = valI;
                        break;
                    case "histfig":
                        HFID = valI;
                        break;
                    case "site":
                        SiteID = valI;
                        break;
                    case "structure":
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
            if (EntityID.HasValue && World.Entities.ContainsKey(EntityID.Value))
                Entity = World.Entities[EntityID.Value];
            if (HFID.HasValue && World.HistoricalFigures.ContainsKey(HFID.Value))
                HF = World.HistoricalFigures[HFID.Value];
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
        }

        internal override void Process()
        {
            base.Process();
            if (AttackerCiv != null)
            {
                if (AttackerCiv.Events == null)
                    AttackerCiv.Events = new List<HistoricalEvent>();
                AttackerCiv.Events.Add(this);
            }
            if (DefenderCiv != null)
            {
                if (DefenderCiv.Events == null)
                    DefenderCiv.Events = new List<HistoricalEvent>();
                DefenderCiv.Events.Add(this);
            }
            if (Entity != null && AttackerCiv != Entity && DefenderCiv != Entity)
            {
                if (Entity.Events == null)
                    Entity.Events = new List<HistoricalEvent>();
                Entity.Events.Add(this);
            }
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
            EventLabel(frm, parent, ref location, "Theif:", HF);
            if (Mat != null || ItemType != null)
                EventLabel(frm, parent, ref location, "Item:", String.Format("{0} {1}",Mat != null ? Materials[Mat.Value] : "UNKNOWN", ItemType != null ? ItemTypes[ItemType.Value] : "UNKNOWN"));
            
            EventLabel(frm, parent, ref location, "Item:", HF);
            EventLabel(frm, parent, ref location, "Site:", Site);
            EventLabel(frm, parent, ref location, "Coords:", new Coordinate(Coords));
        }

        protected override string LegendsDescription() //Matched
        {

            var timestring = base.LegendsDescription();

            if (ItemType == null && Mat == null)
                return string.Format("{0} {1} was stolen from {2} in {3} by the {4} {5} and brought to {6}",
                        timestring, "UNKNOWN", "UNKNOWN", 
                        Site == null ? "UNKNOWN" : Site.ToString(), "UNKNOWN", "UNKNOWN", "UNKNOWN");
            if (HF != null)
                return string.Format("{0} {1} {2} was stolen from {3} by the {4} {5}{6}.",
                    //TODO: Missing "and taken to blah blah"
                    timestring,
                    Mat != null ? Materials[Mat.Value] : "UNKNOWN",
                    ItemType != null ? ItemTypes[ItemType.Value] : "UNKNOWN",
                    Site == null ? "UNKNOWN" : Site.AltName,
                    HF.Race.ToString().ToLower(),
                    HF,
                    ""); //TODO: Missing "and brought to [Site]"
            return string.Format("{0} {1} {2} was stolen from {3} by an unknown creature{4}.",
                timestring,
                Mat != null ? Materials[Mat.Value] : "UNKNOWN",
                ItemType != null ? ItemTypes[ItemType.Value] : "UNKNOWN",
                Site == null ? "UNKNOWN" : Site.AltName,
                ""); //TODO: Missing "and brought to [Site]"
        }

        internal override string ToTimelineString()
        {
            //TODO: Incorporate new data
            var timelinestring = base.ToTimelineString();
            
            if (Site == null)
                return string.Format("{0} Item stolen.",
                        timelinestring);
            return string.Format("{0} Item stolen from {1}",
                timelinestring, Site.AltName);
        }

        internal override void Export(string table)
        {
            base.Export(table);

            table = GetType().Name;

            var vals = new List<object>
            {
                ID, 
                AttackerCivID.DBExport(), 
                DefenderCivID.DBExport(), 
                SiteID.DBExport(),
                Coords.DBExport(),
                Item.DBExport(),
                ItemType.DBExport(ItemTypes),
                ItemSubType.DBExport(ItemSubTypes),
                Mat.DBExport(Materials),
                EntityID.DBExport(),
                HFID.DBExport(),
                StructureID.DBExport()
            };

            Database.ExportWorldItem(table, vals);
        }

    }
}

