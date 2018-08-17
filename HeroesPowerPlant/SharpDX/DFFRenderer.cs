using System.Collections.Generic;
using System.IO;
using RenderWareFile;
using HeroesPowerPlant.LayoutEditor;
using System.Windows.Forms;
using HeroesONE_R.Structures;
using HeroesONE_R.Structures.Subsctructures;
using System;

namespace HeroesPowerPlant
{
    public class DFFRenderer
    {
        public static HashSet<string> filePaths = new HashSet<string>();
        public static HashSet<string> ObjectDFFNames = new HashSet<string>();
        public static Dictionary<string, RenderWareModelFile> DFFModels = new Dictionary<string, RenderWareModelFile>();
        
        public static void Dispose()
        {
            foreach (RenderWareModelFile r in DFFModels.Values)
                foreach (SharpMesh mesh in r.meshList)
                    mesh.DisposeWithTextures();
        }

        public static void AddDFFFiles(IEnumerable<string> fileNames)
        {
            foreach (ObjectEntry o in Program.LayoutEditor.GetAllObjectEntries())
                if (o.ModelNames != null)
                    foreach (string s in o.ModelNames)
                        if (!ObjectDFFNames.Contains(s))
                            ObjectDFFNames.Add(s);

            foreach (string s in fileNames)
            {
                if (!File.Exists(s))
                {
                    MessageBox.Show("Error: file " + s + " not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    continue;
                }

                if (!filePaths.Contains(s))
                    filePaths.Add(s);

                AddDFFFiles(s);
            }

            Program.LayoutEditor.UpdateAllMatrices();
        }

        private static void AddDFFFiles(string fileName)
        {
            byte[] dataBytes = File.ReadAllBytes(fileName);
            foreach (var j in Archive.FromONEFile(ref dataBytes).Files)
                AddDFF(j);
        }

        private static void AddDFF(ArchiveFile j)
        {
            if (ObjectDFFNames.Contains(j.Name))
            {
                RenderWareModelFile d = new RenderWareModelFile(j.Name);
                byte[] dffData = j.DecompressThis();

                d.SetForRendering(ReadFileMethods.ReadRenderWareFile(dffData), dffData);

                if (DFFModels.ContainsKey(j.Name))
                {
                    MessageBox.Show("Object model " + j.Name + " has already been loaded and will be replaced.");
                    
                    foreach (SharpMesh mesh in DFFModels[j.Name].meshList)
                        mesh.Dispose();

                    DFFModels[j.Name] = d;
                }
                else
                {
                    DFFModels.Add(j.Name, d);
                }
            }
        }

        public static void ClearObjectONEFiles()
        {
            Dispose();

            filePaths = new HashSet<string>();
            ObjectDFFNames = new HashSet<string>();
            DFFModels = new Dictionary<string, RenderWareModelFile>();
        }
    }
}
