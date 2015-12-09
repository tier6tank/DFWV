using System;
using System.Collections.Generic;
using System.Linq;
using DFWV.Annotations;
using DFWV.WorldClasses.HistoricalFigureClasses;

namespace DFWV.WorldClasses
{
    public class God : Person
    {
        public int ID { get; set; }
        public static List<string> Types = new List<string>();
        public int Type { get; set; }
        [UsedImplicitly]
        public string GodType => Types[Type];

        public readonly List<Civilization> Civilizations = new List<Civilization>();
        public readonly List<int> Spheres = new List<int>();
        public readonly List<Leader> Leaders = new List<Leader>();

        public HistoricalFigure HF { get; set; }

        [UsedImplicitly]
        public string RaceName => HF != null && HF.Race != null ? HF.Race.Name : "";

        [UsedImplicitly]
        public int CivilizationCount => Civilizations == null ? 0 : Civilizations.Count;

        [UsedImplicitly]
        public int SphereCount => Spheres == null ? 0 : Spheres.Count;

        [UsedImplicitly]
        public int LeaderCount => Leaders == null ? 0 : Leaders.Count;

        public God(string data)
        {
            if (data.Contains(':'))
            {
                //  Erib Oiledrock the Mountain of Gravel, deity: metals
                var spherenames = data.Split(':').Last().Trim().Split(',').ToList().Select(x=>x.Trim());

                foreach (var spherename in spherenames)
                {
                    if (!HistoricalFigure.Spheres.Contains(spherename))
                        HistoricalFigure.Spheres.Add(spherename);

                    Spheres.Add(HistoricalFigure.Spheres.IndexOf(spherename));
                }

                Name = data.Split(':')[0].Split(',')[0].Trim();
                var valType = data.Split(':')[0].Split(',')[1].Trim();

                if (!Types.Contains(valType))
                    Types.Add(valType);
                Type = Types.IndexOf(valType);

            }
            else
                Name = data;

        }
        
        public override string ToString()
        {
            return Name;
        }

        public override void Select(MainForm frm)
        {
            if (frm.grpGod.Text == ToString() && frm.MainTab.SelectedTab == frm.tabGod)
                return;
            Program.MakeSelected(frm.tabGod, frm.lstGod, this);

            frm.grpGod.Text = ToString();
            frm.grpGod.Show();

            frm.lblGodName.Text = ToString();
            frm.lblGodType.Text = Types[Type].ToTitleCase();
            frm.lblGodHF.Data = HF;

            
            frm.lblGodSpheres.Text = string.Join(", ", Spheres.Select(sphere => HistoricalFigure.Spheres[sphere]).ToList()).ToTitleCase();

            frm.grpGodLeaders.FillListboxWith(frm.lstGodLeaders, Leaders);
            frm.grpGodCivilizations.FillListboxWith(frm.lstGodCivilizations, Civilizations);


        }

        internal override void Export(string table)
        {
            var vals = new List<object>
            {
                ID,
                Name.DBExport(),
                GodType.DBExport(),
                HF.DBExport(),
                RaceName.DBExport()
            };

            Database.ExportWorldItem(table, vals);

            foreach (var sphere in Spheres)
            {
                Database.ExportWorldItem("God_Sphere", new List<object> {ID, HistoricalFigure.Spheres[sphere]});
            }
        }

    }
}