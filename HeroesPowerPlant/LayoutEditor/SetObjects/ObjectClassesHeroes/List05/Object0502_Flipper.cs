namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0502_Flipper : SetObjectManagerHeroes
    {
        public float Trance
        {
            get { return ReadFloat(4); }
            set { Write(4, value); }
        }

        public byte Type
        {
            get { return ReadByte(8); }
            set { Write(8, value); }
        }

        public byte KeyFlip
        {
            get { return ReadByte(9); }
            set { Write(9, value); }
        }

        public byte Power
        {
            get { return ReadByte(10); }
            set { Write(10, value); }
        }

        public byte Player
        {
            get { return ReadByte(11); }
            set { Write(11, value); }
        }
    }
}