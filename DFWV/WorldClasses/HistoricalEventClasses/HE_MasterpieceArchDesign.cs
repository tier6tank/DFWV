﻿using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Linq;
using DFWV.WorldClasses.EntityClasses;
using DFWV.WorldClasses.HistoricalFigureClasses;

namespace DFWV.WorldClasses.HistoricalEventClasses
{
    class HE_MasterpieceArchDesign : HistoricalEvent
    {
        private int? HfId { get; }
        private HistoricalFigure Hf { get; set; }
        private int? EntityId { get; }
        private Entity Entity { get; set; }
        private int? SiteId { get; }
        private Site Site { get; set; }
        private int? SkillAtTime { get; }
        public int? BuildingType { get; set; }
        public int? BuildingSubType { get; set; }
        public int? BuildingCustom { get; set; }

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


        public HE_MasterpieceArchDesign(XDocument xdoc, World world)
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
                        HfId = valI;
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
                    case "maker":
                    case "maker_entity":
                    case "site":
                        break;
                    default:
                        DFXMLParser.UnexpectedXmlElement(xdoc.Root.Name.LocalName + "\t" + Types[Type], element, xdoc.Root.ToString());
                        break;
                }
            }
        }

        public override void Process()
        {
            base.Process();
            if (Site != null)
                Site.IsPlayerControlled = true;
            Entity?.MakePlayer();
        }

        protected override void WriteDataOnParent(MainForm frm, Control parent, ref Point location)
        {
            EventLabel(frm, parent, ref location, "HF:", Hf);
            EventLabel(frm, parent, ref location, "Entity:", Entity);
            EventLabel(frm, parent, ref location, "Site:", Site);
            EventLabel(frm, parent, ref location, "Skill:", SkillAtTime.ToString());
        }

        protected override string LegendsDescription()
        {
            var timestring = base.LegendsDescription();

            return $"{timestring} {Hf} designed a masterful {"UNKNOWN"} for {Entity} at {Site.AltName}.";
        }

        internal override string ToTimelineString()
        {
            var timelinestring = base.ToTimelineString();

            return $"{timelinestring} {Hf} designed a masterful arch for {Entity} at {Site.AltName}.";
        }

        internal override void Export(string table)
        {
            base.Export(table);

            table = GetType().Name;

            var vals = new List<object>
            {
                Id, 
                HfId.DBExport(), 
                EntityId.DBExport(),
                SiteId.DBExport(), 
                SkillAtTime
            };

            Database.ExportWorldItem(table, vals);
        }
    }
}

