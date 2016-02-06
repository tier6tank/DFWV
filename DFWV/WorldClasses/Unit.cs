using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;
using DFWV.Annotations;
using DFWV.WorldClasses.EntityClasses;
using DFWV.WorldClasses.HistoricalFigureClasses;

namespace DFWV.WorldClasses
{
    public class Unit : XMLObject
    {

        public static List<string> JobTypes = new List<string>();
        public string AltName { get; private set; }

        [UsedImplicitly]
        public string Profession => ProfessionId.HasValue ? JobTypes[ProfessionId.Value] : "";
        public int? ProfessionId { get; set; }
        private int? RaceID { get; set; }
        public Race Race { get; set; }
        [UsedImplicitly]
        public string RaceName => Race != null ? Race.Name : "";
        private int? CasteID { get; set; }
        public Caste Caste { get; set; }
        public Point3 Coords { get; set; }
        override public Point Location { get { return Point.Empty; } }
        private int? Sex { get; set; }
        private int? CivID { get; set; }
        public Entity Civ { get; private set; }
        private int? PopID { get; set; }
        public EntityPopulation Population { get; private set; }
        private int? SquadID { get; set; }
        public Squad Squad { get; private set; }
        private int? OpponentID { get; set; }
        public HistoricalFigure Opponent { get; private set; }
        private int? Mood { get; set; }
        private int? HistFigureID { get; set; }
        public HistoricalFigure HistFigure { get; private set; }
        private int? HistFigureID2 { get; set; }
        public HistoricalFigure HistFigure2 { get; private set; }
        public static List<string> Flags = new List<string>();
        public List<short> Flag { get; set; }

        [UsedImplicitly]
        public bool IsDead => Flags != null && Flags.Contains("dead") && Flag.Contains((short)Flags.IndexOf("dead"));
        public static List<string> Labors = new List<string>();
        public List<short> Labor { get; set; }
        public Dictionary<string, int> RelationIDs { get; set; }
        public Dictionary<string, HistoricalFigure> Relations { get; set; }
        private static List<string> HealthFlags = new List<string>();
        private List<short> HealthFlag { get; set; }
        private List<int> UsedItemIds { get; set; }
        private List<int> OwnedItemIds { get; set; }
        private List<int> TradedItemIds { get; set; }
        private List<int> OwnedBuildingIds { get; set; }
        private List<UnitInventoryItem> InventoryItems { get; set; }

        [UsedImplicitly]
        public string DispNameLower => ToString().ToLower();


