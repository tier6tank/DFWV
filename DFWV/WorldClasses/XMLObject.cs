using System;
using System.Windows.Forms;
using System.Xml.Linq;

namespace DFWV.WorldClasses
{
    public abstract class XMLObject : WorldObject, IEquatable<XMLObject>
    {
        public int ID { get; protected set; }
        public int Notability { get; set; }

        protected XMLObject(XDocument xdoc, World world) 
            : base(world)
        {
            ID = Convert.ToInt32(xdoc.Root.Element("id").Value);
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
            return base.ToString() != "" ? base.ToString() : ID.ToString();
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

            return ID == other.ID && GetType() == other.GetType();
        }

        public override int GetHashCode()
        {
            return ID * 7 + GetType().GetHashCode();
        }
    }
}