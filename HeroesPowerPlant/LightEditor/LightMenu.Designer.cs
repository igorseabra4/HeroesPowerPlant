namespace HeroesPowerPlant.LightEditor
{
    partial class LightMenu
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
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.numericCurrentLight = new System.Windows.Forms.NumericUpDown();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.buttonCopy = new System.Windows.Forms.Button();
            this.buttonRemove = new System.Windows.Forms.Button();
            this.propertyGridLights = new System.Windows.Forms.PropertyGrid();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.openShadowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsShadowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericCurrentLight)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(321, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem,
            this.openShadowToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.saveAsShadowToolStripMenuItem});
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
            this.openToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.openToolStripMenuItem.Text = "Open (Heroes)";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
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
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.saveAsToolStripMenuItem.Text = "Save As (Heroes)";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
            // 
            // numericCurrentLight
            // 
            this.numericCurrentLight.Location = new System.Drawing.Point(12, 27);
            this.numericCurrentLight.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.numericCurrentLight.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.numericCurrentLight.Name = "numericCurrentLight";
            this.numericCurrentLight.Size = new System.Drawing.Size(96, 20);
            this.numericCurrentLight.TabIndex = 1;
            this.numericCurrentLight.Value = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.numericCurrentLight.ValueChanged += new System.EventHandler(this.numericCurrentLight_ValueChanged);
            // 
            // buttonAdd
            // 
            this.buttonAdd.Location = new System.Drawing.Point(114, 27);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(64, 23);
            this.buttonAdd.TabIndex = 2;
            this.buttonAdd.Text = "Add";
            this.buttonAdd.UseVisualStyleBackColor = true;
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // buttonCopy
            // 
            this.buttonCopy.Location = new System.Drawing.Point(184, 27);
            this.buttonCopy.Name = "buttonCopy";
            this.buttonCopy.Size = new System.Drawing.Size(54, 23);
            this.buttonCopy.TabIndex = 3;
            this.buttonCopy.Text = "Copy";
            this.buttonCopy.UseVisualStyleBackColor = true;
            this.buttonCopy.Click += new System.EventHandler(this.buttonCopy_Click);
            // 
            // buttonRemove
            // 
            this.buttonRemove.Location = new System.Drawing.Point(244, 27);
            this.buttonRemove.Name = "buttonRemove";
            this.buttonRemove.Size = new System.Drawing.Size(64, 23);
            this.buttonRemove.TabIndex = 4;
            this.buttonRemove.Text = "Remove";
            this.buttonRemove.UseVisualStyleBackColor = true;
            this.buttonRemove.Click += new System.EventHandler(this.buttonRemove_Click);
            // 
            // propertyGridLights
            // 
            this.propertyGridLights.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.propertyGridLights.HelpVisible = false;
            this.propertyGridLights.Location = new System.Drawing.Point(12, 56);
            this.propertyGridLights.Name = "propertyGridLights";
            this.propertyGridLights.PropertySort = System.Windows.Forms.PropertySort.NoSort;
            this.propertyGridLights.Size = new System.Drawing.Size(296, 275);
            this.propertyGridLights.TabIndex = 5;
            this.propertyGridLights.ToolbarVisible = false;
            this.propertyGridLights.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.propertyGridLights_PropertyValueChanged);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 334);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(321, 22);
            this.statusStrip1.TabIndex = 6;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(81, 17);
            this.toolStripStatusLabel1.Text = "No file loaded";
            // 
            // openShadowToolStripMenuItem
            // 
            this.openShadowToolStripMenuItem.Name = "openShadowToolStripMenuItem";
            this.openShadowToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.openShadowToolStripMenuItem.Text = "Open (Shadow)";
            this.openShadowToolStripMenuItem.Click += new System.EventHandler(this.openShadowToolStripMenuItem_Click);
            // 
            // saveAsShadowToolStripMenuItem
            // 
            this.saveAsShadowToolStripMenuItem.Name = "saveAsShadowToolStripMenuItem";
            this.saveAsShadowToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.saveAsShadowToolStripMenuItem.Text = "Save As (Shadow)";
            this.saveAsShadowToolStripMenuItem.Click += new System.EventHandler(this.saveAsShadowToolStripMenuItem_Click);
            // 
            // LightMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(321, 356);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.propertyGridLights);
            this.Controls.Add(this.buttonRemove);
            this.Controls.Add(this.buttonCopy);
            this.Controls.Add(this.buttonAdd);
            this.Controls.Add(this.numericCurrentLight);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "LightMenu";
            this.ShowIcon = false;
            this.Text = "Light Editor";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.LightEditor_FormClosing);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericCurrentLight)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.NumericUpDown numericCurrentLight;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.Button buttonCopy;
        private System.Windows.Forms.Button buttonRemove;
        private System.Windows.Forms.PropertyGrid propertyGridLights;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripMenuItem openShadowToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsShadowToolStripMenuItem;
    }
}