        public Unit(XDocument xdoc, World world)
            : base(xdoc, world)
        {
            foreach (var element in xdoc.Root.Elements())
            {
                var val = element.Value.Trim();
                int valI;
                int.TryParse(val, out valI);
                switch (element.Name.LocalName)
                {
                    case "id":
                        break;
                    case "name":
                        Name = val;
                        break;
                    case "name2":
                        AltName = val;
                        break;
                    case "profession":
                        if (!JobTypes.Contains(val))
                            JobTypes.Add(val);
                        ProfessionId = JobTypes.IndexOf(val);
                        break;
                    case "race":
                        if (valI != -1)
                            Race = World.GetAddRace(val);
                        break;
                    case "caste":
                        if (valI != -1)
                        {
                            if (!HistoricalFigure.Castes.Contains(val))
                                HistoricalFigure.Castes.Add(val);
                            CasteID = HistoricalFigure.Castes.IndexOf(val);
                        }
                        break;
                    case "coords":
                        Coords = new Point3(
                            Convert.ToInt32(val.Split(',')[0]),
                            Convert.ToInt32(val.Split(',')[1]),
                            Convert.ToInt32(val.Split(',')[2]));
                        break;
                    case "sex":
                        Sex = valI;
                        break;
                    case "civ_id":
                        if (valI != -1)
                            CivID = valI;
                        break;
                    case "population_id":
                        if (valI != -1)
                            PopID = valI;
                        break;
                    case "squad_id":
                        if (valI != -1)
                            SquadID = valI;
                        break;
                    case "opponent_id":
                        if (valI != -1)
                            OpponentID = valI;
                        break;
                    case "mood":
                        Mood = valI;
                        break;
                    case "hist_figure_id":
                        if (valI != -1)
                            HistFigureID = valI;
                        break;
                    case "hist_figure_id2":
                        if (valI != -1)
                            HistFigureID2 = valI;
                        break;
                    case "labors":
                        var labors = val.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                        foreach (var labor in labors)
                        {
                            if (!Labors.Contains(labor))
                                Labors.Add(labor);
                            if (Labor == null)
                                Labor = new List<short>();
                            Labor.Add((short)Labors.IndexOf(labor));
                        }
                        break;
                    case "flags":
                        var flags = val.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                        foreach (var flag in flags)
                        {
                            if (!Flags.Contains(flag))
                                Flags.Add(flag);
                            if (Flag == null)
                                Flag = new List<short>();
                            Flag.Add((short)Flags.IndexOf(flag));
                        }
                        break;
                    case "health":
                        var healthflags = val.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                        foreach (var healthflag in healthflags)
                        {
                            if (!HealthFlags.Contains(healthflag))
                                HealthFlags.Add(healthflag);
                            if (HealthFlag == null)
                                HealthFlag = new List<short>();
                            HealthFlag.Add((short)HealthFlags.IndexOf(healthflag));
                        }
                        break;
                    case "used_items":
                        UsedItemIds = val.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();
                        break;
                    case "owned_items":
                        OwnedItemIds = val.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();
                        break;
                    case "owned_buildings":
                        OwnedBuildingIds = val.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();
                        break;
                    case "traded_items":
                        TradedItemIds = val.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();
                        break;
                    case "inventory":
                        foreach (var inv in element.Elements())
                        {
                            if (InventoryItems == null)
                                InventoryItems = new List<UnitInventoryItem>();
                            InventoryItems.Add(new UnitInventoryItem(new XDocument(inv),world));
                        }
                        break;
                    case "in_item_id":
                    case "nestbox_id":
                    case "civzone_id":
                        break;
                    case "nemesis_id": // Relations with HFs
                    case "pregnancy_spouse":
                    case "following_unit":
                    case "pet_owner_id":
                    case "spouse_id":
                    case "mother_id":
                    case "father_id":
                    case "last_attacker_id":
                    case "group_leader_id":
                    case "draggee_id":
                    case "dragger_id":
                    case "rider_mount_id":
                    case "lover_id":
                        if (valI != -1)
                        { 
                            if (RelationIDs == null)
                                RelationIDs = new Dictionary<string, int>();
                            RelationIDs.Add(string.Join(" ", element.Name.LocalName.Split('_').Reverse().Skip(1).Reverse().ToArray()), valI);
                        }
                        break;
                    case "pregnancy_timer":
                    case "pregnancy_caste":
                    case "mood_copy":
                    case "anon_1":
                    case "birth_year":
                    case "birth_time":
                    case "curse_year":
                    case "curse_time":
                    case "birth_year_bias":
                    case "birth_time_bias":
                    case "old_year":
                    case "old_time":
                    case "unk_238":
                    case "mount_type":
                        break;
                    default:
                        DFXMLParser.UnexpectedXmlElement(xdoc.Root.Name.LocalName, element, xdoc.Root.ToString());
                        break;
                }
            }
        }

