namespace HeroesPowerPlant.LayoutEditor
{
    public class Object_L1Type : SetObjectManagerHeroes
    {
        public int Type
        {
            get => ReadLong(4);
            set => Write(4, value);
        }
    }
}