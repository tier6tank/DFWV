using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Linq;
using DFWV.WorldClasses.HistoricalFigureClasses;

namespace DFWV.WorldClasses.HistoricalEventClasses
{
    public class HE_HFDied : HistoricalEvent
    {
        private int? HFID { get; set; }
        public HistoricalFigure HF { get; private set; }
        private int? FeatureLayerID { get; set; }
        private int? SlayerHFID { get; set; }
        public HistoricalFigure SlayerHF { get; private set; }
        private int? SlayerItemID { get; set; }
        private Artifact SlayerItem { get; set; }
        private int? SlayerShooterItemID { get; set; }
        private Artifact SlayerShooterItem { get; set; }
        private string SlayerRace_ { get; set; }
        private Race SlayerRace { get; set; }
        private int SlayerCaste { get; set; }
        public int Cause { get; private set; }
        public static List<string> Causes = new List<string>();
        private int? SiteID { get; set; }
        public Site Site { get; private set; }
        private int? SubregionID { get; set; }
        public Region Subregion { get; private set; }

        private int? Item { get; set; }
        private int? ArtifactID { get; set; }
        private int? ItemType { get; set; }
        private int? ItemSubType { get; set; }
        private int? Mat { get; set; }
        private int? MatType { get; set; }
        private int? MatIndex  { get; set; }

        private int? BowItem { get; set; }
        private int? BowArtifactID { get; set; }
        private int? BowItemType { get; set; }
        private int? BowItemSubType { get; set; }
        private int? BowMat { get; set; }
        private int? BowMatType { get; set; }
        private int? BowMatIndex  { get; set; }

        override public Point Location => Site != null ? Site.Location : (Subregion != null ? Subregion.Location : Point.Empty);

        public override IEnumerable<HistoricalFigure> HFsInvolved
        {
            get
            {
                yield return HF;
                yield return SlayerHF;
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

        public HE_HFDied(XDocument xdoc, World world)
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
                    case "hfid":
                        HFID = valI;
                        break;
                    case "slayer_hfid":
                        if (valI != -1)
                            SlayerHFID = valI;
                        break;
                    case "slayer_race":
                        SlayerRace_ = val;
                        break;
                    case "slayer_caste":
                        if (!HistoricalFigure.Castes.Contains(val))
                            HistoricalFigure.Castes.Add(val);
                        SlayerCaste = HistoricalFigure.Castes.IndexOf(val);
                        break;
                    case "slayer_item_id":
                        if (valI != -1)
                            SlayerItemID = valI;
                        break;
                    case "slayer_shooter_item_id":
                        if (valI != -1)
                            SlayerShooterItemID = valI;
                        break;
                    case "cause":
                        if (!Causes.Contains(val))
                            Causes.Add(val);
                        Cause = Causes.IndexOf(val);
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
            if (HFID.HasValue && World.HistoricalFigures.ContainsKey(HFID.Value))
                HF = World.HistoricalFigures[HFID.Value];
            if (SiteID.HasValue && World.Sites.ContainsKey(SiteID.Value))
                Site = World.Sites[SiteID.Value];
            if (SubregionID.HasValue && World.Regions.ContainsKey(SubregionID.Value))
                Subregion = World.Regions[SubregionID.Value];
            if (SlayerHFID.HasValue && World.HistoricalFigures.ContainsKey(SlayerHFID.Value))
                SlayerHF = World.HistoricalFigures[SlayerHFID.Value];
            if (SlayerRace_ != null)
            {
                SlayerRace = World.GetAddRace(SlayerRace_);
                SlayerRace_ = null;
            }
            if (ArtifactID.HasValue && World.Artifacts.ContainsKey(ArtifactID.Value))
                SlayerItem = World.Artifacts[ArtifactID.Value];
            if (BowArtifactID.HasValue && World.Artifacts.ContainsKey(BowArtifactID.Value))
                SlayerShooterItem = World.Artifacts[BowArtifactID.Value];
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
                    case "victim_hf":
                    case "slayer_hf":
                    case "slayer_race":
                    case "slayer_caste":
                    case "site":
                    case "death_cause":
                        break;
                    case "item":
                        Item = valI;
                        break;
                    case "artifact_id":
                        ArtifactID = valI;
                        break;
                    case "item_type":
                        if (!ItemTypes.Contains(val))
                            ItemTypes.Add(val);
                        ItemType = ItemTypes.IndexOf(val);
                        break;
                    case "item_subtype":
                        if (!ItemSubTypes.Contains(val))
                            ItemSubTypes.Add(val);
                        ItemSubType = ItemSubTypes.IndexOf(val);
                        break;
                    case "mattype":
                        MatType = valI;
                        break;
                    case "matindex":
                        MatIndex = valI;
                        break;
                    case "mat":
                        if (!Materials.Contains(val))
                            Materials.Add(val);
                        Mat = Materials.IndexOf(val);
                        break;
                    case "bow_item":
                        BowItem = valI;
                        break;
                    case "bow_artifact_id":
                        BowArtifactID = valI;
                        break;
                    case "bow_item_type":
                        if (!ItemTypes.Contains(val))
                            ItemTypes.Add(val);
                        BowItemType = ItemTypes.IndexOf(val);
                        break;
                    case "bow_item_subtype":
                        if (!ItemSubTypes.Contains(val))
                            ItemSubTypes.Add(val);
                        BowItemSubType = ItemSubTypes.IndexOf(val);
                        break;
                    case "bow_mattype":
                        BowMatType = valI;
                        break;
                    case "bow_matindex":
                        BowMatIndex = valI;
                        break;
                    case "bow_mat":
                        if (!Materials.Contains(val))
                            Materials.Add(val);
                        BowMat = Materials.IndexOf(val);
                        break;
                    default:
                        DFXMLParser.UnexpectedXMLElement(xdoc.Root.Name.LocalName + "\t" + Types[Type], element, xdoc.Root.ToString());
                        break;
                }
            }
        }

