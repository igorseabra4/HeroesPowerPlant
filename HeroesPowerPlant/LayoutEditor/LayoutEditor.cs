using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using SharpDX;
using ShadowFNT.Structures;
using HeroesPowerPlant.Shared.IO.Config;
using Shadow.Structures;

namespace HeroesPowerPlant.LayoutEditor
{
    public partial class LayoutEditor : Form
    {
        private AFSLib.AfsArchive loadedAFS;
        private FNT loadedFNT;
        private ShadowSoundBIN loadedShadowSoundBIN;

        private int darkAudioId = -1;
        private int normalAudioId = -1;
        private int heroAudioId = -1;
        public LayoutEditor()
        {
            InitializeComponent();

            layoutSystem = new LayoutEditorSystem
            {
                autoUnkBytes = checkBoxAutoBytes.Checked
            };

            layoutSystem.BindControl(listBoxObjects);

#if !DEBUG
            importSALayoutFileToolStripMenuItem.Visible = false;
#endif
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
            listBoxObjects.BeginUpdate();
            layoutSystem.NewHeroesLayout();
            listBoxObjects.EndUpdate();

            UpdateObjectComboBox();
            UpdateFileLabel(Program.MainForm);
        }

        private void shadowLayoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listBoxObjects.BeginUpdate();
            layoutSystem.NewShadowLayout();
            listBoxObjects.EndUpdate();

            UpdateObjectComboBox();
            UpdateFileLabel(Program.MainForm);
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog
            {
                Filter = "All supported types|*.bin; *.dat|BIN Files|*.bin|DAT Files|*.dat"
            };

