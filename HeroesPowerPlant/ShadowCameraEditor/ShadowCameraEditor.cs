using System;
using System.Collections.Generic;
using System.IO;
using SharpDX;
using System.Windows.Forms;
using static HeroesPowerPlant.ShadowCameraEditor.ShadowCameraEditorFunctions;
using System.Linq;

namespace HeroesPowerPlant.ShadowCameraEditor {
    public partial class ShadowCameraEditor : Form {

        public string CurrentCameraFile;
        public ShadowCameraFileHeader header;
        bool ProgramIsChangingStuff;
        int CurrentlySelectedCamera = -1;
        private bool hasRemoved = false;

        public ShadowCameraEditor() {
            InitializeComponent();

            numericUpDown_i00.Maximum = Decimal.MaxValue;
            numericUpDown_i04.Maximum = Decimal.MaxValue;
            numericUpDown_i08.Maximum = Decimal.MaxValue;
            numericUpDown_i0C.Maximum = Decimal.MaxValue;
            numericUpDown_i10.Maximum = Decimal.MaxValue;
            numericUpDown_i14.Maximum = Decimal.MaxValue;
            numericUpDown_i18.Maximum = Decimal.MaxValue;
            numericUpDown_i1C.Maximum = Decimal.MaxValue;
            numericUpDown_TriggerPosX.Maximum = Decimal.MaxValue;
            numericUpDownTriggerPosY.Maximum = Decimal.MaxValue;
            numericUpDownTriggerPosZ.Maximum = Decimal.MaxValue;
            numericUpDown_TriggerRotX.Maximum = Decimal.MaxValue;
            numericUpDownTriggerRotY.Maximum = Decimal.MaxValue;
            numericUpDownTriggerRotZ.Maximum = Decimal.MaxValue;
            numericUpDown_TriggerScaleX.Maximum = Decimal.MaxValue;
            numericUpDownTriggerScaleY.Maximum = Decimal.MaxValue;
            numericUpDownTriggerScaleZ.Maximum = Decimal.MaxValue;
            numericUpDown_f44.Maximum = Decimal.MaxValue;
            numericUpDown_f48.Maximum = Decimal.MaxValue;
            numericUpDown_f4C.Maximum = Decimal.MaxValue;
            numericUpDown_PointA_X.Maximum = Decimal.MaxValue;
            numericUpDown_PointA_Y.Maximum = Decimal.MaxValue;
            numericUpDown_PointA_Z.Maximum = Decimal.MaxValue;
            numericUpDown_CameraRotation.Maximum = Decimal.MaxValue;
            numericUpDown_FOV_Height.Maximum = Decimal.MaxValue;
            numericUpDown_FOV_Width.Maximum = Decimal.MaxValue;
            numericUpDown_f68.Maximum = Decimal.MaxValue;
            numericUpDown_f6C.Maximum = Decimal.MaxValue;
            numericUpDown_f70.Maximum = Decimal.MaxValue;
            numericUpDown_f74.Maximum = Decimal.MaxValue;
            numericUpDown_f78.Maximum = Decimal.MaxValue;
            numericUpDown_f7C.Maximum = Decimal.MaxValue;
            numericUpDown_f80.Maximum = Decimal.MaxValue;
            numericUpDown_f84.Maximum = Decimal.MaxValue;
            numericUpDown_f88.Maximum = Decimal.MaxValue;
            numericUpDown_f8C.Maximum = Decimal.MaxValue;
            numericUpDown_CameraDistanceFromPlayer.Maximum = Decimal.MaxValue;
            numericUpDown_CameraHeightFromPlayer.Maximum = Decimal.MaxValue;
            numericUpDown_f98.Maximum = Decimal.MaxValue;
            numericUpDown_f9C.Maximum = Decimal.MaxValue;
            numericUpDown_fA0.Maximum = Decimal.MaxValue;
            numericUpDown_fA4.Maximum = Decimal.MaxValue;
            numericUpDown_fA8.Maximum = Decimal.MaxValue;
            numericUpDown_fAC.Maximum = Decimal.MaxValue;
            numericUpDown_TransitionTimeEnter.Maximum = Decimal.MaxValue;
            numericUpDown_TransitionTimeExit.Maximum = Decimal.MaxValue;
            numericUpDown_fB8.Maximum = Decimal.MaxValue;
            numericUpDown_fBC.Maximum = Decimal.MaxValue;
            numericUpDown_fC0.Maximum = Decimal.MaxValue;
            numericUpDown_fC4.Maximum = Decimal.MaxValue;
            numericUpDown_fC8.Maximum = Decimal.MaxValue;
            numericUpDown_fCC.Maximum = Decimal.MaxValue;
            numericUpDown_fD0.Maximum = Decimal.MaxValue;
            numericUpDown_fD4.Maximum = Decimal.MaxValue;
            numericUpDown_fD8.Maximum = Decimal.MaxValue;

            numericUpDown_i00.Minimum = Decimal.MinValue;
            numericUpDown_i04.Minimum = Decimal.MinValue;
            numericUpDown_i08.Minimum = Decimal.MinValue;
            numericUpDown_i0C.Minimum = Decimal.MinValue;
            numericUpDown_i10.Minimum = Decimal.MinValue;
            numericUpDown_i14.Minimum = Decimal.MinValue;
            numericUpDown_i18.Minimum = Decimal.MinValue;
            numericUpDown_i1C.Minimum = Decimal.MinValue;
            numericUpDown_TriggerPosX.Minimum = Decimal.MinValue;
            numericUpDownTriggerPosY.Minimum = Decimal.MinValue;
            numericUpDownTriggerPosZ.Minimum = Decimal.MinValue;
            numericUpDown_TriggerRotX.Minimum = Decimal.MinValue;
            numericUpDownTriggerRotY.Minimum = Decimal.MinValue;
            numericUpDownTriggerRotZ.Minimum = Decimal.MinValue;
            numericUpDown_TriggerScaleX.Minimum = Decimal.MinValue;
            numericUpDownTriggerScaleY.Minimum = Decimal.MinValue;
            numericUpDownTriggerScaleZ.Minimum = Decimal.MinValue;
            numericUpDown_f44.Minimum = Decimal.MinValue;
            numericUpDown_f48.Minimum = Decimal.MinValue;
            numericUpDown_f4C.Minimum = Decimal.MinValue;
            numericUpDown_PointA_X.Minimum = Decimal.MinValue;
            numericUpDown_PointA_Y.Minimum = Decimal.MinValue;
            numericUpDown_PointA_Z.Minimum = Decimal.MinValue;
            numericUpDown_CameraRotation.Minimum = Decimal.MinValue;
            numericUpDown_FOV_Height.Minimum = Decimal.MinValue;
            numericUpDown_FOV_Width.Minimum = Decimal.MinValue;
            numericUpDown_f68.Minimum = Decimal.MinValue;
            numericUpDown_f6C.Minimum = Decimal.MinValue;
            numericUpDown_f70.Minimum = Decimal.MinValue;
            numericUpDown_f74.Minimum = Decimal.MinValue;
            numericUpDown_f78.Minimum = Decimal.MinValue;
            numericUpDown_f7C.Minimum = Decimal.MinValue;
            numericUpDown_f80.Minimum = Decimal.MinValue;
            numericUpDown_f84.Minimum = Decimal.MinValue;
            numericUpDown_f88.Minimum = Decimal.MinValue;
            numericUpDown_f8C.Minimum = Decimal.MinValue;
            numericUpDown_CameraDistanceFromPlayer.Minimum = Decimal.MinValue;
            numericUpDown_CameraHeightFromPlayer.Minimum = Decimal.MinValue;
            numericUpDown_f98.Minimum = Decimal.MinValue;
            numericUpDown_f9C.Minimum = Decimal.MinValue;
            numericUpDown_fA0.Minimum = Decimal.MinValue;
            numericUpDown_fA4.Minimum = Decimal.MinValue;
            numericUpDown_fA8.Minimum = Decimal.MinValue;
            numericUpDown_fAC.Minimum = Decimal.MinValue;
            numericUpDown_TransitionTimeEnter.Minimum = Decimal.MinValue;
            numericUpDown_TransitionTimeExit.Minimum = Decimal.MinValue;
            numericUpDown_fB8.Minimum = Decimal.MinValue;
            numericUpDown_fBC.Minimum = Decimal.MinValue;
            numericUpDown_fC0.Minimum = Decimal.MinValue;
            numericUpDown_fC4.Minimum = Decimal.MinValue;
            numericUpDown_fC8.Minimum = Decimal.MinValue;
            numericUpDown_fCC.Minimum = Decimal.MinValue;
            numericUpDown_fD0.Minimum = Decimal.MinValue;
            numericUpDown_fD4.Minimum = Decimal.MinValue;
            numericUpDown_fD8.Minimum = Decimal.MinValue;
        }

