namespace HeroesPowerPlant.CollisionEditor
{
    partial class CollisionEditor
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportOBJToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.labelQuadnodes = new System.Windows.Forms.Label();
            this.labelTriangles = new System.Windows.Forms.Label();
            this.labelVertexNum = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.numericDepthLevel = new System.Windows.Forms.NumericUpDown();
            this.buttonImport = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.labelFileLoaded = new System.Windows.Forms.ToolStripStatusLabel();
            this.numericUpDownBasePower = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.checkBoxFlipNormals = new System.Windows.Forms.CheckBox();
            this.buttonNote = new System.Windows.Forms.Button();
            this.checkedListBox1 = new System.Windows.Forms.CheckedListBox();
            this.button1 = new System.Windows.Forms.Button();
            this.progressBarColEditor = new System.Windows.Forms.ProgressBar();
            this.buttonForceReload = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericDepthLevel)).BeginInit();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownBasePower)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(343, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem,
            this.exportOBJToolStripMenuItem,
            this.closeToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
            this.newToolStripMenuItem.Text = "New";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // exportOBJToolStripMenuItem
            // 
            this.exportOBJToolStripMenuItem.Enabled = false;
            this.exportOBJToolStripMenuItem.Name = "exportOBJToolStripMenuItem";
            this.exportOBJToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
            this.exportOBJToolStripMenuItem.Text = "Export OBJ";
            this.exportOBJToolStripMenuItem.Click += new System.EventHandler(this.exportOBJToolStripMenuItem_Click);
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
            this.closeToolStripMenuItem.Text = "Close";
            this.closeToolStripMenuItem.Click += new System.EventHandler(this.closeToolStripMenuItem_Click);
            // 
            // labelQuadnodes
            // 
            this.labelQuadnodes.AutoSize = true;
            this.labelQuadnodes.Location = new System.Drawing.Point(12, 57);
            this.labelQuadnodes.Name = "labelQuadnodes";
            this.labelQuadnodes.Size = new System.Drawing.Size(117, 13);
            this.labelQuadnodes.TabIndex = 12;
            this.labelQuadnodes.Text = "Number of Quadnodes:";
            // 
            // labelTriangles
            // 
            this.labelTriangles.AutoSize = true;
            this.labelTriangles.Location = new System.Drawing.Point(12, 44);
            this.labelTriangles.Name = "labelTriangles";
            this.labelTriangles.Size = new System.Drawing.Size(105, 13);
            this.labelTriangles.TabIndex = 13;
            this.labelTriangles.Text = "Number of Triangles:";
            // 
            // labelVertexNum
            // 
            this.labelVertexNum.AutoSize = true;
            this.labelVertexNum.Location = new System.Drawing.Point(12, 31);
            this.labelVertexNum.Name = "labelVertexNum";
            this.labelVertexNum.Size = new System.Drawing.Size(100, 13);
            this.labelVertexNum.TabIndex = 14;
            this.labelVertexNum.Text = "Number of Vertices:";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(283, 31);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(48, 17);
            this.checkBox1.TabIndex = 11;
            this.checkBox1.Text = "Auto";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(151, 31);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Depth Level:";
            // 
            // numericDepthLevel
            // 
            this.numericDepthLevel.Location = new System.Drawing.Point(225, 29);
            this.numericDepthLevel.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericDepthLevel.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericDepthLevel.Name = "numericDepthLevel";
            this.numericDepthLevel.Size = new System.Drawing.Size(52, 20);
            this.numericDepthLevel.TabIndex = 9;
            this.numericDepthLevel.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // buttonImport
            // 
            this.buttonImport.Location = new System.Drawing.Point(186, 104);
            this.buttonImport.Name = "buttonImport";
            this.buttonImport.Size = new System.Drawing.Size(145, 23);
            this.buttonImport.TabIndex = 8;
            this.buttonImport.Text = "Import OBJ";
            this.buttonImport.UseVisualStyleBackColor = true;
            this.buttonImport.Click += new System.EventHandler(this.buttonImport_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.labelFileLoaded});
            this.statusStrip1.Location = new System.Drawing.Point(0, 162);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(343, 22);
            this.statusStrip1.TabIndex = 15;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // labelFileLoaded
            // 
            this.labelFileLoaded.Name = "labelFileLoaded";
            this.labelFileLoaded.Size = new System.Drawing.Size(79, 17);
            this.labelFileLoaded.Text = "No CL loaded";
            // 
            // numericUpDownBasePower
            // 
            this.numericUpDownBasePower.Hexadecimal = true;
            this.numericUpDownBasePower.Location = new System.Drawing.Point(225, 55);
            this.numericUpDownBasePower.Maximum = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.numericUpDownBasePower.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownBasePower.Name = "numericUpDownBasePower";
            this.numericUpDownBasePower.Size = new System.Drawing.Size(52, 20);
            this.numericUpDownBasePower.TabIndex = 16;
            this.numericUpDownBasePower.Value = new decimal(new int[] {
            13,
            0,
            0,
            0});
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(156, 57);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(63, 13);
            this.label6.TabIndex = 17;
            this.label6.Text = "Power Flag:";
            // 
            // checkBoxFlipNormals
            // 
            this.checkBoxFlipNormals.AutoSize = true;
            this.checkBoxFlipNormals.Location = new System.Drawing.Point(194, 81);
            this.checkBoxFlipNormals.Name = "checkBoxFlipNormals";
            this.checkBoxFlipNormals.Size = new System.Drawing.Size(83, 17);
            this.checkBoxFlipNormals.TabIndex = 18;
            this.checkBoxFlipNormals.Text = "Flip Normals";
            this.checkBoxFlipNormals.UseVisualStyleBackColor = true;
            // 
            // buttonNote
            // 
            this.buttonNote.Location = new System.Drawing.Point(283, 53);
            this.buttonNote.Name = "buttonNote";
            this.buttonNote.Size = new System.Drawing.Size(48, 23);
            this.buttonNote.TabIndex = 19;
            this.buttonNote.Text = "Note";
            this.buttonNote.UseVisualStyleBackColor = true;
            this.buttonNote.Click += new System.EventHandler(this.buttonNote_Click);
            // 
            // checkedListBox1
            // 
            this.checkedListBox1.FormattingEnabled = true;
            this.checkedListBox1.Location = new System.Drawing.Point(12, 162);
            this.checkedListBox1.Name = "checkedListBox1";
            this.checkedListBox1.Size = new System.Drawing.Size(319, 214);
            this.checkedListBox1.TabIndex = 20;
            this.checkedListBox1.Visible = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(256, 382);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 21;
            this.button1.Text = "Send";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // progressBarColEditor
            // 
            this.progressBarColEditor.Location = new System.Drawing.Point(12, 133);
            this.progressBarColEditor.Name = "progressBarColEditor";
            this.progressBarColEditor.Size = new System.Drawing.Size(319, 23);
            this.progressBarColEditor.TabIndex = 22;
            // 
            // buttonForceReload
            // 
            this.buttonForceReload.Enabled = false;
            this.buttonForceReload.Location = new System.Drawing.Point(12, 104);
            this.buttonForceReload.Name = "buttonForceReload";
            this.buttonForceReload.Size = new System.Drawing.Size(168, 23);
            this.buttonForceReload.TabIndex = 23;
            this.buttonForceReload.Text = "Force Reload";
            this.buttonForceReload.UseVisualStyleBackColor = true;
            // 
            // CollisionEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(343, 184);
            this.Controls.Add(this.buttonForceReload);
            this.Controls.Add(this.progressBarColEditor);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.checkedListBox1);
            this.Controls.Add(this.buttonNote);
            this.Controls.Add(this.checkBoxFlipNormals);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.numericUpDownBasePower);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.labelQuadnodes);
            this.Controls.Add(this.labelTriangles);
            this.Controls.Add(this.labelVertexNum);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.numericDepthLevel);
            this.Controls.Add(this.buttonImport);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "CollisionEditor";
            this.ShowIcon = false;
            this.Text = "Collision Editor";
            this.Load += new System.EventHandler(this.CollisionEditor_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericDepthLevel)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownBasePower)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        public System.Windows.Forms.Label labelQuadnodes;
        public System.Windows.Forms.Label labelTriangles;
        public System.Windows.Forms.Label labelVertexNum;
        public System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.NumericUpDown numericDepthLevel;
        private System.Windows.Forms.Button buttonImport;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel labelFileLoaded;
        public System.Windows.Forms.NumericUpDown numericUpDownBasePower;
        private System.Windows.Forms.Label label6;
        public System.Windows.Forms.CheckBox checkBoxFlipNormals;
        private System.Windows.Forms.ToolStripMenuItem exportOBJToolStripMenuItem;
        private System.Windows.Forms.Button buttonNote;
        public System.Windows.Forms.CheckedListBox checkedListBox1;
        private System.Windows.Forms.Button button1;
        public System.Windows.Forms.ProgressBar progressBarColEditor;
        internal System.Windows.Forms.Button buttonForceReload;
    }
}