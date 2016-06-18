using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;
using DFWV.Annotations;
using DFWV.WorldClasses.EntityClasses;
using DFWV.WorldClasses.HistoricalEventClasses;
using DFWV.WorldClasses.HistoricalEventCollectionClasses;
using DFWV.WorldClasses.HistoricalFigureClasses;

namespace DFWV.WorldClasses
{
    public class Site : XMLObject
    {
        [UsedImplicitly]
        public string AltName { get; private set; }

        public string SiteMapPath { get; set; }

        public Entity Owner { get; set; }
        public Civilization Parent { get; private set; }
        public List<Leader> Leaders { get; set; }
        public Dictionary<Race, int> Population { get; private set; }
        public Dictionary<Race, int> Prisoners { get; private set; }
        public Dictionary<Race, int> Outcasts { get; private set; }

        //public string SiteType { get; set; }
        private bool SiteFileMerged { get; set; }

        public int? CivId { get; set; }
        public Entity Civ { get; set; }
        public int? CurOwnerId { get; set; }
        public Entity CurOwner { get; set; }

        public List<HistoricalFigure> Inhabitants { get; set; }
        public List<WorldConstruction> ConstructionLinks { get; set; }

        public HE_CreatedSite CreatedEvent { get; set; }
        public HE_SiteDied DiedEvent { get; set; }

        public List<EC_BeastAttack> BeastAttackEventCollections { get; set; }
        public List<EC_Battle> BattleEventCollections { get; set; }
        public List<EC_Duel> DuelEventCollections { get; set; }
        public List<EC_Abduction> AbductionEventCollections { get; set; }
        public List<EC_SiteConquered> SiteConqueredEventCollections { get; set; }
        public List<EC_Theft> TheftEventCollections { get; set; }
        public List<EC_Insurrection> InsurrectionEventCollections { get; set; }

        public List<Artifact> CreatedArtifacts { get; set; }


        [UsedImplicitly]
        public bool IsPlayerControlled { private get; set; }

        public IEnumerable<HistoricalEvent> Events
        {
            get
            {
                return World.HistoricalEvents.Values.Where(x => x.SitesInvolved.Contains(this));
            }
        }

        [UsedImplicitly]
        public int EventCount { get; set; }

        override public Point Location => Coords;
        public MapLegend MapLegend { get; set; }

        [UsedImplicitly]
        public string DispNameLower => ToString().ToLower();

        [UsedImplicitly]
        public int CreatedArtifactCount => CreatedArtifacts?.Count ?? 0;

        [UsedImplicitly]
        public int HfInhabitantCount => Inhabitants?.Count ?? 0;

        [UsedImplicitly]
        public int TotalPopulation => (Population?.Count ?? 0) +
                                      (Prisoners?.Count ?? 0) +
                                      (Outcasts?.Count ?? 0);



        #region Parse From Site File
        public Site(IEnumerable<string> curSite, World world) : base(world)
        {
            
            World = world;
            Leaders = new List<Leader>();
            Population = new Dictionary<Race,int>();
            Prisoners = new Dictionary<Race,int>();
            Outcasts = new Dictionary<Race, int>();

            var data = curSite.ToList();
            ParseNameLine(data[0]);
            data.RemoveAt(0);

            foreach (var line in data)
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
            var pop = Convert.ToInt32(data.Substring(0, data.IndexOf(' ')));
            var race = data.Substring(data.IndexOf(' ') + 1);
            var thisRace = World.GetAddRace(race);
            if (race.Contains(" prisoner"))
            {
                if (Prisoners.ContainsKey(thisRace))
                    Prisoners[thisRace]++;
                else
                    Prisoners.Add(thisRace, pop);
            }
            else if (race.Contains(" outcast"))
            {
                if (Outcasts.ContainsKey(thisRace))
                    Outcasts[thisRace]++;
                else
                    Outcasts.Add(thisRace, pop);
            }
            else
            {
                if (Population.ContainsKey(thisRace))
                    Population[thisRace] += pop;
                else
                    Population.Add(thisRace, pop);
            }
        }

