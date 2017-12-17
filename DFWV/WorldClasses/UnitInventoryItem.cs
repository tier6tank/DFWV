using System.Collections.Generic;
using System.Drawing;
using System.Xml.Linq;

namespace DFWV.WorldClasses
{
    public class UnitInventoryItem : XMLObject
    {
        public override Point Location { get; }
        public int Mode { get; set; }
        public static List<string> BodyParts = new List<string>();
        public int BodyPart { get; set; }
        public string BodyPartName => BodyParts[BodyPart];

        public UnitInventoryItem(XDocument xdoc, World world)
            : base(xdoc, world)
        {
            foreach (var element in xdoc.Root.Elements())
            {
                var val = element.Value.Trim();
                int valI;
                int.TryParse(val, out valI);
                switch (element.Name.LocalName)
                {
                    case "id":
                        break;
                    case "mode":
                        Mode = valI;
                        break;
                    case "body_part":
                        if (!BodyParts.Contains(val))
                            BodyParts.Add(val);
                        BodyPart = BodyParts.IndexOf(val);
                        break;
                    default:
                        DFXMLParser.UnexpectedXmlElement(xdoc.Root.Name.LocalName, element, xdoc.Root.ToString());
                        break;
                }
            }
        }

        internal override void Export(string table)
        {

            var vals = new List<object>
            {
                Id,
                Name.DBExport()
            };

            Database.ExportWorldItem(table, vals);
        }

        internal override void Link()
        {

        }

        internal override void Plus(XDocument xdoc)
        {

        }

        public override void Select(MainForm frm)
        {
            World.Items[Id]?.Select(frm);
        }

        public override string ToString()
        {
            return $"{BodyPartName.ToTitleCase()} - {World.Items[Id].ToString().ToTitleCase()}";
        }
    }
}