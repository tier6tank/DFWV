using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using DFWV.WorldClasses;
using DFWV.WorldClasses.HistoricalEventCollectionClasses;
using DFWV.WorldClasses.HistoricalFigureClasses;
using Region = DFWV.WorldClasses.Region;

namespace DFWV
{
    public partial class MapForm : Form
    {
        readonly World _world;
        string _selectedMap = "Main";
        Bitmap _mapOverlay;
        Size _mapSize;
        Size _siteSize;
        Site _selectedSite;
        int _siteSelection;
        Region _selectedRegion;
        UndergroundRegion _selectedUndergroundRegion;
        WorldConstruction _selectedWc;
        River _selectedRiver;
        Mountain _selectedMountain;
        Point _selectedCoords;
        private readonly List<string> _selectedSiteTypes = new List<string>();
 
        public MapForm()
        {
            InitializeComponent();
        }

        internal MapForm(World world)
        {
            InitializeComponent();
            _world = world;
            LoadMaps();
            LoadSiteTypes();
            LoadHFs();
            LoadUgRegionDepth();
            ChangeMap();
            DrawMaps();
        }

        private void LoadHFs()
        {
            if (cmbHFTravels.Items.Count == 0)
                cmbHFTravels.Items.AddRange(_world.HistoricalFigures.Values.ToArray());
            cmbHFTravels.SelectedItem = cmbHFTravels.Items[0];
            cmbHFTravels.Text = cmbHFTravels.SelectedItem.ToString();
        }

        private Size MiniMapBoxSize => new Size((int)((float)pnlMap.ClientSize.Width / picMap.Width * picMiniMap.Width),
            (int)((float)pnlMap.ClientSize.Height / picMap.Height * picMiniMap.Height));

        private void LoadMaps()
        {

            var  newItem = new ToolStripMenuItem("Main") {CheckOnClick = true, Checked = true};
            newItem.Click += MapSelectionClicked;
            mapsToolStripMenuItem.DropDownItems.Add(newItem);
            foreach (var thisItem in from mapitem in _world.Maps where mapitem.Key != "Main" select new ToolStripMenuItem(mapitem.Key) {CheckOnClick = true})
            {
                thisItem.Click += MapSelectionClicked;
                mapsToolStripMenuItem.DropDownItems.Add(thisItem);
            }
        }

        private void LoadSiteTypes()
        {
            lstSiteTypes.Items.Clear();
            lstSiteTypes.View = View.Details;
            lstSiteTypes.Columns.Add("Name");
            lstSiteTypes.Columns[0].Width = lstSiteTypes.Width -
            4;
            lstSiteTypes.HeaderStyle = ColumnHeaderStyle.None;
            foreach (var siteType in WorldClasses.Site.Types)
                lstSiteTypes.Items.Add(siteType);
        }

        private void LoadUgRegionDepth()
        {
            foreach (var ugregion in _world.UndergroundRegions.Values)
            {
                if (ugregion.Depth < ugRegionDepthPicker.Minimum)
                    ugRegionDepthPicker.Minimum = ugregion.Depth;
                if (ugregion.Depth > ugRegionDepthPicker.Maximum)
                    ugRegionDepthPicker.Maximum = ugregion.Depth;
            }
        }

        private void ChangeMap()
        {
            Cursor.Current = Cursors.WaitCursor;
            picMap.Image?.Dispose();
            picMap.Image = Image.FromFile(_world.Maps[_selectedMap]);
            picMap.Size = picMap.Image.Size;
            picMiniMap.Image?.Dispose();

            picMiniMap.Image = Image.FromFile(_world.Maps[_selectedMap]);

            var sizeString = _world.Parameters.First(x => x.Name == "DIM").Value;
            var sizeX = Convert.ToInt32(sizeString.Split(':')[0]);
            var sizeY = Convert.ToInt32(sizeString.Split(':')[1]);
            _mapSize = new Size(sizeX, sizeY);
            _siteSize = new Size(picMap.Image.Size.Width / _mapSize.Width,
                                picMap.Image.Size.Height / _mapSize.Height);

            UpdateLegend();
            RedrawOverlay();
            DrawMaps();
        }

        private void DrawMaps()
        {
            picMiniMap.Invalidate();
            picMap.Invalidate();
        }

        #region Overlay Drawing
        
        private void RedrawOverlay()
        {
            Cursor.Current = Cursors.WaitCursor;
            if (_mapOverlay == null || _mapOverlay.Size != new Size(picMap.Image.Width, picMap.Image.Height))
            {
                _mapOverlay?.Dispose();
                _mapOverlay = new Bitmap(picMap.Image.Width, picMap.Image.Height);
            }
            var g = Graphics.FromImage(_mapOverlay);
            g.Clear(Color.Transparent);
            if (chkHighlightCoordinates.Checked)
            {
                using (var p = new Pen(Color.White))
                {
                    g.DrawRectangle(p, _selectedCoords.X*_siteSize.Width, _selectedCoords.Y*_siteSize.Height,
                        _siteSize.Width - 1, _siteSize.Height - 1);
                }
            }
            if (chkRivers.Checked)
                DrawRiverOverlay(g);
            if (chkUGRegions.Checked)
                DrawUndergroundRegionOverlay(g);
            if (chkRegions.Checked)
                DrawRegionOverlay(g);
            if (chkConstructions.Checked)
                DrawWorldConstructionOverlay(g);
            if (chkMountains.Checked)
                DrawMountainOverlay(g);
            if (chkSites.Checked)
                DrawSiteOverlay(g);
            if (chkCivilizations.Checked)
                DrawCivOverlay(g);
            if (chkBattles.Checked)
                DrawBattleOverlay(g);
            if (chkHistoricalFigures.Checked)
                DrawHfOverlay(g);
            if (chkHFTravels.Checked)
                DrawHfTravelsOverlay(g);

            DrawMaps();
            Cursor.Current = Cursors.Default;
        }

