
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
    public class EntityOccasionSchedule
    {
        public short? Id { get; set; }
        public string Type { get; set; }
        public EntityOccasion ThisOccasion { get; }

        public List<EntityOccasionScheduleFeature> Features { get; }

        public EntityOccasionSchedule(XContainer data, EntityOccasion occasion)
        {
            Id = Convert.ToInt16(data.Element("id").Value);
            Type = data.Element("type").Value;
            if (data.Elements("feature").Any())
                Features = new List<EntityOccasionScheduleFeature>();
            foreach (var elem in data.Elements("feature"))
            {
                Features.Add(new EntityOccasionScheduleFeature(elem, this));
            }

            ThisOccasion = occasion;
        }

        internal void Export()
        {
            throw new NotImplementedException();
        }
    }
}
