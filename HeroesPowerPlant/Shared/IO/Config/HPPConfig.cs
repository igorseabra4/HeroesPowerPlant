using HeroesPowerPlant.MainForm;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace HeroesPowerPlant.Shared.IO.Config;

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
    public DateTime LastCheckedForUpdate = DateTime.MinValue;
    public bool VSync { get; set; } = true;
    public bool LimitFPS { get; set; } = true;

    public decimal LimitFPSValue { get; set; } = 60.0000m;

    public Size MainWindowSize { get; set; } = new Size(1280, 720);
    public HashSet<(KeyboardAction Action, KeyBinding Binding)> UserKeyBindings = new HashSet<(KeyboardAction Action, KeyBinding Binding)>();

    private HPPConfig() { }

    static HPPConfig()
    {
        ConfigPath = $"{Path.GetDirectoryName(AppContext.BaseDirectory)}\\HPPConfig.json";
    }

    /// <summary>
    /// Gets the current instance of the Power Plant config.
    /// </summary>
    public static HPPConfig GetInstance() // Technically not a constructor but still a singleton.
    {
        return Instance ??= new HPPConfig();
    }

    /// <summary>
    /// Loads the current Power Plant Config. Note: Returns a new instance on successful load, throw the old instance away.
    /// </summary>
    public HPPConfig Load(MainForm.MainForm mainForm)
    {
        if (!File.Exists(ConfigPath))
        {
            Save();
            MessageBox.Show("It appears this is your first time using Heroes Power Plant.\nIf you haven't yet, please check out the Heroes Power Plant wiki on GitHub, as it features a complete guide on using the program and creating custom stages for Sonic Heroes and Shadow the Hedgehog.", "Heroes Power Plant");
            mainForm.HandleAction(KeyboardAction.ViewAbout);
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
        if (CheckForUpdatesOnStartup && DateTime.Now.AddHours(-21) > LastCheckedForUpdate && AutomaticUpdater.UpdateHeroesPowerPlant(out _))
        {
            mainForm.AfterUpdate();
        }
        else
        {
            if (AutomaticallyLoadLastConfig && File.Exists(LastProjectPath))
            {
                try
                {
                    var config = ProjectConfig.Open(LastProjectPath);
                    ProjectConfig.ApplyInstance(mainForm, config);
                    mainForm.currentSavePath = LastProjectPath;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"There was an error opening the project file: {ex.Message}");
                }
            }

            if (VSync)
                mainForm.EnableVSync();
            else
                mainForm.DisableVSync(); // In case the program default ever changes.

            mainForm.SetLimitFPS(LimitFPS, LimitFPSValue);

            mainForm.SetCheckForUpdatesOnStartup(CheckForUpdatesOnStartup);
            mainForm.SetAutoLoadLastProject(AutomaticallyLoadLastConfig);
            mainForm.SetAutomaticallySaveConfig(AutomaticallySaveConfig);
        }
    }
}
