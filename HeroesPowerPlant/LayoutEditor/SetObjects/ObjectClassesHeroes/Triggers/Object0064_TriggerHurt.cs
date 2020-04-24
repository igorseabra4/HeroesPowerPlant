using SharpDX;
using System.Collections.Generic;

namespace HeroesPowerPlant.LayoutEditor
{
    public enum TriggerHurtShape : int
    {
        Sphere = 0,
        Cube = 1,
        Cylinder = 2
    }

    public class Object0064_TriggerHurt : SetObjectHeroes
    {
        private BoundingSphere sphereBound;

        public override void CreateTransformMatrix()
        {
            switch (TriggerShape)
            {
                case TriggerHurtShape.Sphere:
                    sphereBound = new BoundingSphere(Position, Radius);
                    transformMatrix = Matrix.Scaling(Radius * 2);
                    break;
                case TriggerHurtShape.Cube:
                    transformMatrix = Matrix.Scaling(ScaleX * 2, ScaleY * 2, ScaleZ * 2);
                    break;
                case TriggerHurtShape.Cylinder:
                    transformMatrix = Matrix.Scaling(Radius * 2, Height * 2, Radius * 2);
                    break;
            }

            transformMatrix = transformMatrix
                * Matrix.RotationY(ReadWriteCommon.BAMStoRadians(Rotation.Y))
                * Matrix.RotationX(ReadWriteCommon.BAMStoRadians(Rotation.X))
                * Matrix.RotationZ(ReadWriteCommon.BAMStoRadians(Rotation.Z))
                * Matrix.Translation(Position);

            CreateBoundingBox();
        }

        protected override void CreateBoundingBox()
        {
            List<Vector3> list = new List<Vector3>();

            switch (TriggerShape)
            {
                case TriggerHurtShape.Sphere:
                    list.AddRange(SharpRenderer.sphereVertices);
                    break;
                case TriggerHurtShape.Cube:
                    list.AddRange(SharpRenderer.cubeVertices);
                    break;
                case TriggerHurtShape.Cylinder:
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
                case TriggerHurtShape.Sphere:
                    renderer.DrawSphereTrigger(transformMatrix, isSelected);
                    break;
                case TriggerHurtShape.Cube:
                    renderer.DrawCubeTrigger(transformMatrix, isSelected);
                    break;
                case TriggerHurtShape.Cylinder:
                    renderer.DrawCylinderTrigger(transformMatrix, isSelected);
                    break;
                default:
                    DrawCube(renderer, isSelected);
                    break;
            }
        }

        public override bool TriangleIntersection(Ray r, float initialDistance, out float distance)
        {
            return TriggerShape switch
            {
                TriggerHurtShape.Sphere => r.Intersects(ref sphereBound, out distance),
                TriggerHurtShape.Cube => TriangleIntersection(r, SharpRenderer.cubeTriangles, SharpRenderer.cubeVertices, initialDistance, out distance),
                TriggerHurtShape.Cylinder => TriangleIntersection(r, SharpRenderer.cylinderTriangles, SharpRenderer.cylinderVertices, initialDistance, out distance),
                _ => base.TriangleIntersection(r, initialDistance, out distance),
            };
        }

        public TriggerHurtShape TriggerShape
        {
            get => (TriggerHurtShape)ReadInt(4);
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