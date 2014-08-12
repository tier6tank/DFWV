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
    class HE_AssumeIdentity : HistoricalEvent
    {
        public int? TricksterHFID { get; set; }
        public HistoricalFigure TricksterHF { get; set; }
        public int? TargetEnID { get; set; }
        public Entity TargetEn { get; set; }
        public int? IdentityID { get; set; }
        public HistoricalFigure Identity { get; set; }

        override public Point Location { get { return TargetEn.Location; } }

        public HE_AssumeIdentity(XDocument xdoc, World world)
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
                    case "trickster_hfid":
                        TricksterHFID = valI;
                        break;
                    case "identity_id":
                        IdentityID = valI;
                        break;
                    case "target_enid":
                        TargetEnID = valI;
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
            if (TricksterHFID.HasValue && World.HistoricalFigures.ContainsKey(TricksterHFID.Value))
                TricksterHF = World.HistoricalFigures[TricksterHFID.Value];
            //if (IdentityID.HasValue && World.HistoricalFigures.ContainsKey(IdentityID.Value))
            //    Identity = World.HistoricalFigures[IdentityID.Value];
            if (TargetEnID.HasValue && World.Entities.ContainsKey(TargetEnID.Value))
                TargetEn = World.Entities[TargetEnID.Value];
        }


        internal override void Process()
        {
            base.Process();
            if (TricksterHF != null)
            {
                if (TricksterHF.Events == null)
                    TricksterHF.Events = new List<HistoricalEvent>();
                TricksterHF.Events.Add(this);
            }

            if (TargetEn != null)
            {
                if (TargetEn.Events == null)
                    TargetEn.Events = new List<HistoricalEvent>();
                TargetEn.Events.Add(this);
            }

        }
        public override void WriteDataOnParent(MainForm frm, Control parent, ref Point location)
        {
            EventLabel(frm, parent, ref location, "Trickster:", TricksterHF);
            EventLabel(frm, parent, ref location, "Identity ID:", IdentityID.ToString());
            EventLabel(frm, parent, ref location, "Target Ent:", TargetEn);
        }

        public override string LegendsDescription()
        {
            string timestring = base.LegendsDescription();

            return string.Format("{0} {1} {2} fooled {3} into believing it was {4}.",
                            timestring, TricksterHF.Race.ToString(), TricksterHF.ToString(),
                            TargetEn.ToString(), "UNKNOWN");
        }

        internal override string ToTimelineString()
        {
            string timelinestring = base.ToTimelineString();

            return string.Format("{0} {1} assumed an identity to {2}",
                        timelinestring, TricksterHF.ToString(),
                            TargetEn.ToString());
        }

        internal override void Export(string table)
        {
            base.Export(table);

            
            List<object> vals;
            table = this.GetType().Name.ToString();

            vals = new List<object>() { ID, TricksterHFID, TargetEnID, IdentityID };


            Database.ExportWorldItem(table, vals);

        }

    }
}


