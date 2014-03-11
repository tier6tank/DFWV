using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Diagnostics;
using DFWV.WorldClasses.HistoricalEventClasses;
using DFWV.WorldClasses.HistoricalFigureClasses;
using DFWV.WorldClasses.HistoricalEventCollectionClasses;
using System.Data.SQLite;

namespace DFWV.WorldClasses
{
    #region Delegates
    public delegate void XMLLinkedSectionEventHandler(string section);
    public delegate void XMLLinkedEventHandler();
    public delegate void XMLProcessedSectionEventHandler(string section);
    public delegate void XMLProcessedEventHandler();

    public delegate void FamiliesCountedEventHandler();
    public delegate void DynastiesCreatedEventHandler();
    public delegate void EventCollectionsEvaluatedEventHandler();
    public delegate void HistoricalFiguresPositionedEventHandler();
    public delegate void StatsGatheredEventHandler();
    public delegate void VisualizationsCreatedEventHandler();
    #endregion

    class World
    {
        #region Fields and Properties
        private string historyPath;
        private string sitesPath;
        private string paramPath;
        private string mapPath;
        private string xmlPath;

        private string dbPath;

        public string Name { get; set; }
        public string AltName { get; set; }
        public string Version { get; set; }
        public int LastYear { get; set; }

        public List<Civilization> Civilizations = new List<Civilization>();
        public List<Leader> Leaders = new List<Leader>();
        public Dictionary<string, Race> Races = new Dictionary<string, Race>();
        public List<God> Gods = new List<God>();
        public Dictionary<int, Site> SitesFile = new Dictionary<int, Site>();
        public List<Entity> EntitiesFile = new List<Entity>();
        public List<Parameter> Parameters = new List<Parameter>();
        public List<Dynasty> Dynasties = new List<Dynasty>();

        public Dictionary<int, Region> Regions = new Dictionary<int, Region>();
        public Dictionary<int, UndergroundRegion> UndergroundRegions = new Dictionary<int, UndergroundRegion>();
        public Dictionary<int, Site> Sites = new Dictionary<int, Site>();
        public Dictionary<int, HistoricalFigure> HistoricalFigures = new Dictionary<int, HistoricalFigure>();
        public Dictionary<int, EntityPopulation> EntityPopulations = new Dictionary<int, EntityPopulation>();
        public Dictionary<int, Entity> Entities = new Dictionary<int, Entity>();
        public Dictionary<int, HistoricalEvent> HistoricalEvents = new Dictionary<int, HistoricalEvent>();
        public Dictionary<int, HistoricalEventCollection> HistoricalEventCollections = new Dictionary<int, HistoricalEventCollection>();
        public Dictionary<int, HistoricalEra> HistoricalEras = new Dictionary<int, HistoricalEra>();
        public Dictionary<int, WorldConstruction> WorldConstructions = new Dictionary<int, WorldConstruction>();
        public Dictionary<int, Artifact> Artifacts = new Dictionary<int, Artifact>();

        public Dictionary<string, string> Maps { get; set; }

        public FilterSettings Filters;

        public Stats Stats;
        public VisualizationCollection Visualizations;
        #endregion

        public World(string historyPath, string sitesPath, string paramPath, string xmlPath, string mapPath, int MapYear)
        {
            LastYear = MapYear;
            WorldTime.Present = new WorldTime(LastYear);

            this.historyPath = historyPath;
            this.sitesPath = sitesPath;
            this.paramPath = paramPath;
            this.mapPath = mapPath;
            this.xmlPath = xmlPath;

            Filters = new FilterSettings(this);
        }

        public World(string dbPath)
        {
            this.dbPath = dbPath;
        }

        #region World Loading
        public void LoadFiles()
        {
            LoadHistory();
            LoadSites();
            LoadMaps();
            LoadParam();
            LoadXML();

        }

        /// <summary>
        /// Given the name and path to the Basic Map, this will find and add another other maps found in teh same directory.
        /// </summary>
        private void LoadMaps()
        {

            string filename = Path.GetFileName(mapPath);
            Maps = new Dictionary<string, string>();
            Maps.Add("Main", mapPath);
            List<string> MapSymbols = new List<string>() 
                {"bm", "drn", "el", "elw", "evil", "hyd", "rain",
                    "sal", "sav", "str", "tmp", "trd", "veg", "vol"};
            List<string> MapNames = new List<string>() 
                {"Biome", "Drainage", "Elevations", "Elevations w/Water", "Evil", 
                    "Hydrosphere", "Rainfall", "Sailinity", "Savagry", "Structures", 
                    "Temperature", "Trade", "Vegetation", "Volcanism"};

            string thisMap = mapPath.Replace("world_map", "world_graphic");
            if (File.Exists(thisMap))
                Maps.Add("Standard+Biome", thisMap);

            for (int i = 0; i < MapSymbols.Count; i++)
            {
                thisMap = mapPath.Replace("world_map", "world_graphic-" + MapSymbols[i]);
                if (File.Exists(thisMap))
                    Maps.Add(MapNames[i], thisMap);
            }

        }

        /// <summary>
        /// This splits up the paramters file and adds the lines as new Parameters.
        /// </summary>
        private void LoadParam()
        {
            List<string> lines = File.ReadAllLines(paramPath, Encoding.GetEncoding(437)).ToList<string>();

            Version = lines[0].Substring(15);
            Version = Version.Substring(0, Version.Length - 1);

            lines.RemoveRange(0, 3);
            foreach (string line in lines)
                Parameters.Add(new Parameter(line.Trim(), this));
        }


