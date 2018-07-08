using SharpDX;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using static HeroesPowerPlant.LayoutEditor.LayoutEditorFunctions;
using static HeroesPowerPlant.ReadWriteCommon;

namespace HeroesPowerPlant.LayoutEditor
{
    public partial class LayoutEditor : Form
    {
        public LayoutEditor()
        {
            InitializeComponent();

            try
            {
                HeroesObjectEntries = ReadObjectListData("Resources\\Lists\\HeroesObjectList.ini");
                ShadowObjectEntries = ReadObjectListData("Resources\\Lists\\ShadowObjectList.ini");
            }
            catch (FileNotFoundException e)
            {
                MessageBox.Show("Error: failed to load one or more files during startup. Program will not work correctly.");
                throw e;
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
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.WindowsShutDown) return;
            if (e.CloseReason == CloseReason.FormOwnerClosing) return;

            e.Cancel = true;
            Hide();
        }

        public void RenderAllSetObjects(bool drawEveryObject)
        {
            foreach (SetObject s in listBoxObjects.Items)
                if (SharpRenderer.frustum.Intersects(ref s.boundingBox))
                    s.Draw(drawEveryObject);
        }

        private bool isShadow;
        private bool ProgramIsChangingStuff = false;
        private int selectedObject = -1;
        private string currentlyOpenFileName;

        public ObjectEntry[] HeroesObjectEntries;
        public ObjectEntry[] ShadowObjectEntries;

        private void heroesLayoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            isShadow = false;
            ComboBoxObject.Items.Clear();
            ComboBoxObject.Items.AddRange(HeroesObjectEntries);
            listBoxObjects.Items.Clear();
            currentlyOpenFileName = null;
            UpdateObjectAmountLabel();
            UpdateFileLabel();
        }

        public void ResetMatrices()
        {
            foreach (SetObject s in listBoxObjects.Items)
            {
                s.CreateTransformMatrix();
            }
        }

