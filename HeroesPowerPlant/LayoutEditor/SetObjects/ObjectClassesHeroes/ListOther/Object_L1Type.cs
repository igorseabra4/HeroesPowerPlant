namespace HeroesPowerPlant.LayoutEditor
{
    public class Object_L1Type : SetObjectManagerHeroes
    {
        public int Type
        {
            get => ReadInt(4);
            set => Write(4, value);
        }
    }
}