        internal override void Process()
        {
            base.Process();
            if (HF != null)
                HF.DiedEvent = this;
            if (SlayerHF != null)
            {
                if (SlayerHF.SlayingEvents == null)
                    SlayerHF.SlayingEvents = new List<HE_HFDied>();
                SlayerHF.SlayingEvents.Add(this);
            }
            if (SlayerItem != null)
            {
                if (SlayerItem.Kills == null)
                    SlayerItem.Kills = new List<HE_HFDied>();
                SlayerItem.Kills.Add(this);
            }
            if (SlayerShooterItem == null) return;
            if (SlayerShooterItem.Kills == null)
                SlayerShooterItem.Kills = new List<HE_HFDied>();
            SlayerShooterItem.Kills.Add(this);
        }

        protected override void WriteDataOnParent(MainForm frm, Control parent, ref Point location)
        {
            //TODO: Incorporate new data
            EventLabel(frm, parent, ref location, "HF:", HF);
            EventLabel(frm, parent, ref location, "Cause:", Causes[Cause]);
            if (SlayerItem != null)
                EventLabel(frm, parent, ref location, "Weapon:", SlayerItem);
            if (SlayerShooterItem != null)
                EventLabel(frm, parent, ref location, "Bow:", SlayerShooterItem);
            if (SlayerHF != null)
                EventLabel(frm, parent, ref location, "Slayer:", SlayerHF);
            else if (SlayerHFID.HasValue)
                EventLabel(frm, parent, ref location, "Slayer:", SlayerHFID.ToString());
            if (SlayerRace != null)
                EventLabel(frm, parent, ref location, " Race:", SlayerRace);
            else if (SlayerRace_ != null)
                EventLabel(frm, parent, ref location, " Race:", SlayerRace_);
            EventLabel(frm, parent, ref location, " Caste:", HistoricalFigure.Castes[SlayerCaste]);
            if (Site != null)
                EventLabel(frm, parent, ref location, "Site:", Site);
            else if (SiteID.HasValue)
                EventLabel(frm, parent, ref location, "Site:", SiteID.ToString());
            if (Subregion != null)
                EventLabel(frm, parent, ref location, "Region:", Subregion);
            else if (SubregionID.HasValue)
                EventLabel(frm, parent, ref location, "Region:", SubregionID.ToString());

        }

