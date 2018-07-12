using SharpDX;
using System.Collections.Generic;
using System.IO;
using static HeroesPowerPlant.ReadWriteCommon;

namespace HeroesPowerPlant.CameraEditor
{
    public class CameraEditorFunctions
    {
        public static List<CameraHeroes> importCameraFile(string fileName)
        {
            List<CameraHeroes> list = new List<CameraHeroes>();
            BinaryReader CameraReader = new BinaryReader(new FileStream(fileName, FileMode.Open));

            CameraReader.BaseStream.Position = 0;

            while (CameraReader.BaseStream.Position != CameraReader.BaseStream.Length)
            {
                CameraHeroes TempCam = new CameraHeroes
                {
                    CameraType = Switch(CameraReader.ReadInt32()),
                    CameraSpeed = Switch(CameraReader.ReadInt32()),
                    Integer3 = Switch(CameraReader.ReadInt32()),
                    ActivationType = Switch(CameraReader.ReadInt32()),
                    TriggerShape = Switch(CameraReader.ReadInt32()),
                    TriggerPosition = new Vector3(Switch(CameraReader.ReadSingle()), Switch(CameraReader.ReadSingle()), Switch(CameraReader.ReadSingle())),
                    TriggerRotX = Switch(CameraReader.ReadInt32()),
                    TriggerRotY = Switch(CameraReader.ReadInt32()),
                    TriggerRotZ = Switch(CameraReader.ReadInt32()),
                    TriggerScale = new Vector3(Switch(CameraReader.ReadSingle()), Switch(CameraReader.ReadSingle()), Switch(CameraReader.ReadSingle())),
                    CamPos = new Vector3(Switch(CameraReader.ReadSingle()), Switch(CameraReader.ReadSingle()), Switch(CameraReader.ReadSingle())),
                    CamRotX = Switch(CameraReader.ReadInt32()),
                    CamRotY = Switch(CameraReader.ReadInt32()),
                    CamRotZ = Switch(CameraReader.ReadInt32()),
                    PointA = new Vector3(Switch(CameraReader.ReadSingle()), Switch(CameraReader.ReadSingle()), Switch(CameraReader.ReadSingle())),
                    PointB = new Vector3(Switch(CameraReader.ReadSingle()), Switch(CameraReader.ReadSingle()), Switch(CameraReader.ReadSingle())),
                    PointC = new Vector3(Switch(CameraReader.ReadSingle()), Switch(CameraReader.ReadSingle()), Switch(CameraReader.ReadSingle())),
                    Integer30 = Switch(CameraReader.ReadInt32()),
                    Integer31 = Switch(CameraReader.ReadInt32()),
                    FloatX32 = Switch(CameraReader.ReadSingle()),
                    FloatY33 = Switch(CameraReader.ReadSingle()),
                    FloatX34 = Switch(CameraReader.ReadSingle()),
                    FloatY35 = Switch(CameraReader.ReadSingle()),
                    Integer36 = Switch(CameraReader.ReadInt32()),
                    Integer37 = Switch(CameraReader.ReadInt32()),
                    Integer38 = Switch(CameraReader.ReadInt32()),
                    Integer39 = Switch(CameraReader.ReadInt32())
                };

                if (TempCam.CameraType == 0 & TempCam.CameraSpeed == 0 & TempCam.Integer3 == 0 & TempCam.ActivationType == 0 & TempCam.TriggerShape == 0)
                    continue;

                TempCam.CreateTransformMatrix();

                list.Add(TempCam);
            }

            CameraReader.Close();
            return list;
        }

        public static void saveCameraFile(string fileName, IEnumerable<CameraHeroes> list)
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