        private void shadowLayoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            isShadow = true;
            ComboBoxObject.Items.Clear();
            ComboBoxObject.Items.AddRange(ShadowObjectEntries);
            listBoxObjects.Items.Clear();
            currentlyOpenFileName = null;
            UpdateObjectAmountLabel();
            UpdateFileLabel();
        }

        public void UpdateObjectAmountLabel()
        {
            if (listBoxObjects.Items.Count == 0)
                objectAmountLabel.Text = "0 objects";
            else
            {
                if (selectedObject == -1)
                    objectAmountLabel.Text = listBoxObjects.Items.Count.ToString() + " objects";
                else
                    objectAmountLabel.Text = String.Format("{0}/{1} object", selectedObject + 1, listBoxObjects.Items.Count);
            }
        }

        public void UpdateFileLabel()
        {
            if (currentlyOpenFileName == null)
                openFileLabel.Text = "No file loaded";
            else
                openFileLabel.Text = currentlyOpenFileName;
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog
            {
                Filter = "All supported types|*.bin; *.dat|BIN Files|*.bin|DAT Files|*.dat"
            };
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                OpenLayoutFile(openFile.FileName);
            }
        }

        public void OpenLayoutFile(string fileName)
        {
            currentlyOpenFileName = fileName;
            listBoxObjects.Items.Clear();
            ComboBoxObject.Items.Clear();

            if (Path.GetExtension(currentlyOpenFileName).ToLower() == ".bin")
            {
                isShadow = false;
                ComboBoxObject.Items.AddRange(HeroesObjectEntries);
                listBoxObjects.Items.AddRange(GetHeroesLayout(currentlyOpenFileName, HeroesObjectEntries).ToArray());
            }
            else if (Path.GetExtension(currentlyOpenFileName).ToLower() == ".dat")
            {
                isShadow = true;
                ComboBoxObject.Items.AddRange(ShadowObjectEntries);
                listBoxObjects.Items.AddRange(GetShadowLayout(currentlyOpenFileName, ShadowObjectEntries).ToArray());
            }
            else throw new InvalidDataException("Unknown file type");

            UpdateObjectAmountLabel();
            UpdateFileLabel();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (currentlyOpenFileName != null)
                Save();
            else
                SaveAs();
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveAs();
        }

        private void Save()
        {
            if (isShadow)
            {
                SaveShadowLayout(listBoxObjects.Items.Cast<SetObjectShadow>(), currentlyOpenFileName);
            }
            else
            {
                SaveHeroesLayout(listBoxObjects.Items.Cast<SetObjectHeroes>(), currentlyOpenFileName);
            }
        }

        private void SaveAs()
        {
            SaveFileDialog saveFile = new SaveFileDialog
            {
                Filter = ".dat files|*.dat",
                FileName = currentlyOpenFileName
            };
            if (saveFile.ShowDialog() == DialogResult.OK)
            {
                currentlyOpenFileName = saveFile.FileName;
                Save();
            }
        }

        private void byIDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<SetObject> list = listBoxObjects.Items.Cast<SetObject>().OrderBy(f => f.GetTypeAsOne()).ToList();
            listBoxObjects.Items.Clear();
            foreach (SetObject i in list)
                listBoxObjects.Items.Add(i);
        }

        private void byDistanceFromOriginToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<SetObject> list = listBoxObjects.Items.Cast<SetObject>().OrderBy(f => f.GetDistanceFromOrigin()).ToList();
            listBoxObjects.Items.Clear();
            foreach (SetObject i in list)
                listBoxObjects.Items.Add(i);
        }

        private void exportINIToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog()
            {
                Filter = ".ini files|*.ini",
                FileName = Path.ChangeExtension(currentlyOpenFileName, ".ini")
            };
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                SaveHeroesLayoutINI(listBoxObjects.Items.Cast<SetObjectHeroes>(), saveFileDialog.FileName);
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
                listBoxObjects.Items.AddRange(GetHeroesLayoutFromINI(openFile.FileName, HeroesObjectEntries).ToArray());
            }
        }

        private void importLayoutFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog
            {
                Filter = isShadow? ".dat files|*.dat" : ".bin files|*.bin"
            };
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                if (Path.GetExtension(openFile.FileName).ToLower() == ".bin")
                {
                    listBoxObjects.Items.AddRange(GetHeroesLayout(openFile.FileName, HeroesObjectEntries).ToArray());
                }
                else if (Path.GetExtension(openFile.FileName).ToLower() == ".dat")
                {
                    listBoxObjects.Items.AddRange(GetShadowLayout(openFile.FileName, ShadowObjectEntries).ToArray());
                }
                else throw new InvalidDataException("Unknown file type");
            }
            
            UpdateObjectAmountLabel();
        }

        private void importOBJToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog
            {
                Filter = ".obj files|*.obj"
            };
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                SetObjectHeroes ring = new SetObjectHeroes(0, 3, HeroesObjectEntries, Vector3.Zero, Vector3.Zero, 0, 0);
                listBoxObjects.Items.AddRange(GetObjectsFromObjFile(openFile.FileName, ring.objectEntry).ToArray());
            }
        }

        private void buttonViewHere_Click(object sender, EventArgs e)
        {
            if (selectedObject != -1)
                SharpRenderer.Camera.SetPosition((listBoxObjects.Items[selectedObject] as SetObject).Position - 200 * SharpRenderer.Camera.GetForward());
        }

        private void listBoxObjects_SelectedIndexChanged(object sender, EventArgs e)
        {
            ProgramIsChangingStuff = true;

            foreach (SetObject s in listBoxObjects.Items)
                s.IsSelected = false;

            selectedObject = listBoxObjects.SelectedIndex;

            if (selectedObject < 0) return;

            SetObject currentSet;
            if (isShadow)
            {
                currentSet = listBoxObjects.Items[selectedObject] as SetObjectShadow;

                NumericRotX.Value = (decimal)((SetObjectShadow)currentSet).Rotation.X;
                NumericRotY.Value = (decimal)((SetObjectShadow)currentSet).Rotation.Y;
                NumericRotZ.Value = (decimal)((SetObjectShadow)currentSet).Rotation.Z;

                PropertyGridMisc.SelectedObject = (currentSet as SetObjectShadow).objectManager;
            }
            else
            {
                currentSet = listBoxObjects.Items[selectedObject] as SetObjectHeroes;
                
                NumericRotX.Value = (decimal)BAMStoDegrees(((SetObjectHeroes)currentSet).Rotation.X);
                NumericRotY.Value = (decimal)BAMStoDegrees(((SetObjectHeroes)currentSet).Rotation.Y);
                NumericRotZ.Value = (decimal)BAMStoDegrees(((SetObjectHeroes)currentSet).Rotation.Z);

                PropertyGridMisc.SelectedObject = (currentSet as SetObjectHeroes).objectManager;
            }

            currentSet.IsSelected = true;

            ComboBoxObject.SelectedItem = currentSet.objectEntry;

            NumericPosX.Value = (decimal)(currentSet.Position.X);
            NumericPosY.Value = (decimal)(currentSet.Position.Y);
            NumericPosZ.Value = (decimal)(currentSet.Position.Z);
            NumericObjLink.Value = currentSet.Link;
            NumericObjRend.Value = currentSet.Rend;

            DescriptionPicker(currentSet.objectEntry);

            UpdateObjectAmountLabel();

            ProgramIsChangingStuff = false;
        }

        private void DescriptionPicker(ObjectEntry objectEntry)
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

        private void ButtonAdd_Click(object sender, EventArgs e)
        {
            if (isShadow)
            {
                SetObjectShadow newObject = new SetObjectShadow(0, 0, ShadowObjectEntries, SharpRenderer.Camera.GetPosition() + 100 * SharpRenderer.Camera.GetForward(), Vector3.Zero, 0, 10);
                newObject.CreateTransformMatrix();

                listBoxObjects.Items.Add(newObject);
                listBoxObjects.SelectedIndex = listBoxObjects.Items.Count - 1;
            }
            else
            {
                SetObjectHeroes newObject = new SetObjectHeroes(0, 0, HeroesObjectEntries, SharpRenderer.Camera.GetPosition() + 100 * SharpRenderer.Camera.GetForward(), Vector3.Zero, 0, 10);
                newObject.CreateTransformMatrix();

                listBoxObjects.Items.Add(newObject);
                listBoxObjects.SelectedIndex = listBoxObjects.Items.Count - 1;
            }
        }

        private void buttonCopy_Click(object sender, EventArgs e)
        {
            if (selectedObject >= 0)
            {
                SetObject original = listBoxObjects.Items[selectedObject] as SetObject;
                SetObject destination;

                if (isShadow)
                {
                    destination = new SetObjectShadow(original.objectEntry, original.Position, original.Rotation, original.Link, original.Rend);

                    (destination as SetObjectShadow).objectManager.MiscSettings = new byte[(original as SetObjectShadow).objectManager.MiscSettings.Length];

                    for (int i = 0; i < (destination as SetObjectShadow).objectManager.MiscSettings.Length; i++)
                    {
                        (destination as SetObjectShadow).objectManager.MiscSettings[i] = (original as SetObjectShadow).objectManager.MiscSettings[i];
                    }
                }
                else
                {
                    destination = new SetObjectHeroes(original.objectEntry, original.Position, original.Rotation, original.Link, original.Rend);
                    
                    (destination as SetObjectHeroes).objectManager.MiscSettings = new byte[(original as SetObjectHeroes).objectManager.MiscSettings.Length];

                    for (int i = 0; i < (destination as SetObjectHeroes).objectManager.MiscSettings.Length; i++)
                    {
                        (destination as SetObjectHeroes).objectManager.MiscSettings[i] = (original as SetObjectHeroes).objectManager.MiscSettings[i];
                    }
                }

                destination.CreateTransformMatrix();

                listBoxObjects.Items.Add(destination);

                listBoxObjects.SelectedIndex = listBoxObjects.Items.Count - 1;
            }
        }

        private void ButtonRemove_Click(object sender, EventArgs e)
        {
            if (selectedObject >= 0)
            {
                int Temp = selectedObject;
                listBoxObjects.Items.RemoveAt(Temp);
                if (Temp < listBoxObjects.Items.Count)
                    listBoxObjects.SelectedIndex = Temp;
                else
                    listBoxObjects.SelectedIndex = Temp - 1;
            }
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            listBoxObjects.Items.Clear();
            UpdateObjectAmountLabel();
        }

        private void ComboBoxObject_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!ProgramIsChangingStuff & (selectedObject >= 0))
            {
                SetObject current;

                if (isShadow)
                {
                    current = listBoxObjects.Items[selectedObject] as SetObjectShadow;
                    current.objectEntry = (ComboBoxObject.SelectedItem as ObjectEntry);
                    current.FindNewObjectManager();
                    PropertyGridMisc.SelectedObject = ((SetObjectShadow)current).objectManager;
                }
                else
                {
                    current = listBoxObjects.Items[selectedObject] as SetObjectHeroes;
                    current.objectEntry = (ComboBoxObject.SelectedItem as ObjectEntry);
                    current.FindNewObjectManager();
                    PropertyGridMisc.SelectedObject = ((SetObjectHeroes)current).objectManager;
                }

                current.CreateTransformMatrix();

                listBoxObjects.Items[selectedObject] = current;

                DescriptionPicker(current.objectEntry);
            }
        }

        private void NumericPos_ValueChanged(object sender, EventArgs e)
        {
            if (!ProgramIsChangingStuff & (selectedObject >= 0))
            {
                (listBoxObjects.Items[selectedObject] as SetObject).Position = new Vector3((float)NumericPosX.Value, (float)NumericPosY.Value, (float)NumericPosZ.Value);
                (listBoxObjects.Items[selectedObject] as SetObject).CreateTransformMatrix();
            }
        }

        private void NumericRot_ValueChanged(object sender, EventArgs e)
        {
            if (!ProgramIsChangingStuff & (selectedObject >= 0))
            {
                if (isShadow)
                {
                    (listBoxObjects.Items[selectedObject] as SetObjectShadow).Rotation = new Vector3((float)NumericRotX.Value, (float)NumericRotY.Value, (float)NumericRotZ.Value);
                }
                else
                {
                    (listBoxObjects.Items[selectedObject] as SetObjectHeroes).Rotation.X = DegreesToBAMS((float)NumericRotX.Value);
                    (listBoxObjects.Items[selectedObject] as SetObjectHeroes).Rotation.Y = DegreesToBAMS((float)NumericRotY.Value);
                    (listBoxObjects.Items[selectedObject] as SetObjectHeroes).Rotation.Z = DegreesToBAMS((float)NumericRotZ.Value);
                }
                (listBoxObjects.Items[selectedObject] as SetObject).CreateTransformMatrix();
            }
        }

        private void NumericObjLink_ValueChanged(object sender, EventArgs e)
        {
            if (!ProgramIsChangingStuff & (selectedObject >= 0))
                (listBoxObjects.Items[selectedObject] as SetObject).Link = (byte)NumericObjLink.Value;
        }

        private void ButtonFindNextLink_Click(object sender, EventArgs e)
        {
            if (listBoxObjects.Items.Count < 2)
                return;

            if (selectedObject != listBoxObjects.Items.Count - 1)
                for (int i = selectedObject + 1; i < listBoxObjects.Items.Count; i++)
                    if ((listBoxObjects.Items[i] as SetObject).Link == (listBoxObjects.Items[selectedObject] as SetObject).Link)
                    {
                        listBoxObjects.SelectedIndex = i;
                        return;
                    }
            if (selectedObject > 0)
                for (int i = 0; i < selectedObject; i++)
                    if ((listBoxObjects.Items[i] as SetObject).Link == (listBoxObjects.Items[selectedObject] as SetObject).Link)
                    {
                        listBoxObjects.SelectedIndex = i;
                        return;
                    }

            MessageBox.Show("No other object has this same Link ID!");
        }

        public void ScreenClicked(Ray r)
        {
            float smallerDistance = 500000f;
            int index = listBoxObjects.SelectedIndex;
            for (int i = 0; i < listBoxObjects.Items.Count; i++)
            {
                if (((SetObject)listBoxObjects.Items[i]).IsSelected) continue;

                float? distance = ((SetObject)listBoxObjects.Items[i]).IntersectsWith(r);
                if (distance != null)
                    if (distance < smallerDistance)
                        index = i;
            }
            listBoxObjects.SelectedIndex = index;
        }

        private void NumericObjRend_ValueChanged(object sender, EventArgs e)
        {
            if (!ProgramIsChangingStuff & (selectedObject >= 0))
                (listBoxObjects.Items[selectedObject] as SetObject).Rend = (byte)NumericObjRend.Value;
        }

        private void ButtonGetSpeed_Click(object sender, EventArgs e)
        {
            MemoryFunctions.DeterminePointers();
            if (Program.MemManager.ProcessIsAttached)
            {
                NumericPosX.Value = (decimal)Program.MemManager.ReadFloat(MemoryFunctions.Pointer0X);
                NumericPosY.Value = (decimal)Program.MemManager.ReadFloat(MemoryFunctions.Pointer0Y);
                NumericPosZ.Value = (decimal)Program.MemManager.ReadFloat(MemoryFunctions.Pointer0Z);
            }
            else MessageBox.Show("Error collecting data", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void ButtonGetFly_Click(object sender, EventArgs e)
        {
            MemoryFunctions.DeterminePointers();
            if (Program.MemManager.ProcessIsAttached)
            {
                NumericPosX.Value = (decimal)Program.MemManager.ReadFloat(MemoryFunctions.Pointer1X);
                NumericPosY.Value = (decimal)Program.MemManager.ReadFloat(MemoryFunctions.Pointer1Y);
                NumericPosZ.Value = (decimal)Program.MemManager.ReadFloat(MemoryFunctions.Pointer1Z);
            }
            else MessageBox.Show("Error collecting data", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void ButtonGetPow_Click(object sender, EventArgs e)
        {
            MemoryFunctions.DeterminePointers();
            if (Program.MemManager.ProcessIsAttached)
            {
                NumericPosX.Value = (decimal)Program.MemManager.ReadFloat(MemoryFunctions.Pointer2X);
                NumericPosY.Value = (decimal)Program.MemManager.ReadFloat(MemoryFunctions.Pointer2Y);
                NumericPosZ.Value = (decimal)Program.MemManager.ReadFloat(MemoryFunctions.Pointer2Z);
            }
            else MessageBox.Show("Error collecting data", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void ButtonSpeedRot_Click(object sender, EventArgs e)
        {
            MemoryFunctions.DeterminePointers();
            if (Program.MemManager.ProcessIsAttached)
            {
                NumericRotX.Value = (decimal)BAMStoDegrees(Program.MemManager.ReadUInt32(MemoryFunctions.Pointer0RX));
                NumericRotY.Value = (decimal)BAMStoDegrees(Program.MemManager.ReadUInt32(MemoryFunctions.Pointer0RY));
                NumericRotZ.Value = (decimal)BAMStoDegrees(Program.MemManager.ReadUInt32(MemoryFunctions.Pointer0RZ));
            }
            else MessageBox.Show("Error collecting data", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void ButtonFlyRot_Click(object sender, EventArgs e)
        {
            MemoryFunctions.DeterminePointers();
            if (Program.MemManager.ProcessIsAttached)
            {
                NumericRotX.Value = (decimal)BAMStoDegrees(Program.MemManager.ReadUInt32(MemoryFunctions.Pointer1RX));
                NumericRotY.Value = (decimal)BAMStoDegrees(Program.MemManager.ReadUInt32(MemoryFunctions.Pointer1RY));
                NumericRotZ.Value = (decimal)BAMStoDegrees(Program.MemManager.ReadUInt32(MemoryFunctions.Pointer1RZ));
            }
            else MessageBox.Show("Error collecting data", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void ButtonPowRot_Click(object sender, EventArgs e)
        {
            MemoryFunctions.DeterminePointers();
            if (Program.MemManager.ProcessIsAttached)
            {
                NumericRotX.Value = (decimal)BAMStoDegrees(Program.MemManager.ReadUInt32(MemoryFunctions.Pointer2RX));
                NumericRotY.Value = (decimal)BAMStoDegrees(Program.MemManager.ReadUInt32(MemoryFunctions.Pointer2RY));
                NumericRotZ.Value = (decimal)BAMStoDegrees(Program.MemManager.ReadUInt32(MemoryFunctions.Pointer2RZ));
            }
            else MessageBox.Show("Error collecting data", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void ButtonTeleport_Click(object sender, EventArgs e)
        {
            MemoryFunctions.DeterminePointers();
            if (Program.MemManager.ProcessIsAttached)
            {
               Program.MemManager.Write4bytes(MemoryFunctions.Pointer0X, BitConverter.GetBytes((float)NumericPosX.Value));
               Program.MemManager.Write4bytes(MemoryFunctions.Pointer0Y, BitConverter.GetBytes((float)NumericPosY.Value));
               Program.MemManager.Write4bytes(MemoryFunctions.Pointer0Z, BitConverter.GetBytes((float)NumericPosZ.Value));
               Program.MemManager.Write4bytes(MemoryFunctions.Pointer1X, BitConverter.GetBytes((float)NumericPosX.Value));
               Program.MemManager.Write4bytes(MemoryFunctions.Pointer1Y, BitConverter.GetBytes((float)NumericPosY.Value));
               Program.MemManager.Write4bytes(MemoryFunctions.Pointer1Z, BitConverter.GetBytes((float)NumericPosZ.Value));
               Program.MemManager.Write4bytes(MemoryFunctions.Pointer2X, BitConverter.GetBytes((float)NumericPosX.Value));
               Program.MemManager.Write4bytes(MemoryFunctions.Pointer2Y, BitConverter.GetBytes((float)NumericPosY.Value));
               Program.MemManager.Write4bytes(MemoryFunctions.Pointer2Z, BitConverter.GetBytes((float)NumericPosZ.Value));
            }
            else MessageBox.Show("Error writing data", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
