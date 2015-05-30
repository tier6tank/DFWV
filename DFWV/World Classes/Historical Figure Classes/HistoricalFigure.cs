using DFWV.WorldClasses.HistoricalEventClasses;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;
using DFWV.WorldClasses.HistoricalEventCollectionClasses;
using System.Collections.Specialized;

namespace DFWV.WorldClasses.HistoricalFigureClasses
{
    class HistoricalFigure : XMLObject
    {

        public string Race_ { get; set; } 
        public Race Race { get; set; }
        public int? Caste { get; set; }
        public static List<string> Castes = new List<string>();
        public int? AppearedYear { get; set; }
        public WorldTime Appeared { get; set; }
        public int? BirthYear { get; set; }
        public int? BirthSeconds { get; set; }
        public WorldTime Birth { get; set; }
        public int? DeathYear { get; set; }
        public int? DeathSeconds { get; set; }
        public WorldTime Death { get; set; }
        public static List<string> AssociatedTypes = new List<string>();
        public int? AssociatedType { get; set; }
        public Dictionary<string, List<EntityLink>> EntityLinks { get; set; }
        public Dictionary<string, List<SiteLink>> SiteLinks { get; set; }
        public List<int> Sphere { get; set; }
        public static List<string> Spheres = new List<string>();
        public List<HFSkill> HFSkills { get; set; }
        public static List<string> Interactions = new List<string>();
        public List<int> InteractionKnowledge { get; set; }
        public List<int> JourneyPet { get; set; }
        public static List<string> JourneyPets = new List<string>();
        public Dictionary<string, List<HFLink>> HFLinks { get; set; }
        public bool Deity { get; set; }
        public List<EntityFormerPositionLink> EntityFormerPositionLinks { get; set; }
        public List<EntityPositionLink> EntityPositionLinks { get; set; }
        public int? EntPop_ { get; set; }
        public EntityPopulation EntPop { get; set; }
        public int? ActiveInteraction { get; set; }
        public static List<string> Goals = new List<string>();
        public int? Goal { get; set; }
        public bool Force { get; set; }
        public List<EntityReputation> EntityReputations { get; set; }
        public int? CurrentIdentityID { get; set; }
        public int? UsedIdentityID { get; set; }
        public string HoldsArtifact { get; set; }
        public bool Animated { get; set; }
        public string AnimatedString { get; set; }
        public bool Ghost { get; set; }
        public bool Adventurer { get; set; }

        public Leader Leader { get; set; }
        public God God { get; set; }

        public bool isPlayerControlled { get; set; }

        public string FirstName
        {
            get
            {
                if (ShortName == null)
                    return null;
                return Name.Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)[0];
            }
        }
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

        public List<Entity> EnemyOf { get; set; }
        public List<Entity> MemberOf { get; set; }
        public List<Entity> FormerMemberOf { get; set; }
        public List<Entity> PrisonerOf { get; set; }
        public List<Entity> FormerPrisonerOf { get; set; }
        public List<Entity> CriminalOf { get; set; }
        public List<Entity> SlaveOf { get; set; }
        public List<Entity> FormerSlaveOf { get; set; }
        public List<Entity> HeroOf { get; set; }

        public List<Artifact> CreatedArtifacts { get; set; }

        public List<HistoricalFigure> Children { get; set; }
        public List<HistoricalFigure> Spouses { get; set; }
        public List<HistoricalFigure> Lovers { get; set; }
        public List<HistoricalFigure> Followers { get; set; }
        public List<HistoricalFigure> Deities { get; set; }
        public List<HistoricalFigure> Parents { get; set; }
        public List<HistoricalFigure> Apprentices { get; set; }
        public List<HistoricalFigure> Masters { get; set; }
        public List<HistoricalFigure> Prisoners { get; set; }

        public HistoricalFigure Mother { get; set; }
        public HistoricalFigure Father { get; set; }
        private List<HistoricalFigure> Descendents;
        private List<HistoricalFigure> Ancestors;

        public HE_HFDied DiedEvent { get; set; }
        public List<HistoricalEvent> Events { get; set; }
        public List<HE_HFDied> SlayingEvents { get; set; }


        public List<EC_Battle> BattleEventCollections { get; set; }
        public List<EC_Duel> DuelEventCollections { get; set; }

        override public Point Location { get { return Coords; } }


        public bool Dead { get { return Death != WorldTime.Present; } }
        public bool inEntPop { get { return EntPop != null; } }
        public bool isLeader { get { return Leader != null; } }
        public bool isGod { get { return God != null; } }
        public int CreatedArtifactCount { get { return CreatedArtifacts == null ? 0 : CreatedArtifacts.Count; } }
        public int CreatedMasterpieceCount { get { return Events == null ? 0 : Events.Where(x => HistoricalEvent.Types[x.Type].Contains("masterpiece")).Count(); } }
        public int ChildrenCount { get { return Children == null ? 0 : Children.Count; } }
        public int Kills { get { return SlayingEvents == null ? 0 : SlayingEvents.Count; } }
        public int Battles { get { return BattleEventCollections == null ? 0 : BattleEventCollections.Count; } }
        public string DispNameLower { get { return ToString().ToLower(); } }
        public int EntPopID { get { return EntPop == null ? -1 : EntPop.ID; } }
        public int DescendentCount { get; set; }
        public int AncestorCount { get; set; }
        public int DescendentGenerations { get; set; }
        public bool PlayerControlled { get; set; }
        public string Job { get { return AssociatedType.HasValue ? AssociatedTypes[AssociatedType.Value] : ""; } }
        public string HFCaste { get { return Caste.HasValue ? Castes[Caste.Value] : ""; } }
        public bool isMale { get { return HFCaste.ToLower().Contains("male") && !isFemale; } }
        public bool isFemale { get { return HFCaste.ToLower().Contains("female"); } }

