using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Linq;
using DFWV.WorldClasses.HistoricalFigureClasses;

namespace DFWV.WorldClasses.HistoricalEventClasses
{
    class HE_HFConfronted : HistoricalEvent
    {
        private int? Hfid { get; }
        private HistoricalFigure Hf { get; set; }
        private int? SiteId { get; }
        private Site Site { get; set; }
        private int? SubregionId { get; }
        private Region Subregion { get; set; }
        private int? FeatureLayerId { get; }
        private Point Coords { get; }
        private static readonly List<string> Situations = new List<string>();
        private int? Situation { get; }
        private static readonly List<string> Reasons = new List<string>();
        private int? Reason { get; }

        override public Point Location => Site.Location;

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

        public HE_HFConfronted(XDocument xdoc, World world)
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
                    case "coords":
                        if (val != "-1,-1")
                            Coords = new Point(Convert.ToInt32(val.Split(',')[0]), Convert.ToInt32(val.Split(',')[1]));
                        break;
                    case "hfid":
                        Hfid = valI;
                        break;
                    case "situation":
                        if (!Situations.Contains(val))
                            Situations.Add(val);
                        Situation = Situations.IndexOf(val);
                        break;
                    case "reason":
                        if (!Reasons.Contains(val))
                            Reasons.Add(val);
                        Reason = Reasons.IndexOf(val);
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
            if (Situation.HasValue)
                EventLabel(frm, parent, ref location, "Situation:", Situations[Situation.Value]);
            if (Reason.HasValue)
                EventLabel(frm, parent, ref location, "Reason:", Reasons[Reason.Value]);
            EventLabel(frm, parent, ref location, "Site:", Site);
            EventLabel(frm, parent, ref location, "Region:", Subregion);
            if (FeatureLayerId != null && FeatureLayerId > -1)
                EventLabel(frm, parent, ref location, "Layer:", FeatureLayerId == -1 ? "" : FeatureLayerId.ToString());
            EventLabel(frm, parent, ref location, "Coords:", new Coordinate(Coords));
        }

        protected override string LegendsDescription()
        {
            var timestring = base.LegendsDescription();

            if (!Reason.HasValue || !Situation.HasValue)
                return timestring;
            if (Reasons[Reason.Value] == "murder" && Situations[Situation.Value] == "general suspicion")
                return $"{timestring} {Hf} aroused general suspicion in {Site.AltName} after a murder.";
            if (Reasons[Reason.Value] == "ageless" && Situations[Situation.Value] == "general suspicion")
                return $"{timestring} {Hf} aroused general suspicion in {Site.AltName} after appearing not to age.";
            return timestring;
        }

        internal override string ToTimelineString()
        {
            var timelinestring = base.ToTimelineString();

            if (!Reason.HasValue || !Situation.HasValue)
                return timelinestring;
            if (Reasons[Reason.Value] == "murder" && Situations[Situation.Value] == "general suspicion")
                return $"{timelinestring} {Hf} aroused general suspicion in {Site.AltName} after a murder.";
            if (Reasons[Reason.Value] == "ageless" && Situations[Situation.Value] == "general suspicion")
                return $"{timelinestring} {Hf} aroused general suspicion in {Site.AltName} after appearing not to age.";
            return timelinestring;
        }

        internal override void Export(string table)
        {
            base.Export(table);

            table = GetType().Name;

            var vals = new List<object>
            {
                Id, 
                Hfid.DBExport(), 
                Situation.DBExport(Situations),
                Reason.DBExport(Reasons), 
                SiteId.DBExport(), 
                SubregionId.DBExport(), 
                FeatureLayerId.DBExport(),
                Coords.DBExport()
            };

            Database.ExportWorldItem(table, vals);
        }

    }
}