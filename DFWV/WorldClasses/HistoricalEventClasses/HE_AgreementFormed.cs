using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Linq;
using DFWV.WorldClasses.HistoricalFigureClasses;

namespace DFWV.WorldClasses.HistoricalEventClasses
{
    class HE_AgreementFormed : HistoricalEvent
    {
        public int AgreementId { get; set; }
        public God God { get; set; }
        public HistoricalFigure Hf { get; set; }
        public Site Site { get; set; }
        public Artifact Artifact { get; set; }

        public int? ConcluderHfid { get; set; }
        public HistoricalFigure ConcluderHf { get; set; }
        public int? AgreementSubjectId { get; set; }
        public string Reason { get; set; }

        public override IEnumerable<HistoricalFigure> HFsInvolved
        {
            get
            {
                yield return Hf;
                yield return ConcluderHf;
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
                        AgreementId = valI;
                        break;
                    case "concluder_hfid":
                        ConcluderHfid = valI;
                        break;
                    case "agreement_subject_id":
                        AgreementSubjectId = valI;
                        break;
                    case "reason":
                        Reason = val;
                        break;
                    default:
                        DFXMLParser.UnexpectedXmlElement(xdoc.Root.Name.LocalName + "\t" + Types[Type], element, xdoc.Root.ToString());
                        break;
                }
            }
        }

        protected override void WriteDataOnParent(MainForm frm, Control parent, ref Point location)
        {
            EventLabel(frm, parent, ref location, "Agreement ID:", AgreementId.ToString());
            EventLabel(frm, parent, ref location, "God", God);
            EventLabel(frm, parent, ref location, "HF:", Hf);
            EventLabel(frm, parent, ref location, "Site:", Site);
            EventLabel(frm, parent, ref location, "Artifact:", Artifact);
            EventLabel(frm, parent, ref location, "Concluder HF:", ConcluderHf);
            EventLabel(frm, parent, ref location, "Agreement Subject ID:", AgreementSubjectId.ToString());
            EventLabel(frm, parent, ref location, "Reason:", Reason);

        }

        protected override string LegendsDescription()
        {
            var timestring = base.LegendsDescription();

            if (!ConcluderHfid.HasValue)
                return
                    $"{timestring} {God?.ToString() ?? "UNKNOWN"} aided the {Hf?.Race.ToString() ?? "UNKNOWN"} {Hf?.ToString() ?? "UNKNOWN"} in becoming a permanent part of the living world that great fortresses might be raised and tested in siege. " +
                    $"The ritual took place in {Site?.ToString() ?? "UNKNOWN"} using {Artifact?.ToString() ?? "UNKNOWN"}";
            return $"{timestring} Agreement formed";
        }

        internal override string ToTimelineString()
        {
            var timelinestring = base.ToTimelineString();

            if (!ConcluderHfid.HasValue)
                return
                    $"{timelinestring} {Hf?.ToString() ?? "UNKNOWN"} performed ritual at {Site?.ToString() ?? "UNKNOWN"} with {Artifact?.ToString() ?? "UNKNOWN"}.";
            return $"{timelinestring} Agreement formed";
        }

        internal override void Export(string table)
        {
            base.Export(table);

            table = GetType().Name;

            var vals = new List<object> 
            { 
                Id, 
                AgreementId, 
                Hf.DBExport(),
                Site.DBExport(),
                Artifact.DBExport(),
                ConcluderHfid.DBExport(),
                AgreementSubjectId.DBExport(),
                Reason.DBExport()  
            };

            Database.ExportWorldItem(table, vals);

        }

    }
}