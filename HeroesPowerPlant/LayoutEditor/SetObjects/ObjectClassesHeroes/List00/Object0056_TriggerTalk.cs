using System;
using System.Collections.Generic;
using static HeroesPowerPlant.SharpRenderer;
using SharpDX;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0056_TriggerTalk : SetObjectManagerHeroes
    {
        public override BoundingBox CreateBoundingBox(string[] modelNames)
        {
            return new BoundingBox(-Vector3.One, Vector3.One);

            if (TriggerShape == TriggerTalkShapes.Sphere)
            {
                return BoundingBox.FromSphere(new BoundingSphere(Vector3.Zero, Radius_ScaleX));
            }
            else if (TriggerShape == TriggerTalkShapes.Cylinder)
            {
                return new BoundingBox(new Vector3(-Radius_ScaleX, -Height_ScaleY, -Radius_ScaleX) / 2, new Vector3(Radius_ScaleX, Height_ScaleY, Radius_ScaleX) / 2);
            }
            else if (TriggerShape == TriggerTalkShapes.Cube)
            {
                return new BoundingBox(new Vector3(-Radius_ScaleX, -Height_ScaleY, -ScaleZ) / 2, new Vector3(Radius_ScaleX, Height_ScaleY, ScaleZ) / 2);
            }
            throw new Exception();
        }

        public override void CreateTransformMatrix(Vector3 Position, int XRot, int YRot, int ZRot)
        {
            transformMatrix =
                Matrix.RotationY((float)(YRot * (Math.PI / 32768f)))
                * Matrix.RotationX((float)(XRot * (Math.PI / 32768f)))
                * Matrix.RotationZ((float)(ZRot * (Math.PI / 32768f)))
                * Matrix.Translation(Position);
        }

        public override void Draw(string[] modelNames, bool isSelected)
        {
            if (TriggerShape == TriggerTalkShapes.Sphere)
            {
                DrawSphere(Matrix.Scaling(Radius_ScaleX) * transformMatrix, isSelected);
            }
            else if (TriggerShape == TriggerTalkShapes.Cylinder)
            {
                DrawCylinder(Matrix.Scaling(Radius_ScaleX, Height_ScaleY, Radius_ScaleX) * transformMatrix, isSelected);
            }
            else if (TriggerShape == TriggerTalkShapes.Cube)
            {
                DrawCube(Matrix.Scaling(Radius_ScaleX, Height_ScaleY, ScaleZ) * transformMatrix, isSelected);
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

        public enum TriggerTalkShapes
        {
            Sphere = 0,
            Cylinder = 1,
            Cube = 2
        }
        public TriggerTalkShapes TriggerShape
        {
            get { return (TriggerTalkShapes)ReadWriteLong(8); }
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