namespace HeroesPowerPlant.LayoutEditor
{
    public enum FanMode : byte
    {
        Normal = 0,
        Switchable = 1,
        Normal2 = 2,
        Switchable2 = 3
    }

    public class Object002E_Fan : SetObjectManagerHeroes
    {
        public float Scale
        {
            get { return ReadFloat(4); }
            set { Write(4, value); }
        }

        public float HeightTriangleDive
        {
            get { return ReadFloat(8); }
            set { Write(8, value); }
        }

        public float HeightDefault
        {
            get { return ReadFloat(12); }
            set { Write(12, value); }
        }

        public float Power
        {
            get { return ReadFloat(16); }
            set { Write(16, value); }
        }

        public FanMode Mode
        {
            get { return (FanMode)ReadByte(20); }
            set { Write(20, (byte)value); }
        }

        public byte LinkID
        {
            get { return ReadByte(21); }
            set { Write(21, value); }
        }

        public float WindScale
        {
            get { return ReadFloat(24); }
            set { Write(24, value); }
        }

        public bool IsInvisible
        {
            get { return ReadByte(28) != 0; }
            set { Write(28, (byte)(value ? 1 : 0)); }
        }
    }
}