﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using DFWV.Controls;
using DFWV.WorldClasses;
using DFWV.WorldClasses.EntityClasses;
using DFWV.WorldClasses.HistoricalEventClasses;
using DFWV.WorldClasses.HistoricalEventCollectionClasses;
using DFWV.WorldClasses.HistoricalFigureClasses;
using Region = DFWV.WorldClasses.Region;
using Squad = DFWV.WorldClasses.HistoricalEventCollectionClasses.Squad;

namespace DFWV
{
    public sealed partial class MainForm : Form
    {
        /// <summary>
        /// The Root object which describes everything about the DF world.
        /// </summary>
        internal World World;

        /// <summary>
        /// Stores the dynamically generated controls for the currently viewed Historical Event, whereever it's viewed.
        /// </summary>
        public readonly List<Control> EventDetailControls = new List<Control>();

        /// <summary>
        /// Allows navigation through World Objects. (See Navigation Methods Region)
        /// </summary>
        private readonly Stack<WorldObject> _navBackObjects = new Stack<WorldObject>();
        private readonly Stack<WorldObject> _navForwardObjects = new Stack<WorldObject>();
        private bool _navigatingBack;
        public Dictionary<Type, WorldObject> displayingItem = new Dictionary<Type, WorldObject>(); 

        public MainForm()
        {
            InitializeComponent();

            Text = $"World Viewer v{Application.ProductVersion}";
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
            var selectedFile = AutoFindFiles();

            if (selectedFile == "")
            { 
                var openFileDiag = new OpenFileDialog
                {
                    InitialDirectory = Program.GetDefaultPath(),
                    Filter = @"DF Maps/Archives(*.bmp, *.png, *.7z, *.zip)|*.bmp;*.png;*.7z;*.zip"
                };


                openFileDiag.ShowDialog();

                if (openFileDiag.FileName != "")
                    selectedFile = openFileDiag.FileName;
            }

            if (string.IsNullOrEmpty(selectedFile)) return;
            if (Path.GetExtension(selectedFile) == ".7z" || Path.GetExtension(selectedFile) == ".zip")
            {
                if (Program.ExtractArchive(selectedFile))
                {
                    var newDirectory = Path.Combine(Path.GetDirectoryName(selectedFile), Path.GetFileNameWithoutExtension(selectedFile));

                    selectedFile = Directory.GetFiles(newDirectory, "*.bmp").FirstOrDefault();
                        
                }
            }
            if (selectedFile != null)
                LoadFromFiles(selectedFile);
        }

        private static string AutoFindFiles()
        {
            try
            {
                var workingFolder = Program.GetDefaultPath();
                

                foreach (var file in from file in Directory.GetFiles(workingFolder) let filename = Path.GetFileName(file) where filename.EndsWith("-world_map.bmp") select file)
                {
                    return file;
                }
                return "";
            }
            catch (Exception)
            {
                return "";
            } 
        }

        private void LoadFromFiles(string filename)
        {
            var mapPath = filename;
            var path = Path.GetDirectoryName(mapPath) ?? "";
            if (!mapPath.Contains("-world_map.bmp")) //Picked wrong image
            {
                var thisFile = Path.GetFileName(mapPath);
                var restofFile = thisFile.Split('-').ToList();
                restofFile[restofFile.Count - 1] = "world_map.bmp";
                mapPath = Path.Combine(path, string.Join("-", restofFile));
                //In case they picked a sitemap
                mapPath = mapPath.Replace("-site_map-world_map.bmp", "-world_map.bmp");
            }
            if (!File.Exists(mapPath))
            {
                MessageBox.Show(@"Couldn't find basic map image");
                return;
            }


            var mapFile = Path.GetFileNameWithoutExtension(mapPath);
            var mapSplit = mapFile.Split(new[] {'-'}, StringSplitOptions.RemoveEmptyEntries).ToList();

            mapSplit.Reverse();
            mapSplit.RemoveAt(0);

            var worldGenTime = new WorldTime(Convert.ToInt32(mapSplit[2]), Convert.ToInt32(mapSplit[1]) - 1, Convert.ToInt32(mapSplit[0]) - 1);
            mapSplit.RemoveRange(0,3);
            mapSplit.Reverse();

            var name = string.Join("-", mapSplit);
            var nameWithTime = mapFile.Replace("-world_map", "");

            var xmlPath = Path.Combine(path, nameWithTime + "-legends.xml");
            var paramPath = Path.Combine(path, name + "-world_gen_param.txt");
            var historyPath = Path.Combine(path, nameWithTime + "-world_history.txt");
            var sitesPath = Path.Combine(path, nameWithTime + "-world_sites_and_pops.txt");
            var xmlPlusPath = Path.Combine(path, nameWithTime + "-legends_plus.xml");

            if (File.Exists(xmlPath) && File.Exists(paramPath) && File.Exists(historyPath) && File.Exists(sitesPath))
            {

                DFXMLParser.StartedSection -= XmlSectionStarted;
                DFXMLParser.FinishedSection -= XmlSectionFinished;
                DFXMLParser.Finished -= XmlFinished;

                DFXMLParser.StartedSection += XmlSectionStarted;
                DFXMLParser.FinishedSection += XmlSectionFinished;
                DFXMLParser.Finished += XmlFinished;

                Application.DoEvents();

                World = new World(historyPath, sitesPath, paramPath, xmlPath, xmlPlusPath, mapPath, worldGenTime);

                loadWorldToolStripMenuItem.Visible = false;

                Program.SetMinorProgress(0, 0);
                Program.SetMajorProgress(0, 60);
                World.StartThread(() => World.LoadFiles(), "World Loading");
                
            }
            else
                MessageBox.Show(@"Files not found.  Please make sure all 5 files Legends files are located with the selected map file. See the README.txt file included for details.", @"DF World Viewer",MessageBoxButtons.OK);



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
        /// Cleanly empties EventDetailControls, which stores controls related to displaying the current event.  
        /// Called normally before displaying a new event.
        /// </summary>
        internal void ClearEventDetails()
        {
            foreach (var ctrl in EventDetailControls)
            {
                ctrl.Parent?.Controls.Remove(ctrl);
                ctrl.Dispose();
            }
            EventDetailControls.Clear();

        }

        /// <summary>
        /// Removes all but the World tab from the tablist, when the form loads.
        /// </summary>
        private void ClearTabs()
        {
            foreach (var tabpage in MainTab.TabPages.Cast<TabPage>().Where(tabpage => tabpage.Text != @"World"))
            {
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
            foreach (var listbox in Program.GetControlsOfType<ListBox>(this))
            {
                if (listbox.Parent is GroupBox)
                    listbox.DoubleClick += SubListBoxDoubleClicked;
                else
                {
                    listbox.DrawMode = DrawMode.OwnerDrawFixed;
                    listbox.DrawItem += ListBoxDrawItem;
                    listbox.SelectedIndexChanged += ListBoxSelectedIndexChanged;
                }
            }

            foreach (var treeview in Program.GetControlsOfType<TreeView>(this).Where(treeview => treeview != WorldSummaryTree))
            {
                treeview.DoubleClick += TreeviewDoubleClicked;
                treeview.MouseClick += TreeviewMouseClicked;
            }
        }


        /// <summary>
        /// These are events fired off when a toolstrip menu button is clicked, besdies the navigation ones
        /// </summary>
        #region ToolbarClickEvents
        /// <summary>
        /// Loads and displays the map form.
        /// </summary>
        private void showMapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Program.MapForm == null || Program.MapForm.IsDisposed)
                Program.MapForm = new MapForm(World);
            Program.MapForm.Location = Location;
            Program.MapForm.Show();
        }

