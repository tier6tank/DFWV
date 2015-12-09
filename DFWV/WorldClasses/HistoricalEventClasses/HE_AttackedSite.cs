using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Linq;
using DFWV.WorldClasses.EntityClasses;
using DFWV.WorldClasses.HistoricalFigureClasses;

namespace DFWV.WorldClasses.HistoricalEventClasses
{
    public class HE_AttackedSite : HistoricalEvent
    {

        private int? SiteId { get; }
        private Site Site { get; set; }
        private int? SiteCivId { get; }
        private Entity SiteCiv { get; set; }

        private int? AttackerCivId { get; }
        private Entity AttackerCiv { get; set; }
        private int? DefenderCivId { get; }
        private Entity DefenderCiv { get; set; }
        private int? AttackerGeneralHfid { get; }
        private HistoricalFigure AttackerGeneralHf { get; set; }
        private int? DefenderGeneralHfid { get; }
        private HistoricalFigure DefenderGeneralHf { get; set; }

        override public Point Location => Site.Location;

        public override IEnumerable<HistoricalFigure> HFsInvolved
        {
            get
            {
                yield return AttackerGeneralHf;
                yield return DefenderGeneralHf;
            }
        }
        public override IEnumerable<Site> SitesInvolved
        {
            get { yield return Site; }
        }

        public override IEnumerable<Entity> EntitiesInvolved
        {
            get
            {
                yield return SiteCiv;
                yield return AttackerCiv;
                yield return DefenderCiv;
            }
        }
        public HE_AttackedSite(XDocument xdoc, World world)
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
                    case "defender_civ_id":
                        DefenderCivId = valI;
                        break;
                    case "attacker_civ_id":
                        AttackerCivId = valI;
                        break;
                    case "site_civ_id":
                        SiteCivId = valI;
                        break;
                    case "site_id":
                        SiteId = valI;
                        break;
                    case "attacker_general_hfid":
                        AttackerGeneralHfid = valI;
                        break;
                    case "defender_general_hfid":
                        DefenderGeneralHfid = valI;
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
            if (SiteCivId.HasValue && World.Entities.ContainsKey(SiteCivId.Value))
                SiteCiv = World.Entities[SiteCivId.Value];
            if (AttackerCivId.HasValue && World.Entities.ContainsKey(AttackerCivId.Value))
                AttackerCiv = World.Entities[AttackerCivId.Value];
            if (DefenderCivId.HasValue && World.Entities.ContainsKey(DefenderCivId.Value))
                DefenderCiv = World.Entities[DefenderCivId.Value];
            if (AttackerGeneralHfid.HasValue && World.HistoricalFigures.ContainsKey(AttackerGeneralHfid.Value))
                AttackerGeneralHf = World.HistoricalFigures[AttackerGeneralHfid.Value];
            if (DefenderGeneralHfid.HasValue && World.HistoricalFigures.ContainsKey(DefenderGeneralHfid.Value))
                DefenderGeneralHf = World.HistoricalFigures[DefenderGeneralHfid.Value];
        }

        protected override void WriteDataOnParent(MainForm frm, Control parent, ref Point location)
        {
            EventLabel(frm, parent, ref location, "Attacker:", AttackerCiv);
            EventLabel(frm, parent, ref location, "--General:", AttackerGeneralHf);
            EventLabel(frm, parent, ref location, "Defender:", DefenderCiv);
            EventLabel(frm, parent, ref location, "--General:", DefenderGeneralHf);
            EventLabel(frm, parent, ref location, "Owners:", SiteCiv);
            EventLabel(frm, parent, ref location, "Site:", Site);

        }

        protected override string LegendsDescription() //Matched
        {
            var timestring = base.LegendsDescription();

            if (DefenderGeneralHf == null)
                return $"{timestring} {AttackerCiv} attacked {SiteCiv} of {DefenderCiv} at {Site.AltName}. \n" +
                       $"The {AttackerGeneralHf.Race} {AttackerGeneralHf} led the attack.";
            return $"{timestring} {AttackerCiv} attacked {SiteCiv} of {DefenderCiv} at {Site.AltName}. \n" +
                   $"The {AttackerGeneralHf.Race} {AttackerGeneralHf} led the attack, \nand the defenders were led by the {DefenderGeneralHf.Race} {DefenderGeneralHf}.";
        }

        internal override string ToTimelineString()
        {
            var timelinestring = base.ToTimelineString();

            return $"{timelinestring} {AttackerCiv} attacked {DefenderCiv} at {Site.AltName}";
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
                AttackerCivId.DBExport(), 
                DefenderCivId.DBExport(), 
                AttackerGeneralHfid.DBExport(), 
                DefenderGeneralHfid.DBExport()
            };

            Database.ExportWorldItem(table, vals);

        }

    }
}

