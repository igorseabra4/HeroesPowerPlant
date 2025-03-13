﻿using HeroesPowerPlant.Shared.IO.Config;
using Newtonsoft.Json;
using Ookii.Dialogs.WinForms;
using Shadow.Structures;
using SharpDX;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace HeroesPowerPlant.LayoutEditor
{
    public partial class LayoutEditor : Form, IUnsavedChanges
    {
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
            TextBox_PreviewView.Hide();
            buttonPlayLinkedAudio.Hide();
            GroupBoxSonicHeroesStuff.Hide();

#if !DEBUG
            importSALayoutFileToolStripMenuItem.Visible = false;
            openFolderToolStripMenuItem.Visible = false;
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

            SetObjectFormatToolStripItems();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.WindowsShutDown)
                return;
            if (e.CloseReason == CloseReason.FormOwnerClosing)
                return;

            e.Cancel = true;
            Hide();
        }

        private LayoutEditorSystem layoutSystem;
        private bool ProgramIsChangingStuff = false;

        private void heroesLayoutToolStripMenuItem_Click(object sender, EventArgs e)
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

            listBoxObjects.BeginUpdate();
            layoutSystem.NewHeroesLayout();
            UpdateHiddenUI(false);
            listBoxObjects.EndUpdate();

            UpdateObjectComboBox();
            UpdateFileLabel(Program.MainForm);
            UnsavedChanges = true;
        }


        private void shadowLayoutToolStripMenuItem_Click(object sender, EventArgs e)
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

            listBoxObjects.BeginUpdate();
            layoutSystem.NewShadowLayout();
            UpdateHiddenUI(true);
            listBoxObjects.EndUpdate();

            UpdateObjectComboBox();
            UpdateFileLabel(Program.MainForm);
            UnsavedChanges = true;
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

            VistaOpenFileDialog openFile = new VistaOpenFileDialog
            {
                Filter = "All supported types|*.bin; *.dat|BIN Files|*.bin|DAT Files|*.dat"
            };

            if (openFile.ShowDialog() == DialogResult.OK)
                OpenFile(openFile.FileName, Program.MainForm);
            listBoxObjects.SelectedIndex = -1;
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Save();
        }

        public void Save()
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

        private void alphabeticallyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listBoxObjects.BeginUpdate();
            layoutSystem.SortObjectsAlphabetical();
            listBoxObjects.EndUpdate();
        }

        private void exportINIToolStripMenuItem_Click(object sender, EventArgs e)
        {
            VistaSaveFileDialog saveFileDialog = new VistaSaveFileDialog()
            {
                Filter = ".ini files|*.ini",
                DefaultExt = ".ini",
                FileName = Path.ChangeExtension(layoutSystem.CurrentlyOpenFileName, ".ini")
            };
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                layoutSystem.SaveINI(saveFileDialog.FileName);
            }
        }

        private void importINIToolStripMenuItem_Click(object sender, EventArgs e)
        {
            VistaOpenFileDialog openFile = new VistaOpenFileDialog()
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
            VistaOpenFileDialog openFile = new VistaOpenFileDialog
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
            VistaOpenFileDialog openFile = new VistaOpenFileDialog
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
            Program.MainForm.UnselectEveryoneExceptMe(GetHashCode());

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

            TextAndAudioPreviewUpdate();
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
                    TextBox_PreviewView.Text = sfxStringIdList;
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
                    TextBox_PreviewView.Text = sfxStringIdList;
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
                for (int i = 0; i < Program.MainForm.loadedFNT.GetEntryTableCount(); i++)
                {
                    var entry = Program.MainForm.loadedFNT.GetEntryMessageIdBranchSequence(i).ToString();
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
                    var entry = Program.MainForm.loadedFNT.GetTableEntry(match);
                    var entrySubtitle = entry.messageIdBranchSequence.ToString();
                    var branchType = entrySubtitle.Substring(entrySubtitle.Length - 3);
                    if (branchType.StartsWith("0"))
                    {
                        darkText += entry.subtitle.Replace("\n", "\r\n");
                        darkText += "\r\n";

                        if (!branchType.EndsWith("0"))
                        {
                            if (entry.audioId != -1)
                                darkText += "[WARNING: Sequence has a follow up Audio file, and will not be previewed]\r\n";
                        }
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
                TextBox_PreviewView.Text = "Dark:\r\n";
                TextBox_PreviewView.Text += darkText.Replace("\0", "");
                TextBox_PreviewView.Text += "\r\nNormal:\r\n";
                TextBox_PreviewView.Text += normalText.Replace("\0", "");
                TextBox_PreviewView.Text += "\r\nHero:\r\n";
                TextBox_PreviewView.Text += heroText.Replace("\0", "");
            }
            else
            {
                darkAudioId = -1;
                normalAudioId = -1;
                heroAudioId = -1;
                TextBox_PreviewView.Text = "";
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
                UnsavedChanges = true;
            }
        }

        private void RemoveSetObjectAt(int index)
        {
            int Temp = listBoxObjects.SelectedIndices[0];

            listBoxObjects.ClearSelected();

            layoutSystem.RemoveSetObject(index);

            try
            { listBoxObjects.SelectedIndex = Temp; }
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

        public string GetOpenFileName()
        {
            return layoutSystem.CurrentlyOpenFileName;
        }

        public void OpenFile(string fileName, MainForm.MainForm mainForm, bool visibleObjects = true)
        {
            ProgramIsChangingStuff = true;
            listBoxObjects.BeginUpdate();
            var f = new Dictionary<(byte, byte, string), HashSet<byte[]>>();
            layoutSystem.OpenLayoutFile(fileName, out _, ref f);
            listBoxObjects.EndUpdate();
            UpdateHiddenUI(layoutSystem.IsShadow);
            UpdateObjectComboBox();
            UpdateFileLabel(mainForm);
            checkBoxDrawObjs.Checked = visibleObjects;
            UnsavedChanges = false;
        }

        private void UpdateHiddenUI(bool IsShadow)
        {
            if (IsShadow)
            {
                TextBox_PreviewView.Show();
                GroupBoxSonicHeroesStuff.Hide();
                buttonPlayLinkedAudio.Show();
            }
            else
            {
                TextBox_PreviewView.Hide();
                GroupBoxSonicHeroesStuff.Show();
                buttonPlayLinkedAudio.Hide();
            }
        }

        private void SaveAs()
        {
            VistaSaveFileDialog saveFile = new VistaSaveFileDialog
            {
                Filter = layoutSystem.IsShadow ? "DAT Files|*.dat" : "BIN Files|*.bin",
                DefaultExt = layoutSystem.IsShadow ? ".dat" : ".bin",
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

        private void SetObjectFormatToolStripItems()
        {
            objectToolStripMenuItem.Checked = !ObjectEntry.AlternateFormat;
            object0000ToolStripMenuItem.Checked = ObjectEntry.AlternateFormat;
            useDebugNamesToolStripMenuItem.Checked = ObjectEntry.UseDebugNames;
        }

        private void UpdateList()
        {
            typeof(ListBox).InvokeMember("RefreshItems",
              BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.InvokeMethod,
              null, listBoxObjects, new object[] { });

            UpdateObjectAmountLabel();
        }

        private void UpdateDisplayData()
        {
            buttonGetTemplate.Enabled = false;

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

                buttonGetTemplate.Enabled = templates.ContainsKey((layoutSystem.GetSetObjectAt(listBoxObjects.SelectedIndex).List, layoutSystem.GetSetObjectAt(listBoxObjects.SelectedIndex).Type));

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
                GroupBoxSonicHeroesStuff.Enabled = false;
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
                GroupBoxSonicHeroesStuff.Enabled = true;
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
            if (listBoxObjects.SelectedIndices.Count == 0)
                objectAmountLabel.Text = layoutSystem.GetSetObjectAmount().ToString() + " objects";
            else if (listBoxObjects.SelectedIndices.Count == 1)
                objectAmountLabel.Text = $"Selected object {listBoxObjects.SelectedIndex + 1} of {layoutSystem.GetSetObjectAmount()}";
            else
                objectAmountLabel.Text = $"Selected {listBoxObjects.SelectedIndices.Count} of {layoutSystem.GetSetObjectAmount()} objects";
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
            using VistaOpenFileDialog openFile = new VistaOpenFileDialog() { Multiselect = true };
            if (openFile.ShowDialog() == DialogResult.OK)
                foreach (var s in openFile.FileNames)
                    layoutSystem.ImportSALayout(s);
        }

        private void PropertyGridMisc_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            layoutSystem.GetSetObjectAt(listBoxObjects.SelectedIndex).CreateTransformMatrix();
            UnsavedChanges = true;
            TextAndAudioPreviewUpdate();
        }

        private void buttonPlayTriggerTalkingAudio_Click(object sender, EventArgs e)
        {
            if (Program.MainForm.locationAFS == null || Program.MainForm.loadedFNT.fileName == null)
                return;
            if (listBoxObjects.SelectedItems.Count != 1)
                return;

            // TODO: better type optimization? (make parent container "SupportedFNT"?)

            EAudioBranchType setObjAudioBranchType = EAudioBranchType.CurrentMissionPartner;

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
                case EAudioBranchType.Dark:
                    previewAudio(darkAudioId);
                    break;
                case EAudioBranchType.Normal:
                    previewAudio(normalAudioId);
                    break;
                case EAudioBranchType.Hero:
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
            try
            {
                FileStream fs = File.OpenRead(Program.MainForm.locationAFS);
                var afsData = AFSLib.AfsArchive.SeekToAndLoadDataFromIndex(fs, audioId);
                fs.Dispose();
                var audio = decoder.Read(afsData);
                var writer = new VGAudio.Containers.Wave.WaveWriter();
                waveStream = new();
                writer.WriteToStream(audio, waveStream);
            }
            catch (IOException e)
            {
                MessageBox.Show(e.Message);
                waveStream = null;
            }
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

            using VistaOpenFileDialog openFile = new VistaOpenFileDialog
            {
                Filter = "SHADOW SFX BIN files (*.bin)|*.bin|All files (*.*)|*.*"
            };
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                var data = File.ReadAllBytes(openFile.FileName);
                loadedShadowSoundBIN = ShadowSoundBIN.ParseShadowSoundBINFile(openFile.FileName, ref data);
            }
        }

        private void TextAndAudioPreviewUpdate()
        {
            if (Program.MainForm.loadedFNT.fileName != null)
            {
                LoadShadowSubtitlePreviews();
            }

            if (loadedShadowSoundBIN.fileName != null)
            {
                LoadShadowSFXBinStrings();
            }
        }

        private void openFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var openFolder = new VistaFolderBrowserDialog();
            if (openFolder.ShowDialog() == DialogResult.OK)
            {
                listBoxObjects.DataSource = new System.ComponentModel.BindingList<SetObject>();
                string result = "";
                var miscSettingsDict = new Dictionary<(byte, byte, string), HashSet<byte[]>>();
                foreach (var f in Directory.GetFiles(openFolder.SelectedPath))
                    if (Path.GetExtension(f).ToLower().Equals(".bin") || Path.GetExtension(f).ToLower().Equals(".dat"))
                    {
                        try
                        {
                            layoutSystem.OpenLayoutFile(f, out string fileResult, ref miscSettingsDict);
                            if (!string.IsNullOrEmpty(fileResult))
                            {
                                result += Path.GetFileNameWithoutExtension(f) + ":\n" + fileResult + "\n";
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Error opening file {Path.GetFileNameWithoutExtension(f)}: {ex.Message}");
                        }
                    }
                if (!string.IsNullOrEmpty(result))
                {
                    MessageBox.Show("Errors dumped");
                    File.WriteAllText("layout_result_dump.txt", result);
                }
                else
                    MessageBox.Show("All objects misc settings written successfully.");
                var result2 = new HashSet<string>();
                foreach (var v in miscSettingsDict)
                {
                    foreach (var b in v.Value)
                    {
                        var vv = "";
                        for (int i = 0; i < b.Length; i++)
                            vv += b[i].ToString("X2");
                        result2.Add($"{v.Key.Item3}:{v.Key.Item1:X2}:{v.Key.Item2:X2}:{vv}");
                    }
                }
                File.WriteAllLines("templates.txt", result2.OrderBy(f => f).ToArray());
                MessageBox.Show("Templates dumped");

                layoutSystem.BindControl(listBoxObjects);
            }
        }

        private void objectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ObjectEntry.AlternateFormat)
            {
                ObjectEntry.AlternateFormat = false;
                Program.MainForm.UpdateLayoutEditorMenus();
            }
        }

        private void object0000ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!ObjectEntry.AlternateFormat)
            {
                ObjectEntry.AlternateFormat = true;
                Program.MainForm.UpdateLayoutEditorMenus();
            }
        }

        private void useDebugNamesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ObjectEntry.UseDebugNames = !ObjectEntry.UseDebugNames;
            Program.MainForm.UpdateLayoutEditorMenus();
        }

        public void UpdateMenus()
        {
            SetObjectFormatToolStripItems();
            if (ComboBoxObject.Items.Count > 0)
                UpdateObjectComboBox();
            UpdateList();
        }

        public List<string> GetObjectsForModels() => layoutSystem.GetObjectsForModels();

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool UnsavedChanges
        {
            get => layoutSystem.UnsavedChanges;
            set => layoutSystem.UnsavedChanges = value;
        }

        public bool RenderObjects => checkBoxDrawObjs.Checked;

        private Dictionary<(byte, byte), List<Template>> templates = new Dictionary<(byte, byte), List<Template>>();

        private void loadTemplatesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            templates.Clear();
            var tLines = File.ReadAllLines("templates.txt");
            foreach (var line in tLines.OrderBy(l => Convert.ToInt32(l.Split(":")[0].Split('_')[0].Trim('s', 't', 'g'))))
            {
                var split = line.Split(":");
                var list = Convert.ToByte(split[1], 16);
                var type = Convert.ToByte(split[2], 16);

                var temp = LayoutEditorFunctions.CreateHeroesObject(list, type, new Vector3(), new Vector3(), 0, 0, null, false);

                var miscList = new List<byte>();
                for (int i = 0; i < split[3].Length; i += 2)
                {
                    var b = split[3][i..(i + 2)];
                    miscList.Add(Convert.ToByte(b, 16));
                }
                temp.SetMiscSettings(miscList.ToArray());
                var items1 = new List<string>();
                var items2 = new List<string>();
                foreach (var (property, _) in temp.MiscProperties)
                {
                    items1.Add($"{property.Name}: {property.GetValue(temp)}");
                    items2.Add($"{property.GetValue(temp)}");
                }
                var t = new Template()
                {
                    Name = string.Join(", ", items2),
                    Text = string.Join("\n", items1),
                    MiscSettings = miscList.ToArray()
                };
                t.SetLevel(split[0].Split('_')[0]);
                t.SetTeam(split[0].Split('_')[1]);

                if (!templates.ContainsKey((list, type)))
                    templates[(list, type)] = new List<Template>();
                templates[(list, type)].Add(t);
            }

            foreach (var (key, value) in templates)
                templates[key] = value.OrderBy(t => t.Stage).ToList();

            if (listBoxObjects.SelectedIndex > 0)
                buttonGetTemplate.Enabled = templates.ContainsKey((layoutSystem.GetSetObjectAt(listBoxObjects.SelectedIndex).List, layoutSystem.GetSetObjectAt(listBoxObjects.SelectedIndex).Type));
        }

        private void buttonGetTemplate_Click(object sender, EventArgs e)
        {
            var setObj = layoutSystem.GetSetObjectAt(listBoxObjects.SelectedIndex);
            PickTemplate.GetTarget(templates[(setObj.List, setObj.Type)], out bool success, out Template template);
            if (success && template != null)
            {
                var selObject = layoutSystem.GetSetObjectAt(listBoxObjects.SelectedIndex);
                selObject.SetMiscSettings(template.MiscSettings);
                PropertyGridMisc.Refresh();
            }
        }

        public bool HasSelectedObject() => layoutSystem.HasSelectedObject();
    }
}
