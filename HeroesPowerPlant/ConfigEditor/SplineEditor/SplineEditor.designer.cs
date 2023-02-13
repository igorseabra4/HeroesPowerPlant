﻿namespace HeroesPowerPlant.SplineEditor
{
    partial class SplineEditor
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
            this.listBoxSplines = new System.Windows.Forms.ListBox();
            this.comboBoxType = new System.Windows.Forms.ComboBox();
            this.buttonDelete = new System.Windows.Forms.Button();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonViewHere = new System.Windows.Forms.Button();
            this.listBoxPoints = new System.Windows.Forms.ListBox();
            this.groupBoxPitchRoll = new System.Windows.Forms.GroupBox();
            this.buttonAutoPitchPoint = new System.Windows.Forms.Button();
            this.numericUpDownRoll = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownPitch = new System.Windows.Forms.NumericUpDown();
            this.buttonAutoPitchSpline = new System.Windows.Forms.Button();
            this.buttonAutoPitchAll = new System.Windows.Forms.Button();
            this.buttonExportOBJ = new System.Windows.Forms.Button();
            this.checkBoxExportAll = new System.Windows.Forms.CheckBox();
            this.groupBoxPitchRoll.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRoll)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPitch)).BeginInit();
            this.SuspendLayout();
            // 
            // listBoxSplines
            // 
            this.listBoxSplines.FormattingEnabled = true;
            this.listBoxSplines.ItemHeight = 15;
            this.listBoxSplines.Location = new System.Drawing.Point(14, 47);
            this.listBoxSplines.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.listBoxSplines.Name = "listBoxSplines";
            this.listBoxSplines.Size = new System.Drawing.Size(126, 154);
            this.listBoxSplines.TabIndex = 9;
            this.listBoxSplines.SelectedIndexChanged += new System.EventHandler(this.listBoxSplines_SelectedIndexChanged);
            // 
            // comboBoxType
            // 
            this.comboBoxType.FormattingEnabled = true;
            this.comboBoxType.Items.AddRange(new object[] {
            "Null",
            "Loop",
            "Rail",
            "Ball"});
            this.comboBoxType.Location = new System.Drawing.Point(148, 47);
            this.comboBoxType.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.comboBoxType.Name = "comboBoxType";
            this.comboBoxType.Size = new System.Drawing.Size(126, 23);
            this.comboBoxType.TabIndex = 10;
            this.comboBoxType.SelectedIndexChanged += new System.EventHandler(this.comboBoxType_SelectedIndexChanged);
            // 
            // buttonDelete
            // 
            this.buttonDelete.Location = new System.Drawing.Point(72, 14);
            this.buttonDelete.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(69, 27);
            this.buttonDelete.TabIndex = 11;
            this.buttonDelete.Text = "Delete";
            this.buttonDelete.UseVisualStyleBackColor = true;
            this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
            // 
            // buttonAdd
            // 
            this.buttonAdd.Location = new System.Drawing.Point(14, 14);
            this.buttonAdd.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(51, 27);
            this.buttonAdd.TabIndex = 11;
            this.buttonAdd.Text = "Add";
            this.buttonAdd.UseVisualStyleBackColor = true;
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Enabled = false;
            this.buttonSave.Location = new System.Drawing.Point(148, 14);
            this.buttonSave.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(127, 27);
            this.buttonSave.TabIndex = 12;
            this.buttonSave.Text = "Save Splines";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // buttonViewHere
            // 
            this.buttonViewHere.Location = new System.Drawing.Point(282, 47);
            this.buttonViewHere.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.buttonViewHere.Name = "buttonViewHere";
            this.buttonViewHere.Size = new System.Drawing.Size(149, 24);
            this.buttonViewHere.TabIndex = 62;
            this.buttonViewHere.Text = "View Here";
            this.buttonViewHere.UseVisualStyleBackColor = true;
            this.buttonViewHere.Click += new System.EventHandler(this.buttonViewHere_Click);
            // 
            // listBoxPoints
            // 
            this.listBoxPoints.FormattingEnabled = true;
            this.listBoxPoints.ItemHeight = 15;
            this.listBoxPoints.Location = new System.Drawing.Point(148, 77);
            this.listBoxPoints.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.listBoxPoints.Name = "listBoxPoints";
            this.listBoxPoints.Size = new System.Drawing.Size(126, 154);
            this.listBoxPoints.TabIndex = 63;
            this.listBoxPoints.SelectedIndexChanged += new System.EventHandler(this.listBoxPoints_SelectedIndexChanged);
            // 
            // groupBoxPitchRoll
            // 
            this.groupBoxPitchRoll.Controls.Add(this.buttonAutoPitchPoint);
            this.groupBoxPitchRoll.Controls.Add(this.numericUpDownRoll);
            this.groupBoxPitchRoll.Controls.Add(this.numericUpDownPitch);
            this.groupBoxPitchRoll.Enabled = false;
            this.groupBoxPitchRoll.Location = new System.Drawing.Point(282, 78);
            this.groupBoxPitchRoll.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBoxPitchRoll.Name = "groupBoxPitchRoll";
            this.groupBoxPitchRoll.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBoxPitchRoll.Size = new System.Drawing.Size(149, 115);
            this.groupBoxPitchRoll.TabIndex = 64;
            this.groupBoxPitchRoll.TabStop = false;
            this.groupBoxPitchRoll.Text = "Pitch, Roll";
            // 
            // buttonAutoPitchPoint
            // 
            this.buttonAutoPitchPoint.Location = new System.Drawing.Point(7, 82);
            this.buttonAutoPitchPoint.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.buttonAutoPitchPoint.Name = "buttonAutoPitchPoint";
            this.buttonAutoPitchPoint.Size = new System.Drawing.Size(135, 27);
            this.buttonAutoPitchPoint.TabIndex = 65;
            this.buttonAutoPitchPoint.Text = "AutoPitch Point";
            this.buttonAutoPitchPoint.UseVisualStyleBackColor = true;
            this.buttonAutoPitchPoint.Click += new System.EventHandler(this.buttonAutoPitchPoint_Click);
            // 
            // numericUpDownRoll
            // 
            this.numericUpDownRoll.DecimalPlaces = 4;
            this.numericUpDownRoll.Location = new System.Drawing.Point(7, 52);
            this.numericUpDownRoll.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.numericUpDownRoll.Maximum = new decimal(new int[] {
            360,
            0,
            0,
            0});
            this.numericUpDownRoll.Name = "numericUpDownRoll";
            this.numericUpDownRoll.Size = new System.Drawing.Size(135, 23);
            this.numericUpDownRoll.TabIndex = 1;
            this.numericUpDownRoll.ValueChanged += new System.EventHandler(this.numericUpDown_ValueChanged);
            // 
            // numericUpDownPitch
            // 
            this.numericUpDownPitch.DecimalPlaces = 4;
            this.numericUpDownPitch.Location = new System.Drawing.Point(7, 22);
            this.numericUpDownPitch.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.numericUpDownPitch.Maximum = new decimal(new int[] {
            360,
            0,
            0,
            0});
            this.numericUpDownPitch.Name = "numericUpDownPitch";
            this.numericUpDownPitch.Size = new System.Drawing.Size(135, 23);
            this.numericUpDownPitch.TabIndex = 0;
            this.numericUpDownPitch.ValueChanged += new System.EventHandler(this.numericUpDown_ValueChanged);
            // 
            // buttonAutoPitchSpline
            // 
            this.buttonAutoPitchSpline.Location = new System.Drawing.Point(289, 201);
            this.buttonAutoPitchSpline.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.buttonAutoPitchSpline.Name = "buttonAutoPitchSpline";
            this.buttonAutoPitchSpline.Size = new System.Drawing.Size(135, 27);
            this.buttonAutoPitchSpline.TabIndex = 66;
            this.buttonAutoPitchSpline.Text = "AutoPitch Spline";
            this.buttonAutoPitchSpline.UseVisualStyleBackColor = true;
            this.buttonAutoPitchSpline.Click += new System.EventHandler(this.buttonAutoPitchSpline_Click);
            // 
            // buttonAutoPitchAll
            // 
            this.buttonAutoPitchAll.Location = new System.Drawing.Point(282, 14);
            this.buttonAutoPitchAll.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.buttonAutoPitchAll.Name = "buttonAutoPitchAll";
            this.buttonAutoPitchAll.Size = new System.Drawing.Size(149, 27);
            this.buttonAutoPitchAll.TabIndex = 67;
            this.buttonAutoPitchAll.Text = "AutoPitch All";
            this.buttonAutoPitchAll.UseVisualStyleBackColor = true;
            this.buttonAutoPitchAll.Click += new System.EventHandler(this.buttonAutoPitchAll_Click);
            // 
            // buttonExportOBJ
            // 
            this.buttonExportOBJ.Location = new System.Drawing.Point(14, 205);
            this.buttonExportOBJ.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.buttonExportOBJ.Name = "buttonExportOBJ";
            this.buttonExportOBJ.Size = new System.Drawing.Size(127, 27);
            this.buttonExportOBJ.TabIndex = 68;
            this.buttonExportOBJ.Text = "Export OBJ";
            this.buttonExportOBJ.UseVisualStyleBackColor = true;
            this.buttonExportOBJ.Click += new System.EventHandler(this.buttonExportOBJ_Click);
            // 
            // checkBoxExportAll
            // 
            this.checkBoxExportAll.AutoSize = true;
            this.checkBoxExportAll.Location = new System.Drawing.Point(16, 237);
            this.checkBoxExportAll.Name = "checkBoxExportAll";
            this.checkBoxExportAll.Size = new System.Drawing.Size(117, 19);
            this.checkBoxExportAll.TabIndex = 69;
            this.checkBoxExportAll.Text = "Export All Splines";
            this.checkBoxExportAll.UseVisualStyleBackColor = true;
            // 
            // SplineEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(440, 260);
            this.Controls.Add(this.checkBoxExportAll);
            this.Controls.Add(this.buttonExportOBJ);
            this.Controls.Add(this.buttonAutoPitchAll);
            this.Controls.Add(this.buttonAutoPitchSpline);
            this.Controls.Add(this.groupBoxPitchRoll);
            this.Controls.Add(this.listBoxPoints);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.buttonViewHere);
            this.Controls.Add(this.buttonAdd);
            this.Controls.Add(this.buttonDelete);
            this.Controls.Add(this.comboBoxType);
            this.Controls.Add(this.listBoxSplines);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MaximizeBox = false;
            this.Name = "SplineEditor";
            this.ShowIcon = false;
            this.Text = "Spline Editor";
            this.Load += new System.EventHandler(this.SplineEditor_Load);
            this.groupBoxPitchRoll.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRoll)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPitch)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listBoxSplines;
        private System.Windows.Forms.ComboBox comboBoxType;
        private System.Windows.Forms.Button buttonDelete;
        private System.Windows.Forms.Button buttonAdd;
        public System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonViewHere;
        private System.Windows.Forms.ListBox listBoxPoints;
        private System.Windows.Forms.GroupBox groupBoxPitchRoll;
        private System.Windows.Forms.NumericUpDown numericUpDownRoll;
        private System.Windows.Forms.NumericUpDown numericUpDownPitch;
        private System.Windows.Forms.Button buttonAutoPitchPoint;
        private System.Windows.Forms.Button buttonAutoPitchSpline;
        private System.Windows.Forms.Button buttonAutoPitchAll;
        public System.Windows.Forms.Button buttonExportOBJ;
        private System.Windows.Forms.CheckBox checkBoxExportAll;
    }
}