using DFWV.WorldClasses.HistoricalEventClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Drawing;
using System.Collections.Specialized;

namespace DFWV.WorldClasses
{
    class WorldConstruction: XMLObject
    {

        public List<WorldConstruction> Subconstructions { get; set; }
        public WorldConstruction MasterWC { get; set; }
        public HE_CreatedWorldConstruction CreatedEvent { get; set; }

        public Site From { get; set; }
        public Site To { get; set; }

        public string DispNameLower { get { return ToString().ToLower(); } }

        override public Point Location { get { return From.Location; } }

        public WorldConstruction(XDocument xdoc, World world)
            : base(xdoc, world)
        {
            foreach (XElement element in xdoc.Root.Elements())
            {
                string val = element.Value.ToString();
                switch (element.Name.LocalName)
                {
                    case "id":
                        break;
                    default:
                        DFXMLParser.UnexpectedXMLElement(xdoc.Root.Name.LocalName, element, xdoc.Root.ToString());
                        break;
                }
            }
        }

        //public WorldConstruction(NameValueCollection data, World world) 
        //    : base (world)
        //{
            
        //}


        public WorldConstruction(int id, World world) : base(world) //Created from Historical Events if World Construction List is empty.
        {
            ID = id;
            World = world;
            Name = id.ToString();
        }

        public override string ToString()
        {
            if (base.ToString() == null)
                return "";
            else
                return base.ToString();
        }

        public override void Select(MainForm frm)
        {
            frm.grpWorldConstruction.Text = this.ToString();
            frm.grpWorldConstruction.Show();

            frm.lblWorldConstructionMaster.Data = MasterWC;
            frm.lblWorldConstructionFrom.Data = From;
            frm.lblWorldConstructionTo.Data = To;

            frm.grpWorldConstruction.Visible = CreatedEvent != null;
            if (CreatedEvent != null)
            {
                frm.lblWorldConstructionCreatedBy.Data = CreatedEvent.SiteCiv;
                frm.lblWorldConstructionCreatedByCiv.Data = CreatedEvent.Civ;
                frm.lblWorldConstructionCreatedTime.Data = CreatedEvent;
                frm.lblWorldConstructionCreatedTime.Text = CreatedEvent.Time.ToString();
            }
            Program.MakeSelected(frm.tabWorldConstruction, frm.lstWorldConstruction, this);

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

            if (Name == null)
                vals.Add(DBNull.Value);
            else
                vals.Add(Name.Replace("'", "''"));

            Database.ExportWorldItem(table, vals);
        }
    }
}
