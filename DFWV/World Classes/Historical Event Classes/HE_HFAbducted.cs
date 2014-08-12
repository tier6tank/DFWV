using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;
using DFWV.WorldClasses.HistoricalFigureClasses;

namespace DFWV.WorldClasses.HistoricalEventClasses
{
    class HE_HFAbducted : HistoricalEvent
    {
        public int? TargetHFID { get; set; }
        public HistoricalFigure TargetHF { get; set; }
        public int? SnatcherHFID { get; set; }
        public HistoricalFigure SnatcherHF { get; set; }
        public int? SiteID { get; set; }
        public Site Site { get; set; }
        public int? SubregionID { get; set; }
        public Region Subregion { get; set; }
        public int? FeatureLayerID { get; set; }

        override public Point Location { get { return Site.Location; } }

        public HE_HFAbducted(XDocument xdoc, World world)
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
                    case "target_hfid":
                        TargetHFID = valI;
                        break;
                    case "snatcher_hfid":
                        SnatcherHFID = valI;
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
            if (TargetHFID.HasValue && World.HistoricalFigures.ContainsKey(TargetHFID.Value))
                TargetHF = World.HistoricalFigures[TargetHFID.Value];
            if (SnatcherHFID.HasValue && World.HistoricalFigures.ContainsKey(SnatcherHFID.Value))
                SnatcherHF = World.HistoricalFigures[SnatcherHFID.Value];
        }

        internal override void Process()
        {
            base.Process();
            if (TargetHF != null)
            {
                if (TargetHF.Events == null)
                    TargetHF.Events = new List<HistoricalEvent>();
                TargetHF.Events.Add(this);
            }
            if (SnatcherHF != null)
            {
                if (SnatcherHF.Events == null)
                    SnatcherHF.Events = new List<HistoricalEvent>();
                SnatcherHF.Events.Add(this);
            }
        }

        public override void WriteDataOnParent(MainForm frm, Control parent, ref Point location)
        {
            EventLabel(frm, parent, ref location, "Target:", TargetHF);
            EventLabel(frm, parent, ref location, "Snatcher:", SnatcherHF);
            EventLabel(frm, parent, ref location, "Site:", Site);
            EventLabel(frm, parent, ref location, "Region:", Subregion);

        }

        public override string LegendsDescription()
        {
            string timestring = base.LegendsDescription();

            return string.Format("{0} {1} was abducted from {2} by {3}.",
                                    timestring, TargetHF.ToString(), Site.AltName,
                                    SnatcherHF.ToString());
        }

        internal override string ToTimelineString()
        {
            string timelinestring = base.ToTimelineString();

            return string.Format("{0} {1} was abducted from {2} by {3}.",
                                timelinestring, TargetHF != null ? TargetHF.ToString() : TargetHFID.ToString(), Site.AltName,
                                    SnatcherHF != null ? SnatcherHF.ToString() : SnatcherHFID.ToString());
        }

        internal override void Export(string table)
        {
            base.Export(table);

            
            List<object> vals;
            table = this.GetType().Name.ToString();


            
            vals = new List<object>() { ID, TargetHFID, SnatcherHFID, SiteID, SubregionID, FeatureLayerID };


            Database.ExportWorldItem(table, vals);

        }

    }
}

