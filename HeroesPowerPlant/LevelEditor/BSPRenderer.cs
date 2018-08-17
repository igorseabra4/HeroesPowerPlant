using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using SharpDX;
using SharpDX.Direct3D11;
using HeroesONE_R.Structures;
using HeroesONE_R.Structures.Subsctructures;
using RenderWareFile;
using static HeroesPowerPlant.SharpRenderer;

namespace HeroesPowerPlant
{
    public class BSPRenderer
    {
        public static void Dispose()
        {
            foreach (RenderWareModelFile r in BSPList)
                foreach (SharpMesh mesh in r.meshList)
                    mesh.Dispose();

            foreach (RenderWareModelFile r in ShadowColBSPList)
                foreach (SharpMesh mesh in r.meshList)
                    mesh.Dispose();
            
            foreach (ShaderResourceView texture in Textures.Values)
                if (texture != null)
                    texture.Dispose();
        }

        public static string currentFileNamePrefix = "default";
        public static List<RenderWareModelFile> BSPList = new List<RenderWareModelFile>();

        public static void SetHeroesBSPList(Archive heroesONEfile)
        {
            Dispose();
            Textures.Clear();
            LoadTextures(currentFileNamePrefix);

            ReadFileMethods.isShadow = false;

            BSPList = new List<RenderWareModelFile>(heroesONEfile.Files.Count);
            ShadowColBSPList = new List<RenderWareModelFile>();

            foreach (ArchiveFile file in heroesONEfile.Files)
            {
                if (!(new string[] { ".bsp", ".rg1", ".rx1" }.Contains(Path.GetExtension(file.Name).ToLower())))
                    continue;
                
                RenderWareModelFile TempBSPFile = new RenderWareModelFile(file.Name);
                TempBSPFile.SetChunkNumberAndName();
                byte[] uncompressedData = file.DecompressThis();
                TempBSPFile.SetForRendering(ReadFileMethods.ReadRenderWareFile(uncompressedData), uncompressedData);
                BSPList.Add(TempBSPFile);
            }
        }


        // Texture loader

        public const string DefaultTexture = "default";
        private static Dictionary<string, ShaderResourceView> Textures = new Dictionary<string, ShaderResourceView>();
        
        public static ShaderResourceView GetTextureFromDictionary(string textureName)
        {
            if (Textures.ContainsKey(textureName))
                return Textures[textureName];
            return null;
        }

        public static bool HasTexture(string textureName)
        {
            return Textures.ContainsKey(textureName);
        }

        private static void LoadTextures(string folderPrefix)
        {
            string startupPath = System.Windows.Forms.Application.StartupPath;

            if (!Directory.Exists(startupPath + "\\Textures"))
                Directory.CreateDirectory(startupPath + "\\Textures\\");
            if (!Directory.Exists(startupPath + "\\Textures\\Common\\"))
                Directory.CreateDirectory(startupPath + "\\Textures\\Common\\");

            List<string> FilesToLoad = new List<string>();
            FilesToLoad.AddRange(Directory.GetFiles(startupPath + "\\Textures\\Common\\"));
            foreach (string s in Directory.EnumerateDirectories(startupPath + "\\Textures\\"))
            {
                if (Path.GetFileName(s).Equals(folderPrefix) | Path.GetFileName(s).StartsWith(folderPrefix + "_"))
                    FilesToLoad.AddRange(Directory.GetFiles(s));
            }

            foreach (string i in FilesToLoad)
            {
                string textureName = Path.GetFileNameWithoutExtension(i);

                if (Textures.ContainsKey(textureName))
                {
                    if (Textures[textureName] != null)
                        if (!Textures[textureName].IsDisposed)
                            Textures[textureName].Dispose();

                    Textures[textureName] = device.LoadTextureFromFile(i);
                }
                else
                    Textures.Add(textureName, device.LoadTextureFromFile(i));
            }

            ReapplyTextures();
        }

        //private static void LoadTexturesFromTXD(string fileName)
        //{
        //    RWSection[] file = ReadFileMethods.ReadRenderWareFile(fileName);

        //    foreach (string i in FilesToLoad)
        //    {
        //        string textureName = Path.GetFileNameWithoutExtension(i);

        //        if (Textures.ContainsKey(textureName))
        //        {
        //            if (Textures[textureName] != null)
        //                if (!Textures[textureName].IsDisposed)
        //                    Textures[textureName].Dispose();

