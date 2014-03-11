
namespace DFWV.WorldClasses
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Xml.Linq;
    using System.Windows.Forms;
    using System.Drawing;
    using DFWV.WorldClasses.HistoricalEventClasses;
    using DFWV.WorldClasses.HistoricalFigureClasses;
    using DFWV.WorldClasses.HistoricalEventCollectionClasses;
    using System.Collections.Specialized;

    class Entity : XMLObject
    {
        public Race Race { get; set; }
        public Civilization Civilization { get; set; }
        public Civilization ParentCiv { get; set; }
        public bool EntityFileMerged { get; set; }

        public List<Entity> CivGroups { get; set; }
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

        public List<HistoricalEvent> Events { get; set; }

        public HE_SiteTakenOver SiteTakeoverEvent { get; set; }
        public HE_EntityCreated CreatedEvent { get; set; }

        public List<HE_EntityLaw> LawEvents { get; set; }

        public List<EC_BeastAttack> BeastAttackEventCollections { get; set; }
        public List<EC_War> WarEventCollections { get; set; }
        public List<EC_Abduction> AbductionEventCollections { get; set; }
        public List<EC_SiteConquered> SiteConqueredEventCollections { get; set; }
        public List<EC_Theft> TheftEventCollections { get; set; }

        public bool isPlayerControlled { get; set; }

        override public Point Location 
        { 
            get 
            {
                if (ParentCiv == null && Civilization != null && Civilization.FirstSite != null)
                    return Civilization.FirstSite.Location;
                else if (ParentCiv == null && Civilization != null && Civilization.FirstSite == null)
                    return Point.Empty;
                else if (ParentCiv == null && Civilization == null && CreatedEvent != null)
                    return CreatedEvent.Location;
                else if (ParentCiv != null && ParentCiv.FirstSite != null)
                    return ParentCiv.FirstSite.Location;
                else if (ParentCiv == null && Civilization == null)
                    return Point.Empty;
                else
                    return Point.Empty;
            } 
        }


        public int MemberCount { get { return Members == null ? 0 : Members.Count; } }
        public string DispNameLower { get { return ToString().ToLower(); } }
        public string Type
        {
            get 
            {
                if (Civilization != null)
                    return "Civ";
                else if (CreatedEvent != null)
                    return "Religion";
                else if (ParentCiv != null)
                    return "Group";
                else
                    return "Unknown"; 
            }
        }


        #region Parse from Sites Files
        public Entity(string name, World world) : base(world)
        {
            World = world;
            Name = name;
        }
        #endregion


        public Entity(XDocument xdoc, World world)
            : base(xdoc, world)
        {
 
            foreach (XElement element in xdoc.Root.Elements())
            {
                string val = element.Value.ToString();
                switch (element.Name.LocalName)
                {
                    case "id":
                        break;
                    case "name":
                        Name = val;
                        break;
                    default:
                        DFXMLParser.UnexpectedXMLElement(xdoc.Root.Name.LocalName, element, xdoc.Root.ToString());
                        break;
                }
            }
        }

        //public Entity (NameValueCollection data, World world) 
        //    : base (data, world)
        //{
        //    Name = data["Name"].ToString();
        //}

        public override string ToString()
        {
            if (base.ToString() != null)
                return base.ToString();
            return ID.ToString();
        }

        public override void Select(MainForm frm)
        {
            frm.grpEntity.Text = this.ToString();
            if (isPlayerControlled)
                frm.grpEntity.Text += " (PLAYER CONTROLLED)";
            frm.grpEntity.Show();

            frm.lblEntityName.Text = ToString();
            frm.lblEntityType.Text = Type;
            frm.lblEntityRace.Data = Race;
            frm.lblEntityCivilization.Data = Civilization;
            frm.lblEntityParentCiv.Data = ParentCiv;

            frm.trvEntityRelatedFigures.Nodes.Clear();

            frm.grpEntityRelatedFigures.Visible =
                Enemies != null || Members != null || FormerMembers != null ||
                Prisoners != null || FormerPrisoners != null || Criminals != null ||
                Slaves != null || FormerSlaves != null || Heroes != null;
            if (frm.grpEntityRelatedFigures.Visible)
            {
                List<List<HistoricalFigure>> EntityHFLists = new List<List<HistoricalFigure>>() 
                                {Enemies, Members, FormerMembers, Prisoners, FormerPrisoners, Criminals,
                                Slaves, FormerSlaves, Heroes};
                List<string> EntityHFListNames = new List<string>()
                                {"Enemies", "Members", "Former Members", "Prisoners", "Former Prisoners", "Criminals",
                                "Slaves", "Former Slaves", "Heroes"};
                for (int i = 0; i < EntityHFListNames.Count; i++)
                {
                    if (EntityHFLists[i] != null)
                    {
                        TreeNode thisNode = new TreeNode(EntityHFListNames[i] + " (" + EntityHFLists[i].Count + ")");
                        foreach (HistoricalFigure hf in EntityHFLists[i])
                        {
                            TreeNode newNode;
                            if (hf.Dead)
                                newNode = new TreeNode(hf.ToString() + " (" + hf.Birth.ToString() + " - " + hf.Death.ToString() + ")");
                            else
                                newNode = new TreeNode(hf.ToString() + " (" + hf.Birth.ToString() + " - )");
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
            }

            frm.grpEntityCreated.Visible = CreatedEvent != null;
            if (CreatedEvent != null)
            {
                frm.lblEntityCreatedSite.Data = CreatedEvent.Site;
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

            frm.grpEntitySubGroups.Visible = CivGroups != null;
            
            frm.lstEntitySubGroups.Items.Clear();
            if (CivGroups != null)
            {
                foreach (Entity ent in CivGroups)
                    frm.lstEntitySubGroups.Items.Add(ent);
            }

            frm.lstEntityEvents.BeginUpdate();
            frm.lstEntityEvents.Items.Clear();

            if (Events != null)
            {
                foreach (var evt in Events)
                    frm.lstEntityEvents.Items.Add(evt);
            }

            if (frm.lstEntityEvents.Items.Count > 0)
            {
                frm.grpEntityEvents.Show();
                frm.lstEntityEvents.SelectedIndex = 0;
            }
            else
                frm.grpEntityEvents.Hide();

            frm.lstEntityEvents.EndUpdate();

            Program.MakeSelected(frm.tabEntity, frm.lstEntity, this);
        }

        internal override void Link()
        {
            if (ParentCiv != null && ParentCiv.Entity != null)
            {
                if (ParentCiv.Entity.CivGroups == null)
                    ParentCiv.Entity.CivGroups = new List<Entity>();
                if (!ParentCiv.Entity.CivGroups.Contains(this))
                    ParentCiv.Entity.CivGroups.Add(this);
            }
        }

        internal override void Process()
        {

        }

        public void MakePlayer()
        {
            if (!isPlayerControlled)
            {
                isPlayerControlled = true;
                if (Members != null)
                {
                    foreach (HistoricalFigure hf in Members)
                        hf.isPlayerControlled = true;
                }
                if (FormerMembers != null)
                {
                    foreach (HistoricalFigure hf in FormerMembers)
                        hf.isPlayerControlled = true;
                }
            }
        }

        internal void MergeInEntityFile(Entity ent)
        {
            Race = ent.Race;
            ParentCiv = ent.ParentCiv;
            ent.EntityFileMerged = true;
            foreach (Site site in World.Sites.Values.Where(x => x.Owner == ent))
                    site.Owner = this;
        }

        internal override void Export(string table)
        {

            List<object> vals = new List<object>();

            vals.Add(ID);

            if (Name == null)
                vals.Add(DBNull.Value);
            else
                vals.Add(Name.Replace("'", "''"));

            vals.Add(Type);

            Database.ExportWorldItem(table, vals);
        }
    }
}
