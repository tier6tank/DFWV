using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Linq;
using DFWV.WorldClasses.HistoricalFigureClasses;

namespace DFWV.WorldClasses.HistoricalEventClasses
{
    class HE_HFTravel : HistoricalEvent
    {
        private int? HfId { get; }
        private HistoricalFigure Hf { get; set; }
        private bool Escape { get; }
        public bool Return { get; }
        private int? SiteId { get; }
        public Site Site { get; private set; }
        private int? SubregionId { get; }
        private Region Subregion { get; set; }
        private int? FeatureLayerId { get; }
        private Point Coords { get; }

        override public Point Location => Coords;

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

        public HE_HFTravel(XDocument xdoc, World world)
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

                    case "group_hfid":
                        HfId = valI;
                        break;
                    case "return":
                        Return = true;
                        break;
                    case "escape":
                        Escape = true;
                        break;

                    default:
                        DFXMLParser.UnexpectedXmlElement(xdoc.Root.Name.LocalName + "\t" + Types[Type], element, xdoc.Root.ToString());
                        break;
                }
            }
        }

        protected override void WriteDataOnParent(MainForm frm, Control parent, ref Point location)
        {
            EventLabel(frm, parent, ref location, "HF:", Hf);
            if (Escape)
            {
                EventLabel(frm, parent, ref location, "Escaped from", "");
                EventLabel(frm, parent, ref location, "Layer:", FeatureLayerId == -1 ? "" : FeatureLayerId.ToString());
            }
            else if (Return)
            {
                EventLabel(frm, parent, ref location, "Returned", "");
                EventLabel(frm, parent, ref location, "Site:", Site);
                EventLabel(frm, parent, ref location, "Coords:", new Coordinate(Coords));
            }
            else if (Subregion != null)
            {
                EventLabel(frm, parent, ref location, "Region:", Subregion);
            }
            else if (FeatureLayerId != -1)
            {
                if (FeatureLayerId != null)
                    EventLabel(frm, parent, ref location, "Feature Layer:", FeatureLayerId.Value.ToString());
            }



        }

        protected override string LegendsDescription()
        {
            var timestring = base.LegendsDescription();

            if (Escape)
            {
                return $"{timestring} {Hf} escaped from the Underworld.";

            }
            if (Return)
            {
                return $"{timestring} {Hf} returned to {(Site == null ? "UNKNONW" : Site.AltName)}.";
            }
            return $"{timestring} {Hf} made a journey to {Subregion?.ToString() ?? "UNKNONW"}.";
        }

        internal override string ToTimelineString()
        {
            var timelinestring = base.ToTimelineString();

            if (Escape)
                return $"{timelinestring} {Hf} escaped from the Underworld.";
            if (Return)
                return $"{timelinestring} {Hf} returned to {(Site == null ? "UNKNONW" : Site.AltName)}.";
            return
                $"{timelinestring} {Hf} made a journey to {Subregion?.ToString() ?? "UNKNONW"}.";
        }

        internal override void Export(string table)
        {
            base.Export(table);

            table = GetType().Name;
            
            var vals = new List<object>
            {
                Id, 
                HfId.DBExport(), 
                Escape, 
                Return, 
                SiteId.DBExport(), 
                SubregionId.DBExport(), 
                FeatureLayerId.DBExport(),
                Coords.DBExport()
            };

            Database.ExportWorldItem(table, vals);
        }

    }
}
