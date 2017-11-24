using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using DFWV.WorldClasses.EntityClasses;
using DFWV.WorldClasses.HistoricalEventClasses;
using DFWV.WorldClasses.HistoricalEventCollectionClasses;
using DFWV.WorldClasses.HistoricalFigureClasses;

namespace DFWV.WorldClasses 
{
    #region Delegates
    public delegate void XmlLinkedSectionStartEventHandler(string section);
    public delegate void XmlLinkedSectionEventHandler(string section);
    public delegate void XmlLinkedEventHandler();
    public delegate void XmlProcessedSectionStartEventHandler(string section);
    public delegate void XmlProcessedSectionEventHandler(string section);
    public delegate void XmlProcessedEventHandler();

    public delegate void FamiliesCountedEventHandler();
    public delegate void DynastiesCreatedEventHandler();
    public delegate void EventsCountedEventHandler();
    public delegate void EventCollectionsEvaluatedEventHandler();
    public delegate void HistoricalFiguresPositionedEventHandler();
    public delegate void StatsGatheredEventHandler();
    public delegate void VisualizationsCreatedEventHandler();
    #endregion

    public class World : IDisposable
    {
        #region Fields and Properties
        public readonly string HistoryPath;
        public readonly string SitesPath;
        public readonly string ParamPath;
        public readonly string MapPath;
        public readonly string XmlPath;
        public readonly string XmlPlusPath;
        public bool HasPlusXml;
        public bool IsPlusParsing = false;

        public static List<Thread> Threads = new List<Thread>();

        public string Name { get; private set; }
        public string AltName { get; private set; }
        private string Version { get; set; }
        public int LastYear { get; }

        public readonly List<Civilization> Civilizations = new List<Civilization>();
        public readonly List<Leader> Leaders = new List<Leader>();
        public readonly List<God> Gods = new List<God>();
        public readonly Dictionary<int, Site> SitesFile = new Dictionary<int, Site>();
        public readonly List<Entity> EntitiesFile = new List<Entity>();
        public readonly List<Parameter> Parameters = new List<Parameter>();
        public readonly List<Dynasty> Dynasties = new List<Dynasty>();

        public readonly Dictionary<int, Army> Armies = new Dictionary<int, Army>();
        public readonly Dictionary<int, Unit> Units = new Dictionary<int, Unit>();
        public readonly Dictionary<int, Engraving> Engravings = new Dictionary<int, Engraving>();
        public readonly Dictionary<int, Report> Reports = new Dictionary<int, Report>();
        public readonly Dictionary<int, Building> Buildings = new Dictionary<int, Building>();
        public readonly Dictionary<int, Construction> Constructions = new Dictionary<int, Construction>();
        public readonly Dictionary<int, Item> Items = new Dictionary<int, Item>();
        public readonly Dictionary<int, Plant> Plants = new Dictionary<int, Plant>();
        public readonly Dictionary<int, Squad> Squads = new Dictionary<int, Squad>();
        public readonly Dictionary<int, Race> Races = new Dictionary<int, Race>();
        public readonly Dictionary<int, WrittenContent> WrittenContents = new Dictionary<int, WrittenContent>();
        public readonly Dictionary<int, PoeticForm> PoeticForms = new Dictionary<int, PoeticForm>();
        public readonly Dictionary<int, MusicalForm> MusicalForms = new Dictionary<int, MusicalForm>();
        public readonly Dictionary<int, DanceForm> DanceForms = new Dictionary<int, DanceForm>();
        public readonly Dictionary<int, Landmass> Landmasses = new Dictionary<int, Landmass>();
        public readonly Dictionary<int, Mountain> Mountains = new Dictionary<int, Mountain>();
        public readonly Dictionary<int, River> Rivers = new Dictionary<int, River>();
        public readonly Dictionary<int, Region> Regions = new Dictionary<int, Region>();
        public readonly Dictionary<int, UndergroundRegion> UndergroundRegions = new Dictionary<int, UndergroundRegion>();
        public readonly Dictionary<int, Site> Sites = new Dictionary<int, Site>();
        public readonly Dictionary<int, Structure> Structures = new Dictionary<int, Structure>();
        public readonly Dictionary<int, HistoricalFigure> HistoricalFigures = new Dictionary<int, HistoricalFigure>();
        public readonly Dictionary<int, EntityPopulation> EntityPopulations = new Dictionary<int, EntityPopulation>();
        public readonly Dictionary<int, Entity> Entities = new Dictionary<int, Entity>();
        public readonly Dictionary<int, HistoricalEvent> HistoricalEvents = new Dictionary<int, HistoricalEvent>();
        public readonly Dictionary<int, HistoricalEventCollection> HistoricalEventCollections = new Dictionary<int, HistoricalEventCollection>();
        public readonly Dictionary<int, HistoricalEra> HistoricalEras = new Dictionary<int, HistoricalEra>();
        public readonly Dictionary<int, WorldConstruction> WorldConstructions = new Dictionary<int, WorldConstruction>();
        public readonly Dictionary<int, Artifact> Artifacts = new Dictionary<int, Artifact>();

        public Dictionary<string, string> Maps { get; private set; }
        public Dictionary<string, MapLegend> MapLegends { get; private set; } 

        public readonly FilterSettings Filters;

        public Stats Stats;
        private VisualizationCollection _visualizations;
        #endregion

