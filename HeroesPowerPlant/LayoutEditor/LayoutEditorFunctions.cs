using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using SharpDX;
using static HeroesPowerPlant.ReadWriteCommon;

namespace HeroesPowerPlant.LayoutEditor
{
    public static class LayoutEditorFunctions
    {
        public static List<SetObjectHeroes> GetHeroesLayout(string fileName)
        {
            List<SetObjectHeroes> list = new List<SetObjectHeroes>(2048);
            BinaryReader LayoutFileReader = new BinaryReader(new FileStream(fileName, FileMode.Open));

            for (int i = 0; i < 2048; i++)
            {
                LayoutFileReader.BaseStream.Position = 0x30 * i;
                if (LayoutFileReader.BaseStream.Position == LayoutFileReader.BaseStream.Length)
                    break;

                Vector3 Position = new Vector3(Switch(LayoutFileReader.ReadSingle()), Switch(LayoutFileReader.ReadSingle()), Switch(LayoutFileReader.ReadSingle()));
                Vector3 Rotation = new Vector3(Switch(LayoutFileReader.ReadInt32()), Switch(LayoutFileReader.ReadInt32()), Switch(LayoutFileReader.ReadInt32()));

                byte[] UnkBytes1 = LayoutFileReader.ReadBytes(4);
                LayoutFileReader.BaseStream.Position += 4;

                byte[] UnkBytes2 = LayoutFileReader.ReadBytes(4);
                LayoutFileReader.BaseStream.Position += 4;

                byte List = LayoutFileReader.ReadByte();
                byte Type = LayoutFileReader.ReadByte();
                byte Link = LayoutFileReader.ReadByte();
                byte Rend = LayoutFileReader.ReadByte();

                if (List == 0 & Type == 0)
                    continue;

                SetObjectHeroes TempObject = CreateHeroesObject(List, Type, Position, Rotation, Link, Rend, UnkBytes1.Concat(UnkBytes2).ToArray(), false);

                int MiscSettings = Switch(LayoutFileReader.ReadInt32());

                if (MiscSettings == 0)
                    TempObject.HasMiscSettings = false;
                else
                {
                    LayoutFileReader.BaseStream.Position = 0x18000 + (0x24 * MiscSettings);
                    TempObject.MiscSettings = LayoutFileReader.ReadBytes(36);
                }

                TempObject.CreateTransformMatrix();

                list.Add(TempObject);
            }

            LayoutFileReader.Close();

            return list;
        }

        public static void SaveHeroesLayout(IEnumerable<SetObjectHeroes> list, string outputFile, bool autoValues)
        {
            BinaryWriter layoutWriter = new BinaryWriter(new FileStream(outputFile, FileMode.Create));

            byte currentNum;

            if (outputFile.Contains("P1")) currentNum = 0x20;
            else if (outputFile.Contains("P2")) currentNum = 0x40;
            else if (outputFile.Contains("P3")) currentNum = 0x60;
            else if (outputFile.Contains("P4")) currentNum = 0x80;
            else if (outputFile.Contains("P5")) currentNum = 0xA0;
            else currentNum = 0;

            int j = 1;

            foreach (SetObjectHeroes i in list)
            {
                if (i.List == 0 & i.Type == 0) 
                    continue;

                layoutWriter.Write(Switch(i.Position.X));
                layoutWriter.Write(Switch(i.Position.Y));
                layoutWriter.Write(Switch(i.Position.Z));
                layoutWriter.Write(Switch((int)i.Rotation.X));
                layoutWriter.Write(Switch((int)i.Rotation.Y));
                layoutWriter.Write(Switch((int)i.Rotation.Z));

                if (autoValues || i.UnkBytes.Length != 8)
                    i.UnkBytes = new byte[] { 0, 2, currentNum, 9, 0, 2, currentNum, 9 };
                
                layoutWriter.Write(i.UnkBytes.Take(4).ToArray());
                layoutWriter.Write(0);
                layoutWriter.Write(i.UnkBytes.Skip(4).ToArray());
                layoutWriter.Write(0);

                layoutWriter.Write(i.List);
                layoutWriter.Write(i.Type);
                layoutWriter.Write(i.Link);
                layoutWriter.Write(i.Rend);

                if (i.HasMiscSettings)
                {
                    layoutWriter.Write(Switch(j));

                    long currentPos = layoutWriter.BaseStream.Position;
                    layoutWriter.BaseStream.Position = 0x18000 + (0x24 * j);

                    if (i.MiscSettings.Length != 36) throw new Exception();

                    i.MiscSettings[0] = 1;
                    i.MiscSettings[1] = 0;
                    i.MiscSettings[2] = BitConverter.GetBytes(j)[1];
                    i.MiscSettings[3] = BitConverter.GetBytes(j)[0];

                    layoutWriter.Write(i.MiscSettings);

                    j++;
                    layoutWriter.BaseStream.Position = currentPos;
                }
                else
                    layoutWriter.Write(0);
            }

            layoutWriter.Close();
        }

        public static List<SetObjectShadow> GetShadowLayout(string fileName)
        {
            BinaryReader LayoutFileReader = new BinaryReader(new FileStream(fileName, FileMode.Open));
            LayoutFileReader.BaseStream.Position = 0;

            string FileMagic = new string(LayoutFileReader.ReadChars(4));
            if (FileMagic != "sky2")
            {
                System.Windows.Forms.MessageBox.Show("This is not a valid Shadow the Hedgehog layout file.");
                return null;
            }

            int AmountOfObjects = LayoutFileReader.ReadInt32();
            List<SetObjectShadow> list = new List<SetObjectShadow>(AmountOfObjects);
            List<int> miscCountL = new List<int>(AmountOfObjects);
            uint TotalMiscLength = LayoutFileReader.ReadUInt32();

            for (int i = 0; i < AmountOfObjects; i++)
            {
                LayoutFileReader.BaseStream.Position = 12 + i * 0x2C;

                if (LayoutFileReader.BaseStream.Position >= LayoutFileReader.BaseStream.Length)
                    break;

                Vector3 Position = new Vector3(LayoutFileReader.ReadSingle(), LayoutFileReader.ReadSingle(), LayoutFileReader.ReadSingle());
                Vector3 Rotation = new Vector3(LayoutFileReader.ReadSingle(), LayoutFileReader.ReadSingle(), LayoutFileReader.ReadSingle());

                byte[] UnkBytes = LayoutFileReader.ReadBytes(8);

                byte Type = LayoutFileReader.ReadByte();
                byte List = LayoutFileReader.ReadByte();
                byte Link = LayoutFileReader.ReadByte();
                byte Rend = LayoutFileReader.ReadByte();
                miscCountL.Add(LayoutFileReader.ReadInt32());

                SetObjectShadow TempObject = CreateShadowObject(List, Type, Position, Rotation, Link, Rend, UnkBytes, false);

                list.Add(TempObject);
            }

            LayoutFileReader.BaseStream.Position = 12 + AmountOfObjects * 0x2C;
            for (int i = 0; i < list.Count; i++)
            {
                list[i].MiscSettings = LayoutFileReader.ReadBytes(miscCountL[i]);
                list[i].CreateTransformMatrix();
            }

            LayoutFileReader.Close();

            return list;
        }

