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
    class EntityPositionLink
    {
        public int PositionProfileID { get; set; }
        public int EntityID { get; set; }
        public Entity Entity { get; set; }
        public int StartYear { get; set; }

        public EntityPositionLink(XElement data)
        {
            PositionProfileID = Convert.ToInt32(data.Element("position_profile_id").Value);
            EntityID = Convert.ToInt32(data.Element("entity_id").Value);
            StartYear = Convert.ToInt32(data.Element("start_year").Value);
        }

        public override string ToString()
        {
            if (Entity == null)
                return PositionProfileID + ": " + EntityID + " - " + StartYear;
            else
                return PositionProfileID  + ": " + Entity.Name + " - " + StartYear;
        }


        internal void Export(int HFID)
        {
            List<object> vals;
            string table = "HF_" + this.GetType().Name.ToString();



            vals = new List<object>() { HFID, PositionProfileID, EntityID, StartYear };


            Database.ExportWorldItem(table, vals);
        }
    }

}