using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Linq;
using DFWV.WorldClasses.EntityClasses;

namespace DFWV.WorldClasses.HistoricalEventClasses
{
    class HE_SiteRetired : HistoricalEvent
    {
        private int? EntityId_Civ { get; }
        private Entity Entity_Civ { get; set; }
        private int? EntityId_SiteCiv { get; }
        private Entity Entity_SiteCiv { get; set; }
        private int? SiteId { get; }
        private Site Site { get; set; }
        public bool First { get; set; }

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


        public HE_SiteRetired(XDocument xdoc, World world)
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
                        EntityId_SiteCiv = valI;
                        break;
                    case "site_id":
                        SiteId = valI;
                        break;
                    case "first":
                        First = true;
                        break;
                    default:
                        DFXMLParser.UnexpectedXmlElement(xdoc.Root.Name.LocalName + "\t" + Types[Type], element, xdoc.Root.ToString());
                        break;
                }
            }
        }

        protected override void WriteDataOnParent(MainForm frm, Control parent, ref Point location)
        {
            EventLabel(frm, parent, ref location, "Group:", Entity_SiteCiv);
            EventLabel(frm, parent, ref location, "Civ:", Entity_Civ);
            EventLabel(frm, parent, ref location, "Site:", Site);
        }

        protected override string LegendsDescription()
        {
            var timestring = base.LegendsDescription();

            return string.Format(First ? 
                "{0} {1} of {2} at the settlement of {3} regained their senses after an initial period of questionable judgement." : 
                "{0} {1} of {2} abandoned the settlement of {3} regained their senses after another period of questionable judgement.", 
                timestring, Entity_SiteCiv, Entity_Civ, Site.AltName);
        }

        internal override string ToTimelineString()
        {
            var timelinestring = base.ToTimelineString();

            return $"{timelinestring} {Entity_Civ} retired {Site.AltName}.";
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
                SiteId.DBExport(), 
                First
            };

            Database.ExportWorldItem(table, vals);
        }
    }
}
