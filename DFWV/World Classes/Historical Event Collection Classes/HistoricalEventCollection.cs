using DFWV.WorldClasses.HistoricalEventClasses;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;
using DFWV.WorldClasses.HistoricalFigureClasses;


namespace DFWV.WorldClasses.HistoricalEventCollectionClasses
{

    class HistoricalEventCollection : XMLObject
    {
        public int StartYear { get; set; }
        public int StartSeconds { get; set; }
        public int EndYear { get; set; }
        public int EndSeconds { get; set; }
        public List<int> Event_ { get; set; }
        public List<HistoricalEvent> Event { get; set; }
        public static List<string> Types = new List<string>();
        public int Type { get; set; }

        public WorldTime StartTime { get; set; }
        public WorldTime EndTime { get; set; }

        public string DispNameLower { get { return ToString().ToLower(); } }
        public string EventCollectionType { get { return Types[Type]; } }

        override public Point Location { get { return Point.Empty; } }

        public int Combatants 
        {
            get
            {
                if (Types[Type] == "war")
                {
                    EC_War war = (EC_War)this;
                    return war.WarData.AttackingHFs + war.WarData.AttackingNumber + war.WarData.DefendingHFs + war.WarData.DefendingSquads;
                }
                else if (Types[Type] == "battle")
                {
                    EC_Battle battle = (EC_Battle)this;
                    return battle.BattleData.AttackingHFs + battle.BattleData.AttackingNumber + battle.BattleData.DefendingHFs + battle.BattleData.DefendingSquads;
                }
                else
                    return 0;
            }
        }

        public int Casualties
        {
            get
            {
                if (Types[Type] == "war")
                {
                    EC_War war = (EC_War)this;
                    return war.WarData.AttackingDeaths + war.WarData.DefendingDeaths;
                }
                else if (Types[Type] == "battle")
                {
                    EC_Battle battle = (EC_Battle)this;
                    return battle.BattleData.AttackingDeaths + battle.BattleData.DefendingDeaths;
                }
                else
                    return 0;
            }
        }

        public int Battles
        {
            get
            {
                if (Types[Type] == "war")
                {
                    EC_War war = (EC_War)this;
                    if (war.EventCol == null)
                        return 0;
                    else
                        return war.EventCol.Where(x => Types[x.Type] == "battle").Count();
                }
                else
                    return 0;
            }
        }

        public static HistoricalEventCollection Create(XDocument xdoc, World world)
        {
            switch (xdoc.Root.Element("type").Value.ToString())
            {
                case "abduction":
                    return new EC_Abduction(xdoc, world);
                case "battle":
                    return new EC_Battle(xdoc, world);
                case "beast attack":
                    return new EC_BeastAttack(xdoc, world);
                case "duel":
                    return new EC_Duel(xdoc, world);
                case "journey":
                    return new EC_Journey(xdoc, world);
                case "site conquered":
                    return new EC_SiteConquered(xdoc, world);
                case "theft":
                    return new EC_Theft(xdoc, world);
                case "war":
                    return new EC_War(xdoc, world);
                default:
                    string logtext = "Unassessed Event Collection Type: " + (xdoc.Root.Element("type").Value.ToString());// + raw.Replace("<", "//<") + "\n\t\t\tbreak;");
                    foreach (var ln in ("\t\t" + xdoc.Root.ToString().Replace("<", "//<")).Split('\n'))
                    {
                        if (!ln.Contains("<historical_event_collection>") &&
                            !ln.Contains("<id>") &&
                            !ln.Contains("<start_year>") &&
                            !ln.Contains("<start_seconds72>") &&
                            !ln.Contains("<end_year>") &&
                            !ln.Contains("<end_seconds72>") &&
                            !ln.Contains("<type>") &&
                            !ln.Contains("</historical_event_collection>"))
                            logtext += ln;
                    }
                    logtext += "\t\t\treturn new HistoricalEvent(xdoc, world);";

                    Program.Log(LogType.Warning, logtext);
                    return new EC_UnassessedEventCollection(xdoc, world);
            }
        }

        public HistoricalEventCollection(XDocument xdoc, World world)
            : base(xdoc, world)
        {

            foreach (XElement element in xdoc.Root.Elements())
            {
                string val = element.Value.ToString();
                int valI;
                Int32.TryParse(val, out valI);
                switch (element.Name.LocalName)
                {
                    case "id":
                        break;
                    case "start_year":
                        if (valI != -1)
                            StartYear = valI;
                        break;
                    case "start_seconds72":
                        if (valI != -1)
                            StartSeconds = valI;
                        break;
                    case "end_year":
                        if (valI != -1)
                            EndYear = valI;
                        break;
                    case "end_seconds72":
                        if (valI != -1)
                            EndSeconds = valI;
                        break;
                    case "event":
                        if (Event_ == null)
                            Event_ = new List<int>();
                        Event_.Add(valI);
                        break;
                    case "type":
                        if (!Types.Contains(val))
                            Types.Add(val);
                        Type = Types.IndexOf(val);
                        break;
                    default:
                        break;
                }
            }
        }

        public HistoricalEvent GetPreviousEvent(HistoricalEvent referenceEvt)
        {
            for (int i = 1; i < Event.Count; i++)
            {
                if (Event[i] == referenceEvt)
                    return Event[i - 1];
            }
            return null;
        }

        public HistoricalEvent GetNextEvent(HistoricalEvent referenceEvt)
        {
            for (int i = 0; i < Event.Count - 1; i++)
            {
                if (Event[i] == referenceEvt)
                    return Event[i + 1];
            }
            return null;
        }

        public override string ToString()
        {
            if (Name == null)
                return StartYear.ToString() + " - " + CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Types[Type]);
            else
                return Name;
        }

        internal string ToTimelineString()
        {
            return ToString();
        }

        public override void Select(MainForm frm)
        {
            frm.ClearEventDetails();

            frm.grpHistoricalEventCollection.Text = this.ToString();
            frm.grpHistoricalEventCollection.Show();
        }

        public void SelectTab(MainForm frm)
        {
        
            frm.lstHistoricalEventCollection.Focus();
            Program.MakeSelected(frm.tabHistoricalEventCollection, frm.lstHistoricalEventCollection, this);

        }

        internal void LinkFieldList<T>(List<int> IDs, List<T> list, 
                                      Dictionary<int, T> fromList)
        {
            if (IDs == null)
                return;
            for (int i = 0; i < IDs.Count; i++)
			{
                if (fromList.ContainsKey(IDs[i]))
                {
                    list.Add(fromList[IDs[i]]);
                }
			}
        }
        
        internal override void Link()
        {
            StartTime = new WorldTime(StartYear, StartSeconds);
            if (EndYear == -1)
                EndTime = WorldTime.Present;
            else 
                EndTime = new WorldTime(EndYear, EndSeconds);
            if (Event_ != null)
            {
                Event = new List<HistoricalEvent>();
                LinkFieldList<HistoricalEvent>(Event_,
                    Event, World.HistoricalEvents);
                foreach (HistoricalEvent evt in Event)
                    evt.EventCollection = this;
            }

        }
        

        internal override void Process()
        {
            
        }


        internal override void Export(string table)
        {

            List<object> vals;

            vals = new List<object>() { ID, StartYear, StartSeconds, EndYear, EndSeconds, Types[Type] };


            Database.ExportWorldItem(table, vals);


            if (Event == null)
                return;
            table = "EC_Events";
            foreach (HistoricalEvent evt in Event)
	        {
                vals = new List<object>() {ID,evt.ID };
                Database.ExportWorldItem(table, vals);

	        }
        }

    }

}
