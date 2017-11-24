using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Linq;
using DFWV.WorldClasses.HistoricalFigureClasses;

namespace DFWV.WorldClasses.HistoricalEventClasses
{
    public class HE_ArtifactPossessed : HistoricalEvent
    {
        private int? SiteId { get; }
        private Site Site { get; set; }
        private int? HistFigureId { get; }
        private HistoricalFigure HistFigure { get; set; }
        private int? UnitId { get; }
        private HistoricalFigure Unit { get; set; }
        private int? ArtifactId { get; }
        private Artifact Artifact { get; set; }
        public int? Reason { get; set; }
        public static List<string> Reasons = new List<string>();
        public int? ReasonId { get; set; }
        public int? SubregionId { get; set; }
        public Region Subregion { get; private set; }
        public int? FeatureLayerId { get; set; }

        override public Point Location => Site?.Location ?? Point.Empty;

        public override IEnumerable<HistoricalFigure> HFsInvolved
        {
            get { yield return HistFigure; }
        }
        public override IEnumerable<Site> SitesInvolved
        {
            get { yield return Site; }
        }

        public HE_ArtifactPossessed(XDocument xdoc, World world)
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
                    case "unit_id":
                        UnitId = valI;
                        break;
                    case "reason":
                        if (!Reasons.Contains(val))
                            Reasons.Add(val);
                        Reason = Reasons.IndexOf(val);
                        break;
                    case "reason_id":
                        ReasonId = valI;
                        break;
                    case "hist_figure_id":
                        HistFigureId = valI;
                        break;
                    case "site_id":
                        SiteId = valI;
                        break;
                    case "subregion_id":
                        if (valI != -1)
                            SubregionId = valI;
                        break;
                    case "feature_layer_id":
                        if (valI != -1)
                            FeatureLayerId = valI;
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
            if (HistFigureId.HasValue && World.HistoricalFigures.ContainsKey(HistFigureId.Value))
                HistFigure = World.HistoricalFigures[HistFigureId.Value];
            if (ArtifactId.HasValue && World.Artifacts.ContainsKey(ArtifactId.Value))
                Artifact = World.Artifacts[ArtifactId.Value];
            if (UnitId.HasValue && World.HistoricalFigures.ContainsKey(UnitId.Value))
                Unit = World.HistoricalFigures[UnitId.Value];
        }


        internal override void Process()
        {
            base.Process();


            if (Artifact == null) return;
            if (Artifact.PossessedEvents == null)
                Artifact.PossessedEvents = new List<HE_ArtifactPossessed>();
            Artifact.PossessedEvents.Add(this);
        }

        protected override void WriteDataOnParent(MainForm frm, Control parent, ref Point location)
        {
            EventLabel(frm, parent, ref location, "Artifact:", Artifact);
            if (Artifact.Type != "" && Artifact.Material != "")
                EventLabel(frm, parent, ref location, "Item:", Artifact.Material + " " + Artifact.Type);
            if (UnitId != null && UnitId > -1)
                EventLabel(frm, parent, ref location, "Unit ID:", UnitId.Value.ToString());
            EventLabel(frm, parent, ref location, "Possessor:", HistFigure);
            EventLabel(frm, parent, ref location, "Site:", Site);
        }

        protected override string LegendsDescription() //Not Matched
        {
            var timestring = base.LegendsDescription();

            return
                $"{timestring} the {HistFigure.Race} {HistFigure} learned {(Artifact.Description == "" ? "UNKNOWN" : Artifact.Description)} from {Artifact}.";
        }

        internal override string ToTimelineString()
        {
            var timelinestring = base.ToTimelineString();

            return $"{timelinestring} the {HistFigure} learned secrets from {Artifact}.";
        }

        internal override void Export(string table)
        {
            base.Export(table);


            table = GetType().Name;

            var vals = new List<object>
            {
                Id, 
                ArtifactId.DBExport(), 
                UnitId.DBExport(), 
                SiteId.DBExport(), 
                HistFigureId.DBExport()
            };


            Database.ExportWorldItem(table, vals);

        }

    }
}