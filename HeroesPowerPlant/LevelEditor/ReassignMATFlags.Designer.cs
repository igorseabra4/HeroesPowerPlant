
namespace HeroesPowerPlant.LevelEditor
{
    partial class ReassignMATFlags
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
            this.button_ReplaceFlags = new System.Windows.Forms.Button();
            this.textBox_targetMAT = new System.Windows.Forms.TextBox();
            this.textBox_replacementMAT = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // button_ReplaceFlags
            // 
            this.button_ReplaceFlags.Location = new System.Drawing.Point(74, 83);
            this.button_ReplaceFlags.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.button_ReplaceFlags.Name = "button_ReplaceFlags";
            this.button_ReplaceFlags.Size = new System.Drawing.Size(88, 27);
            this.button_ReplaceFlags.TabIndex = 4;
            this.button_ReplaceFlags.Text = "Replace Flags";
            this.button_ReplaceFlags.UseVisualStyleBackColor = true;
            this.button_ReplaceFlags.Click += new System.EventHandler(this.button_ReplaceFlags_Click);
            // 
            // textBox_targetMAT
            // 
            this.textBox_targetMAT.Location = new System.Drawing.Point(29, 12);
            this.textBox_targetMAT.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.textBox_targetMAT.Name = "textBox_targetMAT";
            this.textBox_targetMAT.Size = new System.Drawing.Size(181, 23);
            this.textBox_targetMAT.TabIndex = 3;
            this.textBox_targetMAT.Text = "target";
            this.textBox_targetMAT.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox_replacementMAT
            // 
            this.textBox_replacementMAT.Location = new System.Drawing.Point(29, 41);
            this.textBox_replacementMAT.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.textBox_replacementMAT.Name = "textBox_replacementMAT";
            this.textBox_replacementMAT.Size = new System.Drawing.Size(181, 23);
            this.textBox_replacementMAT.TabIndex = 6;
            this.textBox_replacementMAT.Text = "replacement";
            this.textBox_replacementMAT.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // ReassignMATFlags
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(232, 122);
            this.Controls.Add(this.textBox_replacementMAT);
            this.Controls.Add(this.button_ReplaceFlags);
            this.Controls.Add(this.textBox_targetMAT);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ReassignMATFlags";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Reassign Material Flags";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button button_ReplaceFlags;
        private System.Windows.Forms.TextBox textBox_targetMAT;
        private System.Windows.Forms.TextBox textBox_replacementMAT;
    }
}