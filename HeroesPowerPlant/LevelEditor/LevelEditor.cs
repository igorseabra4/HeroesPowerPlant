using System;
using System.IO;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using SharpDX;
using HeroesONE_R.Structures;
using HeroesONE_R.Structures.Subsctructures;
using RenderWareFile;
using static HeroesPowerPlant.LevelEditor.VisibilityFunctions;
using static HeroesPowerPlant.LevelEditor.BSP_IO_Shared;
using static HeroesPowerPlant.LevelEditor.BSP_IO_Heroes;
using static HeroesPowerPlant.LevelEditor.BSP_IO_Collada;
using static HeroesPowerPlant.BSPRenderer;
using Microsoft.WindowsAPICodePack.Dialogs;
using HeroesPowerPlant.ShadowSplineEditor;

namespace HeroesPowerPlant.LevelEditor
{
    public partial class LevelEditor : Form
    {
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.WindowsShutDown) return;
            if (e.CloseReason == CloseReason.FormOwnerClosing) return;

            e.Cancel = true;
            Hide();
        }

        public LevelEditor()
        {
            InitializeComponent();
        }

        private void LevelEditor_Load(object sender, EventArgs e)
        {
            TopMost = true;
        }

        private string openONEfilePath;
        
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            New();
        }

        public void New()
        {
            SetHeroesMode();
            ResetEveryting();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog()
            {
                Filter = "ONE files|*.ONE"
            };
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                OpenONEHeroesFile(openFile.FileName);
            }
        }

        public void OpenONEHeroesFile(string fileName)
        {
            SetHeroesMode();
            openONEfilePath = fileName;
            SetFilenamePrefix(openONEfilePath);

            byte[] fileBytes = File.ReadAllBytes(openONEfilePath);
            SetHeroesBSPList(Program.MainForm.renderer.Device, Archive.FromONEFile(ref fileBytes));

            InitBSPList();

            string SupposedBLK = Path.GetDirectoryName(openONEfilePath) + "\\" + currentFileNamePrefix + "_blk.bin";
            if (File.Exists(SupposedBLK))
            {
                initVisibilityEditor(false, SupposedBLK);
            }

            foreach (string s in TextureManager.OpenTXDfiles)
                if (Path.GetFileNameWithoutExtension(s).ToLower() == currentFileNamePrefix.ToLower())
                    return;

            string SupposedTXD = Path.GetDirectoryName(openONEfilePath) + "\\textures\\" + currentFileNamePrefix + ".txd";
            if (File.Exists(SupposedTXD))
            {
                TextureManager.LoadTexturesFromTXD(SupposedTXD);
            }
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
            SaveFileDialog saveFileDialog = new SaveFileDialog()
            {
                Filter = "ONE files|*.one",
                FileName = openONEfilePath,
                AddExtension = true,
                DefaultExt = "ONE",
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

            foreach (RenderWareModelFile i in BSPList)
                Program.LevelEditor.progressBar1.Maximum += i.GetAsByteArray().Length;

            foreach (RenderWareModelFile i in BSPList)
            {
                one.Files.Add(new ArchiveFile(i.fileName, i.GetAsByteArray()));

                progressBar1.Value += i.GetAsByteArray().Length;
            }

            File.WriteAllBytes(file, one.BuildHeroesONEArchive().ToArray());

            openONEfilePath = file;

            SetFilenamePrefix(file);
            InitBSPList();

            progressBar1.Value = 0;
        }

        private void SetFilenamePrefix(string fileName)
        {
            try
            {
                fileName = Path.GetFileName(fileName);
                if (fileName.StartsWith("stg"))
                    currentFileNamePrefix = fileName.Substring(0, 5);
                else if (fileName.StartsWith("s"))
                    currentFileNamePrefix = fileName.Substring(0, 3);
                else
                {
                    MessageBox.Show("Sorry, but I couldn't figure out the level from your file name.");
                    currentFileNamePrefix = "";
                }
            }
            catch
            {
                MessageBox.Show("Sorry, but I couldn't figure out the level from your file name.");
                currentFileNamePrefix = "";
            }
        }

        private void InitBSPList()
        { 
            if (openONEfilePath != null)
            {
                labelLoadedONE.Text = "Loaded " + openONEfilePath;
                listBoxLevelModels.Items.Clear();
                foreach (RenderWareModelFile item in BSPList)
                {
                    listBoxLevelModels.Items.Add(item.fileName);
                }
            }
        }

        private void buttonImport_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog a = new OpenFileDialog()
            {
                Filter = "All supported types|*.dae;*.obj;*.bsp|DAE Files|*.dae|OBJ Files|*.obj|BSP Files|*.bsp|All files|*.*",
                Multiselect = true
            })
                if (a.ShowDialog() == DialogResult.OK)
                {
                    progressBar1.Minimum = 0;
                    progressBar1.Value = 0;
                    progressBar1.Step = 1;
                    progressBar1.Maximum = a.FileNames.Count();

                    foreach (string i in a.FileNames)
                    {
                        RenderWareModelFile file = new RenderWareModelFile(Path.GetFileNameWithoutExtension(i) + ".BSP");
                        file.SetChunkNumberAndName();

                        if (Path.GetExtension(i).ToLower() == ".obj")
                        {
                            file.SetForRendering(Program.MainForm.renderer.Device, CreateBSPFile(i, ReadOBJFile(i, false), checkBoxTristrip.Checked), null);
                        }
                        else if (Path.GetExtension(i).ToLower() == ".dae")
                        {
                            file.SetForRendering(Program.MainForm.renderer.Device, CreateBSPFile(i, ConvertDataFromDAEObject(ReadDAEFile(i), false), checkBoxTristrip.Checked), null);
                        }
                        else if (new string[] { ".bsp", ".rg1", ".rp2", ".rx1" }.Contains(Path.GetExtension(i).ToLower()))
                        {
                            file.SetForRendering(Program.MainForm.renderer.Device, ReadFileMethods.ReadRenderWareFile(i), File.ReadAllBytes(i));
                        }

                        BSPList.Add(file);
                        listBoxLevelModels.Items.Add(file.fileName);
                        progressBar1.PerformStep();
                    }

                    progressBar1.Value = 0;
                }
        }

        private void buttonExportClick(object sender, EventArgs e)
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
                        ConvertBSPtoOBJ(a.FileName, BSPList[listBoxLevelModels.SelectedIndex]);
                    else if (Path.GetExtension(a.FileName).ToLower() == ".bsp")
                        File.WriteAllBytes(a.FileName, BSPList[listBoxLevelModels.SelectedIndex].GetAsByteArray());
                }
            }
            else
            {
                CommonOpenFileDialog commonOpenFileDialog = new CommonOpenFileDialog()
                {
                    IsFolderPicker = true
                };
                if (openONEfilePath != null)
                    commonOpenFileDialog.DefaultFileName = Path.GetDirectoryName(openONEfilePath);
                if (commonOpenFileDialog.ShowDialog() == CommonFileDialogResult.Ok)
                {
                    if (listBoxLevelModels.SelectedIndices.Count > 1)
                        foreach (int i in listBoxLevelModels.SelectedIndices)
                            ConvertBSPtoOBJ(Path.Combine(commonOpenFileDialog.FileName, BSPList[i].fileName), BSPList[i]);
                    else
                        for (int i = 0; i < BSPList.Count; i++)
                            ConvertBSPtoOBJ(Path.Combine(commonOpenFileDialog.FileName, BSPList[i].fileName), BSPList[i]);
                }
            }
        }

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < BSPList.Count; i++)
            {
                if (listBoxLevelModels.SelectedIndices.Contains(i))
                {
                    foreach (SharpMesh mesh in BSPList[i].meshList)
                        mesh.Dispose();

                    BSPList.RemoveAt(i);
                    listBoxLevelModels.Items.RemoveAt(i);
                    i -= 1;
                }
            }
            InitBSPList();
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            foreach (RenderWareModelFile r in BSPList)
                foreach (SharpMesh mesh in r.meshList)
                    mesh.Dispose();

            BSPList.Clear();
            listBoxLevelModels.Items.Clear();
        }
        
        private void listBoxLevelModelsDoubleClick(object sender, EventArgs e)
        {
            if (listBoxLevelModels.SelectedIndices.Count == 1)
            {
                string newName = EditBSPName.GetName(listBoxLevelModels.Items[listBoxLevelModels.SelectedIndex].ToString());

                listBoxLevelModels.Items[listBoxLevelModels.SelectedIndex] = newName;
                BSPList[listBoxLevelModels.SelectedIndex].fileName = newName;
                BSPList[listBoxLevelModels.SelectedIndex].SetChunkNumberAndName();
            }
        }

        private void listBoxLevelModels_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
                buttonRemove_Click(sender, new EventArgs());
        }
        
        private void listBoxLevelModels_SelectedIndexChanged(object sender, EventArgs e)
        {
            uint vertices = 0;
            uint triangles = 0;

            foreach (int i in listBoxLevelModels.SelectedIndices)
            {
                vertices += BSPList[i].vertexAmount;
                triangles += BSPList[i].triangleAmount;
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
            
            saveToolStripMenuItem2.Enabled = false;
            saveAsToolStripMenuItem2.Enabled = false;

            collisionEditorToolStripMenuItem.Enabled = false;
            splineEditorToolStripMenuItem.Enabled = false;
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

            saveToolStripMenuItem2.Enabled = true;
            saveAsToolStripMenuItem2.Enabled = true;

            collisionEditorToolStripMenuItem.Enabled = true;
            splineEditorToolStripMenuItem.Enabled = true;
            buttonImport.Enabled = true;

            isShadowMode = true;
        }

        private void ResetEveryting()
        {
            listBoxLevelModels.Items.Clear();
            openONEfilePath = null;
            currentFileNamePrefix = "default";
            currentShadowFolderNamePrefix = "default";
            labelLoadedONE.Text = "No stage loaded";
            BSPRenderer.Dispose();
            BSPList.Clear();
            ShadowColBSPList.Clear();
            InitBSPList();
        }

        // Shadow Level Editor

        public ShadowCollisionEditor shadowCollisionEditor = new ShadowCollisionEditor();
        public ShadowSplineMenu shadowSplineEditor = new ShadowSplineMenu();

        private void newToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            SetShadowMode();

            shadowCollisionEditor.InitBSPList();
            shadowSplineEditor.Init();

            ResetEveryting();
        }

        private void openToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            CommonOpenFileDialog openFile = new CommonOpenFileDialog()
            {
                IsFolderPicker = true
            };
            if (openONEfilePath != null)
                openFile.DefaultFileName = openONEfilePath;

            if (openFile.ShowDialog() == CommonFileDialogResult.Ok)
            {
                OpenONEShadowFolder(openFile.FileName);
            }
        }

        public void OpenONEShadowFolder(string fileName)
        {
            SetShadowMode();
            openONEfilePath = fileName;

            LoadShadowLevelFolder(Program.MainForm.renderer, openONEfilePath);

            InitBSPList();
            shadowCollisionEditor.InitBSPList();
        }
        
        private void saveToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            if (openONEfilePath != null)
                SaveShadowLevel(openONEfilePath);
            else
                saveAsToolStripMenuItem2_Click(sender, e);
        }

        private void saveAsToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            CommonOpenFileDialog saveFile = new CommonOpenFileDialog()
            {
                DefaultDirectory = Path.GetDirectoryName(openONEfilePath),
                IsFolderPicker = true
            };

            if (saveFile.ShowDialog() == CommonFileDialogResult.Ok)
            {
                SaveShadowLevel(saveFile.FileName);
            }
        }

        private void SaveShadowLevel(string levelPath)
        {
            SetShadowMode();
            openONEfilePath = levelPath;
            currentShadowFolderNamePrefix = Path.GetFileNameWithoutExtension(levelPath);

            string datONEpath = null;

            foreach (string fileName in Directory.GetFiles(levelPath))
            {
                if (Path.GetExtension(fileName).ToLower() == ".one"
                    & Path.GetFileName(fileName).StartsWith(currentShadowFolderNamePrefix)
                    & !fileName.Contains("dat")
                    & !fileName.Contains("fx")
                    & !fileName.Contains("gdt")
                    & !fileName.Contains("tex"))
                {
                    File.Delete(fileName);
                }
                else if (Path.GetExtension(fileName).ToLower() == ".one" & fileName.Contains("dat"))
                {
                    datONEpath = fileName;
                }
            }

            if (datONEpath == null)
                datONEpath = Path.Combine(openONEfilePath, currentShadowFolderNamePrefix + "_dat.one");

            List<RenderWareModelFile> fileList = new List<RenderWareModelFile>();
            fileList.AddRange(BSPList);
            fileList.AddRange(ShadowColBSPList);

            progressBar1.Minimum = 0;
            progressBar1.Value = 0;
            progressBar1.Maximum = 0;
            foreach (RenderWareModelFile i in fileList)
                progressBar1.Maximum += i.GetAsByteArray().Length;

            Dictionary<int, Archive> oneDict = new Dictionary<int, Archive>(fileList.Count);

            bool error = false;

            foreach (RenderWareModelFile i in fileList)
            {
                if (i.ChunkNumber == -1)
                {
                    error = true;
                    continue;
                }

                if (!oneDict.ContainsKey(i.ChunkNumber))
                    oneDict.Add(i.ChunkNumber, new Archive(CommonRWVersions.Shadow060));

                ArchiveFile item = new ArchiveFile(i.fileName, i.GetAsByteArray());
                oneDict[i.ChunkNumber].Files.Add(item);
                progressBar1.Value += i.GetAsByteArray().Length;
            }

            if (error) MessageBox.Show("Some of the files were not included in the archives because I could not figure out their chunk number. Please fix that and save again, otherwise those files will be lost.");

            foreach (int i in oneDict.Keys)
            {
                string fileName = Path.Combine(openONEfilePath, currentShadowFolderNamePrefix + "_" + i.ToString("D2") + ".one");
                File.WriteAllBytes(fileName, oneDict[i].BuildShadowONEArchive(true).ToArray());
            }
            
            InitBSPList();
            shadowCollisionEditor.InitBSPList();

            SaveShadowVisibilityFile(ChunkList, currentShadowFolderNamePrefix, datONEpath);
            shadowSplineEditor.Save(datONEpath);

            progressBar1.Value = 0;
        }

        private void collisionEditorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            shadowCollisionEditor.Show();
        }

        private void splineEditorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            shadowSplineEditor.Show();
        }

        // BLK Editor

        private void newToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            OpenVisibilityFile = null;
            ChunkList.Clear();
            numericCurrentChunk.Maximum = ChunkList.Count();
            labelChunkAmount.Text = "Amount: " + ChunkList.Count();
            labelLoadedBLK.Text = "No BLK loaded";
        }
        
        private void openToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog()
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
                    OpenVisibilityFile = null;
                    byte[] bytes = File.ReadAllBytes(fileName);
                    ChunkList = LoadShadowVisibilityFile(Archive.FromONEFile(ref bytes));
                    labelLoadedBLK.Text = "";
                }
                else
                {
                    OpenVisibilityFile = fileName;
                    ChunkList = LoadHeroesVisibilityFile(OpenVisibilityFile);
                    labelLoadedBLK.Text = "Loaded " + fileName;
                }

                numericCurrentChunk.Minimum = 1;
                numericCurrentChunk.Maximum = ChunkList.Count();
                numericCurrentChunk.Value = ChunkList.Count();
                if (numericCurrentChunk.Maximum != 0)
                    numericCurrentChunk.Value = 1;

                labelChunkAmount.Text = "Amount: " + ChunkList.Count();
            }
        }

        private void saveToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (OpenVisibilityFile != null)
                SaveHeroesVisibilityFile(ChunkList, OpenVisibilityFile);
            else
                saveAsToolStripMenuItem1_Click(sender, e);
        }

        private void saveAsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog()
            {
                Filter = "BIN files|*.bin",
                FileName = Path.GetFileName(OpenVisibilityFile),
                AddExtension = true,
                DefaultExt = "bin",
                InitialDirectory = Path.GetDirectoryName(OpenVisibilityFile)
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                OpenVisibilityFile = saveFileDialog.FileName;
                SaveHeroesVisibilityFile(ChunkList, OpenVisibilityFile);
                labelLoadedBLK.Text = "Loaded " + OpenVisibilityFile;
            }
        }

        private void importToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog()
            {
                Filter = "All supported files|*.bin;*.bdt|BIN files|*.bin|BDT files|*.bdt"
            };
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                if (Path.GetExtension(openFile.FileName).ToLower() == ".bdt")
                {
                    ChunkList.AddRange(LoadShadowVisibilityFile(new FileStream(openFile.FileName, FileMode.Open)));
                }
                else if (Path.GetExtension(openFile.FileName).ToLower() == ".bin")
                {
                    ChunkList.AddRange(LoadHeroesVisibilityFile(openFile.FileName));
                }

                numericCurrentChunk.Minimum = 1;
                numericCurrentChunk.Maximum = ChunkList.Count();
                numericCurrentChunk.Value = ChunkList.Count();
                if (numericCurrentChunk.Maximum != 0)
                    numericCurrentChunk.Value = 1;

                labelChunkAmount.Text = "Amount: " + ChunkList.Count();
            }
        }

        public string OpenVisibilityFile;
        bool ProgramIsChangingStuff = false;
        
        private void numericCurrentChunk_ValueChanged(object sender, EventArgs e)
        {
            ProgramIsChangingStuff = true;

            foreach (Chunk c in ChunkList)
                c.isSelected = false;
            
            int i = (int)numericCurrentChunk.Value - 1;

            if (ChunkList.Count > 0)
            {
                NumChunkNum.Value = ChunkList[i].number;
                NumMinX.Value = (decimal)ChunkList[i].Min.X;
                NumMinY.Value = (decimal)ChunkList[i].Min.Y;
                NumMinZ.Value = (decimal)ChunkList[i].Min.Z;
                NumMaxX.Value = (decimal)ChunkList[i].Max.X;
                NumMaxY.Value = (decimal)ChunkList[i].Max.Y;
                NumMaxZ.Value = (decimal)ChunkList[i].Max.Z;

                ChunkList[i].isSelected = true;
                ChunkList[i].CalculateModel();
            }

            ProgramIsChangingStuff = false;
        }

        private void buttonAddChunkClick(object sender, EventArgs e)
        {
            Chunk NewChunk = new Chunk();
            NewChunk.CalculateModel();
            ChunkList.Add(NewChunk);
            numericCurrentChunk.Minimum = 1;
            numericCurrentChunk.Maximum = ChunkList.Count();
            numericCurrentChunk.Value = ChunkList.Count();
            labelChunkAmount.Text = "Amount: " + ChunkList.Count();
        }

        private void buttonRemoveChunk_Click(object sender, EventArgs e)
        {
            if (ChunkList.Count > 0)
                ChunkList.RemoveAt((int)numericCurrentChunk.Value - 1);

            numericCurrentChunk.Maximum = ChunkList.Count();
            labelChunkAmount.Text = "Amount: " + ChunkList.Count();
            numericCurrentChunk_ValueChanged(new object(), new EventArgs());
        }

        private void buttonAutoChunk_Click(object sender, EventArgs e)
        {
            if (ChunkList.Count > 0)
            {
                AutoChunk(ChunkList[(int)numericCurrentChunk.Value - 1], out bool success, out Vector3 Min, out Vector3 Max);

                if (success)
                {
                    ChunkList[(int)numericCurrentChunk.Value - 1].Min.X = (int)(Min.X - (int)numericUpDownAdd.Value);
                    ChunkList[(int)numericCurrentChunk.Value - 1].Min.Y = (int)(Min.Y - (int)numericUpDownAdd.Value);
                    ChunkList[(int)numericCurrentChunk.Value - 1].Min.Z = (int)(Min.Z - (int)numericUpDownAdd.Value);
                    ChunkList[(int)numericCurrentChunk.Value - 1].Max.X = (int)(Max.X + (int)numericUpDownAdd.Value);
                    ChunkList[(int)numericCurrentChunk.Value - 1].Max.Y = (int)(Max.Y + (int)numericUpDownAdd.Value);
                    ChunkList[(int)numericCurrentChunk.Value - 1].Max.Z = (int)(Max.Z + (int)numericUpDownAdd.Value);
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
                if (ChunkList.Count > 0)
                    ChunkList[(int)numericCurrentChunk.Value - 1].number = (int)NumChunkNum.Value;
        }

        private void NumMaxMin_ValueChanged(object sender, EventArgs e)
        {
            if (!ProgramIsChangingStuff)
                if (ChunkList.Count > 0)
                {
                    ChunkList[(int)numericCurrentChunk.Value - 1].Min.X = (int)NumMinX.Value;
                    ChunkList[(int)numericCurrentChunk.Value - 1].Min.Y = (int)NumMinY.Value;
                    ChunkList[(int)numericCurrentChunk.Value - 1].Min.Z = (int)NumMinZ.Value;
                    ChunkList[(int)numericCurrentChunk.Value - 1].Max.X = (int)NumMaxX.Value;
                    ChunkList[(int)numericCurrentChunk.Value - 1].Max.Y = (int)NumMaxY.Value;
                    ChunkList[(int)numericCurrentChunk.Value - 1].Max.Z = (int)NumMaxZ.Value;
                    ChunkList[(int)numericCurrentChunk.Value - 1].CalculateModel();
                }
        }
    }
}