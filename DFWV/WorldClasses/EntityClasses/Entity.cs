﻿
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;
using DFWV.Annotations;
using DFWV.WorldClasses.HistoricalEventClasses;
using DFWV.WorldClasses.HistoricalEventCollectionClasses;
using DFWV.WorldClasses.HistoricalFigureClasses;
using System.Collections.Concurrent;

namespace DFWV.WorldClasses.EntityClasses
{
    public class Entity : XMLObject
    {

        public Race Race { get; set; }
        [UsedImplicitly]
        public string RaceName => Race != null ? Race.Name : "";

        public Civilization Civilization { get; set; }
        public Civilization ParentCiv { get; set; }
        public bool EntityFileMerged { get; private set; }

        private List<int> ChildrenIDs { get; set; }
        private ConcurrentBag<Entity> Children;
        public ConcurrentBag<HistoricalFigure> Enemies { get; set; }
        public ConcurrentBag<HistoricalFigure> Members { get; set; }
        public List<int> MemberHfids { get; set; }
        private ConcurrentBag<HistoricalFigure> _memberHfs { get; set; }
        public ConcurrentBag<HistoricalFigure> MemberHfs
        {
            get
            {
                if (_memberHfs == null)
                {
                    _memberHfs = new ConcurrentBag<HistoricalFigure>();
                    MemberHfids.Where(id => World.HistoricalFigures.ContainsKey(Id))
                                .Select(id => World.HistoricalFigures[id])
                                .ToList().ForEach(x => _memberHfs.Add(x));
                }
                return _memberHfs;
            }
        }



        public ConcurrentBag<HistoricalFigure> FormerMembers { get; set; }
        public ConcurrentBag<HistoricalFigure> Prisoners { get; set; }
        public ConcurrentBag<HistoricalFigure> FormerPrisoners { get; set; }
        public ConcurrentBag<HistoricalFigure> Criminals { get; set; }
        public ConcurrentBag<HistoricalFigure> Slaves { get; set; }
        public ConcurrentBag<HistoricalFigure> FormerSlaves { get; set; }
        public ConcurrentBag<HistoricalFigure> Heroes { get; set; }

        public List<WorldConstruction> ConstructionsBuilt { get; set; }

        private HE_SiteTakenOver SiteTakeoverEvent { get; set; }
        public HE_EntityCreated CreatedEvent { private get; set; }

        [UsedImplicitly]
        public List<HE_EntityLaw> LawEvents { get; set; }

        [UsedImplicitly]
        public string DispNameLower => ToString().ToLower();

        public ConcurrentBag<EC_BeastAttack> BeastAttackEventCollections { get; set; }
        public ConcurrentBag<EC_War> WarEventCollections { get; set; }
        public ConcurrentBag<EC_Abduction> AbductionEventCollections { get; set; }
        public ConcurrentBag<EC_SiteConquered> SiteConqueredEventCollections { get; set; }
        public ConcurrentBag<EC_Theft> TheftEventCollections { get; set; }
        public ConcurrentBag<EC_Insurrection> InsurrectionEventCollections { get; set; }
        public ConcurrentBag<EC_Occasion> OccasionEventCollections { get; set; }

        public List<Point> Coords { get; set; }

        [UsedImplicitly]
        public bool IsPlayerControlled { get; set; }

        public List<HistoricalEvent> CachedEvents;

        public IEnumerable<HistoricalEvent> Events
        {
            get
            {
                return CachedEvents ??
                       (CachedEvents = World.HistoricalEvents.Values.Where(x => x.EntitiesInvolved.Contains(this)).ToList());
            }
        }

        [UsedImplicitly]
        public int EventCount { get; set; }

        override public Point Location 
        { 
            get
            {
                if (ParentCiv == null && Civilization?.FirstSite != null)
                    return Civilization.FirstSite.Location;
                if (ParentCiv == null && Civilization != null && Civilization.FirstSite == null)
                    return Point.Empty;
                if (ParentCiv == null && Civilization == null && CreatedEvent != null)
                    return CreatedEvent.Location;
                if (ParentCiv?.FirstSite != null)
                    return ParentCiv.FirstSite.Location;
                if (ParentCiv == null && Civilization == null)
                    return Point.Empty;
                return Point.Empty;
            }
        }

        [UsedImplicitly]
        public int MemberCount => Members?.Count ?? 0;


        static public List<string> Types = new List<string>();
        private short _entityType = -1;
        public string Type
        {
            get
            {
                if (_entityType > -1 && Types.Count > 0 && Types.Count > _entityType)
                {
                    switch (Types[_entityType])
                    {
                        case "sitegovernment":
                            return "Site Government";
                        case "migratinggroup":
                            return "Migrating Group";
                        case "nomadicgroup":
                            return "Nomadic Group";
                        //case "civilization":
                        //case "outcast":
                        //case "religion":
                        default:
                            return Types[_entityType].ToTitleCase();
                    }
                }

                if (Civilization != null)
                    return "Civilization";
                if (CreatedEvent != null)
                    return "Religion";
                return ParentCiv != null ? "Group" : "Other";
            }
        }

