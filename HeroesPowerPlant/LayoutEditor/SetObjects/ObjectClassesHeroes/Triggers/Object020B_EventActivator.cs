using SharpDX;
using System.Collections.Generic;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object020B_EventActivator : SetObjectHeroes
    {
        public override bool IsTrigger() => true;

        public enum EEventType : byte
        {
            NotInUse = 0,
            NotInUse2 = 1,
            Elevator0402 = 2,
            EnergyUp0412 = 3,
            Shutter0410 = 4,
            BallGLSOn0480 = 5,
            BallGLSOff0480 = 6,
            SenkanMov = 7,
            Hakai1320 = 8,
            FallAshiba1400 = 9
        }

        public override void CreateTransformMatrix()
        {
            transformMatrix = Matrix.Scaling(ScaleX, ScaleY, ScaleZ) * DefaultTransformMatrix();
            CreateBoundingBox();
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

        [MiscSetting]
        public float ScaleX { get; set; }
        [MiscSetting]
        public float ScaleY { get; set; }
        [MiscSetting]
        public float ScaleZ { get; set; }
        [MiscSetting]
        public EEventType EventType { get; set; }
        [MiscSetting(underlyingType: MiscSettingUnderlyingType.Byte)]
        public bool OnlyLeader { get; set; }
    }
}
