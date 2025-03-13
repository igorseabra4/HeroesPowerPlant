using HeroesPowerPlant.Shared.IO.Config;
using Ookii.Dialogs.WinForms;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace HeroesPowerPlant.TexturePatternEditor
{
    public partial class TexturePatternEditor : Form, IUnsavedChanges
    {
        public TexturePatternEditor()
        {
            InitializeComponent();
            numericFrameCount.Maximum = decimal.MaxValue;
            numericFrameOffset.Maximum = decimal.MaxValue;
            numericTextureNumber.Maximum = decimal.MaxValue;

            patternSystem = new PatternSystem();
        }

        private void PatternEditor_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.WindowsShutDown)
                return;
            if (e.CloseReason == CloseReason.FormOwnerClosing)
                return;

            e.Cancel = true;
            Hide();
        }

        private bool programIsChangingStuff = false;

        private PatternSystem patternSystem;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool UnsavedChanges
        {
            get => patternSystem.UnsavedChanges;
            set => patternSystem.UnsavedChanges = value;
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

        public void New()
        {
            toolStripStatusLabel1.Text = "No file loaded";
            patternSystem = new PatternSystem();
            listBoxPatterns.Items.Clear();
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

            VistaOpenFileDialog openFile = new VistaOpenFileDialog()
            {
                Filter = "TXC Files|*.txc"
            };

            if (openFile.ShowDialog() == DialogResult.OK)
                OpenFile(openFile.FileName);
        }

        public void Save()
        {
            saveToolStripMenuItem_Click(null, null);
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(patternSystem.CurrentlyOpenTXC))
                saveAsToolStripMenuItem_Click(sender, e);
            else
                patternSystem.Save();
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            VistaSaveFileDialog saveFile = new VistaSaveFileDialog()
            {
                Filter = "TXC Files|*.txc",
                DefaultExt = ".txc",
                FileName = patternSystem.CurrentlyOpenTXC
            };

            if (saveFile.ShowDialog() == DialogResult.OK)
            {
                patternSystem.Save(saveFile.FileName);
                toolStripStatusLabel1.Text = patternSystem.CurrentlyOpenTXC;
            }
        }

        public void OpenFile(string fileName)
        {
            patternSystem = new PatternSystem(fileName);

            listBoxPatterns.Items.Clear();
            foreach (string p in patternSystem.GetPatternEntries())
                listBoxPatterns.Items.Add(p);

            listBoxFrames.Items.Clear();

            toolStripStatusLabel1.Text = patternSystem.CurrentlyOpenTXC;
        }

        public string GetCurrentlyOpenTXC()
        {
            return patternSystem.CurrentlyOpenTXC;
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            listBoxPatterns.Items.Add(patternSystem.Add());
            UnsavedChanges = true;
        }

        private void buttonCopy_Click(object sender, EventArgs e)
        {
            if (listBoxPatterns.SelectedItem != null)
            {
                listBoxPatterns.Items.Add(patternSystem.Add(listBoxPatterns.SelectedIndex));
                UnsavedChanges = true;
            }
        }

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            if (listBoxPatterns.SelectedItem != null)
            {
                programIsChangingStuff = true;
                listBoxPatterns.Items.RemoveAt(patternSystem.Remove(listBoxPatterns.SelectedIndex));
                UnsavedChanges = true;
                programIsChangingStuff = false;
            }
        }

        private void listBoxPatterns_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (programIsChangingStuff | listBoxPatterns.SelectedItem == null)
                return;

            programIsChangingStuff = true;

            patternSystem.Deselect();

            PatternEntry selected = patternSystem.GetPatternAt(listBoxPatterns.SelectedIndex);
            selected.isSelected = true;

            textBoxTextureName.Text = selected.TextureName;
            textBoxAnimationName.Text = selected.AnimationName;
            numericFrameCount.Value = selected.FrameCount;

            listBoxFrames.Items.Clear();

            foreach (Frame f in selected.frames)
                listBoxFrames.Items.Add(f.ToString());

            programIsChangingStuff = false;
        }

        private void textBoxTextureName_TextChanged(object sender, EventArgs e)
        {
            if (programIsChangingStuff)
                return;

            // ugly code
            if (listBoxPatterns.SelectedItem != null)
            {
                programIsChangingStuff = true;
                patternSystem.GetPatternAt(listBoxPatterns.SelectedIndex).TextureName = textBoxTextureName.Text;
                listBoxPatterns.Items[listBoxPatterns.SelectedIndex] = patternSystem.GetPatternAt(listBoxPatterns.SelectedIndex).ToString();
                UnsavedChanges = true;
                programIsChangingStuff = false;
            }
        }

        private void textBoxAnimationName_TextChanged(object sender, EventArgs e)
        {
            if (programIsChangingStuff)
                return;

            // ugly code
            if (listBoxPatterns.SelectedItem != null)
            { 
                patternSystem.GetPatternAt(listBoxPatterns.SelectedIndex).AnimationName = textBoxAnimationName.Text;
                UnsavedChanges = true;
            }
        }

        private void numericFrameCount_ValueChanged(object sender, EventArgs e)
        {
            if (programIsChangingStuff)
                return;

            // ugly code
            if (listBoxPatterns.SelectedItem != null)
            {
                programIsChangingStuff = true;
                patternSystem.GetPatternAt(listBoxPatterns.SelectedIndex).FrameCount = (uint)numericFrameCount.Value;
                listBoxPatterns.Items[listBoxPatterns.SelectedIndex] = patternSystem.GetPatternAt(listBoxPatterns.SelectedIndex).ToString();
                UnsavedChanges = true;
                programIsChangingStuff = false;
            }
        }

        private void listBoxFrames_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (programIsChangingStuff | listBoxPatterns.SelectedItem == null | listBoxFrames.SelectedItem == null)
                return;

            if (listBoxFrames.SelectedItem != null)
            {
                programIsChangingStuff = true;

                // ugly code
                PatternEntry p = patternSystem.GetPatternAt(listBoxPatterns.SelectedIndex);
                Frame f = p.frames[listBoxFrames.SelectedIndex];

                numericFrameOffset.Value = f.FrameOffset;
                numericTextureNumber.Value = f.TextureNumber;

                programIsChangingStuff = false;
            }
        }

        private void buttonAddFrame_Click(object sender, EventArgs e)
        {
            if (listBoxPatterns.SelectedItem != null)
            {
                Frame f = new Frame();
                PatternEntry p = patternSystem.GetPatternAt(listBoxPatterns.SelectedIndex);
                p.frames.Add(f);
                listBoxFrames.Items.Add(f.ToString());
                UnsavedChanges = true;
            }
        }

        private void buttonRemoveFrame_Click(object sender, EventArgs e)
        {
            if (listBoxPatterns.SelectedItem != null)
            {
                if (listBoxFrames.SelectedItem != null)
                {
                    PatternEntry p = patternSystem.GetPatternAt(listBoxPatterns.SelectedIndex);
                    int index = listBoxFrames.SelectedIndex;
                    p.frames.RemoveAt(index);
                    listBoxFrames.Items.RemoveAt(index);
                    UnsavedChanges = true;
                }
            }
        }

        private void numericFrameOffset_ValueChanged(object sender, EventArgs e)
        {
            if (programIsChangingStuff)
                return;

            if (listBoxPatterns.SelectedItem != null)
                if (listBoxFrames.SelectedItem != null)
                {
                    PatternEntry p = patternSystem.GetPatternAt(listBoxPatterns.SelectedIndex);

                    Frame f = p.frames[listBoxFrames.SelectedIndex];
                    f.FrameOffset = (ushort)numericFrameOffset.Value;
                    p.frames[listBoxFrames.SelectedIndex] = f;

                    programIsChangingStuff = true;
                    listBoxFrames.Items[listBoxFrames.SelectedIndex] = f.ToString();
                    programIsChangingStuff = false;
                    UnsavedChanges = true;
                }
        }

        private void numericTextureNumber_ValueChanged(object sender, EventArgs e)
        {
            if (programIsChangingStuff)
                return;

            if (listBoxPatterns.SelectedItem != null)
                if (listBoxFrames.SelectedItem != null)
                {
                    PatternEntry p = patternSystem.GetPatternAt(listBoxPatterns.SelectedIndex);

                    Frame f = p.frames[listBoxFrames.SelectedIndex];
                    f.TextureNumber = (ushort)numericTextureNumber.Value;
                    p.frames[listBoxFrames.SelectedIndex] = f;

                    programIsChangingStuff = true;
                    listBoxFrames.Items[listBoxFrames.SelectedIndex] = f.ToString();
                    programIsChangingStuff = false;
                    UnsavedChanges = true;
                }
        }

        public void Animate()
        {
            if (Play)
                patternSystem.Animate(this);
        }

        private bool Play = false;

        private void buttonPlay_Click(object sender, EventArgs e)
        {
            Play = !Play;
            if (Play)
            {
                buttonPlay.Text = "Stop";
            }
            else
            {
                buttonPlay.Text = "Play";
                patternSystem.StopAnimation();
                labelFrame.Text = "Stopped";
            }
        }

        public void SendPlaying(uint counter)
        {
            labelFrame.Text = "Frame: " + counter.ToString();
        }

        private void TexturePatternEditor_Load(object sender, EventArgs e)
        {
            if (HPPConfig.GetInstance().LegacyWindowPriorityBehavior)
                TopMost = true;
            else
                TopMost = false;
        }
    }
}
