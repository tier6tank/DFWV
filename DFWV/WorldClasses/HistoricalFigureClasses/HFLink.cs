using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using DFWV.WorldClasses.HistoricalEventClasses;

namespace DFWV.WorldClasses.HistoricalFigureClasses
{
    public class HFLink
    {
        public static List<string> LinkTypes = new List<string>();
        public int LinkType { get; private set; }
        public int LinkedHFID { get; private set; }
        public int LinkStrength { get; private set; }
        public HistoricalFigure thisHF { get; private set; }

        public HistoricalFigure HF => thisHF.World.HistoricalFigures.ContainsKey(LinkedHFID) ? thisHF.World.HistoricalFigures[LinkedHFID] : null;

        public HE_AddHFHFLink AddEvent { get; set; }
        //public HE_RemoveHFHFLink RemoveEvent { get; set; }

        public HFLink(XContainer data, HistoricalFigure hf)
        {

            var linktypename = data.Element("link_type").Value;
            if (!LinkTypes.Contains(linktypename))
                LinkTypes.Add(linktypename);
            LinkType = LinkTypes.IndexOf(linktypename);

            LinkedHFID = Convert.ToInt32(data.Element("hfid").Value);
            if (data.Elements("link_strength").Any())
                LinkStrength = Convert.ToInt32(data.Element("link_strength").Value);

            thisHF = hf;
        }

        public override string ToString()
        {
            if (HF == null)
                return thisHF + " " + LinkTypes[LinkType] + " " + LinkedHFID;
            return thisHF + " " + LinkTypes[LinkType] + " " + HF;
        }

        internal void Export(int HFID)
        {
            var table = "HF_" + GetType().Name;



            var vals = new List<object> { HFID, LinkTypes[LinkType], LinkedHFID, LinkStrength };


            Database.ExportWorldItem(table, vals);

        }

    }
}