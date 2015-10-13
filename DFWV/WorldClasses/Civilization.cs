using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using DFWV.Annotations;
using DFWV.WorldClasses.EntityClasses;

namespace DFWV.WorldClasses
{
    public class Civilization : Entity 
    {
        public bool isFull { get; private set; }

        public readonly List<God> Gods = new List<God>();
        public readonly Dictionary<string, List<Leader>> Leaders = new Dictionary<string, List<Leader>>();

        [UsedImplicitly]
        public new string DispNameLower { get { return ToString().ToLower(); } }
        public Entity Entity { get; set; }
        public Color Color { get; set; }
        public Site FirstSite { get; set; }

        #region Parse from History File
        public Civilization(List<string> data, World world)
            : base(GetName(data[0]), world)
        {
            ID = World.Civilizations.Count();
            isFull = data.Count > 1;
            SetRace(data[0]);
            Race.isCivilized = isFull;

            if (!isFull)
                return;

            data.RemoveRange(0, 1);

            var curList = new List<string>();
            foreach (var line in data)
            {
                if (!line.StartsWith("  ")
                    || line == "  List")  // Mods might have untitled leaders
                { //Start of new list
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

        private void AddList(IList<string> list)
        {
            if (list[0].Contains("Worship"))
                AddWorshipList(list);
            else
                AddLeaderList(list);
        }

        private void AddLeaderList(IList<string> list)
        {
            var listname = list[0].Replace(" List", "").Trim();
            if (listname == "") // Mods might have untitled leaders
                listname = "untitled";
            var newLeaderList = new List<Leader>();
            list.RemoveAt(0);
            if (list.Count == 0)
                return;

            var curLeader = new List<string>();
            foreach (var leaderline in list)
            {
                if (leaderline.StartsWith("  [*]"))//new Leader
                {
                    if (curLeader.Count > 0)
                    {
                        var newLeader = new Leader(curLeader, listname, this);
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
                var newLeader = new Leader(curLeader, listname, this);
                newLeaderList.Add(newLeader);
                World.Leaders.Add(newLeader);
            }

            newLeaderList.Last().isCurrent = true;
            Leaders.Add(listname, newLeaderList);

        }

        private void AddWorshipList(IList<string> list)
        {
            list.RemoveAt(0);
            foreach (var thisGod in list.Select(godline => World.GetAddGod(new God(godline))))
            {
                thisGod.Civilizations.Add(this);
                Gods.Add(thisGod);
            }
        }

        private static string GetName(string data)
        {
            var split = data.Split(',');
            return split[0];
        }

        private void SetRace(string data)
        {
            var split = data.Split(',');
            Race = World.GetAddRace(split.Last().Trim());
            Race.isCivilized = true;
        }
        #endregion

        public override void Select(MainForm frm)
        {
            if (frm.grpCivilization.Text == ToString() && frm.MainTab.SelectedTab == frm.tabCivilization)
                return;
            Program.MakeSelected(frm.tabCivilization, frm.lstCivilization, this);

            frm.grpCivilization.Text = ToString();
            frm.grpCivilization.Show();

            frm.lblCivilizationName.Text = ToString();
            frm.lblCivilizationFull.Text = isFull ? "Yes" : "No";
            frm.lblCivilizationEntity.Data = Entity;
            frm.lblCivilizationRace.Data = Race;

            frm.grpCivilizationLeaders.FillListboxWith(frm.lstCivilizationLeaders, Leaders.Values.SelectMany(leaderlist => leaderlist));
            frm.grpCivilizationGods.FillListboxWith(frm.lstCivilizationGods, Gods);
            frm.grpCivilizationSites.FillListboxWith(frm.lstCivilizationSites, World.Sites.Values.Where(x => x.Parent != null && x.Parent == this));
            frm.grpCivilizationWars.FillListboxWith(frm.lstCivilizationWars, (Entity != null && Entity.WarEventCollections != null) ? Entity.WarEventCollections : null);


        }
    }
}