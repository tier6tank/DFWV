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

namespace DFWV
{
    public delegate void XMLFinishedSectionEventHandler(string section);
    public delegate void XMLFinishedEventHandler();

    public delegate void XMLStartedSectionEventHandler(string section);

    static class DFXMLParser
    {

        private static string _path;
        public static bool MemoryFailureQuitParsing;

        static readonly Dictionary<string, string> MissingXMLElements = new Dictionary<string, string>();


        public static event XMLFinishedSectionEventHandler FinishedSection;
        public static event XMLFinishedEventHandler Finished;

        public static event XMLStartedSectionEventHandler StartedSection;

        private static bool workflowDetected;
        private static bool autochopDetected;

        private static void OnFinishedSection(string section)
        {
            if (FinishedSection != null)
                FinishedSection(section);
        }

        private static void OnStartedSection(string section)
        {
            if (StartedSection != null)
                StartedSection(section);
        }
        private static void OnFinished()
        {
            if (Finished != null)
                Finished();
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
            if (world.hasPlusXML)
            {
                Program.Log(LogType.Status, "XML Loading Done");
                Program.Log(LogType.Status, "Plus XML Loading");
                PlusParse(world, world.xmlPlusPath);
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
            world.isPlusParsing = true;

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
                            case "races":
                                PlusLoadSection(world.Races, world, xReader);
                                SortRaces(world);
                                break;
                            case "mountains":
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
            Dictionary<int, Race> newRaceList = world.Races.Values.ToDictionary(race => race.ID);

            world.Races.Clear();

            foreach (var race in newRaceList.Values.OrderBy(X=>X.ID))
            {
                world.Races.Add(race.ID, race);
            }
        }


        /// <summary>
        /// Given a specific section of type T if we encounter an open tag at one level below we want to load a new object of type T, starting at that XML.
        /// </summary>
        private static void LoadSection<T>(IDictionary<int, T> WorldList, World world, XmlReader xReader) where T : XMLObject
        {
            OnStartedSection(xReader.Name);
            while (xReader.Read())
            {
                
                if (xReader.NodeType == XmlNodeType.Whitespace)
                    continue;
                if (xReader.NodeType != XmlNodeType.EndElement && xReader.Depth == 2)
                {
                    if (!MemoryFailureQuitParsing)
                        LoadItem(WorldList, world, xReader);
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
        private static void PlusLoadSection<T>(IDictionary<int, T> WorldList, World world, XmlReader xReader) where T : XMLObject
        {
            OnStartedSection(xReader.Name);
            while (xReader.Read())
            {

                if (xReader.NodeType == XmlNodeType.Whitespace)
                    continue;
                if (xReader.NodeType != XmlNodeType.EndElement && xReader.Depth == 2)
                {
                    if (!MemoryFailureQuitParsing)
                        PlusLoadItem(WorldList, world, xReader);
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
        private static void LoadItem<T>(IDictionary<int, T> WorldList, World world, XmlReader xReader) where T : XMLObject
        {
            XDocument xdoc = null;
            try
            {
                xdoc = XDocument.Load(xReader.ReadSubtree());

                if (typeof(T) == typeof(HistoricalEvent))
                {
                    var evt = HistoricalEvent.Create(xdoc, world);
                    world.HistoricalEvents.Add(evt.ID, evt);
                }
                else if (typeof(T) == typeof(HistoricalEventCollection))
                {
                    var evtcol = HistoricalEventCollection.Create(xdoc, world);
                    world.HistoricalEventCollections.Add(evtcol.ID, evtcol);
                }
                else
                {
                    var WorldObject = (T)Activator.CreateInstance(typeof(T), xdoc, world);
                    WorldList.Add(WorldObject.ID, WorldObject);
                }
            }
            catch (OutOfMemoryException e)
            {
                Program.Log(LogType.Error, "Error reading XML item: id\n" + e.Message);


                var fi = new FileInfo(_path);
                Program.Log(LogType.Error, "XML file is" + Math.Round(fi.Length / 1024f / 1024f / 1024f, 2) + " GB");

                Program.Log(LogType.Error, string.Format("Running {0} Bit World Viewer", (Environment.Is64BitProcess ? "64" : "32")));
                Program.Log(LogType.Error, string.Format("Running {0} Bit Operating System", (Environment.Is64BitOperatingSystem ? "64" : "32")));

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
                        var id = Int32.Parse(((XElement)xdoc.Root.Nodes().ToArray()[1]).Value);

                        if (id < 0)
                        {
                            switch (xdoc.Root.Name.LocalName)
                            {
                                case "historical_event":
                                    if (!workflowDetected)
                                    {
                                        Program.Log(LogType.Error,
                                            "Negative ID historical event.  Likely due to dfHack Workflow, ignoring\n" + xdoc);
                                        workflowDetected = true;
                                    }
                                    break;
                                case "historical_figure":
                                    if (!autochopDetected)
                                    {
                                        Program.Log(LogType.Error,
                                            "Negative ID historical figure detected. Likely due to autochop, ignoring\n" + xdoc);
                                        autochopDetected = true;
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
        private static void PlusLoadItem<T>(IDictionary<int, T> WorldList, World world, XmlReader xReader) where T : XMLObject
        {
            XDocument xdoc = null;
            try
            {
                xdoc = XDocument.Load(xReader.ReadSubtree());

                var id = Convert.ToInt32(xdoc.Root.Element("id").Value);
                if (!WorldList.ContainsKey(id))
                {
                    if (typeof(T) == typeof(WorldConstruction))
                    {
                        var newWC = new WorldConstruction(xdoc, world);
                        world.WorldConstructions.Add(newWC.ID, newWC);
                        return;
                    }
                    else if (typeof(T) == typeof(Mountain))
                    {
                        var newMountain = new Mountain(xdoc, world);
                        world.Mountains.Add(newMountain.ID, newMountain);
                        return;
                    }
                    else if (typeof(T) == typeof(River))
                    {
                        var newRiver = new River(xdoc, world);
                        world.Rivers.Add(newRiver.ID, newRiver);
                        return;
                    }
                }
                if (typeof(T) == typeof(Race))
                {
                    var key = xdoc.Root.Element("key").Value.ToLower();
                    var associatedRace = world.FindRace(key) ?? 
                                         world.FindRace(xdoc.Root.Element("nameS").Value.ToLower()) ?? 
                                         world.FindRace(xdoc.Root.Element("nameP").Value.ToLower());
                    
                    if (associatedRace == null || associatedRace.ID > 0)
                    {
                        var newRace = new Race(xdoc, world);
                        world.Races.Add(id, newRace);
                        return;
                    }
                    id = associatedRace.ID;
                }
                WorldList[id].Plus(xdoc);
            }
            catch (OutOfMemoryException e)
            {
                Program.Log(LogType.Error, "Error reading XML item: id\n" + e.Message);


                var fi = new FileInfo(_path);
                Program.Log(LogType.Error, "XML file is" + Math.Round(fi.Length / 1024f / 1024f / 1024f, 2) + " GB");

                Program.Log(LogType.Error, string.Format("Running {0} Bit World Viewer", (Environment.Is64BitProcess ? "64" : "32")));
                Program.Log(LogType.Error, string.Format("Running {0} Bit Operating System", (Environment.Is64BitOperatingSystem ? "64" : "32")));

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
                        var id = Int32.Parse(((XElement)xdoc.Root.Nodes().ToArray()[1]).Value);

                        if (id < 0)
                        {
                            switch (xdoc.Root.Name.LocalName)
                            {
                                case "historical_event":
                                    if (!workflowDetected)
                                    {
                                        Program.Log(LogType.Error,
                                            "Negative ID historical event.  Likely due to dfHack Workflow, ignoring\n" + xdoc);
                                        workflowDetected = true;
                                    }
                                    break;
                                case "historical_figure":
                                    if (!autochopDetected)
                                    {
                                        Program.Log(LogType.Error,
                                            "Negative ID historical figure detected. Likely due to autochop, ignoring\n" + xdoc);
                                        autochopDetected = true;
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
        internal static void UnexpectedXMLElement(string ElementType, XElement ProblemElement, string XML)
        {
            if (MissingXMLElements.ContainsKey(ElementType + "-" + ProblemElement.Name.LocalName)) return;

            var result = ProblemElement.Nodes().Aggregate("", (current, node) => current + node.ToString());
            Program.Log(LogType.Warning, "New XML!" + ElementType + "\t" + ProblemElement.Name.LocalName + "\t" + result.Trim());
#if DEBUG
            Console.WriteLine(XML);
#endif
            MissingXMLElements.Add(ElementType + "-" + ProblemElement.Name.LocalName, ProblemElement.Value);
        }

    }
}
