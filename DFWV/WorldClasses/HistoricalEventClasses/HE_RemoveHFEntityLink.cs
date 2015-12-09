using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Linq;
using DFWV.WorldClasses.EntityClasses;
using DFWV.WorldClasses.HistoricalFigureClasses;

namespace DFWV.WorldClasses.HistoricalEventClasses
{
    public class HE_RemoveHFEntityLink : HistoricalEvent
    {
        private int? CivID { get; set; }
        private Entity Civ { get; set; }
        private int? HFID { get; set; }
        public HistoricalFigure HF { private get; set; }
        public int? LinkType { private get; set; }
        public int? Position { private get; set; }

        public HFEntityLink HFEntityLink { get; set; }

        override public Point Location => Civ.Location;

        public override IEnumerable<HistoricalFigure> HFsInvolved
        {
            get { yield return HF; }
        }

        public override IEnumerable<Entity> EntitiesInvolved
        {
            get { yield return Civ; }
        }

        public HE_RemoveHFEntityLink(XDocument xdoc, World world)
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
                    case "civ_id":
                        CivID = valI;
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
            if (CivID.HasValue && World.Entities.ContainsKey(CivID.Value))
                Civ = World.Entities[CivID.Value];
            if (HFID.HasValue && World.HistoricalFigures.ContainsKey(HFID.Value))
                HF = World.HistoricalFigures[HFID.Value];
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
                    case "link_type":
                        if (!HFEntityLink.LinkTypes.Contains(val))
                            HFEntityLink.LinkTypes.Add(val);
                        LinkType = HFEntityLink.LinkTypes.IndexOf(val);
                        break;
                    case "position":
                        if (valI != -1)
                        {
                            if (!HFEntityLink.Positions.Contains(val))
                                HFEntityLink.Positions.Add(val);
                            Position = HFEntityLink.Positions.IndexOf(val);
                        }
                        break;
                    case "civ":
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
            var matched = false;

            if (HF != null && HF.EntityLinks != null)
            {
                foreach (var entityLinkList in HF.EntityLinks)
                {
                    foreach (var entityLink in entityLinkList.Value)
                    {
                        if (entityLink.Entity == Civ)
                        {
                            entityLink.RemoveEvent = this;
                            HFEntityLink = entityLink;
                            matched = true;
                            break;
                        }
                    }
                    if (matched)
                        break;
                }
            }

        }

        protected override void WriteDataOnParent(MainForm frm, Control parent, ref Point location)
        {
            EventLabel(frm, parent, ref location, "HF:", HF);
            EventLabel(frm, parent, ref location, "Entity:", Civ);
            if (LinkType.HasValue)
            {
                EventLabel(frm, parent, ref location, "Link Type:", HFEntityLink.LinkTypes[LinkType.Value]);
                if (HFEntityLink.LinkTypes[LinkType.Value] == "position" && Position != null)
                    EventLabel(frm, parent, ref location, "Position:", HFEntityLink.Positions[Position.Value]);
            }
        }

        protected override string LegendsDescription() //Matched
        {
            var timestring = base.LegendsDescription();

            if (!Position.HasValue || HFEntityLink.Positions[Position.Value] == "-1")
                return $"{timestring} {(HF == null ? "UNKNOWN" : HF.ToString())} left the {Civ}.";
            if (HF == null)
                return $"{timestring} {"UNKNOWN"} became the {HFEntityLink.Positions[Position.Value]} of {Civ}.";
            return
                $"{timestring} the {HF.Race.ToString().ToLower()} {HF} ceased to be the {HFEntityLink.Positions[Position.Value]} of {Civ}.";
        }

        internal override string ToTimelineString()
        {
            //TODO: Incorporate new data
            var timelinestring = base.ToTimelineString();

            return $"{timelinestring} Remove HF Link from {Civ}";
        }

        internal override void Export(string table)
        {
            base.Export(table);

            table = GetType().Name;

            var vals = new List<object>
            {
                ID, 
                CivID.DBExport(),
                HFID.DBExport(),
                LinkType.DBExport(HFEntityLink.LinkTypes),
                Position.DBExport(HFEntityLink.Positions)
            };

            Database.ExportWorldItem(table, vals);
        }
    }
}


