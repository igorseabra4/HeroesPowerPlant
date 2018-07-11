namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0503_TriBumper : SetObjectManagerHeroes
    {
        public float Power
        {
            get { return ReadFloat(4); }
            set { Write(4, value); }
        }

        public byte AngType
        {
            get { return ReadByte(8); }
            set { Write(8, value); }
        }

        public byte Color
        {
            get { return ReadByte(9); }
            set { Write(9, value); }
        }

        public byte Number
        {
            get { return ReadByte(10); }
            set { Write(10, value); }
        }
    }
}