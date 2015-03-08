using System.Collections.Generic;
using System.Linq;
using DFWV.WorldClasses;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DFWVTests
{
    [TestClass]
    public class ParsingSitesFile
    {

        [TestMethod]
        public void Site_IsCave_Parses()
        {
            // arrange
            var data = new List<string>
            {
                "1: Gujomomsos, \"The Sour Umbra\", cave",
                "\t4 elves"
            };

            // act
            var site = new Site(data, LoadingWorld.GetTestWorld());

            // assert
            Assert.AreEqual("Gujomomsos", site.Name);
            Assert.AreEqual("The Sour Umbra", site.AltName);
            Assert.AreEqual("cave", Site.Types[site.Type.Value]);
            Assert.AreEqual("elf", site.Population.First().Key.Name);
            Assert.AreEqual("elves", site.Population.First().Key.PluralName);
            Assert.AreEqual(4, site.Population.First().Value);
        }

        [TestMethod]
        public void Site_HasOwnerParentCiv_Parses()
        {
            // arrange
            var data = new List<string>
            {
                "76: Zanegamal, \"Relicteach\", fortress",
                "\tOwner: The Even Fountains, dwarves",
                "\tParent Civ: The Stirred Gloves, dwarves",
                "\t203 dwarves"
            };

            // act
            var site = new Site(data, LoadingWorld.GetTestWorld());

            // assert
            Assert.AreEqual("Zanegamal", site.Name);
            Assert.AreEqual("Relicteach", site.AltName);
            Assert.AreEqual("fortress", Site.Types[site.Type.Value]);
            Assert.AreEqual("dwarf", site.Population.First().Key.Name);
            Assert.AreEqual("dwarves", site.Population.First().Key.PluralName);
            Assert.AreEqual(203, site.Population.First().Value);
            Assert.AreEqual("The Even Fountains", site.Owner.Name);
            Assert.AreEqual("dwarf", site.Owner.Race.Name);
            Assert.IsNull(site.Parent);
            //Assert.AreEqual("The Stirred Gloves", site.Parent.Name); //TODO: Site with new civ should create civ?
            //Assert.AreEqual("dwarf", site.Parent.Race.Name);
        }

        [TestMethod]
        public void Site_HasLeader_Parses()
        {
            // arrange
            var data = new List<string>
            {
                "78: Azstrogsat, \"Devilsabre\", dark fortress",
                "\tOwner: The Foolish Flies, goblins",
                "\tParent Civ: The Hell of Rifts, goblins",
                "\tlady: Stosbub Malignedboulders, goblin",
                "\t76 goblins"
            };

            // act
            var site = new Site(data, LoadingWorld.GetTestWorld());

            // assert
            Assert.AreEqual("Azstrogsat", site.Name);
            Assert.AreEqual("Devilsabre", site.AltName);
            Assert.AreEqual("dark fortress", Site.Types[site.Type.Value]);
            Assert.AreEqual("goblin", site.Population.First().Key.Name);
            Assert.AreEqual("goblins", site.Population.First().Key.PluralName);
            Assert.AreEqual(76, site.Population.First().Value);
            Assert.AreEqual("The Foolish Flies", site.Owner.Name);
            Assert.AreEqual("goblin", site.Owner.Race.Name);
            Assert.IsNull(site.Parent);
            //Assert.AreEqual("The Hell of Rifts", site.Parent.Name); //TODO: Site with new civ should create civ?
            //Assert.AreEqual("goblin", site.Parent.Race.Name);
            Assert.AreEqual("lady", Leader.LeaderTypes[site.Leaders[0].LeaderType]);
            Assert.AreEqual("Stosbub Malignedboulders", site.Leaders[0].Name);
            Assert.AreEqual("goblin", site.Leaders[0].Race.Name);
        }
    }
}
