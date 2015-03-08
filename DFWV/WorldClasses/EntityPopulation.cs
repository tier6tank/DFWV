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
    public class EntityPopulation : XMLObject
    {

        public List<EC_Battle> BattleEventCollections { get; set; }
        public List<HistoricalFigure> Members { get; set; }
        public Race Race { private get; set; }
        [UsedImplicitly]
        public string RaceName { get { return Race != null ? Race.Name : ""; } }

        [UsedImplicitly]
        public int MemberCount { get { return Members == null ? 0 : Members.Count; } }
        [UsedImplicitly]
        public int Battles { get { return BattleEventCollections == null ? 0 : BattleEventCollections.Count; } }


        [UsedImplicitly]
        public string DispNameLower { get { return ToString().ToLower(); } }

        override public Point Location { get { return Point.Empty; } }

        public Dictionary<Race, int> RaceCounts { get; set; }

        public int? EntityID { get; set; }
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
                        DFXMLParser.UnexpectedXMLElement(xdoc.Root.Name.LocalName, element, xdoc.Root.ToString());
                        break;
                }
            }
        }

        public override string ToString()
        {
            return ID.ToString();
        }

        public override void Select(MainForm frm)
        {
            frm.grpEntityPopulation.Text = ToString();
            frm.grpEntityPopulation.Show();

            frm.lblEntityPopulationRace.Data = Race;
            frm.lblEntityPopulationCiv.Data = Entity;

            frm.grpEntityPopulationBattles.Visible = BattleEventCollections != null;
            if (BattleEventCollections != null)
            {
                BattleEventCollections = BattleEventCollections.Distinct().ToList();
                frm.lstEntityPopulationBattles.BeginUpdate();
                frm.lstEntityPopulationBattles.Items.Clear();
                frm.lstEntityPopulationBattles.Items.AddRange(BattleEventCollections.ToArray());
                frm.lstEntityPopulationBattles.EndUpdate();
                frm.lstEntityPopulationBattles.SelectedIndex = 0;
                frm.grpEntityPopulationBattles.Text = String.Format("Battles ({0})",
                    frm.lstEntityPopulationBattles.Items.Count);
            }

            frm.grpEntityPopulationMembers.Visible = Members != null;
            if (Members != null)
            {
                frm.lstEntityPopulationMembers.BeginUpdate();
                frm.lstEntityPopulationMembers.Items.Clear();
                frm.lstEntityPopulationMembers.Items.AddRange(Members.ToArray());
                frm.lstEntityPopulationMembers.EndUpdate();
            }

            frm.lstEntityPopluationRaces.Items.Clear();
            frm.grpEntityPopluationRaces.Visible = RaceCounts != null;
            if (RaceCounts != null)
                frm.lstEntityPopluationRaces.Items.AddRange(RaceCounts.Keys.ToArray());


            frm.grpEntityPopulationMembers.Text = string.Format("Members ({0}{1})", 
                frm.lstEntityPopulationMembers.Items.Count, 
                (Members != null && Members.Count > 50000 ? "+" : ""));
            Program.MakeSelected(frm.tabEntityPopulation, frm.lstEntityPopulation, this);
        }

        internal override void Link()
        {
            if (EntityID.HasValue && World.Entities.ContainsKey(EntityID.Value))
                Entity = World.Entities[EntityID.Value];
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
                Int32.TryParse(val, out valI);

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
                        EntityID = valI;
                        break;
                    default:
                        DFXMLParser.UnexpectedXMLElement(xdoc.Root.Name.LocalName + "\t", element, xdoc.Root.ToString());
                        break;
                }
            }
        }

        internal override void Export(string table)
        {

            var vals = new List<object>
            {
                ID,
                Name.DBExport(),
                EntityID.DBExport()
            };

            Database.ExportWorldItem(table, vals);

            if (RaceCounts == null) return;
            foreach (var raceCount in RaceCounts)
                Database.ExportWorldItem("EntityPop_RaceCounts", new List<object> { ID, raceCount.Key.ToString(), raceCount.Value });
        }

    }
}
