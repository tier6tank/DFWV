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
    class EntityLink
    {
        public static List<string> LinkTypes = new List<string>();
        public int LinkType { get; set; }
        public int EntityID { get; set; }
        public Entity Entity { get; set; }
        public int? LinkStrength { get; set; }

        public EntityLink(XElement data)
        {
            string linktypename = data.Element("link_type").Value;
            if (!LinkTypes.Contains(linktypename))
                LinkTypes.Add(linktypename);
            LinkType = LinkTypes.IndexOf(linktypename);

            EntityID = Convert.ToInt32(data.Element("entity_id").Value);
            if (data.Elements("link_strength").Count() > 0)
                LinkStrength = Convert.ToInt32(data.Element("link_strength").Value);
        }

        public override string ToString()
        {
            if (Entity == null)
                return LinkType + " of " + EntityID;
            else
                return LinkType + " of " + Entity.Name;
        }

        internal void Export(int HFID)
        {
            List<object> vals;
            string table = "HF_" + this.GetType().Name.ToString();



            vals = new List<object>() { HFID, LinkTypes[LinkType], EntityID};

            if (LinkStrength.HasValue)
                vals.Add(LinkStrength.Value);
            else
                vals.Add(DBNull.Value);


            Database.ExportWorldItem(table, vals);
        }

    }
}