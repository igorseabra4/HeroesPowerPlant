using HeroesPowerPlant.Shared.Utilities;
using SharpDX;
using System.Collections.Generic;
using System.ComponentModel;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0059_TriggerLight : SetObjectHeroes
    {
        public override bool IsTrigger() => true;

        public enum ENumber : byte
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

        public enum ETriggerType : byte
        {
            Area = 0,
            Switch = 1,
            AreaDef = 2,
            SwitchOff = 3
        }

        public enum EShape : byte
        {
            Cube = 0,
            Sphere = 1,
            Cylinder = 2,
            NotInUse = 3
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
                case EShape.NotInUse:
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
            if (Shape == EShape.NotInUse)
                base.CreateBoundingBox();
            else
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

        public ENumber Number { get; set; }
        public ETriggerType TriggerType { get; set; }
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

        [Description("Used only for Cube")]
        public float ScaleX { get; set; }

        [Description("Used only for Cube")]
        public float ScaleY { get; set; }

        [Description("Used only for Cube")]
        public float ScaleZ { get; set; }

        public override void ReadMiscSettings(EndianBinaryReader reader)
        {
            Number = (ENumber)reader.ReadByte();
            TriggerType = (ETriggerType)reader.ReadByte();
            Shape = (EShape)reader.ReadByte();
            reader.ReadByte();
            ScaleX = reader.ReadSingle();
            ScaleY = reader.ReadSingle();
            ScaleZ = reader.ReadSingle();
        }

        public override void WriteMiscSettings(EndianBinaryWriter writer)
        {
            writer.Write((byte)Number);
            writer.Write((byte)TriggerType);
            writer.Write((byte)Shape);
            writer.Write((byte)0);
            writer.Write(ScaleX);
            writer.Write(ScaleY);
            writer.Write(ScaleZ);
        }
    }
}