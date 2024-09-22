using IWshRuntimeLibrary;
using Shell32;
using Steam.Models;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Drawing.Imaging;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GamesManager
{
    public static class Utils
    {
        public static IReadOnlyCollection<SteamAppModel> AppList;
        public static readonly string steamGameShortcutPrefix = "steam://rungameid/";
        public static readonly string steamGameIconPrefix = "https://cdn.cloudflare.steamstatic.com/steamcommunity/public/images/apps/";
        public static HttpClient client;

        private static readonly char[] InvalidChars = Path.GetInvalidFileNameChars();

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

        public static string GetUrlFromShortcut(this FileInfo file)
        {
            // Read all lines from the .url file
            var lines = System.IO.File.ReadAllLines(file.FullName);

            // Find the line that starts with "URL="
            var urlLine = lines.FirstOrDefault(line => line.StartsWith("URL="));

            // If the line exists, split it and return the target URL
            if (urlLine != null)
            {
                return urlLine.Substring(4);  // Remove "URL=" to get the actual URL
            }

            return null;  // Return null if no URL was found
        }

        public static void CreateUrlFile(this Game game, string urlFolder)
        {
            if (!game.IsSteamGame)
            {
                return;
            }
            string text = "[{000214A0-0000-0000-C000-000000000046}]\nProp3=19,0\n[InternetShortcut]\nIDList=\nIconIndex=0\nURL=";
            text += steamGameShortcutPrefix;
            text += game.AppID.ToString();
            if (game.IconPath != null)
            {
                text += "\nIconFile=";
                text += game.IconPath;
            }
            if (!urlFolder.EndsWith("\\"))
            {
                urlFolder += "\\";
            }
            System.IO.File.WriteAllText(urlFolder + game.Name.ToSafeFileName() + ".url", text);
        }

        public static string ToSafeFileName(this string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                throw new ArgumentException("File name cannot be null or whitespace.", nameof(fileName));
            }

            // Remove invalid characters
            string safeFileName = Regex.Replace(fileName, $"[{Regex.Escape(new string(InvalidChars))}]", "_");

            // Trim leading or trailing spaces
            safeFileName = safeFileName.Trim();

            // Limit length to avoid file system limits (common max is 255 characters for file names)
            if (safeFileName.Length > 255)
            {
                safeFileName = safeFileName.Substring(0, 255);
            }

            return safeFileName;
        }

        public static SteamAppModel GetSteamAppModel(uint appId)
        {
            return AppList.FirstOrDefault(i => i.AppId == appId);
        }

        public static async Task<string> GetGameIconAsync(uint appId, string imgIconUrl)
        {
            var iconsFolder = Path.Combine(Application.LocalUserAppDataPath, "Icons");
            if (!Directory.Exists(iconsFolder))
            {
                Directory.CreateDirectory(iconsFolder);
            }
            // Construct the full URL to the icon
            string iconUrl = $"{steamGameIconPrefix}{appId}/{imgIconUrl}.jpg";
            string localFilePath = Path.Combine(Application.LocalUserAppDataPath, "Icons", $"{appId}.ico");

            if (System.IO.File.Exists(localFilePath))
            {
                Console.WriteLine($"File already exists: {localFilePath}");
                return localFilePath;
            }

            if (client == null)
            {
                client = new HttpClient();
            }
            try
            {
                Stream iconData = await client.GetStreamAsync(iconUrl);

                Image image = Image.FromStream(iconData);

                ConvertToIco(image, localFilePath, image.Width);

                Console.WriteLine($"Icon saved to: {localFilePath}");
                return localFilePath;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error downloading icon: {ex.Message}");
                return null;
            }
        }

        public static void ConvertToIco(Image img, string file, int size)
        {
            Icon icon;
            using (var msImg = new MemoryStream())
            using (var msIco = new MemoryStream())
            {
                img.Save(msImg, ImageFormat.Png);
                using (var bw = new BinaryWriter(msIco))
                {
                    bw.Write((short)0);           //0-1 reserved
                    bw.Write((short)1);           //2-3 image type, 1 = icon, 2 = cursor
                    bw.Write((short)1);           //4-5 number of images
                    bw.Write((byte)size);         //6 image width
                    bw.Write((byte)size);         //7 image height
                    bw.Write((byte)0);            //8 number of colors
                    bw.Write((byte)0);            //9 reserved
                    bw.Write((short)0);           //10-11 color planes
                    bw.Write((short)32);          //12-13 bits per pixel
                    bw.Write((int)msImg.Length);  //14-17 size of image data
                    bw.Write(22);                 //18-21 offset of image data
                    bw.Write(msImg.ToArray());    // write image data
                    bw.Flush();
                    bw.Seek(0, SeekOrigin.Begin);
                    icon = new Icon(msIco);
                }
            }
            using (var fs = new FileStream(file, FileMode.Create, FileAccess.Write))
            {
                icon.Save(fs);
            }
        }
    }
}
