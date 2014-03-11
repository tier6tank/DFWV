using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace DFWV.WorldClasses
{
    class Person : WorldObject
    {
        override public Point Location { get { return Point.Empty; } }

        internal override void Export(string table)
        {

        }
    }
}