        private void ParseLeaderLine(string data)
        {
            data = data.Trim();
            var type = data.Substring(0, data.IndexOf(':'));
            var race = data.Substring(data.LastIndexOf(',') + 2);
            var name = data.Substring(data.IndexOf(':') + 2, data.LastIndexOf(',') - data.IndexOf(':') - 2);

            var thisLeader = World.GetAddLeader(name);
            thisLeader.Site = this;
            thisLeader.IsCurrent = true;
            if (thisLeader.Civilization == null)
            {
                if (Parent != null)
                {
                    thisLeader.Civilization = Parent;
                    if (!Parent.Leaders.ContainsKey(type))
                    {
                        var newLeaderList = new List<Leader>();
                        Parent.Leaders.Add(type, newLeaderList);
                    }
                    Parent.Leaders[type].Add(thisLeader);
                }
            }


            
            thisLeader.Race = World.GetAddRace(race);

            if (!Leader.LeaderTypes.Contains(type))
                Leader.LeaderTypes.Add(type);
            thisLeader.LeaderType = Leader.LeaderTypes.IndexOf(type);

            Leaders.Add(thisLeader);
        }

        private void ParseParentCivLine(string data)
        {
            data = data.Substring(data.IndexOf(':') + 1).Trim();
            var race = data.Substring(data.LastIndexOf(',') + 1).Trim();
            if (race.IsPlural())
                race = race.Singularize();
            data = data.Substring(0, data.LastIndexOf(',')).Trim();
            Parent = World.GetCiv(data);
            if (Parent == null) return;
            if (Parent.Color == Color.Empty)
            {
                Parent.Color = Program.NextDistinctColor();
                Parent.FirstSite = this;
            }
            if (Parent.Race == null || !string.Equals(Parent.Race.Name, race, StringComparison.CurrentCultureIgnoreCase))
            {
                if (Parent.Race == null)
                    Parent.Race = World.GetAddRace(race);
                else
                    Program.Log(LogType.Warning, "Parent Civ Race doesn't match history file race/n" + data);
            }
            if (Owner != null && Owner.ParentCiv == null)
                Owner.ParentCiv = Parent;
        }

        private void ParseOwnerLine(string data)
        {
            data = data.Substring(data.IndexOf(':') + 1).Trim();
            //string race = data.Substring(data.LastIndexOf(',') + 1).Trim();
            //data = data.Substring(0, data.LastIndexOf(',')).Trim();
            Owner = World.GetAddEntity(data);
        }

        private void ParseNameLine(string data)
        {
            Id = Convert.ToInt32(data.Split(':')[0]);
            data = data.Replace(Id.ToString() + ':', "").Trim();

            var siteType = data.Split(',').Last().Trim();

            if (!Types.Contains(siteType))
                Types.Add(siteType);
            Type = Types.IndexOf(siteType);

            data = data.Substring(0, data.LastIndexOf(',') - 1);

            AltName = data.Substring(data.LastIndexOf(',') + 3);

            Name = data.Substring(0, data.LastIndexOf(','));

        }

        #endregion


        public static List<string> Types = new List<string>();
        public int? Type { get; private set; }
        public Point Coords { get; }
        private string StructureList { get; set; }
        public List<Structure> Structures { get; set; }

        [UsedImplicitly]
        public string SiteType => Type.HasValue ? Types[Type.Value] : string.Empty;

        public Site(XDocument xdoc, World world)
            : base(xdoc, world)
        {
            foreach (var element in xdoc.Root.Elements())
            {
                var val = element.Value.Trim();
                int valI;
                int.TryParse(val, out valI);
                switch (element.Name.LocalName)
                {
                    case "id":
                        SiteMapPath = World.MapPath.Replace("world_map", "site_map-" + Id);
                        if (!File.Exists(SiteMapPath))
                            SiteMapPath = null;
                        break;
                    case "type":
                        if (!Types.Contains(val))
                            Types.Add(val);
                        Type = Types.IndexOf(val);
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
                            Program.Log(LogType.Warning, $"Unexpected structures list for site: {Name} - {val}");
                        break;
                    case "civ_id":
                        if (valI != -1)
                            CivId = valI;
                        break;
                    case "cur_owner_id":
                        if (valI != -1)
                            CurOwnerId = valI;
                        break;
                    default:
                        DFXMLParser.UnexpectedXmlElement(xdoc.Root.Name.LocalName, element, xdoc.Root.ToString());
                        break;
                }
            }
        }

