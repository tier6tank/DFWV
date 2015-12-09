using System;
using System.Collections.Generic;
using System.Xml.Linq;
using DFWV.WorldClasses.EntityClasses;

namespace DFWV.WorldClasses.HistoricalFigureClasses
{
     class EntityReputation
    {
        public int EntityId { get; }
        public Entity Entity { private get; set; }
        private int? FirstAgelessYear { get; }
        private int? FirstAgelessSeasonCount { get; }
        private int? UnsolvedMurders { get; }

        public EntityReputation(XContainer data)
        {
            EntityId = Convert.ToInt32(data.Element("entity_id").Value);

            if (data.Element("first_ageless_year") != null)
            {
                FirstAgelessYear = Convert.ToInt32(data.Element("first_ageless_year").Value);
                FirstAgelessSeasonCount = Convert.ToInt32(data.Element("first_ageless_season_count").Value);
            }
            else if (data.Element("unsolved_murders") != null)
            {
                UnsolvedMurders = Convert.ToInt32(data.Element("unsolved_murders").Value);
            }
        }

        public override string ToString()
        {
            var data = "";
            if (UnsolvedMurders.HasValue)
                data = "Unsolved Murders - " + UnsolvedMurders.Value;
            else if (FirstAgelessYear != null)
                data = "FirstAgelessYear - " + FirstAgelessYear.Value + " - " + FirstAgelessSeasonCount;

            if (Entity == null)
                return EntityId + " - " + data;
            return Entity.Name + " - " + data;
        }

        internal void Export(int hfid)
        {
            var table = "HF_" + GetType().Name;



            var vals = new List<object> { hfid, EntityId, FirstAgelessYear, FirstAgelessSeasonCount, UnsolvedMurders};


            Database.ExportWorldItem(table, vals);
        }

    }

}