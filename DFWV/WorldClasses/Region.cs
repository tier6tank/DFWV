using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Xml.Linq;
using DFWV.Annotations;
using DFWV.WorldClasses.HistoricalEventClasses;
using DFWV.WorldClasses.HistoricalEventCollectionClasses;
using DFWV.WorldClasses.HistoricalFigureClasses;

namespace DFWV.WorldClasses
{
    public class Region : XMLObject
    {
        public static List<string> Types = new List<string>();
        public int Type { get; private set; }

        public List<Point> Coords { get; set; }
        public Dictionary<Race, int> Populations { get; set; }

        public List<HistoricalFigure> Inhabitants { get; set; }


        public List<EC_Battle> BattleEventCollections { get; set; }
        public List<EC_Duel> DuelEventCollections { get; set; }
        public List<EC_Abduction> AbductionEventCollections { get; set; }
        public List<EC_Theft> TheftEventCollections { get; set; }

        public IEnumerable<HistoricalEvent> Events
        {
            get
            {
                return World.HistoricalEvents.Values.Where(x => x.RegionsInvolved.Contains(this));
            }
        }

        [UsedImplicitly]
        public int EventCount { get; set; }

        [UsedImplicitly]
        public int Battles { get { return BattleEventCollections == null ? 0 : BattleEventCollections.Count; } }
        [UsedImplicitly]
        public int InhabitantCount { get { return Inhabitants == null ? 0 : Inhabitants.Count; } }
        [UsedImplicitly]
        public string DispNameLower { get { return ToString().ToLower(); } }
        [UsedImplicitly]
        public string RegionType { get { return Types[Type]; } }

        override public Point Location { get { return Coords != null  ? Coords[0] : Point.Empty; } }

        public Region(XDocument xdoc, World world)
            : base(xdoc, world)
        {
            foreach (var element in xdoc.Root.Elements())
            {
                var val = element.Value;

                switch (element.Name.LocalName)
                {
                    case "id":
                        break;
                    case "name":
                        Name = val;
                        break;
                    case "type":
                        if (!Types.Contains(val))
                            Types.Add(val);
                        Type = Types.IndexOf(val);
                        break;
                    default:
                        DFXMLParser.UnexpectedXMLElement(xdoc.Root.Name.LocalName, element, xdoc.Root.ToString());
                        break;
                }
            }

        }

        public override void Select(MainForm frm)
        {
            frm.grpRegion.Text = ToString();
#if DEBUG
            frm.grpRegion.Text += string.Format(" - ID: {0}", ID);
#endif
            frm.grpRegion.Show();

            frm.lblRegionName.Text = ToString();
            frm.lblRegionType.Text = Types[Type].ToTitleCase();

            frm.grpRegionEvents.FillListboxWith(frm.lstRegionEvents, Events);
            frm.grpRegionBattles.FillListboxWith(frm.lstRegionBattles, BattleEventCollections);
            frm.grpRegionInhabitants.FillListboxWith(frm.lstRegionInhabitants, Inhabitants);


            if (Populations != null)
            {
                frm.grpRegionPopulation.FillListboxWith(frm.lstRegionPopulation, Populations.Keys);
                frm.grpRegionPopulation.Text = string.Format("Population ({0})", Populations.Values.Contains(10000001) ? "Unnumbered" : Populations.Values.Sum().ToString());
                frm.grpRegionPopulation.Visible = true;
            }
            else
            {
                frm.grpRegionPopulation.Visible = false;
            }


            Program.MakeSelected(frm.tabRegion, frm.lstRegion, this);
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
                            if (Populations.ContainsKey(World.Races[Convert.ToInt32(popSplit[0])]))
                                Populations[World.Races[Convert.ToInt32(popSplit[0])]] += Convert.ToInt32(popSplit[1]);
                            else
                                Populations.Add(World.Races[Convert.ToInt32(popSplit[0])], Convert.ToInt32(popSplit[1]));
                        }
                        break;
                    default:
                        DFXMLParser.UnexpectedXMLElement(xdoc.Root.Name.LocalName + "\t" + "Region", element, xdoc.Root.ToString());
                        break;
                }
            }

        }

        internal override void Export(string table)
        {

            var vals = new List<object>
            {
                ID,
                Name.DBExport(),
                Type
            };

            Database.ExportWorldItem(table, vals);

            if (Coords != null)
            {
                int coordID = 0;
                foreach (var coord in Coords)
                {
                    Database.ExportWorldItem("Region_Coords", new List<object> { ID, coordID, coord.X, coord.Y });
                    coordID++;
                }
            }

            if (Populations != null)
            {
                foreach (var pop in Populations)
                {
                    Database.ExportWorldItem("Region_Population", new List<object> { ID, pop.Key.ID, pop.Value });
                }
            }

        }
    }
}