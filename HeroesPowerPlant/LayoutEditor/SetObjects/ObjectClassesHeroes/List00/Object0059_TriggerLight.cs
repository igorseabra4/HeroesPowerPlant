using SharpDX;
using System;
using static HeroesPowerPlant.SharpRenderer;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0059_TriggerLight : SetObjectManagerHeroes
    {
        public override BoundingBox CreateBoundingBox(string[] modelNames)
        {
            return new BoundingBox(-Vector3.One, Vector3.One);

            if (Shape == ShapeEnum.Cube | Shape == ShapeEnum.NotInUse)
            {
                return new BoundingBox(new Vector3(-Radius_ScaleX, -Height_ScaleY, -ScaleZ) / 2, new Vector3(Radius_ScaleX, Height_ScaleY, ScaleZ) / 2);
            }
            else if (Shape == ShapeEnum.Sphere)
            {
                return BoundingBox.FromSphere(new BoundingSphere(Vector3.Zero, Radius_ScaleX));
            }
            else if (Shape == ShapeEnum.Cylinder)
            {
                return new BoundingBox(new Vector3(-Radius_ScaleX, -Height_ScaleY, -Radius_ScaleX) / 2, new Vector3(Radius_ScaleX, Height_ScaleY, Radius_ScaleX) / 2);
            }
            throw new Exception();
        }

        public override void CreateTransformMatrix(Vector3 Position, Vector3 Rotation)
        {
            if (Shape == ShapeEnum.Cube)
            {
                transformMatrix = Matrix.Scaling(Radius_ScaleX, Height_ScaleY, ScaleZ)
                    * Matrix.RotationY(ReadWriteCommon.BAMStoRadians(Rotation.Y))
                    * Matrix.RotationX(ReadWriteCommon.BAMStoRadians(Rotation.X))
                    * Matrix.RotationZ(ReadWriteCommon.BAMStoRadians(Rotation.Z))
                    * Matrix.Translation(Position);
            }
            else if (Shape == ShapeEnum.Sphere)
            {
                transformMatrix = Matrix.Scaling(Radius_ScaleX)
                    * Matrix.RotationY(ReadWriteCommon.BAMStoRadians(Rotation.Y))
                    * Matrix.RotationX(ReadWriteCommon.BAMStoRadians(Rotation.X))
                    * Matrix.RotationZ(ReadWriteCommon.BAMStoRadians(Rotation.Z))
                    * Matrix.Translation(Position);
            }
            else if (Shape == ShapeEnum.Cylinder)
            {
                transformMatrix = Matrix.Scaling(Radius_ScaleX, Height_ScaleY, Radius_ScaleX)
                    * Matrix.RotationY(ReadWriteCommon.BAMStoRadians(Rotation.Y))
                    * Matrix.RotationX(ReadWriteCommon.BAMStoRadians(Rotation.X))
                    * Matrix.RotationZ(ReadWriteCommon.BAMStoRadians(Rotation.Z))
                    * Matrix.Translation(Position);
            }
            else if (Shape == ShapeEnum.NotInUse)
            {
                transformMatrix = 
                      Matrix.RotationY(ReadWriteCommon.BAMStoRadians(Rotation.Y))
                    * Matrix.RotationX(ReadWriteCommon.BAMStoRadians(Rotation.X))
                    * Matrix.RotationZ(ReadWriteCommon.BAMStoRadians(Rotation.Z))
                    * Matrix.Translation(Position);
            }
        }

        public override void Draw(string[] modelNames, bool isSelected)
        {
            if (Shape == ShapeEnum.Cube | Shape == ShapeEnum.NotInUse)
            {
                DrawCube(transformMatrix, isSelected);
            }
            else if (Shape == ShapeEnum.Sphere)
            {
                DrawSphere(transformMatrix, isSelected);
            }
            else if (Shape == ShapeEnum.Cylinder)
            {
                DrawCylinder(transformMatrix, isSelected);
            }
        }

        public enum NumberEnum
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

        public NumberEnum Number
        {
            get { return (NumberEnum)ReadWriteByte(4); }
            set { ReadWriteByte(4, (byte)value); }
        }

        public enum TypeEnum
        {
            Area = 0,
            Switch = 1,
            AreaDef = 2,
            SwitchOff = 3
        }
        public TypeEnum Type
        {
            get { return (TypeEnum)ReadWriteByte(5); }
            set { ReadWriteByte(5, (byte)value); }
        }

        public enum ShapeEnum
        {
            Cube = 0,
            Sphere = 1,
            Cylinder = 2,
            NotInUse = 3
        }
        public ShapeEnum Shape
        {
            get { return (ShapeEnum)ReadWriteByte(6); }
            set { ReadWriteByte(6, (byte)value); }
        }

        public float Radius_ScaleX
        {
            get { return ReadWriteSingle(8); }
            set { ReadWriteSingle(8, value); }
        }

        public float Height_ScaleY
        {
            get { return ReadWriteSingle(12); }
            set { ReadWriteSingle(12, value); }
        }

        public float ScaleZ
        {
            get { return ReadWriteSingle(16); }
            set { ReadWriteSingle(16, value); }
        }
    }
}