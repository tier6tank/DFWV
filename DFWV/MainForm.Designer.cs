using System.ComponentModel;
using System.Windows.Forms;
using DFWV.Controls;
using LinkLabel = DFWV.Controls.LinkLabel;

namespace DFWV
{
    sealed partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.MainTab = new System.Windows.Forms.TabControl();
            this.tabWorld = new System.Windows.Forms.TabPage();
            this.grpWorld = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel38 = new System.Windows.Forms.TableLayoutPanel();
            this.WorldSummaryTree = new System.Windows.Forms.TreeView();
            this.IssuesBox = new System.Windows.Forms.RichTextBox();
            this.StatusBox = new System.Windows.Forms.TextBox();
            this.tabArtifact = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.grpArtifact = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel18 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblArtifactWCLabel = new System.Windows.Forms.Label();
            this.label191 = new System.Windows.Forms.Label();
            this.lblArtifactValue = new System.Windows.Forms.Label();
            this.label181 = new System.Windows.Forms.Label();
            this.lblArtifactDescription = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblArtifactName = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblArtifactItem = new System.Windows.Forms.Label();
            this.grpArtifactLost = new System.Windows.Forms.GroupBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.grpArtifactCreated = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.grpArtifactEvents = new System.Windows.Forms.GroupBox();
            this.lstArtifactEvents = new System.Windows.Forms.ListBox();
            this.FilterArtifact = new System.Windows.Forms.Button();
            this.lstArtifact = new System.Windows.Forms.ListBox();
            this.TextFilterArtifact = new System.Windows.Forms.TextBox();
            this.tabCivilization = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.grpCivilization = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel19 = new System.Windows.Forms.TableLayoutPanel();
            this.grpCivilizationWars = new System.Windows.Forms.GroupBox();
            this.lstCivilizationWars = new System.Windows.Forms.ListBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label17 = new System.Windows.Forms.Label();
            this.lblCivilizationName = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.lblCivilizationFull = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.grpCivilizationSites = new System.Windows.Forms.GroupBox();
            this.lstCivilizationSites = new System.Windows.Forms.ListBox();
            this.grpCivilizationLeaders = new System.Windows.Forms.GroupBox();
            this.lstCivilizationLeaders = new System.Windows.Forms.ListBox();
            this.grpCivilizationGods = new System.Windows.Forms.GroupBox();
            this.lstCivilizationGods = new System.Windows.Forms.ListBox();
            this.lstCivilization = new System.Windows.Forms.ListBox();
            this.FilterCivilization = new System.Windows.Forms.Button();
            this.TextFilterCivilization = new System.Windows.Forms.TextBox();
            this.tabEntity = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.FilterEntity = new System.Windows.Forms.Button();
            this.lstEntity = new System.Windows.Forms.ListBox();
            this.TextFilterEntity = new System.Windows.Forms.TextBox();
            this.grpEntity = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel20 = new System.Windows.Forms.TableLayoutPanel();
            this.grpEntityPositions = new System.Windows.Forms.GroupBox();
            this.lstEntityPositions = new System.Windows.Forms.ListBox();
            this.grpEntityRelatedSites = new System.Windows.Forms.GroupBox();
            this.trvEntityRelatedSites = new System.Windows.Forms.TreeView();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label185 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.lblEntityName = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.lblEntityType = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label50 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.grpEntitySiteTakeover = new System.Windows.Forms.GroupBox();
            this.label28 = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.grpEntityEvents = new System.Windows.Forms.GroupBox();
            this.lstEntityEvents = new System.Windows.Forms.ListBox();
            this.grpEntityCreated = new System.Windows.Forms.GroupBox();
            this.label19 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.grpEntityRelatedFigures = new System.Windows.Forms.GroupBox();
            this.trvEntityRelatedFigures = new System.Windows.Forms.TreeView();
            this.grpEntityRelatedEntities = new System.Windows.Forms.GroupBox();
            this.trvEntityRelatedEntities = new System.Windows.Forms.TreeView();
            this.tabEntityPopulation = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.TextFilterEntityPopulation = new System.Windows.Forms.TextBox();
            this.grpEntityPopulation = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel21 = new System.Windows.Forms.TableLayoutPanel();
            this.grpEntityPopluationRaces = new System.Windows.Forms.GroupBox();
            this.lstEntityPopluationRaces = new System.Windows.Forms.ListBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.lblEntityPopulationName = new System.Windows.Forms.Label();
            this.label218 = new System.Windows.Forms.Label();
            this.label186 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.grpEntityPopulationBattles = new System.Windows.Forms.GroupBox();
            this.label36 = new System.Windows.Forms.Label();
            this.lblEntityPopulationBattleDeaths = new System.Windows.Forms.Label();
            this.lblEntityPopulationBattleNumber = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.lstEntityPopulationBattles = new System.Windows.Forms.ListBox();
            this.grpEntityPopulationMembers = new System.Windows.Forms.GroupBox();
            this.lstEntityPopulationMembers = new System.Windows.Forms.ListBox();
            this.FilterEntityPopulation = new System.Windows.Forms.Button();
            this.lstEntityPopulation = new System.Windows.Forms.ListBox();
            this.tabGod = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.TextFilterGod = new System.Windows.Forms.TextBox();
            this.grpGod = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel22 = new System.Windows.Forms.TableLayoutPanel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.label39 = new System.Windows.Forms.Label();
            this.lblGodName = new System.Windows.Forms.Label();
            this.label37 = new System.Windows.Forms.Label();
            this.label38 = new System.Windows.Forms.Label();
            this.lblGodType = new System.Windows.Forms.Label();
            this.lblGodSpheres = new System.Windows.Forms.Label();
            this.label34 = new System.Windows.Forms.Label();
            this.grpGodCivilizations = new System.Windows.Forms.GroupBox();
            this.lstGodCivilizations = new System.Windows.Forms.ListBox();
            this.grpGodLeaders = new System.Windows.Forms.GroupBox();
            this.lstGodLeaders = new System.Windows.Forms.ListBox();
            this.FilterGod = new System.Windows.Forms.Button();
            this.lstGod = new System.Windows.Forms.ListBox();
            this.tabHistoricalFigure = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel7 = new System.Windows.Forms.TableLayoutPanel();
            this.TextFilterHistoricalFigure = new System.Windows.Forms.TextBox();
            this.lstHistoricalFigure = new System.Windows.Forms.ListBox();
            this.FilterHistoricalFigure = new System.Windows.Forms.Button();
            this.grpHistoricalFigure = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel23 = new System.Windows.Forms.TableLayoutPanel();
            this.grpHistoricalFigureArtifacts = new System.Windows.Forms.GroupBox();
            this.lstHistoricalFigureArtifacts = new System.Windows.Forms.ListBox();
            this.panel6 = new System.Windows.Forms.Panel();
            this.label311 = new System.Windows.Forms.Label();
            this.lblHistoricalFigureSex = new System.Windows.Forms.Label();
            this.label192 = new System.Windows.Forms.Label();
            this.label45 = new System.Windows.Forms.Label();
            this.lblHistoricalFigureName = new System.Windows.Forms.Label();
            this.label43 = new System.Windows.Forms.Label();
            this.label172 = new System.Windows.Forms.Label();
            this.label41 = new System.Windows.Forms.Label();
            this.lblHistoricalFigureAppeared = new System.Windows.Forms.Label();
            this.lblHistoricalFigureAge = new System.Windows.Forms.Label();
            this.label35 = new System.Windows.Forms.Label();
            this.lblHistoricalFigureAgeCaption = new System.Windows.Forms.Label();
            this.label47 = new System.Windows.Forms.Label();
            this.lblHistoricalFigureLife = new System.Windows.Forms.Label();
            this.label49 = new System.Windows.Forms.Label();
            this.lblHistoricalFigureAssociatedType = new System.Windows.Forms.Label();
            this.label55 = new System.Windows.Forms.Label();
            this.label59 = new System.Windows.Forms.Label();
            this.lblHistoricalFigureLocationText = new System.Windows.Forms.Label();
            this.label57 = new System.Windows.Forms.Label();
            this.label63 = new System.Windows.Forms.Label();
            this.lblHistoricalFigureAnimated = new System.Windows.Forms.Label();
            this.lblHistoricalFigureCaste = new System.Windows.Forms.Label();
            this.label61 = new System.Windows.Forms.Label();
            this.lblHistoricalFigureGhost = new System.Windows.Forms.Label();
            this.grpHistoricalFigureEvents = new System.Windows.Forms.GroupBox();
            this.lstHistoricalFigureEvents = new System.Windows.Forms.ListBox();
            this.grpHistoricalFigureDescendents = new System.Windows.Forms.GroupBox();
            this.trvHistoricalFigureDescendents = new System.Windows.Forms.TreeView();
            this.grpHistoricalFigureSkills = new System.Windows.Forms.GroupBox();
            this.lstHistoricalFigureSkills = new System.Windows.Forms.ListBox();
            this.grpHistoricalFigureAncestors = new System.Windows.Forms.GroupBox();
            this.trvHistoricalFigureAncestors = new System.Windows.Forms.TreeView();
            this.grpHistoricalFigureDeath = new System.Windows.Forms.GroupBox();
            this.lblHistoricalFigureDeathCause = new System.Windows.Forms.Label();
            this.label46 = new System.Windows.Forms.Label();
            this.label40 = new System.Windows.Forms.Label();
            this.label42 = new System.Windows.Forms.Label();
            this.label44 = new System.Windows.Forms.Label();
            this.grpHistoricalFigureSpheres = new System.Windows.Forms.GroupBox();
            this.lstHistoricalFigureSpheres = new System.Windows.Forms.ListBox();
            this.grpHistoricalFigureKnowledge = new System.Windows.Forms.GroupBox();
            this.lstHistoricalFigureKnowledge = new System.Windows.Forms.ListBox();
            this.grpHistoricalFigurePets = new System.Windows.Forms.GroupBox();
            this.lstHistoricalFigurePets = new System.Windows.Forms.ListBox();
            this.grpHistoricalFigureHFLinks = new System.Windows.Forms.GroupBox();
            this.trvHistoricalFigureHFLinks = new System.Windows.Forms.TreeView();
            this.grpHistoricalFigureEntityLinks = new System.Windows.Forms.GroupBox();
            this.trvHistoricalFigureEntityLinks = new System.Windows.Forms.TreeView();
            this.chkHistoricalFigureDetailedView = new System.Windows.Forms.CheckBox();
            this.tabHistoricalEra = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel8 = new System.Windows.Forms.TableLayoutPanel();
            this.lstHistoricalEra = new System.Windows.Forms.ListBox();
            this.FilterHistoricalEra = new System.Windows.Forms.Button();
            this.TextFilterHistoricalEra = new System.Windows.Forms.TextBox();
            this.grpHistoricalEra = new System.Windows.Forms.GroupBox();
            this.lblHistoricalEraStartYear = new System.Windows.Forms.Label();
            this.label52 = new System.Windows.Forms.Label();
            this.lblHistoricalEraName = new System.Windows.Forms.Label();
            this.label54 = new System.Windows.Forms.Label();
            this.tabHistoricalEvent = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel9 = new System.Windows.Forms.TableLayoutPanel();
            this.lstHistoricalEvent = new System.Windows.Forms.ListBox();
            this.TextFilterHistoricalEvent = new System.Windows.Forms.TextBox();
            this.grpHistoricalEvent = new System.Windows.Forms.GroupBox();
            this.FilterHistoricalEvent = new System.Windows.Forms.Button();
            this.tabHistoricalEventCollection = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel10 = new System.Windows.Forms.TableLayoutPanel();
            this.TextFilterHistoricalEventCollection = new System.Windows.Forms.TextBox();
            this.grpHistoricalEventCollection = new System.Windows.Forms.GroupBox();
            this.MainTabEventCollectionTypes = new System.Windows.Forms.TabControl();
            this.tabEventCollectionJourney = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel24 = new System.Windows.Forms.TableLayoutPanel();
            this.panel7 = new System.Windows.Forms.Panel();
            this.label162 = new System.Windows.Forms.Label();
            this.lblJourneyTime = new System.Windows.Forms.Label();
            this.label147 = new System.Windows.Forms.Label();
            this.label158 = new System.Windows.Forms.Label();
            this.lblJourneyOrdinal = new System.Windows.Forms.Label();
            this.lblJourneyDuration = new System.Windows.Forms.Label();
            this.grpJourneyEvents = new System.Windows.Forms.GroupBox();
            this.lstJourneyEvents = new System.Windows.Forms.ListBox();
            this.tabEventCollectionBeastAttack = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel25 = new System.Windows.Forms.TableLayoutPanel();
            this.panel8 = new System.Windows.Forms.Panel();
            this.label159 = new System.Windows.Forms.Label();
            this.label168 = new System.Windows.Forms.Label();
            this.label137 = new System.Windows.Forms.Label();
            this.label143 = new System.Windows.Forms.Label();
            this.lblBeastAttackOrdinal = new System.Windows.Forms.Label();
            this.label152 = new System.Windows.Forms.Label();
            this.label160 = new System.Windows.Forms.Label();
            this.lblBeastAttackDuration = new System.Windows.Forms.Label();
            this.lblBeastAttackTime = new System.Windows.Forms.Label();
            this.label155 = new System.Windows.Forms.Label();
            this.label157 = new System.Windows.Forms.Label();
            this.label156 = new System.Windows.Forms.Label();
            this.grpBeastAttackEvents = new System.Windows.Forms.GroupBox();
            this.lstBeastAttackEvents = new System.Windows.Forms.ListBox();
            this.tabEventCollectionWar = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel26 = new System.Windows.Forms.TableLayoutPanel();
            this.panel9 = new System.Windows.Forms.Panel();
            this.label103 = new System.Windows.Forms.Label();
            this.lblWarName = new System.Windows.Forms.Label();
            this.label101 = new System.Windows.Forms.Label();
            this.lblWarTime = new System.Windows.Forms.Label();
            this.label99 = new System.Windows.Forms.Label();
            this.lblWarDuration = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.lblWarDefenderWins = new System.Windows.Forms.Label();
            this.label124 = new System.Windows.Forms.Label();
            this.lblWarDefenderSquads = new System.Windows.Forms.Label();
            this.label126 = new System.Windows.Forms.Label();
            this.lblWarDefenderLosses = new System.Windows.Forms.Label();
            this.lblWarDefenderCombatants = new System.Windows.Forms.Label();
            this.label129 = new System.Windows.Forms.Label();
            this.label130 = new System.Windows.Forms.Label();
            this.grpWarEventCols = new System.Windows.Forms.GroupBox();
            this.lstWarEventCols = new System.Windows.Forms.ListBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lblWarAggressorWins = new System.Windows.Forms.Label();
            this.label122 = new System.Windows.Forms.Label();
            this.lblWarAggressorSquads = new System.Windows.Forms.Label();
            this.label120 = new System.Windows.Forms.Label();
            this.lblWarAggressorLosses = new System.Windows.Forms.Label();
            this.lblWarAggressorCombatants = new System.Windows.Forms.Label();
            this.label117 = new System.Windows.Forms.Label();
            this.label118 = new System.Windows.Forms.Label();
            this.grpWarEvents = new System.Windows.Forms.GroupBox();
            this.lstWarEvents = new System.Windows.Forms.ListBox();
            this.tabEventCollectionBattle = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel27 = new System.Windows.Forms.TableLayoutPanel();
            this.panel10 = new System.Windows.Forms.Panel();
            this.label96 = new System.Windows.Forms.Label();
            this.lblBattleName = new System.Windows.Forms.Label();
            this.label94 = new System.Windows.Forms.Label();
            this.lblBattleDuration = new System.Windows.Forms.Label();
            this.label92 = new System.Windows.Forms.Label();
            this.label98 = new System.Windows.Forms.Label();
            this.lblBattleTime = new System.Windows.Forms.Label();
            this.label97 = new System.Windows.Forms.Label();
            this.label95 = new System.Windows.Forms.Label();
            this.label90 = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.lblBattleDefenderLosses = new System.Windows.Forms.Label();
            this.lblBattleDefenderCombatants = new System.Windows.Forms.Label();
            this.label113 = new System.Windows.Forms.Label();
            this.label114 = new System.Windows.Forms.Label();
            this.lblBattleDefenderOutcome = new System.Windows.Forms.Label();
            this.grpBattleDefendingSquad = new System.Windows.Forms.GroupBox();
            this.lblBattleDefendingSquadDeaths = new System.Windows.Forms.Label();
            this.label106 = new System.Windows.Forms.Label();
            this.lblBattleDefendingSquadNumber = new System.Windows.Forms.Label();
            this.label109 = new System.Windows.Forms.Label();
            this.label110 = new System.Windows.Forms.Label();
            this.label111 = new System.Windows.Forms.Label();
            this.label112 = new System.Windows.Forms.Label();
            this.lstBattleDefendingSquad = new System.Windows.Forms.ListBox();
            this.grpBattleDefendingHF = new System.Windows.Forms.GroupBox();
            this.lstBattleDefendingHF = new System.Windows.Forms.ListBox();
            this.grpBattleEvents = new System.Windows.Forms.GroupBox();
            this.lstBattleEvents = new System.Windows.Forms.ListBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblBattleAttackerLosses = new System.Windows.Forms.Label();
            this.lblBattleAttackerCombatants = new System.Windows.Forms.Label();
            this.label108 = new System.Windows.Forms.Label();
            this.label104 = new System.Windows.Forms.Label();
            this.lblBattleAttackerOutcome = new System.Windows.Forms.Label();
            this.grpBattleAttackingSquad = new System.Windows.Forms.GroupBox();
            this.lblBattleAttackingSquadDeaths = new System.Windows.Forms.Label();
            this.label105 = new System.Windows.Forms.Label();
            this.lblBattleAttackingSquadNumber = new System.Windows.Forms.Label();
            this.label107 = new System.Windows.Forms.Label();
            this.label93 = new System.Windows.Forms.Label();
            this.label100 = new System.Windows.Forms.Label();
            this.label102 = new System.Windows.Forms.Label();
            this.lstBattleAttackingSquad = new System.Windows.Forms.ListBox();
            this.grpBattleAttackingHF = new System.Windows.Forms.GroupBox();
            this.lstBattleAttackingHF = new System.Windows.Forms.ListBox();
            this.grpBattleEventCols = new System.Windows.Forms.GroupBox();
            this.lstBattleEventCols = new System.Windows.Forms.ListBox();
            this.grpBattleNonComHFs = new System.Windows.Forms.GroupBox();
            this.lstBattleNonComHFs = new System.Windows.Forms.ListBox();
            this.tabEventCollectionDuel = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel28 = new System.Windows.Forms.TableLayoutPanel();
            this.panel11 = new System.Windows.Forms.Panel();
            this.label148 = new System.Windows.Forms.Label();
            this.label125 = new System.Windows.Forms.Label();
            this.label133 = new System.Windows.Forms.Label();
            this.lblDuelOrdinal = new System.Windows.Forms.Label();
            this.label141 = new System.Windows.Forms.Label();
            this.label142 = new System.Windows.Forms.Label();
            this.label149 = new System.Windows.Forms.Label();
            this.lblDuelDuration = new System.Windows.Forms.Label();
            this.lblDuelTime = new System.Windows.Forms.Label();
            this.label144 = new System.Windows.Forms.Label();
            this.label146 = new System.Windows.Forms.Label();
            this.label145 = new System.Windows.Forms.Label();
            this.grpDuelEvents = new System.Windows.Forms.GroupBox();
            this.lstDuelEvents = new System.Windows.Forms.ListBox();
            this.tabEventCollectionAbduction = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel29 = new System.Windows.Forms.TableLayoutPanel();
            this.grpAbductionEventCols = new System.Windows.Forms.GroupBox();
            this.lstAbductionEventCols = new System.Windows.Forms.ListBox();
            this.panel12 = new System.Windows.Forms.Panel();
            this.label138 = new System.Windows.Forms.Label();
            this.label140 = new System.Windows.Forms.Label();
            this.label123 = new System.Windows.Forms.Label();
            this.lblAbductionOrdinal = new System.Windows.Forms.Label();
            this.label131 = new System.Windows.Forms.Label();
            this.label132 = new System.Windows.Forms.Label();
            this.label139 = new System.Windows.Forms.Label();
            this.lblAbductionDuration = new System.Windows.Forms.Label();
            this.lblAbductionTime = new System.Windows.Forms.Label();
            this.label134 = new System.Windows.Forms.Label();
            this.label136 = new System.Windows.Forms.Label();
            this.label135 = new System.Windows.Forms.Label();
            this.grpAbductionEvents = new System.Windows.Forms.GroupBox();
            this.lstAbductionEvents = new System.Windows.Forms.ListBox();
            this.tabEventCollectionSiteConquered = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel30 = new System.Windows.Forms.TableLayoutPanel();
            this.grpSiteConqueredEvents = new System.Windows.Forms.GroupBox();
            this.lstSiteConqueredEvents = new System.Windows.Forms.ListBox();
            this.panel13 = new System.Windows.Forms.Panel();
            this.label127 = new System.Windows.Forms.Label();
            this.label115 = new System.Windows.Forms.Label();
            this.lblSiteConqueredOrdinal = new System.Windows.Forms.Label();
            this.label151 = new System.Windows.Forms.Label();
            this.label150 = new System.Windows.Forms.Label();
            this.label128 = new System.Windows.Forms.Label();
            this.lblSiteConqueredDuration = new System.Windows.Forms.Label();
            this.lblSiteConqueredTime = new System.Windows.Forms.Label();
            this.label116 = new System.Windows.Forms.Label();
            this.label121 = new System.Windows.Forms.Label();
            this.label119 = new System.Windows.Forms.Label();
            this.tabEventCollectionTheft = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel31 = new System.Windows.Forms.TableLayoutPanel();
            this.grpTheftEventCols = new System.Windows.Forms.GroupBox();
            this.lstTheftEventCols = new System.Windows.Forms.ListBox();
            this.panel14 = new System.Windows.Forms.Panel();
            this.label169 = new System.Windows.Forms.Label();
            this.label153 = new System.Windows.Forms.Label();
            this.lblTheftOrdinal = new System.Windows.Forms.Label();
            this.label161 = new System.Windows.Forms.Label();
            this.label163 = new System.Windows.Forms.Label();
            this.label170 = new System.Windows.Forms.Label();
            this.lblTheftDuration = new System.Windows.Forms.Label();
            this.lblTheftTime = new System.Windows.Forms.Label();
            this.label165 = new System.Windows.Forms.Label();
            this.label167 = new System.Windows.Forms.Label();
            this.label166 = new System.Windows.Forms.Label();
            this.grpTheftEvents = new System.Windows.Forms.GroupBox();
            this.lstTheftEvents = new System.Windows.Forms.ListBox();
            this.tabEventCollectionInsurrection = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel39 = new System.Windows.Forms.TableLayoutPanel();
            this.panel20 = new System.Windows.Forms.Panel();
            this.label179 = new System.Windows.Forms.Label();
            this.label178 = new System.Windows.Forms.Label();
            this.label180 = new System.Windows.Forms.Label();
            this.lblInsurrectionOrdinal = new System.Windows.Forms.Label();
            this.label183 = new System.Windows.Forms.Label();
            this.label184 = new System.Windows.Forms.Label();
            this.lblInsurrectionDuration = new System.Windows.Forms.Label();
            this.lblInsurrectionTime = new System.Windows.Forms.Label();
            this.label187 = new System.Windows.Forms.Label();
            this.label188 = new System.Windows.Forms.Label();
            this.label189 = new System.Windows.Forms.Label();
            this.grpInsurrectionEvents = new System.Windows.Forms.GroupBox();
            this.lstInsurrectionEvents = new System.Windows.Forms.ListBox();
            this.tabEventCollectionOccasion = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel57 = new System.Windows.Forms.TableLayoutPanel();
            this.grpOccasionEventCols = new System.Windows.Forms.GroupBox();
            this.lstOccasionEventCols = new System.Windows.Forms.ListBox();
            this.panel22 = new System.Windows.Forms.Panel();
            this.label231 = new System.Windows.Forms.Label();
            this.lblOccasionID = new System.Windows.Forms.Label();
            this.label230 = new System.Windows.Forms.Label();
            this.label232 = new System.Windows.Forms.Label();
            this.lblOccasionOrdinal = new System.Windows.Forms.Label();
            this.label235 = new System.Windows.Forms.Label();
            this.lblOccasionDuration = new System.Windows.Forms.Label();
            this.lblOccasionTime = new System.Windows.Forms.Label();
            this.label239 = new System.Windows.Forms.Label();
            this.grpOccasionEvents = new System.Windows.Forms.GroupBox();
            this.lstOccasionEvents = new System.Windows.Forms.ListBox();
            this.tabEventCollectionCeremony = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel58 = new System.Windows.Forms.TableLayoutPanel();
            this.panel23 = new System.Windows.Forms.Panel();
            this.label233 = new System.Windows.Forms.Label();
            this.lblCeremonyTime = new System.Windows.Forms.Label();
            this.label236 = new System.Windows.Forms.Label();
            this.label240 = new System.Windows.Forms.Label();
            this.lblCeremonyOrdinal = new System.Windows.Forms.Label();
            this.lblCeremonyDuration = new System.Windows.Forms.Label();
            this.grpCeremonyEvents = new System.Windows.Forms.GroupBox();
            this.lstCeremonyEvents = new System.Windows.Forms.ListBox();
            this.tabEventCollectionProcession = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel59 = new System.Windows.Forms.TableLayoutPanel();
            this.panel24 = new System.Windows.Forms.Panel();
            this.label234 = new System.Windows.Forms.Label();
            this.lblProcessionTime = new System.Windows.Forms.Label();
            this.label238 = new System.Windows.Forms.Label();
            this.label241 = new System.Windows.Forms.Label();
            this.lblProcessionOrdinal = new System.Windows.Forms.Label();
            this.lblProcessionDuration = new System.Windows.Forms.Label();
            this.grpProcessionEvents = new System.Windows.Forms.GroupBox();
            this.lstProcessionEvents = new System.Windows.Forms.ListBox();
            this.tabEventCollectionPerformance = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel60 = new System.Windows.Forms.TableLayoutPanel();
            this.panel25 = new System.Windows.Forms.Panel();
            this.label237 = new System.Windows.Forms.Label();
            this.lblPerformanceTime = new System.Windows.Forms.Label();
            this.label243 = new System.Windows.Forms.Label();
            this.label244 = new System.Windows.Forms.Label();
            this.lblPerformanceOrdinal = new System.Windows.Forms.Label();
            this.lblPerformanceDuration = new System.Windows.Forms.Label();
            this.grpPerformanceEvents = new System.Windows.Forms.GroupBox();
            this.lstPerformanceEvents = new System.Windows.Forms.ListBox();
            this.tabEventCollectionCompetition = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel61 = new System.Windows.Forms.TableLayoutPanel();
            this.panel26 = new System.Windows.Forms.Panel();
            this.label242 = new System.Windows.Forms.Label();
            this.lblCompetitionTime = new System.Windows.Forms.Label();
            this.label246 = new System.Windows.Forms.Label();
            this.label247 = new System.Windows.Forms.Label();
            this.lblCompetitionOrdinal = new System.Windows.Forms.Label();
            this.lblCompetitionDuration = new System.Windows.Forms.Label();
            this.grpCompetitionEvents = new System.Windows.Forms.GroupBox();
            this.lstCompetitionEvents = new System.Windows.Forms.ListBox();
            this.tabEventCollectionPurge = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel45 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lstPurgeEvents = new System.Windows.Forms.ListBox();
            this.panel27 = new System.Windows.Forms.Panel();
            this.lblPurgeOrdinal = new System.Windows.Forms.Label();
            this.label205 = new System.Windows.Forms.Label();
            this.lblPurgeAdjective = new System.Windows.Forms.Label();
            this.label301 = new System.Windows.Forms.Label();
            this.lblPurgeDuration = new System.Windows.Forms.Label();
            this.lblPurgeTime = new System.Windows.Forms.Label();
            this.label315 = new System.Windows.Forms.Label();
            this.label318 = new System.Windows.Forms.Label();
            this.label320 = new System.Windows.Forms.Label();
            this.FilterHistoricalEventCollection = new System.Windows.Forms.Button();
            this.lstHistoricalEventCollection = new System.Windows.Forms.ListBox();
            this.tabLeader = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel11 = new System.Windows.Forms.TableLayoutPanel();
            this.TextFilterLeader = new System.Windows.Forms.TextBox();
            this.grpLeader = new System.Windows.Forms.GroupBox();
            this.label176 = new System.Windows.Forms.Label();
            this.lblLeaderType = new System.Windows.Forms.Label();
            this.label51 = new System.Windows.Forms.Label();
            this.label67 = new System.Windows.Forms.Label();
            this.lblLeaderLife = new System.Windows.Forms.Label();
            this.label64 = new System.Windows.Forms.Label();
            this.label65 = new System.Windows.Forms.Label();
            this.label66 = new System.Windows.Forms.Label();
            this.label68 = new System.Windows.Forms.Label();
            this.lblLeaderInheritance = new System.Windows.Forms.Label();
            this.label70 = new System.Windows.Forms.Label();
            this.label71 = new System.Windows.Forms.Label();
            this.lblLeaderReignBegan = new System.Windows.Forms.Label();
            this.label73 = new System.Windows.Forms.Label();
            this.label74 = new System.Windows.Forms.Label();
            this.lblLeaderName = new System.Windows.Forms.Label();
            this.label76 = new System.Windows.Forms.Label();
            this.FilterLeader = new System.Windows.Forms.Button();
            this.lstLeader = new System.Windows.Forms.ListBox();
            this.tabParameter = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel12 = new System.Windows.Forms.TableLayoutPanel();
            this.FilterParameter = new System.Windows.Forms.Button();
            this.TextFilterParameter = new System.Windows.Forms.TextBox();
            this.grpParameter = new System.Windows.Forms.GroupBox();
            this.lblParameterData = new System.Windows.Forms.Label();
            this.label56 = new System.Windows.Forms.Label();
            this.lblParameterName = new System.Windows.Forms.Label();
            this.label60 = new System.Windows.Forms.Label();
            this.lstParameter = new System.Windows.Forms.ListBox();
            this.tabRace = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel13 = new System.Windows.Forms.TableLayoutPanel();
            this.FilterRace = new System.Windows.Forms.Button();
            this.TextFilterRace = new System.Windows.Forms.TextBox();
            this.grpRace = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel32 = new System.Windows.Forms.TableLayoutPanel();
            this.grpRacePopulation = new System.Windows.Forms.GroupBox();
            this.lstRacePopulation = new System.Windows.Forms.ListBox();
            this.panel15 = new System.Windows.Forms.Panel();
            this.grpRaceCastes = new System.Windows.Forms.GroupBox();
            this.lblRaceCasteGender = new System.Windows.Forms.Label();
            this.label193 = new System.Windows.Forms.Label();
            this.lblRaceCasteDescription = new System.Windows.Forms.Label();
            this.lstRaceCastes = new System.Windows.Forms.ListBox();
            this.label58 = new System.Windows.Forms.Label();
            this.lblRaceName = new System.Windows.Forms.Label();
            this.lblRacePopulation = new System.Windows.Forms.Label();
            this.label174 = new System.Windows.Forms.Label();
            this.grpRaceHistoricalFigures = new System.Windows.Forms.GroupBox();
            this.lstRaceHistoricalFigures = new System.Windows.Forms.ListBox();
            this.grpRaceLeaders = new System.Windows.Forms.GroupBox();
            this.lstRaceLeaders = new System.Windows.Forms.ListBox();
            this.grpRaceCivilizations = new System.Windows.Forms.GroupBox();
            this.lstRaceCivilizations = new System.Windows.Forms.ListBox();
            this.lstRace = new System.Windows.Forms.ListBox();
            this.tabRegion = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.TextFilterRegion = new System.Windows.Forms.TextBox();
            this.grpRegion = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel33 = new System.Windows.Forms.TableLayoutPanel();
            this.grpRegionPopulation = new System.Windows.Forms.GroupBox();
            this.lstRegionPopulation = new System.Windows.Forms.ListBox();
            this.panel16 = new System.Windows.Forms.Panel();
            this.label72 = new System.Windows.Forms.Label();
            this.lblRegionName = new System.Windows.Forms.Label();
            this.label62 = new System.Windows.Forms.Label();
            this.lblRegionType = new System.Windows.Forms.Label();
            this.grpRegionEvents = new System.Windows.Forms.GroupBox();
            this.lstRegionEvents = new System.Windows.Forms.ListBox();
            this.grpRegionBattles = new System.Windows.Forms.GroupBox();
            this.lstRegionBattles = new System.Windows.Forms.ListBox();
            this.grpRegionInhabitants = new System.Windows.Forms.GroupBox();
            this.lstRegionInhabitants = new System.Windows.Forms.ListBox();
            this.FilterRegion = new System.Windows.Forms.Button();
            this.lstRegion = new System.Windows.Forms.ListBox();
            this.tabSite = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel14 = new System.Windows.Forms.TableLayoutPanel();
            this.TextFilterSite = new System.Windows.Forms.TextBox();
            this.grpSite = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel34 = new System.Windows.Forms.TableLayoutPanel();
            this.grpSiteArtifacts = new System.Windows.Forms.GroupBox();
            this.lstSiteArtifacts = new System.Windows.Forms.ListBox();
            this.grpSiteStructures = new System.Windows.Forms.GroupBox();
            this.lstSiteStructures = new System.Windows.Forms.ListBox();
            this.panel17 = new System.Windows.Forms.Panel();
            this.label207 = new System.Windows.Forms.Label();
            this.label295 = new System.Windows.Forms.Label();
            this.label80 = new System.Windows.Forms.Label();
            this.lblSiteName = new System.Windows.Forms.Label();
            this.label78 = new System.Windows.Forms.Label();
            this.lblSiteAltName = new System.Windows.Forms.Label();
            this.label83 = new System.Windows.Forms.Label();
            this.label75 = new System.Windows.Forms.Label();
            this.label53 = new System.Windows.Forms.Label();
            this.lblSiteType = new System.Windows.Forms.Label();
            this.label81 = new System.Windows.Forms.Label();
            this.grpSiteOutcasts = new System.Windows.Forms.GroupBox();
            this.lstSiteOutcasts = new System.Windows.Forms.ListBox();
            this.grpSiteCreated = new System.Windows.Forms.GroupBox();
            this.label69 = new System.Windows.Forms.Label();
            this.label77 = new System.Windows.Forms.Label();
            this.label79 = new System.Windows.Forms.Label();
            this.grpSitePrisoners = new System.Windows.Forms.GroupBox();
            this.lstSitePrisoners = new System.Windows.Forms.ListBox();
            this.grpSiteInhabitants = new System.Windows.Forms.GroupBox();
            this.lstSiteInhabitants = new System.Windows.Forms.ListBox();
            this.grpSitePopulation = new System.Windows.Forms.GroupBox();
            this.lstSitePopulation = new System.Windows.Forms.ListBox();
            this.grpSiteEventCollection = new System.Windows.Forms.GroupBox();
            this.trvSiteEventCollection = new System.Windows.Forms.TreeView();
            this.grpSiteEvent = new System.Windows.Forms.GroupBox();
            this.lstSiteEvent = new System.Windows.Forms.ListBox();
            this.FilterSite = new System.Windows.Forms.Button();
            this.lstSite = new System.Windows.Forms.ListBox();
            this.tabStructure = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel36 = new System.Windows.Forms.TableLayoutPanel();
            this.TextFilterStructure = new System.Windows.Forms.TextBox();
            this.grpStructure = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel37 = new System.Windows.Forms.TableLayoutPanel();
            this.grpStructureEntombedHF = new System.Windows.Forms.GroupBox();
            this.lstStructureEntombedHF = new System.Windows.Forms.ListBox();
            this.grpStructureEvents = new System.Windows.Forms.GroupBox();
            this.lstStructureEvents = new System.Windows.Forms.ListBox();
            this.grpStructureRazed = new System.Windows.Forms.GroupBox();
            this.label48 = new System.Windows.Forms.Label();
            this.label175 = new System.Windows.Forms.Label();
            this.label177 = new System.Windows.Forms.Label();
            this.grpStructureCreated = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.panel19 = new System.Windows.Forms.Panel();
            this.label182 = new System.Windows.Forms.Label();
            this.lblStructureType = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblStructureID = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.FilterStructure = new System.Windows.Forms.Button();
            this.lstStructure = new System.Windows.Forms.ListBox();
            this.tabUndergroundRegion = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel15 = new System.Windows.Forms.TableLayoutPanel();
            this.TextFilterUndergroundRegion = new System.Windows.Forms.TextBox();
            this.grpUndergroundRegion = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel40 = new System.Windows.Forms.TableLayoutPanel();
            this.grpUndergroundRegionPopulation = new System.Windows.Forms.GroupBox();
            this.lstUndergroundRegionPopulation = new System.Windows.Forms.ListBox();
            this.panel21 = new System.Windows.Forms.Panel();
            this.label86 = new System.Windows.Forms.Label();
            this.lblUndergroundRegionType = new System.Windows.Forms.Label();
            this.lblUndergroundRegionDepth = new System.Windows.Forms.Label();
            this.label84 = new System.Windows.Forms.Label();
            this.FilterUndergroundRegion = new System.Windows.Forms.Button();
            this.lstUndergroundRegion = new System.Windows.Forms.ListBox();
            this.tabWorldConstruction = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel16 = new System.Windows.Forms.TableLayoutPanel();
            this.FilterWorldConstruction = new System.Windows.Forms.Button();
            this.TextFilterWorldConstruction = new System.Windows.Forms.TextBox();
            this.grpWorldConstruction = new System.Windows.Forms.GroupBox();
            this.label190 = new System.Windows.Forms.Label();
            this.label33 = new System.Windows.Forms.Label();
            this.grpWorldConstructionCreated = new System.Windows.Forms.GroupBox();
            this.label85 = new System.Windows.Forms.Label();
            this.label87 = new System.Windows.Forms.Label();
            this.label88 = new System.Windows.Forms.Label();
            this.label82 = new System.Windows.Forms.Label();
            this.label89 = new System.Windows.Forms.Label();
            this.label91 = new System.Windows.Forms.Label();
            this.lstWorldConstruction = new System.Windows.Forms.ListBox();
            this.tabDynasty = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel17 = new System.Windows.Forms.TableLayoutPanel();
            this.TextFilterDynasty = new System.Windows.Forms.TextBox();
            this.FilterDynasty = new System.Windows.Forms.Button();
            this.lstDynasty = new System.Windows.Forms.ListBox();
            this.grpDynasty = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel35 = new System.Windows.Forms.TableLayoutPanel();
            this.panel18 = new System.Windows.Forms.Panel();
            this.label173 = new System.Windows.Forms.Label();
            this.label171 = new System.Windows.Forms.Label();
            this.lblDynastyType = new System.Windows.Forms.Label();
            this.label164 = new System.Windows.Forms.Label();
            this.label154 = new System.Windows.Forms.Label();
            this.grpDynastyMembers = new System.Windows.Forms.GroupBox();
            this.lstDynastyMembers = new System.Windows.Forms.ListBox();
            this.tabLandmass = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel47 = new System.Windows.Forms.TableLayoutPanel();
            this.FilterLandmass = new System.Windows.Forms.Button();
            this.TextFilterLandmass = new System.Windows.Forms.TextBox();
            this.grpLandmass = new System.Windows.Forms.GroupBox();
            this.label322 = new System.Windows.Forms.Label();
            this.lblLandmassArea = new System.Windows.Forms.Label();
            this.label330 = new System.Windows.Forms.Label();
            this.label333 = new System.Windows.Forms.Label();
            this.label324 = new System.Windows.Forms.Label();
            this.lblLandmassName = new System.Windows.Forms.Label();
            this.label329 = new System.Windows.Forms.Label();
            this.lstLandmass = new System.Windows.Forms.ListBox();
            this.tabMountain = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel41 = new System.Windows.Forms.TableLayoutPanel();
            this.FilterMountain = new System.Windows.Forms.Button();
            this.TextFilterMountain = new System.Windows.Forms.TextBox();
            this.grpMountain = new System.Windows.Forms.GroupBox();
            this.lblMountainHeight = new System.Windows.Forms.Label();
            this.label198 = new System.Windows.Forms.Label();
            this.label195 = new System.Windows.Forms.Label();
            this.lblMountainAltName = new System.Windows.Forms.Label();
            this.label194 = new System.Windows.Forms.Label();
            this.lblMountainName = new System.Windows.Forms.Label();
            this.label196 = new System.Windows.Forms.Label();
            this.lstMountain = new System.Windows.Forms.ListBox();
            this.tabRiver = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel42 = new System.Windows.Forms.TableLayoutPanel();
            this.FilterRiver = new System.Windows.Forms.Button();
            this.TextFilterRiver = new System.Windows.Forms.TextBox();
            this.grpRiver = new System.Windows.Forms.GroupBox();
            this.grpRiverTributaries = new System.Windows.Forms.GroupBox();
            this.lstRiverTributaries = new System.Windows.Forms.ListBox();
            this.label200 = new System.Windows.Forms.Label();
            this.lblRiverElevation = new System.Windows.Forms.Label();
            this.label197 = new System.Windows.Forms.Label();
            this.label199 = new System.Windows.Forms.Label();
            this.lblRiverAltName = new System.Windows.Forms.Label();
            this.label201 = new System.Windows.Forms.Label();
            this.lblRiverName = new System.Windows.Forms.Label();
            this.label203 = new System.Windows.Forms.Label();
            this.lstRiver = new System.Windows.Forms.ListBox();
            this.tabArmy = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel43 = new System.Windows.Forms.TableLayoutPanel();
            this.FilterArmy = new System.Windows.Forms.Button();
            this.TextFilterArmy = new System.Windows.Forms.TextBox();
            this.grpArmy = new System.Windows.Forms.GroupBox();
            this.lblArmyMaterial = new System.Windows.Forms.Label();
            this.label262 = new System.Windows.Forms.Label();
            this.lblArmyItemSubtype = new System.Windows.Forms.Label();
            this.label252 = new System.Windows.Forms.Label();
            this.lblArmyItemType = new System.Windows.Forms.Label();
            this.label254 = new System.Windows.Forms.Label();
            this.label250 = new System.Windows.Forms.Label();
            this.lblArmyLocation = new System.Windows.Forms.Label();
            this.label248 = new System.Windows.Forms.Label();
            this.lstArmy = new System.Windows.Forms.ListBox();
            this.tabUnit = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel44 = new System.Windows.Forms.TableLayoutPanel();
            this.FilterUnit = new System.Windows.Forms.Button();
            this.TextFilterUnit = new System.Windows.Forms.TextBox();
            this.grpUnit = new System.Windows.Forms.GroupBox();
            this.grpUnitReferences = new System.Windows.Forms.GroupBox();
            this.lstUnitReferences = new System.Windows.Forms.ListBox();
            this.grpUnitOwnedBuildings = new System.Windows.Forms.GroupBox();
            this.lstUnitOwnedBuildings = new System.Windows.Forms.ListBox();
            this.grpUnitInventory = new System.Windows.Forms.GroupBox();
            this.lstUnitInventory = new System.Windows.Forms.ListBox();
            this.grpUnitItems = new System.Windows.Forms.GroupBox();
            this.trvUnitItems = new System.Windows.Forms.TreeView();
            this.grpUnitHealth = new System.Windows.Forms.GroupBox();
            this.lstUnitHealth = new System.Windows.Forms.ListBox();
            this.grpUnitRelations = new System.Windows.Forms.GroupBox();
            this.lstUnitRelations = new System.Windows.Forms.ListBox();
            this.grpUnitFlags = new System.Windows.Forms.GroupBox();
            this.lstUnitFlags = new System.Windows.Forms.ListBox();
            this.grpUnitLabors = new System.Windows.Forms.GroupBox();
            this.lstUnitLabors = new System.Windows.Forms.ListBox();
            this.label272 = new System.Windows.Forms.Label();
            this.label274 = new System.Windows.Forms.Label();
            this.lblUnitProfession = new System.Windows.Forms.Label();
            this.label276 = new System.Windows.Forms.Label();
            this.lblUnitCaste = new System.Windows.Forms.Label();
            this.label278 = new System.Windows.Forms.Label();
            this.label280 = new System.Windows.Forms.Label();
            this.label282 = new System.Windows.Forms.Label();
            this.lblUnitMood = new System.Windows.Forms.Label();
            this.label284 = new System.Windows.Forms.Label();
            this.label286 = new System.Windows.Forms.Label();
            this.label288 = new System.Windows.Forms.Label();
            this.lblUnitSex = new System.Windows.Forms.Label();
            this.label290 = new System.Windows.Forms.Label();
            this.lblUnitCoords = new System.Windows.Forms.Label();
            this.label292 = new System.Windows.Forms.Label();
            this.lblUnitAltName = new System.Windows.Forms.Label();
            this.label294 = new System.Windows.Forms.Label();
            this.lblUnitName = new System.Windows.Forms.Label();
            this.label202 = new System.Windows.Forms.Label();
            this.lstUnit = new System.Windows.Forms.ListBox();
            this.tabEngraving = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel46 = new System.Windows.Forms.TableLayoutPanel();
            this.FilterEngraving = new System.Windows.Forms.Button();
            this.TextFilterEngraving = new System.Windows.Forms.TextBox();
            this.grpEngraving = new System.Windows.Forms.GroupBox();
            this.label204 = new System.Windows.Forms.Label();
            this.lblEngravingHidden = new System.Windows.Forms.Label();
            this.lblEngravingLocation = new System.Windows.Forms.Label();
            this.lblEngravingArtSubID = new System.Windows.Forms.Label();
            this.lblEngravingArtID = new System.Windows.Forms.Label();
            this.label257 = new System.Windows.Forms.Label();
            this.label258 = new System.Windows.Forms.Label();
            this.lblEngravingQuality = new System.Windows.Forms.Label();
            this.label260 = new System.Windows.Forms.Label();
            this.label261 = new System.Windows.Forms.Label();
            this.label263 = new System.Windows.Forms.Label();
            this.lblEngravingTile = new System.Windows.Forms.Label();
            this.label265 = new System.Windows.Forms.Label();
            this.lblEngravingCoords = new System.Windows.Forms.Label();
            this.label267 = new System.Windows.Forms.Label();
            this.lblEngravingSkill = new System.Windows.Forms.Label();
            this.label269 = new System.Windows.Forms.Label();
            this.label271 = new System.Windows.Forms.Label();
            this.lblEngravingName = new System.Windows.Forms.Label();
            this.label209 = new System.Windows.Forms.Label();
            this.lstEngraving = new System.Windows.Forms.ListBox();
            this.tabReport = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel50 = new System.Windows.Forms.TableLayoutPanel();
            this.FilterReport = new System.Windows.Forms.Button();
            this.TextFilterReport = new System.Windows.Forms.TextBox();
            this.grpReport = new System.Windows.Forms.GroupBox();
            this.lblReportContinuation = new System.Windows.Forms.Label();
            this.lblReportAnnouncement = new System.Windows.Forms.Label();
            this.lblReportTime = new System.Windows.Forms.Label();
            this.label214 = new System.Windows.Forms.Label();
            this.lblReportType = new System.Windows.Forms.Label();
            this.label210 = new System.Windows.Forms.Label();
            this.lblReportText = new System.Windows.Forms.Label();
            this.label217 = new System.Windows.Forms.Label();
            this.lstReport = new System.Windows.Forms.ListBox();
            this.tabWrittenContent = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel51 = new System.Windows.Forms.TableLayoutPanel();
            this.FilterWrittenContent = new System.Windows.Forms.Button();
            this.TextFilterWrittenContent = new System.Windows.Forms.TextBox();
            this.grpWrittenContent = new System.Windows.Forms.GroupBox();
            this.grpWrittenContentReferences = new System.Windows.Forms.GroupBox();
            this.lstWrittenContentReferences = new System.Windows.Forms.ListBox();
            this.lblWrittenContentStyle = new System.Windows.Forms.Label();
            this.label309 = new System.Windows.Forms.Label();
            this.lblWrittenContentType = new System.Windows.Forms.Label();
            this.label273 = new System.Windows.Forms.Label();
            this.lblWrittenContentPages = new System.Windows.Forms.Label();
            this.label312 = new System.Windows.Forms.Label();
            this.label314 = new System.Windows.Forms.Label();
            this.lblWrittenContentTitle = new System.Windows.Forms.Label();
            this.label219 = new System.Windows.Forms.Label();
            this.lstWrittenContent = new System.Windows.Forms.ListBox();
            this.tabPoeticForm = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel62 = new System.Windows.Forms.TableLayoutPanel();
            this.FilterPoeticForm = new System.Windows.Forms.Button();
            this.TextFilterPoeticForm = new System.Windows.Forms.TextBox();
            this.grpPoeticForm = new System.Windows.Forms.GroupBox();
            this.lblPoeticFormAltName = new System.Windows.Forms.Label();
            this.label296 = new System.Windows.Forms.Label();
            this.lblPoeticFormName = new System.Windows.Forms.Label();
            this.label220 = new System.Windows.Forms.Label();
            this.lstPoeticForm = new System.Windows.Forms.ListBox();
            this.tabMusicalForm = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel63 = new System.Windows.Forms.TableLayoutPanel();
            this.FilterMusicalForm = new System.Windows.Forms.Button();
            this.TextFilterMusicalForm = new System.Windows.Forms.TextBox();
            this.grpMusicalForm = new System.Windows.Forms.GroupBox();
            this.lblMusicalFormAltName = new System.Windows.Forms.Label();
            this.label226 = new System.Windows.Forms.Label();
            this.lblMusicalFormName = new System.Windows.Forms.Label();
            this.label249 = new System.Windows.Forms.Label();
            this.lstMusicalForm = new System.Windows.Forms.ListBox();
            this.tabDanceForm = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel64 = new System.Windows.Forms.TableLayoutPanel();
            this.FilterDanceForm = new System.Windows.Forms.Button();
            this.TextFilterDanceForm = new System.Windows.Forms.TextBox();
            this.grpDanceForm = new System.Windows.Forms.GroupBox();
            this.lblDanceFormAltName = new System.Windows.Forms.Label();
            this.label297 = new System.Windows.Forms.Label();
            this.lblDanceFormName = new System.Windows.Forms.Label();
            this.label299 = new System.Windows.Forms.Label();
            this.lstDanceForm = new System.Windows.Forms.ListBox();
            this.tabSquad = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel56 = new System.Windows.Forms.TableLayoutPanel();
            this.FilterSquad = new System.Windows.Forms.Button();
            this.TextFilterSquad = new System.Windows.Forms.TextBox();
            this.grpSquad = new System.Windows.Forms.GroupBox();
            this.grpSquadMembers = new System.Windows.Forms.GroupBox();
            this.lstSquadMembers = new System.Windows.Forms.ListBox();
            this.label303 = new System.Windows.Forms.Label();
            this.lblSquadAltName = new System.Windows.Forms.Label();
            this.label307 = new System.Windows.Forms.Label();
            this.lblSquadName = new System.Windows.Forms.Label();
            this.label229 = new System.Windows.Forms.Label();
            this.lstSquad = new System.Windows.Forms.ListBox();
            this.tabBuilding = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel52 = new System.Windows.Forms.TableLayoutPanel();
            this.FilterBuilding = new System.Windows.Forms.Button();
            this.TextFilterBuilding = new System.Windows.Forms.TextBox();
            this.grpBuilding = new System.Windows.Forms.GroupBox();
            this.grpBuildingReferences = new System.Windows.Forms.GroupBox();
            this.lstBuildingReferences = new System.Windows.Forms.ListBox();
            this.label206 = new System.Windows.Forms.Label();
            this.lblBuildingCoordsCenter = new System.Windows.Forms.Label();
            this.lblBuildingDir = new System.Windows.Forms.Label();
            this.label287 = new System.Windows.Forms.Label();
            this.label289 = new System.Windows.Forms.Label();
            this.label291 = new System.Windows.Forms.Label();
            this.lblBuildingZoneFlags = new System.Windows.Forms.Label();
            this.lblBuildingSubType = new System.Windows.Forms.Label();
            this.lblBuildingType = new System.Windows.Forms.Label();
            this.label253 = new System.Windows.Forms.Label();
            this.label255 = new System.Windows.Forms.Label();
            this.label259 = new System.Windows.Forms.Label();
            this.label264 = new System.Windows.Forms.Label();
            this.label266 = new System.Windows.Forms.Label();
            this.label270 = new System.Windows.Forms.Label();
            this.lblBuildingMat = new System.Windows.Forms.Label();
            this.label275 = new System.Windows.Forms.Label();
            this.lblBuildingCoords2 = new System.Windows.Forms.Label();
            this.label279 = new System.Windows.Forms.Label();
            this.label281 = new System.Windows.Forms.Label();
            this.lblBuildingCoords1 = new System.Windows.Forms.Label();
            this.label285 = new System.Windows.Forms.Label();
            this.lblBuildingName = new System.Windows.Forms.Label();
            this.label221 = new System.Windows.Forms.Label();
            this.lstBuilding = new System.Windows.Forms.ListBox();
            this.tabConstruction = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel53 = new System.Windows.Forms.TableLayoutPanel();
            this.FilterConstruction = new System.Windows.Forms.Button();
            this.TextFilterConstruction = new System.Windows.Forms.TextBox();
            this.grpConstruction = new System.Windows.Forms.GroupBox();
            this.lblConstructionMat = new System.Windows.Forms.Label();
            this.label304 = new System.Windows.Forms.Label();
            this.lblConstructionItemType = new System.Windows.Forms.Label();
            this.label306 = new System.Windows.Forms.Label();
            this.lblConstructionCoords = new System.Windows.Forms.Label();
            this.label308 = new System.Windows.Forms.Label();
            this.lblConstructionName = new System.Windows.Forms.Label();
            this.label223 = new System.Windows.Forms.Label();
            this.lstConstruction = new System.Windows.Forms.ListBox();
            this.tabItem = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel54 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.trvItemsByQualityType = new System.Windows.Forms.TreeView();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.trvItemsByTypeMaterial = new System.Windows.Forms.TreeView();
            this.FilterItem = new System.Windows.Forms.Button();
            this.TextFilterItem = new System.Windows.Forms.TextBox();
            this.grpItem = new System.Windows.Forms.GroupBox();
            this.grpItemReferences = new System.Windows.Forms.GroupBox();
            this.lstItemReferences = new System.Windows.Forms.ListBox();
            this.lblItemWeight = new System.Windows.Forms.Label();
            this.label208 = new System.Windows.Forms.Label();
            this.lblItemCoords = new System.Windows.Forms.Label();
            this.label298 = new System.Windows.Forms.Label();
            this.label212 = new System.Windows.Forms.Label();
            this.lblItemDyeMat = new System.Windows.Forms.Label();
            this.lblItemStockpileCoords = new System.Windows.Forms.Label();
            this.label335 = new System.Windows.Forms.Label();
            this.lblItemStockpile = new System.Windows.Forms.Label();
            this.label337 = new System.Windows.Forms.Label();
            this.lblItemBoneMat = new System.Windows.Forms.Label();
            this.label332 = new System.Windows.Forms.Label();
            this.label215 = new System.Windows.Forms.Label();
            this.label300 = new System.Windows.Forms.Label();
            this.lblItemShape = new System.Windows.Forms.Label();
            this.label302 = new System.Windows.Forms.Label();
            this.label305 = new System.Windows.Forms.Label();
            this.lblItemHand = new System.Windows.Forms.Label();
            this.label310 = new System.Windows.Forms.Label();
            this.lblItemWear = new System.Windows.Forms.Label();
            this.label313 = new System.Windows.Forms.Label();
            this.lblItemAge = new System.Windows.Forms.Label();
            this.label316 = new System.Windows.Forms.Label();
            this.grpItemImage = new System.Windows.Forms.GroupBox();
            this.label211 = new System.Windows.Forms.Label();
            this.label213 = new System.Windows.Forms.Label();
            this.lblItemImageSubID = new System.Windows.Forms.Label();
            this.label216 = new System.Windows.Forms.Label();
            this.lblItemImageID = new System.Windows.Forms.Label();
            this.label228 = new System.Windows.Forms.Label();
            this.grpItemImprovement = new System.Windows.Forms.GroupBox();
            this.lstItemImprovement = new System.Windows.Forms.ListBox();
            this.grpItemIngredient = new System.Windows.Forms.GroupBox();
            this.lstItemIngredient = new System.Windows.Forms.ListBox();
            this.lblItemSkill = new System.Windows.Forms.Label();
            this.label251 = new System.Windows.Forms.Label();
            this.lblItemSubType = new System.Windows.Forms.Label();
            this.label245 = new System.Windows.Forms.Label();
            this.lblItemType = new System.Windows.Forms.Label();
            this.label222 = new System.Windows.Forms.Label();
            this.lblItemQuality = new System.Windows.Forms.Label();
            this.label256 = new System.Windows.Forms.Label();
            this.label268 = new System.Windows.Forms.Label();
            this.lblItemMat = new System.Windows.Forms.Label();
            this.label277 = new System.Windows.Forms.Label();
            this.lblItemDescription = new System.Windows.Forms.Label();
            this.label293 = new System.Windows.Forms.Label();
            this.lblItemName = new System.Windows.Forms.Label();
            this.label225 = new System.Windows.Forms.Label();
            this.lstItem = new System.Windows.Forms.ListBox();
            this.tabPlant = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel55 = new System.Windows.Forms.TableLayoutPanel();
            this.FilterPlant = new System.Windows.Forms.Button();
            this.TextFilterPlant = new System.Windows.Forms.TextBox();
            this.grpPlant = new System.Windows.Forms.GroupBox();
            this.lblPlantMat = new System.Windows.Forms.Label();
            this.label224 = new System.Windows.Forms.Label();
            this.lblPlantCoords = new System.Windows.Forms.Label();
            this.label283 = new System.Windows.Forms.Label();
            this.lblPlantName = new System.Windows.Forms.Label();
            this.label227 = new System.Windows.Forms.Label();
            this.lstPlant = new System.Windows.Forms.ListBox();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.loadWorldToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportWorldToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showMapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.timelineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.visualizationsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeWorldToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.BacktoolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ForwardtoolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TestButton = new System.Windows.Forms.Button();
            this.lblArtifactWC = new DFWV.Controls.LinkLabel();
            this.lblArtifactLostTime = new DFWV.Controls.LinkLabel();
            this.lblArtifactLostSite = new DFWV.Controls.LinkLabel();
            this.lblArtifactCreatedTime = new DFWV.Controls.LinkLabel();
            this.lblArtifactCreatedSite = new DFWV.Controls.LinkLabel();
            this.lblArtifactCreatedBy = new DFWV.Controls.LinkLabel();
            this.lblCivilizationRace = new DFWV.Controls.LinkLabel();
            this.lblCivilizationEntity = new DFWV.Controls.LinkLabel();
            this.lblEntityWorshippingHF = new DFWV.Controls.LinkLabel();
            this.lblEntityRace = new DFWV.Controls.LinkLabel();
            this.lblEntityCivilization = new DFWV.Controls.LinkLabel();
            this.lblEntityParentCiv = new DFWV.Controls.LinkLabel();
            this.lblEntitySiteTakeoverTime = new DFWV.Controls.LinkLabel();
            this.lblEntitySiteTakeoverNewLeader = new DFWV.Controls.LinkLabel();
            this.lblEntitySiteTakeoverDefenderEntity = new DFWV.Controls.LinkLabel();
            this.lblEntitySiteTakeoverDefenderCiv = new DFWV.Controls.LinkLabel();
            this.lblEntitySiteTakeoverSite = new DFWV.Controls.LinkLabel();
            this.lblEntityCreatedTime = new DFWV.Controls.LinkLabel();
            this.lblEntityCreatedSite = new DFWV.Controls.LinkLabel();
            this.lblEntityPopulationCiv = new DFWV.Controls.LinkLabel();
            this.lblEntityPopulationRace = new DFWV.Controls.LinkLabel();
            this.lblEntityPopulationBattleTime = new DFWV.Controls.LinkLabel();
            this.lblEntityPopulationBattleWar = new DFWV.Controls.LinkLabel();
            this.lblGodHF = new DFWV.Controls.LinkLabel();
            this.lblHistoricalFigureEntityPopulation = new DFWV.Controls.LinkLabel();
            this.lblHistoricalFigureCoords = new DFWV.Controls.LinkLabel();
            this.lblHistoricalFigureLocation = new DFWV.Controls.LinkLabel();
            this.lblHistoricalFigureRace = new DFWV.Controls.LinkLabel();
            this.lblHistoricalFigureUnit = new DFWV.Controls.LinkLabel();
            this.lblHistoricalFigureGod = new DFWV.Controls.LinkLabel();
            this.lblHistoricalFigureLeader = new DFWV.Controls.LinkLabel();
            this.lblHistoricalFigureDeathTime = new DFWV.Controls.LinkLabel();
            this.lblHistoricalFigureDeathLocation = new DFWV.Controls.LinkLabel();
            this.lblHistoricalFigureDeathSlayer = new DFWV.Controls.LinkLabel();
            this.rtbHistoricalFigureSummary = new DFWV.Controls.RichTextBoxEx();
            this.lblBeastAttackParent = new DFWV.Controls.LinkLabel();
            this.lblBeastAttackSite = new DFWV.Controls.LinkLabel();
            this.lblBeastAttackCoords = new DFWV.Controls.LinkLabel();
            this.lblBeastAttackDefender = new DFWV.Controls.LinkLabel();
            this.lblBeastAttackRegion = new DFWV.Controls.LinkLabel();
            this.lblBeastAttackBeast = new DFWV.Controls.LinkLabel();
            this.lblWarDefender = new DFWV.Controls.LinkLabel();
            this.lblWarAggressor = new DFWV.Controls.LinkLabel();
            this.lblBattleWar = new DFWV.Controls.LinkLabel();
            this.lblBattleRegion = new DFWV.Controls.LinkLabel();
            this.lblBattleSite = new DFWV.Controls.LinkLabel();
            this.lblBattleCoord = new DFWV.Controls.LinkLabel();
            this.lblBattleDefendingSquadRace = new DFWV.Controls.LinkLabel();
            this.lblBattleDefendingSquadEntPop = new DFWV.Controls.LinkLabel();
            this.lblBattleDefendingSquadSite = new DFWV.Controls.LinkLabel();
            this.lblBattleAttackingSquadRace = new DFWV.Controls.LinkLabel();
            this.lblBattleAttackingSquadEntPop = new DFWV.Controls.LinkLabel();
            this.lblBattleAttackingSquadSite = new DFWV.Controls.LinkLabel();
            this.lblDuelParent = new DFWV.Controls.LinkLabel();
            this.lblDuelSite = new DFWV.Controls.LinkLabel();
            this.lblDuelCoords = new DFWV.Controls.LinkLabel();
            this.lblDuelAttacker = new DFWV.Controls.LinkLabel();
            this.lblDuelDefender = new DFWV.Controls.LinkLabel();
            this.lblDuelRegion = new DFWV.Controls.LinkLabel();
            this.lblAbductionParent = new DFWV.Controls.LinkLabel();
            this.lblAbductionSite = new DFWV.Controls.LinkLabel();
            this.lblAbductionCoords = new DFWV.Controls.LinkLabel();
            this.lblAbductionAttacker = new DFWV.Controls.LinkLabel();
            this.lblAbductionDefender = new DFWV.Controls.LinkLabel();
            this.lblAbductionRegion = new DFWV.Controls.LinkLabel();
            this.lblSiteConqueredWar = new DFWV.Controls.LinkLabel();
            this.lblSiteConqueredSite = new DFWV.Controls.LinkLabel();
            this.lblSiteConqueredCoords = new DFWV.Controls.LinkLabel();
            this.lblSiteConqueredAttacker = new DFWV.Controls.LinkLabel();
            this.lblSiteConqueredDefender = new DFWV.Controls.LinkLabel();
            this.lblTheftWar = new DFWV.Controls.LinkLabel();
            this.lblTheftSite = new DFWV.Controls.LinkLabel();
            this.lblTheftCoords = new DFWV.Controls.LinkLabel();
            this.lblTheftAttacker = new DFWV.Controls.LinkLabel();
            this.lblTheftDefender = new DFWV.Controls.LinkLabel();
            this.lblInsurrectionOutcome = new DFWV.Controls.LinkLabel();
            this.lblInsurrectionParent = new DFWV.Controls.LinkLabel();
            this.lblInsurrectionSite = new DFWV.Controls.LinkLabel();
            this.lblInsurrectionCoords = new DFWV.Controls.LinkLabel();
            this.lblInsurrectionTargetEnt = new DFWV.Controls.LinkLabel();
            this.lblOccasionCiv = new DFWV.Controls.LinkLabel();
            this.lblPurgeSite = new DFWV.Controls.LinkLabel();
            this.lblLeaderMarried = new DFWV.Controls.LinkLabel();
            this.lblLeaderHF = new DFWV.Controls.LinkLabel();
            this.lblLeaderInheritedFrom = new DFWV.Controls.LinkLabel();
            this.lblLeaderRace = new DFWV.Controls.LinkLabel();
            this.lblLeaderGod = new DFWV.Controls.LinkLabel();
            this.lblLeaderCivilization = new DFWV.Controls.LinkLabel();
            this.lblLeaderSite = new DFWV.Controls.LinkLabel();
            this.lblSiteCurOwner = new DFWV.Controls.LinkLabel();
            this.lblSiteCiv = new DFWV.Controls.LinkLabel();
            this.SiteMapLabel = new DFWV.Controls.LinkLabel();
            this.lblSiteCoord = new DFWV.Controls.LinkLabel();
            this.lblSiteOwner = new DFWV.Controls.LinkLabel();
            this.lblSiteParentCiv = new DFWV.Controls.LinkLabel();
            this.lblSiteCreatedTime = new DFWV.Controls.LinkLabel();
            this.lblSiteCreatedByCiv = new DFWV.Controls.LinkLabel();
            this.lblSiteCreatedBy = new DFWV.Controls.LinkLabel();
            this.lblStructureRazedTime = new DFWV.Controls.LinkLabel();
            this.lblStructureRazedSite = new DFWV.Controls.LinkLabel();
            this.lblStructureRazedCiv = new DFWV.Controls.LinkLabel();
            this.lblStructureCreatedTime = new DFWV.Controls.LinkLabel();
            this.lblStructureCreatedSite = new DFWV.Controls.LinkLabel();
            this.lblStructureCreatedCiv = new DFWV.Controls.LinkLabel();
            this.lblStructureCreatedSiteCiv = new DFWV.Controls.LinkLabel();
            this.lblStructureSite = new DFWV.Controls.LinkLabel();
            this.lblWorldConstructionCoord = new DFWV.Controls.LinkLabel();
            this.lblWorldConstructionType = new DFWV.Controls.LinkLabel();
            this.lblWorldConstructionCreatedTime = new DFWV.Controls.LinkLabel();
            this.lblWorldConstructionCreatedByCiv = new DFWV.Controls.LinkLabel();
            this.lblWorldConstructionCreatedBy = new DFWV.Controls.LinkLabel();
            this.lblWorldConstructionFrom = new DFWV.Controls.LinkLabel();
            this.lblWorldConstructionMaster = new DFWV.Controls.LinkLabel();
            this.lblWorldConstructionTo = new DFWV.Controls.LinkLabel();
            this.lblDynastyLength = new DFWV.Controls.LinkLabel();
            this.lblDynastyFounder = new DFWV.Controls.LinkLabel();
            this.lblDynastyCivilization = new DFWV.Controls.LinkLabel();
            this.lblLandmassCenter = new DFWV.Controls.LinkLabel();
            this.lblLandmassMax = new DFWV.Controls.LinkLabel();
            this.lblLandmassMin = new DFWV.Controls.LinkLabel();
            this.lblMountainCoord = new DFWV.Controls.LinkLabel();
            this.lblRiverParent = new DFWV.Controls.LinkLabel();
            this.lblRiverEndsAt = new DFWV.Controls.LinkLabel();
            this.lblArmyItem = new DFWV.Controls.LinkLabel();
            this.lblUnitOpponent = new DFWV.Controls.LinkLabel();
            this.lblUnitSquad = new DFWV.Controls.LinkLabel();
            this.lblUnitRace = new DFWV.Controls.LinkLabel();
            this.lblUnitHF = new DFWV.Controls.LinkLabel();
            this.lblUnitPop = new DFWV.Controls.LinkLabel();
            this.lblUnitCiv = new DFWV.Controls.LinkLabel();
            this.lblEngravingCreatedEvent = new DFWV.Controls.LinkLabel();
            this.lblEngravingArtist = new DFWV.Controls.LinkLabel();
            this.lblWrittenContentAuthor = new DFWV.Controls.LinkLabel();
            this.lblSquadEntity = new DFWV.Controls.LinkLabel();
            this.lblBuildingClaimed = new DFWV.Controls.LinkLabel();
            this.lblBuildingSquad = new DFWV.Controls.LinkLabel();
            this.lblBuildingCorpse = new DFWV.Controls.LinkLabel();
            this.lblBuildingCorpseHF = new DFWV.Controls.LinkLabel();
            this.lblBuildingOwner = new DFWV.Controls.LinkLabel();
            this.lblBuildingRace = new DFWV.Controls.LinkLabel();
            this.lblItemMasterpieceEvent = new DFWV.Controls.LinkLabel();
            this.lblItemUnit = new DFWV.Controls.LinkLabel();
            this.lblItemHF = new DFWV.Controls.LinkLabel();
            this.lblItemImageSIte = new DFWV.Controls.LinkLabel();
            this.lblItemImageCiv = new DFWV.Controls.LinkLabel();
            this.lblItemMaker = new DFWV.Controls.LinkLabel();
            this.MainTab.SuspendLayout();
            this.tabWorld.SuspendLayout();
            this.grpWorld.SuspendLayout();
            this.tableLayoutPanel38.SuspendLayout();
            this.tabArtifact.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.grpArtifact.SuspendLayout();
            this.tableLayoutPanel18.SuspendLayout();
            this.panel1.SuspendLayout();
            this.grpArtifactLost.SuspendLayout();
            this.grpArtifactCreated.SuspendLayout();
            this.grpArtifactEvents.SuspendLayout();
            this.tabCivilization.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.grpCivilization.SuspendLayout();
            this.tableLayoutPanel19.SuspendLayout();
            this.grpCivilizationWars.SuspendLayout();
            this.panel2.SuspendLayout();
            this.grpCivilizationSites.SuspendLayout();
            this.grpCivilizationLeaders.SuspendLayout();
            this.grpCivilizationGods.SuspendLayout();
            this.tabEntity.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.grpEntity.SuspendLayout();
            this.tableLayoutPanel20.SuspendLayout();
            this.grpEntityPositions.SuspendLayout();
            this.grpEntityRelatedSites.SuspendLayout();
            this.panel3.SuspendLayout();
            this.grpEntitySiteTakeover.SuspendLayout();
            this.grpEntityEvents.SuspendLayout();
            this.grpEntityCreated.SuspendLayout();
            this.grpEntityRelatedFigures.SuspendLayout();
            this.grpEntityRelatedEntities.SuspendLayout();
            this.tabEntityPopulation.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.grpEntityPopulation.SuspendLayout();
            this.tableLayoutPanel21.SuspendLayout();
            this.grpEntityPopluationRaces.SuspendLayout();
            this.panel4.SuspendLayout();
            this.grpEntityPopulationBattles.SuspendLayout();
            this.grpEntityPopulationMembers.SuspendLayout();
            this.tabGod.SuspendLayout();
            this.tableLayoutPanel6.SuspendLayout();
            this.grpGod.SuspendLayout();
            this.tableLayoutPanel22.SuspendLayout();
            this.panel5.SuspendLayout();
            this.grpGodCivilizations.SuspendLayout();
            this.grpGodLeaders.SuspendLayout();
            this.tabHistoricalFigure.SuspendLayout();
            this.tableLayoutPanel7.SuspendLayout();
            this.grpHistoricalFigure.SuspendLayout();
            this.tableLayoutPanel23.SuspendLayout();
            this.grpHistoricalFigureArtifacts.SuspendLayout();
            this.panel6.SuspendLayout();
            this.grpHistoricalFigureEvents.SuspendLayout();
            this.grpHistoricalFigureDescendents.SuspendLayout();
            this.grpHistoricalFigureSkills.SuspendLayout();
            this.grpHistoricalFigureAncestors.SuspendLayout();
            this.grpHistoricalFigureDeath.SuspendLayout();
            this.grpHistoricalFigureSpheres.SuspendLayout();
            this.grpHistoricalFigureKnowledge.SuspendLayout();
            this.grpHistoricalFigurePets.SuspendLayout();
            this.grpHistoricalFigureHFLinks.SuspendLayout();
            this.grpHistoricalFigureEntityLinks.SuspendLayout();
            this.tabHistoricalEra.SuspendLayout();
            this.tableLayoutPanel8.SuspendLayout();
            this.grpHistoricalEra.SuspendLayout();
            this.tabHistoricalEvent.SuspendLayout();
            this.tableLayoutPanel9.SuspendLayout();
            this.tabHistoricalEventCollection.SuspendLayout();
            this.tableLayoutPanel10.SuspendLayout();
            this.grpHistoricalEventCollection.SuspendLayout();
            this.MainTabEventCollectionTypes.SuspendLayout();
            this.tabEventCollectionJourney.SuspendLayout();
            this.tableLayoutPanel24.SuspendLayout();
            this.panel7.SuspendLayout();
            this.grpJourneyEvents.SuspendLayout();
            this.tabEventCollectionBeastAttack.SuspendLayout();
            this.tableLayoutPanel25.SuspendLayout();
            this.panel8.SuspendLayout();
            this.grpBeastAttackEvents.SuspendLayout();
            this.tabEventCollectionWar.SuspendLayout();
            this.tableLayoutPanel26.SuspendLayout();
            this.panel9.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.grpWarEventCols.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.grpWarEvents.SuspendLayout();
            this.tabEventCollectionBattle.SuspendLayout();
            this.tableLayoutPanel27.SuspendLayout();
            this.panel10.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.grpBattleDefendingSquad.SuspendLayout();
            this.grpBattleDefendingHF.SuspendLayout();
            this.grpBattleEvents.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.grpBattleAttackingSquad.SuspendLayout();
            this.grpBattleAttackingHF.SuspendLayout();
            this.grpBattleEventCols.SuspendLayout();
            this.grpBattleNonComHFs.SuspendLayout();
            this.tabEventCollectionDuel.SuspendLayout();
            this.tableLayoutPanel28.SuspendLayout();
            this.panel11.SuspendLayout();
            this.grpDuelEvents.SuspendLayout();
            this.tabEventCollectionAbduction.SuspendLayout();
            this.tableLayoutPanel29.SuspendLayout();
            this.grpAbductionEventCols.SuspendLayout();
            this.panel12.SuspendLayout();
            this.grpAbductionEvents.SuspendLayout();
            this.tabEventCollectionSiteConquered.SuspendLayout();
            this.tableLayoutPanel30.SuspendLayout();
            this.grpSiteConqueredEvents.SuspendLayout();
            this.panel13.SuspendLayout();
            this.tabEventCollectionTheft.SuspendLayout();
            this.tableLayoutPanel31.SuspendLayout();
            this.grpTheftEventCols.SuspendLayout();
            this.panel14.SuspendLayout();
            this.grpTheftEvents.SuspendLayout();
            this.tabEventCollectionInsurrection.SuspendLayout();
            this.tableLayoutPanel39.SuspendLayout();
            this.panel20.SuspendLayout();
            this.grpInsurrectionEvents.SuspendLayout();
            this.tabEventCollectionOccasion.SuspendLayout();
            this.tableLayoutPanel57.SuspendLayout();
            this.grpOccasionEventCols.SuspendLayout();
            this.panel22.SuspendLayout();
            this.grpOccasionEvents.SuspendLayout();
            this.tabEventCollectionCeremony.SuspendLayout();
            this.tableLayoutPanel58.SuspendLayout();
            this.panel23.SuspendLayout();
            this.grpCeremonyEvents.SuspendLayout();
            this.tabEventCollectionProcession.SuspendLayout();
            this.tableLayoutPanel59.SuspendLayout();
            this.panel24.SuspendLayout();
            this.grpProcessionEvents.SuspendLayout();
            this.tabEventCollectionPerformance.SuspendLayout();
            this.tableLayoutPanel60.SuspendLayout();
            this.panel25.SuspendLayout();
            this.grpPerformanceEvents.SuspendLayout();
            this.tabEventCollectionCompetition.SuspendLayout();
            this.tableLayoutPanel61.SuspendLayout();
            this.panel26.SuspendLayout();
            this.grpCompetitionEvents.SuspendLayout();
            this.tabEventCollectionPurge.SuspendLayout();
            this.tableLayoutPanel45.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel27.SuspendLayout();
            this.tabLeader.SuspendLayout();
            this.tableLayoutPanel11.SuspendLayout();
            this.grpLeader.SuspendLayout();
            this.tabParameter.SuspendLayout();
            this.tableLayoutPanel12.SuspendLayout();
            this.grpParameter.SuspendLayout();
            this.tabRace.SuspendLayout();
            this.tableLayoutPanel13.SuspendLayout();
            this.grpRace.SuspendLayout();
            this.tableLayoutPanel32.SuspendLayout();
            this.grpRacePopulation.SuspendLayout();
            this.panel15.SuspendLayout();
            this.grpRaceCastes.SuspendLayout();
            this.grpRaceHistoricalFigures.SuspendLayout();
            this.grpRaceLeaders.SuspendLayout();
            this.grpRaceCivilizations.SuspendLayout();
            this.tabRegion.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.grpRegion.SuspendLayout();
            this.tableLayoutPanel33.SuspendLayout();
            this.grpRegionPopulation.SuspendLayout();
            this.panel16.SuspendLayout();
            this.grpRegionEvents.SuspendLayout();
            this.grpRegionBattles.SuspendLayout();
            this.grpRegionInhabitants.SuspendLayout();
            this.tabSite.SuspendLayout();
            this.tableLayoutPanel14.SuspendLayout();
            this.grpSite.SuspendLayout();
            this.tableLayoutPanel34.SuspendLayout();
            this.grpSiteArtifacts.SuspendLayout();
            this.grpSiteStructures.SuspendLayout();
            this.panel17.SuspendLayout();
            this.grpSiteOutcasts.SuspendLayout();
            this.grpSiteCreated.SuspendLayout();
            this.grpSitePrisoners.SuspendLayout();
            this.grpSiteInhabitants.SuspendLayout();
            this.grpSitePopulation.SuspendLayout();
            this.grpSiteEventCollection.SuspendLayout();
            this.grpSiteEvent.SuspendLayout();
            this.tabStructure.SuspendLayout();
            this.tableLayoutPanel36.SuspendLayout();
            this.grpStructure.SuspendLayout();
            this.tableLayoutPanel37.SuspendLayout();
            this.grpStructureEntombedHF.SuspendLayout();
            this.grpStructureEvents.SuspendLayout();
            this.grpStructureRazed.SuspendLayout();
            this.grpStructureCreated.SuspendLayout();
            this.panel19.SuspendLayout();
            this.tabUndergroundRegion.SuspendLayout();
            this.tableLayoutPanel15.SuspendLayout();
            this.grpUndergroundRegion.SuspendLayout();
            this.tableLayoutPanel40.SuspendLayout();
            this.grpUndergroundRegionPopulation.SuspendLayout();
            this.panel21.SuspendLayout();
            this.tabWorldConstruction.SuspendLayout();
            this.tableLayoutPanel16.SuspendLayout();
            this.grpWorldConstruction.SuspendLayout();
            this.grpWorldConstructionCreated.SuspendLayout();
            this.tabDynasty.SuspendLayout();
            this.tableLayoutPanel17.SuspendLayout();
            this.grpDynasty.SuspendLayout();
            this.tableLayoutPanel35.SuspendLayout();
            this.panel18.SuspendLayout();
            this.grpDynastyMembers.SuspendLayout();
            this.tabLandmass.SuspendLayout();
            this.tableLayoutPanel47.SuspendLayout();
            this.grpLandmass.SuspendLayout();
            this.tabMountain.SuspendLayout();
            this.tableLayoutPanel41.SuspendLayout();
            this.grpMountain.SuspendLayout();
            this.tabRiver.SuspendLayout();
            this.tableLayoutPanel42.SuspendLayout();
            this.grpRiver.SuspendLayout();
            this.grpRiverTributaries.SuspendLayout();
            this.tabArmy.SuspendLayout();
            this.tableLayoutPanel43.SuspendLayout();
            this.grpArmy.SuspendLayout();
            this.tabUnit.SuspendLayout();
            this.tableLayoutPanel44.SuspendLayout();
            this.grpUnit.SuspendLayout();
            this.grpUnitReferences.SuspendLayout();
            this.grpUnitOwnedBuildings.SuspendLayout();
            this.grpUnitInventory.SuspendLayout();
            this.grpUnitItems.SuspendLayout();
            this.grpUnitHealth.SuspendLayout();
            this.grpUnitRelations.SuspendLayout();
            this.grpUnitFlags.SuspendLayout();
            this.grpUnitLabors.SuspendLayout();
            this.tabEngraving.SuspendLayout();
            this.tableLayoutPanel46.SuspendLayout();
            this.grpEngraving.SuspendLayout();
            this.tabReport.SuspendLayout();
            this.tableLayoutPanel50.SuspendLayout();
            this.grpReport.SuspendLayout();
            this.tabWrittenContent.SuspendLayout();
            this.tableLayoutPanel51.SuspendLayout();
            this.grpWrittenContent.SuspendLayout();
            this.grpWrittenContentReferences.SuspendLayout();
            this.tabPoeticForm.SuspendLayout();
            this.tableLayoutPanel62.SuspendLayout();
            this.grpPoeticForm.SuspendLayout();
            this.tabMusicalForm.SuspendLayout();
            this.tableLayoutPanel63.SuspendLayout();
            this.grpMusicalForm.SuspendLayout();
            this.tabDanceForm.SuspendLayout();
            this.tableLayoutPanel64.SuspendLayout();
            this.grpDanceForm.SuspendLayout();
            this.tabSquad.SuspendLayout();
            this.tableLayoutPanel56.SuspendLayout();
            this.grpSquad.SuspendLayout();
            this.grpSquadMembers.SuspendLayout();
            this.tabBuilding.SuspendLayout();
            this.tableLayoutPanel52.SuspendLayout();
            this.grpBuilding.SuspendLayout();
            this.grpBuildingReferences.SuspendLayout();
            this.tabConstruction.SuspendLayout();
            this.tableLayoutPanel53.SuspendLayout();
            this.grpConstruction.SuspendLayout();
            this.tabItem.SuspendLayout();
            this.tableLayoutPanel54.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.grpItem.SuspendLayout();
            this.grpItemReferences.SuspendLayout();
            this.grpItemImage.SuspendLayout();
            this.grpItemImprovement.SuspendLayout();
            this.grpItemIngredient.SuspendLayout();
            this.tabPlant.SuspendLayout();
            this.tableLayoutPanel55.SuspendLayout();
            this.grpPlant.SuspendLayout();
            this.menuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainTab
            // 
            this.MainTab.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MainTab.Controls.Add(this.tabWorld);
            this.MainTab.Controls.Add(this.tabArtifact);
            this.MainTab.Controls.Add(this.tabCivilization);
            this.MainTab.Controls.Add(this.tabEntity);
            this.MainTab.Controls.Add(this.tabEntityPopulation);
            this.MainTab.Controls.Add(this.tabGod);
            this.MainTab.Controls.Add(this.tabHistoricalFigure);
            this.MainTab.Controls.Add(this.tabHistoricalEra);
            this.MainTab.Controls.Add(this.tabHistoricalEvent);
            this.MainTab.Controls.Add(this.tabHistoricalEventCollection);
            this.MainTab.Controls.Add(this.tabLeader);
            this.MainTab.Controls.Add(this.tabParameter);
            this.MainTab.Controls.Add(this.tabRace);
            this.MainTab.Controls.Add(this.tabRegion);
            this.MainTab.Controls.Add(this.tabSite);
            this.MainTab.Controls.Add(this.tabStructure);
            this.MainTab.Controls.Add(this.tabUndergroundRegion);
            this.MainTab.Controls.Add(this.tabWorldConstruction);
            this.MainTab.Controls.Add(this.tabDynasty);
            this.MainTab.Controls.Add(this.tabLandmass);
            this.MainTab.Controls.Add(this.tabMountain);
            this.MainTab.Controls.Add(this.tabRiver);
            this.MainTab.Controls.Add(this.tabArmy);
            this.MainTab.Controls.Add(this.tabUnit);
            this.MainTab.Controls.Add(this.tabEngraving);
            this.MainTab.Controls.Add(this.tabReport);
            this.MainTab.Controls.Add(this.tabWrittenContent);
            this.MainTab.Controls.Add(this.tabPoeticForm);
            this.MainTab.Controls.Add(this.tabMusicalForm);
            this.MainTab.Controls.Add(this.tabDanceForm);
            this.MainTab.Controls.Add(this.tabSquad);
            this.MainTab.Controls.Add(this.tabBuilding);
            this.MainTab.Controls.Add(this.tabConstruction);
            this.MainTab.Controls.Add(this.tabItem);
            this.MainTab.Controls.Add(this.tabPlant);
            this.MainTab.Location = new System.Drawing.Point(13, 33);
            this.MainTab.Margin = new System.Windows.Forms.Padding(2);
            this.MainTab.Multiline = true;
            this.MainTab.Name = "MainTab";
            this.MainTab.SelectedIndex = 0;
            this.MainTab.Size = new System.Drawing.Size(1881, 917);
            this.MainTab.TabIndex = 0;
            // 
            // tabWorld
            // 
            this.tabWorld.Controls.Add(this.grpWorld);
            this.tabWorld.Location = new System.Drawing.Point(4, 40);
            this.tabWorld.Name = "tabWorld";
            this.tabWorld.Size = new System.Drawing.Size(1873, 873);
            this.tabWorld.TabIndex = 16;
            this.tabWorld.Text = "World";
            this.tabWorld.UseVisualStyleBackColor = true;
            // 
            // grpWorld
            // 
            this.grpWorld.Controls.Add(this.tableLayoutPanel38);
            this.grpWorld.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpWorld.Location = new System.Drawing.Point(0, 0);
            this.grpWorld.Name = "grpWorld";
            this.grpWorld.Size = new System.Drawing.Size(1873, 873);
            this.grpWorld.TabIndex = 3;
            this.grpWorld.TabStop = false;
            // 
            // tableLayoutPanel38
            // 
            this.tableLayoutPanel38.ColumnCount = 2;
            this.tableLayoutPanel38.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 66F));
            this.tableLayoutPanel38.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 34F));
            this.tableLayoutPanel38.Controls.Add(this.WorldSummaryTree, 0, 0);
            this.tableLayoutPanel38.Controls.Add(this.IssuesBox, 1, 1);
            this.tableLayoutPanel38.Controls.Add(this.StatusBox, 1, 0);
            this.tableLayoutPanel38.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel38.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel38.Name = "tableLayoutPanel38";
            this.tableLayoutPanel38.RowCount = 2;
            this.tableLayoutPanel38.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 75F));
            this.tableLayoutPanel38.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel38.Size = new System.Drawing.Size(1867, 854);
            this.tableLayoutPanel38.TabIndex = 5;
            // 
            // WorldSummaryTree
            // 
            this.WorldSummaryTree.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.WorldSummaryTree.Location = new System.Drawing.Point(3, 3);
            this.WorldSummaryTree.Name = "WorldSummaryTree";
            this.tableLayoutPanel38.SetRowSpan(this.WorldSummaryTree, 2);
            this.WorldSummaryTree.Size = new System.Drawing.Size(1226, 848);
            this.WorldSummaryTree.TabIndex = 2;
            this.WorldSummaryTree.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.WorldSummaryTree_NodeMouseDoubleClick);
            this.WorldSummaryTree.MouseLeave += new System.EventHandler(this.WorldSummaryTree_MouseLeave);
            this.WorldSummaryTree.MouseMove += new System.Windows.Forms.MouseEventHandler(this.WorldSummaryTree_MouseMove);
            // 
            // IssuesBox
            // 
            this.IssuesBox.BackColor = System.Drawing.Color.LightGray;
            this.IssuesBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.IssuesBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.IssuesBox.Location = new System.Drawing.Point(1235, 643);
            this.IssuesBox.Name = "IssuesBox";
            this.IssuesBox.ReadOnly = true;
            this.IssuesBox.Size = new System.Drawing.Size(629, 208);
            this.IssuesBox.TabIndex = 3;
            this.IssuesBox.Text = "";
            // 
            // StatusBox
            // 
            this.StatusBox.BackColor = System.Drawing.Color.LightGray;
            this.StatusBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.StatusBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.StatusBox.ForeColor = System.Drawing.Color.Green;
            this.StatusBox.Location = new System.Drawing.Point(1235, 3);
            this.StatusBox.Multiline = true;
            this.StatusBox.Name = "StatusBox";
            this.StatusBox.ReadOnly = true;
            this.StatusBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.StatusBox.Size = new System.Drawing.Size(629, 634);
            this.StatusBox.TabIndex = 4;
            // 
            // tabArtifact
            // 
            this.tabArtifact.Controls.Add(this.tableLayoutPanel2);
            this.tabArtifact.Location = new System.Drawing.Point(4, 40);
            this.tabArtifact.Margin = new System.Windows.Forms.Padding(2);
            this.tabArtifact.Name = "tabArtifact";
            this.tabArtifact.Padding = new System.Windows.Forms.Padding(2);
            this.tabArtifact.Size = new System.Drawing.Size(1873, 873);
            this.tabArtifact.TabIndex = 1;
            this.tabArtifact.Text = "Artifact";
            this.tabArtifact.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 168F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.grpArtifact, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.FilterArtifact, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.lstArtifact, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.TextFilterArtifact, 0, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(2, 2);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(2);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 3;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1869, 869);
            this.tableLayoutPanel2.TabIndex = 8;
            // 
            // grpArtifact
            // 
            this.grpArtifact.Controls.Add(this.tableLayoutPanel18);
            this.grpArtifact.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpArtifact.Location = new System.Drawing.Point(171, 3);
            this.grpArtifact.Name = "grpArtifact";
            this.tableLayoutPanel2.SetRowSpan(this.grpArtifact, 3);
            this.grpArtifact.Size = new System.Drawing.Size(1695, 863);
            this.grpArtifact.TabIndex = 1;
            this.grpArtifact.TabStop = false;
            this.grpArtifact.Visible = false;
            // 
            // tableLayoutPanel18
            // 
            this.tableLayoutPanel18.ColumnCount = 3;
            this.tableLayoutPanel18.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 400F));
            this.tableLayoutPanel18.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 400F));
            this.tableLayoutPanel18.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel18.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel18.Controls.Add(this.grpArtifactLost, 0, 2);
            this.tableLayoutPanel18.Controls.Add(this.grpArtifactCreated, 0, 1);
            this.tableLayoutPanel18.Controls.Add(this.grpArtifactEvents, 1, 0);
            this.tableLayoutPanel18.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel18.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel18.Name = "tableLayoutPanel18";
            this.tableLayoutPanel18.RowCount = 4;
            this.tableLayoutPanel18.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 131F));
            this.tableLayoutPanel18.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel18.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel18.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel18.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel18.Size = new System.Drawing.Size(1689, 844);
            this.tableLayoutPanel18.TabIndex = 8;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblArtifactWC);
            this.panel1.Controls.Add(this.lblArtifactWCLabel);
            this.panel1.Controls.Add(this.label191);
            this.panel1.Controls.Add(this.lblArtifactValue);
            this.panel1.Controls.Add(this.label181);
            this.panel1.Controls.Add(this.lblArtifactDescription);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.lblArtifactName);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.lblArtifactItem);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(394, 125);
            this.panel1.TabIndex = 8;
            // 
            // lblArtifactWCLabel
            // 
            this.lblArtifactWCLabel.AutoSize = true;
            this.lblArtifactWCLabel.Location = new System.Drawing.Point(3, 93);
            this.lblArtifactWCLabel.Name = "lblArtifactWCLabel";
            this.lblArtifactWCLabel.Size = new System.Drawing.Size(48, 13);
            this.lblArtifactWCLabel.TabIndex = 9;
            this.lblArtifactWCLabel.Text = "Writings:";
            // 
            // label191
            // 
            this.label191.AutoSize = true;
            this.label191.Location = new System.Drawing.Point(3, 70);
            this.label191.Name = "label191";
            this.label191.Size = new System.Drawing.Size(37, 13);
            this.label191.TabIndex = 7;
            this.label191.Text = "Value:";
            // 
            // lblArtifactValue
            // 
            this.lblArtifactValue.AutoSize = true;
            this.lblArtifactValue.Location = new System.Drawing.Point(57, 70);
            this.lblArtifactValue.Name = "lblArtifactValue";
            this.lblArtifactValue.Size = new System.Drawing.Size(35, 13);
            this.lblArtifactValue.TabIndex = 8;
            this.lblArtifactValue.Text = "label2";
            // 
            // label181
            // 
            this.label181.AutoSize = true;
            this.label181.Location = new System.Drawing.Point(3, 47);
            this.label181.Name = "label181";
            this.label181.Size = new System.Drawing.Size(30, 13);
            this.label181.TabIndex = 5;
            this.label181.Text = "Item:";
            // 
            // lblArtifactDescription
            // 
            this.lblArtifactDescription.AutoSize = true;
            this.lblArtifactDescription.Location = new System.Drawing.Point(57, 47);
            this.lblArtifactDescription.Name = "lblArtifactDescription";
            this.lblArtifactDescription.Size = new System.Drawing.Size(35, 13);
            this.lblArtifactDescription.TabIndex = 6;
            this.lblArtifactDescription.Text = "label2";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Name: ";
            // 
            // lblArtifactName
            // 
            this.lblArtifactName.AutoSize = true;
            this.lblArtifactName.Location = new System.Drawing.Point(57, 3);
            this.lblArtifactName.Name = "lblArtifactName";
            this.lblArtifactName.Size = new System.Drawing.Size(35, 13);
            this.lblArtifactName.TabIndex = 1;
            this.lblArtifactName.Text = "label2";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 25);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(30, 13);
            this.label6.TabIndex = 3;
            this.label6.Text = "Item:";
            // 
            // lblArtifactItem
            // 
            this.lblArtifactItem.AutoSize = true;
            this.lblArtifactItem.Location = new System.Drawing.Point(57, 25);
            this.lblArtifactItem.Name = "lblArtifactItem";
            this.lblArtifactItem.Size = new System.Drawing.Size(35, 13);
            this.lblArtifactItem.TabIndex = 4;
            this.lblArtifactItem.Text = "label2";
            // 
            // grpArtifactLost
            // 
            this.grpArtifactLost.Controls.Add(this.lblArtifactLostTime);
            this.grpArtifactLost.Controls.Add(this.lblArtifactLostSite);
            this.grpArtifactLost.Controls.Add(this.label12);
            this.grpArtifactLost.Controls.Add(this.label13);
            this.grpArtifactLost.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpArtifactLost.Location = new System.Drawing.Point(3, 234);
            this.grpArtifactLost.Name = "grpArtifactLost";
            this.grpArtifactLost.Size = new System.Drawing.Size(394, 94);
            this.grpArtifactLost.TabIndex = 6;
            this.grpArtifactLost.TabStop = false;
            this.grpArtifactLost.Text = "Lost";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(6, 37);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(24, 13);
            this.label12.TabIndex = 10;
            this.label12.Text = "On:";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(6, 16);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(20, 13);
            this.label13.TabIndex = 9;
            this.label13.Text = "At:";
            // 
            // grpArtifactCreated
            // 
            this.grpArtifactCreated.Controls.Add(this.lblArtifactCreatedTime);
            this.grpArtifactCreated.Controls.Add(this.lblArtifactCreatedSite);
            this.grpArtifactCreated.Controls.Add(this.lblArtifactCreatedBy);
            this.grpArtifactCreated.Controls.Add(this.label5);
            this.grpArtifactCreated.Controls.Add(this.label4);
            this.grpArtifactCreated.Controls.Add(this.label2);
            this.grpArtifactCreated.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpArtifactCreated.Location = new System.Drawing.Point(3, 134);
            this.grpArtifactCreated.Name = "grpArtifactCreated";
            this.grpArtifactCreated.Size = new System.Drawing.Size(394, 94);
            this.grpArtifactCreated.TabIndex = 2;
            this.grpArtifactCreated.TabStop = false;
            this.grpArtifactCreated.Text = "Created";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 64);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(24, 13);
            this.label5.TabIndex = 3;
            this.label5.Text = "On:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 43);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(20, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "At:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(25, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "By: ";
            // 
            // grpArtifactEvents
            // 
            this.grpArtifactEvents.Controls.Add(this.lstArtifactEvents);
            this.grpArtifactEvents.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpArtifactEvents.Location = new System.Drawing.Point(403, 3);
            this.grpArtifactEvents.Name = "grpArtifactEvents";
            this.tableLayoutPanel18.SetRowSpan(this.grpArtifactEvents, 4);
            this.grpArtifactEvents.Size = new System.Drawing.Size(394, 838);
            this.grpArtifactEvents.TabIndex = 5;
            this.grpArtifactEvents.TabStop = false;
            this.grpArtifactEvents.Text = "Events";
            // 
            // lstArtifactEvents
            // 
            this.lstArtifactEvents.Dock = System.Windows.Forms.DockStyle.Top;
            this.lstArtifactEvents.FormattingEnabled = true;
            this.lstArtifactEvents.Location = new System.Drawing.Point(3, 16);
            this.lstArtifactEvents.Name = "lstArtifactEvents";
            this.lstArtifactEvents.Size = new System.Drawing.Size(388, 420);
            this.lstArtifactEvents.TabIndex = 0;
            this.lstArtifactEvents.SelectedIndexChanged += new System.EventHandler(this.EventCollection_EventsListClick);
            // 
            // FilterArtifact
            // 
            this.FilterArtifact.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FilterArtifact.Location = new System.Drawing.Point(3, 844);
            this.FilterArtifact.Name = "FilterArtifact";
            this.FilterArtifact.Size = new System.Drawing.Size(162, 22);
            this.FilterArtifact.TabIndex = 2;
            this.FilterArtifact.Tag = "";
            this.FilterArtifact.Text = "Filter";
            this.FilterArtifact.UseVisualStyleBackColor = true;
            this.FilterArtifact.Click += new System.EventHandler(this.FilterButton_Click);
            // 
            // lstArtifact
            // 
            this.lstArtifact.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstArtifact.FormattingEnabled = true;
            this.lstArtifact.Location = new System.Drawing.Point(3, 3);
            this.lstArtifact.Name = "lstArtifact";
            this.lstArtifact.Size = new System.Drawing.Size(162, 811);
            this.lstArtifact.TabIndex = 0;
            // 
            // TextFilterArtifact
            // 
            this.TextFilterArtifact.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TextFilterArtifact.Location = new System.Drawing.Point(3, 820);
            this.TextFilterArtifact.Name = "TextFilterArtifact";
            this.TextFilterArtifact.Size = new System.Drawing.Size(162, 20);
            this.TextFilterArtifact.TabIndex = 5;
            this.TextFilterArtifact.TextChanged += new System.EventHandler(this.TextFilter_Changed);
            // 
            // tabCivilization
            // 
            this.tabCivilization.Controls.Add(this.tableLayoutPanel3);
            this.tabCivilization.Location = new System.Drawing.Point(4, 40);
            this.tabCivilization.Name = "tabCivilization";
            this.tabCivilization.Size = new System.Drawing.Size(1873, 873);
            this.tabCivilization.TabIndex = 2;
            this.tabCivilization.Text = "Civilization";
            this.tabCivilization.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 171F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Controls.Add(this.grpCivilization, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.lstCivilization, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.FilterCivilization, 0, 2);
            this.tableLayoutPanel3.Controls.Add(this.TextFilterCivilization, 0, 1);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(2);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 3;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(1873, 891);
            this.tableLayoutPanel3.TabIndex = 8;
            // 
            // grpCivilization
            // 
            this.grpCivilization.Controls.Add(this.tableLayoutPanel19);
            this.grpCivilization.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpCivilization.Location = new System.Drawing.Point(174, 3);
            this.grpCivilization.Name = "grpCivilization";
            this.tableLayoutPanel3.SetRowSpan(this.grpCivilization, 3);
            this.grpCivilization.Size = new System.Drawing.Size(1696, 885);
            this.grpCivilization.TabIndex = 3;
            this.grpCivilization.TabStop = false;
            this.grpCivilization.Visible = false;
            // 
            // tableLayoutPanel19
            // 
            this.tableLayoutPanel19.ColumnCount = 2;
            this.tableLayoutPanel19.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33332F));
            this.tableLayoutPanel19.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.tableLayoutPanel19.Controls.Add(this.grpCivilizationWars, 0, 2);
            this.tableLayoutPanel19.Controls.Add(this.panel2, 0, 0);
            this.tableLayoutPanel19.Controls.Add(this.grpCivilizationSites, 0, 1);
            this.tableLayoutPanel19.Controls.Add(this.grpCivilizationLeaders, 1, 1);
            this.tableLayoutPanel19.Controls.Add(this.grpCivilizationGods, 1, 2);
            this.tableLayoutPanel19.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel19.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel19.Name = "tableLayoutPanel19";
            this.tableLayoutPanel19.RowCount = 3;
            this.tableLayoutPanel19.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 96F));
            this.tableLayoutPanel19.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel19.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel19.Size = new System.Drawing.Size(1690, 866);
            this.tableLayoutPanel19.TabIndex = 16;
            // 
            // grpCivilizationWars
            // 
            this.grpCivilizationWars.Controls.Add(this.lstCivilizationWars);
            this.grpCivilizationWars.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpCivilizationWars.Location = new System.Drawing.Point(3, 484);
            this.grpCivilizationWars.Name = "grpCivilizationWars";
            this.grpCivilizationWars.Size = new System.Drawing.Size(838, 379);
            this.grpCivilizationWars.TabIndex = 18;
            this.grpCivilizationWars.TabStop = false;
            this.grpCivilizationWars.Text = "Wars";
            // 
            // lstCivilizationWars
            // 
            this.lstCivilizationWars.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstCivilizationWars.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.lstCivilizationWars.FormattingEnabled = true;
            this.lstCivilizationWars.ItemHeight = 30;
            this.lstCivilizationWars.Location = new System.Drawing.Point(3, 16);
            this.lstCivilizationWars.Name = "lstCivilizationWars";
            this.lstCivilizationWars.Size = new System.Drawing.Size(832, 360);
            this.lstCivilizationWars.TabIndex = 1;
            this.lstCivilizationWars.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.lstCivilizationWars_DrawItem);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label17);
            this.panel2.Controls.Add(this.lblCivilizationName);
            this.panel2.Controls.Add(this.label15);
            this.panel2.Controls.Add(this.lblCivilizationRace);
            this.panel2.Controls.Add(this.lblCivilizationFull);
            this.panel2.Controls.Add(this.label14);
            this.panel2.Controls.Add(this.label16);
            this.panel2.Controls.Add(this.lblCivilizationEntity);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(838, 90);
            this.panel2.TabIndex = 17;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(3, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(41, 13);
            this.label17.TabIndex = 5;
            this.label17.Text = "Name: ";
            // 
            // lblCivilizationName
            // 
            this.lblCivilizationName.AutoSize = true;
            this.lblCivilizationName.Location = new System.Drawing.Point(55, 0);
            this.lblCivilizationName.Name = "lblCivilizationName";
            this.lblCivilizationName.Size = new System.Drawing.Size(35, 13);
            this.lblCivilizationName.TabIndex = 6;
            this.lblCivilizationName.Text = "label2";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(3, 22);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(55, 13);
            this.label15.TabIndex = 7;
            this.label15.Text = "Intelligent:";
            // 
            // lblCivilizationFull
            // 
            this.lblCivilizationFull.AutoSize = true;
            this.lblCivilizationFull.Location = new System.Drawing.Point(55, 22);
            this.lblCivilizationFull.Name = "lblCivilizationFull";
            this.lblCivilizationFull.Size = new System.Drawing.Size(35, 13);
            this.lblCivilizationFull.TabIndex = 8;
            this.lblCivilizationFull.Text = "label2";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(3, 44);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(36, 13);
            this.label14.TabIndex = 13;
            this.label14.Text = "Race:";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(3, 66);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(47, 13);
            this.label16.TabIndex = 9;
            this.label16.Text = "Is Entity:";
            // 
            // grpCivilizationSites
            // 
            this.grpCivilizationSites.Controls.Add(this.lstCivilizationSites);
            this.grpCivilizationSites.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpCivilizationSites.Location = new System.Drawing.Point(3, 99);
            this.grpCivilizationSites.Name = "grpCivilizationSites";
            this.grpCivilizationSites.Size = new System.Drawing.Size(838, 379);
            this.grpCivilizationSites.TabIndex = 15;
            this.grpCivilizationSites.TabStop = false;
            this.grpCivilizationSites.Text = "Sites";
            // 
            // lstCivilizationSites
            // 
            this.lstCivilizationSites.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstCivilizationSites.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.lstCivilizationSites.FormattingEnabled = true;
            this.lstCivilizationSites.Location = new System.Drawing.Point(3, 16);
            this.lstCivilizationSites.Name = "lstCivilizationSites";
            this.lstCivilizationSites.Size = new System.Drawing.Size(832, 360);
            this.lstCivilizationSites.TabIndex = 0;
            this.lstCivilizationSites.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.lstCivilizationSites_DrawItem);
            // 
            // grpCivilizationLeaders
            // 
            this.grpCivilizationLeaders.Controls.Add(this.lstCivilizationLeaders);
            this.grpCivilizationLeaders.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpCivilizationLeaders.Location = new System.Drawing.Point(847, 99);
            this.grpCivilizationLeaders.Name = "grpCivilizationLeaders";
            this.grpCivilizationLeaders.Size = new System.Drawing.Size(840, 379);
            this.grpCivilizationLeaders.TabIndex = 11;
            this.grpCivilizationLeaders.TabStop = false;
            this.grpCivilizationLeaders.Text = "Leaders";
            // 
            // lstCivilizationLeaders
            // 
            this.lstCivilizationLeaders.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstCivilizationLeaders.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.lstCivilizationLeaders.FormattingEnabled = true;
            this.lstCivilizationLeaders.Location = new System.Drawing.Point(3, 16);
            this.lstCivilizationLeaders.Name = "lstCivilizationLeaders";
            this.lstCivilizationLeaders.Size = new System.Drawing.Size(834, 360);
            this.lstCivilizationLeaders.TabIndex = 0;
            this.lstCivilizationLeaders.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.lstCivilizationLeaders_DrawItem);
            // 
            // grpCivilizationGods
            // 
            this.grpCivilizationGods.Controls.Add(this.lstCivilizationGods);
            this.grpCivilizationGods.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpCivilizationGods.Location = new System.Drawing.Point(847, 484);
            this.grpCivilizationGods.Name = "grpCivilizationGods";
            this.grpCivilizationGods.Size = new System.Drawing.Size(840, 379);
            this.grpCivilizationGods.TabIndex = 12;
            this.grpCivilizationGods.TabStop = false;
            this.grpCivilizationGods.Text = "Gods";
            // 
            // lstCivilizationGods
            // 
            this.lstCivilizationGods.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstCivilizationGods.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.lstCivilizationGods.FormattingEnabled = true;
            this.lstCivilizationGods.Location = new System.Drawing.Point(3, 16);
            this.lstCivilizationGods.Name = "lstCivilizationGods";
            this.lstCivilizationGods.Size = new System.Drawing.Size(834, 360);
            this.lstCivilizationGods.TabIndex = 1;
            this.lstCivilizationGods.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.lstCivilizationGods_DrawItem);
            // 
            // lstCivilization
            // 
            this.lstCivilization.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstCivilization.FormattingEnabled = true;
            this.lstCivilization.Location = new System.Drawing.Point(3, 3);
            this.lstCivilization.Name = "lstCivilization";
            this.lstCivilization.Size = new System.Drawing.Size(165, 833);
            this.lstCivilization.TabIndex = 2;
            // 
            // FilterCivilization
            // 
            this.FilterCivilization.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FilterCivilization.Location = new System.Drawing.Point(3, 866);
            this.FilterCivilization.Name = "FilterCivilization";
            this.FilterCivilization.Size = new System.Drawing.Size(165, 22);
            this.FilterCivilization.TabIndex = 4;
            this.FilterCivilization.Tag = "";
            this.FilterCivilization.Text = "Filter";
            this.FilterCivilization.UseVisualStyleBackColor = true;
            this.FilterCivilization.Click += new System.EventHandler(this.FilterButton_Click);
            // 
            // TextFilterCivilization
            // 
            this.TextFilterCivilization.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TextFilterCivilization.Location = new System.Drawing.Point(3, 842);
            this.TextFilterCivilization.Name = "TextFilterCivilization";
            this.TextFilterCivilization.Size = new System.Drawing.Size(165, 20);
            this.TextFilterCivilization.TabIndex = 5;
            this.TextFilterCivilization.TextChanged += new System.EventHandler(this.TextFilter_Changed);
            // 
            // tabEntity
            // 
            this.tabEntity.Controls.Add(this.tableLayoutPanel5);
            this.tabEntity.Location = new System.Drawing.Point(4, 40);
            this.tabEntity.Name = "tabEntity";
            this.tabEntity.Size = new System.Drawing.Size(1873, 873);
            this.tabEntity.TabIndex = 3;
            this.tabEntity.Text = "Entity";
            this.tabEntity.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 2;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 172F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.Controls.Add(this.FilterEntity, 0, 2);
            this.tableLayoutPanel5.Controls.Add(this.lstEntity, 0, 0);
            this.tableLayoutPanel5.Controls.Add(this.TextFilterEntity, 0, 1);
            this.tableLayoutPanel5.Controls.Add(this.grpEntity, 1, 0);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel5.Margin = new System.Windows.Forms.Padding(2);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 3;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(1873, 891);
            this.tableLayoutPanel5.TabIndex = 8;
            // 
            // FilterEntity
            // 
            this.FilterEntity.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FilterEntity.Location = new System.Drawing.Point(3, 866);
            this.FilterEntity.Name = "FilterEntity";
            this.FilterEntity.Size = new System.Drawing.Size(166, 22);
            this.FilterEntity.TabIndex = 5;
            this.FilterEntity.Tag = "";
            this.FilterEntity.Text = "Filter";
            this.FilterEntity.UseVisualStyleBackColor = true;
            this.FilterEntity.Click += new System.EventHandler(this.FilterButton_Click);
            // 
            // lstEntity
            // 
            this.lstEntity.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstEntity.FormattingEnabled = true;
            this.lstEntity.Location = new System.Drawing.Point(3, 3);
            this.lstEntity.Name = "lstEntity";
            this.lstEntity.Size = new System.Drawing.Size(166, 833);
            this.lstEntity.TabIndex = 2;
            // 
            // TextFilterEntity
            // 
            this.TextFilterEntity.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TextFilterEntity.Location = new System.Drawing.Point(3, 842);
            this.TextFilterEntity.Name = "TextFilterEntity";
            this.TextFilterEntity.Size = new System.Drawing.Size(166, 20);
            this.TextFilterEntity.TabIndex = 5;
            this.TextFilterEntity.TextChanged += new System.EventHandler(this.TextFilter_Changed);
            // 
            // grpEntity
            // 
            this.grpEntity.Controls.Add(this.tableLayoutPanel20);
            this.grpEntity.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpEntity.Location = new System.Drawing.Point(175, 3);
            this.grpEntity.Name = "grpEntity";
            this.tableLayoutPanel5.SetRowSpan(this.grpEntity, 3);
            this.grpEntity.Size = new System.Drawing.Size(1695, 885);
            this.grpEntity.TabIndex = 3;
            this.grpEntity.TabStop = false;
            this.grpEntity.Visible = false;
            // 
            // tableLayoutPanel20
            // 
            this.tableLayoutPanel20.ColumnCount = 3;
            this.tableLayoutPanel20.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33332F));
            this.tableLayoutPanel20.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.tableLayoutPanel20.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.tableLayoutPanel20.Controls.Add(this.grpEntityPositions, 0, 3);
            this.tableLayoutPanel20.Controls.Add(this.grpEntityRelatedSites, 1, 3);
            this.tableLayoutPanel20.Controls.Add(this.panel3, 0, 0);
            this.tableLayoutPanel20.Controls.Add(this.grpEntitySiteTakeover, 0, 2);
            this.tableLayoutPanel20.Controls.Add(this.grpEntityEvents, 2, 0);
            this.tableLayoutPanel20.Controls.Add(this.grpEntityCreated, 0, 1);
            this.tableLayoutPanel20.Controls.Add(this.grpEntityRelatedFigures, 1, 0);
            this.tableLayoutPanel20.Controls.Add(this.grpEntityRelatedEntities, 1, 2);
            this.tableLayoutPanel20.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel20.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel20.Name = "tableLayoutPanel20";
            this.tableLayoutPanel20.RowCount = 4;
            this.tableLayoutPanel20.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 156F));
            this.tableLayoutPanel20.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.71215F));
            this.tableLayoutPanel20.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 42.64392F));
            this.tableLayoutPanel20.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 42.64392F));
            this.tableLayoutPanel20.Size = new System.Drawing.Size(1689, 866);
            this.tableLayoutPanel20.TabIndex = 69;
            // 
            // grpEntityPositions
            // 
            this.grpEntityPositions.Controls.Add(this.lstEntityPositions);
            this.grpEntityPositions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpEntityPositions.Location = new System.Drawing.Point(3, 565);
            this.grpEntityPositions.Name = "grpEntityPositions";
            this.grpEntityPositions.Size = new System.Drawing.Size(556, 298);
            this.grpEntityPositions.TabIndex = 72;
            this.grpEntityPositions.TabStop = false;
            this.grpEntityPositions.Text = "Positions";
            // 
            // lstEntityPositions
            // 
            this.lstEntityPositions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstEntityPositions.FormattingEnabled = true;
            this.lstEntityPositions.Location = new System.Drawing.Point(3, 16);
            this.lstEntityPositions.Name = "lstEntityPositions";
            this.lstEntityPositions.Size = new System.Drawing.Size(550, 279);
            this.lstEntityPositions.TabIndex = 1;
            // 
            // grpEntityRelatedSites
            // 
            this.grpEntityRelatedSites.Controls.Add(this.trvEntityRelatedSites);
            this.grpEntityRelatedSites.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpEntityRelatedSites.Location = new System.Drawing.Point(565, 565);
            this.grpEntityRelatedSites.Name = "grpEntityRelatedSites";
            this.grpEntityRelatedSites.Size = new System.Drawing.Size(557, 298);
            this.grpEntityRelatedSites.TabIndex = 71;
            this.grpEntityRelatedSites.TabStop = false;
            this.grpEntityRelatedSites.Text = "Related Sites";
            // 
            // trvEntityRelatedSites
            // 
            this.trvEntityRelatedSites.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trvEntityRelatedSites.Location = new System.Drawing.Point(3, 16);
            this.trvEntityRelatedSites.Name = "trvEntityRelatedSites";
            this.trvEntityRelatedSites.Size = new System.Drawing.Size(551, 279);
            this.trvEntityRelatedSites.TabIndex = 1;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.label185);
            this.panel3.Controls.Add(this.lblEntityWorshippingHF);
            this.panel3.Controls.Add(this.label20);
            this.panel3.Controls.Add(this.lblEntityName);
            this.panel3.Controls.Add(this.label18);
            this.panel3.Controls.Add(this.lblEntityRace);
            this.panel3.Controls.Add(this.lblEntityType);
            this.panel3.Controls.Add(this.label21);
            this.panel3.Controls.Add(this.label50);
            this.panel3.Controls.Add(this.lblEntityCivilization);
            this.panel3.Controls.Add(this.label23);
            this.panel3.Controls.Add(this.lblEntityParentCiv);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(3, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(556, 150);
            this.panel3.TabIndex = 70;
            // 
            // label185
            // 
            this.label185.AutoSize = true;
            this.label185.Location = new System.Drawing.Point(1, 115);
            this.label185.Name = "label185";
            this.label185.Size = new System.Drawing.Size(69, 13);
            this.label185.TabIndex = 28;
            this.label185.Text = "Worshipping:";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(3, 0);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(41, 13);
            this.label20.TabIndex = 15;
            this.label20.Text = "Name: ";
            // 
            // lblEntityName
            // 
            this.lblEntityName.AutoSize = true;
            this.lblEntityName.Location = new System.Drawing.Point(70, 0);
            this.lblEntityName.Name = "lblEntityName";
            this.lblEntityName.Size = new System.Drawing.Size(35, 13);
            this.lblEntityName.TabIndex = 16;
            this.lblEntityName.Text = "label2";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(1, 45);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(36, 13);
            this.label18.TabIndex = 17;
            this.label18.Text = "Race:";
            // 
            // lblEntityType
            // 
            this.lblEntityType.AutoSize = true;
            this.lblEntityType.Location = new System.Drawing.Point(70, 24);
            this.lblEntityType.Name = "lblEntityType";
            this.lblEntityType.Size = new System.Drawing.Size(35, 13);
            this.lblEntityType.TabIndex = 27;
            this.lblEntityType.Text = "label2";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(2, 68);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(36, 13);
            this.label21.TabIndex = 19;
            this.label21.Text = "Is Civ:";
            // 
            // label50
            // 
            this.label50.AutoSize = true;
            this.label50.Location = new System.Drawing.Point(3, 24);
            this.label50.Name = "label50";
            this.label50.Size = new System.Drawing.Size(34, 13);
            this.label50.TabIndex = 26;
            this.label50.Text = "Type:";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(2, 93);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(59, 13);
            this.label23.TabIndex = 23;
            this.label23.Text = "Parent Civ:";
            // 
            // grpEntitySiteTakeover
            // 
            this.grpEntitySiteTakeover.Controls.Add(this.lblEntitySiteTakeoverTime);
            this.grpEntitySiteTakeover.Controls.Add(this.lblEntitySiteTakeoverNewLeader);
            this.grpEntitySiteTakeover.Controls.Add(this.label28);
            this.grpEntitySiteTakeover.Controls.Add(this.label29);
            this.grpEntitySiteTakeover.Controls.Add(this.lblEntitySiteTakeoverDefenderEntity);
            this.grpEntitySiteTakeover.Controls.Add(this.lblEntitySiteTakeoverDefenderCiv);
            this.grpEntitySiteTakeover.Controls.Add(this.label26);
            this.grpEntitySiteTakeover.Controls.Add(this.label27);
            this.grpEntitySiteTakeover.Controls.Add(this.lblEntitySiteTakeoverSite);
            this.grpEntitySiteTakeover.Controls.Add(this.label24);
            this.grpEntitySiteTakeover.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpEntitySiteTakeover.Location = new System.Drawing.Point(3, 263);
            this.grpEntitySiteTakeover.Name = "grpEntitySiteTakeover";
            this.grpEntitySiteTakeover.Size = new System.Drawing.Size(556, 296);
            this.grpEntitySiteTakeover.TabIndex = 25;
            this.grpEntitySiteTakeover.TabStop = false;
            this.grpEntitySiteTakeover.Text = "Created on Site Takeover";
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(6, 100);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(24, 13);
            this.label28.TabIndex = 13;
            this.label28.Text = "On:";
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Location = new System.Drawing.Point(6, 79);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(68, 13);
            this.label29.TabIndex = 12;
            this.label29.Text = "New Leader:";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(6, 58);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(86, 13);
            this.label26.TabIndex = 9;
            this.label26.Text = "Defender Entity: ";
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(6, 37);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(72, 13);
            this.label27.TabIndex = 8;
            this.label27.Text = "Defender Civ:";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(6, 15);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(28, 13);
            this.label24.TabIndex = 3;
            this.label24.Text = "Site:";
            // 
            // grpEntityEvents
            // 
            this.grpEntityEvents.Controls.Add(this.lstEntityEvents);
            this.grpEntityEvents.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpEntityEvents.Location = new System.Drawing.Point(1128, 3);
            this.grpEntityEvents.Name = "grpEntityEvents";
            this.tableLayoutPanel20.SetRowSpan(this.grpEntityEvents, 3);
            this.grpEntityEvents.Size = new System.Drawing.Size(558, 556);
            this.grpEntityEvents.TabIndex = 68;
            this.grpEntityEvents.TabStop = false;
            this.grpEntityEvents.Text = "Events";
            // 
            // lstEntityEvents
            // 
            this.lstEntityEvents.Dock = System.Windows.Forms.DockStyle.Top;
            this.lstEntityEvents.FormattingEnabled = true;
            this.lstEntityEvents.Location = new System.Drawing.Point(3, 16);
            this.lstEntityEvents.Name = "lstEntityEvents";
            this.lstEntityEvents.Size = new System.Drawing.Size(552, 134);
            this.lstEntityEvents.TabIndex = 0;
            this.lstEntityEvents.SelectedIndexChanged += new System.EventHandler(this.EventCollection_EventsListClick);
            // 
            // grpEntityCreated
            // 
            this.grpEntityCreated.Controls.Add(this.lblEntityCreatedTime);
            this.grpEntityCreated.Controls.Add(this.lblEntityCreatedSite);
            this.grpEntityCreated.Controls.Add(this.label19);
            this.grpEntityCreated.Controls.Add(this.label22);
            this.grpEntityCreated.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpEntityCreated.Location = new System.Drawing.Point(3, 159);
            this.grpEntityCreated.Name = "grpEntityCreated";
            this.grpEntityCreated.Size = new System.Drawing.Size(556, 98);
            this.grpEntityCreated.TabIndex = 22;
            this.grpEntityCreated.TabStop = false;
            this.grpEntityCreated.Text = "Founded";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(6, 37);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(24, 13);
            this.label19.TabIndex = 3;
            this.label19.Text = "On:";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(6, 16);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(20, 13);
            this.label22.TabIndex = 2;
            this.label22.Text = "At:";
            // 
            // grpEntityRelatedFigures
            // 
            this.grpEntityRelatedFigures.Controls.Add(this.trvEntityRelatedFigures);
            this.grpEntityRelatedFigures.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpEntityRelatedFigures.Location = new System.Drawing.Point(565, 3);
            this.grpEntityRelatedFigures.Name = "grpEntityRelatedFigures";
            this.tableLayoutPanel20.SetRowSpan(this.grpEntityRelatedFigures, 2);
            this.grpEntityRelatedFigures.Size = new System.Drawing.Size(557, 254);
            this.grpEntityRelatedFigures.TabIndex = 21;
            this.grpEntityRelatedFigures.TabStop = false;
            this.grpEntityRelatedFigures.Text = "Related Historical Figures";
            // 
            // trvEntityRelatedFigures
            // 
            this.trvEntityRelatedFigures.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trvEntityRelatedFigures.Location = new System.Drawing.Point(3, 16);
            this.trvEntityRelatedFigures.Name = "trvEntityRelatedFigures";
            this.trvEntityRelatedFigures.Size = new System.Drawing.Size(551, 235);
            this.trvEntityRelatedFigures.TabIndex = 0;
            // 
            // grpEntityRelatedEntities
            // 
            this.grpEntityRelatedEntities.Controls.Add(this.trvEntityRelatedEntities);
            this.grpEntityRelatedEntities.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpEntityRelatedEntities.Location = new System.Drawing.Point(565, 263);
            this.grpEntityRelatedEntities.Name = "grpEntityRelatedEntities";
            this.grpEntityRelatedEntities.Size = new System.Drawing.Size(557, 296);
            this.grpEntityRelatedEntities.TabIndex = 28;
            this.grpEntityRelatedEntities.TabStop = false;
            this.grpEntityRelatedEntities.Text = "Related Entities";
            // 
            // trvEntityRelatedEntities
            // 
            this.trvEntityRelatedEntities.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trvEntityRelatedEntities.Location = new System.Drawing.Point(3, 16);
            this.trvEntityRelatedEntities.Name = "trvEntityRelatedEntities";
            this.trvEntityRelatedEntities.Size = new System.Drawing.Size(551, 277);
            this.trvEntityRelatedEntities.TabIndex = 1;
            // 
            // tabEntityPopulation
            // 
            this.tabEntityPopulation.Controls.Add(this.tableLayoutPanel4);
            this.tabEntityPopulation.Location = new System.Drawing.Point(4, 40);
            this.tabEntityPopulation.Name = "tabEntityPopulation";
            this.tabEntityPopulation.Size = new System.Drawing.Size(1873, 873);
            this.tabEntityPopulation.TabIndex = 4;
            this.tabEntityPopulation.Text = "Entity Population";
            this.tabEntityPopulation.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 2;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Controls.Add(this.TextFilterEntityPopulation, 0, 1);
            this.tableLayoutPanel4.Controls.Add(this.grpEntityPopulation, 1, 0);
            this.tableLayoutPanel4.Controls.Add(this.FilterEntityPopulation, 0, 2);
            this.tableLayoutPanel4.Controls.Add(this.lstEntityPopulation, 0, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel4.Margin = new System.Windows.Forms.Padding(2);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 3;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(1873, 891);
            this.tableLayoutPanel4.TabIndex = 8;
            // 
            // TextFilterEntityPopulation
            // 
            this.TextFilterEntityPopulation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TextFilterEntityPopulation.Location = new System.Drawing.Point(3, 842);
            this.TextFilterEntityPopulation.Name = "TextFilterEntityPopulation";
            this.TextFilterEntityPopulation.Size = new System.Drawing.Size(114, 20);
            this.TextFilterEntityPopulation.TabIndex = 5;
            this.TextFilterEntityPopulation.TextChanged += new System.EventHandler(this.TextFilter_Changed);
            // 
            // grpEntityPopulation
            // 
            this.grpEntityPopulation.Controls.Add(this.tableLayoutPanel21);
            this.grpEntityPopulation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpEntityPopulation.Location = new System.Drawing.Point(123, 3);
            this.grpEntityPopulation.Name = "grpEntityPopulation";
            this.tableLayoutPanel4.SetRowSpan(this.grpEntityPopulation, 3);
            this.grpEntityPopulation.Size = new System.Drawing.Size(1747, 885);
            this.grpEntityPopulation.TabIndex = 3;
            this.grpEntityPopulation.TabStop = false;
            this.grpEntityPopulation.Visible = false;
            // 
            // tableLayoutPanel21
            // 
            this.tableLayoutPanel21.ColumnCount = 2;
            this.tableLayoutPanel21.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.32051F));
            this.tableLayoutPanel21.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 49.67949F));
            this.tableLayoutPanel21.Controls.Add(this.grpEntityPopluationRaces, 0, 1);
            this.tableLayoutPanel21.Controls.Add(this.panel4, 0, 0);
            this.tableLayoutPanel21.Controls.Add(this.grpEntityPopulationBattles, 0, 2);
            this.tableLayoutPanel21.Controls.Add(this.grpEntityPopulationMembers, 1, 0);
            this.tableLayoutPanel21.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel21.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel21.Name = "tableLayoutPanel21";
            this.tableLayoutPanel21.RowCount = 3;
            this.tableLayoutPanel21.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel21.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel21.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel21.Size = new System.Drawing.Size(1741, 866);
            this.tableLayoutPanel21.TabIndex = 21;
            // 
            // grpEntityPopluationRaces
            // 
            this.grpEntityPopluationRaces.Controls.Add(this.lstEntityPopluationRaces);
            this.grpEntityPopluationRaces.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpEntityPopluationRaces.Location = new System.Drawing.Point(3, 63);
            this.grpEntityPopluationRaces.Name = "grpEntityPopluationRaces";
            this.grpEntityPopluationRaces.Size = new System.Drawing.Size(870, 54);
            this.grpEntityPopluationRaces.TabIndex = 38;
            this.grpEntityPopluationRaces.TabStop = false;
            this.grpEntityPopluationRaces.Text = "Population";
            // 
            // lstEntityPopluationRaces
            // 
            this.lstEntityPopluationRaces.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstEntityPopluationRaces.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.lstEntityPopluationRaces.FormattingEnabled = true;
            this.lstEntityPopluationRaces.ItemHeight = 16;
            this.lstEntityPopluationRaces.Location = new System.Drawing.Point(3, 16);
            this.lstEntityPopluationRaces.Name = "lstEntityPopluationRaces";
            this.lstEntityPopluationRaces.Size = new System.Drawing.Size(864, 35);
            this.lstEntityPopluationRaces.TabIndex = 0;
            this.lstEntityPopluationRaces.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.lstEntityPopluationRaces_DrawItem);
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.lblEntityPopulationName);
            this.panel4.Controls.Add(this.label218);
            this.panel4.Controls.Add(this.label186);
            this.panel4.Controls.Add(this.lblEntityPopulationCiv);
            this.panel4.Controls.Add(this.label25);
            this.panel4.Controls.Add(this.lblEntityPopulationRace);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(3, 3);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(870, 54);
            this.panel4.TabIndex = 22;
            // 
            // lblEntityPopulationName
            // 
            this.lblEntityPopulationName.AutoSize = true;
            this.lblEntityPopulationName.Location = new System.Drawing.Point(43, 0);
            this.lblEntityPopulationName.Name = "lblEntityPopulationName";
            this.lblEntityPopulationName.Size = new System.Drawing.Size(35, 13);
            this.lblEntityPopulationName.TabIndex = 82;
            this.lblEntityPopulationName.Text = "label2";
            // 
            // label218
            // 
            this.label218.AutoSize = true;
            this.label218.Location = new System.Drawing.Point(3, 0);
            this.label218.Name = "label218";
            this.label218.Size = new System.Drawing.Size(38, 13);
            this.label218.TabIndex = 22;
            this.label218.Text = "Name:";
            // 
            // label186
            // 
            this.label186.AutoSize = true;
            this.label186.Location = new System.Drawing.Point(3, 30);
            this.label186.Name = "label186";
            this.label186.Size = new System.Drawing.Size(25, 13);
            this.label186.TabIndex = 20;
            this.label186.Text = "Civ:";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(3, 16);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(36, 13);
            this.label25.TabIndex = 18;
            this.label25.Text = "Race:";
            // 
            // grpEntityPopulationBattles
            // 
            this.grpEntityPopulationBattles.Controls.Add(this.lblEntityPopulationBattleTime);
            this.grpEntityPopulationBattles.Controls.Add(this.label36);
            this.grpEntityPopulationBattles.Controls.Add(this.lblEntityPopulationBattleDeaths);
            this.grpEntityPopulationBattles.Controls.Add(this.lblEntityPopulationBattleNumber);
            this.grpEntityPopulationBattles.Controls.Add(this.lblEntityPopulationBattleWar);
            this.grpEntityPopulationBattles.Controls.Add(this.label30);
            this.grpEntityPopulationBattles.Controls.Add(this.label31);
            this.grpEntityPopulationBattles.Controls.Add(this.label32);
            this.grpEntityPopulationBattles.Controls.Add(this.lstEntityPopulationBattles);
            this.grpEntityPopulationBattles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpEntityPopulationBattles.Location = new System.Drawing.Point(3, 123);
            this.grpEntityPopulationBattles.Name = "grpEntityPopulationBattles";
            this.grpEntityPopulationBattles.Size = new System.Drawing.Size(870, 740);
            this.grpEntityPopulationBattles.TabIndex = 12;
            this.grpEntityPopulationBattles.TabStop = false;
            this.grpEntityPopulationBattles.Text = "Battles";
            // 
            // label36
            // 
            this.label36.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label36.AutoSize = true;
            this.label36.Location = new System.Drawing.Point(6, 809);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(24, 13);
            this.label36.TabIndex = 28;
            this.label36.Text = "On:";
            // 
            // lblEntityPopulationBattleDeaths
            // 
            this.lblEntityPopulationBattleDeaths.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblEntityPopulationBattleDeaths.AutoSize = true;
            this.lblEntityPopulationBattleDeaths.Location = new System.Drawing.Point(70, 765);
            this.lblEntityPopulationBattleDeaths.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblEntityPopulationBattleDeaths.Name = "lblEntityPopulationBattleDeaths";
            this.lblEntityPopulationBattleDeaths.Size = new System.Drawing.Size(41, 13);
            this.lblEntityPopulationBattleDeaths.TabIndex = 27;
            this.lblEntityPopulationBattleDeaths.Text = "label35";
            // 
            // lblEntityPopulationBattleNumber
            // 
            this.lblEntityPopulationBattleNumber.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblEntityPopulationBattleNumber.AutoSize = true;
            this.lblEntityPopulationBattleNumber.Location = new System.Drawing.Point(70, 744);
            this.lblEntityPopulationBattleNumber.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblEntityPopulationBattleNumber.Name = "lblEntityPopulationBattleNumber";
            this.lblEntityPopulationBattleNumber.Size = new System.Drawing.Size(41, 13);
            this.lblEntityPopulationBattleNumber.TabIndex = 26;
            this.lblEntityPopulationBattleNumber.Text = "label34";
            // 
            // label30
            // 
            this.label30.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label30.AutoSize = true;
            this.label30.Location = new System.Drawing.Point(6, 786);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(64, 13);
            this.label30.TabIndex = 23;
            this.label30.Text = "Part of War:";
            // 
            // label31
            // 
            this.label31.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label31.AutoSize = true;
            this.label31.Location = new System.Drawing.Point(6, 765);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(44, 13);
            this.label31.TabIndex = 22;
            this.label31.Text = "Deaths:";
            // 
            // label32
            // 
            this.label32.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label32.AutoSize = true;
            this.label32.Location = new System.Drawing.Point(6, 744);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(47, 13);
            this.label32.TabIndex = 19;
            this.label32.Text = "Number:";
            // 
            // lstEntityPopulationBattles
            // 
            this.lstEntityPopulationBattles.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lstEntityPopulationBattles.FormattingEnabled = true;
            this.lstEntityPopulationBattles.Location = new System.Drawing.Point(3, 16);
            this.lstEntityPopulationBattles.Name = "lstEntityPopulationBattles";
            this.lstEntityPopulationBattles.Size = new System.Drawing.Size(459, 108);
            this.lstEntityPopulationBattles.TabIndex = 0;
            this.lstEntityPopulationBattles.SelectedIndexChanged += new System.EventHandler(this.lstEntityPopulationBattles_SelectedIndexChanged);
            // 
            // grpEntityPopulationMembers
            // 
            this.grpEntityPopulationMembers.Controls.Add(this.lstEntityPopulationMembers);
            this.grpEntityPopulationMembers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpEntityPopulationMembers.Location = new System.Drawing.Point(879, 3);
            this.grpEntityPopulationMembers.Name = "grpEntityPopulationMembers";
            this.tableLayoutPanel21.SetRowSpan(this.grpEntityPopulationMembers, 3);
            this.grpEntityPopulationMembers.Size = new System.Drawing.Size(859, 860);
            this.grpEntityPopulationMembers.TabIndex = 20;
            this.grpEntityPopulationMembers.TabStop = false;
            this.grpEntityPopulationMembers.Text = "Members";
            // 
            // lstEntityPopulationMembers
            // 
            this.lstEntityPopulationMembers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstEntityPopulationMembers.FormattingEnabled = true;
            this.lstEntityPopulationMembers.Location = new System.Drawing.Point(3, 16);
            this.lstEntityPopulationMembers.Name = "lstEntityPopulationMembers";
            this.lstEntityPopulationMembers.Size = new System.Drawing.Size(853, 841);
            this.lstEntityPopulationMembers.TabIndex = 0;
            // 
            // FilterEntityPopulation
            // 
            this.FilterEntityPopulation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FilterEntityPopulation.Location = new System.Drawing.Point(3, 866);
            this.FilterEntityPopulation.Name = "FilterEntityPopulation";
            this.FilterEntityPopulation.Size = new System.Drawing.Size(114, 22);
            this.FilterEntityPopulation.TabIndex = 6;
            this.FilterEntityPopulation.Tag = "";
            this.FilterEntityPopulation.Text = "Filter";
            this.FilterEntityPopulation.UseVisualStyleBackColor = true;
            this.FilterEntityPopulation.Click += new System.EventHandler(this.FilterButton_Click);
            // 
            // lstEntityPopulation
            // 
            this.lstEntityPopulation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstEntityPopulation.FormattingEnabled = true;
            this.lstEntityPopulation.Location = new System.Drawing.Point(3, 3);
            this.lstEntityPopulation.Name = "lstEntityPopulation";
            this.lstEntityPopulation.Size = new System.Drawing.Size(114, 833);
            this.lstEntityPopulation.TabIndex = 2;
            // 
            // tabGod
            // 
            this.tabGod.Controls.Add(this.tableLayoutPanel6);
            this.tabGod.Location = new System.Drawing.Point(4, 40);
            this.tabGod.Name = "tabGod";
            this.tabGod.Size = new System.Drawing.Size(1873, 873);
            this.tabGod.TabIndex = 5;
            this.tabGod.Text = "God";
            this.tabGod.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.ColumnCount = 2;
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 172F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel6.Controls.Add(this.TextFilterGod, 0, 1);
            this.tableLayoutPanel6.Controls.Add(this.grpGod, 1, 0);
            this.tableLayoutPanel6.Controls.Add(this.FilterGod, 0, 2);
            this.tableLayoutPanel6.Controls.Add(this.lstGod, 0, 0);
            this.tableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel6.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel6.Margin = new System.Windows.Forms.Padding(2);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 3;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanel6.Size = new System.Drawing.Size(1873, 891);
            this.tableLayoutPanel6.TabIndex = 8;
            // 
            // TextFilterGod
            // 
            this.TextFilterGod.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TextFilterGod.Location = new System.Drawing.Point(3, 842);
            this.TextFilterGod.Name = "TextFilterGod";
            this.TextFilterGod.Size = new System.Drawing.Size(166, 20);
            this.TextFilterGod.TabIndex = 5;
            this.TextFilterGod.TextChanged += new System.EventHandler(this.TextFilter_Changed);
            // 
            // grpGod
            // 
            this.grpGod.Controls.Add(this.tableLayoutPanel22);
            this.grpGod.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpGod.Location = new System.Drawing.Point(175, 3);
            this.grpGod.Name = "grpGod";
            this.tableLayoutPanel6.SetRowSpan(this.grpGod, 3);
            this.grpGod.Size = new System.Drawing.Size(1695, 885);
            this.grpGod.TabIndex = 3;
            this.grpGod.TabStop = false;
            this.grpGod.Visible = false;
            // 
            // tableLayoutPanel22
            // 
            this.tableLayoutPanel22.ColumnCount = 2;
            this.tableLayoutPanel22.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 49.88688F));
            this.tableLayoutPanel22.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.11312F));
            this.tableLayoutPanel22.Controls.Add(this.panel5, 0, 0);
            this.tableLayoutPanel22.Controls.Add(this.grpGodCivilizations, 1, 0);
            this.tableLayoutPanel22.Controls.Add(this.grpGodLeaders, 0, 1);
            this.tableLayoutPanel22.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel22.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel22.Name = "tableLayoutPanel22";
            this.tableLayoutPanel22.RowCount = 2;
            this.tableLayoutPanel22.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 104F));
            this.tableLayoutPanel22.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel22.Size = new System.Drawing.Size(1689, 866);
            this.tableLayoutPanel22.TabIndex = 23;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.label39);
            this.panel5.Controls.Add(this.lblGodName);
            this.panel5.Controls.Add(this.lblGodHF);
            this.panel5.Controls.Add(this.label37);
            this.panel5.Controls.Add(this.label38);
            this.panel5.Controls.Add(this.lblGodType);
            this.panel5.Controls.Add(this.lblGodSpheres);
            this.panel5.Controls.Add(this.label34);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(3, 3);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(836, 98);
            this.panel5.TabIndex = 24;
            // 
            // label39
            // 
            this.label39.AutoSize = true;
            this.label39.Location = new System.Drawing.Point(3, 0);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(41, 13);
            this.label39.TabIndex = 15;
            this.label39.Text = "Name: ";
            // 
            // lblGodName
            // 
            this.lblGodName.AutoSize = true;
            this.lblGodName.Location = new System.Drawing.Point(58, 0);
            this.lblGodName.Name = "lblGodName";
            this.lblGodName.Size = new System.Drawing.Size(35, 13);
            this.lblGodName.TabIndex = 16;
            this.lblGodName.Text = "label2";
            // 
            // label37
            // 
            this.label37.AutoSize = true;
            this.label37.Location = new System.Drawing.Point(3, 22);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(34, 13);
            this.label37.TabIndex = 17;
            this.label37.Text = "Type:";
            // 
            // label38
            // 
            this.label38.AutoSize = true;
            this.label38.Location = new System.Drawing.Point(3, 43);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(35, 13);
            this.label38.TabIndex = 21;
            this.label38.Text = "Is HF:";
            // 
            // lblGodType
            // 
            this.lblGodType.AutoSize = true;
            this.lblGodType.Location = new System.Drawing.Point(58, 22);
            this.lblGodType.Name = "lblGodType";
            this.lblGodType.Size = new System.Drawing.Size(35, 13);
            this.lblGodType.TabIndex = 18;
            this.lblGodType.Text = "label2";
            // 
            // lblGodSpheres
            // 
            this.lblGodSpheres.AutoSize = true;
            this.lblGodSpheres.Location = new System.Drawing.Point(58, 64);
            this.lblGodSpheres.Name = "lblGodSpheres";
            this.lblGodSpheres.Size = new System.Drawing.Size(35, 13);
            this.lblGodSpheres.TabIndex = 20;
            this.lblGodSpheres.Text = "label2";
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Location = new System.Drawing.Point(3, 64);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(49, 13);
            this.label34.TabIndex = 19;
            this.label34.Text = "Spheres:";
            // 
            // grpGodCivilizations
            // 
            this.grpGodCivilizations.Controls.Add(this.lstGodCivilizations);
            this.grpGodCivilizations.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpGodCivilizations.Location = new System.Drawing.Point(845, 3);
            this.grpGodCivilizations.Name = "grpGodCivilizations";
            this.tableLayoutPanel22.SetRowSpan(this.grpGodCivilizations, 2);
            this.grpGodCivilizations.Size = new System.Drawing.Size(841, 860);
            this.grpGodCivilizations.TabIndex = 14;
            this.grpGodCivilizations.TabStop = false;
            this.grpGodCivilizations.Text = "Civilizations";
            // 
            // lstGodCivilizations
            // 
            this.lstGodCivilizations.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstGodCivilizations.FormattingEnabled = true;
            this.lstGodCivilizations.Location = new System.Drawing.Point(3, 16);
            this.lstGodCivilizations.Name = "lstGodCivilizations";
            this.lstGodCivilizations.Size = new System.Drawing.Size(835, 841);
            this.lstGodCivilizations.TabIndex = 1;
            // 
            // grpGodLeaders
            // 
            this.grpGodLeaders.Controls.Add(this.lstGodLeaders);
            this.grpGodLeaders.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpGodLeaders.Location = new System.Drawing.Point(3, 107);
            this.grpGodLeaders.Name = "grpGodLeaders";
            this.grpGodLeaders.Size = new System.Drawing.Size(836, 756);
            this.grpGodLeaders.TabIndex = 13;
            this.grpGodLeaders.TabStop = false;
            this.grpGodLeaders.Text = "Leaders";
            // 
            // lstGodLeaders
            // 
            this.lstGodLeaders.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstGodLeaders.FormattingEnabled = true;
            this.lstGodLeaders.Location = new System.Drawing.Point(3, 16);
            this.lstGodLeaders.Name = "lstGodLeaders";
            this.lstGodLeaders.Size = new System.Drawing.Size(830, 737);
            this.lstGodLeaders.TabIndex = 0;
            // 
            // FilterGod
            // 
            this.FilterGod.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FilterGod.Location = new System.Drawing.Point(3, 866);
            this.FilterGod.Name = "FilterGod";
            this.FilterGod.Size = new System.Drawing.Size(166, 22);
            this.FilterGod.TabIndex = 6;
            this.FilterGod.Tag = "";
            this.FilterGod.Text = "Filter";
            this.FilterGod.UseVisualStyleBackColor = true;
            this.FilterGod.Click += new System.EventHandler(this.FilterButton_Click);
            // 
            // lstGod
            // 
            this.lstGod.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstGod.FormattingEnabled = true;
            this.lstGod.Location = new System.Drawing.Point(3, 3);
            this.lstGod.Name = "lstGod";
            this.lstGod.Size = new System.Drawing.Size(166, 833);
            this.lstGod.TabIndex = 2;
            // 
            // tabHistoricalFigure
            // 
            this.tabHistoricalFigure.Controls.Add(this.tableLayoutPanel7);
            this.tabHistoricalFigure.Location = new System.Drawing.Point(4, 40);
            this.tabHistoricalFigure.Name = "tabHistoricalFigure";
            this.tabHistoricalFigure.Size = new System.Drawing.Size(1873, 873);
            this.tabHistoricalFigure.TabIndex = 6;
            this.tabHistoricalFigure.Text = "Historical Figure";
            this.tabHistoricalFigure.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel7
            // 
            this.tableLayoutPanel7.ColumnCount = 2;
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 170F));
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel7.Controls.Add(this.TextFilterHistoricalFigure, 0, 2);
            this.tableLayoutPanel7.Controls.Add(this.lstHistoricalFigure, 0, 0);
            this.tableLayoutPanel7.Controls.Add(this.FilterHistoricalFigure, 0, 3);
            this.tableLayoutPanel7.Controls.Add(this.grpHistoricalFigure, 1, 0);
            this.tableLayoutPanel7.Controls.Add(this.chkHistoricalFigureDetailedView, 0, 1);
            this.tableLayoutPanel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel7.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel7.Margin = new System.Windows.Forms.Padding(2);
            this.tableLayoutPanel7.Name = "tableLayoutPanel7";
            this.tableLayoutPanel7.RowCount = 4;
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 21F));
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel7.Size = new System.Drawing.Size(1873, 891);
            this.tableLayoutPanel7.TabIndex = 8;
            // 
            // TextFilterHistoricalFigure
            // 
            this.TextFilterHistoricalFigure.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TextFilterHistoricalFigure.Location = new System.Drawing.Point(3, 839);
            this.TextFilterHistoricalFigure.Name = "TextFilterHistoricalFigure";
            this.TextFilterHistoricalFigure.Size = new System.Drawing.Size(164, 20);
            this.TextFilterHistoricalFigure.TabIndex = 5;
            this.TextFilterHistoricalFigure.TextChanged += new System.EventHandler(this.TextFilter_Changed);
            // 
            // lstHistoricalFigure
            // 
            this.lstHistoricalFigure.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstHistoricalFigure.FormattingEnabled = true;
            this.lstHistoricalFigure.Location = new System.Drawing.Point(3, 3);
            this.lstHistoricalFigure.Name = "lstHistoricalFigure";
            this.lstHistoricalFigure.Size = new System.Drawing.Size(164, 809);
            this.lstHistoricalFigure.TabIndex = 2;
            // 
            // FilterHistoricalFigure
            // 
            this.FilterHistoricalFigure.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FilterHistoricalFigure.Location = new System.Drawing.Point(3, 864);
            this.FilterHistoricalFigure.Name = "FilterHistoricalFigure";
            this.FilterHistoricalFigure.Size = new System.Drawing.Size(164, 24);
            this.FilterHistoricalFigure.TabIndex = 4;
            this.FilterHistoricalFigure.Tag = "";
            this.FilterHistoricalFigure.Text = "Filter";
            this.FilterHistoricalFigure.UseVisualStyleBackColor = true;
            this.FilterHistoricalFigure.Click += new System.EventHandler(this.FilterButton_Click);
            // 
            // grpHistoricalFigure
            // 
            this.grpHistoricalFigure.Controls.Add(this.tableLayoutPanel23);
            this.grpHistoricalFigure.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpHistoricalFigure.Location = new System.Drawing.Point(173, 3);
            this.grpHistoricalFigure.Name = "grpHistoricalFigure";
            this.tableLayoutPanel7.SetRowSpan(this.grpHistoricalFigure, 4);
            this.grpHistoricalFigure.Size = new System.Drawing.Size(1697, 885);
            this.grpHistoricalFigure.TabIndex = 3;
            this.grpHistoricalFigure.TabStop = false;
            this.grpHistoricalFigure.Visible = false;
            // 
            // tableLayoutPanel23
            // 
            this.tableLayoutPanel23.ColumnCount = 5;
            this.tableLayoutPanel23.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 125F));
            this.tableLayoutPanel23.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 125F));
            this.tableLayoutPanel23.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 230F));
            this.tableLayoutPanel23.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel23.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel23.Controls.Add(this.grpHistoricalFigureArtifacts, 0, 4);
            this.tableLayoutPanel23.Controls.Add(this.panel6, 0, 0);
            this.tableLayoutPanel23.Controls.Add(this.grpHistoricalFigureEvents, 2, 1);
            this.tableLayoutPanel23.Controls.Add(this.grpHistoricalFigureDescendents, 4, 0);
            this.tableLayoutPanel23.Controls.Add(this.grpHistoricalFigureSkills, 2, 4);
            this.tableLayoutPanel23.Controls.Add(this.grpHistoricalFigureAncestors, 4, 2);
            this.tableLayoutPanel23.Controls.Add(this.grpHistoricalFigureDeath, 2, 0);
            this.tableLayoutPanel23.Controls.Add(this.grpHistoricalFigureSpheres, 0, 2);
            this.tableLayoutPanel23.Controls.Add(this.grpHistoricalFigureKnowledge, 1, 2);
            this.tableLayoutPanel23.Controls.Add(this.grpHistoricalFigurePets, 0, 3);
            this.tableLayoutPanel23.Controls.Add(this.grpHistoricalFigureHFLinks, 3, 0);
            this.tableLayoutPanel23.Controls.Add(this.grpHistoricalFigureEntityLinks, 3, 3);
            this.tableLayoutPanel23.Controls.Add(this.rtbHistoricalFigureSummary, 3, 4);
            this.tableLayoutPanel23.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel23.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel23.Name = "tableLayoutPanel23";
            this.tableLayoutPanel23.RowCount = 5;
            this.tableLayoutPanel23.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tableLayoutPanel23.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 239F));
            this.tableLayoutPanel23.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 31.48789F));
            this.tableLayoutPanel23.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 35.64014F));
            this.tableLayoutPanel23.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 32.87197F));
            this.tableLayoutPanel23.Size = new System.Drawing.Size(1691, 866);
            this.tableLayoutPanel23.TabIndex = 70;
            // 
            // grpHistoricalFigureArtifacts
            // 
            this.tableLayoutPanel23.SetColumnSpan(this.grpHistoricalFigureArtifacts, 2);
            this.grpHistoricalFigureArtifacts.Controls.Add(this.lstHistoricalFigureArtifacts);
            this.grpHistoricalFigureArtifacts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpHistoricalFigureArtifacts.Location = new System.Drawing.Point(3, 701);
            this.grpHistoricalFigureArtifacts.Name = "grpHistoricalFigureArtifacts";
            this.grpHistoricalFigureArtifacts.Size = new System.Drawing.Size(244, 162);
            this.grpHistoricalFigureArtifacts.TabIndex = 72;
            this.grpHistoricalFigureArtifacts.TabStop = false;
            this.grpHistoricalFigureArtifacts.Text = "Artifacts Created";
            // 
            // lstHistoricalFigureArtifacts
            // 
            this.lstHistoricalFigureArtifacts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstHistoricalFigureArtifacts.FormattingEnabled = true;
            this.lstHistoricalFigureArtifacts.Location = new System.Drawing.Point(3, 16);
            this.lstHistoricalFigureArtifacts.Name = "lstHistoricalFigureArtifacts";
            this.lstHistoricalFigureArtifacts.Size = new System.Drawing.Size(238, 143);
            this.lstHistoricalFigureArtifacts.TabIndex = 0;
            // 
            // panel6
            // 
            this.tableLayoutPanel23.SetColumnSpan(this.panel6, 2);
            this.panel6.Controls.Add(this.label311);
            this.panel6.Controls.Add(this.lblHistoricalFigureSex);
            this.panel6.Controls.Add(this.label192);
            this.panel6.Controls.Add(this.lblHistoricalFigureEntityPopulation);
            this.panel6.Controls.Add(this.label45);
            this.panel6.Controls.Add(this.lblHistoricalFigureName);
            this.panel6.Controls.Add(this.lblHistoricalFigureCoords);
            this.panel6.Controls.Add(this.label43);
            this.panel6.Controls.Add(this.label172);
            this.panel6.Controls.Add(this.label41);
            this.panel6.Controls.Add(this.lblHistoricalFigureAppeared);
            this.panel6.Controls.Add(this.lblHistoricalFigureAge);
            this.panel6.Controls.Add(this.label35);
            this.panel6.Controls.Add(this.lblHistoricalFigureAgeCaption);
            this.panel6.Controls.Add(this.label47);
            this.panel6.Controls.Add(this.lblHistoricalFigureLife);
            this.panel6.Controls.Add(this.label49);
            this.panel6.Controls.Add(this.lblHistoricalFigureAssociatedType);
            this.panel6.Controls.Add(this.label55);
            this.panel6.Controls.Add(this.lblHistoricalFigureLocation);
            this.panel6.Controls.Add(this.label59);
            this.panel6.Controls.Add(this.lblHistoricalFigureLocationText);
            this.panel6.Controls.Add(this.label57);
            this.panel6.Controls.Add(this.label63);
            this.panel6.Controls.Add(this.lblHistoricalFigureRace);
            this.panel6.Controls.Add(this.lblHistoricalFigureAnimated);
            this.panel6.Controls.Add(this.lblHistoricalFigureCaste);
            this.panel6.Controls.Add(this.label61);
            this.panel6.Controls.Add(this.lblHistoricalFigureUnit);
            this.panel6.Controls.Add(this.lblHistoricalFigureGhost);
            this.panel6.Controls.Add(this.lblHistoricalFigureGod);
            this.panel6.Controls.Add(this.lblHistoricalFigureLeader);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel6.Location = new System.Drawing.Point(3, 3);
            this.panel6.Name = "panel6";
            this.tableLayoutPanel23.SetRowSpan(this.panel6, 2);
            this.panel6.Size = new System.Drawing.Size(244, 353);
            this.panel6.TabIndex = 71;
            // 
            // label311
            // 
            this.label311.AutoSize = true;
            this.label311.Location = new System.Drawing.Point(3, 64);
            this.label311.Name = "label311";
            this.label311.Size = new System.Drawing.Size(28, 13);
            this.label311.TabIndex = 72;
            this.label311.Text = "Sex:";
            // 
            // lblHistoricalFigureSex
            // 
            this.lblHistoricalFigureSex.AutoSize = true;
            this.lblHistoricalFigureSex.Location = new System.Drawing.Point(92, 64);
            this.lblHistoricalFigureSex.Name = "lblHistoricalFigureSex";
            this.lblHistoricalFigureSex.Size = new System.Drawing.Size(35, 13);
            this.lblHistoricalFigureSex.TabIndex = 73;
            this.lblHistoricalFigureSex.Text = "label2";
            // 
            // label192
            // 
            this.label192.AutoSize = true;
            this.label192.Location = new System.Drawing.Point(2, 328);
            this.label192.Name = "label192";
            this.label192.Size = new System.Drawing.Size(70, 13);
            this.label192.TabIndex = 70;
            this.label192.Text = "In Entity Pop:";
            // 
            // label45
            // 
            this.label45.AutoSize = true;
            this.label45.Location = new System.Drawing.Point(3, 0);
            this.label45.Name = "label45";
            this.label45.Size = new System.Drawing.Size(41, 13);
            this.label45.TabIndex = 24;
            this.label45.Text = "Name: ";
            // 
            // lblHistoricalFigureName
            // 
            this.lblHistoricalFigureName.AutoSize = true;
            this.lblHistoricalFigureName.Location = new System.Drawing.Point(92, 0);
            this.lblHistoricalFigureName.Name = "lblHistoricalFigureName";
            this.lblHistoricalFigureName.Size = new System.Drawing.Size(35, 13);
            this.lblHistoricalFigureName.TabIndex = 25;
            this.lblHistoricalFigureName.Text = "label2";
            // 
            // label43
            // 
            this.label43.AutoSize = true;
            this.label43.Location = new System.Drawing.Point(3, 22);
            this.label43.Name = "label43";
            this.label43.Size = new System.Drawing.Size(36, 13);
            this.label43.TabIndex = 26;
            this.label43.Text = "Race:";
            // 
            // label172
            // 
            this.label172.AutoSize = true;
            this.label172.Location = new System.Drawing.Point(2, 238);
            this.label172.Name = "label172";
            this.label172.Size = new System.Drawing.Size(43, 13);
            this.label172.TabIndex = 68;
            this.label172.Text = "Coords:";
            // 
            // label41
            // 
            this.label41.AutoSize = true;
            this.label41.Location = new System.Drawing.Point(3, 86);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(56, 13);
            this.label41.TabIndex = 28;
            this.label41.Text = "Appeared:";
            // 
            // lblHistoricalFigureAppeared
            // 
            this.lblHistoricalFigureAppeared.AutoSize = true;
            this.lblHistoricalFigureAppeared.Location = new System.Drawing.Point(92, 86);
            this.lblHistoricalFigureAppeared.Name = "lblHistoricalFigureAppeared";
            this.lblHistoricalFigureAppeared.Size = new System.Drawing.Size(35, 13);
            this.lblHistoricalFigureAppeared.TabIndex = 29;
            this.lblHistoricalFigureAppeared.Text = "label2";
            // 
            // lblHistoricalFigureAge
            // 
            this.lblHistoricalFigureAge.AutoSize = true;
            this.lblHistoricalFigureAge.Location = new System.Drawing.Point(92, 129);
            this.lblHistoricalFigureAge.Name = "lblHistoricalFigureAge";
            this.lblHistoricalFigureAge.Size = new System.Drawing.Size(35, 13);
            this.lblHistoricalFigureAge.TabIndex = 66;
            this.lblHistoricalFigureAge.Text = "label2";
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.Location = new System.Drawing.Point(3, 43);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(37, 13);
            this.label35.TabIndex = 30;
            this.label35.Text = "Caste:";
            // 
            // lblHistoricalFigureAgeCaption
            // 
            this.lblHistoricalFigureAgeCaption.AutoSize = true;
            this.lblHistoricalFigureAgeCaption.Location = new System.Drawing.Point(5, 129);
            this.lblHistoricalFigureAgeCaption.Name = "lblHistoricalFigureAgeCaption";
            this.lblHistoricalFigureAgeCaption.Size = new System.Drawing.Size(29, 13);
            this.lblHistoricalFigureAgeCaption.TabIndex = 65;
            this.lblHistoricalFigureAgeCaption.Text = "Age:";
            // 
            // label47
            // 
            this.label47.AutoSize = true;
            this.label47.Location = new System.Drawing.Point(3, 108);
            this.label47.Name = "label47";
            this.label47.Size = new System.Drawing.Size(27, 13);
            this.label47.TabIndex = 32;
            this.label47.Text = "Life:";
            // 
            // lblHistoricalFigureLife
            // 
            this.lblHistoricalFigureLife.AutoSize = true;
            this.lblHistoricalFigureLife.Location = new System.Drawing.Point(92, 108);
            this.lblHistoricalFigureLife.Name = "lblHistoricalFigureLife";
            this.lblHistoricalFigureLife.Size = new System.Drawing.Size(35, 13);
            this.lblHistoricalFigureLife.TabIndex = 33;
            this.lblHistoricalFigureLife.Text = "label2";
            // 
            // label49
            // 
            this.label49.AutoSize = true;
            this.label49.Location = new System.Drawing.Point(3, 152);
            this.label49.Name = "label49";
            this.label49.Size = new System.Drawing.Size(89, 13);
            this.label49.TabIndex = 36;
            this.label49.Text = "Associated Type:";
            // 
            // lblHistoricalFigureAssociatedType
            // 
            this.lblHistoricalFigureAssociatedType.AutoSize = true;
            this.lblHistoricalFigureAssociatedType.Location = new System.Drawing.Point(92, 152);
            this.lblHistoricalFigureAssociatedType.Name = "lblHistoricalFigureAssociatedType";
            this.lblHistoricalFigureAssociatedType.Size = new System.Drawing.Size(35, 13);
            this.lblHistoricalFigureAssociatedType.TabIndex = 37;
            this.lblHistoricalFigureAssociatedType.Text = "label2";
            // 
            // label55
            // 
            this.label55.AutoSize = true;
            this.label55.Location = new System.Drawing.Point(2, 262);
            this.label55.Name = "label55";
            this.label55.Size = new System.Drawing.Size(44, 13);
            this.label55.TabIndex = 38;
            this.label55.Text = "Is God: ";
            // 
            // label59
            // 
            this.label59.AutoSize = true;
            this.label59.Location = new System.Drawing.Point(2, 284);
            this.label59.Name = "label59";
            this.label59.Size = new System.Drawing.Size(54, 13);
            this.label59.TabIndex = 42;
            this.label59.Text = "Is Leader:";
            // 
            // lblHistoricalFigureLocationText
            // 
            this.lblHistoricalFigureLocationText.AutoSize = true;
            this.lblHistoricalFigureLocationText.Location = new System.Drawing.Point(2, 216);
            this.lblHistoricalFigureLocationText.Name = "lblHistoricalFigureLocationText";
            this.lblHistoricalFigureLocationText.Size = new System.Drawing.Size(44, 13);
            this.lblHistoricalFigureLocationText.TabIndex = 59;
            this.lblHistoricalFigureLocationText.Text = "At Site: ";
            // 
            // label57
            // 
            this.label57.AutoSize = true;
            this.label57.Location = new System.Drawing.Point(2, 306);
            this.label57.Name = "label57";
            this.label57.Size = new System.Drawing.Size(29, 13);
            this.label57.TabIndex = 44;
            this.label57.Text = "Unit:";
            // 
            // label63
            // 
            this.label63.AutoSize = true;
            this.label63.Location = new System.Drawing.Point(2, 173);
            this.label63.Name = "label63";
            this.label63.Size = new System.Drawing.Size(54, 13);
            this.label63.TabIndex = 46;
            this.label63.Text = "Animated:";
            // 
            // lblHistoricalFigureAnimated
            // 
            this.lblHistoricalFigureAnimated.AutoSize = true;
            this.lblHistoricalFigureAnimated.Location = new System.Drawing.Point(92, 173);
            this.lblHistoricalFigureAnimated.Name = "lblHistoricalFigureAnimated";
            this.lblHistoricalFigureAnimated.Size = new System.Drawing.Size(35, 13);
            this.lblHistoricalFigureAnimated.TabIndex = 47;
            this.lblHistoricalFigureAnimated.Text = "label2";
            // 
            // lblHistoricalFigureCaste
            // 
            this.lblHistoricalFigureCaste.AutoSize = true;
            this.lblHistoricalFigureCaste.Location = new System.Drawing.Point(92, 43);
            this.lblHistoricalFigureCaste.Name = "lblHistoricalFigureCaste";
            this.lblHistoricalFigureCaste.Size = new System.Drawing.Size(35, 13);
            this.lblHistoricalFigureCaste.TabIndex = 54;
            this.lblHistoricalFigureCaste.Text = "label2";
            // 
            // label61
            // 
            this.label61.AutoSize = true;
            this.label61.Location = new System.Drawing.Point(2, 195);
            this.label61.Name = "label61";
            this.label61.Size = new System.Drawing.Size(38, 13);
            this.label61.TabIndex = 48;
            this.label61.Text = "Ghost:";
            // 
            // lblHistoricalFigureGhost
            // 
            this.lblHistoricalFigureGhost.AutoSize = true;
            this.lblHistoricalFigureGhost.Location = new System.Drawing.Point(92, 195);
            this.lblHistoricalFigureGhost.Name = "lblHistoricalFigureGhost";
            this.lblHistoricalFigureGhost.Size = new System.Drawing.Size(35, 13);
            this.lblHistoricalFigureGhost.TabIndex = 49;
            this.lblHistoricalFigureGhost.Text = "label2";
            // 
            // grpHistoricalFigureEvents
            // 
            this.grpHistoricalFigureEvents.Controls.Add(this.lstHistoricalFigureEvents);
            this.grpHistoricalFigureEvents.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpHistoricalFigureEvents.Location = new System.Drawing.Point(253, 123);
            this.grpHistoricalFigureEvents.Name = "grpHistoricalFigureEvents";
            this.tableLayoutPanel23.SetRowSpan(this.grpHistoricalFigureEvents, 3);
            this.grpHistoricalFigureEvents.Size = new System.Drawing.Size(224, 572);
            this.grpHistoricalFigureEvents.TabIndex = 67;
            this.grpHistoricalFigureEvents.TabStop = false;
            this.grpHistoricalFigureEvents.Text = "Events";
            // 
            // lstHistoricalFigureEvents
            // 
            this.lstHistoricalFigureEvents.Dock = System.Windows.Forms.DockStyle.Top;
            this.lstHistoricalFigureEvents.FormattingEnabled = true;
            this.lstHistoricalFigureEvents.Location = new System.Drawing.Point(3, 16);
            this.lstHistoricalFigureEvents.Name = "lstHistoricalFigureEvents";
            this.lstHistoricalFigureEvents.Size = new System.Drawing.Size(218, 134);
            this.lstHistoricalFigureEvents.TabIndex = 0;
            this.lstHistoricalFigureEvents.SelectedIndexChanged += new System.EventHandler(this.EventCollection_EventsListClick);
            // 
            // grpHistoricalFigureDescendents
            // 
            this.grpHistoricalFigureDescendents.Controls.Add(this.trvHistoricalFigureDescendents);
            this.grpHistoricalFigureDescendents.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpHistoricalFigureDescendents.Location = new System.Drawing.Point(1088, 3);
            this.grpHistoricalFigureDescendents.Name = "grpHistoricalFigureDescendents";
            this.tableLayoutPanel23.SetRowSpan(this.grpHistoricalFigureDescendents, 2);
            this.grpHistoricalFigureDescendents.Size = new System.Drawing.Size(600, 353);
            this.grpHistoricalFigureDescendents.TabIndex = 22;
            this.grpHistoricalFigureDescendents.TabStop = false;
            this.grpHistoricalFigureDescendents.Text = "Descendents";
            // 
            // trvHistoricalFigureDescendents
            // 
            this.trvHistoricalFigureDescendents.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trvHistoricalFigureDescendents.Location = new System.Drawing.Point(3, 16);
            this.trvHistoricalFigureDescendents.Name = "trvHistoricalFigureDescendents";
            this.trvHistoricalFigureDescendents.Size = new System.Drawing.Size(594, 334);
            this.trvHistoricalFigureDescendents.TabIndex = 0;
            // 
            // grpHistoricalFigureSkills
            // 
            this.grpHistoricalFigureSkills.Controls.Add(this.lstHistoricalFigureSkills);
            this.grpHistoricalFigureSkills.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpHistoricalFigureSkills.Location = new System.Drawing.Point(253, 701);
            this.grpHistoricalFigureSkills.Name = "grpHistoricalFigureSkills";
            this.grpHistoricalFigureSkills.Size = new System.Drawing.Size(224, 162);
            this.grpHistoricalFigureSkills.TabIndex = 64;
            this.grpHistoricalFigureSkills.TabStop = false;
            this.grpHistoricalFigureSkills.Text = "Skills";
            // 
            // lstHistoricalFigureSkills
            // 
            this.lstHistoricalFigureSkills.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstHistoricalFigureSkills.FormattingEnabled = true;
            this.lstHistoricalFigureSkills.Location = new System.Drawing.Point(3, 16);
            this.lstHistoricalFigureSkills.Name = "lstHistoricalFigureSkills";
            this.lstHistoricalFigureSkills.Size = new System.Drawing.Size(218, 143);
            this.lstHistoricalFigureSkills.TabIndex = 0;
            // 
            // grpHistoricalFigureAncestors
            // 
            this.grpHistoricalFigureAncestors.Controls.Add(this.trvHistoricalFigureAncestors);
            this.grpHistoricalFigureAncestors.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpHistoricalFigureAncestors.Location = new System.Drawing.Point(1088, 362);
            this.grpHistoricalFigureAncestors.Name = "grpHistoricalFigureAncestors";
            this.tableLayoutPanel23.SetRowSpan(this.grpHistoricalFigureAncestors, 3);
            this.grpHistoricalFigureAncestors.Size = new System.Drawing.Size(600, 501);
            this.grpHistoricalFigureAncestors.TabIndex = 23;
            this.grpHistoricalFigureAncestors.TabStop = false;
            this.grpHistoricalFigureAncestors.Text = "Ancestors";
            // 
            // trvHistoricalFigureAncestors
            // 
            this.trvHistoricalFigureAncestors.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trvHistoricalFigureAncestors.Location = new System.Drawing.Point(3, 16);
            this.trvHistoricalFigureAncestors.Name = "trvHistoricalFigureAncestors";
            this.trvHistoricalFigureAncestors.Size = new System.Drawing.Size(594, 482);
            this.trvHistoricalFigureAncestors.TabIndex = 0;
            // 
            // grpHistoricalFigureDeath
            // 
            this.grpHistoricalFigureDeath.Controls.Add(this.lblHistoricalFigureDeathCause);
            this.grpHistoricalFigureDeath.Controls.Add(this.lblHistoricalFigureDeathTime);
            this.grpHistoricalFigureDeath.Controls.Add(this.label46);
            this.grpHistoricalFigureDeath.Controls.Add(this.lblHistoricalFigureDeathLocation);
            this.grpHistoricalFigureDeath.Controls.Add(this.lblHistoricalFigureDeathSlayer);
            this.grpHistoricalFigureDeath.Controls.Add(this.label40);
            this.grpHistoricalFigureDeath.Controls.Add(this.label42);
            this.grpHistoricalFigureDeath.Controls.Add(this.label44);
            this.grpHistoricalFigureDeath.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpHistoricalFigureDeath.Location = new System.Drawing.Point(253, 3);
            this.grpHistoricalFigureDeath.Name = "grpHistoricalFigureDeath";
            this.grpHistoricalFigureDeath.Size = new System.Drawing.Size(224, 114);
            this.grpHistoricalFigureDeath.TabIndex = 56;
            this.grpHistoricalFigureDeath.TabStop = false;
            this.grpHistoricalFigureDeath.Text = "Death";
            // 
            // lblHistoricalFigureDeathCause
            // 
            this.lblHistoricalFigureDeathCause.AutoSize = true;
            this.lblHistoricalFigureDeathCause.Location = new System.Drawing.Point(47, 64);
            this.lblHistoricalFigureDeathCause.Name = "lblHistoricalFigureDeathCause";
            this.lblHistoricalFigureDeathCause.Size = new System.Drawing.Size(35, 13);
            this.lblHistoricalFigureDeathCause.TabIndex = 55;
            this.lblHistoricalFigureDeathCause.Text = "label2";
            // 
            // label46
            // 
            this.label46.AutoSize = true;
            this.label46.Location = new System.Drawing.Point(6, 86);
            this.label46.Name = "label46";
            this.label46.Size = new System.Drawing.Size(24, 13);
            this.label46.TabIndex = 8;
            this.label46.Text = "On:";
            // 
            // label40
            // 
            this.label40.AutoSize = true;
            this.label40.Location = new System.Drawing.Point(6, 64);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(40, 13);
            this.label40.TabIndex = 3;
            this.label40.Text = "Cause:";
            // 
            // label42
            // 
            this.label42.AutoSize = true;
            this.label42.Location = new System.Drawing.Point(6, 43);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(20, 13);
            this.label42.TabIndex = 2;
            this.label42.Text = "At:";
            // 
            // label44
            // 
            this.label44.AutoSize = true;
            this.label44.Location = new System.Drawing.Point(6, 21);
            this.label44.Name = "label44";
            this.label44.Size = new System.Drawing.Size(25, 13);
            this.label44.TabIndex = 0;
            this.label44.Text = "By: ";
            // 
            // grpHistoricalFigureSpheres
            // 
            this.grpHistoricalFigureSpheres.Controls.Add(this.lstHistoricalFigureSpheres);
            this.grpHistoricalFigureSpheres.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpHistoricalFigureSpheres.Location = new System.Drawing.Point(3, 362);
            this.grpHistoricalFigureSpheres.Name = "grpHistoricalFigureSpheres";
            this.grpHistoricalFigureSpheres.Size = new System.Drawing.Size(119, 153);
            this.grpHistoricalFigureSpheres.TabIndex = 62;
            this.grpHistoricalFigureSpheres.TabStop = false;
            this.grpHistoricalFigureSpheres.Text = "Spheres";
            // 
            // lstHistoricalFigureSpheres
            // 
            this.lstHistoricalFigureSpheres.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstHistoricalFigureSpheres.FormattingEnabled = true;
            this.lstHistoricalFigureSpheres.Location = new System.Drawing.Point(3, 16);
            this.lstHistoricalFigureSpheres.Name = "lstHistoricalFigureSpheres";
            this.lstHistoricalFigureSpheres.Size = new System.Drawing.Size(113, 134);
            this.lstHistoricalFigureSpheres.TabIndex = 0;
            // 
            // grpHistoricalFigureKnowledge
            // 
            this.grpHistoricalFigureKnowledge.Controls.Add(this.lstHistoricalFigureKnowledge);
            this.grpHistoricalFigureKnowledge.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpHistoricalFigureKnowledge.Location = new System.Drawing.Point(128, 362);
            this.grpHistoricalFigureKnowledge.Name = "grpHistoricalFigureKnowledge";
            this.grpHistoricalFigureKnowledge.Size = new System.Drawing.Size(119, 153);
            this.grpHistoricalFigureKnowledge.TabIndex = 63;
            this.grpHistoricalFigureKnowledge.TabStop = false;
            this.grpHistoricalFigureKnowledge.Text = "Knowledge";
            // 
            // lstHistoricalFigureKnowledge
            // 
            this.lstHistoricalFigureKnowledge.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstHistoricalFigureKnowledge.FormattingEnabled = true;
            this.lstHistoricalFigureKnowledge.Location = new System.Drawing.Point(3, 16);
            this.lstHistoricalFigureKnowledge.Name = "lstHistoricalFigureKnowledge";
            this.lstHistoricalFigureKnowledge.Size = new System.Drawing.Size(113, 134);
            this.lstHistoricalFigureKnowledge.TabIndex = 0;
            // 
            // grpHistoricalFigurePets
            // 
            this.tableLayoutPanel23.SetColumnSpan(this.grpHistoricalFigurePets, 2);
            this.grpHistoricalFigurePets.Controls.Add(this.lstHistoricalFigurePets);
            this.grpHistoricalFigurePets.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpHistoricalFigurePets.Location = new System.Drawing.Point(3, 521);
            this.grpHistoricalFigurePets.Name = "grpHistoricalFigurePets";
            this.grpHistoricalFigurePets.Size = new System.Drawing.Size(244, 174);
            this.grpHistoricalFigurePets.TabIndex = 61;
            this.grpHistoricalFigurePets.TabStop = false;
            this.grpHistoricalFigurePets.Text = "Pets";
            // 
            // lstHistoricalFigurePets
            // 
            this.lstHistoricalFigurePets.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstHistoricalFigurePets.FormattingEnabled = true;
            this.lstHistoricalFigurePets.Location = new System.Drawing.Point(3, 16);
            this.lstHistoricalFigurePets.Name = "lstHistoricalFigurePets";
            this.lstHistoricalFigurePets.Size = new System.Drawing.Size(238, 155);
            this.lstHistoricalFigurePets.TabIndex = 0;
            // 
            // grpHistoricalFigureHFLinks
            // 
            this.grpHistoricalFigureHFLinks.Controls.Add(this.trvHistoricalFigureHFLinks);
            this.grpHistoricalFigureHFLinks.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpHistoricalFigureHFLinks.Location = new System.Drawing.Point(483, 3);
            this.grpHistoricalFigureHFLinks.Name = "grpHistoricalFigureHFLinks";
            this.tableLayoutPanel23.SetRowSpan(this.grpHistoricalFigureHFLinks, 3);
            this.grpHistoricalFigureHFLinks.Size = new System.Drawing.Size(599, 512);
            this.grpHistoricalFigureHFLinks.TabIndex = 57;
            this.grpHistoricalFigureHFLinks.TabStop = false;
            this.grpHistoricalFigureHFLinks.Text = "Related Historical Figures";
            // 
            // trvHistoricalFigureHFLinks
            // 
            this.trvHistoricalFigureHFLinks.Dock = System.Windows.Forms.DockStyle.Top;
            this.trvHistoricalFigureHFLinks.Location = new System.Drawing.Point(3, 16);
            this.trvHistoricalFigureHFLinks.Name = "trvHistoricalFigureHFLinks";
            this.trvHistoricalFigureHFLinks.Size = new System.Drawing.Size(593, 200);
            this.trvHistoricalFigureHFLinks.TabIndex = 0;
            this.trvHistoricalFigureHFLinks.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.trvHistoricalFigureHFLinks_AfterSelect);
            // 
            // grpHistoricalFigureEntityLinks
            // 
            this.grpHistoricalFigureEntityLinks.Controls.Add(this.trvHistoricalFigureEntityLinks);
            this.grpHistoricalFigureEntityLinks.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpHistoricalFigureEntityLinks.Location = new System.Drawing.Point(483, 521);
            this.grpHistoricalFigureEntityLinks.Name = "grpHistoricalFigureEntityLinks";
            this.grpHistoricalFigureEntityLinks.Size = new System.Drawing.Size(599, 174);
            this.grpHistoricalFigureEntityLinks.TabIndex = 58;
            this.grpHistoricalFigureEntityLinks.TabStop = false;
            this.grpHistoricalFigureEntityLinks.Text = "Related Entities";
            // 
            // trvHistoricalFigureEntityLinks
            // 
            this.trvHistoricalFigureEntityLinks.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trvHistoricalFigureEntityLinks.Location = new System.Drawing.Point(3, 16);
            this.trvHistoricalFigureEntityLinks.Name = "trvHistoricalFigureEntityLinks";
            this.trvHistoricalFigureEntityLinks.Size = new System.Drawing.Size(593, 155);
            this.trvHistoricalFigureEntityLinks.TabIndex = 0;
            // 
            // chkHistoricalFigureDetailedView
            // 
            this.chkHistoricalFigureDetailedView.AutoSize = true;
            this.chkHistoricalFigureDetailedView.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
            this.chkHistoricalFigureDetailedView.Checked = true;
            this.chkHistoricalFigureDetailedView.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkHistoricalFigureDetailedView.Location = new System.Drawing.Point(3, 818);
            this.chkHistoricalFigureDetailedView.Name = "chkHistoricalFigureDetailedView";
            this.chkHistoricalFigureDetailedView.Size = new System.Drawing.Size(99, 15);
            this.chkHistoricalFigureDetailedView.TabIndex = 6;
            this.chkHistoricalFigureDetailedView.Text = "Load All Details";
            this.chkHistoricalFigureDetailedView.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkHistoricalFigureDetailedView.UseVisualStyleBackColor = true;
            // 
            // tabHistoricalEra
            // 
            this.tabHistoricalEra.Controls.Add(this.tableLayoutPanel8);
            this.tabHistoricalEra.Location = new System.Drawing.Point(4, 40);
            this.tabHistoricalEra.Name = "tabHistoricalEra";
            this.tabHistoricalEra.Size = new System.Drawing.Size(1873, 873);
            this.tabHistoricalEra.TabIndex = 7;
            this.tabHistoricalEra.Text = "Historical Era";
            this.tabHistoricalEra.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel8
            // 
            this.tableLayoutPanel8.ColumnCount = 2;
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 171F));
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel8.Controls.Add(this.lstHistoricalEra, 0, 0);
            this.tableLayoutPanel8.Controls.Add(this.FilterHistoricalEra, 0, 2);
            this.tableLayoutPanel8.Controls.Add(this.TextFilterHistoricalEra, 0, 1);
            this.tableLayoutPanel8.Controls.Add(this.grpHistoricalEra, 1, 0);
            this.tableLayoutPanel8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel8.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel8.Margin = new System.Windows.Forms.Padding(2);
            this.tableLayoutPanel8.Name = "tableLayoutPanel8";
            this.tableLayoutPanel8.RowCount = 3;
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanel8.Size = new System.Drawing.Size(1873, 891);
            this.tableLayoutPanel8.TabIndex = 8;
            // 
            // lstHistoricalEra
            // 
            this.lstHistoricalEra.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstHistoricalEra.FormattingEnabled = true;
            this.lstHistoricalEra.Location = new System.Drawing.Point(3, 3);
            this.lstHistoricalEra.Name = "lstHistoricalEra";
            this.lstHistoricalEra.Size = new System.Drawing.Size(165, 833);
            this.lstHistoricalEra.TabIndex = 2;
            // 
            // FilterHistoricalEra
            // 
            this.FilterHistoricalEra.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FilterHistoricalEra.Location = new System.Drawing.Point(3, 866);
            this.FilterHistoricalEra.Name = "FilterHistoricalEra";
            this.FilterHistoricalEra.Size = new System.Drawing.Size(165, 22);
            this.FilterHistoricalEra.TabIndex = 6;
            this.FilterHistoricalEra.Tag = "";
            this.FilterHistoricalEra.Text = "Filter";
            this.FilterHistoricalEra.UseVisualStyleBackColor = true;
            this.FilterHistoricalEra.Click += new System.EventHandler(this.FilterButton_Click);
            // 
            // TextFilterHistoricalEra
            // 
            this.TextFilterHistoricalEra.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TextFilterHistoricalEra.Location = new System.Drawing.Point(3, 842);
            this.TextFilterHistoricalEra.Name = "TextFilterHistoricalEra";
            this.TextFilterHistoricalEra.Size = new System.Drawing.Size(165, 20);
            this.TextFilterHistoricalEra.TabIndex = 5;
            this.TextFilterHistoricalEra.TextChanged += new System.EventHandler(this.TextFilter_Changed);
            // 
            // grpHistoricalEra
            // 
            this.grpHistoricalEra.Controls.Add(this.lblHistoricalEraStartYear);
            this.grpHistoricalEra.Controls.Add(this.label52);
            this.grpHistoricalEra.Controls.Add(this.lblHistoricalEraName);
            this.grpHistoricalEra.Controls.Add(this.label54);
            this.grpHistoricalEra.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpHistoricalEra.Location = new System.Drawing.Point(174, 3);
            this.grpHistoricalEra.Name = "grpHistoricalEra";
            this.tableLayoutPanel8.SetRowSpan(this.grpHistoricalEra, 3);
            this.grpHistoricalEra.Size = new System.Drawing.Size(1696, 885);
            this.grpHistoricalEra.TabIndex = 3;
            this.grpHistoricalEra.TabStop = false;
            this.grpHistoricalEra.Visible = false;
            // 
            // lblHistoricalEraStartYear
            // 
            this.lblHistoricalEraStartYear.AutoSize = true;
            this.lblHistoricalEraStartYear.Location = new System.Drawing.Point(67, 44);
            this.lblHistoricalEraStartYear.Name = "lblHistoricalEraStartYear";
            this.lblHistoricalEraStartYear.Size = new System.Drawing.Size(35, 13);
            this.lblHistoricalEraStartYear.TabIndex = 22;
            this.lblHistoricalEraStartYear.Text = "label2";
            // 
            // label52
            // 
            this.label52.AutoSize = true;
            this.label52.Location = new System.Drawing.Point(12, 44);
            this.label52.Name = "label52";
            this.label52.Size = new System.Drawing.Size(57, 13);
            this.label52.TabIndex = 21;
            this.label52.Text = "Start Year:";
            // 
            // lblHistoricalEraName
            // 
            this.lblHistoricalEraName.AutoSize = true;
            this.lblHistoricalEraName.Location = new System.Drawing.Point(67, 22);
            this.lblHistoricalEraName.Name = "lblHistoricalEraName";
            this.lblHistoricalEraName.Size = new System.Drawing.Size(35, 13);
            this.lblHistoricalEraName.TabIndex = 20;
            this.lblHistoricalEraName.Text = "label2";
            // 
            // label54
            // 
            this.label54.AutoSize = true;
            this.label54.Location = new System.Drawing.Point(12, 21);
            this.label54.Name = "label54";
            this.label54.Size = new System.Drawing.Size(41, 13);
            this.label54.TabIndex = 19;
            this.label54.Text = "Name: ";
            // 
            // tabHistoricalEvent
            // 
            this.tabHistoricalEvent.Controls.Add(this.tableLayoutPanel9);
            this.tabHistoricalEvent.Location = new System.Drawing.Point(4, 40);
            this.tabHistoricalEvent.Name = "tabHistoricalEvent";
            this.tabHistoricalEvent.Size = new System.Drawing.Size(1873, 873);
            this.tabHistoricalEvent.TabIndex = 8;
            this.tabHistoricalEvent.Text = "Historical Event";
            this.tabHistoricalEvent.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel9
            // 
            this.tableLayoutPanel9.ColumnCount = 2;
            this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 171F));
            this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel9.Controls.Add(this.lstHistoricalEvent, 0, 0);
            this.tableLayoutPanel9.Controls.Add(this.TextFilterHistoricalEvent, 0, 1);
            this.tableLayoutPanel9.Controls.Add(this.grpHistoricalEvent, 1, 0);
            this.tableLayoutPanel9.Controls.Add(this.FilterHistoricalEvent, 0, 2);
            this.tableLayoutPanel9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel9.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel9.Margin = new System.Windows.Forms.Padding(2);
            this.tableLayoutPanel9.Name = "tableLayoutPanel9";
            this.tableLayoutPanel9.RowCount = 3;
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanel9.Size = new System.Drawing.Size(1873, 891);
            this.tableLayoutPanel9.TabIndex = 8;
            // 
            // lstHistoricalEvent
            // 
            this.lstHistoricalEvent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstHistoricalEvent.FormattingEnabled = true;
            this.lstHistoricalEvent.Location = new System.Drawing.Point(3, 3);
            this.lstHistoricalEvent.Name = "lstHistoricalEvent";
            this.lstHistoricalEvent.Size = new System.Drawing.Size(165, 833);
            this.lstHistoricalEvent.TabIndex = 2;
            // 
            // TextFilterHistoricalEvent
            // 
            this.TextFilterHistoricalEvent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TextFilterHistoricalEvent.Location = new System.Drawing.Point(3, 842);
            this.TextFilterHistoricalEvent.Name = "TextFilterHistoricalEvent";
            this.TextFilterHistoricalEvent.Size = new System.Drawing.Size(165, 20);
            this.TextFilterHistoricalEvent.TabIndex = 5;
            this.TextFilterHistoricalEvent.TextChanged += new System.EventHandler(this.TextFilter_Changed);
            // 
            // grpHistoricalEvent
            // 
            this.grpHistoricalEvent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpHistoricalEvent.Location = new System.Drawing.Point(174, 3);
            this.grpHistoricalEvent.Name = "grpHistoricalEvent";
            this.tableLayoutPanel9.SetRowSpan(this.grpHistoricalEvent, 3);
            this.grpHistoricalEvent.Size = new System.Drawing.Size(1696, 885);
            this.grpHistoricalEvent.TabIndex = 3;
            this.grpHistoricalEvent.TabStop = false;
            this.grpHistoricalEvent.Visible = false;
            // 
            // FilterHistoricalEvent
            // 
            this.FilterHistoricalEvent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FilterHistoricalEvent.Location = new System.Drawing.Point(3, 866);
            this.FilterHistoricalEvent.Name = "FilterHistoricalEvent";
            this.FilterHistoricalEvent.Size = new System.Drawing.Size(165, 22);
            this.FilterHistoricalEvent.TabIndex = 6;
            this.FilterHistoricalEvent.Tag = "";
            this.FilterHistoricalEvent.Text = "Filter";
            this.FilterHistoricalEvent.UseVisualStyleBackColor = true;
            this.FilterHistoricalEvent.Click += new System.EventHandler(this.FilterButton_Click);
            // 
            // tabHistoricalEventCollection
            // 
            this.tabHistoricalEventCollection.Controls.Add(this.tableLayoutPanel10);
            this.tabHistoricalEventCollection.Location = new System.Drawing.Point(4, 40);
            this.tabHistoricalEventCollection.Name = "tabHistoricalEventCollection";
            this.tabHistoricalEventCollection.Size = new System.Drawing.Size(1873, 873);
            this.tabHistoricalEventCollection.TabIndex = 9;
            this.tabHistoricalEventCollection.Text = "Historical Event Collection";
            this.tabHistoricalEventCollection.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel10
            // 
            this.tableLayoutPanel10.ColumnCount = 2;
            this.tableLayoutPanel10.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 161F));
            this.tableLayoutPanel10.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel10.Controls.Add(this.TextFilterHistoricalEventCollection, 0, 1);
            this.tableLayoutPanel10.Controls.Add(this.grpHistoricalEventCollection, 1, 0);
            this.tableLayoutPanel10.Controls.Add(this.FilterHistoricalEventCollection, 0, 2);
            this.tableLayoutPanel10.Controls.Add(this.lstHistoricalEventCollection, 0, 0);
            this.tableLayoutPanel10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel10.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel10.Margin = new System.Windows.Forms.Padding(2);
            this.tableLayoutPanel10.Name = "tableLayoutPanel10";
            this.tableLayoutPanel10.RowCount = 3;
            this.tableLayoutPanel10.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel10.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel10.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanel10.Size = new System.Drawing.Size(1873, 891);
            this.tableLayoutPanel10.TabIndex = 8;
            // 
            // TextFilterHistoricalEventCollection
            // 
            this.TextFilterHistoricalEventCollection.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TextFilterHistoricalEventCollection.Location = new System.Drawing.Point(3, 842);
            this.TextFilterHistoricalEventCollection.Name = "TextFilterHistoricalEventCollection";
            this.TextFilterHistoricalEventCollection.Size = new System.Drawing.Size(155, 20);
            this.TextFilterHistoricalEventCollection.TabIndex = 5;
            this.TextFilterHistoricalEventCollection.TextChanged += new System.EventHandler(this.TextFilter_Changed);
            // 
            // grpHistoricalEventCollection
            // 
            this.grpHistoricalEventCollection.Controls.Add(this.MainTabEventCollectionTypes);
            this.grpHistoricalEventCollection.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpHistoricalEventCollection.Location = new System.Drawing.Point(164, 3);
            this.grpHistoricalEventCollection.Name = "grpHistoricalEventCollection";
            this.tableLayoutPanel10.SetRowSpan(this.grpHistoricalEventCollection, 3);
            this.grpHistoricalEventCollection.Size = new System.Drawing.Size(1706, 885);
            this.grpHistoricalEventCollection.TabIndex = 3;
            this.grpHistoricalEventCollection.TabStop = false;
            this.grpHistoricalEventCollection.Visible = false;
            // 
            // MainTabEventCollectionTypes
            // 
            this.MainTabEventCollectionTypes.Controls.Add(this.tabEventCollectionJourney);
            this.MainTabEventCollectionTypes.Controls.Add(this.tabEventCollectionBeastAttack);
            this.MainTabEventCollectionTypes.Controls.Add(this.tabEventCollectionWar);
            this.MainTabEventCollectionTypes.Controls.Add(this.tabEventCollectionBattle);
            this.MainTabEventCollectionTypes.Controls.Add(this.tabEventCollectionDuel);
            this.MainTabEventCollectionTypes.Controls.Add(this.tabEventCollectionAbduction);
            this.MainTabEventCollectionTypes.Controls.Add(this.tabEventCollectionSiteConquered);
            this.MainTabEventCollectionTypes.Controls.Add(this.tabEventCollectionTheft);
            this.MainTabEventCollectionTypes.Controls.Add(this.tabEventCollectionInsurrection);
            this.MainTabEventCollectionTypes.Controls.Add(this.tabEventCollectionOccasion);
            this.MainTabEventCollectionTypes.Controls.Add(this.tabEventCollectionCeremony);
            this.MainTabEventCollectionTypes.Controls.Add(this.tabEventCollectionProcession);
            this.MainTabEventCollectionTypes.Controls.Add(this.tabEventCollectionPerformance);
            this.MainTabEventCollectionTypes.Controls.Add(this.tabEventCollectionCompetition);
            this.MainTabEventCollectionTypes.Controls.Add(this.tabEventCollectionPurge);
            this.MainTabEventCollectionTypes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainTabEventCollectionTypes.Location = new System.Drawing.Point(3, 16);
            this.MainTabEventCollectionTypes.Margin = new System.Windows.Forms.Padding(2);
            this.MainTabEventCollectionTypes.Name = "MainTabEventCollectionTypes";
            this.MainTabEventCollectionTypes.SelectedIndex = 0;
            this.MainTabEventCollectionTypes.Size = new System.Drawing.Size(1700, 866);
            this.MainTabEventCollectionTypes.TabIndex = 0;
            // 
            // tabEventCollectionJourney
            // 
            this.tabEventCollectionJourney.Controls.Add(this.tableLayoutPanel24);
            this.tabEventCollectionJourney.Location = new System.Drawing.Point(4, 22);
            this.tabEventCollectionJourney.Margin = new System.Windows.Forms.Padding(2);
            this.tabEventCollectionJourney.Name = "tabEventCollectionJourney";
            this.tabEventCollectionJourney.Size = new System.Drawing.Size(1692, 840);
            this.tabEventCollectionJourney.TabIndex = 0;
            this.tabEventCollectionJourney.Text = "Journey";
            this.tabEventCollectionJourney.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel24
            // 
            this.tableLayoutPanel24.ColumnCount = 2;
            this.tableLayoutPanel24.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tableLayoutPanel24.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel24.Controls.Add(this.panel7, 0, 0);
            this.tableLayoutPanel24.Controls.Add(this.grpJourneyEvents, 1, 0);
            this.tableLayoutPanel24.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel24.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel24.Name = "tableLayoutPanel24";
            this.tableLayoutPanel24.RowCount = 1;
            this.tableLayoutPanel24.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel24.Size = new System.Drawing.Size(1692, 840);
            this.tableLayoutPanel24.TabIndex = 133;
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.label162);
            this.panel7.Controls.Add(this.lblJourneyTime);
            this.panel7.Controls.Add(this.label147);
            this.panel7.Controls.Add(this.label158);
            this.panel7.Controls.Add(this.lblJourneyOrdinal);
            this.panel7.Controls.Add(this.lblJourneyDuration);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel7.Location = new System.Drawing.Point(3, 3);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(194, 834);
            this.panel7.TabIndex = 134;
            // 
            // label162
            // 
            this.label162.AutoSize = true;
            this.label162.Location = new System.Drawing.Point(3, 0);
            this.label162.Name = "label162";
            this.label162.Size = new System.Drawing.Size(33, 13);
            this.label162.TabIndex = 126;
            this.label162.Text = "Time:";
            // 
            // lblJourneyTime
            // 
            this.lblJourneyTime.AutoSize = true;
            this.lblJourneyTime.Location = new System.Drawing.Point(61, 0);
            this.lblJourneyTime.Name = "lblJourneyTime";
            this.lblJourneyTime.Size = new System.Drawing.Size(35, 13);
            this.lblJourneyTime.TabIndex = 127;
            this.lblJourneyTime.Text = "label2";
            // 
            // label147
            // 
            this.label147.AutoSize = true;
            this.label147.Location = new System.Drawing.Point(3, 44);
            this.label147.Name = "label147";
            this.label147.Size = new System.Drawing.Size(43, 13);
            this.label147.TabIndex = 132;
            this.label147.Text = "Ordinal:";
            // 
            // label158
            // 
            this.label158.AutoSize = true;
            this.label158.Location = new System.Drawing.Point(3, 21);
            this.label158.Name = "label158";
            this.label158.Size = new System.Drawing.Size(50, 13);
            this.label158.TabIndex = 128;
            this.label158.Text = "Duration:";
            // 
            // lblJourneyOrdinal
            // 
            this.lblJourneyOrdinal.AutoSize = true;
            this.lblJourneyOrdinal.Location = new System.Drawing.Point(61, 44);
            this.lblJourneyOrdinal.Name = "lblJourneyOrdinal";
            this.lblJourneyOrdinal.Size = new System.Drawing.Size(35, 13);
            this.lblJourneyOrdinal.TabIndex = 131;
            this.lblJourneyOrdinal.Text = "label2";
            // 
            // lblJourneyDuration
            // 
            this.lblJourneyDuration.AutoSize = true;
            this.lblJourneyDuration.Location = new System.Drawing.Point(61, 21);
            this.lblJourneyDuration.Name = "lblJourneyDuration";
            this.lblJourneyDuration.Size = new System.Drawing.Size(35, 13);
            this.lblJourneyDuration.TabIndex = 129;
            this.lblJourneyDuration.Text = "label2";
            // 
            // grpJourneyEvents
            // 
            this.grpJourneyEvents.Controls.Add(this.lstJourneyEvents);
            this.grpJourneyEvents.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpJourneyEvents.Location = new System.Drawing.Point(203, 3);
            this.grpJourneyEvents.Name = "grpJourneyEvents";
            this.grpJourneyEvents.Size = new System.Drawing.Size(1486, 834);
            this.grpJourneyEvents.TabIndex = 130;
            this.grpJourneyEvents.TabStop = false;
            this.grpJourneyEvents.Text = "Events";
            // 
            // lstJourneyEvents
            // 
            this.lstJourneyEvents.Dock = System.Windows.Forms.DockStyle.Top;
            this.lstJourneyEvents.FormattingEnabled = true;
            this.lstJourneyEvents.Location = new System.Drawing.Point(3, 16);
            this.lstJourneyEvents.Name = "lstJourneyEvents";
            this.lstJourneyEvents.Size = new System.Drawing.Size(1480, 147);
            this.lstJourneyEvents.TabIndex = 0;
            this.lstJourneyEvents.SelectedIndexChanged += new System.EventHandler(this.EventCollection_EventsListClick);
            this.lstJourneyEvents.DoubleClick += new System.EventHandler(this.SubListBoxDoubleClicked);
            // 
            // tabEventCollectionBeastAttack
            // 
            this.tabEventCollectionBeastAttack.Controls.Add(this.tableLayoutPanel25);
            this.tabEventCollectionBeastAttack.Location = new System.Drawing.Point(4, 22);
            this.tabEventCollectionBeastAttack.Margin = new System.Windows.Forms.Padding(2);
            this.tabEventCollectionBeastAttack.Name = "tabEventCollectionBeastAttack";
            this.tabEventCollectionBeastAttack.Size = new System.Drawing.Size(1692, 840);
            this.tabEventCollectionBeastAttack.TabIndex = 1;
            this.tabEventCollectionBeastAttack.Text = "Beast Attack";
            this.tabEventCollectionBeastAttack.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel25
            // 
            this.tableLayoutPanel25.ColumnCount = 2;
            this.tableLayoutPanel25.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tableLayoutPanel25.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel25.Controls.Add(this.panel8, 0, 0);
            this.tableLayoutPanel25.Controls.Add(this.grpBeastAttackEvents, 1, 0);
            this.tableLayoutPanel25.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel25.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel25.Name = "tableLayoutPanel25";
            this.tableLayoutPanel25.RowCount = 1;
            this.tableLayoutPanel25.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel25.Size = new System.Drawing.Size(1692, 822);
            this.tableLayoutPanel25.TabIndex = 130;
            // 
            // panel8
            // 
            this.panel8.Controls.Add(this.label159);
            this.panel8.Controls.Add(this.lblBeastAttackParent);
            this.panel8.Controls.Add(this.label168);
            this.panel8.Controls.Add(this.lblBeastAttackSite);
            this.panel8.Controls.Add(this.label137);
            this.panel8.Controls.Add(this.lblBeastAttackCoords);
            this.panel8.Controls.Add(this.label143);
            this.panel8.Controls.Add(this.lblBeastAttackDefender);
            this.panel8.Controls.Add(this.lblBeastAttackOrdinal);
            this.panel8.Controls.Add(this.lblBeastAttackRegion);
            this.panel8.Controls.Add(this.label152);
            this.panel8.Controls.Add(this.lblBeastAttackBeast);
            this.panel8.Controls.Add(this.label160);
            this.panel8.Controls.Add(this.lblBeastAttackDuration);
            this.panel8.Controls.Add(this.lblBeastAttackTime);
            this.panel8.Controls.Add(this.label155);
            this.panel8.Controls.Add(this.label157);
            this.panel8.Controls.Add(this.label156);
            this.panel8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel8.Location = new System.Drawing.Point(3, 3);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(194, 816);
            this.panel8.TabIndex = 131;
            // 
            // label159
            // 
            this.label159.AutoSize = true;
            this.label159.Location = new System.Drawing.Point(3, 2);
            this.label159.Name = "label159";
            this.label159.Size = new System.Drawing.Size(33, 13);
            this.label159.TabIndex = 111;
            this.label159.Text = "Time:";
            // 
            // label168
            // 
            this.label168.AutoSize = true;
            this.label168.Location = new System.Drawing.Point(3, 67);
            this.label168.Name = "label168";
            this.label168.Size = new System.Drawing.Size(37, 13);
            this.label168.TabIndex = 128;
            this.label168.Text = "Beast:";
            // 
            // label137
            // 
            this.label137.AutoSize = true;
            this.label137.Location = new System.Drawing.Point(3, 89);
            this.label137.Name = "label137";
            this.label137.Size = new System.Drawing.Size(44, 13);
            this.label137.TabIndex = 126;
            this.label137.Text = "Region:";
            // 
            // label143
            // 
            this.label143.AutoSize = true;
            this.label143.Location = new System.Drawing.Point(1, 176);
            this.label143.Name = "label143";
            this.label143.Size = new System.Drawing.Size(43, 13);
            this.label143.TabIndex = 125;
            this.label143.Text = "Ordinal:";
            // 
            // lblBeastAttackOrdinal
            // 
            this.lblBeastAttackOrdinal.AutoSize = true;
            this.lblBeastAttackOrdinal.Location = new System.Drawing.Point(59, 176);
            this.lblBeastAttackOrdinal.Name = "lblBeastAttackOrdinal";
            this.lblBeastAttackOrdinal.Size = new System.Drawing.Size(35, 13);
            this.lblBeastAttackOrdinal.TabIndex = 124;
            this.lblBeastAttackOrdinal.Text = "label2";
            // 
            // label152
            // 
            this.label152.AutoSize = true;
            this.label152.Location = new System.Drawing.Point(1, 154);
            this.label152.Name = "label152";
            this.label152.Size = new System.Drawing.Size(54, 13);
            this.label152.TabIndex = 122;
            this.label152.Text = "Defender:";
            // 
            // label160
            // 
            this.label160.AutoSize = true;
            this.label160.Location = new System.Drawing.Point(3, 46);
            this.label160.Name = "label160";
            this.label160.Size = new System.Drawing.Size(41, 13);
            this.label160.TabIndex = 109;
            this.label160.Text = "Parent:";
            // 
            // lblBeastAttackDuration
            // 
            this.lblBeastAttackDuration.AutoSize = true;
            this.lblBeastAttackDuration.Location = new System.Drawing.Point(61, 23);
            this.lblBeastAttackDuration.Name = "lblBeastAttackDuration";
            this.lblBeastAttackDuration.Size = new System.Drawing.Size(35, 13);
            this.lblBeastAttackDuration.TabIndex = 118;
            this.lblBeastAttackDuration.Text = "label2";
            // 
            // lblBeastAttackTime
            // 
            this.lblBeastAttackTime.AutoSize = true;
            this.lblBeastAttackTime.Location = new System.Drawing.Point(61, 2);
            this.lblBeastAttackTime.Name = "lblBeastAttackTime";
            this.lblBeastAttackTime.Size = new System.Drawing.Size(35, 13);
            this.lblBeastAttackTime.TabIndex = 112;
            this.lblBeastAttackTime.Text = "label2";
            // 
            // label155
            // 
            this.label155.AutoSize = true;
            this.label155.Location = new System.Drawing.Point(3, 23);
            this.label155.Name = "label155";
            this.label155.Size = new System.Drawing.Size(50, 13);
            this.label155.TabIndex = 117;
            this.label155.Text = "Duration:";
            // 
            // label157
            // 
            this.label157.AutoSize = true;
            this.label157.Location = new System.Drawing.Point(2, 111);
            this.label157.Name = "label157";
            this.label157.Size = new System.Drawing.Size(28, 13);
            this.label157.TabIndex = 113;
            this.label157.Text = "Site:";
            // 
            // label156
            // 
            this.label156.AutoSize = true;
            this.label156.Location = new System.Drawing.Point(1, 133);
            this.label156.Name = "label156";
            this.label156.Size = new System.Drawing.Size(43, 13);
            this.label156.TabIndex = 115;
            this.label156.Text = "Coords:";
            // 
            // grpBeastAttackEvents
            // 
            this.grpBeastAttackEvents.Controls.Add(this.lstBeastAttackEvents);
            this.grpBeastAttackEvents.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpBeastAttackEvents.Location = new System.Drawing.Point(203, 3);
            this.grpBeastAttackEvents.Name = "grpBeastAttackEvents";
            this.grpBeastAttackEvents.Size = new System.Drawing.Size(1486, 816);
            this.grpBeastAttackEvents.TabIndex = 119;
            this.grpBeastAttackEvents.TabStop = false;
            this.grpBeastAttackEvents.Text = "Events";
            // 
            // lstBeastAttackEvents
            // 
            this.lstBeastAttackEvents.Dock = System.Windows.Forms.DockStyle.Top;
            this.lstBeastAttackEvents.FormattingEnabled = true;
            this.lstBeastAttackEvents.Location = new System.Drawing.Point(3, 16);
            this.lstBeastAttackEvents.Name = "lstBeastAttackEvents";
            this.lstBeastAttackEvents.Size = new System.Drawing.Size(1480, 147);
            this.lstBeastAttackEvents.TabIndex = 0;
            this.lstBeastAttackEvents.SelectedIndexChanged += new System.EventHandler(this.EventCollection_EventsListClick);
            this.lstBeastAttackEvents.DoubleClick += new System.EventHandler(this.SubListBoxDoubleClicked);
            // 
            // tabEventCollectionWar
            // 
            this.tabEventCollectionWar.Controls.Add(this.tableLayoutPanel26);
            this.tabEventCollectionWar.Location = new System.Drawing.Point(4, 22);
            this.tabEventCollectionWar.Margin = new System.Windows.Forms.Padding(2);
            this.tabEventCollectionWar.Name = "tabEventCollectionWar";
            this.tabEventCollectionWar.Size = new System.Drawing.Size(1692, 840);
            this.tabEventCollectionWar.TabIndex = 2;
            this.tabEventCollectionWar.Text = "War";
            this.tabEventCollectionWar.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel26
            // 
            this.tableLayoutPanel26.ColumnCount = 2;
            this.tableLayoutPanel26.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 266F));
            this.tableLayoutPanel26.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel26.Controls.Add(this.panel9, 0, 0);
            this.tableLayoutPanel26.Controls.Add(this.groupBox4, 0, 2);
            this.tableLayoutPanel26.Controls.Add(this.grpWarEventCols, 1, 2);
            this.tableLayoutPanel26.Controls.Add(this.groupBox3, 0, 1);
            this.tableLayoutPanel26.Controls.Add(this.grpWarEvents, 1, 0);
            this.tableLayoutPanel26.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel26.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel26.Name = "tableLayoutPanel26";
            this.tableLayoutPanel26.RowCount = 3;
            this.tableLayoutPanel26.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 77F));
            this.tableLayoutPanel26.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel26.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel26.Size = new System.Drawing.Size(1692, 822);
            this.tableLayoutPanel26.TabIndex = 58;
            // 
            // panel9
            // 
            this.panel9.Controls.Add(this.label103);
            this.panel9.Controls.Add(this.lblWarName);
            this.panel9.Controls.Add(this.label101);
            this.panel9.Controls.Add(this.lblWarTime);
            this.panel9.Controls.Add(this.label99);
            this.panel9.Controls.Add(this.lblWarDuration);
            this.panel9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel9.Location = new System.Drawing.Point(3, 3);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(260, 71);
            this.panel9.TabIndex = 59;
            // 
            // label103
            // 
            this.label103.AutoSize = true;
            this.label103.Location = new System.Drawing.Point(3, 0);
            this.label103.Name = "label103";
            this.label103.Size = new System.Drawing.Size(41, 13);
            this.label103.TabIndex = 44;
            this.label103.Text = "Name: ";
            // 
            // lblWarName
            // 
            this.lblWarName.AutoSize = true;
            this.lblWarName.Location = new System.Drawing.Point(61, 0);
            this.lblWarName.Name = "lblWarName";
            this.lblWarName.Size = new System.Drawing.Size(35, 13);
            this.lblWarName.TabIndex = 45;
            this.lblWarName.Text = "label2";
            // 
            // label101
            // 
            this.label101.AutoSize = true;
            this.label101.Location = new System.Drawing.Point(3, 24);
            this.label101.Name = "label101";
            this.label101.Size = new System.Drawing.Size(33, 13);
            this.label101.TabIndex = 46;
            this.label101.Text = "Time:";
            // 
            // lblWarTime
            // 
            this.lblWarTime.AutoSize = true;
            this.lblWarTime.Location = new System.Drawing.Point(61, 24);
            this.lblWarTime.Name = "lblWarTime";
            this.lblWarTime.Size = new System.Drawing.Size(35, 13);
            this.lblWarTime.TabIndex = 47;
            this.lblWarTime.Text = "label2";
            // 
            // label99
            // 
            this.label99.AutoSize = true;
            this.label99.Location = new System.Drawing.Point(4, 45);
            this.label99.Name = "label99";
            this.label99.Size = new System.Drawing.Size(50, 13);
            this.label99.TabIndex = 48;
            this.label99.Text = "Duration:";
            // 
            // lblWarDuration
            // 
            this.lblWarDuration.AutoSize = true;
            this.lblWarDuration.Location = new System.Drawing.Point(62, 45);
            this.lblWarDuration.Name = "lblWarDuration";
            this.lblWarDuration.Size = new System.Drawing.Size(35, 13);
            this.lblWarDuration.TabIndex = 49;
            this.lblWarDuration.Text = "label2";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.lblWarDefenderWins);
            this.groupBox4.Controls.Add(this.label124);
            this.groupBox4.Controls.Add(this.lblWarDefenderSquads);
            this.groupBox4.Controls.Add(this.label126);
            this.groupBox4.Controls.Add(this.lblWarDefenderLosses);
            this.groupBox4.Controls.Add(this.lblWarDefenderCombatants);
            this.groupBox4.Controls.Add(this.label129);
            this.groupBox4.Controls.Add(this.label130);
            this.groupBox4.Controls.Add(this.lblWarDefender);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox4.Location = new System.Drawing.Point(2, 451);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox4.Size = new System.Drawing.Size(262, 369);
            this.groupBox4.TabIndex = 57;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Defender";
            // 
            // lblWarDefenderWins
            // 
            this.lblWarDefenderWins.AutoSize = true;
            this.lblWarDefenderWins.Location = new System.Drawing.Point(71, 128);
            this.lblWarDefenderWins.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblWarDefenderWins.Name = "lblWarDefenderWins";
            this.lblWarDefenderWins.Size = new System.Drawing.Size(47, 13);
            this.lblWarDefenderWins.TabIndex = 70;
            this.lblWarDefenderWins.Text = "label116";
            // 
            // label124
            // 
            this.label124.AutoSize = true;
            this.label124.Location = new System.Drawing.Point(5, 128);
            this.label124.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label124.Name = "label124";
            this.label124.Size = new System.Drawing.Size(50, 13);
            this.label124.TabIndex = 69;
            this.label124.Text = "Victories:";
            // 
            // lblWarDefenderSquads
            // 
            this.lblWarDefenderSquads.AutoSize = true;
            this.lblWarDefenderSquads.Location = new System.Drawing.Point(71, 106);
            this.lblWarDefenderSquads.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblWarDefenderSquads.Name = "lblWarDefenderSquads";
            this.lblWarDefenderSquads.Size = new System.Drawing.Size(47, 13);
            this.lblWarDefenderSquads.TabIndex = 68;
            this.lblWarDefenderSquads.Text = "label116";
            // 
            // label126
            // 
            this.label126.AutoSize = true;
            this.label126.Location = new System.Drawing.Point(5, 106);
            this.label126.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label126.Name = "label126";
            this.label126.Size = new System.Drawing.Size(46, 13);
            this.label126.TabIndex = 67;
            this.label126.Text = "Squads:";
            // 
            // lblWarDefenderLosses
            // 
            this.lblWarDefenderLosses.AutoSize = true;
            this.lblWarDefenderLosses.Location = new System.Drawing.Point(71, 85);
            this.lblWarDefenderLosses.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblWarDefenderLosses.Name = "lblWarDefenderLosses";
            this.lblWarDefenderLosses.Size = new System.Drawing.Size(47, 13);
            this.lblWarDefenderLosses.TabIndex = 66;
            this.lblWarDefenderLosses.Text = "label116";
            // 
            // lblWarDefenderCombatants
            // 
            this.lblWarDefenderCombatants.AutoSize = true;
            this.lblWarDefenderCombatants.Location = new System.Drawing.Point(71, 63);
            this.lblWarDefenderCombatants.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblWarDefenderCombatants.Name = "lblWarDefenderCombatants";
            this.lblWarDefenderCombatants.Size = new System.Drawing.Size(47, 13);
            this.lblWarDefenderCombatants.TabIndex = 65;
            this.lblWarDefenderCombatants.Text = "label115";
            // 
            // label129
            // 
            this.label129.AutoSize = true;
            this.label129.Location = new System.Drawing.Point(5, 85);
            this.label129.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label129.Name = "label129";
            this.label129.Size = new System.Drawing.Size(43, 13);
            this.label129.TabIndex = 64;
            this.label129.Text = "Losses:";
            // 
            // label130
            // 
            this.label130.AutoSize = true;
            this.label130.Location = new System.Drawing.Point(5, 63);
            this.label130.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label130.Name = "label130";
            this.label130.Size = new System.Drawing.Size(66, 13);
            this.label130.TabIndex = 63;
            this.label130.Text = "Combatants:";
            // 
            // grpWarEventCols
            // 
            this.grpWarEventCols.Controls.Add(this.lstWarEventCols);
            this.grpWarEventCols.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpWarEventCols.Location = new System.Drawing.Point(269, 452);
            this.grpWarEventCols.Name = "grpWarEventCols";
            this.grpWarEventCols.Size = new System.Drawing.Size(1420, 367);
            this.grpWarEventCols.TabIndex = 51;
            this.grpWarEventCols.TabStop = false;
            this.grpWarEventCols.Text = "Event Collections";
            // 
            // lstWarEventCols
            // 
            this.lstWarEventCols.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstWarEventCols.FormattingEnabled = true;
            this.lstWarEventCols.Location = new System.Drawing.Point(3, 16);
            this.lstWarEventCols.Name = "lstWarEventCols";
            this.lstWarEventCols.Size = new System.Drawing.Size(1414, 348);
            this.lstWarEventCols.TabIndex = 0;
            this.lstWarEventCols.DoubleClick += new System.EventHandler(this.SubListBoxDoubleClicked);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.lblWarAggressorWins);
            this.groupBox3.Controls.Add(this.label122);
            this.groupBox3.Controls.Add(this.lblWarAggressorSquads);
            this.groupBox3.Controls.Add(this.label120);
            this.groupBox3.Controls.Add(this.lblWarAggressorLosses);
            this.groupBox3.Controls.Add(this.lblWarAggressorCombatants);
            this.groupBox3.Controls.Add(this.label117);
            this.groupBox3.Controls.Add(this.label118);
            this.groupBox3.Controls.Add(this.lblWarAggressor);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(2, 79);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox3.Size = new System.Drawing.Size(262, 368);
            this.groupBox3.TabIndex = 56;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Aggressor";
            // 
            // lblWarAggressorWins
            // 
            this.lblWarAggressorWins.AutoSize = true;
            this.lblWarAggressorWins.Location = new System.Drawing.Point(70, 127);
            this.lblWarAggressorWins.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblWarAggressorWins.Name = "lblWarAggressorWins";
            this.lblWarAggressorWins.Size = new System.Drawing.Size(47, 13);
            this.lblWarAggressorWins.TabIndex = 62;
            this.lblWarAggressorWins.Text = "label116";
            // 
            // label122
            // 
            this.label122.AutoSize = true;
            this.label122.Location = new System.Drawing.Point(4, 127);
            this.label122.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label122.Name = "label122";
            this.label122.Size = new System.Drawing.Size(50, 13);
            this.label122.TabIndex = 61;
            this.label122.Text = "Victories:";
            // 
            // lblWarAggressorSquads
            // 
            this.lblWarAggressorSquads.AutoSize = true;
            this.lblWarAggressorSquads.Location = new System.Drawing.Point(70, 106);
            this.lblWarAggressorSquads.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblWarAggressorSquads.Name = "lblWarAggressorSquads";
            this.lblWarAggressorSquads.Size = new System.Drawing.Size(47, 13);
            this.lblWarAggressorSquads.TabIndex = 60;
            this.lblWarAggressorSquads.Text = "label116";
            // 
            // label120
            // 
            this.label120.AutoSize = true;
            this.label120.Location = new System.Drawing.Point(4, 106);
            this.label120.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label120.Name = "label120";
            this.label120.Size = new System.Drawing.Size(46, 13);
            this.label120.TabIndex = 59;
            this.label120.Text = "Squads:";
            // 
            // lblWarAggressorLosses
            // 
            this.lblWarAggressorLosses.AutoSize = true;
            this.lblWarAggressorLosses.Location = new System.Drawing.Point(70, 84);
            this.lblWarAggressorLosses.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblWarAggressorLosses.Name = "lblWarAggressorLosses";
            this.lblWarAggressorLosses.Size = new System.Drawing.Size(47, 13);
            this.lblWarAggressorLosses.TabIndex = 58;
            this.lblWarAggressorLosses.Text = "label116";
            // 
            // lblWarAggressorCombatants
            // 
            this.lblWarAggressorCombatants.AutoSize = true;
            this.lblWarAggressorCombatants.Location = new System.Drawing.Point(70, 63);
            this.lblWarAggressorCombatants.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblWarAggressorCombatants.Name = "lblWarAggressorCombatants";
            this.lblWarAggressorCombatants.Size = new System.Drawing.Size(47, 13);
            this.lblWarAggressorCombatants.TabIndex = 57;
            this.lblWarAggressorCombatants.Text = "label115";
            // 
            // label117
            // 
            this.label117.AutoSize = true;
            this.label117.Location = new System.Drawing.Point(4, 84);
            this.label117.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label117.Name = "label117";
            this.label117.Size = new System.Drawing.Size(43, 13);
            this.label117.TabIndex = 56;
            this.label117.Text = "Losses:";
            // 
            // label118
            // 
            this.label118.AutoSize = true;
            this.label118.Location = new System.Drawing.Point(4, 63);
            this.label118.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label118.Name = "label118";
            this.label118.Size = new System.Drawing.Size(66, 13);
            this.label118.TabIndex = 55;
            this.label118.Text = "Combatants:";
            // 
            // grpWarEvents
            // 
            this.grpWarEvents.Controls.Add(this.lstWarEvents);
            this.grpWarEvents.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpWarEvents.Location = new System.Drawing.Point(269, 3);
            this.grpWarEvents.Name = "grpWarEvents";
            this.tableLayoutPanel26.SetRowSpan(this.grpWarEvents, 2);
            this.grpWarEvents.Size = new System.Drawing.Size(1420, 443);
            this.grpWarEvents.TabIndex = 50;
            this.grpWarEvents.TabStop = false;
            this.grpWarEvents.Text = "Events";
            // 
            // lstWarEvents
            // 
            this.lstWarEvents.Dock = System.Windows.Forms.DockStyle.Top;
            this.lstWarEvents.FormattingEnabled = true;
            this.lstWarEvents.Location = new System.Drawing.Point(3, 16);
            this.lstWarEvents.Name = "lstWarEvents";
            this.lstWarEvents.Size = new System.Drawing.Size(1414, 147);
            this.lstWarEvents.TabIndex = 0;
            this.lstWarEvents.SelectedIndexChanged += new System.EventHandler(this.EventCollection_EventsListClick);
            this.lstWarEvents.DoubleClick += new System.EventHandler(this.SubListBoxDoubleClicked);
            // 
            // tabEventCollectionBattle
            // 
            this.tabEventCollectionBattle.Controls.Add(this.tableLayoutPanel27);
            this.tabEventCollectionBattle.Location = new System.Drawing.Point(4, 22);
            this.tabEventCollectionBattle.Margin = new System.Windows.Forms.Padding(2);
            this.tabEventCollectionBattle.Name = "tabEventCollectionBattle";
            this.tabEventCollectionBattle.Size = new System.Drawing.Size(1692, 840);
            this.tabEventCollectionBattle.TabIndex = 3;
            this.tabEventCollectionBattle.Text = "Battle";
            this.tabEventCollectionBattle.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel27
            // 
            this.tableLayoutPanel27.ColumnCount = 4;
            this.tableLayoutPanel27.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.5824F));
            this.tableLayoutPanel27.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 28.67703F));
            this.tableLayoutPanel27.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.37028F));
            this.tableLayoutPanel27.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.37028F));
            this.tableLayoutPanel27.Controls.Add(this.panel10, 0, 0);
            this.tableLayoutPanel27.Controls.Add(this.groupBox5, 3, 0);
            this.tableLayoutPanel27.Controls.Add(this.grpBattleEvents, 1, 1);
            this.tableLayoutPanel27.Controls.Add(this.groupBox2, 2, 0);
            this.tableLayoutPanel27.Controls.Add(this.grpBattleEventCols, 0, 1);
            this.tableLayoutPanel27.Controls.Add(this.grpBattleNonComHFs, 0, 2);
            this.tableLayoutPanel27.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel27.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel27.Name = "tableLayoutPanel27";
            this.tableLayoutPanel27.RowCount = 3;
            this.tableLayoutPanel27.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 29.16667F));
            this.tableLayoutPanel27.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 46.73913F));
            this.tableLayoutPanel27.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 24.0942F));
            this.tableLayoutPanel27.Size = new System.Drawing.Size(1692, 822);
            this.tableLayoutPanel27.TabIndex = 49;
            // 
            // panel10
            // 
            this.tableLayoutPanel27.SetColumnSpan(this.panel10, 2);
            this.panel10.Controls.Add(this.label96);
            this.panel10.Controls.Add(this.lblBattleWar);
            this.panel10.Controls.Add(this.lblBattleRegion);
            this.panel10.Controls.Add(this.lblBattleSite);
            this.panel10.Controls.Add(this.lblBattleCoord);
            this.panel10.Controls.Add(this.lblBattleName);
            this.panel10.Controls.Add(this.label94);
            this.panel10.Controls.Add(this.lblBattleDuration);
            this.panel10.Controls.Add(this.label92);
            this.panel10.Controls.Add(this.label98);
            this.panel10.Controls.Add(this.lblBattleTime);
            this.panel10.Controls.Add(this.label97);
            this.panel10.Controls.Add(this.label95);
            this.panel10.Controls.Add(this.label90);
            this.panel10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel10.Location = new System.Drawing.Point(3, 3);
            this.panel10.Name = "panel10";
            this.panel10.Size = new System.Drawing.Size(827, 233);
            this.panel10.TabIndex = 50;
            // 
            // label96
            // 
            this.label96.AutoSize = true;
            this.label96.Location = new System.Drawing.Point(3, 0);
            this.label96.Name = "label96";
            this.label96.Size = new System.Drawing.Size(41, 13);
            this.label96.TabIndex = 28;
            this.label96.Text = "Name: ";
            // 
            // lblBattleName
            // 
            this.lblBattleName.AutoSize = true;
            this.lblBattleName.Location = new System.Drawing.Point(61, 0);
            this.lblBattleName.Name = "lblBattleName";
            this.lblBattleName.Size = new System.Drawing.Size(35, 13);
            this.lblBattleName.TabIndex = 29;
            this.lblBattleName.Text = "label2";
            // 
            // label94
            // 
            this.label94.AutoSize = true;
            this.label94.Location = new System.Drawing.Point(3, 68);
            this.label94.Name = "label94";
            this.label94.Size = new System.Drawing.Size(30, 13);
            this.label94.TabIndex = 30;
            this.label94.Text = "War:";
            // 
            // lblBattleDuration
            // 
            this.lblBattleDuration.AutoSize = true;
            this.lblBattleDuration.Location = new System.Drawing.Point(61, 45);
            this.lblBattleDuration.Name = "lblBattleDuration";
            this.lblBattleDuration.Size = new System.Drawing.Size(35, 13);
            this.lblBattleDuration.TabIndex = 43;
            this.lblBattleDuration.Text = "label2";
            // 
            // label92
            // 
            this.label92.AutoSize = true;
            this.label92.Location = new System.Drawing.Point(3, 24);
            this.label92.Name = "label92";
            this.label92.Size = new System.Drawing.Size(33, 13);
            this.label92.TabIndex = 34;
            this.label92.Text = "Time:";
            // 
            // label98
            // 
            this.label98.AutoSize = true;
            this.label98.Location = new System.Drawing.Point(3, 45);
            this.label98.Name = "label98";
            this.label98.Size = new System.Drawing.Size(50, 13);
            this.label98.TabIndex = 42;
            this.label98.Text = "Duration:";
            // 
            // lblBattleTime
            // 
            this.lblBattleTime.AutoSize = true;
            this.lblBattleTime.Location = new System.Drawing.Point(61, 24);
            this.lblBattleTime.Name = "lblBattleTime";
            this.lblBattleTime.Size = new System.Drawing.Size(35, 13);
            this.lblBattleTime.TabIndex = 35;
            this.lblBattleTime.Text = "label2";
            // 
            // label97
            // 
            this.label97.AutoSize = true;
            this.label97.Location = new System.Drawing.Point(3, 134);
            this.label97.Name = "label97";
            this.label97.Size = new System.Drawing.Size(43, 13);
            this.label97.TabIndex = 40;
            this.label97.Text = "Coords:";
            // 
            // label95
            // 
            this.label95.AutoSize = true;
            this.label95.Location = new System.Drawing.Point(4, 90);
            this.label95.Name = "label95";
            this.label95.Size = new System.Drawing.Size(44, 13);
            this.label95.TabIndex = 36;
            this.label95.Text = "Region:";
            // 
            // label90
            // 
            this.label90.AutoSize = true;
            this.label90.Location = new System.Drawing.Point(4, 112);
            this.label90.Name = "label90";
            this.label90.Size = new System.Drawing.Size(28, 13);
            this.label90.TabIndex = 38;
            this.label90.Text = "Site:";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.lblBattleDefenderLosses);
            this.groupBox5.Controls.Add(this.lblBattleDefenderCombatants);
            this.groupBox5.Controls.Add(this.label113);
            this.groupBox5.Controls.Add(this.label114);
            this.groupBox5.Controls.Add(this.lblBattleDefenderOutcome);
            this.groupBox5.Controls.Add(this.grpBattleDefendingSquad);
            this.groupBox5.Controls.Add(this.grpBattleDefendingHF);
            this.groupBox5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox5.Location = new System.Drawing.Point(1264, 2);
            this.groupBox5.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Padding = new System.Windows.Forms.Padding(2);
            this.tableLayoutPanel27.SetRowSpan(this.groupBox5, 3);
            this.groupBox5.Size = new System.Drawing.Size(426, 818);
            this.groupBox5.TabIndex = 45;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Defenders";
            // 
            // lblBattleDefenderLosses
            // 
            this.lblBattleDefenderLosses.AutoSize = true;
            this.lblBattleDefenderLosses.Location = new System.Drawing.Point(68, 89);
            this.lblBattleDefenderLosses.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblBattleDefenderLosses.Name = "lblBattleDefenderLosses";
            this.lblBattleDefenderLosses.Size = new System.Drawing.Size(47, 13);
            this.lblBattleDefenderLosses.TabIndex = 8;
            this.lblBattleDefenderLosses.Text = "label117";
            // 
            // lblBattleDefenderCombatants
            // 
            this.lblBattleDefenderCombatants.AutoSize = true;
            this.lblBattleDefenderCombatants.Location = new System.Drawing.Point(68, 67);
            this.lblBattleDefenderCombatants.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblBattleDefenderCombatants.Name = "lblBattleDefenderCombatants";
            this.lblBattleDefenderCombatants.Size = new System.Drawing.Size(47, 13);
            this.lblBattleDefenderCombatants.TabIndex = 7;
            this.lblBattleDefenderCombatants.Text = "label118";
            // 
            // label113
            // 
            this.label113.AutoSize = true;
            this.label113.Location = new System.Drawing.Point(4, 89);
            this.label113.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label113.Name = "label113";
            this.label113.Size = new System.Drawing.Size(43, 13);
            this.label113.TabIndex = 6;
            this.label113.Text = "Losses:";
            // 
            // label114
            // 
            this.label114.AutoSize = true;
            this.label114.Location = new System.Drawing.Point(4, 67);
            this.label114.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label114.Name = "label114";
            this.label114.Size = new System.Drawing.Size(66, 13);
            this.label114.TabIndex = 5;
            this.label114.Text = "Combatants:";
            // 
            // lblBattleDefenderOutcome
            // 
            this.lblBattleDefenderOutcome.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblBattleDefenderOutcome.Location = new System.Drawing.Point(7, 15);
            this.lblBattleDefenderOutcome.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblBattleDefenderOutcome.Name = "lblBattleDefenderOutcome";
            this.lblBattleDefenderOutcome.Size = new System.Drawing.Size(413, 14);
            this.lblBattleDefenderOutcome.TabIndex = 3;
            this.lblBattleDefenderOutcome.Text = "label108";
            this.lblBattleDefenderOutcome.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // grpBattleDefendingSquad
            // 
            this.grpBattleDefendingSquad.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpBattleDefendingSquad.Controls.Add(this.lblBattleDefendingSquadDeaths);
            this.grpBattleDefendingSquad.Controls.Add(this.label106);
            this.grpBattleDefendingSquad.Controls.Add(this.lblBattleDefendingSquadNumber);
            this.grpBattleDefendingSquad.Controls.Add(this.label109);
            this.grpBattleDefendingSquad.Controls.Add(this.label110);
            this.grpBattleDefendingSquad.Controls.Add(this.label111);
            this.grpBattleDefendingSquad.Controls.Add(this.label112);
            this.grpBattleDefendingSquad.Controls.Add(this.lblBattleDefendingSquadRace);
            this.grpBattleDefendingSquad.Controls.Add(this.lblBattleDefendingSquadEntPop);
            this.grpBattleDefendingSquad.Controls.Add(this.lblBattleDefendingSquadSite);
            this.grpBattleDefendingSquad.Controls.Add(this.lstBattleDefendingSquad);
            this.grpBattleDefendingSquad.Location = new System.Drawing.Point(4, 287);
            this.grpBattleDefendingSquad.Margin = new System.Windows.Forms.Padding(2);
            this.grpBattleDefendingSquad.Name = "grpBattleDefendingSquad";
            this.grpBattleDefendingSquad.Padding = new System.Windows.Forms.Padding(2);
            this.grpBattleDefendingSquad.Size = new System.Drawing.Size(416, 264);
            this.grpBattleDefendingSquad.TabIndex = 1;
            this.grpBattleDefendingSquad.TabStop = false;
            this.grpBattleDefendingSquad.Text = "Squads";
            // 
            // lblBattleDefendingSquadDeaths
            // 
            this.lblBattleDefendingSquadDeaths.AutoSize = true;
            this.lblBattleDefendingSquadDeaths.Location = new System.Drawing.Point(64, 240);
            this.lblBattleDefendingSquadDeaths.Name = "lblBattleDefendingSquadDeaths";
            this.lblBattleDefendingSquadDeaths.Size = new System.Drawing.Size(35, 13);
            this.lblBattleDefendingSquadDeaths.TabIndex = 51;
            this.lblBattleDefendingSquadDeaths.Text = "label2";
            // 
            // label106
            // 
            this.label106.AutoSize = true;
            this.label106.Location = new System.Drawing.Point(6, 240);
            this.label106.Name = "label106";
            this.label106.Size = new System.Drawing.Size(44, 13);
            this.label106.TabIndex = 50;
            this.label106.Text = "Deaths:";
            // 
            // lblBattleDefendingSquadNumber
            // 
            this.lblBattleDefendingSquadNumber.AutoSize = true;
            this.lblBattleDefendingSquadNumber.Location = new System.Drawing.Point(63, 219);
            this.lblBattleDefendingSquadNumber.Name = "lblBattleDefendingSquadNumber";
            this.lblBattleDefendingSquadNumber.Size = new System.Drawing.Size(35, 13);
            this.lblBattleDefendingSquadNumber.TabIndex = 49;
            this.lblBattleDefendingSquadNumber.Text = "label2";
            // 
            // label109
            // 
            this.label109.AutoSize = true;
            this.label109.Location = new System.Drawing.Point(5, 219);
            this.label109.Name = "label109";
            this.label109.Size = new System.Drawing.Size(47, 13);
            this.label109.TabIndex = 48;
            this.label109.Text = "Number:";
            // 
            // label110
            // 
            this.label110.AutoSize = true;
            this.label110.Location = new System.Drawing.Point(4, 197);
            this.label110.Name = "label110";
            this.label110.Size = new System.Drawing.Size(36, 13);
            this.label110.TabIndex = 46;
            this.label110.Text = "Race:";
            // 
            // label111
            // 
            this.label111.AutoSize = true;
            this.label111.Location = new System.Drawing.Point(5, 176);
            this.label111.Name = "label111";
            this.label111.Size = new System.Drawing.Size(48, 13);
            this.label111.TabIndex = 44;
            this.label111.Text = "Ent Pop:";
            // 
            // label112
            // 
            this.label112.AutoSize = true;
            this.label112.Location = new System.Drawing.Point(5, 154);
            this.label112.Name = "label112";
            this.label112.Size = new System.Drawing.Size(28, 13);
            this.label112.TabIndex = 42;
            this.label112.Text = "Site:";
            // 
            // lstBattleDefendingSquad
            // 
            this.lstBattleDefendingSquad.Dock = System.Windows.Forms.DockStyle.Top;
            this.lstBattleDefendingSquad.FormattingEnabled = true;
            this.lstBattleDefendingSquad.Location = new System.Drawing.Point(2, 15);
            this.lstBattleDefendingSquad.Margin = new System.Windows.Forms.Padding(2);
            this.lstBattleDefendingSquad.Name = "lstBattleDefendingSquad";
            this.lstBattleDefendingSquad.Size = new System.Drawing.Size(412, 134);
            this.lstBattleDefendingSquad.TabIndex = 0;
            this.lstBattleDefendingSquad.SelectedIndexChanged += new System.EventHandler(this.lstBattleDefendingSquad_SelectedIndexChanged);
            // 
            // grpBattleDefendingHF
            // 
            this.grpBattleDefendingHF.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpBattleDefendingHF.Controls.Add(this.lstBattleDefendingHF);
            this.grpBattleDefendingHF.Location = new System.Drawing.Point(4, 122);
            this.grpBattleDefendingHF.Margin = new System.Windows.Forms.Padding(2);
            this.grpBattleDefendingHF.Name = "grpBattleDefendingHF";
            this.grpBattleDefendingHF.Padding = new System.Windows.Forms.Padding(2);
            this.grpBattleDefendingHF.Size = new System.Drawing.Size(417, 160);
            this.grpBattleDefendingHF.TabIndex = 0;
            this.grpBattleDefendingHF.TabStop = false;
            this.grpBattleDefendingHF.Text = "Historical Figures";
            // 
            // lstBattleDefendingHF
            // 
            this.lstBattleDefendingHF.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstBattleDefendingHF.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.lstBattleDefendingHF.FormattingEnabled = true;
            this.lstBattleDefendingHF.Location = new System.Drawing.Point(2, 15);
            this.lstBattleDefendingHF.Margin = new System.Windows.Forms.Padding(2);
            this.lstBattleDefendingHF.Name = "lstBattleDefendingHF";
            this.lstBattleDefendingHF.Size = new System.Drawing.Size(413, 143);
            this.lstBattleDefendingHF.TabIndex = 0;
            this.lstBattleDefendingHF.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.lstBattleDefendingHF_DrawItem);
            this.lstBattleDefendingHF.DoubleClick += new System.EventHandler(this.SubListBoxDoubleClicked);
            // 
            // grpBattleEvents
            // 
            this.grpBattleEvents.Controls.Add(this.lstBattleEvents);
            this.grpBattleEvents.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpBattleEvents.Location = new System.Drawing.Point(351, 242);
            this.grpBattleEvents.Name = "grpBattleEvents";
            this.tableLayoutPanel27.SetRowSpan(this.grpBattleEvents, 2);
            this.grpBattleEvents.Size = new System.Drawing.Size(479, 577);
            this.grpBattleEvents.TabIndex = 47;
            this.grpBattleEvents.TabStop = false;
            this.grpBattleEvents.Text = "Events";
            // 
            // lstBattleEvents
            // 
            this.lstBattleEvents.Dock = System.Windows.Forms.DockStyle.Top;
            this.lstBattleEvents.FormattingEnabled = true;
            this.lstBattleEvents.Location = new System.Drawing.Point(3, 16);
            this.lstBattleEvents.Name = "lstBattleEvents";
            this.lstBattleEvents.Size = new System.Drawing.Size(473, 173);
            this.lstBattleEvents.TabIndex = 0;
            this.lstBattleEvents.SelectedIndexChanged += new System.EventHandler(this.EventCollection_EventsListClick);
            this.lstBattleEvents.DoubleClick += new System.EventHandler(this.SubListBoxDoubleClicked);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lblBattleAttackerLosses);
            this.groupBox2.Controls.Add(this.lblBattleAttackerCombatants);
            this.groupBox2.Controls.Add(this.label108);
            this.groupBox2.Controls.Add(this.label104);
            this.groupBox2.Controls.Add(this.lblBattleAttackerOutcome);
            this.groupBox2.Controls.Add(this.grpBattleAttackingSquad);
            this.groupBox2.Controls.Add(this.grpBattleAttackingHF);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(835, 2);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(2);
            this.tableLayoutPanel27.SetRowSpan(this.groupBox2, 3);
            this.groupBox2.Size = new System.Drawing.Size(425, 818);
            this.groupBox2.TabIndex = 44;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Attackers";
            // 
            // lblBattleAttackerLosses
            // 
            this.lblBattleAttackerLosses.AutoSize = true;
            this.lblBattleAttackerLosses.Location = new System.Drawing.Point(70, 89);
            this.lblBattleAttackerLosses.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblBattleAttackerLosses.Name = "lblBattleAttackerLosses";
            this.lblBattleAttackerLosses.Size = new System.Drawing.Size(47, 13);
            this.lblBattleAttackerLosses.TabIndex = 6;
            this.lblBattleAttackerLosses.Text = "label116";
            // 
            // lblBattleAttackerCombatants
            // 
            this.lblBattleAttackerCombatants.AutoSize = true;
            this.lblBattleAttackerCombatants.Location = new System.Drawing.Point(70, 67);
            this.lblBattleAttackerCombatants.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblBattleAttackerCombatants.Name = "lblBattleAttackerCombatants";
            this.lblBattleAttackerCombatants.Size = new System.Drawing.Size(47, 13);
            this.lblBattleAttackerCombatants.TabIndex = 5;
            this.lblBattleAttackerCombatants.Text = "label115";
            // 
            // label108
            // 
            this.label108.AutoSize = true;
            this.label108.Location = new System.Drawing.Point(4, 89);
            this.label108.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label108.Name = "label108";
            this.label108.Size = new System.Drawing.Size(43, 13);
            this.label108.TabIndex = 4;
            this.label108.Text = "Losses:";
            // 
            // label104
            // 
            this.label104.AutoSize = true;
            this.label104.Location = new System.Drawing.Point(4, 67);
            this.label104.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label104.Name = "label104";
            this.label104.Size = new System.Drawing.Size(66, 13);
            this.label104.TabIndex = 3;
            this.label104.Text = "Combatants:";
            // 
            // lblBattleAttackerOutcome
            // 
            this.lblBattleAttackerOutcome.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblBattleAttackerOutcome.Location = new System.Drawing.Point(7, 15);
            this.lblBattleAttackerOutcome.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblBattleAttackerOutcome.Name = "lblBattleAttackerOutcome";
            this.lblBattleAttackerOutcome.Size = new System.Drawing.Size(412, 14);
            this.lblBattleAttackerOutcome.TabIndex = 2;
            this.lblBattleAttackerOutcome.Text = "label104";
            this.lblBattleAttackerOutcome.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // grpBattleAttackingSquad
            // 
            this.grpBattleAttackingSquad.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpBattleAttackingSquad.Controls.Add(this.lblBattleAttackingSquadDeaths);
            this.grpBattleAttackingSquad.Controls.Add(this.label105);
            this.grpBattleAttackingSquad.Controls.Add(this.lblBattleAttackingSquadNumber);
            this.grpBattleAttackingSquad.Controls.Add(this.label107);
            this.grpBattleAttackingSquad.Controls.Add(this.label93);
            this.grpBattleAttackingSquad.Controls.Add(this.label100);
            this.grpBattleAttackingSquad.Controls.Add(this.label102);
            this.grpBattleAttackingSquad.Controls.Add(this.lblBattleAttackingSquadRace);
            this.grpBattleAttackingSquad.Controls.Add(this.lblBattleAttackingSquadEntPop);
            this.grpBattleAttackingSquad.Controls.Add(this.lblBattleAttackingSquadSite);
            this.grpBattleAttackingSquad.Controls.Add(this.lstBattleAttackingSquad);
            this.grpBattleAttackingSquad.Location = new System.Drawing.Point(5, 287);
            this.grpBattleAttackingSquad.Margin = new System.Windows.Forms.Padding(2);
            this.grpBattleAttackingSquad.Name = "grpBattleAttackingSquad";
            this.grpBattleAttackingSquad.Padding = new System.Windows.Forms.Padding(2);
            this.grpBattleAttackingSquad.Size = new System.Drawing.Size(414, 264);
            this.grpBattleAttackingSquad.TabIndex = 1;
            this.grpBattleAttackingSquad.TabStop = false;
            this.grpBattleAttackingSquad.Text = "Squads";
            // 
            // lblBattleAttackingSquadDeaths
            // 
            this.lblBattleAttackingSquadDeaths.AutoSize = true;
            this.lblBattleAttackingSquadDeaths.Location = new System.Drawing.Point(64, 240);
            this.lblBattleAttackingSquadDeaths.Name = "lblBattleAttackingSquadDeaths";
            this.lblBattleAttackingSquadDeaths.Size = new System.Drawing.Size(35, 13);
            this.lblBattleAttackingSquadDeaths.TabIndex = 51;
            this.lblBattleAttackingSquadDeaths.Text = "label2";
            // 
            // label105
            // 
            this.label105.AutoSize = true;
            this.label105.Location = new System.Drawing.Point(6, 240);
            this.label105.Name = "label105";
            this.label105.Size = new System.Drawing.Size(44, 13);
            this.label105.TabIndex = 50;
            this.label105.Text = "Deaths:";
            // 
            // lblBattleAttackingSquadNumber
            // 
            this.lblBattleAttackingSquadNumber.AutoSize = true;
            this.lblBattleAttackingSquadNumber.Location = new System.Drawing.Point(63, 219);
            this.lblBattleAttackingSquadNumber.Name = "lblBattleAttackingSquadNumber";
            this.lblBattleAttackingSquadNumber.Size = new System.Drawing.Size(35, 13);
            this.lblBattleAttackingSquadNumber.TabIndex = 49;
            this.lblBattleAttackingSquadNumber.Text = "label2";
            // 
            // label107
            // 
            this.label107.AutoSize = true;
            this.label107.Location = new System.Drawing.Point(5, 219);
            this.label107.Name = "label107";
            this.label107.Size = new System.Drawing.Size(47, 13);
            this.label107.TabIndex = 48;
            this.label107.Text = "Number:";
            // 
            // label93
            // 
            this.label93.AutoSize = true;
            this.label93.Location = new System.Drawing.Point(4, 197);
            this.label93.Name = "label93";
            this.label93.Size = new System.Drawing.Size(36, 13);
            this.label93.TabIndex = 46;
            this.label93.Text = "Race:";
            // 
            // label100
            // 
            this.label100.AutoSize = true;
            this.label100.Location = new System.Drawing.Point(5, 176);
            this.label100.Name = "label100";
            this.label100.Size = new System.Drawing.Size(48, 13);
            this.label100.TabIndex = 44;
            this.label100.Text = "Ent Pop:";
            // 
            // label102
            // 
            this.label102.AutoSize = true;
            this.label102.Location = new System.Drawing.Point(5, 154);
            this.label102.Name = "label102";
            this.label102.Size = new System.Drawing.Size(28, 13);
            this.label102.TabIndex = 42;
            this.label102.Text = "Site:";
            // 
            // lstBattleAttackingSquad
            // 
            this.lstBattleAttackingSquad.Dock = System.Windows.Forms.DockStyle.Top;
            this.lstBattleAttackingSquad.FormattingEnabled = true;
            this.lstBattleAttackingSquad.Location = new System.Drawing.Point(2, 15);
            this.lstBattleAttackingSquad.Margin = new System.Windows.Forms.Padding(2);
            this.lstBattleAttackingSquad.Name = "lstBattleAttackingSquad";
            this.lstBattleAttackingSquad.Size = new System.Drawing.Size(410, 134);
            this.lstBattleAttackingSquad.TabIndex = 0;
            this.lstBattleAttackingSquad.SelectedIndexChanged += new System.EventHandler(this.lstBattleAttackingSquad_SelectedIndexChanged);
            // 
            // grpBattleAttackingHF
            // 
            this.grpBattleAttackingHF.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpBattleAttackingHF.Controls.Add(this.lstBattleAttackingHF);
            this.grpBattleAttackingHF.Location = new System.Drawing.Point(4, 122);
            this.grpBattleAttackingHF.Margin = new System.Windows.Forms.Padding(2);
            this.grpBattleAttackingHF.Name = "grpBattleAttackingHF";
            this.grpBattleAttackingHF.Padding = new System.Windows.Forms.Padding(2);
            this.grpBattleAttackingHF.Size = new System.Drawing.Size(417, 160);
            this.grpBattleAttackingHF.TabIndex = 0;
            this.grpBattleAttackingHF.TabStop = false;
            this.grpBattleAttackingHF.Text = "Historical Figures";
            // 
            // lstBattleAttackingHF
            // 
            this.lstBattleAttackingHF.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstBattleAttackingHF.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.lstBattleAttackingHF.FormattingEnabled = true;
            this.lstBattleAttackingHF.Location = new System.Drawing.Point(2, 15);
            this.lstBattleAttackingHF.Margin = new System.Windows.Forms.Padding(2);
            this.lstBattleAttackingHF.Name = "lstBattleAttackingHF";
            this.lstBattleAttackingHF.Size = new System.Drawing.Size(413, 143);
            this.lstBattleAttackingHF.TabIndex = 0;
            this.lstBattleAttackingHF.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.lstBattleAttackingHF_DrawItem);
            this.lstBattleAttackingHF.DoubleClick += new System.EventHandler(this.SubListBoxDoubleClicked);
            // 
            // grpBattleEventCols
            // 
            this.grpBattleEventCols.Controls.Add(this.lstBattleEventCols);
            this.grpBattleEventCols.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpBattleEventCols.Location = new System.Drawing.Point(3, 242);
            this.grpBattleEventCols.Name = "grpBattleEventCols";
            this.grpBattleEventCols.Size = new System.Drawing.Size(342, 378);
            this.grpBattleEventCols.TabIndex = 48;
            this.grpBattleEventCols.TabStop = false;
            this.grpBattleEventCols.Text = "Event Collections";
            // 
            // lstBattleEventCols
            // 
            this.lstBattleEventCols.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstBattleEventCols.FormattingEnabled = true;
            this.lstBattleEventCols.Location = new System.Drawing.Point(3, 16);
            this.lstBattleEventCols.Name = "lstBattleEventCols";
            this.lstBattleEventCols.Size = new System.Drawing.Size(336, 359);
            this.lstBattleEventCols.TabIndex = 0;
            this.lstBattleEventCols.DoubleClick += new System.EventHandler(this.SubListBoxDoubleClicked);
            // 
            // grpBattleNonComHFs
            // 
            this.grpBattleNonComHFs.Controls.Add(this.lstBattleNonComHFs);
            this.grpBattleNonComHFs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpBattleNonComHFs.Location = new System.Drawing.Point(3, 626);
            this.grpBattleNonComHFs.Name = "grpBattleNonComHFs";
            this.grpBattleNonComHFs.Size = new System.Drawing.Size(342, 193);
            this.grpBattleNonComHFs.TabIndex = 46;
            this.grpBattleNonComHFs.TabStop = false;
            this.grpBattleNonComHFs.Text = "Non Combat HFs";
            // 
            // lstBattleNonComHFs
            // 
            this.lstBattleNonComHFs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstBattleNonComHFs.FormattingEnabled = true;
            this.lstBattleNonComHFs.Location = new System.Drawing.Point(3, 16);
            this.lstBattleNonComHFs.Name = "lstBattleNonComHFs";
            this.lstBattleNonComHFs.Size = new System.Drawing.Size(336, 174);
            this.lstBattleNonComHFs.TabIndex = 0;
            this.lstBattleNonComHFs.DoubleClick += new System.EventHandler(this.SubListBoxDoubleClicked);
            // 
            // tabEventCollectionDuel
            // 
            this.tabEventCollectionDuel.Controls.Add(this.tableLayoutPanel28);
            this.tabEventCollectionDuel.Location = new System.Drawing.Point(4, 22);
            this.tabEventCollectionDuel.Margin = new System.Windows.Forms.Padding(2);
            this.tabEventCollectionDuel.Name = "tabEventCollectionDuel";
            this.tabEventCollectionDuel.Size = new System.Drawing.Size(1692, 840);
            this.tabEventCollectionDuel.TabIndex = 4;
            this.tabEventCollectionDuel.Text = "Duel";
            this.tabEventCollectionDuel.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel28
            // 
            this.tableLayoutPanel28.ColumnCount = 2;
            this.tableLayoutPanel28.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tableLayoutPanel28.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel28.Controls.Add(this.panel11, 0, 0);
            this.tableLayoutPanel28.Controls.Add(this.grpDuelEvents, 1, 0);
            this.tableLayoutPanel28.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel28.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel28.Name = "tableLayoutPanel28";
            this.tableLayoutPanel28.RowCount = 1;
            this.tableLayoutPanel28.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel28.Size = new System.Drawing.Size(1692, 822);
            this.tableLayoutPanel28.TabIndex = 109;
            // 
            // panel11
            // 
            this.panel11.Controls.Add(this.label148);
            this.panel11.Controls.Add(this.lblDuelParent);
            this.panel11.Controls.Add(this.label125);
            this.panel11.Controls.Add(this.lblDuelSite);
            this.panel11.Controls.Add(this.label133);
            this.panel11.Controls.Add(this.lblDuelCoords);
            this.panel11.Controls.Add(this.lblDuelOrdinal);
            this.panel11.Controls.Add(this.lblDuelAttacker);
            this.panel11.Controls.Add(this.label141);
            this.panel11.Controls.Add(this.lblDuelDefender);
            this.panel11.Controls.Add(this.label142);
            this.panel11.Controls.Add(this.lblDuelRegion);
            this.panel11.Controls.Add(this.label149);
            this.panel11.Controls.Add(this.lblDuelDuration);
            this.panel11.Controls.Add(this.lblDuelTime);
            this.panel11.Controls.Add(this.label144);
            this.panel11.Controls.Add(this.label146);
            this.panel11.Controls.Add(this.label145);
            this.panel11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel11.Location = new System.Drawing.Point(3, 3);
            this.panel11.Name = "panel11";
            this.panel11.Size = new System.Drawing.Size(194, 816);
            this.panel11.TabIndex = 110;
            // 
            // label148
            // 
            this.label148.AutoSize = true;
            this.label148.Location = new System.Drawing.Point(3, 0);
            this.label148.Name = "label148";
            this.label148.Size = new System.Drawing.Size(33, 13);
            this.label148.TabIndex = 92;
            this.label148.Text = "Time:";
            // 
            // label125
            // 
            this.label125.AutoSize = true;
            this.label125.Location = new System.Drawing.Point(3, 66);
            this.label125.Name = "label125";
            this.label125.Size = new System.Drawing.Size(44, 13);
            this.label125.TabIndex = 107;
            this.label125.Text = "Region:";
            // 
            // label133
            // 
            this.label133.AutoSize = true;
            this.label133.Location = new System.Drawing.Point(1, 176);
            this.label133.Name = "label133";
            this.label133.Size = new System.Drawing.Size(43, 13);
            this.label133.TabIndex = 106;
            this.label133.Text = "Ordinal:";
            // 
            // lblDuelOrdinal
            // 
            this.lblDuelOrdinal.AutoSize = true;
            this.lblDuelOrdinal.Location = new System.Drawing.Point(59, 176);
            this.lblDuelOrdinal.Name = "lblDuelOrdinal";
            this.lblDuelOrdinal.Size = new System.Drawing.Size(35, 13);
            this.lblDuelOrdinal.TabIndex = 105;
            this.lblDuelOrdinal.Text = "label2";
            // 
            // label141
            // 
            this.label141.AutoSize = true;
            this.label141.Location = new System.Drawing.Point(1, 154);
            this.label141.Name = "label141";
            this.label141.Size = new System.Drawing.Size(54, 13);
            this.label141.TabIndex = 103;
            this.label141.Text = "Defender:";
            // 
            // label142
            // 
            this.label142.AutoSize = true;
            this.label142.Location = new System.Drawing.Point(2, 132);
            this.label142.Name = "label142";
            this.label142.Size = new System.Drawing.Size(50, 13);
            this.label142.TabIndex = 101;
            this.label142.Text = "Attacker:";
            // 
            // label149
            // 
            this.label149.AutoSize = true;
            this.label149.Location = new System.Drawing.Point(3, 44);
            this.label149.Name = "label149";
            this.label149.Size = new System.Drawing.Size(41, 13);
            this.label149.TabIndex = 90;
            this.label149.Text = "Parent:";
            // 
            // lblDuelDuration
            // 
            this.lblDuelDuration.AutoSize = true;
            this.lblDuelDuration.Location = new System.Drawing.Point(61, 21);
            this.lblDuelDuration.Name = "lblDuelDuration";
            this.lblDuelDuration.Size = new System.Drawing.Size(35, 13);
            this.lblDuelDuration.TabIndex = 99;
            this.lblDuelDuration.Text = "label2";
            // 
            // lblDuelTime
            // 
            this.lblDuelTime.AutoSize = true;
            this.lblDuelTime.Location = new System.Drawing.Point(61, 0);
            this.lblDuelTime.Name = "lblDuelTime";
            this.lblDuelTime.Size = new System.Drawing.Size(35, 13);
            this.lblDuelTime.TabIndex = 93;
            this.lblDuelTime.Text = "label2";
            // 
            // label144
            // 
            this.label144.AutoSize = true;
            this.label144.Location = new System.Drawing.Point(3, 21);
            this.label144.Name = "label144";
            this.label144.Size = new System.Drawing.Size(50, 13);
            this.label144.TabIndex = 98;
            this.label144.Text = "Duration:";
            // 
            // label146
            // 
            this.label146.AutoSize = true;
            this.label146.Location = new System.Drawing.Point(2, 88);
            this.label146.Name = "label146";
            this.label146.Size = new System.Drawing.Size(28, 13);
            this.label146.TabIndex = 94;
            this.label146.Text = "Site:";
            // 
            // label145
            // 
            this.label145.AutoSize = true;
            this.label145.Location = new System.Drawing.Point(1, 110);
            this.label145.Name = "label145";
            this.label145.Size = new System.Drawing.Size(43, 13);
            this.label145.TabIndex = 96;
            this.label145.Text = "Coords:";
            // 
            // grpDuelEvents
            // 
            this.grpDuelEvents.Controls.Add(this.lstDuelEvents);
            this.grpDuelEvents.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpDuelEvents.Location = new System.Drawing.Point(203, 3);
            this.grpDuelEvents.Name = "grpDuelEvents";
            this.grpDuelEvents.Size = new System.Drawing.Size(1486, 816);
            this.grpDuelEvents.TabIndex = 100;
            this.grpDuelEvents.TabStop = false;
            this.grpDuelEvents.Text = "Events";
            // 
            // lstDuelEvents
            // 
            this.lstDuelEvents.Dock = System.Windows.Forms.DockStyle.Top;
            this.lstDuelEvents.FormattingEnabled = true;
            this.lstDuelEvents.Location = new System.Drawing.Point(3, 16);
            this.lstDuelEvents.Name = "lstDuelEvents";
            this.lstDuelEvents.Size = new System.Drawing.Size(1480, 173);
            this.lstDuelEvents.TabIndex = 0;
            this.lstDuelEvents.SelectedIndexChanged += new System.EventHandler(this.EventCollection_EventsListClick);
            this.lstDuelEvents.DoubleClick += new System.EventHandler(this.SubListBoxDoubleClicked);
            // 
            // tabEventCollectionAbduction
            // 
            this.tabEventCollectionAbduction.Controls.Add(this.tableLayoutPanel29);
            this.tabEventCollectionAbduction.Location = new System.Drawing.Point(4, 22);
            this.tabEventCollectionAbduction.Margin = new System.Windows.Forms.Padding(2);
            this.tabEventCollectionAbduction.Name = "tabEventCollectionAbduction";
            this.tabEventCollectionAbduction.Size = new System.Drawing.Size(1692, 840);
            this.tabEventCollectionAbduction.TabIndex = 5;
            this.tabEventCollectionAbduction.Text = "Abduction";
            this.tabEventCollectionAbduction.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel29
            // 
            this.tableLayoutPanel29.ColumnCount = 2;
            this.tableLayoutPanel29.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tableLayoutPanel29.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel29.Controls.Add(this.grpAbductionEventCols, 1, 1);
            this.tableLayoutPanel29.Controls.Add(this.panel12, 0, 0);
            this.tableLayoutPanel29.Controls.Add(this.grpAbductionEvents, 1, 0);
            this.tableLayoutPanel29.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel29.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel29.Name = "tableLayoutPanel29";
            this.tableLayoutPanel29.RowCount = 2;
            this.tableLayoutPanel29.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel29.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel29.Size = new System.Drawing.Size(1692, 822);
            this.tableLayoutPanel29.TabIndex = 91;
            // 
            // grpAbductionEventCols
            // 
            this.grpAbductionEventCols.Controls.Add(this.lstAbductionEventCols);
            this.grpAbductionEventCols.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpAbductionEventCols.Location = new System.Drawing.Point(203, 414);
            this.grpAbductionEventCols.Name = "grpAbductionEventCols";
            this.grpAbductionEventCols.Size = new System.Drawing.Size(1486, 405);
            this.grpAbductionEventCols.TabIndex = 91;
            this.grpAbductionEventCols.TabStop = false;
            this.grpAbductionEventCols.Text = "Event Collections";
            // 
            // lstAbductionEventCols
            // 
            this.lstAbductionEventCols.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstAbductionEventCols.FormattingEnabled = true;
            this.lstAbductionEventCols.Location = new System.Drawing.Point(3, 16);
            this.lstAbductionEventCols.Name = "lstAbductionEventCols";
            this.lstAbductionEventCols.Size = new System.Drawing.Size(1480, 386);
            this.lstAbductionEventCols.TabIndex = 0;
            // 
            // panel12
            // 
            this.panel12.Controls.Add(this.label138);
            this.panel12.Controls.Add(this.lblAbductionParent);
            this.panel12.Controls.Add(this.label140);
            this.panel12.Controls.Add(this.lblAbductionSite);
            this.panel12.Controls.Add(this.label123);
            this.panel12.Controls.Add(this.lblAbductionCoords);
            this.panel12.Controls.Add(this.lblAbductionOrdinal);
            this.panel12.Controls.Add(this.lblAbductionAttacker);
            this.panel12.Controls.Add(this.label131);
            this.panel12.Controls.Add(this.lblAbductionDefender);
            this.panel12.Controls.Add(this.label132);
            this.panel12.Controls.Add(this.lblAbductionRegion);
            this.panel12.Controls.Add(this.label139);
            this.panel12.Controls.Add(this.lblAbductionDuration);
            this.panel12.Controls.Add(this.lblAbductionTime);
            this.panel12.Controls.Add(this.label134);
            this.panel12.Controls.Add(this.label136);
            this.panel12.Controls.Add(this.label135);
            this.panel12.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel12.Location = new System.Drawing.Point(3, 3);
            this.panel12.Name = "panel12";
            this.tableLayoutPanel29.SetRowSpan(this.panel12, 2);
            this.panel12.Size = new System.Drawing.Size(194, 816);
            this.panel12.TabIndex = 90;
            // 
            // label138
            // 
            this.label138.AutoSize = true;
            this.label138.Location = new System.Drawing.Point(3, 0);
            this.label138.Name = "label138";
            this.label138.Size = new System.Drawing.Size(33, 13);
            this.label138.TabIndex = 73;
            this.label138.Text = "Time:";
            // 
            // label140
            // 
            this.label140.AutoSize = true;
            this.label140.Location = new System.Drawing.Point(3, 66);
            this.label140.Name = "label140";
            this.label140.Size = new System.Drawing.Size(44, 13);
            this.label140.TabIndex = 88;
            this.label140.Text = "Region:";
            // 
            // label123
            // 
            this.label123.AutoSize = true;
            this.label123.Location = new System.Drawing.Point(1, 176);
            this.label123.Name = "label123";
            this.label123.Size = new System.Drawing.Size(43, 13);
            this.label123.TabIndex = 87;
            this.label123.Text = "Ordinal:";
            // 
            // lblAbductionOrdinal
            // 
            this.lblAbductionOrdinal.AutoSize = true;
            this.lblAbductionOrdinal.Location = new System.Drawing.Point(59, 176);
            this.lblAbductionOrdinal.Name = "lblAbductionOrdinal";
            this.lblAbductionOrdinal.Size = new System.Drawing.Size(35, 13);
            this.lblAbductionOrdinal.TabIndex = 86;
            this.lblAbductionOrdinal.Text = "label2";
            // 
            // label131
            // 
            this.label131.AutoSize = true;
            this.label131.Location = new System.Drawing.Point(1, 154);
            this.label131.Name = "label131";
            this.label131.Size = new System.Drawing.Size(54, 13);
            this.label131.TabIndex = 84;
            this.label131.Text = "Defender:";
            // 
            // label132
            // 
            this.label132.AutoSize = true;
            this.label132.Location = new System.Drawing.Point(2, 132);
            this.label132.Name = "label132";
            this.label132.Size = new System.Drawing.Size(50, 13);
            this.label132.TabIndex = 82;
            this.label132.Text = "Attacker:";
            // 
            // label139
            // 
            this.label139.AutoSize = true;
            this.label139.Location = new System.Drawing.Point(3, 44);
            this.label139.Name = "label139";
            this.label139.Size = new System.Drawing.Size(41, 13);
            this.label139.TabIndex = 71;
            this.label139.Text = "Parent:";
            // 
            // lblAbductionDuration
            // 
            this.lblAbductionDuration.AutoSize = true;
            this.lblAbductionDuration.Location = new System.Drawing.Point(61, 21);
            this.lblAbductionDuration.Name = "lblAbductionDuration";
            this.lblAbductionDuration.Size = new System.Drawing.Size(35, 13);
            this.lblAbductionDuration.TabIndex = 80;
            this.lblAbductionDuration.Text = "label2";
            // 
            // lblAbductionTime
            // 
            this.lblAbductionTime.AutoSize = true;
            this.lblAbductionTime.Location = new System.Drawing.Point(61, 0);
            this.lblAbductionTime.Name = "lblAbductionTime";
            this.lblAbductionTime.Size = new System.Drawing.Size(35, 13);
            this.lblAbductionTime.TabIndex = 74;
            this.lblAbductionTime.Text = "label2";
            // 
            // label134
            // 
            this.label134.AutoSize = true;
            this.label134.Location = new System.Drawing.Point(3, 21);
            this.label134.Name = "label134";
            this.label134.Size = new System.Drawing.Size(50, 13);
            this.label134.TabIndex = 79;
            this.label134.Text = "Duration:";
            // 
            // label136
            // 
            this.label136.AutoSize = true;
            this.label136.Location = new System.Drawing.Point(2, 88);
            this.label136.Name = "label136";
            this.label136.Size = new System.Drawing.Size(28, 13);
            this.label136.TabIndex = 75;
            this.label136.Text = "Site:";
            // 
            // label135
            // 
            this.label135.AutoSize = true;
            this.label135.Location = new System.Drawing.Point(1, 110);
            this.label135.Name = "label135";
            this.label135.Size = new System.Drawing.Size(43, 13);
            this.label135.TabIndex = 77;
            this.label135.Text = "Coords:";
            // 
            // grpAbductionEvents
            // 
            this.grpAbductionEvents.Controls.Add(this.lstAbductionEvents);
            this.grpAbductionEvents.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpAbductionEvents.Location = new System.Drawing.Point(203, 3);
            this.grpAbductionEvents.Name = "grpAbductionEvents";
            this.grpAbductionEvents.Size = new System.Drawing.Size(1486, 405);
            this.grpAbductionEvents.TabIndex = 81;
            this.grpAbductionEvents.TabStop = false;
            this.grpAbductionEvents.Text = "Events";
            // 
            // lstAbductionEvents
            // 
            this.lstAbductionEvents.Dock = System.Windows.Forms.DockStyle.Top;
            this.lstAbductionEvents.FormattingEnabled = true;
            this.lstAbductionEvents.Location = new System.Drawing.Point(3, 16);
            this.lstAbductionEvents.Name = "lstAbductionEvents";
            this.lstAbductionEvents.Size = new System.Drawing.Size(1480, 173);
            this.lstAbductionEvents.TabIndex = 0;
            this.lstAbductionEvents.SelectedIndexChanged += new System.EventHandler(this.EventCollection_EventsListClick);
            this.lstAbductionEvents.DoubleClick += new System.EventHandler(this.SubListBoxDoubleClicked);
            // 
            // tabEventCollectionSiteConquered
            // 
            this.tabEventCollectionSiteConquered.Controls.Add(this.tableLayoutPanel30);
            this.tabEventCollectionSiteConquered.Location = new System.Drawing.Point(4, 22);
            this.tabEventCollectionSiteConquered.Margin = new System.Windows.Forms.Padding(2);
            this.tabEventCollectionSiteConquered.Name = "tabEventCollectionSiteConquered";
            this.tabEventCollectionSiteConquered.Size = new System.Drawing.Size(1692, 840);
            this.tabEventCollectionSiteConquered.TabIndex = 6;
            this.tabEventCollectionSiteConquered.Text = "Site Conquered";
            this.tabEventCollectionSiteConquered.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel30
            // 
            this.tableLayoutPanel30.ColumnCount = 2;
            this.tableLayoutPanel30.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tableLayoutPanel30.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel30.Controls.Add(this.grpSiteConqueredEvents, 1, 0);
            this.tableLayoutPanel30.Controls.Add(this.panel13, 0, 0);
            this.tableLayoutPanel30.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel30.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel30.Name = "tableLayoutPanel30";
            this.tableLayoutPanel30.RowCount = 1;
            this.tableLayoutPanel30.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel30.Size = new System.Drawing.Size(1692, 822);
            this.tableLayoutPanel30.TabIndex = 72;
            // 
            // grpSiteConqueredEvents
            // 
            this.grpSiteConqueredEvents.Controls.Add(this.lstSiteConqueredEvents);
            this.grpSiteConqueredEvents.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpSiteConqueredEvents.Location = new System.Drawing.Point(203, 3);
            this.grpSiteConqueredEvents.Name = "grpSiteConqueredEvents";
            this.grpSiteConqueredEvents.Size = new System.Drawing.Size(1486, 816);
            this.grpSiteConqueredEvents.TabIndex = 63;
            this.grpSiteConqueredEvents.TabStop = false;
            this.grpSiteConqueredEvents.Text = "Events";
            // 
            // lstSiteConqueredEvents
            // 
            this.lstSiteConqueredEvents.Dock = System.Windows.Forms.DockStyle.Top;
            this.lstSiteConqueredEvents.FormattingEnabled = true;
            this.lstSiteConqueredEvents.Location = new System.Drawing.Point(3, 16);
            this.lstSiteConqueredEvents.Name = "lstSiteConqueredEvents";
            this.lstSiteConqueredEvents.Size = new System.Drawing.Size(1480, 173);
            this.lstSiteConqueredEvents.TabIndex = 0;
            this.lstSiteConqueredEvents.SelectedIndexChanged += new System.EventHandler(this.EventCollection_EventsListClick);
            this.lstSiteConqueredEvents.DoubleClick += new System.EventHandler(this.SubListBoxDoubleClicked);
            // 
            // panel13
            // 
            this.panel13.Controls.Add(this.label127);
            this.panel13.Controls.Add(this.lblSiteConqueredWar);
            this.panel13.Controls.Add(this.label115);
            this.panel13.Controls.Add(this.lblSiteConqueredSite);
            this.panel13.Controls.Add(this.lblSiteConqueredOrdinal);
            this.panel13.Controls.Add(this.lblSiteConqueredCoords);
            this.panel13.Controls.Add(this.label151);
            this.panel13.Controls.Add(this.lblSiteConqueredAttacker);
            this.panel13.Controls.Add(this.label150);
            this.panel13.Controls.Add(this.lblSiteConqueredDefender);
            this.panel13.Controls.Add(this.label128);
            this.panel13.Controls.Add(this.lblSiteConqueredDuration);
            this.panel13.Controls.Add(this.lblSiteConqueredTime);
            this.panel13.Controls.Add(this.label116);
            this.panel13.Controls.Add(this.label121);
            this.panel13.Controls.Add(this.label119);
            this.panel13.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel13.Location = new System.Drawing.Point(3, 3);
            this.panel13.Name = "panel13";
            this.panel13.Size = new System.Drawing.Size(194, 816);
            this.panel13.TabIndex = 71;
            // 
            // label127
            // 
            this.label127.AutoSize = true;
            this.label127.Location = new System.Drawing.Point(3, 0);
            this.label127.Name = "label127";
            this.label127.Size = new System.Drawing.Size(33, 13);
            this.label127.TabIndex = 53;
            this.label127.Text = "Time:";
            // 
            // label115
            // 
            this.label115.AutoSize = true;
            this.label115.Location = new System.Drawing.Point(2, 154);
            this.label115.Name = "label115";
            this.label115.Size = new System.Drawing.Size(43, 13);
            this.label115.TabIndex = 70;
            this.label115.Text = "Ordinal:";
            // 
            // lblSiteConqueredOrdinal
            // 
            this.lblSiteConqueredOrdinal.AutoSize = true;
            this.lblSiteConqueredOrdinal.Location = new System.Drawing.Point(60, 154);
            this.lblSiteConqueredOrdinal.Name = "lblSiteConqueredOrdinal";
            this.lblSiteConqueredOrdinal.Size = new System.Drawing.Size(35, 13);
            this.lblSiteConqueredOrdinal.TabIndex = 69;
            this.lblSiteConqueredOrdinal.Text = "label2";
            // 
            // label151
            // 
            this.label151.AutoSize = true;
            this.label151.Location = new System.Drawing.Point(2, 132);
            this.label151.Name = "label151";
            this.label151.Size = new System.Drawing.Size(54, 13);
            this.label151.TabIndex = 66;
            this.label151.Text = "Defender:";
            // 
            // label150
            // 
            this.label150.AutoSize = true;
            this.label150.Location = new System.Drawing.Point(3, 110);
            this.label150.Name = "label150";
            this.label150.Size = new System.Drawing.Size(50, 13);
            this.label150.TabIndex = 64;
            this.label150.Text = "Attacker:";
            // 
            // label128
            // 
            this.label128.AutoSize = true;
            this.label128.Location = new System.Drawing.Point(3, 44);
            this.label128.Name = "label128";
            this.label128.Size = new System.Drawing.Size(30, 13);
            this.label128.TabIndex = 51;
            this.label128.Text = "War:";
            // 
            // lblSiteConqueredDuration
            // 
            this.lblSiteConqueredDuration.AutoSize = true;
            this.lblSiteConqueredDuration.Location = new System.Drawing.Point(61, 21);
            this.lblSiteConqueredDuration.Name = "lblSiteConqueredDuration";
            this.lblSiteConqueredDuration.Size = new System.Drawing.Size(35, 13);
            this.lblSiteConqueredDuration.TabIndex = 62;
            this.lblSiteConqueredDuration.Text = "label2";
            // 
            // lblSiteConqueredTime
            // 
            this.lblSiteConqueredTime.AutoSize = true;
            this.lblSiteConqueredTime.Location = new System.Drawing.Point(61, 0);
            this.lblSiteConqueredTime.Name = "lblSiteConqueredTime";
            this.lblSiteConqueredTime.Size = new System.Drawing.Size(35, 13);
            this.lblSiteConqueredTime.TabIndex = 54;
            this.lblSiteConqueredTime.Text = "label2";
            // 
            // label116
            // 
            this.label116.AutoSize = true;
            this.label116.Location = new System.Drawing.Point(3, 21);
            this.label116.Name = "label116";
            this.label116.Size = new System.Drawing.Size(50, 13);
            this.label116.TabIndex = 61;
            this.label116.Text = "Duration:";
            // 
            // label121
            // 
            this.label121.AutoSize = true;
            this.label121.Location = new System.Drawing.Point(3, 66);
            this.label121.Name = "label121";
            this.label121.Size = new System.Drawing.Size(28, 13);
            this.label121.TabIndex = 57;
            this.label121.Text = "Site:";
            // 
            // label119
            // 
            this.label119.AutoSize = true;
            this.label119.Location = new System.Drawing.Point(2, 88);
            this.label119.Name = "label119";
            this.label119.Size = new System.Drawing.Size(43, 13);
            this.label119.TabIndex = 59;
            this.label119.Text = "Coords:";
            // 
            // tabEventCollectionTheft
            // 
            this.tabEventCollectionTheft.Controls.Add(this.tableLayoutPanel31);
            this.tabEventCollectionTheft.Location = new System.Drawing.Point(4, 22);
            this.tabEventCollectionTheft.Margin = new System.Windows.Forms.Padding(2);
            this.tabEventCollectionTheft.Name = "tabEventCollectionTheft";
            this.tabEventCollectionTheft.Size = new System.Drawing.Size(1692, 840);
            this.tabEventCollectionTheft.TabIndex = 7;
            this.tabEventCollectionTheft.Text = "Theft";
            this.tabEventCollectionTheft.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel31
            // 
            this.tableLayoutPanel31.ColumnCount = 2;
            this.tableLayoutPanel31.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tableLayoutPanel31.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel31.Controls.Add(this.grpTheftEventCols, 1, 1);
            this.tableLayoutPanel31.Controls.Add(this.panel14, 0, 0);
            this.tableLayoutPanel31.Controls.Add(this.grpTheftEvents, 1, 0);
            this.tableLayoutPanel31.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel31.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel31.Name = "tableLayoutPanel31";
            this.tableLayoutPanel31.RowCount = 2;
            this.tableLayoutPanel31.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel31.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel31.Size = new System.Drawing.Size(1692, 822);
            this.tableLayoutPanel31.TabIndex = 88;
            // 
            // grpTheftEventCols
            // 
            this.grpTheftEventCols.Controls.Add(this.lstTheftEventCols);
            this.grpTheftEventCols.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpTheftEventCols.Location = new System.Drawing.Point(203, 414);
            this.grpTheftEventCols.Name = "grpTheftEventCols";
            this.grpTheftEventCols.Size = new System.Drawing.Size(1486, 405);
            this.grpTheftEventCols.TabIndex = 90;
            this.grpTheftEventCols.TabStop = false;
            this.grpTheftEventCols.Text = "Event Collections";
            // 
            // lstTheftEventCols
            // 
            this.lstTheftEventCols.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstTheftEventCols.FormattingEnabled = true;
            this.lstTheftEventCols.Location = new System.Drawing.Point(3, 16);
            this.lstTheftEventCols.Name = "lstTheftEventCols";
            this.lstTheftEventCols.Size = new System.Drawing.Size(1480, 386);
            this.lstTheftEventCols.TabIndex = 0;
            // 
            // panel14
            // 
            this.panel14.Controls.Add(this.label169);
            this.panel14.Controls.Add(this.lblTheftWar);
            this.panel14.Controls.Add(this.label153);
            this.panel14.Controls.Add(this.lblTheftSite);
            this.panel14.Controls.Add(this.lblTheftOrdinal);
            this.panel14.Controls.Add(this.lblTheftCoords);
            this.panel14.Controls.Add(this.label161);
            this.panel14.Controls.Add(this.lblTheftAttacker);
            this.panel14.Controls.Add(this.label163);
            this.panel14.Controls.Add(this.lblTheftDefender);
            this.panel14.Controls.Add(this.label170);
            this.panel14.Controls.Add(this.lblTheftDuration);
            this.panel14.Controls.Add(this.lblTheftTime);
            this.panel14.Controls.Add(this.label165);
            this.panel14.Controls.Add(this.label167);
            this.panel14.Controls.Add(this.label166);
            this.panel14.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel14.Location = new System.Drawing.Point(3, 3);
            this.panel14.Name = "panel14";
            this.panel14.Size = new System.Drawing.Size(194, 405);
            this.panel14.TabIndex = 89;
            // 
            // label169
            // 
            this.label169.AutoSize = true;
            this.label169.Location = new System.Drawing.Point(3, 0);
            this.label169.Name = "label169";
            this.label169.Size = new System.Drawing.Size(33, 13);
            this.label169.TabIndex = 73;
            this.label169.Text = "Time:";
            // 
            // label153
            // 
            this.label153.AutoSize = true;
            this.label153.Location = new System.Drawing.Point(2, 154);
            this.label153.Name = "label153";
            this.label153.Size = new System.Drawing.Size(43, 13);
            this.label153.TabIndex = 87;
            this.label153.Text = "Ordinal:";
            // 
            // lblTheftOrdinal
            // 
            this.lblTheftOrdinal.AutoSize = true;
            this.lblTheftOrdinal.Location = new System.Drawing.Point(60, 154);
            this.lblTheftOrdinal.Name = "lblTheftOrdinal";
            this.lblTheftOrdinal.Size = new System.Drawing.Size(35, 13);
            this.lblTheftOrdinal.TabIndex = 86;
            this.lblTheftOrdinal.Text = "label2";
            // 
            // label161
            // 
            this.label161.AutoSize = true;
            this.label161.Location = new System.Drawing.Point(2, 132);
            this.label161.Name = "label161";
            this.label161.Size = new System.Drawing.Size(54, 13);
            this.label161.TabIndex = 84;
            this.label161.Text = "Defender:";
            // 
            // label163
            // 
            this.label163.AutoSize = true;
            this.label163.Location = new System.Drawing.Point(3, 110);
            this.label163.Name = "label163";
            this.label163.Size = new System.Drawing.Size(50, 13);
            this.label163.TabIndex = 82;
            this.label163.Text = "Attacker:";
            // 
            // label170
            // 
            this.label170.AutoSize = true;
            this.label170.Location = new System.Drawing.Point(3, 44);
            this.label170.Name = "label170";
            this.label170.Size = new System.Drawing.Size(30, 13);
            this.label170.TabIndex = 71;
            this.label170.Text = "War:";
            // 
            // lblTheftDuration
            // 
            this.lblTheftDuration.AutoSize = true;
            this.lblTheftDuration.Location = new System.Drawing.Point(61, 21);
            this.lblTheftDuration.Name = "lblTheftDuration";
            this.lblTheftDuration.Size = new System.Drawing.Size(35, 13);
            this.lblTheftDuration.TabIndex = 80;
            this.lblTheftDuration.Text = "label2";
            // 
            // lblTheftTime
            // 
            this.lblTheftTime.AutoSize = true;
            this.lblTheftTime.Location = new System.Drawing.Point(61, 0);
            this.lblTheftTime.Name = "lblTheftTime";
            this.lblTheftTime.Size = new System.Drawing.Size(35, 13);
            this.lblTheftTime.TabIndex = 74;
            this.lblTheftTime.Text = "label2";
            // 
            // label165
            // 
            this.label165.AutoSize = true;
            this.label165.Location = new System.Drawing.Point(3, 21);
            this.label165.Name = "label165";
            this.label165.Size = new System.Drawing.Size(50, 13);
            this.label165.TabIndex = 79;
            this.label165.Text = "Duration:";
            // 
            // label167
            // 
            this.label167.AutoSize = true;
            this.label167.Location = new System.Drawing.Point(3, 66);
            this.label167.Name = "label167";
            this.label167.Size = new System.Drawing.Size(28, 13);
            this.label167.TabIndex = 75;
            this.label167.Text = "Site:";
            // 
            // label166
            // 
            this.label166.AutoSize = true;
            this.label166.Location = new System.Drawing.Point(2, 88);
            this.label166.Name = "label166";
            this.label166.Size = new System.Drawing.Size(43, 13);
            this.label166.TabIndex = 77;
            this.label166.Text = "Coords:";
            // 
            // grpTheftEvents
            // 
            this.grpTheftEvents.Controls.Add(this.lstTheftEvents);
            this.grpTheftEvents.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpTheftEvents.Location = new System.Drawing.Point(203, 3);
            this.grpTheftEvents.Name = "grpTheftEvents";
            this.grpTheftEvents.Size = new System.Drawing.Size(1486, 405);
            this.grpTheftEvents.TabIndex = 81;
            this.grpTheftEvents.TabStop = false;
            this.grpTheftEvents.Text = "Events";
            // 
            // lstTheftEvents
            // 
            this.lstTheftEvents.Dock = System.Windows.Forms.DockStyle.Top;
            this.lstTheftEvents.FormattingEnabled = true;
            this.lstTheftEvents.Location = new System.Drawing.Point(3, 16);
            this.lstTheftEvents.Name = "lstTheftEvents";
            this.lstTheftEvents.Size = new System.Drawing.Size(1480, 173);
            this.lstTheftEvents.TabIndex = 0;
            this.lstTheftEvents.SelectedIndexChanged += new System.EventHandler(this.EventCollection_EventsListClick);
            this.lstTheftEvents.DoubleClick += new System.EventHandler(this.SubListBoxDoubleClicked);
            // 
            // tabEventCollectionInsurrection
            // 
            this.tabEventCollectionInsurrection.Controls.Add(this.tableLayoutPanel39);
            this.tabEventCollectionInsurrection.Location = new System.Drawing.Point(4, 22);
            this.tabEventCollectionInsurrection.Margin = new System.Windows.Forms.Padding(2);
            this.tabEventCollectionInsurrection.Name = "tabEventCollectionInsurrection";
            this.tabEventCollectionInsurrection.Size = new System.Drawing.Size(1692, 840);
            this.tabEventCollectionInsurrection.TabIndex = 8;
            this.tabEventCollectionInsurrection.Text = "Insurrection";
            this.tabEventCollectionInsurrection.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel39
            // 
            this.tableLayoutPanel39.ColumnCount = 2;
            this.tableLayoutPanel39.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tableLayoutPanel39.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel39.Controls.Add(this.panel20, 0, 0);
            this.tableLayoutPanel39.Controls.Add(this.grpInsurrectionEvents, 1, 0);
            this.tableLayoutPanel39.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel39.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel39.Name = "tableLayoutPanel39";
            this.tableLayoutPanel39.RowCount = 1;
            this.tableLayoutPanel39.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel39.Size = new System.Drawing.Size(1692, 822);
            this.tableLayoutPanel39.TabIndex = 109;
            // 
            // panel20
            // 
            this.panel20.Controls.Add(this.lblInsurrectionOutcome);
            this.panel20.Controls.Add(this.label179);
            this.panel20.Controls.Add(this.label178);
            this.panel20.Controls.Add(this.lblInsurrectionParent);
            this.panel20.Controls.Add(this.lblInsurrectionSite);
            this.panel20.Controls.Add(this.label180);
            this.panel20.Controls.Add(this.lblInsurrectionCoords);
            this.panel20.Controls.Add(this.lblInsurrectionOrdinal);
            this.panel20.Controls.Add(this.lblInsurrectionTargetEnt);
            this.panel20.Controls.Add(this.label183);
            this.panel20.Controls.Add(this.label184);
            this.panel20.Controls.Add(this.lblInsurrectionDuration);
            this.panel20.Controls.Add(this.lblInsurrectionTime);
            this.panel20.Controls.Add(this.label187);
            this.panel20.Controls.Add(this.label188);
            this.panel20.Controls.Add(this.label189);
            this.panel20.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel20.Location = new System.Drawing.Point(3, 3);
            this.panel20.Name = "panel20";
            this.panel20.Size = new System.Drawing.Size(194, 816);
            this.panel20.TabIndex = 110;
            // 
            // label179
            // 
            this.label179.AutoSize = true;
            this.label179.Location = new System.Drawing.Point(3, 132);
            this.label179.Name = "label179";
            this.label179.Size = new System.Drawing.Size(53, 13);
            this.label179.TabIndex = 107;
            this.label179.Text = "Outcome:";
            // 
            // label178
            // 
            this.label178.AutoSize = true;
            this.label178.Location = new System.Drawing.Point(3, 0);
            this.label178.Name = "label178";
            this.label178.Size = new System.Drawing.Size(33, 13);
            this.label178.TabIndex = 92;
            this.label178.Text = "Time:";
            // 
            // label180
            // 
            this.label180.AutoSize = true;
            this.label180.Location = new System.Drawing.Point(2, 154);
            this.label180.Name = "label180";
            this.label180.Size = new System.Drawing.Size(43, 13);
            this.label180.TabIndex = 106;
            this.label180.Text = "Ordinal:";
            // 
            // lblInsurrectionOrdinal
            // 
            this.lblInsurrectionOrdinal.AutoSize = true;
            this.lblInsurrectionOrdinal.Location = new System.Drawing.Point(60, 154);
            this.lblInsurrectionOrdinal.Name = "lblInsurrectionOrdinal";
            this.lblInsurrectionOrdinal.Size = new System.Drawing.Size(35, 13);
            this.lblInsurrectionOrdinal.TabIndex = 105;
            this.lblInsurrectionOrdinal.Text = "label2";
            // 
            // label183
            // 
            this.label183.AutoSize = true;
            this.label183.Location = new System.Drawing.Point(3, 110);
            this.label183.Name = "label183";
            this.label183.Size = new System.Drawing.Size(41, 13);
            this.label183.TabIndex = 101;
            this.label183.Text = "Target:";
            // 
            // label184
            // 
            this.label184.AutoSize = true;
            this.label184.Location = new System.Drawing.Point(3, 44);
            this.label184.Name = "label184";
            this.label184.Size = new System.Drawing.Size(41, 13);
            this.label184.TabIndex = 90;
            this.label184.Text = "Parent:";
            // 
            // lblInsurrectionDuration
            // 
            this.lblInsurrectionDuration.AutoSize = true;
            this.lblInsurrectionDuration.Location = new System.Drawing.Point(61, 21);
            this.lblInsurrectionDuration.Name = "lblInsurrectionDuration";
            this.lblInsurrectionDuration.Size = new System.Drawing.Size(35, 13);
            this.lblInsurrectionDuration.TabIndex = 99;
            this.lblInsurrectionDuration.Text = "label2";
            // 
            // lblInsurrectionTime
            // 
            this.lblInsurrectionTime.AutoSize = true;
            this.lblInsurrectionTime.Location = new System.Drawing.Point(61, 0);
            this.lblInsurrectionTime.Name = "lblInsurrectionTime";
            this.lblInsurrectionTime.Size = new System.Drawing.Size(35, 13);
            this.lblInsurrectionTime.TabIndex = 93;
            this.lblInsurrectionTime.Text = "label2";
            // 
            // label187
            // 
            this.label187.AutoSize = true;
            this.label187.Location = new System.Drawing.Point(3, 21);
            this.label187.Name = "label187";
            this.label187.Size = new System.Drawing.Size(50, 13);
            this.label187.TabIndex = 98;
            this.label187.Text = "Duration:";
            // 
            // label188
            // 
            this.label188.AutoSize = true;
            this.label188.Location = new System.Drawing.Point(3, 66);
            this.label188.Name = "label188";
            this.label188.Size = new System.Drawing.Size(28, 13);
            this.label188.TabIndex = 94;
            this.label188.Text = "Site:";
            // 
            // label189
            // 
            this.label189.AutoSize = true;
            this.label189.Location = new System.Drawing.Point(2, 88);
            this.label189.Name = "label189";
            this.label189.Size = new System.Drawing.Size(43, 13);
            this.label189.TabIndex = 96;
            this.label189.Text = "Coords:";
            // 
            // grpInsurrectionEvents
            // 
            this.grpInsurrectionEvents.Controls.Add(this.lstInsurrectionEvents);
            this.grpInsurrectionEvents.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpInsurrectionEvents.Location = new System.Drawing.Point(203, 3);
            this.grpInsurrectionEvents.Name = "grpInsurrectionEvents";
            this.grpInsurrectionEvents.Size = new System.Drawing.Size(1486, 816);
            this.grpInsurrectionEvents.TabIndex = 100;
            this.grpInsurrectionEvents.TabStop = false;
            this.grpInsurrectionEvents.Text = "Events";
            // 
            // lstInsurrectionEvents
            // 
            this.lstInsurrectionEvents.Dock = System.Windows.Forms.DockStyle.Top;
            this.lstInsurrectionEvents.FormattingEnabled = true;
            this.lstInsurrectionEvents.Location = new System.Drawing.Point(3, 16);
            this.lstInsurrectionEvents.Name = "lstInsurrectionEvents";
            this.lstInsurrectionEvents.Size = new System.Drawing.Size(1480, 173);
            this.lstInsurrectionEvents.TabIndex = 0;
            // 
            // tabEventCollectionOccasion
            // 
            this.tabEventCollectionOccasion.Controls.Add(this.tableLayoutPanel57);
            this.tabEventCollectionOccasion.Location = new System.Drawing.Point(4, 22);
            this.tabEventCollectionOccasion.Margin = new System.Windows.Forms.Padding(2);
            this.tabEventCollectionOccasion.Name = "tabEventCollectionOccasion";
            this.tabEventCollectionOccasion.Size = new System.Drawing.Size(1692, 840);
            this.tabEventCollectionOccasion.TabIndex = 9;
            this.tabEventCollectionOccasion.Text = "Occasion";
            this.tabEventCollectionOccasion.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel57
            // 
            this.tableLayoutPanel57.ColumnCount = 2;
            this.tableLayoutPanel57.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tableLayoutPanel57.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel57.Controls.Add(this.grpOccasionEventCols, 1, 1);
            this.tableLayoutPanel57.Controls.Add(this.panel22, 0, 0);
            this.tableLayoutPanel57.Controls.Add(this.grpOccasionEvents, 1, 0);
            this.tableLayoutPanel57.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel57.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel57.Name = "tableLayoutPanel57";
            this.tableLayoutPanel57.RowCount = 2;
            this.tableLayoutPanel57.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel57.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel57.Size = new System.Drawing.Size(1692, 822);
            this.tableLayoutPanel57.TabIndex = 91;
            // 
            // grpOccasionEventCols
            // 
            this.grpOccasionEventCols.Controls.Add(this.lstOccasionEventCols);
            this.grpOccasionEventCols.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpOccasionEventCols.Location = new System.Drawing.Point(203, 414);
            this.grpOccasionEventCols.Name = "grpOccasionEventCols";
            this.grpOccasionEventCols.Size = new System.Drawing.Size(1486, 405);
            this.grpOccasionEventCols.TabIndex = 91;
            this.grpOccasionEventCols.TabStop = false;
            this.grpOccasionEventCols.Text = "Event Collections";
            // 
            // lstOccasionEventCols
            // 
            this.lstOccasionEventCols.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstOccasionEventCols.FormattingEnabled = true;
            this.lstOccasionEventCols.Location = new System.Drawing.Point(3, 16);
            this.lstOccasionEventCols.Name = "lstOccasionEventCols";
            this.lstOccasionEventCols.Size = new System.Drawing.Size(1480, 386);
            this.lstOccasionEventCols.TabIndex = 0;
            // 
            // panel22
            // 
            this.panel22.Controls.Add(this.label231);
            this.panel22.Controls.Add(this.lblOccasionID);
            this.panel22.Controls.Add(this.label230);
            this.panel22.Controls.Add(this.label232);
            this.panel22.Controls.Add(this.lblOccasionOrdinal);
            this.panel22.Controls.Add(this.lblOccasionCiv);
            this.panel22.Controls.Add(this.label235);
            this.panel22.Controls.Add(this.lblOccasionDuration);
            this.panel22.Controls.Add(this.lblOccasionTime);
            this.panel22.Controls.Add(this.label239);
            this.panel22.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel22.Location = new System.Drawing.Point(3, 3);
            this.panel22.Name = "panel22";
            this.tableLayoutPanel57.SetRowSpan(this.panel22, 2);
            this.panel22.Size = new System.Drawing.Size(194, 816);
            this.panel22.TabIndex = 90;
            // 
            // label231
            // 
            this.label231.AutoSize = true;
            this.label231.Location = new System.Drawing.Point(3, 65);
            this.label231.Name = "label231";
            this.label231.Size = new System.Drawing.Size(55, 13);
            this.label231.TabIndex = 90;
            this.label231.Text = "Occasion:";
            // 
            // lblOccasionID
            // 
            this.lblOccasionID.AutoSize = true;
            this.lblOccasionID.Location = new System.Drawing.Point(59, 65);
            this.lblOccasionID.Name = "lblOccasionID";
            this.lblOccasionID.Size = new System.Drawing.Size(35, 13);
            this.lblOccasionID.TabIndex = 89;
            this.lblOccasionID.Text = "label2";
            // 
            // label230
            // 
            this.label230.AutoSize = true;
            this.label230.Location = new System.Drawing.Point(3, 0);
            this.label230.Name = "label230";
            this.label230.Size = new System.Drawing.Size(33, 13);
            this.label230.TabIndex = 73;
            this.label230.Text = "Time:";
            // 
            // label232
            // 
            this.label232.AutoSize = true;
            this.label232.Location = new System.Drawing.Point(3, 87);
            this.label232.Name = "label232";
            this.label232.Size = new System.Drawing.Size(43, 13);
            this.label232.TabIndex = 87;
            this.label232.Text = "Ordinal:";
            // 
            // lblOccasionOrdinal
            // 
            this.lblOccasionOrdinal.AutoSize = true;
            this.lblOccasionOrdinal.Location = new System.Drawing.Point(59, 87);
            this.lblOccasionOrdinal.Name = "lblOccasionOrdinal";
            this.lblOccasionOrdinal.Size = new System.Drawing.Size(35, 13);
            this.lblOccasionOrdinal.TabIndex = 86;
            this.lblOccasionOrdinal.Text = "label2";
            // 
            // label235
            // 
            this.label235.AutoSize = true;
            this.label235.Location = new System.Drawing.Point(3, 43);
            this.label235.Name = "label235";
            this.label235.Size = new System.Drawing.Size(25, 13);
            this.label235.TabIndex = 82;
            this.label235.Text = "Civ:";
            // 
            // lblOccasionDuration
            // 
            this.lblOccasionDuration.AutoSize = true;
            this.lblOccasionDuration.Location = new System.Drawing.Point(61, 21);
            this.lblOccasionDuration.Name = "lblOccasionDuration";
            this.lblOccasionDuration.Size = new System.Drawing.Size(35, 13);
            this.lblOccasionDuration.TabIndex = 80;
            this.lblOccasionDuration.Text = "label2";
            // 
            // lblOccasionTime
            // 
            this.lblOccasionTime.AutoSize = true;
            this.lblOccasionTime.Location = new System.Drawing.Point(61, 0);
            this.lblOccasionTime.Name = "lblOccasionTime";
            this.lblOccasionTime.Size = new System.Drawing.Size(35, 13);
            this.lblOccasionTime.TabIndex = 74;
            this.lblOccasionTime.Text = "label2";
            // 
            // label239
            // 
            this.label239.AutoSize = true;
            this.label239.Location = new System.Drawing.Point(3, 21);
            this.label239.Name = "label239";
            this.label239.Size = new System.Drawing.Size(50, 13);
            this.label239.TabIndex = 79;
            this.label239.Text = "Duration:";
            // 
            // grpOccasionEvents
            // 
            this.grpOccasionEvents.Controls.Add(this.lstOccasionEvents);
            this.grpOccasionEvents.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpOccasionEvents.Location = new System.Drawing.Point(203, 3);
            this.grpOccasionEvents.Name = "grpOccasionEvents";
            this.grpOccasionEvents.Size = new System.Drawing.Size(1486, 405);
            this.grpOccasionEvents.TabIndex = 81;
            this.grpOccasionEvents.TabStop = false;
            this.grpOccasionEvents.Text = "Events";
            // 
            // lstOccasionEvents
            // 
            this.lstOccasionEvents.Dock = System.Windows.Forms.DockStyle.Top;
            this.lstOccasionEvents.FormattingEnabled = true;
            this.lstOccasionEvents.Location = new System.Drawing.Point(3, 16);
            this.lstOccasionEvents.Name = "lstOccasionEvents";
            this.lstOccasionEvents.Size = new System.Drawing.Size(1480, 173);
            this.lstOccasionEvents.TabIndex = 0;
            // 
            // tabEventCollectionCeremony
            // 
            this.tabEventCollectionCeremony.Controls.Add(this.tableLayoutPanel58);
            this.tabEventCollectionCeremony.Location = new System.Drawing.Point(4, 22);
            this.tabEventCollectionCeremony.Margin = new System.Windows.Forms.Padding(2);
            this.tabEventCollectionCeremony.Name = "tabEventCollectionCeremony";
            this.tabEventCollectionCeremony.Size = new System.Drawing.Size(1692, 840);
            this.tabEventCollectionCeremony.TabIndex = 10;
            this.tabEventCollectionCeremony.Text = "Ceremony";
            this.tabEventCollectionCeremony.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel58
            // 
            this.tableLayoutPanel58.ColumnCount = 2;
            this.tableLayoutPanel58.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tableLayoutPanel58.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel58.Controls.Add(this.panel23, 0, 0);
            this.tableLayoutPanel58.Controls.Add(this.grpCeremonyEvents, 1, 0);
            this.tableLayoutPanel58.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel58.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel58.Name = "tableLayoutPanel58";
            this.tableLayoutPanel58.RowCount = 1;
            this.tableLayoutPanel58.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel58.Size = new System.Drawing.Size(1692, 822);
            this.tableLayoutPanel58.TabIndex = 133;
            // 
            // panel23
            // 
            this.panel23.Controls.Add(this.label233);
            this.panel23.Controls.Add(this.lblCeremonyTime);
            this.panel23.Controls.Add(this.label236);
            this.panel23.Controls.Add(this.label240);
            this.panel23.Controls.Add(this.lblCeremonyOrdinal);
            this.panel23.Controls.Add(this.lblCeremonyDuration);
            this.panel23.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel23.Location = new System.Drawing.Point(3, 3);
            this.panel23.Name = "panel23";
            this.panel23.Size = new System.Drawing.Size(194, 816);
            this.panel23.TabIndex = 134;
            // 
            // label233
            // 
            this.label233.AutoSize = true;
            this.label233.Location = new System.Drawing.Point(3, 0);
            this.label233.Name = "label233";
            this.label233.Size = new System.Drawing.Size(33, 13);
            this.label233.TabIndex = 126;
            this.label233.Text = "Time:";
            // 
            // lblCeremonyTime
            // 
            this.lblCeremonyTime.AutoSize = true;
            this.lblCeremonyTime.Location = new System.Drawing.Point(61, 0);
            this.lblCeremonyTime.Name = "lblCeremonyTime";
            this.lblCeremonyTime.Size = new System.Drawing.Size(35, 13);
            this.lblCeremonyTime.TabIndex = 127;
            this.lblCeremonyTime.Text = "label2";
            // 
            // label236
            // 
            this.label236.AutoSize = true;
            this.label236.Location = new System.Drawing.Point(3, 44);
            this.label236.Name = "label236";
            this.label236.Size = new System.Drawing.Size(43, 13);
            this.label236.TabIndex = 132;
            this.label236.Text = "Ordinal:";
            // 
            // label240
            // 
            this.label240.AutoSize = true;
            this.label240.Location = new System.Drawing.Point(3, 21);
            this.label240.Name = "label240";
            this.label240.Size = new System.Drawing.Size(50, 13);
            this.label240.TabIndex = 128;
            this.label240.Text = "Duration:";
            // 
            // lblCeremonyOrdinal
            // 
            this.lblCeremonyOrdinal.AutoSize = true;
            this.lblCeremonyOrdinal.Location = new System.Drawing.Point(61, 44);
            this.lblCeremonyOrdinal.Name = "lblCeremonyOrdinal";
            this.lblCeremonyOrdinal.Size = new System.Drawing.Size(35, 13);
            this.lblCeremonyOrdinal.TabIndex = 131;
            this.lblCeremonyOrdinal.Text = "label2";
            // 
            // lblCeremonyDuration
            // 
            this.lblCeremonyDuration.AutoSize = true;
            this.lblCeremonyDuration.Location = new System.Drawing.Point(61, 21);
            this.lblCeremonyDuration.Name = "lblCeremonyDuration";
            this.lblCeremonyDuration.Size = new System.Drawing.Size(35, 13);
            this.lblCeremonyDuration.TabIndex = 129;
            this.lblCeremonyDuration.Text = "label2";
            // 
            // grpCeremonyEvents
            // 
            this.grpCeremonyEvents.Controls.Add(this.lstCeremonyEvents);
            this.grpCeremonyEvents.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpCeremonyEvents.Location = new System.Drawing.Point(203, 3);
            this.grpCeremonyEvents.Name = "grpCeremonyEvents";
            this.grpCeremonyEvents.Size = new System.Drawing.Size(1486, 816);
            this.grpCeremonyEvents.TabIndex = 130;
            this.grpCeremonyEvents.TabStop = false;
            this.grpCeremonyEvents.Text = "Events";
            // 
            // lstCeremonyEvents
            // 
            this.lstCeremonyEvents.Dock = System.Windows.Forms.DockStyle.Top;
            this.lstCeremonyEvents.FormattingEnabled = true;
            this.lstCeremonyEvents.Location = new System.Drawing.Point(3, 16);
            this.lstCeremonyEvents.Name = "lstCeremonyEvents";
            this.lstCeremonyEvents.Size = new System.Drawing.Size(1480, 147);
            this.lstCeremonyEvents.TabIndex = 0;
            // 
            // tabEventCollectionProcession
            // 
            this.tabEventCollectionProcession.Controls.Add(this.tableLayoutPanel59);
            this.tabEventCollectionProcession.Location = new System.Drawing.Point(4, 22);
            this.tabEventCollectionProcession.Margin = new System.Windows.Forms.Padding(2);
            this.tabEventCollectionProcession.Name = "tabEventCollectionProcession";
            this.tabEventCollectionProcession.Size = new System.Drawing.Size(1692, 840);
            this.tabEventCollectionProcession.TabIndex = 11;
            this.tabEventCollectionProcession.Text = "Procession";
            this.tabEventCollectionProcession.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel59
            // 
            this.tableLayoutPanel59.ColumnCount = 2;
            this.tableLayoutPanel59.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tableLayoutPanel59.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel59.Controls.Add(this.panel24, 0, 0);
            this.tableLayoutPanel59.Controls.Add(this.grpProcessionEvents, 1, 0);
            this.tableLayoutPanel59.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel59.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel59.Name = "tableLayoutPanel59";
            this.tableLayoutPanel59.RowCount = 1;
            this.tableLayoutPanel59.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel59.Size = new System.Drawing.Size(1692, 822);
            this.tableLayoutPanel59.TabIndex = 133;
            // 
            // panel24
            // 
            this.panel24.Controls.Add(this.label234);
            this.panel24.Controls.Add(this.lblProcessionTime);
            this.panel24.Controls.Add(this.label238);
            this.panel24.Controls.Add(this.label241);
            this.panel24.Controls.Add(this.lblProcessionOrdinal);
            this.panel24.Controls.Add(this.lblProcessionDuration);
            this.panel24.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel24.Location = new System.Drawing.Point(3, 3);
            this.panel24.Name = "panel24";
            this.panel24.Size = new System.Drawing.Size(194, 816);
            this.panel24.TabIndex = 134;
            // 
            // label234
            // 
            this.label234.AutoSize = true;
            this.label234.Location = new System.Drawing.Point(3, 0);
            this.label234.Name = "label234";
            this.label234.Size = new System.Drawing.Size(33, 13);
            this.label234.TabIndex = 126;
            this.label234.Text = "Time:";
            // 
            // lblProcessionTime
            // 
            this.lblProcessionTime.AutoSize = true;
            this.lblProcessionTime.Location = new System.Drawing.Point(61, 0);
            this.lblProcessionTime.Name = "lblProcessionTime";
            this.lblProcessionTime.Size = new System.Drawing.Size(35, 13);
            this.lblProcessionTime.TabIndex = 127;
            this.lblProcessionTime.Text = "label2";
            // 
            // label238
            // 
            this.label238.AutoSize = true;
            this.label238.Location = new System.Drawing.Point(3, 44);
            this.label238.Name = "label238";
            this.label238.Size = new System.Drawing.Size(43, 13);
            this.label238.TabIndex = 132;
            this.label238.Text = "Ordinal:";
            // 
            // label241
            // 
            this.label241.AutoSize = true;
            this.label241.Location = new System.Drawing.Point(3, 21);
            this.label241.Name = "label241";
            this.label241.Size = new System.Drawing.Size(50, 13);
            this.label241.TabIndex = 128;
            this.label241.Text = "Duration:";
            // 
            // lblProcessionOrdinal
            // 
            this.lblProcessionOrdinal.AutoSize = true;
            this.lblProcessionOrdinal.Location = new System.Drawing.Point(61, 44);
            this.lblProcessionOrdinal.Name = "lblProcessionOrdinal";
            this.lblProcessionOrdinal.Size = new System.Drawing.Size(35, 13);
            this.lblProcessionOrdinal.TabIndex = 131;
            this.lblProcessionOrdinal.Text = "label2";
            // 
            // lblProcessionDuration
            // 
            this.lblProcessionDuration.AutoSize = true;
            this.lblProcessionDuration.Location = new System.Drawing.Point(61, 21);
            this.lblProcessionDuration.Name = "lblProcessionDuration";
            this.lblProcessionDuration.Size = new System.Drawing.Size(35, 13);
            this.lblProcessionDuration.TabIndex = 129;
            this.lblProcessionDuration.Text = "label2";
            // 
            // grpProcessionEvents
            // 
            this.grpProcessionEvents.Controls.Add(this.lstProcessionEvents);
            this.grpProcessionEvents.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpProcessionEvents.Location = new System.Drawing.Point(203, 3);
            this.grpProcessionEvents.Name = "grpProcessionEvents";
            this.grpProcessionEvents.Size = new System.Drawing.Size(1486, 816);
            this.grpProcessionEvents.TabIndex = 130;
            this.grpProcessionEvents.TabStop = false;
            this.grpProcessionEvents.Text = "Events";
            // 
            // lstProcessionEvents
            // 
            this.lstProcessionEvents.Dock = System.Windows.Forms.DockStyle.Top;
            this.lstProcessionEvents.FormattingEnabled = true;
            this.lstProcessionEvents.Location = new System.Drawing.Point(3, 16);
            this.lstProcessionEvents.Name = "lstProcessionEvents";
            this.lstProcessionEvents.Size = new System.Drawing.Size(1480, 147);
            this.lstProcessionEvents.TabIndex = 0;
            // 
            // tabEventCollectionPerformance
            // 
            this.tabEventCollectionPerformance.Controls.Add(this.tableLayoutPanel60);
            this.tabEventCollectionPerformance.Location = new System.Drawing.Point(4, 22);
            this.tabEventCollectionPerformance.Margin = new System.Windows.Forms.Padding(2);
            this.tabEventCollectionPerformance.Name = "tabEventCollectionPerformance";
            this.tabEventCollectionPerformance.Size = new System.Drawing.Size(1692, 840);
            this.tabEventCollectionPerformance.TabIndex = 12;
            this.tabEventCollectionPerformance.Text = "Performance";
            this.tabEventCollectionPerformance.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel60
            // 
            this.tableLayoutPanel60.ColumnCount = 2;
            this.tableLayoutPanel60.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tableLayoutPanel60.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel60.Controls.Add(this.panel25, 0, 0);
            this.tableLayoutPanel60.Controls.Add(this.grpPerformanceEvents, 1, 0);
            this.tableLayoutPanel60.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel60.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel60.Name = "tableLayoutPanel60";
            this.tableLayoutPanel60.RowCount = 1;
            this.tableLayoutPanel60.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel60.Size = new System.Drawing.Size(1692, 822);
            this.tableLayoutPanel60.TabIndex = 133;
            // 
            // panel25
            // 
            this.panel25.Controls.Add(this.label237);
            this.panel25.Controls.Add(this.lblPerformanceTime);
            this.panel25.Controls.Add(this.label243);
            this.panel25.Controls.Add(this.label244);
            this.panel25.Controls.Add(this.lblPerformanceOrdinal);
            this.panel25.Controls.Add(this.lblPerformanceDuration);
            this.panel25.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel25.Location = new System.Drawing.Point(3, 3);
            this.panel25.Name = "panel25";
            this.panel25.Size = new System.Drawing.Size(194, 816);
            this.panel25.TabIndex = 134;
            // 
            // label237
            // 
            this.label237.AutoSize = true;
            this.label237.Location = new System.Drawing.Point(3, 0);
            this.label237.Name = "label237";
            this.label237.Size = new System.Drawing.Size(33, 13);
            this.label237.TabIndex = 126;
            this.label237.Text = "Time:";
            // 
            // lblPerformanceTime
            // 
            this.lblPerformanceTime.AutoSize = true;
            this.lblPerformanceTime.Location = new System.Drawing.Point(61, 0);
            this.lblPerformanceTime.Name = "lblPerformanceTime";
            this.lblPerformanceTime.Size = new System.Drawing.Size(35, 13);
            this.lblPerformanceTime.TabIndex = 127;
            this.lblPerformanceTime.Text = "label2";
            // 
            // label243
            // 
            this.label243.AutoSize = true;
            this.label243.Location = new System.Drawing.Point(3, 44);
            this.label243.Name = "label243";
            this.label243.Size = new System.Drawing.Size(43, 13);
            this.label243.TabIndex = 132;
            this.label243.Text = "Ordinal:";
            // 
            // label244
            // 
            this.label244.AutoSize = true;
            this.label244.Location = new System.Drawing.Point(3, 21);
            this.label244.Name = "label244";
            this.label244.Size = new System.Drawing.Size(50, 13);
            this.label244.TabIndex = 128;
            this.label244.Text = "Duration:";
            // 
            // lblPerformanceOrdinal
            // 
            this.lblPerformanceOrdinal.AutoSize = true;
            this.lblPerformanceOrdinal.Location = new System.Drawing.Point(61, 44);
            this.lblPerformanceOrdinal.Name = "lblPerformanceOrdinal";
            this.lblPerformanceOrdinal.Size = new System.Drawing.Size(35, 13);
            this.lblPerformanceOrdinal.TabIndex = 131;
            this.lblPerformanceOrdinal.Text = "label2";
            // 
            // lblPerformanceDuration
            // 
            this.lblPerformanceDuration.AutoSize = true;
            this.lblPerformanceDuration.Location = new System.Drawing.Point(61, 21);
            this.lblPerformanceDuration.Name = "lblPerformanceDuration";
            this.lblPerformanceDuration.Size = new System.Drawing.Size(35, 13);
            this.lblPerformanceDuration.TabIndex = 129;
            this.lblPerformanceDuration.Text = "label2";
            // 
            // grpPerformanceEvents
            // 
            this.grpPerformanceEvents.Controls.Add(this.lstPerformanceEvents);
            this.grpPerformanceEvents.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpPerformanceEvents.Location = new System.Drawing.Point(203, 3);
            this.grpPerformanceEvents.Name = "grpPerformanceEvents";
            this.grpPerformanceEvents.Size = new System.Drawing.Size(1486, 816);
            this.grpPerformanceEvents.TabIndex = 130;
            this.grpPerformanceEvents.TabStop = false;
            this.grpPerformanceEvents.Text = "Events";
            // 
            // lstPerformanceEvents
            // 
            this.lstPerformanceEvents.Dock = System.Windows.Forms.DockStyle.Top;
            this.lstPerformanceEvents.FormattingEnabled = true;
            this.lstPerformanceEvents.Location = new System.Drawing.Point(3, 16);
            this.lstPerformanceEvents.Name = "lstPerformanceEvents";
            this.lstPerformanceEvents.Size = new System.Drawing.Size(1480, 147);
            this.lstPerformanceEvents.TabIndex = 0;
            // 
            // tabEventCollectionCompetition
            // 
            this.tabEventCollectionCompetition.Controls.Add(this.tableLayoutPanel61);
            this.tabEventCollectionCompetition.Location = new System.Drawing.Point(4, 22);
            this.tabEventCollectionCompetition.Margin = new System.Windows.Forms.Padding(2);
            this.tabEventCollectionCompetition.Name = "tabEventCollectionCompetition";
            this.tabEventCollectionCompetition.Size = new System.Drawing.Size(1692, 840);
            this.tabEventCollectionCompetition.TabIndex = 13;
            this.tabEventCollectionCompetition.Text = "Competition";
            this.tabEventCollectionCompetition.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel61
            // 
            this.tableLayoutPanel61.ColumnCount = 2;
            this.tableLayoutPanel61.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tableLayoutPanel61.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel61.Controls.Add(this.panel26, 0, 0);
            this.tableLayoutPanel61.Controls.Add(this.grpCompetitionEvents, 1, 0);
            this.tableLayoutPanel61.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel61.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel61.Name = "tableLayoutPanel61";
            this.tableLayoutPanel61.RowCount = 1;
            this.tableLayoutPanel61.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel61.Size = new System.Drawing.Size(1692, 822);
            this.tableLayoutPanel61.TabIndex = 133;
            // 
            // panel26
            // 
            this.panel26.Controls.Add(this.label242);
            this.panel26.Controls.Add(this.lblCompetitionTime);
            this.panel26.Controls.Add(this.label246);
            this.panel26.Controls.Add(this.label247);
            this.panel26.Controls.Add(this.lblCompetitionOrdinal);
            this.panel26.Controls.Add(this.lblCompetitionDuration);
            this.panel26.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel26.Location = new System.Drawing.Point(3, 3);
            this.panel26.Name = "panel26";
            this.panel26.Size = new System.Drawing.Size(194, 816);
            this.panel26.TabIndex = 134;
            // 
            // label242
            // 
            this.label242.AutoSize = true;
            this.label242.Location = new System.Drawing.Point(3, 0);
            this.label242.Name = "label242";
            this.label242.Size = new System.Drawing.Size(33, 13);
            this.label242.TabIndex = 126;
            this.label242.Text = "Time:";
            // 
            // lblCompetitionTime
            // 
            this.lblCompetitionTime.AutoSize = true;
            this.lblCompetitionTime.Location = new System.Drawing.Point(61, 0);
            this.lblCompetitionTime.Name = "lblCompetitionTime";
            this.lblCompetitionTime.Size = new System.Drawing.Size(35, 13);
            this.lblCompetitionTime.TabIndex = 127;
            this.lblCompetitionTime.Text = "label2";
            // 
            // label246
            // 
            this.label246.AutoSize = true;
            this.label246.Location = new System.Drawing.Point(3, 44);
            this.label246.Name = "label246";
            this.label246.Size = new System.Drawing.Size(43, 13);
            this.label246.TabIndex = 132;
            this.label246.Text = "Ordinal:";
            // 
            // label247
            // 
            this.label247.AutoSize = true;
            this.label247.Location = new System.Drawing.Point(3, 21);
            this.label247.Name = "label247";
            this.label247.Size = new System.Drawing.Size(50, 13);
            this.label247.TabIndex = 128;
            this.label247.Text = "Duration:";
            // 
            // lblCompetitionOrdinal
            // 
            this.lblCompetitionOrdinal.AutoSize = true;
            this.lblCompetitionOrdinal.Location = new System.Drawing.Point(61, 44);
            this.lblCompetitionOrdinal.Name = "lblCompetitionOrdinal";
            this.lblCompetitionOrdinal.Size = new System.Drawing.Size(35, 13);
            this.lblCompetitionOrdinal.TabIndex = 131;
            this.lblCompetitionOrdinal.Text = "label2";
            // 
            // lblCompetitionDuration
            // 
            this.lblCompetitionDuration.AutoSize = true;
            this.lblCompetitionDuration.Location = new System.Drawing.Point(61, 21);
            this.lblCompetitionDuration.Name = "lblCompetitionDuration";
            this.lblCompetitionDuration.Size = new System.Drawing.Size(35, 13);
            this.lblCompetitionDuration.TabIndex = 129;
            this.lblCompetitionDuration.Text = "label2";
            // 
            // grpCompetitionEvents
            // 
            this.grpCompetitionEvents.Controls.Add(this.lstCompetitionEvents);
            this.grpCompetitionEvents.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpCompetitionEvents.Location = new System.Drawing.Point(203, 3);
            this.grpCompetitionEvents.Name = "grpCompetitionEvents";
            this.grpCompetitionEvents.Size = new System.Drawing.Size(1486, 816);
            this.grpCompetitionEvents.TabIndex = 130;
            this.grpCompetitionEvents.TabStop = false;
            this.grpCompetitionEvents.Text = "Events";
            // 
            // lstCompetitionEvents
            // 
            this.lstCompetitionEvents.Dock = System.Windows.Forms.DockStyle.Top;
            this.lstCompetitionEvents.FormattingEnabled = true;
            this.lstCompetitionEvents.Location = new System.Drawing.Point(3, 16);
            this.lstCompetitionEvents.Name = "lstCompetitionEvents";
            this.lstCompetitionEvents.Size = new System.Drawing.Size(1480, 147);
            this.lstCompetitionEvents.TabIndex = 0;
            // 
            // tabEventCollectionPurge
            // 
            this.tabEventCollectionPurge.Controls.Add(this.tableLayoutPanel45);
            this.tabEventCollectionPurge.Location = new System.Drawing.Point(4, 22);
            this.tabEventCollectionPurge.Margin = new System.Windows.Forms.Padding(2);
            this.tabEventCollectionPurge.Name = "tabEventCollectionPurge";
            this.tabEventCollectionPurge.Size = new System.Drawing.Size(1692, 840);
            this.tabEventCollectionPurge.TabIndex = 14;
            this.tabEventCollectionPurge.Text = "Purge";
            this.tabEventCollectionPurge.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel45
            // 
            this.tableLayoutPanel45.ColumnCount = 2;
            this.tableLayoutPanel45.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tableLayoutPanel45.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel45.Controls.Add(this.groupBox1, 1, 0);
            this.tableLayoutPanel45.Controls.Add(this.panel27, 0, 0);
            this.tableLayoutPanel45.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel45.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel45.Name = "tableLayoutPanel45";
            this.tableLayoutPanel45.RowCount = 1;
            this.tableLayoutPanel45.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel45.Size = new System.Drawing.Size(1692, 822);
            this.tableLayoutPanel45.TabIndex = 72;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lstPurgeEvents);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(203, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1486, 816);
            this.groupBox1.TabIndex = 63;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Events";
            // 
            // lstPurgeEvents
            // 
            this.lstPurgeEvents.Dock = System.Windows.Forms.DockStyle.Top;
            this.lstPurgeEvents.FormattingEnabled = true;
            this.lstPurgeEvents.Location = new System.Drawing.Point(3, 16);
            this.lstPurgeEvents.Name = "lstPurgeEvents";
            this.lstPurgeEvents.Size = new System.Drawing.Size(1480, 173);
            this.lstPurgeEvents.TabIndex = 0;
            // 
            // panel27
            // 
            this.panel27.Controls.Add(this.lblPurgeOrdinal);
            this.panel27.Controls.Add(this.label205);
            this.panel27.Controls.Add(this.lblPurgeSite);
            this.panel27.Controls.Add(this.lblPurgeAdjective);
            this.panel27.Controls.Add(this.label301);
            this.panel27.Controls.Add(this.lblPurgeDuration);
            this.panel27.Controls.Add(this.lblPurgeTime);
            this.panel27.Controls.Add(this.label315);
            this.panel27.Controls.Add(this.label318);
            this.panel27.Controls.Add(this.label320);
            this.panel27.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel27.Location = new System.Drawing.Point(3, 3);
            this.panel27.Name = "panel27";
            this.panel27.Size = new System.Drawing.Size(194, 816);
            this.panel27.TabIndex = 71;
            // 
            // lblPurgeOrdinal
            // 
            this.lblPurgeOrdinal.AutoSize = true;
            this.lblPurgeOrdinal.Location = new System.Drawing.Point(60, 88);
            this.lblPurgeOrdinal.Name = "lblPurgeOrdinal";
            this.lblPurgeOrdinal.Size = new System.Drawing.Size(35, 13);
            this.lblPurgeOrdinal.TabIndex = 71;
            this.lblPurgeOrdinal.Text = "label2";
            // 
            // label205
            // 
            this.label205.AutoSize = true;
            this.label205.Location = new System.Drawing.Point(3, 0);
            this.label205.Name = "label205";
            this.label205.Size = new System.Drawing.Size(33, 13);
            this.label205.TabIndex = 53;
            this.label205.Text = "Time:";
            // 
            // lblPurgeAdjective
            // 
            this.lblPurgeAdjective.AutoSize = true;
            this.lblPurgeAdjective.Location = new System.Drawing.Point(60, 66);
            this.lblPurgeAdjective.Name = "lblPurgeAdjective";
            this.lblPurgeAdjective.Size = new System.Drawing.Size(35, 13);
            this.lblPurgeAdjective.TabIndex = 69;
            this.lblPurgeAdjective.Text = "label2";
            // 
            // label301
            // 
            this.label301.AutoSize = true;
            this.label301.Location = new System.Drawing.Point(3, 44);
            this.label301.Name = "label301";
            this.label301.Size = new System.Drawing.Size(28, 13);
            this.label301.TabIndex = 51;
            this.label301.Text = "Site:";
            // 
            // lblPurgeDuration
            // 
            this.lblPurgeDuration.AutoSize = true;
            this.lblPurgeDuration.Location = new System.Drawing.Point(61, 21);
            this.lblPurgeDuration.Name = "lblPurgeDuration";
            this.lblPurgeDuration.Size = new System.Drawing.Size(35, 13);
            this.lblPurgeDuration.TabIndex = 62;
            this.lblPurgeDuration.Text = "label2";
            // 
            // lblPurgeTime
            // 
            this.lblPurgeTime.AutoSize = true;
            this.lblPurgeTime.Location = new System.Drawing.Point(61, 0);
            this.lblPurgeTime.Name = "lblPurgeTime";
            this.lblPurgeTime.Size = new System.Drawing.Size(35, 13);
            this.lblPurgeTime.TabIndex = 54;
            this.lblPurgeTime.Text = "label2";
            // 
            // label315
            // 
            this.label315.AutoSize = true;
            this.label315.Location = new System.Drawing.Point(3, 21);
            this.label315.Name = "label315";
            this.label315.Size = new System.Drawing.Size(50, 13);
            this.label315.TabIndex = 61;
            this.label315.Text = "Duration:";
            // 
            // label318
            // 
            this.label318.AutoSize = true;
            this.label318.Location = new System.Drawing.Point(3, 66);
            this.label318.Name = "label318";
            this.label318.Size = new System.Drawing.Size(54, 13);
            this.label318.TabIndex = 57;
            this.label318.Text = "Adjective:";
            // 
            // label320
            // 
            this.label320.AutoSize = true;
            this.label320.Location = new System.Drawing.Point(2, 88);
            this.label320.Name = "label320";
            this.label320.Size = new System.Drawing.Size(43, 13);
            this.label320.TabIndex = 59;
            this.label320.Text = "Ordinal:";
            // 
            // FilterHistoricalEventCollection
            // 
            this.FilterHistoricalEventCollection.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FilterHistoricalEventCollection.Location = new System.Drawing.Point(3, 866);
            this.FilterHistoricalEventCollection.Name = "FilterHistoricalEventCollection";
            this.FilterHistoricalEventCollection.Size = new System.Drawing.Size(155, 22);
            this.FilterHistoricalEventCollection.TabIndex = 6;
            this.FilterHistoricalEventCollection.Tag = "";
            this.FilterHistoricalEventCollection.Text = "Filter";
            this.FilterHistoricalEventCollection.UseVisualStyleBackColor = true;
            this.FilterHistoricalEventCollection.Click += new System.EventHandler(this.FilterButton_Click);
            // 
            // lstHistoricalEventCollection
            // 
            this.lstHistoricalEventCollection.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstHistoricalEventCollection.FormattingEnabled = true;
            this.lstHistoricalEventCollection.Location = new System.Drawing.Point(3, 3);
            this.lstHistoricalEventCollection.Name = "lstHistoricalEventCollection";
            this.lstHistoricalEventCollection.Size = new System.Drawing.Size(155, 833);
            this.lstHistoricalEventCollection.TabIndex = 2;
            // 
            // tabLeader
            // 
            this.tabLeader.Controls.Add(this.tableLayoutPanel11);
            this.tabLeader.Location = new System.Drawing.Point(4, 40);
            this.tabLeader.Name = "tabLeader";
            this.tabLeader.Size = new System.Drawing.Size(1873, 873);
            this.tabLeader.TabIndex = 10;
            this.tabLeader.Text = "Leader";
            this.tabLeader.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel11
            // 
            this.tableLayoutPanel11.ColumnCount = 2;
            this.tableLayoutPanel11.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 176F));
            this.tableLayoutPanel11.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel11.Controls.Add(this.TextFilterLeader, 0, 1);
            this.tableLayoutPanel11.Controls.Add(this.grpLeader, 1, 0);
            this.tableLayoutPanel11.Controls.Add(this.FilterLeader, 0, 2);
            this.tableLayoutPanel11.Controls.Add(this.lstLeader, 0, 0);
            this.tableLayoutPanel11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel11.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel11.Margin = new System.Windows.Forms.Padding(2);
            this.tableLayoutPanel11.Name = "tableLayoutPanel11";
            this.tableLayoutPanel11.RowCount = 3;
            this.tableLayoutPanel11.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel11.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel11.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanel11.Size = new System.Drawing.Size(1873, 891);
            this.tableLayoutPanel11.TabIndex = 8;
            // 
            // TextFilterLeader
            // 
            this.TextFilterLeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TextFilterLeader.Location = new System.Drawing.Point(3, 842);
            this.TextFilterLeader.Name = "TextFilterLeader";
            this.TextFilterLeader.Size = new System.Drawing.Size(170, 20);
            this.TextFilterLeader.TabIndex = 5;
            this.TextFilterLeader.TextChanged += new System.EventHandler(this.TextFilter_Changed);
            // 
            // grpLeader
            // 
            this.grpLeader.Controls.Add(this.lblLeaderMarried);
            this.grpLeader.Controls.Add(this.label176);
            this.grpLeader.Controls.Add(this.lblLeaderType);
            this.grpLeader.Controls.Add(this.lblLeaderHF);
            this.grpLeader.Controls.Add(this.label51);
            this.grpLeader.Controls.Add(this.label67);
            this.grpLeader.Controls.Add(this.lblLeaderInheritedFrom);
            this.grpLeader.Controls.Add(this.lblLeaderRace);
            this.grpLeader.Controls.Add(this.lblLeaderLife);
            this.grpLeader.Controls.Add(this.lblLeaderGod);
            this.grpLeader.Controls.Add(this.lblLeaderCivilization);
            this.grpLeader.Controls.Add(this.lblLeaderSite);
            this.grpLeader.Controls.Add(this.label64);
            this.grpLeader.Controls.Add(this.label65);
            this.grpLeader.Controls.Add(this.label66);
            this.grpLeader.Controls.Add(this.label68);
            this.grpLeader.Controls.Add(this.lblLeaderInheritance);
            this.grpLeader.Controls.Add(this.label70);
            this.grpLeader.Controls.Add(this.label71);
            this.grpLeader.Controls.Add(this.lblLeaderReignBegan);
            this.grpLeader.Controls.Add(this.label73);
            this.grpLeader.Controls.Add(this.label74);
            this.grpLeader.Controls.Add(this.lblLeaderName);
            this.grpLeader.Controls.Add(this.label76);
            this.grpLeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpLeader.Location = new System.Drawing.Point(179, 3);
            this.grpLeader.Name = "grpLeader";
            this.tableLayoutPanel11.SetRowSpan(this.grpLeader, 3);
            this.grpLeader.Size = new System.Drawing.Size(1691, 885);
            this.grpLeader.TabIndex = 3;
            this.grpLeader.TabStop = false;
            this.grpLeader.Visible = false;
            // 
            // label176
            // 
            this.label176.AutoSize = true;
            this.label176.Location = new System.Drawing.Point(12, 237);
            this.label176.Name = "label176";
            this.label176.Size = new System.Drawing.Size(45, 13);
            this.label176.TabIndex = 91;
            this.label176.Text = "Married:";
            // 
            // lblLeaderType
            // 
            this.lblLeaderType.AutoSize = true;
            this.lblLeaderType.Location = new System.Drawing.Point(101, 42);
            this.lblLeaderType.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblLeaderType.Name = "lblLeaderType";
            this.lblLeaderType.Size = new System.Drawing.Size(41, 13);
            this.lblLeaderType.TabIndex = 90;
            this.lblLeaderType.Text = "label53";
            // 
            // label51
            // 
            this.label51.AutoSize = true;
            this.label51.Location = new System.Drawing.Point(12, 260);
            this.label51.Name = "label51";
            this.label51.Size = new System.Drawing.Size(35, 13);
            this.label51.TabIndex = 88;
            this.label51.Text = "Is HF:";
            // 
            // label67
            // 
            this.label67.AutoSize = true;
            this.label67.Location = new System.Drawing.Point(12, 42);
            this.label67.Name = "label67";
            this.label67.Size = new System.Drawing.Size(34, 13);
            this.label67.TabIndex = 86;
            this.label67.Text = "Type:";
            // 
            // lblLeaderLife
            // 
            this.lblLeaderLife.AutoSize = true;
            this.lblLeaderLife.Location = new System.Drawing.Point(101, 87);
            this.lblLeaderLife.Name = "lblLeaderLife";
            this.lblLeaderLife.Size = new System.Drawing.Size(35, 13);
            this.lblLeaderLife.TabIndex = 81;
            this.lblLeaderLife.Text = "label2";
            // 
            // label64
            // 
            this.label64.AutoSize = true;
            this.label64.Location = new System.Drawing.Point(12, 215);
            this.label64.Name = "label64";
            this.label64.Size = new System.Drawing.Size(30, 13);
            this.label64.TabIndex = 73;
            this.label64.Text = "God:";
            // 
            // label65
            // 
            this.label65.AutoSize = true;
            this.label65.Location = new System.Drawing.Point(12, 193);
            this.label65.Name = "label65";
            this.label65.Size = new System.Drawing.Size(28, 13);
            this.label65.TabIndex = 72;
            this.label65.Text = "Site:";
            // 
            // label66
            // 
            this.label66.AutoSize = true;
            this.label66.Location = new System.Drawing.Point(12, 172);
            this.label66.Name = "label66";
            this.label66.Size = new System.Drawing.Size(59, 13);
            this.label66.TabIndex = 71;
            this.label66.Text = "Civilization:";
            // 
            // label68
            // 
            this.label68.AutoSize = true;
            this.label68.Location = new System.Drawing.Point(12, 151);
            this.label68.Name = "label68";
            this.label68.Size = new System.Drawing.Size(33, 13);
            this.label68.TabIndex = 69;
            this.label68.Text = "From:";
            // 
            // lblLeaderInheritance
            // 
            this.lblLeaderInheritance.AutoSize = true;
            this.lblLeaderInheritance.Location = new System.Drawing.Point(101, 130);
            this.lblLeaderInheritance.Name = "lblLeaderInheritance";
            this.lblLeaderInheritance.Size = new System.Drawing.Size(35, 13);
            this.lblLeaderInheritance.TabIndex = 68;
            this.lblLeaderInheritance.Text = "label2";
            // 
            // label70
            // 
            this.label70.AutoSize = true;
            this.label70.Location = new System.Drawing.Point(12, 130);
            this.label70.Name = "label70";
            this.label70.Size = new System.Drawing.Size(63, 13);
            this.label70.TabIndex = 67;
            this.label70.Text = "Inheritance:";
            // 
            // label71
            // 
            this.label71.AutoSize = true;
            this.label71.Location = new System.Drawing.Point(12, 87);
            this.label71.Name = "label71";
            this.label71.Size = new System.Drawing.Size(27, 13);
            this.label71.TabIndex = 66;
            this.label71.Text = "Life:";
            // 
            // lblLeaderReignBegan
            // 
            this.lblLeaderReignBegan.AutoSize = true;
            this.lblLeaderReignBegan.Location = new System.Drawing.Point(101, 108);
            this.lblLeaderReignBegan.Name = "lblLeaderReignBegan";
            this.lblLeaderReignBegan.Size = new System.Drawing.Size(35, 13);
            this.lblLeaderReignBegan.TabIndex = 65;
            this.lblLeaderReignBegan.Text = "label2";
            // 
            // label73
            // 
            this.label73.AutoSize = true;
            this.label73.Location = new System.Drawing.Point(12, 108);
            this.label73.Name = "label73";
            this.label73.Size = new System.Drawing.Size(72, 13);
            this.label73.TabIndex = 64;
            this.label73.Text = "Reign Began:";
            // 
            // label74
            // 
            this.label74.AutoSize = true;
            this.label74.Location = new System.Drawing.Point(12, 66);
            this.label74.Name = "label74";
            this.label74.Size = new System.Drawing.Size(36, 13);
            this.label74.TabIndex = 63;
            this.label74.Text = "Race:";
            // 
            // lblLeaderName
            // 
            this.lblLeaderName.AutoSize = true;
            this.lblLeaderName.Location = new System.Drawing.Point(101, 21);
            this.lblLeaderName.Name = "lblLeaderName";
            this.lblLeaderName.Size = new System.Drawing.Size(35, 13);
            this.lblLeaderName.TabIndex = 62;
            this.lblLeaderName.Text = "label2";
            // 
            // label76
            // 
            this.label76.AutoSize = true;
            this.label76.Location = new System.Drawing.Point(12, 21);
            this.label76.Name = "label76";
            this.label76.Size = new System.Drawing.Size(41, 13);
            this.label76.TabIndex = 61;
            this.label76.Text = "Name: ";
            // 
            // FilterLeader
            // 
            this.FilterLeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FilterLeader.Location = new System.Drawing.Point(3, 866);
            this.FilterLeader.Name = "FilterLeader";
            this.FilterLeader.Size = new System.Drawing.Size(170, 22);
            this.FilterLeader.TabIndex = 6;
            this.FilterLeader.Tag = "";
            this.FilterLeader.Text = "Filter";
            this.FilterLeader.UseVisualStyleBackColor = true;
            this.FilterLeader.Click += new System.EventHandler(this.FilterButton_Click);
            // 
            // lstLeader
            // 
            this.lstLeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstLeader.FormattingEnabled = true;
            this.lstLeader.Location = new System.Drawing.Point(3, 3);
            this.lstLeader.Name = "lstLeader";
            this.lstLeader.Size = new System.Drawing.Size(170, 833);
            this.lstLeader.TabIndex = 2;
            // 
            // tabParameter
            // 
            this.tabParameter.Controls.Add(this.tableLayoutPanel12);
            this.tabParameter.Location = new System.Drawing.Point(4, 40);
            this.tabParameter.Name = "tabParameter";
            this.tabParameter.Size = new System.Drawing.Size(1873, 873);
            this.tabParameter.TabIndex = 11;
            this.tabParameter.Text = "Parameter";
            this.tabParameter.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel12
            // 
            this.tableLayoutPanel12.ColumnCount = 2;
            this.tableLayoutPanel12.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 213F));
            this.tableLayoutPanel12.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel12.Controls.Add(this.FilterParameter, 0, 2);
            this.tableLayoutPanel12.Controls.Add(this.TextFilterParameter, 0, 1);
            this.tableLayoutPanel12.Controls.Add(this.grpParameter, 1, 0);
            this.tableLayoutPanel12.Controls.Add(this.lstParameter, 0, 0);
            this.tableLayoutPanel12.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel12.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel12.Margin = new System.Windows.Forms.Padding(2);
            this.tableLayoutPanel12.Name = "tableLayoutPanel12";
            this.tableLayoutPanel12.RowCount = 3;
            this.tableLayoutPanel12.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel12.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel12.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanel12.Size = new System.Drawing.Size(1873, 891);
            this.tableLayoutPanel12.TabIndex = 8;
            // 
            // FilterParameter
            // 
            this.FilterParameter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FilterParameter.Location = new System.Drawing.Point(3, 866);
            this.FilterParameter.Name = "FilterParameter";
            this.FilterParameter.Size = new System.Drawing.Size(207, 22);
            this.FilterParameter.TabIndex = 6;
            this.FilterParameter.Tag = "";
            this.FilterParameter.Text = "Filter";
            this.FilterParameter.UseVisualStyleBackColor = true;
            this.FilterParameter.Click += new System.EventHandler(this.FilterButton_Click);
            // 
            // TextFilterParameter
            // 
            this.TextFilterParameter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TextFilterParameter.Location = new System.Drawing.Point(3, 842);
            this.TextFilterParameter.Name = "TextFilterParameter";
            this.TextFilterParameter.Size = new System.Drawing.Size(207, 20);
            this.TextFilterParameter.TabIndex = 5;
            this.TextFilterParameter.TextChanged += new System.EventHandler(this.TextFilter_Changed);
            // 
            // grpParameter
            // 
            this.grpParameter.Controls.Add(this.lblParameterData);
            this.grpParameter.Controls.Add(this.label56);
            this.grpParameter.Controls.Add(this.lblParameterName);
            this.grpParameter.Controls.Add(this.label60);
            this.grpParameter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpParameter.Location = new System.Drawing.Point(216, 3);
            this.grpParameter.Name = "grpParameter";
            this.tableLayoutPanel12.SetRowSpan(this.grpParameter, 3);
            this.grpParameter.Size = new System.Drawing.Size(1654, 885);
            this.grpParameter.TabIndex = 3;
            this.grpParameter.TabStop = false;
            this.grpParameter.Visible = false;
            // 
            // lblParameterData
            // 
            this.lblParameterData.AutoSize = true;
            this.lblParameterData.Location = new System.Drawing.Point(67, 43);
            this.lblParameterData.Name = "lblParameterData";
            this.lblParameterData.Size = new System.Drawing.Size(35, 13);
            this.lblParameterData.TabIndex = 22;
            this.lblParameterData.Text = "label2";
            // 
            // label56
            // 
            this.label56.AutoSize = true;
            this.label56.Location = new System.Drawing.Point(12, 43);
            this.label56.Name = "label56";
            this.label56.Size = new System.Drawing.Size(33, 13);
            this.label56.TabIndex = 21;
            this.label56.Text = "Data:";
            // 
            // lblParameterName
            // 
            this.lblParameterName.AutoSize = true;
            this.lblParameterName.Location = new System.Drawing.Point(67, 21);
            this.lblParameterName.Name = "lblParameterName";
            this.lblParameterName.Size = new System.Drawing.Size(35, 13);
            this.lblParameterName.TabIndex = 20;
            this.lblParameterName.Text = "label2";
            // 
            // label60
            // 
            this.label60.AutoSize = true;
            this.label60.Location = new System.Drawing.Point(12, 21);
            this.label60.Name = "label60";
            this.label60.Size = new System.Drawing.Size(41, 13);
            this.label60.TabIndex = 19;
            this.label60.Text = "Name: ";
            // 
            // lstParameter
            // 
            this.lstParameter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstParameter.FormattingEnabled = true;
            this.lstParameter.Location = new System.Drawing.Point(3, 3);
            this.lstParameter.Name = "lstParameter";
            this.lstParameter.Size = new System.Drawing.Size(207, 833);
            this.lstParameter.TabIndex = 2;
            // 
            // tabRace
            // 
            this.tabRace.Controls.Add(this.tableLayoutPanel13);
            this.tabRace.Location = new System.Drawing.Point(4, 40);
            this.tabRace.Name = "tabRace";
            this.tabRace.Size = new System.Drawing.Size(1873, 873);
            this.tabRace.TabIndex = 12;
            this.tabRace.Text = "Race";
            this.tabRace.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel13
            // 
            this.tableLayoutPanel13.ColumnCount = 2;
            this.tableLayoutPanel13.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 176F));
            this.tableLayoutPanel13.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel13.Controls.Add(this.FilterRace, 0, 2);
            this.tableLayoutPanel13.Controls.Add(this.TextFilterRace, 0, 1);
            this.tableLayoutPanel13.Controls.Add(this.grpRace, 1, 0);
            this.tableLayoutPanel13.Controls.Add(this.lstRace, 0, 0);
            this.tableLayoutPanel13.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel13.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel13.Margin = new System.Windows.Forms.Padding(2);
            this.tableLayoutPanel13.Name = "tableLayoutPanel13";
            this.tableLayoutPanel13.RowCount = 3;
            this.tableLayoutPanel13.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel13.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel13.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanel13.Size = new System.Drawing.Size(1873, 891);
            this.tableLayoutPanel13.TabIndex = 8;
            // 
            // FilterRace
            // 
            this.FilterRace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FilterRace.Location = new System.Drawing.Point(3, 866);
            this.FilterRace.Name = "FilterRace";
            this.FilterRace.Size = new System.Drawing.Size(170, 22);
            this.FilterRace.TabIndex = 6;
            this.FilterRace.Tag = "";
            this.FilterRace.Text = "Filter";
            this.FilterRace.UseVisualStyleBackColor = true;
            this.FilterRace.Click += new System.EventHandler(this.FilterButton_Click);
            // 
            // TextFilterRace
            // 
            this.TextFilterRace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TextFilterRace.Location = new System.Drawing.Point(3, 842);
            this.TextFilterRace.Name = "TextFilterRace";
            this.TextFilterRace.Size = new System.Drawing.Size(170, 20);
            this.TextFilterRace.TabIndex = 5;
            this.TextFilterRace.TextChanged += new System.EventHandler(this.TextFilter_Changed);
            // 
            // grpRace
            // 
            this.grpRace.Controls.Add(this.tableLayoutPanel32);
            this.grpRace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpRace.Location = new System.Drawing.Point(179, 3);
            this.grpRace.Name = "grpRace";
            this.tableLayoutPanel13.SetRowSpan(this.grpRace, 3);
            this.grpRace.Size = new System.Drawing.Size(1691, 885);
            this.grpRace.TabIndex = 3;
            this.grpRace.TabStop = false;
            this.grpRace.Visible = false;
            // 
            // tableLayoutPanel32
            // 
            this.tableLayoutPanel32.ColumnCount = 3;
            this.tableLayoutPanel32.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel32.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.tableLayoutPanel32.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.tableLayoutPanel32.Controls.Add(this.grpRacePopulation, 1, 0);
            this.tableLayoutPanel32.Controls.Add(this.panel15, 0, 0);
            this.tableLayoutPanel32.Controls.Add(this.grpRaceHistoricalFigures, 2, 1);
            this.tableLayoutPanel32.Controls.Add(this.grpRaceLeaders, 0, 1);
            this.tableLayoutPanel32.Controls.Add(this.grpRaceCivilizations, 1, 1);
            this.tableLayoutPanel32.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel32.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel32.Name = "tableLayoutPanel32";
            this.tableLayoutPanel32.RowCount = 3;
            this.tableLayoutPanel32.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 199F));
            this.tableLayoutPanel32.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel32.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel32.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel32.Size = new System.Drawing.Size(1685, 866);
            this.tableLayoutPanel32.TabIndex = 28;
            // 
            // grpRacePopulation
            // 
            this.grpRacePopulation.Controls.Add(this.lstRacePopulation);
            this.grpRacePopulation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpRacePopulation.Location = new System.Drawing.Point(564, 3);
            this.grpRacePopulation.Name = "grpRacePopulation";
            this.grpRacePopulation.Size = new System.Drawing.Size(555, 193);
            this.grpRacePopulation.TabIndex = 44;
            this.grpRacePopulation.TabStop = false;
            this.grpRacePopulation.Text = "Population";
            // 
            // lstRacePopulation
            // 
            this.lstRacePopulation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstRacePopulation.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.lstRacePopulation.FormattingEnabled = true;
            this.lstRacePopulation.ItemHeight = 16;
            this.lstRacePopulation.Location = new System.Drawing.Point(3, 16);
            this.lstRacePopulation.Name = "lstRacePopulation";
            this.lstRacePopulation.Size = new System.Drawing.Size(549, 174);
            this.lstRacePopulation.TabIndex = 0;
            this.lstRacePopulation.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.lstRacePopulation_DrawItem);
            // 
            // panel15
            // 
            this.panel15.Controls.Add(this.grpRaceCastes);
            this.panel15.Controls.Add(this.label58);
            this.panel15.Controls.Add(this.lblRaceName);
            this.panel15.Controls.Add(this.lblRacePopulation);
            this.panel15.Controls.Add(this.label174);
            this.panel15.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel15.Location = new System.Drawing.Point(3, 3);
            this.panel15.Name = "panel15";
            this.panel15.Size = new System.Drawing.Size(555, 193);
            this.panel15.TabIndex = 29;
            // 
            // grpRaceCastes
            // 
            this.grpRaceCastes.Controls.Add(this.lblRaceCasteGender);
            this.grpRaceCastes.Controls.Add(this.label193);
            this.grpRaceCastes.Controls.Add(this.lblRaceCasteDescription);
            this.grpRaceCastes.Controls.Add(this.lstRaceCastes);
            this.grpRaceCastes.Location = new System.Drawing.Point(6, 47);
            this.grpRaceCastes.Name = "grpRaceCastes";
            this.grpRaceCastes.Size = new System.Drawing.Size(281, 143);
            this.grpRaceCastes.TabIndex = 31;
            this.grpRaceCastes.TabStop = false;
            this.grpRaceCastes.Text = "Castes";
            // 
            // lblRaceCasteGender
            // 
            this.lblRaceCasteGender.AutoSize = true;
            this.lblRaceCasteGender.Location = new System.Drawing.Point(150, 19);
            this.lblRaceCasteGender.Name = "lblRaceCasteGender";
            this.lblRaceCasteGender.Size = new System.Drawing.Size(13, 13);
            this.lblRaceCasteGender.TabIndex = 33;
            this.lblRaceCasteGender.Text = "0";
            // 
            // label193
            // 
            this.label193.AutoSize = true;
            this.label193.Location = new System.Drawing.Point(99, 19);
            this.label193.Name = "label193";
            this.label193.Size = new System.Drawing.Size(45, 13);
            this.label193.TabIndex = 32;
            this.label193.Text = "Gender:";
            // 
            // lblRaceCasteDescription
            // 
            this.lblRaceCasteDescription.Location = new System.Drawing.Point(99, 47);
            this.lblRaceCasteDescription.Name = "lblRaceCasteDescription";
            this.lblRaceCasteDescription.Size = new System.Drawing.Size(176, 93);
            this.lblRaceCasteDescription.TabIndex = 31;
            // 
            // lstRaceCastes
            // 
            this.lstRaceCastes.FormattingEnabled = true;
            this.lstRaceCastes.Location = new System.Drawing.Point(6, 19);
            this.lstRaceCastes.Name = "lstRaceCastes";
            this.lstRaceCastes.Size = new System.Drawing.Size(87, 121);
            this.lstRaceCastes.TabIndex = 30;
            this.lstRaceCastes.SelectedIndexChanged += new System.EventHandler(this.Caste_ListClick);
            // 
            // label58
            // 
            this.label58.AutoSize = true;
            this.label58.Location = new System.Drawing.Point(3, 0);
            this.label58.Name = "label58";
            this.label58.Size = new System.Drawing.Size(41, 13);
            this.label58.TabIndex = 21;
            this.label58.Text = "Name: ";
            // 
            // lblRaceName
            // 
            this.lblRaceName.AutoSize = true;
            this.lblRaceName.Location = new System.Drawing.Point(64, 0);
            this.lblRaceName.Name = "lblRaceName";
            this.lblRaceName.Size = new System.Drawing.Size(35, 13);
            this.lblRaceName.TabIndex = 22;
            this.lblRaceName.Text = "label2";
            // 
            // lblRacePopulation
            // 
            this.lblRacePopulation.AutoSize = true;
            this.lblRacePopulation.Location = new System.Drawing.Point(64, 22);
            this.lblRacePopulation.Name = "lblRacePopulation";
            this.lblRacePopulation.Size = new System.Drawing.Size(35, 13);
            this.lblRacePopulation.TabIndex = 27;
            this.lblRacePopulation.Text = "label2";
            // 
            // label174
            // 
            this.label174.AutoSize = true;
            this.label174.Location = new System.Drawing.Point(3, 22);
            this.label174.Name = "label174";
            this.label174.Size = new System.Drawing.Size(60, 13);
            this.label174.TabIndex = 26;
            this.label174.Text = "Population:";
            // 
            // grpRaceHistoricalFigures
            // 
            this.grpRaceHistoricalFigures.Controls.Add(this.lstRaceHistoricalFigures);
            this.grpRaceHistoricalFigures.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpRaceHistoricalFigures.Location = new System.Drawing.Point(1125, 202);
            this.grpRaceHistoricalFigures.Name = "grpRaceHistoricalFigures";
            this.grpRaceHistoricalFigures.Size = new System.Drawing.Size(557, 641);
            this.grpRaceHistoricalFigures.TabIndex = 25;
            this.grpRaceHistoricalFigures.TabStop = false;
            this.grpRaceHistoricalFigures.Text = "Historical Figures";
            // 
            // lstRaceHistoricalFigures
            // 
            this.lstRaceHistoricalFigures.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstRaceHistoricalFigures.FormattingEnabled = true;
            this.lstRaceHistoricalFigures.Location = new System.Drawing.Point(3, 16);
            this.lstRaceHistoricalFigures.Name = "lstRaceHistoricalFigures";
            this.lstRaceHistoricalFigures.Size = new System.Drawing.Size(551, 622);
            this.lstRaceHistoricalFigures.TabIndex = 1;
            // 
            // grpRaceLeaders
            // 
            this.grpRaceLeaders.Controls.Add(this.lstRaceLeaders);
            this.grpRaceLeaders.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpRaceLeaders.Location = new System.Drawing.Point(3, 202);
            this.grpRaceLeaders.Name = "grpRaceLeaders";
            this.grpRaceLeaders.Size = new System.Drawing.Size(555, 641);
            this.grpRaceLeaders.TabIndex = 23;
            this.grpRaceLeaders.TabStop = false;
            this.grpRaceLeaders.Text = "Leaders";
            // 
            // lstRaceLeaders
            // 
            this.lstRaceLeaders.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstRaceLeaders.FormattingEnabled = true;
            this.lstRaceLeaders.Location = new System.Drawing.Point(3, 16);
            this.lstRaceLeaders.Name = "lstRaceLeaders";
            this.lstRaceLeaders.Size = new System.Drawing.Size(549, 622);
            this.lstRaceLeaders.TabIndex = 0;
            // 
            // grpRaceCivilizations
            // 
            this.grpRaceCivilizations.Controls.Add(this.lstRaceCivilizations);
            this.grpRaceCivilizations.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpRaceCivilizations.Location = new System.Drawing.Point(564, 202);
            this.grpRaceCivilizations.Name = "grpRaceCivilizations";
            this.grpRaceCivilizations.Size = new System.Drawing.Size(555, 641);
            this.grpRaceCivilizations.TabIndex = 24;
            this.grpRaceCivilizations.TabStop = false;
            this.grpRaceCivilizations.Text = "Civilizations";
            // 
            // lstRaceCivilizations
            // 
            this.lstRaceCivilizations.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstRaceCivilizations.FormattingEnabled = true;
            this.lstRaceCivilizations.Location = new System.Drawing.Point(3, 16);
            this.lstRaceCivilizations.Name = "lstRaceCivilizations";
            this.lstRaceCivilizations.Size = new System.Drawing.Size(549, 622);
            this.lstRaceCivilizations.TabIndex = 1;
            // 
            // lstRace
            // 
            this.lstRace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstRace.FormattingEnabled = true;
            this.lstRace.Location = new System.Drawing.Point(3, 3);
            this.lstRace.Name = "lstRace";
            this.lstRace.Size = new System.Drawing.Size(170, 833);
            this.lstRace.TabIndex = 2;
            // 
            // tabRegion
            // 
            this.tabRegion.Controls.Add(this.tableLayoutPanel1);
            this.tabRegion.Location = new System.Drawing.Point(4, 40);
            this.tabRegion.Name = "tabRegion";
            this.tabRegion.Size = new System.Drawing.Size(1873, 873);
            this.tabRegion.TabIndex = 13;
            this.tabRegion.Text = "Region";
            this.tabRegion.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 171F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.TextFilterRegion, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.grpRegion, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.FilterRegion, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.lstRegion, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1873, 891);
            this.tableLayoutPanel1.TabIndex = 8;
            // 
            // TextFilterRegion
            // 
            this.TextFilterRegion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TextFilterRegion.Location = new System.Drawing.Point(3, 842);
            this.TextFilterRegion.Name = "TextFilterRegion";
            this.TextFilterRegion.Size = new System.Drawing.Size(165, 20);
            this.TextFilterRegion.TabIndex = 5;
            this.TextFilterRegion.TextChanged += new System.EventHandler(this.TextFilter_Changed);
            // 
            // grpRegion
            // 
            this.grpRegion.Controls.Add(this.tableLayoutPanel33);
            this.grpRegion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpRegion.Location = new System.Drawing.Point(174, 3);
            this.grpRegion.Name = "grpRegion";
            this.tableLayoutPanel1.SetRowSpan(this.grpRegion, 3);
            this.grpRegion.Size = new System.Drawing.Size(1696, 885);
            this.grpRegion.TabIndex = 3;
            this.grpRegion.TabStop = false;
            this.grpRegion.Visible = false;
            // 
            // tableLayoutPanel33
            // 
            this.tableLayoutPanel33.ColumnCount = 2;
            this.tableLayoutPanel33.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel33.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel33.Controls.Add(this.grpRegionPopulation, 1, 0);
            this.tableLayoutPanel33.Controls.Add(this.panel16, 0, 0);
            this.tableLayoutPanel33.Controls.Add(this.grpRegionEvents, 0, 2);
            this.tableLayoutPanel33.Controls.Add(this.grpRegionBattles, 1, 2);
            this.tableLayoutPanel33.Controls.Add(this.grpRegionInhabitants, 0, 1);
            this.tableLayoutPanel33.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel33.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel33.Name = "tableLayoutPanel33";
            this.tableLayoutPanel33.RowCount = 3;
            this.tableLayoutPanel33.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel33.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 135F));
            this.tableLayoutPanel33.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 307F));
            this.tableLayoutPanel33.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel33.Size = new System.Drawing.Size(1690, 866);
            this.tableLayoutPanel33.TabIndex = 41;
            // 
            // grpRegionPopulation
            // 
            this.grpRegionPopulation.Controls.Add(this.lstRegionPopulation);
            this.grpRegionPopulation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpRegionPopulation.Location = new System.Drawing.Point(848, 3);
            this.grpRegionPopulation.Name = "grpRegionPopulation";
            this.tableLayoutPanel33.SetRowSpan(this.grpRegionPopulation, 2);
            this.grpRegionPopulation.Size = new System.Drawing.Size(839, 553);
            this.grpRegionPopulation.TabIndex = 43;
            this.grpRegionPopulation.TabStop = false;
            this.grpRegionPopulation.Text = "Population";
            // 
            // lstRegionPopulation
            // 
            this.lstRegionPopulation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstRegionPopulation.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.lstRegionPopulation.FormattingEnabled = true;
            this.lstRegionPopulation.ItemHeight = 16;
            this.lstRegionPopulation.Location = new System.Drawing.Point(3, 16);
            this.lstRegionPopulation.Name = "lstRegionPopulation";
            this.lstRegionPopulation.Size = new System.Drawing.Size(833, 534);
            this.lstRegionPopulation.TabIndex = 0;
            this.lstRegionPopulation.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.lstRegionPopulation_DrawItem);
            // 
            // panel16
            // 
            this.panel16.Controls.Add(this.label72);
            this.panel16.Controls.Add(this.lblRegionName);
            this.panel16.Controls.Add(this.label62);
            this.panel16.Controls.Add(this.lblRegionType);
            this.panel16.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel16.Location = new System.Drawing.Point(3, 3);
            this.panel16.Name = "panel16";
            this.panel16.Size = new System.Drawing.Size(839, 418);
            this.panel16.TabIndex = 42;
            // 
            // label72
            // 
            this.label72.AutoSize = true;
            this.label72.Location = new System.Drawing.Point(3, 0);
            this.label72.Name = "label72";
            this.label72.Size = new System.Drawing.Size(38, 13);
            this.label72.TabIndex = 28;
            this.label72.Text = "Name:";
            // 
            // lblRegionName
            // 
            this.lblRegionName.AutoSize = true;
            this.lblRegionName.Location = new System.Drawing.Point(61, 0);
            this.lblRegionName.Name = "lblRegionName";
            this.lblRegionName.Size = new System.Drawing.Size(35, 13);
            this.lblRegionName.TabIndex = 29;
            this.lblRegionName.Text = "label2";
            // 
            // label62
            // 
            this.label62.AutoSize = true;
            this.label62.Location = new System.Drawing.Point(3, 24);
            this.label62.Name = "label62";
            this.label62.Size = new System.Drawing.Size(34, 13);
            this.label62.TabIndex = 30;
            this.label62.Text = "Type:";
            // 
            // lblRegionType
            // 
            this.lblRegionType.AutoSize = true;
            this.lblRegionType.Location = new System.Drawing.Point(61, 24);
            this.lblRegionType.Name = "lblRegionType";
            this.lblRegionType.Size = new System.Drawing.Size(35, 13);
            this.lblRegionType.TabIndex = 31;
            this.lblRegionType.Text = "label2";
            // 
            // grpRegionEvents
            // 
            this.grpRegionEvents.Controls.Add(this.lstRegionEvents);
            this.grpRegionEvents.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpRegionEvents.Location = new System.Drawing.Point(3, 562);
            this.grpRegionEvents.Name = "grpRegionEvents";
            this.grpRegionEvents.Size = new System.Drawing.Size(839, 301);
            this.grpRegionEvents.TabIndex = 32;
            this.grpRegionEvents.TabStop = false;
            this.grpRegionEvents.Text = "Events";
            // 
            // lstRegionEvents
            // 
            this.lstRegionEvents.Dock = System.Windows.Forms.DockStyle.Top;
            this.lstRegionEvents.FormattingEnabled = true;
            this.lstRegionEvents.Location = new System.Drawing.Point(3, 16);
            this.lstRegionEvents.Name = "lstRegionEvents";
            this.lstRegionEvents.Size = new System.Drawing.Size(833, 121);
            this.lstRegionEvents.TabIndex = 0;
            this.lstRegionEvents.SelectedIndexChanged += new System.EventHandler(this.EventCollection_EventsListClick);
            // 
            // grpRegionBattles
            // 
            this.grpRegionBattles.Controls.Add(this.lstRegionBattles);
            this.grpRegionBattles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpRegionBattles.Location = new System.Drawing.Point(848, 562);
            this.grpRegionBattles.Name = "grpRegionBattles";
            this.grpRegionBattles.Size = new System.Drawing.Size(839, 301);
            this.grpRegionBattles.TabIndex = 33;
            this.grpRegionBattles.TabStop = false;
            this.grpRegionBattles.Text = "Battles";
            // 
            // lstRegionBattles
            // 
            this.lstRegionBattles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstRegionBattles.FormattingEnabled = true;
            this.lstRegionBattles.Location = new System.Drawing.Point(3, 16);
            this.lstRegionBattles.Name = "lstRegionBattles";
            this.lstRegionBattles.Size = new System.Drawing.Size(833, 282);
            this.lstRegionBattles.TabIndex = 1;
            // 
            // grpRegionInhabitants
            // 
            this.grpRegionInhabitants.Controls.Add(this.lstRegionInhabitants);
            this.grpRegionInhabitants.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpRegionInhabitants.Location = new System.Drawing.Point(3, 427);
            this.grpRegionInhabitants.Name = "grpRegionInhabitants";
            this.grpRegionInhabitants.Size = new System.Drawing.Size(839, 129);
            this.grpRegionInhabitants.TabIndex = 40;
            this.grpRegionInhabitants.TabStop = false;
            this.grpRegionInhabitants.Text = "Inhabitants";
            // 
            // lstRegionInhabitants
            // 
            this.lstRegionInhabitants.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstRegionInhabitants.FormattingEnabled = true;
            this.lstRegionInhabitants.Location = new System.Drawing.Point(3, 16);
            this.lstRegionInhabitants.Name = "lstRegionInhabitants";
            this.lstRegionInhabitants.Size = new System.Drawing.Size(833, 110);
            this.lstRegionInhabitants.TabIndex = 0;
            // 
            // FilterRegion
            // 
            this.FilterRegion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FilterRegion.Location = new System.Drawing.Point(3, 866);
            this.FilterRegion.Name = "FilterRegion";
            this.FilterRegion.Size = new System.Drawing.Size(165, 22);
            this.FilterRegion.TabIndex = 6;
            this.FilterRegion.Tag = "";
            this.FilterRegion.Text = "Filter";
            this.FilterRegion.UseVisualStyleBackColor = true;
            this.FilterRegion.Click += new System.EventHandler(this.FilterButton_Click);
            // 
            // lstRegion
            // 
            this.lstRegion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstRegion.FormattingEnabled = true;
            this.lstRegion.Location = new System.Drawing.Point(3, 3);
            this.lstRegion.Name = "lstRegion";
            this.lstRegion.Size = new System.Drawing.Size(165, 833);
            this.lstRegion.TabIndex = 2;
            // 
            // tabSite
            // 
            this.tabSite.Controls.Add(this.tableLayoutPanel14);
            this.tabSite.Location = new System.Drawing.Point(4, 40);
            this.tabSite.Name = "tabSite";
            this.tabSite.Size = new System.Drawing.Size(1873, 873);
            this.tabSite.TabIndex = 14;
            this.tabSite.Text = "Site";
            this.tabSite.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel14
            // 
            this.tableLayoutPanel14.ColumnCount = 2;
            this.tableLayoutPanel14.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 170F));
            this.tableLayoutPanel14.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel14.Controls.Add(this.TextFilterSite, 0, 1);
            this.tableLayoutPanel14.Controls.Add(this.grpSite, 1, 0);
            this.tableLayoutPanel14.Controls.Add(this.FilterSite, 0, 2);
            this.tableLayoutPanel14.Controls.Add(this.lstSite, 0, 0);
            this.tableLayoutPanel14.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel14.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel14.Margin = new System.Windows.Forms.Padding(2);
            this.tableLayoutPanel14.Name = "tableLayoutPanel14";
            this.tableLayoutPanel14.RowCount = 3;
            this.tableLayoutPanel14.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel14.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel14.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanel14.Size = new System.Drawing.Size(1873, 891);
            this.tableLayoutPanel14.TabIndex = 8;
            // 
            // TextFilterSite
            // 
            this.TextFilterSite.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TextFilterSite.Location = new System.Drawing.Point(3, 842);
            this.TextFilterSite.Name = "TextFilterSite";
            this.TextFilterSite.Size = new System.Drawing.Size(164, 20);
            this.TextFilterSite.TabIndex = 5;
            this.TextFilterSite.TextChanged += new System.EventHandler(this.TextFilter_Changed);
            // 
            // grpSite
            // 
            this.grpSite.Controls.Add(this.tableLayoutPanel34);
            this.grpSite.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpSite.Location = new System.Drawing.Point(173, 3);
            this.grpSite.Name = "grpSite";
            this.tableLayoutPanel14.SetRowSpan(this.grpSite, 3);
            this.grpSite.Size = new System.Drawing.Size(1697, 885);
            this.grpSite.TabIndex = 3;
            this.grpSite.TabStop = false;
            this.grpSite.Visible = false;
            // 
            // tableLayoutPanel34
            // 
            this.tableLayoutPanel34.ColumnCount = 3;
            this.tableLayoutPanel34.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 46.14004F));
            this.tableLayoutPanel34.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 53.85996F));
            this.tableLayoutPanel34.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 673F));
            this.tableLayoutPanel34.Controls.Add(this.grpSiteArtifacts, 0, 2);
            this.tableLayoutPanel34.Controls.Add(this.grpSiteStructures, 1, 2);
            this.tableLayoutPanel34.Controls.Add(this.panel17, 0, 0);
            this.tableLayoutPanel34.Controls.Add(this.grpSiteOutcasts, 1, 3);
            this.tableLayoutPanel34.Controls.Add(this.grpSiteCreated, 0, 1);
            this.tableLayoutPanel34.Controls.Add(this.grpSitePrisoners, 0, 3);
            this.tableLayoutPanel34.Controls.Add(this.grpSiteInhabitants, 1, 0);
            this.tableLayoutPanel34.Controls.Add(this.grpSitePopulation, 1, 1);
            this.tableLayoutPanel34.Controls.Add(this.grpSiteEventCollection, 2, 0);
            this.tableLayoutPanel34.Controls.Add(this.grpSiteEvent, 2, 2);
            this.tableLayoutPanel34.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel34.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel34.Name = "tableLayoutPanel34";
            this.tableLayoutPanel34.RowCount = 4;
            this.tableLayoutPanel34.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel34.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.tableLayoutPanel34.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.tableLayoutPanel34.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel34.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel34.Size = new System.Drawing.Size(1691, 866);
            this.tableLayoutPanel34.TabIndex = 62;
            // 
            // grpSiteArtifacts
            // 
            this.grpSiteArtifacts.Controls.Add(this.lstSiteArtifacts);
            this.grpSiteArtifacts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpSiteArtifacts.Location = new System.Drawing.Point(3, 513);
            this.grpSiteArtifacts.Name = "grpSiteArtifacts";
            this.grpSiteArtifacts.Size = new System.Drawing.Size(463, 249);
            this.grpSiteArtifacts.TabIndex = 65;
            this.grpSiteArtifacts.TabStop = false;
            this.grpSiteArtifacts.Text = "Artifacts Created";
            // 
            // lstSiteArtifacts
            // 
            this.lstSiteArtifacts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstSiteArtifacts.FormattingEnabled = true;
            this.lstSiteArtifacts.Location = new System.Drawing.Point(3, 16);
            this.lstSiteArtifacts.Name = "lstSiteArtifacts";
            this.lstSiteArtifacts.Size = new System.Drawing.Size(457, 230);
            this.lstSiteArtifacts.TabIndex = 0;
            // 
            // grpSiteStructures
            // 
            this.grpSiteStructures.Controls.Add(this.lstSiteStructures);
            this.grpSiteStructures.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpSiteStructures.Location = new System.Drawing.Point(472, 513);
            this.grpSiteStructures.Name = "grpSiteStructures";
            this.grpSiteStructures.Size = new System.Drawing.Size(542, 249);
            this.grpSiteStructures.TabIndex = 64;
            this.grpSiteStructures.TabStop = false;
            this.grpSiteStructures.Text = "Structures";
            // 
            // lstSiteStructures
            // 
            this.lstSiteStructures.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstSiteStructures.FormattingEnabled = true;
            this.lstSiteStructures.Location = new System.Drawing.Point(3, 16);
            this.lstSiteStructures.Name = "lstSiteStructures";
            this.lstSiteStructures.Size = new System.Drawing.Size(536, 230);
            this.lstSiteStructures.TabIndex = 0;
            // 
            // panel17
            // 
            this.panel17.Controls.Add(this.label207);
            this.panel17.Controls.Add(this.label295);
            this.panel17.Controls.Add(this.lblSiteCurOwner);
            this.panel17.Controls.Add(this.lblSiteCiv);
            this.panel17.Controls.Add(this.SiteMapLabel);
            this.panel17.Controls.Add(this.label80);
            this.panel17.Controls.Add(this.lblSiteName);
            this.panel17.Controls.Add(this.label78);
            this.panel17.Controls.Add(this.lblSiteCoord);
            this.panel17.Controls.Add(this.lblSiteAltName);
            this.panel17.Controls.Add(this.label83);
            this.panel17.Controls.Add(this.label75);
            this.panel17.Controls.Add(this.label53);
            this.panel17.Controls.Add(this.lblSiteType);
            this.panel17.Controls.Add(this.label81);
            this.panel17.Controls.Add(this.lblSiteOwner);
            this.panel17.Controls.Add(this.lblSiteParentCiv);
            this.panel17.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel17.Location = new System.Drawing.Point(3, 3);
            this.panel17.Name = "panel17";
            this.panel17.Size = new System.Drawing.Size(463, 249);
            this.panel17.TabIndex = 63;
            // 
            // label207
            // 
            this.label207.AutoSize = true;
            this.label207.Location = new System.Drawing.Point(6, 129);
            this.label207.Name = "label207";
            this.label207.Size = new System.Drawing.Size(60, 13);
            this.label207.TabIndex = 62;
            this.label207.Text = "Cur Owner:";
            // 
            // label295
            // 
            this.label295.AutoSize = true;
            this.label295.Location = new System.Drawing.Point(6, 151);
            this.label295.Name = "label295";
            this.label295.Size = new System.Drawing.Size(25, 13);
            this.label295.TabIndex = 63;
            this.label295.Text = "Civ:";
            // 
            // label80
            // 
            this.label80.AutoSize = true;
            this.label80.Location = new System.Drawing.Point(3, 0);
            this.label80.Name = "label80";
            this.label80.Size = new System.Drawing.Size(41, 13);
            this.label80.TabIndex = 23;
            this.label80.Text = "Name: ";
            // 
            // lblSiteName
            // 
            this.lblSiteName.AutoSize = true;
            this.lblSiteName.Location = new System.Drawing.Point(71, 0);
            this.lblSiteName.Name = "lblSiteName";
            this.lblSiteName.Size = new System.Drawing.Size(35, 13);
            this.lblSiteName.TabIndex = 24;
            this.lblSiteName.Text = "label2";
            // 
            // label78
            // 
            this.label78.AutoSize = true;
            this.label78.Location = new System.Drawing.Point(3, 22);
            this.label78.Name = "label78";
            this.label78.Size = new System.Drawing.Size(58, 13);
            this.label78.TabIndex = 25;
            this.label78.Text = "Nickname:";
            // 
            // lblSiteAltName
            // 
            this.lblSiteAltName.AutoSize = true;
            this.lblSiteAltName.Location = new System.Drawing.Point(71, 22);
            this.lblSiteAltName.Name = "lblSiteAltName";
            this.lblSiteAltName.Size = new System.Drawing.Size(35, 13);
            this.lblSiteAltName.TabIndex = 26;
            this.lblSiteAltName.Text = "label2";
            // 
            // label83
            // 
            this.label83.AutoSize = true;
            this.label83.Location = new System.Drawing.Point(3, 64);
            this.label83.Name = "label83";
            this.label83.Size = new System.Drawing.Size(38, 13);
            this.label83.TabIndex = 60;
            this.label83.Text = "Coord:";
            // 
            // label75
            // 
            this.label75.AutoSize = true;
            this.label75.Location = new System.Drawing.Point(3, 85);
            this.label75.Name = "label75";
            this.label75.Size = new System.Drawing.Size(41, 13);
            this.label75.TabIndex = 27;
            this.label75.Text = "Owner:";
            // 
            // label53
            // 
            this.label53.AutoSize = true;
            this.label53.Location = new System.Drawing.Point(3, 43);
            this.label53.Name = "label53";
            this.label53.Size = new System.Drawing.Size(34, 13);
            this.label53.TabIndex = 29;
            this.label53.Text = "Type:";
            // 
            // lblSiteType
            // 
            this.lblSiteType.AutoSize = true;
            this.lblSiteType.Location = new System.Drawing.Point(71, 43);
            this.lblSiteType.Name = "lblSiteType";
            this.lblSiteType.Size = new System.Drawing.Size(35, 13);
            this.lblSiteType.TabIndex = 30;
            this.lblSiteType.Text = "label2";
            // 
            // label81
            // 
            this.label81.AutoSize = true;
            this.label81.Location = new System.Drawing.Point(3, 107);
            this.label81.Name = "label81";
            this.label81.Size = new System.Drawing.Size(59, 13);
            this.label81.TabIndex = 31;
            this.label81.Text = "Parent Civ:";
            // 
            // grpSiteOutcasts
            // 
            this.grpSiteOutcasts.Controls.Add(this.lstSiteOutcasts);
            this.grpSiteOutcasts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpSiteOutcasts.Location = new System.Drawing.Point(472, 768);
            this.grpSiteOutcasts.Name = "grpSiteOutcasts";
            this.grpSiteOutcasts.Size = new System.Drawing.Size(542, 95);
            this.grpSiteOutcasts.TabIndex = 61;
            this.grpSiteOutcasts.TabStop = false;
            this.grpSiteOutcasts.Text = "Outcasts";
            // 
            // lstSiteOutcasts
            // 
            this.lstSiteOutcasts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstSiteOutcasts.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.lstSiteOutcasts.FormattingEnabled = true;
            this.lstSiteOutcasts.ItemHeight = 16;
            this.lstSiteOutcasts.Location = new System.Drawing.Point(3, 16);
            this.lstSiteOutcasts.Name = "lstSiteOutcasts";
            this.lstSiteOutcasts.Size = new System.Drawing.Size(536, 76);
            this.lstSiteOutcasts.TabIndex = 0;
            this.lstSiteOutcasts.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.lstSiteOutcasts_DrawItem);
            // 
            // grpSiteCreated
            // 
            this.grpSiteCreated.Controls.Add(this.lblSiteCreatedTime);
            this.grpSiteCreated.Controls.Add(this.lblSiteCreatedByCiv);
            this.grpSiteCreated.Controls.Add(this.lblSiteCreatedBy);
            this.grpSiteCreated.Controls.Add(this.label69);
            this.grpSiteCreated.Controls.Add(this.label77);
            this.grpSiteCreated.Controls.Add(this.label79);
            this.grpSiteCreated.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpSiteCreated.Location = new System.Drawing.Point(3, 258);
            this.grpSiteCreated.Name = "grpSiteCreated";
            this.grpSiteCreated.Size = new System.Drawing.Size(463, 249);
            this.grpSiteCreated.TabIndex = 40;
            this.grpSiteCreated.TabStop = false;
            this.grpSiteCreated.Text = "Created";
            // 
            // label69
            // 
            this.label69.AutoSize = true;
            this.label69.Location = new System.Drawing.Point(6, 58);
            this.label69.Name = "label69";
            this.label69.Size = new System.Drawing.Size(24, 13);
            this.label69.TabIndex = 3;
            this.label69.Text = "On:";
            // 
            // label77
            // 
            this.label77.AutoSize = true;
            this.label77.Location = new System.Drawing.Point(6, 37);
            this.label77.Name = "label77";
            this.label77.Size = new System.Drawing.Size(21, 13);
            this.label77.TabIndex = 2;
            this.label77.Text = "Of:";
            // 
            // label79
            // 
            this.label79.AutoSize = true;
            this.label79.Location = new System.Drawing.Point(6, 15);
            this.label79.Name = "label79";
            this.label79.Size = new System.Drawing.Size(25, 13);
            this.label79.TabIndex = 0;
            this.label79.Text = "By: ";
            // 
            // grpSitePrisoners
            // 
            this.grpSitePrisoners.Controls.Add(this.lstSitePrisoners);
            this.grpSitePrisoners.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpSitePrisoners.Location = new System.Drawing.Point(3, 768);
            this.grpSitePrisoners.Name = "grpSitePrisoners";
            this.grpSitePrisoners.Size = new System.Drawing.Size(463, 95);
            this.grpSitePrisoners.TabIndex = 38;
            this.grpSitePrisoners.TabStop = false;
            this.grpSitePrisoners.Text = "Prisoners";
            // 
            // lstSitePrisoners
            // 
            this.lstSitePrisoners.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstSitePrisoners.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.lstSitePrisoners.FormattingEnabled = true;
            this.lstSitePrisoners.ItemHeight = 16;
            this.lstSitePrisoners.Location = new System.Drawing.Point(3, 16);
            this.lstSitePrisoners.Name = "lstSitePrisoners";
            this.lstSitePrisoners.Size = new System.Drawing.Size(457, 76);
            this.lstSitePrisoners.TabIndex = 0;
            this.lstSitePrisoners.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.lstSitePrisoners_DrawItem);
            // 
            // grpSiteInhabitants
            // 
            this.grpSiteInhabitants.Controls.Add(this.lstSiteInhabitants);
            this.grpSiteInhabitants.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpSiteInhabitants.Location = new System.Drawing.Point(472, 3);
            this.grpSiteInhabitants.Name = "grpSiteInhabitants";
            this.grpSiteInhabitants.Size = new System.Drawing.Size(542, 249);
            this.grpSiteInhabitants.TabIndex = 39;
            this.grpSiteInhabitants.TabStop = false;
            this.grpSiteInhabitants.Text = "Inhabitants";
            // 
            // lstSiteInhabitants
            // 
            this.lstSiteInhabitants.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstSiteInhabitants.FormattingEnabled = true;
            this.lstSiteInhabitants.Location = new System.Drawing.Point(3, 16);
            this.lstSiteInhabitants.Name = "lstSiteInhabitants";
            this.lstSiteInhabitants.Size = new System.Drawing.Size(536, 230);
            this.lstSiteInhabitants.TabIndex = 0;
            // 
            // grpSitePopulation
            // 
            this.grpSitePopulation.Controls.Add(this.lstSitePopulation);
            this.grpSitePopulation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpSitePopulation.Location = new System.Drawing.Point(472, 258);
            this.grpSitePopulation.Name = "grpSitePopulation";
            this.grpSitePopulation.Size = new System.Drawing.Size(542, 249);
            this.grpSitePopulation.TabIndex = 37;
            this.grpSitePopulation.TabStop = false;
            this.grpSitePopulation.Text = "Population";
            // 
            // lstSitePopulation
            // 
            this.lstSitePopulation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstSitePopulation.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.lstSitePopulation.FormattingEnabled = true;
            this.lstSitePopulation.ItemHeight = 16;
            this.lstSitePopulation.Location = new System.Drawing.Point(3, 16);
            this.lstSitePopulation.Name = "lstSitePopulation";
            this.lstSitePopulation.Size = new System.Drawing.Size(536, 230);
            this.lstSitePopulation.TabIndex = 0;
            this.lstSitePopulation.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.lstSitePopulation_DrawItem);
            // 
            // grpSiteEventCollection
            // 
            this.grpSiteEventCollection.Controls.Add(this.trvSiteEventCollection);
            this.grpSiteEventCollection.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpSiteEventCollection.Location = new System.Drawing.Point(1020, 3);
            this.grpSiteEventCollection.Name = "grpSiteEventCollection";
            this.tableLayoutPanel34.SetRowSpan(this.grpSiteEventCollection, 2);
            this.grpSiteEventCollection.Size = new System.Drawing.Size(668, 504);
            this.grpSiteEventCollection.TabIndex = 59;
            this.grpSiteEventCollection.TabStop = false;
            this.grpSiteEventCollection.Text = "Site Event Collections";
            // 
            // trvSiteEventCollection
            // 
            this.trvSiteEventCollection.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trvSiteEventCollection.Location = new System.Drawing.Point(3, 16);
            this.trvSiteEventCollection.Name = "trvSiteEventCollection";
            this.trvSiteEventCollection.Size = new System.Drawing.Size(662, 485);
            this.trvSiteEventCollection.TabIndex = 0;
            // 
            // grpSiteEvent
            // 
            this.grpSiteEvent.Controls.Add(this.lstSiteEvent);
            this.grpSiteEvent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpSiteEvent.Location = new System.Drawing.Point(1020, 513);
            this.grpSiteEvent.Name = "grpSiteEvent";
            this.tableLayoutPanel34.SetRowSpan(this.grpSiteEvent, 2);
            this.grpSiteEvent.Size = new System.Drawing.Size(668, 350);
            this.grpSiteEvent.TabIndex = 58;
            this.grpSiteEvent.TabStop = false;
            this.grpSiteEvent.Text = "Site Events";
            // 
            // lstSiteEvent
            // 
            this.lstSiteEvent.Dock = System.Windows.Forms.DockStyle.Top;
            this.lstSiteEvent.FormattingEnabled = true;
            this.lstSiteEvent.Location = new System.Drawing.Point(3, 16);
            this.lstSiteEvent.Name = "lstSiteEvent";
            this.lstSiteEvent.Size = new System.Drawing.Size(662, 121);
            this.lstSiteEvent.TabIndex = 0;
            this.lstSiteEvent.SelectedIndexChanged += new System.EventHandler(this.EventCollection_EventsListClick);
            // 
            // FilterSite
            // 
            this.FilterSite.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FilterSite.Location = new System.Drawing.Point(3, 866);
            this.FilterSite.Name = "FilterSite";
            this.FilterSite.Size = new System.Drawing.Size(164, 22);
            this.FilterSite.TabIndex = 6;
            this.FilterSite.Tag = "";
            this.FilterSite.Text = "Filter";
            this.FilterSite.UseVisualStyleBackColor = true;
            this.FilterSite.Click += new System.EventHandler(this.FilterButton_Click);
            // 
            // lstSite
            // 
            this.lstSite.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstSite.FormattingEnabled = true;
            this.lstSite.Location = new System.Drawing.Point(3, 3);
            this.lstSite.Name = "lstSite";
            this.lstSite.Size = new System.Drawing.Size(164, 833);
            this.lstSite.TabIndex = 2;
            // 
            // tabStructure
            // 
            this.tabStructure.Controls.Add(this.tableLayoutPanel36);
            this.tabStructure.Location = new System.Drawing.Point(4, 40);
            this.tabStructure.Name = "tabStructure";
            this.tabStructure.Size = new System.Drawing.Size(1873, 873);
            this.tabStructure.TabIndex = 19;
            this.tabStructure.Text = "Structure";
            this.tabStructure.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel36
            // 
            this.tableLayoutPanel36.ColumnCount = 2;
            this.tableLayoutPanel36.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 171F));
            this.tableLayoutPanel36.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel36.Controls.Add(this.TextFilterStructure, 0, 1);
            this.tableLayoutPanel36.Controls.Add(this.grpStructure, 1, 0);
            this.tableLayoutPanel36.Controls.Add(this.FilterStructure, 0, 2);
            this.tableLayoutPanel36.Controls.Add(this.lstStructure, 0, 0);
            this.tableLayoutPanel36.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel36.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel36.Margin = new System.Windows.Forms.Padding(2);
            this.tableLayoutPanel36.Name = "tableLayoutPanel36";
            this.tableLayoutPanel36.RowCount = 3;
            this.tableLayoutPanel36.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel36.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel36.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanel36.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel36.Size = new System.Drawing.Size(1873, 891);
            this.tableLayoutPanel36.TabIndex = 9;
            // 
            // TextFilterStructure
            // 
            this.TextFilterStructure.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TextFilterStructure.Location = new System.Drawing.Point(3, 842);
            this.TextFilterStructure.Name = "TextFilterStructure";
            this.TextFilterStructure.Size = new System.Drawing.Size(165, 20);
            this.TextFilterStructure.TabIndex = 5;
            this.TextFilterStructure.TextChanged += new System.EventHandler(this.TextFilter_Changed);
            // 
            // grpStructure
            // 
            this.grpStructure.Controls.Add(this.tableLayoutPanel37);
            this.grpStructure.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpStructure.Location = new System.Drawing.Point(174, 3);
            this.grpStructure.Name = "grpStructure";
            this.tableLayoutPanel36.SetRowSpan(this.grpStructure, 3);
            this.grpStructure.Size = new System.Drawing.Size(1696, 885);
            this.grpStructure.TabIndex = 3;
            this.grpStructure.TabStop = false;
            this.grpStructure.Visible = false;
            // 
            // tableLayoutPanel37
            // 
            this.tableLayoutPanel37.ColumnCount = 2;
            this.tableLayoutPanel37.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel37.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel37.Controls.Add(this.grpStructureEntombedHF, 1, 0);
            this.tableLayoutPanel37.Controls.Add(this.grpStructureEvents, 1, 1);
            this.tableLayoutPanel37.Controls.Add(this.grpStructureRazed, 0, 2);
            this.tableLayoutPanel37.Controls.Add(this.grpStructureCreated, 0, 1);
            this.tableLayoutPanel37.Controls.Add(this.panel19, 0, 0);
            this.tableLayoutPanel37.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel37.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel37.Name = "tableLayoutPanel37";
            this.tableLayoutPanel37.RowCount = 3;
            this.tableLayoutPanel37.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel37.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel37.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel37.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel37.Size = new System.Drawing.Size(1690, 866);
            this.tableLayoutPanel37.TabIndex = 41;
            // 
            // grpStructureEntombedHF
            // 
            this.grpStructureEntombedHF.Controls.Add(this.lstStructureEntombedHF);
            this.grpStructureEntombedHF.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpStructureEntombedHF.Location = new System.Drawing.Point(848, 3);
            this.grpStructureEntombedHF.Name = "grpStructureEntombedHF";
            this.grpStructureEntombedHF.Size = new System.Drawing.Size(839, 94);
            this.grpStructureEntombedHF.TabIndex = 70;
            this.grpStructureEntombedHF.TabStop = false;
            this.grpStructureEntombedHF.Text = "Entombed Historical Figures";
            // 
            // lstStructureEntombedHF
            // 
            this.lstStructureEntombedHF.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstStructureEntombedHF.FormattingEnabled = true;
            this.lstStructureEntombedHF.Location = new System.Drawing.Point(3, 16);
            this.lstStructureEntombedHF.Name = "lstStructureEntombedHF";
            this.lstStructureEntombedHF.Size = new System.Drawing.Size(833, 75);
            this.lstStructureEntombedHF.TabIndex = 0;
            // 
            // grpStructureEvents
            // 
            this.grpStructureEvents.Controls.Add(this.lstStructureEvents);
            this.grpStructureEvents.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpStructureEvents.Location = new System.Drawing.Point(848, 103);
            this.grpStructureEvents.Name = "grpStructureEvents";
            this.tableLayoutPanel37.SetRowSpan(this.grpStructureEvents, 2);
            this.grpStructureEvents.Size = new System.Drawing.Size(839, 760);
            this.grpStructureEvents.TabIndex = 68;
            this.grpStructureEvents.TabStop = false;
            this.grpStructureEvents.Text = "Events";
            // 
            // lstStructureEvents
            // 
            this.lstStructureEvents.FormattingEnabled = true;
            this.lstStructureEvents.Location = new System.Drawing.Point(6, 19);
            this.lstStructureEvents.Name = "lstStructureEvents";
            this.lstStructureEvents.Size = new System.Drawing.Size(425, 329);
            this.lstStructureEvents.TabIndex = 0;
            this.lstStructureEvents.SelectedIndexChanged += new System.EventHandler(this.EventCollection_EventsListClick);
            // 
            // grpStructureRazed
            // 
            this.grpStructureRazed.Controls.Add(this.lblStructureRazedTime);
            this.grpStructureRazed.Controls.Add(this.label48);
            this.grpStructureRazed.Controls.Add(this.lblStructureRazedSite);
            this.grpStructureRazed.Controls.Add(this.lblStructureRazedCiv);
            this.grpStructureRazed.Controls.Add(this.label175);
            this.grpStructureRazed.Controls.Add(this.label177);
            this.grpStructureRazed.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpStructureRazed.Location = new System.Drawing.Point(3, 486);
            this.grpStructureRazed.Name = "grpStructureRazed";
            this.grpStructureRazed.Size = new System.Drawing.Size(839, 377);
            this.grpStructureRazed.TabIndex = 44;
            this.grpStructureRazed.TabStop = false;
            this.grpStructureRazed.Text = "Razed";
            // 
            // label48
            // 
            this.label48.AutoSize = true;
            this.label48.Location = new System.Drawing.Point(6, 60);
            this.label48.Name = "label48";
            this.label48.Size = new System.Drawing.Size(24, 13);
            this.label48.TabIndex = 16;
            this.label48.Text = "On:";
            // 
            // label175
            // 
            this.label175.AutoSize = true;
            this.label175.Location = new System.Drawing.Point(6, 38);
            this.label175.Name = "label175";
            this.label175.Size = new System.Drawing.Size(20, 13);
            this.label175.TabIndex = 12;
            this.label175.Text = "At:";
            // 
            // label177
            // 
            this.label177.AutoSize = true;
            this.label177.Location = new System.Drawing.Point(6, 16);
            this.label177.Name = "label177";
            this.label177.Size = new System.Drawing.Size(25, 13);
            this.label177.TabIndex = 10;
            this.label177.Text = "By: ";
            // 
            // grpStructureCreated
            // 
            this.grpStructureCreated.Controls.Add(this.lblStructureCreatedTime);
            this.grpStructureCreated.Controls.Add(this.label7);
            this.grpStructureCreated.Controls.Add(this.lblStructureCreatedSite);
            this.grpStructureCreated.Controls.Add(this.lblStructureCreatedCiv);
            this.grpStructureCreated.Controls.Add(this.lblStructureCreatedSiteCiv);
            this.grpStructureCreated.Controls.Add(this.label9);
            this.grpStructureCreated.Controls.Add(this.label10);
            this.grpStructureCreated.Controls.Add(this.label11);
            this.grpStructureCreated.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpStructureCreated.Location = new System.Drawing.Point(3, 103);
            this.grpStructureCreated.Name = "grpStructureCreated";
            this.grpStructureCreated.Size = new System.Drawing.Size(839, 377);
            this.grpStructureCreated.TabIndex = 43;
            this.grpStructureCreated.TabStop = false;
            this.grpStructureCreated.Text = "Created";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 80);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(24, 13);
            this.label7.TabIndex = 8;
            this.label7.Text = "On:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 58);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(20, 13);
            this.label9.TabIndex = 3;
            this.label9.Text = "At:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(6, 37);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(21, 13);
            this.label10.TabIndex = 2;
            this.label10.Text = "Of:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(6, 15);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(25, 13);
            this.label11.TabIndex = 0;
            this.label11.Text = "By: ";
            // 
            // panel19
            // 
            this.panel19.Controls.Add(this.label182);
            this.panel19.Controls.Add(this.lblStructureType);
            this.panel19.Controls.Add(this.lblStructureSite);
            this.panel19.Controls.Add(this.label3);
            this.panel19.Controls.Add(this.lblStructureID);
            this.panel19.Controls.Add(this.label8);
            this.panel19.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel19.Location = new System.Drawing.Point(3, 3);
            this.panel19.Name = "panel19";
            this.panel19.Size = new System.Drawing.Size(839, 94);
            this.panel19.TabIndex = 42;
            // 
            // label182
            // 
            this.label182.AutoSize = true;
            this.label182.Location = new System.Drawing.Point(3, 46);
            this.label182.Name = "label182";
            this.label182.Size = new System.Drawing.Size(34, 13);
            this.label182.TabIndex = 37;
            this.label182.Text = "Type:";
            // 
            // lblStructureType
            // 
            this.lblStructureType.AutoSize = true;
            this.lblStructureType.Location = new System.Drawing.Point(61, 46);
            this.lblStructureType.Name = "lblStructureType";
            this.lblStructureType.Size = new System.Drawing.Size(35, 13);
            this.lblStructureType.TabIndex = 38;
            this.lblStructureType.Text = "label2";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(21, 13);
            this.label3.TabIndex = 28;
            this.label3.Text = "ID:";
            // 
            // lblStructureID
            // 
            this.lblStructureID.AutoSize = true;
            this.lblStructureID.Location = new System.Drawing.Point(61, 0);
            this.lblStructureID.Name = "lblStructureID";
            this.lblStructureID.Size = new System.Drawing.Size(35, 13);
            this.lblStructureID.TabIndex = 29;
            this.lblStructureID.Text = "label2";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(3, 24);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(28, 13);
            this.label8.TabIndex = 30;
            this.label8.Text = "Site:";
            // 
            // FilterStructure
            // 
            this.FilterStructure.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FilterStructure.Location = new System.Drawing.Point(3, 866);
            this.FilterStructure.Name = "FilterStructure";
            this.FilterStructure.Size = new System.Drawing.Size(165, 22);
            this.FilterStructure.TabIndex = 6;
            this.FilterStructure.Tag = "";
            this.FilterStructure.Text = "Filter";
            this.FilterStructure.UseVisualStyleBackColor = true;
            this.FilterStructure.Click += new System.EventHandler(this.FilterButton_Click);
            // 
            // lstStructure
            // 
            this.lstStructure.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstStructure.FormattingEnabled = true;
            this.lstStructure.Location = new System.Drawing.Point(3, 3);
            this.lstStructure.Name = "lstStructure";
            this.lstStructure.Size = new System.Drawing.Size(165, 833);
            this.lstStructure.TabIndex = 2;
            // 
            // tabUndergroundRegion
            // 
            this.tabUndergroundRegion.Controls.Add(this.tableLayoutPanel15);
            this.tabUndergroundRegion.Location = new System.Drawing.Point(4, 40);
            this.tabUndergroundRegion.Name = "tabUndergroundRegion";
            this.tabUndergroundRegion.Size = new System.Drawing.Size(1873, 873);
            this.tabUndergroundRegion.TabIndex = 15;
            this.tabUndergroundRegion.Text = "Underground Region";
            this.tabUndergroundRegion.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel15
            // 
            this.tableLayoutPanel15.ColumnCount = 2;
            this.tableLayoutPanel15.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 170F));
            this.tableLayoutPanel15.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel15.Controls.Add(this.TextFilterUndergroundRegion, 0, 1);
            this.tableLayoutPanel15.Controls.Add(this.grpUndergroundRegion, 1, 0);
            this.tableLayoutPanel15.Controls.Add(this.FilterUndergroundRegion, 0, 2);
            this.tableLayoutPanel15.Controls.Add(this.lstUndergroundRegion, 0, 0);
            this.tableLayoutPanel15.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel15.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel15.Margin = new System.Windows.Forms.Padding(2);
            this.tableLayoutPanel15.Name = "tableLayoutPanel15";
            this.tableLayoutPanel15.RowCount = 3;
            this.tableLayoutPanel15.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel15.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel15.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanel15.Size = new System.Drawing.Size(1873, 891);
            this.tableLayoutPanel15.TabIndex = 8;
            // 
            // TextFilterUndergroundRegion
            // 
            this.TextFilterUndergroundRegion.Location = new System.Drawing.Point(3, 842);
            this.TextFilterUndergroundRegion.Name = "TextFilterUndergroundRegion";
            this.TextFilterUndergroundRegion.Size = new System.Drawing.Size(114, 20);
            this.TextFilterUndergroundRegion.TabIndex = 5;
            this.TextFilterUndergroundRegion.TextChanged += new System.EventHandler(this.TextFilter_Changed);
            // 
            // grpUndergroundRegion
            // 
            this.grpUndergroundRegion.Controls.Add(this.tableLayoutPanel40);
            this.grpUndergroundRegion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpUndergroundRegion.Location = new System.Drawing.Point(173, 3);
            this.grpUndergroundRegion.Name = "grpUndergroundRegion";
            this.tableLayoutPanel15.SetRowSpan(this.grpUndergroundRegion, 3);
            this.grpUndergroundRegion.Size = new System.Drawing.Size(1697, 885);
            this.grpUndergroundRegion.TabIndex = 3;
            this.grpUndergroundRegion.TabStop = false;
            this.grpUndergroundRegion.Visible = false;
            // 
            // tableLayoutPanel40
            // 
            this.tableLayoutPanel40.ColumnCount = 2;
            this.tableLayoutPanel40.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel40.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel40.Controls.Add(this.grpUndergroundRegionPopulation, 0, 1);
            this.tableLayoutPanel40.Controls.Add(this.panel21, 0, 0);
            this.tableLayoutPanel40.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel40.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel40.Name = "tableLayoutPanel40";
            this.tableLayoutPanel40.RowCount = 2;
            this.tableLayoutPanel40.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel40.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 519F));
            this.tableLayoutPanel40.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel40.Size = new System.Drawing.Size(1691, 866);
            this.tableLayoutPanel40.TabIndex = 42;
            // 
            // grpUndergroundRegionPopulation
            // 
            this.grpUndergroundRegionPopulation.Controls.Add(this.lstUndergroundRegionPopulation);
            this.grpUndergroundRegionPopulation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpUndergroundRegionPopulation.Location = new System.Drawing.Point(3, 350);
            this.grpUndergroundRegionPopulation.Name = "grpUndergroundRegionPopulation";
            this.grpUndergroundRegionPopulation.Size = new System.Drawing.Size(839, 513);
            this.grpUndergroundRegionPopulation.TabIndex = 43;
            this.grpUndergroundRegionPopulation.TabStop = false;
            this.grpUndergroundRegionPopulation.Text = "Population";
            // 
            // lstUndergroundRegionPopulation
            // 
            this.lstUndergroundRegionPopulation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstUndergroundRegionPopulation.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.lstUndergroundRegionPopulation.FormattingEnabled = true;
            this.lstUndergroundRegionPopulation.ItemHeight = 16;
            this.lstUndergroundRegionPopulation.Location = new System.Drawing.Point(3, 16);
            this.lstUndergroundRegionPopulation.Name = "lstUndergroundRegionPopulation";
            this.lstUndergroundRegionPopulation.Size = new System.Drawing.Size(833, 494);
            this.lstUndergroundRegionPopulation.TabIndex = 0;
            this.lstUndergroundRegionPopulation.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.lstRegionPopulation_DrawItem);
            // 
            // panel21
            // 
            this.panel21.Controls.Add(this.label86);
            this.panel21.Controls.Add(this.lblUndergroundRegionType);
            this.panel21.Controls.Add(this.lblUndergroundRegionDepth);
            this.panel21.Controls.Add(this.label84);
            this.panel21.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel21.Location = new System.Drawing.Point(3, 3);
            this.panel21.Name = "panel21";
            this.panel21.Size = new System.Drawing.Size(839, 341);
            this.panel21.TabIndex = 42;
            // 
            // label86
            // 
            this.label86.AutoSize = true;
            this.label86.Location = new System.Drawing.Point(3, 0);
            this.label86.Name = "label86";
            this.label86.Size = new System.Drawing.Size(39, 13);
            this.label86.TabIndex = 32;
            this.label86.Text = "Depth:";
            // 
            // lblUndergroundRegionType
            // 
            this.lblUndergroundRegionType.AccessibleDescription = "";
            this.lblUndergroundRegionType.AutoSize = true;
            this.lblUndergroundRegionType.Location = new System.Drawing.Point(61, 24);
            this.lblUndergroundRegionType.Name = "lblUndergroundRegionType";
            this.lblUndergroundRegionType.Size = new System.Drawing.Size(35, 13);
            this.lblUndergroundRegionType.TabIndex = 35;
            this.lblUndergroundRegionType.Text = "label2";
            // 
            // lblUndergroundRegionDepth
            // 
            this.lblUndergroundRegionDepth.AutoSize = true;
            this.lblUndergroundRegionDepth.Location = new System.Drawing.Point(61, 0);
            this.lblUndergroundRegionDepth.Name = "lblUndergroundRegionDepth";
            this.lblUndergroundRegionDepth.Size = new System.Drawing.Size(35, 13);
            this.lblUndergroundRegionDepth.TabIndex = 33;
            this.lblUndergroundRegionDepth.Text = "label2";
            // 
            // label84
            // 
            this.label84.AutoSize = true;
            this.label84.Location = new System.Drawing.Point(3, 24);
            this.label84.Name = "label84";
            this.label84.Size = new System.Drawing.Size(34, 13);
            this.label84.TabIndex = 34;
            this.label84.Text = "Type:";
            // 
            // FilterUndergroundRegion
            // 
            this.FilterUndergroundRegion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FilterUndergroundRegion.Location = new System.Drawing.Point(3, 866);
            this.FilterUndergroundRegion.Name = "FilterUndergroundRegion";
            this.FilterUndergroundRegion.Size = new System.Drawing.Size(164, 22);
            this.FilterUndergroundRegion.TabIndex = 6;
            this.FilterUndergroundRegion.Tag = "";
            this.FilterUndergroundRegion.Text = "Filter";
            this.FilterUndergroundRegion.UseVisualStyleBackColor = true;
            this.FilterUndergroundRegion.Click += new System.EventHandler(this.FilterButton_Click);
            // 
            // lstUndergroundRegion
            // 
            this.lstUndergroundRegion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstUndergroundRegion.FormattingEnabled = true;
            this.lstUndergroundRegion.Location = new System.Drawing.Point(3, 3);
            this.lstUndergroundRegion.Name = "lstUndergroundRegion";
            this.lstUndergroundRegion.Size = new System.Drawing.Size(164, 833);
            this.lstUndergroundRegion.TabIndex = 2;
            // 
            // tabWorldConstruction
            // 
            this.tabWorldConstruction.Controls.Add(this.tableLayoutPanel16);
            this.tabWorldConstruction.Location = new System.Drawing.Point(4, 40);
            this.tabWorldConstruction.Name = "tabWorldConstruction";
            this.tabWorldConstruction.Size = new System.Drawing.Size(1873, 873);
            this.tabWorldConstruction.TabIndex = 17;
            this.tabWorldConstruction.Text = "World Construction";
            this.tabWorldConstruction.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel16
            // 
            this.tableLayoutPanel16.ColumnCount = 2;
            this.tableLayoutPanel16.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 171F));
            this.tableLayoutPanel16.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel16.Controls.Add(this.FilterWorldConstruction, 0, 2);
            this.tableLayoutPanel16.Controls.Add(this.TextFilterWorldConstruction, 0, 1);
            this.tableLayoutPanel16.Controls.Add(this.grpWorldConstruction, 1, 0);
            this.tableLayoutPanel16.Controls.Add(this.lstWorldConstruction, 0, 0);
            this.tableLayoutPanel16.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel16.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel16.Margin = new System.Windows.Forms.Padding(2);
            this.tableLayoutPanel16.Name = "tableLayoutPanel16";
            this.tableLayoutPanel16.RowCount = 3;
            this.tableLayoutPanel16.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel16.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel16.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanel16.Size = new System.Drawing.Size(1873, 891);
            this.tableLayoutPanel16.TabIndex = 8;
            // 
            // FilterWorldConstruction
            // 
            this.FilterWorldConstruction.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FilterWorldConstruction.Location = new System.Drawing.Point(3, 866);
            this.FilterWorldConstruction.Name = "FilterWorldConstruction";
            this.FilterWorldConstruction.Size = new System.Drawing.Size(165, 22);
            this.FilterWorldConstruction.TabIndex = 6;
            this.FilterWorldConstruction.Tag = "";
            this.FilterWorldConstruction.Text = "Filter";
            this.FilterWorldConstruction.UseVisualStyleBackColor = true;
            this.FilterWorldConstruction.Click += new System.EventHandler(this.FilterButton_Click);
            // 
            // TextFilterWorldConstruction
            // 
            this.TextFilterWorldConstruction.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TextFilterWorldConstruction.Location = new System.Drawing.Point(3, 842);
            this.TextFilterWorldConstruction.Name = "TextFilterWorldConstruction";
            this.TextFilterWorldConstruction.Size = new System.Drawing.Size(165, 20);
            this.TextFilterWorldConstruction.TabIndex = 5;
            this.TextFilterWorldConstruction.TextChanged += new System.EventHandler(this.TextFilter_Changed);
            // 
            // grpWorldConstruction
            // 
            this.grpWorldConstruction.Controls.Add(this.lblWorldConstructionCoord);
            this.grpWorldConstruction.Controls.Add(this.label190);
            this.grpWorldConstruction.Controls.Add(this.lblWorldConstructionType);
            this.grpWorldConstruction.Controls.Add(this.label33);
            this.grpWorldConstruction.Controls.Add(this.grpWorldConstructionCreated);
            this.grpWorldConstruction.Controls.Add(this.lblWorldConstructionFrom);
            this.grpWorldConstruction.Controls.Add(this.lblWorldConstructionMaster);
            this.grpWorldConstruction.Controls.Add(this.lblWorldConstructionTo);
            this.grpWorldConstruction.Controls.Add(this.label82);
            this.grpWorldConstruction.Controls.Add(this.label89);
            this.grpWorldConstruction.Controls.Add(this.label91);
            this.grpWorldConstruction.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpWorldConstruction.Location = new System.Drawing.Point(174, 3);
            this.grpWorldConstruction.Name = "grpWorldConstruction";
            this.tableLayoutPanel16.SetRowSpan(this.grpWorldConstruction, 3);
            this.grpWorldConstruction.Size = new System.Drawing.Size(1696, 885);
            this.grpWorldConstruction.TabIndex = 3;
            this.grpWorldConstruction.TabStop = false;
            this.grpWorldConstruction.Visible = false;
            // 
            // label190
            // 
            this.label190.AutoSize = true;
            this.label190.Location = new System.Drawing.Point(12, 208);
            this.label190.Name = "label190";
            this.label190.Size = new System.Drawing.Size(38, 13);
            this.label190.TabIndex = 40;
            this.label190.Text = "Coord:";
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Location = new System.Drawing.Point(12, 186);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(34, 13);
            this.label33.TabIndex = 38;
            this.label33.Text = "Type:";
            // 
            // grpWorldConstructionCreated
            // 
            this.grpWorldConstructionCreated.Controls.Add(this.lblWorldConstructionCreatedTime);
            this.grpWorldConstructionCreated.Controls.Add(this.lblWorldConstructionCreatedByCiv);
            this.grpWorldConstructionCreated.Controls.Add(this.lblWorldConstructionCreatedBy);
            this.grpWorldConstructionCreated.Controls.Add(this.label85);
            this.grpWorldConstructionCreated.Controls.Add(this.label87);
            this.grpWorldConstructionCreated.Controls.Add(this.label88);
            this.grpWorldConstructionCreated.Location = new System.Drawing.Point(14, 83);
            this.grpWorldConstructionCreated.Name = "grpWorldConstructionCreated";
            this.grpWorldConstructionCreated.Size = new System.Drawing.Size(168, 91);
            this.grpWorldConstructionCreated.TabIndex = 33;
            this.grpWorldConstructionCreated.TabStop = false;
            this.grpWorldConstructionCreated.Text = "Created";
            // 
            // label85
            // 
            this.label85.AutoSize = true;
            this.label85.Location = new System.Drawing.Point(6, 64);
            this.label85.Name = "label85";
            this.label85.Size = new System.Drawing.Size(24, 13);
            this.label85.TabIndex = 3;
            this.label85.Text = "On:";
            // 
            // label87
            // 
            this.label87.AutoSize = true;
            this.label87.Location = new System.Drawing.Point(6, 43);
            this.label87.Name = "label87";
            this.label87.Size = new System.Drawing.Size(21, 13);
            this.label87.TabIndex = 2;
            this.label87.Text = "Of:";
            // 
            // label88
            // 
            this.label88.AutoSize = true;
            this.label88.Location = new System.Drawing.Point(6, 21);
            this.label88.Name = "label88";
            this.label88.Size = new System.Drawing.Size(25, 13);
            this.label88.TabIndex = 0;
            this.label88.Text = "By: ";
            // 
            // label82
            // 
            this.label82.AutoSize = true;
            this.label82.Location = new System.Drawing.Point(12, 58);
            this.label82.Name = "label82";
            this.label82.Size = new System.Drawing.Size(44, 13);
            this.label82.TabIndex = 29;
            this.label82.Text = "To Site:";
            // 
            // label89
            // 
            this.label89.AutoSize = true;
            this.label89.Location = new System.Drawing.Point(12, 37);
            this.label89.Name = "label89";
            this.label89.Size = new System.Drawing.Size(54, 13);
            this.label89.TabIndex = 25;
            this.label89.Text = "From Site:";
            // 
            // label91
            // 
            this.label91.AutoSize = true;
            this.label91.Location = new System.Drawing.Point(12, 15);
            this.label91.Name = "label91";
            this.label91.Size = new System.Drawing.Size(45, 13);
            this.label91.TabIndex = 23;
            this.label91.Text = "Master: ";
            // 
            // lstWorldConstruction
            // 
            this.lstWorldConstruction.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstWorldConstruction.FormattingEnabled = true;
            this.lstWorldConstruction.Location = new System.Drawing.Point(3, 3);
            this.lstWorldConstruction.Name = "lstWorldConstruction";
            this.lstWorldConstruction.Size = new System.Drawing.Size(165, 833);
            this.lstWorldConstruction.TabIndex = 2;
            // 
            // tabDynasty
            // 
            this.tabDynasty.Controls.Add(this.tableLayoutPanel17);
            this.tabDynasty.Location = new System.Drawing.Point(4, 40);
            this.tabDynasty.Name = "tabDynasty";
            this.tabDynasty.Size = new System.Drawing.Size(1873, 873);
            this.tabDynasty.TabIndex = 18;
            this.tabDynasty.Text = "Dynasty";
            this.tabDynasty.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel17
            // 
            this.tableLayoutPanel17.ColumnCount = 2;
            this.tableLayoutPanel17.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 171F));
            this.tableLayoutPanel17.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel17.Controls.Add(this.TextFilterDynasty, 0, 1);
            this.tableLayoutPanel17.Controls.Add(this.FilterDynasty, 0, 2);
            this.tableLayoutPanel17.Controls.Add(this.lstDynasty, 0, 0);
            this.tableLayoutPanel17.Controls.Add(this.grpDynasty, 1, 0);
            this.tableLayoutPanel17.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel17.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel17.Margin = new System.Windows.Forms.Padding(2);
            this.tableLayoutPanel17.Name = "tableLayoutPanel17";
            this.tableLayoutPanel17.RowCount = 3;
            this.tableLayoutPanel17.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel17.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel17.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanel17.Size = new System.Drawing.Size(1873, 891);
            this.tableLayoutPanel17.TabIndex = 9;
            // 
            // TextFilterDynasty
            // 
            this.TextFilterDynasty.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TextFilterDynasty.Location = new System.Drawing.Point(3, 842);
            this.TextFilterDynasty.Name = "TextFilterDynasty";
            this.TextFilterDynasty.Size = new System.Drawing.Size(165, 20);
            this.TextFilterDynasty.TabIndex = 5;
            this.TextFilterDynasty.TextChanged += new System.EventHandler(this.TextFilter_Changed);
            // 
            // FilterDynasty
            // 
            this.FilterDynasty.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FilterDynasty.Location = new System.Drawing.Point(3, 866);
            this.FilterDynasty.Name = "FilterDynasty";
            this.FilterDynasty.Size = new System.Drawing.Size(165, 22);
            this.FilterDynasty.TabIndex = 4;
            this.FilterDynasty.Tag = "";
            this.FilterDynasty.Text = "Filter";
            this.FilterDynasty.UseVisualStyleBackColor = true;
            this.FilterDynasty.TextChanged += new System.EventHandler(this.FilterButton_Click);
            this.FilterDynasty.Click += new System.EventHandler(this.FilterButton_Click);
            // 
            // lstDynasty
            // 
            this.lstDynasty.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstDynasty.FormattingEnabled = true;
            this.lstDynasty.Location = new System.Drawing.Point(3, 3);
            this.lstDynasty.Name = "lstDynasty";
            this.lstDynasty.Size = new System.Drawing.Size(165, 833);
            this.lstDynasty.TabIndex = 2;
            // 
            // grpDynasty
            // 
            this.grpDynasty.Controls.Add(this.tableLayoutPanel35);
            this.grpDynasty.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpDynasty.Location = new System.Drawing.Point(174, 3);
            this.grpDynasty.Name = "grpDynasty";
            this.tableLayoutPanel17.SetRowSpan(this.grpDynasty, 3);
            this.grpDynasty.Size = new System.Drawing.Size(1696, 885);
            this.grpDynasty.TabIndex = 3;
            this.grpDynasty.TabStop = false;
            this.grpDynasty.Visible = false;
            // 
            // tableLayoutPanel35
            // 
            this.tableLayoutPanel35.ColumnCount = 2;
            this.tableLayoutPanel35.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.44633F));
            this.tableLayoutPanel35.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 66.55367F));
            this.tableLayoutPanel35.Controls.Add(this.panel18, 0, 0);
            this.tableLayoutPanel35.Controls.Add(this.grpDynastyMembers, 0, 1);
            this.tableLayoutPanel35.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel35.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel35.Name = "tableLayoutPanel35";
            this.tableLayoutPanel35.RowCount = 2;
            this.tableLayoutPanel35.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 18.65546F));
            this.tableLayoutPanel35.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 81.34454F));
            this.tableLayoutPanel35.Size = new System.Drawing.Size(1690, 866);
            this.tableLayoutPanel35.TabIndex = 24;
            // 
            // panel18
            // 
            this.panel18.Controls.Add(this.label173);
            this.panel18.Controls.Add(this.label171);
            this.panel18.Controls.Add(this.lblDynastyLength);
            this.panel18.Controls.Add(this.lblDynastyType);
            this.panel18.Controls.Add(this.lblDynastyFounder);
            this.panel18.Controls.Add(this.label164);
            this.panel18.Controls.Add(this.label154);
            this.panel18.Controls.Add(this.lblDynastyCivilization);
            this.panel18.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel18.Location = new System.Drawing.Point(3, 3);
            this.panel18.Name = "panel18";
            this.panel18.Size = new System.Drawing.Size(559, 155);
            this.panel18.TabIndex = 25;
            // 
            // label173
            // 
            this.label173.AutoSize = true;
            this.label173.Location = new System.Drawing.Point(3, 0);
            this.label173.Name = "label173";
            this.label173.Size = new System.Drawing.Size(49, 13);
            this.label173.TabIndex = 15;
            this.label173.Text = "Founder:";
            // 
            // label171
            // 
            this.label171.AutoSize = true;
            this.label171.Location = new System.Drawing.Point(3, 22);
            this.label171.Name = "label171";
            this.label171.Size = new System.Drawing.Size(59, 13);
            this.label171.TabIndex = 17;
            this.label171.Text = "Civilization:";
            // 
            // lblDynastyType
            // 
            this.lblDynastyType.AutoSize = true;
            this.lblDynastyType.Location = new System.Drawing.Point(67, 44);
            this.lblDynastyType.Name = "lblDynastyType";
            this.lblDynastyType.Size = new System.Drawing.Size(35, 13);
            this.lblDynastyType.TabIndex = 18;
            this.lblDynastyType.Text = "label2";
            // 
            // label164
            // 
            this.label164.AutoSize = true;
            this.label164.Location = new System.Drawing.Point(3, 66);
            this.label164.Name = "label164";
            this.label164.Size = new System.Drawing.Size(43, 13);
            this.label164.TabIndex = 19;
            this.label164.Text = "Length:";
            // 
            // label154
            // 
            this.label154.AutoSize = true;
            this.label154.Location = new System.Drawing.Point(3, 44);
            this.label154.Name = "label154";
            this.label154.Size = new System.Drawing.Size(34, 13);
            this.label154.TabIndex = 21;
            this.label154.Text = "Type:";
            // 
            // grpDynastyMembers
            // 
            this.grpDynastyMembers.Controls.Add(this.lstDynastyMembers);
            this.grpDynastyMembers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpDynastyMembers.Location = new System.Drawing.Point(3, 164);
            this.grpDynastyMembers.Name = "grpDynastyMembers";
            this.grpDynastyMembers.Size = new System.Drawing.Size(559, 699);
            this.grpDynastyMembers.TabIndex = 11;
            this.grpDynastyMembers.TabStop = false;
            this.grpDynastyMembers.Text = "Leaders";
            // 
            // lstDynastyMembers
            // 
            this.lstDynastyMembers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstDynastyMembers.FormattingEnabled = true;
            this.lstDynastyMembers.Location = new System.Drawing.Point(3, 16);
            this.lstDynastyMembers.Name = "lstDynastyMembers";
            this.lstDynastyMembers.Size = new System.Drawing.Size(553, 680);
            this.lstDynastyMembers.TabIndex = 0;
            // 
            // tabLandmass
            // 
            this.tabLandmass.Controls.Add(this.tableLayoutPanel47);
            this.tabLandmass.Location = new System.Drawing.Point(4, 40);
            this.tabLandmass.Name = "tabLandmass";
            this.tabLandmass.Size = new System.Drawing.Size(1873, 873);
            this.tabLandmass.TabIndex = 39;
            this.tabLandmass.Text = "Landmass";
            this.tabLandmass.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel47
            // 
            this.tableLayoutPanel47.ColumnCount = 2;
            this.tableLayoutPanel47.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 213F));
            this.tableLayoutPanel47.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel47.Controls.Add(this.FilterLandmass, 0, 2);
            this.tableLayoutPanel47.Controls.Add(this.TextFilterLandmass, 0, 1);
            this.tableLayoutPanel47.Controls.Add(this.grpLandmass, 1, 0);
            this.tableLayoutPanel47.Controls.Add(this.lstLandmass, 0, 0);
            this.tableLayoutPanel47.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel47.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel47.Margin = new System.Windows.Forms.Padding(2);
            this.tableLayoutPanel47.Name = "tableLayoutPanel47";
            this.tableLayoutPanel47.RowCount = 3;
            this.tableLayoutPanel47.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel47.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel47.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanel47.Size = new System.Drawing.Size(1873, 891);
            this.tableLayoutPanel47.TabIndex = 8;
            // 
            // FilterLandmass
            // 
            this.FilterLandmass.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FilterLandmass.Location = new System.Drawing.Point(3, 866);
            this.FilterLandmass.Name = "FilterLandmass";
            this.FilterLandmass.Size = new System.Drawing.Size(207, 22);
            this.FilterLandmass.TabIndex = 6;
            this.FilterLandmass.Tag = "";
            this.FilterLandmass.Text = "Filter";
            this.FilterLandmass.UseVisualStyleBackColor = true;
            // 
            // TextFilterLandmass
            // 
            this.TextFilterLandmass.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TextFilterLandmass.Location = new System.Drawing.Point(3, 842);
            this.TextFilterLandmass.Name = "TextFilterLandmass";
            this.TextFilterLandmass.Size = new System.Drawing.Size(207, 20);
            this.TextFilterLandmass.TabIndex = 5;
            // 
            // grpLandmass
            // 
            this.grpLandmass.Controls.Add(this.lblLandmassCenter);
            this.grpLandmass.Controls.Add(this.label322);
            this.grpLandmass.Controls.Add(this.lblLandmassMax);
            this.grpLandmass.Controls.Add(this.lblLandmassMin);
            this.grpLandmass.Controls.Add(this.lblLandmassArea);
            this.grpLandmass.Controls.Add(this.label330);
            this.grpLandmass.Controls.Add(this.label333);
            this.grpLandmass.Controls.Add(this.label324);
            this.grpLandmass.Controls.Add(this.lblLandmassName);
            this.grpLandmass.Controls.Add(this.label329);
            this.grpLandmass.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpLandmass.Location = new System.Drawing.Point(216, 3);
            this.grpLandmass.Name = "grpLandmass";
            this.tableLayoutPanel47.SetRowSpan(this.grpLandmass, 3);
            this.grpLandmass.Size = new System.Drawing.Size(1654, 885);
            this.grpLandmass.TabIndex = 3;
            this.grpLandmass.TabStop = false;
            this.grpLandmass.Visible = false;
            // 
            // label322
            // 
            this.label322.AutoSize = true;
            this.label322.Location = new System.Drawing.Point(12, 66);
            this.label322.Name = "label322";
            this.label322.Size = new System.Drawing.Size(41, 13);
            this.label322.TabIndex = 39;
            this.label322.Text = "Center:";
            // 
            // lblLandmassArea
            // 
            this.lblLandmassArea.AutoSize = true;
            this.lblLandmassArea.Location = new System.Drawing.Point(67, 111);
            this.lblLandmassArea.Name = "lblLandmassArea";
            this.lblLandmassArea.Size = new System.Drawing.Size(35, 13);
            this.lblLandmassArea.TabIndex = 26;
            this.lblLandmassArea.Text = "label2";
            // 
            // label330
            // 
            this.label330.AutoSize = true;
            this.label330.Location = new System.Drawing.Point(12, 111);
            this.label330.Name = "label330";
            this.label330.Size = new System.Drawing.Size(32, 13);
            this.label330.TabIndex = 25;
            this.label330.Text = "Area:";
            // 
            // label333
            // 
            this.label333.AutoSize = true;
            this.label333.Location = new System.Drawing.Point(12, 89);
            this.label333.Name = "label333";
            this.label333.Size = new System.Drawing.Size(30, 13);
            this.label333.TabIndex = 23;
            this.label333.Text = "Max:";
            // 
            // label324
            // 
            this.label324.AutoSize = true;
            this.label324.Location = new System.Drawing.Point(12, 43);
            this.label324.Name = "label324";
            this.label324.Size = new System.Drawing.Size(27, 13);
            this.label324.TabIndex = 21;
            this.label324.Text = "Min:";
            // 
            // lblLandmassName
            // 
            this.lblLandmassName.AutoSize = true;
            this.lblLandmassName.Location = new System.Drawing.Point(67, 21);
            this.lblLandmassName.Name = "lblLandmassName";
            this.lblLandmassName.Size = new System.Drawing.Size(35, 13);
            this.lblLandmassName.TabIndex = 20;
            this.lblLandmassName.Text = "label2";
            // 
            // label329
            // 
            this.label329.AutoSize = true;
            this.label329.Location = new System.Drawing.Point(12, 21);
            this.label329.Name = "label329";
            this.label329.Size = new System.Drawing.Size(41, 13);
            this.label329.TabIndex = 19;
            this.label329.Text = "Name: ";
            // 
            // lstLandmass
            // 
            this.lstLandmass.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstLandmass.FormattingEnabled = true;
            this.lstLandmass.Location = new System.Drawing.Point(3, 3);
            this.lstLandmass.Name = "lstLandmass";
            this.lstLandmass.Size = new System.Drawing.Size(207, 833);
            this.lstLandmass.TabIndex = 2;
            // 
            // tabMountain
            // 
            this.tabMountain.Controls.Add(this.tableLayoutPanel41);
            this.tabMountain.Location = new System.Drawing.Point(4, 40);
            this.tabMountain.Name = "tabMountain";
            this.tabMountain.Size = new System.Drawing.Size(1873, 873);
            this.tabMountain.TabIndex = 20;
            this.tabMountain.Text = "Mountain";
            this.tabMountain.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel41
            // 
            this.tableLayoutPanel41.ColumnCount = 2;
            this.tableLayoutPanel41.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 213F));
            this.tableLayoutPanel41.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel41.Controls.Add(this.FilterMountain, 0, 2);
            this.tableLayoutPanel41.Controls.Add(this.TextFilterMountain, 0, 1);
            this.tableLayoutPanel41.Controls.Add(this.grpMountain, 1, 0);
            this.tableLayoutPanel41.Controls.Add(this.lstMountain, 0, 0);
            this.tableLayoutPanel41.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel41.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel41.Margin = new System.Windows.Forms.Padding(2);
            this.tableLayoutPanel41.Name = "tableLayoutPanel41";
            this.tableLayoutPanel41.RowCount = 3;
            this.tableLayoutPanel41.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel41.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel41.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanel41.Size = new System.Drawing.Size(1873, 891);
            this.tableLayoutPanel41.TabIndex = 8;
            // 
            // FilterMountain
            // 
            this.FilterMountain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FilterMountain.Location = new System.Drawing.Point(3, 866);
            this.FilterMountain.Name = "FilterMountain";
            this.FilterMountain.Size = new System.Drawing.Size(207, 22);
            this.FilterMountain.TabIndex = 6;
            this.FilterMountain.Tag = "";
            this.FilterMountain.Text = "Filter";
            this.FilterMountain.UseVisualStyleBackColor = true;
            this.FilterMountain.Click += new System.EventHandler(this.FilterButton_Click);
            // 
            // TextFilterMountain
            // 
            this.TextFilterMountain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TextFilterMountain.Location = new System.Drawing.Point(3, 842);
            this.TextFilterMountain.Name = "TextFilterMountain";
            this.TextFilterMountain.Size = new System.Drawing.Size(207, 20);
            this.TextFilterMountain.TabIndex = 5;
            this.TextFilterMountain.TextChanged += new System.EventHandler(this.TextFilter_Changed);
            // 
            // grpMountain
            // 
            this.grpMountain.Controls.Add(this.lblMountainCoord);
            this.grpMountain.Controls.Add(this.lblMountainHeight);
            this.grpMountain.Controls.Add(this.label198);
            this.grpMountain.Controls.Add(this.label195);
            this.grpMountain.Controls.Add(this.lblMountainAltName);
            this.grpMountain.Controls.Add(this.label194);
            this.grpMountain.Controls.Add(this.lblMountainName);
            this.grpMountain.Controls.Add(this.label196);
            this.grpMountain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpMountain.Location = new System.Drawing.Point(216, 3);
            this.grpMountain.Name = "grpMountain";
            this.tableLayoutPanel41.SetRowSpan(this.grpMountain, 3);
            this.grpMountain.Size = new System.Drawing.Size(1654, 885);
            this.grpMountain.TabIndex = 3;
            this.grpMountain.TabStop = false;
            this.grpMountain.Visible = false;
            // 
            // lblMountainHeight
            // 
            this.lblMountainHeight.AutoSize = true;
            this.lblMountainHeight.Location = new System.Drawing.Point(68, 89);
            this.lblMountainHeight.Name = "lblMountainHeight";
            this.lblMountainHeight.Size = new System.Drawing.Size(35, 13);
            this.lblMountainHeight.TabIndex = 26;
            this.lblMountainHeight.Text = "label2";
            // 
            // label198
            // 
            this.label198.AutoSize = true;
            this.label198.Location = new System.Drawing.Point(12, 89);
            this.label198.Name = "label198";
            this.label198.Size = new System.Drawing.Size(41, 13);
            this.label198.TabIndex = 25;
            this.label198.Text = "Height:";
            // 
            // label195
            // 
            this.label195.AutoSize = true;
            this.label195.Location = new System.Drawing.Point(12, 67);
            this.label195.Name = "label195";
            this.label195.Size = new System.Drawing.Size(38, 13);
            this.label195.TabIndex = 23;
            this.label195.Text = "Coord:";
            // 
            // lblMountainAltName
            // 
            this.lblMountainAltName.AutoSize = true;
            this.lblMountainAltName.Location = new System.Drawing.Point(67, 43);
            this.lblMountainAltName.Name = "lblMountainAltName";
            this.lblMountainAltName.Size = new System.Drawing.Size(35, 13);
            this.lblMountainAltName.TabIndex = 22;
            this.lblMountainAltName.Text = "label2";
            // 
            // label194
            // 
            this.label194.AutoSize = true;
            this.label194.Location = new System.Drawing.Point(12, 43);
            this.label194.Name = "label194";
            this.label194.Size = new System.Drawing.Size(50, 13);
            this.label194.TabIndex = 21;
            this.label194.Text = "AltName:";
            // 
            // lblMountainName
            // 
            this.lblMountainName.AutoSize = true;
            this.lblMountainName.Location = new System.Drawing.Point(67, 21);
            this.lblMountainName.Name = "lblMountainName";
            this.lblMountainName.Size = new System.Drawing.Size(35, 13);
            this.lblMountainName.TabIndex = 20;
            this.lblMountainName.Text = "label2";
            // 
            // label196
            // 
            this.label196.AutoSize = true;
            this.label196.Location = new System.Drawing.Point(12, 21);
            this.label196.Name = "label196";
            this.label196.Size = new System.Drawing.Size(41, 13);
            this.label196.TabIndex = 19;
            this.label196.Text = "Name: ";
            // 
            // lstMountain
            // 
            this.lstMountain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstMountain.FormattingEnabled = true;
            this.lstMountain.Location = new System.Drawing.Point(3, 3);
            this.lstMountain.Name = "lstMountain";
            this.lstMountain.Size = new System.Drawing.Size(207, 833);
            this.lstMountain.TabIndex = 2;
            // 
            // tabRiver
            // 
            this.tabRiver.Controls.Add(this.tableLayoutPanel42);
            this.tabRiver.Location = new System.Drawing.Point(4, 40);
            this.tabRiver.Name = "tabRiver";
            this.tabRiver.Size = new System.Drawing.Size(1873, 873);
            this.tabRiver.TabIndex = 21;
            this.tabRiver.Text = "River";
            this.tabRiver.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel42
            // 
            this.tableLayoutPanel42.ColumnCount = 2;
            this.tableLayoutPanel42.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 213F));
            this.tableLayoutPanel42.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel42.Controls.Add(this.FilterRiver, 0, 2);
            this.tableLayoutPanel42.Controls.Add(this.TextFilterRiver, 0, 1);
            this.tableLayoutPanel42.Controls.Add(this.grpRiver, 1, 0);
            this.tableLayoutPanel42.Controls.Add(this.lstRiver, 0, 0);
            this.tableLayoutPanel42.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel42.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel42.Margin = new System.Windows.Forms.Padding(2);
            this.tableLayoutPanel42.Name = "tableLayoutPanel42";
            this.tableLayoutPanel42.RowCount = 3;
            this.tableLayoutPanel42.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel42.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel42.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanel42.Size = new System.Drawing.Size(1873, 891);
            this.tableLayoutPanel42.TabIndex = 8;
            // 
            // FilterRiver
            // 
            this.FilterRiver.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FilterRiver.Location = new System.Drawing.Point(3, 866);
            this.FilterRiver.Name = "FilterRiver";
            this.FilterRiver.Size = new System.Drawing.Size(207, 22);
            this.FilterRiver.TabIndex = 6;
            this.FilterRiver.Tag = "";
            this.FilterRiver.Text = "Filter";
            this.FilterRiver.UseVisualStyleBackColor = true;
            this.FilterRiver.Click += new System.EventHandler(this.FilterButton_Click);
            // 
            // TextFilterRiver
            // 
            this.TextFilterRiver.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TextFilterRiver.Location = new System.Drawing.Point(3, 842);
            this.TextFilterRiver.Name = "TextFilterRiver";
            this.TextFilterRiver.Size = new System.Drawing.Size(207, 20);
            this.TextFilterRiver.TabIndex = 5;
            this.TextFilterRiver.TextChanged += new System.EventHandler(this.TextFilter_Changed);
            // 
            // grpRiver
            // 
            this.grpRiver.Controls.Add(this.grpRiverTributaries);
            this.grpRiver.Controls.Add(this.lblRiverParent);
            this.grpRiver.Controls.Add(this.label200);
            this.grpRiver.Controls.Add(this.lblRiverEndsAt);
            this.grpRiver.Controls.Add(this.lblRiverElevation);
            this.grpRiver.Controls.Add(this.label197);
            this.grpRiver.Controls.Add(this.label199);
            this.grpRiver.Controls.Add(this.lblRiverAltName);
            this.grpRiver.Controls.Add(this.label201);
            this.grpRiver.Controls.Add(this.lblRiverName);
            this.grpRiver.Controls.Add(this.label203);
            this.grpRiver.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpRiver.Location = new System.Drawing.Point(216, 3);
            this.grpRiver.Name = "grpRiver";
            this.tableLayoutPanel42.SetRowSpan(this.grpRiver, 3);
            this.grpRiver.Size = new System.Drawing.Size(1654, 885);
            this.grpRiver.TabIndex = 3;
            this.grpRiver.TabStop = false;
            this.grpRiver.Visible = false;
            // 
            // grpRiverTributaries
            // 
            this.grpRiverTributaries.Controls.Add(this.lstRiverTributaries);
            this.grpRiverTributaries.Location = new System.Drawing.Point(15, 128);
            this.grpRiverTributaries.Name = "grpRiverTributaries";
            this.grpRiverTributaries.Size = new System.Drawing.Size(211, 298);
            this.grpRiverTributaries.TabIndex = 40;
            this.grpRiverTributaries.TabStop = false;
            this.grpRiverTributaries.Text = "Tributaries";
            this.grpRiverTributaries.Visible = false;
            // 
            // lstRiverTributaries
            // 
            this.lstRiverTributaries.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstRiverTributaries.FormattingEnabled = true;
            this.lstRiverTributaries.Location = new System.Drawing.Point(3, 16);
            this.lstRiverTributaries.Name = "lstRiverTributaries";
            this.lstRiverTributaries.Size = new System.Drawing.Size(205, 279);
            this.lstRiverTributaries.TabIndex = 0;
            // 
            // label200
            // 
            this.label200.AutoSize = true;
            this.label200.Location = new System.Drawing.Point(12, 111);
            this.label200.Name = "label200";
            this.label200.Size = new System.Drawing.Size(41, 13);
            this.label200.TabIndex = 38;
            this.label200.Text = "Parent:";
            // 
            // lblRiverElevation
            // 
            this.lblRiverElevation.AutoSize = true;
            this.lblRiverElevation.Location = new System.Drawing.Point(68, 89);
            this.lblRiverElevation.Name = "lblRiverElevation";
            this.lblRiverElevation.Size = new System.Drawing.Size(35, 13);
            this.lblRiverElevation.TabIndex = 26;
            this.lblRiverElevation.Text = "label2";
            // 
            // label197
            // 
            this.label197.AutoSize = true;
            this.label197.Location = new System.Drawing.Point(12, 89);
            this.label197.Name = "label197";
            this.label197.Size = new System.Drawing.Size(54, 13);
            this.label197.TabIndex = 25;
            this.label197.Text = "Elevation:";
            // 
            // label199
            // 
            this.label199.AutoSize = true;
            this.label199.Location = new System.Drawing.Point(12, 67);
            this.label199.Name = "label199";
            this.label199.Size = new System.Drawing.Size(47, 13);
            this.label199.TabIndex = 23;
            this.label199.Text = "Ends At:";
            // 
            // lblRiverAltName
            // 
            this.lblRiverAltName.AutoSize = true;
            this.lblRiverAltName.Location = new System.Drawing.Point(67, 43);
            this.lblRiverAltName.Name = "lblRiverAltName";
            this.lblRiverAltName.Size = new System.Drawing.Size(35, 13);
            this.lblRiverAltName.TabIndex = 22;
            this.lblRiverAltName.Text = "label2";
            // 
            // label201
            // 
            this.label201.AutoSize = true;
            this.label201.Location = new System.Drawing.Point(12, 43);
            this.label201.Name = "label201";
            this.label201.Size = new System.Drawing.Size(50, 13);
            this.label201.TabIndex = 21;
            this.label201.Text = "AltName:";
            // 
            // lblRiverName
            // 
            this.lblRiverName.AutoSize = true;
            this.lblRiverName.Location = new System.Drawing.Point(67, 21);
            this.lblRiverName.Name = "lblRiverName";
            this.lblRiverName.Size = new System.Drawing.Size(35, 13);
            this.lblRiverName.TabIndex = 20;
            this.lblRiverName.Text = "label2";
            // 
            // label203
            // 
            this.label203.AutoSize = true;
            this.label203.Location = new System.Drawing.Point(12, 21);
            this.label203.Name = "label203";
            this.label203.Size = new System.Drawing.Size(41, 13);
            this.label203.TabIndex = 19;
            this.label203.Text = "Name: ";
            // 
            // lstRiver
            // 
            this.lstRiver.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstRiver.FormattingEnabled = true;
            this.lstRiver.Location = new System.Drawing.Point(3, 3);
            this.lstRiver.Name = "lstRiver";
            this.lstRiver.Size = new System.Drawing.Size(207, 833);
            this.lstRiver.TabIndex = 2;
            // 
            // tabArmy
            // 
            this.tabArmy.Controls.Add(this.tableLayoutPanel43);
            this.tabArmy.Location = new System.Drawing.Point(4, 40);
            this.tabArmy.Name = "tabArmy";
            this.tabArmy.Size = new System.Drawing.Size(1873, 873);
            this.tabArmy.TabIndex = 22;
            this.tabArmy.Text = "Army";
            this.tabArmy.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel43
            // 
            this.tableLayoutPanel43.ColumnCount = 2;
            this.tableLayoutPanel43.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 213F));
            this.tableLayoutPanel43.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel43.Controls.Add(this.FilterArmy, 0, 2);
            this.tableLayoutPanel43.Controls.Add(this.TextFilterArmy, 0, 1);
            this.tableLayoutPanel43.Controls.Add(this.grpArmy, 1, 0);
            this.tableLayoutPanel43.Controls.Add(this.lstArmy, 0, 0);
            this.tableLayoutPanel43.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel43.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel43.Margin = new System.Windows.Forms.Padding(2);
            this.tableLayoutPanel43.Name = "tableLayoutPanel43";
            this.tableLayoutPanel43.RowCount = 3;
            this.tableLayoutPanel43.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel43.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel43.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanel43.Size = new System.Drawing.Size(1873, 891);
            this.tableLayoutPanel43.TabIndex = 8;
            // 
            // FilterArmy
            // 
            this.FilterArmy.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FilterArmy.Location = new System.Drawing.Point(3, 866);
            this.FilterArmy.Name = "FilterArmy";
            this.FilterArmy.Size = new System.Drawing.Size(207, 22);
            this.FilterArmy.TabIndex = 6;
            this.FilterArmy.Tag = "";
            this.FilterArmy.Text = "Filter";
            this.FilterArmy.UseVisualStyleBackColor = true;
            this.FilterArmy.Click += new System.EventHandler(this.FilterButton_Click);
            // 
            // TextFilterArmy
            // 
            this.TextFilterArmy.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TextFilterArmy.Location = new System.Drawing.Point(3, 842);
            this.TextFilterArmy.Name = "TextFilterArmy";
            this.TextFilterArmy.Size = new System.Drawing.Size(207, 20);
            this.TextFilterArmy.TabIndex = 5;
            this.TextFilterArmy.TextChanged += new System.EventHandler(this.TextFilter_Changed);
            // 
            // grpArmy
            // 
            this.grpArmy.Controls.Add(this.lblArmyItem);
            this.grpArmy.Controls.Add(this.lblArmyMaterial);
            this.grpArmy.Controls.Add(this.label262);
            this.grpArmy.Controls.Add(this.lblArmyItemSubtype);
            this.grpArmy.Controls.Add(this.label252);
            this.grpArmy.Controls.Add(this.lblArmyItemType);
            this.grpArmy.Controls.Add(this.label254);
            this.grpArmy.Controls.Add(this.label250);
            this.grpArmy.Controls.Add(this.lblArmyLocation);
            this.grpArmy.Controls.Add(this.label248);
            this.grpArmy.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpArmy.Location = new System.Drawing.Point(216, 3);
            this.grpArmy.Name = "grpArmy";
            this.tableLayoutPanel43.SetRowSpan(this.grpArmy, 3);
            this.grpArmy.Size = new System.Drawing.Size(1654, 885);
            this.grpArmy.TabIndex = 3;
            this.grpArmy.TabStop = false;
            this.grpArmy.Visible = false;
            // 
            // lblArmyMaterial
            // 
            this.lblArmyMaterial.AutoSize = true;
            this.lblArmyMaterial.Location = new System.Drawing.Point(67, 110);
            this.lblArmyMaterial.Name = "lblArmyMaterial";
            this.lblArmyMaterial.Size = new System.Drawing.Size(35, 13);
            this.lblArmyMaterial.TabIndex = 30;
            this.lblArmyMaterial.Text = "label2";
            // 
            // label262
            // 
            this.label262.AutoSize = true;
            this.label262.Location = new System.Drawing.Point(12, 110);
            this.label262.Name = "label262";
            this.label262.Size = new System.Drawing.Size(47, 13);
            this.label262.TabIndex = 29;
            this.label262.Text = "Material:";
            // 
            // lblArmyItemSubtype
            // 
            this.lblArmyItemSubtype.AutoSize = true;
            this.lblArmyItemSubtype.Location = new System.Drawing.Point(67, 88);
            this.lblArmyItemSubtype.Name = "lblArmyItemSubtype";
            this.lblArmyItemSubtype.Size = new System.Drawing.Size(35, 13);
            this.lblArmyItemSubtype.TabIndex = 28;
            this.lblArmyItemSubtype.Text = "label2";
            // 
            // label252
            // 
            this.label252.AutoSize = true;
            this.label252.Location = new System.Drawing.Point(12, 88);
            this.label252.Name = "label252";
            this.label252.Size = new System.Drawing.Size(56, 13);
            this.label252.TabIndex = 27;
            this.label252.Text = " SubType:";
            // 
            // lblArmyItemType
            // 
            this.lblArmyItemType.AutoSize = true;
            this.lblArmyItemType.Location = new System.Drawing.Point(67, 66);
            this.lblArmyItemType.Name = "lblArmyItemType";
            this.lblArmyItemType.Size = new System.Drawing.Size(35, 13);
            this.lblArmyItemType.TabIndex = 26;
            this.lblArmyItemType.Text = "label2";
            // 
            // label254
            // 
            this.label254.AutoSize = true;
            this.label254.Location = new System.Drawing.Point(12, 66);
            this.label254.Name = "label254";
            this.label254.Size = new System.Drawing.Size(57, 13);
            this.label254.TabIndex = 25;
            this.label254.Text = "Item Type:";
            // 
            // label250
            // 
            this.label250.AutoSize = true;
            this.label250.Location = new System.Drawing.Point(12, 44);
            this.label250.Name = "label250";
            this.label250.Size = new System.Drawing.Size(30, 13);
            this.label250.TabIndex = 23;
            this.label250.Text = "Item:";
            // 
            // lblArmyLocation
            // 
            this.lblArmyLocation.AutoSize = true;
            this.lblArmyLocation.Location = new System.Drawing.Point(67, 21);
            this.lblArmyLocation.Name = "lblArmyLocation";
            this.lblArmyLocation.Size = new System.Drawing.Size(35, 13);
            this.lblArmyLocation.TabIndex = 22;
            this.lblArmyLocation.Text = "label2";
            // 
            // label248
            // 
            this.label248.AutoSize = true;
            this.label248.Location = new System.Drawing.Point(12, 21);
            this.label248.Name = "label248";
            this.label248.Size = new System.Drawing.Size(54, 13);
            this.label248.TabIndex = 21;
            this.label248.Text = "Location: ";
            // 
            // lstArmy
            // 
            this.lstArmy.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstArmy.FormattingEnabled = true;
            this.lstArmy.Location = new System.Drawing.Point(3, 3);
            this.lstArmy.Name = "lstArmy";
            this.lstArmy.Size = new System.Drawing.Size(207, 833);
            this.lstArmy.TabIndex = 2;
            // 
            // tabUnit
            // 
            this.tabUnit.Controls.Add(this.tableLayoutPanel44);
            this.tabUnit.Location = new System.Drawing.Point(4, 40);
            this.tabUnit.Name = "tabUnit";
            this.tabUnit.Size = new System.Drawing.Size(1873, 873);
            this.tabUnit.TabIndex = 23;
            this.tabUnit.Text = "Unit";
            this.tabUnit.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel44
            // 
            this.tableLayoutPanel44.ColumnCount = 2;
            this.tableLayoutPanel44.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 213F));
            this.tableLayoutPanel44.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel44.Controls.Add(this.FilterUnit, 0, 2);
            this.tableLayoutPanel44.Controls.Add(this.TextFilterUnit, 0, 1);
            this.tableLayoutPanel44.Controls.Add(this.grpUnit, 1, 0);
            this.tableLayoutPanel44.Controls.Add(this.lstUnit, 0, 0);
            this.tableLayoutPanel44.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel44.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel44.Margin = new System.Windows.Forms.Padding(2);
            this.tableLayoutPanel44.Name = "tableLayoutPanel44";
            this.tableLayoutPanel44.RowCount = 3;
            this.tableLayoutPanel44.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel44.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel44.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanel44.Size = new System.Drawing.Size(1873, 891);
            this.tableLayoutPanel44.TabIndex = 8;
            // 
            // FilterUnit
            // 
            this.FilterUnit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FilterUnit.Location = new System.Drawing.Point(3, 866);
            this.FilterUnit.Name = "FilterUnit";
            this.FilterUnit.Size = new System.Drawing.Size(207, 22);
            this.FilterUnit.TabIndex = 6;
            this.FilterUnit.Tag = "";
            this.FilterUnit.Text = "Filter";
            this.FilterUnit.UseVisualStyleBackColor = true;
            this.FilterUnit.Click += new System.EventHandler(this.FilterButton_Click);
            // 
            // TextFilterUnit
            // 
            this.TextFilterUnit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TextFilterUnit.Location = new System.Drawing.Point(3, 842);
            this.TextFilterUnit.Name = "TextFilterUnit";
            this.TextFilterUnit.Size = new System.Drawing.Size(207, 20);
            this.TextFilterUnit.TabIndex = 5;
            this.TextFilterUnit.TextChanged += new System.EventHandler(this.TextFilter_Changed);
            // 
            // grpUnit
            // 
            this.grpUnit.Controls.Add(this.grpUnitReferences);
            this.grpUnit.Controls.Add(this.grpUnitOwnedBuildings);
            this.grpUnit.Controls.Add(this.grpUnitInventory);
            this.grpUnit.Controls.Add(this.grpUnitItems);
            this.grpUnit.Controls.Add(this.grpUnitHealth);
            this.grpUnit.Controls.Add(this.grpUnitRelations);
            this.grpUnit.Controls.Add(this.grpUnitFlags);
            this.grpUnit.Controls.Add(this.grpUnitLabors);
            this.grpUnit.Controls.Add(this.lblUnitOpponent);
            this.grpUnit.Controls.Add(this.lblUnitSquad);
            this.grpUnit.Controls.Add(this.lblUnitRace);
            this.grpUnit.Controls.Add(this.lblUnitHF);
            this.grpUnit.Controls.Add(this.lblUnitPop);
            this.grpUnit.Controls.Add(this.lblUnitCiv);
            this.grpUnit.Controls.Add(this.label272);
            this.grpUnit.Controls.Add(this.label274);
            this.grpUnit.Controls.Add(this.lblUnitProfession);
            this.grpUnit.Controls.Add(this.label276);
            this.grpUnit.Controls.Add(this.lblUnitCaste);
            this.grpUnit.Controls.Add(this.label278);
            this.grpUnit.Controls.Add(this.label280);
            this.grpUnit.Controls.Add(this.label282);
            this.grpUnit.Controls.Add(this.lblUnitMood);
            this.grpUnit.Controls.Add(this.label284);
            this.grpUnit.Controls.Add(this.label286);
            this.grpUnit.Controls.Add(this.label288);
            this.grpUnit.Controls.Add(this.lblUnitSex);
            this.grpUnit.Controls.Add(this.label290);
            this.grpUnit.Controls.Add(this.lblUnitCoords);
            this.grpUnit.Controls.Add(this.label292);
            this.grpUnit.Controls.Add(this.lblUnitAltName);
            this.grpUnit.Controls.Add(this.label294);
            this.grpUnit.Controls.Add(this.lblUnitName);
            this.grpUnit.Controls.Add(this.label202);
            this.grpUnit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpUnit.Location = new System.Drawing.Point(216, 3);
            this.grpUnit.Name = "grpUnit";
            this.tableLayoutPanel44.SetRowSpan(this.grpUnit, 3);
            this.grpUnit.Size = new System.Drawing.Size(1654, 885);
            this.grpUnit.TabIndex = 3;
            this.grpUnit.TabStop = false;
            this.grpUnit.Visible = false;
            // 
            // grpUnitReferences
            // 
            this.grpUnitReferences.Controls.Add(this.lstUnitReferences);
            this.grpUnitReferences.Location = new System.Drawing.Point(6, 520);
            this.grpUnitReferences.Name = "grpUnitReferences";
            this.grpUnitReferences.Size = new System.Drawing.Size(391, 202);
            this.grpUnitReferences.TabIndex = 101;
            this.grpUnitReferences.TabStop = false;
            this.grpUnitReferences.Text = "References";
            // 
            // lstUnitReferences
            // 
            this.lstUnitReferences.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstUnitReferences.FormattingEnabled = true;
            this.lstUnitReferences.Location = new System.Drawing.Point(3, 16);
            this.lstUnitReferences.Name = "lstUnitReferences";
            this.lstUnitReferences.Size = new System.Drawing.Size(385, 183);
            this.lstUnitReferences.TabIndex = 0;
            // 
            // grpUnitOwnedBuildings
            // 
            this.grpUnitOwnedBuildings.Controls.Add(this.lstUnitOwnedBuildings);
            this.grpUnitOwnedBuildings.Location = new System.Drawing.Point(200, 325);
            this.grpUnitOwnedBuildings.Name = "grpUnitOwnedBuildings";
            this.grpUnitOwnedBuildings.Size = new System.Drawing.Size(200, 93);
            this.grpUnitOwnedBuildings.TabIndex = 81;
            this.grpUnitOwnedBuildings.TabStop = false;
            this.grpUnitOwnedBuildings.Text = "Owned Buildings";
            // 
            // lstUnitOwnedBuildings
            // 
            this.lstUnitOwnedBuildings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstUnitOwnedBuildings.FormattingEnabled = true;
            this.lstUnitOwnedBuildings.Location = new System.Drawing.Point(3, 16);
            this.lstUnitOwnedBuildings.Name = "lstUnitOwnedBuildings";
            this.lstUnitOwnedBuildings.Size = new System.Drawing.Size(194, 74);
            this.lstUnitOwnedBuildings.TabIndex = 0;
            // 
            // grpUnitInventory
            // 
            this.grpUnitInventory.Controls.Add(this.lstUnitInventory);
            this.grpUnitInventory.Location = new System.Drawing.Point(406, 325);
            this.grpUnitInventory.Name = "grpUnitInventory";
            this.grpUnitInventory.Size = new System.Drawing.Size(254, 269);
            this.grpUnitInventory.TabIndex = 80;
            this.grpUnitInventory.TabStop = false;
            this.grpUnitInventory.Text = "Inventory";
            // 
            // lstUnitInventory
            // 
            this.lstUnitInventory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstUnitInventory.FormattingEnabled = true;
            this.lstUnitInventory.Location = new System.Drawing.Point(3, 16);
            this.lstUnitInventory.Name = "lstUnitInventory";
            this.lstUnitInventory.Size = new System.Drawing.Size(248, 250);
            this.lstUnitInventory.TabIndex = 0;
            // 
            // grpUnitItems
            // 
            this.grpUnitItems.Controls.Add(this.trvUnitItems);
            this.grpUnitItems.Location = new System.Drawing.Point(666, 13);
            this.grpUnitItems.Name = "grpUnitItems";
            this.grpUnitItems.Size = new System.Drawing.Size(254, 309);
            this.grpUnitItems.TabIndex = 79;
            this.grpUnitItems.TabStop = false;
            this.grpUnitItems.Text = "Items";
            // 
            // trvUnitItems
            // 
            this.trvUnitItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trvUnitItems.Location = new System.Drawing.Point(3, 16);
            this.trvUnitItems.Name = "trvUnitItems";
            this.trvUnitItems.Size = new System.Drawing.Size(248, 290);
            this.trvUnitItems.TabIndex = 0;
            // 
            // grpUnitHealth
            // 
            this.grpUnitHealth.Controls.Add(this.lstUnitHealth);
            this.grpUnitHealth.Location = new System.Drawing.Point(200, 421);
            this.grpUnitHealth.Name = "grpUnitHealth";
            this.grpUnitHealth.Size = new System.Drawing.Size(200, 93);
            this.grpUnitHealth.TabIndex = 78;
            this.grpUnitHealth.TabStop = false;
            this.grpUnitHealth.Text = "Health";
            // 
            // lstUnitHealth
            // 
            this.lstUnitHealth.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstUnitHealth.FormattingEnabled = true;
            this.lstUnitHealth.Location = new System.Drawing.Point(3, 16);
            this.lstUnitHealth.Name = "lstUnitHealth";
            this.lstUnitHealth.Size = new System.Drawing.Size(194, 74);
            this.lstUnitHealth.TabIndex = 0;
            // 
            // grpUnitRelations
            // 
            this.grpUnitRelations.Controls.Add(this.lstUnitRelations);
            this.grpUnitRelations.Location = new System.Drawing.Point(406, 13);
            this.grpUnitRelations.Name = "grpUnitRelations";
            this.grpUnitRelations.Size = new System.Drawing.Size(254, 309);
            this.grpUnitRelations.TabIndex = 77;
            this.grpUnitRelations.TabStop = false;
            this.grpUnitRelations.Text = "Relations";
            // 
            // lstUnitRelations
            // 
            this.lstUnitRelations.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstUnitRelations.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.lstUnitRelations.FormattingEnabled = true;
            this.lstUnitRelations.Location = new System.Drawing.Point(3, 16);
            this.lstUnitRelations.Name = "lstUnitRelations";
            this.lstUnitRelations.Size = new System.Drawing.Size(248, 290);
            this.lstUnitRelations.TabIndex = 0;
            this.lstUnitRelations.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.lstUnitRelations_DrawItem);
            // 
            // grpUnitFlags
            // 
            this.grpUnitFlags.Controls.Add(this.lstUnitFlags);
            this.grpUnitFlags.Location = new System.Drawing.Point(200, 169);
            this.grpUnitFlags.Name = "grpUnitFlags";
            this.grpUnitFlags.Size = new System.Drawing.Size(200, 153);
            this.grpUnitFlags.TabIndex = 76;
            this.grpUnitFlags.TabStop = false;
            this.grpUnitFlags.Text = "Flags";
            // 
            // lstUnitFlags
            // 
            this.lstUnitFlags.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstUnitFlags.FormattingEnabled = true;
            this.lstUnitFlags.Location = new System.Drawing.Point(3, 16);
            this.lstUnitFlags.Name = "lstUnitFlags";
            this.lstUnitFlags.Size = new System.Drawing.Size(194, 134);
            this.lstUnitFlags.TabIndex = 0;
            // 
            // grpUnitLabors
            // 
            this.grpUnitLabors.Controls.Add(this.lstUnitLabors);
            this.grpUnitLabors.Location = new System.Drawing.Point(200, 13);
            this.grpUnitLabors.Name = "grpUnitLabors";
            this.grpUnitLabors.Size = new System.Drawing.Size(200, 153);
            this.grpUnitLabors.TabIndex = 75;
            this.grpUnitLabors.TabStop = false;
            this.grpUnitLabors.Text = "Labors";
            // 
            // lstUnitLabors
            // 
            this.lstUnitLabors.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstUnitLabors.FormattingEnabled = true;
            this.lstUnitLabors.Location = new System.Drawing.Point(3, 16);
            this.lstUnitLabors.Name = "lstUnitLabors";
            this.lstUnitLabors.Size = new System.Drawing.Size(194, 134);
            this.lstUnitLabors.TabIndex = 0;
            // 
            // label272
            // 
            this.label272.AutoSize = true;
            this.label272.Location = new System.Drawing.Point(12, 287);
            this.label272.Name = "label272";
            this.label272.Size = new System.Drawing.Size(57, 13);
            this.label272.TabIndex = 67;
            this.label272.Text = "Opponent:";
            // 
            // label274
            // 
            this.label274.AutoSize = true;
            this.label274.Location = new System.Drawing.Point(12, 265);
            this.label274.Name = "label274";
            this.label274.Size = new System.Drawing.Size(41, 13);
            this.label274.TabIndex = 65;
            this.label274.Text = "Squad:";
            // 
            // lblUnitProfession
            // 
            this.lblUnitProfession.AutoSize = true;
            this.lblUnitProfession.Location = new System.Drawing.Point(67, 243);
            this.lblUnitProfession.Name = "lblUnitProfession";
            this.lblUnitProfession.Size = new System.Drawing.Size(35, 13);
            this.lblUnitProfession.TabIndex = 64;
            this.lblUnitProfession.Text = "label2";
            // 
            // label276
            // 
            this.label276.AutoSize = true;
            this.label276.Location = new System.Drawing.Point(12, 243);
            this.label276.Name = "label276";
            this.label276.Size = new System.Drawing.Size(59, 13);
            this.label276.TabIndex = 63;
            this.label276.Text = "Profession:";
            // 
            // lblUnitCaste
            // 
            this.lblUnitCaste.AutoSize = true;
            this.lblUnitCaste.Location = new System.Drawing.Point(67, 221);
            this.lblUnitCaste.Name = "lblUnitCaste";
            this.lblUnitCaste.Size = new System.Drawing.Size(35, 13);
            this.lblUnitCaste.TabIndex = 62;
            this.lblUnitCaste.Text = "label2";
            // 
            // label278
            // 
            this.label278.AutoSize = true;
            this.label278.Location = new System.Drawing.Point(12, 221);
            this.label278.Name = "label278";
            this.label278.Size = new System.Drawing.Size(37, 13);
            this.label278.TabIndex = 61;
            this.label278.Text = "Caste:";
            // 
            // label280
            // 
            this.label280.AutoSize = true;
            this.label280.Location = new System.Drawing.Point(12, 197);
            this.label280.Name = "label280";
            this.label280.Size = new System.Drawing.Size(36, 13);
            this.label280.TabIndex = 59;
            this.label280.Text = "Race:";
            // 
            // label282
            // 
            this.label282.AutoSize = true;
            this.label282.Location = new System.Drawing.Point(12, 175);
            this.label282.Name = "label282";
            this.label282.Size = new System.Drawing.Size(24, 13);
            this.label282.TabIndex = 57;
            this.label282.Text = "HF:";
            // 
            // lblUnitMood
            // 
            this.lblUnitMood.AutoSize = true;
            this.lblUnitMood.Location = new System.Drawing.Point(67, 153);
            this.lblUnitMood.Name = "lblUnitMood";
            this.lblUnitMood.Size = new System.Drawing.Size(35, 13);
            this.lblUnitMood.TabIndex = 56;
            this.lblUnitMood.Text = "label2";
            // 
            // label284
            // 
            this.label284.AutoSize = true;
            this.label284.Location = new System.Drawing.Point(12, 153);
            this.label284.Name = "label284";
            this.label284.Size = new System.Drawing.Size(37, 13);
            this.label284.TabIndex = 55;
            this.label284.Text = "Mood:";
            // 
            // label286
            // 
            this.label286.AutoSize = true;
            this.label286.Location = new System.Drawing.Point(12, 131);
            this.label286.Name = "label286";
            this.label286.Size = new System.Drawing.Size(29, 13);
            this.label286.TabIndex = 53;
            this.label286.Text = "Pop:";
            // 
            // label288
            // 
            this.label288.AutoSize = true;
            this.label288.Location = new System.Drawing.Point(12, 109);
            this.label288.Name = "label288";
            this.label288.Size = new System.Drawing.Size(25, 13);
            this.label288.TabIndex = 51;
            this.label288.Text = "Civ:";
            // 
            // lblUnitSex
            // 
            this.lblUnitSex.AutoSize = true;
            this.lblUnitSex.Location = new System.Drawing.Point(67, 87);
            this.lblUnitSex.Name = "lblUnitSex";
            this.lblUnitSex.Size = new System.Drawing.Size(35, 13);
            this.lblUnitSex.TabIndex = 50;
            this.lblUnitSex.Text = "label2";
            // 
            // label290
            // 
            this.label290.AutoSize = true;
            this.label290.Location = new System.Drawing.Point(12, 87);
            this.label290.Name = "label290";
            this.label290.Size = new System.Drawing.Size(28, 13);
            this.label290.TabIndex = 49;
            this.label290.Text = "Sex:";
            // 
            // lblUnitCoords
            // 
            this.lblUnitCoords.AutoSize = true;
            this.lblUnitCoords.Location = new System.Drawing.Point(67, 65);
            this.lblUnitCoords.Name = "lblUnitCoords";
            this.lblUnitCoords.Size = new System.Drawing.Size(35, 13);
            this.lblUnitCoords.TabIndex = 48;
            this.lblUnitCoords.Text = "label2";
            // 
            // label292
            // 
            this.label292.AutoSize = true;
            this.label292.Location = new System.Drawing.Point(12, 65);
            this.label292.Name = "label292";
            this.label292.Size = new System.Drawing.Size(43, 13);
            this.label292.TabIndex = 47;
            this.label292.Text = "Coords:";
            // 
            // lblUnitAltName
            // 
            this.lblUnitAltName.AutoSize = true;
            this.lblUnitAltName.Location = new System.Drawing.Point(67, 43);
            this.lblUnitAltName.Name = "lblUnitAltName";
            this.lblUnitAltName.Size = new System.Drawing.Size(35, 13);
            this.lblUnitAltName.TabIndex = 46;
            this.lblUnitAltName.Text = "label2";
            // 
            // label294
            // 
            this.label294.AutoSize = true;
            this.label294.Location = new System.Drawing.Point(12, 43);
            this.label294.Name = "label294";
            this.label294.Size = new System.Drawing.Size(53, 13);
            this.label294.TabIndex = 45;
            this.label294.Text = "Alt Name:";
            // 
            // lblUnitName
            // 
            this.lblUnitName.AutoSize = true;
            this.lblUnitName.Location = new System.Drawing.Point(67, 21);
            this.lblUnitName.Name = "lblUnitName";
            this.lblUnitName.Size = new System.Drawing.Size(35, 13);
            this.lblUnitName.TabIndex = 20;
            this.lblUnitName.Text = "label2";
            // 
            // label202
            // 
            this.label202.AutoSize = true;
            this.label202.Location = new System.Drawing.Point(12, 21);
            this.label202.Name = "label202";
            this.label202.Size = new System.Drawing.Size(41, 13);
            this.label202.TabIndex = 19;
            this.label202.Text = "Name: ";
            // 
            // lstUnit
            // 
            this.lstUnit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstUnit.FormattingEnabled = true;
            this.lstUnit.Location = new System.Drawing.Point(3, 3);
            this.lstUnit.Name = "lstUnit";
            this.lstUnit.Size = new System.Drawing.Size(207, 833);
            this.lstUnit.TabIndex = 2;
            // 
            // tabEngraving
            // 
            this.tabEngraving.Controls.Add(this.tableLayoutPanel46);
            this.tabEngraving.Location = new System.Drawing.Point(4, 40);
            this.tabEngraving.Name = "tabEngraving";
            this.tabEngraving.Size = new System.Drawing.Size(1873, 873);
            this.tabEngraving.TabIndex = 25;
            this.tabEngraving.Text = "Engraving";
            this.tabEngraving.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel46
            // 
            this.tableLayoutPanel46.ColumnCount = 2;
            this.tableLayoutPanel46.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 213F));
            this.tableLayoutPanel46.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel46.Controls.Add(this.FilterEngraving, 0, 2);
            this.tableLayoutPanel46.Controls.Add(this.TextFilterEngraving, 0, 1);
            this.tableLayoutPanel46.Controls.Add(this.grpEngraving, 1, 0);
            this.tableLayoutPanel46.Controls.Add(this.lstEngraving, 0, 0);
            this.tableLayoutPanel46.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel46.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel46.Margin = new System.Windows.Forms.Padding(2);
            this.tableLayoutPanel46.Name = "tableLayoutPanel46";
            this.tableLayoutPanel46.RowCount = 3;
            this.tableLayoutPanel46.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel46.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel46.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanel46.Size = new System.Drawing.Size(1873, 891);
            this.tableLayoutPanel46.TabIndex = 8;
            // 
            // FilterEngraving
            // 
            this.FilterEngraving.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FilterEngraving.Location = new System.Drawing.Point(3, 866);
            this.FilterEngraving.Name = "FilterEngraving";
            this.FilterEngraving.Size = new System.Drawing.Size(207, 22);
            this.FilterEngraving.TabIndex = 6;
            this.FilterEngraving.Tag = "";
            this.FilterEngraving.Text = "Filter";
            this.FilterEngraving.UseVisualStyleBackColor = true;
            this.FilterEngraving.Click += new System.EventHandler(this.FilterButton_Click);
            // 
            // TextFilterEngraving
            // 
            this.TextFilterEngraving.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TextFilterEngraving.Location = new System.Drawing.Point(3, 842);
            this.TextFilterEngraving.Name = "TextFilterEngraving";
            this.TextFilterEngraving.Size = new System.Drawing.Size(207, 20);
            this.TextFilterEngraving.TabIndex = 5;
            this.TextFilterEngraving.TextChanged += new System.EventHandler(this.TextFilter_Changed);
            // 
            // grpEngraving
            // 
            this.grpEngraving.Controls.Add(this.lblEngravingCreatedEvent);
            this.grpEngraving.Controls.Add(this.label204);
            this.grpEngraving.Controls.Add(this.lblEngravingHidden);
            this.grpEngraving.Controls.Add(this.lblEngravingLocation);
            this.grpEngraving.Controls.Add(this.lblEngravingArtSubID);
            this.grpEngraving.Controls.Add(this.lblEngravingArtID);
            this.grpEngraving.Controls.Add(this.lblEngravingArtist);
            this.grpEngraving.Controls.Add(this.label257);
            this.grpEngraving.Controls.Add(this.label258);
            this.grpEngraving.Controls.Add(this.lblEngravingQuality);
            this.grpEngraving.Controls.Add(this.label260);
            this.grpEngraving.Controls.Add(this.label261);
            this.grpEngraving.Controls.Add(this.label263);
            this.grpEngraving.Controls.Add(this.lblEngravingTile);
            this.grpEngraving.Controls.Add(this.label265);
            this.grpEngraving.Controls.Add(this.lblEngravingCoords);
            this.grpEngraving.Controls.Add(this.label267);
            this.grpEngraving.Controls.Add(this.lblEngravingSkill);
            this.grpEngraving.Controls.Add(this.label269);
            this.grpEngraving.Controls.Add(this.label271);
            this.grpEngraving.Controls.Add(this.lblEngravingName);
            this.grpEngraving.Controls.Add(this.label209);
            this.grpEngraving.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpEngraving.Location = new System.Drawing.Point(216, 3);
            this.grpEngraving.Name = "grpEngraving";
            this.tableLayoutPanel46.SetRowSpan(this.grpEngraving, 3);
            this.grpEngraving.Size = new System.Drawing.Size(1654, 885);
            this.grpEngraving.TabIndex = 3;
            this.grpEngraving.TabStop = false;
            this.grpEngraving.Visible = false;
            // 
            // label204
            // 
            this.label204.AutoSize = true;
            this.label204.Location = new System.Drawing.Point(12, 241);
            this.label204.Name = "label204";
            this.label204.Size = new System.Drawing.Size(47, 13);
            this.label204.TabIndex = 106;
            this.label204.Text = "Created:";
            // 
            // lblEngravingHidden
            // 
            this.lblEngravingHidden.AutoSize = true;
            this.lblEngravingHidden.Location = new System.Drawing.Point(67, 219);
            this.lblEngravingHidden.Name = "lblEngravingHidden";
            this.lblEngravingHidden.Size = new System.Drawing.Size(35, 13);
            this.lblEngravingHidden.TabIndex = 105;
            this.lblEngravingHidden.Text = "label2";
            // 
            // lblEngravingLocation
            // 
            this.lblEngravingLocation.AutoSize = true;
            this.lblEngravingLocation.Location = new System.Drawing.Point(67, 197);
            this.lblEngravingLocation.Name = "lblEngravingLocation";
            this.lblEngravingLocation.Size = new System.Drawing.Size(35, 13);
            this.lblEngravingLocation.TabIndex = 104;
            this.lblEngravingLocation.Text = "label2";
            // 
            // lblEngravingArtSubID
            // 
            this.lblEngravingArtSubID.AutoSize = true;
            this.lblEngravingArtSubID.Location = new System.Drawing.Point(67, 153);
            this.lblEngravingArtSubID.Name = "lblEngravingArtSubID";
            this.lblEngravingArtSubID.Size = new System.Drawing.Size(35, 13);
            this.lblEngravingArtSubID.TabIndex = 103;
            this.lblEngravingArtSubID.Text = "label2";
            // 
            // lblEngravingArtID
            // 
            this.lblEngravingArtID.AutoSize = true;
            this.lblEngravingArtID.Location = new System.Drawing.Point(67, 131);
            this.lblEngravingArtID.Name = "lblEngravingArtID";
            this.lblEngravingArtID.Size = new System.Drawing.Size(35, 13);
            this.lblEngravingArtID.TabIndex = 102;
            this.lblEngravingArtID.Text = "label2";
            // 
            // label257
            // 
            this.label257.AutoSize = true;
            this.label257.Location = new System.Drawing.Point(12, 219);
            this.label257.Name = "label257";
            this.label257.Size = new System.Drawing.Size(44, 13);
            this.label257.TabIndex = 88;
            this.label257.Text = "Hidden:";
            // 
            // label258
            // 
            this.label258.AutoSize = true;
            this.label258.Location = new System.Drawing.Point(12, 197);
            this.label258.Name = "label258";
            this.label258.Size = new System.Drawing.Size(51, 13);
            this.label258.TabIndex = 87;
            this.label258.Text = "Location:";
            // 
            // lblEngravingQuality
            // 
            this.lblEngravingQuality.AutoSize = true;
            this.lblEngravingQuality.Location = new System.Drawing.Point(67, 175);
            this.lblEngravingQuality.Name = "lblEngravingQuality";
            this.lblEngravingQuality.Size = new System.Drawing.Size(35, 13);
            this.lblEngravingQuality.TabIndex = 86;
            this.lblEngravingQuality.Text = "label2";
            // 
            // label260
            // 
            this.label260.AutoSize = true;
            this.label260.Location = new System.Drawing.Point(12, 175);
            this.label260.Name = "label260";
            this.label260.Size = new System.Drawing.Size(42, 13);
            this.label260.TabIndex = 85;
            this.label260.Text = "Quality:";
            // 
            // label261
            // 
            this.label261.AutoSize = true;
            this.label261.Location = new System.Drawing.Point(12, 153);
            this.label261.Name = "label261";
            this.label261.Size = new System.Drawing.Size(56, 13);
            this.label261.TabIndex = 84;
            this.label261.Text = "Art SubID:";
            // 
            // label263
            // 
            this.label263.AutoSize = true;
            this.label263.Location = new System.Drawing.Point(12, 131);
            this.label263.Name = "label263";
            this.label263.Size = new System.Drawing.Size(37, 13);
            this.label263.TabIndex = 83;
            this.label263.Text = "Art ID:";
            // 
            // lblEngravingTile
            // 
            this.lblEngravingTile.AutoSize = true;
            this.lblEngravingTile.Location = new System.Drawing.Point(67, 109);
            this.lblEngravingTile.Name = "lblEngravingTile";
            this.lblEngravingTile.Size = new System.Drawing.Size(35, 13);
            this.lblEngravingTile.TabIndex = 82;
            this.lblEngravingTile.Text = "label2";
            // 
            // label265
            // 
            this.label265.AutoSize = true;
            this.label265.Location = new System.Drawing.Point(12, 109);
            this.label265.Name = "label265";
            this.label265.Size = new System.Drawing.Size(30, 13);
            this.label265.TabIndex = 81;
            this.label265.Text = "Title:";
            // 
            // lblEngravingCoords
            // 
            this.lblEngravingCoords.AutoSize = true;
            this.lblEngravingCoords.Location = new System.Drawing.Point(67, 87);
            this.lblEngravingCoords.Name = "lblEngravingCoords";
            this.lblEngravingCoords.Size = new System.Drawing.Size(35, 13);
            this.lblEngravingCoords.TabIndex = 80;
            this.lblEngravingCoords.Text = "label2";
            // 
            // label267
            // 
            this.label267.AutoSize = true;
            this.label267.Location = new System.Drawing.Point(12, 87);
            this.label267.Name = "label267";
            this.label267.Size = new System.Drawing.Size(43, 13);
            this.label267.TabIndex = 79;
            this.label267.Text = "Coords:";
            // 
            // lblEngravingSkill
            // 
            this.lblEngravingSkill.AutoSize = true;
            this.lblEngravingSkill.Location = new System.Drawing.Point(67, 65);
            this.lblEngravingSkill.Name = "lblEngravingSkill";
            this.lblEngravingSkill.Size = new System.Drawing.Size(35, 13);
            this.lblEngravingSkill.TabIndex = 78;
            this.lblEngravingSkill.Text = "label2";
            // 
            // label269
            // 
            this.label269.AutoSize = true;
            this.label269.Location = new System.Drawing.Point(12, 65);
            this.label269.Name = "label269";
            this.label269.Size = new System.Drawing.Size(29, 13);
            this.label269.TabIndex = 77;
            this.label269.Text = "Skill:";
            // 
            // label271
            // 
            this.label271.AutoSize = true;
            this.label271.Location = new System.Drawing.Point(12, 43);
            this.label271.Name = "label271";
            this.label271.Size = new System.Drawing.Size(33, 13);
            this.label271.TabIndex = 75;
            this.label271.Text = "Artist:";
            // 
            // lblEngravingName
            // 
            this.lblEngravingName.AutoSize = true;
            this.lblEngravingName.Location = new System.Drawing.Point(67, 21);
            this.lblEngravingName.Name = "lblEngravingName";
            this.lblEngravingName.Size = new System.Drawing.Size(35, 13);
            this.lblEngravingName.TabIndex = 20;
            this.lblEngravingName.Text = "label2";
            // 
            // label209
            // 
            this.label209.AutoSize = true;
            this.label209.Location = new System.Drawing.Point(12, 21);
            this.label209.Name = "label209";
            this.label209.Size = new System.Drawing.Size(41, 13);
            this.label209.TabIndex = 19;
            this.label209.Text = "Name: ";
            // 
            // lstEngraving
            // 
            this.lstEngraving.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstEngraving.FormattingEnabled = true;
            this.lstEngraving.Location = new System.Drawing.Point(3, 3);
            this.lstEngraving.Name = "lstEngraving";
            this.lstEngraving.Size = new System.Drawing.Size(207, 833);
            this.lstEngraving.TabIndex = 2;
            // 
            // tabReport
            // 
            this.tabReport.Controls.Add(this.tableLayoutPanel50);
            this.tabReport.Location = new System.Drawing.Point(4, 40);
            this.tabReport.Name = "tabReport";
            this.tabReport.Size = new System.Drawing.Size(1873, 873);
            this.tabReport.TabIndex = 29;
            this.tabReport.Text = "Report";
            this.tabReport.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel50
            // 
            this.tableLayoutPanel50.ColumnCount = 2;
            this.tableLayoutPanel50.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 689F));
            this.tableLayoutPanel50.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel50.Controls.Add(this.FilterReport, 0, 2);
            this.tableLayoutPanel50.Controls.Add(this.TextFilterReport, 0, 1);
            this.tableLayoutPanel50.Controls.Add(this.grpReport, 1, 0);
            this.tableLayoutPanel50.Controls.Add(this.lstReport, 0, 0);
            this.tableLayoutPanel50.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel50.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel50.Margin = new System.Windows.Forms.Padding(2);
            this.tableLayoutPanel50.Name = "tableLayoutPanel50";
            this.tableLayoutPanel50.RowCount = 3;
            this.tableLayoutPanel50.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel50.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel50.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanel50.Size = new System.Drawing.Size(1873, 891);
            this.tableLayoutPanel50.TabIndex = 8;
            // 
            // FilterReport
            // 
            this.FilterReport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FilterReport.Location = new System.Drawing.Point(3, 866);
            this.FilterReport.Name = "FilterReport";
            this.FilterReport.Size = new System.Drawing.Size(683, 22);
            this.FilterReport.TabIndex = 6;
            this.FilterReport.Tag = "";
            this.FilterReport.Text = "Filter";
            this.FilterReport.UseVisualStyleBackColor = true;
            this.FilterReport.Click += new System.EventHandler(this.FilterButton_Click);
            // 
            // TextFilterReport
            // 
            this.TextFilterReport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TextFilterReport.Location = new System.Drawing.Point(3, 842);
            this.TextFilterReport.Name = "TextFilterReport";
            this.TextFilterReport.Size = new System.Drawing.Size(683, 20);
            this.TextFilterReport.TabIndex = 5;
            this.TextFilterReport.TextChanged += new System.EventHandler(this.TextFilter_Changed);
            // 
            // grpReport
            // 
            this.grpReport.Controls.Add(this.lblReportContinuation);
            this.grpReport.Controls.Add(this.lblReportAnnouncement);
            this.grpReport.Controls.Add(this.lblReportTime);
            this.grpReport.Controls.Add(this.label214);
            this.grpReport.Controls.Add(this.lblReportType);
            this.grpReport.Controls.Add(this.label210);
            this.grpReport.Controls.Add(this.lblReportText);
            this.grpReport.Controls.Add(this.label217);
            this.grpReport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpReport.Location = new System.Drawing.Point(692, 3);
            this.grpReport.Name = "grpReport";
            this.tableLayoutPanel50.SetRowSpan(this.grpReport, 3);
            this.grpReport.Size = new System.Drawing.Size(1178, 885);
            this.grpReport.TabIndex = 3;
            this.grpReport.TabStop = false;
            this.grpReport.Visible = false;
            // 
            // lblReportContinuation
            // 
            this.lblReportContinuation.AutoSize = true;
            this.lblReportContinuation.Location = new System.Drawing.Point(12, 109);
            this.lblReportContinuation.Name = "lblReportContinuation";
            this.lblReportContinuation.Size = new System.Drawing.Size(66, 13);
            this.lblReportContinuation.TabIndex = 27;
            this.lblReportContinuation.Text = "Continuation";
            // 
            // lblReportAnnouncement
            // 
            this.lblReportAnnouncement.AutoSize = true;
            this.lblReportAnnouncement.Location = new System.Drawing.Point(12, 87);
            this.lblReportAnnouncement.Name = "lblReportAnnouncement";
            this.lblReportAnnouncement.Size = new System.Drawing.Size(79, 13);
            this.lblReportAnnouncement.TabIndex = 25;
            this.lblReportAnnouncement.Text = "Announcement";
            // 
            // lblReportTime
            // 
            this.lblReportTime.AutoSize = true;
            this.lblReportTime.Location = new System.Drawing.Point(67, 65);
            this.lblReportTime.Name = "lblReportTime";
            this.lblReportTime.Size = new System.Drawing.Size(35, 13);
            this.lblReportTime.TabIndex = 24;
            this.lblReportTime.Text = "label2";
            // 
            // label214
            // 
            this.label214.AutoSize = true;
            this.label214.Location = new System.Drawing.Point(12, 65);
            this.label214.Name = "label214";
            this.label214.Size = new System.Drawing.Size(33, 13);
            this.label214.TabIndex = 23;
            this.label214.Text = "Time:";
            // 
            // lblReportType
            // 
            this.lblReportType.AutoSize = true;
            this.lblReportType.Location = new System.Drawing.Point(67, 43);
            this.lblReportType.Name = "lblReportType";
            this.lblReportType.Size = new System.Drawing.Size(35, 13);
            this.lblReportType.TabIndex = 22;
            this.lblReportType.Text = "label2";
            // 
            // label210
            // 
            this.label210.AutoSize = true;
            this.label210.Location = new System.Drawing.Point(12, 43);
            this.label210.Name = "label210";
            this.label210.Size = new System.Drawing.Size(34, 13);
            this.label210.TabIndex = 21;
            this.label210.Text = "Type:";
            // 
            // lblReportText
            // 
            this.lblReportText.AutoSize = true;
            this.lblReportText.Location = new System.Drawing.Point(67, 21);
            this.lblReportText.Name = "lblReportText";
            this.lblReportText.Size = new System.Drawing.Size(35, 13);
            this.lblReportText.TabIndex = 20;
            this.lblReportText.Text = "label2";
            // 
            // label217
            // 
            this.label217.AutoSize = true;
            this.label217.Location = new System.Drawing.Point(12, 21);
            this.label217.Name = "label217";
            this.label217.Size = new System.Drawing.Size(31, 13);
            this.label217.TabIndex = 19;
            this.label217.Text = "Text:";
            // 
            // lstReport
            // 
            this.lstReport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstReport.FormattingEnabled = true;
            this.lstReport.Location = new System.Drawing.Point(3, 3);
            this.lstReport.Name = "lstReport";
            this.lstReport.Size = new System.Drawing.Size(683, 833);
            this.lstReport.TabIndex = 2;
            // 
            // tabWrittenContent
            // 
            this.tabWrittenContent.Controls.Add(this.tableLayoutPanel51);
            this.tabWrittenContent.Location = new System.Drawing.Point(4, 40);
            this.tabWrittenContent.Name = "tabWrittenContent";
            this.tabWrittenContent.Size = new System.Drawing.Size(1873, 873);
            this.tabWrittenContent.TabIndex = 30;
            this.tabWrittenContent.Text = "Writen Content";
            this.tabWrittenContent.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel51
            // 
            this.tableLayoutPanel51.ColumnCount = 2;
            this.tableLayoutPanel51.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 213F));
            this.tableLayoutPanel51.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel51.Controls.Add(this.FilterWrittenContent, 0, 2);
            this.tableLayoutPanel51.Controls.Add(this.TextFilterWrittenContent, 0, 1);
            this.tableLayoutPanel51.Controls.Add(this.grpWrittenContent, 1, 0);
            this.tableLayoutPanel51.Controls.Add(this.lstWrittenContent, 0, 0);
            this.tableLayoutPanel51.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel51.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel51.Margin = new System.Windows.Forms.Padding(2);
            this.tableLayoutPanel51.Name = "tableLayoutPanel51";
            this.tableLayoutPanel51.RowCount = 3;
            this.tableLayoutPanel51.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel51.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel51.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanel51.Size = new System.Drawing.Size(1873, 891);
            this.tableLayoutPanel51.TabIndex = 8;
            // 
            // FilterWrittenContent
            // 
            this.FilterWrittenContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FilterWrittenContent.Location = new System.Drawing.Point(3, 866);
            this.FilterWrittenContent.Name = "FilterWrittenContent";
            this.FilterWrittenContent.Size = new System.Drawing.Size(207, 22);
            this.FilterWrittenContent.TabIndex = 6;
            this.FilterWrittenContent.Tag = "";
            this.FilterWrittenContent.Text = "Filter";
            this.FilterWrittenContent.UseVisualStyleBackColor = true;
            this.FilterWrittenContent.Click += new System.EventHandler(this.FilterButton_Click);
            // 
            // TextFilterWrittenContent
            // 
            this.TextFilterWrittenContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TextFilterWrittenContent.Location = new System.Drawing.Point(3, 842);
            this.TextFilterWrittenContent.Name = "TextFilterWrittenContent";
            this.TextFilterWrittenContent.Size = new System.Drawing.Size(207, 20);
            this.TextFilterWrittenContent.TabIndex = 5;
            this.TextFilterWrittenContent.TextChanged += new System.EventHandler(this.TextFilter_Changed);
            // 
            // grpWrittenContent
            // 
            this.grpWrittenContent.Controls.Add(this.grpWrittenContentReferences);
            this.grpWrittenContent.Controls.Add(this.lblWrittenContentStyle);
            this.grpWrittenContent.Controls.Add(this.label309);
            this.grpWrittenContent.Controls.Add(this.lblWrittenContentType);
            this.grpWrittenContent.Controls.Add(this.label273);
            this.grpWrittenContent.Controls.Add(this.lblWrittenContentAuthor);
            this.grpWrittenContent.Controls.Add(this.lblWrittenContentPages);
            this.grpWrittenContent.Controls.Add(this.label312);
            this.grpWrittenContent.Controls.Add(this.label314);
            this.grpWrittenContent.Controls.Add(this.lblWrittenContentTitle);
            this.grpWrittenContent.Controls.Add(this.label219);
            this.grpWrittenContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpWrittenContent.Location = new System.Drawing.Point(216, 3);
            this.grpWrittenContent.Name = "grpWrittenContent";
            this.tableLayoutPanel51.SetRowSpan(this.grpWrittenContent, 3);
            this.grpWrittenContent.Size = new System.Drawing.Size(1654, 885);
            this.grpWrittenContent.TabIndex = 3;
            this.grpWrittenContent.TabStop = false;
            this.grpWrittenContent.Visible = false;
            // 
            // grpWrittenContentReferences
            // 
            this.grpWrittenContentReferences.Controls.Add(this.lstWrittenContentReferences);
            this.grpWrittenContentReferences.Location = new System.Drawing.Point(15, 137);
            this.grpWrittenContentReferences.Name = "grpWrittenContentReferences";
            this.grpWrittenContentReferences.Size = new System.Drawing.Size(422, 173);
            this.grpWrittenContentReferences.TabIndex = 100;
            this.grpWrittenContentReferences.TabStop = false;
            this.grpWrittenContentReferences.Text = "References";
            // 
            // lstWrittenContentReferences
            // 
            this.lstWrittenContentReferences.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstWrittenContentReferences.FormattingEnabled = true;
            this.lstWrittenContentReferences.Location = new System.Drawing.Point(3, 16);
            this.lstWrittenContentReferences.Name = "lstWrittenContentReferences";
            this.lstWrittenContentReferences.Size = new System.Drawing.Size(416, 154);
            this.lstWrittenContentReferences.TabIndex = 0;
            // 
            // lblWrittenContentStyle
            // 
            this.lblWrittenContentStyle.AutoSize = true;
            this.lblWrittenContentStyle.Location = new System.Drawing.Point(81, 87);
            this.lblWrittenContentStyle.Name = "lblWrittenContentStyle";
            this.lblWrittenContentStyle.Size = new System.Drawing.Size(35, 13);
            this.lblWrittenContentStyle.TabIndex = 99;
            this.lblWrittenContentStyle.Text = "label2";
            // 
            // label309
            // 
            this.label309.AutoSize = true;
            this.label309.Location = new System.Drawing.Point(12, 87);
            this.label309.Name = "label309";
            this.label309.Size = new System.Drawing.Size(33, 13);
            this.label309.TabIndex = 98;
            this.label309.Text = "Style:";
            // 
            // lblWrittenContentType
            // 
            this.lblWrittenContentType.AutoSize = true;
            this.lblWrittenContentType.Location = new System.Drawing.Point(81, 65);
            this.lblWrittenContentType.Name = "lblWrittenContentType";
            this.lblWrittenContentType.Size = new System.Drawing.Size(35, 13);
            this.lblWrittenContentType.TabIndex = 97;
            this.lblWrittenContentType.Text = "label2";
            // 
            // label273
            // 
            this.label273.AutoSize = true;
            this.label273.Location = new System.Drawing.Point(12, 65);
            this.label273.Name = "label273";
            this.label273.Size = new System.Drawing.Size(34, 13);
            this.label273.TabIndex = 96;
            this.label273.Text = "Type:";
            // 
            // lblWrittenContentPages
            // 
            this.lblWrittenContentPages.AutoSize = true;
            this.lblWrittenContentPages.Location = new System.Drawing.Point(81, 109);
            this.lblWrittenContentPages.Name = "lblWrittenContentPages";
            this.lblWrittenContentPages.Size = new System.Drawing.Size(35, 13);
            this.lblWrittenContentPages.TabIndex = 78;
            this.lblWrittenContentPages.Text = "label2";
            // 
            // label312
            // 
            this.label312.AutoSize = true;
            this.label312.Location = new System.Drawing.Point(12, 109);
            this.label312.Name = "label312";
            this.label312.Size = new System.Drawing.Size(37, 13);
            this.label312.TabIndex = 77;
            this.label312.Text = "Pages";
            // 
            // label314
            // 
            this.label314.AutoSize = true;
            this.label314.Location = new System.Drawing.Point(12, 43);
            this.label314.Name = "label314";
            this.label314.Size = new System.Drawing.Size(41, 13);
            this.label314.TabIndex = 75;
            this.label314.Text = "Author:";
            // 
            // lblWrittenContentTitle
            // 
            this.lblWrittenContentTitle.AutoSize = true;
            this.lblWrittenContentTitle.Location = new System.Drawing.Point(81, 21);
            this.lblWrittenContentTitle.Name = "lblWrittenContentTitle";
            this.lblWrittenContentTitle.Size = new System.Drawing.Size(35, 13);
            this.lblWrittenContentTitle.TabIndex = 20;
            this.lblWrittenContentTitle.Text = "label2";
            // 
            // label219
            // 
            this.label219.AutoSize = true;
            this.label219.Location = new System.Drawing.Point(12, 21);
            this.label219.Name = "label219";
            this.label219.Size = new System.Drawing.Size(30, 13);
            this.label219.TabIndex = 19;
            this.label219.Text = "Title:";
            // 
            // lstWrittenContent
            // 
            this.lstWrittenContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstWrittenContent.FormattingEnabled = true;
            this.lstWrittenContent.Location = new System.Drawing.Point(3, 3);
            this.lstWrittenContent.Name = "lstWrittenContent";
            this.lstWrittenContent.Size = new System.Drawing.Size(207, 833);
            this.lstWrittenContent.TabIndex = 2;
            // 
            // tabPoeticForm
            // 
            this.tabPoeticForm.Controls.Add(this.tableLayoutPanel62);
            this.tabPoeticForm.Location = new System.Drawing.Point(4, 40);
            this.tabPoeticForm.Name = "tabPoeticForm";
            this.tabPoeticForm.Size = new System.Drawing.Size(1873, 873);
            this.tabPoeticForm.TabIndex = 36;
            this.tabPoeticForm.Text = "Poetic Form";
            this.tabPoeticForm.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel62
            // 
            this.tableLayoutPanel62.ColumnCount = 2;
            this.tableLayoutPanel62.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 213F));
            this.tableLayoutPanel62.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel62.Controls.Add(this.FilterPoeticForm, 0, 2);
            this.tableLayoutPanel62.Controls.Add(this.TextFilterPoeticForm, 0, 1);
            this.tableLayoutPanel62.Controls.Add(this.grpPoeticForm, 1, 0);
            this.tableLayoutPanel62.Controls.Add(this.lstPoeticForm, 0, 0);
            this.tableLayoutPanel62.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel62.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel62.Margin = new System.Windows.Forms.Padding(2);
            this.tableLayoutPanel62.Name = "tableLayoutPanel62";
            this.tableLayoutPanel62.RowCount = 3;
            this.tableLayoutPanel62.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel62.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel62.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanel62.Size = new System.Drawing.Size(1873, 891);
            this.tableLayoutPanel62.TabIndex = 8;
            // 
            // FilterPoeticForm
            // 
            this.FilterPoeticForm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FilterPoeticForm.Location = new System.Drawing.Point(3, 866);
            this.FilterPoeticForm.Name = "FilterPoeticForm";
            this.FilterPoeticForm.Size = new System.Drawing.Size(207, 22);
            this.FilterPoeticForm.TabIndex = 6;
            this.FilterPoeticForm.Tag = "";
            this.FilterPoeticForm.Text = "Filter";
            this.FilterPoeticForm.UseVisualStyleBackColor = true;
            this.FilterPoeticForm.Click += new System.EventHandler(this.FilterButton_Click);
            // 
            // TextFilterPoeticForm
            // 
            this.TextFilterPoeticForm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TextFilterPoeticForm.Location = new System.Drawing.Point(3, 842);
            this.TextFilterPoeticForm.Name = "TextFilterPoeticForm";
            this.TextFilterPoeticForm.Size = new System.Drawing.Size(207, 20);
            this.TextFilterPoeticForm.TabIndex = 5;
            this.TextFilterPoeticForm.TextChanged += new System.EventHandler(this.TextFilter_Changed);
            // 
            // grpPoeticForm
            // 
            this.grpPoeticForm.Controls.Add(this.lblPoeticFormAltName);
            this.grpPoeticForm.Controls.Add(this.label296);
            this.grpPoeticForm.Controls.Add(this.lblPoeticFormName);
            this.grpPoeticForm.Controls.Add(this.label220);
            this.grpPoeticForm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpPoeticForm.Location = new System.Drawing.Point(216, 3);
            this.grpPoeticForm.Name = "grpPoeticForm";
            this.tableLayoutPanel62.SetRowSpan(this.grpPoeticForm, 3);
            this.grpPoeticForm.Size = new System.Drawing.Size(1654, 885);
            this.grpPoeticForm.TabIndex = 3;
            this.grpPoeticForm.TabStop = false;
            this.grpPoeticForm.Visible = false;
            // 
            // lblPoeticFormAltName
            // 
            this.lblPoeticFormAltName.AutoSize = true;
            this.lblPoeticFormAltName.Location = new System.Drawing.Point(67, 43);
            this.lblPoeticFormAltName.Name = "lblPoeticFormAltName";
            this.lblPoeticFormAltName.Size = new System.Drawing.Size(35, 13);
            this.lblPoeticFormAltName.TabIndex = 22;
            this.lblPoeticFormAltName.Text = "label2";
            // 
            // label296
            // 
            this.label296.AutoSize = true;
            this.label296.Location = new System.Drawing.Point(12, 43);
            this.label296.Name = "label296";
            this.label296.Size = new System.Drawing.Size(53, 13);
            this.label296.TabIndex = 21;
            this.label296.Text = "Alt Name:";
            // 
            // lblPoeticFormName
            // 
            this.lblPoeticFormName.AutoSize = true;
            this.lblPoeticFormName.Location = new System.Drawing.Point(67, 21);
            this.lblPoeticFormName.Name = "lblPoeticFormName";
            this.lblPoeticFormName.Size = new System.Drawing.Size(35, 13);
            this.lblPoeticFormName.TabIndex = 20;
            this.lblPoeticFormName.Text = "label2";
            // 
            // label220
            // 
            this.label220.AutoSize = true;
            this.label220.Location = new System.Drawing.Point(12, 21);
            this.label220.Name = "label220";
            this.label220.Size = new System.Drawing.Size(41, 13);
            this.label220.TabIndex = 19;
            this.label220.Text = "Name: ";
            // 
            // lstPoeticForm
            // 
            this.lstPoeticForm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstPoeticForm.FormattingEnabled = true;
            this.lstPoeticForm.Location = new System.Drawing.Point(3, 3);
            this.lstPoeticForm.Name = "lstPoeticForm";
            this.lstPoeticForm.Size = new System.Drawing.Size(207, 833);
            this.lstPoeticForm.TabIndex = 2;
            // 
            // tabMusicalForm
            // 
            this.tabMusicalForm.Controls.Add(this.tableLayoutPanel63);
            this.tabMusicalForm.Location = new System.Drawing.Point(4, 40);
            this.tabMusicalForm.Name = "tabMusicalForm";
            this.tabMusicalForm.Size = new System.Drawing.Size(1873, 873);
            this.tabMusicalForm.TabIndex = 37;
            this.tabMusicalForm.Text = "Musical Form";
            this.tabMusicalForm.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel63
            // 
            this.tableLayoutPanel63.ColumnCount = 2;
            this.tableLayoutPanel63.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 213F));
            this.tableLayoutPanel63.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel63.Controls.Add(this.FilterMusicalForm, 0, 2);
            this.tableLayoutPanel63.Controls.Add(this.TextFilterMusicalForm, 0, 1);
            this.tableLayoutPanel63.Controls.Add(this.grpMusicalForm, 1, 0);
            this.tableLayoutPanel63.Controls.Add(this.lstMusicalForm, 0, 0);
            this.tableLayoutPanel63.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel63.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel63.Margin = new System.Windows.Forms.Padding(2);
            this.tableLayoutPanel63.Name = "tableLayoutPanel63";
            this.tableLayoutPanel63.RowCount = 3;
            this.tableLayoutPanel63.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel63.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel63.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanel63.Size = new System.Drawing.Size(1873, 891);
            this.tableLayoutPanel63.TabIndex = 8;
            // 
            // FilterMusicalForm
            // 
            this.FilterMusicalForm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FilterMusicalForm.Location = new System.Drawing.Point(3, 866);
            this.FilterMusicalForm.Name = "FilterMusicalForm";
            this.FilterMusicalForm.Size = new System.Drawing.Size(207, 22);
            this.FilterMusicalForm.TabIndex = 6;
            this.FilterMusicalForm.Tag = "";
            this.FilterMusicalForm.Text = "Filter";
            this.FilterMusicalForm.UseVisualStyleBackColor = true;
            this.FilterMusicalForm.Click += new System.EventHandler(this.FilterButton_Click);
            // 
            // TextFilterMusicalForm
            // 
            this.TextFilterMusicalForm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TextFilterMusicalForm.Location = new System.Drawing.Point(3, 842);
            this.TextFilterMusicalForm.Name = "TextFilterMusicalForm";
            this.TextFilterMusicalForm.Size = new System.Drawing.Size(207, 20);
            this.TextFilterMusicalForm.TabIndex = 5;
            this.TextFilterMusicalForm.TextChanged += new System.EventHandler(this.TextFilter_Changed);
            // 
            // grpMusicalForm
            // 
            this.grpMusicalForm.Controls.Add(this.lblMusicalFormAltName);
            this.grpMusicalForm.Controls.Add(this.label226);
            this.grpMusicalForm.Controls.Add(this.lblMusicalFormName);
            this.grpMusicalForm.Controls.Add(this.label249);
            this.grpMusicalForm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpMusicalForm.Location = new System.Drawing.Point(216, 3);
            this.grpMusicalForm.Name = "grpMusicalForm";
            this.tableLayoutPanel63.SetRowSpan(this.grpMusicalForm, 3);
            this.grpMusicalForm.Size = new System.Drawing.Size(1654, 885);
            this.grpMusicalForm.TabIndex = 3;
            this.grpMusicalForm.TabStop = false;
            this.grpMusicalForm.Visible = false;
            // 
            // lblMusicalFormAltName
            // 
            this.lblMusicalFormAltName.AutoSize = true;
            this.lblMusicalFormAltName.Location = new System.Drawing.Point(67, 43);
            this.lblMusicalFormAltName.Name = "lblMusicalFormAltName";
            this.lblMusicalFormAltName.Size = new System.Drawing.Size(35, 13);
            this.lblMusicalFormAltName.TabIndex = 22;
            this.lblMusicalFormAltName.Text = "label2";
            // 
            // label226
            // 
            this.label226.AutoSize = true;
            this.label226.Location = new System.Drawing.Point(12, 43);
            this.label226.Name = "label226";
            this.label226.Size = new System.Drawing.Size(53, 13);
            this.label226.TabIndex = 21;
            this.label226.Text = "Alt Name:";
            // 
            // lblMusicalFormName
            // 
            this.lblMusicalFormName.AutoSize = true;
            this.lblMusicalFormName.Location = new System.Drawing.Point(67, 21);
            this.lblMusicalFormName.Name = "lblMusicalFormName";
            this.lblMusicalFormName.Size = new System.Drawing.Size(35, 13);
            this.lblMusicalFormName.TabIndex = 20;
            this.lblMusicalFormName.Text = "label2";
            // 
            // label249
            // 
            this.label249.AutoSize = true;
            this.label249.Location = new System.Drawing.Point(12, 21);
            this.label249.Name = "label249";
            this.label249.Size = new System.Drawing.Size(41, 13);
            this.label249.TabIndex = 19;
            this.label249.Text = "Name: ";
            // 
            // lstMusicalForm
            // 
            this.lstMusicalForm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstMusicalForm.FormattingEnabled = true;
            this.lstMusicalForm.Location = new System.Drawing.Point(3, 3);
            this.lstMusicalForm.Name = "lstMusicalForm";
            this.lstMusicalForm.Size = new System.Drawing.Size(207, 833);
            this.lstMusicalForm.TabIndex = 2;
            // 
            // tabDanceForm
            // 
            this.tabDanceForm.Controls.Add(this.tableLayoutPanel64);
            this.tabDanceForm.Location = new System.Drawing.Point(4, 40);
            this.tabDanceForm.Name = "tabDanceForm";
            this.tabDanceForm.Size = new System.Drawing.Size(1873, 873);
            this.tabDanceForm.TabIndex = 38;
            this.tabDanceForm.Text = "Dance Form";
            this.tabDanceForm.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel64
            // 
            this.tableLayoutPanel64.ColumnCount = 2;
            this.tableLayoutPanel64.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 213F));
            this.tableLayoutPanel64.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel64.Controls.Add(this.FilterDanceForm, 0, 2);
            this.tableLayoutPanel64.Controls.Add(this.TextFilterDanceForm, 0, 1);
            this.tableLayoutPanel64.Controls.Add(this.grpDanceForm, 1, 0);
            this.tableLayoutPanel64.Controls.Add(this.lstDanceForm, 0, 0);
            this.tableLayoutPanel64.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel64.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel64.Margin = new System.Windows.Forms.Padding(2);
            this.tableLayoutPanel64.Name = "tableLayoutPanel64";
            this.tableLayoutPanel64.RowCount = 3;
            this.tableLayoutPanel64.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel64.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel64.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanel64.Size = new System.Drawing.Size(1873, 873);
            this.tableLayoutPanel64.TabIndex = 8;
            // 
            // FilterDanceForm
            // 
            this.FilterDanceForm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FilterDanceForm.Location = new System.Drawing.Point(3, 848);
            this.FilterDanceForm.Name = "FilterDanceForm";
            this.FilterDanceForm.Size = new System.Drawing.Size(207, 22);
            this.FilterDanceForm.TabIndex = 6;
            this.FilterDanceForm.Tag = "";
            this.FilterDanceForm.Text = "Filter";
            this.FilterDanceForm.UseVisualStyleBackColor = true;
            this.FilterDanceForm.Click += new System.EventHandler(this.FilterButton_Click);
            // 
            // TextFilterDanceForm
            // 
            this.TextFilterDanceForm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TextFilterDanceForm.Location = new System.Drawing.Point(3, 824);
            this.TextFilterDanceForm.Name = "TextFilterDanceForm";
            this.TextFilterDanceForm.Size = new System.Drawing.Size(207, 20);
            this.TextFilterDanceForm.TabIndex = 5;
            this.TextFilterDanceForm.TextChanged += new System.EventHandler(this.TextFilter_Changed);
            // 
            // grpDanceForm
            // 
            this.grpDanceForm.Controls.Add(this.lblDanceFormAltName);
            this.grpDanceForm.Controls.Add(this.label297);
            this.grpDanceForm.Controls.Add(this.lblDanceFormName);
            this.grpDanceForm.Controls.Add(this.label299);
            this.grpDanceForm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpDanceForm.Location = new System.Drawing.Point(216, 3);
            this.grpDanceForm.Name = "grpDanceForm";
            this.tableLayoutPanel64.SetRowSpan(this.grpDanceForm, 3);
            this.grpDanceForm.Size = new System.Drawing.Size(1654, 867);
            this.grpDanceForm.TabIndex = 3;
            this.grpDanceForm.TabStop = false;
            this.grpDanceForm.Visible = false;
            // 
            // lblDanceFormAltName
            // 
            this.lblDanceFormAltName.AutoSize = true;
            this.lblDanceFormAltName.Location = new System.Drawing.Point(67, 43);
            this.lblDanceFormAltName.Name = "lblDanceFormAltName";
            this.lblDanceFormAltName.Size = new System.Drawing.Size(35, 13);
            this.lblDanceFormAltName.TabIndex = 22;
            this.lblDanceFormAltName.Text = "label2";
            // 
            // label297
            // 
            this.label297.AutoSize = true;
            this.label297.Location = new System.Drawing.Point(12, 43);
            this.label297.Name = "label297";
            this.label297.Size = new System.Drawing.Size(53, 13);
            this.label297.TabIndex = 21;
            this.label297.Text = "Alt Name:";
            // 
            // lblDanceFormName
            // 
            this.lblDanceFormName.AutoSize = true;
            this.lblDanceFormName.Location = new System.Drawing.Point(67, 21);
            this.lblDanceFormName.Name = "lblDanceFormName";
            this.lblDanceFormName.Size = new System.Drawing.Size(35, 13);
            this.lblDanceFormName.TabIndex = 20;
            this.lblDanceFormName.Text = "label2";
            // 
            // label299
            // 
            this.label299.AutoSize = true;
            this.label299.Location = new System.Drawing.Point(12, 21);
            this.label299.Name = "label299";
            this.label299.Size = new System.Drawing.Size(41, 13);
            this.label299.TabIndex = 19;
            this.label299.Text = "Name: ";
            // 
            // lstDanceForm
            // 
            this.lstDanceForm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstDanceForm.FormattingEnabled = true;
            this.lstDanceForm.Location = new System.Drawing.Point(3, 3);
            this.lstDanceForm.Name = "lstDanceForm";
            this.lstDanceForm.Size = new System.Drawing.Size(207, 815);
            this.lstDanceForm.TabIndex = 2;
            // 
            // tabSquad
            // 
            this.tabSquad.Controls.Add(this.tableLayoutPanel56);
            this.tabSquad.Location = new System.Drawing.Point(4, 40);
            this.tabSquad.Name = "tabSquad";
            this.tabSquad.Size = new System.Drawing.Size(1873, 873);
            this.tabSquad.TabIndex = 35;
            this.tabSquad.Text = "Squad";
            this.tabSquad.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel56
            // 
            this.tableLayoutPanel56.ColumnCount = 2;
            this.tableLayoutPanel56.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 213F));
            this.tableLayoutPanel56.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel56.Controls.Add(this.FilterSquad, 0, 2);
            this.tableLayoutPanel56.Controls.Add(this.TextFilterSquad, 0, 1);
            this.tableLayoutPanel56.Controls.Add(this.grpSquad, 1, 0);
            this.tableLayoutPanel56.Controls.Add(this.lstSquad, 0, 0);
            this.tableLayoutPanel56.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel56.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel56.Margin = new System.Windows.Forms.Padding(2);
            this.tableLayoutPanel56.Name = "tableLayoutPanel56";
            this.tableLayoutPanel56.RowCount = 3;
            this.tableLayoutPanel56.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel56.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel56.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanel56.Size = new System.Drawing.Size(1873, 873);
            this.tableLayoutPanel56.TabIndex = 8;
            // 
            // FilterSquad
            // 
            this.FilterSquad.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FilterSquad.Location = new System.Drawing.Point(3, 848);
            this.FilterSquad.Name = "FilterSquad";
            this.FilterSquad.Size = new System.Drawing.Size(207, 22);
            this.FilterSquad.TabIndex = 6;
            this.FilterSquad.Tag = "";
            this.FilterSquad.Text = "Filter";
            this.FilterSquad.UseVisualStyleBackColor = true;
            this.FilterSquad.Click += new System.EventHandler(this.FilterButton_Click);
            // 
            // TextFilterSquad
            // 
            this.TextFilterSquad.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TextFilterSquad.Location = new System.Drawing.Point(3, 824);
            this.TextFilterSquad.Name = "TextFilterSquad";
            this.TextFilterSquad.Size = new System.Drawing.Size(207, 20);
            this.TextFilterSquad.TabIndex = 5;
            this.TextFilterSquad.TextChanged += new System.EventHandler(this.TextFilter_Changed);
            // 
            // grpSquad
            // 
            this.grpSquad.Controls.Add(this.grpSquadMembers);
            this.grpSquad.Controls.Add(this.lblSquadEntity);
            this.grpSquad.Controls.Add(this.label303);
            this.grpSquad.Controls.Add(this.lblSquadAltName);
            this.grpSquad.Controls.Add(this.label307);
            this.grpSquad.Controls.Add(this.lblSquadName);
            this.grpSquad.Controls.Add(this.label229);
            this.grpSquad.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpSquad.Location = new System.Drawing.Point(216, 3);
            this.grpSquad.Name = "grpSquad";
            this.tableLayoutPanel56.SetRowSpan(this.grpSquad, 3);
            this.grpSquad.Size = new System.Drawing.Size(1654, 867);
            this.grpSquad.TabIndex = 3;
            this.grpSquad.TabStop = false;
            this.grpSquad.Visible = false;
            // 
            // grpSquadMembers
            // 
            this.grpSquadMembers.Controls.Add(this.lstSquadMembers);
            this.grpSquadMembers.Location = new System.Drawing.Point(15, 90);
            this.grpSquadMembers.Name = "grpSquadMembers";
            this.grpSquadMembers.Size = new System.Drawing.Size(200, 153);
            this.grpSquadMembers.TabIndex = 171;
            this.grpSquadMembers.TabStop = false;
            this.grpSquadMembers.Text = "Members";
            // 
            // lstSquadMembers
            // 
            this.lstSquadMembers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstSquadMembers.FormattingEnabled = true;
            this.lstSquadMembers.Location = new System.Drawing.Point(3, 16);
            this.lstSquadMembers.Name = "lstSquadMembers";
            this.lstSquadMembers.Size = new System.Drawing.Size(194, 134);
            this.lstSquadMembers.TabIndex = 0;
            // 
            // label303
            // 
            this.label303.AutoSize = true;
            this.label303.Location = new System.Drawing.Point(12, 65);
            this.label303.Name = "label303";
            this.label303.Size = new System.Drawing.Size(36, 13);
            this.label303.TabIndex = 158;
            this.label303.Text = "Entity:";
            // 
            // lblSquadAltName
            // 
            this.lblSquadAltName.AutoSize = true;
            this.lblSquadAltName.Location = new System.Drawing.Point(67, 43);
            this.lblSquadAltName.Name = "lblSquadAltName";
            this.lblSquadAltName.Size = new System.Drawing.Size(35, 13);
            this.lblSquadAltName.TabIndex = 157;
            this.lblSquadAltName.Text = "label2";
            // 
            // label307
            // 
            this.label307.AutoSize = true;
            this.label307.Location = new System.Drawing.Point(12, 43);
            this.label307.Name = "label307";
            this.label307.Size = new System.Drawing.Size(50, 13);
            this.label307.TabIndex = 156;
            this.label307.Text = "AltName:";
            // 
            // lblSquadName
            // 
            this.lblSquadName.AutoSize = true;
            this.lblSquadName.Location = new System.Drawing.Point(67, 21);
            this.lblSquadName.Name = "lblSquadName";
            this.lblSquadName.Size = new System.Drawing.Size(35, 13);
            this.lblSquadName.TabIndex = 20;
            this.lblSquadName.Text = "label2";
            // 
            // label229
            // 
            this.label229.AutoSize = true;
            this.label229.Location = new System.Drawing.Point(12, 21);
            this.label229.Name = "label229";
            this.label229.Size = new System.Drawing.Size(41, 13);
            this.label229.TabIndex = 19;
            this.label229.Text = "Name: ";
            // 
            // lstSquad
            // 
            this.lstSquad.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstSquad.FormattingEnabled = true;
            this.lstSquad.Location = new System.Drawing.Point(3, 3);
            this.lstSquad.Name = "lstSquad";
            this.lstSquad.Size = new System.Drawing.Size(207, 815);
            this.lstSquad.TabIndex = 2;
            // 
            // tabBuilding
            // 
            this.tabBuilding.Controls.Add(this.tableLayoutPanel52);
            this.tabBuilding.Location = new System.Drawing.Point(4, 40);
            this.tabBuilding.Name = "tabBuilding";
            this.tabBuilding.Size = new System.Drawing.Size(1873, 873);
            this.tabBuilding.TabIndex = 31;
            this.tabBuilding.Text = "Building";
            this.tabBuilding.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel52
            // 
            this.tableLayoutPanel52.ColumnCount = 2;
            this.tableLayoutPanel52.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 213F));
            this.tableLayoutPanel52.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel52.Controls.Add(this.FilterBuilding, 0, 2);
            this.tableLayoutPanel52.Controls.Add(this.TextFilterBuilding, 0, 1);
            this.tableLayoutPanel52.Controls.Add(this.grpBuilding, 1, 0);
            this.tableLayoutPanel52.Controls.Add(this.lstBuilding, 0, 0);
            this.tableLayoutPanel52.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel52.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel52.Margin = new System.Windows.Forms.Padding(2);
            this.tableLayoutPanel52.Name = "tableLayoutPanel52";
            this.tableLayoutPanel52.RowCount = 3;
            this.tableLayoutPanel52.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel52.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel52.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanel52.Size = new System.Drawing.Size(1873, 873);
            this.tableLayoutPanel52.TabIndex = 8;
            // 
            // FilterBuilding
            // 
            this.FilterBuilding.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FilterBuilding.Location = new System.Drawing.Point(3, 848);
            this.FilterBuilding.Name = "FilterBuilding";
            this.FilterBuilding.Size = new System.Drawing.Size(207, 22);
            this.FilterBuilding.TabIndex = 6;
            this.FilterBuilding.Tag = "";
            this.FilterBuilding.Text = "Filter";
            this.FilterBuilding.UseVisualStyleBackColor = true;
            this.FilterBuilding.Click += new System.EventHandler(this.FilterButton_Click);
            // 
            // TextFilterBuilding
            // 
            this.TextFilterBuilding.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TextFilterBuilding.Location = new System.Drawing.Point(3, 824);
            this.TextFilterBuilding.Name = "TextFilterBuilding";
            this.TextFilterBuilding.Size = new System.Drawing.Size(207, 20);
            this.TextFilterBuilding.TabIndex = 5;
            this.TextFilterBuilding.TextChanged += new System.EventHandler(this.TextFilter_Changed);
            // 
            // grpBuilding
            // 
            this.grpBuilding.Controls.Add(this.grpBuildingReferences);
            this.grpBuilding.Controls.Add(this.lblBuildingClaimed);
            this.grpBuilding.Controls.Add(this.label206);
            this.grpBuilding.Controls.Add(this.lblBuildingCoordsCenter);
            this.grpBuilding.Controls.Add(this.lblBuildingSquad);
            this.grpBuilding.Controls.Add(this.lblBuildingCorpse);
            this.grpBuilding.Controls.Add(this.lblBuildingCorpseHF);
            this.grpBuilding.Controls.Add(this.lblBuildingDir);
            this.grpBuilding.Controls.Add(this.label287);
            this.grpBuilding.Controls.Add(this.label289);
            this.grpBuilding.Controls.Add(this.label291);
            this.grpBuilding.Controls.Add(this.lblBuildingOwner);
            this.grpBuilding.Controls.Add(this.lblBuildingRace);
            this.grpBuilding.Controls.Add(this.lblBuildingZoneFlags);
            this.grpBuilding.Controls.Add(this.lblBuildingSubType);
            this.grpBuilding.Controls.Add(this.lblBuildingType);
            this.grpBuilding.Controls.Add(this.label253);
            this.grpBuilding.Controls.Add(this.label255);
            this.grpBuilding.Controls.Add(this.label259);
            this.grpBuilding.Controls.Add(this.label264);
            this.grpBuilding.Controls.Add(this.label266);
            this.grpBuilding.Controls.Add(this.label270);
            this.grpBuilding.Controls.Add(this.lblBuildingMat);
            this.grpBuilding.Controls.Add(this.label275);
            this.grpBuilding.Controls.Add(this.lblBuildingCoords2);
            this.grpBuilding.Controls.Add(this.label279);
            this.grpBuilding.Controls.Add(this.label281);
            this.grpBuilding.Controls.Add(this.lblBuildingCoords1);
            this.grpBuilding.Controls.Add(this.label285);
            this.grpBuilding.Controls.Add(this.lblBuildingName);
            this.grpBuilding.Controls.Add(this.label221);
            this.grpBuilding.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpBuilding.Location = new System.Drawing.Point(216, 3);
            this.grpBuilding.Name = "grpBuilding";
            this.tableLayoutPanel52.SetRowSpan(this.grpBuilding, 3);
            this.grpBuilding.Size = new System.Drawing.Size(1654, 867);
            this.grpBuilding.TabIndex = 3;
            this.grpBuilding.TabStop = false;
            this.grpBuilding.Visible = false;
            // 
            // grpBuildingReferences
            // 
            this.grpBuildingReferences.Controls.Add(this.lstBuildingReferences);
            this.grpBuildingReferences.Location = new System.Drawing.Point(15, 359);
            this.grpBuildingReferences.Name = "grpBuildingReferences";
            this.grpBuildingReferences.Size = new System.Drawing.Size(422, 173);
            this.grpBuildingReferences.TabIndex = 139;
            this.grpBuildingReferences.TabStop = false;
            this.grpBuildingReferences.Text = "References";
            // 
            // lstBuildingReferences
            // 
            this.lstBuildingReferences.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstBuildingReferences.FormattingEnabled = true;
            this.lstBuildingReferences.Location = new System.Drawing.Point(3, 16);
            this.lstBuildingReferences.Name = "lstBuildingReferences";
            this.lstBuildingReferences.Size = new System.Drawing.Size(416, 154);
            this.lstBuildingReferences.TabIndex = 0;
            // 
            // label206
            // 
            this.label206.AutoSize = true;
            this.label206.Location = new System.Drawing.Point(12, 219);
            this.label206.Name = "label206";
            this.label206.Size = new System.Drawing.Size(47, 13);
            this.label206.TabIndex = 137;
            this.label206.Text = "Claimed:";
            // 
            // lblBuildingCoordsCenter
            // 
            this.lblBuildingCoordsCenter.AutoSize = true;
            this.lblBuildingCoordsCenter.Location = new System.Drawing.Point(67, 65);
            this.lblBuildingCoordsCenter.Name = "lblBuildingCoordsCenter";
            this.lblBuildingCoordsCenter.Size = new System.Drawing.Size(35, 13);
            this.lblBuildingCoordsCenter.TabIndex = 136;
            this.lblBuildingCoordsCenter.Text = "label2";
            // 
            // lblBuildingDir
            // 
            this.lblBuildingDir.AutoSize = true;
            this.lblBuildingDir.Location = new System.Drawing.Point(67, 307);
            this.lblBuildingDir.Name = "lblBuildingDir";
            this.lblBuildingDir.Size = new System.Drawing.Size(35, 13);
            this.lblBuildingDir.TabIndex = 131;
            this.lblBuildingDir.Text = "label2";
            // 
            // label287
            // 
            this.label287.AutoSize = true;
            this.label287.Location = new System.Drawing.Point(12, 329);
            this.label287.Name = "label287";
            this.label287.Size = new System.Drawing.Size(41, 13);
            this.label287.TabIndex = 130;
            this.label287.Text = "Squad:";
            // 
            // label289
            // 
            this.label289.AutoSize = true;
            this.label289.Location = new System.Drawing.Point(12, 307);
            this.label289.Name = "label289";
            this.label289.Size = new System.Drawing.Size(23, 13);
            this.label289.TabIndex = 129;
            this.label289.Text = "Dir:";
            // 
            // label291
            // 
            this.label291.AutoSize = true;
            this.label291.Location = new System.Drawing.Point(12, 285);
            this.label291.Name = "label291";
            this.label291.Size = new System.Drawing.Size(57, 13);
            this.label291.TabIndex = 128;
            this.label291.Text = "CorpseHF:";
            // 
            // lblBuildingZoneFlags
            // 
            this.lblBuildingZoneFlags.AutoSize = true;
            this.lblBuildingZoneFlags.Location = new System.Drawing.Point(67, 241);
            this.lblBuildingZoneFlags.Name = "lblBuildingZoneFlags";
            this.lblBuildingZoneFlags.Size = new System.Drawing.Size(35, 13);
            this.lblBuildingZoneFlags.TabIndex = 124;
            this.lblBuildingZoneFlags.Text = "label2";
            // 
            // lblBuildingSubType
            // 
            this.lblBuildingSubType.AutoSize = true;
            this.lblBuildingSubType.Location = new System.Drawing.Point(67, 175);
            this.lblBuildingSubType.Name = "lblBuildingSubType";
            this.lblBuildingSubType.Size = new System.Drawing.Size(35, 13);
            this.lblBuildingSubType.TabIndex = 123;
            this.lblBuildingSubType.Text = "label2";
            // 
            // lblBuildingType
            // 
            this.lblBuildingType.AutoSize = true;
            this.lblBuildingType.Location = new System.Drawing.Point(67, 153);
            this.lblBuildingType.Name = "lblBuildingType";
            this.lblBuildingType.Size = new System.Drawing.Size(35, 13);
            this.lblBuildingType.TabIndex = 122;
            this.lblBuildingType.Text = "label2";
            // 
            // label253
            // 
            this.label253.AutoSize = true;
            this.label253.Location = new System.Drawing.Point(12, 263);
            this.label253.Name = "label253";
            this.label253.Size = new System.Drawing.Size(43, 13);
            this.label253.TabIndex = 120;
            this.label253.Text = "Corpse:";
            // 
            // label255
            // 
            this.label255.AutoSize = true;
            this.label255.Location = new System.Drawing.Point(12, 241);
            this.label255.Name = "label255";
            this.label255.Size = new System.Drawing.Size(57, 13);
            this.label255.TabIndex = 119;
            this.label255.Text = "Zone Flgs:";
            // 
            // label259
            // 
            this.label259.AutoSize = true;
            this.label259.Location = new System.Drawing.Point(12, 197);
            this.label259.Name = "label259";
            this.label259.Size = new System.Drawing.Size(41, 13);
            this.label259.TabIndex = 117;
            this.label259.Text = "Owner:";
            // 
            // label264
            // 
            this.label264.AutoSize = true;
            this.label264.Location = new System.Drawing.Point(12, 175);
            this.label264.Name = "label264";
            this.label264.Size = new System.Drawing.Size(53, 13);
            this.label264.TabIndex = 116;
            this.label264.Text = "SubType:";
            // 
            // label266
            // 
            this.label266.AutoSize = true;
            this.label266.Location = new System.Drawing.Point(12, 153);
            this.label266.Name = "label266";
            this.label266.Size = new System.Drawing.Size(34, 13);
            this.label266.TabIndex = 115;
            this.label266.Text = "Type:";
            // 
            // label270
            // 
            this.label270.AutoSize = true;
            this.label270.Location = new System.Drawing.Point(12, 131);
            this.label270.Name = "label270";
            this.label270.Size = new System.Drawing.Size(36, 13);
            this.label270.TabIndex = 113;
            this.label270.Text = "Race:";
            // 
            // lblBuildingMat
            // 
            this.lblBuildingMat.AutoSize = true;
            this.lblBuildingMat.Location = new System.Drawing.Point(67, 109);
            this.lblBuildingMat.Name = "lblBuildingMat";
            this.lblBuildingMat.Size = new System.Drawing.Size(35, 13);
            this.lblBuildingMat.TabIndex = 112;
            this.lblBuildingMat.Text = "label2";
            // 
            // label275
            // 
            this.label275.AutoSize = true;
            this.label275.Location = new System.Drawing.Point(12, 109);
            this.label275.Name = "label275";
            this.label275.Size = new System.Drawing.Size(28, 13);
            this.label275.TabIndex = 111;
            this.label275.Text = "Mat:";
            // 
            // lblBuildingCoords2
            // 
            this.lblBuildingCoords2.AutoSize = true;
            this.lblBuildingCoords2.Location = new System.Drawing.Point(67, 87);
            this.lblBuildingCoords2.Name = "lblBuildingCoords2";
            this.lblBuildingCoords2.Size = new System.Drawing.Size(35, 13);
            this.lblBuildingCoords2.TabIndex = 110;
            this.lblBuildingCoords2.Text = "label2";
            // 
            // label279
            // 
            this.label279.AutoSize = true;
            this.label279.Location = new System.Drawing.Point(12, 87);
            this.label279.Name = "label279";
            this.label279.Size = new System.Drawing.Size(58, 13);
            this.label279.TabIndex = 109;
            this.label279.Text = "CoordsBR:";
            // 
            // label281
            // 
            this.label281.AutoSize = true;
            this.label281.Location = new System.Drawing.Point(12, 65);
            this.label281.Name = "label281";
            this.label281.Size = new System.Drawing.Size(43, 13);
            this.label281.TabIndex = 108;
            this.label281.Text = "Coords:";
            // 
            // lblBuildingCoords1
            // 
            this.lblBuildingCoords1.AutoSize = true;
            this.lblBuildingCoords1.Location = new System.Drawing.Point(67, 43);
            this.lblBuildingCoords1.Name = "lblBuildingCoords1";
            this.lblBuildingCoords1.Size = new System.Drawing.Size(35, 13);
            this.lblBuildingCoords1.TabIndex = 107;
            this.lblBuildingCoords1.Text = "label2";
            // 
            // label285
            // 
            this.label285.AutoSize = true;
            this.label285.Location = new System.Drawing.Point(12, 43);
            this.label285.Name = "label285";
            this.label285.Size = new System.Drawing.Size(56, 13);
            this.label285.TabIndex = 106;
            this.label285.Text = "CoordsTL:";
            // 
            // lblBuildingName
            // 
            this.lblBuildingName.AutoSize = true;
            this.lblBuildingName.Location = new System.Drawing.Point(67, 21);
            this.lblBuildingName.Name = "lblBuildingName";
            this.lblBuildingName.Size = new System.Drawing.Size(35, 13);
            this.lblBuildingName.TabIndex = 20;
            this.lblBuildingName.Text = "label2";
            // 
            // label221
            // 
            this.label221.AutoSize = true;
            this.label221.Location = new System.Drawing.Point(12, 21);
            this.label221.Name = "label221";
            this.label221.Size = new System.Drawing.Size(41, 13);
            this.label221.TabIndex = 19;
            this.label221.Text = "Name: ";
            // 
            // lstBuilding
            // 
            this.lstBuilding.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstBuilding.FormattingEnabled = true;
            this.lstBuilding.Location = new System.Drawing.Point(3, 3);
            this.lstBuilding.Name = "lstBuilding";
            this.lstBuilding.Size = new System.Drawing.Size(207, 815);
            this.lstBuilding.TabIndex = 2;
            // 
            // tabConstruction
            // 
            this.tabConstruction.Controls.Add(this.tableLayoutPanel53);
            this.tabConstruction.Location = new System.Drawing.Point(4, 40);
            this.tabConstruction.Name = "tabConstruction";
            this.tabConstruction.Size = new System.Drawing.Size(1873, 873);
            this.tabConstruction.TabIndex = 32;
            this.tabConstruction.Text = "Construction";
            this.tabConstruction.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel53
            // 
            this.tableLayoutPanel53.ColumnCount = 2;
            this.tableLayoutPanel53.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 213F));
            this.tableLayoutPanel53.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel53.Controls.Add(this.FilterConstruction, 0, 2);
            this.tableLayoutPanel53.Controls.Add(this.TextFilterConstruction, 0, 1);
            this.tableLayoutPanel53.Controls.Add(this.grpConstruction, 1, 0);
            this.tableLayoutPanel53.Controls.Add(this.lstConstruction, 0, 0);
            this.tableLayoutPanel53.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel53.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel53.Margin = new System.Windows.Forms.Padding(2);
            this.tableLayoutPanel53.Name = "tableLayoutPanel53";
            this.tableLayoutPanel53.RowCount = 3;
            this.tableLayoutPanel53.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel53.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel53.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanel53.Size = new System.Drawing.Size(1873, 873);
            this.tableLayoutPanel53.TabIndex = 8;
            // 
            // FilterConstruction
            // 
            this.FilterConstruction.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FilterConstruction.Location = new System.Drawing.Point(3, 848);
            this.FilterConstruction.Name = "FilterConstruction";
            this.FilterConstruction.Size = new System.Drawing.Size(207, 22);
            this.FilterConstruction.TabIndex = 6;
            this.FilterConstruction.Tag = "";
            this.FilterConstruction.Text = "Filter";
            this.FilterConstruction.UseVisualStyleBackColor = true;
            this.FilterConstruction.Click += new System.EventHandler(this.FilterButton_Click);
            // 
            // TextFilterConstruction
            // 
            this.TextFilterConstruction.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TextFilterConstruction.Location = new System.Drawing.Point(3, 824);
            this.TextFilterConstruction.Name = "TextFilterConstruction";
            this.TextFilterConstruction.Size = new System.Drawing.Size(207, 20);
            this.TextFilterConstruction.TabIndex = 5;
            this.TextFilterConstruction.TextChanged += new System.EventHandler(this.TextFilter_Changed);
            // 
            // grpConstruction
            // 
            this.grpConstruction.Controls.Add(this.lblConstructionMat);
            this.grpConstruction.Controls.Add(this.label304);
            this.grpConstruction.Controls.Add(this.lblConstructionItemType);
            this.grpConstruction.Controls.Add(this.label306);
            this.grpConstruction.Controls.Add(this.lblConstructionCoords);
            this.grpConstruction.Controls.Add(this.label308);
            this.grpConstruction.Controls.Add(this.lblConstructionName);
            this.grpConstruction.Controls.Add(this.label223);
            this.grpConstruction.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpConstruction.Location = new System.Drawing.Point(216, 3);
            this.grpConstruction.Name = "grpConstruction";
            this.tableLayoutPanel53.SetRowSpan(this.grpConstruction, 3);
            this.grpConstruction.Size = new System.Drawing.Size(1654, 867);
            this.grpConstruction.TabIndex = 3;
            this.grpConstruction.TabStop = false;
            this.grpConstruction.Visible = false;
            // 
            // lblConstructionMat
            // 
            this.lblConstructionMat.AutoSize = true;
            this.lblConstructionMat.Location = new System.Drawing.Point(67, 88);
            this.lblConstructionMat.Name = "lblConstructionMat";
            this.lblConstructionMat.Size = new System.Drawing.Size(35, 13);
            this.lblConstructionMat.TabIndex = 166;
            this.lblConstructionMat.Text = "label2";
            // 
            // label304
            // 
            this.label304.AutoSize = true;
            this.label304.Location = new System.Drawing.Point(12, 88);
            this.label304.Name = "label304";
            this.label304.Size = new System.Drawing.Size(28, 13);
            this.label304.TabIndex = 143;
            this.label304.Text = "Mat:";
            // 
            // lblConstructionItemType
            // 
            this.lblConstructionItemType.AutoSize = true;
            this.lblConstructionItemType.Location = new System.Drawing.Point(67, 66);
            this.lblConstructionItemType.Name = "lblConstructionItemType";
            this.lblConstructionItemType.Size = new System.Drawing.Size(35, 13);
            this.lblConstructionItemType.TabIndex = 142;
            this.lblConstructionItemType.Text = "label2";
            // 
            // label306
            // 
            this.label306.AutoSize = true;
            this.label306.Location = new System.Drawing.Point(12, 66);
            this.label306.Name = "label306";
            this.label306.Size = new System.Drawing.Size(57, 13);
            this.label306.TabIndex = 141;
            this.label306.Text = "Item Type:";
            // 
            // lblConstructionCoords
            // 
            this.lblConstructionCoords.AutoSize = true;
            this.lblConstructionCoords.Location = new System.Drawing.Point(67, 44);
            this.lblConstructionCoords.Name = "lblConstructionCoords";
            this.lblConstructionCoords.Size = new System.Drawing.Size(35, 13);
            this.lblConstructionCoords.TabIndex = 140;
            this.lblConstructionCoords.Text = "label2";
            // 
            // label308
            // 
            this.label308.AutoSize = true;
            this.label308.Location = new System.Drawing.Point(12, 44);
            this.label308.Name = "label308";
            this.label308.Size = new System.Drawing.Size(43, 13);
            this.label308.TabIndex = 139;
            this.label308.Text = "Coords:";
            // 
            // lblConstructionName
            // 
            this.lblConstructionName.AutoSize = true;
            this.lblConstructionName.Location = new System.Drawing.Point(67, 21);
            this.lblConstructionName.Name = "lblConstructionName";
            this.lblConstructionName.Size = new System.Drawing.Size(35, 13);
            this.lblConstructionName.TabIndex = 20;
            this.lblConstructionName.Text = "label2";
            // 
            // label223
            // 
            this.label223.AutoSize = true;
            this.label223.Location = new System.Drawing.Point(12, 21);
            this.label223.Name = "label223";
            this.label223.Size = new System.Drawing.Size(41, 13);
            this.label223.TabIndex = 19;
            this.label223.Text = "Name: ";
            // 
            // lstConstruction
            // 
            this.lstConstruction.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstConstruction.FormattingEnabled = true;
            this.lstConstruction.Location = new System.Drawing.Point(3, 3);
            this.lstConstruction.Name = "lstConstruction";
            this.lstConstruction.Size = new System.Drawing.Size(207, 815);
            this.lstConstruction.TabIndex = 2;
            // 
            // tabItem
            // 
            this.tabItem.Controls.Add(this.tableLayoutPanel54);
            this.tabItem.Location = new System.Drawing.Point(4, 40);
            this.tabItem.Name = "tabItem";
            this.tabItem.Size = new System.Drawing.Size(1873, 873);
            this.tabItem.TabIndex = 33;
            this.tabItem.Text = "Item";
            this.tabItem.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel54
            // 
            this.tableLayoutPanel54.ColumnCount = 4;
            this.tableLayoutPanel54.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 213F));
            this.tableLayoutPanel54.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel54.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 250F));
            this.tableLayoutPanel54.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 250F));
            this.tableLayoutPanel54.Controls.Add(this.groupBox7, 3, 0);
            this.tableLayoutPanel54.Controls.Add(this.groupBox6, 2, 0);
            this.tableLayoutPanel54.Controls.Add(this.FilterItem, 0, 2);
            this.tableLayoutPanel54.Controls.Add(this.TextFilterItem, 0, 1);
            this.tableLayoutPanel54.Controls.Add(this.grpItem, 1, 0);
            this.tableLayoutPanel54.Controls.Add(this.lstItem, 0, 0);
            this.tableLayoutPanel54.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel54.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel54.Margin = new System.Windows.Forms.Padding(2);
            this.tableLayoutPanel54.Name = "tableLayoutPanel54";
            this.tableLayoutPanel54.RowCount = 3;
            this.tableLayoutPanel54.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel54.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel54.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanel54.Size = new System.Drawing.Size(1873, 873);
            this.tableLayoutPanel54.TabIndex = 8;
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.trvItemsByQualityType);
            this.groupBox7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox7.Location = new System.Drawing.Point(1626, 3);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(244, 815);
            this.groupBox7.TabIndex = 223;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Items by Quality / Type";
            // 
            // trvItemsByQualityType
            // 
            this.trvItemsByQualityType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trvItemsByQualityType.Location = new System.Drawing.Point(3, 16);
            this.trvItemsByQualityType.Name = "trvItemsByQualityType";
            this.trvItemsByQualityType.Size = new System.Drawing.Size(238, 796);
            this.trvItemsByQualityType.TabIndex = 10;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.trvItemsByTypeMaterial);
            this.groupBox6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox6.Location = new System.Drawing.Point(1376, 3);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(244, 815);
            this.groupBox6.TabIndex = 222;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Items by Type / Material";
            // 
            // trvItemsByTypeMaterial
            // 
            this.trvItemsByTypeMaterial.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trvItemsByTypeMaterial.Location = new System.Drawing.Point(3, 16);
            this.trvItemsByTypeMaterial.Name = "trvItemsByTypeMaterial";
            this.trvItemsByTypeMaterial.Size = new System.Drawing.Size(238, 796);
            this.trvItemsByTypeMaterial.TabIndex = 10;
            // 
            // FilterItem
            // 
            this.FilterItem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FilterItem.Location = new System.Drawing.Point(3, 848);
            this.FilterItem.Name = "FilterItem";
            this.FilterItem.Size = new System.Drawing.Size(207, 22);
            this.FilterItem.TabIndex = 6;
            this.FilterItem.Tag = "";
            this.FilterItem.Text = "Filter";
            this.FilterItem.UseVisualStyleBackColor = true;
            this.FilterItem.Click += new System.EventHandler(this.FilterButton_Click);
            // 
            // TextFilterItem
            // 
            this.TextFilterItem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TextFilterItem.Location = new System.Drawing.Point(3, 824);
            this.TextFilterItem.Name = "TextFilterItem";
            this.TextFilterItem.Size = new System.Drawing.Size(207, 20);
            this.TextFilterItem.TabIndex = 5;
            this.TextFilterItem.TextChanged += new System.EventHandler(this.TextFilter_Changed);
            // 
            // grpItem
            // 
            this.grpItem.Controls.Add(this.grpItemReferences);
            this.grpItem.Controls.Add(this.lblItemWeight);
            this.grpItem.Controls.Add(this.label208);
            this.grpItem.Controls.Add(this.lblItemCoords);
            this.grpItem.Controls.Add(this.label298);
            this.grpItem.Controls.Add(this.lblItemMasterpieceEvent);
            this.grpItem.Controls.Add(this.label212);
            this.grpItem.Controls.Add(this.lblItemDyeMat);
            this.grpItem.Controls.Add(this.lblItemUnit);
            this.grpItem.Controls.Add(this.lblItemHF);
            this.grpItem.Controls.Add(this.lblItemStockpileCoords);
            this.grpItem.Controls.Add(this.label335);
            this.grpItem.Controls.Add(this.lblItemStockpile);
            this.grpItem.Controls.Add(this.label337);
            this.grpItem.Controls.Add(this.lblItemBoneMat);
            this.grpItem.Controls.Add(this.label332);
            this.grpItem.Controls.Add(this.label215);
            this.grpItem.Controls.Add(this.label300);
            this.grpItem.Controls.Add(this.lblItemShape);
            this.grpItem.Controls.Add(this.label302);
            this.grpItem.Controls.Add(this.label305);
            this.grpItem.Controls.Add(this.lblItemHand);
            this.grpItem.Controls.Add(this.label310);
            this.grpItem.Controls.Add(this.lblItemWear);
            this.grpItem.Controls.Add(this.label313);
            this.grpItem.Controls.Add(this.lblItemAge);
            this.grpItem.Controls.Add(this.label316);
            this.grpItem.Controls.Add(this.grpItemImage);
            this.grpItem.Controls.Add(this.grpItemImprovement);
            this.grpItem.Controls.Add(this.grpItemIngredient);
            this.grpItem.Controls.Add(this.lblItemSkill);
            this.grpItem.Controls.Add(this.label251);
            this.grpItem.Controls.Add(this.lblItemSubType);
            this.grpItem.Controls.Add(this.label245);
            this.grpItem.Controls.Add(this.lblItemType);
            this.grpItem.Controls.Add(this.label222);
            this.grpItem.Controls.Add(this.lblItemQuality);
            this.grpItem.Controls.Add(this.lblItemMaker);
            this.grpItem.Controls.Add(this.label256);
            this.grpItem.Controls.Add(this.label268);
            this.grpItem.Controls.Add(this.lblItemMat);
            this.grpItem.Controls.Add(this.label277);
            this.grpItem.Controls.Add(this.lblItemDescription);
            this.grpItem.Controls.Add(this.label293);
            this.grpItem.Controls.Add(this.lblItemName);
            this.grpItem.Controls.Add(this.label225);
            this.grpItem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpItem.Location = new System.Drawing.Point(216, 3);
            this.grpItem.Name = "grpItem";
            this.tableLayoutPanel54.SetRowSpan(this.grpItem, 3);
            this.grpItem.Size = new System.Drawing.Size(1154, 867);
            this.grpItem.TabIndex = 3;
            this.grpItem.TabStop = false;
            this.grpItem.Visible = false;
            // 
            // grpItemReferences
            // 
            this.grpItemReferences.Controls.Add(this.lstItemReferences);
            this.grpItemReferences.Location = new System.Drawing.Point(199, 415);
            this.grpItemReferences.Name = "grpItemReferences";
            this.grpItemReferences.Size = new System.Drawing.Size(391, 202);
            this.grpItemReferences.TabIndex = 221;
            this.grpItemReferences.TabStop = false;
            this.grpItemReferences.Text = "References";
            // 
            // lstItemReferences
            // 
            this.lstItemReferences.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstItemReferences.FormattingEnabled = true;
            this.lstItemReferences.Location = new System.Drawing.Point(3, 16);
            this.lstItemReferences.Name = "lstItemReferences";
            this.lstItemReferences.Size = new System.Drawing.Size(385, 183);
            this.lstItemReferences.TabIndex = 0;
            // 
            // lblItemWeight
            // 
            this.lblItemWeight.AutoSize = true;
            this.lblItemWeight.Location = new System.Drawing.Point(91, 197);
            this.lblItemWeight.Name = "lblItemWeight";
            this.lblItemWeight.Size = new System.Drawing.Size(35, 13);
            this.lblItemWeight.TabIndex = 220;
            this.lblItemWeight.Text = "label2";
            // 
            // label208
            // 
            this.label208.AutoSize = true;
            this.label208.Location = new System.Drawing.Point(12, 197);
            this.label208.Name = "label208";
            this.label208.Size = new System.Drawing.Size(44, 13);
            this.label208.TabIndex = 219;
            this.label208.Text = "Weight:";
            // 
            // lblItemCoords
            // 
            this.lblItemCoords.AutoSize = true;
            this.lblItemCoords.Location = new System.Drawing.Point(91, 65);
            this.lblItemCoords.Name = "lblItemCoords";
            this.lblItemCoords.Size = new System.Drawing.Size(35, 13);
            this.lblItemCoords.TabIndex = 218;
            this.lblItemCoords.Text = "label2";
            // 
            // label298
            // 
            this.label298.AutoSize = true;
            this.label298.Location = new System.Drawing.Point(12, 65);
            this.label298.Name = "label298";
            this.label298.Size = new System.Drawing.Size(43, 13);
            this.label298.TabIndex = 217;
            this.label298.Text = "Coords:";
            // 
            // label212
            // 
            this.label212.AutoSize = true;
            this.label212.Location = new System.Drawing.Point(12, 241);
            this.label212.Name = "label212";
            this.label212.Size = new System.Drawing.Size(68, 13);
            this.label212.TabIndex = 213;
            this.label212.Text = "Masterpiece:";
            // 
            // lblItemDyeMat
            // 
            this.lblItemDyeMat.AutoSize = true;
            this.lblItemDyeMat.Location = new System.Drawing.Point(90, 352);
            this.lblItemDyeMat.Name = "lblItemDyeMat";
            this.lblItemDyeMat.Size = new System.Drawing.Size(35, 13);
            this.lblItemDyeMat.TabIndex = 212;
            this.lblItemDyeMat.Text = "label2";
            // 
            // lblItemStockpileCoords
            // 
            this.lblItemStockpileCoords.AutoSize = true;
            this.lblItemStockpileCoords.Location = new System.Drawing.Point(91, 464);
            this.lblItemStockpileCoords.Name = "lblItemStockpileCoords";
            this.lblItemStockpileCoords.Size = new System.Drawing.Size(35, 13);
            this.lblItemStockpileCoords.TabIndex = 203;
            this.lblItemStockpileCoords.Text = "label2";
            // 
            // label335
            // 
            this.label335.AutoSize = true;
            this.label335.Location = new System.Drawing.Point(12, 464);
            this.label335.Name = "label335";
            this.label335.Size = new System.Drawing.Size(74, 13);
            this.label335.TabIndex = 202;
            this.label335.Text = "StkPile Coord:";
            // 
            // lblItemStockpile
            // 
            this.lblItemStockpile.AutoSize = true;
            this.lblItemStockpile.Location = new System.Drawing.Point(91, 442);
            this.lblItemStockpile.Name = "lblItemStockpile";
            this.lblItemStockpile.Size = new System.Drawing.Size(35, 13);
            this.lblItemStockpile.TabIndex = 201;
            this.lblItemStockpile.Text = "label2";
            // 
            // label337
            // 
            this.label337.AutoSize = true;
            this.label337.Location = new System.Drawing.Point(12, 442);
            this.label337.Name = "label337";
            this.label337.Size = new System.Drawing.Size(54, 13);
            this.label337.TabIndex = 200;
            this.label337.Text = "Stockpile:";
            // 
            // lblItemBoneMat
            // 
            this.lblItemBoneMat.AutoSize = true;
            this.lblItemBoneMat.Location = new System.Drawing.Point(91, 419);
            this.lblItemBoneMat.Name = "lblItemBoneMat";
            this.lblItemBoneMat.Size = new System.Drawing.Size(35, 13);
            this.lblItemBoneMat.TabIndex = 180;
            this.lblItemBoneMat.Text = "label2";
            // 
            // label332
            // 
            this.label332.AutoSize = true;
            this.label332.Location = new System.Drawing.Point(12, 419);
            this.label332.Name = "label332";
            this.label332.Size = new System.Drawing.Size(53, 13);
            this.label332.TabIndex = 179;
            this.label332.Text = "Bone Mat";
            // 
            // label215
            // 
            this.label215.AutoSize = true;
            this.label215.Location = new System.Drawing.Point(12, 397);
            this.label215.Name = "label215";
            this.label215.Size = new System.Drawing.Size(24, 13);
            this.label215.TabIndex = 175;
            this.label215.Text = "HF:";
            // 
            // label300
            // 
            this.label300.AutoSize = true;
            this.label300.Location = new System.Drawing.Point(12, 374);
            this.label300.Name = "label300";
            this.label300.Size = new System.Drawing.Size(26, 13);
            this.label300.TabIndex = 171;
            this.label300.Text = "Unit";
            // 
            // lblItemShape
            // 
            this.lblItemShape.AutoSize = true;
            this.lblItemShape.Location = new System.Drawing.Point(91, 330);
            this.lblItemShape.Name = "lblItemShape";
            this.lblItemShape.Size = new System.Drawing.Size(35, 13);
            this.lblItemShape.TabIndex = 170;
            this.lblItemShape.Text = "label2";
            // 
            // label302
            // 
            this.label302.AutoSize = true;
            this.label302.Location = new System.Drawing.Point(12, 352);
            this.label302.Name = "label302";
            this.label302.Size = new System.Drawing.Size(50, 13);
            this.label302.TabIndex = 168;
            this.label302.Text = "Dye Mat:";
            // 
            // label305
            // 
            this.label305.AutoSize = true;
            this.label305.Location = new System.Drawing.Point(12, 330);
            this.label305.Name = "label305";
            this.label305.Size = new System.Drawing.Size(41, 13);
            this.label305.TabIndex = 167;
            this.label305.Text = "Shape:";
            // 
            // lblItemHand
            // 
            this.lblItemHand.AutoSize = true;
            this.lblItemHand.Location = new System.Drawing.Point(91, 308);
            this.lblItemHand.Name = "lblItemHand";
            this.lblItemHand.Size = new System.Drawing.Size(35, 13);
            this.lblItemHand.TabIndex = 166;
            this.lblItemHand.Text = "label2";
            // 
            // label310
            // 
            this.label310.AutoSize = true;
            this.label310.Location = new System.Drawing.Point(12, 308);
            this.label310.Name = "label310";
            this.label310.Size = new System.Drawing.Size(36, 13);
            this.label310.TabIndex = 165;
            this.label310.Text = "Hand:";
            // 
            // lblItemWear
            // 
            this.lblItemWear.AutoSize = true;
            this.lblItemWear.Location = new System.Drawing.Point(91, 286);
            this.lblItemWear.Name = "lblItemWear";
            this.lblItemWear.Size = new System.Drawing.Size(35, 13);
            this.lblItemWear.TabIndex = 164;
            this.lblItemWear.Text = "label2";
            // 
            // label313
            // 
            this.label313.AutoSize = true;
            this.label313.Location = new System.Drawing.Point(12, 286);
            this.label313.Name = "label313";
            this.label313.Size = new System.Drawing.Size(36, 13);
            this.label313.TabIndex = 163;
            this.label313.Text = "Wear:";
            // 
            // lblItemAge
            // 
            this.lblItemAge.AutoSize = true;
            this.lblItemAge.Location = new System.Drawing.Point(91, 264);
            this.lblItemAge.Name = "lblItemAge";
            this.lblItemAge.Size = new System.Drawing.Size(35, 13);
            this.lblItemAge.TabIndex = 162;
            this.lblItemAge.Text = "label2";
            // 
            // label316
            // 
            this.label316.AutoSize = true;
            this.label316.Location = new System.Drawing.Point(12, 264);
            this.label316.Name = "label316";
            this.label316.Size = new System.Drawing.Size(29, 13);
            this.label316.TabIndex = 161;
            this.label316.Text = "Age:";
            // 
            // grpItemImage
            // 
            this.grpItemImage.Controls.Add(this.lblItemImageSIte);
            this.grpItemImage.Controls.Add(this.lblItemImageCiv);
            this.grpItemImage.Controls.Add(this.label211);
            this.grpItemImage.Controls.Add(this.label213);
            this.grpItemImage.Controls.Add(this.lblItemImageSubID);
            this.grpItemImage.Controls.Add(this.label216);
            this.grpItemImage.Controls.Add(this.lblItemImageID);
            this.grpItemImage.Controls.Add(this.label228);
            this.grpItemImage.Location = new System.Drawing.Point(199, 304);
            this.grpItemImage.Name = "grpItemImage";
            this.grpItemImage.Size = new System.Drawing.Size(251, 105);
            this.grpItemImage.TabIndex = 160;
            this.grpItemImage.TabStop = false;
            this.grpItemImage.Text = "Image";
            // 
            // label211
            // 
            this.label211.AutoSize = true;
            this.label211.Location = new System.Drawing.Point(6, 82);
            this.label211.Name = "label211";
            this.label211.Size = new System.Drawing.Size(28, 13);
            this.label211.TabIndex = 155;
            this.label211.Text = "Site:";
            // 
            // label213
            // 
            this.label213.AutoSize = true;
            this.label213.Location = new System.Drawing.Point(6, 60);
            this.label213.Name = "label213";
            this.label213.Size = new System.Drawing.Size(25, 13);
            this.label213.TabIndex = 153;
            this.label213.Text = "Civ:";
            // 
            // lblItemImageSubID
            // 
            this.lblItemImageSubID.AutoSize = true;
            this.lblItemImageSubID.Location = new System.Drawing.Point(61, 38);
            this.lblItemImageSubID.Name = "lblItemImageSubID";
            this.lblItemImageSubID.Size = new System.Drawing.Size(35, 13);
            this.lblItemImageSubID.TabIndex = 152;
            this.lblItemImageSubID.Text = "label2";
            // 
            // label216
            // 
            this.label216.AutoSize = true;
            this.label216.Location = new System.Drawing.Point(6, 38);
            this.label216.Name = "label216";
            this.label216.Size = new System.Drawing.Size(40, 13);
            this.label216.TabIndex = 151;
            this.label216.Text = "SubID:";
            // 
            // lblItemImageID
            // 
            this.lblItemImageID.AutoSize = true;
            this.lblItemImageID.Location = new System.Drawing.Point(61, 16);
            this.lblItemImageID.Name = "lblItemImageID";
            this.lblItemImageID.Size = new System.Drawing.Size(35, 13);
            this.lblItemImageID.TabIndex = 150;
            this.lblItemImageID.Text = "label2";
            // 
            // label228
            // 
            this.label228.AutoSize = true;
            this.label228.Location = new System.Drawing.Point(6, 16);
            this.label228.Name = "label228";
            this.label228.Size = new System.Drawing.Size(21, 13);
            this.label228.TabIndex = 149;
            this.label228.Text = "ID:";
            // 
            // grpItemImprovement
            // 
            this.grpItemImprovement.Controls.Add(this.lstItemImprovement);
            this.grpItemImprovement.Location = new System.Drawing.Point(196, 162);
            this.grpItemImprovement.Name = "grpItemImprovement";
            this.grpItemImprovement.Size = new System.Drawing.Size(254, 136);
            this.grpItemImprovement.TabIndex = 158;
            this.grpItemImprovement.TabStop = false;
            this.grpItemImprovement.Text = "Improvements";
            // 
            // lstItemImprovement
            // 
            this.lstItemImprovement.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstItemImprovement.FormattingEnabled = true;
            this.lstItemImprovement.Location = new System.Drawing.Point(3, 16);
            this.lstItemImprovement.Name = "lstItemImprovement";
            this.lstItemImprovement.Size = new System.Drawing.Size(248, 117);
            this.lstItemImprovement.TabIndex = 0;
            // 
            // grpItemIngredient
            // 
            this.grpItemIngredient.Controls.Add(this.lstItemIngredient);
            this.grpItemIngredient.Location = new System.Drawing.Point(196, 19);
            this.grpItemIngredient.Name = "grpItemIngredient";
            this.grpItemIngredient.Size = new System.Drawing.Size(254, 140);
            this.grpItemIngredient.TabIndex = 157;
            this.grpItemIngredient.TabStop = false;
            this.grpItemIngredient.Text = "Ingredients";
            // 
            // lstItemIngredient
            // 
            this.lstItemIngredient.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstItemIngredient.FormattingEnabled = true;
            this.lstItemIngredient.Location = new System.Drawing.Point(3, 16);
            this.lstItemIngredient.Name = "lstItemIngredient";
            this.lstItemIngredient.Size = new System.Drawing.Size(248, 121);
            this.lstItemIngredient.TabIndex = 0;
            // 
            // lblItemSkill
            // 
            this.lblItemSkill.AutoSize = true;
            this.lblItemSkill.Location = new System.Drawing.Point(91, 219);
            this.lblItemSkill.Name = "lblItemSkill";
            this.lblItemSkill.Size = new System.Drawing.Size(35, 13);
            this.lblItemSkill.TabIndex = 154;
            this.lblItemSkill.Text = "label2";
            // 
            // label251
            // 
            this.label251.AutoSize = true;
            this.label251.Location = new System.Drawing.Point(12, 219);
            this.label251.Name = "label251";
            this.label251.Size = new System.Drawing.Size(29, 13);
            this.label251.TabIndex = 153;
            this.label251.Text = "Skill:";
            // 
            // lblItemSubType
            // 
            this.lblItemSubType.AutoSize = true;
            this.lblItemSubType.Location = new System.Drawing.Point(91, 175);
            this.lblItemSubType.Name = "lblItemSubType";
            this.lblItemSubType.Size = new System.Drawing.Size(35, 13);
            this.lblItemSubType.TabIndex = 152;
            this.lblItemSubType.Text = "label2";
            // 
            // label245
            // 
            this.label245.AutoSize = true;
            this.label245.Location = new System.Drawing.Point(12, 175);
            this.label245.Name = "label245";
            this.label245.Size = new System.Drawing.Size(53, 13);
            this.label245.TabIndex = 151;
            this.label245.Text = "SubType:";
            // 
            // lblItemType
            // 
            this.lblItemType.AutoSize = true;
            this.lblItemType.Location = new System.Drawing.Point(91, 153);
            this.lblItemType.Name = "lblItemType";
            this.lblItemType.Size = new System.Drawing.Size(35, 13);
            this.lblItemType.TabIndex = 150;
            this.lblItemType.Text = "label2";
            // 
            // label222
            // 
            this.label222.AutoSize = true;
            this.label222.Location = new System.Drawing.Point(12, 153);
            this.label222.Name = "label222";
            this.label222.Size = new System.Drawing.Size(34, 13);
            this.label222.TabIndex = 149;
            this.label222.Text = "Type:";
            // 
            // lblItemQuality
            // 
            this.lblItemQuality.AutoSize = true;
            this.lblItemQuality.Location = new System.Drawing.Point(91, 109);
            this.lblItemQuality.Name = "lblItemQuality";
            this.lblItemQuality.Size = new System.Drawing.Size(35, 13);
            this.lblItemQuality.TabIndex = 148;
            this.lblItemQuality.Text = "label2";
            // 
            // label256
            // 
            this.label256.AutoSize = true;
            this.label256.Location = new System.Drawing.Point(12, 131);
            this.label256.Name = "label256";
            this.label256.Size = new System.Drawing.Size(40, 13);
            this.label256.TabIndex = 142;
            this.label256.Text = "Maker:";
            // 
            // label268
            // 
            this.label268.AutoSize = true;
            this.label268.Location = new System.Drawing.Point(12, 109);
            this.label268.Name = "label268";
            this.label268.Size = new System.Drawing.Size(42, 13);
            this.label268.TabIndex = 141;
            this.label268.Text = "Quality:";
            // 
            // lblItemMat
            // 
            this.lblItemMat.AutoSize = true;
            this.lblItemMat.Location = new System.Drawing.Point(91, 87);
            this.lblItemMat.Name = "lblItemMat";
            this.lblItemMat.Size = new System.Drawing.Size(35, 13);
            this.lblItemMat.TabIndex = 140;
            this.lblItemMat.Text = "label2";
            // 
            // label277
            // 
            this.label277.AutoSize = true;
            this.label277.Location = new System.Drawing.Point(12, 87);
            this.label277.Name = "label277";
            this.label277.Size = new System.Drawing.Size(28, 13);
            this.label277.TabIndex = 139;
            this.label277.Text = "Mat:";
            // 
            // lblItemDescription
            // 
            this.lblItemDescription.AutoSize = true;
            this.lblItemDescription.Location = new System.Drawing.Point(91, 43);
            this.lblItemDescription.Name = "lblItemDescription";
            this.lblItemDescription.Size = new System.Drawing.Size(35, 13);
            this.lblItemDescription.TabIndex = 138;
            this.lblItemDescription.Text = "label2";
            // 
            // label293
            // 
            this.label293.AutoSize = true;
            this.label293.Location = new System.Drawing.Point(12, 43);
            this.label293.Name = "label293";
            this.label293.Size = new System.Drawing.Size(46, 13);
            this.label293.TabIndex = 137;
            this.label293.Text = "Descrip:";
            // 
            // lblItemName
            // 
            this.lblItemName.AutoSize = true;
            this.lblItemName.Location = new System.Drawing.Point(91, 21);
            this.lblItemName.Name = "lblItemName";
            this.lblItemName.Size = new System.Drawing.Size(35, 13);
            this.lblItemName.TabIndex = 20;
            this.lblItemName.Text = "label2";
            // 
            // label225
            // 
            this.label225.AutoSize = true;
            this.label225.Location = new System.Drawing.Point(12, 21);
            this.label225.Name = "label225";
            this.label225.Size = new System.Drawing.Size(41, 13);
            this.label225.TabIndex = 19;
            this.label225.Text = "Name: ";
            // 
            // lstItem
            // 
            this.lstItem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstItem.FormattingEnabled = true;
            this.lstItem.Location = new System.Drawing.Point(3, 3);
            this.lstItem.Name = "lstItem";
            this.lstItem.Size = new System.Drawing.Size(207, 815);
            this.lstItem.TabIndex = 2;
            // 
            // tabPlant
            // 
            this.tabPlant.Controls.Add(this.tableLayoutPanel55);
            this.tabPlant.Location = new System.Drawing.Point(4, 40);
            this.tabPlant.Name = "tabPlant";
            this.tabPlant.Size = new System.Drawing.Size(1873, 873);
            this.tabPlant.TabIndex = 34;
            this.tabPlant.Text = "Plant";
            this.tabPlant.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel55
            // 
            this.tableLayoutPanel55.ColumnCount = 2;
            this.tableLayoutPanel55.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 213F));
            this.tableLayoutPanel55.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel55.Controls.Add(this.FilterPlant, 0, 2);
            this.tableLayoutPanel55.Controls.Add(this.TextFilterPlant, 0, 1);
            this.tableLayoutPanel55.Controls.Add(this.grpPlant, 1, 0);
            this.tableLayoutPanel55.Controls.Add(this.lstPlant, 0, 0);
            this.tableLayoutPanel55.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel55.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel55.Margin = new System.Windows.Forms.Padding(2);
            this.tableLayoutPanel55.Name = "tableLayoutPanel55";
            this.tableLayoutPanel55.RowCount = 3;
            this.tableLayoutPanel55.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel55.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel55.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanel55.Size = new System.Drawing.Size(1873, 873);
            this.tableLayoutPanel55.TabIndex = 8;
            // 
            // FilterPlant
            // 
            this.FilterPlant.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FilterPlant.Location = new System.Drawing.Point(3, 848);
            this.FilterPlant.Name = "FilterPlant";
            this.FilterPlant.Size = new System.Drawing.Size(207, 22);
            this.FilterPlant.TabIndex = 6;
            this.FilterPlant.Tag = "";
            this.FilterPlant.Text = "Filter";
            this.FilterPlant.UseVisualStyleBackColor = true;
            this.FilterPlant.Click += new System.EventHandler(this.FilterButton_Click);
            // 
            // TextFilterPlant
            // 
            this.TextFilterPlant.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TextFilterPlant.Location = new System.Drawing.Point(3, 824);
            this.TextFilterPlant.Name = "TextFilterPlant";
            this.TextFilterPlant.Size = new System.Drawing.Size(207, 20);
            this.TextFilterPlant.TabIndex = 5;
            this.TextFilterPlant.TextChanged += new System.EventHandler(this.TextFilter_Changed);
            // 
            // grpPlant
            // 
            this.grpPlant.Controls.Add(this.lblPlantMat);
            this.grpPlant.Controls.Add(this.label224);
            this.grpPlant.Controls.Add(this.lblPlantCoords);
            this.grpPlant.Controls.Add(this.label283);
            this.grpPlant.Controls.Add(this.lblPlantName);
            this.grpPlant.Controls.Add(this.label227);
            this.grpPlant.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpPlant.Location = new System.Drawing.Point(216, 3);
            this.grpPlant.Name = "grpPlant";
            this.tableLayoutPanel55.SetRowSpan(this.grpPlant, 3);
            this.grpPlant.Size = new System.Drawing.Size(1654, 867);
            this.grpPlant.TabIndex = 3;
            this.grpPlant.TabStop = false;
            this.grpPlant.Visible = false;
            // 
            // lblPlantMat
            // 
            this.lblPlantMat.AutoSize = true;
            this.lblPlantMat.Location = new System.Drawing.Point(67, 66);
            this.lblPlantMat.Name = "lblPlantMat";
            this.lblPlantMat.Size = new System.Drawing.Size(35, 13);
            this.lblPlantMat.TabIndex = 144;
            this.lblPlantMat.Text = "label2";
            // 
            // label224
            // 
            this.label224.AutoSize = true;
            this.label224.Location = new System.Drawing.Point(12, 66);
            this.label224.Name = "label224";
            this.label224.Size = new System.Drawing.Size(28, 13);
            this.label224.TabIndex = 143;
            this.label224.Text = "Mat:";
            // 
            // lblPlantCoords
            // 
            this.lblPlantCoords.AutoSize = true;
            this.lblPlantCoords.Location = new System.Drawing.Point(67, 44);
            this.lblPlantCoords.Name = "lblPlantCoords";
            this.lblPlantCoords.Size = new System.Drawing.Size(35, 13);
            this.lblPlantCoords.TabIndex = 142;
            this.lblPlantCoords.Text = "label2";
            // 
            // label283
            // 
            this.label283.AutoSize = true;
            this.label283.Location = new System.Drawing.Point(12, 44);
            this.label283.Name = "label283";
            this.label283.Size = new System.Drawing.Size(43, 13);
            this.label283.TabIndex = 141;
            this.label283.Text = "Coords:";
            // 
            // lblPlantName
            // 
            this.lblPlantName.AutoSize = true;
            this.lblPlantName.Location = new System.Drawing.Point(67, 21);
            this.lblPlantName.Name = "lblPlantName";
            this.lblPlantName.Size = new System.Drawing.Size(35, 13);
            this.lblPlantName.TabIndex = 20;
            this.lblPlantName.Text = "label2";
            // 
            // label227
            // 
            this.label227.AutoSize = true;
            this.label227.Location = new System.Drawing.Point(12, 21);
            this.label227.Name = "label227";
            this.label227.Size = new System.Drawing.Size(41, 13);
            this.label227.TabIndex = 19;
            this.label227.Text = "Name: ";
            // 
            // lstPlant
            // 
            this.lstPlant.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstPlant.FormattingEnabled = true;
            this.lstPlant.Location = new System.Drawing.Point(3, 3);
            this.lstPlant.Name = "lstPlant";
            this.lstPlant.Size = new System.Drawing.Size(207, 815);
            this.lstPlant.TabIndex = 2;
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadWorldToolStripMenuItem,
            this.exportWorldToolStripMenuItem,
            this.showMapToolStripMenuItem,
            this.timelineToolStripMenuItem,
            this.statsToolStripMenuItem,
            this.visualizationsToolStripMenuItem,
            this.closeWorldToolStripMenuItem,
            this.BacktoolStripMenuItem,
            this.ForwardtoolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
            this.menuStrip.ShowItemToolTips = true;
            this.menuStrip.Size = new System.Drawing.Size(1904, 24);
            this.menuStrip.TabIndex = 2;
            this.menuStrip.Text = "menuStrip1";
            // 
            // loadWorldToolStripMenuItem
            // 
            this.loadWorldToolStripMenuItem.Name = "loadWorldToolStripMenuItem";
            this.loadWorldToolStripMenuItem.Size = new System.Drawing.Size(80, 20);
            this.loadWorldToolStripMenuItem.Text = "&Load World";
            this.loadWorldToolStripMenuItem.Click += new System.EventHandler(this.loadWorldToolStripMenuItem_Click);
            // 
            // exportWorldToolStripMenuItem
            // 
            this.exportWorldToolStripMenuItem.Name = "exportWorldToolStripMenuItem";
            this.exportWorldToolStripMenuItem.Size = new System.Drawing.Size(87, 20);
            this.exportWorldToolStripMenuItem.Text = "E&xport World";
            this.exportWorldToolStripMenuItem.Visible = false;
            this.exportWorldToolStripMenuItem.Click += new System.EventHandler(this.exportWorldToolStripMenuItem_Click);
            // 
            // showMapToolStripMenuItem
            // 
            this.showMapToolStripMenuItem.Name = "showMapToolStripMenuItem";
            this.showMapToolStripMenuItem.Size = new System.Drawing.Size(75, 20);
            this.showMapToolStripMenuItem.Text = "Show &Map";
            this.showMapToolStripMenuItem.Visible = false;
            this.showMapToolStripMenuItem.Click += new System.EventHandler(this.showMapToolStripMenuItem_Click);
            // 
            // timelineToolStripMenuItem
            // 
            this.timelineToolStripMenuItem.Name = "timelineToolStripMenuItem";
            this.timelineToolStripMenuItem.Size = new System.Drawing.Size(65, 20);
            this.timelineToolStripMenuItem.Text = "&Timeline";
            this.timelineToolStripMenuItem.Visible = false;
            this.timelineToolStripMenuItem.Click += new System.EventHandler(this.timelinetoolStripMenuItem_Click);
            // 
            // statsToolStripMenuItem
            // 
            this.statsToolStripMenuItem.Name = "statsToolStripMenuItem";
            this.statsToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.statsToolStripMenuItem.Text = "&Stats";
            this.statsToolStripMenuItem.Visible = false;
            this.statsToolStripMenuItem.Click += new System.EventHandler(this.statsToolStripMenuItem_Click);
            // 
            // visualizationsToolStripMenuItem
            // 
            this.visualizationsToolStripMenuItem.Name = "visualizationsToolStripMenuItem";
            this.visualizationsToolStripMenuItem.Size = new System.Drawing.Size(90, 20);
            this.visualizationsToolStripMenuItem.Text = "&Visualizations";
            this.visualizationsToolStripMenuItem.Visible = false;
            // 
            // closeWorldToolStripMenuItem
            // 
            this.closeWorldToolStripMenuItem.Name = "closeWorldToolStripMenuItem";
            this.closeWorldToolStripMenuItem.Size = new System.Drawing.Size(83, 20);
            this.closeWorldToolStripMenuItem.Text = "Close &World";
            this.closeWorldToolStripMenuItem.Visible = false;
            this.closeWorldToolStripMenuItem.Click += new System.EventHandler(this.closeWorldToolStripMenuItem_Click);
            // 
            // BacktoolStripMenuItem
            // 
            this.BacktoolStripMenuItem.Enabled = false;
            this.BacktoolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BacktoolStripMenuItem.Name = "BacktoolStripMenuItem";
            this.BacktoolStripMenuItem.ShowShortcutKeys = false;
            this.BacktoolStripMenuItem.Size = new System.Drawing.Size(12, 20);
            this.BacktoolStripMenuItem.Click += new System.EventHandler(this.BacktoolStripMenuItem_Click);
            // 
            // ForwardtoolStripMenuItem
            // 
            this.ForwardtoolStripMenuItem.Enabled = false;
            this.ForwardtoolStripMenuItem.Image = global::DFWV.Properties.Resources.RightArrow;
            this.ForwardtoolStripMenuItem.Name = "ForwardtoolStripMenuItem";
            this.ForwardtoolStripMenuItem.ShowShortcutKeys = false;
            this.ForwardtoolStripMenuItem.Size = new System.Drawing.Size(28, 20);
            this.ForwardtoolStripMenuItem.Click += new System.EventHandler(this.ForwardtoolStripMenuItem_Click);
            // 
            // TestButton
            // 
            this.TestButton.Location = new System.Drawing.Point(702, 32);
            this.TestButton.Name = "TestButton";
            this.TestButton.Size = new System.Drawing.Size(75, 23);
            this.TestButton.TabIndex = 3;
            this.TestButton.Text = "Test";
            this.TestButton.UseVisualStyleBackColor = true;
            this.TestButton.Click += new System.EventHandler(this.TestButton_Click);
            // 
            // lblArtifactWC
            // 
            this.lblArtifactWC.AutoSize = true;
            this.lblArtifactWC.Data = null;
            this.lblArtifactWC.ForeColor = System.Drawing.Color.Black;
            this.lblArtifactWC.Location = new System.Drawing.Point(57, 93);
            this.lblArtifactWC.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblArtifactWC.Name = "lblArtifactWC";
            this.lblArtifactWC.Size = new System.Drawing.Size(55, 13);
            this.lblArtifactWC.TabIndex = 23;
            this.lblArtifactWC.Text = "linkLabel1";
            // 
            // lblArtifactLostTime
            // 
            this.lblArtifactLostTime.AutoSize = true;
            this.lblArtifactLostTime.Data = null;
            this.lblArtifactLostTime.ForeColor = System.Drawing.Color.Black;
            this.lblArtifactLostTime.Location = new System.Drawing.Point(28, 37);
            this.lblArtifactLostTime.Name = "lblArtifactLostTime";
            this.lblArtifactLostTime.Size = new System.Drawing.Size(55, 13);
            this.lblArtifactLostTime.TabIndex = 13;
            this.lblArtifactLostTime.Text = "linkLabel1";
            // 
            // lblArtifactLostSite
            // 
            this.lblArtifactLostSite.AutoSize = true;
            this.lblArtifactLostSite.Data = null;
            this.lblArtifactLostSite.ForeColor = System.Drawing.Color.Black;
            this.lblArtifactLostSite.Location = new System.Drawing.Point(28, 16);
            this.lblArtifactLostSite.Name = "lblArtifactLostSite";
            this.lblArtifactLostSite.Size = new System.Drawing.Size(55, 13);
            this.lblArtifactLostSite.TabIndex = 12;
            this.lblArtifactLostSite.Text = "linkLabel3";
            // 
            // lblArtifactCreatedTime
            // 
            this.lblArtifactCreatedTime.AutoSize = true;
            this.lblArtifactCreatedTime.Data = null;
            this.lblArtifactCreatedTime.ForeColor = System.Drawing.Color.Black;
            this.lblArtifactCreatedTime.Location = new System.Drawing.Point(28, 64);
            this.lblArtifactCreatedTime.Name = "lblArtifactCreatedTime";
            this.lblArtifactCreatedTime.Size = new System.Drawing.Size(55, 13);
            this.lblArtifactCreatedTime.TabIndex = 7;
            this.lblArtifactCreatedTime.Text = "linkLabel1";
            // 
            // lblArtifactCreatedSite
            // 
            this.lblArtifactCreatedSite.AutoSize = true;
            this.lblArtifactCreatedSite.Data = null;
            this.lblArtifactCreatedSite.ForeColor = System.Drawing.Color.Black;
            this.lblArtifactCreatedSite.Location = new System.Drawing.Point(28, 43);
            this.lblArtifactCreatedSite.Name = "lblArtifactCreatedSite";
            this.lblArtifactCreatedSite.Size = new System.Drawing.Size(55, 13);
            this.lblArtifactCreatedSite.TabIndex = 6;
            this.lblArtifactCreatedSite.Text = "linkLabel3";
            // 
            // lblArtifactCreatedBy
            // 
            this.lblArtifactCreatedBy.AutoSize = true;
            this.lblArtifactCreatedBy.Data = null;
            this.lblArtifactCreatedBy.ForeColor = System.Drawing.Color.Black;
            this.lblArtifactCreatedBy.Location = new System.Drawing.Point(28, 21);
            this.lblArtifactCreatedBy.Name = "lblArtifactCreatedBy";
            this.lblArtifactCreatedBy.Size = new System.Drawing.Size(55, 13);
            this.lblArtifactCreatedBy.TabIndex = 4;
            this.lblArtifactCreatedBy.Text = "linkLabel1";
            // 
            // lblCivilizationRace
            // 
            this.lblCivilizationRace.AutoSize = true;
            this.lblCivilizationRace.Data = null;
            this.lblCivilizationRace.ForeColor = System.Drawing.Color.Black;
            this.lblCivilizationRace.Location = new System.Drawing.Point(58, 44);
            this.lblCivilizationRace.Name = "lblCivilizationRace";
            this.lblCivilizationRace.Size = new System.Drawing.Size(55, 13);
            this.lblCivilizationRace.TabIndex = 14;
            this.lblCivilizationRace.Text = "linkLabel1";
            // 
            // lblCivilizationEntity
            // 
            this.lblCivilizationEntity.AutoSize = true;
            this.lblCivilizationEntity.Data = null;
            this.lblCivilizationEntity.ForeColor = System.Drawing.Color.Black;
            this.lblCivilizationEntity.Location = new System.Drawing.Point(58, 66);
            this.lblCivilizationEntity.Name = "lblCivilizationEntity";
            this.lblCivilizationEntity.Size = new System.Drawing.Size(55, 13);
            this.lblCivilizationEntity.TabIndex = 10;
            this.lblCivilizationEntity.Text = "linkLabel1";
            // 
            // lblEntityWorshippingHF
            // 
            this.lblEntityWorshippingHF.AutoSize = true;
            this.lblEntityWorshippingHF.Data = null;
            this.lblEntityWorshippingHF.ForeColor = System.Drawing.Color.Black;
            this.lblEntityWorshippingHF.Location = new System.Drawing.Point(68, 115);
            this.lblEntityWorshippingHF.Name = "lblEntityWorshippingHF";
            this.lblEntityWorshippingHF.Size = new System.Drawing.Size(55, 13);
            this.lblEntityWorshippingHF.TabIndex = 29;
            this.lblEntityWorshippingHF.Text = "linkLabel1";
            // 
            // lblEntityRace
            // 
            this.lblEntityRace.AutoSize = true;
            this.lblEntityRace.Data = null;
            this.lblEntityRace.ForeColor = System.Drawing.Color.Black;
            this.lblEntityRace.Location = new System.Drawing.Point(69, 45);
            this.lblEntityRace.Name = "lblEntityRace";
            this.lblEntityRace.Size = new System.Drawing.Size(55, 13);
            this.lblEntityRace.TabIndex = 18;
            this.lblEntityRace.Text = "linkLabel1";
            // 
            // lblEntityCivilization
            // 
            this.lblEntityCivilization.AutoSize = true;
            this.lblEntityCivilization.Data = null;
            this.lblEntityCivilization.ForeColor = System.Drawing.Color.Black;
            this.lblEntityCivilization.Location = new System.Drawing.Point(69, 68);
            this.lblEntityCivilization.Name = "lblEntityCivilization";
            this.lblEntityCivilization.Size = new System.Drawing.Size(55, 13);
            this.lblEntityCivilization.TabIndex = 20;
            this.lblEntityCivilization.Text = "linkLabel1";
            // 
            // lblEntityParentCiv
            // 
            this.lblEntityParentCiv.AutoSize = true;
            this.lblEntityParentCiv.Data = null;
            this.lblEntityParentCiv.ForeColor = System.Drawing.Color.Black;
            this.lblEntityParentCiv.Location = new System.Drawing.Point(69, 93);
            this.lblEntityParentCiv.Name = "lblEntityParentCiv";
            this.lblEntityParentCiv.Size = new System.Drawing.Size(55, 13);
            this.lblEntityParentCiv.TabIndex = 24;
            this.lblEntityParentCiv.Text = "linkLabel1";
            // 
            // lblEntitySiteTakeoverTime
            // 
            this.lblEntitySiteTakeoverTime.AutoSize = true;
            this.lblEntitySiteTakeoverTime.Data = null;
            this.lblEntitySiteTakeoverTime.ForeColor = System.Drawing.Color.Black;
            this.lblEntitySiteTakeoverTime.Location = new System.Drawing.Point(88, 100);
            this.lblEntitySiteTakeoverTime.Name = "lblEntitySiteTakeoverTime";
            this.lblEntitySiteTakeoverTime.Size = new System.Drawing.Size(55, 13);
            this.lblEntitySiteTakeoverTime.TabIndex = 15;
            this.lblEntitySiteTakeoverTime.Text = "linkLabel5";
            // 
            // lblEntitySiteTakeoverNewLeader
            // 
            this.lblEntitySiteTakeoverNewLeader.AutoSize = true;
            this.lblEntitySiteTakeoverNewLeader.Data = null;
            this.lblEntitySiteTakeoverNewLeader.ForeColor = System.Drawing.Color.Black;
            this.lblEntitySiteTakeoverNewLeader.Location = new System.Drawing.Point(88, 79);
            this.lblEntitySiteTakeoverNewLeader.Name = "lblEntitySiteTakeoverNewLeader";
            this.lblEntitySiteTakeoverNewLeader.Size = new System.Drawing.Size(55, 13);
            this.lblEntitySiteTakeoverNewLeader.TabIndex = 14;
            this.lblEntitySiteTakeoverNewLeader.Text = "linkLabel3";
            // 
            // lblEntitySiteTakeoverDefenderEntity
            // 
            this.lblEntitySiteTakeoverDefenderEntity.AutoSize = true;
            this.lblEntitySiteTakeoverDefenderEntity.Data = null;
            this.lblEntitySiteTakeoverDefenderEntity.ForeColor = System.Drawing.Color.Black;
            this.lblEntitySiteTakeoverDefenderEntity.Location = new System.Drawing.Point(88, 58);
            this.lblEntitySiteTakeoverDefenderEntity.Name = "lblEntitySiteTakeoverDefenderEntity";
            this.lblEntitySiteTakeoverDefenderEntity.Size = new System.Drawing.Size(55, 13);
            this.lblEntitySiteTakeoverDefenderEntity.TabIndex = 11;
            this.lblEntitySiteTakeoverDefenderEntity.Text = "linkLabel3";
            // 
            // lblEntitySiteTakeoverDefenderCiv
            // 
            this.lblEntitySiteTakeoverDefenderCiv.AutoSize = true;
            this.lblEntitySiteTakeoverDefenderCiv.Data = null;
            this.lblEntitySiteTakeoverDefenderCiv.ForeColor = System.Drawing.Color.Black;
            this.lblEntitySiteTakeoverDefenderCiv.Location = new System.Drawing.Point(88, 37);
            this.lblEntitySiteTakeoverDefenderCiv.Name = "lblEntitySiteTakeoverDefenderCiv";
            this.lblEntitySiteTakeoverDefenderCiv.Size = new System.Drawing.Size(55, 13);
            this.lblEntitySiteTakeoverDefenderCiv.TabIndex = 10;
            this.lblEntitySiteTakeoverDefenderCiv.Text = "linkLabel3";
            // 
            // lblEntitySiteTakeoverSite
            // 
            this.lblEntitySiteTakeoverSite.AutoSize = true;
            this.lblEntitySiteTakeoverSite.Data = null;
            this.lblEntitySiteTakeoverSite.ForeColor = System.Drawing.Color.Black;
            this.lblEntitySiteTakeoverSite.Location = new System.Drawing.Point(88, 15);
            this.lblEntitySiteTakeoverSite.Name = "lblEntitySiteTakeoverSite";
            this.lblEntitySiteTakeoverSite.Size = new System.Drawing.Size(55, 13);
            this.lblEntitySiteTakeoverSite.TabIndex = 7;
            this.lblEntitySiteTakeoverSite.Text = "linkLabel1";
            // 
            // lblEntityCreatedTime
            // 
            this.lblEntityCreatedTime.AutoSize = true;
            this.lblEntityCreatedTime.Data = null;
            this.lblEntityCreatedTime.ForeColor = System.Drawing.Color.Black;
            this.lblEntityCreatedTime.Location = new System.Drawing.Point(28, 37);
            this.lblEntityCreatedTime.Name = "lblEntityCreatedTime";
            this.lblEntityCreatedTime.Size = new System.Drawing.Size(55, 13);
            this.lblEntityCreatedTime.TabIndex = 7;
            this.lblEntityCreatedTime.Text = "linkLabel1";
            // 
            // lblEntityCreatedSite
            // 
            this.lblEntityCreatedSite.AutoSize = true;
            this.lblEntityCreatedSite.Data = null;
            this.lblEntityCreatedSite.ForeColor = System.Drawing.Color.Black;
            this.lblEntityCreatedSite.Location = new System.Drawing.Point(28, 16);
            this.lblEntityCreatedSite.Name = "lblEntityCreatedSite";
            this.lblEntityCreatedSite.Size = new System.Drawing.Size(55, 13);
            this.lblEntityCreatedSite.TabIndex = 6;
            this.lblEntityCreatedSite.Text = "linkLabel3";
            // 
            // lblEntityPopulationCiv
            // 
            this.lblEntityPopulationCiv.AutoSize = true;
            this.lblEntityPopulationCiv.Data = null;
            this.lblEntityPopulationCiv.ForeColor = System.Drawing.Color.Black;
            this.lblEntityPopulationCiv.Location = new System.Drawing.Point(43, 30);
            this.lblEntityPopulationCiv.Name = "lblEntityPopulationCiv";
            this.lblEntityPopulationCiv.Size = new System.Drawing.Size(55, 13);
            this.lblEntityPopulationCiv.TabIndex = 21;
            this.lblEntityPopulationCiv.Text = "linkLabel1";
            // 
            // lblEntityPopulationRace
            // 
            this.lblEntityPopulationRace.AutoSize = true;
            this.lblEntityPopulationRace.Data = null;
            this.lblEntityPopulationRace.ForeColor = System.Drawing.Color.Black;
            this.lblEntityPopulationRace.Location = new System.Drawing.Point(43, 16);
            this.lblEntityPopulationRace.Name = "lblEntityPopulationRace";
            this.lblEntityPopulationRace.Size = new System.Drawing.Size(55, 13);
            this.lblEntityPopulationRace.TabIndex = 19;
            this.lblEntityPopulationRace.Text = "linkLabel1";
            // 
            // lblEntityPopulationBattleTime
            // 
            this.lblEntityPopulationBattleTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblEntityPopulationBattleTime.AutoSize = true;
            this.lblEntityPopulationBattleTime.Data = null;
            this.lblEntityPopulationBattleTime.ForeColor = System.Drawing.Color.Black;
            this.lblEntityPopulationBattleTime.Location = new System.Drawing.Point(70, 809);
            this.lblEntityPopulationBattleTime.Name = "lblEntityPopulationBattleTime";
            this.lblEntityPopulationBattleTime.Size = new System.Drawing.Size(55, 13);
            this.lblEntityPopulationBattleTime.TabIndex = 29;
            this.lblEntityPopulationBattleTime.Text = "linkLabel5";
            // 
            // lblEntityPopulationBattleWar
            // 
            this.lblEntityPopulationBattleWar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblEntityPopulationBattleWar.AutoSize = true;
            this.lblEntityPopulationBattleWar.Data = null;
            this.lblEntityPopulationBattleWar.ForeColor = System.Drawing.Color.Black;
            this.lblEntityPopulationBattleWar.Location = new System.Drawing.Point(70, 786);
            this.lblEntityPopulationBattleWar.Name = "lblEntityPopulationBattleWar";
            this.lblEntityPopulationBattleWar.Size = new System.Drawing.Size(55, 13);
            this.lblEntityPopulationBattleWar.TabIndex = 25;
            this.lblEntityPopulationBattleWar.Text = "linkLabel5";
            // 
            // lblGodHF
            // 
            this.lblGodHF.AutoSize = true;
            this.lblGodHF.Data = null;
            this.lblGodHF.ForeColor = System.Drawing.Color.Black;
            this.lblGodHF.Location = new System.Drawing.Point(58, 43);
            this.lblGodHF.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblGodHF.Name = "lblGodHF";
            this.lblGodHF.Size = new System.Drawing.Size(55, 13);
            this.lblGodHF.TabIndex = 22;
            this.lblGodHF.Text = "linkLabel1";
            // 
            // lblHistoricalFigureEntityPopulation
            // 
            this.lblHistoricalFigureEntityPopulation.AutoSize = true;
            this.lblHistoricalFigureEntityPopulation.Data = null;
            this.lblHistoricalFigureEntityPopulation.ForeColor = System.Drawing.Color.Black;
            this.lblHistoricalFigureEntityPopulation.Location = new System.Drawing.Point(92, 328);
            this.lblHistoricalFigureEntityPopulation.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblHistoricalFigureEntityPopulation.Name = "lblHistoricalFigureEntityPopulation";
            this.lblHistoricalFigureEntityPopulation.Size = new System.Drawing.Size(55, 13);
            this.lblHistoricalFigureEntityPopulation.TabIndex = 71;
            this.lblHistoricalFigureEntityPopulation.Text = "linkLabel5";
            // 
            // lblHistoricalFigureCoords
            // 
            this.lblHistoricalFigureCoords.AutoSize = true;
            this.lblHistoricalFigureCoords.Data = null;
            this.lblHistoricalFigureCoords.ForeColor = System.Drawing.Color.Black;
            this.lblHistoricalFigureCoords.Location = new System.Drawing.Point(92, 238);
            this.lblHistoricalFigureCoords.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblHistoricalFigureCoords.Name = "lblHistoricalFigureCoords";
            this.lblHistoricalFigureCoords.Size = new System.Drawing.Size(55, 13);
            this.lblHistoricalFigureCoords.TabIndex = 69;
            this.lblHistoricalFigureCoords.Text = "linkLabel3";
            // 
            // lblHistoricalFigureLocation
            // 
            this.lblHistoricalFigureLocation.AutoSize = true;
            this.lblHistoricalFigureLocation.Data = null;
            this.lblHistoricalFigureLocation.ForeColor = System.Drawing.Color.Black;
            this.lblHistoricalFigureLocation.Location = new System.Drawing.Point(92, 216);
            this.lblHistoricalFigureLocation.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblHistoricalFigureLocation.Name = "lblHistoricalFigureLocation";
            this.lblHistoricalFigureLocation.Size = new System.Drawing.Size(55, 13);
            this.lblHistoricalFigureLocation.TabIndex = 60;
            this.lblHistoricalFigureLocation.Text = "linkLabel3";
            // 
            // lblHistoricalFigureRace
            // 
            this.lblHistoricalFigureRace.AutoSize = true;
            this.lblHistoricalFigureRace.Data = null;
            this.lblHistoricalFigureRace.ForeColor = System.Drawing.Color.Black;
            this.lblHistoricalFigureRace.Location = new System.Drawing.Point(92, 22);
            this.lblHistoricalFigureRace.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblHistoricalFigureRace.Name = "lblHistoricalFigureRace";
            this.lblHistoricalFigureRace.Size = new System.Drawing.Size(55, 13);
            this.lblHistoricalFigureRace.TabIndex = 55;
            this.lblHistoricalFigureRace.Text = "linkLabel3";
            // 
            // lblHistoricalFigureUnit
            // 
            this.lblHistoricalFigureUnit.AutoSize = true;
            this.lblHistoricalFigureUnit.Data = null;
            this.lblHistoricalFigureUnit.ForeColor = System.Drawing.Color.Black;
            this.lblHistoricalFigureUnit.Location = new System.Drawing.Point(92, 306);
            this.lblHistoricalFigureUnit.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblHistoricalFigureUnit.Name = "lblHistoricalFigureUnit";
            this.lblHistoricalFigureUnit.Size = new System.Drawing.Size(55, 13);
            this.lblHistoricalFigureUnit.TabIndex = 53;
            this.lblHistoricalFigureUnit.Text = "linkLabel5";
            // 
            // lblHistoricalFigureGod
            // 
            this.lblHistoricalFigureGod.AutoSize = true;
            this.lblHistoricalFigureGod.Data = null;
            this.lblHistoricalFigureGod.ForeColor = System.Drawing.Color.Black;
            this.lblHistoricalFigureGod.Location = new System.Drawing.Point(92, 262);
            this.lblHistoricalFigureGod.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblHistoricalFigureGod.Name = "lblHistoricalFigureGod";
            this.lblHistoricalFigureGod.Size = new System.Drawing.Size(55, 13);
            this.lblHistoricalFigureGod.TabIndex = 51;
            this.lblHistoricalFigureGod.Text = "linkLabel3";
            // 
            // lblHistoricalFigureLeader
            // 
            this.lblHistoricalFigureLeader.AutoSize = true;
            this.lblHistoricalFigureLeader.Data = null;
            this.lblHistoricalFigureLeader.ForeColor = System.Drawing.Color.Black;
            this.lblHistoricalFigureLeader.Location = new System.Drawing.Point(92, 284);
            this.lblHistoricalFigureLeader.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblHistoricalFigureLeader.Name = "lblHistoricalFigureLeader";
            this.lblHistoricalFigureLeader.Size = new System.Drawing.Size(55, 13);
            this.lblHistoricalFigureLeader.TabIndex = 50;
            this.lblHistoricalFigureLeader.Text = "linkLabel2";
            // 
            // lblHistoricalFigureDeathTime
            // 
            this.lblHistoricalFigureDeathTime.AutoSize = true;
            this.lblHistoricalFigureDeathTime.Data = null;
            this.lblHistoricalFigureDeathTime.ForeColor = System.Drawing.Color.Black;
            this.lblHistoricalFigureDeathTime.Location = new System.Drawing.Point(47, 86);
            this.lblHistoricalFigureDeathTime.Name = "lblHistoricalFigureDeathTime";
            this.lblHistoricalFigureDeathTime.Size = new System.Drawing.Size(55, 13);
            this.lblHistoricalFigureDeathTime.TabIndex = 9;
            this.lblHistoricalFigureDeathTime.Text = "linkLabel2";
            // 
            // lblHistoricalFigureDeathLocation
            // 
            this.lblHistoricalFigureDeathLocation.AutoSize = true;
            this.lblHistoricalFigureDeathLocation.Data = null;
            this.lblHistoricalFigureDeathLocation.ForeColor = System.Drawing.Color.Black;
            this.lblHistoricalFigureDeathLocation.Location = new System.Drawing.Point(47, 43);
            this.lblHistoricalFigureDeathLocation.Name = "lblHistoricalFigureDeathLocation";
            this.lblHistoricalFigureDeathLocation.Size = new System.Drawing.Size(55, 13);
            this.lblHistoricalFigureDeathLocation.TabIndex = 6;
            this.lblHistoricalFigureDeathLocation.Text = "linkLabel3";
            // 
            // lblHistoricalFigureDeathSlayer
            // 
            this.lblHistoricalFigureDeathSlayer.AutoSize = true;
            this.lblHistoricalFigureDeathSlayer.Data = null;
            this.lblHistoricalFigureDeathSlayer.ForeColor = System.Drawing.Color.Black;
            this.lblHistoricalFigureDeathSlayer.Location = new System.Drawing.Point(47, 21);
            this.lblHistoricalFigureDeathSlayer.Name = "lblHistoricalFigureDeathSlayer";
            this.lblHistoricalFigureDeathSlayer.Size = new System.Drawing.Size(55, 13);
            this.lblHistoricalFigureDeathSlayer.TabIndex = 4;
            this.lblHistoricalFigureDeathSlayer.Text = "linkLabel1";
            // 
            // rtbHistoricalFigureSummary
            // 
            this.rtbHistoricalFigureSummary.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbHistoricalFigureSummary.Location = new System.Drawing.Point(483, 701);
            this.rtbHistoricalFigureSummary.Name = "rtbHistoricalFigureSummary";
            this.rtbHistoricalFigureSummary.Size = new System.Drawing.Size(599, 162);
            this.rtbHistoricalFigureSummary.TabIndex = 73;
            this.rtbHistoricalFigureSummary.Text = "";
            this.rtbHistoricalFigureSummary.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.rtbHistoricalFigureSummary_LinkClicked);
            // 
            // lblBeastAttackParent
            // 
            this.lblBeastAttackParent.AutoSize = true;
            this.lblBeastAttackParent.Data = null;
            this.lblBeastAttackParent.ForeColor = System.Drawing.Color.Black;
            this.lblBeastAttackParent.Location = new System.Drawing.Point(61, 46);
            this.lblBeastAttackParent.Name = "lblBeastAttackParent";
            this.lblBeastAttackParent.Size = new System.Drawing.Size(55, 13);
            this.lblBeastAttackParent.TabIndex = 110;
            this.lblBeastAttackParent.Text = "linkLabel1";
            // 
            // lblBeastAttackSite
            // 
            this.lblBeastAttackSite.AutoSize = true;
            this.lblBeastAttackSite.Data = null;
            this.lblBeastAttackSite.ForeColor = System.Drawing.Color.Black;
            this.lblBeastAttackSite.Location = new System.Drawing.Point(60, 111);
            this.lblBeastAttackSite.Name = "lblBeastAttackSite";
            this.lblBeastAttackSite.Size = new System.Drawing.Size(55, 13);
            this.lblBeastAttackSite.TabIndex = 114;
            this.lblBeastAttackSite.Text = "linkLabel1";
            // 
            // lblBeastAttackCoords
            // 
            this.lblBeastAttackCoords.AutoSize = true;
            this.lblBeastAttackCoords.Data = null;
            this.lblBeastAttackCoords.ForeColor = System.Drawing.Color.Black;
            this.lblBeastAttackCoords.Location = new System.Drawing.Point(59, 133);
            this.lblBeastAttackCoords.Name = "lblBeastAttackCoords";
            this.lblBeastAttackCoords.Size = new System.Drawing.Size(55, 13);
            this.lblBeastAttackCoords.TabIndex = 116;
            this.lblBeastAttackCoords.Text = "linkLabel3";
            // 
            // lblBeastAttackDefender
            // 
            this.lblBeastAttackDefender.AutoSize = true;
            this.lblBeastAttackDefender.Data = null;
            this.lblBeastAttackDefender.ForeColor = System.Drawing.Color.Black;
            this.lblBeastAttackDefender.Location = new System.Drawing.Point(59, 154);
            this.lblBeastAttackDefender.Name = "lblBeastAttackDefender";
            this.lblBeastAttackDefender.Size = new System.Drawing.Size(55, 13);
            this.lblBeastAttackDefender.TabIndex = 123;
            this.lblBeastAttackDefender.Text = "linkLabel3";
            // 
            // lblBeastAttackRegion
            // 
            this.lblBeastAttackRegion.AutoSize = true;
            this.lblBeastAttackRegion.Data = null;
            this.lblBeastAttackRegion.ForeColor = System.Drawing.Color.Black;
            this.lblBeastAttackRegion.Location = new System.Drawing.Point(61, 89);
            this.lblBeastAttackRegion.Name = "lblBeastAttackRegion";
            this.lblBeastAttackRegion.Size = new System.Drawing.Size(55, 13);
            this.lblBeastAttackRegion.TabIndex = 127;
            this.lblBeastAttackRegion.Text = "linkLabel1";
            // 
            // lblBeastAttackBeast
            // 
            this.lblBeastAttackBeast.AutoSize = true;
            this.lblBeastAttackBeast.Data = null;
            this.lblBeastAttackBeast.ForeColor = System.Drawing.Color.Black;
            this.lblBeastAttackBeast.Location = new System.Drawing.Point(61, 67);
            this.lblBeastAttackBeast.Name = "lblBeastAttackBeast";
            this.lblBeastAttackBeast.Size = new System.Drawing.Size(55, 13);
            this.lblBeastAttackBeast.TabIndex = 129;
            this.lblBeastAttackBeast.Text = "linkLabel1";
            // 
            // lblWarDefender
            // 
            this.lblWarDefender.AutoSize = true;
            this.lblWarDefender.Data = null;
            this.lblWarDefender.ForeColor = System.Drawing.Color.Black;
            this.lblWarDefender.Location = new System.Drawing.Point(5, 24);
            this.lblWarDefender.Name = "lblWarDefender";
            this.lblWarDefender.Size = new System.Drawing.Size(55, 13);
            this.lblWarDefender.TabIndex = 55;
            this.lblWarDefender.Text = "linkLabel1";
            // 
            // lblWarAggressor
            // 
            this.lblWarAggressor.AutoSize = true;
            this.lblWarAggressor.Data = null;
            this.lblWarAggressor.ForeColor = System.Drawing.Color.Black;
            this.lblWarAggressor.Location = new System.Drawing.Point(5, 24);
            this.lblWarAggressor.Name = "lblWarAggressor";
            this.lblWarAggressor.Size = new System.Drawing.Size(55, 13);
            this.lblWarAggressor.TabIndex = 54;
            this.lblWarAggressor.Text = "linkLabel1";
            // 
            // lblBattleWar
            // 
            this.lblBattleWar.AutoSize = true;
            this.lblBattleWar.Data = null;
            this.lblBattleWar.ForeColor = System.Drawing.Color.Black;
            this.lblBattleWar.Location = new System.Drawing.Point(61, 68);
            this.lblBattleWar.Name = "lblBattleWar";
            this.lblBattleWar.Size = new System.Drawing.Size(55, 13);
            this.lblBattleWar.TabIndex = 31;
            this.lblBattleWar.Text = "linkLabel1";
            // 
            // lblBattleRegion
            // 
            this.lblBattleRegion.AutoSize = true;
            this.lblBattleRegion.Data = null;
            this.lblBattleRegion.ForeColor = System.Drawing.Color.Black;
            this.lblBattleRegion.Location = new System.Drawing.Point(62, 90);
            this.lblBattleRegion.Name = "lblBattleRegion";
            this.lblBattleRegion.Size = new System.Drawing.Size(55, 13);
            this.lblBattleRegion.TabIndex = 37;
            this.lblBattleRegion.Text = "linkLabel1";
            // 
            // lblBattleSite
            // 
            this.lblBattleSite.AutoSize = true;
            this.lblBattleSite.Data = null;
            this.lblBattleSite.ForeColor = System.Drawing.Color.Black;
            this.lblBattleSite.Location = new System.Drawing.Point(62, 112);
            this.lblBattleSite.Name = "lblBattleSite";
            this.lblBattleSite.Size = new System.Drawing.Size(55, 13);
            this.lblBattleSite.TabIndex = 39;
            this.lblBattleSite.Text = "linkLabel1";
            // 
            // lblBattleCoord
            // 
            this.lblBattleCoord.AutoSize = true;
            this.lblBattleCoord.Data = null;
            this.lblBattleCoord.ForeColor = System.Drawing.Color.Black;
            this.lblBattleCoord.Location = new System.Drawing.Point(61, 134);
            this.lblBattleCoord.Name = "lblBattleCoord";
            this.lblBattleCoord.Size = new System.Drawing.Size(55, 13);
            this.lblBattleCoord.TabIndex = 41;
            this.lblBattleCoord.Text = "linkLabel3";
            // 
            // lblBattleDefendingSquadRace
            // 
            this.lblBattleDefendingSquadRace.AutoSize = true;
            this.lblBattleDefendingSquadRace.Data = null;
            this.lblBattleDefendingSquadRace.ForeColor = System.Drawing.Color.Black;
            this.lblBattleDefendingSquadRace.Location = new System.Drawing.Point(62, 197);
            this.lblBattleDefendingSquadRace.Name = "lblBattleDefendingSquadRace";
            this.lblBattleDefendingSquadRace.Size = new System.Drawing.Size(55, 13);
            this.lblBattleDefendingSquadRace.TabIndex = 47;
            this.lblBattleDefendingSquadRace.Text = "linkLabel3";
            // 
            // lblBattleDefendingSquadEntPop
            // 
            this.lblBattleDefendingSquadEntPop.AutoSize = true;
            this.lblBattleDefendingSquadEntPop.Data = null;
            this.lblBattleDefendingSquadEntPop.ForeColor = System.Drawing.Color.Black;
            this.lblBattleDefendingSquadEntPop.Location = new System.Drawing.Point(63, 176);
            this.lblBattleDefendingSquadEntPop.Name = "lblBattleDefendingSquadEntPop";
            this.lblBattleDefendingSquadEntPop.Size = new System.Drawing.Size(55, 13);
            this.lblBattleDefendingSquadEntPop.TabIndex = 45;
            this.lblBattleDefendingSquadEntPop.Text = "linkLabel1";
            // 
            // lblBattleDefendingSquadSite
            // 
            this.lblBattleDefendingSquadSite.AutoSize = true;
            this.lblBattleDefendingSquadSite.Data = null;
            this.lblBattleDefendingSquadSite.ForeColor = System.Drawing.Color.Black;
            this.lblBattleDefendingSquadSite.Location = new System.Drawing.Point(63, 154);
            this.lblBattleDefendingSquadSite.Name = "lblBattleDefendingSquadSite";
            this.lblBattleDefendingSquadSite.Size = new System.Drawing.Size(55, 13);
            this.lblBattleDefendingSquadSite.TabIndex = 43;
            this.lblBattleDefendingSquadSite.Text = "linkLabel1";
            // 
            // lblBattleAttackingSquadRace
            // 
            this.lblBattleAttackingSquadRace.AutoSize = true;
            this.lblBattleAttackingSquadRace.Data = null;
            this.lblBattleAttackingSquadRace.ForeColor = System.Drawing.Color.Black;
            this.lblBattleAttackingSquadRace.Location = new System.Drawing.Point(62, 197);
            this.lblBattleAttackingSquadRace.Name = "lblBattleAttackingSquadRace";
            this.lblBattleAttackingSquadRace.Size = new System.Drawing.Size(55, 13);
            this.lblBattleAttackingSquadRace.TabIndex = 47;
            this.lblBattleAttackingSquadRace.Text = "linkLabel3";
            // 
            // lblBattleAttackingSquadEntPop
            // 
            this.lblBattleAttackingSquadEntPop.AutoSize = true;
            this.lblBattleAttackingSquadEntPop.Data = null;
            this.lblBattleAttackingSquadEntPop.ForeColor = System.Drawing.Color.Black;
            this.lblBattleAttackingSquadEntPop.Location = new System.Drawing.Point(63, 176);
            this.lblBattleAttackingSquadEntPop.Name = "lblBattleAttackingSquadEntPop";
            this.lblBattleAttackingSquadEntPop.Size = new System.Drawing.Size(55, 13);
            this.lblBattleAttackingSquadEntPop.TabIndex = 45;
            this.lblBattleAttackingSquadEntPop.Text = "linkLabel1";
            // 
            // lblBattleAttackingSquadSite
            // 
            this.lblBattleAttackingSquadSite.AutoSize = true;
            this.lblBattleAttackingSquadSite.Data = null;
            this.lblBattleAttackingSquadSite.ForeColor = System.Drawing.Color.Black;
            this.lblBattleAttackingSquadSite.Location = new System.Drawing.Point(63, 154);
            this.lblBattleAttackingSquadSite.Name = "lblBattleAttackingSquadSite";
            this.lblBattleAttackingSquadSite.Size = new System.Drawing.Size(55, 13);
            this.lblBattleAttackingSquadSite.TabIndex = 43;
            this.lblBattleAttackingSquadSite.Text = "linkLabel1";
            // 
            // lblDuelParent
            // 
            this.lblDuelParent.AutoSize = true;
            this.lblDuelParent.Data = null;
            this.lblDuelParent.ForeColor = System.Drawing.Color.Black;
            this.lblDuelParent.Location = new System.Drawing.Point(61, 44);
            this.lblDuelParent.Name = "lblDuelParent";
            this.lblDuelParent.Size = new System.Drawing.Size(55, 13);
            this.lblDuelParent.TabIndex = 91;
            this.lblDuelParent.Text = "linkLabel1";
            // 
            // lblDuelSite
            // 
            this.lblDuelSite.AutoSize = true;
            this.lblDuelSite.Data = null;
            this.lblDuelSite.ForeColor = System.Drawing.Color.Black;
            this.lblDuelSite.Location = new System.Drawing.Point(60, 88);
            this.lblDuelSite.Name = "lblDuelSite";
            this.lblDuelSite.Size = new System.Drawing.Size(55, 13);
            this.lblDuelSite.TabIndex = 95;
            this.lblDuelSite.Text = "linkLabel1";
            // 
            // lblDuelCoords
            // 
            this.lblDuelCoords.AutoSize = true;
            this.lblDuelCoords.Data = null;
            this.lblDuelCoords.ForeColor = System.Drawing.Color.Black;
            this.lblDuelCoords.Location = new System.Drawing.Point(59, 110);
            this.lblDuelCoords.Name = "lblDuelCoords";
            this.lblDuelCoords.Size = new System.Drawing.Size(55, 13);
            this.lblDuelCoords.TabIndex = 97;
            this.lblDuelCoords.Text = "linkLabel3";
            // 
            // lblDuelAttacker
            // 
            this.lblDuelAttacker.AutoSize = true;
            this.lblDuelAttacker.Data = null;
            this.lblDuelAttacker.ForeColor = System.Drawing.Color.Black;
            this.lblDuelAttacker.Location = new System.Drawing.Point(60, 132);
            this.lblDuelAttacker.Name = "lblDuelAttacker";
            this.lblDuelAttacker.Size = new System.Drawing.Size(55, 13);
            this.lblDuelAttacker.TabIndex = 102;
            this.lblDuelAttacker.Text = "linkLabel1";
            // 
            // lblDuelDefender
            // 
            this.lblDuelDefender.AutoSize = true;
            this.lblDuelDefender.Data = null;
            this.lblDuelDefender.ForeColor = System.Drawing.Color.Black;
            this.lblDuelDefender.Location = new System.Drawing.Point(59, 154);
            this.lblDuelDefender.Name = "lblDuelDefender";
            this.lblDuelDefender.Size = new System.Drawing.Size(55, 13);
            this.lblDuelDefender.TabIndex = 104;
            this.lblDuelDefender.Text = "linkLabel3";
            // 
            // lblDuelRegion
            // 
            this.lblDuelRegion.AutoSize = true;
            this.lblDuelRegion.Data = null;
            this.lblDuelRegion.ForeColor = System.Drawing.Color.Black;
            this.lblDuelRegion.Location = new System.Drawing.Point(61, 66);
            this.lblDuelRegion.Name = "lblDuelRegion";
            this.lblDuelRegion.Size = new System.Drawing.Size(55, 13);
            this.lblDuelRegion.TabIndex = 108;
            this.lblDuelRegion.Text = "linkLabel1";
            // 
            // lblAbductionParent
            // 
            this.lblAbductionParent.AutoSize = true;
            this.lblAbductionParent.Data = null;
            this.lblAbductionParent.ForeColor = System.Drawing.Color.Black;
            this.lblAbductionParent.Location = new System.Drawing.Point(61, 44);
            this.lblAbductionParent.Name = "lblAbductionParent";
            this.lblAbductionParent.Size = new System.Drawing.Size(55, 13);
            this.lblAbductionParent.TabIndex = 72;
            this.lblAbductionParent.Text = "linkLabel1";
            // 
            // lblAbductionSite
            // 
            this.lblAbductionSite.AutoSize = true;
            this.lblAbductionSite.Data = null;
            this.lblAbductionSite.ForeColor = System.Drawing.Color.Black;
            this.lblAbductionSite.Location = new System.Drawing.Point(60, 88);
            this.lblAbductionSite.Name = "lblAbductionSite";
            this.lblAbductionSite.Size = new System.Drawing.Size(55, 13);
            this.lblAbductionSite.TabIndex = 76;
            this.lblAbductionSite.Text = "linkLabel1";
            // 
            // lblAbductionCoords
            // 
            this.lblAbductionCoords.AutoSize = true;
            this.lblAbductionCoords.Data = null;
            this.lblAbductionCoords.ForeColor = System.Drawing.Color.Black;
            this.lblAbductionCoords.Location = new System.Drawing.Point(59, 110);
            this.lblAbductionCoords.Name = "lblAbductionCoords";
            this.lblAbductionCoords.Size = new System.Drawing.Size(55, 13);
            this.lblAbductionCoords.TabIndex = 78;
            this.lblAbductionCoords.Text = "linkLabel3";
            // 
            // lblAbductionAttacker
            // 
            this.lblAbductionAttacker.AutoSize = true;
            this.lblAbductionAttacker.Data = null;
            this.lblAbductionAttacker.ForeColor = System.Drawing.Color.Black;
            this.lblAbductionAttacker.Location = new System.Drawing.Point(60, 132);
            this.lblAbductionAttacker.Name = "lblAbductionAttacker";
            this.lblAbductionAttacker.Size = new System.Drawing.Size(55, 13);
            this.lblAbductionAttacker.TabIndex = 83;
            this.lblAbductionAttacker.Text = "linkLabel1";
            // 
            // lblAbductionDefender
            // 
            this.lblAbductionDefender.AutoSize = true;
            this.lblAbductionDefender.Data = null;
            this.lblAbductionDefender.ForeColor = System.Drawing.Color.Black;
            this.lblAbductionDefender.Location = new System.Drawing.Point(59, 154);
            this.lblAbductionDefender.Name = "lblAbductionDefender";
            this.lblAbductionDefender.Size = new System.Drawing.Size(55, 13);
            this.lblAbductionDefender.TabIndex = 85;
            this.lblAbductionDefender.Text = "linkLabel3";
            // 
            // lblAbductionRegion
            // 
            this.lblAbductionRegion.AutoSize = true;
            this.lblAbductionRegion.Data = null;
            this.lblAbductionRegion.ForeColor = System.Drawing.Color.Black;
            this.lblAbductionRegion.Location = new System.Drawing.Point(61, 66);
            this.lblAbductionRegion.Name = "lblAbductionRegion";
            this.lblAbductionRegion.Size = new System.Drawing.Size(55, 13);
            this.lblAbductionRegion.TabIndex = 89;
            this.lblAbductionRegion.Text = "linkLabel1";
            // 
            // lblSiteConqueredWar
            // 
            this.lblSiteConqueredWar.AutoSize = true;
            this.lblSiteConqueredWar.Data = null;
            this.lblSiteConqueredWar.ForeColor = System.Drawing.Color.Black;
            this.lblSiteConqueredWar.Location = new System.Drawing.Point(61, 44);
            this.lblSiteConqueredWar.Name = "lblSiteConqueredWar";
            this.lblSiteConqueredWar.Size = new System.Drawing.Size(55, 13);
            this.lblSiteConqueredWar.TabIndex = 52;
            this.lblSiteConqueredWar.Text = "linkLabel1";
            // 
            // lblSiteConqueredSite
            // 
            this.lblSiteConqueredSite.AutoSize = true;
            this.lblSiteConqueredSite.Data = null;
            this.lblSiteConqueredSite.ForeColor = System.Drawing.Color.Black;
            this.lblSiteConqueredSite.Location = new System.Drawing.Point(61, 66);
            this.lblSiteConqueredSite.Name = "lblSiteConqueredSite";
            this.lblSiteConqueredSite.Size = new System.Drawing.Size(55, 13);
            this.lblSiteConqueredSite.TabIndex = 58;
            this.lblSiteConqueredSite.Text = "linkLabel1";
            // 
            // lblSiteConqueredCoords
            // 
            this.lblSiteConqueredCoords.AutoSize = true;
            this.lblSiteConqueredCoords.Data = null;
            this.lblSiteConqueredCoords.ForeColor = System.Drawing.Color.Black;
            this.lblSiteConqueredCoords.Location = new System.Drawing.Point(60, 88);
            this.lblSiteConqueredCoords.Name = "lblSiteConqueredCoords";
            this.lblSiteConqueredCoords.Size = new System.Drawing.Size(55, 13);
            this.lblSiteConqueredCoords.TabIndex = 60;
            this.lblSiteConqueredCoords.Text = "linkLabel3";
            // 
            // lblSiteConqueredAttacker
            // 
            this.lblSiteConqueredAttacker.AutoSize = true;
            this.lblSiteConqueredAttacker.Data = null;
            this.lblSiteConqueredAttacker.ForeColor = System.Drawing.Color.Black;
            this.lblSiteConqueredAttacker.Location = new System.Drawing.Point(61, 110);
            this.lblSiteConqueredAttacker.Name = "lblSiteConqueredAttacker";
            this.lblSiteConqueredAttacker.Size = new System.Drawing.Size(55, 13);
            this.lblSiteConqueredAttacker.TabIndex = 65;
            this.lblSiteConqueredAttacker.Text = "linkLabel1";
            // 
            // lblSiteConqueredDefender
            // 
            this.lblSiteConqueredDefender.AutoSize = true;
            this.lblSiteConqueredDefender.Data = null;
            this.lblSiteConqueredDefender.ForeColor = System.Drawing.Color.Black;
            this.lblSiteConqueredDefender.Location = new System.Drawing.Point(60, 132);
            this.lblSiteConqueredDefender.Name = "lblSiteConqueredDefender";
            this.lblSiteConqueredDefender.Size = new System.Drawing.Size(55, 13);
            this.lblSiteConqueredDefender.TabIndex = 67;
            this.lblSiteConqueredDefender.Text = "linkLabel3";
            // 
            // lblTheftWar
            // 
            this.lblTheftWar.AutoSize = true;
            this.lblTheftWar.Data = null;
            this.lblTheftWar.ForeColor = System.Drawing.Color.Black;
            this.lblTheftWar.Location = new System.Drawing.Point(61, 44);
            this.lblTheftWar.Name = "lblTheftWar";
            this.lblTheftWar.Size = new System.Drawing.Size(55, 13);
            this.lblTheftWar.TabIndex = 72;
            this.lblTheftWar.Text = "linkLabel1";
            // 
            // lblTheftSite
            // 
            this.lblTheftSite.AutoSize = true;
            this.lblTheftSite.Data = null;
            this.lblTheftSite.ForeColor = System.Drawing.Color.Black;
            this.lblTheftSite.Location = new System.Drawing.Point(61, 66);
            this.lblTheftSite.Name = "lblTheftSite";
            this.lblTheftSite.Size = new System.Drawing.Size(55, 13);
            this.lblTheftSite.TabIndex = 76;
            this.lblTheftSite.Text = "linkLabel1";
            // 
            // lblTheftCoords
            // 
            this.lblTheftCoords.AutoSize = true;
            this.lblTheftCoords.Data = null;
            this.lblTheftCoords.ForeColor = System.Drawing.Color.Black;
            this.lblTheftCoords.Location = new System.Drawing.Point(60, 88);
            this.lblTheftCoords.Name = "lblTheftCoords";
            this.lblTheftCoords.Size = new System.Drawing.Size(55, 13);
            this.lblTheftCoords.TabIndex = 78;
            this.lblTheftCoords.Text = "linkLabel3";
            // 
            // lblTheftAttacker
            // 
            this.lblTheftAttacker.AutoSize = true;
            this.lblTheftAttacker.Data = null;
            this.lblTheftAttacker.ForeColor = System.Drawing.Color.Black;
            this.lblTheftAttacker.Location = new System.Drawing.Point(61, 110);
            this.lblTheftAttacker.Name = "lblTheftAttacker";
            this.lblTheftAttacker.Size = new System.Drawing.Size(55, 13);
            this.lblTheftAttacker.TabIndex = 83;
            this.lblTheftAttacker.Text = "linkLabel1";
            // 
            // lblTheftDefender
            // 
            this.lblTheftDefender.AutoSize = true;
            this.lblTheftDefender.Data = null;
            this.lblTheftDefender.ForeColor = System.Drawing.Color.Black;
            this.lblTheftDefender.Location = new System.Drawing.Point(60, 132);
            this.lblTheftDefender.Name = "lblTheftDefender";
            this.lblTheftDefender.Size = new System.Drawing.Size(55, 13);
            this.lblTheftDefender.TabIndex = 85;
            this.lblTheftDefender.Text = "linkLabel3";
            // 
            // lblInsurrectionOutcome
            // 
            this.lblInsurrectionOutcome.AutoSize = true;
            this.lblInsurrectionOutcome.Data = null;
            this.lblInsurrectionOutcome.ForeColor = System.Drawing.Color.Black;
            this.lblInsurrectionOutcome.Location = new System.Drawing.Point(61, 132);
            this.lblInsurrectionOutcome.Name = "lblInsurrectionOutcome";
            this.lblInsurrectionOutcome.Size = new System.Drawing.Size(55, 13);
            this.lblInsurrectionOutcome.TabIndex = 108;
            this.lblInsurrectionOutcome.Text = "linkLabel1";
            // 
            // lblInsurrectionParent
            // 
            this.lblInsurrectionParent.AutoSize = true;
            this.lblInsurrectionParent.Data = null;
            this.lblInsurrectionParent.ForeColor = System.Drawing.Color.Black;
            this.lblInsurrectionParent.Location = new System.Drawing.Point(61, 44);
            this.lblInsurrectionParent.Name = "lblInsurrectionParent";
            this.lblInsurrectionParent.Size = new System.Drawing.Size(55, 13);
            this.lblInsurrectionParent.TabIndex = 91;
            this.lblInsurrectionParent.Text = "linkLabel1";
            // 
            // lblInsurrectionSite
            // 
            this.lblInsurrectionSite.AutoSize = true;
            this.lblInsurrectionSite.Data = null;
            this.lblInsurrectionSite.ForeColor = System.Drawing.Color.Black;
            this.lblInsurrectionSite.Location = new System.Drawing.Point(61, 66);
            this.lblInsurrectionSite.Name = "lblInsurrectionSite";
            this.lblInsurrectionSite.Size = new System.Drawing.Size(55, 13);
            this.lblInsurrectionSite.TabIndex = 95;
            this.lblInsurrectionSite.Text = "linkLabel1";
            // 
            // lblInsurrectionCoords
            // 
            this.lblInsurrectionCoords.AutoSize = true;
            this.lblInsurrectionCoords.Data = null;
            this.lblInsurrectionCoords.ForeColor = System.Drawing.Color.Black;
            this.lblInsurrectionCoords.Location = new System.Drawing.Point(60, 88);
            this.lblInsurrectionCoords.Name = "lblInsurrectionCoords";
            this.lblInsurrectionCoords.Size = new System.Drawing.Size(55, 13);
            this.lblInsurrectionCoords.TabIndex = 97;
            this.lblInsurrectionCoords.Text = "linkLabel3";
            // 
            // lblInsurrectionTargetEnt
            // 
            this.lblInsurrectionTargetEnt.AutoSize = true;
            this.lblInsurrectionTargetEnt.Data = null;
            this.lblInsurrectionTargetEnt.ForeColor = System.Drawing.Color.Black;
            this.lblInsurrectionTargetEnt.Location = new System.Drawing.Point(61, 110);
            this.lblInsurrectionTargetEnt.Name = "lblInsurrectionTargetEnt";
            this.lblInsurrectionTargetEnt.Size = new System.Drawing.Size(55, 13);
            this.lblInsurrectionTargetEnt.TabIndex = 102;
            this.lblInsurrectionTargetEnt.Text = "linkLabel1";
            // 
            // lblOccasionCiv
            // 
            this.lblOccasionCiv.AutoSize = true;
            this.lblOccasionCiv.Data = null;
            this.lblOccasionCiv.ForeColor = System.Drawing.Color.Black;
            this.lblOccasionCiv.Location = new System.Drawing.Point(59, 43);
            this.lblOccasionCiv.Name = "lblOccasionCiv";
            this.lblOccasionCiv.Size = new System.Drawing.Size(55, 13);
            this.lblOccasionCiv.TabIndex = 83;
            this.lblOccasionCiv.Text = "linkLabel1";
            // 
            // lblPurgeSite
            // 
            this.lblPurgeSite.AutoSize = true;
            this.lblPurgeSite.Data = null;
            this.lblPurgeSite.ForeColor = System.Drawing.Color.Black;
            this.lblPurgeSite.Location = new System.Drawing.Point(61, 44);
            this.lblPurgeSite.Name = "lblPurgeSite";
            this.lblPurgeSite.Size = new System.Drawing.Size(55, 13);
            this.lblPurgeSite.TabIndex = 52;
            this.lblPurgeSite.Text = "linkLabel1";
            // 
            // lblLeaderMarried
            // 
            this.lblLeaderMarried.AutoSize = true;
            this.lblLeaderMarried.Data = null;
            this.lblLeaderMarried.ForeColor = System.Drawing.Color.Black;
            this.lblLeaderMarried.Location = new System.Drawing.Point(101, 237);
            this.lblLeaderMarried.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblLeaderMarried.Name = "lblLeaderMarried";
            this.lblLeaderMarried.Size = new System.Drawing.Size(55, 13);
            this.lblLeaderMarried.TabIndex = 92;
            this.lblLeaderMarried.Text = "linkLabel5";
            // 
            // lblLeaderHF
            // 
            this.lblLeaderHF.AutoSize = true;
            this.lblLeaderHF.Data = null;
            this.lblLeaderHF.ForeColor = System.Drawing.Color.Black;
            this.lblLeaderHF.Location = new System.Drawing.Point(101, 260);
            this.lblLeaderHF.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblLeaderHF.Name = "lblLeaderHF";
            this.lblLeaderHF.Size = new System.Drawing.Size(55, 13);
            this.lblLeaderHF.TabIndex = 89;
            this.lblLeaderHF.Text = "linkLabel5";
            // 
            // lblLeaderInheritedFrom
            // 
            this.lblLeaderInheritedFrom.AutoSize = true;
            this.lblLeaderInheritedFrom.Data = null;
            this.lblLeaderInheritedFrom.ForeColor = System.Drawing.Color.Black;
            this.lblLeaderInheritedFrom.Location = new System.Drawing.Point(101, 151);
            this.lblLeaderInheritedFrom.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblLeaderInheritedFrom.Name = "lblLeaderInheritedFrom";
            this.lblLeaderInheritedFrom.Size = new System.Drawing.Size(55, 13);
            this.lblLeaderInheritedFrom.TabIndex = 85;
            this.lblLeaderInheritedFrom.Text = "linkLabel2";
            // 
            // lblLeaderRace
            // 
            this.lblLeaderRace.AutoSize = true;
            this.lblLeaderRace.Data = null;
            this.lblLeaderRace.ForeColor = System.Drawing.Color.Black;
            this.lblLeaderRace.Location = new System.Drawing.Point(101, 66);
            this.lblLeaderRace.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblLeaderRace.Name = "lblLeaderRace";
            this.lblLeaderRace.Size = new System.Drawing.Size(55, 13);
            this.lblLeaderRace.TabIndex = 82;
            this.lblLeaderRace.Text = "linkLabel3";
            // 
            // lblLeaderGod
            // 
            this.lblLeaderGod.AutoSize = true;
            this.lblLeaderGod.Data = null;
            this.lblLeaderGod.ForeColor = System.Drawing.Color.Black;
            this.lblLeaderGod.Location = new System.Drawing.Point(101, 215);
            this.lblLeaderGod.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblLeaderGod.Name = "lblLeaderGod";
            this.lblLeaderGod.Size = new System.Drawing.Size(55, 13);
            this.lblLeaderGod.TabIndex = 80;
            this.lblLeaderGod.Text = "linkLabel5";
            // 
            // lblLeaderCivilization
            // 
            this.lblLeaderCivilization.AutoSize = true;
            this.lblLeaderCivilization.Data = null;
            this.lblLeaderCivilization.ForeColor = System.Drawing.Color.Black;
            this.lblLeaderCivilization.Location = new System.Drawing.Point(101, 172);
            this.lblLeaderCivilization.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblLeaderCivilization.Name = "lblLeaderCivilization";
            this.lblLeaderCivilization.Size = new System.Drawing.Size(55, 13);
            this.lblLeaderCivilization.TabIndex = 79;
            this.lblLeaderCivilization.Text = "linkLabel3";
            // 
            // lblLeaderSite
            // 
            this.lblLeaderSite.AutoSize = true;
            this.lblLeaderSite.Data = null;
            this.lblLeaderSite.ForeColor = System.Drawing.Color.Black;
            this.lblLeaderSite.Location = new System.Drawing.Point(101, 193);
            this.lblLeaderSite.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblLeaderSite.Name = "lblLeaderSite";
            this.lblLeaderSite.Size = new System.Drawing.Size(55, 13);
            this.lblLeaderSite.TabIndex = 78;
            this.lblLeaderSite.Text = "linkLabel2";
            // 
            // lblSiteCurOwner
            // 
            this.lblSiteCurOwner.AutoSize = true;
            this.lblSiteCurOwner.Data = null;
            this.lblSiteCurOwner.ForeColor = System.Drawing.Color.Black;
            this.lblSiteCurOwner.Location = new System.Drawing.Point(71, 129);
            this.lblSiteCurOwner.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblSiteCurOwner.Name = "lblSiteCurOwner";
            this.lblSiteCurOwner.Size = new System.Drawing.Size(55, 13);
            this.lblSiteCurOwner.TabIndex = 64;
            this.lblSiteCurOwner.Text = "linkLabel1";
            // 
            // lblSiteCiv
            // 
            this.lblSiteCiv.AutoSize = true;
            this.lblSiteCiv.Data = null;
            this.lblSiteCiv.ForeColor = System.Drawing.Color.Black;
            this.lblSiteCiv.Location = new System.Drawing.Point(70, 151);
            this.lblSiteCiv.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblSiteCiv.Name = "lblSiteCiv";
            this.lblSiteCiv.Size = new System.Drawing.Size(55, 13);
            this.lblSiteCiv.TabIndex = 65;
            this.lblSiteCiv.Text = "linkLabel2";
            // 
            // SiteMapLabel
            // 
            this.SiteMapLabel.AutoSize = true;
            this.SiteMapLabel.Data = null;
            this.SiteMapLabel.ForeColor = System.Drawing.Color.Black;
            this.SiteMapLabel.Location = new System.Drawing.Point(12, 177);
            this.SiteMapLabel.Name = "SiteMapLabel";
            this.SiteMapLabel.Size = new System.Drawing.Size(49, 13);
            this.SiteMapLabel.TabIndex = 61;
            this.SiteMapLabel.Text = "Site Map";
            this.SiteMapLabel.Click += new System.EventHandler(this.SiteMapLabel_Click);
            // 
            // lblSiteCoord
            // 
            this.lblSiteCoord.AutoSize = true;
            this.lblSiteCoord.Data = null;
            this.lblSiteCoord.ForeColor = System.Drawing.Color.Black;
            this.lblSiteCoord.Location = new System.Drawing.Point(71, 64);
            this.lblSiteCoord.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblSiteCoord.Name = "lblSiteCoord";
            this.lblSiteCoord.Size = new System.Drawing.Size(55, 13);
            this.lblSiteCoord.TabIndex = 36;
            this.lblSiteCoord.Text = "linkLabel1";
            // 
            // lblSiteOwner
            // 
            this.lblSiteOwner.AutoSize = true;
            this.lblSiteOwner.Data = null;
            this.lblSiteOwner.ForeColor = System.Drawing.Color.Black;
            this.lblSiteOwner.Location = new System.Drawing.Point(71, 85);
            this.lblSiteOwner.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblSiteOwner.Name = "lblSiteOwner";
            this.lblSiteOwner.Size = new System.Drawing.Size(55, 13);
            this.lblSiteOwner.TabIndex = 35;
            this.lblSiteOwner.Text = "linkLabel1";
            // 
            // lblSiteParentCiv
            // 
            this.lblSiteParentCiv.AutoSize = true;
            this.lblSiteParentCiv.Data = null;
            this.lblSiteParentCiv.ForeColor = System.Drawing.Color.Black;
            this.lblSiteParentCiv.Location = new System.Drawing.Point(71, 107);
            this.lblSiteParentCiv.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblSiteParentCiv.Name = "lblSiteParentCiv";
            this.lblSiteParentCiv.Size = new System.Drawing.Size(55, 13);
            this.lblSiteParentCiv.TabIndex = 36;
            this.lblSiteParentCiv.Text = "linkLabel2";
            // 
            // lblSiteCreatedTime
            // 
            this.lblSiteCreatedTime.AutoSize = true;
            this.lblSiteCreatedTime.Data = null;
            this.lblSiteCreatedTime.ForeColor = System.Drawing.Color.Black;
            this.lblSiteCreatedTime.Location = new System.Drawing.Point(28, 58);
            this.lblSiteCreatedTime.Name = "lblSiteCreatedTime";
            this.lblSiteCreatedTime.Size = new System.Drawing.Size(55, 13);
            this.lblSiteCreatedTime.TabIndex = 7;
            this.lblSiteCreatedTime.Text = "linkLabel1";
            // 
            // lblSiteCreatedByCiv
            // 
            this.lblSiteCreatedByCiv.AutoSize = true;
            this.lblSiteCreatedByCiv.Data = null;
            this.lblSiteCreatedByCiv.ForeColor = System.Drawing.Color.Black;
            this.lblSiteCreatedByCiv.Location = new System.Drawing.Point(28, 37);
            this.lblSiteCreatedByCiv.Name = "lblSiteCreatedByCiv";
            this.lblSiteCreatedByCiv.Size = new System.Drawing.Size(55, 13);
            this.lblSiteCreatedByCiv.TabIndex = 6;
            this.lblSiteCreatedByCiv.Text = "linkLabel3";
            // 
            // lblSiteCreatedBy
            // 
            this.lblSiteCreatedBy.AutoSize = true;
            this.lblSiteCreatedBy.Data = null;
            this.lblSiteCreatedBy.ForeColor = System.Drawing.Color.Black;
            this.lblSiteCreatedBy.Location = new System.Drawing.Point(28, 15);
            this.lblSiteCreatedBy.Name = "lblSiteCreatedBy";
            this.lblSiteCreatedBy.Size = new System.Drawing.Size(55, 13);
            this.lblSiteCreatedBy.TabIndex = 4;
            this.lblSiteCreatedBy.Text = "linkLabel1";
            // 
            // lblStructureRazedTime
            // 
            this.lblStructureRazedTime.AutoSize = true;
            this.lblStructureRazedTime.Data = null;
            this.lblStructureRazedTime.ForeColor = System.Drawing.Color.Black;
            this.lblStructureRazedTime.Location = new System.Drawing.Point(28, 60);
            this.lblStructureRazedTime.Name = "lblStructureRazedTime";
            this.lblStructureRazedTime.Size = new System.Drawing.Size(55, 13);
            this.lblStructureRazedTime.TabIndex = 17;
            this.lblStructureRazedTime.Text = "linkLabel7";
            // 
            // lblStructureRazedSite
            // 
            this.lblStructureRazedSite.AutoSize = true;
            this.lblStructureRazedSite.Data = null;
            this.lblStructureRazedSite.ForeColor = System.Drawing.Color.Black;
            this.lblStructureRazedSite.Location = new System.Drawing.Point(28, 38);
            this.lblStructureRazedSite.Name = "lblStructureRazedSite";
            this.lblStructureRazedSite.Size = new System.Drawing.Size(55, 13);
            this.lblStructureRazedSite.TabIndex = 15;
            this.lblStructureRazedSite.Text = "linkLabel1";
            // 
            // lblStructureRazedCiv
            // 
            this.lblStructureRazedCiv.AutoSize = true;
            this.lblStructureRazedCiv.Data = null;
            this.lblStructureRazedCiv.ForeColor = System.Drawing.Color.Black;
            this.lblStructureRazedCiv.Location = new System.Drawing.Point(28, 16);
            this.lblStructureRazedCiv.Name = "lblStructureRazedCiv";
            this.lblStructureRazedCiv.Size = new System.Drawing.Size(55, 13);
            this.lblStructureRazedCiv.TabIndex = 13;
            this.lblStructureRazedCiv.Text = "linkLabel1";
            // 
            // lblStructureCreatedTime
            // 
            this.lblStructureCreatedTime.AutoSize = true;
            this.lblStructureCreatedTime.Data = null;
            this.lblStructureCreatedTime.ForeColor = System.Drawing.Color.Black;
            this.lblStructureCreatedTime.Location = new System.Drawing.Point(28, 80);
            this.lblStructureCreatedTime.Name = "lblStructureCreatedTime";
            this.lblStructureCreatedTime.Size = new System.Drawing.Size(55, 13);
            this.lblStructureCreatedTime.TabIndex = 9;
            this.lblStructureCreatedTime.Text = "linkLabel7";
            // 
            // lblStructureCreatedSite
            // 
            this.lblStructureCreatedSite.AutoSize = true;
            this.lblStructureCreatedSite.Data = null;
            this.lblStructureCreatedSite.ForeColor = System.Drawing.Color.Black;
            this.lblStructureCreatedSite.Location = new System.Drawing.Point(28, 58);
            this.lblStructureCreatedSite.Name = "lblStructureCreatedSite";
            this.lblStructureCreatedSite.Size = new System.Drawing.Size(55, 13);
            this.lblStructureCreatedSite.TabIndex = 7;
            this.lblStructureCreatedSite.Text = "linkLabel1";
            // 
            // lblStructureCreatedCiv
            // 
            this.lblStructureCreatedCiv.AutoSize = true;
            this.lblStructureCreatedCiv.Data = null;
            this.lblStructureCreatedCiv.ForeColor = System.Drawing.Color.Black;
            this.lblStructureCreatedCiv.Location = new System.Drawing.Point(28, 37);
            this.lblStructureCreatedCiv.Name = "lblStructureCreatedCiv";
            this.lblStructureCreatedCiv.Size = new System.Drawing.Size(55, 13);
            this.lblStructureCreatedCiv.TabIndex = 6;
            this.lblStructureCreatedCiv.Text = "linkLabel3";
            // 
            // lblStructureCreatedSiteCiv
            // 
            this.lblStructureCreatedSiteCiv.AutoSize = true;
            this.lblStructureCreatedSiteCiv.Data = null;
            this.lblStructureCreatedSiteCiv.ForeColor = System.Drawing.Color.Black;
            this.lblStructureCreatedSiteCiv.Location = new System.Drawing.Point(28, 15);
            this.lblStructureCreatedSiteCiv.Name = "lblStructureCreatedSiteCiv";
            this.lblStructureCreatedSiteCiv.Size = new System.Drawing.Size(55, 13);
            this.lblStructureCreatedSiteCiv.TabIndex = 4;
            this.lblStructureCreatedSiteCiv.Text = "linkLabel1";
            // 
            // lblStructureSite
            // 
            this.lblStructureSite.AutoSize = true;
            this.lblStructureSite.Data = null;
            this.lblStructureSite.ForeColor = System.Drawing.Color.Black;
            this.lblStructureSite.Location = new System.Drawing.Point(61, 24);
            this.lblStructureSite.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblStructureSite.Name = "lblStructureSite";
            this.lblStructureSite.Size = new System.Drawing.Size(55, 13);
            this.lblStructureSite.TabIndex = 36;
            this.lblStructureSite.Text = "linkLabel1";
            // 
            // lblWorldConstructionCoord
            // 
            this.lblWorldConstructionCoord.AutoSize = true;
            this.lblWorldConstructionCoord.Data = null;
            this.lblWorldConstructionCoord.ForeColor = System.Drawing.Color.Black;
            this.lblWorldConstructionCoord.Location = new System.Drawing.Point(67, 208);
            this.lblWorldConstructionCoord.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblWorldConstructionCoord.Name = "lblWorldConstructionCoord";
            this.lblWorldConstructionCoord.Size = new System.Drawing.Size(55, 13);
            this.lblWorldConstructionCoord.TabIndex = 41;
            this.lblWorldConstructionCoord.Text = "linkLabel2";
            // 
            // lblWorldConstructionType
            // 
            this.lblWorldConstructionType.AutoSize = true;
            this.lblWorldConstructionType.Data = null;
            this.lblWorldConstructionType.ForeColor = System.Drawing.Color.Black;
            this.lblWorldConstructionType.Location = new System.Drawing.Point(67, 186);
            this.lblWorldConstructionType.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblWorldConstructionType.Name = "lblWorldConstructionType";
            this.lblWorldConstructionType.Size = new System.Drawing.Size(55, 13);
            this.lblWorldConstructionType.TabIndex = 39;
            this.lblWorldConstructionType.Text = "linkLabel2";
            // 
            // lblWorldConstructionCreatedTime
            // 
            this.lblWorldConstructionCreatedTime.AutoSize = true;
            this.lblWorldConstructionCreatedTime.Data = null;
            this.lblWorldConstructionCreatedTime.ForeColor = System.Drawing.Color.Black;
            this.lblWorldConstructionCreatedTime.Location = new System.Drawing.Point(28, 64);
            this.lblWorldConstructionCreatedTime.Name = "lblWorldConstructionCreatedTime";
            this.lblWorldConstructionCreatedTime.Size = new System.Drawing.Size(55, 13);
            this.lblWorldConstructionCreatedTime.TabIndex = 7;
            this.lblWorldConstructionCreatedTime.Text = "linkLabel1";
            // 
            // lblWorldConstructionCreatedByCiv
            // 
            this.lblWorldConstructionCreatedByCiv.AutoSize = true;
            this.lblWorldConstructionCreatedByCiv.Data = null;
            this.lblWorldConstructionCreatedByCiv.ForeColor = System.Drawing.Color.Black;
            this.lblWorldConstructionCreatedByCiv.Location = new System.Drawing.Point(28, 43);
            this.lblWorldConstructionCreatedByCiv.Name = "lblWorldConstructionCreatedByCiv";
            this.lblWorldConstructionCreatedByCiv.Size = new System.Drawing.Size(55, 13);
            this.lblWorldConstructionCreatedByCiv.TabIndex = 6;
            this.lblWorldConstructionCreatedByCiv.Text = "linkLabel3";
            // 
            // lblWorldConstructionCreatedBy
            // 
            this.lblWorldConstructionCreatedBy.AutoSize = true;
            this.lblWorldConstructionCreatedBy.Data = null;
            this.lblWorldConstructionCreatedBy.ForeColor = System.Drawing.Color.Black;
            this.lblWorldConstructionCreatedBy.Location = new System.Drawing.Point(28, 21);
            this.lblWorldConstructionCreatedBy.Name = "lblWorldConstructionCreatedBy";
            this.lblWorldConstructionCreatedBy.Size = new System.Drawing.Size(55, 13);
            this.lblWorldConstructionCreatedBy.TabIndex = 4;
            this.lblWorldConstructionCreatedBy.Text = "linkLabel1";
            // 
            // lblWorldConstructionFrom
            // 
            this.lblWorldConstructionFrom.AutoSize = true;
            this.lblWorldConstructionFrom.Data = null;
            this.lblWorldConstructionFrom.ForeColor = System.Drawing.Color.Black;
            this.lblWorldConstructionFrom.Location = new System.Drawing.Point(67, 37);
            this.lblWorldConstructionFrom.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblWorldConstructionFrom.Name = "lblWorldConstructionFrom";
            this.lblWorldConstructionFrom.Size = new System.Drawing.Size(55, 13);
            this.lblWorldConstructionFrom.TabIndex = 32;
            this.lblWorldConstructionFrom.Text = "linkLabel3";
            // 
            // lblWorldConstructionMaster
            // 
            this.lblWorldConstructionMaster.AutoSize = true;
            this.lblWorldConstructionMaster.Data = null;
            this.lblWorldConstructionMaster.ForeColor = System.Drawing.Color.Black;
            this.lblWorldConstructionMaster.Location = new System.Drawing.Point(67, 15);
            this.lblWorldConstructionMaster.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblWorldConstructionMaster.Name = "lblWorldConstructionMaster";
            this.lblWorldConstructionMaster.Size = new System.Drawing.Size(55, 13);
            this.lblWorldConstructionMaster.TabIndex = 31;
            this.lblWorldConstructionMaster.Text = "linkLabel2";
            // 
            // lblWorldConstructionTo
            // 
            this.lblWorldConstructionTo.AutoSize = true;
            this.lblWorldConstructionTo.Data = null;
            this.lblWorldConstructionTo.ForeColor = System.Drawing.Color.Black;
            this.lblWorldConstructionTo.Location = new System.Drawing.Point(67, 58);
            this.lblWorldConstructionTo.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblWorldConstructionTo.Name = "lblWorldConstructionTo";
            this.lblWorldConstructionTo.Size = new System.Drawing.Size(55, 13);
            this.lblWorldConstructionTo.TabIndex = 30;
            this.lblWorldConstructionTo.Text = "linkLabel1";
            // 
            // lblDynastyLength
            // 
            this.lblDynastyLength.AutoSize = true;
            this.lblDynastyLength.Data = null;
            this.lblDynastyLength.ForeColor = System.Drawing.Color.Black;
            this.lblDynastyLength.Location = new System.Drawing.Point(67, 66);
            this.lblDynastyLength.Name = "lblDynastyLength";
            this.lblDynastyLength.Size = new System.Drawing.Size(35, 13);
            this.lblDynastyLength.TabIndex = 23;
            this.lblDynastyLength.Text = "label1";
            // 
            // lblDynastyFounder
            // 
            this.lblDynastyFounder.AutoSize = true;
            this.lblDynastyFounder.Data = null;
            this.lblDynastyFounder.ForeColor = System.Drawing.Color.Black;
            this.lblDynastyFounder.Location = new System.Drawing.Point(67, 0);
            this.lblDynastyFounder.Name = "lblDynastyFounder";
            this.lblDynastyFounder.Size = new System.Drawing.Size(55, 13);
            this.lblDynastyFounder.TabIndex = 22;
            this.lblDynastyFounder.Text = "linkLabel1";
            // 
            // lblDynastyCivilization
            // 
            this.lblDynastyCivilization.AutoSize = true;
            this.lblDynastyCivilization.Data = null;
            this.lblDynastyCivilization.ForeColor = System.Drawing.Color.Black;
            this.lblDynastyCivilization.Location = new System.Drawing.Point(67, 22);
            this.lblDynastyCivilization.Name = "lblDynastyCivilization";
            this.lblDynastyCivilization.Size = new System.Drawing.Size(55, 13);
            this.lblDynastyCivilization.TabIndex = 20;
            this.lblDynastyCivilization.Text = "linkLabel1";
            // 
            // lblLandmassCenter
            // 
            this.lblLandmassCenter.AutoSize = true;
            this.lblLandmassCenter.Data = null;
            this.lblLandmassCenter.ForeColor = System.Drawing.Color.Black;
            this.lblLandmassCenter.Location = new System.Drawing.Point(67, 66);
            this.lblLandmassCenter.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblLandmassCenter.Name = "lblLandmassCenter";
            this.lblLandmassCenter.Size = new System.Drawing.Size(55, 13);
            this.lblLandmassCenter.TabIndex = 40;
            this.lblLandmassCenter.Text = "linkLabel1";
            // 
            // lblLandmassMax
            // 
            this.lblLandmassMax.AutoSize = true;
            this.lblLandmassMax.Data = null;
            this.lblLandmassMax.ForeColor = System.Drawing.Color.Black;
            this.lblLandmassMax.Location = new System.Drawing.Point(67, 89);
            this.lblLandmassMax.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblLandmassMax.Name = "lblLandmassMax";
            this.lblLandmassMax.Size = new System.Drawing.Size(55, 13);
            this.lblLandmassMax.TabIndex = 38;
            this.lblLandmassMax.Text = "linkLabel1";
            // 
            // lblLandmassMin
            // 
            this.lblLandmassMin.AutoSize = true;
            this.lblLandmassMin.Data = null;
            this.lblLandmassMin.ForeColor = System.Drawing.Color.Black;
            this.lblLandmassMin.Location = new System.Drawing.Point(67, 43);
            this.lblLandmassMin.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblLandmassMin.Name = "lblLandmassMin";
            this.lblLandmassMin.Size = new System.Drawing.Size(55, 13);
            this.lblLandmassMin.TabIndex = 37;
            this.lblLandmassMin.Text = "linkLabel1";
            // 
            // lblMountainCoord
            // 
            this.lblMountainCoord.AutoSize = true;
            this.lblMountainCoord.Data = null;
            this.lblMountainCoord.ForeColor = System.Drawing.Color.Black;
            this.lblMountainCoord.Location = new System.Drawing.Point(68, 67);
            this.lblMountainCoord.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblMountainCoord.Name = "lblMountainCoord";
            this.lblMountainCoord.Size = new System.Drawing.Size(55, 13);
            this.lblMountainCoord.TabIndex = 37;
            this.lblMountainCoord.Text = "linkLabel1";
            // 
            // lblRiverParent
            // 
            this.lblRiverParent.AutoSize = true;
            this.lblRiverParent.Data = null;
            this.lblRiverParent.ForeColor = System.Drawing.Color.Black;
            this.lblRiverParent.Location = new System.Drawing.Point(68, 111);
            this.lblRiverParent.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblRiverParent.Name = "lblRiverParent";
            this.lblRiverParent.Size = new System.Drawing.Size(55, 13);
            this.lblRiverParent.TabIndex = 39;
            this.lblRiverParent.Text = "linkLabel1";
            // 
            // lblRiverEndsAt
            // 
            this.lblRiverEndsAt.AutoSize = true;
            this.lblRiverEndsAt.Data = null;
            this.lblRiverEndsAt.ForeColor = System.Drawing.Color.Black;
            this.lblRiverEndsAt.Location = new System.Drawing.Point(68, 67);
            this.lblRiverEndsAt.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblRiverEndsAt.Name = "lblRiverEndsAt";
            this.lblRiverEndsAt.Size = new System.Drawing.Size(55, 13);
            this.lblRiverEndsAt.TabIndex = 37;
            this.lblRiverEndsAt.Text = "linkLabel1";
            // 
            // lblArmyItem
            // 
            this.lblArmyItem.AutoSize = true;
            this.lblArmyItem.Data = null;
            this.lblArmyItem.ForeColor = System.Drawing.Color.Black;
            this.lblArmyItem.Location = new System.Drawing.Point(67, 44);
            this.lblArmyItem.Name = "lblArmyItem";
            this.lblArmyItem.Size = new System.Drawing.Size(55, 13);
            this.lblArmyItem.TabIndex = 45;
            this.lblArmyItem.Text = "linkLabel1";
            // 
            // lblUnitOpponent
            // 
            this.lblUnitOpponent.AutoSize = true;
            this.lblUnitOpponent.Data = null;
            this.lblUnitOpponent.ForeColor = System.Drawing.Color.Black;
            this.lblUnitOpponent.Location = new System.Drawing.Point(67, 287);
            this.lblUnitOpponent.Name = "lblUnitOpponent";
            this.lblUnitOpponent.Size = new System.Drawing.Size(55, 13);
            this.lblUnitOpponent.TabIndex = 74;
            this.lblUnitOpponent.Text = "linkLabel1";
            // 
            // lblUnitSquad
            // 
            this.lblUnitSquad.AutoSize = true;
            this.lblUnitSquad.Data = null;
            this.lblUnitSquad.ForeColor = System.Drawing.Color.Black;
            this.lblUnitSquad.Location = new System.Drawing.Point(67, 265);
            this.lblUnitSquad.Name = "lblUnitSquad";
            this.lblUnitSquad.Size = new System.Drawing.Size(55, 13);
            this.lblUnitSquad.TabIndex = 73;
            this.lblUnitSquad.Text = "linkLabel1";
            // 
            // lblUnitRace
            // 
            this.lblUnitRace.AutoSize = true;
            this.lblUnitRace.Data = null;
            this.lblUnitRace.ForeColor = System.Drawing.Color.Black;
            this.lblUnitRace.Location = new System.Drawing.Point(67, 197);
            this.lblUnitRace.Name = "lblUnitRace";
            this.lblUnitRace.Size = new System.Drawing.Size(55, 13);
            this.lblUnitRace.TabIndex = 72;
            this.lblUnitRace.Text = "linkLabel1";
            // 
            // lblUnitHF
            // 
            this.lblUnitHF.AutoSize = true;
            this.lblUnitHF.Data = null;
            this.lblUnitHF.ForeColor = System.Drawing.Color.Black;
            this.lblUnitHF.Location = new System.Drawing.Point(67, 175);
            this.lblUnitHF.Name = "lblUnitHF";
            this.lblUnitHF.Size = new System.Drawing.Size(55, 13);
            this.lblUnitHF.TabIndex = 71;
            this.lblUnitHF.Text = "linkLabel1";
            // 
            // lblUnitPop
            // 
            this.lblUnitPop.AutoSize = true;
            this.lblUnitPop.Data = null;
            this.lblUnitPop.ForeColor = System.Drawing.Color.Black;
            this.lblUnitPop.Location = new System.Drawing.Point(67, 131);
            this.lblUnitPop.Name = "lblUnitPop";
            this.lblUnitPop.Size = new System.Drawing.Size(55, 13);
            this.lblUnitPop.TabIndex = 70;
            this.lblUnitPop.Text = "linkLabel1";
            // 
            // lblUnitCiv
            // 
            this.lblUnitCiv.AutoSize = true;
            this.lblUnitCiv.Data = null;
            this.lblUnitCiv.ForeColor = System.Drawing.Color.Black;
            this.lblUnitCiv.Location = new System.Drawing.Point(67, 109);
            this.lblUnitCiv.Name = "lblUnitCiv";
            this.lblUnitCiv.Size = new System.Drawing.Size(55, 13);
            this.lblUnitCiv.TabIndex = 69;
            this.lblUnitCiv.Text = "linkLabel1";
            // 
            // lblEngravingCreatedEvent
            // 
            this.lblEngravingCreatedEvent.AutoSize = true;
            this.lblEngravingCreatedEvent.Data = null;
            this.lblEngravingCreatedEvent.ForeColor = System.Drawing.Color.Black;
            this.lblEngravingCreatedEvent.Location = new System.Drawing.Point(67, 241);
            this.lblEngravingCreatedEvent.Name = "lblEngravingCreatedEvent";
            this.lblEngravingCreatedEvent.Size = new System.Drawing.Size(55, 13);
            this.lblEngravingCreatedEvent.TabIndex = 107;
            this.lblEngravingCreatedEvent.Text = "linkLabel1";
            // 
            // lblEngravingArtist
            // 
            this.lblEngravingArtist.AutoSize = true;
            this.lblEngravingArtist.Data = null;
            this.lblEngravingArtist.ForeColor = System.Drawing.Color.Black;
            this.lblEngravingArtist.Location = new System.Drawing.Point(67, 43);
            this.lblEngravingArtist.Name = "lblEngravingArtist";
            this.lblEngravingArtist.Size = new System.Drawing.Size(55, 13);
            this.lblEngravingArtist.TabIndex = 101;
            this.lblEngravingArtist.Text = "linkLabel1";
            // 
            // lblWrittenContentAuthor
            // 
            this.lblWrittenContentAuthor.AutoSize = true;
            this.lblWrittenContentAuthor.Data = null;
            this.lblWrittenContentAuthor.ForeColor = System.Drawing.Color.Black;
            this.lblWrittenContentAuthor.Location = new System.Drawing.Point(81, 43);
            this.lblWrittenContentAuthor.Name = "lblWrittenContentAuthor";
            this.lblWrittenContentAuthor.Size = new System.Drawing.Size(55, 13);
            this.lblWrittenContentAuthor.TabIndex = 95;
            this.lblWrittenContentAuthor.Text = "linkLabel1";
            // 
            // lblSquadEntity
            // 
            this.lblSquadEntity.AutoSize = true;
            this.lblSquadEntity.Data = null;
            this.lblSquadEntity.ForeColor = System.Drawing.Color.Black;
            this.lblSquadEntity.Location = new System.Drawing.Point(67, 65);
            this.lblSquadEntity.Name = "lblSquadEntity";
            this.lblSquadEntity.Size = new System.Drawing.Size(55, 13);
            this.lblSquadEntity.TabIndex = 170;
            this.lblSquadEntity.Text = "linkLabel2";
            // 
            // lblBuildingClaimed
            // 
            this.lblBuildingClaimed.AutoSize = true;
            this.lblBuildingClaimed.Data = null;
            this.lblBuildingClaimed.ForeColor = System.Drawing.Color.Black;
            this.lblBuildingClaimed.Location = new System.Drawing.Point(67, 219);
            this.lblBuildingClaimed.Name = "lblBuildingClaimed";
            this.lblBuildingClaimed.Size = new System.Drawing.Size(55, 13);
            this.lblBuildingClaimed.TabIndex = 138;
            this.lblBuildingClaimed.Text = "linkLabel3";
            // 
            // lblBuildingSquad
            // 
            this.lblBuildingSquad.AutoSize = true;
            this.lblBuildingSquad.Data = null;
            this.lblBuildingSquad.ForeColor = System.Drawing.Color.Black;
            this.lblBuildingSquad.Location = new System.Drawing.Point(67, 329);
            this.lblBuildingSquad.Name = "lblBuildingSquad";
            this.lblBuildingSquad.Size = new System.Drawing.Size(55, 13);
            this.lblBuildingSquad.TabIndex = 135;
            this.lblBuildingSquad.Text = "linkLabel6";
            // 
            // lblBuildingCorpse
            // 
            this.lblBuildingCorpse.AutoSize = true;
            this.lblBuildingCorpse.Data = null;
            this.lblBuildingCorpse.ForeColor = System.Drawing.Color.Black;
            this.lblBuildingCorpse.Location = new System.Drawing.Point(67, 263);
            this.lblBuildingCorpse.Name = "lblBuildingCorpse";
            this.lblBuildingCorpse.Size = new System.Drawing.Size(55, 13);
            this.lblBuildingCorpse.TabIndex = 134;
            this.lblBuildingCorpse.Text = "linkLabel5";
            // 
            // lblBuildingCorpseHF
            // 
            this.lblBuildingCorpseHF.AutoSize = true;
            this.lblBuildingCorpseHF.Data = null;
            this.lblBuildingCorpseHF.ForeColor = System.Drawing.Color.Black;
            this.lblBuildingCorpseHF.Location = new System.Drawing.Point(67, 285);
            this.lblBuildingCorpseHF.Name = "lblBuildingCorpseHF";
            this.lblBuildingCorpseHF.Size = new System.Drawing.Size(55, 13);
            this.lblBuildingCorpseHF.TabIndex = 133;
            this.lblBuildingCorpseHF.Text = "linkLabel4";
            // 
            // lblBuildingOwner
            // 
            this.lblBuildingOwner.AutoSize = true;
            this.lblBuildingOwner.Data = null;
            this.lblBuildingOwner.ForeColor = System.Drawing.Color.Black;
            this.lblBuildingOwner.Location = new System.Drawing.Point(67, 197);
            this.lblBuildingOwner.Name = "lblBuildingOwner";
            this.lblBuildingOwner.Size = new System.Drawing.Size(55, 13);
            this.lblBuildingOwner.TabIndex = 127;
            this.lblBuildingOwner.Text = "linkLabel3";
            // 
            // lblBuildingRace
            // 
            this.lblBuildingRace.AutoSize = true;
            this.lblBuildingRace.Data = null;
            this.lblBuildingRace.ForeColor = System.Drawing.Color.Black;
            this.lblBuildingRace.Location = new System.Drawing.Point(67, 131);
            this.lblBuildingRace.Name = "lblBuildingRace";
            this.lblBuildingRace.Size = new System.Drawing.Size(55, 13);
            this.lblBuildingRace.TabIndex = 126;
            this.lblBuildingRace.Text = "linkLabel2";
            // 
            // lblItemMasterpieceEvent
            // 
            this.lblItemMasterpieceEvent.AutoSize = true;
            this.lblItemMasterpieceEvent.Data = null;
            this.lblItemMasterpieceEvent.ForeColor = System.Drawing.Color.Black;
            this.lblItemMasterpieceEvent.Location = new System.Drawing.Point(91, 241);
            this.lblItemMasterpieceEvent.Name = "lblItemMasterpieceEvent";
            this.lblItemMasterpieceEvent.Size = new System.Drawing.Size(55, 13);
            this.lblItemMasterpieceEvent.TabIndex = 214;
            this.lblItemMasterpieceEvent.Text = "linkLabel2";
            // 
            // lblItemUnit
            // 
            this.lblItemUnit.AutoSize = true;
            this.lblItemUnit.Data = null;
            this.lblItemUnit.ForeColor = System.Drawing.Color.Black;
            this.lblItemUnit.Location = new System.Drawing.Point(90, 374);
            this.lblItemUnit.Name = "lblItemUnit";
            this.lblItemUnit.Size = new System.Drawing.Size(55, 13);
            this.lblItemUnit.TabIndex = 206;
            this.lblItemUnit.Text = "linkLabel2";
            // 
            // lblItemHF
            // 
            this.lblItemHF.AutoSize = true;
            this.lblItemHF.Data = null;
            this.lblItemHF.ForeColor = System.Drawing.Color.Black;
            this.lblItemHF.Location = new System.Drawing.Point(91, 397);
            this.lblItemHF.Name = "lblItemHF";
            this.lblItemHF.Size = new System.Drawing.Size(55, 13);
            this.lblItemHF.TabIndex = 204;
            this.lblItemHF.Text = "linkLabel2";
            // 
            // lblItemImageSIte
            // 
            this.lblItemImageSIte.AutoSize = true;
            this.lblItemImageSIte.Data = null;
            this.lblItemImageSIte.ForeColor = System.Drawing.Color.Black;
            this.lblItemImageSIte.Location = new System.Drawing.Point(61, 82);
            this.lblItemImageSIte.Name = "lblItemImageSIte";
            this.lblItemImageSIte.Size = new System.Drawing.Size(55, 13);
            this.lblItemImageSIte.TabIndex = 157;
            this.lblItemImageSIte.Text = "linkLabel2";
            // 
            // lblItemImageCiv
            // 
            this.lblItemImageCiv.AutoSize = true;
            this.lblItemImageCiv.Data = null;
            this.lblItemImageCiv.ForeColor = System.Drawing.Color.Black;
            this.lblItemImageCiv.Location = new System.Drawing.Point(61, 60);
            this.lblItemImageCiv.Name = "lblItemImageCiv";
            this.lblItemImageCiv.Size = new System.Drawing.Size(55, 13);
            this.lblItemImageCiv.TabIndex = 156;
            this.lblItemImageCiv.Text = "linkLabel2";
            // 
            // lblItemMaker
            // 
            this.lblItemMaker.AutoSize = true;
            this.lblItemMaker.Data = null;
            this.lblItemMaker.ForeColor = System.Drawing.Color.Black;
            this.lblItemMaker.Location = new System.Drawing.Point(91, 131);
            this.lblItemMaker.Name = "lblItemMaker";
            this.lblItemMaker.Size = new System.Drawing.Size(55, 13);
            this.lblItemMaker.TabIndex = 147;
            this.lblItemMaker.Text = "linkLabel2";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1904, 961);
            this.Controls.Add(this.TestButton);
            this.Controls.Add(this.MainTab);
            this.Controls.Add(this.menuStrip);
            this.MainMenuStrip = this.menuStrip;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "World Viewer";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.MainTab.ResumeLayout(false);
            this.tabWorld.ResumeLayout(false);
            this.grpWorld.ResumeLayout(false);
            this.tableLayoutPanel38.ResumeLayout(false);
            this.tableLayoutPanel38.PerformLayout();
            this.tabArtifact.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.grpArtifact.ResumeLayout(false);
            this.tableLayoutPanel18.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.grpArtifactLost.ResumeLayout(false);
            this.grpArtifactLost.PerformLayout();
            this.grpArtifactCreated.ResumeLayout(false);
            this.grpArtifactCreated.PerformLayout();
            this.grpArtifactEvents.ResumeLayout(false);
            this.tabCivilization.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.grpCivilization.ResumeLayout(false);
            this.tableLayoutPanel19.ResumeLayout(false);
            this.grpCivilizationWars.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.grpCivilizationSites.ResumeLayout(false);
            this.grpCivilizationLeaders.ResumeLayout(false);
            this.grpCivilizationGods.ResumeLayout(false);
            this.tabEntity.ResumeLayout(false);
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel5.PerformLayout();
            this.grpEntity.ResumeLayout(false);
            this.tableLayoutPanel20.ResumeLayout(false);
            this.grpEntityPositions.ResumeLayout(false);
            this.grpEntityRelatedSites.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.grpEntitySiteTakeover.ResumeLayout(false);
            this.grpEntitySiteTakeover.PerformLayout();
            this.grpEntityEvents.ResumeLayout(false);
            this.grpEntityCreated.ResumeLayout(false);
            this.grpEntityCreated.PerformLayout();
            this.grpEntityRelatedFigures.ResumeLayout(false);
            this.grpEntityRelatedEntities.ResumeLayout(false);
            this.tabEntityPopulation.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            this.grpEntityPopulation.ResumeLayout(false);
            this.tableLayoutPanel21.ResumeLayout(false);
            this.grpEntityPopluationRaces.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.grpEntityPopulationBattles.ResumeLayout(false);
            this.grpEntityPopulationBattles.PerformLayout();
            this.grpEntityPopulationMembers.ResumeLayout(false);
            this.tabGod.ResumeLayout(false);
            this.tableLayoutPanel6.ResumeLayout(false);
            this.tableLayoutPanel6.PerformLayout();
            this.grpGod.ResumeLayout(false);
            this.tableLayoutPanel22.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.grpGodCivilizations.ResumeLayout(false);
            this.grpGodLeaders.ResumeLayout(false);
            this.tabHistoricalFigure.ResumeLayout(false);
            this.tableLayoutPanel7.ResumeLayout(false);
            this.tableLayoutPanel7.PerformLayout();
            this.grpHistoricalFigure.ResumeLayout(false);
            this.tableLayoutPanel23.ResumeLayout(false);
            this.grpHistoricalFigureArtifacts.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.grpHistoricalFigureEvents.ResumeLayout(false);
            this.grpHistoricalFigureDescendents.ResumeLayout(false);
            this.grpHistoricalFigureSkills.ResumeLayout(false);
            this.grpHistoricalFigureAncestors.ResumeLayout(false);
            this.grpHistoricalFigureDeath.ResumeLayout(false);
            this.grpHistoricalFigureDeath.PerformLayout();
            this.grpHistoricalFigureSpheres.ResumeLayout(false);
            this.grpHistoricalFigureKnowledge.ResumeLayout(false);
            this.grpHistoricalFigurePets.ResumeLayout(false);
            this.grpHistoricalFigureHFLinks.ResumeLayout(false);
            this.grpHistoricalFigureEntityLinks.ResumeLayout(false);
            this.tabHistoricalEra.ResumeLayout(false);
            this.tableLayoutPanel8.ResumeLayout(false);
            this.tableLayoutPanel8.PerformLayout();
            this.grpHistoricalEra.ResumeLayout(false);
            this.grpHistoricalEra.PerformLayout();
            this.tabHistoricalEvent.ResumeLayout(false);
            this.tableLayoutPanel9.ResumeLayout(false);
            this.tableLayoutPanel9.PerformLayout();
            this.tabHistoricalEventCollection.ResumeLayout(false);
            this.tableLayoutPanel10.ResumeLayout(false);
            this.tableLayoutPanel10.PerformLayout();
            this.grpHistoricalEventCollection.ResumeLayout(false);
            this.MainTabEventCollectionTypes.ResumeLayout(false);
            this.tabEventCollectionJourney.ResumeLayout(false);
            this.tableLayoutPanel24.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            this.grpJourneyEvents.ResumeLayout(false);
            this.tabEventCollectionBeastAttack.ResumeLayout(false);
            this.tableLayoutPanel25.ResumeLayout(false);
            this.panel8.ResumeLayout(false);
            this.panel8.PerformLayout();
            this.grpBeastAttackEvents.ResumeLayout(false);
            this.tabEventCollectionWar.ResumeLayout(false);
            this.tableLayoutPanel26.ResumeLayout(false);
            this.panel9.ResumeLayout(false);
            this.panel9.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.grpWarEventCols.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.grpWarEvents.ResumeLayout(false);
            this.tabEventCollectionBattle.ResumeLayout(false);
            this.tableLayoutPanel27.ResumeLayout(false);
            this.panel10.ResumeLayout(false);
            this.panel10.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.grpBattleDefendingSquad.ResumeLayout(false);
            this.grpBattleDefendingSquad.PerformLayout();
            this.grpBattleDefendingHF.ResumeLayout(false);
            this.grpBattleEvents.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.grpBattleAttackingSquad.ResumeLayout(false);
            this.grpBattleAttackingSquad.PerformLayout();
            this.grpBattleAttackingHF.ResumeLayout(false);
            this.grpBattleEventCols.ResumeLayout(false);
            this.grpBattleNonComHFs.ResumeLayout(false);
            this.tabEventCollectionDuel.ResumeLayout(false);
            this.tableLayoutPanel28.ResumeLayout(false);
            this.panel11.ResumeLayout(false);
            this.panel11.PerformLayout();
            this.grpDuelEvents.ResumeLayout(false);
            this.tabEventCollectionAbduction.ResumeLayout(false);
            this.tableLayoutPanel29.ResumeLayout(false);
            this.grpAbductionEventCols.ResumeLayout(false);
            this.panel12.ResumeLayout(false);
            this.panel12.PerformLayout();
            this.grpAbductionEvents.ResumeLayout(false);
            this.tabEventCollectionSiteConquered.ResumeLayout(false);
            this.tableLayoutPanel30.ResumeLayout(false);
            this.grpSiteConqueredEvents.ResumeLayout(false);
            this.panel13.ResumeLayout(false);
            this.panel13.PerformLayout();
            this.tabEventCollectionTheft.ResumeLayout(false);
            this.tableLayoutPanel31.ResumeLayout(false);
            this.grpTheftEventCols.ResumeLayout(false);
            this.panel14.ResumeLayout(false);
            this.panel14.PerformLayout();
            this.grpTheftEvents.ResumeLayout(false);
            this.tabEventCollectionInsurrection.ResumeLayout(false);
            this.tableLayoutPanel39.ResumeLayout(false);
            this.panel20.ResumeLayout(false);
            this.panel20.PerformLayout();
            this.grpInsurrectionEvents.ResumeLayout(false);
            this.tabEventCollectionOccasion.ResumeLayout(false);
            this.tableLayoutPanel57.ResumeLayout(false);
            this.grpOccasionEventCols.ResumeLayout(false);
            this.panel22.ResumeLayout(false);
            this.panel22.PerformLayout();
            this.grpOccasionEvents.ResumeLayout(false);
            this.tabEventCollectionCeremony.ResumeLayout(false);
            this.tableLayoutPanel58.ResumeLayout(false);
            this.panel23.ResumeLayout(false);
            this.panel23.PerformLayout();
            this.grpCeremonyEvents.ResumeLayout(false);
            this.tabEventCollectionProcession.ResumeLayout(false);
            this.tableLayoutPanel59.ResumeLayout(false);
            this.panel24.ResumeLayout(false);
            this.panel24.PerformLayout();
            this.grpProcessionEvents.ResumeLayout(false);
            this.tabEventCollectionPerformance.ResumeLayout(false);
            this.tableLayoutPanel60.ResumeLayout(false);
            this.panel25.ResumeLayout(false);
            this.panel25.PerformLayout();
            this.grpPerformanceEvents.ResumeLayout(false);
            this.tabEventCollectionCompetition.ResumeLayout(false);
            this.tableLayoutPanel61.ResumeLayout(false);
            this.panel26.ResumeLayout(false);
            this.panel26.PerformLayout();
            this.grpCompetitionEvents.ResumeLayout(false);
            this.tabEventCollectionPurge.ResumeLayout(false);
            this.tableLayoutPanel45.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.panel27.ResumeLayout(false);
            this.panel27.PerformLayout();
            this.tabLeader.ResumeLayout(false);
            this.tableLayoutPanel11.ResumeLayout(false);
            this.tableLayoutPanel11.PerformLayout();
            this.grpLeader.ResumeLayout(false);
            this.grpLeader.PerformLayout();
            this.tabParameter.ResumeLayout(false);
            this.tableLayoutPanel12.ResumeLayout(false);
            this.tableLayoutPanel12.PerformLayout();
            this.grpParameter.ResumeLayout(false);
            this.grpParameter.PerformLayout();
            this.tabRace.ResumeLayout(false);
            this.tableLayoutPanel13.ResumeLayout(false);
            this.tableLayoutPanel13.PerformLayout();
            this.grpRace.ResumeLayout(false);
            this.tableLayoutPanel32.ResumeLayout(false);
            this.grpRacePopulation.ResumeLayout(false);
            this.panel15.ResumeLayout(false);
            this.panel15.PerformLayout();
            this.grpRaceCastes.ResumeLayout(false);
            this.grpRaceCastes.PerformLayout();
            this.grpRaceHistoricalFigures.ResumeLayout(false);
            this.grpRaceLeaders.ResumeLayout(false);
            this.grpRaceCivilizations.ResumeLayout(false);
            this.tabRegion.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.grpRegion.ResumeLayout(false);
            this.tableLayoutPanel33.ResumeLayout(false);
            this.grpRegionPopulation.ResumeLayout(false);
            this.panel16.ResumeLayout(false);
            this.panel16.PerformLayout();
            this.grpRegionEvents.ResumeLayout(false);
            this.grpRegionBattles.ResumeLayout(false);
            this.grpRegionInhabitants.ResumeLayout(false);
            this.tabSite.ResumeLayout(false);
            this.tableLayoutPanel14.ResumeLayout(false);
            this.tableLayoutPanel14.PerformLayout();
            this.grpSite.ResumeLayout(false);
            this.tableLayoutPanel34.ResumeLayout(false);
            this.grpSiteArtifacts.ResumeLayout(false);
            this.grpSiteStructures.ResumeLayout(false);
            this.panel17.ResumeLayout(false);
            this.panel17.PerformLayout();
            this.grpSiteOutcasts.ResumeLayout(false);
            this.grpSiteCreated.ResumeLayout(false);
            this.grpSiteCreated.PerformLayout();
            this.grpSitePrisoners.ResumeLayout(false);
            this.grpSiteInhabitants.ResumeLayout(false);
            this.grpSitePopulation.ResumeLayout(false);
            this.grpSiteEventCollection.ResumeLayout(false);
            this.grpSiteEvent.ResumeLayout(false);
            this.tabStructure.ResumeLayout(false);
            this.tableLayoutPanel36.ResumeLayout(false);
            this.tableLayoutPanel36.PerformLayout();
            this.grpStructure.ResumeLayout(false);
            this.tableLayoutPanel37.ResumeLayout(false);
            this.grpStructureEntombedHF.ResumeLayout(false);
            this.grpStructureEvents.ResumeLayout(false);
            this.grpStructureRazed.ResumeLayout(false);
            this.grpStructureRazed.PerformLayout();
            this.grpStructureCreated.ResumeLayout(false);
            this.grpStructureCreated.PerformLayout();
            this.panel19.ResumeLayout(false);
            this.panel19.PerformLayout();
            this.tabUndergroundRegion.ResumeLayout(false);
            this.tableLayoutPanel15.ResumeLayout(false);
            this.tableLayoutPanel15.PerformLayout();
            this.grpUndergroundRegion.ResumeLayout(false);
            this.tableLayoutPanel40.ResumeLayout(false);
            this.grpUndergroundRegionPopulation.ResumeLayout(false);
            this.panel21.ResumeLayout(false);
            this.panel21.PerformLayout();
            this.tabWorldConstruction.ResumeLayout(false);
            this.tableLayoutPanel16.ResumeLayout(false);
            this.tableLayoutPanel16.PerformLayout();
            this.grpWorldConstruction.ResumeLayout(false);
            this.grpWorldConstruction.PerformLayout();
            this.grpWorldConstructionCreated.ResumeLayout(false);
            this.grpWorldConstructionCreated.PerformLayout();
            this.tabDynasty.ResumeLayout(false);
            this.tableLayoutPanel17.ResumeLayout(false);
            this.tableLayoutPanel17.PerformLayout();
            this.grpDynasty.ResumeLayout(false);
            this.tableLayoutPanel35.ResumeLayout(false);
            this.panel18.ResumeLayout(false);
            this.panel18.PerformLayout();
            this.grpDynastyMembers.ResumeLayout(false);
            this.tabLandmass.ResumeLayout(false);
            this.tableLayoutPanel47.ResumeLayout(false);
            this.tableLayoutPanel47.PerformLayout();
            this.grpLandmass.ResumeLayout(false);
            this.grpLandmass.PerformLayout();
            this.tabMountain.ResumeLayout(false);
            this.tableLayoutPanel41.ResumeLayout(false);
            this.tableLayoutPanel41.PerformLayout();
            this.grpMountain.ResumeLayout(false);
            this.grpMountain.PerformLayout();
            this.tabRiver.ResumeLayout(false);
            this.tableLayoutPanel42.ResumeLayout(false);
            this.tableLayoutPanel42.PerformLayout();
            this.grpRiver.ResumeLayout(false);
            this.grpRiver.PerformLayout();
            this.grpRiverTributaries.ResumeLayout(false);
            this.tabArmy.ResumeLayout(false);
            this.tableLayoutPanel43.ResumeLayout(false);
            this.tableLayoutPanel43.PerformLayout();
            this.grpArmy.ResumeLayout(false);
            this.grpArmy.PerformLayout();
            this.tabUnit.ResumeLayout(false);
            this.tableLayoutPanel44.ResumeLayout(false);
            this.tableLayoutPanel44.PerformLayout();
            this.grpUnit.ResumeLayout(false);
            this.grpUnit.PerformLayout();
            this.grpUnitReferences.ResumeLayout(false);
            this.grpUnitOwnedBuildings.ResumeLayout(false);
            this.grpUnitInventory.ResumeLayout(false);
            this.grpUnitItems.ResumeLayout(false);
            this.grpUnitHealth.ResumeLayout(false);
            this.grpUnitRelations.ResumeLayout(false);
            this.grpUnitFlags.ResumeLayout(false);
            this.grpUnitLabors.ResumeLayout(false);
            this.tabEngraving.ResumeLayout(false);
            this.tableLayoutPanel46.ResumeLayout(false);
            this.tableLayoutPanel46.PerformLayout();
            this.grpEngraving.ResumeLayout(false);
            this.grpEngraving.PerformLayout();
            this.tabReport.ResumeLayout(false);
            this.tableLayoutPanel50.ResumeLayout(false);
            this.tableLayoutPanel50.PerformLayout();
            this.grpReport.ResumeLayout(false);
            this.grpReport.PerformLayout();
            this.tabWrittenContent.ResumeLayout(false);
            this.tableLayoutPanel51.ResumeLayout(false);
            this.tableLayoutPanel51.PerformLayout();
            this.grpWrittenContent.ResumeLayout(false);
            this.grpWrittenContent.PerformLayout();
            this.grpWrittenContentReferences.ResumeLayout(false);
            this.tabPoeticForm.ResumeLayout(false);
            this.tableLayoutPanel62.ResumeLayout(false);
            this.tableLayoutPanel62.PerformLayout();
            this.grpPoeticForm.ResumeLayout(false);
            this.grpPoeticForm.PerformLayout();
            this.tabMusicalForm.ResumeLayout(false);
            this.tableLayoutPanel63.ResumeLayout(false);
            this.tableLayoutPanel63.PerformLayout();
            this.grpMusicalForm.ResumeLayout(false);
            this.grpMusicalForm.PerformLayout();
            this.tabDanceForm.ResumeLayout(false);
            this.tableLayoutPanel64.ResumeLayout(false);
            this.tableLayoutPanel64.PerformLayout();
            this.grpDanceForm.ResumeLayout(false);
            this.grpDanceForm.PerformLayout();
            this.tabSquad.ResumeLayout(false);
            this.tableLayoutPanel56.ResumeLayout(false);
            this.tableLayoutPanel56.PerformLayout();
            this.grpSquad.ResumeLayout(false);
            this.grpSquad.PerformLayout();
            this.grpSquadMembers.ResumeLayout(false);
            this.tabBuilding.ResumeLayout(false);
            this.tableLayoutPanel52.ResumeLayout(false);
            this.tableLayoutPanel52.PerformLayout();
            this.grpBuilding.ResumeLayout(false);
            this.grpBuilding.PerformLayout();
            this.grpBuildingReferences.ResumeLayout(false);
            this.tabConstruction.ResumeLayout(false);
            this.tableLayoutPanel53.ResumeLayout(false);
            this.tableLayoutPanel53.PerformLayout();
            this.grpConstruction.ResumeLayout(false);
            this.grpConstruction.PerformLayout();
            this.tabItem.ResumeLayout(false);
            this.tableLayoutPanel54.ResumeLayout(false);
            this.tableLayoutPanel54.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.grpItem.ResumeLayout(false);
            this.grpItem.PerformLayout();
            this.grpItemReferences.ResumeLayout(false);
            this.grpItemImage.ResumeLayout(false);
            this.grpItemImage.PerformLayout();
            this.grpItemImprovement.ResumeLayout(false);
            this.grpItemIngredient.ResumeLayout(false);
            this.tabPlant.ResumeLayout(false);
            this.tableLayoutPanel55.ResumeLayout(false);
            this.tableLayoutPanel55.PerformLayout();
            this.grpPlant.ResumeLayout(false);
            this.grpPlant.PerformLayout();
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public TabControl MainTab;
        public TabPage tabArtifact;
        public TabPage tabCivilization;
        public TabPage tabEntity;
        public TabPage tabEntityPopulation;
        public TabPage tabGod;
        public TabPage tabHistoricalFigure;
        public TabPage tabHistoricalEra;
        public TabPage tabHistoricalEvent;
        public TabPage tabHistoricalEventCollection;
        public TabPage tabLeader;
        public TabPage tabParameter;
        public TabPage tabRace;
        public TabPage tabRegion;
        public TabPage tabSite;
        public TabPage tabUndergroundRegion;
        public TabPage tabWorld;
        public TabPage tabWorldConstruction;
        public GroupBox grpArtifact;
        public ListBox lstArtifact;
        public GroupBox grpCivilization;
        public ListBox lstCivilization;
        public GroupBox grpEntity;
        public ListBox lstEntity;
        public GroupBox grpEntityPopulation;
        public ListBox lstEntityPopulation;
        public GroupBox grpGod;
        public ListBox lstGod;
        public GroupBox grpHistoricalFigure;
        public ListBox lstHistoricalFigure;
        public GroupBox grpHistoricalEra;
        public ListBox lstHistoricalEra;
        public GroupBox grpHistoricalEvent;
        public ListBox lstHistoricalEvent;
        public GroupBox grpHistoricalEventCollection;
        public ListBox lstHistoricalEventCollection;
        public ListBox lstLeader;
        public GroupBox grpParameter;
        public ListBox lstParameter;
        public GroupBox grpRace;
        public ListBox lstRace;
        public GroupBox grpRegion;
        public ListBox lstRegion;
        public GroupBox grpSite;
        public ListBox lstSite;
        public GroupBox grpUndergroundRegion;
        public ListBox lstUndergroundRegion;
        public GroupBox grpWorld;
        public GroupBox grpWorldConstruction;
        public ListBox lstWorldConstruction;
        private Label label5;
        private Label label4;
        private Label label2;
        public LinkLabel lblArtifactCreatedSite;
        public LinkLabel lblArtifactCreatedBy;
        public GroupBox grpArtifactCreated;
        public Label lblArtifactItem;
        private Label label6;
        public LinkLabel lblArtifactCreatedTime;
        public ListBox lstArtifactEvents;
        public LinkLabel lblArtifactLostTime;
        public LinkLabel lblArtifactLostSite;
        private Label label12;
        private Label label13;
        public GroupBox grpArtifactLost;
        public GroupBox grpArtifactEvents;
        public Label lblCivilizationName;
        private Label label17;
        public Label lblCivilizationFull;
        private Label label15;
        private Label label16;
        public ListBox lstCivilizationGods;
        public ListBox lstCivilizationLeaders;
        public LinkLabel lblCivilizationEntity;
        public GroupBox grpCivilizationGods;
        public GroupBox grpCivilizationLeaders;
        public LinkLabel lblCivilizationRace;
        private Label label14;
        public LinkLabel lblEntityCivilization;
        private Label label21;
        public LinkLabel lblEntityRace;
        private Label label18;
        public Label lblEntityName;
        private Label label20;
        public GroupBox grpEntityRelatedFigures;
        public TreeView trvEntityRelatedFigures;
        public GroupBox grpEntityCreated;
        public LinkLabel lblEntityCreatedTime;
        public LinkLabel lblEntityCreatedSite;
        private Label label19;
        private Label label22;
        public LinkLabel lblEntityParentCiv;
        private Label label23;
        public GroupBox grpEntitySiteTakeover;
        public LinkLabel lblEntitySiteTakeoverSite;
        private Label label24;
        public LinkLabel lblEntitySiteTakeoverTime;
        public LinkLabel lblEntitySiteTakeoverNewLeader;
        private Label label28;
        private Label label29;
        public LinkLabel lblEntitySiteTakeoverDefenderEntity;
        public LinkLabel lblEntitySiteTakeoverDefenderCiv;
        private Label label26;
        private Label label27;
        public GroupBox grpEntityPopulationBattles;
        public ListBox lstEntityPopulationBattles;
        public LinkLabel lblEntityPopulationBattleWar;
        private Label label30;
        private Label label31;
        private Label label32;
        public LinkLabel lblEntityPopulationBattleTime;
        private Label label36;
        public Label lblEntityPopulationBattleDeaths;
        public Label lblEntityPopulationBattleNumber;
        public LinkLabel lblEntityPopulationRace;
        private Label label25;
        public GroupBox grpGodCivilizations;
        public ListBox lstGodCivilizations;
        public GroupBox grpGodLeaders;
        public ListBox lstGodLeaders;
        private Label label34;
        public Label lblGodType;
        private Label label37;
        public Label lblGodName;
        private Label label39;
        private Label label38;
        public Label lblGodSpheres;
        public LinkLabel lblGodHF;
        public GroupBox grpHistoricalFigureAncestors;
        public TreeView trvHistoricalFigureAncestors;
        public GroupBox grpHistoricalFigureDescendents;
        public TreeView trvHistoricalFigureDescendents;
        public Label lblHistoricalFigureGhost;
        private Label label61;
        public Label lblHistoricalFigureAnimated;
        private Label label63;
        private Label label57;
        private Label label59;
        private Label label55;
        public Label lblHistoricalFigureAssociatedType;
        private Label label49;
        public Label lblHistoricalFigureLife;
        private Label label47;
        private Label label35;
        public Label lblHistoricalFigureAppeared;
        private Label label41;
        private Label label43;
        public Label lblHistoricalFigureName;
        private Label label45;
        public GroupBox grpEntityPopulationMembers;
        public ListBox lstEntityPopulationMembers;
        public LinkLabel lblHistoricalFigureLeader;
        public LinkLabel lblHistoricalFigureGod;
        public LinkLabel lblHistoricalFigureUnit;
        public Label lblHistoricalFigureCaste;
        public LinkLabel lblHistoricalFigureRace;
        public GroupBox grpHistoricalFigureDeath;
        public LinkLabel lblHistoricalFigureDeathLocation;
        public LinkLabel lblHistoricalFigureDeathSlayer;
        private Label label40;
        private Label label42;
        private Label label44;
        public Label lblHistoricalFigureDeathCause;
        public LinkLabel lblHistoricalFigureDeathTime;
        private Label label46;
        public GroupBox grpHistoricalFigureHFLinks;
        public TreeView trvHistoricalFigureHFLinks;
        public GroupBox grpHistoricalFigureEntityLinks;
        public TreeView trvHistoricalFigureEntityLinks;
        public Label lblEntityType;
        private Label label50;
        public LinkLabel lblHistoricalFigureLocation;
        public GroupBox grpHistoricalFigureSkills;
        public ListBox lstHistoricalFigureSkills;
        public GroupBox grpHistoricalFigureKnowledge;
        public ListBox lstHistoricalFigureKnowledge;
        public GroupBox grpHistoricalFigureSpheres;
        public ListBox lstHistoricalFigureSpheres;
        public GroupBox grpHistoricalFigurePets;
        public ListBox lstHistoricalFigurePets;
        public Label lblHistoricalEraStartYear;
        private Label label52;
        public Label lblHistoricalEraName;
        private Label label54;
        public GroupBox grpLeader;
        private Label label67;
        public LinkLabel lblLeaderRace;
        public Label lblLeaderLife;
        public LinkLabel lblLeaderGod;
        public LinkLabel lblLeaderCivilization;
        public LinkLabel lblLeaderSite;
        private Label label64;
        private Label label65;
        private Label label66;
        private Label label68;
        public Label lblLeaderInheritance;
        private Label label70;
        private Label label71;
        public Label lblLeaderReignBegan;
        private Label label73;
        private Label label74;
        public Label lblLeaderName;
        private Label label76;
        public LinkLabel lblLeaderHF;
        private Label label51;
        public Label lblLeaderType;
        public LinkLabel lblLeaderInheritedFrom;
        public Label lblParameterData;
        private Label label56;
        public Label lblParameterName;
        private Label label60;
        public Label lblRaceName;
        private Label label58;
        public GroupBox grpRaceHistoricalFigures;
        public ListBox lstRaceHistoricalFigures;
        public GroupBox grpRaceCivilizations;
        public ListBox lstRaceCivilizations;
        public GroupBox grpRaceLeaders;
        public ListBox lstRaceLeaders;
        public Label lblRegionType;
        private Label label62;
        public Label lblRegionName;
        private Label label72;
        public GroupBox grpRegionBattles;
        public ListBox lstRegionBattles;
        public GroupBox grpRegionEvents;
        public ListBox lstRegionEvents;
        private Label label53;
        private Label label75;
        public Label lblSiteAltName;
        private Label label78;
        public Label lblSiteName;
        private Label label80;
        public Label lblSiteType;
        private Label label81;
        public GroupBox grpSitePopulation;
        public ListBox lstSitePopulation;
        public GroupBox grpSitePrisoners;
        public ListBox lstSitePrisoners;
        public GroupBox grpSiteInhabitants;
        public ListBox lstSiteInhabitants;
        public LinkLabel lblSiteParentCiv;
        public LinkLabel lblSiteOwner;
        public GroupBox grpSiteCreated;
        public LinkLabel lblSiteCreatedTime;
        public LinkLabel lblSiteCreatedByCiv;
        public LinkLabel lblSiteCreatedBy;
        private Label label69;
        private Label label77;
        private Label label79;
        public GroupBox grpSiteEvent;
        public GroupBox grpSiteEventCollection;
        public TreeView trvSiteEventCollection;
        public LinkLabel lblSiteCoord;
        private Label label83;
        public Label lblUndergroundRegionType;
        private Label label84;
        public Label lblUndergroundRegionDepth;
        private Label label86;
        public LinkLabel lblWorldConstructionTo;
        private Label label82;
        private Label label89;
        private Label label91;
        public LinkLabel lblWorldConstructionFrom;
        public LinkLabel lblWorldConstructionMaster;
        public GroupBox grpWorldConstructionCreated;
        public LinkLabel lblWorldConstructionCreatedTime;
        public LinkLabel lblWorldConstructionCreatedByCiv;
        public LinkLabel lblWorldConstructionCreatedBy;
        private Label label85;
        private Label label87;
        private Label label88;
        private MenuStrip menuStrip;
        private ToolStripMenuItem loadWorldToolStripMenuItem;
        public ToolStripMenuItem showMapToolStripMenuItem;
        public GroupBox grpCivilizationSites;
        public ListBox lstCivilizationSites;
        public TabControl MainTabEventCollectionTypes;
        public TabPage tabEventCollectionJourney;
        public TabPage tabEventCollectionBeastAttack;
        public TabPage tabEventCollectionWar;
        public TabPage tabEventCollectionBattle;
        public TabPage tabEventCollectionDuel;
        public TabPage tabEventCollectionAbduction;
        public TabPage tabEventCollectionSiteConquered;
        public TabPage tabEventCollectionTheft;
        public Label lblBattleTime;
        private Label label92;
        public LinkLabel lblBattleWar;
        private Label label94;
        public Label lblBattleName;
        private Label label96;
        public LinkLabel lblBattleCoord;
        private Label label97;
        public LinkLabel lblBattleSite;
        private Label label90;
        public LinkLabel lblBattleRegion;
        private Label label95;
        public Label lblBattleDuration;
        private Label label98;
        public Label lblWarDuration;
        private Label label99;
        public Label lblWarTime;
        private Label label101;
        public Label lblWarName;
        private Label label103;
        private GroupBox groupBox2;
        public Label lblBattleAttackingSquadDeaths;
        private Label label105;
        public Label lblBattleAttackingSquadNumber;
        private Label label107;
        private Label label93;
        private Label label100;
        private Label label102;
        public LinkLabel lblBattleAttackingSquadRace;
        public LinkLabel lblBattleAttackingSquadEntPop;
        public LinkLabel lblBattleAttackingSquadSite;
        private GroupBox groupBox5;
        public Label lblBattleDefendingSquadDeaths;
        private Label label106;
        public Label lblBattleDefendingSquadNumber;
        private Label label109;
        private Label label110;
        private Label label111;
        private Label label112;
        public LinkLabel lblBattleDefendingSquadRace;
        public LinkLabel lblBattleDefendingSquadEntPop;
        public LinkLabel lblBattleDefendingSquadSite;
        public ListBox lstBattleAttackingSquad;
        public ListBox lstBattleAttackingHF;
        public ListBox lstBattleDefendingSquad;
        public ListBox lstBattleDefendingHF;
        public GroupBox grpBattleAttackingSquad;
        public GroupBox grpBattleAttackingHF;
        public GroupBox grpBattleDefendingSquad;
        public GroupBox grpBattleDefendingHF;
        public Label lblBattleAttackerOutcome;
        public Label lblBattleDefenderOutcome;
        private Label label113;
        private Label label114;
        private Label label108;
        private Label label104;
        public Label lblBattleDefenderLosses;
        public Label lblBattleDefenderCombatants;
        public Label lblBattleAttackerLosses;
        public Label lblBattleAttackerCombatants;
        public GroupBox grpBattleEventCols;
        public ListBox lstBattleEventCols;
        public GroupBox grpBattleEvents;
        public ListBox lstBattleEvents;
        public GroupBox grpBattleNonComHFs;
        public ListBox lstBattleNonComHFs;
        public GroupBox grpWarEventCols;
        public ListBox lstWarEventCols;
        public GroupBox grpWarEvents;
        public ListBox lstWarEvents;
        public LinkLabel lblWarDefender;
        private GroupBox groupBox4;
        private GroupBox groupBox3;
        public LinkLabel lblWarAggressor;
        public Label lblWarDefenderWins;
        private Label label124;
        public Label lblWarDefenderSquads;
        private Label label126;
        public Label lblWarDefenderLosses;
        public Label lblWarDefenderCombatants;
        private Label label129;
        private Label label130;
        public Label lblWarAggressorWins;
        private Label label122;
        public Label lblWarAggressorSquads;
        private Label label120;
        public Label lblWarAggressorLosses;
        public Label lblWarAggressorCombatants;
        private Label label117;
        private Label label118;
        public Label lblSiteConqueredOrdinal;
        private Label label151;
        private Label label150;
        public LinkLabel lblSiteConqueredDefender;
        public LinkLabel lblSiteConqueredAttacker;
        public GroupBox grpSiteConqueredEvents;
        public ListBox lstSiteConqueredEvents;
        public Label lblSiteConqueredDuration;
        private Label label116;
        private Label label119;
        private Label label121;
        public Label lblSiteConqueredTime;
        private Label label127;
        private Label label128;
        public LinkLabel lblSiteConqueredCoords;
        public LinkLabel lblSiteConqueredSite;
        public LinkLabel lblSiteConqueredWar;
        private Label label115;
        private Label label140;
        public LinkLabel lblAbductionRegion;
        private Label label123;
        public Label lblAbductionOrdinal;
        private Label label131;
        private Label label132;
        public LinkLabel lblAbductionDefender;
        public LinkLabel lblAbductionAttacker;
        public GroupBox grpAbductionEvents;
        public ListBox lstAbductionEvents;
        public Label lblAbductionDuration;
        private Label label134;
        private Label label135;
        private Label label136;
        public Label lblAbductionTime;
        private Label label138;
        private Label label139;
        public LinkLabel lblAbductionCoords;
        public LinkLabel lblAbductionSite;
        public LinkLabel lblAbductionParent;
        private Label label125;
        public LinkLabel lblDuelRegion;
        private Label label133;
        public Label lblDuelOrdinal;
        private Label label141;
        private Label label142;
        public LinkLabel lblDuelDefender;
        public LinkLabel lblDuelAttacker;
        public GroupBox grpDuelEvents;
        public ListBox lstDuelEvents;
        public Label lblDuelDuration;
        private Label label144;
        private Label label145;
        private Label label146;
        public Label lblDuelTime;
        private Label label148;
        private Label label149;
        public LinkLabel lblDuelCoords;
        public LinkLabel lblDuelSite;
        public LinkLabel lblDuelParent;
        private Label label137;
        public LinkLabel lblBeastAttackRegion;
        private Label label143;
        public Label lblBeastAttackOrdinal;
        private Label label152;
        public LinkLabel lblBeastAttackDefender;
        public GroupBox grpBeastAttackEvents;
        public ListBox lstBeastAttackEvents;
        public Label lblBeastAttackDuration;
        private Label label155;
        private Label label156;
        private Label label157;
        public Label lblBeastAttackTime;
        private Label label159;
        private Label label160;
        public LinkLabel lblBeastAttackCoords;
        public LinkLabel lblBeastAttackSite;
        public LinkLabel lblBeastAttackParent;
        private Label label147;
        public Label lblJourneyOrdinal;
        public GroupBox grpJourneyEvents;
        public ListBox lstJourneyEvents;
        public Label lblJourneyDuration;
        private Label label158;
        public Label lblJourneyTime;
        private Label label162;
        private Label label153;
        public Label lblTheftOrdinal;
        private Label label161;
        private Label label163;
        public GroupBox grpTheftEvents;
        public ListBox lstTheftEvents;
        public Label lblTheftDuration;
        private Label label165;
        private Label label166;
        private Label label167;
        public Label lblTheftTime;
        private Label label169;
        private Label label170;
        public LinkLabel lblTheftDefender;
        public LinkLabel lblTheftAttacker;
        public LinkLabel lblTheftCoords;
        public LinkLabel lblTheftSite;
        public LinkLabel lblTheftWar;
        public Label lblHistoricalFigureAge;
        public Label lblHistoricalFigureAgeCaption;
        private Button FilterArtifact;
        private Button FilterHistoricalFigure;
        private Button FilterCivilization;
        private Button FilterEntity;
        private Button FilterEntityPopulation;
        private Button FilterGod;
        private Button FilterHistoricalEra;
        private Button FilterHistoricalEvent;
        private Button FilterHistoricalEventCollection;
        private Button FilterLeader;
        private Button FilterParameter;
        private Button FilterRace;
        private Button FilterRegion;
        private Button FilterSite;
        private Button FilterUndergroundRegion;
        private Button FilterWorldConstruction;
        private Label label1;
        public Label lblArtifactName;
        private TextBox TextFilterHistoricalFigure;
        private TextBox TextFilterArtifact;
        private TextBox TextFilterCivilization;
        private TextBox TextFilterEntity;
        private TextBox TextFilterEntityPopulation;
        private TextBox TextFilterGod;
        private TextBox TextFilterHistoricalEra;
        private TextBox TextFilterHistoricalEvent;
        private TextBox TextFilterHistoricalEventCollection;
        private TextBox TextFilterLeader;
        private TextBox TextFilterParameter;
        private TextBox TextFilterRace;
        private TextBox TextFilterRegion;
        private TextBox TextFilterSite;
        private TextBox TextFilterUndergroundRegion;
        public GroupBox grpSiteOutcasts;
        public ListBox lstSiteOutcasts;
        public TabPage tabDynasty;
        private TextBox TextFilterDynasty;
        private Button FilterDynasty;
        public GroupBox grpDynasty;
        public GroupBox grpDynastyMembers;
        public ListBox lstDynastyMembers;
        public ListBox lstDynasty;
        public LinkLabel lblDynastyFounder;
        private Label label154;
        public LinkLabel lblDynastyCivilization;
        private Label label164;
        public Label lblDynastyType;
        private Label label171;
        private Label label173;
        public LinkLabel lblDynastyLength;
        private Label label168;
        public LinkLabel lblBeastAttackBeast;
        private TableLayoutPanel tableLayoutPanel2;
        private TableLayoutPanel tableLayoutPanel3;
        private TableLayoutPanel tableLayoutPanel1;
        private TableLayoutPanel tableLayoutPanel5;
        private TableLayoutPanel tableLayoutPanel4;
        private TableLayoutPanel tableLayoutPanel6;
        private TableLayoutPanel tableLayoutPanel7;
        private TableLayoutPanel tableLayoutPanel8;
        private TableLayoutPanel tableLayoutPanel9;
        private TableLayoutPanel tableLayoutPanel10;
        private TableLayoutPanel tableLayoutPanel11;
        private TableLayoutPanel tableLayoutPanel12;
        private TableLayoutPanel tableLayoutPanel13;
        private TableLayoutPanel tableLayoutPanel14;
        private TableLayoutPanel tableLayoutPanel15;
        private TableLayoutPanel tableLayoutPanel16;
        private TextBox TextFilterWorldConstruction;
        public Label lblRacePopulation;
        private Label label174;
        public ToolStripMenuItem exportWorldToolStripMenuItem;
        public LinkLabel lblHistoricalFigureCoords;
        private Label label172;
        public Label lblHistoricalFigureLocationText;
        public GroupBox grpRegionInhabitants;
        public ListBox lstRegionInhabitants;
        public GroupBox grpEntityEvents;
        public ListBox lstEntityEvents;
        private ToolStripMenuItem BacktoolStripMenuItem;
        private TableLayoutPanel tableLayoutPanel17;
        public ToolStripMenuItem timelineToolStripMenuItem;
        private TableLayoutPanel tableLayoutPanel18;
        private Panel panel1;
        private TableLayoutPanel tableLayoutPanel19;
        private Panel panel2;
        private TableLayoutPanel tableLayoutPanel20;
        private Panel panel3;
        public GroupBox grpEntityRelatedEntities;
        private TableLayoutPanel tableLayoutPanel21;
        private Panel panel4;
        private TableLayoutPanel tableLayoutPanel22;
        private Panel panel5;
        private TableLayoutPanel tableLayoutPanel23;
        private Panel panel6;
        public GroupBox grpHistoricalFigureEvents;
        public ListBox lstHistoricalFigureEvents;
        private TableLayoutPanel tableLayoutPanel24;
        private Panel panel7;
        private TableLayoutPanel tableLayoutPanel25;
        private Panel panel8;
        private TableLayoutPanel tableLayoutPanel26;
        private Panel panel9;
        private TableLayoutPanel tableLayoutPanel27;
        private Panel panel10;
        private TableLayoutPanel tableLayoutPanel28;
        private Panel panel11;
        private TableLayoutPanel tableLayoutPanel29;
        private Panel panel12;
        private TableLayoutPanel tableLayoutPanel30;
        private Panel panel13;
        private TableLayoutPanel tableLayoutPanel31;
        private Panel panel14;
        private TableLayoutPanel tableLayoutPanel32;
        private Panel panel15;
        private TableLayoutPanel tableLayoutPanel33;
        private Panel panel16;
        private TableLayoutPanel tableLayoutPanel34;
        private Panel panel17;
        private TableLayoutPanel tableLayoutPanel35;
        private Panel panel18;
        private ToolStripMenuItem statsToolStripMenuItem;
        private ToolStripMenuItem visualizationsToolStripMenuItem;
        private TreeView WorldSummaryTree;
        public RichTextBox IssuesBox;
        public TextBox StatusBox;
        public GroupBox grpSiteStructures;
        public ListBox lstSiteStructures;
        private TableLayoutPanel tableLayoutPanel36;
        private TextBox TextFilterStructure;
        public GroupBox grpStructure;
        private TableLayoutPanel tableLayoutPanel37;
        private Panel panel19;
        private Label label3;
        public Label lblStructureID;
        private Label label8;
        private Button FilterStructure;
        public ListBox lstStructure;
        public LinkLabel lblStructureSite;
        public TabPage tabStructure;
        public GroupBox grpStructureCreated;
        public LinkLabel lblStructureCreatedSite;
        public LinkLabel lblStructureCreatedCiv;
        public LinkLabel lblStructureCreatedSiteCiv;
        private Label label9;
        private Label label10;
        private Label label11;
        public GroupBox grpStructureRazed;
        public GroupBox grpStructureEvents;
        public LinkLabel lblStructureCreatedTime;
        private Label label7;
        public LinkLabel lblStructureRazedTime;
        private Label label48;
        public LinkLabel lblStructureRazedSite;
        public LinkLabel lblStructureRazedCiv;
        private Label label175;
        private Label label177;
        private TableLayoutPanel tableLayoutPanel38;
        private ToolStripMenuItem closeWorldToolStripMenuItem;
        public LinkLabel lblLeaderMarried;
        private Label label176;
        public GroupBox grpHistoricalFigureArtifacts;
        public ListBox lstHistoricalFigureArtifacts;
        public GroupBox grpSiteArtifacts;
        public ListBox lstSiteArtifacts;
        public GroupBox grpStructureEntombedHF;
        public ListBox lstStructureEntombedHF;
        private ToolStripMenuItem ForwardtoolStripMenuItem;
        public ListBox lstStructureEvents;
        public GroupBox grpAbductionEventCols;
        public ListBox lstAbductionEventCols;
        public GroupBox grpTheftEventCols;
        public ListBox lstTheftEventCols;
        public TabPage tabEventCollectionInsurrection;
        private TableLayoutPanel tableLayoutPanel39;
        private Panel panel20;
        private Label label178;
        public LinkLabel lblInsurrectionParent;
        public LinkLabel lblInsurrectionSite;
        private Label label180;
        public LinkLabel lblInsurrectionCoords;
        public Label lblInsurrectionOrdinal;
        public LinkLabel lblInsurrectionTargetEnt;
        private Label label183;
        private Label label184;
        public Label lblInsurrectionDuration;
        public Label lblInsurrectionTime;
        private Label label187;
        private Label label188;
        private Label label189;
        public GroupBox grpInsurrectionEvents;
        public ListBox lstInsurrectionEvents;
        public LinkLabel lblInsurrectionOutcome;
        private Label label179;
        public GroupBox grpCivilizationWars;
        public ListBox lstCivilizationWars;
        private Label label181;
        public Label lblArtifactDescription;
        public GroupBox grpEntityRelatedSites;
        public TreeView trvEntityRelatedSites;
        public TreeView trvEntityRelatedEntities;
        private Label label182;
        public Label lblStructureType;
        private Label label185;
        public LinkLabel lblEntityWorshippingHF;
        public GroupBox grpEntityPopluationRaces;
        public ListBox lstEntityPopluationRaces;
        private Label label186;
        public LinkLabel lblEntityPopulationCiv;
        public LinkLabel lblWorldConstructionType;
        private Label label33;
        public LinkLabel lblWorldConstructionCoord;
        private Label label190;
        private Label label191;
        public Label lblArtifactValue;
        public LinkLabel SiteMapLabel;
        public GroupBox grpRaceCastes;
        public Label label193;
        public Label lblRaceCasteDescription;
        public ListBox lstRaceCastes;
        public Label lblRaceCasteGender;
        public GroupBox grpRegionPopulation;
        public ListBox lstRegionPopulation;
        public GroupBox grpRacePopulation;
        public ListBox lstRacePopulation;
        private TableLayoutPanel tableLayoutPanel40;
        public GroupBox grpUndergroundRegionPopulation;
        public ListBox lstUndergroundRegionPopulation;
        private Panel panel21;
        public ListBox lstSiteEvent;
        public CheckBox chkHistoricalFigureDetailedView;
        public TabPage tabMountain;
        private TableLayoutPanel tableLayoutPanel41;
        private Button FilterMountain;
        private TextBox TextFilterMountain;
        public GroupBox grpMountain;
        public Label lblMountainHeight;
        private Label label198;
        private Label label195;
        public Label lblMountainAltName;
        private Label label194;
        public Label lblMountainName;
        private Label label196;
        public ListBox lstMountain;
        public LinkLabel lblMountainCoord;
        public TabPage tabRiver;
        private TableLayoutPanel tableLayoutPanel42;
        private Button FilterRiver;
        private TextBox TextFilterRiver;
        public GroupBox grpRiver;
        public LinkLabel lblRiverEndsAt;
        public Label lblRiverElevation;
        private Label label197;
        private Label label199;
        public Label lblRiverAltName;
        private Label label201;
        public Label lblRiverName;
        private Label label203;
        public ListBox lstRiver;
        public LinkLabel lblRiverParent;
        private Label label200;
        public GroupBox grpRiverTributaries;
        public ListBox lstRiverTributaries;
        public TabPage tabArmy;
        private TableLayoutPanel tableLayoutPanel43;
        private Button FilterArmy;
        private TextBox TextFilterArmy;
        public GroupBox grpArmy;
        public ListBox lstArmy;
        public TabPage tabUnit;
        private TableLayoutPanel tableLayoutPanel44;
        private Button FilterUnit;
        private TextBox TextFilterUnit;
        public GroupBox grpUnit;
        public Label lblUnitName;
        private Label label202;
        public ListBox lstUnit;
        public TabPage tabEngraving;
        private TableLayoutPanel tableLayoutPanel46;
        private Button FilterEngraving;
        private TextBox TextFilterEngraving;
        public GroupBox grpEngraving;
        public Label lblEngravingName;
        private Label label209;
        public ListBox lstEngraving;
        public TabPage tabReport;
        private TableLayoutPanel tableLayoutPanel50;
        private Button FilterReport;
        private TextBox TextFilterReport;
        public GroupBox grpReport;
        public Label lblReportText;
        private Label label217;
        public ListBox lstReport;
        public TabPage tabWrittenContent;
        private TableLayoutPanel tableLayoutPanel51;
        private Button FilterWrittenContent;
        private TextBox TextFilterWrittenContent;
        public GroupBox grpWrittenContent;
        public Label lblWrittenContentTitle;
        private Label label219;
        public ListBox lstWrittenContent;
        public TabPage tabBuilding;
        private TableLayoutPanel tableLayoutPanel52;
        private Button FilterBuilding;
        private TextBox TextFilterBuilding;
        public GroupBox grpBuilding;
        public Label lblBuildingName;
        private Label label221;
        public ListBox lstBuilding;
        public TabPage tabConstruction;
        private TableLayoutPanel tableLayoutPanel53;
        private Button FilterConstruction;
        private TextBox TextFilterConstruction;
        public GroupBox grpConstruction;
        public Label lblConstructionName;
        private Label label223;
        public ListBox lstConstruction;
        public TabPage tabItem;
        private TableLayoutPanel tableLayoutPanel54;
        private Button FilterItem;
        private TextBox TextFilterItem;
        public GroupBox grpItem;
        public ListBox lstItem;
        public TabPage tabPlant;
        private TableLayoutPanel tableLayoutPanel55;
        private Button FilterPlant;
        private TextBox TextFilterPlant;
        public GroupBox grpPlant;
        public Label lblPlantName;
        private Label label227;
        public ListBox lstPlant;
        public TabPage tabSquad;
        private TableLayoutPanel tableLayoutPanel56;
        private Button FilterSquad;
        private TextBox TextFilterSquad;
        public GroupBox grpSquad;
        public Label lblSquadName;
        private Label label229;
        public ListBox lstSquad;
        public TabPage tabEventCollectionOccasion;
        private TableLayoutPanel tableLayoutPanel57;
        public GroupBox grpOccasionEventCols;
        public ListBox lstOccasionEventCols;
        private Panel panel22;
        private Label label230;
        private Label label232;
        public Label lblOccasionOrdinal;
        public LinkLabel lblOccasionCiv;
        private Label label235;
        public Label lblOccasionDuration;
        public Label lblOccasionTime;
        private Label label239;
        public GroupBox grpOccasionEvents;
        public ListBox lstOccasionEvents;
        private Label label231;
        public Label lblOccasionID;
        public TabPage tabEventCollectionCeremony;
        private TableLayoutPanel tableLayoutPanel58;
        private Panel panel23;
        private Label label233;
        public Label lblCeremonyTime;
        private Label label236;
        private Label label240;
        public Label lblCeremonyOrdinal;
        public Label lblCeremonyDuration;
        public GroupBox grpCeremonyEvents;
        public ListBox lstCeremonyEvents;
        public TabPage tabEventCollectionProcession;
        private TableLayoutPanel tableLayoutPanel59;
        private Panel panel24;
        private Label label234;
        public Label lblProcessionTime;
        private Label label238;
        private Label label241;
        public Label lblProcessionOrdinal;
        public Label lblProcessionDuration;
        public GroupBox grpProcessionEvents;
        public ListBox lstProcessionEvents;
        public TabPage tabEventCollectionPerformance;
        private TableLayoutPanel tableLayoutPanel60;
        private Panel panel25;
        private Label label237;
        public Label lblPerformanceTime;
        private Label label243;
        private Label label244;
        public Label lblPerformanceOrdinal;
        public Label lblPerformanceDuration;
        public GroupBox grpPerformanceEvents;
        public ListBox lstPerformanceEvents;
        public TabPage tabEventCollectionCompetition;
        private TableLayoutPanel tableLayoutPanel61;
        private Panel panel26;
        private Label label242;
        public Label lblCompetitionTime;
        private Label label246;
        private Label label247;
        public Label lblCompetitionOrdinal;
        public Label lblCompetitionDuration;
        public GroupBox grpCompetitionEvents;
        public ListBox lstCompetitionEvents;
        public Label lblArmyMaterial;
        private Label label262;
        public Label lblArmyItemSubtype;
        private Label label252;
        public Label lblArmyItemType;
        private Label label254;
        private Label label250;
        public Label lblArmyLocation;
        private Label label248;
        private Label label272;
        private Label label274;
        public Label lblUnitProfession;
        private Label label276;
        public Label lblUnitCaste;
        private Label label278;
        private Label label280;
        private Label label282;
        public Label lblUnitMood;
        private Label label284;
        private Label label286;
        private Label label288;
        public Label lblUnitSex;
        private Label label290;
        public Label lblUnitCoords;
        private Label label292;
        public Label lblUnitAltName;
        private Label label294;
        public LinkLabel lblArmyItem;
        public LinkLabel lblUnitRace;
        public LinkLabel lblUnitHF;
        public LinkLabel lblUnitPop;
        public LinkLabel lblUnitCiv;
        public LinkLabel lblUnitOpponent;
        public LinkLabel lblUnitSquad;
        private Label label192;
        public LinkLabel lblHistoricalFigureEntityPopulation;
        public GroupBox grpUnitFlags;
        public ListBox lstUnitFlags;
        public GroupBox grpUnitLabors;
        public ListBox lstUnitLabors;
        public GroupBox grpUnitRelations;
        public ListBox lstUnitRelations;
        public Label lblEngravingHidden;
        public Label lblEngravingLocation;
        public Label lblEngravingArtSubID;
        public Label lblEngravingArtID;
        public LinkLabel lblEngravingArtist;
        private Label label257;
        private Label label258;
        public Label lblEngravingQuality;
        private Label label260;
        private Label label261;
        private Label label263;
        public Label lblEngravingTile;
        private Label label265;
        public Label lblEngravingCoords;
        private Label label267;
        public Label lblEngravingSkill;
        private Label label269;
        private Label label271;
        public LinkLabel lblBuildingOwner;
        public LinkLabel lblBuildingRace;
        public Label lblBuildingZoneFlags;
        public Label lblBuildingSubType;
        public Label lblBuildingType;
        private Label label253;
        private Label label255;
        private Label label259;
        private Label label264;
        private Label label266;
        private Label label270;
        public Label lblBuildingMat;
        private Label label275;
        public Label lblBuildingCoords2;
        private Label label279;
        private Label label281;
        public Label lblBuildingCoords1;
        private Label label285;
        public Label lblBuildingCoordsCenter;
        public LinkLabel lblBuildingSquad;
        public LinkLabel lblBuildingCorpse;
        public LinkLabel lblBuildingCorpseHF;
        public Label lblBuildingDir;
        private Label label287;
        private Label label289;
        private Label label291;
        public LinkLabel lblBuildingClaimed;
        private Label label206;
        public Label lblConstructionMat;
        private Label label304;
        public Label lblConstructionItemType;
        private Label label306;
        public Label lblConstructionCoords;
        private Label label308;
        public Label lblPlantMat;
        private Label label224;
        public Label lblPlantCoords;
        private Label label283;
        public LinkLabel lblSquadEntity;
        private Label label303;
        public Label lblSquadAltName;
        private Label label307;
        public GroupBox grpSquadMembers;
        public ListBox lstSquadMembers;
        public TabPage tabPoeticForm;
        private TableLayoutPanel tableLayoutPanel62;
        private Button FilterPoeticForm;
        private TextBox TextFilterPoeticForm;
        public GroupBox grpPoeticForm;
        public Label lblPoeticFormAltName;
        private Label label296;
        public Label lblPoeticFormName;
        private Label label220;
        public ListBox lstPoeticForm;
        public TabPage tabMusicalForm;
        private TableLayoutPanel tableLayoutPanel63;
        private Button FilterMusicalForm;
        private TextBox TextFilterMusicalForm;
        public GroupBox grpMusicalForm;
        public Label lblMusicalFormAltName;
        private Label label226;
        public Label lblMusicalFormName;
        private Label label249;
        public ListBox lstMusicalForm;
        public TabPage tabDanceForm;
        private TableLayoutPanel tableLayoutPanel64;
        private Button FilterDanceForm;
        private TextBox TextFilterDanceForm;
        public GroupBox grpDanceForm;
        public Label lblDanceFormAltName;
        private Label label297;
        public Label lblDanceFormName;
        private Label label299;
        public ListBox lstDanceForm;
        public GroupBox grpUnitHealth;
        public ListBox lstUnitHealth;
        public GroupBox grpUnitItems;
        public TreeView trvUnitItems;
        public GroupBox grpUnitOwnedBuildings;
        public ListBox lstUnitOwnedBuildings;
        public GroupBox grpUnitInventory;
        public ListBox lstUnitInventory;
        private Label label204;
        public LinkLabel lblEngravingCreatedEvent;
        public Label lblReportContinuation;
        public Label lblReportAnnouncement;
        public Label lblReportTime;
        private Label label214;
        public Label lblReportType;
        private Label label210;
        public LinkLabel lblWrittenContentAuthor;
        public Label lblWrittenContentPages;
        private Label label312;
        private Label label314;
        public GroupBox grpItemImprovement;
        public ListBox lstItemImprovement;
        public GroupBox grpItemIngredient;
        public ListBox lstItemIngredient;
        public GroupBox grpItemImage;
        private Label label211;
        private Label label213;
        public Label lblItemImageSubID;
        private Label label216;
        public Label lblItemImageID;
        private Label label228;
        public LinkLabel lblItemImageSIte;
        public LinkLabel lblItemImageCiv;
        public Label lblItemBoneMat;
        private Label label332;
        private Label label215;
        private Label label300;
        public Label lblItemShape;
        private Label label302;
        private Label label305;
        public Label lblItemHand;
        private Label label310;
        public Label lblItemWear;
        private Label label313;
        public Label lblItemAge;
        private Label label316;
        public Label lblItemSkill;
        private Label label251;
        public Label lblItemSubType;
        private Label label245;
        public Label lblItemType;
        private Label label222;
        public Label lblItemQuality;
        public LinkLabel lblItemMaker;
        private Label label256;
        private Label label268;
        public Label lblItemMat;
        private Label label277;
        public Label lblItemDescription;
        private Label label293;
        public Label lblItemName;
        private Label label225;
        public LinkLabel lblItemUnit;
        public LinkLabel lblItemHF;
        public Label lblItemStockpileCoords;
        private Label label335;
        public Label lblItemStockpile;
        private Label label337;
        public Label lblItemDyeMat;
        public LinkLabel lblItemMasterpieceEvent;
        private Label label212;
        public Label lblItemCoords;
        private Label label298;
        public TabPage tabEventCollectionPurge;
        private TableLayoutPanel tableLayoutPanel45;
        public GroupBox groupBox1;
        public ListBox lstPurgeEvents;
        private Panel panel27;
        public Label lblPurgeOrdinal;
        private Label label205;
        public LinkLabel lblPurgeSite;
        public Label lblPurgeAdjective;
        private Label label301;
        public Label lblPurgeDuration;
        public Label lblPurgeTime;
        private Label label315;
        private Label label318;
        private Label label320;
        public Label lblItemWeight;
        private Label label208;
        public Label lblWrittenContentStyle;
        private Label label309;
        public Label lblWrittenContentType;
        private Label label273;
        public TabPage tabLandmass;
        private TableLayoutPanel tableLayoutPanel47;
        private Button FilterLandmass;
        private TextBox TextFilterLandmass;
        public GroupBox grpLandmass;
        public Label lblLandmassName;
        private Label label329;
        public ListBox lstLandmass;
        private Label label207;
        private Label label295;
        public LinkLabel lblSiteCurOwner;
        public LinkLabel lblSiteCiv;
        public LinkLabel lblArtifactWC;
        public Label lblArtifactWCLabel;
        private Label label311;
        public Label lblHistoricalFigureSex;
        public GroupBox grpEntityPositions;
        public ListBox lstEntityPositions;
        public Label lblLandmassArea;
        private Label label330;
        private Label label333;
        private Label label324;
        public LinkLabel lblLandmassMax;
        public LinkLabel lblLandmassMin;
        public LinkLabel lblLandmassCenter;
        private Label label322;
        public GroupBox grpWrittenContentReferences;
        public ListBox lstWrittenContentReferences;
        public GroupBox grpUnitReferences;
        public ListBox lstUnitReferences;
        public GroupBox grpItemReferences;
        public ListBox lstItemReferences;
        public GroupBox grpBuildingReferences;
        public ListBox lstBuildingReferences;
        private GroupBox groupBox7;
        private TreeView trvItemsByQualityType;
        private GroupBox groupBox6;
        private TreeView trvItemsByTypeMaterial;
        public Label lblEntityPopulationName;
        private Label label218;
        public RichTextBoxEx rtbHistoricalFigureSummary;
        private Button TestButton;
    }
}

