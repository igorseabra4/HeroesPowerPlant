namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0023_Chao : SetObjectManagerHeroes
    {
        public float Radius
        {
            get { return ReadWriteSingle(4); }
            set { ReadWriteSingle(4, value); }
        }

        public float AngularSpeed
        {
            get { return ReadWriteSingle(8); }
            set { ReadWriteSingle(8, value); }
        }
    }
}