        protected override string LegendsDescription() //Not Matched (Update commented out items)
        {
            var timestring = base.LegendsDescription();

            var locationText = "";
            if (Site != null)
                locationText = Site.AltName;
            else if (Subregion != null)
                locationText = Subregion.ToString();

            switch (Causes[Cause])
            {
                case "struck":
                    if (Item == null)
                        return
                            $"{timestring} the {HF.Race} {HF} was struck down by \nthe {(SlayerRace == null ? "" : SlayerRace.ToString())} {(SlayerHF == null ? SlayerHFID.ToString() : SlayerHF.ToString())} in {locationText}.";
                    if (SlayerItem != null)
                        return
                            $"{timestring} the {HF.Race} {HF} was struck down by \n the {(SlayerRace == null ? "" : SlayerRace.ToString())} {(SlayerHF == null ? SlayerHFID.ToString() : SlayerHF.ToString())} with {SlayerItem} in {locationText}.";
                    if (ItemType != null && ItemTypes[ItemType.Value] == "corpsepiece")
                        return
                            $"{timestring} the {HF.Race} {HF} was struck down by \nthe {(SlayerRace == null ? "" : SlayerRace.ToString())} {(SlayerHF == null ? SlayerHFID.ToString() : SlayerHF.ToString())} {"with a body part"} in {locationText}.";
                    if (ItemType != null && ItemTypes[ItemType.Value] == "corpse")
                        return
                            $"{timestring} the {HF.Race} {HF} was struck down by \nthe {(SlayerRace == null ? "" : SlayerRace.ToString())} {(SlayerHF == null ? SlayerHFID.ToString() : SlayerHF.ToString())} {"with a corpse"} in {locationText}.";
                    if (Mat != null && ItemType != null && ItemSubType == null) //Test
                        return
                            $"{timestring} the {HF.Race} {HF} was struck down by \nthe {(SlayerRace == null ? "" : SlayerRace.ToString())} {(SlayerHF == null ? SlayerHFID.ToString() : SlayerHF.ToString())} {"with a " + Materials[Mat.Value] + " " + ItemTypes[ItemType.Value]} in {locationText}.";
                    if (Mat != null && ItemType != null && ItemSubType != null)
                        return
                            $"{timestring} the {HF.Race} {HF} was struck down by \nthe {(SlayerRace == null ? "" : SlayerRace.ToString())} {(SlayerHF == null ? SlayerHFID.ToString() : SlayerHF.ToString())} {"with a " + Materials[Mat.Value] + " " + ItemSubTypes[ItemSubType.Value]} in {locationText}.";
                    return
                        $"{timestring} the {HF.Race} {HF} was struck down by \nthe {(SlayerRace == null ? "" : SlayerRace.ToString())} {(SlayerHF == null ? SlayerHFID.ToString() : SlayerHF.ToString())} {"with "} in {locationText}.";
                case "shot":
                    if (BowItem == null)
                    {
                        if (Mat != null && ItemType != null && ItemSubType != null)
                            return
                                $"{timestring} the {HF.Race} {HF} was shot and killed by \nthe {(SlayerRace == null ? "" : SlayerRace.ToString())} {(SlayerHF == null ? SlayerHFID.ToString() : SlayerHF.ToString())} {"with a " + Materials[Mat.Value] + " " + ItemSubTypes[ItemSubType.Value]} in {locationText}.";
                        return
                            $"{timestring} the {HF.Race} {HF} was shot and killed by \nthe {(SlayerRace == null ? "" : SlayerRace.ToString())} {(SlayerHF == null ? SlayerHFID.ToString() : SlayerHF.ToString())} in {locationText}.";
                    }
                    if (SlayerShooterItem != null)
                        return
                            $"{timestring} the {HF.Race} {HF} was shot and killed by \n the {(SlayerRace == null ? "" : SlayerRace.ToString())} {(SlayerHF == null ? SlayerHFID.ToString() : SlayerHF.ToString())} with {SlayerShooterItem} in {locationText}.";
                    if (BowMat != null && BowItemType != null && BowItemSubType == null && ItemType != null && Mat != null) //Test
                        return
                            $"{timestring} the {HF.Race} {HF} was shot and killed by \nthe {(SlayerRace == null ? "" : SlayerRace.ToString())} {(SlayerHF == null ? SlayerHFID.ToString() : SlayerHF.ToString())} {"with a " + Materials[Mat.Value] + " " + ItemTypes[ItemType.Value]} in {locationText}.";
                    if (BowMat != null && BowItemType != null && BowItemSubType != null && Mat != null && ItemType != null && ItemSubType != null)
                        return
                            $"{timestring} the {HF.Race} {HF} was shot and killed by \nthe {(SlayerRace == null ? "" : SlayerRace.ToString())} {(SlayerHF == null ? SlayerHFID.ToString() : SlayerHF.ToString())} {"with a " + Materials[Mat.Value] + " " + ItemSubTypes[ItemSubType.Value] + " from a " + Materials[BowMat.Value] + " " + ItemSubTypes[BowItemSubType.Value]} in {locationText}.";
                    return
                        $"{timestring} the {HF.Race} {HF} was shot and killed by \nthe {(SlayerRace == null ? "" : SlayerRace.ToString())} {(SlayerHF == null ? SlayerHFID.ToString() : SlayerHF.ToString())} {"with "} in {locationText}.";
                case "murdered":
                    return
                        $"{timestring} the {HF.Race} {HF} was murdered by \nthe {(SlayerRace == null ? "" : SlayerRace.ToString())} {(SlayerHF == null ? SlayerHFID.ToString() : SlayerHF.ToString())} in {locationText}.";
                case "old age":
                    return $"{timestring} the {HF.Race} {HF} died of old age.";
                case "infection":
                    return
                        $"{timestring} the {HF.Race} {HF} succumbed to infection, slain by \nthe {(SlayerRace == null ? "" : SlayerRace.ToString())} {(SlayerHF == null ? SlayerHFID.ToString() : SlayerHF.ToString())} in {locationText}.";
                case "blood":
                    if (SlayerHF == null)
                        return $"{timestring} the {HF.Race} {HF} bled to death in {locationText}.";
                    return
                        $"{timestring} the {HF.Race} {HF} bled to death, slain by the {(SlayerRace == null ? "" : SlayerRace.ToString())} {(SlayerHF == null ? SlayerHFID.ToString() : SlayerHF.ToString())} with ITEM: {SlayerItemID} in {locationText}.";
                case "thirst":
                    return $"{timestring} the {HF.Race} {HF} died of thirst in {locationText}.";
                case "collapsed":
                    return
                        $"{timestring} the {HF.Race} {HF} collapsed, struck down by \nthe {(SlayerRace == null ? "" : SlayerRace.ToString())} {(SlayerHF == null ? SlayerHFID.ToString() : SlayerHF.ToString())} in {locationText}.";
                case "vanish":
                    return $"{timestring} the {HF.Race} {HF} vanished in {locationText}.";
                case "drown":
                    return $"{timestring} the {HF.Race} {HF} drowned in {locationText}.";
                case "crushed bridge":
                    return $"{timestring} the {HF.Race} {HF} was crushed by a drawbridge in {locationText}.";
                case "put to rest":
                    return $"{timestring} the {HF.Race} {HF} was put to rest in {locationText}.";
                case "quitdead":
                    return $"{timestring} the {HF.Race} {HF} starved to death in {locationText}.";
                case "exec beheaded":
                    return
                        $"{timestring} the {HF.Race} {HF} was beheaded by \nthe {(SlayerRace == null ? "" : SlayerRace.ToString())} {(SlayerHF == null ? SlayerHFID.ToString() : SlayerHF.ToString())} in {locationText}.";
                case "exec burned alive":
                    return
                        $"{timestring} the {HF.Race} {HF} burned \nthe {(SlayerRace == null ? "" : SlayerRace.ToString())} {(SlayerHF == null ? SlayerHFID.ToString() : SlayerHF.ToString())} alive in {locationText}.";
                case "exec fed to beasts":
                    return
                        $"{timestring} the {HF.Race} {HF} fed \nthe {(SlayerRace == null ? "" : SlayerRace.ToString())} {(SlayerHF == null ? SlayerHFID.ToString() : SlayerHF.ToString())} to beasts in {locationText}.";
                case "exec hacked to pieces":
                    return
                        $"{timestring} the {HF.Race} {HF} hacked \nthe {(SlayerRace == null ? "" : SlayerRace.ToString())} {(SlayerHF == null ? SlayerHFID.ToString() : SlayerHF.ToString())} to pieces in {locationText}.";
                case "exec drowned":
                    return
                        $"{timestring} the {HF.Race} {HF} drowned \nthe {(SlayerRace == null ? "" : SlayerRace.ToString())} {(SlayerHF == null ? SlayerHFID.ToString() : SlayerHF.ToString())} in {locationText}.";
                case "exec buried alive":
                    return
                        $"{timestring} the {HF.Race} {HF} buried \nthe {(SlayerRace == null ? "" : SlayerRace.ToString())} {(SlayerHF == null ? SlayerHFID.ToString() : SlayerHF.ToString())} alive in {locationText}.";
                case "exec crucified":
                    return
                        $"{timestring} the {HF.Race} {HF} was crucified by \nthe {(SlayerRace == null ? "" : SlayerRace.ToString())} {(SlayerHF == null ? SlayerHFID.ToString() : SlayerHF.ToString())} alive in {locationText}.";
                case "air":
                    return
                        $"{timestring} the {HF.Race} {HF} suffocated, slain by {(SlayerRace == null ? "" : SlayerRace.ToString())} {(SlayerHF == null ? SlayerHFID.ToString() : SlayerHF.ToString())} alive in {locationText}.";
                case "obstacle":
                    return
                        $"{timestring} the {HF.Race} {HF} died after colliding with an obstacle, slain by {(SlayerRace == null ? "" : SlayerRace.ToString())} {(SlayerHF == null ? SlayerHFID.ToString() : SlayerHF.ToString())} in {locationText}.";
                case "hunger":
                    return $"{timestring} the {HF.Race} {HF} starved to death in {locationText}.";
                case "scuttled":
                    return string.Format("{0} {2} a {1} was scuttled in {3}.",
                        timestring, HF.Race, HF,
                        locationText);
                case "spikes":
                    return $"{timestring} the {HF.Race} {HF} was impaled on spikes in {locationText}.";
                case "scared to death":
                    return
                        $"{timestring} the {HF.Race} {HF} was scared to death by \nthe {(SlayerRace == null ? "" : SlayerRace.ToString())} {(SlayerHF == null ? SlayerHFID.ToString() : SlayerHF.ToString())} in {locationText}.";
                case "slaughtered":
                    return string.Format("{0} {2} a {1} was slaughtered in {5} by {4} a {3}.",
                        timestring, HF.Race, HF,
                        SlayerRace == null ? "" : SlayerRace.ToString(), SlayerHF == null ? SlayerHFID.ToString() : SlayerHF.ToString(),
                        locationText);
                case "trap":
                    return $"{timestring} the {HF.Race} {HF} was killed by a trap in {locationText}.";
                case "blood drained":
                    return
                        $"{timestring} the {HF.Race} {HF} was drained of blood by \nthe {(SlayerRace == null ? "" : SlayerRace.ToString())} {(SlayerHF == null ? SlayerHFID.ToString() : SlayerHF.ToString())} in {locationText}.";

            }

            return timestring;
        }

