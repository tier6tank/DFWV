using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Linq;
using DFWV.WorldClasses.HistoricalFigureClasses;

namespace DFWV.WorldClasses.HistoricalEventClasses
{
    public class HE_ArtifactStored : HistoricalEvent
    {
        private int? SiteId { get; }
        private Site Site { get; set; }
        private int? HfId { get; }
        private HistoricalFigure Hf { get; set; }
        private int? UnitId { get; }
        private HistoricalFigure Unit { get; set; }
        private int? ArtifactId { get; }
        private Artifact Artifact { get; set; }

        override public Point Location => Site.Location;

        public override IEnumerable<HistoricalFigure> HFsInvolved
        {
            get { yield return Hf; }
        }
        public override IEnumerable<Site> SitesInvolved
        {
            get { yield return Site; }
        }

        public HE_ArtifactStored(XDocument xdoc, World world)
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
                    case "hist_figure_id":
                        HfId = valI;
                        break;
                    case "site_id":
                        SiteId = valI;
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
            if (UnitId.HasValue && World.HistoricalFigures.ContainsKey(UnitId.Value))
                Unit = World.HistoricalFigures[UnitId.Value];
        }


        internal override void Process()
        {
            base.Process();

            if (Artifact == null) return;
            if (Artifact.StoredEvents == null)
                Artifact.StoredEvents = new List<HE_ArtifactStored>();
            Artifact.StoredEvents.Add(this);
        }

        protected override void WriteDataOnParent(MainForm frm, Control parent, ref Point location)
        {
            EventLabel(frm, parent, ref location, "Artifact:", Artifact);
            if (Artifact.Type != "" && Artifact.Material != "")
                EventLabel(frm, parent, ref location, "Item:", Artifact.Material + " " + Artifact.Type);
            if (UnitId != null && UnitId > -1)
                EventLabel(frm, parent, ref location, "Unit ID:", UnitId.Value.ToString());
            EventLabel(frm, parent, ref location, "HF:", Hf);
            EventLabel(frm, parent, ref location, "Site:", Site);
        }

        protected override string LegendsDescription()
        {
            var timestring = base.LegendsDescription();

            return $"{timestring} {Artifact} was stored in {Site.AltName} by the {Hf}.";
        }

        internal override string ToTimelineString()
        {
            var timelinestring = base.ToTimelineString();

            return $"{timelinestring} {Artifact} was stored in {Site.AltName} by {Hf}.";
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
                HfId.DBExport()
            };


            Database.ExportWorldItem(table, vals);

        }

    }
}