﻿using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;

namespace DFWV.WorldClasses.HistoricalEventCollectionClasses
{
    public class EC_Performance : HistoricalEventCollection
    {
        private int Ordinal { get; }
        private List<int> EventCol_ { get; }
        private List<HistoricalEventCollection> EventCol { get; set; }

        override public Point Location => Point.Empty;

        public EC_Performance(XDocument xdoc, World world)
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

            foreach (var tabpage in frm.MainTabEventCollectionTypes.TabPages.Cast<TabPage>().Where(tabpage => tabpage != frm.tabEventCollectionPerformance))
            {
                frm.MainTabEventCollectionTypes.TabPages.Remove(tabpage);
            }
            if (!frm.MainTabEventCollectionTypes.TabPages.Contains(frm.tabEventCollectionPerformance))
                frm.MainTabEventCollectionTypes.TabPages.Add(frm.tabEventCollectionPerformance);

            if (StartTime != null || EndTime != null)
            {
                frm.lblPerformanceTime.Text = $"{StartTime} - {EndTime}";
                frm.lblPerformanceDuration.Text = WorldTime.Duration(EndTime, StartTime);
            }
            else
            {
                frm.lblPerformanceTime.Text = "";
                frm.lblPerformanceDuration.Text = "";
            }
            frm.lblPerformanceOrdinal.Text = Ordinal.ToString();

            frm.lstPerformanceEvents.Items.Clear();
            if (Event != null)
                frm.lstPerformanceEvents.Items.AddRange(Event.ToArray());

            frm.grpPerformanceEvents.Visible = frm.lstPerformanceEvents.Items.Count > 0;

            if (frm.lstPerformanceEvents.Items.Count > 0)
                frm.lstPerformanceEvents.SelectedIndex = 0;

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