        public HistoricalFigure(XDocument xdoc, World world)
            : base(xdoc, world)
        {
            foreach (XElement element in xdoc.Root.Elements())
            {
                string val = element.Value.ToString();
                int valI;
                Int32.TryParse(val, out valI);
                string[] exclude = { "entity_link", "hf_link", "hf_skill", "site_link", "entity_position_link",
                                   "entity_former_position_link", "entity_reputation"};
                if (val.Contains("\n") && !exclude.Contains(element.Name.LocalName))
                    Program.Log(LogType.Warning, element.Name.LocalName + " has unknown sub items!");
                switch (element.Name.LocalName)
                {
                    case "id":
                        break;
                    case "name":
                        Name = val.Trim();
                        break;
                    case "race":
                        Race_ = val;
                        break;
                    case "caste":
                        if (!HistoricalFigure.Castes.Contains(val))
                            HistoricalFigure.Castes.Add(val);
                        Caste = HistoricalFigure.Castes.IndexOf(val);
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
                        if (!HistoricalFigure.AssociatedTypes.Contains(val))
                            HistoricalFigure.AssociatedTypes.Add(val);
                        AssociatedType = HistoricalFigure.AssociatedTypes.IndexOf(val);
                        break;
                    case "entity_link":
                        EntityLink newEL = new EntityLink(element);
                        if (EntityLinks == null)
                            EntityLinks = new Dictionary<string, List<EntityLink>>();
                        if (!EntityLinks.ContainsKey(EntityLink.LinkTypes[newEL.LinkType]))
                            EntityLinks.Add(EntityLink.LinkTypes[newEL.LinkType], new List<EntityLink>());
                        EntityLinks[EntityLink.LinkTypes[newEL.LinkType]].Add(newEL);

                        break;
                    case "site_link":
                        SiteLink newSL = new SiteLink(element);
                        if (SiteLinks == null)
                            SiteLinks = new Dictionary<string, List<SiteLink>>();
                        if (!SiteLinks.ContainsKey(SiteLink.LinkTypes[newSL.LinkType]))
                            SiteLinks.Add(SiteLink.LinkTypes[newSL.LinkType], new List<SiteLink>());
                        SiteLinks[SiteLink.LinkTypes[newSL.LinkType]].Add(newSL);
                        break;
                    case "sphere":
                        if (Sphere == null)
                            Sphere = new List<int>();
                        if (!HistoricalFigure.Spheres.Contains(val))
                            HistoricalFigure.Spheres.Add(val);
                        Sphere.Add(HistoricalFigure.Spheres.IndexOf(val));
                        break;
                    case "hf_skill":
                        if (HFSkills == null)
                            HFSkills = new List<HFSkill>();
                        HFSkills.Add(new HFSkill(element));
                        break;
                    case "interaction_knowledge":
                        if (InteractionKnowledge == null)
                            InteractionKnowledge = new List<int>();
                        if (!HistoricalFigure.Interactions.Contains(val))
                            HistoricalFigure.Interactions.Add(val);
                        InteractionKnowledge.Add(HistoricalFigure.Interactions.IndexOf(val));
                        break;
                    case "journey_pet":
                        if (JourneyPet == null)
                            JourneyPet = new List<int>();
                        if (!HistoricalFigure.JourneyPets.Contains(val))
                            HistoricalFigure.JourneyPets.Add(val);
                        JourneyPet.Add(HistoricalFigure.JourneyPets.IndexOf(val));
                        break;
                    case "hf_link":
                        HFLink newHFL = new HFLink(element);
                        if (HFLinks == null)
                            HFLinks = new Dictionary<string, List<HFLink>>();
                        if (!HFLinks.ContainsKey(HFLink.LinkTypes[newHFL.LinkType]))
                            HFLinks.Add(HFLink.LinkTypes[newHFL.LinkType], new List<HFLink>());
                        HFLinks[HFLink.LinkTypes[newHFL.LinkType]].Add(newHFL);
                        break;
                    case "deity":
                        Deity = true;
                        break;
                    case "entity_former_position_link":
                        if (EntityFormerPositionLinks == null)
                            EntityFormerPositionLinks = new List<EntityFormerPositionLink>();
                        EntityFormerPositionLinks.Add(new EntityFormerPositionLink(element));
                        break;
                    case "entity_position_link":
                        if (EntityPositionLinks == null)
                            EntityPositionLinks = new List<EntityPositionLink>();
                        EntityPositionLinks.Add(new EntityPositionLink(element));
                        break;
                    case "ent_pop_id":
                        EntPop_ = valI;
                        break;
                    case "active_interaction":
                        if (!HistoricalFigure.Interactions.Contains(val))
                            HistoricalFigure.Interactions.Add(val);
                        ActiveInteraction = HistoricalFigure.Interactions.IndexOf(val);
                        break;
                    case "goal":
                        if (!HistoricalFigure.Goals.Contains(val))
                            HistoricalFigure.Goals.Add(val);
                        Goal = HistoricalFigure.Goals.IndexOf(val);
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
                        CurrentIdentityID = valI;
                        break;
                    case "used_identity_id":
                        if (UsedIdentityID != null)
                            break;
                        UsedIdentityID = valI;
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
                    default:
                        DFXMLParser.UnexpectedXMLElement(xdoc.Root.Name.LocalName, element, xdoc.Root.ToString());
                        break;
                }
            }
        }

        //public HistoricalFigure(NameValueCollection data, World world) 
        //    : base (world)
        //{
        //    Name = data["Name"].ToString();

        //}

