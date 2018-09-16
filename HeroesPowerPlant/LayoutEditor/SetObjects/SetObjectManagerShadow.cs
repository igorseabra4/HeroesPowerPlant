using SharpDX;
using System;
using System.Collections.Generic;

namespace HeroesPowerPlant.LayoutEditor
{
    public abstract class SetObjectManagerShadow : SetObjectManager
    {
        public int[] MiscSettingInts
        {
            get
            {
                List<int> result = new List<int>();
                for (int i = 0; i < MiscSettings.Length; i += 4)
                {
                    result.Add(BitConverter.ToInt32(MiscSettings, i));
                }
                return result.ToArray();
            }
            set
            {
                List<byte> result = new List<byte>();
                foreach (int i in value)
                    result.AddRange(BitConverter.GetBytes(i));
                MiscSettings = result.ToArray();
            }
        }

        public float[] MiscSettingFloats
        {
            get
            {
                List<float> result = new List<float>();
                for (int i = 0; i < MiscSettings.Length; i += 4)
                {
                    result.Add(BitConverter.ToSingle(MiscSettings, i));
                }
                return result.ToArray();
            }
            set
            {
                List<byte> result = new List<byte>();
                foreach (float i in value)
                    result.AddRange(BitConverter.GetBytes(i));
                MiscSettings = result.ToArray();
            }
        }

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