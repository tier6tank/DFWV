
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
        private List<Entity> Children { get; set; }
        public List<HistoricalFigure> Enemies { get; set; }
        public List<HistoricalFigure> Members { get; set; }
        public List<HistoricalFigure> FormerMembers { get; set; }
        public List<HistoricalFigure> Prisoners { get; set; }
        public List<HistoricalFigure> FormerPrisoners { get; set; }
        public List<HistoricalFigure> Criminals { get; set; }
        public List<HistoricalFigure> Slaves { get; set; }
        public List<HistoricalFigure> FormerSlaves { get; set; }
        public List<HistoricalFigure> Heroes { get; set; }

        public List<WorldConstruction> ConstructionsBuilt { get; set; }

        private HE_SiteTakenOver SiteTakeoverEvent { get; set; }
        public HE_EntityCreated CreatedEvent { private get; set; }

        [UsedImplicitly]
        public List<HE_EntityLaw> LawEvents { get; set; }

        [UsedImplicitly]
        public string DispNameLower => ToString().ToLower();

        public List<EC_BeastAttack> BeastAttackEventCollections { get; set; }
        public List<EC_War> WarEventCollections { get; set; }
        public List<EC_Abduction> AbductionEventCollections { get; set; }
        public List<EC_SiteConquered> SiteConqueredEventCollections { get; set; }
        public List<EC_Theft> TheftEventCollections { get; set; }
        public List<EC_Insurrection> InsurrectionEventCollections { get; set; }
        public List<EC_Occasion> OccasionEventCollections { get; set; }

        public List<Point> Coords { get; set; }

        [UsedImplicitly]
        public bool isPlayerControlled { get; set; }

        public IEnumerable<HistoricalEvent> Events
        {
            get
            {
                return World.HistoricalEvents.Values.Where(x => x.EntitiesInvolved.Contains(this));
            }
        }

        [UsedImplicitly]
        public int EventCount { get; set; }

        override public Point Location 
        { 
            get
            {
                if (ParentCiv == null && Civilization != null && Civilization.FirstSite != null)
                    return Civilization.FirstSite.Location;
                if (ParentCiv == null && Civilization != null && Civilization.FirstSite == null)
                    return Point.Empty;
                if (ParentCiv == null && Civilization == null && CreatedEvent != null)
                    return CreatedEvent.Location;
                if (ParentCiv != null && ParentCiv.FirstSite != null)
                    return ParentCiv.FirstSite.Location;
                if (ParentCiv == null && Civilization == null)
                    return Point.Empty;
                return Point.Empty;
            }
        }

        [UsedImplicitly]
        public int MemberCount => Members == null ? 0 : Members.Count;


        static public List<string> Types = new List<string>();
        private short entityType = -1;
        public string Type
        {
            get
            {
                if (entityType > -1 && Types.Count > 0 && Types.Count > entityType)
                {
                    switch (Types[entityType])
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
                            return Types[entityType].ToTitleCase();
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

        public int? WorshipHFID { get; set; }
        public HistoricalFigure WorshipHF { get; set; }


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
                        DFXMLParser.UnexpectedXMLElement(xdoc.Root.Name.LocalName, element, xdoc.Root.ToString());
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
            if (isPlayerControlled)
                frm.grpEntity.Text += @" (PLAYER CONTROLLED)";
#if DEBUG
            frm.grpEntity.Text += $" - ID: {ID}";
#endif
            frm.grpEntity.Show();

            frm.lblEntityName.Text = ToString();
            frm.lblEntityType.Text = Type;
            frm.lblEntityRace.Data = Race;
            frm.lblEntityCivilization.Data = Civilization;
            frm.lblEntityParentCiv.Data = ParentCiv;
            frm.lblEntityWorshippingHF.Data = WorshipHF;


            frm.trvEntityRelatedFigures.BeginUpdate();
            frm.trvEntityRelatedFigures.Nodes.Clear();
            var hasHFLinks = Enemies != null || Members != null || FormerMembers != null ||
                Prisoners != null || FormerPrisoners != null || Criminals != null ||
                Slaves != null || FormerSlaves != null || Heroes != null;
            frm.grpEntityRelatedFigures.Visible = hasHFLinks;
            if (hasHFLinks)
            {
                var EntityHFLists = new List<List<HistoricalFigure>>
                {Enemies, Members, FormerMembers, Prisoners, FormerPrisoners, Criminals,
                                Slaves, FormerSlaves, Heroes};
                var EntityHFListNames = new List<string>
                {"Enemies", "Members", "Former Members", "Prisoners", "Former Prisoners", "Criminals",
                                "Slaves", "Former Slaves", "Heroes"};
                for (var i = 0; i < EntityHFListNames.Count; i++)
                {
                    if (EntityHFLists[i] == null) continue;
                    var thisNode = new TreeNode(EntityHFListNames[i] + " (" + EntityHFLists[i].Count + ")");
                    foreach (var hf in EntityHFLists[i])
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
                frm.lblEntityCreatedSite.Text = CreatedEvent.Site.AltName;
                frm.lblEntityCreatedTime.Data = CreatedEvent;
                frm.lblEntityCreatedTime.Text = CreatedEvent.Time.ToString();
            }

            frm.grpEntitySiteTakeover.Visible = SiteTakeoverEvent != null;
            if (SiteTakeoverEvent != null)
            {
                frm.lblEntitySiteTakeoverDefenderCiv.Data = SiteTakeoverEvent.DefenderCiv;
                frm.lblEntitySiteTakeoverDefenderEntity.Data = SiteTakeoverEvent.SiteCiv;
                frm.lblEntitySiteTakeoverSite.Data = SiteTakeoverEvent.Site;
                frm.lblEntitySiteTakeoverTime.Data = SiteTakeoverEvent;
                frm.lblEntitySiteTakeoverTime.Text = SiteTakeoverEvent.Time.ToString();
            }

            frm.grpEntityEvents.FillListboxWith(frm.lstEntityEvents, Events);

        }

        internal override void Link()
        {
            if (ParentCiv != null && ParentCiv.Entity != null)
            {
                if (ParentCiv.Entity.Children == null)
                    ParentCiv.Entity.Children = new List<Entity>();
                if (!ParentCiv.Entity.Children.Contains(this))
                    ParentCiv.Entity.Children.Add(this);
                if (ChildrenIDs != null)
                {
                    foreach (var child in ChildrenIDs)
                    {
                        if (Children == null)
                            Children = new List<Entity>();
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

            if (WorshipHFID.HasValue && World.HistoricalFigures.ContainsKey(WorshipHFID.Value))
                WorshipHF = World.HistoricalFigures[WorshipHFID.Value];
        }

        internal override void Process()
        {

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
                    case "race":
                        Race = World.GetAddRace(val);
                        break;
                    case "type":
                        if (!Types.Contains(val))
                            Types.Add(val);
                        entityType = (short)Types.IndexOf(val);
                        break;
                    case "site_link":
                       var newSL = new EntitySiteLink(element, this);
                        if (SiteLinks == null)
                            SiteLinks = new Dictionary<int, List<EntitySiteLink>>();
                        if (!SiteLinks.ContainsKey(newSL.LinkType))
                            SiteLinks.Add(newSL.LinkType, new List<EntitySiteLink>());
                        SiteLinks[newSL.LinkType].Add(newSL);
                        break;
                    case "entity_link":
                        var newEL = new EntityEntityLink(element, this);
                        if (EntityLinks == null)
                            EntityLinks = new Dictionary<int, List<EntityEntityLink>>();
                        if (!EntityLinks.ContainsKey(newEL.LinkType))
                            EntityLinks.Add(newEL.LinkType, new List<EntityEntityLink>());
                        EntityLinks[newEL.LinkType].Add(newEL);
                        break;
                    case "child":
                        if (ChildrenIDs == null)
                            ChildrenIDs = new List<int>();
                        ChildrenIDs.Add(valI);
                        break;
                    case "worship_id":
                        WorshipHFID = valI;
                        break;
                    case "coords":
                        if (Coords == null)
                            Coords = new List<Point>();
                        foreach (var coordSplit in val.Split(new[] { '|' }, StringSplitOptions.RemoveEmptyEntries).Select(coord => coord.Split(',')).Where(coordSplit => coordSplit.Length == 2))
                        {
                            Coords.Add(new Point(Convert.ToInt32(coordSplit[0]), Convert.ToInt32(coordSplit[1])));
                        }
                        break;
                    default:
                        DFXMLParser.UnexpectedXMLElement(xdoc.Root.Name.LocalName + "\t", element, xdoc.Root.ToString());
                        break;
                }
            }
        }

        public void MakePlayer()
        {
            if (isPlayerControlled) return;
            isPlayerControlled = true;
            if (Members != null)
            {
                foreach (var hf in Members)
                    hf.isPlayerControlled = true;
            }
            if (FormerMembers == null) return;
            foreach (var hf in FormerMembers)
                hf.isPlayerControlled = true;
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
                ID,
                Name.DBExport(),
                Type,
                Race.DBExport(),
                WorshipHFID.DBExport()
            };

            Database.ExportWorldItem(table, vals);

            if (EntityLinks != null)
            {
                foreach (var entityLink in EntityLinks.Values.SelectMany(entityLinkList => entityLinkList))
                    entityLink.Export(ID);
            }

            if (SiteLinks != null)
            {
                foreach (var siteLink in SiteLinks.Values.SelectMany(siteLinkList => siteLinkList))
                    siteLink.Export(ID);
            }

            if (ChildrenIDs == null) return;
            foreach (var child in ChildrenIDs)
                Database.ExportWorldItem("Entity_EntityChild", new List<object>{ID, child});
        }
    }
}
