using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;
using DFWV.WorldClasses.EntityClasses;
using DFWV.WorldClasses.HistoricalFigureClasses;

namespace DFWV.WorldClasses.HistoricalEventClasses
{
    public class HE_AddHFEntityLink : HistoricalEvent
    {
        private int? EntityId { get; }
        public Entity Entity { get; set; }
        private int? HfId { get; set; }
        public HistoricalFigure Hf { private get; set; }
        public int? LinkType { private get; set; }
        public int? Position { private get; set; }

        public HFEntityLink HfEntityLink { get; set; }

        override public Point Location => Entity.Location;

        public override IEnumerable<HistoricalFigure> HFsInvolved
        {
            get { yield return Hf; }
        }

        public override IEnumerable<Entity> EntitiesInvolved
        {
            get { yield return Entity; }
        }

        public HE_AddHFEntityLink(XDocument xdoc, World world)
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
                        EntityId = valI;
                        break;
                    default:
                        DFXMLParser.UnexpectedXmlElement(xdoc.Root.Name.LocalName + "\t" + Types[Type], element, xdoc.Root.ToString());
                        break;
                }
            }

        }

        internal override void Process()
        {
            base.Process();
            var matched = false;

            if (Hf?.EntityLinks == null) return;
            foreach (var entityLinkList in Hf.EntityLinks)
            {
                foreach (var entityLink in entityLinkList.Value.Where(entityLink => entityLink.Entity == Entity))
                {
                    entityLink.AddEvent = this;
                    HfEntityLink = entityLink;
                    matched = true;
                    break;
                }
                if (matched)
                    break;
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
                        DFXMLParser.UnexpectedXmlElement(xdoc.Root.Name.LocalName + "\t" + Types[Type], element, xdoc.Root.ToString());
                        break;
                }
            }
        }

        protected override void WriteDataOnParent(MainForm frm, Control parent, ref Point location)
        {
            EventLabel(frm, parent, ref location, "HF:", Hf);
            EventLabel(frm, parent, ref location, "Entity:", Entity);
            if (LinkType.HasValue)
            {
                EventLabel(frm, parent, ref location, "Link Type:", HFEntityLink.LinkTypes[LinkType.Value]);
                if (HFEntityLink.LinkTypes[LinkType.Value] == "position" && Position != null)
                    EventLabel(frm, parent, ref location, "Position:", HFEntityLink.Positions[Position.Value]);
            }
        }

        protected override string LegendsDescription()
        { 
            var timestring = base.LegendsDescription();
            if (!Position.HasValue || HFEntityLink.Positions[Position.Value] == "-1")
            {
                if (HfEntityLink != null)
                {
                    switch (HFEntityLink.LinkTypes[HfEntityLink.LinkType])
                    {
                        case "member":
                            if (Entity.Civilization.Leaders["master"].Any(x => x.Hf == Hf))
                                return $"{timestring} {Hf?.ToString() ?? "UNKNOWN"} became the master of {Entity}.";
                            return $"{timestring} {Hf?.ToString() ?? "UNKNOWN"} became a {HFEntityLink.LinkTypes[HfEntityLink.LinkType]} of {Entity}.";
                        case "prisoner":
                        case "former prisoner":
                            return $"{timestring} {Hf?.ToString() ?? "UNKNOWN"} was imprisoned by {Entity}.";
                        default:
                            return $"{timestring} {Hf?.ToString() ?? "UNKNOWN"} became a {HFEntityLink.LinkTypes[HfEntityLink.LinkType]} of {Entity}.";
                    }
                    return
                        $"{timestring} {Hf?.ToString() ?? "UNKNOWN"} became a {HFEntityLink.LinkTypes[HfEntityLink.LinkType]} of {Entity}.";
                }
                return
                    $"{timestring} {Hf?.ToString() ?? "UNKNOWN"} became a {(LinkType.HasValue ? HFEntityLink.LinkTypes[LinkType.Value] : "UNKNOWN")} of {Entity}.";
            }
            if (Hf == null)
                return $"{timestring} {"UNKNOWN"} became the {HFEntityLink.Positions[Position.Value]} of {Entity}.";
            
            return
                $"{timestring} the {Hf.Race.ToString().ToLower()} {Hf} became the {HFEntityLink.Positions[Position.Value]} of {Entity}.";
        }

        internal override string ToTimelineString()
        {
            var timelinestring = base.ToTimelineString();

            if (Hf != null && LinkType.HasValue)
                return $"{timelinestring} {Hf} became {HFEntityLink.LinkTypes[LinkType.Value]} of {Entity}.";
            return $"{timelinestring} Added HF Link to {Entity}.";
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
                LinkType.DBExport(HFEntityLink.LinkTypes),
                Position.DBExport(HFEntityLink.Positions)
            };


            Database.ExportWorldItem(table, vals);

        }


    }
}
