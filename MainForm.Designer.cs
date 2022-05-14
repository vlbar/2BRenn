
namespace TwoBRenn
{
    partial class MainForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.autodromToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.effectsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.windowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.glControl = new OpenTK.GLControl();
            this.bottomContainer = new System.Windows.Forms.Panel();
            this.rightContainer = new System.Windows.Forms.Panel();
            this.structuresContainer = new System.Windows.Forms.Panel();
            this.structuresListView = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.structCountLabel = new System.Windows.Forms.Label();
            this.structCountTextLabel = new System.Windows.Forms.Label();
            this.structTextLabel = new System.Windows.Forms.Label();
            this.bottomPanel = new System.Windows.Forms.Panel();
            this.budgetTextLabel = new System.Windows.Forms.Label();
            this.budgetLabel = new System.Windows.Forms.Label();
            this.timeLeftLabel = new System.Windows.Forms.Label();
            this.testButton = new System.Windows.Forms.Button();
            this.mapContainer = new System.Windows.Forms.Panel();
            this.sidebarContainer = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.objectMaterialListView = new System.Windows.Forms.ListView();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.objectZScaleLabel = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.objectYScaleLabel = new System.Windows.Forms.Label();
            this.objectXScaleLabel = new System.Windows.Forms.Label();
            this.objectZScaleTrackBar = new System.Windows.Forms.TrackBar();
            this.objectXScaleTrackBar = new System.Windows.Forms.TrackBar();
            this.objectYScaleTrackBar = new System.Windows.Forms.TrackBar();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.objectZRotationLabel = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.objectYRotationLabel = new System.Windows.Forms.Label();
            this.objectXRotationLabel = new System.Windows.Forms.Label();
            this.objectZRotationTrackBar = new System.Windows.Forms.TrackBar();
            this.objectXRotationTrackBar = new System.Windows.Forms.TrackBar();
            this.objectYRotationTrackBar = new System.Windows.Forms.TrackBar();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.objectXPositionUpDown = new System.Windows.Forms.NumericUpDown();
            this.objectYPositionUpDown = new System.Windows.Forms.NumericUpDown();
            this.objectZPositionUpDown = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.selectedObjectNameLabel = new System.Windows.Forms.Label();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.debugInfoLabel = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.bottomContainer.SuspendLayout();
            this.rightContainer.SuspendLayout();
            this.structuresContainer.SuspendLayout();
            this.bottomPanel.SuspendLayout();
            this.sidebarContainer.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.objectZScaleTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.objectXScaleTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.objectYScaleTrackBar)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.objectZRotationTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.objectXRotationTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.objectYRotationTrackBar)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.objectXPositionUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.objectYPositionUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.objectZPositionUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.autodromToolStripMenuItem,
            this.effectsToolStripMenuItem,
            this.windowToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(907, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.fileToolStripMenuItem.Text = "Файл";
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(73, 20);
            this.editToolStripMenuItem.Text = "Изменить";
            // 
            // autodromToolStripMenuItem
            // 
            this.autodromToolStripMenuItem.Name = "autodromToolStripMenuItem";
            this.autodromToolStripMenuItem.Size = new System.Drawing.Size(74, 20);
            this.autodromToolStripMenuItem.Text = "Автодром";
            // 
            // effectsToolStripMenuItem
            // 
            this.effectsToolStripMenuItem.Name = "effectsToolStripMenuItem";
            this.effectsToolStripMenuItem.Size = new System.Drawing.Size(70, 20);
            this.effectsToolStripMenuItem.Text = "Эффекты";
            // 
            // windowToolStripMenuItem
            // 
            this.windowToolStripMenuItem.Name = "windowToolStripMenuItem";
            this.windowToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.windowToolStripMenuItem.Text = "Окно";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(94, 20);
            this.aboutToolStripMenuItem.Text = "О программе";
            // 
            // glControl
            // 
            this.glControl.BackColor = System.Drawing.Color.Black;
            this.glControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.glControl.Location = new System.Drawing.Point(0, 24);
            this.glControl.Name = "glControl";
            this.glControl.Size = new System.Drawing.Size(719, 477);
            this.glControl.TabIndex = 1;
            this.glControl.VSync = true;
            this.glControl.Load += new System.EventHandler(this.glControl_Load);
            this.glControl.KeyDown += new System.Windows.Forms.KeyEventHandler(this.glControl_KeyDown);
            // 
            // bottomContainer
            // 
            this.bottomContainer.BackColor = System.Drawing.Color.White;
            this.bottomContainer.Controls.Add(this.rightContainer);
            this.bottomContainer.Controls.Add(this.mapContainer);
            this.bottomContainer.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.bottomContainer.Location = new System.Drawing.Point(0, 501);
            this.bottomContainer.Name = "bottomContainer";
            this.bottomContainer.Size = new System.Drawing.Size(907, 110);
            this.bottomContainer.TabIndex = 2;
            // 
            // rightContainer
            // 
            this.rightContainer.BackColor = System.Drawing.SystemColors.Control;
            this.rightContainer.Controls.Add(this.structuresContainer);
            this.rightContainer.Controls.Add(this.bottomPanel);
            this.rightContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rightContainer.Location = new System.Drawing.Point(200, 0);
            this.rightContainer.Name = "rightContainer";
            this.rightContainer.Size = new System.Drawing.Size(707, 110);
            this.rightContainer.TabIndex = 7;
            // 
            // structuresContainer
            // 
            this.structuresContainer.Controls.Add(this.structuresListView);
            this.structuresContainer.Controls.Add(this.structCountLabel);
            this.structuresContainer.Controls.Add(this.structCountTextLabel);
            this.structuresContainer.Controls.Add(this.structTextLabel);
            this.structuresContainer.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.structuresContainer.Location = new System.Drawing.Point(0, 0);
            this.structuresContainer.Name = "structuresContainer";
            this.structuresContainer.Size = new System.Drawing.Size(707, 80);
            this.structuresContainer.TabIndex = 3;
            // 
            // structuresListView
            // 
            this.structuresListView.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.structuresListView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.structuresListView.BackColor = System.Drawing.SystemColors.Control;
            this.structuresListView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.structuresListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.structuresListView.HideSelection = false;
            this.structuresListView.Location = new System.Drawing.Point(174, 6);
            this.structuresListView.Margin = new System.Windows.Forms.Padding(0);
            this.structuresListView.MultiSelect = false;
            this.structuresListView.Name = "structuresListView";
            this.structuresListView.Size = new System.Drawing.Size(533, 74);
            this.structuresListView.TabIndex = 4;
            this.structuresListView.UseCompatibleStateImageBehavior = false;
            this.structuresListView.ItemActivate += new System.EventHandler(this.structuresListView_ItemActivate);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Width = 50;
            // 
            // structCountLabel
            // 
            this.structCountLabel.AutoSize = true;
            this.structCountLabel.Location = new System.Drawing.Point(132, 34);
            this.structCountLabel.Name = "structCountLabel";
            this.structCountLabel.Size = new System.Drawing.Size(36, 13);
            this.structCountLabel.TabIndex = 2;
            this.structCountLabel.Text = "0/128";
            this.structCountLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // structCountTextLabel
            // 
            this.structCountTextLabel.AutoSize = true;
            this.structCountTextLabel.Location = new System.Drawing.Point(12, 34);
            this.structCountTextLabel.Name = "structCountTextLabel";
            this.structCountTextLabel.Size = new System.Drawing.Size(77, 13);
            this.structCountTextLabel.TabIndex = 1;
            this.structCountTextLabel.Text = "Установлено:";
            // 
            // structTextLabel
            // 
            this.structTextLabel.AutoSize = true;
            this.structTextLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.structTextLabel.Location = new System.Drawing.Point(12, 10);
            this.structTextLabel.Name = "structTextLabel";
            this.structTextLabel.Size = new System.Drawing.Size(156, 13);
            this.structTextLabel.TabIndex = 0;
            this.structTextLabel.Text = "Структуры безопасности";
            // 
            // bottomPanel
            // 
            this.bottomPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.bottomPanel.Controls.Add(this.budgetTextLabel);
            this.bottomPanel.Controls.Add(this.budgetLabel);
            this.bottomPanel.Controls.Add(this.timeLeftLabel);
            this.bottomPanel.Controls.Add(this.testButton);
            this.bottomPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.bottomPanel.Location = new System.Drawing.Point(0, 80);
            this.bottomPanel.Name = "bottomPanel";
            this.bottomPanel.Size = new System.Drawing.Size(707, 30);
            this.bottomPanel.TabIndex = 0;
            // 
            // budgetTextLabel
            // 
            this.budgetTextLabel.AutoSize = true;
            this.budgetTextLabel.Location = new System.Drawing.Point(12, 8);
            this.budgetTextLabel.Name = "budgetTextLabel";
            this.budgetTextLabel.Size = new System.Drawing.Size(50, 13);
            this.budgetTextLabel.TabIndex = 0;
            this.budgetTextLabel.Text = "Бюджет:";
            // 
            // budgetLabel
            // 
            this.budgetLabel.AutoSize = true;
            this.budgetLabel.Location = new System.Drawing.Point(68, 8);
            this.budgetLabel.Name = "budgetLabel";
            this.budgetLabel.Size = new System.Drawing.Size(49, 13);
            this.budgetLabel.TabIndex = 1;
            this.budgetLabel.Text = "10 000 ₽";
            // 
            // timeLeftLabel
            // 
            this.timeLeftLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.timeLeftLabel.AutoSize = true;
            this.timeLeftLabel.Location = new System.Drawing.Point(588, 9);
            this.timeLeftLabel.Name = "timeLeftLabel";
            this.timeLeftLabel.Size = new System.Drawing.Size(34, 13);
            this.timeLeftLabel.TabIndex = 4;
            this.timeLeftLabel.Text = "00:00";
            this.timeLeftLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // testButton
            // 
            this.testButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.testButton.Location = new System.Drawing.Point(628, 4);
            this.testButton.Name = "testButton";
            this.testButton.Size = new System.Drawing.Size(75, 23);
            this.testButton.TabIndex = 3;
            this.testButton.Text = "Тест";
            this.testButton.UseVisualStyleBackColor = true;
            // 
            // mapContainer
            // 
            this.mapContainer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.mapContainer.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("mapContainer.BackgroundImage")));
            this.mapContainer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.mapContainer.Dock = System.Windows.Forms.DockStyle.Left;
            this.mapContainer.Location = new System.Drawing.Point(0, 0);
            this.mapContainer.Name = "mapContainer";
            this.mapContainer.Size = new System.Drawing.Size(200, 110);
            this.mapContainer.TabIndex = 6;
            // 
            // sidebarContainer
            // 
            this.sidebarContainer.Controls.Add(this.label1);
            this.sidebarContainer.Controls.Add(this.groupBox4);
            this.sidebarContainer.Controls.Add(this.groupBox3);
            this.sidebarContainer.Controls.Add(this.groupBox2);
            this.sidebarContainer.Controls.Add(this.groupBox1);
            this.sidebarContainer.Controls.Add(this.selectedObjectNameLabel);
            this.sidebarContainer.Dock = System.Windows.Forms.DockStyle.Right;
            this.sidebarContainer.Location = new System.Drawing.Point(723, 24);
            this.sidebarContainer.Name = "sidebarContainer";
            this.sidebarContainer.Size = new System.Drawing.Size(184, 477);
            this.sidebarContainer.TabIndex = 4;
            this.sidebarContainer.Visible = false;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 461);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(157, 13);
            this.label1.TabIndex = 23;
            this.label1.Text = "Нажмите Esc, чтобы закрыть";
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this.objectMaterialListView);
            this.groupBox4.Location = new System.Drawing.Point(3, 306);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(177, 152);
            this.groupBox4.TabIndex = 22;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Материал";
            // 
            // objectMaterialListView
            // 
            this.objectMaterialListView.BackColor = System.Drawing.SystemColors.Control;
            this.objectMaterialListView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.objectMaterialListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.objectMaterialListView.GridLines = true;
            this.objectMaterialListView.HideSelection = false;
            this.objectMaterialListView.Location = new System.Drawing.Point(3, 16);
            this.objectMaterialListView.Name = "objectMaterialListView";
            this.objectMaterialListView.Size = new System.Drawing.Size(171, 133);
            this.objectMaterialListView.TabIndex = 0;
            this.objectMaterialListView.TileSize = new System.Drawing.Size(20, 20);
            this.objectMaterialListView.UseCompatibleStateImageBehavior = false;
            this.objectMaterialListView.View = System.Windows.Forms.View.SmallIcon;
            this.objectMaterialListView.ItemActivate += new System.EventHandler(this.objectMaterialListView_ItemActivate);
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.label12);
            this.groupBox3.Controls.Add(this.objectZScaleLabel);
            this.groupBox3.Controls.Add(this.label14);
            this.groupBox3.Controls.Add(this.objectYScaleLabel);
            this.groupBox3.Controls.Add(this.objectXScaleLabel);
            this.groupBox3.Controls.Add(this.objectZScaleTrackBar);
            this.groupBox3.Controls.Add(this.objectXScaleTrackBar);
            this.groupBox3.Controls.Add(this.objectYScaleTrackBar);
            this.groupBox3.Location = new System.Drawing.Point(3, 213);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(178, 87);
            this.groupBox3.TabIndex = 21;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Размер";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 21);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(14, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "X";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(3, 44);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(14, 13);
            this.label12.TabIndex = 10;
            this.label12.Text = "Y";
            // 
            // objectZScaleLabel
            // 
            this.objectZScaleLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.objectZScaleLabel.AutoSize = true;
            this.objectZScaleLabel.Location = new System.Drawing.Point(152, 67);
            this.objectZScaleLabel.Name = "objectZScaleLabel";
            this.objectZScaleLabel.Size = new System.Drawing.Size(25, 13);
            this.objectZScaleLabel.TabIndex = 18;
            this.objectZScaleLabel.Text = "999";
            this.objectZScaleLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(3, 67);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(14, 13);
            this.label14.TabIndex = 11;
            this.label14.Text = "Z";
            // 
            // objectYScaleLabel
            // 
            this.objectYScaleLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.objectYScaleLabel.AutoSize = true;
            this.objectYScaleLabel.Location = new System.Drawing.Point(152, 44);
            this.objectYScaleLabel.Name = "objectYScaleLabel";
            this.objectYScaleLabel.Size = new System.Drawing.Size(25, 13);
            this.objectYScaleLabel.TabIndex = 17;
            this.objectYScaleLabel.Text = "999";
            this.objectYScaleLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // objectXScaleLabel
            // 
            this.objectXScaleLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.objectXScaleLabel.AutoSize = true;
            this.objectXScaleLabel.Location = new System.Drawing.Point(152, 20);
            this.objectXScaleLabel.Name = "objectXScaleLabel";
            this.objectXScaleLabel.Size = new System.Drawing.Size(25, 13);
            this.objectXScaleLabel.TabIndex = 13;
            this.objectXScaleLabel.Text = "999";
            this.objectXScaleLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // objectZScaleTrackBar
            // 
            this.objectZScaleTrackBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.objectZScaleTrackBar.AutoSize = false;
            this.objectZScaleTrackBar.Location = new System.Drawing.Point(17, 63);
            this.objectZScaleTrackBar.Maximum = 50;
            this.objectZScaleTrackBar.Minimum = 5;
            this.objectZScaleTrackBar.Name = "objectZScaleTrackBar";
            this.objectZScaleTrackBar.Size = new System.Drawing.Size(138, 20);
            this.objectZScaleTrackBar.TabIndex = 16;
            this.objectZScaleTrackBar.TickFrequency = 30;
            this.objectZScaleTrackBar.TickStyle = System.Windows.Forms.TickStyle.None;
            this.objectZScaleTrackBar.Value = 5;
            this.objectZScaleTrackBar.Scroll += new System.EventHandler(this.objectScaleTrackBar_Scroll);
            // 
            // objectXScaleTrackBar
            // 
            this.objectXScaleTrackBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.objectXScaleTrackBar.AutoSize = false;
            this.objectXScaleTrackBar.Location = new System.Drawing.Point(17, 19);
            this.objectXScaleTrackBar.Maximum = 50;
            this.objectXScaleTrackBar.Minimum = 5;
            this.objectXScaleTrackBar.Name = "objectXScaleTrackBar";
            this.objectXScaleTrackBar.Size = new System.Drawing.Size(138, 20);
            this.objectXScaleTrackBar.TabIndex = 14;
            this.objectXScaleTrackBar.TickFrequency = 30;
            this.objectXScaleTrackBar.TickStyle = System.Windows.Forms.TickStyle.None;
            this.objectXScaleTrackBar.Value = 5;
            this.objectXScaleTrackBar.Scroll += new System.EventHandler(this.objectScaleTrackBar_Scroll);
            // 
            // objectYScaleTrackBar
            // 
            this.objectYScaleTrackBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.objectYScaleTrackBar.AutoSize = false;
            this.objectYScaleTrackBar.Location = new System.Drawing.Point(17, 40);
            this.objectYScaleTrackBar.Maximum = 50;
            this.objectYScaleTrackBar.Minimum = 5;
            this.objectYScaleTrackBar.Name = "objectYScaleTrackBar";
            this.objectYScaleTrackBar.Size = new System.Drawing.Size(138, 20);
            this.objectYScaleTrackBar.TabIndex = 15;
            this.objectYScaleTrackBar.TickFrequency = 30;
            this.objectYScaleTrackBar.TickStyle = System.Windows.Forms.TickStyle.None;
            this.objectYScaleTrackBar.Value = 5;
            this.objectYScaleTrackBar.Scroll += new System.EventHandler(this.objectScaleTrackBar_Scroll);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.objectZRotationLabel);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.objectYRotationLabel);
            this.groupBox2.Controls.Add(this.objectXRotationLabel);
            this.groupBox2.Controls.Add(this.objectZRotationTrackBar);
            this.groupBox2.Controls.Add(this.objectXRotationTrackBar);
            this.groupBox2.Controls.Add(this.objectYRotationTrackBar);
            this.groupBox2.Location = new System.Drawing.Point(3, 120);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(178, 87);
            this.groupBox2.TabIndex = 20;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Поворот";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(3, 21);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(14, 13);
            this.label8.TabIndex = 9;
            this.label8.Text = "X";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(3, 44);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(14, 13);
            this.label7.TabIndex = 10;
            this.label7.Text = "Y";
            // 
            // objectZRotationLabel
            // 
            this.objectZRotationLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.objectZRotationLabel.AutoSize = true;
            this.objectZRotationLabel.Location = new System.Drawing.Point(152, 67);
            this.objectZRotationLabel.Name = "objectZRotationLabel";
            this.objectZRotationLabel.Size = new System.Drawing.Size(25, 13);
            this.objectZRotationLabel.TabIndex = 18;
            this.objectZRotationLabel.Text = "999";
            this.objectZRotationLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 67);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(14, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "Z";
            // 
            // objectYRotationLabel
            // 
            this.objectYRotationLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.objectYRotationLabel.AutoSize = true;
            this.objectYRotationLabel.Location = new System.Drawing.Point(152, 44);
            this.objectYRotationLabel.Name = "objectYRotationLabel";
            this.objectYRotationLabel.Size = new System.Drawing.Size(25, 13);
            this.objectYRotationLabel.TabIndex = 17;
            this.objectYRotationLabel.Text = "999";
            this.objectYRotationLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // objectXRotationLabel
            // 
            this.objectXRotationLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.objectXRotationLabel.AutoSize = true;
            this.objectXRotationLabel.Location = new System.Drawing.Point(152, 20);
            this.objectXRotationLabel.Name = "objectXRotationLabel";
            this.objectXRotationLabel.Size = new System.Drawing.Size(25, 13);
            this.objectXRotationLabel.TabIndex = 13;
            this.objectXRotationLabel.Text = "999";
            this.objectXRotationLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // objectZRotationTrackBar
            // 
            this.objectZRotationTrackBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.objectZRotationTrackBar.AutoSize = false;
            this.objectZRotationTrackBar.Location = new System.Drawing.Point(17, 63);
            this.objectZRotationTrackBar.Maximum = 360;
            this.objectZRotationTrackBar.Minimum = -360;
            this.objectZRotationTrackBar.Name = "objectZRotationTrackBar";
            this.objectZRotationTrackBar.Size = new System.Drawing.Size(138, 20);
            this.objectZRotationTrackBar.TabIndex = 16;
            this.objectZRotationTrackBar.TickFrequency = 30;
            this.objectZRotationTrackBar.TickStyle = System.Windows.Forms.TickStyle.None;
            this.objectZRotationTrackBar.Scroll += new System.EventHandler(this.objectRotationTrackBar_ValueChanged);
            // 
            // objectXRotationTrackBar
            // 
            this.objectXRotationTrackBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.objectXRotationTrackBar.AutoSize = false;
            this.objectXRotationTrackBar.Location = new System.Drawing.Point(17, 19);
            this.objectXRotationTrackBar.Maximum = 360;
            this.objectXRotationTrackBar.Minimum = -360;
            this.objectXRotationTrackBar.Name = "objectXRotationTrackBar";
            this.objectXRotationTrackBar.Size = new System.Drawing.Size(138, 20);
            this.objectXRotationTrackBar.TabIndex = 14;
            this.objectXRotationTrackBar.TickFrequency = 30;
            this.objectXRotationTrackBar.TickStyle = System.Windows.Forms.TickStyle.None;
            this.objectXRotationTrackBar.Scroll += new System.EventHandler(this.objectRotationTrackBar_ValueChanged);
            // 
            // objectYRotationTrackBar
            // 
            this.objectYRotationTrackBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.objectYRotationTrackBar.AutoSize = false;
            this.objectYRotationTrackBar.Location = new System.Drawing.Point(17, 40);
            this.objectYRotationTrackBar.Maximum = 360;
            this.objectYRotationTrackBar.Minimum = -360;
            this.objectYRotationTrackBar.Name = "objectYRotationTrackBar";
            this.objectYRotationTrackBar.Size = new System.Drawing.Size(138, 20);
            this.objectYRotationTrackBar.TabIndex = 15;
            this.objectYRotationTrackBar.TickFrequency = 30;
            this.objectYRotationTrackBar.TickStyle = System.Windows.Forms.TickStyle.None;
            this.objectYRotationTrackBar.Scroll += new System.EventHandler(this.objectRotationTrackBar_ValueChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.objectXPositionUpDown);
            this.groupBox1.Controls.Add(this.objectYPositionUpDown);
            this.groupBox1.Controls.Add(this.objectZPositionUpDown);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Location = new System.Drawing.Point(3, 27);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(178, 87);
            this.groupBox1.TabIndex = 19;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Положение";
            // 
            // objectXPositionUpDown
            // 
            this.objectXPositionUpDown.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.objectXPositionUpDown.DecimalPlaces = 5;
            this.objectXPositionUpDown.Increment = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.objectXPositionUpDown.Location = new System.Drawing.Point(23, 19);
            this.objectXPositionUpDown.Maximum = new decimal(new int[] {
            300,
            0,
            0,
            0});
            this.objectXPositionUpDown.Minimum = new decimal(new int[] {
            300,
            0,
            0,
            -2147483648});
            this.objectXPositionUpDown.Name = "objectXPositionUpDown";
            this.objectXPositionUpDown.Size = new System.Drawing.Size(149, 20);
            this.objectXPositionUpDown.TabIndex = 1;
            this.objectXPositionUpDown.Tag = "X";
            this.objectXPositionUpDown.ValueChanged += new System.EventHandler(this.objectPositionUpDown_ValueChanged);
            // 
            // objectYPositionUpDown
            // 
            this.objectYPositionUpDown.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.objectYPositionUpDown.DecimalPlaces = 5;
            this.objectYPositionUpDown.Increment = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.objectYPositionUpDown.Location = new System.Drawing.Point(23, 40);
            this.objectYPositionUpDown.Maximum = new decimal(new int[] {
            300,
            0,
            0,
            0});
            this.objectYPositionUpDown.Minimum = new decimal(new int[] {
            300,
            0,
            0,
            -2147483648});
            this.objectYPositionUpDown.Name = "objectYPositionUpDown";
            this.objectYPositionUpDown.Size = new System.Drawing.Size(149, 20);
            this.objectYPositionUpDown.TabIndex = 3;
            this.objectYPositionUpDown.Tag = "Y";
            this.objectYPositionUpDown.ValueChanged += new System.EventHandler(this.objectPositionUpDown_ValueChanged);
            // 
            // objectZPositionUpDown
            // 
            this.objectZPositionUpDown.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.objectZPositionUpDown.DecimalPlaces = 5;
            this.objectZPositionUpDown.Increment = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.objectZPositionUpDown.Location = new System.Drawing.Point(23, 61);
            this.objectZPositionUpDown.Maximum = new decimal(new int[] {
            300,
            0,
            0,
            0});
            this.objectZPositionUpDown.Minimum = new decimal(new int[] {
            300,
            0,
            0,
            -2147483648});
            this.objectZPositionUpDown.Name = "objectZPositionUpDown";
            this.objectZPositionUpDown.Size = new System.Drawing.Size(149, 20);
            this.objectZPositionUpDown.TabIndex = 4;
            this.objectZPositionUpDown.Tag = "Z";
            this.objectZPositionUpDown.ValueChanged += new System.EventHandler(this.objectPositionUpDown_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(14, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "X";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 42);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(14, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Y";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 63);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(14, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Z";
            // 
            // selectedObjectNameLabel
            // 
            this.selectedObjectNameLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.selectedObjectNameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.selectedObjectNameLabel.Location = new System.Drawing.Point(0, 0);
            this.selectedObjectNameLabel.Name = "selectedObjectNameLabel";
            this.selectedObjectNameLabel.Size = new System.Drawing.Size(184, 24);
            this.selectedObjectNameLabel.TabIndex = 0;
            this.selectedObjectNameLabel.Text = "{SelectedObjectNameLabel}";
            this.selectedObjectNameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // splitter1
            // 
            this.splitter1.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Right;
            this.splitter1.Location = new System.Drawing.Point(719, 24);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(4, 477);
            this.splitter1.TabIndex = 5;
            this.splitter1.TabStop = false;
            // 
            // debugInfoLabel
            // 
            this.debugInfoLabel.AutoSize = true;
            this.debugInfoLabel.Location = new System.Drawing.Point(4, 27);
            this.debugInfoLabel.Name = "debugInfoLabel";
            this.debugInfoLabel.Size = new System.Drawing.Size(36, 13);
            this.debugInfoLabel.TabIndex = 6;
            this.debugInfoLabel.Text = "0 FPS";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(907, 611);
            this.Controls.Add(this.debugInfoLabel);
            this.Controls.Add(this.glControl);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.sidebarContainer);
            this.Controls.Add(this.bottomContainer);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "MainForm";
            this.Text = "2BRenn";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.bottomContainer.ResumeLayout(false);
            this.rightContainer.ResumeLayout(false);
            this.structuresContainer.ResumeLayout(false);
            this.structuresContainer.PerformLayout();
            this.bottomPanel.ResumeLayout(false);
            this.bottomPanel.PerformLayout();
            this.sidebarContainer.ResumeLayout(false);
            this.sidebarContainer.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.objectZScaleTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.objectXScaleTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.objectYScaleTrackBar)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.objectZRotationTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.objectXRotationTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.objectYRotationTrackBar)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.objectXPositionUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.objectYPositionUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.objectZPositionUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem autodromToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem effectsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem windowToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private OpenTK.GLControl glControl;
        private System.Windows.Forms.Panel bottomContainer;
        private System.Windows.Forms.Panel structuresContainer;
        private System.Windows.Forms.Label budgetTextLabel;
        private System.Windows.Forms.Label budgetLabel;
        private System.Windows.Forms.Label timeLeftLabel;
        private System.Windows.Forms.Button testButton;
        private System.Windows.Forms.Label structCountLabel;
        private System.Windows.Forms.Label structCountTextLabel;
        private System.Windows.Forms.Label structTextLabel;
        private System.Windows.Forms.Panel sidebarContainer;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.Panel mapContainer;
        private System.Windows.Forms.Panel rightContainer;
        private System.Windows.Forms.Panel bottomPanel;
        private System.Windows.Forms.Label debugInfoLabel;
        private System.Windows.Forms.ListView structuresListView;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.NumericUpDown objectXPositionUpDown;
        private System.Windows.Forms.Label selectedObjectNameLabel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown objectZPositionUpDown;
        private System.Windows.Forms.NumericUpDown objectYPositionUpDown;
        private System.Windows.Forms.TrackBar objectXRotationTrackBar;
        private System.Windows.Forms.Label objectXRotationLabel;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label objectZRotationLabel;
        private System.Windows.Forms.Label objectYRotationLabel;
        private System.Windows.Forms.TrackBar objectZRotationTrackBar;
        private System.Windows.Forms.TrackBar objectYRotationTrackBar;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.ListView objectMaterialListView;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label objectZScaleLabel;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label objectYScaleLabel;
        private System.Windows.Forms.Label objectXScaleLabel;
        private System.Windows.Forms.TrackBar objectZScaleTrackBar;
        private System.Windows.Forms.TrackBar objectXScaleTrackBar;
        private System.Windows.Forms.TrackBar objectYScaleTrackBar;
        private System.Windows.Forms.Label label1;
    }
}

