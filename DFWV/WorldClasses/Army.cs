using System.Collections.Generic;
using System.Drawing;
using System.Xml.Linq;

namespace DFWV.WorldClasses
{
    public class Army : XmlObject
    {
        override public Point Location => Point.Empty;

        public Army(XDocument xdoc, World world)
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
            if (frm.grpArmy.Text == ToString() && frm.MainTab.SelectedTab == frm.tabArmy)
                return;
            Program.MakeSelected(frm.tabArmy, frm.lstArmy, this);


            //frm.grpArmy.Text = ToString();
            //frm.grpArmy.Show();
#if DEBUG
            //frm.grpArmy.Text += string.Format(" - ID: {0}", ID);
#endif

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