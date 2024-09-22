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
using static System.Net.Mime.MediaTypeNames;

namespace GamesManager
{
    public partial class CreateMessageBox : Form
    {
        public Queue<string> CreationQueue;
        public CreateMessageBoxResult Result;

        private Button addAllButton;
        private Button reviewButton;
        private Button cancelButton;
        private TableLayoutPanel tableLayoutPanel;

        public CreateMessageBox(Queue<string> creationQueue)
        {
            InitializeComponent();
            this.CreationQueue = creationQueue;
            AddButtons();
            SetText();
        }

        private void AddButtons()
        {
            // Initialize buttons
            addAllButton = new Button { Text = "Add all" };
            addAllButton.Click += AddAllButton_Click;
            reviewButton = new Button { Text = "Review and confirm", AutoSize = true, AutoSizeMode = AutoSizeMode.GrowOnly };
            reviewButton.Click += ReviewButton_Click;
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
            tableLayoutPanel.Controls.Add(addAllButton, 0, 0);
            tableLayoutPanel.Controls.Add(reviewButton, 1, 0);
            tableLayoutPanel.Controls.Add(cancelButton, 2, 0);

            // Add TableLayoutPanel to the form
            this.Controls.Add(tableLayoutPanel);
        }

        private void SetText()
        {
            string text = "The following games are about to be added to the program:\n";
            foreach (string file in CreationQueue)
            {
                if (file.StartsWith("{"))
                {
                    Game game = JsonConvert.DeserializeObject<Game>(file);
                    text += $"- {game.Name}\n";
                }
                else
                {
                    text += $"- {file}\n";
                }
            }
            text += "Do you want to continue?";
            this.label1.Text = text;
        }

        private void AddAllButton_Click(object sender, EventArgs e)
        {
            do
            {
                string path = CreationQueue.Dequeue();
                if (path.StartsWith("{"))
                {
                    Game game = JsonConvert.DeserializeObject<Game>(path);
                    SettingsManager.AddGame(game);
                    game.CreateUrlFile(SettingsManager.GamesFolderPath);
                }
                else
                {
                    var info = new FileInfo(path);
                    var gameName = info.Name.Replace(info.Extension, "");
                    SettingsManager.AddGame(new Game(gameName, info.DirectoryName, path));
                    info.CreateShortcut(SettingsManager.GamesFolderPath, gameName, "Created by GamesManager");
                }
            }
            while (CreationQueue.Count > 0);
            Result = CreateMessageBoxResult.AddAll;
            this.Close();
        }

        private void ReviewButton_Click(object sender, EventArgs e)
        {
            Result = CreateMessageBoxResult.Review;
            do
            {
                string path = CreationQueue.Dequeue();
                if (path.StartsWith("{"))
                {
                    var result = AddGameMessageBox.Show(path);
                    if (result == AddGameMessageBox.AddGameMessageBoxResult.Cancel)
                    {
                        Result = CreateMessageBoxResult.Cancel;
                        break;
                    }
                }
                else
                {
                    var dir = new FileInfo(path).DirectoryName;
                    if (SettingsManager.GetIgnoredFolders().ToArray().FirstOrDefault(i => i == dir) == null)
                    {
                        var result = AddGameMessageBox.Show(path, this.setNamesCheckBox.Checked);
                        if (result == AddGameMessageBox.AddGameMessageBoxResult.Cancel)
                        {
                            Result = CreateMessageBoxResult.Cancel;
                            break;
                        }
                    }
                }
            }
            while (CreationQueue.Count > 0);
            this.Close();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            Result = CreateMessageBoxResult.Cancel;
            this.Close();
        }

        public static CreateMessageBoxResult Show(Queue<string> creationQueue)
        {
            CreateMessageBox box = new CreateMessageBox(creationQueue);
            box.ShowDialog();
            return box.Result;
        }

        public enum CreateMessageBoxResult
        {
            AddAll,
            Review,
            Cancel
        }

        private void label1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                Clipboard.SetText(this.label1.Text);
            }
        }
    }
}
