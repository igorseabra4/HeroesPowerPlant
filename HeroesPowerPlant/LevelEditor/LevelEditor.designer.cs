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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.labelTriangleAmount = new System.Windows.Forms.Label();
            this.labelVertexAmount = new System.Windows.Forms.Label();
            this.checkBoxTristrip = new System.Windows.Forms.CheckBox();
            this.buttonImport = new System.Windows.Forms.Button();
            this.checkBoxFlipUVs = new System.Windows.Forms.CheckBox();
            this.buttonRemove = new System.Windows.Forms.Button();
            this.buttonClear = new System.Windows.Forms.Button();
            this.buttonExport = new System.Windows.Forms.Button();
            this.listBoxLevelModels = new System.Windows.Forms.ListBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.labelChunkAmount = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.numericCurrentChunk = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.numericUpDownAdd = new System.Windows.Forms.NumericUpDown();
            this.buttonAutoChunk = new System.Windows.Forms.Button();
            this.buttonRemoveChunk = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.NumMinZ = new System.Windows.Forms.NumericUpDown();
            this.NumMinY = new System.Windows.Forms.NumericUpDown();
            this.NumMinX = new System.Windows.Forms.NumericUpDown();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.NumMaxZ = new System.Windows.Forms.NumericUpDown();
            this.NumMaxY = new System.Windows.Forms.NumericUpDown();
            this.NumMaxX = new System.Windows.Forms.NumericUpDown();
            this.buttonAddChunk = new System.Windows.Forms.Button();
            this.NumChunkNum = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.labelLoadedONE = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.oNEFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bLKFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.shadowLevelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.collisionEditorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.labelLoadedBLK = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericCurrentChunk)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAdd)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NumMinZ)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumMinY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumMinX)).BeginInit();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NumMaxZ)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumMaxY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumMaxX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumChunkNum)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.labelTriangleAmount);
            this.groupBox1.Controls.Add(this.labelVertexAmount);
            this.groupBox1.Controls.Add(this.checkBoxTristrip);
            this.groupBox1.Controls.Add(this.buttonImport);
            this.groupBox1.Controls.Add(this.checkBoxFlipUVs);
            this.groupBox1.Controls.Add(this.buttonRemove);
            this.groupBox1.Controls.Add(this.buttonClear);
            this.groupBox1.Controls.Add(this.buttonExport);
            this.groupBox1.Controls.Add(this.listBoxLevelModels);
            this.groupBox1.Location = new System.Drawing.Point(12, 27);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(206, 349);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Level Models";
            // 
            // labelTriangleAmount
            // 
            this.labelTriangleAmount.AutoSize = true;
            this.labelTriangleAmount.Location = new System.Drawing.Point(6, 252);
            this.labelTriangleAmount.Name = "labelTriangleAmount";
            this.labelTriangleAmount.Size = new System.Drawing.Size(53, 13);
            this.labelTriangleAmount.TabIndex = 25;
            this.labelTriangleAmount.Text = "Triangles:";
            // 
            // labelVertexAmount
            // 
            this.labelVertexAmount.AutoSize = true;
            this.labelVertexAmount.Location = new System.Drawing.Point(6, 234);
            this.labelVertexAmount.Name = "labelVertexAmount";
            this.labelVertexAmount.Size = new System.Drawing.Size(48, 13);
            this.labelVertexAmount.TabIndex = 24;
            this.labelVertexAmount.Text = "Vertices:";
            // 
            // checkBoxTristrip
            // 
            this.checkBoxTristrip.AutoSize = true;
            this.checkBoxTristrip.Enabled = false;
            this.checkBoxTristrip.Location = new System.Drawing.Point(108, 268);
            this.checkBoxTristrip.Name = "checkBoxTristrip";
            this.checkBoxTristrip.Size = new System.Drawing.Size(84, 17);
            this.checkBoxTristrip.TabIndex = 24;
            this.checkBoxTristrip.Text = "Use Tristrips";
            this.checkBoxTristrip.UseVisualStyleBackColor = true;
            // 
            // buttonImport
            // 
            this.buttonImport.Enabled = false;
            this.buttonImport.Location = new System.Drawing.Point(6, 291);
            this.buttonImport.Name = "buttonImport";
            this.buttonImport.Size = new System.Drawing.Size(94, 23);
            this.buttonImport.TabIndex = 2;
            this.buttonImport.Text = "Import";
            this.buttonImport.UseVisualStyleBackColor = true;
            this.buttonImport.Click += new System.EventHandler(this.buttonImport_Click);
            // 
            // checkBoxFlipUVs
            // 
            this.checkBoxFlipUVs.AutoSize = true;
            this.checkBoxFlipUVs.Location = new System.Drawing.Point(6, 268);
            this.checkBoxFlipUVs.Name = "checkBoxFlipUVs";
            this.checkBoxFlipUVs.Size = new System.Drawing.Size(96, 17);
            this.checkBoxFlipUVs.TabIndex = 22;
            this.checkBoxFlipUVs.Text = "Flip UV Coords";
            this.checkBoxFlipUVs.UseVisualStyleBackColor = true;
            // 
            // buttonRemove
            // 
            this.buttonRemove.Location = new System.Drawing.Point(6, 320);
            this.buttonRemove.Name = "buttonRemove";
            this.buttonRemove.Size = new System.Drawing.Size(94, 23);
            this.buttonRemove.TabIndex = 4;
            this.buttonRemove.Text = "Remove";
            this.buttonRemove.UseVisualStyleBackColor = true;
            this.buttonRemove.Click += new System.EventHandler(this.buttonRemove_Click);
            // 
            // buttonClear
            // 
            this.buttonClear.Location = new System.Drawing.Point(106, 320);
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.Size = new System.Drawing.Size(94, 23);
            this.buttonClear.TabIndex = 5;
            this.buttonClear.Text = "Clear";
            this.buttonClear.UseVisualStyleBackColor = true;
            this.buttonClear.Click += new System.EventHandler(this.buttonClear_Click);
            // 
            // buttonExport
            // 
            this.buttonExport.Location = new System.Drawing.Point(106, 291);
            this.buttonExport.Name = "buttonExport";
            this.buttonExport.Size = new System.Drawing.Size(94, 23);
            this.buttonExport.TabIndex = 6;
            this.buttonExport.Text = "Export";
            this.buttonExport.UseVisualStyleBackColor = true;
            this.buttonExport.Click += new System.EventHandler(this.buttonExportClick);
            // 
            // listBoxLevelModels
            // 
            this.listBoxLevelModels.FormattingEnabled = true;
            this.listBoxLevelModels.Location = new System.Drawing.Point(6, 19);
            this.listBoxLevelModels.Name = "listBoxLevelModels";
            this.listBoxLevelModels.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.listBoxLevelModels.Size = new System.Drawing.Size(194, 212);
            this.listBoxLevelModels.TabIndex = 1;
            this.listBoxLevelModels.SelectedIndexChanged += new System.EventHandler(this.listBoxLevelModels_SelectedIndexChanged);
            this.listBoxLevelModels.DoubleClick += new System.EventHandler(this.listBoxLevelModelsDoubleClick);
            this.listBoxLevelModels.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listBoxLevelModels_KeyDown);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.labelChunkAmount);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.numericCurrentChunk);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.numericUpDownAdd);
            this.groupBox2.Controls.Add(this.buttonAutoChunk);
            this.groupBox2.Controls.Add(this.buttonRemoveChunk);
            this.groupBox2.Controls.Add(this.groupBox3);
            this.groupBox2.Controls.Add(this.groupBox4);
            this.groupBox2.Controls.Add(this.buttonAddChunk);
            this.groupBox2.Controls.Add(this.NumChunkNum);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Location = new System.Drawing.Point(224, 27);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(231, 261);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Visibility Editor";
            // 
            // labelChunkAmount
            // 
            this.labelChunkAmount.AutoSize = true;
            this.labelChunkAmount.Location = new System.Drawing.Point(10, 57);
            this.labelChunkAmount.Name = "labelChunkAmount";
            this.labelChunkAmount.Size = new System.Drawing.Size(49, 13);
            this.labelChunkAmount.TabIndex = 27;
            this.labelChunkAmount.Text = "Amount: ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(86, 13);
            this.label3.TabIndex = 26;
            this.label3.Text = "Selected Chunk:";
            // 
            // numericCurrentChunk
            // 
            this.numericCurrentChunk.Location = new System.Drawing.Point(6, 34);
            this.numericCurrentChunk.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericCurrentChunk.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericCurrentChunk.Name = "numericCurrentChunk";
            this.numericCurrentChunk.Size = new System.Drawing.Size(115, 20);
            this.numericCurrentChunk.TabIndex = 25;
            this.numericCurrentChunk.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericCurrentChunk.ValueChanged += new System.EventHandler(this.numericCurrentChunk_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 108);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 13);
            this.label1.TabIndex = 24;
            this.label1.Text = "AutoChunk Add:";
            // 
            // numericUpDownAdd
            // 
            this.numericUpDownAdd.Location = new System.Drawing.Point(97, 106);
            this.numericUpDownAdd.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.numericUpDownAdd.Minimum = new decimal(new int[] {
            -2147483648,
            0,
            0,
            -2147483648});
            this.numericUpDownAdd.Name = "numericUpDownAdd";
            this.numericUpDownAdd.Size = new System.Drawing.Size(128, 20);
            this.numericUpDownAdd.TabIndex = 23;
            this.numericUpDownAdd.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            // 
            // buttonAutoChunk
            // 
            this.buttonAutoChunk.Location = new System.Drawing.Point(127, 77);
            this.buttonAutoChunk.Name = "buttonAutoChunk";
            this.buttonAutoChunk.Size = new System.Drawing.Size(98, 23);
            this.buttonAutoChunk.TabIndex = 22;
            this.buttonAutoChunk.Text = "AutoChunk";
            this.buttonAutoChunk.UseVisualStyleBackColor = true;
            this.buttonAutoChunk.Click += new System.EventHandler(this.buttonAutoChunk_Click);
            // 
            // buttonRemoveChunk
            // 
            this.buttonRemoveChunk.Location = new System.Drawing.Point(127, 48);
            this.buttonRemoveChunk.Name = "buttonRemoveChunk";
            this.buttonRemoveChunk.Size = new System.Drawing.Size(99, 23);
            this.buttonRemoveChunk.TabIndex = 12;
            this.buttonRemoveChunk.Text = "Remove";
            this.buttonRemoveChunk.UseVisualStyleBackColor = true;
            this.buttonRemoveChunk.Click += new System.EventHandler(this.buttonRemoveChunk_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.NumMinZ);
            this.groupBox3.Controls.Add(this.NumMinY);
            this.groupBox3.Controls.Add(this.NumMinX);
            this.groupBox3.Location = new System.Drawing.Point(6, 158);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(104, 97);
            this.groupBox3.TabIndex = 20;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Min (X,Y,Z)";
            // 
            // NumMinZ
            // 
            this.NumMinZ.Location = new System.Drawing.Point(6, 71);
            this.NumMinZ.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.NumMinZ.Minimum = new decimal(new int[] {
            -2147483648,
            0,
            0,
            -2147483648});
            this.NumMinZ.Name = "NumMinZ";
            this.NumMinZ.Size = new System.Drawing.Size(92, 20);
            this.NumMinZ.TabIndex = 16;
            this.NumMinZ.ValueChanged += new System.EventHandler(this.NumMaxMin_ValueChanged);
            // 
            // NumMinY
            // 
            this.NumMinY.Location = new System.Drawing.Point(6, 45);
            this.NumMinY.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.NumMinY.Minimum = new decimal(new int[] {
            -2147483648,
            0,
            0,
            -2147483648});
            this.NumMinY.Name = "NumMinY";
            this.NumMinY.Size = new System.Drawing.Size(92, 20);
            this.NumMinY.TabIndex = 15;
            this.NumMinY.ValueChanged += new System.EventHandler(this.NumMaxMin_ValueChanged);
            // 
            // NumMinX
            // 
            this.NumMinX.Location = new System.Drawing.Point(6, 19);
            this.NumMinX.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.NumMinX.Minimum = new decimal(new int[] {
            -2147483648,
            0,
            0,
            -2147483648});
            this.NumMinX.Name = "NumMinX";
            this.NumMinX.Size = new System.Drawing.Size(92, 20);
            this.NumMinX.TabIndex = 14;
            this.NumMinX.ValueChanged += new System.EventHandler(this.NumMaxMin_ValueChanged);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.NumMaxZ);
            this.groupBox4.Controls.Add(this.NumMaxY);
            this.groupBox4.Controls.Add(this.NumMaxX);
            this.groupBox4.Location = new System.Drawing.Point(118, 158);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(107, 97);
            this.groupBox4.TabIndex = 21;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Max (X,Y,Z)";
            // 
            // NumMaxZ
            // 
            this.NumMaxZ.Location = new System.Drawing.Point(6, 71);
            this.NumMaxZ.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.NumMaxZ.Minimum = new decimal(new int[] {
            -2147483648,
            0,
            0,
            -2147483648});
            this.NumMaxZ.Name = "NumMaxZ";
            this.NumMaxZ.Size = new System.Drawing.Size(92, 20);
            this.NumMaxZ.TabIndex = 19;
            this.NumMaxZ.ValueChanged += new System.EventHandler(this.NumMaxMin_ValueChanged);
            // 
            // NumMaxY
            // 
            this.NumMaxY.Location = new System.Drawing.Point(6, 45);
            this.NumMaxY.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.NumMaxY.Minimum = new decimal(new int[] {
            -2147483648,
            0,
            0,
            -2147483648});
            this.NumMaxY.Name = "NumMaxY";
            this.NumMaxY.Size = new System.Drawing.Size(92, 20);
            this.NumMaxY.TabIndex = 18;
            this.NumMaxY.ValueChanged += new System.EventHandler(this.NumMaxMin_ValueChanged);
            // 
            // NumMaxX
            // 
            this.NumMaxX.Location = new System.Drawing.Point(6, 19);
            this.NumMaxX.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.NumMaxX.Minimum = new decimal(new int[] {
            -2147483648,
            0,
            0,
            -2147483648});
            this.NumMaxX.Name = "NumMaxX";
            this.NumMaxX.Size = new System.Drawing.Size(92, 20);
            this.NumMaxX.TabIndex = 17;
            this.NumMaxX.ValueChanged += new System.EventHandler(this.NumMaxMin_ValueChanged);
            // 
            // buttonAddChunk
            // 
            this.buttonAddChunk.Location = new System.Drawing.Point(127, 19);
            this.buttonAddChunk.Name = "buttonAddChunk";
            this.buttonAddChunk.Size = new System.Drawing.Size(99, 23);
            this.buttonAddChunk.TabIndex = 11;
            this.buttonAddChunk.Text = "Add";
            this.buttonAddChunk.UseVisualStyleBackColor = true;
            this.buttonAddChunk.Click += new System.EventHandler(this.buttonAddChunkClick);
            // 
            // NumChunkNum
            // 
            this.NumChunkNum.Location = new System.Drawing.Point(97, 132);
            this.NumChunkNum.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.NumChunkNum.Name = "NumChunkNum";
            this.NumChunkNum.Size = new System.Drawing.Size(129, 20);
            this.NumChunkNum.TabIndex = 13;
            this.NumChunkNum.ValueChanged += new System.EventHandler(this.NumChunkNum_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 134);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Chunk Number:";
            // 
            // labelLoadedONE
            // 
            this.labelLoadedONE.AutoSize = true;
            this.labelLoadedONE.Location = new System.Drawing.Point(12, 408);
            this.labelLoadedONE.Name = "labelLoadedONE";
            this.labelLoadedONE.Size = new System.Drawing.Size(82, 13);
            this.labelLoadedONE.TabIndex = 2;
            this.labelLoadedONE.Text = "No ONE loaded";
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.oNEFileToolStripMenuItem,
            this.bLKFileToolStripMenuItem,
            this.shadowLevelToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(462, 24);
            this.menuStrip1.TabIndex = 16;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // oNEFileToolStripMenuItem
            // 
            this.oNEFileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem});
            this.oNEFileToolStripMenuItem.Name = "oNEFileToolStripMenuItem";
            this.oNEFileToolStripMenuItem.Size = new System.Drawing.Size(64, 20);
            this.oNEFileToolStripMenuItem.Text = "ONE File";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.newToolStripMenuItem.Text = "New";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Enabled = false;
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Enabled = false;
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.saveAsToolStripMenuItem.Text = "Save As...";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
            // 
            // bLKFileToolStripMenuItem
            // 
            this.bLKFileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem1,
            this.openToolStripMenuItem1,
            this.saveToolStripMenuItem1,
            this.saveAsToolStripMenuItem1});
            this.bLKFileToolStripMenuItem.Name = "bLKFileToolStripMenuItem";
            this.bLKFileToolStripMenuItem.Size = new System.Drawing.Size(60, 20);
            this.bLKFileToolStripMenuItem.Text = "BLK File";
            // 
            // newToolStripMenuItem1
            // 
            this.newToolStripMenuItem1.Name = "newToolStripMenuItem1";
            this.newToolStripMenuItem1.Size = new System.Drawing.Size(123, 22);
            this.newToolStripMenuItem1.Text = "New";
            this.newToolStripMenuItem1.Click += new System.EventHandler(this.newToolStripMenuItem1_Click);
            // 
            // openToolStripMenuItem1
            // 
            this.openToolStripMenuItem1.Name = "openToolStripMenuItem1";
            this.openToolStripMenuItem1.Size = new System.Drawing.Size(123, 22);
            this.openToolStripMenuItem1.Text = "Open";
            this.openToolStripMenuItem1.Click += new System.EventHandler(this.openToolStripMenuItem1_Click);
            // 
            // saveToolStripMenuItem1
            // 
            this.saveToolStripMenuItem1.Name = "saveToolStripMenuItem1";
            this.saveToolStripMenuItem1.Size = new System.Drawing.Size(123, 22);
            this.saveToolStripMenuItem1.Text = "Save";
            this.saveToolStripMenuItem1.Click += new System.EventHandler(this.saveToolStripMenuItem1_Click);
            // 
            // saveAsToolStripMenuItem1
            // 
            this.saveAsToolStripMenuItem1.Name = "saveAsToolStripMenuItem1";
            this.saveAsToolStripMenuItem1.Size = new System.Drawing.Size(123, 22);
            this.saveAsToolStripMenuItem1.Text = "Save As...";
            this.saveAsToolStripMenuItem1.Click += new System.EventHandler(this.saveAsToolStripMenuItem1_Click);
            // 
            // shadowLevelToolStripMenuItem
            // 
            this.shadowLevelToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem2,
            this.openToolStripMenuItem2,
            this.saveToolStripMenuItem2,
            this.saveAsToolStripMenuItem2,
            this.toolStripSeparator1,
            this.collisionEditorToolStripMenuItem});
            this.shadowLevelToolStripMenuItem.Name = "shadowLevelToolStripMenuItem";
            this.shadowLevelToolStripMenuItem.Size = new System.Drawing.Size(91, 20);
            this.shadowLevelToolStripMenuItem.Text = "Shadow Level";
            // 
            // newToolStripMenuItem2
            // 
            this.newToolStripMenuItem2.Name = "newToolStripMenuItem2";
            this.newToolStripMenuItem2.Size = new System.Drawing.Size(180, 22);
            this.newToolStripMenuItem2.Text = "New";
            this.newToolStripMenuItem2.Click += new System.EventHandler(this.newToolStripMenuItem2_Click);
            // 
            // openToolStripMenuItem2
            // 
            this.openToolStripMenuItem2.Name = "openToolStripMenuItem2";
            this.openToolStripMenuItem2.Size = new System.Drawing.Size(180, 22);
            this.openToolStripMenuItem2.Text = "Open";
            this.openToolStripMenuItem2.Click += new System.EventHandler(this.openToolStripMenuItem2_Click);
            // 
            // saveToolStripMenuItem2
            // 
            this.saveToolStripMenuItem2.Enabled = false;
            this.saveToolStripMenuItem2.Name = "saveToolStripMenuItem2";
            this.saveToolStripMenuItem2.Size = new System.Drawing.Size(180, 22);
            this.saveToolStripMenuItem2.Text = "Save";
            this.saveToolStripMenuItem2.Click += new System.EventHandler(this.saveToolStripMenuItem2_Click);
            // 
            // saveAsToolStripMenuItem2
            // 
            this.saveAsToolStripMenuItem2.Enabled = false;
            this.saveAsToolStripMenuItem2.Name = "saveAsToolStripMenuItem2";
            this.saveAsToolStripMenuItem2.Size = new System.Drawing.Size(180, 22);
            this.saveAsToolStripMenuItem2.Text = "Save As...";
            this.saveAsToolStripMenuItem2.Click += new System.EventHandler(this.saveAsToolStripMenuItem2_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(177, 6);
            // 
            // collisionEditorToolStripMenuItem
            // 
            this.collisionEditorToolStripMenuItem.Enabled = false;
            this.collisionEditorToolStripMenuItem.Name = "collisionEditorToolStripMenuItem";
            this.collisionEditorToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.collisionEditorToolStripMenuItem.Text = "Collision Editor";
            this.collisionEditorToolStripMenuItem.Click += new System.EventHandler(this.collisionEditorToolStripMenuItem_Click);
            // 
            // labelLoadedBLK
            // 
            this.labelLoadedBLK.AutoSize = true;
            this.labelLoadedBLK.Location = new System.Drawing.Point(12, 427);
            this.labelLoadedBLK.Name = "labelLoadedBLK";
            this.labelLoadedBLK.Size = new System.Drawing.Size(79, 13);
            this.labelLoadedBLK.TabIndex = 2;
            this.labelLoadedBLK.Text = "No BLK loaded";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(12, 382);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(443, 23);
            this.progressBar1.TabIndex = 23;
            // 
            // LevelEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(462, 448);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.labelLoadedBLK);
            this.Controls.Add(this.labelLoadedONE);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "LevelEditor";
            this.ShowIcon = false;
            this.Text = "Level Editor";
            this.Load += new System.EventHandler(this.LevelEditor_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericCurrentChunk)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAdd)).EndInit();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.NumMinZ)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumMinY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumMinX)).EndInit();
            this.groupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.NumMaxZ)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumMaxY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumMaxX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumChunkNum)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListBox listBoxLevelModels;
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
        private System.Windows.Forms.ToolStripMenuItem shadowLevelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem collisionEditorToolStripMenuItem;
    }
}