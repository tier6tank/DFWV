using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Linq;
using DFWV.WorldClasses.HistoricalFigureClasses;

namespace DFWV.WorldClasses.HistoricalEventClasses
{
    public class HE_HFDied : HistoricalEvent
    {
        private int? Hfid { get; }
        public HistoricalFigure Hf { get; private set; }
        private int? FeatureLayerId { get; }
        private int? SlayerHfid { get; }
        public HistoricalFigure SlayerHf { get; private set; }
        private int? SlayerItemId { get; }
        private Artifact SlayerItem { get; set; }
        private int? SlayerShooterItemId { get; }
        private Artifact SlayerShooterItem { get; set; }
        private string SlayerRace_ { get; set; }
        private Race SlayerRace { get; set; }
        private int SlayerCaste { get; }
        public int Cause { get; }
        public static List<string> Causes = new List<string>();
        private int? SiteId { get; }
        public Site Site { get; private set; }
        private int? SubregionId { get; }
        public Region Subregion { get; private set; }

        private int? Item { get; set; }
        private int? ArtifactId { get; set; }
        private int? ItemType { get; set; }
        private int? ItemSubType { get; set; }
        private int? Mat { get; set; }
        private int? MatType { get; set; }
        private int? MatIndex  { get; set; }

        private int? BowItem { get; set; }
        private int? BowArtifactId { get; set; }
        private int? BowItemType { get; set; }
        private int? BowItemSubType { get; set; }
        private int? BowMat { get; set; }
        private int? BowMatType { get; set; }
        private int? BowMatIndex  { get; set; }

        override public Point Location => Site?.Location ?? (Subregion?.Location ?? Point.Empty);

        public override IEnumerable<HistoricalFigure> HFsInvolved
        {
            get
            {
                yield return Hf;
                yield return SlayerHf;
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
                    case "hfid":
                        Hfid = valI;
                        break;
                    case "slayer_hfid":
                        if (valI != -1)
                            SlayerHfid = valI;
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
                            SlayerItemId = valI;
                        break;
                    case "slayer_shooter_item_id":
                        if (valI != -1)
                            SlayerShooterItemId = valI;
                        break;
                    case "cause":
                        if (!Causes.Contains(val))
                            Causes.Add(val);
                        Cause = Causes.IndexOf(val);
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
            if (Hfid.HasValue && World.HistoricalFigures.ContainsKey(Hfid.Value))
                Hf = World.HistoricalFigures[Hfid.Value];
            if (SiteId.HasValue && World.Sites.ContainsKey(SiteId.Value))
                Site = World.Sites[SiteId.Value];
            if (SubregionId.HasValue && World.Regions.ContainsKey(SubregionId.Value))
                Subregion = World.Regions[SubregionId.Value];
            if (SlayerHfid.HasValue && World.HistoricalFigures.ContainsKey(SlayerHfid.Value))
                SlayerHf = World.HistoricalFigures[SlayerHfid.Value];
            if (SlayerRace_ != null)
            {
                SlayerRace = World.GetAddRace(SlayerRace_);
                SlayerRace_ = null;
            }
            if (ArtifactId.HasValue && World.Artifacts.ContainsKey(ArtifactId.Value))
                SlayerItem = World.Artifacts[ArtifactId.Value];
            if (BowArtifactId.HasValue && World.Artifacts.ContainsKey(BowArtifactId.Value))
                SlayerShooterItem = World.Artifacts[BowArtifactId.Value];
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
                        ArtifactId = valI;
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
                        BowArtifactId = valI;
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
                        DFXMLParser.UnexpectedXmlElement(xdoc.Root.Name.LocalName + "\t" + Types[Type], element, xdoc.Root.ToString());
                        break;
                }
            }
        }

