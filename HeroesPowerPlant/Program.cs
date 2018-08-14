using System;
using System.Windows.Forms;
using HeroesPowerPlant.ConfigEditor;

namespace HeroesPowerPlant
{
    static class Program
    {
        public static MainForm mainForm;
        public static AboutBox aboutBox;

        public static ViewConfig viewConfig;
        public static ConfigEditor.ConfigEditor configEditor;
        public static LevelEditor.LevelEditor levelEditor;
        public static CollisionEditor.CollisionEditor collisionEditor;
        public static LayoutEditor.LayoutEditor layoutEditor;
        public static SplineEditor splineEditor;
        public static CameraEditor.CameraEditor cameraEditor;

        /// <summary>
        /// Ponto de entrada principal para o aplicativo.
        /// </summary>
        [STAThread]
        static void Main()
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-us");

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            mainForm = new MainForm();
            aboutBox = new AboutBox();
            viewConfig = new ViewConfig();
            configEditor = new ConfigEditor.ConfigEditor();
            levelEditor = new LevelEditor.LevelEditor();
            collisionEditor = new CollisionEditor.CollisionEditor();
            layoutEditor = new LayoutEditor.LayoutEditor();
            splineEditor = new SplineEditor();
            cameraEditor = new CameraEditor.CameraEditor();
            
            Application.Run(mainForm);
        }
    }
}
