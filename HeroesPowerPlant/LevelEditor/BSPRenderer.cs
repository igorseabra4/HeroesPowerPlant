using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using SharpDX;
using SharpDX.Direct3D11;
using SharpDX.DXGI;
using HeroesONELib;
using RenderWareFile;
using static HeroesPowerPlant.SharpRenderer;

namespace HeroesPowerPlant
{
    public class BSPRenderer
    {
        public static string currentFileNamePrefix = "default";

        public static List<RenderWareModelFile> BSPStream = new List<RenderWareModelFile>();

        public static void SetHeroesMeshStream(HeroesONEFile heroesONEfile)
        {
            ReadFileMethods.isShadow = false;

            foreach (RenderWareModelFile r in BSPStream)
                foreach (SharpMesh mesh in r.meshList)
                    mesh.Dispose();

            foreach (RenderWareModelFile r in ShadowCollisionBSPStream)
                foreach (SharpMesh mesh in r.meshList)
                    mesh.Dispose();

            LoadTextures(currentFileNamePrefix);

            ReadFileMethods.isShadow = false;

            ShadowCollisionBSPStream = new List<RenderWareModelFile>();
            BSPStream = new List<RenderWareModelFile>(heroesONEfile.Files.Count);

            foreach (HeroesONEFile.File file in heroesONEfile.Files)
            {
                if (Path.GetExtension(file.Name).ToLower() != ".bsp")
                    continue;
                
                RenderWareModelFile TempBSPFile = new RenderWareModelFile(file.Name);
                TempBSPFile.SetChunkNumberAndName();
                TempBSPFile.SetForRendering(ReadFileMethods.ReadRenderWareFile(file.Data), file.Data);
                BSPStream.Add(TempBSPFile);
            }
        }

        // Texture loader

        public const string DefaultTexture = "default";
        public static Dictionary<string, ShaderResourceView> TextureStream = new Dictionary<string, ShaderResourceView>();
        public static ShaderResourceView whiteDefault;
        
        public static void LoadTextures(string folderPrefix)
        {
            foreach (ShaderResourceView texture in TextureStream.Values)
                texture.Dispose();

            if (whiteDefault != null)
                if (!whiteDefault.IsDisposed)
                    whiteDefault.Dispose();

            TextureStream.Clear();            
            whiteDefault = device.LoadTextureFromFile("Resources\\WhiteDefault.png");

            List<string> FilesToLoad = new List<string>();
            FilesToLoad.AddRange(Directory.GetFiles(System.Windows.Forms.Application.StartupPath + "\\Textures\\Common\\"));
            foreach (string s in Directory.EnumerateDirectories(System.Windows.Forms.Application.StartupPath + "\\Textures\\"))
            {
                if (Path.GetFileName(s).Equals(folderPrefix) | Path.GetFileName(s).StartsWith(folderPrefix + "_"))
                    FilesToLoad.AddRange(Directory.GetFiles(s));
            }
            
            bool showMessageBox = false;
            foreach (string i in FilesToLoad)
            {
                string textureName = Path.GetFileNameWithoutExtension(i);

                if (TextureStream.ContainsKey(textureName))
                    showMessageBox = true;
                else
                    try
                    {
                        TextureStream.Add(textureName, device.LoadTextureFromFile(i));
                    }
                    catch (Exception e)
                    {
                        System.Windows.Forms.MessageBox.Show(e.Message);
                    }
            }
            if (showMessageBox) System.Windows.Forms.MessageBox.Show("HeroesPowerPlant found duplicates of one or more textures. The first one will be used.");
        }
        
        // Visibility functions

        public static HashSet<int> VisibleChunks = new HashSet<int>();

        public static void DetermineVisibleChunks()
        {
            VisibleChunks = new HashSet<int>() { -1 };
            Vector3 cameraPos = SharpRenderer.Camera.GetPosition();

            foreach (LevelEditor.VisibilityFunctions.Chunk c in LevelEditor.VisibilityFunctions.ChunkList)
            {
                if ((cameraPos.X > c.Min.X) & (cameraPos.Y > c.Min.Y) & (cameraPos.Z > c.Min.Z) &
                    (cameraPos.X < c.Max.X) & (cameraPos.Y < c.Max.Y) & (cameraPos.Z < c.Max.Z)
                    & !VisibleChunks.Contains(c.number))
                {
                    VisibleChunks.Add(c.number);
                }
            }
        }

        public static bool renderByChunk = true;

        public static void SetRenderByChunk(bool value)
        {
            renderByChunk = value;
        }
        
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

            for (int j = 0; j < BSPStream.Count; j++)
            {
                if ((renderByChunk & !VisibleChunks.Contains(BSPStream[j].ChunkNumber)) |
                    (BSPStream[j].ChunkName == "A" | BSPStream[j].ChunkName == "P" | BSPStream[j].ChunkName == "K"))
                    continue;

                if (BSPStream[j].isNoCulling) device.SetCullModeNone();
                else device.SetCullModeDefault();

                device.ApplyRasterState();
                device.UpdateAllStates();

                BSPStream[j].Render();
            }
        }

