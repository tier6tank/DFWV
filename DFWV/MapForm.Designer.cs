using System.ComponentModel;
using System.Windows.Forms;

namespace DFWV
{
    partial class MapForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.mapsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.picMap = new System.Windows.Forms.PictureBox();
            this.mapTableLayout = new System.Windows.Forms.TableLayoutPanel();
            this.pnlMap = new System.Windows.Forms.Panel();
            this.picLegend = new System.Windows.Forms.PictureBox();
            this.picMiniMap = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblMapObject = new System.Windows.Forms.Label();
            this.lblMapParent = new System.Windows.Forms.Label();
            this.lblMapOwner = new System.Windows.Forms.Label();
            this.lblMapCoords = new System.Windows.Forms.Label();
            this.lblMapOwnerCaption = new System.Windows.Forms.Label();
            this.lblMapType = new System.Windows.Forms.Label();
            this.lblMapTypeCaption = new System.Windows.Forms.Label();
            this.lblMapParentCaption = new System.Windows.Forms.Label();
            this.lblMapAltName = new System.Windows.Forms.Label();
            this.lblMapAltNameCaption = new System.Windows.Forms.Label();
            this.lblMapName = new System.Windows.Forms.Label();
            this.lblMapNameCaption = new System.Windows.Forms.Label();
            this.grpSettings = new System.Windows.Forms.GroupBox();
            this.cmbHFTravels = new System.Windows.Forms.ComboBox();
            this.chkHFTravels = new System.Windows.Forms.CheckBox();
            this.chkRivers = new System.Windows.Forms.CheckBox();
            this.chkMountains = new System.Windows.Forms.CheckBox();
            this.chkShowLegend = new System.Windows.Forms.CheckBox();
            this.ugRegionDepthPicker = new System.Windows.Forms.NumericUpDown();
            this.btnOK = new System.Windows.Forms.Button();
            this.chkSites = new System.Windows.Forms.CheckBox();
            this.grpSites = new System.Windows.Forms.GroupBox();
            this.lstSiteTypes = new System.Windows.Forms.ListView();
            this.chkOwnedSites = new System.Windows.Forms.CheckBox();
            this.chkNeutralSites = new System.Windows.Forms.CheckBox();
            this.chkConstructions = new System.Windows.Forms.CheckBox();
            this.chkHistoricalFigures = new System.Windows.Forms.CheckBox();
            this.chkUGRegions = new System.Windows.Forms.CheckBox();
            this.chkRegions = new System.Windows.Forms.CheckBox();
            this.chkBattles = new System.Windows.Forms.CheckBox();
            this.chkCivilizations = new System.Windows.Forms.CheckBox();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picMap)).BeginInit();
            this.mapTableLayout.SuspendLayout();
            this.pnlMap.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLegend)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picMiniMap)).BeginInit();
            this.panel1.SuspendLayout();
            this.grpSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ugRegionDepthPicker)).BeginInit();
            this.grpSites.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mapsToolStripMenuItem,
            this.settingsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(901, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // mapsToolStripMenuItem
            // 
            this.mapsToolStripMenuItem.Name = "mapsToolStripMenuItem";
            this.mapsToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.mapsToolStripMenuItem.Text = "&Maps";
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.settingsToolStripMenuItem.Text = "Settings";
            this.settingsToolStripMenuItem.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click);
            // 
            // picMap
            // 
            this.picMap.Location = new System.Drawing.Point(0, 0);
            this.picMap.Margin = new System.Windows.Forms.Padding(2);
            this.picMap.Name = "picMap";
            this.picMap.Size = new System.Drawing.Size(411, 397);
            this.picMap.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picMap.TabIndex = 1;
            this.picMap.TabStop = false;
            this.picMap.Paint += new System.Windows.Forms.PaintEventHandler(this.picMap_Paint);
            this.picMap.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picMap_MouseDown);
            this.picMap.MouseMove += new System.Windows.Forms.MouseEventHandler(this.picMap_MouseMove);
            // 
            // mapTableLayout
            // 
            this.mapTableLayout.ColumnCount = 2;
            this.mapTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.mapTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 188F));
            this.mapTableLayout.Controls.Add(this.pnlMap, 0, 0);
            this.mapTableLayout.Controls.Add(this.picMiniMap, 1, 1);
            this.mapTableLayout.Controls.Add(this.panel1, 1, 0);
            this.mapTableLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mapTableLayout.Location = new System.Drawing.Point(0, 24);
            this.mapTableLayout.Margin = new System.Windows.Forms.Padding(2);
            this.mapTableLayout.Name = "mapTableLayout";
            this.mapTableLayout.RowCount = 2;
            this.mapTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.mapTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 122F));
            this.mapTableLayout.Size = new System.Drawing.Size(901, 529);
            this.mapTableLayout.TabIndex = 3;
            // 
            // pnlMap
            // 
            this.pnlMap.AutoScroll = true;
            this.pnlMap.Controls.Add(this.picLegend);
            this.pnlMap.Controls.Add(this.picMap);
            this.pnlMap.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMap.Location = new System.Drawing.Point(2, 2);
            this.pnlMap.Margin = new System.Windows.Forms.Padding(2);
            this.pnlMap.Name = "pnlMap";
            this.mapTableLayout.SetRowSpan(this.pnlMap, 2);
            this.pnlMap.Size = new System.Drawing.Size(709, 525);
            this.pnlMap.TabIndex = 2;
            this.pnlMap.Scroll += new System.Windows.Forms.ScrollEventHandler(this.pnlMap_Scroll);
            // 
            // picLegend
            // 
            this.picLegend.Location = new System.Drawing.Point(0, 1);
            this.picLegend.Name = "picLegend";
            this.picLegend.Size = new System.Drawing.Size(242, 521);
            this.picLegend.TabIndex = 6;
            this.picLegend.TabStop = false;
            this.picLegend.Visible = false;
            this.picLegend.MouseMove += new System.Windows.Forms.MouseEventHandler(this.picLegend_MouseMove);
            // 
            // picMiniMap
            // 
            this.picMiniMap.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picMiniMap.Location = new System.Drawing.Point(715, 409);
            this.picMiniMap.Margin = new System.Windows.Forms.Padding(2);
            this.picMiniMap.Name = "picMiniMap";
            this.picMiniMap.Size = new System.Drawing.Size(184, 118);
            this.picMiniMap.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picMiniMap.TabIndex = 3;
            this.picMiniMap.TabStop = false;
            this.picMiniMap.Paint += new System.Windows.Forms.PaintEventHandler(this.picMiniMap_Paint);
            this.picMiniMap.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picMiniMap_MouseDown);
            this.picMiniMap.MouseMove += new System.Windows.Forms.MouseEventHandler(this.picMiniMap_MouseMove);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblMapObject);
            this.panel1.Controls.Add(this.lblMapParent);
            this.panel1.Controls.Add(this.lblMapOwner);
            this.panel1.Controls.Add(this.lblMapCoords);
            this.panel1.Controls.Add(this.lblMapOwnerCaption);
            this.panel1.Controls.Add(this.lblMapType);
            this.panel1.Controls.Add(this.lblMapTypeCaption);
            this.panel1.Controls.Add(this.lblMapParentCaption);
            this.panel1.Controls.Add(this.lblMapAltName);
            this.panel1.Controls.Add(this.lblMapAltNameCaption);
            this.panel1.Controls.Add(this.lblMapName);
            this.panel1.Controls.Add(this.lblMapNameCaption);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(715, 2);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(184, 403);
            this.panel1.TabIndex = 4;
            // 
            // lblMapObject
            // 
            this.lblMapObject.AutoSize = true;
            this.lblMapObject.Location = new System.Drawing.Point(70, 8);
            this.lblMapObject.Name = "lblMapObject";
            this.lblMapObject.Size = new System.Drawing.Size(38, 13);
            this.lblMapObject.TabIndex = 76;
            this.lblMapObject.Text = "Object";
            this.lblMapObject.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblMapParent
            // 
            this.lblMapParent.AutoSize = true;
            this.lblMapParent.Location = new System.Drawing.Point(58, 117);
            this.lblMapParent.Name = "lblMapParent";
            this.lblMapParent.Size = new System.Drawing.Size(25, 13);
            this.lblMapParent.TabIndex = 75;
            this.lblMapParent.Text = "      ";
            // 
            // lblMapOwner
            // 
            this.lblMapOwner.AutoSize = true;
            this.lblMapOwner.Location = new System.Drawing.Point(58, 96);
            this.lblMapOwner.Name = "lblMapOwner";
            this.lblMapOwner.Size = new System.Drawing.Size(25, 13);
            this.lblMapOwner.TabIndex = 74;
            this.lblMapOwner.Text = "      ";
            // 
            // lblMapCoords
            // 
            this.lblMapCoords.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblMapCoords.AutoSize = true;
            this.lblMapCoords.Location = new System.Drawing.Point(1, 389);
            this.lblMapCoords.Name = "lblMapCoords";
            this.lblMapCoords.Size = new System.Drawing.Size(35, 13);
            this.lblMapCoords.TabIndex = 73;
            this.lblMapCoords.Text = "label2";
            // 
            // lblMapOwnerCaption
            // 
            this.lblMapOwnerCaption.AutoSize = true;
            this.lblMapOwnerCaption.Location = new System.Drawing.Point(3, 96);
            this.lblMapOwnerCaption.Name = "lblMapOwnerCaption";
            this.lblMapOwnerCaption.Size = new System.Drawing.Size(41, 13);
            this.lblMapOwnerCaption.TabIndex = 72;
            this.lblMapOwnerCaption.Text = "Owner:";
            // 
            // lblMapType
            // 
            this.lblMapType.AutoSize = true;
            this.lblMapType.Location = new System.Drawing.Point(58, 75);
            this.lblMapType.Name = "lblMapType";
            this.lblMapType.Size = new System.Drawing.Size(25, 13);
            this.lblMapType.TabIndex = 67;
            this.lblMapType.Text = "      ";
            // 
            // lblMapTypeCaption
            // 
            this.lblMapTypeCaption.AutoSize = true;
            this.lblMapTypeCaption.Location = new System.Drawing.Point(3, 75);
            this.lblMapTypeCaption.Name = "lblMapTypeCaption";
            this.lblMapTypeCaption.Size = new System.Drawing.Size(34, 13);
            this.lblMapTypeCaption.TabIndex = 66;
            this.lblMapTypeCaption.Text = "Type:";
            // 
            // lblMapParentCaption
            // 
            this.lblMapParentCaption.AutoSize = true;
            this.lblMapParentCaption.Location = new System.Drawing.Point(3, 117);
            this.lblMapParentCaption.Name = "lblMapParentCaption";
            this.lblMapParentCaption.Size = new System.Drawing.Size(59, 13);
            this.lblMapParentCaption.TabIndex = 65;
            this.lblMapParentCaption.Text = "Parent Civ:";
            // 
            // lblMapAltName
            // 
            this.lblMapAltName.AutoSize = true;
            this.lblMapAltName.Location = new System.Drawing.Point(58, 54);
            this.lblMapAltName.Name = "lblMapAltName";
            this.lblMapAltName.Size = new System.Drawing.Size(25, 13);
            this.lblMapAltName.TabIndex = 64;
            this.lblMapAltName.Text = "      ";
            // 
            // lblMapAltNameCaption
            // 
            this.lblMapAltNameCaption.AutoSize = true;
            this.lblMapAltNameCaption.Location = new System.Drawing.Point(3, 54);
            this.lblMapAltNameCaption.Name = "lblMapAltNameCaption";
            this.lblMapAltNameCaption.Size = new System.Drawing.Size(58, 13);
            this.lblMapAltNameCaption.TabIndex = 63;
            this.lblMapAltNameCaption.Text = "Nickname:";
            // 
            // lblMapName
            // 
            this.lblMapName.AutoSize = true;
            this.lblMapName.Location = new System.Drawing.Point(58, 32);
            this.lblMapName.Name = "lblMapName";
            this.lblMapName.Size = new System.Drawing.Size(25, 13);
            this.lblMapName.TabIndex = 62;
            this.lblMapName.Text = "      ";
            // 
            // lblMapNameCaption
            // 
            this.lblMapNameCaption.AutoSize = true;
            this.lblMapNameCaption.Location = new System.Drawing.Point(3, 32);
            this.lblMapNameCaption.Name = "lblMapNameCaption";
            this.lblMapNameCaption.Size = new System.Drawing.Size(41, 13);
            this.lblMapNameCaption.TabIndex = 61;
            this.lblMapNameCaption.Text = "Name: ";
            // 
            // grpSettings
            // 
            this.grpSettings.Controls.Add(this.cmbHFTravels);
            this.grpSettings.Controls.Add(this.chkHFTravels);
            this.grpSettings.Controls.Add(this.chkRivers);
            this.grpSettings.Controls.Add(this.chkMountains);
            this.grpSettings.Controls.Add(this.chkShowLegend);
            this.grpSettings.Controls.Add(this.ugRegionDepthPicker);
            this.grpSettings.Controls.Add(this.btnOK);
            this.grpSettings.Controls.Add(this.chkSites);
            this.grpSettings.Controls.Add(this.grpSites);
            this.grpSettings.Controls.Add(this.chkConstructions);
            this.grpSettings.Controls.Add(this.chkHistoricalFigures);
            this.grpSettings.Controls.Add(this.chkUGRegions);
            this.grpSettings.Controls.Add(this.chkRegions);
            this.grpSettings.Controls.Add(this.chkBattles);
            this.grpSettings.Controls.Add(this.chkCivilizations);
            this.grpSettings.Location = new System.Drawing.Point(64, 80);
            this.grpSettings.Name = "grpSettings";
            this.grpSettings.Size = new System.Drawing.Size(361, 284);
            this.grpSettings.TabIndex = 5;
            this.grpSettings.TabStop = false;
            this.grpSettings.Text = "Settings";
            this.grpSettings.Visible = false;
            // 
            // cmbHFTravels
            // 
            this.cmbHFTravels.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbHFTravels.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbHFTravels.FormattingEnabled = true;
            this.cmbHFTravels.Location = new System.Drawing.Point(17, 228);
            this.cmbHFTravels.Name = "cmbHFTravels";
            this.cmbHFTravels.Size = new System.Drawing.Size(156, 21);
            this.cmbHFTravels.TabIndex = 14;
            this.cmbHFTravels.Validating += new System.ComponentModel.CancelEventHandler(this.cmbHFTravels_Validating);
            // 
            // chkHFTravels
            // 
            this.chkHFTravels.AutoSize = true;
            this.chkHFTravels.Location = new System.Drawing.Point(6, 205);
            this.chkHFTravels.Name = "chkHFTravels";
            this.chkHFTravels.Size = new System.Drawing.Size(76, 17);
            this.chkHFTravels.TabIndex = 13;
            this.chkHFTravels.Text = "Travels of:";
            this.chkHFTravels.UseVisualStyleBackColor = true;
            this.chkHFTravels.Click += new System.EventHandler(this.ViewOptionChanged);
            // 
            // chkRivers
            // 
            this.chkRivers.AutoSize = true;
            this.chkRivers.Location = new System.Drawing.Point(6, 181);
            this.chkRivers.Name = "chkRivers";
            this.chkRivers.Size = new System.Drawing.Size(56, 17);
            this.chkRivers.TabIndex = 12;
            this.chkRivers.Text = "Rivers";
            this.chkRivers.UseVisualStyleBackColor = true;
            this.chkRivers.Click += new System.EventHandler(this.ViewOptionChanged);
            // 
            // chkMountains
            // 
            this.chkMountains.AutoSize = true;
            this.chkMountains.Location = new System.Drawing.Point(6, 158);
            this.chkMountains.Name = "chkMountains";
            this.chkMountains.Size = new System.Drawing.Size(75, 17);
            this.chkMountains.TabIndex = 11;
            this.chkMountains.Text = "Mountains";
            this.chkMountains.UseVisualStyleBackColor = true;
            this.chkMountains.Click += new System.EventHandler(this.ViewOptionChanged);
            // 
            // chkShowLegend
            // 
            this.chkShowLegend.AutoSize = true;
            this.chkShowLegend.Checked = true;
            this.chkShowLegend.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkShowLegend.Location = new System.Drawing.Point(88, 259);
            this.chkShowLegend.Name = "chkShowLegend";
            this.chkShowLegend.Size = new System.Drawing.Size(92, 17);
            this.chkShowLegend.TabIndex = 10;
            this.chkShowLegend.Text = "Show Legend";
            this.chkShowLegend.UseVisualStyleBackColor = true;
            this.chkShowLegend.Click += new System.EventHandler(this.ViewOptionChanged);
            // 
            // ugRegionDepthPicker
            // 
            this.ugRegionDepthPicker.Location = new System.Drawing.Point(142, 86);
            this.ugRegionDepthPicker.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.ugRegionDepthPicker.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.ugRegionDepthPicker.Name = "ugRegionDepthPicker";
            this.ugRegionDepthPicker.Size = new System.Drawing.Size(31, 20);
            this.ugRegionDepthPicker.TabIndex = 9;
            this.ugRegionDepthPicker.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.ugRegionDepthPicker.ValueChanged += new System.EventHandler(this.ViewOptionChanged);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(0, 255);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 8;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // chkSites
            // 
            this.chkSites.AutoSize = true;
            this.chkSites.Location = new System.Drawing.Point(195, 19);
            this.chkSites.Name = "chkSites";
            this.chkSites.Size = new System.Drawing.Size(49, 17);
            this.chkSites.TabIndex = 0;
            this.chkSites.Text = "Sites";
            this.chkSites.UseVisualStyleBackColor = true;
            this.chkSites.Click += new System.EventHandler(this.ViewOptionChanged);
            // 
            // grpSites
            // 
            this.grpSites.Controls.Add(this.lstSiteTypes);
            this.grpSites.Controls.Add(this.chkOwnedSites);
            this.grpSites.Controls.Add(this.chkNeutralSites);
            this.grpSites.Location = new System.Drawing.Point(186, 19);
            this.grpSites.Name = "grpSites";
            this.grpSites.Size = new System.Drawing.Size(163, 257);
            this.grpSites.TabIndex = 7;
            this.grpSites.TabStop = false;
            this.grpSites.Visible = false;
            // 
            // lstSiteTypes
            // 
            this.lstSiteTypes.CheckBoxes = true;
            this.lstSiteTypes.Location = new System.Drawing.Point(6, 70);
            this.lstSiteTypes.Name = "lstSiteTypes";
            this.lstSiteTypes.Size = new System.Drawing.Size(151, 173);
            this.lstSiteTypes.TabIndex = 3;
            this.lstSiteTypes.UseCompatibleStateImageBehavior = false;
            this.lstSiteTypes.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.lstSiteTypes_ItemChecked);
            // 
            // chkOwnedSites
            // 
            this.chkOwnedSites.AutoSize = true;
            this.chkOwnedSites.Location = new System.Drawing.Point(19, 43);
            this.chkOwnedSites.Name = "chkOwnedSites";
            this.chkOwnedSites.Size = new System.Drawing.Size(86, 17);
            this.chkOwnedSites.TabIndex = 3;
            this.chkOwnedSites.Text = "Owned Sites";
            this.chkOwnedSites.UseVisualStyleBackColor = true;
            this.chkOwnedSites.Click += new System.EventHandler(this.ViewOptionChanged);
            // 
            // chkNeutralSites
            // 
            this.chkNeutralSites.AutoSize = true;
            this.chkNeutralSites.Location = new System.Drawing.Point(19, 19);
            this.chkNeutralSites.Name = "chkNeutralSites";
            this.chkNeutralSites.Size = new System.Drawing.Size(86, 17);
            this.chkNeutralSites.TabIndex = 2;
            this.chkNeutralSites.Text = "Neutral Sites";
            this.chkNeutralSites.UseVisualStyleBackColor = true;
            this.chkNeutralSites.Click += new System.EventHandler(this.ViewOptionChanged);
            // 
            // chkConstructions
            // 
            this.chkConstructions.AutoSize = true;
            this.chkConstructions.Location = new System.Drawing.Point(6, 135);
            this.chkConstructions.Name = "chkConstructions";
            this.chkConstructions.Size = new System.Drawing.Size(90, 17);
            this.chkConstructions.TabIndex = 6;
            this.chkConstructions.Text = "Constructions";
            this.chkConstructions.UseVisualStyleBackColor = true;
            this.chkConstructions.Click += new System.EventHandler(this.ViewOptionChanged);
            // 
            // chkHistoricalFigures
            // 
            this.chkHistoricalFigures.AutoSize = true;
            this.chkHistoricalFigures.Location = new System.Drawing.Point(6, 112);
            this.chkHistoricalFigures.Name = "chkHistoricalFigures";
            this.chkHistoricalFigures.Size = new System.Drawing.Size(106, 17);
            this.chkHistoricalFigures.TabIndex = 5;
            this.chkHistoricalFigures.Text = "Historical Figures";
            this.chkHistoricalFigures.UseVisualStyleBackColor = true;
            this.chkHistoricalFigures.Click += new System.EventHandler(this.ViewOptionChanged);
            // 
            // chkUGRegions
            // 
            this.chkUGRegions.AutoSize = true;
            this.chkUGRegions.Location = new System.Drawing.Point(6, 89);
            this.chkUGRegions.Name = "chkUGRegions";
            this.chkUGRegions.Size = new System.Drawing.Size(130, 17);
            this.chkUGRegions.TabIndex = 4;
            this.chkUGRegions.Text = "Underground Regions";
            this.chkUGRegions.UseVisualStyleBackColor = true;
            this.chkUGRegions.Click += new System.EventHandler(this.ViewOptionChanged);
            // 
            // chkRegions
            // 
            this.chkRegions.AutoSize = true;
            this.chkRegions.Location = new System.Drawing.Point(6, 66);
            this.chkRegions.Name = "chkRegions";
            this.chkRegions.Size = new System.Drawing.Size(65, 17);
            this.chkRegions.TabIndex = 3;
            this.chkRegions.Text = "Regions";
            this.chkRegions.UseVisualStyleBackColor = true;
            this.chkRegions.Click += new System.EventHandler(this.ViewOptionChanged);
            // 
            // chkBattles
            // 
            this.chkBattles.AutoSize = true;
            this.chkBattles.Location = new System.Drawing.Point(6, 43);
            this.chkBattles.Name = "chkBattles";
            this.chkBattles.Size = new System.Drawing.Size(58, 17);
            this.chkBattles.TabIndex = 2;
            this.chkBattles.Text = "Battles";
            this.chkBattles.UseVisualStyleBackColor = true;
            this.chkBattles.Click += new System.EventHandler(this.ViewOptionChanged);
            // 
            // chkCivilizations
            // 
            this.chkCivilizations.AutoSize = true;
            this.chkCivilizations.Location = new System.Drawing.Point(6, 20);
            this.chkCivilizations.Name = "chkCivilizations";
            this.chkCivilizations.Size = new System.Drawing.Size(80, 17);
            this.chkCivilizations.TabIndex = 1;
            this.chkCivilizations.Text = "Civilizations";
            this.chkCivilizations.UseVisualStyleBackColor = true;
            this.chkCivilizations.Click += new System.EventHandler(this.ViewOptionChanged);
            // 
            // MapForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(901, 553);
            this.Controls.Add(this.grpSettings);
            this.Controls.Add(this.mapTableLayout);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "MapForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Map";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MapForm_FormClosing);
            this.Load += new System.EventHandler(this.MapForm_Load);
            this.Move += new System.EventHandler(this.MapForm_Move);
            this.Resize += new System.EventHandler(this.MapForm_Resize);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picMap)).EndInit();
            this.mapTableLayout.ResumeLayout(false);
            this.pnlMap.ResumeLayout(false);
            this.pnlMap.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLegend)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picMiniMap)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.grpSettings.ResumeLayout(false);
            this.grpSettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ugRegionDepthPicker)).EndInit();
            this.grpSites.ResumeLayout(false);
            this.grpSites.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MenuStrip menuStrip1;
        private PictureBox picMap;
        private TableLayoutPanel mapTableLayout;
        private Panel pnlMap;
        private PictureBox picMiniMap;
        private ToolStripMenuItem mapsToolStripMenuItem;
        private Panel panel1;
        private Label lblMapOwnerCaption;
        private Label lblMapTypeCaption;
        private Label lblMapParentCaption;
        public Label lblMapAltName;
        private Label lblMapAltNameCaption;
        public Label lblMapName;
        private Label lblMapNameCaption;
        public Label lblMapCoords;
        public Label lblMapParent;
        public Label lblMapOwner;
        public Label lblMapType;
        private ToolStripMenuItem settingsToolStripMenuItem;
        private GroupBox grpSettings;
        private Button btnOK;
        private CheckBox chkSites;
        private GroupBox grpSites;
        private ListView lstSiteTypes;
        private CheckBox chkOwnedSites;
        private CheckBox chkNeutralSites;
        private CheckBox chkConstructions;
        private CheckBox chkHistoricalFigures;
        private CheckBox chkUGRegions;
        private CheckBox chkRegions;
        private CheckBox chkBattles;
        private CheckBox chkCivilizations;
        private NumericUpDown ugRegionDepthPicker;
        private Label lblMapObject;
        private CheckBox chkShowLegend;
        private PictureBox picLegend;
        private CheckBox chkRivers;
        private CheckBox chkMountains;
        private CheckBox chkHFTravels;
        public ComboBox cmbHFTravels;
    }
}