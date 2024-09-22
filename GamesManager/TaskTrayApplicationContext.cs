using Newtonsoft.Json;
using Steam.Models;
using Steam.Models.SteamCommunity;
using SteamWebAPI2.Interfaces;
using SteamWebAPI2.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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
        SteamWebInterfaceFactory steamWebInterfaceFactory;
        PlayerService playerServiceInterface;
        SteamApps steamAppsInterface;

        public TaskTrayApplicationContext()
        {
            foreach (var game in SettingsManager.GetGamesList())
            {
                Console.WriteLine(game.ToString());
            }
            if (SettingsManager.FirstTime)
            {
                MessageBox.Show("It is strongly recommended to change the program's settings by right-clicking the program's icon and clicking on \"Configuration\".\nThis message will show at startup until you properly set the configuration", "Warning", MessageBoxButtons.OK);
            }
            MenuItem configMenuItem = new MenuItem("Configuration", new EventHandler(ShowConfig));
            MenuItem exitMenuItem = new MenuItem("Exit", new EventHandler(Exit));

            notifyIcon.Icon = Properties.Resources.AppIcon;
            notifyIcon.DoubleClick += new EventHandler(UpdateGamesFolder);
            notifyIcon.ContextMenu = new ContextMenu(new MenuItem[] { configMenuItem, exitMenuItem });

            if (SettingsManager.UseSteam && SettingsManager.SteamID != 0)
            {
                steamWebInterfaceFactory = new SteamWebInterfaceFactory(Properties.Resources.data);
                playerServiceInterface = steamWebInterfaceFactory.CreateSteamWebInterface<PlayerService>();
                steamAppsInterface = steamWebInterfaceFactory.CreateSteamWebInterface<SteamApps>();
                var task = steamAppsInterface.GetAppListAsync();
                var awaiter = task.GetAwaiter();
                awaiter.OnCompleted(() => {
                    Utils.AppList = awaiter.GetResult().Data;
                    notifyIcon.Visible = true;
                });
            }
            else
            {
                notifyIcon.Visible = true;
            }
        }

        void ShowConfig(object sender, EventArgs e)
        {
            if (configWindow.Visible)
                configWindow.Focus();
            else
                configWindow.ShowDialog();
        }

        async void UpdateGamesFolder(object sender, EventArgs e)
        {
            var gamesList = SettingsManager.GetGamesList();
            foreach (var game in gamesList)
            {
                if (game.IsSteamGame)
                {
                    string path = $"{SettingsManager.GamesFolderPath}\\{game.Name.ToSafeFileName()}.url";
                    if (!File.Exists(path))
                    {
                        Console.WriteLine($"Removing game {game.Name}");
                        SettingsManager.RemoveGame(game);
                    }
                }
                else
                {
                    string path = $"{SettingsManager.GamesFolderPath}\\{game.Name}.lnk";
                    if (!File.Exists(path))
                    {
                        Console.WriteLine($"Removing game {game.Name}");
                        SettingsManager.RemoveGame(game);
                    }
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
            if (SettingsManager.UseSteam && SettingsManager.SteamID != 0)
            {
                var steamFiles = Directory.GetFiles(SettingsManager.GamesFolderPath, "*.url");
                foreach (var file in steamFiles)
                {
                    FileInfo info = new FileInfo(file);
                    string target = info.GetUrlFromShortcut();
                    var substring = target.Substring(Utils.steamGameShortcutPrefix.Length);
                    var allGamesList = SettingsManager.GetGamesList();
                    Game game = new Game("", uint.MinValue);
                    foreach (var item in allGamesList)
                    {
                        if (item.IsSteamGame && item.AppID.ToString() == substring)
                        {
                            game = item;
                        }
                        else
                        {
                            continue;
                        }
                    }
                    if (game.Name == "" && game.AppID == uint.MinValue)
                    {
                        AddToDeletionQueue(file);
                    }
                    /*
                    if (allGamesList.FirstOrDefault(i => i.IsSteamGame && (i.AppID.ToString() == substring)) == null)
                    {
                        AddToDeletionQueue(file);
                    }*/
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
            if (SettingsManager.UseSteam && SettingsManager.SteamID != 0)
            {
                ISteamWebResponse<OwnedGamesResultModel> response = await playerServiceInterface.GetOwnedGamesAsync(SettingsManager.SteamID, true, true);
                IReadOnlyCollection<OwnedGameModel> ownedgames = response.Data.OwnedGames;
                List<Game> steamGames = new List<Game>();
                foreach (OwnedGameModel game in ownedgames)
                {
                    string name = game.Name;
                    if (name == null)
                    {
                        var steamAppModel = Utils.GetSteamAppModel(game.AppId);
                        name = steamAppModel == null ? game.AppId.ToString() : steamAppModel.Name;
                    }
                    if (!IsConfiguredSteamGame(game.AppId))
                    {
                        var iconPath = await Utils.GetGameIconAsync(game.AppId, game.ImgIconUrl);
                        if (File.Exists(iconPath))
                        {
                            AddToCreationQueue(JsonConvert.SerializeObject(new Game(name, game.AppId, iconPath)));
                        }
                        else
                        {
                            AddToCreationQueue(JsonConvert.SerializeObject(new Game(name, game.AppId)));
                        }
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

        bool IsConfiguredSteamGame(uint appID)
        {
            foreach (var game in SettingsManager.GetGamesList())
            {
                if (game.IsSteamGame && game.AppID == appID)
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

            if (Utils.client != null)
            {
                Utils.client.Dispose();
            }

            Application.Exit();
        }
    }
}
