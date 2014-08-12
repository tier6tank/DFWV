using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DFWV.WorldClasses.HistoricalFigureClasses;

namespace DFWV.WorldClasses
{
    class Leader : Person
    {

        public Civilization Civilization { get; set; }
        public string LeaderType { get; set; }
        public WorldTime Birth { get; set; }
        public WorldTime Death { get; set; }
        public WorldTime ReignBegan { get; set; }
        public string Inheritance { get; set; }
        public string InheritedFromName { get; set; }
        public int? InheritanceID { get; set; }
        public HistoricalFigure InheritedFrom { get; set; }
        public bool Married { get; set; }
        public WorldTime MarriedDeath { get; set; }
        public List<Child> Children { get; set; }
        public int ChildrenCount { get; set; }

        public God Worship { get; set; }
        public int WorshipPercent { get; set; }

        public Site Site { get; set; }
        public Race Race { get; set; }
        public Civilization SiteParent { get; set; }

        public HistoricalFigure HF { get; set; }

        public string DispNameLower { get { return ToString().ToLower(); } }

        #region Parse From History File
        public Leader(List<string> data, string leadertype, Civilization civ)
        {
            Civilization = civ;
            LeaderType = leadertype;
            ParseNameLine(data[0].Trim());
            ParseChildrenLine(data[1].Trim());
            if (data.Count == 3)
                ParseWorshipLine(data[2].Trim());
        }

        private void ParseNameLine(string data)
        {
            data = data.Replace("[*]", "").Trim();
            Name = data.Split('(')[0].Trim();

            data = data.Remove(0, Name.Length + 2);
            string dates = data.Split(')')[0];
            if (!dates.Contains("b.???"))
            {
                if (dates.Contains("d."))
                    Birth = new WorldTime(Convert.ToInt32(dates.Substring(2, dates.IndexOf("d.") - 2)));
                else
                    Birth = new WorldTime(Convert.ToInt32(dates.Substring(2, dates.IndexOf(",") - 2)));
            }
            if (dates.Contains("d."))
                Death = new WorldTime(Convert.ToInt32(dates.Substring(dates.IndexOf("d. ") + 3, dates.IndexOf(',') - dates.IndexOf("d. ") - 3)));
            else
                Death = WorldTime.Present;
            ReignBegan = new WorldTime(Convert.ToInt32(dates.Split(':')[1]));
            data = data.Remove(0, dates.Length + 2).Trim();
            Inheritance = data.Split(',')[0];
            if (Inheritance.Contains("from"))
            {
                InheritedFromName = Inheritance.Substring(Inheritance.IndexOf(" from ") + 6);
                Inheritance = "Inherited";
            }
            else
                Inheritance = Inheritance.Replace("***", "").Trim();

            data = data.Split(',')[1];

            Married = !data.Contains("Never");
            if (data.Contains("d."))
                MarriedDeath = new WorldTime(Convert.ToInt32(data.Substring(data.IndexOf("d.") + 3, data.IndexOf(')') - data.IndexOf("d.") - 3)));
        }

        private void ParseChildrenLine(string data)
        {
            if (data == "No Children")
                return;
            int ChildrenCount = Convert.ToInt32(data.Split(' ')[0]);

            string dates = data.Split(':')[1];

            dates = dates.Replace("d. ", "d.").Trim();

            if (Children == null)
                Children = new List<Child>();
            foreach (string date in dates.Split(" ".ToCharArray(),StringSplitOptions.RemoveEmptyEntries))
                Children.Add(new Child(date, this));


        }

        private void ParseWorshipLine(string data)
        {
            data = data.Replace("Worshipped", "").Replace("Worships", "").Trim();
            string godName = data.Split('(')[0].Trim();
            foreach (God curgod in Civilization.Gods)
            {
                if (curgod.Name == godName)
                {
                    Worship = curgod;
                    Worship.Leaders.Add(this);
                    break;
                }
            }
            if (Worship == null)
            {
                God thisGod = Civilization.World.GetAddGod(godName);
                thisGod.Leaders.Add(this);
                Worship = thisGod;
            }

            WorshipPercent = Convert.ToInt32(data.Substring(data.IndexOf("(") + 1, data.IndexOf(")") - data.IndexOf("(") - 2));
        }
        #endregion

        #region Parse From Site File
        public Leader(string leaderName)
        {
            Name = leaderName;
        }
        #endregion

        public void LinkInheritance()
        {
            switch (InheritedFromName)
            {
                case "father":
                    InheritanceID = HF.HFLinks["father"][0].LinkedHFID;
                    break;
                case "mother":
                    InheritanceID = HF.HFLinks["mother"][0].LinkedHFID;
                    break;
                case "paternal grandmother":
                    InheritanceID = -1;
                    break;
                case "maternal grandmother":
                    InheritanceID = -1;
                    break;
                case "paternal grandfather":
                    InheritanceID = -1;
                    break;
                case "maternal grandfather":
                    InheritanceID = -1;
                    break;
                default:
                    break;
            }
        }

        public override void Select(MainForm frm)
        {
            frm.grpLeader.Text = this.ToString();
            frm.grpLeader.Show();

            frm.lblLeaderName.Text = ToString();
            frm.lblLeaderType.Text = LeaderType;
            frm.lblLeaderRace.Data = Race;
            frm.lblLeaderLife.Text = Birth == null ? "" : (Birth.ToString() + " – " + (Death == WorldTime.Present ? "" : Death.ToString()));
            frm.lblLeaderReignBegan.Text = ReignBegan == null ? "" : ReignBegan.ToString();
            frm.lblLeaderInheritance.Text = Inheritance;
            frm.lblLeaderInheritedFrom.Data = InheritedFrom;
            frm.lblLeaderCivilization.Data = Civilization;
            frm.lblLeaderSite.Data = Site;
            frm.lblLeaderGod.Data = Worship;
            if (Worship != null)
                frm.lblLeaderGod.Text = Worship.ToString() + " (" + WorshipPercent + "%)";
            frm.lblLeaderHF.Data = HF;

            Program.MakeSelected(frm.tabLeader, frm.lstLeader, this);
        }


    }
}
