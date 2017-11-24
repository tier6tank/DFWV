using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using DFWV.WorldClasses;
using DFWV.WorldClasses.HistoricalEventClasses;
using DFWV.WorldClasses.HistoricalEventCollectionClasses;
using Squad = DFWV.WorldClasses.Squad;

namespace DFWV
{
    public delegate void XmlFinishedSectionEventHandler(string section);
    public delegate void XmlFinishedEventHandler();

    public delegate void XmlStartedSectionEventHandler(string section);

    public static class DFXMLParser
    {

        private static string _path;
        public static bool MemoryFailureQuitParsing;

        static readonly Dictionary<string, string> MissingXmlElements = new Dictionary<string, string>();


        public static event XmlFinishedSectionEventHandler FinishedSection;
        public static event XmlFinishedEventHandler Finished;

        public static event XmlStartedSectionEventHandler StartedSection;

        private static bool _workflowDetected;
        private static bool _autochopDetected;

        private static void OnFinishedSection(string section)
        {
            FinishedSection?.Invoke(section);
        }

        private static void OnStartedSection(string section)
        {
            StartedSection?.Invoke(section);
        }

        private static void OnFinished()
        {
            Finished?.Invoke();
        }

        /// <summary>
        /// Parses a DF Legends XML file using XmlReader.  
        ///     Steps through each tag and upon finding one related to a known XML section type we fire off a call to LoadSection of T with the dictionary that we're going to fill from that XML section.
        /// </summary>
        public static void Parse(World world, string path)
        {
            _path = path;

            using (var streamReader = new StreamReader(path, Encoding.GetEncoding("ISO-8859-9")))
            using (var xReader = XmlReader.Create(streamReader))
            {
                xReader.Read();
                while (xReader.Read())
                {
                    if (xReader.NodeType == XmlNodeType.Whitespace)
                        continue;
                    if (xReader.Name != "df_world") continue;
                    while (xReader.Read())
                    {
                        if (xReader.NodeType == XmlNodeType.Whitespace)
                            continue;
                        if (xReader.NodeType == XmlNodeType.EndElement && xReader.Name == "df_world")
                            break;
                        var knownSection = true;
                        switch (xReader.Name)
                        {
                            case "regions":
                                LoadSection(world.Regions, world, xReader);
                                break;
                            case "underground_regions":
                                LoadSection(world.UndergroundRegions, world, xReader);
                                break;
                            case "sites":
                                LoadSection(world.Sites, world, xReader);
                                break;
                            case "world_constructions":
                                LoadSection(world.WorldConstructions, world, xReader);
                                break;
                            case "artifacts":
                                LoadSection(world.Artifacts, world, xReader);
                                break;
                            case "historical_figures":
                                LoadSection(world.HistoricalFigures, world, xReader);
                                break;
                            case "entity_populations":
                                LoadSection(world.EntityPopulations, world, xReader);
                                break;
                            case "entities":
                                LoadSection(world.Entities, world, xReader);
                                break;
                            case "historical_events":
                                LoadSection(world.HistoricalEvents, world, xReader);
                                break;
                            case "historical_event_collections":
                                LoadSection(world.HistoricalEventCollections, world, xReader);
                                break;
                            case "historical_eras":
                                LoadSection(world.HistoricalEras, world, xReader);
                                break;
                            case "written_contents":
                                LoadSection(world.WrittenContents, world, xReader);
                                break;
                            case "poetic_forms":
                                LoadSection(world.PoeticForms, world, xReader);
                                break;
                            case "musical_forms":
                                LoadSection(world.MusicalForms, world, xReader);
                                break;
                            case "dance_forms":
                                LoadSection(world.DanceForms, world, xReader);
                                break;
                            default:
                                Program.Log(LogType.Error, "Unknown XML Section: " + xReader.Name);
                                xReader.Skip();
                                knownSection = false;
                                break;
                        }
                        if (knownSection)
                            OnFinishedSection(xReader.Name);
                    }
                    break;
                }
            }
            if (world.HasPlusXml)
            {
                Program.Log(LogType.Status, "XML Loading Done");
                Program.Log(LogType.Status, "Plus XML Loading");
                PlusParse(world, world.XmlPlusPath);
            }
            else
                OnFinished();
        }


