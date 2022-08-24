using HeroesPowerPlant.Shared.IO.Config;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace HeroesPowerPlant.LayoutEditor
{
    public partial class PickTemplate : Form
    {
        public PickTemplate(List<Template> templates)
        {
            InitializeComponent();
            foreach(var t in templates)
                comboBoxTemplates.Items.Add(t);
        }

        private void PickTemplate_Load(object sender, EventArgs e)
        {
            TopMost = HPPConfig.GetInstance().LegacyWindowPriorityBehavior;
        }

        public static void GetTarget(List<Template> templates, out bool success, out Template template)
        {
            PickTemplate pt = new PickTemplate(templates);
            DialogResult d = pt.ShowDialog();

            if (pt.OKed || d == DialogResult.OK)
            {
                template = pt.comboBoxTemplates.SelectedItem == null ? null : (Template)pt.comboBoxTemplates.SelectedItem;
                success = true;
            }
            else
            {
                success = false;
                template = null;
            }
        }

        bool OKed = false;
        private void ButtonOK_Click(object sender, EventArgs e)
        {
            OKed = true;
            Close();
        }

        private void comboBoxTemplates_SelectedIndexChanged(object sender, EventArgs e)
        {
            richTextBox1.Text = ((Template)comboBoxTemplates.SelectedItem).Text;
        }
    }
}
