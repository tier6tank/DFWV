using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace DFWV.WorldClasses.EntityClasses
{
    public class EntityEntityLink
    {
        public static List<string> LinkTypes = new List<string>();

        public int LinkType { get; }
        public Entity ThisEntity { get; }
        public int? TargetEntityId { get; }

        public Entity Target => TargetEntityId.HasValue && ThisEntity.World.Entities.ContainsKey(TargetEntityId.Value) 
            ? ThisEntity.World.Entities[TargetEntityId.Value] 
            : null;

        public int? LinkStrength { get; }

        public EntityEntityLink(XContainer data, Entity ent)
        {
            var linktypename = data.Element("type").Value;
            if (!LinkTypes.Contains(linktypename))
                LinkTypes.Add(linktypename);
            LinkType = LinkTypes.IndexOf(linktypename);

            if (data.Element("target") != null)
                TargetEntityId = Convert.ToInt32(data.Element("target").Value);

            if (data.Elements("strength").Any())
                LinkStrength = Convert.ToInt32(data.Element("strength").Value);

            ThisEntity = ent;
        }


        public override string ToString()
        {
            //TODO Update this
            if (Target == null)
                return LinkTypes[LinkType] + " of " + TargetEntityId;
            return LinkTypes[LinkType] + " of " + Target.Name;
        }

        internal void Export(int hfid)
        {
            var table = "Entity_" + GetType().Name;

            var vals = new List<object>
            {
                ThisEntity.Id, 
                LinkType.DBExport(LinkTypes), 
                TargetEntityId.DBExport(),
                LinkStrength.DBExport()
            };

            Database.ExportWorldItem(table, vals);
        }

    }
}