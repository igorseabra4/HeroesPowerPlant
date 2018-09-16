using SharpDX;
using System;
using System.Linq;

namespace HeroesPowerPlant.LayoutEditor
{
    public abstract class SetObjectManagerHeroes : SetObjectManager
    {
        public override void CreateTransformMatrix(Vector3 Position, Vector3 Rotation)
        {
            this.Position = Position;
            this.Rotation = Rotation;

            transformMatrix =
                Matrix.RotationX(ReadWriteCommon.BAMStoRadians((int)Rotation.X)) *
                Matrix.RotationY(ReadWriteCommon.BAMStoRadians((int)Rotation.Y)) *
                Matrix.RotationZ(ReadWriteCommon.BAMStoRadians((int)Rotation.Z)) *
                Matrix.Translation(Position);
        }

        public float ReadFloat(int j) => BitConverter.ToSingle(new byte[] { MiscSettings[j + 3], MiscSettings[j + 2], MiscSettings[j + 1], MiscSettings[j] }, 0);

        public byte ReadByte(int j) => MiscSettings[j];

        public short ReadShort(int j) => BitConverter.ToInt16(new byte[] { MiscSettings[j + 1], MiscSettings[j] }, 0);

        public int ReadLong(int j) => BitConverter.ToInt32(new byte[] { MiscSettings[j + 3], MiscSettings[j + 2], MiscSettings[j + 1], MiscSettings[j] }, 0);

        public void Write(int j, float value)
        {
            byte[] split = BitConverter.GetBytes(value).Reverse().ToArray();
            for (int i = 0; i < 4; i++)
                MiscSettings[j + i] = split[i];
        }

        public void Write(int j, byte value)
        {
            MiscSettings[j] = value;
        }

        public void Write(int j, short value)
        {
            byte[] split = BitConverter.GetBytes(value).Reverse().ToArray();
            for (int i = 0; i < 2; i++)
                MiscSettings[j + i] = split[i];
        }

        public void Write(int j, int value)
        {
            byte[] split = BitConverter.GetBytes(value).Reverse().ToArray();
            for (int i = 0; i < 4; i++)
                MiscSettings[j + i] = split[i];
        }
    }
}