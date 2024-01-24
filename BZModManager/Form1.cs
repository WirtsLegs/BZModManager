using System;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.Net;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.ConstrainedExecution;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.DataFormats;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace BZModManager
{
    public partial class Form1 : Form
    {
        private bool downloading = false;
        public Form1()
        {
            InitializeComponent();
            initListview();

            updateButtons();

            if (ConfigurationManager.AppSettings["manager"] != "" && ConfigurationManager.AppSettings["saved"] != "")
            {
                resync_list();
            }
            if (ConfigurationManager.AppSettings["manager"] != "")
            {
                this.adminToolStripMenuItem.Enabled = true;
            }



            //this.listView1.Groups[0].Items.Insert(0, item);
        }

        private static void SetSetting(string key, string value)
        {
            Configuration configuration =
                ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            configuration.AppSettings.Settings[key].Value = value;
            configuration.Save(ConfigurationSaveMode.Full, true);
            ConfigurationManager.RefreshSection("appSettings");
        }

        private void initListview()
        {
            //this.listView1.Groups.Add("GroupKey" + 0, "Mods" + 1);
            this.modList.View = View.Details;
            this.modList.Columns.Add("Name", 250, HorizontalAlignment.Left);
            this.modList.Columns.Add("Version", 100, HorizontalAlignment.Right);
            this.modList.Groups.Add(new ListViewGroup("Core Mods", HorizontalAlignment.Left));
            this.modList.Groups.Add(new ListViewGroup("Flyable Mods", HorizontalAlignment.Left));
            this.modList.Groups.Add(new ListViewGroup("Liveries", HorizontalAlignment.Left));
            this.modList.Groups.Add(new ListViewGroup("Optional Mods", HorizontalAlignment.Left));

        }

        private void updateButtons()
        {
            System.Windows.Forms.ListView.SelectedListViewItemCollection curItems = this.modList.SelectedItems;
            if (curItems.Count > 0)
            {
                mod m = mod.getByName(curItems[0].Text);
                if (m.isEnabled())
                {
                    this.toggleONOFF.Text = "Disable";
                }
                else
                {
                    this.toggleONOFF.Text = "Enable";
                }

                if (this.downloading)
                {
                    this.Download.Enabled = false;
                    this.deleteCancel.Enabled = false;
                    this.toggleONOFF.Enabled = false;
                    this.resync.Enabled = false;
                    this.fileMenu.Enabled = false;
                }
                else
                {
                    this.fileMenu.Enabled = true;
                    if (m.getInstalled_Ver() == "")
                    {
                        this.deleteCancel.Enabled = false;
                        this.toggleONOFF.Enabled = false;
                    }
                    else
                    {
                        this.deleteCancel.Enabled = true;
                        this.toggleONOFF.Enabled = true;
                    }
                    if (m.getNewest_Ver() == "")
                    {
                        this.Download.Enabled = false;
                    }
                    else
                    {
                        this.Download.Enabled = true;
                    }
                    if (ConfigurationManager.AppSettings["manager"] != "" && ConfigurationManager.AppSettings["saved"] != "")
                    {
                        this.resync.Enabled = true;
                    }
                    else
                    {
                        this.resync.Enabled = false;
                    }

                }


            }
            else
            {
                this.deleteCancel.Enabled = false;
                this.toggleONOFF.Enabled = false;
                this.Download.Enabled = false;
                if (ConfigurationManager.AppSettings["manager"] != "" && ConfigurationManager.AppSettings["saved"] != "" && this.downloading == false)
                {
                    this.resync.Enabled = true;
                }
                else
                {
                    this.resync.Enabled = false;
                }
            }
        }

        private void modlist_SelectedIndexChanged(object sender, EventArgs e)
        {
            System.Windows.Forms.ListView.SelectedListViewItemCollection curItems = this.modList.SelectedItems;
            if (curItems.Count > 0)
            {
                mod m = mod.getByName(curItems[0].Text);
                this.ModDisplay.Text = m.getText();
            }
            else
            {
                this.ModDisplay.Text = "";
            }
            updateButtons();

        }

        private void resync_Click(object sender, EventArgs e)
        {
            resync_list();
        }

        private void resync_list()
        {
            mod.updateMods();
            List<mod> mods = mod.getMods();
            this.modList.Items.Clear();

            foreach (mod m in mods)
            {
                ListViewItem entry = new ListViewItem(m.getName());
                string installed = m.getInstalled_Ver();
                string newest = m.getNewest_Ver();
                if (newest != installed)
                {
                    if (installed != "" && newest != "")
                    {
                        entry.SubItems.Add(installed + "(" + newest + ")");
                        entry.SubItems[0].ForeColor = Color.Blue;
                    }
                    else if (newest == "")
                    {
                        entry.SubItems.Add(installed);
                        entry.SubItems[0].ForeColor = Color.Purple;
                    }
                    else if (installed == "")
                    {
                        entry.SubItems.Add(newest);
                        entry.SubItems[0].ForeColor = Color.Red;
                    }


                }
                else if (m.isEnabled())
                {
                    entry.SubItems.Add(newest);
                    entry.SubItems[0].ForeColor = Color.Green;
                }
                else
                {
                    entry.SubItems.Add(newest);
                }

                entry.Checked = true;
                entry.Group = this.modList.Groups[m.getType()];
                //entry.BackColor = Color.Red;
                this.modList.Items.Add(entry);

            }
        }

        private async void Download_Click(object sender, EventArgs e)
        {
            string url = "";
            System.Windows.Forms.ListView.SelectedListViewItemCollection curItems = this.modList.SelectedItems;
            if (curItems.Count > 0)
            {
                this.downloading = true;
                updateButtons();
                System.IO.Directory.CreateDirectory(ConfigurationManager.AppSettings["manager"] + "\\" + "Temp");
                System.IO.Directory.CreateDirectory(ConfigurationManager.AppSettings["manager"] + "\\" + "Mods");
                System.IO.Directory.CreateDirectory(ConfigurationManager.AppSettings["manager"] + "\\" + "Liveries");
                System.IO.Directory.CreateDirectory(ConfigurationManager.AppSettings["manager"] + "\\" + "Admin");
                foreach (ListViewItem n in curItems)
                {
                    mod m = mod.getByName(n.Text);
                    url = m.getURL();

                    string zipFile = ConfigurationManager.AppSettings["manager"] + "\\Temp\\" + m.getName() + ".zip";
                    using (var client = new HttpClientDownloadWithProgress(url, zipFile))
                    {
                        client.ProgressChanged += (totalFileSize, totalBytesDownloaded, progressPercentage) =>
                        {
                            Console.WriteLine($"{progressPercentage}% ({totalBytesDownloaded}/{totalFileSize})");
                            this.progressBar.Value = int.Parse(Math.Truncate((decimal)progressPercentage).ToString());
                        };

                        await client.StartDownload();


                        client.Dispose();
                    }
                    m.remove();
                    if (m.getType() == 0 || m.getType() == 1 || m.getType() == 3)
                    {

                        System.IO.Compression.ZipFile.ExtractToDirectory(zipFile, ConfigurationManager.AppSettings["manager"] + "\\Mods\\" + m.getName());
                    }
                    else if (m.getType() == 2)
                    {
                        System.IO.Compression.ZipFile.ExtractToDirectory(zipFile, ConfigurationManager.AppSettings["manager"] + "\\Liveries\\" + m.getName());
                    }
                    if (System.IO.File.Exists(zipFile))
                    {
                        System.IO.File.Delete(zipFile);
                    }
                    this.progressBar.Value = 0;
                    mod.updateMods();
                    resync_list();

                }

                this.downloading = false;
                updateButtons();

            }
        }

        // Event to track the progress
        void Progress_ProgressChanged(object sender, float progress)
        {
            // Do something with your progress
            Console.WriteLine(progress);
        }

        void client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            this.BeginInvoke((MethodInvoker)delegate
            {
                double bytesIn = double.Parse(e.BytesReceived.ToString());
                double totalBytes = double.Parse(e.TotalBytesToReceive.ToString());
                double percentage = bytesIn / totalBytes * 100;
                //label2.Text = "Downloaded " + e.BytesReceived + " of " + e.TotalBytesToReceive;
                this.progressBar.Value = int.Parse(Math.Truncate(percentage).ToString());
            });
        }


        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void setDCSSavedPathToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                if (ConfigurationManager.AppSettings["saved"] != "")
                {
                    fbd.InitialDirectory = ConfigurationManager.AppSettings["saved"];
                    fbd.SelectedPath = ConfigurationManager.AppSettings["saved"];
                }
                else
                {

                    fbd.InitialDirectory = (Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\Saved Games\\");
                }
                DialogResult result = fbd.ShowDialog();
                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    string[] files = Directory.GetDirectories(fbd.SelectedPath + "\\");
                    int mods = 0;
                    int metashaders = 0;
                    int config = 0;
                    foreach (string file in files)
                    {
                        if (file.Contains("Mods"))
                        {
                            mods = 1;
                        }
                        else if (file.Contains("Config"))
                        {
                            config = 1;
                        }
                        else if (file.Contains("metashaders2"))
                        {
                            metashaders = 1;
                        }
                    }

                    if ((metashaders + config + mods) >= 2)
                    {
                        //string[] files = Directory.GetFiles(fbd.SelectedPath);
                        //config.SavedPath = fbd.SelectedPath;
                        ConfigurationManager.AppSettings["saved"] = fbd.SelectedPath;
                        SetSetting("saved", fbd.SelectedPath);
                        if (ConfigurationManager.AppSettings["manager"] != "")
                        {
                            resync_list();
                        }


                    }
                    else
                    {
                        DialogResult r2 = MessageBox.Show(this, "Target folder does not appear to be a DCS Saved game directory",
                            "Invalid target",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error,
                            MessageBoxDefaultButton.Button1, 0);
                    }
                }
            }

            updateButtons();
        }

        private void setManagerPathToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                if (ConfigurationManager.AppSettings["manager"] != "")
                {
                    fbd.InitialDirectory = ConfigurationManager.AppSettings["manager"];
                    fbd.SelectedPath = ConfigurationManager.AppSettings["manager"];
                }
                else
                {
                    fbd.InitialDirectory = "C:\\";
                }
                DialogResult result = fbd.ShowDialog();//MyComputer

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    //string[] files = Directory.GetFiles(fbd.SelectedPath);
                    //config.ManagerPath = fbd.SelectedPath;
                    ConfigurationManager.AppSettings["manager"] = fbd.SelectedPath;
                    SetSetting("manager", fbd.SelectedPath);
                    System.IO.Directory.CreateDirectory(ConfigurationManager.AppSettings["manager"] + "\\" + "Temp");
                    System.IO.Directory.CreateDirectory(ConfigurationManager.AppSettings["manager"] + "\\" + "Mods");
                    System.IO.Directory.CreateDirectory(ConfigurationManager.AppSettings["manager"] + "\\" + "Liveries");
                    System.IO.Directory.CreateDirectory(ConfigurationManager.AppSettings["manager"] + "\\" + "Admin");
                    if (ConfigurationManager.AppSettings["saved"] != "")
                    {
                        resync_list();
                    }

                    //System.Windows.Forms.MessageBox.Show("Files found: " + files.Length.ToString(), "Message");
                }
            }
            updateButtons();
        }

        private void setDCSMainPathToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                if (ConfigurationManager.AppSettings["main"] != "")
                {
                    fbd.InitialDirectory = ConfigurationManager.AppSettings["main"];
                    fbd.SelectedPath = ConfigurationManager.AppSettings["main"];
                }
                else
                {
                    fbd.InitialDirectory = "C:\\";
                }
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    //string[] files = Directory.GetFiles(fbd.SelectedPath);
                    //config.MainPath = fbd.SelectedPath;
                    ConfigurationManager.AppSettings["main"] = fbd.SelectedPath;
                    SetSetting("main", fbd.SelectedPath);

                    //System.Windows.Forms.MessageBox.Show("Files found: " + files.Length.ToString(), "Message");
                }
            }
            updateButtons();
        }

        private void deleteCancel_Click(object sender, EventArgs e)
        {
            //mod.remove()
            System.Windows.Forms.ListView.SelectedListViewItemCollection curItems = this.modList.SelectedItems;
            if (curItems.Count > 0)
            {
                foreach (ListViewItem n in curItems)
                {
                    mod m = mod.getByName(n.Text);
                    m.remove();

                }
                resync_list();
            }
        }

        private void toggleONOFF_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.ListView.SelectedListViewItemCollection curItems = this.modList.SelectedItems;
            if (curItems.Count > 0)
            {
                this.deleteCancel.Enabled = false;
                this.Download.Enabled = false;
                foreach (ListViewItem n in curItems)
                {

                    mod m = mod.getByName(n.Text);
                    if (m != null && m.getInstalled_Ver() != "")
                    {
                        if (m.isEnabled())
                        {
                            m.disable();
                        }
                        else
                        {
                            m.enable();
                        }
                    }


                }
                resync_list();
                this.updateButtons();
            }

        }

        private async void createRepoFilesToolStripMenuItem_ClickAsync(object sender, EventArgs e)
        {

            //var confirmResult = MessageBox.Show("You are about to zip all your enabled mods, this can take some time"+Environment.NewLine+"A new dialogue will inform you when complete, do you wish to continue?",
            //                         MessageBoxButtons.OK);

            this.downloading = true;
            this.updateButtons();

            DialogResult r1 = MessageBox.Show(this, "You are about to zip all your enabled mods, this can take some time." + Environment.NewLine + "A new dialogue will inform you when complete, please do not close the program until then." + Environment.NewLine + Environment.NewLine + "Do you wish to continue?",
                                   "Build zip files for repo?",
                                   MessageBoxButtons.YesNo,
                                   MessageBoxIcon.Question,
                                   MessageBoxDefaultButton.Button1, 0);
            if (r1 == DialogResult.Yes)
            {
                Thread.Sleep(10);
                await mod.generateRepoFiles();

                DialogResult r2 = MessageBox.Show(this, "ZIP and XML file build completed, would you like to view them now?",
                       "Repo Files build completed",
                       MessageBoxButtons.YesNo,
                       MessageBoxIcon.Question,
                       MessageBoxDefaultButton.Button1, 0);
                if (r2 == DialogResult.Yes)
                {
                    Process.Start("explorer.exe", ConfigurationManager.AppSettings["manager"] + "\\Admin");
                }


            }
            this.downloading = false;
            resync_list();
        }

        private void pushToServerToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new AboutForm();
            var parent = this.FindForm();
            var screenBounds = Screen.FromControl(parent).Bounds;
            Point panelLoc = parent.Location;//PointToScreen(this.Location);
            int X = panelLoc.X - screenBounds.X + (this.Width / 2) - (form.Width / 2);
            int Y = panelLoc.Y - screenBounds.Y + (this.Height / 2) - (form.Height / 2);
            form.StartPosition= FormStartPosition.Manual;
            form.Location = new Point(X, Y);
            form.Show(parent);
        }
    }
}