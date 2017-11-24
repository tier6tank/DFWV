using System.Drawing;
using System.Xml.Linq;
using DFWV.WorldClasses;
using DFWV.WorldClasses.EntityClasses;
using DFWV.WorldClasses.HistoricalEventClasses;
using DFWV.WorldClasses.HistoricalEventCollectionClasses;
using DFWV.WorldClasses.HistoricalFigureClasses;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Region = DFWV.WorldClasses.Region;

namespace DFWVTests
{
    /// <summary>
    /// Summary description for ParsingXML
    /// </summary>
    [TestClass]
    public class ParsingXML
    {
        [TestMethod]
        public void RegionXML_Parses()
        {
            var world = LoadingWorld.GetTestWorld();
            var xdoc = new XDocument(new XElement("region",
                    new XElement("id", 0),
                    new XElement("name", "the ocean of luster"),
                    new XElement("type", "Ocean")
                    )
                );

            var region = new Region(xdoc, world);

            Assert.AreEqual(region.Name, "the ocean of luster");
            Assert.AreEqual(Region.Types[region.Type], "Ocean");
        }

        [TestMethod]
        public void UGRegionXML_Parses()
        {
            var world = LoadingWorld.GetTestWorld();
            var xdoc = new XDocument(new XElement("underground_region",
                    new XElement("id", 0),
                    new XElement("type", "cavern"),
                    new XElement("depth", 1)
                    )
                );

            var ugregion = new UndergroundRegion(xdoc, world);


            Assert.AreEqual(ugregion.Id, 0);
            Assert.AreEqual(ugregion.Depth, 1);
            Assert.AreEqual(ugregion.Type , "Cavern");
        }

        [TestMethod]
        public void SiteXML_Parses()
        {
            var world = LoadingWorld.GetTestWorld();
            var xdoc = new XDocument(new XElement("site",
                    new XElement("id", 0),
                    new XElement("type", "cave"),
                    new XElement("name", "callgutter"),
                    new XElement("coords", "6,14")
                    )
                );

            var site = new Site(xdoc, world);


            Assert.AreEqual(site.Id, 0);
            Assert.AreEqual(Site.Types[site.Type.Value], "cave");
            Assert.AreEqual(site.Name, "callgutter");
            Assert.AreEqual(site.Coords, new Point(6, 14));
        }

        [TestMethod]
        public void ArtifactXML_Parses()
        {
            var world = LoadingWorld.GetTestWorld();
            var xdoc = new XDocument(new XElement("artifact",
                    new XElement("id", 3),
                    new XElement("name", "the fair orange"),
                    new XElement("item", "bibeomthu")
                    )
                );

            var artifact = new Artifact(xdoc, world);

            Assert.AreEqual(artifact.Id, 3);
            Assert.AreEqual(artifact.Name, "the fair orange");
            Assert.AreEqual(artifact.ItemName, "bibeomthu");
        }

        [TestMethod]
        public void HistoricalFigureDeityXML_Parses()
        {
            var world = LoadingWorld.GetTestWorld();
            var xdoc = new XDocument(new XElement("historical_figure",
                    new XElement("id", 303),
                    new XElement("name", "arist"),
                    new XElement("race", "DWARF"),
                    new XElement("caste", "FEMALE"),
                    new XElement("appeared", -1),
                    new XElement("birth_year", -1),
                    new XElement("birth_seconds72", -1),
                    new XElement("death_year", -1),
                    new XElement("death_seconds", -1),
                    new XElement("deity"),
                    new XElement("sphere", "fortresses"),
                    new XElement("sphere", "war")
                    )
                );

            var historicalFigure = new HistoricalFigure(xdoc, world);
            Assert.AreEqual(historicalFigure.Id, 303);
            Assert.AreEqual(historicalFigure.Name, "arist");
            Assert.AreEqual(historicalFigure.RaceName, "DWARF");
            Assert.AreEqual(HistoricalFigure.Castes[historicalFigure.Caste.Value], "FEMALE");
            Assert.AreEqual(historicalFigure.Appeared.Year, -1);
            Assert.AreEqual(historicalFigure.Birth.Year, -1);
            Assert.AreEqual(historicalFigure.Death, WorldTime.Present);
            Assert.IsTrue(historicalFigure.Deity);
            Assert.AreEqual(historicalFigure.Sphere.Count, 2);
            Assert.AreEqual(HistoricalFigure.Spheres[historicalFigure.Sphere[0]], "fortresses");
            Assert.AreEqual(HistoricalFigure.Spheres[historicalFigure.Sphere[1]], "war");


        }

