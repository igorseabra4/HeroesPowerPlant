using HeroesPowerPlant.SplineEditor;
using Newtonsoft.Json;
using SharpDX;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace HeroesPowerPlant.ShadowSplineEditor
{
    public class ShadowSpline : AbstractSpline
    {
        public byte Setting1 { get; set; }
        public byte Setting2 { get; set; }
        public byte SplineType { get; set; }
        public byte Setting4 { get; set; }
        public int SettingInt { get; set; }
        public string Name { get; set; }

        public ShadowSplinePOF0 pof0 { get; set; }

        public ShadowSplineVertex[] Vertices { get; set; }

        public ShadowSpline()
        {
            Vertices = new ShadowSplineVertex[0];
            pof0 = new ShadowSplinePOF0();
            Name = "NewSpline";
        }

        public override string ToString()
        {
            return $"{Name} [{Vertices.Length}]";
        }

        public ShadowSpline GetCopy()
        {
            return new ShadowSpline()
            {
                Setting1 = Setting1,
                Setting2 = Setting2,
                SplineType = SplineType,
                Setting4 = Setting4,
                SettingInt = SettingInt,
                Name = Name,
                pof0 = JsonConvert.DeserializeObject<ShadowSplinePOF0>(JsonConvert.SerializeObject(pof0)),
                Vertices = JsonConvert.DeserializeObject<ShadowSplineVertex[]>(JsonConvert.SerializeObject(Vertices))
            };
        }

        public void SetRenderStuff(SharpRenderer renderer)
        {
            List<Vector3> vertices = new List<Vector3>(Vertices.Length);
            foreach (ShadowSplineVertex v in Vertices)
            {
                vertices.Add(v.Position);
            }

            CreateMesh(renderer, vertices.ToArray());
        }

        public static ShadowSpline FromFile(string FileName, int splineId, string splinePrefix)
        {
            string[] SplineFile = File.ReadAllLines(FileName);
            ShadowSpline Temp = new ShadowSpline();
            List<ShadowSplineVertex> Points = new List<ShadowSplineVertex>();

            foreach (string j in SplineFile)
            {
                if (j.StartsWith("v"))
                {
                    string[] a = Regex.Replace(j, @"\s+", " ").Split();
                    Points.Add(new ShadowSplineVertex()
                    {
                        AngularAttachmentToleranceInt = 4,
                        Position = new Vector3(Convert.ToSingle(a[1]), Convert.ToSingle(a[2]), Convert.ToSingle(a[3])),
                        RotationY = Convert.ToUInt16(a[4]) * (360.0f / 65535),
                        RotationX = Convert.ToUInt16(a[5]) * (360.0f / 65535)
                    });
                }
            }
                    // TODO: FIGURE OUT THE Rotation points logic
/*                    var roll = Convert.ToUInt16(a[4]) * (360.0f / 65535);
                    var pitch = Convert.ToUInt16(a[5]) * (360.0f / 65535);
                    Points.Add(new ShadowSplineVertex() {
                        //TODO: Figure out better conversion
                        //Heroes = "v {v.Position.X} {v.Position.Y} {v.Position.Z} {v.Roll} {v.Pitch}");
                        // old : RotationY = Convert.ToUInt16(a[4]) * (360.0f / 65535), RotationX = Convert.ToUInt16(a[5]) * (360.0f / 65535)
                        // a[4] = Roll
                        // a[5] = Pitch
                        AngularAttachmentToleranceInt = 4, Position = new Vector3(
                            Convert.ToSingle(a[1]), Convert.ToSingle(a[2]), Convert.ToSingle(a[3])),
                        RotationX = pitch,
                        RotationY = -pitch,//(roll + pitch) / 2,
                        RotationZ = roll,//(pitch - roll) / 2
                        //RotationY = (float)Math.Atan2((Math.Sin(Convert.ToUInt16(a[5]) * (360.0f / 65535))) * Math.Cos(Convert.ToUInt16(a[4]) * (360.0f / 65535)), Math.Cos(Convert.ToUInt16(a[5]) * (360.0f / 65535))),
                        //RotationZ = (float)Math.Atan2(-Math.Sin(Convert.ToUInt16(a[4]) * (360.0f / 65535)), Math.Cos(Convert.ToUInt16(a[4]) * (360.0f / 65535)))
                        *//*RotationX = Convert.ToUInt16(a[4]) * (360.0f / 65535),
                                                       RotationY = (float)-Math.Atan2((Math.Sin(Convert.ToUInt16(a[5]) * (360.0f / 65535))) * Math.Cos(Convert.ToUInt16(a[4]) * (360.0f / 65535)), Math.Cos(Convert.ToUInt16(a[5]) * (360.0f / 65535))),
                                                       RotationZ = (float)Math.Atan2(-Math.Sin(Convert.ToUInt16(a[4]) * (360.0f / 65535)), Math.Cos(Convert.ToUInt16(a[4]) * (360.0f / 65535)))*//*
                    });//ReadWriteCommon.BAMStoRadians(Convert.ToSingle(a[5])) * 180f * MathUtil.Pi });
                }
            }
            // Now compute the rotations given all the points
*//*            for (int splRot = 0; splRot < Points.Count; splRot++)
            {
                Points[splRot].Rotation.X = 2;
            }*/

            Points.Last().AngularAttachmentToleranceInt = 0; // last point needs AAT of 0
            Temp.Name = splinePrefix + splineId; 
            Temp.SplineType = 2;
            Temp.SettingInt = splineId;
            Temp.Vertices = Points.ToArray();
            Temp.SetRenderStuff(Program.MainForm.renderer);
            return Temp;
        }

        private static byte[] GetBytes(int value, bool bigEndian)
        {
            byte[] b = BitConverter.GetBytes(value);
            return bigEndian ? b.Reverse().ToArray() : b;
        }

        private static byte[] GetBytes(float value, bool bigEndian)
        {
            byte[] b = BitConverter.GetBytes(value);
            return bigEndian ? b.Reverse().ToArray() : b;
        }

        public IEnumerable<byte> ToByteArray(int startOffset, bool isBigEndian)
        {
            List<byte> vertexBytes = new List<byte>(0x20 * Vertices.Length);

            float totalLength = 0;
            Vector3 Max = Vertices[0].Position;
            Vector3 Min = Vertices[0].Position;

            for (int i = 0; i < Vertices.Length; i++)
            {
                float distance = i == Vertices.Length - 1 ? 0 : Vector3.Distance(Vertices[i].Position, Vertices[i + 1].Position);
                totalLength += distance;

                if (Vertices[i].PositionX > Max.X)
                    Max.X = Vertices[i].Position.X;
                if (Vertices[i].PositionY > Max.Y)
                    Max.Y = Vertices[i].PositionY;
                if (Vertices[i].PositionZ > Max.Z)
                    Max.Z = Vertices[i].PositionZ;
                if (Vertices[i].PositionX < Min.X)
                    Min.X = Vertices[i].PositionX;
                if (Vertices[i].PositionY < Min.Y)
                    Min.Y = Vertices[i].PositionY;
                if (Vertices[i].PositionZ < Min.Z)
                    Min.Z = Vertices[i].PositionZ;

                vertexBytes.AddRange(GetBytes(Vertices[i].PositionX, isBigEndian));
                vertexBytes.AddRange(GetBytes(Vertices[i].PositionY, isBigEndian));
                vertexBytes.AddRange(GetBytes(Vertices[i].PositionZ, isBigEndian));
                vertexBytes.AddRange(GetBytes(Vertices[i].Rotation.X, isBigEndian));
                vertexBytes.AddRange(GetBytes(Vertices[i].Rotation.Y, isBigEndian));
                vertexBytes.AddRange(GetBytes(Vertices[i].Rotation.Z, isBigEndian));
                vertexBytes.AddRange(GetBytes(distance, isBigEndian));
                vertexBytes.AddRange(GetBytes(Vertices[i].AngularAttachmentToleranceInt, isBigEndian));
            }

            List<byte> bytes = new List<byte>(0x30 + 0x20 * Vertices.Length);

            bytes.AddRange(GetBytes(Vertices.Length, isBigEndian));
            bytes.AddRange(GetBytes(totalLength, isBigEndian));
            bytes.AddRange(GetBytes(startOffset + 0x30, isBigEndian));
            int settingsValue = (Setting1 << 24) | (Setting2 << 16) | (SplineType << 8) | Setting4;
            bytes.AddRange(GetBytes(settingsValue, isBigEndian));
            bytes.AddRange(GetBytes(Max.X, isBigEndian));
            bytes.AddRange(GetBytes(Max.Y, isBigEndian));
            bytes.AddRange(GetBytes(Max.Z, isBigEndian));
            bytes.AddRange(GetBytes(SettingInt, isBigEndian));
            bytes.AddRange(GetBytes(Min.X, isBigEndian));
            bytes.AddRange(GetBytes(Min.Y, isBigEndian));
            bytes.AddRange(GetBytes(Min.Z, isBigEndian));
            bytes.AddRange(BitConverter.GetBytes(0));

            bytes.AddRange(vertexBytes);

            return bytes;
        }
    }
}