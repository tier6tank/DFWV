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
    public class LoadingWorld
    {
        public static World GetTestWorld()
        {
            return new World("", "", "", "", "", "", new WorldTime(1000, 0));
        }

    }
}
