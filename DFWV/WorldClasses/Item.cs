using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Xml.Linq;
using DFWV.Annotations;
using DFWV.WorldClasses.HistoricalEventClasses;
using DFWV.WorldClasses.HistoricalFigureClasses;

namespace DFWV.WorldClasses
{
    public class Item : XMLObject
    {
        override public Point Location => Point.Empty;
        public static List<string> ItemTypes = new List<string>();
        public static List<string> ItemSubTypes = new List<string>();
        public static List<string> Materials = new List<string>();
        public int? ItemType { get; set; }
        public int? ItemSubType { get; set; }
        public string Description;
        public int? Mat { get; set; }
        public int? MakerID { get; set; }
        public HistoricalFigure Maker { get; set; }
        public int? Quality { get; set; }
        public int? SkillUsed { get; set; }
        public int? MasterpieceEventID { get; set; }
        public HistoricalEvent MasterpieceEvent { get; set; }

        public Item(XDocument xdoc, World world)
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
                    case "type":
                        if (!ItemTypes.Contains(val))
                            ItemTypes.Add(val);
                        ItemType = ItemTypes.IndexOf(val);
                        break;
                    case "subtype":
                        if (valI != -1)
                        {
                            if (!ItemSubTypes.Contains(val))
                                ItemSubTypes.Add(val);
                            ItemSubType = ItemSubTypes.IndexOf(val);
                        }
                        break;
                    case "description":
                        Description = val;
                        break;
                    case "mat":
                        if (!Materials.Contains(val))
                            Materials.Add(val);
                        Mat = Materials.IndexOf(val);
                        break;
                    case "maker_unit":
                        if (valI != -1)
                            MakerID = valI;
                        break;
                    case "quality":
                        if (valI != -1)
                            Quality = valI;
                        break;
                    case "skill_used":
                        if (valI != -1)
                            SkillUsed = valI;
                        break;
                    case "masterpiece_event":
                        if (valI != -1)
                            MasterpieceEventID = valI;
                        break;
                    default:
                        DFXMLParser.UnexpectedXmlElement(xdoc.Root.Name.LocalName, element, xdoc.Root.ToString());
                        break;
                }
            }
        }

        public override void Select(MainForm frm)
        {
            if (frm.grpItem.Text == ToString() && frm.MainTab.SelectedTab == frm.tabItem)
                return;
            Program.MakeSelected(frm.tabItem, frm.lstItem, this);

            //frm.grpItem.Text = ToString();
            //frm.grpItem.Show();
#if DEBUG
            //frm.grpItem.Text += string.Format(" - ID: {0}", ID);
#endif
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
            if (MakerID.HasValue)
                Maker = World.HistoricalFigures[MakerID.Value];
            if (MasterpieceEventID.HasValue)
                MasterpieceEvent = World.HistoricalEvents[MasterpieceEventID.Value];
        }

        internal override void Process()
        {
            
        }

        internal override void Plus(XDocument xdoc)
        {
            
        }
    }


}