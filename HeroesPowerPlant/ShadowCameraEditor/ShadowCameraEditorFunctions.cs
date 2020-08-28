using SharpDX;
using System;
using System.Collections.Generic;
using System.IO;

namespace HeroesPowerPlant.ShadowCameraEditor
{
    public static class ShadowCameraEditorFunctions
    {
        public static Tuple<ShadowCameraFileHeader, List<ShadowCamera>> ImportCameraFile(string fileName)
        {
            List<ShadowCamera> list = new List<ShadowCamera>();
            ShadowCameraFileHeader header;
            using (BinaryReader camReader = new BinaryReader(new FileStream(fileName, FileMode.Open)))
            {
                camReader.BaseStream.Position = 0;
                header = new ShadowCameraFileHeader(
                    camReader.ReadInt32(), camReader.ReadInt32(), camReader.ReadInt32(),
                    camReader.ReadInt32(), camReader.ReadInt32(), camReader.ReadInt32()
                    );

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
                        camReader.ReadSingle(),
                        camReader.ReadSingle(), //60
                        camReader.ReadSingle(),
                        camReader.ReadSingle(),
                        camReader.ReadSingle(),
                        camReader.ReadSingle(), //70
                        camReader.ReadSingle(),
                        camReader.ReadSingle(),
                        camReader.ReadSingle(),
                        camReader.ReadSingle(), //80
                        camReader.ReadSingle(),
                        camReader.ReadSingle(),
                        camReader.ReadSingle(),
                        camReader.ReadSingle(), //90
                        camReader.ReadSingle(),
                        camReader.ReadSingle(),
                        camReader.ReadSingle(),
                        camReader.ReadSingle(), //A0
                        camReader.ReadSingle(),
                        camReader.ReadSingle(),
                        camReader.ReadSingle(),
                        camReader.ReadSingle(), //B0
                        camReader.ReadSingle(),
                        camReader.ReadSingle(),
                        camReader.ReadSingle(),
                        camReader.ReadSingle(), //C0
                        camReader.ReadSingle(),
                        camReader.ReadSingle(),
                        camReader.ReadSingle(),
                        camReader.ReadSingle(), //D0
                        camReader.ReadSingle(),
                        camReader.ReadSingle()
                    );

                    //if (TempCam.CameraType == 0 & TempCam.CameraSpeed == 0 & TempCam.Integer3 == 0 & TempCam.ActivationType == 0 & TempCam.TriggerShape == 0)
                     //   continue;

                    TempCam.CreateTransformMatrix();

                    list.Add(TempCam);
                }
            }
            return Tuple.Create(header, list);
        }

        public static void SaveCameraFile(string fileName, ShadowCameraFileHeader header, IEnumerable<ShadowCamera> list)
        {
            BinaryWriter CameraWriter = new BinaryWriter(new FileStream(fileName, FileMode.Create));

            CameraWriter.Write(header.magic_00);
            CameraWriter.Write(header.magic_04);
            CameraWriter.Write(header.stageId);
            CameraWriter.Write(header.unknown_0C);
            CameraWriter.Write(header.unknown_10);
            CameraWriter.Write(header.numberOfCameras);

            foreach (ShadowCamera i in list)
            {
                CameraWriter.Write(i.CameraNumber);
                CameraWriter.Write((int)i.CameraMode);
                CameraWriter.Write(i.field_08);
                CameraWriter.Write(i.field_0C);
                CameraWriter.Write(i.field_10);
                CameraWriter.Write(i.field_14);
                CameraWriter.Write(i.LookBLinkId);
                CameraWriter.Write(i.field_1C);
                CameraWriter.Write(i.TriggerPosition.X);
                CameraWriter.Write(i.TriggerPosition.Y);
                CameraWriter.Write(i.TriggerPosition.Z);
                CameraWriter.Write(i.TriggerRotation.X);
                CameraWriter.Write(i.TriggerRotation.Y);
                CameraWriter.Write(i.TriggerRotation.Z);
                CameraWriter.Write(i.TriggerScale.X);
                CameraWriter.Write(i.TriggerScale.Y);
                CameraWriter.Write(i.TriggerScale.Z);
                CameraWriter.Write(i.PointA_LookFrom_X);
                CameraWriter.Write(i.PointA_LookFrom_Y);
                CameraWriter.Write(i.PointA_LookFrom_Z);
                CameraWriter.Write(i.PointA_LookAt_X);
                CameraWriter.Write(i.PointA_LookAt_Y);
                CameraWriter.Write(i.PointA_LookAt_Z);
                CameraWriter.Write(i.CameraRotation);
                CameraWriter.Write(i.FOV_Height);
                CameraWriter.Write(i.FOV_Width);
                CameraWriter.Write(i.field_68);
                CameraWriter.Write(i.field_6C);
                CameraWriter.Write(i.field_70);
                CameraWriter.Write(i.field_74);
                CameraWriter.Write(i.PointB_LookFrom_X);
                CameraWriter.Write(i.PointB_LookFrom_Y);
                CameraWriter.Write(i.PointB_LookFrom_Z);
                CameraWriter.Write(i.PointB_LookAt_X);
                CameraWriter.Write(i.PointB_LookAt_Y);
                CameraWriter.Write(i.PointB_LookAt_Z);
                CameraWriter.Write(i.CameraDistanceFromPlayerLookA);
                CameraWriter.Write(i.CameraHeightFromPlayerLookA);
                CameraWriter.Write(i.CameraDistanceFromPlayerLookB);
                CameraWriter.Write(i.CameraHeightFromPlayerLookB);
                CameraWriter.Write(i.field_A0);
                CameraWriter.Write(i.field_A4);
                CameraWriter.Write(i.field_A8);
                CameraWriter.Write(i.field_AC);
                CameraWriter.Write(i.TransitionTimeEnter);
                CameraWriter.Write(i.TransitionTimeExit);
                CameraWriter.Write(i.field_B8);
                CameraWriter.Write(i.field_BC);
                CameraWriter.Write(i.field_C0);
                CameraWriter.Write(i.field_C4);
                CameraWriter.Write(i.field_C8);
                CameraWriter.Write(i.field_CC);
                CameraWriter.Write(i.field_D0);
                CameraWriter.Write(i.field_D4);
                CameraWriter.Write(i.field_D8);
            }
            CameraWriter.Close();
        }
    }
}
