using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using DFWV.WorldClasses.EntityClasses;
using DFWV.WorldClasses.HistoricalEventClasses;

namespace DFWV.WorldClasses.HistoricalFigureClasses
{
    public class HFSiteLink
    {
        public static List<string> LinkTypes = new List<string>();
        public int LinkType { get; }
        public int SiteId { get; }
        private int? SubId { get; }
        private int? OccupationId { get; set; }
        public int? EntityId { get; }
        public HistoricalFigure Hf { get; }

        public Entity Entity => EntityId.HasValue && Hf.World.Entities.ContainsKey(EntityId.Value) ? Hf.World.Entities[EntityId.Value] : null;

        public Site Site => Hf.World.Sites[SiteId];

        public HE_AddHFSiteLink AddEvent { get; set; }
        public HE_RemoveHFSiteLink RemoveEvent { get; set; }

        public HFSiteLink(XContainer data, HistoricalFigure hf)
        {
            var linktypename = data.Element("link_type").Value;
            if (!LinkTypes.Contains(linktypename))
                LinkTypes.Add(linktypename);
            LinkType = LinkTypes.IndexOf(linktypename);

            SiteId = Convert.ToInt32(data.Element("site_id").Value);
            if (data.Elements("sub_id").Count() != 0)
                SubId = Convert.ToInt32(data.Element("sub_id").Value);
            if (data.Elements("occupation_id").Count() != 0)
                OccupationId = Convert.ToInt32(data.Element("occupation_id").Value);
            if (data.Elements("entity_id").Count() != 0)
                EntityId = Convert.ToInt32(data.Element("entity_id").Value);

            Hf = hf;
        }

        public override string ToString()
        {
            if (Site == null)
                return LinkType + ": " + SiteId;
            return LinkType + ": " + Site.Name;
        }

        internal void Export(int hfid)
        {
            var table = "HF_" + GetType().Name;



            var vals = new List<object> { hfid, LinkTypes[LinkType], SiteId, SubId, EntityId };


            Database.ExportWorldItem(table, vals);

        }

    }
}