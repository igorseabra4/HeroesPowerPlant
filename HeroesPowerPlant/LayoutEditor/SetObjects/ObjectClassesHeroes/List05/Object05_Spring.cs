namespace HeroesPowerPlant.LayoutEditor
{
    public class Object05_Spring : SetObjectManagerHeroes
    {
        public float Power
        {
            get { return ReadFloat(4); }
            set { Write(4, value); }
        }

        public float RotSpeed
        {
            get { return ReadFloat(8); }
            set { Write(8, value); }
        }
    }
}