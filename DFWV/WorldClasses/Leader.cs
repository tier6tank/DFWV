﻿using System;
using System.Collections.Generic;
using System.Linq;
using DFWV.Annotations;
using DFWV.WorldClasses.HistoricalFigureClasses;

namespace DFWV.WorldClasses
{
    public class Leader : Person
    {
        public enum InheritanceSource
        {
            None,
            Mother,
            Father,
            PaternalGrandMother,
            MaternalGrandMother,
            PaternalGrandFather,
            MaternalGrandFather,
            Other
        }

        public Civilization Civilization { get; set; }
        public static List<string> LeaderTypes = new List<string>();
        public int LeaderType { get; set; }
        public WorldTime Birth { get; private set; }
        public WorldTime Death { get; private set; }
        public WorldTime ReignBegan { get; private set; }
        public static List<string> InheritanceTypes = new List<string> { "Unknown", "Inherited", "New Line", "Original Line"};
        public int Inheritance { get; private set; }
        public bool IsCurrent { get; set; }

        [UsedImplicitly]
        public bool Inherited => Inheritance == InheritanceTypes.IndexOf("Inherited");

        public InheritanceSource InheritedFromSource { get; private set; }
        public int? InheritanceId { get; private set; }
        public HistoricalFigure InheritedFrom { get; set; }
        [UsedImplicitly]
        public bool Married { get; set; }
        public HistoricalFigure Spouse => Hf?.HfLinks == null 
            ? null 
            : (!Hf.HfLinks.ContainsKey(HFLink.LinkTypes.IndexOf("spouse")) 
                ? null
                : Hf.HfLinks[HFLink.LinkTypes.IndexOf("spouse")][0].Hf
                );

        public WorldTime MarriedDeath { get; set; }
        public List<Child> Children { get; set; }
        [UsedImplicitly]
        public int ChildrenCount { get; set; }

        private God Worship { get; set; }
        private int WorshipPercent { get; set; }

        public Site Site { get; set; }
        public Race Race { get; set; }
        [UsedImplicitly]
        public string RaceName => Race != null ? Race.Name : (Hf?.Race != null ? Hf.Race.Name : "");

        [UsedImplicitly]
        public Civilization SiteParent { get; set; }

        public HistoricalFigure Hf { get; set; }

        [UsedImplicitly]
        public string DispNameLower => ToString().ToLower();

        #region Parse From History File
        public Leader(IList<string> data, string leadertype, Civilization civ)
        {
            Civilization = civ;
            if (!LeaderTypes.Contains(leadertype))
                LeaderTypes.Add(leadertype);
            LeaderType = LeaderTypes.IndexOf(leadertype);

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
            var dates = data.Split(')')[0];
            if (!dates.Contains("b.???"))
            {
                Birth = dates.Contains("d.") ? new WorldTime(Convert.ToInt32(dates.Substring(2, dates.IndexOf("d.") - 2))) : new WorldTime(Convert.ToInt32(dates.Substring(2, dates.IndexOf(",") - 2)));
            }
            Death = dates.Contains("d.") ? new WorldTime(Convert.ToInt32(dates.Substring(dates.IndexOf("d. ") + 3, dates.IndexOf(',') - dates.IndexOf("d. ") - 3))) : WorldTime.Present;
            ReignBegan = new WorldTime(Convert.ToInt32(dates.Split(':')[1]));
            data = data.Remove(0, dates.Length + 2).Trim();

            var inheritanceText = data.Split(',')[0];
            if (inheritanceText.Contains("Inherited"))
            {
                switch (inheritanceText.Substring(inheritanceText.IndexOf(" from ") + 6))
                {
                    case "father":
                        InheritedFromSource = InheritanceSource.Father;
                        break;
                    case "mother":
                        InheritedFromSource = InheritanceSource.Mother;
                        break;
                    case "paternal grandmother":
                        InheritedFromSource = InheritanceSource.PaternalGrandMother;
                        break;
                    case "maternal grandmother":
                        InheritedFromSource = InheritanceSource.MaternalGrandMother;
                        break;
                    case "paternal grandfather":
                        InheritedFromSource = InheritanceSource.PaternalGrandFather;
                        break;
                    case "maternal grandfather":
                        InheritedFromSource = InheritanceSource.MaternalGrandFather;
                        break;
                    default:
                        InheritedFromSource = InheritanceSource.Other;
                        break;
                }
                inheritanceText = "Inherited";
            }
            else
            {
                inheritanceText = inheritanceText.Replace("***", "").Trim();
                if (!InheritanceTypes.Contains(inheritanceText))
                    InheritanceTypes.Add(inheritanceText);

                InheritedFromSource = InheritanceSource.None;
            }
            Inheritance = InheritanceTypes.IndexOf(inheritanceText);

            data = data.Split(',')[1];

            Married = !data.Contains("Never");
            if (data.Contains("d."))
                MarriedDeath = new WorldTime(Convert.ToInt32(data.Substring(data.IndexOf("d.") + 3, data.IndexOf(')') - data.IndexOf("d.") - 3)));
        }

