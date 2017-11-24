
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Linq;
using DFWV.WorldClasses.EntityClasses;

namespace DFWV.WorldClasses.HistoricalEventClasses
{
    class HE_InsurrectionStarted : HistoricalEvent
    {
        private int? SiteId { get; }
        public Site Site { get; private set; }
        private int? EntityId { get; }
        public Entity Entity { get; private set; }
        public string Outcome { get; set; }


        override public Point Location => Site.Location;

        public override IEnumerable<Entity> EntitiesInvolved
        {
            get { yield return Entity; }
        }

        public override IEnumerable<Site> SitesInvolved
        {
            get { yield return Site; }
        }

        public HE_InsurrectionStarted(XDocument xdoc, World world)
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
                    case "target_civ_id":
                        EntityId = valI;
                        break;
                    case "site_id":
                        SiteId = valI;
                        break;
                    case "outcome":
                        Outcome = val;
                        if (Outcome != "population gone" && Outcome != "leadership overthrown")
                            Program.Log(LogType.Warning, "Unexpected Insurrection outcome - " + Outcome);
                        break;

                    default:
                        DFXMLParser.UnexpectedXmlElement(xdoc.Root.Name.LocalName + "\t" + Types[Type], element, xdoc.Root.ToString());
                        break;
                }
            }
        }

        protected override void WriteDataOnParent(MainForm frm, Control parent, ref Point location)
        {
            EventLabel(frm, parent, ref location, "Target Civ:", Entity);
            EventLabel(frm, parent, ref location, "Site:", Site);
            EventLabel(frm, parent, ref location, "Outcome:", Outcome);
        }

        protected override string LegendsDescription()
        {
            var timestring = base.LegendsDescription();

            switch (Outcome)
            {
                case "population gone":
                    return
                        $"{timestring} the insurrection in {Site.AltName} against {Entity} ended with the disappearance of hte rebelling population.";
                case "leadership overthrown":
                    return $"{timestring} the insurrection in {Site.AltName} concluded with {Entity} overthrown.";
            }
            return $"{timestring} the insurrection in {Site.AltName} against {Entity} - {Outcome}.";
        }

        internal override string ToTimelineString()
        {
            var timelinestring = base.ToTimelineString();

            return $"{timelinestring} insurrection in {Site.AltName} against {Entity} - {Outcome}.";
        }

        internal override void Export(string table)
        {
            base.Export(table);

            table = GetType().Name;

            var vals = new List<object>
            {
                Id, 
                SiteId.DBExport(), 
                EntityId.DBExport(), 
                Outcome.DBExport()
            };

            Database.ExportWorldItem(table, vals);
        }
    }
}
