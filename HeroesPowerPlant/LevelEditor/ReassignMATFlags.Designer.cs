
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
            button_ReplaceFlags = new System.Windows.Forms.Button();
            textBox_targetMAT = new System.Windows.Forms.TextBox();
            textBox_replacementMAT = new System.Windows.Forms.TextBox();
            labelShadowSupportedMatFlags = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            buttonWikiForGeoMatFlags = new System.Windows.Forms.Button();
            SuspendLayout();
            // 
            // button_ReplaceFlags
            // 
            button_ReplaceFlags.Location = new System.Drawing.Point(13, 70);
            button_ReplaceFlags.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            button_ReplaceFlags.Name = "button_ReplaceFlags";
            button_ReplaceFlags.Size = new System.Drawing.Size(181, 38);
            button_ReplaceFlags.TabIndex = 4;
            button_ReplaceFlags.Text = "Replace Flags";
            button_ReplaceFlags.UseVisualStyleBackColor = true;
            button_ReplaceFlags.Click += button_ReplaceFlags_Click;
            // 
            // textBox_targetMAT
            // 
            textBox_targetMAT.Location = new System.Drawing.Point(13, 12);
            textBox_targetMAT.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            textBox_targetMAT.Name = "textBox_targetMAT";
            textBox_targetMAT.Size = new System.Drawing.Size(181, 23);
            textBox_targetMAT.TabIndex = 3;
            textBox_targetMAT.Text = "target";
            textBox_targetMAT.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox_replacementMAT
            // 
            textBox_replacementMAT.Location = new System.Drawing.Point(13, 41);
            textBox_replacementMAT.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            textBox_replacementMAT.Name = "textBox_replacementMAT";
            textBox_replacementMAT.Size = new System.Drawing.Size(181, 23);
            textBox_replacementMAT.TabIndex = 6;
            textBox_replacementMAT.Text = "replacement";
            textBox_replacementMAT.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // labelShadowSupportedMatFlags
            // 
            labelShadowSupportedMatFlags.AutoSize = true;
            labelShadowSupportedMatFlags.Location = new System.Drawing.Point(343, 33);
            labelShadowSupportedMatFlags.Name = "labelShadowSupportedMatFlags";
            labelShadowSupportedMatFlags.Size = new System.Drawing.Size(28, 225);
            labelShadowSupportedMatFlags.TabIndex = 7;
            labelShadowSupportedMatFlags.Text = "N\nD\nOS\nOSL\nO\nOW\nOL\nP\nAF\nA\nAL\nK\nM\nG\nGL";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(333, 9);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(49, 15);
            label1.TabIndex = 8;
            label1.Text = "Shadow";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(225, 9);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(44, 15);
            label2.TabIndex = 10;
            label2.Text = "Heroes";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(227, 33);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(42, 360);
            label3.TabIndex = 9;
            label3.Text = "DN\nO\nOS\nON\nONS\nONW\nONWS\nP\nPS\nPN\nPNS\nPNW\nPNWS\nDA\nAF\nAFS\nAFN\nAFNS\nK\nKW\nA\nAS\nAN\nANS";
            // 
            // buttonWikiForGeoMatFlags
            // 
            buttonWikiForGeoMatFlags.Location = new System.Drawing.Point(13, 114);
            buttonWikiForGeoMatFlags.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            buttonWikiForGeoMatFlags.Name = "buttonWikiForGeoMatFlags";
            buttonWikiForGeoMatFlags.Size = new System.Drawing.Size(181, 47);
            buttonWikiForGeoMatFlags.TabIndex = 11;
            buttonWikiForGeoMatFlags.Text = "Wiki for Detailed Breakdown\n of Mat/Geo Flags";
            buttonWikiForGeoMatFlags.UseVisualStyleBackColor = true;
            buttonWikiForGeoMatFlags.Click += buttonWikiForGeoMatFlags_Click;
            // 
            // ReassignMATFlags
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(420, 402);
            Controls.Add(buttonWikiForGeoMatFlags);
            Controls.Add(label2);
            Controls.Add(label3);
            Controls.Add(label1);
            Controls.Add(labelShadowSupportedMatFlags);
            Controls.Add(textBox_replacementMAT);
            Controls.Add(button_ReplaceFlags);
            Controls.Add(textBox_targetMAT);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "ReassignMATFlags";
            ShowIcon = false;
            ShowInTaskbar = false;
            Text = "Reassign Material Flags";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private System.Windows.Forms.Button button_ReplaceFlags;
        private System.Windows.Forms.TextBox textBox_targetMAT;
        private System.Windows.Forms.TextBox textBox_replacementMAT;
        private System.Windows.Forms.Label labelShadowSupportedMatFlags;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button buttonWikiForGeoMatFlags;
    }
}