        public Dictionary<int, List<EntityEntityLink>> EntityLinks { get; set; }
        public Dictionary<int, List<EntitySiteLink>> SiteLinks { get; set; }
        public List<Point> Claims { get; set; }
        public List<EntityPosition> Positions { get; set; }
        public List<EntityPositionAssignment> PositionAssignments { get; set; }
        public List<EntityOccasion> Occasions { get; set; }

        public int? WorshipHfid { get; set; }
        public HistoricalFigure WorshipHf { get; set; }


        #region Parse from Sites Files
        public Entity(string name, World world) : base(world)
        {
            Name = name;
        }
        #endregion

        [UsedImplicitly]
        public Entity(XDocument xdoc, World world)
            : base(xdoc, world)
        {
 
            foreach (var element in xdoc.Root.Elements())
            {
                var val = element.Value;
                switch (element.Name.LocalName)
                {
                    case "id":
                        break;
                    case "name":
                        Name = val;
                        break;
                    case "type":
                    case "race":
                        break;

                    default:
                        DFXMLParser.UnexpectedXmlElement(xdoc.Root.Name.LocalName, element, xdoc.Root.ToString());
                        break;
                }
            }
        }

        public override void Select(MainForm frm)
        {
            if (frm.grpEntity.Text == ToString() && frm.MainTab.SelectedTab == frm.tabEntity)
                return;
            Program.MakeSelected(frm.tabEntity, frm.lstEntity, this);

            frm.grpEntity.Text = ToString();
            if (IsPlayerControlled)
                frm.grpEntity.Text += @" (PLAYER CONTROLLED)";
#if DEBUG
            frm.grpEntity.Text += $" - ID: {Id}";
#endif
            frm.grpEntity.Show();

            frm.lblEntityName.Text = ToString();
            frm.lblEntityType.Text = Type;
            frm.lblEntityRace.Data = Race;
            frm.lblEntityCivilization.Data = Civilization;
            frm.lblEntityParentCiv.Data = ParentCiv;
            frm.lblEntityWorshippingHF.Data = WorshipHf;


            frm.trvEntityRelatedFigures.BeginUpdate();
            frm.trvEntityRelatedFigures.Nodes.Clear();
            var hasHfLinks = Enemies != null || Members != null || FormerMembers != null ||
                Prisoners != null || FormerPrisoners != null || Criminals != null ||
                Slaves != null || FormerSlaves != null || Heroes != null;
            frm.grpEntityRelatedFigures.Visible = hasHfLinks;
            if (hasHfLinks)
            {
                var intersectedMembers = Members.Intersect(MemberHfs).ToList();
                var intersectedMembersBag = new ConcurrentBag<HistoricalFigure>();
                intersectedMembers.ForEach(x => intersectedMembersBag.Add(x));
                var entityHfLists = new List<ConcurrentBag<HistoricalFigure>>
                {Enemies, MemberHfs == null ? Members : intersectedMembersBag, FormerMembers, Prisoners, FormerPrisoners, Criminals,
                                Slaves, FormerSlaves, Heroes};
                var entityHfListNames = new List<string>
                {"Enemies", "Members", "Former Members", "Prisoners", "Former Prisoners", "Criminals",
                                "Slaves", "Former Slaves", "Heroes"};
                for (var i = 0; i < entityHfListNames.Count; i++)
                {
                    if (entityHfLists[i] == null) continue;
                    var thisNode = new TreeNode(entityHfListNames[i] + " (" + entityHfLists[i].Count + ")");
                    foreach (var hf in entityHfLists[i])
                    {
                        var newNode = hf.Dead ? new TreeNode(hf + " (" + hf.Birth + " - " + hf.Death + ")") : new TreeNode(hf + " (" + hf.Birth + " - )");
                        if (hf.Caste.HasValue && HistoricalFigure.Castes[hf.Caste.Value ].ToLower() == "female")
                            newNode.ForeColor = Color.Red;
                        else if (hf.Caste.HasValue && HistoricalFigure.Castes[hf.Caste.Value].ToLower() == "male")
                            newNode.ForeColor = Color.Blue;
                            
                        newNode.Tag = hf;
                        thisNode.Nodes.Add(newNode);
                    }
                    frm.trvEntityRelatedFigures.Nodes.Add(thisNode);
                }
            }
            frm.trvEntityRelatedFigures.ExpandAll();
            frm.trvEntityRelatedFigures.EndUpdate();


            frm.trvEntityRelatedEntities.BeginUpdate();
            frm.trvEntityRelatedEntities.Nodes.Clear();
            frm.grpEntityRelatedEntities.Visible = EntityLinks != null;
            if (EntityLinks != null)
            {
                foreach (var entityLinkList in EntityLinks)
                {
                    var thisNode = new TreeNode(EntityEntityLink.LinkTypes[entityLinkList.Key].ToLower().ToTitleCase() + " (" + entityLinkList.Value.Count() + ")");
                    foreach (var newNode in entityLinkList.Value.Select(entityLink => new TreeNode(entityLink.Target.ToString()) {Tag = entityLink.Target}))
                    {
                        thisNode.Nodes.Add(newNode);
                    }
                    frm.trvEntityRelatedEntities.Nodes.Add(thisNode);
                }
                frm.trvEntityRelatedEntities.ExpandAll();
            }
            frm.trvEntityRelatedEntities.EndUpdate();


            frm.trvEntityRelatedSites.BeginUpdate();
            frm.trvEntityRelatedSites.Nodes.Clear();
            frm.grpEntityRelatedSites.Visible = SiteLinks != null;
            if (SiteLinks != null)
            {
                foreach (var siteLinkList in SiteLinks)
                {
                    var thisNode = new TreeNode(EntitySiteLink.LinkTypes[siteLinkList.Key].ToLower().ToTitleCase() + " (" + siteLinkList.Value.Count() + ")");
                    foreach (var newNode in siteLinkList.Value.Select(siteLink => new TreeNode(siteLink.Site.AltName) {Tag = siteLink.Site}))
                    {
                        thisNode.Nodes.Add(newNode);
                    }
                    frm.trvEntityRelatedSites.Nodes.Add(thisNode);
                }
                frm.trvEntityRelatedSites.ExpandAll();
            }
            frm.trvEntityRelatedSites.EndUpdate();


            frm.grpEntityCreated.Visible = CreatedEvent != null;
            if (CreatedEvent != null)
            {

                frm.lblEntityCreatedSite.Data = CreatedEvent.Site;
                frm.lblEntityCreatedSite.Text = CreatedEvent.Site?.AltName;
                frm.lblEntityCreatedTime.Data = CreatedEvent;
                frm.lblEntityCreatedTime.Text = CreatedEvent.Time.ToString();
            }

            frm.grpEntitySiteTakeover.Visible = SiteTakeoverEvent != null;
            if (SiteTakeoverEvent != null)
            {
                frm.lblEntitySiteTakeoverDefenderCiv.Data = SiteTakeoverEvent.Entity_Defender;
                frm.lblEntitySiteTakeoverDefenderEntity.Data = SiteTakeoverEvent.Entity_SiteCiv;
                frm.lblEntitySiteTakeoverSite.Data = SiteTakeoverEvent.Site;
                frm.lblEntitySiteTakeoverTime.Data = SiteTakeoverEvent;
                frm.lblEntitySiteTakeoverTime.Text = SiteTakeoverEvent.Time.ToString();
            }


            frm.grpEntityPositions.FillListboxWith(frm.lstEntityPositions, Positions);
            frm.grpEntityEvents.FillListboxWith(frm.lstEntityEvents, Events);

        }