        private void ParseChildrenLine(string data)
        {
            if (data == "No Children")
                return;

            var dates = data.Split(':')[1];

            dates = dates.Replace("d. ", "d.").Trim();
            var datesSplit = dates.Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            ChildrenCount = datesSplit.Count();

            if (Children == null)
                Children = new List<Child>();
            foreach (var date in datesSplit)
                Children.Add(new Child(date, this));


        }

        private void ParseWorshipLine(string data)
        {
            data = data.Replace("Worshipped", "").Replace("Worships", "").Trim();
            var godName = data.Split('(')[0].Trim();
            foreach (var curgod in Civilization.Gods.Where(curgod => curgod.Name == godName))
            {
                Worship = curgod;
                Worship.Leaders.Add(this);
                break;
            }
            if (Worship == null)
            {
                var thisGod = Civilization.World.GetAddGod(godName);
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
            try
            {
                switch (InheritedFromSource)
                {
                    case InheritanceSource.Mother:
                        InheritanceId = Hf.HfLinks[HFLink.LinkTypes.IndexOf("mother")][0].LinkedHfid;
                        break;
                    case InheritanceSource.Father:
                        InheritanceId = Hf.HfLinks[HFLink.LinkTypes.IndexOf("father")][0].LinkedHfid;
                        break;
                    case InheritanceSource.PaternalGrandMother:

                        InheritanceId = Hf.HfLinks[HFLink.LinkTypes.IndexOf("father")][0].Hf.HfLinks[HFLink.LinkTypes.IndexOf("mother")][0].LinkedHfid;
                        break;
                    case InheritanceSource.MaternalGrandMother:
                        InheritanceId = Hf.HfLinks[HFLink.LinkTypes.IndexOf("mother")][0].Hf.HfLinks[HFLink.LinkTypes.IndexOf("mother")][0].LinkedHfid;
                        break;
                    case InheritanceSource.PaternalGrandFather:
                        InheritanceId = Hf.HfLinks[HFLink.LinkTypes.IndexOf("father")][0].Hf.HfLinks[HFLink.LinkTypes.IndexOf("father")][0].LinkedHfid;
                        break;
                    case InheritanceSource.MaternalGrandFather:
                        InheritanceId = Hf.HfLinks[HFLink.LinkTypes.IndexOf("mother")][0].Hf.HfLinks[HFLink.LinkTypes.IndexOf("father")][0].LinkedHfid;
                        break;
                    case InheritanceSource.Other:
                        var hfToCheck = new List<HistoricalFigure>();
                        var newHFs = new List<HistoricalFigure>();
                        HistoricalFigure matchHf = null;
                        var leaderToMatch = PreviousLeader();
                        hfToCheck.Add(Hf);

                        while (hfToCheck.Count > 0)
                        {
                            newHFs.Clear();
                            foreach (var hf in hfToCheck)
                            {
                                if (hf.IsLeader && hf.Leader == leaderToMatch)
                                {
                                    matchHf = hf;
                                    newHFs.Clear();
                                    break;
                                }
                                if (hf.HfLinks.ContainsKey(HFLink.LinkTypes.IndexOf("mother")) && hf.HfLinks[HFLink.LinkTypes.IndexOf("mother")][0].Hf != null)
                                    newHFs.Add(hf.HfLinks[HFLink.LinkTypes.IndexOf("mother")][0].Hf);
                                if (hf.HfLinks.ContainsKey(HFLink.LinkTypes.IndexOf("father")) && hf.HfLinks[HFLink.LinkTypes.IndexOf("father")][0].Hf != null)
                                    newHFs.Add(hf.HfLinks[HFLink.LinkTypes.IndexOf("father")][0].Hf);
                            }
                            hfToCheck.Clear();
                            hfToCheck = new List<HistoricalFigure>(newHFs);
                        }


                        InheritanceId = matchHf.Id;
                        break;
                }
            }
            catch (Exception)
            {
                InheritanceId = -1;
                Program.Log(LogType.Error, "Couldn't find Inheritance Match for: " + ToString());
            }

        }

        public HistoricalFigure ScaleFamilyTree(HistoricalFigure branch, Leader leaderMatch)
        {
            if (branch.IsLeader && branch.Leader == leaderMatch)
                return branch;

            if (branch.HfLinks.ContainsKey(HFLink.LinkTypes.IndexOf("mother")))
            {
                var motherMatch = ScaleFamilyTree(branch.HfLinks[HFLink.LinkTypes.IndexOf("mother")][0].Hf, leaderMatch);
                if (motherMatch != null)
                    return motherMatch;
            }
            if (branch.HfLinks.ContainsKey(HFLink.LinkTypes.IndexOf("father")))
            {
                var fatherMatch = ScaleFamilyTree(branch.HfLinks[HFLink.LinkTypes.IndexOf("father")][0].Hf, leaderMatch);
                return fatherMatch;
            }
            return null;
        }

        private Leader PreviousLeader()
        {
            var leaderList = Civilization.Leaders[LeaderTypes[LeaderType]];
            for (var i = 0; i < leaderList.Count; i++)
            {
                if (leaderList[i] != this) continue;
                return i == 0 ? null : leaderList[i - 1];
            }
            return null;
        }

        public override void Select(MainForm frm)
        {
            if (frm.grpLeader.Text == ToString() && frm.MainTab.SelectedTab == frm.tabLeader)
                return;
            Program.MakeSelected(frm.tabLeader, frm.lstLeader, this);

            frm.grpLeader.Text = ToString();
            frm.grpLeader.Show();

            frm.lblLeaderName.Text = ToString();
            frm.lblLeaderType.Text = LeaderTypes[LeaderType].ToTitleCase();
            if (Race != null || (Hf?.Race != null))
                frm.lblLeaderRace.Data = Race ?? Hf.Race;
            else
                frm.lblLeaderRace.Text = "";
            frm.lblLeaderLife.Text = Birth == null ? "" : (Birth + " – " + (Death == WorldTime.Present ? "" : Death.ToString()));
            frm.lblLeaderReignBegan.Text = ReignBegan?.ToString() ?? "";
            frm.lblLeaderInheritance.Text = InheritanceTypes[Inheritance];
            frm.lblLeaderInheritedFrom.Data = InheritedFrom;
            if (InheritedFromSource == InheritanceSource.Other)
                frm.lblLeaderInheritedFrom.Text =
                    $"{InheritedFrom?.ToString() ?? ""} ({"Relative"})";
            else if (InheritedFromSource != InheritanceSource.None)
                frm.lblLeaderInheritedFrom.Text =
                    $"{InheritedFrom?.ToString() ?? ""} ({InheritedFromSource})";
            frm.lblLeaderCivilization.Data = Civilization;
            frm.lblLeaderSite.Data = Site;
            frm.lblLeaderGod.Data = Worship;
            if (Worship != null)
                frm.lblLeaderGod.Text = $"{Worship} ({WorshipPercent}%)";
            frm.lblLeaderMarried.Data = Spouse;
            frm.lblLeaderHF.Data = Hf;

        }

        internal override void Export(string table)
        {
            var vals = new List<object>
            {
                Name.DBExport(),
                Hf.DBExport(),
                LeaderType.DBExport(LeaderTypes),
                Race.DBExport(),
                Birth.DBExport(true),
                Birth.DBExport(false),
                Death.DBExport(true),
                Death.DBExport(false),
                ReignBegan.DBExport(true),
                Inheritance.DBExport(InheritanceTypes),
                InheritedFromSource.ToString(),
                InheritedFrom.DBExport(),
                Civilization.DBExport(),
                Site.DBExport(),
                Worship == null ? DBNull.Value : Worship.Name.DBExport(),
                WorshipPercent,
                Spouse.DBExport()
            };

            Database.ExportWorldItem(table, vals);
        }
    }
}