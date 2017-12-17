using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;
using DFWV.WorldClasses.EntityClasses;
using System.Collections.Concurrent;

namespace DFWV.WorldClasses.HistoricalEventCollectionClasses
{
    public struct WarData
    {
        public int AttackingHFs;
        public int AttackingSquads;
        public int AttackingNumber;
        public int AttackingDeaths;
        public int AttackingWins;
        public int AttackingHfDeaths;

        public int DefendingHFs;
        public int DefendingSquads;
        public int DefendingNumber;
        public int DefendingDeaths;
        public int DefendingWins;
        public int DefendingHfDeaths;

        public int NonCombatHFs;
    }


    public class EC_War : HistoricalEventCollection
    {
        public List<int> EventColIDs { get; set; }
        public List<HistoricalEventCollection> EventCol { get; private set; }
        public int? AggressorEntId { get; set; }
        public Entity AggressorEnt { get; private set; }
        public int? DefenderEntId { get; set; }
        public Entity DefenderEnt { get; private set; }
        public WarData WarData;

        override public Point Location => AggressorEnt.Location;

        public EC_War(XDocument xdoc, World world)
            : base(xdoc, world)
        {
            foreach (var element in xdoc.Root.Elements())
            {
                var val = element.Value;
                int valI;
                int.TryParse(val, out valI);

                switch (element.Name.LocalName)
                {
                    case "id":
                    case "start_year":
                    case "start_seconds72":
                    case "end_year":
                    case "end_seconds72":
                    case "event":
                    case "type":
                        break;
                    case "eventcol":
                        if (EventColIDs == null)
                            EventColIDs = new List<int>();
                        EventColIDs.Add(valI);
                        break;
                    case "name":
                        Name = val.ToTitleCase();
                        break;
                    case "aggressor_ent_id":
                        AggressorEntId = valI;
                        break;
                    case "defender_ent_id":
                        DefenderEntId = valI;
                        break;

                    default:
                        DFXMLParser.UnexpectedXmlElement(xdoc.Root.Name.LocalName + "\t" + Types[Type], element, xdoc.Root.ToString());
                        break;
                }
            }

        }

        internal override void Link()
        {
            base.Link();
            if (AggressorEntId.HasValue && World.Entities.ContainsKey(AggressorEntId.Value))
                AggressorEnt = World.Entities[AggressorEntId.Value];
            if (DefenderEntId.HasValue && World.Entities.ContainsKey(DefenderEntId.Value))
                DefenderEnt = World.Entities[DefenderEntId.Value];

            if (EventColIDs != null)
                EventCol = new List<HistoricalEventCollection>();
            LinkFieldList(EventColIDs,
                EventCol, World.HistoricalEventCollections);

        }

