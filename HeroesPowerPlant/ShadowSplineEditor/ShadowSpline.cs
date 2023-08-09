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

        // workaround to force Collection editor, should not be an array ideally
        public ShadowSplineSec5Bytes[] UnknownSec5Bytes { get; set; }

        public ShadowSplineVertex[] Vertices { get; set; }

        public ShadowSpline()
        {
            Vertices = new ShadowSplineVertex[0];
            UnknownSec5Bytes = new ShadowSplineSec5Bytes[0];
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
                UnknownSec5Bytes = JsonConvert.DeserializeObject<ShadowSplineSec5Bytes[]>(JsonConvert.SerializeObject(UnknownSec5Bytes)),
                Vertices = JsonConvert.DeserializeObject<ShadowSplineVertex[]>(JsonConvert.SerializeObject(Vertices))
            };
        }

        public void SetRenderStuff(SharpRenderer renderer)
        {
            List<Vector3> vertices = new List<Vector3>(Vertices.Length);
            foreach (ShadowSplineVertex v in Vertices)
                vertices.Add(v.Position);

            CreateMesh(renderer, vertices.ToArray());
        }

        public static ShadowSpline FromFile(string FileName)
        {
            string[] SplineFile = File.ReadAllLines(FileName);
            ShadowSpline Temp = new ShadowSpline();
            List<ShadowSplineVertex> Points = new List<ShadowSplineVertex>();

            foreach (string j in SplineFile)
            {
                if (j.StartsWith("v"))
                {
                    string[] a = Regex.Replace(j, @"\s+", " ").Split();
                    Points.Add(new ShadowSplineVertex() { Position = new Vector3(Convert.ToSingle(a[1]), Convert.ToSingle(a[2]), Convert.ToSingle(a[3])), RotationY = Convert.ToUInt16(a[4]) * (360.0f / 65535), RotationX = Convert.ToUInt16(a[5]) * (360.0f / 65535) });//ReadWriteCommon.BAMStoRadians(Convert.ToSingle(a[5])) * 180f * MathUtil.Pi });
                }
            }

            Temp.Vertices = Points.ToArray();
            Temp.SetRenderStuff(Program.MainForm.renderer);
            return Temp;
        }

        public IEnumerable<byte> ToByteArray(int startOffset)
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

                vertexBytes.AddRange(BitConverter.GetBytes(Vertices[i].PositionX).Reverse());
                vertexBytes.AddRange(BitConverter.GetBytes(Vertices[i].PositionY).Reverse());
                vertexBytes.AddRange(BitConverter.GetBytes(Vertices[i].PositionZ).Reverse());
                vertexBytes.AddRange(BitConverter.GetBytes(Vertices[i].Rotation.X).Reverse());
                vertexBytes.AddRange(BitConverter.GetBytes(Vertices[i].Rotation.Y).Reverse());
                vertexBytes.AddRange(BitConverter.GetBytes(Vertices[i].Rotation.Z).Reverse());
                vertexBytes.AddRange(BitConverter.GetBytes(distance).Reverse());
                vertexBytes.AddRange(BitConverter.GetBytes(Vertices[i].AngularAttachmentToleranceInt).Reverse());
            }

            List<byte> bytes = new List<byte>(0x30 + 0x20 * Vertices.Length);

            bytes.AddRange(BitConverter.GetBytes(Vertices.Length).Reverse());
            bytes.AddRange(BitConverter.GetBytes(totalLength).Reverse());
            bytes.AddRange(BitConverter.GetBytes(startOffset + 0x30).Reverse());
            bytes.Add(Setting1);
            bytes.Add(Setting2);
            bytes.Add(SplineType);
            bytes.Add(Setting4);
            bytes.AddRange(BitConverter.GetBytes(Max.X).Reverse());
            bytes.AddRange(BitConverter.GetBytes(Max.Y).Reverse());
            bytes.AddRange(BitConverter.GetBytes(Max.Z).Reverse());
            bytes.AddRange(BitConverter.GetBytes(SettingInt).Reverse());
            bytes.AddRange(BitConverter.GetBytes(Min.X).Reverse());
            bytes.AddRange(BitConverter.GetBytes(Min.Y).Reverse());
            bytes.AddRange(BitConverter.GetBytes(Min.Z).Reverse());
            bytes.AddRange(BitConverter.GetBytes(0));

            bytes.AddRange(vertexBytes);

            return bytes;
        }
    }
}