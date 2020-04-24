namespace HeroesPowerPlant.LayoutEditor
{
    public class Object_L1Offset : SetObjectHeroes
    {
        public int Offset
        {
            get => ReadInt(4);
            set => Write(4, value);
        }
    }
}