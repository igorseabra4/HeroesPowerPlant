using SharpDX;
using System.Collections.Generic;

namespace HeroesPowerPlant.LayoutEditor
{
    public enum TriggerLightNumber
    {
        Player0 = 0,
        Player1 = 1,
        Player2 = 2,
        Player3 = 3,
        Object0 = 4,
        Object1 = 5,
        Object2 = 6,
        Object3 = 7,
        Enemy0 = 8,
        Enemy1 = 9,
        Enemy2 = 10,
        Enemy3 = 11,
        NotInUnse = 12,
        Other0 = 13,
        Other1 = 14,
        Other2 = 15,
        Ignore = 16
    }

    public enum TriggerLightType
    {
        Area = 0,
        Switch = 1,
        AreaDef = 2,
        SwitchOff = 3
    }

    public enum TriggerLightShape
    {
        Cube = 0,
        Sphere = 1,
        Cylinder = 2,
        NotInUse = 3
    }

    public class Object0059_TriggerLight : SetObjectHeroes
    {
        private BoundingSphere sphereBound;

        public override void CreateTransformMatrix()
        {
            switch (TriggerShape)
            {
                case TriggerLightShape.Sphere:
                    sphereBound = new BoundingSphere(Position, Radius);
                    transformMatrix = Matrix.Scaling(Radius * 2);
                    break;
                case TriggerLightShape.Cube:
                    transformMatrix = Matrix.Scaling(ScaleX * 2, ScaleY * 2, ScaleZ * 2);
                    break;
                case TriggerLightShape.Cylinder:
                    transformMatrix = Matrix.Scaling(Radius * 2, Height * 2, Radius * 2);
                    break;
                case TriggerLightShape.NotInUse:
                    base.CreateTransformMatrix();
                    return;
            }

            transformMatrix = transformMatrix
                * Matrix.RotationZ(ReadWriteCommon.BAMStoRadians(Rotation.Z))
                * Matrix.RotationY(ReadWriteCommon.BAMStoRadians(Rotation.Y))
                * Matrix.RotationX(ReadWriteCommon.BAMStoRadians(Rotation.X))
                * Matrix.Translation(Position);

            CreateBoundingBox();
        }

        protected override void CreateBoundingBox()
        {
            if (TriggerShape == TriggerLightShape.NotInUse)
                base.CreateBoundingBox();
            else
            {
                List<Vector3> list = new List<Vector3>();

                switch (TriggerShape)
                {
                    case TriggerLightShape.Sphere:
                        list.AddRange(SharpRenderer.sphereVertices);
                        break;
                    case TriggerLightShape.Cube:
                        list.AddRange(SharpRenderer.cubeVertices);
                        break;
                    case TriggerLightShape.Cylinder:
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
        }

        public override void Draw(SharpRenderer renderer)
        {
            switch (TriggerShape)
            {
                case TriggerLightShape.Sphere:
                    renderer.DrawSphereTrigger(transformMatrix, isSelected);
                    break;
                case TriggerLightShape.Cube:
                    renderer.DrawCubeTrigger(transformMatrix, isSelected);
                    break;
                case TriggerLightShape.Cylinder:
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
                case TriggerLightShape.Sphere:
                    return r.Intersects(ref sphereBound, out distance);
                case TriggerLightShape.Cube:
                    return TriangleIntersection(r, SharpRenderer.cubeTriangles, SharpRenderer.cubeVertices, initialDistance, out distance);
                case TriggerLightShape.Cylinder:
                    return TriangleIntersection(r, SharpRenderer.cylinderTriangles, SharpRenderer.cylinderVertices, initialDistance, out distance);
                default:
                    return base.TriangleIntersection(r, initialDistance, out distance);
            }
        }

        public TriggerLightNumber Number
        {
            get => (TriggerLightNumber)ReadByte(4);
            set => Write(4, (byte)value);
        }

        public TriggerLightType TriggerLightType
        {
            get => (TriggerLightType)ReadByte(5);
            set => Write(5, (byte)value);
        }

        public TriggerLightShape TriggerShape
        {
            get => (TriggerLightShape)ReadByte(6);
            set => Write(6, (byte)value);
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