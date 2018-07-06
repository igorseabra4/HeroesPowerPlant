namespace HeroesPowerPlant.LayoutEditor
{
    public class Object0018_ItemBox : SetObjectManagerHeroes
    {
        public ItemType Item
        {
            get { return (ItemType)ReadWriteByte(4); }
            set { byte a = (byte)value; ReadWriteByte(4, a); }
        }

        public bool HomingOff
        {
            get { return ReadWriteByte(5) != 0; }
            set { ReadWriteByte(5, value ? (byte)1 : (byte)0); }
        }
    }
}