using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace HeroesPowerPlant.ShadowCameraEditor {
    public partial class ShadowCameraEditor : Form {
        public ShadowCameraEditor() {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e) {
            using BinaryReader camReader = new BinaryReader(new FileStream("C:\\Users\\dreamsyntax\\cam.dat", FileMode.Open));
            camReader.BaseStream.Position = 0;
            byte[] headerArray = camReader.ReadBytes(0x18);
            List<byte[]> cameraList = new List<byte[]>();
            float insertject = 8;
            while (camReader.BaseStream.Position != camReader.BaseStream.Length) {
                cameraList.Add(camReader.ReadBytes(0x18));
                cameraList.Add(BitConverter.GetBytes(insertject));
                camReader.ReadBytes(0x4); //trash
                cameraList.Add(camReader.ReadBytes(0xC0));
            }

            BinaryWriter CameraWriter = new BinaryWriter(new FileStream("C:\\Users\\dreamsyntax\\Desktop\\CameraResearch\\files\\stg0403\\stg0403_cam.dat", FileMode.Create));
            CameraWriter.Write(headerArray);
            foreach (byte[] i in cameraList) {
                CameraWriter.Write(i);
            }
        }
    }
}
