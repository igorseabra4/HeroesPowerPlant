using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using HeroesONELib;
using static HeroesPowerPlant.LevelEditor.VisibilityFunctions;
using static HeroesPowerPlant.LevelEditor.LevelEditorFunctions;
using static HeroesPowerPlant.BSPRenderer;
using SharpDX;
using RenderWareFile;
using RenderWareFile.Sections;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace HeroesPowerPlant.LevelEditor
{
    public partial class ShadowCollisionEditor : Form
    {
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.WindowsShutDown) return;
            if (e.CloseReason == CloseReason.FormOwnerClosing) return;

            e.Cancel = true;
            Hide();
        }

        private void ShadowCollisionEditor_Load(object sender, EventArgs e)
        {
            TopMost = true;
        }

        public ShadowCollisionEditor()
        {
            InitializeComponent();
        }

        public void InitBSPList()
        {
            listBoxLevelModels.Items.Clear();
            foreach (RenderWareModelFile item in ShadowCollisionBSPStream)
            {
                listBoxLevelModels.Items.Add(item.fileName);
            }
        }

        private void buttonImport_Click(object sender, EventArgs e)
        {
            OpenFileDialog a = new OpenFileDialog()
            {
                Filter = "All supported types|*.obj;*.bsp|OBJ Files|*.obj|BSP Files|*.bsp|All files|*.*",
                Multiselect = true
            };
            if (a.ShowDialog() == DialogResult.OK)
            {
                foreach (string i in a.FileNames)
                {
                    ReadFileMethods.isCollision = true;

                    RenderWareModelFile file = new RenderWareModelFile(Path.GetFileNameWithoutExtension(i) + ".BSP");
                    file.isShadowCollision = true;

                    try
                    {
                        file.ChunkNumber = Convert.ToByte(Path.GetFileNameWithoutExtension(i).Split('_').Last());
                    }
                    catch { file.ChunkNumber = -1; };

                    if (Path.GetExtension(i).ToLower() == ".obj")
                    {
                        file.SetForRendering(CreateShadowCollisionBSPFile(ReadOBJFile(i, true)), null);
                    }
                    else
                    {
                        file.SetForRendering(ReadFileMethods.ReadRenderWareFile(i), File.ReadAllBytes(i));
                    }

                    ShadowCollisionBSPStream.Add(file);
                    listBoxLevelModels.Items.Add(file.fileName);

                    ReadFileMethods.isCollision = false;
                }

                buttonExport.Enabled = true;
            }
        }

        private void buttonExport_Click(object sender, EventArgs e)
        {
            if (listBoxLevelModels.Items.Count == 0) return;

            if (listBoxLevelModels.SelectedIndices.Count == 1)
            {
                SaveFileDialog a = new SaveFileDialog()
                {
                    Filter = "OBJ Files|*.obj|BSP Files|*.bsp",
                    FileName = Path.ChangeExtension(listBoxLevelModels.GetItemText(listBoxLevelModels.SelectedItem), ".obj")
                };
                if (a.ShowDialog() == DialogResult.OK)
                {
                    if (Path.GetExtension(a.FileName).ToLower() == ".obj")
                        ConvertBSPtoOBJ(a.FileName, ShadowCollisionBSPStream[listBoxLevelModels.SelectedIndex]);
                    else if (Path.GetExtension(a.FileName).ToLower() == ".bsp")
                        File.WriteAllBytes(a.FileName, ShadowCollisionBSPStream[listBoxLevelModels.SelectedIndex].GetAsByteArray());
                }
            }
            else
            {
                CommonOpenFileDialog a = new CommonOpenFileDialog()
                {
                    IsFolderPicker = true
                };
                if (a.ShowDialog() == CommonFileDialogResult.Ok)
                {
                    if (listBoxLevelModels.SelectedIndices.Count > 1)
                        foreach (int i in listBoxLevelModels.SelectedIndices)
                            ConvertBSPtoOBJ(Path.Combine(a.FileName, ShadowCollisionBSPStream[i].fileName), ShadowCollisionBSPStream[i]);
                    else
                        foreach (RenderWareModelFile i in ShadowCollisionBSPStream)
                            ConvertBSPtoOBJ(Path.Combine(a.FileName, i.fileName), i);
                }
            }
        }

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < BSPStream.Count; i++)
            {
                if (listBoxLevelModels.SelectedIndices.Contains(i))
                {
                    foreach (SharpMesh mesh in ShadowCollisionBSPStream[i].meshList)
                        mesh.Dispose();

                    ShadowCollisionBSPStream.RemoveAt(i);
                    listBoxLevelModels.Items.RemoveAt(i);
                    i -= 1;
                }
            }
            InitBSPList();
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            foreach (RenderWareModelFile r in ShadowCollisionBSPStream)
                foreach (SharpMesh mesh in r.meshList)
                    mesh.Dispose();

            ShadowCollisionBSPStream.Clear();
            listBoxLevelModels.Items.Clear();
        }

        private void listBoxLevelModels_SelectedIndexChanged(object sender, EventArgs e)
        {
            uint vertices = 0;
            uint triangles = 0;

            foreach (int i in listBoxLevelModels.SelectedIndices)
            {
                vertices += ShadowCollisionBSPStream[i].vertexAmount;
                triangles += ShadowCollisionBSPStream[i].triangleAmount;
            }

            labelVertexAmount.Text = "Vertices: " + vertices.ToString();
            labelTriangleAmount.Text = "Triangles: " + triangles.ToString();
        }
        
        public static string NewName;
        public static bool ChangeName;

        private void listBoxLevelModels_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (listBoxLevelModels.SelectedIndices.Count == 1)
            {
                string newName = EditBSPName.GetName(listBoxLevelModels.Items[listBoxLevelModels.SelectedIndex].ToString());
                ShadowCollisionBSPStream[listBoxLevelModels.SelectedIndex].fileName = newName;
                listBoxLevelModels.Items[listBoxLevelModels.SelectedIndex] = newName;

                try
                {
                    ShadowCollisionBSPStream[listBoxLevelModels.SelectedIndex].ChunkNumber =
                        Convert.ToByte(Path.GetFileNameWithoutExtension(newName).Split('_').Last());
                }
                catch
                {
                    ShadowCollisionBSPStream[listBoxLevelModels.SelectedIndex].ChunkNumber = -1;
                };
            }
        }
    }
}
