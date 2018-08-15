using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using Newtonsoft.Json;
using SharpDX;

namespace HeroesPowerPlant.Shared.IO.HPPConfig
{
    /// <summary>
    /// Stores an individual Heroes Power Plant project configuration (paths to recently opened files, etc.)
    /// </summary>
    public class HPPConfig
    {
        [JsonIgnore]
        private static string LastConfigCopyPath;

        public bool IsShadow { get; set; }
        public string StageConfigPath { get; set; }
        public string LevelEditorPath { get; set; }
        public string VisibilityPath { get; set; }
        public string CollisionEditorPath { get; set; }
        public string LayoutEditorPath { get; set; }
        public string CameraEditorPath { get; set; }
        public HashSet<string> DFFONEPaths { get; set; }
        public Camera CameraSettings { get; set; }

        /*
            -------------
            Camera Config
            -------------
        */
        public class Camera
        {
            public Vector3 CameraPosition { get; set; }
            public float Yaw { get; set; }
            public float Pitch { get; set; }
            public float Speed { get; set; }
        }

        /*
            ------------
            Constructors
            ------------
        */
        private HPPConfig() { }

        static HPPConfig()
        {
            LastConfigCopyPath = $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\LastConfigCopy.json";
        }

        /*
            ---------
            Delegates
            ---------
        */

        public delegate void ExecuteWithFilePath(string filePath);

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
            File.WriteAllText(LastConfigCopyPath, fileText);
        }

        /// <summary>
        /// Automates the config opening and applying process in order to automatically load the last
        /// written user configuration.
        /// </summary>
        public static void LoadLastConfig()
        {
            if (File.Exists(LastConfigCopyPath))
            {
                var config = Open(LastConfigCopyPath);
                ApplyInstance(config);
            }
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
                DFFONEPaths = DFFRenderer.filePaths,
                CameraSettings = new Camera()
                {
                    CameraPosition = SharpRenderer.Camera.GetPosition(),
                    Pitch = SharpRenderer.Camera.Pitch,
                    Speed = SharpRenderer.Camera.Speed,
                    Yaw   = SharpRenderer.Camera.Yaw
                }
            };
        }

        /// <summary>
        /// Loads the appropriate paths stored in the <see cref="HPPConfig"/> instance into each of the editors.
        /// </summary>
        public static void ApplyInstance(HPPConfig config)
        {
            ExecuteIfFilePresent("Config Editor error: file not found.", "Error", config.StageConfigPath, path => Program.ConfigEditor.ConfigEditorOpen(path));

            if (config.IsShadow)
                ExecuteIfFilePresent("Level Editor error: file not found.", "Error", config.LevelEditorPath, path => Program.LevelEditor.OpenONEShadowFolder(path));
            else
            {
                ExecuteIfFilePresent("Level Editor error: file not found.", "Error", config.LevelEditorPath, path => Program.LevelEditor.OpenONEHeroesFile(path));
                ExecuteIfFilePresent("Visibility Editor error: file not found.", "Error", config.VisibilityPath, path => Program.LevelEditor.initVisibilityEditor(false, path));
            }

            ExecuteIfFilePresent("Collision Editor error: file not found.", "Error", config.CollisionEditorPath, path => Program.CollisionEditor.Open(path));
            ExecuteIfFilePresent("Layout Editor error: file not found.", "Error", config.LayoutEditorPath, path => Program.LayoutEditor.OpenLayoutFile(path));
            ExecuteIfFilePresent("Camera Editor error: file not found.", "Error", config.CameraEditorPath, path => Program.CameraEditor.Open(path));

            if (config.CameraSettings != null)
            {
                SharpRenderer.Camera.SetPosition(config.CameraSettings.CameraPosition);
                SharpRenderer.Camera.SetRotation(config.CameraSettings.Pitch, config.CameraSettings.Yaw);
                SharpRenderer.Camera.SetSpeed(config.CameraSettings.Speed);
            }

            DFFRenderer.ClearObjectONEFiles();
            DFFRenderer.AddDFFFiles(config.DFFONEPaths);
        }

        /// <summary>
        /// Checks if a file exists, and if it does, executes the <see cref="executeIfPresentDelegate"/>
        /// </summary>
        private static void ExecuteIfFilePresent(string messageBoxMessage, string caption, string filePath, ExecuteWithFilePath executeIfPresentDelegate)
        {
            if (File.Exists(filePath))
                executeIfPresentDelegate(filePath);
            else
                MessageBox.Show(messageBoxMessage, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