        /// <summary>
        /// This parses the Sites file, which has multiple sections.  
        ///     This keeps track of data associated with the most recent site, and adds it to cursite, which stores all lines about a site (which could be 1 to more then a dozen).
        ///     When it encounters data about a new site it dumps the data about the current site into a new Site object.
        ///     
        ///     It also retrieves data about the numbers of specific races as listed in the sites file.
        /// </summary>
        private void LoadSites()
        {

            List<string> lines = File.ReadAllLines(sitesPath, Encoding.GetEncoding(437)).ToList<string>();

            lines.RemoveRange(0, 2);

            List<string> curSite = new List<string>();
            foreach (string line in lines)
            {
                if (line == "" || line == "Sites" || line == "Outdoor Animal Populations (Including Undead)")
                {
                    if (curSite.Count > 0)
                    {
                        Site newSite = new Site(curSite, this);
                        SitesFile.Add(newSite.ID, newSite);
                        curSite.Clear();
                    }
                    continue;
                }
                if (line.StartsWith("\t") && curSite.Count == 0)
                {
                    if (!line.Contains("Total: "))
                    {

                        string raceName = line.Trim().Substring(line.Trim().IndexOf(' ') + 1);
                        Race thisRace = GetAddRace(raceName);
                        if (line.Contains("Unnumbered"))
                            thisRace.Population = long.MaxValue;
                        else
                            thisRace.Population = Convert.ToInt32(line.Trim().Split(' ')[0]);
                    }
                }
                else if (!line.StartsWith("\t")) //Start of Site maybe
                {
                    if (curSite.Count > 0)
                    {
                        Site newSite = new Site(curSite, this);
                        SitesFile.Add(newSite.ID, newSite);
                        curSite.Clear();
                    }
                    curSite.Add(line);
                }
                else
                    curSite.Add(line);
            }
        }


        /// <summary>
        ///  This parses the History file.
        ///     First it pulls the Wolrd Name and Alt name
        ///     then it runs through each civ, creating a new object as it does so.
        ///     For civs that are just names, like "Ant Men" they're designated as not being "FullCivs" in the Civilizatio constructor.
        ///  
        /// </summary>
        private void LoadHistory()
        {
            List<string> lines = File.ReadAllLines(historyPath, Encoding.GetEncoding(437)).ToList<string>();

            Name = lines[0];
            AltName = lines[1];
            Program.mainForm.Text = "World Viewer - " + Name + " \"" + AltName + "\"";

            lines.RemoveRange(0, 3);

            List<string> curCiv = new List<string>();
            foreach (string line in lines)
            {
                if (!line.StartsWith(" ")) // Start of a civ
                {
                    if (curCiv.Count > 0)
                    {
                        Civilizations.Add(new Civilization(curCiv, this));
                        curCiv.Clear();
                    }
                }
                curCiv.Add(line);
            }
            if (curCiv.Count > 0)
            {
                Civilizations.Add(new Civilization(curCiv, this));
                curCiv.Clear();
            }
        }

        /// <summary>
        /// This creates a new thread from the DFXMLParser static class, to parse the XML.  
        /// Due to it being exceptionally complext having a separate thread and object made sense.
        /// See DFXMLParser.Parse() for details.
        /// </summary>
        private void LoadXML()
        {
            Thread XMLThread = new Thread(() => DFXMLParser.Parse(this, xmlPath));
            XMLThread.Start();
        }
        #endregion

        #region World Importing (WIP)
        //internal void Import()
        //{
        //    Database.SetConnection();

        //    ImportWorldItems<Artifact>(Artifacts, "artifact");
        //    ImportWorldItems<Entity>(Entities, "entity");
        //    ImportWorldItems<EntityPopulation>(EntityPopulations, "entitypopulation");
        //    ImportWorldItems<HistoricalFigure>(HistoricalFigures, "historicalfigure");
        //    ImportWorldItems<HistoricalEra>(HistoricalEras, "historicalera");
        //    ImportWorldItems<HistoricalEvent>(HistoricalEvents, "historicalevent");
        //    ImportWorldItems<HistoricalEventCollection>(HistoricalEventCollections, "historicaleventcollection");
        //    ImportWorldItems<Parameter>(Parameters, "parameter");
        //    ImportWorldItems<Race>(Races, "race");
        //    ImportWorldItems<Region>(Regions, "region");
        //    ImportWorldItems<Site>(Sites, "site");
        //    ImportWorldItems<UndergroundRegion>(UndergroundRegions, "undergroundregion");
        //    ImportWorldItems<WorldConstruction>(WorldConstructions, "worldconstruction");
        //    //ImportMaps();
        //    //ImportWorldData();
        //    Database.CloseConnection();
        //}

        //private void ImportWorldItems<T>(Dictionary<int, T> dict, string table) where T : XMLObject
        //{
        //    Database.ExecuteQuery("Select * From " + table);
        //    while (Database.Reader.Read())
        //    {
        //        if (typeof(T) == typeof(HistoricalEvent))
        //        {
        //            HistoricalEvent evt = HistoricalEvent.Create(Database.Reader.GetValues(), this);
        //            HistoricalEvents.Add(evt.ID, evt);
        //        }
        //        else if (typeof(T) == typeof(HistoricalEventCollection))
        //        {
        //            HistoricalEventCollection evtcol = HistoricalEventCollection.Create(Database.Reader.GetValues(), this);
        //            HistoricalEventCollections.Add(evtcol.ID, evtcol);
        //        }
        //        else
        //        {
        //            T WorldObject = (T)Activator.CreateInstance(typeof(T), new object[] { Database.Reader.GetValues(), this });
        //            dict.Add(WorldObject.ID, WorldObject);
        //        }
        //    }
        //}

