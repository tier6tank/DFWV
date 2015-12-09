using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Linq;
using DFWV.WorldClasses.HistoricalFigureClasses;

namespace DFWV.WorldClasses.HistoricalEventClasses
{
    class HE_HFAbducted : HistoricalEvent
    {
        private int? TargetHFID { get; set; }
        public HistoricalFigure TargetHF { get; private set; }
        private int? SnatcherHFID { get; set; }
        private HistoricalFigure SnatcherHF { get; set; }
        private int? SiteID { get; set; }
        private Site Site { get; set; }
        private int? SubregionID { get; set; }
        private Region Subregion { get; set; }
        private int? FeatureLayerID { get; set; }

        override public Point Location => Site.Location;

        public override IEnumerable<HistoricalFigure> HFsInvolved
        {
            get
            {
                yield return SnatcherHF;
                yield return TargetHF;
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
                            SiteID = valI;
                        break;
                    case "subregion_id":
                        if (valI != -1)
                            SubregionID = valI;
                        break;
                    case "feature_layer_id":
                        if (valI != -1)
                            FeatureLayerID = valI;
                        break;
                    case "target_hfid":
                        TargetHFID = valI;
                        break;
                    case "snatcher_hfid":
                        SnatcherHFID = valI;
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
            if (SiteID.HasValue && World.Sites.ContainsKey(SiteID.Value))
                Site = World.Sites[SiteID.Value];
            if (SubregionID.HasValue && World.Regions.ContainsKey(SubregionID.Value))
                Subregion = World.Regions[SubregionID.Value];
            if (TargetHFID.HasValue && World.HistoricalFigures.ContainsKey(TargetHFID.Value))
                TargetHF = World.HistoricalFigures[TargetHFID.Value];
            if (SnatcherHFID.HasValue && World.HistoricalFigures.ContainsKey(SnatcherHFID.Value))
                SnatcherHF = World.HistoricalFigures[SnatcherHFID.Value];
        }


        protected override void WriteDataOnParent(MainForm frm, Control parent, ref Point location)
        {
            EventLabel(frm, parent, ref location, "Target:", TargetHF);
            EventLabel(frm, parent, ref location, "Snatcher:", SnatcherHF);
            EventLabel(frm, parent, ref location, "Site:", Site);
            EventLabel(frm, parent, ref location, "Region:", Subregion);

        }

        protected override string LegendsDescription() // Matched
        {
            var timestring = base.LegendsDescription();

            return
                $"{timestring} {TargetHF} was abducted from {Site.AltName} by {(SnatcherHF == null ? "an unknown creature" : SnatcherHF.ToString())}.";
        }

        internal override string ToTimelineString()
        {
            var timelinestring = base.ToTimelineString();

            return
                $"{timelinestring} {(TargetHF != null ? TargetHF.ToString() : TargetHFID.ToString())} was abducted from {Site.AltName} by {(SnatcherHF != null ? SnatcherHF.ToString() : SnatcherHFID.ToString())}.";
        }

        internal override void Export(string table)
        {
            base.Export(table);

            table = GetType().Name;
            
            var vals = new List<object>
            {
                ID, 
                TargetHFID.DBExport(), 
                SnatcherHFID.DBExport(), 
                SiteID.DBExport(), 
                SubregionID.DBExport(), 
                FeatureLayerID.DBExport()
            };

            Database.ExportWorldItem(table, vals);

        }

    }
}