        internal override void Link()
        {
            if (ParentCiv?.Entity != null)
            {
                if (ParentCiv.Entity.Children == null)
                    ParentCiv.Entity.Children = new ConcurrentBag<Entity>();
                if (!ParentCiv.Entity.Children.Contains(this))
                    ParentCiv.Entity.Children.Add(this);
                if (ChildrenIDs != null)
                {
                    foreach (var child in ChildrenIDs)
                    {
                        if (Children == null)
                            Children = new ConcurrentBag<Entity>();
                        if (World.Entities.ContainsKey(child))
                            Children.Add(World.Entities[child]);
                    }
                }
            }

            if (EntityLinks != null && EntityLinks.ContainsKey(EntityEntityLink.LinkTypes.IndexOf("PARENT")))
            {
                if (EntityLinks[EntityEntityLink.LinkTypes.IndexOf("PARENT")].Count == 1)
                {
                    var parentEnt = EntityLinks[EntityEntityLink.LinkTypes.IndexOf("PARENT")][0].Target;
                    if (parentEnt.Civilization != null)
                        ParentCiv = parentEnt.Civilization;
                    else if (parentEnt.ParentCiv != null)
                        ParentCiv = parentEnt.ParentCiv;
                }
            }

            if (WorshipHfid.HasValue && World.HistoricalFigures.ContainsKey(WorshipHfid.Value))
                WorshipHf = World.HistoricalFigures[WorshipHfid.Value];
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
                    case "race_id":
                        if (valI != -1 && World.Races.ContainsKey(valI))
                            Race = World.Races[valI];
                        break;
                    case "type":
                        if (!Types.Contains(val))
                            Types.Add(val);
                        _entityType = (short)Types.IndexOf(val);
                        break;
                    case "site_link":
                       var newSl = new EntitySiteLink(element, this);
                        if (SiteLinks == null)
                            SiteLinks = new Dictionary<int, List<EntitySiteLink>>();
                        if (!SiteLinks.ContainsKey(newSl.LinkType))
                            SiteLinks.Add(newSl.LinkType, new List<EntitySiteLink>());
                        SiteLinks[newSl.LinkType].Add(newSl);
                        break;
                    case "entity_link":
                        var newEl = new EntityEntityLink(element, this);
                        if (EntityLinks == null)
                            EntityLinks = new Dictionary<int, List<EntityEntityLink>>();
                        if (!EntityLinks.ContainsKey(newEl.LinkType))
                            EntityLinks.Add(newEl.LinkType, new List<EntityEntityLink>());
                        EntityLinks[newEl.LinkType].Add(newEl);
                        break;
                    case "children":
                        ChildrenIDs = val.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(x => Convert.ToInt32(x)).ToList();
                        break;
                    case "worship_id":
                        WorshipHfid = valI;
                        break;
                    case "coords":
                        if (Coords == null)
                            Coords = new List<Point>();
                        foreach (var coordSplit in val.Split(new[] { '|' }, StringSplitOptions.RemoveEmptyEntries).Select(coord => coord.Split(',')).Where(coordSplit => coordSplit.Length == 2))
                        {
                            Coords.Add(new Point(Convert.ToInt32(coordSplit[0]), Convert.ToInt32(coordSplit[1])));
                        }
                        break;
                    case "claims":
                        Claims = val.Split(new[] { '|' }, StringSplitOptions.RemoveEmptyEntries)
                            .Select(coord => coord.Split(','))
                            .Where(coordSplit => coordSplit.Length == 2)
                            .Select(split => new Point(Convert.ToInt32(split[0]), Convert.ToInt32(split[1])))
                            .ToList();
                        break;
                    case "entity_position":
                        var newPosition = new EntityPosition(element, this);
                        if (Positions == null)
                            Positions = new List<EntityPosition>();
                        Positions.Add(newPosition);
                        break;
                    case "entity_position_assignment":
                        var newPositionAssignment = new EntityPositionAssignment(element, this);
                        if (PositionAssignments == null)
                            PositionAssignments = new List<EntityPositionAssignment>();
                        PositionAssignments.Add(newPositionAssignment);
                        break;
                    case "histfig_id":
                        if (MemberHfids == null)
                            MemberHfids = new List<int>();
                        MemberHfids.Add(valI);
                        break;
                    case "histfig_ids":
                        MemberHfids = val.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(x => Convert.ToInt32(x)).ToList();
                        break;
                    case "occasion":
                        var newOccasion = new EntityOccasion(element, this);
                        if (Occasions == null)
                            Occasions = new List<EntityOccasion>();
                        Occasions.Add(newOccasion);
                        break;
                    default:
                        DFXMLParser.UnexpectedXmlElement(xdoc.Root.Name.LocalName + "\t", element, xdoc.Root.ToString());
                        break;
                }
            }
        }

        public void MakePlayer()
        {
            if (IsPlayerControlled) return;
            IsPlayerControlled = true;
            if (Members != null)
            {
                foreach (var hf in Members)
                    hf.IsPlayerControlled = true;
            }
            if (FormerMembers == null) return;
            foreach (var hf in FormerMembers)
                hf.IsPlayerControlled = true;
        }

        internal void MergeInEntityFile(Entity ent)
        {
            Race = ent.Race;
            ParentCiv = ent.ParentCiv;
            ent.EntityFileMerged = true;
            foreach (var site in World.Sites.Values.Where(x => x.Owner == ent))
                    site.Owner = this;
        }

        public override string ToString()
        {
            if (Name != null || Race == null)
                return base.ToString();
            
            return Race.ToString();
        }

        internal override void Export(string table)
        {
            //TODO Update export to include entity/site links
            var vals = new List<object>
            {
                Id,
                Name.DBExport(),
                Type,
                Race.DBExport(),
                WorshipHfid.DBExport()
            };

            Database.ExportWorldItem(table, vals);

            if (EntityLinks != null)
            {
                foreach (var entityLink in EntityLinks.Values.SelectMany(entityLinkList => entityLinkList))
                    entityLink.Export(Id);
            }

            if (SiteLinks != null)
            {
                foreach (var siteLink in SiteLinks.Values.SelectMany(siteLinkList => siteLinkList))
                    siteLink.Export(Id);
            }

            if (ChildrenIDs == null) return;
            foreach (var child in ChildrenIDs)
                Database.ExportWorldItem("Entity_EntityChild", new List<object>{Id, child});
        }
    }
}
