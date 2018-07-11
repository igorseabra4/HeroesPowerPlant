using System;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object15C0_EggBishop : SetObjectManagerHeroes
    {
        public enum TypeEnum : byte
        {
            Bishop = 0,
            Magician = 1
        }
        public TypeEnum Type
        {
            get { return (TypeEnum)ReadByte(4); }
            set { byte a = (byte)value; Write(4, a); }
        }

        public float MoveRange
        {
            get { return ReadFloat(8); }
            set { Write(8, value); }
        }

        public float ScopeRange
        {
            get { return ReadFloat(12); }
            set { Write(12, value); }
        }

        public float ScopeOffset
        {
            get { return ReadFloat(16); }
            set { Write(16, value); }
        }

        public Int32 AttackInterval
        {
            get { return ReadLong(20); }
            set { Write(20, value); }
        }

        public float MoveSpeed
        {
            get { return ReadFloat(24); }
            set { Write(24, value); }
        }
    }
}
