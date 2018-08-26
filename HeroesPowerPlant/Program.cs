using System;
using System.Windows.Forms;
using HeroesPowerPlant.ConfigEditor;
using HeroesPowerPlant.MainForm;

namespace HeroesPowerPlant
{
    static class Program
    {
        public static MainForm.MainForm MainForm;
        public static AboutBox AboutBox;

        public static ViewConfig ViewConfig;
        public static ConfigEditor.ConfigEditor ConfigEditor;
        public static LevelEditor.LevelEditor LevelEditor;
        public static CollisionEditor.CollisionEditor CollisionEditor;
        public static LayoutEditor.LayoutEditor LayoutEditor;
        public static SplineEditor.SplineEditor SplineEditor;
        public static CameraEditor.CameraEditor CameraEditor;
        public static ParticleEditor.ParticleMenu ParticleEditor;
        public static TexturePatternEditor.TexturePatternEditor TexturePatternEditor;
        public static SetIdTableEditor.SetIdTableEditor SetIdTableEditor;

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
            AboutBox = new AboutBox();
            ViewConfig = new ViewConfig();
            ConfigEditor = new ConfigEditor.ConfigEditor();
            LevelEditor = new LevelEditor.LevelEditor();
            CollisionEditor = new CollisionEditor.CollisionEditor();
            LayoutEditor = new LayoutEditor.LayoutEditor();
            SplineEditor = new SplineEditor.SplineEditor();
            CameraEditor = new CameraEditor.CameraEditor();
            ParticleEditor = new ParticleEditor.ParticleMenu();
            TexturePatternEditor = new TexturePatternEditor.TexturePatternEditor();
            SetIdTableEditor = new SetIdTableEditor.SetIdTableEditor();

            Application.Run(MainForm);
        }
    }
}
