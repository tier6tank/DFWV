using System;
using System.Xml.Linq;

namespace DFWV.WorldClasses
{
    public abstract class XMLObject : WorldObject, IEquatable<XMLObject>
    {
        public int Id { get; }
        public int Notability { get; set; }

        protected XMLObject(XDocument xdoc, World world) 
            : base(world)
        {
            Id = Convert.ToInt32(xdoc.Root.Element("id").Value);
            World = world;
        }

        protected XMLObject(World world) : base(world)
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
            return Equals(obj as XMLObject);
        }

        public bool Equals(XMLObject other)
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