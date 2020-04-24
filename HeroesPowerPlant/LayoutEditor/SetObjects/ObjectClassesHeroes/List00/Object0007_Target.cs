namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0007_Target : SetObjectHeroes
    {
        public Item Item
        {
            get => (Item)ReadByte(4);
            set => Write(4, (byte)value);
        }

        public byte AppearMode
        {
            get => ReadByte(5);
            set => Write(5, value);
        }

        public byte LinkID
        {
            get => ReadByte(6);
            set => Write(6, value);
        }
    }
}