        //            Textures[textureName] = device.LoadTextureFromFile(i);
        //        }
        //        else
        //            Textures.Add(textureName, device.LoadTextureFromFile(i));
        //    }

        //    ReapplyTextures();
        //}

        private static void ReapplyTextures()
        {
            List<RenderWareModelFile> models = new List<RenderWareModelFile>();
            models.AddRange(BSPList);
            models.AddRange(ShadowColBSPList);
            models.AddRange(DFFRenderer.DFFModels.Values);

            foreach (RenderWareModelFile m in models)
                foreach (SharpMesh mesh in m.meshList)
                    foreach (SharpSubSet sub in mesh.SubSets)
                    {
                        if (sub.DiffuseMap != null)
                            if (!sub.DiffuseMap.IsDisposed)
                                sub.DiffuseMap.Dispose();

                        if (Textures.ContainsKey(sub.DiffuseMapName))
                            sub.DiffuseMap = Textures[sub.DiffuseMapName];
                    }
        }

        public static void SetTexture(string previousTexture, string newTexture)
        {
            List<RenderWareModelFile> models = new List<RenderWareModelFile>();
            models.AddRange(BSPList);
            models.AddRange(ShadowColBSPList);
            models.AddRange(DFFRenderer.DFFModels.Values);

            foreach (RenderWareModelFile m in models)
                foreach (SharpMesh mesh in m.meshList)
                    foreach (SharpSubSet sub in mesh.SubSets)
                    {
                        if (sub.DiffuseMapName == previousTexture)
                            sub.DiffuseMap = Textures[newTexture];
                    }
        }

        // Visibility functions

        private static HashSet<int> VisibleChunks = new HashSet<int>();

        public static void DetermineVisibleChunks()
        {
            VisibleChunks.Clear();
            VisibleChunks.Add(-1);
            Vector3 cameraPos = Camera.GetPosition();

            foreach (LevelEditor.Chunk c in LevelEditor.VisibilityFunctions.ChunkList)
            {
                if ((cameraPos.X > c.Min.X) & (cameraPos.Y > c.Min.Y) & (cameraPos.Z > c.Min.Z) &
                    (cameraPos.X < c.Max.X) & (cameraPos.Y < c.Max.Y) & (cameraPos.Z < c.Max.Z))
                {
                    VisibleChunks.Add(c.number);
                }
            }
        }

        public static bool renderByChunk = true;
                
        // Rendering functions
        
        public static void RenderLevelModel(Matrix viewProjection)
        {
            if (renderByChunk)
                DetermineVisibleChunks();

            device.SetFillModeDefault();
            defaultShader.Apply();

            RenderOpaque(viewProjection);
            RenderAlpha(viewProjection);
        }

        private static void RenderOpaque(Matrix viewProjection)
        {
            device.SetDefaultBlendState();
            device.SetDefaultDepthState();
            device.SetCullModeDefault();

            device.UpdateData(defaultBuffer, viewProjection);
            device.DeviceContext.VertexShader.SetConstantBuffer(0, defaultBuffer);

            for (int j = 0; j < BSPList.Count; j++)
            {
                if ((renderByChunk & !VisibleChunks.Contains(BSPList[j].ChunkNumber)) |
                    (BSPList[j].ChunkName == "A" | BSPList[j].ChunkName == "P" | BSPList[j].ChunkName == "K"))
                    continue;

                if (BSPList[j].isNoCulling) device.SetCullModeNone();
                else device.SetCullModeDefault();

                device.ApplyRasterState();
                device.UpdateAllStates();

                BSPList[j].Render();
            }
        }

        private static void RenderAlpha(Matrix viewProjection)
        {
            for (int j = 0; j < BSPList.Count; j++)
            {
                if ((renderByChunk & !VisibleChunks.Contains(BSPList[j].ChunkNumber)) |
                    (BSPList[j].ChunkName == "O"))
                    continue;

                if (BSPList[j].isNoCulling) device.SetCullModeNone();
                else device.SetCullModeDefault();

                if (BSPList[j].ChunkName == "A" | BSPList[j].ChunkName == "P")
                {
                    device.SetBlendStateAlphaBlend();
                }
                else if (BSPList[j].ChunkName == "K")
                {
                    device.SetBlendStateAdditive();
                }

                device.ApplyRasterState();
                device.UpdateAllStates();

                device.UpdateData(defaultBuffer, viewProjection);
                device.DeviceContext.VertexShader.SetConstantBuffer(0, defaultBuffer);

                BSPList[j].Render();
            }
        }

