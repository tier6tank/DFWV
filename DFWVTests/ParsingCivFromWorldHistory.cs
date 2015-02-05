using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DFWV;
using DFWV.WorldClasses;
using DFWV.WorldClasses.HistoricalFigureClasses;


namespace DFWVTests
{
    [TestClass]
    public class ParsingCivFromWorldHistory
    {
        static World TestWorld = new World("", "", "", "", "", "", new WorldTime(1000, 0));

        [TestMethod]
        public void Civ_WithoutRace_Parses()
        {
            // arrange
            var data = new List<string> {"Bat men"};

            // act
            var civ = new Civilization(data, TestWorld);

            // assert
            Assert.AreEqual("Bat men", civ.Name);
        }

        [TestMethod]
        public void Civ_WithOnlyRace_ParsesWithProperRace()
        {
            // arrange
            var data = new List<string> { "The Towers of Quieting, Dwarves" };

            // act
            var civ = new Civilization(data, TestWorld);

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
            var civ = new Civilization(data, TestWorld);

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
            var civ = new Civilization(data, TestWorld);

            // assert
            Assert.AreEqual("The Towers of Quieting", civ.Name);
            Assert.AreEqual("dwarf", civ.Race.Name);
            Assert.AreEqual("dwarves", civ.Race.PluralName);
            Assert.AreEqual("king", civ.Leaders.First().Key);
            Assert.AreEqual("Olon Channelsnarling", civ.Leaders.First().Value[0].Name);
        }
    }
}
