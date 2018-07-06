using System;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object15_KlagenCameron : SetObjectManagerHeroes
    {
        public enum TypeEnum : byte
        {
            Normal = 0,
            Golden = 1
        }
        public TypeEnum Type
        {
            get { return (TypeEnum)ReadWriteByte(4); }
            set { byte a = (byte)value; ReadWriteByte(4, a); }
        }

        public enum AppearEnum : byte
        {
            Idle = 0,
            Walking = 1,
        }
        public AppearEnum Appear
        {
            get { return (AppearEnum)ReadWriteByte(5); }
            set { byte a = (byte)value; ReadWriteByte(5, a); }
        }

        public float MoveRange
        {
            //F2
            get { return ReadWriteSingle(8); }
            set { ReadWriteSingle(8, value); }
        }

        public float ScopeRange
        {
            //F3
            get { return ReadWriteSingle(12); }
            set { ReadWriteSingle(12, value); }
        }

        public float ScopeOffset
        {
            //F4
            get { return ReadWriteSingle(16); }
            set { ReadWriteSingle(16, value); }
        }

        public Int16 Unknown
        {
            get { return ReadWriteWord(20); }
            set { ReadWriteWord(20, value); }
        }

        public Int16 AttackInterval
        {
            get { return ReadWriteWord(22); }
            set { ReadWriteWord(22, value); }
        }

        public float AttackSpeed
        {
            //F6
            get { return ReadWriteSingle(24); }
            set { ReadWriteSingle(24, value); }
        }
    }
}
