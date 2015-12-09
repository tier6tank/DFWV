using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Xml.Linq;
using DFWV.Annotations;

namespace DFWV.WorldClasses
{
    public class UndergroundRegion : XMLObject
    {
        public string Type { get; set; }
        public int Depth { get; set; }
        public List<Point> Coords { get; set; }
        public Dictionary<Race, int> Populations { get; set; }


        [UsedImplicitly]
        public string DispNameLower => ToString().ToLower();

        override public Point Location => Point.Empty;

        public UndergroundRegion(XDocument xdoc, World world) 
            : base(xdoc, world)
        {
            foreach (var element in xdoc.Root.Elements())
            {
                var val = element.Value;
                switch (element.Name.LocalName)
                {
                    case "id":
                        break;
                    case "type":
                        Type = val.ToTitleCase();
                        break;
                    case "depth":
                        Depth = Convert.ToInt32(val);
                        break;
                    default:
                        DFXMLParser.UnexpectedXmlElement(xdoc.Root.Name.LocalName, element, xdoc.Root.ToString());
                        break;
                }
            }
        }

        //public UndergroundRegion(NameValueCollection data, World world) 
        //    : base (world)
        //{
        //    Depth = Convert.ToInt32(data["Depth"]);
        //    Type = data["Type"].ToString();
        //}

        public override string ToString()
        {
            return Type;
        }

        public override void Select(MainForm frm)
        {
            if (frm.grpUndergroundRegion.Text == ToString() && frm.MainTab.SelectedTab == frm.tabUndergroundRegion)
                return;
            Program.MakeSelected(frm.tabUndergroundRegion, frm.lstUndergroundRegion, this);

            frm.grpUndergroundRegion.Text = ToString();
            frm.grpUndergroundRegion.Show();

            frm.lblUndergroundRegionDepth.Text = Depth.ToString();
            frm.lblUndergroundRegionType.Text = Type;




            if (Populations != null)
            {
                frm.grpUndergroundRegionPopulation.FillListboxWith(frm.lstUndergroundRegionPopulation, Populations.Keys);
                frm.grpUndergroundRegionPopulation.Text =
                    $"Population ({(Populations.Values.Contains(10000001) ? "Unnumbered" : Populations.Values.Sum().ToString())})";
                frm.grpUndergroundRegionPopulation.Visible = true;
            }
            else
            {
                frm.grpUndergroundRegionPopulation.Visible = false;
            }


        }

        internal override void Link()
        {

        }

        internal override void Process()
        {

        }

        internal override void Plus(XDocument xdoc)
        {
            foreach (var element in xdoc.Root.Elements())
            {
                var val = element.Value;
                int valI;
                int.TryParse(val, out valI);

                switch (element.Name.LocalName)
                {
                    case "id":
                    case "type":
                        break;
                    case "coords":
                        if (Coords == null)
                            Coords = new List<Point>();
                        foreach (var coordSplit in val.Split(new[] { '|' }, StringSplitOptions.RemoveEmptyEntries).Select(coord => coord.Split(',')).Where(coordSplit => coordSplit.Length == 2))
                        {
                            Coords.Add(new Point(Convert.ToInt32(coordSplit[0]), Convert.ToInt32(coordSplit[1])));
                        }
                        break;
                    case "population":
                        if (Populations == null)
                            Populations = new Dictionary<Race, int>();
                        foreach (var popSplit in val.Split(new[] { '|' }, StringSplitOptions.RemoveEmptyEntries).Select(pop => pop.Split(',')).Where(popSplit => popSplit.Length == 3))
                        {
                            Populations.Add(World.Races[Convert.ToInt32(popSplit[0])], Convert.ToInt32(popSplit[1]));
                        }
                        break;
                    default:
                        DFXMLParser.UnexpectedXmlElement(xdoc.Root.Name.LocalName + "\t" + "", element, xdoc.Root.ToString());
                        break;
                }
            }
        }

        internal override void Export(string table)
        {

            var vals = new List<object>
            {
                Id, 
                Type, 
                Depth
            };

            Database.ExportWorldItem(table, vals);

            if (Coords != null)
            {
                int coordId = 0;
                foreach (var coord in Coords)
                {
                    Database.ExportWorldItem("UGRegion_Coords", new List<object> { Id, coordId, coord.X, coord.Y });
                    coordId++;
                }
            }

            if (Populations != null)
            {
                foreach (var pop in Populations)
                {
                    Database.ExportWorldItem("UGRegion_Population", new List<object> { Id, pop.Key.Id, pop.Value });
                }
            }

        }
    }
}