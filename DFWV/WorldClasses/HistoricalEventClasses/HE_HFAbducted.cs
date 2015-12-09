using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Linq;
using DFWV.WorldClasses.HistoricalFigureClasses;

namespace DFWV.WorldClasses.HistoricalEventClasses
{
    class HE_HFAbducted : HistoricalEvent
    {
        private int? TargetHfid { get; }
        public HistoricalFigure TargetHf { get; private set; }
        private int? SnatcherHfid { get; }
        private HistoricalFigure SnatcherHf { get; set; }
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
                yield return SnatcherHf;
                yield return TargetHf;
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
                        TargetHfid = valI;
                        break;
                    case "snatcher_hfid":
                        SnatcherHfid = valI;
                        break;
                    default:
                        DFXMLParser.UnexpectedXmlElement(xdoc.Root.Name.LocalName + "\t" + Types[Type], element, xdoc.Root.ToString());
                        break;
                }
            }
        }

        internal override void Link()
        {
            base.Link();
            if (SiteId.HasValue && World.Sites.ContainsKey(SiteId.Value))
                Site = World.Sites[SiteId.Value];
            if (SubregionId.HasValue && World.Regions.ContainsKey(SubregionId.Value))
                Subregion = World.Regions[SubregionId.Value];
            if (TargetHfid.HasValue && World.HistoricalFigures.ContainsKey(TargetHfid.Value))
                TargetHf = World.HistoricalFigures[TargetHfid.Value];
            if (SnatcherHfid.HasValue && World.HistoricalFigures.ContainsKey(SnatcherHfid.Value))
                SnatcherHf = World.HistoricalFigures[SnatcherHfid.Value];
        }


        protected override void WriteDataOnParent(MainForm frm, Control parent, ref Point location)
        {
            EventLabel(frm, parent, ref location, "Target:", TargetHf);
            EventLabel(frm, parent, ref location, "Snatcher:", SnatcherHf);
            EventLabel(frm, parent, ref location, "Site:", Site);
            EventLabel(frm, parent, ref location, "Region:", Subregion);

        }

        protected override string LegendsDescription() // Matched
        {
            var timestring = base.LegendsDescription();

            return
                $"{timestring} {TargetHf} was abducted from {Site.AltName} by {SnatcherHf?.ToString() ?? "an unknown creature"}.";
        }

        internal override string ToTimelineString()
        {
            var timelinestring = base.ToTimelineString();

            return
                $"{timelinestring} {TargetHf?.ToString() ?? TargetHfid.ToString()} was abducted from {Site.AltName} by {SnatcherHf?.ToString() ?? SnatcherHfid.ToString()}.";
        }

        internal override void Export(string table)
        {
            base.Export(table);

            table = GetType().Name;
            
            var vals = new List<object>
            {
                Id, 
                TargetHfid.DBExport(), 
                SnatcherHfid.DBExport(), 
                SiteId.DBExport(), 
                SubregionId.DBExport(), 
                FeatureLayerId.DBExport()
            };

            Database.ExportWorldItem(table, vals);

        }

    }
}

