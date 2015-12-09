using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Linq;
using DFWV.WorldClasses.HistoricalFigureClasses;

namespace DFWV.WorldClasses.HistoricalEventClasses
{
    class HeChangedCreatureType : HistoricalEvent
    {
        private int? ChangeeHfid { get; }
        private HistoricalFigure ChangeeHf { get; set; }
        private int? ChangerHfid { get; }
        private HistoricalFigure ChangerHf { get; set; }
        private string OldRace_ { get; set; }
        private Race OldRace { get; set; }
        private int? OldCaste { get; }
        private string NewRace_ { get; set; }
        private Race NewRace { get; set; }
        private int? NewCaste { get; }

        override public Point Location => Point.Empty;

        public override IEnumerable<HistoricalFigure> HFsInvolved
        {
            get
            {
                yield return ChangeeHf;
                yield return ChangerHf;
            }
        }
        public HeChangedCreatureType(XDocument xdoc, World world)
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
                    case "changee_hfid":
                        ChangeeHfid = valI;
                        break;
                    case "changer_hfid":
                        ChangerHfid = valI;
                        break;
                    case "old_race":
                        OldRace_ = val;
                        break;
                    case "old_caste":
                        if (!HistoricalFigure.Castes.Contains(val))
                            HistoricalFigure.Castes.Add(val);
                        OldCaste = HistoricalFigure.Castes.IndexOf(val);
                        break;
                    case "new_race":
                        NewRace_ = val;
                        break;
                    case "new_caste":
                        if (!HistoricalFigure.Castes.Contains(val))
                            HistoricalFigure.Castes.Add(val);
                        NewCaste = HistoricalFigure.Castes.IndexOf(val);
                        break;
                    default:
                        DfxmlParser.UnexpectedXmlElement(xdoc.Root.Name.LocalName + "\t" + Types[Type], element, xdoc.Root.ToString());
                        break;
                }
            }
        }

        internal override void Link()
        {
            base.Link();
            if (ChangeeHfid.HasValue && World.HistoricalFigures.ContainsKey(ChangeeHfid.Value))
                ChangeeHf = World.HistoricalFigures[ChangeeHfid.Value];
            if (ChangerHfid.HasValue && World.HistoricalFigures.ContainsKey(ChangerHfid.Value))
                ChangerHf = World.HistoricalFigures[ChangerHfid.Value];
            if (OldRace_ != null)
            {
                OldRace = World.GetAddRace(OldRace_);
                OldRace_ = null;
            }
            if (NewRace_ == null) return;
            NewRace = World.GetAddRace(NewRace_);
            NewRace_ = null;
        }

        protected override void WriteDataOnParent(MainForm frm, Control parent, ref Point location)
        {
            EventLabel(frm, parent, ref location, "Changer:", ChangerHf);
            EventLabel(frm, parent, ref location, "Changee:", ChangeeHf);
            EventLabel(frm, parent, ref location, "Old Race:", OldRace);
            if (OldCaste.HasValue)
                EventLabel(frm, parent, ref location, "Old Caste:", HistoricalFigure.Castes[OldCaste.Value]);
            EventLabel(frm, parent, ref location, "New Race:", NewRace);
            if (NewCaste.HasValue)
            EventLabel(frm, parent, ref location, "New Caste:", HistoricalFigure.Castes[NewCaste.Value]);

        }

        protected override string LegendsDescription()
        {
            var timestring = base.LegendsDescription();

            return $"{timestring} {ChangerHf} changed {ChangeeHf} from a {OldRace} into a {NewRace}.";
        }

        internal override string ToTimelineString()
        {
            var timelinestring = base.ToTimelineString();

            return $"{timelinestring} {ChangerHf} transformed {ChangeeHf}.";
        }

        internal override void Export(string table)
        {
            base.Export(table);


            table = GetType().Name;

            var vals = new List<object>
            {
                Id, 
                ChangeeHfid.DBExport(), 
                ChangerHfid.DBExport(), 
                OldRace.DBExport(), 
                OldCaste.DBExport(HistoricalFigure.Castes),
                NewRace.DBExport(),
                NewCaste.DBExport(HistoricalFigure.Castes)
            };

            Database.ExportWorldItem(table, vals);

        }

    }
}


