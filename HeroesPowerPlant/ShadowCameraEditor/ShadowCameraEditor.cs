using System;
using System.Collections.Generic;
using System.IO;
using SharpDX;
using System.Windows.Forms;
using static HeroesPowerPlant.ShadowCameraEditor.ShadowCameraEditorFunctions;


namespace HeroesPowerPlant.ShadowCameraEditor {
    public partial class ShadowCameraEditor : Form {

        public string CurrentCameraFile;
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
                ListBoxCameras.Items.AddRange(ImportCameraFile(CurrentCameraFile).ToArray());

                toolStripStatusFile.Text = "Loaded " + CurrentCameraFile;
                UpdateLabelCameraCount();

                ListBoxCameras.SelectedIndex = -1;
            }
        }
        private void UpdateLabelCameraCount() {
            toolStripStatusLabelCameraCount.Text = ListBoxCameras.Items.Count.ToString() + " cameras";
            /*if (CurrentlySelectedCamera == -1)
                toolStripStatusLabelCameraCount.Text = ListBoxCameras.Items.Count.ToString() + " cameras";
            else if (ListBoxCameras.Items.Count > 0)
                toolStripStatusLabelCameraCount.Text = "Camera " + (CurrentlySelectedCamera + 1).ToString() + "/" + ListBoxCameras.Items.Count.ToString();
        */
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
                current.TriggerRotation = new Vector3(((float)numericUpDownTriggerRotX.Value) / (180f / Math.PI))
                /*
                current.CameraType = (int)numericUpDownCamType.Value;
                current.CameraSpeed = (int)numericUpDownCamSpeed.Value;
                current.Integer3 = (int)numericUpDown3.Value;
                current.ActivationType = (int)numericUpDownActType.Value;
                current.TriggerShape = (int)numericUpDownTrigShape.Value;
                current.TriggerPosition = new Vector3((float)numericUpDownColPosX.Value, (float)numericUpDownColPosY.Value, (float)numericUpDownColPosZ.Value);
                current.TriggerRotX = ReadWriteCommon.DegreesToBAMS((float)numericUpDownColRotX.Value);
                current.TriggerRotY = ReadWriteCommon.DegreesToBAMS((float)numericUpDownColRotY.Value);
                current.TriggerRotZ = ReadWriteCommon.DegreesToBAMS((float)numericUpDownColRotZ.Value);
                current.TriggerScale = new Vector3((float)numericUpDownColSclX.Value, (float)numericUpDownColSclY.Value, (float)numericUpDownColSclZ.Value);
                current.CamPos = new Vector3((float)numericUpDownCamPosX.Value, (float)numericUpDownCamPosY.Value, (float)numericUpDownCamPosZ.Value);
                current.CamRotX = ReadWriteCommon.DegreesToBAMS((float)numericUpDownCamRotX.Value);
                current.CamRotY = ReadWriteCommon.DegreesToBAMS((float)numericUpDownCamRotY.Value);
                current.CamRotZ = ReadWriteCommon.DegreesToBAMS((float)numericUpDownCamRotZ.Value);
                current.PointA = new Vector3((float)numericUpDown21.Value, (float)numericUpDown22.Value, (float)numericUpDown23.Value);
                current.PointB = new Vector3((float)numericUpDown24.Value, (float)numericUpDown25.Value, (float)numericUpDown26.Value);
                current.PointC = new Vector3((float)numericUpDown27.Value, (float)numericUpDown28.Value, (float)numericUpDown29.Value);
                current.Integer30 = (int)numericUpDown30.Value;
                current.Integer31 = (int)numericUpDown31.Value;
                current.FloatX32 = (float)numericUpDown32.Value;
                current.FloatY33 = (float)numericUpDown33.Value;
                current.FloatX34 = (float)numericUpDown34.Value;
                current.FloatY35 = (float)numericUpDown35.Value;
                current.Integer36 = (int)numericUpDown36.Value;
                current.Integer37 = (int)numericUpDown37.Value;
                current.Integer38 = (int)numericUpDown38.Value;
                current.Integer39 = (int)numericUpDown39.Value;
                current.CreateTransformMatrix();

                ListBoxCameras.Items[CurrentlySelectedCamera] = current;*/
            }
        }
    }
}
