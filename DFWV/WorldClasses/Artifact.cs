using System.Collections.Generic;
using System.Drawing;
using System.Xml.Linq;
using DFWV.Annotations;
using DFWV.WorldClasses.HistoricalEventClasses;
using DFWV.WorldClasses.HistoricalFigureClasses;

namespace DFWV.WorldClasses
{
    public class Artifact : XMLObject
    {
        public ArtifactItem ArtifactItem { get; set; }

        private int? ItemID { get; set; }
        private int? ItemType { get; set; }
        private int? ItemSubType { get; set; }
        private int? Mat { get; set; }
        private int? MatType { get; set; }
        private int? MatIndex { get; set; }
        private int? ItemValue { get; set; }
        private int? PageCount { get; set; }
        private int? WritingId { get; set; }
        private WrittenContent WrittenContent { get; set; }

        private WrittenContent AssociatedWrittenContent => WrittenContent ?? ArtifactItem.WritingWrittenContent ?? ArtifactItem.PageWrittenContent;

        private int? SiteId { get; set; }
        private Site Site { get; set; }
        private int? StructureLocalId { get; set; }
        public Structure StructureLocal { get; set; }
        private int? HfId_Holder { get; set; }
        private HistoricalFigure Hf_Holder { get; set; }
        private int? AbsTileX { get; set; }
        private int? AbsTileY { get; set; }
        private int? AbsTileZ { get; set; }
        public int? SubregionId { get; set; }
        public Region Subregion { get; private set; }


        public string Description { get; set; }

        public HE_ArtifactCreated CreatedEvent { get; set; }
        public List<HistoricalEvent> ArtifactEvents { get; set; }

        public HE_ArtifactLost LostEvent { get; set; }

        [UsedImplicitly]
        public bool Lost => LostEvent != null;

        [UsedImplicitly]
        public int Value => ItemValue ?? 0;

        [UsedImplicitly]
        public string Type => (ItemSubType.HasValue
            ? Item.ItemSubTypes[ItemSubType.Value]
            : (ItemType.HasValue
                ? Item.ItemTypes[ItemType.Value]
                : ""));

        [UsedImplicitly]
        public string Material => (Mat.HasValue ? Item.Materials[Mat.Value] + " " : "");

        [UsedImplicitly]
        public string DispNameLower => ToString().ToLower();

        override public Point Location => Point.Empty;

        public Artifact(XDocument xdoc, World world)
            : base(xdoc, world)
        {
            foreach (var element in xdoc.Root.Elements())
            {
                var val = element.Value;
                int valI;
                int.TryParse(val, out valI);
                switch (element.Name.LocalName)
                {
                    case "id":
                        break;
                    case "name":
                        Name = val;
                        break;
                    case "item":
                        ArtifactItem = new ArtifactItem(element, world, this);
                        break;
                    case "site_id":
                        SiteId = valI;
                        break;
                    case "structure_local_id":
                        StructureLocalId = valI;
                        break;
                    case "holder_hfid":
                        HfId_Holder = valI;
                        break;
                    case "abs_tile_x":
                        AbsTileX = valI;
                        break;
                    case "abs_tile_y":
                        AbsTileY = valI;
                        break;
                    case "abs_tile_z":
                        AbsTileZ = valI;
                        break;
                    case "subregion_id":
                        if (valI != -1)
                            SubregionId = valI;
                        break;

                    default:
                        DFXMLParser.UnexpectedXmlElement(xdoc.Root.Name.LocalName, element, xdoc.Root.ToString());
                        break;
                }
            }
        }

