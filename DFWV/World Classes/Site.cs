using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Drawing;
using System.Windows.Forms;
using DFWV.WorldClasses.HistoricalEventClasses;
using DFWV.WorldClasses.HistoricalFigureClasses;
using DFWV.WorldClasses.HistoricalEventCollectionClasses;
using System.Collections.Specialized;

namespace DFWV.WorldClasses
{
    class Site : XMLObject
    {
        public string AltName { get; set; }

        public Entity Owner { get; set; }
        public Civilization Parent { get; set; }
        public List<Leader> Leaders { get; set; }
        public Dictionary<Race, int> Population { get; set; }
        public Dictionary<Race, int> Prisoners { get; set; }
        public Dictionary<Race, int> Outcasts { get; set; }

        public int SiteFileID { get; set; }
        //public string SiteType { get; set; }
        public bool SiteFileMerged { get; set; }

        public List<HistoricalFigure> Inhabitants { get; set; }
        public List<WorldConstruction> ConstructionLinks { get; set; }

        public HE_CreatedSite CreatedEvent { get; set; }
        public List<HE_PlunderedSite> PlunderedEvents { get; set; }
        public List<HE_ReclaimSite> ReclaimedEvents { get; set; }
        public List<HistoricalEvent> NewLeaderEvents { get; set; }
        public List<HE_AttackedSite> AttackedEvents { get; set; }
        public List<HE_DestroyedSite> DestroyedEvents { get; set; }

        public List<EC_BeastAttack> BeastAttackEventCollections { get; set; }
        public List<EC_Battle > BattleEventCollections { get; set; }
        public List<EC_Duel> DuelEventCollections { get; set; }
        public List<EC_Abduction> AbductionEventCollections { get; set; }
        public List<EC_SiteConquered> SiteConqueredEventCollections { get; set; }
        public List<EC_Theft> TheftEventCollections { get; set; }

        public bool isPlayerControlled { get; set; }

        override public Point Location { get { return Coords; } }


        public string DispNameLower { get { return ToString().ToLower(); } }
        public int HFInhabitantCount { get { return Inhabitants == null ? 0 : Inhabitants.Count; } }
        public int TotalPopulation 
        { 
            get 
            { 
                return (Population == null ? 0 : Population.Count) +
                        (Prisoners == null ? 0 : Prisoners.Count) +
                        (Outcasts == null ? 0 : Outcasts.Count); 
            } 
        }


        #region Parse From Site File
        public Site(List<string> curSite, World world) : base(world)
        {
            // TODO: Complete member initialization
            World = world;
            Leaders = new List<Leader>();
            Population = new Dictionary<Race,int>();
            Prisoners = new Dictionary<Race,int>();
            Outcasts = new Dictionary<Race, int>();

            List<string> data = curSite.ToList<string>();
            ParseNameLine(data[0]);
            data.RemoveAt(0);

            foreach (string line in data)
            {
                if (line.Contains("Owner: "))
                    ParseOwnerLine(line);
                else if (line.Contains("Parent Civ: "))
                    ParseParentCivLine(line);
                else if (line.Contains(":"))
                    ParseLeaderLine(line);
                else
                    ParsePopulationLine(line);
            }
        }

        private void ParsePopulationLine(string data)
        {
            data = data.Trim();
            int Pop = Convert.ToInt32(data.Substring(0, data.IndexOf(' ')));
            string race = data.Substring(data.IndexOf(' ') + 1);
            Race thisRace = World.GetAddRace(race);
            if (race.Contains(" prisoner"))
            {
                if (Prisoners.ContainsKey(thisRace))
                    Prisoners[thisRace]++;
                else
                    Prisoners.Add(thisRace, Pop);
            }
            else if (race.Contains(" outcast"))
            {
                if (Outcasts.ContainsKey(thisRace))
                    Outcasts[thisRace]++;
                else
                    Outcasts.Add(thisRace, Pop);
            }
            else
            {
                if (Population.ContainsKey(thisRace))
                    Population[thisRace] += Pop;
                else
                    Population.Add(thisRace, Pop);
            }
        }

