using HeroesPowerPlant.Shared.IO.Config;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace HeroesPowerPlant.MainForm
{
    public partial class AboutBox : Form
    {
        public AboutBox()
        {
            InitializeComponent();
            this.labelProductName.Text = AssemblyProduct;
            this.labelCopyright.Text = AssemblyCopyright;
            this.labelCompanyName.Text = AssemblyCompany;
            ComponentResourceManager resources = new ComponentResourceManager(typeof(AboutBox));
            this.textBoxDescription.Text = resources.GetString("textBoxDescription.Text");
            this.labelVersion.Text = new HPPVersion().version;
            TopMost = true;
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.WindowsShutDown) return;
            if (e.CloseReason == CloseReason.FormOwnerClosing) return;

            e.Cancel = true;
            Hide();
        }

        #region Acessório de Atributos do Assembly

        public string AssemblyProduct
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyProductAttribute)attributes[0]).Product;
            }
        }

        public string AssemblyCopyright
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
            }
        }

        public string AssemblyCompany
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCompanyAttribute)attributes[0]).Company;
            }
        }
        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            OpenBrowser("https://github.com/igorseabra4/HeroesPowerPlant");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenBrowser("https://discordapp.com/invite/Ku9KRy6");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            OpenBrowser("https://github.com/igorseabra4/HeroesPowerPlant/wiki");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Hide();
        }

        private void AboutBox_Load(object sender, EventArgs e)
        {

        }

        public static void OpenBrowser(string url)
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
