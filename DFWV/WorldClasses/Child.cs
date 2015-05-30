using System;

namespace DFWV.WorldClasses
{
    public class Child : Person
    {
        public Child(string data, Leader leader)
        {
            Parent = leader;
            if (data.Contains("d."))
                Death = new WorldTime(Convert.ToInt32(data.Replace("(d.", "").Replace(")", "")));
            else
                AgeAtParentDeath = Convert.ToInt32(data);
        }

        private Leader Parent { get; set; }

        public  WorldTime Death { get; set; }
        public int? AgeAtParentDeath { get; set; }
    }
}