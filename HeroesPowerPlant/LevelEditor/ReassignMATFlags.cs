using HeroesPowerPlant.Shared.IO.Config;
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
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

        private void buttonWikiForGeoMatFlags_Click(object sender, EventArgs e)
        {
            OpenBrowser("https://github.com/igorseabra4/HeroesPowerPlant/wiki/Level-Editor");
        }

        private static void OpenBrowser(string url)
        {
            try
            {
                Process.Start(url);
            }
            catch
            {
                // hack because of this: https://github.com/dotnet/corefx/issues/10361
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    url = url.Replace("&", "^&");
                    Process.Start(new ProcessStartInfo("cmd", $"/c start {url}") { CreateNoWindow = true });
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                {
                    Process.Start("xdg-open", url);
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                {
                    Process.Start("open", url);
                }
                else
                {
                    throw;
                }
            }
        }
    }
}
