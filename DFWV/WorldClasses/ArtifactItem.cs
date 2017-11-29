using System.Drawing;
using System.Xml.Linq;

namespace DFWV.WorldClasses
{

    public class ArtifactItem : XMLObject
    {
        public Artifact Artifact { get; set; }

        public string NameString { get; set; }
        public int PageNumber { get; set; }
        public int? PageWrittenContentId { get; set; }
        public int? WritingWrittenContentId { get; set; }
        public override Point Location => Artifact.Location;


        public ArtifactItem(XElement xml, World world, Artifact artifact)
            : base(world)
        {
            Artifact = artifact;
            foreach (var element in xml.Elements())
            {
                var val = element.Value;
                int valI;
                int.TryParse(val, out valI);
                switch (element.Name.LocalName)
                {
                    case "name_string":
                        NameString = val;
                        break;
                    case "page_number":
                        PageNumber = valI;
                        break;
                    case "page_written_content_id":
                        PageWrittenContentId = valI;
                        break;
                    case "writing_written_content_id":
                        WritingWrittenContentId = valI;
                        break;
                    default:
                        DFXMLParser.UnexpectedXmlElement(xml.Name.LocalName, element, xml.ToString());
                        break;
                }
            }
        }


        internal override void Export(string table)
        {
            throw new System.NotImplementedException();
        }

        internal override void Link()
        {
            throw new System.NotImplementedException();
        }

        internal override void Plus(XDocument xdoc)
        {
            throw new System.NotImplementedException();
        }

        internal override void Process()
        {
            throw new System.NotImplementedException();
        }
    }

}