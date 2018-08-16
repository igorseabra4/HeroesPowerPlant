using System;
using System.Windows.Forms;

namespace HeroesPowerPlant
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.projectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.addObjectONEToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearObjectONEsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editorsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.modLoaderConfigEditorF2ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.levelEditorF3ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.collisionEditorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.layoutEditorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splineEditorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cameraEditorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.noCullingCToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.wireframeFToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.BackgroundColorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selectionColorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.mouseModeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.objectsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.camerasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startPosYToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splinesUToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.renderByChunkHToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.chunkBoxesBToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showCollisionXToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showQuadtreeTToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showObjectsGToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.camerasVToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.vSyncToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.renderPanel = new System.Windows.Forms.Panel();
            this.autoLoadLastProjectOnLaunchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.projectToolStripMenuItem,
            this.editorsToolStripMenuItem,
            this.optionsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1263, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // projectToolStripMenuItem
            // 
            this.projectToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.toolStripSeparator1,
            this.addObjectONEToolStripMenuItem,
            this.clearObjectONEsToolStripMenuItem,
            this.toolStripSeparator2,
            this.aboutToolStripMenuItem});
            this.projectToolStripMenuItem.Name = "projectToolStripMenuItem";
            this.projectToolStripMenuItem.Size = new System.Drawing.Size(56, 20);
            this.projectToolStripMenuItem.Text = "Project";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.ToolstripFileOpen);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.ToolStripFileSave);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.saveAsToolStripMenuItem.Text = "Save As...";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.ToolStripFileSaveAs);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(177, 6);
            // 
            // addObjectONEToolStripMenuItem
            // 
            this.addObjectONEToolStripMenuItem.Name = "addObjectONEToolStripMenuItem";
            this.addObjectONEToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.addObjectONEToolStripMenuItem.Text = "Add Object ONE";
            this.addObjectONEToolStripMenuItem.Click += new System.EventHandler(this.addObjectONEToolStripMenuItem_Click);
            // 
            // clearObjectONEsToolStripMenuItem
            // 
            this.clearObjectONEsToolStripMenuItem.Name = "clearObjectONEsToolStripMenuItem";
            this.clearObjectONEsToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.clearObjectONEsToolStripMenuItem.Text = "Clear Object ONEs";
            this.clearObjectONEsToolStripMenuItem.Click += new System.EventHandler(this.clearObjectONEsToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(177, 6);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // editorsToolStripMenuItem
            // 
            this.editorsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.modLoaderConfigEditorF2ToolStripMenuItem,
            this.levelEditorF3ToolStripMenuItem,
            this.collisionEditorToolStripMenuItem,
            this.layoutEditorToolStripMenuItem,
            this.splineEditorToolStripMenuItem,
            this.cameraEditorToolStripMenuItem});
            this.editorsToolStripMenuItem.Name = "editorsToolStripMenuItem";
            this.editorsToolStripMenuItem.Size = new System.Drawing.Size(55, 20);
            this.editorsToolStripMenuItem.Text = "Editors";
            // 
            // modLoaderConfigEditorF2ToolStripMenuItem
            // 
            this.modLoaderConfigEditorF2ToolStripMenuItem.Name = "modLoaderConfigEditorF2ToolStripMenuItem";
            this.modLoaderConfigEditorF2ToolStripMenuItem.Size = new System.Drawing.Size(234, 22);
            this.modLoaderConfigEditorF2ToolStripMenuItem.Text = "Mod Loader Config Editor (F2)";
            this.modLoaderConfigEditorF2ToolStripMenuItem.Click += new System.EventHandler(this.modLoaderConfigEditorF2ToolStripMenuItem_Click);
            // 
            // levelEditorF3ToolStripMenuItem
            // 
            this.levelEditorF3ToolStripMenuItem.Name = "levelEditorF3ToolStripMenuItem";
            this.levelEditorF3ToolStripMenuItem.Size = new System.Drawing.Size(234, 22);
            this.levelEditorF3ToolStripMenuItem.Text = "Level Editor (F3)";
            this.levelEditorF3ToolStripMenuItem.Click += new System.EventHandler(this.levelEditorF3ToolStripMenuItem_Click);
            // 
            // collisionEditorToolStripMenuItem
            // 
            this.collisionEditorToolStripMenuItem.Name = "collisionEditorToolStripMenuItem";
            this.collisionEditorToolStripMenuItem.Size = new System.Drawing.Size(234, 22);
            this.collisionEditorToolStripMenuItem.Text = "Collision Editor (F4)";
            this.collisionEditorToolStripMenuItem.Click += new System.EventHandler(this.collisionEditorToolStripMenuItem_Click);
            // 
            // layoutEditorToolStripMenuItem
            // 
            this.layoutEditorToolStripMenuItem.Name = "layoutEditorToolStripMenuItem";
            this.layoutEditorToolStripMenuItem.Size = new System.Drawing.Size(234, 22);
            this.layoutEditorToolStripMenuItem.Text = "Layout Editor (F5)";
            this.layoutEditorToolStripMenuItem.Click += new System.EventHandler(this.layoutEditorToolStripMenuItem_Click);
            // 
            // splineEditorToolStripMenuItem
            // 
            this.splineEditorToolStripMenuItem.Enabled = false;
            this.splineEditorToolStripMenuItem.Name = "splineEditorToolStripMenuItem";
            this.splineEditorToolStripMenuItem.Size = new System.Drawing.Size(234, 22);
            this.splineEditorToolStripMenuItem.Text = "Spline Editor (F6)";
            this.splineEditorToolStripMenuItem.Click += new System.EventHandler(this.splineEditorToolStripMenuItem_Click);
            // 
            // cameraEditorToolStripMenuItem
            // 
            this.cameraEditorToolStripMenuItem.Name = "cameraEditorToolStripMenuItem";
            this.cameraEditorToolStripMenuItem.Size = new System.Drawing.Size(234, 22);
            this.cameraEditorToolStripMenuItem.Text = "Camera Editor (F7)";
            this.cameraEditorToolStripMenuItem.Click += new System.EventHandler(this.cameraEditorToolStripMenuItem_Click);
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.noCullingCToolStripMenuItem,
            this.wireframeFToolStripMenuItem,
            this.BackgroundColorToolStripMenuItem,
            this.selectionColorToolStripMenuItem,
            this.toolStripSeparator3,
            this.mouseModeToolStripMenuItem,
            this.startPosYToolStripMenuItem,
            this.splinesUToolStripMenuItem,
            this.renderByChunkHToolStripMenuItem,
            this.chunkBoxesBToolStripMenuItem,
            this.showCollisionXToolStripMenuItem,
            this.showQuadtreeTToolStripMenuItem,
            this.showObjectsGToolStripMenuItem,
            this.camerasVToolStripMenuItem,
            this.toolStripSeparator4,
            this.vSyncToolStripMenuItem,
            this.autoLoadLastProjectOnLaunchToolStripMenuItem});
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.optionsToolStripMenuItem.Text = "Options";
            // 
            // noCullingCToolStripMenuItem
            // 
            this.noCullingCToolStripMenuItem.Name = "noCullingCToolStripMenuItem";
            this.noCullingCToolStripMenuItem.Size = new System.Drawing.Size(254, 22);
            this.noCullingCToolStripMenuItem.Text = "No Culling (C)";
            this.noCullingCToolStripMenuItem.Click += new System.EventHandler(this.noCullingCToolStripMenuItem_Click);
            // 
            // wireframeFToolStripMenuItem
            // 
            this.wireframeFToolStripMenuItem.Name = "wireframeFToolStripMenuItem";
            this.wireframeFToolStripMenuItem.Size = new System.Drawing.Size(254, 22);
            this.wireframeFToolStripMenuItem.Text = "Wireframe (F)";
            this.wireframeFToolStripMenuItem.Click += new System.EventHandler(this.wireframeFToolStripMenuItem_Click);
            // 
            // BackgroundColorToolStripMenuItem
            // 
            this.BackgroundColorToolStripMenuItem.Name = "BackgroundColorToolStripMenuItem";
            this.BackgroundColorToolStripMenuItem.Size = new System.Drawing.Size(254, 22);
            this.BackgroundColorToolStripMenuItem.Text = "Background Color...";
            this.BackgroundColorToolStripMenuItem.Click += new System.EventHandler(this.BackgroundColorToolStripMenuItem_Click);
            // 
            // selectionColorToolStripMenuItem
            // 
            this.selectionColorToolStripMenuItem.Name = "selectionColorToolStripMenuItem";
            this.selectionColorToolStripMenuItem.Size = new System.Drawing.Size(254, 22);
            this.selectionColorToolStripMenuItem.Text = "Selection Color...";
            this.selectionColorToolStripMenuItem.Click += new System.EventHandler(this.selectionColorToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(251, 6);
            // 
            // mouseModeToolStripMenuItem
            // 
            this.mouseModeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.objectsToolStripMenuItem,
            this.camerasToolStripMenuItem});
            this.mouseModeToolStripMenuItem.Name = "mouseModeToolStripMenuItem";
            this.mouseModeToolStripMenuItem.Size = new System.Drawing.Size(254, 22);
            this.mouseModeToolStripMenuItem.Text = "Mouse Mode";
            // 
            // objectsToolStripMenuItem
            // 
            this.objectsToolStripMenuItem.Checked = true;
            this.objectsToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.objectsToolStripMenuItem.Name = "objectsToolStripMenuItem";
            this.objectsToolStripMenuItem.Size = new System.Drawing.Size(120, 22);
            this.objectsToolStripMenuItem.Text = "Objects";
            this.objectsToolStripMenuItem.Click += new System.EventHandler(this.objectsToolStripMenuItem_Click);
            // 
            // camerasToolStripMenuItem
            // 
            this.camerasToolStripMenuItem.Name = "camerasToolStripMenuItem";
            this.camerasToolStripMenuItem.Size = new System.Drawing.Size(120, 22);
            this.camerasToolStripMenuItem.Text = "Cameras";
            this.camerasToolStripMenuItem.Click += new System.EventHandler(this.camerasToolStripMenuItem_Click);
            // 
            // startPosYToolStripMenuItem
            // 
            this.startPosYToolStripMenuItem.Checked = true;
            this.startPosYToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.startPosYToolStripMenuItem.Name = "startPosYToolStripMenuItem";
            this.startPosYToolStripMenuItem.Size = new System.Drawing.Size(254, 22);
            this.startPosYToolStripMenuItem.Text = "Start Pos (Y)";
            this.startPosYToolStripMenuItem.Click += new System.EventHandler(this.startPosYToolStripMenuItem_Click);
            // 
            // splinesUToolStripMenuItem
            // 
            this.splinesUToolStripMenuItem.Checked = true;
            this.splinesUToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.splinesUToolStripMenuItem.Name = "splinesUToolStripMenuItem";
            this.splinesUToolStripMenuItem.Size = new System.Drawing.Size(254, 22);
            this.splinesUToolStripMenuItem.Text = "Splines (U)";
            this.splinesUToolStripMenuItem.Click += new System.EventHandler(this.splinesUToolStripMenuItem_Click);
            // 
            // renderByChunkHToolStripMenuItem
            // 
            this.renderByChunkHToolStripMenuItem.Checked = true;
            this.renderByChunkHToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.renderByChunkHToolStripMenuItem.Name = "renderByChunkHToolStripMenuItem";
            this.renderByChunkHToolStripMenuItem.Size = new System.Drawing.Size(254, 22);
            this.renderByChunkHToolStripMenuItem.Text = "Render By Chunk (H)";
            this.renderByChunkHToolStripMenuItem.Click += new System.EventHandler(this.renderByChunkHToolStripMenuItem_Click);
            // 
            // chunkBoxesBToolStripMenuItem
            // 
            this.chunkBoxesBToolStripMenuItem.Name = "chunkBoxesBToolStripMenuItem";
            this.chunkBoxesBToolStripMenuItem.Size = new System.Drawing.Size(254, 22);
            this.chunkBoxesBToolStripMenuItem.Text = "Chunk Boxes (B)";
            this.chunkBoxesBToolStripMenuItem.Click += new System.EventHandler(this.chunkBoxesBToolStripMenuItem_Click);
            // 
            // showCollisionXToolStripMenuItem
            // 
            this.showCollisionXToolStripMenuItem.Name = "showCollisionXToolStripMenuItem";
            this.showCollisionXToolStripMenuItem.Size = new System.Drawing.Size(254, 22);
            this.showCollisionXToolStripMenuItem.Text = "Collision (X)";
            this.showCollisionXToolStripMenuItem.Click += new System.EventHandler(this.showCollisionXToolStripMenuItem_Click);
            // 
            // showQuadtreeTToolStripMenuItem
            // 
            this.showQuadtreeTToolStripMenuItem.Name = "showQuadtreeTToolStripMenuItem";
            this.showQuadtreeTToolStripMenuItem.Size = new System.Drawing.Size(254, 22);
            this.showQuadtreeTToolStripMenuItem.Text = "Quadtree (T)";
            this.showQuadtreeTToolStripMenuItem.Click += new System.EventHandler(this.showQuadtreeTToolStripMenuItem_Click);
            // 
            // showObjectsGToolStripMenuItem
            // 
            this.showObjectsGToolStripMenuItem.Name = "showObjectsGToolStripMenuItem";
            this.showObjectsGToolStripMenuItem.Size = new System.Drawing.Size(254, 22);
            this.showObjectsGToolStripMenuItem.Text = "Objects (G)";
            this.showObjectsGToolStripMenuItem.Click += new System.EventHandler(this.showObjectsGToolStripMenuItem_Click);
            // 
            // camerasVToolStripMenuItem
            // 
            this.camerasVToolStripMenuItem.Checked = true;
            this.camerasVToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.camerasVToolStripMenuItem.Name = "camerasVToolStripMenuItem";
            this.camerasVToolStripMenuItem.Size = new System.Drawing.Size(254, 22);
            this.camerasVToolStripMenuItem.Text = "Cameras (V)";
            this.camerasVToolStripMenuItem.Click += new System.EventHandler(this.camerasVToolStripMenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(251, 6);
            // 
            // vSyncToolStripMenuItem
            // 
            this.vSyncToolStripMenuItem.Checked = true;
            this.vSyncToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.vSyncToolStripMenuItem.Name = "vSyncToolStripMenuItem";
            this.vSyncToolStripMenuItem.Size = new System.Drawing.Size(254, 22);
            this.vSyncToolStripMenuItem.Text = "VSync";
            this.vSyncToolStripMenuItem.Click += new System.EventHandler(this.vSyncToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 816);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1263, 22);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 17);
            this.toolStripStatusLabel1.Click += new System.EventHandler(this.toolStripStatusLabel1_Click);
            // 
            // renderPanel
            // 
            this.renderPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.renderPanel.Location = new System.Drawing.Point(0, 24);
            this.renderPanel.Name = "renderPanel";
            this.renderPanel.Size = new System.Drawing.Size(1263, 792);
            this.renderPanel.TabIndex = 4;
            this.renderPanel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.renderPanel_MouseClick);
            this.renderPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MouseMoveControl);
            this.renderPanel.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.renderPanel_MouseWheel);
            this.renderPanel.Resize += new System.EventHandler(this.ResetMouseCenter);
            // 
            // autoLoadLastProjectOnLaunchToolStripMenuItem
            // 
            this.autoLoadLastProjectOnLaunchToolStripMenuItem.Checked = true;
            this.autoLoadLastProjectOnLaunchToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.autoLoadLastProjectOnLaunchToolStripMenuItem.Name = "autoLoadLastProjectOnLaunchToolStripMenuItem";
            this.autoLoadLastProjectOnLaunchToolStripMenuItem.Size = new System.Drawing.Size(254, 22);
            this.autoLoadLastProjectOnLaunchToolStripMenuItem.Text = "Auto-Load Last Project on Launch";
            this.autoLoadLastProjectOnLaunchToolStripMenuItem.Click += new System.EventHandler(this.autoLoadLastProjectOnLaunchToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1263, 838);
            this.Controls.Add(this.renderPanel);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "MainForm";
            this.Text = "Heroes Power Plant";
            this.Deactivate += new System.EventHandler(this.MainForm_Deactivate);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyUp);
            this.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.MouseMoveControl);
            this.Move += new System.EventHandler(this.ResetMouseCenter);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Panel renderPanel;
        private ToolStripStatusLabel toolStripStatusLabel1;
        private ToolStripMenuItem projectToolStripMenuItem;
        private ToolStripMenuItem openToolStripMenuItem;
        private ToolStripMenuItem saveToolStripMenuItem;
        private ToolStripMenuItem saveAsToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem addObjectONEToolStripMenuItem;
        private ToolStripMenuItem clearObjectONEsToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripMenuItem aboutToolStripMenuItem;
        private ToolStripMenuItem editorsToolStripMenuItem;
        private ToolStripMenuItem modLoaderConfigEditorF2ToolStripMenuItem;
        private ToolStripMenuItem levelEditorF3ToolStripMenuItem;
        private ToolStripMenuItem collisionEditorToolStripMenuItem;
        private ToolStripMenuItem layoutEditorToolStripMenuItem;
        private ToolStripMenuItem splineEditorToolStripMenuItem;
        private ToolStripMenuItem cameraEditorToolStripMenuItem;
        private ToolStripMenuItem optionsToolStripMenuItem;
        private ToolStripMenuItem noCullingCToolStripMenuItem;
        private ToolStripMenuItem wireframeFToolStripMenuItem;
        private ToolStripMenuItem BackgroundColorToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator3;
        private ToolStripMenuItem showCollisionXToolStripMenuItem;
        private ToolStripMenuItem showQuadtreeTToolStripMenuItem;
        private ToolStripMenuItem showObjectsGToolStripMenuItem;
        private ToolStripMenuItem camerasVToolStripMenuItem;
        private ToolStripMenuItem startPosYToolStripMenuItem;
        private ToolStripMenuItem splinesUToolStripMenuItem;
        private ToolStripMenuItem chunkBoxesBToolStripMenuItem;
        private ToolStripMenuItem renderByChunkHToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator4;
        private ToolStripMenuItem vSyncToolStripMenuItem;
        private ToolStripMenuItem selectionColorToolStripMenuItem;
        private ToolStripMenuItem mouseModeToolStripMenuItem;
        private ToolStripMenuItem objectsToolStripMenuItem;
        private ToolStripMenuItem camerasToolStripMenuItem;
        private ToolStripMenuItem autoLoadLastProjectOnLaunchToolStripMenuItem;
    }
}

