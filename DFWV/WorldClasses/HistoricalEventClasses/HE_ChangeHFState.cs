using System;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Drawing;
using System.Windows.Forms;
using DFWV.WorldClasses.HistoricalFigureClasses;

namespace DFWV.WorldClasses.HistoricalEventClasses
{
    class HE_ChangeHFState : HistoricalEvent
    {
        private int? SiteID { get; set; }
        public Site Site { get; private set; }
        private int? SubregionID { get; set; }
        public Region Subregion { get; private set; }
        private int? State { get; set; }
        public static List<string> States = new List<string>();
        private int? HFID { get; set; }
        public HistoricalFigure HF { get; set; }
        private int? FeatureLayerID { get; set; }
        public Point Coords { get; private set; }

        override public Point Location { get { return Coords; } }

        public override IEnumerable<HistoricalFigure> HFsInvolved
        {
            get { yield return HF; }
        }
        public HE_ChangeHFState(XDocument xdoc, World world)
            : base(xdoc, world)
        {
            foreach (var element in xdoc.Root.Elements())
            {
                var val = element.Value;
                int valI;
                Int32.TryParse(val, out valI);

                switch (element.Name.LocalName)
                {
                    case "id":
                    case "year":
                    case "seconds72":
                    case "type":
                        break;
                    case "hfid":
                        HFID = valI;
                        break;
                    case "state":
                        if (!States.Contains(val))
                            States.Add(val);
                        State = States.IndexOf(val);
                        break;
                    case "site_id":
                        if (valI != -1)
                            SiteID = valI;
                        break;
                    case "subregion_id":
                        if (valI != -1)
                            SubregionID = valI;
                        break;
                    case "feature_layer_id":
                        if (valI != -1)
                            FeatureLayerID = valI;
                        break;
                    case "coords":
                        if (val != "-1,-1")
                            Coords = new Point(Convert.ToInt32(val.Split(',')[0]), Convert.ToInt32(val.Split(',')[1]));
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
            if (SiteID.HasValue && World.Sites.ContainsKey(SiteID.Value))
                Site = World.Sites[SiteID.Value];
            if (SubregionID.HasValue && World.Regions.ContainsKey(SubregionID.Value))
                Subregion = World.Regions[SubregionID.Value];
        }


        internal override void Process()
        {
            base.Process();
            if (HF.isLeader && State.HasValue && States[State.Value] == "settled" && Site != null)
            {
                HF.Leader.Site = Site;
            }
        }

        protected override void WriteDataOnParent(MainForm frm, Control parent, ref Point location)
        {
            EventLabel(frm, parent, ref location, "HF:", HF);
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
                        return string.Format("{0} {1} {2} {3} in {4}.",
                            timestring, HF.Race, HF,
                            States[State.Value], Subregion);
                    if (Site != null)
                        return string.Format("{0} {1} {2} {3} in {4}.",
                            timestring, HF.Race, HF,
                            States[State.Value], Site.AltName);
                    break;
                case "wandering":
                    return string.Format(FeatureLayerID == -1 ? "{0} {1} began wandering the wilds." : "{0} {1} began wandering the depths of the world.", timestring, HF);
                case "scouting":
                    if (Site != null)
                        return string.Format("{0} {1} {2} began scouting the area around {3}.",
                            timestring, HF.Race, HF, Site.AltName);
                    break;
                case "thief":
                    if (Site != null)
                        return string.Format("{0} {1} {2} decided to become a thief, operating out of {3}.",
                            timestring, HF.Race, HF, Site.AltName);
                    break;
                case "snatcher":
                    if (Site != null)
                        return string.Format("{0} {1} {2} decided to become a baby-snatcher, operating out of {3}.",
                            timestring, HF.Race, HF, Site.AltName);
                    break;
                case "hunting":
                    if (Subregion != null)
                        return string.Format("{0} {1} {2} began hunting great beasts in {3}.",
                            timestring, HF.Race, HF, Subregion);
                    break;
                case "refugee":
                    if (Subregion != null)
                        return string.Format("{0} {1} {2} fled into the {3}.",
                            timestring, HF.Race, HF, Subregion);
                    break;
            }

            return timestring;
        }

        internal override string ToTimelineString()
        {
            var timelinestring = base.ToTimelineString();

            if (!State.HasValue)
                return string.Format("{0} HF Changed state - {1}.",
                            timelinestring, HFID);

            if (HF == null )
                return string.Format("{0} HF Changed state - {1} - {2}.",
                            timelinestring, HFID,
                            States[State.Value]);

            switch (States[State.Value])
            {
                case "settled":
                    if (Subregion != null)
                        return string.Format("{0} {1} {2} in {3}.",
                            timelinestring, HF,
                            States[State.Value], Subregion);
                    if (Site != null)
                        return string.Format("{0} {1} {2} in {3}.",
                            timelinestring, HF,
                            States[State.Value], Site.AltName);
                    break;
                case "wandering":
                    return string.Format(FeatureLayerID == -1 ? "{0} {1} began wandering the wilds." : "{0} {1} began wandering the depths of the world.", timelinestring, HF);
                case "scouting":
                    if (Site != null)
                        return string.Format("{0} {1} began scouting the area around {2}.",
                            timelinestring, HF, Site.AltName);
                    break;
                case "thief":
                    if (Site != null)
                        return string.Format("{0} {1} decided to become a thief, operating out of {2}.",
                            timelinestring, HF, Site.AltName);
                    break;
                case "snatcher":
                    if (Site != null)
                        return string.Format("{0} {1} decided to become a baby-snatcher, operating out of {2}.",
                            timelinestring, HF, Site.AltName);
                    break;
                case "hunting":
                    if (Subregion != null)
                        return string.Format("{0} {1} began hunting great beasts in {2}.",
                            timelinestring, HF, Subregion);
                    break;
                case "refugee":
                    if (Subregion != null)
                        return string.Format("{0} {1} fled into the {2}.",
                            timelinestring, HF, Subregion);
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
                ID, 
                HFID.DBExport(), 
                State.DBExport(States), 
                SiteID.DBExport(), 
                SubregionID.DBExport(), 
                FeatureLayerID.DBExport(),
                Coords.DBExport()
            };

            Database.ExportWorldItem(table, vals);

        }

    }
}
