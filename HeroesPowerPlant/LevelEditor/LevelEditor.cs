using Collada141;
using HeroesONE_R.Structures;
using HeroesONE_R.Structures.Subsctructures;
using HeroesPowerPlant.ShadowSplineEditor;
using HeroesPowerPlant.Shared.IO.Config;
using Ookii.Dialogs.WinForms;
using RenderWareFile;
using SharpDX;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using static HeroesPowerPlant.LevelEditor.BSP_IO_Assimp;
using static HeroesPowerPlant.LevelEditor.BSP_IO_Collada;
using static HeroesPowerPlant.LevelEditor.BSP_IO_Heroes;
using static HeroesPowerPlant.LevelEditor.BSP_IO_Shared;

namespace HeroesPowerPlant.LevelEditor
{
    public partial class LevelEditor : Form, IUnsavedChanges
    {
        // Internal value to determine if chunk isolation is active.
        private bool isolationActive = false;

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.WindowsShutDown)
                return;
            if (e.CloseReason == CloseReason.FormOwnerClosing)
                return;

            e.Cancel = true;
            Hide();
        }

        public LevelEditor()
        {
            InitializeComponent();
            visibilityFunctions = new VisibilityFunctions();
            bspRenderer = new BSPRenderer();

            shadowCollisionEditor = new ShadowCollisionEditor(bspRenderer);
            shadowSplineEditor = new ShadowSplineMenu();

            listViewLevelModels.Columns[0].Width = listViewLevelModels.Width - 4 - SystemInformation.VerticalScrollBarWidth;
        }

        private void LevelEditor_Load(object sender, EventArgs e)
        {
            if (HPPConfig.GetInstance().LegacyWindowPriorityBehavior)
                TopMost = true;
            else
                TopMost = false;
        }

        private string openONEfilePath;
        public VisibilityFunctions visibilityFunctions;
        public BSPRenderer bspRenderer;

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ShouldStopBecauseOfUnsavedChanges(sender, e))
                return;
            New();
        }

        public void New()
        {
            SetHeroesMode();
            ResetEveryting();
            _unsavedChangesLevel = false;
            textBox_import_extension.Text = ".BSP";
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ShouldStopBecauseOfUnsavedChanges(sender, e))
                return;

            VistaOpenFileDialog openFile = new VistaOpenFileDialog()
            {
                Filter = "ONE files|*.ONE"
            };
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                OpenONEHeroesFile(openFile.FileName, Program.MainForm.renderer);
            }
            textBox_import_extension.Text = ".BSP";
        }

        public void OpenONEHeroesFile(string fileName, SharpRenderer renderer)
        {
            SetHeroesMode();
            openONEfilePath = fileName;
            SetFilenamePrefix(openONEfilePath);

            byte[] fileBytes = File.ReadAllBytes(openONEfilePath);
            bspRenderer.SetHeroesBSPList(renderer.Device, Archive.FromONEFile(ref fileBytes));

            InitBSPList();

            string probBlk = Path.GetDirectoryName(openONEfilePath) + "\\" + bspRenderer.currentFileNamePrefix + "_blk.bin";
            if (File.Exists(probBlk))
                initVisibilityEditor(false, probBlk);

            _unsavedChangesLevel = false;

            string probTxd = Path.GetDirectoryName(openONEfilePath) + "\\textures\\" + bspRenderer.currentFileNamePrefix + ".txd";
            if (File.Exists(probTxd) && !TextureManager.OpenTXDfiles.Contains(probTxd))
                TextureManager.SetupTextureDisplay(File.ReadAllBytes(probTxd), renderer, bspRenderer);

            // TODO: Something is wrong with loading GCN _indirect .txd; Skipping for now
            /*
            string probIndirectTxd = Path.GetDirectoryName(openONEfilePath) + "\\textures\\" + bspRenderer.currentFileNamePrefix + "_indirect.txd";
            if (File.Exists(probIndirectTxd) && !TextureManager.OpenTXDfiles.Contains(probIndirectTxd))
                TextureManager.SetupTextureDisplay(File.ReadAllBytes(probIndirectTxd), renderer, bspRenderer);*/

            string probEffectTxd = Path.GetDirectoryName(openONEfilePath) + "\\textures\\" + bspRenderer.currentFileNamePrefix + "_effect.txd";
            if (File.Exists(probEffectTxd) && !TextureManager.OpenTXDfiles.Contains(probEffectTxd))
                TextureManager.SetupTextureDisplay(File.ReadAllBytes(probEffectTxd), renderer, bspRenderer);
        }

        public string GetOpenONEFilePath()
        {
            return openONEfilePath;
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openONEfilePath != null)
                SaveONEFile(openONEfilePath);
            else
                saveAsToolStripMenuItem_Click(sender, e);
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            VistaSaveFileDialog saveFileDialog = new VistaSaveFileDialog()
            {
                Filter = "ONE files|*.one",
                FileName = openONEfilePath,
                AddExtension = true,
                DefaultExt = ".one",
                InitialDirectory = Path.GetDirectoryName(openONEfilePath)
            };
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                SaveONEFile(saveFileDialog.FileName);
            }
        }

        private void SaveONEFile(string file)
        {
            SetHeroesMode();

            progressBar1.Minimum = 0;
            progressBar1.Value = 0;
            progressBar1.Maximum = 0;

            Archive one = new Archive(CommonRWVersions.Heroes);

            foreach (RenderWareModelFile i in bspRenderer.BSPList)
                progressBar1.Maximum += i.GetAsByteArray().Length;

            foreach (RenderWareModelFile i in bspRenderer.BSPList)
            {
                one.Files.Add(new ArchiveFile(i.fileName, i.GetAsByteArray()));

                progressBar1.Value += i.GetAsByteArray().Length;
            }

            File.WriteAllBytes(file, one.BuildHeroesONEArchive().ToArray());

            openONEfilePath = file;

            SetFilenamePrefix(file);
            InitBSPList();

            progressBar1.Value = 0;

            _unsavedChangesLevel = false;
        }

        private void SetFilenamePrefix(string fileName)
        {
            try
            {
                fileName = Path.GetFileName(fileName);
                if (fileName.StartsWith("stg"))
                    bspRenderer.currentFileNamePrefix = fileName.Substring(0, 5);
                else if (fileName.StartsWith("s"))
                    bspRenderer.currentFileNamePrefix = fileName.Substring(0, 3);
                else
                {
                    MessageBox.Show("Sorry, but I couldn't figure out the level from your file name.");
                    bspRenderer.currentFileNamePrefix = "";
                }
            }
            catch
            {
                MessageBox.Show("Sorry, but I couldn't figure out the level from your file name.");
                bspRenderer.currentFileNamePrefix = "";
            }
        }

        private void InitBSPList()
        {
            if (openONEfilePath == null)
                return;

            labelLoadedONE.Text = "Loaded " + openONEfilePath;
            listViewLevelModels.BeginUpdate();
            listViewLevelModels.Items.Clear();
            foreach (RenderWareModelFile item in bspRenderer.BSPList)
                AddToListView(item);
            listViewLevelModels.EndUpdate();
        }

        private void AddToListView(RenderWareModelFile file)
        {
            var item = new ListViewItem(file.fileName)
            {
                Checked = true,
                Selected = false
            };
            listViewLevelModels.Items.Add(item);
        }

        private void buttonImport_Click(object sender, EventArgs e)
        {
            VistaOpenFileDialog openFile = new VistaOpenFileDialog()
            {
                Filter = "All supported types|*.dae;*.obj;*.bsp|DAE Files|*.dae|OBJ Files|*.obj|BSP Files|*.bsp|All files|*.*",
                Multiselect = true
            };

            if (openFile.ShowDialog() == DialogResult.OK)
            {
                progressBar1.Minimum = 0;
                progressBar1.Value = 0;
                progressBar1.Step = 1;
                progressBar1.Maximum = openFile.FileNames.Count();

                foreach (string i in openFile.FileNames)
                {
                    _unsavedChangesLevel = true;

                    RenderWareModelFile file = new RenderWareModelFile(Path.GetFileNameWithoutExtension(i) + textBox_import_extension.Text);
                    file.SetChunkNumberAndName();

                    try
                    {
                        if (new string[] { ".bsp", ".rg1", ".rp2", ".rx1" }.Contains(Path.GetExtension(i).ToLower()))
                            file.SetForRendering(Program.MainForm.renderer.Device, ReadFileMethods.ReadRenderWareFile(i), File.ReadAllBytes(i));
                        else if (Path.GetExtension(i).ToLower() == ".obj" || Path.GetExtension(i).ToLower() == ".dae")
                            try
                            {
                                if (Path.GetExtension(i).ToLower() == ".obj")
                                    file.SetForRendering(Program.MainForm.renderer.Device, CreateBSPFile(ReadOBJFile(i, false), checkBoxTristrip.Checked, checkBoxFlipUVs.Checked), null);
                                else
                                    file.SetForRendering(Program.MainForm.renderer.Device, CreateBSPFromAssimp(i, checkBoxFlipUVs.Checked), null);
                                //file.SetForRendering(Program.MainForm.renderer.Device, CreateBSPFile(ConvertDataFromDAEObject(ReadDAEFile(i), false), checkBoxTristrip.Checked, checkBoxFlipUVs.Checked), null);
                            }
                            catch
                            {
                                file.SetForRendering(Program.MainForm.renderer.Device, CreateBSPFromAssimp(i, checkBoxFlipUVs.Checked), null);
                            }
                        else
                            file.SetForRendering(Program.MainForm.renderer.Device, CreateBSPFromAssimp(i, checkBoxFlipUVs.Checked), null);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error importing {Path.GetFileName(i)} : {ex.Message}");
                        progressBar1.PerformStep();
                        continue;
                    }

                    bspRenderer.BSPList.Add(file);
                    AddToListView(file);
                    progressBar1.PerformStep();
                }

                progressBar1.Value = 0;
            }
        }

        private void buttonExportClick(object sender, EventArgs e)
        {
            if (listViewLevelModels.Items.Count == 0)
                return;

            ChooseTarget.GetTarget(out bool success, out Assimp.ExportFormatDescription format, out string textureExtension);

            if (success)
            {
                List<int> indices = new List<int>();
                string fileName = null;

                if (listViewLevelModels.SelectedIndices.Count == 1)
                {
                    VistaSaveFileDialog a = new VistaSaveFileDialog()
                    {
                        Filter = format == null ? "RenderWare BSP|*.bsp" : format.Description + "|*." + format.FileExtension,
                        DefaultExt = ".bsp",
                        FileName = listViewLevelModels.SelectedItems[0].Text + (format == null ? "" : "." + format.FileExtension)
                    };

                    if (a.ShowDialog() == DialogResult.OK)
                    {
                        fileName = a.FileName;
                        indices.Add(listViewLevelModels.SelectedIndices[0]);
                    }
                }
                else
                {
                    VistaFolderBrowserDialog dialog = new VistaFolderBrowserDialog();
                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        fileName = dialog.SelectedPath;

                        if (listViewLevelModels.SelectedIndices.Count > 1)
                            foreach (int i in listViewLevelModels.SelectedIndices)
                                indices.Add(i);
                        else
                            for (int i = 0; i < bspRenderer.BSPList.Count; i++)
                                indices.Add(i);
                    }
                }

                if (fileName != null)
                    foreach (int i in indices)
                    {
                        string path = fileName;

                        if (listViewLevelModels.SelectedIndices.Count != 1)
                            path = Path.Combine(fileName, bspRenderer.BSPList[i].fileName);

                        if (format == null)
                            File.WriteAllBytes(path, bspRenderer.BSPList[i].GetAsByteArray());
                        else if (format.FileExtension.ToLower().Equals("obj"))
                            ConvertBSPtoOBJ(Path.ChangeExtension(path, "obj"), bspRenderer.BSPList[i], checkBoxFlipUVs.Checked);
                        else
                            ExportAssimp(Path.ChangeExtension(path, format.FileExtension), bspRenderer.BSPList[i], checkBoxFlipUVs.Checked, format, textureExtension);
                    }
            }
        }

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < bspRenderer.BSPList.Count; i++)
            {
                if (listViewLevelModels.SelectedIndices.Contains(i))
                {
                    _unsavedChangesLevel = true;
                    foreach (SharpMesh mesh in bspRenderer.BSPList[i].meshList)
                        mesh.Dispose();

                    bspRenderer.BSPList.RemoveAt(i);
                    listViewLevelModels.Items.RemoveAt(i);
                    i -= 1;
                }
            }
            InitBSPList();
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            foreach (RenderWareModelFile r in bspRenderer.BSPList)
                foreach (SharpMesh mesh in r.meshList)
                    mesh.Dispose();

            bspRenderer.BSPList.Clear();
            listViewLevelModels.Items.Clear();

            _unsavedChangesLevel = true;
        }

        private void listBoxLevelModelsDoubleClick(object sender, EventArgs e)
        {
            if (listViewLevelModels.SelectedIndices.Count == 1)
            {
                string newName = EditBSPName.GetName(listViewLevelModels.SelectedItems[0].Text);

                listViewLevelModels.Items[listViewLevelModels.SelectedIndices[0]].Text = newName;
                bspRenderer.BSPList[listViewLevelModels.SelectedIndices[0]].fileName = newName;
                bspRenderer.BSPList[listViewLevelModels.SelectedIndices[0]].SetChunkNumberAndName();

                _unsavedChangesLevel = true;
            }
        }

        private void listBoxLevelModels_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
                buttonRemove_Click(sender, new EventArgs());
            else if (e.KeyCode == Keys.F2)
                listBoxLevelModelsDoubleClick(sender, new EventArgs());
        }

        private void listBoxLevelModels_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (var bsp in bspRenderer.BSPList)
                bsp.isSelected = false;

            uint vertices = 0;
            uint triangles = 0;

            foreach (int i in listViewLevelModels.SelectedIndices)
            {
                vertices += bspRenderer.BSPList[i].vertexAmount;
                triangles += bspRenderer.BSPList[i].triangleAmount;
                bspRenderer.BSPList[i].isSelected = true;
            }

            labelVertexAmount.Text = "Vertices: " + vertices.ToString();
            labelTriangleAmount.Text = "Triangles: " + triangles.ToString();
        }

        public bool isShadowMode = false;

        public void SetHeroesMode()
        {
            saveToolStripMenuItem.Enabled = true;
            saveAsToolStripMenuItem.Enabled = true;

            bLKFileToolStripMenuItem.Enabled = true;
            newToolStripMenuItem1.Enabled = true;
            openToolStripMenuItem1.Enabled = true;
            saveToolStripMenuItem1.Enabled = true;
            saveAsToolStripMenuItem1.Enabled = true;

            ShadowLevelMenuItemSave.Enabled = false;
            ShadowLevelMenuItemSaveAs.Enabled = false;

            ShadowLevelMenuItemCollisionEditor.Enabled = false;
            ShadowLevelMenuItemSplineEditor.Enabled = false;
            ShadowLevelMenuItemImportBLK.Enabled = false;

            shadowCollisionEditor.Hide();
            shadowSplineEditor.Hide();
            buttonImport.Enabled = true;

            isShadowMode = false;
        }

        public void SetShadowMode()
        {
            saveToolStripMenuItem.Enabled = false;
            saveAsToolStripMenuItem.Enabled = false;

            bLKFileToolStripMenuItem.Enabled = false;
            newToolStripMenuItem1.Enabled = false;
            openToolStripMenuItem1.Enabled = false;
            saveToolStripMenuItem1.Enabled = false;
            saveAsToolStripMenuItem1.Enabled = false;

            ShadowLevelMenuItemSave.Enabled = true;
            ShadowLevelMenuItemSaveAs.Enabled = true;

            ShadowLevelMenuItemCollisionEditor.Enabled = true;
            ShadowLevelMenuItemSplineEditor.Enabled = true;
            ShadowLevelMenuItemImportBLK.Enabled = true;
            buttonImport.Enabled = true;

            isShadowMode = true;
        }

        private void ResetEveryting()
        {
            listViewLevelModels.Items.Clear();
            openONEfilePath = null;
            bspRenderer.currentFileNamePrefix = "default";
            bspRenderer.currentShadowFolderNamePrefix = "default";
            labelLoadedONE.Text = "No stage loaded";
            bspRenderer.Dispose();
            bspRenderer.BSPList.Clear();
            bspRenderer.ShadowColBSPList.Clear();
            shadowSplineEditor.Init();
            InitBSPList();

            _unsavedChangesLevel = false;
            _unsavedChangesVisibility = false;
        }

        // Shadow Level Editor

        public ShadowCollisionEditor shadowCollisionEditor;
        public ShadowSplineMenu shadowSplineEditor;

        private void ShadowLevelMenuItemNew_Click(object sender, EventArgs e)
        {
            if (ShouldStopBecauseOfUnsavedChanges(sender, e))
                return;

            SetShadowMode();
            ResetEveryting();
            textBox_import_extension.Text = ".RG1";
        }

        private async void ShadowLevelMenuItemOpen_Click(object sender, EventArgs e)
        {
            if (ShouldStopBecauseOfUnsavedChanges(sender, e))
                return;

            VistaFolderBrowserDialog openFolder = new VistaFolderBrowserDialog();

            if (openFolder.ShowDialog() == DialogResult.OK)
            {
                OpenONEShadowFolder(openFolder.SelectedPath, false);
                await Task.Run(() => Program.MainForm.renderer.dffRenderer.AddDFFFiles(Program.MainForm.dffsToLoad));
                await Task.Run(() => Program.MainForm.AutoLoadFNTAndAFS(Path.GetFileName(openFolder.SelectedPath)));
            }

            textBox_import_extension.Text = ".RG1";
        }

        public void OpenONEShadowFolder(string fileName, bool startup)
        {
            SetShadowMode();
            openONEfilePath = fileName;

            bspRenderer.LoadShadowLevelFolder(Program.MainForm.renderer, openONEfilePath, this);

            InitBSPList();
            shadowCollisionEditor.InitBSPList();

            if (!startup)
                Program.MainForm.OpenShadowLayoutEditors(fileName, bspRenderer.currentShadowFolderNamePrefix);
        }

        private void ShadowLevelMenuItemSave_Click(object sender, EventArgs e)
        {
            if (openONEfilePath != null)
                SaveShadowLevel(openONEfilePath);
            else
                ShadowLevelMenuItemSaveAs_Click(sender, e);
        }

        private void ShadowLevelMenuItemSaveAs_Click(object sender, EventArgs e)
        {
            VistaFolderBrowserDialog saveFolder = new VistaFolderBrowserDialog();

            if (saveFolder.ShowDialog() == DialogResult.OK)
                SaveShadowLevel(saveFolder.SelectedPath);
        }

        private void SaveShadowLevel(string levelPath)
        {
            SetShadowMode();
            openONEfilePath = levelPath;
            bspRenderer.currentShadowFolderNamePrefix = Path.GetFileNameWithoutExtension(levelPath);

            string datONEpath = null;

            foreach (string fileName in Directory.GetFiles(levelPath))
            {
                if (Path.GetExtension(fileName).ToLower() == ".one"
                    & Path.GetFileName(fileName).StartsWith(bspRenderer.currentShadowFolderNamePrefix)
                    & !fileName.Contains("dat")
                    & !fileName.Contains("fx")
                    & !fileName.Contains("gdt")
                    & !fileName.Contains("tex"))
                    File.Delete(fileName);
                else if (Path.GetExtension(fileName).ToLower() == ".one" & fileName.Contains("dat"))
                    datONEpath = fileName;
            }

            if (datONEpath == null)
                datONEpath = Path.Combine(openONEfilePath, bspRenderer.currentShadowFolderNamePrefix + "_dat.one");

            List<RenderWareModelFile> fileList = new List<RenderWareModelFile>();
            fileList.AddRange(bspRenderer.BSPList);
            fileList.AddRange(bspRenderer.ShadowColBSPList);

            progressBar1.Minimum = 0;
            progressBar1.Value = 0;
            progressBar1.Maximum = 0;
            foreach (RenderWareModelFile i in fileList)
                progressBar1.Maximum += i.GetAsByteArray().Length;

            Dictionary<int, Archive> oneDict = new Dictionary<int, Archive>(fileList.Count);

            bool error = false;
            string filesWithError = "";

            foreach (RenderWareModelFile i in fileList)
            {
                if (i.ChunkNumber == -1)
                {
                    error = true;
                    filesWithError += i.fileName + ", ";
                    continue;
                }

                if (!oneDict.ContainsKey(i.ChunkNumber))
                    oneDict.Add(i.ChunkNumber, new Archive(CommonRWVersions.Shadow060));

                ArchiveFile item = new ArchiveFile(i.fileName, i.GetAsByteArray());
                oneDict[i.ChunkNumber].Files.Add(item);
                progressBar1.Value += i.GetAsByteArray().Length;
            }

            if (error)
                MessageBox.Show("Some of the files were not included in the archives because I could not figure out their chunk number. Please fix that and save again, otherwise those files will be lost: " + filesWithError);

            foreach (int i in oneDict.Keys)
            {
                string fileName = Path.Combine(openONEfilePath, bspRenderer.currentShadowFolderNamePrefix + "_" + i.ToString("D2") + ".one");
                File.WriteAllBytes(fileName, oneDict[i].BuildShadowONEArchive(true).ToArray());
            }

            _unsavedChangesLevel = false;
            shadowCollisionEditor.UnsavedChanges = false;
            shadowSplineEditor.UnsavedChanges = false;
            _unsavedChangesVisibility = false;

            SaveShadowDATONE(datONEpath);

            InitBSPList();
            shadowCollisionEditor.InitBSPList();

            progressBar1.Value = 0;
        }

        private void SaveShadowDATONE(string datOneFileName, bool splinesOnly = false)
        {
            byte[] bdtBytes = VisibilityFunctions.ShadowVisibilityFileToArray(visibilityFunctions.ChunkList, bspRenderer.currentShadowFolderNamePrefix);
            byte[] splineBytes = shadowSplineEditor.ShadowSplinesToByteArray(bspRenderer.currentShadowFolderNamePrefix).ToArray();

            Archive shadowDATONE;

            if (File.Exists(datOneFileName))
            {
                byte[] fileContents = File.ReadAllBytes(datOneFileName);
                shadowDATONE = Archive.FromONEFile(ref fileContents);
            }
            else
                shadowDATONE = new Archive(CommonRWVersions.Shadow050);

            bool bdtFound = false;
            bool splFound = false;

            foreach (var file in shadowDATONE.Files)
            {
                if (Path.GetExtension(file.Name).ToLower() == ".bdt" && !splinesOnly)
                {
                    file.CompressedData = Prs.Compress(ref bdtBytes);
                    bdtFound = true;
                }
                else if (file.Name == "PATH.PTP")
                {
                    file.CompressedData = Prs.Compress(ref splineBytes);
                    splFound = true;
                }
            }

            if (!bdtFound && !splinesOnly)
            {
                ArchiveFile file = new ArchiveFile((bspRenderer.currentShadowFolderNamePrefix + ".bdt").ToUpper(), bdtBytes);
                shadowDATONE.Files.Add(file);
            }
            if (!splFound)
            {
                ArchiveFile file = new ArchiveFile("PATH.PTP", splineBytes);
                shadowDATONE.Files.Add(file);
            }

            File.WriteAllBytes(datOneFileName, shadowDATONE.BuildShadowONEArchive(true).ToArray());
            _unsavedChangesVisibility = false;
            shadowSplineEditor.UnsavedChanges = false;
        }

        private void ShadowLevelMenuItemCollisionEditor_Click(object sender, EventArgs e)
        {
            shadowCollisionEditor.Show();
            shadowCollisionEditor.Focus();
        }

        private void ShadowLevelMenuItemSplineEditor_Click(object sender, EventArgs e)
        {
            shadowSplineEditor.Show();
            shadowSplineEditor.Focus();
        }

        public void GetClickedModelPosition(bool isShadowCollision, Ray ray, out bool found, out float smallestDistance)
        {
            if (isShadowMode && isShadowCollision)
            {
                bspRenderer.GetClickedModelPosition(true, ray, out found, out smallestDistance);
            }
            else if (!isShadowCollision)
            {
                bspRenderer.GetClickedModelPosition(false, ray, out found, out smallestDistance);
            }
            else
            {
                found = false;
                smallestDistance = 0;
            }
        }

        public void RenderLevelModel(SharpRenderer renderer)
        {
            bspRenderer.RenderLevelModel(renderer, visibilityFunctions.ChunkList);
        }

        public void RenderShadowCollisionModel(SharpRenderer renderer)
        {
            bspRenderer.RenderShadowCollisionModel(renderer, visibilityFunctions.ChunkList);
        }

        // BLK Editor

        private void newToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (ShouldStopBecauseOfUnsavedChanges(sender, e))
                return;

            NewVisibility();
        }

        public void NewVisibility()
        {
            visibilityFunctions.OpenVisibilityFile = null;
            visibilityFunctions.ChunkList.Clear();
            numericCurrentChunk.Maximum = visibilityFunctions.ChunkList.Count();
            labelChunkAmount.Text = "Amount: " + visibilityFunctions.ChunkList.Count();
            labelLoadedBLK.Text = "No BLK loaded";
            _unsavedChangesVisibility = false;
        }

        private void openToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (ShouldStopBecauseOfUnsavedChanges(sender, e))
                return;

            VistaOpenFileDialog openFile = new VistaOpenFileDialog()
            {
                Filter = "BIN files|*.bin"
            };
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                initVisibilityEditor(false, openFile.FileName);
            }
        }

        public void initVisibilityEditor(bool isShadow, string fileName)
        {
            if (File.Exists(fileName))
            {
                if (isShadow)
                {
                    visibilityFunctions.OpenVisibilityFile = null;
                    byte[] bytes = File.ReadAllBytes(fileName);
                    visibilityFunctions.ChunkList = VisibilityFunctions.LoadShadowVisibilityFile(Archive.FromONEFile(ref bytes));
                    labelLoadedBLK.Text = "";
                }
                else
                {
                    visibilityFunctions.OpenVisibilityFile = fileName;
                    visibilityFunctions.ChunkList = VisibilityFunctions.LoadHeroesVisibilityFile(visibilityFunctions.OpenVisibilityFile);
                    labelLoadedBLK.Text = "Loaded " + fileName;
                }

                numericCurrentChunk.Minimum = 1;
                numericCurrentChunk.Maximum = visibilityFunctions.ChunkList.Count();
                numericCurrentChunk.Value = visibilityFunctions.ChunkList.Count();
                if (numericCurrentChunk.Maximum != 0)
                    numericCurrentChunk.Value = 1;

                labelChunkAmount.Text = "Amount: " + visibilityFunctions.ChunkList.Count();
                _unsavedChangesVisibility = false;
            }
        }

        private void saveToolStripMenuItemVisibility_Click(object sender, EventArgs e)
        {
            if (visibilityFunctions.OpenVisibilityFile != null)
            {
                VisibilityFunctions.SaveHeroesVisibilityFile(visibilityFunctions.ChunkList, visibilityFunctions.OpenVisibilityFile);
                _unsavedChangesVisibility = false;
            }
            else
                saveAsToolStripMenuItem1_Click(sender, e);
        }

        private void saveAsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            VistaSaveFileDialog saveFileDialog = new VistaSaveFileDialog()
            {
                Filter = "BIN files|*.bin",
                FileName = Path.GetFileName(visibilityFunctions.OpenVisibilityFile),
                AddExtension = true,
                DefaultExt = "bin",
                InitialDirectory = Path.GetDirectoryName(visibilityFunctions.OpenVisibilityFile)
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                visibilityFunctions.OpenVisibilityFile = saveFileDialog.FileName;
                VisibilityFunctions.SaveHeroesVisibilityFile(visibilityFunctions.ChunkList, visibilityFunctions.OpenVisibilityFile);
                labelLoadedBLK.Text = "Loaded " + visibilityFunctions.OpenVisibilityFile;
                _unsavedChangesVisibility = false;
            }
        }

        private void importBLKToolStripMenuItem_Click(object sender, EventArgs e)
        {
            VistaOpenFileDialog openFile = new VistaOpenFileDialog()
            {
                Filter = "All supported files|*.bin;*.bdt|BIN files|*.bin|BDT files|*.bdt"
            };
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                if (Path.GetExtension(openFile.FileName).ToLower() == ".bdt")
                {
                    visibilityFunctions.ChunkList.AddRange(VisibilityFunctions.LoadShadowVisibilityFile(new FileStream(openFile.FileName, FileMode.Open)));
                }
                else if (Path.GetExtension(openFile.FileName).ToLower() == ".bin")
                {
                    visibilityFunctions.ChunkList.AddRange(VisibilityFunctions.LoadHeroesVisibilityFile(openFile.FileName));
                }

                numericCurrentChunk.Minimum = 1;
                numericCurrentChunk.Maximum = visibilityFunctions.ChunkList.Count();
                numericCurrentChunk.Value = visibilityFunctions.ChunkList.Count();
                if (numericCurrentChunk.Maximum != 0)
                    numericCurrentChunk.Value = 1;

                labelChunkAmount.Text = "Amount: " + visibilityFunctions.ChunkList.Count();
                _unsavedChangesVisibility = true;
            }
        }

        bool ProgramIsChangingStuff = false;

        private void numericCurrentChunk_ValueChanged(object sender, EventArgs e)
        {
            ProgramIsChangingStuff = true;

            foreach (Chunk c in visibilityFunctions.ChunkList)
                c.isSelected = false;

            int i = (int)numericCurrentChunk.Value - 1;

            if (visibilityFunctions.ChunkList.Count > 0)
            {
                NumChunkNum.Value = visibilityFunctions.ChunkList[i].number;
                NumMinX.Value = (decimal)visibilityFunctions.ChunkList[i].Min.X;
                NumMinY.Value = (decimal)visibilityFunctions.ChunkList[i].Min.Y;
                NumMinZ.Value = (decimal)visibilityFunctions.ChunkList[i].Min.Z;
                NumMaxX.Value = (decimal)visibilityFunctions.ChunkList[i].Max.X;
                NumMaxY.Value = (decimal)visibilityFunctions.ChunkList[i].Max.Y;
                NumMaxZ.Value = (decimal)visibilityFunctions.ChunkList[i].Max.Z;

                visibilityFunctions.ChunkList[i].isSelected = true;
                visibilityFunctions.ChunkList[i].CalculateModel();
            }

            ProgramIsChangingStuff = false;

            // Change the isolation button text.
            buttonIsolateChunk.Text = "Isolate Chunk";

            // Disable the isolation flag.
            isolationActive = false;
        }

        private void buttonAddChunkClick(object sender, EventArgs e)
        {
            var chunk = new Chunk(0, Vector3.Zero, Vector3.Zero);
            chunk.CalculateModel();
            visibilityFunctions.ChunkList.Add(chunk);
            numericCurrentChunk.Minimum = 1;
            numericCurrentChunk.Maximum = visibilityFunctions.ChunkList.Count();
            numericCurrentChunk.Value = visibilityFunctions.ChunkList.Count();
            labelChunkAmount.Text = "Amount: " + visibilityFunctions.ChunkList.Count();
            _unsavedChangesVisibility = true;
        }

        private void buttonRemoveChunk_Click(object sender, EventArgs e)
        {
            if (visibilityFunctions.ChunkList.Count > 0)
                visibilityFunctions.ChunkList.RemoveAt((int)numericCurrentChunk.Value - 1);

            _unsavedChangesVisibility = true;
            numericCurrentChunk.Maximum = visibilityFunctions.ChunkList.Count();
            labelChunkAmount.Text = "Amount: " + visibilityFunctions.ChunkList.Count();
            numericCurrentChunk_ValueChanged(new object(), new EventArgs());
        }

        private void buttonAutoChunk_Click(object sender, EventArgs e)
        {
            if (visibilityFunctions.ChunkList.Count > 0)
            {
                List<RenderWareModelFile> bspAndCol = new List<RenderWareModelFile>();
                bspAndCol.AddRange(bspRenderer.BSPList);
                bspAndCol.AddRange(bspRenderer.ShadowColBSPList);

                VisibilityFunctions.AutoChunk((int)NumChunkNum.Value, bspAndCol, out bool success, out Vector3 Min, out Vector3 Max);

                if (success)
                {
                    visibilityFunctions.ChunkList[(int)numericCurrentChunk.Value - 1].Min.X = (int)(Min.X - (int)numericUpDownAdd.Value);
                    visibilityFunctions.ChunkList[(int)numericCurrentChunk.Value - 1].Min.Y = (int)(Min.Y - (int)numericUpDownAdd.Value);
                    visibilityFunctions.ChunkList[(int)numericCurrentChunk.Value - 1].Min.Z = (int)(Min.Z - (int)numericUpDownAdd.Value);
                    visibilityFunctions.ChunkList[(int)numericCurrentChunk.Value - 1].Max.X = (int)(Max.X + (int)numericUpDownAdd.Value);
                    visibilityFunctions.ChunkList[(int)numericCurrentChunk.Value - 1].Max.Y = (int)(Max.Y + (int)numericUpDownAdd.Value);
                    visibilityFunctions.ChunkList[(int)numericCurrentChunk.Value - 1].Max.Z = (int)(Max.Z + (int)numericUpDownAdd.Value);
                    _unsavedChangesVisibility = true;
                    numericCurrentChunk_ValueChanged(new object(), new EventArgs());
                }
                else
                {
                    MessageBox.Show("I couldn't find any BSP with a matching chunk number.");
                }
            }
        }

        private void NumChunkNum_ValueChanged(object sender, EventArgs e)
        {
            if (!ProgramIsChangingStuff)
                if (visibilityFunctions.ChunkList.Count > 0)
                {
                    visibilityFunctions.ChunkList[(int)numericCurrentChunk.Value - 1].number = (int)NumChunkNum.Value;
                    _unsavedChangesVisibility = true;
                }
        }

        private void NumMaxMin_ValueChanged(object sender, EventArgs e)
        {
            if (!ProgramIsChangingStuff)
                if (visibilityFunctions.ChunkList.Count > 0)
                {
                    visibilityFunctions.ChunkList[(int)numericCurrentChunk.Value - 1].Min.X = (int)NumMinX.Value;
                    visibilityFunctions.ChunkList[(int)numericCurrentChunk.Value - 1].Min.Y = (int)NumMinY.Value;
                    visibilityFunctions.ChunkList[(int)numericCurrentChunk.Value - 1].Min.Z = (int)NumMinZ.Value;
                    visibilityFunctions.ChunkList[(int)numericCurrentChunk.Value - 1].Max.X = (int)NumMaxX.Value;
                    visibilityFunctions.ChunkList[(int)numericCurrentChunk.Value - 1].Max.Y = (int)NumMaxY.Value;
                    visibilityFunctions.ChunkList[(int)numericCurrentChunk.Value - 1].Max.Z = (int)NumMaxZ.Value;
                    visibilityFunctions.ChunkList[(int)numericCurrentChunk.Value - 1].CalculateModel();
                    _unsavedChangesVisibility = true;
                }
        }

        private void ButtonAutoBuild_Click(object sender, EventArgs e)
        {
            DialogResult d = MessageBox.Show("Warning: this will overwrite the current chunk data with an automatically generated one. Continue?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (d != DialogResult.Yes)
                return;

            visibilityFunctions.ChunkList.Clear();

            List<RenderWareModelFile> bspAndCol = new List<RenderWareModelFile>();
            bspAndCol.AddRange(bspRenderer.BSPList);
            bspAndCol.AddRange(bspRenderer.ShadowColBSPList);

            HashSet<int> numbers = new HashSet<int>();
            foreach (RenderWareModelFile rwmf in bspAndCol)
                if (rwmf.ChunkNumber != -1)
                    numbers.Add(rwmf.ChunkNumber);

            var add = new Vector3((int)numericUpDownAdd.Value);

            foreach (int i in numbers)
            {
                VisibilityFunctions.AutoChunk(i, bspAndCol, out bool success, out Vector3 Min, out Vector3 Max);

                if (success)
                    visibilityFunctions.ChunkList.Add(new Chunk(i, Min - add, Max + add));
            }

            visibilityFunctions.ChunkList = visibilityFunctions.ChunkList.OrderBy(c => c.number).ToList();

            numericCurrentChunk.Minimum = 1;
            numericCurrentChunk.Maximum = visibilityFunctions.ChunkList.Count();
            numericCurrentChunk.Value = visibilityFunctions.ChunkList.Count();
            labelChunkAmount.Text = "Amount: " + visibilityFunctions.ChunkList.Count();
            _unsavedChangesVisibility = true;
        }

        private void DisableFilesizeWarningToolStripMenuItem_Click(object sender, EventArgs e)
        {
            disableFilesizeWarningToolStripMenuItem.Checked = !disableFilesizeWarningToolStripMenuItem.Checked;
            RenderWareModelFile.fileSizeCheck = !disableFilesizeWarningToolStripMenuItem.Checked;
        }

        private void ButtonReassignMATFlag_Click(object sender, EventArgs e)
        {
            ReassignMATFlags.OpenReassignMATFlags(this);
        }

        public void MATFlag_Reassignment(string target, string replacement)
        {
            for (int i = 0; i < listViewLevelModels.Items.Count; i++)
            {
                if (listViewLevelModels.Items[i].ToString().Contains(target))
                {
                    var newName = listViewLevelModels.Items[i].Text.Replace(target, replacement);
                    listViewLevelModels.Items[i].Text = newName;
                    bspRenderer.BSPList[i].fileName = newName;
                    bspRenderer.BSPList[i].SetChunkNumberAndName();
                }
            }
        }

        private void buttonIsolateChunk_Click(object sender, EventArgs e)
        {
            // Recheck all the level models in the list view.
            foreach (ListViewItem item in listViewLevelModels.Items)
                item.Checked = true;

            // Check if the isolation isn't active.
            if (!isolationActive)
            {
                // Set up a list of level model indices to be hidden.
                List<int> indicesToHide = new();

                // Loop through each entry in the BSP list.
                for (int bspIndex = 0; bspIndex < bspRenderer.BSPList.Count; bspIndex++)
                {
                    // Check if this BSP's chunk index doesn't match the selected visiblity block's chunk value.
                    if (bspRenderer.BSPList[bspIndex].ChunkNumber != NumChunkNum.Value)
                    {
                        // Hide this chunk.
                        bspRenderer.BSPList[bspIndex].isVisible = false;

                        // Mark the index of this BSP to be hidden.
                        indicesToHide.Add(bspIndex);
                    }

                    // If not, then make this chunk visible.
                    else
                        bspRenderer.BSPList[bspIndex].isVisible = true;
                }

                // Loop through and uncheck each level model in our list of indices.
                foreach (int index in indicesToHide)
                    listViewLevelModels.Items[index].Checked = false;

                // Change the button text.
                buttonIsolateChunk.Text = "Disable Isolation";

                // Set the isolation flag.
                isolationActive = true;
            }
            else
            {
                // Loop through and make every chunk visible.
                for (int bspIndex = 0; bspIndex < bspRenderer.BSPList.Count; bspIndex++)
                    bspRenderer.BSPList[bspIndex].isVisible = true;

                // Change the button text.
                buttonIsolateChunk.Text = "Isolate Chunk";

                // Disable the isolation flag.
                isolationActive = false;
            }
        }

        private bool _unsavedChangesLevel = false;
        private bool _unsavedChangesVisibility = false;

        public bool UnsavedChanges =>
            _unsavedChangesLevel ||
            _unsavedChangesVisibility ||
            shadowCollisionEditor.UnsavedChanges ||
            shadowSplineEditor.UnsavedChanges;

        private bool ShouldStopBecauseOfUnsavedChanges(object sender, EventArgs e)
        {
            if (isShadowMode)
            {
                if (UnsavedChanges)
                {
                    var result = Extensions.UnsavedChangesMessageBox("Level Editor");
                    if (result == DialogResult.Yes)
                    {
                        ShadowLevelMenuItemSave_Click(sender, e);
                        if (UnsavedChanges)
                            return true;
                    }
                    else if (result == DialogResult.Cancel)
                        return true;
                }
            }
            else
            {
                if (_unsavedChangesLevel)
                {
                    var result = Extensions.UnsavedChangesMessageBox("Level Editor");
                    if (result == DialogResult.Yes)
                    {
                        saveToolStripMenuItem_Click(sender, e);
                        if (_unsavedChangesLevel)
                            return true;
                    }
                    else if (result == DialogResult.Cancel)
                        return true;
                }
                if (_unsavedChangesVisibility)
                {
                    var result = Extensions.UnsavedChangesMessageBox("Visibility Editor");
                    if (result == DialogResult.Yes)
                    {
                        saveToolStripMenuItemVisibility_Click(sender, e);
                        if (_unsavedChangesVisibility)
                            return true;
                    }
                    else if (result == DialogResult.Cancel)
                        return true;
                }
            }

            return false;
        }

        public void Save()
        {
            if (isShadowMode)
            {
                ShadowLevelMenuItemSave_Click(null, null);
            }
            else
            {
                if (_unsavedChangesLevel)
                    saveToolStripMenuItem_Click(null, null);
                if (_unsavedChangesVisibility)
                    saveToolStripMenuItemVisibility_Click(null, null);
            }
        }

        private void listViewLevelModels_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if (e.Item.Index < bspRenderer.BSPList.Count)
                bspRenderer.BSPList[e.Item.Index].isVisible = e.Item.Checked;
        }

        private void ShadowLevelMenuItemSaveSplineDataOnly_Click(object sender, EventArgs e)
        {
            SaveShadowDATONE(Path.Combine(openONEfilePath, bspRenderer.currentShadowFolderNamePrefix + "_dat.one"), true);
        }
    }
}