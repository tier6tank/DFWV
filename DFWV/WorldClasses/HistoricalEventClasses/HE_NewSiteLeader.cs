using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Linq;
using DFWV.WorldClasses.EntityClasses;
using DFWV.WorldClasses.HistoricalFigureClasses;

namespace DFWV.WorldClasses.HistoricalEventClasses
{
    public class HeNewSiteLeader : HistoricalEvent
    {
        private int? SiteId { get; }
        private Site Site { get; set; }
        private int? SiteCivId { get; }
        private Entity SiteCiv { get; set; }
        private int? AttackerCivId { get; }
        public Entity AttackerCiv { get; private set; }
        private int? DefenderCivId { get; }
        private Entity DefenderCiv { get; set; }
        private int? NewSiteCivId { get; }
        private Entity NewSiteCiv { get; set; }
        private int? NewLeaderHfid { get; }
        private HistoricalFigure NewLeaderHf { get; set; }

        override public Point Location => Site.Location;

        public override IEnumerable<HistoricalFigure> HFsInvolved
        {
            get { yield return NewLeaderHf; }
        }
        public override IEnumerable<Entity> EntitiesInvolved
        {
            get
            {
                yield return SiteCiv;
                yield return AttackerCiv;
                yield return DefenderCiv;
                yield return NewSiteCiv;
            }
        }
        public override IEnumerable<Site> SitesInvolved
        {
            get { yield return Site; }
        }



        public HeNewSiteLeader(XDocument xdoc, World world)
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
                    case "attacker_civ_id":
                        AttackerCivId = valI;
                        break;
                    case "new_site_civ_id":
                        NewSiteCivId = valI;
                        break;
                    case "defender_civ_id":
                        DefenderCivId = valI;
                        break;
                    case "site_civ_id":
                        SiteCivId = valI;
                        break;
                    case "site_id":
                        SiteId = valI;
                        break;
                    case "new_leader_hfid":
                        NewLeaderHfid = valI;
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
            if (SiteCivId.HasValue && World.Entities.ContainsKey(SiteCivId.Value))
                SiteCiv = World.Entities[SiteCivId.Value];
            if (AttackerCivId.HasValue && World.Entities.ContainsKey(AttackerCivId.Value))
                AttackerCiv = World.Entities[AttackerCivId.Value];
            if (DefenderCivId.HasValue && World.Entities.ContainsKey(DefenderCivId.Value))
                DefenderCiv = World.Entities[DefenderCivId.Value];
            if (NewSiteCivId.HasValue && World.Entities.ContainsKey(NewSiteCivId.Value))
                NewSiteCiv = World.Entities[NewSiteCivId.Value];
            if (NewLeaderHfid.HasValue && World.HistoricalFigures.ContainsKey(NewLeaderHfid.Value))
                NewLeaderHf = World.HistoricalFigures[NewLeaderHfid.Value];
        }


        protected override void WriteDataOnParent(MainForm frm, Control parent, ref Point location)
        {
            EventLabel(frm, parent, ref location, "Attacker:", AttackerCiv);
            EventLabel(frm, parent, ref location, "New Owner:", NewSiteCiv);
            EventLabel(frm, parent, ref location, "Defender:", DefenderCiv);
            EventLabel(frm, parent, ref location, "Old Owner:", SiteCiv);
            EventLabel(frm, parent, ref location, "Site:", Site);
            EventLabel(frm, parent, ref location, "New Leader:", NewLeaderHf);
        }

        protected override string LegendsDescription() //Matched
        {
            var timestring = base.LegendsDescription();

            return
                $"{timestring} {AttackerCiv} defeated {SiteCiv} and placed the {NewLeaderHf.Race} {NewLeaderHf} in charge of {Site.AltName}. \nThe new government was called {NewSiteCiv}.";
        }

        internal override string ToTimelineString()
        {
            var timelinestring = base.ToTimelineString();

            return $"{timelinestring} {AttackerCiv} captured {Site.AltName} from {DefenderCiv}.";
        }

        internal override void Export(string table)
        {
            base.Export(table);

            table = GetType().Name;

            var vals = new List<object>
            {
                Id, 
                SiteId.DBExport(), 
                SiteCivId.DBExport(), 
                DefenderCivId.DBExport(), 
                NewSiteCivId.DBExport(), 
                AttackerCivId.DBExport(), 
                NewLeaderHfid.DBExport()
            };

            Database.ExportWorldItem(table, vals);
        }
    }
}
