using SharpDX;
using SharpDX.Direct3D11;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using static HeroesPowerPlant.SharpRenderer;

namespace HeroesPowerPlant.Config
{
    public partial class ConfigEditor : Form
    {
        public enum ModeType
        {
            SinglePlayer,
            MultiPlayer
        }

        public class LevelConfigEntry
        {
            public string Name;
            public ModeType Mode;
            public int SplinePointer;
            public int StartPointer;
            public int EndingPointer;
            public int StartMultiPointer;
            public int BragPointer;

            public override string ToString()
            {
                return Name;
            }
        }

        public List<LevelConfigEntry> ReadConfigListData(string FileName)
        {
            List<LevelConfigEntry> list = new List<LevelConfigEntry>();

            string[] ConfigListFile = File.ReadAllLines(FileName);

            string Name = "";
            ModeType Mode = ModeType.SinglePlayer;
            int SplinePointer = 0;
            int StartPointer = 0;
            int EndingPointer = 0;
            int StartMultiPointer = 0;
            int BragPointer = 0;

            foreach (string i in ConfigListFile)
            {
                if (i.StartsWith("["))
                {
                    Name = i.Trim(new char[] { '[', ']' });
                }
                else if (i.StartsWith("Mode="))
                {
                    if (i.Contains("SinglePlayer"))
                        Mode = ModeType.SinglePlayer;
                    else if (i.Contains("MultiPlayer"))
                        Mode = ModeType.MultiPlayer;
                }
                else if (i.StartsWith("Spline="))
                    SplinePointer = Convert.ToInt32(i.Split('=')[1], 16);
                else if (i.StartsWith("Start="))
                    StartPointer = Convert.ToInt32(i.Split('=')[1], 16);
                else if (i.StartsWith("Ending="))
                    EndingPointer = Convert.ToInt32(i.Split('=')[1], 16);
                else if (i.StartsWith("StartMulti="))
                    StartMultiPointer = Convert.ToInt32(i.Split('=')[1], 16);
                else if (i.StartsWith("Brag="))
                    BragPointer = Convert.ToInt32(i.Split('=')[1], 16);
                else if ((i.Length == 0) | (i.StartsWith("EndOfFile")))
                {
                    list.Add(new LevelConfigEntry()
                    {
                        Name = Name,
                        Mode = Mode,
                        SplinePointer = SplinePointer,
                        StartPointer = StartPointer,
                        EndingPointer = EndingPointer,
                        StartMultiPointer = StartMultiPointer,
                        BragPointer = BragPointer,
                    });
                    Name = "";
                    Mode = ModeType.SinglePlayer;
                    SplinePointer = 0;
                    StartPointer = 0;
                    EndingPointer = 0;
                    StartMultiPointer = 0;
                    BragPointer = 0;
                }
            }

            return list;
        }

        public class EntryRenderer
        {
            private Matrix world;
            private DefaultRenderData renderData = new DefaultRenderData();

            public EntryRenderer(Vector3 Position, ushort YRot, Vector3 v)
            {
                NewMatrix(Position, YRot);
                renderData.Color = new Vector4(v, 0.3f);
            }

            public void NewMatrix(Vector3 Position, ushort YRot)
            {
                world = Matrix.Scaling(20f, 20f, 30f) * Matrix.RotationY(YRot * MathUtil.Pi / 32768) * Matrix.Translation(Position);
            }
            
            public void Render(Matrix viewProjection)
            {
                renderData.worldViewProjection = world * viewProjection;

                device.SetFillModeSolid();
                device.SetCullModeNormal();
                device.SetBlendStateAlphaBlend();
                device.ApplyRasterState();
                device.UpdateAllStates();

                device.UpdateData(basicBuffer, renderData);
                device.DeviceContext.VertexShader.SetConstantBuffer(0, basicBuffer);
                basicShader.Apply();

                Pyramid.Draw();
            }
        }
        
        public class PositionEntry
        {
            public Vector3 Position = new Vector3();
            public ushort Pitch;
            public byte Mode;
            public int HoldTime;

            private EntryRenderer entryRenderer;
            private Vector3 vColor;

