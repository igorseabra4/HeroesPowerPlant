using System;
using System.Windows.Forms;

namespace HeroesPowerPlant
{
    static class Program
    {
        public static MainForm.MainForm MainForm;

        /// <summary>
        /// Ponto de entrada principal para o aplicativo.
        /// </summary>
        [STAThread]
        static void Main()
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-us");

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (!SharpDevice.IsDirectX11Supported())
            {
                MessageBox.Show("DirectX11 feature level 11.0 is required to run Heroes Power Plant. Maximum supported feature level is " + SharpDevice.GetSupportedFeatureLevel().ToString() + ". Please update your DirectX.");
                return;
            }

            MainForm = new MainForm.MainForm();

            Application.Run(MainForm);

            try
            {
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Unhandled Exception in Power Plant: {ex.Message} {ex.InnerException} {ex.StackTrace}");
            }
        }
    }
}
