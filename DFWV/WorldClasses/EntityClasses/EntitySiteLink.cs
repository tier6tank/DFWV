using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace DFWV.WorldClasses.EntityClasses
{
    public class EntitySiteLink
    {
        public static List<string> LinkTypes = new List<string>();

        public int LinkType { get; private set; }
        public Entity thisEntity { get; private set; }

        public int? SiteID { get; private set; }

        public int? LinkStrength { get; private set; }

        public Site Site => SiteID.HasValue && thisEntity.World.Sites.ContainsKey(SiteID.Value) 
            ? thisEntity.World.Sites[SiteID.Value] 
            : null;


        public EntitySiteLink(XContainer data, Entity ent)
        {
            var linktypename = data.Element("type").Value;
            if (!LinkTypes.Contains(linktypename))
                LinkTypes.Add(linktypename);
            LinkType = LinkTypes.IndexOf(linktypename);

            if (data.Elements("site").Count() != 0)
                SiteID = Convert.ToInt32(data.Element("site").Value);

            if (data.Elements("strength").Count() != 0)
                LinkStrength = Convert.ToInt32(data.Element("strength").Value);

            thisEntity = ent;

        }

        public override string ToString()
        {
            //TODO Update this
            if (Site == null)
                return LinkType + ": " + SiteID;
            return LinkType + ": " + Site.Name;
        }

        internal void Export(int HFID)
        {
            var table = "Entity_" + GetType().Name;

            var vals = new List<object>
            {
                thisEntity.ID, 
                LinkType.DBExport(LinkTypes), 
                SiteID.DBExport(),
                LinkStrength.DBExport()
            };

            Database.ExportWorldItem(table, vals);

        }

    }
}