        private void DrawHfTravelsOverlay(Graphics g)
        {
            var hf = cmbHFTravels.SelectedItem as HistoricalFigure;

            var myColor = Color.FromArgb(255, 100, 255);
            using (var p = new Pen(myColor))
            {
                var events = hf.Events.Where(x => !x.Location.IsEmpty).ConsecutiveDistict();

                
                var points = events.Select(evt => new Point(evt.Location.X*_siteSize.Width + _siteSize.Width/2, evt.Location.Y*_siteSize.Height + _siteSize.Height/2)).ToList();
                if (points.Count > 1)
                    g.DrawLines(p, points.ToArray());

            }

        }

        private void DrawSiteOverlay(Graphics g)
        {
            foreach (var site in _world.Sites.Values)
            {
                if (!_selectedSiteTypes.Contains(site.SiteType)) 
                    continue;
                if (site.Parent != null || !chkNeutralSites.Checked)
                    continue;


                var myColor = site.Parent?.Color ?? Color.White;
                using (var p = new Pen(myColor))
                {
                    using (Brush b = new SolidBrush(Color.FromArgb(150,myColor)))
                    {
                        g.FillRectangle(b, site.Coords.X * _siteSize.Width, site.Coords.Y * _siteSize.Height, _siteSize.Width - 1, _siteSize.Height - 1);
                        g.DrawRectangle(p, site.Coords.X * _siteSize.Width, site.Coords.Y * _siteSize.Height, _siteSize.Width - 1, _siteSize.Height - 1);
                    }
                }
            }
            foreach (var site in _world.Sites.Values)
            {
                if (!_selectedSiteTypes.Contains(site.SiteType))
                    continue;
                if (site.Parent == null || !chkOwnedSites.Checked)
                    continue;


                var myColor = site.Parent.Color;
                using (var p = new Pen(myColor))
                {
                    using (Brush b = new SolidBrush(Color.FromArgb(150, myColor)))
                    {
                        g.FillRectangle(b, site.Coords.X * _siteSize.Width, site.Coords.Y * _siteSize.Height, _siteSize.Width - 1, _siteSize.Height - 1);
                        g.DrawRectangle(p, site.Coords.X * _siteSize.Width, site.Coords.Y * _siteSize.Height, _siteSize.Width - 1, _siteSize.Height - 1);
                    }
                }
            }
        }

        private void DrawCivOverlay(Graphics g)
        {
            foreach (var site in _world.Sites.Values)
            {
                if (site.Parent == null || site.Parent.FirstSite == site) continue;
                var myColor = Color.FromArgb(100,site.Parent.Color);
                using (var p = new Pen( myColor))
                {
                    if (site.CreatedEvent != null && site.CreatedEvent.Civ.Civilization != site.Parent)
                        p.DashStyle = DashStyle.Dot;
                    else
                        p.DashStyle = DashStyle.Solid;

                    var gap = (int)((_siteSize.Width + _siteSize.Height) / 2.0);
                    var fromPoint = new Point(site.Parent.FirstSite.Coords.X * _siteSize.Width + _siteSize.Width / 2,
                        site.Parent.FirstSite.Coords.Y * _siteSize.Height + _siteSize.Height / 2);
                    var toPoint = new Point(site.Coords.X * _siteSize.Width + _siteSize.Width / 2,
                        site.Coords.Y * _siteSize.Height + _siteSize.Height / 2);
                    if (!(Math.Sqrt(Math.Pow(fromPoint.X - toPoint.X, 2) + Math.Pow(fromPoint.Y - toPoint.Y, 2)) > gap))
                        continue;
                    var ang = Math.Atan2(fromPoint.Y - toPoint.Y, fromPoint.X - toPoint.X);
                    fromPoint.X = (int)(fromPoint.X - Math.Cos(ang) * gap);
                    fromPoint.Y = (int)(fromPoint.Y - Math.Sin(ang) * gap);
                    toPoint.X = (int)(toPoint.X + Math.Cos(ang) * gap);
                    toPoint.Y = (int)(toPoint.Y + Math.Sin(ang) * gap);

                    g.DrawLine(p, fromPoint, toPoint);
                }
            }

            foreach (var entity in _world.Entities.Values.Where(entity => entity.Coords != null && entity.Civilization != null))
            {
                using (var p = new Pen(entity.Civilization.Color))
                {
                    var topLeft = new Point();
                    var topRight = new Point();
                    var bottomLeft = new Point();
                    var bottomRight = new Point();
                    using (Brush b = new SolidBrush(Color.FromArgb(50, entity.Civilization.Color)))
                    {
                        foreach (var coord in entity.Coords)
                        {
                            const int areaMultFactor = 16;
                            topLeft.X = coord.X * _siteSize.Width * areaMultFactor;
                            topLeft.Y = coord.Y * _siteSize.Height * areaMultFactor;
                            bottomLeft.X = coord.X * _siteSize.Width * areaMultFactor;
                            bottomLeft.Y = topLeft.Y + (_siteSize.Height * areaMultFactor) - 1;
                            topRight.X = topLeft.X + (_siteSize.Width * areaMultFactor) - 1;
                            topRight.Y = topLeft.Y;
                            bottomRight.X = topRight.X;
                            bottomRight.Y = bottomLeft.Y;

                            g.FillRectangle(b, topLeft.X, topLeft.Y, (_siteSize.Width * areaMultFactor) - 1, (_siteSize.Height * areaMultFactor) - 1);


                            //g.DrawRectangle(p, TopLeft.X , TopLeft.Y , siteSize.Width - 1, siteSize.Height - 1);
                            if (!entity.Coords.Contains(new Point(coord.X, coord.Y - 1)))
                                g.DrawLine(p, topLeft, topRight);
                            if (!entity.Coords.Contains(new Point(coord.X + 1, coord.Y)))
                                g.DrawLine(p, topRight, bottomRight);
                            if (!entity.Coords.Contains(new Point(coord.X, coord.Y + 1)))
                                g.DrawLine(p, bottomRight, bottomLeft);
                            if (!entity.Coords.Contains(new Point(coord.X - 1, coord.Y)))
                                g.DrawLine(p, bottomLeft, topLeft);
                        }
                    }
                        
                }
            }
        }

