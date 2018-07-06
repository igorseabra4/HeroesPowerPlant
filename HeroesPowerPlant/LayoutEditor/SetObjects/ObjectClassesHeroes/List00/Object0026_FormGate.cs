namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0026_FormGate : SetObjectManagerHeroes
    {
        public Formations Formation
        {
            get { return (Formations)ReadWriteByte(4); }
            set { byte a = (byte)value; ReadWriteByte(4, a); }
        }

        public float Width
        {
            get { return ReadWriteSingle(8); }
            set { ReadWriteSingle(8, value); }
        }

        public float Height
        {
            get { return ReadWriteSingle(12); }
            set { ReadWriteSingle(12, value); }
        }
    }
}