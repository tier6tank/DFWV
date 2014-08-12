
namespace DFWV.WorldClasses
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Xml.Linq;
    using System.Collections.Specialized;


    abstract class XMLObject : WorldObject
    {
        public int ID { get; set; }


        public XMLObject(XDocument xdoc, World world) 
            : base(world)
        {
            ID = Convert.ToInt32(xdoc.Root.Element("id").Value.ToString());
            World = world;
        }

        public XMLObject(World world) : base(world)
        {
            World = world;
        }

        //public XMLObject(NameValueCollection data, World world)
        //    : base (world)
        //{
        //    ID = Convert.ToInt32(data["ID"]);
        //    World = world;
        //}


        internal abstract void Link();

        internal abstract void Process();
    }
}