        public override void Select(MainForm frm)
        {

            if (frm.grpUnit.Text == ToString() && frm.MainTab.SelectedTab == frm.tabUnit)
                return;
            Program.MakeSelected(frm.tabUnit, frm.lstUnit, this);

            try
            {
                frm.grpUnit.Text = ToString();
                frm.grpUnit.Show();
#if DEBUG
                frm.grpUnit.Text += string.Format(" - ID: {0}", Id);
#endif

                frm.lblUnitName.Text = Name;
                frm.lblUnitAltName.Text = AltName;
                frm.lblUnitCoords.Text = string.Format("({0}, {1}, {2})", Coords.X, Coords.Y, Coords.Z);
                frm.lblUnitSex.Text = Sex.ToString();
                frm.lblUnitCiv.Data = Civ;
                frm.lblUnitPop.Data = Population;
                frm.lblUnitMood.Text = Mood.ToString();
                frm.lblUnitHF.Data = HistFigure;
                frm.lblUnitRace.Data = Race;
                frm.lblUnitCaste.Text = CasteID.HasValue ? HistoricalFigure.Castes[CasteID.Value] : "";
                if (ProfessionId.HasValue)
                {
                    frm.lblUnitProfession.Text = JobTypes[ProfessionId.Value];
                    frm.lblUnitProfession.Visible = true;
                }
                else
                    frm.lblUnitProfession.Visible = false;
                frm.lblUnitSquad.Data = Squad;
                frm.lblUnitOpponent.Data = Opponent;

                frm.grpUnitFlags.Visible = Flag != null;
                frm.lstUnitFlags.Items.Clear();
                if (frm.grpUnitFlags.Visible)
                {
                    foreach (var flag in Flag.Distinct())
                    {
                        frm.lstUnitFlags.Items.Add(Flags[flag].ToLower().Replace("_", " ").ToTitleCase());
                    }
                }

                frm.grpUnitLabors.Visible = Labor != null;
                frm.lstUnitLabors.Items.Clear();
                if (frm.grpUnitLabors.Visible)
                {
                    foreach (var labor in Labor.Distinct())
                    {
                        frm.lstUnitLabors.Items.Add(Labors[labor].ToLower().Replace("_", " ").ToTitleCase());
                    }
                }

                frm.grpUnitRelations.Visible = Relations != null;
                frm.lstUnitRelations.Items.Clear();
                if (Relations != null)
                { 
                    if (Relations.Any())
                    {
                        frm.lstUnitRelations.Items.AddRange(Relations.Select(x=>x.Value).ToArray());
                    }
                }

                frm.grpUnitHealth.Visible = HealthFlag != null;
                frm.lstUnitHealth.Items.Clear();
                if (HealthFlag != null)
                {
                    foreach (var flag in HealthFlag.Distinct())
                    {
                        frm.lstUnitHealth.Items.Add(HealthFlags[flag].ToLower().Replace("_", " ").ToTitleCase());
                    }
                }

                frm.grpUnitItems.Visible = OwnedItemIds != null || TradedItemIds != null || UsedItemIds != null;
                frm.trvUnitItems.Nodes.Clear();
                if (frm.grpUnitItems.Visible)
                {
                    if (OwnedItemIds != null)
                    {
                        var node = new TreeNode("Owned Items");
                        foreach (var itemId in OwnedItemIds.Where(i => World.Items.ContainsKey(i)))
                        {
                            node.Nodes.Add(new TreeNode(World.Items[itemId].ToString())
                            {
                                Tag = World.Items[itemId]
                            });
                        }
                        frm.trvUnitItems.Nodes.Add(node);
                    }
                    if (TradedItemIds != null)
                    {
                        var node = new TreeNode("Traded Items");
                        foreach (var itemId in TradedItemIds.Where(i => World.Items.ContainsKey(i)))
                        {
                            node.Nodes.Add(new TreeNode(World.Items[itemId].ToString())
                            {
                                Tag = World.Items[itemId]
                            });
                        }
                        frm.trvUnitItems.Nodes.Add(node);
                    }
                    if (UsedItemIds != null)
                    {
                        var node = new TreeNode("Used Items");
                        foreach (var itemId in UsedItemIds.Where(i => World.Items.ContainsKey(i)))
                        {
                            node.Nodes.Add(new TreeNode(World.Items[itemId].ToString())
                            {
                                Tag = World.Items[itemId]
                            });
                        }
                        frm.trvUnitItems.Nodes.Add(node);
                    }
                }

                frm.grpUnitInventory.Visible = InventoryItems != null;
                frm.lstUnitInventory.Items.Clear();
                if (InventoryItems != null)
                {
                    frm.lstUnitInventory.Items.AddRange(InventoryItems.ToArray());
                }

                frm.grpUnitOwnedBuildings.Visible = OwnedBuildingIds != null;
                frm.lstUnitOwnedBuildings.Items.Clear();
                if (OwnedBuildingIds != null)
                {
                    frm.lstUnitOwnedBuildings.Items.AddRange(
                        OwnedBuildingIds.Where(x => World.Buildings.ContainsKey(x))
                        .Select(x => World.Buildings[x]).ToArray());
                }
            }
            finally
            {
                Program.MakeSelected(frm.tabUnit, frm.lstUnit, this);
            }
        }

        internal override void Export(string table)
        {

            var vals = new List<object>
            {
                Id,
                Name.DBExport()
            };

            Database.ExportWorldItem(table, vals);
        }

        internal override void Link()
        {
            if (CivID.HasValue)
                Civ = World.Entities[CivID.Value];
            if (RaceID.HasValue && RaceID.Value != 0) //TODO: Remove and condition
            {
                Race = World.Races[RaceID.Value];
                if (CasteID.HasValue)
                    Caste = Race.Castes[CasteID.Value];
            }
            if (CivID.HasValue)
                Civ = World.Entities[CivID.Value];
            if (PopID.HasValue)
                Population = World.EntityPopulations[PopID.Value];
            if (SquadID.HasValue && World.Squads.ContainsKey(SquadID.Value))
                Squad = World.Squads[SquadID.Value];
            if (OpponentID.HasValue && World.Units.ContainsKey(OpponentID.Value))
                Opponent = World.HistoricalFigures[OpponentID.Value];

            if (HistFigureID.HasValue)
            {
                HistFigure = World.HistoricalFigures[HistFigureID.Value];
                HistFigure.Unit = this;
            }
            if (HistFigureID2.HasValue)
                HistFigure2 = World.HistoricalFigures[HistFigureID2.Value];

            if (RelationIDs != null)
            {
                foreach (var relationId in RelationIDs.Where(relationId => World.HistoricalFigures.ContainsKey(relationId.Value)))
                {
                    if (Relations == null)
                        Relations = new Dictionary<string, HistoricalFigure>();
                    Relations.Add(relationId.Key, World.HistoricalFigures[relationId.Value]);
                }
            }
        }

        internal override void Process()
        {

        }

        internal override void Plus(XDocument xdoc)
        {

        }

        public override string ToString()
        {
            if (Name != "")
            {
                if (HistFigure != null)
                    return HistFigure.ToString();
                return Name;
            }
            else
            {
                if (Race != null)
                    return Race.ToString() + " - " + Id;
                else
                    return base.ToString();
            }
        }
    }


}