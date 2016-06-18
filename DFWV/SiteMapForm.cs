using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using DFWV.WorldClasses;

namespace DFWV
{
    public partial class SiteMapForm : Form
    {
        readonly World _world;
        private Site _site;

        public MapLegend CurLegend { get; set; }


        internal new Site Site
        {
            get { return _site; }
            set
            {
                _site = value;
                var siteMapPath = _site.SiteMapPath;
                if (siteMapPath != null && File.Exists(siteMapPath))
                {
                    picSiteMap.ImageLocation = siteMapPath;
                    picSiteMap.Load();
                    picSiteMap.SizeMode = PictureBoxSizeMode.AutoSize;
                    Width = picSiteMap.Right + 27;
                    Height = Math.Max(picSiteMap.Bottom, picSiteMapLegend.Bottom) + 51;
                    CurLegend = _site.Type != null && Site.Types[_site.Type.Value].Contains("dark") ? _world.MapLegends["site_color_key_dark"] : _world.MapLegends["site_color_key"];
                    CurLegend?.DrawTo(picSiteMapLegend);
                }
                if (_site.Type != null)
                    lblSiteName.Text = $"{_site.Name} \"{_site.AltName}\" ({Site.Types[_site.Type.Value]})";
                else
                    lblSiteName.Text = $"{_site.Name} \"{_site.AltName}\"";
            }
        }


        public SiteMapForm()
        {
            InitializeComponent();
        }

        internal SiteMapForm(World world)
        {
            InitializeComponent();
            _world = world;
        }

        private void SiteMapForm_Load(object sender, EventArgs e)
        {

        }

        private void picSiteMap_Click(object sender, EventArgs e)
        {

        }

        private int _lastX;
        private int _lastY;

        private void picSiteMap_MouseMove(object sender, MouseEventArgs e)
        {
            var pixel = (picSiteMap.Image as Bitmap).GetPixel(e.X, e.Y);

            if (e.X == _lastX && e.Y == _lastY)
                return;
            _lastX = e.X;
            _lastY = e.Y;
            var pixeltext = CurLegend.NameForColor(pixel);

            if (pixeltext != string.Empty)
            {
                toolTip.SetToolTip(picSiteMap, pixeltext);
                if (!toolTip.Active)
                    toolTip.Active = true;
            }
            else
                toolTip.Active = false;
        }
    }
}
