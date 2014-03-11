using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using DFWV.WorldClasses.HistoricalFigureClasses;
using DFWV.WorldClasses.HistoricalEventCollectionClasses;
using System.Drawing;
using System.Collections.Specialized;

namespace DFWV.WorldClasses
{
    class EntityPopulation : XMLObject
    {

        public List<EC_Battle> BattleEventCollections { get; set; }
        public List<HistoricalFigure> Members { get; set; }
        public Race Race { get; set; }


        public int MemberCount { get { return Members == null ? 0 : Members.Count; } }
        public int Battles { get { return BattleEventCollections == null ? 0 : BattleEventCollections.Count; } }
        public string DispNameLower { get { return ToString().ToLower(); } }

        override public Point Location { get { return Point.Empty; } }


        public EntityPopulation(XDocument xdoc, World world)
            : base(xdoc, world)
        {
            foreach (XElement element in xdoc.Root.Elements())
            {
                string val = element.Value.ToString();
                switch (element.Name.LocalName)
                {
                    case "id":
                        break;
                    default:
                        DFXMLParser.UnexpectedXMLElement(xdoc.Root.Name.LocalName, element, xdoc.Root.ToString());
                        break;
                }
            }
        }

        //public EntityPopulation(NameValueCollection data, World world) 
        //    : base (data, world)
        //{
            
        //}

        public override string ToString()
        {
            return ID.ToString();
        }

        public override void Select(MainForm frm)
        {
            frm.grpEntityPopulation.Text = this.ToString();
            frm.grpEntityPopulation.Show();

            frm.lblEntityPopulationRace.Data = Race;

            frm.grpEntityPopulationBattles.Visible = BattleEventCollections != null;
            if (BattleEventCollections != null)
            {
                frm.lstEntityPopulationBattles.Items.Clear();
                foreach (EC_Battle evtcol in BattleEventCollections)
                {
                    frm.lstEntityPopulationBattles.Items.Add(evtcol);
                }
                frm.lstEntityPopulationBattles.SelectedIndex = 0;
            }
            frm.grpEntityPopulationMembers.Visible = Members != null;
            if (Members != null)
            {
                frm.lstEntityPopulationMembers.BeginUpdate();
                frm.lstEntityPopulationMembers.Items.Clear();
                foreach (HistoricalFigure hf in Members.Take(50000))
                {
                    frm.lstEntityPopulationMembers.Items.Add(hf);
                }
                frm.lstEntityPopulationMembers.EndUpdate();
            }
            frm.grpEntityPopulationMembers.Text = "Members (" + frm.lstEntityPopulationMembers.Items.Count +
                                                        (Members != null && Members.Count > 50000 ? "+" : "") + ")";
            Program.MakeSelected(frm.tabEntityPopulation, frm.lstEntityPopulation, this);
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

            Database.ExportWorldItem(table, vals);
        }

    }
}
