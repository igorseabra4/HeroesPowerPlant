using HeroesPowerPlant.Shared.Utilities;
using SharpDX;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object01FF_SetParticle : SetObjectHeroes
    {
        public override bool IsTrigger() => true;

        public override void CreateTransformMatrix()
        {
            Vector3 box = Program.MainForm.ParticleEditor.GetBoxForSetParticle(Number - 50);

            if (box != Vector3.Zero)
            {
                box.X = Math.Max(1f, box.X);
                box.Y = Math.Max(1f, box.Y);
                box.Z = Math.Max(1f, box.Z);

                transformMatrix = Matrix.Scaling(box * 2) * DefaultTransformMatrix();
                CreateBoundingBox();
            }
            else base.CreateTransformMatrix();
        }

        protected override void CreateBoundingBox()
        {
            List<Vector3> list = new List<Vector3>();
            list.AddRange(SharpRenderer.cubeVertices);
            for (int i = 0; i < list.Count; i++)
                list[i] = (Vector3)Vector3.Transform(list[i], transformMatrix);

            boundingBox = BoundingBox.FromPoints(list.ToArray());
        }

        public override void Draw(SharpRenderer renderer)
        {
            renderer.DrawCubeTrigger(transformMatrix, isSelected);
        }

        public override bool TriangleIntersection(Ray r, float initialDistance, out float distance)
        {
            return TriangleIntersection(r, SharpRenderer.cubeTriangles, SharpRenderer.cubeVertices, initialDistance, out distance);
        }

        [Description("Number, if taken from the ptcl file, is offset by 50 (so 50 here is entry 0 in the ptcl).")]
        public byte Number { get; set; }
        public float SpeedX { get; set; }
        public float SpeedY { get; set; }
        public float SpeedZ { get; set; }
        public float UnknownFloat { get; set; }
        public int UnknownInteger { get; set; }
        public byte UnknownByte1 { get; set; }
        public byte UnknownByte2 { get; set; }
        public byte UnknownByte3 { get; set; }

        public override void ReadMiscSettings(EndianBinaryReader reader)
        {
            Number = reader.ReadByte();
            reader.BaseStream.Position += 3;
            SpeedX = reader.ReadSingle();
            SpeedY = reader.ReadSingle();
            SpeedZ = reader.ReadSingle();
            UnknownFloat = reader.ReadSingle();
            UnknownInteger = reader.ReadInt32();
            UnknownByte1 = reader.ReadByte();
            UnknownByte2 = reader.ReadByte();
            UnknownByte3 = reader.ReadByte();
        }

        public override void WriteMiscSettings(EndianBinaryWriter writer)
        {
            writer.Write(Number);
            writer.Pad(3);
            writer.Write(SpeedX);
            writer.Write(SpeedY);
            writer.Write(SpeedZ);
            writer.Write(UnknownFloat);
            writer.Write(UnknownInteger);
            writer.Write(UnknownByte1);
            writer.Write(UnknownByte2);
            writer.Write(UnknownByte3);
        }
    }
}
