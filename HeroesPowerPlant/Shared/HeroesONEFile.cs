using FraGag.Compression;
using HeroesPowerPlant;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace HeroesONELib
{
    public class HeroesONEFile
    {
        public class File
        {
            public string Name { get; set; }
            public byte[] Data { get; set; }

            public File()
            {
                Name = string.Empty;
            }

            public File(string fileName)
            {
                Name = Path.GetFileName(fileName);
                Data = System.IO.File.ReadAllBytes(fileName);
            }

            public File(string name, byte[] data)
            {
                Name = name;
                Data = data;
            }
        }

        public ArchiveType Type { get; private set; }
        public List<File> Files { get; set; }

        const int HeroesMagic = 0x1400FFFF;
        const int HeroesE3Magic = 0x1005FFFF;
        const int HeroesPreE3Magic = 0x1003FFFF;
        const int Shadow060Magic = 0x1C020037;
        const int Shadow050Magic = 0x1C020020;

        public HeroesONEFile()
        {
            Files = new List<File>();
        }

        public HeroesONEFile(string filename)
        {
            Files = new List<File>();
            using (FileStream stream = System.IO.File.OpenRead(filename))
            using (BinaryReader reader = new BinaryReader(stream, System.Text.Encoding.ASCII))
            {
                stream.Seek(4, SeekOrigin.Current);
                int filesize = reader.ReadInt32() + 0xC;
                switch (reader.ReadInt32())
                {
                    case HeroesMagic: { DoHeroesMagic(stream, reader, filesize, 1); } break;
                    case HeroesE3Magic: { DoHeroesMagic(stream, reader, filesize, 2); } break;
                    case HeroesPreE3Magic: { DoHeroesMagic(stream, reader, filesize, 3); } break;
                    case Shadow060Magic:
                    case Shadow050Magic:
                        {
                            switch (reader.ReadString(12))
                            {
                                case "One Ver 0.60":
                                    Type = ArchiveType.Shadow060;
                                    break;
                                case "One Ver 0.50":
                                    Type = ArchiveType.Shadow050;
                                    break;
                            }
                            if (Type == ArchiveType.Heroes || Type == ArchiveType.HeroesE3 || Type == ArchiveType.HeroesPreE3) goto default;
                            stream.Seek(4, SeekOrigin.Current);
                            int fnum = reader.ReadInt32();
                            stream.Seek(0x90, SeekOrigin.Current);
                            List<string> filenames = new List<string>(fnum);
                            List<int> fileaddrs = new List<int>(fnum);
                            for (int i = 0; i < fnum; i++)
                            {
                                filenames.Add(reader.ReadString(Type == ArchiveType.Shadow060 ? 0x2C : 0x20));
                                stream.Seek(4, SeekOrigin.Current);
                                fileaddrs.Add(reader.ReadInt32());
                                stream.Seek(4, SeekOrigin.Current);
                                if (Type == ArchiveType.Shadow050)
                                    stream.Seek(0xC, SeekOrigin.Current);
                            }
                            fileaddrs.Add(filesize);
                            for (int i = 0; i < fnum; i++)
                            {
                                stream.Seek(fileaddrs[i] + 0xC, SeekOrigin.Begin);
                                Files.Add(new File(filenames[i], Prs.Decompress(reader.ReadBytes(fileaddrs[i + 1] - fileaddrs[i]))));
                            }
                        }
                        break;
                    default:
                        throw new Exception("Error: Unknown archive type");
                }
            }
        }

        // Copy of case HeroesMagic to avoid redundant code when E3 version is chosen. .ONE format seems to always be the same, just different headers.
        public void DoHeroesMagic(FileStream stream, BinaryReader reader, int filesize, int HeroesType)
        {
            if (HeroesType == 1) { Type = ArchiveType.Heroes; } // Final
            else if (HeroesType == 2) { Type = ArchiveType.HeroesE3; } // E3
            else if (HeroesType == 3) { Type = ArchiveType.HeroesPreE3; } // Pre-E3
            stream.Seek(4, SeekOrigin.Current);
            long fnlength = reader.ReadInt32();
            stream.Seek(4, SeekOrigin.Current);
            List<string> filenames = new List<string>((int)(fnlength / 64));
            fnlength += stream.Position;
            while (stream.Position < fnlength)
                filenames.Add(reader.ReadString(64));
            stream.Seek(fnlength, SeekOrigin.Begin);
            while (stream.Position < filesize)
            {
                int fn = reader.ReadInt32();
                int sz = reader.ReadInt32();
                stream.Seek(4, SeekOrigin.Current);
                Files.Add(new File(filenames[fn], Prs.Decompress(reader.ReadBytes(sz))));
            }
        }

        public void Save(string filename, ArchiveType type)
        {
            foreach (File item in Files)
            {
                Program.levelEditor.progressBar1.Maximum += item.Data.Length;
            }

            using (FileStream stream = System.IO.File.Open(filename, FileMode.Create, FileAccess.Write))
            using (BinaryWriter writer = new BinaryWriter(stream, System.Text.Encoding.ASCII))
            {
                writer.Write(0);
                long fspos = stream.Position;
                writer.Write(-1);
                if (type == ArchiveType.Heroes || type == ArchiveType.HeroesE3 || type == ArchiveType.HeroesPreE3)
                {
                    byte[] filenames = new byte[256 * 64];
                    if (type == ArchiveType.Heroes) { writer.Write(HeroesMagic); }
                    else if (type == ArchiveType.HeroesE3) { writer.Write(HeroesE3Magic); }
                    else if (type == ArchiveType.HeroesPreE3) { writer.Write(HeroesPreE3Magic); }
                    writer.Write(1);
                    writer.Write(filenames.Length);
                    if (type == ArchiveType.Heroes) { writer.Write(HeroesMagic); }
                    else if (type == ArchiveType.HeroesE3) { writer.Write(HeroesE3Magic); }
                    else if (type == ArchiveType.HeroesPreE3) { writer.Write(HeroesPreE3Magic); }
                    long fnpos = stream.Position;
                    writer.Write(filenames);
                    int i = 2;
                    foreach (File item in Files)
                    {
                        byte[] data = Prs.Compress(item.Data);
                        writer.Write(i++);
                        writer.Write(data.Length);
                        if (type == ArchiveType.Heroes) { writer.Write(HeroesMagic); }
                        else if (type == ArchiveType.HeroesE3) { writer.Write(HeroesE3Magic); }
                        else if (type == ArchiveType.HeroesPreE3) { writer.Write(HeroesPreE3Magic); }
                        writer.Write(data);
                        System.Text.Encoding.ASCII.GetBytes(item.Name).CopyTo(filenames, (i - 1) * 64);

                        Program.levelEditor.progressBar1.Increment(item.Data.Length);
                    }
                    stream.Seek(fnpos, SeekOrigin.Begin);
                    writer.Write(filenames);
                }
                else
                {
                    writer.Write(type == ArchiveType.Shadow060 ? Shadow060Magic : Shadow050Magic);
                    writer.Write(type == ArchiveType.Shadow060 ? "One Ver 0.60" : "One Ver 0.50", 12);
                    writer.Write(0);
                    writer.Write(Files.Count);
                    byte[] buf = new byte[0x90];
                    for (int i = 1; i < 0x20; i++)
                        buf[i] = 0xCD;
                    writer.Write(buf);
                    Queue<long> addrpos = new Queue<long>(Files.Count);
                    foreach (File item in Files)
                    {
                        writer.Write(item.Name, type == ArchiveType.Shadow060 ? 0x2C : 0x20);
                        writer.Write(item.Data.Length);
                        addrpos.Enqueue(stream.Position);
                        writer.Write(-1);
                        writer.Write(1);
                        if (type == ArchiveType.Shadow050)
                            writer.Write(new byte[0xC]);
                    }
                    foreach (File item in Files)
                    {
                        stream.Seek(addrpos.Dequeue(), SeekOrigin.Begin);
                        writer.Write((int)(stream.Length - 0xC));
                        stream.Seek(0, SeekOrigin.End);
                        writer.Write(Prs.Compress(item.Data));
                    }
                }
                stream.Seek(fspos, SeekOrigin.Begin);
                writer.Write((int)(stream.Length - 0xC));
            }
        }
    }

    public enum ArchiveType
    {
        Heroes,
        Shadow060,
        Shadow050,
        HeroesE3,
        HeroesPreE3
    }

    public static class Extensions
    {
        /// <summary>
        /// Reads a null-terminated ASCII string from the current stream and advances the position by <paramref name="length"/> bytes.
        /// </summary>
        /// <param name="length">The maximum length of the string, in bytes.</param>
        public static string ReadString(this BinaryReader br, int length)
        {
            byte[] buffer = br.ReadBytes(length);
            for (int i = 0; i < length; i++)
                if (buffer[i] == 0)
                    return Encoding.ASCII.GetString(buffer, 0, i);
            return Encoding.ASCII.GetString(buffer);
        }

        public static void Write(this BinaryWriter bw, string str, int length)
        {
            byte[] buffer = Encoding.ASCII.GetBytes(str);
            Array.Resize(ref buffer, length);
            bw.Write(buffer);
        }
    }
}