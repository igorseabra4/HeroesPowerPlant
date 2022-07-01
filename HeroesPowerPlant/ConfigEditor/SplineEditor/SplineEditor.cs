using Heroes.SDK.Definitions.Structures.Stage.Splines;
using HeroesPowerPlant.Shared.IO.Config;
using System;
using System.Collections.Generic;
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

        private void SplineEditor_Load(object sender, EventArgs e)
        {
            if (HPPConfig.GetInstance().LegacyWindowPriorityBehavior)
                TopMost = true;
            else
                TopMost = false;
        }

        private SplineEditorFunctions splineEditorFunctions = new SplineEditorFunctions();
        private bool ProgramIsChangingStuff = false;

        private void listBoxSplines_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectedSplineChanged();
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
            listBoxPoints.Items.Clear();

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

        private void listBoxPoints_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxPoints.SelectedIndex == -1)
                groupBoxPitchRoll.Enabled = false;
            else
            {
                groupBoxPitchRoll.Enabled = true;
                ProgramIsChangingStuff = true;

                numericUpDownPitch.Value = (decimal)ReadWriteCommon.BAMStoDegrees(splineEditorFunctions.GetSelected().Points[listBoxPoints.SelectedIndex].Pitch);
                numericUpDownRoll.Value = (decimal)ReadWriteCommon.BAMStoDegrees(splineEditorFunctions.GetSelected().Points[listBoxPoints.SelectedIndex].Roll);

                ProgramIsChangingStuff = false;
            }
        }

        private void numericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (!ProgramIsChangingStuff && listBoxPoints.SelectedIndex != -1)
            {
                splineEditorFunctions.GetSelected().Points[listBoxPoints.SelectedIndex].Pitch = (ushort)ReadWriteCommon.DegreesToBAMS((float)numericUpDownPitch.Value);
                splineEditorFunctions.GetSelected().Points[listBoxPoints.SelectedIndex].Roll = (ushort)ReadWriteCommon.DegreesToBAMS((float)numericUpDownRoll.Value);
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            splineEditorFunctions.SaveJson();
        }

        private void buttonViewHere_Click(object sender, EventArgs e)
        {
            splineEditorFunctions.ViewHere(listBoxPoints.SelectedIndex);
        }

        public void SplineEditorNewConfig()
        {
            splineEditorFunctions.SplineEditorNewConfig();
            UpdateSplineList();
        }

        public void SplineEditorOpenConfig(SharpRenderer renderer)
        {
            splineEditorFunctions.SplineEditorOpenConfig(renderer);
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

        private void SelectedSplineChanged()
        {
            ProgramIsChangingStuff = true;

            splineEditorFunctions.SelectedIndexChanged(listBoxSplines.SelectedIndex);
            comboBoxType.SelectedItem = splineEditorFunctions.GetSelected().Type.ToString();

            groupBoxPitchRoll.Enabled = false;

            listBoxPoints.Items.Clear();
            for (int i = 0; i < splineEditorFunctions.GetSelected().Points.Length; i++)
                listBoxPoints.Items.Add("Point " + (i + 1).ToString());

            ProgramIsChangingStuff = false;
        }

        private void buttonAutoPitchPoint_Click(object sender, EventArgs e)
        {
            if (listBoxPoints.SelectedIndex == listBoxPoints.Items.Count - 1)
                MessageBox.Show("Cannot AutoPitch last point of spline");
            else
                splineEditorFunctions.AutoPitchPoint(listBoxPoints.SelectedIndex);
        }

        private void buttonAutoPitchSpline_Click(object sender, EventArgs e)
        {
            splineEditorFunctions.AutoPitchSpline();
        }

        private void buttonAutoPitchAll_Click(object sender, EventArgs e)
        {
            splineEditorFunctions.AutoPitchAll();
        }

        private void buttonExportOBJ_Click(object sender, EventArgs e)
        {
            if (listBoxSplines.SelectedIndex >= 0 && listBoxSplines.SelectedIndex < splineEditorFunctions.GetSplineCount())
            {
                using SaveFileDialog saveFile = new SaveFileDialog() { Filter = "*.obj|OBJ files|*.*|All Files" };
                if (saveFile.ShowDialog() == DialogResult.OK)
                    splineEditorFunctions.ExportOBJ(saveFile.FileName);
            }
        }

        public void AddFromExe(List<SplineVertex> vertices, Heroes.SDK.Definitions.Structures.Stage.Splines.SplineType splineType)
        {
            splineEditorFunctions.AddSpline(vertices, splineType, Program.MainForm.renderer);
            UpdateSplineList();
            listBoxSplines.SelectedIndex = listBoxSplines.Items.Count - 1;
        }

        public void RenderSplines(SharpRenderer renderer)
        {
            splineEditorFunctions.RenderSplines(renderer);
        }

        public void DisposeSplines()
        {
            splineEditorFunctions.DisposeSplines();
        }
    }
}