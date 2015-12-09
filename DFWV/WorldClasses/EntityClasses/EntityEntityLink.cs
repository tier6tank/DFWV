using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace DFWV.WorldClasses.EntityClasses
{
    public class EntityEntityLink
    {
        public static List<string> LinkTypes = new List<string>();

        public int LinkType { get; private set; }
        public Entity thisEntity { get; private set; }
        public int? TargetEntityID { get; private set; }

        public Entity Target => TargetEntityID.HasValue && thisEntity.World.Entities.ContainsKey(TargetEntityID.Value) 
            ? thisEntity.World.Entities[TargetEntityID.Value] 
            : null;

        public int? LinkStrength { get; private set; }

        public EntityEntityLink(XContainer data, Entity ent)
        {
            var linktypename = data.Element("type").Value;
            if (!LinkTypes.Contains(linktypename))
                LinkTypes.Add(linktypename);
            LinkType = LinkTypes.IndexOf(linktypename);

            if (data.Element("target") != null)
                TargetEntityID = Convert.ToInt32(data.Element("target").Value);

            if (data.Elements("strength").Any())
                LinkStrength = Convert.ToInt32(data.Element("strength").Value);

            thisEntity = ent;
        }


        public override string ToString()
        {
            //TODO Update this
            if (Target == null)
                return LinkTypes[LinkType] + " of " + TargetEntityID;
            return LinkTypes[LinkType] + " of " + Target.Name;
        }

        internal void Export(int HFID)
        {
            var table = "Entity_" + GetType().Name;

            var vals = new List<object>
            {
                thisEntity.ID, 
                LinkType.DBExport(LinkTypes), 
                TargetEntityID.DBExport(),
                LinkStrength.DBExport()
            };

            Database.ExportWorldItem(table, vals);
        }

    }
}