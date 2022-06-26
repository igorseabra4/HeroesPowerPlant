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
using HeroesPowerPlant.Shared.IO.Config;
using SharpDX;
using static HeroesPowerPlant.ReadWriteCommon;

namespace HeroesPowerPlant.ParticleEditor
{
    public partial class ParticleMenu : Form
    {
        private ParticleEditor ParticleEditor;

        /*
            ------------
            Constructors
            ------------
        */

        public ParticleMenu()
        {
            InitializeComponent();
            ParticleEditor = new ParticleEditor();
            UpdateValues();
        }

        /*
            -------
            Methods
            -------
        */

        public void UpdateValues()
        {
            if (String.IsNullOrEmpty(ParticleEditor.CurrentlyOpenParticleFile))
                toolStripStatusLabel1.Text = "No file loaded";
            else
                toolStripStatusLabel1.Text = ParticleEditor.CurrentlyOpenParticleFile;

            numericCurrentParticle.Minimum = ParticleEditor.Particles.Count == 0 ? -1 : 0;
            numericCurrentParticle.Maximum = ParticleEditor.Particles.Count == 0 ? -1 : ParticleEditor.Particles.Count - 1;
        }

        public void OpenFile(string fileName)
        {
            ParticleEditor = new ParticleEditor(fileName);
            UpdateValues();
        }

        private void SaveParticleFile(string fileName)
        {
            ParticleEditor.Save(fileName);
            UpdateValues();
        }

        public string GetCurrentlyOpenParticleFile()
        {
            return ParticleEditor.CurrentlyOpenParticleFile;
        }

        public Vector3 GetBoxForSetParticle(int index)
        {
            return ParticleEditor.GetBoxForSetParticle(index);
        }

        /*
            -------
            Methods
            -------
        */

        private void ParticleEditor_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.WindowsShutDown)  return;
            if (e.CloseReason == CloseReason.FormOwnerClosing) return;

            e.Cancel = true;
            Hide();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            New();
        }

        public void New()
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
                OpenFile(openFile.FileName);
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(ParticleEditor.CurrentlyOpenParticleFile))
                saveAsToolStripMenuItem_Click(sender, e);
            else
                ParticleEditor.Save(ParticleEditor.CurrentlyOpenParticleFile);
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFile = new SaveFileDialog()
            {
                Filter = "BIN Files|*.bin",
                FileName = ParticleEditor.CurrentlyOpenParticleFile
            };

            if (saveFile.ShowDialog() == DialogResult.OK)
            {
                SaveParticleFile(saveFile.FileName);
                UpdateValues();
            }
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
            else
                propertyGridParticles.SelectedObject = null;
        }

        private void propertyGridParticles_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            int index = (int)numericCurrentParticle.Value;

            if (index >= 0 & index < ParticleEditor.Particles.Count)
                ParticleEditor.Particles[index] = (Particle)propertyGridParticles.SelectedObject;

            foreach (var v in Program.MainForm.LayoutEditors)
                v.UpdateSetParticleMatrices();
        }

        private void ParticleMenu_Load(object sender, EventArgs e)
        {
            if (HPPConfig.GetInstance().LegacyWindowPriorityBehavior)
                TopMost = true;
            else
                TopMost = false;
        }
    }
}
