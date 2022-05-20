using System;
using System.Windows.Forms;

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

        private void buttonDiff_Click(object sender, EventArgs e)
        {

        }
    }
}
