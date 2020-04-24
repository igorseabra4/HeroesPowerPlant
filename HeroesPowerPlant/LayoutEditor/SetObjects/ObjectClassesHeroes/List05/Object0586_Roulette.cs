namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0586_Roulette : SetObjectHeroes
    {
        public float Scale
        {
            get => ReadFloat(4);
            set => Write(4, value);
        }

        public int Speed
        {
            get => ReadInt(8);
            set => Write(8, value);
        }
    }
}