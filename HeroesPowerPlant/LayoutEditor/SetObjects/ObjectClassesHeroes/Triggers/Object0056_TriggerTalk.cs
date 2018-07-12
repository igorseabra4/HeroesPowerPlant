using System;
using static HeroesPowerPlant.SharpRenderer;
using SharpDX;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0056_TriggerTalk : SetObjectManagerHeroes
    {
        private BoundingSphere sphereBound;

        public override bool TriangleIntersection(Ray r, string[] ModelNames)
        {
            if (TriggerShape == TriggerTalkShape.Sphere)
                return r.Intersects(sphereBound);
            else
                return base.TriangleIntersection(r, ModelNames);
        }

        public override BoundingBox CreateBoundingBox(string[] modelNames)
        {
            return new BoundingBox(-Vector3.One / 2, Vector3.One / 2);
        }

        public override void CreateTransformMatrix(Vector3 Position, Vector3 Rotation)
        {
            this.Position = Position;
            this.Rotation = Rotation;

            switch (TriggerShape)
            {
                case TriggerTalkShape.Sphere:
                    sphereBound = new BoundingSphere(Position, Radius_ScaleX);
                    transformMatrix = Matrix.Scaling(Radius_ScaleX * 2);
                    break;
                case TriggerTalkShape.Cube:
                    transformMatrix = Matrix.Scaling(Radius_ScaleX * 2, Height_ScaleY * 2, ScaleZ * 2);
                    break;
                case TriggerTalkShape.Cylinder:
                    transformMatrix = Matrix.Scaling(Radius_ScaleX * 2, Height_ScaleY * 2, Radius_ScaleX * 2);
                    break;
            }

            transformMatrix = transformMatrix
                * Matrix.RotationY(ReadWriteCommon.BAMStoRadians(Rotation.Y))
                * Matrix.RotationX(ReadWriteCommon.BAMStoRadians(Rotation.X))
                * Matrix.RotationZ(ReadWriteCommon.BAMStoRadians(Rotation.Z))
                * Matrix.Translation(Position);
        }
        
        public override void Draw(string[] modelNames, bool isSelected)
        {
            if (TriggerShape == TriggerTalkShape.Sphere)
                DrawSphereTrigger(transformMatrix, isSelected);
            else if (TriggerShape == TriggerTalkShape.Cylinder)
                DrawCylinderTrigger(transformMatrix, isSelected);
            else if (TriggerShape == TriggerTalkShape.Cube)
                DrawCubeTrigger(transformMatrix, isSelected);
            else
                base.Draw(modelNames, isSelected);
        }

        public enum TypeType
        {
            Event = 0,
            Tutorial = 1,
            Hint = 2
        }

        public TypeType Type
        {
            get { return (TypeType)ReadShort(4); }
            set { Write(4, (Int16)value); }
        }

        public Int16 CommonLineToPlay
        {
            get { return ReadShort(6); }
            set { Write(6, value); }
        }

        public enum TriggerTalkShape
        {
            Sphere = 0,
            Cylinder = 1,
            Cube = 2
        }
        public TriggerTalkShape TriggerShape
        {
            get { return (TriggerTalkShape)ReadLong(8); }
            set { Write(8, (int)value); CreateTransformMatrix(Position, Rotation); }
        }

        public float Radius_ScaleX
        {
            get { return ReadFloat(12); }
            set { Write(12, value); CreateTransformMatrix(Position, Rotation); }
        }

        public float Height_ScaleY
        {
            get { return ReadFloat(16); }
            set { Write(16, value); CreateTransformMatrix(Position, Rotation); }
        }

        public float ScaleZ
        {
            get { return ReadFloat(20); }
            set { Write(20, value); CreateTransformMatrix(Position, Rotation); }
        }

        public Int16 HintStart1
        {
            get { return ReadShort(24); }
            set { Write(24, value); }
        }

        public Int16 HintEnd1
        {
            get { return ReadShort(26); }
            set { Write(26, value); }
        }

        public Int16 HintStart2
        {
            get { return ReadShort(28); }
            set { Write(28, value); }
        }

        public Int16 HintEnd2
        {
            get { return ReadShort(30); }
            set { Write(30, value); }
        }

        public Int16 HintStart3
        {
            get { return ReadShort(32); }
            set { Write(32, value); }
        }

        public Int16 HintEnd3
        {
            get { return ReadShort(34); }
            set { Write(34, value); }
        }
    }
}