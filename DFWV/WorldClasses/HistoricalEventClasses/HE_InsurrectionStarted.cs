
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
        private int? TargetCivId { get; }
        public Entity TargetCiv { get; private set; }
        public string Outcome { get; set; }


        override public Point Location => Site.Location;

        public override IEnumerable<Entity> EntitiesInvolved
        {
            get { yield return TargetCiv; }
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
                        TargetCivId = valI;
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

        internal override void Link()
        {
            base.Link();
            if (SiteId.HasValue && World.Sites.ContainsKey(SiteId.Value))
                Site = World.Sites[SiteId.Value];
            if (TargetCivId.HasValue && World.Entities.ContainsKey(TargetCivId.Value))
                TargetCiv = World.Entities[TargetCivId.Value];
        }


        protected override void WriteDataOnParent(MainForm frm, Control parent, ref Point location)
        {
            EventLabel(frm, parent, ref location, "Target Civ:", TargetCiv);
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
                        $"{timestring} the insurrection in {Site.AltName} against {TargetCiv} ended with the disappearance of hte rebelling population.";
                case "leadership overthrown":
                    return $"{timestring} the insurrection in {Site.AltName} concluded with {TargetCiv} overthrown.";
            }
            return $"{timestring} the insurrection in {Site.AltName} against {TargetCiv} - {Outcome}.";
        }

        internal override string ToTimelineString()
        {
            var timelinestring = base.ToTimelineString();

            return $"{timelinestring} insurrection in {Site.AltName} against {TargetCiv} - {Outcome}.";
        }

        internal override void Export(string table)
        {
            base.Export(table);

            table = GetType().Name;

            var vals = new List<object>
            {
                Id, 
                SiteId.DBExport(), 
                TargetCivId.DBExport(), 
                Outcome.DBExport()
            };

            Database.ExportWorldItem(table, vals);
        }
    }
}
