using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Drawing;
using System.Windows.Forms;
using DFWV.WorldClasses.HistoricalFigureClasses;

namespace DFWV.WorldClasses.HistoricalEventClasses
{
    class HE_HFConfronted : HistoricalEvent
    {
        public int? HFID { get; set; }
        public HistoricalFigure HF { get; set; }
        public int? SiteID { get; set; }
        public Site Site { get; set; }
        public int? SubregionID { get; set; }
        public Region Subregion { get; set; }
        public int? FeatureLayerID { get; set; }
        public Point Coords { get; set; }
        public string Situation { get; set; }
        public string Reason { get; set; }

        override public Point Location { get { return Site.Location; } }

        public HE_HFConfronted(XDocument xdoc, World world)
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
                    case "coords":
                        if (val != "-1,-1")
                            Coords = new Point(Convert.ToInt32(val.Split(',')[0]), Convert.ToInt32(val.Split(',')[1]));
                        break;
                    case "hfid":
                        HFID = valI;
                        break;
                    case "situation":
                        Situation = val;
                        break;
                    case "reason":
                        Reason = val;
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
            if (HFID.HasValue && World.HistoricalFigures.ContainsKey(HFID.Value))
                HF = World.HistoricalFigures[HFID.Value];
            if (SiteID.HasValue && World.Sites.ContainsKey(SiteID.Value))
                Site = World.Sites[SiteID.Value];
            if (SubregionID.HasValue && World.Regions.ContainsKey(SubregionID.Value))
                Subregion = World.Regions[SubregionID.Value];
        }


        internal override void Process()
        {
            base.Process();
            if (HF != null)
            {
                if (HF.Events == null)
                    HF.Events = new List<HistoricalEvent>();
                HF.Events.Add(this);
            }
        }

        public override void WriteDataOnParent(MainForm frm, Control parent, ref Point location)
        {
            EventLabel(frm, parent, ref location, "HF:", HF);
            EventLabel(frm, parent, ref location, "Situation:", Situation);
            EventLabel(frm, parent, ref location, "Reason:", Reason);
            EventLabel(frm, parent, ref location, "Site:", Site);
            EventLabel(frm, parent, ref location, "Region:", Subregion);
            EventLabel(frm, parent, ref location, "Layer:", FeatureLayerID == -1 ? "" : FeatureLayerID.ToString());
            EventLabel(frm, parent, ref location, "Coords:", new Coordinate(Coords));
        }

        public override string LegendsDescription()
        {
            string timestring = base.LegendsDescription();

            if (Reason == "murder" && Situation == "general suspicion")
                return string.Format("{0} {1} aroused general suspicion in {2} after a murder.",
                    timestring, HF.ToString(),
                    Site.AltName);
            else if (Reason == "ageless" && Situation == "general suspicion")
                return string.Format("{0} {1} aroused general suspicion in {2} after appearing not to age.",
                    timestring, HF.ToString(),
                    Site.AltName);
            else
                return timestring;
        }

        internal override string ToTimelineString()
        {
            string timelinestring = base.ToTimelineString();

            if (Reason == "murder" && Situation == "general suspicion")
                return string.Format("{0} {1} aroused general suspicion in {2} after a murder.",
                    timelinestring, HF.ToString(),
                    Site.AltName);
            else if (Reason == "ageless" && Situation == "general suspicion")
                return string.Format("{0} {1} aroused general suspicion in {2} after appearing not to age.",
                    timelinestring, HF.ToString(),
                    Site.AltName);
            else
                return timelinestring;
        }

        internal override void Export(string table)
        {
            base.Export(table);

            
            List<object> vals;
            table = this.GetType().Name.ToString();

            vals = new List<object>() { ID, HFID, Situation, Reason, SiteID, SubregionID, FeatureLayerID };

            if (Coords.IsEmpty)
                vals.Add(DBNull.Value);
            else
                vals.Add(Coords.X + "," + Coords.Y);

            Database.ExportWorldItem(table, vals);

        }

    }
}