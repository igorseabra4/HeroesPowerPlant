namespace HeroesPowerPlant.LayoutEditor
{
    public class Object_L1Type : SetObjectHeroes
    {
        public int ObjectType
        {
            get => ReadInt(4);
            set => Write(4, value);
        }
    }
}