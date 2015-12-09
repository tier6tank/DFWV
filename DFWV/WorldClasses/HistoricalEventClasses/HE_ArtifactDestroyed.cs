using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Linq;
using DFWV.WorldClasses.EntityClasses;

namespace DFWV.WorldClasses.HistoricalEventClasses
{
    //TODO: Missing Details:  UnitID does not associate with anything, "NameOnly" events aren't shown in legends
    public class HeArtifactDestroyed : HistoricalEvent
    {
        private int? SiteId { get; }
        public Site Site { get; set; }
        private int? DestroyerEnId { get; }
        private Entity DestroyerEn { get; set; }
        private int? ArtifactId { get; }
        public Artifact Artifact { get; set; }

        override public Point Location => Site?.Location ?? Point.Empty;

        public override IEnumerable<Entity> EntitiesInvolved
        {
            get { yield return DestroyerEn; }
        }
        public override IEnumerable<Site> SitesInvolved
        {
            get { yield return Site; }
        }

        public HeArtifactDestroyed(XDocument xdoc, World world)
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
                        DestroyerEnId = valI;
                        break;
                    case "site_id":
                        if (valI != -1)
                        SiteId = valI;
                        break;

                    default:
                        DfxmlParser.UnexpectedXmlElement(xdoc.Root.Name.LocalName + "\t" + Types[Type], element, xdoc.Root.ToString());
                        break;
                }
            }
        }

        internal override void Link()
        {
            base.Link();
            if (SiteId.HasValue && World.Sites.ContainsKey(SiteId.Value))
                Site = World.Sites[SiteId.Value];
            if (DestroyerEnId.HasValue && World.Entities.ContainsKey(DestroyerEnId.Value))
                DestroyerEn = World.Entities[DestroyerEnId.Value];
            if (ArtifactId.HasValue && World.Artifacts.ContainsKey(ArtifactId.Value))
                Artifact = World.Artifacts[ArtifactId.Value];
            //if (UnitID.HasValue && World.HistoricalFigures.ContainsKey(UnitID.Value))
            //    Unit = World.HistoricalFigures[UnitID.Value];
        }


        protected override void WriteDataOnParent(MainForm frm, Control parent, ref Point location)
        {
            EventLabel(frm, parent, ref location, "Artifact:", Artifact);
            EventLabel(frm, parent, ref location, "Site:", Site);
            EventLabel(frm, parent, ref location, "Destroyer:", DestroyerEn);
        }

        protected override string LegendsDescription()
        {
            var timestring = base.LegendsDescription();


            return $"{timestring} {Artifact} was destroyed by {DestroyerEn} in {Site.AltName}.";
        }

        internal override string ToTimelineString()
        {
            var timelinestring = base.ToTimelineString();

            return $"{timelinestring} {Artifact} was destroyed by {DestroyerEn} in {Site.AltName}.";
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
                DestroyerEnId.DBExport(), 
            };

            Database.ExportWorldItem(table, vals);

        }

    }
}