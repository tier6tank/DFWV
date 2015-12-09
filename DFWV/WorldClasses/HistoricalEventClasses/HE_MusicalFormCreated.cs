using System.Xml.Linq;

namespace DFWV.WorldClasses.HistoricalEventClasses
{
    class HeMusicalFormCreated : HistoricalEventCultureCreatedBase
    {

        public HeMusicalFormCreated(XDocument xdoc, World world)
            : base(xdoc, world)
        {
          
        }

        protected override string LegendsDescription() 
        {
            var timestring = base.LegendsDescription();

            var reasoncircumstancestring = GetReasonCircumstanceString();

            return $"{timestring} UNKNOWN was created by the {HistFigure.Race.ToString().ToLower()} {HistFigure} in {Site.AltName}{reasoncircumstancestring}.";
        }

        internal override string ToTimelineString()
        {
            var timelinestring = base.ToTimelineString();

            return $"{timelinestring} Musical form created in {Site.AltName}.";

        }

        internal override void Export(string table)
        {
            base.Export(table);

            table = GetType().Name;

            var vals = GetExportVals();

            Database.ExportWorldItem(table, vals);
        }

    }
}
