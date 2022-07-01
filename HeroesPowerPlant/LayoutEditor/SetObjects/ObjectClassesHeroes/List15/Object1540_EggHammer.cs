namespace HeroesPowerPlant.LayoutEditor
{
    public class Object1540_EggHammer : SetObjectHeroes
    {
        public enum TypeEnum : byte
        {
            Normal = 0,
            Helmet = 1
        }
        public TypeEnum EggHammerType
        {
            get => (TypeEnum)ReadByte(4);
            set => Write(4, (byte)value);
        }

        public enum AppearEnum : byte
        {
            Idle = 0,
            Fall = 1,
        }
        public AppearEnum Appear
        {
            get => (AppearEnum)ReadByte(5);
            set => Write(5, (byte)value);
        }

        public float MoveSpeed
        {
            get => ReadFloat(8);
            set => Write(8, value);
        }

        public float MoveRange
        {
            get => ReadFloat(12);
            set => Write(12, value);
        }

        public float ScopeRange
        {
            get => ReadFloat(16);
            set => Write(16, value);
        }

        public float ScopeOffset
        {
            get => ReadFloat(20);
            set => Write(20, value);
        }

        public int AttackInterval
        {
            get => ReadInt(24);
            set => Write(24, value);
        }

        public float WeaponSpeed
        {
            get => ReadFloat(28);
            set => Write(28, value);
        }

        public float FallDistance
        {
            get => ReadFloat(32);
            set => Write(32, value);
        }
    }
}
