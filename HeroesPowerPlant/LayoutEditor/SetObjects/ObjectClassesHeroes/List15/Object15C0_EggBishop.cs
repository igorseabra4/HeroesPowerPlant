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
            get { return (TypeEnum)ReadWriteByte(4); }
            set { byte a = (byte)value; ReadWriteByte(4, a); }
        }

        public float MoveRange
        {
            get { return ReadWriteSingle(8); }
            set { ReadWriteSingle(8, value); }
        }

        public float ScopeRange
        {
            get { return ReadWriteSingle(12); }
            set { ReadWriteSingle(12, value); }
        }

        public float ScopeOffset
        {
            get { return ReadWriteSingle(16); }
            set { ReadWriteSingle(16, value); }
        }

        public Int32 AttackInterval
        {
            get { return ReadWriteLong(20); }
            set { ReadWriteLong(20, value); }
        }

        public float MoveSpeed
        {
            get { return ReadWriteSingle(24); }
            set { ReadWriteSingle(24, value); }
        }
    }
}