        internal override void Process()
        {
            base.Process();
            if (Hf != null)
                Hf.DiedEvent = this;
            if (SlayerHf != null)
            {
                if (SlayerHf.SlayingEvents == null)
                    SlayerHf.SlayingEvents = new List<HE_HFDied>();
                SlayerHf.SlayingEvents.Add(this);
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
            EventLabel(frm, parent, ref location, "HF:", Hf);
            EventLabel(frm, parent, ref location, "Cause:", Causes[Cause]);
            if (SlayerItem != null)
                EventLabel(frm, parent, ref location, "Weapon:", SlayerItem);
            if (SlayerShooterItem != null)
                EventLabel(frm, parent, ref location, "Bow:", SlayerShooterItem);
            if (SlayerHf != null)
                EventLabel(frm, parent, ref location, "Slayer:", SlayerHf);
            else if (SlayerHfid.HasValue)
                EventLabel(frm, parent, ref location, "Slayer:", SlayerHfid.ToString());
            if (SlayerRace != null)
                EventLabel(frm, parent, ref location, " Race:", SlayerRace);
            else if (SlayerRace_ != null)
                EventLabel(frm, parent, ref location, " Race:", SlayerRace_);
            EventLabel(frm, parent, ref location, " Caste:", HistoricalFigure.Castes[SlayerCaste]);
            if (Site != null)
                EventLabel(frm, parent, ref location, "Site:", Site);
            else if (SiteId.HasValue)
                EventLabel(frm, parent, ref location, "Site:", SiteId.ToString());
            if (Subregion != null)
                EventLabel(frm, parent, ref location, "Region:", Subregion);
            else if (SubregionId.HasValue)
                EventLabel(frm, parent, ref location, "Region:", SubregionId.ToString());

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
                            $"{timestring} the {Hf.Race} {Hf} was struck down by \nthe {SlayerRace?.ToString() ?? ""} {SlayerHf?.ToString() ?? SlayerHfid.ToString()} in {locationText}.";
                    if (SlayerItem != null)
                        return
                            $"{timestring} the {Hf.Race} {Hf} was struck down by \n the {SlayerRace?.ToString() ?? ""} {SlayerHf?.ToString() ?? SlayerHfid.ToString()} with {SlayerItem} in {locationText}.";
                    if (ItemType != null && ItemTypes[ItemType.Value] == "corpsepiece")
                        return
                            $"{timestring} the {Hf.Race} {Hf} was struck down by \nthe {SlayerRace?.ToString() ?? ""} {SlayerHf?.ToString() ?? SlayerHfid.ToString()} {"with a body part"} in {locationText}.";
                    if (ItemType != null && ItemTypes[ItemType.Value] == "corpse")
                        return
                            $"{timestring} the {Hf.Race} {Hf} was struck down by \nthe {SlayerRace?.ToString() ?? ""} {SlayerHf?.ToString() ?? SlayerHfid.ToString()} {"with a corpse"} in {locationText}.";
                    if (Mat != null && ItemType != null && ItemSubType == null) //Test
                        return
                            $"{timestring} the {Hf.Race} {Hf} was struck down by \nthe {SlayerRace?.ToString() ?? ""} {SlayerHf?.ToString() ?? SlayerHfid.ToString()} {"with a " + Materials[Mat.Value] + " " + ItemTypes[ItemType.Value]} in {locationText}.";
                    if (Mat != null && ItemType != null && ItemSubType != null)
                        return
                            $"{timestring} the {Hf.Race} {Hf} was struck down by \nthe {SlayerRace?.ToString() ?? ""} {SlayerHf?.ToString() ?? SlayerHfid.ToString()} {"with a " + Materials[Mat.Value] + " " + ItemSubTypes[ItemSubType.Value]} in {locationText}.";
                    return
                        $"{timestring} the {Hf.Race} {Hf} was struck down by \nthe {SlayerRace?.ToString() ?? ""} {SlayerHf?.ToString() ?? SlayerHfid.ToString()} {"with "} in {locationText}.";
                case "shot":
                    if (BowItem == null)
                    {
                        if (Mat != null && ItemType != null && ItemSubType != null)
                            return
                                $"{timestring} the {Hf.Race} {Hf} was shot and killed by \nthe {SlayerRace?.ToString() ?? ""} {SlayerHf?.ToString() ?? SlayerHfid.ToString()} {"with a " + Materials[Mat.Value] + " " + ItemSubTypes[ItemSubType.Value]} in {locationText}.";
                        return
                            $"{timestring} the {Hf.Race} {Hf} was shot and killed by \nthe {SlayerRace?.ToString() ?? ""} {SlayerHf?.ToString() ?? SlayerHfid.ToString()} in {locationText}.";
                    }
                    if (SlayerShooterItem != null)
                        return
                            $"{timestring} the {Hf.Race} {Hf} was shot and killed by \n the {SlayerRace?.ToString() ?? ""} {SlayerHf?.ToString() ?? SlayerHfid.ToString()} with {SlayerShooterItem} in {locationText}.";
                    if (BowMat != null && BowItemType != null && BowItemSubType == null && ItemType != null && Mat != null) //Test
                        return
                            $"{timestring} the {Hf.Race} {Hf} was shot and killed by \nthe {SlayerRace?.ToString() ?? ""} {SlayerHf?.ToString() ?? SlayerHfid.ToString()} {"with a " + Materials[Mat.Value] + " " + ItemTypes[ItemType.Value]} in {locationText}.";
                    if (BowMat != null && BowItemType != null && BowItemSubType != null && Mat != null && ItemType != null && ItemSubType != null)
                        return
                            $"{timestring} the {Hf.Race} {Hf} was shot and killed by \nthe {SlayerRace?.ToString() ?? ""} {SlayerHf?.ToString() ?? SlayerHfid.ToString()} {"with a " + Materials[Mat.Value] + " " + ItemSubTypes[ItemSubType.Value] + " from a " + Materials[BowMat.Value] + " " + ItemSubTypes[BowItemSubType.Value]} in {locationText}.";
                    return
                        $"{timestring} the {Hf.Race} {Hf} was shot and killed by \nthe {SlayerRace?.ToString() ?? ""} {SlayerHf?.ToString() ?? SlayerHfid.ToString()} {"with "} in {locationText}.";
                case "murdered":
                    return
                        $"{timestring} the {Hf.Race} {Hf} was murdered by \nthe {SlayerRace?.ToString() ?? ""} {SlayerHf?.ToString() ?? SlayerHfid.ToString()} in {locationText}.";
                case "old age":
                    return $"{timestring} the {Hf.Race} {Hf} died of old age.";
                case "infection":
                    return
                        $"{timestring} the {Hf.Race} {Hf} succumbed to infection, slain by \nthe {SlayerRace?.ToString() ?? ""} {SlayerHf?.ToString() ?? SlayerHfid.ToString()} in {locationText}.";
                case "blood":
                    return SlayerHf == null ? 
                        $"{timestring} the {Hf.Race} {Hf} bled to death in {locationText}." : 
                        $"{timestring} the {Hf.Race} {Hf} bled to death, slain by the {SlayerRace?.ToString() ?? ""} {SlayerHf} with ITEM: {SlayerItemId} in {locationText}.";
                case "thirst":
                    return $"{timestring} the {Hf.Race} {Hf} died of thirst in {locationText}.";
                case "collapsed":
                    return
                        $"{timestring} the {Hf.Race} {Hf} collapsed, struck down by \nthe {SlayerRace?.ToString() ?? ""} {SlayerHf?.ToString() ?? SlayerHfid.ToString()} in {locationText}.";
                case "vanish":
                    return $"{timestring} the {Hf.Race} {Hf} vanished in {locationText}.";
                case "drown":
                    return $"{timestring} the {Hf.Race} {Hf} drowned in {locationText}.";
                case "crushed bridge":
                    return $"{timestring} the {Hf.Race} {Hf} was crushed by a drawbridge in {locationText}.";
                case "put to rest":
                    return $"{timestring} the {Hf.Race} {Hf} was put to rest in {locationText}.";
                case "quitdead":
                    return $"{timestring} the {Hf.Race} {Hf} starved to death in {locationText}.";
                case "exec beheaded":
                    return
                        $"{timestring} the {Hf.Race} {Hf} was beheaded by \nthe {SlayerRace?.ToString() ?? ""} {SlayerHf?.ToString() ?? SlayerHfid.ToString()} in {locationText}.";
                case "exec burned alive":
                    return
                        $"{timestring} the {Hf.Race} {Hf} burned \nthe {(SlayerRace?.ToString() ?? "")} {(SlayerHf?.ToString() ?? SlayerHfid.ToString())} alive in {locationText}.";
                case "exec fed to beasts":
                    return
                        $"{timestring} the {Hf.Race} {Hf} fed \nthe {(SlayerRace?.ToString() ?? "")} {(SlayerHf?.ToString() ?? SlayerHfid.ToString())} to beasts in {locationText}.";
                case "exec hacked to pieces":
                    return
                        $"{timestring} the {Hf.Race} {Hf} hacked \nthe {(SlayerRace?.ToString() ?? "")} {(SlayerHf?.ToString() ?? SlayerHfid.ToString())} to pieces in {locationText}.";
                case "exec drowned":
                    return
                        $"{timestring} the {Hf.Race} {Hf} drowned \nthe {(SlayerRace?.ToString() ?? "")} {(SlayerHf?.ToString() ?? SlayerHfid.ToString())} in {locationText}.";
                case "exec buried alive":
                    return
                        $"{timestring} the {Hf.Race} {Hf} buried \nthe {(SlayerRace?.ToString() ?? "")} {(SlayerHf?.ToString() ?? SlayerHfid.ToString())} alive in {locationText}.";
                case "exec crucified":
                    return
                        $"{timestring} the {Hf.Race} {Hf} was crucified by \nthe {(SlayerRace?.ToString() ?? "")} {(SlayerHf?.ToString() ?? SlayerHfid.ToString())} alive in {locationText}.";
                case "air":
                    return
                        $"{timestring} the {Hf.Race} {Hf} suffocated, slain by {(SlayerRace?.ToString() ?? "")} {(SlayerHf?.ToString() ?? SlayerHfid.ToString())} alive in {locationText}.";
                case "obstacle":
                    return
                        $"{timestring} the {Hf.Race} {Hf} died after colliding with an obstacle, slain by {(SlayerRace?.ToString() ?? "")} {(SlayerHf?.ToString() ?? SlayerHfid.ToString())} in {locationText}.";
                case "hunger":
                    return $"{timestring} the {Hf.Race} {Hf} starved to death in {locationText}.";
                case "scuttled":
                    return string.Format("{0} {2} a {1} was scuttled in {3}.",
                        timestring, Hf.Race, Hf,
                        locationText);
                case "spikes":
                    return $"{timestring} the {Hf.Race} {Hf} was impaled on spikes in {locationText}.";
                case "scared to death":
                    return
                        $"{timestring} the {Hf.Race} {Hf} was scared to death by \nthe {(SlayerRace?.ToString() ?? "")} {(SlayerHf?.ToString() ?? SlayerHfid.ToString())} in {locationText}.";
                case "slaughtered":
                    return string.Format("{0} {2} a {1} was slaughtered in {5} by {4} a {3}.",
                        timestring, Hf.Race, Hf,
                        SlayerRace?.ToString() ?? "", SlayerHf?.ToString() ?? SlayerHfid.ToString(),
                        locationText);
                case "trap":
                    return $"{timestring} the {Hf.Race} {Hf} was killed by a trap in {locationText}.";
                case "blood drained":
                    return
                        $"{timestring} the {Hf.Race} {Hf} was drained of blood by \nthe {(SlayerRace?.ToString() ?? "")} {(SlayerHf?.ToString() ?? SlayerHfid.ToString())} in {locationText}.";

            }

            return timestring;
        }

