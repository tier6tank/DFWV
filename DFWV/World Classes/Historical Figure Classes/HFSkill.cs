using DFWV.WorldClasses.HistoricalEventClasses;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;

namespace DFWV.WorldClasses.HistoricalFigureClasses
{
    class HFSkill
    {
        public int Skill { get; set; }
        public int TotalIP { get; set; }
        public static List<string> Skills = new List<string>();

        public HFSkill(XElement data)
        {
            
            string skillname = data.Element("skill").Value;
            if (!Skills.Contains(skillname))
                Skills.Add(skillname);
            Skill = Skills.IndexOf(skillname);
                

            TotalIP = Convert.ToInt32(data.Element("total_ip").Value);
        }

        public override string ToString()
        {
            return Skill + ": " + TotalIP;
        }

        internal void Export(int HFID)
        {
            List<object> vals;
            string table = "HF_" + this.GetType().Name.ToString();



            vals = new List<object>() { HFID, Skills[Skill], TotalIP};


            Database.ExportWorldItem(table, vals);
        }
    }
}