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
    class EC_Theft : HistoricalEventCollection
    {
        public int? WarEventCol_ { get; set; }
        public EC_War WarEventCol { get; set; }
        public int? SubregionID { get; set; }
        public Region Subregion { get; set; }
        public int? FeatureLayerID { get; set; }
        public int? SiteID { get; set; }
        public Site Site { get; set; }
        public Point Coords { get; set; }
        public int? ParentEventCol_ { get; set; }
        public HistoricalEventCollection ParentEventCol { get; set; }
        public int Ordinal { get; set; }
        public int? DefendingEnid { get; set; }
        public Entity DefendingEn { get; set; }
        public int? AttackingEnid { get; set; }
        public Entity AttackingEn { get; set; }

        override public Point Location { get { return Site.Coords; } }

        public EC_Theft(XDocument xdoc, World world)
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
                        if (valI != -1)
                            ParentEventCol_ = valI;
                        break;
                    case "subregion_id":
                        if (valI != -1)
                            SubregionID = valI;
                        break;
                    case "feature_layer_id":
                        if (valI != -1)
                            FeatureLayerID = valI;
                        break;
                    case "site_id":
                        if (valI != -1)
                            SiteID = valI;
                        break;
                    case "coords":
                        Coords = new Point(Convert.ToInt32(val.Split(',')[0]), Convert.ToInt32(val.Split(',')[1]));
                        break;
                    case "defending_enid":
                        DefendingEnid = valI;
                        break;
                    case "attacking_enid":
                        AttackingEnid = valI;
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
            if (DefendingEnid.HasValue && World.Entities.ContainsKey(DefendingEnid.Value))
                DefendingEn = World.Entities[DefendingEnid.Value];
            if (AttackingEnid.HasValue && World.Entities.ContainsKey(AttackingEnid.Value))
                AttackingEn = World.Entities[AttackingEnid.Value];

        }

        public override void Select(MainForm frm)
        {
            base.Select(frm);

            foreach (TabPage tabpage in frm.MainTabEventCollectionTypes.TabPages)
            {
                if (tabpage != frm.tabEventCollectionTheft)
                    frm.MainTabEventCollectionTypes.TabPages.Remove(tabpage);
            }
            if (!frm.MainTabEventCollectionTypes.TabPages.Contains(frm.tabEventCollectionTheft))
                frm.MainTabEventCollectionTypes.TabPages.Add(frm.tabEventCollectionTheft);

            frm.lblTheftSite.Data = Site;
            frm.lblTheftCoords.Data = Site != null ? new Coordinate(Site.Coords) : null;
            frm.lblTheftAttacker.Data = AttackingEn;
            frm.lblTheftDefender.Data = DefendingEn;
            frm.lblTheftWar.Data = WarEventCol;
            if (StartTime != null || EndTime != null)
            {
                frm.lblTheftTime.Text = StartTime.ToString() + " - " + EndTime.ToString();
                frm.lblTheftDuration.Text = WorldTime.Duration(EndTime, StartTime);
            }
            else
            {
                frm.lblTheftTime.Text = "";
                frm.lblTheftDuration.Text = "";
            }
            frm.lblTheftOrdinal.Text = Ordinal.ToString();

            frm.lstTheftEvents.Items.Clear();
            if (Event != null)
                frm.lstTheftEvents.Items.AddRange(Event.ToArray());

            frm.grpTheftEvents.Visible = frm.lstTheftEvents.Items.Count > 0;

            if (frm.lstTheftEvents.Items.Count > 0)
                frm.lstTheftEvents.SelectedIndex = 0;

            base.SelectTab(frm);
        }

        internal override void Process()
        {
            base.Process();
            if (Subregion != null)
            {
                if (Subregion.TheftEventCollections == null)
                    Subregion.TheftEventCollections = new List<EC_Theft>();
                Subregion.TheftEventCollections.Add(this);
            }
            if (Site != null)
            {
                if (Site.TheftEventCollections == null)
                    Site.TheftEventCollections = new List<EC_Theft>();
                Site.TheftEventCollections.Add(this);
            }
            if (AttackingEn.TheftEventCollections == null)
                AttackingEn.TheftEventCollections = new List<EC_Theft>();
            AttackingEn.TheftEventCollections.Add(this);
            if (DefendingEn.TheftEventCollections == null)
                DefendingEn.TheftEventCollections = new List<EC_Theft>();
            DefendingEn.TheftEventCollections.Add(this);

        }

        internal override void Export(string table)
        {
            base.Export(table);


            List<object> vals;
            table = this.GetType().Name.ToString();

            vals = new List<object>() { ID, WarEventCol_, ParentEventCol_, Ordinal, AttackingEnid, DefendingEnid, SiteID, SubregionID, FeatureLayerID };

            if (Coords.IsEmpty)
                vals.Add(DBNull.Value);
            else
                vals.Add(Coords.X + "," + Coords.Y);

            Database.ExportWorldItem(table, vals);

        }

    }
}
