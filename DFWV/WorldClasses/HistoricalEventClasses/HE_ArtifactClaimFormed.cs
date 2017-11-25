using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Linq;
using DFWV.WorldClasses.EntityClasses;
using DFWV.WorldClasses.HistoricalFigureClasses;

namespace DFWV.WorldClasses.HistoricalEventClasses
{
    class HE_ArtifactClaimFormed : HistoricalEvent
    {

        private int? ArtifactId { get; }
        private Artifact Artifact { get; set; }
        public int? Claim { get; set; }
        public static List<string> Claims = new List<string>();
        private int? HfId { get; }
        private HistoricalFigure Hf { get; set; }
        private int? EntityId { get; }
        private Entity Entity { get; set; }
        public int PositionProfileId { get; set; }


        override public Point Location => Entity != null ? Entity.Location : Point.Empty;

        public override IEnumerable<HistoricalFigure> HFsInvolved
        {
            get {
                yield return Hf;
            }
        }
        public override IEnumerable<Entity> EntitiesInvolved
        {
            get
            {
                yield return Entity;
            }
        }

        public HE_ArtifactClaimFormed(XDocument xdoc, World world)
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
                    case "claim":
                        if (!Claims.Contains(val))
                            Claims.Add(val);
                        Claim = Claims.IndexOf(val);
                        break;
                    case "hist_figure_id":
                        HfId = valI;
                        break;
                    case "entity_id":
                        EntityId = valI;
                        break;

                    case "position_profile_id":
                        PositionProfileId = valI;
                        break;
                    default:
                        DFXMLParser.UnexpectedXmlElement(xdoc.Root.Name.LocalName + "\t" + Types[Type], element, xdoc.Root.ToString());
                        break;
                }
            }
        }

        protected override void WriteDataOnParent(MainForm frm, Control parent, ref Point location)
        {
            EventLabel(frm, parent, ref location, "Hist Fig:", Hf);
            EventLabel(frm, parent, ref location, "Artifact", Artifact);
            if (Claim.HasValue)
                EventLabel(frm, parent, ref location, "Claim", Claims[Claim.Value]);
        }

        protected override string LegendsDescription()
        {
            var timestring = base.LegendsDescription();

            var claimer = (Hf == null ? Entity?.ToString() : Hf.ToString());
            if (Claim.HasValue)
            {
                switch (Claims[Claim.Value])
                {
                    case "heirloom":
                        return $"{timestring} {Artifact} was made a family heirloom by {claimer}";
                    case "treasure":
                        return $"{timestring} {Artifact} was claimed by {claimer}";
                    case "symbol":
                        return $"{timestring} {Artifact} was made a symbol of the king by {claimer}";
                    default:
                        return $"{timestring} {Artifact} was claimed by {claimer}";
                }
            }
            return $"{timestring} {Artifact} was claimed by {claimer}";

        }

        internal override string ToTimelineString()
        {
            var timelinestring = base.ToTimelineString();

            return timelinestring;
        }
    }
}