using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SteamWebAPI2.Interfaces;
using SteamWebAPI2.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamesManager
{
    public static class SettingsManager
    {
        public static string GamesFolderPath
        {
            get
            {
                if (Properties.Settings.Default.gamesFolderPath == null)
                {
                    Properties.Settings.Default.gamesFolderPath = "";
                }
                return Properties.Settings.Default.gamesFolderPath;
            }
            set
            {
                Properties.Settings.Default.gamesFolderPath = value;
                Properties.Settings.Default.Save();
            }
        }

        public static StringCollection GetGamesSearchFolders()
        {
            if (Properties.Settings.Default.gamesSearchFolders == null)
            {
                Properties.Settings.Default.gamesSearchFolders = new StringCollection();
            }
            return Properties.Settings.Default.gamesSearchFolders;
        }

        public static void AddGamesSearchFolder(string path)
        {
            var gamesSearchFolders = GetGamesSearchFolders();
            gamesSearchFolders.Add(path);
            Properties.Settings.Default.gamesSearchFolders = gamesSearchFolders;
            Properties.Settings.Default.Save();
        }

        public static void RemoveGamesSearchFolder(string path)
        {
            var gamesSearchFolders = GetGamesSearchFolders();
            gamesSearchFolders.Remove(path);
            Properties.Settings.Default.gamesSearchFolders = gamesSearchFolders;
            Properties.Settings.Default.Save();
        }

        public static List<Game> GetGamesList()
        {
            if (Properties.Settings.Default.gamesList == null || Properties.Settings.Default.gamesList == "")
            {
                Properties.Settings.Default.gamesList = JsonConvert.SerializeObject(new List<Game>());
                Properties.Settings.Default.Save();
            }
            return JsonConvert.DeserializeObject<List<Game>>(Properties.Settings.Default.gamesList);
        }

        public static void AddGame(Game value)
        {
            var gamesList = GetGamesList();
            gamesList.Add(value);
            Properties.Settings.Default.gamesList = JsonConvert.SerializeObject(gamesList);
            Properties.Settings.Default.Save();
        }

        public static void RemoveGame(Game game)
        {
            var gamesList = GetGamesList();
            gamesList.Remove(gamesList.Find(i => i.Executable == game.Executable));
            Properties.Settings.Default.gamesList = JsonConvert.SerializeObject(gamesList);
            Properties.Settings.Default.Save();
        }

        public static bool IgnoreUnityCrashHandler
        {
            get
            {
                return Properties.Settings.Default.ignoreUnityCrashHandler;
            }
            set
            {
                Properties.Settings.Default.ignoreUnityCrashHandler = value;
                Properties.Settings.Default.Save();
            }
        }

        public static bool IgnoreUnins000
        {
            get
            {
                return Properties.Settings.Default.ignoreUnins000;
            }
            set
            {
                Properties.Settings.Default.ignoreUnins000 = value;
                Properties.Settings.Default.Save();
            }
        }

        public static bool FirstTime
        {
            get
            {
                return Properties.Settings.Default.FirstTime;
            }
            set
            {
                Properties.Settings.Default.FirstTime = value;
                Properties.Settings.Default.Save();
            }
        }

        public static StringCollection GetIgnoredFolders()
        {
            if (Properties.Settings.Default.ignoredFolders == null)
            {
                Properties.Settings.Default.ignoredFolders = new StringCollection();
            }
            return Properties.Settings.Default.ignoredFolders;
        }

        public static void AddIgnoredFolder(string path)
        {
            var ignoredFolders = GetIgnoredFolders();
            ignoredFolders.Add(path);
            Properties.Settings.Default.ignoredFolders = ignoredFolders;
            Properties.Settings.Default.Save();
        }

        public static void RemoveIgnoredFolder(string path)
        {
            var ignoredFolders = GetIgnoredFolders();
            ignoredFolders.Remove(path);
            Properties.Settings.Default.ignoredFolders = ignoredFolders;
            Properties.Settings.Default.Save();
        }

        public static bool IsGamesSearchFoldersValid()
        {
            return GetGamesSearchFolders().ToArray().All(i => Directory.Exists(i));
        }

        public static bool UseSteam
        {
            get
            {
                return Properties.Settings.Default.useSteam;
            }
            set
            {
                Properties.Settings.Default.useSteam = value;
                Properties.Settings.Default.Save();
            }
        }

        public static ulong SteamID
        {
            get
            {
                return Properties.Settings.Default.steamID;
            }
            set
            {
                Properties.Settings.Default.steamID = value;
                Properties.Settings.Default.Save();
            }
        }

        public static string SteamPath
        {
            get
            {
                return Properties.Settings.Default.steamPath;
            }
            set
            {
                if (!value.EndsWith("\\"))
                {
                    value += "\\";
                }
                Properties.Settings.Default.steamPath = value;
                Properties.Settings.Default.Save();
            }
        }

        public static bool IsSteamIDValid(string steamID)
        {
            // Known range of valid SteamID64s
            ulong minValidSteamId = 76561197960265728;
            ulong maxValidSteamId = ulong.MaxValue;

            if (ulong.TryParse(steamID, out ulong steamId))
            {
                // Check if the parsed SteamID falls within the valid range
                return steamId >= minValidSteamId && steamId <= maxValidSteamId;
            }

            // If it can't be parsed as a 64-bit integer, it's invalid
            return false;
        }

        public static bool IsSteamIDValid()
        {
            return IsSteamIDValid(SteamID.ToString());
        }

        public static void Reset()
        {
            Properties.Settings.Default.Reset();
            Console.WriteLine(Properties.Settings.Default);
        }
    }
}
