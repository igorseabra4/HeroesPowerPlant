using System;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object000D_BigRings : SetObjectManagerHeroes
    {
        public enum RainbowType : Int16
        {
            Speed = 0,
            FlyA = 1,
            FlyB = 2,
            PowerS = 3,
            PowerL = 4
        }

        public RainbowType Type
        {
            get { return (RainbowType)ReadShort(4); }
            set { Int16 a = (Int16)value; Write(4, a); }
        }

        public Int16 AdditionalControlTime
        {
            get { return (Int16)ReadShort(6); }
            set { Write(6, value); }
        }

        public float Speed
        {
            get { return ReadFloat(8); }
            set { Write(8, value); }
        }

        public float Offset
        {
            get { return ReadFloat(12); }
            set { Write(12, value); }
        }

    }
}