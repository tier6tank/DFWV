using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Linq;
using DFWV.WorldClasses.HistoricalFigureClasses;

namespace DFWV.WorldClasses.HistoricalEventClasses
{
    class HE_KnowledgeDiscovered : HistoricalEvent
    {
        private int? Hfid { get; }
        private HistoricalFigure Hf { get; set; }
        public int Knowledge { get; }
        public static List<string> Knowledges = new List<string>();
        private readonly bool _first;

        override public Point Location => Point.Empty;

        public override IEnumerable<HistoricalFigure> HFsInvolved
        {
            get { yield return Hf; }
        }

        public HE_KnowledgeDiscovered(XDocument xdoc, World world)
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
                    case "knowledge":
                        if (!Knowledges.Contains(val))
                            Knowledges.Add(val);
                        Knowledge = Knowledges.IndexOf(val);
                        break;
                    case "hfid":
                        Hfid = valI;
                        break;
                    case "first":
                        _first = true;
                        break;
                    default:
                        DFXMLParser.UnexpectedXmlElement(xdoc.Root.Name.LocalName + "\t" + Types[Type], element, xdoc.Root.ToString());
                        break;
                }
            }
        }

        internal override void Link()
        {
            base.Link();
            if (Hfid.HasValue && World.HistoricalFigures.ContainsKey(Hfid.Value))
                Hf = World.HistoricalFigures[Hfid.Value];

        }

        protected override void WriteDataOnParent(MainForm frm, Control parent, ref Point location)
        {
            EventLabel(frm, parent, ref location, "HF:", Hf);
            EventLabel(frm, parent, ref location, "Knowledge:", Knowledges[Knowledge]);

            if (_first)
                EventLabel(frm, parent, ref location, "First", "");
        }

        protected override string LegendsDescription() 
        {
            var timestring = base.LegendsDescription();

            var knowledgeString = Knowledges[Knowledge];
            switch (Knowledges[Knowledge])
            {
                case "philosophy:specialized:law":
                    knowledgeString = "discourse on law";
                    break;
                default: // Others
                    break;
            }

            if (_first)
                return $"{timestring} {Hf} was the very first to discover {knowledgeString}.";
            else
                return $"{timestring} {Hf} independently discovered {knowledgeString}.";

        }

        internal override string ToTimelineString()
        {
            var timelinestring = base.ToTimelineString();

            return $"{timelinestring} {Hf} discovered {Knowledges[Knowledge]}.";

        }

        internal override void Export(string table)
        {
            base.Export(table);

            table = GetType().Name;

            var vals = new List<object>
            {
                Id,
                Hfid.DBExport(),
                Knowledge.DBExport(Knowledges)
            };

            Database.ExportWorldItem(table, vals);
        }

    }
}
