using System;
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
        private int? HFID { get; set; }
        private HistoricalFigure HF { get; set; }
        private int? EntityID { get; set; }
        private Entity Entity { get; set; }
        private int? SiteID { get; set; }
        private Site Site { get; set; }
        private int? SkillAtTime { get; set; }

        private int? ItemType { get; set; }
        public int? ItemSubType { get; set; }
        public int? MatType { get; set; }
        public int? MatIndex { get; set; }
        public int? Mat { get; set; }
        public int? ImprovementType { get; set; }
        public int? ImprovementSubType { get; set; }
        public int? ImprovementMat { get; set; }
        public int? ArtID { get; set; }
        public int? ArtSubID { get; set; }

        public static List<string> ImprovementTypes = new List<string>();

        override public Point Location { get { return Site.Location; } }
        public override IEnumerable<HistoricalFigure> HFsInvolved
        {
            get { yield return HF; }
        }
        public override IEnumerable<Entity> EntitiesInvolved
        {
            get { yield return Entity; }
        }

        public HE_MasterpieceItemImprovement(XDocument xdoc, World world)
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
                    case "hfid":
                        HFID = valI;
                        break;
                    case "entity_id":
                        EntityID = valI;
                        break;
                    case "site_id":
                        SiteID = valI;
                        break;
                    case "skill_at_time":
                        SkillAtTime = valI;
                        break;
                    default:
                        DFXMLParser.UnexpectedXMLElement(xdoc.Root.Name.LocalName + "\t" + Types[Type], element, xdoc.Root.ToString());
                        break;
                }
            }
        }

        internal override void Link()
        {
            //TODO: Incorporate new data
            base.Link();
            if (HFID.HasValue && World.HistoricalFigures.ContainsKey(HFID.Value))
                HF = World.HistoricalFigures[HFID.Value];
            if (SiteID.HasValue && World.Sites.ContainsKey(SiteID.Value))
                Site = World.Sites[SiteID.Value];
            if (EntityID.HasValue && World.Entities.ContainsKey(EntityID.Value))
                Entity = World.Entities[EntityID.Value];
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
                    case "maker":
                    case "maker_entity":
                    case "site":
                        break;
                    case "item_type":
                        if (!ItemTypes.Contains(val))
                            ItemTypes.Add(val);
                        ItemType = ItemTypes.IndexOf(val);
                        break;
                    case "item_subtype":
                        if (!ItemSubTypes.Contains(val))
                            ItemSubTypes.Add(val);
                        ItemSubType = ItemSubTypes.IndexOf(val);
                        break;
                    case "mat":
                        if (!Materials.Contains(val))
                            Materials.Add(val);
                        Mat = Materials.IndexOf(val);
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
                        if (!Materials.Contains(val))
                            Materials.Add(val);
                        ImprovementMat = Materials.IndexOf(val);
                        break;
                    case "art_id":
                        ArtID = valI;
                        break;
                    case "art_subid":
                        ArtSubID = valI;
                        break;
                    default:
                        DFXMLParser.UnexpectedXMLElement(xdoc.Root.Name.LocalName + "\t" + Types[Type], element, xdoc.Root.ToString());
                        break;
                }
            }
        }

        internal override void Process()
        {
            base.Process();
            if (Site != null)
                Site.isPlayerControlled = true;
            if (Entity != null)
                Entity.MakePlayer();
        }

        protected override void WriteDataOnParent(MainForm frm, Control parent, ref Point location)
        {
            //TODO: Incorporate new data
            EventLabel(frm, parent, ref location, "HF:", HF);
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
                        return string.Format("{0} {1} added masterful {2} of {3} to a {4} {5} for {6} at {7}.",
                            timestring, HF, ImprovementTypes[ImprovementType.Value], Materials[ImprovementMat.Value],
                            Materials[Mat.Value], ItemTypes[ItemType.Value], Entity,
                            Site.AltName);
                    case "rings_hanging":
                        return string.Format("{0} {1} added masterful rings in {2} to a {3} {4} for {5} at {6}.",
                            timestring, HF, Materials[ImprovementMat.Value],
                            Materials[Mat.Value], ItemTypes[ItemType.Value], Entity,
                            Site.AltName);
                    case "bands":
                        return string.Format("{0} {1} added masterful bands in {2} to a {3} {4} for {5} at {6}.",
                            timestring, HF, Materials[ImprovementMat.Value],
                            Materials[Mat.Value], ItemTypes[ItemType.Value], Entity,
                            Site.AltName);
                    case "covered":
                        return string.Format("{0} {1} added masterful covering in {2} to a {3} {4} for {5} at {6}.",
                            timestring, HF, Materials[ImprovementMat.Value],
                            Materials[Mat.Value], ItemTypes[ItemType.Value], Entity,
                            Site.AltName);
                    default:
                        return string.Format("{0} {1} added masterful {2} of {3} to a {4} {5} for {6} at {7}.",
                            timestring, HF, ImprovementTypes[ImprovementType.Value], Materials[ImprovementMat.Value],
                            Materials[Mat.Value], ItemTypes[ItemType.Value], Entity,
                            Site.AltName);
                }
            }

            return string.Format("{0} {1} added a masterful {2} for {3} at {4}.",
                                timestring, HF, "UNKNOWN", Entity,
                                Site.AltName);
        }

        internal override string ToTimelineString()
        {
            //TODO: Incorporate new data
            var timelinestring = base.ToTimelineString();

            return string.Format("{0} {1} masterfully improved an item for {2} at {3}.",
                                timelinestring, HF, Entity,
                                Site.AltName);
        }

        internal override void Export(string table)
        {
            base.Export(table);

            table = GetType().Name;

            var vals = new List<object>
            {
                ID, 
                HFID.DBExport(), 
                EntityID.DBExport(), 
                SiteID.DBExport(), 
                SkillAtTime,
                ItemType.DBExport(ItemTypes),
                ItemSubType.DBExport(ItemSubTypes),
                Mat.DBExport(Materials),
                ImprovementType.DBExport(ImprovementTypes),
                //ImprovementSubType.DBExport(), //TODO: Uncomment this
                ImprovementMat.DBExport(Materials),
                ArtID.DBExport(),
                ArtSubID.DBExport()
            };

            Database.ExportWorldItem(table, vals);
        }
    }
}

