using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GamesManager
{
    public partial class Configuration : Form
    {
        public Configuration()
        {
            InitializeComponent();
        }

        private void selectGamesFolderButton_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.ShowDialog();
            SettingsManager.GamesFolderPath = folderBrowserDialog1.SelectedPath;
            Properties.Settings.Default.Save();
            UpdateGamesFolderPathUI();
        }

        private void Configuration_Load(object sender, EventArgs e)
        {
            UpdateUI();
        }

        private void UpdateUI()
        {
            UpdateGamesSearchPathsUI();
            UpdateGamesFolderPathUI();
            this.ignoreUnityCrashHandlerCheckBox.Checked = SettingsManager.IgnoreUnityCrashHandler;
            this.ignoreunins000CheckBox.Checked = SettingsManager.IgnoreUnins000;
            UpdateIgnoredFoldersUI();
            this.steamCheckBox.Checked = SettingsManager.UseSteam;
            this.steamIDTextBox.Text = SettingsManager.SteamID.ToString();
        }

        private void UpdateGamesSearchPathsUI()
        {
            gamesSearchPaths.Items.Clear();
            foreach (var item in SettingsManager.GetGamesSearchFolders())
            {
                gamesSearchPaths.Items.Add(item);
            }
        }

        private void UpdateGamesFolderPathUI()
        {
            this.textBox1.Text = SettingsManager.GamesFolderPath;
        }

        private void UpdateIgnoredFoldersUI()
        {
            ignoredFoldersPaths.Items.Clear();
            foreach (var item in SettingsManager.GetIgnoredFolders())
            {
                ignoredFoldersPaths.Items.Add(item);
            }
        }

        private void Configuration_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (SettingsManager.FirstTime && Directory.Exists(SettingsManager.GamesFolderPath) && SettingsManager.IsGamesSearchFoldersValid())
            {
                SettingsManager.FirstTime = false;
            }
        }

        private void addPathButton_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            SettingsManager.AddGamesSearchFolder(folderBrowserDialog1.SelectedPath);
            UpdateGamesSearchPathsUI();
        }

        private void removePathButton_Click(object sender, EventArgs e)
        {
            if (gamesSearchPaths.SelectedIndex  <= 0)
            {
                return;
            }
            var item = (string)gamesSearchPaths.SelectedItem;
            SettingsManager.RemoveGamesSearchFolder(item);
            UpdateGamesSearchPathsUI();
        }

        private void ignoreUnityCrashHandlerCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            SettingsManager.IgnoreUnityCrashHandler = this.ignoreUnityCrashHandlerCheckBox.Checked;
        }

        private void ignoreunins000CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            SettingsManager.IgnoreUnins000 = this.ignoreunins000CheckBox.Checked;
        }

        private void resetConfigurationButton_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("This function's usage is strongly discouraged!\nIf you confirm, you will completely reset the program's configuration to its default values.\nDo you wish to continue?", "Warning", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                SettingsManager.Reset();
                UpdateUI();
            }
        }

        private void addIgnoredButton_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            SettingsManager.AddIgnoredFolder(folderBrowserDialog1.SelectedPath);
            UpdateIgnoredFoldersUI();
        }

        private void removeIgnoredButton_Click(object sender, EventArgs e)
        {
            if (ignoredFoldersPaths.SelectedIndex <= 0)
            {
                return;
            }
            var item = (string)ignoredFoldersPaths.SelectedItem;
            SettingsManager.RemoveIgnoredFolder(item);
            UpdateIgnoredFoldersUI();
        }

        private void resetGamesListButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show(Properties.Settings.Default.gamesList, "gamesList", MessageBoxButtons.OK);
            var result = MessageBox.Show("This function's usage is strongly discouraged!\nIf you confirm, you will completely reset the program's games list to its default value.\nDo you wish to continue?", "Warning", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                Properties.Settings.Default.gamesList = "";
                Properties.Settings.Default.Save();
                UpdateUI();
            }
        }

        private void steamIDTextBox_HelpRequested(object sender, HelpEventArgs hlpevent)
        {
            Process.Start("explorer", "https://www.steamidfinder.com");
        }

        private void steamCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            this.label4.Enabled = steamCheckBox.Checked;
            this.steamIDTextBox.Enabled = steamCheckBox.Checked;
            SettingsManager.UseSteam = steamCheckBox.Checked;
        }

        private void steamIDTextBox_TextChanged(object sender, EventArgs e)
        {
            if (!SettingsManager.IsSteamIDValid(steamIDTextBox.Text))
            {
                label5.Visible = true;
            }
            else
            {
                label5.Visible = false;
                SettingsManager.SteamID = Convert.ToUInt64(steamIDTextBox.Text);
            }
        }
    }
}
