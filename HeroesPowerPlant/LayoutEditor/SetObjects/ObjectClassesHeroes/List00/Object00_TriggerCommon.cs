using System;
using SharpDX;
using static HeroesPowerPlant.SharpRenderer;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object00_TriggerCommon : SetObjectManagerHeroes
    {
        public override bool TriangleIntersection(Ray r, string[] ModelNames)
        {
            if (TriggerShape == TriggerCommonShape.Sphere)
            {
                Vector3 center = Vector3.Zero;
                center = (Vector3)Vector3.Transform(center, transformMatrix);

                return r.Intersects(new BoundingSphere(center, Radius_ScaleX / 2));
            }
            else
                return base.TriangleIntersection(r, ModelNames);
        }

        public override BoundingBox CreateBoundingBox(string[] modelNames)
        {
            return new BoundingBox(-Vector3.One / 2, Vector3.One / 2);
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