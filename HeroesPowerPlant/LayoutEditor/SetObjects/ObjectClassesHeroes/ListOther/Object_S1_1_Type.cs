namespace HeroesPowerPlant.LayoutEditor
{
    public class Object_S1_1_Type : SetObjectHeroes
    {
        public short ObjectType
        {
            get => ReadShort(4);
            set => Write(4, value);
        }
    }
}