namespace BZModManager
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            tableLayoutPanel1 = new TableLayoutPanel();
            tableLayoutPanel2 = new TableLayoutPanel();
            toggleONOFF = new Button();
            deleteCancel = new Button();
            Download = new Button();
            tableLayoutPanel3 = new TableLayoutPanel();
            menuStrip1 = new MenuStrip();
            fileMenu = new ToolStripMenuItem();
            settingsToolStripMenuItem = new ToolStripMenuItem();
            setManagerPathToolStripMenuItem = new ToolStripMenuItem();
            setDCSSavedPathToolStripMenuItem = new ToolStripMenuItem();
            setDCSMainPathToolStripMenuItem = new ToolStripMenuItem();
            purgeInstallToolStripMenuItem = new ToolStripMenuItem();
            adminToolStripMenuItem = new ToolStripMenuItem();
            createRepoFilesToolStripMenuItem = new ToolStripMenuItem();
            pushToServerToolStripMenuItem = new ToolStripMenuItem();
            aboutToolStripMenuItem = new ToolStripMenuItem();
            resync = new Button();
            tableLayoutPanel4 = new TableLayoutPanel();
            modList = new ListView();
            ModDisplay = new RichTextBox();
            progressBar = new ProgressBar();
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            tableLayoutPanel3.SuspendLayout();
            menuStrip1.SuspendLayout();
            tableLayoutPanel4.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(tableLayoutPanel2, 0, 2);
            tableLayoutPanel1.Controls.Add(tableLayoutPanel3, 0, 0);
            tableLayoutPanel1.Controls.Add(tableLayoutPanel4, 0, 1);
            tableLayoutPanel1.Controls.Add(progressBar, 0, 3);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 4;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 8.333333F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 75F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 8.333333F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 8.333333F));
            tableLayoutPanel1.Size = new Size(637, 440);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 3;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel2.Controls.Add(toggleONOFF, 2, 0);
            tableLayoutPanel2.Controls.Add(deleteCancel, 1, 0);
            tableLayoutPanel2.Controls.Add(Download, 0, 0);
            tableLayoutPanel2.Location = new Point(3, 368);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 1;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.Size = new Size(631, 30);
            tableLayoutPanel2.TabIndex = 0;
            // 
            // toggleONOFF
            // 
            toggleONOFF.Location = new Point(423, 3);
            toggleONOFF.Name = "toggleONOFF";
            toggleONOFF.Size = new Size(205, 22);
            toggleONOFF.TabIndex = 2;
            toggleONOFF.Text = "Enable";
            toggleONOFF.UseVisualStyleBackColor = true;
            toggleONOFF.Click += toggleONOFF_Click;
            // 
            // deleteCancel
            // 
            deleteCancel.Location = new Point(213, 3);
            deleteCancel.Name = "deleteCancel";
            deleteCancel.Size = new Size(204, 22);
            deleteCancel.TabIndex = 1;
            deleteCancel.Text = "Delete";
            deleteCancel.UseVisualStyleBackColor = true;
            deleteCancel.Click += deleteCancel_Click;
            // 
            // Download
            // 
            Download.Location = new Point(3, 3);
            Download.Name = "Download";
            Download.Size = new Size(204, 23);
            Download.TabIndex = 0;
            Download.Text = "Download";
            Download.UseVisualStyleBackColor = true;
            Download.Click += Download_Click;
            // 
            // tableLayoutPanel3
            // 
            tableLayoutPanel3.ColumnCount = 4;
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanel3.Controls.Add(menuStrip1, 0, 0);
            tableLayoutPanel3.Controls.Add(resync, 3, 0);
            tableLayoutPanel3.Location = new Point(3, 3);
            tableLayoutPanel3.Name = "tableLayoutPanel3";
            tableLayoutPanel3.RowCount = 1;
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel3.Size = new Size(631, 30);
            tableLayoutPanel3.TabIndex = 2;
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { fileMenu });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(157, 24);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // fileMenu
            // 
            fileMenu.DropDownItems.AddRange(new ToolStripItem[] { settingsToolStripMenuItem, purgeInstallToolStripMenuItem, adminToolStripMenuItem, aboutToolStripMenuItem });
            fileMenu.Name = "fileMenu";
            fileMenu.Size = new Size(37, 20);
            fileMenu.Text = "File";
            // 
            // settingsToolStripMenuItem
            // 
            settingsToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { setManagerPathToolStripMenuItem, setDCSSavedPathToolStripMenuItem, setDCSMainPathToolStripMenuItem });
            settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            settingsToolStripMenuItem.Size = new Size(180, 22);
            settingsToolStripMenuItem.Text = "Settings";
            // 
            // setManagerPathToolStripMenuItem
            // 
            setManagerPathToolStripMenuItem.Name = "setManagerPathToolStripMenuItem";
            setManagerPathToolStripMenuItem.Size = new Size(188, 22);
            setManagerPathToolStripMenuItem.Text = "Set Mod Storage Path";
            setManagerPathToolStripMenuItem.Click += setManagerPathToolStripMenuItem_Click;
            // 
            // setDCSSavedPathToolStripMenuItem
            // 
            setDCSSavedPathToolStripMenuItem.Name = "setDCSSavedPathToolStripMenuItem";
            setDCSSavedPathToolStripMenuItem.Size = new Size(188, 22);
            setDCSSavedPathToolStripMenuItem.Text = "Set DCS Saved Path";
            setDCSSavedPathToolStripMenuItem.Click += setDCSSavedPathToolStripMenuItem_Click;
            // 
            // setDCSMainPathToolStripMenuItem
            // 
            setDCSMainPathToolStripMenuItem.Enabled = false;
            setDCSMainPathToolStripMenuItem.Name = "setDCSMainPathToolStripMenuItem";
            setDCSMainPathToolStripMenuItem.Size = new Size(188, 22);
            setDCSMainPathToolStripMenuItem.Text = "Set DCS Main Path";
            setDCSMainPathToolStripMenuItem.Click += setDCSMainPathToolStripMenuItem_Click;
            // 
            // purgeInstallToolStripMenuItem
            // 
            purgeInstallToolStripMenuItem.Enabled = false;
            purgeInstallToolStripMenuItem.Name = "purgeInstallToolStripMenuItem";
            purgeInstallToolStripMenuItem.Size = new Size(180, 22);
            purgeInstallToolStripMenuItem.Text = "Purge Install";
            // 
            // adminToolStripMenuItem
            // 
            adminToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { createRepoFilesToolStripMenuItem, pushToServerToolStripMenuItem });
            adminToolStripMenuItem.Enabled = false;
            adminToolStripMenuItem.Name = "adminToolStripMenuItem";
            adminToolStripMenuItem.Size = new Size(180, 22);
            adminToolStripMenuItem.Text = "Admin";
            // 
            // createRepoFilesToolStripMenuItem
            // 
            createRepoFilesToolStripMenuItem.Name = "createRepoFilesToolStripMenuItem";
            createRepoFilesToolStripMenuItem.Size = new Size(164, 22);
            createRepoFilesToolStripMenuItem.Text = "Create Repo Files";
            createRepoFilesToolStripMenuItem.Click += createRepoFilesToolStripMenuItem_ClickAsync;
            // 
            // pushToServerToolStripMenuItem
            // 
            pushToServerToolStripMenuItem.Name = "pushToServerToolStripMenuItem";
            pushToServerToolStripMenuItem.Size = new Size(164, 22);
            pushToServerToolStripMenuItem.Text = "Push to Server";
            pushToServerToolStripMenuItem.Click += pushToServerToolStripMenuItem_Click;
            // 
            // aboutToolStripMenuItem
            // 
            aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            aboutToolStripMenuItem.Size = new Size(180, 22);
            aboutToolStripMenuItem.Text = "About";
            aboutToolStripMenuItem.Click += aboutToolStripMenuItem_Click;
            // 
            // resync
            // 
            resync.Location = new Point(474, 3);
            resync.Name = "resync";
            resync.Size = new Size(154, 23);
            resync.TabIndex = 1;
            resync.Text = "RESYNC";
            resync.UseVisualStyleBackColor = true;
            resync.Click += resync_Click;
            // 
            // tableLayoutPanel4
            // 
            tableLayoutPanel4.ColumnCount = 2;
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60F));
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
            tableLayoutPanel4.Controls.Add(modList, 0, 0);
            tableLayoutPanel4.Controls.Add(ModDisplay, 1, 0);
            tableLayoutPanel4.Location = new Point(3, 39);
            tableLayoutPanel4.Name = "tableLayoutPanel4";
            tableLayoutPanel4.RowCount = 1;
            tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel4.Size = new Size(631, 321);
            tableLayoutPanel4.TabIndex = 3;
            // 
            // modList
            // 
            modList.Location = new Point(3, 3);
            modList.Name = "modList";
            modList.Size = new Size(372, 315);
            modList.TabIndex = 2;
            modList.UseCompatibleStateImageBehavior = false;
            modList.SelectedIndexChanged += modlist_SelectedIndexChanged;
            // 
            // ModDisplay
            // 
            ModDisplay.Location = new Point(381, 3);
            ModDisplay.Name = "ModDisplay";
            ModDisplay.Size = new Size(247, 315);
            ModDisplay.TabIndex = 1;
            ModDisplay.Text = "";
            // 
            // progressBar
            // 
            progressBar.Location = new Point(3, 404);
            progressBar.Name = "progressBar";
            progressBar.Size = new Size(631, 32);
            progressBar.TabIndex = 4;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(637, 440);
            Controls.Add(tableLayoutPanel1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MainMenuStrip = menuStrip1;
            MaximizeBox = false;
            MaximumSize = new Size(653, 479);
            Name = "Form1";
            Text = "Border Zone Mod Manager";
            Load += Form1_Load;
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel3.ResumeLayout(false);
            tableLayoutPanel3.PerformLayout();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            tableLayoutPanel4.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private TableLayoutPanel tableLayoutPanel2;
        private Button toggleONOFF;
        private Button deleteCancel;
        private Button Download;
        private TableLayoutPanel tableLayoutPanel3;
        private TableLayoutPanel tableLayoutPanel4;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileMenu;
        private ToolStripMenuItem settingsToolStripMenuItem;
        private RichTextBox ModDisplay;
        private ListView modList;
        private Button resync;
        private ProgressBar progressBar;
        private ToolStripMenuItem setManagerPathToolStripMenuItem;
        private ToolStripMenuItem setDCSSavedPathToolStripMenuItem;
        private ToolStripMenuItem setDCSMainPathToolStripMenuItem;
        private ToolStripMenuItem purgeInstallToolStripMenuItem;
        private ToolStripMenuItem adminToolStripMenuItem;
        private ToolStripMenuItem createRepoFilesToolStripMenuItem;
        private ToolStripMenuItem pushToServerToolStripMenuItem;
        private ToolStripMenuItem aboutToolStripMenuItem;
    }
}