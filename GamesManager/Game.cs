using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamesManager
{
    public class Game
    {
        public string Name;
        public string Folder;
        public string Executable;
        public bool IsSteamGame;
        public uint AppID;
        public string IconPath;

        public Game()
        {
            this.Name = "";
            this.Folder = "";
            this.Executable = "";
            this.IsSteamGame = false;
            this.AppID = uint.MinValue;
            this.IconPath = "";
        }

        public Game(string name, string folder, string executable)
        {
            this.Name = name;
            this.Folder = folder;
            this.Executable = executable;
            this.IsSteamGame = false;
            this.AppID = uint.MinValue;
            this.IconPath = "";
        }

        public Game(string name, uint appID, string iconPath = null)
        {
            this.Name = name;
            this.Folder = "";
            this.Executable = "";
            this.IsSteamGame = true;
            this.AppID = appID;
            this.IconPath = iconPath;
        }

        public override string ToString()
        {
            if (this.IsSteamGame)
            {
                if (this.IconPath == null)
                {
                    return $"{this.Name} ({this.AppID})";
                }
                else
                {
                    return $"{this.Name} ({this.AppID}) [{this.IconPath}]";
                }
            }
            else
            {
                return $"{this.Name} ({this.Executable} [{this.Folder}])";
            }
        }
    }
}
