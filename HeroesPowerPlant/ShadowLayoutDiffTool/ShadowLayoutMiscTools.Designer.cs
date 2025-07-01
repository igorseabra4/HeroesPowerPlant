namespace HeroesPowerPlant.ShadowLayoutMiscTools
{
    partial class ShadowLayoutMiscTools
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
            buttonDiff = new System.Windows.Forms.Button();
            LabelInstructions = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            groupBox1 = new System.Windows.Forms.GroupBox();
            groupBox2 = new System.Windows.Forms.GroupBox();
            ComboBoxObject = new System.Windows.Forms.ComboBox();
            label5 = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            label4 = new System.Windows.Forms.Label();
            buttonFindObjectInFiles = new System.Windows.Forms.Button();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            SuspendLayout();
            // 
            // buttonDiff
            // 
            buttonDiff.Location = new System.Drawing.Point(7, 67);
            buttonDiff.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            buttonDiff.Name = "buttonDiff";
            buttonDiff.Size = new System.Drawing.Size(219, 27);
            buttonDiff.TabIndex = 21;
            buttonDiff.Text = "Diff";
            buttonDiff.UseVisualStyleBackColor = true;
            buttonDiff.Click += buttonDiff_Click;
            // 
            // LabelInstructions
            // 
            LabelInstructions.AutoSize = true;
            LabelInstructions.Location = new System.Drawing.Point(7, 19);
            LabelInstructions.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            LabelInstructions.Name = "LabelInstructions";
            LabelInstructions.Size = new System.Drawing.Size(162, 15);
            LabelInstructions.TabIndex = 69;
            LabelInstructions.Text = "1. Choose layout file previous";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(7, 34);
            label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(221, 15);
            label1.TabIndex = 70;
            label1.Text = "2. Choose layout file containing changes";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(7, 49);
            label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(219, 15);
            label2.TabIndex = 71;
            label2.Text = "3. Choose output directory for diff result";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(LabelInstructions);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(buttonDiff);
            groupBox1.Controls.Add(label1);
            groupBox1.Location = new System.Drawing.Point(12, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new System.Drawing.Size(231, 99);
            groupBox1.TabIndex = 72;
            groupBox1.TabStop = false;
            groupBox1.Text = "Shadow Layout Diff";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(ComboBoxObject);
            groupBox2.Controls.Add(label5);
            groupBox2.Controls.Add(label3);
            groupBox2.Controls.Add(label4);
            groupBox2.Controls.Add(buttonFindObjectInFiles);
            groupBox2.Location = new System.Drawing.Point(12, 117);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new System.Drawing.Size(342, 131);
            groupBox2.TabIndex = 73;
            groupBox2.TabStop = false;
            groupBox2.Text = "Shadow Object Usage Check";
            // 
            // ComboBoxObject
            // 
            ComboBoxObject.FormattingEnabled = true;
            ComboBoxObject.Location = new System.Drawing.Point(8, 69);
            ComboBoxObject.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            ComboBoxObject.Name = "ComboBoxObject";
            ComboBoxObject.Size = new System.Drawing.Size(327, 23);
            ComboBoxObject.TabIndex = 74;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new System.Drawing.Point(49, 51);
            label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(207, 15);
            label5.TabIndex = 73;
            label5.Text = "3. List of files containing obj will show";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(49, 21);
            label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(131, 15);
            label3.TabIndex = 69;
            label3.Text = "1. Pick Object ID to find";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new System.Drawing.Point(49, 36);
            label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(179, 15);
            label4.TabIndex = 71;
            label4.Text = "2. Choose \"files\" extracted folder";
            // 
            // buttonFindObjectInFiles
            // 
            buttonFindObjectInFiles.Location = new System.Drawing.Point(7, 98);
            buttonFindObjectInFiles.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            buttonFindObjectInFiles.Name = "buttonFindObjectInFiles";
            buttonFindObjectInFiles.Size = new System.Drawing.Size(328, 27);
            buttonFindObjectInFiles.TabIndex = 21;
            buttonFindObjectInFiles.Text = "Find";
            buttonFindObjectInFiles.UseVisualStyleBackColor = true;
            buttonFindObjectInFiles.Click += buttonFindObjectInFiles_Click;
            // 
            // ShadowLayoutMiscTools
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(353, 262);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            MaximizeBox = false;
            Name = "ShadowLayoutMiscTools";
            ShowIcon = false;
            Text = "Shadow Layout Misc Tools";
            Load += ShadowLayoutMiscTools_Load;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonDiff;
        internal System.Windows.Forms.Label LabelInstructions;
        internal System.Windows.Forms.Label label1;
        internal System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        internal System.Windows.Forms.Label label3;
        internal System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button buttonFindObjectInFiles;
        internal System.Windows.Forms.Label label5;
        public System.Windows.Forms.ComboBox ComboBoxObject;
    }
}