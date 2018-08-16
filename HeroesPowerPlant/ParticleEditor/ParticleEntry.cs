using SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HeroesPowerPlant.Shared.Utilities;

namespace HeroesPowerPlant.ParticleEditor
{
    public class ParticleEntry
    {
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

        public string TextureName { get; set; }
        //{
        //    get => TextureName;
        //    set => TextureName = ((value.Length > 0x18) ? value.Substring(0, 0x18) : value);
        //}

        /*
            ------------
            Constructors
            ------------
        */

        public ParticleEntry()
        {
            TextureName = "";
        }

        /*
            -------
            Methods
            -------
        */

        public static ParticleEntry FromParticleEntry(ParticleEntry particleEntry)
        {
            return particleEntry.Clone();
        }
    }
}
