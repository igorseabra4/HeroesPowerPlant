using System;
using System.ComponentModel;
using System.Linq;
using HeroesONE_R.Utilities;
using HeroesPowerPlant.Shared.Utilities;
using System.Drawing;

namespace HeroesPowerPlant.LightEditor
{
    public unsafe struct Light
    {
        public const int SIZE = 0x34;

        public float Ambient_Red { get; set; }
        public float Ambient_Green { get; set; }
        public float Ambient_Blue { get; set; }
        public float Ambient_Alpha { get; set; }

        [DisplayName("Ambient ([A,] R, G, B)")]
        public Color Ambient
        {
            get
            {
                return Color.FromArgb(
                    (byte)(Ambient_Alpha * 255),
                    (byte)(Ambient_Red * 255),
                    (byte)(Ambient_Green * 255),
                    (byte)(Ambient_Blue * 255));
            }
            set
            {
                Ambient_Red = value.R / 255f;
                Ambient_Green = value.G / 255f;
                Ambient_Blue = value.B / 255f;
                Ambient_Alpha = value.A / 255f;
            }
        }
        public float Directional_Red { get; set; }
        public float Directional_Green { get; set; }
        public float Directional_Blue { get; set; }
        public float Directional_Alpha { get; set; }

        [DisplayName("Directional ([A,] R, G, B)")]
        public Color Directional
        {
            get
            {
                return Color.FromArgb(
                    (byte)(Directional_Alpha * 255),
                    (byte)(Directional_Red * 255),
                    (byte)(Directional_Green * 255),
                    (byte)(Directional_Blue * 255));
            }
            set
            {
                Directional_Red = value.R / 255f;
                Directional_Green = value.G / 255f;
                Directional_Blue = value.B / 255f;
                Directional_Alpha = value.A / 255f;
            }
        }

        public float Unknown_Red { get; set; }
        public float Unknown_Green { get; set; }
        public float Unknown_Blue { get; set; }
        private fixed byte member2C[4];
        private fixed byte member30[4];
        
        public float Unknown_2C_Float
        {
            get => BitConverter.ToSingle(new byte[] { member2C[0], member2C[1], member2C[2], member2C[3] }, 0);
            set
            {
                byte[] bytes = BitConverter.GetBytes(value);
                for (int i = 0; i < bytes.Length; i++)
                    member2C[i] = bytes[i];
            }
        }
        public int VerticalRotation
        {
            get => BitConverter.ToInt32(new byte[] { member2C[0], member2C[1], member2C[2], member2C[3] }, 0);
            set
            {
                byte[] bytes = BitConverter.GetBytes(value);
                for (int i = 0; i < bytes.Length; i++)
                    member2C[i] = bytes[i];
            }
        }
        public float Unknown_30_Float
        {
            get => BitConverter.ToSingle(new byte[] { member30[0], member30[1], member30[2], member30[3] }, 0);
            set
            {
                byte[] bytes = BitConverter.GetBytes(value);
                for (int i = 0; i < bytes.Length; i++)
                    member30[i] = bytes[i];
            }
        }
        public int HorizontalRotation
        {
            get => BitConverter.ToInt32(new byte[] { member30[0], member30[1], member30[2], member30[3] }, 0);
            set
            {
                byte[] bytes = BitConverter.GetBytes(value);
                for (int i = 0; i < bytes.Length; i++)
                    member30[i] = bytes[i];
            }
        }

        public static Light FromBigEndianBytes(ref byte[] bytes, int offset)
        {
            Light bigEndianParticle = StructUtilities.ArrayToStructureUnsafe<Light>(ref bytes, offset);
            return ConvertEndian(bigEndianParticle);
        }

        public static Light FromLittleEndianBytes(ref byte[] bytes, int offset)
        {
            return StructUtilities.ArrayToStructureUnsafe<Light>(ref bytes, offset);
        }

        public static Light FromLightEntry(Light lightEntry)
        {
            return lightEntry.Clone();
        }

        public static Light FromLightEntry(ref Light lightEntry)
        {
            return lightEntry.Clone();
        }

        public static byte[] GetBytesBigEndian(Light lightEntry)
        {
            Light newParticleEntry = ConvertEndian(lightEntry);
            return StructUtilities.ConvertStructureToByteArrayUnsafe(ref newParticleEntry);
        }

        public static byte[] GetBytesLittleEndian(Light particleEntry)
        {
            return StructUtilities.ConvertStructureToByteArrayUnsafe(ref particleEntry);
        }

        public static Light ConvertEndian(Light light)
        {
            light.Ambient_Red = light.Ambient_Red.ReverseEndian();
            light.Ambient_Green = light.Ambient_Green.ReverseEndian();
            light.Ambient_Blue = light.Ambient_Blue.ReverseEndian();
            light.Ambient_Alpha = light.Ambient_Alpha.ReverseEndian();
            light.Directional_Red = light.Directional_Red.ReverseEndian();
            light.Directional_Green = light.Directional_Green.ReverseEndian();
            light.Directional_Blue = light.Directional_Blue.ReverseEndian();
            light.Directional_Alpha = light.Directional_Alpha.ReverseEndian();
            light.Unknown_Red = light.Unknown_Red.ReverseEndian();
            light.Unknown_Green = light.Unknown_Green.ReverseEndian();
            light.Unknown_Blue = light.Unknown_Blue.ReverseEndian();

            byte[] member2CTemp = new byte[] { light.member2C[0], light.member2C[1], light.member2C[2], light.member2C[3] }.Reverse().ToArray();
            for (int i = 0; i < 4; i++)
                light.member2C[i] = member2CTemp[i];

            byte[] member30Temp = new byte[] { light.member30[0], light.member30[1], light.member30[2], light.member30[3] }.Reverse().ToArray();
            for (int i = 0; i < 4; i++)
                light.member30[i] = member30Temp[i];

            return light;
        }
    }
}
