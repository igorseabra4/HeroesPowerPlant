namespace HeroesPowerPlant.LayoutEditor
{
    public class Object_B1_1_Type : SetObjectHeroes
    {
        public byte ObjectType
        {
            get => ReadByte(4);
            set => Write(4, value);
        }
    }
}