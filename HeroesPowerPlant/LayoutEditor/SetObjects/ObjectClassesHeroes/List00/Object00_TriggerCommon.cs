using System;
using SharpDX;
using static HeroesPowerPlant.SharpRenderer;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object00_TriggerCommon : SetObjectManagerHeroes
    {
        public override BoundingBox CreateBoundingBox(string[] modelNames)
        {
            return new BoundingBox(-Vector3.One, Vector3.One);

            if (Type == TypeType.Sphere)
            {
                return BoundingBox.FromSphere(new BoundingSphere(Vector3.Zero, Radius_ScaleX));
            }
            else if (Type == TypeType.Cylinder)
            {
                return new BoundingBox(new Vector3(-1, -1, -1) / 2, new Vector3(Radius_ScaleX, Height_ScaleY, Radius_ScaleX) / 2);
            }
            else if (Type == TypeType.Rectangle)
            {
                return new BoundingBox(new Vector3(-1, -1, -ScaleZ) / 2, new Vector3(Radius_ScaleX, Height_ScaleY, ScaleZ) / 2);
            }
            else if (Type == TypeType.CylinderXZ)
            {
                return new BoundingBox(new Vector3(-1, -Height_ScaleY, -Radius_ScaleX) / 2, new Vector3(Radius_ScaleX, Height_ScaleY, Radius_ScaleX) / 2);
            }
            throw new Exception();
        }

        public override void CreateTransformMatrix(Vector3 Position, Vector3 Rotation)
        {
            if (Type == TypeType.Sphere)
            {
                transformMatrix = Matrix.Scaling(Radius_ScaleX)
                    * Matrix.RotationY(ReadWriteCommon.BAMStoRadians(Rotation.Y))
                    * Matrix.RotationX(ReadWriteCommon.BAMStoRadians(Rotation.X))
                    * Matrix.RotationZ(ReadWriteCommon.BAMStoRadians(Rotation.Z))
                    * Matrix.Translation(Position);
            }
            else if (Type == TypeType.Cylinder)
            {
                transformMatrix = Matrix.Scaling(Radius_ScaleX, Height_ScaleY, Radius_ScaleX)
                    * Matrix.RotationY(ReadWriteCommon.BAMStoRadians(Rotation.Y))
                    * Matrix.RotationX(ReadWriteCommon.BAMStoRadians(Rotation.X))
                    * Matrix.RotationZ(ReadWriteCommon.BAMStoRadians(Rotation.Z))
                    * Matrix.Translation(Position);
            }
            else if (Type == TypeType.Rectangle)
            {
                transformMatrix = Matrix.Scaling(Radius_ScaleX, Height_ScaleY, ScaleZ)
                    * Matrix.RotationY(ReadWriteCommon.BAMStoRadians(Rotation.Y))
                    * Matrix.RotationX(ReadWriteCommon.BAMStoRadians(Rotation.X))
                    * Matrix.RotationZ(ReadWriteCommon.BAMStoRadians(Rotation.Z))
                    * Matrix.Translation(Position);
            }
            else if (Type == TypeType.CylinderXZ)
            {
                transformMatrix = Matrix.Scaling(Radius_ScaleX, Height_ScaleY, Radius_ScaleX)
                    * Matrix.RotationY(ReadWriteCommon.BAMStoRadians(Rotation.Y))
                    * Matrix.RotationX(ReadWriteCommon.BAMStoRadians(Rotation.X))
                    * Matrix.RotationZ(ReadWriteCommon.BAMStoRadians(Rotation.Z))
                    * Matrix.Translation(Position);
            }
        }

        public override void Draw(string[] modelNames, bool isSelected)
        {
            if (Type == TypeType.Sphere)
            {
                DrawSphere(transformMatrix, isSelected);
            }
            else if (Type == TypeType.Cylinder)
            {
                DrawCylinder(transformMatrix, isSelected);
            }
            else if (Type == TypeType.Rectangle)
            {
                DrawCube(transformMatrix, isSelected);
            }
            else if (Type == TypeType.CylinderXZ)
            {
                DrawCylinder(transformMatrix, isSelected);
            }
        }

        public enum TypeType : Int32
        {
            Sphere = 0,
            Cylinder = 1,
            Rectangle = 2,
            CylinderXZ = 3,
        }

        public TypeType Type
        {
            get { return (TypeType)ReadWriteLong(4); }
            set { Int32 a = (Int32)value; ReadWriteLong(4, a); }
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