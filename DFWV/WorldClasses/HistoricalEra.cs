using System;
using System.Collections.Generic;
using System.Drawing;
using System.Xml.Linq;
using DFWV.Annotations;

namespace DFWV.WorldClasses
{
    public class HistoricalEra : XmlObject
    {
        [UsedImplicitly]
        public int StartYear { get; set; }
        private WorldTime Start { get; set; }
        private WorldTime End { get; set; }


        [UsedImplicitly]
        public string DispNameLower => ToString().ToLower();

        override public Point Location => Point.Empty;

        public HistoricalEra(XDocument xdoc, World world) 
            : base(world)
        {
            foreach (var element in xdoc.Root.Elements())
            {
                var val = element.Value;
                switch (element.Name.LocalName)
                {
                    case "name":
                        Name = val;
                        break;
                    case "start_year":
                        Id = Convert.ToInt32(val);
                        StartYear = Id;
                        break;
                    default:
                        DfxmlParser.UnexpectedXmlElement(xdoc.Root.Name.LocalName, element, xdoc.Root.ToString());
                        break;
                }
            }
        }

        //public HistoricalEra(NameValueCollection data, World world) 
        //    : base (data, world)
        //{
        //    Name = data["Name"].ToString();
        //    StartYear = Convert.ToInt32(data["StartYear"]);
        //}


        public override void Select(MainForm frm)
        {
            if (frm.grpHistoricalEra.Text == ToString() && frm.MainTab.SelectedTab == frm.tabHistoricalEra)
                return;
            Program.MakeSelected(frm.tabHistoricalEra, frm.lstHistoricalEra, this);

            frm.grpHistoricalEra.Text = ToString();
            frm.grpHistoricalEra.Show();

            frm.lblHistoricalEraName.Text = ToString();
            if (Start != null && End != null)
                frm.lblHistoricalEraStartYear.Text =
                    $"{(Start.Year == -1 ? 0 : Start.Year)} - {(End == WorldTime.Present ? "" : End.ToString())}";
        }

        internal override void Link()
        {
            Start = new WorldTime(StartYear);
            var getNext = false;
            foreach (var era in World.HistoricalEras)
            {
                if (era.Key == StartYear)
                    getNext = true;
                else if (getNext)
                {
                    End = new WorldTime(era.Value.StartYear - 1);
                    break;
                }
            }
            if (End == null)
            {
                End = WorldTime.Present;
            }
        }

        internal override void Process()
        {

        }

        internal override void Plus(XDocument xdoc)
        {
            
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
    }
}