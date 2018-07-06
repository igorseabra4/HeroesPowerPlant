namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0007_Target : SetObjectManagerHeroes
    {
        public ItemType Item
        {
            get { return (ItemType)ReadWriteByte(4); }
            set { byte a = (byte)value; ReadWriteByte(4, a); }
        }

        public byte AppearMode
        {
            get { return ReadWriteByte(5); }
            set { ReadWriteByte(5, value); }
        }

        public byte LinkID
        {
            get { return ReadWriteByte(6); }
            set { ReadWriteByte(6, value); }
        }
    }
}