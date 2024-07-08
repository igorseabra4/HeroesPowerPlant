namespace HeroesPowerPlant.MainForm
{
    partial class ViewConfig
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ViewConfig));
            groupBox2 = new System.Windows.Forms.GroupBox();
            NumericCameraYaw = new System.Windows.Forms.NumericUpDown();
            NumericCameraPitch = new System.Windows.Forms.NumericUpDown();
            groupBox4 = new System.Windows.Forms.GroupBox();
            NumericInterval = new System.Windows.Forms.NumericUpDown();
            groupBox3 = new System.Windows.Forms.GroupBox();
            NumericDrawD = new System.Windows.Forms.NumericUpDown();
            groupBox1 = new System.Windows.Forms.GroupBox();
            NumericCameraZ = new System.Windows.Forms.NumericUpDown();
            NumericCameraY = new System.Windows.Forms.NumericUpDown();
            NumericCameraX = new System.Windows.Forms.NumericUpDown();
            groupBox6 = new System.Windows.Forms.GroupBox();
            NumericQuadHeight = new System.Windows.Forms.NumericUpDown();
            groupBox5 = new System.Windows.Forms.GroupBox();
            NumericFOV = new System.Windows.Forms.NumericUpDown();
            groupBox7 = new System.Windows.Forms.GroupBox();
            NumericMouseSens = new System.Windows.Forms.NumericUpDown();
            groupBox8 = new System.Windows.Forms.GroupBox();
            NumericKeyboardSens = new System.Windows.Forms.NumericUpDown();
            buttonTeleport = new System.Windows.Forms.Button();
            groupBox9 = new System.Windows.Forms.GroupBox();
            maxFps_numericUpDown = new System.Windows.Forms.NumericUpDown();
            buttonCopyPositionForNukkoro2 = new System.Windows.Forms.Button();
            groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)NumericCameraYaw).BeginInit();
            ((System.ComponentModel.ISupportInitialize)NumericCameraPitch).BeginInit();
            groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)NumericInterval).BeginInit();
            groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)NumericDrawD).BeginInit();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)NumericCameraZ).BeginInit();
            ((System.ComponentModel.ISupportInitialize)NumericCameraY).BeginInit();
            ((System.ComponentModel.ISupportInitialize)NumericCameraX).BeginInit();
            groupBox6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)NumericQuadHeight).BeginInit();
            groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)NumericFOV).BeginInit();
            groupBox7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)NumericMouseSens).BeginInit();
            groupBox8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)NumericKeyboardSens).BeginInit();
            groupBox9.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)maxFps_numericUpDown).BeginInit();
            SuspendLayout();
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(NumericCameraYaw);
            groupBox2.Controls.Add(NumericCameraPitch);
            resources.ApplyResources(groupBox2, "groupBox2");
            groupBox2.Name = "groupBox2";
            groupBox2.TabStop = false;
            // 
            // NumericCameraYaw
            // 
            NumericCameraYaw.DecimalPlaces = 4;
            resources.ApplyResources(NumericCameraYaw, "NumericCameraYaw");
            NumericCameraYaw.Maximum = new decimal(new int[] { -1, -1, -1, 0 });
            NumericCameraYaw.Minimum = new decimal(new int[] { -1, -1, -1, int.MinValue });
            NumericCameraYaw.Name = "NumericCameraYaw";
            NumericCameraYaw.ValueChanged += NumericCameraRot_ValueChanged;
            // 
            // NumericCameraPitch
            // 
            NumericCameraPitch.DecimalPlaces = 4;
            resources.ApplyResources(NumericCameraPitch, "NumericCameraPitch");
            NumericCameraPitch.Maximum = new decimal(new int[] { -1, -1, -1, 0 });
            NumericCameraPitch.Minimum = new decimal(new int[] { -1, -1, -1, int.MinValue });
            NumericCameraPitch.Name = "NumericCameraPitch";
            NumericCameraPitch.ValueChanged += NumericCameraRot_ValueChanged;
            // 
            // groupBox4
            // 
            groupBox4.Controls.Add(NumericInterval);
            resources.ApplyResources(groupBox4, "groupBox4");
            groupBox4.Name = "groupBox4";
            groupBox4.TabStop = false;
            // 
            // NumericInterval
            // 
            NumericInterval.DecimalPlaces = 4;
            resources.ApplyResources(NumericInterval, "NumericInterval");
            NumericInterval.Name = "NumericInterval";
            NumericInterval.Value = new decimal(new int[] { 35, 0, 0, 65536 });
            NumericInterval.ValueChanged += NumericInterval_ValueChanged;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(NumericDrawD);
            resources.ApplyResources(groupBox3, "groupBox3");
            groupBox3.Name = "groupBox3";
            groupBox3.TabStop = false;
            // 
            // NumericDrawD
            // 
            NumericDrawD.DecimalPlaces = 4;
            resources.ApplyResources(NumericDrawD, "NumericDrawD");
            NumericDrawD.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            NumericDrawD.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            NumericDrawD.Name = "NumericDrawD";
            NumericDrawD.Value = new decimal(new int[] { 500000, 0, 0, 0 });
            NumericDrawD.ValueChanged += NumericDrawD_ValueChanged;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(NumericCameraZ);
            groupBox1.Controls.Add(NumericCameraY);
            groupBox1.Controls.Add(NumericCameraX);
            resources.ApplyResources(groupBox1, "groupBox1");
            groupBox1.Name = "groupBox1";
            groupBox1.TabStop = false;
            // 
            // NumericCameraZ
            // 
            NumericCameraZ.DecimalPlaces = 4;
            resources.ApplyResources(NumericCameraZ, "NumericCameraZ");
            NumericCameraZ.Maximum = new decimal(new int[] { -1, -1, -1, 0 });
            NumericCameraZ.Minimum = new decimal(new int[] { -1, -1, -1, int.MinValue });
            NumericCameraZ.Name = "NumericCameraZ";
            NumericCameraZ.ValueChanged += NumericCamera_ValueChanged;
            // 
            // NumericCameraY
            // 
            NumericCameraY.DecimalPlaces = 4;
            resources.ApplyResources(NumericCameraY, "NumericCameraY");
            NumericCameraY.Maximum = new decimal(new int[] { -1, -1, -1, 0 });
            NumericCameraY.Minimum = new decimal(new int[] { -1, -1, -1, int.MinValue });
            NumericCameraY.Name = "NumericCameraY";
            NumericCameraY.ValueChanged += NumericCamera_ValueChanged;
            // 
            // NumericCameraX
            // 
            NumericCameraX.DecimalPlaces = 4;
            resources.ApplyResources(NumericCameraX, "NumericCameraX");
            NumericCameraX.Maximum = new decimal(new int[] { -1, -1, -1, 0 });
            NumericCameraX.Minimum = new decimal(new int[] { -1, -1, -1, int.MinValue });
            NumericCameraX.Name = "NumericCameraX";
            NumericCameraX.ValueChanged += NumericCamera_ValueChanged;
            // 
            // groupBox6
            // 
            groupBox6.Controls.Add(NumericQuadHeight);
            resources.ApplyResources(groupBox6, "groupBox6");
            groupBox6.Name = "groupBox6";
            groupBox6.TabStop = false;
            // 
            // NumericQuadHeight
            // 
            NumericQuadHeight.DecimalPlaces = 4;
            resources.ApplyResources(NumericQuadHeight, "NumericQuadHeight");
            NumericQuadHeight.Maximum = new decimal(new int[] { -1, -1, -1, 0 });
            NumericQuadHeight.Minimum = new decimal(new int[] { -1, -1, -1, int.MinValue });
            NumericQuadHeight.Name = "NumericQuadHeight";
            NumericQuadHeight.ValueChanged += NumericQuadHeight_ValueChanged;
            // 
            // groupBox5
            // 
            groupBox5.Controls.Add(NumericFOV);
            resources.ApplyResources(groupBox5, "groupBox5");
            groupBox5.Name = "groupBox5";
            groupBox5.TabStop = false;
            // 
            // NumericFOV
            // 
            NumericFOV.DecimalPlaces = 4;
            resources.ApplyResources(NumericFOV, "NumericFOV");
            NumericFOV.Maximum = new decimal(new int[] { 1799999, 0, 0, 262144 });
            NumericFOV.Minimum = new decimal(new int[] { 1, 0, 0, 262144 });
            NumericFOV.Name = "NumericFOV";
            NumericFOV.Value = new decimal(new int[] { 45, 0, 0, 0 });
            NumericFOV.ValueChanged += NumericFOV_ValueChanged;
            // 
            // groupBox7
            // 
            groupBox7.Controls.Add(NumericMouseSens);
            resources.ApplyResources(groupBox7, "groupBox7");
            groupBox7.Name = "groupBox7";
            groupBox7.TabStop = false;
            // 
            // NumericMouseSens
            // 
            NumericMouseSens.DecimalPlaces = 4;
            NumericMouseSens.Increment = new decimal(new int[] { 1, 0, 0, 131072 });
            resources.ApplyResources(NumericMouseSens, "NumericMouseSens");
            NumericMouseSens.Maximum = new decimal(new int[] { 666666, 0, 0, 0 });
            NumericMouseSens.Name = "NumericMouseSens";
            NumericMouseSens.Value = new decimal(new int[] { 1, 0, 0, 0 });
            NumericMouseSens.ValueChanged += NumericMouseSens_ValueChanged;
            // 
            // groupBox8
            // 
            groupBox8.Controls.Add(NumericKeyboardSens);
            resources.ApplyResources(groupBox8, "groupBox8");
            groupBox8.Name = "groupBox8";
            groupBox8.TabStop = false;
            // 
            // NumericKeyboardSens
            // 
            NumericKeyboardSens.DecimalPlaces = 4;
            NumericKeyboardSens.Increment = new decimal(new int[] { 1, 0, 0, 131072 });
            resources.ApplyResources(NumericKeyboardSens, "NumericKeyboardSens");
            NumericKeyboardSens.Maximum = new decimal(new int[] { 666666, 0, 0, 0 });
            NumericKeyboardSens.Name = "NumericKeyboardSens";
            NumericKeyboardSens.Value = new decimal(new int[] { 1, 0, 0, 0 });
            NumericKeyboardSens.ValueChanged += NumericKeyboardSens_ValueChanged;
            // 
            // buttonTeleport
            // 
            resources.ApplyResources(buttonTeleport, "buttonTeleport");
            buttonTeleport.Name = "buttonTeleport";
            buttonTeleport.UseVisualStyleBackColor = true;
            buttonTeleport.Click += buttonTeleport_Click;
            // 
            // groupBox9
            // 
            groupBox9.Controls.Add(maxFps_numericUpDown);
            resources.ApplyResources(groupBox9, "groupBox9");
            groupBox9.Name = "groupBox9";
            groupBox9.TabStop = false;
            // 
            // maxFps_numericUpDown
            // 
            maxFps_numericUpDown.DecimalPlaces = 4;
            maxFps_numericUpDown.Increment = new decimal(new int[] { 1, 0, 0, 131072 });
            resources.ApplyResources(maxFps_numericUpDown, "maxFps_numericUpDown");
            maxFps_numericUpDown.Maximum = new decimal(new int[] { 666666, 0, 0, 0 });
            maxFps_numericUpDown.Minimum = new decimal(new int[] { 10, 0, 0, 0 });
            maxFps_numericUpDown.Name = "maxFps_numericUpDown";
            maxFps_numericUpDown.Value = new decimal(new int[] { 60, 0, 0, 0 });
            maxFps_numericUpDown.ValueChanged += maxFps_numericUpDown_ValueChanged;
            // 
            // buttonCopyPositionForNukkoro2
            // 
            resources.ApplyResources(buttonCopyPositionForNukkoro2, "buttonCopyPositionForNukkoro2");
            buttonCopyPositionForNukkoro2.Name = "buttonCopyPositionForNukkoro2";
            buttonCopyPositionForNukkoro2.UseVisualStyleBackColor = true;
            buttonCopyPositionForNukkoro2.Click += buttonCopyPositionForNukkoro2_Click;
            // 
            // ViewConfig
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(buttonCopyPositionForNukkoro2);
            Controls.Add(groupBox9);
            Controls.Add(buttonTeleport);
            Controls.Add(groupBox8);
            Controls.Add(groupBox7);
            Controls.Add(groupBox5);
            Controls.Add(groupBox6);
            Controls.Add(groupBox2);
            Controls.Add(groupBox4);
            Controls.Add(groupBox3);
            Controls.Add(groupBox1);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "ViewConfig";
            ShowIcon = false;
            Load += ViewConfig_Load;
            VisibleChanged += ViewConfig_VisibleChanged;
            groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)NumericCameraYaw).EndInit();
            ((System.ComponentModel.ISupportInitialize)NumericCameraPitch).EndInit();
            groupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)NumericInterval).EndInit();
            groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)NumericDrawD).EndInit();
            groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)NumericCameraZ).EndInit();
            ((System.ComponentModel.ISupportInitialize)NumericCameraY).EndInit();
            ((System.ComponentModel.ISupportInitialize)NumericCameraX).EndInit();
            groupBox6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)NumericQuadHeight).EndInit();
            groupBox5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)NumericFOV).EndInit();
            groupBox7.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)NumericMouseSens).EndInit();
            groupBox8.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)NumericKeyboardSens).EndInit();
            groupBox9.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)maxFps_numericUpDown).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        public System.Windows.Forms.NumericUpDown NumericCameraYaw;
        public System.Windows.Forms.NumericUpDown NumericCameraPitch;
        private System.Windows.Forms.GroupBox groupBox4;
        public System.Windows.Forms.NumericUpDown NumericInterval;
        private System.Windows.Forms.GroupBox groupBox3;
        public System.Windows.Forms.NumericUpDown NumericDrawD;
        private System.Windows.Forms.GroupBox groupBox1;
        public System.Windows.Forms.NumericUpDown NumericCameraY;
        public System.Windows.Forms.NumericUpDown NumericCameraX;
        public System.Windows.Forms.NumericUpDown NumericCameraZ;
        private System.Windows.Forms.GroupBox groupBox6;
        public System.Windows.Forms.NumericUpDown NumericQuadHeight;
        private System.Windows.Forms.GroupBox groupBox5;
        public System.Windows.Forms.NumericUpDown NumericFOV;
        private System.Windows.Forms.GroupBox groupBox7;
        public System.Windows.Forms.NumericUpDown NumericMouseSens;
        private System.Windows.Forms.GroupBox groupBox8;
        public System.Windows.Forms.NumericUpDown NumericKeyboardSens;
        private System.Windows.Forms.Button buttonTeleport;
        private System.Windows.Forms.GroupBox groupBox9;
        public System.Windows.Forms.NumericUpDown maxFps_numericUpDown;
        private System.Windows.Forms.Button buttonCopyPositionForNukkoro2;
    }
}