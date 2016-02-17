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
    public enum ItemQuality
    {
        Standard = 0,
        Well_Crafted,
        Finely_Crafted,
        Superior_Quality,
        Exceptional,
        Masterful,
        Artifact
    }

    public class Item : XMLObject
    {
        public static Dictionary<ItemQuality,char> QualityLabels = new Dictionary<ItemQuality, char>()
        { {ItemQuality.Standard, '\0'}, {ItemQuality.Well_Crafted, '-'}, {ItemQuality.Finely_Crafted, '+'},
            { ItemQuality.Superior_Quality, '*'}, { ItemQuality.Exceptional, '\u2261'}, { ItemQuality.Masterful, '\u263C'}, { ItemQuality.Artifact, '\0'} };

        public ItemQuality Quality => (ItemQuality)QualityVal;
        public char QualityLabel => QualityLabels[Quality];

        public char ImprovementQualityLabel
            => Improvements == null ? '\0' : QualityLabels[(ItemQuality)Improvements.Max(i => i.Quality)];

        public string ImprovementPrefix => Improvements == null ? "" : (ImprovementQualityLabel == '\0' ? '\u00AB'.ToString() : new string(new char[] { ImprovementQualityLabel, '\u00AB'}));
        public string ImprovementSuffix => Improvements == null ? "" : (ImprovementQualityLabel == '\0' ? '\u00BB'.ToString() : new string(new char[] { '\u00BB', ImprovementQualityLabel, }));

        override public Point Location => Point.Empty;
        public static List<string> ItemTypes = new List<string>();
        public static List<string> ItemSubTypes = new List<string>();
        public static List<string> Materials = new List<string>();
        public int? ItemTypeId { get; set; }
        [UsedImplicitly]
        public string ItemType => ItemTypeId.HasValue ? ItemTypes[ItemTypeId.Value] : "Unknown";
        public int? ItemSubTypeId { get; set; }
        [UsedImplicitly]
        public string ItemSubType => ItemSubTypeId.HasValue ? ItemSubTypes[ItemSubTypeId.Value] : "";
        [UsedImplicitly]
        public string Material => Mat.HasValue ? Materials[Mat.Value] : "Unknown";
        public string Description;
        public int? Mat { get; set; }
        public int? MakerID { get; set; }
        public HistoricalFigure Maker { get; set; }
        public int QualityVal { get; set; }
        public int? SkillUsed { get; set; }
        public int? MasterpieceEventID { get; set; }
        public HistoricalEvent MasterpieceEvent { get; set; }
        public static List<string> Flags = new List<string>();
        private List<short> Flag { get; set; }
        public double Temperature { get; set; }
        public int? TopicId { get; set; }
        public int? EngravingType { get; set; }
        public string Title { get; set; }
        public Point3 Coords { get; set; }
        public int? Age { get; set; }
        public int? Wear { get; set; }
        public int? Handedness { get; set; }
        public int? Shape { get; set; }
        public int? DyeMat { get; set; }
        public int? UnitId { get; set; }
        public int? HistFigureId { get; set; }
        public int? BoneMat { get; set; }
        public int? Weight { get; set; }
        private List<ItemIngredient> Ingredients { get; set; }
        private List<ItemImprovement> Improvements { get; set; }
        public int? ImageId { get; set; }
        public int? ImageSubId { get; set; }
        public int? ImageCivId { get; set; }
        public int? ImageSiteId { get; set; }
        public int? StockpileId { get; set; }
        public Point StockpileCoords { get; set; }
        public List<Reference> References { get; set; }

        [UsedImplicitly]
        public string DispNameLower => ToString().ToLower();


        [UsedImplicitly]
        public bool IsOnMap => !Coords.IsEmpty;
        [UsedImplicitly]
        public bool IsMadeOnMap => Maker != null;

        [UsedImplicitly] //TODO REVIEW
        public bool IsArtifact => References?.Any(r => r.Type == ReferenceType.IS_ARTIFACT) ?? false; 
        [UsedImplicitly]
        public bool IsHeld => References?.Any(r => r.Type == ReferenceType.UNIT_HOLDER) ?? false; 
        [UsedImplicitly]
        public bool IsOwned => References?.Any(r => r.Type == ReferenceType.UNIT_ITEMOWNER) ?? false; 
        [UsedImplicitly]
        public bool InContainer => References?.Any(r => r.Type == ReferenceType.CONTAINED_IN_ITEM) ?? false; 
        [UsedImplicitly]
        public bool InBuilding => References?.Any(r => r.Type == ReferenceType.BUILDING_HOLDER) ?? false;

        public Item(XDocument xdoc, World world)
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
                    case "type":
                        if (!ItemTypes.Contains(val))
                            ItemTypes.Add(val);
                        ItemTypeId = ItemTypes.IndexOf(val);
                        break;
                    case "subtype":
                        if (valI != -1)
                        {
                            if (!ItemSubTypes.Contains(val))
                                ItemSubTypes.Add(val);
                            ItemSubTypeId = ItemSubTypes.IndexOf(val);
                        }
                        break;
                    case "description":
                        Description = val;
                        break;
                    case "mat":
                        if (!Materials.Contains(val))
                            Materials.Add(val);
                        Mat = Materials.IndexOf(val);
                        break;
                    case "maker":
                        if (valI != -1)
                            MakerID = valI;
                        break;
                    case "quality":
                        if (valI != -1)
                            QualityVal = valI;
                        break;
                    case "skill_used":
                        if (valI != -1)
                            SkillUsed = valI;
                        break;
                    case "masterpiece_event":
                        if (valI != -1)
                            MasterpieceEventID = valI;
                        break;
                    case "flags":
                        var flags = val.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                        foreach (var flag in flags)
                        {
                            if (!Flags.Contains(flag))
                                Flags.Add(flag);
                            if (Flag == null)
                                Flag = new List<short>();
                            Flag.Add((short)Flags.IndexOf(flag));
                        }
                        break;
                    case "coords":
                        Coords = new Point3(
                            Convert.ToInt32(val.Split(',')[0]),
                            Convert.ToInt32(val.Split(',')[1]),
                            Convert.ToInt32(val.Split(',')[2]));
                        break;
                    case "temperature":
                        Temperature = Convert.ToDouble(val);
                        break;
                    case "topic":
                        if (valI != -1)
                            TopicId = valI;
                        break;
                    case "engraving_type":
                        if (valI != -1)
                            EngravingType = valI;
                        break;
                    case "title":
                        Title = val;
                        break;
                    case "age":
                        if (valI != -1)
                            Age = valI;
                        break;
                    case "wear":
                        if (valI != -1)
                            Wear = valI;
                        break;
                    case "handedness":
                        if (val.Count(x => x == ',') == 1)
                            Handedness = Convert.ToInt32(val.TrimEnd(','));
                        break;
                    case "shape":
                        if (valI != -1)
                            Shape = valI;
                        break;
                    case "dye_mat":
                        if (!Materials.Contains(val))
                            Materials.Add(val);
                        DyeMat = Materials.IndexOf(val);
                        break;
                    case "unit_id":
                        if (valI != -1)
                            UnitId = valI;
                        break;
                    case "hist_figure_id":
                        if (valI != -1)
                            HistFigureId = valI;
                        break;
                    case "bone1":
                        if (!Materials.Contains(val))
                            Materials.Add(val);
                        BoneMat = Materials.IndexOf(val);
                        break;
                    case "weight":
                        if (valI != -1)
                            Weight = valI;
                        break;
                    case "ingredients":
                        foreach (var inv in element.Elements())
                        {
                            if (Ingredients == null)
                                Ingredients = new List<ItemIngredient>();
                            Ingredients.Add(new ItemIngredient(new XDocument(inv), world));
                        }
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
                    case "reference":
                        if (References == null)
                            References = new List<Reference>();
                        References.Add(new Reference(element, this));
                        break;
                    case "improvements":
                        foreach (var imp in element.Elements())
                        {
                            if (Improvements == null)
                                Improvements = new List<ItemImprovement>();
                            Improvements.Add(new ItemImprovement(new XDocument(imp), world));
                        }
                        break;
                    case "stockpile":
                        StockpileId = Convert.ToInt32(element.Element("id").Value);
                        StockpileCoords = new Point(
                            Convert.ToInt32(element.Element("x").Value),
                            Convert.ToInt32(element.Element("y").Value));
                        break;
                    default:
                        DFXMLParser.UnexpectedXmlElement(xdoc.Root.Name.LocalName, element, xdoc.Root.ToString());
                        break;
                }
            }
            Name = $"{(Mat.HasValue ? Materials[Mat.Value] + " " : "")}{(ItemSubTypeId.HasValue ? ItemSubTypes[ItemSubTypeId.Value] + " " : ItemTypes[ItemTypeId.Value])}"
                    .Trim().ToTitleCase();
        }

        public override void Select(MainForm frm)
        {
            if (frm.grpItem.Text == ToString() && frm.MainTab.SelectedTab == frm.tabItem)
                return;
            Program.MakeSelected(frm.tabItem, frm.lstItem, this);

            frm.grpItem.Text = ToString();
            frm.grpItem.Show();
#if DEBUG
            frm.grpItem.Text += $" - ID: {Id}";
#endif
            frm.lblItemDescription.Text = Description;
            frm.lblItemCoords.Text = Coords.IsEmpty ? "Off Map" : Coords.ToString();
            frm.lblItemMaker.Data = Maker;
            frm.lblItemMasterpieceEvent.Data = MasterpieceEvent;
            frm.lblItemMat.Text = Mat.HasValue ? Materials[Mat.Value].ToTitleCase() : "";
            frm.lblItemType.Text = ItemTypeId.HasValue ? ItemTypes[ItemTypeId.Value].ToTitleCase() : "";
            frm.lblItemSubType.Text = ItemSubTypeId.HasValue ? ItemSubTypes[ItemSubTypeId.Value].ToTitleCase() : "";
            frm.lblItemName.Text = Name;
            frm.lblItemQuality.Text = Quality.ToString();
            frm.lblItemWeight.Text = Weight.ToString();
            frm.lblItemSkill.Text = SkillUsed.ToString();
            var AgeTime = new WorldTime(0, Age).ToString().Split('.').Select(x => Convert.ToInt32(x)).ToArray();

            if (AgeTime[2] > 0)
                frm.lblItemAge.Text = AgeTime[2] + " year" + (AgeTime[2] > 0 ? "s" : "");
            else if (AgeTime[1] > 1)
                frm.lblItemAge.Text = AgeTime[1] - 1 + " month" + (AgeTime[1] > 1 ? "s" : "");
            else if (AgeTime[0] > 1)
                frm.lblItemAge.Text = AgeTime[0] - 1 + " day" + (AgeTime[0] > 1 ? "s" : "");
            else if (Age.HasValue)
                frm.lblItemAge.Text = Age + " seconds";
            else
                frm.lblItemAge.Text = "";

            frm.lblItemWear.Text = Wear.ToString();
            frm.lblItemHand.Text = Handedness.ToString();
            frm.lblItemShape.Text = Shape.ToString();
            frm.lblItemDyeMat.Text = DyeMat.HasValue ? Materials[DyeMat.Value] : "";
            frm.lblItemUnit.Data = UnitId.HasValue && World.Units.ContainsKey(UnitId.Value)
                ? World.Units[UnitId.Value]
                : null;
            frm.lblItemHF.Data = HistFigureId.HasValue && World.HistoricalFigures.ContainsKey(HistFigureId.Value)
                ? World.HistoricalFigures[HistFigureId.Value]
                : null;
            frm.lblItemBoneMat.Text = BoneMat.HasValue ? Materials[BoneMat.Value] : "";

            frm.lblItemStockpile.Text = StockpileId.ToString();
            frm.lblItemStockpileCoords.Text = StockpileCoords.IsEmpty ? "" : StockpileCoords.ToString();

            frm.grpItemImage.Visible = ImageId.HasValue;
            if (ImageId.HasValue)
            {
                frm.lblItemImageID.Text = ImageId.ToString();
                frm.lblItemImageSubID.Text = ImageSubId.ToString();
                frm.lblItemImageCiv.Data = ImageCivId.HasValue && World.Entities.ContainsKey(ImageCivId.Value)
                    ? World.Entities[ImageCivId.Value]
                    : null;
                frm.lblItemImageSIte.Data = ImageSiteId.HasValue && World.Sites.ContainsKey(ImageSiteId.Value)
                    ? World.Sites[ImageSiteId.Value]
                    : null;

            }

            frm.grpItemIngredient.FillListboxWith(frm.lstItemIngredient, Ingredients);
            frm.grpItemImprovement.FillListboxWith(frm.lstItemImprovement, Improvements);
            frm.grpItemReferences.FillListboxWith(frm.lstItemReferences, References);

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
            if (MakerID.HasValue && World.HistoricalFigures.ContainsKey(MakerID.Value))
                Maker = World.HistoricalFigures[MakerID.Value];
            if (MasterpieceEventID.HasValue)
                MasterpieceEvent = World.HistoricalEvents[MasterpieceEventID.Value];
            References?.ForEach(x => x.Link());
            if (IsArtifact)
                QualityVal = 6;
        }

        internal override void Process()
        {
            
        }

        internal override void Plus(XDocument xdoc)
        {
            
        }

        public override string ToString()
        {
            if (IsArtifact)
                return ((Artifact) References.First(r => r.Type == ReferenceType.IS_ARTIFACT).refObject).ToString();
            return QualityLabel == '\0' ? Name : $"{ImprovementPrefix}{QualityLabel}{Name}{QualityLabel}{ImprovementSuffix}";
        }
    }


}