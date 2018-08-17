using System;
using System.Windows.Forms;

namespace HeroesPowerPlant.TexturePatternEditor
{
    public partial class TexturePatternEditor : Form
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
            if (e.CloseReason == CloseReason.WindowsShutDown) return;
            if (e.CloseReason == CloseReason.FormOwnerClosing) return;

            e.Cancel = true;
            Hide();
        }
        
        private bool programIsChangingStuff = false;

        private PatternSystem patternSystem;

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "No file loaded";
            patternSystem = new PatternSystem();
            listBoxPatterns.Items.Clear();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog()
            {
                Filter = "TXC Files|*.txc"
            };

            if (openFile.ShowDialog() == DialogResult.OK)
                OpenFile(openFile.FileName);
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (patternSystem.CurrentlyOpenTXC != null)
                patternSystem.Save(patternSystem.CurrentlyOpenTXC);
            else
                saveAsToolStripMenuItem_Click(sender, e);
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFile = new SaveFileDialog()
            {
                Filter = "TXC Files|*.txc",
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

            toolStripStatusLabel1.Text = patternSystem.CurrentlyOpenTXC;
        }

        public string GetCurrentlyOpenTXC()
        {
            return patternSystem.CurrentlyOpenTXC;
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            listBoxPatterns.Items.Add(patternSystem.Add());
        }

        private void buttonCopy_Click(object sender, EventArgs e)
        {
            if (listBoxPatterns.SelectedItem != null)
                listBoxPatterns.Items.Add(patternSystem.Add(listBoxPatterns.SelectedIndex));
        }

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            if (listBoxPatterns.SelectedItem != null)
                listBoxPatterns.Items.RemoveAt(patternSystem.Remove(listBoxPatterns.SelectedIndex));
        }

        private void listBoxPatterns_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (programIsChangingStuff)
                return;

            programIsChangingStuff = true;

            PatternEntry selected = patternSystem.GetPatternAt(listBoxPatterns.SelectedIndex);

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
            if (programIsChangingStuff) return;

            // ugly code
            if (listBoxPatterns.SelectedItem != null)
            {
                programIsChangingStuff = true;
                patternSystem.GetPatternAt(listBoxPatterns.SelectedIndex).TextureName = textBoxTextureName.Text;
                listBoxPatterns.Items[listBoxPatterns.SelectedIndex] = patternSystem.GetPatternAt(listBoxPatterns.SelectedIndex).ToString();
                programIsChangingStuff = false;
            }
        }

        private void textBoxAnimationName_TextChanged(object sender, EventArgs e)
        {
            if (programIsChangingStuff) return;

            // ugly code
            if (listBoxPatterns.SelectedItem != null)
                patternSystem.GetPatternAt(listBoxPatterns.SelectedIndex).AnimationName = textBoxAnimationName.Text;
        }

        private void numericFrameCount_ValueChanged(object sender, EventArgs e)
        {
            if (programIsChangingStuff) return;

            // ugly code
            if (listBoxPatterns.SelectedItem != null)
            {
                programIsChangingStuff = true;
                patternSystem.GetPatternAt(listBoxPatterns.SelectedIndex).FrameCount = (uint)numericFrameCount.Value;
                listBoxPatterns.Items[listBoxPatterns.SelectedIndex] = patternSystem.GetPatternAt(listBoxPatterns.SelectedIndex).ToString();
                programIsChangingStuff = false;
            }
        }

        private void listBoxFrames_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (programIsChangingStuff) return;
            
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
                }
            }
        }

        private void numericFrameOffset_ValueChanged(object sender, EventArgs e)
        {
            if (programIsChangingStuff) return;

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
                }
        }

        private void numericTextureNumber_ValueChanged(object sender, EventArgs e)
        {
            if (programIsChangingStuff) return;

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
                }
        }

        public void Animate()
        {
            if (Play)
                patternSystem.Animate();
        }

        private bool Play = false;

        private void buttonPlay_Click(object sender, EventArgs e)
        {
            Play = true;
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            Play = false;
            patternSystem.StopAnimation();
        }
    }
}
