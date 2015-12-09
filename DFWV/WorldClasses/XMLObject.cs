using System;
using System.Xml.Linq;

namespace DFWV.WorldClasses
{
    public abstract class XmlObject : WorldObject, IEquatable<XmlObject>
    {
        public int Id { get; set; }
        public int Notability { get; set; }

        protected XmlObject(XDocument xdoc, World world) 
            : base(world)
        {
            Id = Convert.ToInt32(xdoc.Root.Element("id").Value);
            World = world;
        }

        protected XmlObject(World world) : base(world)
        {
            
        }

        internal abstract void Link();

        internal abstract void Process();

        internal abstract void Plus(XDocument xdoc);

        public override string ToString()
        {
            return base.ToString() != "" ? base.ToString() : Id.ToString();
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as XmlObject);
        }

        public bool Equals(XmlObject other)
        {
            // Check for null
            if (ReferenceEquals(other, null))
                return false;

            // Check for same reference
            if (ReferenceEquals(this, other))
                return true;

            return Id == other.Id && GetType() == other.GetType();
        }

        public override int GetHashCode()
        {
            return Id * 7 + GetType().GetHashCode();
        }
    }
}