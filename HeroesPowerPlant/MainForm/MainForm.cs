using HeroesPowerPlant.Other;
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

namespace HeroesPowerPlant.MainForm;

public partial class MainForm : Form
{
    public AboutBox AboutBox;
    public ViewControls ViewControls;
    public ViewConfig ViewConfig;
    public ConfigEditor.ConfigEditor ConfigEditor;
    public LevelEditor.LevelEditor LevelEditor;
    public Dictionary<ToolStripDropDownItem, CollisionEditor.CollisionEditor> CollisionEditorDict;
    public Dictionary<ToolStripDropDownItem, LayoutEditor.LayoutEditor> LayoutEditorDict;
    public CameraEditor.CameraEditor CameraEditor;
    public ShadowCameraEditor.ShadowCameraEditor ShadowCameraEditor;
    public ShadowLayoutMiscTools.ShadowLayoutMiscTools ShadowLayoutMiscTools;
    public ParticleEditor.ParticleMenu ParticleEditor;
    public TexturePatternEditor.TexturePatternEditor TexturePatternEditor;
    public ShadowTexturePatternEditor.ShadowTexturePatternEditor ShadowTexturePatternEditor;
    public LightEditor.LightMenu LightEditor;
    public SetIdTableEditor.SetIdTableEditor SetIdTableEditor;

    public SharpRenderer renderer;
    public List<string> dffsToLoad = new List<string>();
    public string currentShadowLevelRoot = "";
    public Dictionary<string, Nukkoro2Stage> shadowNukkoro2;

    public List<CollisionEditor.CollisionEditor> CollisionEditors => CollisionEditorDict.Values.ToList();
    public List<LayoutEditor.LayoutEditor> LayoutEditors => LayoutEditorDict.Values.ToList();
    public string locationAFS;
    public FNT loadedFNT;

    public MainForm()
    {
        StartPosition = FormStartPosition.CenterScreen;

        InitializeComponent();
        Text += " " + HPPVersion.Current;

        renderer = new SharpRenderer(renderPanel) { dffRenderer = new DFFRenderer(this) };

        showObjectsGToolStripMenuItem.CheckState = CheckState.Indeterminate;

        AboutBox = new AboutBox();
        ViewControls = new ViewControls(this);
        ViewConfig = new ViewConfig(renderer.Camera);
        ConfigEditor = new ConfigEditor.ConfigEditor();
        LevelEditor = new LevelEditor.LevelEditor();
        CollisionEditorDict = new Dictionary<ToolStripDropDownItem, CollisionEditor.CollisionEditor>();
        LayoutEditorDict = new Dictionary<ToolStripDropDownItem, LayoutEditor.LayoutEditor>();
        CameraEditor = new CameraEditor.CameraEditor();
        ShadowCameraEditor = new ShadowCameraEditor.ShadowCameraEditor();
        ShadowLayoutMiscTools = new ShadowLayoutMiscTools.ShadowLayoutMiscTools();
        ParticleEditor = new ParticleEditor.ParticleMenu();
        TexturePatternEditor = new TexturePatternEditor.TexturePatternEditor();
        ShadowTexturePatternEditor = new ShadowTexturePatternEditor.ShadowTexturePatternEditor();
        LightEditor = new LightEditor.LightMenu();
        SetIdTableEditor = new SetIdTableEditor.SetIdTableEditor();

        LayoutEditor.LayoutEditorSystem.SetupLayoutEditorSystem();
    }

    private void MainForm_Load(object sender, EventArgs e)
    {
        HPPConfig.GetInstance().Load(this);
        Size = HPPConfig.GetInstance().MainWindowSize;
        ResetMouseCenter(sender, e);
        if (UserKeyBindings.Count == 0)
            ResetKeyBinds();
    }

    public void ShowForm(Form form)
    {
        if (!form.Visible)
            form.Show(this);
        form.WindowState = FormWindowState.Normal;
        form.Activate();
    }

    public string currentSavePath;

    private void openProjectToolStripMenuItem_Click(object sender, EventArgs e)
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

