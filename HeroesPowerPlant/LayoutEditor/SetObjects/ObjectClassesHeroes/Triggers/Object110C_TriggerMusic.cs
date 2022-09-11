using SharpDX;
using System.Collections.Generic;
using System.ComponentModel;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object110C_TriggerMusic : SetObjectHeroes
    {
        public override bool IsTrigger() => true;

        public enum EShape : int
        {
            Sphere = 0,
            Cylinder = 1,
            Cube = 2
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
                case EShape.Cylinder:
                    transformMatrix = Matrix.Scaling(Radius * 2, Height * 2, Radius * 2);
                    break;
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
                case EShape.Cylinder:
                    list.AddRange(SharpRenderer.cylinderVertices);
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
                case EShape.Cylinder:
                    renderer.DrawCylinderTrigger(transformMatrix, isSelected);
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
                case EShape.Cylinder:
                    return TriangleIntersection(r, SharpRenderer.cylinderTriangles, SharpRenderer.cylinderVertices, initialDistance, out distance);
                default:
                    return base.TriangleIntersection(r, initialDistance, out distance);
            }
        }

        [MiscSetting(padAfter: 2)]
        public short MusicNumber { get; set; }
        [MiscSetting]
        public EShape Shape { get; set; }

        [Description("Used only for Sphere and Cylinder")]
        public float Radius
        {
            get => ScaleX;
            set => ScaleX = value;
        }

        [Description("Used only for Cylinder")]
        public float Height
        {
            get => ScaleY;
            set => ScaleY = value;
        }

        [MiscSetting, Description("Used only for Cube")]
        public float ScaleX { get; set; }

        [MiscSetting, Description("Used only for Cube")]
        public float ScaleY { get; set; }

        [MiscSetting, Description("Used only for Cube")]
        public float ScaleZ { get; set; }
    }
}