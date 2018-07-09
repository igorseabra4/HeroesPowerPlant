namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0306_AirCar : SetObjectManagerHeroes
    {
        public byte FarType
        {
            get { return ReadWriteByte(4); }
            set { ReadWriteByte(4, value); }
        }

        public byte BlockType
        {
            get { return ReadWriteByte(5); }
            set { ReadWriteByte(5, value); }
        }

        public byte CrossWize
        {
            get { return ReadWriteByte(6); }
            set { ReadWriteByte(6, value); }
        }

        public byte LengthWize
        {
            get { return ReadWriteByte(7); }
            set { ReadWriteByte(7, value); }
        }

        public short Time
        {
            get { return ReadWriteWord(8); }
            set { ReadWriteWord(8, value); }
        }

        public short TimeRnd
        {
            get { return ReadWriteWord(10); }
            set { ReadWriteWord(10, value); }
        }

        public float Length
        {
            get { return ReadWriteSingle(12); }
            set { ReadWriteSingle(12, value); }
        }

        public float Speed
        {
            get { return ReadWriteSingle(16); }
            set { ReadWriteSingle(16, value); }
        }

        public float SpeedRnd
        {
            get { return ReadWriteSingle(20); }
            set { ReadWriteSingle(20, value); }
        }
    }
}