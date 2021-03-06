﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity.Design.PluralizationServices;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using DFWV.Controls;
using DFWV.Properties;
using DFWV.WorldClasses;
using SevenZip;

//TODO: Match up property names across HE classes

//TODO: Validate linking on all events
//TODO: Validate processing on all events
//TODO: Validate WriteDataOnParent on all events
//TODO: Validate LegendsDescription on all events
//TODO: Validate ToTimelineString on all events
//TODO: Validate Export on all events

//TODO: Validate linking on all xml world classes
//TODO: Validate processing on all xml world classes
//TODO: Validate select on all xml world classes

//TOOD: Generic reflection based Linker method?


//TODO: Export doesn't properly release link to exported DB (can't be deleted while app is running following export
//TODO: Add Total Population listing in civ screen
//TODO: Group gods by first civ
//TODO: Dead civs filter option, display it
//TODO: Leaders sorting by reign started and by civ
//TODO: Group races by civilized or not
//TODO: Better arrange Unit List


namespace DFWV
{
    public enum LogType
    {
        Status,
        Warning,
        Error
    }

    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// 
        static public MainForm MainForm;
        static public MapForm MapForm;
        static public SiteMapForm SiteMapForm;
        static public TimelineForm TimelineForm;
        static public StatsForm StatsForm;
        //static public VisualizationForm visualizationForm;

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            InitiailzePluralService();
            MainForm = new MainForm();
            
