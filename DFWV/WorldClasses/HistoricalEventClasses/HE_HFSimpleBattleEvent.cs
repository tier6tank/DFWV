using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;
using DFWV.WorldClasses.HistoricalFigureClasses;

namespace DFWV.WorldClasses.HistoricalEventClasses
{
    class HE_HFSimpleBattleEvent : HistoricalEvent
    {
        private List<int> Group1Hfid { get; }
        public List<HistoricalFigure> Group1Hf { get; private set; }
        private List<int> Group2Hfid { get; }
        private List<HistoricalFigure> Group2Hf { get; set; }
        private int? SiteId { get; }
        private Site Site { get; set; }
        private int? SubregionId { get; }
        private Region Subregion { get; set; }
        private int? FeatureLayerId { get; }
        private int Subtype { get; }
        public static List<string> SubTypes = new List<string>();

        override public Point Location => Site?.Location ?? (Subregion?.Location ?? Point.Empty);

        public override IEnumerable<HistoricalFigure> HFsInvolved
        {
            get
            {
                if (Group1Hf != null)
                {
                    foreach (var historicalFigure in Group1Hf)
                        yield return historicalFigure;
                }
                if (Group2Hf != null)
                {
                    foreach (var historicalFigure in Group2Hf)
                        yield return historicalFigure;
                }
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

        public HE_HFSimpleBattleEvent(XDocument xdoc, World world)
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
                    case "subtype":
                        if (!SubTypes.Contains(val))
                            SubTypes.Add(val);
                        Subtype = SubTypes.IndexOf(val);
                        break;
                    case "group_1_hfid":
                        if (Group1Hfid == null)
                            Group1Hfid = new List<int>();
                        Group1Hfid.Add(valI);
                        break;
                    case "group_2_hfid":
                        if (Group2Hfid == null)
                            Group2Hfid = new List<int>();
                        Group2Hfid.Add(valI);
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
            if (Group1Hfid != null)
            {
                Group1Hf = new List<HistoricalFigure>();
                foreach (var group1Hfid in Group1Hfid.Where(group1Hfid => World.HistoricalFigures.ContainsKey(group1Hfid)))
                {
                    Group1Hf.Add(World.HistoricalFigures[group1Hfid]);
                }
            }
            if (Group2Hfid == null) return;
            Group2Hf = new List<HistoricalFigure>();
            foreach (var group2Hfid in Group2Hfid.Where(group2Hfid => World.HistoricalFigures.ContainsKey(group2Hfid)))
            {
                Group2Hf.Add(World.HistoricalFigures[group2Hfid]);
            }
        }

        protected override void WriteDataOnParent(MainForm frm, Control parent, ref Point location)
        {
            EventLabel(frm, parent, ref location, "Subtype:", SubTypes[Subtype]);
            foreach (var hf in Group1Hf)
                EventLabel(frm, parent, ref location, "Side 1:", hf);
            foreach (var hf in Group2Hf)
                EventLabel(frm, parent, ref location, "Side 2:", hf);
            EventLabel(frm, parent, ref location, "Site:", Site);
            EventLabel(frm, parent, ref location, "Region:", Subregion);

        }

        protected override string LegendsDescription() //Matched
        {
            var timestring = base.LegendsDescription();

            switch (SubTypes[Subtype])
            {
                case "attacked":
                case "ambushed":
                case "surprised":
                    return
                        $"{timestring} the {Group1Hf[0].Race} {Group1Hf[0]} {SubTypes[Subtype]} the {Group2Hf[0].Race} {Group2Hf[0]}.";
                case "corner":
                case "confront":
                    return
                        $"{timestring} the {Group1Hf[0].Race} {Group1Hf[0]} {SubTypes[Subtype]}ed the {Group2Hf[0].Race} {Group2Hf[0]}.";
                case "scuffle":
                    return
                        $"{timestring} the {Group1Hf[0].Race} {Group1Hf[0]} fought with the {Group2Hf[0].Race} {Group2Hf[0]}.";
                case "2 lost after receiving wounds":
                    return
                        $"{timestring} the {Group2Hf[0].Race} {Group2Hf[0]} managed to escape from the {Group1Hf[0].Race} {Group1Hf[0]}'s onslaught.";
                case "2 lost after giving wounds":
                    return
                        $"{timestring} the {Group2Hf[0].Race} {Group2Hf[0]} was forced to retreat from {Group1Hf[0].Race} {Group1Hf[0]} despite the latter's wounds.";
                case "happen upon":
                    return
                        $"{timestring} the {Group1Hf[0].Race} {Group1Hf[0]} happened upon the {Group2Hf[0].Race} {Group2Hf[0]}.";
                case "2 lost after mutual wounds":
                    return
                        $"{timestring} the {Group2Hf[0].Race} {Group2Hf[0]} eventually prevailled and the {Group1Hf[0].Race} {Group1Hf[0]} was forced to make a hasty escape.";
            }

            return timestring;
        }

        internal override string ToTimelineString()
        {
            var timelinestring = base.ToTimelineString();

            if (Group1Hf.Count == Group1Hfid.Count && Group2Hf.Count == Group2Hfid.Count)
                return $"{timelinestring} {Group1Hf[0]} fought {Group2Hf[0]}.";
            return $"{timelinestring} {Group1Hfid[0]} fought {Group2Hfid[0]}.";
        }

        internal override void Export(string table)
        {
            base.Export(table);

            table = GetType().Name;

            var vals = new List<object>
            {
                Id,
                Group1Hfid.DBExport(),
                Group2Hfid.DBExport(),
                Subtype.DBExport(SubTypes),
                SiteId.DBExport(),
                SubregionId.DBExport(),
                FeatureLayerId.DBExport()
            };

            Database.ExportWorldItem(table, vals);
        }

    }
}
