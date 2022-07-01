using HeroesPowerPlant.Shared.IO.Config;
using System;
using System.Windows.Forms;

namespace HeroesPowerPlant.LevelEditor
{
    public partial class ReassignMATFlags : Form
    {
        public ReassignMATFlags()
        {
            InitializeComponent();
            if (HPPConfig.GetInstance().LegacyWindowPriorityBehavior)
                TopMost = true;
            else
                TopMost = false;
        }

        public static (string, string) GetMATSwap()
        {
            ReassignMATFlags form = new ReassignMATFlags();
            form.ShowDialog();

            return ("_" + form.textBox_targetMAT.Text + "_", "_" + form.textBox_replacementMAT.Text + "_");
        }

        private void button_ReplaceFlags_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
