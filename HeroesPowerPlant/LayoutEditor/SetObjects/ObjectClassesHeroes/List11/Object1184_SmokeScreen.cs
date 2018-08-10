namespace HeroesPowerPlant.LayoutEditor
{
    public class Object1184_SmokeScreen : SetObjectManagerHeroes
    {
        public int ModelNumber
        {
            get { return ReadLong(4); }
            set { Write(4, value); }
        }

        public float Speed
        {
            get { return ReadFloat(8); }
            set { Write(8, value); }
        }

        public bool IsUpsideDown
        {
            get { return ReadByte(12) != 0; }
            set { Write(12, (byte)(value ? 1 : 0)); }
        }
    }
}
