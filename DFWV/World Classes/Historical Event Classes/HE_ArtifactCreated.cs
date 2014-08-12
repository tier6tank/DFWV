using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;
using DFWV.WorldClasses.HistoricalFigureClasses;

namespace DFWV.WorldClasses.HistoricalEventClasses
{
    class HE_ArtifactCreated : HistoricalEvent
    {
        public int? SiteID { get; set; }
        public Site Site { get; set; }
        public int? HistFigureID { get; set; }
        public HistoricalFigure HistFigure { get; set; }
        public int? UnitID { get; set; }
        public HistoricalFigure Unit { get; set; }
        public int? ArtifactID { get; set; }
        public Artifact Artifact { get; set; }
        public bool NameOnly { get; set; }

        override public Point Location { get { return Site != null ? Site.Location : Point.Empty; } }

        public HE_ArtifactCreated(XDocument xdoc, World world)
            : base(xdoc, world)
        {
            foreach (XElement element in xdoc.Root.Elements())
            {
                string val = element.Value.ToString();
                int valI;
                Int32.TryParse(val, out valI);

                switch (element.Name.LocalName)
                {
                    case "id":
                    case "year":
                    case "seconds72":
                    case "type":
                        break;
                    case "artifact_id":
                        ArtifactID = valI;
                        break;
                    case "unit_id":
                        if (valI != -1)
                        UnitID = valI;
                        break;
                    case "hist_figure_id":
                        HistFigureID = valI;
                        break;
                    case "site_id":
                        if (valI != -1)
                        SiteID = valI;
                        break;
                    case "name_only":
                        NameOnly = true;
                        break;

                    default:
                        DFXMLParser.UnexpectedXMLElement(xdoc.Root.Name.LocalName + "\t" + HistoricalEvent.Types[Type], element, xdoc.Root.ToString());
                        break;
                }
            }
        }

        internal override void Link()
        {
            base.Link();
            if (SiteID.HasValue && World.Sites.ContainsKey(SiteID.Value))
                Site = World.Sites[SiteID.Value];
            if (HistFigureID.HasValue && World.HistoricalFigures.ContainsKey(HistFigureID.Value))
                HistFigure = World.HistoricalFigures[HistFigureID.Value];
            if (ArtifactID.HasValue && World.Artifacts.ContainsKey(ArtifactID.Value))
                Artifact = World.Artifacts[ArtifactID.Value];
            if (UnitID.HasValue && World.HistoricalFigures.ContainsKey(UnitID.Value))
                Unit = World.HistoricalFigures[UnitID.Value];
        }


        internal override void Process()
        {
            base.Process();
            if (HistFigure != null)
            {
                if (HistFigure.Events == null)
                    HistFigure.Events = new List<HistoricalEvent>();
                HistFigure.Events.Add(this);
            }
        }

        public override void WriteDataOnParent(MainForm frm, Control parent, ref Point location)
        {
            EventLabel(frm, parent, ref location, "Artifact:", Artifact);
            if (UnitID != null)
                EventLabel(frm, parent, ref location, "Unit ID:", UnitID.Value.ToString());
            EventLabel(frm, parent, ref location, "Made by:", HistFigure);
            EventLabel(frm, parent, ref location, "Site:", Site);
        }

        public override string LegendsDescription()
        {
            string timestring = base.LegendsDescription();

            if (Site == null)
                return string.Format("{0} {1} was created by {2}.",
                    timestring, Artifact.ToString(),
                    HistFigure.ToString());
            else
                return string.Format("{0} {1} was created in {2} by {3}.",
                    timestring, Artifact.ToString(), Site.AltName,
                    HistFigure.ToString());
        }

        internal override string ToTimelineString()
        {
            string timelinestring = base.ToTimelineString();

            if (Site == null)
                return string.Format("{0} {1} was created by {2}.",
                    timelinestring, Artifact.ToString(),
                    HistFigure.ToString());
            else
                return string.Format("{0} {1} was created in {2} by {3}.",
                    timelinestring, Artifact.ToString(), Site.AltName,
                    HistFigure.ToString());
        }

        internal override void Export(string table)
        {
            base.Export(table);

            
            List<object> vals;
            table = this.GetType().Name.ToString();

            vals = new List<object>() { ID, ArtifactID, UnitID, SiteID, HistFigureID, NameOnly };


            Database.ExportWorldItem(table, vals);

        }

    }
}