using HeroesPowerPlant.Shared.Utilities;
using SharpDX;
using System.Collections.Generic;
using System.IO;

namespace HeroesPowerPlant.CameraEditor
{
    public static class CameraEditorFunctions
    {
        public static List<CameraHeroes> ImportCameraFile(string fileName)
        {
            var list = new List<CameraHeroes>();
            using (var camReader = new EndianBinaryReader(new FileStream(fileName, FileMode.Open), Endianness.Big))
            {
                camReader.BaseStream.Position = 0;
                while (camReader.BaseStream.Position != camReader.BaseStream.Length)
                {
                    var cam = new CameraHeroes(
                        cameraType: camReader.ReadInt32(),
                        cameraSpeed: camReader.ReadInt32(),
                        integer3: camReader.ReadInt32(),
                        activationType: camReader.ReadInt32(),
                        triggerShape: camReader.ReadInt32(),
                        triggerPosition: new Vector3(camReader.ReadSingle(), camReader.ReadSingle(), camReader.ReadSingle()),
                        triggerRotX: camReader.ReadInt32(),
                        triggerRotY: camReader.ReadInt32(),
                        triggerRotZ: camReader.ReadInt32(),
                        triggerScale: new Vector3(camReader.ReadSingle(), camReader.ReadSingle(), camReader.ReadSingle()),
                        camPos: new Vector3(camReader.ReadSingle(), camReader.ReadSingle(), camReader.ReadSingle()),
                        camRotX: camReader.ReadInt32(),
                        camRotY: camReader.ReadInt32(),
                        camRotZ: camReader.ReadInt32(),
                        pointA: new Vector3(camReader.ReadSingle(), camReader.ReadSingle(), camReader.ReadSingle()),
                        pointB: new Vector3(camReader.ReadSingle(), camReader.ReadSingle(), camReader.ReadSingle()),
                        pointC: new Vector3(camReader.ReadSingle(), camReader.ReadSingle(), camReader.ReadSingle()),
                        integer30: camReader.ReadInt32(),
                        integer31: camReader.ReadInt32(),
                        floatX32: camReader.ReadSingle(),
                        floatY33: camReader.ReadSingle(),
                        floatX34: camReader.ReadSingle(),
                        floatY35: camReader.ReadSingle(),
                        integer36: camReader.ReadInt32(),
                        integer37: camReader.ReadInt32(),
                        integer38: camReader.ReadInt32(),
                        integer39: camReader.ReadInt32()
                    );

                    if (cam.CameraType == 0 & cam.CameraSpeed == 0 & cam.Integer3 == 0 & cam.ActivationType == 0 & cam.TriggerShape == 0)
                        continue;

                    cam.CreateTransformMatrix();

                    list.Add(cam);
                }
            }
            return list;
        }

        public static void SaveCameraFile(string fileName, IEnumerable<CameraHeroes> list)
        {
            using (var writer = new EndianBinaryWriter(new FileStream(fileName, FileMode.Create), Endianness.Big))
                foreach (CameraHeroes i in list)
                {
                    if (i.CameraType == 0 & i.CameraSpeed == 0 & i.Integer3 == 0 & i.ActivationType == 0 & i.TriggerShape == 0)
                        continue;

                    writer.Write(i.CameraType);
                    writer.Write(i.CameraSpeed);
                    writer.Write(i.Integer3);
                    writer.Write(i.ActivationType);
                    writer.Write(i.TriggerShape);
                    writer.Write(i.TriggerPosition.X);
                    writer.Write(i.TriggerPosition.Y);
                    writer.Write(i.TriggerPosition.Z);
                    writer.Write(i.TriggerRotX);
                    writer.Write(i.TriggerRotY);
                    writer.Write(i.TriggerRotZ);
                    writer.Write(i.TriggerScale.X);
                    writer.Write(i.TriggerScale.Y);
                    writer.Write(i.TriggerScale.Z);
                    writer.Write(i.CamPos.X);
                    writer.Write(i.CamPos.Y);
                    writer.Write(i.CamPos.Z);
                    writer.Write(i.CamRotX);
                    writer.Write(i.CamRotY);
                    writer.Write(i.CamRotZ);
                    writer.Write(i.PointA.X);
                    writer.Write(i.PointA.Y);
                    writer.Write(i.PointA.Z);
                    writer.Write(i.PointB.X);
                    writer.Write(i.PointB.Y);
                    writer.Write(i.PointB.Z);
                    writer.Write(i.PointC.X);
                    writer.Write(i.PointC.Y);
                    writer.Write(i.PointC.Z);
                    writer.Write(i.Integer30);
                    writer.Write(i.Integer31);
                    writer.Write(i.FloatX32);
                    writer.Write(i.FloatY33);
                    writer.Write(i.FloatX34);
                    writer.Write(i.FloatY35);
                    writer.Write(i.Integer36);
                    writer.Write(i.Integer37);
                    writer.Write(i.Integer38);
                    writer.Write(i.Integer39);
                }
        }
    }
}
