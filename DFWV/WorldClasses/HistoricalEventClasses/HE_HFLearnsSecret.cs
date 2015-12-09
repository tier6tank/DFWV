using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Linq;
using DFWV.WorldClasses.HistoricalFigureClasses;

namespace DFWV.WorldClasses.HistoricalEventClasses
{
    class HeHfLearnsSecret : HistoricalEvent
    {
        private int? StudentHfid { get; }
        private HistoricalFigure StudentHf { get; set; }
        private int? TeacherHfid { get; }
        private HistoricalFigure TeacherHf { get; set; }
        private int? ArtifactId { get; }
        private Artifact Artifact { get; set; }
        private int Interaction { get; }
        private string SecretText { get; set; }

        override public Point Location => Point.Empty;

        public override IEnumerable<HistoricalFigure> HFsInvolved
        {
            get
            {
                yield return StudentHf;
                yield return TeacherHf;
            }
        }
        public HeHfLearnsSecret(XDocument xdoc, World world)
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
                        StudentHfid = valI;
                        break;
                    case "teacher_hfid":
                        if (valI != -1)
                            TeacherHfid = valI;
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
                        DfxmlParser.UnexpectedXmlElement(xdoc.Root.Name.LocalName + "\t" + Types[Type], element, xdoc.Root.ToString());
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
                        DfxmlParser.UnexpectedXmlElement(xdoc.Root.Name.LocalName + "\t" + Types[Type], element, xdoc.Root.ToString());
                        break;
                }
            }
        }

        internal override void Link()
        {
            base.Link();
            if (StudentHfid.HasValue && World.HistoricalFigures.ContainsKey(StudentHfid.Value))
                StudentHf = World.HistoricalFigures[StudentHfid.Value];
            if (TeacherHfid.HasValue && World.HistoricalFigures.ContainsKey(TeacherHfid.Value))
                TeacherHf = World.HistoricalFigures[TeacherHfid.Value];
            if (ArtifactId.HasValue && World.Artifacts.ContainsKey(ArtifactId.Value))
                Artifact = World.Artifacts[ArtifactId.Value];
        }

        protected override void WriteDataOnParent(MainForm frm, Control parent, ref Point location)
        {
            EventLabel(frm, parent, ref location, "Student:", StudentHf);
            EventLabel(frm, parent, ref location, "Teacher:", TeacherHf);
            EventLabel(frm, parent, ref location, "Artifact:", Artifact);
            EventLabel(frm, parent, ref location, "Interaction:", HistoricalFigure.Interactions[Interaction]);
        }

        protected override string LegendsDescription() //Not Matched
        {
            var timestring = base.LegendsDescription();

            if (TeacherHf == null && Artifact != null)
            {
                if (SecretText != null)
                    return $"{timestring} {StudentHf} learned {SecretText} {Artifact}.";
                switch (HistoricalFigure.Interactions[Interaction])
                {
                    case "ANIMALS_SECRET":
                        return $"{timestring} {StudentHf} learned the secrets of the wilds from {Artifact}.";
                    case "SECRET_30":
                        return $"{timestring} {StudentHf} learned the secrets of life and death from {Artifact}.";
                    case "DISCIPLINE_SECRET":
                        return $"{timestring} {StudentHf} learned the secrets of mental discipline from {Artifact}.";
                    case "WISDOM_SECRET":
                        return $"{timestring} {StudentHf} learned the secrets of wisdom from {Artifact}.";
                    case "FOOD_SECRET":
                        return $"{timestring} {StudentHf} learned the secrets of conjuring food from {Artifact}.";
                    case "WAR_SECRET":
                        return $"{timestring} {StudentHf} learned the secret of berserking from {Artifact}.";
                    default:
                        return
                            $"{timestring} {StudentHf} learned {HistoricalFigure.Interactions[Interaction]} from {Artifact}.";
                }
            }
            if (SecretText != null)
                return $"{timestring} {TeacherHf} taught {StudentHf} {SecretText}.";
            switch (HistoricalFigure.Interactions[Interaction])
            {
                case "ANIMALS_SECRET":
                    return $"{timestring} {TeacherHf} taught {StudentHf} the secrets of the wilds.";
                case "SECRET_5":
                case "SECRET_6":
                case "SECRET_12":
                    return $"{timestring} {TeacherHf} taught {StudentHf} the secrets of life and death.";
                case "DISCIPLINE_SECRET":
                    return $"{timestring} {TeacherHf} taught {StudentHf} the secrets of mental discipline.";
                case "WAR_SECRET":
                    return $"{timestring} {TeacherHf} taught {StudentHf} the secrets of bezerking.";
                case "SUMMONER":
                    return $"{timestring} {TeacherHf} taught {StudentHf} the secrets of summoning.";
                case "SUICIDE_SECRET":
                    return $"{timestring} {TeacherHf} taught {StudentHf} the secrets of living death.";
                case "MERCY_SECRET":
                    return $"{timestring} {TeacherHf} taught {StudentHf} the secrets of mercy and prophecy.";
                case "NATURE_SECRET":
                    return $"{timestring} {TeacherHf} taught {StudentHf} the secrets of nature.";
                default:
                    return $"{timestring} {TeacherHf} taught {StudentHf} {HistoricalFigure.Interactions[Interaction]}.";
            }
        }

        internal override string ToTimelineString()
        {
            var timelinestring = base.ToTimelineString();

            if (TeacherHf == null && Artifact != null)
                return $"{timelinestring} {StudentHf} learned secrets from {Artifact}";
            return $"{timelinestring} {StudentHf} taught secrets by {TeacherHf}";
        }

        internal override void Export(string table)
        {
            base.Export(table);

            table = GetType().Name;
            
            var vals = new List<object>
            {
                Id, 
                StudentHfid.DBExport(), 
                TeacherHfid.DBExport(), 
                ArtifactId.DBExport(),
                Interaction.DBExport(HistoricalFigure.Interactions)
            };

            Database.ExportWorldItem(table, vals);
        }

    }
}