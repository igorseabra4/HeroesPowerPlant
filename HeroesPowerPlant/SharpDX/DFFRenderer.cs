using System.Collections.Generic;
using System.IO;
using RenderWareFile;
using HeroesPowerPlant.LayoutEditor;
using System.Windows.Forms;
using HeroesONE_R.Structures;
using HeroesONE_R.Structures.Subsctructures;

namespace HeroesPowerPlant
{
    public class DFFRenderer
    {
        public static HashSet<string> filePaths = new HashSet<string>();
        public static HashSet<string> ObjectDFFNames = new HashSet<string>();
        public static Dictionary<string, RenderWareModelFile> DFFStream = new Dictionary<string, RenderWareModelFile>();

        public static void importObjectONEFile()
        {
            OpenFileDialog openFile = new OpenFileDialog()
            {
                Filter = "ONE Files|*.one",
                Multiselect = true
            };

            if (openFile.ShowDialog() == DialogResult.OK)
            {
                AddDFFFiles(openFile.FileNames);
            }
        }

        public static void clearObjectONEFiles()
        {
            foreach (RenderWareModelFile rw in DFFStream.Values)
                foreach (SharpMesh mesh in rw.meshList)
                    mesh.Dispose();

            filePaths = new HashSet<string>();
            ObjectDFFNames = new HashSet<string>();
            DFFStream = new Dictionary<string, RenderWareModelFile>();
        }

        public static void AddDFFFiles(IEnumerable<string> fileNames)
        {
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

            Program.LayoutEditor.layoutSystem.ResetMatrices();
        }

        private static void AddDFFFiles(string fileName)
        {
            foreach (ObjectEntry o in Program.LayoutEditor.layoutSystem.GetAllObjectEntries())
                if (o.ModelNames != null)
                {
                    foreach (string s in o.ModelNames)
                        if (!ObjectDFFNames.Contains(s))
                            ObjectDFFNames.Add(s);
                }

            byte[] dataBytes = File.ReadAllBytes(fileName);
            foreach (var j in Archive.FromONEFile(ref dataBytes).Files)
            {
                AddDFF(j);
            }
        }


        private static void AddDFF(ArchiveFile j)
        {
            if (ObjectDFFNames.Contains(j.Name))
            {
                RenderWareModelFile d = new RenderWareModelFile(j.Name);
                byte[] dffData = j.DecompressThis();

                d.SetForRendering(ReadFileMethods.ReadRenderWareFile(dffData), dffData);

                if (DFFStream.ContainsKey(j.Name))
                {
                    MessageBox.Show("Object model " + j.Name + " has already been loaded and will be replaced.");
                    
                    foreach (SharpMesh mesh in DFFStream[j.Name].meshList)
                        mesh.Dispose();

                    DFFStream[j.Name] = d;
                }
                else
                {
                    DFFStream.Add(j.Name, d);
                }
            }
        }
    }
}
