using SharpDX;
using System;
using System.Collections.Generic;
using System.IO;
using static HeroesPowerPlant.ReadWriteCommon;

namespace HeroesPowerPlant.ShadowCameraEditor
{
    public static class ShadowCameraEditorFunctions
    {
        public static Tuple<byte[], List<ShadowCamera>> ImportCameraFile(string fileName)
        {
            List<ShadowCamera> list = new List<ShadowCamera>();
            byte[] headerArray;
            using (BinaryReader camReader = new BinaryReader(new FileStream(fileName, FileMode.Open)))
            {
                camReader.BaseStream.Position = 0;
                headerArray = camReader.ReadBytes(0x18);

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
            return Tuple.Create(headerArray, list);
        }

        public static void SaveCameraFile(string fileName, byte[] headerArray, IEnumerable<ShadowCamera> list)
        {
            BinaryWriter CameraWriter = new BinaryWriter(new FileStream(fileName, FileMode.Create));

            CameraWriter.Write(headerArray);
            /*foreach (byte[] i in cameraList)
            {
                CameraWriter.Write(i);
            }*/

            foreach (ShadowCamera i in list)
            {
                CameraWriter.Write(i.field_00);
                CameraWriter.Write(i.field_04);
                CameraWriter.Write(i.field_08);
                CameraWriter.Write(i.field_0C);
                CameraWriter.Write(i.field_10);
                CameraWriter.Write(i.field_14);
                CameraWriter.Write(i.field_18);
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
                CameraWriter.Write(i.field_44);
                CameraWriter.Write(i.field_48);
                CameraWriter.Write(i.field_4C);
                CameraWriter.Write(i.field_50);
                CameraWriter.Write(i.field_54);
                CameraWriter.Write(i.field_58);
                CameraWriter.Write(i.UnknownSection3);


                /*CameraWriter.Write(Switch(i.CameraType));
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
                CameraWriter.Write(Switch(i.Integer39));*/
            }
            CameraWriter.Close();
        }
    }
}
