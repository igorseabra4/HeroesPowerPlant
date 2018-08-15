using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace HeroesPowerPlant.Shared.IO.HPPConfig
{
    /// <summary>
    /// Stores an individual Heroes Power Plant project configuration (paths to recently opened files, etc.)
    /// </summary>
    public class HPPConfig
    {
        public bool IsShadow { get; set; }
        public string StageConfigPath { get; set; }
        public string LevelEditorPath { get; set; }
        public string VisibilityPath { get; set; }
        public string CollisionEditorPath { get; set; }
        public string LayoutEditorPath { get; set; }
        public string CameraEditorPath { get; set; }
        public List<string> DFFONEPaths { get; set; }


        /*
            ------------
            Constructors
            ------------
        */
        private HPPConfig() { }

        /*
            -------
            Methods
            -------
        */
        public static HPPConfig Open(string filePath)
        {
            string fileText = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<HPPConfig>(fileText);
        }

        public static void Save(HPPConfig config, string filePath)
        {
            string fileText = JsonConvert.SerializeObject(config, Formatting.Indented);
            File.WriteAllText(filePath, fileText);
        }

        /// <summary>
        /// Creates a HPPConfig based on the currently opened files in each of the editors.
        /// </summary>
        public static HPPConfig FromCurrentInstance()
        {
            HPPConfig config = new HPPConfig();

            // Get info from existing editors.
            config.IsShadow = Program.LevelEditor.isShadowMode;

            config.LevelEditorPath = Program.LevelEditor.GetOpenONEFilePath();
            config.StageConfigPath = Program.ConfigEditor.OpenConfigFileName;
            config.VisibilityPath  = Program.LevelEditor.OpenVisibilityFile;
            config.CollisionEditorPath = Program.CollisionEditor.GetFileName();
            config.LayoutEditorPath = Program.LayoutEditor.GetOpenFileName();
            config.CameraEditorPath = Program.CameraEditor.CurrentCameraFile;
            config.DFFONEPaths = DFFRenderer.filePaths;

            return config;
        }

        /// <summary>
        /// Loads the appropriate paths stored in the <see cref="HPPConfig"/> instance into each of the editors.
        /// </summary>
        public static void ApplyInstance(HPPConfig config)
        {
            if (config.IsShadow)
                Program.LevelEditor.OpenONEShadowFolder(config.LevelEditorPath);
            else 
                Program.LevelEditor.OpenONEHeroesFile(config.LevelEditorPath);

            Program.ConfigEditor.ConfigEditorOpen(config.StageConfigPath);
            Program.LevelEditor.initVisibilityEditor(false, config.VisibilityPath);
            Program.CollisionEditor.Open(config.CollisionEditorPath);
            Program.LayoutEditor.OpenLayoutFile(config.LayoutEditorPath);
            Program.CameraEditor.Open(config.CameraEditorPath);

            DFFRenderer.filePaths.AddRange(config.DFFONEPaths);
            DFFRenderer.AddDFFFiles(config.DFFONEPaths.ToArray());
        }
    }
}
