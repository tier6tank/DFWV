using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;
using DFWV.Annotations;
using DFWV.WorldClasses.EntityClasses;
using DFWV.WorldClasses.HistoricalEventClasses;
using DFWV.WorldClasses.HistoricalEventCollectionClasses;

namespace DFWV.WorldClasses.HistoricalFigureClasses
{
    public class HistoricalFigure : XMLObject
    {
        private string Race_ { get; set; } 
        public Race Race { get; private set; }
        [UsedImplicitly]
        public string RaceName => Race != null ? Race.Name : Race_ ?? "";

        public int? Sex { get; set; }

        public int? Flags { get; set; }
        
        public int? Caste { get; }
        public static List<string> Castes = new List<string>();
        private int? AppearedYear { get; }
        public WorldTime Appeared => AppearedYear.HasValue ? new WorldTime(AppearedYear.Value) : null;

        [UsedImplicitly]
        public int? BirthYear { get; private set; }
        private int? BirthSeconds { get; }
        public WorldTime Birth => BirthYear.HasValue ? new WorldTime(BirthYear.Value, BirthSeconds) : WorldTime.Present;
        public int? DeathYear { get; }
        private int? DeathSeconds { get; }
        public WorldTime Death => DeathYear.HasValue ? new WorldTime(DeathYear.Value, DeathSeconds) : WorldTime.Present;
        public static List<string> AssociatedTypes = new List<string>();
        public int? AssociatedType { get; }
        public Dictionary<int, List<HFEntityLink>> EntityLinks { get; set; }
        public Dictionary<int, List<HFSiteLink>> SiteLinks { get; set; }
        public List<int> Sphere { get; }
        public static List<string> Spheres = new List<string>();
        public List<HFSkill> HfSkills { get; set; }
        private List<RelationshipProfileHF> RelationshipProfileHFs { get; }
        public static List<string> Interactions = new List<string>();
        public List<int> InteractionKnowledge { get; }
        public List<int> JourneyPet { get; }
        public static List<string> JourneyPets = new List<string>();
        public Dictionary<int, List<HFLink>> HfLinks { get; }
        [UsedImplicitly]
        public bool Deity { get; private set; }

        public List<EntityFormerPositionLink> EntityFormerPositionLinks { get; set; }
        private List<EntityPositionLink> EntityPositionLinks { get; }
        private List<EntitySquadLink> EntitySquadLinks { get; }
        private List<EntityFormerSquadLink> EntityFormerSquadLinks { get; }


        private int? EntPop_ { get; }
        private EntityPopulation EntPop { get; set; }
        public int? ActiveInteraction { get; private set; }
        public static List<string> Goals = new List<string>();
        public int? Goal { get; }
        [UsedImplicitly]
        public bool Force { get; private set; }
        private List<EntityReputation> EntityReputations { get; }
        private int? CurrentIdentityId { get; set; }
        private int? UsedIdentityId { get; }
        private string HoldsArtifact { get; set; }
        [UsedImplicitly]
        public bool Animated { get; private set; }
        private string AnimatedString { get; }
        [UsedImplicitly]
        public bool Ghost { get; private set; }
        public bool Adventurer { get; }
        public int? BreedId { get; set; }
        public Leader Leader { get; set; }
        public God God { private get; set; }
        public Unit Unit { get; set; }

        [UsedImplicitly]
        public bool IsPlayerControlled { private get; set; }

        public List<HistoricalEvent> CachedEvents;

        public IEnumerable<HistoricalEvent> Events
        {
            get {
                return CachedEvents ??
                       (CachedEvents = World.HistoricalEvents.Values.Where(x => x.HFsInvolved.Contains(this)).ToList());
            }
        }

        [UsedImplicitly]
        public int EventCount { get; set; }

        [UsedImplicitly]
        public string FirstName => ShortName == null ? null : Name.Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)[0];

