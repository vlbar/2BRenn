
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
            this.structListView = new System.Windows.Forms.ListView();
            this.structCountLabel = new System.Windows.Forms.Label();
            this.structCountTextLabel = new System.Windows.Forms.Label();
            this.structTextLabel = new System.Windows.Forms.Label();
            this.bottomPanel = new System.Windows.Forms.Panel();
            this.budgetTextLabel = new System.Windows.Forms.Label();
            this.budgetLabel = new System.Windows.Forms.Label();
            this.timeLeftLabel = new System.Windows.Forms.Label();
            this.testButton = new System.Windows.Forms.Button();
            this.controlButton = new System.Windows.Forms.Button();
            this.mapContainer = new System.Windows.Forms.Panel();
            this.sidebarContainer = new System.Windows.Forms.Panel();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.debugInfoLabel = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.bottomContainer.SuspendLayout();
            this.rightContainer.SuspendLayout();
            this.structuresContainer.SuspendLayout();
            this.bottomPanel.SuspendLayout();
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
            this.structuresContainer.Controls.Add(this.structListView);
            this.structuresContainer.Controls.Add(this.structCountLabel);
            this.structuresContainer.Controls.Add(this.structCountTextLabel);
            this.structuresContainer.Controls.Add(this.structTextLabel);
            this.structuresContainer.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.structuresContainer.Location = new System.Drawing.Point(0, 0);
            this.structuresContainer.Name = "structuresContainer";
            this.structuresContainer.Size = new System.Drawing.Size(707, 80);
            this.structuresContainer.TabIndex = 3;
            // 
            // structListView
            // 
            this.structListView.HideSelection = false;
            this.structListView.Location = new System.Drawing.Point(180, 0);
            this.structListView.Name = "structListView";
            this.structListView.Size = new System.Drawing.Size(527, 80);
            this.structListView.TabIndex = 0;
            this.structListView.UseCompatibleStateImageBehavior = false;
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
            this.bottomPanel.Controls.Add(this.controlButton);
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
            this.timeLeftLabel.Location = new System.Drawing.Point(474, 8);
            this.timeLeftLabel.Name = "timeLeftLabel";
            this.timeLeftLabel.Size = new System.Drawing.Size(34, 13);
            this.timeLeftLabel.TabIndex = 4;
            this.timeLeftLabel.Text = "00:00";
            this.timeLeftLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // testButton
            // 
            this.testButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.testButton.Location = new System.Drawing.Point(519, 3);
            this.testButton.Name = "testButton";
            this.testButton.Size = new System.Drawing.Size(75, 23);
            this.testButton.TabIndex = 3;
            this.testButton.Text = "Тест";
            this.testButton.UseVisualStyleBackColor = true;
            // 
            // controlButton
            // 
            this.controlButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.controlButton.Location = new System.Drawing.Point(595, 3);
            this.controlButton.Name = "controlButton";
            this.controlButton.Size = new System.Drawing.Size(100, 23);
            this.controlButton.TabIndex = 2;
            this.controlButton.Text = "Контроль";
            this.controlButton.UseVisualStyleBackColor = true;
            // 
            // mapContainer
            // 
            this.mapContainer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.mapContainer.Dock = System.Windows.Forms.DockStyle.Left;
            this.mapContainer.Location = new System.Drawing.Point(0, 0);
            this.mapContainer.Name = "mapContainer";
            this.mapContainer.Size = new System.Drawing.Size(200, 110);
            this.mapContainer.TabIndex = 6;
            // 
            // sidebarContainer
            // 
            this.sidebarContainer.Dock = System.Windows.Forms.DockStyle.Right;
            this.sidebarContainer.Location = new System.Drawing.Point(723, 24);
            this.sidebarContainer.Name = "sidebarContainer";
            this.sidebarContainer.Size = new System.Drawing.Size(184, 477);
            this.sidebarContainer.TabIndex = 4;
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
            this.debugInfoLabel.Location = new System.Drawing.Point(12, 35);
            this.debugInfoLabel.Name = "debugInfoLabel";
            this.debugInfoLabel.Size = new System.Drawing.Size(35, 13);
            this.debugInfoLabel.TabIndex = 6;
            this.debugInfoLabel.Text = "label1";
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
        private System.Windows.Forms.Button controlButton;
        private System.Windows.Forms.Label structCountLabel;
        private System.Windows.Forms.Label structCountTextLabel;
        private System.Windows.Forms.Label structTextLabel;
        private System.Windows.Forms.Panel sidebarContainer;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.Panel mapContainer;
        private System.Windows.Forms.Panel rightContainer;
        private System.Windows.Forms.Panel bottomPanel;
        private System.Windows.Forms.ListView structListView;
        private System.Windows.Forms.Label debugInfoLabel;
    }
}

