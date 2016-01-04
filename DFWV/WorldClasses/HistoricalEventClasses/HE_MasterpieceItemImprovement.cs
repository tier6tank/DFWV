using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Linq;
using DFWV.WorldClasses.EntityClasses;
using DFWV.WorldClasses.HistoricalFigureClasses;

namespace DFWV.WorldClasses.HistoricalEventClasses
{
    class HE_MasterpieceItemImprovement : HistoricalEvent
    {
        private int? Hfid { get; }
        private HistoricalFigure Hf { get; set; }
        private int? EntityId { get; }
        private Entity Entity { get; set; }
        private int? SiteId { get; }
        private Site Site { get; set; }
        private int? SkillAtTime { get; }

        private int? ItemType { get; set; }
        public int? ItemSubType { get; set; }
        public int? MatType { get; set; }
        public int? MatIndex { get; set; }
        public int? Mat { get; set; }
        public int? ImprovementType { get; set; }
        public int? ImprovementSubType { get; set; }
        public int? ImprovementMat { get; set; }
        public int? ArtId { get; set; }
        public int? ArtSubId { get; set; }

        public static List<string> ImprovementTypes = new List<string>();

        override public Point Location => Site.Location;

        public override IEnumerable<HistoricalFigure> HFsInvolved
        {
            get { yield return Hf; }
        }
        public override IEnumerable<Entity> EntitiesInvolved
        {
            get { yield return Entity; }
        }
        public override IEnumerable<Site> SitesInvolved
        {
            get { yield return Site; }
        }


        public HE_MasterpieceItemImprovement(XDocument xdoc, World world)
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
                    case "hfid":
                        Hfid = valI;
                        break;
                    case "entity_id":
                        EntityId = valI;
                        break;
                    case "site_id":
                        SiteId = valI;
                        break;
                    case "skill_at_time":
                        SkillAtTime = valI;
                        break;
                    default:
                        DFXMLParser.UnexpectedXmlElement(xdoc.Root.Name.LocalName + "\t" + Types[Type], element, xdoc.Root.ToString());
                        break;
                }
            }
        }

        internal override void Link()
        {
            //TODO: Incorporate new data
            base.Link();
            if (Hfid.HasValue && World.HistoricalFigures.ContainsKey(Hfid.Value))
                Hf = World.HistoricalFigures[Hfid.Value];
            if (SiteId.HasValue && World.Sites.ContainsKey(SiteId.Value))
                Site = World.Sites[SiteId.Value];
            if (EntityId.HasValue && World.Entities.ContainsKey(EntityId.Value))
                Entity = World.Entities[EntityId.Value];
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
                    case "maker":
                    case "maker_entity":
                    case "site":
                        break;
                    case "item_type":
                        if (!Item.ItemTypes.Contains(val))
                            Item.ItemTypes.Add(val);
                        ItemType = Item.ItemTypes.IndexOf(val);
                        break;
                    case "item_subtype":
                        if (!Item.ItemSubTypes.Contains(val))
                            Item.ItemSubTypes.Add(val);
                        ItemSubType = Item.ItemSubTypes.IndexOf(val);
                        break;
                    case "mat":
                        if (!Item.Materials.Contains(val))
                            Item.Materials.Add(val);
                        Mat = Item.Materials.IndexOf(val);
                        break;
                    case "mattype":
                        MatType = valI;
                        break;
                    case "matindex":
                        MatIndex = valI;
                        break;
                    case "improvement_type":
                        if (!ImprovementTypes.Contains(val))
                            ImprovementTypes.Add(val);
                        ImprovementType = ImprovementTypes.IndexOf(val);
                        break;
                    case "improvement_subtype":
                        ImprovementSubType = valI;
                        break;
                    case "imp_mat":
                        if (!Item.Materials.Contains(val))
                            Item.Materials.Add(val);
                        ImprovementMat = Item.Materials.IndexOf(val);
                        break;
                    case "art_id":
                        ArtId = valI;
                        break;
                    case "art_subid":
                        ArtSubId = valI;
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
            if (Site != null)
                Site.IsPlayerControlled = true;
            Entity?.MakePlayer();
        }

        protected override void WriteDataOnParent(MainForm frm, Control parent, ref Point location)
        {
            //TODO: Incorporate new data
            EventLabel(frm, parent, ref location, "HF:", Hf);
            EventLabel(frm, parent, ref location, "Entity:", Entity);
            EventLabel(frm, parent, ref location, "Site:", Site);
            EventLabel(frm, parent, ref location, "Skill:", SkillAtTime.ToString());
        }

        protected override string LegendsDescription()
        {
            //TODO: Incorporate new data
            var timestring = base.LegendsDescription();

            if (ImprovementType.HasValue && ImprovementMat.HasValue && Mat.HasValue && ItemType.HasValue)
            {
                switch (ImprovementTypes[ImprovementType.Value])
                {
                    case "spikes":
                        return
                            $"{timestring} {Hf} added masterful {ImprovementTypes[ImprovementType.Value]} of {Materials[ImprovementMat.Value]} to a {Materials[Mat.Value]} {ItemTypes[ItemType.Value]} for {Entity} at {Site.AltName}.";
                    case "rings_hanging":
                        return
                            $"{timestring} {Hf} added masterful rings in {Materials[ImprovementMat.Value]} to a {Materials[Mat.Value]} {ItemTypes[ItemType.Value]} for {Entity} at {Site.AltName}.";
                    case "bands":
                        return
                            $"{timestring} {Hf} added masterful bands in {Materials[ImprovementMat.Value]} to a {Materials[Mat.Value]} {ItemTypes[ItemType.Value]} for {Entity} at {Site.AltName}.";
                    case "covered":
                        return
                            $"{timestring} {Hf} added masterful covering in {Materials[ImprovementMat.Value]} to a {Materials[Mat.Value]} {ItemTypes[ItemType.Value]} for {Entity} at {Site.AltName}.";
                    default:
                        return
                            $"{timestring} {Hf} added masterful {ImprovementTypes[ImprovementType.Value]} of {Materials[ImprovementMat.Value]} to a {Materials[Mat.Value]} {ItemTypes[ItemType.Value]} for {Entity} at {Site.AltName}.";
                }
            }

            return $"{timestring} {Hf} added a masterful {"UNKNOWN"} for {Entity} at {Site.AltName}.";
        }

        internal override string ToTimelineString()
        {
            //TODO: Incorporate new data
            var timelinestring = base.ToTimelineString();

            return $"{timelinestring} {Hf} masterfully improved an item for {Entity} at {Site.AltName}.";
        }

        internal override void Export(string table)
        {
            base.Export(table);

            table = GetType().Name;

            var vals = new List<object>
            {
                Id, 
                Hfid.DBExport(), 
                EntityId.DBExport(), 
                SiteId.DBExport(), 
                SkillAtTime,
                ItemType.DBExport(ItemTypes),
                ItemSubType.DBExport(ItemSubTypes),
                Mat.DBExport(Materials),
                ImprovementType.DBExport(ImprovementTypes),
                //ImprovementSubType.DBExport(), //TODO: Uncomment this
                ImprovementMat.DBExport(Materials),
                ArtId.DBExport(),
                ArtSubId.DBExport()
            };

            Database.ExportWorldItem(table, vals);
        }
    }
}