        /// <summary>
        /// Parses a DF Legends Plus XML file using XmlReader.  
        ///     Steps through each tag and upon finding one related to a known XML section type we fire off a call to LoadSection of T with the dictionary that we're going to fill from that XML section.
        /// </summary>
        public static void PlusParse(World world, string path)
        {
            _path = path;
            world.IsPlusParsing = true;

            using (var streamReader = new StreamReader(path, Encoding.GetEncoding("ISO-8859-9")))
            using (var xReader = XmlReader.Create(streamReader))
            {

                xReader.Read();
                while (xReader.Read())
                {
                    if (xReader.NodeType == XmlNodeType.Whitespace)
                        continue;
                    if (xReader.Name != "df_world") 
                        continue;
                    while (xReader.Read())
                    {
                        if (xReader.NodeType == XmlNodeType.Whitespace)
                            continue;
                        if (xReader.NodeType == XmlNodeType.EndElement && xReader.Name == "df_world")
                            break;
                        var knownSection = true;
                        switch (xReader.Name)
                        {
                            case "name":
                            case "altname":
                                xReader.Read();
                                xReader.Read();
                                knownSection = false;
                                break;
                            case "armies":
                                PlusLoadSection(world.Armies, world, xReader);
                                break;
                            case "units":
                                PlusLoadSection(world.Units, world, xReader);
                                break;
                            case "engravings":
                                PlusLoadSection(world.Engravings, world, xReader);
                                break;
                            case "reports":
                                PlusLoadSection(world.Reports, world, xReader);
                                break;
                            case "buildings":
                                PlusLoadSection(world.Buildings, world, xReader);
                                break;
                            case "constructions":
                                PlusLoadSection(world.Constructions, world, xReader);
                                break;
                            case "items":
                                PlusLoadSection(world.Items, world, xReader);
                                break;
                            case "plants":
                                PlusLoadSection(world.Plants, world, xReader);
                                break;
                            case "squads":
                                PlusLoadSection(world.Squads, world, xReader);
                                break;
                            case "races":
                                PlusLoadSection(world.Races, world, xReader);
                                SortRaces(world);
                                break;
                            case "written_contents":
                                PlusLoadSection(world.WrittenContents, world, xReader);
                                break;
                            case "poetic_forms":
                                PlusLoadSection(world.PoeticForms, world, xReader);
                                break;
                            case "musical_forms":
                                PlusLoadSection(world.MusicalForms, world, xReader);
                                break;
                            case "dance_forms":
                                PlusLoadSection(world.DanceForms, world, xReader);
                                break;
                            case "landmasses":
                                PlusLoadSection(world.Landmasses, world, xReader);
                                break;
                            case "mountain_peaks":
                                PlusLoadSection(world.Mountains, world, xReader);
                                break;
                            case "rivers":
                                PlusLoadSection(world.Rivers, world, xReader);
                                break;
                            case "regions":
                                PlusLoadSection(world.Regions, world, xReader);
                                break;
                            case "underground_regions":
                                PlusLoadSection(world.UndergroundRegions, world, xReader);
                                break;
                            case "sites":
                                PlusLoadSection(world.Sites, world, xReader);
                                break;
                            case "world_constructions":
                                PlusLoadSection(world.WorldConstructions, world, xReader);
                                break;
                            case "artifacts":
                                PlusLoadSection(world.Artifacts, world, xReader);
                                break;
                            case "historical_figures":
                                PlusLoadSection(world.HistoricalFigures, world, xReader);
                                break;
                            case "entity_populations":
                                PlusLoadSection(world.EntityPopulations, world, xReader);
                                break;
                            case "entities":
                                PlusLoadSection(world.Entities, world, xReader);
                                break;
                            case "historical_events":
                                PlusLoadSection(world.HistoricalEvents, world, xReader);
                                break;
                            case "historical_event_collections":
                                PlusLoadSection(world.HistoricalEventCollections, world, xReader);
                                break;
                            case "historical_eras":
                                PlusLoadSection(world.HistoricalEras, world, xReader);
                                break;
                            default:
                                Program.Log(LogType.Error, "Unknown XML Section: " + xReader.Name);
                                xReader.Skip();
                                knownSection = false;
                                break;
                        }
                        if (knownSection)
                            OnFinishedSection(xReader.Name);
                    }
                    break;
                }
            }
            OnFinished();
        }

        private static void SortRaces(World world)
        {
            var newRaceList = world.Races.Values.ToDictionary(race => race.Id);

            world.Races.Clear();

            foreach (var race in newRaceList.Values.OrderBy(x=>x.Id))
            {
                world.Races.Add(race.Id, race);
            }
        }