        private void DrawBattleOverlay(Graphics g)
        {
            foreach (var evtcol in _world.HistoricalEventCollections.Values.Where(x => HistoricalEventCollection.Types[x.Type] == "battle"))
            {
                var battleEventCol = (EC_Battle) evtcol;
                if (!battleEventCol.BattleTotaled) continue;
                var battlePoint = new Point(battleEventCol.Coords.X * _siteSize.Width + _siteSize.Width / 2,
                    battleEventCol.Coords.Y * _siteSize.Height + _siteSize.Height / 2);
                var radius = (int)Math.Sqrt(battleEventCol.BattleData.AttackingDeaths + battleEventCol.BattleData.DefendingDeaths);
                if (battleEventCol.AttackingSquad != null)
                {
                    foreach (var squad in battleEventCol.AttackingSquad)
                    {
                        if (squad.Site == null) continue;
                        var sitePoint = new Point(squad.Site.Coords.X * _siteSize.Width + _siteSize.Width / 2,
                            squad.Site.Coords.Y * _siteSize.Height + _siteSize.Height / 2);
                        var alpha = (int)(Math.Sqrt(squad.Number / 10f) < 255 ? Math.Sqrt(squad.Number) : 255);
                        if (alpha == 0)
                            alpha = 1;
                        using (var p = new Pen(Color.FromArgb(alpha, Color.Blue)))
                        {
                            g.DrawLine(p, sitePoint, battlePoint);
                        }
                    }
                }
                if (battleEventCol.DefendingSquad != null)
                {
                    foreach (var squad in battleEventCol.DefendingSquad)
                    {
                        if (squad.Site == null) continue;
                        var sitePoint = new Point(squad.Site.Coords.X * _siteSize.Width + _siteSize.Width / 2,
                            squad.Site.Coords.Y * _siteSize.Height + _siteSize.Height / 2);

                        var alpha = (int)(Math.Sqrt(squad.Number / 10f) < 255 ? Math.Sqrt(squad.Number) : 255); 
                        if (alpha == 0)
                            alpha = 1;
                        using (var p = new Pen(Color.FromArgb(alpha, Color.Green)))
                        {
                            g.DrawLine(p, sitePoint, battlePoint);
                        }
                    }
                }
                g.DrawEllipse(Pens.Red, battlePoint.X - radius, battlePoint.Y - radius, radius * 2, radius * 2);
                g.DrawLine(Pens.Red, new Point(battlePoint.X - radius, battlePoint.Y), battlePoint);
                g.DrawLine(Pens.Red, new Point(battlePoint.X , battlePoint.Y - radius), battlePoint);
            }
        }

