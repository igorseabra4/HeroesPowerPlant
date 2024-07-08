using HeroesPowerPlant.Shared.IO.Config;
using Ookii.Dialogs.WinForms;
using SharpDX;
using System;
using System.Windows.Forms;

namespace HeroesPowerPlant.ParticleEditor
{
    public partial class ParticleMenu : Form, IUnsavedChanges
    {
        private ParticleEditor ParticleEditor;

        public bool UnsavedChanges
        {
            get => ParticleEditor.UnsavedChanges;
            set => ParticleEditor.UnsavedChanges = value;
        }

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
            UnsavedChanges = false;
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
            if (e.CloseReason == CloseReason.WindowsShutDown)
                return;
            if (e.CloseReason == CloseReason.FormOwnerClosing)
                return;

            e.Cancel = true;
            Hide();
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
            ParticleEditor = new ParticleEditor();
            UpdateValues();
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
                Filter = "BIN Files|*.bin"
            };
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                OpenFile(openFile.FileName);
            }
        }

        public void Save()
        {
            saveToolStripMenuItem_Click(null, null);
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
            VistaSaveFileDialog saveFile = new VistaSaveFileDialog()
            {
                Filter = "BIN Files|*.bin",
                DefaultExt = ".bin",
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
            UnsavedChanges = true;
        }

        private void buttonCopy_Click(object sender, EventArgs e)
        {
            int index = (int)numericCurrentParticle.Value;
            if (index >= 0 & index < ParticleEditor.Particles.Count)
            {
                var particleToClone = Particle.FromParticleEntry(ParticleEditor.Particles[index]);
                ParticleEditor.Particles.Add(particleToClone);
                UpdateValues();
                UnsavedChanges = true;
            }
        }

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            int index = (int)numericCurrentParticle.Value;
            if (index >= 0 & index < ParticleEditor.Particles.Count)
            {
                ParticleEditor.Particles.RemoveAt(index);
                UpdateValues();
                UnsavedChanges = true;
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
            {
                ParticleEditor.Particles[index] = (Particle)propertyGridParticles.SelectedObject;
                UnsavedChanges = true;
            }

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
