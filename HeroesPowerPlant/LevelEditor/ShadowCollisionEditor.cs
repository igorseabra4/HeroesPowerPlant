using HeroesPowerPlant.Shared.IO.Config;
using Ookii.Dialogs.WinForms;
using RenderWareFile;
using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using static HeroesPowerPlant.LevelEditor.BSP_IO_Heroes;
using static HeroesPowerPlant.LevelEditor.BSP_IO_ShadowCollision;
using static HeroesPowerPlant.LevelEditor.BSP_IO_Shared;

namespace HeroesPowerPlant.LevelEditor
{
    public partial class ShadowCollisionEditor : Form
    {
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.WindowsShutDown)
                return;
            if (e.CloseReason == CloseReason.FormOwnerClosing)
                return;

            e.Cancel = true;
            Hide();
        }

        private void ShadowCollisionEditor_Load(object sender, EventArgs e)
        {
            if (HPPConfig.GetInstance().LegacyWindowPriorityBehavior)
                TopMost = true;
            else
                TopMost = false;
        }

        public ShadowCollisionEditor(BSPRenderer bspRenderer)
        {
            InitializeComponent();
            this.bspRenderer = bspRenderer;
        }

        private BSPRenderer bspRenderer; // MUST HOLD SAME REFERENCE TO LEVEL EDITOR BSPRENDERER. I MIGHT REFACTOR THIS

        public void InitBSPList()
        {
            listBoxLevelModels.Items.Clear();
            foreach (RenderWareModelFile item in bspRenderer.ShadowColBSPList)
            {
                listBoxLevelModels.Items.Add(item.fileName);
            }
        }

        private void buttonImport_Click(object sender, EventArgs e)
        {
            VistaOpenFileDialog a = new VistaOpenFileDialog()
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

                    int parsedChunk = ParseChunkNumberFromFileName(Path.GetFileNameWithoutExtension(i));
                    if (parsedChunk == -1)
                        MessageBox.Show("Error parsing chunk number from name. Setting to -1. Report this to HeroesPowerPlant Issue Tracker");

                    file.ChunkNumber = parsedChunk;

                    if (Path.GetExtension(i).ToLower() == ".obj")
                    {
                        file.SetForRendering(Program.MainForm.renderer.Device, CreateShadowCollisionBSPFile(ReadOBJFile(i, true)), null);
                    }
                    else
                    {
                        file.SetForRendering(Program.MainForm.renderer.Device, ReadFileMethods.ReadRenderWareFile(i), File.ReadAllBytes(i));
                    }

                    bspRenderer.ShadowColBSPList.Add(file);
                    listBoxLevelModels.Items.Add(file.fileName);

                    UnsavedChanges = true;

                    ReadFileMethods.isCollision = false;
                }

                buttonExport.Enabled = true;
            }
        }

        /// <summary>
        /// Parses a chunk number from various filename formats. Returns -1 if none found.
        /// Strategy:
        ///  - normalize separators to underscores and split
        ///  - if a token contains "COLI", prefer numeric tokens immediately after or before it
        ///  - otherwise pick the last short numeric token (1-2 digits)
        ///  - fallback: pick the last numeric sequence, skipping the leading STG#### stage number when present
        /// </summary>
        private static int ParseChunkNumberFromFileName(string nameNoExt)
        {
            if (string.IsNullOrWhiteSpace(nameNoExt))
                return -1;

            // Normalize separators and collapse runs of non-alnum to single underscore
            string normalized = Regex.Replace(nameNoExt, "[^A-Za-z0-9]+", "_");
            var tokens = normalized.Split(new[] { '_' }, StringSplitOptions.RemoveEmptyEntries);

            // Prefer numbers near a 'COLI' token
            int coliIndex = Array.FindIndex(tokens, t => t.IndexOf("COLI", StringComparison.OrdinalIgnoreCase) >= 0);
            if (coliIndex >= 0)
            {
                // look after COLI first
                for (int j = coliIndex + 1; j < tokens.Length; j++)
                    if (int.TryParse(tokens[j], out var v))
                        return v;

                // then look before COLI
                for (int j = coliIndex - 1; j >= 0; j--)
                    if (int.TryParse(tokens[j], out var v))
                        return v;
            }

            // No COLI or not found near it. Prefer short numeric tokens (1-2 digits) from the end.
            for (int j = tokens.Length - 1; j >= 0; j--)
            {
                var t = tokens[j];
                if (int.TryParse(t, out var v) && t.Length <= 2)
                    return v;
            }

            // Fallback: find all digit runs in the original name and pick the last one.
            var matches = Regex.Matches(nameNoExt, "\\d+");
            if (matches.Count > 0)
            {
                int startIndex = 0;
                // If filename starts with STG, skip the leading stage number (e.g. STG0100)
                if (nameNoExt.StartsWith("STG", StringComparison.OrdinalIgnoreCase) && matches[0].Value.Length >= 3)
                    startIndex = 1;

                for (int m = matches.Count - 1; m >= startIndex; m--)
                {
                    if (int.TryParse(matches[m].Value, out var v))
                        return v;
                }
            }

            return -1;
        }

        private void buttonExport_Click(object sender, EventArgs e)
        {
            if (listBoxLevelModels.Items.Count == 0)
                return;

            if (listBoxLevelModels.SelectedIndices.Count == 1)
            {
                VistaSaveFileDialog saveDialog = new VistaSaveFileDialog()
                {
                    Filter = "OBJ Files|*.obj|BSP Files|*.bsp",
                    DefaultExt = ".obj",
                    FileName = Path.ChangeExtension(listBoxLevelModels.GetItemText(listBoxLevelModels.SelectedItem), ".obj")
                };
                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    if (Path.GetExtension(saveDialog.FileName).ToLower() == ".obj")
                        ConvertBSPtoOBJ(saveDialog.FileName, bspRenderer.ShadowColBSPList[listBoxLevelModels.SelectedIndex], false);
                    else if (Path.GetExtension(saveDialog.FileName).ToLower() == ".bsp")
                        File.WriteAllBytes(saveDialog.FileName, bspRenderer.ShadowColBSPList[listBoxLevelModels.SelectedIndex].GetAsByteArray());
                }
            }
            else
            {
                VistaFolderBrowserDialog folderDialog = new VistaFolderBrowserDialog();
                if (folderDialog.ShowDialog() == DialogResult.OK)
                {
                    if (listBoxLevelModels.SelectedIndices.Count > 1)
                        foreach (int i in listBoxLevelModels.SelectedIndices)
                            ConvertBSPtoOBJ(Path.Combine(folderDialog.SelectedPath, bspRenderer.ShadowColBSPList[i].fileName), bspRenderer.ShadowColBSPList[i], false);
                    else
                        foreach (RenderWareModelFile i in bspRenderer.ShadowColBSPList)
                            ConvertBSPtoOBJ(Path.Combine(folderDialog.SelectedPath, i.fileName), i, false);
                }
            }
        }

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < bspRenderer.ShadowColBSPList.Count; i++)
            {
                if (listBoxLevelModels.SelectedIndices.Contains(i))
                {
                    foreach (SharpMesh mesh in bspRenderer.ShadowColBSPList[i].meshList)
                        mesh.Dispose();

                    bspRenderer.ShadowColBSPList.RemoveAt(i);
                    listBoxLevelModels.Items.RemoveAt(i);
                    i -= 1;
                    UnsavedChanges = true;
                }
            }
            InitBSPList();
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            foreach (RenderWareModelFile r in bspRenderer.ShadowColBSPList)
                foreach (SharpMesh mesh in r.meshList)
                    mesh.Dispose();

            bspRenderer.ShadowColBSPList.Clear();
            listBoxLevelModels.Items.Clear();
            UnsavedChanges = true;
        }

        private void listBoxLevelModels_SelectedIndexChanged(object sender, EventArgs e)
        {
            uint vertices = 0;
            uint triangles = 0;

            foreach (int i in listBoxLevelModels.SelectedIndices)
            {
                vertices += bspRenderer.ShadowColBSPList[i].vertexAmount;
                triangles += bspRenderer.ShadowColBSPList[i].triangleAmount;
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
                bspRenderer.ShadowColBSPList[listBoxLevelModels.SelectedIndex].fileName = newName;
                listBoxLevelModels.Items[listBoxLevelModels.SelectedIndex] = newName;

                int parsed = ParseChunkNumberFromFileName(Path.GetFileNameWithoutExtension(newName));
                if (parsed == -1)
                    MessageBox.Show("Error parsing chunk number from name. Setting to -1. Report this to HeroesPowerPlant Issue Tracker");
                bspRenderer.ShadowColBSPList[listBoxLevelModels.SelectedIndex].ChunkNumber = parsed;
                UnsavedChanges = true;
            }
        }

        private void listBoxLevelModels_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
                buttonRemove_Click(sender, new EventArgs());
            if (e.KeyCode == Keys.F2)
                listBoxLevelModels_MouseDoubleClick(sender, null);
        }

        public bool UnsavedChanges = false;
    }
}
