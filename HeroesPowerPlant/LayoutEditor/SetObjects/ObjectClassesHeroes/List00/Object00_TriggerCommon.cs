using System;
using SharpDX;
using static HeroesPowerPlant.SharpRenderer;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object00_TriggerCommon : SetObjectManagerHeroes
    {
        public override BoundingBox CreateBoundingBox(string[] modelNames)
        {
            if (TriggerShape == TriggerCommonShape.Sphere)
            {
                return BoundingBox.FromSphere(new BoundingSphere(Vector3.Zero, Radius_ScaleX));
            }
            else if (TriggerShape == TriggerCommonShape.Cylinder)
            {
                return new BoundingBox(new Vector3(-Radius_ScaleX, -Height_ScaleY, -Radius_ScaleX) / 2, new Vector3(Radius_ScaleX, Height_ScaleY, Radius_ScaleX) / 2);
            }
            else if (TriggerShape == TriggerCommonShape.Cube)
            {
                return new BoundingBox(new Vector3(-Radius_ScaleX, -Height_ScaleY, -ScaleZ) / 2, new Vector3(Radius_ScaleX, Height_ScaleY, ScaleZ) / 2);
            }
            else if (TriggerShape == TriggerCommonShape.CylinderXZ)
            {
                return new BoundingBox(new Vector3(-Radius_ScaleX, -Height_ScaleY, -Radius_ScaleX) / 2, new Vector3(Radius_ScaleX, Height_ScaleY, Radius_ScaleX) / 2);
            }
            throw new Exception();
        }

        public override void CreateTransformMatrix(Vector3 Position, Vector3 Rotation)
        {
            if (TriggerShape == TriggerCommonShape.Sphere)
            {
                transformMatrix = Matrix.Scaling(Radius_ScaleX)
                    * Matrix.RotationY(ReadWriteCommon.BAMStoRadians(Rotation.Y))
                    * Matrix.RotationX(ReadWriteCommon.BAMStoRadians(Rotation.X))
                    * Matrix.RotationZ(ReadWriteCommon.BAMStoRadians(Rotation.Z))
                    * Matrix.Translation(Position);
            }
            else if (TriggerShape == TriggerCommonShape.Cylinder)
            {
                transformMatrix = Matrix.Scaling(Radius_ScaleX, Height_ScaleY, Radius_ScaleX)
                    * Matrix.RotationY(ReadWriteCommon.BAMStoRadians(Rotation.Y))
                    * Matrix.RotationX(ReadWriteCommon.BAMStoRadians(Rotation.X))
                    * Matrix.RotationZ(ReadWriteCommon.BAMStoRadians(Rotation.Z))
                    * Matrix.Translation(Position);
            }
            else if (TriggerShape == TriggerCommonShape.Cube)
            {
                transformMatrix = Matrix.Scaling(Radius_ScaleX, Height_ScaleY, ScaleZ)
                    * Matrix.RotationY(ReadWriteCommon.BAMStoRadians(Rotation.Y))
                    * Matrix.RotationX(ReadWriteCommon.BAMStoRadians(Rotation.X))
                    * Matrix.RotationZ(ReadWriteCommon.BAMStoRadians(Rotation.Z))
                    * Matrix.Translation(Position);
            }
            else if (TriggerShape == TriggerCommonShape.CylinderXZ)
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
            if (TriggerShape == TriggerCommonShape.Sphere)
            {
                DrawSphereTrigger(transformMatrix, isSelected);
            }
            else if (TriggerShape == TriggerCommonShape.Cylinder)
            {
                DrawCylinder(transformMatrix, isSelected);
            }
            else if (TriggerShape == TriggerCommonShape.Cube)
            {
                DrawCubeTrigger(transformMatrix, isSelected);
            }
            else if (TriggerShape == TriggerCommonShape.CylinderXZ)
            {
                DrawCylinder(transformMatrix, isSelected);
            }
        }

        public enum TriggerCommonShape : Int32
        {
            Sphere = 0,
            Cylinder = 1,
            Cube = 2,
            CylinderXZ = 3,
        }

        public TriggerCommonShape TriggerShape
        {
            get { return (TriggerCommonShape)ReadWriteLong(4); }
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