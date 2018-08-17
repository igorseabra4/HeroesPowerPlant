using System;
using System.Collections.Generic;
using System.Windows.Forms;
using HeroesPowerPlant.Shared.IO.Config;
using SharpDX;
using SharpDX.Direct3D11;

namespace HeroesPowerPlant.MainForm
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            StartPosition = FormStartPosition.CenterScreen;
            
            InitializeComponent();

            showObjectsGToolStripMenuItem.CheckState = CheckState.Indeterminate;

#if DEBUG
            debugToolStripMenuItem.Visible = true;
#else
            debugToolStripMenuItem.Visible = false;
#endif
            new SharpRenderer(renderPanel);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            var hppConfig = HPPConfig.GetInstance();
            hppConfig.Load();
        }

        public string currentSavePath;

        private void ToolstripFileOpen(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog()
            { Filter = "Power Plant Config File|*.json" };

            if (openFile.ShowDialog() == DialogResult.OK)
            {
                currentSavePath = openFile.FileName;
                ProjectConfig projectConfig = ProjectConfig.Open(openFile.FileName);
                ProjectConfig.ApplyInstance(projectConfig);

                // The ViewConfig screen should refresh in the case of loading new camera values.
                Program.ViewConfig.UpdateValues();
            }
        }

        private void ToolStripFileSave(object sender, EventArgs e)
        {
            if (currentSavePath != null)
            {
                var hppConfig = ProjectConfig.FromCurrentInstance();
                ProjectConfig.Save(hppConfig, currentSavePath);

            }  
            else
                ToolStripFileSaveAs(null, null);
        }

        private void ToolStripFileSaveAs(object sender, EventArgs e)
        {
            SaveFileDialog openFile = new SaveFileDialog()
            { Filter = "Power Plant Config File|*.json" };

            if (openFile.ShowDialog() == DialogResult.OK)
            {
                currentSavePath = openFile.FileName;
                var hppConfig = ProjectConfig.FromCurrentInstance();
                ProjectConfig.Save(hppConfig, currentSavePath);
            }
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Warning! This will close the files open in each editor. If you have unsaved changes, they will be lost. Procceed?",
                "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                Program.LevelEditor.New();
                Program.CollisionEditor.CloseFile();
                Program.ConfigEditor.New();
                Program.LayoutEditor.New();
                Program.CameraEditor.New();
                Program.ParticleEditor.New();
                Program.TexturePatternEditor.New();
                DFFRenderer.ClearObjectONEFiles();
                SharpRenderer.Camera.Reset();
            }
        }

        private void addObjectONEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog()
            {
                Filter = "ONE Files|*.one",
                Multiselect = true
            };

            if (openFile.ShowDialog() == DialogResult.OK)
                DFFRenderer.AddDFFFiles(openFile.FileNames);
        }

        private void clearObjectONEsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DFFRenderer.ClearObjectONEFiles();
        }

        public void ApplyConfig(ProjectConfig.RenderOptions renderingOptions)
        {
            noCullingCToolStripMenuItem.Checked = renderingOptions.NoCulling;
            if (noCullingCToolStripMenuItem.Checked)
                SharpRenderer.device.SetNormalCullMode(CullMode.None);
            else
                SharpRenderer.device.SetNormalCullMode(CullMode.Back);

            wireframeFToolStripMenuItem.Checked = renderingOptions.Wireframe;
            if (wireframeFToolStripMenuItem.Checked)
                SharpRenderer.device.SetNormalFillMode(FillMode.Wireframe);
            else
                SharpRenderer.device.SetNormalFillMode(FillMode.Solid);

            SharpRenderer.backgroundColor = new Color(
                renderingOptions.BackgroundColor.X,
                renderingOptions.BackgroundColor.Y,
                renderingOptions.BackgroundColor.Z,
                renderingOptions.BackgroundColor.W);

            SharpRenderer.selectedColor = new Vector4(
                renderingOptions.SelectionColor.X,
                renderingOptions.SelectionColor.Y,
                renderingOptions.SelectionColor.Z,
                SharpRenderer.selectedColor.W);
            SharpRenderer.selectedObjectColor = new Vector4(
                renderingOptions.SelectionColor.X,
                renderingOptions.SelectionColor.Y,
                renderingOptions.SelectionColor.Z,
                SharpRenderer.selectedObjectColor.W);
            LevelEditor.VisibilityFunctions.SetSelectedChunkColor(renderingOptions.SelectionColor);

            startPosToolStripMenuItem.Checked = renderingOptions.ShowStartPos;
            SharpRenderer.ShowStartPositions = startPosToolStripMenuItem.Checked;

            splinesToolStripMenuItem.Checked = renderingOptions.ShowSplines;
            SharpRenderer.ShowSplines = splinesToolStripMenuItem.Checked;

            renderByChunkToolStripMenuItem.Checked = renderingOptions.RenderByChunk;
            BSPRenderer.renderByChunk = renderByChunkToolStripMenuItem.Checked;

            chunkBoxesToolStripMenuItem.Checked = renderingOptions.ShowChunkBoxes;
            SharpRenderer.ShowChunkBoxes = chunkBoxesToolStripMenuItem.Checked;

            showCollisionXToolStripMenuItem.Checked = renderingOptions.ShowCollision;
            SharpRenderer.ShowCollision = showCollisionXToolStripMenuItem.Checked;

            showQuadtreeTToolStripMenuItem.Checked = renderingOptions.ShowQuadtree;
            SharpRenderer.ShowQuadtree = showQuadtreeTToolStripMenuItem.Checked;

            showObjectsGToolStripMenuItem.CheckState = renderingOptions.ShowObjects;
            SharpRenderer.ShowObjects = showObjectsGToolStripMenuItem.CheckState;

            camerasVToolStripMenuItem.Checked = renderingOptions.ShowCameras;
            SharpRenderer.ShowCameras = camerasVToolStripMenuItem.Checked;
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.AboutBox.Show();
        }

        private void modLoaderConfigEditorF2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.ConfigEditor.Show();
        }

        private void levelEditorF3ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.LevelEditor.Show();
        }

        private void collisionEditorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.CollisionEditor.Show();
        }

        private void layoutEditorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.LayoutEditor.Show();
        }

        private void splineEditorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.SplineEditor.Show();
        }

        private void cameraEditorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.CameraEditor.Show();
        }

        private void particleEditorF8ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.ParticleEditor.Show();
        }

        private void texturePatternEditorF9ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.TexturePatternEditor.Show();
        }

        public void EnableSplineEditor()
        {
            splineEditorToolStripMenuItem.Enabled = true;
        }

        public void SetToolStripStatusLabel(string Text)
        {
            toolStripStatusLabel1.Text = Text;
        }
                
        private void renderPanel_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                SharpRenderer.ScreenClicked(new Rectangle(
                    renderPanel.ClientRectangle.X,
                    renderPanel.ClientRectangle.Y,
                    renderPanel.ClientRectangle.Width,
                    renderPanel.ClientRectangle.Height), e.X, e.Y);
            }
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
                SharpRenderer.Camera.AddYaw(MathUtil.DegreesToRadians(Cursor.Position.X - MouseCenter.X) / 4);
                SharpRenderer.Camera.AddPitch(MathUtil.DegreesToRadians(Cursor.Position.Y - MouseCenter.Y) / 4);

                Cursor.Position = MouseCenter;
            }
            else
            {
                if (e.Button == MouseButtons.Middle)
                {
                    SharpRenderer.Camera.AddYaw(MathUtil.DegreesToRadians(e.X - oldMouseX));
                    SharpRenderer.Camera.AddPitch(MathUtil.DegreesToRadians(e.Y - oldMouseY));
                }
                if (e.Button == MouseButtons.Right)
                {
                    SharpRenderer.Camera.AddPositionSideways(e.X - oldMouseX);
                    SharpRenderer.Camera.AddPositionUp(e.Y - oldMouseY);
                }
            }

            oldMouseX = e.X;
            oldMouseY = e.Y;
            
            if (loopNotStarted)
            {
                loopNotStarted = false;
                SharpRenderer.RunMainLoop(renderPanel);
            }
        }

        private void renderPanel_MouseWheel(object sender, MouseEventArgs e)
        {
            SharpRenderer.Camera.AddPositionForward(e.Delta / 12);
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

            if (e.KeyCode == Keys.Z)
                MouseModeToggle();
            else if (e.KeyCode == Keys.Q)
                SharpRenderer.Camera.IncreaseCameraSpeed(-1);
            else if (e.KeyCode == Keys.E)
                SharpRenderer.Camera.IncreaseCameraSpeed(1);
            else if (e.KeyCode == Keys.C)
                ToggleCulling();
            else if (e.KeyCode == Keys.F)
                ToggleWireFrame();
            else if (e.KeyCode == Keys.H)
                ToggleRenderByChunk();
            else if (e.KeyCode == Keys.B)
                ToggleChunkBoxes();
            else if (e.KeyCode == Keys.X)
                ToggleShowCollision();
            else if (e.KeyCode == Keys.T)
                ToggleShowQuadtree();
            else if (e.KeyCode == Keys.Y)
                ToggleStartPos();
            else if (e.KeyCode == Keys.U)
                ToggleSplines();
            else if (e.KeyCode == Keys.G)
                ToggleShowObjects();
            else if (e.KeyCode == Keys.V)
                ToggleShowCameras();
            else if (e.KeyCode == Keys.F1)
                Program.ViewConfig.Show();
            else if (e.KeyCode == Keys.F2)
                Program.ConfigEditor.Show();
            else if (e.KeyCode == Keys.F3)
                Program.LevelEditor.Show();
            else if (e.KeyCode == Keys.F4)
                Program.CollisionEditor.Show();
            else if (e.KeyCode == Keys.F5)
                Program.LayoutEditor.Show();
            else if (e.KeyCode == Keys.F6 & splineEditorToolStripMenuItem.Enabled)
                Program.SplineEditor.Show();
            else if (e.KeyCode == Keys.F7)
                Program.CameraEditor.Show();
            else if (e.KeyCode == Keys.F8)
                Program.ParticleEditor.Show();
            else if (e.KeyCode == Keys.F9)
                Program.TexturePatternEditor.Show();
        }

        private void MainForm_KeyUp(object sender, KeyEventArgs e)
        {
            PressedKeys.Remove(e.KeyCode);
        }

        private void MainForm_Deactivate(object sender, EventArgs e)
        {
            PressedKeys.Clear();
        }

        public void KeyboardController(float speedMultiplier)
        {
            if (PressedKeys.Contains(Keys.A) & PressedKeys.Contains(Keys.ControlKey))
                SharpRenderer.Camera.AddYaw(-0.1f * speedMultiplier);
            else if (PressedKeys.Contains(Keys.A))
                SharpRenderer.Camera.AddPositionSideways(0.5f * speedMultiplier);

            if (PressedKeys.Contains(Keys.D) & PressedKeys.Contains(Keys.ControlKey))
                SharpRenderer.Camera.AddYaw(0.1f * speedMultiplier);
            else if (PressedKeys.Contains(Keys.D))
                SharpRenderer.Camera.AddPositionSideways(-0.5f * speedMultiplier);

            if (PressedKeys.Contains(Keys.W) & PressedKeys.Contains(Keys.ControlKey))
                SharpRenderer.Camera.AddPitch(-0.1f * speedMultiplier);
            else if (PressedKeys.Contains(Keys.W) & PressedKeys.Contains(Keys.ShiftKey))
                SharpRenderer.Camera.AddPositionUp(0.5f * speedMultiplier);
            else if (PressedKeys.Contains(Keys.W))
                SharpRenderer.Camera.AddPositionForward(0.5f * speedMultiplier);

            if (PressedKeys.Contains(Keys.S) & PressedKeys.Contains(Keys.ControlKey))
                SharpRenderer.Camera.AddPitch(0.1f * speedMultiplier);
            else if (PressedKeys.Contains(Keys.S) & PressedKeys.Contains(Keys.ShiftKey))
                SharpRenderer.Camera.AddPositionUp(-0.5f * speedMultiplier);
            else if (PressedKeys.Contains(Keys.S))
                SharpRenderer.Camera.AddPositionForward(-0.5f * speedMultiplier);

            if (PressedKeys.Contains(Keys.R))
                SharpRenderer.Camera.Reset();
        }

        private void noCullingCToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToggleCulling();
        }

        public void ToggleCulling()
        {
            noCullingCToolStripMenuItem.Checked = !noCullingCToolStripMenuItem.Checked;
            if (noCullingCToolStripMenuItem.Checked)
                SharpRenderer.device.SetNormalCullMode(CullMode.None);
            else
                SharpRenderer.device.SetNormalCullMode(CullMode.Back);
        }

        private void wireframeFToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToggleWireFrame();
        }

        public void ToggleWireFrame()
        {
            wireframeFToolStripMenuItem.Checked = !wireframeFToolStripMenuItem.Checked;
            if (wireframeFToolStripMenuItem.Checked)
                SharpRenderer.device.SetNormalFillMode(FillMode.Wireframe);
            else
                SharpRenderer.device.SetNormalFillMode(FillMode.Solid);
        }

        private void BackgroundColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog() == DialogResult.OK)
                SharpRenderer.backgroundColor = new Color(colorDialog.Color.R, colorDialog.Color.G, colorDialog.Color.B, colorDialog.Color.A);
        }

        private void selectionColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                SharpRenderer.selectedObjectColor = new Vector4(colorDialog.Color.R, colorDialog.Color.G, colorDialog.Color.B, SharpRenderer.selectedObjectColor.W);
                SharpRenderer.selectedColor = new Vector4(colorDialog.Color.R, colorDialog.Color.G, colorDialog.Color.B, SharpRenderer.selectedColor.W);
                LevelEditor.VisibilityFunctions.SetSelectedChunkColor(colorDialog.Color);
            }
        }

        private void resetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SharpRenderer.ResetColors();
        }

        private void objectsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SharpRenderer.MouseModeObjects = true;
            objectsToolStripMenuItem.Checked = true;
            camerasToolStripMenuItem.Checked = false;
        }
        
        private void camerasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SharpRenderer.MouseModeObjects = false;
            camerasToolStripMenuItem.Checked = true;
            objectsToolStripMenuItem.Checked = false;
        }

        private void startPosYToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToggleStartPos();
        }

        private void ToggleStartPos()
        {
            startPosToolStripMenuItem.Checked = !startPosToolStripMenuItem.Checked;
            SharpRenderer.ShowStartPositions = startPosToolStripMenuItem.Checked;
        }

        private void splinesUToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToggleSplines();
        }

        private void ToggleSplines()
        {
            splinesToolStripMenuItem.Checked = !splinesToolStripMenuItem.Checked;
            SharpRenderer.ShowSplines = splinesToolStripMenuItem.Checked;
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
            SharpRenderer.ShowChunkBoxes = chunkBoxesToolStripMenuItem.Checked;
        }

        private void showCollisionXToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToggleShowCollision();
        }

        private void ToggleShowCollision()
        {
            showCollisionXToolStripMenuItem.Checked = !showCollisionXToolStripMenuItem.Checked;
            SharpRenderer.ShowCollision = showCollisionXToolStripMenuItem.Checked;
        }

        private void showQuadtreeTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToggleShowQuadtree();
        }

        private void ToggleShowQuadtree()
        {
            showQuadtreeTToolStripMenuItem.Checked = !showQuadtreeTToolStripMenuItem.Checked;
            SharpRenderer.ShowQuadtree = showQuadtreeTToolStripMenuItem.Checked;
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

            SharpRenderer.ShowObjects = showObjectsGToolStripMenuItem.CheckState;
        }

        private void camerasVToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToggleShowCameras();
        }

        private void ToggleShowCameras()
        {
            camerasVToolStripMenuItem.Checked = !camerasVToolStripMenuItem.Checked;
            SharpRenderer.ShowCameras = camerasVToolStripMenuItem.Checked;
        }

        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {
            Program.ViewConfig.Show();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            HPPConfig.GetInstance().Save();
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
            SharpRenderer.dontRender = true;
            vSyncToolStripMenuItem.Checked = true;
            SharpRenderer.device.SetVSync(vSyncToolStripMenuItem.Checked);
            SharpRenderer.dontRender = false;

            HPPConfig.GetInstance().VSync = true;
        }

        public void DisableVSync()
        {
            SharpRenderer.dontRender = true;
            vSyncToolStripMenuItem.Checked = false;
            SharpRenderer.device.SetVSync(vSyncToolStripMenuItem.Checked);
            SharpRenderer.dontRender = false;

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

        private void reloadTexturesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BSPRenderer.ReloadTextures();
        }
    }
}