        /// <summary>
        /// Given a specific section of type T if we encounter an open tag at one level below we want to load a new object of type T, starting at that XML.
        /// </summary>
        private static void LoadSection<T>(IDictionary<int, T> worldList, World world, XmlReader xReader) where T : XMLObject
        {
            OnStartedSection(xReader.Name);
            while (xReader.Read())
            {
                
                if (xReader.NodeType == XmlNodeType.Whitespace)
                    continue;
                if (xReader.NodeType != XmlNodeType.EndElement && xReader.Depth == 2)
                {
                    if (!MemoryFailureQuitParsing)
                        LoadItem(worldList, world, xReader);
                    else
                        xReader.ReadSubtree();
                }
                else if (xReader.Depth >= 2)
                {

                }
                else if (xReader.NodeType == XmlNodeType.EndElement)
                    break;
                else
                    Program.Log(LogType.Error, "Unknown part of section xml/n" + xReader.Name);
            }
        }

        /// <summary>
        /// Given a specific section of type T if we encounter an open tag at one level below we want to load a new object of type T, starting at that XML.
        /// </summary>
        private static void PlusLoadSection<T>(IDictionary<int, T> worldList, World world, XmlReader xReader) where T : XMLObject
        {
            OnStartedSection(xReader.Name);
            while (xReader.Read())
            {

                if (xReader.NodeType == XmlNodeType.Whitespace)
                    continue;
                if (xReader.NodeType != XmlNodeType.EndElement && xReader.Depth == 2)
                {
                    if (!MemoryFailureQuitParsing)
                        PlusLoadItem(worldList, world, xReader);
                    else
                        xReader.ReadSubtree();
                }
                else if (xReader.Depth >= 2)
                {

                }
                else if (xReader.NodeType == XmlNodeType.EndElement)
                    break;
                else
                    Program.Log(LogType.Error, "Unknown part of section xml/n" + xReader.Name);
            }
        }

        /// <summary>
        /// Now at the open tag of a single object in our XML, we want to dump the entire contents into an XDocument.
        ///   If we have a historical event or historical event collection we want to use their factory methods to create a new version of those with the given XDocument.
        ///   For all other object types we want to use their constructor to make a new object of that type.
        ///   In any case we add the object after making it to the appropriate dictionary.
        /// Individual object reads are separated out to allow us to work past failing to load any specific XML item for some weird reason.
        /// </summary>
        private static void LoadItem<T>(IDictionary<int, T> worldList, World world, XmlReader xReader) where T : XMLObject
        {
            XDocument xdoc = null;
            try
            {
                xdoc = XDocument.Load(xReader.ReadSubtree());

                if (typeof(T) == typeof(HistoricalEvent))
                {
                    var evt = HistoricalEvent.Create(xdoc, world);
                    world.HistoricalEvents.Add(evt.Id, evt);
                }
                else if (typeof(T) == typeof(HistoricalEventCollection))
                {
                    var evtcol = HistoricalEventCollection.Create(xdoc, world);
                    world.HistoricalEventCollections.Add(evtcol.Id, evtcol);
                }
                else
                {
                    var worldObject = (T)Activator.CreateInstance(typeof(T), xdoc, world);
                    worldList.Add(worldObject.Id, worldObject);
                }
            }
            catch (OutOfMemoryException e)
            {
                Program.Log(LogType.Error, "Error reading XML item: id\n" + e.Message);


                var fi = new FileInfo(_path);
                Program.Log(LogType.Error, "XML file is" + Math.Round(fi.Length / 1024f / 1024f / 1024f, 2) + " GB");

                Program.Log(LogType.Error, $"Running {(Environment.Is64BitProcess ? "64" : "32")} Bit World Viewer");
                Program.Log(LogType.Error,
                    $"Running {(Environment.Is64BitOperatingSystem ? "64" : "32")} Bit Operating System");

                if (!Environment.Is64BitOperatingSystem) //Running 32 bit OS
                {
                    Program.Log(LogType.Error, "32 Bit World Viewer does not support Huge XML files");
                }
                else if (!Environment.Is64BitProcess) //Running 32 bit app in 64 bit OS
                {
                    Program.Log(LogType.Error, "Recommend using 64 Bit World Viewer");
                }
                else
                {
                    Program.Log(LogType.Error, "Please report Log");
                }


                MemoryFailureQuitParsing = true;
            }
            catch (Exception e)
            {
                try
                {
                    if (xdoc != null)
                    {
                        var id = int.Parse(((XElement)xdoc.Root.Nodes().ToArray()[1]).Value);

                        if (id < 0)
                        {
                            switch (xdoc.Root.Name.LocalName)
                            {
                                case "historical_event":
                                    if (!_workflowDetected)
                                    {
                                        Program.Log(LogType.Error,
                                            "Negative ID historical event.  Likely due to dfHack Workflow, ignoring\n" + xdoc);
                                        _workflowDetected = true;
                                    }
                                    break;
                                case "historical_figure":
                                    if (!_autochopDetected)
                                    {
                                        Program.Log(LogType.Error,
                                            "Negative ID historical figure detected. Likely due to autochop, ignoring\n" + xdoc);
                                        _autochopDetected = true;
                                    }
                                    break;
                                default:
                                    Program.Log(LogType.Error,
                                        "Negative ID " + xdoc.Root.Name.LocalName + " detected. Unknown cause, ignoring\n" + xdoc);
                                    break;
                            }
                        }
                        else
                            Program.Log(LogType.Error, "Error reading XML item: id\n" + e.Message);
                    }
                }
                catch (Exception)
                {
                    Program.Log(LogType.Error, "Error reading XML item: id\n" + e.Message);
                    throw;
                }

            }

            
            
        }

