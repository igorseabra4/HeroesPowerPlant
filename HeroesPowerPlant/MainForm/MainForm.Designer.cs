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
            Reloaded_GUI.Styles.Animation.AnimProperties animProperties1 = new Reloaded_GUI.Styles.Animation.AnimProperties();
            Reloaded_GUI.Styles.Animation.AnimMessage animMessage1 = new Reloaded_GUI.Styles.Animation.AnimMessage();
            Reloaded_GUI.Styles.Animation.AnimMessage animMessage2 = new Reloaded_GUI.Styles.Animation.AnimMessage();
            Reloaded_GUI.Styles.Animation.AnimProperties animProperties2 = new Reloaded_GUI.Styles.Animation.AnimProperties();
            Reloaded_GUI.Styles.Animation.AnimMessage animMessage3 = new Reloaded_GUI.Styles.Animation.AnimMessage();
            Reloaded_GUI.Styles.Animation.AnimMessage animMessage4 = new Reloaded_GUI.Styles.Animation.AnimMessage();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.categoryBar_Close = new Reloaded_GUI.Styles.Controls.Animated.AnimatedButton();
            this.titleBar_Title = new Reloaded_GUI.Styles.Controls.Animated.AnimatedButton();
            this.categoryBar_MenuStrip = new System.Windows.Forms.MenuStrip();
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
            this.graphicsModeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.vSyncToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.categoryBar_StatusStrip = new System.Windows.Forms.StatusStrip();
            this.categoryBar_ToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.renderPanel = new System.Windows.Forms.Panel();
            this.titleBar_Panel = new System.Windows.Forms.Panel();
            this.categoryBar_MenuStrip.SuspendLayout();
            this.categoryBar_StatusStrip.SuspendLayout();
            this.titleBar_Panel.SuspendLayout();
            this.SuspendLayout();
            // 
            // categoryBar_Close
            // 
            animMessage1.Control = this.categoryBar_Close;
            animMessage1.PlayAnimation = true;
            animProperties1.BackColorMessage = animMessage1;
            animMessage2.Control = this.categoryBar_Close;
            animMessage2.PlayAnimation = true;
            animProperties1.ForeColorMessage = animMessage2;
            animProperties1.MouseEnterBackColor = System.Drawing.Color.Empty;
            animProperties1.MouseEnterDuration = 0F;
            animProperties1.MouseEnterForeColor = System.Drawing.Color.Empty;
            animProperties1.MouseEnterFramerate = 0F;
            animProperties1.MouseEnterOverride = Reloaded_GUI.Styles.Animation.AnimOverrides.MouseEnterOverride.None;
            animProperties1.MouseLeaveBackColor = System.Drawing.Color.Empty;
            animProperties1.MouseLeaveDuration = 0F;
            animProperties1.MouseLeaveForeColor = System.Drawing.Color.Empty;
            animProperties1.MouseLeaveFramerate = 0F;
            animProperties1.MouseLeaveOverride = Reloaded_GUI.Styles.Animation.AnimOverrides.MouseLeaveOverride.None;
            this.categoryBar_Close.AnimProperties = animProperties1;
            this.categoryBar_Close.BackColor = System.Drawing.Color.Transparent;
            this.categoryBar_Close.CaptureChildren = false;
            this.categoryBar_Close.Dock = System.Windows.Forms.DockStyle.Right;
            this.categoryBar_Close.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.categoryBar_Close.FlatAppearance.BorderSize = 0;
            this.categoryBar_Close.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.categoryBar_Close.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.categoryBar_Close.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.categoryBar_Close.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.categoryBar_Close.ForeColor = System.Drawing.Color.White;
            this.categoryBar_Close.IgnoreMouse = false;
            this.categoryBar_Close.IgnoreMouseClicks = false;
            this.categoryBar_Close.Location = new System.Drawing.Point(1233, 0);
            this.categoryBar_Close.Name = "categoryBar_Close";
            this.categoryBar_Close.Size = new System.Drawing.Size(30, 32);
            this.categoryBar_Close.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            this.categoryBar_Close.TabIndex = 52;
            this.categoryBar_Close.Text = "X";
            this.categoryBar_Close.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            this.categoryBar_Close.UseVisualStyleBackColor = false;
            this.categoryBar_Close.Click += new System.EventHandler(this.categoryBar_Close_Click);
            // 
            // titleBar_Title
            // 
            animMessage3.Control = this.titleBar_Title;
            animMessage3.PlayAnimation = true;
            animProperties2.BackColorMessage = animMessage3;
            animMessage4.Control = this.titleBar_Title;
            animMessage4.PlayAnimation = true;
            animProperties2.ForeColorMessage = animMessage4;
            animProperties2.MouseEnterBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(53)))), ((int)(((byte)(64)))));
            animProperties2.MouseEnterDuration = 200F;
            animProperties2.MouseEnterForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(163)))), ((int)(((byte)(244)))));
            animProperties2.MouseEnterFramerate = 144F;
            animProperties2.MouseEnterOverride = Reloaded_GUI.Styles.Animation.AnimOverrides.MouseEnterOverride.None;
            animProperties2.MouseLeaveBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(53)))), ((int)(((byte)(64)))));
            animProperties2.MouseLeaveDuration = 200F;
            animProperties2.MouseLeaveForeColor = System.Drawing.Color.White;
            animProperties2.MouseLeaveFramerate = 144F;
            animProperties2.MouseLeaveOverride = Reloaded_GUI.Styles.Animation.AnimOverrides.MouseLeaveOverride.None;
            this.titleBar_Title.AnimProperties = animProperties2;
            this.titleBar_Title.CaptureChildren = false;
            this.titleBar_Title.Dock = System.Windows.Forms.DockStyle.Fill;
            this.titleBar_Title.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.titleBar_Title.FlatAppearance.BorderSize = 0;
            this.titleBar_Title.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.titleBar_Title.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.titleBar_Title.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.titleBar_Title.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.titleBar_Title.ForeColor = System.Drawing.Color.White;
            this.titleBar_Title.IgnoreMouse = false;
            this.titleBar_Title.IgnoreMouseClicks = false;
            this.titleBar_Title.Location = new System.Drawing.Point(0, 0);
            this.titleBar_Title.Name = "titleBar_Title";
            this.titleBar_Title.Size = new System.Drawing.Size(1263, 32);
            this.titleBar_Title.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            this.titleBar_Title.TabIndex = 53;
            this.titleBar_Title.Text = "Heroes Power Plant";
            this.titleBar_Title.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
            this.titleBar_Title.UseVisualStyleBackColor = true;
            // 
            // categoryBar_MenuStrip
            // 
            this.categoryBar_MenuStrip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(41)))), ((int)(((byte)(56)))));
            this.categoryBar_MenuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.categoryBar_MenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.projectToolStripMenuItem,
            this.editorsToolStripMenuItem,
            this.optionsToolStripMenuItem});
            this.categoryBar_MenuStrip.Location = new System.Drawing.Point(0, 32);
            this.categoryBar_MenuStrip.Name = "categoryBar_MenuStrip";
            this.categoryBar_MenuStrip.Size = new System.Drawing.Size(1263, 24);
            this.categoryBar_MenuStrip.TabIndex = 3;
            this.categoryBar_MenuStrip.Text = "menuStrip1";
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
            this.projectToolStripMenuItem.ForeColor = System.Drawing.Color.Silver;
            this.projectToolStripMenuItem.Name = "projectToolStripMenuItem";
            this.projectToolStripMenuItem.Size = new System.Drawing.Size(56, 20);
            this.projectToolStripMenuItem.Text = "Project";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.saveAsToolStripMenuItem.Text = "Save As...";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
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
            this.cameraEditorToolStripMenuItem});
            this.editorsToolStripMenuItem.ForeColor = System.Drawing.Color.Silver;
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
            this.graphicsModeToolStripMenuItem,
            this.vSyncToolStripMenuItem});
            this.optionsToolStripMenuItem.ForeColor = System.Drawing.Color.Silver;
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.optionsToolStripMenuItem.Text = "Options";
            // 
            // noCullingCToolStripMenuItem
            // 
            this.noCullingCToolStripMenuItem.Name = "noCullingCToolStripMenuItem";
            this.noCullingCToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
            this.noCullingCToolStripMenuItem.Text = "No Culling (C)";
            this.noCullingCToolStripMenuItem.Click += new System.EventHandler(this.noCullingCToolStripMenuItem_Click);
            // 
            // wireframeFToolStripMenuItem
            // 
            this.wireframeFToolStripMenuItem.Name = "wireframeFToolStripMenuItem";
            this.wireframeFToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
            this.wireframeFToolStripMenuItem.Text = "Wireframe (F)";
            this.wireframeFToolStripMenuItem.Click += new System.EventHandler(this.wireframeFToolStripMenuItem_Click);
            // 
            // BackgroundColorToolStripMenuItem
            // 
            this.BackgroundColorToolStripMenuItem.Name = "BackgroundColorToolStripMenuItem";
            this.BackgroundColorToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
            this.BackgroundColorToolStripMenuItem.Text = "Background Color...";
            this.BackgroundColorToolStripMenuItem.Click += new System.EventHandler(this.BackgroundColorToolStripMenuItem_Click);
            // 
            // selectionColorToolStripMenuItem
            // 
            this.selectionColorToolStripMenuItem.Name = "selectionColorToolStripMenuItem";
            this.selectionColorToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
            this.selectionColorToolStripMenuItem.Text = "Selection Color...";
            this.selectionColorToolStripMenuItem.Click += new System.EventHandler(this.selectionColorToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(182, 6);
            // 
            // mouseModeToolStripMenuItem
            // 
            this.mouseModeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.objectsToolStripMenuItem,
            this.camerasToolStripMenuItem});
            this.mouseModeToolStripMenuItem.Name = "mouseModeToolStripMenuItem";
            this.mouseModeToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
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
            this.startPosYToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
            this.startPosYToolStripMenuItem.Text = "Start Pos (Y)";
            this.startPosYToolStripMenuItem.Click += new System.EventHandler(this.startPosYToolStripMenuItem_Click);
            // 
            // splinesUToolStripMenuItem
            // 
            this.splinesUToolStripMenuItem.Checked = true;
            this.splinesUToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.splinesUToolStripMenuItem.Name = "splinesUToolStripMenuItem";
            this.splinesUToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
            this.splinesUToolStripMenuItem.Text = "Splines (U)";
            this.splinesUToolStripMenuItem.Click += new System.EventHandler(this.splinesUToolStripMenuItem_Click);
            // 
            // renderByChunkHToolStripMenuItem
            // 
            this.renderByChunkHToolStripMenuItem.Checked = true;
            this.renderByChunkHToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.renderByChunkHToolStripMenuItem.Name = "renderByChunkHToolStripMenuItem";
            this.renderByChunkHToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
            this.renderByChunkHToolStripMenuItem.Text = "Render By Chunk (H)";
            this.renderByChunkHToolStripMenuItem.Click += new System.EventHandler(this.renderByChunkHToolStripMenuItem_Click);
            // 
            // chunkBoxesBToolStripMenuItem
            // 
            this.chunkBoxesBToolStripMenuItem.Name = "chunkBoxesBToolStripMenuItem";
            this.chunkBoxesBToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
            this.chunkBoxesBToolStripMenuItem.Text = "Chunk Boxes (B)";
            this.chunkBoxesBToolStripMenuItem.Click += new System.EventHandler(this.chunkBoxesBToolStripMenuItem_Click);
            // 
            // showCollisionXToolStripMenuItem
            // 
            this.showCollisionXToolStripMenuItem.Name = "showCollisionXToolStripMenuItem";
            this.showCollisionXToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
            this.showCollisionXToolStripMenuItem.Text = "Collision (X)";
            this.showCollisionXToolStripMenuItem.Click += new System.EventHandler(this.showCollisionXToolStripMenuItem_Click);
            // 
            // showQuadtreeTToolStripMenuItem
            // 
            this.showQuadtreeTToolStripMenuItem.Name = "showQuadtreeTToolStripMenuItem";
            this.showQuadtreeTToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
            this.showQuadtreeTToolStripMenuItem.Text = "Quadtree (T)";
            this.showQuadtreeTToolStripMenuItem.Click += new System.EventHandler(this.showQuadtreeTToolStripMenuItem_Click);
            // 
            // showObjectsGToolStripMenuItem
            // 
            this.showObjectsGToolStripMenuItem.Name = "showObjectsGToolStripMenuItem";
            this.showObjectsGToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
            this.showObjectsGToolStripMenuItem.Text = "Objects (G)";
            this.showObjectsGToolStripMenuItem.Click += new System.EventHandler(this.showObjectsGToolStripMenuItem_Click);
            // 
            // camerasVToolStripMenuItem
            // 
            this.camerasVToolStripMenuItem.Checked = true;
            this.camerasVToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.camerasVToolStripMenuItem.Name = "camerasVToolStripMenuItem";
            this.camerasVToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
            this.camerasVToolStripMenuItem.Text = "Cameras (V)";
            this.camerasVToolStripMenuItem.Click += new System.EventHandler(this.camerasVToolStripMenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(182, 6);
            // 
            // graphicsModeToolStripMenuItem
            // 
            this.graphicsModeToolStripMenuItem.Checked = true;
            this.graphicsModeToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.graphicsModeToolStripMenuItem.Name = "graphicsModeToolStripMenuItem";
            this.graphicsModeToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
            this.graphicsModeToolStripMenuItem.Text = "Graphics Mode";
            this.graphicsModeToolStripMenuItem.Click += new System.EventHandler(this.graphicsModeToolStripMenuItem_Click);
            // 
            // vSyncToolStripMenuItem
            // 
            this.vSyncToolStripMenuItem.Checked = true;
            this.vSyncToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.vSyncToolStripMenuItem.Name = "vSyncToolStripMenuItem";
            this.vSyncToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
            this.vSyncToolStripMenuItem.Text = "VSync";
            this.vSyncToolStripMenuItem.Click += new System.EventHandler(this.vSyncToolStripMenuItem_Click);
            // 
            // categoryBar_StatusStrip
            // 
            this.categoryBar_StatusStrip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(41)))), ((int)(((byte)(56)))));
            this.categoryBar_StatusStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.categoryBar_StatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.categoryBar_ToolStripStatusLabel});
            this.categoryBar_StatusStrip.Location = new System.Drawing.Point(0, 816);
            this.categoryBar_StatusStrip.Name = "categoryBar_StatusStrip";
            this.categoryBar_StatusStrip.Size = new System.Drawing.Size(1263, 22);
            this.categoryBar_StatusStrip.SizingGrip = false;
            this.categoryBar_StatusStrip.TabIndex = 3;
            this.categoryBar_StatusStrip.Resize += new System.EventHandler(this.categoryBar_StatusStrip_Resize);
            // 
            // categoryBar_ToolStripStatusLabel
            // 
            this.categoryBar_ToolStripStatusLabel.ForeColor = System.Drawing.Color.Silver;
            this.categoryBar_ToolStripStatusLabel.Name = "categoryBar_ToolStripStatusLabel";
            this.categoryBar_ToolStripStatusLabel.Size = new System.Drawing.Size(0, 17);
            this.categoryBar_ToolStripStatusLabel.Click += new System.EventHandler(this.toolStripStatusLabel1_Click);
            // 
            // renderPanel
            // 
            this.renderPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.renderPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.renderPanel.Location = new System.Drawing.Point(0, 56);
            this.renderPanel.Name = "renderPanel";
            this.renderPanel.Size = new System.Drawing.Size(1263, 760);
            this.renderPanel.TabIndex = 4;
            this.renderPanel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.renderPanel_MouseClick);
            this.renderPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MouseMoveControl);
            this.renderPanel.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.renderPanel_MouseWheel);
            this.renderPanel.Resize += new System.EventHandler(this.ResetMouseCenter);
            // 
            // titleBar_Panel
            // 
            this.titleBar_Panel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(53)))), ((int)(((byte)(64)))));
            this.titleBar_Panel.Controls.Add(this.categoryBar_Close);
            this.titleBar_Panel.Controls.Add(this.titleBar_Title);
            this.titleBar_Panel.Dock = System.Windows.Forms.DockStyle.Top;
            this.titleBar_Panel.Location = new System.Drawing.Point(0, 0);
            this.titleBar_Panel.Name = "titleBar_Panel";
            this.titleBar_Panel.Size = new System.Drawing.Size(1263, 32);
            this.titleBar_Panel.TabIndex = 1;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1263, 838);
            this.Controls.Add(this.renderPanel);
            this.Controls.Add(this.categoryBar_MenuStrip);
            this.Controls.Add(this.titleBar_Panel);
            this.Controls.Add(this.categoryBar_StatusStrip);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "MainForm";
            this.Text = "Heroes Power Plant";
            this.Deactivate += new System.EventHandler(this.MainForm_Deactivate);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyUp);
            this.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.MouseMoveControl);
            this.Move += new System.EventHandler(this.ResetMouseCenter);
            this.categoryBar_MenuStrip.ResumeLayout(false);
            this.categoryBar_MenuStrip.PerformLayout();
            this.categoryBar_StatusStrip.ResumeLayout(false);
            this.categoryBar_StatusStrip.PerformLayout();
            this.titleBar_Panel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip categoryBar_MenuStrip;
        private System.Windows.Forms.StatusStrip categoryBar_StatusStrip;
        private Panel renderPanel;
        private ToolStripStatusLabel categoryBar_ToolStripStatusLabel;
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
        private ToolStripMenuItem graphicsModeToolStripMenuItem;
        private ToolStripMenuItem vSyncToolStripMenuItem;
        private ToolStripMenuItem selectionColorToolStripMenuItem;
        private ToolStripMenuItem mouseModeToolStripMenuItem;
        private ToolStripMenuItem objectsToolStripMenuItem;
        private ToolStripMenuItem camerasToolStripMenuItem;
        private Panel titleBar_Panel;
        private Reloaded_GUI.Styles.Controls.Animated.AnimatedButton categoryBar_Close;
        private Reloaded_GUI.Styles.Controls.Animated.AnimatedButton titleBar_Title;
    }
}