        public override void Select(MainForm frm)
        {
            frm.grpHistoricalFigure.Text = ToString();
            if (isPlayerControlled)
                frm.grpHistoricalFigure.Text += " (PLAYER CONTROLLED)";
            frm.grpHistoricalFigure.Show();
            frm.lblHistoricalFigureName.Text = ToString();
            frm.lblHistoricalFigureRace.Data = Race;
            frm.lblHistoricalFigureCaste.Text = Caste.HasValue ? Castes[Caste.Value] : "";
            frm.lblHistoricalFigureAppeared.Text = Appeared == null ? "" : Appeared.ToString();
            frm.lblHistoricalFigureLife.Text = Birth == null ? "" : (Birth.ToString() + " – " + (Death == WorldTime.Present ? "" : Death.ToString()));
            frm.lblHistoricalFigureAge.Text = Birth == null ? "" : WorldTime.Duration(Death, Birth);
            frm.lblHistoricalFigureAgeCaption.Text = Death == WorldTime.Present ? "Age:" : "Age at death:";
            frm.lblHistoricalFigureAssociatedType.Text = AssociatedType.HasValue ? AssociatedTypes[AssociatedType.Value]: "";
            frm.lblHistoricalFigureAnimated.Text = Animated.ToString();
            frm.lblHistoricalFigureGhost.Text = Ghost.ToString();

            if (Site != null)
            { 
                frm.lblHistoricalFigureLocation.Data = Site;
                frm.lblHistoricalFigureLocationText.Text = "At Site:";
                frm.lblHistoricalFigureCoords.Data = new Coordinate(Site.Coords);
            }
            else if (Region != null)
            {
                frm.lblHistoricalFigureLocation.Data = Region;
                frm.lblHistoricalFigureLocationText.Text = "At Region:";
                frm.lblHistoricalFigureCoords.Data = new Coordinate(Coords);
            }
            else
            {
                frm.lblHistoricalFigureLocation.Data = null;
                frm.lblHistoricalFigureCoords.Data = null;
            }
            frm.lblHistoricalFigureGod.Data = God;
            frm.lblHistoricalFigureGod.Text = God == null ? "" : (God.ToString() + " (" + (Deity ? "Deity" : "Force") + ")");
            frm.lblHistoricalFigureLeader.Data = Leader;
            frm.lblHistoricalFigureEntityPopulation.Data = EntPop;

            frm.grpHistoricalFigureSpheres.Visible = Sphere != null;
            if (Sphere != null)
            {
                frm.lstHistoricalFigureSpheres.Items.Clear();
                foreach (int curSphere in Sphere)
                    frm.lstHistoricalFigureSpheres.Items.Add(
                        CultureInfo.CurrentCulture.TextInfo.ToTitleCase(HistoricalFigure.Spheres[curSphere]));
            }

            frm.grpHistoricalFigureKnowledge.Visible = InteractionKnowledge != null;
            if (InteractionKnowledge != null)
            {
                frm.lstHistoricalFigureKnowledge.Items.Clear();
                foreach (int curInteractionKnowledge in InteractionKnowledge)
                    frm.lstHistoricalFigureKnowledge.Items.Add(
                        CultureInfo.CurrentCulture.TextInfo.ToTitleCase(HistoricalFigure.Interactions[curInteractionKnowledge].Replace("_"," ").ToLower()));
            }

            frm.grpHistoricalFigurePets.Visible = JourneyPet != null;
            if (JourneyPet != null)
            {
                frm.lstHistoricalFigurePets.Items.Clear();
                foreach (int curJourneyPet in JourneyPet)
                    frm.lstHistoricalFigurePets.Items.Add(
                        CultureInfo.CurrentCulture.TextInfo.ToTitleCase(
                         CultureInfo.CurrentCulture.TextInfo.ToTitleCase(HistoricalFigure.JourneyPets[curJourneyPet].Replace("_", " ").ToLower())));
            }

            frm.grpHistoricalFigureSkills.Visible = HFSkills != null;
            if (HFSkills != null)
            {
                frm.lstHistoricalFigureSkills.Items.Clear();
                foreach (HFSkill curHFSkill in HFSkills.OrderByDescending(x => x.TotalIP))
                    frm.lstHistoricalFigureSkills.Items.Add(
                        CultureInfo.CurrentCulture.TextInfo.ToTitleCase(HFSkill.Skills[curHFSkill.Skill].Replace("_"," ").ToLower()) + 
                        " - " + IPToTitle(curHFSkill.TotalIP));
            }


            frm.grpHistoricalFigureEntityLinks.Visible = EntityLinks != null;
            frm.trvHistoricalFigureEntityLinks.BeginUpdate();
            frm.trvHistoricalFigureEntityLinks.Nodes.Clear();
            if (EntityLinks != null)
            {
                foreach (var elList in EntityLinks)
                {
                    TreeNode thisNode = new TreeNode(CultureInfo.CurrentCulture.TextInfo.ToTitleCase(elList.Key));
                    foreach (EntityLink el in elList.Value)
                    {
                        TreeNode eNode;
                        string linkStrength = el.LinkStrength.HasValue ? " (" + el.LinkStrength + "%)" : "";
                        if (el.Entity == null)
                            eNode = new TreeNode("Entity: " + el.EntityID + linkStrength);
                        else
                            eNode = new TreeNode(el.Entity.ToString() + " - " + el.Entity.Type + linkStrength);
                        eNode.Tag = el.Entity;
                        thisNode.Nodes.Add(eNode);
                    }
                    frm.trvHistoricalFigureEntityLinks.Nodes.Add(thisNode);
                }

                frm.trvHistoricalFigureEntityLinks.ExpandAll();
            }
            frm.trvHistoricalFigureEntityLinks.EndUpdate();

            frm.grpHistoricalFigureHFLinks.Visible =
                Spouses != null || Lovers != null || Followers != null ||
                Deities != null || Masters != null || Apprentices != null || Prisoners != null;
            frm.trvHistoricalFigureHFLinks.BeginUpdate();
            frm.trvHistoricalFigureHFLinks.Nodes.Clear();
            if (frm.grpHistoricalFigureHFLinks.Visible)
            {
                if (Spouses != null)
                {
                    TreeNode thisNode = new TreeNode("Spouses");
                    foreach (HistoricalFigure HF in Spouses)
                    {
                        TreeNode hfNode = new TreeNode(HF.ToString());
                        hfNode.Tag = HF;
                        if (HF.Caste.HasValue && HistoricalFigure.Castes[HF.Caste.Value].ToLower().StartsWith("male"))
                            hfNode.ForeColor = Color.Blue;
                        else if (HF.Caste.HasValue && HistoricalFigure.Castes[HF.Caste.Value].ToLower().StartsWith("female"))
                            hfNode.ForeColor = Color.Red;

                        thisNode.Nodes.Add(hfNode);
                    }
                    thisNode.Text = thisNode.Text + " (" + thisNode.Nodes.Count + ")";
                    frm.trvHistoricalFigureHFLinks.Nodes.Add(thisNode);
                }
                if (Lovers != null)
                {
                    TreeNode thisNode = new TreeNode("Lovers");
                    foreach (HistoricalFigure HF in Lovers)
                    {
                        TreeNode hfNode = new TreeNode(HF.ToString());
                        hfNode.Tag = HF;
                        if (HF.Caste.HasValue && HistoricalFigure.Castes[HF.Caste.Value].ToLower().StartsWith("male"))
                            hfNode.ForeColor = Color.Blue;
                        else if (HF.Caste.HasValue && HistoricalFigure.Castes[HF.Caste.Value].ToLower().StartsWith("female"))
                            hfNode.ForeColor = Color.Red;
                        thisNode.Nodes.Add(hfNode);
                    }
                    thisNode.Text = thisNode.Text + " (" + thisNode.Nodes.Count + ")";
                    frm.trvHistoricalFigureHFLinks.Nodes.Add(thisNode);
                }
                if (Followers != null)
                {
                    TreeNode thisNode = new TreeNode("Followers");
                    foreach (HistoricalFigure HF in Followers)
                    {
                        TreeNode hfNode = new TreeNode(HF.ToString());
                        hfNode.Tag = HF;
                        if (HF.Caste.HasValue && HistoricalFigure.Castes[HF.Caste.Value].ToLower().StartsWith("male"))
                            hfNode.ForeColor = Color.Blue;
                        else if (HF.Caste.HasValue && HistoricalFigure.Castes[HF.Caste.Value].ToLower().StartsWith("female"))
                            hfNode.ForeColor = Color.Red;
                        thisNode.Nodes.Add(hfNode);
                    }
                    thisNode.Text = thisNode.Text + " (" + thisNode.Nodes.Count + ")";
                    frm.trvHistoricalFigureHFLinks.Nodes.Add(thisNode);
                }
                if (Deities != null)
                {
                    TreeNode thisNode = new TreeNode("Deities");

                    foreach (HFLink hflink in HFLinks["deity"])
	                {

                        TreeNode hfNode = new TreeNode(hflink.HF.ToString() + " (" + hflink.LinkStrength + "%)");
                        hfNode.Tag = hflink.HF;
                        if (hflink.HF.Caste.HasValue && HistoricalFigure.Castes[hflink.HF.Caste.Value].ToLower().StartsWith("male"))
                            hfNode.ForeColor = Color.Blue;
                        else if (hflink.HF.Caste.HasValue && HistoricalFigure.Castes[hflink.HF.Caste.Value].ToLower().StartsWith("female"))
                            hfNode.ForeColor = Color.Red;
                        thisNode.Nodes.Add(hfNode);
	                }

                    thisNode.Text = thisNode.Text + " (" + thisNode.Nodes.Count + ")";
                    frm.trvHistoricalFigureHFLinks.Nodes.Add(thisNode);
                }
                if (Masters != null)
                {
                    TreeNode thisNode = new TreeNode("Masters");
                    foreach (HistoricalFigure HF in Masters)
                    {
                        TreeNode hfNode = new TreeNode(HF.ToString());
                        hfNode.Tag = HF;
                        if (HF.Caste.HasValue && HistoricalFigure.Castes[HF.Caste.Value].ToLower().StartsWith("male"))
                            hfNode.ForeColor = Color.Blue;
                        else if (HF.Caste.HasValue && HistoricalFigure.Castes[HF.Caste.Value].ToLower().StartsWith("female"))
                            hfNode.ForeColor = Color.Red;
                        thisNode.Nodes.Add(hfNode);
                    }
                    thisNode.Text = thisNode.Text + " (" + thisNode.Nodes.Count + ")";
                    frm.trvHistoricalFigureHFLinks.Nodes.Add(thisNode);
                }
                if (Apprentices != null)
                {
                    TreeNode thisNode = new TreeNode("Apprentices");
                    foreach (HistoricalFigure HF in Apprentices)
                    {
                        TreeNode hfNode = new TreeNode(HF.ToString());
                        hfNode.Tag = HF;
                        if (HF.Caste.HasValue && HistoricalFigure.Castes[HF.Caste.Value].ToLower().StartsWith("male"))
                            hfNode.ForeColor = Color.Blue;
                        else if (HF.Caste.HasValue && HistoricalFigure.Castes[HF.Caste.Value].ToLower().StartsWith("female"))
                            hfNode.ForeColor = Color.Red;
                        thisNode.Nodes.Add(hfNode);
                    }
                    thisNode.Text = thisNode.Text + " (" + thisNode.Nodes.Count + ")";
                    frm.trvHistoricalFigureHFLinks.Nodes.Add(thisNode);
                }
                if (Prisoners != null)
                {
                    TreeNode thisNode = new TreeNode("Prisoners");
                    foreach (HistoricalFigure HF in Prisoners)
                    {
                        TreeNode hfNode = new TreeNode(HF.ToString());
                        hfNode.Tag = HF;
                        if (HF.Caste.HasValue && HistoricalFigure.Castes[HF.Caste.Value].ToLower().StartsWith("male"))
                            hfNode.ForeColor = Color.Blue;
                        else if (HF.Caste.HasValue && HistoricalFigure.Castes[HF.Caste.Value].ToLower().StartsWith("female"))
                            hfNode.ForeColor = Color.Red;
                        thisNode.Nodes.Add(hfNode);
                    }
                    thisNode.Text = thisNode.Text + " (" + thisNode.Nodes.Count + ")";
                    frm.trvHistoricalFigureHFLinks.Nodes.Add(thisNode);
                }
                if (frm.trvHistoricalFigureHFLinks.GetNodeCount(true) <= 5000)
                    frm.trvHistoricalFigureHFLinks.ExpandAll();
            }
            


            if (SlayingEvents != null)
                frm.grpHistoricalFigureHFLinks.Show();
            if (SlayingEvents != null)
            {
                TreeNode thisNode = new TreeNode("Kills");
                foreach (HE_HFDied evt in SlayingEvents)
                {
                    if (evt.SlayerHF == this && evt.HF != null)
                    {

                        TreeNode hfNode = new TreeNode(evt.HF.ToString());
                        hfNode.Tag = evt.HF;
                        if (evt.HF.Caste.HasValue && HistoricalFigure.Castes[evt.HF.Caste.Value].ToLower().StartsWith("male"))
                            hfNode.ForeColor = Color.Blue;
                        else if (evt.HF.Caste.HasValue && HistoricalFigure.Castes[evt.HF.Caste.Value].ToLower().StartsWith("female"))
                            hfNode.ForeColor = Color.Red;

                        thisNode.Nodes.Add(hfNode);
                    }
                }
                if (thisNode.Nodes.Count > 0)
                {
                    thisNode.Text = thisNode.Text + " (" + thisNode.Nodes.Count + ")";
                    frm.trvHistoricalFigureHFLinks.Nodes.Add(thisNode);
                    frm.trvHistoricalFigureHFLinks.ExpandAll();
                }
            }
            frm.trvHistoricalFigureHFLinks.EndUpdate();

            frm.lstHistoricalFigureEvents.BeginUpdate();
            frm.lstHistoricalFigureEvents.Items.Clear();

            if (Events != null)
            {
                foreach (var evt in Events)
                    frm.lstHistoricalFigureEvents.Items.Add(evt);
            }
            frm.lstHistoricalFigureEvents.EndUpdate();

            if (frm.lstHistoricalFigureEvents.Items.Count > 0)
            {
                frm.grpHistoricalFigureEvents.Show();
                frm.lstHistoricalFigureEvents.SelectedIndex = 0;
            }
            else
                frm.grpHistoricalFigureEvents.Hide();



            frm.grpHistoricalFigureDeath.Visible = DiedEvent != null;
            if (DiedEvent != null)
            {
                frm.lblHistoricalFigureDeathSlayer.Data = DiedEvent.SlayerHF;
                frm.lblHistoricalFigureDeathLocation.Data = DiedEvent.Site == null ? (WorldObject)DiedEvent.Subregion : (WorldObject)DiedEvent.Site;
                frm.lblHistoricalFigureDeathCause.Text = HE_HFDied.Causes[DiedEvent.Cause];
                frm.lblHistoricalFigureDeathTime.Data = DiedEvent;
                frm.lblHistoricalFigureDeathTime.Text = DiedEvent.Time.ToString();
            }


            frm.grpHistoricalFigureAncestors.Visible = (Mother != null || Father != null);
            Ancestors = new List<HistoricalFigure>();
            frm.trvHistoricalFigureAncestors.BeginUpdate();
            frm.trvHistoricalFigureAncestors.Nodes.Clear();
            bool hitMax = false;
            if (Mother != null)
            {
                TreeNode motherNode;
                if (Mother.Dead)
                    motherNode = new TreeNode(Mother.ToString() + " (" + Mother.Birth.ToString() + " - " + Mother.Death.ToString() + ")");
                else
                    motherNode = new TreeNode(Mother.ToString() + " (" + Mother.Birth.ToString() + " - )");

                motherNode.ForeColor = Color.Red;
                motherNode.Tag = Mother;
                frm.trvHistoricalFigureAncestors.Nodes.Add(motherNode);
                Ancestors.Add(Mother);
                hitMax = AddToAncestorsTree(motherNode, 1, 10);
            }
            if (Father != null)
            {
                TreeNode fatherNode;
                if (Father.Dead)
                    fatherNode = new TreeNode(Father.ToString() + " (" + Father.Birth.ToString() + " - " + Father.Death.ToString() + ")");
                else
                    fatherNode = new TreeNode(Father.ToString() + " (" + Father.Birth.ToString() + " - )");

                fatherNode.ForeColor = Color.Blue;
                fatherNode.Tag = Father;
                frm.trvHistoricalFigureAncestors.Nodes.Add(fatherNode);
                Ancestors.Add(Father);
                hitMax = AddToAncestorsTree(fatherNode, 1, 10);
            }
            frm.trvHistoricalFigureAncestors.EndUpdate();
            frm.grpHistoricalFigureAncestors.Text = "Ancestors (" + Ancestors.Count + (hitMax ? "+" :"") + ")";
            Ancestors = null;

            frm.grpHistoricalFigureDescendents.Visible = (Children != null);
            Descendents = new List<HistoricalFigure>();
            hitMax = false;
            frm.trvHistoricalFigureDescendents.BeginUpdate();
            if (Children != null)
            {
                frm.trvHistoricalFigureDescendents.Nodes.Clear();

                foreach (HistoricalFigure child in Children)
                {
                    TreeNode childNode;
                    if (child.Dead)
                        childNode = new TreeNode(child.ToString() + " (" + child.Birth.ToString() + " - " + child.Death.ToString() + ")");
                    else
                        childNode = new TreeNode(child.ToString() + " (" + child.Birth.ToString() + " - )");

                    childNode.Tag = child;
                    frm.trvHistoricalFigureDescendents.Nodes.Add(childNode);
                    Descendents.Add(child);
                    if (AddToDescendentsTree(childNode, 1, 8))
                        hitMax = true;
                }
            }
            frm.trvHistoricalFigureDescendents.EndUpdate();
            if (DescendentCount == 0)
                frm.grpHistoricalFigureDescendents.Text = "Descendents (" + Descendents.Count + (hitMax ? "+" : "") + ")";
            else
            { 
                if (DescendentCount > 50000)
                    frm.grpHistoricalFigureDescendents.Text = "Descendents (" + DescendentCount + "+ - " + DescendentGenerations + "+ Generations)";
                else
                    frm.grpHistoricalFigureDescendents.Text = "Descendents (" + DescendentCount + " - " + DescendentGenerations + " Generations)";
            }
            Descendents = null;

            Program.MakeSelected(frm.tabHistoricalFigure, frm.lstHistoricalFigure, this);
        }