            public PositionEntry()
            {
                vColor = Color.White.ToVector3();
                entryRenderer = new EntryRenderer(Position, Pitch, vColor);
            }

            public void NewColorAndRenderer(Vector3 v)
            {
                vColor = v;
                entryRenderer = new EntryRenderer(Position, Pitch, vColor);
            }
            
            public void newMatrix()
            {
                entryRenderer.NewMatrix(Position, Pitch);
            }

            public void Render(Matrix viewProjection)
            {
                entryRenderer.Render(viewProjection);
            }
        }
        
        public List<PositionEntry> Start_Positions = new List<PositionEntry>();
        public List<PositionEntry> End_Positions = new List<PositionEntry>();
        public List<PositionEntry> Brag_Positions = new List<PositionEntry>();

        public void RenderStartPositions( Matrix viewProjection)
        {
            foreach (PositionEntry p in Start_Positions) p.Render(viewProjection);
            foreach (PositionEntry p in End_Positions) p.Render(viewProjection);
            foreach (PositionEntry p in Brag_Positions) p.Render(viewProjection);
        }

        private void Clean_File()
        {
            groupBoxStart.Enabled = false;
            groupBoxEnd.Enabled = false;
            groupBoxBrag.Enabled = false;

            ComboLevelConfig.Enabled = true;
            ComboLevelConfig.SelectedIndex = 1;
            CurrentLevelConfig.Mode = ModeType.SinglePlayer;

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
            
            Start_Positions.Add(SonicStartPosition);
            Start_Positions.Add(DarkStartPosition);
            Start_Positions.Add(RoseStartPosition);
            Start_Positions.Add(ChaotixStartPosition);
            Start_Positions.Add(ForeditStartPosition);
            Start_Positions[0].NewColorAndRenderer(Color.Blue.ToVector3());
            Start_Positions[1].NewColorAndRenderer(Color.Red.ToVector3());
            Start_Positions[2].NewColorAndRenderer(Color.HotPink.ToVector3());
            Start_Positions[3].NewColorAndRenderer(Color.Green.ToVector3());
            Start_Positions[4].NewColorAndRenderer(Color.Orange.ToVector3());

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

            groupBoxStart.Enabled = true;
            groupBoxEnd.Enabled = true;
            ComboBoxTeam.Enabled = true;
            ComboBoxTeam.SelectedIndex = 0;
        }

