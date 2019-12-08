namespace HeroesPowerPlant.SplineEditor
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
            this.buttonSaveJson = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listBoxSplines
            // 
            this.listBoxSplines.FormattingEnabled = true;
            this.listBoxSplines.ItemHeight = 15;
            this.listBoxSplines.Location = new System.Drawing.Point(14, 14);
            this.listBoxSplines.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.listBoxSplines.Name = "listBoxSplines";
            this.listBoxSplines.Size = new System.Drawing.Size(126, 169);
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
            this.comboBoxType.Location = new System.Drawing.Point(14, 190);
            this.comboBoxType.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.comboBoxType.Name = "comboBoxType";
            this.comboBoxType.Size = new System.Drawing.Size(126, 23);
            this.comboBoxType.TabIndex = 10;
            this.comboBoxType.SelectedIndexChanged += new System.EventHandler(this.comboBoxType_SelectedIndexChanged);
            // 
            // buttonDelete
            // 
            this.buttonDelete.Location = new System.Drawing.Point(148, 47);
            this.buttonDelete.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(89, 27);
            this.buttonDelete.TabIndex = 11;
            this.buttonDelete.Text = "Delete";
            this.buttonDelete.UseVisualStyleBackColor = true;
            this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
            // 
            // buttonAdd
            // 
            this.buttonAdd.Location = new System.Drawing.Point(148, 14);
            this.buttonAdd.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(89, 27);
            this.buttonAdd.TabIndex = 11;
            this.buttonAdd.Text = "Add";
            this.buttonAdd.UseVisualStyleBackColor = true;
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Enabled = false;
            this.buttonSave.Location = new System.Drawing.Point(148, 117);
            this.buttonSave.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(89, 45);
            this.buttonSave.TabIndex = 12;
            this.buttonSave.Text = "Save Splines";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // buttonViewHere
            // 
            this.buttonViewHere.Location = new System.Drawing.Point(148, 81);
            this.buttonViewHere.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.buttonViewHere.Name = "buttonViewHere";
            this.buttonViewHere.Size = new System.Drawing.Size(89, 27);
            this.buttonViewHere.TabIndex = 62;
            this.buttonViewHere.Text = "View Here";
            this.buttonViewHere.UseVisualStyleBackColor = true;
            this.buttonViewHere.Click += new System.EventHandler(this.buttonViewHere_Click);
            // 
            // buttonSaveJson
            // 
            this.buttonSaveJson.Enabled = false;
            this.buttonSaveJson.Location = new System.Drawing.Point(148, 168);
            this.buttonSaveJson.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.buttonSaveJson.Name = "buttonSaveJson";
            this.buttonSaveJson.Size = new System.Drawing.Size(89, 45);
            this.buttonSaveJson.TabIndex = 12;
            this.buttonSaveJson.Text = "Save (Json)";
            this.buttonSaveJson.UseVisualStyleBackColor = true;
            this.buttonSaveJson.Click += new System.EventHandler(this.buttonSaveJson_Click);
            // 
            // SplineEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(251, 223);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.buttonSaveJson);
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
            this.Load += new System.EventHandler(this.CollisionEditor_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox listBoxSplines;
        private System.Windows.Forms.ComboBox comboBoxType;
        private System.Windows.Forms.Button buttonDelete;
        private System.Windows.Forms.Button buttonAdd;
        public System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonViewHere;
        public System.Windows.Forms.Button buttonSaveJson;
    }
}