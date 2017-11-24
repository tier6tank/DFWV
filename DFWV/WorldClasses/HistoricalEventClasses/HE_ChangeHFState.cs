using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Linq;
using DFWV.WorldClasses.HistoricalFigureClasses;

namespace DFWV.WorldClasses.HistoricalEventClasses
{
    public class HE_ChangeHFState : HistoricalEvent
    {
        public int? State { get; set; }
        public static List<string> States = new List<string>();
        public int? Reason { get; set; }
        public static List<string> Reasons = new List<string>();
        public int? Mood { get; set; }
        public static List<string> Moods = new List<string>();
        public int? HfId { get; set; }
        public HistoricalFigure Hf { get; set; }
        public int? SiteId { get; set; }
        public Site Site { get; private set; }
        public int? SubregionId { get; set; }
        public Region Subregion { get; private set; }
        public int? FeatureLayerId { get; set; }
        public Point Coords { get; }

        override public Point Location => Coords;

        public override IEnumerable<HistoricalFigure> HFsInvolved
        {
            get { yield return Hf; }
        }
        public override IEnumerable<Site> SitesInvolved
        {
            get { yield return Site; }
        }
        public override IEnumerable<Region> RegionsInvolved
        {
            get { yield return Subregion; }
        }

        public HE_ChangeHFState(XDocument xdoc, World world)
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
                    case "state":
                        if (!States.Contains(val))
                            States.Add(val);
                        State = States.IndexOf(val);
                        break;
                    case "reason":
                        if (!Reasons.Contains(val))
                            Reasons.Add(val);
                        Reason = Reasons.IndexOf(val);
                        break;
                    case "mood":
                        if (!Moods.Contains(val))
                            Moods.Add(val);
                        Mood = Moods.IndexOf(val);
                        break;
                    case "site_id":
                        if (valI != -1)
                            SiteId = valI;
                        break;
                    case "subregion_id":
                        if (valI != -1)
                            SubregionId = valI;
                        break;
                    case "feature_layer_id":
                        if (valI != -1)
                            FeatureLayerId = valI;
                        break;
                    case "coords":
                        if (val != "-1,-1")
                            Coords = new Point(Convert.ToInt32(val.Split(',')[0]), Convert.ToInt32(val.Split(',')[1]));
                        break;
                    default:
                        DFXMLParser.UnexpectedXmlElement(xdoc.Root.Name.LocalName + "\t" + Types[Type], element, xdoc.Root.ToString());
                        break;
                }
            }
        }

        internal override void Process()
        {
            base.Process();
            if (Hf != null && Hf.IsLeader && State.HasValue && States[State.Value] == "settled" && Site != null)
            {
                Hf.Leader.Site = Site;
            }
        }

        protected override void WriteDataOnParent(MainForm frm, Control parent, ref Point location)
        {
            EventLabel(frm, parent, ref location, "HF:", Hf);
            if (State.HasValue)
            EventLabel(frm, parent, ref location, "State:", States[State.Value]);
            EventLabel(frm, parent, ref location, "Site:", Site);
            EventLabel(frm, parent, ref location, "Region:", Subregion);
            if (!Coords.IsEmpty)
                EventLabel(frm, parent, ref location, "Coords:", new Coordinate(Coords));

        }

        protected override string LegendsDescription()
        {
            var timestring = base.LegendsDescription();
            if (!State.HasValue)
                return "";
            switch (States[State.Value])
            {
                case "settled":
                    if (Subregion != null)
                        return $"{timestring} {Hf.Race} {Hf} {States[State.Value]} in {Subregion}.";
                    if (Site != null)
                        return $"{timestring} {Hf.Race} {Hf} {States[State.Value]} in {Site.AltName}.";
                    break;
                case "wandering":
                    return string.Format(FeatureLayerId == -1 ? "{0} {1} began wandering the wilds." : "{0} {1} began wandering the depths of the world.", timestring, Hf);
                case "scouting":
                    if (Site != null)
                        return $"{timestring} {Hf.Race} {Hf} began scouting the area around {Site.AltName}.";
                    break;
                case "thief":
                    if (Site != null)
                        return
                            $"{timestring} {Hf.Race} {Hf} decided to become a thief, operating out of {Site.AltName}.";
                    break;
                case "snatcher":
                    if (Site != null)
                        return
                            $"{timestring} {Hf.Race} {Hf} decided to become a baby-snatcher, operating out of {Site.AltName}.";
                    break;
                case "hunting":
                    if (Subregion != null)
                        return $"{timestring} {Hf.Race} {Hf} began hunting great beasts in {Subregion}.";
                    break;
                case "refugee":
                    if (Subregion != null)
                        return $"{timestring} {Hf.Race} {Hf} fled into the {Subregion}.";
                    break;
            }

            return timestring;
        }

        internal override string ToTimelineString()
        {
            var timelinestring = base.ToTimelineString();

            if (!State.HasValue)
                return $"{timelinestring} HF Changed state - {HfId}.";

            if (Hf == null )
                return $"{timelinestring} HF Changed state - {HfId} - {States[State.Value]}.";

            switch (States[State.Value])
            {
                case "settled":
                    if (Subregion != null)
                        return $"{timelinestring} {Hf} {States[State.Value]} in {Subregion}.";
                    if (Site != null)
                        return $"{timelinestring} {Hf} {States[State.Value]} in {Site.AltName}.";
                    break;
                case "wandering":
                    return string.Format(FeatureLayerId == -1 ? "{0} {1} began wandering the wilds." : "{0} {1} began wandering the depths of the world.", timelinestring, Hf);
                case "scouting":
                    if (Site != null)
                        return $"{timelinestring} {Hf} began scouting the area around {Site.AltName}.";
                    break;
                case "thief":
                    if (Site != null)
                        return $"{timelinestring} {Hf} decided to become a thief, operating out of {Site.AltName}.";
                    break;
                case "snatcher":
                    if (Site != null)
                        return
                            $"{timelinestring} {Hf} decided to become a baby-snatcher, operating out of {Site.AltName}.";
                    break;
                case "hunting":
                    if (Subregion != null)
                        return $"{timelinestring} {Hf} began hunting great beasts in {Subregion}.";
                    break;
                case "refugee":
                    if (Subregion != null)
                        return $"{timelinestring} {Hf} fled into the {Subregion}.";
                    break;
            }

            return timelinestring;
        }

        internal override void Export(string table)
        {
            base.Export(table);


            table = GetType().Name;

            var vals = new List<object>
            {
                Id, 
                HfId.DBExport(), 
                State.DBExport(States), 
                SiteId.DBExport(), 
                SubregionId.DBExport(), 
                FeatureLayerId.DBExport(),
                Coords.DBExport()
            };

            Database.ExportWorldItem(table, vals);

        }

    }
}
