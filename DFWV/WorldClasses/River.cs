using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Xml.Linq;
using DFWV.Annotations;

namespace DFWV.WorldClasses
{
    public class River : XmlObject
    {

        [UsedImplicitly]
        public string AltName { get; set; }
        public List<Point> Coords { get; set; }
        public List<int> Elevation { get; set; }
        public River Parent { get; set; }
        public List<River> Tributaries { get; set; }

        override public Point Location => Coords.Last();

        public River(XDocument xdoc, World world)
            : base(xdoc, world)
        {
            foreach (var element in xdoc.Root.Elements())
            {
                var val = element.Value.Trim();
                switch (element.Name.LocalName)
                {
                    case "id":
                        break;
                    case "name":
                        Name = val;
                        break;
                    case "name2":
                        AltName = val;
                        break;
                    case "coords":
                        if (Coords == null)
                            Coords = new List<Point>();
                        foreach (var coordSplit in val.Split(new[] { '|' }, StringSplitOptions.RemoveEmptyEntries).Select(coord => coord.Split(',')).Where(coordSplit => coordSplit.Length == 2))
                        {
                            Coords.Add(new Point(Convert.ToInt32(coordSplit[0]), Convert.ToInt32(coordSplit[1])));
                        }
                        break;
                    case "elevation":
                        Elevation = val.Split(new[] { '|' }, StringSplitOptions.RemoveEmptyEntries).Select(x => Convert.ToInt32(x)).ToList();
                        break;
                    default:
                        DfxmlParser.UnexpectedXmlElement(xdoc.Root.Name.LocalName, element, xdoc.Root.ToString());
                        break;
                }
            }
        }

        public override void Select(MainForm frm)
        {
            if (frm.grpRiver.Text == ToString() && frm.MainTab.SelectedTab == frm.tabRiver)
                return;
            Program.MakeSelected(frm.tabRiver, frm.lstRiver, this);

            frm.grpRiver.Text = ToString();
            frm.grpRiver.Show();
#if DEBUG
            frm.grpRiver.Text += $" - ID: {Id}";
#endif


            frm.lblRiverName.Text = ToString();
            frm.lblRiverAltName.Text = AltName;
            frm.lblRiverEndsAt.Data = new Coordinate(Coords.Last());
            frm.lblRiverElevation.Text = string.Join(",", Elevation);
            frm.lblRiverParent.Data = Parent;


            frm.grpRiverTributaries.FillListboxWith(frm.lstRiverTributaries, Tributaries);
        }

        internal override void Export(string table)
        {

            var vals = new List<object>
            {
                Id, 
                Name.DBExport(), 
                AltName.DBExport(),
                Parent.DBExport()
            };

            Database.ExportWorldItem(table, vals);


            if (Coords != null)
            {
                int coordId = 0;
                foreach (var coord in Coords)
                {
                    Database.ExportWorldItem("River_Coords", new List<object> { Id, coordId, coord.X, coord.Y });
                    coordId++;
                }
            }

            if (Elevation != null)
            {
                int elevationId = 0;
                foreach (var elevation in Elevation)
                {
                    Database.ExportWorldItem("River_Elevation", new List<object> { Id, elevationId, elevation });
                    elevationId++;
                }
            }
        }

        internal override void Link()
        {
            Point endPt = Coords.Last();
            foreach (var river in World.Rivers.Values)
            {
                if (river == this || (Tributaries != null && Tributaries.Contains(river)))
                    continue;
                if (river.Coords.Contains(endPt))
                {
                    if (river.Coords.Last() == endPt)
                        continue;
                    Parent = river;
                    if (river.Tributaries == null)
                        river.Tributaries = new List<River>();
                    river.Tributaries.Add(this);
                }
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