using System;
using System.IO;
using System.Windows.Forms;
using GenericStageInjectionCommon.Shared.Enums;

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
            NumericStartRot.Minimum = decimal.MinValue;
            NumericStartHold.Minimum = 0;
            NumericStartX.Maximum = decimal.MaxValue;
            NumericStartY.Maximum = decimal.MaxValue;
            NumericStartZ.Maximum = decimal.MaxValue;
            NumericStartRot.Maximum = decimal.MaxValue;
            NumericStartHold.Maximum = 65535;

            NumericEndX.Minimum = decimal.MinValue;
            NumericEndY.Minimum = decimal.MinValue;
            NumericEndZ.Minimum = decimal.MinValue;
            NumericEndRot.Minimum = decimal.MinValue;
            NumericEndX.Maximum = decimal.MaxValue;
            NumericEndY.Maximum = decimal.MaxValue;
            NumericEndZ.Maximum = decimal.MaxValue;
            NumericEndRot.Maximum = decimal.MaxValue;

            NumericBragX.Minimum = decimal.MinValue;
            NumericBragY.Minimum = decimal.MinValue;
            NumericBragZ.Minimum = decimal.MinValue;
            NumericBragRot.Minimum = decimal.MinValue;
            NumericBragX.Maximum = decimal.MaxValue;
            NumericBragY.Maximum = decimal.MaxValue;
            NumericBragZ.Maximum = decimal.MaxValue;
            NumericBragRot.Maximum = decimal.MaxValue;

            ProgramIsChangingStuff = false;
            
            foreach (StageID i in Enum.GetValues(typeof(StageID)))
                ComboLevelConfig.Items.Add(i);

            CleanFile();
        }

        private void LayoutEditor_Load(object sender, EventArgs e)
        {
            TopMost = true;
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

            Program.MainForm.EnableSplineEditor();
            Program.SplineEditor.SplineEditorNewConfig();
        }

        private string OpenConfigFileName = "";

        public string GetOpenConfigFileName()
        {
            return OpenConfigFileName;
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog OpenConfigFile = new OpenFileDialog
            {
                Filter = "JSON files (Reloaded Stage Injection)|*.json|.CC files (Legacy Heroes Mod Loader Stage Injection)|*.cc"
            };

            if (OpenConfigFile.ShowDialog() == DialogResult.OK)
                OpenFile(OpenConfigFile.FileName);
        }
        
        /// <summary>
        /// Reads a specified Config editor config.
        /// </summary>
        /// <param name="fileName"></param>
        public void OpenFile(string fileName)
        {
            ProgramIsChangingStuff = true;

            if (Path.GetExtension(fileName).ToLower().Equals(".cc"))
                ReadINIConfig(fileName);
            else if (Path.GetExtension(fileName).ToLower().Equals(".json"))
                ReadJSONConfig(fileName);
            else throw new Exception("Error: Unknown file type.");

            OpenConfigFileName = fileName;
            LabelFileLoaded.Text = "Loaded " + fileName;
            Program.MainForm.EnableSplineEditor();
            Program.SplineEditor.SplineEditorOpenConfig(fileName);
            Program.SplineEditor.buttonSave.Enabled = true;

            ProgramIsChangingStuff = false;
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (OpenConfigFileName != null)
                if (OpenConfigFileName != "")
                    if (Path.GetExtension(OpenConfigFileName).ToLower().Equals(".json"))
                    {
                        SaveFileJson(OpenConfigFileName);
                        return;
                    }

            saveAsToolStripMenuItem_Click(sender, e);
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog SaveConfigFile = new SaveFileDialog
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
                else throw new Exception("Error: Unknown file type.");
            }
        }
        
        private void ComboBoxTeam_SelectedIndexChanged(object sender, EventArgs e)
        {
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

            currentID = (StageID)ComboLevelConfig.SelectedItem;
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
            StartPositions[ComboBoxTeam.SelectedIndex].Pitch = ReadWriteCommon.DegreesToBAMS((float)NumericStartRot.Value);
            StartPositions[ComboBoxTeam.SelectedIndex].CreateTransformMatrix();
        }
        
        private void ComboStartMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ProgramIsChangingStuff)
                return;
            if (ComboBoxTeam.SelectedItem == null)
                return;

            StartPositions[ComboBoxTeam.SelectedIndex].Mode = (GenericStageInjectionCommon.Structs.Enums.StartPositionMode)ComboStartMode.SelectedIndex;
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
                return
                    ;
            EndPositions[ComboBoxTeam.SelectedIndex].PositionX = (float)NumericEndX.Value;
            EndPositions[ComboBoxTeam.SelectedIndex].PositionY = (float)NumericEndY.Value;
            EndPositions[ComboBoxTeam.SelectedIndex].PositionZ = (float)NumericEndZ.Value;
            EndPositions[ComboBoxTeam.SelectedIndex].Pitch = ReadWriteCommon.DegreesToBAMS((float)NumericEndRot.Value);
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
            BragPositions[ComboBoxTeam.SelectedIndex].Pitch = ReadWriteCommon.DegreesToBAMS((float)NumericBragRot.Value);
            BragPositions[ComboBoxTeam.SelectedIndex].CreateTransformMatrix();
        }
    }
}