        //private void ImportWorldItems<T>(Dictionary<string, T> dict, string table) where T : WorldObject
        //{
        //    Database.ExecuteQuery("Select * From " + table);
        //    while (Database.Reader.Read())
        //    {
        //        T WorldObject = (T)Activator.CreateInstance(typeof(T), new object[] { Database.Reader.GetValues(), this });
        //        dict.Add(WorldObject.Name, WorldObject);
        //    }
        //}

        //private void ImportWorldItems<T>(List<T> list, string table) where T : WorldObject
        //{
        //    Database.ExecuteQuery("Select * From " + table);
        //    while (Database.Reader.Read())
        //    {
        //        T WorldObject = (T)Activator.CreateInstance(typeof(T), new object[] { Database.Reader.GetValues(), this });
        //        list.Add(WorldObject);
        //    }
        //}
        #endregion

        /// <summary>
        /// These methods all work in the same way.  Since the site and history file might list the same civ, god, entity, or leader in multiple places 
        ///     we need to check if we already created that object before we create it again.  These methods all check the current list of Objects for one matching the given one.
        ///     If a match is found it's returned, otherwise a new object is created, and then that is returned.
        /// </summary>
        /// <param name="raceName"></param>
        /// <returns></returns>
        #region GetAdd Functions
        internal Race GetAddRace(string raceName)
        {
            string lname = raceName.ToLower();
            lname = StripRaceNumbers(lname);
            lname = lname.Replace("_", " ");
            if (lname.Contains("prisoners"))
                lname = lname.Replace(" prisoners", "");
            else if (lname.Contains("prisoner"))
                lname = lname.Replace(" prisoner", "");
            else if (lname.Contains("outcasts"))
                lname = lname.Replace(" outcasts", "");
            else if (lname.Contains("outcast"))
                lname = lname.Replace(" outcast", "");

            if (Races.ContainsKey(lname))
                return Races[lname];
            else if (Races.ContainsKey(lname.Remove(lname.Length - 1) + "ves"))
                return Races[lname.Remove(lname.Length - 1) + "ves"];
            else if (Races.ContainsKey(lname + "s"))
                return Races[lname + "s"];
            else if (lname.Contains("men") && Races.ContainsKey(lname.Replace("men", "man")))
                return Races[lname.Replace("men", "man")];
            else if (lname.Contains("man") && Races.ContainsKey(lname.Replace("man", "men")))
                return Races[lname.Replace("man", "men")];


            foreach (Race race in Races.Values)
            {
                string thisRaceName = race.Name.ToLower();
                if (thisRaceName == lname)
                    return race;
                else if (thisRaceName.EndsWith("s") && thisRaceName.Remove(thisRaceName.Length - 1) == lname)
                    return race;
                else if (thisRaceName.EndsWith("ves") && thisRaceName.Remove(thisRaceName.Length - 3) + "f" == lname)
                    return race;
                else if (thisRaceName.Contains("men") && thisRaceName.Replace("men", "man") == lname)
                    return race;
                else if (lname.Contains("men") && thisRaceName.Replace("men", "man") == lname)
                    return race;
            }

            if (!Races.ContainsKey(lname))
            { 
                Race newRace = new Race(lname, this);
                Races.Add(lname, newRace);
                return newRace;
            }
            else
                return Races[lname];
            
        }

        private string StripRaceNumbers(string race)
        {
            int num;
            while (Int32.TryParse(race.Substring(race.Length - 1, 1), out num) || race[race.Length - 1] == '_')
            {
                race = race.Remove(race.Length - 1);
            }
            return race;
        }

        internal God GetAddGod(God tempGod)
        {
            foreach (God god in Gods)
            {
                if (god.Name.ToLower() == tempGod.Name.ToLower())
                {
                    return god;
                }
            }
            Gods.Add(tempGod);
            return tempGod;
        }

        internal God GetAddGod(string godName)
        {
            foreach (God god in Gods)
            {
                if (god.Name.ToLower() == godName.ToLower())
                {
                    return god;
                }
            }
            God newGod = new God(godName);
            Gods.Add(newGod);
            return newGod;

        }

        internal Civilization GetCiv(string civName)
        {
            foreach (Civilization civ in Civilizations)
            {
                if (civ.Name == civName)
                    return civ;
            }
            Program.Log(LogType.Warning, "Site Civ doesn't exist in history file/n" + civName); // 
            return null;

        }

        internal Entity GetAddEntity(string data)
        {
            string entityName = data.Split(',')[0].Trim();
            string entityRaceName = data.Split(',')[1].Trim();

            Race entityRace = GetAddRace(entityRaceName);

            foreach (Civilization civ in Civilizations)
            {
                if (civ.Name == entityName && civ.Race == entityRace)
                    return civ;
            }
            foreach (Entity entity in EntitiesFile)
            {
                if (entity.Name == entityName)
                {
                    Program.Log(LogType.Warning, "Duplicate named Entities: " + entityName);
                    if (entity.Race == entityRace)
                        return entity;
                }
                
            }
            Entity newEntity = new Entity(entityName, this);
            newEntity.Race = entityRace;
            EntitiesFile.Add(newEntity);
            return newEntity;
        }

