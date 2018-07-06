namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0503_TriBumper : SetObjectManagerHeroes
    {
        public float Power
        {
            get { return ReadWriteSingle(4); }
            set { ReadWriteSingle(4, value); }
        }

        public byte AngType
        {
            get { return ReadWriteByte(8); }
            set { ReadWriteByte(8, value); }
        }

        public byte Color
        {
            get { return ReadWriteByte(9); }
            set { ReadWriteByte(9, value); }
        }

        public byte Number
        {
            get { return ReadWriteByte(10); }
            set { ReadWriteByte(10, value); }
        }
    }
}