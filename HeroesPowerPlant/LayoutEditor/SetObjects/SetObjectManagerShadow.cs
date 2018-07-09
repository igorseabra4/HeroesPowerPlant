using SharpDX;
using System;

namespace HeroesPowerPlant.LayoutEditor
{
    public abstract class SetObjectManagerShadow : SetObjectManager
    {
        public override void CreateTransformMatrix(Vector3 Position, Vector3 Rotation)
        {
            transformMatrix =
                Matrix.RotationX(MathUtil.DegreesToRadians(Rotation.X)) *
                Matrix.RotationY(MathUtil.DegreesToRadians(Rotation.Y)) *
                Matrix.RotationZ(MathUtil.DegreesToRadians(Rotation.Z)) *
                Matrix.Translation(Position);
        }
        
        public float ReadSingle(int j)
        {
            return BitConverter.ToSingle(MiscSettings, j);
        }

        public void Write(int j, float value)
        {
            for (int i = 0; i < 4; i++)
                MiscSettings[j + i] = BitConverter.GetBytes(value)[i];
        }
        
        public int ReadLong(int j)
        {
            return BitConverter.ToInt32(MiscSettings, j);
        }

        public void Write(int j, int value)
        {
            for (int i = 0; i < 4; i++)
                MiscSettings[j + i] = BitConverter.GetBytes(value)[i];
        }
    }
}