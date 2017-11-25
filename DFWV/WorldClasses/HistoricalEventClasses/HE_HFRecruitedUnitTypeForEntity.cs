using DFWV.WorldClasses.EntityClasses;
using DFWV.WorldClasses.HistoricalFigureClasses;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Linq;

namespace DFWV.WorldClasses.HistoricalEventClasses
{
    internal class HE_HFRecruitedUnitTypeForEntity : HistoricalEvent
    {

        private int? HfId { get; set; }
        public HistoricalFigure Hf { get; set; }
        private int? SiteId { get; }
        private Site Site { get; set; }
        private int? EntityId { get; }
        private Entity Entity { get; set; }
        private int? SubregionId { get; }
        private Region Subregion { get; set; }
        private int? FeatureLayerId { get; }
        public int? UnitType { get; set; }
        public static List<string> UnitTypes = new List<string>();

        override public Point Location => Site.Location;

        public override IEnumerable<HistoricalFigure> HFsInvolved
        {
            get { yield return Hf; }
        }
        public override IEnumerable<Site> SitesInvolved
        {
            get { yield return Site; }
        }

        public HE_HFRecruitedUnitTypeForEntity(XDocument xdoc, World world)
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
                        HfId = valI;
                        break;
                    case "site_id":
                        SiteId = valI;
                        break;
                    case "entity_id":
                        EntityId = valI;
                        break;
                    case "subregion_id":
                        if (valI != -1)
                            SubregionId = valI;
                        break;
                    case "feature_layer_id":
                        if (valI != -1)
                            FeatureLayerId = valI;
                        break;
                    case "unit_type":
                        if (!UnitTypes.Contains(val))
                            UnitTypes.Add(val);
                        UnitType = UnitTypes.IndexOf(val);
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
            EventLabel(frm, parent, ref location, "Site:", Site);
            EventLabel(frm, parent, ref location, "Entity:", Entity);
            EventLabel(frm, parent, ref location, "Subregion:", Subregion);
            if (UnitType.HasValue)
                EventLabel(frm, parent, ref location, "Unit Type:", UnitTypes[UnitType.Value]);

        }

        protected override string LegendsDescription() //Matched
        {
            var timestring = base.LegendsDescription();

            if (UnitType.HasValue)
                return $"{timestring} {(Hf == null ? "UNKNOWN" : Hf.ToString())} recruited {UnitTypes[UnitType.Value]} into {Entity} in {Site.AltName}.";
            return $"{timestring} {(Hf == null ? "UNKNOWN" : Hf.ToString())} recruited {UnitTypes[UnitType.Value]} into {Entity} in {Site.AltName}.";
        }

        internal override string ToTimelineString()
        {
            var timelinestring = base.ToTimelineString();

            return timelinestring;
        }
    }
}