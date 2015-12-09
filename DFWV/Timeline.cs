using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Resources;
using System.Windows.Forms;
using DFWV.WorldClasses;
using DFWV.WorldClasses.HistoricalEventClasses;
using DFWV.WorldClasses.HistoricalEventCollectionClasses;

namespace DFWV
{
    public partial class TimelineForm : Form
    {
        readonly World _world;
        int _lastEvent;
        readonly List<XmlObject> _worldEvents = new List<XmlObject>();

        private readonly List<string> _criticalEventTypes = new List<string>
        { "created site", "entity created", "peace accepted", "created world construction", 
            "artifact created", "reclaim site", "destroyed site", "new site leader", "site taken over", "site abandoned"};

        private readonly List<string> _majorEventTypes = new List<string>
        {"hf died", "hf simple battle event", "created structure", "field battle", "attacked site", 
            "entity relocate", "plundered site", "peace rejected", "razed structure", "artifact lost", "diplomat lost", "hf revived", "agreement rejected", "first contact", 
            "agreement made", "first contact failed"};

        private readonly List<string> _minorEventTypes = new List<string>
        { "create entity position", "merchant", "item stolen", "masterpiece lost", "change hf state", 
            "add hf entity link", "add hf hf link", "add hf site link",  "change hf job", "hf travel", "hf new pet", 
            "hf wounded", "creature devoured", "hf profaned structure", "hf does interaction", "hf abducted", "changed creature type", 
            "assume identity", "remove hf entity link", "entity primary criminals", "hf reunion", "body abused", "hf gains secret goal", "hf learns secret", 
            "remove hf site link","artifact possessed", "hf confronted","artifact stored", 
            "entity law", "masterpiece item", "change hf body state",
            "masterpiece arch constructed", "masterpiece food", "masterpiece engraving", "masterpiece item improvement", "masterpiece dye", 
            "masterpiece arch design"};

/*
        public List<string> KnownEventTypes = new List<string>
        { "create entity position", "merchant", "item stolen", "masterpiece lost", "hf simple battle event", "change hf state", 
            "add hf entity link", "created site", "add hf hf link", "entity created", "add hf site link", "created structure", "change hf job", "hf travel", "hf new pet", "field battle", 
            "peace accepted", "hf died", "hf wounded", "creature devoured", "attacked site", "hf profaned structure", "hf does interaction", "hf abducted", "changed creature type", 
            "entity relocate", "assume identity", "remove hf entity link", "entity primary criminals", "hf reunion", "plundered site", "body abused", "hf gains secret goal", "hf learns secret", 
            "created world construction", "remove hf site link", "artifact created", "artifact possessed", "hf confronted", "peace rejected", "reclaim site", "razed structure", "artifact stored", 
            "entity law", "masterpiece item", "artifact lost", "diplomat lost", "hf revived", "agreement rejected", "first contact", "destroyed site", "change hf body state", "new site leader", 
            "site taken over", "site abandoned", "masterpiece arch constructed", "masterpiece food", "masterpiece engraving", "masterpiece item improvement", "agreement made", "masterpiece dye", 
            "first contact failed", "masterpiece arch design" };
*/

/*
        public List<string> UnknownEventTypes = new List<string>
        {"agreement void", "site tribute forced", "hf destroyed site", "site died", "replaced structure", "hf razed structure", 
            "hf reach summit", "remove hf hf link", "agreement concluded", "artifact hidden", "artifact found", "artifact recovered", "artifact dropped", "entity incorporated", "impersonate hf"};
*/

        private readonly List<string> _criticalEventCollectionTypes = new List<string> { "battle", "war", "site conquered" };

        private readonly List<string> _minorEventCollectionTypes = new List<string> { "abduction", "beast attack", "duel", "journey", "theft" };

        private Image _stretchedMap;
        private Point _pingLocation;
        private readonly Image _marker;
        private SizeF _siteSize;

        internal TimelineForm(World world)
        {
            InitializeComponent();
            _world = world;
            var resources = new ResourceManager("DFWV.Properties.Resources", typeof(TimelineForm).Assembly);
            _marker = resources.GetObject("Marker") as Bitmap;

            var sizeString = _world.Parameters.First(x => x.Name == "DIM").Value;
            var sizeX = Convert.ToInt32(sizeString.Split(':')[0]);
            var sizeY = Convert.ToInt32(sizeString.Split(':')[1]);
            var mapSize = new Size(sizeX, sizeY);


            _stretchedMap = new Bitmap(picMap.Width, picMap.Height);
            var g = Graphics.FromImage(_stretchedMap);
            g.DrawImage(Image.FromFile(_world.Maps["Main"]), picMap.DisplayRectangle);
            g.Dispose();

            picMap.Image = _stretchedMap;
            
            _siteSize = new SizeF(picMap.Width / (float)mapSize.Width,
                                picMap.Height / (float)mapSize.Height);
        }

