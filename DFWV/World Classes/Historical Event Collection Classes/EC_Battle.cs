using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;
using DFWV.WorldClasses.HistoricalFigureClasses;
using DFWV.WorldClasses.HistoricalEventClasses;
using System.Globalization;

namespace DFWV.WorldClasses.HistoricalEventCollectionClasses
{
    struct BattleData
    {
        public int AttackingHFs;
        public int AttackingSquads;
        public int AttackingNumber;
        public int AttackingDeaths;
        public int AttackingHFDeaths;

        public int DefendingHFs;
        public int DefendingSquads;
        public int DefendingNumber;
        public int DefendingDeaths;
        public int DefendingHFDeaths;

        public int NonCombatHFs;
    }

    class EC_Battle : HistoricalEventCollection
    {
        public int? WarEventCol_ { get; set; }
        public EC_War WarEventCol { get; set; }
        public int? SubregionID { get; set; }
        public Region Subregion { get; set; }
        public int? FeatureLayerID { get; set; }
        public int? SiteID { get; set; }
        public Site Site { get; set; }
        public Point Coords { get; set; }
        public string Outcome { get; set; }
        public List<int> NonComHFID { get; set; }
        public List<HistoricalFigure> NonComHF;
        public List<Squad> AttackingSquad { get; set; }
        public List<string> AttackingSquadRace { get; set; }
        public List<int> AttackingSquadEntityPop { get; set; }
        public List<int> AttackingSquadNumber { get; set; }
        public List<int> AttackingSquadDeaths { get; set; }
        public List<int> AttackingSquadSite { get; set; }
        public List<Squad> DefendingSquad { get; set; }
        public List<string> DefendingSquadRace { get; set; }
        public List<int> DefendingSquadEntityPop { get; set; }
        public List<int> DefendingSquadNumber { get; set; }
        public List<int> DefendingSquadDeaths { get; set; }
        public List<int> DefendingSquadSite { get; set; }
        public List<int> EventCol_ { get; set; }
        public List<HistoricalEventCollection> EventCol { get; set; }
        public List<int> AttackingHFID { get; set; }
        public List<HistoricalFigure> AttackingHF;
        public List<HistoricalFigure> AttackingDiedHF;
        public List<int> DefendingHFID { get; set; }
        public List<HistoricalFigure> DefendingHF;
        public List<HistoricalFigure> DefendingDiedHF;

        public bool battleTotaled = false;
        public BattleData BattleData;

        override public Point Location { get { return Site != null ? Site.Coords : Coords; } }

        public EC_Battle(XDocument xdoc, World world)
            : base(xdoc, world)
        {
            foreach (XElement element in xdoc.Root.Elements())
            {
                string val = element.Value.ToString();
                int valI;
                Int32.TryParse(val, out valI);

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
                            SubregionID = valI;
                        break;
                    case "feature_layer_id":
                        if (valI != -1)
                            FeatureLayerID = valI;
                        break;
                    case "site_id":
                        if (valI != -1)
                            SiteID = valI;
                        break;
                    case "coords":
                        Coords = new Point(Convert.ToInt32(val.Split(',')[0]), Convert.ToInt32(val.Split(',')[1]));
                        break;
                    case "eventcol":
                        if (EventCol_ == null)
                            EventCol_ = new List<int>();
                        EventCol_.Add(valI);
                        break;
                    case "name":
                        Name = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(val);
                        break;
                    case "attacking_hfid":
                        if (AttackingHFID == null)
                            AttackingHFID = new List<int>();
                        AttackingHFID.Add(valI);
                        break;
                    case "defending_hfid":
                        if (DefendingHFID == null)
                            DefendingHFID = new List<int>();
                        DefendingHFID.Add(valI);
                        break;
                    case "war_eventcol":
                        WarEventCol_ = valI;
                        break;
                    case "noncom_hfid":
                        if (NonComHFID == null)
                            NonComHFID = new List<int>();
                        NonComHFID.Add(valI);
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
                        DFXMLParser.UnexpectedXMLElement(xdoc.Root.Name.LocalName + "\t" + HistoricalEventCollection.Types[Type], element, xdoc.Root.ToString());
                        break;
                }
            }

        }

