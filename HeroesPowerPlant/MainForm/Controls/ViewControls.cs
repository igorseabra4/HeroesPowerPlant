using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using static HeroesPowerPlant.MainForm.MainForm;

namespace HeroesPowerPlant.MainForm;

public partial class ViewControls : Form
{

    private MainForm MainForm;

    public ViewControls(MainForm MainForm)
    {
        this.MainForm = MainForm;
        InitializeComponent();
        originalText = labelEditHelp.Text;

        foreach (var s in shortcuts)
            s.IsPreset = !s.EditorAction.HasValue || MainForm.IsKeyBindPreset(s.EditorAction.Value);

        shortcutBindingSource.DataSource = shortcuts;

        dataGridView1.AutoGenerateColumns = true;
        dataGridView1.DataSource = shortcutBindingSource;

        dataGridView1.DataBindingComplete += (_, _) =>
        {
            dataGridView1.Columns[0].Width = 136;
            dataGridView1.Columns[1].Width = 360;
            dataGridView1.Columns[2].Width = 66;
        };
        KeyPreview = true;

        foreach (DataGridViewRow row in dataGridView1.Rows)
            row.Selected = false;
    }

    protected override void OnFormClosing(FormClosingEventArgs e)
    {
        if (e.CloseReason == CloseReason.WindowsShutDown)
            return;
        if (e.CloseReason == CloseReason.FormOwnerClosing)
            return;

        e.Cancel = true;
        Hide();
    }

    private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
        if (_capturingRow >= 0)
            return;

