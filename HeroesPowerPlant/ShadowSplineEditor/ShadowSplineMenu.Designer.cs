namespace HeroesPowerPlant.ShadowSplineEditor
{
    partial class ShadowSplineMenu
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
            this.buttonAdd = new System.Windows.Forms.Button();
            this.buttonCopy = new System.Windows.Forms.Button();
            this.buttonRemove = new System.Windows.Forms.Button();
            this.propertyGridSplines = new System.Windows.Forms.PropertyGrid();
            this.buttonViewHere = new System.Windows.Forms.Button();
            this.listBoxSplines = new System.Windows.Forms.ListBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.buttonImport = new System.Windows.Forms.Button();
            this.buttonExport = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonAdd
            // 
            this.buttonAdd.Location = new System.Drawing.Point(12, 12);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(94, 21);
            this.buttonAdd.TabIndex = 2;
            this.buttonAdd.Text = "Add";
            this.buttonAdd.UseVisualStyleBackColor = true;
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // buttonCopy
            // 
            this.buttonCopy.Location = new System.Drawing.Point(212, 12);
            this.buttonCopy.Name = "buttonCopy";
            this.buttonCopy.Size = new System.Drawing.Size(94, 21);
            this.buttonCopy.TabIndex = 3;
            this.buttonCopy.Text = "Copy";
            this.buttonCopy.UseVisualStyleBackColor = true;
            this.buttonCopy.Click += new System.EventHandler(this.buttonCopy_Click);
            // 
            // buttonRemove
            // 
            this.buttonRemove.Location = new System.Drawing.Point(212, 39);
            this.buttonRemove.Name = "buttonRemove";
            this.buttonRemove.Size = new System.Drawing.Size(94, 21);
            this.buttonRemove.TabIndex = 4;
            this.buttonRemove.Text = "Remove";
            this.buttonRemove.UseVisualStyleBackColor = true;
            this.buttonRemove.Click += new System.EventHandler(this.buttonRemove_Click);
            // 
            // propertyGridSplines
            // 
            this.propertyGridSplines.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyGridSplines.HelpVisible = false;
            this.propertyGridSplines.Location = new System.Drawing.Point(3, 179);
            this.propertyGridSplines.Name = "propertyGridSplines";
            this.propertyGridSplines.PropertySort = System.Windows.Forms.PropertySort.NoSort;
            this.propertyGridSplines.Size = new System.Drawing.Size(288, 164);
            this.propertyGridSplines.TabIndex = 5;
            this.propertyGridSplines.ToolbarVisible = false;
            this.propertyGridSplines.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.propertyGridSplines_PropertyValueChanged);
            // 
            // buttonViewHere
            // 
            this.buttonViewHere.Location = new System.Drawing.Point(112, 39);
            this.buttonViewHere.Name = "buttonViewHere";
            this.buttonViewHere.Size = new System.Drawing.Size(94, 21);
            this.buttonViewHere.TabIndex = 65;
            this.buttonViewHere.Text = "View";
            this.buttonViewHere.UseVisualStyleBackColor = true;
            this.buttonViewHere.Click += new System.EventHandler(this.buttonViewHere_Click);
            // 
            // listBoxSplines
            // 
            this.listBoxSplines.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxSplines.FormattingEnabled = true;
            this.listBoxSplines.Location = new System.Drawing.Point(3, 3);
            this.listBoxSplines.Name = "listBoxSplines";
            this.listBoxSplines.Size = new System.Drawing.Size(288, 170);
            this.listBoxSplines.TabIndex = 63;
            this.listBoxSplines.SelectedIndexChanged += new System.EventHandler(this.listBoxSplines_SelectedIndexChanged);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.propertyGridSplines, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.listBoxSplines, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 66);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 170F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(294, 346);
            this.tableLayoutPanel1.TabIndex = 66;
            // 
            // buttonImport
            // 
            this.buttonImport.Location = new System.Drawing.Point(112, 12);
            this.buttonImport.Name = "buttonImport";
            this.buttonImport.Size = new System.Drawing.Size(94, 21);
            this.buttonImport.TabIndex = 67;
            this.buttonImport.Text = "Import";
            this.buttonImport.UseVisualStyleBackColor = true;
            this.buttonImport.Click += new System.EventHandler(this.buttonImport_Click);
            // 
            // buttonExport
            // 
            this.buttonExport.Location = new System.Drawing.Point(12, 39);
            this.buttonExport.Name = "buttonExport";
            this.buttonExport.Size = new System.Drawing.Size(94, 21);
            this.buttonExport.TabIndex = 68;
            this.buttonExport.Text = "Export";
            this.buttonExport.UseVisualStyleBackColor = true;
            this.buttonExport.Click += new System.EventHandler(this.buttonExport_Click);
            // 
            // ShadowSplineMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(318, 424);
            this.Controls.Add(this.buttonExport);
            this.Controls.Add(this.buttonImport);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.buttonViewHere);
            this.Controls.Add(this.buttonRemove);
            this.Controls.Add(this.buttonCopy);
            this.Controls.Add(this.buttonAdd);
            this.MaximizeBox = false;
            this.Name = "ShadowSplineMenu";
            this.ShowIcon = false;
            this.Text = "Shadow Spline Editor";
            this.TopMost = false;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ParticleEditor_FormClosing);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.Button buttonCopy;
        private System.Windows.Forms.Button buttonRemove;
        private System.Windows.Forms.PropertyGrid propertyGridSplines;
        private System.Windows.Forms.Button buttonViewHere;
        private System.Windows.Forms.ListBox listBoxSplines;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button buttonImport;
        private System.Windows.Forms.Button buttonExport;
    }
}