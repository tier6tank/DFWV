﻿using DFWV.WorldClasses.HistoricalFigureClasses;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Linq;

namespace DFWV.WorldClasses.HistoricalEventClasses
{
    public class HE_HFViewedArtifact : HistoricalEvent
    {
        private int? ArtifactId { get; }
        private Artifact Artifact { get; set; }
        private int? HfId { get; }
        private HistoricalFigure Hf { get; set; }
        private int? SiteId { get; }
        private Site Site { get; set; }
        private int? StructureId { get; set; }
        public Structure Structure { get; set; }

        override public Point Location => Site.Location;

        public override IEnumerable<HistoricalFigure> HFsInvolved
        {
            get
            {
                yield return Hf;
            }
        }

        public override IEnumerable<Site> SitesInvolved
        {
            get
            {
                yield return Site;
            }
        }


        public HE_HFViewedArtifact(XDocument xdoc, World world) : base(xdoc, world)
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
                    case "hist_fig_id":
                        HfId = valI;
                        break;
                    case "site_id":
                        SiteId = valI;
                        break;
                    case "structure_id":
                        SiteId = valI;
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

            if (Artifact == null) return;
            if (Artifact.ArtifactEvents == null)
                Artifact.ArtifactEvents = new List<HistoricalEvent>();
            Artifact.ArtifactEvents.Add(this);
        }

        protected override void WriteDataOnParent(MainForm frm, Control parent, ref Point location)
        {
            EventLabel(frm, parent, ref location, "Artifact:", Artifact);
            EventLabel(frm, parent, ref location, "Hist Fig:", Hf);
            EventLabel(frm, parent, ref location, "Site:", Site);
            EventLabel(frm, parent, ref location, "Structure:", Structure);
        }

        protected override string LegendsDescription()
        {
            var timestring = base.LegendsDescription();

            return $"{timestring} {Hf} viewed {Artifact} in {Structure} in {Site}.";
        }

        internal override string ToTimelineString()
        {
            var timelinestring = base.ToTimelineString();

            return timelinestring;
        }

    }
}