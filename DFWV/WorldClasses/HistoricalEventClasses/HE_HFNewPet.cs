﻿using System;
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
        private int? SiteId { get; }
        private Site Site { get; set; }
        private int? SubregionId { get; }
        private Region Subregion { get; set; }
        private int? FeatureLayerId { get; }
        private Point Coords { get; }
        private List<int> HfIds { get; set; }
        private List<Race> Pets { get; set; }
        private List<HistoricalFigure> Hfs { get; set; }

        override public Point Location => Coords;

        public override IEnumerable<HistoricalFigure> HFsInvolved => Hfs ?? Enumerable.Empty<HistoricalFigure>();

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
                    case "group_hfid":
                        if (HfIds == null)
                            HfIds = new List<int>();
                        HfIds.Add(valI);
                        break;

                    default:
                        DFXMLParser.UnexpectedXmlElement(xdoc.Root.Name.LocalName + "\t" + Types[Type], element, xdoc.Root.ToString());
                        break;
                }
            }
        }
        internal override void Link()
        {
            //TODO: Incorporate new data
            base.Link();
            if (HfIds == null) return;
            Hfs = new List<HistoricalFigure>();
            foreach (var grouphfid in HfIds.Where(grouphfid => World.HistoricalFigures.ContainsKey(grouphfid)))
            {
                Hfs.Add(World.HistoricalFigures[grouphfid]);
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
                        if (HfIds == null)
                            HfIds = new List<int>();
                        if (!HfIds.Contains(valI))
                            HfIds.Add(valI);
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
                        DFXMLParser.UnexpectedXmlElement(xdoc.Root.Name.LocalName + "\t" + Types[Type], element, xdoc.Root.ToString());
                        break;
                }
            }
        }

        protected override void WriteDataOnParent(MainForm frm, Control parent, ref Point location) //TODO: Test Display
        {
            foreach (var hf in Hfs)
                EventLabel(frm, parent, ref location, "HF:", hf);
            if (Pets != null)
            {
                foreach (var pet in Pets)
                    EventLabel(frm, parent, ref location, "Pet:", pet);
            }            
            if (Site != null)
                EventLabel(frm, parent, ref location, "Site:", Site);
            if (Subregion != null)
                EventLabel(frm, parent, ref location, "Region:", Subregion);
            if (FeatureLayerId != null && FeatureLayerId.Value != -1)
                EventLabel(frm, parent, ref location, "Feature Layer:", FeatureLayerId == -1 ? "" : FeatureLayerId.ToString());
            if (Coords != Point.Empty) 
                EventLabel(frm, parent, ref location, "Coords:", new Coordinate(Coords));

        }

        protected override string LegendsDescription() //TODO: Test Display
        {
            //TODO: Incorporate new data (multiple GroupHFs)
            var timestring = base.LegendsDescription();

            if (Pets != null && Pets.Count == 1)
            {
                return
                    $"{timestring} {Hfs[0]} tamed the {Pets[0]} of {Subregion?.ToString() ?? "UNKNOWN"}.";
            }
            return
                $"{timestring} {Hfs[0]} tamed the {"UNKNOWN"} of {Subregion?.ToString() ?? "UNKNOWN"}.";
        }

        internal override string ToTimelineString()
        {
            //TODO: Incorporate new data (multiple GroupHFs)
            var timelinestring = base.ToTimelineString();

            return $"{timelinestring} {Hfs[0]} got a new pet.";
        }

        internal override void Export(string table)
        {
            base.Export(table);

            table = GetType().Name;
            
            var vals = new List<object>
            {
                Id, 
                HfIds.DBExport(), 
                SiteId.DBExport(), 
                SubregionId.DBExport(), 
                FeatureLayerId.DBExport(),
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