        private string IPToTitle(int IP)
        {
            if (IP < 500)
                return "Dabbling";
            else if (IP < 1100)
                return "Novice";
            else if (IP < 1800)
                return "Adequate";
            else if (IP < 2600)
                return "Competent";
            else if (IP < 3500)
                return "Skilled";
            else if (IP < 4500)
                return "Proficient";
            else if (IP < 5600)
                return "Talented";
            else if (IP < 6800)
                return "Adept";
            else if (IP < 8100)
                return "Expert";
            else if (IP < 9500)
                return "Professional";
            else if (IP < 11000)
                return "Accomplished";
            else if (IP < 12600)
                return "Great";
            else if (IP < 14300)
                return "Master";
            else if (IP < 16100)
                return "High Master";
            else if (IP < 18000)
                return "Grand Master";
            else if (IP < 20000)
                return "Legendary";
            else if (IP < 22100)
                return "Legendeary+1";
            else if (IP < 24300)
                return "Legendary+2";
            else if (IP < 26600)
                return "Legendary+3";
            else if (IP < 2900)
                return "Legendary+4";
            else
                return "Legendary+5";

        }

        private bool AddToDescendentsTree(TreeNode parentNode, int depth, int maxDepth)
        {
            HistoricalFigure HF = (HistoricalFigure)parentNode.Tag;
            bool hitMax = false;
            if (HF.Children != null && depth <= maxDepth)
            {
                foreach (HistoricalFigure child in HF.Children)
                {
                    if (child.Mother == HF)
                        parentNode.ForeColor = Color.Red;
                    else if (child.Father == HF)
                        parentNode.ForeColor = Color.Blue;
                    TreeNode childNode;
                    if (child.Dead)
                        childNode = new TreeNode(child.ToString() + " (" + child.Birth.ToString() + " - " + child.Death.ToString() +")");
                    else
                        childNode = new TreeNode(child.ToString() + " (" + child.Birth.ToString() + " - )");
                    if (depth == maxDepth)
                        childNode.Text += "...";
                    childNode.Tag = child;
                    if (!Descendents.Contains(child))
                        Descendents.Add(child);
                    parentNode.Nodes.Add(childNode);
                    if (AddToDescendentsTree(childNode, depth + 1, maxDepth))
                        hitMax = true;
                }
            }
            else
            {
                if (HF.Caste.HasValue && HistoricalFigure.Castes[HF.Caste.Value].ToLower() == "male")
                    parentNode.ForeColor = Color.Blue;
                else if (HF.Caste.HasValue && HistoricalFigure.Castes[HF.Caste.Value].ToLower() == "female")
                    parentNode.ForeColor = Color.Red;
            }
            if (depth > maxDepth || hitMax)
                return true;
            else
                return false;
        }

