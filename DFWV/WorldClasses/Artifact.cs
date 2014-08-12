using DFWV.Annotations;
using DFWV.WorldClasses.HistoricalEventClasses;
using System;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Drawing;

namespace DFWV.WorldClasses
{
    class Artifact : XMLObject
    {
        private string Item { get; set; }

        private int? ItemType { get; set; }
        private int? ItemSubType { get; set; }
        private int? Mat { get; set; }
        private int? MatType { get; set; }
        private int? MatIndex { get; set; }
        private int? ItemValue { get; set; }


        public HE_ArtifactCreated CreatedEvent { get; set; }
        public List<HE_ArtifactStored> StoredEvents { get; set; }
        public List<HE_ArtifactPossessed> PossessedEvents { get; set; }
        public HE_ArtifactLost LostEvent { get; set; }
        public List<HE_HFDied> Kills { get; set; }

        [UsedImplicitly]
        public bool Lost { get { return LostEvent != null; } }
        [UsedImplicitly]
        public int Value { get { return ItemValue.HasValue ? ItemValue.Value : 0; } }
        [UsedImplicitly]
        public string Type
        {
            get
            {
                return (ItemSubType.HasValue
                    ? HistoricalEvent.ItemSubTypes[ItemSubType.Value]
                    : (ItemType.HasValue
                        ? HistoricalEvent.Items[ItemType.Value]
                        : ""));
            }
        }
        [UsedImplicitly]
        public string Material
        {
            get { return (Mat.HasValue ? HistoricalEvent.Materials[Mat.Value] + " " : ""); }
        }
        [UsedImplicitly]
        public string DispNameLower { get { return ToString().ToLower(); } }

        override public Point Location { get { return Point.Empty; } }

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
                        Item = val;
                        break;
                    default:
                        DFXMLParser.UnexpectedXMLElement(xdoc.Root.Name.LocalName, element, xdoc.Root.ToString());
                        break;
                }
            }
        }

        //public Artifact(NameValueCollection data, World world) 
        //    : base (data, world)
        //{
        //    Name = data["Name"].ToString();
        //}

        public override void Select(MainForm frm)
        {
            frm.grpArtifact.Text = ToString();
#if DEBUG
            frm.grpArtifact.Text += string.Format(" - ID: {0}", ID);
#endif
            frm.grpArtifact.Show();

            frm.lblArtifactName.Text = Name.ToTitleCase();
            frm.lblArtifactItem.Text = Item.ToTitleCase();

            frm.lblArtifactDescription.Text = (Mat.HasValue ? HistoricalEvent.Materials[Mat.Value] + " " : "") +
                                              (ItemSubType.HasValue
                                                  ? HistoricalEvent.ItemSubTypes[ItemSubType.Value]
                                                  : (ItemType.HasValue
                                                      ? HistoricalEvent.Items[ItemType.Value]
                                                      : ""));

            frm.lblArtifactValue.Text = ItemValue.HasValue ? ItemValue.Value.ToString() : "";

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

            frm.grpArtifactStored.Visible = StoredEvents != null;
            if (StoredEvents != null)
            {
                frm.lstArtifactStored.Items.Clear();
                foreach (var evt in StoredEvents)
                {
                    frm.lstArtifactStored.Items.Add(evt);
                }
                frm.lstArtifactStored.SelectedIndex = 0;
            }

            frm.grpArtifactPossessed.Visible = PossessedEvents != null;
            if (PossessedEvents != null)
            {
                frm.lstArtifactPossessed.Items.Clear();
                foreach (var evt in PossessedEvents)
                {
                    frm.lstArtifactPossessed.Items.Add(evt);
                }
                frm.lstArtifactPossessed.SelectedIndex = 0;
            }

            frm.lstArtifactKills.Items.Clear();
            if (Kills != null)
                frm.lstArtifactKills.Items.AddRange(Kills.ToArray());
            frm.grpArtifactKills.Visible = frm.lstArtifactKills.Items.Count > 0;

            Program.MakeSelected(frm.tabArtifact, frm.lstArtifact, this);
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
                Int32.TryParse(val, out valI);

                switch (element.Name.LocalName)
                {
                    case "id":
                    case "type":
                        break;
                    case "item_type":
                        if (!HistoricalEvent.Items.Contains(val))
                            HistoricalEvent.Items.Add(val);
                        ItemType = HistoricalEvent.Items.IndexOf(val);
                        break;
                    case "item_subtype":
                        if (valI != -1)
                        {
                            if (!HistoricalEvent.ItemSubTypes.Contains(val))
                                HistoricalEvent.ItemSubTypes.Add(val);
                            ItemSubType = HistoricalEvent.ItemSubTypes.IndexOf(val);
                        }
                        break;
                    case "mattype":
                        MatType = valI;
                        break;
                    case "matindex":
                        MatIndex = valI;
                        break;
                    case "mat":
                        if (!HistoricalEvent.Materials.Contains(val))
                            HistoricalEvent.Materials.Add(val);
                        Mat = HistoricalEvent.Materials.IndexOf(val);
                        break;
                    case "value":
                        ItemValue = valI;
                        break;
                    default:
                        DFXMLParser.UnexpectedXMLElement(xdoc.Root.Name.LocalName + "\t" , element, xdoc.Root.ToString());
                        break;
                }
            }


        }

        internal override void Export(string table)
        {

            var vals = new List<object> {ID};

            if (Name == null)
                vals.Add(DBNull.Value);
            else
                vals.Add(Name.Replace("'", "''"));

            if (Item == null)
                vals.Add(DBNull.Value);
            else
                vals.Add(Item.Replace("'", "''"));


            Database.ExportWorldItem(table, vals);
        }
    }
}
