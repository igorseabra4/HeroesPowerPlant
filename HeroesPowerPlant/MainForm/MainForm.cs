using HeroesPowerPlant.Shared.IO.Config;
using Ookii.Dialogs.WinForms;
using ShadowFNT;
using SharpDX;
using SharpDX.Direct3D11;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HeroesPowerPlant.MainForm
{
    public partial class MainForm : Form
    {
        public AboutBox AboutBox;
        public ViewConfig ViewConfig;
        public ConfigEditor.ConfigEditor ConfigEditor;
        public LevelEditor.LevelEditor LevelEditor;
        public Dictionary<ToolStripDropDownItem, CollisionEditor.CollisionEditor> CollisionEditorDict;
        public Dictionary<ToolStripDropDownItem, LayoutEditor.LayoutEditor> LayoutEditorDict;
        public CameraEditor.CameraEditor CameraEditor;
        public ShadowCameraEditor.ShadowCameraEditor ShadowCameraEditor;
        public ShadowLayoutDiffTool.ShadowLayoutDiffTool ShadowLayoutDiffTool;
        public ParticleEditor.ParticleMenu ParticleEditor;
        public TexturePatternEditor.TexturePatternEditor TexturePatternEditor;
        public LightEditor.LightMenu LightEditor;
        public SetIdTableEditor.SetIdTableEditor SetIdTableEditor;

        public SharpRenderer renderer;
        public List<string> dffsToLoad = new List<string>();
        public string currentShadowLevelRoot = "";

        public List<CollisionEditor.CollisionEditor> CollisionEditors => CollisionEditorDict.Values.ToList();
        public List<LayoutEditor.LayoutEditor> LayoutEditors => LayoutEditorDict.Values.ToList();
        public string locationAFS;
        public FNT loadedFNT;

        public MainForm()
        {
            StartPosition = FormStartPosition.CenterScreen;

            InitializeComponent();

            renderer = new SharpRenderer(renderPanel) { dffRenderer = new DFFRenderer(this) };

            showObjectsGToolStripMenuItem.CheckState = CheckState.Indeterminate;

            AboutBox = new AboutBox();
            ViewConfig = new ViewConfig(renderer.Camera);
            ConfigEditor = new ConfigEditor.ConfigEditor();
            LevelEditor = new LevelEditor.LevelEditor();
            CollisionEditorDict = new Dictionary<ToolStripDropDownItem, CollisionEditor.CollisionEditor>();
            LayoutEditorDict = new Dictionary<ToolStripDropDownItem, LayoutEditor.LayoutEditor>();
            CameraEditor = new CameraEditor.CameraEditor();
            ShadowCameraEditor = new ShadowCameraEditor.ShadowCameraEditor();
            ShadowLayoutDiffTool = new ShadowLayoutDiffTool.ShadowLayoutDiffTool();
            ParticleEditor = new ParticleEditor.ParticleMenu();
            TexturePatternEditor = new TexturePatternEditor.TexturePatternEditor();
            LightEditor = new LightEditor.LightMenu();
            SetIdTableEditor = new SetIdTableEditor.SetIdTableEditor();

            LayoutEditor.LayoutEditorSystem.SetupLayoutEditorSystem();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            HPPConfig.GetInstance().Load(this);
            Size = HPPConfig.GetInstance().MainWindowSize;
            ResetMouseCenter(sender, e);
        }

        public string currentSavePath;

        private void ToolstripFileOpen(object sender, EventArgs e)
        {
            VistaOpenFileDialog openFile = new VistaOpenFileDialog()
            { Filter = "Power Plant Config File|*.json" };

            if (openFile.ShowDialog() == DialogResult.OK)
            {
                currentSavePath = openFile.FileName;
                try
                {
                    ProjectConfig projectConfig = ProjectConfig.Open(openFile.FileName);
                    ProjectConfig.ApplyInstance(this, projectConfig);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"There was an error opening the project file: {ex.Message}");
                }

                // The ViewConfig screen should refresh in the case of loading new camera values.
                ViewConfig.UpdateValues();
            }
        }

        private void ToolStripFileSave(object sender, EventArgs e)
        {
            if (currentSavePath != null)
            {
                var hppConfig = ProjectConfig.FromCurrentInstance(this);
                ProjectConfig.Save(hppConfig, currentSavePath);
            }
            else
                ToolStripFileSaveAs(null, null);
        }

        private void ToolStripFileSaveAs(object sender, EventArgs e)
        {
            VistaSaveFileDialog openFile = new VistaSaveFileDialog()
            { 
                Filter = "Power Plant Config File|*.json",
                DefaultExt = ".json"
            };

            if (openFile.ShowDialog() == DialogResult.OK)
            {
                currentSavePath = openFile.FileName;
                var hppConfig = ProjectConfig.FromCurrentInstance(this);
                ProjectConfig.Save(hppConfig, currentSavePath);
            }
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var unsavedChanges = this.unsavedChanges;
            if (unsavedChanges.Any(uc => uc.UnsavedChanges))
            {
                var unsaved = new List<string>();
                foreach (var uc in unsavedChanges)
                    if (uc.UnsavedChanges)
                        unsaved.Add(uc.Text);

                var result = MessageBox.Show($"You have unsaved changes on the following editors: {string.Join(", ", unsaved)}. Do you wish to save before closing?", "Unsaved changes", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                if (result == DialogResult.Cancel)
                {
                    return;
                }
                else if (result == DialogResult.Yes)
                {
                    foreach (var uc in unsavedChanges)
                        if (uc.UnsavedChanges)
                            uc.Save();
                }
            }

            ClearConfig();
        }

        public void ClearConfig()
        {
            LevelEditor.New();
            LevelEditor.NewVisibility();
            ConfigEditor.New();
            ClearCollisionEditors();
            ClearLayoutEditors();
            CameraEditor.New();
            ShadowCameraEditor.New();
            ShadowLayoutDiffTool.New();
            ParticleEditor.New();
            TexturePatternEditor.New();
            SetIdTableEditor.New();
            LightEditor.New();
            renderer.dffRenderer.ClearObjectONEFiles();
            TextureManager.ClearTextures(renderer, LevelEditor.bspRenderer);
            renderer.Camera.Reset();
            currentSavePath = null;
            dffsToLoad.Clear();
        }

        public void ApplyConfig(ProjectConfig.RenderOptions renderingOptions)
        {
            noCullingCToolStripMenuItem.Checked = renderingOptions.NoCulling;
            if (noCullingCToolStripMenuItem.Checked)
                renderer.Device.SetNormalCullMode(CullMode.None);
            else
                renderer.Device.SetNormalCullMode(CullMode.Back);

            wireframeFToolStripMenuItem.Checked = renderingOptions.Wireframe;
            if (wireframeFToolStripMenuItem.Checked)
                renderer.Device.SetNormalFillMode(FillMode.Wireframe);
            else
                renderer.Device.SetNormalFillMode(FillMode.Solid);

            renderer.backgroundColor = new Color(
                renderingOptions.BackgroundColor.X,
                renderingOptions.BackgroundColor.Y,
                renderingOptions.BackgroundColor.Z,
                renderingOptions.BackgroundColor.W);

            renderer.selectedColor = new Vector4(
                renderingOptions.SelectionColor.X,
                renderingOptions.SelectionColor.Y,
                renderingOptions.SelectionColor.Z,
                renderer.selectedColor.W);
            renderer.selectedObjectColor = new Vector4(
                renderingOptions.SelectionColor.X,
                renderingOptions.SelectionColor.Y,
                renderingOptions.SelectionColor.Z,
                renderer.selectedObjectColor.W);
            LevelEditor.visibilityFunctions.SetSelectedChunkColor(renderingOptions.SelectionColor);

            startPosToolStripMenuItem.Checked = renderingOptions.ShowStartPos;
            renderer.ShowStartPositions = startPosToolStripMenuItem.Checked;

            splinesToolStripMenuItem.Checked = renderingOptions.ShowSplines;
            renderer.ShowSplines = splinesToolStripMenuItem.Checked;

            renderByChunkToolStripMenuItem.Checked = renderingOptions.RenderByChunk;
            BSPRenderer.renderByChunk = renderByChunkToolStripMenuItem.Checked;

            chunkBoxesToolStripMenuItem.Checked = renderingOptions.ShowChunkBoxes;
            renderer.ShowChunkBoxes = chunkBoxesToolStripMenuItem.Checked;

            showCollisionXToolStripMenuItem.Checked = renderingOptions.ShowCollision;
            renderer.ShowCollision = showCollisionXToolStripMenuItem.Checked;

            showQuadtreeTToolStripMenuItem.Checked = renderingOptions.ShowQuadtree;
            renderer.ShowQuadtree = showQuadtreeTToolStripMenuItem.Checked;

            showObjectsGToolStripMenuItem.CheckState = renderingOptions.ShowObjects;
            renderer.ShowObjects = showObjectsGToolStripMenuItem.CheckState;

            camerasVToolStripMenuItem.Checked = renderingOptions.ShowCameras;
            renderer.ShowCameras = camerasVToolStripMenuItem.Checked;
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox.Show();
            AboutBox.Focus();
            AboutBox.WindowState = FormWindowState.Normal;
        }

        private void modLoaderConfigEditorF2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConfigEditor.Show();
            ConfigEditor.Focus();
            ConfigEditor.WindowState = FormWindowState.Normal;
        }

        private void levelEditorF3ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LevelEditor.Show();
            LevelEditor.Focus();
            LevelEditor.WindowState = FormWindowState.Normal;
        }

        private void newCollisionEditorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddCollisionEditor(show: true);
        }

        public void AddCollisionEditor(string filePath = null, bool show = false)
        {
            ToolStripMenuItem tempMenuItem = new ToolStripMenuItem("No file loaded");
            tempMenuItem.Click += new EventHandler(CollisionEditorToolStripMenuItemClick);
            collisionEditorToolStripMenuItem.DropDownItems.Add(tempMenuItem);

            CollisionEditor.CollisionEditor tempColEditor = new CollisionEditor.CollisionEditor();

            CollisionEditorDict.Add(tempMenuItem, tempColEditor);
            if (show)
            {
                CollisionEditorDict[tempMenuItem].Show();
                CollisionEditorDict[tempMenuItem].Focus();
                CollisionEditorDict[tempMenuItem].WindowState = FormWindowState.Normal;
            }

            if (filePath != null)
                tempColEditor.OpenFile(filePath, this);
        }

        private void CollisionEditorToolStripMenuItemClick(object sender, EventArgs e)
        {
            CollisionEditorDict[sender as ToolStripDropDownItem].Show();
            CollisionEditorDict[sender as ToolStripDropDownItem].Focus();
            CollisionEditorDict[sender as ToolStripDropDownItem].WindowState = FormWindowState.Normal;
        }

        public void SetCollisionEditorStripItemName(CollisionEditor.CollisionEditor sender, string newName)
        {
            foreach (ToolStripDropDownItem t in CollisionEditorDict.Keys)
                if (CollisionEditorDict[t].Equals(sender))
                {
                    t.Text = newName;
                    return;
                }
            throw new Exception("Error renaming collision editor");
        }

        public void CloseCollisionEditor(CollisionEditor.CollisionEditor sender)
        {
            foreach (ToolStripDropDownItem t in CollisionEditorDict.Keys)
                if (CollisionEditorDict[t].Equals(sender))
                {
                    collisionEditorToolStripMenuItem.DropDownItems.Remove(t);
                    CollisionEditorDict[t].Close();
                    CollisionEditorDict.Remove(t);
                    return;
                }
            throw new Exception("Error closing collision editor");
        }

        public void ClearCollisionEditors()
        {
            var list = new List<CollisionEditor.CollisionEditor>();
            list.AddRange(CollisionEditors);

            foreach (var c in list)
                CloseCollisionEditor(c);
        }

        private void newLayoutEditorToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            AddLayoutEditor(show: true);
        }

        public void AddLayoutEditor(string filePath = null, bool show = false, bool visibleObjects = true)
        {
            ToolStripMenuItem tempMenuItem = new ToolStripMenuItem("No file loaded");
            tempMenuItem.Click += new EventHandler(LayoutEditorToolStripMenuItemClick);
            layoutEditorToolStripMenuItem.DropDownItems.Add(tempMenuItem);

            LayoutEditor.LayoutEditor tempLayoutEditor = new LayoutEditor.LayoutEditor();

            LayoutEditorDict.Add(tempMenuItem, tempLayoutEditor);
            if (show)
            {
                LayoutEditorDict[tempMenuItem].Show();
                LayoutEditorDict[tempMenuItem].Focus();
                LayoutEditorDict[tempMenuItem].WindowState = FormWindowState.Normal;
            }

            if (filePath != null)
                tempLayoutEditor.OpenFile(filePath, this, visibleObjects);
        }

        private void LayoutEditorToolStripMenuItemClick(object sender, EventArgs e)
        {
            LayoutEditorDict[sender as ToolStripDropDownItem].Show();
            LayoutEditorDict[sender as ToolStripDropDownItem].Focus();
            LayoutEditorDict[sender as ToolStripDropDownItem].WindowState = FormWindowState.Normal;
        }

        public void SetLayoutEditorStripItemName(LayoutEditor.LayoutEditor sender, string newName)
        {
            foreach (ToolStripDropDownItem t in LayoutEditorDict.Keys)
                if (LayoutEditorDict[t].Equals(sender))
                {
                    t.Text = newName;
                    return;
                }
            throw new Exception("Error renaming layout editor");
        }

        public void CloseLayoutEditor(LayoutEditor.LayoutEditor sender)
        {
            if (sender.UnsavedChanges)
            {
                var result = Extensions.UnsavedChangesMessageBox(sender.Text);
                if (result == DialogResult.Yes)
                {
                    sender.Save();
                    if (sender.UnsavedChanges)
                        return;
                }
                else if (result == DialogResult.Cancel)
                    return;
            }

            foreach (ToolStripDropDownItem t in LayoutEditorDict.Keys)
                if (LayoutEditorDict[t].Equals(sender))
                {
                    layoutEditorToolStripMenuItem.DropDownItems.Remove(t);
                    LayoutEditorDict[t].Close();
                    LayoutEditorDict.Remove(t);
                    return;
                }
            throw new Exception("Error closing layout editor");
        }

        public void ClearLayoutEditors()
        {
            var list = new List<LayoutEditor.LayoutEditor>();
            list.AddRange(LayoutEditors);

            foreach (var c in list)
                CloseLayoutEditor(c);
        }

        private void cameraEditorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CameraEditor.Show();
            CameraEditor.Focus();
            CameraEditor.WindowState = FormWindowState.Normal;
        }

        private void particleEditorF8ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ParticleEditor.Show();
            ParticleEditor.Focus();
            ParticleEditor.WindowState = FormWindowState.Normal;
        }

        private void texturePatternEditorF9ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TexturePatternEditor.Show();
            TexturePatternEditor.Focus();
            TexturePatternEditor.WindowState = FormWindowState.Normal;
        }

        private void sETIDTableEditorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetIdTableEditor.Show();
            SetIdTableEditor.Focus();
            SetIdTableEditor.WindowState = FormWindowState.Normal;
        }

        private void lightEditorF10ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LightEditor.Show();
            LightEditor.Focus();
            LightEditor.WindowState = FormWindowState.Normal;
        }

        public void SetToolStripStatusLabel(string Text)
        {
            toolStripStatusLabel1.Text = Text;
        }

        private void renderPanel_MouseClick(object sender, MouseEventArgs e)
        {
            ScreenClicked(e, false);
        }

        private void renderPanel_MouseDown(object sender, MouseEventArgs e)
        {
            ScreenClicked(e, true);
        }

        private void ScreenClicked(MouseEventArgs e, bool isMouseDown)
        {
            if (e.Button == MouseButtons.Left || e.Button == MouseButtons.Right)
                ScreenClicked(new Rectangle(
                    renderPanel.ClientRectangle.X,
                    renderPanel.ClientRectangle.Y,
                    renderPanel.ClientRectangle.Width,
                    renderPanel.ClientRectangle.Height), e.X, e.Y, isMouseDown,
                    PressedKeys.Contains(Keys.ControlKey),
                    e.Button == MouseButtons.Left,
                    PressedKeys.Contains(Keys.ShiftKey) && e.Button == MouseButtons.Right);
        }

        private void ScreenClicked(Rectangle viewRectangle, int X, int Y, bool isMouseDown, bool isCtrlDown, bool leftClick, bool placeNewObject)
        {
            Ray ray = Ray.GetPickRay(X, Y, new Viewport(viewRectangle), renderer.viewProjection);

            if (!isMouseDown && placeNewObject)
                ScreenClickedPlaceObject(ray);

            else if (leftClick && renderer.MouseModeObjects && renderer.ShowObjects != CheckState.Unchecked)
                ScreenClickedSelectObject(ray, isMouseDown, isCtrlDown);

            else if (leftClick && renderer.ShowCameras && !isMouseDown)
            {
                CameraEditor.ScreenClicked(ray);
                ShadowCameraEditor.ScreenClicked(ray);
            }
        }

        private void ScreenClickedPlaceObject(Ray ray)
        {
            float? distance = null;

            if (renderer.ShowCollision)
            {
                // verify position in collision editors
                foreach (var c in CollisionEditors)
                {
                    c.GetClickedModelPosition(ray, out bool has1, out float dist1);
                    if (has1 && (distance == null || (distance != null && dist1 < distance)))
                        distance = dist1;
                }

                LevelEditor.GetClickedModelPosition(true, ray, out bool has2, out float dist2);
                if (has2 && (distance == null || (distance != null && dist2 < distance)))
                    distance = dist2;
            }
            else
            {
                // verify position in level editor
                LevelEditor.GetClickedModelPosition(false, ray, out bool has3, out float dist3);
                if (has3)
                    distance = dist3;
            }

            // verify position in layout editors
            foreach (var c in LayoutEditors)
            {
                bool seeAllObjects;

                if (renderer.ShowObjects == CheckState.Checked)
                    seeAllObjects = true;
                else if (renderer.ShowObjects == CheckState.Indeterminate)
                    seeAllObjects = false;
                else
                    break;

                c.GetClickedModelPosition(ray, renderer.Camera.GetPosition(), seeAllObjects, out bool has4, out float dist4);

                if (has4 && (distance == null || (distance != null && dist4 < distance)))
                    distance = dist4;
            }

            Vector3 position = ray.Position + Vector3.Normalize(ray.Direction) * (distance == null ? 100f : (float)distance);

            if (renderer.MouseModeObjects)
                foreach (var l in LayoutEditors)
                    l.PlaceObject(position);
            //else
            //    Program.CameraEditor.PlaceObject(position);
        }

        private void ScreenClickedSelectObject(Ray ray, bool isMouseDown, bool isCtrlDown)
        {
            // select object in layout editor
            float minDistance = 40000f;
            int index = -1;
            LayoutEditor.LayoutEditor e = null;

            foreach (var l in LayoutEditors)
            {
                l.ScreenClicked(renderer, ray, isMouseDown, renderer.ShowObjects == CheckState.Checked, out float distance, out int newIndex);
                if (newIndex != -1 && distance < minDistance)
                {
                    index = newIndex;
                    minDistance = distance;
                    e = l;
                }
            }

            if (!isMouseDown)
                foreach (var l in LayoutEditors)
                {
                    if (l.finishedMovingGizmo)
                        l.finishedMovingGizmo = false;
                    else if (l == e)
                        l.SetSelectedIndex(index, isCtrlDown);
                    else if (!isCtrlDown)
                        l.SetSelectedIndex(-1, false);
                }
        }

        public void UnselectEveryoneExceptMe(int hashCode)
        {
            foreach (var l in LayoutEditors)
                if (!l.GetHashCode().Equals(hashCode))
                    l.SetSelectedIndex(-1, false);
        }

        public void OpenShadowLayoutEditors(string fileName, string fileNamePrefix)
        {
            if (LayoutEditors.Any())
            {
                var result = MessageBox.Show("Close currently open Layout Editors?", "Layout Editor Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                    Program.MainForm.ClearLayoutEditors();
            }
            foreach (var s in new[]
            {
                (Path.Combine(fileName, fileNamePrefix) + "_ds1.dat", true),
                (Path.Combine(fileName, fileNamePrefix) + "_cmn.dat", true),
                (Path.Combine(fileName, fileNamePrefix) + "_nrm.dat", true),
                (Path.Combine(fileName, fileNamePrefix) + "_hrd.dat", true)
            })
            if (File.Exists(s.Item1))
                AddLayoutEditor(s.Item1, false, s.Item2);
            foreach (var l in LayoutEditors) // reset selected index to prevent F5 not opening all if initial without object click
                l.SetSelectedIndex(-1, false);

        }

        private void renderPanel_MouseUp(object sender, MouseEventArgs e)
        {
            foreach (var l in LayoutEditors)
                l.ScreenUnclicked();
        }

        private void renderPanel_MouseLeave(object sender, EventArgs e)
        {
            foreach (var l in LayoutEditors)
                l.ScreenUnclicked();
        }

        private bool mouseMode = false;
        private System.Drawing.Point MouseCenter = new System.Drawing.Point();
        private int oldMouseX;
        private int oldMouseY;

        private bool loopNotStarted = true;

        private void MouseMoveControl(object sender, MouseEventArgs e)
        {
            if (mouseMode)
            {
                renderer.Camera.AddYaw((Cursor.Position.X - MouseCenter.X) / 4F);
                renderer.Camera.AddPitch((Cursor.Position.Y - MouseCenter.Y) / 4F);

                Cursor.Position = MouseCenter;
            }
            else
            {
                int deltaX = e.X - oldMouseX;
                int deltaY = e.Y - oldMouseY;
                if (e.Button == MouseButtons.Middle)
                {
                    renderer.Camera.AddYaw(deltaX * renderer.Camera.MouseSensitivity);
                    renderer.Camera.AddPitch(deltaY * renderer.Camera.MouseSensitivity);
                }
                if (e.Button == MouseButtons.Right)
                {
                    // Do not scale with framerate; WinForms events are not scalable with FPS.
                    renderer.Camera.AddPositionSideways(deltaX, false);
                    renderer.Camera.AddPositionUp(deltaY, false);
                }

                foreach (var l in LayoutEditors)
                    l.MouseMoveForPosition(renderer.viewProjection, deltaX, deltaY);
            }

            oldMouseX = e.X;
            oldMouseY = e.Y;

            if (loopNotStarted)
            {
                loopNotStarted = false;
                renderer.RunMainLoop(renderPanel, this);
            }
        }

        private void renderPanel_MouseWheel(object sender, MouseEventArgs e)
        {
            renderer.Camera.AddPositionForward(e.Delta, false);
        }

        private void MouseModeToggle()
        {
            mouseMode = !mouseMode;
        }

        private void ResetMouseCenter(object sender, EventArgs e)
        {
            MouseCenter = renderPanel.PointToScreen(new System.Drawing.Point(renderPanel.Width / 2, renderPanel.Height / 2));
        }

        private HashSet<Keys> PressedKeys = new HashSet<Keys>();

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (!PressedKeys.Contains(e.KeyCode))
                PressedKeys.Add(e.KeyCode);

            switch (e.KeyCode)
            {
                case Keys.Z:
                    MouseModeToggle();
                    break;
                case Keys.C:
                    ToggleCulling();
                    break;
                case Keys.F:
                    ToggleWireFrame();
                    break;
                case Keys.H:
                    ToggleRenderByChunk();
                    break;
                case Keys.B:
                    ToggleChunkBoxes();
                    break;
                case Keys.X:
                    ToggleShowCollision();
                    break;
                case Keys.T:
                    ToggleShowQuadtree();
                    break;
                case Keys.Y:
                    ToggleStartPos();
                    break;
                case Keys.U:
                    ToggleSplines();
                    break;
                case Keys.G:
                    ToggleShowObjects();
                    break;
                case Keys.V:
                    ToggleShowCameras();
                    break;
                case Keys.M:
                    ToggleSelectionMode();
                    break;
                case Keys.F1:
                    ViewConfig.Show();
                    ViewConfig.Focus();
                    ViewConfig.WindowState = FormWindowState.Normal;
                    break;
                case Keys.F2:
                    ConfigEditor.Show();
                    ConfigEditor.Focus();
                    ConfigEditor.WindowState = FormWindowState.Normal;
                    break;
                case Keys.F3:
                    LevelEditor.Show();
                    LevelEditor.Focus();
                    LevelEditor.WindowState = FormWindowState.Normal;
                    break;
                case Keys.F4:
                    if (CollisionEditors.Count == 0)
                        AddCollisionEditor(show: true);
                    else
                        foreach (var c in CollisionEditors)
                        {
                            c.Show();
                            c.Focus();
                            c.WindowState = FormWindowState.Normal;
                        }
                    break;
                case Keys.F5:
                    if (LayoutEditors.Count == 0)
                        AddLayoutEditor(show: true);
                    else
                    {
                        bool opened = false;
                        foreach (var l in LayoutEditors)
                            if (l.HasSelectedObject())
                            {
                                l.Show();
                                l.Focus();
                                l.WindowState = FormWindowState.Normal;
                                opened = true;
                            }
                        if (!opened)
                            foreach (var l in LayoutEditors)
                            {
                                l.Show();
                                l.Focus();
                                l.WindowState = FormWindowState.Normal;
                            }
                    }
                    break;
                case Keys.F6:
                    TeleportPlayerToCamera();
                    break;
                case Keys.F7:
                    CameraEditor.Show();
                    CameraEditor.Focus();
                    CameraEditor.WindowState = FormWindowState.Normal;
                    break;
                case Keys.F8:
                    ParticleEditor.Show();
                    ParticleEditor.Focus();
                    ParticleEditor.WindowState = FormWindowState.Normal;
                    break;
                case Keys.F9:
                    TexturePatternEditor.Show();
                    TexturePatternEditor.Focus();
                    TexturePatternEditor.WindowState = FormWindowState.Normal;
                    break;
                case Keys.F10:
                    LightEditor.Show();
                    LightEditor.Focus();
                    LightEditor.WindowState = FormWindowState.Normal;
                    break;
            }
        }

        private void MainForm_KeyUp(object sender, KeyEventArgs e)
        {
            PressedKeys.Remove(e.KeyCode);
        }

        private void MainForm_Deactivate(object sender, EventArgs e)
        {
            mouseMode = false;
            PressedKeys.Clear();
        }

        public void KeyboardController()
        {
            if (PressedKeys.Contains(Keys.Q))
                renderer.Camera.IncreaseCameraSpeed(-0.05F);
            if (PressedKeys.Contains(Keys.E))
                renderer.Camera.IncreaseCameraSpeed(0.05F);

            if (PressedKeys.Contains(Keys.A) & PressedKeys.Contains(Keys.ControlKey))
                renderer.Camera.AddYaw(-renderer.Camera.KeyboardSensitivity);
            else if (PressedKeys.Contains(Keys.A))
                renderer.Camera.AddPositionSideways(1f);

            if (PressedKeys.Contains(Keys.D) & PressedKeys.Contains(Keys.ControlKey))
                renderer.Camera.AddYaw(renderer.Camera.KeyboardSensitivity);
            else if (PressedKeys.Contains(Keys.D))
                renderer.Camera.AddPositionSideways(-1f);

            if (PressedKeys.Contains(Keys.W) & PressedKeys.Contains(Keys.ControlKey))
                renderer.Camera.AddPitch(-renderer.Camera.KeyboardSensitivity);
            else if (PressedKeys.Contains(Keys.W) & PressedKeys.Contains(Keys.ShiftKey))
                renderer.Camera.AddPositionUp(1f);
            else if (PressedKeys.Contains(Keys.W))
                renderer.Camera.AddPositionForward(1f);

            if (PressedKeys.Contains(Keys.S) & PressedKeys.Contains(Keys.ControlKey))
                renderer.Camera.AddPitch(renderer.Camera.KeyboardSensitivity);
            else if (PressedKeys.Contains(Keys.S) & PressedKeys.Contains(Keys.ShiftKey))
                renderer.Camera.AddPositionUp(-1f);
            else if (PressedKeys.Contains(Keys.S))
                renderer.Camera.AddPositionForward(-1f);

            if (PressedKeys.Contains(Keys.Up))
                renderer.Camera.AddPitch(-renderer.Camera.KeyboardSensitivity);

            if (PressedKeys.Contains(Keys.Down))
                renderer.Camera.AddPitch(renderer.Camera.KeyboardSensitivity);

            if (PressedKeys.Contains(Keys.Left))
                renderer.Camera.AddYaw(-renderer.Camera.KeyboardSensitivity);

            if (PressedKeys.Contains(Keys.Right))
                renderer.Camera.AddYaw(renderer.Camera.KeyboardSensitivity);

            if (PressedKeys.Contains(Keys.R))
                renderer.Camera.Reset();
        }

        private void noCullingCToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToggleCulling();
        }

        public void ToggleCulling()
        {
            noCullingCToolStripMenuItem.Checked = !noCullingCToolStripMenuItem.Checked;
            if (noCullingCToolStripMenuItem.Checked)
                renderer.Device.SetNormalCullMode(CullMode.None);
            else
                renderer.Device.SetNormalCullMode(CullMode.Back);
        }

        private void wireframeFToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToggleWireFrame();
        }

        public void ToggleWireFrame()
        {
            wireframeFToolStripMenuItem.Checked = !wireframeFToolStripMenuItem.Checked;
            if (wireframeFToolStripMenuItem.Checked)
                renderer.Device.SetNormalFillMode(FillMode.Wireframe);
            else
                renderer.Device.SetNormalFillMode(FillMode.Solid);
        }

        private void BackgroundColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog() == DialogResult.OK)
                renderer.backgroundColor = new Color(colorDialog.Color.R, colorDialog.Color.G, colorDialog.Color.B, (byte)(renderer.backgroundColor.Alpha * 255));
        }

        private void selectionColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                renderer.selectedObjectColor = new Vector4(colorDialog.Color.R, colorDialog.Color.G, colorDialog.Color.B, renderer.selectedObjectColor.W);
                renderer.selectedColor = new Vector4(colorDialog.Color.R, colorDialog.Color.G, colorDialog.Color.B, renderer.selectedColor.W);
                LevelEditor.visibilityFunctions.SetSelectedChunkColor(colorDialog.Color);
            }
        }

        private void resetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            renderer.ResetColors();
        }

        private void objectsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            renderer.MouseModeObjects = true;
            objectsToolStripMenuItem.Checked = true;
            camerasToolStripMenuItem.Checked = false;
        }

        private void camerasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            renderer.MouseModeObjects = false;
            camerasToolStripMenuItem.Checked = true;
            objectsToolStripMenuItem.Checked = false;
        }

        private void ToggleSelectionMode()
        {
            if (renderer.MouseModeObjects)
                camerasToolStripMenuItem_Click(null, null);
            else
                objectsToolStripMenuItem_Click(null, null);
        }

        private void startPosYToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToggleStartPos();
        }

        private void ToggleStartPos()
        {
            startPosToolStripMenuItem.Checked = !startPosToolStripMenuItem.Checked;
            renderer.ShowStartPositions = startPosToolStripMenuItem.Checked;
        }

        private void splinesUToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToggleSplines();
        }

        private void ToggleSplines()
        {
            splinesToolStripMenuItem.Checked = !splinesToolStripMenuItem.Checked;
            renderer.ShowSplines = splinesToolStripMenuItem.Checked;
        }

        private void renderByChunkHToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToggleRenderByChunk();
        }

        public void ToggleRenderByChunk()
        {
            renderByChunkToolStripMenuItem.Checked = !renderByChunkToolStripMenuItem.Checked;
            BSPRenderer.renderByChunk = renderByChunkToolStripMenuItem.Checked;
        }

        private void chunkBoxesBToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToggleChunkBoxes();
        }

        private void ToggleChunkBoxes()
        {
            chunkBoxesToolStripMenuItem.Checked = !chunkBoxesToolStripMenuItem.Checked;
            renderer.ShowChunkBoxes = chunkBoxesToolStripMenuItem.Checked;
        }

        private void showCollisionXToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToggleShowCollision();
        }

        private void ToggleShowCollision()
        {
            showCollisionXToolStripMenuItem.Checked = !showCollisionXToolStripMenuItem.Checked;
            renderer.ShowCollision = showCollisionXToolStripMenuItem.Checked;
        }

        private void showQuadtreeTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToggleShowQuadtree();
        }

        private void ToggleShowQuadtree()
        {
            showQuadtreeTToolStripMenuItem.Checked = !showQuadtreeTToolStripMenuItem.Checked;
            renderer.ShowQuadtree = showQuadtreeTToolStripMenuItem.Checked;
        }

        private void showObjectsGToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToggleShowObjects();
        }

        private void ToggleShowObjects()
        {
            if (showObjectsGToolStripMenuItem.CheckState == CheckState.Checked)
                showObjectsGToolStripMenuItem.CheckState = CheckState.Unchecked;

            else if (showObjectsGToolStripMenuItem.CheckState == CheckState.Indeterminate)
                showObjectsGToolStripMenuItem.CheckState = CheckState.Checked;

            else if (showObjectsGToolStripMenuItem.CheckState == CheckState.Unchecked)
                showObjectsGToolStripMenuItem.CheckState = CheckState.Indeterminate;

            renderer.ShowObjects = showObjectsGToolStripMenuItem.CheckState;
        }

        private void camerasVToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToggleShowCameras();
        }

        private void ToggleShowCameras()
        {
            camerasVToolStripMenuItem.Checked = !camerasVToolStripMenuItem.Checked;
            renderer.ShowCameras = camerasVToolStripMenuItem.Checked;
        }

        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {
            ViewConfig.Show();
            ViewConfig.Focus();
            ViewConfig.WindowState = FormWindowState.Normal;
        }

        private IEnumerable<IUnsavedChanges> unsavedChanges => new List<IUnsavedChanges>(LayoutEditors)
        {
            ConfigEditor,
            ConfigEditor.SplineEditor,
            LevelEditor,
            CameraEditor,
            ShadowCameraEditor,
            ParticleEditor,
            TexturePatternEditor,
            LightEditor,
            SetIdTableEditor,
        };
        
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            var unsavedChanges = this.unsavedChanges;
            if (unsavedChanges.Any(uc => uc.UnsavedChanges))
            {
                var unsaved = new List<string>();
                foreach (var uc in unsavedChanges)
                    if (uc.UnsavedChanges)
                        unsaved.Add(uc.Text);

                var result = MessageBox.Show($"You have unsaved changes on the following editors: {string.Join(", ", unsaved)}. Do you wish to save before closing?", "Unsaved changes", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                if (result == DialogResult.Cancel)
                {
                    e.Cancel = true;
                    return;
                }
                else if (result == DialogResult.Yes)
                {
                    foreach (var uc in unsavedChanges)
                        if (uc.UnsavedChanges)
                            uc.Save();
                }
            }

            HPPConfig.GetInstance().Save();
            if (HPPConfig.GetInstance().AutomaticallySaveConfig)
                if (currentSavePath != null)
                {
                    var hppConfig = ProjectConfig.FromCurrentInstance(this);
                    ProjectConfig.Save(hppConfig, currentSavePath);
                }

            //Environment.Exit(0); // Ensure background threads close too!
        }

        public void AfterUpdate()
        {
            Close();
            System.Diagnostics.Process.Start(Application.StartupPath + "\\HeroesPowerPlant.exe");
        }

        private void CheckForUpdatesOnStartupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            checkForUpdatesOnStartupToolStripMenuItem.Checked = !checkForUpdatesOnStartupToolStripMenuItem.Checked;
            HPPConfig.GetInstance().CheckForUpdatesOnStartup = checkForUpdatesOnStartupToolStripMenuItem.Checked;
        }

        public void SetCheckForUpdatesOnStartup(bool value)
        {
            checkForUpdatesOnStartupToolStripMenuItem.Checked = value;
            HPPConfig.GetInstance().CheckForUpdatesOnStartup = value;
        }

        private void CheckForUpdatesNowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (AutomaticUpdater.UpdateHeroesPowerPlant(out bool hasChecked))
            {
                Close();
                System.Diagnostics.Process.Start(Application.StartupPath + "\\HeroesPowerPlant.exe");
            }
            else if (hasChecked)
                MessageBox.Show("No update found.");
        }

        private void vSyncToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (vSyncToolStripMenuItem.Checked)
                DisableVSync();
            else
                EnableVSync();
        }

        public void EnableVSync()
        {
            renderer.dontRender = true;
            vSyncToolStripMenuItem.Checked = true;
            renderer.Device.SetVSync(vSyncToolStripMenuItem.Checked);
            if (!disableRendering_ToolStripMenuItem.Checked)
                renderer.dontRender = false;

            HPPConfig.GetInstance().VSync = true;
        }

        public void DisableVSync()
        {
            renderer.dontRender = true;
            vSyncToolStripMenuItem.Checked = false;
            renderer.Device.SetVSync(vSyncToolStripMenuItem.Checked);
            if (!disableRendering_ToolStripMenuItem.Checked)
                renderer.dontRender = false;

            HPPConfig.GetInstance().VSync = false;
        }

        private void autoLoadLastProjectOnLaunchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetAutoLoadLastProject(!autoLoadLastProjectOnLaunchToolStripMenuItem.Checked);
        }

        public void SetAutoLoadLastProject(bool value)
        {
            autoLoadLastProjectOnLaunchToolStripMenuItem.Checked = value;
            HPPConfig.GetInstance().AutomaticallyLoadLastConfig = value;
        }

        private void autoSaveProjectOnClosingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetAutomaticallySaveConfig(!autoSaveProjectOnClosingToolStripMenuItem.Checked);
        }

        public void SetAutomaticallySaveConfig(bool value)
        {
            autoSaveProjectOnClosingToolStripMenuItem.Checked = value;
            HPPConfig.GetInstance().AutomaticallySaveConfig = value;
        }

        private async void addObjectONEToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            VistaOpenFileDialog openFile = new VistaOpenFileDialog()
            {
                Filter = "ONE Files|*.one",
                Multiselect = true
            };

            if (openFile.ShowDialog() == DialogResult.OK)
                await Task.Run(() => renderer.dffRenderer.AddDFFFiles(openFile.FileNames));
        }

        private void clearObjectONEsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            renderer.dffRenderer.ClearObjectONEFiles();
            foreach (var v in LayoutEditors)
                v.UpdateAllMatrices();
        }

        private void addTXDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            VistaOpenFileDialog openTXD = new VistaOpenFileDialog()
            {
                Filter = "All supported filetypes|*.txd;*.one|TXD files|*.txd|ONE files|*.one",
                Multiselect = true
            };
            if (openTXD.ShowDialog() == DialogResult.OK)
                foreach (var fileName in openTXD.FileNames)
                {
                    if (fileName.ToLower().EndsWith(".one"))
                    {
                        TextureManager.LoadTexturesFromTXD(fileName, renderer, LevelEditor.bspRenderer);
                    } else
                    {
                        TextureManager.SetupTextureDisplay(File.ReadAllBytes(fileName), renderer, LevelEditor.bspRenderer);
                    }
                }
        }

        private void addTextureFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            VistaFolderBrowserDialog openFolder = new VistaFolderBrowserDialog();
            if (openFolder.ShowDialog() == DialogResult.OK)
                TextureManager.LoadTexturesFromFolder(openFolder.SelectedPath, renderer, LevelEditor.bspRenderer);
        }

        private void clearTXDsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TextureManager.ClearTextures(renderer, LevelEditor.bspRenderer);
        }

        public void ReapplyTextures()
        {
            TextureManager.ReapplyTextures(renderer, LevelEditor.bspRenderer);
        }

        public void LoadTexturesFromTXD(byte[] data)
        {
            TextureManager.SetupTextureDisplay(data, renderer, LevelEditor.bspRenderer);
        }

        private void SetAllTopMost(bool value)
        {
            if (!HPPConfig.GetInstance().LegacyWindowPriorityBehavior)
                value = false;

            AboutBox.TopMost = value;
            ViewConfig.TopMost = value;
            ConfigEditor.TopMost = value;
            LevelEditor.TopMost = value;
            foreach (var c in CollisionEditors)
                c.TopMost = value;
            foreach (var l in LayoutEditors)
                l.TopMost = value;
            ConfigEditor.SplineEditor.TopMost = value;
            CameraEditor.TopMost = value;
            ShadowCameraEditor.TopMost = value;
            ParticleEditor.TopMost = value;
            TexturePatternEditor.TopMost = value;
            SetIdTableEditor.TopMost = value;
            LightEditor.TopMost = value;
            ShadowLayoutDiffTool.TopMost = value;

            allTopMost = value;
        }

        bool allTopMost = true;

        private void MainForm_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                if (renderer != null)
                {
                    renderer.dontRender = true;
                    SetAllTopMost(false);
                }
            }
            else
            {
                if (!disableRendering_ToolStripMenuItem.Checked && renderer != null)
                {
                    renderer.dontRender = false;
                    SetAllTopMost(true);
                }
                HPPConfig.GetInstance().MainWindowSize = Size;
            }
        }

        /// <summary>
        /// This is just a shortcut for opening the view menu.
        /// </summary>
        private void cameraViewSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ViewConfig.Show();
            ViewConfig.Focus();
            ViewConfig.WindowState = FormWindowState.Normal;
        }

        public void TeleportPlayerToCamera()
        {
            if (!MemoryFunctions.Teleport(renderer.Camera.GetPosition()))
                MessageBox.Show("Unable to teleport player.");
        }

        private void shadowCameraEditorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //TODO: Fix disposed obj exception
            ShadowCameraEditor.Show();
            ShadowCameraEditor.Focus();
            ShadowCameraEditor.WindowState = FormWindowState.Normal;
        }

        private void disableRendering_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!disableRendering_ToolStripMenuItem.Checked)
            {
                renderer.dontRender = true;
                disableRendering_ToolStripMenuItem.Checked = true;
            }
            else
            {
                renderer.dontRender = false;
                disableRendering_ToolStripMenuItem.Checked = false;
            }
        }

        private void LimitFPS_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!LimitFPS_ToolStripMenuItem.Checked)
                LimitFPS_ToolStripMenuItem.Checked = true;
            else
                LimitFPS_ToolStripMenuItem.Checked = false;
            HPPConfig.GetInstance().LimitFPS = LimitFPS_ToolStripMenuItem.Checked;
            SetMaxFPS();
        }

        public void SetLimitFPS(bool isEnabled, decimal fpsLimit)
        {
            LimitFPS_ToolStripMenuItem.Checked = isEnabled;
            ViewConfig.maxFps_numericUpDown.Value = fpsLimit;
            SetMaxFPS();
        }

        public void SetWindowPriorityBehavior (bool isEnabled)
        {
            LegacyWindowPriorityBehavior_ToolStripMenuItem.Checked = isEnabled;
        }

        public void SetMaxFPS()
        {
            if (LimitFPS_ToolStripMenuItem.Checked)
            {
                renderer.SharpFps.FPSLimit = (float)ViewConfig.maxFps_numericUpDown.Value;
                HPPConfig.GetInstance().LimitFPSValue = ViewConfig.maxFps_numericUpDown.Value;
            }
            else
            {
                renderer.SharpFps.FPSLimit = float.MaxValue;
            }
        }

        private void shadowLayoutDiffToolToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShadowLayoutDiffTool.Show();
            ShadowLayoutDiffTool.Focus();
            ShadowLayoutDiffTool.WindowState = FormWindowState.Normal;
        }

        private void LegacyWindowPriorityBehavior_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!LegacyWindowPriorityBehavior_ToolStripMenuItem.Checked)
                LegacyWindowPriorityBehavior_ToolStripMenuItem.Checked = true;
            else
                LegacyWindowPriorityBehavior_ToolStripMenuItem.Checked = false;
            HPPConfig.GetInstance().LegacyWindowPriorityBehavior = LegacyWindowPriorityBehavior_ToolStripMenuItem.Checked;
            SetAllTopMost(true); // invoke reset
        }

        private void ResourceToolStripMenuItemSetAFS_Click(object sender, EventArgs e)
        {
            using VistaOpenFileDialog openFile = new VistaOpenFileDialog
            {
                Filter = "AFS files (*.afs)|*.afs|All files (*.*)|*.*"
            };
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                SetAFSlocation(openFile.FileName);
            }
        }

        private void ResourceToolStripMenuItemSetFNT_Click(object sender, EventArgs e)
        {
            using VistaOpenFileDialog openFile = new VistaOpenFileDialog()
            {
                Filter = "FNT files (*.fnt)|*.fnt|All files (*.*)|*.*"
            };
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                LoadFNT(openFile.FileName);
            }
        }

        public void AutoLoadFNTAndAFS(string shadowStage)
        {
            SetAFSlocation(currentShadowLevelRoot + "/PRS_VOICE_E.afs");
            var fntFileName = currentShadowLevelRoot + "/fonts/" + shadowStage + "/" + shadowStage + "_EN.fnt";
            LoadFNT(fntFileName);
        }

        private void SetAFSlocation(string fileName)
        {
            locationAFS = fileName;
        }

        private void LoadFNT(string fileName)
        {
            try
            {
                var fntData = File.ReadAllBytes(fileName);
                loadedFNT = FNT.ParseFNTFile(fileName, ref fntData);
            }
            catch (IOException e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public void UpdateLayoutEditorMenus()
        {
            foreach (var le in LayoutEditorDict.Values)
                le.UpdateMenus();
        }

        private void openHeroesLevelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var unsavedChanges = this.unsavedChanges;
            if (unsavedChanges.Any(uc => uc.UnsavedChanges))
            {
                var unsaved = new List<string>();
                foreach (var uc in unsavedChanges)
                    if (uc.UnsavedChanges)
                        unsaved.Add(uc.Text);

                var result = MessageBox.Show($"You have unsaved changes on the following editors: {string.Join(", ", unsaved)}. Do you wish to save before closing?", "Unsaved changes", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                if (result == DialogResult.Cancel)
                {
                    return;
                }
                else if (result == DialogResult.Yes)
                {
                    foreach (var uc in unsavedChanges)
                        if (uc.UnsavedChanges)
                            uc.Save();
                }
            }

            var openFile = new VistaOpenFileDialog()
            {
                Filter = "ONE files|*.ONE",
                Title = "Choose the level's ONE file..."
            };
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                ClearConfig();

                LevelEditor.OpenONEHeroesFile(openFile.FileName, renderer);

                var dvdroot = Path.GetDirectoryName(openFile.FileName);
                var filenamePrefix = LevelEditor.bspRenderer.currentFileNamePrefix;

                string probObjOne = dvdroot + "\\" + filenamePrefix + "obj.one";
                if (File.Exists(probObjOne) && !renderer.dffRenderer.filePaths.Contains(probObjOne))
                    renderer.dffRenderer.AddDFFFiles(new string[] { probObjOne });

                string probComObjOne = dvdroot + "\\comobj.one";
                if (File.Exists(probComObjOne) && !renderer.dffRenderer.filePaths.Contains(probComObjOne))
                    renderer.dffRenderer.AddDFFFiles(new string[] { probComObjOne });

                string probComObjTxd = dvdroot + "\\textures\\obj_common.txd";
                if (File.Exists(probComObjTxd) && !TextureManager.OpenTXDfiles.Contains(probComObjTxd))
                    TextureManager.LoadTexturesFromTXD(probComObjTxd, renderer, LevelEditor.bspRenderer);

                foreach (var s in new (string, bool)[] {
                    (dvdroot + "\\" + filenamePrefix + "_DB.bin", true),
                    (dvdroot + "\\" + filenamePrefix + "_PB.bin", true),
                    (dvdroot + "\\" + filenamePrefix + "_P1.bin", true),
                    (dvdroot + "\\" + filenamePrefix + "_P2.bin", false),
                    (dvdroot + "\\" + filenamePrefix + "_P3.bin", false),
                    (dvdroot + "\\" + filenamePrefix + "_P4.bin", false),
                    (dvdroot + "\\" + filenamePrefix + "_P5.bin", false),
                })
                    if (File.Exists(s.Item1) && !LayoutEditors.Any(l => l.GetOpenFileName().Equals(s.Item1)))
                        AddLayoutEditor(s.Item1, false, s.Item2);

                foreach (var s in new string[] {
                    dvdroot + "\\collisions\\" + filenamePrefix + ".cl",
                    dvdroot + "\\collisions\\" + filenamePrefix + "_wt.cl",
                    dvdroot + "\\collisions\\" + filenamePrefix + "_xx.cl",
                })
                    if (File.Exists(s) && !CollisionEditors.Any(l => l.GetOpenFileName().Equals(s)))
                        AddCollisionEditor(s);

                CameraEditor.OpenFile(dvdroot + "\\" + filenamePrefix + "_cam.bin");

                var ptcl = dvdroot + "\\" + filenamePrefix + "_ptcl.bin";
                if (File.Exists(ptcl))
                    ParticleEditor.OpenFile(ptcl);

                var txc = dvdroot + "\\" + filenamePrefix + ".txc";
                if (File.Exists(txc))
                    TexturePatternEditor.OpenFile(txc);

                var light = dvdroot + "\\" + filenamePrefix + "_light.bin";
                if (File.Exists(light))
                    LightEditor.OpenFile(light, false);

                var setidtbl = dvdroot + "\\setidtbl.bin";
                if (File.Exists(setidtbl))
                    SetIdTableEditor.OpenExternal(setidtbl, false);

                var objectOnes = new HashSet<string>();
                foreach (var l in LayoutEditors)
                    foreach (var s in l.GetObjectsForModels())
                        objectOnes.Add(dvdroot + "\\" + s);
                renderer.dffRenderer.AddDFFFiles(objectOnes);

                var tSonicWin = Path.Combine(Path.GetDirectoryName(dvdroot), "Tsonic_win.exe");
                if (File.Exists(tSonicWin))
                    ConfigEditor.EXEExtract(tSonicWin, filenamePrefix);
            }
        }
    }
}