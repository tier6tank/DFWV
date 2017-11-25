using DFWV.WorldClasses.EntityClasses;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Linq;

namespace DFWV.WorldClasses.HistoricalEventClasses
{
    internal class HE_ArtifactCopied : HistoricalEvent
    {
        private int? ArtifactId { get; }
        private Artifact Artifact { get; set; }
        private int? SiteId_Destination { get; }
        private Site Site_Destination { get; set; }
        private int? StructureId_Destination { get; set; }
        public Structure Structure_Destination { get; set; }
        private int? EntityId_Destination { get; set; }
        public Entity Entity_Destination { get; set; }
        private int? SiteId_Source { get; set; }
        private Site Site_Source { get; set; }
        private int? StructureId_Source { get; set; }
        public Structure Structure_Source { get; set; }
        private int? EntityId_Source { get; set; }
        public Entity Entity_Source { get; set; }
        public bool CopiedFromOriginal { get; set; }

        override public Point Location => Site_Source.Location;

        public override IEnumerable<Site> SitesInvolved
        {
            get
            {
                yield return Site_Destination;
                yield return Site_Source;
            }
        }

        public override IEnumerable<Entity> EntitiesInvolved
        {
            get
            {
                yield return Entity_Destination;
                yield return Entity_Source;
            }
        }


        public HE_ArtifactCopied(XDocument xdoc, World world) : base(xdoc, world)
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
                    case "dest_site_id":
                        SiteId_Destination = valI;
                        break;
                    case "dest_structure_id":
                        StructureId_Destination = valI;
                        break;
                    case "dest_entity_id":
                        EntityId_Destination = valI;
                        break;
                    case "source_site_id":
                        SiteId_Source = valI;
                        break;
                    case "source_structure_id":
                        StructureId_Source = valI;
                        break;
                    case "source_entity_id":
                        EntityId_Source = valI;
                        break;
                    case "from_original":
                        CopiedFromOriginal = true;
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

            if (StructureId_Destination.HasValue && StructureId_Destination.Value != -1 && Site_Destination != null)
                Structure_Destination = Site_Destination.GetStructure(StructureId_Destination.Value);
            if (StructureId_Source.HasValue && StructureId_Source.Value != -1 && Site_Source != null)
                Structure_Source = Site_Source.GetStructure(StructureId_Source.Value);
        }

        protected override void WriteDataOnParent(MainForm frm, Control parent, ref Point location)
        {
            EventLabel(frm, parent, ref location, "Artifact:", Artifact);
            EventLabel(frm, parent, ref location, "Destination:", "");
            EventLabel(frm, parent, ref location, " Entity:", Entity_Destination);
            EventLabel(frm, parent, ref location, " Structure:", Structure_Destination);
            EventLabel(frm, parent, ref location, " Site:", Site_Destination);
            EventLabel(frm, parent, ref location, "Source:", "");
            EventLabel(frm, parent, ref location, " Entity:", Entity_Source);
            EventLabel(frm, parent, ref location, " Structure:", Structure_Source);
            EventLabel(frm, parent, ref location, " Site:", Site_Source);

        }

        protected override string LegendsDescription()
        {
            var timestring = base.LegendsDescription();

            return $"{timestring} {Entity_Destination} made a copy of the original {Artifact} from {Structure_Source} in {Site_Source.AltName} of {Entity_Source},\n keeping it within {Structure_Destination} in {Site_Destination.AltName}.";
        }

        internal override string ToTimelineString()
        {
            var timelinestring = base.ToTimelineString();

            return timelinestring;
        }
    }
}