        public void RenderCameras(SharpRenderer renderer)
        {
            Vector4 oldColor = renderer.normalColor;
            renderer.normalColor = new Vector4(0.6f, 0.25f, 0.7f, 0.8f);

            foreach (ShadowCamera c in ListBoxCameras.Items)
                if (renderer.frustum.Intersects(ref c.boundingBox))
                    c.Draw(renderer);

            renderer.normalColor = oldColor;
        }
        private void openToolStripMenuItem_Click(object sender, EventArgs e) {
            OpenFileDialog OpenCamera = new OpenFileDialog() {
                Filter = "DAT Files|*.dat"
            };
            if (OpenCamera.ShowDialog() == DialogResult.OK) {
                OpenFile(OpenCamera.FileName);
            }
        }

        public void OpenFile(string fileName) {
            if (File.Exists(fileName)) {
                CurrentCameraFile = fileName;

                ListBoxCameras.Items.Clear();
                (ShadowCameraFileHeader header, List<ShadowCamera> tempList) = ImportCameraFile(CurrentCameraFile);
                this.header = header;
                ListBoxCameras.Items.AddRange(tempList.ToArray());
                toolStripStatusFile.Text = "Loaded " + CurrentCameraFile;
                UpdateLabelCameraCount();

                ListBoxCameras.SelectedIndex = -1;
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CurrentCameraFile != null) {
                header.numberOfCameras = ListBoxCameras.Items.Count;
                SaveCameraFile(CurrentCameraFile, header, ListBoxCameras.Items.Cast<ShadowCamera>());
            } else
                saveAsToolStripMenuItem_Click(sender, e);
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog SaveCamera = new SaveFileDialog()
            {
                Filter = "DAT Files|*.dat",
                FileName = CurrentCameraFile
            };
            if (SaveCamera.ShowDialog() == DialogResult.OK)
            {
                CurrentCameraFile = SaveCamera.FileName;
                header.numberOfCameras = ListBoxCameras.Items.Count;
                SaveCameraFile(CurrentCameraFile, header, ListBoxCameras.Items.Cast<ShadowCamera>());
            }
        }

