using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Linq;
using DFWV.WorldClasses.EntityClasses;
using DFWV.WorldClasses.HistoricalFigureClasses;

namespace DFWV.WorldClasses.HistoricalEventClasses
{
    class HE_MasterpieceArchDesign : HistoricalEvent
    {
        private int? HFID { get; set; }
        private HistoricalFigure HF { get; set; }
        private int? EntityID { get; set; }
        private Entity Entity { get; set; }
        private int? SiteID { get; set; }
        private Site Site { get; set; }
        private int? SkillAtTime { get; set; }

        override public Point Location => Site.Location;

        public override IEnumerable<HistoricalFigure> HFsInvolved
        {
            get { yield return HF; }
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
                int.TryParse(val, out valI);

                switch (element.Name.LocalName)
                {
                    case "id":
                    case "type":
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
            EventLabel(frm, parent, ref location, "HF:", HF);
            EventLabel(frm, parent, ref location, "Entity:", Entity);
            EventLabel(frm, parent, ref location, "Site:", Site);
            EventLabel(frm, parent, ref location, "Skill:", SkillAtTime.ToString());
        }

        protected override string LegendsDescription()
        {
            var timestring = base.LegendsDescription();

            return $"{timestring} {HF} designed a masterful {"UNKNOWN"} for {Entity} at {Site.AltName}.";
        }

        internal override string ToTimelineString()
        {
            var timelinestring = base.ToTimelineString();

            return $"{timelinestring} {HF} designed a masterful arch for {Entity} at {Site.AltName}.";
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
                SkillAtTime
            };

            Database.ExportWorldItem(table, vals);
        }
    }
}

