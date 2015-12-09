﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;
using DFWV.WorldClasses.EntityClasses;
using DFWV.WorldClasses.HistoricalFigureClasses;

namespace DFWV.WorldClasses.HistoricalEventClasses
{
    class HE_BodyAbused : HistoricalEvent
    {
        private int? SiteID { get; set; }
        private Site Site { get; set; }
        private int? SubregionID { get; set; }
        private Region Subregion { get; set; }
        private int? FeatureLayerID { get; set; }
        private Point Coords { get; set; }
        public Entity AbuserEn { private get; set; }

        public List<int> BodyHFIDs;
        public List<HistoricalFigure> BodyHFs;
        public int? AbuserEnID { get; set; }
        public int? AbuseType { get; set; }
        public int? HFID { get; set; }
        public HistoricalFigure HF { get; set; }
        public int? ItemType { get; set; }
        public int? ItemSubType { get; set; }
        public int? ItemMat { get; set; }
        private int? ItemMatType { get; set; }
        private int? ItemMatIndex { get; set; }
        public int? PileType { get; set; }
        
        override public Point Location => Coords != Point.Empty ? Coords : (Site != null ? Site.Location : Subregion.Location);

        public override IEnumerable<HistoricalFigure> HFsInvolved
        {
            get
            {
                yield return HF;
                if (BodyHFs != null)
                {
                    foreach (var historicalFigure in BodyHFs)
                        yield return historicalFigure;
                }
            }
        }
        public override IEnumerable<Site> SitesInvolved
        {
            get { yield return Site; }
        }
        public override IEnumerable<Region> RegionsInvolved
        {
            get { yield return Subregion; }
        }

