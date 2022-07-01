namespace HeroesPowerPlant.LayoutEditor
{
    public class Object1590_RhinoLiner : SetObjectHeroes
    {
        public enum TypeEnum : byte
        {
            Standard = 0,
            Attack = 1
        }
        public TypeEnum RhinoType
        {
            get => (TypeEnum)ReadByte(4);
            set => Write(4, (byte)value);
        }

        public enum PathEnum : byte
        {
            Standard = 0,
            Loop = 1
        }
        public PathEnum PathMode
        {
            get => (PathEnum)ReadByte(5);
            set => Write(5, (byte)value);
        }

        public enum IronballEnum : byte
        {
            Homing = 0,
            NotHoming = 1
        }
        public IronballEnum IronBallMode
        {
            get => (IronballEnum)ReadByte(6);
            set { byte a = (byte)value; Write(6, a); }
        }

        public float WeaponSpeed
        {
            get => ReadFloat(8);
            set => Write(8, value);
        }

        public int AttackInterval
        {
            get => ReadInt(12);
            set => Write(12, value);
        }

        public float MoveSpeedMin
        {
            get => ReadFloat(16);
            set => Write(16, value);
        }

        public float MoveSpeed
        {
            get => ReadFloat(20);
            set => Write(20, value);
        }

        public float MoveSpeedMax
        {
            get => ReadFloat(24);
            set => Write(24, value);
        }
    }
}
