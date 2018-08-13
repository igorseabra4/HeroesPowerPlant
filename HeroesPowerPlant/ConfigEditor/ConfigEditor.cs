using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using SharpDX;

namespace HeroesPowerPlant.Config
{
    public partial class ConfigEditor : Form
    {
        public ConfigEditor()
        {
            InitializeComponent();

            if (File.Exists("Resources/Lists/LevelConfigList.ini"))
               ComboLevelConfig.Items.AddRange(ReadConfigListData("Resources/Lists/LevelConfigList.ini").ToArray());
            else
            {
                MessageBox.Show("Error loading level config list file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
            }

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
            OpenConfigFileName = null;
            LabelFileLoaded.Text = "No file loaded";
            CleanFile();

            Program.mainForm.EnableSplineEditor();
            Program.splineEditor.SplineEditorNewConfig();
        }
        
        public string OpenConfigFileName = "";

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog OpenConfigFile = new OpenFileDialog
            {
                Filter = ".cc files|*.cc"
            };

            if (OpenConfigFile.ShowDialog() == DialogResult.OK)
            {
                ConfigEditorOpen(OpenConfigFile.FileName);
            }
        }

        public void ConfigEditorOpen(string fileName)
        {
            OpenConfigFileName = fileName;
            LabelFileLoaded.Text = "Loaded " + OpenConfigFileName;
            if (ReadINIConfig(OpenConfigFileName))
            {
                Program.mainForm.EnableSplineEditor();
                Program.splineEditor.SplineEditorOpenConfig(OpenConfigFileName);
                Program.splineEditor.buttonSave.Enabled = true;
            }
            else
            {
                MessageBox.Show("Error opening file");
                newToolStripMenuItem_Click(new object(), new EventArgs());
            }
        }
        
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (OpenConfigFileName != null)
            {
                if (OpenConfigFileName != "")
                    if (Path.GetExtension(OpenConfigFileName).ToLower().Equals(".cc"))
                        SaveFileIni(OpenConfigFileName);
                    else if (Path.GetExtension(OpenConfigFileName).ToLower().Equals(".json"))
                        SaveFileJson(OpenConfigFileName);
                    else throw new Exception();
            }
            else
            {
                saveAsToolStripMenuItem_Click(sender, e);
            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog SaveConfigFile = new SaveFileDialog
            {
                Filter = "JSON files (Reloaded/New Stage Injection)|*.json|.cc files (Heroes Mod Loader/Legacy)|*.cc",
                FileName = OpenConfigFileName
            };
            DialogResult result = SaveConfigFile.ShowDialog();
            if (result == DialogResult.OK)
            {
                OpenConfigFileName = SaveConfigFile.FileName;
                LabelFileLoaded.Text = "Loaded " + OpenConfigFileName;

                if (Path.GetExtension(OpenConfigFileName).ToLower().Equals(".cc"))
                    SaveFileIni(OpenConfigFileName);
                else if (Path.GetExtension(OpenConfigFileName).ToLower().Equals(".json"))
                    SaveFileJson(OpenConfigFileName);
                else throw new Exception();
            }
        }
        
