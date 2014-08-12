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
    class HE_ArtifactStored : HistoricalEvent
    {
        public int? SiteID { get; set; }
        public Site Site { get; set; }
        public int? HistFigureID { get; set; }
        public HistoricalFigure HistFigure { get; set; }
        public int? UnitID { get; set; }
        public HistoricalFigure Unit { get; set; }
        public int? ArtifactID { get; set; }
        public Artifact Artifact { get; set; }

        override public Point Location { get { return Site.Location; } }

        public HE_ArtifactStored(XDocument xdoc, World world)
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
                        UnitID = valI;
                        break;
                    case "hist_figure_id":
                        HistFigureID = valI;
                        break;
                    case "site_id":
                        SiteID = valI;
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
            EventLabel(frm, parent, ref location, "Unit ID:", UnitID.Value.ToString());
            EventLabel(frm, parent, ref location, "HF:", HistFigure);
            EventLabel(frm, parent, ref location, "Site:", Site);
        }

        public override string LegendsDescription()
        {
            string timestring = base.LegendsDescription();

            return string.Format("{0} {1} was stored in {2} by the {3} {4}.",
                                timestring, Artifact.ToString(), Site.AltName,
                                HistFigure.Race.ToString(), HistFigure.ToString());
        }

        internal override string ToTimelineString()
        {
            string timelinestring = base.ToTimelineString();

            return string.Format("{0} {1} was stored in {1} by {2}.",
                        timelinestring, Artifact.ToString(), Site.AltName,
                                HistFigure.ToString());
        }

        internal override void Export(string table)
        {
            base.Export(table);

            
            List<object> vals;
            table = this.GetType().Name.ToString();


            vals = new List<object>() { ID, ArtifactID, UnitID, SiteID, HistFigureID };


            Database.ExportWorldItem(table, vals);

        }

    }
}