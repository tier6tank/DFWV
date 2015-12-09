using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;
using DFWV.WorldClasses.HistoricalEventClasses;
using DFWV.WorldClasses.HistoricalFigureClasses;

namespace DFWV.WorldClasses.HistoricalEventCollectionClasses
{
    public struct BattleData
    {
        public int AttackingHFs;
        public int AttackingSquads;
        public int AttackingNumber;
        public int AttackingDeaths;
        public int AttackingHfDeaths;

        public int DefendingHFs;
        public int DefendingSquads;
        public int DefendingNumber;
        public int DefendingDeaths;
        public int DefendingHfDeaths;

        public int NonCombatHFs;
    }

    public class EC_Battle : HistoricalEventCollection
    {
        public int? WarEventColId { get; set; }
        public EC_War WarEventCol { get; private set; }
        public int? SubregionId { get; set; }
        private Region Subregion { get; set; }
        public int? FeatureLayerId { get; set; }
        public int? SiteId { get; set; }
        private Site Site { get; set; }
        public Point Coords { get; }
        public string Outcome { get; }
        public List<int> NonComHfid { get; set; }
        private List<HistoricalFigure> _nonComHf;
        public List<Squad> AttackingSquad { get; private set; }
        public List<string> AttackingSquadRace { get; set; }
        public List<int> AttackingSquadEntityPop { get; set; }
        public List<int> AttackingSquadNumber { get; set; }
        public List<int> AttackingSquadDeaths { get; set; }
        public List<int> AttackingSquadSite { get; set; }
        public List<Squad> DefendingSquad { get; private set; }
        public List<string> DefendingSquadRace { get; set; }
        public List<int> DefendingSquadEntityPop { get; set; }
        public List<int> DefendingSquadNumber { get; set; }
        public List<int> DefendingSquadDeaths { get; set; }
        public List<int> DefendingSquadSite { get; set; }
        public List<int> EventColIDs { get; set; }
        private List<HistoricalEventCollection> EventCol { get; set; }
        public List<int> AttackingHfid { get; set; }
        public List<HistoricalFigure> AttackingHf;
        public List<HistoricalFigure> AttackingDiedHf;
        public List<int> DefendingHfid { get; set; }
        public List<HistoricalFigure> DefendingHf;
        public List<HistoricalFigure> DefendingDiedHf;

        public bool BattleTotaled;
        public BattleData BattleData;

        override public Point Location => Site?.Coords ?? Coords;

        public EC_Battle(XDocument xdoc, World world)
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
                    case "subregion_id":
                        if (valI != -1)
                            SubregionId = valI;
                        break;
                    case "feature_layer_id":
                        if (valI != -1)
                            FeatureLayerId = valI;
                        break;
                    case "site_id":
                        if (valI != -1)
                            SiteId = valI;
                        break;
                    case "coords":
                        Coords = new Point(Convert.ToInt32(val.Split(',')[0]), Convert.ToInt32(val.Split(',')[1]));
                        break;
                    case "eventcol":
                        if (EventColIDs == null)
                            EventColIDs = new List<int>();
                        EventColIDs.Add(valI);
                        break;
                    case "name":
                        Name = val.ToTitleCase();
                        break;
                    case "attacking_hfid":
                        if (AttackingHfid == null)
                            AttackingHfid = new List<int>();
                        AttackingHfid.Add(valI);
                        break;
                    case "defending_hfid":
                        if (DefendingHfid == null)
                            DefendingHfid = new List<int>();
                        DefendingHfid.Add(valI);
                        break;
                    case "war_eventcol":
                        WarEventColId = valI;
                        break;
                    case "noncom_hfid":
                        if (NonComHfid == null)
                            NonComHfid = new List<int>();
                        NonComHfid.Add(valI);
                        break;
                    case "attacking_squad_race":
                        if (AttackingSquadRace == null)
                            AttackingSquadRace = new List<string>();
                        AttackingSquadRace.Add(val);
                        break;
                    case "attacking_squad_entity_pop":
                        if (AttackingSquadEntityPop == null)
                            AttackingSquadEntityPop = new List<int>();
                        AttackingSquadEntityPop.Add(valI);
                        break;
                    case "attacking_squad_number":
                        if (AttackingSquadNumber == null)
                            AttackingSquadNumber = new List<int>();
                        AttackingSquadNumber.Add(valI);
                        break;
                    case "attacking_squad_deaths":
                        if (AttackingSquadDeaths == null)
                            AttackingSquadDeaths = new List<int>();
                        AttackingSquadDeaths.Add(valI);
                        break;
                    case "attacking_squad_site":
                        if (AttackingSquadSite == null)
                            AttackingSquadSite = new List<int>();
                        AttackingSquadSite.Add(valI);
                        break;
                    case "defending_squad_race":
                        if (DefendingSquadRace == null)
                            DefendingSquadRace = new List<string>();
                        DefendingSquadRace.Add(val);
                        break;
                    case "defending_squad_entity_pop":
                        if (DefendingSquadEntityPop == null)
                            DefendingSquadEntityPop = new List<int>();
                        DefendingSquadEntityPop.Add(valI);
                        break;
                    case "defending_squad_number":
                        if (DefendingSquadNumber == null)
                            DefendingSquadNumber = new List<int>();
                        DefendingSquadNumber.Add(valI);
                        break;
                    case "defending_squad_deaths":
                        if (DefendingSquadDeaths == null)
                            DefendingSquadDeaths = new List<int>();
                        DefendingSquadDeaths.Add(valI);
                        break;
                    case "defending_squad_site":
                        if (DefendingSquadSite == null)
                            DefendingSquadSite = new List<int>();
                        DefendingSquadSite.Add(valI);
                        break;
                    case "outcome":
                        Outcome = val;
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
            if (SubregionId.HasValue && World.Regions.ContainsKey(SubregionId.Value))
                Subregion = World.Regions[SubregionId.Value];
            if (SiteId.HasValue && World.Sites.ContainsKey(SiteId.Value))
                Site = World.Sites[SiteId.Value];
            if (WarEventColId.HasValue && World.HistoricalEventCollections.ContainsKey(WarEventColId.Value))
                WarEventCol = (EC_War)World.HistoricalEventCollections[WarEventColId.Value];

