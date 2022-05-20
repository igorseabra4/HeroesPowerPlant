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

        // TODO: Expand this feature to eventually check .cam, .one for spline changes, visibility changes, and geo/coli changes
        private void buttonDiff_Click(object sender, EventArgs e)
        {
            // Parse folder 1 for .cmn .nrm .hrd .ds1

            // Parse folder 2 for .cmn .nrm .hrd .ds1

            // Object Order Check
            // 1. -> check obj count; if size match, then compare individual obj data
            // 2. Obj count does not match OR has been sorted; Search individual object for matching pairs (exact data matches only!) Any leftover mismatches from folder2 add to new nrm/cmn/hrd
            // 3. Export new nrm/cmn/hrd with changes; Add a log file mentioning any that were tweaked (ex position, misc byte) but exist in original AND their slot position in folder 1 and folder 2


        }
    }
}
