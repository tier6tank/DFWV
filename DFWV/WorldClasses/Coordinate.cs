using System.Drawing;

namespace DFWV.WorldClasses
{
    class Coordinate : WorldObject
    {
        readonly int _x;
        readonly int _y;

        override public Point Location => new Point(_x,_y);

        public Coordinate(Point coord)
        {
            _x = coord.X;
            _y = coord.Y;
            World = Program.MainForm.World;
        }

        public override string ToString()
        {
            return "(" + _x + ", " + _y + ")";
        }

        public override void Select(MainForm frm)
        {
            base.Select(frm);
            if (Program.MapForm == null || Program.MapForm.IsDisposed)
                Program.MapForm = new MapForm(World);
            if (!Program.MapForm.Visible)
                Program.MapForm.Location = Location;
            Program.MapForm.Show();

            Program.MapForm.Select(new Point(_x,_y));
        }

        internal override void Export(string table)
        {

        }
    }
}