            if (EventColIDs != null)
                EventCol = new List<HistoricalEventCollection>();
            LinkFieldList(EventColIDs,
                EventCol, World.HistoricalEventCollections);
            if (AttackingHfid != null)
                AttackingHf = new List<HistoricalFigure>();
            LinkFieldList(AttackingHfid,
                AttackingHf, World.HistoricalFigures);
            if (DefendingHfid != null)
                DefendingHf = new List<HistoricalFigure>();
            LinkFieldList(DefendingHfid,
                DefendingHf, World.HistoricalFigures);
            if (NonComHfid != null)
                _nonComHf = new List<HistoricalFigure>();
            LinkFieldList(NonComHfid,
                _nonComHf, World.HistoricalFigures);


            if (AttackingSquadRace != null)
            {
                AttackingSquad = new List<Squad>();
                for (var i = 0; i < AttackingSquadRace.Count; i++)
                {
                    var newSquad = new Squad(
                        World.GetAddRace(AttackingSquadRace[i]),
                        World.EntityPopulations.ContainsKey(AttackingSquadEntityPop[i]) ? World.EntityPopulations[AttackingSquadEntityPop[i]] : null,
                        AttackingSquadNumber[i],
                        AttackingSquadDeaths[i],
                        World.Sites.ContainsKey(AttackingSquadSite[i]) ? World.Sites[AttackingSquadSite[i]] : null
                        );
                    AttackingSquad.Add(newSquad);
                }
            }
            if (DefendingSquadRace == null) return;
            DefendingSquad = new List<Squad>();
            for (var i = 0; i < DefendingSquadRace.Count; i++)
            {
                var newSquad = new Squad(
                    World.GetAddRace(DefendingSquadRace[i]),
                    World.EntityPopulations.ContainsKey(DefendingSquadEntityPop[i]) ? World.EntityPopulations[DefendingSquadEntityPop[i]] : null,
                    DefendingSquadNumber[i],
                    DefendingSquadDeaths[i],
                    World.Sites.ContainsKey(DefendingSquadSite[i]) ? World.Sites[DefendingSquadSite[i]] : null
                    );
                DefendingSquad.Add(newSquad);
            }
        }

