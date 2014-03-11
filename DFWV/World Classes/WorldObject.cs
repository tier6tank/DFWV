namespace DFWV.WorldClasses
{
    using System.Drawing;
    using System.Globalization;

    public abstract class WorldObject
    {
        public string Name { get; set; }
        internal World World { get; set; }

        abstract public Point Location { get; }

        public WorldObject()
        {

        }

        internal WorldObject(World world)
        {
            World = world;
        }

        public override string ToString()
        {
            if (Name == null)
                return Name;
            TextInfo ti = CultureInfo.CurrentCulture.TextInfo;
            return ti.ToTitleCase(Name);
        }

        public virtual void Select(MainForm frm)
        {
    
        }

        internal abstract void Export(string table);

    }
}