        internal Leader GetAddLeader(string leaderName)
        {
            foreach (Leader leader in Leaders)
            {
                if (leader.Name.ToLower() == leaderName.ToLower())
                    return leader;
            }
            Leader newLeader = new Leader(leaderName);
            Leaders.Add(newLeader);
            return newLeader;
        }
        #endregion 

        /// <summary>
        /// These methods handle object linking, which generally is used to turn object IDs (from XML) into references to World Objects in code.
        ///     They raise events up to the MainForm to notify when things are finished.
        /// </summary>
        #region Data Linking
        public static event XMLLinkedSectionEventHandler LinkedSection;
        public static event XMLLinkedEventHandler Linked;

        internal void LinkXMLData()
        {
            Thread XMLThread = new Thread(() => Link());
            XMLThread.Start();
        }

        private void LinkSection<T>(Dictionary<int, T>.ValueCollection List, string sectionName) where T : XMLObject
        {


            foreach (var item in List)
                item.Link();
            OnLinkedSection(sectionName);

        }

        private void Link()
        {

            LinkSection<Artifact>(Artifacts.Values, "Artifacts");
            LinkSection<Entity>(Entities.Values, "Entities");
            LinkSection<EntityPopulation>(EntityPopulations.Values, "Entity Populations");
            LinkSection<HistoricalEra>(HistoricalEras.Values, "Historical Eras");
            LinkSection<HistoricalEvent>(HistoricalEvents.Values, "Historical Events");
            LinkSection<HistoricalEventCollection>(HistoricalEventCollections.Values, "Historical Event Collections");
            LinkSection<HistoricalFigure>(HistoricalFigures.Values, "Historical Figures");
            LinkSection<Region>(Regions.Values, "Regions");
            LinkSection<Site>(Sites.Values, "Sites");
            LinkSection<UndergroundRegion>(UndergroundRegions.Values, "Underground Regions");
            LinkSection<WorldConstruction>(WorldConstructions.Values, "World Constructions");

            OnLinked();
        }

        public static void OnLinkedSection(string section)
        {
            if (LinkedSection != null)
                LinkedSection(section);
        }

        public static void OnLinked()
        {
            if (Linked != null)
                Linked();
        }
        #endregion

        /// <summary>
        /// These methods handle object processing, which gathers additional information about objects including creating lists of things.
        ///     They raise events up to the MainForm to notify when things are finished.
        /// </summary>
        #region Data Processing
        public static event XMLProcessedSectionEventHandler ProcessedSection;
        public static event XMLProcessedEventHandler Processed;
        public static event FamiliesCountedEventHandler FamiliesCounted;
        public static event DynastiesCreatedEventHandler DynastiesCreated;
        public static event EventCollectionsEvaluatedEventHandler EventCollectionsEvaluated;
        public static event HistoricalFiguresPositionedEventHandler HistoricalFiguresPositioned;
        public static event StatsGatheredEventHandler StatsGathered;
        public static event VisualizationsCreatedEventHandler VisualizationsCreated;

        internal void ProcessXMLData()
        {
            Thread XMLThread = new Thread(() => Process());
            XMLThread.Start();
        }

        private void Process()
        {
            ProcessSection<Region>(Regions.Values, "Regions");
            ProcessSection<Site>(Sites.Values, "Sites");
            ProcessSection<UndergroundRegion>(UndergroundRegions.Values, "Underground Regions");
            ProcessSection<WorldConstruction>(WorldConstructions.Values, "World Constructions");
            ProcessSection<Artifact>(Artifacts.Values, "Artifacts");
            ProcessSection<Entity>(Entities.Values, "Entities");
            ProcessSection<EntityPopulation>(EntityPopulations.Values, "Entity Populations");
            ProcessSection<HistoricalEra>(HistoricalEras.Values, "Historical Eras");
            ProcessSection<HistoricalFigure>(HistoricalFigures.Values, "Historical Figures");
            ProcessSection<HistoricalEvent>(HistoricalEvents.Values, "Historical Events");
            ProcessSection<HistoricalEventCollection>(HistoricalEventCollections.Values, "Historical Event Collections");

            OnProcessed();
        }

        private void ProcessSection<T>(Dictionary<int, T>.ValueCollection List, string sectionName) where T : XMLObject
        {
            foreach (var item in List)
                item.Process();
            OnProcessedSection(sectionName);

        }

        public static void OnProcessedSection(string section)
        {
            if (ProcessedSection != null)
                ProcessedSection(section);

        }

        public static void OnProcessed()
        {
            if (Processed != null)
                Processed();
        }
        #endregion

        /// <summary>
        /// These methods handle Family processing, which counts families and dynasties to allow sorting by number of descendents/ancestors.
        ///     They raise events up to the MainForm to notify when things are finished.
        /// </summary>
        #region Family Processing
        internal void FamilyProcessing()
        {
            Thread CountFamiliesThread = new Thread(() => CountFamilies());
            CountFamiliesThread.Start();

            Thread CreateDynastiesThread = new Thread(() => CreateDynasties());
            CreateDynastiesThread.Start();
        }

        private void CountFamilies()
        {
            foreach (HistoricalFigure HF in HistoricalFigures.Values)
                HF.CountAncestors();
            foreach (HistoricalFigure HF in HistoricalFigures.Values)
                HF.CountDescendents();

            OnFamiliesCounted();
        }

        public static void OnFamiliesCounted()
        {
            if (FamiliesCounted != null)
                FamiliesCounted();
        }

