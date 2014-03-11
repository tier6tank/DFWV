using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Drawing;
using System.Windows.Forms;
using DFWV.WorldClasses.HistoricalFigureClasses;

namespace DFWV.WorldClasses.HistoricalEventClasses
{
    class HE_FieldBattle : HistoricalEvent
    {
        public int? SubregionID { get; set; }
        public Region Subregion { get; set; }
        public int? FeatureLayerID { get; set; }
        public Point Coords { get; set; }
        public int? DefenderCivID { get; set; }
        public Entity DefenderCiv { get; set; }
        public int? AttackerCivID { get; set; }
        public Entity AttackerCiv { get; set; }
        public int? AttackerGeneralHFID { get; set; }
        public HistoricalFigure AttackerGeneralHF { get; set; }
        public int? DefenderGeneralHFID { get; set; }
        public HistoricalFigure DefenderGeneralHF { get; set; }

        override public Point Location { get { return Coords; } }

        public HE_FieldBattle(XDocument xdoc, World world)
            : base(xdoc, world)
        {
            foreach (XElement element in xdoc.Root.Elements())
            {
                string val = element.Value.ToString();
                int valI;
                Int32.TryParse(val, out valI);

                switch (element.Name.LocalName)
                {
                    case "id":
                    case "year":
                    case "seconds72":
                    case "type":
                        break;
                    case "subregion_id":
                        if (valI != -1)
                            SubregionID = valI;
                        break;
                    case "feature_layer_id":
                        if (valI != -1)
                            FeatureLayerID = valI;
                        break;
                    case "coords":
                        if (val != "-1,-1")
                            Coords = new Point(Convert.ToInt32(val.Split(',')[0]), Convert.ToInt32(val.Split(',')[1]));
                        break;
                    case "attacker_civ_id":
                        AttackerCivID = valI;
                        break;
                    case "defender_civ_id":
                        DefenderCivID = valI;
                        break;
                    case "attacker_general_hfid":
                        AttackerGeneralHFID = valI;
                        break;
                    case "defender_general_hfid":
                        DefenderGeneralHFID = valI;
                        break;

                    default:
                        DFXMLParser.UnexpectedXMLElement(xdoc.Root.Name.LocalName + "\t" + HistoricalEvent.Types[Type], element, xdoc.Root.ToString());
                        break;
                }
            }
        }

        internal override void Link()
        {
            base.Link();
            if (SubregionID.HasValue && World.Regions.ContainsKey(SubregionID.Value))
                Subregion = World.Regions[SubregionID.Value];
            if (AttackerCivID.HasValue && World.Entities.ContainsKey(AttackerCivID.Value))
                AttackerCiv = World.Entities[AttackerCivID.Value];
            if (DefenderCivID.HasValue && World.Entities.ContainsKey(DefenderCivID.Value))
                DefenderCiv = World.Entities[DefenderCivID.Value];
            if (AttackerGeneralHFID.HasValue && World.HistoricalFigures.ContainsKey(AttackerGeneralHFID.Value))
                AttackerGeneralHF = World.HistoricalFigures[AttackerGeneralHFID.Value];
            if (DefenderGeneralHFID.HasValue && World.HistoricalFigures.ContainsKey(DefenderGeneralHFID.Value))
                DefenderGeneralHF = World.HistoricalFigures[DefenderGeneralHFID.Value];
        }

        internal override void Process()
        {
            base.Process();
            if (Subregion.FieldBattleEvents == null)
                Subregion.FieldBattleEvents = new List<HE_FieldBattle>();
            Subregion.FieldBattleEvents.Add(this);
            if (AttackerGeneralHF != null)
            {
                if (AttackerGeneralHF.Events == null)
                    AttackerGeneralHF.Events = new List<HistoricalEvent>();
                AttackerGeneralHF.Events.Add(this);
            }
            if (DefenderGeneralHF != null)
            {
                if (DefenderGeneralHF.Events == null)
                    DefenderGeneralHF.Events = new List<HistoricalEvent>();
                DefenderGeneralHF.Events.Add(this);
            }

            if (AttackerCiv != null)
            {
                if (AttackerCiv.Events == null)
                    AttackerCiv.Events = new List<HistoricalEvent>();
                AttackerCiv.Events.Add(this);
            }


            if (DefenderCiv != null)
            {
                if (AttackerCiv.Events == null)
                    AttackerCiv.Events = new List<HistoricalEvent>();
                AttackerCiv.Events.Add(this);
            }
        }



        public override void WriteDataOnParent(MainForm frm, Control parent, ref Point location)
        {
            EventLabel(frm, parent, ref location, "Attacker:", AttackerCiv);
            EventLabel(frm, parent, ref location, "--General:", AttackerGeneralHF);
            EventLabel(frm, parent, ref location, "Defender:", DefenderCiv);
            EventLabel(frm, parent, ref location, "--General:", DefenderGeneralHF);
            EventLabel(frm, parent, ref location, "Region:", Subregion);
            EventLabel(frm, parent, ref location, "Coords:", new Coordinate(Coords));
        }

        public override string LegendsDescription()
        {
            string timestring = base.LegendsDescription();

            return string.Format("{0} {1} attacked {2} in {3}. \nThe {4} {5} led the attack, \nand the defenders were led by the {6} {7}.",
                            timestring, AttackerCiv.ToString(), DefenderCiv.ToString(),
                            Subregion.ToString(), AttackerGeneralHF.Race.ToString(), AttackerGeneralHF.ToString(),
                            DefenderGeneralHF.Race.ToString(), DefenderGeneralHF.ToString());
        }

        internal override string ToTimelineString()
        {
            string timelinestring = base.ToTimelineString();

            return string.Format("{0} {1} attacked {2} in {3}.",
                        timelinestring, AttackerCiv.ToString(), DefenderCiv.ToString(),
                            Subregion.ToString());
        }

        internal override void Export(string table)
        {
            base.Export(table);

            
            List<object> vals;
            table = this.GetType().Name.ToString();

            vals = new List<object>() { ID, SubregionID, FeatureLayerID, AttackerCivID, AttackerGeneralHFID, DefenderCivID, DefenderGeneralHFID };

            if (Coords.IsEmpty)
                vals.Add(DBNull.Value);
            else
                vals.Add(Coords.X + "," + Coords.Y);
            

            Database.ExportWorldItem(table, vals);

        }

    }
}
