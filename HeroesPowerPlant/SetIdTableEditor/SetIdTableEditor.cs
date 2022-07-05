using HeroesPowerPlant.LayoutEditor;
using HeroesPowerPlant.Shared.IO.Config;
using Ookii.Dialogs.WinForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using static HeroesPowerPlant.SetIdTableEditor.SetIdTableFunctions;

namespace HeroesPowerPlant.SetIdTableEditor
{
    public partial class SetIdTableEditor : Form
    {
        public SetIdTableEditor()
        {
            InitializeComponent();
            heroesStageEntries = ReadStageListData(Application.StartupPath + "/Resources\\Lists\\HeroesStageList.ini");
            shadowStageEntries = ReadStageListData(Application.StartupPath + "/Resources\\Lists\\ShadowStageList.ini");

            if (HPPConfig.GetInstance().LegacyWindowPriorityBehavior)
                TopMost = true;
            else
                TopMost = false;
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.WindowsShutDown) return;
            if (e.CloseReason == CloseReason.FormOwnerClosing) return;

            e.Cancel = true;
            Hide();
        }

        private Dictionary<(byte, byte), ObjectEntry> heroesObjectEntries => LayoutEditorSystem.heroesObjectEntries;
        private Dictionary<(byte, byte), ObjectEntry> shadowObjectEntries => LayoutEditorSystem.shadowObjectEntries;
        private StageEntry[] heroesStageEntries;
        private StageEntry[] shadowStageEntries;

        private bool isShadow = false;
        private bool programIsChangingStuff = false;
        private string currentFileName;

