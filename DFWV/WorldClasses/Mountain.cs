using System;
using System.Collections.Generic;
using System.Drawing;
using System.Xml.Linq;
using DFWV.Annotations;

namespace DFWV.WorldClasses
{
    public class Mountain : XMLObject
    {

        [UsedImplicitly]
        public string AltName { get; set; }
        public Point Coords { get; set; }
        public int Height { get; set; }

        override public Point Location => Coords;

        public Mountain(XDocument xdoc, World world)
            : base(xdoc, world)
        {
            foreach (var element in xdoc.Root.Elements())
            {
                var val = element.Value.Trim();
                switch (element.Name.LocalName)
                {
                    case "id":
                        break;
                    case "name":
                        Name = val;
                        break;
                    case "name2":
                        AltName = val;
                        break;
                    case "coords":
                        Coords = new Point(
                            Convert.ToInt32(val.Split(',')[0]),
                            Convert.ToInt32(val.Split(',')[1]));
                        break;
                    case "height":
                        Height = Convert.ToInt32(val);
                        break;
                    default:
                        DFXMLParser.UnexpectedXmlElement(xdoc.Root.Name.LocalName, element, xdoc.Root.ToString());
                        break;
                }
            }
        }

        public override void Select(MainForm frm)
        {
            if (frm.grpMountain.Text == ToString() && frm.MainTab.SelectedTab == frm.tabMountain)
                return;
            Program.MakeSelected(frm.tabMountain, frm.lstMountain, this);

            frm.grpMountain.Text = ToString();
            frm.grpMountain.Show();

            frm.lblMountainName.Text = ToString();
            frm.lblMountainAltName.Text = AltName;
            frm.lblMountainCoord.Data = new Coordinate(Coords);
            frm.lblMountainHeight.Text = Height.ToString();
        }


        internal override void Export(string table)
        {

            var vals = new List<object>
            {
                Id, 
                Name.DBExport(), 
                AltName.DBExport(),
                Height
            };

            Database.ExportWorldItem(table, vals);
        }

        internal override void Link()
        {
            
        }

        internal override void Plus(XDocument xdoc)
        {
            
        }
    }


}