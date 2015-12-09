﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;
using DFWV.WorldClasses.HistoricalFigureClasses;

namespace DFWV.WorldClasses.HistoricalEventClasses
{
    public class HE_ChangeHFBodyState : HistoricalEvent
    {
        
        private int? SiteID { get; set; }
        private Site Site { get; set; }
        private int? SubregionID { get; set; }
        private Region Subregion { get; set; }
        private int? HFID { get; set; }
        public HistoricalFigure HF { get; set; }
        private int? FeatureLayerID { get; set; }
        private Point Coords { get; set; }
        private int? BuildingID { get; set; }
        private Structure Structure { get; set; }
        public string BodyState { get; set; }

        override public Point Location => Coords;

        public override IEnumerable<HistoricalFigure> HFsInvolved
        {
            get { yield return HF; }
        }
        public override IEnumerable<Site> SitesInvolved
        {
            get { yield return Site; }
        }

        public HE_ChangeHFBodyState(XDocument xdoc, World world)
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
                    case "hfid":
                        HFID = valI;
                        break;
                    case "body_state":
                        BodyState = val;
                        break;
                    case "site_id":
                        SiteID = valI;
                        break;
                    case "building_id":
                        BuildingID = valI;
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


                    default:
                        DFXMLParser.UnexpectedXMLElement(xdoc.Root.Name.LocalName + "\t" + Types[Type], element, xdoc.Root.ToString());
                        break;
                }
            }
        }

        internal override void Link()
        {
            base.Link();
            if (HFID.HasValue && World.HistoricalFigures.ContainsKey(HFID.Value))
                HF = World.HistoricalFigures[HFID.Value];
            if (SiteID.HasValue && World.Sites.ContainsKey(SiteID.Value))
                Site = World.Sites[SiteID.Value];
            if (SubregionID.HasValue && World.Regions.ContainsKey(SubregionID.Value))
                Subregion = World.Regions[SubregionID.Value];

            if (!BuildingID.HasValue || Site.Structures == null) return;
            foreach (var structure in Site.Structures.Where(structure => structure.SiteID == BuildingID))
            {
                Structure = structure;
                if (Structure.Events == null)
                    Structure.Events = new List<HistoricalEvent>();
                Structure.Events.Add(this);
                break;
            }
        }


        internal override void Process()
        {
            base.Process();


            if (BodyState == "entombed at site")
            {
                HF.EntombedEvent = this;
            }
        }


        protected override void WriteDataOnParent(MainForm frm, Control parent, ref Point location)
        {
            EventLabel(frm, parent, ref location, "HF:", HF);
            EventLabel(frm, parent, ref location, "State:", BodyState);
            EventLabel(frm, parent, ref location, "Site:", Site);
            if (Structure != null)
                EventLabel(frm, parent, ref location, "Structure:", Structure);
            else
                EventLabel(frm, parent, ref location, "Building ID:", BuildingID.ToString());
            EventLabel(frm, parent, ref location, "Region:", Subregion);
            EventLabel(frm, parent, ref location, "Layer:", FeatureLayerID == -1 ? "" : FeatureLayerID.ToString());
            EventLabel(frm, parent, ref location, "Coords:", new Coordinate(Coords));
        }

        protected override string LegendsDescription() //Matched
        {
            var timestring = base.LegendsDescription();

            return $"{timestring} {HF} was entombed in {Site.AltName} within {Structure}.";
        }

        internal override string ToTimelineString()
        {
            var timelinestring = base.ToTimelineString();

            return $"{timelinestring} {HF} was entombed at {Site.AltName}.";
        }

        internal override void Export(string table)
        {
            base.Export(table);


            table = GetType().Name;


            var vals = new List<object>
            {
                ID, 
                HFID.DBExport(), 
                BodyState.DBExport(), 
                BuildingID.DBExport(), 
                SiteID.DBExport(),  
                SubregionID.DBExport(), 
                FeatureLayerID.DBExport(),
                Coords.DBExport()
            };

            Database.ExportWorldItem(table, vals);

        }

    }
}
