using System;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using RenderWareFile;
using Microsoft.WindowsAPICodePack.Dialogs;
using static HeroesPowerPlant.BSPRenderer;
using static HeroesPowerPlant.LevelEditor.BSP_IO_Shared;
using static HeroesPowerPlant.LevelEditor.BSP_IO_Heroes;
using static HeroesPowerPlant.LevelEditor.BSP_IO_ShadowCollision;

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
            foreach (RenderWareModelFile item in ShadowColBSPList)
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
                        file.SetForRendering(Program.MainForm.renderer.device, CreateShadowCollisionBSPFile(ReadOBJFile(i, true)), null);
                    }
                    else
                    {
                        file.SetForRendering(Program.MainForm.renderer.device, ReadFileMethods.ReadRenderWareFile(i), File.ReadAllBytes(i));
                    }

                    ShadowColBSPList.Add(file);
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
                        ConvertBSPtoOBJ(a.FileName, ShadowColBSPList[listBoxLevelModels.SelectedIndex]);
                    else if (Path.GetExtension(a.FileName).ToLower() == ".bsp")
                        File.WriteAllBytes(a.FileName, ShadowColBSPList[listBoxLevelModels.SelectedIndex].GetAsByteArray());
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
                            ConvertBSPtoOBJ(Path.Combine(a.FileName, ShadowColBSPList[i].fileName), ShadowColBSPList[i]);
                    else
                        foreach (RenderWareModelFile i in ShadowColBSPList)
                            ConvertBSPtoOBJ(Path.Combine(a.FileName, i.fileName), i);
                }
            }
        }

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < BSPList.Count; i++)
            {
                if (listBoxLevelModels.SelectedIndices.Contains(i))
                {
                    foreach (SharpMesh mesh in ShadowColBSPList[i].meshList)
                        mesh.Dispose();

                    ShadowColBSPList.RemoveAt(i);
                    listBoxLevelModels.Items.RemoveAt(i);
                    i -= 1;
                }
            }
            InitBSPList();
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            foreach (RenderWareModelFile r in ShadowColBSPList)
                foreach (SharpMesh mesh in r.meshList)
                    mesh.Dispose();

            ShadowColBSPList.Clear();
            listBoxLevelModels.Items.Clear();
        }

        private void listBoxLevelModels_SelectedIndexChanged(object sender, EventArgs e)
        {
            uint vertices = 0;
            uint triangles = 0;

            foreach (int i in listBoxLevelModels.SelectedIndices)
            {
                vertices += ShadowColBSPList[i].vertexAmount;
                triangles += ShadowColBSPList[i].triangleAmount;
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
                ShadowColBSPList[listBoxLevelModels.SelectedIndex].fileName = newName;
                listBoxLevelModels.Items[listBoxLevelModels.SelectedIndex] = newName;

                try
                {
                    ShadowColBSPList[listBoxLevelModels.SelectedIndex].ChunkNumber =
                        Convert.ToByte(Path.GetFileNameWithoutExtension(newName).Split('_').Last());
                }
                catch
                {
                    ShadowColBSPList[listBoxLevelModels.SelectedIndex].ChunkNumber = -1;
                };
            }
        }
    }
}
