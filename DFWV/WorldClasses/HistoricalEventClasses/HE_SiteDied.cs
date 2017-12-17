using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Linq;
using DFWV.WorldClasses.EntityClasses;

namespace DFWV.WorldClasses.HistoricalEventClasses
{
    public class HE_SiteDied : HistoricalEvent
    {
        private int? SiteId { get; }
        private Site Site { get; set; }
        private int? EntityId_SiteCiv { get; }
        public Entity Entity_SiteCiv { get; private set; }
        private int? EntityId_Civ { get; }
        public Entity Entity_Civ { get; private set; }
        public bool Abandoned { get; set; }

        override public Point Location => Site.Location;

        public override IEnumerable<Entity> EntitiesInvolved
        {
            get
            {
                yield return Entity_SiteCiv;
                yield return Entity_Civ;
            }
        }
        public override IEnumerable<Site> SitesInvolved
        {
            get { yield return Site; }
        }


        public HE_SiteDied(XDocument xdoc, World world)
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
                    case "civ_id":
                        EntityId_Civ = valI;
                        break;
                    case "site_civ_id":
                        if (valI != -1)
                            EntityId_SiteCiv = valI;
                        break;
                    case "site_id":
                        SiteId = valI;
                        break;
                    case "abandoned":
                        Abandoned = true;
                        break;
                    default:
                        DFXMLParser.UnexpectedXmlElement(xdoc.Root.Name.LocalName + "\t" + Types[Type], element, xdoc.Root.ToString());
                        break;
                }
            }
        }

        public override void Process()
        {
            base.Process();
            Site.DiedEvent = this;
        }

        protected override void WriteDataOnParent(MainForm frm, Control parent, ref Point location)
        {
            EventLabel(frm, parent, ref location, "Civ:", Entity_Civ);
            EventLabel(frm, parent, ref location, "Owner:", Entity_SiteCiv);
            EventLabel(frm, parent, ref location, "Site:", Site);
            EventLabel(frm, parent, ref location, "Abandoned:", Abandoned ? "Yes": "No" );
        }

        protected override string LegendsDescription()
        {
            var timestring = base.LegendsDescription();

            if (Abandoned)
                return $"{timestring} {Entity_SiteCiv} abandoned the settlement of {Site.AltName}.";
            return $"{timestring} {Entity_SiteCiv} and {Entity_Civ} settlement of {Site.AltName} withered.";
        }

        internal override string ToTimelineString()
        {
            var timelinestring = base.ToTimelineString();

            if (Abandoned)
                return $"{timelinestring} {Entity_SiteCiv} abandoned the settlement of {Site.AltName}.";
            return $"{timelinestring} {Site.AltName} died.";
        }

        internal override void Export(string table)
        {
            base.Export(table);

            table = GetType().Name;

            var vals = new List<object>
            {
                Id, 
                EntityId_Civ.DBExport(), 
                EntityId_SiteCiv.DBExport(), 
                SiteId.DBExport()
            };

            Database.ExportWorldItem(table, vals);
        }
    }
}