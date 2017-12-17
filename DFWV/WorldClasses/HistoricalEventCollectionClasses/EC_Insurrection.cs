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
    public class EC_Insurrection : HistoricalEventCollection
    {
        private int? SiteId { get; }
        private Site Site { get; set; }
        private int? ParentEventCol_ { get; }
        private HistoricalEventCollection ParentEventCol { get; set; }
        public string Outcome { get; set; }
        private int Ordinal { get; }
        private int? TargetEntId { get; }
        public Entity TargetEnt { get; private set; }

        override public Point Location => Site?.Coords ?? Point.Empty;

        public EC_Insurrection(XDocument xdoc, World world)
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
                    case "parent_eventcol":
                        ParentEventCol_ = valI;
                        break;
                    case "site_id":
                        SiteId = valI;
                        break;
                    case "target_enid":
                        TargetEntId = valI;
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
            if (ParentEventCol_.HasValue && World.HistoricalEventCollections.ContainsKey(ParentEventCol_.Value))
                ParentEventCol = World.HistoricalEventCollections[ParentEventCol_.Value];
            if (SiteId.HasValue && World.Sites.ContainsKey(SiteId.Value))
                Site = World.Sites[SiteId.Value];
            if (TargetEntId.HasValue && World.Entities.ContainsKey(TargetEntId.Value))
                TargetEnt = World.Entities[TargetEntId.Value];

        }

        public override void Select(MainForm frm)
        {
            base.Select(frm);

            foreach (var tabpage in frm.MainTabEventCollectionTypes.TabPages.Cast<TabPage>().Where(tabpage => tabpage != frm.tabEventCollectionInsurrection))
            {
                frm.MainTabEventCollectionTypes.TabPages.Remove(tabpage);
            }
            if (!frm.MainTabEventCollectionTypes.TabPages.Contains(frm.tabEventCollectionInsurrection))
                frm.MainTabEventCollectionTypes.TabPages.Add(frm.tabEventCollectionInsurrection);


            frm.lblInsurrectionSite.Data = Site;
            frm.lblInsurrectionParent.Data = ParentEventCol;

            frm.lblInsurrectionCoords.Data = new Coordinate(Site?.Location ?? Point.Empty);

            if (StartTime != null || EndTime != null)
            {
                frm.lblInsurrectionTime.Text = $"{StartTime} - {EndTime}";
                frm.lblInsurrectionDuration.Text = WorldTime.Duration(EndTime, StartTime);
            }
            else
            {
                frm.lblInsurrectionTime.Text = "";
                frm.lblInsurrectionDuration.Text = "";
            }
            frm.lblInsurrectionTargetEnt.Data = TargetEnt;

            frm.lblInsurrectionOutcome.Text = Outcome;

            frm.lblInsurrectionOrdinal.Text = Ordinal.ToString();

            frm.lstInsurrectionEvents.Items.Clear();
            if (Event != null)
                frm.lstInsurrectionEvents.Items.AddRange(Event.ToArray());

            frm.grpInsurrectionEvents.Visible = frm.lstInsurrectionEvents.Items.Count > 0;

            if (frm.lstInsurrectionEvents.Items.Count > 0)
                frm.lstInsurrectionEvents.SelectedIndex = 0;

            SelectTab(frm);
        }

        public override void Process()
        {
            base.Process();
            if (Site != null)
            {
                if (Site.InsurrectionEventCollections == null)
                    Site.InsurrectionEventCollections = new ConcurrentBag<EC_Insurrection>();
                Site.InsurrectionEventCollections.Add(this);
            }
            if (TargetEnt != null)
            {
                if (TargetEnt.InsurrectionEventCollections == null)
                    TargetEnt.InsurrectionEventCollections = new ConcurrentBag<EC_Insurrection>();
                TargetEnt.InsurrectionEventCollections.Add(this);
            }

            if (Event.Last() is HE_InsurrectionStarted)
            {
                Outcome = (Event.Last() as HE_InsurrectionStarted).Outcome;
            }
            else
                Outcome = "ongoing";
        }

        internal override void Export(string table)
        {
            base.Export(table);

            table = GetType().Name;

            var vals = new List<object> { Id, ParentEventCol_ ?? -1, Ordinal, SiteId, TargetEntId, Outcome };

            Database.ExportWorldItem(table, vals);

        }

    }
}
