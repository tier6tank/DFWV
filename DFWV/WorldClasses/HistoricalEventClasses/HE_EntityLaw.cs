using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Linq;
using DFWV.WorldClasses.EntityClasses;
using DFWV.WorldClasses.HistoricalFigureClasses;

//TODO: Missing agreement details

namespace DFWV.WorldClasses.HistoricalEventClasses
{
    public class HE_EntityLaw : HistoricalEvent
    {
        private int? HistFigureID { get; set; }
        private HistoricalFigure HistFigure { get; set; }
        private int? EntityID { get; set; }
        private Entity Entity { get; set; }
        private string LawAdd { get; set; }
        private string LawRemove { get; set; }


        override public Point Location { get { return Entity.Location; } }

        public override IEnumerable<HistoricalFigure> HFsInvolved
        {
            get { yield return HistFigure; }
        }
        public override IEnumerable<Entity> EntitiesInvolved
        {
            get { yield return Entity; }
        }

        public HE_EntityLaw(XDocument xdoc, World world)
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
            if (HistFigureID.HasValue && World.HistoricalFigures.ContainsKey(HistFigureID.Value))
                HistFigure = World.HistoricalFigures[HistFigureID.Value];
        }


        protected override void WriteDataOnParent(MainForm frm, Control parent, ref Point location)
        {
            EventLabel(frm, parent, ref location, "Entity:", Entity);
            EventLabel(frm, parent, ref location, "Hist Fig:", HistFigure);
            if (LawAdd != null)
                EventLabel(frm, parent, ref location, "Add Law:", LawAdd);
            if (LawRemove != null)
                EventLabel(frm, parent, ref location, "Remove Law:", LawRemove);
        }

        protected override string LegendsDescription()
        {
            var timestring = base.LegendsDescription();

            if (LawAdd == "harsh")
                return string.Format("{0} {1} laid a series of oppressive edicts upon {2}.",
                    timestring, HistFigure.Race, HistFigure,
                    Entity);
            if (LawRemove == "harsh")
            {
                return string.Format("{0} {1} lifted numerous  oppressive laws from {2}.", 
                    timestring, HistFigure.Race, HistFigure,
                    Entity);
            }
            return timestring;
        }

        internal override string ToTimelineString()
        {
            var timelinestring = base.ToTimelineString();

            if (LawAdd == "harsh")
                return string.Format("{0} {1} created harsh laws for {2}.",
                    timelinestring, HistFigure, Entity);
            return timelinestring;
        }

        internal override void Export(string table)
        {
            base.Export(table);

            table = GetType().Name;

            var vals = new List<object>
            {
                ID, 
                HistFigureID.DBExport(), 
                EntityID.DBExport(), 
                LawAdd.DBExport(), 
                LawRemove.DBExport()
            };

            Database.ExportWorldItem(table, vals);

        }

    }
}