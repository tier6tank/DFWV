using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;
using DFWV.WorldClasses.HistoricalFigureClasses;

namespace DFWV.WorldClasses.HistoricalEventClasses
{
    class HE_MasterpieceDye : HistoricalEvent
    {
        public int? HFID { get; set; }
        public HistoricalFigure HF { get; set; }
        public int? EntityID { get; set; }
        public Entity Entity { get; set; }
        public int? SiteID { get; set; }
        public Site Site { get; set; }
        public int? SkillAtTime { get; set; }

        override public Point Location { get { return Site.Location; } }

        public HE_MasterpieceDye(XDocument xdoc, World world)
            : base(xdoc, world)
        {
            foreach (XElement element in xdoc.Root.Elements())
            {
                string val = element.Value.ToString();
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
                        DFXMLParser.UnexpectedXMLElement(xdoc.Root.Name.LocalName + "\t" + HistoricalEvent.Types[Type], element, xdoc.Root.ToString());
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
            if (Entity != null)
            {
                if (Entity.Events == null)
                    Entity.Events = new List<HistoricalEvent>();
                Entity.Events.Add(this);
            }
        }

        public override void WriteDataOnParent(MainForm frm, Control parent, ref Point location)
        {
            EventLabel(frm, parent, ref location, "HF:", HF);
            EventLabel(frm, parent, ref location, "Entity:", Entity);
            EventLabel(frm, parent, ref location, "Site:", Site);
            EventLabel(frm, parent, ref location, "Skill:", SkillAtTime.ToString());
        }

        public override string LegendsDescription()
        {
            string timestring = base.LegendsDescription();

            return string.Format("{0} {1} created a masterful {2} for {3} at {4}.",
                                timestring, HF.ToString(), "UNKNOWN", Entity.ToString(),
                                Site.AltName);
        }

        internal override string ToTimelineString()
        {
            string timelinestring = base.ToTimelineString();

            return string.Format("{0} {1} created a masterful dye for {2} at {3}.",
                                timelinestring, HF.ToString(), Entity.ToString(),
                                Site.AltName); ;
        }

        internal override void Export(string table)
        {
            base.Export(table);

            
            List<object> vals;
            table = this.GetType().Name.ToString();



            vals = new List<object>() { ID, HFID, EntityID, SiteID, SkillAtTime };


            Database.ExportWorldItem(table, vals);

        }

    }
}

