using DFWV.WorldClasses.HistoricalEventClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Drawing;
using System.Collections.Specialized;

namespace DFWV.WorldClasses
{
    class Artifact : XMLObject
    {
        public string Item { get; set; }

        public HE_ArtifactCreated CreatedEvent { get; set; }
        public List<HE_ArtifactStored> StoredEvents { get; set; }
        public List<HE_ArtifactLost> PossessedEvents { get; set; }
        public List<HE_HFDied> Kills { get; set; }
        public HE_ArtifactLost LostEvent { get; set; }

        public bool Lost { get { return CreatedEvent == null; } }
        public string DispNameLower { get { return ToString().ToLower(); } }

        override public Point Location { get { return Point.Empty; } }

        public Artifact(XDocument xdoc, World world)
            : base(xdoc, world)
        {
            foreach (XElement element in xdoc.Root.Elements())
            {
                string val = element.Value.ToString();
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

        public override string ToString()
        {
            if (base.ToString() != null)
                return base.ToString();
            else
                return Item.ToString();
        }
        public override void Select(MainForm frm)
        {
            frm.grpArtifact.Text = this.ToString();
            frm.grpArtifact.Show();

            frm.lblArtifactName.Text = Name;
            frm.lblArtifactItem.Text = Item;


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

        internal override void Export(string table)
        {

            List<object> vals = new List<object>();

            vals.Add(ID);

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
