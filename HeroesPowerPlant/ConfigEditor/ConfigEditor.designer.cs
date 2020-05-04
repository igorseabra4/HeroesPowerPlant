namespace HeroesPowerPlant.ConfigEditor
{
    partial class ConfigEditor
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
            this.ComboBoxTeam = new System.Windows.Forms.ComboBox();
            this.LabelObject = new System.Windows.Forms.Label();
            this.GroupBox2 = new System.Windows.Forms.GroupBox();
            this.NumericStartRot = new System.Windows.Forms.NumericUpDown();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.NumericStartZ = new System.Windows.Forms.NumericUpDown();
            this.NumericStartY = new System.Windows.Forms.NumericUpDown();
            this.NumericStartX = new System.Windows.Forms.NumericUpDown();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.LabelFileLoaded = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splineEditorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rankEditorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.eXEExtractorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.NumericStartHold = new System.Windows.Forms.NumericUpDown();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.ComboStartMode = new System.Windows.Forms.ComboBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.NumericEndZ = new System.Windows.Forms.NumericUpDown();
            this.NumericEndY = new System.Windows.Forms.NumericUpDown();
            this.NumericEndX = new System.Windows.Forms.NumericUpDown();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.NumericEndRot = new System.Windows.Forms.NumericUpDown();
            this.groupBoxStart = new System.Windows.Forms.GroupBox();
            this.buttonCurrentViewDrop = new System.Windows.Forms.Button();
            this.buttonDrop = new System.Windows.Forms.Button();
            this.buttonViewHere = new System.Windows.Forms.Button();
            this.groupBoxEnd = new System.Windows.Forms.GroupBox();
            this.buttonCurrentViewDropEnding = new System.Windows.Forms.Button();
            this.buttonViewHereEnding = new System.Windows.Forms.Button();
            this.buttonDropEnding = new System.Windows.Forms.Button();
            this.groupBoxBrag = new System.Windows.Forms.GroupBox();
            this.buttonCurrentViewDropBrag = new System.Windows.Forms.Button();
            this.groupBox10 = new System.Windows.Forms.GroupBox();
            this.NumericBragZ = new System.Windows.Forms.NumericUpDown();
            this.NumericBragY = new System.Windows.Forms.NumericUpDown();
            this.NumericBragX = new System.Windows.Forms.NumericUpDown();
            this.buttonViewHereBrag = new System.Windows.Forms.Button();
            this.buttonDropBrag = new System.Windows.Forms.Button();
            this.groupBox11 = new System.Windows.Forms.GroupBox();
            this.NumericBragRot = new System.Windows.Forms.NumericUpDown();
            this.ComboLevelConfig = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.GroupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NumericStartRot)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NumericStartZ)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumericStartY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumericStartX)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NumericStartHold)).BeginInit();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NumericEndZ)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumericEndY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumericEndX)).BeginInit();
            this.groupBox6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NumericEndRot)).BeginInit();
            this.groupBoxStart.SuspendLayout();
            this.groupBoxEnd.SuspendLayout();
            this.groupBoxBrag.SuspendLayout();
            this.groupBox10.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NumericBragZ)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumericBragY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumericBragX)).BeginInit();
            this.groupBox11.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NumericBragRot)).BeginInit();
            this.SuspendLayout();
            // 
            // ComboBoxTeam
            // 
            this.ComboBoxTeam.FormattingEnabled = true;
            this.ComboBoxTeam.Items.AddRange(new object[] {
            "Team Sonic / Player 1",
            "Team Dark / Player 2",
            "Team Rose",
            "Team Chaotix",
            "Team Foredit"});
            this.ComboBoxTeam.Location = new System.Drawing.Point(12, 40);
            this.ComboBoxTeam.Name = "ComboBoxTeam";
            this.ComboBoxTeam.Size = new System.Drawing.Size(173, 21);
            this.ComboBoxTeam.TabIndex = 44;
            this.ComboBoxTeam.SelectedIndexChanged += new System.EventHandler(this.ComboBoxTeam_SelectedIndexChanged);
            // 
            // LabelObject
            // 
            this.LabelObject.AutoSize = true;
            this.LabelObject.Location = new System.Drawing.Point(12, 24);
            this.LabelObject.Name = "LabelObject";
            this.LabelObject.Size = new System.Drawing.Size(37, 13);
            this.LabelObject.TabIndex = 40;
            this.LabelObject.Text = "Team:";
            // 
            // GroupBox2
            // 
            this.GroupBox2.Controls.Add(this.NumericStartRot);
            this.GroupBox2.Location = new System.Drawing.Point(6, 72);
            this.GroupBox2.Name = "GroupBox2";
            this.GroupBox2.Size = new System.Drawing.Size(134, 47);
            this.GroupBox2.TabIndex = 39;
            this.GroupBox2.TabStop = false;
            this.GroupBox2.Text = "Rotation";
            // 
            // NumericStartRot
            // 
            this.NumericStartRot.DecimalPlaces = 4;
            this.NumericStartRot.Location = new System.Drawing.Point(6, 19);
            this.NumericStartRot.Maximum = new decimal(new int[] {
            360,
            0,
            0,
            0});
            this.NumericStartRot.Name = "NumericStartRot";
            this.NumericStartRot.Size = new System.Drawing.Size(119, 20);
            this.NumericStartRot.TabIndex = 28;
            this.NumericStartRot.ValueChanged += new System.EventHandler(this.NumericStart_ValueChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.NumericStartZ);
            this.groupBox3.Controls.Add(this.NumericStartY);
            this.groupBox3.Controls.Add(this.NumericStartX);
            this.groupBox3.Location = new System.Drawing.Point(6, 19);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(385, 47);
            this.groupBox3.TabIndex = 38;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Position (X, Y, Z)";
            // 
            // NumericStartZ
            // 
            this.NumericStartZ.DecimalPlaces = 4;
            this.NumericStartZ.Location = new System.Drawing.Point(256, 19);
            this.NumericStartZ.Maximum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.NumericStartZ.Name = "NumericStartZ";
            this.NumericStartZ.Size = new System.Drawing.Size(119, 20);
            this.NumericStartZ.TabIndex = 26;
            this.NumericStartZ.ValueChanged += new System.EventHandler(this.NumericStart_ValueChanged);
            // 
            // NumericStartY
            // 
            this.NumericStartY.DecimalPlaces = 4;
            this.NumericStartY.Location = new System.Drawing.Point(131, 19);
            this.NumericStartY.Maximum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.NumericStartY.Name = "NumericStartY";
            this.NumericStartY.Size = new System.Drawing.Size(119, 20);
            this.NumericStartY.TabIndex = 25;
            this.NumericStartY.ValueChanged += new System.EventHandler(this.NumericStart_ValueChanged);
            // 
            // NumericStartX
            // 
            this.NumericStartX.DecimalPlaces = 4;
            this.NumericStartX.Location = new System.Drawing.Point(6, 19);
            this.NumericStartX.Maximum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.NumericStartX.Name = "NumericStartX";
            this.NumericStartX.Size = new System.Drawing.Size(119, 20);
            this.NumericStartX.TabIndex = 24;
            this.NumericStartX.ValueChanged += new System.EventHandler(this.NumericStart_ValueChanged);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.LabelFileLoaded});
            this.statusStrip1.Location = new System.Drawing.Point(0, 457);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(520, 22);
            this.statusStrip1.TabIndex = 56;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // LabelFileLoaded
            // 
            this.LabelFileLoaded.Name = "LabelFileLoaded";
            this.LabelFileLoaded.Size = new System.Drawing.Size(81, 17);
            this.LabelFileLoaded.Text = "No file loaded";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.splineEditorToolStripMenuItem,
            this.rankEditorToolStripMenuItem,
            this.eXEExtractorToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(520, 24);
            this.menuStrip1.TabIndex = 59;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
            this.newToolStripMenuItem.Text = "New";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
            this.saveAsToolStripMenuItem.Text = "Save As...";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
            // 
            // splineEditorToolStripMenuItem
            // 
            this.splineEditorToolStripMenuItem.Enabled = false;
            this.splineEditorToolStripMenuItem.Name = "splineEditorToolStripMenuItem";
            this.splineEditorToolStripMenuItem.Size = new System.Drawing.Size(85, 20);
            this.splineEditorToolStripMenuItem.Text = "Spline Editor";
            this.splineEditorToolStripMenuItem.ToolTipText = "To enable the Spline Editor, please save or open an existing config file.";
            this.splineEditorToolStripMenuItem.Click += new System.EventHandler(this.splineEditorToolStripMenuItem_Click);
            // 
            // rankEditorToolStripMenuItem
            // 
            this.rankEditorToolStripMenuItem.Enabled = false;
            this.rankEditorToolStripMenuItem.Name = "rankEditorToolStripMenuItem";
            this.rankEditorToolStripMenuItem.Size = new System.Drawing.Size(79, 20);
            this.rankEditorToolStripMenuItem.Text = "Rank Editor";
            this.rankEditorToolStripMenuItem.Click += new System.EventHandler(this.rankEditorToolStripMenuItem_Click);
            // 
            // eXEExtractorToolStripMenuItem
            // 
            this.eXEExtractorToolStripMenuItem.Enabled = false;
            this.eXEExtractorToolStripMenuItem.Name = "eXEExtractorToolStripMenuItem";
            this.eXEExtractorToolStripMenuItem.Size = new System.Drawing.Size(88, 20);
            this.eXEExtractorToolStripMenuItem.Text = "EXE Extractor";
            this.eXEExtractorToolStripMenuItem.Click += new System.EventHandler(this.eXEExtractorToolStripMenuItem_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.NumericStartHold);
            this.groupBox1.Location = new System.Drawing.Point(257, 72);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(134, 47);
            this.groupBox1.TabIndex = 40;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Hold Time";
            // 
            // NumericStartHold
            // 
            this.NumericStartHold.DecimalPlaces = 4;
            this.NumericStartHold.Location = new System.Drawing.Point(6, 19);
            this.NumericStartHold.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.NumericStartHold.Minimum = new decimal(new int[] {
            65535,
            0,
            0,
            -2147483648});
            this.NumericStartHold.Name = "NumericStartHold";
            this.NumericStartHold.Size = new System.Drawing.Size(119, 20);
            this.NumericStartHold.TabIndex = 28;
            this.NumericStartHold.ValueChanged += new System.EventHandler(this.NumericStartHold_ValueChanged);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.ComboStartMode);
            this.groupBox4.Location = new System.Drawing.Point(146, 72);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(105, 47);
            this.groupBox4.TabIndex = 41;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Mode";
            // 
            // ComboStartMode
            // 
            this.ComboStartMode.FormattingEnabled = true;
            this.ComboStartMode.Items.AddRange(new object[] {
            "Normal",
            "Running",
            "Rail"});
            this.ComboStartMode.Location = new System.Drawing.Point(6, 18);
            this.ComboStartMode.Name = "ComboStartMode";
            this.ComboStartMode.Size = new System.Drawing.Size(93, 21);
            this.ComboStartMode.TabIndex = 62;
            this.ComboStartMode.SelectedIndexChanged += new System.EventHandler(this.ComboStartMode_SelectedIndexChanged);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.NumericEndZ);
            this.groupBox5.Controls.Add(this.NumericEndY);
            this.groupBox5.Controls.Add(this.NumericEndX);
            this.groupBox5.Location = new System.Drawing.Point(6, 18);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(385, 47);
            this.groupBox5.TabIndex = 38;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Position (X, Y, Z)";
            // 
            // NumericEndZ
            // 
            this.NumericEndZ.DecimalPlaces = 4;
            this.NumericEndZ.Location = new System.Drawing.Point(256, 19);
            this.NumericEndZ.Maximum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.NumericEndZ.Name = "NumericEndZ";
            this.NumericEndZ.Size = new System.Drawing.Size(119, 20);
            this.NumericEndZ.TabIndex = 26;
            this.NumericEndZ.ValueChanged += new System.EventHandler(this.NumericEnd_ValueChanged);
            // 
            // NumericEndY
            // 
            this.NumericEndY.DecimalPlaces = 4;
            this.NumericEndY.Location = new System.Drawing.Point(131, 19);
            this.NumericEndY.Maximum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.NumericEndY.Name = "NumericEndY";
            this.NumericEndY.Size = new System.Drawing.Size(119, 20);
            this.NumericEndY.TabIndex = 25;
            this.NumericEndY.ValueChanged += new System.EventHandler(this.NumericEnd_ValueChanged);
            // 
            // NumericEndX
            // 
            this.NumericEndX.DecimalPlaces = 4;
            this.NumericEndX.Location = new System.Drawing.Point(6, 19);
            this.NumericEndX.Maximum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.NumericEndX.Name = "NumericEndX";
            this.NumericEndX.Size = new System.Drawing.Size(119, 20);
            this.NumericEndX.TabIndex = 24;
            this.NumericEndX.ValueChanged += new System.EventHandler(this.NumericEnd_ValueChanged);
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.NumericEndRot);
            this.groupBox6.Location = new System.Drawing.Point(6, 71);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(134, 47);
            this.groupBox6.TabIndex = 39;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Rotation";
            // 
            // NumericEndRot
            // 
            this.NumericEndRot.DecimalPlaces = 4;
            this.NumericEndRot.Location = new System.Drawing.Point(6, 19);
            this.NumericEndRot.Maximum = new decimal(new int[] {
            360,
            0,
            0,
            0});
            this.NumericEndRot.Name = "NumericEndRot";
            this.NumericEndRot.Size = new System.Drawing.Size(119, 20);
            this.NumericEndRot.TabIndex = 28;
            this.NumericEndRot.ValueChanged += new System.EventHandler(this.NumericEnd_ValueChanged);
            // 
            // groupBoxStart
            // 
            this.groupBoxStart.Controls.Add(this.buttonCurrentViewDrop);
            this.groupBoxStart.Controls.Add(this.groupBox3);
            this.groupBoxStart.Controls.Add(this.buttonDrop);
            this.groupBoxStart.Controls.Add(this.GroupBox2);
            this.groupBoxStart.Controls.Add(this.buttonViewHere);
            this.groupBoxStart.Controls.Add(this.groupBox4);
            this.groupBoxStart.Controls.Add(this.groupBox1);
            this.groupBoxStart.Location = new System.Drawing.Point(12, 67);
            this.groupBoxStart.Name = "groupBoxStart";
            this.groupBoxStart.Size = new System.Drawing.Size(503, 125);
            this.groupBoxStart.TabIndex = 63;
            this.groupBoxStart.TabStop = false;
            this.groupBoxStart.Text = "Start Position";
            // 
            // buttonCurrentViewDrop
            // 
            this.buttonCurrentViewDrop.Location = new System.Drawing.Point(397, 48);
            this.buttonCurrentViewDrop.Name = "buttonCurrentViewDrop";
            this.buttonCurrentViewDrop.Size = new System.Drawing.Size(99, 23);
            this.buttonCurrentViewDrop.TabIndex = 83;
            this.buttonCurrentViewDrop.TabStop = false;
            this.buttonCurrentViewDrop.Text = "C-Drop";
            this.buttonCurrentViewDrop.UseVisualStyleBackColor = true;
            this.buttonCurrentViewDrop.Click += new System.EventHandler(this.buttonCurrentViewDrop_Click);
            // 
            // buttonDrop
            // 
            this.buttonDrop.Location = new System.Drawing.Point(397, 77);
            this.buttonDrop.Name = "buttonDrop";
            this.buttonDrop.Size = new System.Drawing.Size(99, 23);
            this.buttonDrop.TabIndex = 82;
            this.buttonDrop.TabStop = false;
            this.buttonDrop.Text = "Drop";
            this.buttonDrop.UseVisualStyleBackColor = true;
            this.buttonDrop.Click += new System.EventHandler(this.buttonDrop_Click);
            // 
            // buttonViewHere
            // 
            this.buttonViewHere.Location = new System.Drawing.Point(397, 19);
            this.buttonViewHere.Name = "buttonViewHere";
            this.buttonViewHere.Size = new System.Drawing.Size(99, 23);
            this.buttonViewHere.TabIndex = 81;
            this.buttonViewHere.TabStop = false;
            this.buttonViewHere.Text = "View Here";
            this.buttonViewHere.UseVisualStyleBackColor = true;
            this.buttonViewHere.Click += new System.EventHandler(this.buttonViewHere_Click);
            // 
            // groupBoxEnd
            // 
            this.groupBoxEnd.Controls.Add(this.buttonCurrentViewDropEnding);
            this.groupBoxEnd.Controls.Add(this.groupBox5);
            this.groupBoxEnd.Controls.Add(this.groupBox6);
            this.groupBoxEnd.Controls.Add(this.buttonViewHereEnding);
            this.groupBoxEnd.Controls.Add(this.buttonDropEnding);
            this.groupBoxEnd.Location = new System.Drawing.Point(12, 198);
            this.groupBoxEnd.Name = "groupBoxEnd";
            this.groupBoxEnd.Size = new System.Drawing.Size(503, 125);
            this.groupBoxEnd.TabIndex = 64;
            this.groupBoxEnd.TabStop = false;
            this.groupBoxEnd.Text = "Ending Position";
            // 
            // buttonCurrentViewDropEnding
            // 
            this.buttonCurrentViewDropEnding.Location = new System.Drawing.Point(397, 48);
            this.buttonCurrentViewDropEnding.Name = "buttonCurrentViewDropEnding";
            this.buttonCurrentViewDropEnding.Size = new System.Drawing.Size(99, 23);
            this.buttonCurrentViewDropEnding.TabIndex = 86;
            this.buttonCurrentViewDropEnding.TabStop = false;
            this.buttonCurrentViewDropEnding.Text = "C-Drop";
            this.buttonCurrentViewDropEnding.UseVisualStyleBackColor = true;
            this.buttonCurrentViewDropEnding.Click += new System.EventHandler(this.buttonCurrentViewDropEnding_Click);
            // 
            // buttonViewHereEnding
            // 
            this.buttonViewHereEnding.Location = new System.Drawing.Point(397, 19);
            this.buttonViewHereEnding.Name = "buttonViewHereEnding";
            this.buttonViewHereEnding.Size = new System.Drawing.Size(99, 23);
            this.buttonViewHereEnding.TabIndex = 84;
            this.buttonViewHereEnding.TabStop = false;
            this.buttonViewHereEnding.Text = "View Here";
            this.buttonViewHereEnding.UseVisualStyleBackColor = true;
            this.buttonViewHereEnding.Click += new System.EventHandler(this.buttonViewHereEnding_Click);
            // 
            // buttonDropEnding
            // 
            this.buttonDropEnding.Location = new System.Drawing.Point(397, 77);
            this.buttonDropEnding.Name = "buttonDropEnding";
            this.buttonDropEnding.Size = new System.Drawing.Size(99, 23);
            this.buttonDropEnding.TabIndex = 85;
            this.buttonDropEnding.TabStop = false;
            this.buttonDropEnding.Text = "Drop";
            this.buttonDropEnding.UseVisualStyleBackColor = true;
            this.buttonDropEnding.Click += new System.EventHandler(this.buttonDropEnding_Click);
            // 
            // groupBoxBrag
            // 
            this.groupBoxBrag.Controls.Add(this.buttonCurrentViewDropBrag);
            this.groupBoxBrag.Controls.Add(this.groupBox10);
            this.groupBoxBrag.Controls.Add(this.buttonViewHereBrag);
            this.groupBoxBrag.Controls.Add(this.buttonDropBrag);
            this.groupBoxBrag.Controls.Add(this.groupBox11);
            this.groupBoxBrag.Location = new System.Drawing.Point(12, 329);
            this.groupBoxBrag.Name = "groupBoxBrag";
            this.groupBoxBrag.Size = new System.Drawing.Size(503, 125);
            this.groupBoxBrag.TabIndex = 64;
            this.groupBoxBrag.TabStop = false;
            this.groupBoxBrag.Text = "Bragging Position";
            // 
            // buttonCurrentViewDropBrag
            // 
            this.buttonCurrentViewDropBrag.Location = new System.Drawing.Point(397, 48);
            this.buttonCurrentViewDropBrag.Name = "buttonCurrentViewDropBrag";
            this.buttonCurrentViewDropBrag.Size = new System.Drawing.Size(99, 23);
            this.buttonCurrentViewDropBrag.TabIndex = 89;
            this.buttonCurrentViewDropBrag.TabStop = false;
            this.buttonCurrentViewDropBrag.Text = "C-Drop";
            this.buttonCurrentViewDropBrag.UseVisualStyleBackColor = true;
            this.buttonCurrentViewDropBrag.Click += new System.EventHandler(this.buttonCurrentViewDropBrag_Click);
            // 
            // groupBox10
            // 
            this.groupBox10.Controls.Add(this.NumericBragZ);
            this.groupBox10.Controls.Add(this.NumericBragY);
            this.groupBox10.Controls.Add(this.NumericBragX);
            this.groupBox10.Location = new System.Drawing.Point(6, 19);
            this.groupBox10.Name = "groupBox10";
            this.groupBox10.Size = new System.Drawing.Size(385, 47);
            this.groupBox10.TabIndex = 38;
            this.groupBox10.TabStop = false;
            this.groupBox10.Text = "Position (X, Y, Z)";
            // 
            // NumericBragZ
            // 
            this.NumericBragZ.DecimalPlaces = 4;
            this.NumericBragZ.Location = new System.Drawing.Point(256, 19);
            this.NumericBragZ.Maximum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.NumericBragZ.Name = "NumericBragZ";
            this.NumericBragZ.Size = new System.Drawing.Size(119, 20);
            this.NumericBragZ.TabIndex = 26;
            this.NumericBragZ.ValueChanged += new System.EventHandler(this.NumericBrag_ValueChanged);
            // 
            // NumericBragY
            // 
            this.NumericBragY.DecimalPlaces = 4;
            this.NumericBragY.Location = new System.Drawing.Point(131, 19);
            this.NumericBragY.Maximum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.NumericBragY.Name = "NumericBragY";
            this.NumericBragY.Size = new System.Drawing.Size(119, 20);
            this.NumericBragY.TabIndex = 25;
            this.NumericBragY.ValueChanged += new System.EventHandler(this.NumericBrag_ValueChanged);
            // 
            // NumericBragX
            // 
            this.NumericBragX.DecimalPlaces = 4;
            this.NumericBragX.Location = new System.Drawing.Point(6, 19);
            this.NumericBragX.Maximum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.NumericBragX.Name = "NumericBragX";
            this.NumericBragX.Size = new System.Drawing.Size(119, 20);
            this.NumericBragX.TabIndex = 24;
            this.NumericBragX.ValueChanged += new System.EventHandler(this.NumericBrag_ValueChanged);
            // 
            // buttonViewHereBrag
            // 
            this.buttonViewHereBrag.Location = new System.Drawing.Point(397, 19);
            this.buttonViewHereBrag.Name = "buttonViewHereBrag";
            this.buttonViewHereBrag.Size = new System.Drawing.Size(99, 23);
            this.buttonViewHereBrag.TabIndex = 87;
            this.buttonViewHereBrag.TabStop = false;
            this.buttonViewHereBrag.Text = "View Here";
            this.buttonViewHereBrag.UseVisualStyleBackColor = true;
            this.buttonViewHereBrag.Click += new System.EventHandler(this.buttonViewHereBrag_Click);
            // 
            // buttonDropBrag
            // 
            this.buttonDropBrag.Location = new System.Drawing.Point(397, 77);
            this.buttonDropBrag.Name = "buttonDropBrag";
            this.buttonDropBrag.Size = new System.Drawing.Size(99, 23);
            this.buttonDropBrag.TabIndex = 88;
            this.buttonDropBrag.TabStop = false;
            this.buttonDropBrag.Text = "Drop";
            this.buttonDropBrag.UseVisualStyleBackColor = true;
            this.buttonDropBrag.Click += new System.EventHandler(this.buttonDropBrag_Click);
            // 
            // groupBox11
            // 
            this.groupBox11.Controls.Add(this.NumericBragRot);
            this.groupBox11.Location = new System.Drawing.Point(6, 72);
            this.groupBox11.Name = "groupBox11";
            this.groupBox11.Size = new System.Drawing.Size(134, 47);
            this.groupBox11.TabIndex = 39;
            this.groupBox11.TabStop = false;
            this.groupBox11.Text = "Rotation";
            // 
            // NumericBragRot
            // 
            this.NumericBragRot.DecimalPlaces = 4;
            this.NumericBragRot.Location = new System.Drawing.Point(6, 19);
            this.NumericBragRot.Maximum = new decimal(new int[] {
            360,
            0,
            0,
            0});
            this.NumericBragRot.Name = "NumericBragRot";
            this.NumericBragRot.Size = new System.Drawing.Size(119, 20);
            this.NumericBragRot.TabIndex = 28;
            this.NumericBragRot.ValueChanged += new System.EventHandler(this.NumericBrag_ValueChanged);
            // 
            // ComboLevelConfig
            // 
            this.ComboLevelConfig.FormattingEnabled = true;
            this.ComboLevelConfig.Location = new System.Drawing.Point(191, 40);
            this.ComboLevelConfig.Name = "ComboLevelConfig";
            this.ComboLevelConfig.Size = new System.Drawing.Size(317, 21);
            this.ComboLevelConfig.TabIndex = 65;
            this.ComboLevelConfig.SelectedIndexChanged += new System.EventHandler(this.ComboLevelConfig_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(188, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 13);
            this.label1.TabIndex = 40;
            this.label1.Text = "Level Flag:";
            // 
            // ConfigEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(520, 479);
            this.Controls.Add(this.ComboLevelConfig);
            this.Controls.Add(this.groupBoxBrag);
            this.Controls.Add(this.groupBoxEnd);
            this.Controls.Add(this.groupBoxStart);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.ComboBoxTeam);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.LabelObject);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "ConfigEditor";
            this.ShowIcon = false;
            this.Text = "Mod Loader Config Editor";
            this.Load += new System.EventHandler(this.LayoutEditor_Load);
            this.GroupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.NumericStartRot)).EndInit();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.NumericStartZ)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumericStartY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumericStartX)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.NumericStartHold)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.NumericEndZ)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumericEndY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumericEndX)).EndInit();
            this.groupBox6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.NumericEndRot)).EndInit();
            this.groupBoxStart.ResumeLayout(false);
            this.groupBoxEnd.ResumeLayout(false);
            this.groupBoxBrag.ResumeLayout(false);
            this.groupBox10.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.NumericBragZ)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumericBragY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumericBragX)).EndInit();
            this.groupBox11.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.NumericBragRot)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        internal System.Windows.Forms.ComboBox ComboBoxTeam;
        internal System.Windows.Forms.Label LabelObject;
        internal System.Windows.Forms.GroupBox GroupBox2;
        internal System.Windows.Forms.NumericUpDown NumericStartRot;
        internal System.Windows.Forms.GroupBox groupBox3;
        internal System.Windows.Forms.NumericUpDown NumericStartZ;
        internal System.Windows.Forms.NumericUpDown NumericStartY;
        internal System.Windows.Forms.NumericUpDown NumericStartX;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel LabelFileLoaded;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        internal System.Windows.Forms.GroupBox groupBox1;
        internal System.Windows.Forms.NumericUpDown NumericStartHold;
        internal System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.ComboBox ComboStartMode;
        internal System.Windows.Forms.GroupBox groupBox5;
        internal System.Windows.Forms.NumericUpDown NumericEndZ;
        internal System.Windows.Forms.NumericUpDown NumericEndY;
        internal System.Windows.Forms.NumericUpDown NumericEndX;
        internal System.Windows.Forms.GroupBox groupBox6;
        internal System.Windows.Forms.NumericUpDown NumericEndRot;
        private System.Windows.Forms.GroupBox groupBoxStart;
        private System.Windows.Forms.GroupBox groupBoxEnd;
        private System.Windows.Forms.GroupBox groupBoxBrag;
        internal System.Windows.Forms.GroupBox groupBox10;
        internal System.Windows.Forms.NumericUpDown NumericBragZ;
        internal System.Windows.Forms.NumericUpDown NumericBragY;
        internal System.Windows.Forms.NumericUpDown NumericBragX;
        internal System.Windows.Forms.GroupBox groupBox11;
        internal System.Windows.Forms.NumericUpDown NumericBragRot;
        private System.Windows.Forms.ComboBox ComboLevelConfig;
        internal System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonCurrentViewDrop;
        private System.Windows.Forms.Button buttonDrop;
        private System.Windows.Forms.Button buttonViewHere;
        private System.Windows.Forms.Button buttonCurrentViewDropEnding;
        private System.Windows.Forms.Button buttonViewHereEnding;
        private System.Windows.Forms.Button buttonDropEnding;
        private System.Windows.Forms.Button buttonCurrentViewDropBrag;
        private System.Windows.Forms.Button buttonViewHereBrag;
        private System.Windows.Forms.Button buttonDropBrag;
        private System.Windows.Forms.ToolStripMenuItem splineEditorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rankEditorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem eXEExtractorToolStripMenuItem;
    }
}