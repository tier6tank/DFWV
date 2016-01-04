using System.Collections.Generic;
using System.Drawing;
using System.Xml.Linq;
using DFWV.Annotations;
using DFWV.WorldClasses.HistoricalEventClasses;

namespace DFWV.WorldClasses
{
    public class Artifact : XMLObject
    {
        public string ItemName { get; set; }

        private int? ItemID { get; set; }
        private int? ItemType { get; set; }
        private int? ItemSubType { get; set; }
        private int? Mat { get; set; }
        private int? MatType { get; set; }
        private int? MatIndex { get; set; }
        private int? ItemValue { get; set; }

        public string Description { get; set; }

        public HE_ArtifactCreated CreatedEvent { get; set; }
        public List<HE_ArtifactStored> StoredEvents { get; set; }
        public List<HE_ArtifactPossessed> PossessedEvents { get; set; }
        public HE_ArtifactLost LostEvent { get; set; }
        public List<HE_HFDied> Kills { get; set; }

        [UsedImplicitly]
        public bool Lost => LostEvent != null;

        [UsedImplicitly]
        public int Value => ItemValue ?? 0;

        [UsedImplicitly]
        public string Type => (ItemSubType.HasValue
            ? HistoricalEvent.ItemSubTypes[ItemSubType.Value]
            : (ItemType.HasValue
                ? HistoricalEvent.ItemTypes[ItemType.Value]
                : ""));

        [UsedImplicitly]
        public string Material => (Mat.HasValue ? HistoricalEvent.Materials[Mat.Value] + " " : "");

        [UsedImplicitly]
        public string DispNameLower => ToString().ToLower();

        override public Point Location => Point.Empty;

        public Artifact(XDocument xdoc, World world)
            : base(xdoc, world)
        {
            foreach (var element in xdoc.Root.Elements())
            {
                var val = element.Value;
                switch (element.Name.LocalName)
                {
                    case "id":
                        break;
                    case "name":
                        Name = val;
                        break;
                    case "item":
                        ItemName = val;
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
            frm.lblArtifactItem.Text = ItemName.ToTitleCase();

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
                frm.lblArtifactCreatedBy.Data = CreatedEvent.HistFigure;
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

            frm.grpArtifactStored.FillListboxWith(frm.lstArtifactStored, StoredEvents);
            frm.grpArtifactPossessed.FillListboxWith(frm.lstArtifactPossessed, PossessedEvents);
            frm.grpArtifactKills.FillListboxWith(frm.lstArtifactKills, Kills);

        }

        internal override void Link()
        {

        }

        internal override void Process()
        {

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
                Item.DBExport(),
                Mat.DBExport(HistoricalEvent.Materials),
                ItemType.DBExport(HistoricalEvent.ItemTypes),
                ItemSubType.DBExport(HistoricalEvent.ItemSubTypes),
                ItemValue.DBExport()
            };


            Database.ExportWorldItem(table, vals);
        }
    }
}