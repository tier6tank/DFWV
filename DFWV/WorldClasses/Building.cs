using System.Collections.Generic;
using System.Drawing;
using System.Xml.Linq;

namespace DFWV.WorldClasses
{
    public class Building : XmlObject
    {
        override public Point Location => Point.Empty;

        public Building(XDocument xdoc, World world)
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
                        DfxmlParser.UnexpectedXmlElement(xdoc.Root.Name.LocalName, element, xdoc.Root.ToString());
                        break;
                }
            }
        }

        public override void Select(MainForm frm)
        {
            if (frm.grpBuilding.Text == ToString() && frm.MainTab.SelectedTab == frm.tabBuilding)
                return;
            Program.MakeSelected(frm.tabBuilding, frm.lstBuilding, this);

            //frm.grpBuilding.Text = ToString();
            //frm.grpBuilding.Show();
#if DEBUG
            //frm.grpBuilding.Text += string.Format(" - ID: {0}", ID);
#endif


            //frm.lblBuildingName.Text = ToString();

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