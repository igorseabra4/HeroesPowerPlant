using RenderWareFile;
using RenderWareFile.Sections;
using SharpDX.Direct3D11;
using System;
using System.Collections.Generic;
using System.IO;
using static HeroesPowerPlant.SharpRenderer;

namespace HeroesPowerPlant
{
    public static class TextureManager
    {
        public const string DefaultTexture = "default";
        private static Dictionary<string, ShaderResourceView> Textures = new Dictionary<string, ShaderResourceView>();

        public static HashSet<string> OpenTXDfiles { get; private set; } = new HashSet<string>();
        public static HashSet<string> OpenTextureFolders { get; private set; } = new HashSet<string>();
        
        public static bool HasTexture(string textureName)
        {
            return Textures.ContainsKey(textureName);
        }

        public static ShaderResourceView GetTextureFromDictionary(string textureName)
        {
            if (Textures.ContainsKey(textureName))
                return Textures[textureName];
            return null;
        }

        public static void LoadTexturesFromTXD(string fileName)
        {
            OpenTXDfiles.Add(fileName);
            RWSection[] file = ReadFileMethods.ReadRenderWareFile(fileName);
            LoadTexturesFromTXD(fileName, file);
        }

        public static void LoadTexturesFromTXD(string fileName, byte[] txdData)
        {
            RWSection[] file = ReadFileMethods.ReadRenderWareFile(txdData);
            LoadTexturesFromTXD(fileName, file);
        }

        public static void LoadTexturesFromTXD(string fileName, RWSection[] txdFile)
        {
            foreach (RWSection rw in txdFile)
                if (rw is TextureDictionary_0016 td)
                    foreach (TextureNative_0015 tn in td.textureNativeList)
                        AddTextureNative(tn.textureNativeStruct);

            ReapplyTextures();
        }

        private static void AddTextureNative(TextureNativeStruct_0001 tnStruct)
        {
            if (Textures.ContainsKey(tnStruct.textureName))
            {
                if (Textures[tnStruct.textureName] != null)
                    if (!Textures[tnStruct.textureName].IsDisposed)
                        Textures[tnStruct.textureName].Dispose();

                Textures[tnStruct.textureName] = device.LoadTextureFromRenderWareNative(tnStruct);
            }
            else
                Textures.Add(tnStruct.textureName, device.LoadTextureFromRenderWareNative(tnStruct));
        }

        public static void LoadTexturesFromFolder(HashSet<string> fileNames)
        {
            foreach (string s in fileNames)
                LoadTexturesFromFolder(s);
        }

        public static void LoadTexturesFromFolder(string folderName)
        {
            OpenTextureFolders.Add(folderName);

            foreach (string i in Directory.GetFiles(folderName))
                if (Path.GetExtension(i).ToLower().Equals(".png"))
                    AddTexturePNG(i);

            ReapplyTextures();
        }

        private static void AddTexturePNG(string path)
        {
            string textureName = Path.GetFileNameWithoutExtension(path);

            if (Textures.ContainsKey(textureName))
            {
                if (Textures[textureName] != null)
                    if (!Textures[textureName].IsDisposed)
                        Textures[textureName].Dispose();

                Textures[textureName] = device.LoadTextureFromFile(path);
            }
            else
                Textures.Add(textureName, device.LoadTextureFromFile(path));
        }

        public static void ReapplyTextures()
        {
            List<RenderWareModelFile> models = new List<RenderWareModelFile>();
            models.AddRange(BSPRenderer.BSPList);
            models.AddRange(BSPRenderer.ShadowColBSPList);
            models.AddRange(DFFRenderer.DFFModels.Values);

            foreach (RenderWareModelFile m in models)
                foreach (SharpMesh mesh in m.meshList)
                    foreach (SharpSubSet sub in mesh.SubSets)
                    {
                        if (Textures.ContainsKey(sub.DiffuseMapName))
                        {
                            if (sub.DiffuseMap != Textures[sub.DiffuseMapName])
                            {
                                if (sub.DiffuseMap != null)
                                    if (!sub.DiffuseMap.IsDisposed)
                                        sub.DiffuseMap.Dispose();

                                sub.DiffuseMap = Textures[sub.DiffuseMapName];
                            }
                        }
                        else
                        {
                            if (sub.DiffuseMap != null)
                                if (!sub.DiffuseMap.IsDisposed)
                                    sub.DiffuseMap.Dispose();

                            sub.DiffuseMap = whiteDefault;
                        }
                    }
        }

        public static void SetTextureForAnimation(string diffuseMapName, string newMapName)
        {
            List<RenderWareModelFile> models = new List<RenderWareModelFile>();
            models.AddRange(BSPRenderer.BSPList);
            models.AddRange(BSPRenderer.ShadowColBSPList);

            foreach (RenderWareModelFile m in models)
                foreach (SharpMesh mesh in m.meshList)
                    foreach (SharpSubSet sub in mesh.SubSets)
                    {
                        if (sub.DiffuseMapName == diffuseMapName)
                            sub.DiffuseMap = Textures[newMapName];
                    }
        }

        public static void DisposeTextures()
        {
            foreach (ShaderResourceView texture in Textures.Values)
                if (texture != null)
                    texture.Dispose();
        }

        public static void ClearTextures()
        {
            OpenTXDfiles.Clear();
            OpenTextureFolders.Clear();
            DisposeTextures();
            Textures.Clear();
            ReapplyTextures();
        }
    }
}
