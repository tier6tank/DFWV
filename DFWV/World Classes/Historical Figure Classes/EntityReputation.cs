using DFWV.WorldClasses.HistoricalEventClasses;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;

namespace DFWV.WorldClasses.HistoricalFigureClasses
{
     class EntityReputation
    {
        public int EntityID { get; set; }
        public Entity Entity { get; set; }
        public int? FirstAgelessYear { get; set; }
        public int? FirstAgelessSeasonCount { get; set; }
        public int? UnsolvedMurders { get; set; }

        public EntityReputation(XElement data)
        {
            EntityID = Convert.ToInt32(data.Element("entity_id").Value);

            if (data.Element("first_ageless_year") != null)
            {
                FirstAgelessYear = Convert.ToInt32(data.Element("first_ageless_year").Value);
                FirstAgelessSeasonCount = Convert.ToInt32(data.Element("first_ageless_season_count").Value);
            }
            else if (data.Element("unsolved_murders") != null)
            {
                UnsolvedMurders = Convert.ToInt32(data.Element("unsolved_murders").Value);
            }
            else
                return;
        }

        public override string ToString()
        {
            string data;
            if (UnsolvedMurders.HasValue)
                data = "Unsolved Murders - " + UnsolvedMurders.Value.ToString();
            else
                data = "FirstAgelessYear - " + FirstAgelessYear.Value + " - " + FirstAgelessSeasonCount;

            if (Entity == null)
                return EntityID + " - " + data;
            else
                return Entity.Name + " - " + data;

        }

        internal void Export(int HFID)
        {
            List<object> vals;
            string table = "HF_" + this.GetType().Name.ToString();



            vals = new List<object>() { HFID, EntityID, FirstAgelessYear, FirstAgelessSeasonCount, UnsolvedMurders};


            Database.ExportWorldItem(table, vals);
        }

    }

}