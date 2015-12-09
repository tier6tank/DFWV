using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace DFWV.WorldClasses.HistoricalFigureClasses
{
    public struct HFSkill
    {
        public int Skill { get; }
        public int TotalIp { get; }
        public static List<string> Skills = new List<string>();

        public HFSkill(XContainer data) : this()
        {
            
            var skillname = data.Element("skill").Value;
            if (!Skills.Contains(skillname))
                Skills.Add(skillname);
            Skill = Skills.IndexOf(skillname);
                

            TotalIp = Convert.ToInt32(data.Element("total_ip").Value);
        }

        public override string ToString()
        {
            return Skill + ": " + TotalIp;
        }

        internal void Export(int hfid)
        {
            var table = "HF_" + GetType().Name;



            var vals = new List<object> { hfid, Skills[Skill], TotalIp};


            Database.ExportWorldItem(table, vals);
        }
    }
}