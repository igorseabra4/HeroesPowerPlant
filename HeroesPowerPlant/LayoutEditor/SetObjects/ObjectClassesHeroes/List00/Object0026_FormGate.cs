namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0026_FormGate : SetObjectManagerHeroes
    {
        public Formation Formation
        {
            get { return (Formation)ReadByte(4); }
            set { Write(4, (byte)value); }
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