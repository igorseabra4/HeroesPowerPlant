namespace HeroesPowerPlant.LevelEditor
{
    partial class ShadowCollisionEditor
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
            this.labelTriangleAmount = new System.Windows.Forms.Label();
            this.labelVertexAmount = new System.Windows.Forms.Label();
            this.buttonImport = new System.Windows.Forms.Button();
            this.buttonRemove = new System.Windows.Forms.Button();
            this.buttonClear = new System.Windows.Forms.Button();
            this.buttonExport = new System.Windows.Forms.Button();
            this.listBoxLevelModels = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // labelTriangleAmount
            // 
            this.labelTriangleAmount.AutoSize = true;
            this.labelTriangleAmount.Location = new System.Drawing.Point(14, 283);
            this.labelTriangleAmount.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelTriangleAmount.Name = "labelTriangleAmount";
            this.labelTriangleAmount.Size = new System.Drawing.Size(56, 15);
            this.labelTriangleAmount.TabIndex = 75;
            this.labelTriangleAmount.Text = "Triangles:";
            // 
            // labelVertexAmount
            // 
            this.labelVertexAmount.AutoSize = true;
            this.labelVertexAmount.Location = new System.Drawing.Point(14, 262);
            this.labelVertexAmount.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelVertexAmount.Name = "labelVertexAmount";
            this.labelVertexAmount.Size = new System.Drawing.Size(50, 15);
            this.labelVertexAmount.TabIndex = 74;
            this.labelVertexAmount.Text = "Vertices:";
            // 
            // buttonImport
            // 
            this.buttonImport.Location = new System.Drawing.Point(14, 301);
            this.buttonImport.Margin = new System.Windows.Forms.Padding(4);
            this.buttonImport.Name = "buttonImport";
            this.buttonImport.Size = new System.Drawing.Size(109, 26);
            this.buttonImport.TabIndex = 70;
            this.buttonImport.Text = "Import";
            this.buttonImport.UseVisualStyleBackColor = true;
            this.buttonImport.Click += new System.EventHandler(this.buttonImport_Click);
            // 
            // buttonRemove
            // 
            this.buttonRemove.Location = new System.Drawing.Point(14, 335);
            this.buttonRemove.Margin = new System.Windows.Forms.Padding(4);
            this.buttonRemove.Name = "buttonRemove";
            this.buttonRemove.Size = new System.Drawing.Size(109, 26);
            this.buttonRemove.TabIndex = 71;
            this.buttonRemove.Text = "Remove";
            this.buttonRemove.UseVisualStyleBackColor = true;
            this.buttonRemove.Click += new System.EventHandler(this.buttonRemove_Click);
            // 
            // buttonClear
            // 
            this.buttonClear.Location = new System.Drawing.Point(130, 335);
            this.buttonClear.Margin = new System.Windows.Forms.Padding(4);
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.Size = new System.Drawing.Size(109, 26);
            this.buttonClear.TabIndex = 72;
            this.buttonClear.Text = "Clear";
            this.buttonClear.UseVisualStyleBackColor = true;
            this.buttonClear.Click += new System.EventHandler(this.buttonClear_Click);
            // 
            // buttonExport
            // 
            this.buttonExport.Location = new System.Drawing.Point(130, 301);
            this.buttonExport.Margin = new System.Windows.Forms.Padding(4);
            this.buttonExport.Name = "buttonExport";
            this.buttonExport.Size = new System.Drawing.Size(109, 26);
            this.buttonExport.TabIndex = 73;
            this.buttonExport.Text = "Export";
            this.buttonExport.UseVisualStyleBackColor = true;
            this.buttonExport.Click += new System.EventHandler(this.buttonExport_Click);
            // 
            // listBoxLevelModels
            // 
            this.listBoxLevelModels.FormattingEnabled = true;
            this.listBoxLevelModels.ItemHeight = 15;
            this.listBoxLevelModels.Location = new System.Drawing.Point(14, 14);
            this.listBoxLevelModels.Margin = new System.Windows.Forms.Padding(4);
            this.listBoxLevelModels.Name = "listBoxLevelModels";
            this.listBoxLevelModels.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.listBoxLevelModels.Size = new System.Drawing.Size(225, 244);
            this.listBoxLevelModels.TabIndex = 69;
            this.listBoxLevelModels.SelectedIndexChanged += new System.EventHandler(this.listBoxLevelModels_SelectedIndexChanged);
            this.listBoxLevelModels.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listBoxLevelModels_KeyDown);
            this.listBoxLevelModels.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listBoxLevelModels_MouseDoubleClick);
            // 
            // ShadowCollisionEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(251, 374);
            this.Controls.Add(this.labelTriangleAmount);
            this.Controls.Add(this.labelVertexAmount);
            this.Controls.Add(this.buttonImport);
            this.Controls.Add(this.buttonRemove);
            this.Controls.Add(this.buttonClear);
            this.Controls.Add(this.buttonExport);
            this.Controls.Add(this.listBoxLevelModels);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "ShadowCollisionEditor";
            this.ShowIcon = false;
            this.Text = "Shadow Collision Editor";
            this.Load += new System.EventHandler(this.ShadowCollisionEditor_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelTriangleAmount;
        private System.Windows.Forms.Label labelVertexAmount;
        private System.Windows.Forms.Button buttonImport;
        private System.Windows.Forms.Button buttonRemove;
        private System.Windows.Forms.Button buttonClear;
        public System.Windows.Forms.Button buttonExport;
        private System.Windows.Forms.ListBox listBoxLevelModels;
    }
}