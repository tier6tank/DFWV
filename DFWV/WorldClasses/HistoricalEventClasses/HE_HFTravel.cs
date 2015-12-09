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
        private int? GroupHFID { get; set; }
        private HistoricalFigure GroupHF { get; set; }
        private bool Escape { get; set; }
        public bool Return { get; private set; }
        private int? SiteID { get; set; }
        public Site Site { get; private set; }
        private int? SubregionID { get; set; }
        private Region Subregion { get; set; }
        private int? FeatureLayerID { get; set; }
        private Point Coords { get; set; }

        override public Point Location => Coords;

        public override IEnumerable<HistoricalFigure> HFsInvolved
        {
            get { yield return GroupHF; }
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
                    case "coords":
                        if (val != "-1,-1")
                            Coords = new Point(Convert.ToInt32(val.Split(',')[0]), Convert.ToInt32(val.Split(',')[1]));
                        break;

                    case "group_hfid":
                        GroupHFID = valI;
                        break;
                    case "return":
                        Return = true;
                        break;
                    case "escape":
                        Escape = true;
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
            if (GroupHFID.HasValue && World.HistoricalFigures.ContainsKey(GroupHFID.Value))
                GroupHF = World.HistoricalFigures[GroupHFID.Value];
        }

        protected override void WriteDataOnParent(MainForm frm, Control parent, ref Point location)
        {
            EventLabel(frm, parent, ref location, "HF:", GroupHF);
            if (Escape)
            {
                EventLabel(frm, parent, ref location, "Escaped from", "");
                EventLabel(frm, parent, ref location, "Layer:", FeatureLayerID == -1 ? "" : FeatureLayerID.ToString());
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
            else if (FeatureLayerID != -1)
            {
                if (FeatureLayerID != null)
                    EventLabel(frm, parent, ref location, "Feature Layer:", FeatureLayerID.Value.ToString());
            }



        }

        protected override string LegendsDescription()
        {
            var timestring = base.LegendsDescription();

            if (Escape)
            {
                return $"{timestring} {GroupHF} escaped from the Underworld.";

            }
            if (Return)
            {
                return $"{timestring} {GroupHF} returned to {(Site == null ? "UNKNONW" : Site.AltName)}.";
            }
            return $"{timestring} {GroupHF} made a journey to {(Subregion == null ? "UNKNONW" : Subregion.ToString())}.";
        }

        internal override string ToTimelineString()
        {
            var timelinestring = base.ToTimelineString();

            if (Escape)
                return $"{timelinestring} {GroupHF} escaped from the Underworld.";
            if (Return)
                return $"{timelinestring} {GroupHF} returned to {(Site == null ? "UNKNONW" : Site.AltName)}.";
            return
                $"{timelinestring} {GroupHF} made a journey to {(Subregion == null ? "UNKNONW" : Subregion.ToString())}.";
        }

        internal override void Export(string table)
        {
            base.Export(table);

            table = GetType().Name;
            
            var vals = new List<object>
            {
                ID, 
                GroupHFID.DBExport(), 
                Escape, 
                Return, 
                SiteID.DBExport(), 
                SubregionID.DBExport(), 
                FeatureLayerID.DBExport(),
                Coords.DBExport()
            };

            Database.ExportWorldItem(table, vals);
        }

    }
}
