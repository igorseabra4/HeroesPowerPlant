using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using Newtonsoft.Json;
using SharpDX;

namespace HeroesPowerPlant.Shared.IO.Config
{
    /// <summary>
    /// Stores an individual Heroes Power Plant project configuration (paths to recently opened files, etc.)
    /// </summary>
    public class ProjectConfig
    {
        public bool IsShadow { get; set; }
        public string StageConfigPath { get; set; }
        public string LevelEditorPath { get; set; }
        public string VisibilityPath { get; set; }
        public string CollisionEditorPath { get; set; }
        public string LayoutEditorPath { get; set; }
        public string CameraEditorPath { get; set; }
        public string ParticleEditorPath { get; set; }
        public string TexturePatternEditorPath { get; set; }
        public string LightEditorPath { get; set; }
        public string SetIdTableEditorPath { get; set; }
        public bool LightEditorIsShadow { get; set; }
        public bool SetIdTableEditorIsShadow { get; set; }
        public HashSet<string> DFFONEPaths { get; set; }
        public HashSet<string> TXDPaths { get; set; }
        public HashSet<string> TextureFolderPaths { get; set; }
        public Camera CameraSettings { get; set; }
        public RenderOptions RenderingOptions { get; set; }

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
            public float Speed { get; set; } = 20F;
            public float FieldOfView { get; set; } = 45;
            public float DrawDistance { get; set; } = 500000F;
        }

        public class RenderOptions
        {
            public float QuadtreeHeight { get; set; }
            public bool NoCulling { get; set; }
            public bool Wireframe { get; set; }
            public bool ShowStartPos { get; set; }
            public bool ShowSplines { get; set; }
            public bool RenderByChunk { get; set; }
            public bool ShowChunkBoxes { get; set; }
            public bool ShowCollision { get; set; }
            public bool ShowQuadtree { get; set; }
            public CheckState ShowObjects { get; set; }
            public bool ShowCameras { get; set; }
            public Vector4 BackgroundColor { get; set; }
            public Vector4 SelectionColor  { get; set; }
        }

        /*
            ------------
            Constructors
            ------------
        */
        private ProjectConfig() { }

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
        public static ProjectConfig Open(string filePath)
        {
            string fileText = File.ReadAllText(filePath);
            HPPConfig.GetInstance().LastProjectPath = filePath;
            return JsonConvert.DeserializeObject<ProjectConfig>(fileText);
        }

        public static void Save(ProjectConfig config, string filePath)
        {
            string fileText = JsonConvert.SerializeObject(config, Formatting.Indented);
            File.WriteAllText(filePath, fileText);
            HPPConfig.GetInstance().LastProjectPath = filePath;
        }

        /// <summary>
        /// Creates a ProjectConfig based on the currently opened files in each of the editors.
        /// </summary>
        public static ProjectConfig FromCurrentInstance(SharpRenderer renderer)
        {
            return new ProjectConfig
            {
                // Get info from existing editors.
                IsShadow = Program.LevelEditor.isShadowMode,
                LevelEditorPath = Program.LevelEditor.GetOpenONEFilePath(),
                StageConfigPath = Program.ConfigEditor.GetOpenConfigFileName(),
                VisibilityPath = Program.LevelEditor.OpenVisibilityFile,
                CollisionEditorPath = Program.CollisionEditor.GetFileName(),
                LayoutEditorPath = Program.LayoutEditor.GetOpenFileName(),
                CameraEditorPath = Program.CameraEditor.CurrentCameraFile,
                ParticleEditorPath = Program.ParticleEditor.GetCurrentlyOpenParticleFile(),
                TexturePatternEditorPath = Program.TexturePatternEditor.GetCurrentlyOpenTXC(),
                LightEditorPath = Program.LightEditor.GetCurrentlyOpenLightFile(),
                SetIdTableEditorPath = Program.SetIdTableEditor.GetCurrentFileName(),
                LightEditorIsShadow = Program.LightEditor.GetIsShadow(),
                SetIdTableEditorIsShadow = Program.SetIdTableEditor.GetIsShadow(),
                TXDPaths = TextureManager.OpenTXDfiles,
                TextureFolderPaths = TextureManager.OpenTextureFolders,

                DFFONEPaths = DFFRenderer.filePaths,

                CameraSettings = new Camera()
                {
                    CameraPosition = renderer.Camera.ViewMatrix.Position,
                    Pitch = renderer.Camera.ViewMatrix.Pitch,
                    Speed = renderer.Camera.Speed,
                    Yaw   = renderer.Camera.ViewMatrix.Yaw,
                    FieldOfView = renderer.Camera.ProjectionMatrix.FieldOfView,
                    DrawDistance = renderer.Camera.ProjectionMatrix.FarPlane
                },

                RenderingOptions = new RenderOptions()
                {
                    QuadtreeHeight = (float)Program.ViewConfig.NumericQuadHeight.Value,
                    NoCulling = renderer.Device.GetCullMode() == SharpDX.Direct3D11.CullMode.None,
                    Wireframe = renderer.Device.GetFillMode() == SharpDX.Direct3D11.FillMode.Wireframe,
                    ShowStartPos = renderer.ShowStartPositions,
                    ShowSplines = renderer.ShowSplines,
                    RenderByChunk = BSPRenderer.renderByChunk,
                    ShowChunkBoxes = renderer.ShowChunkBoxes,
                    ShowCollision = renderer.ShowCollision,
                    ShowQuadtree = renderer.ShowQuadtree,
                    ShowObjects = renderer.ShowObjects,
                    ShowCameras = renderer.ShowCameras,
                    BackgroundColor = renderer.backgroundColor,
                    SelectionColor = renderer.selectedColor,
                }
            };
        }

