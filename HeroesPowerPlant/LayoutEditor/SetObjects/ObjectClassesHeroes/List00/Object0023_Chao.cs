namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0023_Chao : SetObjectManagerHeroes
    {
        public float Radius
        {
            get { return ReadFloat(4); }
            set { Write(4, value); }
        }

        public float AngularSpeed
        {
            get { return ReadFloat(8); }
            set { Write(8, value); }
        }
    }
}