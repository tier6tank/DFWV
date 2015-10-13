using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Xml.Linq;
using DFWV.Annotations;

namespace DFWV.WorldClasses
{
    public class Crime : XMLObject
    {
        override public Point Location { get { return Point.Empty; } }

        public Crime(XDocument xdoc, World world)
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
                        DFXMLParser.UnexpectedXMLElement(xdoc.Root.Name.LocalName, element, xdoc.Root.ToString());
                        break;
                }
            }
        }

        public override void Select(MainForm frm)
        {
            if (frm.grpCrime.Text == ToString() && frm.MainTab.SelectedTab == frm.tabCrime)
                return;
            Program.MakeSelected(frm.tabCrime, frm.lstCrime, this);

            //frm.grpCrime.Text = ToString();
            //frm.grpCrime.Show();
#if DEBUG
            //frm.grpCrime.Text += string.Format(" - ID: {0}", ID);
#endif


            //frm.lblCrimeName.Text = ToString();

        }

        internal override void Export(string table)
        {

            var vals = new List<object>
            {
                ID, 
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