        public override IEnumerable<Entity> EntitiesInvolved
        {
            get { yield return AbuserEn; }
        }
        public HE_BodyAbused(XDocument xdoc, World world)
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
                    default:
                        DFXMLParser.UnexpectedXMLElement(xdoc.Root.Name.LocalName + "\t" + Types[Type], element, xdoc.Root.ToString());
                        break;
                }
            }
        }

        internal override void Link()
        {
            base.Link();
            if (SiteID.HasValue && World.Sites.ContainsKey(SiteID.Value))
                Site = World.Sites[SiteID.Value];
            if (SubregionID.HasValue && World.Regions.ContainsKey(SubregionID.Value))
                Subregion = World.Regions[SubregionID.Value];
            if (BodyHFIDs != null)
            {
                foreach (var hfid in BodyHFIDs.Where(hfid => World.HistoricalFigures.ContainsKey(hfid)))
                {
                    if (BodyHFs == null)
                        BodyHFs = new List<HistoricalFigure>();
                    BodyHFs.Add(World.HistoricalFigures[hfid]);
                }
            }
            if (AbuserEnID.HasValue && World.Entities.ContainsKey(AbuserEnID.Value))
                AbuserEn = World.Entities[AbuserEnID.Value];
            if (HFID.HasValue && World.HistoricalFigures.ContainsKey(HFID.Value))
                HF = World.HistoricalFigures[HFID.Value];
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
                    case "bodies":
                        if (BodyHFIDs == null)
                            BodyHFIDs = new List<int>();
                        BodyHFIDs.Add(valI);
                        break;
                    case "civ":
                        if (valI != -1)
                            AbuserEnID = valI;
                        break;
                    case "site":
                        break;
                    case "props_item_type":
                        if (!ItemTypes.Contains(val))
                            ItemTypes.Add(val);
                        ItemType = ItemTypes.IndexOf(val);
                        break;
                    case "props_item_subtype":
                        if (!ItemSubTypes.Contains(val))
                            ItemSubTypes.Add(val);
                        ItemSubType = ItemSubTypes.IndexOf(val);
                        break;
                    case "props_item_mattype":
                    case "props_item_mat_type":
                        ItemMatType = valI;
                        break;
                    case "props_item_matindex":
                    case "props_item_mat_index":
                        ItemMatIndex = valI;
                        break;
                    case "props_item_mat":
                        if (!Materials.Contains(val))
                            Materials.Add(val);
                        ItemMat = Materials.IndexOf(val);
                        break;
                    case "props_pile_type":
                        PileType = -1;
                        break;
                    case "histfig":
                        HFID = valI;
                        break;
                    case "abuse_type":
                        AbuseType = valI;
                        break;
                    default:
                        DFXMLParser.UnexpectedXMLElement(xdoc.Root.Name.LocalName + "\t" + Types[Type], element, xdoc.Root.ToString());
                        break;
                }
            }
        }

        protected override void WriteDataOnParent(MainForm frm, Control parent, ref Point location)
        {
            EventLabel(frm, parent, ref location, "Abuser:", AbuserEn);
            EventLabel(frm, parent, ref location, "Abuser:", HF);
            if (BodyHFs != null)
            {
                foreach (var hf in BodyHFs)
                    EventLabel(frm, parent, ref location, "Victim:", hf);
            }

            EventLabel(frm, parent, ref location, "Site:", Site);
            EventLabel(frm, parent, ref location, "Region:", Subregion);
            if (FeatureLayerID != null && FeatureLayerID > -1)
                EventLabel(frm, parent, ref location, "Layer:", FeatureLayerID == -1 ? "" : FeatureLayerID.ToString());
            EventLabel(frm, parent, ref location, "Coords:", new Coordinate(Coords));
        }

        protected override string LegendsDescription() //Matched
        {
            var timestring = base.LegendsDescription();


            var locationtext = "";
            if (Site != null)
                locationtext = Site.AltName;
            else if (Subregion != null)
                locationtext = Subregion.ToString();


            var abusedHFtext = "UNKNOWN";

            if (BodyHFs != null && BodyHFs.Count == 1)
                abusedHFtext = $"the body of the {BodyHFs[0].Race.Name.ToLower()} {BodyHFs[0]} was ";
            else if (BodyHFs != null && BodyHFs.Count == 2)
                abusedHFtext =
                    $"the bodies of the {BodyHFs[0].Race.Name.ToLower()} {BodyHFs[0]} and the {BodyHFs[1].Race.Name.ToLower()} {BodyHFs[0]} were ";
            else if (BodyHFs == null)
                abusedHFtext = "the body of an unknown creature was";
            else if (BodyHFs != null && BodyHFs.Count > 2)
            {
                abusedHFtext = BodyHFs.TakeWhile(hf => hf != BodyHFs.Last()).Aggregate("the bodies of ", (current, hf) => current +
                                                                                                                          $"the {hf.Race.Name.ToLower()} {hf}, ");

                abusedHFtext += $"and the {BodyHFs.Last().Race.Name.ToLower()} {BodyHFs.Last()} ";
            }

            if (AbuserEn != null && PileType == -1 && ItemMat.HasValue && ItemType.HasValue)
            {
                if (ItemSubType.HasValue)
                {
                    return
                        $"{timestring} {abusedHFtext} impaled on a {Materials[ItemMat.Value] + " " + ItemSubTypes[ItemSubType.Value]} by {AbuserEn} in {locationtext}.";
                }
                return
                    $"{timestring} {abusedHFtext} impaled on a {Materials[ItemMat.Value] + " " + ItemTypes[ItemType.Value]} by {AbuserEn} in {locationtext}.";
            }
            if (AbuserEn != null && PileType == -1 && ItemMat == null && ItemType != null && ItemTypes[ItemType.Value] == "none")
                return $"{timestring} {abusedHFtext} horribly mutilated by {AbuserEn} in {locationtext}.";
            if (AbuserEn == null && HF != null && PileType == -1 && ItemMat == null)
                return $"{timestring} {abusedHFtext} animated by the {HF.Race} {HF} in {locationtext}.";
            if (AbuserEn == null && HF == null && PileType == -1 && ItemMat == null)
                return $"{timestring} {abusedHFtext} animated in {locationtext}.";



            return
                $"{timestring} {abusedHFtext} were added to a grisly mound by {(AbuserEn == null ? "UNKNOWN" : AbuserEn.ToString())} in {(Site == null ? "UNKNOWN" : Site.AltName)}.";

        }

        internal override string ToTimelineString()
        {
            //TODO: Incorporate new data
            var timelinestring = base.ToTimelineString();

            if (Site == null && AbuserEn == null)
                return $"{timelinestring} Bodies abused.";
            if (Site == null)
                return $"{timelinestring} Bodies abused by {AbuserEn}.";
            if (AbuserEn == null)
                return $"{timelinestring} Bodies abused at {Site.AltName}.";
            return $"{timelinestring} Bodies abused at {Site.AltName} by {AbuserEn}.";
        }

        internal override void Export(string table)
        {
            //TODO: Incorporate new data
            base.Export(table);

            table = GetType().Name;


            var vals = new List<object>
            {
                ID, 
                SiteID.DBExport(), 
                SubregionID.DBExport(), 
                FeatureLayerID.DBExport(),
                Coords.DBExport(),
                AbuserEnID.DBExport(),
                BodyHFIDs.DBExport(),
                ItemType.DBExport(ItemTypes),
                ItemSubType.DBExport(ItemSubTypes),
                ItemMat.DBExport(Materials),
                HFID.DBExport(),
                AbuseType.DBExport()
            };



            Database.ExportWorldItem(table, vals);


        }

    }
}