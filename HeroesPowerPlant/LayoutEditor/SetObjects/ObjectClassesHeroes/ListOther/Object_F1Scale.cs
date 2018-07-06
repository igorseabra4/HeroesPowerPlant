namespace HeroesPowerPlant.LayoutEditor
{
    public class Object_F1Scale : SetObjectManagerHeroes
    {
        public float Scale
        {
            get { return ReadWriteSingle(4); }
            set { ReadWriteSingle(4, value); }
        }
    }
}