        public override void Select(MainForm frm)
        {
            if (frm.grpSite.Text == ToString() && frm.MainTab.SelectedTab == frm.tabSite)
                return;
            Program.MakeSelected(frm.tabSite, frm.lstSite, this);

            frm.grpSite.Text = ToString();
            if (IsPlayerControlled)
                frm.grpSite.Text += @" (PLAYER CONTROLLED)";
#if DEBUG
            frm.grpSite.Text += $" - ID: {Id}";
#endif
            frm.grpSite.Show();

            frm.lblSiteName.Text = ToString();
            frm.lblSiteAltName.Text = AltName;
            frm.lblSiteType.Text = SiteType;
            frm.lblSiteCoord.Data = new Coordinate(Coords);
            frm.lblSiteOwner.Data = Owner;
            frm.lblSiteParentCiv.Data = Parent;
            frm.lblSiteCurOwner.Data = CurOwner;
            frm.lblSiteCiv.Data = Civ;

            var siteMapPath = World.MapPath.Replace("world_map", "site_map-" + Id);
            frm.SiteMapLabel.Visible = File.Exists(siteMapPath);

            

            frm.grpSiteCreated.Visible = CreatedEvent != null;
            if (CreatedEvent != null)
            {
                frm.lblSiteCreatedBy.Data = CreatedEvent.SiteCiv;
                frm.lblSiteCreatedByCiv.Data = CreatedEvent.Civ;
                frm.lblSiteCreatedTime.Data = CreatedEvent;
                frm.lblSiteCreatedTime.Text = CreatedEvent.Time.ToString();
            }

            if (Population != null)
            {
                frm.grpSitePopulation.FillListboxWith(frm.lstSitePopulation, Population.Keys, this);
                frm.grpSitePopulation.Text = $"Population ({Population.Sum(x => x.Value)})";
            }
            else
            {
                frm.grpSitePopulation.Visible = false;
            }
            frm.grpSiteArtifacts.FillListboxWith(frm.lstSiteArtifacts, CreatedArtifacts);
            frm.grpSiteStructures.FillListboxWith(frm.lstSiteStructures, Structures);
            if (Prisoners != null)
            {
                frm.grpSitePrisoners.FillListboxWith(frm.lstSitePrisoners, Prisoners.Keys, this);
                frm.grpSitePrisoners.Text = $"Prisoners ({Prisoners.Sum(x => x.Value)})";
            }
            else
            {
                frm.grpSitePrisoners.Visible = false;
            }
            if (Outcasts != null)
            {
                frm.grpSiteOutcasts.FillListboxWith(frm.lstSiteOutcasts, Outcasts.Keys, this);
                frm.grpSiteOutcasts.Text = $"Outcasts ({Outcasts.Sum(x => x.Value)})";
            }
            else
            {
                frm.grpSiteOutcasts.Visible = false;
            }
            frm.grpSiteInhabitants.FillListboxWith(frm.lstSiteInhabitants, Inhabitants);
            frm.grpSiteEvent.FillListboxWith(frm.lstSiteEvent, Events);

            
            frm.trvSiteEventCollection.Nodes.Clear();
            if (AbductionEventCollections != null)
            {
                var thisNode = new TreeNode("Abduction");
                foreach (var newNode in AbductionEventCollections.Select(evtcol => new TreeNode(evtcol.ToString()) {Tag = evtcol}))
                {
                    thisNode.Nodes.Add(newNode);
                }
                thisNode.Text += $" ({thisNode.Nodes.Count})";
                frm.trvSiteEventCollection.Nodes.Add(thisNode);
            }
            if (BattleEventCollections != null)
            {
                var thisNode = new TreeNode("Battle");
                foreach (var newNode in BattleEventCollections.Select(evtcol => new TreeNode(evtcol.ToString()) {Tag = evtcol}))
                {
                    thisNode.Nodes.Add(newNode);
                }
                thisNode.Text += $" ({thisNode.Nodes.Count})";
                frm.trvSiteEventCollection.Nodes.Add(thisNode);
            }
            if (BeastAttackEventCollections != null)
            {
                var thisNode = new TreeNode("Beast Attack");
                foreach (var newNode in BeastAttackEventCollections.Select(evtcol => new TreeNode(evtcol.ToString()) {Tag = evtcol}))
                {
                    thisNode.Nodes.Add(newNode);
                }
                thisNode.Text += $" ({thisNode.Nodes.Count})";
                frm.trvSiteEventCollection.Nodes.Add(thisNode);
            }
            if (DuelEventCollections != null)
            {
                var thisNode = new TreeNode("Duel");
                foreach (var newNode in DuelEventCollections.Select(evtcol => new TreeNode(evtcol.ToString()) {Tag = evtcol}))
                {
                    thisNode.Nodes.Add(newNode);
                }
                thisNode.Text += $" ({thisNode.Nodes.Count})";
                frm.trvSiteEventCollection.Nodes.Add(thisNode);
            }
            if (SiteConqueredEventCollections != null)
            {
                var thisNode = new TreeNode("Site Conquered");
                foreach (var newNode in SiteConqueredEventCollections.Select(evtcol => new TreeNode(evtcol.ToString()) {Tag = evtcol}))
                {
                    thisNode.Nodes.Add(newNode);
                }
                thisNode.Text += $" ({thisNode.Nodes.Count})";
                frm.trvSiteEventCollection.Nodes.Add(thisNode);
            }
            if (TheftEventCollections != null)
            {
                var thisNode = new TreeNode("Theft");
                foreach (var newNode in TheftEventCollections.Select(evtcol => new TreeNode(evtcol.ToString()) { Tag = evtcol }))
                {
                    thisNode.Nodes.Add(newNode);
                }
                thisNode.Text += $" ({thisNode.Nodes.Count})";
                frm.trvSiteEventCollection.Nodes.Add(thisNode);
            }
            if (InsurrectionEventCollections != null)
            {
                var thisNode = new TreeNode("Insurrection");
                foreach (var newNode in InsurrectionEventCollections.Select(evtcol => new TreeNode(evtcol.ToString()) { Tag = evtcol }))
                {
                    thisNode.Nodes.Add(newNode);
                }
                thisNode.Text += $" ({thisNode.Nodes.Count})";
                frm.trvSiteEventCollection.Nodes.Add(thisNode);
            }


            frm.grpSiteEventCollection.Visible = frm.trvSiteEventCollection.Nodes.Count > 0;

            frm.SetDisplayedItem(this);
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
            MapLegend = sf.MapLegend;
            sf.SiteFileMerged = true;
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
                    case "structures":
                        foreach (var structureXml in element.Elements())
                        {
                            Structure thisStructure = null;
                            foreach (var strElement in structureXml.Elements())
                            {
                                var strval = strElement.Value;
                                int strvalI;
                                int.TryParse(strval, out strvalI);
                                switch (strElement.Name.LocalName)
                                {
                                    case "id":
                                        thisStructure = GetStructure(strvalI);
                                        if (thisStructure == null)
                                        {
                                            thisStructure = new Structure(this, strvalI, World);
                                            AddStructure(thisStructure);
                                        }
                                        break;
                                    case "type":
                                        if (!Structure.Types.Contains(strval))
                                            Structure.Types.Add(strval);
                                        thisStructure.Type = Structure.Types.IndexOf(strval);
                                        break;
                                    case "name":
                                        thisStructure.Name = strval;
                                        break;
                                    case "name2":
                                        break;
                                }
                            }
                        }
                        break;
                    case "civ_id":
                        if (valI != -1)
                            CivId = valI;
                        break;
                    case "cur_owner_id":
                        if (valI != -1)
                            CurOwnerId = valI;
                        break;
                    default:
                        DFXMLParser.UnexpectedXmlElement(xdoc.Root.Name.LocalName + "\t" + SiteType, element, xdoc.Root.ToString());
                        break;
                }
            }
        }

        internal override void Export(string table)
        {

            var vals = new List<object>
            {
                Id,
                Name.DBExport(),
                AltName.DBExport(),
                SiteType
            };

            Database.ExportWorldItem(table, vals);
        }

        internal void AddStructure(Structure structure)
        {
            if (Structures == null)
                Structures = new List<Structure>();
            World.Structures.Add(structure.Id, structure);
            Structures.Add(structure);
        }

        internal Structure GetStructure(int structSiteId)
        {
            return Structures?.FirstOrDefault(structure => structure.SiteId == structSiteId);
        }

        internal override void Link()
        {
            if (CivId.HasValue && World.Entities.ContainsKey(CivId.Value))
                Civ = World.Entities[CivId.Value];
            if (CurOwnerId.HasValue && World.Entities.ContainsKey(CurOwnerId.Value))
                CurOwner = World.Entities[CurOwnerId.Value];
        }

        internal override void Process()
        {

        }
    }
}