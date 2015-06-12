using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;
using DFWV.WorldClasses.HistoricalFigureClasses;

namespace DFWV.WorldClasses.HistoricalEventClasses
{

    class HE_HFNewPet : HistoricalEvent
    {
        private int? SiteID { get; set; }
        private Site Site { get; set; }
        private int? SubregionID { get; set; }
        private Region Subregion { get; set; }
        private int? FeatureLayerID { get; set; }
        private Point Coords { get; set; }
        private List<int> GroupHFIDs { get; set; }
        private List<Race> Pets { get; set; }
        private List<HistoricalFigure> GroupHFs { get; set; }

        override public Point Location { get { return Coords; } }
        public override IEnumerable<HistoricalFigure> HFsInvolved
        {
            get { return GroupHFs ?? Enumerable.Empty<HistoricalFigure>(); }
        }
        public override IEnumerable<Site> SitesInvolved
        {
            get { yield return Site; }
        }
        public override IEnumerable<Region> RegionsInvolved
        {
            get { yield return Subregion; }
        }

        public HE_HFNewPet(XDocument xdoc, World world)
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
                    case "group_hfid":
                        if (GroupHFIDs == null)
                            GroupHFIDs = new List<int>();
                        GroupHFIDs.Add(valI);
                        break;

                    default:
                        DFXMLParser.UnexpectedXMLElement(xdoc.Root.Name.LocalName + "\t" + Types[Type], element, xdoc.Root.ToString());
                        break;
                }
            }
        }
        internal override void Link()
        {
            //TODO: Incorporate new data
            base.Link();
            if (SiteID.HasValue && World.Sites.ContainsKey(SiteID.Value))
                Site = World.Sites[SiteID.Value];
            if (SubregionID.HasValue && World.Regions.ContainsKey(SubregionID.Value))
                Subregion = World.Regions[SubregionID.Value];
            if (GroupHFIDs == null) return;
            GroupHFs = new List<HistoricalFigure>();
            foreach (var grouphfid in GroupHFIDs.Where(grouphfid => World.HistoricalFigures.ContainsKey(grouphfid)))
            {
                GroupHFs.Add(World.HistoricalFigures[grouphfid]);
            }
        }

        internal override void Plus(XDocument xdoc)
        {
            foreach (var element in xdoc.Root.Elements())
            {
                var val = element.Value;
                int valI;
                int.TryParse(val, out valI);

                switch (element.Name.LocalName)
                {
                    case "id":
                    case "type":
                        break;
                    case "group":
                        if (GroupHFIDs == null)
                            GroupHFIDs = new List<int>();
                        if (!GroupHFIDs.Contains(valI))
                            GroupHFIDs.Add(valI);
                        break;
                    case "pets":
                        var race = World.GetAddRace(val);
                        if (Pets == null)
                            Pets = new List<Race>();
                        Pets.Add(race);
                        break;
                    case "site":
                        break;
                    default:
                        DFXMLParser.UnexpectedXMLElement(xdoc.Root.Name.LocalName + "\t" + Types[Type], element, xdoc.Root.ToString());
                        break;
                }
            }
        }

        protected override void WriteDataOnParent(MainForm frm, Control parent, ref Point location) //TODO: Test Display
        {
            foreach (var hf in GroupHFs)
                EventLabel(frm, parent, ref location, "HF:", hf);
            foreach (var pet in Pets)
                EventLabel(frm, parent, ref location, "Pet:", pet);
                            
            if (Site != null)
                EventLabel(frm, parent, ref location, "Site:", Site);
            if (Subregion != null)
                EventLabel(frm, parent, ref location, "Region:", Subregion);
            if (FeatureLayerID != null && FeatureLayerID.Value != -1)
                EventLabel(frm, parent, ref location, "Feature Layer:", FeatureLayerID == -1 ? "" : FeatureLayerID.ToString());
            if (Coords != Point.Empty) 
                EventLabel(frm, parent, ref location, "Coords:", new Coordinate(Coords));

        }

        protected override string LegendsDescription() //TODO: Test Display
        {
            //TODO: Incorporate new data (multiple GroupHFs)
            var timestring = base.LegendsDescription();

            if (Pets != null && Pets.Count == 1)
            {
                return string.Format("{0} {1} tamed the {2} of {3}.",
                    timestring, GroupHFs[0], Pets[0],
                    Subregion == null ? "UNKNOWN" : Subregion.ToString());
            }
            return string.Format("{0} {1} tamed the {2} of {3}.",
                timestring, GroupHFs[0], "UNKNOWN",
                Subregion == null ? "UNKNOWN" : Subregion.ToString());
        }

        internal override string ToTimelineString()
        {
            //TODO: Incorporate new data (multiple GroupHFs)
            var timelinestring = base.ToTimelineString();

            return string.Format("{0} {1} got a new pet.",
                                timelinestring, GroupHFs[0]);
        }

        internal override void Export(string table)
        {
            base.Export(table);

            table = GetType().Name;
            
            var vals = new List<object>
            {
                ID, 
                GroupHFIDs.DBExport(), 
                SiteID.DBExport(), 
                SubregionID.DBExport(), 
                FeatureLayerID.DBExport(),
                Coords.DBExport()
            };

            if (Pets != null)
            {
                var petExport = Pets.Aggregate("", (current, petRace) => current + (petRace.ToString() + ","));
                petExport = petExport.TrimEnd(',');
                vals.Add(petExport);
            }
            else
                vals.Add(DBNull.Value);

            Database.ExportWorldItem(table, vals);

        }

    }
}
