using System;
using System.Collections.Generic;
using System.Drawing;
using System.Xml.Linq;
using DFWV.Annotations;

namespace DFWV.WorldClasses
{
    public class Construction : XMLObject
    {
        private Point3 Coords { get; }
        override public Point Location => Point.Empty;
        private int? ItemTypeId { get; }
        public string ItemType => ItemTypeId.HasValue ? Item.ItemTypes[ItemTypeId.Value] : "";
        private int? Mat { get; }
        [UsedImplicitly]
        public string Material => Mat.HasValue ? Item.Materials[Mat.Value] : "";

        [UsedImplicitly]
        public string DispNameLower => ToString().ToLower();

        public Construction(XDocument xdoc, World world)
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
                    case "item_type":
                        if (!Item.ItemTypes.Contains(val))
                            Item.ItemTypes.Add(val);
                        ItemTypeId = Item.ItemTypes.IndexOf(val);
                        break;
                    case "mat":
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
            if (frm.grpConstruction.Text == ToString() && frm.MainTab.SelectedTab == frm.tabConstruction)
                return;
            Program.MakeSelected(frm.tabConstruction, frm.lstConstruction, this);


            frm.grpConstruction.Text = ToString();
            frm.grpConstruction.Show();
#if DEBUG
            frm.grpConstruction.Text += $" - ID: {Id}";
#endif


            frm.grpConstruction.Text = ToString();
            frm.lblConstructionName.Text = ToString();
            frm.lblConstructionCoords.Text = Coords.ToString();
            frm.lblConstructionItemType.Text = ItemTypeId.HasValue ? Item.ItemTypes[ItemTypeId.Value] : "";
            frm.lblConstructionMat.Text = Mat.HasValue ? Item.Materials[Mat.Value] : "";
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

        internal override void Plus(XDocument xdoc)
        {
            
        }

        public override string ToString()
        {
            return
                $"{(ItemTypeId.HasValue ? Item.ItemTypes[ItemTypeId.Value] : "")}{(Mat.HasValue ? " " + Item.Materials[Mat.Value] : "")}"
                    .ToTitleCase();
        }
    }


}