        private void Start_Click(object sender, EventArgs e)
        {
            switch (Start.Text)
            {
                case "Start":
                    Restart.PerformClick();
                    break;
                case "Continue":
                    EventTimer.Enabled = true;
                    Start.Text = @"Pause";
                    break;
                default:
                    EventTimer.Enabled = false;
                    Start.Text = @"Continue";
                    break;
            }
        }

        private void SortEvents()
        {
            _worldEvents.Clear();

            foreach (var he in _world.HistoricalEvents.Values)
            {
                if (he.EventCollection != null)
                { 
                    if (he == he.EventCollection.Event.First())
                        _worldEvents.Add(he.EventCollection);
                    _worldEvents.Add(he);
                    if (he == he.EventCollection.Event.Last())
                        _worldEvents.Add(he.EventCollection);
                }
                else
                    _worldEvents.Add(he);
            }

        }

        private void EventTimer_Tick(object sender, EventArgs e)
        {
            var nextItem = GetNextItem();

            if (nextItem != null)
            {

                lstEvents.Items.Add(nextItem);
                ModifyMap(nextItem);
                lstEvents.SelectedIndex = lstEvents.Items.Count - 1;
            }
            else
            {
                EventTimer.Enabled = false;
                Start.Text = @"Start";
            }
        }

        private void ModifyMap(XmlObject nextItem)
        {
            if (nextItem is HeCreatedSite || nextItem is HeReclaimSite || 
                nextItem is HeNewSiteLeader || nextItem is HeSiteTakenOver || 
                nextItem is HeSiteAbandoned || nextItem is HeDestroyedSite)
            {
                var evt = (HistoricalEvent)nextItem;
                
                Color penCol;
                if (nextItem is HeCreatedSite)
                    penCol = (nextItem as HeCreatedSite).Civ?.Civilization?.Color ?? Color.White;
                else if (nextItem is HeReclaimSite)
                    penCol = (nextItem as HeReclaimSite).Civ.Civilization.Color;
                else if (nextItem is HeNewSiteLeader)
                {
                    penCol = (nextItem as HeNewSiteLeader).AttackerCiv.Civilization?.Color ?? Color.White;
                }
                else if (nextItem is HeSiteTakenOver)
                    penCol = (nextItem as HeSiteTakenOver).AttackerCiv.Civilization.Color;
                else if (nextItem is HeSiteAbandoned)
                    penCol = Color.White;
                else // if (nextItem is HE_DestroyedSite)
                    penCol = Color.White;

                
                var g = Graphics.FromImage(_stretchedMap);
                var loc = new Point();
                var markerSize = new SizeF(7.0f,7.0f);
                if (_siteSize.Width > 7)
                    markerSize.Width = (int)_siteSize.Width;
                if (_siteSize.Height > 7)
                    markerSize.Height = (int)_siteSize.Height;

                loc.X = (int)(evt.Location.X * _siteSize.Width + _siteSize.Width / 2 - markerSize.Width / 2);
                loc.Y = (int)(evt.Location.Y * _siteSize.Height + _siteSize.Height / 2 - markerSize.Height / 2);

                using (var p = new Pen(penCol))
                {
                    p.Width = 2;
                    g.DrawEllipse(p, loc.X, loc.Y, markerSize.Width, markerSize.Height);
                }
                g.Dispose();
            }
            else
            {
                var worldConstruction = nextItem as HeCreatedWorldConstruction;
                if (worldConstruction == null) 
                    return;
                var evt = worldConstruction;
                var g = Graphics.FromImage(_stretchedMap);


                var from = new Point();
                var to = new Point();

                @from.X = (int)(evt.Site1.Location.X * _siteSize.Width + _siteSize.Width / 2);
                @from.Y = (int)(evt.Site1.Location.Y * _siteSize.Height + _siteSize.Height / 2);

                to.X = (int)(evt.Site2.Location.X * _siteSize.Width + _siteSize.Width / 2);
                to.Y = (int)(evt.Site2.Location.Y * _siteSize.Height + _siteSize.Height / 2);

                var lineColor = Color.White;
                if (evt.Civ?.Civilization != null)
                    lineColor = evt.Civ.Civilization.Color;

                using (var p = new Pen(lineColor))
                {
                    p.Width = 2;
                    g.DrawLine(p, @from, to);
                }


                g.Dispose();
            }
        }

        private XmlObject GetNextItem()
        {


            do
                _lastEvent++;
            while (SkipEvent());


            return _lastEvent == _worldEvents.Count ? null : _worldEvents[_lastEvent];
        }

