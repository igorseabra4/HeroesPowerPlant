using System;
using System.Windows.Forms;

namespace HeroesPowerPlant.SplineEditor
{
    public partial class SplineEditor : Form
    {
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.WindowsShutDown) return;
            if (e.CloseReason == CloseReason.FormOwnerClosing) return;

            e.Cancel = true;
            Hide();
        }

        public SplineEditor()
        {
            InitializeComponent();
        }

        private void CollisionEditor_Load(object sender, EventArgs e)
        {
            TopMost = true;
        }

        private SplineEditorFunctions splineEditorFunctions = new SplineEditorFunctions();
        private bool ProgramIsChangingStuff = false;

        private void listBoxSplines_SelectedIndexChanged(object sender, EventArgs e)
        {
            splineEditorFunctions.SelectedIndexChanged(listBoxSplines.SelectedIndex);
            
            ProgramIsChangingStuff = true;
            comboBoxType.SelectedItem = splineEditorFunctions.GetSelectedType();
            ProgramIsChangingStuff = false;
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            OpenFileDialog openSpline = new OpenFileDialog()
            {
                Multiselect = true,
                Filter = ".obj files|*.obj"
            };
            if (openSpline.ShowDialog() == DialogResult.OK)
            {
                splineEditorFunctions.AddSplines(openSpline.FileNames, Program.MainForm.renderer);
                UpdateSplineList();
                listBoxSplines.SelectedIndex = listBoxSplines.Items.Count - 1;
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            splineEditorFunctions.DeleteSelectedSpline();

            int previous = listBoxSplines.SelectedIndex;

            UpdateSplineList();

            if (previous < listBoxSplines.Items.Count)
                listBoxSplines.SelectedIndex = previous;
            else
                listBoxSplines.SelectedIndex = previous - 1;
        }

        private void comboBoxType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!ProgramIsChangingStuff)
                splineEditorFunctions.ChangeType(comboBoxType.SelectedIndex);
        }

        public void buttonSave_Click(object sender, EventArgs e)
        {
            splineEditorFunctions.Save(Program.MainForm.ConfigEditor.GetOpenConfigFileName());
        }

        private void buttonViewHere_Click(object sender, EventArgs e)
        {
            splineEditorFunctions.ViewHere();
        }

        public void SplineEditorNewConfig()
        {
            splineEditorFunctions.SplineEditorNewConfig();
            UpdateSplineList();
        }

        public void SplineEditorOpenConfig(string fileName, SharpRenderer renderer)
        {
            splineEditorFunctions.SplineEditorOpenConfig(fileName, renderer);
            UpdateSplineList();
        }

        private void UpdateSplineList()
        {
            ProgramIsChangingStuff = true;
            listBoxSplines.Items.Clear();
            for (int i = 0; i < splineEditorFunctions.GetSplineCount(); i++)
                listBoxSplines.Items.Add("Spline " + (i + 1).ToString());
            ProgramIsChangingStuff = false;
        }

        public void RenderSplines(SharpRenderer renderer)
        {
            splineEditorFunctions.RenderSplines(renderer);
        }

        public void DisposeSplines()
        {
            splineEditorFunctions.DisposeSplines();
        }

        public void Save(string configFilename)
        {
            splineEditorFunctions.Save(configFilename);
        }

        private void buttonSaveJson_Click(object sender, EventArgs e)
        {
            splineEditorFunctions.SaveJson(Program.MainForm.ConfigEditor.GetOpenConfigFileName());
        }
    }
}