using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using SharpDX;
using RenderWareFile;
using RenderWareFile.Sections;
using HeroesONE_R.Structures;
using HeroesONE_R.Structures.Subsctructures;
using static HeroesPowerPlant.BSPRenderer;

namespace HeroesPowerPlant.LevelEditor
{
    public static class VisibilityFunctions
    {
        public static void RenderChunkModels(SharpRenderer renderer)
        {
            renderer.basicShader.Apply();
            for (int j = 0; j < ChunkList.Count; j++)
                ChunkList[j].Render(renderer);
        }

        public static void SetSelectedChunkColor(System.Drawing.Color color)
        {
            SetSelectedChunkColor(new SharpDX.Color(color.R, color.G, color.B).ToVector4());
        }

        public static void SetSelectedChunkColor(Vector4 newColor)
        {
            newColor.W = Chunk.selectedChunkColor.W;
            Chunk.selectedChunkColor = newColor;
        }

        public static void ResetSelectedChunkColor()
        {
            Chunk.selectedChunkColor = defaultSelectedChunkColor;
        }

        private static Vector4 defaultSelectedChunkColor = new Vector4(1f, 0.5f, 0.1f, 0.3f);

        public static List<Chunk> ChunkList = new List<Chunk>();

        public static List<Chunk> LoadHeroesVisibilityFile(string fileName)
        {
            List<Chunk> list = new List<Chunk>();

            BinaryReader BLKFileReader = new BinaryReader(new FileStream(fileName, FileMode.Open));

            BLKFileReader.BaseStream.Position = 0;

            for (int i = 0; i < (BLKFileReader.BaseStream.Length / 0x1C); i++)
            {
                byte[] BCArray = BLKFileReader.ReadBytes(0x1C);
                Chunk TempChunk = new Chunk
                {
                    number = BitConverter.ToInt32(new byte[] { BCArray[3], BCArray[2], BCArray[1], BCArray[0] }, 0)
                };

                if (TempChunk.number == -1) continue;

                TempChunk.Min.X = BitConverter.ToInt32(new byte[] { BCArray[7], BCArray[6], BCArray[5], BCArray[4] }, 0);
                TempChunk.Min.Y = BitConverter.ToInt32(new byte[] { BCArray[11], BCArray[10], BCArray[9], BCArray[8] }, 0);
                TempChunk.Min.Z = BitConverter.ToInt32(new byte[] { BCArray[15], BCArray[14], BCArray[13], BCArray[12] }, 0);
                TempChunk.Max.X = BitConverter.ToInt32(new byte[] { BCArray[19], BCArray[18], BCArray[17], BCArray[16] }, 0);
                TempChunk.Max.Y = BitConverter.ToInt32(new byte[] { BCArray[23], BCArray[22], BCArray[21], BCArray[20] }, 0);
                TempChunk.Max.Z = BitConverter.ToInt32(new byte[] { BCArray[27], BCArray[26], BCArray[25], BCArray[24] }, 0);
                TempChunk.CalculateModel();

                list.Add(TempChunk);
            }
            BLKFileReader.Close();

            return list;
        }

        public static List<Chunk> LoadShadowVisibilityFile(Archive shadowDATONE)
        {
            List<Chunk> list = new List<Chunk>();

            foreach (var i in shadowDATONE.Files)
                if (Path.GetExtension(i.Name).ToLower() == ".bdt")
                    return (LoadShadowVisibilityFile(new MemoryStream(i.DecompressThis())));

            throw new Exception("No visibility BDT file found");
        }

        public static List<Chunk> LoadShadowVisibilityFile(Stream bdtFile)
        {
            List<Chunk> list = new List<Chunk>();

            BinaryReader BLKFileReader = new BinaryReader(bdtFile);

            BLKFileReader.BaseStream.Position = 0x40;

            int numberOfChunks = BLKFileReader.ReadInt32();
            for (int j = 0; j < numberOfChunks; j++)
            {
                Chunk TempChunk = new Chunk
                {
                    number = BLKFileReader.ReadInt32(),
                    Min = new Vector3(BLKFileReader.ReadSingle(), BLKFileReader.ReadSingle(), BLKFileReader.ReadSingle()),
                    Max = new Vector3(BLKFileReader.ReadSingle(), BLKFileReader.ReadSingle(), BLKFileReader.ReadSingle()),
                };
                TempChunk.CalculateModel();

                list.Add(TempChunk);
                BLKFileReader.ReadInt32();
            }
            BLKFileReader.Close();
            
            return list;
        }

