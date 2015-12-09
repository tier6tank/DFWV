using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Linq;
using DFWV.WorldClasses.EntityClasses;
using DFWV.WorldClasses.HistoricalFigureClasses;

namespace DFWV.WorldClasses.HistoricalEventClasses
{
    public class HeRemoveHfEntityLink : HistoricalEvent
    {
        private int? CivId { get; }
        private Entity Civ { get; set; }
        private int? Hfid { get; set; }
        public HistoricalFigure Hf { private get; set; }
        public int? LinkType { private get; set; }
        public int? Position { private get; set; }

        public HfEntityLink HfEntityLink { get; set; }

        override public Point Location => Civ.Location;

        public override IEnumerable<HistoricalFigure> HFsInvolved
        {
            get { yield return Hf; }
        }

        public override IEnumerable<Entity> EntitiesInvolved
        {
            get { yield return Civ; }
        }

        public HeRemoveHfEntityLink(XDocument xdoc, World world)
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
                        CivId = valI;
                        break;
                    default:
                        DfxmlParser.UnexpectedXmlElement(xdoc.Root.Name.LocalName + "\t" + Types[Type], element, xdoc.Root.ToString());
                        break;
                }
            }
        }

        internal override void Link()
        {
            //TODO: Incorporate new data
            base.Link();
            if (CivId.HasValue && World.Entities.ContainsKey(CivId.Value))
                Civ = World.Entities[CivId.Value];
            if (Hfid.HasValue && World.HistoricalFigures.ContainsKey(Hfid.Value))
                Hf = World.HistoricalFigures[Hfid.Value];
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
                    case "link_type":
                        if (!HfEntityLink.LinkTypes.Contains(val))
                            HfEntityLink.LinkTypes.Add(val);
                        LinkType = HfEntityLink.LinkTypes.IndexOf(val);
                        break;
                    case "position":
                        if (valI != -1)
                        {
                            if (!HfEntityLink.Positions.Contains(val))
                                HfEntityLink.Positions.Add(val);
                            Position = HfEntityLink.Positions.IndexOf(val);
                        }
                        break;
                    case "civ":
                        break;
                    default:
                        DfxmlParser.UnexpectedXmlElement(xdoc.Root.Name.LocalName + "\t" + Types[Type], element, xdoc.Root.ToString());
                        break;
                }
            }
        }

        internal override void Process()
        {
            base.Process();
            var matched = false;

            if (Hf?.EntityLinks != null)
            {
                foreach (var entityLinkList in Hf.EntityLinks)
                {
                    foreach (var entityLink in entityLinkList.Value)
                    {
                        if (entityLink.Entity == Civ)
                        {
                            entityLink.RemoveEvent = this;
                            HfEntityLink = entityLink;
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
            EventLabel(frm, parent, ref location, "HF:", Hf);
            EventLabel(frm, parent, ref location, "Entity:", Civ);
            if (LinkType.HasValue)
            {
                EventLabel(frm, parent, ref location, "Link Type:", HfEntityLink.LinkTypes[LinkType.Value]);
                if (HfEntityLink.LinkTypes[LinkType.Value] == "position" && Position != null)
                    EventLabel(frm, parent, ref location, "Position:", HfEntityLink.Positions[Position.Value]);
            }
        }

        protected override string LegendsDescription() //Matched
        {
            var timestring = base.LegendsDescription();

            if (!Position.HasValue || HfEntityLink.Positions[Position.Value] == "-1")
                return $"{timestring} {Hf?.ToString() ?? "UNKNOWN"} left the {Civ}.";
            if (Hf == null)
                return $"{timestring} {"UNKNOWN"} became the {HfEntityLink.Positions[Position.Value]} of {Civ}.";
            return
                $"{timestring} the {Hf.Race.ToString().ToLower()} {Hf} ceased to be the {HfEntityLink.Positions[Position.Value]} of {Civ}.";
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
                Id, 
                CivId.DBExport(),
                Hfid.DBExport(),
                LinkType.DBExport(HfEntityLink.LinkTypes),
                Position.DBExport(HfEntityLink.Positions)
            };

            Database.ExportWorldItem(table, vals);
        }
    }
}


