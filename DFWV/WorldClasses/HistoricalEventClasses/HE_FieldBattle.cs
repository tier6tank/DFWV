using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Linq;
using DFWV.WorldClasses.EntityClasses;
using DFWV.WorldClasses.HistoricalFigureClasses;

namespace DFWV.WorldClasses.HistoricalEventClasses
{
    public class HeFieldBattle : HistoricalEvent
    {
        private int? SubregionId { get; }
        private Region Subregion { get; set; }
        private int? FeatureLayerId { get; }
        private Point Coords { get; }
        private int? DefenderCivId { get; }
        private Entity DefenderCiv { get; set; }
        private int? AttackerCivId { get; }
        private Entity AttackerCiv { get; set; }
        private int? AttackerGeneralHfid { get; }
        private HistoricalFigure AttackerGeneralHf { get; set; }
        private int? DefenderGeneralHfid { get; }
        private HistoricalFigure DefenderGeneralHf { get; set; }

        override public Point Location => Coords;

        public override IEnumerable<HistoricalFigure> HFsInvolved
        {
            get
            {
                yield return AttackerGeneralHf;
                yield return DefenderGeneralHf;
            }
        }
        public override IEnumerable<Entity> EntitiesInvolved
        {
            get
            {
                yield return DefenderCiv;
                yield return AttackerCiv;
            }
        }
        public override IEnumerable<Region> RegionsInvolved
        {
            get { yield return Subregion; }
        }

        public HeFieldBattle(XDocument xdoc, World world)
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
                        AttackerCivId = valI;
                        break;
                    case "defender_civ_id":
                        DefenderCivId = valI;
                        break;
                    case "attacker_general_hfid":
                        AttackerGeneralHfid = valI;
                        break;
                    case "defender_general_hfid":
                        DefenderGeneralHfid = valI;
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
            if (SubregionId.HasValue && World.Regions.ContainsKey(SubregionId.Value))
                Subregion = World.Regions[SubregionId.Value];
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
            EventLabel(frm, parent, ref location, "Region:", Subregion);
            EventLabel(frm, parent, ref location, "Coords:", new Coordinate(Coords));
        }

        protected override string LegendsDescription()
        {
            var timestring = base.LegendsDescription();

            return
                $"{timestring} {AttackerCiv} attacked {DefenderCiv} in {Subregion}. \n{(AttackerGeneralHf == null ? "An unknown creature" : ("The " + AttackerGeneralHf.Race + " " + AttackerGeneralHf))} led the attack, and the defenders were led by {(DefenderGeneralHf == null ? "an unknown creature" : ("The " + DefenderGeneralHf.Race + " " + DefenderGeneralHf))}.";

        }

        internal override string ToTimelineString()
        {
            var timelinestring = base.ToTimelineString();

            return $"{timelinestring} {AttackerCiv} attacked {DefenderCiv} in {Subregion}.";
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
                AttackerCivId.DBExport(), 
                AttackerGeneralHfid.DBExport(), 
                DefenderCivId.DBExport(), 
                DefenderGeneralHfid.DBExport(),
                Coords.DBExport()
            };

            Database.ExportWorldItem(table, vals);

        }

    }
}
