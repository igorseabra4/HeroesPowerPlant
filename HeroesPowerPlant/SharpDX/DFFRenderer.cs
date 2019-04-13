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
        private MainForm.MainForm mainForm;

        public HashSet<string> filePaths = new HashSet<string>();
        public HashSet<string> ObjectDFFNames = new HashSet<string>();
        public Dictionary<string, RenderWareModelFile> DFFModels = new Dictionary<string, RenderWareModelFile>();
        
        public DFFRenderer(MainForm.MainForm mainForm)
        {
            this.mainForm = mainForm;
        }

        public void Dispose()
        {
            foreach (RenderWareModelFile r in DFFModels.Values)
                foreach (SharpMesh mesh in r.meshList)
                    mesh.Dispose();
        }

        public void AddDFFFiles(IEnumerable<string> fileNames)
        {
            foreach (ObjectEntry o in LayoutEditorSystem.GetAllObjectEntries())
                if (o.ModelNames != null)
                    foreach (string s in o.ModelNames)
                        if (!ObjectDFFNames.Contains(s))
                            ObjectDFFNames.Add(s);

            foreach (string s in fileNames)
            {
                if (!File.Exists(s))
                {
                    MessageBox.Show("Error: file not found: " + s, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    continue;
                }

                if (!filePaths.Contains(s))
                    filePaths.Add(s);

                AddDFFFiles(s);
            }
            
            foreach (var v in mainForm.LayoutEditors)
                v.UpdateAllMatrices();

            mainForm.ReapplyTextures();
        }

        private void AddDFFFiles(string fileName)
        {
            byte[] dataBytes = File.ReadAllBytes(fileName);
            foreach (var j in Archive.FromONEFile(ref dataBytes).Files)
                AddDFF(j);
        }

        private void AddDFF(ArchiveFile j)
        {
            if (ObjectDFFNames.Contains(j.Name))
            {
                RenderWareModelFile d = new RenderWareModelFile(j.Name);
                byte[] dffData = j.DecompressThis();

                try
                {
                    d.SetForRendering(mainForm.renderer.Device, ReadFileMethods.ReadRenderWareFile(dffData), dffData);

                    if (DFFModels.ContainsKey(j.Name))
                    {
                        foreach (SharpMesh mesh in DFFModels[j.Name].meshList)
                            mesh.Dispose();

                        DFFModels[j.Name] = d;
                    }
                    else
                    {
                        DFFModels.Add(j.Name, d);
                    }
                }
                catch (Exception ex)
                {
                    // TODO: Investigate RenderWareFile failing to open certain DFFs.
                    MessageBox.Show($"RenderWareFile: Failed to open DFF model file; {j.Name} | Exception Message: {ex.Message}");
                }
            }
            else if (Path.GetExtension(j.Name).ToLower().Equals(".txd"))
            {
                try
                {
                    byte[] txdData = j.DecompressThis();

                    mainForm.LoadTexturesFromTXD(txdData);
                }
                catch (Exception ex)
                {
                    #if DEBUG
                    MessageBox.Show("Unable to load textures from TXD: " + ex.Message);
                    #endif
                }
            }
        }

        public void ClearObjectONEFiles()
        {
            Dispose();

            filePaths = new HashSet<string>();
            ObjectDFFNames = new HashSet<string>();
            DFFModels = new Dictionary<string, RenderWareModelFile>();
        }
    }
}