        [TestMethod]
        public void HistoricalFigureComplexXML_Parses()
        {
            var world = LoadingWorld.GetTestWorld();
            var xdoc = new XDocument(new XElement("historical_figure",
                    new XElement("id", 308),
                    new XElement("name", "logem dancelabored"),
                    new XElement("race", "NIGHT_CREATURE_8"),
                    new XElement("caste", "FEMALE"),
                    new XElement("appeared", 1),
                    new XElement("birth_year", -55),
                    new XElement("birth_seconds72", -1),
                    new XElement("death_year", 126),
                    new XElement("death_seconds", -1),
                    new XElement("associated_type", "STANDARD"),
                    new XElement("hf_link",
                        new XElement("link_type", "deity"),
                        new XElement("hfid", 304),
                        new XElement("link_strength", 61)
                        ),
                    new XElement("hf_link",
                        new XElement("link_type", "child"),
                        new XElement("hfid", 501)
                        ),
                    new XElement("entity_link",
                        new XElement("link_type", "former member"),
                        new XElement("entity_id", 80),
                        new XElement("link_strength", 60)
                        ),
                    new XElement("entity_link",
                        new XElement("link_type", "enemy"),
                        new XElement("entity_id", 123)
                        ),
                    new XElement("entity_former_position_link",
                        new XElement("position_profile_id", 2),
                        new XElement("entity_id", 80),
                        new XElement("start_year", 1),
                        new XElement("end_year", 23)
                        ),
                    new XElement("hf_skill",
                        new XElement("skill", "INTIMIDATION"),
                        new XElement("total_ip", 5600)
                        ),
                    new XElement("ent_pop_id", 25)
                    )
                );

            var historicalFigure = new HistoricalFigure(xdoc, world);
            Assert.AreEqual(historicalFigure.Id, 308);
            Assert.AreEqual(historicalFigure.Name, "logem dancelabored");
            Assert.AreEqual(historicalFigure.RaceName, "NIGHT_CREATURE_8");
            Assert.AreEqual(HistoricalFigure.Castes[historicalFigure.Caste.Value], "FEMALE");
            Assert.AreEqual(historicalFigure.Appeared.Year, 1);
            Assert.AreEqual(historicalFigure.Birth.Year, -55);
            Assert.AreEqual(historicalFigure.Death.Year, 126);
            Assert.AreEqual(historicalFigure.HfLinks.Count, 2);
            Assert.AreEqual(historicalFigure.HfLinks[HFLink.LinkTypes.IndexOf("deity")].Count, 1);
            Assert.AreEqual(historicalFigure.HfLinks[HFLink.LinkTypes.IndexOf("deity")][0].LinkedHfid, 304);
            Assert.AreEqual(historicalFigure.HfLinks[HFLink.LinkTypes.IndexOf("deity")][0].LinkStrength, 61);
            Assert.AreEqual(historicalFigure.HfLinks[HFLink.LinkTypes.IndexOf("child")].Count, 1);
            Assert.AreEqual(historicalFigure.HfLinks[HFLink.LinkTypes.IndexOf("child")][0].LinkedHfid, 501);
            Assert.AreEqual(historicalFigure.EntityLinks[HFEntityLink.LinkTypes.IndexOf("former member")].Count, 1);
            Assert.AreEqual(historicalFigure.EntityLinks[HFEntityLink.LinkTypes.IndexOf("former member")][0].EntityId, 80);
            Assert.AreEqual(historicalFigure.EntityLinks[HFEntityLink.LinkTypes.IndexOf("former member")][0].LinkStrength, 60);
            Assert.AreEqual(historicalFigure.EntityLinks[HFEntityLink.LinkTypes.IndexOf("enemy")].Count, 1);
            Assert.AreEqual(historicalFigure.EntityLinks[HFEntityLink.LinkTypes.IndexOf("enemy")][0].EntityId, 123);
            Assert.AreEqual(historicalFigure.EntityFormerPositionLinks.Count, 1);
            Assert.AreEqual(historicalFigure.EntityFormerPositionLinks[0].PositionProfileId, 2);
            Assert.AreEqual(historicalFigure.EntityFormerPositionLinks[0].EntityId, 80);
            Assert.AreEqual(historicalFigure.EntityFormerPositionLinks[0].StartYear, 1);
            Assert.AreEqual(historicalFigure.EntityFormerPositionLinks[0].EndYear, 23);
            Assert.AreEqual(historicalFigure.HfSkills.Count, 1);
            Assert.AreEqual(HFSkill.Skills[historicalFigure.HfSkills[0].Skill], "INTIMIDATION");
            Assert.AreEqual(historicalFigure.HfSkills[0].TotalIp, 5600);
            Assert.AreEqual(historicalFigure.EntPopId, 25);
        }