        public override void Select(MainForm frm)
        {
            base.Select(frm);

            foreach (var tabpage in frm.MainTabEventCollectionTypes.TabPages.Cast<TabPage>().Where(tabpage => tabpage != frm.tabEventCollectionBattle))
            {
                frm.MainTabEventCollectionTypes.TabPages.Remove(tabpage);
            }
            if (!frm.MainTabEventCollectionTypes.TabPages.Contains(frm.tabEventCollectionBattle))
                frm.MainTabEventCollectionTypes.TabPages.Add(frm.tabEventCollectionBattle);

            frm.lblBattleName.Text = ToString();
            if (StartTime != null || EndTime != null)
            {
                frm.lblBattleTime.Text = $"{StartTime} - {EndTime}";
                frm.lblBattleDuration.Text = WorldTime.Duration(EndTime, StartTime);
            }
            else
            {
                frm.lblBattleTime.Text = "";
                frm.lblBattleDuration.Text = "";
            }
            frm.lblBattleWar.Data = WarEventCol;
            frm.lblBattleRegion.Data = Subregion;
            frm.lblBattleSite.Data = Site;
            frm.lblBattleCoord.Data = new Coordinate(Coords);

            frm.lblBattleAttackerCombatants.Text = BattleData.AttackingNumber.ToString();
            if (BattleData.AttackingHFs > 0)
                frm.lblBattleAttackerCombatants.Text += $" + {BattleData.AttackingHFs} HFs";
            frm.lblBattleAttackerLosses.Text = BattleData.AttackingDeaths.ToString();
            if (BattleData.AttackingHfDeaths > 0)
                frm.lblBattleAttackerLosses.Text += $" + {BattleData.AttackingHfDeaths} HFs";

            frm.lblBattleDefenderCombatants.Text = BattleData.DefendingNumber.ToString();
            if (BattleData.DefendingHFs > 0)
                frm.lblBattleDefenderCombatants.Text += $" + {BattleData.DefendingHFs} HFs";
            frm.lblBattleDefenderLosses.Text = BattleData.DefendingDeaths.ToString();
            if (BattleData.DefendingHfDeaths > 0)
                frm.lblBattleDefenderLosses.Text += $" + {BattleData.DefendingHfDeaths} HFs";



            if (Outcome == "attacker won")
            {
                frm.lblBattleAttackerOutcome.Text = @"Attacker Won";
                frm.lblBattleDefenderOutcome.Text = @"Defender Lost";
            }
            else
            {
                frm.lblBattleAttackerOutcome.Text = @"Attacker Lost";
                frm.lblBattleDefenderOutcome.Text = @"Defender Won";
            }
            frm.lstBattleAttackingHF.Items.Clear();
            frm.lstBattleAttackingHF.Tag = this;
            if (AttackingHf != null)
                frm.lstBattleAttackingHF.Items.AddRange(AttackingHf.ToArray());

            frm.grpBattleAttackingHF.Text = $"Historical Figures ({frm.lstBattleAttackingHF.Items.Count})";

            frm.lstBattleDefendingHF.Items.Clear();
            frm.lstBattleDefendingHF.Tag = this;
            if (DefendingHf != null)
                frm.lstBattleDefendingHF.Items.AddRange(DefendingHf.ToArray());

            frm.grpBattleDefendingHF.Text = $"Historical Figures ({frm.lstBattleDefendingHF.Items.Count})";

            frm.lstBattleAttackingSquad.Items.Clear();
            if (AttackingSquad != null)
                frm.lstBattleAttackingSquad.Items.AddRange(AttackingSquad.ToArray());

            if (frm.lstBattleAttackingSquad.Items.Count > 0)
                frm.lstBattleAttackingSquad.SelectedIndex = 0;
            else
            {
                frm.lblBattleAttackingSquadDeaths.Text = "";
                frm.lblBattleAttackingSquadEntPop.Data = null;
                frm.lblBattleAttackingSquadNumber.Text = "";
                frm.lblBattleAttackingSquadRace.Data = null;
                frm.lblBattleAttackingSquadSite.Data = null;
            }
            frm.grpBattleAttackingSquad.Text = $"Squads ({frm.lstBattleAttackingSquad.Items.Count})";


            frm.lstBattleDefendingSquad.Items.Clear();
            if (DefendingSquad != null)
                frm.lstBattleDefendingSquad.Items.AddRange(DefendingSquad.ToArray());

            if (frm.lstBattleDefendingSquad.Items.Count > 0)
                frm.lstBattleDefendingSquad.SelectedIndex = 0;
            else
            {
                frm.lblBattleDefendingSquadDeaths.Text = "";
                frm.lblBattleDefendingSquadEntPop.Data = null;
                frm.lblBattleDefendingSquadNumber.Text = "";
                frm.lblBattleDefendingSquadRace.Data = null;
                frm.lblBattleDefendingSquadSite.Data = null;
            }
            frm.grpBattleDefendingSquad.Text = $"Squads ({frm.lstBattleDefendingSquad.Items.Count})";

            frm.lstBattleEventCols.Items.Clear();
            if (EventCol != null)
                frm.lstBattleEventCols.Items.AddRange(EventCol.ToArray());

            frm.grpBattleEventCols.Visible = frm.lstBattleEventCols.Items.Count > 0;

            frm.lstBattleEvents.Items.Clear();
            if (Event != null)
                frm.lstBattleEvents.Items.AddRange(Event.ToArray());

            frm.grpBattleEvents.Visible = frm.lstBattleEvents.Items.Count > 0;
            if (frm.lstBattleEvents.Items.Count > 0)
                frm.lstBattleEvents.SelectedIndex = 0;

            frm.lstBattleNonComHFs.Items.Clear();
            if (_nonComHf != null)
                frm.lstBattleNonComHFs.Items.AddRange(_nonComHf.ToArray());

            frm.grpBattleNonComHFs.Visible = frm.lstBattleNonComHFs.Items.Count > 0;

            SelectTab(frm);
        }

