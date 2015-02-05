using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DFWV.WorldClasses;

namespace DFWV
{
    public partial class SiteMapForm : Form
    {
        readonly World World;
        private Site site;

        internal Site Site
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
                    this.Width = picSiteMap.Right + 27;
                    this.Height = Math.Max(picSiteMap.Bottom, picSiteMapLegend.Bottom) + 51;
                    if (World.MapLegends.ContainsKey("site_color_key"))
                    {
                        World.MapLegends["site_color_key"].DrawTo(picSiteMapLegend);
                    }
                }
                lblSiteName.Text = String.Format("{0} \"{1}\" ({2})", site.Name, site.AltName,
                    Site.Types[site.Type.Value]);
            }
        }


        public SiteMapForm()
        {
            InitializeComponent();
        }

        internal SiteMapForm(World world) : base()
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
    }
}
