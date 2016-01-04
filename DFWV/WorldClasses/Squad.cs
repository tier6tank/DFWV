using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Design;
using System.Linq;
using System.Xml.Linq;
using DFWV.Annotations;
using DFWV.WorldClasses.EntityClasses;
using DFWV.WorldClasses.HistoricalFigureClasses;

namespace DFWV.WorldClasses
{
    public class Squad : XMLObject
    {
        override public Point Location => Point.Empty;
        public string AltName { get; set; }
        private int? EntityID { get; set; }
        public Entity Entity { get; set; }
        public List<int> MemberIDs { get; set; }
        public List<HistoricalFigure> Members { get; set; }

        public Squad(XDocument xdoc, World world)
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
                    case "name":
                        Name = val;
                        break;
                    case "altname":
                        AltName = val;
                        break;
                    case "entity_id":
                        if (valI != -1)
                            EntityID = valI;
                        break;
                    case "member":
                        if (valI != -1)
                        {
                            if (MemberIDs == null)
                                MemberIDs = new List<int>();
                            MemberIDs.Add(valI);
                        }
                        break;
                    default:
                        DFXMLParser.UnexpectedXmlElement(xdoc.Root.Name.LocalName, element, xdoc.Root.ToString());
                        break;
                }
            }
        }

        public override void Select(MainForm frm)
        {
            if (frm.grpSquad.Text == ToString() && frm.MainTab.SelectedTab == frm.tabSquad)
                return;
            Program.MakeSelected(frm.tabSquad, frm.lstSquad, this);

            //frm.grpSquad.Text = ToString();
            //frm.grpSquad.Show();
#if DEBUG
            //frm.grpSquad.Text += string.Format(" - ID: {0}", ID);
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
            if (EntityID.HasValue)
                Entity = World.Entities[EntityID.Value];
            if (MemberIDs != null)
            {
                Members = new List<HistoricalFigure>();
                foreach (var memberID in MemberIDs)
                    Members.Add(World.HistoricalFigures[memberID]);
            }
        }
    

        internal override void Process()
        {
            
        }

        internal override void Plus(XDocument xdoc)
        {
            
        }
    }


}