        internal override void Link()
        {
            base.Link();
            if (SubregionID.HasValue && World.Regions.ContainsKey(SubregionID.Value))
                Subregion = World.Regions[SubregionID.Value];
            if (SiteID.HasValue && World.Sites.ContainsKey(SiteID.Value))
                Site = World.Sites[SiteID.Value];
            if (WarEventCol_.HasValue && World.HistoricalEventCollections.ContainsKey(WarEventCol_.Value))
                WarEventCol = (EC_War)World.HistoricalEventCollections[WarEventCol_.Value];

            if (EventCol_ != null)
                EventCol = new List<HistoricalEventCollection>();
            LinkFieldList<HistoricalEventCollection>(EventCol_,
                EventCol, World.HistoricalEventCollections);
            if (AttackingHFID != null)
                AttackingHF = new List<HistoricalFigure>();
            LinkFieldList<HistoricalFigure>(AttackingHFID,
                AttackingHF, World.HistoricalFigures);
            if (DefendingHFID != null)
                DefendingHF = new List<HistoricalFigure>();
            LinkFieldList<HistoricalFigure>(DefendingHFID,
                DefendingHF, World.HistoricalFigures);
            if (NonComHFID != null)
                NonComHF = new List<HistoricalFigure>();
            LinkFieldList<HistoricalFigure>(NonComHFID,
                NonComHF, World.HistoricalFigures);


            if (AttackingSquadRace != null)
            {
                AttackingSquad = new List<Squad>();
                for (int i = 0; i < AttackingSquadRace.Count; i++)
                {
                    Squad newSquad = new Squad(
                        World.GetAddRace(AttackingSquadRace[i]),
                        World.EntityPopulations.ContainsKey(AttackingSquadEntityPop[i]) ? World.EntityPopulations[AttackingSquadEntityPop[i]] : null,
                        AttackingSquadNumber[i],
                        AttackingSquadDeaths[i],
                        World.Sites.ContainsKey(AttackingSquadSite[i]) ? World.Sites[AttackingSquadSite[i]] : null
                        );
                    AttackingSquad.Add(newSquad);
                }
            }
            if (DefendingSquadRace != null)
            {
                DefendingSquad = new List<Squad>();
                for (int i = 0; i < DefendingSquadRace.Count; i++)
                {
                    Squad newSquad = new Squad(
                        World.GetAddRace(DefendingSquadRace[i]),
                        World.EntityPopulations.ContainsKey(DefendingSquadEntityPop[i]) ? World.EntityPopulations[DefendingSquadEntityPop[i]] : null,
                        DefendingSquadNumber[i],
                        DefendingSquadDeaths[i],
                        World.Sites.ContainsKey(DefendingSquadSite[i]) ? World.Sites[DefendingSquadSite[i]] : null
                        );
                    DefendingSquad.Add(newSquad);
                }
            }

        }

