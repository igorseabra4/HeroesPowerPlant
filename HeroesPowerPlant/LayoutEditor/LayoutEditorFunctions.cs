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
        public static List<SetObjectHeroes> GetHeroesLayout(string fileName, ObjectEntry[] objectEntries)
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

                LayoutFileReader.BaseStream.Position += 16;
                byte List = LayoutFileReader.ReadByte();
                byte Type = LayoutFileReader.ReadByte();
                byte Link = LayoutFileReader.ReadByte();
                byte Rend = LayoutFileReader.ReadByte();

                SetObjectHeroes TempObject = new SetObjectHeroes(List, Type, objectEntries, Position, Rotation, Link, Rend);

                if (List == 0 & Type == 0)
                    continue;

                int MiscSettings = Switch(LayoutFileReader.ReadInt32());

                if (MiscSettings == 0)
                    TempObject.objectEntry.HasMiscSettings = false;
                else
                {
                    LayoutFileReader.BaseStream.Position = 0x18000 + (0x24 * MiscSettings);
                    TempObject.objectManager.MiscSettings = LayoutFileReader.ReadBytes(36);
                }

                TempObject.CreateTransformMatrix();

                list.Add(TempObject);
            }

            LayoutFileReader.Close();

            return list;
        }

        public static void SaveHeroesLayout(IEnumerable<SetObjectHeroes> list, string outputFile)
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
                if (i.objectEntry.List == 0 & i.objectEntry.Type == 0) continue;

                layoutWriter.Write(Switch(i.Position.X));
                layoutWriter.Write(Switch(i.Position.Y));
                layoutWriter.Write(Switch(i.Position.Z));
                layoutWriter.Write(Switch((int)i.Rotation.X));
                layoutWriter.Write(Switch((int)i.Rotation.Y));
                layoutWriter.Write(Switch((int)i.Rotation.Z));
                layoutWriter.Write(new byte[] { 0, 2, currentNum, 9, 0, 0, 0, 0, 0, 2, currentNum, 9, 0, 0, 0, 0 });
                layoutWriter.Write(i.objectEntry.List);
                layoutWriter.Write(i.objectEntry.Type);
                layoutWriter.Write(i.Link);
                layoutWriter.Write(i.Rend);

                if (i.objectEntry.HasMiscSettings == true)
                {
                    layoutWriter.Write(Switch(j));

                    long currentPos = layoutWriter.BaseStream.Position;
                    layoutWriter.BaseStream.Position = 0x18000 + (0x24 * j);

                    if (i.objectManager.MiscSettings.Length != 36) throw new Exception();

                    i.objectManager.MiscSettings[0] = 1;
                    i.objectManager.MiscSettings[1] = 0;
                    i.objectManager.MiscSettings[2] = BitConverter.GetBytes(j)[1];
                    i.objectManager.MiscSettings[3] = BitConverter.GetBytes(j)[0];

                    layoutWriter.Write(i.objectManager.MiscSettings);

                    j++;
                    layoutWriter.BaseStream.Position = currentPos;
                }
                else
                    layoutWriter.Write(0);
            }

            layoutWriter.Close();
        }

        public static List<SetObjectShadow> GetShadowLayout(string fileName, ObjectEntry[] objectEntries)
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
            uint TotalMiscLenght = LayoutFileReader.ReadUInt32();

            for (int i = 0; i < AmountOfObjects; i++)
            {
                LayoutFileReader.BaseStream.Position = 12 + i * 0x2C;

                if (LayoutFileReader.BaseStream.Position >= LayoutFileReader.BaseStream.Length)
                    break;

                Vector3 Position = new Vector3(LayoutFileReader.ReadSingle(), LayoutFileReader.ReadSingle(), LayoutFileReader.ReadSingle());
                Vector3 Rotation = new Vector3(LayoutFileReader.ReadSingle(), LayoutFileReader.ReadSingle(), LayoutFileReader.ReadSingle());
                LayoutFileReader.BaseStream.Position += 8;
                byte Type = LayoutFileReader.ReadByte();
                byte List = LayoutFileReader.ReadByte();
                byte Link = LayoutFileReader.ReadByte();
                byte Rend = LayoutFileReader.ReadByte();
                int MiscSettingCount = LayoutFileReader.ReadInt32();

                SetObjectShadow TempObject = new SetObjectShadow(List, Type, objectEntries, Position, Rotation, Link, Rend, MiscSettingCount);

                list.Add(TempObject);
            }

            LayoutFileReader.BaseStream.Position = 12 + AmountOfObjects * 0x2C;
            for (int i = 0; i < list.Count; i++)
            {
                list[i].objectManager.MiscSettings = LayoutFileReader.ReadBytes(list[i].MiscSettingCount);
                list[i].CreateTransformMatrix();
            }

            LayoutFileReader.Close();

            return list;
        }

        public static void SaveShadowLayout(IEnumerable<SetObjectShadow> list, string outputFile)
        {
            byte CurrentNum;

            if (outputFile.ToLower().Contains("cmn"))
                CurrentNum = 0x10;
            else if (outputFile.ToLower().Contains("nrm"))
                CurrentNum = 0x20;
            else if (outputFile.ToLower().Contains("hrd"))
                CurrentNum = 0x40;
            else if (outputFile.ToLower().Contains("ds1"))
                CurrentNum = 0x80;
            else
                CurrentNum = 0;

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

                layoutWriter.Write(new byte[] { 1, CurrentNum });
                if (CurrentNum == 0x80)
                    layoutWriter.Write(new byte[] { 0x40, 0x80 });
                else
                    layoutWriter.Write(new byte[] { 0, 0x80 });

                layoutWriter.Write(new byte[] { 1, CurrentNum });
                if (CurrentNum == 0x80)
                    layoutWriter.Write(new byte[] { 0x40, 0x80 });
                else
                    layoutWriter.Write(new byte[] { 0, 0 });

                layoutWriter.Write(i.objectEntry.Type);
                layoutWriter.Write(i.objectEntry.List);
                layoutWriter.Write(i.Link);
                layoutWriter.Write(i.Rend);
                layoutWriter.Write(i.MiscSettingCount);
                layoutWriter.Write(0);
            }

            int MiscSettingLenght = -(int)layoutWriter.BaseStream.Position;

            foreach (SetObjectShadow i in list)
                layoutWriter.Write(i.objectManager.MiscSettings);

            MiscSettingLenght += (int)layoutWriter.BaseStream.Position;

            layoutWriter.BaseStream.Position = 8;
            layoutWriter.Write(MiscSettingLenght);

            layoutWriter.Close();
        }

        public static List<SetObjectHeroes> GetObjectsFromObjFile(string FileName, ObjectEntry objectEntry)
        {
            List<SetObjectHeroes> list = new List<SetObjectHeroes>();

            string[] SplineFile = File.ReadAllLines(FileName);
            foreach (string j in SplineFile)
            {
                if (j.StartsWith("v"))
                {
                    string[] a = Regex.Replace(j, @"\s+", " ").Split();
                    SetObjectHeroes heroesSetObject = new SetObjectHeroes(objectEntry, new Vector3(Convert.ToSingle(a[1]), Convert.ToSingle(a[2]), Convert.ToSingle(a[3])), Vector3.Zero, 0, 10);

                    list.Add(heroesSetObject);
                }
            }
            return list;
        }

        public static List<SetObjectHeroes> GetHeroesLayoutFromINI(string fileName, ObjectEntry[] objectEntries)
        {
            string[] file = File.ReadAllLines(fileName);
            List<SetObjectHeroes> list = new List<SetObjectHeroes>();

            SetObjectHeroes TempObject = new SetObjectHeroes();

            foreach (string s in file)
            {
                if (s.StartsWith("obj "))
                {
                    if (!(TempObject.objectEntry.List == 0 & TempObject.objectEntry.Type == 0))
                    {
                        TempObject.CreateTransformMatrix();
                        list.Add(TempObject);
                    }
                    TempObject = null;
                    TempObject = new SetObjectHeroes(Convert.ToByte(s.Substring(4, 2), 16), Convert.ToByte(s.Substring(6, 2), 16), objectEntries, Vector3.Zero, Vector3.Zero, 0, 10);
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
                    TempObject.Position.X = Convert.ToSingle(j[1]);
                    TempObject.Position.Y = Convert.ToSingle(j[2]);
                    TempObject.Position.Z = Convert.ToSingle(j[3]);
                }
                else if (s.StartsWith("r "))
                {
                    string[] j = s.Split(' ');
                    TempObject.Rotation.X = Convert.ToInt32(j[1]);
                    TempObject.Rotation.Y = Convert.ToInt32(j[2]);
                    TempObject.Rotation.Z = Convert.ToInt32(j[3]);
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

                        TempObject.objectManager.MiscSettings = NewMiscSettings.ToArray();
                    }
                    else
                    {
                        TempObject.objectEntry.HasMiscSettings = false;
                    }
                }
                else if (s == "EndOfFile")
                {
                    if (!(TempObject.objectEntry.List == 0 & TempObject.objectEntry.Type == 0))
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
                    + String.Format("{0, 2:X2}", i.objectEntry.List)
                    + String.Format("{0, 2:X2}", i.objectEntry.Type)
                    + "_" + i.objectEntry.Name.Replace(' ', '-'));
                iniWriter.WriteLine("link " + String.Format("{0, 2:D2}", i.Link));
                iniWriter.WriteLine("rend " + String.Format("{0, 2:D2}", i.Rend));
                iniWriter.WriteLine("v " + i.Position.X.ToString() + " " + i.Position.Y.ToString() + " " + i.Position.Z.ToString());
                iniWriter.WriteLine("r " + i.Rotation.X.ToString() + " " + i.Rotation.Y.ToString() + " " + i.Rotation.Z.ToString());
                if (i.objectEntry.HasMiscSettings)
                {
                    List<char> m = new List<char>();
                    for (int j = 0; j < i.objectManager.MiscSettings.Length; j++)
                    {
                        if (j % 4 == 0) m.Add(' ');
                        m.AddRange(String.Format("{0, 2:X2}", i.objectManager.MiscSettings[j]).ToCharArray());
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

        public static ObjectEntry[] ReadObjectListData(string FileName)
        {
            List<ObjectEntry> list = new List<ObjectEntry>();

            byte List = 0;
            byte Type = 0;
            string Name = "";
            bool HasMiscSettings = true;
            string DebugName = "";
            string Description = "";
            List<string> Model = new List<string>();

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
                else if (i.StartsWith("Debug="))
                    DebugName = i.Split('=')[1];
                else if (i.StartsWith("Description="))
                    Description = i.Split('=')[1];
                else if (i.StartsWith("Model="))
                    Model.Add(i.Split('=')[1]);
                else if (i.StartsWith("EndOfFile"))
                {
                    list.Add(new ObjectEntry()
                    {
                        List = List,
                        Type = Type,
                        Name = Name,
                        HasMiscSettings = HasMiscSettings,
                        DebugName = DebugName,
                        ModelNames = Model.ToArray(),
                        Description = Description,
                    });
                    break;
                }
                else if (i.Length == 0)
                {
                    list.Add(new ObjectEntry()
                    {
                        List = List,
                        Type = Type,
                        Name = Name,
                        HasMiscSettings = HasMiscSettings,
                        DebugName = DebugName,
                        ModelNames = Model.ToArray(),
                        Description = Description,
                    });
                    List = 0;
                    Type = 0;
                    Name = "";
                    HasMiscSettings = true;
                    DebugName = "";
                    Model = new List<string>();
                    Description = "";
                }
            }

            return list.ToArray();
        }

        public static List<SetObjectHeroes> GetHeroesLayoutFromShadow(string fileName, ObjectEntry[] HeroesEntries, ObjectEntry[] ShadowEntries)
        {
            List<SetObjectHeroes> list = new List<SetObjectHeroes>();

            foreach (SetObjectShadow i in GetShadowLayout(fileName, ShadowEntries))
            {
                SetObjectHeroes TempObject = SetObjectShadowToHeroes(i, HeroesEntries);

                if (TempObject != null)
                {
                    TempObject.CreateTransformMatrix();
                    list.Add(TempObject);
                }
            }

            return list;
        }

        private static SetObjectHeroes SetObjectShadowToHeroes(SetObjectShadow i, ObjectEntry[] HeroesEntries)
        {
            bool MatchNotFound = false;
            byte List = 0;
            byte Type = 0;
            byte[] MiscSettings = new byte[36];

            switch (i.objectEntry.List)
            {
                case 0:
                    List = 0;
                    if (i.objectEntry.Type == 0x00) Type = 0x03; // Ring
                    else if (i.objectEntry.Type == 0x01) Type = 0x01; // Spring
                    else if (i.objectEntry.Type == 0x02) Type = 0x02; // 3Spring
                    else if (i.objectEntry.Type == 0x03) Type = 0x0B; // Dash panel
                    else if (i.objectEntry.Type == 0x04) Type = 0x0F; // Dash ramp
                    else if (i.objectEntry.Type == 0x05) Type = 0x0E; // Checkpoint
                    else if (i.objectEntry.Type == 0x06) Type = 0x0C; // Dash Ring
                    else if (i.objectEntry.Type == 0x07) Type = 0x31; // Case
                    else if (i.objectEntry.Type == 0x08) Type = 0x1D; // Pulley
                    else if (i.objectEntry.Type == 0x09) Type = 0x20; // Gun Wood box
                    else if (i.objectEntry.Type == 0x0A) Type = 0x21; // Metal box
                    else if (i.objectEntry.Type == 0x0B) Type = 0x22; // Unbreakable box
                    else if (i.objectEntry.Type == 0x0C) Type = 0x20; // Normal Wood box
                    else if (i.objectEntry.Type == 0x0F) // Moving platform
                    {
                        // List = 0x13;
                        // Type = 0x06; // Egg fleet square platform
                        List = 0x5;
                        Type = 0xA; // dice
                    }
                    else if (i.objectEntry.Type == 0x10)  // Ring
                    {
                        Type = 0x03;

                        MiscSettings[5] = i.objectManager.MiscSettings[0];
                        MiscSettings[4] = i.objectManager.MiscSettings[1];

                        MiscSettings[7] = i.objectManager.MiscSettings[4];
                        MiscSettings[6] = i.objectManager.MiscSettings[5];

                        MiscSettings[11] = i.objectManager.MiscSettings[8];
                        MiscSettings[10] = i.objectManager.MiscSettings[9];
                        MiscSettings[9] = i.objectManager.MiscSettings[10];
                        MiscSettings[8] = i.objectManager.MiscSettings[11];

                        MiscSettings[15] = i.objectManager.MiscSettings[12];
                        MiscSettings[14] = i.objectManager.MiscSettings[13];
                        MiscSettings[13] = i.objectManager.MiscSettings[14];
                        MiscSettings[12] = i.objectManager.MiscSettings[15];

                        if (i.objectManager.MiscSettings[0] == 1)
                        {
                            i.Rotation.Y += 180;
                        }
                    }
                    else if (i.objectEntry.Type == 0x11) Type = 0x04; // Hint
                    else if (i.objectEntry.Type == 0x12) Type = 0x18; // Item balloon
                    else if (i.objectEntry.Type == 0x13) Type = 0x19; // Item balloon
                    else if (i.objectEntry.Type == 0x14) Type = 0x1B; // Goal ring
                    else if (i.objectEntry.Type == 0x15) Type = 0x05; // Switch
                                                                      // else if (i.objectEntry.Type == 0x1D) Type = 0x67; // Key
                    else if (i.objectEntry.Type == 0x1F) Type = 0x80; // Teleport
                    else if (i.objectEntry.Type == 0x3A) Type = 0x24; // shadow box
                    else if (i.objectEntry.Type == 0x65) // Beetle
                    {
                        List = 0x15;
                        Type = 0x00;
                    }
                    else if (i.objectEntry.Type == 0x78) // Egg bommer
                    {
                        List = 0x15;
                        Type = 0x20; //Klagen
                    }
                    else if (i.objectEntry.Type == 0x79 | i.objectEntry.Type == 0x8D) // Egg pawn
                    {
                        List = 0x15;
                        Type = 0x10;
                    }
                    else if (i.objectEntry.Type == 0x7A | i.objectEntry.Type == 0x90) // Shadow android / worm
                    {
                        List = 0x15;
                        Type = 0x70; // Cameron
                    }
                    else
                        MatchNotFound = true;
                    break;
                case 0x07:
                    if (i.objectEntry.Type == 0xD1) // Digital searchlight
                    {
                        List = 0x00;
                        Type = 0x2E; // Fan
                        i.Rotation.X = 0;
                        i.Rotation.Y = 0;
                        i.Rotation.Z = 0;
                    }
                    else if (i.objectEntry.Type == 0xD5) // Digital big block
                    {
                        float scale = BitConverter.ToSingle(i.objectManager.MiscSettings, 4);
                        if (BitConverter.ToSingle(i.objectManager.MiscSettings, 8) < scale)
                            scale = BitConverter.ToSingle(i.objectManager.MiscSettings, 8);
                        if (BitConverter.ToSingle(i.objectManager.MiscSettings, 12) < scale)
                            scale = BitConverter.ToSingle(i.objectManager.MiscSettings, 12);

                        if (i.objectManager.MiscSettings[0] < 9)
                        {
                            List = 0x01;
                            Type = 0x80; // Flower

                            MiscSettings[4] = i.objectManager.MiscSettings[0];

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
                    else if (i.objectEntry.Type == 0xD7) // Digital panel
                    {
                        List = 0x05;
                        Type = 0x05; // Casino panel

                        if (i.objectManager.MiscSettings[0] == 1)
                        {
                            i.Rotation.X -= 90;
                        }
                        else
                        {
                            MiscSettings[4] = BitConverter.GetBytes(5f)[3];
                            MiscSettings[5] = BitConverter.GetBytes(5f)[2];
                            MiscSettings[6] = BitConverter.GetBytes(5f)[1];
                            MiscSettings[7] = BitConverter.GetBytes(5f)[0];
                        }
                    }
                    else if (i.objectEntry.Type == 0xDB) // Digital core
                    {
                        List = 0x00;
                        Type = 0x1B; // Goal
                    }
                    else if (i.objectEntry.Type == 0xDE) // Digital spring
                    {
                        List = 0x05;
                        Type = 0x01; // Green spring

                        MiscSettings[4] = BitConverter.GetBytes(4f)[3];
                        MiscSettings[5] = BitConverter.GetBytes(4f)[2];
                        MiscSettings[6] = BitConverter.GetBytes(4f)[1];
                        MiscSettings[7] = BitConverter.GetBytes(4f)[0];
                    }
                    else if (i.objectEntry.Type == 0xDF) // Firewall
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
                    else if (i.objectEntry.Type == 0xE2) // Spinning cube dark
                    {
                        List = 0x05;
                        Type = 0x84; // Dice big
                    }
                    else if (i.objectEntry.Type == 0xE5) // Spinning cube PURPLE
                    {
                        List = 0x05;
                        Type = 0x84; // Dice big
                        MiscSettings[7] = 1;
                    }
                    else
                        MatchNotFound = true;
                    break;
                case 0x0B:
                    if (i.objectEntry.Type == 0xBB | i.objectEntry.Type == 0xC9) // small lantern, castle file
                    {
                        List = 0x11;
                        Type = 0x04; // flame torch
                    }
                    else
                        MatchNotFound = true;
                    break;
                case 0x0C:
                    if (i.objectEntry.Type == 0xDE) // bouncy ball
                    {
                        List = 0x05;
                        Type = 0x01; // Green spring

                        MiscSettings[4] = BitConverter.GetBytes(4f)[3];
                        MiscSettings[5] = BitConverter.GetBytes(4f)[2];
                        MiscSettings[6] = BitConverter.GetBytes(4f)[1];
                        MiscSettings[7] = BitConverter.GetBytes(4f)[0];
                    }
                    else if (i.objectEntry.Type == 0x82) // ghost
                    {
                        List = 0x11;
                        Type = 0x05; // ghost
                    }
                    else if (i.objectEntry.Type == 0x88) // zipline balloon
                    {
                        List = 0x11;
                        Type = 0x00; // teleport switch
                    }
                    else
                        MatchNotFound = true;
                    break;
                case 0x11:
                    if (i.objectEntry.Type == 0x33) // Castle door
                    {
                        List = 0x11;
                        Type = 0x01; // castle door
                    }
                    else
                        MatchNotFound = true;
                    break;
                case 0x13:
                    if (i.objectEntry.Type == 0xF1) // Lava shelter door
                    {
                        List = 0x07;
                        Type = 0x06; // Rail canyon door
                    }
                    else if (i.objectEntry.Type == 0xEF) // LASER
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
            else return new SetObjectHeroes(List, Type, HeroesEntries, i.Position,
                new Vector3(DegreesToBAMS(i.Rotation.X), DegreesToBAMS(i.Rotation.Y), DegreesToBAMS(i.Rotation.Z)), 0, (byte)(2 * i.Rend));
        }
    }
}
