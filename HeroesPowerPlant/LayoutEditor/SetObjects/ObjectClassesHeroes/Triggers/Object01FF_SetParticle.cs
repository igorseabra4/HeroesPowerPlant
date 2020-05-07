using SharpDX;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object01FF_SetParticle : SetObjectHeroes
    {
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
        public byte Number
        {
            get => ReadByte(4);
            set => Write(4, value);
        }

        public float SpeedX
        {
            get => ReadFloat(8);
            set => Write(8, value);
        }

        public float SpeedY
        {
            get => ReadFloat(12);
            set => Write(12, value);
        }

        public float SpeedZ
        {
            get => ReadFloat(16);
            set => Write(16, value);
        }

        public float UnknownFloat
        {
            get => ReadFloat(20);
            set => Write(20, value);
        }

        public int UnknownInteger
        {
            get => ReadInt(24);
            set => Write(24, value);
        }

        public byte UnknownByte1
        {
            get => ReadByte(28);
            set => Write(28, value);
        }

        public byte UnknownByte2
        {
            get => ReadByte(29);
            set => Write(29, value);
        }

        public byte UnknownByte3
        {
            get => ReadByte(30);
            set => Write(30, value);
        }
    }
}
