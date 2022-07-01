using Newtonsoft.Json;
using System;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Windows.Forms;

namespace HeroesPowerPlant.Shared.IO.Config
{
    public static class AutomaticUpdater
    {
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
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                    updatedJson = reader.ReadToEnd();

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

                        string updatedIPfilePath = Application.StartupPath + "\\Resources\\" + updatedIPfileName;

                        using (var webClient = new WebClient())
                            webClient.DownloadFile(updatedIPURL, updatedIPfilePath);

                        string oldPath = Application.StartupPath + "\\HeroesPowerPlant_old\\";

                        if (!Directory.Exists(oldPath))
                            Directory.CreateDirectory(oldPath);

                        foreach (string s in new string[]
                        {
                                "",
                                "\\ref",
                                "\\Resources",
                                "\\Resources\\Lists",
                                "\\Resources\\Models",
                                "\\Resources\\SharpDX",
                                "\\runtimes",
                                "\\runtimes\\linux-x64",
                                "\\runtimes\\linux-x64\\native",
                                "\\runtimes\\osx-x64",
                                "\\runtimes\\osx-x64\\native",
                                "\\runtimes\\win",
                                "\\runtimes\\win\\lib",
                                "\\runtimes\\win\\lib\\netcoreapp2.0",
                                "\\runtimes\\win\\native",
                                "\\runtimes\\win-x64",
                                "\\runtimes\\win-x64\\native",
                                "\\runtimes\\win-x86",
                                "\\runtimes\\win-x86\\native",
                                "\\Tools",
                                "\\Tools\\Reloaded Generic Stage Injection Mod",
                                "\\Tools\\txdgen_1.0",
                                "\\Tools\\txdgen_1.0\\LICENSES",
                                "\\Tools\\txdgen_1.0\\LICENSES\\eirrepo",
                                "\\Tools\\txdgen_1.0\\LICENSES\\libimagequant",
                                "\\Tools\\txdgen_1.0\\LICENSES\\libjpeg",
                                "\\Tools\\txdgen_1.0\\LICENSES\\libpng",
                                "\\Tools\\txdgen_1.0\\LICENSES\\libsquish",
                                "\\Tools\\txdgen_1.0\\LICENSES\\lzo-2.08",
                                "\\Tools\\txdgen_1.0\\LICENSES\\pvrtextool",
                                "\\Tools\\txdgen_1.0\\LICENSES\\rwtools",
                        })
                        {
                            if (!Directory.Exists(oldPath + s))
                                Directory.CreateDirectory(oldPath + s);

                            if (Directory.Exists(Application.StartupPath + s))
                                foreach (string s2 in Directory.GetFiles(Application.StartupPath + s))
                                {
                                    if (Path.GetExtension(s2).ToLower().Equals(".zip") || Path.GetFileNameWithoutExtension(s2).Equals("HPPConfig.json"))
                                        continue;

                                    string newFilePath = oldPath + s + "\\" + Path.GetFileName(s2);

                                    if (File.Exists(newFilePath))
                                        File.Delete(newFilePath);

                                    File.Move(s2, newFilePath);
                                }
                        }

                        ZipFile.ExtractToDirectory(updatedIPfilePath, Application.StartupPath);

                        File.Delete(updatedIPfilePath);

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
    }
}
