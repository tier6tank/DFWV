using System.Collections.Generic;
using System.Drawing;
using System.Xml.Linq;

namespace DFWV.WorldClasses
{
    public class Engraving : XMLObject
    {
        override public Point Location => Point.Empty;

        public Engraving(XDocument xdoc, World world)
            : base(xdoc, world)
        {
            foreach (var element in xdoc.Root.Elements())
            {
                var val = element.Value.Trim();
                switch (element.Name.LocalName)
                {
                    case "id":
                        break;
                    default:
                        DFXMLParser.UnexpectedXmlElement(xdoc.Root.Name.LocalName, element, xdoc.Root.ToString());
                        break;
                }
            }
        }

        public override void Select(MainForm frm)
        {
            if (frm.grpEngraving.Text == ToString() && frm.MainTab.SelectedTab == frm.tabEngraving)
                return;
            Program.MakeSelected(frm.tabEngraving, frm.lstEngraving, this);

            //frm.grpEngraving.Text = ToString();
            //frm.grpEngraving.Show();
#if DEBUG
            //frm.grpEngraving.Text += string.Format(" - ID: {0}", ID);
#endif


            //frm.lblEngravingName.Text = ToString();

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

        internal override void Process()
        {
            
        }

        internal override void Plus(XDocument xdoc)
        {
            
        }
    }


}