using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using DFWV.WorldClasses.EntityClasses;
using DFWV.WorldClasses.HistoricalEventClasses;

namespace DFWV.WorldClasses.HistoricalFigureClasses
{
    public class HFEntityLink
    {
        public static List<string> LinkTypes = new List<string>();

        public static List<string> Positions = new List<string>();
        public int LinkType { get; private set; }
        public int? EntityID { get; private set; }
        public HistoricalFigure HF { get; private set; }

        public Entity Entity => EntityID.HasValue ? HF.World.Entities[EntityID.Value] : null;

        public int? LinkStrength { get; private set; }

        public HE_AddHFEntityLink AddEvent { get; set; }
        public HE_RemoveHFEntityLink RemoveEvent { get; set; }

        public HFEntityLink(XContainer data, HistoricalFigure hf)
        {
            var linktypename = data.Element("link_type").Value;
            if (!LinkTypes.Contains(linktypename))
                LinkTypes.Add(linktypename);
            LinkType = LinkTypes.IndexOf(linktypename);

            if (data.Element("entity_id") != null)
                EntityID = Convert.ToInt32(data.Element("entity_id").Value);

            if (data.Elements("link_strength").Any())
                LinkStrength = Convert.ToInt32(data.Element("link_strength").Value);

            HF = hf;
        }


        public override string ToString()
        {
            if (Entity == null)
                return LinkType + " of " + EntityID;
            return LinkType + " of " + Entity.Name;
        }

        internal void Export(int HFID)
        {
            var table = "HF_" + GetType().Name;

            var vals = new List<object> { HFID, LinkTypes[LinkType], EntityID};

            if (LinkStrength.HasValue)
                vals.Add(LinkStrength.Value);
            else
                vals.Add(DBNull.Value);

            Database.ExportWorldItem(table, vals);
        }

    }
}