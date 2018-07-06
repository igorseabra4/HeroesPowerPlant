using System;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object050A_Dice : SetObjectManagerHeroes
    {
        public float Speed
        {
            get { return ReadWriteSingle(4); }
            set { ReadWriteSingle(4, value); }
        }

        public float Height
        {
            get { return ReadWriteSingle(8); }
            set { ReadWriteSingle(8, value); }
        }

        public float Radius
        {
            get { return ReadWriteSingle(12); }
            set { ReadWriteSingle(12, value); }
        }

        public Int16 StopTime
        {
            get { return ReadWriteWord(16); }
            set { ReadWriteWord(16, value); }
        }

        public enum TypeEnum : Int16
        {
            UpDown = 0,
            Horizontal = 1
        }

        public TypeEnum Type
        {
            get { return (TypeEnum)ReadWriteWord(18); }
            set { ReadWriteWord(18, (Int16)value); }
        }

        public Int16 OffsetTime
        {
            get { return ReadWriteWord(20); }
            set { ReadWriteWord(20, value); }
        }
    }
}