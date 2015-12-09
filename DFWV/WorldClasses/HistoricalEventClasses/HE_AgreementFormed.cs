﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Linq;
using DFWV.WorldClasses.HistoricalFigureClasses;

namespace DFWV.WorldClasses.HistoricalEventClasses
{
    class HE_AgreementFormed : HistoricalEvent
    {
        public int AgreementID { get; set; }
        public God God { get; set; }
        public HistoricalFigure HF { get; set; }
        public Site Site { get; set; }
        public Artifact Artifact { get; set; }

        public int? ConcluderHFID { get; set; }
        public HistoricalFigure ConcluderHF { get; set; }
        public int? AgreementSubjectID { get; set; }
        public string Reason { get; set; }

        public override IEnumerable<HistoricalFigure> HFsInvolved
        {
            get
            {
                yield return HF;
                yield return ConcluderHF;
            }
        }
        public override IEnumerable<Site> SitesInvolved
        {
            get { yield return Site; }
        }

        public HE_AgreementFormed(XDocument xdoc, World world)
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
                    case "agreement_id":
                        AgreementID = valI;
                        break;
                    case "concluder_hfid":
                        ConcluderHFID = valI;
                        break;
                    case "agreement_subject_id":
                        AgreementSubjectID = valI;
                        break;
                    case "reason":
                        Reason = val;
                        break;
                    default:
                        DFXMLParser.UnexpectedXMLElement(xdoc.Root.Name.LocalName + "\t" + Types[Type], element, xdoc.Root.ToString());
                        break;
                }
            }
        }

        protected override void WriteDataOnParent(MainForm frm, Control parent, ref Point location)
        {
            EventLabel(frm, parent, ref location, "Agreement ID:", AgreementID.ToString());
            EventLabel(frm, parent, ref location, "God", God);
            EventLabel(frm, parent, ref location, "HF:", HF);
            EventLabel(frm, parent, ref location, "Site:", Site);
            EventLabel(frm, parent, ref location, "Artifact:", Artifact);
            EventLabel(frm, parent, ref location, "Concluder HF:", ConcluderHF);
            EventLabel(frm, parent, ref location, "Agreement Subject ID:", AgreementSubjectID.ToString());
            EventLabel(frm, parent, ref location, "Reason:", Reason);

        }

        protected override string LegendsDescription()
        {
            var timestring = base.LegendsDescription();

            if (!ConcluderHFID.HasValue)
                return
                    $"{timestring} {(God != null ? God.ToString() : "UNKNOWN")} aided the {(HF != null ? HF.Race.ToString() : "UNKNOWN")} {(HF != null ? HF.ToString() : "UNKNOWN")} in becoming a permanent part of the living world that great fortresses might be raised and tested in siege. " +
                    $"The ritual took place in {(Site != null ? Site.ToString() : "UNKNOWN")} using {(Artifact != null ? Artifact.ToString() : "UNKNOWN")}";
            return $"{timestring} Agreement formed";
        }

        internal override string ToTimelineString()
        {
            var timelinestring = base.ToTimelineString();

            if (!ConcluderHFID.HasValue)
                return
                    $"{timelinestring} {(HF != null ? HF.ToString() : "UNKNOWN")} performed ritual at {(Site != null ? Site.ToString() : "UNKNOWN")} with {(Artifact != null ? Artifact.ToString() : "UNKNOWN")}.";
            return $"{timelinestring} Agreement formed";
        }

        internal override void Export(string table)
        {
            base.Export(table);

            table = GetType().Name;

            var vals = new List<object> 
            { 
                ID, 
                AgreementID, 
                HF.DBExport(),
                Site.DBExport(),
                Artifact.DBExport(),
                ConcluderHFID.DBExport(),
                AgreementSubjectID.DBExport(),
                Reason.DBExport()  
            };

            Database.ExportWorldItem(table, vals);

        }

    }
}