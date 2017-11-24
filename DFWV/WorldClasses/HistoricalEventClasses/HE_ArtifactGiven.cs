using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Linq;
using DFWV.WorldClasses.EntityClasses;
using DFWV.WorldClasses.HistoricalFigureClasses;

namespace DFWV.WorldClasses.HistoricalEventClasses
{
    class HE_ArtifactGiven : HistoricalEvent
    {
        private int? ArtifactId { get; }
        private Artifact Artifact { get; set; }
        private int? GiverHistFigureId { get; }
        private HistoricalFigure GiverHistFigure { get; set; }
        private int? GiverEntityId { get; }
        private Entity GiverEntity { get; set; }
        private int? ReceiverHistFigureId { get; }
        private HistoricalFigure ReceiverHistFigure { get; set; }
        private int? ReceiverEntityId { get; }
        private Entity ReceiverEntity { get; set; }

        override public Point Location => ReceiverEntity.Location;

        public override IEnumerable<HistoricalFigure> HFsInvolved
        {
            get {
                yield return GiverHistFigure;
                yield return ReceiverHistFigure;
            }
        }
        public override IEnumerable<Entity> EntitiesInvolved
        {
            get
            {
                yield return GiverEntity;
                yield return ReceiverEntity;
            }
        }

        public HE_ArtifactGiven(XDocument xdoc, World world)
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
                    case "giver_hist_figure_id":
                        GiverHistFigureId = valI;
                        break;
                    case "giver_entity_id":
                        GiverEntityId = valI;
                        break;
                    case "receiver_hist_figure_id":
                        ReceiverHistFigureId = valI;
                        break;
                    case "receiver_entity_id":
                        ReceiverEntityId = valI;
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
            if (ArtifactId.HasValue && World.Artifacts.ContainsKey(ArtifactId.Value))
                Artifact = World.Artifacts[ArtifactId.Value];
            if (GiverHistFigureId.HasValue && World.HistoricalFigures.ContainsKey(GiverHistFigureId.Value))
                GiverHistFigure = World.HistoricalFigures[GiverHistFigureId.Value];
            if (GiverEntityId.HasValue && World.Entities.ContainsKey(GiverEntityId.Value))
                GiverEntity = World.Entities[GiverEntityId.Value];
            if (ReceiverHistFigureId.HasValue && World.HistoricalFigures.ContainsKey(ReceiverHistFigureId.Value))
                ReceiverHistFigure = World.HistoricalFigures[ReceiverHistFigureId.Value];
            if (ReceiverEntityId.HasValue && World.Entities.ContainsKey(ReceiverEntityId.Value))
                ReceiverEntity = World.Entities[ReceiverEntityId.Value];
        }

        protected override void WriteDataOnParent(MainForm frm, Control parent, ref Point location)
        {
            EventLabel(frm, parent, ref location, "Hist Fig:", GiverHistFigure);
        }

        protected override string LegendsDescription()
        {
            var timestring = base.LegendsDescription();

            return "";
        }

        internal override string ToTimelineString()
        {
            var timelinestring = base.ToTimelineString();

            return timelinestring;
        }
    }
}