        private void UpdateLabelCameraCount() {
            toolStripStatusLabelCameraCount.Text = ListBoxCameras.Items.Count.ToString() + " cameras";
            if (CurrentlySelectedCamera == -1)
                toolStripStatusLabelCameraCount.Text = ListBoxCameras.Items.Count.ToString() + " cameras";
            else if (ListBoxCameras.Items.Count > 0)
                toolStripStatusLabelCameraCount.Text = "Camera " + (CurrentlySelectedCamera + 1).ToString() + "/" + ListBoxCameras.Items.Count.ToString();
        }

        private void ListBoxCameras_SelectedIndexChanged(object sender, EventArgs e) {
            ProgramIsChangingStuff = true;

            if (!hasRemoved & CurrentlySelectedCamera != -1)
                (ListBoxCameras.Items[CurrentlySelectedCamera] as ShadowCamera).isSelected = false;
            else if (hasRemoved) hasRemoved = false;

            CurrentlySelectedCamera = ListBoxCameras.SelectedIndex;

            if (CurrentlySelectedCamera != -1) {
                try {
                    ShadowCamera current = ListBoxCameras.Items[CurrentlySelectedCamera] as ShadowCamera;

                    current.isSelected = true;

                    numericUpDown_i00.Value = current.field_00;
                    numericUpDown_i04.Value = current.field_04;
                    numericUpDown_i08.Value = current.field_08;
                    numericUpDown_i0C.Value = current.field_0C;
                    numericUpDown_i10.Value = current.field_10;
                    numericUpDown_i14.Value = current.field_14;
                    numericUpDown_i18.Value = current.field_18;
                    numericUpDown_i1C.Value = current.field_1C;
                    numericUpDown_TriggerPosX.Value = (decimal)current.TriggerPosition.X;
                    numericUpDownTriggerPosY.Value = (decimal)current.TriggerPosition.Y;
                    numericUpDownTriggerPosZ.Value = (decimal)current.TriggerPosition.Z;
                    numericUpDown_TriggerRotX.Value = (decimal)(current.TriggerRotation.X * (180f / Math.PI)); //(current.TriggerRotationX * (360f / 65536f));
                    numericUpDownTriggerRotY.Value = (decimal)(current.TriggerRotation.Y * (180f / Math.PI)); //(current.TriggerRotationY * (360f / 65536f));
                    numericUpDownTriggerRotZ.Value = (decimal)(current.TriggerRotation.Z * (180f / Math.PI)); //(current.TriggerRotationZ * (360f / 65536f));
                    numericUpDown_TriggerScaleX.Value = (decimal)current.TriggerScale.X;
                    numericUpDownTriggerScaleY.Value = (decimal)current.TriggerScale.Y;
                    numericUpDownTriggerScaleZ.Value = (decimal)current.TriggerScale.Z;
                    numericUpDown_f44.Value = (decimal)current.field_44;
                    numericUpDown_f48.Value = (decimal)current.field_48;
                    numericUpDown_f4C.Value = (decimal)current.field_4C;
                    numericUpDown_PointA_X.Value = (decimal)current.PointA_X;
                    numericUpDown_PointA_Y.Value = (decimal)current.PointA_Y;
                    numericUpDown_PointA_Z.Value = (decimal)current.PointA_Z;
                    numericUpDown_CameraRotation.Value = (decimal)(current.CameraRotation * (180f / Math.PI));
                    numericUpDown_FOV_Height.Value = (decimal)(current.FOV_Height * (180f / Math.PI));
                    numericUpDown_FOV_Width.Value = (decimal)(current.FOV_Width * (180f / Math.PI));
                    numericUpDown_f68.Value = (decimal)current.field_68;
                    numericUpDown_f6C.Value = (decimal)current.field_6C;
                    numericUpDown_f70.Value = (decimal)current.field_70;
                    numericUpDown_f74.Value = (decimal)current.field_74;
                    numericUpDown_f78.Value = (decimal)current.field_78;
                    numericUpDown_f7C.Value = (decimal)current.field_7C;
                    numericUpDown_f80.Value = (decimal)current.field_80;
                    numericUpDown_f84.Value = (decimal)current.field_84;
                    numericUpDown_f88.Value = (decimal)current.field_88;
                    numericUpDown_f8C.Value = (decimal)current.field_8C;
                    numericUpDown_CameraDistanceFromPlayer.Value = (decimal)current.CameraDistanceFromPlayer;
                    numericUpDown_CameraHeightFromPlayer.Value = (decimal)current.CameraHeightFromPlayer;
                    numericUpDown_f98.Value = (decimal)current.field_98;
                    numericUpDown_f9C.Value = (decimal)current.field_9C;
                    numericUpDown_fA0.Value = (decimal)current.field_A0;
                    numericUpDown_fA4.Value = (decimal)current.field_A4;
                    numericUpDown_fA8.Value = (decimal)current.field_A8;
                    numericUpDown_fAC.Value = (decimal)current.field_AC;
                    numericUpDown_TransitionTimeEnter.Value = (decimal)current.TransitionTimeEnter;
                    numericUpDown_TransitionTimeExit.Value = (decimal)current.TransitionTimeExit;
                    numericUpDown_fB8.Value = (decimal)current.field_B8;
                    numericUpDown_fBC.Value = (decimal)current.field_BC;
                    numericUpDown_fC0.Value = (decimal)current.field_C0;
                    numericUpDown_fC4.Value = (decimal)current.field_C4;
                    numericUpDown_fC8.Value = (decimal)current.field_C8;
                    numericUpDown_fCC.Value = (decimal)current.field_CC;
                    numericUpDown_fD0.Value = (decimal)current.field_D0;
                    numericUpDown_fD4.Value = (decimal)current.field_D4;
                    numericUpDown_fD8.Value = (decimal)current.field_D8;
                } catch {
                    MessageBox.Show("Could not load this camera properly: one or more properties are unsupported.");
                }
            }

            UpdateLabelCameraCount();
            ProgramIsChangingStuff = false;
        }

