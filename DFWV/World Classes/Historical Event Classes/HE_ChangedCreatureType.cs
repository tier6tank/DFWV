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
    class HE_ChangedCreatureType : HistoricalEvent
    {
        public int? ChangeeHFID { get; set; }
        public HistoricalFigure ChangeeHF { get; set; }
        public int? ChangerHFID { get; set; }
        public HistoricalFigure ChangerHF { get; set; }
        public string OldRace_ { get; set; }
        public Race OldRace { get; set; }
        public int OldCaste { get; set; }
        public string NewRace_ { get; set; }
        public Race NewRace { get; set; }
        public int NewCaste { get; set; }

        override public Point Location { get { return Point.Empty ; } }

        public HE_ChangedCreatureType(XDocument xdoc, World world)
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
                    case "changee_hfid":
                        ChangeeHFID = valI;
                        break;
                    case "changer_hfid":
                        ChangerHFID = valI;
                        break;
                    case "old_race":
                        OldRace_ = val;
                        break;
                    case "old_caste":
                        if (!HistoricalFigure.Castes.Contains(val))
                            HistoricalFigure.Castes.Add(val);
                        OldCaste = HistoricalFigure.Castes.IndexOf(val);
                        break;
                    case "new_race":
                        NewRace_ = val;
                        break;
                    case "new_caste":
                        if (!HistoricalFigure.Castes.Contains(val))
                            HistoricalFigure.Castes.Add(val);
                        NewCaste = HistoricalFigure.Castes.IndexOf(val);
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
            if (ChangeeHFID.HasValue && World.HistoricalFigures.ContainsKey(ChangeeHFID.Value))
                ChangeeHF = World.HistoricalFigures[ChangeeHFID.Value];
            if (ChangerHFID.HasValue && World.HistoricalFigures.ContainsKey(ChangerHFID.Value))
                ChangerHF = World.HistoricalFigures[ChangerHFID.Value];
            if (OldRace_ != null)
            {
                OldRace = World.GetAddRace(OldRace_);
                OldRace_ = null;
            }
            if (NewRace_ != null)
            {
                NewRace = World.GetAddRace(NewRace_);
                NewRace_ = null;
            }
        }

        internal override void Process()
        {
            base.Process();
            if (ChangeeHF != null)
            {
                if (ChangeeHF.Events == null)
                    ChangeeHF.Events = new List<HistoricalEvent>();
                ChangeeHF.Events.Add(this);
            }
            if (ChangerHF != null)
            {
                if (ChangerHF.Events == null)
                    ChangerHF.Events = new List<HistoricalEvent>();
                ChangerHF.Events.Add(this);
            }
        }

        public override void WriteDataOnParent(MainForm frm, Control parent, ref Point location)
        {
            EventLabel(frm, parent, ref location, "Changer:", ChangerHF);
            EventLabel(frm, parent, ref location, "Changee:", ChangeeHF);
            EventLabel(frm, parent, ref location, "Old Race:", OldRace);
            EventLabel(frm, parent, ref location, "Old Caste:", HistoricalFigure.Castes[OldCaste]);
            EventLabel(frm, parent, ref location, "New Race:", NewRace);
            EventLabel(frm, parent, ref location, "New Caste:", HistoricalFigure.Castes[NewCaste]);

        }

        public override string LegendsDescription()
        {
            string timestring = base.LegendsDescription();

            return string.Format("{0} {1} {2} changed the {3} {4} from {5} into {6}.",
                                    timestring, ChangerHF.Race.ToString(), ChangerHF.ToString(),
                                    ChangeeHF.Race.ToString(), ChangeeHF.ToString(), OldRace.ToString(),
                                    NewRace.ToString());
        }

        internal override string ToTimelineString()
        {
            string timelinestring = base.ToTimelineString();

            return string.Format("{0} {1} transformed {2}.",
                        timelinestring, ChangerHF.ToString(),
                                    ChangeeHF.ToString());
        }

        internal override void Export(string table)
        {
            base.Export(table);

            
            List<object> vals;
            table = this.GetType().Name.ToString();

            vals = new List<object>() { ID, ChangeeHFID, ChangerHFID, OldRace.ToString(), HistoricalFigure.Castes[OldCaste], NewRace.ToString(), HistoricalFigure.Castes[NewCaste] };


            Database.ExportWorldItem(table, vals);

        }

    }
}


