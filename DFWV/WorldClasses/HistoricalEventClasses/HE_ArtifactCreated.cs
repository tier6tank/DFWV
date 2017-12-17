using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Linq;
using DFWV.WorldClasses.HistoricalFigureClasses;

namespace DFWV.WorldClasses.HistoricalEventClasses
{
    //TODO: Missing Details:  UnitID does not associate with anything, "NameOnly" events aren't shown in legends
    public class HE_ArtifactCreated : HistoricalEvent
    {
        private int? SiteId { get; }
        public Site Site { get; set; }
        private int? HfId { get; }
        public HistoricalFigure Hf { get; private set; }
        private int? UnitId { get; }
        private int? ArtifactId { get; }
        public Artifact Artifact { get; set; }
        private bool NameOnly { get; }

        override public Point Location => Site?.Location ?? Point.Empty;

        public override IEnumerable<HistoricalFigure> HFsInvolved
        {
            get { yield return Hf; }
        }
        public override IEnumerable<Site> SitesInvolved
        {
            get { yield return Site; }
        }

        public HE_ArtifactCreated(XDocument xdoc, World world)
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
                        if (valI != -1)
                        UnitId = valI;
                        break;
                    case "hist_figure_id":
                        HfId = valI;
                        break;
                    case "site_id":
                        if (valI != -1)
                        SiteId = valI;
                        break;
                    case "name_only":
                        NameOnly = true;
                        break;

                    default:
                        DFXMLParser.UnexpectedXmlElement(xdoc.Root.Name.LocalName + "\t" + Types[Type], element, xdoc.Root.ToString());
                        break;
                }
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
                    case "artifact_id":
                    case "unit_id":
                    case "hfid":
                    case "site":
                        break;
                    default:
                        DFXMLParser.UnexpectedXmlElement(xdoc.Root.Name.LocalName + "\t" + Types[Type], element, xdoc.Root.ToString());
                        break;
                }
            }
        }

        public override void Process()
        {
            base.Process();
            if (Artifact != null)
            {
                Artifact.CreatedEvent = this;
            }

            if (Site != null)
            {
                if (Site.CreatedArtifacts == null)
                    Site.CreatedArtifacts = new List<Artifact>();
                Site.CreatedArtifacts.Add(Artifact);
            }

            if (Hf != null)
            {
                if (Hf.CreatedArtifacts == null)
                    Hf.CreatedArtifacts = new List<Artifact>();
                Hf.CreatedArtifacts.Add(Artifact);
            }
        }

        protected override void WriteDataOnParent(MainForm frm, Control parent, ref Point location)
        {
            EventLabel(frm, parent, ref location, "Artifact:", Artifact);
            if (Artifact.Type != "" && Artifact.Material != "")
                EventLabel(frm, parent, ref location, "Item:", Artifact.Material + " " + Artifact.Type);
            if (UnitId != null)
                EventLabel(frm, parent, ref location, "Unit ID:", UnitId.Value.ToString());
            EventLabel(frm, parent, ref location, "Made by:", Hf);
            EventLabel(frm, parent, ref location, "Site:", Site);
        }

        protected override string LegendsDescription()
        {
            var timestring = base.LegendsDescription();

            if (Site == null)
                return $"{timestring} {Artifact} was created by {Hf}.";
            return $"{timestring} {Artifact} was created in {Site.AltName} by {Hf}.";
        }

        internal override string ToTimelineString()
        {
            //TODO: Incorporate new data
            var timelinestring = base.ToTimelineString();

            if (Site == null)
                return $"{timelinestring} {Artifact} was created by {Hf}.";
            return $"{timelinestring} {Artifact} was created in {Site.AltName} by {Hf}.";
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
                HfId.DBExport(), 
                NameOnly
            };

            Database.ExportWorldItem(table, vals);

        }

    }
}