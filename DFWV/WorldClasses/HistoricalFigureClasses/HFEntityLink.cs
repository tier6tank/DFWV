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
        public int LinkType { get; }
        public int? EntityId { get; }
        public HistoricalFigure Hf { get; }

        public Entity Entity => EntityId.HasValue ? Hf.World.Entities[EntityId.Value] : null;

        public int? LinkStrength { get; }

        public HE_AddHFEntityLink AddEvent { get; set; }
        public HE_RemoveHFEntityLink RemoveEvent { get; set; }

        public HFEntityLink(XContainer data, HistoricalFigure hf)
        {
            var linktypename = data.Element("link_type").Value;
            if (!LinkTypes.Contains(linktypename))
                LinkTypes.Add(linktypename);
            LinkType = LinkTypes.IndexOf(linktypename);

            if (data.Element("entity_id") != null)
                EntityId = Convert.ToInt32(data.Element("entity_id").Value);

            if (data.Elements("link_strength").Any())
                LinkStrength = Convert.ToInt32(data.Element("link_strength").Value);

            Hf = hf;
        }


        public override string ToString()
        {
            if (Entity == null)
                return LinkType + " of " + EntityId;
            return LinkType + " of " + Entity.Name;
        }

        internal void Export(int hfid)
        {
            var table = "HF_" + GetType().Name;

            var vals = new List<object> { hfid, LinkTypes[LinkType], EntityId};

            if (LinkStrength.HasValue)
                vals.Add(LinkStrength.Value);
            else
                vals.Add(DBNull.Value);

            Database.ExportWorldItem(table, vals);
        }

    }
}