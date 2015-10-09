using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Xml.Linq;
using DFWV.Annotations;

namespace DFWV.WorldClasses
{
    public class Incident : XMLObject
    {
        override public Point Location { get { return Point.Empty; } }

        public Incident(XDocument xdoc, World world)
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
                //frm.grpIncident.Text = ToString();
                //frm.grpIncident.Show();
#if DEBUG
                //frm.grpIncident.Text += string.Format(" - ID: {0}", ID);
#endif


                //frm.lblIncidentName.Text = ToString();
            }
            finally
            {
                Program.MakeSelected(frm.tabIncident, frm.lstIncident, this);
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