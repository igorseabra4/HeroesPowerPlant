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
            Clean_File();

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
            if (Read_Config_File(OpenConfigFileName))
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
                    SaveFile(OpenConfigFileName);
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
                Filter = ".cc files|*.cc",
                FileName = OpenConfigFileName
            };
            DialogResult result = SaveConfigFile.ShowDialog();
            if (result == DialogResult.OK)
            {
                OpenConfigFileName = SaveConfigFile.FileName;
                LabelFileLoaded.Text = "Loaded " + OpenConfigFileName;
                SaveFile(OpenConfigFileName);
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

                NumericStartX.Value = (decimal)Start_Positions[ComboBoxTeam.SelectedIndex].Position.X;
                NumericStartY.Value = (decimal)Start_Positions[ComboBoxTeam.SelectedIndex].Position.Y;
                NumericStartZ.Value = (decimal)Start_Positions[ComboBoxTeam.SelectedIndex].Position.Z;
                NumericStartRot.Value = (decimal)ReadWriteCommon.BAMStoDegrees(Start_Positions[ComboBoxTeam.SelectedIndex].Pitch);
                ComboStartMode.SelectedIndex = Start_Positions[ComboBoxTeam.SelectedIndex].Mode;
                NumericStartHold.Value = Start_Positions[ComboBoxTeam.SelectedIndex].HoldTime;

                NumericEndX.Value = (decimal)End_Positions[ComboBoxTeam.SelectedIndex].Position.X;
                NumericEndY.Value = (decimal)End_Positions[ComboBoxTeam.SelectedIndex].Position.Y;
                NumericEndZ.Value = (decimal)End_Positions[ComboBoxTeam.SelectedIndex].Position.Z;
                NumericEndRot.Value = (decimal)ReadWriteCommon.BAMStoDegrees(End_Positions[ComboBoxTeam.SelectedIndex].Pitch);
            }
            else if (CurrentLevelConfig.Mode == ModeType.MultiPlayer)
            {
                if (ComboBoxTeam.SelectedIndex <= 1)
                {
                    groupBoxStart.Enabled = true;
                    NumericStartX.Value = (decimal)Start_Positions[ComboBoxTeam.SelectedIndex].Position.X;
                    NumericStartY.Value = (decimal)Start_Positions[ComboBoxTeam.SelectedIndex].Position.Y;
                    NumericStartZ.Value = (decimal)Start_Positions[ComboBoxTeam.SelectedIndex].Position.Z;
                    NumericStartRot.Value = (decimal)ReadWriteCommon.BAMStoDegrees(Start_Positions[ComboBoxTeam.SelectedIndex].Pitch);
                    ComboStartMode.SelectedIndex = Start_Positions[ComboBoxTeam.SelectedIndex].Mode;
                    NumericStartHold.Value = Start_Positions[ComboBoxTeam.SelectedIndex].HoldTime;
                }
                else
                    groupBoxStart.Enabled = false;

                NumericEndX.Value = (decimal)End_Positions[ComboBoxTeam.SelectedIndex].Position.X;
                NumericEndY.Value = (decimal)End_Positions[ComboBoxTeam.SelectedIndex].Position.Y;
                NumericEndZ.Value = (decimal)End_Positions[ComboBoxTeam.SelectedIndex].Position.Z;
                NumericEndRot.Value = (decimal)ReadWriteCommon.BAMStoDegrees(End_Positions[ComboBoxTeam.SelectedIndex].Pitch);

                groupBoxBrag.Enabled = true;
                NumericBragX.Value = (decimal)Brag_Positions[ComboBoxTeam.SelectedIndex].Position.X;
                NumericBragY.Value = (decimal)Brag_Positions[ComboBoxTeam.SelectedIndex].Position.Y;
                NumericBragZ.Value = (decimal)Brag_Positions[ComboBoxTeam.SelectedIndex].Position.Z;
                NumericBragRot.Value = (decimal)ReadWriteCommon.BAMStoDegrees(Brag_Positions[ComboBoxTeam.SelectedIndex].Pitch);
            }
            ProgramIsChangingStuff = false;
        }

        LevelConfigEntry CurrentLevelConfig = new LevelConfigEntry();
        
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CurrentLevelConfig.Mode != ((LevelConfigEntry)ComboLevelConfig.SelectedItem).Mode)
            {
                groupBoxStart.Enabled = false;
                groupBoxEnd.Enabled = false;
                groupBoxBrag.Enabled = false;

                Start_Positions = new List<PositionEntry>();
                End_Positions = new List<PositionEntry>();
                Brag_Positions = new List<PositionEntry>();

                PositionEntry SonicStartPosition = new PositionEntry();
                PositionEntry DarkStartPosition = new PositionEntry();
                PositionEntry RoseStartPosition = new PositionEntry();
                PositionEntry ChaotixStartPosition = new PositionEntry();
                PositionEntry ForeditStartPosition = new PositionEntry();

                PositionEntry SonicEndPosition = new PositionEntry();
                PositionEntry DarkEndPosition = new PositionEntry();
                PositionEntry RoseEndPosition = new PositionEntry();
                PositionEntry ChaotixEndPosition = new PositionEntry();
                PositionEntry ForeditEndPosition = new PositionEntry();

                PositionEntry SonicBragPosition = new PositionEntry();
                PositionEntry DarkBragPosition = new PositionEntry();
                PositionEntry RoseBragPosition = new PositionEntry();
                PositionEntry ChaotixBragPosition = new PositionEntry();
                PositionEntry ForeditBragPosition = new PositionEntry();
                
                Start_Positions.Add(SonicStartPosition);
                Start_Positions.Add(DarkStartPosition);
                Start_Positions[0].NewColorAndRenderer(Color.Blue.ToVector3());
                Start_Positions[1].NewColorAndRenderer(Color.Red.ToVector3());

                End_Positions.Add(SonicEndPosition);
                End_Positions.Add(DarkEndPosition);
                End_Positions.Add(RoseEndPosition);
                End_Positions.Add(ChaotixEndPosition);
                End_Positions.Add(ForeditEndPosition);
                End_Positions[0].NewColorAndRenderer(Color.LightBlue.ToVector3());
                End_Positions[1].NewColorAndRenderer(Color.IndianRed.ToVector3());
                End_Positions[2].NewColorAndRenderer(Color.Pink.ToVector3());
                End_Positions[3].NewColorAndRenderer(Color.LightGreen.ToVector3());
                End_Positions[4].NewColorAndRenderer(Color.Yellow.ToVector3());

                if (((LevelConfigEntry)ComboLevelConfig.SelectedItem).Mode == ModeType.SinglePlayer)
                {
                    Start_Positions.Add(RoseStartPosition);
                    Start_Positions.Add(ChaotixStartPosition);
                    Start_Positions.Add(ForeditStartPosition);
                    Start_Positions[2].NewColorAndRenderer(Color.HotPink.ToVector3());
                    Start_Positions[3].NewColorAndRenderer(Color.Green.ToVector3());
                    Start_Positions[4].NewColorAndRenderer(Color.Orange.ToVector3());
                }
                else if (((LevelConfigEntry)ComboLevelConfig.SelectedItem).Mode == ModeType.MultiPlayer)
                {
                    Brag_Positions.Add(SonicBragPosition);
                    Brag_Positions.Add(DarkBragPosition);
                    Brag_Positions.Add(RoseBragPosition);
                    Brag_Positions.Add(ChaotixBragPosition);
                    Brag_Positions.Add(ForeditBragPosition);
                    Brag_Positions[0].NewColorAndRenderer(Color.DarkBlue.ToVector3());
                    Brag_Positions[1].NewColorAndRenderer(Color.DarkRed.ToVector3());
                    Brag_Positions[2].NewColorAndRenderer(Color.DarkMagenta.ToVector3());
                    Brag_Positions[3].NewColorAndRenderer(Color.DarkGreen.ToVector3());
                    Brag_Positions[4].NewColorAndRenderer(Color.DarkOrange.ToVector3());
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

            if (Start_Positions.Count > 0)
                ComboBoxTeam.SelectedIndex = 0;
        }

        private void NumericStart_ValueChanged(object sender, EventArgs e)
        {
            if (!ProgramIsChangingStuff)
            {
                Start_Positions[ComboBoxTeam.SelectedIndex].Position = new Vector3((float)NumericStartX.Value, (float)NumericStartY.Value, (float)NumericStartZ.Value);
                Start_Positions[ComboBoxTeam.SelectedIndex].Pitch = ReadWriteCommon.DegreesToBAMS((float)NumericStartRot.Value);
                Start_Positions[ComboBoxTeam.SelectedIndex].newMatrix();
            }
        }
        
        private void ComboStartMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!ProgramIsChangingStuff)
                Start_Positions[ComboBoxTeam.SelectedIndex].Mode = (byte)ComboStartMode.SelectedIndex;
        }

        private void NumericStartHold_ValueChanged(object sender, EventArgs e)
        {
            if (!ProgramIsChangingStuff)
                Start_Positions[ComboBoxTeam.SelectedIndex].HoldTime = (int)NumericStartHold.Value;
        }

        private void NumericEnd_ValueChanged(object sender, EventArgs e)
        {
            if (!ProgramIsChangingStuff)
            {
                End_Positions[ComboBoxTeam.SelectedIndex].Position = new Vector3((float)NumericEndX.Value, (float)NumericEndY.Value, (float)NumericEndZ.Value);
                End_Positions[ComboBoxTeam.SelectedIndex].Pitch = ReadWriteCommon.DegreesToBAMS((float)NumericEndRot.Value);
                End_Positions[ComboBoxTeam.SelectedIndex].newMatrix();
            }
        }

        private void NumericBrag_ValueChanged(object sender, EventArgs e)
        {
            if (!ProgramIsChangingStuff)
            {
                Brag_Positions[ComboBoxTeam.SelectedIndex].Position = new Vector3((float)NumericBragX.Value, (float)NumericBragY.Value, (float)NumericBragZ.Value);
                Brag_Positions[ComboBoxTeam.SelectedIndex].Pitch = ReadWriteCommon.DegreesToBAMS((float)NumericBragRot.Value);
                Brag_Positions[ComboBoxTeam.SelectedIndex].newMatrix();
            }
        }
    }
}