        private void DrawRegionOverlay(Graphics g)
        {
            var colorNames = new List<string>
            {"#00FF00", "#0000FF", "#FF0000", "#01FFFE", "#FFA6FE", "#FFDB66", "#006401", "#010067", 
                "#95003A", "#007DB5", "#FF00F6", "#FFEEE8", "#774D00", "#90FB92", "#0076FF", "#D5FF00", 
                "#FF937E", "#6A826C", "#FF029D", "#FE8900", "#7A4782", "#7E2DD2", "#85A900", "#FF0056", 
                "#A42400", "#00AE7E", "#683D3B", "#BDC6FF", "#263400", "#BDD393", "#00B917", "#9E008E", 
                "#001544", "#C28C9F", "#FF74A3", "#01D0FF", "#004754", "#E56FFE", "#788231", "#0E4CA1", 
                "#91D0CB", "#BE9970", "#968AE8", "#BB8800", "#43002C", "#DEFF74", "#00FFC6", "#FFE502", 
                "#620E00", "#008F9C", "#98FF52", "#7544B1", "#B500FF", "#00FF78", "#FF6E41", "#005F39", 
                "#6B6882", "#5FAD4E", "#A75740", "#A5FFD2", "#FFB167", "#009BFF", "#E85EBE"};
            var rnd = new Random();
            
            var curDistinctColor = 0; 
            foreach (var region in _world.Regions.Values.Where(region => region.Coords != null))
            {
                curDistinctColor++;
                Color thisColor;
                if (curDistinctColor < colorNames.Count)
                {
                    var rgb = int.Parse(colorNames[curDistinctColor].Replace("#", ""), NumberStyles.HexNumber);
                    thisColor = Color.FromArgb(255,Color.FromArgb(rgb));
                }
                else
                {
                    thisColor = Color.FromArgb(rnd.Next(150) + 100, rnd.Next(150) + 100, rnd.Next(150) + 100);
                }
                using (var p = new Pen(thisColor))
                {
                    var topLeft = new Point();
                    var topRight = new Point();
                    var bottomLeft = new Point();
                    var bottomRight = new Point();
                    using (Brush b = new SolidBrush(Color.FromArgb(50, thisColor)))
                    {
                        foreach (var coord in region.Coords.Distinct())
                        {
                            topLeft.X = coord.X * _siteSize.Width;
                            topLeft.Y = coord.Y * _siteSize.Height;
                            bottomLeft.X = coord.X * _siteSize.Width;
                            bottomLeft.Y = topLeft.Y + _siteSize.Height - 1;
                            topRight.X = topLeft.X + _siteSize.Width - 1;
                            topRight.Y = coord.Y * _siteSize.Height;
                            bottomRight.X = topRight.X;
                            bottomRight.Y = bottomLeft.Y;

                            g.FillRectangle(b, topLeft.X, topLeft.Y, _siteSize.Width - 1, _siteSize.Height - 1);
                            

                            //g.DrawRectangle(p, TopLeft.X , TopLeft.Y , siteSize.Width - 1, siteSize.Height - 1);
                            if (!region.Coords.Contains(new Point(coord.X, coord.Y-1)))
                                g.DrawLine(p, topLeft, topRight);
                            if (!region.Coords.Contains(new Point(coord.X+1, coord.Y)))
                                g.DrawLine(p, topRight, bottomRight);
                            if (!region.Coords.Contains(new Point(coord.X, coord.Y+1)))
                                g.DrawLine(p, bottomRight, bottomLeft);
                            if (!region.Coords.Contains(new Point(coord.X - 1, coord.Y)))
                                g.DrawLine(p, bottomLeft, topLeft);
                        }
                    }
                }
            }
        }

        private void DrawUndergroundRegionOverlay(Graphics g)
        {
            var colorNames = new List<string>
            {"#00FF00", "#0000FF", "#FF0000", "#01FFFE", "#FFA6FE", "#FFDB66", "#006401", "#010067", 
                "#95003A", "#007DB5", "#FF00F6", "#FFEEE8", "#774D00", "#90FB92", "#0076FF", "#D5FF00", 
                "#FF937E", "#6A826C", "#FF029D", "#FE8900", "#7A4782", "#7E2DD2", "#85A900", "#FF0056", 
                "#A42400", "#00AE7E", "#683D3B", "#BDC6FF", "#263400", "#BDD393", "#00B917", "#9E008E", 
                "#001544", "#C28C9F", "#FF74A3", "#01D0FF", "#004754", "#E56FFE", "#788231", "#0E4CA1", 
                "#91D0CB", "#BE9970", "#968AE8", "#BB8800", "#43002C", "#DEFF74", "#00FFC6", "#FFE502", 
                "#620E00", "#008F9C", "#98FF52", "#7544B1", "#B500FF", "#00FF78", "#FF6E41", "#005F39", 
                "#6B6882", "#5FAD4E", "#A75740", "#A5FFD2", "#FFB167", "#009BFF", "#E85EBE"};
            var rnd = new Random();

            var curDistinctColor = 0;
            foreach (var ugregion in _world.UndergroundRegions.Values.Where(ugregion => ugregion.Depth == ugRegionDepthPicker.Value).Where(ugregion => ugregion.Coords != null))
            {
                curDistinctColor++;
                Color thisColor;
                if (curDistinctColor < colorNames.Count)
                {
                    var rgb = int.Parse(colorNames[curDistinctColor].Replace("#", ""), NumberStyles.HexNumber);
                    thisColor = Color.FromArgb(255, Color.FromArgb(rgb));
                }
                else
                {
                    thisColor = Color.FromArgb(rnd.Next(150) + 100, rnd.Next(150) + 100, rnd.Next(150) + 100);
                }
                using (var p = new Pen(thisColor))
                {
                    var topLeft = new Point();
                    var topRight = new Point();
                    var bottomLeft = new Point();
                    var bottomRight = new Point();
                    using (Brush b = new SolidBrush(Color.FromArgb(50, thisColor)))
                    {
                        foreach (var coord in ugregion.Coords)
                        {
                            topLeft.X = coord.X * _siteSize.Width;
                            topLeft.Y = coord.Y * _siteSize.Height;
                            bottomLeft.X = coord.X * _siteSize.Width;
                            bottomLeft.Y = topLeft.Y + _siteSize.Height - 1;
                            topRight.X = topLeft.X + _siteSize.Width - 1;
                            topRight.Y = coord.Y * _siteSize.Height;
                            bottomRight.X = topRight.X;
                            bottomRight.Y = bottomLeft.Y;

                            g.FillRectangle(b, topLeft.X, topLeft.Y, _siteSize.Width - 1, _siteSize.Height - 1);


                            //g.DrawRectangle(p, TopLeft.X , TopLeft.Y , siteSize.Width - 1, siteSize.Height - 1);
                            if (!ugregion.Coords.Contains(new Point(coord.X, coord.Y - 1)))
                                g.DrawLine(p, topLeft, topRight);
                            if (!ugregion.Coords.Contains(new Point(coord.X + 1, coord.Y)))
                                g.DrawLine(p, topRight, bottomRight);
                            if (!ugregion.Coords.Contains(new Point(coord.X, coord.Y + 1)))
                                g.DrawLine(p, bottomRight, bottomLeft);
                            if (!ugregion.Coords.Contains(new Point(coord.X - 1, coord.Y)))
                                g.DrawLine(p, bottomLeft, topLeft);
                        }
                    }
                }
            }
        }


