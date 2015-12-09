using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;
using DFWV.WorldClasses.HistoricalFigureClasses;

namespace DFWV.WorldClasses.HistoricalEventClasses
{
    class HeHfReunion : HistoricalEvent
    {
        private List<int> Group1Hfid { get; }
        private List<HistoricalFigure> Group1Hf { get; set; }
        private List<int> Group2Hfid { get; }
        private List<HistoricalFigure> Group2Hf { get; set; }
        private int? SiteId { get; }
        private Site Site { get; set; }
        private int? SubregionId { get; }
        private Region Subregion { get; set; }
        private int? FeatureLayerId { get; }

        override public Point Location => Site.Location;

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

        public HeHfReunion(XDocument xdoc, World world)
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
                        DfxmlParser.UnexpectedXmlElement(xdoc.Root.Name.LocalName + "\t" + Types[Type], element, xdoc.Root.ToString());
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
            foreach (var hf in Group1Hf)
                EventLabel(frm, parent, ref location, "Group 1:", hf);
            foreach (var hf in Group2Hf)
                EventLabel(frm, parent, ref location, "Group 2:", hf);

            EventLabel(frm, parent, ref location, "Site:", Site);
            EventLabel(frm, parent, ref location, "Region:", Subregion);
            if (FeatureLayerId != null && FeatureLayerId > -1)
                EventLabel(frm, parent, ref location, "Layer:", FeatureLayerId == -1 ? "" : FeatureLayerId.ToString());
        }

        protected override string LegendsDescription() //Matched
        {
            var timestring = base.LegendsDescription();

            string reunitedText;
            if (Group2Hfid.Count == 2)
            {
                switch (Group2Hf.Count)
                {
                    case 2:
                        reunitedText = Group2Hf[0] + " and " + Group2Hf[1];
                        break;
                    case 0:
                        reunitedText = "an unknown creature and an unknown creature";
                        break;
                    default:
                        if (Group2Hf[0].Id == Group2Hfid[0])
                            reunitedText = Group2Hf[0] + " and an unknown creature";
                        else
                            reunitedText = "an unknown creature" + Group2Hf[0];
                        break;
                }
            }
            else
                reunitedText = Group2Hf.Count == 0 ? "an unknown creature" : Group2Hf[0].ToString();



                return $"{timestring} {Group1Hf[0]} was reunited with {reunitedText} in {Site.AltName}.";
        }

        internal override string ToTimelineString()
        {
            var timelinestring = base.ToTimelineString();

            if (Group2Hf.Count == Group2Hfid.Count && Group1Hf.Count == Group1Hfid.Count)
                return
                    $"{timelinestring} {Group1Hf[0]} was reunited with {(Group2Hf.Count == 2 ? " and " + Group2Hf[1] : "")}{Group2Hf[0]} in {Site.AltName}.";
            return
                $"{timelinestring} {Group1Hfid[0]} was reunited with {(Group2Hf.Count == 2 ? " and " + Group2Hfid[1] : "")}{Group2Hfid[0]} in {Site.AltName}.";
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
                SiteId.DBExport(),
                SubregionId.DBExport(),
                FeatureLayerId.DBExport()
            };

            Database.ExportWorldItem(table, vals);
        }

    }
}