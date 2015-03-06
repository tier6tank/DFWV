using System;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Drawing;
using DFWV.Annotations;

namespace DFWV.WorldClasses
{
    public class UndergroundRegion : XMLObject
    {
        private string Type { get; set; }
        [UsedImplicitly]
        public int Depth { get; set; }
        public List<Point> Coords { get; set; }
        public Dictionary<Race, int> Populations { get; set; }


        [UsedImplicitly]
        public string DispNameLower { get { return ToString().ToLower(); } }

        override public Point Location { get { return Point.Empty; } }

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
                        DFXMLParser.UnexpectedXMLElement(xdoc.Root.Name.LocalName, element, xdoc.Root.ToString());
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
            frm.grpUndergroundRegion.Text = ToString();
            frm.grpUndergroundRegion.Show();

            frm.lblUndergroundRegionDepth.Text = Depth.ToString();
            frm.lblUndergroundRegionType.Text = Type;

            frm.lstUndergroundRegionPopulation.BeginUpdate();
            frm.lstUndergroundRegionPopulation.Items.Clear();
            if (Populations != null)
            {
                Race[] array = new Race[25];
                Populations.Keys.CopyTo(array, 0);
                frm.lstUndergroundRegionPopulation.Items.AddRange(array);
            }
            frm.lstUndergroundRegionPopulation.EndUpdate();
            frm.grpUndergroundRegionPopulation.Visible = frm.lstUndergroundRegionPopulation.Items.Count > 0;

            Program.MakeSelected(frm.tabUndergroundRegion, frm.lstUndergroundRegion, this);
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
                Int32.TryParse(val, out valI);

                switch (element.Name.LocalName)
                {
                    case "id":
                    case "type":
                        break;
                    case "coords":
                        if (Coords == null)
                            Coords = new List<Point>();
                        foreach (var coord in val.Split(new[] { '|' }, StringSplitOptions.RemoveEmptyEntries))
                        {
                            var coordSplit = coord.Split(',');
                            if (coordSplit.Length == 2)
                                Coords.Add(new Point(Convert.ToInt32(coordSplit[0]), Convert.ToInt32(coordSplit[1])));
                        }
                        break;
                    case "population":
                        if (Populations == null)
                            Populations = new Dictionary<Race, int>();
                        foreach (var pop in val.Split(new[] { '|' }, StringSplitOptions.RemoveEmptyEntries))
                        {
                            var popSplit = pop.Split(',');
                            if (popSplit.Length == 2)
                                Populations.Add(World.Races[Convert.ToInt32(popSplit[0])], Convert.ToInt32(popSplit[1]));
                        }
                        break;
                    default:
                        DFXMLParser.UnexpectedXMLElement(xdoc.Root.Name.LocalName + "\t" + "", element, xdoc.Root.ToString());
                        break;
                }
            }
        }

        internal override void Export(string table)
        {

            var vals = new List<object>
            {
                ID, 
                Type, 
                Depth
            };

            Database.ExportWorldItem(table, vals);

            if (Coords != null)
            {
                int coordID = 0;
                foreach (var coord in Coords)
                {
                    Database.ExportWorldItem("UGRegion_Coords", new List<object> { ID, coordID, coord.X, coord.Y });
                    coordID++;
                }
            }
        }
    }
}

