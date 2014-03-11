using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using DFWV.WorldClasses;
using DFWV.WorldClasses.HistoricalEventClasses;
using DFWV.WorldClasses.HistoricalEventCollectionClasses;
using DFWV.WorldClasses.HistoricalFigureClasses;
using Drawing = System.Drawing;

namespace DFWV
{
    public partial class MainForm : Form
    {
        /// <summary>
        /// The Root object which describes everything about the DF world.
        /// </summary>
        internal World World;

        /// <summary>
        /// Stores the dynamically generated controls for the currently viewed Historical Event, whereever it's viewed.
        /// </summary>
        public List<Control> EventDetailControls = new List<Control>();

        /// <summary>
        /// Allows backwards navigation through World Objects. (See Navigation Methods Region)
        /// </summary>
        public Stack<WorldObject> ViewedObjects = new Stack<WorldObject>(); /// 

        public MainForm()
        {
            InitializeComponent();

            LinkEvents();
            ClearTabs();
        }

        /// <summary>
        /// Displays box for searching for image map file to represent the world.
        /// The map is used because it's straightforward to find other related files for the world from the map, 
        ///     but not vice versa (because the world map file name contains extra data)
        /// After finding the map file it calls World.LoadFiles() which does all the initial loading.
        /// Events are subscribed which call XMLSectionFinished() and XMLFinished() which do post-loading processing of various sorts.
        /// </summary>
        private void loadWorldToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string selectedFile = AutoFindFiles();

            if (selectedFile == "")
            { 
                OpenFileDialog openFileDiag = new OpenFileDialog();

                openFileDiag.InitialDirectory = Program.GetDefaultPath();
                openFileDiag.Filter = "DF World Maps (*.bmp, *.png)|*.bmp;*.png";//|World Viewer Export (*.sqlite3)|*.sqlite3";

            
                openFileDiag.ShowDialog();

                if (openFileDiag.FileName != "")
                    selectedFile = openFileDiag.FileName;
            }

            if (selectedFile != "")
            {
                //if (Path.GetExtension(filename) != ".sqlite3")
                LoadFromFiles(selectedFile);
                //else
                //    LoadFromDB(filename);
            }
        }

        

        private string AutoFindFiles()
        {
            try
            {
                string selectedFile = "";

                string workingFolder = Program.GetDefaultPath();
                

                foreach (string file in Directory.GetFiles(workingFolder))
                {
                    if ((Path.GetExtension(file) == ".bmp" || Path.GetExtension(file) == ".png") && Path.GetFileNameWithoutExtension(file).StartsWith("world_map-"))
                    {
                        string mapFile = Path.GetFileNameWithoutExtension(file);
                        List<string> mapSplit = mapFile.Split("-".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).ToList<string>();

                        string tempName = mapSplit[1];

                        string xmlPath = workingFolder + "\\" + tempName + "-legends.xml";
                        string paramPath = workingFolder + "\\" + tempName + "-world_gen_param.txt";
                        string historyPath = workingFolder + "\\" + tempName + "-world_history.txt";
                        string sitesPath = workingFolder + "\\" + tempName + "-world_sites_and_pops.txt";

                        if (File.Exists(xmlPath) && File.Exists(paramPath) && File.Exists(historyPath) && File.Exists(sitesPath))
                        {
                            if (selectedFile == "")
                                selectedFile = file;
                            else
                            { //In case of multiple possibilities, make the user decide.
                                selectedFile = "";
                                break;
                            }
                        }
                        else
                            continue;

                    }
                }
                return selectedFile;
            }
            catch (Exception)
            {
                return "";
            }

            
            
        }

        private void LoadFromFiles(string filename)
        {
            string mapPath = filename;
            string path = Path.GetDirectoryName(mapPath) + "\\";
            if (mapPath.Contains("_graphic-")) //Picked wrong image
            {
                string thisFile = Path.GetFileName(mapPath);
                List<string> restofFile = thisFile.Split('-').ToList();
                int year;
                if (Int32.TryParse(restofFile[2], out year))
                    restofFile.RemoveRange(0, 1);
                else
                    restofFile.RemoveRange(0, 2);
                thisFile = String.Join("-", restofFile);
                mapPath = path + "world_map-" + thisFile;
            }
            if (!File.Exists(mapPath))
            {
                MessageBox.Show("Couldn't find basic map image");
                return;
            }

            string mapFile = Path.GetFileNameWithoutExtension(mapPath);
            List<string> mapSplit = mapFile.Split("-".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).ToList<string>();

            mapSplit.RemoveAt(0);
            mapSplit.RemoveAt(mapSplit.Count - 1);

            int Year = Convert.ToInt32(mapSplit[mapSplit.Count - 1]);

            mapSplit.RemoveAt(mapSplit.Count - 1);

            string name = string.Join("-", mapSplit);


            string xmlPath = path + name + "-legends.xml";
            string paramPath = path + name + "-world_gen_param.txt";
            string historyPath = path + name + "-world_history.txt";
            string sitesPath = path + name + "-world_sites_and_pops.txt";

            if (File.Exists(xmlPath) && File.Exists(paramPath) && File.Exists(historyPath) && File.Exists(sitesPath))
            {
                DFXMLParser.FinishedSection += new XMLFinishedSectionEventHandler(XMLSectionFinished);
                DFXMLParser.Finished += new XMLFinishedEventHandler(XMLFinished);

                Program.Log(LogType.Status, "Loading World...");

                Application.DoEvents();

                World = new World(historyPath, sitesPath, paramPath, xmlPath, mapPath, Year);

                loadWorldToolStripMenuItem.Visible = false;

                World.LoadFiles();
                FillNonXMLLists();
            }
            else
                MessageBox.Show("Files not found.  Please make sure all 5 files Legends files are located with the selected map file. See the README.txt file included for details.", "World Viewer",MessageBoxButtons.OK);



        }

        //private void LoadFromDB(string filename)
        //{

