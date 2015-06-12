using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace DFWV.WorldClasses.HistoricalFigureClasses
{
    public struct HFSkill
    {
        public int Skill { get; private set; }
        public int TotalIP { get; private set; }
        public static List<string> Skills = new List<string>();

        public HFSkill(XContainer data) : this()
        {
            
            var skillname = data.Element("skill").Value;
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
            var table = "HF_" + GetType().Name;



            var vals = new List<object> { HFID, Skills[Skill], TotalIP};


            Database.ExportWorldItem(table, vals);
        }
    }
}