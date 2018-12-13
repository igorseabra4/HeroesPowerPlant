using System;
using System.Linq;
using System.Windows.Forms;

namespace HeroesPowerPlant.ShadowSplineEditor
{
    public partial class ShadowSplineMenu : Form
    {
        private ShadowSplineEditor SplineEditor;

        public ShadowSplineMenu()
        {
            InitializeComponent();
        }

        private void ParticleEditor_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.WindowsShutDown) return;
            if (e.CloseReason == CloseReason.FormOwnerClosing) return;

            e.Cancel = true;
            Hide();
        }

        public void Init(string fileName)
        {
            SplineEditor = new ShadowSplineEditor(fileName);
            UpdateSplineList();
        }

        public void Save(string fileName)
        {
            MessageBox.Show("Can't save shadow splines yet!");
            return;
            SplineEditor.Save(fileName);
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            SplineEditor.Add();
            UpdateSplineList();
            listBoxSplines.SelectedIndex = listBoxSplines.Items.Count - 1;
        }

        private void buttonImport_Click(object sender, EventArgs e)
        {
            OpenFileDialog openSpline = new OpenFileDialog()
            {
                Multiselect = true,
                Filter = ".obj files|*.obj"
            };
            if (openSpline.ShowDialog() == DialogResult.OK)
            {
                SplineEditor.Add(openSpline.FileNames);
                UpdateSplineList();
                listBoxSplines.SelectedIndex = listBoxSplines.Items.Count - 1;
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
            SaveFileDialog saveSpline = new SaveFileDialog()
            {
                Filter = ".obj files|*.obj",
                FileName = SplineEditor.GetSplineAt(listBoxSplines.SelectedIndex) + ".obj"
            };
            if (saveSpline.ShowDialog() == DialogResult.OK)
            {
                SplineEditor.Export(saveSpline.FileName);
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
    }
}