        private void ParseLeaderLine(string data)
        {
            data = data.Trim();
            string type = data.Substring(0, data.IndexOf(':'));
            string race = data.Substring(data.LastIndexOf(',') + 2);
            string name = data.Substring(data.IndexOf(':') + 2, data.LastIndexOf(',') - data.IndexOf(':') - 2);

            Leader thisLeader = World.GetAddLeader(name);
            thisLeader.Site = this;
            if (thisLeader.Civilization == null)
            {
                if (Parent != null)
                    thisLeader.Civilization = Parent;
            }

            
            thisLeader.Race = World.GetAddRace(race);
            thisLeader.LeaderType = type;

            Leaders.Add(thisLeader);
        }

        private void ParseParentCivLine(string data)
        {
            data = data.Substring(data.IndexOf(':') + 1).Trim();
            string race = data.Substring(data.LastIndexOf(',') + 1).Trim();
            data = data.Substring(0, data.LastIndexOf(',')).Trim();
            Parent = World.GetCiv(data);
            if (Parent != null)
            { 
                if (Parent.Color == Color.Empty)
                {
                    Parent.Color = Program.NextDistinctColor();
                    Parent.FirstSite = this;
                }
                if (Parent.Race == null || Parent.Race.Name.ToLower() != race.ToLower())
                {
                    if (Parent.Race == null)
                        Parent.Race = World.GetAddRace(race);
                    else
                        Program.Log(LogType.Warning, "Parent Civ Race doesn't match history file race/n" + data);
                }
                if (Owner != null)
                    Owner.ParentCiv = Parent;
            }
        }

        private void ParseOwnerLine(string data)
        {
            data = data.Substring(data.IndexOf(':') + 1).Trim();
            string race = data.Substring(data.LastIndexOf(',') + 1).Trim();
            //data = data.Substring(0, data.LastIndexOf(',')).Trim();
            Owner = World.GetAddEntity(data);
        }

        private void ParseNameLine(string data)
        {
            ID = Convert.ToInt32(data.Split(':')[0]);
            data = data.Replace(ID.ToString() + ':', "").Trim();

            string siteType = data.Split(',').Last<string>().Trim();

            if (!Site.Types.Contains(siteType))
                Site.Types.Add(siteType);
            Type = Site.Types.IndexOf(siteType);

            data = data.Substring(0, data.LastIndexOf(',') - 1);

            AltName = data.Substring(data.LastIndexOf(',') + 3);

            Name = data.Substring(0, data.LastIndexOf(','));

        }

        #endregion


        public static List<string> Types = new List<string>();
        public int Type { get; set; }
        public Point Coords { get; set; }
        public string StructureList { get; set; }
        public Dictionary<int, Structure> Structures { get; set; }

        public string SiteType { get { return Types[Type]; } }

        public Site(XDocument xdoc, World world)
            : base(xdoc, world)
        {
            //Inhabitants = new List<HistoricalFigure>();

            foreach (XElement element in xdoc.Root.Elements())
            {
                string val = element.Value.ToString().Trim();
                switch (element.Name.LocalName)
                {
                    case "id":
                        break;
                    case "type":
                        if (!Site.Types.Contains(val))
                            Site.Types.Add(val);
                        Type = Site.Types.IndexOf(val);
                        break;
                    case "name":
                        Name = val;
                        break;
                    case "coords":
                        Coords = new Point(
                            Convert.ToInt32(val.Split(',')[0]),
                            Convert.ToInt32(val.Split(',')[1]));
                        break;
                    case "structures":
                        StructureList = val;
                        if (val != "")
                            break;
                        break;
                    
                    default:
                        DFXMLParser.UnexpectedXMLElement(xdoc.Root.Name.LocalName, element, xdoc.Root.ToString());
                        break;
                }
            }
        }

        //public Site(NameValueCollection data, World world) 
        //    : base (world)
        //{
        //    Name = data["Name"].ToString();
        //    Type = data["Type"].ToString();
        //    Coords = new Point(
        //                Convert.ToInt32(data["Coords"].ToString().Split(',')[0]),
        //                Convert.ToInt32(data["Coords"].ToString().Split(',')[1]));
        //}