        private void ComboBoxTeam_SelectedIndexChanged(object sender, EventArgs e)
        {
            ProgramIsChangingStuff = true;

            groupBoxEnd.Enabled = true;
            if (CurrentLevelConfig.Mode == ModeType.SinglePlayer)
            {
                groupBoxStart.Enabled = true;
                groupBoxBrag.Enabled = false;

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
            }
            else if (CurrentLevelConfig.Mode == ModeType.MultiPlayer)
            {
                if (ComboBoxTeam.SelectedIndex <= 1)
                {
                    groupBoxStart.Enabled = true;
                    NumericStartX.Value = (decimal)StartPositions[ComboBoxTeam.SelectedIndex].PositionX;
                    NumericStartY.Value = (decimal)StartPositions[ComboBoxTeam.SelectedIndex].PositionY;
                    NumericStartZ.Value = (decimal)StartPositions[ComboBoxTeam.SelectedIndex].PositionZ;
                    NumericStartRot.Value = (decimal)ReadWriteCommon.BAMStoDegrees(StartPositions[ComboBoxTeam.SelectedIndex].Pitch);
                    ComboStartMode.SelectedIndex = (int)StartPositions[ComboBoxTeam.SelectedIndex].Mode;
                    NumericStartHold.Value = StartPositions[ComboBoxTeam.SelectedIndex].HoldTime;
                }
                else
                    groupBoxStart.Enabled = false;

                NumericEndX.Value = (decimal)EndPositions[ComboBoxTeam.SelectedIndex].PositionX;
                NumericEndY.Value = (decimal)EndPositions[ComboBoxTeam.SelectedIndex].PositionY;
                NumericEndZ.Value = (decimal)EndPositions[ComboBoxTeam.SelectedIndex].PositionZ;
                NumericEndRot.Value = (decimal)ReadWriteCommon.BAMStoDegrees(EndPositions[ComboBoxTeam.SelectedIndex].Pitch);

                groupBoxBrag.Enabled = true;
                NumericBragX.Value = (decimal)BragPositions[ComboBoxTeam.SelectedIndex].PositionX;
                NumericBragY.Value = (decimal)BragPositions[ComboBoxTeam.SelectedIndex].PositionY;
                NumericBragZ.Value = (decimal)BragPositions[ComboBoxTeam.SelectedIndex].PositionZ;
                NumericBragRot.Value = (decimal)ReadWriteCommon.BAMStoDegrees(BragPositions[ComboBoxTeam.SelectedIndex].Pitch);
            }
            ProgramIsChangingStuff = false;
        }
        
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CurrentLevelConfig.Mode != ((LevelConfigEntry)ComboLevelConfig.SelectedItem).Mode)
            {
                groupBoxStart.Enabled = false;
                groupBoxEnd.Enabled = false;
                groupBoxBrag.Enabled = false;

                StartPositions = new List<StartPositionEntry>();
                EndPositions = new List<EndPositionEntry>();
                BragPositions = new List<EndPositionEntry>();

                StartPositionEntry SonicStartPosition = new StartPositionEntry();
                StartPositionEntry DarkStartPosition = new StartPositionEntry();
                StartPositionEntry RoseStartPosition = new StartPositionEntry();
                StartPositionEntry ChaotixStartPosition = new StartPositionEntry();
                StartPositionEntry ForeditStartPosition = new StartPositionEntry();

                EndPositionEntry SonicEndPosition = new EndPositionEntry();
                EndPositionEntry DarkEndPosition = new EndPositionEntry();
                EndPositionEntry RoseEndPosition = new EndPositionEntry();
                EndPositionEntry ChaotixEndPosition = new EndPositionEntry();
                EndPositionEntry ForeditEndPosition = new EndPositionEntry();

                EndPositionEntry SonicBragPosition = new EndPositionEntry();
                EndPositionEntry DarkBragPosition = new EndPositionEntry();
                EndPositionEntry RoseBragPosition = new EndPositionEntry();
                EndPositionEntry ChaotixBragPosition = new EndPositionEntry();
                EndPositionEntry ForeditBragPosition = new EndPositionEntry();
                
                StartPositions.Add(SonicStartPosition);
                StartPositions.Add(DarkStartPosition);
                StartPositions[0].NewColor(Color.Blue.ToVector3());
                StartPositions[1].NewColor(Color.Red.ToVector3());

                EndPositions.Add(SonicEndPosition);
                EndPositions.Add(DarkEndPosition);
                EndPositions.Add(RoseEndPosition);
                EndPositions.Add(ChaotixEndPosition);
                EndPositions.Add(ForeditEndPosition);
                EndPositions[0].NewColor(Color.LightBlue.ToVector3());
                EndPositions[1].NewColor(Color.IndianRed.ToVector3());
                EndPositions[2].NewColor(Color.Pink.ToVector3());
                EndPositions[3].NewColor(Color.LightGreen.ToVector3());
                EndPositions[4].NewColor(Color.Yellow.ToVector3());

                if (((LevelConfigEntry)ComboLevelConfig.SelectedItem).Mode == ModeType.SinglePlayer)
                {
                    StartPositions.Add(RoseStartPosition);
                    StartPositions.Add(ChaotixStartPosition);
                    StartPositions.Add(ForeditStartPosition);
                    StartPositions[2].NewColor(Color.HotPink.ToVector3());
                    StartPositions[3].NewColor(Color.Green.ToVector3());
                    StartPositions[4].NewColor(Color.Orange.ToVector3());
                }
                else if (((LevelConfigEntry)ComboLevelConfig.SelectedItem).Mode == ModeType.MultiPlayer)
                {
                    BragPositions.Add(SonicBragPosition);
                    BragPositions.Add(DarkBragPosition);
                    BragPositions.Add(RoseBragPosition);
                    BragPositions.Add(ChaotixBragPosition);
                    BragPositions.Add(ForeditBragPosition);
                    BragPositions[0].NewColor(Color.DarkBlue.ToVector3());
                    BragPositions[1].NewColor(Color.DarkRed.ToVector3());
                    BragPositions[2].NewColor(Color.DarkMagenta.ToVector3());
                    BragPositions[3].NewColor(Color.DarkGreen.ToVector3());
                    BragPositions[4].NewColor(Color.DarkOrange.ToVector3());
                }
            }

