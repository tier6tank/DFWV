using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Xml.Linq;
using DFWV.Annotations;

namespace DFWV.WorldClasses
{
    public class AdamantineTube : XMLObject
    {
        override public Point Location { get { return Point.Empty; } }

        public AdamantineTube(XDocument xdoc, World world)
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
            try
            {
                //frm.grpAdamantineTube.Text = ToString();
                //frm.grpAdamantineTube.Show();
#if DEBUG
                //frm.grpAdamantineTube.Text += string.Format(" - ID: {0}", ID);
#endif


                //frm.lblAdamantineTubeName.Text = ToString();
            }
            finally
            {
                Program.MakeSelected(frm.tabAdamantineTube, frm.lstAdamantineTube, this);
            }
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