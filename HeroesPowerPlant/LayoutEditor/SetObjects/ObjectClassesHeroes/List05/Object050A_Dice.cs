using System;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object050A_Dice : SetObjectManagerHeroes
    {
        public float Speed
        {
            get { return ReadFloat(4); }
            set { Write(4, value); }
        }

        public float Height
        {
            get { return ReadFloat(8); }
            set { Write(8, value); }
        }

        public float Radius
        {
            get { return ReadFloat(12); }
            set { Write(12, value); }
        }

        public Int16 StopTime
        {
            get { return ReadShort(16); }
            set { Write(16, value); }
        }

        public enum TypeEnum : Int16
        {
            UpDown = 0,
            Horizontal = 1
        }

        public TypeEnum Type
        {
            get { return (TypeEnum)ReadShort(18); }
            set { Write(18, (Int16)value); }
        }

        public Int16 OffsetTime
        {
            get { return ReadShort(20); }
            set { Write(20, value); }
        }
    }
}