using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using DFWV.WorldClasses;

namespace DFWV
{
    public partial class SiteMapForm : Form
    {
        readonly World World;
        private Site site;

        public MapLegend curLegend { get; set; }


        internal new Site Site
        {
            get { return site; }
            set
            {
                site = value;
                var siteMapPath = site.SiteMapPath;
                if (siteMapPath != null && File.Exists(siteMapPath))
                {
                    picSiteMap.ImageLocation = siteMapPath;
                    picSiteMap.Load();
                    picSiteMap.SizeMode = PictureBoxSizeMode.AutoSize;
                    Width = picSiteMap.Right + 27;
                    Height = Math.Max(picSiteMap.Bottom, picSiteMapLegend.Bottom) + 51;
                    curLegend = Site.Types[site.Type.Value].Contains("dark") ? World.MapLegends["site_color_key_dark"] : World.MapLegends["site_color_key"];
                    curLegend.DrawTo(picSiteMapLegend);

                }
                lblSiteName.Text = string.Format("{0} \"{1}\" ({2})", site.Name, site.AltName,
                    Site.Types[site.Type.Value]);
            }
        }


        public SiteMapForm()
        {
            InitializeComponent();
        }

        internal SiteMapForm(World world)
        {
            InitializeComponent();
            World = world;
        }

        private void SiteMapForm_Load(object sender, EventArgs e)
        {

        }

        private void picSiteMap_Click(object sender, EventArgs e)
        {

        }

        private int lastX;
        private int lastY;

        private void picSiteMap_MouseMove(object sender, MouseEventArgs e)
        {
            var pixel = (picSiteMap.Image as Bitmap).GetPixel(e.X, e.Y);

            if (e.X == lastX && e.Y == lastY)
                return;
            lastX = e.X;
            lastY = e.Y;
            var pixeltext = curLegend.NameForColor(pixel);

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
