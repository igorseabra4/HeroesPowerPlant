using SharpDX;
using System.Collections.Generic;

namespace HeroesPowerPlant.LayoutEditor
{
    public enum TriggerMusicShape
    {
        Sphere = 0,
        Cylinder = 1,
        Cube = 2
    }

    public class Object110C_TriggerMusic : SetObjectHeroes
    {
        private BoundingSphere sphereBound;

        public override void CreateTransformMatrix()
        {
            switch (TriggerShape)
            {
                case TriggerMusicShape.Sphere:
                    sphereBound = new BoundingSphere(Position, Radius_ScaleX);
                    transformMatrix = Matrix.Scaling(Radius_ScaleX * 2);
                    break;
                case TriggerMusicShape.Cube:
                    transformMatrix = Matrix.Scaling(Radius_ScaleX * 2, Height_ScaleY * 2, ScaleZ * 2);
                    break;
                case TriggerMusicShape.Cylinder:
                    transformMatrix = Matrix.Scaling(Radius_ScaleX * 2, Height_ScaleY * 2, Radius_ScaleX * 2);
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
                case TriggerMusicShape.Sphere:
                    list.AddRange(SharpRenderer.sphereVertices);
                    break;
                case TriggerMusicShape.Cube:
                    list.AddRange(SharpRenderer.cubeVertices);
                    break;
                case TriggerMusicShape.Cylinder:
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
                case TriggerMusicShape.Sphere:
                    renderer.DrawSphereTrigger(transformMatrix, isSelected);
                    break;
                case TriggerMusicShape.Cube:
                    renderer.DrawCubeTrigger(transformMatrix, isSelected);
                    break;
                case TriggerMusicShape.Cylinder:
                    renderer.DrawCylinderTrigger(transformMatrix, isSelected);
                    break;
                default:
                    DrawCube(renderer, isSelected);
                    break;
            }
        }

        public override bool TriangleIntersection(Ray r, float initialDistance, out float distance)
        {
            switch (TriggerShape)
            {
                case TriggerMusicShape.Sphere:
                    return r.Intersects(ref sphereBound, out distance);
                case TriggerMusicShape.Cube:
                    return TriangleIntersection(r, SharpRenderer.cubeTriangles, SharpRenderer.cubeVertices, initialDistance, out distance);
                case TriggerMusicShape.Cylinder:
                    return TriangleIntersection(r, SharpRenderer.cylinderTriangles, SharpRenderer.cylinderVertices, initialDistance, out distance);
                default:
                    return base.TriangleIntersection(r, initialDistance, out distance);
            }
        }

        public short MusicNumber
        {
            get => ReadShort(4);
            set => Write(4, value);
        }

        public TriggerMusicShape TriggerShape
        {
            get => (TriggerMusicShape)ReadInt(8);
            set => Write(8, (int)value);
        }

        public float Radius_ScaleX
        {
            get => ReadFloat(12);
            set => Write(12, value);
        }

        public float Height_ScaleY
        {
            get => ReadFloat(16);
            set => Write(16, value);
        }

        public float ScaleZ
        {
            get => ReadFloat(20);
            set => Write(20, value);
        }
    }
}