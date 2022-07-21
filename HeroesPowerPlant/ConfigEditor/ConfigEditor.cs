using Heroes.SDK.Definitions.Enums;
using HeroesPowerPlant.Shared.IO.Config;
using Ookii.Dialogs.WinForms;
using SharpDX;
using System;
using System.IO;
using System.Windows.Forms;
using Heroes.SDK.Definitions.Structures.Stage.Spawn;


namespace HeroesPowerPlant.ConfigEditor
{
    public partial class ConfigEditor : Form
    {
        public ConfigEditor()
        {
            InitializeComponent();

            NumericStartX.Minimum = decimal.MinValue;
            NumericStartY.Minimum = decimal.MinValue;
            NumericStartZ.Minimum = decimal.MinValue;
            NumericStartHold.Minimum = 0;
            NumericStartX.Maximum = decimal.MaxValue;
            NumericStartY.Maximum = decimal.MaxValue;
            NumericStartZ.Maximum = decimal.MaxValue;
            NumericStartHold.Maximum = 65535;

            NumericEndX.Minimum = decimal.MinValue;
            NumericEndY.Minimum = decimal.MinValue;
            NumericEndZ.Minimum = decimal.MinValue;
            NumericEndX.Maximum = decimal.MaxValue;
            NumericEndY.Maximum = decimal.MaxValue;
            NumericEndZ.Maximum = decimal.MaxValue;

            NumericBragX.Minimum = decimal.MinValue;
            NumericBragY.Minimum = decimal.MinValue;
            NumericBragZ.Minimum = decimal.MinValue;
            NumericBragX.Maximum = decimal.MaxValue;
            NumericBragY.Maximum = decimal.MaxValue;
            NumericBragZ.Maximum = decimal.MaxValue;

            ProgramIsChangingStuff = false;

            foreach (Stage i in Enum.GetValues(typeof(Stage)))
                ComboLevelConfig.Items.Add(i);

            SplineEditor = new SplineEditor.SplineEditor();
            RankEditor = new RankEditor.RankEditor();
            EXEExtractor = new EXEExtractor();

            CleanFile();
        }

        private void LayoutEditor_Load(object sender, EventArgs e)
        {
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

        bool ProgramIsChangingStuff = false;

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            New();
        }

        public void New()
        {
            OpenConfigFileName = null;
            LabelFileLoaded.Text = "No file loaded";
            CleanFile();

            SplineEditor.SplineEditorNewConfig();
            RankEditor.RankEditorNewConfig();
            EnableSplineEditor();
            EnableRankEditor();
            EnableEXEExtractor();
        }

        public SplineEditor.SplineEditor SplineEditor;
        public RankEditor.RankEditor RankEditor;
        public EXEExtractor EXEExtractor;
        private string OpenConfigFileName = "";

        public string GetOpenConfigFileName()
        {
            return OpenConfigFileName;
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            VistaOpenFileDialog OpenConfigFile = new VistaOpenFileDialog
            {
                Filter = "JSON files (Reloaded Stage Injection)|*.json|.CC files (Legacy Heroes Mod Loader Stage Injection)|*.cc"
            };

            if (OpenConfigFile.ShowDialog() == DialogResult.OK)
                OpenFile(OpenConfigFile.FileName, Program.MainForm);
        }

        /// <summary>
        /// Reads a specified Config editor config.
        /// </summary>
        /// <param name="fileName"></param>
        public void OpenFile(string fileName, MainForm.MainForm mainForm)
        {
            ProgramIsChangingStuff = true;

            if (Path.GetExtension(fileName).ToLower().Equals(".cc"))
                ReadINIConfig(fileName);
            else if (Path.GetExtension(fileName).ToLower().Equals(".json"))
                ReadJSONConfig(fileName);
            else throw new Exception("Error: Unknown file type.");

            OpenConfigFileName = fileName;
            LabelFileLoaded.Text = "Loaded " + fileName;
            SplineEditor.SplineEditorOpenConfig(mainForm.renderer);
            RankEditor.RankEditorOpenConfig();
            EnableSplineEditor();
            EnableRankEditor();
            EnableEXEExtractor();

            ProgramIsChangingStuff = false;
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(OpenConfigFileName) && Path.GetExtension(OpenConfigFileName).ToLower().Equals(".json"))
                SaveFileJson(OpenConfigFileName);
            else
                saveAsToolStripMenuItem_Click(sender, e);
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            VistaSaveFileDialog SaveConfigFile = new VistaSaveFileDialog
            {
                Filter = "JSON files (Reloaded/New Stage Injection)|*.json",
                FileName = OpenConfigFileName
            };
            DialogResult result = SaveConfigFile.ShowDialog();
            if (result == DialogResult.OK)
            {
                OpenConfigFileName = SaveConfigFile.FileName;
                LabelFileLoaded.Text = "Loaded " + OpenConfigFileName;

                if (Path.GetExtension(OpenConfigFileName).ToLower().Equals(".json"))
                    SaveFileJson(OpenConfigFileName);
                else
                    throw new Exception("Error: Unknown file type.");
            }
        }

