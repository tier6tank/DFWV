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
    class HE_AddHFEntityLink : HistoricalEvent
    {
        public int? CivID { get; set; }
        public Entity Civ { get; set; }
        public HistoricalFigure HF { get; set; }
        public string LinkType { get; set; }

        override public Point Location { get { return Civ.Location; } }


        public HE_AddHFEntityLink(XDocument xdoc, World world)
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
                    case "civ_id":
                        CivID = valI;
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
            if (CivID.HasValue && World.Entities.ContainsKey(CivID.Value))
                Civ = World.Entities[CivID.Value];
        }

        internal override void Process()
        {
            base.Process();

            if (Civ != null)
            {
                if (Civ.Events == null)
                    Civ.Events = new List<HistoricalEvent>();
                Civ.Events.Add(this);
            }

        }


        public override void WriteDataOnParent(MainForm frm, Control parent, ref Point location)
        {
            EventLabel(frm, parent, ref location, "HF:", HF);
            EventLabel(frm, parent, ref location, "Entity:", Civ);
            EventLabel(frm, parent, ref location, "Link Type:", LinkType);

        }

        public override string LegendsDescription()
        {
            string timestring = base.LegendsDescription();

            return string.Format("{0} {1} became {2} of {3}.",
                            timestring, HF == null ? "UNKNOWN" : HF.ToString(),
                            LinkType == null ? "UNKNOWN" : LinkType, Civ.ToString());
        }

        internal override string ToTimelineString()
        {
            string timelinestring = base.ToTimelineString();

            if (HF != null && LinkType != null)
                return string.Format("{0} {1} became {2} of {3}.",
                            timelinestring, HF.ToString(),
                                LinkType, Civ.ToString());
            else
                return string.Format("{0} Added HF Link to {1}.",
                             timelinestring, Civ.ToString());
        }


        internal override void Export(string table)
        {
            base.Export(table);

            
            List<object> vals;
            table = this.GetType().Name.ToString();

            vals = new List<object>() { ID, HF, CivID, LinkType};


            Database.ExportWorldItem(table, vals);

        }


    }
}