        // Shadow functions
        public static string currentShadowFolderNamePrefix = "default";

        public static void LoadShadowLevelFolder(string Folder)
        {
            List<Archive> ShadowONEFiles = new List<Archive>();
            currentShadowFolderNamePrefix = Path.GetFileNameWithoutExtension(Folder);

            foreach (string fileName in Directory.GetFiles(Folder))
            {
                if (Path.GetExtension(fileName).ToLower() == ".one")
                    if (!(fileName.Contains("dat") |
                        fileName.Contains("fx") |
                        fileName.Contains("gdt") |
                        fileName.Contains("tex")))
                    {
                        byte[] oneDataBytes = File.ReadAllBytes(fileName);
                        ShadowONEFiles.Add(Archive.FromONEFile(ref oneDataBytes));
                    }
                    else if (fileName.Contains("dat"))
                    {
                        Program.LevelEditor.initVisibilityEditor(true, fileName);
                    }
                    else if (fileName.Contains("fx"))
                    {
                        //  OpenShadowFXONE = new HeroesONEFile(fileName);
                    }
                    else if (fileName.Contains("gdt"))
                    {
                        // OpenShadowGDTONE = new HeroesONEFile(fileName);
                    }
                    else if (fileName.Contains("tex"))
                    {
                        // OpenShadowTexONE = new HeroesONEFile(fileName);
                    }
            }

            SetShadowBSPList(ShadowONEFiles);
        }

        public static List<RenderWareModelFile> ShadowColBSPList = new List<RenderWareModelFile>();

        private static void SetShadowBSPList(List<Archive> OpenShadowONEFiles)
        {
            Dispose();

            Textures.Clear();
            LoadTextures(currentShadowFolderNamePrefix);

            BSPList = new List<RenderWareModelFile>();
            ShadowColBSPList = new List<RenderWareModelFile>();

            ReadFileMethods.isShadow = true;

            foreach (Archive f in OpenShadowONEFiles)
                foreach (ArchiveFile file in f.Files)
                {
                    string ChunkName = Path.GetFileNameWithoutExtension(file.Name);

                    if (ChunkName.Contains("COLI"))
                    {
                        ReadFileMethods.isCollision = true;

                        RenderWareModelFile TempBSPFile = new RenderWareModelFile(file.Name);
                        try
                        {
                            TempBSPFile.ChunkNumber = Convert.ToByte(ChunkName.Split('_').Last());
                        }
                        catch { TempBSPFile.ChunkNumber = -1; };

                        TempBSPFile.isShadowCollision = true;
                        try
                        {
                            byte[] data = file.DecompressThis();
                            TempBSPFile.SetForRendering(ReadFileMethods.ReadRenderWareFile(data), data);
                        }
                        catch (Exception e)
                        {
                            System.Windows.Forms.MessageBox.Show("Error on opening " + file.Name + ": " + e.Message);
                        }
                        ShadowColBSPList.Add(TempBSPFile);

                        ReadFileMethods.isCollision = false;
                    }
                    else
                    {
                        RenderWareModelFile TempBSPFile = new RenderWareModelFile(file.Name);
                        TempBSPFile.SetChunkNumberAndName();
                        byte[] data = file.DecompressThis();
                        TempBSPFile.SetForRendering(ReadFileMethods.ReadRenderWareFile(data), data);
                        BSPList.Add(TempBSPFile);
                    }
                }
        }

        public static void RenderShadowCollisionModel(Matrix viewProjection)
        {
            if (renderByChunk)
                DetermineVisibleChunks();

            device.SetDefaultBlendState();
            device.SetFillModeDefault();
            device.SetCullModeDefault();
            device.ApplyRasterState();
            device.UpdateAllStates();

            device.UpdateData(defaultBuffer, viewProjection);
            device.DeviceContext.VertexShader.SetConstantBuffer(0, defaultBuffer);
            defaultShader.Apply();
                        
            for (int j = 0; j < ShadowColBSPList.Count; j++)
            {
                if (renderByChunk & !VisibleChunks.Contains(ShadowColBSPList[j].ChunkNumber))
                    continue;

                ShadowColBSPList[j].Render();
            }
        }

        // Debug

        public static void ReloadTextures()
        {
            foreach (ShaderResourceView texture in Textures.Values)
                if (texture != null)
                    texture.Dispose();

            LoadTextures(currentFileNamePrefix);
        }
    }
}
