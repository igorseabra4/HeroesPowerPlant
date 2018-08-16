using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using SharpDX;
using static HeroesPowerPlant.ReadWriteCommon;

namespace HeroesPowerPlant.ParticleEditor
{
    public partial class ParticleEditor : Form
    {
        public ParticleEditor()
        {
            InitializeComponent();
            particleEntries = new List<ParticleEntry>();
        }

        private void ParticleEditor_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.WindowsShutDown) return;
            if (e.CloseReason == CloseReason.FormOwnerClosing) return;

            e.Cancel = true;
            Hide();
        }

        private List<ParticleEntry> particleEntries;
        private string CurrentlyOpenParticleFile;

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CurrentlyOpenParticleFile = null;
            toolStripStatusLabel1.Text = "No file loaded";
            particleEntries = new List<ParticleEntry>();
            numericCurrentParticle.Maximum = 0;

            Program.LayoutEditor.UpdateSetParticleMatrices();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog()
            {
                Filter = "BIN Files|*.bin"
            };

            if (openFile.ShowDialog() == DialogResult.OK)
                OpenParticleFile(openFile.FileName);
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CurrentlyOpenParticleFile != null)
                SaveParticleFile(particleEntries, CurrentlyOpenParticleFile);
            else
                saveAsToolStripMenuItem_Click(sender, e);
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFile = new SaveFileDialog()
            {
                Filter = "BIN Files|*.bin",
                FileName = CurrentlyOpenParticleFile
            };

            if (saveFile.ShowDialog() == DialogResult.OK)
                SaveParticleFile(particleEntries, saveFile.FileName);
        }

        public void OpenParticleFile(string fileName)
        {
            CurrentlyOpenParticleFile = fileName;

            particleEntries = new List<ParticleEntry>();
            particleEntries.AddRange(GetParticleEntriesFromFile(CurrentlyOpenParticleFile));

            numericCurrentParticle.Maximum = particleEntries.Count > 0 ? particleEntries.Count - 1 : 0;
            numericCurrentParticle.Value = 0;
            toolStripStatusLabel1.Text = CurrentlyOpenParticleFile;

            Program.LayoutEditor.UpdateSetParticleMatrices();
        }

        private IEnumerable<ParticleEntry> GetParticleEntriesFromFile(string fileName)
        {
            BinaryReader particleReader = new BinaryReader(new FileStream(fileName, FileMode.Open));
            List<ParticleEntry> particles = new List<ParticleEntry>();

            while (particleReader.BaseStream.Position < particleReader.BaseStream.Length)
            {
                particles.Add(new ParticleEntry
                {
                    SpreadUVType = particleReader.ReadByte(),
                    UvFrameType = particleReader.ReadByte(),
                    Unknown1 = particleReader.ReadByte(),
                    Unknown2 = particleReader.ReadByte(),
                    Unknown3 = Switch(particleReader.ReadInt32()),
                    ColorR = particleReader.ReadByte(),
                    ColorG = particleReader.ReadByte(),
                    ColorB = particleReader.ReadByte(),
                    ColorA = particleReader.ReadByte(),
                    FadeTime = Switch(particleReader.ReadInt32()),
                    BirthDelay = Switch(particleReader.ReadInt16()),
                    AmountOfParticles = Switch(particleReader.ReadInt16()),
                    Unknown4 = Switch(particleReader.ReadInt16()),
                    Unknown5 = Switch(particleReader.ReadInt16()),
                    Unknown6 = Switch(particleReader.ReadInt16()),
                    Unknown7 = Switch(particleReader.ReadInt16()),
                    Velocity = Switch(particleReader.ReadInt16()),
                    Unknown8 = Switch(particleReader.ReadInt16()),

                    Always05 = Switch(particleReader.ReadInt16()),
                    BlendMode = Switch(particleReader.ReadInt16()),
                    Rotation = Switch(particleReader.ReadInt32()),
                    RotateAnimationSpeed = Switch(particleReader.ReadInt16()),
                    RotateAnimation = Switch(particleReader.ReadInt16()),
                    InverseLifeTime = Switch(particleReader.ReadSingle()),
                    LifeThreshold = Switch(particleReader.ReadSingle()),
                    EmitterScaleX = Switch(particleReader.ReadSingle()),
                    EmitterScaleY = Switch(particleReader.ReadSingle()),
                    EmitterScaleZ = Switch(particleReader.ReadSingle()),

                    SpreadSpeedRate = Switch(particleReader.ReadSingle()),
                    VelocityRate = Switch(particleReader.ReadSingle()),
                    Unknown9 = Switch(particleReader.ReadSingle()),
                    Unknown10 = Switch(particleReader.ReadSingle()),
                    Unknown11 = Switch(particleReader.ReadSingle()),
                    Unknown12 = Switch(particleReader.ReadSingle()),
                    ParticleSize = Switch(particleReader.ReadSingle()),
                    Unknown13 = Switch(particleReader.ReadSingle()),

                    SpreadSize = Switch(particleReader.ReadSingle()),
                    SameAsAbove = Switch(particleReader.ReadSingle()),
                    TextureName = new string(particleReader.ReadChars(0x18)).Trim('\0')
                });
            }

            particleReader.Close();
            return particles;
        }

        private void SaveParticleFile(List<ParticleEntry> particleEntries, string fileName)
        {
            BinaryWriter particleWriter = new BinaryWriter(new FileStream(fileName, FileMode.Create));

            foreach (ParticleEntry p in particleEntries)
            {
                particleWriter.Write(p.SpreadUVType);
                particleWriter.Write(p.UvFrameType);
                particleWriter.Write(p.Unknown1);
                particleWriter.Write(p.Unknown2);
                particleWriter.Write(Switch(p.Unknown3));
                particleWriter.Write(p.ColorR);
                particleWriter.Write(p.ColorG);
                particleWriter.Write(p.ColorB);
                particleWriter.Write(p.ColorA);
                particleWriter.Write(Switch(p.FadeTime));
                particleWriter.Write(Switch(p.BirthDelay));
                particleWriter.Write(Switch(p.AmountOfParticles));
                particleWriter.Write(Switch(p.Unknown4));
                particleWriter.Write(Switch(p.Unknown5));
                particleWriter.Write(Switch(p.Unknown6));
                particleWriter.Write(Switch(p.Unknown7));
                particleWriter.Write(Switch(p.Velocity));
                particleWriter.Write(Switch(p.Unknown8));

                particleWriter.Write(Switch(p.Always05));
                particleWriter.Write(Switch(p.BlendMode));
                particleWriter.Write(Switch(p.Rotation));
                particleWriter.Write(Switch(p.RotateAnimationSpeed));
                particleWriter.Write(Switch(p.RotateAnimation));
                particleWriter.Write(Switch(p.InverseLifeTime));
                particleWriter.Write(Switch(p.LifeThreshold));
                particleWriter.Write(Switch(p.EmitterScaleX));
                particleWriter.Write(Switch(p.EmitterScaleY));
                particleWriter.Write(Switch(p.EmitterScaleZ));

                particleWriter.Write(Switch(p.SpreadSpeedRate));
                particleWriter.Write(Switch(p.VelocityRate));
                particleWriter.Write(Switch(p.Unknown9));
                particleWriter.Write(Switch(p.Unknown10));
                particleWriter.Write(Switch(p.Unknown11));
                particleWriter.Write(Switch(p.Unknown12));
                particleWriter.Write(Switch(p.ParticleSize));
                particleWriter.Write(Switch(p.Unknown13));

                particleWriter.Write(Switch(p.SpreadSize));
                particleWriter.Write(Switch(p.SameAsAbove));

                foreach (char c in p.TextureName.Length > 0x18 ? p.TextureName.Substring(0, 0x18) : p.TextureName)
                    particleWriter.Write(c);

                for (int i = p.TextureName.Length; i < 0x18; i++)
                    particleWriter.Write((byte)0);
            }

            particleWriter.Close();
            CurrentlyOpenParticleFile = fileName;
            toolStripStatusLabel1.Text = CurrentlyOpenParticleFile;
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            particleEntries.Add(new ParticleEntry());
            numericCurrentParticle.Maximum = particleEntries.Count > 0 ? particleEntries.Count - 1 : 0;
        }

        private void buttonCopy_Click(object sender, EventArgs e)
        {
            int index = (int)numericCurrentParticle.Value;
            if (index >= 0 & index < particleEntries.Count)
            {
                particleEntries.Add(ParticleEntry.FromParticleEntry(particleEntries[index]));
                numericCurrentParticle.Maximum = particleEntries.Count > 0 ? particleEntries.Count - 1 : 0;
            }
        }

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            int index = (int)numericCurrentParticle.Value;
            if (index >= 0 & index < particleEntries.Count)
            {
                particleEntries.RemoveAt(index);
                numericCurrentParticle.Maximum = particleEntries.Count > 0 ? particleEntries.Count - 1 : 0;
            }
            Program.LayoutEditor.UpdateSetParticleMatrices();
        }

        private void numericCurrentParticle_ValueChanged(object sender, EventArgs e)
        {
            int index = (int)numericCurrentParticle.Value;
            if (index >= 0 & index < particleEntries.Count)
                propertyGridParticles.SelectedObject = particleEntries[index];
        }
        
        public Vector3 GetBoxForSetParticle(int index)
        {
            if (index < particleEntries.Count)
                return new Vector3(particleEntries[index].EmitterScaleX, particleEntries[index].EmitterScaleY, particleEntries[index].EmitterScaleZ);
            else
                return Vector3.Zero;
        }

        private void propertyGridParticles_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            Program.LayoutEditor.UpdateSetParticleMatrices();
        }
    }
}