        /// <summary>
        /// Creates Dynasties given familial relationships from the XML and data on inheritance from the history file.
        /// </summary>
        private void CreateDynasties()
        {
            foreach (Civilization civ in Civilizations)
            {
                foreach (List<Leader> leaderList in civ.Leaders.Values)
                {
                    //king list
                    leaderList.Reverse();

                    Dynasty thisDynasty = null;
                    
                    for (int i = 0; i < leaderList.Count - 1; i++)
                    {
                        if (leaderList[i].HF  != null &&
                            leaderList[i].InheritedFrom != null &&
                            leaderList[i + 1].HF != null &&
                            leaderList[i].InheritedFrom == leaderList[i + 1].HF)
                        {
                            //i and i+1 are in the same dynasty
                            if (thisDynasty == null)
                            {
                                thisDynasty = new Dynasty(this, leaderList[i].HF, leaderList[i].LeaderType, civ);
                                thisDynasty.Members.Add(leaderList[i + 1].HF);
                            }
                            else
                            {
                                thisDynasty.Members.Add(leaderList[i + 1].HF);
                            }
                        }
                        else if (leaderList[i].Inheritance != "New Line" &&
                                 leaderList[i].Inheritance != "Original Line")
                        {

                        }
                        else if (thisDynasty != null)
                        {
                            thisDynasty.Members.Reverse();
                            Dynasties.Add(thisDynasty);
                            thisDynasty = null;
                        }
                    }
                    if (thisDynasty != null)
                    {
                        thisDynasty.Members.Reverse();
                        Dynasties.Add(thisDynasty);
                        thisDynasty = null;
                    }
                    
                    leaderList.Reverse();
                }
            }

            OnDynastiesCreated();
        }

        public static void OnDynastiesCreated()
        {
            if (DynastiesCreated != null)
                DynastiesCreated();
        }

        #endregion

        /// <summary>
        /// These methods handle further evluation of Event Collections following all XML data processing.
        ///     They raise events up to the MainForm to notify when things are finished.
        /// </summary>
        #region Event Collection Evaluation
        internal void EventCollectionEvaluation()
        {
            Thread EventCollectionEvaluationsThread = new Thread(() => EvaluateEventCollections());
            EventCollectionEvaluationsThread.Start();
        }

        /// <summary>
        /// Evaluates event collections in detail.  Allowing events to be linked to additional pieces of information only available by analyzing the event collection they are a part of.
        /// </summary>
        private void EvaluateEventCollections()
        {
            
            // Only check Historical Event Collections that contain events
            foreach (HistoricalEventCollection evtcol in HistoricalEventCollections.Values.Where(x => x.Event != null))
            {
                switch (HistoricalEventCollection.Types[evtcol.Type])
                {
                    case "abduction":
                        // For abduction event collections, if we have a new HF entity link following an hf abducted link, 
                        //      then we can say that the hf entity link is of type "prisoner", and the HF in Add HF Entity link is the one that was abducted in our abduction event.
                        for (int i = 1; i < evtcol.Event.Count; i++)
                        {
                            if (HistoricalEvent.Types[evtcol.Event[i].Type] == "add hf entity link" && 
                                HistoricalEvent.Types[evtcol.Event[i- 1].Type] == "hf abducted")
                            {
                                ((HE_AddHFEntityLink)evtcol.Event[i]).HF = ((HE_HFAbducted)evtcol.Event[i - 1]).TargetHF;
                                ((HE_AddHFEntityLink)evtcol.Event[i]).LinkType = "prisoner";
                            }
                        }
                        break;

                    case "theft":
                        // For theft event collections, if we have item stolen events within that collection, we can note information about who stole, 
                        //      and who was stolen from and where based on data from the event collection.
                        EC_Theft theft_ec = (EC_Theft)evtcol;
                        foreach (HistoricalEvent evt in theft_ec.Event.Where(x => HistoricalEvent.Types[x.Type] == "item stolen"))
                        {
                            HE_ItemStolen ev = (HE_ItemStolen)evt;
                            ev.AttackerCiv = theft_ec.AttackingEn;
                            ev.DefenderCiv = theft_ec.DefendingEn;
                            ev.Site = theft_ec.Site;
                            ev.Coords = theft_ec.Coords;
                        }

                        break;
                    case "beast attack":
                        // For beast attack event collections, for every hf simple battle event, if the first HF is the same then we know that's the beast in the beast attack.
                        //      This can be noted in the event collection and any creature devoured events part of the collection can link back to the beast as the devourer.
                        HistoricalFigure BeastHF = null;
                        EC_BeastAttack beastattack_ec = (EC_BeastAttack)evtcol;
                        foreach (HistoricalEvent evt in beastattack_ec.Event.Where(x => HistoricalEvent.Types[x.Type] == "hf simple battle event"))
                        {
                            HE_HFSimpleBattleEvent ev = (HE_HFSimpleBattleEvent)evt;

                            if (ev.Group1HF.Count == 0)
                                break;

                            if (BeastHF == null)
                                BeastHF = ev.Group1HF[0];
                            else if (BeastHF == ev.Group1HF[0])
                                continue;
                            else
                                break;
                        }

                        if (BeastHF != null)
                        {
                            beastattack_ec.BeastHF = BeastHF;
                            foreach (HistoricalEvent evt in beastattack_ec.Event.Where(x => HistoricalEvent.Types[x.Type] == "creature devoured"))
                            {
                                HE_CreatureDevoured ev = (HE_CreatureDevoured)evt;
                                ev.Devourer = BeastHF;
                            }
                        }


                        break;
                    case "battle":
                        // For battle event collections, if we have hf died events we can add that HF as a casualty of the battle, 
                        //      which will be displayed in bold when viewing participating HFs.
                        EC_Battle battle_ec = (EC_Battle)evtcol;
                        foreach (HistoricalEvent evt in evtcol.Event.Where(x => HistoricalEvent.Types[x.Type] == "hf died"))
                        {
                            HE_HFDied ev = (HE_HFDied)evt;

                            if (battle_ec.AttackingHF.Contains(ev.HF))
                            {
                                if (battle_ec.AttackingDiedHF == null)
                                    battle_ec.AttackingDiedHF = new List<HistoricalFigure>();
                                battle_ec.AttackingDiedHF.Add(ev.HF);
                            }
                            else if (battle_ec.DefendingHF.Contains(ev.HF))
                            {
                                if (battle_ec.DefendingDiedHF == null)
                                    battle_ec.DefendingDiedHF = new List<HistoricalFigure>();
                                battle_ec.DefendingDiedHF.Add(ev.HF);
                            }
                        }
                        break;

                    case "site conquered":
                        // For site conquered event collections, if there are body abused events, we can link the attacking entity as the abuser witin those events.
                        foreach (HistoricalEvent evt in evtcol.Event.Where(x => HistoricalEvent.Types[x.Type] == "body abused"))
                        {
                            HE_BodyAbused ev = (HE_BodyAbused)evt;

                            ev.AbuserEn = ((EC_SiteConquered)evtcol).AttackingEn;
                        }

                        //Case: Site conquered event with Add hf entity link related to group that is part of attacker entity.   Also with change hf state with hf (former member of group of defender civ) settling in new site.
                        //Change: Associate add hf entity link for prisoner relationship where change HF state HF was captured and brought to new site as prisoner.

                        //To Do
                        break;

                    default:
                        break;
                }

                //Case: Any simple battle event in event col with following hf died.
                //Change: Tie simple battle event to death event, noting that one person died.
            }

            // Total up deaths/fighters in a war from the battle
            foreach (EC_War war in HistoricalEventCollections.Values.Where(x => HistoricalEventCollection.Types[x.Type] == "war"))
            {
                foreach (EC_Battle battle in war.EventCol.Where(x => HistoricalEventCollection.Types[x.Type] == "battle"))
                    battle.battleTotaled = false;
                war.TotalWar();
            }
             
            OnEventCollectionsEvaluated();
        }
        
