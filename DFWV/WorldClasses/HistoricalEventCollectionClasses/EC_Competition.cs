using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;

namespace DFWV.WorldClasses.HistoricalEventCollectionClasses
{
    public class EC_Competition : HistoricalEventCollection
    {
        private int Ordinal { get; }

        override public Point Location => Point.Empty;

        public EC_Competition(XDocument xdoc, World world)
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

                    default:
                        DFXMLParser.UnexpectedXmlElement(xdoc.Root.Name.LocalName + "\t" + Types[Type], element, xdoc.Root.ToString());
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

            foreach (var tabpage in frm.MainTabEventCollectionTypes.TabPages.Cast<TabPage>().Where(tabpage => tabpage != frm.tabEventCollectionCompetition))
            {
                frm.MainTabEventCollectionTypes.TabPages.Remove(tabpage);
            }
            if (!frm.MainTabEventCollectionTypes.TabPages.Contains(frm.tabEventCollectionCompetition))
                frm.MainTabEventCollectionTypes.TabPages.Add(frm.tabEventCollectionCompetition);

            if (StartTime != null || EndTime != null)
            {
                frm.lblCompetitionTime.Text = $"{StartTime} - {EndTime}";
                frm.lblCompetitionDuration.Text = WorldTime.Duration(EndTime, StartTime);
            }
            else
            {
                frm.lblCompetitionTime.Text = "";
                frm.lblCompetitionDuration.Text = "";
            }
            frm.lblCompetitionOrdinal.Text = Ordinal.ToString();

            frm.lstCompetitionEvents.Items.Clear();
            if (Event != null)
                frm.lstCompetitionEvents.Items.AddRange(Event.ToArray());

            frm.grpCompetitionEvents.Visible = frm.lstCompetitionEvents.Items.Count > 0;

            if (frm.lstCompetitionEvents.Items.Count > 0)
                frm.lstCompetitionEvents.SelectedIndex = 0;

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
