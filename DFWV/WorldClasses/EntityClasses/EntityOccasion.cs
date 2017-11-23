
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;
using DFWV.Annotations;
using DFWV.WorldClasses.HistoricalEventClasses;
using DFWV.WorldClasses.HistoricalEventCollectionClasses;
using DFWV.WorldClasses.HistoricalFigureClasses;

namespace DFWV.WorldClasses.EntityClasses
{
    public class EntityOccasion
    {
        public short? Id { get; set; }
        public string Name { get; set; }
        public int? EventId { get; set; }
        public Entity ThisEntity { get; }

        public List<EntityOccasionSchedule> Schedules { get; }

        public EntityOccasion(XContainer data, Entity ent)
        {
            Id = Convert.ToInt16(data.Element("id").Value);
            Name = data.Element("name").Value;
            EventId = Convert.ToInt32(data.Element("event").Value);
            if (data.Elements("schedule").Any())
                Schedules = new List<EntityOccasionSchedule>();
            foreach (var elem in data.Elements("schedule"))
            {
                Schedules.Add(new EntityOccasionSchedule(elem, this));
            }

            ThisEntity = ent;
        }

        internal void Export()
        {
            throw new NotImplementedException();
        }
    }
}
