using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Linq;
using DFWV.WorldClasses.EntityClasses;
using DFWV.WorldClasses.HistoricalFigureClasses;

namespace DFWV.WorldClasses.HistoricalEventClasses
{
    class HE_HFDestroyedSite : HistoricalEvent
    {
        private int? SiteId { get; }
        public Site Site { get; private set; }
        private int? HfId { get; }
        public HistoricalFigure Hf { get; private set; }
        private int? EntityId_Defender { get; }
        public Entity Entity_Defender { get; private set; }
        private int? EntityId_SiteCiv { get; }
        public Entity Entity_SiteCiv { get; private set; }

        override public Point Location => Site.Location;

        public override IEnumerable<HistoricalFigure> HFsInvolved
        {
            get { yield return Hf; }
        }
        public override IEnumerable<Entity> EntitiesInvolved
        {
            get
            {
                yield return Entity_Defender;
                yield return Entity_SiteCiv;
            }
        }
        public override IEnumerable<Site> SitesInvolved
        {
            get { yield return Site; }
        }

        public HE_HFDestroyedSite(XDocument xdoc, World world)
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
                    case "attacker_hfid":
                        HfId = valI;
                        break;
                    case "defender_civ_id":
                        EntityId_Defender = valI;
                        break;
                    case "site_civ_id":
                        EntityId_SiteCiv = valI;
                        break;
                    case "site_id":
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
            EventLabel(frm, parent, ref location, "Attacker:", Hf);
            EventLabel(frm, parent, ref location, "Defender:", Entity_Defender);
            EventLabel(frm, parent, ref location, "Old Owner:", Entity_SiteCiv);
            EventLabel(frm, parent, ref location, "Site:", Site);
        }

        protected override string LegendsDescription()
        {
            var timestring = base.LegendsDescription();


            return
                $"{timestring} {Hf.FirstName.ToTitleCase()} routed {Entity_SiteCiv} of {Entity_Defender} and destroyed {Site.AltName}.";
        }

        internal override string ToTimelineString()
        {
            var timelinestring = base.ToTimelineString();

            return $"{timelinestring} {Hf} destroyed {Site.AltName}.";
        }

        internal override void Export(string table)
        {
            base.Export(table);

            table = GetType().Name;

            var vals = new List<object>
            {
                Id, 
                SiteId.DBExport(), 
                EntityId_SiteCiv.DBExport(), 
                EntityId_Defender.DBExport(), 
                HfId.DBExport()
            };

            Database.ExportWorldItem(table, vals);

        }

    }
}
