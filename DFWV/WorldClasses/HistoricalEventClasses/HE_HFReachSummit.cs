using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;
using DFWV.WorldClasses.HistoricalFigureClasses;

namespace DFWV.WorldClasses.HistoricalEventClasses
{
    class HE_HFReachSummit : HistoricalEvent
    {

        private int? SubregionId { get; }
        private Region Subregion { get; set; }
        private int? FeatureLayerId { get; }
        private Point Coords { get; }
        private List<int> GroupHfid { get; }
        private List<HistoricalFigure> GroupHf { get; set; }

        override public Point Location => Coords != Point.Empty ? Coords : Subregion.Location;

        public override IEnumerable<HistoricalFigure> HFsInvolved => GroupHf ?? Enumerable.Empty<HistoricalFigure>();

        public override IEnumerable<Region> RegionsInvolved
        {
            get { yield return Subregion; }
        }

        public HE_HFReachSummit(XDocument xdoc, World world)
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
                    case "group_hfid":
                        if (GroupHfid == null)
                            GroupHfid = new List<int>();
                        GroupHfid.Add(valI);
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

            if (SubregionId.HasValue && World.Regions.ContainsKey(SubregionId.Value))
                Subregion = World.Regions[SubregionId.Value];

            if (GroupHfid == null) return;
            GroupHf = new List<HistoricalFigure>();
            foreach (var group1Hfid in GroupHfid.Where(group1Hfid => World.HistoricalFigures.ContainsKey(group1Hfid)))
            {
                GroupHf.Add(World.HistoricalFigures[group1Hfid]);
            }
        }

        internal override void Plus(XDocument xdoc)
        {
            foreach (var element in xdoc.Root.Elements())
            {
                var val = element.Value;
                int valI;
                int.TryParse(val, out valI);

                switch (element.Name.LocalName)
                {
                    case "id":
                    case "type":
                        break;
                    case "figures":
                        if (!GroupHfid.Contains(valI))
                            GroupHfid.Add(valI);
                        break;
                    default:
                        DFXMLParser.UnexpectedXmlElement(xdoc.Root.Name.LocalName + "\t" + Types[Type], element, xdoc.Root.ToString());
                        break;
                }
            }
        }

        protected override void WriteDataOnParent(MainForm frm, Control parent, ref Point location)
        {
            if (GroupHf!= null)
            {
                foreach (var hf in GroupHf)
                    EventLabel(frm, parent, ref location, "HF:", hf);
            }

            if (Subregion != null)
            {
                EventLabel(frm, parent, ref location, "Region:", Subregion);
            }
            if (Coords != Point.Empty)
            {
                EventLabel(frm, parent, ref location, "Coords:", new Coordinate(Coords));
            }

        }

        protected override string LegendsDescription()
        {
            var timestring = base.LegendsDescription();

            if (GroupHf == null)
                return "";
            if (Subregion != null)
            {
                if (GroupHf.Count == 1)
                    return
                        $"{timestring} {GroupHf[0].Name} was the first to reach the summit of {"UNKNOWN"}, which rises above {Subregion.Name}.";
                return
                    $"{timestring} {GroupHf[0].Name} and {GroupHf[1].Name} were the first to reach the summit of {"UNKNOWN"}, which rises above {Subregion.Name}.";
            }
            if (GroupHf.Count == 1)
                return
                    $"{timestring} {GroupHf[0].Name} was the first to reach the summit of {"UNKNOWN"}, which rises above {"UNKNOWN"}.";
            return
                $"{timestring} {GroupHf[0].Name} and {GroupHf[1].Name} were the first to reach the summit of {"UNKNOWN"}, which rises above {"UNKNOWN"}.";
        }

        internal override string ToTimelineString()
        {
            var timelinestring = base.ToTimelineString();

            if (GroupHf == null)
                return "";
            if (Subregion != null)
            {
                if (GroupHf.Count == 1)
                    return
                        $"{timelinestring} {GroupHf[0].Name} was the first to reach a summit, which rises above {Subregion.Name}.";
                return
                    $"{timelinestring} {GroupHf[0].Name} and {GroupHf[1].Name} were the first to reach a summit, which rises above {Subregion.Name}.";
            }
            if (GroupHf.Count == 1)
                return $"{timelinestring} {GroupHf[0].Name} was the first to reach a summit.";
            return $"{timelinestring} {GroupHf[0].Name} and {GroupHf[1].Name} were the first to reach a summit.";
        }

        internal override void Export(string table)
        {
            base.Export(table);

            table = GetType().Name;

            var vals = new List<object>
            {
                Id,
                GroupHfid.DBExport(),
                SubregionId.DBExport(),
                FeatureLayerId.DBExport(),
                Coords.DBExport()
            };

            Database.ExportWorldItem(table, vals);
        }

    }
}