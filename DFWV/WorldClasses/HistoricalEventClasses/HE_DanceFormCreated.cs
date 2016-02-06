using System.Drawing;
using System.Windows.Forms;
using System.Xml.Linq;

namespace DFWV.WorldClasses.HistoricalEventClasses
{
    class HE_DanceFormCreated : HistoricalEvent_CultureCreatedBase
    {

        public HE_DanceFormCreated(XDocument xdoc, World world)
            : base(xdoc, world)
        {

        }

        protected override void WriteDataOnParent(MainForm frm, Control parent, ref Point location)
        {
            base.WriteDataOnParent(frm, parent, ref location);

            if (FormId.HasValue && World.DanceForms.ContainsKey(FormId.Value))
                EventLabel(frm, parent, ref location, "Form:", World.DanceForms[FormId.Value]);

        }

        protected override string LegendsDescription() 
        {
            var timestring = base.LegendsDescription();

            var reasoncircumstancestring = GetReasonCircumstanceString();

            var Form = "UNKNOWN";
            if (FormId.HasValue && World.DanceForms.ContainsKey(FormId.Value))
                Form = World.DanceForms[FormId.Value].ToString();

            return $"{timestring} {Form} was created by the {HistFigure.Race.ToString().ToLower()} {HistFigure} in {Site.AltName}{reasoncircumstancestring}.";
        }

        internal override string ToTimelineString()
        {
            var timelinestring = base.ToTimelineString();

            return $"{timelinestring} Dance form created in {Site.AltName}.";

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
