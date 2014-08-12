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
    class HE_CreatureDevoured : HistoricalEvent
    {
        public int? SiteID { get; set; }
        public Site Site { get; set; }
        public int? SubregionID { get; set; }
        public Region Subregion { get; set; }
        public int? FeatureLayerID { get; set; }
        public HistoricalFigure Devourer { get; set; }

        override public Point Location { get { return Site != null ? Site.Location : Subregion.Location; } }

        public HE_CreatureDevoured(XDocument xdoc, World world)
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
        }

        public override void WriteDataOnParent(MainForm frm, Control parent, ref Point location)
        {
            EventLabel(frm, parent, ref location, "Devourer:", Devourer);
            EventLabel(frm, parent, ref location, "Site:", Site);
            EventLabel(frm, parent, ref location, "Region:", Subregion);
            EventLabel(frm, parent, ref location, "Layer:", FeatureLayerID == -1 ? "" : FeatureLayerID.ToString());

        }

        public override string LegendsDescription()
        {
            string timestring = base.LegendsDescription();

            string location = "in ";
            if (Subregion != null)
                location += Subregion.ToString();
            else
                location += Site.AltName;

            if (Devourer != null)
                return string.Format("{0} the {1} {2} devoured the {3} {4} {5}.",
                    timestring, Devourer.Race.ToString(), Devourer.ToString(), "UNKNOWN", "UNKNOWN",
                    location);
            else
                return string.Format("{0} the {1} {2} devoured the {3} {4} {5}.",
                    timestring, "UNKNOWN", "UNKNOWN", "UNKNOWN", "UNKNOWN",
                    location);

        }

        internal override string ToTimelineString()
        {
            string timelinestring = base.ToTimelineString();

            string location = "in ";
            if (Subregion != null)
                location += Subregion.ToString();
            else
                location += Site.AltName;

            if (Devourer != null)
                return string.Format("{0} {1} devoured someone {2}.",
                    timelinestring, Devourer.ToString(), location);
            else
                return string.Format("{0} Creature devoured {1}.",
                    timelinestring, location);
        }

        internal override void Export(string table)
        {
            base.Export(table);

            
            List<object> vals;
            table = this.GetType().Name.ToString();


            
            vals = new List<object>() { ID, SiteID, SubregionID, FeatureLayerID };

            if (Devourer == null)
                vals.Add(DBNull.Value);
            else
                vals.Add(Devourer.ID);


            Database.ExportWorldItem(table, vals);

        }

    }
}
