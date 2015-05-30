using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Xml.Linq;
using DFWV.Annotations;

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
                return World.Regions.Values.Where(region => region.Populations != null && region.Populations.ContainsKey(this)).ToDictionary(region => region, region => region.Populations[this]);
            }
        }

        public Dictionary<UndergroundRegion, int> UGPopulations
        {
            get
            {
                return World.UndergroundRegions.Values.Where(ugregion => ugregion.Populations != null && ugregion.Populations.ContainsKey(this)).ToDictionary(ugregion => ugregion, ugregion => ugregion.Populations[this]);
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

                frm.grpRaceLeaders.FillListboxWith(frm.lstRaceLeaders, World.Leaders.Where(x => x.Race == this));
                frm.grpRaceCivilizations.FillListboxWith(frm.lstRaceCivilizations, World.Civilizations.Where(x => x.Race == this));
                frm.grpRaceHistoricalFigures.FillListboxWith(frm.lstRaceHistoricalFigures, World.HistoricalFigures.Values.Where(x => x.Race == this).Take(50000));
                if (frm.lstRaceHistoricalFigures.Items.Count == 50000)
                    frm.grpRaceHistoricalFigures.Text = "Historical Figures (50000+)";

                frm.grpRaceCastes.FillListboxWith(frm.lstRaceCastes, Castes);


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
                    frm.grpRacePopulation.Text = string.Format("Population ({0})", pops.Values.Contains(10000001) || ugpops.Values.Contains(10000001) ? "Unnumbered" : (pops.Values.Sum() + ugpops.Values.Sum()).ToString());
                frm.grpRacePopulation.Visible = frm.lstRacePopulation.Items.Count > 0;
            }
            finally
            {
                Program.MakeSelected(frm.tabRace, frm.lstRace, this);
            }
        }

        internal override void Export(string table)
        {

            var vals = new List<object>
            {
                ID,
                Key.DBExport(),
                Name.DBExport(),
                PluralName.DBExport(),
                isCivilized,
                Population == Int64.MaxValue ? -1 : Population
            };

            Database.ExportWorldItem(table, vals);

            Castes.ForEach(x => x.Export("Race_Castes"));

        }


        internal override void Link()
        {
            throw new NotImplementedException();
        }

        internal override void Process()
        {
            throw new NotImplementedException();
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