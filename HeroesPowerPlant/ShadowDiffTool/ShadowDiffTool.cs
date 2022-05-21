using Ookii.Dialogs.WinForms;
using System;
using System.IO;
using System.Windows.Forms;
using HeroesPowerPlant.LayoutEditor;

namespace HeroesPowerPlant.ShadowDiffTool
{
    public partial class ShadowDiffTool : Form
    {
        public ShadowDiffTool()
        {
            InitializeComponent();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.WindowsShutDown) return;
            if (e.CloseReason == CloseReason.FormOwnerClosing) return;

            e.Cancel = true;
            Hide();
        }

        public void New()
        {
    
        }

        // TODO: Expand this feature to eventually check .cam, .one for spline changes, visibility changes, and geo/coli changes
        private void buttonDiff_Click(object sender, EventArgs e)
        {
            // Parse folder 1 for .cmn .nrm .hrd .ds1
            VistaFolderBrowserDialog openFolder = new VistaFolderBrowserDialog();
            var folder1 = "";
            var folder1_name = "";
            if (openFolder.ShowDialog() == DialogResult.OK)
            {
                folder1_name = openFolder.SelectedPath;
                folder1 = Path.GetFileNameWithoutExtension(openFolder.SelectedPath);
            }

            var layoutSystem1 = new LayoutEditorSystem
            {
                autoUnkBytes = false
            };

            foreach (string s in new string[]
            {
                    //Path.Combine(folder1_name, folder1) + "_ds1.dat",
                    Path.Combine(folder1_name, folder1) + "_cmn.dat",
                    //Path.Combine(folder1_name, folder1) + "_nrm.dat",
                    //Path.Combine(folder1_name, folder1) + "_hrd.dat"
            }) if (File.Exists(s))
                {
                    layoutSystem1.OpenLayoutFile(s);

                    //var layout = LayoutEditorFunctions.GetShadowLayout(s);
                    //GetShadowLayout(s);//.ForEach(setObjects.Add)
                }


            // Parse folder 2 for .cmn .nrm .hrd .ds1

            VistaFolderBrowserDialog openFolder2 = new VistaFolderBrowserDialog();
            var folder2 = "";
            var folder2_name = "";
            if (openFolder2.ShowDialog() == DialogResult.OK)
            {
                folder2_name = openFolder2.SelectedPath;
                folder2 = Path.GetFileNameWithoutExtension(openFolder2.SelectedPath);
            }

            var layoutSystem2 = new LayoutEditorSystem
            {
                autoUnkBytes = false
            };

            foreach (string s in new string[]
            {
                    //Path.Combine(folder2_name, folder2) + "_ds1.dat",
                    Path.Combine(folder2_name, folder2) + "_cmn.dat",
                    //Path.Combine(folder2_name, folder2) + "_nrm.dat",
                    //Path.Combine(folder2_name, folder2) + "_hrd.dat"
            }) if (File.Exists(s))
                {
                    layoutSystem2.OpenLayoutFile(s);

                    //var layout = LayoutEditorFunctions.GetShadowLayout(s);
                    //GetShadowLayout(s);//.ForEach(setObjects.Add)
                }

            if (layoutSystem1.Equals(layoutSystem2))
            {
                MessageBox.Show("Nothing to do, files are equal");
            } else
            {
                MessageBox.Show("Files do not match");
            }


            // Object Order Check
            // 1. -> check obj count; if size match, then compare individual obj data
            // 2. Obj count does not match OR has been sorted; Search individual object for matching pairs (exact data matches only!) Any leftover mismatches from folder2 add to new nrm/cmn/hrd
            // 3. Export new nrm/cmn/hrd with changes; Add a log file mentioning any that were tweaked (ex position, misc byte) but exist in original AND their slot position in folder 1 and folder 2


        }
    }
}