        internal override void Process()
        {
            base.Process();
            if (Subregion != null)
            {
                if (Subregion.BattleEventCollections == null)
                    Subregion.BattleEventCollections = new List<EC_Battle>();
                Subregion.BattleEventCollections.Add(this);
                if (Subregion.Coords == null)
                    Subregion.Coords = new List<Point>();
                Subregion.Coords.Add(Coords);
            }
            if (Site != null)
            {
                if (Site.BattleEventCollections == null)
                    Site.BattleEventCollections = new List<EC_Battle>();
                Site.BattleEventCollections.Add(this);
            }
            if (AttackingHf != null)
            {
                foreach (var hf in AttackingHf)
                {
                    if (hf.BattleEventCollections == null)
                        hf.BattleEventCollections = new List<EC_Battle>();
                    hf.BattleEventCollections.Add(this);
                }
            }
            if (DefendingHf != null)
            {
                foreach (var hf in DefendingHf)
                {
                    if (hf.BattleEventCollections == null)
                        hf.BattleEventCollections = new List<EC_Battle>();
                    hf.BattleEventCollections.Add(this);
                }
            }
            if (_nonComHf != null)
            {
                foreach (var hf in _nonComHf)
                {
                    if (hf.BattleEventCollections == null)
                        hf.BattleEventCollections = new List<EC_Battle>();
                    hf.BattleEventCollections.Add(this);
                }
            }
            if (AttackingSquad != null)
            {
                foreach (var squad in AttackingSquad.Where(squad => squad.EntityPopulation != null))
                {
                    if (squad.EntityPopulation.BattleEventCollections == null)
                        squad.EntityPopulation.BattleEventCollections = new List<EC_Battle>();
                    squad.EntityPopulation.BattleEventCollections.Add(this);
                }
            }
            if (DefendingSquad != null)
            {
                foreach (var squad in DefendingSquad.Where(squad => squad.EntityPopulation != null))
                {
                    if (squad.EntityPopulation.BattleEventCollections == null)
                        squad.EntityPopulation.BattleEventCollections = new List<EC_Battle>();
                    squad.EntityPopulation.BattleEventCollections.Add(this);
                }
            }
            if (!BattleTotaled)
                TotalBattle();

        }

        internal override void Evaluate()
        {
            base.Evaluate();

            // For battle event collections, if we have hf died events we can add that HF as a casualty of the battle, 
            //      which will be displayed in bold when viewing participating HFs.

            foreach (var ev in Event.Where(x => HistoricalEvent.Types[x.Type] == "hf died").Cast<HE_HFDied>().Where(ev => ev.Hf != null))
            {
                if (AttackingHf.Contains(ev.Hf))
                {
                    if (AttackingDiedHf == null)
                        AttackingDiedHf = new List<HistoricalFigure>();
                    AttackingDiedHf.Add(ev.Hf);
                }
                else if (DefendingHf.Contains(ev.Hf))
                {
                    if (DefendingDiedHf == null)
                        DefendingDiedHf = new List<HistoricalFigure>();
                    DefendingDiedHf.Add(ev.Hf);
                }
            }
        }