        public static void OnEventCollectionsEvaluated()
        {
            if (EventCollectionsEvaluated != null)
                EventCollectionsEvaluated();
        }

        #endregion

        /// <summary>
        /// These methods handle assigning Historical Figures to coordinates, sites, or regions.
        ///     They raise events up to the MainForm to notify when things are finished.
        /// </summary>
        #region Historical Figures Positioning
        internal void HistoricalFiguresPositioning()
        {
            Thread HistoricalFiguresPositioningThread = new Thread(() => PositionHistoricalFigures());
            HistoricalFiguresPositioningThread.Start();
        }

        /// <summary>
        /// Evaluates the last known change HF state for a HF.  We then set the location of that HF at their last known location.
        /// </summary>
        private void PositionHistoricalFigures()
        {

            foreach (HistoricalFigure hf in HistoricalFigures.Values.Where(x => x.DiedEvent == null))
            {

                if (hf.Events != null && hf.Events.Where(x => HistoricalEvent.Types[x.Type] == "change hf state").Count() > 0)
                {
                    HE_ChangeHFState evt = (HE_ChangeHFState)hf.Events.Where(x => HistoricalEvent.Types[x.Type] == "change hf state").Last();

                    hf.Site = evt.Site;
                    hf.Region = evt.Subregion;
                    hf.Coords = evt.Coords;

                    if (hf.Site != null)
                    {
                        if (hf.Site.Inhabitants == null)
                            hf.Site.Inhabitants = new List<HistoricalFigure>();
                        if (!hf.Site.Inhabitants.Contains(hf))
                            hf.Site.Inhabitants.Add(hf);
                    }
                    if (hf.Region != null)
                    {
                        if (hf.Region.Inhabitants == null)
                            hf.Region.Inhabitants = new List<HistoricalFigure>();
                        if (!hf.Region.Inhabitants.Contains(hf))
                            hf.Region.Inhabitants.Add(hf);
                    }

                }
            }

            

            OnHistoricalFiguresPositioned();
        }

        public static void OnHistoricalFiguresPositioned()
        {
            if (HistoricalFiguresPositioned != null)
                HistoricalFiguresPositioned();
        }

        #endregion

        /// <summary>
        /// These methods handle gathering stats following all XML Processing.
        ///     They raise events up to the MainForm to notify when things are finished.
        /// </summary>
        #region Stats Gathering
        internal void StatsGathering()
        {
            Thread StatsGatheringThread = new Thread(() => GatherStats());
            StatsGatheringThread.Start();
        }

        private void GatherStats()
        {
            Stats = new Stats(this);
            Stats.Gather();
            OnStatsGathered();
        }

        public static void OnStatsGathered()
        {
            if (StatsGathered != null)
                StatsGathered();
        }
        #endregion


        /// <summary>
        /// These methods handle creation of visualization maps and follows Event Collection Evaluation.
        ///     They raise events up to the MainForm to notify when things are finished.
        /// </summary>
        #region Visualization Creation
        internal void VisualizationCreation()
        {
            Thread VisualizationCreationThread = new Thread(() => CreateVisualizations());
            VisualizationCreationThread.Start();
        }