        public bool Read_Config_File(string FileName)
        {
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

            string[] ConfigFile = File.ReadAllLines(FileName);

            if (ConfigFile[0] != "HEROES_MOD_LOADER_LEVEL_CONFIG_FILE") throw new Exception();

            string seename;
            if (ConfigFile[1].Contains("LEVEL_FLAG"))
                seename = ConfigFile[1].Split('=')[1];
            else throw new Exception();

            bool NotFoundEntry = true;
            foreach (LevelConfigEntry i in ComboLevelConfig.Items)
            {
                if (i.Name == seename)
                {
                    ComboLevelConfig.SelectedItem = i;
                    CurrentLevelConfig = i;
                    NotFoundEntry = false;
                    
                }
            }
            if (NotFoundEntry) throw new Exception();

            for (int x = 2; x < ConfigFile.Length; x++)
            {
                string Value = ConfigFile[x].Substring(ConfigFile[x].IndexOf("=") + 1);

                if (ConfigFile[x].StartsWith("SONIC_"))
                {
                    if (ConfigFile[x].Contains("START_"))
                    {
                        if (ConfigFile[x].Contains("POSITIONX")) { SonicStartPosition.Position.X = Convert.ToSingle(Value); continue; }
                        else if (ConfigFile[x].Contains("POSITIONY")) { SonicStartPosition.Position.Y = Convert.ToSingle(Value); continue; }
                        else if (ConfigFile[x].Contains("POSITIONZ")) { SonicStartPosition.Position.Z = Convert.ToSingle(Value); continue; }
                        else if (ConfigFile[x].Contains("PITCH")) { SonicStartPosition.Pitch = Convert.ToUInt16(Value); continue; }
                        else if (ConfigFile[x].Contains("MODE")) { SonicStartPosition.Mode = Convert.ToByte(Value); continue; }
                        else if (ConfigFile[x].Contains("RUNNING")) { SonicStartPosition.HoldTime = Convert.ToInt32(Value); continue; }
                    }
                    else if (ConfigFile[x].Contains("END_"))
                    {
                        if (ConfigFile[x].Contains("POSITIONX")) { SonicEndPosition.Position.X = Convert.ToSingle(Value); continue; }
                        else if (ConfigFile[x].Contains("POSITIONY")) { SonicEndPosition.Position.Y = Convert.ToSingle(Value); continue; }
                        else if (ConfigFile[x].Contains("POSITIONZ")) { SonicEndPosition.Position.Z = Convert.ToSingle(Value); continue; }
                        else if (ConfigFile[x].Contains("PITCH")) { SonicEndPosition.Pitch = Convert.ToUInt16(Value); continue; }
                    }
                    else if (ConfigFile[x].Contains("BRAG_"))
                    {
                        if (ConfigFile[x].Contains("POSITIONX")) { SonicBragPosition.Position.X = Convert.ToSingle(Value); continue; }
                        else if (ConfigFile[x].Contains("POSITIONY")) { SonicBragPosition.Position.Y = Convert.ToSingle(Value); continue; }
                        else if (ConfigFile[x].Contains("POSITIONZ")) { SonicBragPosition.Position.Z = Convert.ToSingle(Value); continue; }
                        else if (ConfigFile[x].Contains("PITCH")) { SonicBragPosition.Pitch = Convert.ToUInt16(Value); continue; }
                    }
                }
                else if (ConfigFile[x].StartsWith("DARK_"))
                {
                    if (ConfigFile[x].Contains("START_"))
                    {
                        if (ConfigFile[x].Contains("POSITIONX")) { DarkStartPosition.Position.X = Convert.ToSingle(Value); continue; }
                        else if (ConfigFile[x].Contains("POSITIONY")) { DarkStartPosition.Position.Y = Convert.ToSingle(Value); continue; }
                        else if (ConfigFile[x].Contains("POSITIONZ")) { DarkStartPosition.Position.Z = Convert.ToSingle(Value); continue; }
                        else if (ConfigFile[x].Contains("PITCH")) { DarkStartPosition.Pitch = Convert.ToUInt16(Value); continue; }
                        else if (ConfigFile[x].Contains("MODE")) { DarkStartPosition.Mode = Convert.ToByte(Value); continue; }
                        else if (ConfigFile[x].Contains("RUNNING")) { DarkStartPosition.HoldTime = Convert.ToInt32(Value); continue; }
                    }
                    else if (ConfigFile[x].Contains("END_"))
                    {
                        if (ConfigFile[x].Contains("POSITIONX")) { DarkEndPosition.Position.X = Convert.ToSingle(Value); continue; }
                        else if (ConfigFile[x].Contains("POSITIONY")) { DarkEndPosition.Position.Y = Convert.ToSingle(Value); continue; }
                        else if (ConfigFile[x].Contains("POSITIONZ")) { DarkEndPosition.Position.Z = Convert.ToSingle(Value); continue; }
                        else if (ConfigFile[x].Contains("PITCH")) { DarkEndPosition.Pitch = Convert.ToUInt16(Value); continue; }
                    }
                    else if (ConfigFile[x].Contains("BRAG_"))
                    {
                        if (ConfigFile[x].Contains("POSITIONX")) { DarkBragPosition.Position.X = Convert.ToSingle(Value); continue; }
                        else if (ConfigFile[x].Contains("POSITIONY")) { DarkBragPosition.Position.Y = Convert.ToSingle(Value); continue; }
                        else if (ConfigFile[x].Contains("POSITIONZ")) { DarkBragPosition.Position.Z = Convert.ToSingle(Value); continue; }
                        else if (ConfigFile[x].Contains("PITCH")) { DarkBragPosition.Pitch = Convert.ToUInt16(Value); continue; }
                    }
                }
                else if (ConfigFile[x].StartsWith("ROSE_"))
                {
                    if (ConfigFile[x].Contains("START_"))
                    {
                        if (ConfigFile[x].Contains("POSITIONX")) { RoseStartPosition.Position.X = Convert.ToSingle(Value); continue; }
                        else if (ConfigFile[x].Contains("POSITIONY")) { RoseStartPosition.Position.Y = Convert.ToSingle(Value); continue; }
                        else if (ConfigFile[x].Contains("POSITIONZ")) { RoseStartPosition.Position.Z = Convert.ToSingle(Value); continue; }
                        else if (ConfigFile[x].Contains("PITCH")) { RoseStartPosition.Pitch = Convert.ToUInt16(Value); continue; }
                        else if (ConfigFile[x].Contains("MODE")) { RoseStartPosition.Mode = Convert.ToByte(Value); continue; }
                        else if (ConfigFile[x].Contains("RUNNING")) { RoseStartPosition.HoldTime = Convert.ToInt32(Value); continue; }
                    }
                    else if (ConfigFile[x].Contains("END_"))
                    {
                        if (ConfigFile[x].Contains("POSITIONX")) { RoseEndPosition.Position.X = Convert.ToSingle(Value); continue; }
                        else if (ConfigFile[x].Contains("POSITIONY")) { RoseEndPosition.Position.Y = Convert.ToSingle(Value); continue; }
                        else if (ConfigFile[x].Contains("POSITIONZ")) { RoseEndPosition.Position.Z = Convert.ToSingle(Value); continue; }
                        else if (ConfigFile[x].Contains("PITCH")) { RoseEndPosition.Pitch = Convert.ToUInt16(Value); continue; }
                    }
                    else if (ConfigFile[x].Contains("BRAG_"))
                    {
                        if (ConfigFile[x].Contains("POSITIONX")) { RoseBragPosition.Position.X = Convert.ToSingle(Value); continue; }
                        else if (ConfigFile[x].Contains("POSITIONY")) { RoseBragPosition.Position.Y = Convert.ToSingle(Value); continue; }
                        else if (ConfigFile[x].Contains("POSITIONZ")) { RoseBragPosition.Position.Z = Convert.ToSingle(Value); continue; }
                        else if (ConfigFile[x].Contains("PITCH")) { RoseBragPosition.Pitch = Convert.ToUInt16(Value); continue; }
                    }
                }
                else if (ConfigFile[x].StartsWith("CHAOTIX_"))
                {
                    if (ConfigFile[x].Contains("START_"))
                    {
                        if (ConfigFile[x].Contains("POSITIONX")) { ChaotixStartPosition.Position.X = Convert.ToSingle(Value); continue; }
                        else if (ConfigFile[x].Contains("POSITIONY")) { ChaotixStartPosition.Position.Y = Convert.ToSingle(Value); continue; }
                        else if (ConfigFile[x].Contains("POSITIONZ")) { ChaotixStartPosition.Position.Z = Convert.ToSingle(Value); continue; }
                        else if (ConfigFile[x].Contains("PITCH")) { ChaotixStartPosition.Pitch = Convert.ToUInt16(Value); continue; }
                        else if (ConfigFile[x].Contains("MODE")) { ChaotixStartPosition.Mode = Convert.ToByte(Value); continue; }
                        else if (ConfigFile[x].Contains("RUNNING")) { ChaotixStartPosition.HoldTime = Convert.ToInt32(Value); continue; }
                    }
                    else if (ConfigFile[x].Contains("END_"))
                    {
                        if (ConfigFile[x].Contains("POSITIONX")) { ChaotixEndPosition.Position.X = Convert.ToSingle(Value); continue; }
                        else if (ConfigFile[x].Contains("POSITIONY")) { ChaotixEndPosition.Position.Y = Convert.ToSingle(Value); continue; }
                        else if (ConfigFile[x].Contains("POSITIONZ")) { ChaotixEndPosition.Position.Z = Convert.ToSingle(Value); continue; }
                        else if (ConfigFile[x].Contains("PITCH")) { ChaotixEndPosition.Pitch = Convert.ToUInt16(Value); continue; }
                    }
                    else if (ConfigFile[x].Contains("BRAG_"))
                    {
                        if (ConfigFile[x].Contains("POSITIONX")) { ChaotixBragPosition.Position.X = Convert.ToSingle(Value); continue; }
                        else if (ConfigFile[x].Contains("POSITIONY")) { ChaotixBragPosition.Position.Y = Convert.ToSingle(Value); continue; }
                        else if (ConfigFile[x].Contains("POSITIONZ")) { ChaotixBragPosition.Position.Z = Convert.ToSingle(Value); continue; }
                        else if (ConfigFile[x].Contains("PITCH")) { ChaotixBragPosition.Pitch = Convert.ToUInt16(Value); continue; }
                    }
                }
                else if (ConfigFile[x].StartsWith("FOREDIT_"))
                {
                    if (ConfigFile[x].Contains("START_"))
                    {
                        if (ConfigFile[x].Contains("POSITIONX")) { ForeditStartPosition.Position.X = Convert.ToSingle(Value); continue; }
                        else if (ConfigFile[x].Contains("POSITIONY")) { ForeditStartPosition.Position.Y = Convert.ToSingle(Value); continue; }
                        else if (ConfigFile[x].Contains("POSITIONZ")) { ForeditStartPosition.Position.Z = Convert.ToSingle(Value); continue; }
                        else if (ConfigFile[x].Contains("PITCH")) { ForeditStartPosition.Pitch = Convert.ToUInt16(Value); continue; }
                        else if (ConfigFile[x].Contains("MODE")) { ForeditStartPosition.Mode = Convert.ToByte(Value); continue; }
                        else if (ConfigFile[x].Contains("RUNNING")) { ForeditStartPosition.HoldTime = Convert.ToInt32(Value); continue; }
                    }
                    else if (ConfigFile[x].Contains("END_"))
                    {
                        if (ConfigFile[x].Contains("POSITIONX")) { ForeditEndPosition.Position.X = Convert.ToSingle(Value); continue; }
                        else if (ConfigFile[x].Contains("POSITIONY")) { ForeditEndPosition.Position.Y = Convert.ToSingle(Value); continue; }
                        else if (ConfigFile[x].Contains("POSITIONZ")) { ForeditEndPosition.Position.Z = Convert.ToSingle(Value); continue; }
                        else if (ConfigFile[x].Contains("PITCH")) { ForeditEndPosition.Pitch = Convert.ToUInt16(Value); continue; }
                    }
                    else if (ConfigFile[x].Contains("BRAG_"))
                    {
                        if (ConfigFile[x].Contains("POSITIONX")) { ForeditBragPosition.Position.X = Convert.ToSingle(Value); continue; }
                        else if (ConfigFile[x].Contains("POSITIONY")) { ForeditBragPosition.Position.Y = Convert.ToSingle(Value); continue; }
                        else if (ConfigFile[x].Contains("POSITIONZ")) { ForeditBragPosition.Position.Z = Convert.ToSingle(Value); continue; }
                        else if (ConfigFile[x].Contains("PITCH")) { ForeditBragPosition.Pitch = Convert.ToUInt16(Value); continue; }
                    }
                }
            }

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

            if (CurrentLevelConfig.Mode == ModeType.SinglePlayer)
            {
                Start_Positions.Add(RoseStartPosition);
                Start_Positions.Add(ChaotixStartPosition);
                Start_Positions.Add(ForeditStartPosition);
                Start_Positions[2].NewColorAndRenderer(Color.HotPink.ToVector3());
                Start_Positions[3].NewColorAndRenderer(Color.Green.ToVector3());
                Start_Positions[4].NewColorAndRenderer(Color.Orange.ToVector3());
            }

            if (CurrentLevelConfig.Mode == ModeType.MultiPlayer)
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

            ComboLevelConfig.Enabled = true;
            ComboBoxTeam.Enabled = true;
            ComboBoxTeam.SelectedIndex = 0;
            return true;
        }

