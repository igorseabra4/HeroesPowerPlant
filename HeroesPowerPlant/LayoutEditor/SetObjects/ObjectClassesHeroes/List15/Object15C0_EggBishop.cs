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
            get => (TypeEnum)ReadByte(4);
            set => Write(4, (byte)value);
        }

        public float MoveRange
        {
            get => ReadFloat(8);
            set => Write(8, value);
        }

        public float ScopeRange
        {
            get => ReadFloat(12);
            set => Write(12, value);
        }

        public float ScopeOffset
        {
            get => ReadFloat(16);
            set => Write(16, value);
        }

        public int AttackInterval
        {
            get => ReadInt(20);
            set => Write(20, value);
        }

        public float MoveSpeed
        {
            get => ReadFloat(24);
            set => Write(24, value);
        }
    }
}
