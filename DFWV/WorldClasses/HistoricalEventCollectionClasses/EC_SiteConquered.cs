using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;
using DFWV.WorldClasses.EntityClasses;
using DFWV.WorldClasses.HistoricalEventClasses;
using System.Collections.Concurrent;

namespace DFWV.WorldClasses.HistoricalEventCollectionClasses
{
    public class EC_SiteConquered : HistoricalEventCollection
    {
        private int? WarEventCol_ { get; }
        private EC_War WarEventCol { get; set; }
        private int? SiteId { get; }
        private Site Site { get; set; }
        private int Ordinal { get; }
        private int? DefendingEnid { get; }
        private Entity DefendingEn { get; set; }
        private int? AttackingEnid { get; }
        public Entity AttackingEn { get; private set; }

        override public Point Location => Site.Coords;

        public EC_SiteConquered(XDocument xdoc, World world)
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
                        SiteId = valI;
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
                        DFXMLParser.UnexpectedXmlElement(xdoc.Root.Name.LocalName + "\t" + Types[Type], element, xdoc.Root.ToString());
                        break;
                }
            }

        }

        internal override void Link()
        {
            base.Link();
            if (SiteId.HasValue && World.Sites.ContainsKey(SiteId.Value))
                Site = World.Sites[SiteId.Value];
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

            foreach (var tabpage in frm.MainTabEventCollectionTypes.TabPages.Cast<TabPage>().Where(tabpage => tabpage != frm.tabEventCollectionSiteConquered))
            {
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
                frm.lblSiteConqueredTime.Text = $"{StartTime} - {EndTime}";
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

            SelectTab(frm);
        }

        public override void Process()
        {
            base.Process();
            if (Site.SiteConqueredEventCollections == null)
                Site.SiteConqueredEventCollections = new ConcurrentBag<EC_SiteConquered>();
            Site.SiteConqueredEventCollections.Add(this);
            if (AttackingEn.SiteConqueredEventCollections == null)
                AttackingEn.SiteConqueredEventCollections = new ConcurrentBag<EC_SiteConquered>();
            AttackingEn.SiteConqueredEventCollections.Add(this);
            if (DefendingEn != null)
            {
                if (DefendingEn.SiteConqueredEventCollections == null)
                    DefendingEn.SiteConqueredEventCollections = new ConcurrentBag<EC_SiteConquered>();
                DefendingEn.SiteConqueredEventCollections.Add(this);
            }
        }

        internal override void Evaluate()
        {
            base.Evaluate();

            // For site conquered event collections, if there are body abused events, we can link the attacking entity as the abuser witin those events.
            foreach (var ev in Event.Where(x => HistoricalEvent.Types[x.Type] == "body abused").Cast<HE_BodyAbused>())
            {
                ev.Entity_Abuser = AttackingEn;
            }

            //TODO
            //Case: Site conquered event with Add hf entity link related to group that is part of attacker entity.   Also with change hf state with hf (former member of group of defender civ) settling in new site.
            //Change: Associate add hf entity link for prisoner relationship where change HF state HF was captured and brought to new site as prisoner.

        }

        internal override void Export(string table)
        {
            base.Export(table);


            table = GetType().Name;

            var vals = new List<object> { Id, WarEventCol_ ?? -1, Ordinal, AttackingEnid, DefendingEnid, SiteId};



            Database.ExportWorldItem(table, vals);

        }

    }
}
