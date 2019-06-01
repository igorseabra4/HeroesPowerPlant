using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Reloaded.Injector;
using RemoteControl;
using RemoteControl.Structs;

namespace HeroesPowerPlant.Shared
{
    public static class RemoteControl
    {
        private const string WINDOW_NAME = "SONIC HEROES(TM)";

        private static Injector Injector;
        private static Process GameProcess;
        private static string DllPath;

        /// <summary>
        /// Forces the game to reload collision.
        /// </summary>
        public static void LoadCollision(string fileNameWithoutExtension)
        {
            UpdateFields();

            // Assume already injected.
            if (AssertGameRunning())
            {
                NativeString64Char nativeStr = new NativeString64Char(fileNameWithoutExtension);
                Injector.CallFunction(DllPath, CollisionReloader.LoadCollisionFunctionName, nativeStr, true);
            }
        }

        private static bool AssertGameRunning()
        {
            if (GameProcess == null || GameProcess.HasExited)
            {
                MessageBox.Show("Game not running");
                return false;
            }

            return true;
        }

        private static void UpdateFields()
        {
            if (GameProcess == null || GameProcess.HasExited)
            {
                // Get new process.
                GameProcess = GetGame();

                // There might not be a process running.
                if (GameProcess != null)
                {
                    Injector = new Injector(GameProcess);

                    // Inject our RemoteControl DLL.
                    DllPath = Path.GetFullPath($"{AppDomain.CurrentDomain.BaseDirectory}\\RemoteControl.dll");
                    Injector.Inject(DllPath);
                }
            }
        }

        /// <summary>
        /// Returns a <see cref="Process"/> if the game is running, else null.
        /// </summary>
        private static Process GetGame()
        {
            Process[] _allProcesses = Process.GetProcesses();
            foreach (Process process in _allProcesses)
                if (process.MainWindowTitle.ToLower().Contains(WINDOW_NAME.ToLower()))
                    return process;

            return null;
        }
    }
}
