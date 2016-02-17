using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Xml.Linq;
using DFWV.Annotations;
using DFWV.WorldClasses.HistoricalEventClasses;
using DFWV.WorldClasses.HistoricalFigureClasses;

namespace DFWV.WorldClasses
{
    public class ItemImprovement : WorldObject
    {
        public ItemQuality Quality => (ItemQuality)QualityVal;
        public char QualityLabel => Item.QualityLabels[Quality];

        override public Point Location => Point.Empty;
        public static List<string> ImprovementTypes = new List<string>(); 
        public int ImprovementType { get; set; }
        public string ImprovementTypeName => ImprovementTypes[ImprovementType];
        public int? Mat { get; set; }
        public int? Maker { get; set; }
        public int? Count { get; set; }
        public int? Contents { get; set; }
        public int? SkillRating { get; set; }
        public int QualityVal { get; set; }
        public int? Shape { get; set; }
        public int? MasterpieceEventId { get; set; }
        public int? ImageId { get; set; }
        public int? ImageSubId { get; set; }
        public int? ImageCivId { get; set; }
        public int? ImageSiteId { get; set; }
        public int? DyeMat { get; set; }
        public int? Dyer { get; set; }
        public int? DyeQuality { get; set; }
        public int? DyeSkillRating { get; set; }
        public int? Type { get; set; }

        public ItemImprovement(XDocument xdoc, World world)
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
                    case "improvement_type":
                        if (!ImprovementTypes.Contains(val))
                            ImprovementTypes.Add(val);
                        ImprovementType = ImprovementTypes.IndexOf(val);
                        break;
                    case "mat":
                        if (!Item.Materials.Contains(val))
                            Item.Materials.Add(val);
                        Mat = Item.Materials.IndexOf(val);
                        break;
                    case "maker":
                        Maker = valI;
                        break;
                    case "type":
                        Type = valI;
                        break;
                    case "count":
                        Count = valI;
                        break;
                    case "contents":
                        Contents = valI;
                        break;
                    case "skill_rating":
                        SkillRating = valI;
                        break;
                    case "quality":
                        QualityVal = valI;
                        break;
                    case "shape":
                        Shape = valI;
                        break;
                    case "masterpiece_event":
                        MasterpieceEventId = valI;
                        break;
                    case "image":
                        if (!ImageId.HasValue)
                        {
                            ImageId = Convert.ToInt32(element.Element("id").Value);
                            ImageSubId = Convert.ToInt32(element.Element("subid").Value);
                            ImageCivId = Convert.ToInt32(element.Element("civ_id").Value);
                            ImageSiteId = Convert.ToInt32(element.Element("site_id").Value);
                        }
                        break;
                    case "dye":
                        if (!DyeMat.HasValue)
                        {
                            if (element.Element("mat") != null)
                            {
                                if (!Item.Materials.Contains(element.Element("mat").Value))
                                    Item.Materials.Add(element.Element("mat").Value);
                                DyeMat = Item.Materials.IndexOf(element.Element("mat").Value);
                            }
                            if (element.Element("dyer") != null)
                                Dyer = Convert.ToInt32(element.Element("dyer").Value);
                            if (element.Element("quality") != null)
                                DyeQuality = Convert.ToInt32(element.Element("quality").Value);
                            if (element.Element("skill_rating") != null)
                                DyeSkillRating = Convert.ToInt32(element.Element("skill_rating").Value);
                        }
                        break;
                    default:
                        DFXMLParser.UnexpectedXmlElement(xdoc.Root.Name.LocalName, element, xdoc.Root.ToString());
                        break;
                }
            }
            Name = ((Mat.HasValue ? Item.Materials[Mat.Value] + " " : "") + ImprovementTypeName).ToTitleCase();
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
            return QualityLabel == '\0' ? Name : $"{QualityLabel}{Name}{QualityLabel}";
        }
    }


}