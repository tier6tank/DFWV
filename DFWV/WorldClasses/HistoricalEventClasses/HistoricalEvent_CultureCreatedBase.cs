using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Linq;
using DFWV.WorldClasses.EntityClasses;
using DFWV.WorldClasses.HistoricalFigureClasses;

namespace DFWV.WorldClasses.HistoricalEventClasses
{
    class HistoricalEvent_CultureCreatedBase : HistoricalEvent
    {
        internal int? HistFigureID { get; set; }
        internal HistoricalFigure HistFigure { get; set; }
        internal int? SiteID { get; set; }
        internal Site Site { get; set; }

        override public Point Location => Point.Empty;

        public int? FormID { get; set; }
        public int? ReasonID { get; set; }
        public int? Reason { get; internal set; }
        public static List<string> Reasons = new List<string>();
        public int? CircumstanceID { get; set; }
        public int? Circumstance { get; internal set; }
        public static List<string> Circumstances = new List<string>();

        public override IEnumerable<HistoricalFigure> HFsInvolved
        {
            get { yield return HistFigure; }
        }
        public override IEnumerable<Site> SitesInvolved
        {
            get { yield return Site; }
        }

        public HistoricalEvent_CultureCreatedBase(XDocument xdoc, World world)
            : base(xdoc, world)
        {
            foreach (var element in xdoc.Root.Elements())
            {
                var val = element.Value;
                int valI;
                int.TryParse(val, out valI);

                switch (element.Name.LocalName)
                {
                    case "id":
                    case "year":
                    case "seconds72":
                    case "type":
                        break;
                    case "hist_figure_id":
                        HistFigureID = valI;
                        break;
                    case "reason":
                        if (!Reasons.Contains(val))
                            Reasons.Add(val);
                        Reason = Reasons.IndexOf(val);
                        break;
                    case "reason_id":
                        if (valI != -1)
                            ReasonID = valI;
                        break;
                    case "circumstance":
                        if (!Circumstances.Contains(val))
                            Circumstances.Add(val);
                        Circumstance = Circumstances.IndexOf(val);
                        break;
                    case "circumstance_id":
                        if (valI != -1)
                            CircumstanceID = valI;
                        break;
                    case "site_id":
                        if (valI != -1)
                            SiteID = valI;
                        break;
                    case "form_id":
                        if (valI != -1)
                            FormID = valI;
                        break;
                    case "wc_id": //handled in HE_WrittenContentComposed
                        break;
                    default:
                        DFXMLParser.UnexpectedXMLElement(xdoc.Root.Name.LocalName + "\t" + Types[Type], element, xdoc.Root.ToString());
                        break;
                }
            }
        }

        internal override void Link()
        {
            base.Link();
            if (HistFigureID.HasValue && World.HistoricalFigures.ContainsKey(HistFigureID.Value))
                HistFigure = World.HistoricalFigures[HistFigureID.Value];
            if (SiteID.HasValue && World.Sites.ContainsKey(SiteID.Value))
                Site = World.Sites[SiteID.Value];

        }

        protected override void WriteDataOnParent(MainForm frm, Control parent, ref Point location)
        {
            EventLabel(frm, parent, ref location, "HF:", HistFigure);
            EventLabel(frm, parent, ref location, "Site:", Site);
            if (Reason.HasValue)
                EventLabel(frm, parent, ref location, "Reason:", Reasons[Reason.Value]);
            if (ReasonID.HasValue)
                EventLabel(frm, parent, ref location, "Reason ID:", ReasonID.Value.ToString());
            if (Circumstance.HasValue)
                EventLabel(frm, parent, ref location, "Circumstance:", Circumstances[Circumstance.Value]);
            if (ReasonID.HasValue)
                EventLabel(frm, parent, ref location, "Circumstance ID:", CircumstanceID.Value.ToString());
        }

        internal string GetReasonCircumstanceString()
        {
            var reasonString = "";
            if (Reason.HasValue)
            {
                switch (Reasons[Reason.Value])
                {
                    case "glorify hf":
                        reasonString = " in order to glorify UNKNOWN";
                        break;
                    default:
                        reasonString = "in order to UNKNOWN";
                        break;
                }
            }


            var circumstanceString = "";
            if (Circumstance.HasValue)
            {
                switch (Circumstances[Circumstance.Value])
                {
                    case "pray to hf":
                        circumstanceString = " after praying to UNKNOWN";
                        break;
                    case "nightmare":
                        circumstanceString = " after a nightmare";
                        break;
                    case "dream":
                        circumstanceString = " after a dream";
                        break;
                    case "dream about hf":
                        circumstanceString = " after dreaming of UNKNOWN";
                        break;
                    default:
                        break;
                }
            }

            return $"{reasonString}{circumstanceString}";

        }


        internal override string ToTimelineString()
        {
            //var timelinestring = base.ToTimelineString();

            //return string.Format("{0} Diplomat lost at {1}.",
            //            timelinestring, Site.AltName);
            return "";

        }

        internal List<object> GetExportVals()
        {
            return new List<object>
            {
                ID,
                SiteID.DBExport(),
                HistFigureID.DBExport(),
                Reason.DBExport(Reasons),
                ReasonID.DBExport(),
                Circumstance.DBExport(Circumstances),
                CircumstanceID.DBExport(),
                FormID.DBExport()
            };
        }

    }
}
