using System.Collections.Generic;
using System.Xml.Linq;
using DFWV.WorldClasses.EntityClasses;

namespace DFWV.WorldClasses.HistoricalFigureClasses
{
    struct EntitySquadLink
    {
        private int SquadId { get; }
        private int SquadPosition { get; }
        public int EntityId { get; }
        private int StartYear { get; }

        public HistoricalFigure Hf { get; }

        public Entity Entity => Hf.World.Entities[EntityId];


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
                        SquadId = valI;
                        break;
                    case "squad_position":
                        SquadPosition = valI;
                        break;
                    case "entity_id":
                        EntityId = valI;
                        break;
                    case "start_year":
                        StartYear = valI;
                        break;
                }
            }

            

            Hf = hf;
        }

        public override string ToString()
        {
            if (Entity == null)
                return SquadId + ": " + SquadPosition + " - " + EntityId + " - " + (StartYear != 0 ? StartYear.ToString() : "?");
            return SquadId + ": " + SquadPosition + " - " + Entity.Name + " - " + (StartYear != 0 ? StartYear.ToString() : "?");
        }


        internal void Export(int hfid)
        {
            var table = "HF_" + GetType().Name;



            var vals = new List<object> { hfid, SquadId, EntityId, StartYear };


            Database.ExportWorldItem(table, vals);
        }
    }

}