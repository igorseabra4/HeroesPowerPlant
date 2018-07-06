namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0502_Flipper : SetObjectManagerHeroes
    {
        public float Trance
        {
            get { return ReadWriteSingle(4); }
            set { ReadWriteSingle(4, value); }
        }

        public byte Type
        {
            get { return ReadWriteByte(8); }
            set { ReadWriteByte(8, value); }
        }

        public byte KeyFlip
        {
            get { return ReadWriteByte(9); }
            set { ReadWriteByte(9, value); }
        }

        public byte Power
        {
            get { return ReadWriteByte(10); }
            set { ReadWriteByte(10, value); }
        }

        public byte Player
        {
            get { return ReadWriteByte(11); }
            set { ReadWriteByte(11, value); }
        }
    }
}