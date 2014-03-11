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
    class EC_Journey : HistoricalEventCollection
    {
        public int Ordinal { get; set; }

        override public Point Location 
        { 
            get 
            {
                if (Event[0] is HE_HFTravel)
                {
                    HE_HFTravel evt = (HE_HFTravel)Event[0];
                    if (evt.Site != null)
                        return evt.Site.Location;
                }
                if (Event.Last() is HE_HFTravel)
                {
                    HE_HFTravel evt = (HE_HFTravel)Event.Last();
                    if (evt.Return && evt.Site != null)
                        return evt.Site.Location;
                }
                return Point.Empty;
            } 
        }


        public EC_Journey(XDocument xdoc, World world)
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
                    default:
                        DFXMLParser.UnexpectedXMLElement(xdoc.Root.Name.LocalName + "\t" + HistoricalEventCollection.Types[Type], element, xdoc.Root.ToString());
                        break;
                }
            }

        }

        internal override void Link()
        {
            base.Link();
        }

        public override void Select(MainForm frm)
        {
            base.Select(frm);

            foreach (TabPage tabpage in frm.MainTabEventCollectionTypes.TabPages)
            {
                if (tabpage != frm.tabEventCollectionJourney)
                    frm.MainTabEventCollectionTypes.TabPages.Remove(tabpage);
            }
            if (!frm.MainTabEventCollectionTypes.TabPages.Contains(frm.tabEventCollectionJourney))
                frm.MainTabEventCollectionTypes.TabPages.Add(frm.tabEventCollectionJourney);

            if (StartTime != null || EndTime != null)
            {
                frm.lblJourneyTime.Text = StartTime.ToString() + " - " + EndTime.ToString();
                frm.lblJourneyDuration.Text = WorldTime.Duration(EndTime, StartTime);
            }
            else
            {
                frm.lblJourneyTime.Text = "";
                frm.lblJourneyDuration.Text = "";
            }
            frm.lblJourneyOrdinal.Text = Ordinal.ToString();

            frm.lstJourneyEvents.Items.Clear();
            if (Event != null)
                frm.lstJourneyEvents.Items.AddRange(Event.ToArray());

            frm.grpJourneyEvents.Visible = frm.lstJourneyEvents.Items.Count > 0;

            if (frm.lstJourneyEvents.Items.Count > 0)
                frm.lstJourneyEvents.SelectedIndex = 0;

            base.SelectTab(frm);
        }

        internal override void Process()
        {
            base.Process();
        }

        internal override void Export(string table)
        {
            base.Export(table);


            List<object> vals;
            table = this.GetType().Name.ToString();

            vals = new List<object>() { ID, Ordinal };


            Database.ExportWorldItem(table, vals);

        }

    }
}
