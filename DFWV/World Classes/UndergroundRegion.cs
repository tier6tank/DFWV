using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Drawing;
using System.Collections.Specialized;

namespace DFWV.WorldClasses
{
    class UndergroundRegion : XMLObject
    {
        public string Type { get; set; }
        public int Depth { get; set; }

        public string DispNameLower { get { return ToString().ToLower(); } }

        override public Point Location { get { return Point.Empty; } }

        public UndergroundRegion(XDocument xdoc, World world) 
            : base(xdoc, world)
        {
            foreach (XElement element in xdoc.Root.Elements())
            {
                string val = element.Value.ToString();
                switch (element.Name.LocalName)
                {
                    case "id":
                        break;
                    case "type":
                        Type = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(val);
                        break;
                    case "depth":
                        Depth = Convert.ToInt32(val);
                        break;
                    default:
                        DFXMLParser.UnexpectedXMLElement(xdoc.Root.Name.LocalName, element, xdoc.Root.ToString());
                        break;
                }
            }
        }

        //public UndergroundRegion(NameValueCollection data, World world) 
        //    : base (world)
        //{
        //    Depth = Convert.ToInt32(data["Depth"]);
        //    Type = data["Type"].ToString();
        //}

        public override string ToString()
        {
            return Type;
        }

        public override void Select(MainForm frm)
        {
            frm.grpUndergroundRegion.Text = this.ToString();
            frm.grpUndergroundRegion.Show();

            frm.lblUndergroundRegionDepth.Text = Depth.ToString();
            frm.lblUndergroundRegionType.Text = Type;

            Program.MakeSelected(frm.tabUndergroundRegion, frm.lstUndergroundRegion, this);
        }

        internal override void Link()
        {

        }

        internal override void Process()
        {

        }

        internal override void Export(string table)
        {

            List<object> vals = new List<object>();

            vals.Add(ID);
            vals.Add(Type);
            vals.Add(Depth);

            Database.ExportWorldItem(table, vals);
        }
    }
}