        AboutBox.OpenWiki();
    }

    private readonly BindingSource shortcutBindingSource = new();

    private void textBoxSearch_TextChanged(object sender, EventArgs e)
    {
        _capturingRow = -1;
        string search = textBoxSearch.Text.Trim();

        shortcutBindingSource.DataSource = string.IsNullOrEmpty(search)
            ? shortcuts
            : [.. shortcuts.Where(s =>
                (s.Input?.Contains(search, StringComparison.OrdinalIgnoreCase) ?? false) ||
                (s.Description?.Contains(search, StringComparison.OrdinalIgnoreCase) ?? false) ||
                (s.Category?.Contains(search, StringComparison.OrdinalIgnoreCase) ?? false)
                )];

        foreach (DataGridViewRow row in dataGridView1.Rows)
            row.Selected = false;
    }

    private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
    {
        if (dataGridView1.Rows[e.RowIndex].DataBoundItem is not ShortcutInfo shortcut)
            return;

        if (shortcut.EditorAction.HasValue)
        {
            var binding = MainForm.GetKeyBind(shortcut.EditorAction.Value);

            if (!shortcut.IsPreset)
                e.CellStyle.BackColor = binding == null ? System.Drawing.Color.DarkRed : System.Drawing.Color.DarkSlateBlue;
                    //(MainForm.IsDarkMode ? System.Drawing.Color.DarkRed : System.Drawing.Color.Orange) :
                    //(MainForm.IsDarkMode ? System.Drawing.Color.DarkSlateBlue : System.Drawing.Color.LightBlue);

            if (e.ColumnIndex == 0)
            {
                if (e.RowIndex == _capturingRow)
                    e.Value = "Press a key...";
                else if (binding != null)
                    e.Value = binding.ToString() + shortcut.Input;
                else
                    e.Value = "Unassigned";
            }

            e.FormattingApplied = true;
        }
    }

    private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex < 0)
            return;

        if (dataGridView1.Rows[e.RowIndex].DataBoundItem is not ShortcutInfo shortcut || shortcut.IsPreset)
            return;

        _capturingRow = e.RowIndex;
        dataGridView1.Invalidate();
    }

    private readonly string originalText;
    private int _capturingRow
    {
        get;
        set
        {
            field = value;

            if (value >= 0)
                labelEditHelp.Text = "Press a key to bind, or Esc to cancel";
            else
                labelEditHelp.Text = originalText;
        }
    } = -1;

    private void ViewControls_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Escape)
        {
            _capturingRow = -1;
            dataGridView1.Refresh();
            e.Handled = true;
            return;
        }

        if (_capturingRow < 0)
            return;
        if (e.KeyCode is Keys.ControlKey or Keys.ShiftKey or Keys.Menu)
            return;

        Keys modifiers = ModifierKeys & (Keys.Control | Keys.Shift | Keys.Alt);
        var binding = new KeyBinding(e.KeyCode, modifiers);
        var shortcut = (ShortcutInfo)dataGridView1.Rows[_capturingRow].DataBoundItem;
        MainForm.SetKeyBind(shortcut.EditorAction.Value, binding, true);
        _capturingRow = -1;
        dataGridView1.Refresh();

        e.Handled = true;
        e.SuppressKeyPress = true;
    }

    private readonly List<ShortcutInfo> shortcuts = new()
    {
        new("W / A / S / D", "Move camera forward / left / backward / right", "Camera"),
        new("E / Q", "Move camera up / down", "Camera"),
        new("Shift + W / A / S / D", "Move camera up / left / down / right", "Camera"),
        new("Ctrl + W / A / S / D", "Rotate camera up / left / down / right (pitch and yaw)", "Camera"),
        new("Middle mouse + drag", "Rotate camera", "Camera"),
        new("Shift + Middle mouse + drag", "Move camera up / left / down / right", "Camera"),
        new("Ctrl + Middle mouse + drag", "Move camera forward / backward", "Camera"),
        new("Mouse wheel", "Move camera forward / backward", "Camera"),
        new("Decrease camera movement speed", "Camera", KeyboardAction.DecreaseCameraSpeed),
        new("Increase camera movement speed", "Camera", KeyboardAction.IncreaseCameraSpeed),
        new("Reset camera view to origin", "Camera", KeyboardAction.ResetCamera),
        new("Toggle fly mode; rotate with the mouse and move with the keyboard", "Camera", KeyboardAction.ToggleMouseMode),

        new("Left mouse", "Select an object or camera", "Selection"),
        new("Ctrl + Left mouse", "Select multiple objects", "Selection"),
        new("Left mouse + drag", "Transform selected objects with gizmo", "Editing"),
        new("Shift + Right mouse", "Place a copy of an object", "Editing"),

        new("Open Heroes Level - opens all files needed for a level", "Tools", KeyboardAction.OpenLevel),

        new("Toggle backface culling", "View", KeyboardAction.ToggleCulling),
        new("Toggle wireframe mode", "View", KeyboardAction.ToggleWireframe),
        new("Toggle start positions", "View", KeyboardAction.ToggleStartPos),
        new("Toggle render by chunk", "View", KeyboardAction.ToggleRenderByChunk),
        new("Toggle chunk boxes", "View", KeyboardAction.ToggleChunkBoxes),
        new("Toggle collision display", "View", KeyboardAction.ToggleCollision),
        new("Toggle quadtree display", "View", KeyboardAction.ToggleQuadtree),
        new("Toggle spline display", "View", KeyboardAction.ToggleSplines),
        new("Toggle object display", "View", KeyboardAction.ToggleShowObjects),
        new("Toggle camera display", "View", KeyboardAction.ToggleShowCameras),
        new("Toggle selection mode", "View", KeyboardAction.ToggleSelectionMode),
        new("Show View Controls", "Tools", KeyboardAction.ShowViewControls),
        new("Show View Config", "Tools", KeyboardAction.ShowViewConfig),
        new("Show Level Editor", "Tools", KeyboardAction.ShowLevelEditor),
        new("Show Collision Editor", "Tools", KeyboardAction.ShowCollisionEditor),
        new("Show Layout Editor", "Tools", KeyboardAction.ShowLayoutEditor),
        new("Ctrl + 1–9", "Open corresponding Layout Editor window", "Tools"),
        new("Teleport Player To Camera", "Tools", KeyboardAction.TeleportPlayerToCamera),
        new("Show Camera Editor", "Tools", KeyboardAction.ShowCameraEditor),
        new("Show Particle Editor", "Tools", KeyboardAction.ShowParticleEditor),
        new("Show Texture Pattern Editor", "Tools", KeyboardAction.ShowTexturePatternEditor),
        new("Show Light Editor", "Tools", KeyboardAction.ShowLightEditor),
        new("Show Level Config Editor", "Tools", KeyboardAction.ShowModLoaderConfigEditor),
        new("Show SET ID Table Editor", "Tools", KeyboardAction.ShowSetIdTableEditor),
        new("Open About Box", "Tools", KeyboardAction.ViewAbout),
    };

    private void buttonDefaults_Click(object sender, EventArgs e)
    {
        MainForm.ResetKeyBinds();
        dataGridView1.Refresh();
    }

    private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
    {
        if (e.RowIndex < 0 || dataGridView1.Rows[e.RowIndex].DataBoundItem is not ShortcutInfo shortcut || shortcut.IsPreset)
            return;

        if (e.Button == MouseButtons.Right)
        {
            _capturingRow = -1;
            MainForm.ResetKeyBind(shortcut.EditorAction.Value);
            dataGridView1.Invalidate();
        }
        else if (e.Button == MouseButtons.Middle)
        {
            _capturingRow = -1;
            MainForm.RemoveKeyBind(shortcut.EditorAction.Value);
            dataGridView1.Invalidate();
        }
    }
}
