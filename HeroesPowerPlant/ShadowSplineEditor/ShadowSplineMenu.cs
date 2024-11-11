using HeroesPowerPlant.Shared.IO.Config;
using Ookii.Dialogs.WinForms;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace HeroesPowerPlant.ShadowSplineEditor {
    public partial class ShadowSplineMenu : Form
    {
        private ShadowSplineEditor SplineEditor;

        public ShadowSplineMenu()
        {
            InitializeComponent();
        }

        private void ParticleEditor_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.WindowsShutDown)
                return;
            if (e.CloseReason == CloseReason.FormOwnerClosing)
                return;

            e.Cancel = true;
            Hide();
        }

        public void Init()
        {
            if (SplineEditor != null)
                SplineEditor.Dispose();
            propertyGridSplines.SelectedObject = null;
            SplineEditor = new ShadowSplineEditor();
            UpdateSplineList();
        }

        public void Init(string fileName)
        {
            if (SplineEditor != null)
                SplineEditor.Dispose();
            propertyGridSplines.SelectedObject = null;
            SplineEditor = new ShadowSplineEditor(fileName);
            UpdateSplineList();
        }

        public IEnumerable<byte> ShadowSplinesToByteArray(string shadowFolderNamePrefix)
        {
            return SplineEditor.ShadowSplinesToByteArray(shadowFolderNamePrefix);
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            SplineEditor.Add();
            UpdateSplineList();
            listBoxSplines.SelectedIndex = listBoxSplines.Items.Count - 1;
        }

        private void buttonImport_Click(object sender, EventArgs e)
        {
            try
            {
                int suffix = int.Parse(textBox_splineSuffixNumber.Text);
                VistaOpenFileDialog openSpline = new VistaOpenFileDialog()
                {
                    Multiselect = true,
                    Filter = ".obj files|*.obj"
                };
                if (openSpline.ShowDialog() == DialogResult.OK)
                {
                    SplineEditor.Add(openSpline.FileNames, textBox_splineNamePrefix.Text, suffix);
                    UpdateSplineList();
                    listBoxSplines.SelectedIndex = listBoxSplines.Items.Count - 1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Double check your spline suffix number.\n\n" + ex.Message);
            }
        }

        private void buttonCopy_Click(object sender, EventArgs e)
        {
            int index = listBoxSplines.SelectedIndex;
            if (SplineEditor.Copy(listBoxSplines.SelectedIndex))
            {
                UpdateSplineList();
                listBoxSplines.SelectedIndex = listBoxSplines.Items.Count - 1;
            }
        }

        private void buttonExport_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Export All?", "Option", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.No)
            {
                VistaSaveFileDialog saveSpline = new VistaSaveFileDialog()
                {
                    Filter = ".obj files|*.obj",
                    DefaultExt = ".obj",
                    FileName = SplineEditor.GetSplineAt(listBoxSplines.SelectedIndex) + ".obj"
                };
                if (saveSpline.ShowDialog() == DialogResult.OK)
                {
                    SplineEditor.ExportSelectedSpline(saveSpline.FileName);
                }
            }
            else if (result == DialogResult.Yes)
            {
                VistaFolderBrowserDialog saveSplines = new VistaFolderBrowserDialog()
                {
                    Description = "Pick folder to extract all splines to."
                };
                if (saveSplines.ShowDialog() == DialogResult.OK)
                {
                    SplineEditor.ExportAllSplines(saveSplines.SelectedPath);
                }
            }
        }

        private void buttonViewHere_Click(object sender, EventArgs e)
        {
            SplineEditor.ViewHere();
        }

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            int index = listBoxSplines.SelectedIndex;
            if (SplineEditor.Remove(listBoxSplines.SelectedIndex))
            {
                UpdateSplineList();
                try
                {
                    listBoxSplines.SelectedIndex = index;
                }
                catch { }
            }
        }

        private void UpdateSplineList()
        {
            listBoxSplines.Items.Clear();
            listBoxSplines.Items.AddRange(SplineEditor.GetAllSplines());
        }

        private void listBoxSplines_SelectedIndexChanged(object sender, EventArgs e)
        {
            SplineEditor.SetSelectedSpline(listBoxSplines.SelectedIndex, propertyGridSplines);
        }

        internal void RenderSplines(SharpRenderer sharpRenderer)
        {
            if (SplineEditor != null)
                SplineEditor.RenderSplines(sharpRenderer);
        }

        private void propertyGridSplines_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            SplineEditor.PropertyValueChanged();

            int index = listBoxSplines.SelectedIndex;
            UpdateSplineList();
            try
            {
                listBoxSplines.SelectedIndex = index;
            }
            catch { }
        }

        private void ShadowSplineMenu_Load(object sender, EventArgs e)
        {
            TopMost = HPPConfig.GetInstance().LegacyWindowPriorityBehavior;
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            SplineEditor.RemoveAll();
            UpdateSplineList();
            try
            {
                listBoxSplines.SelectedIndex = -1;
            }
            catch { }
        }

        public bool UnsavedChanges
        {
            get => SplineEditor != null && SplineEditor.UnsavedChanges;
            set
            {
                if (SplineEditor != null)
                    SplineEditor.UnsavedChanges = value;
            }
        }
    }
}
