using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using SharpDX;

namespace HeroesPowerPlant.LayoutEditor
{
    public partial class LayoutEditor : Form
    {
        public LayoutEditor()
        {
            InitializeComponent();

            try
            {
                layoutSystem = new LayoutEditorSystem();
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show("Error: failed to load one or more files during startup. Program will not work correctly.");
            }
            
            NumericPosX.Maximum = decimal.MaxValue;
            NumericPosX.Minimum = decimal.MinValue;
            NumericPosY.Maximum = decimal.MaxValue;
            NumericPosY.Minimum = decimal.MinValue;
            NumericPosZ.Maximum = decimal.MaxValue;
            NumericPosZ.Minimum = decimal.MinValue;

            NumericRotX.Maximum = decimal.MaxValue;
            NumericRotX.Minimum = decimal.MinValue;
            NumericRotY.Maximum = decimal.MaxValue;
            NumericRotY.Minimum = decimal.MinValue;
            NumericRotZ.Maximum = decimal.MaxValue;
            NumericRotZ.Minimum = decimal.MinValue;

            gizmos = new Gizmo[3];
            gizmos[0] = new Gizmo(GizmoType.X);
            gizmos[1] = new Gizmo(GizmoType.Y);
            gizmos[2] = new Gizmo(GizmoType.Z);
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.WindowsShutDown) return;
            if (e.CloseReason == CloseReason.FormOwnerClosing) return;

            e.Cancel = true;
            Hide();
        }

        private LayoutEditorSystem layoutSystem;
        private bool ProgramIsChangingStuff = false;

