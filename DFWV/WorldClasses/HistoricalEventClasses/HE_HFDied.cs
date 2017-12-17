using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Linq;
using DFWV.WorldClasses.HistoricalFigureClasses;

namespace DFWV.WorldClasses.HistoricalEventClasses
{
    public class HE_HFDied : HistoricalEvent
    {
        private int? HfId { get; }
        public HistoricalFigure Hf { get; private set; }
        private int? FeatureLayerId { get; }
        private int? HfId_Slayer { get; }
        public HistoricalFigure Hf_Slayer { get; private set; }
        private int? ArtifiactId_SlayerItem { get; }
        private Artifact Artifact_SlayerItem { get; set; }
        private int? ArtifactId_SlayerShooterItem { get; }
        private Artifact Artifact_SlayerShooterItem { get; set; }
        private string SlayerRace_ { get; set; }
        private Race SlayerRace { get; set; }
        private int SlayerCaste { get; }
        public int Cause { get; }
        public static List<string> Causes = new List<string>();
        private int? SiteId { get; }
        public Site Site { get; private set; }
        private int? SubregionId { get; }
        public Region Subregion { get; private set; }

        private int? ItemID { get; set; }
        private int? ArtifactId { get; set; }
        private Artifact Artifact { get; set; }
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
                yield return Hf_Slayer;
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
                        HfId = valI;
                        break;
                    case "slayer_hfid":
                        if (valI != -1)
                            HfId_Slayer = valI;
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
                            ArtifiactId_SlayerItem = valI;
                        break;
                    case "slayer_shooter_item_id":
                        if (valI != -1)
                            ArtifactId_SlayerShooterItem = valI;
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
            base.Link();
            if (SlayerRace_ != null)
            {
                SlayerRace = World.GetAddRace(SlayerRace_);
                SlayerRace_ = null;
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
                    case "victim_hf":
                    case "slayer_hf":
                    case "slayer_race":
                    case "slayer_caste":
                    case "site":
                    case "death_cause":
                        break;
                    case "item":
                        ItemID = valI;
                        break;
                    case "artifact_id":
                        ArtifactId = valI;
                        break;
                    case "item_type":
                        if (!Item.ItemTypes.Contains(val))
                            Item.ItemTypes.Add(val);
                        ItemType = Item.ItemTypes.IndexOf(val);
                        break;
                    case "item_subtype":
                        if (!Item.ItemSubTypes.Contains(val))
                            Item.ItemSubTypes.Add(val);
                        ItemSubType = Item.ItemSubTypes.IndexOf(val);
                        break;
                    case "mattype":
                        MatType = valI;
                        break;
                    case "matindex":
                        MatIndex = valI;
                        break;
                    case "mat":
                        if (!Item.Materials.Contains(val))
                            Item.Materials.Add(val);
                        Mat = Item.Materials.IndexOf(val);
                        break;
                    case "bow_item":
                        BowItem = valI;
                        break;
                    case "bow_artifact_id":
                        BowArtifactId = valI;
                        break;
                    case "bow_item_type":
                        if (!Item.ItemTypes.Contains(val))
                            Item.ItemTypes.Add(val);
                        BowItemType = Item.ItemTypes.IndexOf(val);
                        break;
                    case "bow_item_subtype":
                        if (!Item.ItemSubTypes.Contains(val))
                            Item.ItemSubTypes.Add(val);
                        BowItemSubType = Item.ItemSubTypes.IndexOf(val);
                        break;
                    case "bow_mattype":
                        BowMatType = valI;
                        break;
                    case "bow_matindex":
                        BowMatIndex = valI;
                        break;
                    case "bow_mat":
                        if (!Item.Materials.Contains(val))
                            Item.Materials.Add(val);
                        BowMat = Item.Materials.IndexOf(val);
                        break;
                    default:
                        DFXMLParser.UnexpectedXmlElement(xdoc.Root.Name.LocalName + "\t" + Types[Type], element, xdoc.Root.ToString());
                        break;
                }
            }
        }

        public override void Process()
        {
            base.Process();
            if (Hf != null)
                Hf.DiedEvent = this;
            if (Hf_Slayer != null)
            {
                if (Hf_Slayer.SlayingEvents == null)
                    Hf_Slayer.SlayingEvents = new List<HE_HFDied>();
                Hf_Slayer.SlayingEvents.Add(this);
            }
            if (Artifact_SlayerItem != null)
            {
                if (Artifact.ArtifactEvents == null)
                    Artifact.ArtifactEvents = new List<HistoricalEvent>();
                Artifact.ArtifactEvents.Add(this);
            }
            if (Artifact_SlayerShooterItem == null) return;
            if (Artifact_SlayerShooterItem.ArtifactEvents == null)
                Artifact_SlayerShooterItem.ArtifactEvents = new List<HistoricalEvent>();
            Artifact_SlayerShooterItem.ArtifactEvents.Add(this);
        }

        protected override void WriteDataOnParent(MainForm frm, Control parent, ref Point location)
        {
            //TODO: Incorporate new data
            EventLabel(frm, parent, ref location, "HF:", Hf);
            EventLabel(frm, parent, ref location, "Cause:", Causes[Cause]);
            if (Artifact_SlayerItem != null)
                EventLabel(frm, parent, ref location, "Weapon:", Artifact_SlayerItem);
            if (Artifact_SlayerShooterItem != null)
                EventLabel(frm, parent, ref location, "Bow:", Artifact_SlayerShooterItem);
            if (Hf_Slayer != null)
                EventLabel(frm, parent, ref location, "Slayer:", Hf_Slayer);
            else if (HfId_Slayer.HasValue)
                EventLabel(frm, parent, ref location, "Slayer:", HfId_Slayer.ToString());
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

        protected override string LegendsDescription()
        {
            var timestring = base.LegendsDescription();

            var locationText = "";
            if (Site != null)
                locationText = Site.AltName;
            else if (Subregion != null)
                locationText = Subregion.ToString();

            var hfText = Hf != null ? $"{Hf.Race} {Hf}" : "UNKNOWN";

            switch (Causes[Cause])
            {
                case "struck":
                    if (ItemID == null)
                        return
                            $"{timestring} the {hfText} was struck down by \nthe {SlayerRace?.ToString() ?? ""} {Hf_Slayer?.ToString() ?? HfId_Slayer.ToString()} in {locationText}.";
                    if (Artifact_SlayerItem != null)
                        return
                            $"{timestring} the {hfText} was struck down by \n the {SlayerRace?.ToString() ?? ""} {Hf_Slayer?.ToString() ?? HfId_Slayer.ToString()} with {Artifact_SlayerItem} in {locationText}.";
                    if (ItemType != null && Item.ItemTypes[ItemType.Value] == "corpsepiece")
                        return
                            $"{timestring} the {hfText} was struck down by \nthe {SlayerRace?.ToString() ?? ""} {Hf_Slayer?.ToString() ?? HfId_Slayer.ToString()} {"with a body part"} in {locationText}.";
                    if (ItemType != null && Item.ItemTypes[ItemType.Value] == "corpse")
                        return
                            $"{timestring} the {hfText} was struck down by \nthe {SlayerRace?.ToString() ?? ""} {Hf_Slayer?.ToString() ?? HfId_Slayer.ToString()} {"with a corpse"} in {locationText}.";
                    if (Mat != null && ItemType != null && ItemSubType == null) //Test
                        return
                            $"{timestring} the {hfText} was struck down by \nthe {SlayerRace?.ToString() ?? ""} {Hf_Slayer?.ToString() ?? HfId_Slayer.ToString()} {"with a " + Item.Materials[Mat.Value] + " " + Item.ItemTypes[ItemType.Value]} in {locationText}.";
                    if (Mat != null && ItemType != null && ItemSubType != null)
                        return
                            $"{timestring} the {hfText} was struck down by \nthe {SlayerRace?.ToString() ?? ""} {Hf_Slayer?.ToString() ?? HfId_Slayer.ToString()} {"with a " + Item.Materials[Mat.Value] + " " + Item.ItemSubTypes[ItemSubType.Value]} in {locationText}.";
                    return
                        $"{timestring} the {hfText} was struck down by \nthe {SlayerRace?.ToString() ?? ""} {Hf_Slayer?.ToString() ?? HfId_Slayer.ToString()} {"with "} in {locationText}.";
                case "shot":
                    if (BowItem == null)
                    {
                        if (Mat != null && ItemType != null && ItemSubType != null)
                            return
                                $"{timestring} the {hfText} was shot and killed by \nthe {SlayerRace?.ToString() ?? ""} {Hf_Slayer?.ToString() ?? HfId_Slayer.ToString()} {"with a " + Item.Materials[Mat.Value] + " " + Item.ItemSubTypes[ItemSubType.Value]} in {locationText}.";
                        return
                            $"{timestring} the {hfText} was shot and killed by \nthe {SlayerRace?.ToString() ?? ""} {Hf_Slayer?.ToString() ?? HfId_Slayer.ToString()} in {locationText} " +
                            $"with a {(Mat.HasValue ? Item.Materials[Mat.Value] : "UNKNOWN")}  {(ItemSubType.HasValue ? Item.ItemSubTypes[ItemSubType.Value] : "UNKNOWN")}";
                    }
                    if (Artifact_SlayerShooterItem != null)
                        return
                            $"{timestring} the {hfText} was shot and killed by \n the {SlayerRace?.ToString() ?? ""} {Hf_Slayer?.ToString() ?? HfId_Slayer.ToString()} with {Artifact_SlayerShooterItem} in {locationText}.";
                    if (BowMat != null && BowItemType != null && BowItemSubType == null && ItemType != null && Mat != null) //Test
                        return
                            $"{timestring} the {hfText} was shot and killed by \nthe {SlayerRace?.ToString() ?? ""} {Hf_Slayer?.ToString() ?? HfId_Slayer.ToString()} {"with a " + Item.Materials[Mat.Value] + " " + Item.ItemTypes[ItemType.Value]} in {locationText}.";
                    if (BowMat != null && BowItemType != null && BowItemSubType != null && Mat != null && ItemType != null && ItemSubType != null)
                        return
                            $"{timestring} the {hfText} was shot and killed by \nthe {SlayerRace?.ToString() ?? ""} {Hf_Slayer?.ToString() ?? HfId_Slayer.ToString()} {"with a " + Item.Materials[Mat.Value] + " " + Item.ItemSubTypes[ItemSubType.Value] + " from a " + Item.Materials[BowMat.Value] + " " + Item.ItemSubTypes[BowItemSubType.Value]} in {locationText}.";
                    return
                        $"{timestring} the {hfText} was shot and killed by \nthe {SlayerRace?.ToString() ?? ""} {Hf_Slayer?.ToString() ?? HfId_Slayer.ToString()} {"with "} in {locationText}.";
                case "murdered":
                    return
                        $"{timestring} the {hfText} was murdered by \nthe {SlayerRace?.ToString() ?? ""} {Hf_Slayer?.ToString() ?? HfId_Slayer.ToString()} in {locationText}.";
                case "old age":
                    return $"{timestring} the {hfText} died of old age.";
                case "infection":
                    return
                        $"{timestring} the {hfText} succumbed to infection, slain by \nthe {SlayerRace?.ToString() ?? ""} {Hf_Slayer?.ToString() ?? HfId_Slayer.ToString()} in {locationText}.";
                case "blood":
                    return Hf_Slayer == null ? 
                        $"{timestring} the {hfText} bled to death in {locationText}." : 
                        $"{timestring} the {hfText} bled to death, slain by the {SlayerRace?.ToString() ?? ""} {Hf_Slayer} with ITEM: {ArtifiactId_SlayerItem} in {locationText}.";
                case "thirst":
                    return $"{timestring} the {hfText} died of thirst in {locationText}.";
                case "collapsed":
                    return
                        $"{timestring} the {hfText} collapsed, struck down by \nthe {SlayerRace?.ToString() ?? ""} {Hf_Slayer?.ToString() ?? HfId_Slayer.ToString()} in {locationText}.";
                case "vanish":
                    return $"{timestring} the {hfText} vanished in {locationText}.";
                case "drown":
                    return $"{timestring} the {hfText} drowned in {locationText}.";
                case "crushed bridge":
                    return $"{timestring} the {hfText} was crushed by a drawbridge in {locationText}.";
                case "put to rest":
                    return $"{timestring} the {hfText} was put to rest in {locationText}.";
                case "quitdead":
                    return $"{timestring} the {hfText} starved to death in {locationText}.";
                case "exec beheaded":
                    return
                        $"{timestring} the {hfText} was beheaded by \nthe {SlayerRace?.ToString() ?? ""} {Hf_Slayer?.ToString() ?? HfId_Slayer.ToString()} in {locationText}.";
                case "exec burned alive":
                    return
                        $"{timestring} the {hfText} burned \nthe {(SlayerRace?.ToString() ?? "")} {(Hf_Slayer?.ToString() ?? HfId_Slayer.ToString())} alive in {locationText}.";
                case "exec fed to beasts":
                    return
                        $"{timestring} the {hfText} fed \nthe {(SlayerRace?.ToString() ?? "")} {(Hf_Slayer?.ToString() ?? HfId_Slayer.ToString())} to beasts in {locationText}.";
                case "exec hacked to pieces":
                    return
                        $"{timestring} the {hfText} hacked \nthe {(SlayerRace?.ToString() ?? "")} {(Hf_Slayer?.ToString() ?? HfId_Slayer.ToString())} to pieces in {locationText}.";
                case "exec drowned":
                    return
                        $"{timestring} the {hfText} drowned \nthe {(SlayerRace?.ToString() ?? "")} {(Hf_Slayer?.ToString() ?? HfId_Slayer.ToString())} in {locationText}.";
                case "exec buried alive":
                    return
                        $"{timestring} the {hfText} buried \nthe {(SlayerRace?.ToString() ?? "")} {(Hf_Slayer?.ToString() ?? HfId_Slayer.ToString())} alive in {locationText}.";
                case "exec crucified":
                    return
                        $"{timestring} the {hfText} was crucified by \nthe {(SlayerRace?.ToString() ?? "")} {(Hf_Slayer?.ToString() ?? HfId_Slayer.ToString())} alive in {locationText}.";
                case "air":
                    return
                        $"{timestring} the {hfText} suffocated, slain by {(SlayerRace?.ToString() ?? "")} {(Hf_Slayer?.ToString() ?? HfId_Slayer.ToString())} alive in {locationText}.";
                case "obstacle":
                    return
                        $"{timestring} the {hfText} died after colliding with an obstacle, slain by {(SlayerRace?.ToString() ?? "")} {(Hf_Slayer?.ToString() ?? HfId_Slayer.ToString())} in {locationText}.";
                case "hunger":
                    return $"{timestring} the {hfText} starved to death in {locationText}.";
                case "scuttled":
                    return $"{timestring} {hfText} was scuttled in {locationText}.";
                case "spikes":
                    return $"{timestring} the {hfText} was impaled on spikes in {locationText}.";
                case "scared to death":
                    return
                        $"{timestring} the {hfText} was scared to death by \nthe {(SlayerRace?.ToString() ?? "")} {(Hf_Slayer?.ToString() ?? HfId_Slayer.ToString())} in {locationText}.";
                case "slaughtered":
                    return $"{timestring} the {hfText} was slaughtered in {locationText} by {Hf_Slayer?.ToString() ?? HfId_Slayer.ToString()} a {SlayerRace?.ToString() ?? ""}.";
                case "trap":
                    return $"{timestring} the {hfText} was killed by a trap in {locationText}.";
                case "blood drained":
                    return
                        $"{timestring} the {hfText} was drained of blood by \nthe {(SlayerRace?.ToString() ?? "")} {(Hf_Slayer?.ToString() ?? HfId_Slayer.ToString())} in {locationText}.";

            }

            return timestring;
        }

        internal override string ToTimelineString()
        {
            //TODO: Incorporate new data
            var timelinestring = base.ToTimelineString();
            if (Site == null)
            {
                if (Hf_Slayer == null)
                    return $"{timelinestring} {Hf?.ToString() ?? HfId.ToString()} died.";
                return $"{timelinestring} {Hf?.ToString() ?? HfId.ToString()} killed by {Hf_Slayer}.";
            }
            if (Hf_Slayer == null)
                return $"{timelinestring} {Hf?.ToString() ?? HfId.ToString()} died at {Site.AltName}";
            return
                $"{timelinestring} {Hf?.ToString() ?? HfId.ToString()} killed at {Site.AltName} by {Hf_Slayer}";
        }

        internal override void Export(string table)
        {
            base.Export(table);

            table = GetType().Name;

            var vals = new List<object>
            {
                Id, 
                HfId.DBExport(), 
                HfId_Slayer.DBExport(), 
                SlayerRace.DBExport(),
                SlayerCaste.DBExport(HistoricalFigure.Castes),
                ArtifiactId_SlayerItem.DBExport(), 
                ArtifactId_SlayerShooterItem.DBExport(), 
                Cause.DBExport(Causes), 
                SiteId.DBExport(), 
                SubregionId.DBExport(), 
                FeatureLayerId.DBExport(),
                ItemID.DBExport(),
                ArtifactId.DBExport(),
                ItemType.DBExport(Item.ItemTypes),
                ItemSubType.DBExport(Item.ItemSubTypes),
                Mat.DBExport(Item.Materials),
                BowItem.DBExport(),
                BowArtifactId.DBExport(),
                BowItemType.DBExport(Item.ItemTypes),
                BowItemSubType.DBExport(Item.ItemSubTypes),
                BowMat.DBExport(Item.Materials)
            };

            Database.ExportWorldItem(table, vals);

        }

    }
}
