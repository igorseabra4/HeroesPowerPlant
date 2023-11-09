namespace HeroesPowerPlant.ShadowSplineEditor {
    partial class ShadowSplineMenu {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            buttonAdd = new System.Windows.Forms.Button();
            buttonCopy = new System.Windows.Forms.Button();
            buttonRemove = new System.Windows.Forms.Button();
            propertyGridSplines = new System.Windows.Forms.PropertyGrid();
            buttonViewHere = new System.Windows.Forms.Button();
            listBoxSplines = new System.Windows.Forms.ListBox();
            tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            buttonImport = new System.Windows.Forms.Button();
            buttonExport = new System.Windows.Forms.Button();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // buttonAdd
            // 
            buttonAdd.Location = new System.Drawing.Point(14, 14);
            buttonAdd.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            buttonAdd.Name = "buttonAdd";
            buttonAdd.Size = new System.Drawing.Size(110, 24);
            buttonAdd.TabIndex = 2;
            buttonAdd.Text = "Add";
            buttonAdd.UseVisualStyleBackColor = true;
            buttonAdd.Click += buttonAdd_Click;
            // 
            // buttonCopy
            // 
            buttonCopy.Location = new System.Drawing.Point(247, 14);
            buttonCopy.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            buttonCopy.Name = "buttonCopy";
            buttonCopy.Size = new System.Drawing.Size(110, 24);
            buttonCopy.TabIndex = 3;
            buttonCopy.Text = "Copy";
            buttonCopy.UseVisualStyleBackColor = true;
            buttonCopy.Click += buttonCopy_Click;
            // 
            // buttonRemove
            // 
            buttonRemove.Location = new System.Drawing.Point(247, 45);
            buttonRemove.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            buttonRemove.Name = "buttonRemove";
            buttonRemove.Size = new System.Drawing.Size(110, 24);
            buttonRemove.TabIndex = 4;
            buttonRemove.Text = "Remove";
            buttonRemove.UseVisualStyleBackColor = true;
            buttonRemove.Click += buttonRemove_Click;
            // 
            // propertyGridSplines
            // 
            propertyGridSplines.Dock = System.Windows.Forms.DockStyle.Fill;
            propertyGridSplines.HelpVisible = false;
            propertyGridSplines.Location = new System.Drawing.Point(4, 206);
            propertyGridSplines.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            propertyGridSplines.Name = "propertyGridSplines";
            propertyGridSplines.PropertySort = System.Windows.Forms.PropertySort.NoSort;
            propertyGridSplines.Size = new System.Drawing.Size(335, 190);
            propertyGridSplines.TabIndex = 5;
            propertyGridSplines.ToolbarVisible = false;
            propertyGridSplines.PropertyValueChanged += propertyGridSplines_PropertyValueChanged;
            // 
            // buttonViewHere
            // 
            buttonViewHere.Location = new System.Drawing.Point(131, 45);
            buttonViewHere.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            buttonViewHere.Name = "buttonViewHere";
            buttonViewHere.Size = new System.Drawing.Size(110, 24);
            buttonViewHere.TabIndex = 65;
            buttonViewHere.Text = "View";
            buttonViewHere.UseVisualStyleBackColor = true;
            buttonViewHere.Click += buttonViewHere_Click;
            // 
            // listBoxSplines
            // 
            listBoxSplines.Dock = System.Windows.Forms.DockStyle.Fill;
            listBoxSplines.FormattingEnabled = true;
            listBoxSplines.ItemHeight = 15;
            listBoxSplines.Location = new System.Drawing.Point(4, 3);
            listBoxSplines.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            listBoxSplines.Name = "listBoxSplines";
            listBoxSplines.Size = new System.Drawing.Size(335, 197);
            listBoxSplines.TabIndex = 63;
            listBoxSplines.SelectedIndexChanged += listBoxSplines_SelectedIndexChanged;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(propertyGridSplines, 0, 1);
            tableLayoutPanel1.Controls.Add(listBoxSplines, 0, 0);
            tableLayoutPanel1.Location = new System.Drawing.Point(14, 76);
            tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 196F));
            tableLayoutPanel1.Size = new System.Drawing.Size(343, 399);
            tableLayoutPanel1.TabIndex = 66;
            // 
            // buttonImport
            // 
            buttonImport.Location = new System.Drawing.Point(131, 14);
            buttonImport.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            buttonImport.Name = "buttonImport";
            buttonImport.Size = new System.Drawing.Size(110, 24);
            buttonImport.TabIndex = 67;
            buttonImport.Text = "Import";
            buttonImport.UseVisualStyleBackColor = true;
            buttonImport.Click += buttonImport_Click;
            // 
            // buttonExport
            // 
            buttonExport.Location = new System.Drawing.Point(14, 45);
            buttonExport.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            buttonExport.Name = "buttonExport";
            buttonExport.Size = new System.Drawing.Size(110, 24);
            buttonExport.TabIndex = 68;
            buttonExport.Text = "Export...";
            buttonExport.UseVisualStyleBackColor = true;
            buttonExport.Click += buttonExport_Click;
            // 
            // ShadowSplineMenu
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(371, 489);
            Controls.Add(buttonExport);
            Controls.Add(buttonImport);
            Controls.Add(tableLayoutPanel1);
            Controls.Add(buttonViewHere);
            Controls.Add(buttonRemove);
            Controls.Add(buttonCopy);
            Controls.Add(buttonAdd);
            Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            MaximizeBox = false;
            Name = "ShadowSplineMenu";
            ShowIcon = false;
            Text = "Shadow Spline Editor";
            FormClosing += ParticleEditor_FormClosing;
            Load += ShadowSplineMenu_Load;
            tableLayoutPanel1.ResumeLayout(false);
            ResumeLayout(false);
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