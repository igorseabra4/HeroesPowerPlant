using HeroesPowerPlant.LayoutEditor;
using HeroesPowerPlant.Shared.IO.Config;
using Microsoft.VisualBasic.Logging;
using Ookii.Dialogs.WinForms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace HeroesPowerPlant.ShadowLayoutMiscTools
{
    public partial class ShadowLayoutMiscTools : Form
    {
        public ShadowLayoutMiscTools()
        {
            InitializeComponent();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.WindowsShutDown)
                return;
            if (e.CloseReason == CloseReason.FormOwnerClosing)
                return;

            e.Cancel = true;
            Hide();
        }

        public void New() {}

        // TODO: Expand this feature to eventually check .cam, .one for spline changes, visibility changes, and geo/coli changes
        private void buttonDiff_Click(object sender, EventArgs e)
        {
            VistaOpenFileDialog openFile = new VistaOpenFileDialog { Filter = "DAT Files (*.dat)|*.dat|All files (*.*)|*.*" };
            string layout1;
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                layout1 = openFile.FileName;
            }
            else
            {
                return;
            }

            string layout2;
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                layout2 = openFile.FileName;
            }
            else
            {
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
                var f = new Dictionary<(byte, byte, string), HashSet<byte[]>>();
                layoutSystem1.OpenLayoutFile(layout1, out _, ref f);
                layoutSystem2.OpenLayoutFile(layout2, out _, ref f);

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
                    }
                    else
                    {
                        MessageBox.Show("Cancelled, no files written");
                        return;
                    }
                    MessageBox.Show("Success");
                }
            }
            else
            {
                MessageBox.Show("Files were not found");
            }
        }

        private void ShadowLayoutMiscTools_Load(object sender, EventArgs e)
        {
            if (HPPConfig.GetInstance().LegacyWindowPriorityBehavior)
                TopMost = true;
            else
                TopMost = false;

            ComboBoxObject.DisplayMember = Name;
            ComboBoxObject.Items.AddRange(LayoutEditorSystem.GetAllShadowObjectEntries());
        }

        private void buttonFindObjectInFiles_Click(object sender, EventArgs e)
        {
            var index = ComboBoxObject.SelectedIndex;
            if (index == -1)
            {
                MessageBox.Show("Pick an object first!");
                return;
            }
            var targetObject = (ObjectEntry)ComboBoxObject.Items[index];
            VistaFolderBrowserDialog dialog = new VistaFolderBrowserDialog();
            var filesUsing = "";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string[] foundOnes = Directory.GetFiles(dialog.SelectedPath, "*.dat", SearchOption.AllDirectories);
                for (int i = 0; i < foundOnes.Length; i++)
                {
                    if (foundOnes[i].EndsWith("_cmn.dat") || foundOnes[i].EndsWith("_nrm.dat") || foundOnes[i].EndsWith("_hrd.dat") || foundOnes[i].EndsWith("_ds1.dat"))
                    {
                        var layoutSystem = new LayoutEditorSystem
                        {
                            autoUnkBytes = false
                        };
                        var f = new Dictionary<(byte, byte, string), HashSet<byte[]>>();
                        layoutSystem.OpenLayoutFile(foundOnes[i], out _, ref f);

                        var layoutObjs = layoutSystem.GetAllCurrentObjectEntries();
                        if (layoutObjs.Contains((targetObject.List, targetObject.Type)))
                        {
                            filesUsing += foundOnes[i].Split('\\').Last();
                            filesUsing += Environment.NewLine;
                            continue;
                        }
                    }
                }
                MessageBox.Show(filesUsing, "Results");
            }
            else
            {
                return;
            }
        }
    }
}
