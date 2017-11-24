using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Linq;
using DFWV.WorldClasses.EntityClasses;
using DFWV.WorldClasses.HistoricalFigureClasses;

namespace DFWV.WorldClasses.HistoricalEventClasses
{
    class HE_MasterpieceItem : HistoricalEvent
    {
        private int? Hfid { get; }
        private HistoricalFigure Hf { get; set; }
        private int? EntityId { get; }
        private Entity Entity { get; set; }
        private int? SiteId { get; }
        private Site Site { get; set; }
        private int? SkillAtTime { get; }

        private int? ItemID { get; set; }
        private int? ItemType { get; set; }
        private int? ItemSubType { get; set; }
        private int? Mat { get; set; }
        private int? MatType { get; set; }
        private int? MatIndex  { get; set; }

        override public Point Location => Site?.Location ?? Point.Empty;

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


        public HE_MasterpieceItem(XDocument xdoc, World world)
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
                    case "item_id":
                        ItemID = valI;
                        break;
                    case "item_type":
                        if (!Item.ItemTypes.Contains(val))
                            Item.ItemTypes.Add(val);
                        ItemType = Item.ItemTypes.IndexOf(val);
                        break;
                    case "item_subtype":
                        if (val != "-1")
                        {
                            if (!Item.ItemSubTypes.Contains(val))
                                Item.ItemSubTypes.Add(val);
                            ItemSubType = Item.ItemSubTypes.IndexOf(val);
                        }
                        break;
                    case "mat_type":
                        MatType = valI;
                        break;
                    case "mat_index":
                        MatIndex = valI;
                        break;
                    case "mat":
                        if (!Item.Materials.Contains(val))
                            Item.Materials.Add(val);
                        Mat = Item.Materials.IndexOf(val);
                        break;
                    case "maker":
                    case "maker_entity":
                    case "site":
                    case "skill_used":
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

        protected override string LegendsDescription() //Matched
        {
            var timestring = base.LegendsDescription();


            if (ItemType.HasValue && Mat.HasValue)
            {
                if (ItemSubType.HasValue && Item.ItemSubTypes[ItemSubType.Value] != "-1")
                    return
                        $"{timestring} {Hf} created a masterful {Item.Materials[Mat.Value]} {Item.ItemSubTypes[ItemSubType.Value]} for {Entity} at {Site.AltName}.";

                return
                    $"{timestring} {Hf} created a masterful {Item.Materials[Mat.Value]} {Item.ItemTypes[ItemType.Value]} for {Entity} at {Site.AltName}.";
            }

            return $"{timestring} {Hf} created a masterful {"UNKNOWN"} for {Entity} at {Site.AltName}.";
        }

        internal override string ToTimelineString()
        {
            //TODO: Incorporate new data
            var timelinestring = base.ToTimelineString();

            return $"{timelinestring} {Hf} created a masterful item for {Entity} at {Site.AltName}.";
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
                ItemID.DBExport(),
                ItemType.DBExport(Item.ItemTypes),
                ItemSubType.DBExport(Item.ItemSubTypes),
                Mat.DBExport(Item.Materials)
            };

            Database.ExportWorldItem(table, vals);
        }
    }
}