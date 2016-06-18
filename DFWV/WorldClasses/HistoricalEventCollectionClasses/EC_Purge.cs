using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;

namespace DFWV.WorldClasses.HistoricalEventCollectionClasses
{
    public class EC_Purge : HistoricalEventCollection
    {
        private int Ordinal { get; }
        private int? SiteId { get; }
        private string Adjective { get; }
        private Site Site { get; set; }
        private int? OccasionId { get; set; }
        private List<int> EventCol_ { get; }
        private List<HistoricalEventCollection> EventCol { get; set; }

        override public Point Location => Site.Location;

        public EC_Purge(XDocument xdoc, World world)
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
                    case "adjective":
                        Adjective = val;
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
            if (EventCol_ != null)
                EventCol = new List<HistoricalEventCollection>();
            LinkFieldList(EventCol_,
                EventCol, World.HistoricalEventCollections);

            if (SiteId.HasValue && World.Sites.ContainsKey(SiteId.Value))
                Site = World.Sites[SiteId.Value];

        }

        public override void Select(MainForm frm)
        {

            base.Select(frm);

            foreach (var tabpage in frm.MainTabEventCollectionTypes.TabPages.Cast<TabPage>().Where(tabpage => tabpage != frm.tabEventCollectionPurge))
            {
                frm.MainTabEventCollectionTypes.TabPages.Remove(tabpage);
            }
            if (!frm.MainTabEventCollectionTypes.TabPages.Contains(frm.tabEventCollectionPurge))
                frm.MainTabEventCollectionTypes.TabPages.Add(frm.tabEventCollectionPurge);

            if (StartTime != null || EndTime != null)
            {
                frm.lblPurgeTime.Text = $"{StartTime} - {EndTime}";
                frm.lblPurgeDuration.Text = WorldTime.Duration(EndTime, StartTime);
            }
            else
            {
                frm.lblPurgeTime.Text = "";
                frm.lblPurgeDuration.Text = "";
            }
            frm.lblPurgeOrdinal.Text = Ordinal.ToString();
            frm.lblPurgeSite.Data = Site;
            frm.lblPurgeAdjective.Text = Adjective;

            frm.lstPurgeEvents.Items.Clear();
            if (Event != null)
                frm.lstPurgeEvents.Items.AddRange(Event.ToArray());

            SelectTab(frm);
        }

        internal override void Process()
        {
            base.Process();
        }

        internal override void Export(string table)
        {
            base.Export(table);


            table = GetType().Name;

            var vals = new List<object> { Id, Ordinal };


            Database.ExportWorldItem(table, vals);

        }

    }
}
