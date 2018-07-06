using System;
using SharpDX;
using static HeroesPowerPlant.SharpRenderer;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object020B_EventActivator : SetObjectManagerHeroes
    {
        public override void CreateTransformMatrix(Vector3 Position, int XRot, int YRot, int ZRot)
        {
            transformMatrix = Matrix.Scaling(ScaleX, ScaleY, ScaleZ)
                    * Matrix.RotationX((float)(XRot * (Math.PI / 32768f)))
                    * Matrix.RotationY((float)(YRot * (Math.PI / 32768f)))
                    * Matrix.RotationZ((float)(ZRot * (Math.PI / 32768f)))
                    * Matrix.Translation(Position);
        }

        public override void Draw(string[] modelNames, bool isSelected)
        {
            DrawCube(transformMatrix, isSelected);
        }

        public float ScaleX
        {
            get { return ReadWriteSingle(4); }
            set { ReadWriteSingle(4, value); }
        }

        public float ScaleY
        {
            get { return ReadWriteSingle(8); }
            set { ReadWriteSingle(8, value); }
        }

        public float ScaleZ
        {
            get { return ReadWriteSingle(12); }
            set { ReadWriteSingle(12, value); }
        }

        public enum TypeType
        {
            NotInUse = 0,
            NotInUse2 = 1,
            Elevator0402 = 2,
            EnergyUp0412 = 3,
            Shutter0410 = 4,
            BallGLSOn0480 = 5,
            BallGLSOff0480 = 6,
            SenkanMov = 7,
            Hakai1320 = 8,
            FallAshiba1400 = 9
        }
        public TypeType Type
        {
            get { return (TypeType)ReadWriteByte(16); }
            set { ReadWriteSingle(16, (byte)value); }
        }

        public bool OnlyLeader
        {
            get { return ReadWriteByte(17) != 0; }
            set { ReadWriteByte(17, value ? (byte)1 : (byte)0); }
        }
    }
}
