using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using static HeroesPowerPlant.MemoryFunctions;
using static HeroesPowerPlant.CameraEditor.CameraEditorFunctions;
using SharpDX;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace HeroesPowerPlant.CameraEditor
{
    public partial class CameraEditor : Form
    {
        public CameraEditor()
        {
            InitializeComponent();
            numericUpDownCamType.Maximum = Decimal.MaxValue;
            numericUpDownCamSpeed.Maximum = Decimal.MaxValue;
            numericUpDown3.Maximum = Decimal.MaxValue;
            numericUpDownActType.Maximum = Decimal.MaxValue;
            numericUpDownTrigShape.Maximum = Decimal.MaxValue;
            numericUpDownColPosX.Maximum = Decimal.MaxValue;
            numericUpDownColPosY.Maximum = Decimal.MaxValue;
            numericUpDownColPosZ.Maximum = Decimal.MaxValue;
            numericUpDownColRotX.Maximum = Decimal.MaxValue;
            numericUpDownColRotY.Maximum = Decimal.MaxValue;
            numericUpDownColRotZ.Maximum = Decimal.MaxValue;
            numericUpDownColSclX.Maximum = Decimal.MaxValue;
            numericUpDownColSclY.Maximum = Decimal.MaxValue;
            numericUpDownColSclZ.Maximum = Decimal.MaxValue;
            numericUpDownCamPosX.Maximum = Decimal.MaxValue;
            numericUpDownCamPosY.Maximum = Decimal.MaxValue;
            numericUpDownCamPosZ.Maximum = Decimal.MaxValue;
            numericUpDownCamRotX.Maximum = Decimal.MaxValue;
            numericUpDownCamRotY.Maximum = Decimal.MaxValue;
            numericUpDownCamRotZ.Maximum = Decimal.MaxValue;
            numericUpDown21.Maximum = Decimal.MaxValue;
            numericUpDown22.Maximum = Decimal.MaxValue;
            numericUpDown23.Maximum = Decimal.MaxValue;
            numericUpDown24.Maximum = Decimal.MaxValue;
            numericUpDown25.Maximum = Decimal.MaxValue;
            numericUpDown26.Maximum = Decimal.MaxValue;
            numericUpDown27.Maximum = Decimal.MaxValue;
            numericUpDown28.Maximum = Decimal.MaxValue;
            numericUpDown29.Maximum = Decimal.MaxValue;
            numericUpDown30.Maximum = Decimal.MaxValue;
            numericUpDown31.Maximum = Decimal.MaxValue;
            numericUpDown32.Maximum = Decimal.MaxValue;
            numericUpDown33.Maximum = Decimal.MaxValue;
            numericUpDown34.Maximum = Decimal.MaxValue;
            numericUpDown35.Maximum = Decimal.MaxValue;
            numericUpDown36.Maximum = Decimal.MaxValue;
            numericUpDown37.Maximum = Decimal.MaxValue;
            numericUpDown38.Maximum = Decimal.MaxValue;
            numericUpDown39.Maximum = Decimal.MaxValue;

            numericUpDownCamType.Minimum = Decimal.MinValue;
            numericUpDownCamSpeed.Minimum = Decimal.MinValue;
            numericUpDown3.Minimum = Decimal.MinValue;
            numericUpDownActType.Minimum = Decimal.MinValue;
            numericUpDownTrigShape.Minimum = Decimal.MinValue;
            numericUpDownColPosX.Minimum = Decimal.MinValue;
            numericUpDownColPosY.Minimum = Decimal.MinValue;
            numericUpDownColPosZ.Minimum = Decimal.MinValue;
            numericUpDownColRotX.Minimum = Decimal.MinValue;
            numericUpDownColRotY.Minimum = Decimal.MinValue;
            numericUpDownColRotZ.Minimum = Decimal.MinValue;
            numericUpDownColSclX.Minimum = Decimal.MinValue;
            numericUpDownColSclY.Minimum = Decimal.MinValue;
            numericUpDownColSclZ.Minimum = Decimal.MinValue;
            numericUpDownCamPosX.Minimum = Decimal.MinValue;
            numericUpDownCamPosY.Minimum = Decimal.MinValue;
            numericUpDownCamPosZ.Minimum = Decimal.MinValue;
            numericUpDownCamRotX.Minimum = Decimal.MinValue;
            numericUpDownCamRotY.Minimum = Decimal.MinValue;
            numericUpDownCamRotZ.Minimum = Decimal.MinValue;
            numericUpDown21.Minimum = Decimal.MinValue;
            numericUpDown22.Minimum = Decimal.MinValue;
            numericUpDown23.Minimum = Decimal.MinValue;
            numericUpDown24.Minimum = Decimal.MinValue;
            numericUpDown25.Minimum = Decimal.MinValue;
            numericUpDown26.Minimum = Decimal.MinValue;
            numericUpDown27.Minimum = Decimal.MinValue;
            numericUpDown28.Minimum = Decimal.MinValue;
            numericUpDown29.Minimum = Decimal.MinValue;
            numericUpDown30.Minimum = Decimal.MinValue;
            numericUpDown31.Minimum = Decimal.MinValue;
            numericUpDown32.Minimum = Decimal.MinValue;
            numericUpDown33.Minimum = Decimal.MinValue;
            numericUpDown34.Minimum = Decimal.MinValue;
            numericUpDown35.Minimum = Decimal.MinValue;
            numericUpDown36.Minimum = Decimal.MinValue;
            numericUpDown37.Minimum = Decimal.MinValue;
            numericUpDown38.Minimum = Decimal.MinValue;
            numericUpDown39.Minimum = Decimal.MinValue;
        }
        
        private void CameraEditor_Load(object sender, EventArgs e)
        {
            TopMost = true;
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.WindowsShutDown) return;
            if (e.CloseReason == CloseReason.FormOwnerClosing) return;

            e.Cancel = true;
            Hide();
        }

        public string CurrentCameraFile;
        bool ProgramIsChangingStuff;
        int CurrentlySelectedCamera = -1;
                
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            New();
        }

        public void New()
        {
            CurrentCameraFile = null;
            ListBoxCameras.Items.Clear();
            toolStripStatusFile.Text = "No file loaded";
            UpdateLabelCameraCount();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog OpenCamera = new OpenFileDialog()
            {
                Filter = "BIN Files|*.bin"
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
                ListBoxCameras.Items.AddRange(ImportCameraFile(CurrentCameraFile).ToArray());

                toolStripStatusFile.Text = "Loaded " + CurrentCameraFile;
                UpdateLabelCameraCount();

                ListBoxCameras.SelectedIndex = -1;
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CurrentCameraFile != null)
                saveCameraFile(CurrentCameraFile, ListBoxCameras.Items.Cast<CameraHeroes>());
            else
                saveAsToolStripMenuItem_Click(sender, e);
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog SaveCamera = new SaveFileDialog()
            {
                Filter = "Binary Files|*.bin",
                FileName = CurrentCameraFile
            };
            if (SaveCamera.ShowDialog() == DialogResult.OK)
            {
                CurrentCameraFile = SaveCamera.FileName;
                saveCameraFile(CurrentCameraFile, ListBoxCameras.Items.Cast<CameraHeroes>());
            }
        }

        private bool hasRemoved = false;

        private void ListBoxCameras_SelectedIndexChanged(object sender, EventArgs e)
        {
            ProgramIsChangingStuff = true;

            if (!hasRemoved & CurrentlySelectedCamera != -1)
                (ListBoxCameras.Items[CurrentlySelectedCamera] as CameraHeroes).isSelected = false;
            else if (hasRemoved) hasRemoved = false;

            CurrentlySelectedCamera = ListBoxCameras.SelectedIndex;
            
            if (CurrentlySelectedCamera != -1)
            {
                try
                {
                    CameraHeroes current = ListBoxCameras.Items[CurrentlySelectedCamera] as CameraHeroes;

                    current.isSelected = true;

                    numericUpDownCamType.Value = current.CameraType;
                    numericUpDownCamSpeed.Value = current.CameraSpeed;
                    numericUpDown3.Value = current.Integer3;
                    numericUpDownActType.Value = current.ActivationType;
                    numericUpDownTrigShape.Value = current.TriggerShape;
                    numericUpDownColPosX.Value = (decimal)current.TriggerPosition.X;
                    numericUpDownColPosY.Value = (decimal)current.TriggerPosition.Y;
                    numericUpDownColPosZ.Value = (decimal)current.TriggerPosition.Z;
                    numericUpDownColRotX.Value = (decimal)(current.TriggerRotX * (360f / 65536f));
                    numericUpDownColRotY.Value = (decimal)(current.TriggerRotY * (360f / 65536f));
                    numericUpDownColRotZ.Value = (decimal)(current.TriggerRotZ * (360f / 65536f));
                    numericUpDownColSclX.Value = (decimal)current.TriggerScale.X;
                    numericUpDownColSclY.Value = (decimal)current.TriggerScale.Y;
                    numericUpDownColSclZ.Value = (decimal)current.TriggerScale.Z;
                    numericUpDownCamPosX.Value = (decimal)current.CamPos.X;
                    numericUpDownCamPosY.Value = (decimal)current.CamPos.Y;
                    numericUpDownCamPosZ.Value = (decimal)current.CamPos.Z;
                    numericUpDownCamRotX.Value = (decimal)(current.CamRotX * (360f / 65536f));
                    numericUpDownCamRotY.Value = (decimal)(current.CamRotY * (360f / 65536f));
                    numericUpDownCamRotZ.Value = (decimal)(current.CamRotZ * (360f / 65536f));
                    numericUpDown21.Value = (decimal)current.PointA.X;
                    numericUpDown22.Value = (decimal)current.PointA.Y;
                    numericUpDown23.Value = (decimal)current.PointA.Z;
                    numericUpDown24.Value = (decimal)current.PointB.X;
                    numericUpDown25.Value = (decimal)current.PointB.Y;
                    numericUpDown26.Value = (decimal)current.PointB.Z;
                    numericUpDown27.Value = (decimal)current.PointC.X;
                    numericUpDown28.Value = (decimal)current.PointC.Y;
                    numericUpDown29.Value = (decimal)current.PointC.Z;
                    numericUpDown30.Value = current.Integer30;
                    numericUpDown31.Value = current.Integer31;
                    numericUpDown32.Value = (decimal)current.FloatX32;
                    numericUpDown33.Value = (decimal)current.FloatY33;
                    numericUpDown34.Value = (decimal)current.FloatX34;
                    numericUpDown35.Value = (decimal)current.FloatY35;
                    numericUpDown36.Value = current.Integer36;
                    numericUpDown37.Value = current.Integer37;
                    numericUpDown38.Value = current.Integer38;
                    numericUpDown39.Value = current.Integer39;
                }
                catch
                {
                    MessageBox.Show("Could not load this camera properly: one or more properties are unsupported.");
                }
            }

            UpdateLabelCameraCount();
            ProgramIsChangingStuff = false;
        }

        private void numericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (!ProgramIsChangingStuff & CurrentlySelectedCamera != -1)
            {
                CameraHeroes current = ListBoxCameras.Items[CurrentlySelectedCamera] as CameraHeroes;

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

                ListBoxCameras.Items[CurrentlySelectedCamera] = current;
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            CameraHeroes newCamera = new CameraHeroes() { TriggerPosition = Program.MainForm.renderer.Camera.GetPosition() };
            ListBoxCameras.Items.Add(newCamera);
            ListBoxCameras.SelectedIndex = ListBoxCameras.Items.Count - 1;
            UpdateLabelCameraCount();
        }

        private void buttonCopy_Click(object sender, EventArgs e)
        {
            CameraHeroes newCamera = new CameraHeroes((CameraHeroes)ListBoxCameras.Items[CurrentlySelectedCamera]);
            newCamera.CreateTransformMatrix();
            ListBoxCameras.Items.Add(newCamera);
            ListBoxCameras.SelectedIndex = ListBoxCameras.Items.Count - 1;
            UpdateLabelCameraCount();
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
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            hasRemoved = true;
            ListBoxCameras.Items.Clear();
            UpdateLabelCameraCount();
        }

        private void UpdateLabelCameraCount()
        {
            if (CurrentlySelectedCamera == -1)
                LabelCameraCount.Text = ListBoxCameras.Items.Count.ToString() + " cameras";
            else if (ListBoxCameras.Items.Count > 0)
                LabelCameraCount.Text = "Camera " + (CurrentlySelectedCamera + 1).ToString() + "/" + ListBoxCameras.Items.Count.ToString();
        }

        private void buttonTeleportToTrigger_Click(object sender, EventArgs e)
        {
            if (!Teleport((float)numericUpDownColPosX.Value, (float)numericUpDownColPosY.Value, (float)numericUpDownColPosZ.Value))
                MessageBox.Show("Error writing data", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        
        private void buttonCopyLeaderPos_Click(object sender, EventArgs e)
        {
            if (TryAttach())
                Clipboard.SetText(JsonConvert.SerializeObject(GetPlayer0Position()));
            else MessageBox.Show("Error reading data", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void buttonGetCamera_Click(object sender, EventArgs e)
        {
            if (TryAttach())
                Clipboard.SetText(JsonConvert.SerializeObject(GetCameraPosition()));
            else MessageBox.Show("Error reading data", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        
        private void buttonComeHere_Click(object sender, EventArgs e)
        {
            Program.MainForm.renderer.Camera.ViewMatrix.Position = new Vector3((float)numericUpDownCamPosX.Value, (float)numericUpDownCamPosY.Value, (float)numericUpDownCamPosZ.Value);
        }

        private void buttonComeTrigger_Click(object sender, EventArgs e)
        {
            Program.MainForm.renderer.Camera.ViewMatrix.Position = new Vector3((float)numericUpDownColPosX.Value, (float)numericUpDownColPosY.Value, (float)numericUpDownColPosZ.Value);
        }

        private void buttonCopyViewPos_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(JsonConvert.SerializeObject(Program.MainForm.renderer.Camera.ViewMatrix.Position));
        }

        public void RenderCameras(SharpRenderer renderer)
        {
            Vector4 oldColor = renderer.normalColor;
            renderer.normalColor = new Vector4(0.6f, 0.25f, 0.7f, 0.8f);

            foreach (CameraHeroes c in ListBoxCameras.Items)
                if (renderer.frustum.Intersects(ref c.boundingBox))
                    c.Draw(renderer);

            renderer.normalColor = oldColor;
        }

        public void ScreenClicked(Ray r)
        {
            int index = ListBoxCameras.SelectedIndex;

            float smallerDistance = 10000f;
            for (int i = 0; i < ListBoxCameras.Items.Count; i++)
            {
                if (((CameraHeroes)ListBoxCameras.Items[i]).isSelected) continue;

                float? distance = ((CameraHeroes)ListBoxCameras.Items[i]).IntersectsWith(r);
                if (distance != null)
                    if (distance < smallerDistance)
                        index = i;
            }

            ListBoxCameras.SelectedIndex = index;
        }

        private void sortByDistanceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IEnumerable<CameraHeroes> cameras = ListBoxCameras.Items.Cast<CameraHeroes>();
            cameras = cameras.OrderBy(c => c.GetDistance()).ToList();
            ListBoxCameras.Items.Clear();
            ListBoxCameras.Items.AddRange(cameras.ToArray());
        }

        private const string pasteErrorMessage = "Error pasting coordinates from clipboard. Are you sure you have a Vector3 copied?";

        private void buttonPasteTriggerPos_Click(object sender, EventArgs e)
        {
            try
            {
                Vector3 position = JsonConvert.DeserializeObject<Vector3>(Clipboard.GetText());
                numericUpDownColPosX.Value = (decimal)position.X;
                numericUpDownColPosY.Value = (decimal)position.Y;
                numericUpDownColPosZ.Value = (decimal)position.Z;
            }
            catch
            {
                MessageBox.Show(pasteErrorMessage);
            }
        }

        private void buttonPasteCamPos_Click(object sender, EventArgs e)
        {
            try
            {
                Vector3 position = JsonConvert.DeserializeObject<Vector3>(Clipboard.GetText());
                numericUpDownCamPosX.Value = (decimal)position.X;
                numericUpDownCamPosY.Value = (decimal)position.Y;
                numericUpDownCamPosZ.Value = (decimal)position.Z;
            }
            catch
            {
                MessageBox.Show(pasteErrorMessage);
            }
        }

        private void buttonPastePointA_Click(object sender, EventArgs e)
        {
            try
            {
                Vector3 position = JsonConvert.DeserializeObject<Vector3>(Clipboard.GetText());
                numericUpDown21.Value = (decimal)position.X;
                numericUpDown22.Value = (decimal)position.Y;
                numericUpDown23.Value = (decimal)position.Z;
            }
            catch
            {
                MessageBox.Show(pasteErrorMessage);
            }
        }

        private void buttonPastePointB_Click(object sender, EventArgs e)
        {
            try
            {
                Vector3 position = JsonConvert.DeserializeObject<Vector3>(Clipboard.GetText());
                numericUpDown24.Value = (decimal)position.X;
                numericUpDown25.Value = (decimal)position.Y;
                numericUpDown26.Value = (decimal)position.Z;
            }
            catch
            {
                MessageBox.Show(pasteErrorMessage);
            }
        }

        private void buttonPastePointc_Click(object sender, EventArgs e)
        {
            try
            {
                Vector3 position = JsonConvert.DeserializeObject<Vector3>(Clipboard.GetText());
                numericUpDown27.Value = (decimal)position.X;
                numericUpDown28.Value = (decimal)position.Y;
                numericUpDown29.Value = (decimal)position.Z;
            }
            catch
            {
                MessageBox.Show(pasteErrorMessage);
            }
        }
    }
}
