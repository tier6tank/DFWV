using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Linq;
using DFWV.WorldClasses.HistoricalFigureClasses;

namespace DFWV.WorldClasses.HistoricalEventClasses
{
    class HE_HFFormsReputationRelationship : HistoricalEvent
    {



        private int? HistFigId { get; }
        private HistoricalFigure HistFig { get; set; }
        private int? IdentityId { get; }
        private int? HistFig2Id { get; }
        private HistoricalFigure HistFig2 { get; set; }
        private int? Identity2Id { get; }
        private int? HFRep1Of2 { get; set; }
        private int? HFRep2Of1 { get; set; }
        public static List<string> RepTypes = new List<string>();

        private int? SiteId { get; }
        private Site Site { get; set; }
        private int? SubregionId { get; }
        private Region Subregion { get; set; }
        private int? FeatureLayerId { get; }

        override public Point Location => Site.Location;

        public override IEnumerable<HistoricalFigure> HFsInvolved
        {
            get {
                yield return HistFig;
                yield return HistFig2;
            }
        }
        public override IEnumerable<Site> SitesInvolved
        {
            get { yield return Site; }
        }

        public HE_HFFormsReputationRelationship(XDocument xdoc, World world)
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

                    case "hfid1":
                        HistFigId = valI;
                        break;
                    case "identity_id1":
                        HistFigId = valI;
                        break;
                    case "hfid2":
                        HistFig2Id = valI;
                        break;
                    case "identity_id2":
                        Identity2Id = valI;
                        break;
                    case "hf_rep_1_of_2":
                        if (!RepTypes.Contains(val))
                            RepTypes.Add(val);
                        HFRep1Of2 = RepTypes.IndexOf(val);
                        break;
                    case "hf_rep_2_of_1":
                        if (!RepTypes.Contains(val))
                            RepTypes.Add(val);
                        HFRep1Of2 = RepTypes.IndexOf(val);
                        break;
                    case "site_id":
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
            if (HistFigId.HasValue && World.HistoricalFigures.ContainsKey(HistFigId.Value))
                HistFig = World.HistoricalFigures[HistFigId.Value];
            if (HistFig2Id.HasValue && World.HistoricalFigures.ContainsKey(HistFig2Id.Value))
                HistFig2 = World.HistoricalFigures[HistFig2Id.Value];
        }

        protected override void WriteDataOnParent(MainForm frm, Control parent, ref Point location)
        {
            EventLabel(frm, parent, ref location, "Hist Fig:", HistFig);
            EventLabel(frm, parent, ref location, "Site:", Site);
        }

        protected override string LegendsDescription()
        {
            var timestring = base.LegendsDescription();

            return "";
        }

        internal override string ToTimelineString()
        {
            var timelinestring = base.ToTimelineString();

            return timelinestring;
        }
    }
}