        public static void SaveShadowLayout(IEnumerable<SetObjectShadow> list, string outputFile, bool autoValues)
        {
            byte currentNum;

            if (outputFile.ToLower().Contains("cmn"))
                currentNum = 0x10;
            else if (outputFile.ToLower().Contains("nrm"))
                currentNum = 0x20;
            else if (outputFile.ToLower().Contains("hrd"))
                currentNum = 0x40;
            else if (outputFile.ToLower().Contains("ds1"))
                currentNum = 0x80;
            else
                currentNum = 0;

            BinaryWriter layoutWriter = new BinaryWriter(new FileStream(outputFile, FileMode.Create));

            layoutWriter.Write(new byte[] { 0x73, 0x6B, 0x79, 0x32 });
            layoutWriter.Write(list.Count());
            layoutWriter.Write(0);

            foreach (SetObjectShadow i in list)
            {
                layoutWriter.Write(i.Position.X);
                layoutWriter.Write(i.Position.Y);
                layoutWriter.Write(i.Position.Z);
                layoutWriter.Write(i.Rotation.X);
                layoutWriter.Write(i.Rotation.Y);
                layoutWriter.Write(i.Rotation.Z);

                if (autoValues || i.UnkBytes.Length != 8)
                {
                    var unkBytes = new List<byte>() { 1, currentNum };
                    unkBytes.AddRange(currentNum == 0x80 ? new byte[] { 0x40, 0x80 } : new byte[] { 0, 0x80 });
                    unkBytes.AddRange(new byte[] { 1, currentNum });
                    unkBytes.AddRange(currentNum == 0x80 ? new byte[] { 0x40, 0x80 } : new byte[] { 0, 0 });

                    i.UnkBytes = unkBytes.ToArray();
                }

                layoutWriter.Write(i.UnkBytes);

                layoutWriter.Write(i.Type);
                layoutWriter.Write(i.List);
                layoutWriter.Write(i.Link);
                layoutWriter.Write(i.Rend);
                layoutWriter.Write(i.MiscSettings.Length);
                layoutWriter.Write(0);
            }

            int MiscSettingLength = -(int)layoutWriter.BaseStream.Position;

            foreach (SetObjectShadow i in list)
                layoutWriter.Write(i.MiscSettings);

            MiscSettingLength += (int)layoutWriter.BaseStream.Position;

            layoutWriter.BaseStream.Position = 8;
            layoutWriter.Write(MiscSettingLength);

            layoutWriter.Close();
        }

        public static List<SetObjectHeroes> GetObjectsFromObjFile(string FileName, byte List, byte Type)
        {
            List<SetObjectHeroes> list = new List<SetObjectHeroes>();

            string[] SplineFile = File.ReadAllLines(FileName);
            foreach (string j in SplineFile)
            {
                if (j.StartsWith("v"))
                {
                    string[] a = Regex.Replace(j, @"\s+", " ").Split();
                    SetObjectHeroes heroesSetObject = CreateHeroesObject
                        (List, Type, new Vector3(Convert.ToSingle(a[1]), Convert.ToSingle(a[2]), Convert.ToSingle(a[3])), Vector3.Zero, 0, 10);

                    list.Add(heroesSetObject);
                }
            }
            return list;
        }

        public static List<SetObjectHeroes> GetHeroesLayoutFromINI(string fileName)
        {
            string[] file = File.ReadAllLines(fileName);
            List<SetObjectHeroes> list = new List<SetObjectHeroes>();

            SetObjectHeroes TempObject = CreateHeroesObject(0, 0, Vector3.Zero, Vector3.Zero, 0, 10, new byte[8], false);

            foreach (string s in file)
            {
                if (s.StartsWith("obj "))
                {
                    if (!(TempObject.List == 0 & TempObject.Type == 0))
                    {
                        TempObject.CreateTransformMatrix();
                        list.Add(TempObject);
                    }
                    TempObject = null;
                    TempObject = CreateHeroesObject(Convert.ToByte(s.Substring(4, 2), 16), Convert.ToByte(s.Substring(6, 2), 16), Vector3.Zero, Vector3.Zero, 0, 10, createMatrix: false);
                }
                else if (s.StartsWith("link "))
                {
                    TempObject.Link = Convert.ToByte(s.Substring(5, 2));
                }
                else if (s.StartsWith("rend "))
                {
                    TempObject.Rend = Convert.ToByte(s.Substring(5, 2));
                }
                else if (s.StartsWith("v "))
                {
                    string[] j = s.Split(' ');
                    TempObject.Position = new Vector3(Convert.ToSingle(j[1]), Convert.ToSingle(j[2]), Convert.ToSingle(j[3]));
                }
                else if (s.StartsWith("r "))
                {
                    string[] j = s.Split(' ');
                    TempObject.Rotation = new Vector3(Convert.ToSingle(j[1]), Convert.ToSingle(j[2]), Convert.ToSingle(j[3]));
                }
                else if (s.StartsWith("b "))
                {
                    string[] j = s.Split(' ');
                    TempObject.UnkBytes[0] = Convert.ToByte(j[1]);
                    TempObject.UnkBytes[1] = Convert.ToByte(j[2]);
                    TempObject.UnkBytes[2] = Convert.ToByte(j[3]);
                    TempObject.UnkBytes[3] = Convert.ToByte(j[4]);
                    TempObject.UnkBytes[4] = Convert.ToByte(j[5]);
                    TempObject.UnkBytes[5] = Convert.ToByte(j[6]);
                    TempObject.UnkBytes[6] = Convert.ToByte(j[7]);
                    TempObject.UnkBytes[7] = Convert.ToByte(j[8]);
                }
                else if (s.StartsWith("misc "))
                {
                    if (s.Length == 9 * 9 + 4)
                    {
                        string NewMiscString = Regex.Replace(s.Substring(5), @"\s+", ""); ;
                        List<byte> NewMiscSettings = new List<byte>();

                        for (int j = 0; j < 8 * 9; j += 2)
                        {
                            string byteasstring = new string(new char[] { NewMiscString[j], NewMiscString[j + 1] });
                            NewMiscSettings.Add(Convert.ToByte(byteasstring, 16));
                        }

                        TempObject.MiscSettings = NewMiscSettings.ToArray();
                    }
                    else
                    {
                        TempObject.HasMiscSettings = false;
                    }
                }
                else if (s == "EndOfFile")
                {
                    if (!(TempObject.List == 0 & TempObject.Type == 0))
                    {
                        TempObject.CreateTransformMatrix();
                        list.Add(TempObject);
                    }
                    TempObject = null;
                }
            }

            return list;
        }

