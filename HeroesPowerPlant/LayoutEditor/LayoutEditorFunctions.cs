using HeroesPowerPlant.Shared.Utilities;
using SharpDX;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using static HeroesPowerPlant.ReadWriteCommon;

namespace HeroesPowerPlant.LayoutEditor
{
    public static class LayoutEditorFunctions
    {
        private const int HeroesObjectCount = 2048;

        public static List<SetObjectHeroes> GetHeroesLayout(string fileName, out string result)
        {
            result = "";
            int count = 0;
            var list = new List<SetObjectHeroes>(HeroesObjectCount);

            using (var reader = new EndianBinaryReader(new FileStream(fileName, FileMode.Open), Endianness.Big))
                for (int i = 0; i < HeroesObjectCount; i++)
                {
                    reader.BaseStream.Position = 0x30 * i;
                    if (reader.EndOfStream)
                        break;

                    var pos = new Vector3(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
                    var rot = new Vector3(reader.ReadInt32(), reader.ReadInt32(), reader.ReadInt32());

                    var unkBytes1 = reader.ReadBytes(4);
                    reader.BaseStream.Position += 4;

                    var unkBytes2 = reader.ReadBytes(4);
                    reader.BaseStream.Position += 4;

                    byte List = reader.ReadByte();
                    byte Type = reader.ReadByte();
                    byte Link = reader.ReadByte();
                    byte Rend = reader.ReadByte();

                    if (List == 0 & Type == 0)
                        continue;

                    count++;

                    var obj = CreateHeroesObject(List, Type, pos, rot, Link, Rend, unkBytes1.Concat(unkBytes2).ToArray(), false);

                    int miscSettingsPos = reader.ReadInt32();

                    if (miscSettingsPos == 0)
                        obj.HasMiscSettings = false;
                    else
                    {
                        reader.BaseStream.Position = 0x18000 + (0x24 * miscSettingsPos) + 4;
                        obj.ReadMiscSettings(reader);
# if DEBUG
                        reader.BaseStream.Position = 0x18000 + (0x24 * miscSettingsPos) + 4;
                        var miscSettingsForTest = reader.ReadBytes(32);
                        result += CheckMiscSettingsBuilder(miscSettingsForTest, obj, count);
#endif
                    }

                    obj.CreateTransformMatrix();

                    list.Add(obj);
                }
            return list;
        }

        public static void SaveHeroesLayout(IEnumerable<SetObjectHeroes> list, string outputFile, bool autoValues)
        {
            using var writer = new EndianBinaryWriter(new FileStream(outputFile, FileMode.Create), Endianness.Big);
            byte currentNum;

            if (outputFile.Contains("P1")) currentNum = 0x20;
            else if (outputFile.Contains("P2")) currentNum = 0x40;
            else if (outputFile.Contains("P3")) currentNum = 0x60;
            else if (outputFile.Contains("P4")) currentNum = 0x80;
            else if (outputFile.Contains("P5")) currentNum = 0xA0;
            else currentNum = 0;

            int i = 0;
            short j = 1;

            foreach (SetObjectHeroes obj in list)
            {
                if (obj.List == 0 & obj.Type == 0)
                    continue;

                writer.BaseStream.Position = 0x30 * i;

                writer.Write(obj.Position.X);
                writer.Write(obj.Position.Y);
                writer.Write(obj.Position.Z);
                writer.Write((int)obj.Rotation.X);
                writer.Write((int)obj.Rotation.Y);
                writer.Write((int)obj.Rotation.Z);

                if (autoValues || obj.UnkBytes.Length != 8)
                    obj.UnkBytes = new byte[] { 0, 2, currentNum, 9, 0, 2, currentNum, 9 };

                writer.Write(obj.UnkBytes.Take(4).ToArray());
                writer.Write(0);
                writer.Write(obj.UnkBytes.Skip(4).ToArray());
                writer.Write(0);

                writer.Write(obj.List);
                writer.Write(obj.Type);
                writer.Write(obj.Link);
                writer.Write(obj.Rend);

                if (obj.HasMiscSettings)
                {
                    writer.Write((int)j);

                    writer.BaseStream.Position = 0x18000 + (0x24 * j);

                    var sBytes = BitConverter.GetBytes(j);

                    writer.Write((byte)1);
                    writer.Write((byte)0);
                    writer.Write(sBytes);
                    
                    obj.WriteMiscSettings(writer);

                    j++;
                }
                else
                    writer.Write(0);

                i++;
            }
        }

        public static List<SetObjectShadow> GetShadowLayout(string fileName, out string result)
        {
            result = "";
            using var reader = new BinaryReader(new FileStream(fileName, FileMode.Open));

            var magic = new string(reader.ReadChars(4));
            if (magic != "sky2")
            {
                System.Windows.Forms.MessageBox.Show("This is not a valid Shadow the Hedgehog layout file.");
                throw new InvalidDataException("This is not a valid Shadow the Hedgehog layout file.");
            }

            int count = reader.ReadInt32();
            var list = new List<SetObjectShadow>(count);
            var miscCountList = new List<int>(count);
            var totalMiscLength = reader.ReadUInt32();

            for (int i = 0; i < count; i++)
            {
                reader.BaseStream.Position = 12 + i * 0x2C;
                if (reader.BaseStream.Position >= reader.BaseStream.Length)
                    break;

                var pos = new Vector3(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
                var rot = new Vector3(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());

                byte[] UnkBytes = reader.ReadBytes(8);

                byte Type = reader.ReadByte();
                byte List = reader.ReadByte();
                byte Link = reader.ReadByte();
                byte Rend = reader.ReadByte();
                miscCountList.Add(reader.ReadInt32());

                list.Add(CreateShadowObject(List, Type, pos, rot, Link, Rend, UnkBytes, false));
            }

            reader.BaseStream.Position = 12 + count * 0x2C;
            for (int i = 0; i < count; i++)
            {
                list[i].ReadMiscSettings(reader, miscCountList[i]);
#if DEBUG
                reader.BaseStream.Position -= miscCountList[i];
                var miscSettingsForTest = reader.ReadBytes(miscCountList[i]);
                result += CheckMiscSettingsBuilder(miscSettingsForTest, list[i], i+1);
#endif
                list[i].CreateTransformMatrix();
            }

            return list;
        }

        public static void SaveShadowLayout(IEnumerable<SetObjectShadow> list, string outputFile, bool autoValues)
        {
            byte currentNum = 0;

            if (outputFile.ToLower().Contains("cmn"))
                currentNum = 0x10;
            else if (outputFile.ToLower().Contains("nrm"))
                currentNum = 0x20;
            else if (outputFile.ToLower().Contains("hrd"))
                currentNum = 0x40;
            else if (outputFile.ToLower().Contains("ds1"))
                currentNum = 0x80;

            using var writer = new BinaryWriter(new FileStream(outputFile, FileMode.Create));
            using var miscSettingsWriter = new BinaryWriter(new MemoryStream());

            writer.Write(new byte[] { 0x73, 0x6B, 0x79, 0x32 });
            writer.Write(list.Count());
            writer.Write(0);

            foreach (var obj in list)
            {
                var miscSettingsCount = -miscSettingsWriter.BaseStream.Position;
                obj.WriteMiscSettings(miscSettingsWriter);
                miscSettingsCount += miscSettingsWriter.BaseStream.Position;

                writer.Write(obj.Position.X);
                writer.Write(obj.Position.Y);
                writer.Write(obj.Position.Z);
                writer.Write(obj.Rotation.X);
                writer.Write(obj.Rotation.Y);
                writer.Write(obj.Rotation.Z);

                if (autoValues || obj.UnkBytes.Length != 8)
                {
                    var unkBytes = new List<byte>() { 1, currentNum };
                    unkBytes.AddRange(currentNum == 0x80 ? new byte[] { 0x40, 0x80 } : new byte[] { 0, 0x80 });
                    unkBytes.AddRange(new byte[] { 1, currentNum });
                    unkBytes.AddRange(currentNum == 0x80 ? new byte[] { 0x40, 0x80 } : new byte[] { 0, 0 });

                    obj.UnkBytes = unkBytes.ToArray();
                }

                writer.Write(obj.UnkBytes);

                writer.Write(obj.Type);
                writer.Write(obj.List);
                writer.Write(obj.Link);
                writer.Write(obj.Rend);
                writer.Write((int)miscSettingsCount);
                writer.Write(0);
            }

            writer.Write(((MemoryStream)miscSettingsWriter.BaseStream).ToArray());

            writer.BaseStream.Position = 8;
            writer.Write((int)miscSettingsWriter.BaseStream.Length);
        }

        public static List<SetObjectHeroes> GetObjectsFromObjFile(string FileName, byte List, byte Type)
        {
            var list = new List<SetObjectHeroes>();

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
            var list = new List<SetObjectHeroes>();

            SetObjectHeroes temp = CreateHeroesObject(0, 0, Vector3.Zero, Vector3.Zero, 0, 10, new byte[8], false);

            foreach (string s in file)
            {
                if (s.StartsWith("obj "))
                {
                    if (!(temp.List == 0 & temp.Type == 0))
                    {
                        temp.CreateTransformMatrix();
                        list.Add(temp);
                    }
                    temp = null;
                    temp = CreateHeroesObject(Convert.ToByte(s.Substring(4, 2), 16), Convert.ToByte(s.Substring(6, 2), 16), Vector3.Zero, Vector3.Zero, 0, 10, createMatrix: false);
                }
                else if (s.StartsWith("link "))
                {
                    temp.Link = Convert.ToByte(s.Substring(5, 2));
                }
                else if (s.StartsWith("rend "))
                {
                    temp.Rend = Convert.ToByte(s.Substring(5, 2));
                }
                else if (s.StartsWith("v "))
                {
                    string[] j = s.Split(' ');
                    temp.Position = new Vector3(Convert.ToSingle(j[1]), Convert.ToSingle(j[2]), Convert.ToSingle(j[3]));
                }
                else if (s.StartsWith("r "))
                {
                    string[] j = s.Split(' ');
                    temp.Rotation = new Vector3(Convert.ToSingle(j[1]), Convert.ToSingle(j[2]), Convert.ToSingle(j[3]));
                }
                else if (s.StartsWith("b "))
                {
                    string[] j = s.Split(' ');
                    temp.UnkBytes[0] = Convert.ToByte(j[1]);
                    temp.UnkBytes[1] = Convert.ToByte(j[2]);
                    temp.UnkBytes[2] = Convert.ToByte(j[3]);
                    temp.UnkBytes[3] = Convert.ToByte(j[4]);
                    temp.UnkBytes[4] = Convert.ToByte(j[5]);
                    temp.UnkBytes[5] = Convert.ToByte(j[6]);
                    temp.UnkBytes[6] = Convert.ToByte(j[7]);
                    temp.UnkBytes[7] = Convert.ToByte(j[8]);
                }
                else if (s.StartsWith("misc "))
                {
                    if (s.Length == 9 * 9 + 4)
                    {
                        string newMiscString = Regex.Replace(s[5..], @"\s+", "");
                        var miscSettings = new List<byte>();
                        for (int j = 0; j < newMiscString.Length; j += 2)
                        {
                            var byteasstring = new string(new char[] { newMiscString[j], newMiscString[j + 1] });
                            miscSettings.Add(Convert.ToByte(byteasstring, 16));
                        }
                        temp.SetMiscSettings(miscSettings.ToArray());
                    }
                    else
                    {
                        temp.HasMiscSettings = false;
                    }
                }
                else if (s == "EndOfFile")
                {
                    if (!(temp.List == 0 & temp.Type == 0))
                    {
                        temp.CreateTransformMatrix();
                        list.Add(temp);
                    }
                    temp = null;
                }
            }

            return list;
        }

        public static void SaveHeroesLayoutINI(IEnumerable<SetObjectHeroes> list, string outputFile)
        {
            var iniWriter = new StreamWriter(new FileStream(outputFile, FileMode.Create));
            iniWriter.WriteLine("#Exported by HeroesPowerPlant");
            iniWriter.WriteLine("#Heroes Layout Editor INI");
            iniWriter.WriteLine();

            foreach (var obj in list)
            {
                iniWriter.WriteLine("obj "
                    + String.Format("{0, 2:X2}", obj.List)
                    + String.Format("{0, 2:X2}", obj.Type)
                    + "_" + obj.GetName.Replace(' ', '-'));
                iniWriter.WriteLine("link " + String.Format("{0, 2:D2}", obj.Link));
                iniWriter.WriteLine("rend " + String.Format("{0, 2:D2}", obj.Rend));
                iniWriter.WriteLine("b " +
                    obj.UnkBytes[0].ToString() + " " + obj.UnkBytes[1].ToString() + " " + obj.UnkBytes[2].ToString() + " " + obj.UnkBytes[3].ToString() + " " +
                    obj.UnkBytes[4].ToString() + " " + obj.UnkBytes[5].ToString() + " " + obj.UnkBytes[6].ToString() + " " + obj.UnkBytes[7].ToString());
                iniWriter.WriteLine("v " + obj.Position.X.ToString() + " " + obj.Position.Y.ToString() + " " + obj.Position.Z.ToString());
                iniWriter.WriteLine("r " + obj.Rotation.X.ToString() + " " + obj.Rotation.Y.ToString() + " " + obj.Rotation.Z.ToString());
                if (obj.HasMiscSettings)
                {
                    var miscSettings = obj.GetMiscSettings();
                    var m = new List<char>();
                    for (int j = 0; j < miscSettings.Length; j++)
                    {
                        if (j % 4 == 0) m.Add(' ');
                        m.AddRange(string.Format("{0, 2:X2}", miscSettings[j]).ToCharArray());
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
            string ModelMiscSetting = "";
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
                    ModelMiscSetting = i.Split('=')[1];
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
                    ModelMiscSetting = "";
                }
            }

            return list;
        }

        private static string CheckMiscSettingsBuilder(byte[] miscSettingsForTest, SetObject obj, int index)
        {
            byte[] newMiscSettings = null;
            if (obj is SetObjectHeroes objHeroes)
            {
                using var writer = new EndianBinaryWriter(new MemoryStream(), Endianness.Big);
                objHeroes.WriteMiscSettings(writer);
                while (writer.BaseStream.Length < 32)
                    writer.Write((byte)0);
                newMiscSettings = ((MemoryStream)writer.BaseStream).ToArray();
            }
            else if (obj is SetObjectShadow objShadow)
            {
                using var writer = new BinaryWriter(new MemoryStream());
                objShadow.WriteMiscSettings(writer);
                newMiscSettings = ((MemoryStream)writer.BaseStream).ToArray();
            }

            var og = "";
            foreach (var i in miscSettingsForTest)
                og += i.ToString("X2") + " ";
            var ne = "";
            foreach (var i in newMiscSettings)
                ne += i.ToString("X2") + " ";

            if (!Enumerable.SequenceEqual(miscSettingsForTest, newMiscSettings))
                return $"{obj} [{index}]\norig: {og}\nnews: {ne}\n";
            return "";
        }

        public static SetObjectHeroes CreateHeroesObject
            (byte List, byte Type, Vector3 Position, Vector3 Rotation, byte Link, byte Rend, byte[] UnkBytes = null, bool createMatrix = true)
        {
            var objEntry = LayoutEditorSystem.heroesObjectEntry(List, Type);
            SetObjectHeroes heroesObj = objEntry.HasMiscSettings ? FindObjectClassHeroes(List, Type) : new SetObjectHeroes();
            heroesObj.Position = Position;
            heroesObj.Rotation = Rotation;
            heroesObj.List = List;
            heroesObj.Type = Type;
            heroesObj.Link = Link;
            heroesObj.Rend = Rend;
            heroesObj.UnkBytes = UnkBytes ?? new byte[8];
            heroesObj.SetObjectEntry(objEntry);
            if (createMatrix)
                heroesObj.CreateTransformMatrix();

            return heroesObj;
        }

        private static SetObjectHeroes FindObjectClassHeroes(byte List, byte Type)
        {
            return List switch
            {
                0 => Type switch
                {
                    0 => new Object0000_Nothing(),
                    0x1B or 0x28 or 0x67 => new SetObjectHeroes(),
                    0x1 => new Object0001_Spring(),
                    0x2 => new Object0002_TripleSpring(),
                    0x3 => new Object0003_Ring(),
                    0x4 => new Object0004_HintRing(),
                    0x5 => new Object0005_Switch(),
                    0x6 => new Object0006_SwitchPP(),
                    0x7 => new Object0007_Target(),
                    0xB => new Object000B_DashPanel(),
                    0xC => new Object000C_DashRing(),
                    0xD => new Object000D_BigRings(),
                    0xE => new Object000E_Checkpoint(),
                    0xF => new Object000F_DashRamp(),
                    0x10 => new Object0010_Cannon(),
                    0x13 or 0x14 => new Object00_Weight(),
                    0x15 => new Object0015_SpikeBall(),
                    0x16 => new Object0016_LaserFence(),
                    0x18 => new Object0018_ItemBox(),
                    0x19 => new Object0019_ItemBalloon(),
                    0x1D => new Object001D_Pulley(),
                    0x20 or 0x21 or 0x22 => new Object00_Box(),
                    0x23 => new Object0023_Chao(),
                    0x24 => new Object0024_Cage(),
                    0x25 => new Object0025_FormSign(),
                    0x26 => new Object0026_FormGate(),
                    0x29 => new Object0029_Pole(),
                    0x2C => new Object002C_RollDoor(),
                    0x2E => new Object002E_Fan(),
                    0x31 => new Object0031_Case(),
                    0x32 => new Object0032_WarpFlower(),
                    // invisible collision
                    0x50 or 0x61 or 0x82 => new Object00_TriggerCommon(),
                    0x56 => new Object0056_TriggerTalk(),
                    0x59 => new Object0059_TriggerLight(),
                    0x60 => new Object0060_TriggerRhinoLiner(),
                    //case 0x62: return new Object0062_TriggerEggHalk();
                    //case 0x63: return new Object0063_TriggerFalco();
                    0x64 => new Object0064_TriggerHurt(),// damage collision
                                                         //case 0x65: return new Object0065_TriggerKlagen();
                    0x66 => new Object0066_TriggerBobJump(),
                    0x80 => new Object0080_TriggerTeleport(),
                    0x81 => new Object0081_TriggerSE(),
                    _ => new Object_HeroesDefault(),
                },
                1 => Type switch
                {
                    0x2 => new Object0102_TruckRail(),
                    0x3 => new Object0103_TruckPath(),
                    0x4 => new SetObjectHeroes(),
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
                },
                2 => Type switch
                {
                    0x0 => new Object0200_CrumbleStonePillar(),
                    //case 0x1: return new Object_();
                    0x2 => new Object_F1Scale(),
                    0x3 => new Object_B1_1_Type(),
                    0x4 => new Object0204_Kaos(),
                    //case 0x5: return new Object0205_ScrollRing();
                    0x6 => new Object0206_ScrollBalloon(),
                    0xA => new Object020A_ColliQuake(),
                    0xB => new Object020B_EventActivator(),
                    0xC => new Object020C_TriggerKaos(),
                    //case 0x80: return new Object0280_MovingLand();
                    0x81 => new Object0281_TurtleFeet(),
                    0x82 => new Object0282_KameWave(),
                    0x83 or 0x84 or 0x85 => new Object_IntTypeFloatScale(),
                    _ => new Object_HeroesDefault(),
                },
                3 => Type switch
                {
                    0 => new Object0300_AcceleratorRoad(),
                    0x2 => new Object0302_RoadCap(),
                    0x3 or 0x4 => new SetObjectHeroes(),
                    0x5 => new Object0305_BigBridge(),
                    0x6 => new Object0306_AirCar(),
                    0x7 or 0x81 or 0x82 => new Object_XYZScale(0),
                    0x8 => new Object0308_Accelerator(),
                    0x80 => new Object0380_BalloonDesign(),
                    //case 0x82: return new Object0382_Train();
                    //case 0x83: return new Object0383_PipeDesign();
                    _ => new Object_HeroesDefault(),
                },
                4 => Type switch
                {
                    0x80 or 0x81 => new SetObjectHeroes(),
                    0x1 => new Object0401_EnergyColumn(),
                    0x3 or 0x8 or 0x10 => new Object_B1_1_Type(),
                    0x82 or 0x84 => new Object04_CraneWallLight(),
                    0x83 => new Object_F1Speed(),
                    0x85 => new Object_XYZScale(1),
                    _ => new Object_HeroesDefault(),
                },
                5 => Type switch
                {
                    0x0 or 0x1 => new Object05_Spring(),
                    0x2 => new Object0502_Flipper(),
                    0x3 => new Object0503_TriBumper(),
                    0x4 or 0x5 => new Object05_StarPanel(),
                    0x7 or 0xD or 0x10 => new Object_F1Scale(),
                    0x8 or 0x9 or 0x81 or 0x82 => new Object_L1Type(),
                    0x0A => new Object050A_Dice(),
                    0x0B => new Object050B_Slot(),
                    0x84 => new Object0584_GiantDice(),
                    0x86 => new Object0586_Roulette(),
                    0x87 => new Object0587_GiantCasinoChip(),
                    0x88 => new SetObjectHeroes(),
                    _ => new Object_HeroesDefault(),
                },
                7 => Type switch
                {
                    0x00 or 0x01 or 0x0A or 0x1B or 0x42 or 0x82 or 0x83 or 0x85 or 0x86 or 0x88 or 0x89 or 0x8A or 0x8B or 0x8C or 0x8D or 0x8E or 0x8F or 0x90 or 0x91 or 0x92 or 0x93 or 0x98 => new SetObjectHeroes(),
                    0x03 or 0x80 => new Object_F1Speed(),
                    0x04 => new Object0704_RailRoadblock(),
                    0x05 => new Object0705_Capsule(),
                    0x06 => new Object_L1Type(),
                    0x43 => new Object_F1Range(),
                    0x81 => new Object_F1Scale(),
                    0x87 => new Object_XYZScale(11),
                    0x94 => new Object_F1Range(),
                    0x96 => new Object_F1Scale(),
                    0x97 => new Object_L1Type(),
                    _ => new Object_HeroesDefault(),
                },
                8 => Type switch
                {
                    0x0 => new Object_F1Speed(),
                    0x2 => new Object_L1Offset(),
                    0x3 or 0x4 => new Object_L1Type(),
                    _ => new Object_HeroesDefault(),
                },
                9 => Type switch
                {
                    0x0 => new Object0900_Frog(),
                    0x2 => new Object0902_RainLeaf(),
                    0x5 or 0x7 => new Object_F1Range(),
                    0x3 => new Object0903_RainMush(),
                    0x4 => new Object0904_RainIvy(),
                    0x6 or 0x10 or 0x11 or 0x12 or 0x98 => new SetObjectHeroes(),
                    0x8 => new Object0908_RainFruit(),
                    0x9 => new Object000B_DashPanel(),
                    0xB => new Object090B_IvyJump(),
                    0xC or 0x85 or 0x86 or 0x8B or 0x8C or 0x8D or 0x91 or 0x92 or 0x93 or 0x95 => new Object_F1Scale(),
                    0xD => new Object_XYZScale(0),
                    0xE => new Object090E_Alligator(),
                    0x13 => new Object0913_RainCollision(),
                    0x80 => new Object0980_Butterfly(),
                    0x81 => new Object0981_Flower(),
                    0x82 or 0x83 or 0x87 or 0x88 or 0x8A => new Object_IntTypeFloatScale(),
                    0x84 => new Object0984_RedWeed(),
                    0x89 => new Object0989_Pond(),
                    0x97 => new Object0987_RedGreenPlant(),
                    0x99 => new Object0999_Powder(),
                    0x9A => new Object099A_Wanibreak(),
                    _ => new Object_HeroesDefault(),
                },
                0x11 => Type switch
                {
                    0x0 => new Object1100_TeleportSwitch(),
                    0x1 => new Object1101_CastleDoor(),
                    0x2 => new Object1102_CastleWall(),
                    0x3 or 0x6 => new Object11_FloatingPlatform(),
                    0x4 => new Object1104_FlameTorch(),
                    0x5 => new Object1105_Ghost(),
                    0x7 or 0x82 or 0x89 => new Object_L1Type(),
                    0x8 => new Object1108_MansionDoor(),
                    0x9 => new SetObjectHeroes(),
                    0xA => new SetObjectHeroes(),
                    0xB => new Object_F1Range(),
                    0xC => new Object110C_TriggerMusic(),
                    0x80 => new Object_IntTypeFloatScale(),
                    0x81 => new Object1181_Celestial(),
                    0x83 => new Object_F1Range(),
                    0x84 => new Object1184_SmokeScreen(),
                    0x85 => new Object1185_Bone(),
                    0x88 => new Object1188_Curtain(),
                    _ => new Object_HeroesDefault(),
                },
                0x13 => Type switch
                {
                    0x2 => new Object1302_HorizCannon(),
                    0x3 => new Object1303_MovingCannon(),
                    0x4 => new Object1304_RectFloatingPlatform(),
                    0x5 => new Object1305_EggFleetDoor(),
                    0x8 or 0x80 => new Object_F1Speed(),
                    0x20 => new Object_B1_1_Type(),
                    0x07 or 0x81 or 0x82 or 0x83 or 0x85 or 0x86 or 0x87 or 0x88 or 0x89 or 0x8A or 0x8B or 0x8C or 0x8D or 0x8E or 0x8F or 0x90 or 0x91 or 0x92 or 0x93 or 0x94 => new SetObjectHeroes(),
                    _ => new Object_HeroesDefault(),
                },
                0x14 => Type switch
                {
                    _ => new Object_HeroesDefault(),
                },
                0x15 => Type switch
                {
                    0 => new Object1500_EggFlapper(),
                    0x10 => new Object1510_EggPawn(),
                    0x20 or 0x70 => new Object15_KlagenCameron(),
                    0x40 => new Object1540_EggHammer(),
                    0x90 => new Object1590_RhinoLiner(),
                    0xC0 => new Object15C0_EggBishop(),
                    0xD0 => new Object15D0_E2000(),
                    0xF4 => new Object0026_FormGate(),
                    _ => new Object_HeroesDefault(),
                },
                0x16 => Type switch
                {
                    0x0 or 0x1 => new Object_16_00_01(),
                    0x2 => new Object_16_02(),
                    _ => new Object_HeroesDefault(),
                },
                0x20 => Type switch
                {
                    0x80 or 0x81 => new Object_B1_1_Type(),
                    _ => new Object_HeroesDefault(),
                },
                0x23 => Type switch
                {
                    0x0 => new Object2300_EggAlbatross(),
                    _ => new Object_HeroesDefault(),
                },
                0x33 => Type switch
                {
                    0x0 => new Object_S1_1_Type(),
                    0x80 => new Object_F1Speed(),
                    _ => new Object_HeroesDefault(),
                },
                _ => new Object_HeroesDefault(),
            };
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
            return List switch
            {
                0x00 => Type switch
                {
                    0x01 => new Object0001_SpringShadow(),
                    0x02 => new Object0002_WideSpring(),
                    0x03 or 0x06 => new Object0003_DashPanel(),
                    0x04 => new Object0004_DashRamp(),
                    0x05 => new Object0005_Checkpoint(),
                    0x07 => new Object0007_Case(),
                    0x08 => new Object0008_Pulley(),
                    0x09 => new Object0009_WoodBox(),
                    0x0A => new Object000A_MetalBox(),
                    0x0B => new Object000B_UnbreakableBox(),
                    0x0C => new Object000C_WeaponBox(),
                    0x0E => new Object000E_Rocket(),
                    0x0F => new Object000F_Platform(),
                    0x10 => new Object0010_Ring(),
                    0x11 => new Object0011_HintBall(),
                    0x12 => new Object0012_ItemCapsule(),
                    0x13 => new Object0013_Balloon(),
                    0x14 => new Object0014_GoalRing(),
                    0x15 => new Object0015_BallSwitch(),
                    0x16 => new Object0016_TargetSwitch(),
                    0x19 => new Object0019_Weight(),
                    0x1A => new Object001A_Wind(),
                    0x1B => new Object001B_Roadblock(),
                    0x20 => new Object0020_Weapon(),
                    0x23 => new Object0023_OverturnableObject(),
                    0x3A => new Object003A_SpecialWeaponBox(),
                    0x33 => new Object0033_EnergyCore(),
                    0x34 => new Object003X_UnusedMiscByteScaleType(),//Fire
                    0x35 => new Object003X_UnusedMiscByteScaleType(),//PoisonGas
                    0x37 => new Object003X_UnusedMiscByteScaleType(),//CaptureCage
                    0x4F => new Object004F_Vehicle(),
                    0x50 => new Object0050_Trigger(),
                    0x51 => new Object0051_TriggerTalking(),
                    0x59 => new Object0059_TriggerSkybox(),
                    0x5A => new Object005A_Pole(),
                    0x61 => new Object0061_DarkSpinEntrance(),
                    0x62 => new Object0062_LightColli(),
                    0x64 => new Object0064_GUNSoldier(),
                    0x65 => new Object0065_GUNBeetle(),
                    0x66 => new Object0066_GUNBigfoot(),
                    0x68 => new Object0068_GUNRobot(),
                    0x78 => new Object0078_EggPierrot(),
                    0x79 => new Object0079_EggPawn(),
                    0x7A => new Object007A_EggShadowAndroid(),
                    0x8C => new Object008C_BkGiant(),
                    0x8D => new Object008D_BkSoldier(),
                    0x8E => new Object008E_BkWingLarge(),
                    0x8F => new Object008F_BkWingSmall(),
                    0x90 => new Object0090_BkWorm(),
                    0x91 => new Object0091_BkLarva(),
                    0x92 => new Object0092_BkChaos(),
                    0x93 => new Object0093_BkNinja(),
                    _ => new Object_ShadowDefault(),// warp hole
                },
                0x01 => Type switch
                {
                    0x2C => new Object012C_EnvironmentalWeapon(),
                    0x90 => new Object0190_Partner(),
                    _ => new Object_ShadowDefault(),
                },
                0x03 => Type switch
                {
                    0xE9 => new Object03E9_FallingBuilding(),
                    0xEA => new Object03EA_CityLaser(),
                    _ => new Object_ShadowDefault(),
                },
                0x07 => Type switch
                {
                    0xD1 => new Object07D1_Searchlight(),
                    0xD2 => new Object07D2_ColorSwitch(),
                    0xD3 => new Object07D3_RisingLaserBar(),
                    0xD4 => new Object07D4_ElecSecurity(),
                    0xD5 => new Object07D5_LightspeedRisingBlock(),
                    0xD7 => new Object07D7_DigitalBreakableTile(),
                    0xD8 => new Object07D8_LaserWallBarrier(),
                    0xDA => new Object07DA_MatrixTerminalElecFan(),
                    0xDE => new Object0001_SpringShadow(),
                    0xDF => new Object07DF_LightspeedFirewall(),
                    0xE1 => new Object07E1_TriggerDigitalBreakableTile(),
                    0xE2 => new Object07E2_ElecCube(),
                    0xE8 => new Object07E8_ElecRollHexa(),
                    0xEB => new Object07EB_CubePlatformCircle(),
                    _ => new Object_ShadowDefault(),
                },
                0x08 => Type switch
                {
                    0x34 => new Object0834_Tornado(),
                    0x35 => new Object0835_TornadoCollision(),
                    0x36 => new Object0836_RollCircle(),
                    0x37 => new Object0837_CollapsingPillar(),
                    0x38 => new Object0838_RuinsStoneGuardian(),
                    0x39 => new Object106C_SkyRuinsJewel(),//RuinsJewel / PowerDeviceCage
                    0x99 => new Object0899_BlackTankCommandCollision(),
                    0x9A => new Object089A_BreakingRoad(),
                    0x9C => new Object089C_FallingRoad(),
                    _ => new Object_ShadowDefault(),
                },
                0x0B => Type switch
                {
                    0xBB => new Object0BBB_SmallLantern(),
                    0xBC => new Object0BBC_PopupDummyGhost(),
                    0xBE => new Object0BBE_Chao(),
                    0xC7 => new Object0BC7_CastleMonster(),
                    0xC8 => new Object0BC8_CastleMonsterControl(),
                    _ => new Object_ShadowDefault(),
                },
                0x0C => Type switch
                {
                    0x80 => new Object0C80_BounceBall(),
                    0x81 => new Object0C81_CircusGong(),
                    0x82 => new Object0C82_GameBalloonsGhosts(),
                    0x83 => new Object0C83_CircusGameTarget(),
                    0x87 => new Object005A_Pole(),//CircusPole
                    0x88 => new Object0C88_Zipline(),
                    0x89 => new Object1133_ProximityDoor(),//TentCurtain
                    _ => new Object_ShadowDefault(),
                },
                0x0F => Type switch
                {
                    0xA1 => new Object0FA1_BAMiniBomb(),
                    0xA2 => new Object0FA2_Helicopter(),
                    0xA4 => new Object0FA4_BuildingChunk(),
                    _ => new Object_ShadowDefault(),
                },
                0x10 => Type switch
                {
                    0x04 => new Object1004_ArkCrackedWall(),
                    0x05 => new Object1005_Researcher(),
                    0x06 => new Object1006_HealUnitServer(),
                    0x69 => new Object1069_FleetHolderEggmanBattleship(),
                    0x6C => new Object106C_SkyRuinsJewel(),
                    0x6D => new Object106D_RainEffect(),
                    _ => new Object_ShadowDefault(),
                },
                0x11 => Type switch
                {
                    0x30 => new Object1130_FenceWall(),
                    0x31 => new Object1131_Vine(),
                    0x32 => new Object1132_ElevatorPlatformColumn(),
                    0x33 => new Object1133_ProximityDoor(),
                    0x34 => new Object1134_DamageBlock(),
                    0x35 => new Object1130_FenceWall(),//ShatterTrijumpPanel (BreakWall)
                    0x37 => new Object1137_StretchGrass(),
                    0x38 => new Object1138_JumpPanel(),
                    _ => new Object_ShadowDefault(),
                },
                0x13 => Type switch
                {
                    0x8A => new Object138B_MeteorsHolder(),//Meteor
                    0x8B => new Object138B_MeteorsHolder(),
                    0x8E => new Object138E_ArkCannon(),
                    0x92 => new Object1392_SpaceDebris(),
                    0xED => new Object13ED_EscapePodPathSwitch(),
                    0xEF => new Object13EF_SecurityLaser(),
                    0xF0 => new Object13F0_SlideoutPlatform(),
                    0xF1 => new Object1133_ProximityDoor(),//HeavyBaseDoor
                    0xF2 => new Object0899_BlackTankCommandCollision(),//EscapePodCommandCollision
                    0xF3 => new Object13F3_EscapePodDownRail(),
                    _ => new Object_ShadowDefault(),
                },
                0x14 => Type switch
                {
                    0x51 => new Object1451_EggBalloonCommandCollision(),
                    0xB4 => new Object14B5_GravityChangeZone(),//GravityChangeSwitch
                    0xB5 => new Object14B5_GravityChangeZone(),
                    0xB6 => new Object14B6_GravityChangeCollision(),
                    0xBE => new Object14BE_ArkGreenLaser(),
                    0xED => new Object13ED_EscapePodPathSwitch(),
                    _ => new Object_ShadowDefault(),
                },
                0x17 => Type switch
                {
                    0x70 => new Object1770_GUNCamera(),
                    0x72 => new Object1772_ConcreteDoor(),
                    0x73 => new Object1773_CrushingWalls(),
                    0xD4 => new Object11D4_BAGunShip(),
                    0xD5 => new Object17D5_BlackArmsMine(),
                    _ => new Object_ShadowDefault(),
                },
                0x18 => Type switch
                {
                    0x39 => new Object1839_RisingLava(),
                    0x9E => new Object189E_ARKDriftingPlat1(),
                    0x9F => new Object189F_ArkRollPlatform(),
                    _ => new Object_ShadowDefault(),
                },
                0x19 => Type switch
                {
                    0x01 => new Object1901_CometBarrier(),
                    0x03 => new Object1903_BlackDoomHologram(),
                    _ => new Object_ShadowDefault(),
                },
                0x25 => Type switch
                {
                    0x86 => new Object2586_Sample(),
                    0x88 => new Object2588_Decoration1(),
                    0x89 => new Object2589_Destructable1(),
                    0x8A => new Object258A_Effect1(),
                    0x90 => new Object2588_Decoration1(),
                    0x91 => new Object2589_Destructable1(),
                    0x92 => new Object2592_DebugMissionClearCollision(),
                    0x93 => new Object2593_SetGenerator(),
                    0x94 => new Object2594_Fan(),
                    0x95 => new Object2595_MissionClearCollision(),
                    0x97 => new Object2597_SetSeLoop(),
                    0x98 => new Object2598_SetSeOneShot(),
                    _ => new Object_ShadowDefault(),
                },
                _ => new Object_ShadowDefault(),
            };
        }

        public static List<SetObjectHeroes> GetHeroesLayoutFromShadow(string fileName)
        {
            var list = new List<SetObjectHeroes>();

            try
            {
                foreach (SetObjectShadow i in GetShadowLayout(fileName, out _))
                {
                    SetObjectHeroes TempObject = SetObjectShadowToHeroes(i);

                    if (TempObject != null)
                    {
                        TempObject.CreateTransformMatrix();
                        list.Add(TempObject);
                    }
                }
            } catch (InvalidDataException)
            {
                // cancel gracefully
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