        private bool SkipEvent()
        {
            if (_lastEvent >= _worldEvents.Count || !(_worldEvents[_lastEvent] is HeCreatedWorldConstruction))
                return _lastEvent < _worldEvents.Count &&
                       !(
                           _worldEvents[_lastEvent] is HistoricalEventCollection &&
                           (
                               chkEventCollectionCritical.Checked &&
                               _criticalEventCollectionTypes.Contains(
                                   HistoricalEventCollection.Types[
                                       ((HistoricalEventCollection) _worldEvents[_lastEvent]).Type])
                               ||
                               chkEventCollectionMinor.Checked &&
                               _minorEventCollectionTypes.Contains(
                                   HistoricalEventCollection.Types[
                                       ((HistoricalEventCollection) _worldEvents[_lastEvent]).Type])
                               )
                           ||
                           _worldEvents[_lastEvent] is HistoricalEvent &&
                           (
                               chkEventCritical.Checked &&
                               _criticalEventTypes.Contains(
                                   HistoricalEvent.Types[((HistoricalEvent) _worldEvents[_lastEvent]).Type])
                               ||
                               chkEventMajor.Checked &&
                               _majorEventTypes.Contains(
                                   HistoricalEvent.Types[((HistoricalEvent) _worldEvents[_lastEvent]).Type])
                               ||
                               chkEventMinor.Checked &&
                               _minorEventTypes.Contains(
                                   HistoricalEvent.Types[((HistoricalEvent) _worldEvents[_lastEvent]).Type])
                               )
                           );

            var evt = (HeCreatedWorldConstruction)_worldEvents[_lastEvent];

            return !((chkEventCritical.Checked && evt.MasterWc == null) ||
                     (chkEventMajor.Checked && evt.MasterWc != null));
        }

        private void lstEvents_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();

            if (e.Index != -1)
            {
                string drawstring;
                Color mColor;

                var evt = lstEvents.Items[e.Index] as HistoricalEvent;
                if (evt != null)
                {
                    var loc = evt.Location;
                    if (loc == Point.Empty)
                        drawstring = evt.ToTimelineString();
                    else
                        drawstring = evt.ToTimelineString() + " - (" + loc.X + "," + loc.Y + ")";
                    mColor = Color.Blue;
                }
                else
                {
                    var hec = (HistoricalEventCollection)lstEvents.Items[e.Index];

                    var isHeCending = lstEvents.Items.IndexOf(hec) != e.Index;

                    var loc = hec.Location;
                    if (loc == Point.Empty)
                        drawstring = hec.ToTimelineString() + (isHeCending ? " Ended " : "");
                    else
                        drawstring = hec.ToTimelineString() + (isHeCending ? " Ended " : "") + " - (" + loc.X + "," + loc.Y + ")";


                    mColor = Color.Red;
                }
                
                e.Graphics.DrawString(drawstring, new Font(e.Font.FontFamily.ToString(), e.Font.Size, FontStyle.Regular), new SolidBrush(mColor), e.Bounds);

            }
            e.DrawFocusRectangle();
        }

        private void Restart_Click(object sender, EventArgs e)
        {
            lstEvents.Items.Clear();
            SortEvents();
            _lastEvent = -1;

            _stretchedMap = new Bitmap(picMap.Width, picMap.Height);
            var g = Graphics.FromImage(_stretchedMap);
            g.DrawImage(Image.FromFile(_world.Maps["Main"]), picMap.DisplayRectangle);
            g.Dispose(); 

            EventTimer.Enabled = true;
            Start.Text = @"Pause";
        }

        private void lstEvents_DoubleClick(object sender, EventArgs e)
        {
            var listBox = (ListBox)sender;
            var selectedItem = (WorldObject)listBox.SelectedItem;
            selectedItem?.Select(Program.MainForm);
        }

        private void Clear_Click(object sender, EventArgs e)
        {
            lstEvents.Items.Clear();
        }

        private void ShowMap_Click(object sender, EventArgs e)
        {
            if (ShowMap.Text == @"Show Map")
                DisplayMap();
            else
                HideMap();
        }

        private void HideMap()
        {
            ShowMap.Text = @"Show Map";
            Height = lstEvents.Bottom + 44;
        }

        private void DisplayMap()
        {
            ShowMap.Text = @"Hide Map";
            picMap.Image = _stretchedMap;
            Height = picMap.Bottom + 50;
        }

        private void lstEvents_SelectedIndexChanged(object sender, EventArgs e)
        {
            var lstbox = (ListBox)sender;
            var evt = lstbox.SelectedItem as HistoricalEvent;
            if (evt != null)
                Ping(evt);
            else
                Ping((HistoricalEventCollection)lstbox.SelectedItem);
        }

        private void Ping(WorldObject evt)
        {
            _pingLocation = evt.Location == Point.Empty ? Point.Empty : evt.Location;
            picMap.Refresh();
        }

        private void picMap_Paint(object sender, PaintEventArgs e)
        {

            e.Graphics.DrawImage(_stretchedMap, new Point(0,0));
            if (_pingLocation == Point.Empty)
                return;
            var loc = new Point
            {
                X = (int) (_pingLocation.X*_siteSize.Width + _siteSize.Width/2 - _marker.Width / 2f),
                Y = (int) (_pingLocation.Y*_siteSize.Height + _siteSize.Height/2 - _marker.Height / 2f)
            };

            e.Graphics.DrawImage(_marker, loc);
        }

        private void trackSpeed_Scroll(object sender, EventArgs e)
        {
            var values = new List<int> {10, 50, 100, 200, 500, 1000, 1500, 2000, 3000, 4000};
            EventTimer.Interval = values[10 - trackSpeed.Value];
        }


    }
}
