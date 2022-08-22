using HeroesONE_R.Structures;
using HeroesPowerPlant.Shared.Utilities;
using RenderWareFile;
using RenderWareFile.Sections;
using SharpDX;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace HeroesPowerPlant.LevelEditor
{
    public class VisibilityFunctions
    {
        public void RenderChunkModels(SharpRenderer renderer)
        {
            renderer.basicShader.Apply();
            for (int j = 0; j < ChunkList.Count; j++)
                ChunkList[j].Render(renderer);
        }

        public void SetSelectedChunkColor(System.Drawing.Color color)
        {
            SetSelectedChunkColor(new SharpDX.Color(color.R, color.G, color.B).ToVector4());
        }

        public void SetSelectedChunkColor(Vector4 newColor)
        {
            newColor.W = Chunk.selectedChunkColor.W;
            Chunk.selectedChunkColor = newColor;
        }

        public static void ResetSelectedChunkColor()
        {
            Chunk.selectedChunkColor = defaultSelectedChunkColor;
        }

        private static Vector4 defaultSelectedChunkColor => new Vector4(1f, 0.5f, 0.1f, 0.3f);

        public string OpenVisibilityFile;
        public List<Chunk> ChunkList = new List<Chunk>();

        public static List<Chunk> LoadHeroesVisibilityFile(string fileName)
        {
            var list = new List<Chunk>();
            using (var reader = new EndianBinaryReader(new FileStream(fileName, FileMode.Open), Endianness.Big))
            {
                while (reader.BaseStream.Position < reader.BaseStream.Length)
                {
                    var chunk = new Chunk(reader.ReadInt32(),
                        new Vector3(reader.ReadInt32(), reader.ReadInt32(), reader.ReadInt32()),
                        new Vector3(reader.ReadInt32(), reader.ReadInt32(), reader.ReadInt32()));

                    if (chunk.number == -1)
                        continue;

                    chunk.CalculateModel();
                    list.Add(chunk);
                }
            }

            return list;
        }

        public static List<Chunk> LoadShadowVisibilityFile(Archive shadowDATONE)
        {
            foreach (var i in shadowDATONE.Files)
                if (Path.GetExtension(i.Name).ToLower() == ".bdt")
                    return (LoadShadowVisibilityFile(new MemoryStream(i.DecompressThis())));

            throw new Exception("No visibility BDT file found");
        }

        public static List<Chunk> LoadShadowVisibilityFile(Stream bdtFile)
        {
            var list = new List<Chunk>();
            using (var reader = new BinaryReader(bdtFile))
            {
                reader.BaseStream.Position = 0x40;
                int count = reader.ReadInt32();
                for (int j = 0; j < count; j++)
                {
                    var chunk = new Chunk(reader.ReadInt32(),
                        new Vector3(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle()),
                        new Vector3(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle()));
                    chunk.CalculateModel();
                    list.Add(chunk);
                    reader.ReadInt32();
                }
            }

            return list;
        }

        public static void SaveHeroesVisibilityFile(IEnumerable<Chunk> chunkList, string fileName)
        {
            using var writer = new EndianBinaryWriter(new FileStream(fileName, FileMode.Create), Endianness.Big);
            foreach (Chunk i in chunkList)
            {
                if (i.number == -1)
                    continue;
                writer.Write(i.number);

                writer.Write(i.Min.X);
                writer.Write(i.Min.Y);
                writer.Write(i.Min.Z);
                writer.Write(i.Max.X);
                writer.Write(i.Max.Y);
                writer.Write(i.Max.Z);
            }
        }

        public static byte[] ShadowVisibilityFileToArray(IEnumerable<Chunk> chunkList, string levelName)
        {
            using var writer = new BinaryWriter(new MemoryStream());

            foreach (char c in levelName)
                writer.Write((byte)c);

            writer.BaseStream.Position = 0x40;

            writer.Write(chunkList.Count());
            foreach (Chunk i in chunkList)
            {
                writer.Write(i.number);
                writer.Write(i.Min.X);
                writer.Write(i.Min.Y);
                writer.Write(i.Min.Z);
                writer.Write(i.Max.X);
                writer.Write(i.Max.Y);
                writer.Write(i.Max.Z);
                writer.Write(80);
            }

            return (writer.BaseStream as MemoryStream).ToArray();
        }

        public static void AutoChunk(int number, List<RenderWareModelFile> bspAndCol, out bool success, out Vector3 Min, out Vector3 Max)
        {
            Min = Max = Vector3.Zero;
            success = false;

            foreach (RenderWareModelFile b in bspAndCol)
            {
                if (b.ChunkNumber == number)
                {
                    if (!success)
                    {
                        success = true;
                        foreach (RWSection rwSection in b.GetAsRWSectionArray())
                            if (rwSection is World_000B w)
                            {
                                Min.X = w.worldStruct.boxMinimum.X;
                                Min.Y = w.worldStruct.boxMinimum.Y;
                                Min.Z = w.worldStruct.boxMinimum.Z;
                                Max.X = w.worldStruct.boxMaximum.X;
                                Max.Y = w.worldStruct.boxMaximum.Y;
                                Max.Z = w.worldStruct.boxMaximum.Z;
                                break;
                            }
                    }

                    foreach (RWSection rwSection in b.GetAsRWSectionArray())
                        if (rwSection is World_000B w)
                        {
                            if (w.worldStruct.boxMinimum.X < Min.X)
                                Min.X = w.worldStruct.boxMinimum.X;
                            if (w.worldStruct.boxMinimum.Y < Min.Y)
                                Min.Y = w.worldStruct.boxMinimum.Y;
                            if (w.worldStruct.boxMinimum.Z < Min.Z)
                                Min.Z = w.worldStruct.boxMinimum.Z;
                            if (w.worldStruct.boxMaximum.X > Max.X)
                                Max.X = w.worldStruct.boxMaximum.X;
                            if (w.worldStruct.boxMaximum.Y > Max.Y)
                                Max.Y = w.worldStruct.boxMaximum.Y;
                            if (w.worldStruct.boxMaximum.Z > Max.Z)
                                Max.Z = w.worldStruct.boxMaximum.Z;

                            break;
                        }
                }
            }
        }
    }
}