        /// <summary>
        /// Loads and displays the map form.
        /// </summary>
        private void timelinetoolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Program.TimelineForm == null || Program.TimelineForm.IsDisposed)
                Program.TimelineForm = new TimelineForm(World);
            Program.TimelineForm.Location = Location;
            Program.TimelineForm.Show();
        }

        /// <summary>
        /// Loads and displays the stats form.
        /// </summary>
        private void statsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Program.StatsForm == null || Program.StatsForm.IsDisposed)
                Program.StatsForm = new StatsForm(World);
            Program.StatsForm.Location = Location;
            Program.StatsForm.Show();
        }

        /// <summary>
        /// Fires off the Export method of the World Object on a new thread, which outputs all relevant world data to a SQLite database.
        /// </summary>
        private void exportWorldToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var saveFileDiag = new SaveFileDialog
            {
                InitialDirectory = Application.StartupPath,
                Filter = @"World Viewer Export (*.sqlite3)|*.sqlite3",
                FileName = Program.CleanString(World.Name) + " Export.sqlite3"
            };

            saveFileDiag.ShowDialog();

            if (saveFileDiag.FileName == "")
                return;
            var filename = saveFileDiag.FileName;
            if (Path.GetExtension(filename) != ".sqlite3")
                filename = Path.GetFileNameWithoutExtension(filename) + ".sqlite3";

            if (File.Exists(filename))
                File.Delete(filename);
            File.Copy(Application.StartupPath + @"\DFWV_Template.sqlite3.backup", filename);

            exportWorldToolStripMenuItem.Visible = false;

            World.StartThread(() => World.Export(filename), "Exporting World");
        }

        /// <summary>
        /// Closes the existing world so allow a new world to be loaded.
        /// </summary>
        private void closeWorldToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearTabs();
            closeWorldToolStripMenuItem.Visible = false;
            exportWorldToolStripMenuItem.Visible = false;
            showMapToolStripMenuItem.Visible = false;
            statsToolStripMenuItem.Visible = false;
            timelineToolStripMenuItem.Visible = false;
            visualizationsToolStripMenuItem.Visible = false;
            ClearNav();

            WorldSummaryTree.Nodes.Clear();
            StatusBox.Clear();
            IssuesBox.Clear();

            loadWorldToolStripMenuItem.Visible = true;

            World.Dispose();
            World = null;
        }
        #endregion

        /// <summary>
        /// These are methods that are fired off for nearly every treeview and listbox, so allow easy navigation between objects on the form without a lot of coding work.
        /// </summary>
        #region Generic Control Events
        private static void TreeviewMouseClicked(object sender, MouseEventArgs e)
        {
            var treeview = (TreeView)sender; 
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
            var treeview = (TreeView)sender;
            var selectedNode = treeview.SelectedNode;
            if (selectedNode?.Tag == null) 
                return;
            var selectedItem = (WorldObject)selectedNode.Tag;
            selectedItem.Select(this);
        }

        /// <summary>
        /// When sub-listbox items are added, they are not always world objects (for example, historical figure spheres)
        /// In those cases we want to first verify if the list box item is a world object, and if so we 
        ///     want to route to it by running it's Select() method.
        /// </summary>
        private void SubListBoxDoubleClicked(object sender, EventArgs e)
        {
            var listBox = (ListBox)sender;
            var selectedItem = listBox.SelectedItem as WorldObject;
            selectedItem?.Select(this);
        }


        /// <summary>
        /// Called from all the "primary" listboxes, on the left of each of the tabs.  These always are world objects 
        ///     and if we select one of them we should route to it by running it's Select() method.
        /// </summary>
        private void ListBoxSelectedIndexChanged(object sender, EventArgs e)
        {
            var listBox = (ListBox)sender;
            var selectedItem = listBox.SelectedItem as WorldObject;
            selectedItem?.Select(this);
        }

        /// <summary>
        ///  Used to Draw the main list box on each tab, allowing a different font/color for section headers.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListBoxDrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();

            if (e.Index != -1)
            {
                var lstBox = sender as ListBox;
                if (lstBox.Items[e.Index] is WorldObject)
                {
                    e.Graphics.DrawString(lstBox.Items[e.Index].ToString(),
                            e.Font, Brushes.Black, e.Bounds, StringFormat.GenericDefault);
                }
                else
                {
                    e.Graphics.DrawString(lstBox.Items[e.Index].ToString(),

                            new Font(FontFamily.GenericMonospace, e.Font.Size, FontStyle.Bold), Brushes.Red, e.Bounds, StringFormat.GenericDefault);
                }

            }
            e.DrawFocusRectangle();
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
            FillNonXmlLists();
            FillXmlLists();
        }

        public void FillNonXmlLists()
        {
            FillList(lstCivilization, World.Civilizations, tabCivilization);
            FillList(lstGod, World.Gods, tabGod);
            FillList(lstLeader, World.Leaders, tabLeader);
            FillList(lstParameter, World.Parameters, tabParameter);
            FillList(lstRace, World.Races, tabRace);
            FillList(lstSite, World.SitesFile, tabSite);
            FillList(lstEntity, World.EntitiesFile, tabEntity);
        }

        private void FillXmlLists()
        {
            FillList(lstArmy, World.Armies, tabArmy);
            FillList(lstUnit, World.Units, tabUnit);
            FillList(lstEngraving, World.Engravings, tabEngraving);
            FillList(lstReport, World.Reports, tabReport);
            FillList(lstWrittenContent, World.WrittenContents, tabWrittenContent);
            FillList(lstMusicalForm, World.MusicalForms, tabMusicalForm);
            FillList(lstDanceForm, World.DanceForms, tabDanceForm);
            FillList(lstPoeticForm, World.PoeticForms, tabPoeticForm);
            FillList(lstBuilding, World.Buildings, tabBuilding);
            FillList(lstConstruction, World.Constructions, tabConstruction);
            FillList(lstItem, World.Items, tabItem);
            FillItemTreeviews();
            FillList(lstPlant, World.Plants, tabPlant);
            FillList(lstSquad, World.Squads, tabSquad);
            FillList(lstRiver, World.Rivers, tabRiver);
            FillList(lstMountain, World.Mountains, tabMountain);
            FillList(lstLandmass, World.Landmasses, tabLandmass);
            FillList(lstRegion, World.Regions, tabRegion);
            FillList(lstUndergroundRegion, World.UndergroundRegions, tabUndergroundRegion);
            FillList(lstEntity, World.Entities, tabEntity);
            FillList(lstEntityPopulation, World.EntityPopulations, tabEntityPopulation);
            FillList(lstSite, World.Sites, tabSite);
            FillList(lstWorldConstruction, World.WorldConstructions, tabWorldConstruction);
            FillList(lstArtifact, World.Artifacts, tabArtifact);
            FillList(lstHistoricalFigure, World.HistoricalFigures, tabHistoricalFigure);
            FillList(lstHistoricalEvent, World.HistoricalEvents, tabHistoricalEvent);
            FillList(lstHistoricalEventCollection, World.HistoricalEventCollections, tabHistoricalEventCollection);
            FillList(lstHistoricalEra , World.HistoricalEras, tabHistoricalEra);
        }

        private void FillItemTreeviews()
        {
            this.InvokeEx(x =>
            {
                trvItemsByTypeMaterial.BeginUpdate();
                trvItemsByTypeMaterial.Nodes.Clear();
                var itemGroupings = World.Items.Values.GroupBy(i => i.ItemType).OrderBy(i => i.Key);

                foreach (var itemGrouping in itemGroupings)
                {
                    var typeNode = new TreeNode(itemGrouping.Key.ToTitleCase());
                    var typeCount = 0;

                    var subTypeGroups = itemGrouping.GroupBy(st => st.ItemSubType).OrderBy(st => st.Key);
                    foreach (var subTypeGroup in subTypeGroups)
                    {
                        var subTypeNode = subTypeGroup.Key == "" ? typeNode : new TreeNode(subTypeGroup.Key.ToTitleCase());

                        var subTypeCount = 0;

                        var matGroups = subTypeGroup.GroupBy(m => m.Material).OrderBy(m => m.Key);
                        foreach (var matGroup in matGroups)
                        {
                            var matNode = new TreeNode(matGroup.Key.ToTitleCase());

                            foreach (var item in matGroup)
                            {
                                var itemNode = new TreeNode(item.ToString()) { Tag = item };
                                matNode.Nodes.Add(itemNode);
                            }

                            subTypeCount += matGroup.Count();
                            matNode.Text += $" ({matGroup.Count()})";
                            subTypeNode.Nodes.Add(matNode);
                        }

                        typeCount += subTypeCount;
                        if (typeNode != subTypeNode)
                        {
                            subTypeNode.Text += $" ({subTypeCount})";
                            typeNode.Nodes.Add(subTypeNode);
                        }
                    }

                    typeNode.Text += $" ({typeCount})";
                    trvItemsByTypeMaterial.Nodes.Add(typeNode);
                }

                trvItemsByTypeMaterial.EndUpdate();

                trvItemsByQualityType.BeginUpdate();
                trvItemsByQualityType.Nodes.Clear();
                var itemQualityGroupings = World.Items.Values.GroupBy(i => i.Quality).OrderBy(i => i.Key)
                    .Select(g => new
                    {
                        ItemQuality = g.Key,
                        TypeGroups = g.ToList().GroupBy(i => i.ItemType)
                    });

                foreach (var itemQualityGrouping in itemQualityGroupings)
                {
                    var qualityString = itemQualityGrouping.ItemQuality.ToString().Replace('_', ' ');
                    var qLabel = Item.QualityLabels[itemQualityGrouping.ItemQuality];
                    if (qLabel != '\0')
                    {
                        qualityString = $"{qLabel}{qualityString}{qLabel}";
                    }
                    var qualityNode = new TreeNode(qualityString);
                    var typeCount = 0;
                    foreach (var typeGroup in itemQualityGrouping.TypeGroups)
                    {
                        var typeNode = new TreeNode(typeGroup.Key.ToTitleCase());
                        qualityNode.Nodes.Add(typeNode);
                        foreach (var itemNode in typeGroup.Select(item => new TreeNode(item.ToString().ToTitleCase()) { Tag = item }))
                        {
                            typeNode.Nodes.Add(itemNode);
                        }
                        typeCount += typeGroup.Count();
                        typeNode.Text += $" ({typeGroup.Count()})";
                    }
                    qualityNode.Text += $" ({typeCount})";
                    trvItemsByQualityType.Nodes.Add(qualityNode);
                }
                trvItemsByQualityType.EndUpdate();
            });
        }


        /// <summary>
        /// Fill listboxes (from a non-UI thread) in a AddRange() call from a generic list.
        /// Takes into account the current filters on that type of object before filling the list.
        /// </summary>
        /// <typeparam name="T">The type of the object which will fill our listbox</typeparam>
        /// <param name="listBox">The listbox to fill</param>
        /// <param name="list">The list of objects to fill from</param>
        /// <param name="tabPage">The page this listbox is on</param>
        public void FillList<T>(ListBox listBox, List<T> list, TabPage tabPage) where T : class
        {
            listBox.InvokeEx(f =>
            {
                f.BeginUpdate();
                f.Items.Clear();
                f.Items.AddRange(World.Filters[typeof(T)].Get(list).ToArray());
                f.EndUpdate();

            });


            if (!MainTab.TabPages.Contains(tabPage))
                this.InvokeEx(f => f.MainTab.TabPages.Add(tabPage));
        }

        /// <summary>
        /// Same as FillList<T>() except for use with World Dictionaries instead of World Lists (dictionaries are normally from the XML, where IDs are given)
        /// </summary>
        /// <typeparam name="T">The type of the object which will fill our listbox</typeparam>
        /// <typeparam name="TK">The type of the key for the dictionary, often int</typeparam>
        /// <param name="listBox">The listbox to fill</param>
        /// <param name="dict">The dictionary of objects to fill from</param>
        /// <param name="tabPage">The page this listbox is on</param>
        public void FillList<T, TK>(ListBox listBox, IDictionary<TK, T> dict, TabPage tabPage) where T : WorldObject
        {

            listBox.InvokeEx(f =>
            {
                f.BeginUpdate();
                f.Items.Clear();
                f.Items.AddRange(World.Filters[typeof(T)].Get(dict.Values.ToList()).ToArray());
                f.EndUpdate();
            });

            if (!MainTab.TabPages.Contains(tabPage) && listBox.Items.Count > 0)
                this.InvokeEx(f => f.MainTab.TabPages.Add(tabPage));
        }

    #endregion

        #region Loading Events
        /// <summary>
        /// These methods are called from the World object after being subscribed to, 
        ///     they allow the UI to operate while other threads are handling world *XML* loading.
        ///     Other loading is so quick it doesn't need events.
        /// </summary>

        /// <summary>
        /// Called when a specific section of XML starts loading.  
        /// </summary>
        private void XmlSectionStarted(string section)
        {
            Program.Log(LogType.Status, section + " Loading...");
        }

        /// <summary>
        /// Called when a specific section of XML is finished loading.  
        ///     After each is finished the details are written on the World Tab.  The first section writes the details from all the other files which are loaded first.
        /// </summary>
        private void XmlSectionFinished(string section)
        {
            Program.Log(LogType.Status, " Done");
            if (DFXMLParser.MemoryFailureQuitParsing)
                return;
            if (World.HasPlusXml && !World.IsPlusParsing) //Don't Provide Summary info or Fill Lists when there is still PlusXML to handle
                return;
            switch (section)
            {
                case "regions":
                    FillList(lstRegion, World.Regions, tabRegion);

                    AddSummaryItem(World.Name  + World.AltName  );
                    AddSummaryItem(@"Last Year: " + World.LastYear );
                    AddSummaryItem(@"Maps Found: " + World.Maps.Count  );
                    AddSummaryItem(@"History File" );
                    AddSummaryItem(@"Civilizations: " + World.Civilizations.Count, "History File",
                        new NavigationFilter(typeof(Civilization), new Filter("Name", "IsFull = true", "Race.Name", -1)));
                    AddSummaryItem(@"Gods: " + World.Gods.Count, "History File",
                        new NavigationFilter(typeof (God), new Filter("Name", null, null, -1)));
                    AddSummaryItem(@"Leaders: " + World.Leaders.Count, "History File",
                        new NavigationFilter(typeof (Leader), new Filter("Name", null, null, -1)));
                    AddSummaryItem(@"Site File" );
                    AddSummaryItem(@"Sites: " + World.SitesFile.Count, "Site File",
                        new NavigationFilter(typeof (Site), new Filter("Name", null, null, -1)));
                    AddSummaryItem(@"Entities: " + World.EntitiesFile.Count, "Site File",
                        new NavigationFilter(typeof (Entity), new Filter(new List<string> {"Name", "Type"}, null, null, -1)));
                    AddSummaryItem(@"Races: " + World.Races.Count, "Site File",
                        new NavigationFilter(typeof (Race), new Filter(new List<string> {"Name", "!isCivilized"}, null, null, -1)));
                    AddSummaryItem(@"XML" );
                    AddSummaryItem(@"Regions: " + World.Regions.Count, "XML",
                        new NavigationFilter(typeof (Region), new Filter("Name", null, null, -1)));
                    
                    break;
                case "underground_regions":
                    FillList(lstUndergroundRegion, World.UndergroundRegions, tabUndergroundRegion);
                    AddSummaryItem(@"Underground Regions: " + World.UndergroundRegions.Count, "XML",
                        new NavigationFilter(typeof (UndergroundRegion),
                            new Filter(new List<string> {"Depth", "Name"}, null, null, -1)));
                    break;
                case "entities":
                    FillList(lstEntity, World.Entities, tabEntity);
                    AddSummaryItem(@"Entities: " + World.Entities.Count, "XML",
                        new NavigationFilter(typeof (Entity), new Filter(new List<string> {"Name", "Type"}, null, null, -1)));
                    break;
                case "entity_populations":
                    FillList(lstEntityPopulation, World.EntityPopulations, tabEntityPopulation);
                    AddSummaryItem(@"Entity Populations: " + World.EntityPopulations.Count, "XML",
                        new NavigationFilter(typeof (EntityPopulation),
                            new Filter(new List<string> {"ID", "Race = null"}, null, null, -1)));
                    break;
                case "sites":
                    FillList(lstSite, World.Sites, tabSite);
                    AddSummaryItem(@"Sites: " + World.Sites.Count, "XML",
                        new NavigationFilter(typeof (Site), new Filter("Name", null, null, -1)));
                    break;
                case "world_constructions":
                    FillList(lstWorldConstruction, World.WorldConstructions, tabWorldConstruction);
                    
                    break;
                case "artifacts":
                    FillList(lstArtifact, World.Artifacts, tabArtifact);
                    AddSummaryItem(@"Artifacts: " + World.Artifacts.Count, "XML",
                        new NavigationFilter(typeof (Artifact), new Filter("Name", null, null, -1)));
                    break;
                case "historical_figures":
                    FillList(lstHistoricalFigure, World.HistoricalFigures, tabHistoricalFigure);
                    var hfLabel = @"Historical Figures: " + World.HistoricalFigures.Count ;
                    AddSummaryItem(hfLabel, "XML",
                        new NavigationFilter(typeof (HistoricalFigure), new Filter("Name", null, null, 50000)));

                    AddSummaryItem(@"Castes" , hfLabel);
                    foreach (var hfCaste in HistoricalFigure.Castes)
                    {
                        AddSummaryItem(
                            $@"{hfCaste}: {
                                World.HistoricalFigures.Values.Count(
                                    x => x.Caste.HasValue && HistoricalFigure.Castes[x.Caste.Value] == hfCaste)}", "Castes",
                            new NavigationFilter(typeof(HistoricalFigure), new Filter(new List<string> { "Name" }, new List<string> { "HFCaste == \"" + hfCaste + "\"" }, null, -1)));
                    }
                    
                    AddSummaryItem($@"{"*NONE*"}: {World.HistoricalFigures.Values.Count(x => !x.Caste.HasValue)}", "Castes");

                    AddSummaryItem(@"      Associated Types" , hfLabel);
                    foreach (var hfAssociatedType in HistoricalFigure.AssociatedTypes)
                    {
                        AddSummaryItem(
                            $@"{hfAssociatedType}: {
                                World.HistoricalFigures.Values.Count(
                                    x =>
                                        x.AssociatedType.HasValue &&
                                        HistoricalFigure.AssociatedTypes[x.AssociatedType.Value] == hfAssociatedType)}", "Associated Types",
                            new NavigationFilter(typeof(HistoricalFigure), new Filter(new List<string> { "Name" }, new List<string> { "Job == \"" + hfAssociatedType + "\"" }, null, -1)));
                    }

                    AddSummaryItem(
                        $@"{"*NONE*"}: {World.HistoricalFigures.Values.Count(x => !x.AssociatedType.HasValue)}", "Associated Types");


                    AddSummaryItem(@"Adventurers: " + World.HistoricalFigures.Values.Count(x => x.Adventurer), hfLabel,
                            new NavigationFilter(typeof(HistoricalFigure), new Filter(new List<string> { "Name" }, new List<string> { "Adventurer == true" }, null, -1)));
                    AddSummaryItem(@"Animated: " + World.HistoricalFigures.Values.Count(x => x.Animated), hfLabel,
                            new NavigationFilter(typeof(HistoricalFigure), new Filter(new List<string> { "Name" }, new List<string> { "Animated == true" }, null, -1)));
                    AddSummaryItem(@"Ghost: " + World.HistoricalFigures.Values.Count(x => x.Ghost), hfLabel,
                            new NavigationFilter(typeof(HistoricalFigure), new Filter(new List<string> { "Name" }, new List<string> { "Ghost == true" }, null, -1)));
                    AddSummaryItem(@"Deity: " + World.HistoricalFigures.Values.Count(x => x.Deity), hfLabel,
                            new NavigationFilter(typeof(HistoricalFigure), new Filter(new List<string> { "Name" }, new List<string> { "Deity == true" }, null, -1)));
                    AddSummaryItem(@"Force: " + World.HistoricalFigures.Values.Count(x => x.Force), hfLabel,
                            new NavigationFilter(typeof(HistoricalFigure), new Filter(new List<string> { "Name" }, new List<string> { "Force == true" }, null, -1)));


                    AddSummaryItem(@"Sphere Alignment", hfLabel);
                    for (var i = 0; i < HistoricalFigure.Spheres.Count; i++)
                    {
                        AddSummaryItem(HistoricalFigure.Spheres[i] + ": " +  World.HistoricalFigures.Values.Count(x => x.Sphere != null && x.Sphere.Contains(i)), @"Sphere Alignment");
                    }

                    AddSummaryItem(@"Has Pet: " + World.HistoricalFigures.Values.Count(x => x.JourneyPet != null), hfLabel);

                    break;
                case "historical_events":
                    FillList(lstHistoricalEvent, World.HistoricalEvents, tabHistoricalEvent);
                    var evtLabel = @"    Historical Events: " + World.HistoricalEvents.Count ;
                    AddSummaryItem(evtLabel, "XML",
                            new NavigationFilter(typeof(HistoricalEvent), new Filter("Year", null, null, 50000)));
                    foreach (var evtType in HistoricalEvent.Types)
                    {
                        AddSummaryItem(
                            $@"      {evtType}: {World.HistoricalEvents.Values.Count(x => x.EventType == evtType)}", evtLabel,
                            new NavigationFilter(typeof(HistoricalEvent), new Filter(new List<string> { "Year" }, new List<string> { "EventType == \"" + evtType + "\"" }, null, 50000)));
                    }
                    break;
                case "historical_event_collections":
                    FillList(lstHistoricalEventCollection, World.HistoricalEventCollections, tabHistoricalEventCollection);
                    var evtColLabel = @"    Historical Event Collections: " + World.HistoricalEventCollections.Count ;
                    AddSummaryItem(evtColLabel, "XML",
                            new NavigationFilter(typeof(HistoricalEventCollection), new Filter(new List<string> {"StartYear"}, null, null, 50000)));
                    foreach (var evtColType in HistoricalEventCollection.Types)
                    {
                        AddSummaryItem(
                            $@"      {evtColType}: {
                                World.HistoricalEventCollections.Values.Count(x => x.EventCollectionType == evtColType)
                                }", evtColLabel,
                            new NavigationFilter(typeof(HistoricalEventCollection), new Filter(new List<string> { "StartYear" }, new List<string> { "EventCollectionType == \"" + evtColType + "\"" }, null, 50000)));
                    }
                    break;
                case "historical_eras":
                    FillList(lstHistoricalEra, World.HistoricalEras, tabHistoricalEra);
                    AddSummaryItem(@"    Historical Eras: " + World.HistoricalEras.Count, "XML",
                        new NavigationFilter(typeof(HistoricalEra), new Filter("StartYear", null, null, -1)));
                    break;
            }
        }

        /// <summary>
        /// Adds an additional item to the World Summary Tree, optionally under a specific parent
        /// </summary>
        private void AddSummaryItem(string item, string parent = null, NavigationFilter navigationFilter = null)
        {
            //this.InvokeEx(f => f.WorldSummary.Text += item);
            this.InvokeEx(f =>
            {
                if (!(f.WorldSummaryTree.TreeViewNodeSorter is WorldSummaryNodeSorter))
                    f.WorldSummaryTree.TreeViewNodeSorter = new WorldSummaryNodeSorter();
            });
            if (parent == null)
                this.InvokeEx(f =>
                {
                    var itemNode = new TreeNode(item.Trim());
                    if (navigationFilter != null)
                    {
                        itemNode.Tag = navigationFilter;
                        itemNode.ForeColor = Color.Blue;
                    }
                    f.WorldSummaryTree.Nodes.Add(itemNode);
                });

                

            else
            {
                this.InvokeEx(f =>
                {
                    var parentNode = f.WorldSummaryTree.FlattenTree().FirstOrDefault(n => n.Text == parent.Trim());
                    if (parentNode == null) return;
                    var itemNode = new TreeNode(item.Trim());
                    if (navigationFilter != null)
                    {
                        itemNode.Tag = navigationFilter;
                        itemNode.ForeColor = Color.Blue;
                    }
                    parentNode.Nodes.Add(itemNode);
                });
            }

        }

        private void WorldSummaryTree_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (!(e.Node.Tag is NavigationFilter)) return;
            var filter = (NavigationFilter) e.Node.Tag;
            filter.Select(this);
        }

        private void WorldSummaryTree_MouseLeave(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.Default;
        }

        private void WorldSummaryTree_MouseMove(object sender, MouseEventArgs e)
        {
            var node = WorldSummaryTree.GetNodeAt(e.Location);

            if (node?.Tag is NavigationFilter)
                Cursor.Current = Cursors.Hand;
            else
                Cursor.Current = Cursors.Default;
        }

        /// <summary>
        /// Once all the sections are finished we spin off 4 different threads which handle making matching up the XML with the history/site files.
        /// Since the XML is all loaded we allow world exporting at this point, since nothing else after this point is data that's exported.
        /// Events are subscribed to for the "Linking" step, which turns associations to XML objects by IDs, to actual references to the corresponding object.
        /// </summary>
        private void XmlFinished()
        {
            Program.Log(LogType.Status, "XML Loading Done"); 

            World.MergeSites();
            World.MergeEntities();
            World.MergeCivs();
            World.MatchHistoricalFiguresToPeople();


            //Turn off export temporarily
            //this.InvokeEx(f => f.exportWorldToolStripMenuItem.Visible = true);
            
            Program.Log(LogType.Status, "XML Linking Started");

            World.LinkedSectionStart -= XmlSectionLinkedStart;
            World.LinkedSection -= XmlSectionLinked;
            World.Linked -= XmlLinked;

            World.LinkedSectionStart += XmlSectionLinkedStart;
            World.LinkedSection += XmlSectionLinked;
            World.Linked += XmlLinked;

            World.LinkXmlData();

        }

        /// <summary>
        /// When a section linking starts it's noted.
        /// </summary>
        private static void XmlSectionLinkedStart(string section)
        {
            Program.Log(LogType.Status, section + " Linking...");
        }

        /// <summary>
        /// After a section is linked, it's noted.
        /// </summary>
        private static void XmlSectionLinked(string section)
        {
            Program.Log(LogType.Status, " Done");
        }

        /// <summary>
        /// Once all the sections are linked we allow map and timeline viewing, further processing just improves data, which isn't necessary for those features.
        /// Events are subscribed to for the "Processing" step which makes data more efficiently pulled back, including filling various "events" lists which will be blank before that item is processed.
        /// Dynasties Created and Families Counted are event handlers which are called after completing the World.FamilyProcessing() method below.
        /// </summary>
        private void XmlLinked()
        {
            this.InvokeEx(f => f.showMapToolStripMenuItem.Visible = true);
            this.InvokeEx(f => f.timelineToolStripMenuItem.Visible = true);
            FillList(lstStructure, World.Structures, tabStructure);

            Program.IncrementMajorProgress();
            Program.Log(LogType.Status, "Add World Summary Items");
            AddSummaryItemsLearnedFromLinking();

            Program.Log(LogType.Status, "XML Linking Done");
            World.ProcessedSectionStart -= XmlSectionProcessedStart;
            World.ProcessedSection -= XmlSectionProcessed;
            World.Processed -= XmlProcessed;
            World.FamiliesCounted -= FamiliesCounted;
            World.DynastiesCreated -= DynastiesCreated;

            World.ProcessedSectionStart += XmlSectionProcessedStart;
            World.ProcessedSection += XmlSectionProcessed;
            World.Processed += XmlProcessed;
            World.FamiliesCounted += FamiliesCounted;
            World.DynastiesCreated += DynastiesCreated;
            World.EventsCounted += EventsCounted;

            Program.Log(LogType.Status, "XML Processing Started");

            World.ProcessXmlData();
        }

        /// <summary>
        /// After linking is complete, certain additional bits of World Summary Data can be dropped into the summary.
        /// </summary>
        private void AddSummaryItemsLearnedFromLinking()
        {
            Program.SetMinorProgress(0, 7);
            Program.IncrementMinorProgress();
            AddSummaryItem(@"World Constructions: " + World.WorldConstructions.Count, null,
                        new NavigationFilter(typeof(WorldConstruction), new Filter()));

            Program.IncrementMinorProgress();
            var hfLabel = @"    Historical Figures: " + World.HistoricalFigures.Count;
            AddSummaryItem(@"Races", hfLabel,
                        new NavigationFilter(typeof(Race), new Filter(new List<string> {"Name", "!isCivilized"}, null, null, -1)));

            Program.IncrementMinorProgress();
            foreach (var race in World.Races.Values)
            {
                var count = World.HistoricalFigures.Values.Count(x => Equals(x.Race, race));
                if (count == 0)
                    continue;
                AddSummaryItem($@"{race.Name}: {count}", "Races",
                            new NavigationFilter(typeof(HistoricalFigure), new Filter(new List<string> { "Name" }, new List<string> { "RaceName == \"" + race.Name + "\"" }, null, -1)));
            }

            Program.IncrementMinorProgress();
            AddSummaryItem(@"Alive: " + World.HistoricalFigures.Values.Count(x => !x.Dead), hfLabel,
                            new NavigationFilter(typeof(HistoricalFigure), new Filter(new List<string> { "Name" }, new List<string> { "Dead == false" }, null, -1)));
            Program.IncrementMinorProgress();
            AddSummaryItem(@" Dead: " + World.HistoricalFigures.Values.Count(x => x.Dead), hfLabel,
                            new NavigationFilter(typeof(HistoricalFigure), new Filter(new List<string> { "Name" }, new List<string> { "Dead == true" }, null, -1)));
            Program.IncrementMinorProgress();
            AddSummaryItem(@"Is Leader: " + World.HistoricalFigures.Values.Count(x => x.IsLeader), hfLabel,
                            new NavigationFilter(typeof(HistoricalFigure), new Filter(new List<string> { "Name" }, new List<string> { "isLeader == true" }, null, -1)));

        }

        /// <summary>
        /// After processing is complete, certain additional bits of World Summary Data can be dropped into the summary.
        /// </summary>
        private void AddSummaryItemsLearnedFromProcessing()
        {
            var hfLabel = @"    Historical Figures: " + World.HistoricalFigures.Count;

            AddSummaryItem(@"Prisoners: " + World.HistoricalFigures.Values.Count(x => x.PrisonerOf != null), hfLabel);
            AddSummaryItem(@"Slaves: " +  World.HistoricalFigures.Values.Count(x => x.SlaveOf != null), hfLabel);
            AddSummaryItem(@"Heroes: " + World.HistoricalFigures.Values.Count(x => x.HeroOf != null), hfLabel);
            AddSummaryItem(@"Had Children: " + World.HistoricalFigures.Values.Count(x => x.Children != null), hfLabel,
                            new NavigationFilter(typeof(HistoricalFigure), new Filter(new List<string> { "ID", "Race = null" }, new List<string> { "ChildrenCount > 0" }, null, -1)));
            AddSummaryItem(@"Childless: " + World.HistoricalFigures.Values.Count(x => x.Children == null), hfLabel,
                            new NavigationFilter(typeof(HistoricalFigure), new Filter(new List<string> { "ID", "Race = null" }, new List<string> { "ChildrenCount == 0" }, null, -1)));
            AddSummaryItem(@"Married: " + World.HistoricalFigures.Values.Count(x => x.Spouses != null), hfLabel);
            AddSummaryItem(@"Killers: " + World.HistoricalFigures.Values.Count(x => x.Kills > 0), hfLabel,
                            new NavigationFilter(typeof(HistoricalFigure), new Filter(new List<string> { "ID", "Race = null" }, new List<string> { "Kills > 0" }, null, -1)));

        }

        /// <summary>
        /// Before a section is processed, it's noted.
        /// </summary>
        private void XmlSectionProcessedStart(string section)
        {
            Program.Log(LogType.Status, section + " Processing...");
        }


        /// <summary>
        /// After a section is processed, it's noted.  The World.FamilyProcessing() method starts threads which create dynasties and 
        ///     count family members (this could take an enormous amount of time on old worlds, so it'll likely just keep running in the background as you view everything else)
        /// </summary>
        private void XmlSectionProcessed(string section)
        {
            Program.Log(LogType.Status, " Done");
            if (section == "Historical Figures")
                World.FamilyProcessing();
            if (section == "Historical Events")
                World.CountEvents();
        }

        /// <summary>
        /// When families are done being counted that's noted.  Once this is done the Ancestor and Descendnet counts in the Historical Figures filter option will give good results.
        /// </summary>
        static void FamiliesCounted()
        {
           
            Program.Log(LogType.Status, "Families Counted");
        }

        /// <summary>
        /// Fill Dynasties when they're created.  This is a new type of World Object I came up with since the data existed and it seems interesting.  See World.CreateDynasties() for details.
        /// </summary>
        void DynastiesCreated()
        {
            FillList(lstDynasty, World.Dynasties, tabDynasty);
            Program.Log(LogType.Status, "Dynasties Created");
            AddSummaryItem(@"    Dynasties: " + World.Dynasties.Count, "XML");
        }

        /// <summary>
        /// When events are done being counted that's noted.  Once this is done the sort by event counts will work
        /// </summary>
        static void EventsCounted()
        {
            Program.Log(LogType.Status, "Events Counted");
        }

        /// <summary>
        /// After all the sections have been processed (families might not have been counted yet, but that's okay)
        ///     We start Evaluating Event Collections - This gathers extra data by viewing Historical Events from their event collections, this lets us get new info like:
        ///         finding out where item's were stolen, what beast devoured a historical figure, and other things.  See World.EventCollectionEvaluation() for details.
        ///     and Positioning Historical figures - This pulls in all the data we have so far to try to place a historical figure in some location, it's incomplete but still interesting:
        ///         See World.HistoricalFiguresPositioning() for details.
        /// </summary>
        private void XmlProcessed()
        {
            Program.Log(LogType.Status, "XML Processing Done");

            Program.Log(LogType.Status, "Filling Lists");
            Program.IncrementMajorProgress();
            FillAllLists();

            Program.Log(LogType.Status, "Adding Items to World Summary");
            Program.IncrementMajorProgress();
            AddSummaryItemsLearnedFromProcessing();

            World.EventCollectionsEvaluated -= World_EventCollectionsEvaluated;
            World.HistoricalFiguresPositioned -= World_HistoricalFiguresPositioned;
            World.StatsGathered -= World_StatsGathered;

            World.EventCollectionsEvaluated += World_EventCollectionsEvaluated;
            World.HistoricalFiguresPositioned += World_HistoricalFiguresPositioned;
            World.StatsGathered += World_StatsGathered;

            World.EventCollectionEvaluation();
            World.HistoricalFiguresPositioning();
            World.StatsGathering();
        }

        private void World_EventCollectionsEvaluated()
        {
            Program.Log(LogType.Status, "Event Collections Evaluated");

            Program.Log(LogType.Status, $"Completed main load in {(DateTime.Now - World.StartTime).TotalSeconds} seconds.");
            Program.SetMajorProgress(1, 1);
            Program.SetMinorProgress(1, 1);

            //TODO: Implement Visualization Code:
            //World.VisualizationsCreated -= World_VisualizationsCreated;

            //World.VisualizationCreation();

            //TODO: Move to a better spot

            Program.Log(LogType.Status, "Calculating Notability");

            foreach (var xmlObject in World.HistoricalEvents.Values.SelectMany(evt => evt.Relationships))
            {
                xmlObject.Notability++;
            }
            foreach (var evt in World.HistoricalEvents.Values)
            {
                foreach (var xmlObject in evt.Relationships)
                {
                    evt.Notability += xmlObject.Notability;
                }
            }
            foreach (var evt in World.HistoricalEvents.Values)
            {
                foreach (var xmlObject in evt.Relationships)
                {
                    xmlObject.Notability += evt.Notability;
                }
            }
            Program.Log(LogType.Status, "Calculating Notability done");

            this.InvokeEx(f => f.closeWorldToolStripMenuItem.Visible = true);
        }

        /// <summary>
        ///  After trying to position historical figures, note how many were succesfully positioned out of all those who are alive (dead people aren't positioned because they're dead).
        /// </summary>
        private void World_HistoricalFiguresPositioned()
        {
            var hFsPositioned = World.HistoricalFigures.Values.Count(x => x.IsPositioned && !x.Dead);
            var hFsAlive = World.HistoricalFigures.Values.Count(x => !x.Dead);

            AddSummaryItemsLearnedFromPositioning();            

            Program.Log(LogType.Status, string.Format("Historical Figures Positioned (" + Math.Round(100.0f * hFsPositioned / hFsAlive, 0) + "% located)"));

            Program.Log(LogType.Status, $"Completed in {(DateTime.Now - World.StartTime).TotalSeconds} seconds.");
            Program.SetMajorProgress(1, 1);
            Program.SetMinorProgress(1, 1);
       }

        /// <summary>
        /// After hf positioning is complete, certain additional bits of World Summary Data can be dropped into the summary.
        /// </summary>
        private void AddSummaryItemsLearnedFromPositioning()
        {
            var hfLabel = @"    Historical Figures: " + World.HistoricalFigures.Count;
            AddSummaryItem(@" and Positioned: " + World.HistoricalFigures.Values.Count(x => x.IsPositioned && !x.Dead), hfLabel,
                            new NavigationFilter(typeof(HistoricalFigure), new Filter(new List<string> { "Name" }, new List<string> { "isPositioned == true", "Dead == false" }, null, -1)));
            AddSummaryItem(@" and Not Positioned: " + World.HistoricalFigures.Values.Count(x => !x.IsPositioned && !x.Dead), hfLabel,
                            new NavigationFilter(typeof(HistoricalFigure), new Filter(new List<string> { "Name" }, new List<string> { "isPositioned == false", "Dead == false" }, null, -1)));
        }


        private void World_StatsGathered()
        {
            Program.Log(LogType.Status, "World Stats Gathered");

            this.InvokeEx(f => f.statsToolStripMenuItem.Visible = true);
        }

        private void World_VisualizationsCreated() //Not used
        {
            Program.Log(LogType.Status, "Visualizations Created");

            this.InvokeEx(f => f.visualizationsToolStripMenuItem.Visible = true);
            this.InvokeEx(f => f.closeWorldToolStripMenuItem.Visible = true);
        }

        #endregion

        #region Entity Population Tab
        /// <summary>
        /// When an entity population is part of a war and we select that war we want to display that entity populations role in that war.
        /// </summary>
        private void lstEntityPopulationBattles_SelectedIndexChanged(object sender, EventArgs e)
        {
            var evtcol = (EC_Battle)lstEntityPopulationBattles.SelectedItem;
            var entpop = (EntityPopulation)lstEntityPopulation.SelectedItem;

            var number = 0;
            var deaths = 0;
            var squads = 0;
            if (evtcol.AttackingSquad != null)
            {
                foreach (var squad in evtcol.AttackingSquad.Where(squad => squad.EntityPopulation == entpop))
                {
                    squads++;
                    number += squad.Number;
                    deaths += squad.Deaths;
                }
            }
            if (evtcol.DefendingSquad != null)
            {
                foreach (var squad in evtcol.DefendingSquad.Where(squad => squad.EntityPopulation == entpop))
                {
                    squads++;
                    number += squad.Number;
                    deaths += squad.Deaths;
                }
            }
            if (squads == 0)
            {
                lblEntityPopulationBattleDeaths.Text = "";
                lblEntityPopulationBattleNumber.Text = "";
                lblEntityPopulationBattleTime.Data = null;
                lblEntityPopulationBattleTime.Text = "";
                lblEntityPopulationBattleWar.Data = null;

            }
            else
            {
                lblEntityPopulationBattleDeaths.Text = deaths.ToString();
                lblEntityPopulationBattleNumber.Text = number.ToString();
                lblEntityPopulationBattleTime.Data = evtcol;
                lblEntityPopulationBattleTime.Text = evtcol.StartTime.ToString();
                lblEntityPopulationBattleWar.Data = evtcol.WarEventCol;
            }
        }

        private void lstEntityPopluationRaces_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();

            if (e.Index != -1)
            {
                var thisEntPop = (EntityPopulation)lstEntityPopluationRaces.Tag ?? (EntityPopulation)lstEntityPopulation.SelectedItem;
                if (thisEntPop == null)
                {
                    grpEntityPopulation.Visible = false;
                    return;
                }
                string drawString;
                if (thisEntPop.RaceCounts[(Race)lstEntityPopluationRaces.Items[e.Index]] == 1)
                    drawString = thisEntPop.RaceCounts[(Race)lstEntityPopluationRaces.Items[e.Index]] +
                        " " + lstEntityPopluationRaces.Items[e.Index];
                else
                    drawString = thisEntPop.RaceCounts[(Race)lstEntityPopluationRaces.Items[e.Index]] +
                        " " + lstEntityPopluationRaces.Items[e.Index].ToString().Pluralize();

                e.Graphics.DrawString(drawString,
                    e.Font, Brushes.Black, e.Bounds, StringFormat.GenericDefault);
            }
            e.DrawFocusRectangle();
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
                var thisSite = (Site)lstSitePopulation.Tag ?? (Site)lstSite.SelectedItem;
                if (thisSite == null)
                {
                    grpSite.Visible = false;
                    return;
                }
                string drawString;

                if (thisSite.Population[(Race)lstSitePopulation.Items[e.Index]] == 1)
                    drawString = thisSite.Population[(Race)lstSitePopulation.Items[e.Index]] +
                        " " + lstSitePopulation.Items[e.Index];
                else
                    drawString = thisSite.Population[(Race)lstSitePopulation.Items[e.Index]] +
                        " " + ((Race)lstSitePopulation.Items[e.Index]).PluralizeName();

                e.Graphics.DrawString(drawString,
                    e.Font, Brushes.Black, e.Bounds, StringFormat.GenericDefault);
            }
            e.DrawFocusRectangle();
        }

        private void lstSitePrisoners_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();


            if (e.Index != -1)
            {
                var thisSite = (Site)lstSitePrisoners.Tag ?? (Site)lstSite.SelectedItem;
                if (thisSite == null)
                {
                    grpSite.Visible = false;
                    return;
                }

                string drawString;

                if (thisSite.Prisoners[(Race)lstSitePrisoners.Items[e.Index]] == 1)
                    drawString = thisSite.Prisoners[(Race)lstSitePrisoners.Items[e.Index]] +
                        " " + lstSitePrisoners.Items[e.Index];
                else
                    drawString = thisSite.Prisoners[(Race)lstSitePrisoners.Items[e.Index]] +
                        " " + ((Race)lstSitePrisoners.Items[e.Index]).PluralizeName();

                e.Graphics.DrawString(drawString,
                    e.Font, Brushes.Black, e.Bounds, StringFormat.GenericDefault);
            }
            e.DrawFocusRectangle();
        }

        private void lstSiteOutcasts_DrawItem(object sender, DrawItemEventArgs e)
        {

            e.DrawBackground();


            if (e.Index != -1)
            {
                var thisSite = (Site)lstSiteOutcasts.Tag ?? (Site)lstSite.SelectedItem;
                if (thisSite == null)
                {
                    grpSite.Visible = false;
                    return;
                }
                string drawString;

                if (thisSite.Outcasts[(Race)lstSiteOutcasts.Items[e.Index]] == 1)
                    drawString = thisSite.Outcasts[(Race)lstSiteOutcasts.Items[e.Index]] +
                        " " + lstSiteOutcasts.Items[e.Index];
                else
                    drawString = thisSite.Outcasts[(Race)lstSiteOutcasts.Items[e.Index]] +
                        " " + ((Race)lstSiteOutcasts.Items[e.Index]).PluralizeName();

                e.Graphics.DrawString(drawString,
                    e.Font, Brushes.Black, e.Bounds, StringFormat.GenericDefault);
            }
            e.DrawFocusRectangle();

        }
        #endregion

        #region Region/UGRegion Tabs
        private void lstRegionPopulation_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();

            if (e.Index != -1)
            {
                Dictionary<Race, int> pops = null;
                if (sender == lstRegionPopulation)
                {
                    var thisRegion = (Region)lstRegionPopulation.Tag ?? (Region) lstRegion.SelectedItem;
                    if (thisRegion == null)
                    {
                        grpRegion.Visible = false;
                        return;
                    }
                    pops = thisRegion.Populations;

                }
                else if (sender == lstUndergroundRegionPopulation)
                {
                    var thisUgRegion = (UndergroundRegion)lstUndergroundRegion.Tag ?? (UndergroundRegion) lstUndergroundRegion.SelectedItem;
                    if (thisUgRegion == null)
                    {
                        grpUndergroundRegion.Visible = false;
                        return;
                    }
                    pops = thisUgRegion.Populations;
                }

                var selectedRace = (Race) (sender as ListBox).Items[e.Index];
                var drawString = string.Empty;
                if (selectedRace != null && pops.ContainsKey(selectedRace))
                {
                    if (pops[selectedRace] == 1)
                        drawString = pops[selectedRace] +
                                     " " + selectedRace.Name.ToTitleCase();
                    else if (pops[selectedRace] == 10000001)
                        drawString = "Unnumbered " + selectedRace.PluralName.ToTitleCase();
                    else
                        drawString = pops[selectedRace] +
                                     " " + selectedRace.PluralName.ToTitleCase();
                }

                e.Graphics.DrawString(drawString,
                    e.Font, Brushes.Black, e.Bounds, StringFormat.GenericDefault);
            }
            e.DrawFocusRectangle();
        }
        #endregion

        /// <summary>
        /// Since civilizations contain leaders, sites, gods, and have wars...
        ///     These methods handle modifying how items are displayed in those listboxes.
        /// </summary>
        #region Civilization Tab
        private void lstCivilizationSites_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();

            if (e.Index != -1)
            {
                var thisSite = (Site)lstCivilizationSites.Items[e.Index];


                e.Graphics.DrawString(thisSite.Name,
                        e.Font, Brushes.Black, e.Bounds, StringFormat.GenericDefault);

                var lineAlignFormat = new StringFormat
                {
                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Near
                };

                e.Graphics.DrawString(thisSite.SiteType.ToTitleCase(),
                        e.Font, Brushes.Black, e.Bounds, lineAlignFormat);

                lineAlignFormat.Alignment = StringAlignment.Far;

                if (thisSite.Parent == null)
                    if (thisSite.Population.First().Value == 1)
                        e.Graphics.DrawString(thisSite.Population.First().Value + " " + thisSite.Population.First().Key,
                            e.Font, Brushes.Black, e.Bounds, lineAlignFormat);
                    else
                        e.Graphics.DrawString(thisSite.Population.First().Value + " " + thisSite.Population.First().Key.PluralizeName(),
                            e.Font, Brushes.Black, e.Bounds, lineAlignFormat);

                else
                {
                    if (thisSite.Population.ContainsKey(thisSite.Parent.Race))
                    {
                        if (thisSite.Population[thisSite.Parent.Race] == 1)
                            e.Graphics.DrawString(thisSite.Population[thisSite.Parent.Race] + " " + thisSite.Parent.Race,
                                e.Font, Brushes.Black, e.Bounds, lineAlignFormat);
                        else
                            e.Graphics.DrawString(thisSite.Population[thisSite.Parent.Race] + " " + thisSite.Parent.Race.PluralizeName(),
                                e.Font, Brushes.Black, e.Bounds, lineAlignFormat);

                    }
                    else if (thisSite.Population.Count > 0)
                    {
                        if (thisSite.Population.First().Value == 1)
                            e.Graphics.DrawString(thisSite.Population.First().Value + " " + thisSite.Population.First().Key,
                                e.Font, Brushes.Black, e.Bounds, lineAlignFormat);
                        else
                            e.Graphics.DrawString(thisSite.Population.First().Value + " " + thisSite.Population.First().Key.PluralizeName(),
                                e.Font, Brushes.Black, e.Bounds, lineAlignFormat);
                    }
                }

            }
            e.DrawFocusRectangle();
        }

        private void lstCivilizationLeaders_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();
            if (e.Index != -1)
            {
                var thisLeader = (Leader)lstCivilizationLeaders.Items[e.Index];

                e.Graphics.DrawString(
                    thisLeader.Name.Split(new[] { " the ", " The " }, StringSplitOptions.RemoveEmptyEntries)[0],
                    thisLeader.IsCurrent
                        ? new Font(e.Font.FontFamily.ToString(), e.Font.Size, FontStyle.Underline)
                        : e.Font, Brushes.Black, e.Bounds, StringFormat.GenericDefault);

                var typeString = thisLeader.Hf?.Caste != null
                    ? HistoricalFigure.Castes[thisLeader.Hf.Caste.Value].ToLower().ToTitleCase() + " "
                    : "";


                typeString += Leader.LeaderTypes[thisLeader.LeaderType].ToTitleCase();


                if (thisLeader.Site != null) //Leaders only in site file
                {
                    typeString += " at " + thisLeader.Site.Name;
                }

                if (thisLeader.ReignBegan != null)
                {
                    typeString += (thisLeader.ReignBegan.Year > -1
                           ? " from " + thisLeader.ReignBegan.Year
                           : "");
                }

                e.Graphics.DrawString(typeString,
                            e.Font, Brushes.Black, new PointF(e.Bounds.Width / 5 * 2, e.Bounds.Top), StringFormat.GenericDefault);

                var lineAlignFormat = new StringFormat
                {
                    Alignment = StringAlignment.Far,
                    LineAlignment = StringAlignment.Near
                };

                e.Graphics.DrawString(thisLeader.Race?.ToString() ?? (thisLeader.Hf?.Race?.ToString() ?? ""),
                    e.Font, Brushes.Black, e.Bounds, lineAlignFormat);


            }
            e.DrawFocusRectangle();
        }

        private void lstCivilizationGods_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();

            if (e.Index != -1)
            {
                var thisGod = (God)lstCivilizationGods.Items[e.Index];

                e.Graphics.DrawString(thisGod.Name.Split(new[] { " the ", " The " }, StringSplitOptions.RemoveEmptyEntries)[0],
                        e.Font, Brushes.Black, e.Bounds, StringFormat.GenericDefault);

                var lineAlignFormat = new StringFormat
                {
                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Near
                };

                e.Graphics.DrawString(God.Types[thisGod.Type],
                        e.Font, Brushes.Black, e.Bounds, lineAlignFormat);

                lineAlignFormat.Alignment = StringAlignment.Far;

                e.Graphics.DrawString(thisGod.Hf?.Race?.ToString() ?? "",
                    e.Font, Brushes.Black, e.Bounds, lineAlignFormat);

            }
            e.DrawFocusRectangle();
        }


        private void lstCivilizationWars_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();
            if (e.Index != -1)
            {
                var thisWar = (EC_War)lstCivilizationWars.Items[e.Index];
                if (!(lstCivilization.SelectedItem is WorldObject))
                    return;
                var thisCiv = (Civilization)lstCivilization.SelectedItem;
                e.Graphics.DrawString(thisWar.ToString(),
                        e.Font, Brushes.Black, e.Bounds, StringFormat.GenericDefault);

                // Create a StringFormat object with the each line of text, and the block 
                // of text centered on the page.
                var rightAlignFormat = new StringFormat
                {
                    Alignment = StringAlignment.Far,
                    LineAlignment = StringAlignment.Near
                };

                var timeString =
                    $"({thisWar.StartTime} - {(thisWar.EndTime != WorldTime.Present ? thisWar.EndTime.ToString() : "")})";
                e.Graphics.DrawString(timeString,
                    e.Font, Brushes.Black, e.Bounds, rightAlignFormat);



                e.Graphics.DrawString((thisWar.AggressorEnt == thisCiv.Entity ? "Defender: " + thisWar.DefenderEnt : "Aggressor: " + thisWar.AggressorEnt),
                    e.Font, Brushes.Black, new PointF(e.Bounds.Width / 8.0f, e.Bounds.Top + e.Bounds.Height / 2.0f), StringFormat.GenericDefault);

                e.Graphics.DrawString((thisWar.EndTime == WorldTime.Present
                        ? "Ongoing"
                        : WorldTime.Duration(thisWar.EndTime, thisWar.StartTime)),
                    e.Font, Brushes.Black, new PointF(e.Bounds.Width, e.Bounds.Top + e.Bounds.Height / 2.0f), rightAlignFormat);

            }
            e.DrawFocusRectangle();
        }
        #endregion

        #region Race Tab
        /// <summary>
        /// On Race tab, if the caste list is clicked call the select method on that caste
        /// </summary>
        private void Caste_ListClick(object sender, EventArgs e)
        {
            var listBox = (ListBox)sender;

            (listBox.SelectedItem as Caste)?.Select(this);
        }

        private void lstRacePopulation_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();

            if (e.Index != -1)
            {
                var thisRace = (Race)lstRacePopulation.Tag ?? (Race)lstRace.SelectedItem;
                if (thisRace == null)
                {
                    grpRace.Visible = false;
                    return;
                }
                var drawString = "";

                if (lstRacePopulation.Items[e.Index] is Region)
                {
                    var selectedRegion = (Region)lstRacePopulation.Items[e.Index];
                    if (thisRace.Populations[selectedRegion] == 10000001)
                        drawString = "Unnumbered - " + selectedRegion.Name.ToTitleCase();
                    else
                        drawString = thisRace.Populations[selectedRegion] +
                            " - " + selectedRegion.Name.ToTitleCase();
                }
                else if (lstRacePopulation.Items[e.Index] is UndergroundRegion)
                {
                    var selectedUgRegion = (UndergroundRegion)lstRacePopulation.Items[e.Index];
                    if (thisRace.UgPopulations[selectedUgRegion] == 10000001)
                        drawString = "Unnumbered - " + selectedUgRegion.ToString().ToTitleCase();
                    else
                        drawString = thisRace.UgPopulations[selectedUgRegion] +
                                     " - " + selectedUgRegion.ToString().ToTitleCase();
                }

                e.Graphics.DrawString(drawString,
                    e.Font, Brushes.Black, e.Bounds, StringFormat.GenericDefault);
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
            var squad = (Squad)((ListBox)sender).SelectedItem;

            lblBattleAttackingSquadSite.Data = squad.Site;
            lblBattleAttackingSquadRace.Data = squad.Race;
            lblBattleAttackingSquadNumber.Text = squad.Number.ToString();
            lblBattleAttackingSquadEntPop.Data = squad.EntityPopulation;
            lblBattleAttackingSquadDeaths.Text = squad.Deaths.ToString();
        }

        private void lstBattleDefendingSquad_SelectedIndexChanged(object sender, EventArgs e)
        {
            var squad = (Squad)((ListBox)sender).SelectedItem;

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

            if (e.Index != -1 && lstHistoricalEventCollection.SelectedItem is EC_Battle)
            {
                var thisBattle = (EC_Battle)lstBattleAttackingHF.Tag ?? (EC_Battle)lstHistoricalEventCollection.SelectedItem;
                var drawstring = thisBattle.AttackingHf[e.Index].ToString();

                var mColor = Color.Black;
                if (thisBattle.AttackingHf[e.Index].Caste.HasValue && HistoricalFigure.Castes[thisBattle.AttackingHf[e.Index].Caste.Value].ToLower().StartsWith("male"))
                    mColor = Color.Blue;
                else if (thisBattle.AttackingHf[e.Index].Caste.HasValue && HistoricalFigure.Castes[thisBattle.AttackingHf[e.Index].Caste.Value].ToLower().StartsWith("female"))
                    mColor = Color.Red; 
                  

                if (thisBattle.AttackingDiedHf != null && thisBattle.AttackingDiedHf.Contains(thisBattle.AttackingHf[e.Index]))
                    e.Graphics.DrawString(drawstring, new Font(e.Font.FontFamily.ToString(), e.Font.Size, FontStyle.Bold), new SolidBrush(mColor), e.Bounds);
                else
                    e.Graphics.DrawString(drawstring, e.Font, new SolidBrush(mColor), e.Bounds);

            }
            e.DrawFocusRectangle();
        }

        private void lstBattleDefendingHF_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();

            if (e.Index != -1)
            {
                var thisBattle = (EC_Battle)lstBattleDefendingHF.Tag ?? (EC_Battle)lstHistoricalEventCollection.SelectedItem;
                if (thisBattle.DefendingHf != null)
                {
                    var drawstring = thisBattle.DefendingHf[e.Index].ToString();


                    var mColor = Color.Black;
                    if (thisBattle.DefendingHf[e.Index].Caste.HasValue &&
                        HistoricalFigure.Castes[thisBattle.DefendingHf[e.Index].Caste.Value].ToLower()
                            .StartsWith("male"))
                        mColor = Color.Blue;
                    else if (thisBattle.DefendingHf[e.Index].Caste.HasValue &&
                             HistoricalFigure.Castes[thisBattle.DefendingHf[e.Index].Caste.Value].ToLower()
                                 .StartsWith("female"))
                        mColor = Color.Red;

                    if (thisBattle.DefendingDiedHf != null &&
                        thisBattle.DefendingDiedHf.Contains(thisBattle.DefendingHf[e.Index]))
                        e.Graphics.DrawString(drawstring,
                            new Font(e.Font.FontFamily.ToString(), e.Font.Size, FontStyle.Bold), new SolidBrush(mColor),
                            e.Bounds);
                    else
                        e.Graphics.DrawString(drawstring, e.Font, new SolidBrush(mColor), e.Bounds);
                }
            }
            e.DrawFocusRectangle();
        }

        /// <summary>
        /// On ANY tab if a listbox contains events selecting that listbox leads here, 
        ///     which dynamically generates controls as needed to write information on the event.
        /// </summary>
        private void EventCollection_EventsListClick(object sender, EventArgs e)
        {
            var listBox = (ListBox)sender;
            if (!(listBox.SelectedItem is HistoricalEvent)) 
                return;

            var evt = (HistoricalEvent)listBox.SelectedItem;
                
            evt.WriteDetailsOnParent(this, listBox.Parent, new Point(listBox.Left, listBox.Bottom + 10));
        }

        #endregion

        #region Unit Tab
        /// <summary>
        /// If a HF is clicked in the HF treeview, display event details underneath the tree view
        /// </summary>
        private void lstUnitRelations_DrawItem(object sender, DrawItemEventArgs e)
        {

            e.DrawBackground();


            if (e.Index != -1)
            {
                var thisUnit = (Unit)lstUnitRelations.Tag ?? (Unit)lstUnit.SelectedItem;
                if (thisUnit == null)
                {
                    grpUnit.Visible = false;
                    return;
                }

                if (thisUnit.Relations == null)
                {
                    grpUnitRelations.Visible = false;
                    return;
                }
                var selectedRelation = (HistoricalFigure)lstUnitRelations.Items[e.Index];

                string relationName = thisUnit.Relations.FirstOrDefault(x => x.Value == selectedRelation).Key.ToTitleCase();

                string drawString = $"{relationName} - {selectedRelation}";

                e.Graphics.DrawString(drawString,
                    e.Font, Brushes.Black, e.Bounds, StringFormat.GenericDefault);
            }
            e.DrawFocusRectangle();
        }
        #endregion

        #region Historical Figure Tab
        /// <summary>
        /// If a HF is clicked in the HF treeview, display event details underneath the tree view
        /// </summary>
        private void trvHistoricalFigureHFLinks_AfterSelect(object sender, TreeViewEventArgs e)
        {

            var treeView = (TreeView)sender;
            var selectedHfNode = e.Node;

            var thisHf = (HistoricalFigure)lstHistoricalFigure.SelectedItem;

            HistoricalEvent evt = null;
            if (selectedHfNode.Parent != null)
            {
                switch (selectedHfNode.Parent.Text.Split(' ')[0])
                {
                    case "Relationships":

                    case "Spouses":
                        if (thisHf.HfLinks != null)
                        {
                            foreach (var hfLinkList in thisHf.HfLinks)
                            {
                                foreach (var hfLink in hfLinkList.Value.Where(hfLink => hfLink.Hf == selectedHfNode.Tag))
                                {
                                    evt = hfLink.AddEvent;
                                    break;
                                }
                            }
                        }
                        break;
                    case "Kills":
                        if (thisHf.SlayingEvents != null)
                        {
                            foreach (var slayingEvent in thisHf.SlayingEvents.Where(slayingEvent => slayingEvent.Hf == selectedHfNode.Tag))
                            {
                                evt = slayingEvent;
                                break;
                            }
                        }
                        break;
                }
            }

            evt?.WriteDetailsOnParent(this, treeView.Parent, new Point(treeView.Left, treeView.Bottom + 10));
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
                    StartFilter(lstRegion, World.Regions, tabRegion);
                    break;
                case "FilterUndergroundRegion":
                    StartFilter(lstUndergroundRegion, World.UndergroundRegions, tabUndergroundRegion);
                    break;
                case "FilterEntity":
                    StartFilter(lstEntity, World.Entities, tabEntity);
                    break;
                case "FilterEntityPopulation":
                    StartFilter(lstEntityPopulation, World.EntityPopulations, tabEntityPopulation);
                    break;
                case "FilterSite":
                    StartFilter(lstSite, World.Sites, tabSite);
                    break;
                case "FilterWorldConstruction":
                    StartFilter(lstWorldConstruction, World.WorldConstructions, tabWorldConstruction);
                    break;
                case "FilterArtifact":
                    StartFilter(lstArtifact, World.Artifacts, tabArtifact);
                    break;
                case "FilterHistoricalFigure":
                    StartFilter(lstHistoricalFigure, World.HistoricalFigures, tabHistoricalFigure);
                    break;
                case "FilterHistoricalEvent":
                    StartFilter(lstHistoricalEvent, World.HistoricalEvents, tabHistoricalEvent);
                    break;
                case "FilterHistoricalEventCollection":
                    StartFilter(lstHistoricalEventCollection, World.HistoricalEventCollections, tabHistoricalEventCollection);
                    break;
                case "FilterHistoricalEra":
                    StartFilter(lstHistoricalEra, World.HistoricalEras, tabHistoricalEra);
                    break;
                case "FilterCivilization":
                    StartFilter(lstCivilization, World.Civilizations, tabCivilization);
                    break;
                case "FilterGod":
                    StartFilter(lstGod, World.Gods, tabGod);
                    break;
                case "FilterLeader":
                    StartFilter(lstLeader, World.Leaders, tabLeader);
                    break;
                case "FilterParameter":
                    StartFilter(lstParameter, World.Parameters, tabParameter);
                    break;
                case "FilterRace":
                    StartFilter(lstRace, World.Races, tabRace);
                    break;
                case "FilterDynasty":
                    StartFilter(lstDynasty, World.Dynasties, tabDynasty);
                    break;
                case "FilterStructure":
                    StartFilter(lstStructure, World.Structures, tabStructure);
                    break;
                case "FilterRiver":
                    StartFilter(lstRiver, World.Rivers, tabRiver);
                    break;
                case "FilterMountain":
                    StartFilter(lstMountain, World.Mountains, tabMountain);
                    break;
                case "FilterLandmass":
                    StartFilter(lstLandmass, World.Landmasses, tabLandmass);
                    break;
                case "FilterArmy":
                    StartFilter(lstArmy, World.Armies, tabArmy);
                    break;
                case "FilterUnit":
                    StartFilter(lstUnit, World.Units, tabUnit);
                    break;
                case "FilterEngraving":
                    StartFilter(lstEngraving, World.Engravings, tabEngraving);
                    break;
                case "FilterReport":
                    StartFilter(lstReport, World.Reports, tabReport);
                    break;
                case "FilterWrittenContent":
                    StartFilter(lstWrittenContent, World.WrittenContents, tabWrittenContent);
                    break;
                case "FilterPoeticForm":
                    StartFilter(lstPoeticForm, World.PoeticForms, tabPoeticForm);
                    break;
                case "FilterMusicalForm":
                    StartFilter(lstMusicalForm, World.MusicalForms, tabMusicalForm);
                    break;
                case "FilterDanceForm":
                    StartFilter(lstDanceForm, World.DanceForms, tabDanceForm);
                    break;
                case "FilterSquad":
                    StartFilter(lstSquad, World.Squads, tabSquad);
                    break;
                case "FilterBuilding":
                    StartFilter(lstBuilding, World.Buildings, tabBuilding);
                    break;
                case "FilterItem":
                    StartFilter(lstItem, World.Items, tabItem);
                    break;
                case "FilterPlant":
                    StartFilter(lstPlant, World.Plants, tabPlant);
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
            var filterForm = new FilterForm(World, typeof(T));
            var res = filterForm.ShowDialog();
            if (res == DialogResult.OK)
            {
                World.Filters[typeof(T)] = filterForm.OutFilter;

                FillList(listBox, list, tabPage);
            }
            filterForm.Dispose();
        }

        /// <summary>
        /// Same as StartFilter<T>() but with filtering dictionaries (usually XML data)
        /// </summary>
        /// <typeparam name="T">The type of object getting filtered</typeparam>
        /// <typeparam name="TK">The type of the key of the dictionary getting filtered</typeparam>
        /// <param name="listBox">The listbox to display into</param>
        /// <param name="dict">The dictionary we're filtering against</param>
        /// <param name="tabPage">The tabPage our listbox is on.</param>
        private void StartFilter<T, TK>(ListBox listBox, IDictionary<TK, T> dict, TabPage tabPage) where T : WorldObject
        {
            var filterForm = new FilterForm(World, typeof(T));
            World.Filters.LoadOptions();
            var res = filterForm.ShowDialog();
            if (res == DialogResult.OK)
            {
                World.Filters[typeof(T)] = filterForm.OutFilter;

                FillList(listBox, dict, tabPage);
            }
            filterForm.Dispose();
        }


        /// <summary>
        /// Depending on which Fitler textbox we change we want to start filtering against a different type of object.
        /// </summary>
        private void TextFilter_Changed(object sender, EventArgs e)
        {
            var txt = ((TextBox)sender).Text;
            switch (((TextBox)sender).Name)
            {
                case "TextFilterRegion":
                    TextFilter(txt, lstRegion, World.Regions, tabRegion);
                    break;
                case "TextFilterUndergroundRegion":
                    TextFilter(txt, lstUndergroundRegion, World.UndergroundRegions, tabUndergroundRegion);
                    break;
                case "TextFilterEntity":
                    TextFilter(txt, lstEntity, World.Entities, tabEntity);
                    break;
                case "TextFilterEntityPopulation":
                    TextFilter(txt, lstEntityPopulation, World.EntityPopulations, tabEntityPopulation);
                    break;
                case "TextFilterSite":
                    TextFilter(txt, lstSite, World.Sites, tabSite);
                    break;
                case "TextFilterWorldConstruction":
                    TextFilter(txt, lstWorldConstruction, World.WorldConstructions, tabWorldConstruction);
                    break;
                case "TextFilterArtifact":
                    TextFilter(txt, lstArtifact, World.Artifacts, tabArtifact);
                    break;
                case "TextFilterHistoricalFigure":
                    TextFilter(txt, lstHistoricalFigure, World.HistoricalFigures, tabHistoricalFigure);
                    break;
                case "TextFilterHistoricalEvent":
                    TextFilter(txt, lstHistoricalEvent, World.HistoricalEvents, tabHistoricalEvent);
                    break;
                case "TextFilterHistoricalEventCollection":
                    TextFilter(txt, lstHistoricalEventCollection, World.HistoricalEventCollections, tabHistoricalEventCollection);
                    break;
                case "TextFilterHistoricalEra":
                    TextFilter(txt, lstHistoricalEra, World.HistoricalEras, tabHistoricalEra);
                    break;
                case "TextFilterCivilization":
                    TextFilter(txt, lstCivilization, World.Civilizations, tabCivilization);
                    break;
                case "TextFilterGod":
                    TextFilter(txt, lstGod, World.Gods, tabGod);
                    break;
                case "TextFilterLeader":
                    TextFilter(txt, lstLeader, World.Leaders, tabLeader);
                    break;
                case "TextFilterParameter":
                    TextFilter(txt, lstParameter, World.Parameters, tabParameter);
                    break;
                case "TextFilterRace":
                    TextFilter(txt, lstRace, World.Races, tabRace);
                    break;
                case "TextFilterDynasty":
                    TextFilter(txt, lstDynasty, World.Dynasties, tabDynasty);
                    break;
                case "TextFilterStructure":
                    TextFilter(txt, lstStructure, World.Structures, tabStructure);
                    break;
                case "TextFilterRiver":
                    TextFilter(txt, lstRiver, World.Rivers, tabRiver);
                    break;
                case "TextFilterLandmass":
                    TextFilter(txt, lstLandmass, World.Landmasses, tabLandmass);
                    break;
                case "TextFilterMountain":
                    TextFilter(txt, lstMountain, World.Mountains, tabMountain);
                    break;
                case "TextFilterArmy":
                    TextFilter(txt, lstArmy, World.Armies, tabArmy);
                    break;
                case "TextFilterUnit":
                    TextFilter(txt, lstUnit, World.Units, tabUnit);
                    break;
                case "TextFilterEngraving":
                    TextFilter(txt, lstEngraving, World.Engravings, tabEngraving);
                    break;
                case "TextFilterReport":
                    TextFilter(txt, lstReport, World.Reports, tabReport);
                    break;
                case "TextFilterWrittenContent":
                    TextFilter(txt, lstWrittenContent, World.WrittenContents, tabWrittenContent);
                    break;
                case "TextFilterPoeticForm":
                    TextFilter(txt, lstPoeticForm, World.PoeticForms, tabPoeticForm);
                    break;
                case "TextFilterMusicalForm":
                    TextFilter(txt, lstMusicalForm, World.MusicalForms, tabMusicalForm);
                    break;
                case "TextFilterDanceForm":
                    TextFilter(txt, lstDanceForm, World.DanceForms, tabDanceForm);
                    break;
                case "TextFilterSquad":
                    TextFilter(txt, lstSquad, World.Squads, tabSquad);
                    break;
                case "TextFilterBuilding":
                    TextFilter(txt, lstBuilding, World.Buildings, tabBuilding);
                    break;
                case "TextFilterItem":
                    TextFilter(txt, lstItem, World.Items, tabItem);
                    break;
                case "TextFilterPlant":
                    TextFilter(txt, lstPlant, World.Plants, tabPlant);
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
            var tempFilter = "DispNameLower.Contains(\"" + txt.ToLower() + "\")";
            World.Filters[typeof(T)].Where.Add(tempFilter);

            FillList(listBox, list, tabPage);

            World.Filters[typeof(T)].Where.Remove(tempFilter);
        }

        /// <summary>
        /// Same as TextFilter<T>() but with a dictionary
        /// </summary>
        /// <typeparam name="T">The type of object getting filtered</typeparam>
        /// <typeparam name="TK">The type of the key of the dictionary getting filtered</typeparam>
        /// <param name="txt">The text to filter on</param>
        /// <param name="listBox">The listbox to display into</param>
        /// <param name="dict">The dictionary we're filtering against</param>
        /// <param name="tabPage">The tabPage our listbox is on.</param>
        private void TextFilter<T, TK>(string txt, ListBox listBox, IDictionary<TK, T> dict, TabPage tabPage) where T: WorldObject
        {

            var tempSelected = listBox.SelectedItem;

            var tempFilter = "DispNameLower.Contains(\"" + txt.ToLower() + "\")";
            World.Filters[typeof(T)].Where.Add(tempFilter);

            FillList(listBox, dict, tabPage);

            World.Filters[typeof(T)].Where.Remove(tempFilter);

            if (tempSelected != null)
            { 
            if (listBox.Items.Contains(tempSelected))
                listBox.SelectedItem = tempSelected;
            else
                listBox.SelectedIndex  = -1;
            }
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
            if (_navBackObjects.Count > 0 && _navBackObjects.Peek() == item)
            {
                return;
            }
            if (!_navigatingBack)
            {
                _navForwardObjects.Clear();
                ForwardtoolStripMenuItem.Enabled = false;
            }
            _navigatingBack = false;
            if (_navBackObjects.Count > 0)
                BacktoolStripMenuItem.ToolTipText =
                    $"{_navBackObjects.Peek()} ({_navBackObjects.Peek().GetType().Name})";
            _navBackObjects.Push(item);
            BacktoolStripMenuItem.Enabled = _navBackObjects.Count >= 2;
        }

        /// <summary>
        /// When a world is Closed, the Nav must be cleared
        /// </summary>
        internal void ClearNav()
        {
            _navBackObjects.Clear();
            _navForwardObjects.Clear();

            BacktoolStripMenuItem.Enabled = false;
            ForwardtoolStripMenuItem.Enabled = false;
        }

        /// <summary>
        /// When we go backwards we first pop off the first item (the one we're on now) then pop the previous item to travel back to it.
        /// </summary>
        private void BacktoolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Remove Top Object
            _navForwardObjects.Push(_navBackObjects.Pop());

            var selObject = _navBackObjects.Pop();

            _navigatingBack = true;

            selObject.Select(this);

            BacktoolStripMenuItem.Enabled = _navBackObjects.Count >= 2;
            ForwardtoolStripMenuItem.Enabled = _navForwardObjects.Count >= 1;
            if (ForwardtoolStripMenuItem.Enabled)
                ForwardtoolStripMenuItem.ToolTipText =
                    $"{_navForwardObjects.Peek()} ({_navForwardObjects.Peek().GetType().Name})";
        }

        /// <summary>
        /// When we go forwards we first pop off the first item in the Stack then pop the previous item to travel back to it.
        /// </summary>
        private void ForwardtoolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Remove Top Object
            var selObject = _navForwardObjects.Pop();
            _navBackObjects.Push(selObject);
            selObject.Select(this);

            BacktoolStripMenuItem.Enabled = _navBackObjects.Count >= 1;
            ForwardtoolStripMenuItem.Enabled = _navForwardObjects.Count >= 1;
        }
        
        #endregion

 
        private void SiteMapLabel_Click(object sender, EventArgs e)
        {
            if (lstSite.SelectedItem == null)
                return;
            if (Program.SiteMapForm == null || Program.SiteMapForm.IsDisposed)
            {
                Program.SiteMapForm = new SiteMapForm(World) {Location = Location};
            }
            Program.SiteMapForm.Site = (Site)lstSite.SelectedItem;
            Program.SiteMapForm.Show();
        }

        public void SetDisplayedItem(WorldObject obj)
        {
            if (!displayingItem.ContainsKey(obj.GetType()))
                displayingItem.Add(obj.GetType(), obj);
            else
                displayingItem[obj.GetType()] = obj;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void SummaryTextBox_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            var tagId = Convert.ToInt32(e.LinkText.Split('#').Last()) - 1;
            var objList = ((sender as RichTextBoxEx).Tag as List<WorldObject>);
            objList[tagId].Select(this);
        }

        private void PositionChildren(HistoricalFigure hf, Dictionary<int, int> curX)
        {
            const int GAP = 7;
            if (hf.Children != null)
            {
                if (hf.Children.First().Father != hf)
                    return;
                int gen = hf.Generation.Value;
                int curY = GAP * (gen + 1);
                foreach (var child in hf.Children)
                {
                    if (child.mapPt == Point.Empty)
                    {
                        if (!curX.ContainsKey(child.Generation.Value))
                            curX.Add(child.Generation.Value, GAP);
                        if (curX[child.Generation.Value] <= curX[gen])
                            curX[child.Generation.Value] = curX[gen];
                        else
                            curX[child.Generation.Value] += GAP;
                        child.mapPt = new Point(curX[child.Generation.Value], curY + GAP);
                    }
                }
            }

        }
    }
}