        public override void Select(MainForm frm)
        {
            base.Select(frm);

            foreach (var tabpage in frm.MainTabEventCollectionTypes.TabPages.Cast<TabPage>().Where(tabpage => tabpage != frm.tabEventCollectionWar))
            {
                frm.MainTabEventCollectionTypes.TabPages.Remove(tabpage);
            }
            if (!frm.MainTabEventCollectionTypes.TabPages.Contains(frm.tabEventCollectionWar))
                frm.MainTabEventCollectionTypes.TabPages.Add(frm.tabEventCollectionWar);

            frm.lblWarName.Text = ToString();
            if (StartTime != null || EndTime != null)
            {
                frm.lblWarTime.Text = $"{StartTime} - {(EndTime == WorldTime.Present ? "" : EndTime.ToString())}";
                frm.lblWarDuration.Text = WorldTime.Duration(EndTime, StartTime);
            }
            else
            {
                frm.lblWarTime.Text = "";
                frm.lblWarDuration.Text = "";
            }

            frm.lblWarAggressor.Data = AggressorEnt;
            frm.lblWarDefender.Data = DefenderEnt;

            frm.lstWarEventCols.Items.Clear();
            if (EventCol != null)
                frm.lstWarEventCols.Items.AddRange(EventCol.ToArray());

            frm.grpWarEventCols.Visible = frm.lstWarEventCols.Items.Count > 0;

            frm.lstWarEvents.Items.Clear();
            if (Event != null)
                frm.lstWarEvents.Items.AddRange(Event.ToArray());

            frm.grpWarEvents.Visible = frm.lstWarEvents.Items.Count > 0;
            if (frm.lstWarEvents.Items.Count > 0)
                frm.lstWarEvents.SelectedIndex = 0;

            frm.lblWarAggressorCombatants.Text = WarData.AttackingNumber.ToString();
            if (WarData.AttackingHFs > 0)
                frm.lblWarAggressorCombatants.Text += $" + {WarData.AttackingHFs} HFs";
            frm.lblWarAggressorLosses.Text = WarData.AttackingDeaths.ToString();
            if (WarData.AttackingHfDeaths > 0)
                frm.lblWarAggressorLosses.Text += $" + {WarData.AttackingHfDeaths} HFs";

            frm.lblWarDefenderCombatants.Text = WarData.DefendingNumber.ToString();
            if (WarData.DefendingHFs > 0)
                frm.lblWarDefenderCombatants.Text += $" + {WarData.DefendingHFs} HFs";
            frm.lblWarDefenderLosses.Text = WarData.DefendingDeaths.ToString();
            if (WarData.DefendingHfDeaths > 0)
                frm.lblWarDefenderLosses.Text += $" + {WarData.DefendingHfDeaths} HFs";



            frm.lblWarAggressorSquads.Text = WarData.AttackingSquads.ToString();
            frm.lblWarAggressorWins.Text = WarData.AttackingWins.ToString();

            frm.lblWarDefenderSquads.Text = WarData.DefendingSquads.ToString();
            frm.lblWarDefenderWins.Text = WarData.DefendingWins.ToString();

            SelectTab(frm);
        }

        public override void Process()
        {
            base.Process();
            if (AggressorEnt.WarEventCollections == null)
                AggressorEnt.WarEventCollections = new ConcurrentBag<EC_War>();
            AggressorEnt.WarEventCollections.Add(this);
            if (DefenderEnt.WarEventCollections == null)
                DefenderEnt.WarEventCollections = new ConcurrentBag<EC_War>();
            DefenderEnt.WarEventCollections.Add(this);
            if (EventCol != null)
                TotalWar();

        }

        internal override void Evaluate()
        {
            base.Evaluate();

            // Total up deaths/fighters in a war from the battle
            foreach (var battle in EventCol.Where(x => x is EC_Battle).Cast<EC_Battle>())
                battle.BattleTotaled = false;
            TotalWar();

        }

        public void TotalWar()
        {
            foreach (var battle in EventCol.Where(x => Types[x.Type] == "battle").Cast<EC_Battle>())
            {
                if (!battle.BattleTotaled)
                    battle.TotalBattle();

                WarData.AttackingDeaths += battle.BattleData.AttackingDeaths;
                WarData.AttackingNumber += battle.BattleData.AttackingNumber;
                WarData.AttackingSquads += battle.BattleData.AttackingSquads;
                WarData.AttackingHFs += battle.BattleData.AttackingHFs;
                WarData.AttackingHfDeaths += battle.BattleData.AttackingHfDeaths;

                WarData.DefendingDeaths += battle.BattleData.DefendingDeaths;
                WarData.DefendingNumber += battle.BattleData.DefendingNumber;
                WarData.DefendingSquads += battle.BattleData.DefendingSquads;
                WarData.DefendingHFs += battle.BattleData.DefendingHFs;
                WarData.DefendingHfDeaths += battle.BattleData.DefendingHfDeaths;

                WarData.NonCombatHFs += battle.BattleData.NonCombatHFs;

                if (battle.Outcome == "attacker won")
                    WarData.AttackingWins++;
                else if (battle.Outcome == "defender won")
                    WarData.DefendingWins++;
                else
                    break;
            }
        }


        internal override void Export(string table)
        {
            base.Export(table);


            table = GetType().Name;

            var vals = new List<object> { Id, AggressorEntId, DefenderEntId };

            Database.ExportWorldItem(table, vals);

            if (EventCol == null)
                return;
            table = "EC_EventCols";
            foreach (var evtcol in EventCol)
            {
                vals = new List<object> { Id, evtcol.Id };
                Database.ExportWorldItem(table, vals);

            }
        }

    }
}