        private void numericUpDown_ValueChanged(object sender, EventArgs e) {
            if (!ProgramIsChangingStuff & CurrentlySelectedCamera != -1) {
                ShadowCamera current = ListBoxCameras.Items[CurrentlySelectedCamera] as ShadowCamera;
                current.field_00 = (int)numericUpDown_i00.Value;
                current.field_04 = (int)numericUpDown_i04.Value;
                current.field_08 = (int)numericUpDown_i08.Value;
                current.field_0C = (int)numericUpDown_i0C.Value;
                current.field_10 = (int)numericUpDown_i10.Value;
                current.field_14 = (int)numericUpDown_i14.Value;
                current.field_18 = (int)numericUpDown_i18.Value;
                current.field_1C = (int)numericUpDown_i1C.Value;
                current.TriggerPosition = new Vector3((float)numericUpDown_TriggerPosX.Value, (float)numericUpDownTriggerPosY.Value, (float)numericUpDownTriggerPosZ.Value);
                current.TriggerRotation = new Vector3((float)((float)numericUpDown_TriggerRotX.Value / (180f / Math.PI)), (float)((float)numericUpDownTriggerRotY.Value / (180f / Math.PI)), (float)((float)numericUpDownTriggerRotZ.Value / (180f / Math.PI)));
                current.TriggerScale = new Vector3((float)numericUpDown_TriggerScaleX.Value, (float)numericUpDownTriggerScaleY.Value, (float)numericUpDownTriggerScaleZ.Value);
                current.field_44 = (float)numericUpDown_f44.Value;
                current.field_48 = (float)numericUpDown_f48.Value;
                current.field_4C = (float)numericUpDown_f4C.Value;
                current.PointA_X = (float)numericUpDown_PointA_X.Value;
                current.PointA_Y = (float)numericUpDown_PointA_Y.Value;
                current.PointA_Z = (float)numericUpDown_PointA_Z.Value;
                current.CameraRotation = (float)((float)numericUpDown_CameraRotation.Value / (180f / Math.PI));
                current.FOV_Height = (float)((float)numericUpDown_FOV_Height.Value / (180f / Math.PI));
                current.FOV_Width = (float)((float)numericUpDown_FOV_Width.Value / (180f / Math.PI));
                current.field_68 = (float)numericUpDown_f68.Value;
                current.field_6C = (float)numericUpDown_f6C.Value;
                current.field_70 = (float)numericUpDown_f70.Value;
                current.field_74 = (float)numericUpDown_f74.Value;
                current.field_78 = (float)numericUpDown_f78.Value;
                current.field_7C = (float)numericUpDown_f7C.Value;
                current.field_80 = (float)numericUpDown_f80.Value;
                current.field_84 = (float)numericUpDown_f84.Value;
                current.field_88 = (float)numericUpDown_f88.Value;
                current.field_8C = (float)numericUpDown_f8C.Value;
                current.CameraDistanceFromPlayer = (float)numericUpDown_CameraDistanceFromPlayer.Value;
                current.CameraHeightFromPlayer = (float)numericUpDown_CameraHeightFromPlayer.Value;
                current.field_98 = (float)numericUpDown_f98.Value;
                current.field_9C = (float)numericUpDown_f9C.Value;
                current.field_A0 = (float)numericUpDown_fA0.Value;
                current.field_A4 = (float)numericUpDown_fA4.Value;
                current.field_A8 = (float)numericUpDown_fA8.Value;
                current.field_AC = (float)numericUpDown_fAC.Value;
                current.TransitionTimeEnter = (float)numericUpDown_TransitionTimeEnter.Value;
                current.TransitionTimeExit = (float)numericUpDown_TransitionTimeExit.Value;
                current.field_B8 = (float)numericUpDown_fB8.Value;
                current.field_BC = (float)numericUpDown_fBC.Value;
                current.field_C0 = (float)numericUpDown_fC0.Value;
                current.field_C4 = (float)numericUpDown_fC4.Value;
                current.field_C8 = (float)numericUpDown_fC8.Value;
                current.field_CC = (float)numericUpDown_fCC.Value;
                current.field_D0 = (float)numericUpDown_fD0.Value;
                current.field_D4 = (float)numericUpDown_fD4.Value;
                current.field_D8 = (float)numericUpDown_fD8.Value;
                current.CreateTransformMatrix();
                ListBoxCameras.Items[CurrentlySelectedCamera] = current;
            }
        }

