using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Windows.Forms;

namespace HeroesPowerPlant.Shared.IO.Config
{
    public static class AutomaticUpdater
    {
        private const string OldFolderName = "HeroesPowerPlant_old";

        private static readonly string[] KnownFolders =
        {
            "",
            "ref",
            "Resources",
            "Resources\\Lists",
            "Resources\\Models",
            "Resources\\SharpDX",
            "runtimes",
            "runtimes\\linux-x64",
            "runtimes\\linux-x64\\native",
            "runtimes\\osx-x64",
            "runtimes\\osx-x64\\native",
            "runtimes\\win",
            "runtimes\\win\\lib",
            "runtimes\\win\\lib\\netcoreapp2.0",
            "runtimes\\win\\native",
            "runtimes\\win-x64",
            "runtimes\\win-x64\\native",
            "runtimes\\win-x86",
            "runtimes\\win-x86\\native",
            "Tools",
            "Tools\\Reloaded Generic Stage Injection Mod",
            "Tools\\txdgen_1.0",
            "Tools\\txdgen_1.0\\LICENSES",
            "Tools\\txdgen_1.0\\LICENSES\\eirrepo",
            "Tools\\txdgen_1.0\\LICENSES\\libimagequant",
            "Tools\\txdgen_1.0\\LICENSES\\libjpeg",
            "Tools\\txdgen_1.0\\LICENSES\\libpng",
            "Tools\\txdgen_1.0\\LICENSES\\libsquish",
            "Tools\\txdgen_1.0\\LICENSES\\lzo-2.08",
            "Tools\\txdgen_1.0\\LICENSES\\pvrtextool",
            "Tools\\txdgen_1.0\\LICENSES\\rwtools",
        };

        private static readonly HashSet<string> PreservedFiles = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            "HPPConfig.json",
        };

        public static bool UpdateHeroesPowerPlant(out bool hasChecked)
        {
            hasChecked = false;

            try
            {
                string versionInfoURL = "https://raw.githubusercontent.com/igorseabra4/HeroesPowerPlant/master/HeroesPowerPlant/Resources/hpp_version.json";

                string updatedJson;

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(versionInfoURL);
                request.AutomaticDecompression = DecompressionMethods.GZip;
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                    {
                        updatedJson = reader.ReadToEnd();
                    }
                }

                HPPVersion updatedVersion = JsonConvert.DeserializeObject<HPPVersion>(updatedJson);
                HPPVersion oldVersion = new HPPVersion();

                hasChecked = true;

                if (oldVersion.version != updatedVersion.version)
                {
                    string messageText = "There is an update available for Heroes Power Plant: \n\n" + updatedVersion.versionName + "\n\nDo you wish to download it?";
                    DialogResult d = MessageBox.Show(messageText, "Update Available", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                    if (d == DialogResult.Yes)
                    {
                        string updatedIPfileName = "HeroesPowerPlant_" + updatedVersion.version + ".zip";
                        string updatedIPURL = "https://github.com/igorseabra4/HeroesPowerPlant/releases/download/" + updatedVersion.version + "/" + updatedIPfileName;

                        string updatedIPfilePath = Path.Combine(Path.GetTempPath(), updatedIPfileName);

                        using (var webClient = new WebClient())
                        {
                            webClient.DownloadFile(updatedIPURL, updatedIPfilePath);
                        }

                        try
                        {
                            InstallUpdate(updatedIPfilePath, Application.StartupPath);
                        }
                        finally
                        {
                            File.Delete(updatedIPfilePath);
                        }

                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("There was an error checking for updates: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            return false;
        }

        private static void InstallUpdate(string zipPath, string installPath)
        {
            string oldPath = Path.Combine(installPath, OldFolderName);
            Directory.CreateDirectory(oldPath);

            var folders = new HashSet<string>(KnownFolders, StringComparer.OrdinalIgnoreCase);

            using (ZipArchive archive = ZipFile.OpenRead(zipPath))
            {
                foreach (ZipArchiveEntry entry in archive.Entries)
                {
                    string dir = Path.GetDirectoryName(entry.FullName.Replace('/', '\\')) ?? "";
                    while (true)
                    {
                        folders.Add(dir);
                        if (dir.Length == 0)
                        {
                            break;
                        }
                        dir = Path.GetDirectoryName(dir) ?? "";
                    }
                }
            }

            foreach (string folder in folders)
            {
                string currentDir = Path.Combine(installPath, folder);
                if (!Directory.Exists(currentDir))
                {
                    continue;
                }

                string backupDir = Path.Combine(oldPath, folder);
                Directory.CreateDirectory(backupDir);

                foreach (string file in Directory.GetFiles(currentDir))
                {
                    if (folder.Length == 0 && PreservedFiles.Contains(Path.GetFileName(file)))
                    {
                        continue;
                    }

                    File.Move(file, Path.Combine(backupDir, Path.GetFileName(file)), true);
                }
            }

            ZipFile.ExtractToDirectory(zipPath, installPath, true);
        }
    }
}