        public static void SaveHeroesLayoutINI(IEnumerable<SetObjectHeroes> list, string outputFile)
        {
            StreamWriter iniWriter = new StreamWriter(new FileStream(outputFile, FileMode.Create));
            iniWriter.WriteLine("#Exported by HeroesPowerPlant");
            iniWriter.WriteLine("#Heroes Layout Editor INI");
            iniWriter.WriteLine();

            foreach (SetObjectHeroes i in list)
            {
                iniWriter.WriteLine("obj "
                    + String.Format("{0, 2:X2}", i.List)
                    + String.Format("{0, 2:X2}", i.Type)
                    + "_" + i.GetName.Replace(' ', '-'));
                iniWriter.WriteLine("link " + String.Format("{0, 2:D2}", i.Link));
                iniWriter.WriteLine("rend " + String.Format("{0, 2:D2}", i.Rend));
                iniWriter.WriteLine("b " +
                    i.UnkBytes[0].ToString() + " " + i.UnkBytes[1].ToString() + " " + i.UnkBytes[2].ToString() + " " + i.UnkBytes[3].ToString() + " " +
                    i.UnkBytes[4].ToString() + " " + i.UnkBytes[5].ToString() + " " + i.UnkBytes[6].ToString() + " " + i.UnkBytes[7].ToString());
                iniWriter.WriteLine("v " + i.Position.X.ToString() + " " + i.Position.Y.ToString() + " " + i.Position.Z.ToString());
                iniWriter.WriteLine("r " + i.Rotation.X.ToString() + " " + i.Rotation.Y.ToString() + " " + i.Rotation.Z.ToString());
                if (i.HasMiscSettings)
                {
                    List<char> m = new List<char>();
                    for (int j = 0; j < i.MiscSettings.Length; j++)
                    {
                        if (j % 4 == 0) m.Add(' ');
                        m.AddRange(String.Format("{0, 2:X2}", i.MiscSettings[j]).ToCharArray());
                    }
                    iniWriter.WriteLine("misc" + new string(m.ToArray()));
                }
                else
                    iniWriter.WriteLine("misc -");
                iniWriter.WriteLine();
            }

            iniWriter.WriteLine("EndOfFile");
            iniWriter.Close();
        }

        public static Dictionary<(byte, byte), ObjectEntry> ReadObjectListData(string FileName)
        {
            var list = new Dictionary<(byte, byte), ObjectEntry>();

            byte List = 0;
            byte Type = 0;
            string Name = "";
            bool HasMiscSettings = true;
            int MiscSettingCount = -1;
            int ModelMiscSetting = -1;
            string DebugName = "";
            List<string[]> Models = new List<string[]>();

            foreach (string i in File.ReadAllLines(FileName))
            {
                if (i.StartsWith("["))
                {
                    List = Convert.ToByte(i.Substring(1, 2), 16);
                    Type = Convert.ToByte(i.Substring(5, 2), 16);
                }
                else if (i.StartsWith("Object="))
                    Name = i.Split('=')[1];
                else if (i.StartsWith("NoMiscSettings="))
                    HasMiscSettings = !Convert.ToBoolean(i.Split('=')[1]);
                else if (i.StartsWith("MiscSettingCount="))
                    MiscSettingCount = Convert.ToInt32(i.Split('=')[1]);
                else if (i.StartsWith("Debug="))
                    DebugName = i.Split('=')[1];
                else if (i.StartsWith("Model="))
                    Models.Add(i.Split('=')[1].Split(','));
                else if (i.StartsWith("ModelMiscSetting="))
                    ModelMiscSetting = Convert.ToInt32(i.Split('=')[1]);
                else if (i.StartsWith("EndOfFile"))
                {
                    list.Add((List, Type), new ObjectEntry()
                    {
                        List = List,
                        Type = Type,
                        Name = Name,
                        HasMiscSettings = HasMiscSettings,
                        DebugName = DebugName,
                        ModelNames = Models.ToArray(),
                        MiscSettingCount = MiscSettingCount,
                        ModelMiscSetting = ModelMiscSetting
                    });
                    break;
                }
                else if (i.Length == 0)
                {
                    list.Add((List, Type), new ObjectEntry()
                    {
                        List = List,
                        Type = Type,
                        Name = Name,
                        HasMiscSettings = HasMiscSettings,
                        DebugName = DebugName,
                        ModelNames = Models.ToArray(),
                        MiscSettingCount = MiscSettingCount,
                        ModelMiscSetting = ModelMiscSetting
                    });
                    List = 0;
                    Type = 0;
                    Name = "";
                    HasMiscSettings = true;
                    DebugName = "";
                    Models = new List<string[]>();
                    MiscSettingCount = -1;
                    ModelMiscSetting = -1;
                }
            }

            return list;
        }
        
        public static SetObjectHeroes CreateHeroesObject
            (byte List, byte Type, Vector3 Position, Vector3 Rotation, byte Link, byte Rend, byte[] UnkBytes = null, bool createMatrix = true)
        {
            SetObjectHeroes heroesObj = FindObjectClassHeroes(List, Type);
            heroesObj.Position = Position;
            heroesObj.Rotation = Rotation;
            heroesObj.List = List;
            heroesObj.Type = Type;
            heroesObj.Link = Link;
            heroesObj.Rend = Rend;
            heroesObj.UnkBytes = UnkBytes ?? new byte[8];
            heroesObj.SetObjectEntry(LayoutEditorSystem.heroesObjectEntry(List, Type));
            if (createMatrix)
                heroesObj.CreateTransformMatrix();

            return heroesObj;
        }

