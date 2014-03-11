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
    class EC_BeastAttack : HistoricalEventCollection
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
        public List<int> EventCol_ { get; set; }
        public List<HistoricalEventCollection> EventCol { get; set; }
        public int? DefendingEnid { get; set; }
        public Entity DefendingEn { get; set; }
        public HistoricalFigure BeastHF { get; set; }

        override public Point Location { get { return Site.Coords; } }

        public EC_BeastAttack(XDocument xdoc, World world)
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
                    case "eventcol":
                        if (EventCol_ == null)
                            EventCol_ = new List<int>();
                        EventCol_.Add(valI);
                        break;
                    case "defending_enid":
                        DefendingEnid = valI;
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

            if (EventCol_ != null)
                EventCol = new List<HistoricalEventCollection>();
            LinkFieldList<HistoricalEventCollection>(EventCol_,
                EventCol, World.HistoricalEventCollections);

        }

        public override void Select(MainForm frm)
        {
            base.Select(frm);

            foreach (TabPage tabpage in frm.MainTabEventCollectionTypes.TabPages)
            {
                if (tabpage != frm.tabEventCollectionBeastAttack)
                    frm.MainTabEventCollectionTypes.TabPages.Remove(tabpage);
            }
            if (!frm.MainTabEventCollectionTypes.TabPages.Contains(frm.tabEventCollectionBeastAttack))
                frm.MainTabEventCollectionTypes.TabPages.Add(frm.tabEventCollectionBeastAttack);

            frm.lblBeastAttackRegion.Data = Subregion;
            frm.lblBeastAttackSite.Data = Site;
            frm.lblBeastAttackCoords.Data = new Coordinate(Coords);
            frm.lblBeastAttackDefender.Data = DefendingEn;
            frm.lblBeastAttackParent.Data = ParentEventCol;
            frm.lblBeastAttackBeast.Data = BeastHF;

            if (StartTime != null || EndTime != null)
            {
                frm.lblBeastAttackTime.Text = StartTime.ToString() + " - " + EndTime.ToString();
                frm.lblBeastAttackDuration.Text = WorldTime.Duration(EndTime, StartTime);
            }
            else
            {
                frm.lblBeastAttackTime.Text = "";
                frm.lblBeastAttackDuration.Text = "";
            }
            frm.lblBeastAttackOrdinal.Text = Ordinal.ToString();

            frm.lstBeastAttackEvents.Items.Clear();
            if (Event != null)
                frm.lstBeastAttackEvents.Items.AddRange(Event.ToArray());

            frm.grpBeastAttackEvents.Visible = frm.lstBeastAttackEvents.Items.Count > 0;

            if (frm.lstBeastAttackEvents.Items.Count > 0)
                frm.lstBeastAttackEvents.SelectedIndex = 0;

            base.SelectTab(frm);
        }

        internal override void Process()
        {
            base.Process();
            if (Site.BeastAttackEventCollections == null)
                Site.BeastAttackEventCollections = new List<EC_BeastAttack>();
            Site.BeastAttackEventCollections.Add(this);
            if (DefendingEn.BeastAttackEventCollections == null)
                DefendingEn.BeastAttackEventCollections = new List<EC_BeastAttack>();
            DefendingEn.BeastAttackEventCollections.Add(this);
        }

        internal override void Export(string table)
        {
            base.Export(table);


            List<object> vals;
            table = this.GetType().Name.ToString();

            vals = new List<object>() { ID, ParentEventCol, Ordinal, BeastHF == null ? (object)DBNull.Value : BeastHF.ID, DefendingEnid, SiteID, SubregionID, FeatureLayerID };

            if (Coords.IsEmpty)
                vals.Add(DBNull.Value);
            else
                vals.Add(Coords.X + "," + Coords.Y);

            Database.ExportWorldItem(table, vals);

            if (EventCol == null)
                return;
            table = "EC_EventCols";
            foreach (HistoricalEventCollection evtcol in EventCol)
            {
                vals = new List<object>() { ID, evtcol.ID };
                Database.ExportWorldItem(table, vals);

            }

        }

    }
}
