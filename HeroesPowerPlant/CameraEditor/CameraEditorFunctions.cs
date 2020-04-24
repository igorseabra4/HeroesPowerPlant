using SharpDX;
using System.Collections.Generic;
using System.IO;
using static HeroesPowerPlant.ReadWriteCommon;

namespace HeroesPowerPlant.CameraEditor
{
    public static class CameraEditorFunctions
    {
        public static List<CameraHeroes> ImportCameraFile(string fileName)
        {
            List<CameraHeroes> list = new List<CameraHeroes>();

            using (BinaryReader camReader = new BinaryReader(new FileStream(fileName, FileMode.Open)))
            {
                camReader.BaseStream.Position = 0;

                while (camReader.BaseStream.Position != camReader.BaseStream.Length)
                {
                    CameraHeroes TempCam = new CameraHeroes(
                        cameraType: Switch(camReader.ReadInt32()),
                        cameraSpeed: Switch(camReader.ReadInt32()),
                        integer3: Switch(camReader.ReadInt32()),
                        activationType: Switch(camReader.ReadInt32()),
                        triggerShape: Switch(camReader.ReadInt32()),
                        triggerPosition: new Vector3(Switch(camReader.ReadSingle()), Switch(camReader.ReadSingle()), Switch(camReader.ReadSingle())),
                        triggerRotX: Switch(camReader.ReadInt32()),
                        triggerRotY: Switch(camReader.ReadInt32()),
                        triggerRotZ: Switch(camReader.ReadInt32()),
                        triggerScale: new Vector3(Switch(camReader.ReadSingle()), Switch(camReader.ReadSingle()), Switch(camReader.ReadSingle())),
                        camPos: new Vector3(Switch(camReader.ReadSingle()), Switch(camReader.ReadSingle()), Switch(camReader.ReadSingle())),
                        camRotX: Switch(camReader.ReadInt32()),
                        camRotY: Switch(camReader.ReadInt32()),
                        camRotZ: Switch(camReader.ReadInt32()),
                        pointA: new Vector3(Switch(camReader.ReadSingle()), Switch(camReader.ReadSingle()), Switch(camReader.ReadSingle())),
                        pointB: new Vector3(Switch(camReader.ReadSingle()), Switch(camReader.ReadSingle()), Switch(camReader.ReadSingle())),
                        pointC: new Vector3(Switch(camReader.ReadSingle()), Switch(camReader.ReadSingle()), Switch(camReader.ReadSingle())),
                        integer30: Switch(camReader.ReadInt32()),
                        integer31: Switch(camReader.ReadInt32()),
                        floatX32: Switch(camReader.ReadSingle()),
                        floatY33: Switch(camReader.ReadSingle()),
                        floatX34: Switch(camReader.ReadSingle()),
                        floatY35: Switch(camReader.ReadSingle()),
                        integer36: Switch(camReader.ReadInt32()),
                        integer37: Switch(camReader.ReadInt32()),
                        integer38: Switch(camReader.ReadInt32()),
                        integer39: Switch(camReader.ReadInt32())
                    );

                    if (TempCam.CameraType == 0 & TempCam.CameraSpeed == 0 & TempCam.Integer3 == 0 & TempCam.ActivationType == 0 & TempCam.TriggerShape == 0)
                        continue;

                    TempCam.CreateTransformMatrix();

                    list.Add(TempCam);
                }
            }
            return list;
        }

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

            CameraWriter.Close();
        }
    }
}
