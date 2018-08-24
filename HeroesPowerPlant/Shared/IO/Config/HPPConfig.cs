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
        public bool   AutomaticallyLoadLastConfig { get; set; } = true;
        public bool   AutomaticallySaveConfig { get; set; } = true;
        public bool   VSync { get; set; } = true;

        /*
            ------------
            Constructors
            ------------
        */

        private HPPConfig() { }

        static HPPConfig()
        {
            ConfigPath = $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\HPPConfig.json";
        }

        /*
            -------
            Methods
            -------
        */

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
        public HPPConfig Load()
        {
            if (!File.Exists(ConfigPath))
                Save();

            string fileText = File.ReadAllText(ConfigPath);
            Instance = JsonConvert.DeserializeObject<HPPConfig>(fileText);
            Instance.ApplyConfig();

            return Instance;
        }

        public void Save()
        {
            string fileText = JsonConvert.SerializeObject(this, Formatting.Indented);
            File.WriteAllText(ConfigPath, fileText);
        }

        private void ApplyConfig()
        {
            if (AutomaticallyLoadLastConfig)
            {
                if (File.Exists(LastProjectPath))
                {
                    var config = ProjectConfig.Open(LastProjectPath);
                    ProjectConfig.ApplyInstance(config);
                    Program.MainForm.currentSavePath = LastProjectPath;
                }
            }

            if (VSync)
                Program.MainForm.EnableVSync();
            else
                Program.MainForm.DisableVSync(); // In case the program default ever changes.

            Program.MainForm.SetAutoLoadLastProject(AutomaticallyLoadLastConfig);
            Program.MainForm.SetAutomaticallySaveConfig(AutomaticallySaveConfig);
        }
    }
}