        private static SetObjectHeroes FindObjectClassHeroes(byte List, byte Type)
        {
            switch (List)
            {
                case 0:
                    switch (Type)
                    {
                        case 0: return new Object0000_Nothing();
                        case 0x1B: case 0x28: case 0x67: return new Object_HeroesEmpty();
                        case 0x1: return new Object0001_Spring();
                        case 0x2: return new Object0002_TripleSpring();
                        case 0x3: return new Object0003_Ring();
                        case 0x4: return new Object0004_HintRing();
                        case 0x5: return new Object0005_Switch();
                        case 0x6: return new Object0006_SwitchPP();
                        case 0x7: return new Object0007_Target();
                        case 0xB: return new Object000B_DashPanel();
                        case 0xC: return new Object000C_DashRing();
                        case 0xD: return new Object000D_BigRings();
                        case 0xE: return new Object000E_Checkpoint();
                        case 0xF: return new Object000F_DashRamp();
                        case 0x10: return new Object0010_Cannon();
                        case 0x13: case 0x14: return new Object00_Weight();
                        case 0x15: return new Object0015_SpikeBall();
                        case 0x16: return new Object0016_LaserFence();
                        case 0x18: return new Object0018_ItemBox();
                        case 0x19: return new Object0019_ItemBalloon();
                        case 0x1D: return new Object001D_Pulley();
                        case 0x20: case 0x21: case 0x22: return new Object00_Box();
                        case 0x23: return new Object0023_Chao();
                        case 0x24: return new Object0024_Cage();
                        case 0x25: return new Object0025_FormSign();
                        case 0x26: return new Object0026_FormGate();
                        case 0x29: return new Object0029_Pole();
                        case 0x2C: return new Object002C_RollDoor();
                        case 0x2E: return new Object002E_Fan();
                        case 0x31: return new Object0031_Case();
                        case 0x32: return new Object0032_WarpFlower();
                        case 0x50: // invisible collision
                        case 0x61: // no input collision
                        case 0x82: // no ottotto collision
                            return new Object00_TriggerCommon();
                        case 0x56: return new Object0056_TriggerTalk();
                        case 0x59: return new Object0059_TriggerLight();
                        case 0x60: return new Object0060_TriggerRhinoLiner();
                        //case 0x62: return new Object0062_TriggerEggHalk();
                        //case 0x63: return new Object0063_TriggerFalco();
                        case 0x64: return new Object0064_TriggerHurt(); // damage collision
                        //case 0x65: return new Object0065_TriggerKlagen();
                        case 0x66: return new Object0066_TriggerBobJump();
                        case 0x80: return new Object0080_TriggerTeleport();
                        case 0x81: return new Object0081_TriggerSE();
                        default: return new Object_HeroesDefault();
                    }
                case 1:
                    return Type switch
                    {
                        0x2 => new Object0102_TruckRail(),
                        0x3 => new Object0103_TruckPath(),
                        0x4 => new Object_HeroesEmpty(),
                        0x5 => new Object0105_MovingRuin(),
                        0x8 => new Object0108_TriggerRuins(),
                        0xA => new Object_B1_1_Type(),
                        0xB => new Object0023_Chao(),
                        0x80 => new Object0180_FlowerPatch(),
                        0x81 => new Object0181_SeaPole(),
                        0x82 => new Object0182_Whale(),
                        0x83 => new Object0183_Seagulls(),
                        0x84 => new Object0184_LargeBird(),
                        0x85 => new Object_XYZScale(1),
                        //case 0x86: return new Object0186_WaterfallLarge();
                        0x87 => new Object0187_Tides(),
                        0x88 => new Object_F1Scale(),
                        0x89 => new Object0189_WaterfallSmall(),
                        0xFF => new Object01FF_SetParticle(),
                        _ => new Object_HeroesDefault(),
                    };
                case 2:
                    switch (Type)
                    {
                        case 0x0: return new Object0200_CrumbleStonePillar();
                        //case 0x1: return new Object_();
                        case 0x2: return new Object_F1Scale();
                        case 0x3: return new Object_B1_1_Type();
                        case 0x4: return new Object0204_Kaos();
                        //case 0x5: return new Object0205_ScrollRing();
                        //case 0x6: return new Object0206_ScrollBalloon();
                        case 0xA: return new Object020A_ColliQuake();
                        case 0xB: return new Object020B_EventActivator();
                        case 0xC: return new Object020C_TriggerKaos();
                        //case 0x80: return new Object0280_MovingLand();
                        case 0x81: return new Object0281_TurtleFeet();
                        case 0x82: return new Object0282_KameWave();
                        case 0x83: case 0x84: case 0x85: return new Object_IntTypeFloatScale();
                        default: return new Object_HeroesDefault();
                    }
                case 3:
                    switch (Type)
                    {
                        case 0: return new Object0300_AcceleratorRoad();
                        case 0x2: return new Object0302_RoadCap();
                        case 0x3: case 0x4: return new Object_HeroesEmpty();
                        case 0x5: return new Object0305_BigBridge();
                        case 0x6: return new Object0306_AirCar();
                        case 0x7: case 0x81: case 0x82: return new Object_XYZScale(0);
                        case 0x8: return new Object0308_Accelerator();
                        case 0x80: return new Object0380_BalloonDesign();
                        //case 0x82: return new Object0382_Train();
                        //case 0x83: return new Object0383_PipeDesign();
                        default: return new Object_HeroesDefault();
                    }
                case 4:
                    switch (Type)
                    {
                        case 0x80: case 0x81: return new Object_HeroesEmpty();
                        case 0x1: return new Object0401_EnergyColumn();
                        case 0x3: case 0x8: case 0x10: return new Object_B1_1_Type();
                        case 0x82: case 0x84: return new Object04_CraneWallLight();
                        case 0x83: return new Object_F1Speed();
                        case 0x85: return new Object_XYZScale(1);
                        default: return new Object_HeroesDefault();
                    }
                case 5:
                    switch (Type)
                    {
                        case 0x0: case 0x1: return new Object05_Spring();
                        case 0x2: return new Object0502_Flipper();
                        case 0x3: return new Object0503_TriBumper();
                        case 0x4: case 0x5: return new Object05_StarPanel();
                        case 0x7: case 0xD: case 0x10: return new Object_F1Scale();
                        case 0x8: case 0x9: case 0x81: case 0x82: return new Object_L1Type();
                        case 0x0A: return new Object050A_Dice();
                        case 0x0B: return new Object050B_Slot();
                        case 0x86: return new Object0586_Roulette();
                        case 0x87: return new Object0587_GiantCasinoChip();
                        case 0x88: return new Object_HeroesEmpty();
                        default: return new Object_HeroesDefault();
                    }
                case 7:
                    switch (Type)
                    {
                        case 0x00:
                        case 0x01:
                        case 0x0A:
                        case 0x1B:
                        case 0x42:
                        case 0x82:
                        case 0x83:
                        case 0x85:
                        case 0x86:
                        case 0x88:
                        case 0x89:
                        case 0x8A:
                        case 0x8B:
                        case 0x8C:
                        case 0x8D:
                        case 0x8E:
                        case 0x8F:
                        case 0x90:
                        case 0x91:
                        case 0x92:
                        case 0x93:
                        case 0x98: return new Object_HeroesEmpty();
                        case 0x03: case 0x80: return new Object_F1Speed();
                        case 0x04: return new Object0704_RailRoadblock();
                        case 0x05: return new Object0705_Capsule();
                        case 0x06: return new Object_L1Type();
                        case 0x43: return new Object_F1Range();
                        case 0x81: return new Object_F1Scale();
                        case 0x87: return new Object_XYZScale(11);
                        case 0x94: return new Object_F1Range();
                        case 0x96: return new Object_F1Scale();
                        case 0x97: return new Object_L1Type();
                        default: return new Object_HeroesDefault();
                    }
                case 8:
                    switch (Type)
                    {
                        case 0x0: return new Object_F1Speed();
                        case 0x2: return new Object_L1Offset();
                        case 0x3: case 0x4: return new Object_L1Type();
                        default: return new Object_HeroesDefault();
                    }
                case 9:
                    switch (Type)
                    {
                        case 0x0: return new Object0900_Frog();
                        case 0x2: case 0x5: case 0x7: return new Object_F1Range();
                        case 0x3: return new Object0903_RainMush();
                        case 0x4: return new Object0904_RainIvy();
                        case 0x6: case 0x10: case 0x11: case 0x12: case 0x98: return new Object_HeroesEmpty();
                        case 0x8: return new Object0908_RainFruit();
                        case 0x9: return new Object000B_DashPanel();
                        case 0xB: return new Object090B_IvyJump();
                        case 0xC: case 0x85: case 0x86: case 0x8B: case 0x8C: case 0x8D:
                        case 0x91: case 0x92: case 0x93: case 0x95:
                            return new Object_F1Scale();
                        case 0xD: return new Object_XYZScale(0);
                        case 0xE: return new Object090E_Alligator();
                        case 0x13: return new Object0913_RainCollision();
                        case 0x80: return new Object0980_Butterfly();
                        case 0x81: return new Object0981_Flower();
                        case 0x82: case 0x83: return new Object0982_Mushroom();
                        case 0x84: return new Object0984_RedWeed();
                        case 0x87: case 0x88: case 0x8A: return new Object_IntTypeFloatScale();
                        case 0x89: return new Object0989_Pond();
                        case 0x97: return new Object0987_RedGreenPlant();
                        case 0x99: return new Object0999_Powder();
                        case 0x9A: return new Object099A_Wanibreak();
                        default: return new Object_HeroesDefault();
                    }
                case 0x11:
                    switch (Type)
                    {
                        case 0x0: return new Object1100_TeleportSwitch();
                        case 0x1: return new Object1101_CastleDoor();
                        case 0x2: return new Object1102_CastleWall();
                        case 0x3: return new Object11_FloatingPlatform();
                        case 0x4: return new Object1104_FlameTorch();
                        case 0x5: return new Object1105_Ghost();
                        case 0x6: return new Object11_FloatingPlatform();
                        case 0x7: return new Object11_MansionWallThunder();
                        case 0x8: return new Object1108_MansionDoor();
                        case 0x9: return new Object_HeroesEmpty();
                        case 0xA: return new Object_HeroesEmpty();
                        case 0xB: return new Object_F1Range();
                        case 0xC: return new Object110C_TriggerMusic();
                        case 0x80: return new Object_IntTypeFloatScale();
                        case 0x81: return new Object1181_Celestial();
                        case 0x82: return new Object11_MansionWallThunder();
                        case 0x83: return new Object_F1Range();
                        case 0x84: return new Object1184_SmokeScreen();
                        case 0x85: return new Object1185_Bone();
                        case 0x88: return new Object1188_Curtain();
                        case 0x89: return new Object_L1Type();
                        default: return new Object_HeroesDefault();
                    }
                case 0x13:
                    switch (Type)
                    {
                        case 0x2: return new Object1302_HorizCannon();
                        case 0x3: return new Object1303_MovingCannon();
                        case 0x5: return new Object1305_EggFleetDoor();
                        case 0x8: case 0x80: return new Object_F1Speed();
                        case 0x20: return new Object_B1_1_Type();
                        case 0x07:
                        case 0x81:
                        case 0x82:
                        case 0x83:
                        case 0x85:
                        case 0x86:
                        case 0x87:
                        case 0x88:
                        case 0x89:
                        case 0x8A:
                        case 0x8B:
                        case 0x8C:
                        case 0x8D:
                        case 0x8E:
                        case 0x8F:
                        case 0x90:
                        case 0x91:
                        case 0x92:
                        case 0x93:
                        case 0x94: return new Object_HeroesEmpty();
                        default: return new Object_HeroesDefault();
                    }
                case 0x14:
                    switch (Type)
                    {
                        default: return new Object_HeroesDefault();
                    }
                case 0x15:
                    switch (Type)
                    {
                        case 0: return new Object1500_EggFlapper();
                        case 0x10: return new Object1510_EggPawn();
                        case 0x20: case 0x70: return new Object15_KlagenCameron();
                        case 0x40: return new Object1540_EggHammer();
                        case 0x90: return new Object1590_RhinoLiner();
                        case 0xC0: return new Object15C0_EggBishop();
                        case 0xD0: return new Object15D0_E2000();
                        case 0xF4: return new Object0026_FormGate();
                        default: return new Object_HeroesDefault();
                    }
                case 0x16:
                    switch (Type)
                    {
                        case 0x0: case 0x1: return new Object_16_00_01();
                        case 0x2: return new Object_16_02();
                        default: return new Object_HeroesDefault();
                    }
                case 0x20:
                    switch (Type)
                    {
                        case 0x80: case 0x81: return new Object_B1_1_Type();
                        default: return new Object_HeroesDefault();
                    }
                case 0x23:
                    switch (Type)
                    {
                        case 0x0: return new Object2300_EggAlbatross();
                        default: return new Object_HeroesDefault();
                    }
                case 0x33:
                    switch (Type)
                    {
                        case 0x0: return new Object_S1_1_Type();
                        default: return new Object_HeroesDefault();
                    }
                default:
                    return new Object_HeroesDefault();
            }
        }

