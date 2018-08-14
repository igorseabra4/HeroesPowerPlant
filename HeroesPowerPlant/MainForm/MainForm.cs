using SharpDX;
using SharpDX.Direct3D11;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using HeroesPowerPlant.VisualGUI;
using Reloaded_GUI;
using Reloaded_GUI.Styles.Themes;
using Reloaded_GUI.Styles.Themes.ApplyTheme;
using Reloaded_GUI.Utilities.Windows;
using static HeroesPowerPlant.PowerPlantPaths;
using Message = System.Windows.Forms.Message;

namespace HeroesPowerPlant
{
    public partial class MainForm : Form
    {
        #region Reloaded GUI
        private Reloaded_GUI.Styles.Themes.Theme reloadedTheme;

        private void InitReloadedGUI()
        {
            if (reloadedTheme == null)
                reloadedTheme = new Theme();
            
            reloadedTheme.LoadCurrentTheme();
            ApplyTheme.ThemeWindowsForm(this);
        }
        #endregion Reloaded GUI

        #region Resize Hit Test Passthrough
        private ControlParentResizeUtility renderPanelUtility;
        private ControlParentResizeUtility categoryBarUtility;
        private ControlParentResizeUtility statusBarUtility;

        private void SetupResizeHitTestPassthrough()
        {
            renderPanelUtility = new ControlParentResizeUtility(renderPanel);
            categoryBarUtility = new ControlParentResizeUtility(categoryBar_MenuStrip);
            statusBarUtility = new ControlParentResizeUtility(categoryBar_StatusStrip);
        }

        #endregion


        public MainForm()
        {
            StartPosition = FormStartPosition.CenterScreen;
            
            InitializeComponent();

            categoryBar_MenuStrip.Renderer = new MyRenderer();
            showObjectsGToolStripMenuItem.CheckState = CheckState.Indeterminate;

            new SharpRenderer(renderPanel);

            InitReloadedGUI();
            SetupResizeHitTestPassthrough();
        }

        string currentPathsFile;

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            currentPathsFile = open();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (currentPathsFile != null)
                save(currentPathsFile);
            else
                currentPathsFile = saveAs();
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            currentPathsFile = saveAs();
        }