        public static void SaveHeroesVisibilityFile(IEnumerable<Chunk> chunkList, string fileName)
        {
            BinaryWriter BLKFileWriter = new BinaryWriter(new FileStream(fileName, FileMode.Create));

            foreach (Chunk i in chunkList)
            {
                if (i.number == -1)
                    continue;
                BLKFileWriter.Write(BitConverter.GetBytes(i.number)[3]);
                BLKFileWriter.Write(BitConverter.GetBytes(i.number)[2]);
                BLKFileWriter.Write(BitConverter.GetBytes(i.number)[1]);
                BLKFileWriter.Write(BitConverter.GetBytes(i.number)[0]);

                BLKFileWriter.Write(BitConverter.GetBytes((int)i.Min.X).Reverse().ToArray());
                BLKFileWriter.Write(BitConverter.GetBytes((int)i.Min.Y).Reverse().ToArray());
                BLKFileWriter.Write(BitConverter.GetBytes((int)i.Min.Z).Reverse().ToArray());
                BLKFileWriter.Write(BitConverter.GetBytes((int)i.Max.X).Reverse().ToArray());
                BLKFileWriter.Write(BitConverter.GetBytes((int)i.Max.Y).Reverse().ToArray());
                BLKFileWriter.Write(BitConverter.GetBytes((int)i.Max.Z).Reverse().ToArray());
            }

            BLKFileWriter.Close();
        }

        public static void SaveShadowVisibilityFile(IEnumerable<Chunk> chunkList, string levelName, string visibilityONEpath)
        {
            BinaryWriter BLKFileWriter = new BinaryWriter(new MemoryStream());

            foreach (char c in levelName)
                BLKFileWriter.Write((byte)c);

            BLKFileWriter.BaseStream.Position = 0x40;

            BLKFileWriter.Write(chunkList.Count());
            foreach (Chunk i in chunkList)
            {
                BLKFileWriter.Write(i.number);
                BLKFileWriter.Write(i.Min.X);
                BLKFileWriter.Write(i.Min.Y);
                BLKFileWriter.Write(i.Min.Z);
                BLKFileWriter.Write(i.Max.X);
                BLKFileWriter.Write(i.Max.Y);
                BLKFileWriter.Write(i.Max.Z);
                BLKFileWriter.Write(80);
            }

            Archive shadowDATONE;

            if (File.Exists(visibilityONEpath))
            {
                byte[] fileContents = File.ReadAllBytes(visibilityONEpath);
                shadowDATONE = Archive.FromONEFile(ref fileContents);
            }
            else
            {
                shadowDATONE = new Archive(CommonRWVersions.Shadow050);
            }

            bool found = false;
            foreach (var file in shadowDATONE.Files)
            {
                if (Path.GetExtension(file.Name).ToLower() == ".bdt")
                {
                    byte[] bytes = (BLKFileWriter.BaseStream as MemoryStream).ToArray();
                    file.CompressedData = Prs.Compress(ref bytes);
                    found = true;
                    break;
                }
            }
            if (!found)
            {
                byte[] bytes = (BLKFileWriter.BaseStream as MemoryStream).ToArray();
                ArchiveFile file = new ArchiveFile((levelName + ".bdt").ToUpper(), bytes);
                shadowDATONE.Files.Add(file);
            }

            
            List<byte> fileBytes = shadowDATONE.BuildShadowONEArchive(true);
            File.WriteAllBytes(visibilityONEpath, fileBytes.ToArray());

            BLKFileWriter.Close();
        }

        public static void AutoChunk(Chunk chunk, out bool success, out Vector3 Min, out Vector3 Max)
        {
            Min = Vector3.Zero;
            Max = Vector3.Zero;

            success = false;

            List<RenderWareModelFile> bspAndCol = new List<RenderWareModelFile>();
            bspAndCol.AddRange(BSPList);
            bspAndCol.AddRange(ShadowColBSPList);

            foreach (RenderWareModelFile b in bspAndCol)
            {
                if (b.ChunkNumber == chunk.number)
                {
                    if (!success)
                    {
                        success = true;
                        foreach (RWSection rwSection in b.GetAsRWSectionArray())
                        {
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
                    }

                    foreach (RWSection rwSection in b.GetAsRWSectionArray())
                    {
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
}