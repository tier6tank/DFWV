
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
    public class EntityOccasionScheduleFeature
    {
        public string Type { get; set; }
        public EntityOccasionSchedule ThisSchedule { get; }
        public int? ReferenceId { get; }


        public EntityOccasionScheduleFeature(XContainer data, EntityOccasionSchedule schedule)
        {
            Type = data.Element("type").Value;
            ReferenceId = Convert.ToInt32(data.Element("reference").Value);

            ThisSchedule = schedule;
        }

        internal void Export()
        {
            throw new NotImplementedException();
        }
    }
}