    private void saveProjectToolStripMenuItem_Click(object sender, EventArgs e)
    {
        if (currentSavePath != null)
        {
            var hppConfig = ProjectConfig.FromCurrentInstance(this);
            ProjectConfig.Save(hppConfig, currentSavePath);
        }
        else
            saveProjectAsToolStripMenuItem_Click(null, null);
    }

    private void saveProjectAsToolStripMenuItem_Click(object sender, EventArgs e)
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

    private void closeProjectToolStripMenuItem_Click(object sender, EventArgs e)
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
        ShadowLayoutMiscTools.New();
        ParticleEditor.New();
        TexturePatternEditor.New();
        ShadowTexturePatternEditor.New();
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
        ShowForm(AboutBox);
    }

    private void modLoaderConfigEditorF2ToolStripMenuItem_Click(object sender, EventArgs e)
    {
        ShowForm(ConfigEditor);
    }

    private void levelEditorToolStripMenuItem_Click(object sender, EventArgs e)
    {
        ShowForm(LevelEditor);
    }

    private void newCollisionEditorToolStripMenuItem_Click(object sender, EventArgs e)
    {
        AddCollisionEditor(show: true);
    }

    public void AddCollisionEditor(string filePath = null, bool show = false)
    {
        var collisionEditor = new CollisionEditor.CollisionEditor();

        var colEditorMenuItem = new ToolStripMenuItem("No file loaded");
        colEditorMenuItem.Click += (sender, e) => ShowForm(collisionEditor);
        collisionEditorToolStripMenuItem.DropDownItems.Add(colEditorMenuItem);

        CollisionEditorDict.Add(colEditorMenuItem, collisionEditor);
        if (show)
            ShowForm(collisionEditor);

        if (filePath != null)
            collisionEditor.OpenFile(filePath, this);
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

        LayoutEditor.LayoutEditor layoutEditor = new LayoutEditor.LayoutEditor();

        LayoutEditorDict.Add(tempMenuItem, layoutEditor);
        if (show)
            ShowForm(layoutEditor);

        if (filePath != null)
            layoutEditor.OpenFile(filePath, this, visibleObjects);
    }

    private void LayoutEditorToolStripMenuItemClick(object sender, EventArgs e)
    {
        ShowForm(LayoutEditorDict[sender as ToolStripDropDownItem]);
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
        ShowForm(CameraEditor);
    }

    private void particleEditorToolStripMenuItem_Click(object sender, EventArgs e)
    {
        ShowForm(ParticleEditor);
    }

    private void texturePatternEditorF9ToolStripMenuItem_Click(object sender, EventArgs e)
    {
        ShowForm(TexturePatternEditor);
    }

    private void shadowTexturePatternEditorToolStripMenuItem_Click(object sender, EventArgs e)
    {
        ShowForm(ShadowTexturePatternEditor);
    }

    private void sETIDTableEditorToolStripMenuItem_Click(object sender, EventArgs e)
    {
        ShowForm(SetIdTableEditor);
    }

    private void lightEditorToolStripMenuItem_Click(object sender, EventArgs e)
    {
        ShowForm(LightEditor);
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

    private void renderPanel_MouseMove(object sender, MouseEventArgs e)
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
                if (PressedKeys.Contains(Keys.ControlKey))
                {
                    renderer.Camera.AddPositionForward(-deltaY * renderer.Camera.MouseSensitivity);
                }
                else if (PressedKeys.Contains(Keys.ShiftKey) && !PressedKeys.Contains(Keys.W) && !PressedKeys.Contains(Keys.A) && !PressedKeys.Contains(Keys.S) && !PressedKeys.Contains(Keys.D))
                {
                    renderer.Camera.AddPositionSideways(deltaX * renderer.Camera.MouseSensitivity);
                    renderer.Camera.AddPositionUp(deltaY * renderer.Camera.MouseSensitivity);
                }
                else
                {
                    renderer.Camera.AddYaw(deltaX * renderer.Camera.MouseSensitivity);
                    renderer.Camera.AddPitch(deltaY * renderer.Camera.MouseSensitivity);
                }
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
        PressedKeys.Add(e.KeyCode);

        if (e.Handled)
            return;

        Keys key = e.KeyCode;
        Keys modifiers = e.Modifiers;

        var bindings = PresetKeyBindings.Concat(UserKeyBindings).Where(x => x.Binding.Key == key && x.Binding.Modifiers == modifiers);
        if (bindings.Any())
        {
            HandleAction(bindings.First().Action);
            e.Handled = true;
        }
        else if (PressedKeys.Contains(Keys.ControlKey))
        {
            const int numberKeyOffset = 49;

            foreach (var item in PressedKeys)
            {
                if ((int)item >= numberKeyOffset
                    && (int)item <= numberKeyOffset + 8
                    && (int)item - numberKeyOffset < LayoutEditors.Count)
                {
                    ShowForm(LayoutEditors[(int)item - numberKeyOffset]);
                    e.Handled = true;
                    return;
                }
            }
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

    private readonly HashSet<(KeyboardAction Action, KeyBinding Binding)> PresetKeyBindings = PresetBindings.ToHashSet();

    private HashSet<(KeyboardAction Action, KeyBinding Binding)> UserKeyBindings => HPPConfig.GetInstance().UserKeyBindings;

    public KeyBinding GetKeyBind(KeyboardAction editorAction)
    {
        IEnumerable<(KeyboardAction Action, KeyBinding Binding)> all = [.. PresetKeyBindings, .. UserKeyBindings];
        return all.Where(b => b.Action == editorAction).Select(b => b.Binding).FirstOrDefault();
    }

    public bool IsKeyBindPreset(KeyboardAction editorAction) => PresetKeyBindings.Any(b => b.Action == editorAction);

    public void SetKeyBind(KeyboardAction editorAction, KeyBinding binding, bool displayError)
    {
        var existing = PresetKeyBindings.Where(x => x.Binding.Key == binding.Key && x.Binding.Modifiers == binding.Modifiers);

        if (existing.Any())
        {
            if (displayError)
                MessageBox.Show($"The binding {binding} is already assigned and cannot be changed.", "Key Binding Conflict", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        UserKeyBindings.RemoveWhere(x => x.Action == editorAction || (x.Binding.Key == binding.Key && x.Binding.Modifiers == binding.Modifiers));
        UserKeyBindings.Add((editorAction, new KeyBinding(binding.Key, binding.Modifiers)));
    }

    public void ResetKeyBind(KeyboardAction editorAction)
    {
        RemoveKeyBind(editorAction);
        foreach (var binding in ModernBindings.Where(b => b.Action == editorAction))
            UserKeyBindings.Add(binding);
    }

    public void RemoveKeyBind(KeyboardAction editorAction)
    {
        UserKeyBindings.RemoveWhere(x => x.Action == editorAction);
    }

    public void ResetKeyBinds()
    {
        UserKeyBindings.Clear();
        foreach (var binding in ModernBindings)
            UserKeyBindings.Add(binding);
    }

    public void HandleAction(KeyboardAction action)
    {
        switch (action)
        {
            case KeyboardAction.ToggleMouseMode:
                MouseModeToggle();
                break;
            case KeyboardAction.ToggleCulling:
                ToggleCulling();
                break;
            case KeyboardAction.ToggleWireframe:
                ToggleWireFrame();
                break;
            case KeyboardAction.ToggleRenderByChunk:
                ToggleRenderByChunk();
                break;
            case KeyboardAction.ToggleChunkBoxes:
                ToggleChunkBoxes();
                break;
            case KeyboardAction.ToggleCollision:
                ToggleShowCollision();
                break;
            case KeyboardAction.ToggleQuadtree:
                ToggleShowQuadtree();
                break;
            case KeyboardAction.ToggleStartPos:
                ToggleStartPos();
                break;
            case KeyboardAction.ToggleSplines:
                ToggleSplines();
                break;
            case KeyboardAction.ToggleShowObjects:
                ToggleShowObjects();
                break;
            case KeyboardAction.ToggleShowCameras:
                ToggleShowCameras();
                break;
            case KeyboardAction.ToggleSelectionMode:
                ToggleSelectionMode();
                break;
            case KeyboardAction.ShowViewControls:
                ShowForm(ViewControls);
                break;
            case KeyboardAction.ShowViewConfig:
                ShowForm(ViewConfig);
                break;
            case KeyboardAction.ShowModLoaderConfigEditor:
                ShowForm(ConfigEditor);
                break;
            case KeyboardAction.ShowLevelEditor:
                ShowForm(LevelEditor);
                break;
            case KeyboardAction.ShowCollisionEditor:
                if (CollisionEditors.Count == 0)
                    AddCollisionEditor(show: true);
                else
                    foreach (var c in CollisionEditors)
                        ShowForm(c);
                break;
            case KeyboardAction.ShowLayoutEditor:
                if (LayoutEditors.Count == 0)
                    AddLayoutEditor(show: true);
                else
                {
                    var toOpen = LayoutEditors.FirstOrDefault(l => l.HasSelectedObject());
                    if (toOpen != null)
                        ShowForm(toOpen);
                    else
                        foreach (var l in LayoutEditors)
                            ShowForm(l);
                }
                break;
            case KeyboardAction.TeleportPlayerToCamera:
                TeleportPlayerToCamera();
                break;
            case KeyboardAction.ShowCameraEditor:
                ShowForm((currentShadowLevelRoot != "") ? ShadowCameraEditor : CameraEditor);
                break;
            case KeyboardAction.ShowParticleEditor:
                ShowForm(ParticleEditor);
                break;
            case KeyboardAction.ShowTexturePatternEditor:
                ShowForm((currentShadowLevelRoot != "") ? ShadowTexturePatternEditor : TexturePatternEditor);
                break;
            case KeyboardAction.ShowLightEditor:
                ShowForm(LightEditor);
                break;
            case KeyboardAction.ShowSetIdTableEditor:
                ShowForm(SetIdTableEditor);
                break;
            case KeyboardAction.ResetCamera:
                renderer.Camera.Reset();
                if (shadowNukkoro2 != null && shadowNukkoro2.Count > 0)
                {
                    try
                    {
                        var shadowSpawnPosition = shadowNukkoro2[Nukkoro2.Nukkoro2StageIdentifiers[LevelEditor.bspRenderer.currentShadowFolderNamePrefix]].Player[0].Position;
                        renderer.Camera.SetPosition(new Vector3(shadowSpawnPosition.X, shadowSpawnPosition.Y, shadowSpawnPosition.Z));
                    }
                    catch
                    {
                        // ignore
                    }
                }
                break;
            case KeyboardAction.ViewAbout:
                ShowForm(AboutBox);
                break;
            case KeyboardAction.IncreaseCameraSpeed:
                renderer.Camera.ModifyCameraSpeed(0.05F);
                break;
            case KeyboardAction.DecreaseCameraSpeed:
                renderer.Camera.ModifyCameraSpeed(-0.05F);
                break;
            case KeyboardAction.OpenLevel:
                openHeroesLevelToolStripMenuItem.PerformClick();
                break;
        }
    }

    public static readonly HashSet<(KeyboardAction Action, KeyBinding Binding)> PresetBindings = new()
    {
        (KeyboardAction.OpenLevel, new KeyBinding(Keys.O, Keys.Control)),

        (KeyboardAction.None, new KeyBinding(Keys.W)),
        (KeyboardAction.None, new KeyBinding(Keys.A)),
        (KeyboardAction.None, new KeyBinding(Keys.S)),
        (KeyboardAction.None, new KeyBinding(Keys.D)),
        (KeyboardAction.None, new KeyBinding(Keys.E)),
        (KeyboardAction.None, new KeyBinding(Keys.Q)),
        (KeyboardAction.None, new KeyBinding(Keys.W, Keys.Control)),
        (KeyboardAction.None, new KeyBinding(Keys.A, Keys.Control)),
        (KeyboardAction.None, new KeyBinding(Keys.S, Keys.Control)),
        (KeyboardAction.None, new KeyBinding(Keys.D, Keys.Control)),
        (KeyboardAction.None, new KeyBinding(Keys.W, Keys.Shift)),
        (KeyboardAction.None, new KeyBinding(Keys.A, Keys.Shift)),
        (KeyboardAction.None, new KeyBinding(Keys.S, Keys.Shift)),
        (KeyboardAction.None, new KeyBinding(Keys.D, Keys.Shift)),
    };

    public static HashSet<(KeyboardAction Action, KeyBinding Binding)> ModernBindings =>
    [
        (KeyboardAction.ResetCamera, new KeyBinding(Keys.R)),
        (KeyboardAction.DecreaseCameraSpeed, new KeyBinding(Keys.D1)),
        (KeyboardAction.IncreaseCameraSpeed, new KeyBinding(Keys.D2)),
        (KeyboardAction.ToggleMouseMode, new KeyBinding(Keys.Z)),
        (KeyboardAction.ToggleCulling, new KeyBinding(Keys.C)),
        (KeyboardAction.ToggleWireframe, new KeyBinding(Keys.F)),
        (KeyboardAction.ToggleStartPos, new KeyBinding(Keys.Y)),
        (KeyboardAction.ToggleRenderByChunk, new KeyBinding(Keys.H)),
        (KeyboardAction.ToggleChunkBoxes, new KeyBinding(Keys.B)),
        (KeyboardAction.ToggleCollision, new KeyBinding(Keys.X)),
        (KeyboardAction.ToggleQuadtree, new KeyBinding(Keys.T)),
        (KeyboardAction.ToggleSplines, new KeyBinding(Keys.U)),
        (KeyboardAction.ToggleShowObjects, new KeyBinding(Keys.G)),
        (KeyboardAction.ToggleShowCameras, new KeyBinding(Keys.V)),
        (KeyboardAction.ToggleSelectionMode, new KeyBinding(Keys.M)),
        (KeyboardAction.ShowViewControls, new KeyBinding(Keys.F1)),
        (KeyboardAction.ShowViewConfig, new KeyBinding(Keys.F2)),
        (KeyboardAction.ShowLevelEditor, new KeyBinding(Keys.F3)),
        (KeyboardAction.ShowCollisionEditor, new KeyBinding(Keys.F4)),
        (KeyboardAction.ShowLayoutEditor, new KeyBinding(Keys.F5)),
        (KeyboardAction.TeleportPlayerToCamera, new KeyBinding(Keys.F6)),
        (KeyboardAction.ShowCameraEditor, new KeyBinding(Keys.F7)),
        (KeyboardAction.ShowParticleEditor, new KeyBinding(Keys.F8)),
        (KeyboardAction.ShowTexturePatternEditor, new KeyBinding(Keys.F9)),
        (KeyboardAction.ShowLightEditor, new KeyBinding(Keys.F10)),
        (KeyboardAction.ShowModLoaderConfigEditor, new KeyBinding(Keys.F11)),
    ];

    public void KeyboardController()
    {
        var direction = new Vector3(0, 0, 0);

        if (PressedKeys.Contains(Keys.A) & PressedKeys.Contains(Keys.ControlKey))
            renderer.Camera.AddYaw(-renderer.Camera.KeyboardSensitivity);
        else if (PressedKeys.Contains(Keys.D) & PressedKeys.Contains(Keys.ControlKey))
            renderer.Camera.AddYaw(renderer.Camera.KeyboardSensitivity);
        else if (PressedKeys.Contains(Keys.A))
            direction += renderer.Camera.GetLeft();
        else if (PressedKeys.Contains(Keys.D))
            direction -= renderer.Camera.GetLeft();

        if (PressedKeys.Contains(Keys.W) & PressedKeys.Contains(Keys.ControlKey))
            renderer.Camera.AddPitch(-renderer.Camera.KeyboardSensitivity);
        else if (PressedKeys.Contains(Keys.S) & PressedKeys.Contains(Keys.ControlKey))
            renderer.Camera.AddPitch(renderer.Camera.KeyboardSensitivity);
        else if (PressedKeys.Contains(Keys.W) & PressedKeys.Contains(Keys.ShiftKey))
            direction += renderer.Camera.GetUp();
        else if (PressedKeys.Contains(Keys.S) & PressedKeys.Contains(Keys.ShiftKey))
            direction -= renderer.Camera.GetUp();
        else if (PressedKeys.Contains(Keys.W))
            direction += renderer.Camera.GetForward();
        else if (PressedKeys.Contains(Keys.S))
            direction -= renderer.Camera.GetForward();
        else if (PressedKeys.Contains(Keys.E))
            direction += renderer.Camera.GetUp();
        else if (PressedKeys.Contains(Keys.Q))
            direction -= renderer.Camera.GetUp();

        if (direction != Vector3.Zero)
            renderer.Camera.AddPosition(direction);

        if (PressedKeys.Contains(Keys.Up))
            renderer.Camera.AddPitch(-renderer.Camera.KeyboardSensitivity);
        else if (PressedKeys.Contains(Keys.Down))
            renderer.Camera.AddPitch(renderer.Camera.KeyboardSensitivity);

        if (PressedKeys.Contains(Keys.Left))
            renderer.Camera.AddYaw(-renderer.Camera.KeyboardSensitivity);
        else if (PressedKeys.Contains(Keys.Right))
            renderer.Camera.AddYaw(renderer.Camera.KeyboardSensitivity);
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

    private void viewControlsToolStripMenuItem_Click(object sender, EventArgs e)
    {
        HandleAction(KeyboardAction.ShowViewControls);
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
        ShowForm(ViewConfig);
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
        ShadowTexturePatternEditor,
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
                }
                else
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

    private void MainForm_Resize(object sender, EventArgs e)
    {
        if (WindowState == FormWindowState.Minimized)
        {
            renderer?.dontRender = true;
        }
        else
        {
            if (!disableRendering_ToolStripMenuItem.Checked)
                renderer?.dontRender = false;

            HPPConfig.GetInstance().MainWindowSize = Size;
        }
    }

    /// <summary>
    /// This is just a shortcut for opening the view menu.
    /// </summary>
    private void cameraViewSettingsToolStripMenuItem_Click(object sender, EventArgs e)
    {
        ShowForm(ViewConfig);
    }

    public void TeleportPlayerToCamera()
    {
        if (!MemoryFunctions.Teleport(renderer.Camera.GetPosition()))
            MessageBox.Show("Unable to teleport player.");
    }

    private void shadowCameraEditorToolStripMenuItem_Click(object sender, EventArgs e)
    {
        //TODO: Fix disposed obj exception
        ShowForm(ShadowCameraEditor);
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

    public void SetUseShadowTXDForTextures(bool isEnabled)
    {
        UseShadowTXDForTextures_ToolStripMenuItem.Checked = isEnabled;
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

    private void shadowLayoutMiscToolsToolStripMenuItem_Click(object sender, EventArgs e)
    {
        ShowForm(ShadowLayoutMiscTools);
    }

    private void UseShadowTXDForTextures_ToolStripMenuItem_Click(object sender, EventArgs e)
    {
        UseShadowTXDForTextures_ToolStripMenuItem.Checked = !UseShadowTXDForTextures_ToolStripMenuItem.Checked;
        HPPConfig.GetInstance().UseShadowTXDForTextures = UseShadowTXDForTextures_ToolStripMenuItem.Checked;
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
        SetAFSlocation(currentShadowLevelRoot + "\\PRS_VOICE_E.afs");
        var fntFileName = currentShadowLevelRoot + "\\fonts\\" + shadowStage + "\\" + shadowStage + "_EN.fnt";
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
                TextureManager.SetupTextureDisplay(File.ReadAllBytes(probComObjTxd), renderer, LevelEditor.bspRenderer);

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