        internal override string ToTimelineString()
        {
            //TODO: Incorporate new data
            var timelinestring = base.ToTimelineString();
            if (Site == null)
            {
                if (SlayerHf == null)
                    return $"{timelinestring} {Hf?.ToString() ?? Hfid.ToString()} died.";
                return $"{timelinestring} {Hf?.ToString() ?? Hfid.ToString()} killed by {SlayerHf}.";
            }
            if (SlayerHf == null)
                return $"{timelinestring} {Hf?.ToString() ?? Hfid.ToString()} died at {Site.AltName}";
            return
                $"{timelinestring} {Hf?.ToString() ?? Hfid.ToString()} killed at {Site.AltName} by {SlayerHf}";
        }

        internal override void Export(string table)
        {
            base.Export(table);

            table = GetType().Name;

            var vals = new List<object>
            {
                Id, 
                Hfid.DBExport(), 
                SlayerHfid.DBExport(), 
                SlayerRace.DBExport(),
                SlayerCaste.DBExport(HistoricalFigure.Castes),
                SlayerItemId.DBExport(), 
                SlayerShooterItemId.DBExport(), 
                Cause.DBExport(Causes), 
                SiteId.DBExport(), 
                SubregionId.DBExport(), 
                FeatureLayerId.DBExport(),
                Item.DBExport(),
                ArtifactId.DBExport(),
                ItemType.DBExport(ItemTypes),
                ItemSubType.DBExport(ItemSubTypes),
                Mat.DBExport(Materials),
                BowItem.DBExport(),
                BowArtifactId.DBExport(),
                BowItemType.DBExport(ItemTypes),
                BowItemSubType.DBExport(ItemSubTypes),
                BowMat.DBExport(Materials)
            };

            Database.ExportWorldItem(table, vals);

        }

    }
}
