using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Xml.Linq;
using DFWV.Annotations;
using DFWV.WorldClasses.EntityClasses;
using DFWV.WorldClasses.HistoricalEventCollectionClasses;
using DFWV.WorldClasses.HistoricalFigureClasses;

namespace DFWV.WorldClasses
{
    public class EntityPopulation : XmlObject
    {

        public List<EcBattle> BattleEventCollections { get; set; }
        public List<HistoricalFigure> Members { get; set; }
        public Race Race { private get; set; }
        [UsedImplicitly]
        public string RaceName => Race != null ? Race.Name : "";

        [UsedImplicitly]
        public int MemberCount => Members?.Count ?? 0;

        [UsedImplicitly]
        public int Battles => BattleEventCollections?.Count ?? 0;


        [UsedImplicitly]
        public string DispNameLower => ToString().ToLower();

        override public Point Location => Point.Empty;

        public Dictionary<Race, int> RaceCounts { get; set; }

        public int? EntityId { get; set; }
        public Entity Entity { get; set; }


        public EntityPopulation(XDocument xdoc, World world)
            : base(xdoc, world)
        {
            foreach (var element in xdoc.Root.Elements())
            {
                // string val = element.Value;
                switch (element.Name.LocalName)
                {
                    case "id":
                        break;
                    default:
                        DfxmlParser.UnexpectedXmlElement(xdoc.Root.Name.LocalName, element, xdoc.Root.ToString());
                        break;
                }
            }
        }

        public override string ToString()
        {
            return Id.ToString();
        }

        public override void Select(MainForm frm)
        {
            if (frm.grpEntityPopulation.Text == ToString() && frm.MainTab.SelectedTab == frm.tabEntityPopulation)
                return;
            Program.MakeSelected(frm.tabEntityPopulation, frm.lstEntityPopulation, this);

            frm.grpEntityPopulation.Text = ToString();
            frm.grpEntityPopulation.Show();

            frm.lblEntityPopulationRace.Data = Race;
            frm.lblEntityPopulationCiv.Data = Entity;

            frm.grpEntityPopulationBattles.FillListboxWith(frm.lstEntityPopulationBattles, BattleEventCollections);
            frm.grpEntityPopulationMembers.FillListboxWith(frm.lstEntityPopulationMembers, Members);
            frm.grpEntityPopluationRaces.FillListboxWith(frm.lstEntityPopluationRaces, RaceCounts.Keys);


            frm.grpEntityPopulationMembers.Text =
                $"Members ({frm.lstEntityPopulationMembers.Items.Count}{(Members != null && Members.Count > 50000 ? "+" : "")})";
        }

        internal override void Link()
        {
            if (EntityId.HasValue && World.Entities.ContainsKey(EntityId.Value))
                Entity = World.Entities[EntityId.Value];
        }

        internal override void Process()
        {
            if (Race == null && RaceCounts != null)
                Race = RaceCounts.First().Key;
        }

        internal override void Plus(XDocument xdoc)
        {
            foreach (var element in xdoc.Root.Elements())
            {
                var val = element.Value;
                int valI;
                int.TryParse(val, out valI);

                switch (element.Name.LocalName)
                {
                    case "id":
                        break;
                    case "race":
                        var raceName = val.Split(':')[0];
                        var race = World.GetAddRace(raceName);
                        if (RaceCounts == null)
                            RaceCounts = new Dictionary<Race, int>();
                        RaceCounts.Add(race, Convert.ToInt32(val.Split(':')[1]));
                        break;
                    case "civ_id":
                        EntityId = valI;
                        break;
                    default:
                        DfxmlParser.UnexpectedXmlElement(xdoc.Root.Name.LocalName + "\t", element, xdoc.Root.ToString());
                        break;
                }
            }
        }

        internal override void Export(string table)
        {

            var vals = new List<object>
            {
                Id,
                Name.DBExport(),
                EntityId.DBExport()
            };

            Database.ExportWorldItem(table, vals);

            if (RaceCounts == null) return;
            foreach (var raceCount in RaceCounts)
                Database.ExportWorldItem("EntityPop_RaceCounts", new List<object> { Id, raceCount.Key.ToString(), raceCount.Value });
        }

    }
}