        //    //DFXMLParser.FinishedSection += new XMLFinishedSectionEventHandler(XMLSectionFinished);
        //    //DFXMLParser.Finished += new XMLFinishedEventHandler(XMLFinished);

        //    Program.Log(LogType.Status, "Loading World...");

        //    Application.DoEvents();

        //    World = new World(filename);

        //    loadWorldToolStripMenuItem.Visible = false;

        //    World.Import();
        //    FillNonXMLLists();
        //}

        /// <summary>
        /// Loads and displays the map form.
        /// </summary>
        private void showMapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Program.mapForm == null || Program.mapForm.IsDisposed)
                Program.mapForm = new MapForm(World);
            Program.mapForm.Location = this.Location;
            Program.mapForm.Show();
        }

        /// <summary>
        /// Loads and displays the map form.
        /// </summary>
        private void timelinetoolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Program.timelineForm == null || Program.timelineForm.IsDisposed)
                Program.timelineForm = new TimelineForm(World);
            Program.timelineForm.Location = this.Location;
            Program.timelineForm.Show();
        }

        /// <summary>
        /// Loads and displays the stats form.
        /// </summary>
        private void statsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Program.statsForm == null || Program.statsForm.IsDisposed)
                Program.statsForm = new StatsForm(World);
            Program.statsForm.Location = this.Location;
            Program.statsForm.Show();
        }

        /// <summary>
        /// Fires off the Export method of the World Object on a new thread, which outputs all relevant world data to a SQLite database.
        /// </summary>
        private void exportWorldToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDiag = new SaveFileDialog();

            saveFileDiag.InitialDirectory = Application.StartupPath;
            saveFileDiag.Filter = "World Viewer Export (*.sqlite3)|*.sqlite3";
            saveFileDiag.FileName = World.Name + " Export.sqlite3";

            saveFileDiag.ShowDialog();

            if (saveFileDiag.FileName != "")
            {
                string filename = saveFileDiag.FileName;
                if (Path.GetExtension(filename) != ".sqlite3")
                    filename = Path.GetFileNameWithoutExtension(filename) + ".sqlite3";

                if (File.Exists(filename))
                    File.Delete(filename);
                File.Copy(Application.StartupPath + @"\DFWV_Template.sqlite3.backup",filename);

                exportWorldToolStripMenuItem.Visible = false;
                Thread XMLThread = new Thread(() => World.Export(filename));

                XMLThread.Start();

            }



        }

        /// <summary>
        /// Cleanly empties EventDetailControls, which stores controls related to displaying the current event.  
        /// Called normally before displaying a new event.
        /// </summary>
        internal void ClearEventDetails()
        {
            foreach (Control ctrl in EventDetailControls)
            {
                if (ctrl.Parent != null)
                    ctrl.Parent.Controls.Remove(ctrl);
                ctrl.Dispose();
            }
            EventDetailControls.Clear();

        }

        /// <summary>
        /// Removes all but the World tab from the tablist, when the form loads.
        /// </summary>
        private void ClearTabs()
        {
            foreach (TabPage tabpage in MainTab.TabPages)
            {
                if (tabpage.Text != "World")
                    MainTab.TabPages.Remove(tabpage);
            }
            foreach (TabPage tabpage in MainTabEventCollectionTypes.TabPages)
            {
                MainTabEventCollectionTypes.TabPages.Remove(tabpage);
            }

        }


        /// <summary>
        /// Links all listboxes and treeviews to their appropriate "generic" methods, found in the Generic Control Events region below.
        /// The main listbox on all tabs won't be linked by design, they are linked manually when they are added.
        /// </summary>
        private void LinkEvents()
        {

            foreach (ListBox listbox in Program.GetControlsOfType<ListBox>(this))
            {
                if (listbox.Parent is GroupBox)
                    listbox.DoubleClick += new EventHandler(SubListBoxDoubleClicked);
                else
                    listbox.SelectedIndexChanged += new EventHandler(ListBoxSelectedIndexChanged);
            }

            foreach (TreeView treeview in Program.GetControlsOfType<TreeView>(this))
            {
                treeview.DoubleClick += new EventHandler(TreeviewDoubleClicked);
                treeview.MouseClick += new MouseEventHandler(TreeviewMouseClicked);
            }
        }


        /// <summary>
        /// These are methods that are fired off for nearly every treeview and listbox, so allow easy navigation between objects on the form without a lot of coding work.
        /// </summary>
        #region Generic Control Events
        private void TreeviewMouseClicked(object sender, MouseEventArgs e)
        {
            TreeView treeview = (TreeView)sender; 
            if (e.Button == MouseButtons.Right)
                treeview.ExpandAll();
        }

        /// <summary>
        /// When treeview items are added, they will be given a tag of a World Object 
        ///     (for example, the descendents treeview has historical figure objects added as tags)
        /// When those treeview nodes are double clicked we should route to the associated world object by running it's Select() method.
        /// </summary>
        private void TreeviewDoubleClicked(object sender, EventArgs e)
        {
            TreeView treeview = (TreeView)sender;
            TreeNode selectedNode = treeview.SelectedNode;
            if (selectedNode != null && selectedNode.Tag != null)
            {
                WorldObject selectedItem = (WorldObject)selectedNode.Tag;
                selectedItem.Select(this);
            }
        }

        /// <summary>
        /// When sub-listbox items are added, they are not always world objects (for example, historical figure spheres)
        /// In those cases we want to first verify if the list box item is a world object, and if so we 
        ///     want to route to it by running it's Select() method.
        /// </summary>
        private void SubListBoxDoubleClicked(object sender, EventArgs e)
        {
            ListBox listBox = (ListBox)sender;
            if (typeof(WorldObject).IsAssignableFrom(listBox.SelectedItem.GetType()))
            {
                WorldObject selectedItem = (WorldObject)listBox.SelectedItem;

                selectedItem.Select(this);
            }
        }


        /// <summary>
        /// Called from all the "primary" listboxes, on the left of each of the tabs.  These always are world objects 
        ///     and if we select one of them we should route to it by running it's Select() method.
        /// </summary>
        private void ListBoxSelectedIndexChanged(object sender, EventArgs e)
        {
            ListBox listBox = (ListBox)sender;
            WorldObject selectedItem = (WorldObject)listBox.SelectedItem;
            if (selectedItem != null)
                selectedItem.Select(this);
        }
