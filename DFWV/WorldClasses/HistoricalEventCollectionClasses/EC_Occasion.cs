using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;
using DFWV.WorldClasses.EntityClasses;

namespace DFWV.WorldClasses.HistoricalEventCollectionClasses
{
    public class EcOccasion : HistoricalEventCollection
    {
        private int Ordinal { get; }
        private int? CivId { get; }
        private Entity Civ { get; set; }
        private int? OccasionId { get; set; }
        private List<int> EventCol_ { get; }
        private List<HistoricalEventCollection> EventCol { get; set; }

        override public Point Location => Civ.Location;

        public EcOccasion(XDocument xdoc, World world)
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
                    case "eventcol":
                        if (EventCol_ == null)
                            EventCol_ = new List<int>();
                        EventCol_.Add(valI);
                        break;
                    case "civ_id":
                        CivId = valI;
                        break;
                    case "occasion_id":
                        OccasionId = valI;
                        break;

                    default:
                        DfxmlParser.UnexpectedXmlElement(xdoc.Root.Name.LocalName + "\t" + Types[Type], element, xdoc.Root.ToString());
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

            if (CivId.HasValue && World.Entities.ContainsKey(CivId.Value))
                Civ = World.Entities[CivId.Value];

        }

        public override void Select(MainForm frm)
        {

            base.Select(frm);

            foreach (var tabpage in frm.MainTabEventCollectionTypes.TabPages.Cast<TabPage>().Where(tabpage => tabpage != frm.tabEventCollectionOccasion))
            {
                frm.MainTabEventCollectionTypes.TabPages.Remove(tabpage);
            }
            if (!frm.MainTabEventCollectionTypes.TabPages.Contains(frm.tabEventCollectionOccasion))
                frm.MainTabEventCollectionTypes.TabPages.Add(frm.tabEventCollectionOccasion);

            if (StartTime != null || EndTime != null)
            {
                frm.lblOccasionTime.Text = $"{StartTime} - {EndTime}";
                frm.lblOccasionDuration.Text = WorldTime.Duration(EndTime, StartTime);
            }
            else
            {
                frm.lblOccasionTime.Text = "";
                frm.lblOccasionDuration.Text = "";
            }
            frm.lblOccasionOrdinal.Text = Ordinal.ToString();
            frm.lblOccasionCiv.Data = Civ;

            frm.lstOccasionEvents.Items.Clear();
            if (Event != null)
                frm.lstOccasionEvents.Items.AddRange(Event.ToArray());

            frm.grpOccasionEvents.Visible = frm.lstOccasionEvents.Items.Count > 0;

            if (frm.lstOccasionEvents.Items.Count > 0)
                frm.lstOccasionEvents.SelectedIndex = 0;

            frm.lstOccasionEventCols.Items.Clear();
            if (EventCol != null)
                frm.lstOccasionEventCols.Items.AddRange(EventCol.ToArray());

            frm.grpOccasionEventCols.Visible = frm.lstOccasionEventCols.Items.Count > 0;

            SelectTab(frm);
        }

        internal override void Process()
        {
            base.Process();
            if (Civ.OccasionEventCollections == null)
                Civ.OccasionEventCollections = new List<EcOccasion>();


        }

        internal override void Export(string table)
        {
            base.Export(table);


            table = GetType().Name;

            var vals = new List<object> { Id, Ordinal, CivId.DBExport() };


            Database.ExportWorldItem(table, vals);

        }

    }
}
