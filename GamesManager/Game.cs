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

        public Game()
        {
            this.Name = "";
            this.Folder = "";
            this.Executable = "";
        }

        public Game(string name, string folder, string executable)
        {
            this.Name = name;
            this.Folder = folder;
            this.Executable = executable;
        }
    }
}
