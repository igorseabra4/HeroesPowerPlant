using SharpDX;
using System;

namespace HeroesPowerPlant.LayoutEditor
{
    public abstract class SetObjectManagerHeroes : SetObjectManager
    {
        public override void CreateTransformMatrix(Vector3 Position, Vector3 Rotation)
        {
            transformMatrix =
                Matrix.RotationX(ReadWriteCommon.BAMStoRadians((int)Rotation.X)) *
                Matrix.RotationY(ReadWriteCommon.BAMStoRadians((int)Rotation.Y)) *
                Matrix.RotationZ(ReadWriteCommon.BAMStoRadians((int)Rotation.Z)) *
                Matrix.Translation(Position);
        }

        public float ReadWriteSingle(int j)
        {
            return BitConverter.ToSingle(new byte[] { MiscSettings[j + 3], MiscSettings[j + 2], MiscSettings[j + 1], MiscSettings[j] }, 0);
        }

        public void ReadWriteSingle(int j, float value)
        {
            MiscSettings[j] = BitConverter.GetBytes(value)[3];
            MiscSettings[j + 1] = BitConverter.GetBytes(value)[2];
            MiscSettings[j + 2] = BitConverter.GetBytes(value)[1];
            MiscSettings[j + 3] = BitConverter.GetBytes(value)[0];
        }

        public short ReadWriteWord(int j)
        {
            return BitConverter.ToInt16(new byte[] { MiscSettings[j + 1], MiscSettings[j] }, 0);
        }

        public void ReadWriteWord(int j, Int16 value)
        {
            MiscSettings[j] = BitConverter.GetBytes(value)[1];
            MiscSettings[j + 1] = BitConverter.GetBytes(value)[0];
        }

        public byte ReadWriteByte(int j)
        {
            return MiscSettings[j];
        }

        public void ReadWriteByte(int j, byte value)
        {
            MiscSettings[j] = value;
        }

        public int ReadWriteLong(int j)
        {
            return BitConverter.ToInt32(new byte[] { MiscSettings[j + 3], MiscSettings[j + 2], MiscSettings[j + 1], MiscSettings[j] }, 0);
        }

        public void ReadWriteLong(int j, Int32 value)
        {
            MiscSettings[j] = BitConverter.GetBytes(value)[3];
            MiscSettings[j + 1] = BitConverter.GetBytes(value)[2];
            MiscSettings[j + 2] = BitConverter.GetBytes(value)[1];
            MiscSettings[j + 3] = BitConverter.GetBytes(value)[0];
        }
    }
}