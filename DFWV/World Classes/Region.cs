namespace DFWV.WorldClasses
{
    using DFWV.WorldClasses.HistoricalEventClasses;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Globalization;
    using System.Linq;
    using System.Text;
    using System.Xml.Linq;
    using DFWV.WorldClasses.HistoricalEventCollectionClasses;
    using DFWV.WorldClasses.HistoricalFigureClasses;
using System.Collections.Specialized;

    class Region : XMLObject
    {
        public static List<string> Types = new List<string>();
        public int Type { get; set; }

        public List<Point> Coords { get; set; }
        public Point LowestCoord;
        public Point HighestCoord;

        public List<HistoricalFigure> Inhabitants { get; set; }


        public List<EC_Battle> BattleEventCollections { get; set; }
        public List<EC_Duel> DuelEventCollections { get; set; }
        public List<EC_Abduction> AbductionEventCollections { get; set; }
        public List<EC_Theft> TheftEventCollections { get; set; }
        public List<HE_FieldBattle> FieldBattleEvents { get; set; }


        public int Battles { get { return BattleEventCollections == null ? 0 : BattleEventCollections.Count; } }
        public int InhabitantCount { get { return Inhabitants == null ? 0 : Inhabitants.Count; } }
        public string DispNameLower { get { return ToString().ToLower(); } }
        public string RegionType { get { return Types[Type]; } }

        override public Point Location { get { return Coords != null  ? Coords[0] : Point.Empty; } }

        public Region(XDocument xdoc, World world)
            : base(xdoc, world)
        {
            foreach (XElement element in xdoc.Root.Elements())
            {
                string val = element.Value.ToString();

                switch (element.Name.LocalName)
                {
                    case "id":
                        break;
                    case "name":
                        Name = val;
                        break;
                    case "type":
                        if (!Region.Types.Contains(val))
                            Region.Types.Add(val);
                        Type = Region.Types.IndexOf(val);
                        break;
                    default:
                        DFXMLParser.UnexpectedXMLElement(xdoc.Root.Name.LocalName, element, xdoc.Root.ToString());
                        break;
                }
            }

        }

        //public Region(NameValueCollection data, World world) 
        //    : base (world)
        //{
        //    Name = data["Name"].ToString();
        //    Type = data["Type"].ToString();
        //}

        public override void Select(MainForm frm)
        {
            frm.grpRegion.Text = this.ToString();
            frm.grpRegion.Show();

            frm.lblRegionName.Text = ToString();
            frm.lblRegionType.Text = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Region.Types[Type]);

            frm.lstRegionFieldBattles.BeginUpdate();
            frm.lstRegionFieldBattles.Items.Clear();
            if (FieldBattleEvents != null)
                frm.lstRegionFieldBattles.Items.AddRange(FieldBattleEvents.ToArray());
            frm.lstRegionFieldBattles.EndUpdate();
            frm.grpRegionFieldBattles.Visible = frm.lstRegionFieldBattles.Items.Count > 0;
            frm.grpRegionFieldBattles.Text = "Field Battles (" + frm.lstRegionFieldBattles.Items.Count + ")";

            if (frm.lstRegionFieldBattles.Items.Count > 0)
                frm.lstRegionFieldBattles.SelectedIndex = 0;


            frm.lstRegionBattles.BeginUpdate();
            frm.lstRegionBattles.Items.Clear();
            if (BattleEventCollections != null)
                frm.lstRegionBattles.Items.AddRange(BattleEventCollections.ToArray());
            frm.lstRegionBattles.EndUpdate();
            frm.grpRegionBattles.Visible = frm.lstRegionBattles.Items.Count > 0;
            frm.grpRegionBattles.Text = " Battles (" + frm.lstRegionBattles.Items.Count + ")";

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

        internal override void Export(string table)
        {

            List<object> vals = new List<object>();

            vals.Add(ID);

            if (Name == null)
                vals.Add(DBNull.Value);
            else
                vals.Add(Name.Replace("'", "''"));

            vals.Add(Type);

            Database.ExportWorldItem(table, vals);
        }
    }
}
