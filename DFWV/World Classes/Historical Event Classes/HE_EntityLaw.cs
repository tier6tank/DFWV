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
    class HE_EntityLaw : HistoricalEvent
    {
        public int? HistFigureID { get; set; }
        public HistoricalFigure HistFigure { get; set; }
        public int? EntityID { get; set; }
        public Entity Entity { get; set; }
        public string LawAdd { get; set; }
        public string LawRemove { get; set; }


        override public Point Location { get { return Entity.Location; } }

        public HE_EntityLaw(XDocument xdoc, World world)
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
                    case "entity_id":
                        EntityID = valI;
                        break;
                    case "hist_figure_id":
                        HistFigureID = valI;
                        break;
                    case "law_add":
                        LawAdd = val;
                        break;
                    case "law_remove":
                        LawRemove = val;
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
            if (EntityID.HasValue && World.Entities.ContainsKey(EntityID.Value))
                Entity = World.Entities[EntityID.Value];
            if (HistFigureID.HasValue && World.HistoricalFigures.ContainsKey(HistFigureID.Value))
                HistFigure = World.HistoricalFigures[HistFigureID.Value];
        }


        internal override void Process()
        {
            base.Process();
            if (HistFigure != null)
            {
                if (HistFigure.Events == null)
                    HistFigure.Events = new List<HistoricalEvent>();
                HistFigure.Events.Add(this);
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
            EventLabel(frm, parent, ref location, "Entity:", Entity);
            EventLabel(frm, parent, ref location, "Hist Fig:", HistFigure);
            if (LawAdd != null)
                EventLabel(frm, parent, ref location, "Add Law:", LawAdd);
            if (LawRemove != null)
                EventLabel(frm, parent, ref location, "Remove Law:", LawRemove);
        }

        public override string LegendsDescription()
        {
            string timestring = base.LegendsDescription();

            if (LawAdd == "harsh")
                return string.Format("{0} the {1} {2} laid a series of oppressive edicts upon {3}.",
                    timestring, HistFigure.Race.ToString(), HistFigure.ToString(),
                    Entity.ToString());
            else
                return timestring;
        }

        internal override string ToTimelineString()
        {
            string timelinestring = base.ToTimelineString();

            if (LawAdd == "harsh")
                return string.Format("{0} {1} created harsh laws for {2}.",
                    timelinestring, HistFigure.ToString(), Entity.ToString());
            else
                return timelinestring;
        }

        internal override void Export(string table)
        {
            base.Export(table);

            
            List<object> vals;
            table = this.GetType().Name.ToString();


            
            vals = new List<object>() { ID, HistFigureID, EntityID, LawAdd, LawRemove };


            Database.ExportWorldItem(table, vals);

        }

    }
}