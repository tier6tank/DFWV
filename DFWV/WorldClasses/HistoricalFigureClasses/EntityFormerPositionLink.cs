using System.Collections.Generic;
using System.Xml.Linq;
using DFWV.WorldClasses.EntityClasses;

namespace DFWV.WorldClasses.HistoricalFigureClasses
{
    public struct EntityFormerPositionLink
    {
        public int PositionProfileId { get; set; }
        public int EntityId { get; }
        public HistoricalFigure Hf { get; }

        public Entity Entity => Hf.World.Entities[EntityId];

        public int StartYear { get; set; }
        public int EndYear { get; set; }

        public EntityFormerPositionLink(XContainer data, HistoricalFigure hf) : this()
        {
            foreach (var element in data.Elements())
            {
                var val = element.Value;
                int valI;
                int.TryParse(val, out valI);
                switch (element.Name.LocalName)
                {
                    case "position_profile_id":
                        PositionProfileId = valI;
                        break;
                    case "entity_id":
                        EntityId = valI;
                        break;
                    case "start_year":
                        StartYear = valI;
                        break;
                    case "end_year":
                        EndYear = valI;
                        break;
                }
            }
            Hf = hf;
        }

        public override string ToString()
        {
            if (Entity == null)
                return PositionProfileId + ": " + EntityId + " - " + (StartYear != 0 ? StartYear.ToString() : "?") + " - " + EndYear;
            return PositionProfileId + ": " + Entity.Name + " - " + (StartYear != 0 ? StartYear.ToString() : "?") + " - " + EndYear;
        }

        internal void Export(int hfid)
        {
            var table = "HF_" + GetType().Name;

            var vals = new List<object> {hfid, PositionProfileId, EntityId, StartYear, EndYear };

            Database.ExportWorldItem(table, vals);
        }

    }

}