namespace HeroesPowerPlant.LevelEditor
{
    partial class LevelEditor
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            groupBox1 = new System.Windows.Forms.GroupBox();
            labelTriangleAmount = new System.Windows.Forms.Label();
            labelVertexAmount = new System.Windows.Forms.Label();
            checkBoxTristrip = new System.Windows.Forms.CheckBox();
            buttonImport = new System.Windows.Forms.Button();
            checkBoxFlipUVs = new System.Windows.Forms.CheckBox();
            buttonRemove = new System.Windows.Forms.Button();
            buttonClear = new System.Windows.Forms.Button();
            buttonExport = new System.Windows.Forms.Button();
            listViewLevelModels = new System.Windows.Forms.ListView();
            file = new System.Windows.Forms.ColumnHeader();
            groupBox2 = new System.Windows.Forms.GroupBox();
            buttonAutoBuild = new System.Windows.Forms.Button();
            labelChunkAmount = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            numericCurrentChunk = new System.Windows.Forms.NumericUpDown();
            label1 = new System.Windows.Forms.Label();
            numericUpDownAdd = new System.Windows.Forms.NumericUpDown();
            buttonAutoChunk = new System.Windows.Forms.Button();
            buttonRemoveChunk = new System.Windows.Forms.Button();
            groupBox3 = new System.Windows.Forms.GroupBox();
            NumMinZ = new System.Windows.Forms.NumericUpDown();
            NumMinY = new System.Windows.Forms.NumericUpDown();
            NumMinX = new System.Windows.Forms.NumericUpDown();
            groupBox4 = new System.Windows.Forms.GroupBox();
            NumMaxZ = new System.Windows.Forms.NumericUpDown();
            NumMaxY = new System.Windows.Forms.NumericUpDown();
            NumMaxX = new System.Windows.Forms.NumericUpDown();
            buttonAddChunk = new System.Windows.Forms.Button();
            NumChunkNum = new System.Windows.Forms.NumericUpDown();
            label2 = new System.Windows.Forms.Label();
            labelLoadedONE = new System.Windows.Forms.Label();
            menuStrip1 = new System.Windows.Forms.MenuStrip();
            oNEFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            disableFilesizeWarningToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            bLKFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            newToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            openToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            saveToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            saveAsToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            importToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ShadowLevelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ShadowLevelMenuItemNew = new System.Windows.Forms.ToolStripMenuItem();
            ShadowLevelMenuItemOpen = new System.Windows.Forms.ToolStripMenuItem();
            ShadowLevelMenuItemSave = new System.Windows.Forms.ToolStripMenuItem();
            ShadowLevelMenuItemSaveAs = new System.Windows.Forms.ToolStripMenuItem();
            toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            ShadowLevelMenuItemCollisionEditor = new System.Windows.Forms.ToolStripMenuItem();
            ShadowLevelMenuItemSplineEditor = new System.Windows.Forms.ToolStripMenuItem();
            ShadowLevelMenuItemImportBLK = new System.Windows.Forms.ToolStripMenuItem();
            ShadowLevelMenuItemSaveSplineDataOnly = new System.Windows.Forms.ToolStripMenuItem();
            labelLoadedBLK = new System.Windows.Forms.Label();
            progressBar1 = new System.Windows.Forms.ProgressBar();
            ButtonReassignMATFlag = new System.Windows.Forms.Button();
            textBox_import_extension = new System.Windows.Forms.TextBox();
            label_import_extension = new System.Windows.Forms.Label();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericCurrentChunk).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownAdd).BeginInit();
            groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)NumMinZ).BeginInit();
            ((System.ComponentModel.ISupportInitialize)NumMinY).BeginInit();
            ((System.ComponentModel.ISupportInitialize)NumMinX).BeginInit();
            groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)NumMaxZ).BeginInit();
            ((System.ComponentModel.ISupportInitialize)NumMaxY).BeginInit();
            ((System.ComponentModel.ISupportInitialize)NumMaxX).BeginInit();
            ((System.ComponentModel.ISupportInitialize)NumChunkNum).BeginInit();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(labelTriangleAmount);
            groupBox1.Controls.Add(labelVertexAmount);
            groupBox1.Controls.Add(checkBoxTristrip);
            groupBox1.Controls.Add(buttonImport);
            groupBox1.Controls.Add(checkBoxFlipUVs);
            groupBox1.Controls.Add(buttonRemove);
            groupBox1.Controls.Add(buttonClear);
            groupBox1.Controls.Add(buttonExport);
            groupBox1.Controls.Add(listViewLevelModels);
            groupBox1.Location = new System.Drawing.Point(14, 31);
            groupBox1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            groupBox1.Size = new System.Drawing.Size(240, 403);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Level Models";
            // 
            // labelTriangleAmount
            // 
            labelTriangleAmount.AutoSize = true;
            labelTriangleAmount.Location = new System.Drawing.Point(7, 291);
            labelTriangleAmount.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            labelTriangleAmount.Name = "labelTriangleAmount";
            labelTriangleAmount.Size = new System.Drawing.Size(56, 15);
            labelTriangleAmount.TabIndex = 25;
            labelTriangleAmount.Text = "Triangles:";
            // 
            // labelVertexAmount
            // 
            labelVertexAmount.AutoSize = true;
            labelVertexAmount.Location = new System.Drawing.Point(7, 270);
            labelVertexAmount.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            labelVertexAmount.Name = "labelVertexAmount";
            labelVertexAmount.Size = new System.Drawing.Size(50, 15);
            labelVertexAmount.TabIndex = 24;
            labelVertexAmount.Text = "Vertices:";
            // 
            // checkBoxTristrip
            // 
            checkBoxTristrip.AutoSize = true;
            checkBoxTristrip.Enabled = false;
            checkBoxTristrip.Location = new System.Drawing.Point(126, 309);
            checkBoxTristrip.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            checkBoxTristrip.Name = "checkBoxTristrip";
            checkBoxTristrip.Size = new System.Drawing.Size(88, 19);
            checkBoxTristrip.TabIndex = 24;
            checkBoxTristrip.Text = "Use Tristrips";
            checkBoxTristrip.UseVisualStyleBackColor = true;
            // 
            // buttonImport
            // 
            buttonImport.Enabled = false;
            buttonImport.Location = new System.Drawing.Point(7, 336);
            buttonImport.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            buttonImport.Name = "buttonImport";
            buttonImport.Size = new System.Drawing.Size(110, 27);
            buttonImport.TabIndex = 2;
            buttonImport.Text = "Import";
            buttonImport.UseVisualStyleBackColor = true;
            buttonImport.Click += buttonImport_Click;
            // 
            // checkBoxFlipUVs
            // 
            checkBoxFlipUVs.AutoSize = true;
            checkBoxFlipUVs.Location = new System.Drawing.Point(7, 309);
            checkBoxFlipUVs.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            checkBoxFlipUVs.Name = "checkBoxFlipUVs";
            checkBoxFlipUVs.Size = new System.Drawing.Size(104, 19);
            checkBoxFlipUVs.TabIndex = 22;
            checkBoxFlipUVs.Text = "Flip UV Coords";
            checkBoxFlipUVs.UseVisualStyleBackColor = true;
            // 
            // buttonRemove
            // 
            buttonRemove.Location = new System.Drawing.Point(7, 369);
            buttonRemove.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            buttonRemove.Name = "buttonRemove";
            buttonRemove.Size = new System.Drawing.Size(110, 27);
            buttonRemove.TabIndex = 4;
            buttonRemove.Text = "Remove";
            buttonRemove.UseVisualStyleBackColor = true;
            buttonRemove.Click += buttonRemove_Click;
            // 
            // buttonClear
            // 
            buttonClear.Location = new System.Drawing.Point(124, 369);
            buttonClear.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            buttonClear.Name = "buttonClear";
            buttonClear.Size = new System.Drawing.Size(110, 27);
            buttonClear.TabIndex = 5;
            buttonClear.Text = "Clear";
            buttonClear.UseVisualStyleBackColor = true;
            buttonClear.Click += buttonClear_Click;
            // 
            // buttonExport
            // 
            buttonExport.Location = new System.Drawing.Point(124, 336);
            buttonExport.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            buttonExport.Name = "buttonExport";
            buttonExport.Size = new System.Drawing.Size(110, 27);
            buttonExport.TabIndex = 6;
            buttonExport.Text = "Export";
            buttonExport.UseVisualStyleBackColor = true;
            buttonExport.Click += buttonExportClick;
            // 
            // listViewLevelModels
            // 
            listViewLevelModels.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            listViewLevelModels.CheckBoxes = true;
            listViewLevelModels.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] { file });
            listViewLevelModels.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            listViewLevelModels.Location = new System.Drawing.Point(7, 22);
            listViewLevelModels.Name = "listViewLevelModels";
            listViewLevelModels.Size = new System.Drawing.Size(226, 244);
            listViewLevelModels.TabIndex = 28;
            listViewLevelModels.UseCompatibleStateImageBehavior = false;
            listViewLevelModels.View = System.Windows.Forms.View.Details;
            listViewLevelModels.ItemChecked += listViewLevelModels_ItemChecked;
            listViewLevelModels.SelectedIndexChanged += listBoxLevelModels_SelectedIndexChanged;
            listViewLevelModels.DoubleClick += listBoxLevelModelsDoubleClick;
            listViewLevelModels.KeyDown += listBoxLevelModels_KeyDown;
            // 
            // file
            // 
            file.Text = "File";
            file.Width = 226;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(buttonAutoBuild);
            groupBox2.Controls.Add(labelChunkAmount);
            groupBox2.Controls.Add(label3);
            groupBox2.Controls.Add(numericCurrentChunk);
            groupBox2.Controls.Add(label1);
            groupBox2.Controls.Add(numericUpDownAdd);
            groupBox2.Controls.Add(buttonAutoChunk);
            groupBox2.Controls.Add(buttonRemoveChunk);
            groupBox2.Controls.Add(groupBox3);
            groupBox2.Controls.Add(groupBox4);
            groupBox2.Controls.Add(buttonAddChunk);
            groupBox2.Controls.Add(NumChunkNum);
            groupBox2.Controls.Add(label2);
            groupBox2.Location = new System.Drawing.Point(261, 31);
            groupBox2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            groupBox2.Name = "groupBox2";
            groupBox2.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            groupBox2.Size = new System.Drawing.Size(270, 339);
            groupBox2.TabIndex = 1;
            groupBox2.TabStop = false;
            groupBox2.Text = "Visibility Editor";
            // 
            // buttonAutoBuild
            // 
            buttonAutoBuild.Location = new System.Drawing.Point(14, 301);
            buttonAutoBuild.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            buttonAutoBuild.Name = "buttonAutoBuild";
            buttonAutoBuild.Size = new System.Drawing.Size(238, 27);
            buttonAutoBuild.TabIndex = 24;
            buttonAutoBuild.Text = "AutoBuild";
            buttonAutoBuild.UseVisualStyleBackColor = true;
            buttonAutoBuild.Click += ButtonAutoBuild_Click;
            // 
            // labelChunkAmount
            // 
            labelChunkAmount.AutoSize = true;
            labelChunkAmount.Location = new System.Drawing.Point(12, 66);
            labelChunkAmount.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            labelChunkAmount.Name = "labelChunkAmount";
            labelChunkAmount.Size = new System.Drawing.Size(57, 15);
            labelChunkAmount.TabIndex = 27;
            labelChunkAmount.Text = "Amount: ";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(12, 18);
            label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(92, 15);
            label3.TabIndex = 26;
            label3.Text = "Selected Chunk:";
            // 
            // numericCurrentChunk
            // 
            numericCurrentChunk.Location = new System.Drawing.Point(7, 39);
            numericCurrentChunk.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            numericCurrentChunk.Maximum = new decimal(new int[] { 1, 0, 0, 0 });
            numericCurrentChunk.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numericCurrentChunk.Name = "numericCurrentChunk";
            numericCurrentChunk.Size = new System.Drawing.Size(134, 23);
            numericCurrentChunk.TabIndex = 25;
            numericCurrentChunk.Value = new decimal(new int[] { 1, 0, 0, 0 });
            numericCurrentChunk.ValueChanged += numericCurrentChunk_ValueChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(7, 125);
            label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(96, 15);
            label1.TabIndex = 24;
            label1.Text = "AutoChunk Add:";
            // 
            // numericUpDownAdd
            // 
            numericUpDownAdd.Location = new System.Drawing.Point(113, 122);
            numericUpDownAdd.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            numericUpDownAdd.Maximum = new decimal(new int[] { int.MaxValue, 0, 0, 0 });
            numericUpDownAdd.Minimum = new decimal(new int[] { int.MinValue, 0, 0, int.MinValue });
            numericUpDownAdd.Name = "numericUpDownAdd";
            numericUpDownAdd.Size = new System.Drawing.Size(149, 23);
            numericUpDownAdd.TabIndex = 23;
            numericUpDownAdd.Value = new decimal(new int[] { 1000, 0, 0, 0 });
            // 
            // buttonAutoChunk
            // 
            buttonAutoChunk.Location = new System.Drawing.Point(148, 89);
            buttonAutoChunk.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            buttonAutoChunk.Name = "buttonAutoChunk";
            buttonAutoChunk.Size = new System.Drawing.Size(114, 27);
            buttonAutoChunk.TabIndex = 22;
            buttonAutoChunk.Text = "AutoChunk";
            buttonAutoChunk.UseVisualStyleBackColor = true;
            buttonAutoChunk.Click += buttonAutoChunk_Click;
            // 
            // buttonRemoveChunk
            // 
            buttonRemoveChunk.Location = new System.Drawing.Point(148, 55);
            buttonRemoveChunk.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            buttonRemoveChunk.Name = "buttonRemoveChunk";
            buttonRemoveChunk.Size = new System.Drawing.Size(115, 27);
            buttonRemoveChunk.TabIndex = 12;
            buttonRemoveChunk.Text = "Remove";
            buttonRemoveChunk.UseVisualStyleBackColor = true;
            buttonRemoveChunk.Click += buttonRemoveChunk_Click;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(NumMinZ);
            groupBox3.Controls.Add(NumMinY);
            groupBox3.Controls.Add(NumMinX);
            groupBox3.Location = new System.Drawing.Point(7, 182);
            groupBox3.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            groupBox3.Name = "groupBox3";
            groupBox3.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            groupBox3.Size = new System.Drawing.Size(121, 112);
            groupBox3.TabIndex = 20;
            groupBox3.TabStop = false;
            groupBox3.Text = "Min (X,Y,Z)";
            // 
            // NumMinZ
            // 
            NumMinZ.Location = new System.Drawing.Point(7, 82);
            NumMinZ.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            NumMinZ.Maximum = new decimal(new int[] { int.MaxValue, 0, 0, 0 });
            NumMinZ.Minimum = new decimal(new int[] { int.MinValue, 0, 0, int.MinValue });
            NumMinZ.Name = "NumMinZ";
            NumMinZ.Size = new System.Drawing.Size(107, 23);
            NumMinZ.TabIndex = 16;
            NumMinZ.ValueChanged += NumMaxMin_ValueChanged;
            // 
            // NumMinY
            // 
            NumMinY.Location = new System.Drawing.Point(7, 52);
            NumMinY.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            NumMinY.Maximum = new decimal(new int[] { int.MaxValue, 0, 0, 0 });
            NumMinY.Minimum = new decimal(new int[] { int.MinValue, 0, 0, int.MinValue });
            NumMinY.Name = "NumMinY";
            NumMinY.Size = new System.Drawing.Size(107, 23);
            NumMinY.TabIndex = 15;
            NumMinY.ValueChanged += NumMaxMin_ValueChanged;
            // 
            // NumMinX
            // 
            NumMinX.Location = new System.Drawing.Point(7, 22);
            NumMinX.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            NumMinX.Maximum = new decimal(new int[] { int.MaxValue, 0, 0, 0 });
            NumMinX.Minimum = new decimal(new int[] { int.MinValue, 0, 0, int.MinValue });
            NumMinX.Name = "NumMinX";
            NumMinX.Size = new System.Drawing.Size(107, 23);
            NumMinX.TabIndex = 14;
            NumMinX.ValueChanged += NumMaxMin_ValueChanged;
            // 
            // groupBox4
            // 
            groupBox4.Controls.Add(NumMaxZ);
            groupBox4.Controls.Add(NumMaxY);
            groupBox4.Controls.Add(NumMaxX);
            groupBox4.Location = new System.Drawing.Point(138, 182);
            groupBox4.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            groupBox4.Name = "groupBox4";
            groupBox4.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            groupBox4.Size = new System.Drawing.Size(125, 112);
            groupBox4.TabIndex = 21;
            groupBox4.TabStop = false;
            groupBox4.Text = "Max (X,Y,Z)";
            // 
            // NumMaxZ
            // 
            NumMaxZ.Location = new System.Drawing.Point(7, 82);
            NumMaxZ.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            NumMaxZ.Maximum = new decimal(new int[] { int.MaxValue, 0, 0, 0 });
            NumMaxZ.Minimum = new decimal(new int[] { int.MinValue, 0, 0, int.MinValue });
            NumMaxZ.Name = "NumMaxZ";
            NumMaxZ.Size = new System.Drawing.Size(107, 23);
            NumMaxZ.TabIndex = 19;
            NumMaxZ.ValueChanged += NumMaxMin_ValueChanged;
            // 
            // NumMaxY
            // 
            NumMaxY.Location = new System.Drawing.Point(7, 52);
            NumMaxY.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            NumMaxY.Maximum = new decimal(new int[] { int.MaxValue, 0, 0, 0 });
            NumMaxY.Minimum = new decimal(new int[] { int.MinValue, 0, 0, int.MinValue });
            NumMaxY.Name = "NumMaxY";
            NumMaxY.Size = new System.Drawing.Size(107, 23);
            NumMaxY.TabIndex = 18;
            NumMaxY.ValueChanged += NumMaxMin_ValueChanged;
            // 
            // NumMaxX
            // 
            NumMaxX.Location = new System.Drawing.Point(7, 22);
            NumMaxX.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            NumMaxX.Maximum = new decimal(new int[] { int.MaxValue, 0, 0, 0 });
            NumMaxX.Minimum = new decimal(new int[] { int.MinValue, 0, 0, int.MinValue });
            NumMaxX.Name = "NumMaxX";
            NumMaxX.Size = new System.Drawing.Size(107, 23);
            NumMaxX.TabIndex = 17;
            NumMaxX.ValueChanged += NumMaxMin_ValueChanged;
            // 
            // buttonAddChunk
            // 
            buttonAddChunk.Location = new System.Drawing.Point(148, 22);
            buttonAddChunk.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            buttonAddChunk.Name = "buttonAddChunk";
            buttonAddChunk.Size = new System.Drawing.Size(115, 27);
            buttonAddChunk.TabIndex = 11;
            buttonAddChunk.Text = "Add";
            buttonAddChunk.UseVisualStyleBackColor = true;
            buttonAddChunk.Click += buttonAddChunkClick;
            // 
            // NumChunkNum
            // 
            NumChunkNum.Location = new System.Drawing.Point(113, 152);
            NumChunkNum.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            NumChunkNum.Maximum = new decimal(new int[] { 255, 0, 0, 0 });
            NumChunkNum.Name = "NumChunkNum";
            NumChunkNum.Size = new System.Drawing.Size(150, 23);
            NumChunkNum.TabIndex = 13;
            NumChunkNum.ValueChanged += NumChunkNum_ValueChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(12, 155);
            label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(92, 15);
            label2.TabIndex = 8;
            label2.Text = "Chunk Number:";
            // 
            // labelLoadedONE
            // 
            labelLoadedONE.AutoSize = true;
            labelLoadedONE.Location = new System.Drawing.Point(14, 471);
            labelLoadedONE.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            labelLoadedONE.Name = "labelLoadedONE";
            labelLoadedONE.Size = new System.Drawing.Size(89, 15);
            labelLoadedONE.TabIndex = 2;
            labelLoadedONE.Text = "No ONE loaded";
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { oNEFileToolStripMenuItem, bLKFileToolStripMenuItem, ShadowLevelToolStripMenuItem });
            menuStrip1.Location = new System.Drawing.Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Padding = new System.Windows.Forms.Padding(7, 2, 0, 2);
            menuStrip1.Size = new System.Drawing.Size(539, 24);
            menuStrip1.TabIndex = 16;
            menuStrip1.Text = "menuStrip1";
            // 
            // oNEFileToolStripMenuItem
            // 
            oNEFileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { newToolStripMenuItem, openToolStripMenuItem, saveToolStripMenuItem, saveAsToolStripMenuItem, toolStripSeparator3, disableFilesizeWarningToolStripMenuItem });
            oNEFileToolStripMenuItem.Name = "oNEFileToolStripMenuItem";
            oNEFileToolStripMenuItem.Size = new System.Drawing.Size(64, 20);
            oNEFileToolStripMenuItem.Text = "ONE File";
            // 
            // newToolStripMenuItem
            // 
            newToolStripMenuItem.Name = "newToolStripMenuItem";
            newToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
            newToolStripMenuItem.Text = "New";
            newToolStripMenuItem.Click += newToolStripMenuItem_Click;
            // 
            // openToolStripMenuItem
            // 
            openToolStripMenuItem.Name = "openToolStripMenuItem";
            openToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
            openToolStripMenuItem.Text = "Open";
            openToolStripMenuItem.Click += openToolStripMenuItem_Click;
            // 
            // saveToolStripMenuItem
            // 
            saveToolStripMenuItem.Enabled = false;
            saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            saveToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
            saveToolStripMenuItem.Text = "Save";
            saveToolStripMenuItem.Click += saveToolStripMenuItem_Click;
            // 
            // saveAsToolStripMenuItem
            // 
            saveAsToolStripMenuItem.Enabled = false;
            saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            saveAsToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
            saveAsToolStripMenuItem.Text = "Save As...";
            saveAsToolStripMenuItem.Click += saveAsToolStripMenuItem_Click;
            // 
            // toolStripSeparator3
            // 
            toolStripSeparator3.Name = "toolStripSeparator3";
            toolStripSeparator3.Size = new System.Drawing.Size(197, 6);
            // 
            // disableFilesizeWarningToolStripMenuItem
            // 
            disableFilesizeWarningToolStripMenuItem.Name = "disableFilesizeWarningToolStripMenuItem";
            disableFilesizeWarningToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
            disableFilesizeWarningToolStripMenuItem.Text = "Disable Filesize Warning";
            disableFilesizeWarningToolStripMenuItem.Click += DisableFilesizeWarningToolStripMenuItem_Click;
            // 
            // bLKFileToolStripMenuItem
            // 
            bLKFileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { newToolStripMenuItem1, openToolStripMenuItem1, saveToolStripMenuItem1, saveAsToolStripMenuItem1, toolStripSeparator2, importToolStripMenuItem });
            bLKFileToolStripMenuItem.Name = "bLKFileToolStripMenuItem";
            bLKFileToolStripMenuItem.Size = new System.Drawing.Size(60, 20);
            bLKFileToolStripMenuItem.Text = "BLK File";
            // 
            // newToolStripMenuItem1
            // 
            newToolStripMenuItem1.Name = "newToolStripMenuItem1";
            newToolStripMenuItem1.Size = new System.Drawing.Size(123, 22);
            newToolStripMenuItem1.Text = "New";
            newToolStripMenuItem1.Click += newToolStripMenuItem1_Click;
            // 
            // openToolStripMenuItem1
            // 
            openToolStripMenuItem1.Name = "openToolStripMenuItem1";
            openToolStripMenuItem1.Size = new System.Drawing.Size(123, 22);
            openToolStripMenuItem1.Text = "Open";
            openToolStripMenuItem1.Click += openToolStripMenuItem1_Click;
            // 
            // saveToolStripMenuItem1
            // 
            saveToolStripMenuItem1.Name = "saveToolStripMenuItem1";
            saveToolStripMenuItem1.Size = new System.Drawing.Size(123, 22);
            saveToolStripMenuItem1.Text = "Save";
            saveToolStripMenuItem1.Click += saveToolStripMenuItemVisibility_Click;
            // 
            // saveAsToolStripMenuItem1
            // 
            saveAsToolStripMenuItem1.Name = "saveAsToolStripMenuItem1";
            saveAsToolStripMenuItem1.Size = new System.Drawing.Size(123, 22);
            saveAsToolStripMenuItem1.Text = "Save As...";
            saveAsToolStripMenuItem1.Click += saveAsToolStripMenuItem1_Click;
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new System.Drawing.Size(120, 6);
            // 
            // importToolStripMenuItem
            // 
            importToolStripMenuItem.Name = "importToolStripMenuItem";
            importToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
            importToolStripMenuItem.Text = "Import";
            importToolStripMenuItem.Click += importBLKToolStripMenuItem_Click;
            // 
            // ShadowLevelToolStripMenuItem
            // 
            ShadowLevelToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { ShadowLevelMenuItemNew, ShadowLevelMenuItemOpen, ShadowLevelMenuItemSave, ShadowLevelMenuItemSaveAs, toolStripSeparator1, ShadowLevelMenuItemCollisionEditor, ShadowLevelMenuItemSplineEditor, ShadowLevelMenuItemSaveSplineDataOnly, ShadowLevelMenuItemImportBLK });
            ShadowLevelToolStripMenuItem.Name = "ShadowLevelToolStripMenuItem";
            ShadowLevelToolStripMenuItem.Size = new System.Drawing.Size(91, 20);
            ShadowLevelToolStripMenuItem.Text = "Shadow Level";
            // 
            // ShadowLevelMenuItemNew
            // 
            ShadowLevelMenuItemNew.Name = "ShadowLevelMenuItemNew";
            ShadowLevelMenuItemNew.Size = new System.Drawing.Size(188, 22);
            ShadowLevelMenuItemNew.Text = "New";
            ShadowLevelMenuItemNew.Click += ShadowLevelMenuItemNew_Click;
            // 
            // ShadowLevelMenuItemOpen
            // 
            ShadowLevelMenuItemOpen.Name = "ShadowLevelMenuItemOpen";
            ShadowLevelMenuItemOpen.Size = new System.Drawing.Size(188, 22);
            ShadowLevelMenuItemOpen.Text = "Open";
            ShadowLevelMenuItemOpen.Click += ShadowLevelMenuItemOpen_Click;
            // 
            // ShadowLevelMenuItemSave
            // 
            ShadowLevelMenuItemSave.Enabled = false;
            ShadowLevelMenuItemSave.Name = "ShadowLevelMenuItemSave";
            ShadowLevelMenuItemSave.Size = new System.Drawing.Size(188, 22);
            ShadowLevelMenuItemSave.Text = "Save";
            ShadowLevelMenuItemSave.Click += ShadowLevelMenuItemSave_Click;
            // 
            // ShadowLevelMenuItemSaveAs
            // 
            ShadowLevelMenuItemSaveAs.Enabled = false;
            ShadowLevelMenuItemSaveAs.Name = "ShadowLevelMenuItemSaveAs";
            ShadowLevelMenuItemSaveAs.Size = new System.Drawing.Size(188, 22);
            ShadowLevelMenuItemSaveAs.Text = "Save As...";
            ShadowLevelMenuItemSaveAs.Click += ShadowLevelMenuItemSaveAs_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new System.Drawing.Size(185, 6);
            // 
            // ShadowLevelMenuItemCollisionEditor
            // 
            ShadowLevelMenuItemCollisionEditor.Enabled = false;
            ShadowLevelMenuItemCollisionEditor.Name = "ShadowLevelMenuItemCollisionEditor";
            ShadowLevelMenuItemCollisionEditor.Size = new System.Drawing.Size(188, 22);
            ShadowLevelMenuItemCollisionEditor.Text = "Collision Editor";
            ShadowLevelMenuItemCollisionEditor.Click += ShadowLevelMenuItemCollisionEditor_Click;
            // 
            // ShadowLevelMenuItemSplineEditor
            // 
            ShadowLevelMenuItemSplineEditor.Enabled = false;
            ShadowLevelMenuItemSplineEditor.Name = "ShadowLevelMenuItemSplineEditor";
            ShadowLevelMenuItemSplineEditor.Size = new System.Drawing.Size(188, 22);
            ShadowLevelMenuItemSplineEditor.Text = "Spline Editor";
            ShadowLevelMenuItemSplineEditor.Click += ShadowLevelMenuItemSplineEditor_Click;
            // 
            // ShadowLevelMenuItemImportBLK
            // 
            ShadowLevelMenuItemImportBLK.Enabled = false;
            ShadowLevelMenuItemImportBLK.Name = "ShadowLevelMenuItemImportBLK";
            ShadowLevelMenuItemImportBLK.Size = new System.Drawing.Size(188, 22);
            ShadowLevelMenuItemImportBLK.Text = "Import BLK";
            ShadowLevelMenuItemImportBLK.Click += importBLKToolStripMenuItem_Click;
            // 
            // ShadowLevelMenuItemSaveSplineDataOnly
            // 
            ShadowLevelMenuItemSaveSplineDataOnly.Name = "ShadowLevelMenuItemSaveSplineDataOnly";
            ShadowLevelMenuItemSaveSplineDataOnly.Size = new System.Drawing.Size(188, 22);
            ShadowLevelMenuItemSaveSplineDataOnly.Text = "Save Spline Data Only";
            ShadowLevelMenuItemSaveSplineDataOnly.Click += ShadowLevelMenuItemSaveSplineDataOnly_Click;
            // 
            // labelLoadedBLK
            // 
            labelLoadedBLK.AutoSize = true;
            labelLoadedBLK.Location = new System.Drawing.Point(14, 493);
            labelLoadedBLK.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            labelLoadedBLK.Name = "labelLoadedBLK";
            labelLoadedBLK.Size = new System.Drawing.Size(85, 15);
            labelLoadedBLK.TabIndex = 2;
            labelLoadedBLK.Text = "No BLK loaded";
            // 
            // progressBar1
            // 
            progressBar1.Location = new System.Drawing.Point(14, 441);
            progressBar1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            progressBar1.Name = "progressBar1";
            progressBar1.Size = new System.Drawing.Size(517, 27);
            progressBar1.TabIndex = 23;
            // 
            // ButtonReassignMATFlag
            // 
            ButtonReassignMATFlag.Location = new System.Drawing.Point(262, 400);
            ButtonReassignMATFlag.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            ButtonReassignMATFlag.Name = "ButtonReassignMATFlag";
            ButtonReassignMATFlag.Size = new System.Drawing.Size(110, 27);
            ButtonReassignMATFlag.TabIndex = 24;
            ButtonReassignMATFlag.Text = "Reassign MATFlag";
            ButtonReassignMATFlag.UseVisualStyleBackColor = true;
            ButtonReassignMATFlag.Click += ButtonReassignMATFlag_Click;
            // 
            // textBox_import_extension
            // 
            textBox_import_extension.Location = new System.Drawing.Point(406, 404);
            textBox_import_extension.Name = "textBox_import_extension";
            textBox_import_extension.Size = new System.Drawing.Size(100, 23);
            textBox_import_extension.TabIndex = 25;
            textBox_import_extension.Text = ".BSP";
            textBox_import_extension.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label_import_extension
            // 
            label_import_extension.AutoSize = true;
            label_import_extension.Location = new System.Drawing.Point(406, 386);
            label_import_extension.Name = "label_import_extension";
            label_import_extension.Size = new System.Drawing.Size(97, 15);
            label_import_extension.TabIndex = 26;
            label_import_extension.Text = "Import Extension";
            // 
            // LevelEditor
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(539, 517);
            Controls.Add(label_import_extension);
            Controls.Add(textBox_import_extension);
            Controls.Add(ButtonReassignMATFlag);
            Controls.Add(progressBar1);
            Controls.Add(labelLoadedBLK);
            Controls.Add(labelLoadedONE);
            Controls.Add(menuStrip1);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            MainMenuStrip = menuStrip1;
            Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            MaximizeBox = false;
            Name = "LevelEditor";
            ShowIcon = false;
            Text = "Level Editor";
            Load += LevelEditor_Load;
            KeyDown += listBoxLevelModels_KeyDown;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numericCurrentChunk).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownAdd).EndInit();
            groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)NumMinZ).EndInit();
            ((System.ComponentModel.ISupportInitialize)NumMinY).EndInit();
            ((System.ComponentModel.ISupportInitialize)NumMinX).EndInit();
            groupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)NumMaxZ).EndInit();
            ((System.ComponentModel.ISupportInitialize)NumMaxY).EndInit();
            ((System.ComponentModel.ISupportInitialize)NumMaxX).EndInit();
            ((System.ComponentModel.ISupportInitialize)NumChunkNum).EndInit();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button buttonRemove;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button buttonClear;
        public System.Windows.Forms.Button buttonExport;
        private System.Windows.Forms.Button buttonImport;
        private System.Windows.Forms.Label labelLoadedONE;
        internal System.Windows.Forms.Button buttonRemoveChunk;
        internal System.Windows.Forms.GroupBox groupBox3;
        internal System.Windows.Forms.NumericUpDown NumMinZ;
        internal System.Windows.Forms.NumericUpDown NumMinY;
        internal System.Windows.Forms.NumericUpDown NumMinX;
        internal System.Windows.Forms.GroupBox groupBox4;
        internal System.Windows.Forms.NumericUpDown NumMaxZ;
        internal System.Windows.Forms.NumericUpDown NumMaxY;
        internal System.Windows.Forms.NumericUpDown NumMaxX;
        internal System.Windows.Forms.Button buttonAddChunk;
        internal System.Windows.Forms.NumericUpDown NumChunkNum;
        internal System.Windows.Forms.Label label2;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem oNEFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bLKFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem1;
        private System.Windows.Forms.Label labelLoadedBLK;
        public System.Windows.Forms.CheckBox checkBoxFlipUVs;
        public System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.NumericUpDown numericUpDownAdd;
        private System.Windows.Forms.Button buttonAutoChunk;
        public System.Windows.Forms.CheckBox checkBoxTristrip;
        private System.Windows.Forms.Label labelTriangleAmount;
        private System.Windows.Forms.Label labelVertexAmount;
        internal System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelChunkAmount;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numericCurrentChunk;
        private System.Windows.Forms.ToolStripMenuItem ShadowLevelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ShadowLevelMenuItemNew;
        private System.Windows.Forms.ToolStripMenuItem ShadowLevelMenuItemOpen;
        private System.Windows.Forms.ToolStripMenuItem ShadowLevelMenuItemSave;
        private System.Windows.Forms.ToolStripMenuItem ShadowLevelMenuItemSaveAs;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem ShadowLevelMenuItemCollisionEditor;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem importToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ShadowLevelMenuItemSplineEditor;
        private System.Windows.Forms.ToolStripMenuItem ShadowLevelMenuItemImportBLK;
        private System.Windows.Forms.Button buttonAutoBuild;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem disableFilesizeWarningToolStripMenuItem;
        private System.Windows.Forms.Button ButtonReassignMATFlag;
        private System.Windows.Forms.TextBox textBox_import_extension;
        private System.Windows.Forms.Label label_import_extension;
        private System.Windows.Forms.ListView listViewLevelModels;
        private System.Windows.Forms.ColumnHeader file;
        private System.Windows.Forms.ToolStripMenuItem ShadowLevelMenuItemSaveSplineDataOnly;
    }
}