        private void DrawHfOverlay(Graphics g)
        {
            var coordPop = new Dictionary<Point, int>();


            foreach (var hf in _world.HistoricalFigures.Values.Where(x => x.DiedEvent == null))
            {
                var hfCoord = Point.Empty;
                if (hf.Site != null)
                    hfCoord = hf.Site.Coords;
                else if (hf.Coords != Point.Empty)
                    hfCoord = hf.Coords;
                else if (hf.Region != null)
                    continue;

                if (hfCoord == Point.Empty) continue;
                if (!coordPop.ContainsKey(hfCoord))
                    coordPop.Add(hfCoord, 1);
                else
                    coordPop[hfCoord]++;
            }

            var maxPop = Math.Sqrt(coordPop.Values.Max());

            foreach (var coord in coordPop.Keys)
            {
                var red = (int)(255.0f * (Math.Sqrt(coordPop[coord]) / maxPop));

                using (Brush b = new SolidBrush(Color.FromArgb(225, red, 0, 0)))
                {
                    g.FillRectangle(b, coord.X * _siteSize.Width + 1, coord.Y * _siteSize.Height + 1, _siteSize.Width - 2, _siteSize.Height - 2);
                }

            }

        }

        private void DrawWorldConstructionOverlay(Graphics g)
        {
            foreach (var wc in _world.WorldConstructions.Values)
            {
                var myColor = wc.CreatedEvent?.Civ?.Civilization?.Color ?? Color.White;
                using (var p = new Pen(myColor))
                {
                    
                    p.DashStyle = DashStyle.Solid;
                    p.Width = 3;

                    if (wc.Coords == null)
                    {
                        var gap = (int) ((_siteSize.Width + _siteSize.Height)/4.0);
                        var fromPoint = new Point(wc.From.Coords.X*_siteSize.Width + _siteSize.Width/2,
                            wc.From.Coords.Y*_siteSize.Height + _siteSize.Height/2);
                        var toPoint = new Point(wc.To.Coords.X*_siteSize.Width + _siteSize.Width/2,
                            wc.To.Coords.Y*_siteSize.Height + _siteSize.Height/2);
                        if (
                            !(Math.Sqrt(Math.Pow(fromPoint.X - toPoint.X, 2) + Math.Pow(fromPoint.Y - toPoint.Y, 2)) >
                              gap))
                            continue;
                        var ang = Math.Atan2(fromPoint.Y - toPoint.Y, fromPoint.X - toPoint.X);
                        fromPoint.X = (int) (fromPoint.X - Math.Cos(ang)*gap);
                        fromPoint.Y = (int) (fromPoint.Y - Math.Sin(ang)*gap);
                        toPoint.X = (int) (toPoint.X + Math.Cos(ang)*gap);
                        toPoint.Y = (int) (toPoint.Y + Math.Sin(ang)*gap);
                        g.DrawLine(p, fromPoint, toPoint);
                    }
                    else
                    {
                        if (wc.Coords.Count > 1)
                        {
                            for (var i = 0; i < wc.Coords.Count - 1; i++)
                            {
                                var fromPoint = new Point(wc.Coords[i].X * _siteSize.Width + _siteSize.Width / 2,
                                    wc.Coords[i].Y * _siteSize.Height + _siteSize.Height / 2);
                                var toPoint = new Point(wc.Coords[i+1].X * _siteSize.Width + _siteSize.Width / 2,
                                    wc.Coords[i+1].Y * _siteSize.Height + _siteSize.Height / 2);
                                p.DashStyle = wc.ConstructionType == "tunnel" 
                                    ? DashStyle.Dot 
                                    : DashStyle.Solid;
                                g.DrawLine(p, fromPoint, toPoint);
                            }
                        }
                        else
                        {
                            //var min = Math.Min(siteSize.Width, siteSize.Height);
                            var pt = new[] { new Point(wc.Coords[0].X * _siteSize.Width,  wc.Coords[0].Y * _siteSize.Height + _siteSize.Height / 2),
                                                   new Point(wc.Coords[0].X * _siteSize.Width + _siteSize.Width / 2,  wc.Coords[0].Y * _siteSize.Height), 
                                                   new Point(wc.Coords[0].X * _siteSize.Width + _siteSize.Width,  wc.Coords[0].Y * _siteSize.Height + _siteSize.Height / 2) };

                            //g.DrawEllipse(p, new Rectangle(wc.Coords[0].X * siteSize.Width, wc.Coords[0].Y * siteSize.Height, min, min));
                            g.DrawCurve(p, pt);
                        }
                    }
                    
                }
            }
        }

