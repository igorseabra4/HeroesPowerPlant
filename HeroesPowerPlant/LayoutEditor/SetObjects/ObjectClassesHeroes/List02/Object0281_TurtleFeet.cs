namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0281_TurtleFeet : SetObjectManagerHeroes
    {
        public float Scale
        {
            get { return ReadFloat(4); }
            set { Write(4, value); }
        }

        public float Speed
        {
            get { return ReadFloat(8); }
            set { Write(8, value); }
        }
    }
}
