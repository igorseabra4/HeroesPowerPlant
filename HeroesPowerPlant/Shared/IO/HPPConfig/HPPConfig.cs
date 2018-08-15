using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
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
        public HashSet<string> DFFONEPaths { get; set; }

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
            return new HPPConfig
            {
                // Get info from existing editors.
                IsShadow = Program.LevelEditor.isShadowMode,
                LevelEditorPath = Program.LevelEditor.GetOpenONEFilePath(),
                StageConfigPath = Program.ConfigEditor.GetOpenConfigFileName(),
                VisibilityPath = Program.LevelEditor.OpenVisibilityFile,
                CollisionEditorPath = Program.CollisionEditor.GetFileName(),
                LayoutEditorPath = Program.LayoutEditor.GetOpenFileName(),
                CameraEditorPath = Program.CameraEditor.CurrentCameraFile,
                DFFONEPaths = DFFRenderer.filePaths
            };
        }

        /// <summary>
        /// Loads the appropriate paths stored in the <see cref="HPPConfig"/> instance into each of the editors.
        /// </summary>
        public static void ApplyInstance(HPPConfig config)
        {
            if (File.Exists(config.StageConfigPath))
                Program.ConfigEditor.ConfigEditorOpen(config.StageConfigPath);
            else
                MessageBox.Show("Config Editor error: file not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            if (config.IsShadow)
            {
                if (File.Exists(config.LevelEditorPath))
                    Program.LevelEditor.OpenONEShadowFolder(config.LevelEditorPath);
                else
                    MessageBox.Show("Level Editor error: file not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (File.Exists(config.LevelEditorPath))
                    Program.LevelEditor.OpenONEHeroesFile(config.LevelEditorPath);
                else
                    MessageBox.Show("Level Editor error: file not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                if (File.Exists(config.VisibilityPath))
                    Program.LevelEditor.initVisibilityEditor(false, config.VisibilityPath);
                else
                    MessageBox.Show("Visibility Editor error: file not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
            if (File.Exists(config.CollisionEditorPath))
                Program.CollisionEditor.Open(config.CollisionEditorPath);
            else
                MessageBox.Show("Collision Editor error: file not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            if (File.Exists(config.LayoutEditorPath))
                Program.LayoutEditor.OpenLayoutFile(config.LayoutEditorPath);
            else
                MessageBox.Show("Layout Editor error: file not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            if (File.Exists(config.CameraEditorPath))
                Program.CameraEditor.Open(config.CameraEditorPath);
            else
                MessageBox.Show("Camera Editor error: file not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            DFFRenderer.clearObjectONEFiles();
            DFFRenderer.AddDFFFiles(config.DFFONEPaths);
        }
    }
}
