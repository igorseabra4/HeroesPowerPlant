using Ookii.Dialogs.WinForms;
using System;
using System.IO;
using System.Windows.Forms;
using HeroesPowerPlant.LayoutEditor;
using HeroesPowerPlant.Shared.IO.Config;

namespace HeroesPowerPlant.ShadowLayoutDiffTool
{
    public partial class ShadowLayoutDiffTool : Form
    {
        public ShadowLayoutDiffTool()
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
            VistaOpenFileDialog openFile = new VistaOpenFileDialog { Filter = "DAT Files (*.dat)|*.dat|All files (*.*)|*.*" };
            string layout1;
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                layout1 = openFile.FileName;
            } else { 
                return;
            }

            string layout2;
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                layout2 = openFile.FileName;
            } else {
                return;
            }

            var layoutSystem1 = new LayoutEditorSystem
            {
                autoUnkBytes = false
            };

            var layoutSystem2 = new LayoutEditorSystem
            {
                autoUnkBytes = false
            };

            if (File.Exists(layout1) && File.Exists(layout2))
            {
                layoutSystem1.OpenLayoutFile(layout1);
                layoutSystem2.OpenLayoutFile(layout2);

                if (layoutSystem1.Equals(layoutSystem2))
                {
                    MessageBox.Show("Nothing to do, files are equal");
                    return;
                }
                else
                {
                    MessageBox.Show("Files do not match");
                    LayoutEditorSystem layoutSystemOriginalDiff;
                    LayoutEditorSystem layoutSystemResultDiff;
                    string log;
                    (layoutSystemOriginalDiff, layoutSystemResultDiff, log) = layoutSystem1.Diff(layoutSystem2);
                    
                    VistaFolderBrowserDialog dialog = new VistaFolderBrowserDialog();
                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        layoutSystemOriginalDiff.Save(Path.Combine(dialog.SelectedPath, Path.GetFileNameWithoutExtension(layoutSystemOriginalDiff.CurrentlyOpenFileName) + "_1st_file_diff.dat"));
                        layoutSystemResultDiff.Save(Path.Combine(dialog.SelectedPath, Path.GetFileNameWithoutExtension(layoutSystemResultDiff.CurrentlyOpenFileName) + "_2nd_file_diff.dat"));

                        File.WriteAllText(Path.Combine(dialog.SelectedPath, "diff_log.txt"), log);
                    } else
                    {
                        MessageBox.Show("Cancelled, no files written");
                        return;
                    }
                    MessageBox.Show("Success");
                }
            } else
            {
                MessageBox.Show("Files were not found");
            }
        }

        private void ShadowLayoutDiffTool_Load(object sender, EventArgs e)
        {
            if (HPPConfig.GetInstance().LegacyWindowPriorityBehavior)
                TopMost = true;
            else
                TopMost = false;
        }
    }
}
