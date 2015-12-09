using System;
using System.Linq;
using System.Xml.Linq;

namespace DFWV.WorldClasses.HistoricalEventClasses
{
    class HeWrittenContentComposed : HistoricalEventCultureCreatedBase
    {

        public int? Wcid { get; set; }

        public HeWrittenContentComposed(XDocument xdoc, World world)
            : base(xdoc, world)
        {
            if (xdoc.Root.Elements("wc_id").Count() != 0)
                Wcid = Convert.ToInt32(xdoc.Root.Element("wc_id").Value);
        }

        protected override string LegendsDescription()
        {
            var timestring = base.LegendsDescription();

            var reasoncircumstancestring = GetReasonCircumstanceString();

            return
                $"{timestring} UNKNOWN was authored by the {HistFigure.Race.ToString().ToLower()} {HistFigure} in {Site.AltName}{reasoncircumstancestring}.";
        }

        internal override string ToTimelineString()
        {
            var timelinestring = base.ToTimelineString();

            return $"{timelinestring} Written content composed in {Site.AltName}.";

        }

        internal override void Export(string table)
        {
            base.Export(table);

            table = GetType().Name;

            var vals = GetExportVals();
            vals.Add(Wcid.DBExport());

            Database.ExportWorldItem(table, vals);
        }

    }
}
