using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;
using DFWV.WorldClasses.HistoricalFigureClasses;

namespace DFWV.WorldClasses.HistoricalEventClasses
{
    class HE_AddHFSiteLink : HistoricalEvent
    {
        public int? SiteID { get; set; }
        public Site Site { get; set; }
        public HistoricalFigure HF { get; set; }
        public string LinkType { get; set; }

        override public Point Location { get { return Site.Location; } }

        public HE_AddHFSiteLink(XDocument xdoc, World world)
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
        }

        public override void WriteDataOnParent(MainForm frm, Control parent, ref Point location)
        {
             EventLabel(frm, parent, ref location, "Site:", Site);
        }

        public override string LegendsDescription()
        {
            string timestring = base.LegendsDescription();

            return string.Format("{0} {1} became {2} of {3}.",
                            timestring, HF == null ? "UNKNOWN" : HF.ToString(),
                            LinkType == null ? "UNKNOWN" : LinkType, Site.AltName);
        }

        internal override string ToTimelineString()
        {
            string timelinestring = base.ToTimelineString();

            if (HF != null && LinkType != null)
                return string.Format("{0} {1} became {2} of {3}.",
                            timelinestring, HF.ToString(),
                                LinkType, Site.AltName);
            else
                return string.Format("{0} Added Site Link to {1}.",
                             timelinestring, Site.AltName);
        }

        internal override void Export(string table)
        {
            base.Export(table);

            
            List<object> vals;
            table = this.GetType().Name.ToString();


            vals = new List<object>() { ID, SiteID };


            Database.ExportWorldItem(table, vals);

        }

    }
}
