using System;
using System.Collections.Generic;
using System.Xml.Linq;
using DFWV.WorldClasses.HistoricalFigureClasses;

namespace DFWV.WorldClasses.HistoricalEventClasses
{
    class HE_MasterpieceLost : HistoricalEvent
    {
        public int? HFID { get; set; }
        public HistoricalFigure HF { get; set; }
        public int? SiteID { get; set; }
        public Site Site { get; set; }
        public int? Method { get; set; }
        public override IEnumerable<HistoricalFigure> HFsInvolved
        {
            get { yield return HF; }
        }
        public override IEnumerable<Site> SitesInvolved
        {
            get { yield return Site; }
        }


        public HE_MasterpieceLost(XDocument xdoc, World world)
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
                    default:
                        DFXMLParser.UnexpectedXMLElement(xdoc.Root.Name.LocalName + "\t" + Types[Type], element, xdoc.Root.ToString());
                        break;
                }
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
                    case "histfig":
                        HFID = valI;
                        break;
                    case "site":
                        SiteID = valI;
                        break;
                    case "method":
                        Method = valI;
                        break;
                    default:
                        DFXMLParser.UnexpectedXMLElement(xdoc.Root.Name.LocalName + "\t" + Types[Type], element, xdoc.Root.ToString());
                        break;
                }
            }
        }

        internal override void Link()
        {
            //TODO: Incorporate new data
            base.Link();
            if (HFID.HasValue && World.HistoricalFigures.ContainsKey(HFID.Value))
                HF = World.HistoricalFigures[HFID.Value];
            if (SiteID.HasValue && World.Sites.ContainsKey(SiteID.Value))
                Site = World.Sites[SiteID.Value];


        }


        protected override string LegendsDescription() //Matched (add method to write hf and site alt name)
        {
            var timestring = base.LegendsDescription();
            if (HF != null && Site != null)
                return
                    $"{timestring} {HF} destroyed the masterful {"UNKNOWN"} created by {"UNKNOWN"} for {"UNKNOWN"} at {"UNKNOWN"} in {"UNKNOWN"} at {Site.AltName}.";


            return
                $"{timestring} {"UNKNOWN"} destroyed the masterful {"UNKNOWN"} created by {"UNKNOWN"} for {"UNKNOWN"} at {"UNKNOWN"} in {"UNKNOWN"} at {"UNKNOWN"}.";


        }

        internal override string ToTimelineString()
        {
            //TODO: Incorporate new data
            return base.ToTimelineString();
        }

        internal override void Export(string table)
        {
            base.Export(table);

            table = GetType().Name;

            var vals = new List<object>
            {
                ID,
                HFID.DBExport(),
                SiteID.DBExport(),
                Method.DBExport()
            };

            Database.ExportWorldItem(table, vals);
        }
    }
}

