using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HeroesPowerPlant.RemoteControl.Shared;

namespace HeroesPowerPlant.Shared
{
    public static class RemoteControl
    {
        private static Client Client;

        /// <summary>
        /// Forces the game to reload collision.
        /// </summary>
        public static void LoadCollision(string fileNameWithoutExtension)
        {
            if (TryConnectToGame())
                Client.LoadCollision(fileNameWithoutExtension, 1000);
        }

        private static bool TryConnectToGame()
        {
            try
            {
                if (Client == null || !Client.IsConnected())
                    Client = new Client(Process.GetProcessesByName("tsonic_win")[0]);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
