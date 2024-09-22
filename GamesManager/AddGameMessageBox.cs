using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GamesManager
{
    public partial class AddGameMessageBox : Form
    {
        public Game Game;
        public bool SetCustomName;
        public AddGameMessageBoxResult Result;

        private Button yesButton;
        private Button noButton;
        private Button cancelButton;
        private TableLayoutPanel tableLayoutPanel;

        public AddGameMessageBox(string gamePath, bool setCustomName)
        {
            InitializeComponent();
            this.Game = new Game();
            this.Game.Executable = gamePath;
            this.Game.Folder = new FileInfo(gamePath).DirectoryName;
            this.SetCustomName = setCustomName;
            SetText();
            if (!this.SetCustomName)
            {
                this.textBox1.Visible = false;
                this.label2.Visible = false;
            }
            AddButtons();
        }

        public AddGameMessageBox(string steamGame)
        {
            InitializeComponent();
            this.Game = JsonConvert.DeserializeObject<Game>(steamGame);
            this.SetCustomName = false;
            SetText();
            this.textBox1.Visible = false;
            this.label2.Visible = false;
            this.addIgnoreCheckBox.Visible = false;
            AddButtons();
        }

        private void AddButtons()
        {
            // Initialize buttons
            yesButton = new Button { Text = "Yes" };
            yesButton.Click += YesButton_Click;
            noButton = new Button { Text = "No", AutoSize = true, AutoSizeMode = AutoSizeMode.GrowOnly };
            noButton.Click += NoButton_Click;
            cancelButton = new Button { Text = "Cancel" };
            cancelButton.Click += CancelButton_Click;

            // Initialize TableLayoutPanel
            tableLayoutPanel = new TableLayoutPanel
            {
                Dock = DockStyle.Bottom,
                RowCount = 1,
                ColumnCount = 3,
                AutoSize = true
            };

            // Add buttons to TableLayoutPanel
            tableLayoutPanel.Controls.Add(yesButton, 0, 0);
            tableLayoutPanel.Controls.Add(noButton, 1, 0);
            tableLayoutPanel.Controls.Add(cancelButton, 2, 0);

            // Add TableLayoutPanel to the form
            this.Controls.Add(tableLayoutPanel);
            this.AcceptButton = yesButton;
            this.CancelButton = noButton;
        }

        private void SetText()
        {
            if (SettingsManager.UseSteam && Game.IsSteamGame)
            {
                this.label1.Text = $"Do you want to add the following steam game to the program?\n- {Game.Name}";
            }
            else
            {
                this.label1.Text = $"Do you want to add the following game to the program?\n- {Game.Executable}";
            }
        }

        private void YesButton_Click(object sender, EventArgs e)
        {
            if (SettingsManager.UseSteam && Game.IsSteamGame)
            {
                Game.CreateUrlFile(SettingsManager.GamesFolderPath);
            }
            else
            {
                FileInfo info = new FileInfo(Game.Executable);
                if (!SetCustomName || this.textBox1.Text == "")
                {
                    Game.Name = info.Name.Replace(info.Extension, "");
                }
                else
                {
                    Game.Name = this.textBox1.Text;
                }
                info.CreateShortcut(SettingsManager.GamesFolderPath, Game.Name, "Created by GamesManager");
            }
            SettingsManager.AddGame(Game);
            Result = AddGameMessageBoxResult.Yes;
            this.Close();
        }

        private void NoButton_Click(object sender, EventArgs e)
        {
            Result = AddGameMessageBoxResult.No;
            if (this.addIgnoreCheckBox.Checked)
            {
                SettingsManager.AddIgnoredFolder(Game.Folder);
                Result = AddGameMessageBoxResult.AddIgnore;
            }
            this.Close();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            Result = AddGameMessageBoxResult.Cancel;
            this.Close();
        }

        public static AddGameMessageBoxResult Show(string gamePath, bool setCustomName)
        {
            AddGameMessageBox box = new AddGameMessageBox(gamePath, setCustomName);
            box.ShowDialog();
            return box.Result;
        }

        public static AddGameMessageBoxResult Show(string steamGame)
        {
            AddGameMessageBox box = new AddGameMessageBox(steamGame);
            box.ShowDialog();
            return box.Result;
        }

        public enum AddGameMessageBoxResult
        {
            Yes,
            No,
            AddIgnore,
            Cancel
        }
    }
}
