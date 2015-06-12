using System;
using System.Collections.Generic;
using System.Xml.Linq;
using DFWV.WorldClasses.EntityClasses;

namespace DFWV.WorldClasses.HistoricalFigureClasses
{
    public struct EntityFormerPositionLink
    {
        public int PositionProfileID { get; set; }
        public int EntityID { get; private set; }
        public HistoricalFigure HF { get; private set; }

        public Entity Entity
        {
            get
            {
                return HF.World.Entities[EntityID];
            }
        }

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
                        PositionProfileID = valI;
                        break;
                    case "entity_id":
                        EntityID = valI;
                        break;
                    case "start_year":
                        StartYear = valI;
                        break;
                    case "end_year":
                        EndYear = valI;
                        break;
                }
            }
            HF = hf;
        }

        public override string ToString()
        {
            if (Entity == null)
                return PositionProfileID + ": " + EntityID + " - " + (StartYear != 0 ? StartYear.ToString() : "?") + " - " + EndYear;
            return PositionProfileID + ": " + Entity.Name + " - " + (StartYear != 0 ? StartYear.ToString() : "?") + " - " + EndYear;
        }

        internal void Export(int HFID)
        {
            var table = "HF_" + GetType().Name;

            var vals = new List<object> {HFID, PositionProfileID, EntityID, StartYear, EndYear };

            Database.ExportWorldItem(table, vals);
        }

    }

}