        private void CreateVisualizations()
        {
            Visualizations = new VisualizationCollection(this);
            Visualizations.Create();
            OnVisualizationsCreated();
        }

        public static void OnVisualizationsCreated()
        {
            if (VisualizationsCreated != null)
                VisualizationsCreated();
        }
        #endregion

        /// <summary>
        /// These methods all find existing site/entity/civ objects from the history/site files, and if they were also found in the XML 
        ///     the data from the history/site file is copied over and the old object removed.
        /// </summary>
        #region Merging History/Site file data with XML
        internal void MergeSites()
        {
            foreach (Site sf in SitesFile.Values)
            {
                if (Sites[sf.ID].Name == sf.AltName.ToLower())
                    Sites[sf.ID].MergeInSiteFile(sf);
                else if (Sites[sf.ID].Name == Program.CleanString(sf.AltName.ToLower()))
                    Sites[sf.ID].MergeInSiteFile(sf);
                else if (PartialNameMatch(Sites[sf.ID].Name, sf.AltName.ToLower()) > .5)
                    Sites[sf.ID].MergeInSiteFile(sf);
                else
                    Program.Log(LogType.Warning, "Site from site file doesn't exist in XML");
                    
            }
            SitesFile.Clear();
            Program.Log(LogType.Status, "Sites Merged");
        }

        internal void MergeEntities()
        {
            foreach (Entity ent in EntitiesFile)
            {
                string entname = ent.Name.ToLower();
                foreach (Entity entxml in Entities.Values)
	            {
                    if (entxml.Name == entname && (entxml.Race == null || entxml.Race == ent.Race))
                    {
                        entxml.MergeInEntityFile(ent);
                        break;
                    }
	            }
                if (!ent.EntityFileMerged)
                {
                    entname = Program.CleanString(entname);
                    foreach (Entity entxml in Entities.Values)
                    {
                        if (entxml.Name == entname)
                        {
                            entxml.MergeInEntityFile(ent);
                            break;
                        }
                    }
                }
                if (!ent.EntityFileMerged)
                    Program.Log(LogType.Warning, "Entity from file doesn't exist in XML");
            }
            EntitiesFile.Clear();
            Program.Log(LogType.Status, "Entities Merged");
        }

        internal void MergeCivs()
        {
            foreach (Civilization civ in Civilizations.Where(x => x.isFull))
            {
                string civname = civ.Name.ToLower();
                if (civ.Color == Color.Empty)
                    civ.Color = Program.NextDistinctColor();
                foreach (Entity ent in Entities.Values)
                {
                    if (ent.Name == civname)
                    {
                        civ.Entity = ent;
                        ent.Civilization = civ;
                        break;
                    }
                }
                if (civ.Entity == null)
                {
                    civname = Program.CleanString(civname);
                    foreach (Entity ent in Entities.Values)
                    {
                        if (ent.Name == civname)
                        {
                            civ.Entity = ent;
                            ent.Civilization = civ;
                            break;
                        }
                    }
                }
                if (civ.Entity == null)
                    Program.Log(LogType.Warning, "Civ from file doesn't exist in XML");
            }
            Program.Log(LogType.Status, "Civs Linked");
        }
        #endregion


        /// <summary>
        /// These methods are used to match a leader/god to a historical figure, based on name comparisons and other data.
        /// </summary>
        #region Matching HFsToPeople
        internal void MatchHistoricalFiguresToPeople()
        {
            foreach (Leader leader in Leaders)
            {
                string leadername = leader.Name.ToLower();
                foreach (HistoricalFigure hf in HistoricalFigures.Values)
                {
                    if (hf.Name != null && hf.Name.ToLower() == leadername)
                    {
                        leader.HF = hf;
                        if (leader.InheritedFromName != null)
                            leader.LinkInheritance();

                        hf.Leader = leader;
                        break;
                    }
                }
                if (leader.HF == null)
                {
                    leadername = Program.CleanString(leadername);
                    foreach (HistoricalFigure hf in HistoricalFigures.Values)
                    {
                        if (hf.Name != null &&  hf.Name.ToLower() == leadername)
                        {
                            leader.HF = hf;
                            if (leader.InheritedFromName != null)
                                leader.LinkInheritance();

                            hf.Leader = leader;
                            break;
                        }
                    }
                }
                if (leader.HF == null)
                {
                    foreach (HistoricalFigure hf in HistoricalFigures.Values.Where(x =>
                        ((x.BirthYear < 0 && leader.Birth == null) || 
                            ((leader.Birth != null) && x.BirthYear == leader.Birth.Year)) &&
                        ((x.DeathYear == null && leader.Death  == WorldTime.Present) || 
                            (leader.Death != null && (x.DeathYear == leader.Death.Year)))
                        ))
                    {
                        if (isPartialMatch(hf, leader))
                        {
                            leader.HF = hf;
                            if (leader.InheritedFromName != null)
                                leader.LinkInheritance();
                            hf.Leader = leader;
                            break;
                        }
                    }
                }
                if (leader.HF == null)
                    Program.Log(LogType.Warning, "Leaderfrom File not in XML: " + leader.Name);
            }
            foreach (God god in Gods)
            {
                string godname = god.Name.ToLower();
                foreach (HistoricalFigure hf in HistoricalFigures.Values)
                {

                    if (hf.Name != null && hf.Name.ToLower() == godname)
                    {
                        god.HF = hf;
                        hf.God = god;
                        break;
                    }
                }
                if (god.HF == null)
                {
                    godname = Program.CleanString(godname);
                    foreach (HistoricalFigure hf in HistoricalFigures.Values)
                    {

                        if (hf.Name != null && hf.Name.ToLower() == godname)
                        {
                            god.HF = hf;
                            hf.God = god;
                            break;
                        }
                    }
                }
                if (god.HF == null)
                {
                    //var q = HistoricalFigures.Values.Where(x => x.Name.Length == 3 && x.Name.Contains("le")).ToList();
                    foreach (HistoricalFigure hf in HistoricalFigures.Values.Where(x => x.Deity || x.Force))//!= null && x.Sphere.Count == god.Spheres.Count))
                    {
                        if (isPartialMatch(hf, god))
                        {
                            god.HF = hf;
                            hf.God = god;
                            break;
                        }
                    }
                }
                if (god.HF == null)
                    Program.Log(LogType.Warning, "God from File not in XML: " + god.Name);
            }

            Program.Log(LogType.Status, "Historical Figures Found!");
        }