        /// <summary>
        /// Loads the appropriate paths stored in the <see cref="ProjectConfig"/> instance into each of the editors.
        /// </summary>
        public static void ApplyInstance(SharpRenderer renderer, ProjectConfig config)
        {
            ExecuteIfFilePresent($"Config Editor error: file not found: {config.StageConfigPath}", "Error", config.StageConfigPath, path => Program.ConfigEditor.OpenFile(path));

            if (config.IsShadow)
            {
                if (Directory.Exists(config.LevelEditorPath))
                    Program.LevelEditor.OpenONEShadowFolder(config.LevelEditorPath);
                else if (!string.IsNullOrEmpty(config.LevelEditorPath))
                    MessageBox.Show($"Level Editor error: file not found: {config.LevelEditorPath}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                ExecuteIfFilePresent($"Level Editor error: file not found: {config.LevelEditorPath}", "Error", config.LevelEditorPath, path => Program.LevelEditor.OpenONEHeroesFile(path));
                ExecuteIfFilePresent($"Visibility Editor error: file not found: {config.VisibilityPath}", "Error", config.VisibilityPath, path => Program.LevelEditor.initVisibilityEditor(false, path));
            }

            ExecuteIfFilePresent($"Collision Editor error: file not found: {config.CollisionEditorPath}", "Error", config.CollisionEditorPath, path => Program.CollisionEditor.OpenFile(path));
            ExecuteIfFilePresent($"Layout Editor error: file not found: {config.LayoutEditorPath}", "Error", config.LayoutEditorPath, path => Program.LayoutEditor.OpenFile(path));
            ExecuteIfFilePresent($"Camera Editor error: file not found: {config.CameraEditorPath}", "Error", config.CameraEditorPath, path => Program.CameraEditor.OpenFile(path));
            ExecuteIfFilePresent($"Particle Editor error: file not found: {config.ParticleEditorPath}", "Error", config.ParticleEditorPath, path => Program.ParticleEditor.OpenFile(path));
            ExecuteIfFilePresent($"Texture Pattern Editor error: file not found: {config.TexturePatternEditorPath}", "Error", config.TexturePatternEditorPath, path => Program.TexturePatternEditor.OpenFile(path));
            ExecuteIfFilePresent($"Light Editor error: file not found: {config.LightEditorPath}", "Error", config.LightEditorPath, path => Program.LightEditor.OpenFile(path, config.LightEditorIsShadow));
            ExecuteIfFilePresent($"SET ID Table Editor error: file not found: {config.SetIdTableEditorPath}", "Error", config.SetIdTableEditorPath, path => Program.SetIdTableEditor.OpenExternal(path, config.SetIdTableEditorIsShadow));

            if (config.CameraSettings != null)
                renderer.Camera.ApplyConfig(config.CameraSettings);

            TextureManager.ClearTextures();
            if (config.TXDPaths != null)
                foreach (string s in config.TXDPaths)
                    ExecuteIfFilePresent($"Error: TXD file not found: {s}", "Error", s, path => TextureManager.LoadTexturesFromTXD(s));

            if (config.TextureFolderPaths != null)
                foreach (string s in config.TextureFolderPaths)
                    ExecuteIfFilePresent($"Error: Folder not found: {s}", "Error", s, path => TextureManager.LoadTexturesFromFolder(s));

            DFFRenderer.ClearObjectONEFiles();
            if (config.DFFONEPaths != null)
                DFFRenderer.AddDFFFiles(config.DFFONEPaths);

            if (config.RenderingOptions != null)
            {
                Program.MainForm.ApplyConfig(config.RenderingOptions);
                Program.ViewConfig.NumericQuadHeight.Value = (decimal)config.RenderingOptions.QuadtreeHeight;
            }
        }

        /// <summary>
        /// Checks if a file exists, and if it does, executes the <see cref="executeIfPresentDelegate"/>
        /// </summary>
        private static void ExecuteIfFilePresent(string messageBoxMessage, string caption, string filePath, ExecuteWithFilePath executeIfPresentDelegate)
        {
            if (File.Exists(filePath))
                executeIfPresentDelegate(filePath);
            else
                if (! String.IsNullOrEmpty(filePath)) // Do not throw errors on paths that are simply not set.
                    MessageBox.Show(messageBoxMessage, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
