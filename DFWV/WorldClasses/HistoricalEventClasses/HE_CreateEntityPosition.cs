using System;
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

        private int? HFID { get; set; }
        private HistoricalFigure HF { get; set; }
        public int? CivID { get; set; }
        public Entity Civ { get; set; }
        public int? GroupID { get; set; }
        public Entity Group { get; set; }
        public int? Position { get; set; }
        public int? Reason { get; set; }

        public override IEnumerable<HistoricalFigure> HFsInvolved
        {
            get { yield return HF; }
        }
        public override IEnumerable<Entity> EntitiesInvolved
        {
            get
            {
                yield return Civ;
                yield return Group;
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
                        DFXMLParser.UnexpectedXMLElement(xdoc.Root.Name.LocalName + "\t" + Types[Type], element, xdoc.Root.ToString());
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
                        HFID = valI;
                        break;
                    case "civ":
                        CivID = valI;
                        break;
                    case "group":
                    case "site_civ":
                        GroupID = valI;
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
                        DFXMLParser.UnexpectedXMLElement(xdoc.Root.Name.LocalName + "\t" + Types[Type], element, xdoc.Root.ToString());
                        break;
                }
            }
        }

        internal override void Link()
        {
            //TODO: Add Group Item
            base.Link();
            if (CivID.HasValue && World.Entities.ContainsKey(CivID.Value))
                Civ = World.Entities[CivID.Value];
            if (GroupID.HasValue && World.Entities.ContainsKey(GroupID.Value) && CivID.HasValue && GroupID.Value != CivID.Value)
                Group = World.Entities[GroupID.Value];
            if (HFID.HasValue && World.HistoricalFigures.ContainsKey(HFID.Value))
                HF = World.HistoricalFigures[HFID.Value];
       
        }

        protected override void WriteDataOnParent(MainForm frm, Control parent, ref Point location)
        {
            //TODO: Incorporate new data
            base.WriteDataOnParent(frm, parent, ref location);

            if (Civ != null)
                EventLabel(frm, parent, ref location, "Entity:", Civ);
            if (Group != null)
                EventLabel(frm, parent, ref location, "Group:",  Group);
            if (HF != null)
                EventLabel(frm, parent, ref location, "HF:", HF);
            if (Position.HasValue)
                EventLabel(frm, parent, ref location, "Position:", HFEntityLink.Positions[Position.Value]);
        }

        protected override string LegendsDescription() //Matched
        {
            var timestring = base.LegendsDescription();

            var positionText = "a position";
            if (Position.HasValue)
                positionText = "the position of " + HFEntityLink.Positions[Position.Value];
                

            switch (Reason)
            {
                case 0:
                    return $"{timestring} {HF} of {Civ} created {positionText} through force of argument.";
                case 1:
                    return
                        $"{timestring} {HF} of {Civ} compelled the creation of {positionText} with threats of violence.";
                case 2:
                    if (HF == null)
                    {
                        return $"{timestring} members of {Civ} created {positionText}.";
                    }
                    return "";
                case 3:
                    return $"{timestring} {HF} of {Civ} created {positionText}, pushed by a wave of popular support.";
                case 4:
                    return $"{timestring} {HF} of {Civ} created {positionText} as a matter of course.";
                default:
                    if (HF == null)
                    {
                        return $"{timestring} members of {Civ} created {positionText} for UNKNOWN reason.";
                    }
                    return $"{timestring} {HF} of {Civ} created {positionText} for UNKNOWN reason.";

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
                ID,
                HFID.DBExport(),
                CivID.DBExport(),
                GroupID.DBExport(),
                Position.DBExport(),
                Reason.DBExport()
            };

            Database.ExportWorldItem(table, vals);
        }

    }
}