            CurrentLevelConfig = (LevelConfigEntry)ComboLevelConfig.SelectedItem;
            
            ComboBoxTeam.Items.Clear();
            if (CurrentLevelConfig.Mode == ModeType.SinglePlayer)
            {
                ComboBoxTeam.Items.Add("Team Sonic");
                ComboBoxTeam.Items.Add("Team Dark");
            }
            else
            {
                ComboBoxTeam.Items.Add("Team Sonic / Player 1");
                ComboBoxTeam.Items.Add("Team Dark / Player 2");
            }
            ComboBoxTeam.Items.Add("Team Rose");
            ComboBoxTeam.Items.Add("Team Chaotix");
            ComboBoxTeam.Items.Add("Team Foredit");

            if (StartPositions.Count > 0)
                ComboBoxTeam.SelectedIndex = 0;
        }

        private void NumericStart_ValueChanged(object sender, EventArgs e)
        {
            if (!ProgramIsChangingStuff)
            {
                StartPositions[ComboBoxTeam.SelectedIndex].PositionX = (float)NumericStartX.Value;
                StartPositions[ComboBoxTeam.SelectedIndex].PositionY = (float)NumericStartY.Value;
                StartPositions[ComboBoxTeam.SelectedIndex].PositionZ = (float)NumericStartZ.Value;
                StartPositions[ComboBoxTeam.SelectedIndex].Pitch = ReadWriteCommon.DegreesToBAMS((float)NumericStartRot.Value);
                StartPositions[ComboBoxTeam.SelectedIndex].CreateTransformMatrix();
            }
        }
        
        private void ComboStartMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!ProgramIsChangingStuff)
                StartPositions[ComboBoxTeam.SelectedIndex].Mode = (GenericStageInjectionCommon.Structs.Enums.StartPositionMode)ComboStartMode.SelectedIndex;
        }

        private void NumericStartHold_ValueChanged(object sender, EventArgs e)
        {
            if (!ProgramIsChangingStuff)
                StartPositions[ComboBoxTeam.SelectedIndex].HoldTime = (int)NumericStartHold.Value;
        }

        private void NumericEnd_ValueChanged(object sender, EventArgs e)
        {
            if (!ProgramIsChangingStuff)
            {
                EndPositions[ComboBoxTeam.SelectedIndex].PositionX = (float)NumericEndX.Value;
                EndPositions[ComboBoxTeam.SelectedIndex].PositionY = (float)NumericEndY.Value;
                EndPositions[ComboBoxTeam.SelectedIndex].PositionZ = (float)NumericEndZ.Value;
                EndPositions[ComboBoxTeam.SelectedIndex].Pitch = ReadWriteCommon.DegreesToBAMS((float)NumericEndRot.Value);
                EndPositions[ComboBoxTeam.SelectedIndex].CreateTransformMatrix();
            }
        }

        private void NumericBrag_ValueChanged(object sender, EventArgs e)
        {
            if (!ProgramIsChangingStuff)
            {
                BragPositions[ComboBoxTeam.SelectedIndex].PositionX = (float)NumericBragX.Value;
                BragPositions[ComboBoxTeam.SelectedIndex].PositionY = (float)NumericBragY.Value;
                BragPositions[ComboBoxTeam.SelectedIndex].PositionZ = (float)NumericBragZ.Value;
                BragPositions[ComboBoxTeam.SelectedIndex].Pitch = ReadWriteCommon.DegreesToBAMS((float)NumericBragRot.Value);
                BragPositions[ComboBoxTeam.SelectedIndex].CreateTransformMatrix();
            }
        }
    }
}