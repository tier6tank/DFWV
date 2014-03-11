using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Drawing;
using System.Windows.Forms;
using DFWV.WorldClasses.HistoricalFigureClasses;

namespace DFWV.WorldClasses.HistoricalEventClasses
{
    class HE_ChangeHFState : HistoricalEvent
    {
        public int? SiteID { get; set; }
        public Site Site { get; set; }
        public int? SubregionID { get; set; }
        public Region Subregion { get; set; }
        public int State { get; set; }
        public static List<string> States = new List<string>();
        public int? HFID { get; set; }
        public HistoricalFigure HF { get; set; }
        public int? FeatureLayerID { get; set; }
        public Point Coords { get; set; }

        override public Point Location { get { return Coords; } }

        public HE_ChangeHFState(XDocument xdoc, World world)
            : base(xdoc, world)
        {
            foreach (XElement element in xdoc.Root.Elements())
            {
                string val = element.Value.ToString();
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
                        DFXMLParser.UnexpectedXMLElement(xdoc.Root.Name.LocalName + "\t" + HistoricalEvent.Types[Type], element, xdoc.Root.ToString());
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
            if (HF != null)
            {
                if (HF.Events == null)
                    HF.Events = new List<HistoricalEvent>();
                HF.Events.Add(this);
            }
        }

        public override void WriteDataOnParent(MainForm frm, Control parent, ref Point location)
        {
            EventLabel(frm, parent, ref location, "HF:", HF);
            EventLabel(frm, parent, ref location, "State:", States[State]);
            EventLabel(frm, parent, ref location, "Site:", Site);
            EventLabel(frm, parent, ref location, "Region:", Subregion);
            if (!Coords.IsEmpty)
                EventLabel(frm, parent, ref location, "Coords:", new Coordinate(Coords));

        }

        public override string LegendsDescription()
        {
            string timestring = base.LegendsDescription();

            switch (States[State])
            {
                case "settled":
                    if (Subregion != null)
                        return string.Format("{0} {1} {2} {3} in {4}.",
                            timestring, HF.Race.ToString(), HF.ToString(),
                            States[State], Subregion.ToString());
                    else if (Site != null)
                        return string.Format("{0} {1} {2} {3} in {4}.",
                            timestring, HF.Race.ToString(), HF.ToString(),
                            States[State], Site.AltName);
                    else
                        break;
                case "wandering":
                    if (FeatureLayerID == -1)
                        return string.Format("{0} {1} began wandering the wilds.",
                            timestring, HF.ToString());
                    else
                        return string.Format("{0} {1} began wandering the depths of the world.",
                            timestring, HF.ToString());
                case "scouting":
                    if (Site != null)
                        return string.Format("{0} {1} {2} began scouting the area around {3}.",
                            timestring, HF.Race.ToString(), HF.ToString(), Site.AltName);
                    else
                        break;
                case "thief":
                    if (Site != null)
                        return string.Format("{0} {1} {2} decided to become a thief, operating out of {3}.",
                            timestring, HF.Race.ToString(), HF.ToString(), Site.AltName);
                    else
                        break;
                case "snatcher":
                    if (Site != null)
                        return string.Format("{0} {1} {2} decided to become a baby-snatcher, operating out of {3}.",
                            timestring, HF.Race.ToString(), HF.ToString(), Site.AltName);
                    else
                        break;
                case "hunting":
                    if (Subregion != null)
                        return string.Format("{0} {1} {2} began hunting great beasts in {3}.",
                            timestring, HF.Race.ToString(), HF.ToString(), Subregion.ToString());
                    else
                        break;
                case "refugee":
                    if (Subregion != null)
                        return string.Format("{0} {1} {2} fled into the {3}.",
                            timestring, HF.Race.ToString(), HF.ToString(), Subregion.ToString());
                    else
                        break;
                default:
                    break;
            }

            return timestring;
        }

        internal override string ToTimelineString()
        {
            string timelinestring = base.ToTimelineString();

            if (HF == null )
                return string.Format("{0} HF Changed state - {1} in {2}.",
                            timelinestring, HFID.ToString(),
                            States[State]);

            switch (States[State])
            {
                case "settled":
                    if (Subregion != null)
                        return string.Format("{0} {1} {2} in {3}.",
                            timelinestring, HF.ToString(),
                            States[State], Subregion.ToString());
                    else if (Site != null)
                        return string.Format("{0} {1} {2} in {3}.",
                            timelinestring, HF.ToString(),
                            States[State], Site.AltName);
                    else
                        break;
                case "wandering":
                    if (FeatureLayerID == -1)
                        return string.Format("{0} {1} began wandering the wilds.",
                            timelinestring, HF.ToString());
                    else 
                        return string.Format("{0} {1} began wandering the depths of the world.",
                            timelinestring, HF.ToString());
                case "scouting":
                    if (Site != null)
                        return string.Format("{0} {1} began scouting the area around {2}.",
                            timelinestring, HF.ToString(), Site.AltName);
                    else
                        break;
                case "thief":
                    if (Site != null)
                        return string.Format("{0} {1} decided to become a thief, operating out of {2}.",
                            timelinestring, HF.ToString(), Site.AltName);
                    else
                        break;
                case "snatcher":
                    if (Site != null)
                        return string.Format("{0} {1} decided to become a baby-snatcher, operating out of {2}.",
                            timelinestring, HF.ToString(), Site.AltName);
                    else
                        break;
                case "hunting":
                    if (Subregion != null)
                        return string.Format("{0} {1} began hunting great beasts in {2}.",
                            timelinestring, HF.ToString(), Subregion.ToString());
                    else
                        break;
                case "refugee":
                    if (Subregion != null)
                        return string.Format("{0} {1} fled into the {2}.",
                            timelinestring, HF.ToString(), Subregion.ToString());
                    else
                        break;
                default:
                    break;
            }

            return timelinestring;
        }

        internal override void Export(string table)
        {
            base.Export(table);

            
            List<object> vals;
            table = this.GetType().Name.ToString();

            vals = new List<object>() { ID, HFID, States[State], SiteID, Subregion, FeatureLayerID};

            if (Coords.IsEmpty)
                vals.Add(DBNull.Value);
            else
                vals.Add(Coords.X + "," + Coords.Y);


            Database.ExportWorldItem(table, vals);

        }

    }
}
