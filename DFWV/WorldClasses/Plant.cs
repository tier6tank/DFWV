using System;
using System.Collections.Generic;
using System.Drawing;
using System.Xml.Linq;
using DFWV.Annotations;

namespace DFWV.WorldClasses
{
    public class Plant : XMLObject
    {
        override public Point Location => Point.Empty;
        public Point3 Coords { get; set; }
        public int? Mat { get; set; }
        [UsedImplicitly]
        public string Material => Mat.HasValue ? Item.Materials[Mat.Value] : "";

        [UsedImplicitly]
        public string DispNameLower => ToString().ToLower();

        public Plant(XDocument xdoc, World world)
            : base(xdoc, world)
        {
            foreach (var element in xdoc.Root.Elements())
            {
                var val = element.Value.Trim();
                switch (element.Name.LocalName)
                {
                    case "id":
                        break;
                    case "coords":
                        Coords = new Point3(
                            Convert.ToInt32(val.Split(',')[0]),
                            Convert.ToInt32(val.Split(',')[1]),
                            Convert.ToInt32(val.Split(',')[2]));
                        break;
                    case "material":
                        if (!Item.Materials.Contains(val))
                            Item.Materials.Add(val);
                        Mat = Item.Materials.IndexOf(val);
                        break;
                    default:
                        DFXMLParser.UnexpectedXmlElement(xdoc.Root.Name.LocalName, element, xdoc.Root.ToString());
                        break;
                }
            }
        }

        public override void Select(MainForm frm)
        {
            if (frm.grpPlant.Text == ToString() && frm.MainTab.SelectedTab == frm.tabPlant)
                return;
            Program.MakeSelected(frm.tabPlant, frm.lstPlant, this);

            frm.grpPlant.Text = ToString();
            frm.grpPlant.Show();
#if DEBUG
            frm.grpPlant.Text += string.Format(" - ID: {0}", Id);
#endif

            frm.lblPlantCoords.Text = Coords.ToString();
            frm.lblPlantMat.Text = Mat.HasValue ? Item.Materials[Mat.Value] : "";
            frm.lblPlantName.Text = ToString();
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

        public override string ToString()
        {
            return Mat.HasValue ? Item.Materials[Mat.Value].ToTitleCase() : base.ToString();
        }

    }


}