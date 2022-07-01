namespace HeroesPowerPlant.ShadowLayoutDiffTool
{
    partial class ShadowLayoutDiffTool
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
            this.buttonDiff = new System.Windows.Forms.Button();
            this.LabelInstructions = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // buttonDiff
            // 
            this.buttonDiff.Location = new System.Drawing.Point(56, 67);
            this.buttonDiff.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.buttonDiff.Name = "buttonDiff";
            this.buttonDiff.Size = new System.Drawing.Size(172, 27);
            this.buttonDiff.TabIndex = 21;
            this.buttonDiff.Text = "Diff";
            this.buttonDiff.UseVisualStyleBackColor = true;
            this.buttonDiff.Click += new System.EventHandler(this.buttonDiff_Click);
            // 
            // LabelInstructions
            // 
            this.LabelInstructions.AutoSize = true;
            this.LabelInstructions.Location = new System.Drawing.Point(23, 9);
            this.LabelInstructions.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LabelInstructions.Name = "LabelInstructions";
            this.LabelInstructions.Size = new System.Drawing.Size(228, 15);
            this.LabelInstructions.TabIndex = 69;
            this.LabelInstructions.Text = "1. choose file containing unmodified level";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 24);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(183, 15);
            this.label1.TabIndex = 70;
            this.label1.Text = "2. choose file containing changes";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(23, 39);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(217, 15);
            this.label2.TabIndex = 71;
            this.label2.Text = "3. choose output directory for diff result";
            // 
            // ShadowLayoutDiffTool
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 101);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.LabelInstructions);
            this.Controls.Add(this.buttonDiff);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(300, 140);
            this.Name = "ShadowLayoutDiffTool";
            this.ShowIcon = false;
            this.Text = "Shadow Layout Diff Tool";
            this.Load += new System.EventHandler(this.ShadowLayoutDiffTool_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonDiff;
        internal System.Windows.Forms.Label LabelInstructions;
        internal System.Windows.Forms.Label label1;
        internal System.Windows.Forms.Label label2;
    }
}