using System;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object001D_Pulley : SetObjectManagerHeroes
    {
        public float Elevation
        {
            get { return ReadFloat(4); }
            set { Write(4, value); }
        }

        public float ElevationAngle
        {
            get { return ReadFloat(8); }
            set { Write(8, value); }
        }

        public float Power
        {
            get { return ReadFloat(12); }
            set { Write(12, value); }
        }

        public enum PulleyType
        {
            Up = 0,
            Down = 1
        }

        public PulleyType Type
        {
            get { return (PulleyType)ReadShort(16); }
            set { Int16 a = (Int16)value; Write(16, a); }
        }
    }
}