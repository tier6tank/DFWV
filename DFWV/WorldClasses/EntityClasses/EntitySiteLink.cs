using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace DFWV.WorldClasses.EntityClasses
{
    public class EntitySiteLink
    {
        public static List<string> LinkTypes = new List<string>();

        public int LinkType { get; }
        public Entity ThisEntity { get; }

        public int? SiteId { get; }

        public int? LinkStrength { get; }

        public Site Site => SiteId.HasValue && ThisEntity.World.Sites.ContainsKey(SiteId.Value) 
            ? ThisEntity.World.Sites[SiteId.Value] 
            : null;


        public EntitySiteLink(XContainer data, Entity ent)
        {
            var linktypename = data.Element("type").Value;
            if (!LinkTypes.Contains(linktypename))
                LinkTypes.Add(linktypename);
            LinkType = LinkTypes.IndexOf(linktypename);

            if (data.Elements("site").Count() != 0)
                SiteId = Convert.ToInt32(data.Element("site").Value);

            if (data.Elements("strength").Count() != 0)
                LinkStrength = Convert.ToInt32(data.Element("strength").Value);

            ThisEntity = ent;

        }

        public override string ToString()
        {
            //TODO Update this
            if (Site == null)
                return LinkType + ": " + SiteId;
            return LinkType + ": " + Site.Name;
        }

        internal void Export(int hfid)
        {
            var table = "Entity_" + GetType().Name;

            var vals = new List<object>
            {
                ThisEntity.Id, 
                LinkType.DBExport(LinkTypes), 
                SiteId.DBExport(),
                LinkStrength.DBExport()
            };

            Database.ExportWorldItem(table, vals);

        }

    }
}