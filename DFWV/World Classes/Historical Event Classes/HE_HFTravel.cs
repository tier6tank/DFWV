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
    class HE_HFTravel : HistoricalEvent
    {
        public int? GroupHFID { get; set; }
        public HistoricalFigure GroupHF { get; set; }
        public bool Escape { get; set; }
        public bool Return { get; set; }
        public int? SiteID { get; set; }
        public Site Site { get; set; }
        public int? SubregionID { get; set; }
        public Region Subregion { get; set; }
        public int? FeatureLayerID { get; set; }
        public Point Coords { get; set; }

        override public Point Location { get { return Coords; } }

        public HE_HFTravel(XDocument xdoc, World world)
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
            if (GroupHFID.HasValue && World.HistoricalFigures.ContainsKey(GroupHFID.Value))
                GroupHF = World.HistoricalFigures[GroupHFID.Value];
        }

        internal override void Process()
        {
            base.Process();
            if (GroupHF != null)
            {
                if (GroupHF.Events == null)
                    GroupHF.Events = new List<HistoricalEvent>();
                GroupHF.Events.Add(this);
            }
        }

        public override void WriteDataOnParent(MainForm frm, Control parent, ref Point location)
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
                EventLabel(frm, parent, ref location, "Feature Layer:", FeatureLayerID.Value.ToString());
            }



        }

        public override string LegendsDescription()
        {
            string timestring = base.LegendsDescription();

            if (Escape)
            {
                return string.Format("{0} {1} escaped from the Underworld.",
                    timestring, GroupHF.ToString());

            }
            else if (Return)
            {
                return string.Format("{0} {1} returned to {2}.",
                    timestring, GroupHF.ToString(),
                    Site == null ? "UNKNONW" : Site.AltName);
            }
            else
            {
                return string.Format("{0} {1} made a journey to {2}.",
                    timestring, GroupHF.ToString(),
                    Subregion == null ? "UNKNONW" : Subregion.ToString());
            }
        }

        internal override string ToTimelineString()
        {
            string timelinestring = base.ToTimelineString();

            if (Escape)
                return string.Format("{0} {1} escaped from the Underworld.",
                    timelinestring, GroupHF.ToString());
            else if (Return)
                return string.Format("{0} {1} returned to {2}.",
                    timelinestring, GroupHF.ToString(),
                    Site == null ? "UNKNONW" : Site.AltName);
            else
                return string.Format("{0} {1} made a journey to {2}.",
                    timelinestring, GroupHF.ToString(),
                    Subregion == null ? "UNKNONW" : Subregion.ToString());
        }

        internal override void Export(string table)
        {
            base.Export(table);

            
            List<object> vals;
            table = this.GetType().Name.ToString();


            
            vals = new List<object>() { ID, GroupHFID, Escape, Return, SiteID, SubregionID, FeatureLayerID };


            if (Coords.IsEmpty)
                vals.Add(DBNull.Value);
            else
                vals.Add(Coords.X + "," + Coords.Y);

            Database.ExportWorldItem(table, vals);

        }

    }
}
