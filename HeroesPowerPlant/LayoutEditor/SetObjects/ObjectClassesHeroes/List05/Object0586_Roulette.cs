namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0586_Roulette : SetObjectManagerHeroes
    {
        public float Scale
        {
            get => ReadFloat(4);
            set => Write(4, value);
        }

        public int Speed
        {
            get => ReadLong(8);
            set => Write(8, value);
        }
    }
}