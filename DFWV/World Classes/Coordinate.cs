using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace DFWV.WorldClasses
{
    class Coordinate : WorldObject
    {
        int X;
        int Y;

        override public Point Location { get { return new Point(X,Y); } }

        public Coordinate(string coord)
        {
            X = Convert.ToInt32(coord.Split(',')[0]);
            Y = Convert.ToInt32(coord.Split(',')[1]);
            World = Program.mainForm.World;
        }
        public Coordinate(Point coord)
        {
            X = coord.X;
            Y = coord.Y;
            World = Program.mainForm.World;
        }

        public override string ToString()
        {
            return "(" + X + ", " + Y + ")";
        }

        public override void Select(MainForm frm)
        {
            base.Select(frm);
            if (Program.mapForm == null || Program.mapForm.IsDisposed)
                Program.mapForm = new MapForm(World);
            if (!Program.mapForm.Visible)
                Program.mapForm.Location = this.Location;
            Program.mapForm.Show();

            Program.mapForm.Select(new Point(X,Y));
        }

        internal override void Export(string table)
        {

        }
    }
}
