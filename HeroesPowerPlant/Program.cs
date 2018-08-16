using System;
using System.Windows.Forms;
using HeroesPowerPlant.ConfigEditor;

namespace HeroesPowerPlant
{
    static class Program
    {
        public static MainForm MainForm;
        public static AboutBox AboutBox;

        public static ViewConfig ViewConfig;
        public static ConfigEditor.ConfigEditor ConfigEditor;
        public static LevelEditor.LevelEditor LevelEditor;
        public static CollisionEditor.CollisionEditor CollisionEditor;
        public static LayoutEditor.LayoutEditor LayoutEditor;
        public static SplineEditor SplineEditor;
        public static CameraEditor.CameraEditor CameraEditor;
        public static ParticleEditor.ParticleEditor ParticleEditor;

        /// <summary>
        /// Ponto de entrada principal para o aplicativo.
        /// </summary>
        [STAThread]
        static void Main()
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-us");

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            MainForm = new MainForm();
            AboutBox = new AboutBox();
            ViewConfig = new ViewConfig();
            ConfigEditor = new ConfigEditor.ConfigEditor();
            LevelEditor = new LevelEditor.LevelEditor();
            CollisionEditor = new CollisionEditor.CollisionEditor();
            LayoutEditor = new LayoutEditor.LayoutEditor();
            SplineEditor = new SplineEditor();
            CameraEditor = new CameraEditor.CameraEditor();
            ParticleEditor = new ParticleEditor.ParticleEditor();

            Application.Run(MainForm);
        }
    }
}
