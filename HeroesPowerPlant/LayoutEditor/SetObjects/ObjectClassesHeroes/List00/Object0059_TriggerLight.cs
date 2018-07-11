using SharpDX;
using System;
using static HeroesPowerPlant.SharpRenderer;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0059_TriggerLight : SetObjectManagerHeroes
    {
        public override bool TriangleIntersection(Ray r, string[] ModelNames)
        {
            if (TriggerShape == TriggerLightShape.Sphere)
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
            if (TriggerShape == TriggerLightShape.NotInUse)
                return base.CreateBoundingBox(modelNames);
            else
                return new BoundingBox(-Vector3.One / 2, Vector3.One / 2);
        }

        public override void CreateTransformMatrix(Vector3 Position, Vector3 Rotation)
        {
            if (TriggerShape == TriggerLightShape.Cube)
            {
                transformMatrix = Matrix.Scaling(Radius_ScaleX, Height_ScaleY, ScaleZ)
                    * Matrix.RotationY(ReadWriteCommon.BAMStoRadians(Rotation.Y))
                    * Matrix.RotationX(ReadWriteCommon.BAMStoRadians(Rotation.X))
                    * Matrix.RotationZ(ReadWriteCommon.BAMStoRadians(Rotation.Z))
                    * Matrix.Translation(Position);
            }
            else if (TriggerShape == TriggerLightShape.Sphere)
            {
                transformMatrix = Matrix.Scaling(Radius_ScaleX)
                    * Matrix.RotationY(ReadWriteCommon.BAMStoRadians(Rotation.Y))
                    * Matrix.RotationX(ReadWriteCommon.BAMStoRadians(Rotation.X))
                    * Matrix.RotationZ(ReadWriteCommon.BAMStoRadians(Rotation.Z))
                    * Matrix.Translation(Position);
            }
            else if (TriggerShape == TriggerLightShape.Cylinder)
            {
                transformMatrix = Matrix.Scaling(Radius_ScaleX, Height_ScaleY, Radius_ScaleX)
                    * Matrix.RotationY(ReadWriteCommon.BAMStoRadians(Rotation.Y))
                    * Matrix.RotationX(ReadWriteCommon.BAMStoRadians(Rotation.X))
                    * Matrix.RotationZ(ReadWriteCommon.BAMStoRadians(Rotation.Z))
                    * Matrix.Translation(Position);
            }
            else if (TriggerShape == TriggerLightShape.NotInUse)
            {
                base.CreateTransformMatrix(Position, Rotation);
            }
        }

        public override void Draw(string[] modelNames, bool isSelected)
        {
            if (TriggerShape == TriggerLightShape.Cube)
            {
                DrawCubeTrigger(transformMatrix, isSelected);
            }
            else if (TriggerShape == TriggerLightShape.Sphere)
            {
                DrawSphereTrigger(transformMatrix, isSelected);
            }
            else if (TriggerShape == TriggerLightShape.Cylinder)
            {
                DrawCylinder(transformMatrix, isSelected);
            }
            else if (TriggerShape ==  TriggerLightShape.NotInUse)
            {
                base.Draw(modelNames, isSelected);
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

        public enum TriggerLightShape
        {
            Cube = 0,
            Sphere = 1,
            Cylinder = 2,
            NotInUse = 3
        }

        public TriggerLightShape TriggerShape
        {
            get { return (TriggerLightShape)ReadWriteByte(6); }
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