using SharpDX;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using GenericStageInjectionCommon.Structs.Positions.Substructures;
using GenericStageInjectionCommon.Shared.Enums;

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
            public StageID StageID;

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
            int StageID = 0;

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
                else if (i.StartsWith("StageID="))
                    StageID = Convert.ToInt32(i.Split('=')[1], 16);
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
                        StageID = (StageID)StageID
                    });
                    Name = "";
                    Mode = ModeType.SinglePlayer;
                    SplinePointer = 0;
                    StartPointer = 0;
                    EndingPointer = 0;
                    StartMultiPointer = 0;
                    BragPointer = 0;
                    StageID = 0;
                }
            }

            return list;
        }
        
        public List<StartPositionEntry> StartPositions = new List<StartPositionEntry>();
        public List<EndPositionEntry> EndPositions = new List<EndPositionEntry>();
        public List<EndPositionEntry> BragPositions = new List<EndPositionEntry>();

        LevelConfigEntry CurrentLevelConfig = new LevelConfigEntry();

        public void RenderStartPositions()
        {
            foreach (StartPositionEntry p in StartPositions) p.Render();
            foreach (EndPositionEntry p in EndPositions) p.Render();
            foreach (EndPositionEntry p in BragPositions) p.Render();
        }

        private void CleanFile()
        {
            groupBoxStart.Enabled = false;
            groupBoxEnd.Enabled = false;
            groupBoxBrag.Enabled = false;

            ComboLevelConfig.Enabled = true;
            ComboLevelConfig.SelectedIndex = 1;
            CurrentLevelConfig.Mode = ModeType.SinglePlayer;

            StartPositions = new List<StartPositionEntry>();
            EndPositions = new List<EndPositionEntry>();
            BragPositions = new List<EndPositionEntry>();
                        
            for (int i = 0; i < 5; i++)
                StartPositions.Add(new StartPositionEntry());
            StartPositions[0].NewColor(Color.Blue.ToVector3());
            StartPositions[1].NewColor(Color.Red.ToVector3());
            StartPositions[2].NewColor(Color.HotPink.ToVector3());
            StartPositions[3].NewColor(Color.Green.ToVector3());
            StartPositions[4].NewColor(Color.Orange.ToVector3());

            for (int i = 0; i < 5; i++)
                EndPositions.Add(new EndPositionEntry());
            EndPositions[0].NewColor(Color.LightBlue.ToVector3());
            EndPositions[1].NewColor(Color.IndianRed.ToVector3());
            EndPositions[2].NewColor(Color.Pink.ToVector3());
            EndPositions[3].NewColor(Color.LightGreen.ToVector3());
            EndPositions[4].NewColor(Color.Yellow.ToVector3());

            groupBoxStart.Enabled = true;
            groupBoxEnd.Enabled = true;
            ComboBoxTeam.Enabled = true;
            ComboBoxTeam.SelectedIndex = 0;
        }

        public bool ReadINIConfig(string FileName)
        {
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
                    break;
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
                        if (ConfigFile[x].Contains("POSITIONX")) { SonicStartPosition.PositionX = Convert.ToSingle(Value); continue; }
                        else if (ConfigFile[x].Contains("POSITIONY")) { SonicStartPosition.PositionY = Convert.ToSingle(Value); continue; }
                        else if (ConfigFile[x].Contains("POSITIONZ")) { SonicStartPosition.PositionZ = Convert.ToSingle(Value); continue; }
                        else if (ConfigFile[x].Contains("PITCH")) { SonicStartPosition.Pitch = Convert.ToUInt16(Value); continue; }
                        else if (ConfigFile[x].Contains("MODE")) { SonicStartPosition.Mode = (GenericStageInjectionCommon.Structs.Enums.StartPositionMode)Convert.ToByte(Value); continue; }
                        else if (ConfigFile[x].Contains("RUNNING")) { SonicStartPosition.HoldTime = Convert.ToInt32(Value); continue; }
                    }
                    else if (ConfigFile[x].Contains("END_"))
                    {
                        if (ConfigFile[x].Contains("POSITIONX")) { SonicEndPosition.PositionX = Convert.ToSingle(Value); continue; }
                        else if (ConfigFile[x].Contains("POSITIONY")) { SonicEndPosition.PositionY = Convert.ToSingle(Value); continue; }
                        else if (ConfigFile[x].Contains("POSITIONZ")) { SonicEndPosition.PositionZ = Convert.ToSingle(Value); continue; }
                        else if (ConfigFile[x].Contains("PITCH")) { SonicEndPosition.Pitch = Convert.ToUInt16(Value); continue; }
                    }
                    else if (ConfigFile[x].Contains("BRAG_"))
                    {
                        if (ConfigFile[x].Contains("POSITIONX")) { SonicBragPosition.PositionX = Convert.ToSingle(Value); continue; }
                        else if (ConfigFile[x].Contains("POSITIONY")) { SonicBragPosition.PositionY = Convert.ToSingle(Value); continue; }
                        else if (ConfigFile[x].Contains("POSITIONZ")) { SonicBragPosition.PositionZ = Convert.ToSingle(Value); continue; }
                        else if (ConfigFile[x].Contains("PITCH")) { SonicBragPosition.Pitch = Convert.ToUInt16(Value); continue; }
                    }
                }
                else if (ConfigFile[x].StartsWith("DARK_"))
                {
                    if (ConfigFile[x].Contains("START_"))
                    {
                        if (ConfigFile[x].Contains("POSITIONX")) { DarkStartPosition.PositionX = Convert.ToSingle(Value); continue; }
                        else if (ConfigFile[x].Contains("POSITIONY")) { DarkStartPosition.PositionY = Convert.ToSingle(Value); continue; }
                        else if (ConfigFile[x].Contains("POSITIONZ")) { DarkStartPosition.PositionZ = Convert.ToSingle(Value); continue; }
                        else if (ConfigFile[x].Contains("PITCH")) { DarkStartPosition.Pitch = Convert.ToUInt16(Value); continue; }
                        else if (ConfigFile[x].Contains("MODE")) { DarkStartPosition.Mode = (GenericStageInjectionCommon.Structs.Enums.StartPositionMode)Convert.ToByte(Value); continue; }
                        else if (ConfigFile[x].Contains("RUNNING")) { DarkStartPosition.HoldTime = Convert.ToInt32(Value); continue; }
                    }
                    else if (ConfigFile[x].Contains("END_"))
                    {
                        if (ConfigFile[x].Contains("POSITIONX")) { DarkEndPosition.PositionX = Convert.ToSingle(Value); continue; }
                        else if (ConfigFile[x].Contains("POSITIONY")) { DarkEndPosition.PositionY = Convert.ToSingle(Value); continue; }
                        else if (ConfigFile[x].Contains("POSITIONZ")) { DarkEndPosition.PositionZ = Convert.ToSingle(Value); continue; }
                        else if (ConfigFile[x].Contains("PITCH")) { DarkEndPosition.Pitch = Convert.ToUInt16(Value); continue; }
                    }
                    else if (ConfigFile[x].Contains("BRAG_"))
                    {
                        if (ConfigFile[x].Contains("POSITIONX")) { DarkBragPosition.PositionX = Convert.ToSingle(Value); continue; }
                        else if (ConfigFile[x].Contains("POSITIONY")) { DarkBragPosition.PositionY = Convert.ToSingle(Value); continue; }
                        else if (ConfigFile[x].Contains("POSITIONZ")) { DarkBragPosition.PositionZ = Convert.ToSingle(Value); continue; }
                        else if (ConfigFile[x].Contains("PITCH")) { DarkBragPosition.Pitch = Convert.ToUInt16(Value); continue; }
                    }
                }
                else if (ConfigFile[x].StartsWith("ROSE_"))
                {
                    if (ConfigFile[x].Contains("START_"))
                    {
                        if (ConfigFile[x].Contains("POSITIONX")) { RoseStartPosition.PositionX = Convert.ToSingle(Value); continue; }
                        else if (ConfigFile[x].Contains("POSITIONY")) { RoseStartPosition.PositionY = Convert.ToSingle(Value); continue; }
                        else if (ConfigFile[x].Contains("POSITIONZ")) { RoseStartPosition.PositionZ = Convert.ToSingle(Value); continue; }
                        else if (ConfigFile[x].Contains("PITCH")) { RoseStartPosition.Pitch = Convert.ToUInt16(Value); continue; }
                        else if (ConfigFile[x].Contains("MODE")) { RoseStartPosition.Mode = (GenericStageInjectionCommon.Structs.Enums.StartPositionMode)Convert.ToByte(Value); continue; }
                        else if (ConfigFile[x].Contains("RUNNING")) { RoseStartPosition.HoldTime = Convert.ToInt32(Value); continue; }
                    }
                    else if (ConfigFile[x].Contains("END_"))
                    {
                        if (ConfigFile[x].Contains("POSITIONX")) { RoseEndPosition.PositionX = Convert.ToSingle(Value); continue; }
                        else if (ConfigFile[x].Contains("POSITIONY")) { RoseEndPosition.PositionY = Convert.ToSingle(Value); continue; }
                        else if (ConfigFile[x].Contains("POSITIONZ")) { RoseEndPosition.PositionZ = Convert.ToSingle(Value); continue; }
                        else if (ConfigFile[x].Contains("PITCH")) { RoseEndPosition.Pitch = Convert.ToUInt16(Value); continue; }
                    }
                    else if (ConfigFile[x].Contains("BRAG_"))
                    {
                        if (ConfigFile[x].Contains("POSITIONX")) { RoseBragPosition.PositionX = Convert.ToSingle(Value); continue; }
                        else if (ConfigFile[x].Contains("POSITIONY")) { RoseBragPosition.PositionY = Convert.ToSingle(Value); continue; }
                        else if (ConfigFile[x].Contains("POSITIONZ")) { RoseBragPosition.PositionZ = Convert.ToSingle(Value); continue; }
                        else if (ConfigFile[x].Contains("PITCH")) { RoseBragPosition.Pitch = Convert.ToUInt16(Value); continue; }
                    }
                }
                else if (ConfigFile[x].StartsWith("CHAOTIX_"))
                {
                    if (ConfigFile[x].Contains("START_"))
                    {
                        if (ConfigFile[x].Contains("POSITIONX")) { ChaotixStartPosition.PositionX = Convert.ToSingle(Value); continue; }
                        else if (ConfigFile[x].Contains("POSITIONY")) { ChaotixStartPosition.PositionY = Convert.ToSingle(Value); continue; }
                        else if (ConfigFile[x].Contains("POSITIONZ")) { ChaotixStartPosition.PositionZ = Convert.ToSingle(Value); continue; }
                        else if (ConfigFile[x].Contains("PITCH")) { ChaotixStartPosition.Pitch = Convert.ToUInt16(Value); continue; }
                        else if (ConfigFile[x].Contains("MODE")) { ChaotixStartPosition.Mode = (GenericStageInjectionCommon.Structs.Enums.StartPositionMode)Convert.ToByte(Value); continue; }
                        else if (ConfigFile[x].Contains("RUNNING")) { ChaotixStartPosition.HoldTime = Convert.ToInt32(Value); continue; }
                    }
                    else if (ConfigFile[x].Contains("END_"))
                    {
                        if (ConfigFile[x].Contains("POSITIONX")) { ChaotixEndPosition.PositionX = Convert.ToSingle(Value); continue; }
                        else if (ConfigFile[x].Contains("POSITIONY")) { ChaotixEndPosition.PositionY = Convert.ToSingle(Value); continue; }
                        else if (ConfigFile[x].Contains("POSITIONZ")) { ChaotixEndPosition.PositionZ = Convert.ToSingle(Value); continue; }
                        else if (ConfigFile[x].Contains("PITCH")) { ChaotixEndPosition.Pitch = Convert.ToUInt16(Value); continue; }
                    }
                    else if (ConfigFile[x].Contains("BRAG_"))
                    {
                        if (ConfigFile[x].Contains("POSITIONX")) { ChaotixBragPosition.PositionX = Convert.ToSingle(Value); continue; }
                        else if (ConfigFile[x].Contains("POSITIONY")) { ChaotixBragPosition.PositionY = Convert.ToSingle(Value); continue; }
                        else if (ConfigFile[x].Contains("POSITIONZ")) { ChaotixBragPosition.PositionZ = Convert.ToSingle(Value); continue; }
                        else if (ConfigFile[x].Contains("PITCH")) { ChaotixBragPosition.Pitch = Convert.ToUInt16(Value); continue; }
                    }
                }
                else if (ConfigFile[x].StartsWith("FOREDIT_"))
                {
                    if (ConfigFile[x].Contains("START_"))
                    {
                        if (ConfigFile[x].Contains("POSITIONX")) { ForeditStartPosition.PositionX = Convert.ToSingle(Value); continue; }
                        else if (ConfigFile[x].Contains("POSITIONY")) { ForeditStartPosition.PositionY = Convert.ToSingle(Value); continue; }
                        else if (ConfigFile[x].Contains("POSITIONZ")) { ForeditStartPosition.PositionZ = Convert.ToSingle(Value); continue; }
                        else if (ConfigFile[x].Contains("PITCH")) { ForeditStartPosition.Pitch = Convert.ToUInt16(Value); continue; }
                        else if (ConfigFile[x].Contains("MODE")) { ForeditStartPosition.Mode = (GenericStageInjectionCommon.Structs.Enums.StartPositionMode)Convert.ToByte(Value); continue; }
                        else if (ConfigFile[x].Contains("RUNNING")) { ForeditStartPosition.HoldTime = Convert.ToInt32(Value); continue; }
                    }
                    else if (ConfigFile[x].Contains("END_"))
                    {
                        if (ConfigFile[x].Contains("POSITIONX")) { ForeditEndPosition.PositionX = Convert.ToSingle(Value); continue; }
                        else if (ConfigFile[x].Contains("POSITIONY")) { ForeditEndPosition.PositionY = Convert.ToSingle(Value); continue; }
                        else if (ConfigFile[x].Contains("POSITIONZ")) { ForeditEndPosition.PositionZ = Convert.ToSingle(Value); continue; }
                        else if (ConfigFile[x].Contains("PITCH")) { ForeditEndPosition.Pitch = Convert.ToUInt16(Value); continue; }
                    }
                    else if (ConfigFile[x].Contains("BRAG_"))
                    {
                        if (ConfigFile[x].Contains("POSITIONX")) { ForeditBragPosition.PositionX = Convert.ToSingle(Value); continue; }
                        else if (ConfigFile[x].Contains("POSITIONY")) { ForeditBragPosition.PositionY = Convert.ToSingle(Value); continue; }
                        else if (ConfigFile[x].Contains("POSITIONZ")) { ForeditBragPosition.PositionZ = Convert.ToSingle(Value); continue; }
                        else if (ConfigFile[x].Contains("PITCH")) { ForeditBragPosition.Pitch = Convert.ToUInt16(Value); continue; }
                    }
                }
            }

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

            if (CurrentLevelConfig.Mode == ModeType.SinglePlayer)
            {
                StartPositions.Add(RoseStartPosition);
                StartPositions.Add(ChaotixStartPosition);
                StartPositions.Add(ForeditStartPosition);
                StartPositions[2].NewColor(Color.HotPink.ToVector3());
                StartPositions[3].NewColor(Color.Green.ToVector3());
                StartPositions[4].NewColor(Color.Orange.ToVector3());
            }

            if (CurrentLevelConfig.Mode == ModeType.MultiPlayer)
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

            ComboLevelConfig.Enabled = true;
            ComboBoxTeam.Enabled = true;
            ComboBoxTeam.SelectedIndex = 0;
            return true;
        }

        private void SaveFileJson(string FileName)
        {
            GenericStageInjectionCommon.Shared.Config c = new GenericStageInjectionCommon.Shared.Config
            {
                StartPositions = new List<PositionStart>(),
                EndPositions = new List<PositionEnd>(),
                BragPositions = new List<PositionEnd>()
            };

            foreach (StartPositionEntry s in StartPositions)
                c.StartPositions.Add(s.Position);

            foreach (EndPositionEntry s in EndPositions)
                c.EndPositions.Add(s.Position);

            foreach (EndPositionEntry s in BragPositions)
                c.BragPositions.Add(s.Position);

            c.StageId = CurrentLevelConfig.StageID;

            GenericStageInjectionCommon.Shared.Config.WriteConfigEntries(FileName, c);

            Program.splineEditor.buttonSave.Enabled = true;

            Program.splineEditor.Save();
        }

        private void SaveFileIni(string FileName)
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

                if (i < 2 | StartPositions.Count > 2)
                {
                    streamWriter.WriteLine(team + "START_POSITIONX=" + StartPositions[i].PositionX.ToString());
                    streamWriter.WriteLine(team + "START_POSITIONY=" + StartPositions[i].PositionY.ToString());
                    streamWriter.WriteLine(team + "START_POSITIONZ=" + StartPositions[i].PositionZ.ToString());
                    streamWriter.WriteLine(team + "START_PITCH=" + StartPositions[i].Pitch.ToString());
                    streamWriter.WriteLine(team + "START_MODE=" + StartPositions[i].Mode.ToString());
                    streamWriter.WriteLine(team + "START_RUNNING=" + StartPositions[i].HoldTime.ToString());
                    streamWriter.WriteLine();
                }

                streamWriter.WriteLine(team + "END_POSITIONX=" + EndPositions[i].PositionX.ToString());
                streamWriter.WriteLine(team + "END_POSITIONY=" + EndPositions[i].PositionY.ToString());
                streamWriter.WriteLine(team + "END_POSITIONZ=" + EndPositions[i].PositionZ.ToString());
                streamWriter.WriteLine(team + "END_PITCH=" + EndPositions[i].Pitch.ToString());
                streamWriter.WriteLine();

                if (BragPositions.Count > 0)
                {
                    streamWriter.WriteLine(team + "BRAG_POSITIONX=" + BragPositions[i].PositionX.ToString());
                    streamWriter.WriteLine(team + "BRAG_POSITIONY=" + BragPositions[i].PositionY.ToString());
                    streamWriter.WriteLine(team + "BRAG_POSITIONZ=" + BragPositions[i].PositionZ.ToString());
                    streamWriter.WriteLine(team + "BRAG_PITCH=" + BragPositions[i].Pitch.ToString());
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
