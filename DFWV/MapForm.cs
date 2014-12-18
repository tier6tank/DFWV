﻿using System.Drawing.Drawing2D;
using System.IO;
using DFWV.WorldClasses;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using DFWV.WorldClasses.HistoricalEventCollectionClasses;
using Region = DFWV.WorldClasses.Region;

namespace DFWV
{
    public partial class MapForm : Form
    {
        readonly World World;
        string selectedMap = "Main";
        Bitmap MapOverlay;
        Size mapSize;
        Size siteSize;
        Site selectedSite;
        int SiteSelection;
        Region selectedRegion;
        UndergroundRegion selectedUndergroundRegion;
        WorldConstruction selectedWC;
        private readonly List<string> selectedSiteTypes = new List<string>();
 
        public MapForm()
        {
            InitializeComponent();
        }

        internal MapForm(World world)
        {
            InitializeComponent();
            World = world;
            LoadMaps();
            LoadSiteTypes();
            LoadUGRegionDepth();
            ChangeMap();
            DrawMaps();
        }

        private Size MiniMapBoxSize 
        { 
            get  
            {
                return new Size((int)((float)pnlMap.ClientSize.Width / picMap.Width * picMiniMap.Width),
                                    (int)((float)pnlMap.ClientSize.Height / picMap.Height * picMiniMap.Height));
            }
            
        }

