using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;

namespace DFWV.WorldClasses.HistoricalEventCollectionClasses
{
    public class EC_Ceremony : HistoricalEventCollection
    {
        private int Ordinal { get; }
        private List<int> EventCol_ { get; }
        private List<HistoricalEventCollection> EventCol { get; set; }

        override public Point Location => Point.Empty;

        public EC_Ceremony(XDocument xdoc, World world)
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
                    case "eventcol":
                        if (EventCol_ == null)
                            EventCol_ = new List<int>();
                        EventCol_.Add(valI);
                        break;
                    case "ordinal":
                        Ordinal = valI;
                        break;

                    default:
                        DFXMLParser.UnexpectedXmlElement(xdoc.Root.Name.LocalName + "\t" + Types[Type], element, xdoc.Root.ToString());
                        break;
                }
            }

        }

        internal override void Link()
        {
            if (EventCol_ != null)
                EventCol = new List<HistoricalEventCollection>();
            LinkFieldList(EventCol_,
                EventCol, World.HistoricalEventCollections);
            base.Link();
        }

        public override void Select(MainForm frm)
        {

            base.Select(frm);

            foreach (var tabpage in frm.MainTabEventCollectionTypes.TabPages.Cast<TabPage>().Where(tabpage => tabpage != frm.tabEventCollectionCeremony))
            {
                frm.MainTabEventCollectionTypes.TabPages.Remove(tabpage);
            }
            if (!frm.MainTabEventCollectionTypes.TabPages.Contains(frm.tabEventCollectionCeremony))
                frm.MainTabEventCollectionTypes.TabPages.Add(frm.tabEventCollectionCeremony);

            if (StartTime != null || EndTime != null)
            {
                frm.lblCeremonyTime.Text = $"{StartTime} - {EndTime}";
                frm.lblCeremonyDuration.Text = WorldTime.Duration(EndTime, StartTime);
            }
            else
            {
                frm.lblCeremonyTime.Text = "";
                frm.lblCeremonyDuration.Text = "";
            }
            frm.lblCeremonyOrdinal.Text = Ordinal.ToString();

            frm.lstCeremonyEvents.Items.Clear();
            if (Event != null)
                frm.lstCeremonyEvents.Items.AddRange(Event.ToArray());

            frm.grpCeremonyEvents.Visible = frm.lstCeremonyEvents.Items.Count > 0;

            if (frm.lstCeremonyEvents.Items.Count > 0)
                frm.lstCeremonyEvents.SelectedIndex = 0;

            SelectTab(frm);
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
