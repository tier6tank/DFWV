using System;
using System.Collections.Generic;
using System.Drawing;
using System.Xml.Linq;
using DFWV.Annotations;
using DFWV.WorldClasses.EntityClasses;

namespace DFWV.WorldClasses
{
    public class Army : XMLObject
    {
        private int? EntityId { get; }
        private Entity EntityController { get; set; }

        private int? ItemID { get; }
        public Item ArmyItem { get; set; }
        private int? ItemType { get; }
        private int? ItemSubtype { get; }
        private int? Mat { get; }
        public Point Coords { get; set; }
        override public Point Location => Point.Empty;

        [UsedImplicitly]
        public string DispNameLower => ToString().ToLower();

        public Army(XDocument xdoc, World world)
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
                    case "controller_entity_id":
                        EntityId = valI;
                        break;
                    case "item":
                        if (valI != -1)
                            ItemID = valI;
                        break;
                    case "item_type":
                        if (!Item.ItemTypes.Contains(val))
                            Item.ItemTypes.Add(val);
                        ItemType = Item.ItemTypes.IndexOf(val);
                        break;
                    case "item_subtype":
                        if (!Item.ItemTypes.Contains(val))
                            Item.ItemTypes.Add(val);
                        ItemSubtype = Item.ItemTypes.IndexOf(val);
                        break;
                    case "mat":
                        if (!Item.Materials.Contains(val))
                            Item.Materials.Add(val);
                        Mat = Item.Materials.IndexOf(val);
                        break;
                    case "coords":
                        Coords = new Point(
                            Convert.ToInt32(val.Split(',')[0]),
                            Convert.ToInt32(val.Split(',')[1]));
                        break;
                    default:
                        DFXMLParser.UnexpectedXmlElement(xdoc.Root.Name.LocalName, element, xdoc.Root.ToString());
                        break;
                }
            }
        }

        public override void Select(MainForm frm)
        {
			if (frm.grpArmy.Text == ToString() && frm.MainTab.SelectedTab == frm.tabArmy)
                return;
            Program.MakeSelected(frm.tabArmy, frm.lstArmy, this);

            try
            {
                frm.grpArmy.Text = ToString();
                frm.grpArmy.Show();
#if DEBUG
                frm.grpArmy.Text += $" - ID: {Id}";
#endif
                frm.lblArmyLocation.Text = Coords.ToString();
                frm.lblArmyItem.Data = ArmyItem;
                
                if (ItemType.HasValue)
                {
                    frm.lblArmyItemType.Visible = true;
                    frm.lblArmyItemType.Text = Item.ItemTypes[ItemType.Value];
                }
                else
                    frm.lblArmyItemType.Visible = false;

                if (ItemSubtype.HasValue)
                {
                    frm.lblArmyItemSubtype.Visible = true;
                    frm.lblArmyItemSubtype.Text = Item.ItemTypes[ItemSubtype.Value];
                }
                else
                    frm.lblArmyItemSubtype.Visible = false;

                if (Mat.HasValue)
                {
                    frm.lblArmyMaterial.Visible = true;
                    frm.lblArmyMaterial.Text = Item.Materials[Mat.Value];
                }
                else
                    frm.lblArmyMaterial.Visible = false;


            }
            finally
            {
                Program.MakeSelected(frm.tabArmy, frm.lstArmy, this);
            }
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
            if (ItemID.HasValue)
                ArmyItem = World.Items[ItemID.Value];
            if (EntityId.HasValue && World.Entities.ContainsKey(EntityId.Value))
                EntityController = World.Entities[EntityId.Value];
        }

        internal override void Process()
        {
            
        }

        internal override void Plus(XDocument xdoc)
        {
            
        }
    }


}