        internal override string ToTimelineString()
        {
            //TODO: Incorporate new data
            var timelinestring = base.ToTimelineString();
            if (Site == null)
            {
                if (SlayerHF == null)
                    return $"{timelinestring} {(HF != null ? HF.ToString() : HFID.ToString())} died.";
                return $"{timelinestring} {(HF != null ? HF.ToString() : HFID.ToString())} killed by {SlayerHF}.";
            }
            if (SlayerHF == null)
                return $"{timelinestring} {(HF != null ? HF.ToString() : HFID.ToString())} died at {Site.AltName}";
            return
                $"{timelinestring} {(HF != null ? HF.ToString() : HFID.ToString())} killed at {Site.AltName} by {SlayerHF}";
        }

        internal override void Export(string table)
        {
            base.Export(table);

            table = GetType().Name;

            var vals = new List<object>
            {
                ID, 
                HFID.DBExport(), 
                SlayerHFID.DBExport(), 
                SlayerRace.DBExport(),
                SlayerCaste.DBExport(HistoricalFigure.Castes),
                SlayerItemID.DBExport(), 
                SlayerShooterItemID.DBExport(), 
                Cause.DBExport(Causes), 
                SiteID.DBExport(), 
                SubregionID.DBExport(), 
                FeatureLayerID.DBExport(),
                Item.DBExport(),
                ArtifactID.DBExport(),
                ItemType.DBExport(ItemTypes),
                ItemSubType.DBExport(ItemSubTypes),
                Mat.DBExport(Materials),
                BowItem.DBExport(),
                BowArtifactID.DBExport(),
                BowItemType.DBExport(ItemTypes),
                BowItemSubType.DBExport(ItemSubTypes),
                BowMat.DBExport(Materials)
            };

            Database.ExportWorldItem(table, vals);

        }

    }
}
