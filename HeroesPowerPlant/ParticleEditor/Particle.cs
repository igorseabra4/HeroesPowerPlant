using SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HeroesONE_R.Utilities;
using HeroesPowerPlant.Shared.Utilities;

namespace HeroesPowerPlant.ParticleEditor
{
    public unsafe struct Particle
    {
        /// <summary>
        /// The size of this structure.
        /// </summary>
        public const int SIZE = 0x80;

        public byte SpreadUVType { get; set; }
        public byte UvFrameType { get; set; }
        public byte Unknown1 { get; set; }
        public byte Unknown2 { get; set; }
        public int  Unknown3 { get; set; }
        public byte ColorR { get; set; }
        public byte ColorG { get; set; }
        public byte ColorB { get; set; }
        public byte ColorA { get; set; }
        public int  FadeTime { get; set; }
        public short BirthDelay { get; set; }
        public short AmountOfParticles { get; set; }
        public short Unknown4 { get; set; }
        public short Unknown5 { get; set; }
        public short Unknown6 { get; set; }
        public short Unknown7 { get; set; }
        public short Velocity { get; set; }
        public short Unknown8 { get; set; }

        public short Always05 { get; set; }
        public short BlendMode { get; set; }
        public int   Rotation { get; set; }
        public short RotateAnimationSpeed { get; set; }
        public short RotateAnimation { get; set; }
        public float InverseLifeTime { get; set; }
        public float LifeThreshold { get; set; }
        public float EmitterScaleX { get; set; }
        public float EmitterScaleY { get; set; }
        public float EmitterScaleZ { get; set; }

        public float SpreadSpeedRate { get; set; }
        public float VelocityRate { get; set; }
        public float Unknown9 { get; set; }
        public float Unknown10 { get; set; }
        public float Unknown11 { get; set; }
        public float Unknown12 { get; set; }
        public float ParticleSize { get; set; }
        public float Unknown13 { get; set; }

        public float SpreadSize { get; set; }
        public float SameAsAbove { get; set; }
        private fixed byte _textureName[0x18];

        public string TextureName
        {
            get
            {
                fixed (byte* fileName = _textureName)
                    return StringUtilities.CharPointerToString(fileName);
            }
            set
            {
                fixed (byte* fileNamePointer = _textureName)
                    StringUtilities.StringToCharPointer(value, fileNamePointer);
            }
        }

        /*
            -------
            Methods
            -------
        */

        /// <summary>
        /// Returns a particle from a specified array of bytes.
        /// </summary>
        /// <param name="bytes">The bytes to return a particle from.</param>
        /// <param name="offset">The offset into the byte array to get hte particle from.</param>
        /// <returns></returns>
        public static Particle FromBytes(ref byte[] bytes, int offset)
        {
            return StructUtilities.ArrayToStructureUnsafe<Particle>(ref bytes, offset);
        }

        public static Particle FromParticleEntry(Particle particleEntry)
        {
            return particleEntry.Clone();
        }

        public static Particle FromParticleEntry(ref Particle particleEntry)
        {
            return particleEntry.Clone();
        }

        /// <summary>
        /// Converts this instance into a byte array.
        /// </summary>
        public static byte[] GetBytes(Particle particleEntry)
        {
            return StructUtilities.ConvertStructureToByteArrayUnsafe(ref particleEntry); // In HeroesONE-R
        }
    }
}
