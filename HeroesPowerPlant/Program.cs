using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace HeroesPowerPlant
{
    static class Program
    {
        public static MainForm mainForm;
        public static AboutBox aboutBox;

        public static ReadWriteProcess MemManager = new ReadWriteProcess();

        public static ViewConfig viewConfig;
        public static Config.ConfigEditor configEditor;
        public static LevelEditor.LevelEditor levelEditor;
        public static Collision.CollisionEditor collisionEditor;
        public static LayoutEditor.LayoutEditor layoutEditor;

        public static Config.SplineEditor splineEditor;





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
            configEditor = new Config.ConfigEditor();
            levelEditor = new LevelEditor.LevelEditor();
            collisionEditor = new Collision.CollisionEditor();
            layoutEditor = new LayoutEditor.LayoutEditor();

            splineEditor = new Config.SplineEditor();


            Application.Run(mainForm);
        }
    }
}