        /// <summary>
        /// Now at the open tag of a single object in our XML, we want to dump the entire contents into an XDocument.
        ///   If we have a World Construction it's a new item so it needs to be added, 
        ///   in other cases, it already exists and details need to be added to the existing items.
        /// Individual object reads are separated out to allow us to work past failing to load any specific XML item for any reason.
        /// </summary>
        private static void PlusLoadItem<T>(IDictionary<int, T> worldList, World world, XmlReader xReader) where T : XMLObject
        {
            XDocument xdoc = null;
            try
            {
                xdoc = XDocument.Load(xReader.ReadSubtree());

                var id = Convert.ToInt32(xdoc.Root.Element("id").Value);
                if (!worldList.ContainsKey(id))
                {
                    if (typeof(T) == typeof(WorldConstruction))
                    {
                        var newWc = new WorldConstruction(xdoc, world);
                        world.WorldConstructions.Add(newWc.Id, newWc);
                        return;
                    }
                    if (typeof(T) == typeof(Landmass))
                    {
                        var newLandmass = new Landmass(xdoc, world);
                        world.Landmasses.Add(newLandmass.Id, newLandmass);
                        return;
                    }
                    if (typeof(T) == typeof(Mountain))
                    {
                        var newMountain = new Mountain(xdoc, world);
                        world.Mountains.Add(newMountain.Id, newMountain);
                        return;
                    }
                    if (typeof(T) == typeof(River))
                    {
                        var newRiver = new River(xdoc, world);
                        world.Rivers.Add(newRiver.Id, newRiver);
                        return;
                    }
                    if (typeof(T) == typeof(Army))
                    {
                        var newArmy = new Army(xdoc, world);
                        world.Armies.Add(newArmy.Id, newArmy);
                        return;
                    }
                    if (typeof(T) == typeof(Unit))
                    {
                        var newUnit = new Unit(xdoc, world);
                        world.Units.Add(newUnit.Id, newUnit);
                        return;
                    }
                    if (typeof(T) == typeof(Engraving))
                    {
                        var newEngraving = new Engraving(xdoc, world);
                        world.Engravings.Add(newEngraving.Id, newEngraving);
                        return;
                    }
                    if (typeof(T) == typeof(Report))
                    {
                        var newReport = new Report(xdoc, world);
                        world.Reports.Add(newReport.Id, newReport);
                        return;
                    }
                    if (typeof(T) == typeof(Building))
                    {
                        var newBuilding = new Building(xdoc, world);
                        world.Buildings.Add(newBuilding.Id, newBuilding);
                        return;
                    }
                    if (typeof(T) == typeof(Construction))
                    {
                        var newConstruction = new Construction(xdoc, world);
                        world.Constructions.Add(newConstruction.Id, newConstruction);
                        return;
                    }
                    if (typeof(T) == typeof(Item))
                    {
                        var newItem = new Item(xdoc, world);
                        world.Items.Add(newItem.Id, newItem);
                        return;
                    }
                    if (typeof(T) == typeof(Plant))
                    {
                        var newPlant = new Plant(xdoc, world);
                        world.Plants.Add(newPlant.Id, newPlant);
                        return;
                    }
                    if (typeof(T) == typeof(Squad))
                    {
                        var newSquad = new Squad(xdoc, world);
                        world.Squads.Add(newSquad.Id, newSquad);
                        return;
                    }
                    if (typeof(T) == typeof(WrittenContent))
                    {
                        var newWrittenContent = new WrittenContent(xdoc, world);
                        world.WrittenContents.Add(newWrittenContent.Id, newWrittenContent);
                        return;
                    }
                    if (typeof(T) == typeof(PoeticForm))
                    {
                        var newPoeticForm = new PoeticForm(xdoc, world);
                        world.PoeticForms.Add(newPoeticForm.Id, newPoeticForm);
                        Console.WriteLine(newPoeticForm.Id);
                        return;
                    }
                    if (typeof(T) == typeof(MusicalForm))
                    {
                        var newMusicalForm = new MusicalForm(xdoc, world);
                        world.MusicalForms.Add(newMusicalForm.Id, newMusicalForm);
                        return;
                    }
                    if (typeof(T) == typeof(DanceForm))
                    {
                        var newDanceForm = new DanceForm(xdoc, world);
                        world.DanceForms.Add(newDanceForm.Id, newDanceForm);
                        return;
                    }

                }
                if (typeof(T) == typeof(Race))
                {
                    var key = xdoc.Root.Element("key").Value.ToLower();
                    var associatedRace = world.FindRace(key) ?? 
                                         world.FindRace(xdoc.Root.Element("nameS").Value.ToLower()) ?? 
                                         world.FindRace(xdoc.Root.Element("nameP").Value.ToLower());
                    
                    if (associatedRace == null || associatedRace.Id > 0)
                    {
                        var newRace = new Race(xdoc, world)
                        {
                            AddedOrder = world.Races.Keys.Min() - 1
                        };
                        world.Races.Add(id, newRace);
                        return;
                    }
                    id = associatedRace.AddedOrder;
                }
                if (worldList.ContainsKey(id))
                    worldList[id].Plus(xdoc);
            }
            catch (OutOfMemoryException e)
            {
                Program.Log(LogType.Error, "Error reading XML item: id\n" + e.Message);


                var fi = new FileInfo(_path);
                Program.Log(LogType.Error, "XML file is" + Math.Round(fi.Length / 1024f / 1024f / 1024f, 2) + " GB");

                Program.Log(LogType.Error, $"Running {(Environment.Is64BitProcess ? "64" : "32")} Bit World Viewer");
                Program.Log(LogType.Error,
                    $"Running {(Environment.Is64BitOperatingSystem ? "64" : "32")} Bit Operating System");

                if (!Environment.Is64BitOperatingSystem) //Running 32 bit OS
                {
                    Program.Log(LogType.Error, "32 Bit World Viewer does not support Huge XML files");
                }
                else if (!Environment.Is64BitProcess) //Running 32 bit app in 64 bit OS
                {
                    Program.Log(LogType.Error, "Recommend using 64 Bit World Viewer");
                }
                else
                {
                    Program.Log(LogType.Error, "Please report Log");
                }


                MemoryFailureQuitParsing = true;
            }
            catch (Exception e)
            {
                try
                {
                    if (xdoc != null)
                    {
                        var id = int.Parse(((XElement)xdoc.Root.Nodes().ToArray()[1]).Value);

                        if (id < 0)
                        {
                            switch (xdoc.Root.Name.LocalName)
                            {
                                case "historical_event":
                                    if (!_workflowDetected)
                                    {
                                        Program.Log(LogType.Error,
                                            "Negative ID historical event.  Likely due to dfHack Workflow, ignoring\n" + xdoc);
                                        _workflowDetected = true;
                                    }
                                    break;
                                case "historical_figure":
                                    if (!_autochopDetected)
                                    {
                                        Program.Log(LogType.Error,
                                            "Negative ID historical figure detected. Likely due to autochop, ignoring\n" + xdoc);
                                        _autochopDetected = true;
                                    }
                                    break;
                                default:
                                    Program.Log(LogType.Error,
                                        "Negative ID " + xdoc.Root.Name.LocalName + " detected. Unknown cause, ignoring\n" + xdoc);
                                    break;
                            }
                        }
                        else
                            Program.Log(LogType.Error, "Error reading XML item: id\n" + e.Message);
                    }
                }
                catch (Exception)
                {
                    Program.Log(LogType.Error, "Error reading XML item: id\n" + e.Message);
                    throw;
                }

            }



        }

        /// <summary>
        /// While parsing individual objects XML if an original element is discovered it will be reported in the log, but only one time per new element.
        /// </summary>
        internal static void UnexpectedXmlElement(string elementType, XElement problemElement, string xml)
        {
            if (MissingXmlElements.ContainsKey(elementType + "-" + problemElement.Name.LocalName)) return;

            var result = problemElement.Nodes().Aggregate("", (current, node) => current + node.ToString());
            Program.Log(LogType.Warning, "New XML!" + elementType + "\t" + problemElement.Name.LocalName + "\t" + result.Trim());
#if DEBUG
            Console.WriteLine(xml);
#endif
            MissingXmlElements.Add(elementType + "-" + problemElement.Name.LocalName, problemElement.Value);
        }
    }


}
