using System.IO;
using System.Reflection;
using Newtonsoft.Json;

namespace HeroesPowerPlant.Shared.IO.Config
{
    /// <summary>
    /// Stores the configuration for Power Plant.
    /// </summary>
    public class HPPConfig
    {
        [JsonIgnore]
        private static readonly string ConfigPath;
        private static HPPConfig Instance; // This is a singleton.

        public string LastProjectPath { get; set; }
        public bool AutomaticallyLoadLastConfig { get; set; } = true;
        public bool AutomaticallySaveConfig { get; set; } = true;
        public bool CheckForUpdatesOnStartup { get; set; } = true;
        public bool VSync { get; set; } = true;
        
        private HPPConfig() { }

        static HPPConfig()
        {
            ConfigPath = $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\HPPConfig.json";
        }
        
        /// <summary>
        /// Gets the current instance of the Power Plant config.
        /// </summary>
        public static HPPConfig GetInstance() // Technically not a constructor but still a singleton.
        {
            if (Instance == null)
                Instance = new HPPConfig();

            return Instance;
        }

        /// <summary>
        /// Loads the current Power Plant Config. Note: Returns a new instance on successful load, throw the old instance away.
        /// </summary>
        public HPPConfig Load(MainForm.MainForm mainForm)
        {
            if (!File.Exists(ConfigPath))
            {
                Save();
                System.Windows.Forms.MessageBox.Show("It appears this is your first time using Heroes Power Plant.\nIf you haven't yet, please check out readme.md as that file has useful info regarding use of the program.\nThere are also tutorials available on YouTube and Sonic Retro.");
                mainForm.AboutBox.Show();
            }

            string fileText = File.ReadAllText(ConfigPath);
            Instance = JsonConvert.DeserializeObject<HPPConfig>(fileText);
            Instance.ApplyConfig(mainForm);

            return Instance;
        }

        public void Save()
        {
            string fileText = JsonConvert.SerializeObject(this, Formatting.Indented);
            File.WriteAllText(ConfigPath, fileText);
        }

        private void ApplyConfig(MainForm.MainForm mainForm)
        {
            if (CheckForUpdatesOnStartup && AutomaticUpdater.UpdateHeroesPowerPlant(out _))
            {
                mainForm.AfterUpdate();
            }
            else
            {
                if (AutomaticallyLoadLastConfig && File.Exists(LastProjectPath))
                {
                    var config = ProjectConfig.Open(LastProjectPath);
                    ProjectConfig.ApplyInstance(mainForm, config);
                    mainForm.currentSavePath = LastProjectPath;
                }

                if (VSync)
                    mainForm.EnableVSync();
                else
                    mainForm.DisableVSync(); // In case the program default ever changes.

                mainForm.SetCheckForUpdatesOnStartup(CheckForUpdatesOnStartup);
                mainForm.SetAutoLoadLastProject(AutomaticallyLoadLastConfig);
                mainForm.SetAutomaticallySaveConfig(AutomaticallySaveConfig);
            }
        }
    }
}
