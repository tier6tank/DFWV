using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;

namespace DFWV.WorldClasses.HistoricalEventClasses
{
    class HE_ItemStolen : HistoricalEvent
    {
        public int? AttackerCivID { get; set; }
        public Entity AttackerCiv { get; set; }
        public int? DefenderCivID { get; set; }
        public Entity DefenderCiv { get; set; }
        public int? SiteID { get; set; }
        public Site Site { get; set; }
        public Point Coords { get; set; }

        override public Point Location { get { return Site != null ? Site.Location : Coords; } }


        public HE_ItemStolen(XDocument xdoc, World world)
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
                    default:
                        DFXMLParser.UnexpectedXMLElement(xdoc.Root.Name.LocalName + "\t" + HistoricalEvent.Types[Type], element, xdoc.Root.ToString());
                        break;
                }
            }
        }

        internal override void Link()
        {
            base.Link();
        }

        internal override void Process()
        {
            base.Process();
            if (AttackerCiv != null)
            {
                if (AttackerCiv.Events == null)
                    AttackerCiv.Events = new List<HistoricalEvent>();
                AttackerCiv.Events.Add(this);
            }


            if (DefenderCiv != null)
            {
                if (AttackerCiv.Events == null)
                    AttackerCiv.Events = new List<HistoricalEvent>();
                AttackerCiv.Events.Add(this);
            }
        }

        public override void WriteDataOnParent(MainForm frm, Control parent, ref Point location)
        {
            EventLabel(frm, parent, ref location, "Victim:", DefenderCiv);
            EventLabel(frm, parent, ref location, "Theif:", AttackerCiv);
            EventLabel(frm, parent, ref location, "Site:", Site);
            EventLabel(frm, parent, ref location, "Coords:", new Coordinate(Coords));
        }

        public override string LegendsDescription()
        {
            string timestring = base.LegendsDescription();

            return string.Format("{0} {1} was stolen from {2} in {3} by the {4} {5} and brought to {6}",
                    timestring, "UNKNOWN", "UNKNOWN", 
                    Site == null ? "UNKNOWN" : Site.ToString(), "UNKNOWN", "UNKNOWN", "UNKNOWN");

        }

        internal override string ToTimelineString()
        {
            string timelinestring = base.ToTimelineString();
            
            if (Site == null)
                return string.Format("{0} Item stolen.",
                        timelinestring);
            else
                return string.Format("{0} Item stolen from {1}",
                        timelinestring, Site.AltName);

        }

        internal override void Export(string table)
        {
            base.Export(table);

            
            List<object> vals;
            table = this.GetType().Name.ToString();


            
            vals = new List<object>() { ID, AttackerCivID, DefenderCivID, SiteID };

            if (Coords.IsEmpty)
                vals.Add(DBNull.Value);
            else
                vals.Add(Coords.X + "," + Coords.Y);

            Database.ExportWorldItem(table, vals);

        }

    }
}

