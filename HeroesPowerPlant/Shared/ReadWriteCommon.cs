using System;

namespace HeroesPowerPlant
{
    public class ReadWriteCommon
    {
        public static int[] Range(int a)
        {
            int[] b = new int[a];

            for (int i = 0; i < a; i++)
            {
                b[i] = i;
            }

            return b;
        }

        public static ushort[] Range(ushort a)
        {
            ushort[] b = new ushort[a];

            for (ushort i = 0; i < a; i++)
            {
                b[i] = i;
            }

            return b;
        }

        public static float BAMStoDegrees(float bams)
        {
            return bams * (180f / 32768f);
        }

        public static float BAMStoRadians(float bams)
        {
            return (float)(bams * (Math.PI / 32768f));
        }

        public static int DegreesToBAMS(float degrees)
        {
            return (int)(degrees * 32768f / 180f);
        }
    }
}
