using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SharpDX;
using static HeroesPowerPlant.ReadWriteCommon;

namespace HeroesPowerPlant.ParticleEditor
{
    public partial class ParticleMenu : Form
    {
        private ParticleEditor ParticleEditor;
        private string currentlyOpenParticleFile;
        
        public ParticleMenu()
        {
            InitializeComponent();
            ParticleEditor = new ParticleEditor();
        }
        
        public void UpdateValues()
        {
            if (String.IsNullOrEmpty(currentlyOpenParticleFile))
                toolStripStatusLabel1.Text = "No file loaded";
            else
                toolStripStatusLabel1.Text = currentlyOpenParticleFile;

            numericCurrentParticle.Maximum = ParticleEditor.Particles.Count > 0 ? ParticleEditor.Particles.Count - 1 : 0;
        }

        private void SaveParticleFile(string fileName)
        {
            ParticleEditor.Save(fileName);
            currentlyOpenParticleFile = fileName;
            UpdateValues();
        }

        public Vector3 GetBoxForSetParticle(int index)
        {
            return ParticleEditor.GetBoxForSetParticle(index);
        }

        private void ParticleEditor_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.WindowsShutDown)  return;
            if (e.CloseReason == CloseReason.FormOwnerClosing) return;

            e.Cancel = true;
            Hide();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ParticleEditor = new ParticleEditor();
            UpdateValues();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog()
            {
                Filter = "BIN Files|*.bin"
            };

            if (openFile.ShowDialog() == DialogResult.OK)
            {
                currentlyOpenParticleFile = openFile.FileName;
                ParticleEditor = new ParticleEditor(openFile.FileName);

                numericCurrentParticle.Maximum = ParticleEditor.Particles.Count > 0 ? ParticleEditor.Particles.Count - 1 : 0;
                numericCurrentParticle.Value   = 0;

                toolStripStatusLabel1.Text = currentlyOpenParticleFile;
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (currentlyOpenParticleFile != null)
                SaveParticleFile(currentlyOpenParticleFile);
            else
                saveAsToolStripMenuItem_Click(sender, e);
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFile = new SaveFileDialog()
            {
                Filter = "BIN Files|*.bin",
                FileName = currentlyOpenParticleFile
            };

            if (saveFile.ShowDialog() == DialogResult.OK)
                SaveParticleFile(saveFile.FileName);
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            ParticleEditor.Particles.Add(new Particle());
            UpdateValues();
        }

        private void buttonCopy_Click(object sender, EventArgs e)
        {
            int index = (int)numericCurrentParticle.Value;
            if (index >= 0 & index < ParticleEditor.Particles.Count)
            {
                var particleToClone = Particle.FromParticleEntry(ParticleEditor.Particles[index]);
                ParticleEditor.Particles.Add(particleToClone);
                UpdateValues();
            }
        }

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            int index = (int)numericCurrentParticle.Value;
            if (index >= 0 & index < ParticleEditor.Particles.Count)
            {
                ParticleEditor.Particles.RemoveAt(index);
                UpdateValues();
            }
        }

        private void numericCurrentParticle_ValueChanged(object sender, EventArgs e)
        {
            int index = (int)numericCurrentParticle.Value;
            if (index >= 0 & index < ParticleEditor.Particles.Count)
                propertyGridParticles.SelectedObject = ParticleEditor.Particles[index];
        }
    }
}
