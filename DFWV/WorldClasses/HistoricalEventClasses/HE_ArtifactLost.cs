using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Linq;

namespace DFWV.WorldClasses.HistoricalEventClasses
{
    public class HeArtifactLost : HistoricalEvent
    {
        private int? SiteId { get; }
        public Site Site { get; private set; }
        private int? ArtifactId { get; }
        private Artifact Artifact { get; set; }

        override public Point Location => Site?.Location ?? Point.Empty;

        public override IEnumerable<Site> SitesInvolved
        {
            get { yield return Site; }
        }

        public HeArtifactLost(XDocument xdoc, World world)
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
                    case "artifact_id":
                        ArtifactId = valI;
                        break;
                    case "site_id":
                        SiteId = valI;
                        break;

                    default:
                        DfxmlParser.UnexpectedXmlElement(xdoc.Root.Name.LocalName + "\t" + Types[Type], element, xdoc.Root.ToString());
                        break;
                }
            }
        }

        internal override void Link()
        {
            base.Link();
            if (SiteId.HasValue && World.Sites.ContainsKey(SiteId.Value))
                Site = World.Sites[SiteId.Value];
            if (ArtifactId.HasValue && World.Artifacts.ContainsKey(ArtifactId.Value))
                Artifact = World.Artifacts[ArtifactId.Value];
        }

        internal override void Process()
        {
            base.Process();
            if (Artifact != null)
            {
                Artifact.LostEvent = this;
            }

        }

        protected override void WriteDataOnParent(MainForm frm, Control parent, ref Point location)
        {
            EventLabel(frm, parent, ref location, "Artifact:", Artifact);
            EventLabel(frm, parent, ref location, "Site:", Site);
        }

        protected override string LegendsDescription()
        {
            var timestring = base.LegendsDescription();

            if (Site == null)
                return $"{timestring} {Artifact} was lost in an unknown site.";
            return $"{timestring} {Artifact} was lost in {Site.AltName}.";
        }

        internal override string ToTimelineString()
        {
            var timelinestring = base.ToTimelineString();

            if (Site == null)
                return $"{timelinestring} {Artifact} was lost.";
            return $"{timelinestring} {Artifact} was lost in {Site.AltName}.";
        }

        internal override void Export(string table)
        {
            base.Export(table);


            table = GetType().Name;


            var vals = new List<object>
            {
                Id, 
                ArtifactId.DBExport(), 
                SiteId.DBExport()
            };


            Database.ExportWorldItem(table, vals);

        }

    }
}