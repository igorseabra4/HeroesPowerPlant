using SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
            Particle bigEndianParticle = StructUtilities.ArrayToStructureUnsafe<Particle>(ref bytes, offset);
            return ConvertEndian(bigEndianParticle);
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
        public static byte[] GetBytesBigEndian(Particle particleEntry)
        {
            Particle newParticleEntry = ConvertEndian(particleEntry);
            return StructUtilities.ConvertStructureToByteArrayUnsafe(ref newParticleEntry); // In HeroesONE-R
        }

        /// <summary>
        /// Converts this instance into a byte array.
        /// </summary>
        public static byte[] GetBytes(Particle particleEntry)
        {
            return StructUtilities.ConvertStructureToByteArrayUnsafe(ref particleEntry); // In HeroesONE-R
        }

        /// <summary>
        /// Changes the endian type from little endian to big endian or vice-versa.
        /// </summary>
        public static Particle ConvertEndian(Particle particle)
        {
            // If the lines below highlight red and complain, don't worry, it'll still build.
            // The constraint on this is a very recent C# 7.3 language feature.
            particle.Unknown3           = particle.Unknown3.ReverseEndian();
            particle.FadeTime           = particle.FadeTime.ReverseEndian();
            particle.BirthDelay         = particle.BirthDelay.ReverseEndian();
            particle.AmountOfParticles  = particle.AmountOfParticles.ReverseEndian();
            particle.Unknown4           = particle.Unknown4.ReverseEndian();
            particle.Unknown5           = particle.Unknown5.ReverseEndian();
            particle.Unknown6           = particle.Unknown6.ReverseEndian();
            particle.Unknown7           = particle.Unknown7.ReverseEndian();
            particle.Velocity           = particle.Velocity.ReverseEndian();
            particle.Unknown8           = particle.Unknown8.ReverseEndian();
            particle.Always05           = particle.Always05.ReverseEndian();
            particle.BlendMode          = particle.BlendMode.ReverseEndian();
            particle.Rotation           = particle.Rotation.ReverseEndian();
            particle.RotateAnimationSpeed = particle.RotateAnimationSpeed.ReverseEndian();
            particle.RotateAnimation    = particle.RotateAnimation.ReverseEndian();
            particle.InverseLifeTime    = particle.InverseLifeTime.ReverseEndian();
            particle.LifeThreshold      = particle.LifeThreshold.ReverseEndian();
            particle.EmitterScaleX      = particle.EmitterScaleX.ReverseEndian();
            particle.EmitterScaleY      = particle.EmitterScaleY.ReverseEndian();
            particle.EmitterScaleZ      = particle.EmitterScaleZ.ReverseEndian();
            particle.SpreadSpeedRate    = particle.SpreadSpeedRate.ReverseEndian();
            particle.VelocityRate       = particle.VelocityRate.ReverseEndian();
            particle.Unknown9           = particle.Unknown9.ReverseEndian();
            particle.Unknown10          = particle.Unknown10.ReverseEndian();
            particle.Unknown11          = particle.Unknown11.ReverseEndian();
            particle.Unknown12          = particle.Unknown12.ReverseEndian();
            particle.ParticleSize       = particle.ParticleSize.ReverseEndian();
            particle.Unknown13          = particle.Unknown13.ReverseEndian();
            particle.SpreadSize         = particle.SpreadSize.ReverseEndian();
            particle.SameAsAbove        = particle.SameAsAbove.ReverseEndian();

            return particle;
        }
    }
}
