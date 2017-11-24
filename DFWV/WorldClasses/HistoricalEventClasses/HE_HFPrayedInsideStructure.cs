using DFWV.WorldClasses.HistoricalFigureClasses;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Linq;

namespace DFWV.WorldClasses.HistoricalEventClasses
{
    internal class HE_HFPrayedInsideStructure : HistoricalEvent
    {
        private int? HfId { get; set; }
        public HistoricalFigure Hf { get; set; }
        private int? StructureId { get; set; }
        public Structure Structure { get; set; }
        private int? SiteId { get; }
        private Site Site { get; set; }

        override public Point Location => Site.Location;

        public override IEnumerable<HistoricalFigure> HFsInvolved
        {
            get { yield return Hf; }
        }
        public override IEnumerable<Site> SitesInvolved
        {
            get { yield return Site; }
        }

        public HE_HFPrayedInsideStructure(XDocument xdoc, World world)
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
                    case "hist_fig_id":
                        HfId = valI;
                        break;
                    case "site_id":
                        SiteId = valI;
                        break;
                    case "structure_id":
                        StructureId = valI;
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

        }

        protected override string LegendsDescription() //Matched
        {
            var timestring = base.LegendsDescription();


            return timestring;
        }

        internal override string ToTimelineString()
        {
            var timelinestring = base.ToTimelineString();

            return timelinestring;
        }
    }
}