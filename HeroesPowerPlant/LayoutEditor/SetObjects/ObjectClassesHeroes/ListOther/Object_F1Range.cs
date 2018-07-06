namespace HeroesPowerPlant.LayoutEditor
{
    public class Object_F1Range : SetObjectManagerHeroes
    {
        public float Range
        {
            get { return ReadWriteSingle(4); }
            set { ReadWriteSingle(4, value); }
        }
    }
}