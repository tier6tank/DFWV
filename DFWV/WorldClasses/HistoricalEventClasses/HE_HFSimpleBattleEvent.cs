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
        private List<int> HfId_Group1 { get; }
        public List<HistoricalFigure> Hf_Group1 { get; private set; }
        private List<int> HfId_Group2 { get; }
        private List<HistoricalFigure> Hf_Group2 { get; set; }
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
                if (Hf_Group1 != null)
                {
                    foreach (var historicalFigure in Hf_Group1)
                        yield return historicalFigure;
                }
                if (Hf_Group2 != null)
                {
                    foreach (var historicalFigure in Hf_Group2)
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
                        if (HfId_Group1 == null)
                            HfId_Group1 = new List<int>();
                        HfId_Group1.Add(valI);
                        break;
                    case "group_2_hfid":
                        if (HfId_Group2 == null)
                            HfId_Group2 = new List<int>();
                        HfId_Group2.Add(valI);
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
            if (HfId_Group1 != null)
            {
                Hf_Group1 = new List<HistoricalFigure>();
                foreach (var group1Hfid in HfId_Group1.Where(group1Hfid => World.HistoricalFigures.ContainsKey(group1Hfid)))
                {
                    Hf_Group1.Add(World.HistoricalFigures[group1Hfid]);
                }
            }
            if (HfId_Group2 == null) return;
            Hf_Group2 = new List<HistoricalFigure>();
            foreach (var group2Hfid in HfId_Group2.Where(group2Hfid => World.HistoricalFigures.ContainsKey(group2Hfid)))
            {
                Hf_Group2.Add(World.HistoricalFigures[group2Hfid]);
            }
        }

        protected override void WriteDataOnParent(MainForm frm, Control parent, ref Point location)
        {
            EventLabel(frm, parent, ref location, "Subtype:", SubTypes[Subtype]);
            foreach (var hf in Hf_Group1)
                EventLabel(frm, parent, ref location, "Side 1:", hf);
            foreach (var hf in Hf_Group2)
                EventLabel(frm, parent, ref location, "Side 2:", hf);
            EventLabel(frm, parent, ref location, "Site:", Site);
            EventLabel(frm, parent, ref location, "Region:", Subregion);
        }

        protected override string LegendsDescription()
        {
            var timestring = base.LegendsDescription();

            switch (SubTypes[Subtype])
            {
                case "attacked":
                case "ambushed":
                case "surprised":
                    return
                        $"{timestring} the {Hf_Group1[0].Race} {Hf_Group1[0]} {SubTypes[Subtype]} the {Hf_Group2[0].Race} {Hf_Group2[0]}.";
                case "corner":
                case "confront":
                    return
                        $"{timestring} the {Hf_Group1[0].Race} {Hf_Group1[0]} {SubTypes[Subtype]}ed the {Hf_Group2[0].Race} {Hf_Group2[0]}.";
                case "scuffle":
                    return
                        $"{timestring} the {Hf_Group1[0].Race} {Hf_Group1[0]} fought with the {Hf_Group2[0].Race} {Hf_Group2[0]}.";
                case "2 lost after receiving wounds":
                    return
                        $"{timestring} the {Hf_Group2[0].Race} {Hf_Group2[0]} managed to escape from the {Hf_Group1[0].Race} {Hf_Group1[0]}'s onslaught.";
                case "2 lost after giving wounds":
                    return
                        $"{timestring} the {Hf_Group2[0].Race} {Hf_Group2[0]} was forced to retreat from {Hf_Group1[0].Race} {Hf_Group1[0]} despite the latter's wounds.";
                case "happen upon":
                    return
                        $"{timestring} the {Hf_Group1[0].Race} {Hf_Group1[0]} happened upon the {Hf_Group2[0].Race} {Hf_Group2[0]}.";
                case "2 lost after mutual wounds":
                    return
                        $"{timestring} the {Hf_Group2[0].Race} {Hf_Group2[0]} eventually prevailled and the {Hf_Group1[0].Race} {Hf_Group1[0]} was forced to make a hasty escape.";
                default:
                    return $"{timestring} the {Hf_Group2[0].Race} {Hf_Group2[0]} UNKNOWN simple battle event with {Hf_Group1[0].Race} {Hf_Group1[0]}.";

            }

            return timestring;
        }

        internal override string ToTimelineString()
        {
            var timelinestring = base.ToTimelineString();

            if (Hf_Group1.Count == HfId_Group1.Count && Hf_Group2.Count == HfId_Group2.Count)
                return $"{timelinestring} {Hf_Group1[0]} fought {Hf_Group2[0]}.";
            return $"{timelinestring} {HfId_Group1[0]} fought {HfId_Group2[0]}.";
        }

        internal override void Export(string table)
        {
            base.Export(table);

            table = GetType().Name;

            var vals = new List<object>
            {
                Id,
                HfId_Group1.DBExport(),
                HfId_Group2.DBExport(),
                Subtype.DBExport(SubTypes),
                SiteId.DBExport(),
                SubregionId.DBExport(),
                FeatureLayerId.DBExport()
            };

            Database.ExportWorldItem(table, vals);
        }

    }
}
