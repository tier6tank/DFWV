using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Linq;
using DFWV.WorldClasses.EntityClasses;

namespace DFWV.WorldClasses.HistoricalEventClasses
{
    class HE_FirstContactFailed : HistoricalEvent
    {
        private int? SiteId { get; }
        private Site Site { get; set; }
        private int? ContactorEnId { get; }
        private Entity ContactorEn { get; set; }
        private int? RejectorEnId { get; }
        private Entity RejectorEn { get; set; }

        override public Point Location => Site.Location;

        public override IEnumerable<Entity> EntitiesInvolved
        {
            get
            {
                yield return ContactorEn;
                yield return RejectorEn;
            }
        }
        public override IEnumerable<Site> SitesInvolved
        {
            get { yield return Site; }
        }

        public HE_FirstContactFailed(XDocument xdoc, World world)
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
                    case "contactor_enid":
                        ContactorEnId = valI;
                        break;
                    case "rejector_enid":
                        RejectorEnId = valI;
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

        internal override void Link()
        {
            base.Link();
            if (SiteId.HasValue && World.Sites.ContainsKey(SiteId.Value))
                Site = World.Sites[SiteId.Value];
            if (ContactorEnId.HasValue && World.Entities.ContainsKey(ContactorEnId.Value))
                ContactorEn = World.Entities[ContactorEnId.Value];
            if (RejectorEnId.HasValue && World.Entities.ContainsKey(RejectorEnId.Value))
                RejectorEn = World.Entities[RejectorEnId.Value];
        }


        protected override void WriteDataOnParent(MainForm frm, Control parent, ref Point location)
        {
            EventLabel(frm, parent, ref location, "Contactor:", ContactorEn);
            EventLabel(frm, parent, ref location, "Rejecter:", RejectorEn);
            EventLabel(frm, parent, ref location, "Site:", Site);
        }

        protected override string LegendsDescription()
        {
            var timestring = base.LegendsDescription();

            return $"{timestring} {RejectorEn} rejected contact with {ContactorEn} at {Site.AltName}.";
        }

        internal override string ToTimelineString()
        {
            var timelinestring = base.ToTimelineString();

            return $"{timelinestring} {RejectorEn} rejected contact with {ContactorEn} at {Site.AltName}.";
        }

        internal override void Export(string table)
        {
            base.Export(table);

            table = GetType().Name;
            
            var vals = new List<object>
            {
                Id, 
                SiteId.DBExport(), 
                ContactorEnId.DBExport(), 
                RejectorEnId.DBExport()
            };

            Database.ExportWorldItem(table, vals);

        }


    }
}