        public override void Select(MainForm frm)
        {
            base.Select(frm);

            foreach (TabPage tabpage in frm.MainTabEventCollectionTypes.TabPages)
            {
                if (tabpage != frm.tabEventCollectionBattle)
                    frm.MainTabEventCollectionTypes.TabPages.Remove(tabpage);
            }
            if (!frm.MainTabEventCollectionTypes.TabPages.Contains(frm.tabEventCollectionBattle))
                frm.MainTabEventCollectionTypes.TabPages.Add(frm.tabEventCollectionBattle);

            frm.lblBattleName.Text = ToString();
            if (StartTime != null || EndTime != null)
            {
                frm.lblBattleTime.Text = StartTime.ToString() + " - " + EndTime.ToString();
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
                frm.lblBattleAttackerCombatants.Text += " + " + BattleData.AttackingHFs + " HFs";
            frm.lblBattleAttackerLosses.Text = BattleData.AttackingDeaths.ToString();
            if (BattleData.AttackingHFDeaths > 0)
                frm.lblBattleAttackerLosses.Text += " + " + BattleData.AttackingHFDeaths + " HFs";

            frm.lblBattleDefenderCombatants.Text = BattleData.DefendingNumber.ToString();
            if (BattleData.DefendingHFs > 0)
                frm.lblBattleDefenderCombatants.Text += " + " + BattleData.DefendingHFs + " HFs";
            frm.lblBattleDefenderLosses.Text = BattleData.DefendingDeaths.ToString();
            if (BattleData.DefendingHFDeaths > 0)
                frm.lblBattleDefenderLosses.Text += " + " + BattleData.DefendingHFDeaths + " HFs";



            if (Outcome == "attacker won")
            {
                frm.lblBattleAttackerOutcome.Text = "Attacker Won";
                frm.lblBattleDefenderOutcome.Text = "Defender Lost";
            }
            else
            {
                frm.lblBattleAttackerOutcome.Text = "Attacker Lost";
                frm.lblBattleDefenderOutcome.Text = "Defender Won";
            }
            frm.lstBattleAttackingHF.Items.Clear();
            if (AttackingHF != null)
                frm.lstBattleAttackingHF.Items.AddRange(AttackingHF.ToArray());

            frm.grpBattleAttackingHF.Text = "Historical Figures (" + frm.lstBattleAttackingHF.Items.Count + ")";

            frm.lstBattleDefendingHF.Items.Clear();
            if (DefendingHF != null)
                frm.lstBattleDefendingHF.Items.AddRange(DefendingHF.ToArray());

            frm.grpBattleDefendingHF.Text = "Historical Figures (" + frm.lstBattleDefendingHF.Items.Count + ")";

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
            frm.grpBattleAttackingSquad.Text = "Squads (" + frm.lstBattleAttackingSquad.Items.Count + ")";


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
            frm.grpBattleDefendingSquad.Text = "Squads (" + frm.lstBattleDefendingSquad.Items.Count + ")";

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
            if (NonComHF != null)
                frm.lstBattleNonComHFs.Items.AddRange(NonComHF.ToArray());

            frm.grpBattleNonComHFs.Visible = frm.lstBattleNonComHFs.Items.Count > 0;

            base.SelectTab(frm);
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
            if (AttackingHF != null)
            {
                foreach (HistoricalFigure hf in AttackingHF)
                {
                    if (hf.BattleEventCollections == null)
                        hf.BattleEventCollections = new List<EC_Battle>();
                    hf.BattleEventCollections.Add(this);
                }
            }
            if (DefendingHF != null)
            {
                foreach (HistoricalFigure hf in DefendingHF)
                {
                    if (hf.BattleEventCollections == null)
                        hf.BattleEventCollections = new List<EC_Battle>();
                    hf.BattleEventCollections.Add(this);
                }
            }
            if (NonComHF != null)
            {
                foreach (HistoricalFigure hf in NonComHF)
                {
                    if (hf.BattleEventCollections == null)
                        hf.BattleEventCollections = new List<EC_Battle>();
                    hf.BattleEventCollections.Add(this);
                }
            }
            if (AttackingSquad != null)
            {
                foreach (Squad squad in AttackingSquad)
                {
                    if (squad.EntityPopulation == null)
                        continue;
                    if (squad.EntityPopulation.BattleEventCollections == null)
                        squad.EntityPopulation.BattleEventCollections = new List<EC_Battle>();
                    squad.EntityPopulation.BattleEventCollections.Add(this);
                }
            }
            if (DefendingSquad != null)
            {
                foreach (Squad squad in DefendingSquad)
                {
                    if (squad.EntityPopulation == null)
                        continue;
                    if (squad.EntityPopulation.BattleEventCollections == null)
                        squad.EntityPopulation.BattleEventCollections = new List<EC_Battle>();
                    squad.EntityPopulation.BattleEventCollections.Add(this);
                }
            }
            if (!battleTotaled)
                TotalBattle();

        }

        internal override void Export(string table)
        {
            base.Export(table);


            List<object> vals;
            table = this.GetType().Name.ToString();

            vals = new List<object>() { ID, WarEventCol_, Outcome, SiteID, SubregionID, FeatureLayerID };

            if (Coords.IsEmpty)
                vals.Add(DBNull.Value);
            else
                vals.Add(Coords.X + "," + Coords.Y);

            Database.ExportWorldItem(table, vals);


            if (AttackingSquad != null)
            {
                foreach (Squad squad in AttackingSquad)
                {
                    vals = new List<object>() { ID, squad.Race.ToString(), squad.EntityPopulation == null ? (object)DBNull.Value : squad.EntityPopulation.ID, squad.Number, squad.Deaths, squad.Site == null ? (object)DBNull.Value : squad.Site.ID };
                    Database.ExportWorldItem("EC_Battle_Attacking_Squads", vals);
                }
            }
            if (DefendingSquad != null)
            {
                foreach (Squad squad in DefendingSquad)
                {
                    vals = new List<object>() { ID, squad.Race.ToString(), squad.EntityPopulation == null ? (object)DBNull.Value : squad.EntityPopulation.ID, squad.Number, squad.Deaths, squad.Site == null ? (object)DBNull.Value : squad.Site.ID };
                    Database.ExportWorldItem("EC_Battle_Defending_Squads", vals);
                }
            }
            if (AttackingHF != null)
            {
                foreach (HistoricalFigure hf in AttackingHF)
                {
                    vals = new List<object>() { ID, hf.ID };
                    Database.ExportWorldItem("EC_Battle_Attacking_HF", vals);
                }
            }
            if (DefendingHF != null)
            {
                foreach (HistoricalFigure hf in DefendingHF)
                {
                    vals = new List<object>() { ID, hf.ID };
                    Database.ExportWorldItem("EC_Battle_Defending_HF", vals);
                }
            }
            if (NonComHF != null)
            {
                foreach (HistoricalFigure hf in NonComHF)
                {
                    vals = new List<object>() { ID, hf.ID };
                    Database.ExportWorldItem("EC_Battle_NonCom_HF", vals);
                }
            }

            if (EventCol == null)
                return;
            table = "EC_EventCols";
            foreach (HistoricalEventCollection evtcol in EventCol)
            {
                vals = new List<object>() { ID, evtcol.ID };
                Database.ExportWorldItem(table, vals);

            }
        }

        internal void TotalBattle()
        {
            if (AttackingSquad != null)
            {
                foreach (Squad squad in AttackingSquad)
                {
                    BattleData.AttackingDeaths += squad.Deaths;
                    BattleData.AttackingNumber += squad.Number;
                    BattleData.AttackingSquads++;
                }
            }
            if (AttackingHF != null)
                BattleData.AttackingHFs += AttackingHF.Count;
            if (AttackingDiedHF != null)
                BattleData.AttackingHFDeaths += AttackingDiedHF.Count;

            if (DefendingSquad != null)
            {
                foreach (Squad squad in DefendingSquad)
                {
                    BattleData.DefendingDeaths += squad.Deaths;
                    BattleData.DefendingNumber += squad.Number;
                    BattleData.DefendingSquads++;
                }
            }

            if (DefendingHF != null)
                BattleData.DefendingHFs += DefendingHF.Count;
            if (DefendingDiedHF != null)
                BattleData.DefendingHFDeaths += DefendingDiedHF.Count;

            if (NonComHF != null)
            {
                foreach (HistoricalFigure hf in NonComHF)
                    BattleData.NonCombatHFs++;
            }
            battleTotaled = true;
        }


    }

    class Squad
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
        public Race Race { get; set; }
        public EntityPopulation EntityPopulation { get; set; }
        public int Number { get; set; }
        public int Deaths { get; set; }
        public Site Site { get; set; }

        public override string ToString()
        {
            if (Site != null)
                return Race.ToString() + " from " + Site.ToString() + " - " + (Number - Deaths).ToString() + "/" + Number;
            else
                return Race.ToString() + " - " + (Number - Deaths).ToString() + "/" + Number;
        }
    }
}
