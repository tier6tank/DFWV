using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Linq;
using DFWV.WorldClasses.EntityClasses;
using DFWV.WorldClasses.HistoricalFigureClasses;

namespace DFWV.WorldClasses.HistoricalEventClasses
{
    class HeCreateEntityPosition : HistoricalEvent
    {

        private int? Hfid { get; set; }
        private HistoricalFigure Hf { get; set; }
        public int? CivId { get; set; }
        public Entity Civ { get; set; }
        public int? GroupId { get; set; }
        public Entity Group { get; set; }
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
                yield return Civ;
                yield return Group;
            }
        }

        public HeCreateEntityPosition(XDocument xdoc, World world)
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
                        DfxmlParser.UnexpectedXmlElement(xdoc.Root.Name.LocalName + "\t" + Types[Type], element, xdoc.Root.ToString());
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
                        Hfid = valI;
                        break;
                    case "civ":
                        CivId = valI;
                        break;
                    case "group":
                    case "site_civ":
                        GroupId = valI;
                        break;
                    case "position":
                        if (valI != -1)
                        {
                            if (!HfEntityLink.Positions.Contains(val))
                                HfEntityLink.Positions.Add(val);
                            Position = HfEntityLink.Positions.IndexOf(val);
                        }
                        break;
                    case "reason":
                        Reason = valI;
                        break;
                    default:
                        DfxmlParser.UnexpectedXmlElement(xdoc.Root.Name.LocalName + "\t" + Types[Type], element, xdoc.Root.ToString());
                        break;
                }
            }
        }

        internal override void Link()
        {
            //TODO: Add Group Item
            base.Link();
            if (CivId.HasValue && World.Entities.ContainsKey(CivId.Value))
                Civ = World.Entities[CivId.Value];
            if (GroupId.HasValue && World.Entities.ContainsKey(GroupId.Value) && CivId.HasValue && GroupId.Value != CivId.Value)
                Group = World.Entities[GroupId.Value];
            if (Hfid.HasValue && World.HistoricalFigures.ContainsKey(Hfid.Value))
                Hf = World.HistoricalFigures[Hfid.Value];
       
        }

        protected override void WriteDataOnParent(MainForm frm, Control parent, ref Point location)
        {
            //TODO: Incorporate new data
            base.WriteDataOnParent(frm, parent, ref location);

            if (Civ != null)
                EventLabel(frm, parent, ref location, "Entity:", Civ);
            if (Group != null)
                EventLabel(frm, parent, ref location, "Group:",  Group);
            if (Hf != null)
                EventLabel(frm, parent, ref location, "HF:", Hf);
            if (Position.HasValue)
                EventLabel(frm, parent, ref location, "Position:", HfEntityLink.Positions[Position.Value]);
        }

        protected override string LegendsDescription() //Matched
        {
            var timestring = base.LegendsDescription();

            var positionText = "a position";
            if (Position.HasValue)
                positionText = "the position of " + HfEntityLink.Positions[Position.Value];
                

            switch (Reason)
            {
                case 0:
                    return $"{timestring} {Hf} of {Civ} created {positionText} through force of argument.";
                case 1:
                    return
                        $"{timestring} {Hf} of {Civ} compelled the creation of {positionText} with threats of violence.";
                case 2:
                    if (Hf == null)
                    {
                        return $"{timestring} members of {Civ} created {positionText}.";
                    }
                    return "";
                case 3:
                    return $"{timestring} {Hf} of {Civ} created {positionText}, pushed by a wave of popular support.";
                case 4:
                    return $"{timestring} {Hf} of {Civ} created {positionText} as a matter of course.";
                default:
                    if (Hf == null)
                    {
                        return $"{timestring} members of {Civ} created {positionText} for UNKNOWN reason.";
                    }
                    return $"{timestring} {Hf} of {Civ} created {positionText} for UNKNOWN reason.";

            }
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
                Hfid.DBExport(),
                CivId.DBExport(),
                GroupId.DBExport(),
                Position.DBExport(),
                Reason.DBExport()
            };

            Database.ExportWorldItem(table, vals);
        }

    }
}


