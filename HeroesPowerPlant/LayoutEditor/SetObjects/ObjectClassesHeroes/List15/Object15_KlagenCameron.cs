using System;

namespace HeroesPowerPlant.LayoutEditor
{
    public class Object15_KlagenCameron : SetObjectHeroes
    {
        public enum TypeEnum : byte
        {
            Normal = 0,
            Golden = 1
        }
        public TypeEnum EnemyType
        {
            get => (TypeEnum)ReadByte(4);
            set => Write(4, (byte)value);
        }

        public enum AppearEnum : byte
        {
            Idle = 0,
            Walking = 1,
        }
        public AppearEnum Appear
        {
            get => (AppearEnum)ReadByte(5);
            set => Write(5, (byte)value);
        }

        public float MoveRange
        {
            //F2
            get => ReadFloat(8);
            set => Write(8, value);
        }

        public float ScopeRange
        {
            //F3
            get => ReadFloat(12);
            set => Write(12, value);
        }

        public float ScopeOffset
        {
            //F4
            get => ReadFloat(16);
            set => Write(16, value);
        }

        public short Unknown
        {
            get => ReadShort(20);
            set => Write(20, value);
        }

        public short AttackInterval
        {
            get => ReadShort(22);
            set => Write(22, value);
        }

        public float AttackSpeed
        {
            //F6
            get => ReadFloat(24);
            set => Write(24, value);
        }
    }
}
