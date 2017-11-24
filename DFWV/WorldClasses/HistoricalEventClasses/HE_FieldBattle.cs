using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Linq;
using DFWV.WorldClasses.EntityClasses;
using DFWV.WorldClasses.HistoricalFigureClasses;

namespace DFWV.WorldClasses.HistoricalEventClasses
{
    public class HE_FieldBattle : HistoricalEvent
    {
        private int? SubregionId { get; }
        private Region Subregion { get; set; }
        private int? FeatureLayerId { get; }
        private Point Coords { get; }
        private int? EntityId_Defender { get; }
        private Entity Entity_Defender { get; set; }
        private int? EntityId_Attacker { get; }
        private Entity Entity_Attacker { get; set; }
        private int? HfId_AttackerGeneral { get; }
        private HistoricalFigure Hf_AttackerGeneral { get; set; }
        private int? HfId_DefenderGeneral { get; }
        private HistoricalFigure Hf_DefenderGeneral { get; set; }

        override public Point Location => Coords;

        public override IEnumerable<HistoricalFigure> HFsInvolved
        {
            get
            {
                yield return Hf_AttackerGeneral;
                yield return Hf_DefenderGeneral;
            }
        }
        public override IEnumerable<Entity> EntitiesInvolved
        {
            get
            {
                yield return Entity_Defender;
                yield return Entity_Attacker;
            }
        }
        public override IEnumerable<Region> RegionsInvolved
        {
            get { yield return Subregion; }
        }

        public HE_FieldBattle(XDocument xdoc, World world)
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
                    case "subregion_id":
                        if (valI != -1)
                            SubregionId = valI;
                        break;
                    case "feature_layer_id":
                        if (valI != -1)
                            FeatureLayerId = valI;
                        break;
                    case "coords":
                        if (val != "-1,-1")
                            Coords = new Point(Convert.ToInt32(val.Split(',')[0]), Convert.ToInt32(val.Split(',')[1]));
                        break;
                    case "attacker_civ_id":
                        EntityId_Attacker = valI;
                        break;
                    case "defender_civ_id":
                        EntityId_Defender = valI;
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
            EventLabel(frm, parent, ref location, "Region:", Subregion);
            EventLabel(frm, parent, ref location, "Coords:", new Coordinate(Coords));
        }

        protected override string LegendsDescription()
        {
            var timestring = base.LegendsDescription();

            return
                $"{timestring} {Entity_Attacker} attacked {Entity_Defender} in {Subregion}. \n{(Hf_AttackerGeneral == null ? "An unknown creature" : ("The " + Hf_AttackerGeneral.Race + " " + Hf_AttackerGeneral))} led the attack, and the defenders were led by {(Hf_DefenderGeneral == null ? "an unknown creature" : ("The " + Hf_DefenderGeneral.Race + " " + Hf_DefenderGeneral))}.";

        }

        internal override string ToTimelineString()
        {
            var timelinestring = base.ToTimelineString();

            return $"{timelinestring} {Entity_Attacker} attacked {Entity_Defender} in {Subregion}.";
        }

        internal override void Export(string table)
        {
            base.Export(table);

            table = GetType().Name;

            var vals = new List<object>
            {
                Id, 
                SubregionId.DBExport(), 
                FeatureLayerId.DBExport(), 
                EntityId_Attacker.DBExport(), 
                HfId_AttackerGeneral.DBExport(), 
                EntityId_Defender.DBExport(), 
                HfId_DefenderGeneral.DBExport(),
                Coords.DBExport()
            };

            Database.ExportWorldItem(table, vals);

        }

    }
}
