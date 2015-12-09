using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Xml.Linq;
using DFWV.Annotations;

namespace DFWV.WorldClasses
{
    public class Vehicle : XMLObject
    {
        override public Point Location => Point.Empty;

        public Vehicle(XDocument xdoc, World world)
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
            if (frm.grpVehicle.Text == ToString() && frm.MainTab.SelectedTab == frm.tabVehicle)
                return;
            Program.MakeSelected(frm.tabVehicle, frm.lstVehicle, this);


            //frm.grpVehicle.Text = ToString();
            //frm.grpVehicle.Show();
#if DEBUG
            //frm.grpVehicle.Text += string.Format(" - ID: {0}", ID);
#endif


            //frm.lblVehicleName.Text = ToString();

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