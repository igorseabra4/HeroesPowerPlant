namespace HeroesPowerPlant.LayoutEditor
{
    public enum FanMode : byte
    {
        Normal = 0,
        Switchable = 1,
        Normal2 = 2,
        Switchable2 = 3
    }

    public class Object002E_Fan : SetObjectHeroes
    {
        public float Scale
        {
            get => ReadFloat(4);
            set => Write(4, value);
        }

        public float HeightTriangleDive
        {
            get => ReadFloat(8);
            set => Write(8, value);
        }

        public float HeightDefault
        {
            get => ReadFloat(12);
            set => Write(12, value);
        }

        public float Power
        {
            get => ReadFloat(16);
            set => Write(16, value);
        }

        public FanMode Mode
        {
            get => (FanMode)ReadByte(20);
            set => Write(20, (byte)value);
        }

        public byte LinkID
        {
            get => ReadByte(21);
            set => Write(21, value);
        }

        public float WindScale
        {
            get => ReadFloat(24);
            set => Write(24, value);
        }

        public bool IsInvisible
        {
            get => ReadByte(28) != 0;
            set => Write(28, (byte)(value ? 1 : 0));
        }
    }
}