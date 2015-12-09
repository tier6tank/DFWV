using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using DFWV.WorldClasses.HistoricalEventClasses;

namespace DFWV.WorldClasses.HistoricalFigureClasses
{
    public class HfLink
    {
        public static List<string> LinkTypes = new List<string>();
        public int LinkType { get; }
        public int LinkedHfid { get; }
        public int LinkStrength { get; }
        public HistoricalFigure ThisHf { get; }

        public HistoricalFigure Hf => ThisHf.World.HistoricalFigures.ContainsKey(LinkedHfid) ? ThisHf.World.HistoricalFigures[LinkedHfid] : null;

        public HeAddHfhfLink AddEvent { get; set; }
        //public HE_RemoveHFHFLink RemoveEvent { get; set; }

        public HfLink(XContainer data, HistoricalFigure hf)
        {

            var linktypename = data.Element("link_type").Value;
            if (!LinkTypes.Contains(linktypename))
                LinkTypes.Add(linktypename);
            LinkType = LinkTypes.IndexOf(linktypename);

            LinkedHfid = Convert.ToInt32(data.Element("hfid").Value);
            if (data.Elements("link_strength").Any())
                LinkStrength = Convert.ToInt32(data.Element("link_strength").Value);

            ThisHf = hf;
        }

        public override string ToString()
        {
            if (Hf == null)
                return ThisHf + " " + LinkTypes[LinkType] + " " + LinkedHfid;
            return ThisHf + " " + LinkTypes[LinkType] + " " + Hf;
        }

        internal void Export(int hfid)
        {
            var table = "HF_" + GetType().Name;



            var vals = new List<object> { hfid, LinkTypes[LinkType], LinkedHfid, LinkStrength };


            Database.ExportWorldItem(table, vals);

        }

    }
}