using System.Windows.Forms;

namespace HeroesPowerPlant.MainForm
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
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
            this.particleEditorF8ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.texturePatternEditorF9ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.noCullingCToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.wireframeFToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.colorsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.backgroundColorToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.selectionColorToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.resetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.mouseModeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.objectsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.camerasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startPosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splinesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.renderByChunkToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.chunkBoxesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showCollisionXToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showQuadtreeTToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showObjectsGToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.camerasVToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.vSyncToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.autoLoadLastProjectOnLaunchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.debugToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reloadTexturesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.renderPanel = new System.Windows.Forms.Panel();
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
            this.optionsToolStripMenuItem,
            this.debugToolStripMenuItem});
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
            this.closeToolStripMenuItem,
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
            this.openToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.ToolstripFileOpen);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.ToolStripFileSave);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.saveAsToolStripMenuItem.Text = "Save As...";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.ToolStripFileSaveAs);
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.closeToolStripMenuItem.Text = "Close";
            this.closeToolStripMenuItem.Click += new System.EventHandler(this.closeToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(168, 6);
            // 
            // addObjectONEToolStripMenuItem
            // 
            this.addObjectONEToolStripMenuItem.Name = "addObjectONEToolStripMenuItem";
            this.addObjectONEToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.addObjectONEToolStripMenuItem.Text = "Add Object ONE";
            this.addObjectONEToolStripMenuItem.Click += new System.EventHandler(this.addObjectONEToolStripMenuItem_Click);
            // 
            // clearObjectONEsToolStripMenuItem
            // 
            this.clearObjectONEsToolStripMenuItem.Name = "clearObjectONEsToolStripMenuItem";
            this.clearObjectONEsToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.clearObjectONEsToolStripMenuItem.Text = "Clear Object ONEs";
            this.clearObjectONEsToolStripMenuItem.Click += new System.EventHandler(this.clearObjectONEsToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(168, 6);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
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
            this.cameraEditorToolStripMenuItem,
            this.particleEditorF8ToolStripMenuItem,
            this.texturePatternEditorF9ToolStripMenuItem});
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
            // particleEditorF8ToolStripMenuItem
            // 
            this.particleEditorF8ToolStripMenuItem.Name = "particleEditorF8ToolStripMenuItem";
            this.particleEditorF8ToolStripMenuItem.Size = new System.Drawing.Size(234, 22);
            this.particleEditorF8ToolStripMenuItem.Text = "Particle Editor (F8)";
            this.particleEditorF8ToolStripMenuItem.Click += new System.EventHandler(this.particleEditorF8ToolStripMenuItem_Click);
            // 
            // texturePatternEditorF9ToolStripMenuItem
            // 
            this.texturePatternEditorF9ToolStripMenuItem.Name = "texturePatternEditorF9ToolStripMenuItem";
            this.texturePatternEditorF9ToolStripMenuItem.Size = new System.Drawing.Size(234, 22);
            this.texturePatternEditorF9ToolStripMenuItem.Text = "Texture Pattern Editor (F9)";
            this.texturePatternEditorF9ToolStripMenuItem.Click += new System.EventHandler(this.texturePatternEditorF9ToolStripMenuItem_Click);
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.noCullingCToolStripMenuItem,
            this.wireframeFToolStripMenuItem,
            this.colorsToolStripMenuItem,
            this.toolStripSeparator3,
            this.mouseModeToolStripMenuItem,
            this.startPosToolStripMenuItem,
            this.splinesToolStripMenuItem,
            this.renderByChunkToolStripMenuItem,
            this.chunkBoxesToolStripMenuItem,
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
            // colorsToolStripMenuItem
            // 
            this.colorsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.backgroundColorToolStripMenuItem1,
            this.selectionColorToolStripMenuItem1,
            this.resetToolStripMenuItem});
            this.colorsToolStripMenuItem.Name = "colorsToolStripMenuItem";
            this.colorsToolStripMenuItem.Size = new System.Drawing.Size(254, 22);
            this.colorsToolStripMenuItem.Text = "Colors";
            // 
            // backgroundColorToolStripMenuItem1
            // 
            this.backgroundColorToolStripMenuItem1.Name = "backgroundColorToolStripMenuItem1";
            this.backgroundColorToolStripMenuItem1.Size = new System.Drawing.Size(179, 22);
            this.backgroundColorToolStripMenuItem1.Text = "Background Color...";
            this.backgroundColorToolStripMenuItem1.Click += new System.EventHandler(this.BackgroundColorToolStripMenuItem_Click);
            // 
            // selectionColorToolStripMenuItem1
            // 
            this.selectionColorToolStripMenuItem1.Name = "selectionColorToolStripMenuItem1";
            this.selectionColorToolStripMenuItem1.Size = new System.Drawing.Size(179, 22);
            this.selectionColorToolStripMenuItem1.Text = "Selection Color...";
            this.selectionColorToolStripMenuItem1.Click += new System.EventHandler(this.selectionColorToolStripMenuItem_Click);
            // 
            // resetToolStripMenuItem
            // 
            this.resetToolStripMenuItem.Name = "resetToolStripMenuItem";
            this.resetToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.resetToolStripMenuItem.Text = "Reset";
            this.resetToolStripMenuItem.Click += new System.EventHandler(this.resetToolStripMenuItem_Click);
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
            // startPosToolStripMenuItem
            // 
            this.startPosToolStripMenuItem.Checked = true;
            this.startPosToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.startPosToolStripMenuItem.Name = "startPosToolStripMenuItem";
            this.startPosToolStripMenuItem.Size = new System.Drawing.Size(254, 22);
            this.startPosToolStripMenuItem.Text = "Start Pos (Y)";
            this.startPosToolStripMenuItem.Click += new System.EventHandler(this.startPosYToolStripMenuItem_Click);
            // 
            // splinesToolStripMenuItem
            // 
            this.splinesToolStripMenuItem.Checked = true;
            this.splinesToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.splinesToolStripMenuItem.Name = "splinesToolStripMenuItem";
            this.splinesToolStripMenuItem.Size = new System.Drawing.Size(254, 22);
            this.splinesToolStripMenuItem.Text = "Splines (U)";
            this.splinesToolStripMenuItem.Click += new System.EventHandler(this.splinesUToolStripMenuItem_Click);
            // 
            // renderByChunkToolStripMenuItem
            // 
            this.renderByChunkToolStripMenuItem.Checked = true;
            this.renderByChunkToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.renderByChunkToolStripMenuItem.Name = "renderByChunkToolStripMenuItem";
            this.renderByChunkToolStripMenuItem.Size = new System.Drawing.Size(254, 22);
            this.renderByChunkToolStripMenuItem.Text = "Render By Chunk (H)";
            this.renderByChunkToolStripMenuItem.Click += new System.EventHandler(this.renderByChunkHToolStripMenuItem_Click);
            // 
            // chunkBoxesToolStripMenuItem
            // 
            this.chunkBoxesToolStripMenuItem.Name = "chunkBoxesToolStripMenuItem";
            this.chunkBoxesToolStripMenuItem.Size = new System.Drawing.Size(254, 22);
            this.chunkBoxesToolStripMenuItem.Text = "Chunk Boxes (B)";
            this.chunkBoxesToolStripMenuItem.Click += new System.EventHandler(this.chunkBoxesBToolStripMenuItem_Click);
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
            // autoLoadLastProjectOnLaunchToolStripMenuItem
            // 
            this.autoLoadLastProjectOnLaunchToolStripMenuItem.Checked = true;
            this.autoLoadLastProjectOnLaunchToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.autoLoadLastProjectOnLaunchToolStripMenuItem.Name = "autoLoadLastProjectOnLaunchToolStripMenuItem";
            this.autoLoadLastProjectOnLaunchToolStripMenuItem.Size = new System.Drawing.Size(254, 22);
            this.autoLoadLastProjectOnLaunchToolStripMenuItem.Text = "Auto-Load Last Project on Launch";
            this.autoLoadLastProjectOnLaunchToolStripMenuItem.Click += new System.EventHandler(this.autoLoadLastProjectOnLaunchToolStripMenuItem_Click);
            // 
            // debugToolStripMenuItem
            // 
            this.debugToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.reloadTexturesToolStripMenuItem});
            this.debugToolStripMenuItem.Name = "debugToolStripMenuItem";
            this.debugToolStripMenuItem.Size = new System.Drawing.Size(54, 20);
            this.debugToolStripMenuItem.Text = "Debug";
            // 
            // reloadTexturesToolStripMenuItem
            // 
            this.reloadTexturesToolStripMenuItem.Name = "reloadTexturesToolStripMenuItem";
            this.reloadTexturesToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.reloadTexturesToolStripMenuItem.Text = "Reload Textures";
            this.reloadTexturesToolStripMenuItem.Click += new System.EventHandler(this.reloadTexturesToolStripMenuItem_Click);
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
        private ToolStripSeparator toolStripSeparator3;
        private ToolStripMenuItem showCollisionXToolStripMenuItem;
        private ToolStripMenuItem showQuadtreeTToolStripMenuItem;
        private ToolStripMenuItem showObjectsGToolStripMenuItem;
        private ToolStripMenuItem camerasVToolStripMenuItem;
        private ToolStripMenuItem startPosToolStripMenuItem;
        private ToolStripMenuItem splinesToolStripMenuItem;
        private ToolStripMenuItem chunkBoxesToolStripMenuItem;
        private ToolStripMenuItem renderByChunkToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator4;
        private ToolStripMenuItem vSyncToolStripMenuItem;
        private ToolStripMenuItem mouseModeToolStripMenuItem;
        private ToolStripMenuItem objectsToolStripMenuItem;
        private ToolStripMenuItem camerasToolStripMenuItem;
        private ToolStripMenuItem autoLoadLastProjectOnLaunchToolStripMenuItem;
        private ToolStripMenuItem particleEditorF8ToolStripMenuItem;
        private ToolStripMenuItem texturePatternEditorF9ToolStripMenuItem;
        private ToolStripMenuItem colorsToolStripMenuItem;
        private ToolStripMenuItem backgroundColorToolStripMenuItem1;
        private ToolStripMenuItem selectionColorToolStripMenuItem1;
        private ToolStripMenuItem resetToolStripMenuItem;
        private ToolStripMenuItem debugToolStripMenuItem;
        private ToolStripMenuItem reloadTexturesToolStripMenuItem;
        private ToolStripMenuItem closeToolStripMenuItem;
    }
}