        [TestMethod]
        public void EntityPopulationXML_Parses()
        {
            var world = LoadingWorld.GetTestWorld();
            var xdoc = new XDocument(new XElement("entity_population",
                    new XElement("id", 1)
                    )
                );

            var entitypopulation = new EntityPopulation(xdoc, world);

            Assert.AreEqual(entitypopulation.Id, 1);
        }

        [TestMethod]
        public void EntityXML_Parses()
        {
            var world = LoadingWorld.GetTestWorld();
            var xdoc = new XDocument(new XElement("entity",
                    new XElement("id", 5)
                    )
                );

            var entity = new Entity(xdoc, world);


            Assert.AreEqual(entity.Id, 5);
        }

        [TestMethod]
        public void HistoricalEvent_ChangeHFStateXML_Parses()
        {
            var world = LoadingWorld.GetTestWorld();
            var xdoc = new XDocument(new XElement("historical_event",
                    new XElement("id", 0),
                    new XElement("year", 1),
                    new XElement("seconds72", -1),
                    new XElement("type", "change hf state"),
                    new XElement("hfid", 264),
                    new XElement("state", "settled"),
                    new XElement("site_id", -1),
                    new XElement("subregion_id", 4),
                    new XElement("feature_layer_id", -1),
                    new XElement("coords", "20,16")
                    )
                );


            var historicalevent = HistoricalEvent.Create(xdoc, world);
            Assert.AreEqual(historicalevent.Id, 0);
            Assert.AreEqual(historicalevent.Year, 1);
            Assert.AreEqual(HistoricalEvent.Types[historicalevent.Type], "change hf state");
            Assert.IsInstanceOfType(historicalevent, typeof (HE_ChangeHFState));
            var changehfstateEvent = historicalevent as HE_ChangeHFState;
            Assert.AreEqual(changehfstateEvent.HfId, 264);
            Assert.AreEqual(HE_ChangeHFState.States[changehfstateEvent.State.Value], "settled");
            Assert.IsNull(changehfstateEvent.SiteId);
            Assert.AreEqual(changehfstateEvent.SubregionId.Value, 4);
            Assert.IsNull(changehfstateEvent.FeatureLayerId);
            Assert.AreEqual(changehfstateEvent.Coords, new Point(20,16));
        }

        [TestMethod]
        public void HistoricalEvent_NewTypeXML_Parses()
        {
            var world = LoadingWorld.GetTestWorld();
            var xdoc = new XDocument(new XElement("historical_event",
                    new XElement("id", 0),
                    new XElement("year", 1),
                    new XElement("seconds72", -1),
                    new XElement("type", "unknown event type")
                    )
                );

            var historicalevent = HistoricalEvent.Create(xdoc, world);
            Assert.AreEqual(historicalevent.Id, 0);
            Assert.AreEqual(historicalevent.Year, 1);
            Assert.IsInstanceOfType(historicalevent, typeof(HE_UnassessedEvent));
        }

        //TODO: Add other Historical Event tests.


