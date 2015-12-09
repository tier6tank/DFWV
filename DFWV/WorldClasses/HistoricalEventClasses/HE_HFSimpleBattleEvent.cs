using System;
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
        private List<int> Group1HFID { get; set; }
        public List<HistoricalFigure> Group1HF { get; private set; }
        private List<int> Group2HFID { get; set; }
        private List<HistoricalFigure> Group2HF { get; set; }
        private int? SiteID { get; set; }
        private Site Site { get; set; }
        private int? SubregionID { get; set; }
        private Region Subregion { get; set; }
        private int? FeatureLayerID { get; set; }
        private int Subtype { get; set; }
        public static List<string> SubTypes = new List<string>();

        override public Point Location => Site != null ? Site.Location : (Subregion != null ? Subregion.Location : Point.Empty);

        public override IEnumerable<HistoricalFigure> HFsInvolved
        {
            get
            {
                if (Group1HF != null)
                {
                    foreach (var historicalFigure in Group1HF)
                        yield return historicalFigure;
                }
                if (Group2HF != null)
                {
                    foreach (var historicalFigure in Group2HF)
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
                    case "subtype":
                        if (!SubTypes.Contains(val))
                            SubTypes.Add(val);
                        Subtype = SubTypes.IndexOf(val);
                        break;
                    case "group_1_hfid":
                        if (Group1HFID == null)
                            Group1HFID = new List<int>();
                        Group1HFID.Add(valI);
                        break;
                    case "group_2_hfid":
                        if (Group2HFID == null)
                            Group2HFID = new List<int>();
                        Group2HFID.Add(valI);
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
            if (SiteID.HasValue && World.Sites.ContainsKey(SiteID.Value))
                Site = World.Sites[SiteID.Value];
            if (SubregionID.HasValue && World.Regions.ContainsKey(SubregionID.Value))
                Subregion = World.Regions[SubregionID.Value];
            if (Group1HFID != null)
            {
                Group1HF = new List<HistoricalFigure>();
                foreach (var group1hfid in Group1HFID.Where(group1hfid => World.HistoricalFigures.ContainsKey(group1hfid)))
                {
                    Group1HF.Add(World.HistoricalFigures[group1hfid]);
                }
            }
            if (Group2HFID == null) return;
            Group2HF = new List<HistoricalFigure>();
            foreach (var group2hfid in Group2HFID.Where(group2hfid => World.HistoricalFigures.ContainsKey(group2hfid)))
            {
                Group2HF.Add(World.HistoricalFigures[group2hfid]);
            }
        }

        protected override void WriteDataOnParent(MainForm frm, Control parent, ref Point location)
        {
            EventLabel(frm, parent, ref location, "Subtype:", SubTypes[Subtype]);
            foreach (var hf in Group1HF)
                EventLabel(frm, parent, ref location, "Side 1:", hf);
            foreach (var hf in Group2HF)
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
                        $"{timestring} the {Group1HF[0].Race} {Group1HF[0]} {SubTypes[Subtype]} the {Group2HF[0].Race} {Group2HF[0]}.";
                case "corner":
                case "confront":
                    return
                        $"{timestring} the {Group1HF[0].Race} {Group1HF[0]} {SubTypes[Subtype]}ed the {Group2HF[0].Race} {Group2HF[0]}.";
                case "scuffle":
                    return
                        $"{timestring} the {Group1HF[0].Race} {Group1HF[0]} fought with the {Group2HF[0].Race} {Group2HF[0]}.";
                case "2 lost after receiving wounds":
                    return
                        $"{timestring} the {Group2HF[0].Race} {Group2HF[0]} managed to escape from the {Group1HF[0].Race} {Group1HF[0]}'s onslaught.";
                case "2 lost after giving wounds":
                    return
                        $"{timestring} the {Group2HF[0].Race} {Group2HF[0]} was forced to retreat from {Group1HF[0].Race} {Group1HF[0]} despite the latter's wounds.";
                case "happen upon":
                    return
                        $"{timestring} the {Group1HF[0].Race} {Group1HF[0]} happened upon the {Group2HF[0].Race} {Group2HF[0]}.";
                case "2 lost after mutual wounds":
                    return
                        $"{timestring} the {Group2HF[0].Race} {Group2HF[0]} eventually prevailled and the {Group1HF[0].Race} {Group1HF[0]} was forced to make a hasty escape.";
            }

            return timestring;
        }

        internal override string ToTimelineString()
        {
            var timelinestring = base.ToTimelineString();

            if (Group1HF.Count == Group1HFID.Count && Group2HF.Count == Group2HFID.Count)
                return $"{timelinestring} {Group1HF[0]} fought {Group2HF[0]}.";
            return $"{timelinestring} {Group1HFID[0]} fought {Group2HFID[0]}.";
        }

        internal override void Export(string table)
        {
            base.Export(table);

            table = GetType().Name;

            var vals = new List<object>
            {
                ID,
                Group1HFID.DBExport(),
                Group2HFID.DBExport(),
                Subtype.DBExport(SubTypes),
                SiteID.DBExport(),
                SubregionID.DBExport(),
                FeatureLayerID.DBExport()
            };

            Database.ExportWorldItem(table, vals);
        }

    }
}