        private void addObjectONEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DFFRenderer.importObjectONEFile();
        }

        private void clearObjectONEsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DFFRenderer.clearObjectONEFiles();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.aboutBox.Show();
        }

        private void modLoaderConfigEditorF2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.configEditor.Show();
        }

        private void levelEditorF3ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.levelEditor.Show();
        }

        private void collisionEditorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.collisionEditor.Show();
        }

        private void layoutEditorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.layoutEditor.Show();
        }

        private void splineEditorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.splineEditor.Show();
        }

        private void cameraEditorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.cameraEditor.Show();
        }

        public void EnableSplineEditor()
        {
            splineEditorToolStripMenuItem.Enabled = true;
        }

        public void SetToolStripStatusLabel(string Text)
        {
            categoryBar_ToolStripStatusLabel.Text = Text;
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

            if (e.KeyCode == Keys.F1)
                Program.viewConfig.Show();
            else if (e.KeyCode == Keys.F2)
                Program.configEditor.Show();
            else if (e.KeyCode == Keys.F3)
                Program.levelEditor.Show();
            else if (e.KeyCode == Keys.F4)
                Program.collisionEditor.Show();
            else if (e.KeyCode == Keys.F5)
                Program.layoutEditor.Show();
            else if (e.KeyCode == Keys.F6 & splineEditorToolStripMenuItem.Enabled)
                Program.splineEditor.Show();
            else if (e.KeyCode == Keys.F7)
                Program.cameraEditor.Show();
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
                SharpRenderer.Camera.AddYaw(-0.1f);
            else if (PressedKeys.Contains(Keys.A))
                SharpRenderer.Camera.AddPositionSideways(0.5f);

            if (PressedKeys.Contains(Keys.D) & PressedKeys.Contains(Keys.ControlKey))
                SharpRenderer.Camera.AddYaw(0.1f);
            else if (PressedKeys.Contains(Keys.D))
                SharpRenderer.Camera.AddPositionSideways(-0.5f);

            if (PressedKeys.Contains(Keys.W) & PressedKeys.Contains(Keys.ControlKey))
                SharpRenderer.Camera.AddPitch(-0.1f);
            else if (PressedKeys.Contains(Keys.W) & PressedKeys.Contains(Keys.ShiftKey))
                SharpRenderer.Camera.AddPositionUp(0.5f);
            else if (PressedKeys.Contains(Keys.W))
                SharpRenderer.Camera.AddPositionForward(0.5f);

            if (PressedKeys.Contains(Keys.S) & PressedKeys.Contains(Keys.ControlKey))
                SharpRenderer.Camera.AddPitch(0.1f);
            else if (PressedKeys.Contains(Keys.S) & PressedKeys.Contains(Keys.ShiftKey))
                SharpRenderer.Camera.AddPositionUp(-0.5f);
            else if (PressedKeys.Contains(Keys.S))
                SharpRenderer.Camera.AddPositionForward(-0.5f);

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
                LevelEditor.VisibilityFunctions.selectedChunkColor = new Vector4(colorDialog.Color.R, colorDialog.Color.G, colorDialog.Color.B, LevelEditor.VisibilityFunctions.selectedChunkColor.W);
            }
        }

        private void objectsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SharpRenderer.SetMouseModeObjects(true);
            objectsToolStripMenuItem.Checked = true;
            camerasToolStripMenuItem.Checked = false;
        }
        
        private void camerasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SharpRenderer.SetMouseModeObjects(false);
            camerasToolStripMenuItem.Checked = true;
            objectsToolStripMenuItem.Checked = false;
        }

        private void startPosYToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToggleStartPos();
        }

        private void ToggleStartPos()
        {
            startPosYToolStripMenuItem.Checked = !startPosYToolStripMenuItem.Checked;
            SharpRenderer.SetShowStartPos(startPosYToolStripMenuItem.Checked);
        }

        private void splinesUToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToggleSplines();
        }

        private void ToggleSplines()
        {
            splinesUToolStripMenuItem.Checked = !splinesUToolStripMenuItem.Checked;
            SharpRenderer.SetSplines(splinesUToolStripMenuItem.Checked);
        }

        private void renderByChunkHToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToggleRenderByChunk();
        }
        
        public void ToggleRenderByChunk()
        {
            renderByChunkHToolStripMenuItem.Checked = !renderByChunkHToolStripMenuItem.Checked;
            BSPRenderer.SetRenderByChunk(renderByChunkHToolStripMenuItem.Checked);
        }
        
        private void chunkBoxesBToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToggleChunkBoxes();
        }

        private void ToggleChunkBoxes()
        {
            chunkBoxesBToolStripMenuItem.Checked = !chunkBoxesBToolStripMenuItem.Checked;
            SharpRenderer.SetChunkBoxes(chunkBoxesBToolStripMenuItem.Checked);
        }

        private void showCollisionXToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToggleShowCollision();
        }

        private void ToggleShowCollision()
        {
            showCollisionXToolStripMenuItem.Checked = !showCollisionXToolStripMenuItem.Checked;
            SharpRenderer.SetShowCollision(showCollisionXToolStripMenuItem.Checked);
        }

        private void showQuadtreeTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToggleShowQuadtree();
        }

        private void ToggleShowQuadtree()
        {
            showQuadtreeTToolStripMenuItem.Checked = !showQuadtreeTToolStripMenuItem.Checked;
            SharpRenderer.SetShowQuadtree(showQuadtreeTToolStripMenuItem.Checked);
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

            SharpRenderer.SetShowObjects(showObjectsGToolStripMenuItem.CheckState);
        }

        private void camerasVToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToggleShowCameras();
        }

        private void ToggleShowCameras()
        {
            camerasVToolStripMenuItem.Checked = !camerasVToolStripMenuItem.Checked;
            SharpRenderer.SetShowCameras(camerasVToolStripMenuItem.Checked);
        }

        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {
            Program.viewConfig.Show();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void graphicsModeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToggleGraphicsMode();
        }

        private void ToggleGraphicsMode()
        {
            SharpRenderer.dontRender = true;
            graphicsModeToolStripMenuItem.Checked = !graphicsModeToolStripMenuItem.Checked;
            SharpRenderer.device.SetGraphicsMode(graphicsModeToolStripMenuItem.Checked);
            SharpRenderer.dontRender = false;
        }

        private void vSyncToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToggleVSync();
        }

        private void ToggleVSync()
        {
            SharpRenderer.dontRender = true;
            vSyncToolStripMenuItem.Checked = !vSyncToolStripMenuItem.Checked;
            SharpRenderer.device.SetVSync(vSyncToolStripMenuItem.Checked);
            SharpRenderer.dontRender = false;
        }

        private void categoryBar_Close_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void categoryBar_StatusStrip_Resize(object sender, EventArgs e)
        {

        }
    }
}