        private void heroesLayoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            New();
        }

        public void New()
        {
            layoutSystem.NewHeroesLayout();

            UpdateObjectComboBox();
            UpdateFileLabel();
        }

        private void shadowLayoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            layoutSystem.NewShadowLayout();

            UpdateObjectComboBox();
            UpdateFileLabel();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog
            {
                Filter = "All supported types|*.bin; *.dat|BIN Files|*.bin|DAT Files|*.dat"
            };

            if (openFile.ShowDialog() == DialogResult.OK)
                OpenFile(openFile.FileName);
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!layoutSystem.Save())
                SaveAs();
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveAs();
        }
        
        private void byIDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            layoutSystem.SortObjectsByID();
            UpdateEntireObjectList();
        }

        private void byDistanceFromOriginToolStripMenuItem_Click(object sender, EventArgs e)
        {
            layoutSystem.SortObjectsByDistance();
            UpdateEntireObjectList();
        }

        private void exportINIToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog()
            {
                Filter = ".ini files|*.ini",
                FileName = Path.ChangeExtension(layoutSystem.CurrentlyOpenFileName, ".ini")
            };
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                layoutSystem.SaveINI(saveFileDialog.FileName);
            }
        }

        private void importINIToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog()
            {
                Filter = ".ini files|*.ini"
            };
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                layoutSystem.ImportINI(openFile.FileName);
                UpdateEntireObjectList();
            }
        }

        private void importLayoutFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog
            {
#if DEBUG
                Filter = ".bin files|*.bin|.dat files|*.dat"
#else
                Filter = layoutSystem.IsShadow ? ".dat files|*.dat" : ".bin files|*.bin"
#endif
            };
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                layoutSystem.ImportLayoutFile(openFile.FileName);
                UpdateEntireObjectList();
            }
        }

        private void importOBJToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog
            {
                Filter = ".obj files|*.obj"
            };
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                layoutSystem.ImportOBJ(openFile.FileName);
                UpdateEntireObjectList();
            }
        }

        private void buttonViewHere_Click(object sender, EventArgs e)
        {
            layoutSystem.ViewHere();
        }

        private void listBoxObjects_SelectedIndexChanged(object sender, EventArgs e)
        {
            ProgramIsChangingStuff = true;

            if (listBoxObjects.SelectedIndex < layoutSystem.GetSetObjectAmount())
            {
                layoutSystem.SelectedIndexChanged(listBoxObjects.SelectedIndex);
                if (listBoxObjects.SelectedIndex != -1)
                {
                    UpdateDisplayData();
                    UpdateGizmoPosition();
                }
                else
                {
                    ClearGizmos();
                }
            }

            ProgramIsChangingStuff = false;
        }
        
        private void ButtonAdd_Click(object sender, EventArgs e)
        {
            layoutSystem.AddNewSetObject();
            listBoxObjects.Items.Add(layoutSystem.GetSetObjectAt(layoutSystem.GetSetObjectAmount() - 1));
            listBoxObjects.SelectedIndex = layoutSystem.GetSetObjectAmount() - 1;
            UpdateSingleObjectList();
        }

        private void buttonCopy_Click(object sender, EventArgs e)
        {
            layoutSystem.CopySetObject();
            listBoxObjects.Items.Add(layoutSystem.GetSetObjectAt(layoutSystem.GetSetObjectAmount() - 1));
            listBoxObjects.SelectedIndex = layoutSystem.GetSetObjectAmount() - 1;
            UpdateSingleObjectList();
        }

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            if (layoutSystem.CurrentlySelectedIndex >= 0)
            {
                ProgramIsChangingStuff = true;
                int Temp = layoutSystem.RemoveSetObject();
                listBoxObjects.Items.RemoveAt(Temp);
                try { listBoxObjects.SelectedIndex = Temp; }
                catch { listBoxObjects.SelectedIndex = Temp - 1; }
                ProgramIsChangingStuff = false;
            }
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            layoutSystem.ClearList();
            UpdateEntireObjectList();
        }

        private void ComboBoxObject_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!ProgramIsChangingStuff)
            {
                layoutSystem.ComboBoxObjectChanged(ComboBoxObject.SelectedItem as ObjectEntry);
                UpdateSingleObjectList();
                PropertyGridMisc.SelectedObject = layoutSystem.GetSelectedObjectManager();
                UpdateDescriptionBox(layoutSystem.GetSelectedSetObjectEntry());
            }
        }

        private void NumericPos_ValueChanged(object sender, EventArgs e)
        {
            if (!ProgramIsChangingStuff)
            {
                layoutSystem.SetObjectPosition((float)NumericPosX.Value, (float)NumericPosY.Value, (float)NumericPosZ.Value);
            }
        }

        private void NumericRot_ValueChanged(object sender, EventArgs e)
        {
            if (!ProgramIsChangingStuff)
                layoutSystem.SetObjectRotation((float)NumericRotX.Value, (float)NumericRotY.Value, (float)NumericRotZ.Value);
        }

        private void NumericObjLink_ValueChanged(object sender, EventArgs e)
        {
            if (!ProgramIsChangingStuff)
            {
                layoutSystem.SetObjectLink((byte)NumericObjLink.Value);
                UpdateSingleObjectList();
            }
        }

        private void ButtonFindNextLink_Click(object sender, EventArgs e)
        {
            int newIndex = layoutSystem.FindNext();

            if (newIndex == listBoxObjects.SelectedIndex)
                MessageBox.Show("No other object has this same Link ID!");
            else
                listBoxObjects.SelectedIndex = newIndex;
        }

        private void NumericObjRend_ValueChanged(object sender, EventArgs e)
        {
            if (!ProgramIsChangingStuff)
                layoutSystem.SetObjectRend((byte)NumericObjRend.Value);
        }

        private void ButtonGetSpeed_Click(object sender, EventArgs e)
        {
            if (layoutSystem.GetSpeedMemory())
                UpdateDisplayData();
            else
                MessageBox.Show("Error collecting data", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void ButtonGetFly_Click(object sender, EventArgs e)
        {
            if (layoutSystem.GetFlyMemory())
                UpdateDisplayData();
            else
                MessageBox.Show("Error collecting data", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void ButtonGetPow_Click(object sender, EventArgs e)
        {
            if (layoutSystem.GetPowMemory())
                UpdateDisplayData();
            else
                MessageBox.Show("Error collecting data", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void ButtonSpeedRot_Click(object sender, EventArgs e)
        {
            if (layoutSystem.GetSpeedRotMemory())
                UpdateDisplayData();
            else
                MessageBox.Show("Error collecting data", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void ButtonFlyRot_Click(object sender, EventArgs e)
        {
            if (layoutSystem.GetFlyRotMemory())
                UpdateDisplayData();
            else
                MessageBox.Show("Error collecting data", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void ButtonPowRot_Click(object sender, EventArgs e)
        {
            if (layoutSystem.GetPowRotMemory())
                UpdateDisplayData();
            else
                MessageBox.Show("Error collecting data", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void ButtonTeleport_Click(object sender, EventArgs e)
        {
            if (!layoutSystem.Teleport())
                MessageBox.Show("Error writing data", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void buttonDrop_Click(object sender, EventArgs e)
        {
            layoutSystem.Drop();
            UpdateDisplayData();
        }

        private void buttonForceReload_Click(object sender, EventArgs e)
        {

        }

        public string GetOpenFileName()
        {
            return layoutSystem.CurrentlyOpenFileName;
        }

        public void OpenFile(string fileName)
        {
            layoutSystem.SelectedIndexChanged(-1);
            ProgramIsChangingStuff = true;
            layoutSystem.OpenLayoutFile(fileName);
            UpdateObjectComboBox();
            UpdateFileLabel();
        }

        private void SaveAs()
        {
            SaveFileDialog saveFile = new SaveFileDialog
            {
                Filter = layoutSystem.IsShadow ? "DAT Files|*.dat" : "BIN Files|*.bin",
                FileName = layoutSystem.CurrentlyOpenFileName
            };
            if (saveFile.ShowDialog() == DialogResult.OK)
            {
                layoutSystem.Save(saveFile.FileName);
                UpdateFileLabel();
            }
        }

        public void ScreenClicked(SharpRenderer renderer, Ray r, bool isMouseDown, bool showAllObjects)
        {
            if (isMouseDown)
                GizmoSelect(r);
            else
                listBoxObjects.SelectedIndex = layoutSystem.ScreenClicked(renderer, r, showAllObjects);
        }

        private void UpdateObjectComboBox()
        {
            ComboBoxObject.Items.Clear();
            ComboBoxObject.Items.AddRange(layoutSystem.GetActiveObjectEntries());

            UpdateEntireObjectList();
        }

        private void UpdateEntireObjectList()
        {
            ProgramIsChangingStuff = true;
            listBoxObjects.Items.Clear();

            for (int i = 0; i < layoutSystem.GetSetObjectAmount(); i++)
            {
                listBoxObjects.Items.Add(layoutSystem.GetSetObjectAt(i).ToString());
            }
            
            UpdateObjectAmountLabel();
            ProgramIsChangingStuff = false;
        }

        private void UpdateSingleObjectList()
        {
            if (listBoxObjects.SelectedIndex != -1)
                listBoxObjects.Items[listBoxObjects.SelectedIndex] = layoutSystem.GetSetObjectAt(listBoxObjects.SelectedIndex).ToString();
        }

        private void UpdateDisplayData()
        {
            if (listBoxObjects.SelectedIndex != -1)
            {
                ProgramIsChangingStuff = true;
                NumericPosX.Value = layoutSystem.GetPosX();
                NumericPosY.Value = layoutSystem.GetPosY();
                NumericPosZ.Value = layoutSystem.GetPosZ();
                NumericRotX.Value = layoutSystem.GetRotX();
                NumericRotY.Value = layoutSystem.GetRotY();
                NumericRotZ.Value = layoutSystem.GetRotZ();

                ComboBoxObject.SelectedItem = layoutSystem.GetSelectedSetObjectEntry();

                NumericObjLink.Value = layoutSystem.GetSelectedObjectLink();
                NumericObjRend.Value = layoutSystem.GetSelectedObjectRend();

                PropertyGridMisc.SelectedObject = layoutSystem.GetSelectedObjectManager();
                UpdateDescriptionBox(layoutSystem.GetSelectedSetObjectEntry());
                ProgramIsChangingStuff = false;
            }

            UpdateObjectAmountLabel();
        }

        private void UpdateObjectAmountLabel()
        {
            if (listBoxObjects.Items.Count == 0)
                objectAmountLabel.Text = "0 objects";
            else
            {
                if (layoutSystem.GetSelectedObject() == null)
                    objectAmountLabel.Text = layoutSystem.GetSetObjectAmount().ToString() + " objects";
                else
                    objectAmountLabel.Text = String.Format("{0}/{1} objects", layoutSystem.GetSelectedIndex() + 1, layoutSystem.GetSetObjectAmount());
            }
        }

        private void UpdateFileLabel()
        {
            if (layoutSystem.CurrentlyOpenFileName == null)
                openFileLabel.Text = "No file loaded";
            else
                openFileLabel.Text = layoutSystem.CurrentlyOpenFileName;
        }

        private void UpdateDescriptionBox(ObjectEntry objectEntry)
        {
            try
            {
                RichTextBoxDescription.Text = objectEntry.Description;
            }
            catch
            {
                RichTextBoxDescription.Text = "";
            }
        }

        public void RenderSetObjects(SharpRenderer renderer, bool drawEveryObject)
        {
            layoutSystem.RenderSetObjects(renderer, drawEveryObject);

            if (DrawGizmos)
                foreach (Gizmo g in gizmos)
                    g.Draw(renderer);
        }

        public void UpdateAllMatrices()
        {
            layoutSystem.UpdateAllMatrices();
        }

        public void UpdateSetParticleMatrices()
        {
            layoutSystem.UpdateSetParticleMatrices();
        }

        public IEnumerable<ObjectEntry> GetAllObjectEntries()
        {
            return layoutSystem.GetAllObjectEntries();
        }

        // Gizmos
        private Gizmo[] gizmos;
        private bool DrawGizmos = false;

        public void UpdateGizmoPosition()
        {
            UpdateGizmoPosition(layoutSystem.GetSelectedObject().Position, layoutSystem.GetSelectedObject().boundingBox.Size);
        }

        private void UpdateGizmoPosition(Vector3 position, Vector3 distance)
        {
            DrawGizmos = true;
            foreach (Gizmo g in gizmos)
                g.SetPosition(position, distance);
        }

        private void ClearGizmos()
        {
            DrawGizmos = false;
        }

        private bool GizmoSelect(Ray r)
        {
            if (!DrawGizmos)
                return false;

            float dist = 10000f;
            int index = -1;

            for (int g = 0; g < gizmos.Length; g++)
            {
                float? distance = gizmos[g].IntersectsWith(r);
                if (distance != null)
                {
                    if (distance < dist)
                    {
                        dist = (float)distance;
                        index = g;
                    }
                }
            }

            if (index == -1)
                return false;

            gizmos[index].isSelected = true;
            return true;
        }

        public void ScreenUnclicked()
        {
            foreach (Gizmo g in gizmos)
                g.isSelected = false;
        }

        public void MouseMoveX(SharpCamera camera, int distance)
        {
            // TODO: The yaw checking code is probably redundant since Sewer's camera code but is kept here for now.
            // It's not redundant, the movement needs to be done in a different direction depending on the yaw.

            if (gizmos[0].isSelected)
                NumericPosX.Value += (
                    (camera.ViewMatrix.Yaw >= -360 & camera.ViewMatrix.Yaw < -270) |
                    (camera.ViewMatrix.Yaw >= -90 & camera.ViewMatrix.Yaw < 90) |
                    (camera.ViewMatrix.Yaw >= 270)) ? distance / 2 : -distance / 2;
            else if (gizmos[2].isSelected)
                NumericPosZ.Value +=(
                    (camera.ViewMatrix.Yaw >= -180 & camera.ViewMatrix.Yaw < 0) |
                    (camera.ViewMatrix.Yaw >= 180)) ? distance / 2 : -distance / 2;
        }

        public void MouseMoveY(SharpCamera camera, int distance)
        {
            if (gizmos[1].isSelected)
                NumericPosY.Value -= distance / 2;
        }
    }
}
