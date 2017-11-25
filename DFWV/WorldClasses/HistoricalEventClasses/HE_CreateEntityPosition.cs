using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Linq;
using DFWV.WorldClasses.EntityClasses;
using DFWV.WorldClasses.HistoricalFigureClasses;

namespace DFWV.WorldClasses.HistoricalEventClasses
{
    class HE_CreateEntityPosition : HistoricalEvent
    {

        private int? HfId { get; set; }
        private HistoricalFigure Hf { get; set; }
        public int? EntityId { get; set; }
        public Entity Entity { get; set; }
        public int? EntityId_Group { get; set; }
        public Entity Entity_Group { get; set; }
        public int? Position { get; set; }
        public int? Reason { get; set; }

        public override IEnumerable<HistoricalFigure> HFsInvolved
        {
            get { yield return Hf; }
        }
        public override IEnumerable<Entity> EntitiesInvolved
        {
            get
            {
                yield return Entity;
                yield return Entity_Group;
            }
        }

        public HE_CreateEntityPosition(XDocument xdoc, World world)
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
                    case "histfig":
                        HfId = valI;
                        break;
                    case "civ":
                        EntityId = valI;
                        break;
                    case "group":
                    case "site_civ":
                        EntityId_Group = valI;
                        break;
                    case "position":
                        if (valI != -1)
                        {
                            if (!HFEntityLink.Positions.Contains(val))
                                HFEntityLink.Positions.Add(val);
                            Position = HFEntityLink.Positions.IndexOf(val);
                        }
                        break;
                    case "reason":
                        Reason = valI;
                        break;
                    default:
                        DFXMLParser.UnexpectedXmlElement(xdoc.Root.Name.LocalName + "\t" + Types[Type], element, xdoc.Root.ToString());
                        break;
                }
            }
        }

        protected override void WriteDataOnParent(MainForm frm, Control parent, ref Point location)
        {
            //TODO: Incorporate new data
            base.WriteDataOnParent(frm, parent, ref location);

            if (Entity != null)
                EventLabel(frm, parent, ref location, "Entity:", Entity);
            if (Entity_Group != null)
                EventLabel(frm, parent, ref location, "Group:",  Entity_Group);
            if (Hf != null)
                EventLabel(frm, parent, ref location, "HF:", Hf);
            if (Position.HasValue)
                EventLabel(frm, parent, ref location, "Position:", HFEntityLink.Positions[Position.Value]);
        }

        protected override string LegendsDescription()
        {
            var timestring = base.LegendsDescription();

            var positionText = "a position";
            if (Position.HasValue)
                positionText = "the position of " + HFEntityLink.Positions[Position.Value];

            if (Reason.HasValue)
            {
                switch (Reason)
                {
                    case 0:
                        return $"{timestring} {Hf} of {Entity} created {positionText} through force of argument.";
                    case 1:
                        return
                            $"{timestring} {Hf} of {Entity} compelled the creation of {positionText} with threats of violence.";
                    case 2:
                        if (Hf == null)
                        {
                            return $"{timestring} members of {Entity} created {positionText}.";
                        }
                        return "";
                    case 3:
                        return $"{timestring} {Hf} of {Entity} created {positionText}, pushed by a wave of popular support.";
                    case 4:
                        return $"{timestring} {Hf} of {Entity} created {positionText} as a matter of course.";
                    default:
                        if (Hf == null)
                        {
                            return $"{timestring} members of {Entity} created {positionText} for UNKNOWN reason.";
                        }
                        return $"{timestring} {Hf} of {Entity} created {positionText} for UNKNOWN reason.";

                }
            }
            else
                return $"{timestring} {Hf} of {Entity} created {positionText} for UNKNOWN reason.";
        }

        internal override string ToTimelineString()
        {
            //TODO: Incorporate new data
            return base.ToTimelineString();
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
                EntityId_Group.DBExport(),
                Position.DBExport(),
                Reason.DBExport()
            };

            Database.ExportWorldItem(table, vals);
        }

    }
}


