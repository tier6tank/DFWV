using System;
using System.Collections.Generic;
using System.Drawing;
using System.Xml.Linq;
using DFWV.Annotations;

namespace DFWV.WorldClasses
{
    public class Landmass : XMLObject
    {

        [UsedImplicitly]
        public string AltName { get; set; }
        public Point CoordMin { get; set; }
        public Point CoordMax { get; set; }
        public Point CoordCenter => new Point((CoordMax.X + CoordMin.X) / 2, (CoordMax.Y + CoordMin.Y) / 2);

        public Rectangle Range
            => new Rectangle(CoordMin.X, CoordMin.Y, CoordMax.X - CoordMin.X + 1, CoordMax.Y - CoordMin.Y + 1);
        public int Area { get; set; }

        override public Point Location => CoordMin;

        public Landmass(XDocument xdoc, World world)
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
                    case "name":
                        Name = val;
                        break;
                    case "coord_1":
                        CoordMin = new Point(
                            Convert.ToInt32(val.Split(',')[0]),
                            Convert.ToInt32(val.Split(',')[1]));
                        break;
                    case "coord_2":
                        CoordMax = new Point(
                            Convert.ToInt32(val.Split(',')[0]),
                            Convert.ToInt32(val.Split(',')[1]));
                        break;
                    case "area":
                        Area = valI;
                        break;
                    default:
                        DFXMLParser.UnexpectedXmlElement(xdoc.Root.Name.LocalName, element, xdoc.Root.ToString());
                        break;
                }
            }
        }

        public override void Select(MainForm frm)
        {
            if (frm.grpLandmass.Text == ToString() && frm.MainTab.SelectedTab == frm.tabLandmass)
                return;
            Program.MakeSelected(frm.tabLandmass, frm.lstLandmass, this);

            frm.grpLandmass.Text = ToString();
            frm.grpLandmass.Show();

            frm.lblLandmassMin.Data = new Coordinate(CoordMin);
            frm.lblLandmassMax.Data = new Coordinate(CoordMax);
            frm.lblLandmassCenter.Data = new Coordinate(CoordCenter);
            frm.lblLandmassArea.Text = Area.ToString();

            frm.lblLandmassName.Text = ToString();
        }


        internal override void Export(string table)
        {

            var vals = new List<object>
            {
                Id, 
                Name.DBExport(), 
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