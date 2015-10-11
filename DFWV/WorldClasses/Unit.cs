using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Xml.Linq;
using DFWV.Annotations;
using DFWV.WorldClasses.EntityClasses;
using DFWV.WorldClasses.HistoricalFigureClasses;

namespace DFWV.WorldClasses
{
    public class Unit : XMLObject
    {

        public static List<string> JobTypes = new List<string>();
        public string AltName { get; private set; }
        public int? Profession { get; set; }
        public int? Profession2 { get; set; }
        private int? RaceID { get; set; }
        public Race Race { get; set; }
        private int? CasteID { get; set; }
        public Caste Caste { get; set; }
        public Point3 Coords { get; set; }
        override public Point Location { get { return Point.Empty; } }
        private int? Sex { get; set; }
        private int? CivID { get; set; }
        public Entity Civ { get; private set; }
        private int? PopID { get; set; }
        public EntityPopulation Population { get; private set; }
        private int? SquadID { get; set; }
        public Squad Squad { get; private set; }
        private int? OpponentID { get; set; }
        public HistoricalFigure Opponent { get; private set; }
        private int? Mood { get; set; }
        private int? HistFigureID { get; set; }
        public HistoricalFigure HistFigure { get; private set; }
        private int? HistFigureID2 { get; set; }
        public HistoricalFigure HistFigure2 { get; private set; }
        public string[] Labors { get; private set; }
        public string[] Flags { get; set; }

        public Unit(XDocument xdoc, World world)
            : base(xdoc, world)
        {
            foreach (var element in xdoc.Root.Elements())
            {
                var val = element.Value.Trim();
                int valI;
                int.TryParse(val, out valI);
                switch (element.Name.LocalName)
                {
                    case "id":
                        break;
                    case "name":
                        Name = val;
                        break;
                    case "name2":
                        AltName = val;
                        break;
                    case "profession":
                        if (!JobTypes.Contains(val))
                            JobTypes.Add(val);
                        Profession = JobTypes.IndexOf(val);
                        break;
                    case "profession2":
                        if (!JobTypes.Contains(val))
                            JobTypes.Add(val);
                        Profession2 = JobTypes.IndexOf(val);
                        break;
                    case "race":
                        RaceID = valI;
                        break;
                    case "caste":
                        CasteID = valI;
                        break;
                    case "coords":
                        Coords = new Point3(
                            Convert.ToInt32(val.Split(',')[0]),
                            Convert.ToInt32(val.Split(',')[1]),
                            Convert.ToInt32(val.Split(',')[2]));
                        break;
                    case "sex":
                        Sex = valI;
                        break;
                    case "civ_id":
                        if (valI != -1)
                            CivID = valI;
                        break;
                    case "population_id":
                        if (valI != -1)
                            PopID = valI;
                        break;
                    case "squad_id":
                        if (valI != -1)
                            SquadID = valI;
                        break;
                    case "opponent_id":
                        if (valI != -1)
                            OpponentID = valI;
                        break;
                    case "mood":
                        Mood = valI;
                        break;
                    case "labors":
                        Labors = val.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                        break;
                    case "hist_figure_id":
                        if (valI != -1)
                            HistFigureID = valI;
                        break;
                    case "hist_figure_id2":
                        if (valI != -1)
                            HistFigureID2 = valI;
                        break;
                    case "flags":
                        Flags = val.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                        break;
                    default:
                        DFXMLParser.UnexpectedXMLElement(xdoc.Root.Name.LocalName, element, xdoc.Root.ToString());
                        break;
                }
            }
        }

        public override void Select(MainForm frm)
        {
            try
            {
                frm.grpUnit.Text = ToString();
                frm.grpUnit.Show();
#if DEBUG
                frm.grpUnit.Text += string.Format(" - ID: {0}", ID);
#endif

                frm.lblUnitName.Text = Name;
                frm.lblUnitAltName.Text = AltName;
                frm.lblUnitCoords.Text = Coords.ToString();
                frm.lblUnitSex.Text = Sex.ToString();
                frm.lblUnitCiv.Data = Civ;
                frm.lblUnitPop.Data = Population;
                frm.lblUnitMood.Text = Mood.ToString();
                frm.lblUnitHF.Data = HistFigure;
                frm.lblUnitHF2.Data = HistFigure2;
                frm.lblUnitRace.Data = Race;
                frm.lblUnitCaste.Text = Caste.ToString();
                if (Profession.HasValue)
                {
                    frm.lblUnitProfession.Text = JobTypes[Profession.Value];
                    frm.lblUnitProfession.Visible = true;
                }
                else
                    frm.lblUnitProfession.Visible = false;
                if (Profession2.HasValue)
                {
                    frm.lblUnitProfession2.Text = JobTypes[Profession2.Value];
                    frm.lblUnitProfession2.Visible = true;
                }
                else
                    frm.lblUnitProfession2.Visible = false;
                frm.lblUnitSquad.Data = Squad;
                frm.lblUnitFlags.Text = string.Join(",",Flags);
                frm.lblUnitLabors.Text = string.Join(",", Labors);
                frm.lblUnitOpponent.Data = Opponent;
            }
            finally
            {
                Program.MakeSelected(frm.tabUnit, frm.lstUnit, this);
            }
        }

        internal override void Export(string table)
        {

            var vals = new List<object>
            {
                ID,
                Name.DBExport()
            };

            Database.ExportWorldItem(table, vals);
        }

        internal override void Link()
        {
            if (CivID.HasValue)
                Civ = World.Entities[CivID.Value];
            if (RaceID.HasValue)
            {
                Race = World.Races[RaceID.Value];
                if (CasteID.HasValue)
                    Caste = Race.Castes[CasteID.Value];
            }
            if (CivID.HasValue)
                Civ = World.Entities[CivID.Value];
            if (PopID.HasValue)
                Population = World.EntityPopulations[PopID.Value];
            if (SquadID.HasValue)
                Squad = World.Squads[SquadID.Value];
            if (OpponentID.HasValue && World.Units.ContainsKey(OpponentID.Value))
                Opponent = World.HistoricalFigures[OpponentID.Value];

            if (HistFigureID.HasValue)
                HistFigure = World.HistoricalFigures[HistFigureID.Value];
            if (HistFigureID2.HasValue)
                HistFigure2 = World.HistoricalFigures[HistFigureID2.Value];
        }

        internal override void Process()
        {

        }

        internal override void Plus(XDocument xdoc)
        {

        }
    }


}