        private void LoadMaps()
        {

            var  newItem = new ToolStripMenuItem("Main") {CheckOnClick = true, Checked = true};
            newItem.Click += MapSelectionClicked;
            mapsToolStripMenuItem.DropDownItems.Add(newItem);
            foreach (var mapitem in World.Maps)
            {
                if (mapitem.Key == "Main") continue;
                var thisItem = new ToolStripMenuItem(mapitem.Key) {CheckOnClick = true};
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

        private void LoadUGRegionDepth()
        {
            foreach (var ugregion in World.UndergroundRegions.Values)
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
            if (picMap.Image != null)
                picMap.Image.Dispose();
            picMap.Image = Image.FromFile(World.Maps[selectedMap]);
            picMap.Size = picMap.Image.Size;
            if (picMiniMap.Image != null)
                picMiniMap.Image.Dispose();
            
            picMiniMap.Image = Image.FromFile(World.Maps[selectedMap]);

            var sizeString = World.Parameters.First(x => x.Name == "DIM").Value;
            var sizeX = Convert.ToInt32(sizeString.Split(':')[0]);
            var sizeY = Convert.ToInt32(sizeString.Split(':')[1]);
            mapSize = new Size(sizeX, sizeY);
            siteSize = new Size(picMap.Image.Size.Width / mapSize.Width,
                                picMap.Image.Size.Height / mapSize.Height);

            updateLegend();
            redrawOverlay();
            DrawMaps();
        }

        private void DrawMaps()
        {
            picMiniMap.Invalidate();
            picMap.Invalidate();
        }

        #region Overlay Drawing
        
        private void redrawOverlay()
        {
            Cursor.Current = Cursors.WaitCursor;
            if (MapOverlay == null || MapOverlay.Size != new Size(picMap.Image.Width, picMap.Image.Height))
            {
                if (MapOverlay != null)
                    MapOverlay.Dispose();
                MapOverlay = new Bitmap(picMap.Image.Width, picMap.Image.Height);
            }
            var g = Graphics.FromImage(MapOverlay);
            g.Clear(Color.Transparent);
            if (chkSites.Checked)
                DrawSiteOverlay(g);
            if (chkCivilizations.Checked)
                DrawCivOverlay(g);
            if (chkBattles.Checked)
                DrawBattleOverlay(g);
            if (chkRegions.Checked)
                DrawRegionOverlay(g);
            if (chkUGRegions.Checked)
                DrawUndergroundRegionOverlay(g);
            if (chkHistoricalFigures.Checked)
                DrawHFOverlay(g);
            if (chkConstructions.Checked)
                DrawWorldConstructionOverlay(g);

            DrawMaps();
            Cursor.Current = Cursors.Default;
        }

        private void DrawSiteOverlay(Graphics g)
        {
            foreach (var site in World.Sites.Values)
            {
                if (!selectedSiteTypes.Contains(site.SiteType)) 
                    continue;
                if (site.Parent != null || !chkNeutralSites.Checked)
                    continue;


                var myColor = site.Parent == null ? Color.White : site.Parent.Color;
                using (var p = new Pen(myColor))
                {
                    using (Brush b = new SolidBrush(Color.FromArgb(150,myColor)))
                    {
                        g.FillRectangle(b, site.Coords.X * siteSize.Width, site.Coords.Y * siteSize.Height, siteSize.Width - 1, siteSize.Height - 1);
                        g.DrawRectangle(p, site.Coords.X * siteSize.Width, site.Coords.Y * siteSize.Height, siteSize.Width - 1, siteSize.Height - 1);
                    }
                }
            }
            foreach (var site in World.Sites.Values)
            {
                if (!selectedSiteTypes.Contains(site.SiteType))
                    continue;
                if (site.Parent == null || !chkOwnedSites.Checked)
                    continue;


                var myColor = site.Parent == null ? Color.White : site.Parent.Color;
                using (var p = new Pen(myColor))
                {
                    using (Brush b = new SolidBrush(Color.FromArgb(150, myColor)))
                    {
                        g.FillRectangle(b, site.Coords.X * siteSize.Width, site.Coords.Y * siteSize.Height, siteSize.Width - 1, siteSize.Height - 1);
                        g.DrawRectangle(p, site.Coords.X * siteSize.Width, site.Coords.Y * siteSize.Height, siteSize.Width - 1, siteSize.Height - 1);
                    }
                }
            }
        }

        private void DrawCivOverlay(Graphics g)
        {
            foreach (var site in World.Sites.Values)
            {
                if (site.Parent == null || site.Parent.FirstSite == site) continue;
                var myColor = Color.FromArgb(100,site.Parent.Color);
                using (var p = new Pen( myColor))
                {
                    if (site.CreatedEvent != null && site.CreatedEvent.Civ.Civilization != site.Parent)
                        p.DashStyle = DashStyle.Dot;
                    else
                        p.DashStyle = DashStyle.Solid;

                    var Gap = (int)((siteSize.Width + siteSize.Height) / 2.0);
                    var FromPoint = new Point(site.Parent.FirstSite.Coords.X * siteSize.Width + siteSize.Width / 2,
                        site.Parent.FirstSite.Coords.Y * siteSize.Height + siteSize.Height / 2);
                    var ToPoint = new Point(site.Coords.X * siteSize.Width + siteSize.Width / 2,
                        site.Coords.Y * siteSize.Height + siteSize.Height / 2);
                    if (!(Math.Sqrt(Math.Pow(FromPoint.X - ToPoint.X, 2) + Math.Pow(FromPoint.Y - ToPoint.Y, 2)) > Gap))
                        continue;
                    var ang = Math.Atan2(FromPoint.Y - ToPoint.Y, FromPoint.X - ToPoint.X);
                    FromPoint.X = (int)(FromPoint.X - Math.Cos(ang) * Gap);
                    FromPoint.Y = (int)(FromPoint.Y - Math.Sin(ang) * Gap);
                    ToPoint.X = (int)(ToPoint.X + Math.Cos(ang) * Gap);
                    ToPoint.Y = (int)(ToPoint.Y + Math.Sin(ang) * Gap);

                    g.DrawLine(p, FromPoint, ToPoint);
                }
            }

            foreach (var entity in World.Entities.Values)
            {
                if (entity.Coords != null && entity.Civilization != null)
                {
                    using (var p = new Pen(entity.Civilization.Color))
                    {
                        var TopLeft = new Point();
                        var TopRight = new Point();
                        var BottomLeft = new Point();
                        var BottomRight = new Point();
                        using (Brush b = new SolidBrush(Color.FromArgb(50, entity.Civilization.Color)))
                        {
                            foreach (var coord in entity.Coords)
                            {
                                const int AreaMultFactor = 16;
                                TopLeft.X = coord.X * siteSize.Width * AreaMultFactor;
                                TopLeft.Y = coord.Y * siteSize.Height * AreaMultFactor;
                                BottomLeft.X = coord.X * siteSize.Width * AreaMultFactor;
                                BottomLeft.Y = TopLeft.Y + (siteSize.Height * AreaMultFactor) - 1;
                                TopRight.X = TopLeft.X + (siteSize.Width * AreaMultFactor) - 1;
                                TopRight.Y = TopLeft.Y;
                                BottomRight.X = TopRight.X;
                                BottomRight.Y = BottomLeft.Y;

                                g.FillRectangle(b, TopLeft.X, TopLeft.Y, (siteSize.Width * AreaMultFactor) - 1, (siteSize.Height * AreaMultFactor) - 1);


                                //g.DrawRectangle(p, TopLeft.X , TopLeft.Y , siteSize.Width - 1, siteSize.Height - 1);
                                if (!entity.Coords.Contains(new Point(coord.X, coord.Y - 1)))
                                    g.DrawLine(p, TopLeft, TopRight);
                                if (!entity.Coords.Contains(new Point(coord.X + 1, coord.Y)))
                                    g.DrawLine(p, TopRight, BottomRight);
                                if (!entity.Coords.Contains(new Point(coord.X, coord.Y + 1)))
                                    g.DrawLine(p, BottomRight, BottomLeft);
                                if (!entity.Coords.Contains(new Point(coord.X - 1, coord.Y)))
                                    g.DrawLine(p, BottomLeft, TopLeft);
                            }
                        }
                        
                    }
                }
            }
        }

        private void DrawBattleOverlay(Graphics g)
        {
            foreach (var evtcol in World.HistoricalEventCollections.Values.Where(x => HistoricalEventCollection.Types[x.Type] == "battle"))
            {
                var battleEventCol = (EC_Battle) evtcol;
                if (!battleEventCol.battleTotaled) continue;
                var BattlePoint = new Point(battleEventCol.Coords.X * siteSize.Width + siteSize.Width / 2,
                    battleEventCol.Coords.Y * siteSize.Height + siteSize.Height / 2);
                var radius = (int)Math.Sqrt(battleEventCol.BattleData.AttackingDeaths + battleEventCol.BattleData.DefendingDeaths);
                if (battleEventCol.AttackingSquad != null)
                {
                    foreach (var squad in battleEventCol.AttackingSquad)
                    {
                        if (squad.Site == null) continue;
                        var SitePoint = new Point(squad.Site.Coords.X * siteSize.Width + siteSize.Width / 2,
                            squad.Site.Coords.Y * siteSize.Height + siteSize.Height / 2);
                        var alpha = (int)(Math.Sqrt(squad.Number / 10f) < 255 ? Math.Sqrt(squad.Number) : 255);
                        if (alpha == 0)
                            alpha = 1;
                        using (var p = new Pen(Color.FromArgb(alpha, Color.Blue)))
                        {
                            g.DrawLine(p, SitePoint, BattlePoint);
                        }
                    }
                }
                if (battleEventCol.DefendingSquad != null)
                {
                    foreach (var squad in battleEventCol.DefendingSquad)
                    {
                        if (squad.Site == null) continue;
                        var SitePoint = new Point(squad.Site.Coords.X * siteSize.Width + siteSize.Width / 2,
                            squad.Site.Coords.Y * siteSize.Height + siteSize.Height / 2);

                        var alpha = (int)(Math.Sqrt(squad.Number / 10f) < 255 ? Math.Sqrt(squad.Number) : 255); 
                        if (alpha == 0)
                            alpha = 1;
                        using (var p = new Pen(Color.FromArgb(alpha, Color.Green)))
                        {
                            g.DrawLine(p, SitePoint, BattlePoint);
                        }
                    }
                }
                g.DrawEllipse(Pens.Red, BattlePoint.X - radius, BattlePoint.Y - radius, radius * 2, radius * 2);
                g.DrawLine(Pens.Red, new Point(BattlePoint.X - radius, BattlePoint.Y), BattlePoint);
                g.DrawLine(Pens.Red, new Point(BattlePoint.X , BattlePoint.Y - radius), BattlePoint);
            }
        }

        private void DrawRegionOverlay(Graphics g)
        {
            var ColorNames = new List<string>
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
            foreach (var region in World.Regions.Values)
            {
                if (region.Coords == null)
                    continue;
                curDistinctColor++;
                Color thisColor;
                if (curDistinctColor < ColorNames.Count)
                {
                    var rgb = Int32.Parse(ColorNames[curDistinctColor].Replace("#", ""), NumberStyles.HexNumber);
                    thisColor = Color.FromArgb(255,Color.FromArgb(rgb));
                }
                else
                {
                    thisColor = Color.FromArgb(rnd.Next(150) + 100, rnd.Next(150) + 100, rnd.Next(150) + 100);
                }
                using (var p = new Pen(thisColor))
                {
                    var TopLeft = new Point();
                    var TopRight = new Point();
                    var BottomLeft = new Point();
                    var BottomRight = new Point();
                    using (Brush b = new SolidBrush(Color.FromArgb(50, thisColor)))
                    {
                        foreach (var coord in region.Coords)
                        {
                            TopLeft.X = coord.X * siteSize.Width;
                            TopLeft.Y = coord.Y * siteSize.Height;
                            BottomLeft.X = coord.X * siteSize.Width;
                            BottomLeft.Y = TopLeft.Y + siteSize.Height - 1;
                            TopRight.X = TopLeft.X + siteSize.Width - 1;
                            TopRight.Y = coord.Y * siteSize.Height;
                            BottomRight.X = TopRight.X;
                            BottomRight.Y = BottomLeft.Y;

                            g.FillRectangle(b, TopLeft.X, TopLeft.Y, siteSize.Width - 1, siteSize.Height - 1);
                            

                            //g.DrawRectangle(p, TopLeft.X , TopLeft.Y , siteSize.Width - 1, siteSize.Height - 1);
                            if (!region.Coords.Contains(new Point(coord.X, coord.Y-1)))
                                g.DrawLine(p, TopLeft, TopRight);
                            if (!region.Coords.Contains(new Point(coord.X+1, coord.Y)))
                                g.DrawLine(p, TopRight, BottomRight);
                            if (!region.Coords.Contains(new Point(coord.X, coord.Y+1)))
                                g.DrawLine(p, BottomRight, BottomLeft);
                            if (!region.Coords.Contains(new Point(coord.X - 1, coord.Y)))
                                g.DrawLine(p, BottomLeft, TopLeft);
                        }
                    }
                }
            }
        }

        private void DrawUndergroundRegionOverlay(Graphics g)
        {
            var ColorNames = new List<string>
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
            foreach (var ugregion in World.UndergroundRegions.Values)
            {
                if (ugregion.Depth != ugRegionDepthPicker.Value)
                    continue;
                if (ugregion.Coords == null)
                    continue;
                curDistinctColor++;
                Color thisColor;
                if (curDistinctColor < ColorNames.Count)
                {
                    var rgb = Int32.Parse(ColorNames[curDistinctColor].Replace("#", ""), NumberStyles.HexNumber);
                    thisColor = Color.FromArgb(255, Color.FromArgb(rgb));
                }
                else
                {
                    thisColor = Color.FromArgb(rnd.Next(150) + 100, rnd.Next(150) + 100, rnd.Next(150) + 100);
                }
                using (var p = new Pen(thisColor))
                {
                    var TopLeft = new Point();
                    var TopRight = new Point();
                    var BottomLeft = new Point();
                    var BottomRight = new Point();
                    using (Brush b = new SolidBrush(Color.FromArgb(50, thisColor)))
                    {
                        foreach (var coord in ugregion.Coords)
                        {
                            TopLeft.X = coord.X * siteSize.Width;
                            TopLeft.Y = coord.Y * siteSize.Height;
                            BottomLeft.X = coord.X * siteSize.Width;
                            BottomLeft.Y = TopLeft.Y + siteSize.Height - 1;
                            TopRight.X = TopLeft.X + siteSize.Width - 1;
                            TopRight.Y = coord.Y * siteSize.Height;
                            BottomRight.X = TopRight.X;
                            BottomRight.Y = BottomLeft.Y;

                            g.FillRectangle(b, TopLeft.X, TopLeft.Y, siteSize.Width - 1, siteSize.Height - 1);


                            //g.DrawRectangle(p, TopLeft.X , TopLeft.Y , siteSize.Width - 1, siteSize.Height - 1);
                            if (!ugregion.Coords.Contains(new Point(coord.X, coord.Y - 1)))
                                g.DrawLine(p, TopLeft, TopRight);
                            if (!ugregion.Coords.Contains(new Point(coord.X + 1, coord.Y)))
                                g.DrawLine(p, TopRight, BottomRight);
                            if (!ugregion.Coords.Contains(new Point(coord.X, coord.Y + 1)))
                                g.DrawLine(p, BottomRight, BottomLeft);
                            if (!ugregion.Coords.Contains(new Point(coord.X - 1, coord.Y)))
                                g.DrawLine(p, BottomLeft, TopLeft);
                        }
                    }
                }
            }
        }


        private void DrawHFOverlay(Graphics g)
        {
            var CoordPop = new Dictionary<Point, int>();


            foreach (var hf in World.HistoricalFigures.Values.Where(x => x.DiedEvent == null))
            {
                var hfCoord = Point.Empty;
                if (hf.Site != null)
                    hfCoord = hf.Site.Coords;
                else if (hf.Coords != Point.Empty)
                    hfCoord = hf.Coords;
                else if (hf.Region != null)
                    continue;

                if (hfCoord == Point.Empty) continue;
                if (!CoordPop.ContainsKey(hfCoord))
                    CoordPop.Add(hfCoord, 1);
                else
                    CoordPop[hfCoord]++;
            }

            var maxPop = Math.Sqrt(CoordPop.Values.Max());

            foreach (var coord in CoordPop.Keys)
            {
                var red = (int)(255.0f * (Math.Sqrt(CoordPop[coord]) / maxPop));

                using (Brush b = new SolidBrush(Color.FromArgb(225, red, 0, 0)))
                {
                    g.FillRectangle(b, coord.X * siteSize.Width + 1, coord.Y * siteSize.Height + 1, siteSize.Width - 2, siteSize.Height - 2);
                }

            }

        }

        private void DrawWorldConstructionOverlay(Graphics g)
        {
            foreach (var wc in World.WorldConstructions.Values)
            {
                Color myColor;
                if (wc.CreatedEvent != null && wc.CreatedEvent.Civ != null && wc.CreatedEvent.Civ.Civilization != null)
                    myColor = wc.CreatedEvent.Civ.Civilization.Color;
                else
                    myColor = Color.White;
                using (var p = new Pen(myColor))
                {
                    
                    p.DashStyle = DashStyle.Solid;
                    p.Width = 3;

                    if (wc.Coords == null)
                    {
                        var gap = (int) ((siteSize.Width + siteSize.Height)/4.0);
                        var fromPoint = new Point(wc.From.Coords.X*siteSize.Width + siteSize.Width/2,
                            wc.From.Coords.Y*siteSize.Height + siteSize.Height/2);
                        var toPoint = new Point(wc.To.Coords.X*siteSize.Width + siteSize.Width/2,
                            wc.To.Coords.Y*siteSize.Height + siteSize.Height/2);
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
                                var fromPoint = new Point(wc.Coords[i].X * siteSize.Width + siteSize.Width / 2,
                                    wc.Coords[i].Y * siteSize.Height + siteSize.Height / 2);
                                var toPoint = new Point(wc.Coords[i+1].X * siteSize.Width + siteSize.Width / 2,
                                    wc.Coords[i+1].Y * siteSize.Height + siteSize.Height / 2);
                                p.DashStyle = wc.ConstructionType == "tunnel" 
                                    ? DashStyle.Dot 
                                    : DashStyle.Solid;
                                g.DrawLine(p, fromPoint, toPoint);
                            }
                        }
                        else
                        {
                            //var min = Math.Min(siteSize.Width, siteSize.Height);
                            var pt = new[] { new Point(wc.Coords[0].X * siteSize.Width,  wc.Coords[0].Y * siteSize.Height + siteSize.Height / 2),
                                                   new Point(wc.Coords[0].X * siteSize.Width + siteSize.Width / 2,  wc.Coords[0].Y * siteSize.Height), 
                                                   new Point(wc.Coords[0].X * siteSize.Width + siteSize.Width,  wc.Coords[0].Y * siteSize.Height + siteSize.Height / 2) };

                            //g.DrawEllipse(p, new Rectangle(wc.Coords[0].X * siteSize.Width, wc.Coords[0].Y * siteSize.Height, min, min));
                            g.DrawCurve(p, pt);
                        }
                    }
                    
                }
            }

        }