        public World(string historyPath, string sitesPath, string paramPath, string xmlPath, string xmlPlusPath, string mapPath, WorldTime worldGenTime)
        {
            LastYear = worldGenTime.Year;
            WorldTime.Present = worldGenTime;

            HistoryPath = historyPath;
            SitesPath = sitesPath;
            ParamPath = paramPath;
            MapPath = mapPath;
            XmlPath = xmlPath;
            XmlPlusPath = xmlPlusPath;
            
            HasPlusXml = File.Exists(xmlPlusPath);
            if (!HasPlusXml) //Check if used open-legends for extraxml
            {
                var directory = Path.GetDirectoryName(XmlPath);
                var saveName = xmlPlusPath.Replace(directory, "").Trim('\\').Split('-')[0];
                var xmlpluspaths = Directory.GetFiles(directory, "*legends_plus.xml");
                var xmlpluspath = xmlpluspaths.FirstOrDefault(x => x.StartsWith(Path.Combine(directory, saveName)));
                if (xmlpluspath != null)
                {
                    HasPlusXml = true;
                    XmlPlusPath = xmlpluspath;
                }
            }

            Filters = new FilterSettings(this);
        }


        #region World Loading
        public void LoadFiles()
        {
            LoadHistory();
            LoadSites();
            LoadMaps();
            LoadParam();
            Program.MainForm.FillNonXmlLists();
            LoadXml();

        }

        /// <summary>
        /// Given the name and path to the Basic Map, this will find and add another other maps found in teh same directory.
        /// </summary>
        private void LoadMaps()
        {
            Program.Log(LogType.Status, "Loading Maps");
            Maps = new Dictionary<string, string> {{"Main", MapPath}};
            var mapSymbols = new List<string>
            {"bm", "detailed", "dip", "drn", "el", "elw", "evil", "hyd", "nob", "rain",
                    "sal", "sav", "str", "tmp", "trd", "veg", "vol"};
            var mapNames = new List<string>
            {"Biome", "Standard+Biome", "Diplomacy", "Drainage", "Elevations", "Elevations w/Water", "Evil", 
                    "Hydrosphere", "Nobility", "Rainfall", "Sailinity", "Savagry", "Structures", 
                    "Temperature", "Trade", "Vegetation", "Volcanism"};

            for (var i = 0; i < mapSymbols.Count; i++)
            {
                var thisMap = MapPath.Replace("world_map", mapSymbols[i]);
                if (File.Exists(thisMap))
                    Maps.Add(mapNames[i], thisMap);
            }

            var mapLegendsName = new List<string>
                { "structure_color_key", "hydro_color_key", "biome_color_key"};

            MapLegends = new Dictionary<string, MapLegend>();
            foreach (var maplegendname in mapLegendsName)
            {
                var thisLegend = Path.Combine(Path.GetDirectoryName(MapPath), maplegendname) + ".txt";
                if (File.Exists(thisLegend))
                    MapLegends.Add(maplegendname, new MapLegend(thisLegend));
            }

            MapLegends.Add("site_color_key_dark", new MapLegend("site_color_key_dark", new List<string>
            {
                "main tower (255,0,255)",
                "watch tower (128,128,128)",
                "trenches (0,0,0)",
                "dungeon-tower interface (50,20,50)",
                "work pits (50,30,20)",
                "living pits (20,50,40)"
            }));

            MapLegends.Add("site_color_key", new MapLegend("site_color_key", new List<string>
            {
                "castle tower (110,110,110)",
                "castle wall (70,70,70)",
                "castle wall walkway (40,40,40)",
                "warehouse (50,50,50)",
                "general import market (50,30,15)",
                "food import market (50,40,15)",
                "clothing import market (50,30,35)",
                "meat market (255,0,0)",
                "edible produce market (0,255,0)",
                "cheese market (255,255,0)",
                "edible processed plants market (255,200,200)",
                "general import store (192,192,192), border (128,128,128)",
                "food import store (128,192,128), border (128,128,128)",
                "clothing import store (255,255,255), border (128,128,128)",
                "cloth shop (200,200,200)",
                "tanning shop (128,100,50)",
                "cloth clothing shop (255,255,100)",
                "leather clothing shop (255,128,64)",
                "bone carver shop (128,255,255)",
                "gem cutter shop (0,255,0)",
                "metal weapons shop (255,0,255)",
                "wood weapons shop (255,128,128)",
                "blacksmith shop (64,64,64)",
                "armorsmith shop (0,0,0)",
                "metal crafter shop (128,128,128)",
                "leather accessories shop (100,70,30)",
                "wooden furniture shop (255,0,0)",
                "stone furniture shop (255,64,0)",
                "metal furniture shop (255,0,64)",
                "fortress (129,129,129)",
                "underground farming (50,20,50)",
                "underground industrial (250,50,20)",
                "underground living (20,50,30)",
                "trenches (0,0,0)",
                "tavern (255,255,200)",
                "well (0,0,255)",
                "house (100,70,10)",
                "cottage plot (30,100,10)",
                "mead hall (255,128,255)",
                "temple (170,120,230)",
                "crops 1 (255,195,0), rows",
                "crops 2 (195,255,0), rows",
                "crops 3 (100,60,20), rows",
                "meadow (100,255,0)",
                "pasture (0,255,0)",
                "orchard (0,100,0)/(0,200,0)",
                "woodland (0,95,0)",
                "waste (0,20,0)",
                "town yards (100,50,10)",
                "abandoned (75,50,20), border (50,50,50)",
                "ruin (50,20,20), border (50,50,50)",
                "unknown (100,100,100)"
            }));

        }