        [TestMethod]
        public void HistoricalEventCollection_WarXML_Parses()
        {
            var world = LoadingWorld.GetTestWorld();
            var xdoc = new XDocument(new XElement("historical_event_collection",
                    new XElement("id", 86),
                    new XElement("start_year", 1),
                    new XElement("start_seconds72", 92400),
                    new XElement("end_year", 2),
                    new XElement("end_seconds72", 100800),
                    new XElement("eventcol", 87),
                    new XElement("eventcol", 97),
                    new XElement("event", 1212),
                    new XElement("type", "war"),
                    new XElement("name", "the war of screams"),
                    new XElement("aggressor_ent_id", 84),
                    new XElement("defender_ent_id", 88)
                    )
                );

            var historicalEventCollection = HistoricalEventCollection.Create(xdoc, world);
            Assert.AreEqual(historicalEventCollection.Id, 86);
            Assert.AreEqual(historicalEventCollection.StartTime, new WorldTime(1, 92400));
            Assert.AreEqual(historicalEventCollection.EndTime, new WorldTime(2, 100800));
            Assert.AreEqual(historicalEventCollection.EventIDs.Count, 1);
            Assert.AreEqual(historicalEventCollection.EventIDs[0], 1212);
            Assert.AreEqual(HistoricalEventCollection.Types[historicalEventCollection.Type], "war");
            var warEventCollection = historicalEventCollection as EC_War;
            Assert.AreEqual(warEventCollection.EventColIDs.Count, 2);
            Assert.AreEqual(warEventCollection.EventColIDs[0], 87);
            Assert.AreEqual(warEventCollection.EventColIDs[1], 97);
            Assert.AreEqual(warEventCollection.AggressorEntId, 84);
            Assert.AreEqual(warEventCollection.DefenderEntId, 88);
        }

