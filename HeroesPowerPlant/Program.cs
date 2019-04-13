using System;
using System.Collections.Generic;
using System.Windows.Forms;
using HeroesPowerPlant.ConfigEditor;
using HeroesPowerPlant.MainForm;

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
