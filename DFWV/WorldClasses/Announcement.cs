using System.Collections.Generic;
using System.Drawing;
using System.Xml.Linq;

namespace DFWV.WorldClasses
{
    public class Announcement : XMLObject
    {
        override public Point Location => Point.Empty;

        public Announcement(XDocument xdoc, World world)
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
            if (frm.grpAnnouncement.Text == ToString() && frm.MainTab.SelectedTab == frm.tabAnnouncement)
                return;
            Program.MakeSelected(frm.tabAnnouncement, frm.lstAnnouncement, this);

            //frm.grpAnnouncement.Text = ToString();
            //frm.grpAnnouncement.Show();
#if DEBUG
            //frm.grpAnnouncement.Text += string.Format(" - ID: {0}", ID);
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