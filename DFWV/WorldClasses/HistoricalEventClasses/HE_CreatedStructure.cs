using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Linq;
using DFWV.WorldClasses.EntityClasses;
using DFWV.WorldClasses.HistoricalFigureClasses;

namespace DFWV.WorldClasses.HistoricalEventClasses
{
    class HE_CreatedStructure : HistoricalEvent
    {
        private int? SiteID { get; set; }
        public Site Site { get; set; }
        private int? StructureID { get; set; }
        private Structure Structure { get; set; }
        private int? SiteCivID { get; set; }
        public Entity SiteCiv { get; set; }
        private int? CivID { get; set; }
        public Entity Civ { get; set; }
        private int? BuilderHFID { get; set; }
        public HistoricalFigure BuilderHF { get; set; }

        override public Point Location { get { return Site.Location; } }

        public HE_CreatedStructure(XDocument xdoc, World world)
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
                    case "site_civ_id":
                        SiteCivID = valI;
                        break;
                    case "site_id":
                        SiteID = valI;
                        break;
                    case "structure_id":
                        StructureID = valI;
                        break;
                    case "builder_hfid":
                        BuilderHFID = valI;
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
            if (SiteID.HasValue && World.Sites.ContainsKey(SiteID.Value))
            {
                Site = World.Sites[SiteID.Value];
                if (StructureID.HasValue)
                { 
                    Structure = Site.GetStructure(StructureID.Value);

                    if (Structure == null)
                    {
                        Structure = new Structure(Site, StructureID.Value, World);
                        Site.AddStructure(Structure);
                    }
                }
            }

            if (CivID.HasValue && World.Entities.ContainsKey(CivID.Value))
                Civ = World.Entities[CivID.Value];
            if (SiteCivID.HasValue && World.Entities.ContainsKey(SiteCivID.Value))
                SiteCiv = World.Entities[SiteCivID.Value];
            if (BuilderHFID.HasValue && World.HistoricalFigures.ContainsKey(BuilderHFID.Value))
                BuilderHF = World.HistoricalFigures[BuilderHFID.Value];


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
                    case "civ":
                    case "group":
                    case "site":
                    case "structure":
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
            if (Structure != null)
            {
                if (Structure.Events == null)
                    Structure.Events = new List<HistoricalEvent>();
                Structure.Events.Add(this);
            }


            if (SiteCiv != null)
            {
                if (SiteCiv.Events == null)
                    SiteCiv.Events = new List<HistoricalEvent>();
                SiteCiv.Events.Add(this);
            }

            if (Civ != null)
            {
                if (Civ.Events == null)
                    Civ.Events = new List<HistoricalEvent>();
                Civ.Events.Add(this);
            }

            if (BuilderHF != null)
            {
                if (BuilderHF.Events == null)
                    BuilderHF.Events = new List<HistoricalEvent>();
                BuilderHF.Events.Add(this);
                if (Time.Year == -1 &&
                    NextEvent().Type == Types.IndexOf("add hf entity link") &&
                    NextEvent().NextEvent().Type == Types.IndexOf("change hf state") &&
                    NextEvent().NextEvent().NextEvent().Type == Types.IndexOf("add hf site link"))
                {
                    ProcessSladeSpireEventSet();
                }
            }
        }


        private void ProcessSladeSpireEventSet()
        {
            var AddHFEntityLinkEvent = NextEvent() as HE_AddHFEntityLink;
            var ChangeHFStateEvent = NextEvent().NextEvent() as HE_ChangeHFState;
            var AddHFSiteLinkEvent = NextEvent().NextEvent().NextEvent() as HE_AddHFSiteLink;

            AddHFEntityLinkEvent.HF = BuilderHF;
            AddHFSiteLinkEvent.HF = BuilderHF;
            AddHFSiteLinkEvent.Structure = Structure;
            AddHFSiteLinkEvent.Civ = AddHFEntityLinkEvent.Civ;
            ChangeHFStateEvent.HF = BuilderHF;
        }

        protected override void WriteDataOnParent(MainForm frm, Control parent, ref Point location)
        {
            if (Civ != null)
                EventLabel(frm, parent, ref location, "Civ:", Civ);
            if (SiteCiv != null)
                EventLabel(frm, parent, ref location, "Owner:", SiteCiv);
            if (BuilderHF != null)
                EventLabel(frm, parent, ref location, "Builder:", BuilderHF);
            EventLabel(frm, parent, ref location, "Site:", Site);
            EventLabel(frm, parent, ref location, "Structure:", Structure);
        }

        protected override string LegendsDescription() //Matched
        {
            var timestring = base.LegendsDescription();

            if (BuilderHF != null)
                return string.Format("{0} {1} thrust a spire of slade up from the underworld naming it {2}, and established a gateway between worlds in {3}.",
                                timestring, BuilderHF, Structure,
                                Site.AltName);

            if (SiteCiv == null)
                return string.Format("{0} {1} constructed {2} in {3}.",
                                timestring, Civ, Structure,
                                Site.AltName);

            return string.Format("{0} {1} of {2} constructed {3} in {4}.",
                timestring, SiteCiv, Civ, Structure,
                Site.AltName);

        }

        internal override string ToTimelineString()
        {
            var timelinestring = base.ToTimelineString();

            if (BuilderHF != null)
                return string.Format("{0} {1} established a gateway between worlds in {2}.",
                                timelinestring, BuilderHF,
                                Site.AltName);

            return string.Format("{0} {1} built a structure in {2}.",
                        timelinestring, Civ,
                                Site.AltName);
        }

        internal override void Export(string table)
        {
            base.Export(table);


            table = GetType().Name;

            var vals = new List<object> { ID, SiteID, StructureID, SiteCivID, CivID, BuilderHFID };


            Database.ExportWorldItem(table, vals);

        }

    }
}