        private static void RenderAlpha(Matrix viewProjection)
        {
            for (int j = 0; j < BSPStream.Count; j++)
            {
                if ((renderByChunk & !VisibleChunks.Contains(BSPStream[j].ChunkNumber)) |
                    (BSPStream[j].ChunkName == "O"))
                    continue;

                if (BSPStream[j].isNoCulling) device.SetCullModeNone();
                else device.SetCullModeDefault();

                if (BSPStream[j].ChunkName == "A" | BSPStream[j].ChunkName == "P")
                {
                    device.SetBlendStateAlphaBlend();
                }
                else if (BSPStream[j].ChunkName == "K")
                {
                    device.SetBlendStateAdditive();
                }

                device.ApplyRasterState();
                device.UpdateAllStates();

                device.UpdateData(defaultBuffer, viewProjection);
                device.DeviceContext.VertexShader.SetConstantBuffer(0, defaultBuffer);

                BSPStream[j].Render();
            }
        }

        // Shadow functions
        public static string currentShadowFolderNamePrefix = "default";

        public static void LoadShadowLevelFolder(string Folder)
        {
            List<HeroesONEFile> OpenShadowONEFiles = new List<HeroesONEFile>();
            currentShadowFolderNamePrefix = Path.GetFileNameWithoutExtension(Folder);

            foreach (string fileName in Directory.GetFiles(Folder))
            {
                if (Path.GetExtension(fileName).ToLower() == ".one"
                    & !fileName.Contains("dat")
                    & !fileName.Contains("fx")
                    & !fileName.Contains("gdt")
                    & !fileName.Contains("tex"))
                {
                    OpenShadowONEFiles.Add(new HeroesONEFile(fileName));
                }
                else if (Path.GetExtension(fileName).ToLower() == ".one" & fileName.Contains("dat"))
                {
                    Program.levelEditor.initVisibilityEditor(true, fileName);
                }
                else if (Path.GetExtension(fileName).ToLower() == ".one" & fileName.Contains("fx"))
                {
                  //  OpenShadowFXONE = new HeroesONEFile(fileName);
                }
                else if (Path.GetExtension(fileName).ToLower() == ".one" & fileName.Contains("gdt"))
                {
                   // OpenShadowGDTONE = new HeroesONEFile(fileName);
                }
                else if (Path.GetExtension(fileName).ToLower() == ".one" & fileName.Contains("tex"))
                {
                   // OpenShadowTexONE = new HeroesONEFile(fileName);
                }
            }

            SetShadowMeshStream(OpenShadowONEFiles);
        }

        public static List<RenderWareModelFile> ShadowCollisionBSPStream = new List<RenderWareModelFile>();

        public static void SetShadowMeshStream(List<HeroesONEFile> OpenShadowONEFiles)
        {
            foreach (RenderWareModelFile r in BSPStream)
                foreach (SharpMesh mesh in r.meshList)
                    mesh.Dispose();

            foreach (RenderWareModelFile r in ShadowCollisionBSPStream)
                foreach (SharpMesh mesh in r.meshList)
                    mesh.Dispose();

            LoadTextures(currentShadowFolderNamePrefix);

            BSPStream = new List<RenderWareModelFile>();
            ShadowCollisionBSPStream = new List<RenderWareModelFile>();

            ReadFileMethods.isShadow = true;

            foreach (HeroesONEFile f in OpenShadowONEFiles)
                foreach (HeroesONEFile.File file in f.Files)
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

                        TempBSPFile.isCollision = true;
                        try
                        {
                            TempBSPFile.SetForRendering(ReadFileMethods.ReadRenderWareFile(file.Data), file.Data);
                        }
                        catch (Exception e)
                        {
                            System.Windows.Forms.MessageBox.Show("Error on opening " + file.Name + ": " + e.Message);
                        }
                        ShadowCollisionBSPStream.Add(TempBSPFile);

                        ReadFileMethods.isCollision = false;
                    }
                    else
                    {
                        RenderWareModelFile TempBSPFile = new RenderWareModelFile(file.Name);
                        TempBSPFile.SetChunkNumberAndName();
                        TempBSPFile.SetForRendering(ReadFileMethods.ReadRenderWareFile(file.Data), file.Data);
                        BSPStream.Add(TempBSPFile);
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
                        
            for (int j = 0; j < ShadowCollisionBSPStream.Count; j++)
            {
                if (renderByChunk & !VisibleChunks.Contains(ShadowCollisionBSPStream[j].ChunkNumber))
                    continue;

                ShadowCollisionBSPStream[j].Render();
            }
        }
    }
}
