using Heroes.SDK.Definitions.Enums;
using Heroes.SDK.Definitions.Structures.Stage.Spawn;
using SharpDX;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace HeroesPowerPlant.ConfigEditor
{
    public partial class ConfigEditor : Form
    {
        public List<StartPositionEntry> StartPositions = new List<StartPositionEntry>();
        public List<EndPositionEntry> EndPositions = new List<EndPositionEntry>();
        public List<EndPositionEntry> BragPositions = new List<EndPositionEntry>();

        public Stage currentID;

        public void RenderStartPositions(SharpRenderer renderer)
        {
            foreach (StartPositionEntry p in StartPositions) p.Render(renderer);
            foreach (EndPositionEntry p in EndPositions) p.Render(renderer);
            foreach (EndPositionEntry p in BragPositions) p.Render(renderer);
        }

        private void CleanFile()
        {
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

            for (int i = 0; i < 5; i++)
                BragPositions.Add(new EndPositionEntry());
            BragPositions[0].NewColor(Color.DarkBlue.ToVector3());
            BragPositions[1].NewColor(Color.DarkRed.ToVector3());
            BragPositions[2].NewColor(Color.DarkMagenta.ToVector3());
            BragPositions[3].NewColor(Color.DarkGreen.ToVector3());
            BragPositions[4].NewColor(Color.DarkOrange.ToVector3());
        }

        public void ReadINIConfig(string FileName)
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

            if (ConfigFile[0] != "HEROES_MOD_LOADER_LEVEL_CONFIG_FILE")
                throw new Exception("Error: unsupported file.");

            string seename;
            if (ConfigFile[1].Contains("LEVEL_FLAG"))
                seename = ConfigFile[1].Split('=')[1];
            else
                throw new Exception("Error: unsupported file.");

            currentID = Stage.Null;

            foreach (Stage i in Enum.GetValues(typeof(Stage)))
            {
                if (i.ToString() == seename)
                {
                    currentID = i;
                    break;
                }
            }
            if (currentID == Stage.Null)
                throw new Exception("Error: unsupported file.");

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
                        else if (ConfigFile[x].Contains("MODE")) { SonicStartPosition.Mode = (StartPositionMode)Convert.ToByte(Value); continue; }
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
                        else if (ConfigFile[x].Contains("MODE")) { DarkStartPosition.Mode = (StartPositionMode)Convert.ToByte(Value); continue; }
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
                        else if (ConfigFile[x].Contains("MODE")) { RoseStartPosition.Mode = (StartPositionMode) Convert.ToByte(Value); continue; }
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
                        else if (ConfigFile[x].Contains("MODE")) { ChaotixStartPosition.Mode = (StartPositionMode)Convert.ToByte(Value); continue; }
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
                        else if (ConfigFile[x].Contains("MODE")) { ForeditStartPosition.Mode = (StartPositionMode)Convert.ToByte(Value); continue; }
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
            StartPositions.Add(RoseStartPosition);
            StartPositions.Add(ChaotixStartPosition);
            StartPositions.Add(ForeditStartPosition);
            StartPositions[0].NewColor(Color.Blue.ToVector3());
            StartPositions[1].NewColor(Color.Red.ToVector3());
            StartPositions[2].NewColor(Color.HotPink.ToVector3());
            StartPositions[3].NewColor(Color.Green.ToVector3());
            StartPositions[4].NewColor(Color.Orange.ToVector3());

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

            ComboLevelConfig.SelectedItem = currentID;
            ComboBoxTeam.SelectedIndex = 0;
        }

        private void ReadJSONConfig(string fileName)
        {
            Heroes.SDK.Parsers.Custom.StageConfig c = Heroes.SDK.Parsers.Custom.StageConfig.FromPath(fileName);

            StartPositions.Clear();
            EndPositions.Clear();
            BragPositions.Clear();

            if (c.StartPositions != null)
                foreach (PositionStart s in c.StartPositions)
                {
                    int pitch = s.Pitch;
                    while (pitch < 0)
                        pitch += ushort.MaxValue;

                    StartPositions.Add(new StartPositionEntry()
                    {
                        PositionX = s.Position.X,
                        PositionY = s.Position.Y,
                        PositionZ = s.Position.Z,
                        HoldTime = s.HoldTime,
                        Mode = s.Mode,
                        Pitch = (ushort)pitch
                    });
                }

            while (StartPositions.Count < 5)
                StartPositions.Add(new StartPositionEntry());
            while (StartPositions.Count > 5)
                StartPositions.RemoveAt(StartPositions.Count - 1);

            if (c.EndPositions != null)
                foreach (PositionEnd s in c.EndPositions)
                {
                    int pitch = s.Pitch;
                    while (pitch < 0)
                        pitch += ushort.MaxValue;

                    EndPositions.Add(new EndPositionEntry()
                    {
                        PositionX = s.Position.X,
                        PositionY = s.Position.Y,
                        PositionZ = s.Position.Z,
                        Pitch = (ushort)pitch
                    });
                }

            while (EndPositions.Count < 5)
                EndPositions.Add(new EndPositionEntry());
            while (EndPositions.Count > 5)
                EndPositions.RemoveAt(EndPositions.Count - 1);

            if (c.BragPositions != null)
                foreach (PositionEnd s in c.BragPositions)
                {
                    int pitch = s.Pitch;
                    while (pitch < 0)
                        pitch += ushort.MaxValue;

                    BragPositions.Add(new EndPositionEntry()
                    {
                        PositionX = s.Position.X,
                        PositionY = s.Position.Y,
                        PositionZ = s.Position.Z,
                        Pitch = (ushort)pitch
                    });
                }

            while (BragPositions.Count < 5)
                BragPositions.Add(new EndPositionEntry());
            while (BragPositions.Count > 5)
                BragPositions.RemoveAt(BragPositions.Count - 1);

            StartPositions[0].NewColor(Color.Blue.ToVector3());
            StartPositions[1].NewColor(Color.Red.ToVector3());
            StartPositions[2].NewColor(Color.HotPink.ToVector3());
            StartPositions[3].NewColor(Color.Green.ToVector3());
            StartPositions[4].NewColor(Color.Orange.ToVector3());

            EndPositions[0].NewColor(Color.LightBlue.ToVector3());
            EndPositions[1].NewColor(Color.IndianRed.ToVector3());
            EndPositions[2].NewColor(Color.Pink.ToVector3());
            EndPositions[3].NewColor(Color.LightGreen.ToVector3());
            EndPositions[4].NewColor(Color.Yellow.ToVector3());

            BragPositions[0].NewColor(Color.DarkBlue.ToVector3());
            BragPositions[1].NewColor(Color.DarkRed.ToVector3());
            BragPositions[2].NewColor(Color.DarkMagenta.ToVector3());
            BragPositions[3].NewColor(Color.DarkGreen.ToVector3());
            BragPositions[4].NewColor(Color.DarkOrange.ToVector3());

            currentID = c.StageId;
            ComboLevelConfig.SelectedItem = currentID;
        }

        private void SaveFileJson(string FileName)
        {
            Heroes.SDK.Parsers.Custom.StageConfig c = new()
            {
                StartPositions = new PositionStart[StartPositions.Count],
                EndPositions = new PositionEnd[EndPositions.Count],
                BragPositions = new PositionEnd[BragPositions.Count]
            };

            for (int i = 0; i < StartPositions.Count; i++)
                c.StartPositions[i] = StartPositions[i].Position;

            for (int i = 0; i < EndPositions.Count; i++)
                c.EndPositions[i] = EndPositions[i].Position;

            for (int i = 0; i < BragPositions.Count; i++)
                c.BragPositions[i] = BragPositions[i].Position;

            c.StageId = currentID;

            Heroes.SDK.Parsers.Custom.StageConfig.ToPath(c, FileName);
            EnableSplineEditor();
            EnableRankEditor();
        }

        private void EnableSplineEditor()
        {
            SplineEditor.buttonSave.Enabled = true;
            splineEditorToolStripMenuItem.Enabled = true;
        }

        private void EnableRankEditor()
        {
            rankEditorToolStripMenuItem.Enabled = true;
        }

        private void EnableEXEExtractor()
        {
            eXEExtractorToolStripMenuItem.Enabled = true;
        }

        public void GetStartPositions(PositionStart[] pos)
        {
            for (int i = 0; i < StartPositions.Count; i++)
            {
                StartPositions[i].PositionX = pos[i].Position.X;
                StartPositions[i].PositionY = pos[i].Position.Y;
                StartPositions[i].PositionZ = pos[i].Position.Z;
                StartPositions[i].HoldTime = pos[i].HoldTime;
                StartPositions[i].Mode = pos[i].Mode;
                StartPositions[i].Pitch = pos[i].Pitch;
                StartPositions[i].CreateTransformMatrix();
            }
            ComboBoxTeam_SelectedIndexChanged(null, null);
        }

        public void GetEndPositions(PositionEnd[] pos)
        {
            for (int i = 0; i < EndPositions.Count; i++)
            {
                EndPositions[i].PositionX = pos[i].Position.X;
                EndPositions[i].PositionY = pos[i].Position.Y;
                EndPositions[i].PositionZ = pos[i].Position.Z;
                EndPositions[i].Pitch = pos[i].Pitch;
                EndPositions[i].CreateTransformMatrix();
            }
            ComboBoxTeam_SelectedIndexChanged(null, null);
        }

        public void GetBragPositions(PositionEnd[] pos)
        {
            for (int i = 0; i < pos.Length; i++)
            {
                BragPositions[i].PositionX = pos[i].Position.X;
                BragPositions[i].PositionY = pos[i].Position.Y;
                BragPositions[i].PositionZ = pos[i].Position.Z;
                BragPositions[i].Pitch = pos[i].Pitch;
                BragPositions[i].CreateTransformMatrix();
            }
            ComboBoxTeam_SelectedIndexChanged(null, null);
        }
    }
}
