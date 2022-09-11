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
            else
                base.CreateTransformMatrix();
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

        [MiscSetting, Description("Number, if taken from the ptcl file, is offset by 50 (so 50 here is entry 0 in the ptcl).")]
        public byte Number { get; set; }
        [MiscSetting]
        public float SpeedX { get; set; }
        [MiscSetting]
        public float SpeedY { get; set; }
        [MiscSetting]
        public float SpeedZ { get; set; }
        [MiscSetting]
        public float UnknownFloat { get; set; }
        [MiscSetting]
        public int UnknownInteger { get; set; }
        [MiscSetting]
        public byte UnknownByte1 { get; set; }
        [MiscSetting]
        public byte UnknownByte2 { get; set; }
        [MiscSetting]
        public byte UnknownByte3 { get; set; }
    }
}
