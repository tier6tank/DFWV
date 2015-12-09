using System;
using System.Collections.Generic;
using System.Xml.Linq;
using DFWV.WorldClasses.EntityClasses;

namespace DFWV.WorldClasses.HistoricalFigureClasses
{
    struct EntityPositionLink
    {
        private int PositionProfileID { get; set; }
        public int EntityID { get; private set; }
        public HistoricalFigure HF { get; private set; }

        public Entity Entity => HF.World.Entities[EntityID];


        private int StartYear { get; set; }

        public EntityPositionLink(XContainer data, HistoricalFigure hf) : this()
        {
            foreach (var element in data.Elements())
            {
                var val = element.Value;
                int valI;
                int.TryParse(val, out valI);
                switch (element.Name.LocalName)
                {
                    case "position_profile_id":
                        PositionProfileID = valI;
                        break;
                    case "entity_id":
                        EntityID = valI;
                        break;
                    case "start_year":
                        StartYear = valI;
                        break;
                }
            }

            

            HF = hf;
        }

        public override string ToString()
        {
            if (Entity == null)
                return PositionProfileID + ": " + EntityID + " - " + (StartYear != 0 ? StartYear.ToString() : "?");
            return PositionProfileID + ": " + Entity.Name + " - " + (StartYear != 0 ? StartYear.ToString() : "?");
        }


        internal void Export(int HFID)
        {
            var table = "HF_" + GetType().Name;

            var vals = new List<object> { HFID, PositionProfileID, EntityID, StartYear };

            Database.ExportWorldItem(table, vals);
        }
    }

}