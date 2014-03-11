using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;

namespace DFWV.WorldClasses.HistoricalEventClasses
{
    class HE_FirstContact : HistoricalEvent
    {
        public int? SiteID { get; set; }
        public Site Site { get; set; }
        public int? ContactorEnID { get; set; }
        public Entity ContactorEn { get; set; }
        public int? ContactedEnID { get; set; }
        public Entity ContactedEn { get; set; }

        override public Point Location { get { return Site.Location; } }


        public HE_FirstContact(XDocument xdoc, World world)
            : base(xdoc, world)
        {
            foreach (XElement element in xdoc.Root.Elements())
            {
                string val = element.Value.ToString();
                int valI;
                Int32.TryParse(val, out valI);

                switch (element.Name.LocalName)
                {
                    case "id":
                    case "year":
                    case "seconds72":
                    case "type":
                        break;
                    case "contactor_enid":
                        ContactorEnID = valI;
                        break;
                    case "contacted_enid":
                        ContactedEnID = valI;
                        break;
                    case "site_id":
                        SiteID = valI;
                        break;
                    default:
                        DFXMLParser.UnexpectedXMLElement(xdoc.Root.Name.LocalName + "\t" + HistoricalEvent.Types[Type], element, xdoc.Root.ToString());
                        break;
                }
            }
        }
        internal override void Link()
        {
            base.Link();
            if (SiteID.HasValue && World.Sites.ContainsKey(SiteID.Value))
                Site = World.Sites[SiteID.Value];
            if (ContactorEnID.HasValue && World.Entities.ContainsKey(ContactorEnID.Value))
                ContactorEn = World.Entities[ContactorEnID.Value];
            if (ContactedEnID.HasValue && World.Entities.ContainsKey(ContactedEnID.Value))
                ContactedEn = World.Entities[ContactedEnID.Value];
        }

        internal override void Process()
        {
            base.Process();
            if (ContactorEn != null)
            {
                if (ContactorEn.Events == null)
                    ContactorEn.Events = new List<HistoricalEvent>();
                ContactorEn.Events.Add(this);
            }

            if (ContactedEn != null)
            {
                if (ContactedEn.Events == null)
                    ContactedEn.Events = new List<HistoricalEvent>();
                ContactedEn.Events.Add(this);
            }

        }
        public override void WriteDataOnParent(MainForm frm, Control parent, ref Point location)
        {
            EventLabel(frm, parent, ref location, "Contactor:", ContactorEn);
            EventLabel(frm, parent, ref location, "Contacted:", ContactedEn);
            EventLabel(frm, parent, ref location, "Site:", Site);
        }

        public override string LegendsDescription()
        {
            string timestring = base.LegendsDescription();

            return string.Format("{0} {1} made contact with {2} at {3}.",
                                    timestring, ContactorEn.ToString(), ContactedEn.ToString(), Site.AltName);
        }

        internal override string ToTimelineString()
        {
            string timelinestring = base.ToTimelineString();

            return string.Format("{0} {1} made contact with {2} at {3}.",
                                    timelinestring, ContactorEn.ToString(), ContactedEn.ToString(), Site.AltName);
        }

        internal override void Export(string table)
        {
            base.Export(table);

            
            List<object> vals;
            table = this.GetType().Name.ToString();


            
            vals = new List<object>() { ID, SiteID, ContactorEnID, ContactedEnID };


            Database.ExportWorldItem(table, vals);

        }

    }
}
