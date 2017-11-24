using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Linq;
using DFWV.WorldClasses.EntityClasses;

namespace DFWV.WorldClasses.HistoricalEventClasses
{
    //TODO: Missing Details:  UnitID does not associate with anything, "NameOnly" events aren't shown in legends
    public class HE_ArtifactDestroyed : HistoricalEvent
    {
        private int? SiteId { get; }
        public Site Site { get; set; }
        private int? EntityId { get; }
        private Entity Entity { get; set; }
        private int? ArtifactId { get; }
        public Artifact Artifact { get; set; }

        override public Point Location => Site?.Location ?? Point.Empty;

        public override IEnumerable<Entity> EntitiesInvolved
        {
            get { yield return Entity; }
        }
        public override IEnumerable<Site> SitesInvolved
        {
            get { yield return Site; }
        }

        public HE_ArtifactDestroyed(XDocument xdoc, World world)
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
                    case "artifact_id":
                        ArtifactId = valI;
                        break;
                    case "destroyer_enid":
                        EntityId = valI;
                        break;
                    case "site_id":
                        if (valI != -1)
                        SiteId = valI;
                        break;

                    default:
                        DFXMLParser.UnexpectedXmlElement(xdoc.Root.Name.LocalName + "\t" + Types[Type], element, xdoc.Root.ToString());
                        break;
                }
            }
        }

        protected override void WriteDataOnParent(MainForm frm, Control parent, ref Point location)
        {
            EventLabel(frm, parent, ref location, "Artifact:", Artifact);
            EventLabel(frm, parent, ref location, "Site:", Site);
            EventLabel(frm, parent, ref location, "Destroyer:", Entity);
        }

        protected override string LegendsDescription()
        {
            var timestring = base.LegendsDescription();


            return $"{timestring} {Artifact} was destroyed by {Entity} in {Site.AltName}.";
        }

        internal override string ToTimelineString()
        {
            var timelinestring = base.ToTimelineString();

            return $"{timelinestring} {Artifact} was destroyed by {Entity} in {Site.AltName}.";
        }

        internal override void Export(string table)
        {
            base.Export(table);

            table = GetType().Name;

            var vals = new List<object>
            {
                Id, 
                ArtifactId.DBExport(), 
                SiteId.DBExport(), 
                EntityId.DBExport(), 
            };

            Database.ExportWorldItem(table, vals);

        }

    }
}