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
            this.SuspendLayout();
            // 
            // listBoxSplines
            // 
            this.listBoxSplines.FormattingEnabled = true;
            this.listBoxSplines.Location = new System.Drawing.Point(12, 12);
            this.listBoxSplines.Name = "listBoxSplines";
            this.listBoxSplines.Size = new System.Drawing.Size(109, 147);
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
            this.comboBoxType.Location = new System.Drawing.Point(12, 165);
            this.comboBoxType.Name = "comboBoxType";
            this.comboBoxType.Size = new System.Drawing.Size(109, 21);
            this.comboBoxType.TabIndex = 10;
            this.comboBoxType.SelectedIndexChanged += new System.EventHandler(this.comboBoxType_SelectedIndexChanged);
            // 
            // buttonDelete
            // 
            this.buttonDelete.Location = new System.Drawing.Point(127, 41);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(76, 23);
            this.buttonDelete.TabIndex = 11;
            this.buttonDelete.Text = "Delete";
            this.buttonDelete.UseVisualStyleBackColor = true;
            this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
            // 
            // buttonAdd
            // 
            this.buttonAdd.Location = new System.Drawing.Point(127, 12);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(76, 23);
            this.buttonAdd.TabIndex = 11;
            this.buttonAdd.Text = "Add";
            this.buttonAdd.UseVisualStyleBackColor = true;
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Enabled = false;
            this.buttonSave.Location = new System.Drawing.Point(127, 101);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(76, 39);
            this.buttonSave.TabIndex = 12;
            this.buttonSave.Text = "Save Splines";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // buttonViewHere
            // 
            this.buttonViewHere.Location = new System.Drawing.Point(127, 70);
            this.buttonViewHere.Name = "buttonViewHere";
            this.buttonViewHere.Size = new System.Drawing.Size(76, 23);
            this.buttonViewHere.TabIndex = 62;
            this.buttonViewHere.Text = "View Here";
            this.buttonViewHere.UseVisualStyleBackColor = true;
            this.buttonViewHere.Click += new System.EventHandler(this.buttonViewHere_Click);
            // 
            // SplineEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(215, 193);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.buttonViewHere);
            this.Controls.Add(this.buttonAdd);
            this.Controls.Add(this.buttonDelete);
            this.Controls.Add(this.comboBoxType);
            this.Controls.Add(this.listBoxSplines);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
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
    }
}