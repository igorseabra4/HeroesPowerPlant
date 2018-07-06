namespace HeroesPowerPlant.LayoutEditor
{
    public class Object_F1Speed : SetObjectManagerHeroes
    {
        public float Speed
        {
            get { return ReadWriteSingle(4); }
            set { ReadWriteSingle(4, value); }
        }
    }
}