using HeroesONELib;
using System.Collections.Generic;
using RenderWareFile;
using HeroesPowerPlant.LayoutEditor;
using System.Windows.Forms;

namespace HeroesPowerPlant
{
    public class DFFRenderer
    {
        public static List<string> filePaths = new List<string>();

        public static void importObjectONEFile()
        {
            OpenFileDialog openFile = new OpenFileDialog()
            {
                Filter = "ONE Files|*.one",
                Multiselect = true
            };

            if (openFile.ShowDialog() == DialogResult.OK)
            {
                filePaths.AddRange(openFile.FileNames);
                AddDFFFiles(openFile.FileNames);
            }
        }

        public static void clearObjectONEFiles()
        {
            foreach (RenderWareModelFile rw in DFFStream.Values)
                foreach (SharpMesh mesh in rw.meshList)
                    mesh.Dispose();

            filePaths = new List<string>();
            ObjectDFFNames = new List<string>();
            DFFStream = new Dictionary<string, RenderWareModelFile>();
        }

        public static List<string> ObjectDFFNames = new List<string>();
        public static Dictionary<string, RenderWareModelFile> DFFStream = new Dictionary<string, RenderWareModelFile>();

        public static void AddDFFFiles(string[] fileNames)
        {
            foreach (string s in fileNames) AddDFFFiles(s);
        }

        public static void AddDFFFiles(string fileName)
        {
            foreach (ObjectEntry o in Program.layoutEditor.layoutSystem.GetAllObjectEntries())
                if (o.ModelNames != null)
                {
                    foreach (string s in o.ModelNames)
                        if (!ObjectDFFNames.Contains(s))
                            ObjectDFFNames.Add(s);
                }
            
            foreach (HeroesONEFile.File j in new HeroesONEFile(fileName).Files)
            {
                AddDFF(j);
            }

            Program.layoutEditor.layoutSystem.ResetMatrices();
        }

        public static void AddDFF(HeroesONEFile.File j)
        {
            if (ObjectDFFNames.Contains(j.Name))
            {
                RenderWareModelFile d = new RenderWareModelFile(j.Name);
                d.SetForRendering(ReadFileMethods.ReadRenderWareFile(j.Data), j.Data);

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
