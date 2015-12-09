using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Linq;
using DFWV.WorldClasses.EntityClasses;
using DFWV.WorldClasses.HistoricalFigureClasses;

namespace DFWV.WorldClasses.HistoricalEventClasses
{
    //TODO: Missing Details:  UnitID does not associate with anything, "NameOnly" events aren't shown in legends
    public class HE_ArtifactDestroyed : HistoricalEvent
    {
        private int? SiteID { get; set; }
        public Site Site { get; set; }
        private int? DestroyerEnID { get; set; }
        private Entity DestroyerEn { get; set; }
        private int? ArtifactID { get; set; }
        public Artifact Artifact { get; set; }

        override public Point Location => Site != null ? Site.Location : Point.Empty;

        public override IEnumerable<Entity> EntitiesInvolved
        {
            get { yield return DestroyerEn; }
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
                        ArtifactID = valI;
                        break;
                    case "destroyer_enid":
                        DestroyerEnID = valI;
                        break;
                    case "site_id":
                        if (valI != -1)
                        SiteID = valI;
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
            if (SiteID.HasValue && World.Sites.ContainsKey(SiteID.Value))
                Site = World.Sites[SiteID.Value];
            if (DestroyerEnID.HasValue && World.Entities.ContainsKey(DestroyerEnID.Value))
                DestroyerEn = World.Entities[DestroyerEnID.Value];
            if (ArtifactID.HasValue && World.Artifacts.ContainsKey(ArtifactID.Value))
                Artifact = World.Artifacts[ArtifactID.Value];
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
                ID, 
                ArtifactID.DBExport(), 
                SiteID.DBExport(), 
                DestroyerEnID.DBExport(), 
            };

            Database.ExportWorldItem(table, vals);

        }

    }
}