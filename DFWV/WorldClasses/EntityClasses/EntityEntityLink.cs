﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace DFWV.WorldClasses.EntityClasses
{
    class EntityEntityLink
    {
        public static List<string> LinkTypes = new List<string>();

        public int LinkType { get; private set; }
        public Entity thisEntity { get; private set; }
        public int? TargetEntityID { get; private set; }

        public Entity Target
        {
            get
            {
                return TargetEntityID.HasValue && thisEntity.World.Entities.ContainsKey(TargetEntityID.Value) 
                    ? thisEntity.World.Entities[TargetEntityID.Value] 
                    : null; 
            }
        }

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
            //TODO Update this
            var table = "HF_" + GetType().Name;



            var vals = new List<object> { HFID, LinkTypes[LinkType]};

            if (LinkStrength.HasValue)
                vals.Add(LinkStrength.Value);
            else
                vals.Add(DBNull.Value);


            Database.ExportWorldItem(table, vals);
        }

    }
}
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                              