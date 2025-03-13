﻿using Ookii.Dialogs.WinForms;
using SharpDX;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using static HeroesPowerPlant.ShadowCameraEditor.ShadowCameraEditorFunctions;

namespace HeroesPowerPlant.ShadowCameraEditor
{
    public partial class ShadowCameraEditor : Form, IUnsavedChanges
    {
        public string CurrentCameraFile;
        public ShadowCameraFileHeader header;
        bool ProgramIsChangingStuff;
        int CurrentlySelectedCamera = -1;
        private bool hasRemoved = false;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool UnsavedChanges { get; private set; } = false;

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.WindowsShutDown)
                return;
            if (e.CloseReason == CloseReason.FormOwnerClosing)
                return;

            e.Cancel = true;
            Hide();
        }

        public ShadowCameraEditor()
        {
            InitializeComponent();
            comboBox_cameraMode.DataSource = Enum.GetValues(typeof(ShadowCameraMode));
            comboBox_cameraTriggerShape.DataSource = Enum.GetValues(typeof(ShadowCameraTriggerShape));

            numericUpDown_cameraNumber.Maximum = Decimal.MaxValue;
            numericUpDown_CameraPersistFlag.Maximum = Decimal.MaxValue;
            numericUpDown_i0C.Maximum = Decimal.MaxValue;
            numericUpDown_i10.Maximum = Decimal.MaxValue;
            numericUpDown_i14.Maximum = Decimal.MaxValue;
            numericUpDown_LookBLinkId.Maximum = Decimal.MaxValue;
            numericUpDown_TriggerPosX.Maximum = Decimal.MaxValue;
            numericUpDown_TriggerPosY.Maximum = Decimal.MaxValue;
            numericUpDown_TriggerPosZ.Maximum = Decimal.MaxValue;
            numericUpDown_TriggerRotX.Maximum = Decimal.MaxValue;
            numericUpDownTriggerRotY.Maximum = Decimal.MaxValue;
            numericUpDownTriggerRotZ.Maximum = Decimal.MaxValue;
            numericUpDown_TriggerScaleX.Maximum = Decimal.MaxValue;
            numericUpDownTriggerScaleY.Maximum = Decimal.MaxValue;
            numericUpDownTriggerScaleZ.Maximum = Decimal.MaxValue;
            numericUpDown_PointA_LookFrom_X.Maximum = Decimal.MaxValue;
            numericUpDown_PointA_LookFrom_Y.Maximum = Decimal.MaxValue;
            numericUpDown_PointA_LookFrom_Z.Maximum = Decimal.MaxValue;
            numericUpDown_PointA_LookAt_X.Maximum = Decimal.MaxValue;
            numericUpDown_PointA_LookAt_Y.Maximum = Decimal.MaxValue;
            numericUpDown_PointA_LookAt_Z.Maximum = Decimal.MaxValue;
            numericUpDown_CameraRotation.Maximum = Decimal.MaxValue;
            numericUpDown_FOV_Height.Maximum = Decimal.MaxValue;
            numericUpDown_FOV_Width.Maximum = Decimal.MaxValue;
            numericUpDown_f68.Maximum = Decimal.MaxValue;
            numericUpDown_f6C.Maximum = Decimal.MaxValue;
            numericUpDown_f70.Maximum = Decimal.MaxValue;
            numericUpDown_f74.Maximum = Decimal.MaxValue;
            numericUpDown_PointB_LookFrom_X.Maximum = Decimal.MaxValue;
            numericUpDown_PointB_LookFrom_Y.Maximum = Decimal.MaxValue;
            numericUpDown_PointB_LookFrom_Z.Maximum = Decimal.MaxValue;
            numericUpDown_PointB_LookAt_X.Maximum = Decimal.MaxValue;
            numericUpDown_PointB_LookAt_Y.Maximum = Decimal.MaxValue;
            numericUpDown_PointB_LookAt_Z.Maximum = Decimal.MaxValue;
            numericUpDown_CameraDistanceFromPlayerLookA.Maximum = Decimal.MaxValue;
            numericUpDown_CameraHeightFromPlayerLookA.Maximum = Decimal.MaxValue;
            numericUpDown_CameraDistanceFromPlayerLookB.Maximum = Decimal.MaxValue;
            numericUpDown_CameraHeightFromPlayerLookB.Maximum = Decimal.MaxValue;
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

            numericUpDown_cameraNumber.Minimum = Decimal.MinValue;
            numericUpDown_CameraPersistFlag.Minimum = Decimal.MinValue;
            numericUpDown_i0C.Minimum = Decimal.MinValue;
            numericUpDown_i10.Minimum = Decimal.MinValue;
            numericUpDown_i14.Minimum = Decimal.MinValue;
            numericUpDown_LookBLinkId.Minimum = Decimal.MinValue;
            numericUpDown_TriggerPosX.Minimum = Decimal.MinValue;
            numericUpDown_TriggerPosY.Minimum = Decimal.MinValue;
            numericUpDown_TriggerPosZ.Minimum = Decimal.MinValue;
            numericUpDown_TriggerRotX.Minimum = Decimal.MinValue;
            numericUpDownTriggerRotY.Minimum = Decimal.MinValue;
            numericUpDownTriggerRotZ.Minimum = Decimal.MinValue;
            numericUpDown_TriggerScaleX.Minimum = Decimal.MinValue;
            numericUpDownTriggerScaleY.Minimum = Decimal.MinValue;
            numericUpDownTriggerScaleZ.Minimum = Decimal.MinValue;
            numericUpDown_PointA_LookFrom_X.Minimum = Decimal.MinValue;
            numericUpDown_PointA_LookFrom_Y.Minimum = Decimal.MinValue;
            numericUpDown_PointA_LookFrom_Z.Minimum = Decimal.MinValue;
            numericUpDown_PointA_LookAt_X.Minimum = Decimal.MinValue;
            numericUpDown_PointA_LookAt_Y.Minimum = Decimal.MinValue;
            numericUpDown_PointA_LookAt_Z.Minimum = Decimal.MinValue;
            numericUpDown_CameraRotation.Minimum = Decimal.MinValue;
            numericUpDown_FOV_Height.Minimum = Decimal.MinValue;
            numericUpDown_FOV_Width.Minimum = Decimal.MinValue;
            numericUpDown_f68.Minimum = Decimal.MinValue;
            numericUpDown_f6C.Minimum = Decimal.MinValue;
            numericUpDown_f70.Minimum = Decimal.MinValue;
            numericUpDown_f74.Minimum = Decimal.MinValue;
            numericUpDown_PointB_LookFrom_X.Minimum = Decimal.MinValue;
            numericUpDown_PointB_LookFrom_Y.Minimum = Decimal.MinValue;
            numericUpDown_PointB_LookFrom_Z.Minimum = Decimal.MinValue;
            numericUpDown_PointB_LookAt_X.Minimum = Decimal.MinValue;
            numericUpDown_PointB_LookAt_Y.Minimum = Decimal.MinValue;
            numericUpDown_PointB_LookAt_Z.Minimum = Decimal.MinValue;
            numericUpDown_CameraDistanceFromPlayerLookA.Minimum = Decimal.MinValue;
            numericUpDown_CameraHeightFromPlayerLookA.Minimum = Decimal.MinValue;
            numericUpDown_CameraDistanceFromPlayerLookB.Minimum = Decimal.MinValue;
            numericUpDown_CameraHeightFromPlayerLookB.Minimum = Decimal.MinValue;
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

        public void New()
        {
            CurrentCameraFile = null;
            header = null;
            CurrentlySelectedCamera = -1;
            ListBoxCameras.Items.Clear();
            toolStripStatusFile.Text = "No file loaded";
            UpdateLabelCameraCount();
            UnsavedChanges = false;
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

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (UnsavedChanges)
            {
                var result = Extensions.UnsavedChangesMessageBox(Text);
                if (result == DialogResult.Yes)
                {
                    saveToolStripMenuItem_Click(sender, e);
                    if (UnsavedChanges)
                        return;
                }
                else if (result == DialogResult.Cancel)
                    return;
            }

            VistaOpenFileDialog OpenCamera = new VistaOpenFileDialog()
            {
                Filter = "DAT Files|*.dat"
            };
            if (OpenCamera.ShowDialog() == DialogResult.OK)
            {
                OpenFile(OpenCamera.FileName);
            }
        }

        public void OpenFile(string fileName)
        {
            if (File.Exists(fileName))
            {
                CurrentCameraFile = fileName;

                ListBoxCameras.Items.Clear();
                (ShadowCameraFileHeader header, List<ShadowCamera> tempList) = ImportCameraFile(CurrentCameraFile);
                this.header = header;
                ListBoxCameras.Items.AddRange(tempList.ToArray());
                toolStripStatusFile.Text = "Loaded " + CurrentCameraFile;
                UpdateLabelCameraCount();

                ListBoxCameras.SelectedIndex = -1;
                CurrentlySelectedCamera = -1;
                UnsavedChanges = false;
            }
        }

        public void Save()
        {
            saveToolStripMenuItem_Click(null, null);
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CurrentCameraFile != null)
            {
                header.numberOfCameras = ListBoxCameras.Items.Count;
                SaveCameraFile(CurrentCameraFile, header, ListBoxCameras.Items.Cast<ShadowCamera>());
                UnsavedChanges = false;
            }
            else
                saveAsToolStripMenuItem_Click(sender, e);
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            VistaSaveFileDialog SaveCamera = new VistaSaveFileDialog()
            {
                Filter = "DAT Files|*.dat",
            };
            if (SaveCamera.ShowDialog() == DialogResult.OK)
            {
                CurrentCameraFile = SaveCamera.FileName;
                header.numberOfCameras = ListBoxCameras.Items.Count;
                SaveCameraFile(CurrentCameraFile, header, ListBoxCameras.Items.Cast<ShadowCamera>());
                UnsavedChanges = false;
            }
        }

        private void UpdateLabelCameraCount()
        {
            toolStripStatusLabelCameraCount.Text = ListBoxCameras.Items.Count.ToString() + " cameras";
            if (CurrentlySelectedCamera == -1)
                toolStripStatusLabelCameraCount.Text = ListBoxCameras.Items.Count.ToString() + " cameras";
            else if (ListBoxCameras.Items.Count > 0)
                toolStripStatusLabelCameraCount.Text = "Camera " + (CurrentlySelectedCamera + 1).ToString() + "/" + ListBoxCameras.Items.Count.ToString();
        }

        private void ListBoxCameras_SelectedIndexChanged(object sender, EventArgs e)
        {
            ProgramIsChangingStuff = true;

            if (!hasRemoved & CurrentlySelectedCamera != -1)
                (ListBoxCameras.Items[CurrentlySelectedCamera] as ShadowCamera).isSelected = false;
            else if (hasRemoved)
                hasRemoved = false;

            CurrentlySelectedCamera = ListBoxCameras.SelectedIndex;

            if (CurrentlySelectedCamera != -1)
            {
                try
                {
                    ShadowCamera current = ListBoxCameras.Items[CurrentlySelectedCamera] as ShadowCamera;

                    current.isSelected = true;

                    numericUpDown_cameraNumber.Value = current.CameraNumber;
                    comboBox_cameraMode.SelectedItem = current.CameraMode;
                    numericUpDown_CameraPersistFlag.Value = current.CameraPersistFlag;
                    numericUpDown_i0C.Value = current.field_0C;
                    numericUpDown_i10.Value = current.field_10;
                    numericUpDown_i14.Value = current.field_14;
                    numericUpDown_LookBLinkId.Value = current.LookBLinkId;
                    comboBox_cameraTriggerShape.SelectedItem = current.TriggerShape;
                    numericUpDown_TriggerPosX.Value = (decimal)current.TriggerPosition.X;
                    numericUpDown_TriggerPosY.Value = (decimal)current.TriggerPosition.Y;
                    numericUpDown_TriggerPosZ.Value = (decimal)current.TriggerPosition.Z;
                    numericUpDown_TriggerRotX.Value = (decimal)(current.TriggerRotation.X * (180f / Math.PI));
                    numericUpDownTriggerRotY.Value = (decimal)(current.TriggerRotation.Y * (180f / Math.PI));
                    numericUpDownTriggerRotZ.Value = (decimal)(current.TriggerRotation.Z * (180f / Math.PI));
                    numericUpDown_TriggerScaleX.Value = (decimal)current.TriggerScale.X;
                    numericUpDownTriggerScaleY.Value = (decimal)current.TriggerScale.Y;
                    numericUpDownTriggerScaleZ.Value = (decimal)current.TriggerScale.Z;
                    numericUpDown_PointA_LookFrom_X.Value = (decimal)current.PointA_LookFrom_X;
                    numericUpDown_PointA_LookFrom_Y.Value = (decimal)current.PointA_LookFrom_Y;
                    numericUpDown_PointA_LookFrom_Z.Value = (decimal)current.PointA_LookFrom_Z;
                    numericUpDown_PointA_LookAt_X.Value = (decimal)current.PointA_LookAt_X;
                    numericUpDown_PointA_LookAt_Y.Value = (decimal)current.PointA_LookAt_Y;
                    numericUpDown_PointA_LookAt_Z.Value = (decimal)current.PointA_LookAt_Z;
                    numericUpDown_CameraRotation.Value = (decimal)(current.CameraRotation * (180f / Math.PI));
                    numericUpDown_FOV_Height.Value = (decimal)(current.FOV_Height * (180f / Math.PI));
                    numericUpDown_FOV_Width.Value = (decimal)(current.FOV_Width * (180f / Math.PI));
                    numericUpDown_f68.Value = (decimal)current.field_68;
                    numericUpDown_f6C.Value = (decimal)current.field_6C;
                    numericUpDown_f70.Value = (decimal)current.field_70;
                    numericUpDown_f74.Value = (decimal)current.field_74;
                    numericUpDown_PointB_LookFrom_X.Value = (decimal)current.PointB_LookFrom_X;
                    numericUpDown_PointB_LookFrom_Y.Value = (decimal)current.PointB_LookFrom_Y;
                    numericUpDown_PointB_LookFrom_Z.Value = (decimal)current.PointB_LookFrom_Z;
                    numericUpDown_PointB_LookAt_X.Value = (decimal)current.PointB_LookAt_X;
                    numericUpDown_PointB_LookAt_Y.Value = (decimal)current.PointB_LookAt_Y;
                    numericUpDown_PointB_LookAt_Z.Value = (decimal)current.PointB_LookAt_Z;
                    numericUpDown_CameraDistanceFromPlayerLookA.Value = (decimal)current.CameraDistanceFromPlayerLookA;
                    numericUpDown_CameraHeightFromPlayerLookA.Value = (decimal)current.CameraHeightFromPlayerLookA;
                    numericUpDown_CameraDistanceFromPlayerLookB.Value = (decimal)current.CameraDistanceFromPlayerLookB;
                    numericUpDown_CameraHeightFromPlayerLookB.Value = (decimal)current.CameraHeightFromPlayerLookB;
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
                }
                catch
                {
                    MessageBox.Show("Could not load this camera properly: one or more properties are unsupported.");
                }
            }

            UpdateLabelCameraCount();
            ProgramIsChangingStuff = false;
        }

        private void CameraData_ValueChanged(object sender, EventArgs e)
        {
            if (!ProgramIsChangingStuff & CurrentlySelectedCamera != -1)
            {
                ShadowCamera current = ListBoxCameras.Items[CurrentlySelectedCamera] as ShadowCamera;
                current.CameraNumber = (int)numericUpDown_cameraNumber.Value;
                current.CameraMode = (ShadowCameraMode)comboBox_cameraMode.SelectedItem;
                current.CameraPersistFlag = (int)numericUpDown_CameraPersistFlag.Value;
                current.field_0C = (int)numericUpDown_i0C.Value;
                current.field_10 = (int)numericUpDown_i10.Value;
                current.field_14 = (int)numericUpDown_i14.Value;
                current.LookBLinkId = (int)numericUpDown_LookBLinkId.Value;
                current.TriggerShape = (ShadowCameraTriggerShape)comboBox_cameraTriggerShape.SelectedItem;
                current.TriggerPosition = new Vector3((float)numericUpDown_TriggerPosX.Value, (float)numericUpDown_TriggerPosY.Value, (float)numericUpDown_TriggerPosZ.Value);
                current.TriggerRotation = new Vector3((float)((float)numericUpDown_TriggerRotX.Value / (180f / Math.PI)), (float)((float)numericUpDownTriggerRotY.Value / (180f / Math.PI)), (float)((float)numericUpDownTriggerRotZ.Value / (180f / Math.PI)));
                current.TriggerScale = new Vector3((float)numericUpDown_TriggerScaleX.Value, (float)numericUpDownTriggerScaleY.Value, (float)numericUpDownTriggerScaleZ.Value);
                current.PointA_LookFrom_X = (float)numericUpDown_PointA_LookFrom_X.Value;
                current.PointA_LookFrom_Y = (float)numericUpDown_PointA_LookFrom_Y.Value;
                current.PointA_LookFrom_Z = (float)numericUpDown_PointA_LookFrom_Z.Value;
                current.PointA_LookAt_X = (float)numericUpDown_PointA_LookAt_X.Value;
                current.PointA_LookAt_Y = (float)numericUpDown_PointA_LookAt_Y.Value;
                current.PointA_LookAt_Z = (float)numericUpDown_PointA_LookAt_Z.Value;
                current.CameraRotation = (float)((float)numericUpDown_CameraRotation.Value / (180f / Math.PI));
                current.FOV_Height = (float)((float)numericUpDown_FOV_Height.Value / (180f / Math.PI));
                current.FOV_Width = (float)((float)numericUpDown_FOV_Width.Value / (180f / Math.PI));
                current.field_68 = (float)numericUpDown_f68.Value;
                current.field_6C = (float)numericUpDown_f6C.Value;
                current.field_70 = (float)numericUpDown_f70.Value;
                current.field_74 = (float)numericUpDown_f74.Value;
                current.PointB_LookFrom_X = (float)numericUpDown_PointB_LookFrom_X.Value;
                current.PointB_LookFrom_Y = (float)numericUpDown_PointB_LookFrom_Y.Value;
                current.PointB_LookFrom_Z = (float)numericUpDown_PointB_LookFrom_Z.Value;
                current.PointB_LookAt_X = (float)numericUpDown_PointB_LookAt_X.Value;
                current.PointB_LookAt_Y = (float)numericUpDown_PointB_LookAt_Y.Value;
                current.PointB_LookAt_Z = (float)numericUpDown_PointB_LookAt_Z.Value;
                current.CameraDistanceFromPlayerLookA = (float)numericUpDown_CameraDistanceFromPlayerLookA.Value;
                current.CameraHeightFromPlayerLookA = (float)numericUpDown_CameraHeightFromPlayerLookA.Value;
                current.CameraDistanceFromPlayerLookB = (float)numericUpDown_CameraDistanceFromPlayerLookB.Value;
                current.CameraHeightFromPlayerLookB = (float)numericUpDown_CameraHeightFromPlayerLookB.Value;
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
                UnsavedChanges = true;
            }
        }

        public void ScreenClicked(Ray r)
        {
            int index = -1;

            float smallerDistance = 10000f;
            for (int i = 0; i < ListBoxCameras.Items.Count; i++)
            {
                if (((ShadowCamera)ListBoxCameras.Items[i]).isSelected)
                    continue;

                float? distance = ((ShadowCamera)ListBoxCameras.Items[i]).IntersectsWith(r);
                if (distance != null)
                    if (distance < smallerDistance)
                        index = i;
            }

            ListBoxCameras.SelectedIndex = index;
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            ShadowCamera newCamera = new ShadowCamera() { TriggerPosition = Program.MainForm.renderer.Camera.GetPosition() };
            newCamera.CreateTransformMatrix();
            ListBoxCameras.Items.Add(newCamera);
            ListBoxCameras.SelectedIndex = ListBoxCameras.Items.Count - 1;
            UpdateLabelCameraCount();
            UnsavedChanges = true;
        }

        private void buttonDuplicate_Click(object sender, EventArgs e)
        {
            ShadowCamera newCamera = new ShadowCamera((ShadowCamera)ListBoxCameras.Items[CurrentlySelectedCamera]);
            newCamera.CreateTransformMatrix();
            ListBoxCameras.Items.Add(newCamera);
            ListBoxCameras.SelectedIndex = ListBoxCameras.Items.Count - 1;
            UpdateLabelCameraCount();
            UnsavedChanges = true;
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
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
            UnsavedChanges = true;
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            hasRemoved = true;
            ListBoxCameras.Items.Clear();
            UpdateLabelCameraCount();
            UnsavedChanges = true;
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (UnsavedChanges)
            {
                var result = Extensions.UnsavedChangesMessageBox(Text);
                if (result == DialogResult.Yes)
                {
                    saveToolStripMenuItem_Click(sender, e);
                    if (UnsavedChanges)
                        return;
                }
                else if (result == DialogResult.Cancel)
                    return;
            }

            New();
        }

        private void sortByDistanceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IEnumerable<ShadowCamera> cameras = ListBoxCameras.Items.Cast<ShadowCamera>();
            cameras = cameras.OrderBy(c => c.GetDistance()).ToList();
            ListBoxCameras.Items.Clear();
            ListBoxCameras.Items.AddRange(cameras.ToArray());
            UnsavedChanges = true;
        }

        private void sortByCameraNumberToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IEnumerable<ShadowCamera> cameras = ListBoxCameras.Items.Cast<ShadowCamera>();
            cameras = cameras.OrderBy(c => c.CameraNumber).ToList();
            ListBoxCameras.Items.Clear();
            ListBoxCameras.Items.AddRange(cameras.ToArray());
            UnsavedChanges = true;
        }

        private void ViewPosition(float positionX, float positionY, float positionZ)
        {
            ViewPosition(new Vector3(positionX, positionY, positionZ));
        }

        private void ViewPosition(Vector3 position)
        {
            Program.MainForm.renderer.Camera.SetPosition(position);
        }

        private void Button_View_LookFromPointA_Click(object sender, EventArgs e)
        {
            ViewPosition((float)numericUpDown_PointA_LookFrom_X.Value, (float)numericUpDown_PointA_LookFrom_Y.Value, (float)numericUpDown_PointA_LookFrom_Z.Value);
        }

        private void Button_Set_LookFromPointA_Click(object sender, EventArgs e)
        {
            Vector3 position = Program.MainForm.renderer.Camera.GetPosition();
            numericUpDown_PointA_LookFrom_X.Value = (decimal)position.X;
            numericUpDown_PointA_LookFrom_Y.Value = (decimal)position.Y;
            numericUpDown_PointA_LookFrom_Z.Value = (decimal)position.Z;
        }

        private void Button_View_LookAtPointA_Click(object sender, EventArgs e)
        {
            ViewPosition((float)numericUpDown_PointA_LookAt_X.Value, (float)numericUpDown_PointA_LookAt_Y.Value, (float)numericUpDown_PointA_LookAt_Z.Value);
        }

        private void Button_Set_LookAtPointA_Click(object sender, EventArgs e)
        {
            Vector3 position = Program.MainForm.renderer.Camera.GetPosition();
            numericUpDown_PointA_LookAt_X.Value = (decimal)position.X;
            numericUpDown_PointA_LookAt_Y.Value = (decimal)position.Y;
            numericUpDown_PointA_LookAt_Z.Value = (decimal)position.Z;
        }

        private void Button_View_LookFromPointB_Click(object sender, EventArgs e)
        {
            ViewPosition((float)numericUpDown_PointB_LookFrom_X.Value, (float)numericUpDown_PointB_LookFrom_Y.Value, (float)numericUpDown_PointB_LookFrom_Z.Value);
        }

        private void Button_Set_LookFromPointB_Click(object sender, EventArgs e)
        {
            Vector3 position = Program.MainForm.renderer.Camera.GetPosition();
            numericUpDown_PointB_LookFrom_X.Value = (decimal)position.X;
            numericUpDown_PointB_LookFrom_Y.Value = (decimal)position.Y;
            numericUpDown_PointB_LookFrom_Z.Value = (decimal)position.Z;
        }

        private void Button_View_LookAtPointB_Click(object sender, EventArgs e)
        {
            ViewPosition((float)numericUpDown_PointB_LookAt_X.Value, (float)numericUpDown_PointB_LookAt_Y.Value, (float)numericUpDown_PointB_LookAt_Z.Value);
        }

        private void Button_Set_LookAtPointB_Click(object sender, EventArgs e)
        {
            Vector3 position = Program.MainForm.renderer.Camera.GetPosition();
            numericUpDown_PointB_LookAt_X.Value = (decimal)position.X;
            numericUpDown_PointB_LookAt_Y.Value = (decimal)position.Y;
            numericUpDown_PointB_LookAt_Z.Value = (decimal)position.Z;
        }

        private void Button_View_Trigger_Position_Click(object sender, EventArgs e)
        {
            ViewPosition((float)numericUpDown_TriggerPosX.Value, (float)numericUpDown_TriggerPosY.Value, (float)numericUpDown_TriggerPosZ.Value);
        }

        private void Button_Set_TriggerPosition_Click(object sender, EventArgs e)
        {
            Vector3 position = Program.MainForm.renderer.Camera.GetPosition();
            numericUpDown_TriggerPosX.Value = (decimal)position.X;
            numericUpDown_TriggerPosY.Value = (decimal)position.Y;
            numericUpDown_TriggerPosZ.Value = (decimal)position.Z;
        }
    }
}
