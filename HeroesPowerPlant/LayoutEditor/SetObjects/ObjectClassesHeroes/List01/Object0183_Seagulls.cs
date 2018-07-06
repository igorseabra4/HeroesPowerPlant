namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0183_Seagulls : SetObjectManagerHeroes
    {
        public byte Number
        {
            get { return ReadWriteByte(4); }
            set { ReadWriteByte(4, value); }
        }

        public float Radius
        {
            get { return ReadWriteSingle(8); }
            set { ReadWriteSingle(8, value); }
        }

        public float Speed
        {
            get { return ReadWriteSingle(12); }
            set { ReadWriteSingle(12, value); }
        }
    }
}
