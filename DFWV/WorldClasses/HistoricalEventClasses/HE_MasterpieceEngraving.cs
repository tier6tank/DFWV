using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Linq;
using DFWV.WorldClasses.EntityClasses;
using DFWV.WorldClasses.HistoricalFigureClasses;

namespace DFWV.WorldClasses.HistoricalEventClasses
{
    class HE_MasterpieceEngraving : HistoricalEvent
    {
        private int? HFID { get; set; }
        private HistoricalFigure HF { get; set; }
        private int? EntityID { get; set; }
        private Entity Entity { get; set; }
        private int? SiteID { get; set; }
        private Site Site { get; set; }
        private int? SkillAtTime { get; set; }

        public int? Mat { get; set; }
        public int? ArtID { get; set; }
        public int? ArtSubID { get; set; }

        override public Point Location { get { return Site.Location; } }


        public HE_MasterpieceEngraving(XDocument xdoc, World world)
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
                    case "art_id":
                        ArtID = valI;
                        break;
                    case "art_subid":
                        ArtSubID = valI;
                        break;
                    case "mat":
                        if (!Materials.Contains(val))
                            Materials.Add(val);
                        Mat = Materials.IndexOf(val);
                        break;
                    case "maker":
                    case "maker_entity":
                    case "site":
                    case "skill_rating":
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
            if (HF != null)
            {
                if (HF.Events == null)
                    HF.Events = new List<HistoricalEvent>();
                HF.Events.Add(this);
            }
            if (Entity == null) return;
            if (Entity.Events == null)
                Entity.Events = new List<HistoricalEvent>();
            Entity.Events.Add(this);
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

            return string.Format("{0} {1} created a masterful engraving \"{2}\" for {3} at {4}.",
                                timestring, HF, "Art ID: " + ArtID, Entity,
                                Site.AltName);
        }

        internal override string ToTimelineString()
        {
            //TODO: Incorporate new data
            var timelinestring = base.ToTimelineString();

            return string.Format("{0} {1} created a masterful engraving for {2} at {3}.",
                                timelinestring, HF, Entity,
                                Site.AltName);
        }

        internal override void Export(string table)
        {
            //TODO: Incorporate new data
            base.Export(table);


            table = GetType().Name;



            var vals = new List<object> { ID, HFID, EntityID, SiteID, SkillAtTime };


            Database.ExportWorldItem(table, vals);

        }

    }
}

