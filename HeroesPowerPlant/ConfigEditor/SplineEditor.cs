using System;
using System.Windows.Forms;

namespace HeroesPowerPlant.Config
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
                
        private void listBoxSplines_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CurrentlySelectedObject != -1 & CurrentlySelectedObject < SplineList.Count)
                SplineList[CurrentlySelectedObject].isSelected = false;
            SplineList[listBoxSplines.SelectedIndex].isSelected = true;
            CurrentlySelectedObject = listBoxSplines.SelectedIndex;

            ProgramIsChangingStuff = true;
            comboBoxType.SelectedItem = SplineList[CurrentlySelectedObject].Type.ToString();
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
                foreach (string i in openSpline.FileNames)
                {
                    Spline s = SplineFromFile(i);

                    if (s.Points.Length < 2)
                    {
                        MessageBox.Show("Error: file " + i + " has less than two vertices. Skipping...");
                        continue;
                    }
                    SplineList.Add(SplineFromFile(i));
                    listBoxSplines.Items.Add("Spline " + listBoxSplines.Items.Count.ToString());
                }
                listBoxSplines.SelectedIndex = listBoxSplines.Items.Count - 1;
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (CurrentlySelectedObject != -1 & CurrentlySelectedObject < SplineList.Count)
            {
                int previous = CurrentlySelectedObject;
                SplineList.RemoveAt(CurrentlySelectedObject);

                listBoxSplines.Items.Clear();
                for (int i = 0; i < SplineList.Count; i++)
                    listBoxSplines.Items.Add("Spline " + i.ToString());
                if (previous < listBoxSplines.Items.Count)
                    listBoxSplines.SelectedIndex = previous;
                else
                    listBoxSplines.SelectedIndex = previous - 1;
            }
        }

        private void comboBoxType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!ProgramIsChangingStuff & CurrentlySelectedObject != -1 & CurrentlySelectedObject < SplineList.Count)
                SplineList[CurrentlySelectedObject].Type = (SplineType)comboBoxType.SelectedIndex;
        }

        public void buttonSave_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void buttonViewHere_Click(object sender, EventArgs e)
        {
            if (listBoxSplines.SelectedIndex != -1)
                SharpRenderer.Camera.SetPosition(SplineList[listBoxSplines.SelectedIndex].Points[0].Position - 200 * SharpRenderer.Camera.GetForward());
        }
    }
}