using SharpDX;
using System.Collections.Generic;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0081_TriggerSE : SetObjectHeroes
    {
        public override bool IsTrigger() => true;

        public enum SE_CALL : byte
        {
            SE_CALL = 0,
            SE_CALL_VP = 1,
            SE_LOOP = 2,
            SE_LOOP_VP = 3,
            SE_SCLOOP = 4,
            SE_LOOP_VP_ = 5
        }

        public enum EShape : byte
        {
            Sphere = 0,
            Cube = 1,
        }

        private BoundingSphere sphereBound;

        public override void CreateTransformMatrix()
        {
            switch (Shape)
            {
                case EShape.Sphere:
                    sphereBound = new BoundingSphere(Position, Radius);
                    transformMatrix = Matrix.Scaling(Radius * 2);
                    break;
                case EShape.Cube:
                    transformMatrix = Matrix.Scaling(ScaleX * 2, ScaleY * 2, ScaleZ * 2);
                    break;
                default:
                    base.CreateTransformMatrix();
                    return;
            }

            transformMatrix *= DefaultTransformMatrix();

            CreateBoundingBox();
        }

        protected override void CreateBoundingBox()
        {
            List<Vector3> list = new List<Vector3>();

            switch (Shape)
            {
                case EShape.Sphere:
                    list.AddRange(SharpRenderer.sphereVertices);
                    break;
                case EShape.Cube:
                    list.AddRange(SharpRenderer.cubeVertices);
                    break;
                default:
                    base.CreateBoundingBox();
                    return;
            }

            for (int i = 0; i < list.Count; i++)
                list[i] = (Vector3)Vector3.Transform(list[i], transformMatrix);

            boundingBox = BoundingBox.FromPoints(list.ToArray());
        }

        public override void Draw(SharpRenderer renderer)
        {
            switch (Shape)
            {
                case EShape.Sphere:
                    renderer.DrawSphereTrigger(transformMatrix, isSelected);
                    break;
                case EShape.Cube:
                    renderer.DrawCubeTrigger(transformMatrix, isSelected);
                    break;
                default:
                    DrawCube(renderer);
                    break;
            }
        }

        public override bool TriangleIntersection(Ray r, float initialDistance, out float distance)
        {
            switch (Shape)
            {
                case EShape.Sphere:
                    return r.Intersects(ref sphereBound, out distance);
                case EShape.Cube:
                    return TriangleIntersection(r, SharpRenderer.cubeTriangles, SharpRenderer.cubeVertices, initialDistance, out distance);
                default:
                    return base.TriangleIntersection(r, initialDistance, out distance);
            }
        }

        [MiscSetting]
        public int SE_ID { get; set; }
        [MiscSetting]
        public SE_CALL CallType { get; set; }
        [MiscSetting(underlyingType: MiscSettingUnderlyingType.Byte)]
        public bool Doppler { get; set; }
        [MiscSetting]
        public byte Volume { get; set; }
        [MiscSetting]
        public EShape Shape { get; set; }
        [MiscSetting]
        public short Time { get; set; }
        [MiscSetting]
        public short Radius { get; set; }
        [MiscSetting]
        public short ScaleX { get; set; }
        [MiscSetting]
        public short ScaleY { get; set; }
        [MiscSetting]
        public short ScaleZ { get; set; }
    }
}