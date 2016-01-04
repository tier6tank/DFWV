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
        private int? ItemID { get; set; }
        private Unit Item { get; set; }

        public Vehicle(XDocument xdoc, World world)
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
                    case "item_id":
                        ItemID = valI;
                        break;
                    default:
                        DFXMLParser.UnexpectedXmlElement(xdoc.Root.Name.LocalName, element, xdoc.Root.ToString());
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
                Id, 
                Name.DBExport()
            };

            Database.ExportWorldItem(table, vals);
        }

        internal override void Link()
        {
            //if (ItemID.HasValue)
            //    Item = World.Units[ItemID.Value];
        }

        internal override void Process()
        {
            
        }

        internal override void Plus(XDocument xdoc)
        {
            
        }
    }


}