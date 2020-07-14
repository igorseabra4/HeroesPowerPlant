using SharpDX;
using System.Collections.Generic;
using System.IO;
using static HeroesPowerPlant.ReadWriteCommon;

namespace HeroesPowerPlant.ShadowCameraEditor
{
    public static class ShadowCameraEditorFunctions
    {
        public static List<ShadowCamera> ImportCameraFile(string fileName)
        {
            List<ShadowCamera> list = new List<ShadowCamera>();
            using (BinaryReader camReader = new BinaryReader(new FileStream(fileName, FileMode.Open)))
            {
                camReader.BaseStream.Position = 0;
                byte[] headerArray = camReader.ReadBytes(0x18);

                while (camReader.BaseStream.Position != camReader.BaseStream.Length)
                {
                    ShadowCamera TempCam = new ShadowCamera(
                        camReader.ReadInt32(), //i_00
                        camReader.ReadInt32(),
                        camReader.ReadInt32(),
                        camReader.ReadInt32(),
                        camReader.ReadInt32(),
                        camReader.ReadInt32(),
                        camReader.ReadInt32(),
                        camReader.ReadInt32(), //i_1C
                        new Vector3(camReader.ReadSingle(), camReader.ReadSingle(), camReader.ReadSingle()),
                        new Vector3(camReader.ReadSingle(), camReader.ReadSingle(), camReader.ReadSingle()),
                        new Vector3(camReader.ReadSingle(), camReader.ReadSingle(), camReader.ReadSingle()),
                        camReader.ReadSingle(), //i_44
                        camReader.ReadSingle(),
                        camReader.ReadSingle(),
                        camReader.ReadSingle(),
                        camReader.ReadSingle(),
                        camReader.ReadSingle(), //i_58
                        camReader.ReadBytes(0x80)
                    );

                    //if (TempCam.CameraType == 0 & TempCam.CameraSpeed == 0 & TempCam.Integer3 == 0 & TempCam.ActivationType == 0 & TempCam.TriggerShape == 0)
                     //   continue;

                    TempCam.CreateTransformMatrix();

                    list.Add(TempCam);
                }
            }
            return list;
        }

        /*
        public static void SaveCameraFile(string fileName, IEnumerable<CameraHeroes> list)
        {
            BinaryWriter CameraWriter = new BinaryWriter(new FileStream(fileName, FileMode.Create));

            foreach (CameraHeroes i in list)
            {
                if (i.CameraType == 0 & i.CameraSpeed == 0 & i.Integer3 == 0 & i.ActivationType == 0 & i.TriggerShape == 0)
                    continue;

                CameraWriter.Write(Switch(i.CameraType));
                CameraWriter.Write(Switch(i.CameraSpeed));
                CameraWriter.Write(Switch(i.Integer3));
                CameraWriter.Write(Switch(i.ActivationType));
                CameraWriter.Write(Switch(i.TriggerShape));
                CameraWriter.Write(Switch(i.TriggerPosition.X));
                CameraWriter.Write(Switch(i.TriggerPosition.Y));
                CameraWriter.Write(Switch(i.TriggerPosition.Z));
                CameraWriter.Write(Switch(i.TriggerRotX));
                CameraWriter.Write(Switch(i.TriggerRotY));
                CameraWriter.Write(Switch(i.TriggerRotZ));
                CameraWriter.Write(Switch(i.TriggerScale.X));
                CameraWriter.Write(Switch(i.TriggerScale.Y));
                CameraWriter.Write(Switch(i.TriggerScale.Z));
                CameraWriter.Write(Switch(i.CamPos.X));
                CameraWriter.Write(Switch(i.CamPos.Y));
                CameraWriter.Write(Switch(i.CamPos.Z));
                CameraWriter.Write(Switch(i.CamRotX));
                CameraWriter.Write(Switch(i.CamRotY));
                CameraWriter.Write(Switch(i.CamRotZ));
                CameraWriter.Write(Switch(i.PointA.X));
                CameraWriter.Write(Switch(i.PointA.Y));
                CameraWriter.Write(Switch(i.PointA.Z));
                CameraWriter.Write(Switch(i.PointB.X));
                CameraWriter.Write(Switch(i.PointB.Y));
                CameraWriter.Write(Switch(i.PointB.Z));
                CameraWriter.Write(Switch(i.PointC.X));
                CameraWriter.Write(Switch(i.PointC.Y));
                CameraWriter.Write(Switch(i.PointC.Z));
                CameraWriter.Write(Switch(i.Integer30));
                CameraWriter.Write(Switch(i.Integer31));
                CameraWriter.Write(Switch(i.FloatX32));
                CameraWriter.Write(Switch(i.FloatY33));
                CameraWriter.Write(Switch(i.FloatX34));
                CameraWriter.Write(Switch(i.FloatY35));
                CameraWriter.Write(Switch(i.Integer36));
                CameraWriter.Write(Switch(i.Integer37));
                CameraWriter.Write(Switch(i.Integer38));
                CameraWriter.Write(Switch(i.Integer39));
            }

            CameraWriter.Close();*/
    }
}
