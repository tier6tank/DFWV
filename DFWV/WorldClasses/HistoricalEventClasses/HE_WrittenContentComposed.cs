using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;

namespace DFWV.WorldClasses.HistoricalEventClasses
{
    class HE_WrittenContentComposed : HistoricalEvent_CultureCreatedBase
    {

        public int? Wcid { get; set; }

        public HE_WrittenContentComposed(XDocument xdoc, World world)
            : base(xdoc, world)
        {
            if (xdoc.Root.Elements("wc_id").Count() != 0)
                Wcid = Convert.ToInt32(xdoc.Root.Element("wc_id").Value);
        }

        protected override void WriteDataOnParent(MainForm frm, Control parent, ref Point location)
        {
            base.WriteDataOnParent(frm, parent, ref location);

            if (Wcid.HasValue && World.WrittenContents.ContainsKey(Wcid.Value))
                EventLabel(frm, parent, ref location, "Written Content:", World.WrittenContents[Wcid.Value]);

        }

        protected override string LegendsDescription()
        {
            var timestring = base.LegendsDescription();

            var reasoncircumstancestring = GetReasonCircumstanceString();

            var wcName = "UNKNOWN";
            if (Wcid.HasValue && World.WrittenContents.ContainsKey(Wcid.Value))
                wcName = World.WrittenContents[Wcid.Value].ToString();

            return $"{timestring} {wcName} was authored by the {Hf.Race.ToString().ToLower()} {Hf} in {Site.AltName}{reasoncircumstancestring}.";
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
