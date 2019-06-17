using System.Collections.Generic;
using System.IO;
using SharpDX.Direct3D11;
using HeroesONE_R.Structures;
using HeroesONE_R.Structures.Subsctructures;
using RenderWareFile;
using RenderWareFile.Sections;
using System;
using System.Windows.Forms;

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
            return SharpRenderer.whiteDefault;
        }

        public static void LoadTexturesFromTXD(string filePath, SharpRenderer renderer, BSPRenderer bspRenderer)
        { 
            if (!Path.GetFileName(filePath).Equals("temp.txd"))
                OpenTXDfiles.Add(filePath);

            try
            {
                if (Path.GetExtension(filePath).ToLower().Equals(".one"))
                {
                    byte[] oneFile = File.ReadAllBytes(filePath);
                    Archive archive = Archive.FromONEFile(ref oneFile);

                    foreach (ArchiveFile archiveFile in archive.Files)
                        if (Path.GetExtension(archiveFile.Name).ToLower().Equals(".txd"))
                            SetupTextureDisplay(archiveFile.DecompressThis(), renderer, bspRenderer);
                }
                else if (Path.GetExtension(filePath).ToLower().Equals(".txd"))
                    LoadTexturesFromTXD(File.ReadAllBytes(filePath), renderer, bspRenderer);

                else throw new InvalidDataException(filePath);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error opening " + filePath + ": " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                OpenTXDfiles.Remove(filePath);
            }
        }

        public static void LoadTexturesFromTXD(byte[] txdData, SharpRenderer renderer, BSPRenderer bspRenderer)
        {
            LoadTexturesFromTXD(ReadFileMethods.ReadRenderWareFile(txdData), renderer, bspRenderer);
        }

        public static void LoadTexturesFromTXD(RWSection[] txdFile, SharpRenderer renderer, BSPRenderer bspRenderer)
        {
            foreach (RWSection rw in txdFile)
                if (rw is TextureDictionary_0016 td)
                    foreach (TextureNative_0015 tn in td.textureNativeList)
                        AddTextureNative(tn.textureNativeStruct, renderer);

            ReapplyTextures(renderer, bspRenderer);
        }

        private static void AddTextureNative(TextureNativeStruct_0001 tnStruct, SharpRenderer renderer)
        {
            if (Textures.ContainsKey(tnStruct.textureName))
            {
                if (Textures[tnStruct.textureName] != null)
                    if (!Textures[tnStruct.textureName].IsDisposed)
                        Textures[tnStruct.textureName].Dispose();

                Textures[tnStruct.textureName] = renderer.Device.LoadTextureFromRenderWareNative(tnStruct);
            }
            else
                Textures.Add(tnStruct.textureName, renderer.Device.LoadTextureFromRenderWareNative(tnStruct));
        }
        
        public static void LoadTexturesFromFolder(string folderName, SharpRenderer renderer, BSPRenderer bspRenderer)
        {
            OpenTextureFolders.Add(folderName);

            foreach (string i in Directory.GetFiles(folderName))
                if (Path.GetExtension(i).ToLower().Equals(".png"))
                    AddTexturePNG(i, renderer);

            ReapplyTextures(renderer, bspRenderer);
        }

        private static void AddTexturePNG(string path, SharpRenderer renderer)
        {
            string textureName = Path.GetFileNameWithoutExtension(path);

            if (Textures.ContainsKey(textureName))
            {
                if (Textures[textureName] != null)
                    if (!Textures[textureName].IsDisposed)
                        Textures[textureName].Dispose();

                Textures[textureName] = renderer.Device.LoadTextureFromFile(path);
            }
            else
                Textures.Add(textureName, renderer.Device.LoadTextureFromFile(path));
        }

        public static void ReapplyTextures(SharpRenderer renderer, BSPRenderer bspRenderer)
        {
            List<RenderWareModelFile> models = new List<RenderWareModelFile>();
            models.AddRange(bspRenderer.BSPList);
            models.AddRange(bspRenderer.ShadowColBSPList);
            models.AddRange(renderer.dffRenderer.DFFModels.Values);

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
                                        if (sub.DiffuseMap != SharpRenderer.whiteDefault)
                                            sub.DiffuseMap.Dispose();

                                sub.DiffuseMap = Textures[sub.DiffuseMapName];
                            }
                        }
                        else
                        {
                            if (sub.DiffuseMap != null)
                                if (!sub.DiffuseMap.IsDisposed)
                                    if (sub.DiffuseMap != SharpRenderer.whiteDefault)
                                        sub.DiffuseMap.Dispose();

                            sub.DiffuseMap = SharpRenderer.whiteDefault;
                        }
                    }
        }

        public static void SetTextureForAnimation(string diffuseMapName, string newMapName, BSPRenderer bspRenderer, DFFRenderer dffRenderer)
        {
            List<RenderWareModelFile> models = new List<RenderWareModelFile>();
            models.AddRange(bspRenderer.BSPList);
            models.AddRange(bspRenderer.ShadowColBSPList);
            models.AddRange(dffRenderer.DFFModels.Values);

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

        public static void ClearTextures(SharpRenderer renderer, BSPRenderer bspRenderer)
        {
            OpenTXDfiles.Clear();
            OpenTextureFolders.Clear();
            DisposeTextures();
            Textures.Clear();
            ReapplyTextures(renderer, bspRenderer);
        }

        private static string txdGenFolder => Application.StartupPath + "\\Tools\\txdgen_1.0\\";
        private static string tempGcTxdsDir => txdGenFolder + "Temp\\txds_gc\\";
        private static string tempPcTxdsDir => txdGenFolder + "Temp\\txds_pc\\";
        private static string pathToGcTXD => tempGcTxdsDir + "temp.txd";
        private static string pathToPcTXD => tempPcTxdsDir + "temp.txd";
        
        public static void SetupTextureDisplay(byte[] txdFile, SharpRenderer renderer, BSPRenderer bspRenderer)
        {
            if (!Directory.Exists(tempGcTxdsDir))
                Directory.CreateDirectory(tempGcTxdsDir);
            if (!Directory.Exists(tempPcTxdsDir))
                Directory.CreateDirectory(tempPcTxdsDir);

            File.WriteAllBytes(pathToGcTXD, txdFile);

            PerformTXDConversionExternal();

            LoadTexturesFromTXD(pathToPcTXD, renderer, bspRenderer);
            ReapplyTextures(renderer, bspRenderer);

            File.Delete(pathToGcTXD);
            File.Delete(pathToPcTXD);
        }

        private static void PerformTXDConversionExternal(bool toPC = true, bool compress = false, bool generateMipmaps = false)
        {
            string ini =
                "[Main]\r\n" +

                (toPC ?
                "gameRoot=" + tempGcTxdsDir + "\r\n" +
                "outputRoot=" + tempPcTxdsDir + "\r\n" +
                "targetVersion=VC\r\n" +
                "targetPlatform=PC\r\n"
                :
                "gameRoot=" + tempPcTxdsDir + "\r\n" +
                "outputRoot=" + tempGcTxdsDir + "\r\n" +
                "targetVersion=VC\r\n" +
                "targetPlatform=Gamecube\r\n") +

                "clearMipmaps=false\r\n" +
                "generateMipmaps=" + generateMipmaps.ToString().ToLower() + "\r\n" +
                "mipGenMode=default\r\n" +
                "mipGenMaxLevel=10\r\n" +
                "improveFiltering=true\r\n" +
                "compressTextures=" + compress.ToString().ToLower() + "\r\n" +
                "compressionQuality=1.0\r\n" +
                "palRuntimeType=PNGQUANT\r\n" +
                "dxtRuntimeType=SQUISH\r\n" +
                "warningLevel=1\r\n" +
                "ignoreSecureWarnings=true\r\n" +
                "reconstructIMGArchives=false\r\n" +
                "fixIncompatibleRasters=true\r\n" +
                "dxtPackedDecompression=false\r\n" +
                "imgArchivesCompressed=false\r\n" +
                "ignoreSerializationRegions=true";

            string curr = Directory.GetCurrentDirectory();
            Directory.SetCurrentDirectory(txdGenFolder);

            File.WriteAllText("txdgen.ini", ini);

            System.Diagnostics.Process txdgen = System.Diagnostics.Process.Start("txdgen.exe");
            txdgen.WaitForExit();

            Directory.SetCurrentDirectory(curr);
        }
    }
}
