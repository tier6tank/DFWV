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
    class HE_AddHFHFLink : HistoricalEvent
    {
        public int? HFID { get; set; }
        public HistoricalFigure HF { get; set; }
        public int? HFIDTarget { get; set; }
        public HistoricalFigure HFTarget { get; set; }

        override public Point Location { get { return Point.Empty; } }

        public HE_AddHFHFLink(XDocument xdoc, World world)
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
                    case "hfid":
                        HFID = valI;
                        break;
                    case "hfid_target":
                        HFIDTarget = valI;
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
            if (HFID.HasValue && World.HistoricalFigures.ContainsKey(HFID.Value))
                HF = World.HistoricalFigures[HFID.Value];
            if (HFIDTarget.HasValue && World.HistoricalFigures.ContainsKey(HFIDTarget.Value))
                HFTarget = World.HistoricalFigures[HFIDTarget.Value];
        }

        internal override void Process()
        {
            base.Process();
            bool matched = false;
            if (HF != null && HF.HFLinks != null)
            {
                foreach (var hfLinkList in HF.HFLinks)
                {
                    foreach (HFLink hflink in hfLinkList.Value)
                    {
                        if (hflink.HF == HFTarget)
                        {
                            hflink.Event = this;
                            matched = true;
                            break;
                        }
                    }
                    if (matched)
                        break;
                }
            }
            matched = false;
            if (HFTarget != null && HFTarget.HFLinks != null)
            {
                foreach (var hfLinkList in HFTarget.HFLinks)
                {
                    foreach (HFLink hflink in hfLinkList.Value)
                    {
                        if (hflink.HF == HF)
                        {
                            hflink.Event = this;
                            matched = true;
                            break;
                        }
                    }
                    if (matched)
                        break;
                }
            }

            if (HF != null)
            {
                if (HF.Events == null)
                    HF.Events = new List<HistoricalEvent>();
                HF.Events.Add(this);
            }


            if (HFTarget != null)
            {
                if (HFTarget.Events == null)
                    HFTarget.Events = new List<HistoricalEvent>();
                HFTarget.Events.Add(this);
            }
        }

        public override void WriteDataOnParent(MainForm frm, Control parent, ref Point location)
        {
            EventLabel(frm, parent, ref location, "HF:", HF);
            EventLabel(frm, parent, ref location, "Target:", HFTarget);
        }

        public override string LegendsDescription()
        {
            string timestring = base.LegendsDescription();

            return string.Format("{0} {1} {2} {3}.",
                                    timestring, HF.ToString(), "UNKNOWN", HFTarget.ToString());
        }

        internal override string ToTimelineString()
        {
            string timelinestring = base.ToTimelineString();

            return string.Format("{0} {1} {2} {3}.",
                timelinestring, HF != null ? HF.ToString() : HFID.ToString(), " linked to ", HFTarget != null ? HFTarget.ToString() : HFIDTarget.ToString());
        }

        internal override void Export(string table)
        {
            base.Export(table);

            
            List<object> vals;
            table = this.GetType().Name.ToString();

            vals = new List<object>() { ID, HFID, HFIDTarget };


            Database.ExportWorldItem(table, vals);

        }

    }
}
