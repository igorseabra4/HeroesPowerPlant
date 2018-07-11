using System;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object15D0_E2000 : SetObjectManagerHeroes
    {
        public enum TypeEnum : byte
        {
            Normal = 0,
            Special = 1
        }
        public TypeEnum Type
        {
            get { return (TypeEnum)ReadByte(4); }
            set { byte a = (byte)value; Write(4, a); }
        }

        public enum AppearEnum : byte
        {
            Idle = 0,
            Fall = 1,
        }
        public AppearEnum Appear
        {
            get { return (AppearEnum)ReadByte(5); }
            set { byte a = (byte)value; Write(5, a); }
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

        public Int32 AttackFrame
        {
            get { return ReadLong(24); }
            set { Write(24, value); }
        }

        public float Distance
        {
            get { return ReadFloat(28); }
            set { Write(28, value); }
        }
    }
}
