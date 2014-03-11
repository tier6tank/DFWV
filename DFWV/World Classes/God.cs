using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DFWV.WorldClasses.HistoricalFigureClasses;

namespace DFWV.WorldClasses
{
    class God : Person
    {
        public string Type { get; set; }
        public List<Civilization> Civilizations = new List<Civilization>();
        public List<string> Spheres = new List<string>();
        public List<Leader> Leaders = new List<Leader>();

        public HistoricalFigure HF { get; set; }


        public int CivilizationCount { get { return Civilizations == null ? 0 : Civilizations.Count; } }
        public int SphereCount { get { return Spheres == null ? 0 : Spheres.Count; } }
        public int LeaderCount { get { return Leaders == null ? 0 : Leaders.Count; } }
        public string DispNameLower { get { return ToString().ToLower(); } }

        public God(string data)
        {
            if (data.Contains(':'))
            {
                //  Erib Oiledrock the Mountain of Gravel, deity: metals
                Spheres = data.Split(':').Last<string>().Split(',').ToList<string>();
                Name = data.Split(':')[0].Split(',')[0].Trim();
                Type = data.Split(':')[0].Split(',')[1].Trim();

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
            frm.grpGod.Text = this.ToString();
            frm.grpGod.Show();

            frm.lblGodName.Text = ToString();
            frm.lblGodType.Text = Type == null ? "" : CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Type);
            frm.lblGodHF.Data = HF;
            frm.lblGodSpheres.Text = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(String.Join(", ", Spheres));

            frm.grpGodLeaders.Visible = Leaders.Count > 0;
            if (Leaders != null)
            {
                frm.lstGodLeaders.Items.Clear();
                foreach (Leader leader in Leaders)
                {
                    frm.lstGodLeaders.Items.Add(leader);
                }
            }

            frm.grpGodCivilizations.Visible = Civilizations.Count > 0;
            if (Civilizations != null)
            {
                frm.lstGodCivilizations.Items.Clear();
                foreach (Civilization civ in Civilizations)
                {
                    frm.lstGodCivilizations.Items.Add(civ);
                }
            }

            Program.MakeSelected(frm.tabGod, frm.lstGod, this);
        }

    }
}
