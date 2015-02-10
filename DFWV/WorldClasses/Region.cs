﻿using DFWV.Annotations;

namespace DFWV.WorldClasses
{
    using HistoricalEventClasses;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Xml.Linq;
    using HistoricalEventCollectionClasses;
    using HistoricalFigureClasses;

    public class Region : XMLObject
    {
        public static List<string> Types = new List<string>();
        public int Type { get; private set; }

        public List<Point> Coords { get; set; }

        public List<HistoricalFigure> Inhabitants { get; set; }


        public List<EC_Battle> BattleEventCollections { get; set; }
        public List<EC_Duel> DuelEventCollections { get; set; }
        public List<EC_Abduction> AbductionEventCollections { get; set; }
        public List<EC_Theft> TheftEventCollections { get; set; }
        public List<HE_FieldBattle> FieldBattleEvents { get; set; }


        [UsedImplicitly]
        public int Battles { get { return BattleEventCollections == null ? 0 : BattleEventCollections.Count; } }
        [UsedImplicitly]
        public int InhabitantCount { get { return Inhabitants == null ? 0 : Inhabitants.Count; } }
        [UsedImplicitly]
        public string DispNameLower { get { return ToString().ToLower(); } }
        [UsedImplicitly]
        public string RegionType { get { return Types[Type]; } }

        override public Point Location { get { return Coords != null  ? Coords[0] : Point.Empty; } }

        public Region(XDocument xdoc, World world)
            : base(xdoc, world)
        {
            foreach (var element in xdoc.Root.Elements())
            {
                var val = element.Value;

                switch (element.Name.LocalName)
                {
                    case "id":
                        break;
                    case "name":
                        Name = val;
                        break;
                    case "type":
                        if (!Types.Contains(val))
                            Types.Add(val);
                        Type = Types.IndexOf(val);
                        break;
                    default:
                        DFXMLParser.UnexpectedXMLElement(xdoc.Root.Name.LocalName, element, xdoc.Root.ToString());
                        break;
                }
            }

        }

        public override void Select(MainForm frm)
        {
            frm.grpRegion.Text = ToString();
            frm.grpRegion.Show();

            frm.lblRegionName.Text = ToString();
            frm.lblRegionType.Text = Types[Type].ToTitleCase();

            frm.lstRegionFieldBattles.BeginUpdate();
            frm.lstRegionFieldBattles.Items.Clear();
            if (FieldBattleEvents != null)
                frm.lstRegionFieldBattles.Items.AddRange(FieldBattleEvents.ToArray());
            frm.lstRegionFieldBattles.EndUpdate();
            frm.grpRegionFieldBattles.Visible = frm.lstRegionFieldBattles.Items.Count > 0;
            frm.grpRegionFieldBattles.Text = string.Format("Field Battles ({0})", frm.lstRegionFieldBattles.Items.Count);

            if (frm.lstRegionFieldBattles.Items.Count > 0)
                frm.lstRegionFieldBattles.SelectedIndex = 0;


            frm.lstRegionBattles.BeginUpdate();
            frm.lstRegionBattles.Items.Clear();
            if (BattleEventCollections != null)
                frm.lstRegionBattles.Items.AddRange(BattleEventCollections.ToArray());
            frm.lstRegionBattles.EndUpdate();
            frm.grpRegionBattles.Visible = frm.lstRegionBattles.Items.Count > 0;
            frm.grpRegionBattles.Text = string.Format(" Battles ({0})", frm.lstRegionBattles.Items.Count);

            frm.lstRegionInhabitants.BeginUpdate();
            frm.lstRegionInhabitants.Items.Clear();
            if (Inhabitants != null)
                frm.lstRegionInhabitants.Items.AddRange(Inhabitants.ToArray());
            frm.lstRegionInhabitants.EndUpdate();
            frm.grpRegionInhabitants.Visible = frm.lstRegionInhabitants.Items.Count > 0;

            Program.MakeSelected(frm.tabRegion, frm.lstRegion, this);
        }

        internal override void Link()
        {

        }

        internal override void Process()
        {

        }
        internal override void Plus(XDocument xdoc)
        {
            foreach (var element in xdoc.Root.Elements())
            {
                var val = element.Value;
                int valI;
                Int32.TryParse(val, out valI);

                switch (element.Name.LocalName)
                {
                    case "id":
                    case "type":
                        break;
                    case "coords":
                        if (Coords == null)
                            Coords = new List<Point>();
                        foreach (var coord in val.Split(new[] { '|' }, StringSplitOptions.RemoveEmptyEntries))
                        {
                            var coordSplit = coord.Split(',');
                            if (coordSplit.Length == 2)
                                Coords.Add(new Point(Convert.ToInt32(coordSplit[0]), Convert.ToInt32(coordSplit[1])));
                        }
                        break;
                    default:
                        DFXMLParser.UnexpectedXMLElement(xdoc.Root.Name.LocalName + "\t" + Types[Type], element, xdoc.Root.ToString());
                        break;
                }
            }

        }

        internal override void Export(string table)
        {

            var vals = new List<object>
            {
                ID,
                Name.DBExport(),
                Type
            };

            Database.ExportWorldItem(table, vals);

            if (Coords != null)
            {
                int coordID = 0;
                foreach (var coord in Coords)
                {
                    Database.ExportWorldItem("Region_Coords", new List<object> { ID, coordID, coord.X, coord.Y });
                    coordID++;
                }
            }
        }
    }
}