        public override void Select(MainForm frm)
        {
            if (frm.grpArtifact.Text == ToString() && frm.MainTab.SelectedTab == frm.tabArtifact)
                return;
            Program.MakeSelected(frm.tabArtifact, frm.lstArtifact, this);

            frm.grpArtifact.Text = ToString();
#if DEBUG
            frm.grpArtifact.Text += $" - ID: {Id}";
#endif
            frm.grpArtifact.Show();

            frm.lblArtifactName.Text = Name.ToTitleCase();
            frm.lblArtifactItem.Text = ArtifactItem.ToString();

            frm.lblArtifactDescription.Text = (Mat.HasValue ? Item.Materials[Mat.Value] + " " : "") +
                                              (ItemSubType.HasValue
                                                  ? Item.ItemSubTypes[ItemSubType.Value]
                                                  : (ItemType.HasValue
                                                      ? Item.ItemTypes[ItemType.Value]
                                                      : ""));

            frm.lblArtifactValue.Text = ItemValue?.ToString() ?? "";

            frm.grpArtifactCreated.Visible = CreatedEvent != null;
            if (CreatedEvent != null)
            {
                frm.lblArtifactCreatedBy.Data = CreatedEvent.Hf;
                frm.lblArtifactCreatedSite.Data = CreatedEvent.Site;
                frm.lblArtifactCreatedTime.Data = CreatedEvent;
                frm.lblArtifactCreatedTime.Text = CreatedEvent.Time.ToString();
            }

            frm.grpArtifactLost.Visible = LostEvent != null;
            if (LostEvent != null)
            {
                frm.lblArtifactLostSite.Data = LostEvent.Site;
                frm.lblArtifactLostTime.Data = LostEvent;
                frm.lblArtifactLostTime.Text = LostEvent.Time.ToString();
            }

            frm.grpArtifactEvents.FillListboxWith(frm.lstArtifactEvents, ArtifactEvents);
            frm.lblArtifactWCLabel.Visible = AssociatedWrittenContent != null;

            frm.lblArtifactWC.Data = AssociatedWrittenContent;
            frm.lblArtifactWC.Text = AssociatedWrittenContent == null ? "" : $"{AssociatedWrittenContent}";

            frm.lblArtifactHolder.Data = Hf_Holder;
            frm.lblArtifactSite.Data = Site;
            frm.lblArtifactRegion.Data = Subregion;
        }

        internal override void Link()
        {
            if (WritingId.HasValue && World.WrittenContents.ContainsKey(WritingId.Value))
                WrittenContent = World.WrittenContents[WritingId.Value];
            ArtifactItem?.Link();
            if (SubregionId.HasValue && World.Regions.ContainsKey(SubregionId.Value))
                Subregion = World.Regions[SubregionId.Value];
            if (HfId_Holder.HasValue && World.HistoricalFigures.ContainsKey(HfId_Holder.Value))
                Hf_Holder = World.HistoricalFigures[HfId_Holder.Value];
            if (SiteId.HasValue && World.Sites.ContainsKey(SiteId.Value))
            {
                Site = World.Sites[SiteId.Value];
                if (StructureLocalId.HasValue)
                {
                    StructureLocal = Site.GetStructure(StructureLocalId.Value);
                }
            }
            if (SubregionId.HasValue && World.Regions.ContainsKey(SubregionId.Value))
                Subregion = World.Regions[SubregionId.Value];
            if (SubregionId.HasValue && World.Regions.ContainsKey(SubregionId.Value))
                Subregion = World.Regions[SubregionId.Value];
        }

        internal override void Plus(XDocument xdoc)
        {
            foreach (var element in xdoc.Root.Elements())
            {
                var val = element.Value;
                int valI;
                int.TryParse(val, out valI);

                
                switch (element.Name.LocalName)
                {
                    case "id":
                    case "type":
                        break;
                    case "item_type":
                        if (!Item.ItemTypes.Contains(val))
                            Item.ItemTypes.Add(val);
                        ItemType = Item.ItemTypes.IndexOf(val);
                        break;
                    case "item_subtype":
                        if (valI != -1)
                        {
                            if (!Item.ItemSubTypes.Contains(val))
                                Item.ItemSubTypes.Add(val);
                            ItemSubType = Item.ItemSubTypes.IndexOf(val);
                        }
                        break;
                    case "mattype":
                        MatType = valI;
                        break;
                    case "matindex":
                        MatIndex = valI;
                        break;
                    case "mat":
                        if (!Item.Materials.Contains(val))
                            Item.Materials.Add(val);
                        Mat = Item.Materials.IndexOf(val);
                        break;
                    case "value":
                        ItemValue = valI;
                        break;
                    case "item_description":
                        Description = val;
                        break;
                    case "item_id":
                        if (valI != -1)
                            ItemID = valI;
                        break;
                    case "page_count":
                        if (valI != -1)
                            PageCount = valI;
                        break;
                    case "writing":
                        if (valI != -1)
                            WritingId = valI;
                        break;

                    default:
                        DFXMLParser.UnexpectedXmlElement(xdoc.Root.Name.LocalName + "\t" , element, xdoc.Root.ToString());
                        break;
                }
            }


        }

        internal override void Export(string table)
        {

            var vals = new List<object>
            {
                Id,
                Name.DBExport(),
                ItemID.DBExport(),
                Mat.DBExport(Item.Materials),
                ItemType.DBExport(Item.ItemTypes),
                ItemSubType.DBExport(Item.ItemSubTypes),
                ItemValue.DBExport()
            };


            Database.ExportWorldItem(table, vals);
        }
    }
}