        public void ComboBoxTeam_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ComboBoxTeam.SelectedIndex == -1)
                return;

            ProgramIsChangingStuff = true;

            NumericStartX.Value = (decimal)StartPositions[ComboBoxTeam.SelectedIndex].PositionX;
            NumericStartY.Value = (decimal)StartPositions[ComboBoxTeam.SelectedIndex].PositionY;
            NumericStartZ.Value = (decimal)StartPositions[ComboBoxTeam.SelectedIndex].PositionZ;
            NumericStartRot.Value = (decimal)ReadWriteCommon.BAMStoDegrees(StartPositions[ComboBoxTeam.SelectedIndex].Pitch);
            ComboStartMode.SelectedIndex = (int)StartPositions[ComboBoxTeam.SelectedIndex].Mode;
            NumericStartHold.Value = StartPositions[ComboBoxTeam.SelectedIndex].HoldTime;

            NumericEndX.Value = (decimal)EndPositions[ComboBoxTeam.SelectedIndex].PositionX;
            NumericEndY.Value = (decimal)EndPositions[ComboBoxTeam.SelectedIndex].PositionY;
            NumericEndZ.Value = (decimal)EndPositions[ComboBoxTeam.SelectedIndex].PositionZ;
            NumericEndRot.Value = (decimal)ReadWriteCommon.BAMStoDegrees(EndPositions[ComboBoxTeam.SelectedIndex].Pitch);

            NumericBragX.Value = (decimal)BragPositions[ComboBoxTeam.SelectedIndex].PositionX;
            NumericBragY.Value = (decimal)BragPositions[ComboBoxTeam.SelectedIndex].PositionY;
            NumericBragZ.Value = (decimal)BragPositions[ComboBoxTeam.SelectedIndex].PositionZ;
            NumericBragRot.Value = (decimal)ReadWriteCommon.BAMStoDegrees(BragPositions[ComboBoxTeam.SelectedIndex].Pitch);

            ProgramIsChangingStuff = false;
        }

        private void ComboLevelConfig_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ProgramIsChangingStuff)
                return;

            currentID = (Stage)ComboLevelConfig.SelectedItem;
            ComboBoxTeam.SelectedIndex = 0;
        }

        private void NumericStart_ValueChanged(object sender, EventArgs e)
        {
            if (ProgramIsChangingStuff)
                return;
            if (ComboBoxTeam.SelectedItem == null)
                return;

            StartPositions[ComboBoxTeam.SelectedIndex].PositionX = (float)NumericStartX.Value;
            StartPositions[ComboBoxTeam.SelectedIndex].PositionY = (float)NumericStartY.Value;
            StartPositions[ComboBoxTeam.SelectedIndex].PositionZ = (float)NumericStartZ.Value;
            StartPositions[ComboBoxTeam.SelectedIndex].Pitch = (ushort)ReadWriteCommon.DegreesToBAMS((float)NumericStartRot.Value);
            StartPositions[ComboBoxTeam.SelectedIndex].CreateTransformMatrix();
        }

        private void ComboStartMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ProgramIsChangingStuff)
                return;
            if (ComboBoxTeam.SelectedItem == null)
                return;

            StartPositions[ComboBoxTeam.SelectedIndex].Mode = (StartPositionMode)ComboStartMode.SelectedIndex;
        }

        private void NumericStartHold_ValueChanged(object sender, EventArgs e)
        {
            if (ProgramIsChangingStuff)
                return;
            if (ComboBoxTeam.SelectedItem == null)
                return;

            StartPositions[ComboBoxTeam.SelectedIndex].HoldTime = (int)NumericStartHold.Value;
        }

        private void NumericEnd_ValueChanged(object sender, EventArgs e)
        {
            if (ProgramIsChangingStuff)
                return;
            if (ComboBoxTeam.SelectedItem == null)
                return;

            EndPositions[ComboBoxTeam.SelectedIndex].PositionX = (float)NumericEndX.Value;
            EndPositions[ComboBoxTeam.SelectedIndex].PositionY = (float)NumericEndY.Value;
            EndPositions[ComboBoxTeam.SelectedIndex].PositionZ = (float)NumericEndZ.Value;
            EndPositions[ComboBoxTeam.SelectedIndex].Pitch = (ushort)ReadWriteCommon.DegreesToBAMS((float)NumericEndRot.Value);
            EndPositions[ComboBoxTeam.SelectedIndex].CreateTransformMatrix();
        }

        private void NumericBrag_ValueChanged(object sender, EventArgs e)
        {
            if (ProgramIsChangingStuff)
                return;
            if (ComboBoxTeam.SelectedItem == null)
                return;

            BragPositions[ComboBoxTeam.SelectedIndex].PositionX = (float)NumericBragX.Value;
            BragPositions[ComboBoxTeam.SelectedIndex].PositionY = (float)NumericBragY.Value;
            BragPositions[ComboBoxTeam.SelectedIndex].PositionZ = (float)NumericBragZ.Value;
            BragPositions[ComboBoxTeam.SelectedIndex].Pitch = (ushort)ReadWriteCommon.DegreesToBAMS((float)NumericBragRot.Value);
            BragPositions[ComboBoxTeam.SelectedIndex].CreateTransformMatrix();
        }

        private void buttonViewHere_Click(object sender, EventArgs e)
        {
            if (ComboBoxTeam.SelectedIndex != -1)
                Program.MainForm.renderer.Camera.SetPosition(StartPositions[ComboBoxTeam.SelectedIndex].Position.Position.ToSharpDXVector3() - 200 * Program.MainForm.renderer.Camera.GetForward());
        }

        private void buttonCurrentViewDrop_Click(object sender, EventArgs e)
        {
            if (ComboBoxTeam.SelectedItem == null)
                return;

            Vector3 Position = Program.MainForm.renderer.Camera.GetPosition() + 200 * Program.MainForm.renderer.Camera.GetForward();

            NumericStartX.Value = (decimal)Position.X;
            NumericStartY.Value = (decimal)Position.Y;
            NumericStartZ.Value = (decimal)Position.Z;
        }

        private void buttonDrop_Click(object sender, EventArgs e)
        {
            if (ComboBoxTeam.SelectedItem == null)
                return;

            Vector3 Position = Program.MainForm.LevelEditor.bspRenderer.GetDroppedPosition(StartPositions[ComboBoxTeam.SelectedIndex].Position.Position.ToSharpDXVector3());

            NumericStartX.Value = (decimal)Position.X;
            NumericStartY.Value = (decimal)Position.Y;
            NumericStartZ.Value = (decimal)Position.Z;
        }

        private void buttonViewHereEnding_Click(object sender, EventArgs e)
        {
            if (ComboBoxTeam.SelectedIndex != -1)
                Program.MainForm.renderer.Camera.SetPosition(EndPositions[ComboBoxTeam.SelectedIndex].Position.Position.ToSharpDXVector3() - 200 * Program.MainForm.renderer.Camera.GetForward());
        }

        private void buttonCurrentViewDropEnding_Click(object sender, EventArgs e)
        {
            if (ComboBoxTeam.SelectedItem == null)
                return;

            Vector3 Position = Program.MainForm.renderer.Camera.GetPosition() + 200 * Program.MainForm.renderer.Camera.GetForward();

            NumericEndX.Value = (decimal)Position.X;
            NumericEndY.Value = (decimal)Position.Y;
            NumericEndZ.Value = (decimal)Position.Z;
        }

        private void buttonDropEnding_Click(object sender, EventArgs e)
        {
            if (ComboBoxTeam.SelectedItem == null)
                return;

            Vector3 Position = Program.MainForm.LevelEditor.bspRenderer.GetDroppedPosition(EndPositions[ComboBoxTeam.SelectedIndex].Position.Position.ToSharpDXVector3());

            NumericEndX.Value = (decimal)Position.X;
            NumericEndY.Value = (decimal)Position.Y;
            NumericEndZ.Value = (decimal)Position.Z;
        }

        private void buttonViewHereBrag_Click(object sender, EventArgs e)
        {
            if (ComboBoxTeam.SelectedIndex != -1)
                Program.MainForm.renderer.Camera.SetPosition(BragPositions[ComboBoxTeam.SelectedIndex].Position.Position.ToSharpDXVector3() - 200 * Program.MainForm.renderer.Camera.GetForward());
        }

        private void buttonCurrentViewDropBrag_Click(object sender, EventArgs e)
        {
            if (ComboBoxTeam.SelectedItem == null)
                return;

            Vector3 Position = Program.MainForm.renderer.Camera.GetPosition() + 200 * Program.MainForm.renderer.Camera.GetForward();

            NumericBragX.Value = (decimal)Position.X;
            NumericBragY.Value = (decimal)Position.Y;
            NumericBragZ.Value = (decimal)Position.Z;
        }

        private void buttonDropBrag_Click(object sender, EventArgs e)
        {
            if (ComboBoxTeam.SelectedItem == null)
                return;

            Vector3 Position = Program.MainForm.LevelEditor.bspRenderer.GetDroppedPosition(BragPositions[ComboBoxTeam.SelectedIndex].Position.Position.ToSharpDXVector3());

            NumericBragX.Value = (decimal)Position.X;
            NumericBragY.Value = (decimal)Position.Y;
            NumericBragZ.Value = (decimal)Position.Z;
        }

        private void splineEditorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SplineEditor.Show();
        }

        private void rankEditorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RankEditor.Show();
        }

        private void eXEExtractorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EXEExtractor.Show();
        }
    }
}