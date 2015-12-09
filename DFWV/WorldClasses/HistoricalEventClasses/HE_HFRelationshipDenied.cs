using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Linq;
using DFWV.WorldClasses.EntityClasses;
using DFWV.WorldClasses.HistoricalFigureClasses;

namespace DFWV.WorldClasses.HistoricalEventClasses
{
    class HE_HFRelationshipDenied : HistoricalEvent
    {
        private int? SeekerHFID { get; set; }
        private HistoricalFigure SeekerHF { get; set; }
        private int? TargetHFID { get; set; }
        private HistoricalFigure TargetHF { get; set; }
        private int? SiteID { get; set; }
        private Site Site { get; set; }
        private int? SubregionID { get; set; }
        private Region Subregion { get; set; }
        private int? FeatureLayerID { get; set; }

        override public Point Location => Point.Empty;

        public int? WCID { get; set; }
        public int? ReasonID { get; set; }
        public int Reason { get; private set; }
        public static List<string> Reasons = new List<string>();
        public int Relationship { get; private set; }
        public static List<string> Relationships = new List<string>();

        public override IEnumerable<HistoricalFigure> HFsInvolved
        {
            get
            {
                yield return SeekerHF;
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
                        SeekerHFID = valI;
                        break;
                    case "target_hfid":
                        TargetHFID = valI;
                        break;
                    case "wc_id":
                        if (valI != -1)
                            WCID = valI;
                        break;
                    case "reason":
                        if (!Reasons.Contains(val))
                            Reasons.Add(val);
                        Reason = Reasons.IndexOf(val);
                        break;
                    case "reason_id":
                        if (valI != -1)
                            ReasonID = valI;
                        break;
                    case "relationship":
                        if (!Relationships.Contains(val))
                            Relationships.Add(val);
                        Relationship = Relationships.IndexOf(val);
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
                    default:
                        DFXMLParser.UnexpectedXMLElement(xdoc.Root.Name.LocalName + "\t" + Types[Type], element, xdoc.Root.ToString());
                        break;
                }
            }
        }

        internal override void Link()
        {
            base.Link();
            if (SeekerHFID.HasValue && World.HistoricalFigures.ContainsKey(SeekerHFID.Value))
                SeekerHF = World.HistoricalFigures[SeekerHFID.Value];
            if (TargetHFID.HasValue && World.HistoricalFigures.ContainsKey(TargetHFID.Value))
                TargetHF = World.HistoricalFigures[TargetHFID.Value];
            if (SiteID.HasValue && World.Sites.ContainsKey(SiteID.Value))
                Site = World.Sites[SiteID.Value];
            if (SubregionID.HasValue && World.Regions.ContainsKey(SubregionID.Value))
                Subregion = World.Regions[SubregionID.Value];

        }

        protected override void WriteDataOnParent(MainForm frm, Control parent, ref Point location)
        {
            EventLabel(frm, parent, ref location, "Seeker:", SeekerHF);
            EventLabel(frm, parent, ref location, "Target:", TargetHF);

            EventLabel(frm, parent, ref location, "Relationship:", Relationships[Relationship]);
            EventLabel(frm, parent, ref location, "Reason:", Reasons[Reason]);
            if (ReasonID.HasValue)
                EventLabel(frm, parent, ref location, "Reason ID:", ReasonID.Value.ToString());
            EventLabel(frm, parent, ref location, "Site:", Site);
            EventLabel(frm, parent, ref location, "Region:", Subregion);

        }

        protected override string LegendsDescription()
        {
            var timestring = base.LegendsDescription();


            var relationshipString = "";
            switch (Relationships[Relationship])
            {
                case "apprentice":
                    relationshipString = $"an apprenticeship under the {TargetHF.Race.ToString().ToLower()} {TargetHF}";
                    break;
                default:
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
                default:
                    break;
            }

            return
                $"{timestring} {SeekerHF} was denied {relationshipString} {reasonString}.";


        }

        internal override string ToTimelineString()
        {
            var timelinestring = base.ToTimelineString();

            return $"{SeekerHF} was denied relationship with {TargetHF}.";

        }

        internal override void Export(string table)
        {
            base.Export(table);

            table = GetType().Name;

            var vals = new List<object>
            {
                ID,
                SiteID.DBExport(),
                SubregionID.DBExport(),
                FeatureLayerID.DBExport(),
                SeekerHFID.DBExport(),
                TargetHFID.DBExport(),
                Relationship.DBExport(Relationships),
                Reason.DBExport(Reasons),
                ReasonID.DBExport()
            };

            Database.ExportWorldItem(table, vals);

        }

    }
}
