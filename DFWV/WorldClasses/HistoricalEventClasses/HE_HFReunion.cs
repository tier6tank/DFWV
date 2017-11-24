using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;
using DFWV.WorldClasses.HistoricalFigureClasses;

namespace DFWV.WorldClasses.HistoricalEventClasses
{
    class HE_HFReunion : HistoricalEvent
    {
        private List<int> HfId_Group1 { get; }
        private List<HistoricalFigure> Hf_Group1 { get; set; }
        private List<int> HfId_Group2 { get; }
        private List<HistoricalFigure> Hf_Group2 { get; set; }
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

        public HE_HFReunion(XDocument xdoc, World world)
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
            foreach (var hf in Hf_Group1)
                EventLabel(frm, parent, ref location, "Group 1:", hf);
            foreach (var hf in Hf_Group2)
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
            if (HfId_Group2.Count == 2)
            {
                switch (Hf_Group2.Count)
                {
                    case 2:
                        reunitedText = Hf_Group2[0] + " and " + Hf_Group2[1];
                        break;
                    case 0:
                        reunitedText = "an unknown creature and an unknown creature";
                        break;
                    default:
                        if (Hf_Group2[0].Id == HfId_Group2[0])
                            reunitedText = Hf_Group2[0] + " and an unknown creature";
                        else
                            reunitedText = "an unknown creature" + Hf_Group2[0];
                        break;
                }
            }
            else
                reunitedText = Hf_Group2.Count == 0 ? "an unknown creature" : Hf_Group2[0].ToString();



                return $"{timestring} {Hf_Group1[0]} was reunited with {reunitedText} in {Site.AltName}.";
        }

        internal override string ToTimelineString()
        {
            var timelinestring = base.ToTimelineString();

            if (Hf_Group2.Count == HfId_Group2.Count && Hf_Group1.Count == HfId_Group1.Count)
                return
                    $"{timelinestring} {Hf_Group1[0]} was reunited with {(Hf_Group2.Count == 2 ? " and " + Hf_Group2[1] : "")}{Hf_Group2[0]} in {Site.AltName}.";
            return
                $"{timelinestring} {HfId_Group1[0]} was reunited with {(Hf_Group2.Count == 2 ? " and " + HfId_Group2[1] : "")}{HfId_Group2[0]} in {Site.AltName}.";
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
                SiteId.DBExport(),
                SubregionId.DBExport(),
                FeatureLayerId.DBExport()
            };

            Database.ExportWorldItem(table, vals);
        }

    }
}