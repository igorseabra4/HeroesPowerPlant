namespace HeroesPowerPlant.LayoutEditor
{
    public class Object_F1Speed : SetObjectManagerHeroes
    {
        public float Speed
        {
            get { return ReadFloat(4); }
            set { Write(4, value); }
        }
    }
}