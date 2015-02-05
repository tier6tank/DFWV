namespace DFWV
{
    partial class SiteMapForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.picSiteMapLegend = new System.Windows.Forms.PictureBox();
            this.picSiteMap = new System.Windows.Forms.PictureBox();
            this.lblSiteName = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.picSiteMapLegend)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picSiteMap)).BeginInit();
            this.SuspendLayout();
            // 
            // picSiteMapLegend
            // 
            this.picSiteMapLegend.Location = new System.Drawing.Point(12, 31);
            this.picSiteMapLegend.Name = "picSiteMapLegend";
            this.picSiteMapLegend.Size = new System.Drawing.Size(184, 399);
            this.picSiteMapLegend.TabIndex = 0;
            this.picSiteMapLegend.TabStop = false;
            // 
            // picSiteMap
            // 
            this.picSiteMap.Location = new System.Drawing.Point(202, 31);
            this.picSiteMap.Name = "picSiteMap";
            this.picSiteMap.Size = new System.Drawing.Size(561, 399);
            this.picSiteMap.TabIndex = 1;
            this.picSiteMap.TabStop = false;
            this.picSiteMap.Click += new System.EventHandler(this.picSiteMap_Click);
            // 
            // lblSiteName
            // 
            this.lblSiteName.AutoSize = true;
            this.lblSiteName.Location = new System.Drawing.Point(13, 12);
            this.lblSiteName.Name = "lblSiteName";
            this.lblSiteName.Size = new System.Drawing.Size(35, 13);
            this.lblSiteName.TabIndex = 2;
            this.lblSiteName.Text = "label1";
            // 
            // SiteMapForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(774, 442);
            this.Controls.Add(this.lblSiteName);
            this.Controls.Add(this.picSiteMap);
            this.Controls.Add(this.picSiteMapLegend);
            this.Name = "SiteMapForm";
            this.Text = "SiteMapForm";
            this.Load += new System.EventHandler(this.SiteMapForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picSiteMapLegend)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picSiteMap)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox picSiteMapLegend;
        private System.Windows.Forms.PictureBox picSiteMap;
        private System.Windows.Forms.Label lblSiteName;
    }
}