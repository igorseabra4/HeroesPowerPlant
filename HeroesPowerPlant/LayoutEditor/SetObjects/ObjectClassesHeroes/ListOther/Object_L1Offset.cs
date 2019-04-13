namespace HeroesPowerPlant.LayoutEditor
{
    public class Object_L1Offset : SetObjectManagerHeroes
    {
        public int Offset
        {
            get => ReadLong(4);
            set => Write(4, value);
        }
    }
}