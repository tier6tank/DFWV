using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Linq;
using DFWV.WorldClasses.EntityClasses;
using DFWV.WorldClasses.HistoricalFigureClasses;

namespace DFWV.WorldClasses.HistoricalEventClasses
{
    class HE_MasterpieceArchConstructed : HistoricalEvent
    {
        private int? HFID { get; set; }
        private HistoricalFigure HF { get; set; }
        private int? EntityID { get; set; }
        private Entity Entity { get; set; }
        private int? SiteID { get; set; }
        private Site Site { get; set; }
        private int? SkillAtTime { get; set; }
        public int? BuildingType { get; set; }
        public int? BuildingSubType { get; set; }
        public int? BuildingCustom { get; set; }

        override public Point Location { get { return Site.Location; } }

        public override IEnumerable<HistoricalFigure> HFsInvolved
        {
            get { yield return HF; }
        }
        public override IEnumerable<Entity> EntitiesInvolved
        {
            get { yield return Entity; }
        }

        public HE_MasterpieceArchConstructed(XDocument xdoc, World world)
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
                    case "building_type":
                        if (!Buildings.Contains(val))
                            Buildings.Add(val);
                        BuildingType = Buildings.IndexOf(val);
                        break;
                    case "building_subtype":
                        if (val != "-1")
                        {
                            if (!Buildings.Contains(val))
                                Buildings.Add(val);
                            BuildingSubType = Buildings.IndexOf(val);
                        }
                        break;
                    case "building_custom":
                        if (valI != -1)
                            BuildingCustom = valI;
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
            if (BuildingType.HasValue)
                EventLabel(frm, parent, ref location, "Building Type:", Buildings[BuildingType.Value]);
        }

        protected override string LegendsDescription() //Matched
        {
            var timestring = base.LegendsDescription();

            if (BuildingType.HasValue)
                return string.Format("{0} {1} constructed a masterful {2} for {3} at {4}.",
                                    timestring, HF, Buildings[BuildingType.Value], Entity,
                                    Site.AltName);

            return string.Format("{0} {1} constructed a masterful {2} for {3} at {4}.",
                                timestring, HF, "UNKNOWN", Entity,
                                Site.AltName);
        }

        internal override string ToTimelineString()
        {
            //TODO: Incorporate new data
            var timelinestring = base.ToTimelineString();

            return string.Format("{0} {1} constructed a masterful arch for {2} at {3}.",
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
                BuildingType.DBExport(Buildings),
                BuildingSubType.DBExport(Buildings),
                BuildingCustom.DBExport()
            };

            Database.ExportWorldItem(table, vals);
        }
    }
}