        /// <summary>
        /// This splits up the paramters file and adds the lines as new Parameters.
        /// </summary>
        private void LoadParam()
        {
            Program.Log(LogType.Status, "Loading Params");
            var lines = File.ReadAllLines(ParamPath, Encoding.GetEncoding(437)).ToList();

            Version = lines[0].Substring(15);
            Version = Version.Substring(0, Version.Length - 1);

            lines.RemoveRange(0, 3);
            foreach (var line in lines)
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
            Program.Log(LogType.Status, "Loading Sites File");
            var lines = File.ReadAllLines(SitesPath, Encoding.GetEncoding(437)).ToList();

            lines.RemoveRange(0, 2);

            var curSite = new List<string>();
            foreach (var line in lines)
            {
                if (line == "" || line == "Sites" || line == "Outdoor Animal Populations (Including Undead)" || line == "Underground Animal Populations (Including Undead)")
                {
                    if (curSite.Count > 0)
                    {
                        var newSite = new Site(curSite, this);
                        SitesFile.Add(newSite.Id, newSite);
                        curSite.Clear();
                    }
                    continue;
                }
                if (line.StartsWith("\t") && curSite.Count == 0)
                {
                    if (line.Contains("Total: ")) continue;
                    var raceName = line.Trim().Substring(line.Trim().IndexOf(' ') + 1);
                    var thisRace = GetAddRace(raceName);
                    thisRace.Population = line.Contains("Unnumbered") ? long.MaxValue : Convert.ToInt32(line.Trim().Split(' ')[0]);
                }
                else if (!line.StartsWith("\t")) //Start of Site maybe
                {
                    if (curSite.Count > 0)
                    {
                        var newSite = new Site(curSite, this);
                        SitesFile.Add(newSite.Id, newSite);
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
            Program.Log(LogType.Status, "Loading History File");
            var lines = File.ReadAllLines(HistoryPath, Encoding.GetEncoding(437)).ToList();

            Name = lines[0];
            AltName = lines[1];
            Program.MainForm.InvokeEx(f =>
            {
                f.Text = $"World Viewer v{Application.ProductVersion} - {Name} \"{AltName}\"";
            });
            //Program.mainForm.Text = string.Format("World Viewer v{0} - {1} \"{2}\"", Application.ProductVersion, Name, AltName);

            lines.RemoveRange(0, 3);

            var curCiv = new List<string>();
            foreach (var line in lines)
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
            if (curCiv.Count <= 0) return;
            Civilizations.Add(new Civilization(curCiv, this));
            curCiv.Clear();
        }

        /// <summary>
        /// This creates a new thread from the DFXMLParser static class, to parse the XML.  
        /// Due to it being exceptionally complext having a separate thread and object made sense.
        /// See DFXMLParser.Parse() for details.
        /// </summary>
        private void LoadXml()
        {
            Program.Log(LogType.Status, "Loading XML");
            StartThread(() => DFXMLParser.Parse(this, XmlPath), "XML Parsing");
        }

        #endregion

        #region World Importing (WIP)

        //private string dbPath;

        //public World(string dbPath)
        //{
        //    this.dbPath = dbPath;
        //}

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
            var lname = raceName.ToLower();
            //lname = StripRaceNumbers(lname);
            lname = lname.Replace("_", " ");
            if (lname.Contains("prisoners"))
                lname = lname.Replace(" prisoners", "");
            else if (lname.Contains("prisoner"))
                lname = lname.Replace(" prisoner", "");
            else if (lname.Contains("outcasts"))
                lname = lname.Replace(" outcasts", "");
            else if (lname.Contains("outcast"))
                lname = lname.Replace(" outcast", "");

            if (ExistsRace(lname))
                return FindRace(lname);
            if (ExistsRace(lname.Remove(lname.Length - 1) + "ves"))
                return FindRace(lname.Remove(lname.Length - 1) + "ves");
            if (ExistsRace(lname + "s"))
                return FindRace(lname + "s");
            if (lname.Contains("men") && ExistsRace(lname.Replace("men", "man")))
                return FindRace(lname.Replace("men", "man"));
            if (lname.Contains("man") && ExistsRace(lname.Replace("man", "men")))
                return FindRace(lname.Replace("man", "men"));


            foreach (var race in Races.Values)
            {
                var thisRaceName = race.Name.ToLower();
                if (thisRaceName == lname)
                    return race;
                if (thisRaceName.Pluralize() == lname || lname.Singularize() == thisRaceName)
                    return race;
                if (thisRaceName.EndsWith("s") && thisRaceName.Remove(thisRaceName.Length - 1) == lname)
                    return race;
                if (thisRaceName.EndsWith("ves") && thisRaceName.Remove(thisRaceName.Length - 3) + "f" == lname)
                    return race;
                if (thisRaceName.Contains("men") && thisRaceName.Replace("men", "man") == lname)
                    return race;
                if (lname.Contains("men") && thisRaceName.Replace("men", "man") == lname)
                    return race;
            }

            if (ExistsRace(lname)) 
                return FindRace(lname);

            var newRace = new Race(lname, -(Races.Count + 1), this);
            Races.Add(-(Races.Count + 1), newRace);
            return newRace;
        }

        private bool ExistsRace(string name)
        {
            return FindRace(name) != null;
        }

        public Race FindRace(string name)
        {
            return Races.Values.FirstOrDefault(x => x.Key == name || x.Key.Replace("_"," ") == name || x.Name == name || x.PluralName == name);
        }

        internal God GetAddGod(God tempGod)
        {
            foreach (var god in Gods.Where(god => string.Equals(god.Name, tempGod.Name, StringComparison.CurrentCultureIgnoreCase)))
            {
                return god;
            }
            tempGod.Id = Gods.Count == 0 ? 0 : Gods.Max(x => x.Id) + 1;
            Gods.Add(tempGod);
            return tempGod;
        }

        internal God GetAddGod(string godName)
        {
            foreach (var god in Gods.Where(god => string.Equals(god.Name, godName, StringComparison.CurrentCultureIgnoreCase)))
            {
                return god;
            }
            var newGod = new God(godName) { Id = Gods.Count == 0 ? 0 : Gods.Max(x => x.Id) + 1 };
            Gods.Add(newGod);
            return newGod;

        }

        internal Civilization GetCiv(string civName)
        {
            foreach (var civ in Civilizations.Where(civ => civ.Name == civName))
            {
                return civ;
            }
            Program.Log(LogType.Warning, "Site Civ doesn't exist in history file/n" + civName); // 
            return null;

        }

        internal Entity GetAddEntity(string data)
        {
            var entityName = data.Split(',')[0].Trim();
            var entityRaceName = data.Split(',')[1].Trim();

            Race entityRace = null;
            if (entityRaceName != "")
                entityRace = GetAddRace(entityRaceName);

            foreach (var civ in Civilizations.Where(civ => civ.Name == entityName && civ.Race == entityRace))
            {
                return civ;
            }
            foreach (var entity in EntitiesFile.Where(entity => entity.Name == entityName).Where(entity => entity.Race == entityRace))
            {
                return entity;
            }
            var newEntity = new Entity(entityName, this) {Race = entityRace};
            EntitiesFile.Add(newEntity);
            return newEntity;
        }

        internal Leader GetAddLeader(string leaderName)
        {
            foreach (var leader in Leaders.Where(leader => string.Equals(leader.Name, leaderName, StringComparison.CurrentCultureIgnoreCase)))
            {
                return leader;
            }
            var newLeader = new Leader(leaderName);
            Leaders.Add(newLeader);
            return newLeader;
        }
        #endregion 

        /// <summary>
        /// These methods handle object linking, which generally is used to turn object IDs (from XML) into references to World Objects in code.
        ///     They raise events up to the MainForm to notify when things are finished.
        /// </summary>
        #region Data Linking
        
        public static event XmlLinkedSectionStartEventHandler LinkedSectionStart;
        public static event XmlLinkedSectionEventHandler LinkedSection;
        public static event XmlLinkedEventHandler Linked;

        internal void LinkXmlData()
        {
            StartThread(Link, "XML Linking");
        }

        private static void LinkSection<T>(IEnumerable<T> list, string sectionName) where T : XMLObject
        {

            OnLinkedSectionStart(sectionName);
            foreach (var item in list)
                item.Link();
            OnLinkedSection(sectionName);

        }

        private void Link()
        {

            LinkSection(Artifacts.Values, "Artifacts");
            LinkSection(Entities.Values, "Entities");
            LinkSection(EntityPopulations.Values, "Entity Populations");
            LinkSection(HistoricalEras.Values, "Historical Eras");
            LinkSection(HistoricalEvents.Values, "Historical Events");
            LinkSection(HistoricalEventCollections.Values, "Historical Event Collections");
            LinkSection(HistoricalFigures.Values, "Historical Figures");
            LinkSection(Regions.Values, "Regions");
            LinkSection(Sites.Values, "Sites");
            LinkSection(UndergroundRegions.Values, "Underground Regions");
            LinkSection(WorldConstructions.Values, "World Constructions");
            LinkSection(Rivers.Values, "Rivers");
            LinkSection(Mountains.Values, "Mountains");
            LinkSection(Squads.Values, "Squads");
            LinkSection(Plants.Values, "Plants");
            LinkSection(Items.Values, "Items");
            LinkSection(Constructions.Values, "Constructions");
            LinkSection(Buildings.Values, "Buildings");
            LinkSection(WrittenContents.Values, "WrittenContents");
            LinkSection(PoeticForms.Values, "PoeticForms");
            LinkSection(DanceForms.Values, "DanceForms");
            LinkSection(MusicalForms.Values, "MusicalForms");
            LinkSection(Reports.Values, "Reports");
            LinkSection(Engravings.Values, "Engravings");
            LinkSection(Units.Values, "Units");
            LinkSection(Armies.Values, "Armies");

            OnLinked();
        }

        private static void OnLinkedSectionStart(string section)
        {
            LinkedSectionStart?.Invoke(section);
        }

        private static void OnLinkedSection(string section)
        {
            LinkedSection?.Invoke(section);
        }

        private static void OnLinked()
        {
            Linked?.Invoke();
        }

        #endregion

        /// <summary>
        /// These methods handle object processing, which gathers additional information about objects including creating lists of things.
        ///     They raise events up to the MainForm to notify when things are finished.
        /// </summary>
        #region Data Processing
        public static event XmlProcessedSectionStartEventHandler ProcessedSectionStart;
        public static event XmlProcessedSectionEventHandler ProcessedSection;
        public static event XmlProcessedEventHandler Processed;
        public static event FamiliesCountedEventHandler FamiliesCounted;
        public static event DynastiesCreatedEventHandler DynastiesCreated;
        public static event EventsCountedEventHandler EventsCounted;
        public static event EventCollectionsEvaluatedEventHandler EventCollectionsEvaluated;
        public static event HistoricalFiguresPositionedEventHandler HistoricalFiguresPositioned;
        public static event StatsGatheredEventHandler StatsGathered;
        public static event VisualizationsCreatedEventHandler VisualizationsCreated;

        internal void ProcessXmlData()
        {
            StartThread(Process, "XML Processing");
        }

        private void Process()
        {
            ProcessSection(Regions.Values, "Regions");
            ProcessSection(Sites.Values, "Sites");
            ProcessSection(UndergroundRegions.Values, "Underground Regions");
            ProcessSection(WorldConstructions.Values, "World Constructions");
            ProcessSection(Artifacts.Values, "Artifacts");
            ProcessSection(Entities.Values, "Entities");
            ProcessSection(EntityPopulations.Values, "Entity Populations");
            ProcessSection(HistoricalEras.Values, "Historical Eras");
            ProcessSection(HistoricalFigures.Values, "Historical Figures");
            ProcessSection(HistoricalEvents.Values, "Historical Events");
            ProcessSection(HistoricalEventCollections.Values, "Historical Event Collections");
            ProcessSection(Rivers.Values, "Rivers");
            ProcessSection(Mountains.Values, "Mountains");

            OnProcessed();
        }

        private static void ProcessSection<T>(IEnumerable<T> list, string sectionName) where T : XMLObject
        {
            OnProcessedSectionStart(sectionName);
            foreach (var item in list)
                item.Process();
            OnProcessedSection(sectionName);

        }

        private static void OnProcessedSectionStart(string section)
        {
            ProcessedSectionStart?.Invoke(section);
        }


        private static void OnProcessedSection(string section)
        {
            ProcessedSection?.Invoke(section);
        }

        private static void OnProcessed()
        {
            Processed?.Invoke();
        }

        #endregion

        /// <summary>
        /// These methods handle Family processing, which counts families and dynasties to allow sorting by number of descendents/ancestors.
        ///     They raise events up to the MainForm to notify when things are finished.
        /// </summary>
        #region Family Processing
        internal void FamilyProcessing()
        {
            StartThread(CountFamilies, "Counting Familes");
            StartThread(CreateDynasties, "Creating Dynasties");
        }

        private void CountFamilies()
        {
            foreach (var hf in HistoricalFigures.Values)
                hf.CountAncestors();
            foreach (var hf in HistoricalFigures.Values)
                hf.CountDescendents();

            OnFamiliesCounted();
        }

        private static void OnFamiliesCounted()
        {
            FamiliesCounted?.Invoke();
        }

        /// <summary>
        /// Creates Dynasties given familial relationships from the XML and data on inheritance from the history file.
        /// </summary>
        private void CreateDynasties()
        {
            foreach (var civ in Civilizations)
            {
                foreach (var leaderList in civ.Leaders.Values)
                {
                    //king list
                    leaderList.Reverse();

                    Dynasty thisDynasty = null;
                    
                    for (var i = 0; i < leaderList.Count - 1; i++)
                    {
                        if (leaderList[i].Hf  != null &&
                            leaderList[i].InheritedFrom != null &&
                            leaderList[i + 1].Hf != null &&
                            leaderList[i].InheritedFrom == leaderList[i + 1].Hf)
                        {
                            //i and i+1 are in the same dynasty
                            if (thisDynasty == null)
                            {
                                thisDynasty = new Dynasty(this, leaderList[i].Hf, Leader.LeaderTypes[leaderList[i].LeaderType], civ);
                                thisDynasty.Members.Add(leaderList[i + 1].Hf);
                            }
                            else
                            {
                                thisDynasty.Members.Add(leaderList[i + 1].Hf);
                            }
                        }
                        else if (Leader.InheritanceTypes[leaderList[i].Inheritance] != "New Line" &&
                                 Leader.InheritanceTypes[leaderList[i].Inheritance] != "Original Line")
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
                    }
                    
                    leaderList.Reverse();
                }
            }

            OnDynastiesCreated();
        }

        private static void OnDynastiesCreated()
        {
            DynastiesCreated?.Invoke();
        }

        #endregion

        /// <summary>
        /// These methods handle getting event counts for every object, to make sorting by event count feasible
        /// </summary>
        #region Event Counting

        internal void CountEvents()
        {
            StartThread(CountingEvents, "Counting Events");
        }

        private void CountingEvents()
        {
            foreach (var evt in HistoricalEvents.Values)
            {
                foreach (var hf in evt.HFsInvolved.Where(x => x != null))
                    hf.EventCount++;
                foreach (var site in evt.SitesInvolved.Where(x => x != null))
                    site.EventCount++;
                foreach (var entity in evt.EntitiesInvolved.Where(x => x != null))
                    entity.EventCount++;
            }

            OnEventsCounted();
        }

        private static void OnEventsCounted()
        {
            EventsCounted?.Invoke();
        }

        #endregion

        /// <summary>
        /// These methods handle further evluation of Event Collections following all XML data processing.
        ///     They raise events up to the MainForm to notify when things are finished.
        /// </summary>
        #region Event Collection Evaluation
        internal void EventCollectionEvaluation()
        {
            StartThread(EvaluateEventCollections, "Evaluating Event Collections");
        }

        /// <summary>
        /// Evaluates event collections in detail.  Allowing events to be linked to additional pieces of information only available by analyzing the event collection they are a part of.
        /// </summary>
        private void EvaluateEventCollections()
        {
            // Only check Historical Event Collections that contain events
            foreach (var evtcol in HistoricalEventCollections.Values.Where(x => x.Event != null))
                evtcol.Evaluate();

            //TODO
            //Case: Any simple battle event in event col with following hf died.
            //Change: Tie simple battle event to death event, noting that one person died.

            OnEventCollectionsEvaluated();
        }

        private static void OnEventCollectionsEvaluated()
        {
            EventCollectionsEvaluated?.Invoke();
        }

        #endregion

        /// <summary>
        /// These methods handle assigning Historical Figures to coordinates, sites, or regions.
        ///     They raise events up to the MainForm to notify when things are finished.
        /// </summary>
        #region Historical Figures Positioning
        internal void HistoricalFiguresPositioning()
        {
            StartThread(PositionHistoricalFigures, "Positioning HFs");
        }

        /// <summary>
        /// Evaluates the last known change HF state for a HF.  We then set the location of that HF at their last known location.
        /// </summary>
        private void PositionHistoricalFigures()
        {

            foreach (var hf in HistoricalFigures.Values.Where(x => x.DiedEvent == null))
            {
                //Position off Site Links
                if (hf.SiteLinks?.Count > 0)
                {
                    if (hf.SiteLinks.Count > 1)
                    {
                        
                    }
                    else
                    {
                        hf.Site = hf.SiteLinks.First().Value.First().Site;
                        hf.Coords = hf.Site.Coords;
                    }
                }

                //Position off change hf state
                if (hf.Events.All(x => HistoricalEvent.Types[x.Type] != "change hf state"))
                    continue;
                var evt = (HE_ChangeHFState)hf.Events.Last(x => HistoricalEvent.Types[x.Type] == "change hf state");

                hf.Site = evt.Site ?? hf.Site;
                hf.Region = evt.Subregion;
                hf.Coords = evt.Coords == Point.Empty ? hf.Coords : evt.Coords;

                if (hf.Site != null)
                {
                    if (hf.Site.Inhabitants == null)
                        hf.Site.Inhabitants = new List<HistoricalFigure>();
                    if (!hf.Site.Inhabitants.Contains(hf))
                        hf.Site.Inhabitants.Add(hf);
                }
                if (hf.Region == null) continue;
                if (hf.Region.Inhabitants == null)
                    hf.Region.Inhabitants = new List<HistoricalFigure>();
                if (!hf.Region.Inhabitants.Contains(hf))
                    hf.Region.Inhabitants.Add(hf);
            }

            

            OnHistoricalFiguresPositioned();
        }

        private static void OnHistoricalFiguresPositioned()
        {
            HistoricalFiguresPositioned?.Invoke();
        }

        #endregion

        /// <summary>
        /// These methods handle gathering stats following all XML Processing.
        ///     They raise events up to the MainForm to notify when things are finished.
        /// </summary>
        #region Stats Gathering
        internal void StatsGathering()
        {
            StartThread(GatherStats, "Gathering Stats");
        }

        private void GatherStats()
        {
            Stats = new Stats(this);
            Stats.Gather();
            OnStatsGathered();
        }

        private static void OnStatsGathered()
        {
            StatsGathered?.Invoke();
        }

        #endregion


        /// <summary>
        /// These methods handle creation of visualization maps and follows Event Collection Evaluation.
        ///     They raise events up to the MainForm to notify when things are finished.
        /// </summary>
        #region Visualization Creation
        internal void VisualizationCreation()
        {
            StartThread(CreateVisualizations, "Creating Visualizations");
        }

        private void CreateVisualizations()
        {
            _visualizations = new VisualizationCollection(this);
            VisualizationCollection.Create();
            OnVisualizationsCreated();
        }

        private static void OnVisualizationsCreated()
        {
            VisualizationsCreated?.Invoke();
        }

        #endregion

        /// <summary>
        /// These methods all find existing site/entity/civ objects from the history/site files, and if they were also found in the XML 
        ///     the data from the history/site file is copied over and the old object removed.
        /// </summary>
        #region Merging History/Site file data with XML
        internal void MergeSites()
        {
            Program.Log(LogType.Status, "Site Merging...");
            foreach (var sf in SitesFile.Values)
            {
                if (Sites[sf.Id].Name == sf.AltName.ToLower())
                    Sites[sf.Id].MergeInSiteFile(sf);
                else if (Sites[sf.Id].Name == Program.CleanString(sf.AltName.ToLower()))
                    Sites[sf.Id].MergeInSiteFile(sf);
                else if (PartialNameMatch(Sites[sf.Id].Name, sf.AltName.ToLower()) > .5)
                    Sites[sf.Id].MergeInSiteFile(sf);
                else
                    Program.Log(LogType.Warning, "Site from site file doesn't exist in XML");
                    
            }
            SitesFile.Clear();
            Program.Log(LogType.Status, " Done");
        }

        internal void MergeEntities()
        {
            Program.Log(LogType.Status, "Entity Merging...");
            foreach (var ent in EntitiesFile)
            {
                var entname = ent.Name.ToLower();
                foreach (var entxml in Entities.Values.Where(entxml => entxml.Name == ent.Name.ToLower() && (entxml.Race == null || entxml.Race == ent.Race)))
                {
                    entxml.MergeInEntityFile(ent);
                    break;
                }
                if (!ent.EntityFileMerged)
                {
                    entname = Program.CleanString(entname);
                    foreach (var entxml in Entities.Values.Where(entxml => entxml.Name == entname))
                    {
                        entxml.MergeInEntityFile(ent);
                        break;
                    }
                }
                if (!ent.EntityFileMerged)
                    Program.Log(LogType.Warning, "Entity from file doesn't exist in XML");
            }
            EntitiesFile.Clear();
            Program.Log(LogType.Status, " Done");
        }

        internal void MergeCivs()
        {
            Program.Log(LogType.Status, "Civilization Merging...");
            foreach (var civ in Civilizations.Where(x => x.IsFull))
            {
                var civname = civ.Name.ToLower();
                if (civ.Color == Color.Empty)
                    civ.Color = Program.NextDistinctColor();
                foreach (var ent in Entities.Values.Where(ent => ent.Name == civname))
                {
                    civ.Entity = ent;
                    ent.Civilization = civ;
                    break;
                }
                if (civ.Entity == null)
                {
                    civname = Program.CleanString(civname);
                    foreach (var ent in Entities.Values.Where(ent => ent.Name == civname))
                    {
                        civ.Entity = ent;
                        ent.Civilization = civ;
                        break;
                    }
                }
                if (civ.Entity == null)
                    Program.Log(LogType.Warning, "Civ from file doesn't exist in XML");
            }
            Program.Log(LogType.Status, " Done");
        }
        #endregion


        /// <summary>
        /// These methods are used to match a leader/god to a historical figure, based on name comparisons and other data.
        /// </summary>
        #region Matching HFsToPeople
        internal void MatchHistoricalFiguresToPeople()
        {
            Program.Log(LogType.Status, "Historical Figure Matching...");

            foreach (var leader in Leaders)
            {
                var leadername = leader.Name.ToLower();
                foreach (var hf in HistoricalFigures.Values.Where(hf => hf.Name != null && hf.Name.ToLower() == leadername))
                {
                    leader.Hf = hf;
                    if (leader.InheritedFromSource != Leader.InheritanceSource.None)
                        leader.LinkInheritance();

                    hf.Leader = leader;
                    break;
                }
                if (leader.Hf == null)
                {
                    leadername = Program.CleanString(leadername);
                    foreach (var hf in HistoricalFigures.Values.Where(hf => hf.Name != null && hf.Name.ToLower() == leadername))
                    {
                        leader.Hf = hf;
                        if (leader.InheritedFromSource != Leader.InheritanceSource.None)
                            leader.LinkInheritance();

                        hf.Leader = leader;
                        break;
                    }
                }
                if (leader.Hf == null)
                {
                    foreach (var hf in HistoricalFigures.Values.Where(x =>
                        ((x.BirthYear < 0 && leader.Birth == null) || 
                         ((leader.Birth != null) && x.BirthYear == leader.Birth.Year)) &&
                        ((x.DeathYear == null && leader.Death  == WorldTime.Present) || 
                         (leader.Death != null && (x.DeathYear == leader.Death.Year)))
                        ).Where(hf => isPartialMatch(hf, leader)))
                    {
                        leader.Hf = hf;
                        if (leader.InheritedFromSource != Leader.InheritanceSource.None)
                            leader.LinkInheritance();
                        hf.Leader = leader;
                        break;
                    }
                }
                if (leader.Hf == null)
                    Program.Log(LogType.Warning, "Leaderfrom File not in XML: " + leader.Name);
            }
            foreach (var god in Gods)
            {
                var godname = god.Name.ToLower();
                foreach (var hf in HistoricalFigures.Values.Where(hf => hf.Name != null && hf.Name.ToLower() == godname))
                {
                    god.Hf = hf;
                    hf.God = god;
                    break;
                }
                if (god.Hf == null)
                {
                    godname = Program.CleanString(godname);
                    foreach (var hf in HistoricalFigures.Values.Where(hf => hf.Name != null && hf.Name.ToLower() == godname))
                    {
                        god.Hf = hf;
                        hf.God = god;
                        break;
                    }
                }
                if (god.Hf == null)
                {
                    //var q = HistoricalFigures.Values.Where(x => x.Name.Length == 3 && x.Name.Contains("le")).ToList();
                    foreach (var hf in HistoricalFigures.Values.Where(x => x.Deity || x.Force).Where(hf => isPartialMatch(hf, god)))
                    {
                        god.Hf = hf;
                        hf.God = god;
                        break;
                    }
                }
                if (god.Hf == null)
                    Program.Log(LogType.Warning, "God from File not in XML: " + god.Name);
            }

            Program.Log(LogType.Status, " Done");
        }

        private static bool isPartialMatch(HistoricalFigure hf, WorldObject leader)
        {
            var useFull = false;
            if (hf.Name == null)
                return false;
            if (hf.Name.Length == leader.Name.Length)
                useFull = true;
            else if (hf.ShortName == null || hf.ShortName.Length != leader.Name.Length)
                return false;
            var hfname = useFull ? hf.Name : hf.ShortName;
            return PartialNameMatch(hfname, leader.Name.ToLower()) > .5;
        }

        private static float PartialNameMatch(string a, string b)
        {
            var i = 0;
            if (a == null || b == null)
                return 0;
            var matches = a.Count(t => t == b[i++]);
            return matches / (float)a.Length;
        }

        private static bool isPartialMatch(HistoricalFigure hf, God god)
        {
            var useFull = false;
            if (hf.Name.Length == god.Name.Length)
                useFull = true;
            else if (hf.Name == "" || hf.ShortName.Length != god.Name.Length)
                return false;
            var hfname = useFull ? hf.Name : hf.ShortName;
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

            ExportWorldItems(Artifacts.Values, "artifact");
            ExportWorldItems(Entities.Values, "entity");
            ExportWorldItems(EntityPopulations.Values, "entitypopulation");
            ExportWorldItems(HistoricalFigures.Values, "historicalfigure"); //
            ExportWorldItems(HistoricalEras.Values, "historicalera");
            ExportWorldItems(HistoricalEvents.Values, "historicalevent"); //
            ExportWorldItems(HistoricalEventCollections.Values, "historicaleventcollection"); //
            ExportWorldItems(Parameters, "parameter");
            ExportWorldItems(Races.Values, "race");
            ExportWorldItems(Regions.Values, "region");
            ExportWorldItems(Sites.Values, "site");
            ExportWorldItems(Structures.Values, "structure");
            ExportWorldItems(UndergroundRegions.Values, "undergroundregion");
            ExportWorldItems(WorldConstructions.Values, "worldconstruction");
            ExportWorldItems(Rivers.Values, "river");
            ExportWorldItems(Mountains.Values, "mountain");
            ExportWorldItems(Leaders, "leader");
            ExportWorldItems(Gods, "god");
            ExportMaps();
            ExportWorldData();
            Database.CloseConnection();

            Program.MainForm.InvokeEx(f => f.exportWorldToolStripMenuItem.Visible = true);
        }

        private void ExportWorldData()
        {
            Database.BeginTransaction();

            Database.ExportWorldItem("World", new List<object> { "Name", LastYear.ToString() });
            Database.ExportWorldItem("World", new List<object> { "AltName", Name });
            Database.ExportWorldItem("World", new List<object> { "LastYear", AltName });

            Database.CommitTransaction();
        }

        private void ExportMaps()
        {
            foreach (var map in Maps)
            {
                Database.BeginTransaction();


                var vals = new List<object> { map.Key, Database.ImageToBlob(Image.FromFile(map.Value)) };

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
        private static void ExportWorldItems<T>(IEnumerable<T> arr, string table) where T : WorldObject
        {

            var iCounter = 0;
            foreach (var item in arr)
            {

                if (iCounter == 0)
                    Database.BeginTransaction();
                item.Export(table);
                iCounter++;
                if (iCounter < 500) continue;
                Database.CommitTransaction();
                iCounter = 0;
            }

            if (iCounter > 0)
                Database.CommitTransaction();
        }

        #endregion

        #region World Closing
        public void Dispose()
        {
            CloseActiveThreads();
            ClearStaticLists();
        }

        private static void CloseActiveThreads()
        {
            foreach (var thread in Threads)
            {
                if (thread.IsAlive)
                {
                    thread.Abort();
                }
                while (thread.IsAlive)
                {

                }
            }
            Threads = new List<Thread>();
        }

        private static void ClearStaticLists()
        {
            God.Types = new List<string>();
            Leader.LeaderTypes = new List<string>();
            Leader.InheritanceTypes = new List<string> {"Unknown", "Inherited", "New Line", "Original Line"};
            Region.Types = new List<string>();
            Site.Types = new List<string>();
            WorldConstruction.Types = new List<string>();
            Structure.Types = new List<string>();
            Structure.NumStructures = 0;

            EntityEntityLink.LinkTypes = new List<string>();
            EntitySiteLink.LinkTypes = new List<string>();

            Item.ItemTypes = new List<string>();
            Item.ItemSubTypes = new List<string>();
            Item.Materials = new List<string>();
            HistoricalEvent.Types = new List<string>();
            HistoricalEvent.MeetingResults = new List<string>();
            HistoricalEvent.MeetingTopics = new List<string>();
            HistoricalEvent.Buildings = new List<string>();
            HistoricalEvent_CultureCreatedBase.Reasons = new List<string>();
            HistoricalEvent_CultureCreatedBase.Circumstances = new List<string>();
            HE_ChangeHFState.States = new List<string>();
            HE_HFDied.Causes = new List<string>();
            HE_HFRelationshipDenied.Reasons = new List<string>();
            HE_HFRelationshipDenied.RelationshipStrings = new List<string>();
            HE_HFSimpleBattleEvent.SubTypes = new List<string>();
            HE_KnowledgeDiscovered.Knowledges = new List<string>();
            HE_MasterpieceItemImprovement.ImprovementTypes = new List<string>();

            HistoricalEventCollection.Types = new List<string>();

            HistoricalFigure.AssociatedTypes = new List<string>();
            HistoricalFigure.Castes = new List<string>();
            HistoricalFigure.Goals = new List<string>();
            HistoricalFigure.Interactions = new List<string>();
            HistoricalFigure.JourneyPets = new List<string>();
            HistoricalFigure.Spheres = new List<string>();
            HFEntityLink.LinkTypes = new List<string>();
            HFEntityLink.Positions = new List<string>();
            HFLink.LinkTypes = new List<string>();
            HFSkill.Skills = new List<string>();
            HFSiteLink.LinkTypes = new List<string>();

            WrittenContent.Forms = new List<string>();
            WrittenContent.Styles = new List<string>();
            Building.Flags = new List<string>();
            Building.BuildingTypes = new List<string>();
            Building.BuildingSubTypes = new List<string>();
            EntityPosition.PositionTitles = new List<string>();
            EntityPosition.LinkTypes = new List<string>();
            EntityPositionAssignment.LinkTypes = new List<string>();
            Item.Flags = new List<string>();
            ItemImprovement.ImprovementTypes = new List<string>();
            Race.Flags = new List<string>();
            Report.Types = new List<string>();
            Unit.Flags = new List<string>();
            Unit.JobTypes = new List<string>();
            Unit.Labors = new List<string>();
            Unit.HealthFlags = new List<string>();
            UnitInventoryItem.BodyParts = new List<string>();

        }
        #endregion

        public override string ToString()
        {
            return Name;
        }

        public void StartThread(Action action, string name = "Unnamed")
        {
            var threadStart = new ThreadStart(action);
            var thread = new Thread(threadStart) {IsBackground = true, Name = name};
            Threads.Add(thread);

            thread.Start();
        }
    }
}