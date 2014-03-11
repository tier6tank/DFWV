namespace DFWV
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Windows.Forms;
    using System.Xml;
    using System.Xml.Linq;
    using DFWV.WorldClasses;
    using DFWV.WorldClasses.HistoricalEventClasses;
    using DFWV.WorldClasses.HistoricalEventCollectionClasses;
    using DFWV.WorldClasses.HistoricalFigureClasses;

    public delegate void XMLFinishedSectionEventHandler(string section);
    public delegate void XMLFinishedEventHandler();

    static class DFXMLParser
    {
        private static World World;
        private static string Path;
        static XmlReader XReader;
        static Dictionary<string, string> MissingXMLElements = new Dictionary<string, string>();
        public static bool isFinished = true;


        public static event XMLFinishedSectionEventHandler FinishedSection;
        public static event XMLFinishedEventHandler Finished;

        public static void OnFinishedSection(string section)
        {
            if (FinishedSection != null)
                FinishedSection(section);
        }

        public static void OnFinished()
        {
            if (Finished != null)
                Finished();
        }

        /// <summary>
        /// Parses a DF Legends XML file using XmlReader.  
        ///     Steps through each tag and upon finding one related to a known XML section type we fire off a call to LoadSection<T>() with the dictionary that we're going to fill from that XML section.
        /// </summary>
        public static void Parse(World world, string path)
        {
            isFinished = false;

            World = world;
            Path = path;

            XReader = XmlReader.Create(new StreamReader(Path, Encoding.GetEncoding("ISO-8859-9")));

            XReader.Read();
            while (XReader.Read())
            {
                if (XReader.NodeType == XmlNodeType.Whitespace)
                    continue;
                if (XReader.Name == "df_world")
                {
                    while (XReader.Read())
                    {
                        if (XReader.NodeType == XmlNodeType.Whitespace)
                            continue;
                        if (XReader.NodeType == XmlNodeType.EndElement && XReader.Name == "df_world")
                            break;
                        bool knownSection = true;
                        switch (XReader.Name)
                        {
                            case "regions":
                                LoadSection<Region>(World.Regions);
                                break;
                            case "underground_regions":
                                LoadSection<UndergroundRegion>(World.UndergroundRegions);
                                break;
                            case "sites":
                                LoadSection<Site>(World.Sites);
                                break;
                            case "world_constructions":
                                LoadSection<WorldConstruction>(World.WorldConstructions);
                                break;
                            case "artifacts":
                                LoadSection<Artifact>(World.Artifacts);
                                break;
                            case "historical_figures":
                                LoadSection<HistoricalFigure>(World.HistoricalFigures);
                                break;
                            case "entity_populations":
                                LoadSection<EntityPopulation>(World.EntityPopulations);
                                break;
                            case "entities":
                                LoadSection<Entity>(World.Entities);
                                break;
                            case "historical_events":
                                LoadSection<HistoricalEvent>(World.HistoricalEvents);
                                break;
                            case "historical_event_collections":
                                LoadSection<HistoricalEventCollection>(World.HistoricalEventCollections);
                                break;
                            case "historical_eras":
                                LoadSection<HistoricalEra>(World.HistoricalEras);
                                break;
                            default:
                                Program.Log(LogType.Error, "Unknown XML Section: " + XReader.Name);
                                XReader.Skip();
                                knownSection = false;
                                break;
                        }
                        if (knownSection)
                            OnFinishedSection(XReader.Name);
                    }
                    break;
                }
            }
            isFinished = true;
            OnFinished();
        }

        /// <summary>
        /// Given a specific section of type T if we encounter an open tag at one level below we want to load a new object of type T, starting at that XML.
        /// </summary>
        private static void LoadSection<T>(Dictionary<int, T> WorldList) where T : XMLObject
        {
            while (XReader.Read())
            {
                if (XReader.NodeType == XmlNodeType.Whitespace)
                    continue;
                if (XReader.NodeType != XmlNodeType.EndElement && XReader.Depth == 2)
                    LoadItem<T>(WorldList);
                else if (XReader.Depth >= 2)
                    continue;
                else if (XReader.NodeType == XmlNodeType.EndElement)
                    break;
                else
                    Program.Log(LogType.Error, "Unknown part of section xml/n" + XReader.Name);
            }
        }

        /// <summary>
        /// Now at the open tag of a single object in our XML, we want to dump the entire contents into an XDocument.
        ///   If we have a historical event or historical event collection we want to use their factory methods to create a new version of those with the given XDocument.
        ///   For all other object types we want to use their constructor to make a new object of that type.
        ///   In any case we add the object after making it to the appropriate dictionary.
        /// Individual object reads are separated out to allow us to work past failing to load any specific XML item for some weird reason.
        /// </summary>
        private static void LoadItem<T>(Dictionary<int, T> WorldList) where T : XMLObject
        {
            try
            {
                XDocument xdoc = XDocument.Load(XReader.ReadSubtree());
                if (typeof(T)  == typeof(HistoricalEvent))
                {
                    HistoricalEvent evt = HistoricalEvent.Create(xdoc, World);
                    World.HistoricalEvents.Add(evt.ID, evt);
                }
                else if (typeof(T) == typeof(HistoricalEventCollection))
                {
                    HistoricalEventCollection evtcol = HistoricalEventCollection.Create(xdoc, World);
                    World.HistoricalEventCollections.Add(evtcol.ID, evtcol);
                }
                else
                {
                    T WorldObject = (T)Activator.CreateInstance(typeof(T), new object[] { xdoc, World });
                    WorldList.Add(WorldObject.ID, WorldObject);
                }
                
            }
            catch (Exception e )
            {
                Program.Log(LogType.Error, "Error reading XML item\n" + e.Message);
            }

        }


        /// <summary>
        /// While parsing individual objects XML if an original element is discovered it will be reported in the log, but only one time per new element.
        /// </summary>
        internal static void UnexpectedXMLElement(string ElementType, XElement ProblemElement, string XML)
        {

            if (!MissingXMLElements.ContainsKey(ElementType + "-" + ProblemElement.Name.LocalName))
            {

                Program.Log(LogType.Warning, "New XML!" + ElementType + "\t" + ProblemElement.Name.LocalName + "\t" + ProblemElement.Nodes().Aggregate("", (b, node) => b += node.ToString()).Trim());
                MissingXMLElements.Add(ElementType + "-" + ProblemElement.Name.LocalName, ProblemElement.Value.ToString());
            }

        }

    }
}
