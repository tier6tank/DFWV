﻿using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Linq;
using DFWV.WorldClasses.HistoricalFigureClasses;

namespace DFWV.WorldClasses.HistoricalEventClasses
{
    class HistoricalEvent_CultureCreatedBase : HistoricalEvent
    {
        internal int? HfId { get; set; }
        internal HistoricalFigure Hf { get; set; }
        internal int? SiteId { get; set; }
        internal Site Site { get; set; }
        internal int? SubregionId { get; set; }
        internal Region Subregion { get; set; }

        override public Point Location => Point.Empty;

        public int? FormId { get; set; }
        public int? ReasonId { get; set; }
        public int? Reason { get; internal set; }
        public static List<string> Reasons = new List<string>();
        public int? CircumstanceId { get; set; }
        public int? Circumstance { get; internal set; }
        public static List<string> Circumstances = new List<string>();

        public override IEnumerable<HistoricalFigure> HFsInvolved
        {
            get { yield return Hf; }
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
                        HfId = valI;
                        break;
                    case "reason":
                        if (!Reasons.Contains(val))
                            Reasons.Add(val);
                        Reason = Reasons.IndexOf(val);
                        break;
                    case "reason_id":
                        if (valI != -1)
                            ReasonId = valI;
                        break;
                    case "circumstance":
                        if (!Circumstances.Contains(val))
                            Circumstances.Add(val);
                        Circumstance = Circumstances.IndexOf(val);
                        break;
                    case "circumstance_id":
                        if (valI != -1)
                            CircumstanceId = valI;
                        break;
                    case "site_id":
                        if (valI != -1)
                            SiteId = valI;
                        break;
                    case "subregion_id":
                        if (valI != -1)
                            SiteId = valI;
                        break;
                    case "form_id":
                        if (valI != -1)
                            FormId = valI;
                        break;
                    case "wc_id": //handled in HE_WrittenContentComposed
                        break;
                    default:
                        DFXMLParser.UnexpectedXmlElement(xdoc.Root.Name.LocalName + "\t" + Types[Type], element, xdoc.Root.ToString());
                        break;
                }
            }
        }

        protected override void WriteDataOnParent(MainForm frm, Control parent, ref Point location)
        {
            EventLabel(frm, parent, ref location, "HF:", Hf);
            EventLabel(frm, parent, ref location, "Site:", Site);
            if (Reason.HasValue)
                EventLabel(frm, parent, ref location, "Reason:", Reasons[Reason.Value]);
            if (ReasonId.HasValue)
                EventLabel(frm, parent, ref location, "Reason ID:", ReasonId.Value.ToString());
            if (Circumstance.HasValue)
                EventLabel(frm, parent, ref location, "Circumstance:", Circumstances[Circumstance.Value]);
            if (ReasonId.HasValue)
                EventLabel(frm, parent, ref location, "Circumstance ID:", CircumstanceId);
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
                Id,
                SiteId.DBExport(),
                HfId.DBExport(),
                Reason.DBExport(Reasons),
                ReasonId.DBExport(),
                Circumstance.DBExport(Circumstances),
                CircumstanceId.DBExport(),
                FormId.DBExport()
            };
        }

    }
}
