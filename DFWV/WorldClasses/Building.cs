using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Xml.Linq;
using DFWV.Annotations;
using DFWV.WorldClasses.HistoricalFigureClasses;

namespace DFWV.WorldClasses
{
    public class Building : XMLObject
    {
        public Point3 CoordsCenter { get; set; }
        override public Point Location => Point.Empty;
        public Rectangle Rect { get; set; }
        private int? Z { get; set; }
        private int? Mat { get; }

        [UsedImplicitly]
        public string Material => Mat.HasValue ? Item.Materials[Mat.Value] : "";
        private int? RaceID { get; set; }
        public Race Race { get; set; }
        [UsedImplicitly]
        public string RaceName => Race != null ? Race.Name : "";
        private int? BuildingTypeID { get; }
        public string Type => BuildingTypeID.HasValue ? BuildingTypes[BuildingTypeID.Value] : "";
        public static List<string> BuildingTypes = new List<string>();
        private int? BuildingSubTypeID { get; }
        public string SubType => BuildingSubTypeID.HasValue ? BuildingSubTypes[BuildingSubTypeID.Value]: "";
        public static List<string> BuildingSubTypes =  new List<string>();
        private int? OwnerUnitID { get; }
        public Unit Owner { get; set; }
        public int? CorpseUnitID { get; set; }
        public Unit CorpseUnit { get; set; }
        public int? CorpseHFID { get; set; }
        public HistoricalFigure CorpseHF { get; set; }
        public int? ClaimedByID { get; set; }
        public Unit ClaimedBy { get; set; }
        public int? SquadID { get; set; }
        public Squad Squad { get; set; }
        public static List<string> Flags = new List<string>();
        public List<short> Flag { get; set; }
        public int? Direction { get; set; }
        public List<Reference> References { get; set; }

        [UsedImplicitly]
        public string DispNameLower => ToString().ToLower();

        public Building(XDocument xdoc, World world)
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
                    case "coordscenter":
                        CoordsCenter = new Point3(
                            Convert.ToInt32(val.Split(',')[0]),
                            Convert.ToInt32(val.Split(',')[1]),
                            Convert.ToInt32(val.Split(',')[2]));
                        break;
                    case "coords1":
                        Point TL = new Point(
                            Convert.ToInt32(val.Split(',')[0]),
                            Convert.ToInt32(val.Split(',')[1]));
                        Rect = new Rectangle(TL, new Size(0, 0));
                        break;
                    case "coords2":
                        Point BR = new Point(
                            Convert.ToInt32(val.Split(',')[0]),
                            Convert.ToInt32(val.Split(',')[1]));
                        Rect = new Rectangle(Rect.Location, new Size(BR.X - Rect.Left, BR.Y - Rect.Top));
                        break;
                    case "mat":
                        if (!Item.Materials.Contains(val))
                            Item.Materials.Add(val);
                        Mat = Item.Materials.IndexOf(val);
                        break;
                    case "race":
                        if (valI != -1)
                            Race = World.GetAddRace(val);
                        break;
                    case "type":
                        if (!BuildingTypes.Contains(val))
                            BuildingTypes.Add(val);
                        BuildingTypeID = BuildingTypes.IndexOf(val);
                        break;
                    case "subtype":
                        if (!BuildingSubTypes.Contains(val))
                            BuildingSubTypes.Add(val);
                        BuildingSubTypeID = BuildingSubTypes.IndexOf(val);
                        break;
                    case "corpse_unit":
                        CorpseUnitID = valI;
                        break;
                    case "corpse_hf":
                        CorpseHFID = valI;
                        break;
                    case "owner_unit_id":
                        OwnerUnitID = valI;
                        break;
                    case "claimed_by":
                        if (valI != -1)
                            ClaimedByID = valI;
                        break;
                    case "squad":
                        SquadID = valI;
                        break;
                    case "zone_flags":
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
                    case "direction":
                        Direction = valI;
                        break;
                    case "reference":
                        if (References == null)
                            References = new List<Reference>();
                        References.Add(new Reference(element, this));
                        break;
                    default:
                        DFXMLParser.UnexpectedXmlElement(xdoc.Root.Name.LocalName, element, xdoc.Root.ToString());
                        break;
                }
            }
        }

        public override void Select(MainForm frm)
        {
            if (frm.grpBuilding.Text == ToString() && frm.MainTab.SelectedTab == frm.tabBuilding)
                return;
            Program.MakeSelected(frm.tabBuilding, frm.lstBuilding, this);

            frm.grpBuilding.Text = ToString();
            frm.grpBuilding.Show();
#if DEBUG
            frm.grpBuilding.Text += string.Format(" - ID: {0}", Id);
#endif

            frm.lblBuildingName.Text = ToString();
            frm.lblBuildingCoords1.Text = $"({Rect.Left}, {Rect.Top}, {CoordsCenter.Z})";
            frm.lblBuildingCoordsCenter.Text = $"({CoordsCenter.X}, {CoordsCenter.Y}, {CoordsCenter.Z})";
            frm.lblBuildingCoords2.Text = $"({Rect.Left}, {Rect.Top}, {CoordsCenter.Z})";
            frm.lblBuildingMat.Text = Mat.HasValue ? Item.Materials[Mat.Value] : "";
            frm.lblBuildingRace.Data = Race;
            frm.lblBuildingType.Text = BuildingTypeID.HasValue ? BuildingTypes[BuildingTypeID.Value] : "";
            frm.lblBuildingSubType.Text = BuildingSubTypeID.HasValue ? BuildingSubTypes[BuildingSubTypeID.Value] : "";
            frm.lblBuildingOwner.Data = Owner;
            frm.lblBuildingZoneFlags.Text = Flag != null ?  string.Join(",", Flag.Select(x=>Flags[x])) : "";
            frm.lblBuildingCorpse.Data = CorpseUnit;
            frm.lblBuildingCorpseHF.Data = CorpseHF;
            frm.lblBuildingClaimed.Data = ClaimedBy;
            frm.lblBuildingDir.Text = Direction.ToString();
            frm.lblBuildingSquad.Data = Squad;
            frm.grpBuildingReferences.FillListboxWith(frm.lstBuildingReferences, References);
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
            if (RaceID.HasValue)
                Race = World.Races[RaceID.Value];
            if (OwnerUnitID.HasValue)
                Owner = World.Units[OwnerUnitID.Value];
            if (CorpseUnitID.HasValue)
                CorpseUnit = World.Units[CorpseUnitID.Value];
            if (CorpseHFID.HasValue)
                CorpseHF = World.HistoricalFigures[CorpseHFID.Value];
            if (OwnerUnitID.HasValue)
                Owner = World.Units[OwnerUnitID.Value];
            if (ClaimedByID.HasValue)
                ClaimedBy = World.Units[ClaimedByID.Value];
            if (SquadID.HasValue)
                Squad = World.Squads[SquadID.Value];
            References?.ForEach(x => x.Link());

        }

        internal override void Plus(XDocument xdoc)
        {
            
        }

        public override string ToString()
        {
            if (!BuildingTypeID.HasValue && !BuildingSubTypeID.HasValue)
                return base.ToString();
            if (!BuildingSubTypeID.HasValue)
                return ($"{BuildingTypes[BuildingTypeID.Value]}").ToTitleCase();
            return ($"{BuildingTypes[BuildingTypeID.Value]} - {BuildingSubTypes[BuildingSubTypeID.Value]}").ToTitleCase();
        }

    }


}