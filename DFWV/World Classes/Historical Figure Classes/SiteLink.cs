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
    class SiteLink
    {
        public static List<string> LinkTypes = new List<string>();
        public int LinkType { get; set; }
        public int SiteID { get; set; }
        public Site Site { get; set; }
        public int? SubID { get; set; }
        public int? EntityID { get; set; }
        public Entity Entity { get; set; }

        public SiteLink(XElement data)
        {
            string linktypename = data.Element("link_type").Value;
            if (!LinkTypes.Contains(linktypename))
                LinkTypes.Add(linktypename);
            LinkType = LinkTypes.IndexOf(linktypename);

            SiteID = Convert.ToInt32(data.Element("site_id").Value);
            if (data.Elements("sub_id").Count() != 0)
                SubID = Convert.ToInt32(data.Element("sub_id").Value);
            if (data.Elements("entity_id").Count() != 0)
                EntityID = Convert.ToInt32(data.Element("entity_id").Value);

        }

        public override string ToString()
        {
            if (Site == null)
                return LinkType + ": " + SiteID;
            else
                return LinkType + ": " + Site.Name;
        }

        internal void Export(int HFID)
        {
            List<object> vals;
            string table = "HF_" + this.GetType().Name.ToString();



            vals = new List<object>() { HFID, LinkTypes[LinkType], SiteID, SubID, EntityID };


            Database.ExportWorldItem(table, vals);

        }

    }
}