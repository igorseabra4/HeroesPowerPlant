namespace HeroesPowerPlant.LayoutEditor
{
    public class Object2300_EggAlbatross : SetObjectHeroes
    {
        public float YOffset
        {
            get => ReadFloat(4);
            set => Write(4, value);
        }
    }
}