        public static SetObjectShadow CreateShadowObject
            (byte List, byte Type, Vector3 Position, Vector3 Rotation, byte Link, byte Rend, byte[] UnkBytes = null, bool createMatrix = true)
        {
            SetObjectShadow shadowObj = FindObjectClassShadow(List, Type);
            shadowObj.Position = Position;
            shadowObj.Rotation = Rotation;
            shadowObj.List = List;
            shadowObj.Type = Type;
            shadowObj.Link = Link;
            shadowObj.Rend = Rend;
            shadowObj.UnkBytes = UnkBytes ?? new byte[8];
            shadowObj.SetObjectEntry(LayoutEditorSystem.shadowObjectEntry(List, Type));
            if (createMatrix)
                shadowObj.CreateTransformMatrix();

            return shadowObj;
        }

        private static SetObjectShadow FindObjectClassShadow(byte List, byte Type)
        {
            switch (List) {
                case 0x00:
                    switch (Type) {
                        case 0x01: case 0x02: case 0x03: case 0x06: return new Object00_SpringShadow();
                        case 0x04: return new Object0004_DashRamp();
                        case 0x05: return new Object0005_Checkpoint();
                        case 0x07: return new Object0007_Case();
                        case 0x08: return new Object0008_Pulley();
                        case 0x09: return new Object0009_WoodBox();
                        case 0x0A: return new Object000A_MetalBox();
                        case 0x0B: return new Object000B_UnbreakableBox();
                        case 0x0C: return new Object000C_WeaponBox();
                        case 0x0E: return new Object000E_Rocket();
                        case 0x0F: return new Object000F_Platform();
                        case 0x10: return new Object0010_Ring();
                        case 0x11: return new Object0011_HintBall();
                        case 0x12: return new Object0012_ItemCapsule();
                        case 0x13: return new Object0013_Balloon();
                        case 0x14: return new Object0014_GoalRing();
                        case 0x15: return new Object0015_BallSwitch();
                        case 0x16: return new Object0016_TargetSwitch();
                        case 0x19: return new Object0019_Weight();
                        case 0x1A: return new Object001A_Wind();
                        case 0x1B: return new Object001B_Roadblock();
                        case 0x20: return new Object0020_Weapon();
                        case 0x23: return new Object0023_OverturnableObject();
                        case 0x3A: return new Object003A_SpecialWeaponBox();
                        case 0x33: return new Object0033_EnergyCore();
                        case 0x34: return new Object003X_UnusedMiscByteScaleType(); //Fire
                        case 0x35: return new Object003X_UnusedMiscByteScaleType(); //PoisonGas
                        case 0x37: return new Object003X_UnusedMiscByteScaleType(); //CaptureCage
                        case 0x4F: return new Object004F_Vehicle();
                        case 0x50: return new Object0050_Trigger();
                        case 0x51: return new Object0051_TriggerTalking();
                        case 0x59: return new Object0059_TriggerSkybox();
                        case 0x5A: return new Object005A_Pole();
                        case 0x61: return new Object0061_DarkSpinEntrance();
                        case 0x62: return new Object0062_LightColli();
                        case 0x64: return new Object0064_GUNSoldier();
                        case 0x65: return new Object0065_GUNBeetle();
                        case 0x66: return new Object0066_GUNBigfoot();
                        case 0x68: return new Object0068_GUNRobot();
                        case 0x78: return new Object0078_EggPierrot();
                        case 0x79: return new Object0079_EggPawn();
                        case 0x7A: return new Object007A_EggShadowAndroid();
                        case 0x8C: return new Object008C_BkGiant();
                        case 0x8D: return new Object008D_BkSoldier();
                        case 0x8E: return new Object008E_BkWingLarge();
                        case 0x8F: return new Object008F_BkWingSmall();
                        case 0x90: return new Object0090_BkWorm();
                        case 0x91: return new Object0091_BkLarva();
                        case 0x92: return new Object0092_BkChaos();
                        case 0x93: return new Object0093_BkNinja();
                        case 0x1F: default: return new Object_ShadowDefault(); // warp hole
                    }
                case 0x01:
                    switch (Type) {
                        case 0x2C: return new Object012C_EnvironmentalWeapon();
                        case 0x90: return new Object0190_Partner();
                        default: return new Object_ShadowDefault();
                    }
                case 0x03:
                    switch (Type) {
                        case 0xE9: return new Object03E9_FallingBuilding();
                        case 0xEA: return new Object03EA_GiantSkyLaser();
                        default: return new Object_ShadowDefault();
                    }
                case 0x07:
                    switch (Type) {
                        case 0xD1: return new Object07D1_Searchlight();
                        case 0xD2: return new Object07D2_ColorSwitch();
                        case 0xD3: return new Object07D3_RisingLaserBar();
                        case 0xD4: return new Object07D4_ElecSecurity();
                        case 0xD5: return new Object07D5_LightspeedRisingBlock();
                        case 0xD7: return new Object07D7_DigitalBreakableTile();
                        case 0xD8: return new Object07D8_LaserWallBarrier();
                        case 0xDA: return new Object07DA_MatrixTerminal1();
                        case 0xDE: return new Object00_SpringShadow();
                        case 0xDF: return new Object07DF_LightspeedFirewall();
                        case 0xE1: return new Object07E1_TriggerDigitalBreakableTile();
                        case 0xE2: return new Object07EX_SpinningElec(); //Spinning Dark Cube
                        case 0xE8: return new Object07EX_SpinningElec(); //Spinning Hexagon
                        case 0xEB: return new Object07EB_CubePlatformCircle();
                        default: return new Object_ShadowDefault();
                    }
                case 0x08:
                    switch (Type) {
                        case 0x34: return new Object0834_TornadoObject1();
                        case 0x35: return new Object0835_TornadoObject2();
                        case 0x36: return new Object0836_TornadoObject3();
                        case 0x37: return new Object0837_CollapsingPillar();
                        case 0x38: return new Object0838_RuinsStoneGuardian();
                        case 0x39: return new Object0839_RuinsJewel();
                        case 0x99: return new Object0899_BlackTankPathing();
                        case 0x9A: return new Object089A_BreakingRoad();
                        case 0x9C: return new Object089C_FallingRoad();
                        default: return new Object_ShadowDefault();
                    }
                case 0x0B:
                    switch (Type) {
                        case 0xBB: return new Object0BBB_SmallLantern();
                        case 0xBC: return new Object0BBC_PopupDummyGhost();
                        case 0xBE: return new Object0BBE_Chao();
                        case 0xC7: return new Object0BC7_CastleMonster1();
                        case 0xC8: return new Object0BC8_CastleMonster2();
                        default: return new Object_ShadowDefault();
                    }
                case 0x0C:
                    switch (Type) {
                        case 0x80: return new Object0C80_BouncyBall();
                        case 0x81: return new Object0C81_CircusGong();
                        case 0x82: return new Object0C82_GameBalloonsGhosts();
                        case 0x83: return new Object0C83_CircusGameTarget();
                        case 0x87: return new Object005A_Pole(); //CircusPole
                        case 0x88: return new Object0C88_Zipline();
                        case 0x89: return new Object0C89_TentCurtain();
                        default: return new Object_ShadowDefault();
                    }
                case 0x0F:
                    switch (Type) {
                        case 0xA1: return new Object0FA1_BAMiniBomb();
                        case 0xA2: return new Object0FA2_Helicopter();
                        case 0xA4: return new Object0FA4_BuildingChunk();
                        default: return new Object_ShadowDefault();
                    }
                case 0x10:
                    switch (Type) {
                        case 0x04: return new Object1004_ArkCrackedWall();
                        case 0x05: return new Object1005_Researcher();
                        case 0x06: return new Object1006_HealUnitServer();
                        case 0x69: return new Object1069_FleetHolderEggmanBattleship();
                        case 0x6C: return new Object106C_SkyRuinsJewel();
                        case 0x6D: return new Object106D_RainEffect();
                        default: return new Object_ShadowDefault();
                    }
                case 0x11:
                    switch (Type) {
                        case 0x30: return new Object1130_FenceWall();
                        case 0x31: return new Object1131_Vine();
                        case 0x32: return new Object1132_ElevatorPlatformColumn();
                        case 0x33: return new Object1133_ProximityDoor();
                        case 0x37: return new Object1137_StretchGrass();
                        case 0x38: return new Object1138_JumpPanel();
                        default: return new Object_ShadowDefault();
                    }
                case 0x13:
                    switch (Type) {
                        case 0x8A: return new Object138B_MeteorsHolder(); //138A Meteor
                        case 0x8B: return new Object138B_MeteorsHolder();
                        case 0x8E: return new Object138E_ArkCannon();
                        case 0x92: return new Object1392_SpaceDebris();
                        case 0xED: return new Object13ED_EscapePodPathSwitch();
                        case 0xEF: return new Object13EF_SecurityLaser();
                        case 0xF0: return new Object13F0_SlideoutPlatform();
                        case 0xF1: return new Object13F1_HeavyBaseDoor();
                        case 0xF2: return new Object13F2_EscapePodPathing();
                        default: return new Object_ShadowDefault();
                    }
                case 0x14:
                    switch (Type) {
                        case 0x51: return new Object1451_CommandCollision();
                        case 0xB4: return new Object14B5_GravityChangeZone(); //GravityChangeSwitch
                        case 0xB5: return new Object14B5_GravityChangeZone();
                        case 0xB6: return new Object14B6_GravityChangeCollision();
                        case 0xBE: return new Object14BE_ArkGreenLaser();
                        case 0xED: return new Object13ED_EscapePodPathSwitch();
                        default: return new Object_ShadowDefault();
                    }
                case 0x17:
                    switch (Type) {
                        case 0x70: return new Object1770_GUNCamera();
                        case 0x72: return new Object1772_ConcreteDoor();
                        case 0x73: return new Object1773_CrushingWalls();
                        case 0xD4: return new Object11D4_BAGunShip();
                        case 0xD5: return new Object17D5_BlackArmsMine();
                        default: return new Object_ShadowDefault();
                    }
                case 0x18:
                    switch (Type) {
                        case 0x39: return new Object1839_RisingLava();
                        case 0x9E: return new Object189E_ARKDriftingPlat1();
                        case 0x9F: return new Object189F_ArkRollPlatform();
                        default: return new Object_ShadowDefault();
                    }
                case 0x19:
                    switch (Type) {
                        case 0x01: return new Object1901_CometBarrier();
                        case 0x03: return new Object1903_BlackDoomHologram();
                        default: return new Object_ShadowDefault();
                    }
                case 0x25:
                    switch (Type) {
                        case 0x88: return new Object2588_Decoration1();
                        case 0x89: return new Object2589_Destructable1();
                        case 0x8A: return new Object258A_Effect1();
                        case 0x90: return new Object2588_Decoration1();
                        case 0x91: return new Object2589_Destructable1();
                        case 0x93: return new Object2593_SetGenerator();
                        case 0x94: return new Object2594_Fan();
                        case 0x95: return new Object2595_MissionClearCollision();
                        case 0x97: return new Object2597_SetSeLoop();
                        case 0x98: return new Object2598_SetSeOneShot();
                        default: return new Object_ShadowDefault();
                    }
                default: return new Object_ShadowDefault();
            }
        }

