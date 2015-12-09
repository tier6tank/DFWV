using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Linq;
using DFWV.WorldClasses.HistoricalFigureClasses;

namespace DFWV.WorldClasses.HistoricalEventClasses
{
    class HE_HFLearnsSecret : HistoricalEvent
    {
        private int? StudentHFID { get; set; }
        private HistoricalFigure StudentHF { get; set; }
        private int? TeacherHFID { get; set; }
        private HistoricalFigure TeacherHF { get; set; }
        private int? ArtifactID { get; set; }
        private Artifact Artifact { get; set; }
        private int Interaction { get; set; }
        private string SecretText { get; set; }

        override public Point Location => Point.Empty;

        public override IEnumerable<HistoricalFigure> HFsInvolved
        {
            get
            {
                yield return StudentHF;
                yield return TeacherHF;
            }
        }
        public HE_HFLearnsSecret(XDocument xdoc, World world)
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
                    case "student_hfid":
                        StudentHFID = valI;
                        break;
                    case "teacher_hfid":
                        if (valI != -1)
                            TeacherHFID = valI;
                        break;
                    case "artifact_id":
                        if (valI != -1)
                            ArtifactID = valI;
                        break;
                    case "interaction":
                        if (!HistoricalFigure.Interactions.Contains(val))
                            HistoricalFigure.Interactions.Add(val);
                        Interaction = HistoricalFigure.Interactions.IndexOf(val);
                        break;

                    default:
                        DFXMLParser.UnexpectedXMLElement(xdoc.Root.Name.LocalName + "\t" + Types[Type], element, xdoc.Root.ToString());
                        break;
                }
            }
        }

        internal override void Plus(XDocument xdoc)
        {
            foreach (var element in xdoc.Root.Elements())
            {
                var val = element.Value;
                int valI;
                int.TryParse(val, out valI);

                switch (element.Name.LocalName)
                {
                    case "id":
                    case "type":
                    case "student":
                    case "teacher":
                    case "artifact":
                        break;
                    case "secret_text": //[IS_NAME:the secrets of life and death]
                        SecretText = val.Replace("[IS_NAME:", "").TrimEnd(']').Trim();
                        break;
                    default:
                        DFXMLParser.UnexpectedXMLElement(xdoc.Root.Name.LocalName + "\t" + Types[Type], element, xdoc.Root.ToString());
                        break;
                }
            }
        }

        internal override void Link()
        {
            base.Link();
            if (StudentHFID.HasValue && World.HistoricalFigures.ContainsKey(StudentHFID.Value))
                StudentHF = World.HistoricalFigures[StudentHFID.Value];
            if (TeacherHFID.HasValue && World.HistoricalFigures.ContainsKey(TeacherHFID.Value))
                TeacherHF = World.HistoricalFigures[TeacherHFID.Value];
            if (ArtifactID.HasValue && World.Artifacts.ContainsKey(ArtifactID.Value))
                Artifact = World.Artifacts[ArtifactID.Value];
        }

        protected override void WriteDataOnParent(MainForm frm, Control parent, ref Point location)
        {
            EventLabel(frm, parent, ref location, "Student:", StudentHF);
            EventLabel(frm, parent, ref location, "Teacher:", TeacherHF);
            EventLabel(frm, parent, ref location, "Artifact:", Artifact);
            EventLabel(frm, parent, ref location, "Interaction:", HistoricalFigure.Interactions[Interaction]);
        }

        protected override string LegendsDescription() //Not Matched
        {
            var timestring = base.LegendsDescription();

            if (TeacherHF == null && Artifact != null)
            {
                if (SecretText != null)
                    return $"{timestring} {StudentHF} learned {SecretText} {Artifact}.";
                switch (HistoricalFigure.Interactions[Interaction])
                {
                    case "ANIMALS_SECRET":
                        return $"{timestring} {StudentHF} learned the secrets of the wilds from {Artifact}.";
                    case "SECRET_30":
                        return $"{timestring} {StudentHF} learned the secrets of life and death from {Artifact}.";
                    case "DISCIPLINE_SECRET":
                        return $"{timestring} {StudentHF} learned the secrets of mental discipline from {Artifact}.";
                    case "WISDOM_SECRET":
                        return $"{timestring} {StudentHF} learned the secrets of wisdom from {Artifact}.";
                    case "FOOD_SECRET":
                        return $"{timestring} {StudentHF} learned the secrets of conjuring food from {Artifact}.";
                    case "WAR_SECRET":
                        return $"{timestring} {StudentHF} learned the secret of berserking from {Artifact}.";
                    default:
                        return
                            $"{timestring} {StudentHF} learned {HistoricalFigure.Interactions[Interaction]} from {Artifact}.";
                }
            }
            if (SecretText != null)
                return $"{timestring} {TeacherHF} taught {StudentHF} {SecretText}.";
            switch (HistoricalFigure.Interactions[Interaction])
            {
                case "ANIMALS_SECRET":
                    return $"{timestring} {TeacherHF} taught {StudentHF} the secrets of the wilds.";
                case "SECRET_5":
                case "SECRET_6":
                case "SECRET_12":
                    return $"{timestring} {TeacherHF} taught {StudentHF} the secrets of life and death.";
                case "DISCIPLINE_SECRET":
                    return $"{timestring} {TeacherHF} taught {StudentHF} the secrets of mental discipline.";
                case "WAR_SECRET":
                    return $"{timestring} {TeacherHF} taught {StudentHF} the secrets of bezerking.";
                case "SUMMONER":
                    return $"{timestring} {TeacherHF} taught {StudentHF} the secrets of summoning.";
                case "SUICIDE_SECRET":
                    return $"{timestring} {TeacherHF} taught {StudentHF} the secrets of living death.";
                case "MERCY_SECRET":
                    return $"{timestring} {TeacherHF} taught {StudentHF} the secrets of mercy and prophecy.";
                case "NATURE_SECRET":
                    return $"{timestring} {TeacherHF} taught {StudentHF} the secrets of nature.";
                default:
                    return $"{timestring} {TeacherHF} taught {StudentHF} {HistoricalFigure.Interactions[Interaction]}.";
            }
        }

        internal override string ToTimelineString()
        {
            var timelinestring = base.ToTimelineString();

            if (TeacherHF == null && Artifact != null)
                return $"{timelinestring} {StudentHF} learned secrets from {Artifact}";
            return $"{timelinestring} {StudentHF} taught secrets by {TeacherHF}";
        }

        internal override void Export(string table)
        {
            base.Export(table);

            table = GetType().Name;
            
            var vals = new List<object>
            {
                ID, 
                StudentHFID.DBExport(), 
                TeacherHFID.DBExport(), 
                ArtifactID.DBExport(),
                Interaction.DBExport(HistoricalFigure.Interactions)
            };

            Database.ExportWorldItem(table, vals);
        }

    }
}