using System.Drawing;
using System.Windows.Forms;
using System.Xml.Linq;

namespace DFWV.WorldClasses.HistoricalEventClasses
{
    class HE_MusicalFormCreated : HistoricalEvent_CultureCreatedBase
    {

        public HE_MusicalFormCreated(XDocument xdoc, World world)
            : base(xdoc, world)
        {
          
        }

        protected override void WriteDataOnParent(MainForm frm, Control parent, ref Point location)
        {
            base.WriteDataOnParent(frm, parent, ref location);

            if (FormId.HasValue && World.MusicalForms.ContainsKey(FormId.Value))
                EventLabel(frm, parent, ref location, "Form:", World.MusicalForms[FormId.Value]);

        }

        protected override string LegendsDescription() 
        {
            var timestring = base.LegendsDescription();

            var reasoncircumstancestring = GetReasonCircumstanceString();
            var Form = "UNKNOWN";
            if (FormId.HasValue && World.MusicalForms.ContainsKey(FormId.Value))
                Form = World.MusicalForms[FormId.Value].ToString();

            return $"{timestring} {Form} was created by the {Hf.Race.ToString().ToLower()} {Hf} in {Site.AltName}{reasoncircumstancestring}.";
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
