using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Linq;
using DFWV.WorldClasses.EntityClasses;
using DFWV.WorldClasses.HistoricalFigureClasses;

namespace DFWV.WorldClasses.HistoricalEventClasses
{
    public class HE_ArtifactGiven : HistoricalEvent
    {
        private int? ArtifactId { get; }
        private Artifact Artifact { get; set; }
        private int? HfId_Giver { get; }
        private HistoricalFigure Hf_Giver { get; set; }
        private int? EntityId_Giver { get; }
        private Entity Entity_Giver { get; set; }
        private int? HfId_Receiver { get; }
        private HistoricalFigure Hf_Receiver { get; set; }
        private int? EntityId_Receiver { get; }
        private Entity Entity_Receiver { get; set; }

        override public Point Location => Entity_Receiver != null ? Entity_Receiver.Location : Point.Empty;

        public override IEnumerable<HistoricalFigure> HFsInvolved
        {
            get {
                yield return Hf_Giver;
                yield return Hf_Receiver;
            }
        }
        public override IEnumerable<Entity> EntitiesInvolved
        {
            get
            {
                yield return Entity_Giver;
                yield return Entity_Receiver;
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
                        HfId_Giver = valI;
                        break;
                    case "giver_entity_id":
                        EntityId_Giver = valI;
                        break;
                    case "receiver_hist_figure_id":
                        HfId_Receiver = valI;
                        break;
                    case "receiver_entity_id":
                        EntityId_Receiver = valI;
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
            EventLabel(frm, parent, ref location, "Giver:", Hf_Giver);
            EventLabel(frm, parent, ref location, "Giver Entity:", Entity_Giver);
            EventLabel(frm, parent, ref location, "Receiver:", Hf_Receiver);
            EventLabel(frm, parent, ref location, "Receiver Entity:", Entity_Receiver);
        }

        protected override string LegendsDescription()
        {
            var timestring = base.LegendsDescription();

            return $"{timestring} {Artifact} was offered to {Hf_Receiver} of {Entity_Receiver} by {Hf_Giver}.";
        }

        internal override string ToTimelineString()
        {
            var timelinestring = base.ToTimelineString();

            return timelinestring;
        }
    }
}