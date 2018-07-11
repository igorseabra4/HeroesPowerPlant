namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0026_FormGate : SetObjectManagerHeroes
    {
        public Formations Formation
        {
            get { return (Formations)ReadByte(4); }
            set { byte a = (byte)value; Write(4, a); }
        }

        public float Width
        {
            get { return ReadFloat(8); }
            set { Write(8, value); }
        }

        public float Height
        {
            get { return ReadFloat(12); }
            set { Write(12, value); }
        }
    }
}