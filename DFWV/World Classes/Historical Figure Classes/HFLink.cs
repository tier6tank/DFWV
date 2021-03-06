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
    class HFLink
    {
        public static List<string> LinkTypes = new List<string>();
        public int LinkType { get; set; }
        public int LinkedHFID { get; set; }
        public int LinkStrength { get; set; }
        public HistoricalFigure HF { get; set; }
        public HistoricalEvent Event { get; set; }

        public HFLink(XElement data)
        {

            string linktypename = data.Element("link_type").Value;
            if (!LinkTypes.Contains(linktypename))
                LinkTypes.Add(linktypename);
            LinkType = LinkTypes.IndexOf(linktypename);

            LinkedHFID = Convert.ToInt32(data.Element("hfid").Value);
            if (data.Elements("link_strength").Count() > 0)
                LinkStrength = Convert.ToInt32(data.Element("link_strength").Value);

        }

        public override string ToString()
        {
            if (HF == null)
                return LinkType + ": " + LinkedHFID;
            else
                return LinkType + ": " + HF.ToString();
        }

        internal void Export(int HFID)
        {
            List<object> vals;
            string table = "HF_" + this.GetType().Name.ToString();



            vals = new List<object>() { HFID, LinkTypes[LinkType], LinkedHFID, LinkStrength };


            Database.ExportWorldItem(table, vals);

        }

    }
}