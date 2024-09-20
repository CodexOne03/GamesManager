using IWshRuntimeLibrary;
using Shell32;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GamesManager
{
    public static class Utils
    {
        public static FileInfo GetShortcutTarget(this FileInfo info)
        {
            string file = info.FullName;
            Shell shell = new Shell();
            Shell32.Folder folder = shell.NameSpace(System.IO.Path.GetDirectoryName(file));
            FolderItem folderItem = folder.ParseName(System.IO.Path.GetFileName(file));

            if (folderItem != null)
            {
                ShellLinkObject link = (ShellLinkObject)folderItem.GetLink;
                return new FileInfo(link.Path);
            }

            return null;
        }

        public static bool IsInsideFolders(this FileInfo info, string[] folders)
        {
            bool result = false;
            foreach (string folder in folders)
            {
                result = result || info.FullName.StartsWith(folder);
            }
            return result;
        }

        public static string[] ToArray(this StringCollection collection)
        {
            string[] array = new string[collection.Count];
            collection.CopyTo(array, 0);
            return array;
        }
        public static void CreateShortcut(this FileInfo info, string shortcutFolder, string shortcutName, string description)
        {
            string targetFilePath = info.FullName;

            // Create a new WshShell object
            WshShell shell = new WshShell();

            // Create a shortcut object
            IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut($"{shortcutFolder}\\{shortcutName}.lnk");

            // Set the target file path
            shortcut.TargetPath = targetFilePath;

            // Set the description
            shortcut.Description = description;

            // Optionally set the working directory
            shortcut.WorkingDirectory = Path.GetDirectoryName(targetFilePath);

            // Save the shortcut
            shortcut.Save();
        }
    }
}
