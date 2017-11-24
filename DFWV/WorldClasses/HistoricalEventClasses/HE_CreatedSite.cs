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
        private int? EntityId_SiteCiv { get; }
        public Entity Entity_SiteCiv { get; private set; }
        private int? EntityId { get; }
        public Entity Entity { get; private set; }
        private int? HfId { get; }
        public HistoricalFigure Hf { get; set; }

        override public Point Location => Site.Location;

        public override IEnumerable<HistoricalFigure> HFsInvolved
        {
            get { yield return Hf; }
        }
        public override IEnumerable<Entity> EntitiesInvolved
        {
            get
            {
                yield return Entity;
                yield return Entity_SiteCiv;
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
                        EntityId = valI;
                        break;
                    case "site_civ_id":
                        if (valI != -1)
                        EntityId_SiteCiv = valI;
                        break;
                    case "site_id":
                        SiteId = valI;
                        break;
                    case "builder_hfid":
                        HfId = valI;
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
            Site.CreatedEvent = this;

            if (Hf == null) return;
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
            agreementFormedEvent.Hf = Hf;
            agreementFormedEvent.Site = Site;
            agreementFormedEvent.Artifact = artifactCreatedEvent.Artifact;
        }

        protected override void WriteDataOnParent(MainForm frm, Control parent, ref Point location)
        {
            if (Entity != null)
                EventLabel(frm, parent, ref location, "Civ:", Entity);
            if (Entity_SiteCiv != null)
                EventLabel(frm, parent, ref location, "Owner:", Entity_SiteCiv);
            if (Hf != null)
                EventLabel(frm, parent, ref location, "Builder:", Hf);
            EventLabel(frm, parent, ref location, "Site:", Site);
        }

        protected override string LegendsDescription() //Matched
        {
            var timestring = base.LegendsDescription();

            if (Hf != null)
                return $"{timestring} {Hf} founded {Site.AltName}.";
            return Entity_SiteCiv != null ?
                $"{timestring} {Entity_SiteCiv} of {Entity} founded {Site.AltName}."
                : $"{timestring} {Entity} founded {Site.AltName}.";
        }

        internal override string ToTimelineString()
        {
            var timelinestring = base.ToTimelineString();

            if (Hf != null)
                return $"{timelinestring} {Hf} founded {Site.AltName}.";

            return $"{timelinestring} {Entity} founded {Site.AltName}.";
        }

        internal override void Export(string table)
        {
            base.Export(table);


            table = GetType().Name;

            var vals = new List<object>
            {
                Id, 
                SiteId.DBExport(), 
                EntityId_SiteCiv.DBExport(), 
                EntityId.DBExport(), 
                HfId.DBExport()
            };


            Database.ExportWorldItem(table, vals);

        }

    }
}
