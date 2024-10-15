namespace HeroesPowerPlant.ShadowTexturePatternEditor
{
    partial class ShadowTexturePatternEditor
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
            menuStrip1 = new System.Windows.Forms.MenuStrip();
            fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            buttonAdd = new System.Windows.Forms.Button();
            buttonCopy = new System.Windows.Forms.Button();
            buttonRemove = new System.Windows.Forms.Button();
            statusStrip1 = new System.Windows.Forms.StatusStrip();
            toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            listBoxPatterns = new System.Windows.Forms.ListBox();
            groupBox1 = new System.Windows.Forms.GroupBox();
            groupBox2 = new System.Windows.Forms.GroupBox();
            textBoxTextureName = new System.Windows.Forms.TextBox();
            groupBox3 = new System.Windows.Forms.GroupBox();
            textBoxAnimationName = new System.Windows.Forms.TextBox();
            groupBox4 = new System.Windows.Forms.GroupBox();
            numericFrameCount = new System.Windows.Forms.NumericUpDown();
            groupBox5 = new System.Windows.Forms.GroupBox();
            labelFrame = new System.Windows.Forms.Label();
            buttonPlay = new System.Windows.Forms.Button();
            groupBox7 = new System.Windows.Forms.GroupBox();
            numericTextureNumber = new System.Windows.Forms.NumericUpDown();
            groupBox6 = new System.Windows.Forms.GroupBox();
            numericFrameOffset = new System.Windows.Forms.NumericUpDown();
            listBoxFrames = new System.Windows.Forms.ListBox();
            buttonAddFrame = new System.Windows.Forms.Button();
            button3 = new System.Windows.Forms.Button();
            menuStrip1.SuspendLayout();
            statusStrip1.SuspendLayout();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox3.SuspendLayout();
            groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericFrameCount).BeginInit();
            groupBox5.SuspendLayout();
            groupBox7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericTextureNumber).BeginInit();
            groupBox6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericFrameOffset).BeginInit();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { fileToolStripMenuItem });
            menuStrip1.Location = new System.Drawing.Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Padding = new System.Windows.Forms.Padding(7, 2, 0, 2);
            menuStrip1.Size = new System.Drawing.Size(464, 24);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { newToolStripMenuItem, openToolStripMenuItem, saveToolStripMenuItem, saveAsToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            fileToolStripMenuItem.Text = "File";
            // 
            // newToolStripMenuItem
            // 
            newToolStripMenuItem.Name = "newToolStripMenuItem";
            newToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
            newToolStripMenuItem.Text = "New";
            newToolStripMenuItem.Click += newToolStripMenuItem_Click;
            // 
            // openToolStripMenuItem
            // 
            openToolStripMenuItem.Name = "openToolStripMenuItem";
            openToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
            openToolStripMenuItem.Text = "Open...";
            openToolStripMenuItem.Click += openToolStripMenuItem_Click;
            // 
            // saveToolStripMenuItem
            // 
            saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            saveToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
            saveToolStripMenuItem.Text = "Save";
            saveToolStripMenuItem.Click += saveToolStripMenuItem_Click;
            // 
            // saveAsToolStripMenuItem
            // 
            saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            saveAsToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
            saveAsToolStripMenuItem.Text = "Save As...";
            saveAsToolStripMenuItem.Click += saveAsToolStripMenuItem_Click;
            // 
            // buttonAdd
            // 
            buttonAdd.Location = new System.Drawing.Point(7, 348);
            buttonAdd.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            buttonAdd.Name = "buttonAdd";
            buttonAdd.Size = new System.Drawing.Size(48, 27);
            buttonAdd.TabIndex = 2;
            buttonAdd.Text = "Add";
            buttonAdd.UseVisualStyleBackColor = true;
            buttonAdd.Click += buttonAdd_Click;
            // 
            // buttonCopy
            // 
            buttonCopy.Location = new System.Drawing.Point(62, 348);
            buttonCopy.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            buttonCopy.Name = "buttonCopy";
            buttonCopy.Size = new System.Drawing.Size(50, 27);
            buttonCopy.TabIndex = 3;
            buttonCopy.Text = "Copy";
            buttonCopy.UseVisualStyleBackColor = true;
            buttonCopy.Click += buttonCopy_Click;
            // 
            // buttonRemove
            // 
            buttonRemove.Location = new System.Drawing.Point(119, 348);
            buttonRemove.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            buttonRemove.Name = "buttonRemove";
            buttonRemove.Size = new System.Drawing.Size(75, 27);
            buttonRemove.TabIndex = 4;
            buttonRemove.Text = "Remove";
            buttonRemove.UseVisualStyleBackColor = true;
            buttonRemove.Click += buttonRemove_Click;
            // 
            // statusStrip1
            // 
            statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { toolStripStatusLabel1 });
            statusStrip1.Location = new System.Drawing.Point(0, 421);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 16, 0);
            statusStrip1.Size = new System.Drawing.Size(464, 22);
            statusStrip1.TabIndex = 6;
            statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            toolStripStatusLabel1.Size = new System.Drawing.Size(81, 17);
            toolStripStatusLabel1.Text = "No file loaded";
            // 
            // listBoxPatterns
            // 
            listBoxPatterns.FormattingEnabled = true;
            listBoxPatterns.ItemHeight = 15;
            listBoxPatterns.Location = new System.Drawing.Point(7, 22);
            listBoxPatterns.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            listBoxPatterns.Name = "listBoxPatterns";
            listBoxPatterns.Size = new System.Drawing.Size(186, 319);
            listBoxPatterns.TabIndex = 7;
            listBoxPatterns.SelectedIndexChanged += listBoxPatterns_SelectedIndexChanged;
            listBoxPatterns.DoubleClick += listBoxPatterns_DoubleClick;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(listBoxPatterns);
            groupBox1.Controls.Add(buttonAdd);
            groupBox1.Controls.Add(buttonCopy);
            groupBox1.Controls.Add(buttonRemove);
            groupBox1.Location = new System.Drawing.Point(14, 31);
            groupBox1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            groupBox1.Size = new System.Drawing.Size(202, 381);
            groupBox1.TabIndex = 8;
            groupBox1.TabStop = false;
            groupBox1.Text = "Texture Patterns";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(textBoxTextureName);
            groupBox2.Location = new System.Drawing.Point(223, 31);
            groupBox2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            groupBox2.Name = "groupBox2";
            groupBox2.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            groupBox2.Size = new System.Drawing.Size(233, 53);
            groupBox2.TabIndex = 9;
            groupBox2.TabStop = false;
            groupBox2.Text = "Texture Name";
            // 
            // textBoxTextureName
            // 
            textBoxTextureName.Location = new System.Drawing.Point(7, 22);
            textBoxTextureName.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            textBoxTextureName.Name = "textBoxTextureName";
            textBoxTextureName.Size = new System.Drawing.Size(219, 23);
            textBoxTextureName.TabIndex = 10;
            textBoxTextureName.TextChanged += textBoxTextureName_TextChanged;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(textBoxAnimationName);
            groupBox3.Location = new System.Drawing.Point(223, 91);
            groupBox3.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            groupBox3.Name = "groupBox3";
            groupBox3.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            groupBox3.Size = new System.Drawing.Size(233, 53);
            groupBox3.TabIndex = 11;
            groupBox3.TabStop = false;
            groupBox3.Text = "Animation Name";
            // 
            // textBoxAnimationName
            // 
            textBoxAnimationName.Location = new System.Drawing.Point(7, 22);
            textBoxAnimationName.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            textBoxAnimationName.Name = "textBoxAnimationName";
            textBoxAnimationName.Size = new System.Drawing.Size(219, 23);
            textBoxAnimationName.TabIndex = 10;
            textBoxAnimationName.TextChanged += textBoxAnimationName_TextChanged;
            // 
            // groupBox4
            // 
            groupBox4.Controls.Add(numericFrameCount);
            groupBox4.Location = new System.Drawing.Point(223, 151);
            groupBox4.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            groupBox4.Name = "groupBox4";
            groupBox4.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            groupBox4.Size = new System.Drawing.Size(233, 53);
            groupBox4.TabIndex = 12;
            groupBox4.TabStop = false;
            groupBox4.Text = "Frame Count";
            // 
            // numericFrameCount
            // 
            numericFrameCount.Location = new System.Drawing.Point(7, 22);
            numericFrameCount.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            numericFrameCount.Name = "numericFrameCount";
            numericFrameCount.Size = new System.Drawing.Size(219, 23);
            numericFrameCount.TabIndex = 13;
            numericFrameCount.ValueChanged += numericFrameCount_ValueChanged;
            // 
            // groupBox5
            // 
            groupBox5.Controls.Add(labelFrame);
            groupBox5.Controls.Add(buttonPlay);
            groupBox5.Controls.Add(groupBox7);
            groupBox5.Controls.Add(groupBox6);
            groupBox5.Controls.Add(listBoxFrames);
            groupBox5.Controls.Add(buttonAddFrame);
            groupBox5.Controls.Add(button3);
            groupBox5.Location = new System.Drawing.Point(223, 211);
            groupBox5.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            groupBox5.Name = "groupBox5";
            groupBox5.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            groupBox5.Size = new System.Drawing.Size(233, 201);
            groupBox5.TabIndex = 9;
            groupBox5.TabStop = false;
            groupBox5.Text = "Keyframes";
            // 
            // labelFrame
            // 
            labelFrame.AutoSize = true;
            labelFrame.Location = new System.Drawing.Point(120, 147);
            labelFrame.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            labelFrame.Name = "labelFrame";
            labelFrame.Size = new System.Drawing.Size(51, 15);
            labelFrame.TabIndex = 17;
            labelFrame.Text = "Stopped";
            // 
            // buttonPlay
            // 
            buttonPlay.Location = new System.Drawing.Point(120, 168);
            buttonPlay.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            buttonPlay.Name = "buttonPlay";
            buttonPlay.Size = new System.Drawing.Size(106, 27);
            buttonPlay.TabIndex = 16;
            buttonPlay.Text = "Play";
            buttonPlay.UseVisualStyleBackColor = true;
            buttonPlay.Click += buttonPlay_Click;
            // 
            // groupBox7
            // 
            groupBox7.Controls.Add(numericTextureNumber);
            groupBox7.Location = new System.Drawing.Point(120, 83);
            groupBox7.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            groupBox7.Name = "groupBox7";
            groupBox7.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            groupBox7.Size = new System.Drawing.Size(106, 54);
            groupBox7.TabIndex = 15;
            groupBox7.TabStop = false;
            groupBox7.Text = "Texture Num";
            // 
            // numericTextureNumber
            // 
            numericTextureNumber.Location = new System.Drawing.Point(7, 22);
            numericTextureNumber.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            numericTextureNumber.Name = "numericTextureNumber";
            numericTextureNumber.Size = new System.Drawing.Size(92, 23);
            numericTextureNumber.TabIndex = 14;
            numericTextureNumber.ValueChanged += numericTextureNumber_ValueChanged;
            // 
            // groupBox6
            // 
            groupBox6.Controls.Add(numericFrameOffset);
            groupBox6.Location = new System.Drawing.Point(120, 22);
            groupBox6.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            groupBox6.Name = "groupBox6";
            groupBox6.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            groupBox6.Size = new System.Drawing.Size(106, 54);
            groupBox6.TabIndex = 11;
            groupBox6.TabStop = false;
            groupBox6.Text = "Frame Offset";
            // 
            // numericFrameOffset
            // 
            numericFrameOffset.Location = new System.Drawing.Point(7, 22);
            numericFrameOffset.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            numericFrameOffset.Name = "numericFrameOffset";
            numericFrameOffset.Size = new System.Drawing.Size(92, 23);
            numericFrameOffset.TabIndex = 14;
            numericFrameOffset.ValueChanged += numericFrameOffset_ValueChanged;
            // 
            // listBoxFrames
            // 
            listBoxFrames.FormattingEnabled = true;
            listBoxFrames.ItemHeight = 15;
            listBoxFrames.Location = new System.Drawing.Point(7, 22);
            listBoxFrames.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            listBoxFrames.Name = "listBoxFrames";
            listBoxFrames.Size = new System.Drawing.Size(106, 139);
            listBoxFrames.TabIndex = 7;
            listBoxFrames.SelectedIndexChanged += listBoxFrames_SelectedIndexChanged;
            // 
            // buttonAddFrame
            // 
            buttonAddFrame.Location = new System.Drawing.Point(7, 168);
            buttonAddFrame.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            buttonAddFrame.Name = "buttonAddFrame";
            buttonAddFrame.Size = new System.Drawing.Size(42, 27);
            buttonAddFrame.TabIndex = 2;
            buttonAddFrame.Text = "Add";
            buttonAddFrame.UseVisualStyleBackColor = true;
            buttonAddFrame.Click += buttonAddFrame_Click;
            // 
            // button3
            // 
            button3.Location = new System.Drawing.Point(49, 168);
            button3.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            button3.Name = "button3";
            button3.Size = new System.Drawing.Size(64, 27);
            button3.TabIndex = 4;
            button3.Text = "Remove";
            button3.UseVisualStyleBackColor = true;
            button3.Click += buttonRemoveFrame_Click;
            // 
            // ShadowTexturePatternEditor
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(464, 443);
            Controls.Add(groupBox5);
            Controls.Add(groupBox4);
            Controls.Add(groupBox3);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Controls.Add(statusStrip1);
            Controls.Add(menuStrip1);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            MainMenuStrip = menuStrip1;
            Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            MaximizeBox = false;
            Name = "ShadowTexturePatternEditor";
            ShowIcon = false;
            Text = "Shadow Texture Pattern Editor";
            FormClosing += ShadowTexturePatternEditor_FormClosing;
            Load += ShadowTexturePatternEditor_Load;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            groupBox1.ResumeLayout(false);
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            groupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)numericFrameCount).EndInit();
            groupBox5.ResumeLayout(false);
            groupBox5.PerformLayout();
            groupBox7.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)numericTextureNumber).EndInit();
            groupBox6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)numericFrameOffset).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.Button buttonCopy;
        private System.Windows.Forms.Button buttonRemove;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ListBox listBoxPatterns;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox textBoxTextureName;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox textBoxAnimationName;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.NumericUpDown numericFrameCount;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.ListBox listBoxFrames;
        private System.Windows.Forms.Button buttonAddFrame;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.NumericUpDown numericTextureNumber;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.NumericUpDown numericFrameOffset;
        private System.Windows.Forms.Button buttonPlay;
        private System.Windows.Forms.Label labelFrame;
    }
}