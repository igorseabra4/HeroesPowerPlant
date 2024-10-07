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
            menuStrip1 = new MenuStrip();
            projectToolStripMenuItem = new ToolStripMenuItem();
            openToolStripMenuItem = new ToolStripMenuItem();
            saveToolStripMenuItem = new ToolStripMenuItem();
            saveAsToolStripMenuItem = new ToolStripMenuItem();
            closeToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator1 = new ToolStripSeparator();
            aboutToolStripMenuItem = new ToolStripMenuItem();
            editorsToolStripMenuItem = new ToolStripMenuItem();
            modLoaderConfigEditorF2ToolStripMenuItem = new ToolStripMenuItem();
            levelEditorF3ToolStripMenuItem = new ToolStripMenuItem();
            collisionEditorToolStripMenuItem = new ToolStripMenuItem();
            newToolStripMenuItem = new ToolStripMenuItem();
            layoutEditorToolStripMenuItem = new ToolStripMenuItem();
            newToolStripMenuItem1 = new ToolStripMenuItem();
            cameraEditorToolStripMenuItem = new ToolStripMenuItem();
            shadowCameraEditorToolStripMenuItem = new ToolStripMenuItem();
            particleEditorF8ToolStripMenuItem = new ToolStripMenuItem();
            texturePatternEditorF9ToolStripMenuItem = new ToolStripMenuItem();
            lightEditorF10ToolStripMenuItem = new ToolStripMenuItem();
            sETIDTableEditorToolStripMenuItem = new ToolStripMenuItem();
            shadowLayoutDiffToolToolStripMenuItem = new ToolStripMenuItem();
            resourceToolStripMenuItem = new ToolStripMenuItem();
            openHeroesLevelToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator2 = new ToolStripSeparator();
            addTXDToolStripMenuItem = new ToolStripMenuItem();
            addTextureFolderToolStripMenuItem = new ToolStripMenuItem();
            clearTXDsToolStripMenuItem = new ToolStripMenuItem();
            addObjectONEToolStripMenuItem1 = new ToolStripMenuItem();
            clearObjectONEsToolStripMenuItem1 = new ToolStripMenuItem();
            ResourceToolStripMenuItemSetAFS = new ToolStripMenuItem();
            ResourceToolStripMenuItemSetFNT = new ToolStripMenuItem();
            optionsToolStripMenuItem = new ToolStripMenuItem();
            noCullingCToolStripMenuItem = new ToolStripMenuItem();
            wireframeFToolStripMenuItem = new ToolStripMenuItem();
            colorsToolStripMenuItem = new ToolStripMenuItem();
            backgroundColorToolStripMenuItem1 = new ToolStripMenuItem();
            selectionColorToolStripMenuItem1 = new ToolStripMenuItem();
            resetToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator3 = new ToolStripSeparator();
            mouseModeToolStripMenuItem = new ToolStripMenuItem();
            objectsToolStripMenuItem = new ToolStripMenuItem();
            camerasToolStripMenuItem = new ToolStripMenuItem();
            startPosToolStripMenuItem = new ToolStripMenuItem();
            splinesToolStripMenuItem = new ToolStripMenuItem();
            renderByChunkToolStripMenuItem = new ToolStripMenuItem();
            chunkBoxesToolStripMenuItem = new ToolStripMenuItem();
            showCollisionXToolStripMenuItem = new ToolStripMenuItem();
            showQuadtreeTToolStripMenuItem = new ToolStripMenuItem();
            showObjectsGToolStripMenuItem = new ToolStripMenuItem();
            camerasVToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator4 = new ToolStripSeparator();
            checkForUpdatesOnStartupToolStripMenuItem = new ToolStripMenuItem();
            checkForUpdatesNowToolStripMenuItem = new ToolStripMenuItem();
            vSyncToolStripMenuItem = new ToolStripMenuItem();
            autoLoadLastProjectOnLaunchToolStripMenuItem = new ToolStripMenuItem();
            autoSaveProjectOnClosingToolStripMenuItem = new ToolStripMenuItem();
            cameraViewSettingsToolStripMenuItem = new ToolStripMenuItem();
            disableRendering_ToolStripMenuItem = new ToolStripMenuItem();
            LimitFPS_ToolStripMenuItem = new ToolStripMenuItem();
            LegacyWindowPriorityBehavior_ToolStripMenuItem = new ToolStripMenuItem();
            statusStrip1 = new StatusStrip();
            toolStripStatusLabel1 = new ToolStripStatusLabel();
            renderPanel = new Panel();
            shadowTexturePatternEditorToolStripMenuItem = new ToolStripMenuItem();
            menuStrip1.SuspendLayout();
            statusStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] { projectToolStripMenuItem, editorsToolStripMenuItem, resourceToolStripMenuItem, optionsToolStripMenuItem });
            menuStrip1.Location = new System.Drawing.Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Padding = new Padding(7, 2, 0, 2);
            menuStrip1.Size = new System.Drawing.Size(1474, 24);
            menuStrip1.TabIndex = 2;
            menuStrip1.Text = "menuStrip1";
            // 
            // projectToolStripMenuItem
            // 
            projectToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { openToolStripMenuItem, saveToolStripMenuItem, saveAsToolStripMenuItem, closeToolStripMenuItem, toolStripSeparator1, aboutToolStripMenuItem });
            projectToolStripMenuItem.Name = "projectToolStripMenuItem";
            projectToolStripMenuItem.Size = new System.Drawing.Size(56, 20);
            projectToolStripMenuItem.Text = "Project";
            // 
            // openToolStripMenuItem
            // 
            openToolStripMenuItem.Name = "openToolStripMenuItem";
            openToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
            openToolStripMenuItem.Text = "Open";
            openToolStripMenuItem.Click += ToolstripFileOpen;
            // 
            // saveToolStripMenuItem
            // 
            saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            saveToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
            saveToolStripMenuItem.Text = "Save";
            saveToolStripMenuItem.Click += ToolStripFileSave;
            // 
            // saveAsToolStripMenuItem
            // 
            saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            saveAsToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
            saveAsToolStripMenuItem.Text = "Save As...";
            saveAsToolStripMenuItem.Click += ToolStripFileSaveAs;
            // 
            // closeToolStripMenuItem
            // 
            closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            closeToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
            closeToolStripMenuItem.Text = "Close";
            closeToolStripMenuItem.Click += closeToolStripMenuItem_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new System.Drawing.Size(120, 6);
            // 
            // aboutToolStripMenuItem
            // 
            aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            aboutToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
            aboutToolStripMenuItem.Text = "About";
            aboutToolStripMenuItem.Click += aboutToolStripMenuItem_Click;
            // 
            // editorsToolStripMenuItem
            // 
            editorsToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { modLoaderConfigEditorF2ToolStripMenuItem, levelEditorF3ToolStripMenuItem, collisionEditorToolStripMenuItem, layoutEditorToolStripMenuItem, cameraEditorToolStripMenuItem, shadowCameraEditorToolStripMenuItem, particleEditorF8ToolStripMenuItem, texturePatternEditorF9ToolStripMenuItem, shadowTexturePatternEditorToolStripMenuItem, lightEditorF10ToolStripMenuItem, sETIDTableEditorToolStripMenuItem, shadowLayoutDiffToolToolStripMenuItem });
            editorsToolStripMenuItem.Name = "editorsToolStripMenuItem";
            editorsToolStripMenuItem.Size = new System.Drawing.Size(55, 20);
            editorsToolStripMenuItem.Text = "Editors";
            // 
            // modLoaderConfigEditorF2ToolStripMenuItem
            // 
            modLoaderConfigEditorF2ToolStripMenuItem.Name = "modLoaderConfigEditorF2ToolStripMenuItem";
            modLoaderConfigEditorF2ToolStripMenuItem.Size = new System.Drawing.Size(234, 22);
            modLoaderConfigEditorF2ToolStripMenuItem.Text = "Mod Loader Config Editor (F2)";
            modLoaderConfigEditorF2ToolStripMenuItem.Click += modLoaderConfigEditorF2ToolStripMenuItem_Click;
            // 
            // levelEditorF3ToolStripMenuItem
            // 
            levelEditorF3ToolStripMenuItem.Name = "levelEditorF3ToolStripMenuItem";
            levelEditorF3ToolStripMenuItem.Size = new System.Drawing.Size(234, 22);
            levelEditorF3ToolStripMenuItem.Text = "Level Editor (F3)";
            levelEditorF3ToolStripMenuItem.Click += levelEditorF3ToolStripMenuItem_Click;
            // 
            // collisionEditorToolStripMenuItem
            // 
            collisionEditorToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { newToolStripMenuItem });
            collisionEditorToolStripMenuItem.Name = "collisionEditorToolStripMenuItem";
            collisionEditorToolStripMenuItem.Size = new System.Drawing.Size(234, 22);
            collisionEditorToolStripMenuItem.Text = "Collision Editor (F4)";
            // 
            // newToolStripMenuItem
            // 
            newToolStripMenuItem.Name = "newToolStripMenuItem";
            newToolStripMenuItem.Size = new System.Drawing.Size(98, 22);
            newToolStripMenuItem.Text = "New";
            newToolStripMenuItem.Click += newCollisionEditorToolStripMenuItem_Click;
            // 
            // layoutEditorToolStripMenuItem
            // 
            layoutEditorToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { newToolStripMenuItem1 });
            layoutEditorToolStripMenuItem.Name = "layoutEditorToolStripMenuItem";
            layoutEditorToolStripMenuItem.Size = new System.Drawing.Size(234, 22);
            layoutEditorToolStripMenuItem.Text = "Layout Editor (F5)";
            // 
            // newToolStripMenuItem1
            // 
            newToolStripMenuItem1.Name = "newToolStripMenuItem1";
            newToolStripMenuItem1.Size = new System.Drawing.Size(98, 22);
            newToolStripMenuItem1.Text = "New";
            newToolStripMenuItem1.Click += newLayoutEditorToolStripMenuItem1_Click;
            // 
            // cameraEditorToolStripMenuItem
            // 
            cameraEditorToolStripMenuItem.Name = "cameraEditorToolStripMenuItem";
            cameraEditorToolStripMenuItem.Size = new System.Drawing.Size(234, 22);
            cameraEditorToolStripMenuItem.Text = "Camera Editor (F7)";
            cameraEditorToolStripMenuItem.Click += cameraEditorToolStripMenuItem_Click;
            // 
            // shadowCameraEditorToolStripMenuItem
            // 
            shadowCameraEditorToolStripMenuItem.Name = "shadowCameraEditorToolStripMenuItem";
            shadowCameraEditorToolStripMenuItem.Size = new System.Drawing.Size(234, 22);
            shadowCameraEditorToolStripMenuItem.Text = "Shadow Camera Editor";
            shadowCameraEditorToolStripMenuItem.Click += shadowCameraEditorToolStripMenuItem_Click;
            // 
            // particleEditorF8ToolStripMenuItem
            // 
            particleEditorF8ToolStripMenuItem.Name = "particleEditorF8ToolStripMenuItem";
            particleEditorF8ToolStripMenuItem.Size = new System.Drawing.Size(234, 22);
            particleEditorF8ToolStripMenuItem.Text = "Particle Editor (F8)";
            particleEditorF8ToolStripMenuItem.Click += particleEditorF8ToolStripMenuItem_Click;
            // 
            // texturePatternEditorF9ToolStripMenuItem
            // 
            texturePatternEditorF9ToolStripMenuItem.Name = "texturePatternEditorF9ToolStripMenuItem";
            texturePatternEditorF9ToolStripMenuItem.Size = new System.Drawing.Size(234, 22);
            texturePatternEditorF9ToolStripMenuItem.Text = "Texture Pattern Editor (F9)";
            texturePatternEditorF9ToolStripMenuItem.Click += texturePatternEditorF9ToolStripMenuItem_Click;
            // 
            // lightEditorF10ToolStripMenuItem
            // 
            lightEditorF10ToolStripMenuItem.Name = "lightEditorF10ToolStripMenuItem";
            lightEditorF10ToolStripMenuItem.Size = new System.Drawing.Size(234, 22);
            lightEditorF10ToolStripMenuItem.Text = "Light Editor (F10)";
            lightEditorF10ToolStripMenuItem.Click += lightEditorF10ToolStripMenuItem_Click;
            // 
            // sETIDTableEditorToolStripMenuItem
            // 
            sETIDTableEditorToolStripMenuItem.Name = "sETIDTableEditorToolStripMenuItem";
            sETIDTableEditorToolStripMenuItem.Size = new System.Drawing.Size(234, 22);
            sETIDTableEditorToolStripMenuItem.Text = "SET ID Table Editor";
            sETIDTableEditorToolStripMenuItem.Click += sETIDTableEditorToolStripMenuItem_Click;
            // 
            // shadowLayoutDiffToolToolStripMenuItem
            // 
            shadowLayoutDiffToolToolStripMenuItem.Name = "shadowLayoutDiffToolToolStripMenuItem";
            shadowLayoutDiffToolToolStripMenuItem.Size = new System.Drawing.Size(234, 22);
            shadowLayoutDiffToolToolStripMenuItem.Text = "Shadow Layout Diff Tool";
            shadowLayoutDiffToolToolStripMenuItem.Click += shadowLayoutDiffToolToolStripMenuItem_Click;
            // 
            // resourceToolStripMenuItem
            // 
            resourceToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { openHeroesLevelToolStripMenuItem, toolStripSeparator2, addTXDToolStripMenuItem, addTextureFolderToolStripMenuItem, clearTXDsToolStripMenuItem, addObjectONEToolStripMenuItem1, clearObjectONEsToolStripMenuItem1, ResourceToolStripMenuItemSetAFS, ResourceToolStripMenuItemSetFNT });
            resourceToolStripMenuItem.Name = "resourceToolStripMenuItem";
            resourceToolStripMenuItem.Size = new System.Drawing.Size(72, 20);
            resourceToolStripMenuItem.Text = "Resources";
            // 
            // openHeroesLevelToolStripMenuItem
            // 
            openHeroesLevelToolStripMenuItem.Name = "openHeroesLevelToolStripMenuItem";
            openHeroesLevelToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            openHeroesLevelToolStripMenuItem.Text = "Open Heroes Level";
            openHeroesLevelToolStripMenuItem.Click += openHeroesLevelToolStripMenuItem_Click;
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new System.Drawing.Size(177, 6);
            // 
            // addTXDToolStripMenuItem
            // 
            addTXDToolStripMenuItem.Name = "addTXDToolStripMenuItem";
            addTXDToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            addTXDToolStripMenuItem.Text = "Add TXD(s)";
            addTXDToolStripMenuItem.Click += addTXDToolStripMenuItem_Click;
            // 
            // addTextureFolderToolStripMenuItem
            // 
            addTextureFolderToolStripMenuItem.Name = "addTextureFolderToolStripMenuItem";
            addTextureFolderToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            addTextureFolderToolStripMenuItem.Text = "Add Texture Folder";
            addTextureFolderToolStripMenuItem.Click += addTextureFolderToolStripMenuItem_Click;
            // 
            // clearTXDsToolStripMenuItem
            // 
            clearTXDsToolStripMenuItem.Name = "clearTXDsToolStripMenuItem";
            clearTXDsToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            clearTXDsToolStripMenuItem.Text = "Clear Textures";
            clearTXDsToolStripMenuItem.Click += clearTXDsToolStripMenuItem_Click;
            // 
            // addObjectONEToolStripMenuItem1
            // 
            addObjectONEToolStripMenuItem1.Name = "addObjectONEToolStripMenuItem1";
            addObjectONEToolStripMenuItem1.Size = new System.Drawing.Size(180, 22);
            addObjectONEToolStripMenuItem1.Text = "Add Object ONE";
            addObjectONEToolStripMenuItem1.Click += addObjectONEToolStripMenuItem1_Click;
            // 
            // clearObjectONEsToolStripMenuItem1
            // 
            clearObjectONEsToolStripMenuItem1.Name = "clearObjectONEsToolStripMenuItem1";
            clearObjectONEsToolStripMenuItem1.Size = new System.Drawing.Size(180, 22);
            clearObjectONEsToolStripMenuItem1.Text = "Clear Object ONEs";
            clearObjectONEsToolStripMenuItem1.Click += clearObjectONEsToolStripMenuItem1_Click;
            // 
            // ResourceToolStripMenuItemSetAFS
            // 
            ResourceToolStripMenuItemSetAFS.Name = "ResourceToolStripMenuItemSetAFS";
            ResourceToolStripMenuItemSetAFS.Size = new System.Drawing.Size(180, 22);
            ResourceToolStripMenuItemSetAFS.Text = "Set AFS";
            ResourceToolStripMenuItemSetAFS.Click += ResourceToolStripMenuItemSetAFS_Click;
            // 
            // ResourceToolStripMenuItemSetFNT
            // 
            ResourceToolStripMenuItemSetFNT.Name = "ResourceToolStripMenuItemSetFNT";
            ResourceToolStripMenuItemSetFNT.Size = new System.Drawing.Size(180, 22);
            ResourceToolStripMenuItemSetFNT.Text = "Set FNT";
            ResourceToolStripMenuItemSetFNT.Click += ResourceToolStripMenuItemSetFNT_Click;
            // 
            // optionsToolStripMenuItem
            // 
            optionsToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { noCullingCToolStripMenuItem, wireframeFToolStripMenuItem, colorsToolStripMenuItem, toolStripSeparator3, mouseModeToolStripMenuItem, startPosToolStripMenuItem, splinesToolStripMenuItem, renderByChunkToolStripMenuItem, chunkBoxesToolStripMenuItem, showCollisionXToolStripMenuItem, showQuadtreeTToolStripMenuItem, showObjectsGToolStripMenuItem, camerasVToolStripMenuItem, toolStripSeparator4, checkForUpdatesOnStartupToolStripMenuItem, checkForUpdatesNowToolStripMenuItem, vSyncToolStripMenuItem, autoLoadLastProjectOnLaunchToolStripMenuItem, autoSaveProjectOnClosingToolStripMenuItem, cameraViewSettingsToolStripMenuItem, disableRendering_ToolStripMenuItem, LimitFPS_ToolStripMenuItem, LegacyWindowPriorityBehavior_ToolStripMenuItem });
            optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            optionsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            optionsToolStripMenuItem.Text = "Options";
            // 
            // noCullingCToolStripMenuItem
            // 
            noCullingCToolStripMenuItem.Name = "noCullingCToolStripMenuItem";
            noCullingCToolStripMenuItem.Size = new System.Drawing.Size(254, 22);
            noCullingCToolStripMenuItem.Text = "No Culling (C)";
            noCullingCToolStripMenuItem.Click += noCullingCToolStripMenuItem_Click;
            // 
            // wireframeFToolStripMenuItem
            // 
            wireframeFToolStripMenuItem.Name = "wireframeFToolStripMenuItem";
            wireframeFToolStripMenuItem.Size = new System.Drawing.Size(254, 22);
            wireframeFToolStripMenuItem.Text = "Wireframe (F)";
            wireframeFToolStripMenuItem.Click += wireframeFToolStripMenuItem_Click;
            // 
            // colorsToolStripMenuItem
            // 
            colorsToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { backgroundColorToolStripMenuItem1, selectionColorToolStripMenuItem1, resetToolStripMenuItem });
            colorsToolStripMenuItem.Name = "colorsToolStripMenuItem";
            colorsToolStripMenuItem.Size = new System.Drawing.Size(254, 22);
            colorsToolStripMenuItem.Text = "Colors";
            // 
            // backgroundColorToolStripMenuItem1
            // 
            backgroundColorToolStripMenuItem1.Name = "backgroundColorToolStripMenuItem1";
            backgroundColorToolStripMenuItem1.Size = new System.Drawing.Size(179, 22);
            backgroundColorToolStripMenuItem1.Text = "Background Color...";
            backgroundColorToolStripMenuItem1.Click += BackgroundColorToolStripMenuItem_Click;
            // 
            // selectionColorToolStripMenuItem1
            // 
            selectionColorToolStripMenuItem1.Name = "selectionColorToolStripMenuItem1";
            selectionColorToolStripMenuItem1.Size = new System.Drawing.Size(179, 22);
            selectionColorToolStripMenuItem1.Text = "Selection Color...";
            selectionColorToolStripMenuItem1.Click += selectionColorToolStripMenuItem_Click;
            // 
            // resetToolStripMenuItem
            // 
            resetToolStripMenuItem.Name = "resetToolStripMenuItem";
            resetToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            resetToolStripMenuItem.Text = "Reset";
            resetToolStripMenuItem.Click += resetToolStripMenuItem_Click;
            // 
            // toolStripSeparator3
            // 
            toolStripSeparator3.Name = "toolStripSeparator3";
            toolStripSeparator3.Size = new System.Drawing.Size(251, 6);
            // 
            // mouseModeToolStripMenuItem
            // 
            mouseModeToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { objectsToolStripMenuItem, camerasToolStripMenuItem });
            mouseModeToolStripMenuItem.Name = "mouseModeToolStripMenuItem";
            mouseModeToolStripMenuItem.Size = new System.Drawing.Size(254, 22);
            mouseModeToolStripMenuItem.Text = "Selection Mode (M)";
            // 
            // objectsToolStripMenuItem
            // 
            objectsToolStripMenuItem.Checked = true;
            objectsToolStripMenuItem.CheckState = CheckState.Checked;
            objectsToolStripMenuItem.Name = "objectsToolStripMenuItem";
            objectsToolStripMenuItem.Size = new System.Drawing.Size(120, 22);
            objectsToolStripMenuItem.Text = "Objects";
            objectsToolStripMenuItem.Click += objectsToolStripMenuItem_Click;
            // 
            // camerasToolStripMenuItem
            // 
            camerasToolStripMenuItem.Name = "camerasToolStripMenuItem";
            camerasToolStripMenuItem.Size = new System.Drawing.Size(120, 22);
            camerasToolStripMenuItem.Text = "Cameras";
            camerasToolStripMenuItem.Click += camerasToolStripMenuItem_Click;
            // 
            // startPosToolStripMenuItem
            // 
            startPosToolStripMenuItem.Checked = true;
            startPosToolStripMenuItem.CheckState = CheckState.Checked;
            startPosToolStripMenuItem.Name = "startPosToolStripMenuItem";
            startPosToolStripMenuItem.Size = new System.Drawing.Size(254, 22);
            startPosToolStripMenuItem.Text = "Start Pos (Y)";
            startPosToolStripMenuItem.Click += startPosYToolStripMenuItem_Click;
            // 
            // splinesToolStripMenuItem
            // 
            splinesToolStripMenuItem.Checked = true;
            splinesToolStripMenuItem.CheckState = CheckState.Checked;
            splinesToolStripMenuItem.Name = "splinesToolStripMenuItem";
            splinesToolStripMenuItem.Size = new System.Drawing.Size(254, 22);
            splinesToolStripMenuItem.Text = "Splines (U)";
            splinesToolStripMenuItem.Click += splinesUToolStripMenuItem_Click;
            // 
            // renderByChunkToolStripMenuItem
            // 
            renderByChunkToolStripMenuItem.Checked = true;
            renderByChunkToolStripMenuItem.CheckState = CheckState.Checked;
            renderByChunkToolStripMenuItem.Name = "renderByChunkToolStripMenuItem";
            renderByChunkToolStripMenuItem.Size = new System.Drawing.Size(254, 22);
            renderByChunkToolStripMenuItem.Text = "Render By Chunk (H)";
            renderByChunkToolStripMenuItem.Click += renderByChunkHToolStripMenuItem_Click;
            // 
            // chunkBoxesToolStripMenuItem
            // 
            chunkBoxesToolStripMenuItem.Name = "chunkBoxesToolStripMenuItem";
            chunkBoxesToolStripMenuItem.Size = new System.Drawing.Size(254, 22);
            chunkBoxesToolStripMenuItem.Text = "Chunk Boxes (B)";
            chunkBoxesToolStripMenuItem.Click += chunkBoxesBToolStripMenuItem_Click;
            // 
            // showCollisionXToolStripMenuItem
            // 
            showCollisionXToolStripMenuItem.Name = "showCollisionXToolStripMenuItem";
            showCollisionXToolStripMenuItem.Size = new System.Drawing.Size(254, 22);
            showCollisionXToolStripMenuItem.Text = "Collision (X)";
            showCollisionXToolStripMenuItem.Click += showCollisionXToolStripMenuItem_Click;
            // 
            // showQuadtreeTToolStripMenuItem
            // 
            showQuadtreeTToolStripMenuItem.Name = "showQuadtreeTToolStripMenuItem";
            showQuadtreeTToolStripMenuItem.Size = new System.Drawing.Size(254, 22);
            showQuadtreeTToolStripMenuItem.Text = "Quadtree (T)";
            showQuadtreeTToolStripMenuItem.Click += showQuadtreeTToolStripMenuItem_Click;
            // 
            // showObjectsGToolStripMenuItem
            // 
            showObjectsGToolStripMenuItem.Name = "showObjectsGToolStripMenuItem";
            showObjectsGToolStripMenuItem.Size = new System.Drawing.Size(254, 22);
            showObjectsGToolStripMenuItem.Text = "Objects (G)";
            showObjectsGToolStripMenuItem.Click += showObjectsGToolStripMenuItem_Click;
            // 
            // camerasVToolStripMenuItem
            // 
            camerasVToolStripMenuItem.Checked = true;
            camerasVToolStripMenuItem.CheckState = CheckState.Checked;
            camerasVToolStripMenuItem.Name = "camerasVToolStripMenuItem";
            camerasVToolStripMenuItem.Size = new System.Drawing.Size(254, 22);
            camerasVToolStripMenuItem.Text = "Cameras (V)";
            camerasVToolStripMenuItem.Click += camerasVToolStripMenuItem_Click;
            // 
            // toolStripSeparator4
            // 
            toolStripSeparator4.Name = "toolStripSeparator4";
            toolStripSeparator4.Size = new System.Drawing.Size(251, 6);
            // 
            // checkForUpdatesOnStartupToolStripMenuItem
            // 
            checkForUpdatesOnStartupToolStripMenuItem.Checked = true;
            checkForUpdatesOnStartupToolStripMenuItem.CheckState = CheckState.Checked;
            checkForUpdatesOnStartupToolStripMenuItem.Name = "checkForUpdatesOnStartupToolStripMenuItem";
            checkForUpdatesOnStartupToolStripMenuItem.Size = new System.Drawing.Size(254, 22);
            checkForUpdatesOnStartupToolStripMenuItem.Text = "Check For Updates on Startup";
            checkForUpdatesOnStartupToolStripMenuItem.Click += CheckForUpdatesOnStartupToolStripMenuItem_Click;
            // 
            // checkForUpdatesNowToolStripMenuItem
            // 
            checkForUpdatesNowToolStripMenuItem.Name = "checkForUpdatesNowToolStripMenuItem";
            checkForUpdatesNowToolStripMenuItem.Size = new System.Drawing.Size(254, 22);
            checkForUpdatesNowToolStripMenuItem.Text = "Check For Updates Now";
            checkForUpdatesNowToolStripMenuItem.Click += CheckForUpdatesNowToolStripMenuItem_Click;
            // 
            // vSyncToolStripMenuItem
            // 
            vSyncToolStripMenuItem.Checked = true;
            vSyncToolStripMenuItem.CheckState = CheckState.Checked;
            vSyncToolStripMenuItem.Name = "vSyncToolStripMenuItem";
            vSyncToolStripMenuItem.Size = new System.Drawing.Size(254, 22);
            vSyncToolStripMenuItem.Text = "VSync";
            vSyncToolStripMenuItem.Click += vSyncToolStripMenuItem_Click;
            // 
            // autoLoadLastProjectOnLaunchToolStripMenuItem
            // 
            autoLoadLastProjectOnLaunchToolStripMenuItem.Checked = true;
            autoLoadLastProjectOnLaunchToolStripMenuItem.CheckState = CheckState.Checked;
            autoLoadLastProjectOnLaunchToolStripMenuItem.Name = "autoLoadLastProjectOnLaunchToolStripMenuItem";
            autoLoadLastProjectOnLaunchToolStripMenuItem.Size = new System.Drawing.Size(254, 22);
            autoLoadLastProjectOnLaunchToolStripMenuItem.Text = "Auto-Load Last Project on Launch";
            autoLoadLastProjectOnLaunchToolStripMenuItem.Click += autoLoadLastProjectOnLaunchToolStripMenuItem_Click;
            // 
            // autoSaveProjectOnClosingToolStripMenuItem
            // 
            autoSaveProjectOnClosingToolStripMenuItem.Name = "autoSaveProjectOnClosingToolStripMenuItem";
            autoSaveProjectOnClosingToolStripMenuItem.Size = new System.Drawing.Size(254, 22);
            autoSaveProjectOnClosingToolStripMenuItem.Text = "Auto-Save Project on Closing";
            autoSaveProjectOnClosingToolStripMenuItem.Click += autoSaveProjectOnClosingToolStripMenuItem_Click;
            // 
            // cameraViewSettingsToolStripMenuItem
            // 
            cameraViewSettingsToolStripMenuItem.Name = "cameraViewSettingsToolStripMenuItem";
            cameraViewSettingsToolStripMenuItem.Size = new System.Drawing.Size(254, 22);
            cameraViewSettingsToolStripMenuItem.Text = "Camera/View Settings";
            cameraViewSettingsToolStripMenuItem.Click += cameraViewSettingsToolStripMenuItem_Click;
            // 
            // disableRendering_ToolStripMenuItem
            // 
            disableRendering_ToolStripMenuItem.Name = "disableRendering_ToolStripMenuItem";
            disableRendering_ToolStripMenuItem.Size = new System.Drawing.Size(254, 22);
            disableRendering_ToolStripMenuItem.Text = "Disable Rendering";
            disableRendering_ToolStripMenuItem.Click += disableRendering_ToolStripMenuItem_Click;
            // 
            // LimitFPS_ToolStripMenuItem
            // 
            LimitFPS_ToolStripMenuItem.Name = "LimitFPS_ToolStripMenuItem";
            LimitFPS_ToolStripMenuItem.Size = new System.Drawing.Size(254, 22);
            LimitFPS_ToolStripMenuItem.Text = "Limit FPS (Set In View Settings)";
            LimitFPS_ToolStripMenuItem.Click += LimitFPS_ToolStripMenuItem_Click;
            // 
            // LegacyWindowPriorityBehavior_ToolStripMenuItem
            // 
            LegacyWindowPriorityBehavior_ToolStripMenuItem.Name = "LegacyWindowPriorityBehavior_ToolStripMenuItem";
            LegacyWindowPriorityBehavior_ToolStripMenuItem.Size = new System.Drawing.Size(254, 22);
            LegacyWindowPriorityBehavior_ToolStripMenuItem.Text = "Legacy Window Priority Behavior";
            LegacyWindowPriorityBehavior_ToolStripMenuItem.Click += LegacyWindowPriorityBehavior_ToolStripMenuItem_Click;
            // 
            // statusStrip1
            // 
            statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            statusStrip1.Items.AddRange(new ToolStripItem[] { toolStripStatusLabel1 });
            statusStrip1.Location = new System.Drawing.Point(0, 769);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Padding = new Padding(1, 0, 16, 0);
            statusStrip1.Size = new System.Drawing.Size(1474, 22);
            statusStrip1.TabIndex = 3;
            statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            toolStripStatusLabel1.Size = new System.Drawing.Size(0, 17);
            toolStripStatusLabel1.Click += toolStripStatusLabel1_Click;
            // 
            // renderPanel
            // 
            renderPanel.Dock = DockStyle.Fill;
            renderPanel.Location = new System.Drawing.Point(0, 24);
            renderPanel.Margin = new Padding(4, 3, 4, 3);
            renderPanel.Name = "renderPanel";
            renderPanel.Size = new System.Drawing.Size(1474, 745);
            renderPanel.TabIndex = 4;
            renderPanel.MouseClick += renderPanel_MouseClick;
            renderPanel.MouseDown += renderPanel_MouseDown;
            renderPanel.MouseLeave += renderPanel_MouseLeave;
            renderPanel.MouseMove += MouseMoveControl;
            renderPanel.MouseUp += renderPanel_MouseUp;
            renderPanel.MouseWheel += renderPanel_MouseWheel;
            renderPanel.Resize += ResetMouseCenter;
            // 
            // shadowTexturePatternEditorToolStripMenuItem
            // 
            shadowTexturePatternEditorToolStripMenuItem.Name = "shadowTexturePatternEditorToolStripMenuItem";
            shadowTexturePatternEditorToolStripMenuItem.Size = new System.Drawing.Size(234, 22);
            shadowTexturePatternEditorToolStripMenuItem.Text = "Shadow Texture Pattern Editor";
            shadowTexturePatternEditorToolStripMenuItem.Click += shadowTexturePatternEditorToolStripMenuItem_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoValidate = AutoValidate.EnableAllowFocusChange;
            ClientSize = new System.Drawing.Size(1474, 791);
            Controls.Add(renderPanel);
            Controls.Add(statusStrip1);
            Controls.Add(menuStrip1);
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            KeyPreview = true;
            Margin = new Padding(4, 3, 4, 3);
            Name = "MainForm";
            Text = "Heroes Power Plant 2024.10.05";
            Deactivate += MainForm_Deactivate;
            FormClosing += MainForm_FormClosing;
            Load += MainForm_Load;
            KeyDown += MainForm_KeyDown;
            KeyUp += MainForm_KeyUp;
            MouseWheel += MouseMoveControl;
            Move += ResetMouseCenter;
            Resize += MainForm_Resize;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
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
        private ToolStripMenuItem aboutToolStripMenuItem;
        private ToolStripMenuItem editorsToolStripMenuItem;
        private ToolStripMenuItem modLoaderConfigEditorF2ToolStripMenuItem;
        private ToolStripMenuItem levelEditorF3ToolStripMenuItem;
        private ToolStripMenuItem collisionEditorToolStripMenuItem;
        private ToolStripMenuItem layoutEditorToolStripMenuItem;
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
        private ToolStripMenuItem closeToolStripMenuItem;
        private ToolStripMenuItem resourceToolStripMenuItem;
        private ToolStripMenuItem addTXDToolStripMenuItem;
        private ToolStripMenuItem clearTXDsToolStripMenuItem;
        private ToolStripMenuItem addObjectONEToolStripMenuItem1;
        private ToolStripMenuItem clearObjectONEsToolStripMenuItem1;
        private ToolStripMenuItem addTextureFolderToolStripMenuItem;
        private ToolStripMenuItem autoSaveProjectOnClosingToolStripMenuItem;
        private ToolStripMenuItem sETIDTableEditorToolStripMenuItem;
        private ToolStripMenuItem cameraViewSettingsToolStripMenuItem;
        private ToolStripMenuItem lightEditorF10ToolStripMenuItem;
        private ToolStripMenuItem newToolStripMenuItem;
        private ToolStripMenuItem newToolStripMenuItem1;
        private ToolStripMenuItem checkForUpdatesOnStartupToolStripMenuItem;
        private ToolStripMenuItem checkForUpdatesNowToolStripMenuItem;
        private ToolStripMenuItem shadowCameraEditorToolStripMenuItem;
        private ToolStripMenuItem disableRendering_ToolStripMenuItem;
        private ToolStripMenuItem LimitFPS_ToolStripMenuItem;
        private ToolStripMenuItem shadowLayoutDiffToolToolStripMenuItem;
        private ToolStripMenuItem LegacyWindowPriorityBehavior_ToolStripMenuItem;
        private ToolStripMenuItem ResourceToolStripMenuItemSetAFS;
        private ToolStripMenuItem ResourceToolStripMenuItemSetFNT;
        private ToolStripMenuItem openHeroesLevelToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripMenuItem shadowTexturePatternEditorToolStripMenuItem;
    }
}

