using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;
using DFWV.WorldClasses.HistoricalFigureClasses;

namespace DFWV.WorldClasses.HistoricalEventClasses
{
    class HE_HFLearnsSecret : HistoricalEvent
    {
        public int? StudentHFID { get; set; }
        public HistoricalFigure StudentHF { get; set; }
        public int? TeacherHFID { get; set; }
        public HistoricalFigure TeacherHF { get; set; }
        public int? ArtifactID { get; set; }
        public Artifact Artifact { get; set; }
        public int Interaction { get; set; }

        override public Point Location { get { return Point.Empty; } }

        public HE_HFLearnsSecret(XDocument xdoc, World world)
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
                        DFXMLParser.UnexpectedXMLElement(xdoc.Root.Name.LocalName + "\t" + HistoricalEvent.Types[Type], element, xdoc.Root.ToString());
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

        internal override void Process()
        {
            base.Process();
            if (StudentHF != null)
            {
                if (StudentHF.Events == null)
                    StudentHF.Events = new List<HistoricalEvent>();
                StudentHF.Events.Add(this);
            }
            if (TeacherHF != null)
            {
                if (TeacherHF.Events == null)
                    TeacherHF.Events = new List<HistoricalEvent>();
                TeacherHF.Events.Add(this);
            }

        }

        public override void WriteDataOnParent(MainForm frm, Control parent, ref Point location)
        {
            EventLabel(frm, parent, ref location, "Student:", StudentHF);
            EventLabel(frm, parent, ref location, "Teacher:", TeacherHF);
            EventLabel(frm, parent, ref location, "Artifact:", Artifact);
            EventLabel(frm, parent, ref location, "Interaction:", HistoricalFigure.Interactions[Interaction]);
        }

        public override string LegendsDescription()
        {
            string timestring = base.LegendsDescription();

            if (TeacherHF == null && Artifact != null)
            {

                switch (HistoricalFigure.Interactions[Interaction])
                {
                    case "ANIMALS_SECRET":
                        return string.Format("{0} {1} learned the secrets of the wilds from {2}.",
                            timestring, StudentHF.ToString(), Artifact.ToString());
                    case "SECRET_30":
                        return string.Format("{0} {1} learned the secrets of life and death from {2}.",
                            timestring, StudentHF.ToString(), Artifact.ToString());
                    case "DISCIPLINE_SECRET":
                        return string.Format("{0} {1} learned the secrets of mental discipline from {2}.",
                            timestring, StudentHF.ToString(), Artifact.ToString());
                    case "WISDOM_SECRET":
                        return string.Format("{0} {1} learned the secrets of wisdom from {2}.",
                            timestring, StudentHF.ToString(), Artifact.ToString());
                    case "FOOD_SECRET":
                        return string.Format("{0} {1} learned the secrets of conjuring food from {2}.",
                            timestring, StudentHF.ToString(), Artifact.ToString());
                    case "WAR_SECRET":
                        return string.Format("{0} {1} learned the secret of berserking from {2}.",
                            timestring, StudentHF.ToString(), Artifact.ToString());
                    default:
                        return string.Format("{0} {1} learned {2} from {3}.",
                            timestring, StudentHF.ToString(), HistoricalFigure.Interactions[Interaction], Artifact.ToString());
                }
            }
            else
            {

                switch (HistoricalFigure.Interactions[Interaction])
                {
                    case "ANIMALS_SECRET":
                        return string.Format("{0} {1} taught {2} the secrets of the wilds.",
                            timestring, TeacherHF.ToString(), StudentHF.ToString());
                    case "SECRET_5":
                    case "SECRET_6":
                    case "SECRET_12":
                        return string.Format("{0} {1} taught {2} the secrets of life and death.",
                            timestring, TeacherHF.ToString(), StudentHF.ToString(), HistoricalFigure.Interactions[Interaction]);
                    case "DISCIPLINE_SECRET":
                        return string.Format("{0} {1} taught {2} the secrets of mental discipline.",
                            timestring, TeacherHF.ToString(), StudentHF.ToString());
                    case "WAR_SECRET":
                        return string.Format("{0} {1} taught {2} the secrets of bezerking.",
                            timestring, TeacherHF.ToString(), StudentHF.ToString());
                    case "SUMMONER":
                        return string.Format("{0} {1} taught {2} the secrets of summoning.",
                            timestring, TeacherHF.ToString(), StudentHF.ToString());
                    case "SUICIDE_SECRET":
                        return string.Format("{0} {1} taught {2} the secrets of living death.",
                            timestring, TeacherHF.ToString(), StudentHF.ToString());
                    case "MERCY_SECRET":
                        return string.Format("{0} {1} taught {2} the secrets of mercy and prophecy.",
                            timestring, TeacherHF.ToString(), StudentHF.ToString());
                    case "NATURE_SECRET":
                        return string.Format("{0} {1} taught {2} the secrets of nature.",
                            timestring, TeacherHF.ToString(), StudentHF.ToString());
                    default:
                        return string.Format("{0} {1} taught {2} {3}.",
                            timestring, TeacherHF.ToString(), StudentHF.ToString(), HistoricalFigure.Interactions[Interaction]);
                }
            }
        }

        internal override string ToTimelineString()
        {
            string timelinestring = base.ToTimelineString();

            if (TeacherHF == null && Artifact != null)
                return string.Format("{0} {1} learned secrets from {2}",
                            timelinestring, StudentHF.ToString(), Artifact.ToString());
            else
                return string.Format("{0} {1} taught secrets by {2}",
                            timelinestring, StudentHF.ToString(), TeacherHF.ToString());


        }

        internal override void Export(string table)
        {
            base.Export(table);

            
            List<object> vals;
            table = this.GetType().Name.ToString();


            
            vals = new List<object>() { ID, StudentHFID, TeacherHFID, ArtifactID, HistoricalFigure.Interactions[Interaction] };


            Database.ExportWorldItem(table, vals);

        }

    }
}