        [UsedImplicitly]
        public string LastName
        {
            get
            {
                if (ShortName == null)
                    return null;
                return ShortName.Contains(' ') ? ShortName.Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)[1] : null;
            }
        }
        public string ShortName 
        { 
            get 
            {
                if (Name == null)
                    return null;
                return Name.Contains(" the ") ?  Name.Substring(0, Name.IndexOf(" the ")) : Name; 
            } 
        }

        [UsedImplicitly]
        public string Title 
        { 
            get 
            {
                if (Name == null)
                    return null;
                return Name.Contains(" the ") ?  Name.Substring(Name.IndexOf("the ")) : null;
            } 
        }

        public Site Site { get; set; }
        public Region Region { get; set; }
        public Point Coords { get; set; }

        private List<Entity> EnemyOf { get; set; }
        private List<Entity> MemberOf { get; set; }
        private List<Entity> FormerMemberOf { get; set; }
        public List<Entity> PrisonerOf { get; set; }
        private List<Entity> FormerPrisonerOf { get; set; }
        private List<Entity> CriminalOf { get; set; }
        public List<Entity> SlaveOf { get; set; }
        private List<Entity> FormerSlaveOf { get; set; }
        public List<Entity> HeroOf { get; set; }

        public List<Artifact> CreatedArtifacts { get; set; }

        public List<HistoricalFigure> Children { get; set; }
        public List<HistoricalFigure> Spouses { get; set; }
        private List<HistoricalFigure> Lovers { get; set; }
        private List<HistoricalFigure> Followers { get; set; }
        private List<HistoricalFigure> Deities { get; set; }
        private List<HistoricalFigure> Parents { get; set; }
        private List<HistoricalFigure> Apprentices { get; set; }
        private List<HistoricalFigure> Masters { get; set; }
        private List<HistoricalFigure> FormerApprentices { get; set; }
        private List<HistoricalFigure> FormerMasters { get; set; }
        private List<HistoricalFigure> Prisoners { get; set; }
        private List<HistoricalFigure> Imprisoners { get; set; }
        private List<HistoricalFigure> Companions { get; set; }

        public HistoricalFigure Mother { get; set; }
        public HistoricalFigure Father { get; set; }
        private List<HistoricalFigure> _descendents;
        private List<HistoricalFigure> _ancestors;

        public HE_HFDied DiedEvent { get; set; }
        public List<HE_HFDied> SlayingEvents { get; set; }
        public HE_ChangeHFBodyState EntombedEvent { get; set; }


        public List<EC_Battle> BattleEventCollections { get; set; }
        public List<EC_Duel> DuelEventCollections { get; set; }

        override public Point Location => Coords;

        [UsedImplicitly]
        public bool Dead => Death != WorldTime.Present;

        [UsedImplicitly]
        public bool InEntPop => EntPop != null;
        [UsedImplicitly]
        public bool IsLeader => Leader != null;
        [UsedImplicitly]
        public bool IsPositioned => Location != Point.Empty;
        [UsedImplicitly]
        public bool IsGod => God != null;
        [UsedImplicitly]
        public bool IsUnit => Unit != null;
        [UsedImplicitly]
        public int CreatedArtifactCount => CreatedArtifacts?.Count ?? 0;
        [UsedImplicitly]
        public int CreatedMasterpieceCount { get { return Events.Count(x => HistoricalEvent.Types[x.Type].Contains("masterpiece")); } }
        [UsedImplicitly]
        public int ChildrenCount => Children?.Count ?? 0;
        [UsedImplicitly]
        public int Kills => SlayingEvents?.Count ?? 0;
        [UsedImplicitly]
        public int Battles => BattleEventCollections?.Count ?? 0;
        [UsedImplicitly]
        public string DispNameLower => ToString().ToLower();
        [UsedImplicitly]
        public int EntPopId => EntPop?.Id ?? (EntPop_ ?? -1);

        [UsedImplicitly]
        public int DescendentCount { get; set; } = -1;

        [UsedImplicitly]
        public int AncestorCount { get; set; } = -1;
        [UsedImplicitly]
        public int DescendentGenerations { get; set; }
        [UsedImplicitly]
        public bool PlayerControlled => IsPlayerControlled || Adventurer;

        [UsedImplicitly]
        public string Job => AssociatedType.HasValue ? AssociatedTypes[AssociatedType.Value] : "";

        [UsedImplicitly]
        public string HfCaste => Caste.HasValue ? Castes[Caste.Value] : "";

        public bool IsMale => HfCaste.ToLower().Contains("male") && !IsFemale;
        public bool IsFemale => HfCaste.ToLower().Contains("female");

        public HistoricalFigure(XDocument xdoc, World world)
            : base(xdoc, world)
        {
            foreach (var element in xdoc.Root.Elements())
            {
                var val = element.Value;
                int valI;
                int.TryParse(val, out valI);
                string[] exclude = { "entity_link", "hf_link", "hf_skill", "site_link", "entity_position_link",
                                   "entity_former_position_link", "entity_reputation", "entity_squad_link",
                                   "entity_former_squad_link", "relationship_profile_hf_visual"};
                if (val.Contains("\n") && !exclude.Contains(element.Name.LocalName))
                    Program.Log(LogType.Warning, "Historical Figures." + element.Name.LocalName + " has unknown sub items!");
                switch (element.Name.LocalName)
                {
                    case "id":
                        if (Id < 0)
                            throw new Exception();
                        break;
                    case "name":
                        Name = val.Trim();
                        break;
                    case "race":
                        Race_ = val;
                        break;
                    case "caste":
                        if (!Castes.Contains(val))
                            Castes.Add(val);
                        Caste = Castes.IndexOf(val);
                        break;
                    case "appeared":
                        AppearedYear = valI;
                        break;
                    case "birth_year":
                        BirthYear = valI;
                        break;
                    case "birth_seconds72":
                        if (valI != -1)
                            BirthSeconds = valI;
                        break;
                    case "death_year":
                        if (valI != -1)
                            DeathYear = valI;
                        break;
                    case "death_seconds72":
                        if (valI != -1)
                            DeathSeconds = valI;
                        break;
                    case "associated_type":
                        if (!AssociatedTypes.Contains(val))
                            AssociatedTypes.Add(val);
                        AssociatedType = AssociatedTypes.IndexOf(val);
                        break;
                    case "entity_link":
                        var newEl = new HFEntityLink(element, this);
                        if (EntityLinks == null)
                            EntityLinks = new Dictionary<int, List<HFEntityLink>>();
                        if (!EntityLinks.ContainsKey(newEl.LinkType))
                            EntityLinks.Add(newEl.LinkType, new List<HFEntityLink>());
                        EntityLinks[newEl.LinkType].Add(newEl);
                        break;
                    case "site_link":
                        var newSl = new HFSiteLink(element, this);
                        if (SiteLinks == null)
                            SiteLinks = new Dictionary<int, List<HFSiteLink>>();
                        if (!SiteLinks.ContainsKey(newSl.LinkType))
                            SiteLinks.Add(newSl.LinkType, new List<HFSiteLink>());
                        SiteLinks[newSl.LinkType].Add(newSl);
                        break;
                    case "sphere":
                        if (Sphere == null)
                            Sphere = new List<int>();
                        if (!Spheres.Contains(val))
                            Spheres.Add(val);
                        Sphere.Add(Spheres.IndexOf(val));
                        break;
                    case "hf_skill":
                        if (HfSkills == null)
                            HfSkills = new List<HFSkill>();
                        HfSkills.Add(new HFSkill(element));
                        break;
                    case "interaction_knowledge":
                        if (InteractionKnowledge == null)
                            InteractionKnowledge = new List<int>();
                        if (!Interactions.Contains(val))
                            Interactions.Add(val);
                        InteractionKnowledge.Add(Interactions.IndexOf(val));
                        break;
                    case "journey_pet":
                        if (JourneyPet == null)
                            JourneyPet = new List<int>();
                        if (!JourneyPets.Contains(val))
                            JourneyPets.Add(val);
                        JourneyPet.Add(JourneyPets.IndexOf(val));
                        break;
                    case "hf_link":
                        var newHfl = new HFLink(element, this);
                        if (HfLinks == null)
                            HfLinks = new Dictionary<int, List<HFLink>>();
                        if (!HfLinks.ContainsKey(newHfl.LinkType))
                            HfLinks.Add(newHfl.LinkType, new List<HFLink>());
                        HfLinks[newHfl.LinkType].Add(newHfl);
                        break;
                    case "deity":
                        Deity = true;
                        break;
                    case "entity_former_position_link":
                        if (EntityFormerPositionLinks == null)
                            EntityFormerPositionLinks = new List<EntityFormerPositionLink>();
                        EntityFormerPositionLinks.Add(new EntityFormerPositionLink(element, this));
                        break;
                    case "entity_squad_link":
                        if (EntitySquadLinks == null)
                            EntitySquadLinks = new List<EntitySquadLink>();
                        EntitySquadLinks.Add(new EntitySquadLink(element, this));
                        break;
                    case "entity_former_squad_link":
                        if (EntityFormerSquadLinks == null)
                            EntityFormerSquadLinks = new List<EntityFormerSquadLink>();
                        EntityFormerSquadLinks.Add(new EntityFormerSquadLink(element, this));
                        break;
                    case "entity_position_link":
                        if (EntityPositionLinks == null)
                            EntityPositionLinks = new List<EntityPositionLink>();
                        EntityPositionLinks.Add(new EntityPositionLink(element, this));
                        break;
                    case "relationship_profile_hf_visual":
                        if (RelationshipProfileHFs == null)
                            RelationshipProfileHFs = new List<RelationshipProfileHF>();
                        RelationshipProfileHFs.Add(new RelationshipProfileHF(element, this));
                        break;

                    case "ent_pop_id":
                        EntPop_ = valI;
                        break;
                    case "active_interaction":
                        if (!Interactions.Contains(val))
                            Interactions.Add(val);
                        ActiveInteraction = Interactions.IndexOf(val);
                        break;
                    case "goal":
                        if (!Goals.Contains(val))
                            Goals.Add(val);
                        Goal = Goals.IndexOf(val);
                        break;
                    case "force":
                        Force = true;
                        break;
                    case "entity_reputation":
                        if (EntityReputations == null)
                            EntityReputations = new List<EntityReputation>();
                        EntityReputations.Add(new EntityReputation(element));
                        break;
                    case "current_identity_id":
                        CurrentIdentityId = valI;
                        break;
                    case "used_identity_id":
                        if (UsedIdentityId != null)
                            break;
                        UsedIdentityId = valI;
                        break;
                    case "holds_artifact":
                        HoldsArtifact = val;
                        break;
                    case "animated":
                        Animated = true;
                        break;
                    case "animated_string":
                        AnimatedString = val;
                        if (Name == null)
                            Name = AnimatedString;
                        break;
                    case "ghost":
                        Ghost = true;
                        break;
                    case "adventurer":
                        Adventurer = true;
                        break;
                    case "breed_id":
                        BreedId = valI;
                        break;
                    default:
                        DFXMLParser.UnexpectedXmlElement(xdoc.Root.Name.LocalName, element, xdoc.Root.ToString());
                        break;
                }
            }
        }

        public override void Select(MainForm frm)
        {
            if (frm.grpHistoricalFigure.Text == ToString() && frm.MainTab.SelectedTab == frm.tabHistoricalFigure)
                return;
            Program.MakeSelected(frm.tabHistoricalFigure, frm.lstHistoricalFigure, this);

            frm.grpHistoricalFigure.Text = ToString();
            if (PlayerControlled)
                frm.grpHistoricalFigure.Text += @" (PLAYER CONTROLLED)";
#if DEBUG
            frm.grpHistoricalFigure.Text += $" - ID: {Id} - Notability: {Notability} - Flags: {Flags}";
#endif
            frm.grpHistoricalFigure.Show();
            frm.lblHistoricalFigureName.Text = ToString();
            frm.lblHistoricalFigureRace.Data = Race;
            frm.lblHistoricalFigureCaste.Text = Caste.HasValue ? Castes[Caste.Value] : "";
            frm.lblHistoricalFigureSex.Text = Sex.HasValue ? "" : Sex.ToString();
            frm.lblHistoricalFigureAppeared.Text = Appeared?.ToString() ?? "";
            frm.lblHistoricalFigureLife.Text = Birth == null ? "" : (Birth + " – " + (Death == WorldTime.Present ? "" : Death.ToString()));
            frm.lblHistoricalFigureAge.Text = Birth == null ? "" : WorldTime.Duration(Death, Birth);
            frm.lblHistoricalFigureAgeCaption.Text = Death == WorldTime.Present ? "Age:" : "Age at death:";
            frm.lblHistoricalFigureAssociatedType.Text = AssociatedType.HasValue ? AssociatedTypes[AssociatedType.Value]: "";
            frm.lblHistoricalFigureAnimated.Text = Animated.ToString();
            frm.lblHistoricalFigureGhost.Text = Ghost.ToString();

            if (Site != null)
            { 
                frm.lblHistoricalFigureLocation.Data = Site;
                frm.lblHistoricalFigureLocationText.Text = @"At Site:";
                frm.lblHistoricalFigureCoords.Data = new Coordinate(Site.Coords);
            }
            else if (Region != null)
            {
                frm.lblHistoricalFigureLocation.Data = Region;
                frm.lblHistoricalFigureLocationText.Text = @"At Region:";
                frm.lblHistoricalFigureCoords.Data = new Coordinate(Coords);
            }
            else
            {
                frm.lblHistoricalFigureLocation.Data = null;
                frm.lblHistoricalFigureCoords.Data = null;
            }
            frm.lblHistoricalFigureGod.Data = God;
            frm.lblHistoricalFigureGod.Text = God == null ? "" : (God + " (" + (Deity ? "Deity" : "Force") + ")");
            frm.lblHistoricalFigureLeader.Data = Leader;
            frm.lblHistoricalFigureLeader.Text = Leader == null ? "" : Leader.LeaderTypes[Leader.LeaderType].ToTitleCase();
            frm.lblHistoricalFigureUnit.Data = Unit;
            frm.lblHistoricalFigureEntityPopulation.Data = EntPop;

            frm.grpHistoricalFigureSpheres.FillListboxWith(frm.lstHistoricalFigureSpheres, Sphere?.Select(x => Spheres[x].ToTitleCase()));
            frm.grpHistoricalFigureKnowledge.FillListboxWith(frm.lstHistoricalFigureKnowledge, InteractionKnowledge?.Select(x => Interactions[x].Replace("_", " ").ToLower().ToTitleCase()));
            frm.grpHistoricalFigurePets.FillListboxWith(frm.lstHistoricalFigurePets, JourneyPet?.Select(x => JourneyPets[x].Replace("_", " ").ToLower().ToTitleCase()));
            frm.grpHistoricalFigureSkills.FillListboxWith(frm.lstHistoricalFigureSkills, HfSkills?.OrderByDescending(x => x.TotalIp)
                .Select(x => HFSkill.Skills[x.Skill].Replace("_", " ").ToLower().ToTitleCase() + " - " + IpToTitle(x.TotalIp)));

            WriteHFSummary(frm);

            if (!frm.chkHistoricalFigureDetailedView.Checked)
            {
                frm.grpHistoricalFigureEntityLinks.Visible = false;
                frm.grpHistoricalFigureHFLinks.Visible = false;
                frm.grpHistoricalFigureEvents.Visible = false;
                frm.grpHistoricalFigureAncestors.Visible = false;
                frm.grpHistoricalFigureDescendents.Visible = false;
                return;
            }


            frm.grpHistoricalFigureEntityLinks.Visible = EntityLinks != null;
            frm.trvHistoricalFigureEntityLinks.BeginUpdate();
            frm.trvHistoricalFigureEntityLinks.Nodes.Clear();
            if (EntityLinks != null)
            {
                foreach (var elList in EntityLinks)
                {
                    var thisNode = new TreeNode(HFEntityLink.LinkTypes[elList.Key].ToTitleCase());
                    foreach (var el in elList.Value)
                    {
                        var linkStrength = el.LinkStrength.HasValue ? " (" + el.LinkStrength + "%)" : "";
                        var eNode = el.Entity == null ? new TreeNode("Entity: " + el.EntityId + linkStrength) : new TreeNode(el.Entity + " - " + el.Entity.Type + linkStrength);
                        eNode.Tag = el.Entity;
                        thisNode.Nodes.Add(eNode);
                    }
                    frm.trvHistoricalFigureEntityLinks.Nodes.Add(thisNode);
                }

                frm.trvHistoricalFigureEntityLinks.ExpandAll();
            }
            frm.trvHistoricalFigureEntityLinks.EndUpdate();

            var hasHfLinks = Spouses != null || Lovers != null || Followers != null ||
                Deities != null || Masters != null || Apprentices != null ||
                Prisoners != null || Imprisoners != null || Companions != null ||
                RelationshipProfileHFs != null;
            frm.grpHistoricalFigureHFLinks.Visible = hasHfLinks; 
            frm.trvHistoricalFigureHFLinks.BeginUpdate();
            frm.trvHistoricalFigureHFLinks.Nodes.Clear();
            if (hasHfLinks)
            {
                LoadHfLinkItems(frm, Spouses, "Spouses");
                LoadHfLinkItems(frm, Lovers, "Lovers");
                LoadHfLinkItems(frm, Followers, "Followers");
                LoadHfLinkItems(frm, Masters, "Masters");
                LoadHfLinkItems(frm, Apprentices, "Apprentices");
                LoadHfLinkItems(frm, FormerMasters, "Former Masters");
                LoadHfLinkItems(frm, FormerApprentices, "Former Apprentices");
                LoadHfLinkItems(frm, Prisoners, "Prisoners");
                LoadHfLinkItems(frm, Imprisoners, "Imprisoners");
                LoadHfLinkItems(frm, Companions, "Companions");

                if (RelationshipProfileHFs != null)
                {
                    var thisNode = new TreeNode("Relationships");
                    foreach (var profile in RelationshipProfileHFs)
                    {
                        var hfNode = new TreeNode(profile.Hf.ToString()) { Tag = profile.Hf };
                        if (profile.Hf.Caste.HasValue && Castes[profile.Hf.Caste.Value].ToLower().StartsWith("male"))
                            hfNode.ForeColor = Color.Blue;
                        else if (profile.Hf.Caste.HasValue && Castes[profile.Hf.Caste.Value].ToLower().StartsWith("female"))
                            hfNode.ForeColor = Color.Red;

                        thisNode.Nodes.Add(hfNode);
                    }

                    thisNode.Text = $"{thisNode.Text} ({thisNode.Nodes.Count})";
                    frm.trvHistoricalFigureHFLinks.Nodes.Add(thisNode);
                }
                if (Deities != null)
                {
                    var thisNode = new TreeNode("Deities");

                    foreach (var hflink in HfLinks[HFLink.LinkTypes.IndexOf("deity")])
	                {

                        var hfNode = new TreeNode(hflink.Hf + " (" + hflink.LinkStrength + "%)") {Tag = hflink.Hf};
	                    if (hflink.Hf.Caste.HasValue && Castes[hflink.Hf.Caste.Value].ToLower().StartsWith("male"))
                            hfNode.ForeColor = Color.Blue;
                        else if (hflink.Hf.Caste.HasValue && Castes[hflink.Hf.Caste.Value].ToLower().StartsWith("female"))
                            hfNode.ForeColor = Color.Red;
                        thisNode.Nodes.Add(hfNode);
	                }

                    thisNode.Text = $"{thisNode.Text} ({thisNode.Nodes.Count})";
                    frm.trvHistoricalFigureHFLinks.Nodes.Add(thisNode);
                }


                if (frm.trvHistoricalFigureHFLinks.GetNodeCount(true) <= 5000)
                    frm.trvHistoricalFigureHFLinks.ExpandAll();
            }
            
            if (SlayingEvents != null)
                frm.grpHistoricalFigureHFLinks.Show();
            if (SlayingEvents != null)
            {
                var thisNode = new TreeNode("Kills");
                foreach (var evt in SlayingEvents)
                {
                    if (evt.Hf_Slayer != this || evt.Hf == null) continue;
                    var hfNode = new TreeNode(evt.Hf.ToString()) {Tag = evt.Hf};
                    if (evt.Hf.Caste.HasValue && Castes[evt.Hf.Caste.Value].ToLower().StartsWith("male"))
                        hfNode.ForeColor = Color.Blue;
                    else if (evt.Hf.Caste.HasValue && Castes[evt.Hf.Caste.Value].ToLower().StartsWith("female"))
                        hfNode.ForeColor = Color.Red;

                    thisNode.Nodes.Add(hfNode);
                }
                if (thisNode.Nodes.Count > 0)
                {
                    thisNode.Text = $"{thisNode.Text} ({thisNode.Nodes.Count})";
                    frm.trvHistoricalFigureHFLinks.Nodes.Add(thisNode);
                    frm.trvHistoricalFigureHFLinks.ExpandAll();
                }
            }
            frm.trvHistoricalFigureHFLinks.EndUpdate();


            frm.grpHistoricalFigureDeath.Visible = DiedEvent != null;
            if (DiedEvent != null)
            {
                frm.lblHistoricalFigureDeathSlayer.Data = DiedEvent.Hf_Slayer;
                frm.lblHistoricalFigureDeathLocation.Data = DiedEvent.Site == null ? DiedEvent.Subregion : (WorldObject)DiedEvent.Site;
                frm.lblHistoricalFigureDeathCause.Text = HE_HFDied.Causes[DiedEvent.Cause];
                frm.lblHistoricalFigureDeathTime.Data = DiedEvent;
                frm.lblHistoricalFigureDeathTime.Text = DiedEvent.Time.ToString();
            }

            frm.grpHistoricalFigureEvents.FillListboxWith(frm.lstHistoricalFigureEvents, Events);
            frm.grpHistoricalFigureArtifacts.FillListboxWith(frm.lstHistoricalFigureArtifacts, CreatedArtifacts);


            frm.grpHistoricalFigureAncestors.Visible = (Mother != null || Father != null);
            _ancestors = new List<HistoricalFigure>();
            frm.trvHistoricalFigureAncestors.BeginUpdate();
            frm.trvHistoricalFigureAncestors.Nodes.Clear();
            var hitMax = false;
            if (Mother != null)
            {
                var motherNode = Mother.Dead ? new TreeNode(Mother + " (" + Mother.Birth + " - " + Mother.Death + ")") : new TreeNode(Mother + " (" + Mother.Birth + " - )");

                motherNode.ForeColor = Color.Red;
                motherNode.Tag = Mother;
                frm.trvHistoricalFigureAncestors.Nodes.Add(motherNode);
                _ancestors.Add(Mother);
                hitMax = AddToAncestorsTree(motherNode, 1, 10);
            }
            if (Father != null)
            {
                var fatherNode = Father.Dead ? new TreeNode(Father + " (" + Father.Birth + " - " + Father.Death + ")") : new TreeNode(Father + " (" + Father.Birth + " - )");

                fatherNode.ForeColor = Color.Blue;
                fatherNode.Tag = Father;
                frm.trvHistoricalFigureAncestors.Nodes.Add(fatherNode);
                _ancestors.Add(Father);
                hitMax = AddToAncestorsTree(fatherNode, 1, 10);
            }
            frm.trvHistoricalFigureAncestors.EndUpdate();
            frm.grpHistoricalFigureAncestors.Text = $"Ancestors ({_ancestors.Count}{(hitMax ? "+" : "")})";
            _ancestors = null;

            frm.grpHistoricalFigureDescendents.Visible = (Children != null);
            _descendents = new List<HistoricalFigure>();
            hitMax = false;
            frm.trvHistoricalFigureDescendents.BeginUpdate();
            if (Children != null)
            {
                frm.trvHistoricalFigureDescendents.Nodes.Clear();

                foreach (var child in Children)
                {
                    var childNode = child.Dead ? new TreeNode(child + " (" + child.Birth + " - " + child.Death + ")") : new TreeNode(child + " (" + child.Birth + " - )");

                    childNode.Tag = child;
                    frm.trvHistoricalFigureDescendents.Nodes.Add(childNode);
                    _descendents.Add(child);
                    if (AddToDescendentsTree(childNode, 1, 8))
                        hitMax = true;
                }
            }
            frm.trvHistoricalFigureDescendents.EndUpdate();
            if (DescendentCount == 0)
                frm.grpHistoricalFigureDescendents.Text = $"Descendents ({_descendents.Count}{(hitMax ? "+" : "")})";
            else
            {
                frm.grpHistoricalFigureDescendents.Text = string.Format(
                    DescendentCount > 50000 ?  "Descendents ({0}+ - {1}+ Generations)" : "Descendents ({0} - {1} Generations)", 
                    DescendentCount, DescendentGenerations);
            }
            _descendents = null;
        }

