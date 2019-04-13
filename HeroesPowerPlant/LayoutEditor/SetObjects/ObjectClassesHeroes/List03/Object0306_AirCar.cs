namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0306_AirCar : SetObjectManagerHeroes
    {
        public byte FarType
        {
            get => ReadByte(4);
            set => Write(4, value);
        }

        public byte BlockType
        {
            get => ReadByte(5);
            set => Write(5, value);
        }

        public byte CrossWize
        {
            get => ReadByte(6);
            set => Write(6, value);
        }

        public byte LengthWize
        {
            get => ReadByte(7);
            set => Write(7, value);
        }

        public short Time
        {
            get => ReadShort(8);
            set => Write(8, value);
        }

        public short TimeRnd
        {
            get => ReadShort(10);
            set => Write(10, value);
        }

        public float Length
        {
            get => ReadFloat(12);
            set => Write(12, value);
        }

        public float Speed
        {
            get => ReadFloat(16);
            set => Write(16, value);
        }

        public float SpeedRnd
        {
            get => ReadFloat(20);
            set => Write(20, value);
        }
    }
}