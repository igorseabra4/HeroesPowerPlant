using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using static HeroesPowerPlant.TexturePatternEditor.TexturePatternEditorFunctions;

namespace HeroesPowerPlant.TexturePatternEditor
{
    public partial class TexturePatternEditor : Form
    {
        public TexturePatternEditor()
        {
            InitializeComponent();
        }
        
        private void PatternEditor_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.WindowsShutDown) return;
            if (e.CloseReason == CloseReason.FormOwnerClosing) return;

            e.Cancel = true;
            Hide();
        }

        private string CurrentlyOpenTXC;
        private bool programIsChangingStuff = false;

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CurrentlyOpenTXC = null;
            toolStripStatusLabel1.Text = "No file loaded";
            listBoxPatterns.Items.Clear();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog()
            {
                Filter = "TXC Files|*.txc"
            };

            if (openFile.ShowDialog() == DialogResult.OK)
                OpenTXC(openFile.FileName);
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CurrentlyOpenTXC != null)
                SaveTXC(listBoxPatterns.Items.Cast<PatternEntry>(), CurrentlyOpenTXC);
            else
                saveAsToolStripMenuItem_Click(sender, e);
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFile = new SaveFileDialog()
            {
                Filter = "TXC Files|*.txc",
                FileName = CurrentlyOpenTXC
            };

            if (saveFile.ShowDialog() == DialogResult.OK)
            {
                CurrentlyOpenTXC = saveFile.FileName;
                toolStripStatusLabel1.Text = CurrentlyOpenTXC;
                SaveTXC(listBoxPatterns.Items.Cast<PatternEntry>(), CurrentlyOpenTXC);
            }
        }

        public void OpenTXC(string fileName)
        {
            CurrentlyOpenTXC = fileName;

            listBoxPatterns.Items.Clear();
            foreach (PatternEntry p in GetPatternEntriesFromFile(CurrentlyOpenTXC))
                listBoxPatterns.Items.Add(p);

            toolStripStatusLabel1.Text = CurrentlyOpenTXC;
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            listBoxPatterns.Items.Add(new PatternEntry());
        }

        private void buttonCopy_Click(object sender, EventArgs e)
        {
            if (listBoxPatterns.SelectedItem != null)
                listBoxPatterns.Items.Add(new PatternEntry((PatternEntry)listBoxPatterns.SelectedItem));
        }

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            if (listBoxPatterns.SelectedItem != null)
                listBoxPatterns.Items.Remove(listBoxPatterns.SelectedItem);
        }

        private void listBoxPatterns_SelectedIndexChanged(object sender, EventArgs e)
        {
            programIsChangingStuff = true;

            PatternEntry selected = (PatternEntry)listBoxPatterns.SelectedItem;

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

            if (listBoxPatterns.SelectedItem != null)
                ((PatternEntry)listBoxPatterns.SelectedItem).TextureName = textBoxTextureName.Text;
        }

        private void textBoxAnimationName_TextChanged(object sender, EventArgs e)
        {
            if (programIsChangingStuff) return;

            if (listBoxPatterns.SelectedItem != null)
                ((PatternEntry)listBoxPatterns.SelectedItem).AnimationName = textBoxAnimationName.Text;
        }

        private void numericFrameCount_ValueChanged(object sender, EventArgs e)
        {
            if (programIsChangingStuff) return;

            if (listBoxPatterns.SelectedItem != null)
                ((PatternEntry)listBoxPatterns.SelectedItem).FrameCount = (uint)numericFrameCount.Value;
        }

        private void listBoxFrames_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (programIsChangingStuff) return;
            
            if (listBoxFrames.SelectedItem != null)
            {
                PatternEntry p = (PatternEntry)listBoxPatterns.SelectedItem;
                Frame f = p.frames[listBoxFrames.SelectedIndex];

                numericFrameOffset.Value = f.FrameOffset;
                numericTextureNumber.Value = f.TextureNumber;
            }
        }

        private void buttonAddFrame_Click(object sender, EventArgs e)
        {
            if (listBoxPatterns.SelectedItem != null)
            {
                Frame f = new Frame();
                ((PatternEntry)listBoxPatterns.SelectedItem).frames.Add(f);
                listBoxFrames.Items.Add(f.ToString());
            }
        }

        private void buttonRemoveFrame_Click(object sender, EventArgs e)
        {
            if (listBoxPatterns.SelectedItem != null)
            {
                if (listBoxFrames.SelectedItem != null)
                {
                    int index = listBoxFrames.SelectedIndex;
                    ((PatternEntry)listBoxPatterns.SelectedItem).frames.RemoveAt(index);
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
                    Frame f = ((PatternEntry)listBoxPatterns.SelectedItem).frames[listBoxFrames.SelectedIndex];
                    f.FrameOffset = (ushort)numericFrameOffset.Value;
                    ((PatternEntry)listBoxPatterns.SelectedItem).frames[listBoxFrames.SelectedIndex] = f;

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
                    Frame f = ((PatternEntry)listBoxPatterns.SelectedItem).frames[listBoxFrames.SelectedIndex];
                    f.TextureNumber = (ushort)numericTextureNumber.Value;
                    ((PatternEntry)listBoxPatterns.SelectedItem).frames[listBoxFrames.SelectedIndex] = f;

                    programIsChangingStuff = true;
                    listBoxFrames.Items[listBoxFrames.SelectedIndex] = f.ToString();
                    programIsChangingStuff = false;
                }
        }
    }
}
