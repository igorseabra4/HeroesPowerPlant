namespace HeroesPowerPlant.LayoutEditor
{
    public class Object15D0_E2000 : SetObjectHeroes
    {
        public enum TypeEnum : byte
        {
            Normal = 0,
            Special = 1
        }
        public TypeEnum E2000Type
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
            set { byte a = (byte)value; Write(5, a); }
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

        public int AttackFrame
        {
            get => ReadInt(24);
            set => Write(24, value);
        }

        public float Distance
        {
            get => ReadFloat(28);
            set => Write(28, value);
        }
    }
}
