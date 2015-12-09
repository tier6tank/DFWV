using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Linq;
using DFWV.WorldClasses.EntityClasses;
using DFWV.WorldClasses.HistoricalFigureClasses;

namespace DFWV.WorldClasses.HistoricalEventClasses
{
    class HE_PoeticFormCreated : HistoricalEvent_CultureCreatedBase
    {

        public HE_PoeticFormCreated(XDocument xdoc, World world)
            : base(xdoc, world)
        {
 
        }

        protected override string LegendsDescription() 
        {
            var timestring = base.LegendsDescription();

            var reasoncircumstancestring = base.GetReasonCircumstanceString();

            return $"{timestring} UNKNOWN was created by the {HistFigure.Race.ToString().ToLower()} {HistFigure} in {Site.AltName}{reasoncircumstancestring}.";

        }

        internal override string ToTimelineString()
        {
            var timelinestring = base.ToTimelineString();

            return $"{timelinestring} Poetic form created in {Site.AltName}.";

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