        private void SaveFile(string FileName)
        {
            StreamWriter streamWriter = new StreamWriter(new FileStream(FileName, FileMode.Create));

            streamWriter.WriteLine("HEROES_MOD_LOADER_LEVEL_CONFIG_FILE");
            streamWriter.WriteLine("LEVEL_FLAG=" + CurrentLevelConfig.Name);
            streamWriter.WriteLine();

            if (CurrentLevelConfig.SplinePointer != 0)
                streamWriter.WriteLine("PUSH_INSTRUCTION_POINTER=0x" + (CurrentLevelConfig.SplinePointer + 0x400000 - 1).ToString("X"));
            if (CurrentLevelConfig.StartPointer != 0)
                streamWriter.WriteLine("START_POSITION_POINTER=0x" + (CurrentLevelConfig.StartPointer + 0x400000).ToString("X"));
            else if (CurrentLevelConfig.StartMultiPointer != 0)
                streamWriter.WriteLine("START_POSITION_POINTER=0x" + (CurrentLevelConfig.StartMultiPointer + 0x400000).ToString("X"));
            if (CurrentLevelConfig.EndingPointer != 0)
                streamWriter.WriteLine("END_POSITION_POINTER=0x" + (CurrentLevelConfig.EndingPointer + 0x400000).ToString("X"));
            if (CurrentLevelConfig.BragPointer != 0)
                streamWriter.WriteLine("BRAG_POSITION_POINTER=0x" + (CurrentLevelConfig.BragPointer + 0x400000).ToString("X"));
            streamWriter.WriteLine();

            string team;

            for (int i = 0; i < 5; i++)
            {
                if (i == 0) team = "SONIC_";
                else if (i == 1) team = "DARK_";
                else if (i == 2) team = "ROSE_";
                else if (i == 3) team = "CHAOTIX_";
                else team = "FOREDIT_";

                if (i < 2 | Start_Positions.Count > 2)
                {
                    streamWriter.WriteLine(team + "START_POSITIONX=" + Start_Positions[i].Position.X.ToString());
                    streamWriter.WriteLine(team + "START_POSITIONY=" + Start_Positions[i].Position.Y.ToString());
                    streamWriter.WriteLine(team + "START_POSITIONZ=" + Start_Positions[i].Position.Z.ToString());
                    streamWriter.WriteLine(team + "START_PITCH=" + Start_Positions[i].Pitch.ToString());
                    streamWriter.WriteLine(team + "START_MODE=" + Start_Positions[i].Mode.ToString());
                    streamWriter.WriteLine(team + "START_RUNNING=" + Start_Positions[i].HoldTime.ToString());
                    streamWriter.WriteLine();
                }

                streamWriter.WriteLine(team + "END_POSITIONX=" + End_Positions[i].Position.X.ToString());
                streamWriter.WriteLine(team + "END_POSITIONY=" + End_Positions[i].Position.Y.ToString());
                streamWriter.WriteLine(team + "END_POSITIONZ=" + End_Positions[i].Position.Z.ToString());
                streamWriter.WriteLine(team + "END_PITCH=" + End_Positions[i].Pitch.ToString());
                streamWriter.WriteLine();

                if (Brag_Positions.Count > 0)
                {
                    streamWriter.WriteLine(team + "BRAG_POSITIONX=" + Brag_Positions[i].Position.X.ToString());
                    streamWriter.WriteLine(team + "BRAG_POSITIONY=" + Brag_Positions[i].Position.Y.ToString());
                    streamWriter.WriteLine(team + "BRAG_POSITIONZ=" + Brag_Positions[i].Position.Z.ToString());
                    streamWriter.WriteLine(team + "BRAG_PITCH=" + Brag_Positions[i].Pitch.ToString());
                    streamWriter.WriteLine();
                }
            }

            streamWriter.Close();
            Program.splineEditor.buttonSave.Enabled = true;

            if (CurrentLevelConfig.SplinePointer == 0 & Program.splineEditor.SplineList.Count > 0)
                MessageBox.Show("Notice that this level config doesn't have a spline pointer. Your splines won't work ingame.");
            else if (CurrentLevelConfig.SplinePointer != 0 & Program.splineEditor.SplineList.Count == 0)
                Program.splineEditor.AddBlankSpline();

            Program.splineEditor.Save();
        }
    }
}
