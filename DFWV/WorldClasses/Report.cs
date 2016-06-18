using System.Collections.Generic;
using System.Drawing;
using System.Xml.Linq;
using DFWV.Annotations;

namespace DFWV.WorldClasses
{
    public class Report : XMLObject
    {
        override public Point Location => Point.Empty;
        public string Text { get; set; }
        public static List<string> Types = new List<string>();
        public int TypeId { get; set; }
        public string Type => Types[TypeId];
        public int Year { get; set; }
        public int Seconds { get; set; }
        [UsedImplicitly]
        public int Time => new WorldTime(Year, Seconds).TotalSeconds;
        public bool Continuation { get; set; }
        public bool Announcement { get; set; }

        [UsedImplicitly]
        public string DispNameLower => ToString().ToLower();


        public Report(XDocument xdoc, World world)
            : base(xdoc, world)
        {
            foreach (var element in xdoc.Root.Elements())
            {
                var val = element.Value.Trim();
                int valI;
                int.TryParse(val, out valI);
                switch (element.Name.LocalName)
                {
                    case "id":
                        break;
                    case "text":
                        Text = val;
                        break;
                    case "type":
                        if (!Types.Contains(val))
                            Types.Add(val);
                        TypeId = Types.IndexOf(val);
                        break;
                    case "year":
                        Year = valI;
                        break;
                    case "time":
                        Seconds = valI;
                        break;
                    case "continuation":
                        Continuation = true;
                        break;
                    case "announcement":
                        Announcement = true;
                        break;
                    default:
                        DFXMLParser.UnexpectedXmlElement(xdoc.Root.Name.LocalName, element, xdoc.Root.ToString());
                        break;
                }
            }
        }

        public override void Select(MainForm frm)
        {
            if (frm.grpReport.Text == ToString() && frm.MainTab.SelectedTab == frm.tabReport)
                return;
            Program.MakeSelected(frm.tabReport, frm.lstReport, this);

            frm.grpReport.Text = ToString();
            frm.grpReport.Show();
#if DEBUG
            frm.grpReport.Text += $" - ID: {Id}";
#endif


            frm.lblReportText.Text = ToString();
            frm.lblReportType.Text = Type;
            frm.lblReportTime.Text = new WorldTime(Year,Seconds).ToString();
            frm.lblReportAnnouncement.Visible = Announcement;
            frm.lblReportContinuation.Visible = Continuation;
        }

        internal override void Export(string table)
        {

            var vals = new List<object>
            {
                Id, 
                Name.DBExport()
            };

            Database.ExportWorldItem(table, vals);
        }

        internal override void Link()
        {

        }

        internal override void Process()
        {
            
        }

        internal override void Plus(XDocument xdoc)
        {
            
        }

        public override string ToString()
        {
            return Text;
            
            
        }
    }


}