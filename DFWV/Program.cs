using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using System.ComponentModel;
using DFWV.WorldClasses;
using System.Drawing;
using System.Globalization;
using System.IO;

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
        static public MainForm mainForm;
        static public MapForm mapForm;
        static public TimelineForm timelineForm;
        static public StatsForm statsForm;
        //static public VisualizationForm visualizationForm;

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            mainForm = new MainForm();
            
            Application.Run(mainForm);

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
            Program.mainForm.MainTab.SelectedTab = tabPage;
            if (!listBox.Items.Contains(item))
                listBox.Items.Add(item);
            listBox.SelectedItem = item;
            Program.mainForm.AddToNav(item);
        }


        /// <summary>
        /// This region is used to return a series of colors which are visually distinct from 
        /// one another for use in assigning colors to civilizations for mapping purposes
        /// </summary>
        #region DistinctColors
        private static int curDistinctColor = 0;
        private static List<string> ColorNames = new List<string>()
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
            Color thisColor;

            if (curDistinctColor < ColorNames.Count)
            {
                int rgb = Int32.Parse(ColorNames[curDistinctColor].Replace("#", ""), NumberStyles.HexNumber);
                thisColor = Color.FromArgb(255,Color.FromArgb(rgb));
                curDistinctColor++;
            }
            else
            {
                return Color.White;

            }

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

            
            foreach (char c in str)
            {
                if (!((c >= 'a' && c <= 'z') ||
                    (c >= 'A' && c <= 'Z') ||
                    (c == '-') ||
                    (c == ' ') || c == 'ê' || c == 'î' || c == 'ô' || c == 'á' || c == 'í' || c == 'ó' || c == (char)158 || c == 'ı' ||
                         c == 'ï' || c == '¿' || c == '½'))
                    Program.Log(LogType.Warning, "Unexpected character - " + c);
            }
            return str;
        }


        /// <summary>
        /// Handles all logging from world generation
        /// </summary>
        public static void Log(LogType type, string txt)
        {
            switch (type)
            {
                case LogType.Status:
                    mainForm.InvokeEx(x => x.StatusBox.Select(x.StatusBox.TextLength, 0));
                    mainForm.InvokeEx(x => x.StatusBox.SelectionColor = Color.Green);
                    mainForm.InvokeEx(x => x.StatusBox.AppendText(txt + Environment.NewLine));
                    Console.WriteLine("Status: " + txt);
                    break;
                case LogType.Warning:
                    mainForm.InvokeEx(x => x.StatusBox.Select(x.StatusBox.TextLength, 0));
                    mainForm.InvokeEx(x => x.StatusBox.SelectionColor = Color.Orange);
                    mainForm.InvokeEx(x => x.StatusBox.AppendText(txt + Environment.NewLine));
                    Console.WriteLine("Warning: " + txt);
                    break;
                case LogType.Error:
                    mainForm.InvokeEx(x => x.StatusBox.Select(x.StatusBox.TextLength, 0));
                    mainForm.InvokeEx(x => x.StatusBox.SelectionColor = Color.Red);
                    mainForm.InvokeEx(x => x.StatusBox.AppendText(txt + Environment.NewLine));
                    Console.WriteLine("ERROR: " + txt);
                    break;
                default:
                    break;
            }
            
        }

        public static string GetDefaultPath()
        {
            string workingFolder = Application.StartupPath;
            string defaultfolder = Properties.Settings.Default.DefaultFolder;
            defaultfolder = defaultfolder.Trim('"');
            defaultfolder = defaultfolder.TrimEnd('\\');

            if (defaultfolder.StartsWith(@"..\"))
            {
                while (defaultfolder.StartsWith(@"..\"))
                {
                    workingFolder = Directory.GetParent(workingFolder).FullName;
                    if (defaultfolder.Length > 3)
                        defaultfolder = defaultfolder.Substring(3);
                    else
                        defaultfolder = "";
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
            // Determine the Type we need to look out for
            Type searchType = typeof(T);

            foreach (Control c in ctrMain.Controls)
            {

                // If a match is found then yield this item back directly    
                if (c is T) yield return (c as T);

                // If the control hosts other controls then recursively call this function again.
                if (c.Controls.Count > 0)
                    foreach (T t in GetControlsOfType<T>(c))
                        yield return t;
            }
        }
    }


    /// <summary>
    /// This code calls an invoke against an object if it's needed to complete some action, otherwise it just calls that action.
    ///    This is used to update the UI from other threads.
    /// </summary>
    public static class ISynchronizeInvokeExtensions
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

    
}
