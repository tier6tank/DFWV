using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Linq;
using DFWV.WorldClasses.EntityClasses;

namespace DFWV.WorldClasses.HistoricalEventClasses
{
    //TODO: Verify new event
    class HE_SiteDispute : HistoricalEvent
    {
        public string Dispute { get; set; }
        public int? EntityId_1 { get; set; }
        public Entity Entity_1 { get; set; }
        public int? EntityId_2 { get; set; }
        public Entity Entity_2 { get; set; }
        public int? SiteId_1 { get; set; }
        public Site Site_1 { get; set; }
        public int? SiteId_2 { get; set; }
        public Site Site_2 { get; set; }
        public override IEnumerable<Entity> EntitiesInvolved
        {
            get
            {
                yield return Entity_1;
                yield return Entity_2;
            }
        }
        public override IEnumerable<Site> SitesInvolved
        {
            get
            {
                yield return Site_1;
                yield return Site_2;
            }
        }


        public HE_SiteDispute(XDocument xdoc, World world)
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

                    case "dispute":
                        Dispute = val;
                        break;
                    case "entity_id_1":
                        EntityId_1 = valI;
                        break;
                    case "entity_id_2":
                        EntityId_2 = valI;
                        break;
                    case "site_id_1":
                        SiteId_1 = valI;
                        break;
                    case "site_id_2":
                        SiteId_2 = valI;
                        break;


                    default:
                        DFXMLParser.UnexpectedXmlElement(xdoc.Root.Name.LocalName + "\t" + Types[Type], element, xdoc.Root.ToString());
                        break;
                }
            }
        }

        protected override void WriteDataOnParent(MainForm frm, Control parent, ref Point location)
        {
            EventLabel(frm, parent, ref location, "Dispute:", Dispute);
            EventLabel(frm, parent, ref location, "Entity 1:", Entity_1);
            EventLabel(frm, parent, ref location, "Entity 2:", Entity_2);
            EventLabel(frm, parent, ref location, "Site 1:", Site_1);
            EventLabel(frm, parent, ref location, "Site 2:", Site_2);
        }
        

        protected override string LegendsDescription()
        {
            var timestring = base.LegendsDescription();

            return
                $"{timestring} {Entity_1} of {Site_1.AltName} and {Entity_2} of {Site_2.AltName} became embroiled in a dispute over {Dispute}.";
        }

        internal override string ToTimelineString()
        {
            var timelinestring = base.ToTimelineString();

            return $"{timelinestring} {Entity_1} and {Entity_2} dispute {Dispute}.";
        }

        internal override void Export(string table)
        {
            base.Export(table);

            table = GetType().Name;

            var vals = new List<object>
            {
                Id, 
                EntityId_1.DBExport(), 
                EntityId_2.DBExport(), 
                SiteId_1.DBExport(), 
                SiteId_2.DBExport(), 
                Dispute.DBExport()
            };

            Database.ExportWorldItem(table, vals);
        }
    }
}