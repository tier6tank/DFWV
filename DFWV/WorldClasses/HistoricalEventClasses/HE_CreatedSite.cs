using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Linq;
using DFWV.WorldClasses.EntityClasses;
using DFWV.WorldClasses.HistoricalFigureClasses;

namespace DFWV.WorldClasses.HistoricalEventClasses
{
    public class HE_CreatedSite : HistoricalEvent
    {
        private int? SiteID { get; set; }
        private Site Site { get; set; }
        private int? SiteCivID { get; set; }
        public Entity SiteCiv { get; private set; }
        private int? CivID { get; set; }
        public Entity Civ { get; private set; }
        private int? BuilderHFID { get; set; }
        public HistoricalFigure BuilderHF { get; set; }

        override public Point Location { get { return Site.Location; } }

        public override IEnumerable<HistoricalFigure> HFsInvolved
        {
            get { yield return BuilderHF; }
        }

        public HE_CreatedSite(XDocument xdoc, World world)
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
                        if (valI != -1)
                        SiteCivID = valI;
                        break;
                    case "site_id":
                        SiteID = valI;
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
                Site = World.Sites[SiteID.Value];
            if (CivID.HasValue && World.Entities.ContainsKey(CivID.Value))
                Civ = World.Entities[CivID.Value];
            if (SiteCivID.HasValue && World.Entities.ContainsKey(SiteCivID.Value))
                SiteCiv = World.Entities[SiteCivID.Value];
            if (BuilderHFID.HasValue && World.HistoricalFigures.ContainsKey(BuilderHFID.Value))
                BuilderHF = World.HistoricalFigures[BuilderHFID.Value];

        }


        internal override void Process()
        {
            base.Process();
            Site.CreatedEvent = this;

            if (Civ != null)
            {
                if (Civ.Events == null)
                    Civ.Events = new List<HistoricalEvent>();
                Civ.Events.Add(this);
            }
            if (SiteCiv != null)
            {

                if (SiteCiv.Events == null)
                    SiteCiv.Events = new List<HistoricalEvent>();
                SiteCiv.Events.Add(this);
            }
            if (BuilderHF != null)
            {
                if (Time.Year == -1 &&
                    NextEvent().Type == Types.IndexOf("artifact created") &&
                    NextEvent().NextEvent().Type == Types.IndexOf("agreement formed") &&
                    NextEvent().NextEvent().NextEvent().Type == Types.IndexOf("artifact stored"))
                {
                    ProcessSladeSpireEventSet();
                }
            }
        }

        private void ProcessSladeSpireEventSet()
        {
            var artifactCreatedEvent = NextEvent() as HE_ArtifactCreated;
            var agreementFormedEvent = NextEvent().NextEvent() as HE_AgreementFormed;
            //HE_ArtifactStored artifactStoredEvent = NextEvent().NextEvent().NextEvent() as HE_ArtifactStored;

            artifactCreatedEvent.Site = Site;
            agreementFormedEvent.HF = BuilderHF;
            agreementFormedEvent.Site = Site;
            agreementFormedEvent.Artifact = artifactCreatedEvent.Artifact;
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
        }

        protected override string LegendsDescription() //Matched
        {
            var timestring = base.LegendsDescription();

            if (BuilderHF != null)
                return string.Format("{0} {1} founded {2}.", timestring, BuilderHF, Site.AltName);
            if (SiteCiv != null)
                return string.Format("{0} {1} of {2} founded {3}.", timestring, SiteCiv, Civ, Site.AltName);
            return string.Format("{0} {1} founded {2}.", timestring, Civ, Site.AltName);
        }

        internal override string ToTimelineString()
        {
            var timelinestring = base.ToTimelineString();

            if (BuilderHF != null)
                return string.Format("{0} {1} founded {2}.",
                    timelinestring, BuilderHF, Site.AltName);

            return string.Format("{0} {1} founded {2}.",
                        timelinestring, Civ, Site.AltName);
        }

        internal override void Export(string table)
        {
            base.Export(table);


            table = GetType().Name;

            var vals = new List<object>
            {
                ID, 
                SiteID.DBExport(), 
                SiteCivID.DBExport(), 
                CivID.DBExport(), 
                BuilderHFID.DBExport()
            };


            Database.ExportWorldItem(table, vals);

        }

    }
}
