using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using DFWV.Annotations;
using DFWV.WorldClasses.HistoricalFigureClasses;

namespace DFWV.WorldClasses
{
    public class Dynasty : WorldObject
    {
        public List<HistoricalFigure> Members { get; }
        private string Type { get; }
        private Civilization Civilization { get; }

        override public Point Location => Members.Last().Location;

        [UsedImplicitly]
        public string DispNameLower => ToString().ToLower();

        [UsedImplicitly]
        public int Duration => (int)((Members.Last().Leader.Death != null ? Members.Last().Leader.Death.ToSeconds() : WorldTime.Present.ToSeconds()) - 
                                     Members[0].Leader.ReignBegan.ToSeconds());

        [UsedImplicitly]
        public int MemberCount => Members.Count;


        public Dynasty(World world, HistoricalFigure hf, string type, Civilization civ) : base(world)
        {
            Members = new List<HistoricalFigure> {hf};
            Type = type;
            Civilization = civ;
        }

        public override string ToString()
        {
            return Members[0].ToString();
        }

        public override void Select(MainForm frm)
        {
            if (frm.grpDynasty.Text == ToString() && frm.MainTab.SelectedTab == frm.tabDynasty)
                return;
            Program.MakeSelected(frm.tabDynasty, frm.lstDynasty, this);

            frm.grpDynasty.Text = ToString();
            frm.grpDynasty.Show();

            frm.lblDynastyFounder.Data = Members[0];
            frm.lblDynastyCivilization.Data = Civilization;
            frm.lblDynastyType.Text = Type;
            frm.lblDynastyLength.Text = WorldTime.Duration(Members.Last().Leader.Death ?? WorldTime.Present, Members[0].Leader.ReignBegan);
            if (Members.Last().Leader.Death == WorldTime.Present)
                frm.lblDynastyLength.Text += @"+";

            frm.grpDynastyMembers.FillListboxWith(frm.lstDynastyMembers, Members);

        }

        internal override void Export(string table)
        {

            //var vals = new List<object> {Members[0].ID};

            //if (Members[0].Name == null)
            //    vals.Add(DBNull.Value);
            //else
            //    vals.Add(Members[0].Name.Replace("'", "''"));

            //Database.ExportWorldItem(table, vals);
        }
    }
}