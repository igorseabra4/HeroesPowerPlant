using HeroesPowerPlant.Shared.IO.Config;
using Ookii.Dialogs.WinForms;
using System;
using System.Windows.Forms;

namespace HeroesPowerPlant.LightEditor
{
    public partial class LightMenu : Form
    {
        private LightEditor LightEditor;

        public LightMenu()
        {
            InitializeComponent();
            LightEditor = new LightEditor();
            UpdateValues();
        }

        public bool GetIsShadow() => LightEditor.isShadow;

        public void UpdateValues()
        {
            if (string.IsNullOrEmpty(LightEditor.CurrentlyOpenLightFile))
                toolStripStatusLabel1.Text = "No file loaded";
            else
                toolStripStatusLabel1.Text = LightEditor.CurrentlyOpenLightFile;

            numericCurrentLight.Minimum = LightEditor.Lights.Count == 0 ? -1 : 0;
            numericCurrentLight.Maximum = LightEditor.Lights.Count == 0 ? -1 : LightEditor.Lights.Count - 1;
        }

        public void OpenFile(string fileName, bool isShadow)
        {
            LightEditor = new LightEditor(fileName, isShadow);
            UpdateValues();
        }

        private void SaveLightFile(string fileName, bool isShadow)
        {
            LightEditor.Save(fileName, isShadow);
            UpdateValues();
        }

        public string GetCurrentlyOpenLightFile()
        {
            return LightEditor.CurrentlyOpenLightFile;
        }

        private void LightEditor_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.WindowsShutDown) return;
            if (e.CloseReason == CloseReason.FormOwnerClosing) return;

            e.Cancel = true;
            Hide();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            New();
        }

        public void New()
        {
            LightEditor = new LightEditor();
            UpdateValues();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            VistaOpenFileDialog openFile = new VistaOpenFileDialog()
            {
                Filter = "BIN Files|*.bin"
            };
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                OpenFile(openFile.FileName, false);
            }
        }

        private void openShadowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            VistaOpenFileDialog openFile = new VistaOpenFileDialog()
            {
                Filter = "BIN Files|*.bin"
            };
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                OpenFile(openFile.FileName, true);
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(LightEditor.CurrentlyOpenLightFile))
                saveAsToolStripMenuItem_Click(sender, e);
            else
                LightEditor.Save(LightEditor.CurrentlyOpenLightFile);
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            VistaSaveFileDialog saveFile = new VistaSaveFileDialog()
            {
                Filter = "BIN Files|*.bin",
                FileName = LightEditor.CurrentlyOpenLightFile
            };

            if (saveFile.ShowDialog() == DialogResult.OK)
            {
                SaveLightFile(saveFile.FileName, false);
                UpdateValues();
            }
        }

        private void saveAsShadowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            VistaSaveFileDialog saveFile = new VistaSaveFileDialog()
            {
                Filter = "BIN Files|*.bin",
                FileName = LightEditor.CurrentlyOpenLightFile
            };

            if (saveFile.ShowDialog() == DialogResult.OK)
            {
                SaveLightFile(saveFile.FileName, true);
                UpdateValues();
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            LightEditor.Lights.Add(new Light());
            UpdateValues();
        }

        private void buttonCopy_Click(object sender, EventArgs e)
        {
            int index = (int)numericCurrentLight.Value;
            if (index >= 0 & index < LightEditor.Lights.Count)
            {
                var particleToClone = Light.FromLightEntry(LightEditor.Lights[index]);
                LightEditor.Lights.Add(particleToClone);
                UpdateValues();
            }
        }

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            int index = (int)numericCurrentLight.Value;
            if (index >= 0 & index < LightEditor.Lights.Count)
            {
                LightEditor.Lights.RemoveAt(index);
                UpdateValues();
            }
        }

        private void numericCurrentLight_ValueChanged(object sender, EventArgs e)
        {
            int index = (int)numericCurrentLight.Value;

            if (index >= 0 & index < LightEditor.Lights.Count)
                propertyGridLights.SelectedObject = LightEditor.Lights[index];
            else
                propertyGridLights.SelectedObject = null;
        }

        private void propertyGridLights_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            int index = (int)numericCurrentLight.Value;

            if (index >= 0 & index < LightEditor.Lights.Count)
                LightEditor.Lights[index] = (Light)propertyGridLights.SelectedObject;
        }

        private void LightMenu_Load(object sender, EventArgs e)
        {
            if (HPPConfig.GetInstance().LegacyWindowPriorityBehavior)
                TopMost = true;
            else
                TopMost = false;
        }
    }
}