        public void ScreenClicked(Ray r) {
            int index = -1;

            float smallerDistance = 10000f;
            for (int i = 0; i < ListBoxCameras.Items.Count; i++) {
                if (((ShadowCamera)ListBoxCameras.Items[i]).isSelected) continue;

                float? distance = ((ShadowCamera)ListBoxCameras.Items[i]).IntersectsWith(r);
                if (distance != null)
                    if (distance < smallerDistance)
                        index = i;
            }

            ListBoxCameras.SelectedIndex = index;
        }

        private void buttonAdd_Click(object sender, EventArgs e) {
            ShadowCamera newCamera = new ShadowCamera() { TriggerPosition = Program.MainForm.renderer.Camera.GetPosition() };
            newCamera.CreateTransformMatrix();
            ListBoxCameras.Items.Add(newCamera);
            ListBoxCameras.SelectedIndex = ListBoxCameras.Items.Count - 1;
            UpdateLabelCameraCount();
        }

        private void buttonDuplicate_Click(object sender, EventArgs e) {
            ShadowCamera newCamera = new ShadowCamera((ShadowCamera)ListBoxCameras.Items[CurrentlySelectedCamera]);
            newCamera.CreateTransformMatrix();
            ListBoxCameras.Items.Add(newCamera);
            ListBoxCameras.SelectedIndex = ListBoxCameras.Items.Count - 1;
            UpdateLabelCameraCount();
        }

        private void buttonDelete_Click(object sender, EventArgs e) {
            if (CurrentlySelectedCamera == -1)
                return;

            int Save = CurrentlySelectedCamera;
            hasRemoved = true;
            ListBoxCameras.Items.RemoveAt(CurrentlySelectedCamera);

            if (Save < ListBoxCameras.Items.Count)
                ListBoxCameras.SelectedIndex = Save;
            else
                ListBoxCameras.SelectedIndex = ListBoxCameras.Items.Count - 1;

            UpdateLabelCameraCount();
        }

        private void buttonClear_Click(object sender, EventArgs e) {
            hasRemoved = true;
            ListBoxCameras.Items.Clear();
            UpdateLabelCameraCount();
        }
    }
}