            listBoxObjects.BeginUpdate();
            if (openFile.ShowDialog() == DialogResult.OK)
                OpenFile(openFile.FileName, Program.MainForm);
            listBoxObjects.EndUpdate();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(layoutSystem.CurrentlyOpenFileName))
                SaveAs();
            else
                layoutSystem.Save();
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveAs();
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.MainForm.CloseLayoutEditor(this);
        }

        private void byIDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listBoxObjects.BeginUpdate();
            layoutSystem.SortObjectsByID();
            listBoxObjects.EndUpdate();
        }

        private void byDistanceFromOriginToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listBoxObjects.BeginUpdate();
            layoutSystem.SortObjectsByDistance();
            listBoxObjects.EndUpdate();
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
                listBoxObjects.BeginUpdate();
                layoutSystem.ImportINI(openFile.FileName);
                listBoxObjects.EndUpdate();
            }
        }

        private void importLayoutFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog
            {
                Multiselect = true,
#if DEBUG
                Filter = ".bin files|*.bin|.dat files|*.dat"
#else
                Filter = layoutSystem.IsShadow ? ".dat files|*.dat" : ".bin files|*.bin"
#endif
            };
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                listBoxObjects.BeginUpdate();
                foreach (var s in openFile.FileNames)
                    layoutSystem.ImportLayoutFile(s);
                listBoxObjects.EndUpdate();
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
                listBoxObjects.BeginUpdate();
                layoutSystem.ImportOBJ(openFile.FileName);
                listBoxObjects.EndUpdate();
            }
        }

        private void buttonViewHere_Click(object sender, EventArgs e)
        {
            if (listBoxObjects.SelectedIndices.Count == 1)
                layoutSystem.ViewHere(listBoxObjects.SelectedIndex);
        }

        private void listBoxObjects_SelectedIndexChanged(object sender, EventArgs e)
        {
            ProgramIsChangingStuff = true;

            layoutSystem.SelectedIndexChanged(listBoxObjects.SelectedIndices);
            Program.MainForm.UnselectEveryoneExceptMe(this);

            if (listBoxObjects.SelectedIndices.Count == 1)
            {
                UpdateDisplayData();
                Focus();
            }
            else
            {
                DisableDisplayData();
                UpdateObjectAmountLabel();
            }

            UpdateGizmoPosition();

            ProgramIsChangingStuff = false;

            if (loadedFNT.fileName != null)
            {
                LoadShadowSubtitlePreviews();
            }

            if (loadedShadowSoundBIN.fileName != null)
            {
                LoadShadowSFXBinStrings();
            }
        }

        private void LoadShadowSFXBinStrings()
        {
            if (listBoxObjects.SelectedItems.Count != 1)
                return;

            if (listBoxObjects.SelectedItem.GetType() == typeof(Object2598_SetSeOneShot)
                || (listBoxObjects.SelectedItem.GetType() == typeof(Object2597_SetSeLoop)))
            {
                if (listBoxObjects.SelectedItem.GetType() == typeof(Object2598_SetSeOneShot))
                {
                    var setObject = (Object2598_SetSeOneShot)listBoxObjects.SelectedItem;
                    var audioID = setObject.AudioID;
                    // TODO: Probably cache results instead of searching every click
                    var sfxStringIdList = "";
                    for (int i = 0; i < loadedShadowSoundBIN.sfxTable.Count; i++)
                    {
                        var entrySfxId = loadedShadowSoundBIN.sfxTable[i].sfxId;
                        if (entrySfxId == audioID)
                        {
                            setObject.sfxEntry = loadedShadowSoundBIN.sfxTable[i];
                        }
                        sfxStringIdList += loadedShadowSoundBIN.sfxTable[i].sfxId + " " + loadedShadowSoundBIN.sfxTable[i].sfxString.Replace('\0', ' ') + "\r\n";
                    }
                    textBox_FNT_TriggerTalkingPreview.Text = sfxStringIdList;
                }
                else if (listBoxObjects.SelectedItem.GetType() == typeof(Object2597_SetSeLoop))
                {
                    var setObject = (Object2597_SetSeLoop)listBoxObjects.SelectedItem;
                    var audioID = setObject.AudioID;
                    // TODO: Probably cache results instead of searching every click
                    var sfxStringIdList = "";
                    for (int i = 0; i < loadedShadowSoundBIN.sfxTable.Count; i++)
                    {
                        var entrySfxId = loadedShadowSoundBIN.sfxTable[i].sfxId;
                        if (entrySfxId == audioID)
                        {
                            setObject.sfxEntry = loadedShadowSoundBIN.sfxTable[i];
                        }
                        sfxStringIdList += loadedShadowSoundBIN.sfxTable[i].sfxId + " " + loadedShadowSoundBIN.sfxTable[i].sfxString.Replace('\0', ' ') + "\r\n";
                    }
                    textBox_FNT_TriggerTalkingPreview.Text = sfxStringIdList;
                }
            }
        }

        private void LoadShadowSubtitlePreviews()
        {
            if (listBoxObjects.SelectedItems.Count != 1)
                return;

            if (listBoxObjects.SelectedItem.GetType() == typeof(Object0051_TriggerTalking)
                || (listBoxObjects.SelectedItem.GetType() == typeof(Object0011_HintBall)))
            {
                string setObjAudioBranchID = "";

                if (listBoxObjects.SelectedItem.GetType() == typeof(Object0051_TriggerTalking))
                {
                    var setObject = (Object0051_TriggerTalking)listBoxObjects.SelectedItem;
                    setObjAudioBranchID = setObject.AudioBranchID.ToString();
                }
                else if (listBoxObjects.SelectedItem.GetType() == typeof(Object0011_HintBall))
                {
                    var setObject = (Object0011_HintBall)listBoxObjects.SelectedItem;
                    setObjAudioBranchID = setObject.AudioBranchID.ToString();
                }

                // TODO: Probably cache results instead of searching every click

                List<int> matchesIndex = new();

                var targetSize = setObjAudioBranchID.Length;
                for (int i = 0; i < loadedFNT.entryTable.Count; i++)
                {
                    var entry = loadedFNT.entryTable[i].messageIdBranchSequence.ToString();
                    //size comparison is faster
                    if (entry.Length != targetSize + 3)
                        continue;
                    if (entry.StartsWith(setObjAudioBranchID))
                        matchesIndex.Add(i);
                }

                var darkText = "";
                var normalText = "";
                var heroText = "";
                darkAudioId = -1;
                normalAudioId = -1;
                heroAudioId = -1;

                foreach (int match in matchesIndex)
                {
                    var entry = loadedFNT.entryTable[match];
                    var entrySubtitle = entry.messageIdBranchSequence.ToString();
                    var branchType = entrySubtitle.Substring(entrySubtitle.Length - 3);
                    if (branchType.StartsWith("0"))
                    {
                        darkText += entry.subtitle.Replace("\n", "\r\n");
                        darkText += "\r\n";

                        if (!branchType.EndsWith("0") && entry.audioId != -1)
                            darkText += "[WARNING: Sequence has a follow up Audio file, and will not be previewed]\r\n";
                        else
                            darkAudioId = entry.audioId;
                    }
                    else if (branchType.StartsWith("1"))
                    {
                        normalText += entry.subtitle.Replace("\n", "\r\n");
                        normalText += "\r\n";
                        if (!branchType.EndsWith("0") && entry.audioId != -1)
                            normalText += "[WARNING: Sequence has a follow up Audio file, and will not be previewed]\r\n";
                        else
                            normalAudioId = entry.audioId;
                    }
                    else if (branchType.StartsWith("2"))
                    {
                        heroText += entry.subtitle.Replace("\n", "\r\n");
                        heroText += "\r\n";
                        if (!branchType.EndsWith("0") && entry.audioId != -1)
                            heroText += "[WARNING: Sequence has a follow up Audio file, and will not be previewed]\r\n";
                        else
                            heroAudioId = entry.audioId;
                    }
                    else
                    {
                        MessageBox.Show("Report this if you encounter it.", "Impossible Bug Occurred");
                    }
                }
                if (darkText == "")
                    darkText = "|| No entry ||\r\n";
                if (normalText == "")
                    normalText = "|| No entry ||\r\n";
                if (heroText == "")
                    heroText = "|| No entry ||";
                textBox_FNT_TriggerTalkingPreview.Text = "Dark:\r\n";
                textBox_FNT_TriggerTalkingPreview.Text += darkText.Replace("\0", "");
                textBox_FNT_TriggerTalkingPreview.Text += "\r\nNormal:\r\n";
                textBox_FNT_TriggerTalkingPreview.Text += normalText.Replace("\0", "");
                textBox_FNT_TriggerTalkingPreview.Text += "\r\nHero:\r\n";
                textBox_FNT_TriggerTalkingPreview.Text += heroText.Replace("\0", "");
            }
        }

        private void ButtonAdd_Click(object sender, EventArgs e)
        {
            layoutSystem.AddNewSetObject();
            listBoxObjects.ClearSelected();
            listBoxObjects.SelectedIndex = layoutSystem.GetSetObjectAmount() - 1;
        }

        private void buttonDuplicate_Click(object sender, EventArgs e)
        {
            int count = layoutSystem.PasteSetObject(layoutSystem.SerializeSetObject(listBoxObjects.SelectedIndices));

            listBoxObjects.ClearSelected();
            for (int i = 0; i < count; i++)
                listBoxObjects.SetSelected(listBoxObjects.Items.Count - i - 1, true);
        }

        private void buttonCopy_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(layoutSystem.SerializeSetObject(listBoxObjects.SelectedIndices));
        }

        private void buttonPaste_Click(object sender, EventArgs e)
        {
            int count = layoutSystem.PasteSetObject();

            listBoxObjects.ClearSelected();
            for (int i = 0; i < count; i++)
                listBoxObjects.SetSelected(listBoxObjects.Items.Count - i - 1, true);
        }

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            if (listBoxObjects.SelectedIndices.Count > 0)
            {
                ProgramIsChangingStuff = true;

                listBoxObjects.BeginUpdate();

                var sel = new List<int>();
                foreach (int v in listBoxObjects.SelectedIndices)
                    sel.Add(v);

                sel.Reverse();

                foreach (int v in sel)
                    RemoveSetObjectAt(v);

                listBoxObjects.EndUpdate();

                ProgramIsChangingStuff = false;
                listBoxObjects_SelectedIndexChanged(null, null);
            }
        }

        private void RemoveSetObjectAt(int index)
        {
            int Temp = listBoxObjects.SelectedIndices[0];

            listBoxObjects.ClearSelected();

            layoutSystem.RemoveSetObject(index);

            try { listBoxObjects.SelectedIndex = Temp; }
            catch { listBoxObjects.SelectedIndex = Temp - 1; }
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            listBoxObjects.BeginUpdate();
            layoutSystem.ClearList();
            listBoxObjects.EndUpdate();
        }

        private void ComboBoxObject_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!ProgramIsChangingStuff && listBoxObjects.SelectedIndices.Count == 1)
            {
                layoutSystem.ChangeObjectType(listBoxObjects.SelectedIndex, ComboBoxObject.SelectedItem as ObjectEntry);
                UpdateList();
                PropertyGridMisc.SelectedObject = layoutSystem.GetSetObjectAt(listBoxObjects.SelectedIndex);
            }
        }

        private void NumericPos_ValueChanged(object sender, EventArgs e)
        {
            if (!ProgramIsChangingStuff && listBoxObjects.SelectedIndices.Count == 1)
            {
                layoutSystem.SetObjectPosition(listBoxObjects.SelectedIndex, (float)NumericPosX.Value, (float)NumericPosY.Value, (float)NumericPosZ.Value);
                UpdateGizmoPosition();
            }
        }

        private void NumericRot_ValueChanged(object sender, EventArgs e)
        {
            if (!ProgramIsChangingStuff && listBoxObjects.SelectedIndices.Count == 1)
            {
                layoutSystem.SetObjectRotation(listBoxObjects.SelectedIndex, (float)NumericRotX.Value, (float)NumericRotY.Value, (float)NumericRotZ.Value);
                UpdateGizmoPosition();
            }
        }

        private void NumericObjLink_ValueChanged(object sender, EventArgs e)
        {
            if (!ProgramIsChangingStuff && listBoxObjects.SelectedIndices.Count == 1)
            {
                layoutSystem.SetObjectLink(listBoxObjects.SelectedIndex, (byte)NumericObjLink.Value);
                UpdateList();
            }
        }

        private void ButtonFindNextLink_Click(object sender, EventArgs e)
        {
            if (listBoxObjects.SelectedIndices.Count == 1)
            {
                int newIndex = layoutSystem.FindNext(listBoxObjects.SelectedIndex);

                if (newIndex == listBoxObjects.SelectedIndex)
                    MessageBox.Show("No other object has this same Link ID!");
                else
                {
                    listBoxObjects.SelectedIndices.Clear();
                    listBoxObjects.SelectedIndex = newIndex;
                }
            }
        }

        private void NumericObjRend_ValueChanged(object sender, EventArgs e)
        {
            if (!ProgramIsChangingStuff && listBoxObjects.SelectedIndices.Count == 1)
                layoutSystem.SetObjectRend(listBoxObjects.SelectedIndex, (byte)NumericObjRend.Value);
        }

        private void numericUnkB_ValueChanged(object sender, EventArgs e)
        {
            if (!ProgramIsChangingStuff && listBoxObjects.SelectedIndices.Count == 1)
                layoutSystem.SetUnkBytes(listBoxObjects.SelectedIndex,
                    (byte)numericUnkB1.Value, (byte)numericUnkB2.Value, (byte)numericUnkB3.Value, (byte)numericUnkB4.Value,
                    (byte)numericUnkB5.Value, (byte)numericUnkB6.Value, (byte)numericUnkB7.Value, (byte)numericUnkB8.Value);
        }

        private void ButtonGetSpeed_Click(object sender, EventArgs e)
        {
            if (listBoxObjects.SelectedIndices.Count == 1)
            {
                if (layoutSystem.GetSpeedMemory(listBoxObjects.SelectedIndex))
                    UpdateDisplayData();
                else
                    MessageBox.Show("Error collecting data", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ButtonGetFly_Click(object sender, EventArgs e)
        {
            if (listBoxObjects.SelectedIndices.Count == 1)
            {
                if (layoutSystem.GetFlyMemory(listBoxObjects.SelectedIndex))
                    UpdateDisplayData();
                else
                    MessageBox.Show("Error collecting data", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ButtonGetPow_Click(object sender, EventArgs e)
        {
            if (listBoxObjects.SelectedIndices.Count == 1)
            {
                if (layoutSystem.GetPowMemory(listBoxObjects.SelectedIndex))
                    UpdateDisplayData();
                else
                    MessageBox.Show("Error collecting data", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ButtonSpeedRot_Click(object sender, EventArgs e)
        {
            if (listBoxObjects.SelectedIndices.Count == 1)
            {
                if (layoutSystem.GetSpeedRotMemory(listBoxObjects.SelectedIndex))
                    UpdateDisplayData();
                else
                    MessageBox.Show("Error collecting data", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ButtonFlyRot_Click(object sender, EventArgs e)
        {
            if (listBoxObjects.SelectedIndices.Count == 1)
            {
                if (layoutSystem.GetFlyRotMemory(listBoxObjects.SelectedIndex))
                    UpdateDisplayData();
                else
                    MessageBox.Show("Error collecting data", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ButtonPowRot_Click(object sender, EventArgs e)
        {
            if (listBoxObjects.SelectedIndices.Count == 1)
            {
                if (layoutSystem.GetPowRotMemory(listBoxObjects.SelectedIndex))
                    UpdateDisplayData();
                else
                    MessageBox.Show("Error collecting data", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ButtonTeleport_Click(object sender, EventArgs e)
        {
            if (listBoxObjects.SelectedIndices.Count == 1)
            {
                if (!layoutSystem.Teleport(listBoxObjects.SelectedIndex))
                    MessageBox.Show("Error writing data", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonDrop_Click(object sender, EventArgs e)
        {
            if (listBoxObjects.SelectedIndices.Count == 1)
            {
                layoutSystem.Drop(listBoxObjects.SelectedIndex);
                UpdateDisplayData();
                UpdateGizmoPosition();
            }
        }

        private void buttonForceReload_Click(object sender, EventArgs e)
        {

        }

        public string GetOpenFileName()
        {
            return layoutSystem.CurrentlyOpenFileName;
        }

        public void OpenFile(string fileName, MainForm.MainForm mainForm)
        {
            layoutSystem.SelectedIndexChanged(new int[] { -1 });
            ProgramIsChangingStuff = true;
            layoutSystem.OpenLayoutFile(fileName);
            UpdateObjectComboBox();
            UpdateFileLabel(mainForm);
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
                UpdateFileLabel(Program.MainForm);
            }
        }

        public bool finishedMovingGizmo = false;

        public void ScreenClicked(SharpRenderer renderer, Ray r, bool isMouseDown, bool showAllObjects, out float distance, out int index)
        {
            distance = 40000f;
            index = -1;

            if (checkBoxDrawObjs.Checked)
            {
                if (isMouseDown)
                    GizmoSelect(r);
                else
                    layoutSystem.ScreenClicked(renderer.Camera.GetPosition(), r, showAllObjects, checkBoxDrawTriggerObjs.Checked, out index, out distance);
            }
        }

        public void SetSelectedIndex(int index, bool isCtrlDown)
        {
            if (!isCtrlDown)
                listBoxObjects.ClearSelected();
            listBoxObjects.SelectedIndices.Add(index);
        }

        public void PlaceObject(Vector3 Position)
        {
            if (listBoxObjects.SelectedIndices.Count != 1)
                return;
            layoutSystem.DuplicateSetObjectAt(listBoxObjects.SelectedIndex, Position);
            listBoxObjects.ClearSelected();
            listBoxObjects.SelectedIndex = layoutSystem.GetSetObjectAmount() - 1;
        }

        private void UpdateObjectComboBox()
        {
            ComboBoxObject.Items.Clear();
            ComboBoxObject.Items.AddRange(LayoutEditorSystem.GetActiveObjectEntries());

            UpdateList();
        }

        private void UpdateList()
        {
            typeof(ListBox).InvokeMember("RefreshItems",
              BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.InvokeMethod,
              null, listBoxObjects, new object[] { });

            UpdateObjectAmountLabel();
        }

        public static void BindField(Control control, string propertyName, object dataSource, string dataMember)
        {
            Binding bd;

            for (int index = control.DataBindings.Count - 1; (index == 0); index--)
            {
                bd = control.DataBindings[index];
                if (bd.PropertyName == propertyName)
                    control.DataBindings.Remove(bd);
            }
            control.DataBindings.Add(propertyName, dataSource, dataMember);
        }

        private void UpdateDisplayData()
        {
            if (listBoxObjects.SelectedIndices.Count == 1)
            {
                if (displayDataDisabled)
                    EnableDisplayData();

                ProgramIsChangingStuff = true;

                NumericPosX.Value = layoutSystem.GetPosX(listBoxObjects.SelectedIndex);
                NumericPosY.Value = layoutSystem.GetPosY(listBoxObjects.SelectedIndex);
                NumericPosZ.Value = layoutSystem.GetPosZ(listBoxObjects.SelectedIndex);
                NumericRotX.Value = layoutSystem.GetRotX(listBoxObjects.SelectedIndex);
                NumericRotY.Value = layoutSystem.GetRotY(listBoxObjects.SelectedIndex);
                NumericRotZ.Value = layoutSystem.GetRotZ(listBoxObjects.SelectedIndex);

                foreach (ObjectEntry o in LayoutEditorSystem.GetActiveObjectEntries())
                    if (o.List == layoutSystem.GetSetObjectAt(listBoxObjects.SelectedIndex).List && o.Type == layoutSystem.GetSetObjectAt(listBoxObjects.SelectedIndex).Type)
                        ComboBoxObject.SelectedItem = o;

                NumericObjLink.Value = layoutSystem.GetObjectLink(listBoxObjects.SelectedIndex);
                NumericObjRend.Value = layoutSystem.GetObjectRend(listBoxObjects.SelectedIndex);

                numericUnkB1.Value = layoutSystem.GetUnkBytes(listBoxObjects.SelectedIndex)[0];
                numericUnkB2.Value = layoutSystem.GetUnkBytes(listBoxObjects.SelectedIndex)[1];
                numericUnkB3.Value = layoutSystem.GetUnkBytes(listBoxObjects.SelectedIndex)[2];
                numericUnkB4.Value = layoutSystem.GetUnkBytes(listBoxObjects.SelectedIndex)[3];
                numericUnkB5.Value = layoutSystem.GetUnkBytes(listBoxObjects.SelectedIndex)[4];
                numericUnkB6.Value = layoutSystem.GetUnkBytes(listBoxObjects.SelectedIndex)[5];
                numericUnkB7.Value = layoutSystem.GetUnkBytes(listBoxObjects.SelectedIndex)[6];
                numericUnkB8.Value = layoutSystem.GetUnkBytes(listBoxObjects.SelectedIndex)[7];

                PropertyGridMisc.SelectedObject = layoutSystem.GetSetObjectAt(listBoxObjects.SelectedIndex);
                ProgramIsChangingStuff = false;
            }

            UpdateObjectAmountLabel();
        }

        private void UpdateDisplayData_PositionRotation_Only()
        {
            if (listBoxObjects.SelectedIndices.Count == 1)
            {
                if (displayDataDisabled)
                    EnableDisplayData();

                ProgramIsChangingStuff = true;

                NumericPosX.Value = layoutSystem.GetPosX(listBoxObjects.SelectedIndex);
                NumericPosY.Value = layoutSystem.GetPosY(listBoxObjects.SelectedIndex);
                NumericPosZ.Value = layoutSystem.GetPosZ(listBoxObjects.SelectedIndex);
                NumericRotX.Value = layoutSystem.GetRotX(listBoxObjects.SelectedIndex);
                NumericRotY.Value = layoutSystem.GetRotY(listBoxObjects.SelectedIndex);
                NumericRotZ.Value = layoutSystem.GetRotZ(listBoxObjects.SelectedIndex);

                ProgramIsChangingStuff = false;
            }
        }

        private bool displayDataDisabled = true;

        private void DisableDisplayData()
        {
            if (!displayDataDisabled)
            {
                ComboBoxObject.Enabled = false;
                groupBox3.Enabled = false;
                GroupBox2.Enabled = false;
                NumericObjLink.Enabled = false;
                NumericObjRend.Enabled = false;
                GroupBoxGameStuff.Enabled = false;
                PropertyGridMisc.Enabled = false;
                buttonViewHere.Enabled = false;
                buttonDrop.Enabled = false;
                buttonCurrentViewDrop.Enabled = false;
                groupBox1.Enabled = false;

                displayDataDisabled = true;
            }
        }

        private void EnableDisplayData()
        {
            if (displayDataDisabled)
            {
                ComboBoxObject.Enabled = true;
                groupBox3.Enabled = true;
                GroupBox2.Enabled = true;
                NumericObjLink.Enabled = true;
                NumericObjRend.Enabled = true;
                GroupBoxGameStuff.Enabled = true;
                PropertyGridMisc.Enabled = true;
                buttonViewHere.Enabled = true;
                buttonDrop.Enabled = true;
                buttonCurrentViewDrop.Enabled = true;
                groupBox1.Enabled = true;

                displayDataDisabled = false;
            }
        }

        private void UpdateObjectAmountLabel()
        {
            if (listBoxObjects.Items.Count == 0)
                objectAmountLabel.Text = "0 objects";
            else
            {
                objectAmountLabel.Text = listBoxObjects.SelectedIndex == -1 ?
                    layoutSystem.GetSetObjectAmount().ToString() + " objects" :
                    $"{listBoxObjects.SelectedIndex + 1}/{layoutSystem.GetSetObjectAmount()} objects";
            }
        }

        private void UpdateFileLabel(MainForm.MainForm mainForm)
        {
            if (string.IsNullOrEmpty(layoutSystem.CurrentlyOpenFileName))
                openFileLabel.Text = "No file loaded";
            else
                openFileLabel.Text = layoutSystem.CurrentlyOpenFileName;

            Text = "Layout Editor - " + Path.GetFileName(layoutSystem.CurrentlyOpenFileName);
            mainForm.SetLayoutEditorStripItemName(this, Path.GetFileName(layoutSystem.CurrentlyOpenFileName));
        }

        private void buttonCopyMisc_Click(object sender, EventArgs e)
        {
            if (listBoxObjects.SelectedIndices.Count == 1)
                layoutSystem.CopyMisc(listBoxObjects.SelectedIndex);
        }

        private void buttonPasteMisc_Click(object sender, EventArgs e)
        {
            if (listBoxObjects.SelectedIndices.Count == 1)
            {
                layoutSystem.PasteMisc(listBoxObjects.SelectedIndex);
                PropertyGridMisc.Refresh();
            }
        }

        public void RenderSetObjects(SharpRenderer renderer, bool drawEveryObject)
        {
            if (checkBoxDrawObjs.Checked)
            {
                layoutSystem.RenderSetObjects(renderer, drawEveryObject, checkBoxDrawTriggerObjs.Checked);

                if (DrawGizmos)
                    foreach (Gizmo g in gizmos)
                        g.Draw(renderer);
            }
        }

        public void UpdateAllMatrices()
        {
            layoutSystem.UpdateAllMatrices();
        }

        public void UpdateSetParticleMatrices()
        {
            layoutSystem.UpdateSetParticleMatrices();
        }

        public (byte, byte)[] GetAllCurrentObjectEntries()
        {
            return layoutSystem.GetAllCurrentObjectEntries();
        }

        // Gizmos
        private Gizmo[] gizmos;
        private bool DrawGizmos = false;

        public void UpdateGizmoPosition()
        {
            if (listBoxObjects.SelectedIndices.Count == 0 || !checkBoxDrawObjs.Checked)
                ClearGizmos();
            else
            {
                BoundingSphere b = layoutSystem.GetSetObjectAt(listBoxObjects.SelectedIndex).GetGizmoCenter();
                foreach (int i in listBoxObjects.SelectedIndices)
                    b = BoundingSphere.Merge(b, layoutSystem.GetSetObjectAt(i).GetGizmoCenter());
                UpdateGizmoPosition(b);
            }
        }

        private void UpdateGizmoPosition(BoundingSphere position)
        {
            DrawGizmos = true;
            foreach (Gizmo g in gizmos)
                g.SetPosition(position);
        }

        private void ClearGizmos()
        {
            DrawGizmos = false;
        }

        private bool GizmoSelect(Ray r)
        {
            if (!DrawGizmos || !checkBoxDrawObjs.Checked)
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

        public void MouseMoveForPosition(Matrix viewProjection, int distanceX, int distanceY)
        {
            if (gizmos[0].isSelected || gizmos[1].isSelected || gizmos[2].isSelected)
            {
                Vector3 gizmoCenter = layoutSystem.GetSetObjectAt(listBoxObjects.SelectedIndex).GetGizmoCenter().Center;
                Vector3 direction1 = (Vector3)Vector3.Transform(gizmoCenter, viewProjection);

                if (gizmos[0].isSelected)
                {
                    Vector3 direction2 = (Vector3)Vector3.Transform(gizmoCenter + Vector3.UnitX, viewProjection);
                    Vector3 direction = direction2 - direction1;
                    direction.Z = 0;
                    direction.Normalize();

                    foreach (int i in listBoxObjects.SelectedIndices)
                    {
                        layoutSystem.GetSetObjectAt(i).Position.X += (distanceX * direction.X - distanceY * direction.Y) / 2;
                        layoutSystem.GetSetObjectAt(i).CreateTransformMatrix();
                        UpdateDisplayData_PositionRotation_Only();
                    }
                }
                else if (gizmos[1].isSelected)
                {
                    Vector3 direction2 = (Vector3)Vector3.Transform(gizmoCenter + Vector3.UnitY, viewProjection);
                    Vector3 direction = direction2 - direction1;
                    direction.Z = 0;
                    direction.Normalize();

                    foreach (int i in listBoxObjects.SelectedIndices)
                    {
                        layoutSystem.GetSetObjectAt(i).Position.Y += (distanceX * direction.X - distanceY * direction.Y) / 2;
                        layoutSystem.GetSetObjectAt(i).CreateTransformMatrix();
                        UpdateDisplayData_PositionRotation_Only();
                    }
                }
                else if (gizmos[2].isSelected)
                {
                    Vector3 direction2 = (Vector3)Vector3.Transform(gizmoCenter + Vector3.UnitZ, viewProjection);
                    Vector3 direction = direction2 - direction1;
                    direction.Z = 0;
                    direction.Normalize();

                    foreach (int i in listBoxObjects.SelectedIndices)
                    {
                        layoutSystem.GetSetObjectAt(i).Position.Z += (distanceX * direction.X - distanceY * direction.Y) / 2;
                        layoutSystem.GetSetObjectAt(i).CreateTransformMatrix();
                        UpdateDisplayData_PositionRotation_Only();
                    }
                }

                finishedMovingGizmo = true;
                UpdateGizmoPosition();
            }
        }

        private void buttonCurrentViewDrop_Click(object sender, EventArgs e)
        {
            if (listBoxObjects.SelectedIndices.Count == 1)
            {
                layoutSystem.DropToCurrentView(listBoxObjects.SelectedIndex);
                UpdateDisplayData();
                UpdateGizmoPosition();
            }
        }

        private void LayoutEditor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Modifiers == Keys.Shift && e.KeyCode == Keys.Tab)
            {
                this.SelectNextControl(ActiveControl, false, true, true, true);
                e.Handled = e.SuppressKeyPress = true;
            }
            else if (e.KeyCode == Keys.Tab)
            {
                this.SelectNextControl(ActiveControl, true, true, true, true);
                e.Handled = e.SuppressKeyPress = true;
            }
        }

        private void checkBoxAutoBytes_CheckedChanged(object sender, EventArgs e)
        {
            layoutSystem.autoUnkBytes = checkBoxAutoBytes.Checked;
        }

        public void GetClickedModelPosition(Ray ray, Vector3 camPos, bool seeAllObjects, out bool hasIntersected, out float smallestDistance)
        {
            hasIntersected = false;
            smallestDistance = 0;

            if (checkBoxDrawObjs.Checked)
                layoutSystem.GetClickedModelPosition(ray, camPos, seeAllObjects, out hasIntersected, out smallestDistance);
        }

        private void importSALayoutFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using OpenFileDialog openFile = new OpenFileDialog() { Multiselect = true };
            if (openFile.ShowDialog() == DialogResult.OK)
                foreach (var s in openFile.FileNames)
                    layoutSystem.ImportSALayout(s);
        }

        private void PropertyGridMisc_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            layoutSystem.GetSetObjectAt(listBoxObjects.SelectedIndex).CreateTransformMatrix();
        }

        private void buttonLoadAFS_Click(object sender, EventArgs e)
        {

            // TODO migrate this to project scope rather than layout

            using OpenFileDialog openFile = new OpenFileDialog
            {
                Filter = "AFS files (*.afs)|*.afs|All files (*.*)|*.*"
            };
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                var data = File.ReadAllBytes(openFile.FileName);
                AFSLib.AfsArchive.TryFromFile(data, out var afsArchive);
                {
                    loadedAFS = afsArchive;
                }
            }
        }

        private void buttonLoadFNT_Click(object sender, EventArgs e)
        {
            using OpenFileDialog openFile = new OpenFileDialog
            {
                Filter = "FNT files (*.fnt)|*.fnt|All files (*.*)|*.*"
            };
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                SetLoadedFNT(openFile.FileName);
            }
        }
        private void SetLoadedFNT(string fileName)
        {
            var data = File.ReadAllBytes(fileName);
            loadedFNT = FNT.ParseFNTFile(fileName, ref data);
        }

        private void buttonPlayTriggerTalkingAudio_Click(object sender, EventArgs e)
        {
            if (loadedAFS == null || loadedFNT.fileName == null)
                return;
            if (listBoxObjects.SelectedItems.Count != 1)
                return;

            // TODO: better type optimization? (make parent container "SupportedFNT"?)

            AudioBranchType setObjAudioBranchType = AudioBranchType.CurrentMissionPartner;

            if (listBoxObjects.SelectedItem.GetType() == typeof(Object0051_TriggerTalking))
            {
                var setObject = (Object0051_TriggerTalking)listBoxObjects.SelectedItem;
                setObjAudioBranchType = setObject.AudioBranchType;
            }
            else if (listBoxObjects.SelectedItem.GetType() == typeof(Object0011_HintBall))
            {
                var setObject = (Object0011_HintBall)listBoxObjects.SelectedItem;
                setObjAudioBranchType = setObject.AudioBranchType;
            }

            switch (setObjAudioBranchType)
            {
                case AudioBranchType.Dark:
                    previewAudio(darkAudioId);
                    break;
                case AudioBranchType.Normal:
                    previewAudio(normalAudioId);
                    break;
                case AudioBranchType.Hero:
                    previewAudio(heroAudioId);
                    break;
                default:
                    //queue all
                    previewAudio(darkAudioId, normalAudioId, heroAudioId);
                    break;
            }
        }

        private void previewAudio(int audioId)
        {
            if (audioId == -1)
                return;
            try
            {
                LoadAdxToWav(audioId, out var stream);

                var player = new System.Media.SoundPlayer();
                stream.Position = 0;
                player.Stream = stream;
                player.Play();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "An Exception Occurred While Trying to Play Audio");
            }
        }

        private void previewAudio(int audioId1, int audioId2, int audioId3)
        {
            System.Threading.Tasks.Task.Run(() =>
            {
                try
                {
                    var player = new System.Media.SoundPlayer();

                    if (audioId1 != -1)
                    {
                        LoadAdxToWav(audioId1, out var stream);
                        stream.Position = 0;
                        player.Stream = stream;
                        player.PlaySync();
                    }
                    if (audioId2 != -1)
                    {
                        LoadAdxToWav(audioId2, out var stream);
                        stream.Position = 0;
                        player.Stream = stream;
                        player.PlaySync();

                    }
                    if (audioId3 != -1)
                    {
                        LoadAdxToWav(audioId3, out var stream);
                        stream.Position = 0;
                        player.Stream = stream;
                        player.PlaySync();

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "An Exception Occurred While Trying to Play Audio");
                }
            });
        }

        private void LoadAdxToWav(int audioId, out MemoryStream waveStream)
        {
            var decoder = new VGAudio.Containers.Adx.AdxReader();
            var audio = decoder.Read(loadedAFS.Files[audioId].Data);
            var writer = new VGAudio.Containers.Wave.WaveWriter();
            waveStream = new();
            writer.WriteToStream(audio, waveStream);
        }

        private void LayoutEditor_Load(object sender, EventArgs e)
        {
            if (HPPConfig.GetInstance().LegacyWindowPriorityBehavior)
                TopMost = true;
            else
                TopMost = false;
        }

        private void buttonLoadShadowSFXBIN_Click(object sender, EventArgs e)
        {
            // TODO migrate this to project scope rather than layout

            using OpenFileDialog openFile = new OpenFileDialog
            {
                Filter = "SHADOW SFX BIN files (*.bin)|*.bin|All files (*.*)|*.*"
            };
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                var data = File.ReadAllBytes(openFile.FileName);
                loadedShadowSoundBIN = ShadowSoundBIN.ParseShadowSoundBINFile(openFile.FileName, ref data);
            }
        }
    }
}
