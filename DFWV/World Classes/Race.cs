using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Collections.Specialized;

namespace DFWV.WorldClasses
{
    class Race : WorldObject
    {
        public long Population { get; set; }
        public bool isCivilized { get; set; }

        public int CivCount { get { return World.Civilizations.Where(x => x.Race == this).Count(); } }
        public int LeaderCount { get { return World.Leaders.Where(x => x.Race == this).Count(); } }
        public int HFCount { get { return World.HistoricalFigures.Values.Where(x => x.Race == this).Count(); } }
        public string DispNameLower { get { return ToString().ToLower(); } }

        override public Point Location { get { return Point.Empty; } }


        public Race(string name, World world) 
            : base(world)
        {
            Name = name;
        }

        //public Race(NameValueCollection data, World world) 
        //    : base (world)
        //{
        //    Name = data["Name"].ToString();
        //}

        public override void Select(MainForm frm)
        {
            try
            {
                frm.grpRace.Text = this.ToString();
                frm.grpRace.Show();

                frm.lblRaceName.Text = ToString();
                frm.lblRacePopulation.Text = Population == Int64.MaxValue ? "Unnumbered" :  Population.ToString();

                frm.lstRaceLeaders.BeginUpdate();
                frm.lstRaceLeaders.Items.Clear();
                frm.lstRaceLeaders.Items.AddRange(World.Leaders.Where(x => x.Race == this).ToArray());
                frm.lstRaceLeaders.EndUpdate();
                frm.grpRaceLeaders.Name = "Leaders (" + frm.lstRaceLeaders.Items.Count + ")";

                frm.lstRaceCivilizations.BeginUpdate();
                frm.lstRaceCivilizations.Items.Clear();
                frm.lstRaceCivilizations.Items.AddRange(World.Civilizations.Where(x => x.Race == this).ToArray());
                frm.lstRaceCivilizations.EndUpdate();
                frm.grpRaceCivilizations.Name = "Civilizations (" + frm.lstRaceCivilizations.Items.Count + ")";

                frm.lstRaceHistoricalFigures.BeginUpdate();
                frm.lstRaceHistoricalFigures.Items.Clear();
                frm.lstRaceHistoricalFigures.Items.AddRange(World.HistoricalFigures.Values.Where(x => x.Race == this).Take(50000).ToArray());
                frm.lstRaceHistoricalFigures.EndUpdate();
                frm.grpRaceHistoricalFigures.Name = "Historical Figures (" + frm.lstRaceHistoricalFigures.Items.Count + (frm.lstRaceHistoricalFigures.Items.Count == 50000 ? "+" : "") + ")";

            }
            catch (Exception)
            {

            }
            finally
            {
                frm.grpRaceLeaders.Visible = frm.lstRaceLeaders.Items.Count > 0;
                frm.grpRaceCivilizations.Visible = frm.lstRaceCivilizations.Items.Count > 0;
                frm.grpRaceHistoricalFigures.Visible = frm.lstRaceHistoricalFigures.Items.Count > 0;
                Program.MakeSelected(frm.tabRace, frm.lstRace, this);
            }
        }

        internal override void Export(string table)
        {

            List<object> vals = new List<object>();

            vals.Add(Name.Replace("'", "''"));
            vals.Add(isCivilized);
            vals.Add(Population == Int64.MaxValue ? -1 : Population);

            Database.ExportWorldItem(table, vals);
        }
        
    }


}