        #endregion
        
        #region Form Events

        private void MapSelectionClicked(object sender, EventArgs e)
        {
            UncheckOtherToolStripMenuItems((ToolStripMenuItem)sender);
            selectedMap = ((ToolStripMenuItem)sender).Text;


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


            e.Graphics.DrawImage(MapOverlay, new Point(0, 0));
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


        private static bool isEnablingSections;

        private void ViewOptionChanged(object sender, EventArgs e)
        {
            if (isEnablingSections)
                return;
            if (sender is CheckBox && sender == chkSites)
            {
                var chkbox = sender as CheckBox;
                grpSites.Visible = chkbox.Checked;
                isEnablingSections = true;
                chkOwnedSites.Checked = chkbox.Checked;
                chkNeutralSites.Checked = chkbox.Checked;
                foreach (ListViewItem item in lstSiteTypes.Items)
                    item.Checked = chkSites.Checked;
                isEnablingSections = false;
            }
            selectedSiteTypes.Clear();
            foreach (ListViewItem item in lstSiteTypes.Items)
            {
                if (item.Checked)
                    selectedSiteTypes.Add(item.Text);
            }
            updateLegend();
            redrawOverlay();

        }

        private void updateLegend()
        {

            if (chkShowLegend.Checked)
            {
                picLegend.Visible = true;
                switch (selectedMap)
                {
                    case "Biome":
                        World.MapLegends["biome_color_key"].DrawTo(picLegend);
                        break;
                    case "Hydrosphere":
                        World.MapLegends["hydro_color_key"].DrawTo(picLegend);
                        break;
                    case "Diplomacy":
                    case "Nobility":
                    case "Structures":
                    case "Trade":
                        World.MapLegends["structure_color_key"].DrawTo(picLegend);
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
            var mouseCoord = new Point(e.X / siteSize.Width, e.Y / siteSize.Height);
            lblMapCoords.Text = string.Format("({0}, {1})", mouseCoord.X, mouseCoord.Y);
            var selectedObject = getSelectedObject(mouseCoord);
            var nameText = "";
            var altNameText = "";
            var ownerText = "";
            var parentText = "";
            var typeText = "";
            var objectTypeText = "";
            if (selectedObject is Site)
            {
                nameText = selectedSite.ToString();
                altNameText = selectedSite.AltName;
                ownerText = selectedSite.Owner != null ? selectedSite.Owner.ToString() : "";
                parentText = selectedSite.Parent != null ? selectedSite.Parent.ToString() : "";
                typeText = selectedSite.SiteType;
                objectTypeText = "Site";
            }
            if (selectedObject is WorldConstruction)
            {
                nameText = selectedWC.ToString();
                typeText = selectedWC.ConstructionType;
                if (selectedWC.CreatedEvent != null && selectedWC.CreatedEvent.Civ != null &&
                    selectedWC.CreatedEvent.Civ.Civilization != null)
                    ownerText = selectedWC.CreatedEvent.Civ.Civilization.ToString();

                lblMapAltNameCaption.Visible = false;
                if (selectedWC.MasterWC != null)
                    parentText = selectedWC.MasterWC.ToString();
                objectTypeText = "World Construction";
            }
            if (selectedObject is Region)
            {

                nameText = selectedRegion.ToString();
                typeText = WorldClasses.Region.Types[selectedRegion.Type];
                objectTypeText = "Region";
            }
            if (selectedObject is UndergroundRegion)
            {
                nameText = selectedUndergroundRegion.ToString();
                objectTypeText = "Underground Region";
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
                picLegend.Left = e.X + 10;
                picLegend.Top = e.Y + 10;
            }
        }

        private WorldObject getSelectedObject(Point mouseCoord)
        {
            if (chkSites.Checked)
            {
                selectedSite = GetSiteAt(mouseCoord);
                if (selectedSite != null)
                    return selectedSite;
            }
            if (chkConstructions.Checked)
            {
                selectedWC = GetWorldConstructionAt(mouseCoord);
                if (selectedWC != null)
                    return selectedWC;
            }
            if (chkRegions.Checked)
            {
                selectedRegion = GetRegionAt(mouseCoord);
                if (selectedRegion != null)
                    return selectedRegion;
            }
            if (chkUGRegions.Checked)
            {
                selectedUndergroundRegion = GetUndergroundRegionAt(mouseCoord);
                if (selectedUndergroundRegion != null)
                    return selectedUndergroundRegion;
            }
            return null;
        }

        private WorldConstruction GetWorldConstructionAt(Point coord)
        {
            if (World.hasPlusXML)
            {
                var returnWC = World.WorldConstructions.Values.Where(wc => wc.Coords != null && wc.ConstructionType != "road")
                        .FirstOrDefault(wc => wc.Coords.Contains(coord));
                if (returnWC != null)
                    return returnWC;

                return World.WorldConstructions.Values.Where(wc => wc.Coords != null && wc.ConstructionType == "road")
                    .FirstOrDefault(wc => wc.Coords.Contains(coord));
            }
            return
                World.WorldConstructions.Values.Where(wc => wc.From != null && wc.To != null)
                    .FirstOrDefault(wc => wc.From.Coords == coord || wc.To.Coords == coord);
        }

        private Region GetRegionAt(Point coord)
        {
            return World.Regions.Values.Where(region => region.Coords != null).FirstOrDefault(region => region.Coords.Contains(coord));
        }
        private UndergroundRegion GetUndergroundRegionAt(Point coord)
        {
            return World.UndergroundRegions.Values.Where(ugregion => ugregion.Coords != null).FirstOrDefault(ugregion => ugregion.Coords.Contains(coord) && ugregion.Depth == ugRegionDepthPicker.Value);
        }

        private Site GetSiteAt(Point coord)
        {
            return World.Sites.Values.FirstOrDefault(
                site => selectedSiteTypes.Contains(site.SiteType) 
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
                    if (chkSites.Checked && selectedSite != null)
                    {
                        selectedSite.Select(Program.mainForm);
                        Program.mainForm.BringToFront();
                    }
                    else if (chkConstructions.Checked && selectedWC != null)
                    {
                        selectedWC.Select(Program.mainForm);
                        Program.mainForm.BringToFront();
                    }
                    else if (chkRegions.Checked && selectedRegion != null)
                    {
                        selectedRegion.Select(Program.mainForm);
                        Program.mainForm.BringToFront();
                    }
                    else if (chkUGRegions.Checked && selectedUndergroundRegion != null)
                    {
                        selectedUndergroundRegion.Select(Program.mainForm);
                        Program.mainForm.BringToFront();
                    }
                    break;
                case MouseButtons.Right:
                    SiteSelection++;
                    break;
            }
        }

        #endregion


        internal void Select(Point loc)
        {
            BringToFront();

            MiniMapCenterOn(new Point((int)(picMiniMap.Width * (float)loc.X / mapSize.Width), (int)(picMiniMap.Height * (float)loc.Y / mapSize.Height)));

            var locationOnForm = picMap.PointToScreen(Point.Empty);
                
            Cursor.Position = new Point(loc.X * siteSize.Width + locationOnForm.X + siteSize.Width / 2, loc.Y * siteSize.Height + locationOnForm.Y + siteSize.Height / 2);

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


    }
}
