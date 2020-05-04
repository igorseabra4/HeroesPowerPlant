using SharpDX;
using System.Collections.Generic;

namespace HeroesPowerPlant.LayoutEditor
{
    public enum TriggerCommonShape : int
    {
        Sphere = 0,
        Cylinder = 1,
        Cube = 2,
        CylinderXZ = 3,
    }

    public class Object00_TriggerCommon : SetObjectHeroes
    {
        private BoundingSphere sphereBound;


        public override void CreateTransformMatrix()
        {
            switch (TriggerShape)
            {
                case TriggerCommonShape.Sphere:
                    sphereBound = new BoundingSphere(Position, Radius);
                    transformMatrix = Matrix.Scaling(Radius * 2);
                    break;
                case TriggerCommonShape.Cube:
                    transformMatrix = Matrix.Scaling(ScaleX * 2, ScaleY * 2, ScaleZ * 2);
                    break;
                case TriggerCommonShape.Cylinder:
                case TriggerCommonShape.CylinderXZ:
                    transformMatrix = Matrix.Scaling(Radius * 2, Height * 2, Radius * 2);
                    break;
            }

            transformMatrix *= DefaultTransformMatrix();

            CreateBoundingBox();
        }

        protected override void CreateBoundingBox()
        {
            List<Vector3> list = new List<Vector3>();

            switch (TriggerShape)
            {
                case TriggerCommonShape.Sphere:
                    list.AddRange(SharpRenderer.sphereVertices);
                    break;
                case TriggerCommonShape.Cube:
                    list.AddRange(SharpRenderer.cubeVertices);
                    break;
                case TriggerCommonShape.Cylinder:
                case TriggerCommonShape.CylinderXZ:
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
            switch (TriggerShape)
            {
                case TriggerCommonShape.Sphere:
                    renderer.DrawSphereTrigger(transformMatrix, isSelected);
                    break;
                case TriggerCommonShape.Cube:
                    renderer.DrawCubeTrigger(transformMatrix, isSelected);
                    break;
                case TriggerCommonShape.Cylinder:
                case TriggerCommonShape.CylinderXZ:
                    renderer.DrawCylinderTrigger(transformMatrix, isSelected);
                    break;
                default:
                    DrawCube(renderer);
                    break;
            }
        }

        public override bool TriangleIntersection(Ray r, float initialDistance, out float distance)
        {
            switch (TriggerShape)
            {
                case TriggerCommonShape.Sphere:
                    return r.Intersects(ref sphereBound, out distance);
                case TriggerCommonShape.Cube:
                    return TriangleIntersection(r, SharpRenderer.cubeTriangles, SharpRenderer.cubeVertices, initialDistance, out distance);
                case TriggerCommonShape.Cylinder:
                case TriggerCommonShape.CylinderXZ:
                    return TriangleIntersection(r, SharpRenderer.cylinderTriangles, SharpRenderer.cylinderVertices, initialDistance, out distance);
                default:
                    return base.TriangleIntersection(r, initialDistance, out distance);
            }
        }

        public TriggerCommonShape TriggerShape
        {
            get => (TriggerCommonShape)ReadInt(4);
            set => Write(4, (int)value);
        }

        public float Radius
        {
            get => ReadFloat(8);
            set => Write(8, value);
        }

        public float Height
        {
            get => ReadFloat(12);
            set => Write(12, value);
        }

        public float ScaleX
        {
            get => ReadFloat(8);
            set => Write(8, value);
        }

        public float ScaleY
        {
            get => ReadFloat(12);
            set => Write(12, value);
        }

        public float ScaleZ
        {
            get => ReadFloat(16);
            set => Write(16, value);
        }
    }
}