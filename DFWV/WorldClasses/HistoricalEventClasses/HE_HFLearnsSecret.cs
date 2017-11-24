using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Linq;
using DFWV.WorldClasses.HistoricalFigureClasses;

namespace DFWV.WorldClasses.HistoricalEventClasses
{
    class HE_HFLearnsSecret : HistoricalEvent
    {
        private int? HfId_Student { get; }
        private HistoricalFigure Hf_Student { get; set; }
        private int? HfId_Teacher { get; }
        private HistoricalFigure Hf_Teacher { get; set; }
        private int? ArtifactId { get; }
        private Artifact Artifact { get; set; }
        private int Interaction { get; }
        private string SecretText { get; set; }

        override public Point Location => Point.Empty;

        public override IEnumerable<HistoricalFigure> HFsInvolved
        {
            get
            {
                yield return Hf_Student;
                yield return Hf_Teacher;
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
                        HfId_Student = valI;
                        break;
                    case "teacher_hfid":
                        if (valI != -1)
                            HfId_Teacher = valI;
                        break;
                    case "artifact_id":
                        if (valI != -1)
                            ArtifactId = valI;
                        break;
                    case "interaction":
                        if (!HistoricalFigure.Interactions.Contains(val))
                            HistoricalFigure.Interactions.Add(val);
                        Interaction = HistoricalFigure.Interactions.IndexOf(val);
                        break;

                    default:
                        DFXMLParser.UnexpectedXmlElement(xdoc.Root.Name.LocalName + "\t" + Types[Type], element, xdoc.Root.ToString());
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
                        DFXMLParser.UnexpectedXmlElement(xdoc.Root.Name.LocalName + "\t" + Types[Type], element, xdoc.Root.ToString());
                        break;
                }
            }
        }

        protected override void WriteDataOnParent(MainForm frm, Control parent, ref Point location)
        {
            EventLabel(frm, parent, ref location, "Student:", Hf_Student);
            EventLabel(frm, parent, ref location, "Teacher:", Hf_Teacher);
            EventLabel(frm, parent, ref location, "Artifact:", Artifact);
            EventLabel(frm, parent, ref location, "Interaction:", HistoricalFigure.Interactions[Interaction]);
        }

        protected override string LegendsDescription() //Not Matched
        {
            var timestring = base.LegendsDescription();

            if (Hf_Teacher == null && Artifact != null)
            {
                if (SecretText != null)
                    return $"{timestring} {Hf_Student} learned {SecretText} {Artifact}.";
                switch (HistoricalFigure.Interactions[Interaction])
                {
                    case "ANIMALS_SECRET":
                        return $"{timestring} {Hf_Student} learned the secrets of the wilds from {Artifact}.";
                    case "SECRET_30":
                        return $"{timestring} {Hf_Student} learned the secrets of life and death from {Artifact}.";
                    case "DISCIPLINE_SECRET":
                        return $"{timestring} {Hf_Student} learned the secrets of mental discipline from {Artifact}.";
                    case "WISDOM_SECRET":
                        return $"{timestring} {Hf_Student} learned the secrets of wisdom from {Artifact}.";
                    case "FOOD_SECRET":
                        return $"{timestring} {Hf_Student} learned the secrets of conjuring food from {Artifact}.";
                    case "WAR_SECRET":
                        return $"{timestring} {Hf_Student} learned the secret of berserking from {Artifact}.";
                    default:
                        return
                            $"{timestring} {Hf_Student} learned {HistoricalFigure.Interactions[Interaction]} from {Artifact}.";
                }
            }
            if (SecretText != null)
                return $"{timestring} {Hf_Teacher} taught {Hf_Student} {SecretText}.";
            switch (HistoricalFigure.Interactions[Interaction])
            {
                case "ANIMALS_SECRET":
                    return $"{timestring} {Hf_Teacher} taught {Hf_Student} the secrets of the wilds.";
                case "SECRET_5":
                case "SECRET_6":
                case "SECRET_12":
                    return $"{timestring} {Hf_Teacher} taught {Hf_Student} the secrets of life and death.";
                case "DISCIPLINE_SECRET":
                    return $"{timestring} {Hf_Teacher} taught {Hf_Student} the secrets of mental discipline.";
                case "WAR_SECRET":
                    return $"{timestring} {Hf_Teacher} taught {Hf_Student} the secrets of bezerking.";
                case "SUMMONER":
                    return $"{timestring} {Hf_Teacher} taught {Hf_Student} the secrets of summoning.";
                case "SUICIDE_SECRET":
                    return $"{timestring} {Hf_Teacher} taught {Hf_Student} the secrets of living death.";
                case "MERCY_SECRET":
                    return $"{timestring} {Hf_Teacher} taught {Hf_Student} the secrets of mercy and prophecy.";
                case "NATURE_SECRET":
                    return $"{timestring} {Hf_Teacher} taught {Hf_Student} the secrets of nature.";
                default:
                    return $"{timestring} {Hf_Teacher} taught {Hf_Student} {HistoricalFigure.Interactions[Interaction]}.";
            }
        }

        internal override string ToTimelineString()
        {
            var timelinestring = base.ToTimelineString();

            if (Hf_Teacher == null && Artifact != null)
                return $"{timelinestring} {Hf_Student} learned secrets from {Artifact}";
            return $"{timelinestring} {Hf_Student} taught secrets by {Hf_Teacher}";
        }

        internal override void Export(string table)
        {
            base.Export(table);

            table = GetType().Name;
            
            var vals = new List<object>
            {
                Id, 
                HfId_Student.DBExport(), 
                HfId_Teacher.DBExport(), 
                ArtifactId.DBExport(),
                Interaction.DBExport(HistoricalFigure.Interactions)
            };

            Database.ExportWorldItem(table, vals);
        }

    }
}