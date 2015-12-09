using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Linq;
using DFWV.WorldClasses.HistoricalFigureClasses;

namespace DFWV.WorldClasses.HistoricalEventClasses
{
    class HE_HFRelationshipDenied : HistoricalEvent
    {
        private int? SeekerHfid { get; }
        private HistoricalFigure SeekerHf { get; set; }
        private int? TargetHfid { get; }
        private HistoricalFigure TargetHf { get; set; }
        private int? SiteId { get; }
        private Site Site { get; set; }
        private int? SubregionId { get; }
        private Region Subregion { get; set; }
        private int? FeatureLayerId { get; }

        override public Point Location => Point.Empty;

        public int? Wcid { get; set; }
        public int? ReasonId { get; set; }
        public int Reason { get; }
        public static List<string> Reasons = new List<string>();
        public int RelationshipString { get; }
        public static List<string> RelationshipStrings = new List<string>();

        public override IEnumerable<HistoricalFigure> HFsInvolved
        {
            get
            {
                yield return SeekerHf;
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
                        SeekerHfid = valI;
                        break;
                    case "target_hfid":
                        TargetHfid = valI;
                        break;
                    case "wc_id":
                        if (valI != -1)
                            Wcid = valI;
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

        internal override void Link()
        {
            base.Link();
            if (SeekerHfid.HasValue && World.HistoricalFigures.ContainsKey(SeekerHfid.Value))
                SeekerHf = World.HistoricalFigures[SeekerHfid.Value];
            if (TargetHfid.HasValue && World.HistoricalFigures.ContainsKey(TargetHfid.Value))
                TargetHf = World.HistoricalFigures[TargetHfid.Value];
            if (SiteId.HasValue && World.Sites.ContainsKey(SiteId.Value))
                Site = World.Sites[SiteId.Value];
            if (SubregionId.HasValue && World.Regions.ContainsKey(SubregionId.Value))
                Subregion = World.Regions[SubregionId.Value];

        }

        protected override void WriteDataOnParent(MainForm frm, Control parent, ref Point location)
        {
            EventLabel(frm, parent, ref location, "Seeker:", SeekerHf);
            EventLabel(frm, parent, ref location, "Target:", TargetHf);

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
                    relationshipString = $"an apprenticeship under the {TargetHf.Race.ToString().ToLower()} {TargetHf}";
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
                $"{timestring} {SeekerHf} was denied {relationshipString} {reasonString}.";


        }

        internal override string ToTimelineString()
        {
            var timelinestring = base.ToTimelineString();

            return $"{timelinestring} {SeekerHf} was denied relationship with {TargetHf}.";

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
                SeekerHfid.DBExport(),
                TargetHfid.DBExport(),
                RelationshipString.DBExport(RelationshipStrings),
                Reason.DBExport(Reasons),
                ReasonId.DBExport()
            };

            Database.ExportWorldItem(table, vals);

        }

    }
}
