using System.Collections.Generic;
using System.Linq;
using DFWV.WorldClasses;
using DFWV.WorldClasses.HistoricalFigureClasses;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DFWVTests
{
    [TestClass]
    public class ParsingWorldHistoryFile
    {
        public static Civilization GetTestCivilization()
        {
            var data = new List<string> { "Bat men" };
            return new Civilization(data, LoadingWorld.GetTestWorld());
        }

        [TestMethod]
        public void Civ_WithoutRace_Parses()
        {
            // arrange
            var civ = GetTestCivilization();
            // act
            

            // assert
            Assert.AreEqual("Bat men", civ.Name);
        }

        [TestMethod]
        public void Civ_WithOnlyRace_Parses()
        {
            // arrange
            var data = new List<string> { "The Towers of Quieting, Dwarves" };

            // act
            var civ = new Civilization(data, LoadingWorld.GetTestWorld());

            // assert
            Assert.AreEqual("The Towers of Quieting", civ.Name);
            Assert.AreEqual("dwarf", civ.Race.Name);
            Assert.AreEqual("dwarves", civ.Race.PluralName);
        }

        [TestMethod]
        public void Civ_WithWorshipList_Parses()
        {
            // arrange
            var data = new List<string>
            {
                "The Towers of Quieting, Dwarves",
                " Worship List",
                "  Atir, deity: wealth"
            };

            // act
            var civ = new Civilization(data, LoadingWorld.GetTestWorld());

            // assert
            Assert.AreEqual("The Towers of Quieting", civ.Name);
            Assert.AreEqual("dwarf", civ.Race.Name);
            Assert.AreEqual("dwarves", civ.Race.PluralName);
            Assert.AreEqual("Atir", civ.Gods[0].Name);
            Assert.AreEqual("deity", civ.Gods[0].GodType);
            Assert.AreEqual("wealth", HistoricalFigure.Spheres[civ.Gods[0].Spheres[0]]);
        }

        [TestMethod]
        public void Civ_WithLeaderList_Parses()
        {
            // arrange
            var data = new List<string>
            {
                "The Towers of Quieting, Dwarves",
                " king List",
                "  [*] Olon Channelsnarling (b.??? d. 5, Reign Began: 1), *** Original Line, Never Married",
                "      No Children"
            };

            // act
            var civ = new Civilization(data, LoadingWorld.GetTestWorld());

            // assert
            Assert.AreEqual("The Towers of Quieting", civ.Name);
            Assert.AreEqual("dwarf", civ.Race.Name);
            Assert.AreEqual("dwarves", civ.Race.PluralName);
            Assert.AreEqual("king", civ.Leaders.First().Key);
            Assert.AreEqual("Olon Channelsnarling", civ.Leaders.First().Value[0].Name);
        }

        [TestMethod]
        public void Leader_WithMinimalData_Parses()
        {
            // arrange
            var data = new List<string>
            {
                "  [*] Atho (b.???, Reign Began: 1), *** Original Line, Never Married",
                "      No Children"
            };
            const string leaderType = "master";

            // act
            var leader = new Leader(data, leaderType, GetTestCivilization());

            // assert
            Assert.AreEqual("Atho", leader.Name);
            Assert.IsNull(leader.Birth);
            Assert.AreEqual(WorldTime.Present.Year, leader.Death.Year);
            Assert.AreEqual(1, leader.ReignBegan.Year);
            Assert.AreEqual("Original Line", Leader.InheritanceTypes[leader.Inheritance]);
            Assert.IsFalse(leader.Married);
            Assert.AreEqual(0, leader.ChildrenCount);
        }

        [TestMethod]
        public void Leader_WithMaximumData_Parses()
        {
            // arrange
            var data = new List<string>
            {
                "  [*] Onol Flankgranite the Silt of Turquoise (b.188 d. 340, Reign Began: 329), Inherited from mother, Married (d. 212)",
                "      3 Children (out-lived 1 of them) -- Ages at death: 130 129 (d. 224)",
                "      Worshipped Elana Viperwashed (45%)"
            };
            var leaderType = "master";

            // act
            var leader = new Leader(data, leaderType, GetTestCivilization());

            // assert
            Assert.AreEqual("Onol Flankgranite the Silt of Turquoise", leader.Name);
            Assert.AreEqual(188, leader.Birth.Year);
            Assert.AreEqual(340, leader.Death.Year);
            Assert.AreEqual(329, leader.ReignBegan.Year);
            Assert.AreEqual("Inherited", Leader.InheritanceTypes[leader.Inheritance]);
            Assert.AreEqual(Leader.InheritanceSource.Mother, leader.InheritedFromSource);
            Assert.IsTrue(leader.Married);
            Assert.AreEqual(3, leader.ChildrenCount);
            Assert.AreEqual(130, leader.Children.First().AgeAtParentDeath);
            Assert.AreEqual(224, leader.Children.Last().Death.Year);
        }

        [TestMethod]
        public void God_WithMultipleSpheres_Parses()
        {
            // arrange
            var data = "  Risen Crystalcanyons the Oily Boulders, deity: mountains, earth";

            // act
            var god = new God(data);

            // assert
            Assert.AreEqual("Risen Crystalcanyons the Oily Boulders", god.Name);
            Assert.AreEqual("deity", god.GodType);
            Assert.AreEqual("mountains", HistoricalFigure.Spheres[god.Spheres[0]]);
            Assert.AreEqual("earth", HistoricalFigure.Spheres[god.Spheres[1]]);
        }
    }
}