        [TestMethod]
        public void HistoricalEventCollection_BattleXML_Parses()
        {
            var world = LoadingWorld.GetTestWorld();
            var xdoc = new XDocument(new XElement("historical_event_collection",
                    new XElement("id", 21796),
                    new XElement("start_year", 660),
                    new XElement("start_seconds72", 285600),
                    new XElement("end_year", 660),
                    new XElement("end_seconds72", 285600),
                    new XElement("eventcol", 21797),
                    new XElement("eventcol", 21798),
                    new XElement("event", 180182),
                    new XElement("event", 180183),
                    new XElement("type", "battle"),
                    new XElement("name", "the siege of assault"),
                    new XElement("war_eventcol", 21795),
                    new XElement("subregion_id", -1),
                    new XElement("feature_layer_id", -1),
                    new XElement("site_id", 88),
                    new XElement("coords", "27,16"),
                    new XElement("subregion_id", -1),
                    new XElement("attacking_hfid", 27436),
                    new XElement("attacking_hfid", 27437),
                    new XElement("defending_hfid", 27278),
                    new XElement("defending_hfid", 27284),
                    new XElement("noncom_hfid", 27299),
                    new XElement("attacking_squad_race", "GIANT_RATTLESNAKE"),
                    new XElement("attacking_squad_entity_pop", -1),
                    new XElement("attacking_squad_number", 5),
                    new XElement("attacking_squad_deaths", 0),
                    new XElement("attacking_squad_site", -1),
                    new XElement("defending_squad_race", "CROCODILE_CAVE"),
                    new XElement("defending_squad_entity_pop", -1),
                    new XElement("defending_squad_number", 5),
                    new XElement("defending_squad_deaths", 5),
                    new XElement("defending_squad_site", -1),
                    new XElement("outcome", "defender won")
                    )
                );

            var historicalEventCollection = HistoricalEventCollection.Create(xdoc, world);

            Assert.AreEqual(historicalEventCollection.Id, 21796);
            Assert.AreEqual(historicalEventCollection.StartTime, new WorldTime(660, 285600));
            Assert.AreEqual(historicalEventCollection.EndTime, new WorldTime(660, 285600));
            Assert.AreEqual(historicalEventCollection.EventIDs.Count, 2);
            Assert.AreEqual(historicalEventCollection.EventIDs[0], 180182);
            Assert.AreEqual(historicalEventCollection.EventIDs[1], 180183);
            Assert.AreEqual(HistoricalEventCollection.Types[historicalEventCollection.Type], "battle");
            var battleEventCollection = historicalEventCollection as EC_Battle;
            Assert.AreEqual(battleEventCollection.EventColIDs.Count, 2);
            Assert.AreEqual(battleEventCollection.EventColIDs[0], 21797);
            Assert.AreEqual(battleEventCollection.EventColIDs[1], 21798);
            Assert.AreEqual(battleEventCollection.WarEventColId, 21795);
            Assert.IsNull(battleEventCollection.SubregionId);
            Assert.IsNull(battleEventCollection.FeatureLayerId);
            Assert.AreEqual(battleEventCollection.SiteId, 88);
            Assert.AreEqual(battleEventCollection.Coords, new Point(27,16));
            Assert.AreEqual(battleEventCollection.AttackingHfid.Count, 2);
            Assert.AreEqual(battleEventCollection.AttackingHfid[0], 27436);
            Assert.AreEqual(battleEventCollection.AttackingHfid[1], 27437);
            Assert.AreEqual(battleEventCollection.DefendingHfid.Count, 2);
            Assert.AreEqual(battleEventCollection.DefendingHfid[0], 27278);
            Assert.AreEqual(battleEventCollection.DefendingHfid[1], 27284);
            Assert.AreEqual(battleEventCollection.NonComHfid.Count, 1);
            Assert.AreEqual(battleEventCollection.NonComHfid[0], 27299);
            Assert.AreEqual(battleEventCollection.AttackingSquadRace[0], "GIANT_RATTLESNAKE");
            Assert.AreEqual(battleEventCollection.AttackingSquadEntityPop[0], -1);
            Assert.AreEqual(battleEventCollection.AttackingSquadNumber[0], 5);
            Assert.AreEqual(battleEventCollection.AttackingSquadDeaths[0], 0);
            Assert.AreEqual(battleEventCollection.AttackingSquadSite[0], -1);
            Assert.AreEqual(battleEventCollection.DefendingSquadRace[0], "CROCODILE_CAVE");
            Assert.AreEqual(battleEventCollection.DefendingSquadEntityPop[0], -1);
            Assert.AreEqual(battleEventCollection.DefendingSquadNumber[0], 5);
            Assert.AreEqual(battleEventCollection.DefendingSquadDeaths[0], 5);
            Assert.AreEqual(battleEventCollection.DefendingSquadSite[0], -1);
            Assert.AreEqual(battleEventCollection.Outcome, "defender won");

        }

        [TestMethod]
        public void HistoricalEventCollection_NewTypeXML_Parses()
        {
            var world = LoadingWorld.GetTestWorld();
            var xdoc = new XDocument(new XElement("historical_event_collection",
                    new XElement("id", 30),
                    new XElement("start_year", 45),
                    new XElement("start_seconds72", 285600),
                    new XElement("end_year", 50),
                    new XElement("end_seconds72", 285600),
                    new XElement("type", "unknown new type")
                    )
                );

            var historicalEventCollection = HistoricalEventCollection.Create(xdoc, world);

            Assert.AreEqual(historicalEventCollection.Id, 30);
            Assert.AreEqual(historicalEventCollection.StartTime, new WorldTime(45, 285600));
            Assert.AreEqual(historicalEventCollection.EndTime, new WorldTime(50, 285600));
            Assert.IsInstanceOfType(historicalEventCollection, typeof(EC_UnassessedEventCollection));
        }


        [TestMethod]
        public void HistoricalEraXML_Parses()
        {
            var world = LoadingWorld.GetTestWorld();
            var xdoc = new XDocument(new XElement("historical_era",
                    new XElement("name", "Age of Dragon and Brush Titan"),
                    new XElement("start_year", -1)
                    )
                );

            var historicalEventCollection = new HistoricalEra(xdoc, world);

            Assert.AreEqual(historicalEventCollection.Name, "Age of Dragon and Brush Titan");
            Assert.AreEqual(historicalEventCollection.StartYear, -1);
        }
    }
}
