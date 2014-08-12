using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Linq;
using DFWV.WorldClasses.HistoricalFigureClasses;

namespace DFWV.WorldClasses.HistoricalEventClasses
{
    class HE_HFDied : HistoricalEvent
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

        override public Point Location { get { return Site != null ? Site.Location : (Subregion != null ? Subregion.Location : Point.Empty); } }

        public HE_HFDied(XDocument xdoc, World world)
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
                Int32.TryParse(val, out valI);

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
                        if (!Items.Contains(val))
                            Items.Add(val);
                        ItemType = Items.IndexOf(val);
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
                        if (!Items.Contains(val))
                            Items.Add(val);
                        BowItemType = Items.IndexOf(val);
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
            if (SlayerShooterItem != null)
            {
                if (SlayerShooterItem.Kills == null)
                    SlayerShooterItem.Kills = new List<HE_HFDied>();
                SlayerShooterItem.Kills.Add(this);
            }
            if (HF != null)
            {
                if (HF.Events == null)
                    HF.Events = new List<HistoricalEvent>();
                HF.Events.Add(this);
            }
            if (SlayerHF == null) return;
            if (SlayerHF.Events == null)
                SlayerHF.Events = new List<HistoricalEvent>();
            SlayerHF.Events.Add(this);
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
                        return string.Format("{0} the {1} {2} was struck down by \nthe {3} {4} in {5}.",
                            timestring, HF.Race, HF,
                            SlayerRace == null ? "" : SlayerRace.ToString(), SlayerHF == null ? SlayerHFID.ToString() : SlayerHF.ToString(),
                            locationText);
                    if (SlayerItem != null)
                        return string.Format("{0} the {1} {2} was struck down by \n the {3} {4} with {5} in {6}.",
                            timestring, HF.Race, HF,
                            SlayerRace == null ? "" : SlayerRace.ToString(), SlayerHF == null ? SlayerHFID.ToString() : SlayerHF.ToString(),
                            SlayerItem, locationText);
                    if (ItemType != null && Items[ItemType.Value] == "corpsepiece")
                        return string.Format("{0} the {1} {2} was struck down by \nthe {3} {4} {5} in {6}.",
                            timestring, HF.Race, HF,
                            SlayerRace == null ? "" : SlayerRace.ToString(), SlayerHF == null ? SlayerHFID.ToString() : SlayerHF.ToString(),
                            "with a body part", locationText);
                    if (ItemType != null && Items[ItemType.Value] == "corpse")
                        return string.Format("{0} the {1} {2} was struck down by \nthe {3} {4} {5} in {6}.",
                            timestring, HF.Race, HF,
                            SlayerRace == null ? "" : SlayerRace.ToString(), SlayerHF == null ? SlayerHFID.ToString() : SlayerHF.ToString(),
                            "with a corpse", locationText);
                    if (Mat != null && ItemType != null && ItemSubType == null) //Test
                        return string.Format("{0} the {1} {2} was struck down by \nthe {3} {4} {5} in {6}.",
                            timestring, HF.Race, HF,
                            SlayerRace == null ? "" : SlayerRace.ToString(), SlayerHF == null ? SlayerHFID.ToString() : SlayerHF.ToString(),
                            "with a " + Materials[Mat.Value] + " " + Items[ItemType.Value] , locationText);
                    if (Mat != null && ItemType != null && ItemSubType != null)
                        return string.Format("{0} the {1} {2} was struck down by \nthe {3} {4} {5} in {6}.",
                            timestring, HF.Race, HF,
                            SlayerRace == null ? "" : SlayerRace.ToString(), SlayerHF == null ? SlayerHFID.ToString() : SlayerHF.ToString(),
                            "with a " + Materials[Mat.Value] + " " + ItemSubTypes[ItemSubType.Value], locationText);
                    return string.Format("{0} the {1} {2} was struck down by \nthe {3} {4} {5} in {6}.",
                        timestring, HF.Race, HF,
                        SlayerRace == null ? "" : SlayerRace.ToString(), SlayerHF == null ? SlayerHFID.ToString() : SlayerHF.ToString(),
                        "with " , locationText);
                case "shot":
                    if (BowItem == null)
                    {
                        if (Mat != null && ItemType != null && ItemSubType != null)
                            return string.Format("{0} the {1} {2} was shot and killed by \nthe {3} {4} {5} in {6}.",
                                timestring, HF.Race, HF,
                                SlayerRace == null ? "" : SlayerRace.ToString(), SlayerHF == null ? SlayerHFID.ToString() : SlayerHF.ToString(),
                                "with a " + Materials[Mat.Value] + " " + ItemSubTypes[ItemSubType.Value],
                                locationText);
                        return string.Format("{0} the {1} {2} was shot and killed by \nthe {3} {4} in {5}.",  //Test
                            timestring, HF.Race, HF,
                            SlayerRace == null ? "" : SlayerRace.ToString(), SlayerHF == null ? SlayerHFID.ToString() : SlayerHF.ToString(),
                            locationText);
                    }
                    if (SlayerShooterItem != null)
                        return string.Format("{0} the {1} {2} was shot and killed by \n the {3} {4} with {5} in {6}.",
                            timestring, HF.Race, HF,
                            SlayerRace == null ? "" : SlayerRace.ToString(), SlayerHF == null ? SlayerHFID.ToString() : SlayerHF.ToString(),
                            SlayerShooterItem, locationText);
                    if (BowMat != null && BowItemType != null && BowItemSubType == null && ItemType != null && Mat != null) //Test
                        return string.Format("{0} the {1} {2} was shot and killed by \nthe {3} {4} {5} in {6}.",
                            timestring, HF.Race, HF,
                            SlayerRace == null ? "" : SlayerRace.ToString(), SlayerHF == null ? SlayerHFID.ToString() : SlayerHF.ToString(),
                            "with a " + Materials[Mat.Value] + " " + Items[ItemType.Value], locationText);
                    if (BowMat != null && BowItemType != null && BowItemSubType != null && Mat != null && ItemType != null && ItemSubType != null)
                        return string.Format("{0} the {1} {2} was shot and killed by \nthe {3} {4} {5} in {6}.",
                            timestring, HF.Race, HF,
                            SlayerRace == null ? "" : SlayerRace.ToString(), SlayerHF == null ? SlayerHFID.ToString() : SlayerHF.ToString(),
                            "with a " + Materials[Mat.Value] + " " + ItemSubTypes[ItemSubType.Value] + " from a " + Materials[BowMat.Value] + " " + ItemSubTypes[BowItemSubType.Value],
                            locationText);
                    return string.Format("{0} the {1} {2} was shot and killed by \nthe {3} {4} {5} in {6}.",
                        timestring, HF.Race, HF,
                        SlayerRace == null ? "" : SlayerRace.ToString(), SlayerHF == null ? SlayerHFID.ToString() : SlayerHF.ToString(),
                        "with ", locationText);
                case "murdered":
                    return string.Format("{0} the {1} {2} was murdered by \nthe {3} {4} in {5}.",
                        timestring, HF.Race, HF,
                        SlayerRace == null ? "" : SlayerRace.ToString(), SlayerHF == null ? SlayerHFID.ToString() : SlayerHF.ToString(),
                        locationText);
                case "old age":
                    return string.Format("{0} the {1} {2} died of old age.",
                        timestring, HF.Race, HF);
                case "infection":
                    return string.Format("{0} the {1} {2} succumbed to infection, slain by \nthe {3} {4} in {5}.",
                        timestring, HF.Race, HF,
                        SlayerRace == null ? "" : SlayerRace.ToString(), SlayerHF == null ? SlayerHFID.ToString() : SlayerHF.ToString(),
                        locationText);
                case "blood":
                    if (SlayerHF == null)
                        return string.Format("{0} the {1} {2} bled to death in {3}.",
                            timestring, HF.Race, HF,
                            locationText);
                    return string.Format("{0} the {1} {2} bled to death, slain by the {3} {4} with ITEM: {5} in {6}.",
                        timestring, HF.Race, HF, SlayerRace == null ? "" : SlayerRace.ToString(), SlayerHF == null ? SlayerHFID.ToString() : SlayerHF.ToString(), SlayerItemID,
                        locationText);
                case "thirst":
                    return string.Format("{0} the {1} {2} died of thirst in {3}.",
                        timestring, HF.Race, HF,
                        locationText);
                case "collapsed":
                    return string.Format("{0} the {1} {2} collapsed, struck down by \nthe {3} {4} in {5}.",
                        timestring, HF.Race, HF,
                        SlayerRace == null ? "" : SlayerRace.ToString(), SlayerHF == null ? SlayerHFID.ToString() : SlayerHF.ToString(),
                        locationText);
                case "vanish":
                    return string.Format("{0} the {1} {2} vanished in {3}.",
                        timestring, HF.Race, HF,
                        locationText);
                case "drown":
                    return string.Format("{0} the {1} {2} drowned in {3}.",
                        timestring, HF.Race, HF,
                        locationText);
                case "crushed bridge":
                    return string.Format("{0} the {1} {2} was crushed by a drawbridge in {3}.",
                        timestring, HF.Race, HF,
                        locationText);
                case "put to rest":
                    return string.Format("{0} the {1} {2} was put to rest in {3}.",
                        timestring, HF.Race, HF,
                        locationText);
                case "quitdead":
                    return string.Format("{0} the {1} {2} starved to death in {3}.",
                        timestring, HF.Race, HF,
                        locationText);
                case "exec beheaded":
                    return string.Format("{0} the {1} {2} was beheaded by \nthe {3} {4} in {5}.",
                        timestring, HF.Race, HF,
                        SlayerRace == null ? "" : SlayerRace.ToString(), SlayerHF == null ? SlayerHFID.ToString() : SlayerHF.ToString(),
                        locationText);
                case "exec burned alive":
                    return string.Format("{0} the {1} {2} burned \nthe {3} {4} alive in {5}.",
                        timestring, HF.Race, HF,
                        SlayerRace == null ? "" : SlayerRace.ToString(), SlayerHF == null ? SlayerHFID.ToString() : SlayerHF.ToString(),
                        locationText);
                case "exec fed to beasts":
                    return string.Format("{0} the {1} {2} fed \nthe {3} {4} to beasts in {5}.",
                        timestring, HF.Race, HF,
                        SlayerRace == null ? "" : SlayerRace.ToString(), SlayerHF == null ? SlayerHFID.ToString() : SlayerHF.ToString(),
                        locationText);
                case "exec hacked to pieces":
                    return string.Format("{0} the {1} {2} hacked \nthe {3} {4} to pieces in {5}.",
                        timestring, HF.Race, HF,
                        SlayerRace == null ? "" : SlayerRace.ToString(), SlayerHF == null ? SlayerHFID.ToString() : SlayerHF.ToString(),
                        locationText);
                case "exec drowned":
                    return string.Format("{0} the {1} {2} drowned \nthe {3} {4} in {5}.",
                        timestring, HF.Race, HF,
                        SlayerRace == null ? "" : SlayerRace.ToString(), SlayerHF == null ? SlayerHFID.ToString() : SlayerHF.ToString(),
                        locationText);
                case "exec buried alive":
                    return string.Format("{0} the {1} {2} buried \nthe {3} {4} alive in {5}.",
                        timestring, HF.Race, HF,
                        SlayerRace == null ? "" : SlayerRace.ToString(), SlayerHF == null ? SlayerHFID.ToString() : SlayerHF.ToString(),
                        locationText);
                case "exec crucified":
                    return string.Format("{0} the {1} {2} was crucified by \nthe {3} {4} alive in {5}.",
                        timestring, HF.Race, HF,
                        SlayerRace == null ? "" : SlayerRace.ToString(), SlayerHF == null ? SlayerHFID.ToString() : SlayerHF.ToString(),
                        locationText);
                case "air":
                    return string.Format("{0} the {1} {2} suffocated, slain by {3} {4} alive in {5}.",
                        timestring, HF.Race, HF,
                        SlayerRace == null ? "" : SlayerRace.ToString(), SlayerHF == null ? SlayerHFID.ToString() : SlayerHF.ToString(),
                        locationText);
                case "obstacle":
                    return string.Format("{0} the {1} {2} died after colliding with an obstacle, slain by {3} {4} in {5}.",
                        timestring, HF.Race, HF,
                        SlayerRace == null ? "" : SlayerRace.ToString(), SlayerHF == null ? SlayerHFID.ToString() : SlayerHF.ToString(),
                        locationText);
                case "hunger":
                    return string.Format("{0} the {1} {2} starved to death in {3}.",
                        timestring, HF.Race, HF,
                         locationText);
                case "scuttled":
                    return string.Format("{0} {2} a {1} was scuttled in {3}.",
                        timestring, HF.Race, HF,
                        locationText);
                case "spikes":
                    return string.Format("{0} the {1} {2} was impaled on spikes in {3}.",
                        timestring, HF.Race, HF,
                        locationText);
                case "scared to death":
                    return string.Format("{0} the {1} {2} was scared to death by \nthe {3} {4} in {5}.",
                        timestring, HF.Race, HF,
                        SlayerRace == null ? "" : SlayerRace.ToString(), SlayerHF == null ? SlayerHFID.ToString() : SlayerHF.ToString(),
                        locationText);
                case "slaughtered":
                    return string.Format("{0} {2} a {1} was slaughtered in {5} by {4} a {3}.",
                        timestring, HF.Race, HF,
                        SlayerRace == null ? "" : SlayerRace.ToString(), SlayerHF == null ? SlayerHFID.ToString() : SlayerHF.ToString(),
                        locationText);
                case "trap":
                    return string.Format("{0} the {1} {2} was killed by a trap in {3}.",
                        timestring, HF.Race, HF,
                        locationText);
                case "blood drained":
                    return string.Format("{0} the {1} {2} was drained of blood by \nthe {3} {4} in {5}.",
                        timestring, HF.Race, HF,
                        SlayerRace == null ? "" : SlayerRace.ToString(), SlayerHF == null ? SlayerHFID.ToString() : SlayerHF.ToString(),
                        locationText);

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
                    return string.Format("{0} {1} died.",
                        timelinestring, HF != null ? HF.ToString() : HFID.ToString());
                return string.Format("{0} {1} killed by {2}.",
                    timelinestring, HF != null ? HF.ToString() : HFID.ToString(), SlayerHF);
            }
            if (SlayerHF == null)
                return string.Format("{0} {1} died at {2}",
                    timelinestring, HF != null ? HF.ToString() : HFID.ToString(), Site.AltName);
            return string.Format("{0} {1} killed at {2} by {3}",
                timelinestring, HF != null ? HF.ToString() : HFID.ToString(), Site.AltName, SlayerHF);
        }

        internal override void Export(string table)
        {
            //TODO: Incorporate new data
            base.Export(table);

            table = GetType().Name;

            var vals = new List<object> { ID, HFID, SlayerHFID, SlayerRace == null ? (object)DBNull.Value : SlayerRace.ToString(), HistoricalFigure.Castes[SlayerCaste], SlayerItemID, SlayerShooterItemID, Causes[Cause], SiteID, SubregionID, FeatureLayerID   };

            Database.ExportWorldItem(table, vals);

        }

    }
}
