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
        public byte[] headerArray;
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
            numericUpDownTriggerPosX.Maximum = Decimal.MaxValue;
            numericUpDownTriggerPosY.Maximum = Decimal.MaxValue;
            numericUpDownTriggerPosZ.Maximum = Decimal.MaxValue;
            numericUpDownTriggerRotX.Maximum = Decimal.MaxValue;
            numericUpDownTriggerRotY.Maximum = Decimal.MaxValue;
            numericUpDownTriggerRotZ.Maximum = Decimal.MaxValue;
            numericUpDownTriggerScaleX.Maximum = Decimal.MaxValue;
            numericUpDownTriggerScaleY.Maximum = Decimal.MaxValue;
            numericUpDownTriggerScaleZ.Maximum = Decimal.MaxValue;
            numericUpDownCamPosX.Maximum = Decimal.MaxValue;
            numericUpDownCamPosY.Maximum = Decimal.MaxValue;
            numericUpDownCamPosZ.Maximum = Decimal.MaxValue;
            numericUpDown_fXX1.Maximum = Decimal.MaxValue;
            numericUpDown_fXX2.Maximum = Decimal.MaxValue;
            numericUpDown_fXX3.Maximum = Decimal.MaxValue;

            numericUpDown_i00.Minimum = Decimal.MinValue;
            numericUpDown_i04.Minimum = Decimal.MinValue;
            numericUpDown_i08.Minimum = Decimal.MinValue;
            numericUpDown_i0C.Minimum = Decimal.MinValue;
            numericUpDown_i10.Minimum = Decimal.MinValue;
            numericUpDown_i14.Minimum = Decimal.MinValue;
            numericUpDown_i18.Minimum = Decimal.MinValue;
            numericUpDown_i1C.Minimum = Decimal.MinValue;
            numericUpDownTriggerPosX.Minimum = Decimal.MinValue;
            numericUpDownTriggerPosY.Minimum = Decimal.MinValue;
            numericUpDownTriggerPosZ.Minimum = Decimal.MinValue;
            numericUpDownTriggerRotX.Minimum = Decimal.MinValue;
            numericUpDownTriggerRotY.Minimum = Decimal.MinValue;
            numericUpDownTriggerRotZ.Minimum = Decimal.MinValue;
            numericUpDownTriggerScaleX.Minimum = Decimal.MinValue;
            numericUpDownTriggerScaleY.Minimum = Decimal.MinValue;
            numericUpDownTriggerScaleZ.Minimum = Decimal.MinValue;
            numericUpDownCamPosX.Minimum = Decimal.MinValue;
            numericUpDownCamPosY.Minimum = Decimal.MinValue;
            numericUpDownCamPosZ.Minimum = Decimal.MinValue;
            numericUpDown_fXX1.Minimum = Decimal.MinValue;
            numericUpDown_fXX2.Minimum = Decimal.MinValue;
            numericUpDown_fXX3.Minimum = Decimal.MinValue;

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

        private void button1_Click(object sender, EventArgs e) {
            using BinaryReader camReader = new BinaryReader(new FileStream("C:\\Users\\dreamsyntax\\cam.dat", FileMode.Open));
            camReader.BaseStream.Position = 0;
            byte[] headerArray = camReader.ReadBytes(0x18);
            List<byte[]> cameraList = new List<byte[]>();
            float insertject = 8;
            while (camReader.BaseStream.Position != camReader.BaseStream.Length) {
                cameraList.Add(camReader.ReadBytes(0x18));
                cameraList.Add(BitConverter.GetBytes(insertject));
                camReader.ReadBytes(0x4); //trash
                cameraList.Add(camReader.ReadBytes(0xC0));
            }

            BinaryWriter CameraWriter = new BinaryWriter(new FileStream("C:\\Users\\dreamsyntax\\Desktop\\CameraResearch\\files\\stg0403\\stg0403_cam.dat", FileMode.Create));
            CameraWriter.Write(headerArray);
            foreach (byte[] i in cameraList) {
                CameraWriter.Write(i);
            }
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
                (byte[] header, List<ShadowCamera> tempList) = ImportCameraFile(CurrentCameraFile);
                headerArray = header;
                ListBoxCameras.Items.AddRange(tempList.ToArray());
                toolStripStatusFile.Text = "Loaded " + CurrentCameraFile;
                UpdateLabelCameraCount();

                ListBoxCameras.SelectedIndex = -1;
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CurrentCameraFile != null)
                SaveCameraFile(CurrentCameraFile, headerArray, ListBoxCameras.Items.Cast<ShadowCamera>());
            else
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
                SaveCameraFile(CurrentCameraFile, headerArray, ListBoxCameras.Items.Cast<ShadowCamera>());
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
                    numericUpDownTriggerPosX.Value = (decimal)current.TriggerPosition.X;
                    numericUpDownTriggerPosY.Value = (decimal)current.TriggerPosition.Y;
                    numericUpDownTriggerPosZ.Value = (decimal)current.TriggerPosition.Z;
                    numericUpDownTriggerRotX.Value = (decimal)(current.TriggerRotation.X * (180f / Math.PI)); //(current.TriggerRotationX * (360f / 65536f));
                    numericUpDownTriggerRotY.Value = (decimal)(current.TriggerRotation.Y * (180f / Math.PI)); //(current.TriggerRotationY * (360f / 65536f));
                    numericUpDownTriggerRotZ.Value = (decimal)(current.TriggerRotation.Z * (180f / Math.PI)); //(current.TriggerRotationZ * (360f / 65536f));
                    numericUpDownTriggerScaleX.Value = (decimal)current.TriggerScale.X;
                    numericUpDownTriggerScaleY.Value = (decimal)current.TriggerScale.Y;
                    numericUpDownTriggerScaleZ.Value = (decimal)current.TriggerScale.Z;
                    numericUpDownCamPosX.Value = (decimal)current.field_44;
                    numericUpDownCamPosY.Value = (decimal)current.field_48;
                    numericUpDownCamPosZ.Value = (decimal)current.field_4C;
                    numericUpDown_fXX1.Value = (decimal)(current.field_50);
                    numericUpDown_fXX2.Value = (decimal)(current.field_54);
                    numericUpDown_fXX3.Value = (decimal)(current.field_58);
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
                current.TriggerPosition = new Vector3((float)numericUpDownTriggerPosX.Value, (float)numericUpDownTriggerPosY.Value, (float)numericUpDownTriggerPosZ.Value);
                current.TriggerRotation = new Vector3((float)((float)numericUpDownTriggerRotX.Value / (180f / Math.PI)), (float)((float)numericUpDownTriggerRotY.Value / (180f / Math.PI)), (float)((float)numericUpDownTriggerRotZ.Value / (180f / Math.PI)));
                current.TriggerScale = new Vector3((float)numericUpDownTriggerScaleX.Value, (float)numericUpDownTriggerScaleY.Value, (float)numericUpDownTriggerScaleZ.Value);
                current.field_44 = (float)numericUpDownCamPosX.Value;
                current.field_48 = (float)numericUpDownCamPosY.Value;
                current.field_4C = (float)numericUpDownCamPosZ.Value;
                current.field_50 = (float)numericUpDown_fXX1.Value;
                current.field_54 = (float)numericUpDown_fXX2.Value;
                current.field_58 = (float)numericUpDown_fXX3.Value;
                current.CreateTransformMatrix();
                ListBoxCameras.Items[CurrentlySelectedCamera] = current;
            }
        }
    }
}