#endregion


        /// <summary>
        /// These all are used to fill the primary listboxes without having to write loops for each of them, 
        ///     designed with the goal of having a single line call to be able to clear and refill a listbox appropriately.
        /// FillList<T>() and  FillList<T,K>() are the interesting bits.
        /// </summary>
        #region Fill Lists
        private void FillAllLists()
        {
            FillNonXMLLists();
            FillXMLLists();
        }

        private void FillNonXMLLists()
        {
            FillList<Civilization>(lstCivilization, World.Civilizations, tabCivilization);
            FillList<God>(lstGod, World.Gods, tabGod);
            FillList<Leader>(lstLeader, World.Leaders, tabLeader);
            FillList<Parameter>(lstParameter, World.Parameters, tabParameter);
            FillList<Race, string>(lstRace, World.Races, tabRace);
            FillList<Site, int>(lstSite, World.SitesFile, tabSite);
            FillList<Entity>(lstEntity, World.EntitiesFile, tabEntity);
        }

        private void FillXMLLists()
        {
            FillList<Region, int>(lstRegion, World.Regions, tabRegion);
            FillList<UndergroundRegion, int>(lstUndergroundRegion, World.UndergroundRegions, tabUndergroundRegion);
            FillList<Entity, int>(lstEntity, World.Entities, tabEntity);
            FillList<EntityPopulation, int>(lstEntityPopulation, World.EntityPopulations, tabEntityPopulation);
            FillList<Site, int>(lstSite, World.Sites, tabSite);
            FillList<WorldConstruction, int>(lstWorldConstruction, World.WorldConstructions, tabWorldConstruction);
            FillList<Artifact, int>(lstArtifact, World.Artifacts, tabArtifact);
            FillList<HistoricalFigure, int>(lstHistoricalFigure, World.HistoricalFigures, tabHistoricalFigure);
            FillList<HistoricalEvent, int>(lstHistoricalEvent, World.HistoricalEvents, tabHistoricalEvent);
            FillList<HistoricalEventCollection, int>(lstHistoricalEventCollection, World.HistoricalEventCollections, tabHistoricalEventCollection);
            FillList<HistoricalEra, int>(lstHistoricalEra , World.HistoricalEras, tabHistoricalEra);
        }


        /// <summary>
        /// Fill listboxes (from a non-UI thread) in a AddRange() call from a generic list.
        /// Takes into account the current filters on that type of object before filling the list.
        /// </summary>
        /// <typeparam name="T">The type of the object which will fill our listbox</typeparam>
        /// <param name="listBox">The listbox to fill</param>
        /// <param name="list">The list of objects to fill from</param>
        /// <param name="tabPage">The page this listbox is on</param>
        private void FillList<T>(ListBox listBox, List<T> list, TabPage tabPage) where T : class
        {
            listBox.InvokeEx(f => f.BeginUpdate());

            listBox.InvokeEx(f => f.Items.Clear());
            listBox.InvokeEx(f => f.Items.AddRange(World.Filters[typeof(T)].Get<T>(list)));

            listBox.InvokeEx(f => f.EndUpdate());

            if (!MainTab.TabPages.Contains(tabPage))
                this.InvokeEx(f => f.MainTab.TabPages.Add(tabPage));
        }

        /// <summary>
        /// Same as FillList<T>() except for use with World Dictionaries instead of World Lists (dictionaries are normally from the XML, where IDs are given)
        /// </summary>
        /// <typeparam name="T">The type of the object which will fill our listbox</typeparam>
        /// <typeparam name="K">The type of the key for the dictionary, often int</typeparam>
        /// <param name="listBox">The listbox to fill</param>
        /// <param name="dict">The dictionary of objects to fill from</param>
        /// <param name="tabPage">The page this listbox is on</param>
        private void FillList<T, K>(ListBox listBox, Dictionary<K, T> dict, TabPage tabPage) where T : WorldObject
        {

            listBox.InvokeEx(f => f.BeginUpdate());
            listBox.InvokeEx(f => f.Items.Clear());

            listBox.InvokeEx(f => f.Items.AddRange(World.Filters[typeof(T)].Get<T>(dict.Values.ToList())));

            listBox.InvokeEx(f => f.EndUpdate());
            if (!MainTab.TabPages.Contains(tabPage))
                this.InvokeEx(f => f.MainTab.TabPages.Add(tabPage));
        }

    #endregion


        /// <summary>
        /// These methods are called from the World object after being subscribed to, 
        ///     they allow the UI to operate while other threads are handling world *XML* loading.
        ///     Other loading is so quick it doesn't need events.
        /// </summary>
        #region Loading Events

        /// <summary>
        /// Called when a specific section of XML is finished loading.  
        ///     After each is finished the details are written on the World Tab.  The first section writes the details from all the other files which are loaded first.
        /// </summary>
        /// <param name="e"></param>
        private void XMLSectionFinished(string section)
        {
            Program.Log(LogType.Status, section + " finished!");
            switch (section)
            {
                case "regions":
                    FillList<Region, int>(lstRegion, World.Regions, tabRegion);
                    this.InvokeEx(f => f.WorldSummary.Text = World.Name + Environment.NewLine + World.AltName + Environment.NewLine + Environment.NewLine);
                    this.InvokeEx(f => f.WorldSummary.Text += "  Last Year: " + World.LastYear + Environment.NewLine);
                    this.InvokeEx(f => f.WorldSummary.Text += "  Maps Found: " + World.Maps.Count + Environment.NewLine + Environment.NewLine);
                    this.InvokeEx(f => f.WorldSummary.Text += "  History File" + Environment.NewLine);
                    this.InvokeEx(f => f.WorldSummary.Text += "    Civilizations: " + World.Civilizations.Count + Environment.NewLine);
                    this.InvokeEx(f => f.WorldSummary.Text += "    Gods: " + World.Gods.Count + Environment.NewLine);
                    this.InvokeEx(f => f.WorldSummary.Text += "    Leaders: " + World.Leaders.Count + Environment.NewLine + Environment.NewLine);
                    this.InvokeEx(f => f.WorldSummary.Text += "  Site File" + Environment.NewLine);
                    this.InvokeEx(f => f.WorldSummary.Text += "    Sites: " + World.SitesFile.Count + Environment.NewLine);
                    this.InvokeEx(f => f.WorldSummary.Text += "    Entities: " + World.EntitiesFile.Count + Environment.NewLine);
                    this.InvokeEx(f => f.WorldSummary.Text += "    Races: " + World.Races.Count + Environment.NewLine + Environment.NewLine);
                    this.InvokeEx(f => f.WorldSummary.Text += "  XML" + Environment.NewLine);
                    this.InvokeEx(f => f.WorldSummary.Text += "    Regions: " + World.Regions.Count + Environment.NewLine);
                    break;
                case "underground_regions":
                    FillList<UndergroundRegion, int>(lstUndergroundRegion, World.UndergroundRegions, tabUndergroundRegion);
                    this.InvokeEx(f => f.WorldSummary.Text += "    Underground Regions: " + World.UndergroundRegions.Count + Environment.NewLine);
                    break;
                case "entities":
                    FillList<Entity, int>(lstEntity, World.Entities, tabEntity);
                    this.InvokeEx(f => f.WorldSummary.Text += "    Entities: " + World.Entities.Count + Environment.NewLine);
                    break;
                case "entity_populations":
                    FillList<EntityPopulation, int>(lstEntityPopulation, World.EntityPopulations, tabEntityPopulation);
                    this.InvokeEx(f => f.WorldSummary.Text += "    Entity Populations: " + World.EntityPopulations.Count + Environment.NewLine);
                    break;
                case "sites":
                    FillList<Site, int>(lstSite, World.Sites, tabSite);
                    this.InvokeEx(f => f.WorldSummary.Text += "    Sites: " + World.Sites.Count + Environment.NewLine);
                    break;
                case "world_constructions":
                    FillList<WorldConstruction, int>(lstWorldConstruction, World.WorldConstructions, tabWorldConstruction);
                    break;
                case "artifacts":
                    FillList<Artifact, int>(lstArtifact, World.Artifacts, tabArtifact);
                    this.InvokeEx(f => f.WorldSummary.Text += "    Artifacts: " + World.Artifacts.Count + Environment.NewLine);
                    break;
                case "historical_figures":
                    FillList<HistoricalFigure, int>(lstHistoricalFigure, World.HistoricalFigures, tabHistoricalFigure);
                    this.InvokeEx(f => f.WorldSummary.Text += "    Historical Figures: " + World.HistoricalFigures.Count + Environment.NewLine);
                    break;
                case "historical_events":
                    FillList<HistoricalEvent, int>(lstHistoricalEvent, World.HistoricalEvents, tabHistoricalEvent);
                    this.InvokeEx(f => f.WorldSummary.Text += "    Historical Events: " + World.HistoricalEvents.Count + Environment.NewLine);
                    break;
                case "historical_event_collections":
                    FillList<HistoricalEventCollection, int>(lstHistoricalEventCollection, World.HistoricalEventCollections, tabHistoricalEventCollection);
                    this.InvokeEx(f => f.WorldSummary.Text += "    Historical Event Collections: " + World.HistoricalEventCollections.Count + Environment.NewLine);
                    break;
                case "historical_eras":
                    FillList<HistoricalEra, int>(lstHistoricalEra, World.HistoricalEras, tabHistoricalEra);
                    this.InvokeEx(f => f.WorldSummary.Text += "    Historical Eras: " + World.HistoricalEras.Count + Environment.NewLine);
                    break;
                default:
                    break;
            }
        }
        

        /// <summary>
        /// Once all the sections are finished we spin off 4 different threads which handle making matching up the XML with the history/site files.
        /// Since the XML is all loaded we allow world exporting at this point, since nothing else after this point is data that's exported.
        /// Events are subscribed to for the "Linking" step, which turns associations to XML objects by IDs, to actual references to the corresponding object.
        /// </summary>
        private void XMLFinished()
        {
            Program.Log(LogType.Status, "XML finished!");

            World.MergeSites();
            World.MergeEntities();
            World.MergeCivs();
            World.MatchHistoricalFiguresToPeople();


            this.InvokeEx(f => f.exportWorldToolStripMenuItem.Visible = true);
            

            World.LinkedSection += new XMLLinkedSectionEventHandler(XMLSectionLinked);
            World.Linked += new XMLLinkedEventHandler(XMLLinked);

            World.LinkXMLData();

        }

        /// <summary>
        /// After a section is linked, it's noted.
        /// </summary>
        private void XMLSectionLinked(string section)
        {
            Program.Log(LogType.Status, section + " linked!");
        }

        /// <summary>
        /// Once all the sections are linked we allow map and timeline viewing, further processing just improves data, which isn't necessary for those features.
        /// Events are subscribed to for the "Processing" step which makes data more efficiently pulled back, including filling various "events" lists which will be blank before that item is processed.
        /// Dynasties Created and Families Counted are event handlers which are called after completing the World.FamilyProcessing() method below.
        /// </summary>
        private void XMLLinked()
        {
            Program.Log(LogType.Status, "XML linked!");
            this.InvokeEx(f => f.showMapToolStripMenuItem.Visible = true);
            this.InvokeEx(f => f.timelineToolStripMenuItem.Visible = true);
            this.InvokeEx(f => f.WorldSummary.Text += "    World Constructions: " + World.WorldConstructions.Count + Environment.NewLine);

            World.ProcessedSection += new XMLProcessedSectionEventHandler(XMLSectionProcessed);
            World.Processed += new XMLProcessedEventHandler(XMLProcessed);
            World.FamiliesCounted += new FamiliesCountedEventHandler(FamiliesCounted);
            World.DynastiesCreated += new DynastiesCreatedEventHandler(DynastiesCreated);
            


            World.ProcessXMLData();
        }

        /// <summary>
        /// After a section is processed, it's noted.  The World.FamilyProcessing() method starts threads which create dynasties and 
        ///     count family members (this could take an enormous amount of time on old worlds, so it'll likely just keep running in the background as you view everything else)
        /// </summary>
        private void XMLSectionProcessed(string section)
        {
            Program.Log(LogType.Status, section + " processed!");
            if (section == "Historical Figures")
                World.FamilyProcessing();
        }

        /// <summary>
        /// When families are done being counted that's noted.  Once this is done the Ancestor and Descendnet counts in the Historical Figures filter option will give good results.
        /// </summary>
        void FamiliesCounted()
        {
            Program.Log(LogType.Status, "Families Counted!");
        }

        /// <summary>
        /// Fill Dynasties when they're created.  This is a new type of World Object I came up with since the data existed and it seems interesting.  See World.CreateDynasties() for details.
        /// </summary>
        /// <param name="e"></param>
        void DynastiesCreated()
        {
            FillList<Dynasty>(lstDynasty, World.Dynasties, tabDynasty);
            Program.Log(LogType.Status, "Dynasties Created!");
            this.InvokeEx(f => f.WorldSummary.Text += "    Dynasties: " + World.Dynasties.Count + Environment.NewLine);
        }

        /// <summary>
        /// After all the sections have been processed (families might not have been counted yet, but that's okay)
        ///     We start Evaluating Event Collections - This gathers extra data by viewing Historical Events from their event collections, this lets us get new info like:
        ///         finding out where item's were stolen, what beast devoured a historical figure, and other things.  See World.EventCollectionEvaluation() for details.
        ///     and Positioning Historical figures - This pulls in all the data we have so far to try to place a historical figure in some location, it's incomplete but still interesting:
        ///         See World.HistoricalFiguresPositioning() for details.
        /// </summary>
        private void XMLProcessed()
        {
            Program.Log(LogType.Status, "XML processed!");

            FillAllLists();

            World.EventCollectionsEvaluated += new EventCollectionsEvaluatedEventHandler(World_EventCollectionsEvaluated);
            World.HistoricalFiguresPositioned += new HistoricalFiguresPositionedEventHandler(World_HistoricalFiguresPositioned);
            World.StatsGathered += new StatsGatheredEventHandler(World_StatsGathered);

            World.EventCollectionEvaluation();
            World.HistoricalFiguresPositioning();
            World.StatsGathering();
        }

        private void World_EventCollectionsEvaluated()
        {
            Program.Log(LogType.Status, "Event Collections Evaluated!");

            World.VisualizationsCreated += new VisualizationsCreatedEventHandler(World_VisualizationsCreated);

            World.VisualizationCreation();
        }

        /// <summary>
        ///  After trying to position historical figures, note how many were succesfully positioned out of all those who are alive (dead people aren't positioned because they're dead).
        /// </summary>
        private void World_HistoricalFiguresPositioned()
        {
            int HFsPositioned = World.HistoricalFigures.Values.Where(x => x.Site != null || x.Region != null || x.Coords != System.Drawing.Point.Empty).Count();
            int HFsAlive = World.HistoricalFigures.Values.Where(x => x.DiedEvent == null).Count();

            Program.Log(LogType.Status, String.Format("Historical Figures Positioned (" + Math.Round((float)HFsPositioned / (float)HFsAlive * 100.0f, 0) + "% located)"));

        }

        private void World_StatsGathered()
        {
            Program.Log(LogType.Status, "World Stats Gathered!");

            statsToolStripMenuItem.Visible = true;
        }

        private void World_VisualizationsCreated()
        {
            Program.Log(LogType.Status, "Visualizations Created!");

            visualizationsToolStripMenuItem.Visible = true;
        }

        #endregion

        #region Entity Population Tab
        /// <summary>
        /// When an entity population is part of a war and we select that war we want to display that entity populations role in that war.
        /// </summary>
        private void lstEntityPopulationBattles_SelectedIndexChanged(object sender, EventArgs e)
        {
            EC_Battle evtcol = (EC_Battle)lstEntityPopulationBattles.SelectedItem;
            EntityPopulation entpop = (EntityPopulation)lstEntityPopulation.SelectedItem;
            Squad thisSquad = null;
            foreach (Squad squad in evtcol.AttackingSquad)
	        {
                if (squad.EntityPopulation == entpop)
                {
                    thisSquad = squad;
                    break;
                }
	        }
            if (thisSquad == null)
            {
                foreach (Squad squad in evtcol.DefendingSquad)
	            {
                    if (squad.EntityPopulation == entpop)
                    {
                        thisSquad = squad;
                        break;
                    }
	            }
            }
            if (thisSquad == null)
            {
                lblEntityPopulationBattleDeaths.Text = "";
                lblEntityPopulationBattleNumber.Text = "";
                lblEntityPopulationBattleSite.Data = null;
                lblEntityPopulationBattleTime.Data = null;
                lblEntityPopulationBattleTime.Text = "";
                lblEntityPopulationBattleWar.Data = null;

            }
            else
            {
                lblEntityPopulationBattleDeaths.Text = thisSquad.Deaths.ToString();
                lblEntityPopulationBattleNumber.Text = thisSquad.Number.ToString();
                lblEntityPopulationBattleSite.Data = thisSquad.Site;
                lblEntityPopulationBattleTime.Data = evtcol;
                lblEntityPopulationBattleTime.Text = evtcol.StartTime.ToString();
                lblEntityPopulationBattleWar.Data = evtcol.WarEventCol;
            }
        }
        #endregion

        /// <summary>
        /// Because sites list the things that live there and the number of those things when that information is added to a listbox 
        ///     it needs to both be added as the race in question, but then displayed with addition information.  
        ///     These methods handle modifying how items are displayed in those three listboxes.
        /// </summary>
        #region Site Tab
        private void lstSitePopulation_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();

            if (e.Index != -1)
            {
                Site thisSite = (Site)lstSite.SelectedItem;
                string drawstring = thisSite.Population[(Race)lstSitePopulation.Items[e.Index]].ToString() +
                    " " + lstSitePopulation.Items[e.Index].ToString();
                e.Graphics.DrawString(drawstring,
                    e.Font, System.Drawing.Brushes.Black, e.Bounds, System.Drawing.StringFormat.GenericDefault);
            }
            e.DrawFocusRectangle();
        }

        private void lstSitePrisoners_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();


            if (e.Index != -1)
            {
                Site thisSite = (Site)lstSite.SelectedItem;
                string drawstring = thisSite.Prisoners[(Race)lstSitePrisoners.Items[e.Index]].ToString() +
                    " " + lstSitePrisoners.Items[e.Index].ToString();
                e.Graphics.DrawString(drawstring,
                    e.Font, System.Drawing.Brushes.Black, e.Bounds, System.Drawing.StringFormat.GenericDefault);
            }
            e.DrawFocusRectangle();
        }

        private void lstSiteOutcasts_DrawItem(object sender, DrawItemEventArgs e)
        {

            e.DrawBackground();


            if (e.Index != -1)
            {
                Site thisSite = (Site)lstSite.SelectedItem;
                string drawstring = thisSite.Outcasts[(Race)lstSiteOutcasts.Items[e.Index]].ToString() +
                    " " + lstSiteOutcasts.Items[e.Index].ToString();
                e.Graphics.DrawString(drawstring,
                    e.Font, System.Drawing.Brushes.Black, e.Bounds, System.Drawing.StringFormat.GenericDefault);
            }
            e.DrawFocusRectangle();

        }
        #endregion

        #region Historical Event Collection Tab

        /// <summary>
        /// When a squad in a battle is selected, we want to display information on that squads role in the battle.
        /// </summary>
        private void lstBattleAttackingSquad_SelectedIndexChanged(object sender, EventArgs e)
        {
            Squad squad = (Squad)((ListBox)sender).SelectedItem;

            lblBattleAttackingSquadSite.Data = squad.Site;
            lblBattleAttackingSquadRace.Data = squad.Race;
            lblBattleAttackingSquadNumber.Text = squad.Number.ToString();
            lblBattleAttackingSquadEntPop.Data = squad.EntityPopulation;
            lblBattleAttackingSquadDeaths.Text = squad.Deaths.ToString();
        }

        private void lstBattleDefendingSquad_SelectedIndexChanged(object sender, EventArgs e)
        {
            Squad squad = (Squad)((ListBox)sender).SelectedItem;

            lblBattleDefendingSquadSite.Data = squad.Site;
            lblBattleDefendingSquadRace.Data = squad.Race;
            lblBattleDefendingSquadNumber.Text = squad.Number.ToString();
            lblBattleDefendingSquadEntPop.Data = squad.EntityPopulation;
            lblBattleDefendingSquadDeaths.Text = squad.Deaths.ToString();
        }

        /// <summary>
        /// HFs that fought in a battle are displayed in a color based on gender and listed bold if they died.
        /// </summary>
        private void lstBattleAttackingHF_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();

            if (e.Index != -1)
            {
                EC_Battle thisBattle = (EC_Battle)lstHistoricalEventCollection.SelectedItem;
                string drawstring = thisBattle.AttackingHF[e.Index].ToString();

                Drawing.Color mColor = Drawing.Color.Black;
                if (thisBattle.AttackingHF[e.Index].Caste.HasValue && HistoricalFigure.Castes[thisBattle.AttackingHF[e.Index].Caste.Value].ToLower().StartsWith("male"))
                    mColor = Drawing.Color.Blue;
                else if (thisBattle.AttackingHF[e.Index].Caste.HasValue && HistoricalFigure.Castes[thisBattle.AttackingHF[e.Index].Caste.Value].ToLower().StartsWith("female"))
                    mColor = Drawing.Color.Red; 
                  

                if (thisBattle.AttackingDiedHF != null && thisBattle.AttackingDiedHF.Contains(thisBattle.AttackingHF[e.Index]))
                    e.Graphics.DrawString(drawstring, new System.Drawing.Font(e.Font.FontFamily.ToString(), e.Font.Size, System.Drawing.FontStyle.Bold), new Drawing.SolidBrush(mColor), e.Bounds);
                else
                    e.Graphics.DrawString(drawstring, e.Font, new Drawing.SolidBrush(mColor), e.Bounds);

            }
            e.DrawFocusRectangle();
        }

        private void lstBattleDefendingHF_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();
            System.Drawing.Brush myBrush = System.Drawing.Brushes.Black;


            if (e.Index != -1)
            {
                EC_Battle thisBattle = (EC_Battle)lstHistoricalEventCollection.SelectedItem;
                string drawstring = thisBattle.DefendingHF[e.Index].ToString();

                Drawing.Color mColor = Drawing.Color.Black;
                if (thisBattle.DefendingHF[e.Index].Caste.HasValue && HistoricalFigure.Castes[thisBattle.DefendingHF[e.Index].Caste.Value].ToLower().StartsWith("male"))
                    mColor = Drawing.Color.Blue;
                else if (thisBattle.DefendingHF[e.Index].Caste.HasValue && HistoricalFigure.Castes[thisBattle.DefendingHF[e.Index].Caste.Value].ToLower().StartsWith("female"))
                    mColor = Drawing.Color.Red;
                
                if (thisBattle.DefendingDiedHF != null && thisBattle.DefendingDiedHF.Contains(thisBattle.DefendingHF[e.Index]))
                    e.Graphics.DrawString(drawstring, new System.Drawing.Font(e.Font.FontFamily.ToString(), e.Font.Size, System.Drawing.FontStyle.Bold), new Drawing.SolidBrush(mColor), e.Bounds);
                else
                    e.Graphics.DrawString(drawstring, e.Font, new Drawing.SolidBrush(mColor), e.Bounds);
            }
            e.DrawFocusRectangle();
        }

        /// <summary>
        /// On ANY tab if a listbox contains events selecting that listbox leads here, 
        ///     which dynamically generates controls as needed to write information on the event.
        /// </summary>
        private void EventCollection_EventsListClick(object sender, EventArgs e)
        {
            ListBox listBox = (ListBox)sender;
            if (typeof(HistoricalEvent).IsAssignableFrom(listBox.SelectedItem.GetType()))
            {
                HistoricalEvent evt = (HistoricalEvent)listBox.SelectedItem;
                

                evt.WriteDetailsOnParent(this, listBox.Parent, new System.Drawing.Point(listBox.Left, listBox.Bottom + 10));
            }
            
        }

        #endregion 


        /// <summary>
        /// Filtering is handled by clicking the filter button under a primary list box or typing into the textbox under that listbox, 
        ///     these methods handle those controls.
        /// </summary>
        #region Filter Methods


        /// <summary>
        /// Depending on which Fitler button we click we want to start filtering against a different type of object.
        /// </summary>
        private void FilterButton_Click(object sender, EventArgs e)
        {
            switch (((Button)sender).Name)
            {
                case "FilterRegion":
                    StartFilter<Region, int>(lstRegion, World.Regions, tabRegion);
                    break;
                case "FilterUndergroundRegion":
                    StartFilter<UndergroundRegion, int>(lstUndergroundRegion, World.UndergroundRegions, tabUndergroundRegion);
                    break;
                case "FilterEntity":
                    StartFilter<Entity, int>(lstEntity, World.Entities, tabEntity);
                    break;
                case "FilterEntityPopulation":
                    StartFilter<EntityPopulation, int>(lstEntityPopulation, World.EntityPopulations, tabEntityPopulation);
                    break;
                case "FilterSite":
                    StartFilter<Site, int>(lstSite, World.Sites, tabSite);
                    break;
                case "FilterWorldConstruction":
                    StartFilter<WorldConstruction, int>(lstWorldConstruction, World.WorldConstructions, tabWorldConstruction);
                    break;
                case "FilterArtifact":
                    StartFilter<Artifact, int>(lstArtifact, World.Artifacts, tabArtifact);
                    break;
                case "FilterHistoricalFigure":
                    StartFilter<HistoricalFigure, int>(lstHistoricalFigure, World.HistoricalFigures, tabHistoricalFigure);
                    break;
                case "FilterHistoricalEvent":
                    StartFilter<HistoricalEvent, int>(lstHistoricalEvent, World.HistoricalEvents, tabHistoricalEvent);
                    break;
                case "FilterHistoricalEventCollection":
                    StartFilter<HistoricalEventCollection, int>(lstHistoricalEventCollection, World.HistoricalEventCollections, tabHistoricalEventCollection);
                    break;
                case "FilterHistoricalEra":
                    StartFilter<HistoricalEra, int>(lstHistoricalEra, World.HistoricalEras, tabHistoricalEra);
                    break;
                case "FilterCivilization":
                    StartFilter<Civilization>(lstCivilization, World.Civilizations, tabCivilization);
                    break;
                case "FilterGod":
                    StartFilter<God>(lstGod, World.Gods, tabGod);
                    break;
                case "FilterLeader":
                    StartFilter<Leader>(lstLeader, World.Leaders, tabLeader);
                    break;
                case "FilterParameter":
                    StartFilter<Parameter>(lstParameter, World.Parameters, tabParameter);
                    break;
                case "FilterRace":
                    StartFilter<Race, string>(lstRace, World.Races, tabRace);
                    break;
                case "FilterDynasty":
                    StartFilter<Dynasty>(lstDynasty, World.Dynasties, tabDynasty);
                    break;
                default:
                    break;
            }

        }


        /// <summary>
        /// This calls the filter form which handles the filtering of objects, 
        ///     after returning results we modify the acting filter and then reload the appropriate list with the new filter.
        /// </summary>
        /// <typeparam name="T">The type of object getting filtered</typeparam>
        /// <param name="listBox">The listbox to display into</param>
        /// <param name="list">The list we're filtering against</param>
        /// <param name="tabPage">The tabPage our listbox is on.</param>
        private void StartFilter<T>(ListBox listBox, List<T> list, TabPage tabPage) where T : WorldObject
        {
            FilterForm FilterForm = new FilterForm(World, typeof(T));
            DialogResult res = FilterForm.ShowDialog();
            if (res == DialogResult.OK)
            {
                World.Filters[typeof(T)] = FilterForm.outFilter;

                FillList<T>(listBox, list, tabPage);
            }
            FilterForm.Dispose();
            FilterForm = null;
        }

        /// <summary>
        /// Same as StartFilter<T>() but with filtering dictionaries (usually XML data)
        /// </summary>
        /// <typeparam name="T">The type of object getting filtered</typeparam>
        /// <typeparam name="K">The type of the key of the dictionary getting filtered</typeparam>
        /// <param name="listBox">The listbox to display into</param>
        /// <param name="dict">The dictionary we're filtering against</param>
        /// <param name="tabPage">The tabPage our listbox is on.</param>
        private void StartFilter<T, K>(ListBox listBox, Dictionary<K, T> dict, TabPage tabPage) where T : WorldObject
        {
            FilterForm FilterForm = new FilterForm(World, typeof(T));
            DialogResult res = FilterForm.ShowDialog();
            if (res == DialogResult.OK)
            {
                World.Filters[typeof(T)] = FilterForm.outFilter;

                FillList<T, K>(listBox, dict, tabPage);
            }
            FilterForm.Dispose();
            FilterForm = null;
        }


        /// <summary>
        /// Depending on which Fitler textbox we change we want to start filtering against a different type of object.
        /// </summary>
        private void TextFilter_Changed(object sender, EventArgs e)
        {
            string txt = ((TextBox)sender).Text;
            switch (((TextBox)sender).Name)
            {
                case "TextFilterRegion":
                    TextFilter<Region, int>(txt, lstRegion, World.Regions, tabRegion);
                    break;
                case "TextFilterUndergroundRegion":
                    TextFilter<UndergroundRegion, int>(txt, lstUndergroundRegion, World.UndergroundRegions, tabUndergroundRegion);
                    break;
                case "TextFilterEntity":
                    TextFilter<Entity, int>(txt, lstEntity, World.Entities, tabEntity);
                    break;
                case "TextFilterEntityPopulation":
                    TextFilter<EntityPopulation, int>(txt, lstEntityPopulation, World.EntityPopulations, tabEntityPopulation);
                    break;
                case "TextFilterSite":
                    TextFilter<Site, int>(txt, lstSite, World.Sites, tabSite);
                    break;
                case "TextFilterWorldConstruction":
                    TextFilter<WorldConstruction, int>(txt, lstWorldConstruction, World.WorldConstructions, tabWorldConstruction);
                    break;
                case "TextFilterArtifact":
                    TextFilter<Artifact, int>(txt, lstArtifact, World.Artifacts, tabArtifact);
                    break;
                case "TextFilterHistoricalFigure":
                    TextFilter<HistoricalFigure, int>(txt, lstHistoricalFigure, World.HistoricalFigures, tabHistoricalFigure);
                    break;
                case "TextFilterHistoricalEvent":
                    TextFilter<HistoricalEvent, int>(txt, lstHistoricalEvent, World.HistoricalEvents, tabHistoricalEvent);
                    break;
                case "TextFilterHistoricalEventCollection":
                    TextFilter<HistoricalEventCollection, int>(txt, lstHistoricalEventCollection, World.HistoricalEventCollections, tabHistoricalEventCollection);
                    break;
                case "TextFilterHistoricalEra":
                    TextFilter<HistoricalEra, int>(txt, lstHistoricalEra, World.HistoricalEras, tabHistoricalEra);
                    break;
                case "TextFilterCivilization":
                    TextFilter<Civilization>(txt, lstCivilization, World.Civilizations, tabCivilization);
                    break;
                case "TextFilterGod":
                    TextFilter<God>(txt, lstGod, World.Gods, tabGod);
                    break;
                case "TextFilterLeader":
                    TextFilter<Leader>(txt, lstLeader, World.Leaders, tabLeader);
                    break;
                case "TextFilterParameter":
                    TextFilter<Parameter>(txt, lstParameter, World.Parameters, tabParameter);
                    break;
                case "TextFilterRace":
                    TextFilter<Race, string>(txt, lstRace, World.Races, tabRace);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Creates a temporary filter from the text typed into the textbox.
        /// Then we reload from that fitler, and remove the temporary filter.
        /// </summary>
        /// <typeparam name="T">The type of object getting filtered</typeparam>
        /// <param name="txt">The text to filter on</param>
        /// <param name="listBox">The listbox to display into</param>
        /// <param name="list">The list we're filtering against</param>
        /// <param name="tabPage">The tabPage our listbox is on.</param>
        private void TextFilter<T>(string txt, ListBox listBox, List<T> list, TabPage tabPage) where T : WorldObject
        {
            string tempFilter = "DispNameLower.Contains(\"" + txt.ToLower() + "\")";
            World.Filters[typeof(T)].Where.Add(tempFilter);

            FillList<T>(listBox, list, tabPage);

            World.Filters[typeof(T)].Where.Remove(tempFilter);
        }

        /// <summary>
        /// Same as TextFilter<T>() but with a dictionary
        /// </summary>
        /// <typeparam name="T">The type of object getting filtered</typeparam>
        /// <typeparam name="K">The type of the key of the dictionary getting filtered</typeparam>
        /// <param name="txt">The text to filter on</param>
        /// <param name="listBox">The listbox to display into</param>
        /// <param name="dict">The dictionary we're filtering against</param>
        /// <param name="tabPage">The tabPage our listbox is on.</param>
        private void TextFilter<T, K>(string txt, ListBox listBox, Dictionary<K, T> dict, TabPage tabPage) where T: WorldObject
        {

            string tempFilter = "DispNameLower.Contains(\"" + txt.ToLower() + "\")";
            World.Filters[typeof(T)].Where.Add(tempFilter);

            FillList<T, K>(listBox, dict, tabPage);

            World.Filters[typeof(T)].Where.Remove(tempFilter);

        }
        
        #endregion


        /// <summary>
        /// Handles the backward navigation through objects
        /// </summary>
        #region Navigation Methods

        /// <summary>
        /// When an object is selected it's added to the navigation, we only display the back button if we have a previous item to go back to now.
        /// </summary>
        /// <param name="item"></param>
        internal void AddToNav(WorldObject item)
        {
            if (ViewedObjects.Count > 0 && ViewedObjects.Peek() == item)
                return;
            ViewedObjects.Push(item);

            BacktoolStripMenuItem.Visible = ViewedObjects.Count >= 2;
        }

        /// <summary>
        /// When we go backwards we first pop off the first item (the one we're on now) then pop the previous item to travel back to it.
        /// </summary>
        private void BacktoolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Remove Top Object
            ViewedObjects.Pop();

            WorldObject selObject = ViewedObjects.Pop();

            selObject.Select(this);

            BacktoolStripMenuItem.Visible = ViewedObjects.Count >= 2;
        }
        #endregion

    }
}
