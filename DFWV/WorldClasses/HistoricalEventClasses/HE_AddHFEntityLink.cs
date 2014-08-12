using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Linq;
using DFWV.WorldClasses.EntityClasses;
using DFWV.WorldClasses.HistoricalFigureClasses;

namespace DFWV.WorldClasses.HistoricalEventClasses
{
    class HE_AddHFEntityLink : HistoricalEvent
    {
        private int? CivID { get; set; }
        public Entity Civ { get; set; }
        private int? HFID { get; set; }
        public HistoricalFigure HF { private get; set; }
        public int? LinkType { private get; set; }
        public int? Position { private get; set; }

        public HFEntityLink HFEntityLink { get; set; }

        override public Point Location { get { return Civ.Location; } }

        public HE_AddHFEntityLink(XDocument xdoc, World world)
            : base(xdoc, world)
        {
            foreach (var element in xdoc.Root.Elements())
            {
                var val = element.Value;
                int valI;
                Int32.TryParse(val, out valI);

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
            base.Link();
            if (CivID.HasValue && World.Entities.ContainsKey(CivID.Value))
                Civ = World.Entities[CivID.Value];
            if (HFID.HasValue && World.HistoricalFigures.ContainsKey(HFID.Value))
                HF = World.HistoricalFigures[HFID.Value];
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
                            entityLink.AddEvent = this;
                            HFEntityLink = entityLink;
                            matched = true;
                            break;
                        }
                    }
                    if (matched)
                        break;
                }
            }

            if (Civ != null)
            {
                if (Civ.Events == null)
                    Civ.Events = new List<HistoricalEvent>();
                Civ.Events.Add(this);
            }
            if (HF != null)
            {
                if (HF.Events == null)
                    HF.Events = new List<HistoricalEvent>();
                HF.Events.Add(this);
            }
        }

        internal override void Plus(XDocument xdoc)
        {
            foreach (var element in xdoc.Root.Elements())
            {
                var val = element.Value;
                int valI;
                Int32.TryParse(val, out valI);

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
                return string.Format("{0} {1} became a {2} of {3}.",
                                timestring, HF == null ? "UNKNOWN" : HF.ToString(),
                                LinkType.HasValue ? HFEntityLink.LinkTypes[LinkType.Value] : "UNKNOWN", Civ);
            if (HF == null)
                return string.Format("{0} {1} became the {2} of {3}.",
                    timestring, "UNKNOWN",
                    HFEntityLink.Positions[Position.Value], Civ);
            
            return string.Format("{0} the {1} {2} became the {3} of {4}.",
                timestring, HF.Race.ToString().ToLower(), HF,
                HFEntityLink.Positions[Position.Value], Civ);
        }

        internal override string ToTimelineString()
        {
            var timelinestring = base.ToTimelineString();

            if (HF != null && LinkType.HasValue)
                return string.Format("{0} {1} became {2} of {3}.",
                            timelinestring, HF,
                                HFSiteLink.LinkTypes[LinkType.Value], Civ);
            return string.Format("{0} Added HF Link to {1}.",
                timelinestring, Civ);
        }


        internal override void Export(string table)
        {
            //TODO: Incorporate new data
            base.Export(table);


            table = GetType().Name;
            var vals = new List<object> { ID, HF, CivID, LinkType};


            Database.ExportWorldItem(table, vals);

        }


    }
}
