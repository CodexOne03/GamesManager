using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GamesManager
{
    public class TaskTrayApplicationContext : ApplicationContext
    {
        NotifyIcon notifyIcon = new NotifyIcon();
        Configuration configWindow = new Configuration();
        Queue<string> deletionQueue = new Queue<string>();
        Queue<string> creationQueue = new Queue<string>();

        public TaskTrayApplicationContext()
        {
            if (SettingsManager.FirstTime)
            {
                MessageBox.Show("It is strongly recommended to change the program's settings by right-clicking the program's icon and clicking on \"Configuration\".\nThis message will show at startup until you properly set the configuration", "Warning", MessageBoxButtons.OK);
            }
            MenuItem configMenuItem = new MenuItem("Configuration", new EventHandler(ShowConfig));
            MenuItem exitMenuItem = new MenuItem("Exit", new EventHandler(Exit));

            notifyIcon.Icon = Properties.Resources.AppIcon;
            notifyIcon.DoubleClick += new EventHandler(UpdateGamesFolder);
            notifyIcon.ContextMenu = new ContextMenu(new MenuItem[] { configMenuItem, exitMenuItem });
            notifyIcon.Visible = true;
        }

        void ShowConfig(object sender, EventArgs e)
        {
            if (configWindow.Visible)
                configWindow.Focus();
            else
                configWindow.ShowDialog();
        }

        void UpdateGamesFolder(object sender, EventArgs e)
        {
            var gamesList = SettingsManager.GetGamesList();
            foreach (var game in gamesList)
            {
                string path = $"{SettingsManager.GamesFolderPath}\\{game.Name}.lnk";
                if (!File.Exists(path))
                {
                    Console.WriteLine($"Removing game {game.Name}");
                    SettingsManager.RemoveGame(game);
                }
            }
            var files = Directory.GetFiles(SettingsManager.GamesFolderPath, "*.lnk");
            foreach (var file in files)
            {
                FileInfo info = new FileInfo(file);
                FileInfo target = info.GetShortcutTarget();
                if (target.IsInsideFolders(SettingsManager.GetGamesSearchFolders().ToArray()))
                {
                    if (!target.Exists)
                    {
                        AddToDeletionQueue(file);
                    }
                }
            }
            if (this.deletionQueue != null && this.deletionQueue.Count > 0)
            {
                AskDeleteFiles();
            }

            foreach (var folder in SettingsManager.GetGamesSearchFolders())
            {
                foreach (var directory in Directory.GetDirectories(folder))
                {
                    if (SettingsManager.GetIgnoredFolders().Contains(directory))
                    {
                        continue;
                    }
                    try
                    {
                        var executables = Directory.GetFiles(directory, "*.exe");
                        foreach (var exe in executables)
                        {
                            var info = new FileInfo(exe);
                            if ((SettingsManager.IgnoreUnityCrashHandler && info.Name.StartsWith("UnityCrashHandler")) || (SettingsManager.IgnoreUnins000 && info.Name.StartsWith("unins000")))
                            {
                                continue;
                            }
                            if (!IsConfiguredGame(info))
                            {
                                AddToCreationQueue(exe);
                            }
                        }
                    }
                    catch
                    {

                    }
                }
            }
            if (this.creationQueue != null && this.creationQueue.Count > 0)
            {
                //Console.WriteLine($"Warn create files");
                WarnCreateFiles();
            }
            MessageBox.Show("Games folder updated");
        }

        bool IsConfiguredGame(FileInfo executableInfo)
        {
            foreach (var game in SettingsManager.GetGamesList())
            {
                if (executableInfo.FullName == game.Executable)
                {
                    return true;
                }
            }
            return false;
        }

        void AddToDeletionQueue(string file)
        {
            if (this.deletionQueue == null)
            {
                this.deletionQueue = new Queue<string>();
            }
            this.deletionQueue.Enqueue(file);
        }

        void AddToCreationQueue(string file)
        {
            if (this.creationQueue == null)
            {
                this.creationQueue = new Queue<string>();
            }
            this.creationQueue.Enqueue(file);
        }

        void AskDeleteFiles()
        {
            DeleteMessageBox.Show(deletionQueue);
        }

        void WarnCreateFiles()
        {
            CreateMessageBox.Show(creationQueue);
        }

        void Exit(object sender, EventArgs e)
        {
            // We must manually tidy up and remove the icon before we exit.
            // Otherwise it will be left behind until the user mouses over.
            notifyIcon.Visible = false;

            Application.Exit();
        }
    }
}