#region Displaying Summary

        private string Pronoun => Sex == 0 ? "She" : "He";
        private Site BirthSite => Events.FirstOrDefault(e => e.SitesInvolved.Any())?.SitesInvolved.FirstOrDefault();
        private string BirthText => $"{Birth.ToLongString()}";
        private string BirthSiteText
        {
            get
            {

                var birthSite = BirthSite;
                return birthSite == null ? "at an unknown location" : $"at {birthSite}";
            }
        }

        private Entity BirthEntity => EntityLinks != null && EntityLinks.ContainsKey(HFEntityLink.LinkTypes.IndexOf("member")) 
            ? EntityLinks?[HFEntityLink.LinkTypes.IndexOf("member")].FirstOrDefault()?.Entity 
            : null;

        private string LastJob
        {
            get
            {
                var newjobId =
                    (Events?.LastOrDefault(e => HistoricalEvent.Types[e.Type] == "change hf job") as HE_ChangeHFJob)?
                        .NewJob;
                return newjobId != null ? (Unit.JobTypes[newjobId.Value] == "standard" ? null : Unit.JobTypes[newjobId.Value].Replace('_', ' ')) : null;
            }
        }

        private IEnumerable<HistoricalFigure> Siblings
            => Father == null || Mother == null ? Enumerable.Empty<HistoricalFigure>() : Father.Children.Intersect(Mother.Children).Where(hf => !ReferenceEquals(hf, this)); 

        private string IsWas => Dead ? "was" : "is";
        private string HasHad => Dead ? "had" : "has";
        public int? Generation;
        public Point mapPt;
        public Point mapGap = new Point(7, 7);

        private void WriteHFSummary(MainForm frm)
        {
            var rtb = frm.rtbHistoricalFigureSummary;
            try
            {
                
                rtb.Clear();
                rtb.Tag = new List<WorldObject>();

                rtb.SelectionFont = new Font(rtb.SelectionFont.FontFamily, rtb.SelectionFont.Size, FontStyle.Bold);
                rtb.AddText(ToString());
                rtb.SelectionFont = new Font(rtb.SelectionFont.FontFamily, rtb.SelectionFont.Size, FontStyle.Regular);
                var deathDisplay = Dead ? $", died {Death.ToLongString()}" : "";
                rtb.AddText($" (born {BirthText}{deathDisplay}) {IsWas} a ");
                rtb.AddLink(Race);


                if (LastJob != null)
                {
                    rtb.AddText($" {LastJob}");
                }
                if (BirthEntity != null)
                {
                    rtb.AddText(" of ");
                    rtb.AddLink(BirthEntity);
                }
                rtb.AddText(".  ");

                if (IsLeader)
                {
                    rtb.AddText($"{Pronoun} {IsWas} a ");
                    rtb.AddLink(Leader, Leader.LeaderTypes[Leader.LeaderType]);
                    rtb.AddText(" of ");
                    rtb.AddLink(Leader.Civilization);
                    if (Leader.Site != null)
                    {
                        rtb.AddText(" at ");
                        rtb.AddLink(Leader.Site);
                    }
                    rtb.AddText(".  ");
                }

                if (BirthSite != null || (Father != null && Mother != null))
                { 
                    rtb.AddText($"{Pronoun} was born");
                    if (Father != null && Mother != null)
                    {
                        rtb.AddText(" to ");
                        rtb.AddLink(Mother);
                        rtb.AddText(" and ");
                        rtb.AddLink(Father);
                    }
                    var birthSite = BirthSite;
                    if (birthSite != null)
                    {
                        rtb.AddText(" at ");
                        rtb.AddText(BirthSiteText);
                    }
                    rtb.AddText(".  ");
                }

                var siblings = Siblings.ToList();
                if (Siblings.Any())
                {
                    rtb.AddText($"{Pronoun} {HasHad} {siblings.Count} sibling{(siblings.Count > 1 ? "s" : "")} (");
                    for (var i = 0; i < siblings.Count; i++)
                    {
                        rtb.AddLink(siblings[i]);
                        if (i < siblings.Count - 2)
                            rtb.AddText(", ");
                        else if (i < siblings.Count - 1)
                            rtb.AddText(", and ");

                    }
                    rtb.AddText(").  ");
                }

                var childrenBySpouse = Children?.GroupBy(c => c.Mother == this ? c.Father : c.Mother);

                if (childrenBySpouse != null)
                {
                    foreach (var spouseChildrenGroup in childrenBySpouse)
                    {
                        rtb.AddText(
                            $"{Pronoun} {HasHad} {(spouseChildrenGroup.Count() == 1 ? "1 child" : $"{spouseChildrenGroup.Count()} children")} with ");
                        rtb.AddLink(spouseChildrenGroup.Key);
                        rtb.AddText(": ");
                        foreach (var spouseChild in spouseChildrenGroup)
                        {
                            rtb.AddLink(spouseChild);
                            if (spouseChildrenGroup.Count() > 1 && spouseChild == spouseChildrenGroup.ToArray()[spouseChildrenGroup.Count() - 2])
                            {
                                rtb.AddText(", and ");
                            }
                            else if (spouseChild != spouseChildrenGroup.Last())
                            {
                                rtb.AddText(", ");
                            }
                        }
                        rtb.AddText(".  ");

                    }
                }

                if (Dead && DiedEvent != null)
                {
                    //HE_HFDied.Causes[DiedEvent.Cause]
                    if (DiedEvent.Hf_Slayer != null)
                    {
                        rtb.AddText($"{Pronoun} was {HE_HFDied.Causes[DiedEvent.Cause]} by ");
                        rtb.AddLink(DiedEvent.Hf_Slayer);
                        if (DiedEvent.Site != null)
                        { 
                            rtb.AddText(" at ");
                            rtb.AddLink(DiedEvent.Site);
                        }
                    }
                    else
                    {
                        rtb.AddText($"{Pronoun} died of {HE_HFDied.Causes[DiedEvent.Cause]}");
                        if (DiedEvent.Site != null)
                        {
                            rtb.AddText(" at ");
                            rtb.AddLink(DiedEvent.Site);
                        }
                    }
                    rtb.AddText(".  ");
                }

                rtb.AddText("\n\n");
                //rtb.AddText(timer.ElapsedMilliseconds.ToString());
            }
            catch (Exception e)
            {
                rtb.Clear();
                rtb.AddText("Error generating HF Summary: " + e);
            }
        }

