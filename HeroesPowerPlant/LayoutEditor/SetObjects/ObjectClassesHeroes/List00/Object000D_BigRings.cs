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
            get { return (RainbowType)ReadWriteWord(4); }
            set { Int16 a = (Int16)value; ReadWriteWord(4, a); }
        }

        public Int16 AdditionalControlTime
        {
            get { return (Int16)ReadWriteWord(6); }
            set { ReadWriteWord(6, value); }
        }

        public float Speed
        {
            get { return ReadWriteSingle(8); }
            set { ReadWriteSingle(8, value); }
        }

        public float Offset
        {
            get { return ReadWriteSingle(12); }
            set { ReadWriteSingle(12, value); }
        }

    }
}