using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Linq;
using DFWV.WorldClasses.HistoricalFigureClasses;

namespace DFWV.WorldClasses.HistoricalEventClasses
{
    class HE_HFFormsReputationRelationship : HistoricalEvent
    {
        private int? HfId_1 { get; }
        private HistoricalFigure Hf_1 { get; set; }
        private int? IdentityId { get; }
        private int? HfId_2 { get; }
        private HistoricalFigure Hf_2 { get; set; }
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
                yield return Hf_1;
                yield return Hf_2;
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
                        HfId_1 = valI;
                        break;
                    case "identity_id1":
                        if (valI != -1)
                            HfId_1 = valI;
                        break;
                    case "hfid2":
                        if (valI != -1)
                            HfId_2 = valI;
                        break;
                    case "identity_id2":
                        if (valI != -1)
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

        protected override void WriteDataOnParent(MainForm frm, Control parent, ref Point location)
        {
            EventLabel(frm, parent, ref location, "Hist Fig 1:", Hf_1);
            if (HFRep1Of2.HasValue)
                EventLabel(frm, parent, ref location, "Rep 1 of 2:", RepTypes[HFRep1Of2.Value]);
            if (IdentityId.HasValue)
                EventLabel(frm, parent, ref location, "Identity 1:", IdentityId.Value);
            EventLabel(frm, parent, ref location, "Hist Fig 2:", Hf_2);
            if (HFRep2Of1.HasValue)
                EventLabel(frm, parent, ref location, "Rep 2 of 1:", RepTypes[HFRep2Of1.Value]);
            if (Identity2Id.HasValue)
                EventLabel(frm, parent, ref location, "Identity 2:", Identity2Id.Value);
            EventLabel(frm, parent, ref location, "Site:", Site);
            EventLabel(frm, parent, ref location, "Subregion:", Subregion);
        }

        protected override string LegendsDescription()
        {
            var timestring = base.LegendsDescription();

            if (HFRep1Of2.HasValue)
            {
                switch (RepTypes[HFRep1Of2.Value])
                {
                    case "information source":
                        return $"{timestring} {Hf_1}, as \"{"UKNOWN Identity"}\", formed a false friendship with {Hf_2} where each used the other for information {Site.AltName}";
                    case "buddy":
                        return $"{timestring} {Hf_1}, as \"{"UKNOWN Identity"}\", formed UNKNOWN Relationship with {Hf_2} in {Site.AltName}";

                    default:
                        break;
                }
            }
            return $"{timestring} {Hf_1}, as \"{"UKNOWN Identity"}\", formed a false friendship with {Hf_2} in order to extract information in {Site.AltName}";


        }

        internal override string ToTimelineString()
        {
            var timelinestring = base.ToTimelineString();

            return timelinestring;
        }
    }
}