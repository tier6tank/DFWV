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
    class EC_SiteConquered : HistoricalEventCollection
    {
        public int? WarEventCol_ { get; set; }
        public EC_War WarEventCol { get; set; }
        public int? SiteID { get; set; }
        public Site Site { get; set; }
        public int Ordinal { get; set; }
        public int? DefendingEnid { get; set; }
        public Entity DefendingEn { get; set; }
        public int? AttackingEnid { get; set; }
        public Entity AttackingEn { get; set; }

        override public Point Location { get { return Site.Coords; } }

        public EC_SiteConquered(XDocument xdoc, World world)
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
                    case "site_id":
                        SiteID = valI;
                        break;
                    case "defending_enid":
                        DefendingEnid = valI;
                        break;
                    case "attacking_enid":
                        AttackingEnid = valI;
                        break;
                    case "war_eventcol":
                        WarEventCol_ = valI;
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
            if (SiteID.HasValue && World.Sites.ContainsKey(SiteID.Value))
                Site = World.Sites[SiteID.Value];
            if (DefendingEnid.HasValue && World.Entities.ContainsKey(DefendingEnid.Value))
                DefendingEn = World.Entities[DefendingEnid.Value];
            if (AttackingEnid.HasValue && World.Entities.ContainsKey(AttackingEnid.Value))
                AttackingEn = World.Entities[AttackingEnid.Value];
            if (WarEventCol_.HasValue && World.HistoricalEventCollections.ContainsKey(WarEventCol_.Value))
                WarEventCol = (EC_War)World.HistoricalEventCollections[WarEventCol_.Value];

        }

        public override void Select(MainForm frm)
        {
            base.Select(frm);

            foreach (TabPage tabpage in frm.MainTabEventCollectionTypes.TabPages)
            {
                if (tabpage != frm.tabEventCollectionSiteConquered)
                    frm.MainTabEventCollectionTypes.TabPages.Remove(tabpage);
            }
            if (!frm.MainTabEventCollectionTypes.TabPages.Contains(frm.tabEventCollectionSiteConquered))
                frm.MainTabEventCollectionTypes.TabPages.Add(frm.tabEventCollectionSiteConquered);

            frm.lblSiteConqueredSite.Data = Site;
            frm.lblSiteConqueredCoords.Data = Site != null ? new Coordinate(Site.Coords) : null;
            frm.lblSiteConqueredAttacker.Data = AttackingEn;
            frm.lblSiteConqueredDefender.Data = DefendingEn;
            frm.lblSiteConqueredWar.Data = WarEventCol;
            if (StartTime != null || EndTime != null)
            {
                frm.lblSiteConqueredTime.Text = StartTime.ToString() + " - " + EndTime.ToString();
                frm.lblSiteConqueredDuration.Text = WorldTime.Duration(EndTime, StartTime);
            }
            else
            {
                frm.lblSiteConqueredTime.Text = "";
                frm.lblSiteConqueredDuration.Text = "";
            }
            frm.lblSiteConqueredOrdinal.Text = Ordinal.ToString();

            frm.lstSiteConqueredEvents.Items.Clear();
            if (Event != null)
                frm.lstSiteConqueredEvents.Items.AddRange(Event.ToArray());

            frm.grpSiteConqueredEvents.Visible = frm.lstSiteConqueredEvents.Items.Count > 0;

            if (frm.lstSiteConqueredEvents.Items.Count > 0)
                frm.lstSiteConqueredEvents.SelectedIndex = 0;

            base.SelectTab(frm);
        }

        internal override void Process()
        {
            base.Process();
            if (Site.SiteConqueredEventCollections == null)
                Site.SiteConqueredEventCollections = new List<EC_SiteConquered>();
            Site.SiteConqueredEventCollections.Add(this);
            if (AttackingEn.SiteConqueredEventCollections == null)
                AttackingEn.SiteConqueredEventCollections = new List<EC_SiteConquered>();
            AttackingEn.SiteConqueredEventCollections.Add(this);
            if (DefendingEn.SiteConqueredEventCollections == null)
                DefendingEn.SiteConqueredEventCollections = new List<EC_SiteConquered>();
            DefendingEn.SiteConqueredEventCollections.Add(this);

        }

        internal override void Export(string table)
        {
            base.Export(table);


            List<object> vals;
            table = this.GetType().Name.ToString();

            vals = new List<object>() { ID, WarEventCol.ID, Ordinal, AttackingEnid, DefendingEnid, SiteID};



            Database.ExportWorldItem(table, vals);

        }

    }
}
