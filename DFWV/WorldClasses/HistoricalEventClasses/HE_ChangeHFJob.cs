using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Linq;
using DFWV.WorldClasses.HistoricalFigureClasses;

namespace DFWV.WorldClasses.HistoricalEventClasses
{
    class HE_ChangeHFJob : HistoricalEvent
    {
        private int? HFID { get; set; }
        private HistoricalFigure HF { get; set; }
        private int? SiteID { get; set; }
        private Site Site { get; set; }
        private int? SubregionID { get; set; }
        private Region Subregion { get; set; }
        private int? FeatureLayerID { get; set; }
        public int? NewJobID { get; set; }
        public int? OldJobID { get; set; }
        public string NewJob { get; set; }
        public string OldJob { get; set; }

        override public Point Location { get { return Site != null ? Site.Location : (Subregion != null ? Subregion.Location : Point.Empty); } }

        public override IEnumerable<HistoricalFigure> HFsInvolved
        {
            get { yield return HF; }
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
                    case "hfid":
                        HFID = valI;
                        break;

                    default:
                        DFXMLParser.UnexpectedXMLElement(xdoc.Root.Name.LocalName + "\t" + Types[Type], element, xdoc.Root.ToString());
                        break;
                }
            }
        }
        internal override void Link()
        {
            //TODO: Incorporate new data
            base.Link();
            if (HFID.HasValue && World.HistoricalFigures.ContainsKey(HFID.Value))
                HF = World.HistoricalFigures[HFID.Value];
            if (SiteID.HasValue && World.Sites.ContainsKey(SiteID.Value))
                Site = World.Sites[SiteID.Value];
            if (SubregionID.HasValue && World.Regions.ContainsKey(SubregionID.Value))
                Subregion = World.Regions[SubregionID.Value];
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
                        NewJob = val.Replace("_", " ");
                        break;
                    case "old_job":
                        OldJob = val.Replace("_", " ");
                        break;
                    default:
                        DFXMLParser.UnexpectedXMLElement(xdoc.Root.Name.LocalName + "\t" + Types[Type], element, xdoc.Root.ToString());
                        break;
                }
            }
        }

        protected override void WriteDataOnParent(MainForm frm, Control parent, ref Point location)
        {
            //TODO: Incorporate new data
            EventLabel(frm, parent, ref location, "HF:", HF);
            if (Site != null)
                EventLabel(frm, parent, ref location, "Site:", Site);
            if (Subregion != null)
                EventLabel(frm, parent, ref location, "Region:", Subregion);
            if (FeatureLayerID != null)
                EventLabel(frm, parent, ref location, "Layer:", FeatureLayerID == -1 ? "" : FeatureLayerID.ToString());
        }

        protected override string LegendsDescription() //Not Matched
        {
            //TODO: Incorporate new data
            var timestring = base.LegendsDescription();

            if (NewJob != "standard")
                return string.Format("{0} {1} {2} became {3} in {4}.",
                    timestring, HF.Race, HF, NewJob ?? "UNKNOWN",
                    Site != null ? Site.AltName : Subregion.Name.ToTitleCase());
            return string.Format("{0} {1} {2} stopped being a {3} in {4}.",
                timestring, HF.Race, HF, OldJob ?? "UNKNOWN",
                            Site != null ? Site.AltName : Subregion.Name);
        }

        internal override string ToTimelineString()
        {
            //TODO: Incorporate new data
            var timelinestring = base.ToTimelineString();

            if (Site != null)
                return string.Format("{0} {1} changed jobs at {2}",
                    timelinestring, HF != null ? HF.ToString() : HFID.ToString(), 
                                Site.AltName);
            return string.Format("{0} {1} changed jobs",
                timelinestring, HF != null ? HF.ToString() : HFID.ToString());
        }

        internal override void Export(string table)
        {
            base.Export(table);

            table = GetType().Name;


            var vals = new List<object>
            {
                ID, 
                HFID.DBExport(), 
                SiteID.DBExport(), 
                SubregionID.DBExport(), 
                FeatureLayerID.DBExport(),
                NewJobID.DBExport(),
                OldJobID.DBExport()
            };

            Database.ExportWorldItem(table, vals);

        }

    }
}
