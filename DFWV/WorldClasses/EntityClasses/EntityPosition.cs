using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Xml.Linq;
using DFWV.WorldClasses.HistoricalFigureClasses;

namespace DFWV.WorldClasses.EntityClasses
{
    public class EntityPosition : WorldObject
    {
        public static List<string> LinkTypes = new List<string>();

        public Entity ThisEntity { get; }
        public short? Id { get; set; }
        public static List<string> PositionTitles = new List<string>();
        public int? Name { get; set; }
        public string NameText => Name.HasValue ? PositionTitles[Name.Value] : "";
        public override Point Location => ThisEntity.Location;

        public int? NameMale { get; set; }
        public int? NameFemale { get; set; }
        public int? Spouse { get; set; }
        public int? SpouseMale { get; set; }
        public int? SpouseFemale { get; set; }

        public EntityPosition(XContainer data, Entity ent)
        {
            Id = Convert.ToInt16(data.Element("id").Value);

            Name = GetPositionTitleIndex(data, "name");
            NameMale = GetPositionTitleIndex(data, "name_male");
            NameFemale = GetPositionTitleIndex(data, "name_female");
            Spouse = GetPositionTitleIndex(data, "spouse");
            SpouseMale = GetPositionTitleIndex(data, "spouse_male");
            SpouseFemale = GetPositionTitleIndex(data, "spouse_female");

            ThisEntity = ent;
            World = ThisEntity.World;
        }

        private int GetPositionTitleIndex(XContainer data, string fieldName)
        {
            if (data.Element(fieldName) == null)
                return -1;
            var val = data.Element(fieldName).Value;
            if (!PositionTitles.Contains(val))
                PositionTitles.Add(val);
            return PositionTitles.IndexOf(val);
        }

        internal override void Export(string Table)
        {
            var table = "Entity_" + GetType().Name;

            var vals = new List<object>
            {
                ThisEntity.Id,

            };

            Database.ExportWorldItem(table, vals);

        }

        public override string ToString()
        {
            var Assigned = GetAssignedHF() == null ? "Vacant" : GetAssignedHF().ToString();
            return $"{(NameText)} - {Assigned}";
        }

        public override void Select(MainForm frm)
        {
            var assignedHF = GetAssignedHF();
            assignedHF?.Select(frm);
        }

        private HistoricalFigure GetAssignedHF()
        {
            var HFid = ThisEntity.PositionAssignments.FirstOrDefault(p => p.Id == Id)?.HistFigId;
            if (HFid.HasValue && HFid.Value != -1 && World.HistoricalFigures.ContainsKey(HFid.Value))
            {
                return World.HistoricalFigures[HFid.Value];
            }
            return null;
        }
    }
}