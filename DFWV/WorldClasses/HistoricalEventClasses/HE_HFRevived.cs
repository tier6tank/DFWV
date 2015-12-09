using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Linq;
using DFWV.WorldClasses.HistoricalFigureClasses;

namespace DFWV.WorldClasses.HistoricalEventClasses
{
    class HE_HFRevived : HistoricalEvent
    {
        private int? Hfid { get; }
        private HistoricalFigure Hf { get; set; }
        private string Ghost { get; }
        private bool RaisedBefore { get; }
        private int? SiteId { get; }
        private Site Site { get; set; }
        private int? SubregionId { get; }
        private Region Subregion { get; set; }
        private int? FeatureLayerId { get; }

        override public Point Location => Site?.Location ?? Subregion.Location;

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

        public HE_HFRevived(XDocument xdoc, World world)
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
                        Hfid = valI;
                        break;
                    case "ghost":
                        Ghost = val;
                        break;
                    case "raised_before":
                        RaisedBefore = true;
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
            if (Hfid.HasValue && World.HistoricalFigures.ContainsKey(Hfid.Value))
                Hf = World.HistoricalFigures[Hfid.Value];
            if (SiteId.HasValue && World.Sites.ContainsKey(SiteId.Value))
                Site = World.Sites[SiteId.Value];
            if (SubregionId.HasValue && World.Regions.ContainsKey(SubregionId.Value))
                Subregion = World.Regions[SubregionId.Value];
        }

        protected override void WriteDataOnParent(MainForm frm, Control parent, ref Point location)
        {
            EventLabel(frm, parent, ref location, "HF:", Hf);
            EventLabel(frm, parent, ref location, "Ghost:", Ghost);
            EventLabel(frm, parent, ref location, "Site:", Site);
            EventLabel(frm, parent, ref location, "Region:", Subregion);
            EventLabel(frm, parent, ref location, "Layer:", FeatureLayerId == -1 ? "" : FeatureLayerId.ToString());
        }

        protected override string LegendsDescription() //Matched
        {
            var timestring = base.LegendsDescription();

            return $"{timestring} {Hf} came back from the dead as in {Site.AltName}.";
        }

        internal override string ToTimelineString()
        {
            var timelinestring = base.ToTimelineString();

            if (Site != null)
                return
                    $"{timelinestring} {Hf?.ToString() ?? Hfid.ToString()} came back from the dead as a {Ghost} in {Site.AltName}*";
            return
                $"{timelinestring} {Hf?.ToString() ?? Hfid.ToString()} came back from the dead as a {Ghost}";
        }

        internal override void Export(string table)
        {
            base.Export(table);

            table = GetType().Name;

            var vals = new List<object>
            {
                Id, 
                Hfid.DBExport(), 
                Ghost, 
                RaisedBefore, 
                SiteId.DBExport(), 
                SubregionId.DBExport(), 
                FeatureLayerId.DBExport()
            };

            Database.ExportWorldItem(table, vals);
        }

    }
}
