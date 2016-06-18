using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace DFWV.WorldClasses.EntityClasses
{
    public class EntityPositionAssignment
    {
        public static List<string> LinkTypes = new List<string>();

        public Entity ThisEntity { get; }
        public short? Id { get; set; }
        public int? HistFigId { get; set; }
        public int? PositionId { get; set; }
        public int? SquadId { get; set; }


        public EntityPositionAssignment(XContainer data, Entity ent)
        {
            Id = Convert.ToInt16(data.Element("id").Value);
            HistFigId = Convert.ToInt32(data.Element("histfig").Value);
            PositionId = Convert.ToInt32(data.Element("position_id").Value);
            SquadId = Convert.ToInt32(data.Element("squad_id").Value);

            ThisEntity = ent;
        }

        internal void Export()
        {
            var table = "Entity_" + GetType().Name;

            var vals = new List<object>
            {
                ThisEntity.Id,

            };

            Database.ExportWorldItem(table, vals);

        }

    }
}