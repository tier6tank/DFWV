using System;
using System.Collections.Generic;
using System.Xml.Linq;
using DFWV.WorldClasses.EntityClasses;

namespace DFWV.WorldClasses.HistoricalFigureClasses
{
    struct EntitySquadLink
    {
        private int SquadID { get; set; }
        private int SquadPosition { get; set; }
        public int EntityID { get; private set; }
        private int StartYear { get; set; }

        public HistoricalFigure HF { get; private set; }

        public Entity Entity => HF.World.Entities[EntityID];


        public EntitySquadLink(XContainer data, HistoricalFigure hf)
            : this()
        {
            foreach (var element in data.Elements())
            {
                var val = element.Value;
                int valI;
                int.TryParse(val, out valI);
                switch (element.Name.LocalName)
                {
                    case "squad_id":
                        SquadID = valI;
                        break;
                    case "squad_position":
                        SquadPosition = valI;
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
                return SquadID + ": " + SquadPosition + " - " + EntityID + " - " + (StartYear != 0 ? StartYear.ToString() : "?");
            return SquadID + ": " + SquadPosition + " - " + Entity.Name + " - " + (StartYear != 0 ? StartYear.ToString() : "?");
        }


        internal void Export(int HFID)
        {
            var table = "HF_" + GetType().Name;



            var vals = new List<object> { HFID, SquadID, EntityID, StartYear };


            Database.ExportWorldItem(table, vals);
        }
    }

}