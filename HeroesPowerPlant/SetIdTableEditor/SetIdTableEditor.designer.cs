namespace HeroesPowerPlant.SetIdTableEditor
{
    partial class SetIdTableEditor
    {
        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.heroesSetidtblbinToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.shadowSetidbinToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.comboBoxTableEntries = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.checkedListBoxStageEntries = new System.Windows.Forms.CheckedListBox();
            this.comboBoxAutoLevel = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonPerformAutoLevel = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(379, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.heroesSetidtblbinToolStripMenuItem,
            this.shadowSetidbinToolStripMenuItem});
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.openToolStripMenuItem.Text = "Open";
            // 
            // heroesSetidtblbinToolStripMenuItem
            // 
            this.heroesSetidtblbinToolStripMenuItem.Name = "heroesSetidtblbinToolStripMenuItem";
            this.heroesSetidtblbinToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.heroesSetidtblbinToolStripMenuItem.Text = "Heroes setidtbl.bin";
            this.heroesSetidtblbinToolStripMenuItem.Click += new System.EventHandler(this.heroesSetidtblbinToolStripMenuItem_Click);
            // 
            // shadowSetidbinToolStripMenuItem
            // 
            this.shadowSetidbinToolStripMenuItem.Name = "shadowSetidbinToolStripMenuItem";
            this.shadowSetidbinToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.shadowSetidbinToolStripMenuItem.Text = "Shadow setid.bin";
            this.shadowSetidbinToolStripMenuItem.Click += new System.EventHandler(this.shadowSetidbinToolStripMenuItem_Click);
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
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 509);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(379, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 17);
            // 
            // comboBoxTableEntries
            // 
            this.comboBoxTableEntries.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxTableEntries.FormattingEnabled = true;
            this.comboBoxTableEntries.Location = new System.Drawing.Point(59, 27);
            this.comboBoxTableEntries.Name = "comboBoxTableEntries";
            this.comboBoxTableEntries.Size = new System.Drawing.Size(308, 21);
            this.comboBoxTableEntries.TabIndex = 2;
            this.comboBoxTableEntries.SelectedIndexChanged += new System.EventHandler(this.comboBoxTableEntries_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Object:";
            // 
            // checkedListBoxStageEntries
            // 
            this.checkedListBoxStageEntries.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.checkedListBoxStageEntries.FormattingEnabled = true;
            this.checkedListBoxStageEntries.Location = new System.Drawing.Point(12, 54);
            this.checkedListBoxStageEntries.Name = "checkedListBoxStageEntries";
            this.checkedListBoxStageEntries.Size = new System.Drawing.Size(355, 394);
            this.checkedListBoxStageEntries.TabIndex = 4;
            this.checkedListBoxStageEntries.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.checkedListBoxStageEntries_ItemCheck);
            // 
            // comboBoxAutoLevel
            // 
            this.comboBoxAutoLevel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.comboBoxAutoLevel.FormattingEnabled = true;
            this.comboBoxAutoLevel.Location = new System.Drawing.Point(6, 19);
            this.comboBoxAutoLevel.Name = "comboBoxAutoLevel";
            this.comboBoxAutoLevel.Size = new System.Drawing.Size(232, 21);
            this.comboBoxAutoLevel.TabIndex = 5;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.buttonPerformAutoLevel);
            this.groupBox1.Controls.Add(this.comboBoxAutoLevel);
            this.groupBox1.Location = new System.Drawing.Point(12, 454);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(355, 49);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "AutoLevel";
            // 
            // buttonPerformAutoLevel
            // 
            this.buttonPerformAutoLevel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonPerformAutoLevel.Location = new System.Drawing.Point(244, 17);
            this.buttonPerformAutoLevel.Name = "buttonPerformAutoLevel";
            this.buttonPerformAutoLevel.Size = new System.Drawing.Size(105, 23);
            this.buttonPerformAutoLevel.TabIndex = 6;
            this.buttonPerformAutoLevel.Text = "Perform";
            this.buttonPerformAutoLevel.UseVisualStyleBackColor = true;
            this.buttonPerformAutoLevel.Click += new System.EventHandler(this.buttonPerformAutoLevel_Click);
            // 
            // SetIdTableEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(379, 531);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.checkedListBoxStageEntries);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBoxTableEntries);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "SetIdTableEditor";
            this.ShowIcon = false;
            this.Text = "SET ID Table Editor";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ComboBox comboBoxTableEntries;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckedListBox checkedListBoxStageEntries;
        private System.Windows.Forms.ToolStripMenuItem heroesSetidtblbinToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem shadowSetidbinToolStripMenuItem;
        private System.Windows.Forms.ComboBox comboBoxAutoLevel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button buttonPerformAutoLevel;
    }
}