        public static List<SetObjectHeroes> GetHeroesLayoutFromShadow(string fileName)
        {
            List<SetObjectHeroes> list = new List<SetObjectHeroes>();

            foreach (SetObjectShadow i in GetShadowLayout(fileName))
            {
                SetObjectHeroes TempObject = SetObjectShadowToHeroes(i);

                if (TempObject != null)
                {
                    TempObject.CreateTransformMatrix();
                    list.Add(TempObject);
                }
            }

            return list;
        }

        private static SetObjectHeroes SetObjectShadowToHeroes(SetObjectShadow i)
        {
            bool MatchNotFound = false;
            byte List = 0;
            byte Type = 0;
            byte[] MiscSettings = new byte[36];

            switch (i.List)
            {
                case 0:
                    List = 0;
                    if (i.Type == 0x00) Type = 0x03; // Ring
                    else if (i.Type == 0x01) Type = 0x01; // Spring
                    else if (i.Type == 0x02) Type = 0x02; // 3Spring
                    else if (i.Type == 0x03) Type = 0x0B; // Dash panel
                    else if (i.Type == 0x04) Type = 0x0F; // Dash ramp
                    else if (i.Type == 0x05) Type = 0x0E; // Checkpoint
                    else if (i.Type == 0x06) Type = 0x0C; // Dash Ring
                    else if (i.Type == 0x07) Type = 0x31; // Case
                    else if (i.Type == 0x08) Type = 0x1D; // Pulley
                    else if (i.Type == 0x09) Type = 0x20; // Gun Wood box
                    else if (i.Type == 0x0A) Type = 0x21; // Metal box
                    else if (i.Type == 0x0B) Type = 0x22; // Unbreakable box
                    else if (i.Type == 0x0C) Type = 0x20; // Normal Wood box
                    else if (i.Type == 0x0F) // Moving platform
                    {
                        // List = 0x13;
                        // Type = 0x06; // Egg fleet square platform
                        List = 0x5;
                        Type = 0xA; // dice
                    }
                    else if (i.Type == 0x10)  // Ring
                    {
                        Type = 0x03;

                        MiscSettings[5] = i.MiscSettings[0];
                        MiscSettings[4] = i.MiscSettings[1];

                        MiscSettings[7] = i.MiscSettings[4];
                        MiscSettings[6] = i.MiscSettings[5];

                        MiscSettings[11] = i.MiscSettings[8];
                        MiscSettings[10] = i.MiscSettings[9];
                        MiscSettings[9] = i.MiscSettings[10];
                        MiscSettings[8] = i.MiscSettings[11];

                        MiscSettings[15] = i.MiscSettings[12];
                        MiscSettings[14] = i.MiscSettings[13];
                        MiscSettings[13] = i.MiscSettings[14];
                        MiscSettings[12] = i.MiscSettings[15];

                        if (i.MiscSettings[0] == 1)
                            i.Rotation = new Vector3(i.Rotation.X, i.Rotation.Y + 180, i.Rotation.Z);
                    }
                    else if (i.Type == 0x11) Type = 0x04; // Hint
                    else if (i.Type == 0x12) Type = 0x18; // Item balloon
                    else if (i.Type == 0x13) Type = 0x19; // Item balloon
                    else if (i.Type == 0x14) Type = 0x1B; // Goal ring
                    else if (i.Type == 0x15) Type = 0x05; // Switch
                                                                      // else if (i.Type == 0x1D) Type = 0x67; // Key
                    else if (i.Type == 0x1F) Type = 0x80; // Teleport
                    else if (i.Type == 0x3A) Type = 0x24; // shadow box
                    else if (i.Type == 0x65) // Beetle
                    {
                        List = 0x15;
                        Type = 0x00;
                    }
                    else if (i.Type == 0x78) // Egg bommer
                    {
                        List = 0x15;
                        Type = 0x20; //Klagen
                    }
                    else if (i.Type == 0x79 | i.Type == 0x8D) // Egg pawn
                    {
                        List = 0x15;
                        Type = 0x10;
                    }
                    else if (i.Type == 0x7A | i.Type == 0x90) // Shadow android / worm
                    {
                        List = 0x15;
                        Type = 0x70; // Cameron
                    }
                    else
                        MatchNotFound = true;
                    break;
                case 0x07:
                    if (i.Type == 0xD1) // Digital searchlight
                    {
                        List = 0x00;
                        Type = 0x2E; // Fan
                        i.Rotation = new Vector3();
                    }
                    else if (i.Type == 0xD5) // Digital big block
                    {
                        float scale = BitConverter.ToSingle(i.MiscSettings, 4);
                        if (BitConverter.ToSingle(i.MiscSettings, 8) < scale)
                            scale = BitConverter.ToSingle(i.MiscSettings, 8);
                        if (BitConverter.ToSingle(i.MiscSettings, 12) < scale)
                            scale = BitConverter.ToSingle(i.MiscSettings, 12);

                        if (i.MiscSettings[0] < 9)
                        {
                            List = 0x01;
                            Type = 0x80; // Flower

                            MiscSettings[4] = i.MiscSettings[0];

                            MiscSettings[8] = BitConverter.GetBytes(scale)[3];
                            MiscSettings[9] = BitConverter.GetBytes(scale)[2];
                            MiscSettings[10] = BitConverter.GetBytes(scale)[1];
                            MiscSettings[11] = BitConverter.GetBytes(scale)[0];
                        }
                        else
                        {
                            List = 0x01;
                            Type = 0x88; // stone

                            MiscSettings[4] = BitConverter.GetBytes(scale)[3];
                            MiscSettings[5] = BitConverter.GetBytes(scale)[2];
                            MiscSettings[6] = BitConverter.GetBytes(scale)[1];
                            MiscSettings[7] = BitConverter.GetBytes(scale)[0];
                        }
                    }
                    else if (i.Type == 0xD7) // Digital panel
                    {
                        List = 0x05;
                        Type = 0x05; // Casino panel

                        if (i.MiscSettings[0] == 1)
                        {
                            if (i.MiscSettings[0] == 1)
                                i.Rotation = new Vector3(i.Rotation.X - 90, i.Rotation.Y, i.Rotation.Z);
                        }
                        else
                        {
                            MiscSettings[4] = BitConverter.GetBytes(5f)[3];
                            MiscSettings[5] = BitConverter.GetBytes(5f)[2];
                            MiscSettings[6] = BitConverter.GetBytes(5f)[1];
                            MiscSettings[7] = BitConverter.GetBytes(5f)[0];
                        }
                    }
                    else if (i.Type == 0xDB) // Digital core
                    {
                        List = 0x00;
                        Type = 0x1B; // Goal
                    }
                    else if (i.Type == 0xDE) // Digital spring
                    {
                        List = 0x05;
                        Type = 0x01; // Green spring

                        MiscSettings[4] = BitConverter.GetBytes(4f)[3];
                        MiscSettings[5] = BitConverter.GetBytes(4f)[2];
                        MiscSettings[6] = BitConverter.GetBytes(4f)[1];
                        MiscSettings[7] = BitConverter.GetBytes(4f)[0];
                    }
                    else if (i.Type == 0xDF) // Firewall
                    {
                        List = 0x00;
                        Type = 0x15; // Spikeball

                        MiscSettings[8] = BitConverter.GetBytes(2f)[3];
                        MiscSettings[9] = BitConverter.GetBytes(2f)[2];
                        MiscSettings[10] = BitConverter.GetBytes(2f)[1];
                        MiscSettings[11] = BitConverter.GetBytes(2f)[0];
                        MiscSettings[12] = BitConverter.GetBytes(2f)[3];
                        MiscSettings[13] = BitConverter.GetBytes(2f)[2];
                        MiscSettings[14] = BitConverter.GetBytes(2f)[1];
                        MiscSettings[15] = BitConverter.GetBytes(2f)[0];
                    }
                    else if (i.Type == 0xE2) // Spinning cube dark
                    {
                        List = 0x05;
                        Type = 0x84; // Dice big
                    }
                    else if (i.Type == 0xE5) // Spinning cube PURPLE
                    {
                        List = 0x05;
                        Type = 0x84; // Dice big
                        MiscSettings[7] = 1;
                    }
                    else
                        MatchNotFound = true;
                    break;
                case 0x0B:
                    if (i.Type == 0xBB | i.Type == 0xC9) // small lantern, castle file
                    {
                        List = 0x11;
                        Type = 0x04; // flame torch
                    }
                    else
                        MatchNotFound = true;
                    break;
                case 0x0C:
                    if (i.Type == 0xDE) // bouncy ball
                    {
                        List = 0x05;
                        Type = 0x01; // Green spring

                        MiscSettings[4] = BitConverter.GetBytes(4f)[3];
                        MiscSettings[5] = BitConverter.GetBytes(4f)[2];
                        MiscSettings[6] = BitConverter.GetBytes(4f)[1];
                        MiscSettings[7] = BitConverter.GetBytes(4f)[0];
                    }
                    else if (i.Type == 0x82) // ghost
                    {
                        List = 0x11;
                        Type = 0x05; // ghost
                    }
                    else if (i.Type == 0x88) // zipline balloon
                    {
                        List = 0x11;
                        Type = 0x00; // teleport switch
                    }
                    else
                        MatchNotFound = true;
                    break;
                case 0x11:
                    if (i.Type == 0x33) // Castle door
                    {
                        List = 0x11;
                        Type = 0x01; // castle door
                    }
                    else
                        MatchNotFound = true;
                    break;
                case 0x13:
                    if (i.Type == 0xF1) // Lava shelter door
                    {
                        List = 0x07;
                        Type = 0x06; // Rail canyon door
                    }
                    else if (i.Type == 0xEF) // LASER
                    {
                        List = 0x00;
                        Type = 0x16;
                    }
                    else
                        MatchNotFound = true;
                    break;
                default:
                    MatchNotFound = true;
                    break;
            }

            if (MatchNotFound) return null;
            else return CreateHeroesObject(List, Type, i.Position,
                new Vector3(DegreesToBAMS(i.Rotation.X), DegreesToBAMS(i.Rotation.Y), DegreesToBAMS(i.Rotation.Z)), 0, (byte)(2 * i.Rend));
        }
    }
}
