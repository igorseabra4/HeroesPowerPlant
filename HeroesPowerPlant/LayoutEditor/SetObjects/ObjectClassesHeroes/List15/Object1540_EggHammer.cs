using System;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object1540_EggHammer : SetObjectManagerHeroes
    {
        public enum TypeEnum : byte
        {
            Normal = 0,
            Helmet = 1
        }
        public TypeEnum Type
        {
            get { return (TypeEnum)ReadWriteByte(4); }
            set { byte a = (byte)value; ReadWriteByte(4, a); }
        }

        public enum AppearEnum : byte
        {
            Idle = 0,
            Fall = 1,
        }
        public AppearEnum Appear
        {
            get { return (AppearEnum)ReadWriteByte(5); }
            set { byte a = (byte)value; ReadWriteByte(5, a); }
        }

        public float MoveSpeed
        {
            get { return ReadWriteSingle(8); }
            set { ReadWriteSingle(8, value); }
        }

        public float MoveRange
        {
            get { return ReadWriteSingle(12); }
            set { ReadWriteSingle(12, value); }
        }

        public float ScopeRange
        {
            get { return ReadWriteSingle(16); }
            set { ReadWriteSingle(16, value); }
        }

        public float ScopeOffset
        {
            get { return ReadWriteSingle(20); }
            set { ReadWriteSingle(20, value); }
        }

        public Int32 AttackInterval
        {
            get { return ReadWriteLong(24); }
            set { ReadWriteLong(24, value); }
        }

        public float WeaponSpeed
        {
            get { return ReadWriteSingle(28); }
            set { ReadWriteSingle(28, value); }
        }

        public float FallDistance
        {
            get { return ReadWriteSingle(32); }
            set { ReadWriteSingle(32, value); }
        }
    }
}
