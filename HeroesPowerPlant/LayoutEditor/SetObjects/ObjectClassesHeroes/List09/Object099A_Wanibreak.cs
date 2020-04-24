namespace HeroesPowerPlant.LayoutEditor
{
    public class Object099A_Wanibreak : SetObjectHeroes
    {
        public byte ObjectType
        {
            get => ReadByte(4);
            set => Write(4, value);
        }

        public byte Kazari
        {
            get => ReadByte(5);
            set => Write(5, value);
        }
    }
}