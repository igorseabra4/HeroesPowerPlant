namespace HeroesPowerPlant.LayoutEditor
{
    public class Object_S1_1_Type : SetObjectManagerHeroes
    {
        public short Type
        {
            get => ReadShort(4);
            set => Write(4, value);
        }
    }
}