#endregion


        private void LoadHfLinkItems(MainForm frm, List<HistoricalFigure> hflinklist, string treenodename)
        {
            if (hflinklist == null) return;

            var thisNode = new TreeNode(treenodename);
            foreach (var hf in hflinklist)
            {
                var hfNode = new TreeNode(hf.ToString()) {Tag = hf};
                if (hf.Caste.HasValue && Castes[hf.Caste.Value].ToLower().StartsWith("male"))
                    hfNode.ForeColor = Color.Blue;
                else if (hf.Caste.HasValue && Castes[hf.Caste.Value].ToLower().StartsWith("female"))
                    hfNode.ForeColor = Color.Red;
                thisNode.Nodes.Add(hfNode);
            }
            thisNode.Text = $"{thisNode.Text} ({thisNode.Nodes.Count})";
            frm.trvHistoricalFigureHFLinks.Nodes.Add(thisNode);
        }

        private static string IpToTitle(int ip)
        {
            if (ip < 500)
                return "Dabbling";
            if (ip < 1100)
                return "Novice";
            if (ip < 1800)
                return "Adequate";
            if (ip < 2600)
                return "Competent";
            if (ip < 3500)
                return "Skilled";
            if (ip < 4500)
                return "Proficient";
            if (ip < 5600)
                return "Talented";
            if (ip < 6800)
                return "Adept";
            if (ip < 8100)
                return "Expert";
            if (ip < 9500)
                return "Professional";
            if (ip < 11000)
                return "Accomplished";
            if (ip < 12600)
                return "Great";
            if (ip < 14300)
                return "Master";
            if (ip < 16100)
                return "High Master";
            if (ip < 18000)
                return "Grand Master";
            if (ip < 20000)
                return "Legendary";
            if (ip < 22100)
                return "Legendeary+1";
            if (ip < 24300)
                return "Legendary+2";
            if (ip < 26600)
                return "Legendary+3";
            return ip < 2900 ? "Legendary+4" : "Legendary+5";
        }

        private bool AddToDescendentsTree(TreeNode parentNode, int depth, int maxDepth)
        {
            var hf = (HistoricalFigure)parentNode.Tag;
            var hitMax = false;
            if (hf.Children != null && depth <= maxDepth)
            {
                foreach (var child in hf.Children)
                {
                    if (child.Mother == hf)
                        parentNode.ForeColor = Color.Red;
                    else if (child.Father == hf)
                        parentNode.ForeColor = Color.Blue;
                    var childNode = child.Dead ? new TreeNode(child + " (" + child.Birth + " - " + child.Death +")") : new TreeNode(child + " (" + child.Birth + " - )");
                    if (depth == maxDepth)
                        childNode.Text += @"...";
                    childNode.Tag = child;
                    if (!_descendents.Contains(child))
                        _descendents.Add(child);
                    parentNode.Nodes.Add(childNode);
                    if (AddToDescendentsTree(childNode, depth + 1, maxDepth))
                        hitMax = true;
                }
            }
            else
            {
                if (hf.Caste.HasValue && Castes[hf.Caste.Value].ToLower() == "male")
                    parentNode.ForeColor = Color.Blue;
                else if (hf.Caste.HasValue && Castes[hf.Caste.Value].ToLower() == "female")
                    parentNode.ForeColor = Color.Red;
            }
            return depth > maxDepth || hitMax;
        }

        private bool AddToAncestorsTree(TreeNode childNode, int depth, int maxDepth)
        {
            var hf = (HistoricalFigure)childNode.Tag;
            var hitMax = false;
            if (hf.Mother != null && depth <= maxDepth)
            {
                var motherNode = new TreeNode(hf.Mother.ToString());
                if (depth == maxDepth)
                    motherNode.Text += @"...";

                motherNode.ForeColor = Color.Red;
                motherNode.Tag = hf.Mother;
                if (!_ancestors.Contains(hf.Mother))
                    _ancestors.Add(hf.Mother);
                childNode.Nodes.Add(motherNode);
                hitMax = AddToAncestorsTree(motherNode, depth + 1, maxDepth);
            }
            else if (depth > maxDepth)
                hitMax = true;
            if (hf.Father != null && depth <= maxDepth)
            {
                var fatherNode = new TreeNode(hf.Father.ToString());
                if (depth == maxDepth)
                    fatherNode.Text += @"...";
                fatherNode.ForeColor = Color.Blue;
                fatherNode.Tag = hf.Father;
                if (!_ancestors.Contains(hf.Father))
                    _ancestors.Add(hf.Father);
                childNode.Nodes.Add(fatherNode);
                if (AddToAncestorsTree(fatherNode, depth + 1, maxDepth))
                    hitMax = true;
            }
            else if (depth > maxDepth)
                hitMax = true;
            return hitMax;
        }

        internal override void Link()
        {
            //Birth = BirthYear.HasValue ? new WorldTime(BirthYear.Value, BirthSeconds) : WorldTime.Present;
            //Death = DeathYear.HasValue ? new WorldTime(DeathYear.Value, DeathSeconds) : WorldTime.Present;
            //if (AppearedYear != null) 
            //    Appeared = new WorldTime(AppearedYear.Value);
            try
            {
                if (Race_ != null)
                {
                    Race = World.GetAddRace(Race_);
                    Race_ = null;
                }

                if (EntPop_.HasValue && World.EntityPopulations.ContainsKey(EntPop_.Value))
                {
                    EntPop = World.EntityPopulations[EntPop_.Value];
                    if (EntPop.Members == null)
                        EntPop.Members = new List<HistoricalFigure>();
                    EntPop.Members.Add(this);
                }
                if (Leader?.InheritanceId != null && World.HistoricalFigures.ContainsKey(Leader.InheritanceId.Value))
                {
                    Leader.InheritedFrom = World.HistoricalFigures[Leader.InheritanceId.Value];
                }
            }
            catch (Exception)
            {

                Program.Log(LogType.Error, $"Error Linking HF: {this}");
            }
        }

        internal override void Process()
        {
            if (SiteLinks != null)
            {
                foreach (var linklist in SiteLinks)
                {
                    foreach (var sl in linklist.Value)
                    {
                        if (sl.Site.Inhabitants == null)
                            sl.Site.Inhabitants = new List<HistoricalFigure>();
                        if (!sl.Site.Inhabitants.Contains(this))
                            sl.Site.Inhabitants.Add(this);
                        Site = sl.Site;
                        switch (HFSiteLink.LinkTypes[linklist.Key])
                        {
                            case "lair":
                            case "home structure":
                            case "seat of power":
                            case "home site building":
                            case "home site underground":
                            case "hangout":
                                break;
                            case "occupation":
                                break;
                            default:
                                Program.Log(LogType.Warning, "Unknown HF Site Link: " + linklist.Key + " - " + HFSiteLink.LinkTypes[linklist.Key]);
                                break;
                        }
                    }

                }
            }
            if (EntityLinks != null)
            {
                foreach (var linklist in EntityLinks)
                {
                    foreach (var el in linklist.Value.Where(el => el.Entity != null))
                    {
                        switch (HFEntityLink.LinkTypes[linklist.Key])
                        {
                            case "enemy":

                                if (el.Entity.Enemies == null)
                                    el.Entity.Enemies = new List<HistoricalFigure>();
                                el.Entity.Enemies.Add(this);
                                if (EnemyOf == null)
                                    EnemyOf = new List<Entity>();
                                EnemyOf.Add(el.Entity);
                                break;
                            case "member":
                                if (el.Entity.Members == null)
                                    el.Entity.Members = new List<HistoricalFigure>();
                                el.Entity.Members.Add(this);
                                if (MemberOf == null)
                                    MemberOf = new List<Entity>();
                                MemberOf.Add(el.Entity);
                                break;
                            case "former member":
                                if (el.Entity.FormerMembers == null)
                                    el.Entity.FormerMembers = new List<HistoricalFigure>();
                                el.Entity.FormerMembers.Add(this);
                                if (FormerMemberOf == null)
                                    FormerMemberOf = new List<Entity>();
                                FormerMemberOf.Add(el.Entity);
                                break;
                            case "prisoner":
                                if (el.Entity.Prisoners == null)
                                    el.Entity.Prisoners = new List<HistoricalFigure>();
                                el.Entity.Prisoners.Add(this);
                                if (PrisonerOf == null)
                                    PrisonerOf = new List<Entity>();
                                PrisonerOf.Add(el.Entity);
                                break;
                            case "former prisoner":
                                if (el.Entity.FormerPrisoners == null)
                                    el.Entity.FormerPrisoners = new List<HistoricalFigure>();
                                el.Entity.FormerPrisoners.Add(this);
                                if (FormerPrisonerOf == null)
                                    FormerPrisonerOf = new List<Entity>();
                                FormerPrisonerOf.Add(el.Entity);
                                break;
                            case "criminal":
                                if (el.Entity.Criminals == null)
                                    el.Entity.Criminals = new List<HistoricalFigure>();
                                el.Entity.Criminals.Add(this);
                                if (CriminalOf == null)
                                    CriminalOf = new List<Entity>();
                                CriminalOf.Add(el.Entity);
                                break;
                            case "slave":
                                if (el.Entity.Slaves == null)
                                    el.Entity.Slaves = new List<HistoricalFigure>();
                                el.Entity.Slaves.Add(this);
                                if (SlaveOf == null)
                                    SlaveOf = new List<Entity>();
                                SlaveOf.Add(el.Entity);
                                break;
                            case "former slave":
                                if (el.Entity.FormerSlaves == null)
                                    el.Entity.FormerSlaves = new List<HistoricalFigure>();
                                el.Entity.FormerSlaves.Add(this);
                                if (FormerSlaveOf == null)
                                    FormerSlaveOf = new List<Entity>();
                                FormerSlaveOf.Add(el.Entity);
                                break;
                            case "hero":
                                if (el.Entity.Heroes == null)
                                    el.Entity.Heroes = new List<HistoricalFigure>();
                                el.Entity.Heroes.Add(this);
                                if (HeroOf == null)
                                    HeroOf = new List<Entity>();
                                HeroOf.Add(el.Entity);
                                break;
                            default:
                                Program.Log(LogType.Warning, "Unknown HF Entity Link: " + linklist.Key);
                                break;
                        }
                    }
                }
            }
            if (HfLinks != null)
            {
                foreach (var linklist in HfLinks)
                {
                    foreach (var hfl in linklist.Value.Where(hfl => hfl.Hf != null))
                    {
                        switch (HFLink.LinkTypes[linklist.Key])
                        {

                            case "child":
                                if (Children == null)
                                    Children = new List<HistoricalFigure>();
                                if (!Children.Contains(hfl.Hf))
                                {
                                    Children.Add(hfl.Hf);
                                    if (hfl.Hf.Parents == null)
                                        hfl.Hf.Parents = new List<HistoricalFigure>();
                                    hfl.Hf.Parents.Add(this);
                                }
                                break;
                            case "spouse":
                                if (hfl.Hf.Spouses == null)
                                    hfl.Hf.Spouses = new List<HistoricalFigure>();
                                if (!hfl.Hf.Spouses.Contains(this))
                                {
                                    hfl.Hf.Spouses.Add(this);
                                    if (Spouses == null)
                                        Spouses = new List<HistoricalFigure>();
                                    Spouses.Add(hfl.Hf);
                                }
                                break;
                            case "lover":
                                if (hfl.Hf.Lovers == null)
                                    hfl.Hf.Lovers = new List<HistoricalFigure>();
                                if (!hfl.Hf.Lovers.Contains(this))
                                {
                                    hfl.Hf.Lovers.Add(this);
                                    if (Lovers == null)
                                        Lovers = new List<HistoricalFigure>();
                                    Lovers.Add(hfl.Hf);
                                }
                                break;
                            case "deity":
                                if (hfl.Hf.Followers == null)
                                    hfl.Hf.Followers = new List<HistoricalFigure>();
                                hfl.Hf.Followers.Add(this);
                                if (Deities == null)
                                    Deities = new List<HistoricalFigure>();
                                Deities.Add(hfl.Hf);
                                break;
                            case "mother":
                                Mother = hfl.Hf;
                                if (hfl.Hf.Children == null)
                                    hfl.Hf.Children = new List<HistoricalFigure>();
                                if (!hfl.Hf.Children.Contains(this))
                                    hfl.Hf.Children.Add(this);
                                else
                                {
                                    if (Parents == null)
                                        Parents = new List<HistoricalFigure>();
                                    if (Parents.Contains(hfl.Hf))
                                        Parents.Remove(Mother);
                                }
                                break;
                            case "father":
                                Father = hfl.Hf;
                                if (hfl.Hf.Children == null)
                                    hfl.Hf.Children = new List<HistoricalFigure>();
                                if (!hfl.Hf.Children.Contains(this))
                                    hfl.Hf.Children.Add(this);
                                else
                                {
                                    if (Parents == null)
                                        Parents = new List<HistoricalFigure>();
                                    if (Parents.Contains(hfl.Hf))
                                        Parents.Remove(Father);
                                }
                                break;
                            case "master":
                                if (hfl.Hf.Apprentices == null)
                                    hfl.Hf.Apprentices = new List<HistoricalFigure>();
                                if (!hfl.Hf.Apprentices.Contains(this))
                                {
                                    hfl.Hf.Apprentices.Add(this);
                                    if (Masters == null)
                                        Masters = new List<HistoricalFigure>();
                                    Masters.Add(hfl.Hf);
                                }
                                break;
                            case "apprentice":
                                if (hfl.Hf.Masters == null)
                                    hfl.Hf.Masters = new List<HistoricalFigure>();
                                if (!hfl.Hf.Masters.Contains(this))
                                {
                                    hfl.Hf.Masters.Add(this);
                                    if (Apprentices == null)
                                        Apprentices = new List<HistoricalFigure>();
                                    Apprentices.Add(hfl.Hf);
                                }
                                break;
                            case "former master":
                                if (hfl.Hf.FormerApprentices == null)
                                    hfl.Hf.FormerApprentices = new List<HistoricalFigure>();
                                if (!hfl.Hf.FormerApprentices.Contains(this))
                                {
                                    hfl.Hf.FormerApprentices.Add(this);
                                    if (FormerMasters == null)
                                        FormerMasters = new List<HistoricalFigure>();
                                    FormerMasters.Add(hfl.Hf);
                                }
                                break;
                            case "former apprentice":
                                if (hfl.Hf.FormerMasters == null)
                                    hfl.Hf.FormerMasters = new List<HistoricalFigure>();
                                if (!hfl.Hf.FormerMasters.Contains(this))
                                {
                                    hfl.Hf.FormerMasters.Add(this);
                                    if (FormerApprentices == null)
                                        FormerApprentices = new List<HistoricalFigure>();
                                    FormerApprentices.Add(hfl.Hf);
                                }
                                break;
                            case "prisoner":
                                if (hfl.Hf.Prisoners == null)
                                    hfl.Hf.Prisoners = new List<HistoricalFigure>();
                                if (!hfl.Hf.Prisoners.Contains(this))
                                {
                                    hfl.Hf.Prisoners.Add(this);
                                }
                                break;
                            case "imprisoner":
                                if (hfl.Hf.Imprisoners == null)
                                    hfl.Hf.Imprisoners = new List<HistoricalFigure>();
                                if (!hfl.Hf.Imprisoners.Contains(this))
                                {
                                    hfl.Hf.Imprisoners.Add(this);
                                }
                                break;
                            case "companion":
                                if (hfl.Hf.Companions == null)
                                    hfl.Hf.Companions = new List<HistoricalFigure>();
                                if (!hfl.Hf.Companions.Contains(this))
                                {
                                    hfl.Hf.Companions.Add(this);
                                }
                                break;
                            default:
                                Program.Log(LogType.Warning, "Unknown HF HF Link: " + linklist.Key + " - " + HFLink.LinkTypes[linklist.Key]);
                                break;
                        }
                    }
                }
            }

            //if (EntitySquadLinks != null)
            //{
            //        foreach (var esl in EntitySquadLinks)
            //            Console.WriteLine(ToString() + " - " + esl.ToString());
            //}

            if (!IsLeader || Leader.InheritanceId != -1) return;

            switch (Leader.InheritedFromSource)
            {
                case Leader.InheritanceSource.PaternalGrandMother:
                    Leader.InheritedFrom = Father.Mother;
                    break;
                case Leader.InheritanceSource.MaternalGrandMother:
                    Leader.InheritedFrom = Mother.Mother;
                    break;
                case Leader.InheritanceSource.PaternalGrandFather:
                    Leader.InheritedFrom = Father.Father;
                    break;
                case Leader.InheritanceSource.MaternalGrandFather:
                    Leader.InheritedFrom = Mother.Father;
                    break;
                case Leader.InheritanceSource.Mother:
                case Leader.InheritanceSource.Father:
                case Leader.InheritanceSource.Other:
                    break;
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
                        break;
                    case "flags":
                        Flags = Convert.ToInt32(val, 2);
                        break;
                    case "sex":
                        if (valI != -1)
                            Sex = valI;
                        break;
                    case "race": //Ignore, captured elsewhere
                        break;
                    default:
                        DFXMLParser.UnexpectedXmlElement(xdoc.Root.Name.LocalName + "\t", element, xdoc.Root.ToString());
                        break;
                }
            }


        }


        #region Count Families
        internal void CountDescendents()
        {
            if (DescendentCount == -1)
                DescendentCount = 0;
            if (Children != null)
            {
                foreach (var hf in Children)
                {
                    if (hf != null)
                    {
                        if (hf.DescendentCount == -1) 
                            hf.CountDescendents();
                        DescendentCount += hf.DescendentCount;
                    }
                }
            }
        }

        internal void CountAncestors()
        {
            if (AncestorCount == -1)
                AncestorCount = 0;
            if (Mother != null)
            {
                if (Mother.AncestorCount == -1)
                    Mother.CountAncestors();
                AncestorCount += Mother.AncestorCount;
            }
            if (Father != null)
            {
                if (Father.AncestorCount == -1)
                    Father.CountAncestors();
                AncestorCount += Father.AncestorCount;
            }
        }
        #endregion 
    
        internal override void Export(string table)
        {

            var vals = new List<object>
            {
                Id,
                Name.DBExport(),
                Race.DBExport(),
                Caste.DBExport(Castes),
                Appeared.DBExport(true),
                Birth.DBExport(true),
                Birth.DBExport(false),
                Death.DBExport(true),
                Death.DBExport(false),
                AssociatedType.DBExport(AssociatedTypes),
                Deity,
                Force,
                Animated,
                Ghost,
                Adventurer,
                EntPop.DBExport(),
                Goal.DBExport(Goals),
                Sphere.DBExport(Spheres),
                InteractionKnowledge.DBExport(Interactions),
                JourneyPet.DBExport(JourneyPets)
            };

                
            Database.ExportWorldItem(table, vals);

            if (EntityFormerPositionLinks != null)
            {
                foreach (var entityFormerPositionLink in EntityFormerPositionLinks)
                    entityFormerPositionLink.Export(Id);
            }

            if (EntityPositionLinks != null)
            {
                foreach (var entityPositionLink in EntityPositionLinks)
                    entityPositionLink.Export(Id);
            }

            if (EntityReputations != null)
            {
                foreach (var entityReputation in EntityReputations)
                    entityReputation.Export(Id);
            }

            if (HfSkills != null)
            {
                foreach (var hfSkill in HfSkills)
                    hfSkill.Export(Id);
            }

            if (EntityLinks != null)
            {
                foreach (var entityLink in EntityLinks.Values.SelectMany(entityLinkList => entityLinkList))
                {
                    entityLink.Export(Id);
                }
            }

            if (HfLinks != null)
            {
                foreach (var hfLink in HfLinks.Values.SelectMany(hfLinkList => hfLinkList))
                {
                    hfLink.Export(Id);
                }
            }

            if (SiteLinks == null) return;
            foreach (var siteLink in SiteLinks.Values.SelectMany(siteLinkList => siteLinkList))
            {
                siteLink.Export(Id);
            }
        }


        public override string ToString()
        {
            return base.ToString().Replace(" The ", " the ");
        }
    }
}