using System.Drawing;

namespace DFWV.WorldClasses
{
    public class Person : WorldObject
    {
        override public Point Location => Point.Empty;

        internal override void Export(string table)
        {

        }
    }
}