        private void heroesSetidtblbinToolStripMenuItem_Click(object sender, EventArgs e)
        {
            VistaOpenFileDialog openFileDialog = new VistaOpenFileDialog()
            {
                Filter = ".bin files|*.bin",
                FileName = "setidtbl.bin"
            };
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                currentFileName = openFileDialog.FileName;
                SetHeroesMode();

                comboBoxTableEntries.Items.Clear();
                foreach (TableEntry t in LoadTable(currentFileName, isShadow, heroesObjectEntries))
                    comboBoxTableEntries.Items.Add(t);

                UpdateStatusStripLabel();
            }
        }

        private void shadowSetidbinToolStripMenuItem_Click(object sender, EventArgs e)
        {
            VistaOpenFileDialog openFileDialog = new VistaOpenFileDialog()
            {
                Filter = ".bin files|*.bin",
                FileName = "setid.bin"
            };
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                currentFileName = openFileDialog.FileName;
                SetShadowMode();

                comboBoxTableEntries.Items.Clear();
                foreach (TableEntry t in LoadTable(currentFileName, isShadow, shadowObjectEntries))
                    comboBoxTableEntries.Items.Add(t);

                UpdateStatusStripLabel();
            }
        }

        public void New()
        {
            currentFileName = null;
            isShadow = false;
            comboBoxTableEntries.Items.Clear();
            checkedListBoxStageEntries.Items.Clear();
            saveToolStripMenuItem.Enabled = false;
            saveAsToolStripMenuItem.Enabled = false;
            UpdateStatusStripLabel();
        }

        public void OpenExternal(string fileName, bool isShadow)
        {
            if (isShadow)
                SetShadowMode();
            else
                SetHeroesMode();

            currentFileName = fileName;

            comboBoxTableEntries.Items.Clear();
            foreach (TableEntry t in LoadTable(currentFileName, isShadow, isShadow ? shadowObjectEntries : heroesObjectEntries))
                comboBoxTableEntries.Items.Add(t);
        }

        private void SetHeroesMode()
        {
            isShadow = false;

            checkedListBoxStageEntries.Items.Clear();
            foreach (StageEntry o in heroesStageEntries)
                checkedListBoxStageEntries.Items.Add(o);

            comboBoxAutoLevel.Items.Clear();
            foreach (StageEntry o in heroesStageEntries)
                comboBoxAutoLevel.Items.Add(o);

            saveToolStripMenuItem.Enabled = true;
            saveAsToolStripMenuItem.Enabled = true;
        }

        private void SetShadowMode()
        {
            isShadow = true;

            checkedListBoxStageEntries.Items.Clear();
            foreach (StageEntry o in shadowStageEntries)
                checkedListBoxStageEntries.Items.Add(o);

            comboBoxAutoLevel.Items.Clear();
            foreach (StageEntry o in shadowStageEntries)
                comboBoxAutoLevel.Items.Add(o);

            saveToolStripMenuItem.Enabled = true;
            saveAsToolStripMenuItem.Enabled = true;
        }

        public bool GetIsShadow()
        {
            return isShadow;
        }

        public string GetCurrentFileName()
        {
            return currentFileName;
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (currentFileName != null)
                SaveTable(currentFileName, isShadow, comboBoxTableEntries.Items.Cast<TableEntry>().ToList());
            else
                saveAsToolStripMenuItem_Click(sender, e);
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            VistaSaveFileDialog saveFileDialog = new VistaSaveFileDialog
            {
                Filter = ".bin files|*.bin",
                FileName = currentFileName
            };
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                currentFileName = saveFileDialog.FileName;
                UpdateStatusStripLabel();
                SaveTable(currentFileName, isShadow, comboBoxTableEntries.Items.Cast<TableEntry>().ToList());
            }
        }

        private void UpdateStatusStripLabel()
        {
            toolStripStatusLabel1.Text = currentFileName;
        }

        private void comboBoxTableEntries_SelectedIndexChanged(object sender, EventArgs e)
        {
            programIsChangingStuff = true;

            for (int i = 0; i < checkedListBoxStageEntries.Items.Count; i++)
            {
                checkedListBoxStageEntries.SetItemChecked(i,
                    ((comboBoxTableEntries.SelectedItem as TableEntry).values0 & (checkedListBoxStageEntries.Items[i] as StageEntry).flag0) != 0 |
                    ((comboBoxTableEntries.SelectedItem as TableEntry).values1 & (checkedListBoxStageEntries.Items[i] as StageEntry).flag1) != 0 |
                    ((comboBoxTableEntries.SelectedItem as TableEntry).values2 & (checkedListBoxStageEntries.Items[i] as StageEntry).flag2) != 0);
            }

            programIsChangingStuff = false;
        }

        private void checkedListBoxStageEntries_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (!programIsChangingStuff & comboBoxTableEntries.SelectedIndex > -1)
            {
                if (e.NewValue == CheckState.Checked)
                {
                    (comboBoxTableEntries.SelectedItem as TableEntry).values0 |= (checkedListBoxStageEntries.Items[e.Index] as StageEntry).flag0;
                    (comboBoxTableEntries.SelectedItem as TableEntry).values1 |= (checkedListBoxStageEntries.Items[e.Index] as StageEntry).flag1;
                    (comboBoxTableEntries.SelectedItem as TableEntry).values2 |= (checkedListBoxStageEntries.Items[e.Index] as StageEntry).flag2;
                }
                else
                {
                    (comboBoxTableEntries.SelectedItem as TableEntry).values0 ^= (checkedListBoxStageEntries.Items[e.Index] as StageEntry).flag0;
                    (comboBoxTableEntries.SelectedItem as TableEntry).values1 ^= (checkedListBoxStageEntries.Items[e.Index] as StageEntry).flag1;
                    (comboBoxTableEntries.SelectedItem as TableEntry).values2 ^= (checkedListBoxStageEntries.Items[e.Index] as StageEntry).flag2;
                }
            }
        }

        private void buttonPerformAutoLevel_Click(object sender, EventArgs e)
        {
            if (comboBoxAutoLevel.SelectedItem != null)
            {
                for (int i = 0; i < comboBoxTableEntries.Items.Count; i++)
                    foreach (var v in Program.MainForm.LayoutEditors)
                        foreach ((byte, byte) o in v.GetAllCurrentObjectEntries())
                            if ((comboBoxTableEntries.Items[i] as TableEntry).objectEntry.List == o.Item1 & (comboBoxTableEntries.Items[i] as TableEntry).objectEntry.Type == o.Item2)
                            {
                                (comboBoxTableEntries.Items[i] as TableEntry).values0 |= (comboBoxAutoLevel.SelectedItem as StageEntry).flag0;
                                (comboBoxTableEntries.Items[i] as TableEntry).values1 |= (comboBoxAutoLevel.SelectedItem as StageEntry).flag1;
                                (comboBoxTableEntries.Items[i] as TableEntry).values2 |= (comboBoxAutoLevel.SelectedItem as StageEntry).flag2;
                            }

                if (comboBoxTableEntries.SelectedItem != null)
                    comboBoxTableEntries_SelectedIndexChanged(null, null);
            }

        }
    }
}
