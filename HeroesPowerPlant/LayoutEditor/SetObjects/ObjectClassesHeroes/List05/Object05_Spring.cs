namespace HeroesPowerPlant.LayoutEditor
{
    public class Object05_Spring : SetObjectManagerHeroes
    {
        public float Power
        {
            get { return ReadWriteSingle(4); }
            set { ReadWriteSingle(4, value); }
        }

        public float Rotation
        {
            get { return ReadWriteSingle(8); }
            set { ReadWriteSingle(8, value); }
        }
    }
}