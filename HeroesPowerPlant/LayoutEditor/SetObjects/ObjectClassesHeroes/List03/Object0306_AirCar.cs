namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0306_AirCar : SetObjectManagerHeroes
    {
        public byte FarType
        {
            get { return ReadByte(4); }
            set { Write(4, value); }
        }

        public byte BlockType
        {
            get { return ReadByte(5); }
            set { Write(5, value); }
        }

        public byte CrossWize
        {
            get { return ReadByte(6); }
            set { Write(6, value); }
        }

        public byte LengthWize
        {
            get { return ReadByte(7); }
            set { Write(7, value); }
        }

        public short Time
        {
            get { return ReadShort(8); }
            set { Write(8, value); }
        }

        public short TimeRnd
        {
            get { return ReadShort(10); }
            set { Write(10, value); }
        }

        public float Length
        {
            get { return ReadFloat(12); }
            set { Write(12, value); }
        }

        public float Speed
        {
            get { return ReadFloat(16); }
            set { Write(16, value); }
        }

        public float SpeedRnd
        {
            get { return ReadFloat(20); }
            set { Write(20, value); }
        }
    }
}