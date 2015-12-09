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
        private int? SiteId { get; }
        private Site Site { get; set; }
        private int? SiteCivId { get; }
        public Entity SiteCiv { get; private set; }
        private int? CivId { get; }
        public Entity Civ { get; private set; }
        private int? BuilderHfid { get; }
        public HistoricalFigure BuilderHf { get; set; }

        override public Point Location => Site.Location;

        public override IEnumerable<HistoricalFigure> HFsInvolved
        {
            get { yield return BuilderHf; }
        }
        public override IEnumerable<Entity> EntitiesInvolved
        {
            get
            {
                yield return Civ;
                yield return SiteCiv;
            }
        }
        public override IEnumerable<Site> SitesInvolved
        {
            get { yield return Site; }
        }

        public HE_CreatedSite(XDocument xdoc, World world)
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
                    case "site_civ_id":
                        if (valI != -1)
                        SiteCivId = valI;
                        break;
                    case "site_id":
                        SiteId = valI;
                        break;
                    case "builder_hfid":
                        BuilderHfid = valI;
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
            if (SiteId.HasValue && World.Sites.ContainsKey(SiteId.Value))
                Site = World.Sites[SiteId.Value];
            if (CivId.HasValue && World.Entities.ContainsKey(CivId.Value))
                Civ = World.Entities[CivId.Value];
            if (SiteCivId.HasValue && World.Entities.ContainsKey(SiteCivId.Value))
                SiteCiv = World.Entities[SiteCivId.Value];
            if (BuilderHfid.HasValue && World.HistoricalFigures.ContainsKey(BuilderHfid.Value))
                BuilderHf = World.HistoricalFigures[BuilderHfid.Value];

        }


        internal override void Process()
        {
            base.Process();
            Site.CreatedEvent = this;

            if (BuilderHf == null) return;
            if (Time.Year == -1 &&
                NextEvent().Type == Types.IndexOf("artifact created") &&
                NextEvent().NextEvent().Type == Types.IndexOf("agreement formed") &&
                NextEvent().NextEvent().NextEvent().Type == Types.IndexOf("artifact stored"))
            {
                ProcessSladeSpireEventSet();
            }
        }

        private void ProcessSladeSpireEventSet()
        {
            var artifactCreatedEvent = NextEvent() as HE_ArtifactCreated;
            var agreementFormedEvent = NextEvent().NextEvent() as HE_AgreementFormed;
            //HE_ArtifactStored artifactStoredEvent = NextEvent().NextEvent().NextEvent() as HE_ArtifactStored;

            artifactCreatedEvent.Site = Site;
            agreementFormedEvent.Hf = BuilderHf;
            agreementFormedEvent.Site = Site;
            agreementFormedEvent.Artifact = artifactCreatedEvent.Artifact;
        }

        protected override void WriteDataOnParent(MainForm frm, Control parent, ref Point location)
        {
            if (Civ != null)
                EventLabel(frm, parent, ref location, "Civ:", Civ);
            if (SiteCiv != null)
                EventLabel(frm, parent, ref location, "Owner:", SiteCiv);
            if (BuilderHf != null)
                EventLabel(frm, parent, ref location, "Builder:", BuilderHf);
            EventLabel(frm, parent, ref location, "Site:", Site);
        }

        protected override string LegendsDescription() //Matched
        {
            var timestring = base.LegendsDescription();

            if (BuilderHf != null)
                return $"{timestring} {BuilderHf} founded {Site.AltName}.";
            return SiteCiv != null ?
                $"{timestring} {SiteCiv} of {Civ} founded {Site.AltName}."
                : $"{timestring} {Civ} founded {Site.AltName}.";
        }

        internal override string ToTimelineString()
        {
            var timelinestring = base.ToTimelineString();

            if (BuilderHf != null)
                return $"{timelinestring} {BuilderHf} founded {Site.AltName}.";

            return $"{timelinestring} {Civ} founded {Site.AltName}.";
        }

        internal override void Export(string table)
        {
            base.Export(table);


            table = GetType().Name;

            var vals = new List<object>
            {
                Id, 
                SiteId.DBExport(), 
                SiteCivId.DBExport(), 
                CivId.DBExport(), 
                BuilderHfid.DBExport()
            };


            Database.ExportWorldItem(table, vals);

        }

    }
}
