using System;
using static HeroesPowerPlant.SharpRenderer;
using SharpDX;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0056_TriggerTalk : SetObjectManagerHeroes
    {
        public override bool TriangleIntersection(Ray r, string[] ModelNames)
        {
            if (TriggerShape == TriggerTalkShape.Sphere)
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
            if (TriggerShape == TriggerTalkShape.Sphere)
            {
                transformMatrix = Matrix.Scaling(Radius_ScaleX)
                    * Matrix.RotationY(ReadWriteCommon.BAMStoRadians(Rotation.Y))
                    * Matrix.RotationX(ReadWriteCommon.BAMStoRadians(Rotation.X))
                    * Matrix.RotationZ(ReadWriteCommon.BAMStoRadians(Rotation.Z))
                    * Matrix.Translation(Position);
            }
            else if (TriggerShape == TriggerTalkShape.Cylinder)
            {
                transformMatrix = Matrix.Scaling(Radius_ScaleX, Height_ScaleY, Radius_ScaleX)
                    * Matrix.RotationY(ReadWriteCommon.BAMStoRadians(Rotation.Y))
                    * Matrix.RotationX(ReadWriteCommon.BAMStoRadians(Rotation.X))
                    * Matrix.RotationZ(ReadWriteCommon.BAMStoRadians(Rotation.Z))
                    * Matrix.Translation(Position);
            }
            else if (TriggerShape == TriggerTalkShape.Cube)
            {
                transformMatrix = Matrix.Scaling(Radius_ScaleX, Height_ScaleY, ScaleZ)
                    * Matrix.RotationY(ReadWriteCommon.BAMStoRadians(Rotation.Y))
                    * Matrix.RotationX(ReadWriteCommon.BAMStoRadians(Rotation.X))
                    * Matrix.RotationZ(ReadWriteCommon.BAMStoRadians(Rotation.Z))
                    * Matrix.Translation(Position);
            }
        }
        
        public override void Draw(string[] modelNames, bool isSelected)
        {
            if (TriggerShape == TriggerTalkShape.Sphere)
            {
                DrawSphereTrigger(transformMatrix, isSelected);
            }
            else if (TriggerShape == TriggerTalkShape.Cylinder)
            {
                DrawCylinder(transformMatrix, isSelected);
            }
            else if (TriggerShape == TriggerTalkShape.Cube)
            {
                DrawCubeTrigger(transformMatrix, isSelected);
            }
        }

        public enum TypeType
        {
            Event = 0,
            Tutorial = 1,
            Hint = 2
        }
        public TypeType Type
        {
            get { return (TypeType)ReadWriteWord(4); }
            set { ReadWriteWord(4, (Int16)value); }
        }

        public Int16 CommonLineToPlay
        {
            get { return ReadWriteWord(6); }
            set { ReadWriteWord(6, value); }
        }

        public enum TriggerTalkShape
        {
            Sphere = 0,
            Cylinder = 1,
            Cube = 2
        }
        public TriggerTalkShape TriggerShape
        {
            get { return (TriggerTalkShape)ReadWriteLong(8); }
            set { ReadWriteLong(8, (int)value); }
        }

        public float Radius_ScaleX
        {
            get { return ReadWriteSingle(12); }
            set { ReadWriteSingle(12, value); }
        }

        public float Height_ScaleY
        {
            get { return ReadWriteSingle(16); }
            set { ReadWriteSingle(16, value); }
        }

        public float ScaleZ
        {
            get { return ReadWriteSingle(20); }
            set { ReadWriteSingle(20, value); }
        }

        public Int16 HintStart1
        {
            get { return ReadWriteWord(24); }
            set { ReadWriteWord(24, value); }
        }

        public Int16 HintEnd1
        {
            get { return ReadWriteWord(26); }
            set { ReadWriteWord(26, value); }
        }

        public Int16 HintStart2
        {
            get { return ReadWriteWord(28); }
            set { ReadWriteWord(28, value); }
        }

        public Int16 HintEnd2
        {
            get { return ReadWriteWord(30); }
            set { ReadWriteWord(30, value); }
        }

        public Int16 HintStart3
        {
            get { return ReadWriteWord(32); }
            set { ReadWriteWord(32, value); }
        }

        public Int16 HintEnd3
        {
            get { return ReadWriteWord(34); }
            set { ReadWriteWord(34, value); }
        }
    }
}