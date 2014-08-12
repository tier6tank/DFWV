using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DFWV.WorldClasses
{
    class Child : Person
    {

        public Leader Parent { get; set; }
        
        public WorldTime Death { get; set; }
        public int? AgeAtParentDeath { get; set; }

        public Child(string data, Leader leader)
        {
            Parent = leader;
            if (data.Contains("d."))
                Death = new WorldTime(Convert.ToInt32(data.Replace("(d.", "").Replace(")", "")));
            else
                AgeAtParentDeath = Convert.ToInt32(data);
        }
    }
}