        public override void Select(MainForm frm)
        {
            frm.grpSite.Text = this.ToString();
            if (isPlayerControlled)
                frm.grpSite.Text += " (PLAYER CONTROLLED)";
            frm.grpSite.Show();

            frm.lblSiteName.Text = ToString();
            frm.lblSiteAltName.Text = AltName;
            frm.lblSiteType.Text = WorldClasses.Site.Types[Type];
            frm.lblSiteCoord.Data = new Coordinate(Coords);
            frm.lblSiteOwner.Data = Owner;
            frm.lblSiteParentCiv.Data = Parent;

            frm.grpSiteCreated.Visible = CreatedEvent != null;
            if (CreatedEvent != null)
            {
                frm.lblSiteCreatedBy.Data = CreatedEvent.SiteCiv;
                frm.lblSiteCreatedByCiv.Data = CreatedEvent.Civ;
                frm.lblSiteCreatedTime.Data = CreatedEvent;
                frm.lblSiteCreatedTime.Text = CreatedEvent.Time.ToString();
            }
            frm.lstSitePopulation.Items.Clear();
            if (Population != null)
                frm.lstSitePopulation.Items.AddRange(Population.Keys.ToArray());
            frm.grpSitePopulation.Visible = frm.lstSitePopulation.Items.Count > 0;

            frm.lstSitePrisoners.Items.Clear();
            if (Outcasts != null)
                frm.lstSitePrisoners.Items.AddRange(Prisoners.Keys.ToArray());
            frm.grpSitePrisoners.Visible = frm.lstSitePrisoners.Items.Count > 0;

            frm.lstSiteOutcasts.Items.Clear();
            if (Outcasts != null)
                frm.lstSiteOutcasts.Items.AddRange(Outcasts.Keys.ToArray());
            frm.grpSiteOutcasts.Visible = frm.lstSiteOutcasts.Items.Count > 0;

            frm.lstSiteInhabitants.BeginUpdate();
            frm.lstSiteInhabitants.Items.Clear();
            if (Inhabitants != null)
                frm.lstSiteInhabitants.Items.AddRange(Inhabitants.ToArray());
            frm.lstSiteInhabitants.EndUpdate();
            frm.grpSiteInhabitants.Visible = frm.lstSiteInhabitants.Items.Count > 0;
            frm.grpSiteInhabitants.Text = "Historical Figures (" + frm.lstSiteInhabitants.Items.Count + ")";

            frm.trvSiteEvent.Nodes.Clear();
            if (AttackedEvents != null)
            {
                TreeNode thisNode = new TreeNode("Attacked");
                foreach (HistoricalEvent evt in AttackedEvents)
                {
                    TreeNode newNode = new TreeNode(evt.ToString());
                    newNode.Tag = evt;
                    thisNode.Nodes.Add(newNode);
                }
                thisNode.Text += " (" + thisNode.Nodes.Count + ")";
                frm.trvSiteEvent.Nodes.Add(thisNode);
            }
            if (DestroyedEvents != null)
            {
                TreeNode thisNode = new TreeNode("Destroyed");
                foreach (HistoricalEvent evt in DestroyedEvents)
                {
                    TreeNode newNode = new TreeNode(evt.ToString());
                    newNode.Tag = evt;
                    thisNode.Nodes.Add(newNode);
                }
                thisNode.Text += " (" + thisNode.Nodes.Count + ")";
                frm.trvSiteEvent.Nodes.Add(thisNode);
            }
            if (NewLeaderEvents != null)
            {
                TreeNode thisNode = new TreeNode("NewLeader");
                foreach (HistoricalEvent evt in NewLeaderEvents)
                {
                    TreeNode newNode = new TreeNode(evt.ToString());
                    newNode.Tag = evt;
                    thisNode.Nodes.Add(newNode);
                }
                thisNode.Text += " (" + thisNode.Nodes.Count + ")";
                frm.trvSiteEvent.Nodes.Add(thisNode);
            }
            if (PlunderedEvents != null)
            {
                TreeNode thisNode = new TreeNode("Plundered");
                foreach (HistoricalEvent evt in PlunderedEvents)
                {
                    TreeNode newNode = new TreeNode(evt.ToString());
                    newNode.Tag = evt;
                    thisNode.Nodes.Add(newNode);
                }
                thisNode.Text += " (" + thisNode.Nodes.Count + ")";
                frm.trvSiteEvent.Nodes.Add(thisNode);
            }
            if (ReclaimedEvents != null)
            {
                TreeNode thisNode = new TreeNode("Reclaimed");
                foreach (HistoricalEvent evt in ReclaimedEvents)
                {
                    TreeNode newNode = new TreeNode(evt.ToString());
                    newNode.Tag = evt;
                    thisNode.Nodes.Add(newNode);
                }
                thisNode.Text += " (" + thisNode.Nodes.Count + ")";
                frm.trvSiteEvent.Nodes.Add(thisNode);
            }

            frm.grpSiteEvent.Visible = frm.trvSiteEvent.Nodes.Count > 0;

            frm.trvSiteEventCollection.Nodes.Clear();
            if (AbductionEventCollections != null)
            {
                TreeNode thisNode = new TreeNode("Abduction");
                foreach (EC_Abduction evtcol in AbductionEventCollections)
                {
                    TreeNode newNode = new TreeNode(evtcol.ToString());
                    newNode.Tag = evtcol;
                    thisNode.Nodes.Add(newNode);
                }
                thisNode.Text += " (" + thisNode.Nodes.Count + ")";
                frm.trvSiteEventCollection.Nodes.Add(thisNode);
            }
            if (BattleEventCollections != null)
            {
                TreeNode thisNode = new TreeNode("Battle");
                foreach (EC_Battle evtcol in BattleEventCollections)
                {
                    TreeNode newNode = new TreeNode(evtcol.ToString());
                    newNode.Tag = evtcol;
                    thisNode.Nodes.Add(newNode);
                }
                thisNode.Text += " (" + thisNode.Nodes.Count + ")";
                frm.trvSiteEventCollection.Nodes.Add(thisNode);
            }
            if (BeastAttackEventCollections != null)
            {
                TreeNode thisNode = new TreeNode("Beast Attack");
                foreach (EC_BeastAttack evtcol in BeastAttackEventCollections)
                {
                    TreeNode newNode = new TreeNode(evtcol.ToString());
                    newNode.Tag = evtcol;
                    thisNode.Nodes.Add(newNode);
                }
                thisNode.Text += " (" + thisNode.Nodes.Count + ")";
                frm.trvSiteEventCollection.Nodes.Add(thisNode);
            }
            if (DuelEventCollections != null)
            {
                TreeNode thisNode = new TreeNode("Duel");
                foreach (EC_Duel evtcol in DuelEventCollections)
                {
                    TreeNode newNode = new TreeNode(evtcol.ToString());
                    newNode.Tag = evtcol;
                    thisNode.Nodes.Add(newNode);
                }
                thisNode.Text += " (" + thisNode.Nodes.Count + ")";
                frm.trvSiteEventCollection.Nodes.Add(thisNode);
            }
            if (SiteConqueredEventCollections != null)
            {
                TreeNode thisNode = new TreeNode("Site Conquered");
                foreach (EC_SiteConquered evtcol in SiteConqueredEventCollections)
                {
                    TreeNode newNode = new TreeNode(evtcol.ToString());
                    newNode.Tag = evtcol;
                    thisNode.Nodes.Add(newNode);
                }
                thisNode.Text += " (" + thisNode.Nodes.Count + ")";
                frm.trvSiteEventCollection.Nodes.Add(thisNode);
            }
            if (TheftEventCollections != null)
            {
                TreeNode thisNode = new TreeNode("Theft");
                foreach (EC_Theft evtcol in TheftEventCollections)
                {
                    TreeNode newNode = new TreeNode(evtcol.ToString());
                    newNode.Tag = evtcol;
                    thisNode.Nodes.Add(newNode);
                }
                thisNode.Text += " (" + thisNode.Nodes.Count + ")";
                frm.trvSiteEventCollection.Nodes.Add(thisNode);
            }


            frm.grpSiteEventCollection.Visible = frm.trvSiteEventCollection.Nodes.Count > 0;

            Program.MakeSelected(frm.tabSite, frm.lstSite, this);
        }

        internal void MergeInSiteFile(Site sf)
        {
            Name = sf.Name;
            AltName = sf.AltName;
            Owner = sf.Owner;
            Parent = sf.Parent;
            Leaders = sf.Leaders;
            Population = sf.Population;
            Prisoners = sf.Prisoners;
            Outcasts = sf.Outcasts;
            Type = sf.Type;
            if (sf.Parent != null && sf.Parent.FirstSite == sf)
                sf.Parent.FirstSite = this;
            sf.SiteFileMerged = true;
        }
        internal override void Link()
        {

        }

        internal override void Process()
        {

        }

        internal override void Export(string table)
        {

            List<object> vals = new List<object>();

            vals.Add(ID);

            if (Name == null)
                vals.Add(DBNull.Value);
            else
                vals.Add(Name.Replace("'", "''"));

            vals.Add(AltName);
            vals.Add(Types[Type]);

            Database.ExportWorldItem(table, vals);
        }
    }
}
