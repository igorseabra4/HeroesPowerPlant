namespace HeroesPowerPlant.ConfigEditor
{
    public partial class EXEExtractor
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
            this.comboBoxStages = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonStartPos = new System.Windows.Forms.Button();
            this.buttonSplines = new System.Windows.Forms.Button();
            this.buttonOpenEXE = new System.Windows.Forms.Button();
            this.buttonScores = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // comboBoxStages
            // 
            this.comboBoxStages.FormattingEnabled = true;
            this.comboBoxStages.Location = new System.Drawing.Point(6, 19);
            this.comboBoxStages.Name = "comboBoxStages";
            this.comboBoxStages.Size = new System.Drawing.Size(250, 21);
            this.comboBoxStages.TabIndex = 66;
            this.comboBoxStages.SelectedIndexChanged += new System.EventHandler(this.comboBoxStages_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.comboBoxStages);
            this.groupBox1.Enabled = false;
            this.groupBox1.Location = new System.Drawing.Point(12, 41);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(262, 47);
            this.groupBox1.TabIndex = 67;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Stage";
            // 
            // buttonStartPos
            // 
            this.buttonStartPos.Enabled = false;
            this.buttonStartPos.Location = new System.Drawing.Point(12, 94);
            this.buttonStartPos.Name = "buttonStartPos";
            this.buttonStartPos.Size = new System.Drawing.Size(262, 23);
            this.buttonStartPos.TabIndex = 68;
            this.buttonStartPos.Text = "Send Positions to Config Editor";
            this.buttonStartPos.UseVisualStyleBackColor = true;
            this.buttonStartPos.Click += new System.EventHandler(this.buttonStartPos_Click);
            // 
            // buttonSplines
            // 
            this.buttonSplines.Enabled = false;
            this.buttonSplines.Location = new System.Drawing.Point(12, 123);
            this.buttonSplines.Name = "buttonSplines";
            this.buttonSplines.Size = new System.Drawing.Size(262, 23);
            this.buttonSplines.TabIndex = 69;
            this.buttonSplines.Text = "Send Splines to Spline Editor";
            this.buttonSplines.UseVisualStyleBackColor = true;
            this.buttonSplines.Click += new System.EventHandler(this.buttonSplines_Click);
            // 
            // buttonOpenEXE
            // 
            this.buttonOpenEXE.Location = new System.Drawing.Point(12, 12);
            this.buttonOpenEXE.Name = "buttonOpenEXE";
            this.buttonOpenEXE.Size = new System.Drawing.Size(262, 23);
            this.buttonOpenEXE.TabIndex = 70;
            this.buttonOpenEXE.Text = "Open Tsonic_win.exe";
            this.buttonOpenEXE.UseVisualStyleBackColor = true;
            this.buttonOpenEXE.Click += new System.EventHandler(this.buttonOpenExe_Click);
            // 
            // buttonScores
            // 
            this.buttonScores.Enabled = false;
            this.buttonScores.Location = new System.Drawing.Point(12, 152);
            this.buttonScores.Name = "buttonScores";
            this.buttonScores.Size = new System.Drawing.Size(262, 23);
            this.buttonScores.TabIndex = 71;
            this.buttonScores.Text = "Send Scores to Rank Editor";
            this.buttonScores.UseVisualStyleBackColor = true;
            this.buttonScores.Click += new System.EventHandler(this.buttonScores_Click);
            // 
            // EXEExtractor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(286, 183);
            this.Controls.Add(this.buttonScores);
            this.Controls.Add(this.buttonOpenEXE);
            this.Controls.Add(this.buttonSplines);
            this.Controls.Add(this.buttonStartPos);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "EXEExtractor";
            this.ShowIcon = false;
            this.Text = "EXE Data Extractor";
            this.Load += new System.EventHandler(this.LayoutEditor_Load);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxStages;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button buttonStartPos;
        private System.Windows.Forms.Button buttonSplines;
        private System.Windows.Forms.Button buttonOpenEXE;
        private System.Windows.Forms.Button buttonScores;
    }
}