namespace HeroesPowerPlant.LayoutEditor
{
    public class Object050B_Slot : SetObjectManagerHeroes
    {
        public int Rate
        {
            get => ReadLong(4);
            set => Write(4, value);
        }
    }
}