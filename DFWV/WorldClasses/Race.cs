using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using DFWV.Annotations;
using System.Xml.Linq;


namespace DFWV.WorldClasses
{
    public class Race : XMLObject
    {
        [UsedImplicitly]
        public long Population { private get; set; }
        public bool isCivilized { get; set; }

        public string Key { get; set; }
        [UsedImplicitly]
        public string PluralName { get; set; }
        [UsedImplicitly]
        public int CivCount { get { return World.Civilizations.Count(x => x.Race == this); } }
        [UsedImplicitly]
        public int LeaderCount { get { return World.Leaders.Count(x => x.Race == this); } }
        [UsedImplicitly]
        public int HFCount { get { return World.HistoricalFigures.Values.Count(x => x.Race == this); } }
        [UsedImplicitly]
        public string DispNameLower { get { return ToString().ToLower(); } }


        public List<Caste> Castes { get; set; }

        override public Point Location { get { return Point.Empty; } }

        public Dictionary<Region, int> Populations
        {
            get 
            {
                var populations = new Dictionary<Region, int>();
                foreach (var region in World.Regions.Values)
                {
                    if (region.Populations.ContainsKey(this))
                        populations.Add(region, region.Populations[this]);
                }
                return populations;
            }

        }

        public Dictionary<UndergroundRegion, int> UGPopulations
        {
            get
            {
                var populations = new Dictionary<UndergroundRegion, int>();
                foreach (var ugregion in World.UndergroundRegions.Values)
                {
                    if (ugregion.Populations.ContainsKey(this))
                        populations.Add(ugregion, ugregion.Populations[this]);
                }
                return populations;
            }

        }

        public Race(string name, int id, World world) 
            : base(world)
        {
            if (name.isPlural())
            {
                Name = name.Singularize();
                PluralName = name;
            }
            else
            {
                Name = name;
                PluralName = name.Pluralize();
            }
            Key = Name;
            ID = id;
        }

        public Race(XDocument xdoc, World world) 
            : base(world)
        {
            Plus(xdoc);
        }

        public override void Select(MainForm frm)
        {
            try
            {
                frm.grpRace.Text = ToString();
                frm.grpRace.Show();

                frm.lblRaceName.Text = ToString();
                frm.lblRacePopulation.Text = Population == Int64.MaxValue ? "Unnumbered" :  Population.ToString();

                frm.lstRaceLeaders.BeginUpdate();
                frm.lstRaceLeaders.Items.Clear();
                frm.lstRaceLeaders.Items.AddRange(World.Leaders.Where(x => x.Race == this).ToArray());
                frm.lstRaceLeaders.EndUpdate();
                frm.grpRaceLeaders.Name = "Leaders (" + frm.lstRaceLeaders.Items.Count + ")";

                frm.lstRaceCivilizations.BeginUpdate();
                frm.lstRaceCivilizations.Items.Clear();
                frm.lstRaceCivilizations.Items.AddRange(World.Civilizations.Where(x => x.Race == this).ToArray());
                frm.lstRaceCivilizations.EndUpdate();
                frm.grpRaceCivilizations.Name = "Civilizations (" + frm.lstRaceCivilizations.Items.Count + ")";

                frm.lstRaceHistoricalFigures.BeginUpdate();
                frm.lstRaceHistoricalFigures.Items.Clear();
                frm.lstRaceHistoricalFigures.Items.AddRange(World.HistoricalFigures.Values.Where(x => x.Race == this).Take(50000).ToArray());
                frm.lstRaceHistoricalFigures.EndUpdate();
                frm.grpRaceHistoricalFigures.Name = "Historical Figures (" + frm.lstRaceHistoricalFigures.Items.Count + (frm.lstRaceHistoricalFigures.Items.Count == 50000 ? "+" : "") + ")";


                frm.lstRaceCastes.BeginUpdate();
                frm.lstRaceCastes.Items.Clear();
                if (Castes != null)
                    frm.lstRaceCastes.Items.AddRange(Castes.ToArray());
                frm.lstRaceCastes.EndUpdate();
                frm.grpRaceCastes.Visible = frm.lstRaceCastes.Items.Count > 0;

                frm.lstRacePopulation.BeginUpdate();
                frm.lstRacePopulation.Items.Clear();
                var pops = Populations;
                if (pops.Count > 0)
                    frm.lstRacePopulation.Items.AddRange(pops.Keys.ToArray());
                var ugpops = UGPopulations;
                if (ugpops.Count > 0)
                    frm.lstRacePopulation.Items.AddRange(ugpops.Keys.ToArray());

                frm.lstRacePopulation.EndUpdate();
                if (frm.lstRacePopulation.Items.Count > 0)
                    frm.grpRacePopulation.Text = "Population (" + (pops.Values.Sum() + ugpops.Values.Sum()) + ")";
                frm.grpRacePopulation.Visible = frm.lstRacePopulation.Items.Count > 0;
                
            }
            finally
            {
                frm.grpRaceLeaders.Visible = frm.lstRaceLeaders.Items.Count > 0;
                frm.grpRaceCivilizations.Visible = frm.lstRaceCivilizations.Items.Count > 0;
                frm.grpRaceHistoricalFigures.Visible = frm.lstRaceHistoricalFigures.Items.Count > 0;
                Program.MakeSelected(frm.tabRace, frm.lstRace, this);
            }
        }

        internal override void Export(string table)
        {

            var vals = new List<object>
            {
                Name.DBExport(),
                PluralName.DBExport(),
                isCivilized,
                Population == Int64.MaxValue ? -1 : Population
            };

            Database.ExportWorldItem(table, vals);
        }


        internal override void Link()
        {
            throw new NotImplementedException();
        }

        internal override void Process()
        {
            throw new NotImplementedException();
        }

        internal override void Plus(System.Xml.Linq.XDocument xdoc)
        {
            foreach (var element in xdoc.Root.Elements())
            {
                var val = element.Value;
                int valI;
                Int32.TryParse(val, out valI);

                switch (element.Name.LocalName)
                {
                    case "id":
                        ID = valI;
                        break;
                    case "key":
                        Key = val.ToLower();
                        break;
                    case "nameS":
                        Name = val.ToLower();
                        break;
                    case "nameP":
                        PluralName = val.ToLower();
                        break;
                    case "caste":
                        var newCaste = new Caste(element, this);
                        if (Castes == null)
                            Castes = new List<Caste>();
                        Castes.Add(newCaste);
                        break;
                    default:
                        DFXMLParser.UnexpectedXMLElement(xdoc.Root.Name.LocalName + "\t Race", element, xdoc.Root.ToString());
                        break;
                }
            }
        }
    }


}