        internal override void Export(string table)
        {
            base.Export(table);


            table = GetType().Name;

            var vals = new List<object> { Id, WarEventColId, Outcome, SiteId, SubregionId, FeatureLayerId };

            if (Coords.IsEmpty)
                vals.Add(DBNull.Value);
            else
                vals.Add(Coords.X + "," + Coords.Y);

            Database.ExportWorldItem(table, vals);


            if (AttackingSquad != null)
            {
                foreach (var squad in AttackingSquad)
                {
                    vals = new List<object> { Id, squad.Race.ToString(), squad.EntityPopulation?.Id ?? (object)DBNull.Value, squad.Number, squad.Deaths, squad.Site?.Id ?? (object)DBNull.Value };
                    Database.ExportWorldItem("EC_Battle_Attacking_Squads", vals);
                }
            }
            if (DefendingSquad != null)
            {
                foreach (var squad in DefendingSquad)
                {
                    vals = new List<object> { Id, squad.Race.ToString(), squad.EntityPopulation?.Id ?? (object)DBNull.Value, squad.Number, squad.Deaths, squad.Site?.Id ?? (object)DBNull.Value };
                    Database.ExportWorldItem("EC_Battle_Defending_Squads", vals);
                }
            }
            if (AttackingHf != null)
            {
                foreach (var hf in AttackingHf)
                {
                    vals = new List<object> { Id, hf.Id };
                    Database.ExportWorldItem("EC_Battle_Attacking_HF", vals);
                }
            }
            if (DefendingHf != null)
            {
                foreach (var hf in DefendingHf)
                {
                    vals = new List<object> { Id, hf.Id };
                    Database.ExportWorldItem("EC_Battle_Defending_HF", vals);
                }
            }
            if (_nonComHf != null)
            {
                foreach (var hf in _nonComHf)
                {
                    vals = new List<object> { Id, hf.Id };
                    Database.ExportWorldItem("EC_Battle_NonCom_HF", vals);
                }
            }

            if (EventCol == null)
                return;
            table = "EC_EventCols";
            foreach (var evtcol in EventCol)
            {
                vals = new List<object> { Id, evtcol.Id };
                Database.ExportWorldItem(table, vals);

            }
        }

        internal void TotalBattle()
        {
            if (AttackingSquad != null)
            {
                foreach (var squad in AttackingSquad)
                {
                    BattleData.AttackingDeaths += squad.Deaths;
                    BattleData.AttackingNumber += squad.Number;
                    BattleData.AttackingSquads++;
                }
            }
            if (AttackingHf != null)
                BattleData.AttackingHFs += AttackingHf.Count;
            if (AttackingDiedHf != null)
                BattleData.AttackingHfDeaths += AttackingDiedHf.Count;

            if (DefendingSquad != null)
            {
                foreach (var squad in DefendingSquad)
                {
                    BattleData.DefendingDeaths += squad.Deaths;
                    BattleData.DefendingNumber += squad.Number;
                    BattleData.DefendingSquads++;
                }
            }

            if (DefendingHf != null)
                BattleData.DefendingHFs += DefendingHf.Count;
            if (DefendingDiedHf != null)
                BattleData.DefendingHfDeaths += DefendingDiedHf.Count;

            if (_nonComHf != null)
            {
                BattleData.NonCombatHFs += _nonComHf.Count;
            }
            BattleTotaled = true;
        }


    }

    public class Squad
    {
        public Squad(Race race,
                    EntityPopulation entityPopulation,
                    int number, int deaths, Site site)
        {
            Race = race;
            EntityPopulation = entityPopulation;
            if (EntityPopulation != null)
                EntityPopulation.Race = Race;
            Number = number;
            Deaths = deaths;
            Site = site;
        }
        public Race Race { get; }
        public EntityPopulation EntityPopulation { get; }
        public int Number { get; }
        public int Deaths { get; }
        public Site Site { get; }

        public override string ToString()
        {
            if (Site != null)
            {
                if (Number == 1)
                    return Race + " from " + Site + " - " + (Number - Deaths) + "/" + Number;
                return Race.ToString().Pluralize() + " from " + Site + " - " + (Number - Deaths) + "/" + Number;
            }
            return Race + " - " + (Number - Deaths) + "/" + Number;
        }
    }
}
