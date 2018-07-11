namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0184_LargeBird : SetObjectManagerHeroes
    {
        public float Radius
        {
            get { return ReadFloat(4); }
            set { Write(4, value); }
        }

        public float Speed
        {
            get { return ReadFloat(8); }
            set { Write(8, value); }
        }

        public float Scale
        {
            get { return ReadFloat(12); }
            set { Write(12, value); }
        }
    }
}
