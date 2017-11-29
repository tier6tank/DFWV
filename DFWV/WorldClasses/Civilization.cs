using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using DFWV.Annotations;
using DFWV.WorldClasses.EntityClasses;
using System;
using DFWV.WorldClasses.HistoricalEventClasses;
using DFWV.WorldClasses.HistoricalEventCollectionClasses;

namespace DFWV.WorldClasses
{
    public class Civilization : Entity 
    {
        public bool IsFull { get; }

        public readonly List<God> Gods = new List<God>();
        public readonly Dictionary<string, List<Leader>> Leaders = new Dictionary<string, List<Leader>>();

        [UsedImplicitly]
        public new string DispNameLower => ToString().ToLower();

        public Entity Entity { get; set; }
        public Color Color { get; set; }
        public Site FirstSite { get; set; }

        #region Parse from History File
        public Civilization(List<string> data, World world)
            : base(GetName(data[0]), world)
        {
            Id = World.Civilizations.Count();
            IsFull = data.Count > 1;
            SetRace(data[0]);
            Race.IsCivilized = IsFull;

            if (!IsFull)
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
                IsFull = false;
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

            newLeaderList.Last().IsCurrent = true;
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
            Race.IsCivilized = true;
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
            frm.lblCivilizationFull.Text = IsFull ? "Yes" : "No";
            frm.lblCivilizationEntity.Data = Entity;
            frm.lblCivilizationRace.Data = Race;

            frm.grpCivilizationLeaders.FillListboxWith(frm.lstCivilizationLeaders, Leaders.Values.SelectMany(leaderlist => leaderlist));
            frm.grpCivilizationGods.FillListboxWith(frm.lstCivilizationGods, Gods);
            frm.grpCivilizationSites.FillListboxWith(frm.lstCivilizationSites, World.Sites.Values.Where(x => x.Parent != null && x.Parent == this));
            frm.grpCivilizationWars.FillListboxWith(frm.lstCivilizationWars, Entity?.WarEventCollections);

            WriteCivilizationSummary(frm);
        }

        #region Displaying Summary

        public bool IsAlive => World.Sites.Values.Any(x => x.Parent != null && x.Parent == this);
        public string IsWas => IsAlive ? "is" : "was";