            Application.Run(MainForm);

        }

        /// <summary>
        /// This function is called at the end of most WorldObject's Select() call.  
        /// It 
        ///  * Selects the appropriate tab, 
        ///  * Adds the item to the listbox if it isn't there (in the case of long worlds the events listbox is limited by default for example)
        ///  * Selects that item
        ///  * Adds that item to the navigation system, used to scroll backward through items you've viewed
        /// </summary>
        public static void MakeSelected(TabPage tabPage, ListBox listBox, WorldObject item)
        {
            MainForm.AddToNav(item);
            MainForm.MainTab.SelectedTab = tabPage;
            if (!listBox.Items.Contains(item))
                listBox.Items.Add(item);
            if (listBox.SelectedItem != item)
                listBox.SelectedItem = item;
        }

        /// <summary>
        /// This function is called when an Item on the WorldSummaryTree is double clicked.  
        /// It 
        ///  * Selects the appropriate tab, 
        ///  * Selects the first item on the listbox if there are any.
        ///  * Adds that item to the navigation system, used to scroll backward through items you've viewed
        /// </summary>
        public static void MakeSelected(TabPage tabPage, ListBox listBox)
        {
            MainForm.MainTab.SelectedTab = tabPage;
            if (listBox.SelectedIndex == -1 && listBox.Items.Count > 0)
                listBox.SelectedIndex = 0;
        }


        /// <summary>
        /// This region is used to return a series of colors which are visually distinct from 
        /// one another for use in assigning colors to civilizations for mapping purposes
        /// </summary>
        #region DistinctColors
        private static int _curDistinctColor;
        public static List<string> ColorNames = new List<string>
        {"#00FF00", "#0000FF", "#FF0000", "#01FFFE", "#FFA6FE", "#FFDB66", "#006401", "#010067", 
                    "#95003A", "#007DB5", "#FF00F6", "#FFEEE8", "#774D00", "#90FB92", "#0076FF", "#D5FF00", 
                    "#FF937E", "#6A826C", "#FF029D", "#FE8900", "#7A4782", "#7E2DD2", "#85A900", "#FF0056", 
                    "#A42400", "#00AE7E", "#683D3B", "#BDC6FF", "#263400", "#BDD393", "#00B917", "#9E008E", 
                    "#001544", "#C28C9F", "#FF74A3", "#01D0FF", "#004754", "#E56FFE", "#788231", "#0E4CA1", 
                    "#91D0CB", "#BE9970", "#968AE8", "#BB8800", "#43002C", "#DEFF74", "#00FFC6", "#FFE502", 
                    "#620E00", "#008F9C", "#98FF52", "#7544B1", "#B500FF", "#00FF78", "#FF6E41", "#005F39", 
                    "#6B6882", "#5FAD4E", "#A75740", "#A5FFD2", "#FFB167", "#009BFF", "#E85EBE"};
        public static Color NextDistinctColor()
        {
            var rgb = int.Parse(ColorNames[_curDistinctColor % ColorNames.Count].Replace("#", ""), NumberStyles.HexNumber);
            var thisColor = Color.FromArgb(255,Color.FromArgb(rgb));
            _curDistinctColor++;

            return thisColor;
        }

        #endregion

        /// <summary>
        /// Returns a string with all the special unicode characters replaced with corresponding characters.
        /// Some weird characters come up when using popular mods, and they are replaced as well.
        /// If a new weird character is found a note is thrown up about it.  
        /// These strings are cleaned so that the XML and the Site/History files can be compared, the latter uses names with special characters.
        /// In most cases even if a new character is found a follow up method which does a fuzzier match will still make the connection, so just a single character warning is fine.
        /// </summary>
        internal static string CleanString(string str)
        {
            str = str.Replace('æ', 'a');
            str = str.Replace('â', 'a');
            str = str.Replace('á', 'a');
            str = str.Replace('å', 'a');
            str = str.Replace('à', 'a');
            str = str.Replace('ä', 'a');
            str = str.Replace('à', 'a');
            str = str.Replace('ê', 'e');
            str = str.Replace('ë', 'e');
            str = str.Replace('è', 'e');
            str = str.Replace('é', 'e');
            str = str.Replace('í', 'i');
            str = str.Replace('ì', 'i');
            str = str.Replace('î', 'i');
            str = str.Replace('ï', 'i');
            str = str.Replace('ñ', 'n');
            str = str.Replace('ô', 'o');
            str = str.Replace('ò', 'o');
            str = str.Replace('ö', 'o');
            str = str.Replace('ó', 'o');
            str = str.Replace('ú', 'u');
            str = str.Replace('û', 'u');
            str = str.Replace('ù', 'u');
            str = str.Replace('ü', 'u');
            str = str.Replace('ÿ', 'y');

            //Modded games only it appears
            str = str.Replace('ω', 'ê');
            str = str.Replace('ε', 'î');
            str = str.Replace('⌠', 'ô');
            str = str.Replace('ß', 'á');
            str = str.Replace('φ', 'í');
            str = str.Replace('≤', 'ó');
            str = str.Replace('₧', (char)158);
            str = str.Replace('²', 'ı');
            str = str.Replace('∩', 'ï');
            str = str.Replace('┐', '¿');
            str = str.Replace('╜', '½');


            
            foreach (var c in str.Where(c => !((c >= 'a' && c <= 'z') ||
                                               (c >= 'A' && c <= 'Z') ||
                                               (c == '-') ||
                                               (c == ' ') || c == 'ê' || c == 'î' || c == 'ô' || c == 'á' || c == 'í' || c == 'ó' || c == (char)158 || c == 'ı' ||
                                               c == 'ï' || c == '¿' || c == '½')))
            {
                Log(LogType.Warning, "Unexpected character - " + c);
            }
            return str;
        }

        public static void IncrementMinorProgress() => SetProgress("Minor", MainForm.WorldProgressBarMinor.Value + 1);
        public static void IncrementMajorProgress() => SetProgress("Major", MainForm.WorldProgressBarMajor.Value + 1);
        public static void SetMinorProgress(int value, int max = -1) => SetProgress("Minor", value, max);
        public static void SetMajorProgress(int value, int max = -1) => SetProgress("Major", value, max);

        private static void SetProgress(string progressBarType, int value, int max = -1)
        {
            MainForm.InvokeEx(x =>
            {
                ProgressBar progressBar = progressBarType == "Minor" ? x.WorldProgressBarMinor : x.WorldProgressBarMajor;

                if (max != -1)
                    progressBar.Maximum = max;
                if (value == 0 || value > progressBar.Value)
                {
                    progressBar.Value = value <= progressBar.Maximum ? value : progressBar.Maximum;
                }
            });
        }


        /// <summary>
        /// Handles all logging from world generation
        /// </summary>
        private static readonly object ThisLock = new object();

        public static void Log(LogType type, string txt)
        {
            if (MainForm == null)
                return;
            lock (ThisLock)
            {
                var curText = (string) MainForm.StatusBox.Invoke(new Func<string>(() => MainForm.StatusBox.Text));

                switch (type)
                {
                    case LogType.Status:
                        if (txt.EndsWith("..."))
                            MainForm.InvokeEx(x => x.StatusBox.AppendText(txt));
                        else if (txt == " Done") //ensure that sections that are finished are put in the right place
                        {

                            if (curText.EndsWith("\n"))
                                //Another section finished while this section was in progress
                            {

                                var elipsisPos =
                                    (int)
                                        MainForm.StatusBox.Invoke(
                                            new Func<int>(() => MainForm.StatusBox.Text.LastIndexOf("...",
                                                StringComparison.Ordinal)));

                                MainForm.InvokeEx(x => x.StatusBox.Text = x.StatusBox.Text.Insert(elipsisPos + 3, txt));
                            }
                            else
                                MainForm.InvokeEx(x => x.StatusBox.AppendText(txt + Environment.NewLine));
                        }
                        else
                        {
                            if (curText.EndsWith("\n") ||
                                curText == string.Empty)
                                MainForm.InvokeEx(x => x.StatusBox.AppendText(txt + Environment.NewLine));
                            else //Section finished while another section is in progress
                                MainForm.InvokeEx(
                                    x => x.StatusBox.AppendText(Environment.NewLine + txt + Environment.NewLine));
                        }
                        Console.WriteLine(@"Status: {0}", txt);
                        break;
                    case LogType.Warning:
                        MainForm.InvokeEx(x =>
                        {
                            x.IssuesBox.Select(x.IssuesBox.TextLength, 0);
                            x.IssuesBox.SelectionColor = Color.Orange;
                            x.IssuesBox.AppendText(txt + Environment.NewLine);
                        });

                        Console.WriteLine(@"Warning: {0}", txt);
                        break;
                    case LogType.Error:
                        MainForm.InvokeEx(x =>
                        {
                            x.IssuesBox.Select(x.IssuesBox.TextLength, 0);
                            x.IssuesBox.SelectionColor = Color.Red;
                            x.IssuesBox.AppendText(txt + Environment.NewLine);
                        });
                        Console.WriteLine(@"ERROR: {0}", txt);
                        break;
                }
            }
        }

        /// <summary>
        /// Handles the access to the default path, which makes it more convenient to load details from worlds.
        /// </summary>
        public static string GetDefaultPath()
        {
            var workingFolder = Application.StartupPath;
            var defaultfolder = Settings.Default.DefaultFolder;
            defaultfolder = defaultfolder.Trim('"');
            defaultfolder = defaultfolder.TrimEnd('\\');

            if (defaultfolder.StartsWith(@"..\"))
            {
                while (defaultfolder.StartsWith(@"..\"))
                {
                    workingFolder = Directory.GetParent(workingFolder).FullName;
                    defaultfolder = defaultfolder.Length > 3 ? defaultfolder.Substring(3) : "";
                }
            }

            if (Directory.Exists(defaultfolder))
            {
                workingFolder = defaultfolder;
            }
            else if (Directory.Exists(workingFolder + "\\" + defaultfolder))
            {
                workingFolder = workingFolder + "\\" + defaultfolder;
            }

            return workingFolder;
        }

        /// <summary>
        /// Returns all controls within a control of a specific type.
        /// This is used to pull back all listboxes and link their events, or all linklabels and link their events.
        /// </summary>
        public static IEnumerable<T> GetControlsOfType<T>(Control ctrMain) where T : class
        {
            foreach (Control c in ctrMain.Controls)
            {

                // If a match is found then yield this item back directly    
                if (c is T) yield return (c as T);

                // If the control hosts other controls then recursively call this function again.
                if (c.Controls.Count <= 0) continue;
                foreach (var t in GetControlsOfType<T>(c))
                    yield return t;
            }
        }

        /// <summary>
        /// Extracts a 7z archive at the given path for file loading
        /// </summary>
        public static bool ExtractArchive(string path)
        {
            Console.WriteLine(Directory.GetCurrentDirectory());

            SevenZipBase.SetLibraryPath(IntPtr.Size == 8
                ? Path.Combine(Directory.GetCurrentDirectory(), @"7z64.dll")
                : Path.Combine(Directory.GetCurrentDirectory(), @"7z.dll"));

            var directory = Path.GetDirectoryName(path);
            var filename = Path.GetFileNameWithoutExtension(path);

            if (directory == null)
                return false;
            var newDirectory = Path.Combine(directory, filename);
            if (Directory.Exists(newDirectory))
                Directory.Delete(newDirectory, true);
            Directory.CreateDirectory(newDirectory);

            using (var tmp = new SevenZipExtractor(path))
            {
                foreach (var fileinfo in tmp.ArchiveFileData)
                    tmp.ExtractFiles(newDirectory, fileinfo.Index);
            }
            return true;
        }

        /// <summary>
        /// Takes a groupbox and loads an inner Listbox with a list of objects efficiently
        /// </summary>
        internal static void FillListboxWith(this GroupBox groupbox, ListBox listbox, IEnumerable<object> objects, object listboxTag = null)
        {
            try
            {
                var objectsAsArray = objects as object[] ?? objects?.ToArray();
                if (objects == null || !objectsAsArray.Any())
                {
                    groupbox.Visible = false;
                    return;
                }
                groupbox.Visible = true;
                listbox.BeginUpdate();
                if (listboxTag != null)
                    listbox.Tag = listboxTag;
                listbox.Items.Clear();
                listbox.Items.AddRange(objectsAsArray.ToArray());
                listbox.EndUpdate();
                listbox.SelectedIndex = 0;
                //typeof(ListBox).InvokeMember("RefreshItems",
                //  BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.InvokeMethod,
                //  null, listbox, new object[] { });
                var title = groupbox.Text.Split('(')[0].Trim();
                groupbox.Text = $"{title} ({listbox.Items.Count})";
            }
            catch (InvalidOperationException e)
            {
                if (e.Message == "Collection was modified; enumeration operation may not execute.")
                {
                    groupbox.Visible = false;
                    listbox.EndUpdate();
                }
                else
                {
                    throw;
                }
                
            }
        }


        public static void AddText(this RichTextBoxEx rtb, string text)
        {
            rtb.SelectedText = text;            
        }

        public static void AddLink(this RichTextBoxEx rtb, WorldObject obj, string title = "")
        {
            if (obj == null)
            {
                rtb.AddText(title == "" ? "Null" : title);
            }
            else
            {
                var tags = rtb.Tag as List<WorldObject>;
                tags.Add(obj);
                rtb.InsertLink(title == "" ? obj.ToString() : title, tags.Count.ToString(), rtb.TextLength);
            }
        }


        #region Pluralization/Capitalization Services
        static PluralizationService _pluralService;
        private static void InitiailzePluralService()
        {
            _pluralService = PluralizationService.CreateService(CultureInfo.GetCultureInfo("en-us"));
        }

        static public string Pluralize(this string str)
        {
            if (_pluralService == null)
                InitiailzePluralService();
            return _pluralService.Pluralize(str);
        }

        static public string Singularize(this string str)
        {
            if (_pluralService == null)
                InitiailzePluralService();
            return _pluralService.Singularize(str);
        }

        static public bool IsPlural(this string str)
        {
            if (_pluralService == null)
                InitiailzePluralService();
            return _pluralService.IsPlural(str);
        }

        static public bool IsSingular(this string str)
        {
            if (_pluralService == null)
                InitiailzePluralService();
            return _pluralService.IsSingular(str);
        }

        static public string ToTitleCase(this string str)
        {
            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(str ?? "");
        }

        #endregion

    }


    /// <summary>
    /// This code calls an invoke against an object if it's needed to complete some action, otherwise it just calls that action.
    ///    This is used to update the UI from other threads.
    /// </summary>
    public static class SynchronizeInvokeExtensions
    {
      public static void InvokeEx<T>(this T @this, Action<T> action) where T : ISynchronizeInvoke
      {
        if (@this.InvokeRequired)
        {
          @this.Invoke(action, new object[] { @this });
        }
        else
        {
          action(@this);
        }
      }
    }

    /// <summary>
    /// These methods are used with the World Summary TreeView (on the World tab on the main form) to allow it to properly sort and insert data as required.
    /// </summary>
    #region WorldSummaryTree Helper Functions
    public static class SoExtension
    {
        public static IEnumerable<TreeNode> FlattenTree(this TreeView tv)
        {
            return FlattenTree(tv.Nodes);
        }

        public static IEnumerable<TreeNode> FlattenTree(this TreeNodeCollection coll)
        {
            return coll.Cast<TreeNode>()
                        .Concat(coll.Cast<TreeNode>()
                                    .SelectMany(x => FlattenTree(x.Nodes)));
        }

        public static IEnumerable<T> ConsecutiveDistict<T>(this IEnumerable<T> input)
        {
            if (input == null) throw new ArgumentNullException(nameof(input));
            return ConsecutiveDistictImplementation(input);
        }

        static IEnumerable<T> ConsecutiveDistictImplementation<T>(this IEnumerable<T> input)
        {
            var isFirst = true;
            var last = default(T);
            foreach (var item in input)
            {
                if (isFirst || !Equals(item, last))
                {
                    yield return item;
                    last = item;
                    isFirst = false;
                }
            }
        }
    }

    // Create a node sorter that implements the IComparer interface. 
    public class WorldSummaryNodeSorter : IComparer
    {
        // Compare the length of the strings, or the strings 
        // themselves, if they are the same length. 
        public int Compare(object x, object y)
        {
            var tx = x as TreeNode;
            var ty = y as TreeNode;

            if (ty != null && (tx != null && (!tx.Text.Contains(":") || tx.Nodes.Count > 0 || ty.Nodes.Count > 0)))
                return -1;
            if (!ty.Text.Contains(":"))
                return 1;
            return Convert.ToInt32(ty.Text.Split(':')[1]) - Convert.ToInt32(tx.Text.Split(':')[1]);
        }
    }

#endregion
}
