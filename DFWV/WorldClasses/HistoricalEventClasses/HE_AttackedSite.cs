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
        private int? EntityId_SiteCiv { get; }
        private Entity Entity_SiteCiv { get; set; }

        private int? EntityId_Attacker { get; }
        private Entity Entity_Attacker { get; set; }
        private int? EntityId_Defender { get; }
        private Entity Entity_Defender { get; set; }
        private int? HfId_AttackerGeneral { get; }
        private HistoricalFigure Hf_AttackerGeneral { get; set; }
        private int? HfId_DefenderGeneral { get; }
        private HistoricalFigure Hf_DefenderGeneral { get; set; }

        override public Point Location => Site.Location;

        public override IEnumerable<HistoricalFigure> HFsInvolved
        {
            get
            {
                yield return Hf_AttackerGeneral;
                yield return Hf_DefenderGeneral;
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
                yield return Entity_SiteCiv;
                yield return Entity_Attacker;
                yield return Entity_Defender;
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
                        EntityId_Defender = valI;
                        break;
                    case "attacker_civ_id":
                        EntityId_Attacker = valI;
                        break;
                    case "site_civ_id":
                        EntityId_SiteCiv = valI;
                        break;
                    case "site_id":
                        SiteId = valI;
                        break;
                    case "attacker_general_hfid":
                        HfId_AttackerGeneral = valI;
                        break;
                    case "defender_general_hfid":
                        HfId_DefenderGeneral = valI;
                        break;
                    default:
                        DFXMLParser.UnexpectedXmlElement(xdoc.Root.Name.LocalName + "\t" + Types[Type], element, xdoc.Root.ToString());
                        break;
                }
            }
        }

        protected override void WriteDataOnParent(MainForm frm, Control parent, ref Point location)
        {
            EventLabel(frm, parent, ref location, "Attacker:", Entity_Attacker);
            EventLabel(frm, parent, ref location, "--General:", Hf_AttackerGeneral);
            EventLabel(frm, parent, ref location, "Defender:", Entity_Defender);
            EventLabel(frm, parent, ref location, "--General:", Hf_DefenderGeneral);
            EventLabel(frm, parent, ref location, "Owners:", Entity_SiteCiv);
            EventLabel(frm, parent, ref location, "Site:", Site);

        }

        protected override string LegendsDescription() //Matched
        {
            var timestring = base.LegendsDescription();

            if (Hf_DefenderGeneral == null)
                return $"{timestring} {Entity_Attacker} attacked {Entity_SiteCiv} of {Entity_Defender} at {Site.AltName}. \n" +
                       $"The {Hf_AttackerGeneral.Race} {Hf_AttackerGeneral} led the attack.";
            return $"{timestring} {Entity_Attacker} attacked {Entity_SiteCiv} of {Entity_Defender} at {Site.AltName}. \n" +
                   $"The {Hf_AttackerGeneral.Race} {Hf_AttackerGeneral} led the attack, \nand the defenders were led by the {Hf_DefenderGeneral.Race} {Hf_DefenderGeneral}.";
        }

        internal override string ToTimelineString()
        {
            var timelinestring = base.ToTimelineString();

            return $"{timelinestring} {Entity_Attacker} attacked {Entity_Defender} at {Site.AltName}";
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
                EntityId_Attacker.DBExport(), 
                EntityId_Defender.DBExport(), 
                HfId_AttackerGeneral.DBExport(), 
                HfId_DefenderGeneral.DBExport()
            };

            Database.ExportWorldItem(table, vals);

        }

    }
}

