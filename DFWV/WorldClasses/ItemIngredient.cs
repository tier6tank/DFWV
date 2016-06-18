using System.Collections.Generic;
using System.Drawing;
using System.Xml.Linq;

namespace DFWV.WorldClasses
{
    public class ItemIngredient : WorldObject
    {
        override public Point Location => Point.Empty;
        public int? Type { get; set; }
        public int? Mat { get; set; }
        public int? Maker { get; set; }


        public ItemIngredient(XDocument xdoc, World world)
            : base(world)
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
                    case "item_type":
                        if (!Item.ItemTypes.Contains(val))
                            Item.ItemTypes.Add(val);
                        Type = Item.ItemTypes.IndexOf(val);
                        break;
                    case "mat":
                        if (!Item.Materials.Contains(val))
                            Item.Materials.Add(val);
                        Mat = Item.Materials.IndexOf(val);
                        break;
                    case "maker":
                        Maker = valI;
                        break;
                    default:
                        DFXMLParser.UnexpectedXmlElement(xdoc.Root.Name.LocalName, element, xdoc.Root.ToString());
                        break;
                }
            }
        }

        public override void Select(MainForm frm)
        {
//            if (frm.grpItem.Text == ToString() && frm.MainTab.SelectedTab == frm.tabItem)
//                return;
//            Program.MakeSelected(frm.tabItem, frm.lstItem, this);

//            frm.grpItem.Text = ToString();
//            frm.grpItem.Show();
//#if DEBUG
//            frm.grpItem.Text += $" - ID: {Id}";
//#endif
//            frm.lblItemDescription.Text = Description;
//            frm.lblItemMaker.Data = Maker;
//            frm.lblItemMasterpieceEvent.Data = MasterpieceEvent;
//            frm.lblItemMat.Text = Mat.HasValue ? Materials[Mat.Value] : "";
//            frm.lblItemType.Text = ItemType.HasValue ? ItemTypes[ItemType.Value] : "";
//            frm.lblItemSubType.Text = ItemSubType.HasValue ? ItemSubTypes[ItemSubType.Value] : "";
//            frm.lblItemName.Text = ToString();
//            frm.lblItemQuality.Text = Quality.ToString();
//            frm.lblItemSkill.Text = SkillUsed.ToString();

        }


        internal override void Export(string table)
        {

            var vals = new List<object>
            {
                Name.DBExport()
            };

            Database.ExportWorldItem(table, vals);
        }

        public override string ToString()
        {
            if (Mat.HasValue && Item.Materials[Mat.Value] == "any" && (Type != null))
                return Item.ItemTypes[Type.Value].ToTitleCase();

            return Mat.HasValue ? Item.Materials[Mat.Value].ToTitleCase(): "Other";
        }
    }


}