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
        private int? HistFigureId { get; }
        private HistoricalFigure HistFigure { get; set; }
        private int? EntityId { get; }
        private Entity Entity { get; set; }
        private string LawAdd { get; }
        private string LawRemove { get; }


        override public Point Location => Entity.Location;

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
                        EntityId = valI;
                        break;
                    case "hist_figure_id":
                        HistFigureId = valI;
                        break;
                    case "law_add":
                        LawAdd = val;
                        break;
                    case "law_remove":
                        LawRemove = val;
                        break;

                    default:
                        DFXMLParser.UnexpectedXmlElement(xdoc.Root.Name.LocalName + "\t" + Types[Type], element, xdoc.Root.ToString());
                        break;
                }
            }
        }
        internal override void Link()
        {
            base.Link();
            if (EntityId.HasValue && World.Entities.ContainsKey(EntityId.Value))
                Entity = World.Entities[EntityId.Value];
            if (HistFigureId.HasValue && World.HistoricalFigures.ContainsKey(HistFigureId.Value))
                HistFigure = World.HistoricalFigures[HistFigureId.Value];
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
                return $"{timestring} {HistFigure.Race} {HistFigure} laid a series of oppressive edicts upon {Entity}.";
            return LawRemove == "harsh" ? 
                $"{timestring} {HistFigure.Race} {HistFigure} lifted numerous  oppressive laws from {Entity}." : 
                timestring;
        }

        internal override string ToTimelineString()
        {
            var timelinestring = base.ToTimelineString();

            return LawAdd == "harsh" ? 
                $"{timelinestring} {HistFigure} created harsh laws for {Entity}." : 
                timelinestring;
        }

        internal override void Export(string table)
        {
            base.Export(table);

            table = GetType().Name;

            var vals = new List<object>
            {
                Id, 
                HistFigureId.DBExport(), 
                EntityId.DBExport(), 
                LawAdd.DBExport(), 
                LawRemove.DBExport()
            };

            Database.ExportWorldItem(table, vals);

        }

    }
}