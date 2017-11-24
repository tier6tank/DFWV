using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Linq;
using DFWV.WorldClasses.HistoricalFigureClasses;

namespace DFWV.WorldClasses.HistoricalEventClasses
{
    class HE_HFAbducted : HistoricalEvent
    {
        private int? HfId_Target { get; }
        public HistoricalFigure Hf_Target { get; private set; }
        private int? HfId_Snatcher { get; }
        private HistoricalFigure Hf_Snatcher { get; set; }
        private int? SiteId { get; }
        private Site Site { get; set; }
        private int? SubregionId { get; }
        private Region Subregion { get; set; }
        private int? FeatureLayerId { get; }

        override public Point Location => Site.Location;

        public override IEnumerable<HistoricalFigure> HFsInvolved
        {
            get
            {
                yield return Hf_Snatcher;
                yield return Hf_Target;
            }
        }
        public override IEnumerable<Site> SitesInvolved
        {
            get { yield return Site; }
        }
        public override IEnumerable<Region> RegionsInvolved
        {
            get { yield return Subregion; }
        }

        public HE_HFAbducted(XDocument xdoc, World world)
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
                    case "target_hfid":
                        HfId_Target = valI;
                        break;
                    case "snatcher_hfid":
                        HfId_Snatcher = valI;
                        break;
                    default:
                        DFXMLParser.UnexpectedXmlElement(xdoc.Root.Name.LocalName + "\t" + Types[Type], element, xdoc.Root.ToString());
                        break;
                }
            }
        }

        protected override void WriteDataOnParent(MainForm frm, Control parent, ref Point location)
        {
            EventLabel(frm, parent, ref location, "Target:", Hf_Target);
            EventLabel(frm, parent, ref location, "Snatcher:", Hf_Snatcher);
            EventLabel(frm, parent, ref location, "Site:", Site);
            EventLabel(frm, parent, ref location, "Region:", Subregion);

        }

        protected override string LegendsDescription() // Matched
        {
            var timestring = base.LegendsDescription();

            return
                $"{timestring} {Hf_Target} was abducted from {Site.AltName} by {Hf_Snatcher?.ToString() ?? "an unknown creature"}.";
        }

        internal override string ToTimelineString()
        {
            var timelinestring = base.ToTimelineString();

            return
                $"{timelinestring} {Hf_Target?.ToString() ?? HfId_Target.ToString()} was abducted from {Site.AltName} by {Hf_Snatcher?.ToString() ?? HfId_Snatcher.ToString()}.";
        }

        internal override void Export(string table)
        {
            base.Export(table);

            table = GetType().Name;
            
            var vals = new List<object>
            {
                Id, 
                HfId_Target.DBExport(), 
                HfId_Snatcher.DBExport(), 
                SiteId.DBExport(), 
                SubregionId.DBExport(), 
                FeatureLayerId.DBExport()
            };

            Database.ExportWorldItem(table, vals);

        }

    }
}

