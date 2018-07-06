using System;
using System.Linq;

namespace HeroesPowerPlant
{
    public class ReadWriteCommon
    {
        public static int Switch(int a)
        {
            return BitConverter.ToInt32(BitConverter.GetBytes(a).Reverse().ToArray(), 0);
        }

        public static uint Switch(uint a)
        {
            return BitConverter.ToUInt32(BitConverter.GetBytes(a).Reverse().ToArray(), 0);
        }

        public static short Switch(short a)
        {
            return BitConverter.ToInt16(BitConverter.GetBytes(a).Reverse().ToArray(), 0);
        }

        public static ushort Switch(ushort a)
        {
            return BitConverter.ToUInt16(BitConverter.GetBytes(a).Reverse().ToArray(), 0);
        }
        
        public static float Switch(float a)
        {
            return BitConverter.ToSingle(BitConverter.GetBytes(a).Reverse().ToArray(), 0);
        }

        public static int[] Range(int a)
        {
            int[] b = new int[a];

            for (int i = 0; i < a; i++)
            {
                b[i] = i;
            }

            return b;
        }

        public static float BAMStoDegrees(int bams)
        {
            return bams * (360f / 65536f);
        }

        public static float BAMStoRadians(int bams)
        {
            return (float)(bams * (Math.PI / 32768f));
        }

        public static int DegreesToBAMS(float degrees)
        {
            return (int)(degrees * 65536f / 360f);
        }
    }
}
