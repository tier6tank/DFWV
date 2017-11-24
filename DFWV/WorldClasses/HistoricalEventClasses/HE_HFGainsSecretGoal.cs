using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Linq;
using DFWV.WorldClasses.HistoricalFigureClasses;

namespace DFWV.WorldClasses.HistoricalEventClasses
{
    class HE_HFGainsSecretGoal : HistoricalEvent
    {
        private int? HfId { get; }
        private HistoricalFigure Hf { get; set; }
        private int SecretGoal { get; }


        override public Point Location => Point.Empty;

        public override IEnumerable<HistoricalFigure> HFsInvolved
        {
            get { yield return Hf; }
        }
        public HE_HFGainsSecretGoal(XDocument xdoc, World world)
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
                    case "hfid":
                        HfId = valI;
                        break;
                    case "secret_goal":
                        if (!HistoricalFigure.Goals.Contains(val))
                            HistoricalFigure.Goals.Add(val);
                        SecretGoal = HistoricalFigure.Goals.IndexOf(val);
                        break;

                    default:
                        DFXMLParser.UnexpectedXmlElement(xdoc.Root.Name.LocalName + "\t" + Types[Type], element, xdoc.Root.ToString());
                        break;
                }
            }
        }

        protected override void WriteDataOnParent(MainForm frm, Control parent, ref Point location)
        {
            EventLabel(frm, parent, ref location, "HF:", Hf);
            EventLabel(frm, parent, ref location, "Goal:", HistoricalFigure.Goals[SecretGoal]);
        }

        protected override string LegendsDescription()
        {
            var timestring = base.LegendsDescription();

            return string.Format("{0} {1} became obsessed with {2} own mortality and sought to extend {2} life by any means.",
                                    timestring, Hf,
                                    Hf.Caste.HasValue ? (HistoricalFigure.Castes[Hf.Caste.Value].ToLower() == "female" ? "her" : "his") : "his");
        }

        internal override string ToTimelineString()
        {
            var timelinestring = base.ToTimelineString();

            return $"{timelinestring} {Hf} got immortality goal.";
        }

        internal override void Export(string table)
        {
            base.Export(table);

            table = GetType().Name;

            var vals = new List<object>
            {
                Id, 
                HfId.DBExport(),
                SecretGoal.DBExport(HistoricalFigure.Goals)
            };

            Database.ExportWorldItem(table, vals);

        }

    }
}