        private void WriteCivilizationSummary(MainForm frm)
        {

            var rtb = frm.rtbCivilizationSummary;
            try
            {
                rtb.Clear();
                rtb.Tag = new List<WorldObject>();

                rtb.SelectionFont = new Font(rtb.SelectionFont.FontFamily, rtb.SelectionFont.Size, FontStyle.Bold);
                rtb.AddText(ToString());
                rtb.SelectionFont = new Font(rtb.SelectionFont.FontFamily, rtb.SelectionFont.Size, FontStyle.Regular);
                rtb.AddText($" {IsWas} a ");
                rtb.AddLink(Race);
                rtb.AddText($" civilization.  ");

                var foundingEvent = Entity?.Events.OfType<HE_CreatedSite>().FirstOrDefault() as HE_CreatedSite;

                if (foundingEvent != null)
                {
                    rtb.AddText("It ");
                    rtb.AddLink(foundingEvent, "was founded");
                    rtb.AddText(" at ");
                    rtb.AddLink(foundingEvent.Site);
                    rtb.AddText(" by ");
                    rtb.AddLink(foundingEvent.Entity_SiteCiv);
                    rtb.AddText(".  ");
                }

                var firstLeader = World.Leaders.First(x => x.Civilization == this);
                var currentLeader = Leaders[Leader.LeaderTypes[firstLeader.LeaderType]].Last();

                var andOnly = (firstLeader == currentLeader) ? "and only " : "";
                rtb.AddText($" The first {andOnly}{Leader.LeaderTypes[firstLeader.LeaderType]} was ");
                rtb.AddLink(firstLeader);
                rtb.AddText(".  ");

                var siteCreatedEvents = Entity?.Events.OfType<HE_CreatedSite>().Skip(1).ToArray();

                if (siteCreatedEvents != null && siteCreatedEvents.Count() > 0)
                { 
                    rtb.AddText($"It founded {siteCreatedEvents.Count()} other sites: ");

                    for (int i = 0; i < siteCreatedEvents.Count(); i++)
                    {
                        rtb.AddLink(siteCreatedEvents[i].Site);
                        if (i < siteCreatedEvents.Count() - 1)
                        {
                            rtb.AddText(", ");
                        }
                    }
                    rtb.AddText(".  ");
                }

                if (currentLeader != firstLeader)
                {
                    rtb.AddText($"The {(IsAlive ? "current" : "last")} {Leader.LeaderTypes[currentLeader.LeaderType]} {IsWas} ");
                    rtb.AddLink(currentLeader);
                    rtb.AddText(".  ");
                }

                if (Entity?.WarEventCollections != null && Entity?.WarEventCollections.Count() > 0)
                {
                    var aggressorWars = Entity.WarEventCollections.Where(x => x.AggressorEnt == Entity);
                    var defenderWars = Entity.WarEventCollections.Where(x => x.DefenderEnt == Entity);
                    if (aggressorWars.Any())
                    {
                        var battles = aggressorWars.SelectMany(x => x.EventCol).OfType<HistoricalEventCollectionClasses.EC_Battle>().ToArray();
                        rtb.AddText($"It initiated {aggressorWars.Count()} wars ");
                        rtb.AddText($"winning {battles.Count(x => x.Outcome == "attacker won")} ");
                        rtb.AddText($"of {battles.Count()} battles, it took {aggressorWars.Sum(x => x.WarData.AttackingDeaths)} causalties while killing {aggressorWars.Sum(x => x.WarData.DefendingDeaths)}.  ");

                    }
                    if (defenderWars.Any())
                    {
                        var battles = defenderWars.SelectMany(x => x.EventCol).OfType<HistoricalEventCollectionClasses.EC_Battle>().ToArray();

                        rtb.AddText($"It was attacked in {defenderWars.Count()} wars ");
                        rtb.AddText($"winning {battles.Count(x => x.Outcome != "attacker won")} ");
                        rtb.AddText($"of {battles.Count()} battles, it took {defenderWars.Sum(x => x.WarData.DefendingDeaths)} causalties while killing {defenderWars.Sum(x => x.WarData.AttackingDeaths)}.  ");


                    }

                    var existingTime = IsAlive ? new WorldTime(World.LastYear) : (Entity != null && Entity.Events.Any() ? Entity.Events.Last().Time : new WorldTime(World.LastYear));
                    var warTime = GetTimeAtWar(existingTime);

                    rtb.AddText($" It's been at war for {WorldTime.Duration(warTime, new WorldTime(0))}, {(int)(warTime.ToSeconds()/(float)existingTime.ToSeconds() * 100)}% of the time.");
                }


            }
            catch (Exception e)
            {
                rtb.Clear();
                rtb.AddText("Error generating Civilization Summary: " + e);
            }
        }

        private WorldTime GetTimeAtWar(WorldTime endAtTime)
        {
            var warCollections = Entity?.WarEventCollections.OrderBy(x => x.StartTime.ToSeconds()).ToList();

            EC_War war = warCollections.First();
            WorldTime curStartTime;
            WorldTime timeAtWar = new WorldTime(0);

            do
            {
                var endTime = war.EndTime < endAtTime ? war.EndTime : endAtTime;
                curStartTime = war.StartTime;
                timeAtWar += (endTime - curStartTime);
                if (endTime == endAtTime)
                    break;
                warCollections = warCollections.Where(x => x.EndTime > war.EndTime).OrderBy(x => x.StartTime.ToSeconds()).ToList();
                war = warCollections.FirstOrDefault();
            } while (warCollections.Count() > 0);

            return timeAtWar;
        }


        #endregion
    }
}