        private bool AddToAncestorsTree(TreeNode childNode, int depth, int maxDepth)
        {
            HistoricalFigure HF = (HistoricalFigure)childNode.Tag;
            bool hitMax = false;
            if (HF.Mother != null && depth <= maxDepth)
            {
                TreeNode motherNode = new TreeNode(HF.Mother.ToString());
                if (depth == maxDepth)
                    motherNode.Text += "...";

                motherNode.ForeColor = Color.Red;
                motherNode.Tag = HF.Mother;
                if (!Ancestors.Contains(HF.Mother))
                    Ancestors.Add(HF.Mother);
                childNode.Nodes.Add(motherNode);
                hitMax = AddToAncestorsTree(motherNode, depth + 1, maxDepth);
            }
            else if (depth > maxDepth)
                hitMax = true;
            if (HF.Father != null && depth <= maxDepth)
            {
                TreeNode fatherNode = new TreeNode(HF.Father.ToString());
                if (depth == maxDepth)
                    fatherNode.Text += "...";
                fatherNode.ForeColor = Color.Blue;
                fatherNode.Tag = HF.Father;
                if (!Ancestors.Contains(HF.Father))
                    Ancestors.Add(HF.Father);
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
            if (BirthYear.HasValue)
                Birth = new WorldTime(BirthYear.Value, BirthSeconds);
            else
                Birth = WorldTime.Present;
            if (DeathYear.HasValue)
                Death = new WorldTime(DeathYear.Value, DeathSeconds);
            else
                Death = WorldTime.Present;
            Appeared = new WorldTime(AppearedYear.Value);

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
            if (Leader != null && Leader.InheritanceID != null &&
                 World.HistoricalFigures.ContainsKey(Leader.InheritanceID.Value))
            {
                Leader.InheritedFrom = World.HistoricalFigures[Leader.InheritanceID.Value];
            }

            if (EntityFormerPositionLinks != null)
            {
                foreach (var entityformerpositionlink in EntityFormerPositionLinks)
                {
                    if (World.Entities.ContainsKey(entityformerpositionlink.EntityID))
                        entityformerpositionlink.Entity = World.Entities[entityformerpositionlink.EntityID];
                }
            }
            if (EntityPositionLinks != null)
            {
                foreach (var entitypositionlink in EntityPositionLinks)
                {
                    if (World.Entities.ContainsKey(entitypositionlink.EntityID))
                        entitypositionlink.Entity = World.Entities[entitypositionlink.EntityID];
                }
            }
            if (EntityReputations != null)
            {
                foreach (var entityreputation in EntityReputations)
                {
                    if (World.Entities.ContainsKey(entityreputation.EntityID))
                        entityreputation.Entity = World.Entities[entityreputation.EntityID];
                }
            }
            if (EntityLinks != null)
            {
                foreach (var list in EntityLinks.Values)
                {
                    foreach (var entitylink in list)
                    {
                        if (World.Entities.ContainsKey(entitylink.EntityID))
                            entitylink.Entity = World.Entities[entitylink.EntityID];
                    }
                }
            }
            if (SiteLinks != null)
            {
                foreach (var list in SiteLinks.Values)
                {
                    foreach (var sitelink in list)
                    {
                        if (World.Sites.ContainsKey(sitelink.SiteID))
                            sitelink.Site = World.Sites[sitelink.SiteID];
                    }
                }
            }
            if (HFLinks != null)
            {
                foreach (var list in HFLinks.Values)
                {
                    foreach (var hflink in list)
                    {
                        if (World.HistoricalFigures.ContainsKey(hflink.LinkedHFID))
                            hflink.HF = World.HistoricalFigures[hflink.LinkedHFID];
                    }
                }
            }

        }

        internal override void Process()
        {
            if (SiteLinks != null)
            {
                foreach (var linklist in SiteLinks)
                {
                    foreach (SiteLink sl in linklist.Value)
                    {
                        switch (linklist.Key)
                        {
                            case "lair":
                            case "home structure":
                            case "seat of power":
                            case "home site building":
                            case "home site underground":
                                if (sl.Site.Inhabitants == null)
                                    sl.Site.Inhabitants = new List<HistoricalFigure>();
                                sl.Site.Inhabitants.Add(this);
                                Site = sl.Site;

                                if (sl.EntityID.HasValue && World.Entities.ContainsKey(sl.EntityID.Value))
                                    sl.Entity = World.Entities[sl.EntityID.Value];

                                break;
                            default:
                                Program.Log(LogType.Warning, "Unknown HF Site Link: " + linklist.Key);
                                break;
                        }
                    }

                }
            }
            if (EntityLinks != null)
            {
                foreach (var linklist in EntityLinks)
                {
                    foreach (EntityLink el in linklist.Value)
                    {
                        switch (linklist.Key)
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
            if (HFLinks != null)
            {
                foreach (var linklist in HFLinks)
                {
                    foreach (HFLink hfl in linklist.Value)
                    {
                        if (hfl.HF == null)
                            continue;
                        switch (linklist.Key)
                        {

                            case "child":
                                if (Children == null)
                                    Children = new List<HistoricalFigure>();
                                if (!Children.Contains(hfl.HF))
                                {
                                    Children.Add(hfl.HF);
                                    if (hfl.HF.Parents == null)
                                        hfl.HF.Parents = new List<HistoricalFigure>();
                                    hfl.HF.Parents.Add(this);
                                }
                                break;
                            case "spouse":
                                if (hfl.HF.Spouses == null)
                                    hfl.HF.Spouses = new List<HistoricalFigure>();
                                if (!hfl.HF.Spouses.Contains(this))
                                {
                                    hfl.HF.Spouses.Add(this);
                                    if (Spouses == null)
                                        Spouses = new List<HistoricalFigure>();
                                    Spouses.Add(hfl.HF);
                                }
                                break;
                            case "lover":
                                if (hfl.HF.Lovers == null)
                                    hfl.HF.Lovers = new List<HistoricalFigure>();
                                if (!hfl.HF.Lovers.Contains(this))
                                {
                                    hfl.HF.Lovers.Add(this);
                                    if (Lovers == null)
                                        Lovers = new List<HistoricalFigure>();
                                    Lovers.Add(hfl.HF);
                                }
                                break;
                            case "deity":
                                if (hfl.HF.Followers == null)
                                    hfl.HF.Followers = new List<HistoricalFigure>();
                                hfl.HF.Followers.Add(this);
                                if (Deities == null)
                                    Deities = new List<HistoricalFigure>();
                                Deities.Add(hfl.HF);
                                break;
                            case "mother":
                                Mother = hfl.HF;
                                if (hfl.HF.Children == null)
                                    hfl.HF.Children = new List<HistoricalFigure>();
                                if (!hfl.HF.Children.Contains(this))
                                    hfl.HF.Children.Add(this);
                                else
                                {
                                    if (Parents == null)
                                        Parents = new List<HistoricalFigure>();
                                    if (Parents.Contains(hfl.HF))
                                        Parents.Remove(Mother);
                                }
                                break;
                            case "father":
                                Father = hfl.HF;
                                if (hfl.HF.Children == null)
                                    hfl.HF.Children = new List<HistoricalFigure>();
                                if (!hfl.HF.Children.Contains(this))
                                    hfl.HF.Children.Add(this);
                                else
                                {
                                    if (Parents == null)
                                        Parents = new List<HistoricalFigure>();
                                    if (Parents.Contains(hfl.HF))
                                        Parents.Remove(Father);
                                }
                                break;
                            case "master":
                                if (hfl.HF.Apprentices == null)
                                    hfl.HF.Apprentices = new List<HistoricalFigure>();
                                if (!hfl.HF.Apprentices.Contains(this))
                                {
                                    hfl.HF.Apprentices.Add(this);
                                    if (Masters == null)
                                        Masters = new List<HistoricalFigure>();
                                    Masters.Add(hfl.HF);
                                }
                                break;
                            case "apprentice":
                                if (hfl.HF.Masters == null)
                                    hfl.HF.Masters = new List<HistoricalFigure>();
                                if (!hfl.HF.Masters.Contains(this))
                                {
                                    hfl.HF.Masters.Add(this);
                                    if (Apprentices == null)
                                        Apprentices = new List<HistoricalFigure>();
                                    Apprentices.Add(hfl.HF);
                                }
                                break;
                            case "prisoner":
                                if (hfl.HF.Prisoners == null)
                                    hfl.HF.Prisoners = new List<HistoricalFigure>();
                                if (!hfl.HF.Prisoners.Contains(this))
                                {
                                    hfl.HF.Prisoners.Add(this);
                                }
                                break;
                            default:
                                Program.Log(LogType.Warning, "Unknown HF HF Link: " + linklist.Key);
                                break;
                        }
                    }
                }
            }

            if (isLeader && Leader.InheritanceID == -1)
            {
                switch (Leader.InheritedFromName)
                {
                    case "paternal grandmother":
                        //InheritanceID = -1;
                        Leader.InheritedFrom = Father.Mother;
                        break;
                    case "maternal grandmother":
                        Leader.InheritedFrom = Mother.Mother;
                        //InheritanceID = -1;
                        break;
                    case "paternal grandfather":
                        Leader.InheritedFrom = Father.Father;
                        //InheritanceID = -1;
                        break;
                    case "maternal grandfather":
                        Leader.InheritedFrom = Mother.Father;
                        //InheritanceID
                        break;
                }
            }
        }

        public override string ToString()
        {
            if (base.ToString() != null)
                return base.ToString();
            return ID.ToString();
        }

        #region Count Families
        internal void CountDescendents()
        {
            List<HistoricalFigure> CurrentDescendents = new List<HistoricalFigure>() ;

            int generation = 1;
            int generationmax = 1;

            if (Children != null)
            {
                foreach (HistoricalFigure hf in Children)
                {
                    if (!CurrentDescendents.Contains(hf))
                    {
                        CurrentDescendents.Add(hf);
                        hf.CountDescendents(CurrentDescendents, generation, ref generationmax);
                    }
                }
            }

            DescendentCount = CurrentDescendents.Count;
            DescendentGenerations = generationmax;
            CurrentDescendents.Clear();
        }

        private void CountDescendents(List<HistoricalFigure> CurrentDescendents, int gen, ref int genmax)
        {
            if (Children != null & CurrentDescendents.Count <= 50000)
            {
                gen++;
                if (gen > genmax)
                    genmax = gen;
                foreach (HistoricalFigure hf in Children)
                {
                    if (!CurrentDescendents.Contains(hf))
                    {
                        CurrentDescendents.Add(hf);
                        hf.CountDescendents(CurrentDescendents, gen, ref genmax);
                    }
                }
            }
        }

        internal void CountAncestors()
        {
            List<HistoricalFigure> CurrentAncestors = new List<HistoricalFigure>();

            if (Mother != null && !CurrentAncestors.Contains(Mother))
            {
                CurrentAncestors.Add(Mother);
                Mother.CountAncestors(CurrentAncestors);
            }
            if (Father != null && !CurrentAncestors.Contains(Father))
            {
                CurrentAncestors.Add(Father);
                Father.CountAncestors(CurrentAncestors);
            }
            
            AncestorCount = CurrentAncestors.Count;
            CurrentAncestors.Clear();
        }

        private void CountAncestors(List<HistoricalFigure> CurrentAncestors)
        {
            if (Mother != null && !CurrentAncestors.Contains(Mother))
            {
                CurrentAncestors.Add(Mother);
                Mother.CountAncestors(CurrentAncestors);
            }
            if (Father != null && !CurrentAncestors.Contains(Father))
            {
                CurrentAncestors.Add(Father);
                Father.CountAncestors(CurrentAncestors);
            }
        }



        #endregion 
    
        internal override void Export(string table)
        {

            List<object> vals = new List<object>();

            vals.Add(ID);

            if (Name == null)
                vals.Add(DBNull.Value);
            else
                vals.Add(Name.Replace("'", "''"));

            if (Race == null)
                vals.Add(DBNull.Value);
            else
                vals.Add(Race.Name.Replace("'", "''"));

            if (!Caste.HasValue)
                vals.Add(DBNull.Value);
            else
                vals.Add(Castes[Caste.Value].Replace("'", "''"));
            vals.Add(Appeared.Year);
            vals.Add(Birth.Year);
            vals.Add(Birth.TotalSeconds);
            if (Death == WorldTime.Present)
            {
                vals.Add(DBNull.Value);
                vals.Add(DBNull.Value);
            }
            else
            { 
                vals.Add(Death.Year);
                vals.Add(Death.TotalSeconds);
            }

            if (!AssociatedType.HasValue)
                vals.Add(DBNull.Value);
            else
                vals.Add(AssociatedTypes[AssociatedType.Value].Replace("'", "''"));

            vals.Add(Deity);
            vals.Add(Force);
            vals.Add(Animated);
            vals.Add(Ghost );
            vals.Add(Adventurer);

            if (EntPop == null)
                vals.Add(DBNull.Value);
            else
                vals.Add(EntPop.ID);

            if (!Goal.HasValue)
                vals.Add(DBNull.Value);
            else
                vals.Add(Goals[Goal.Value]);

            if (Sphere == null)
                vals.Add(DBNull.Value);
            else
            {
                string exportText = "";
                foreach (int curSphere in Sphere)
                    exportText += Spheres[curSphere] + ",";
                exportText = exportText.TrimEnd(',');
                vals.Add(exportText);
            }

            if (InteractionKnowledge == null)
                vals.Add(DBNull.Value);
            else
            {
                string exportText = "";
                foreach (int curInteractionKnowledge in InteractionKnowledge)
                    exportText += Interactions[curInteractionKnowledge] + ",";
                exportText = exportText.TrimEnd(',');
                vals.Add(exportText);
            }

            if (JourneyPet == null)
                vals.Add(DBNull.Value);
            else
            {
                string exportText = "";
                foreach (int curJourneyPet in JourneyPet)
                    exportText += JourneyPets[curJourneyPet] + ",";
                exportText = exportText.TrimEnd(',');
                vals.Add(exportText);
            }

                
            Database.ExportWorldItem(table, vals);

            if (EntityFormerPositionLinks != null)
            {
                foreach (var EntityFormerPositionLink in EntityFormerPositionLinks)
                    EntityFormerPositionLink.Export(ID);
            }

            if (EntityPositionLinks != null)
            {
                foreach (var EntityPositionLink in EntityPositionLinks)
                    EntityPositionLink.Export(ID);
            }

            if (EntityReputations != null)
            {
            foreach (var EntityReputation in EntityReputations)
                EntityReputation.Export(ID);
            }

            if (HFSkills != null)
            {
            foreach (var HFSkill in HFSkills)
                HFSkill.Export(ID);
            }

            if (EntityLinks != null)
            {
            foreach (var EntityLinkList in EntityLinks.Values)
            {
                foreach (var EntityLink in EntityLinkList)
                    EntityLink.Export(ID);
            }
            }

            if (HFLinks != null)
            {
                foreach (var HFLinkList in HFLinks.Values)
                {
                    foreach (var HFLink in HFLinkList)
                        HFLink.Export(ID);
                }
            }

            if (SiteLinks != null)
            {
                foreach (var SiteLinkList in SiteLinks.Values)
                {
                    foreach (var SiteLink in SiteLinkList)
                        SiteLink.Export(ID);
                }            
            }



        }
    }

}