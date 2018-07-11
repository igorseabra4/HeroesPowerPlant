namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0183_Seagulls : SetObjectManagerHeroes
    {
        public byte Number
        {
            get { return ReadByte(4); }
            set { Write(4, value); }
        }

        public float Radius
        {
            get { return ReadFloat(8); }
            set { Write(8, value); }
        }

        public float Speed
        {
            get { return ReadFloat(12); }
            set { Write(12, value); }
        }
    }
}
