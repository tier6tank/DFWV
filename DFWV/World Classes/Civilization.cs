using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace DFWV.WorldClasses
{

    class Civilization : Entity 
    {
        public bool isFull { get; set; }

        public List<God> Gods = new List<God>();
        public Dictionary<string, List<Leader>> Leaders = new Dictionary<string, List<Leader>>();
        
        public Entity Entity { get; set; }
        public Color Color { get; set; }
        public Site FirstSite { get; set; }

        new public string DispNameLower { get { return ToString().ToLower(); } }

        #region Parse from History File
        public Civilization(List<string> data, World world)
            : base(GetName(data[0]), world)
        {

            isFull = data.Count > 1;
            SetRace(data[0]);
            Race.isCivilized = isFull;

            if (!isFull)
                return;

            data.RemoveRange(0, 1);

            List<string> curList = new List<string>();
            foreach (string line in data)
            {
                if (!line.StartsWith("  ")) //Start of new list
                {
                    if (curList.Count > 0)
                    {
                        AddList(curList);
                        curList.Clear();
                    }
                }
                curList.Add(line);
            }
            if (curList.Count > 0)
                AddList(curList);

            if (Leaders.Count == 0 && Gods.Count == 0)
                isFull = false;
        }

        private void AddList(List<string> list)
        {
            if (list[0].Contains("Worship"))
                AddWorshipList(list);
            else
                AddLeaderList(list);
        }

        private void AddLeaderList(List<string> list)
        {
            string listname = list[0].Replace(" List", "").Trim();
            List<Leader> newLeaderList = new List<Leader>();
            list.RemoveAt(0);
            if (list.Count == 0)
                return;

            List<string> curLeader = new List<string>();
            foreach (string leaderline in list)
            {
                if (leaderline.StartsWith("  [*]"))//new Leader
                {
                    if (curLeader.Count > 0)
                    {
                        Leader newLeader = new Leader(curLeader, listname, this);
                        //Leader newLeader = World.GetAddLeader(name);
                        newLeaderList.Add(newLeader);
                        World.Leaders.Add(newLeader);
                        curLeader.Clear();
                    }
                }
                curLeader.Add(leaderline);
            }
            if (curLeader.Count > 0)
            {
                Leader newLeader = new Leader(curLeader, listname, this);
                newLeaderList.Add(newLeader);
                World.Leaders.Add(newLeader);
            }

            Leaders.Add(listname, newLeaderList);

        }

        private void AddWorshipList(List<string> list)
        {
            list.RemoveAt(0);
            foreach (string godline in list)
            {
                God thisGod = World.GetAddGod(new God(godline));

                thisGod.Civilizations.Add(this);
                Gods.Add(thisGod);
            }
        }

        private static string GetName(string data)
        {
            string[] split = data.Split(',');
            return split[0];
        }

        private void SetRace(string data)
        {
            string[] split = data.Split(',');
            Race = World.GetAddRace(split.Last<string>().Trim());
            Race.isCivilized = true;
        }
        #endregion

        public override void Select(MainForm frm)
        {
            frm.grpCivilization.Text = this.ToString();
            frm.grpCivilization.Show();

            frm.lblCivilizationName.Text = ToString();
            frm.lblCivilizationFull.Text = isFull ? "Yes" : "No";
            frm.lblCivilizationEntity.Data = Entity;
            frm.lblCivilizationRace.Data = Race;

            frm.grpCivilizationLeaders.Visible = Leaders.Count > 0;
            if (Leaders.Count > 0)
            {
                frm.lstCivilizationLeaders.BeginUpdate();
                frm.lstCivilizationLeaders.Items.Clear();
                foreach (List<Leader> leaderlist in Leaders.Values)
                {
                    foreach (Leader leader in leaderlist)
                    {
                        frm.lstCivilizationLeaders.Items.Add(leader);
                    }
                }
                frm.lstCivilizationLeaders.EndUpdate();
            }
            frm.grpCivilizationGods.Visible = Gods.Count > 0;
            if (Gods.Count > 0)
            {
                frm.lstCivilizationGods.BeginUpdate();
                frm.lstCivilizationGods.Items.Clear();

                foreach (God god in Gods)
                {
                    frm.lstCivilizationGods.Items.Add(god);
                }
                frm.lstCivilizationGods.EndUpdate();
            }

            frm.lstCivilizationSites.BeginUpdate();

            frm.lstCivilizationSites.Items.Clear();

            foreach (Site site in World.Sites.Values.Where(x => x.Parent != null && x.Parent == this))
            {
                frm.lstCivilizationSites.Items.Add(site);
            }
            frm.lstCivilizationSites.EndUpdate();

            frm.grpCivilization.Visible = frm.lstCivilizationSites.Items.Count > 0;

            Program.MakeSelected(frm.tabCivilization, frm.lstCivilization, this);
        }
    }
}
