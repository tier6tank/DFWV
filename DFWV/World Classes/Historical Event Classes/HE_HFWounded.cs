﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;
using DFWV.WorldClasses.HistoricalFigureClasses;

namespace DFWV.WorldClasses.HistoricalEventClasses
{
    class HE_HFWounded : HistoricalEvent
    {
        public int? WoundeeHFID { get; set; }
        public HistoricalFigure WoundeeHF { get; set; }
        public int? WounderHFID { get; set; }
        public HistoricalFigure WounderHF { get; set; }
        public int? SiteID { get; set; }
        public Site Site { get; set; }
        public int? SubregionID { get; set; }
        public Region Subregion { get; set; }
        public int? FeatureLayerID { get; set; }

        override public Point Location { get { return Site != null ? Site.Location : (Subregion != null ? Subregion.Location : Point.Empty); } }

        public HE_HFWounded(XDocument xdoc, World world)
            : base(xdoc, world)
        {
            foreach (XElement element in xdoc.Root.Elements())
            {
                string val = element.Value.ToString();
                int valI;
                Int32.TryParse(val, out valI);

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
                    case "woundee_hfid":
                        WoundeeHFID = valI;
                        break;
                    case "wounder_hfid":
                        WounderHFID = valI;
                        break;
                    default:
                        DFXMLParser.UnexpectedXMLElement(xdoc.Root.Name.LocalName + "\t" + HistoricalEvent.Types[Type], element, xdoc.Root.ToString());
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
            if (WoundeeHFID.HasValue && World.HistoricalFigures.ContainsKey(WoundeeHFID.Value))
                WoundeeHF = World.HistoricalFigures[WoundeeHFID.Value];
            if (WounderHFID.HasValue && World.HistoricalFigures.ContainsKey(WounderHFID.Value))
                WounderHF = World.HistoricalFigures[WounderHFID.Value];
        }

        internal override void Process()
        {
            base.Process();
            if (WoundeeHF != null)
            {
                if (WoundeeHF.Events == null)
                    WoundeeHF.Events = new List<HistoricalEvent>();
                WoundeeHF.Events.Add(this);
            }
            if (WounderHF != null)
            {
                if (WounderHF.Events == null)
                    WounderHF.Events = new List<HistoricalEvent>();
                WounderHF.Events.Add(this);
            }
        }

        public override void WriteDataOnParent(MainForm frm, Control parent, ref Point location)
        {
            EventLabel(frm, parent, ref location, "HF:", WoundeeHF);
            EventLabel(frm, parent, ref location, "By:", WounderHF);
            EventLabel(frm, parent, ref location, "Site:", Site);
            EventLabel(frm, parent, ref location, "Region:", Subregion);

        }

        public override string LegendsDescription()
        {
            string timestring = base.LegendsDescription();

            return string.Format("{0} the {1} {2} was wounded by the {3} {4}.",
                            timestring, WoundeeHF.Race.ToString(), WoundeeHF.ToString(),
                            WounderHF.Race.ToString(), WounderHF.ToString());
        }

        internal override string ToTimelineString()
        {
            string timelinestring = base.ToTimelineString();

            return string.Format("{0} {1} was wounded by the {2}.",
                timelinestring,  WoundeeHF != null ? WoundeeHF.ToString() : WoundeeHFID.ToString(), WounderHF != null ? WounderHF.ToString() : WounderHFID.ToString());
        }        

        internal override void Export(string table)
        {
            base.Export(table);

            
            List<object> vals;
            table = this.GetType().Name.ToString();


            
            vals = new List<object>() { ID, WoundeeHFID, WounderHFID, SiteID, SubregionID, FeatureLayerID };


            Database.ExportWorldItem(table, vals);

        }

    }
}
