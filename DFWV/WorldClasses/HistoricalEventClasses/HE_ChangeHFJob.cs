using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Linq;
using DFWV.WorldClasses.HistoricalFigureClasses;

namespace DFWV.WorldClasses.HistoricalEventClasses
{
    class HE_ChangeHFJob : HistoricalEvent
    {
        private int? HfId { get; }
        private HistoricalFigure Hf { get; set; }
        private int? SiteId { get; }
        private Site Site { get; set; }
        private int? SubregionId { get; }
        private Region Subregion { get; set; }
        private int? FeatureLayerId { get; }
        public int? NewJobId { get; set; }
        public int? OldJobId { get; set; }
        public int? NewJob { get; set; }
        public int? OldJob { get; set; }

        override public Point Location => Site?.Location ?? (Subregion?.Location ?? Point.Empty);

        public override IEnumerable<HistoricalFigure> HFsInvolved
        {
            get { yield return Hf; }
        }
        public override IEnumerable<Site> SitesInvolved
        {
            get { yield return Site; }
        }
        public override IEnumerable<Region> RegionsInvolved
        {
            get { yield return Subregion; }
        }

        public HE_ChangeHFJob(XDocument xdoc, World world)
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
                    case "site_id":
                        if (valI != -1)
                            SiteId = valI;
                        break;
                    case "subregion_id":
                        if (valI != -1)
                            SubregionId = valI;
                        break;
                    case "feature_layer_id":
                        if (valI != -1)
                            FeatureLayerId = valI;
                        break;
                    case "hfid":
                        HfId = valI;
                        break;

                    default:
                        DFXMLParser.UnexpectedXmlElement(xdoc.Root.Name.LocalName + "\t" + Types[Type], element, xdoc.Root.ToString());
                        break;
                }
            }
        }

        internal override void Plus(XDocument xdoc)
        {
            foreach (var element in xdoc.Root.Elements())
            {
                var val = element.Value;
                int valI;
                int.TryParse(val, out valI);

                switch (element.Name.LocalName)
                {
                    case "id":
                    case "type":
                        break;
                    case "hfid":
                    case "site":
                        break;
                    case "new_job":
                        if (!Unit.JobTypes.Contains(val))
                            Unit.JobTypes.Add(val);
                        NewJob = Unit.JobTypes.IndexOf(val);
                        break;
                    case "old_job":
                        if (!Unit.JobTypes.Contains(val))
                            Unit.JobTypes.Add(val);
                        OldJob = Unit.JobTypes.IndexOf(val);
                        break;
                    default:
                        DFXMLParser.UnexpectedXmlElement(xdoc.Root.Name.LocalName + "\t" + Types[Type], element, xdoc.Root.ToString());
                        break;
                }
            }
        }

        protected override void WriteDataOnParent(MainForm frm, Control parent, ref Point location)
        {
            //TODO: Incorporate new data
            EventLabel(frm, parent, ref location, "HF:", Hf);
            if (Site != null)
                EventLabel(frm, parent, ref location, "Site:", Site);
            if (Subregion != null)
                EventLabel(frm, parent, ref location, "Region:", Subregion);
            if (FeatureLayerId != null)
                EventLabel(frm, parent, ref location, "Layer:", FeatureLayerId == -1 ? "" : FeatureLayerId.ToString());
            if (OldJob.HasValue)
                EventLabel(frm, parent, ref location, "Old Job:", Unit.JobTypes[OldJob.Value]);
            if (NewJob.HasValue)
                EventLabel(frm, parent, ref location, "New Job:", Unit.JobTypes[NewJob.Value]);
        }

        protected override string LegendsDescription()
        {
            var timestring = base.LegendsDescription();
            if (NewJob.HasValue && OldJob.HasValue)
            {
                if (Unit.JobTypes[NewJob.Value] != "standard")
                    return
                        $"{timestring} {Hf.Race} {Hf} became {Unit.JobTypes[NewJob.Value]} in {(Site != null ? Site.AltName : Subregion.Name.ToTitleCase())}.";
                return
                    $"{timestring} {Hf.Race} {Hf} stopped being a {Unit.JobTypes[OldJob.Value]} in {(Site != null ? Site.AltName : Subregion.Name)}.";
            }
            return $"{timestring} {Hf?.Race?.ToString() ?? ""} {(Hf == null ? "UNKNOWN" : Hf.ToString())} became {"UNKNOWN"} in {(Site != null ? Site.AltName : (Subregion != null ? Subregion.Name.ToTitleCase() : "UNKNOWN"))}.";

        }

        internal override string ToTimelineString()
        {
            //TODO: Incorporate new data
            var timelinestring = base.ToTimelineString();

            if (Site != null)
                return
                    $"{timelinestring} {Hf?.ToString() ?? HfId.ToString()} changed jobs at {Site.AltName}";
            return $"{timelinestring} {Hf?.ToString() ?? HfId.ToString()} changed jobs";
        }

        internal override void Export(string table)
        {
            base.Export(table);

            table = GetType().Name;


            var vals = new List<object>
            {
                Id, 
                HfId.DBExport(), 
                SiteId.DBExport(), 
                SubregionId.DBExport(), 
                FeatureLayerId.DBExport(),
                NewJobId.DBExport(),
                OldJobId.DBExport()
            };

            Database.ExportWorldItem(table, vals);

        }

    }
}
