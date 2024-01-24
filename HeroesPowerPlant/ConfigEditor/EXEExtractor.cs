using Heroes.SDK.Definitions.Enums;
using Heroes.SDK.Definitions.Structures.Stage.Spawn;
using Heroes.SDK.Definitions.Structures.Stage.Splines;
using HeroesPowerPlant.Shared.IO.Config;
using Ookii.Dialogs.WinForms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace HeroesPowerPlant.ConfigEditor
{
    public partial class EXEExtractor : Form
    {
        private ConfigEditor configEditor;

        public EXEExtractor(ConfigEditor configEditor)
        {
            InitializeComponent();

            splineHeaderListOffsets = new Dictionary<Stage, int>
            {
                { Stage.TestLevel, 0x929F1 },
                { Stage.SeasideHill, 0x9E444 },
                { Stage.OceanPalace, 0xA7173 },
                { Stage.GrandMetropolis, 0x176E5D },
                { Stage.PowerPlant, 0xB3B07 },
                { Stage.CasinoPark, 0xC39A1 },
                { Stage.BingoHighway, 0xC6D91 },
                { Stage.RailCanyon, 0xD7E13 },
                { Stage.BulletStation, 0xDC130 },
                { Stage.FrogForest, 0xEE184 },
                { Stage.LostJungle, 0xF6AC9 },
                { Stage.HangCastle, 0x108C26 },
                { Stage.MysticMansion, 0x108E85 },
                { Stage.EggFleet, 0x118EA3 },
                { Stage.FinalFortress, 0x1256B0 },
                { Stage.RobotCarnival, 0x1583ED },
                { Stage.EggAlbatross, 0x15F703 },
                { Stage.RobotStorm, 0x16132D },
                { Stage.RailCanyonChaotix, 0x148DA3 },
                { Stage.SeasideHill2P, 0x169353 },
                { Stage.GrandMetropolis2P, 0x177C8D },
                { Stage.BingoHighway2P, 0x1694C2 },
                { Stage.PinballMatch, 0x169642 },
                { Stage.MadExpress, 0x16973D },
                { Stage.TerrorHall, 0x1785D3 },
                { Stage.RailCanyonExpert, 0x16983D },
                { Stage.FrogForestExpert,0x169998 },
                { Stage.EggFleetExpert, 0x178473 }
            };

            foreach (Stage i in Enum.GetValues(typeof(Stage)))
                comboBoxStages.Items.Add(i);

            this.configEditor = configEditor;
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
            if (e.CloseReason == CloseReason.WindowsShutDown)
                return;
            if (e.CloseReason == CloseReason.FormOwnerClosing)
                return;

            e.Cancel = true;
            Hide();
        }

        private void buttonOpenExe_Click(object sender, EventArgs e)
        {
            using VistaOpenFileDialog openFile = new VistaOpenFileDialog();
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                OpenExe(openFile.FileName);
            }
        }

        private void OpenExe(string fileName)
        {
            exe = File.ReadAllBytes(fileName);
            groupBox1.Enabled = true;
        }

        private byte[] exe;
        private readonly Dictionary<Stage, int> splineHeaderListOffsets;

        private void comboBoxStages_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxStages.SelectedItem == null)
                return;
            SetStage((Stage)comboBoxStages.SelectedItem);
        }

        private void SetStage(Stage stage)
        {
            buttonStartPos.Enabled = true;

            if (stage >= Stage.SeasideHill && stage <= Stage.FinalFortress)
                buttonScores.Enabled = true;
            else
                buttonScores.Enabled = false;

            if (splineHeaderListOffsets.ContainsKey(stage))
                buttonSplines.Enabled = true;
            else
                buttonSplines.Enabled = false;
        }

        private int ReadInt(int offset) => BitConverter.ToInt32(exe, offset);
        private float ReadFloat(int offset) => BitConverter.ToSingle(exe, offset);
        private short ReadWord(int offset) => BitConverter.ToInt16(exe, offset);
        private ushort ReadUWord(int offset) => BitConverter.ToUInt16(exe, offset);
        private int startPosStart => 0x3C2FC8;
        private int endPosStart => 0x3C45B8;
        private int bragPosStart => 0x3C6380;
        private int bragPosEnd => 0x3C6A64;

        private void buttonStartPos_Click(object sender, EventArgs e)
        {
            Stage stage = (Stage)comboBoxStages.SelectedItem;

            SendStartPosToConfig((int)stage);
            SendEndPosToConfig((int)stage, false);
            SendEndPosToConfig((int)stage, true);
        }

        private void SendStartPosToConfig(int stage)
        {
            int startPosOffset = startPosStart;
            while (startPosOffset < endPosStart)
            {
                if (exe[startPosOffset] == stage)
                {
                    startPosOffset += 4;

                    PositionStart[] pos = new PositionStart[5];
                    for (int i = 0; i < 5; i++)
                    {
                        pos[i] = new PositionStart()
                        {
                            Position = new Heroes.SDK.Utilities.Math.Structs.Vector3
                            {
                                X = ReadFloat(startPosOffset),
                                Y = ReadFloat(startPosOffset + 4),
                                Z = ReadFloat(startPosOffset + 8)
                            },
                            Pitch = ReadUWord(startPosOffset + 0xC),
                            HoldTime = ReadWord(startPosOffset + 0x18),
                            Mode = (StartPositionMode)exe[startPosOffset + 0x14]
                        };
                        startPosOffset += 0x1C;
                    }

                    configEditor.GetStartPositions(pos);
                }
                else
                    startPosOffset += 0x90;
            }
        }

        private void SendEndPosToConfig(int stage, bool brag)
        {
            int offset = brag ? bragPosStart : endPosStart;

            while (offset < (brag ? bragPosEnd : bragPosStart))
            {
                if (exe[offset] == stage)
                {
                    offset += 4;

                    PositionEnd[] pos = new PositionEnd[brag ? 4 : 5];
                    for (int i = 0; i < pos.Length; i++)
                    {
                        pos[i] = new PositionEnd()
                        {
                            Position = new Heroes.SDK.Utilities.Math.Structs.Vector3
                            {
                                X = ReadFloat(offset),
                                Y = ReadFloat(offset + 4),
                                Z = ReadFloat(offset + 8)
                            },
                            Pitch = ReadUWord(offset + 0xC)
                        };
                        offset += 0x14;
                    }

                    if (brag)
                        configEditor.GetBragPositions(pos);
                    else
                        configEditor.GetEndPositions(pos);
                }
                else
                    offset += 0x68;
            }
        }

        private void buttonSplines_Click(object sender, EventArgs e)
        {
            SendSplines();
        }

        private void SendSplines()
        {
            int currentHeaderPointerOffset = ReadInt(splineHeaderListOffsets[(Stage)comboBoxStages.SelectedItem]) - 0x400000;

            int splineHeaderStart = ReadInt(currentHeaderPointerOffset);
            while (splineHeaderStart != 0)
            {
                splineHeaderStart -= 0x400000;
                short vertexNum = ReadWord(splineHeaderStart + 2);

                int firstVertexOffset = ReadInt(splineHeaderStart + 8) - 0x400000;
                SplineType splineType = (SplineType)ReadInt(splineHeaderStart + 12);

                var vertices = new List<SplineVertex>();

                for (int i = 0; i < vertexNum; i++)
                    vertices.Add(new SplineVertex(ReadFloat(firstVertexOffset + 20 * i + 8), ReadFloat(firstVertexOffset + 20 * i + 12), ReadFloat(firstVertexOffset + 20 * i + 16))
                    {
                        Pitch = ReadUWord(firstVertexOffset + 20 * i),
                        Roll = ReadUWord(firstVertexOffset + 20 * i + 2),
                    });

                configEditor.SplineEditor.AddFromExe(vertices, splineType);

                currentHeaderPointerOffset += 4;
                splineHeaderStart = ReadInt(currentHeaderPointerOffset);
            }
        }

        private void buttonScores_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Not supported yet");
        }

        public void EXEExtract(string tSonicWin, Stage stage)
        {
            OpenExe(tSonicWin);
            comboBoxStages.SelectedItem = stage;
            SetStage(stage);
            SendStartPosToConfig((int)stage);
            SendEndPosToConfig((int)stage, false);
            SendEndPosToConfig((int)stage, true);
            if (buttonSplines.Enabled)
                SendSplines();
        }
    }
}
