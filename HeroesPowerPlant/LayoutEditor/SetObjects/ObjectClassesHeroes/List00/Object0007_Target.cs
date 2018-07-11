namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0007_Target : SetObjectManagerHeroes
    {
        public ItemType Item
        {
            get { return (ItemType)ReadByte(4); }
            set { byte a = (byte)value; Write(4, a); }
        }

        public byte AppearMode
        {
            get { return ReadByte(5); }
            set { Write(5, value); }
        }

        public byte LinkID
        {
            get { return ReadByte(6); }
            set { Write(6, value); }
        }
    }
}