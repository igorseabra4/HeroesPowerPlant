using System;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object001D_Pulley : SetObjectManagerHeroes
    {
        public float Elevation
        {
            get { return ReadWriteSingle(4); }
            set { ReadWriteSingle(4, value); }
        }

        public float ElevationAngle
        {
            get { return ReadWriteSingle(8); }
            set { ReadWriteSingle(8, value); }
        }

        public float Power
        {
            get { return ReadWriteSingle(12); }
            set { ReadWriteSingle(12, value); }
        }

        public enum PulleyType
        {
            Up = 0,
            Down = 1
        }

        public PulleyType Type
        {
            get { return (PulleyType)ReadWriteWord(16); }
            set { Int16 a = (Int16)value; ReadWriteWord(16, a); }
        }
    }
}