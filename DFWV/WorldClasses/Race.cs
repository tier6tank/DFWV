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
        public bool IsCivilized { get; set; }
        public int AddedOrder { get; set; }
        public string Key { get; set; }
        [UsedImplicitly]
        public string PluralName { get; set; }
        [UsedImplicitly]
        public int CivCount { get { return World.Civilizations.Count(x => x.Race == this); } }
        [UsedImplicitly]
        public int LeaderCount { get { return World.Leaders.Count(x => x.Race == this); } }
        [UsedImplicitly]
        public int HfCount { get { return World.HistoricalFigures.Values.Count(x => x.Race == this); } }
        [UsedImplicitly]
        public string DispNameLower => ToString().ToLower();

        public static List<string> Flags = new List<string>();
        public List<short> Flag { get; set; }
        public static List<string> Spheres = new List<string>();
        public List<short> Sphere { get; set; }

        public List<Caste> Castes { get; set; }

        override public Point Location => Point.Empty;

        public Dictionary<Region, int> Populations
        {
            get
            {
                return World.Regions.Values.Where(region => region.Populations != null && region.Populations.ContainsKey(this)).ToDictionary(region => region, region => region.Populations[this]);
            }
        }

        public Dictionary<UndergroundRegion, int> UgPopulations
        {
            get
            {
                return World.UndergroundRegions.Values.Where(ugregion => ugregion.Populations != null && ugregion.Populations.ContainsKey(this)).ToDictionary(ugregion => ugregion, ugregion => ugregion.Populations[this]);
            }
        }

        public Race(string name, int addedorder, World world) 
            : base(world)
        {
            if (name.IsPlural())
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
            AddedOrder = addedorder;
            Id = addedorder;
        }

        public Race(XDocument xdoc, World world) 
            : base(world)
        {
            Plus(xdoc);
        }

        public override void Select(MainForm frm)
        {
            if (frm.grpRace.Text == ToString() && frm.MainTab.SelectedTab == frm.tabRace)
                return;
            Program.MakeSelected(frm.tabRace, frm.lstRace, this);

            frm.grpRace.Text = ToString();
            frm.grpRace.Show();

            frm.lblRaceName.Text = ToString();
            frm.lblRacePopulation.Text = Population == long.MaxValue ? "Unnumbered" :  Population.ToString();

            frm.grpRaceLeaders.FillListboxWith(frm.lstRaceLeaders, World.Leaders.Where(x => x.Race == this));
            frm.grpRaceCivilizations.FillListboxWith(frm.lstRaceCivilizations, World.Civilizations.Where(x => x.Race == this));
            frm.grpRaceHistoricalFigures.FillListboxWith(frm.lstRaceHistoricalFigures, World.HistoricalFigures.Values.Where(x => x.Race == this).Take(50000));
            if (frm.lstRaceHistoricalFigures.Items.Count == 50000)
                frm.grpRaceHistoricalFigures.Text = @"Historical Figures (50000+)";

            frm.grpRaceCastes.FillListboxWith(frm.lstRaceCastes, Castes);


            frm.lstRacePopulation.BeginUpdate();
            frm.lstRacePopulation.Tag = this;
            frm.lstRacePopulation.Items.Clear();
            var pops = Populations;
            if (pops.Count > 0)
                frm.lstRacePopulation.Items.AddRange(pops.Keys.ToArray());
            var ugpops = UgPopulations;
            if (ugpops.Count > 0)
                frm.lstRacePopulation.Items.AddRange(ugpops.Keys.ToArray());

            frm.lstRacePopulation.EndUpdate();
            if (frm.lstRacePopulation.Items.Count > 0)
                frm.grpRacePopulation.Text =
                    $"Population ({(pops.Values.Contains(10000001) || ugpops.Values.Contains(10000001) ? "Unnumbered" : (pops.Values.Sum() + ugpops.Values.Sum()).ToString())})";
            frm.grpRacePopulation.Visible = frm.lstRacePopulation.Items.Count > 0;
        }

        internal override void Export(string table)
        {

            var vals = new List<object>
            {
                Id,
                Key.DBExport(),
                Name.DBExport(),
                PluralName.DBExport(),
                IsCivilized,
                Population == long.MaxValue ? -1 : Population
            };

            Database.ExportWorldItem(table, vals);

            Castes?.ForEach(x => x.Export("Race_Castes"));
        }


        internal override void Link()
        {
            throw new NotImplementedException();
        }

        internal override void Process()
        {
            throw new NotImplementedException();
        }

        internal override sealed void Plus(XDocument xdoc)
        {
            foreach (var element in xdoc.Root.Elements())
            {
                var val = element.Value;
                int valI;
                int.TryParse(val, out valI);

                switch (element.Name.LocalName)
                {
                    case "id":
                        Id = valI;
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
                    case "flags":
                        var flags = val.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                        foreach (var flag in flags)
                        {
                            if (!Flags.Contains(flag))
                                Flags.Add(flag);
                            if (Flag == null)
                                Flag = new List<short>();
                            Flag.Add((short)Flags.IndexOf(flag));
                        }
                        break;
                    case "sphere":
                        if (!Spheres.Contains(val))
                            Spheres.Add(val);
                        if (Sphere == null)
                            Sphere = new List<short>();
                        break;
                    default:
                        DFXMLParser.UnexpectedXmlElement(xdoc.Root.Name.LocalName + "\t Race", element, xdoc.Root.ToString());
                        break;
                }
            }
        }

        public override int GetHashCode()
        {
            return AddedOrder.GetHashCode() * 7 + GetType().GetHashCode();
        }

        internal string PluralizeName()
        {
            return PluralName == string.Empty ? ToString().Pluralize() : PluralName.ToTitleCase();
        }
    }


}