        private bool isNameMatch(string nameHF, string name)
        {
            if (nameHF == name.ToLower())
                return true;
            return false;
        }

        private bool isPartialMatch(HistoricalFigure hf, Leader leader)
        {
            bool useFull = false;
            if (hf.Name == null)
                return false;
            if (hf.Name.Length == leader.Name.Length)
                useFull = true;
            else if (hf.ShortName == null || hf.ShortName.Length != leader.Name.Length)
                return false;
            string hfname = useFull ? hf.Name : hf.ShortName;
            return PartialNameMatch(hfname, leader.Name.ToLower()) > .5;
        }

        private float PartialNameMatch(string A, string B)
        {
            int matches = 0;
            for (int i = 0; i < A.Length; i++)
            {
                if (A[i] == B[i])
                    matches++;
            }
            return (float)matches / (float)A.Length;
        }

        private bool isPartialMatch(HistoricalFigure hf, God god)
        {
            bool useFull = false;
            if (hf.Name.Length == god.Name.Length)
                useFull = true;
            else if (hf.Name == "" || hf.ShortName.Length != god.Name.Length)
                return false;
            string hfname = useFull ? hf.Name : hf.ShortName;
            return PartialNameMatch(hfname, god.Name.ToLower()) > .5;
        }
        #endregion

        #region Exporting

        /// <summary>
        /// When export is ran against  the world it exports all objects it first empties all the tables, then exports all the objects in sequence.
        /// </summary>
        internal void Export(string dbPath)
        {
            Database.SetConnection(dbPath);
            Database.EmptyAllTables();

            ExportWorldItems<Artifact>(Artifacts.Values.ToArray(), "artifact");
            ExportWorldItems<Entity>(Entities.Values.ToArray(), "entity");
            ExportWorldItems<EntityPopulation>(EntityPopulations.Values.ToArray(), "entitypopulation");
            ExportWorldItems<HistoricalFigure>(HistoricalFigures.Values.ToArray(), "historicalfigure"); //
            ExportWorldItems<HistoricalEra>(HistoricalEras.Values.ToArray(), "historicalera");
            ExportWorldItems<HistoricalEvent>(HistoricalEvents.Values.ToArray(), "historicalevent"); //
            ExportWorldItems<HistoricalEventCollection>(HistoricalEventCollections.Values.ToArray(), "historicaleventcollection"); //
            ExportWorldItems<Parameter>(Parameters.ToArray(), "parameter");
            ExportWorldItems<Race>(Races.Values.ToArray(), "race");
            ExportWorldItems<Region>(Regions.Values.ToArray(), "region");
            ExportWorldItems<Site>(Sites.Values.ToArray(), "site");
            ExportWorldItems<UndergroundRegion>(UndergroundRegions.Values.ToArray(), "undergroundregion");
            ExportWorldItems<WorldConstruction>(WorldConstructions.Values.ToArray(), "worldconstruction");
            ExportMaps();
            ExportWorldData();
            Database.CloseConnection();

            Program.mainForm.InvokeEx(f => f.exportWorldToolStripMenuItem.Visible = true);
        }

        private void ExportWorldData()
        {
            Database.BeginTransaction();

            Database.ExportWorldItem("World", new List<object>() { "Name", LastYear.ToString() });
            Database.ExportWorldItem("World", new List<object>() { "AltName", Name });
            Database.ExportWorldItem("World", new List<object>() { "LastYear", AltName });

            Database.CommitTransaction();
        }

        private void ExportMaps()
        {
            foreach (var map in Maps)
            {
                Database.BeginTransaction();


                List<object> vals = new List<object>() { map.Key, Database.ImageToBlob(Image.FromFile(map.Value)) };

                Database.ExportWorldItem("Map", vals);
                Database.CommitTransaction();
                vals.Clear();
                //Memory grew too fast without this.
                GC.Collect();
            }
        }


        /// <summary>
        /// Objects are exported 500 at a time in each transaction.
        /// </summary>
        private void ExportWorldItems<T>(T[] arr, string table) where T : WorldObject
        {

            int iCounter = 0;
            foreach (T item in arr)
            {

                if (iCounter == 0)
                    Database.BeginTransaction();
                item.Export(table);
                iCounter++;
                if (iCounter >= 500)
                {
                    Database.CommitTransaction();
                    iCounter = 0;
                }
            }

            if (iCounter > 0)
                Database.CommitTransaction();
        }

        #endregion

        public override string ToString()
        {
            return Name;
        }


    }
}
