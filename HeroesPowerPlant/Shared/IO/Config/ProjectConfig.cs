using Newtonsoft.Json;
using SharpDX;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

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
        public List<string> CollisionEditorPaths { get; set; }
        public List<(string, bool)> LayoutEditorPaths { get; set; }
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
            public Vector4 SelectionColor { get; set; }
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
        public static ProjectConfig FromCurrentInstance(MainForm.MainForm mainForm)
        {
            List<string> colEditorPaths = new List<string>();
            foreach (var v in mainForm.CollisionEditors)
                colEditorPaths.Add(v.GetOpenFileName());

            var layoutEditorPaths = new List<(string, bool)>();
            foreach (var v in mainForm.LayoutEditors)
                layoutEditorPaths.Add((v.GetOpenFileName(), v.RenderObjects));

            return new ProjectConfig
            {
                // Get info from existing editors.
                IsShadow = mainForm.LevelEditor.isShadowMode,
                LevelEditorPath = mainForm.LevelEditor.GetOpenONEFilePath(),
                StageConfigPath = mainForm.ConfigEditor.GetOpenConfigFileName(),
                VisibilityPath = mainForm.LevelEditor.visibilityFunctions.OpenVisibilityFile,
                CollisionEditorPaths = colEditorPaths,
                LayoutEditorPaths = layoutEditorPaths,
                CameraEditorPath = mainForm.CameraEditor.CurrentCameraFile,
                ParticleEditorPath = mainForm.ParticleEditor.GetCurrentlyOpenParticleFile(),
                TexturePatternEditorPath = mainForm.TexturePatternEditor.GetCurrentlyOpenTXC(),
                LightEditorPath = mainForm.LightEditor.GetCurrentlyOpenLightFile(),
                SetIdTableEditorPath = mainForm.SetIdTableEditor.GetCurrentFileName(),
                LightEditorIsShadow = mainForm.LightEditor.GetIsShadow(),
                SetIdTableEditorIsShadow = mainForm.SetIdTableEditor.GetIsShadow(),
                TXDPaths = TextureManager.OpenTXDfiles,
                TextureFolderPaths = TextureManager.OpenTextureFolders,

                DFFONEPaths = mainForm.renderer.dffRenderer.filePaths,

                CameraSettings = new Camera()
                {
                    CameraPosition = mainForm.renderer.Camera.ViewMatrix.Position,
                    Pitch = mainForm.renderer.Camera.ViewMatrix.Pitch,
                    Speed = mainForm.renderer.Camera.Speed,
                    Yaw = mainForm.renderer.Camera.ViewMatrix.Yaw,
                    FieldOfView = mainForm.renderer.Camera.ProjectionMatrix.FieldOfView,
                    DrawDistance = mainForm.renderer.Camera.ProjectionMatrix.FarPlane
                },

                RenderingOptions = new RenderOptions()
                {
                    QuadtreeHeight = (float)mainForm.ViewConfig.NumericQuadHeight.Value,
                    NoCulling = mainForm.renderer.Device.GetCullMode() == SharpDX.Direct3D11.CullMode.None,
                    Wireframe = mainForm.renderer.Device.GetFillMode() == SharpDX.Direct3D11.FillMode.Wireframe,
                    ShowStartPos = mainForm.renderer.ShowStartPositions,
                    ShowSplines = mainForm.renderer.ShowSplines,
                    RenderByChunk = BSPRenderer.renderByChunk,
                    ShowChunkBoxes = mainForm.renderer.ShowChunkBoxes,
                    ShowCollision = mainForm.renderer.ShowCollision,
                    ShowQuadtree = mainForm.renderer.ShowQuadtree,
                    ShowObjects = mainForm.renderer.ShowObjects,
                    ShowCameras = mainForm.renderer.ShowCameras,
                    BackgroundColor = mainForm.renderer.backgroundColor,
                    SelectionColor = mainForm.renderer.selectedColor,
                }
            };
        }

        /// <summary>
        /// Loads the appropriate paths stored in the <see cref="ProjectConfig"/> instance into each of the editors.
        /// </summary>
        public static async void ApplyInstance(MainForm.MainForm mainForm, ProjectConfig config)
        {
            mainForm.ClearConfig();

            ExecuteIfFilePresent($"Config Editor error: file not found: {config.StageConfigPath}", "Error", config.StageConfigPath, path => mainForm.ConfigEditor.OpenFile(path, mainForm));

            if (config.IsShadow)
            {
                if (Directory.Exists(config.LevelEditorPath))
                {
                    mainForm.LevelEditor.OpenONEShadowFolder(config.LevelEditorPath, true);
                    await Task.Run(() => Program.MainForm.AutoLoadFNTAndAFS(Path.GetFileName(config.LevelEditorPath)));
                }
                else if (!string.IsNullOrEmpty(config.LevelEditorPath))
                    MessageBox.Show($"Level Editor error: file not found: {config.LevelEditorPath}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                ExecuteIfFilePresent($"Level Editor error: file not found: {config.LevelEditorPath}", "Error", config.LevelEditorPath, path => mainForm.LevelEditor.OpenONEHeroesFile(path, mainForm.renderer));
                ExecuteIfFilePresent($"Visibility Editor error: file not found: {config.VisibilityPath}", "Error", config.VisibilityPath, path => mainForm.LevelEditor.initVisibilityEditor(false, path));
            }

            if (config.CollisionEditorPaths != null)
                foreach (var v in config.CollisionEditorPaths)
                    ExecuteIfFilePresent($"Collision Editor error: file not found: {v}", "Error", v, path => mainForm.AddCollisionEditor(path));
            if (config.LayoutEditorPaths != null)
                foreach (var v in config.LayoutEditorPaths)
                    ExecuteIfFilePresent($"Layout Editor error: file not found: {v}", "Error", v.Item1, path => mainForm.AddLayoutEditor(path, false, v.Item2));

            ExecuteIfFilePresent($"Camera Editor error: file not found: {config.CameraEditorPath}", "Error", config.CameraEditorPath, path => mainForm.CameraEditor.OpenFile(path));
            ExecuteIfFilePresent($"Particle Editor error: file not found: {config.ParticleEditorPath}", "Error", config.ParticleEditorPath, path => mainForm.ParticleEditor.OpenFile(path));
            ExecuteIfFilePresent($"Texture Pattern Editor error: file not found: {config.TexturePatternEditorPath}", "Error", config.TexturePatternEditorPath, path => mainForm.TexturePatternEditor.OpenFile(path));
            ExecuteIfFilePresent($"Light Editor error: file not found: {config.LightEditorPath}", "Error", config.LightEditorPath, path => mainForm.LightEditor.OpenFile(path, config.LightEditorIsShadow));
            ExecuteIfFilePresent($"SET ID Table Editor error: file not found: {config.SetIdTableEditorPath}", "Error", config.SetIdTableEditorPath, path => mainForm.SetIdTableEditor.OpenExternal(path, config.SetIdTableEditorIsShadow));

            if (config.CameraSettings != null)
                mainForm.renderer.Camera.ApplyConfig(config.CameraSettings);

            TextureManager.ClearTextures(mainForm.renderer, mainForm.LevelEditor.bspRenderer);
            if (config.TXDPaths != null)
                foreach (string s in config.TXDPaths)
                    ExecuteIfFilePresent($"Error: TXD file not found: {s}", "Error", s, path => TextureManager.LoadTexturesFromTXD(s, mainForm.renderer, mainForm.LevelEditor.bspRenderer));

            if (config.TextureFolderPaths != null)
                foreach (string s in config.TextureFolderPaths)
                    ExecuteIfFilePresent($"Error: Folder not found: {s}", "Error", s, path => TextureManager.LoadTexturesFromFolder(s, mainForm.renderer, mainForm.LevelEditor.bspRenderer));

            mainForm.renderer.dffRenderer.ClearObjectONEFiles();
            if (config.DFFONEPaths != null)
                await Task.Run(() => mainForm.renderer.dffRenderer.AddDFFFiles(config.DFFONEPaths));

            if (config.RenderingOptions != null)
            {
                mainForm.ApplyConfig(config.RenderingOptions);
                mainForm.ViewConfig.NumericQuadHeight.Value = (decimal)config.RenderingOptions.QuadtreeHeight;
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
                if (!String.IsNullOrEmpty(filePath)) // Do not throw errors on paths that are simply not set.
                MessageBox.Show(messageBoxMessage, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
