using System;
using System.Collections.Generic;
using System.Windows.Forms;
using HeroesPowerPlant.Shared.IO.Config;
using Microsoft.WindowsAPICodePack.Dialogs;
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
            
            LostFocus += new EventHandler(MainForm_LostFocus);
            GotFocus += new EventHandler(MainForm_GotFocus);

            renderer = new SharpRenderer(renderPanel);
        }

        public SharpRenderer renderer;

        private void MainForm_Load(object sender, EventArgs e)
        {
            var hppConfig = HPPConfig.GetInstance();
            hppConfig.Load(renderer);
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
                ProjectConfig.ApplyInstance(renderer, projectConfig);

                // The ViewConfig screen should refresh in the case of loading new camera values.
                Program.ViewConfig.UpdateValues();
            }
        }

        private void ToolStripFileSave(object sender, EventArgs e)
        {
            if (currentSavePath != null)
            {
                var hppConfig = ProjectConfig.FromCurrentInstance(renderer);
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
                var hppConfig = ProjectConfig.FromCurrentInstance(renderer);
                ProjectConfig.Save(hppConfig, currentSavePath);
            }
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Warning! This will close the files open in each editor. If you have unsaved changes, they will be lost. Your project file will also not be saved. Proceed?",
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
                TextureManager.ClearTextures();
                renderer.Camera.Reset();
                currentSavePath = null;
            }
        }
        
        public void ApplyConfig(ProjectConfig.RenderOptions renderingOptions)
        {
            noCullingCToolStripMenuItem.Checked = renderingOptions.NoCulling;
            if (noCullingCToolStripMenuItem.Checked)
                renderer.device.SetNormalCullMode(CullMode.None);
            else
                renderer.device.SetNormalCullMode(CullMode.Back);

            wireframeFToolStripMenuItem.Checked = renderingOptions.Wireframe;
            if (wireframeFToolStripMenuItem.Checked)
                renderer.device.SetNormalFillMode(FillMode.Wireframe);
            else
                renderer.device.SetNormalFillMode(FillMode.Solid);

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
            LevelEditor.VisibilityFunctions.SetSelectedChunkColor(renderingOptions.SelectionColor);

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
                renderer.ScreenClicked(new Rectangle(
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
                    renderer.Camera.AddPositionSideways(deltaX);
                    renderer.Camera.AddPositionUp(deltaY);
                }

                Program.LayoutEditor.MouseMoveX(renderer.Camera, deltaX);
                Program.LayoutEditor.MouseMoveY(renderer.Camera, deltaY);
            }

            oldMouseX = e.X;
            oldMouseY = e.Y;
            
            if (loopNotStarted)
            {
                loopNotStarted = false;
                renderer.RunMainLoop(renderPanel);
            }
        }

        private void renderPanel_MouseWheel(object sender, MouseEventArgs e)
        {
            renderer.Camera.AddPositionForward(e.Delta);
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
                renderer.Camera.IncreaseCameraSpeed(-0.1F);
            else if (e.KeyCode == Keys.E)
                renderer.Camera.IncreaseCameraSpeed(0.1F);
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

        public void KeyboardController()
        {
            if (PressedKeys.Contains(Keys.A) & PressedKeys.Contains(Keys.ControlKey))
                renderer.Camera.AddYaw(-0.1f);
            else if (PressedKeys.Contains(Keys.A))
                renderer.Camera.AddPositionSideways(1f);

            if (PressedKeys.Contains(Keys.D) & PressedKeys.Contains(Keys.ControlKey))
                renderer.Camera.AddYaw(0.1f);
            else if (PressedKeys.Contains(Keys.D))
                renderer.Camera.AddPositionSideways(-1f);

            if (PressedKeys.Contains(Keys.W) & PressedKeys.Contains(Keys.ControlKey))
                renderer.Camera.AddPitch(-0.1f);
            else if (PressedKeys.Contains(Keys.W) & PressedKeys.Contains(Keys.ShiftKey))
                renderer.Camera.AddPositionUp(1f);
            else if (PressedKeys.Contains(Keys.W))
                renderer.Camera.AddPositionForward(1f);

            if (PressedKeys.Contains(Keys.S) & PressedKeys.Contains(Keys.ControlKey))
                renderer.Camera.AddPitch(0.1f);
            else if (PressedKeys.Contains(Keys.S) & PressedKeys.Contains(Keys.ShiftKey))
                renderer.Camera.AddPositionUp(-1f);
            else if (PressedKeys.Contains(Keys.S))
                renderer.Camera.AddPositionForward(-1f);

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
                renderer.device.SetNormalCullMode(CullMode.None);
            else
                renderer.device.SetNormalCullMode(CullMode.Back);
        }

        private void wireframeFToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToggleWireFrame();
        }

        public void ToggleWireFrame()
        {
            wireframeFToolStripMenuItem.Checked = !wireframeFToolStripMenuItem.Checked;
            if (wireframeFToolStripMenuItem.Checked)
                renderer.device.SetNormalFillMode(FillMode.Wireframe);
            else
                renderer.device.SetNormalFillMode(FillMode.Solid);
        }

        private void BackgroundColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog() == DialogResult.OK)
                renderer.backgroundColor = new Color(colorDialog.Color.R, colorDialog.Color.G, colorDialog.Color.B, colorDialog.Color.A);
        }

        private void selectionColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                renderer.selectedObjectColor = new Vector4(colorDialog.Color.R, colorDialog.Color.G, colorDialog.Color.B, renderer.selectedObjectColor.W);
                renderer.selectedColor = new Vector4(colorDialog.Color.R, colorDialog.Color.G, colorDialog.Color.B, renderer.selectedColor.W);
                LevelEditor.VisibilityFunctions.SetSelectedChunkColor(colorDialog.Color);
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
            Program.ViewConfig.Show();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            HPPConfig.GetInstance().Save();
            if (HPPConfig.GetInstance().AutomaticallySaveConfig)
                if (currentSavePath != null)
                {
                    var hppConfig = ProjectConfig.FromCurrentInstance(renderer);
                    ProjectConfig.Save(hppConfig, currentSavePath);
                }

            Environment.Exit(0); // Ensure background threads close too!
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
            renderer.device.SetVSync(vSyncToolStripMenuItem.Checked);
            renderer.dontRender = false;

            HPPConfig.GetInstance().VSync = true;
        }

        public void DisableVSync()
        {
            renderer.dontRender = true;
            vSyncToolStripMenuItem.Checked = false;
            renderer.device.SetVSync(vSyncToolStripMenuItem.Checked);
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

        private void renderPanel_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                renderer.ScreenClicked(new Rectangle(
                    renderPanel.ClientRectangle.X,
                    renderPanel.ClientRectangle.Y,
                    renderPanel.ClientRectangle.Width,
                    renderPanel.ClientRectangle.Height), e.X, e.Y, true);
            }
        }

        private void renderPanel_MouseUp(object sender, MouseEventArgs e)
        {
            Program.LayoutEditor.ScreenUnclicked();
        }

        private void renderPanel_MouseLeave(object sender, EventArgs e)
        {
            Program.LayoutEditor.ScreenUnclicked();
        }
                
        private void addObjectONEToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog()
            {
                Filter = "ONE Files|*.one",
                Multiselect = true
            };

            if (openFile.ShowDialog() == DialogResult.OK)
                DFFRenderer.AddDFFFiles(openFile.FileNames);
        }

        private void clearObjectONEsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DFFRenderer.ClearObjectONEFiles();
        }

        private void addTXDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openTXD = new OpenFileDialog()
            {
                Filter = "All supported filetypes|*.txd;*.one|TXD files|*.txd|ONE files|*.one"
            };
            if (openTXD.ShowDialog() == DialogResult.OK)
                TextureManager.LoadTexturesFromTXD(openTXD.FileName);
        }

        private void addTextureFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CommonOpenFileDialog openFile = new CommonOpenFileDialog()
            {
                IsFolderPicker = true
            };
            if (openFile.ShowDialog() == CommonFileDialogResult.Ok)
                TextureManager.LoadTexturesFromFolder(openFile.FileName);
        }

        private void clearTXDsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TextureManager.ClearTextures();
        }
        
        private void SetAllTopMost(bool value)
        {
            Program.ViewConfig.TopMost = value;
            Program.ConfigEditor.TopMost = value;
            Program.LevelEditor.TopMost = value;
            Program.CollisionEditor.TopMost = value;
            Program.LayoutEditor.TopMost = value;
            Program.SplineEditor.TopMost = value;
            Program.CameraEditor.TopMost = value;
            Program.ParticleEditor.TopMost = value;
            Program.TexturePatternEditor.TopMost = value;

            allTopMost = value;
        }

        bool allTopMost = true;

        private void MainForm_LostFocus(object sender, EventArgs e)
        {
            if (allTopMost)
                SetAllTopMost(false);
        }

        private void MainForm_GotFocus(object sender, EventArgs e)
        {
            if (!allTopMost)
                SetAllTopMost(true);
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
                renderer.dontRender = true;
            else
                renderer.dontRender = false;
        }
    }
}