        private void DrawMountainOverlay(Graphics g)
        {
            foreach (var mnt in _world.Mountains.Values)
            {
                using (var p = new Pen(Color.White))
                {

                    p.DashStyle = DashStyle.Solid;
                    p.Width = 2;


                    g.DrawPolygon(p, new []{new Point(mnt.Coords.X * _siteSize.Width + 1, mnt.Coords.Y * _siteSize.Height + _siteSize.Height - 1),
                                     new Point(mnt.Coords.X * _siteSize.Width + _siteSize.Width / 2, mnt.Coords.Y * _siteSize.Height + 1),
                                     new Point(mnt.Coords.X * _siteSize.Width + _siteSize.Width - 1, mnt.Coords.Y * _siteSize.Height + _siteSize.Height - 1)});
                }
            }
        }

        private void DrawRiverOverlay(Graphics g)
        {
            foreach (var river in _world.Rivers.Values)
            {
                var myColor = Color.FromArgb(100,100,255);

                if (river.Parent == null)
                    myColor = Color.Blue;
                using (var p = new Pen(myColor))
                {

                    p.DashStyle = DashStyle.Solid;
                    p.Width = 2;


                    var points = river.Coords.Select(coord => new Point(coord.X*_siteSize.Width + _siteSize.Width/2, coord.Y*_siteSize.Height + _siteSize.Height/2)).ToList();


                    g.DrawLines(p, points.ToArray());

                    if (river.Parent == null)
                        g.DrawEllipse(p, points.Last().X - 2, points.Last().Y - 2, 5, 5);

                    g.DrawEllipse(p, points.First().X - 2, points.First().Y - 2, 5, 5);
                }
            }

        }

        
        #endregion
        
        #region Form Events

        private void MapSelectionClicked(object sender, EventArgs e)
        {
            UncheckOtherToolStripMenuItems((ToolStripMenuItem)sender);
            _selectedMap = ((ToolStripMenuItem)sender).Text;


            ChangeMap();
        }

        private static void UncheckOtherToolStripMenuItems(ToolStripMenuItem selectedMenuItem)
        {
            selectedMenuItem.Checked = true;

            foreach (var ltoolStripMenuItem in (from object
                                                    item in selectedMenuItem.Owner.Items
                                                let ltoolStripMenuItem = item as ToolStripMenuItem
                                                where ltoolStripMenuItem != null
                                                where !item.Equals(selectedMenuItem)
                                                select ltoolStripMenuItem))
                (ltoolStripMenuItem).Checked = false;

        }

        private void picMap_Paint(object sender, PaintEventArgs e)
        {


            e.Graphics.DrawImage(_mapOverlay, new Point(0, 0));
        }

        private void picMiniMap_Paint(object sender, PaintEventArgs e)
        {

            var percentX = (float)pnlMap.HorizontalScroll.Value / pnlMap.HorizontalScroll.Maximum;
            var pointX = (int)(percentX * picMiniMap.Width);
            var percentY = (float)pnlMap.VerticalScroll.Value / pnlMap.VerticalScroll.Maximum;
            var pointY = (int)(percentY * picMiniMap.Height);
            var topLeft = new Point(pointX, pointY);

            var rectSize = MiniMapBoxSize;

            //this.Text = ((float)pnlMap.ClientSize.Width / picMap.Width).ToString() + " - " + ((float)pnlMap.ClientSize.Height / picMap.Height).ToString();
            e.Graphics.DrawRectangle(Pens.White, new Rectangle(topLeft, rectSize));
        }

        private void MapForm_Resize(object sender, EventArgs e)
        {
            DrawMaps();
        }

        private void MapForm_Move(object sender, EventArgs e)
        {
            DrawMaps();
        }

        private void pnlMap_Scroll(object sender, ScrollEventArgs e)
        {
            DrawMaps();
        }

        private void lstSiteTypes_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            ViewOptionChanged(sender, new EventArgs());
        }


        private static bool _isEnablingSections;

        private void ViewOptionChanged(object sender, EventArgs e)
        {
            if (_isEnablingSections)
                return;
            if (sender is CheckBox && sender == chkSites)
            {
                var chkbox = (CheckBox) sender;
                grpSites.Visible = chkbox.Checked;
                _isEnablingSections = true;
                chkOwnedSites.Checked = chkbox.Checked;
                chkNeutralSites.Checked = chkbox.Checked;
                foreach (ListViewItem item in lstSiteTypes.Items)
                    item.Checked = chkSites.Checked;
                _isEnablingSections = false;
            }
            _selectedSiteTypes.Clear();
            foreach (ListViewItem item in lstSiteTypes.Items)
            {
                if (item.Checked)
                    _selectedSiteTypes.Add(item.Text);
            }

            UpdateLegend();
            RedrawOverlay();

        }

        private void UpdateLegend()
        {

            if (chkShowLegend.Checked)
            {
                picLegend.Visible = true;
                switch (_selectedMap)
                {
                    case "Biome":
                        if (_world.MapLegends.ContainsKey("biome_color_key"))
                            _world.MapLegends["biome_color_key"].DrawTo(picLegend);
                        else
                            picLegend.Visible = false;
                        break;
                    case "Hydrosphere":
                        if (_world.MapLegends.ContainsKey("hydro_color_key"))
                            _world.MapLegends["hydro_color_key"].DrawTo(picLegend);
                        else
                            picLegend.Visible = false;
                        break;
                    case "Diplomacy":
                    case "Nobility":
                    case "Structures":
                    case "Trade":
                        if (_world.MapLegends.ContainsKey("structure_color_key"))
                            _world.MapLegends["structure_color_key"].DrawTo(picLegend);
                        else
                            picLegend.Visible = false;
                        break;
                    default:
                        picLegend.Visible = false;
                        break;
                }

            }
            else
                picLegend.Visible = false;
        }

