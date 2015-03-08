using DFWV.WorldClasses;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DFWVTests
{
    [TestClass]
    public class LoadingWorld
    {
        public static World GetTestWorld()
        {
            return new World("", "", "", "", "", "", new WorldTime(1000, 0));
        }

    }
}
