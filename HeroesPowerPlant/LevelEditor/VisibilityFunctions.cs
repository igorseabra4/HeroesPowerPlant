using System;
using System.IO;
using System.Windows.Forms;
using System.Collections.Generic;
using SharpDX;
using SharpDX.Direct3D11;
using HeroesONELib;
using static HeroesPowerPlant.BSPRenderer;
using RenderWareFile;
using RenderWareFile.Sections;
using System.Linq;
using static HeroesPowerPlant.SharpRenderer;

namespace HeroesPowerPlant.LevelEditor
{
    public class VisibilityFunctions
    {
        public class Chunk
        {
            public int number;
            public Vector3 Min;
            public Vector3 Max;

            public bool isSelected;
            public int index;

            Matrix chunkTransform;
            DefaultRenderData renderData = new DefaultRenderData();

            public void CalculateModel()
            {
                chunkTransform = Matrix.Scaling(Max - Min) * Matrix.Translation((Max + Min) / 2);
            }
            
            public void Render(Matrix viewProjection)
            {
                renderData.worldViewProjection = chunkTransform * viewProjection;
                if (isSelected) renderData.Color = new Vector4(1f, 0.5f, 0.25f, 0.01f);
                else renderData.Color = new Vector4(0.75f, 0.75f, 1f, 0.01f);
                
                device.SetFillModeDefault();
                device.SetCullModeReverse();
                device.SetBlendStateAdditive();// (BlendOperation.Subtract, BlendOption.SourceColor, BlendOption.InverseSourceColor);
                device.ApplyRasterState();
                device.UpdateAllStates();

                device.UpdateData(basicBuffer, renderData);
                device.DeviceContext.VertexShader.SetConstantBuffer(0, basicBuffer);
                basicShader.Apply();

                Cube.Draw();

                device.SetFillModeWireframe();
                device.SetCullModeNone();
                device.SetDefaultBlendState();
                device.ApplyRasterState();
                device.UpdateAllStates();

                device.UpdateData(basicBuffer, renderData);
                device.DeviceContext.VertexShader.SetConstantBuffer(0, basicBuffer);
                basicShader.Apply();

                Cube.Draw();
            }
        }

        public static void RenderChunkModels(Matrix viewProjection)
        {
            for (int j = 0; j < ChunkList.Count; j++)
            {
                ChunkList[j].Render(viewProjection);
            }
        }

        public static List<Chunk> ChunkList = new List<Chunk>();

        public static List<Chunk> loadHeroesVisibilityFile(string fileName)
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

        public static List<Chunk> loadShadowVisibilityFile(HeroesONEFile shadowDATONE)
        {
            List<Chunk> list = new List<Chunk>();

                foreach (HeroesONEFile.File i in shadowDATONE.Files)
                {
                    if (Path.GetExtension(i.Name).ToLower() == ".bdt")
                    {
                        BinaryReader BLKFileReader = new BinaryReader(new MemoryStream(i.Data));

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

                        
                    }
                }

            return list;
        }

        public static void saveHeroesVisibilityFile(IEnumerable<Chunk> chunkList, string fileName)
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

            HeroesONEFile shadowDATONE;

            if (File.Exists(visibilityONEpath))
            {
                shadowDATONE = new HeroesONEFile(visibilityONEpath);
            }
            else
            {
                shadowDATONE = new HeroesONEFile();
            }

            bool found = false;
            foreach (HeroesONEFile.File i in shadowDATONE.Files)
            {
                if (Path.GetExtension(i.Name).ToLower() == ".bdt")
                {
                    i.Data = (BLKFileWriter.BaseStream as MemoryStream).ToArray();
                    found = true;
                    break;
                }
            }
            if (!found)
            {
                shadowDATONE.Files.Add(new HeroesONEFile.File((levelName + ".bdt").ToUpper(), (BLKFileWriter.BaseStream as MemoryStream).ToArray()));
            }

            shadowDATONE.Save(visibilityONEpath, ArchiveType.Shadow060);

            BLKFileWriter.Close();
        }

        public static void AutoChunk(Chunk chunk, out bool success, out Vector3 Min, out Vector3 Max)
        {
            Min = Vector3.Zero;
            Max = Vector3.Zero;

            success = false;

            foreach (RenderWareModelFile b in BSPStream)
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