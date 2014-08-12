using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;
using DFWV.WorldClasses.HistoricalFigureClasses;
using DFWV.WorldClasses.HistoricalEventClasses;

namespace DFWV.WorldClasses.HistoricalEventCollectionClasses
{
    class EC_Duel : HistoricalEventCollection
    {
        public int? SubregionID { get; set; }
        public Region Subregion { get; set; }
        public int? FeatureLayerID { get; set; }
        public int? SiteID { get; set; }
        public Site Site { get; set; }
        public Point Coords { get; set; }
        public int? ParentEventCol_ { get; set; }
        public HistoricalEventCollection ParentEventCol { get; set; }
        public int Ordinal { get; set; }
        public List<int> AttackingHFID { get; set; }
        public List<HistoricalFigure> AttackingHF;
        public List<int> DefendingHFID { get; set; }
        public List<HistoricalFigure> DefendingHF;

        override public Point Location { get { return Site != null ? Site.Coords : Coords; } }

        public EC_Duel(XDocument xdoc, World world)
            : base(xdoc, world)
        {
            foreach (XElement element in xdoc.Root.Elements())
            {
                string val = element.Value.ToString();
                int valI;
                Int32.TryParse(val, out valI);

                switch (element.Name.LocalName)
                {
                    case "id":
                    case "start_year":
                    case "start_seconds72":
                    case "end_year":
                    case "end_seconds72":
                    case "event":
                    case "type":
                        break;

                    case "ordinal":
                        Ordinal = valI;
                        break;
                    case "parent_eventcol":
                        ParentEventCol_ = valI;
                        break;
                    case "subregion_id":
                        SubregionID = valI;
                        break;
                    case "feature_layer_id":
                        FeatureLayerID = valI;
                        break;
                    case "site_id":
                        SiteID = valI;
                        break;
                    case "coords":
                        Coords = new Point(Convert.ToInt32(val.Split(',')[0]), Convert.ToInt32(val.Split(',')[1]));
                        break;
                    case "attacking_hfid":
                        if (AttackingHFID == null)
                            AttackingHFID = new List<int>();
                        AttackingHFID.Add(valI);
                        break;
                    case "defending_hfid":
                        if (DefendingHFID == null)
                            DefendingHFID = new List<int>();
                        DefendingHFID.Add(valI);
                        break;

                    default:
                        DFXMLParser.UnexpectedXMLElement(xdoc.Root.Name.LocalName + "\t" + HistoricalEventCollection.Types[Type], element, xdoc.Root.ToString());
                        break;
                }
            }

        }

        internal override void Link()
        {
            base.Link();
            if (ParentEventCol_.HasValue && World.HistoricalEventCollections.ContainsKey(ParentEventCol_.Value))
                ParentEventCol = World.HistoricalEventCollections[ParentEventCol_.Value];
            if (SubregionID.HasValue && World.Regions.ContainsKey(SubregionID.Value))
                Subregion = World.Regions[SubregionID.Value];
            if (SiteID.HasValue && World.Sites.ContainsKey(SiteID.Value))
                Site = World.Sites[SiteID.Value];
            if (AttackingHFID != null)
                AttackingHF = new List<HistoricalFigure>();
            LinkFieldList<HistoricalFigure>(AttackingHFID,
                AttackingHF, World.HistoricalFigures);
            if (DefendingHFID != null)
                DefendingHF = new List<HistoricalFigure>();
            LinkFieldList<HistoricalFigure>(DefendingHFID,
                DefendingHF, World.HistoricalFigures);

        }

        public override void Select(MainForm frm)
        {
            base.Select(frm);

            foreach (TabPage tabpage in frm.MainTabEventCollectionTypes.TabPages)
            {
                if (tabpage != frm.tabEventCollectionDuel)
                    frm.MainTabEventCollectionTypes.TabPages.Remove(tabpage);
            }
            if (!frm.MainTabEventCollectionTypes.TabPages.Contains(frm.tabEventCollectionDuel))
                frm.MainTabEventCollectionTypes.TabPages.Add(frm.tabEventCollectionDuel);

            frm.lblDuelRegion.Data = Subregion;
            frm.lblDuelSite.Data = Site;
            frm.lblDuelCoords.Data = new Coordinate(Coords);
            frm.lblDuelAttacker.Data = AttackingHF != null ? AttackingHF[0] : null;
            frm.lblDuelDefender.Data = DefendingHF != null ? DefendingHF[0] : null;
            frm.lblDuelParent.Data = ParentEventCol;
            if (StartTime != null || EndTime != null)
            {
                frm.lblDuelTime.Text = StartTime.ToString() + " - " + EndTime.ToString();
                frm.lblDuelDuration.Text = WorldTime.Duration(EndTime, StartTime);
            }
            else
            {
                frm.lblDuelTime.Text = "";
                frm.lblDuelDuration.Text = "";
            }
            frm.lblDuelOrdinal.Text = Ordinal.ToString();

            frm.lstDuelEvents.Items.Clear();
            if (Event != null)
                frm.lstDuelEvents.Items.AddRange(Event.ToArray());

            frm.grpDuelEvents.Visible = frm.lstDuelEvents.Items.Count > 0;

            if (frm.lstDuelEvents.Items.Count > 0)
                frm.lstDuelEvents.SelectedIndex = 0;

            base.SelectTab(frm);
        }

        internal override void Process()
        {
            base.Process();
            if (Subregion != null)
            {
                if (Subregion.DuelEventCollections == null)
                    Subregion.DuelEventCollections = new List<EC_Duel>();
                Subregion.DuelEventCollections.Add(this);
            }
            if (Site != null)
            {
                if (Site.DuelEventCollections == null)
                    Site.DuelEventCollections = new List<EC_Duel>();
                Site.DuelEventCollections.Add(this);
            }
            if (AttackingHF != null && AttackingHF.Count != 0)
            {
                if (AttackingHF[0].DuelEventCollections == null)
                    AttackingHF[0].DuelEventCollections = new List<EC_Duel>();
                AttackingHF[0].DuelEventCollections.Add(this);
            }
            if (DefendingHF != null && DefendingHF.Count != 0)
            {
                if (DefendingHF[0].DuelEventCollections == null)
                    DefendingHF[0].DuelEventCollections = new List<EC_Duel>();
                DefendingHF[0].DuelEventCollections.Add(this);
            }

        }

        internal override void Export(string table)
        {
            base.Export(table);


            List<object> vals;
            table = this.GetType().Name.ToString();

            vals = new List<object>() { ID };


            Database.ExportWorldItem(table, vals);

            if (AttackingHF != null)
            {
                foreach (HistoricalFigure hf in AttackingHF)
                {
                    vals = new List<object>() { ID, hf.ID };
                    Database.ExportWorldItem("EC_Duel_Attacking_HF", vals);
                }
            }
            if (DefendingHF != null)
            {
                foreach (HistoricalFigure hf in DefendingHF)
                {
                    vals = new List<object>() { ID, hf.ID };
                    Database.ExportWorldItem("EC_Duel_Defending_HF", vals);
                }
            }


        }

    }
}
