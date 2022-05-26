namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0206_ScrollBalloon : SetObjectHeroes
    {
        public Item Item
        {
            get => (Item)ReadByte(4);
            set => Write(4, (byte)value);
        }

        public float Scale
        {
            get => ReadFloat(8);
            set => Write(8, value);
        }

        public float Speed
        {
            get => ReadFloat(12);
            set => Write(12, value);
        }

        public float EndX
        {
            get => ReadFloat(16);
            set => Write(16, value);
        }

        public float EndY
        {
            get => ReadFloat(20);
            set => Write(20, value);
        }

        public float EndZ
        {
            get => ReadFloat(24);
            set => Write(24, value);
        }

        public short InvokeOffX
        {
            get => ReadShort(28);
            set => Write(28, value);
        }

        public short InvokeOffY
        {
            get => ReadShort(30);
            set => Write(30, value);
        }

        public short InvokeOffZ
        {
            get => ReadShort(32);
            set => Write(32, value);
        }

        public short InvokeSize
        {
            get => ReadShort(34);
            set => Write(34, value);
        }
    }
}
