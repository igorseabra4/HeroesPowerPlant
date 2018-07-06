namespace HeroesPowerPlant.LayoutEditor
{
    public class Object2300_EggAlbatross : SetObjectManagerHeroes
    {
        public float YOffset
        {
            get { return ReadWriteSingle(4); }
            set { ReadWriteSingle(4, value); }
        }
    }
}