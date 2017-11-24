using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Linq;
using DFWV.WorldClasses.HistoricalFigureClasses;

namespace DFWV.WorldClasses.HistoricalEventClasses
{
    class HE_HFRelationshipDenied : HistoricalEvent
    {
        private int? HfId_Seeker { get; }
        private HistoricalFigure Hf_Seeker { get; set; }
        private int? HfId_Target { get; }
        private HistoricalFigure Hf_Target { get; set; }
        private int? SiteId { get; }
        private Site Site { get; set; }
        private int? SubregionId { get; }
        private Region Subregion { get; set; }
        private int? FeatureLayerId { get; }

        override public Point Location => Point.Empty;

        public int? WcId { get; set; }
        public WorldConstruction Wc { get; set; }
        public int? ReasonId { get; set; }
        public int Reason { get; }
        public static List<string> Reasons = new List<string>();
        public int RelationshipString { get; }
        public static List<string> RelationshipStrings = new List<string>();

        public override IEnumerable<HistoricalFigure> HFsInvolved
        {
            get
            {
                yield return Hf_Seeker;
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




        public HE_HFRelationshipDenied(XDocument xdoc, World world)
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
                    case "seeker_hfid":
                        HfId_Seeker = valI;
                        break;
                    case "target_hfid":
                        HfId_Target = valI;
                        break;
                    case "wc_id":
                        if (valI != -1)
                            WcId = valI;
                        break;
                    case "reason":
                        if (!Reasons.Contains(val))
                            Reasons.Add(val);
                        Reason = Reasons.IndexOf(val);
                        break;
                    case "reason_id":
                        if (valI != -1)
                            ReasonId = valI;
                        break;
                    case "relationship":
                        if (!RelationshipStrings.Contains(val))
                            RelationshipStrings.Add(val);
                        RelationshipString = RelationshipStrings.IndexOf(val);
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
                    default:
                        DFXMLParser.UnexpectedXmlElement(xdoc.Root.Name.LocalName + "\t" + Types[Type], element, xdoc.Root.ToString());
                        break;
                }
            }
        }

        protected override void WriteDataOnParent(MainForm frm, Control parent, ref Point location)
        {
            EventLabel(frm, parent, ref location, "Seeker:", Hf_Seeker);
            EventLabel(frm, parent, ref location, "Target:", Hf_Target);

            EventLabel(frm, parent, ref location, "Relationship:", RelationshipStrings[RelationshipString]);
            EventLabel(frm, parent, ref location, "Reason:", Reasons[Reason]);
            if (ReasonId.HasValue)
                EventLabel(frm, parent, ref location, "Reason ID:", ReasonId.Value.ToString());
            EventLabel(frm, parent, ref location, "Site:", Site);
            EventLabel(frm, parent, ref location, "Region:", Subregion);

        }

        protected override string LegendsDescription()
        {
            var timestring = base.LegendsDescription();


            var relationshipString = "";
            switch (RelationshipStrings[RelationshipString])
            {
                case "apprentice":
                    relationshipString = $"an apprenticeship under the {Hf_Target.Race.ToString().ToLower()} {Hf_Target}";
                    break;
            }

            var reasonString = "";
            switch (Reasons[Reason])
            {
                case "prefers working alone":
                    reasonString = "as the latter prefers to work alone";
                    break;
                case "jealousy":
                    reasonString = "due to UNKNOWN's jealousy";
                    break;
            }

            return
                $"{timestring} {Hf_Seeker} was denied {relationshipString} {reasonString}.";


        }

        internal override string ToTimelineString()
        {
            var timelinestring = base.ToTimelineString();

            return $"{timelinestring} {Hf_Seeker} was denied relationship with {Hf_Target}.";

        }

        internal override void Export(string table)
        {
            base.Export(table);

            table = GetType().Name;

            var vals = new List<object>
            {
                Id,
                SiteId.DBExport(),
                SubregionId.DBExport(),
                FeatureLayerId.DBExport(),
                HfId_Seeker.DBExport(),
                HfId_Target.DBExport(),
                RelationshipString.DBExport(RelationshipStrings),
                Reason.DBExport(Reasons),
                ReasonId.DBExport()
            };

            Database.ExportWorldItem(table, vals);

        }

    }
}
