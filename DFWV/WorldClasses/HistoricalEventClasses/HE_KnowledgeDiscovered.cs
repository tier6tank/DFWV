﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Linq;
using DFWV.WorldClasses.EntityClasses;
using DFWV.WorldClasses.HistoricalFigureClasses;

namespace DFWV.WorldClasses.HistoricalEventClasses
{
    class HE_KnowledgeDiscovered : HistoricalEvent
    {
        private int? HFID { get; set; }
        private HistoricalFigure HF { get; set; }
        public int Knowledge { get; private set; }
        public static List<string> Knowledges = new List<string>();
        private bool First;

        override public Point Location => Point.Empty;

        public override IEnumerable<HistoricalFigure> HFsInvolved
        {
            get { yield return HF; }
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
                        HFID = valI;
                        break;
                    case "first":
                        First = true;
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
            if (HFID.HasValue && World.HistoricalFigures.ContainsKey(HFID.Value))
                HF = World.HistoricalFigures[HFID.Value];

        }

        protected override void WriteDataOnParent(MainForm frm, Control parent, ref Point location)
        {
            EventLabel(frm, parent, ref location, "HF:", HF);
            EventLabel(frm, parent, ref location, "Knowledge:", Knowledges[Knowledge]);

            if (First)
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

            if (First)
                return $"{timestring} {HF} was the very first to discover {knowledgeString}.";
            else
                return $"{timestring} {HF} independently discovered {knowledgeString}.";

        }

        internal override string ToTimelineString()
        {
            var timelinestring = base.ToTimelineString();

            return $"{timelinestring} {HF} discovered {Knowledges[Knowledge]}.";

        }

        internal override void Export(string table)
        {
            base.Export(table);

            table = GetType().Name;

            var vals = new List<object>
            {
                ID,
                HFID.DBExport(),
                Knowledge.DBExport(Knowledges)
            };

            Database.ExportWorldItem(table, vals);
        }

    }
}