namespace HeroesPowerPlant.LayoutEditor
{
    public class Object_16_02 : SetObjectManagerHeroes
    {
        public float CoreHP
        {
            get { return ReadFloat(4); }
            set { Write(4, value); }
        }

        public float ShieldHP
        {
            get { return ReadFloat(8); }
            set { Write(8, value); }
        }

        public float MissleHP
        {
            get { return ReadFloat(12); }
            set { Write(12, value); }
        }
    }
}