        private void picMiniMap_MouseDown(object sender, MouseEventArgs e)
        {
            MiniMapCenterOn(new Point(e.X, e.Y));
        }

        private void MiniMapCenterOn(Point loc)
        {
            if (pnlMap.HorizontalScroll.Visible)
            {
                var newX = (int)((float)pnlMap.HorizontalScroll.Maximum * (loc.X - MiniMapBoxSize.Width / 2) / picMiniMap.Width);

                if (newX < 0)
                    newX = 0;
                pnlMap.HorizontalScroll.Value = newX < pnlMap.HorizontalScroll.Maximum ? newX : pnlMap.HorizontalScroll.Maximum;
            }
            if (pnlMap.VerticalScroll.Visible)
            {
                var newY = (int)((float)pnlMap.VerticalScroll.Maximum * (loc.Y - MiniMapBoxSize.Height / 2) / picMiniMap.Height);
                newY -= MiniMapBoxSize.Height / 2;
                if (newY < 0)
                    newY = 0;
                pnlMap.VerticalScroll.Value = newY < pnlMap.VerticalScroll.Maximum ? newY : pnlMap.VerticalScroll.Maximum;
            }
            picMiniMap.Invalidate();

        }

        private void picMiniMap_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                MiniMapCenterOn(new Point(e.X, e.Y));
        }

        private void picMap_MouseMove(object sender, MouseEventArgs e)
        {
            var oldCoords = _selectedCoords;
            _selectedCoords = new Point(e.X / _siteSize.Width, e.Y / _siteSize.Height);
            if (_selectedCoords != oldCoords && chkHighlightCoordinates.Checked)
                RedrawOverlay();
            lblMapCoords.Text = $"({_selectedCoords.X}, {_selectedCoords.Y})";
            var selectedObject = GetSelectedObject(_selectedCoords);
            var nameText = "";
            var altNameText = "";
            var ownerText = "";
            var parentText = "";
            var typeText = "";
            var objectTypeText = "";
            if (selectedObject is Site)
            {
                nameText = _selectedSite.ToString();
                altNameText = _selectedSite.AltName;
                ownerText = _selectedSite.Owner?.ToString() ?? "";
                parentText = _selectedSite.Parent?.ToString() ?? "";
                typeText = _selectedSite.SiteType;
                objectTypeText = "Site";
            }
            if (selectedObject is Mountain)
            {
                nameText = _selectedMountain.ToString();
                altNameText = _selectedMountain.AltName;

                objectTypeText = "Mountain";

            }
            if (selectedObject is WorldConstruction)
            {
                nameText = _selectedWc.ToString();
                typeText = _selectedWc.ConstructionType;
                if (_selectedWc.CreatedEvent?.Civ?.Civilization != null)
                    ownerText = _selectedWc.CreatedEvent.Civ.Civilization.ToString();

                lblMapAltNameCaption.Visible = false;
                if (_selectedWc.MasterWc != null)
                    parentText = _selectedWc.MasterWc.ToString();
                objectTypeText = "World Construction";
            }
            if (selectedObject is Region)
            {
                nameText = _selectedRegion.ToString();
                typeText = WorldClasses.Region.Types[_selectedRegion.Type];
                objectTypeText = "Region";
            }
            if (selectedObject is UndergroundRegion)
            {
                nameText = _selectedUndergroundRegion.ToString();
                objectTypeText = "Underground Region";
            }
            if (selectedObject is River)
            {
                nameText = _selectedRiver.ToString();
                altNameText = _selectedRiver.AltName;
                objectTypeText = "River";
            }
            lblMapName.Text = nameText;
            lblMapAltName.Text = altNameText;
            lblMapOwner.Text = ownerText;
            lblMapParent.Text = parentText;
            lblMapType.Text = typeText;

            lblMapNameCaption.Visible = nameText != "";
            lblMapAltNameCaption.Visible = altNameText != "";
            lblMapOwnerCaption.Visible = ownerText != "";
            lblMapParentCaption.Visible = parentText != "";
            lblMapTypeCaption.Visible = typeText != "";

            lblMapObject.Text = objectTypeText;
            lblMapObject.Visible = objectTypeText != "";

            if (picLegend.Visible)
            {
                picLegend.Left = e.X + 10 + picMap.Left;
                picLegend.Top = e.Y + 10 + picMap.Top;
            }
        }

        private WorldObject GetSelectedObject(Point mouseCoord)
        {
            if (chkSites.Checked)
            {
                _selectedSite = GetSiteAt(mouseCoord);
                if (_selectedSite != null)
                    return _selectedSite;
            }
            if (chkConstructions.Checked)
            {
                _selectedWc = GetWorldConstructionAt(mouseCoord);
                if (_selectedWc != null)
                    return _selectedWc;
            }
            if (chkMountains.Checked)
            {
                _selectedMountain = GetMountainAt(mouseCoord);
                if (_selectedMountain != null)
                    return _selectedMountain;
            }
            if (chkRivers.Checked)
            {
                _selectedRiver = GetRiverAt(mouseCoord);
                if (_selectedRiver != null)
                    return _selectedRiver;
            }
            if (chkRegions.Checked)
            {
                _selectedRegion = GetRegionAt(mouseCoord);
                if (_selectedRegion != null)
                    return _selectedRegion;
            }
            if (chkUGRegions.Checked)
            {
                _selectedUndergroundRegion = GetUndergroundRegionAt(mouseCoord);
                return _selectedUndergroundRegion;
            }
            return null;
        }

        private River GetRiverAt(Point mouseCoord)
        {
            if (!_world.HasPlusXml)
                return null;
            var rivermatches = _world.Rivers.Values.Where(x => x.Coords.Contains(mouseCoord));
            var riverArray = rivermatches as River[] ?? rivermatches.ToArray();
            if (!riverArray.Any())
                return null;
            var nonParentMatchArray = riverArray.Where(x => x.Parent == null).ToArray();
            return nonParentMatchArray.Any() ? 
                nonParentMatchArray.OrderByDescending(x => x.Coords.Count).FirstOrDefault() : 
                riverArray.OrderByDescending(x => x.Coords.Count).FirstOrDefault();
        }

        private Mountain GetMountainAt(Point mouseCoord)
        {
            if (!_world.HasPlusXml)
                return null;


            return _world.Mountains.Values.FirstOrDefault(x => x.Coords == mouseCoord);

        }

        private WorldConstruction GetWorldConstructionAt(Point coord)
        {
            if (!_world.HasPlusXml)
                return
                    _world.WorldConstructions.Values.Where(wc => wc.From != null && wc.To != null)
                        .FirstOrDefault(wc => wc.From.Coords == coord || wc.To.Coords == coord);
            var returnWc = _world.WorldConstructions.Values.Where(wc => wc.Coords != null && wc.ConstructionType != "road")
                .FirstOrDefault(wc => wc.Coords.Contains(coord));
            if (returnWc != null)
                return returnWc;

            return _world.WorldConstructions.Values.Where(wc => wc.Coords != null && wc.ConstructionType == "road")
                .FirstOrDefault(wc => wc.Coords.Contains(coord));
        }

        private Region GetRegionAt(Point coord)
        {
            return _world.Regions.Values.Where(region => region.Coords != null).FirstOrDefault(region => region.Coords.Contains(coord));
        }
        private UndergroundRegion GetUndergroundRegionAt(Point coord)
        {
            return _world.UndergroundRegions.Values.Where(ugregion => ugregion.Coords != null).FirstOrDefault(ugregion => ugregion.Coords.Contains(coord) && ugregion.Depth == ugRegionDepthPicker.Value);
        }

        private Site GetSiteAt(Point coord)
        {
            return _world.Sites.Values.FirstOrDefault(
                site => _selectedSiteTypes.Contains(site.SiteType) 
                    && site.Coords == coord 
                    && ((site.Parent == null && chkNeutralSites.Checked) 
                       || (site.Parent != null && chkOwnedSites.Checked)) 
                       );
        }

        private void MapForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Visible = false;
            e.Cancel = true;

        }

        private void picMap_MouseDown(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Left:
                    if (chkSites.Checked && _selectedSite != null)
                    {
                        _selectedSite.Select(Program.MainForm);
                        Program.MainForm.BringToFront();
                    }
                    else if (chkMountains.Checked && _selectedMountain != null)
                    {
                        _selectedMountain.Select(Program.MainForm);
                        Program.MainForm.BringToFront();
                    }
                    else if (chkConstructions.Checked && _selectedWc != null)
                    {
                        _selectedWc.Select(Program.MainForm);
                        Program.MainForm.BringToFront();
                    }
                    else if (chkRivers.Checked && _selectedRiver != null)
                    {
                        _selectedRiver.Select(Program.MainForm);
                        Program.MainForm.BringToFront();
                    }
                    else if (chkRegions.Checked && _selectedRegion != null)
                    {
                        _selectedRegion.Select(Program.MainForm);
                        Program.MainForm.BringToFront();
                    }
                    else if (chkUGRegions.Checked && _selectedUndergroundRegion != null)
                    {
                        _selectedUndergroundRegion.Select(Program.MainForm);
                        Program.MainForm.BringToFront();
                    }
                    break;
                case MouseButtons.Right:
                    _siteSelection++;
                    break;
            }
        }

        #endregion


        internal void Select(Point loc)
        {
            BringToFront();

            MiniMapCenterOn(new Point((int)(picMiniMap.Width * (float)loc.X / _mapSize.Width), (int)(picMiniMap.Height * (float)loc.Y / _mapSize.Height)));

            var locationOnForm = picMap.PointToScreen(Point.Empty);
                
            Cursor.Position = new Point(loc.X * _siteSize.Width + locationOnForm.X + _siteSize.Width / 2, loc.Y * _siteSize.Height + locationOnForm.Y + _siteSize.Height / 2);

        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            grpSettings.Visible = true;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            grpSettings.Visible = false;
        }

        private void MapForm_Load(object sender, EventArgs e)
        {

        }

        private void picLegend_MouseMove(object sender, MouseEventArgs e)
        {
            picLegend.Left += e.X + 10;
            picLegend.Top += e.Y + 10;
        }

        private void cmbHFTravels_Validating(object sender, CancelEventArgs e)
        {
            var cmbBox = sender as ComboBox;

            var selectedItem = cmbBox.Items.Cast<object>().FirstOrDefault(item => item.ToString() == cmbBox.Text);

            if (selectedItem == null)
            {
                e.Cancel = true;
                cmbBox.BackColor = Color.Red;
            }
            else
            {
                cmbBox.BackColor = Color.White;
            }
        }
    }
}
